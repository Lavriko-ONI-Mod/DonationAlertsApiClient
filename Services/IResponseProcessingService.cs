using DonationAlertsApiClient.Data;

namespace DonationAlertsApiClient.Services;

public interface IResponseProcessingService
{
    event Action<DonationAlertData> ReceivedDonationAlert;
    event Action<PollData> ReceivedPollUpdate;
    event Action<DonationGoalsData> ReceivedDonationGoalsUpdate;
    
    void OnResponseReceived(CentrifugoResponse centrifugoResponse);
}