using DonationAlertsApiClient.Data;

namespace DonationAlertsApiClient.Factories;

public interface ICentrifugoRequestFactory
{
    ICentrifugoRequest CreateRequest(CentrifugoRequestType method, params (string Key, object Value)[] parameters);
}