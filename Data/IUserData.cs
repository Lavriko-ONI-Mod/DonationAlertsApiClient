namespace DonationAlertsApiClient.Data;

public interface IUserData
{
    long Id { get; }
    string Code { get; }
    string Name { get; }
    string Avatar { get; }
    string Email { get; }
    string Language { get; }
    string SocketConnectionToken { get; }
}