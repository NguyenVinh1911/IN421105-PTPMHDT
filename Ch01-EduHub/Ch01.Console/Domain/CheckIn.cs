namespace EduHub.Ch01.Domain;

/// <summary>Các phương thức check-in hợp lệ.</summary>
public static class CheckInMethod
{
    public const string Manual = "Manual";
    public const string QrCode = "QrCode";
}

/// <summary>Ghi nhận sự hiện diện thực tế của một Registration (quan hệ 1–1).</summary>
public class CheckIn
{
    public Guid Id { get; private set; }
    public Guid RegistrationId { get; private set; }
    public DateTime CheckedInAt { get; private set; }
    public string Method { get; private set; }

    private CheckIn(Guid registrationId, string method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    public static CheckIn Create(Registration registration, string method)
    {
        ArgumentNullException.ThrowIfNull(registration);

        if (method != CheckInMethod.Manual && method != CheckInMethod.QrCode)
            throw new ArgumentException(
                $"Phương thức check-in phải là \"{CheckInMethod.Manual}\" hoặc \"{CheckInMethod.QrCode}\".",
                nameof(method));
        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException(
                $"Chỉ check-in được đăng ký đã {RegistrationStatus.Confirmed} (hiện tại: {registration.Status}).");

        return new CheckIn(registration.Id, method);
    }

    /// <summary>Chuỗi mô tả để in báo cáo.</summary>
    public string Summary()
        => $"Check-in lúc {CheckedInAt.ToLocalTime():HH:mm:ss dd/MM/yyyy} bằng {Method} (phiếu {RegistrationId.ToString()[..8]})";
}
