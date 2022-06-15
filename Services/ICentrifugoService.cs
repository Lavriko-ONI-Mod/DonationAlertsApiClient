using DonationAlertsApiClient.Data;
using Websocket.Client;

namespace DonationAlertsApiClient.Services;

public interface ICentrifugoService
{
    event Action<CentrifugoResponse> ResponseReceived;
    event Action<ReconnectionType> ReconnectionHappened;
    string CentrifugoUserId { get; }
    Task Start();
    Task Connect();
    Task SubscribeToChannel(ChannelSubscriptionData channelSubscriptionData);
    Task SendRequest(CentrifugoRequest request);
    Task UnsubscribeFromChannel(string channel);
}