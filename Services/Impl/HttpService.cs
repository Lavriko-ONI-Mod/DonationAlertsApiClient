using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DonationAlertsApiClient.Services.Impl;

public class HttpService : IHttpService
{
    private readonly ILoggerService _loggerService;
    private readonly string _userToken;
    private readonly HttpClient _httpClient;

    public HttpService(ILoggerService loggerService, string userToken)
    {
        _loggerService = loggerService;
        _userToken = userToken;
        _httpClient = new HttpClient();
    }

    public async Task<string> SendRequestAsync(string requestUri, HttpMethod method, params (string Key, object Value)[] parameters)
    {
        using var request = new HttpRequestMessage(method, requestUri);

        if (parameters.Length > 0)
        {
            var param = JsonConvert.SerializeObject(parameters.ToDictionary(pair => pair.Key, pair => pair.Value));
            var content = new StringContent(param);

            request.Content = content;
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _userToken);
        
        var response = await _httpClient.SendAsync(request);

        var result = await response.Content.ReadAsStringAsync();

        _loggerService.Log(this, $"[RECEIVED] {result}");
        
        return result;
    }
}