using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Data.Impl;

namespace DonationAlertsApiClient.Services;

public interface IDonationAlertsApiService
{
    Task<IUserData> GetUserData();
    Task<ChannelSubscriptionData[]> GetChannelsData(string channelPrefix, long donationAlertsUserId, string centrifugoUserId);
    Task<IDonationAlertListData> GetDonationAlertsList();
}