using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class FileAppLogger : IAppLogger
{
    private readonly string _path;
    public FileAppLogger(string path = "club-log.txt") => _path = path;
    public void Log(string message) =>
        File.AppendAllText(_path, $"[{DateTime.Now}] {message}\n");
}

public class InMemoryClubRepository : IClubRepository
{
    private readonly List<Club> _clubs = new();
    public void Add(Club club) => _clubs.Add(club);
    public Club? GetById(Guid id) => _clubs.FirstOrDefault(c => c.Id == id);
    public IReadOnlyList<Club> GetAll() => _clubs.AsReadOnly();
}

public class InMemoryMemberRepository : IMemberRepository
{
    private readonly List<Member> _members = new();
    public void Add(Member member) => _members.Add(member);
    public int CountByClub(Guid clubId) => _members.Count(m => m.ClubId == clubId);
    public IReadOnlyList<Member> GetByClub(Guid clubId) =>
        _members.Where(m => m.ClubId == clubId).ToList();
}