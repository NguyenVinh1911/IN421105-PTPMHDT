using EduHub.Ch01.Domain;

// ═══════════════════════════════════════════════════════════════
//  EduHub Ch01 — Console demo (luồng nghiệp vụ đầy đủ)
//  Dùng hằng số cho separator, KHÔNG hardcode chuỗi lặp lại.
// ═══════════════════════════════════════════════════════════════

const string Separator = "──────────────────────────────────────────────";
const string HeavySeparator = "══════════════════════════════════════════════";

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("=== EduHub — Club & Event Management ===");

// TODO 1: Tạo CLB "Tech Club" với mô tả phù hợp
Console.WriteLine(Separator);
Console.WriteLine("1) Tạo câu lạc bộ");
var club = Club.Create("Tech Club", "Câu lạc bộ công nghệ cho sinh viên yêu thích lập trình và AI.");
Console.WriteLine($"   CLB       : {club.Name}");
Console.WriteLine($"   Mô tả     : {club.Description}");
Console.WriteLine($"   Ngày tạo  : {club.CreatedAt.ToLocalTime():dd/MM/yyyy HH:mm}");

// TODO 2: Tạo 3 Member, thêm vào CLB
Console.WriteLine(Separator);
Console.WriteLine("2) Tạo thành viên và thêm vào CLB");
var members = new List<Member>
{
    Member.Create("Nguyễn Văn An", "an@example.com", "SV001"),
    Member.Create("Trần Thị Bình", "binh@example.com", "SV002"),
    Member.Create("Lê Minh Châu", "chau@example.com", "SV003"),
};
foreach (var member in members)
{
    club.AddMember(member);
    Console.WriteLine($"   + {member} | {member.Email} | hoạt động: {member.IsActive()}");
}
Console.WriteLine($"   Tổng số thành viên của {club.Name}: {club.MemberCount()}");

// TODO 3: Tạo sự kiện "Workshop AI", capacity = 50
Console.WriteLine(Separator);
Console.WriteLine("3) Tạo sự kiện");
var workshop = Event.Create(club.Id, "Workshop AI", DateTime.Today.AddDays(14), capacity: 50);
Console.WriteLine($"   Sự kiện   : {workshop.Title}");
Console.WriteLine($"   Ngày      : {workshop.Date:dd/MM/yyyy}");
Console.WriteLine($"   Hình thức : {(workshop.IsOnline ? "Online" : "Offline")}");
Console.WriteLine($"   Sức chứa  : {workshop.Capacity}");

// TODO 4: 2 Member đăng ký (kiểm tra HasCapacity) → Pending → gọi Confirm()
Console.WriteLine(Separator);
Console.WriteLine("4) Đăng ký và xác nhận");
var registrations = new List<Registration>();
foreach (var member in members.Take(2))
{
    if (!workshop.HasCapacity(registrations.Count))
    {
        Console.WriteLine($"   ! {member.Name}: sự kiện đã hết chỗ.");
        continue;
    }

    var registration = Registration.Create(member.Id, workshop.Id);
    registrations.Add(registration);
    Console.WriteLine($"   {member.Name} đăng ký → {registration.Status}");

    registration.Confirm();
    Console.WriteLine($"   {member.Name} được xác nhận → {registration.Status}");
}
Console.WriteLine($"   Số đăng ký / sức chứa: {registrations.Count}/{workshop.Capacity}");

// TODO 5: 1 Member check-in
Console.WriteLine(Separator);
Console.WriteLine("5) Check-in");
var checkIns = new List<CheckIn>();
var firstRegistration = registrations[0];
var checkIn = CheckIn.Create(firstRegistration, CheckInMethod.QrCode);
checkIns.Add(checkIn);
var checkedInMember = members.First(m => m.Id == firstRegistration.MemberId);
Console.WriteLine($"   {checkedInMember.Name}: {checkIn.Summary()}");

// TODO 6: In báo cáo tham dự: số đăng ký, số check-in, danh sách tên
Console.WriteLine(HeavySeparator);
Console.WriteLine($"BÁO CÁO THAM DỰ — {workshop.Title}");
Console.WriteLine(HeavySeparator);
Console.WriteLine($"Số đăng ký : {registrations.Count}/{workshop.Capacity}");
Console.WriteLine($"Số check-in: {checkIns.Count}");
Console.WriteLine("Danh sách tên:");
foreach (var registration in registrations)
{
    var member = members.First(m => m.Id == registration.MemberId);
    var attended = checkIns.Any(c => c.RegistrationId == registration.Id) ? "đã check-in" : "chưa check-in";
    Console.WriteLine($"   - {member.Name} ({registration.Status}, {attended})");
}
Console.WriteLine(HeavySeparator);

Console.WriteLine("\n✅ Demo complete!");
