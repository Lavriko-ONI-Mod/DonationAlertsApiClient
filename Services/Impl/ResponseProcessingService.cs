using DonationAlertsApiClient.Data;
using DonationAlertsApiClient.Helpers;
using Newtonsoft.Json;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;

namespace DonationAlertsApiClient.Services.Impl;

public class ResponseProcessingService : IResponseProcessingService
{
    private readonly ILoggerService _logger;
    private readonly bool _suppressSerializationExceptions;
    private readonly JsonSerializerSettings _serializerSettings;
    
    public event Action<DonationAlertData> ReceivedDonationAlert = delegate { };
    public event Action<PollData> ReceivedPollUpdate = delegate { };
    public event Action<DonationGoalsData> ReceivedDonationGoalsUpdate = delegate { };

    public ResponseProcessingService(ILoggerService logger, bool suppressSerializationExceptions = true)
    {
        _logger = logger;
        _suppressSerializationExceptions = suppressSerializationExceptions;
        _serializerSettings = new JsonSerializerSettings
        {
            Error = OnJsonSerializationError
        };
    }
    
    public void OnResponseReceived(CentrifugoResponse centrifugoResponse)
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
        var donationData = JsonConvert.DeserializeObject<DonationAlertData>(dataJson, _serializerSettings);
        ReceivedDonationAlert(donationData);
    }
    
    private void ProcessPollUpdate(string dataJson)
    {
        var pollData = JsonConvert.DeserializeObject<PollData>(dataJson, _serializerSettings);
        ReceivedPollUpdate(pollData);
    }
    
    private void ProcessDonationGoalsUpdate(string dataJson)
    {
        var goalsData = JsonConvert.DeserializeObject<DonationGoalsData>(dataJson, _serializerSettings);
        ReceivedDonationGoalsUpdate(goalsData);
    }
    
    private void OnJsonSerializationError(object sender, ErrorEventArgs errorArgs)
    {
        _logger.Log(this, errorArgs.ErrorContext.Error.Message);
        if (_suppressSerializationExceptions)
            errorArgs.ErrorContext.Handled = true;
    }
}