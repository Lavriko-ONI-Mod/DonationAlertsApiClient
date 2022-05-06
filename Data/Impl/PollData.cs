using Newtonsoft.Json;

namespace DonationAlertsApiClient.Data.Impl;

public class PollData : IPollData
{
    public long Id { get; }
    public string Title { get; }
    public string Type { get; }
    public PollOptionData[] Options { get; }
    public string Reason { get; }
    
    [JsonProperty("per_amount_votes")]
    public Dictionary<string, float> PerAmountVotes { get; }
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; }
    
    [JsonProperty("ended_at")]
    public DateTime EndedAt { get; }
    
    [JsonProperty("allow_user_options")]
    public bool AllowUserOptions { get; }
    
    [JsonProperty("is_active")]
    public bool IsActive { get; }

    public PollData(long id, string title, string type, PollOptionData[] options, string reason, Dictionary<string, float> perAmountVotes, DateTime createdAt, DateTime endedAt, bool allowUserOptions, bool isActive)
    {
        Id = id;
        Title = title;
        Type = type;
        Options = options;
        Reason = reason;
        PerAmountVotes = perAmountVotes;
        CreatedAt = createdAt;
        EndedAt = endedAt;
        AllowUserOptions = allowUserOptions;
        IsActive = isActive;
    }
}