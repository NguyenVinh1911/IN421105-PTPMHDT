namespace EduHub.Ch02;

public class ConsoleReportStrategy : IReportStrategy
{
    public void Render(IReadOnlyList<ClubSummary> rows)
    {
        foreach (var r in rows)
            Console.WriteLine($"Club: {r.Name} | Members: {r.MemberCount}");
    }
}

public class CsvReportStrategy : IReportStrategy
{
    private readonly string _path;
    public CsvReportStrategy(string path = "club-report.csv") => _path = path;

    public void Render(IReadOnlyList<ClubSummary> rows) =>
        File.WriteAllLines(_path,
            new[] { "Name,MemberCount" }.Concat(rows.Select(r => $"{r.Name},{r.MemberCount}")));
}