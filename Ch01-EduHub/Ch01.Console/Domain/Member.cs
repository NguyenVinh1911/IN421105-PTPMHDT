namespace EduHub.Ch01.Domain;

/// <summary>Thành viên sinh viên của câu lạc bộ.</summary>
public class Member
{
    /// <summary>Thời hạn (ngày) kể từ lúc tham gia mà thành viên được xem là còn hoạt động.</summary>
    private const int ActiveDays = 365;

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string StudentCode { get; private set; }
    public DateTime JoinedAt { get; private set; }

    private Member(string name, string email, string studentCode)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.UtcNow;
    }

    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được để trống.", nameof(name));
        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã sinh viên không được để trống.", nameof(studentCode));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ (phải chứa '@').", nameof(email));

        return new Member(name.Trim(), email.Trim(), studentCode.Trim());
    }

    /// <summary>Thành viên còn hoạt động nếu đã tham gia chưa quá 365 ngày.</summary>
    public bool IsActive() => (DateTime.UtcNow - JoinedAt).TotalDays <= ActiveDays;

    public override string ToString() => $"{Name} ({StudentCode})";
}
