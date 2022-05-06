using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Data.Impl;
using Newtonsoft.Json;

namespace DonationAlertsApiClient.Services.Impl;

public class DonationAlertsApiService : IDonationAlertsApiService
{
    private readonly IHttpService _httpService;

    public DonationAlertsApiService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<IUserData> GetUserData()
    {
        var response = await _httpService.SendRequestAsync("https://www.donationalerts.com/api/v1/user/oauth", HttpMethod.Get);
        return JsonConvert.DeserializeObject<UserDataResponse>(response)?.Data;
    }

    public async Task<ChannelSubscriptionData[]> GetChannelsData(string channelPrefix, long donationAlertsUserId, string centrifugoUserId)
    {
        var response = await _httpService.SendRequestAsync("https://www.donationalerts.com/api/v1/centrifuge/subscribe", HttpMethod.Post,
            ("channels", new[] {$"{channelPrefix}{donationAlertsUserId}"}), ("client", centrifugoUserId));

        return JsonConvert.DeserializeObject<ChannelsSubscriptionDataResponse>(response)?.Channels;
    }

    public async Task<IDonationAlertListData> GetDonationAlertsList()
    {
        var response = await _httpService.SendRequestAsync("https://www.donationalerts.com/api/v1/alerts/donations", HttpMethod.Get);
        return JsonConvert.DeserializeObject<DonationAlertListData>(response);
    }
}