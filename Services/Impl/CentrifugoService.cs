using System.Net.WebSockets;
using System.Text;
using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Factories;
using DonationAlertsApiClient.Helpers;
using Newtonsoft.Json;
using Websocket.Client;

namespace DonationAlertsApiClient.Services.Impl;

public class CentrifugoService : ICentrifugoService
{
    private const string WebsocketUri = "wss://centrifugo.donationalerts.com/connection/websocket";
    
    private readonly TaskCompletionSource<bool> _centrifugoUserIdAcquired;
    private readonly IWebsocketClient _websocketClient;
    private readonly ICentrifugoRequestFactory _requestFactory;
    private readonly ILoggerService _loggerService;
    private readonly string _socketConnectionToken;
    
    public event Action<CentrifugoResponse> ResponseReceived = delegate { };
    public event Action<ReconnectionType> ReconnectionHappened = delegate { };

    public string CentrifugoUserId { get; private set; }
    
    public CentrifugoService(ICentrifugoRequestFactory requestFactory, ILoggerService loggerService, string socketConnectionToken)
    {
        _requestFactory = requestFactory;
        _loggerService = loggerService;
        _socketConnectionToken = socketConnectionToken;
        _centrifugoUserIdAcquired = new TaskCompletionSource<bool>();
        
        _websocketClient = new WebsocketClient(new Uri(WebsocketUri));
        _websocketClient.ReconnectTimeout = TimeSpan.FromSeconds(30);
        //todo: handle reconnection properly
        _websocketClient.ReconnectionHappened.Subscribe(info =>
        {
            _loggerService.Log(this, $"Reconnection happened, type: {info.Type}");
            ReconnectionHappened(info.Type);
        });
        _websocketClient.MessageReceived.Subscribe(OnMessageReceived);

        CentrifugoUserId = string.Empty;
    }

    public async Task Start()
    {
        await _websocketClient.Start();
    }

    public async Task Connect()
    {
        var connectRequest = _requestFactory.CreateRequest(CentrifugoRequestType.Connect, ("token", _socketConnectionToken));
        await SendRequest(connectRequest);

        await WaitForCentrifugoUserId();

        var pingThread = new Thread(PingTask);
        pingThread.Start();
    }

    public async Task SubscribeToChannel(ChannelSubscriptionData channelSubscriptionData)
    {
        var subscribeRequest = _requestFactory.CreateRequest(CentrifugoRequestType.Subscribe, 
            ("channel", channelSubscriptionData.Channel), ("token", channelSubscriptionData.Token));
        
        await SendRequest(subscribeRequest);
    }
    
    public async Task UnsubscribeFromChannel(string channel)
    {
        var subscribeRequest = _requestFactory.CreateRequest(CentrifugoRequestType.Unsubscribe, ("channel", channel));
        
        await SendRequest(subscribeRequest);
    }
    
    public async Task SendRequest(CentrifugoRequest request)
    {
        var requestJson = JsonConvert.SerializeObject(request);
        _loggerService.Log(this, $"[SENT] {requestJson}");
    
        await Task.Run(() => _websocketClient.Send(Encoding.Default.GetBytes(requestJson)));
    }

    private async void PingTask()
    {
        while (_websocketClient.IsRunning)
        {
            Thread.Sleep(ConstantDataHelper.PingPeriodicityMilliseconds);
            await SendRequest(_requestFactory.CreateRequest(CentrifugoRequestType.Ping));
        }
    }
    
    private void OnMessageReceived(ResponseMessage responseMessage)
    {
        switch (responseMessage.MessageType)
        {
            case WebSocketMessageType.Text:
            {
                var messageText = responseMessage.Text.Replace("\n", "");
                _loggerService.Log(this, $"[RECEIVED] {messageText}");

                var message = JsonConvert.DeserializeObject<CentrifugoResponse>(messageText);
                if (message == null) return;

                if (string.IsNullOrEmpty(CentrifugoUserId) && message.Result.TryGetValue("client", out var userId)) 
                    SetUserId(userId.ToString() ?? string.Empty);

                ResponseReceived(message);
                break;
            }
            case WebSocketMessageType.Close:
            case WebSocketMessageType.Binary:
            default:
                _loggerService.Log(this, $"[CLOSED] {responseMessage.Text}");
                break;
        }
    }

    private void SetUserId(string userId)
    {
        CentrifugoUserId = userId;
        
        if (!string.IsNullOrEmpty(CentrifugoUserId))
            _centrifugoUserIdAcquired.SetResult(true);
    }
    
    private async Task WaitForCentrifugoUserId()
    {
        await _centrifugoUserIdAcquired.Task;
    }
}