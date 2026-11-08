namespace EduHub.Ch02;

public class ConsoleNotificationService : INotificationService
{
    private readonly string _channelName;
    public ConsoleNotificationService(string channelName) => _channelName = channelName;

    public void Send(string to, string message) =>
        Console.WriteLine($"[{_channelName}] to {to}: {message}");
}

public static class NotificationChannelResolver
{
    public static NotificationChannel FromEmail(string email) =>
        email.Contains("@gmail.com") ? NotificationChannel.Gmail :
        email.Contains("@yahoo.com") ? NotificationChannel.Yahoo :
        NotificationChannel.GenericEmail;
}

public class NotificationServiceFactory : INotificationServiceFactory
{
    public INotificationService Create(NotificationChannel channel) => channel switch
    {
        NotificationChannel.Gmail => new ConsoleNotificationService("Gmail"),
        NotificationChannel.Yahoo => new ConsoleNotificationService("Yahoo"),
        NotificationChannel.GenericEmail => new ConsoleNotificationService("Email"),
        NotificationChannel.Slack => new ConsoleNotificationService("Slack"),
        _ => throw new ArgumentOutOfRangeException(nameof(channel))
    };
}