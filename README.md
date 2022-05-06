# DonationAlertsApiClient
A C# client for the Donation Alerts API. Most features are implemented except from Custom Alerts and Merchandise related stuff. 

Main class is the [DonationAlertsClient](Client/Impl/DonationAlertsClient.cs), all interactions with the API should be done with it.
The only prerequisite is [acquiring the access token](https://www.donationalerts.com/apidoc#authorization) from Donation Alerts.

<br/><br>
<b/>Here's an example of using this library:</b>
```
var loggerService = new LoggerService();
var donationAlertsApiServiceFactory = new DonationAlertsApiServiceFactory(loggerService, userToken);
var centrifugoServiceFactory = new CentrifugoServiceFactory(loggerService);
var responseProcessingService = new ResponseProcessingService();

IDonationAlertsClient donationAlertsClient = 
    new DonationAlertsClient(donationAlertsApiServiceFactory, centrifugoServiceFactory, responseProcessingService);

await donationAlertsClient.Initialise();
await donationAlertsClient.Connect();
await donationAlertsClient.SubscribeToDonationAlerts();
await donationAlertsClient.SubscribeToPolls();
await donationAlertsClient.SubscribeToDonationGoals();
```
