using EduHub.Ch02;

IAppLogger logger = new FileAppLogger();
IClubRepository clubs = new LoggingClubRepository(new InMemoryClubRepository(), logger);
IMemberRepository members = new InMemoryMemberRepository();
INotificationServiceFactory factory = new NotificationServiceFactory();

var clubService = new ClubService(clubs, members, logger, factory);
var eventService = new EventService(clubs);
eventService.Subscribe(new EmailNotifier(members, factory));
eventService.Subscribe(new LogNotifier(logger));
var reportService = new ReportService(clubs, members, logger);

var club = clubService.CreateClub("Tech Club", "Câu lạc bộ công nghệ", "president@gmail.com");
clubService.AddMemberToClub(club.Id, "Nguyễn Văn A", "a@student.dthu.edu.vn");
eventService.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

reportService.Generate(new ConsoleReportStrategy());
reportService.Generate(new CsvReportStrategy());