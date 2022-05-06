using DonationAlertsApiClient.Services;
using DonationAlertsApiClient.Services.Impl;

namespace DonationAlertsApiClient.Factories.Impl;

public class CentrifugoServiceFactory : ICentrifugoServiceFactory
{
    private readonly ILoggerService _loggerService;
    private readonly CentrifugoRequestFactory _requestFactory;
    
    public CentrifugoServiceFactory(ILoggerService loggerService)
    {
        _loggerService = loggerService;
        _requestFactory = new CentrifugoRequestFactory(new RequestIdService());
    }

    public ICentrifugoService CreateService(string socketConnectionToken)
    {
        return new CentrifugoService(_requestFactory, _loggerService, socketConnectionToken);
    }
}