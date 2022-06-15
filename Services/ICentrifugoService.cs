using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Data.Impl;
using Websocket.Client;

namespace DonationAlertsApiClient.Services;

public interface ICentrifugoService
{
    event Action<CentrifugoResponse> ResponseReceived;
    event Action<ReconnectionType> ReconnectionHappened;
    string CentrifugoUserId { get; }
    Task Start();
    Task Connect();
    Task SubscribeToChannel(IChannelSubscriptionData channelSubscriptionData);
    Task SendRequest(ICentrifugoRequest request);
    Task UnsubscribeFromChannel(string channel);
}