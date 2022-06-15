using Newtonsoft.Json;

namespace DonationAlertsApiClient.Data;

public class DonationAlertData
{
    public long Id { get; }
    public string Name { get; }
    public string Username { get; }
    public string Message { get; }
    public float Amount { get; }
    public string Currency { get; }
    public string Reason { get; }
    
    [JsonProperty("recipient_name")]
    public string RecipientName { get; }
    
    [JsonProperty("message_type")]
    public string MessageType { get; }
    
    [JsonProperty("payin_system")]
    public PaymentSystemData PaymentSystem { get; }
    
    [JsonProperty("is_shown")]
    public bool IsShown { get; }
    
    [JsonProperty("amount_in_user_currency")]
    public float AmountInUserCurrency { get; }
    
    [JsonProperty("created_at")]
    public DateTime? CreatedAt { get; }
    
    [JsonProperty("shown_at")]
    public DateTime? ShownAt { get; }
    
    public DonationAlertData(long id, string name, string username, string recipientName, string message, string messageType, PaymentSystemData paymentSystem, float amount, 
        string currency, bool isShown, float amountInUserCurrency, DateTime? createdAt, DateTime? shownAt, string reason)
    {
        Id = id;
        Name = name;
        Username = username;
        RecipientName = recipientName;
        Message = message;
        MessageType = messageType;
        PaymentSystem = paymentSystem;
        Amount = amount;
        Currency = currency;
        IsShown = isShown;
        AmountInUserCurrency = amountInUserCurrency;
        CreatedAt = createdAt;
        ShownAt = shownAt;
        Reason = reason;
    }
}

public class PaymentSystemData
{
    public string Title { get; }

    public PaymentSystemData(string title)
    {
        Title = title;
    }
}