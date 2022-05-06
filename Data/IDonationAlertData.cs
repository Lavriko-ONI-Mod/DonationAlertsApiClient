namespace DonationAlertsApiClient.Data;

public interface IDonationAlertData
{
    long Id { get; }
    string Name { get; }
    string Username { get; }
    string RecipientName { get; }
    string Message { get; }
    string MessageType { get; }
    string PaymentSystem { get; }
    float Amount { get; }
    string Currency { get; }
    bool IsShown { get; }
    float AmountInUserCurrency { get; }
    DateTime? CreatedAt { get; }
    DateTime? ShownAt { get; }
    string Reason { get; }
}