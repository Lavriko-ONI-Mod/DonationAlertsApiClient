using Newtonsoft.Json;

namespace DonationAlertsApiClient.Data.Impl;

public class UserData : IUserData
{
    public long Id { get; }
    public string Code { get; }
    public string Name { get; }
    public string Avatar { get; }
    public string Email { get; }
    public string Language { get; }
    
    [JsonProperty("socket_connection_token")]
    public string SocketConnectionToken { get; }

    public UserData(long id, string code, string name, string avatar, string email, string language, string socketConnectionToken)
    {
        Id = id;
        Code = code;
        Name = name;
        Avatar = avatar;
        Email = email;
        Language = language;
        SocketConnectionToken = socketConnectionToken;
    }
}

public class UserDataResponse
{
    public UserData Data { get; }

    public UserDataResponse(UserData data)
    {
        Data = data;
    }
}