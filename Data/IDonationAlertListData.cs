using DonationAlertsApiClient.Data.Impl;

namespace DonationAlertsApiClient.Data;

public interface IDonationAlertListData
{
    DonationAlertListLinksData Links { get; }
    DonationAlertListMetaData Meta { get; }
    DonationAlertData[] Donations { get; }
}