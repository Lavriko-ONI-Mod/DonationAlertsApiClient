using Newtonsoft.Json;

namespace DonationAlertsApiClient.Data;

public class DonationAlertListData
{
    public DonationAlertListLinksData Links { get; }
    public DonationAlertListMetaData Meta { get; }
    
    [JsonProperty("data")]
    public DonationAlertData[] Donations { get; }

    public DonationAlertListData(DonationAlertListLinksData links, DonationAlertListMetaData meta, DonationAlertData[] donations)
    {
        Links = links;
        Meta = meta;
        Donations = donations;
    }
}

public class DonationAlertListLinksData
{
    public string First { get; }
    public string Last { get; }
    
    public DonationAlertListLinksData(string first, string last)
    {
        First = first;
        Last = last;
    }
}

public class DonationAlertListMetaData
{
    public int From { get; }
    public int To { get; }
    public int Total { get; }
    public DonationAlertMetaLinkData[] Links { get; }
    public string Path { get; }

    [JsonProperty("current_page")]
    public int CurrentPage { get; }
    
    [JsonProperty("last_page")]
    public int LastPage { get; }
    
    [JsonProperty("per_page")]
    public int PerPage { get; }

    public DonationAlertListMetaData(int from, int to, int total, DonationAlertMetaLinkData[] links, string path, int currentPage, int lastPage, int perPage)
    {
        From = from;
        To = to;
        Total = total;
        Links = links;
        Path = path;
        CurrentPage = currentPage;
        LastPage = lastPage;
        PerPage = perPage;
    }
}

public class DonationAlertMetaLinkData
{
    public string Url { get; }
    public string Label { get; }
    public bool Active { get; }

    public DonationAlertMetaLinkData(string url, string label, bool active)
    {
        Url = url;
        Label = label;
        Active = active;
    }
}