namespace DonationAlertsApiClient.Data;

public interface ICentrifugoResponse
{
    int Id { get; }
    Dictionary<string, object> Result { get; }
}