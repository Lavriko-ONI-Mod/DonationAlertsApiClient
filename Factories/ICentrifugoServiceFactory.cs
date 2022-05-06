using DonationAlertsApiClient.Services;

namespace DonationAlertsApiClient.Factories;

public interface ICentrifugoServiceFactory
{
    ICentrifugoService CreateService(string socketConnectionToken);
}