using DonationAlertsApiClient.Data;

namespace DonationAlertsApiClient.Services;

public interface IResponseProcessingService
{
    event Action<IDonationAlertData> ReceivedDonationAlert;
    event Action<IPollData> ReceivedPollUpdate;
    event Action<IDonationGoalsData> ReceivedDonationGoalsUpdate;
    
    void OnResponseReceived(ICentrifugoResponse centrifugoResponse);
}