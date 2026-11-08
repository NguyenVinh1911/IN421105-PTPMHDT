using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class ClubService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly IAppLogger _logger;
    private readonly INotificationServiceFactory _notifications;

    public ClubService(IClubRepository clubs, IMemberRepository members,
                       IAppLogger logger, INotificationServiceFactory notifications)
    {
        _clubs = clubs;
        _members = members;
        _logger = logger;
        _notifications = notifications;
    }

    public Club CreateClub(string name, string description, string presidentEmail)
    {
	var club = Club.Create(name, description);
        _clubs.Add(club);
        _logger.Log($"Club created: {name}");

        var channel = NotificationChannelResolver.FromEmail(presidentEmail);

        _notifications.Create(channel).Send(presidentEmail, $"Club '{name}' created");
        return club;
    }

    public Member AddMemberToClub(Guid clubId, string memberName, string email)
    {
        var club = _clubs.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        if (!club.HasRoom(_members.CountByClub(clubId)))
            throw new InvalidOperationException($"Club is full (max {Club.MaxMembers} members)");

        var member = new Member { Id = Guid.NewGuid(), Name = memberName, Email = email, ClubId = clubId };
        _members.Add(member);
        _logger.Log($"Member {memberName} joined {club.Name}");

        _notifications.Create(NotificationChannel.GenericEmail).Send(email, "Welcome!");
        _notifications.Create(NotificationChannel.Slack).Send("#clubs", $"{memberName} joined {club.Name}");
        return member;
    }
}

public class EventService
{
    private readonly IClubRepository _clubs;
    private readonly List<IEventObserver> _observers = new();

    public EventService(IClubRepository clubs) => _clubs = clubs;

    public void Subscribe(IEventObserver observer) => _observers.Add(observer);

    public Event CreateEvent(Guid clubId, string title, DateTime date)

    {
        var club = _clubs.GetById(clubId)
            ?? throw new InvalidOperationException("Club not found");

        var ev = new Event { Id = Guid.NewGuid(), ClubId = clubId, Title = title, Date = date };
        foreach (var observer in _observers)
            observer.OnEventCreated(ev, club);
        return ev;
    }
}

public class ReportService
{
    private readonly IClubRepository _clubs;
    private readonly IMemberRepository _members;
    private readonly IAppLogger _logger;

    public ReportService(IClubRepository clubs, IMemberRepository members, IAppLogger logger)
    {
        _clubs = clubs;
        _members = members;
        _logger = logger;
    }

    public void Generate(IReportStrategy strategy)
    {
        var rows = _clubs.GetAll()
            .Select(c => new ClubSummary(c.Name, _members.CountByClub(c.Id)))
            .ToList();
        strategy.Render(rows);
        _logger.Log($"Report generated ({rows.Count} clubs)");
    }

}