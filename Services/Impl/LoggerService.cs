namespace DonationAlertsApiClient.Services.Impl;

public class LoggerService : ILoggerService
{
    public void Log(object source, string message)
    {
        Console.WriteLine($"[{source.GetType().Name}] {message}");
    }
}