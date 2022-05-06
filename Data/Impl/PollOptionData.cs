using Newtonsoft.Json;

namespace DonationAlertsApiClient.Data.Impl;

public class PollOptionData
{
    public long Id { get; }
    public string Title { get; }
    
    [JsonProperty("amount_value")]
    public int AmountValue { get; }
    
    [JsonProperty("amount_percent")]
    public float AmountPercent { get; }
    
    [JsonProperty("is_winner")]
    public bool IsWinner { get; }

    public PollOptionData(long id, string title, int amountValue, float amountPercent, bool isWinner)
    {
        Id = id;
        Title = title;
        AmountValue = amountValue;
        AmountPercent = amountPercent;
        IsWinner = isWinner;
    }
}