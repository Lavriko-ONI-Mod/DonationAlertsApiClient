using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Services;

namespace DonationAlertsApiClient.Factories.Impl;

public class CentrifugoRequestFactory : ICentrifugoRequestFactory
{
    private readonly IRequestIdService _requestIdService;

    public CentrifugoRequestFactory(IRequestIdService requestIdService)
    {
        _requestIdService = requestIdService;
    }

    public CentrifugoRequest CreateRequest(CentrifugoRequestType method, params (string Key, object Value)[] parameters)
    {
        return new CentrifugoRequest(_requestIdService.GetId(), method, parameters);
    }
}