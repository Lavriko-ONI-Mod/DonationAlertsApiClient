using DonationAlertsApiClient.Data;
using Websocket.Client;

namespace DonationAlertsApiClient.Client;

public interface IDonationAlertsClient
{
    //todo: add summary to events
    event Action<IDonationAlertData> ReceivedDonationAlert;
    event Action<IPollData> ReceivedPollUpdate;
    event Action<IDonationGoalsData> ReceivedDonationGoalsUpdate;
    event Action<ReconnectionType> CentrifugoReconnectionHappened;
    
    /// <summary>
    /// Creates internal services, gets UserData and Socket connection token from DonationAlerts. This method needs to be called first. Requires <b>oauth-user-show</b> scope.
    /// </summary>
    Task Initialise();
    
    /// <summary>
    /// Connects to Centrifugo with the token obtained from the <see cref="Initialise"/> method. This method needs to be called before the subscription methods below.
    /// </summary>
    Task Connect();
    
    /// <summary>
    /// Subscribes to new donation alerts and fires <see cref="ReceivedDonationAlert"/> event. Requires <b>oauth-donation-subscribe</b> scope.
    /// </summary>
    Task SubscribeToDonationAlerts();
    
    /// <summary>
    /// Subscribes to donation goals updates and fires <see cref="ReceivedDonationGoalsUpdate"/> event. Requires <b>oauth-goal-subscribe</b> scope.
    /// </summary>
    Task SubscribeToDonationGoals();
    
    /// <summary>
    /// Subscribes to poll updates and fires <see cref="ReceivedPollUpdate"/> event. Requires <b>oauth-poll-subscribe</b> scope.
    /// </summary>
    Task SubscribeToPolls();
    
    /// <summary>
    /// Gets last donation alerts. Requires <b>oauth-donation-index</b> scope.
    /// </summary>
    Task<IDonationAlertListData> GetDonationAlertsList();

    Task UnsubscribeFromDonationAlerts();
    Task UnsubscribeFromDonationGoals();
    Task UnsubscribeFromPolls();
}