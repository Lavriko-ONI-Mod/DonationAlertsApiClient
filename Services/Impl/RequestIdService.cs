namespace DonationAlertsApiClient.Services.Impl;

public class RequestIdService : IRequestIdService
{
    private int _currentId;
    
    public RequestIdService()
    {
        _currentId = 1;
    }

    public int GetId()
    {
        var id = _currentId;
        _currentId++;
        
        return id;
    }
}