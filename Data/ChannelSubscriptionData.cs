namespace DonationAlertsApiClient.Data;

public class ChannelSubscriptionData
{
    public string Channel { get; }
    public string Token { get; }

    public ChannelSubscriptionData(string channel, string token)
    {
        Channel = channel;
        Token = token;
    }
}

public class ChannelsSubscriptionDataResponse
{
    public ChannelSubscriptionData[] Channels { get; }

    public ChannelsSubscriptionDataResponse(ChannelSubscriptionData[] channels)
    {
        Channels = channels;
    }
}