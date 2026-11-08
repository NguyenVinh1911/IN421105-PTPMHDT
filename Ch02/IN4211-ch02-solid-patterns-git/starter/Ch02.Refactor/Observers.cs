using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class EmailNotifier : IEventObserver
{
    private readonly IMemberRepository _members;
    private readonly INotificationServiceFactory _factory;

    public EmailNotifier(IMemberRepository members, INotificationServiceFactory factory)
    {
        _members = members;
        _factory = factory;
    }

    public void OnEventCreated(Event ev, Club club)
    {
        var email = _factory.Create(NotificationChannel.GenericEmail);
        foreach (var m in _members.GetByClub(club.Id))
            email.Send(m.Email, $"new event '{ev.Title}' at {club.Name}");
    }
}

public class LogNotifier : IEventObserver
{
    private readonly IAppLogger _logger;
    public LogNotifier(IAppLogger logger) => _logger = logger;

    public void OnEventCreated(Event ev, Club club) =>
        _logger.Log($"Event created: {ev.Title}");
}