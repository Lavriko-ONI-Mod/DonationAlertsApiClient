using DonationAlertsApiClient.Data.Impl;

namespace DonationAlertsApiClient.Data;

public interface IPollData
{
    long Id { get; }
    string Title { get; }
    string Type { get; }
    PollOptionData[] Options { get; }
    string Reason { get; }
    Dictionary<string, float> PerAmountVotes { get; }
    DateTime CreatedAt { get; }
    DateTime EndedAt { get; }
    bool AllowUserOptions { get; }
    bool IsActive { get; }
}