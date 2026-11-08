using EduHub.Ch02.Domain;

namespace EduHub.Ch02;

public class LoggingClubRepository : IClubRepository
{
    private readonly IClubRepository _inner;
    private readonly IAppLogger _logger;

    public LoggingClubRepository(IClubRepository inner, IAppLogger logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public void Add(Club club)
    {
        _logger.Log($"Repository.Add: {club.Name}");
        _inner.Add(club);
    }

    public Club? GetById(Guid id)
    {
        _logger.Log($"Repository.GetById: {id}");
        return _inner.GetById(id);
    }

    public IReadOnlyList<Club> GetAll()
    {
        _logger.Log("Repository.GetAll");
        return _inner.GetAll();
    }
}