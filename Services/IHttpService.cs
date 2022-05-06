namespace DonationAlertsApiClient.Services;

public interface IHttpService
{
    Task<string> SendRequestAsync(string requestUri, HttpMethod method, params (string Key, object Value)[] parameters);
}