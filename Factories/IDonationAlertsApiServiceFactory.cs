using DonationAlertsApiClient.Services;

namespace DonationAlertsApiClient.Factories;

public interface IDonationAlertsApiServiceFactory
{
    IDonationAlertsApiService CreateService();
}