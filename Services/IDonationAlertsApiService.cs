using DonationAlertsApiClient.Data;

namespace DonationAlertsApiClient.Services;

public interface IDonationAlertsApiService
{
    Task<UserData> GetUserData();
    Task<ChannelSubscriptionData[]> GetChannelsData(string channelPrefix, long donationAlertsUserId, string centrifugoUserId);
    Task<DonationAlertListData> GetDonationAlertsList();
}