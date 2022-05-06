namespace DonationAlertsApiClient.Data;

public interface IDonationGoalsData
{
    long Id { get; }
    string Title { get; }
    string Currency { get; }
    string Reason { get; }
    bool IsActive { get; }
    float StartAmount { get; }
    float RaisedAmount { get; }
    float GoalAmount { get; }
    DateTime StartedAt { get; }
    DateTime ExpiresAt { get; }
}