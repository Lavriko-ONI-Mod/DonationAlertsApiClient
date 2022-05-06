using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Data.Impl;
using DonationAlertsApiClient.Helpers;
using Newtonsoft.Json;

namespace DonationAlertsApiClient.Services.Impl;

public class ResponseProcessingService : IResponseProcessingService
{
    public event Action<IDonationAlertData> ReceivedDonationAlert = delegate { };
    public event Action<IPollData> ReceivedPollUpdate = delegate { };
    public event Action<IDonationGoalsData> ReceivedDonationGoalsUpdate = delegate { };

    public void OnResponseReceived(ICentrifugoResponse centrifugoResponse)
    {
        if (centrifugoResponse.Result == null || !centrifugoResponse.Result.ContainsKey("data")) return;
        if (!centrifugoResponse.Result.TryGetValue("channel", out var channel)) return;
            
        var channelName = channel.ToString();
        if (channelName == null) return;
                
        var dataParent = JsonConvert.DeserializeObject<Dictionary<string, object>>(centrifugoResponse.Result["data"].ToString() ?? string.Empty);
        if (dataParent == null || !dataParent.ContainsKey("data")) return;
        
        var dataJson = dataParent["data"].ToString() ?? string.Empty;
        
        if (channelName.Contains(ConstantDataHelper.DonationAlertsPrefix))
            ProcessDonationAlert(dataJson);
        else if (channelName.Contains(ConstantDataHelper.PollsPrefix))
            ProcessPollUpdate(dataJson);
        else if (channelName.Contains(ConstantDataHelper.DonationGoalsPrefix))
            ProcessDonationGoalsUpdate(dataJson);
    }

    private void ProcessDonationAlert(string dataJson)
    {
        var donationData = JsonConvert.DeserializeObject<DonationAlertData>(dataJson);
        ReceivedDonationAlert(donationData);
    }
    
    private void ProcessPollUpdate(string dataJson)
    {
        var pollData = JsonConvert.DeserializeObject<PollData>(dataJson);
        ReceivedPollUpdate(pollData);
    }
    
    private void ProcessDonationGoalsUpdate(string dataJson)
    {
        var goalsData = JsonConvert.DeserializeObject<DonationGoalsData>(dataJson);
        ReceivedDonationGoalsUpdate(goalsData);
    }
}