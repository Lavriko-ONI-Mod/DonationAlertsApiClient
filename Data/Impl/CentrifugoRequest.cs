namespace DonationAlertsApiClient.Data.Impl;

public class CentrifugoRequest : ICentrifugoRequest
{
    public int Id { get; }
    public CentrifugoRequestType Method { get; }
    public Dictionary<string, object> Params { get; }

    public CentrifugoRequest(int id, CentrifugoRequestType method, params (string Key, object Value)[] parameters)
    {
        Id = id;
        Method = method;
        Params = parameters.ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}