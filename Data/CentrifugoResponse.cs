namespace DonationAlertsApiClient.Data;

public class CentrifugoResponse
{
    public int Id { get; }
    public Dictionary<string, object> Result { get; }

    public CentrifugoResponse(int id, Dictionary<string, object> result)
    {
        Id = id;
        Result = result;
    }
}