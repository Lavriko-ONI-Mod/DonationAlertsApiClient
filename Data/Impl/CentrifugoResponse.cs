namespace DonationAlertsApiClient.Data.Impl;

public class CentrifugoResponse : ICentrifugoResponse
{
    public int Id { get; }
    public Dictionary<string, object> Result { get; }

    public CentrifugoResponse(int id, Dictionary<string, object> result)
    {
        Id = id;
        Result = result;
    }
}