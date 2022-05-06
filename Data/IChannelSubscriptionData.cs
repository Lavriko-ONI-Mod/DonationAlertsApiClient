namespace DonationAlertsApiClient.Data;

public interface IChannelSubscriptionData
{
    string Channel { get; }
    string Token { get; }
}