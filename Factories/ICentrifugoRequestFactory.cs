using DonationAlertsApiClient.Data;

namespace DonationAlertsApiClient.Factories;

public interface ICentrifugoRequestFactory
{
    CentrifugoRequest CreateRequest(CentrifugoRequestType method, params (string Key, object Value)[] parameters);
}