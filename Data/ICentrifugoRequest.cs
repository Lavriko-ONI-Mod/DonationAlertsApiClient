namespace DonationAlertsApiClient.Data;

public interface ICentrifugoRequest
{
    int Id { get; }
    CentrifugoRequestType Method { get; }
    Dictionary<string, object> Params { get; }
}