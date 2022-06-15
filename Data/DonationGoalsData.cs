using Newtonsoft.Json;

namespace DonationAlertsApiClient.Data;

public class DonationGoalsData
{
    public long Id { get; }
    public string Title { get; }
    public string Currency { get; }
    public string Reason { get; }
    
    [JsonProperty("is_active")]
    public bool IsActive { get; }
    
    [JsonProperty("start_amount")]
    public float StartAmount { get; }
    
    [JsonProperty("raised_amount")]
    public float RaisedAmount { get; }
    
    [JsonProperty("goal_amount")]
    public float GoalAmount { get; }
    
    [JsonProperty("started_at")]
    public DateTime StartedAt { get; }
    
    [JsonProperty("expires_at")]
    public DateTime ExpiresAt { get; }

    public DonationGoalsData(long id, string title, string currency, string reason, bool isActive, float startAmount, float raisedAmount, float goalAmount, DateTime startedAt, DateTime expiresAt)
    {
        Id = id;
        Title = title;
        Currency = currency;
        Reason = reason;
        IsActive = isActive;
        StartAmount = startAmount;
        RaisedAmount = raisedAmount;
        GoalAmount = goalAmount;
        StartedAt = startedAt;
        ExpiresAt = expiresAt;
    }
}