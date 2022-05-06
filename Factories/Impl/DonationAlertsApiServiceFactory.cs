using DonationAlertsApiClient.Services;
using DonationAlertsApiClient.Services.Impl;

namespace DonationAlertsApiClient.Factories.Impl;

public class DonationAlertsApiServiceFactory : IDonationAlertsApiServiceFactory
{
    private readonly IHttpService _httpService;
    
    public DonationAlertsApiServiceFactory(ILoggerService loggerService, string userToken)
    {
        _httpService = new HttpService(loggerService, userToken);
    }

    public IDonationAlertsApiService CreateService()
    {
        return new DonationAlertsApiService(_httpService);
    }
}