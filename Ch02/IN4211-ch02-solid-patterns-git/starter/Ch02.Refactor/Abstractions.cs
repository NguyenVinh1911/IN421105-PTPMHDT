using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public interface IAppLogger { void Log(string message); }

public interface INotificationService { void Send(string to, string message); }

public enum NotificationChannel { Gmail, Yahoo, GenericEmail, Slack }

public interface INotificationServiceFactory
{
    INotificationService Create(NotificationChannel channel);
}

public interface IClubRepository
{
    void Add(Club club);
    Club? GetById(Guid id);
    IReadOnlyList<Club> GetAll();
}

public interface IMemberRepository
{
    void Add(Member member);
    int CountByClub(Guid clubId);
    IReadOnlyList<Member> GetByClub(Guid clubId);
}

public interface IEventObserver { void OnEventCreated(Event ev, Club club); }

public record ClubSummary(string Name, int MemberCount);


public interface IReportStrategy { void Render(IReadOnlyList<ClubSummary> rows); }