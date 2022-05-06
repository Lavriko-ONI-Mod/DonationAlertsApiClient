using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Data.Impl;

namespace DonationAlertsApiClient.Services;

public interface ICentrifugoService
{
    event Action<CentrifugoResponse> ResponseReceived;
    string CentrifugoUserId { get; }
    Task Start();
    Task Connect();
    Task SubscribeToChannel(IChannelSubscriptionData channelSubscriptionData);
    Task SendRequest(ICentrifugoRequest request);
    Task UnsubscribeFromChannel(string channel);
}