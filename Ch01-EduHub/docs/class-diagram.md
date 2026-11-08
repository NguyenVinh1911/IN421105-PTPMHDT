# Sơ đồ lớp sơ bộ — EduHub

Vẽ bằng Mermaid (hiển thị trực tiếp trên GitHub) hoặc xuất ảnh từ draw.io rồi đặt cạnh file này.
Ghi rõ thuộc tính chính, khoá ngoại (FK) và bội số quan hệ (1, *, 0..1).

```mermaid
classDiagram
    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +IReadOnlyList~Member~ Members
        +Create(name, description)$ Club
        +AddMember(member) void
        +MemberCount() int
    }
    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +Create(name, email, studentCode)$ Member
        +IsActive() bool
        +ToString() string
    }
    class Event {
        +Guid Id
        +Guid ClubId  FK
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(clubId, title, date, capacity, isOnline)$ Event
        +HasCapacity(registeredCount) bool
    }
    class Registration {
        +Guid Id
        +Guid MemberId  FK
        +Guid EventId  FK
        +DateTime RegisteredAt
        +string Status
        +Create(memberId, eventId)$ Registration
        +Confirm() void
        +Cancel() void
    }
    class CheckIn {
        +Guid Id
        +Guid RegistrationId  FK
        +DateTime CheckedInAt
        +string Method
        +Create(registration, method)$ CheckIn
        +Summary() string
    }

    Club "1" o-- "*" Member : có thành viên
    Club "1" --> "*" Event : tổ chức
    Member "1" --> "*" Registration : thực hiện
    Event "1" --> "*" Registration : nhận
    Registration "1" --> "0..1" CheckIn : được ghi nhận
```

## Giải thích

- **Club – Member (1 – \*)**: một CLB có nhiều thành viên; `Club` giữ danh sách `Members` (chỉ thêm qua `AddMember`).
- **Club – Event (1 – \*)**: một CLB tổ chức nhiều sự kiện; `Event.ClubId` là khoá ngoại tới `Club.Id`.
- **Member – Registration (1 – \*)** và **Event – Registration (1 – \*)**: `Registration` là bảng nối quan hệ nhiều–nhiều giữa Member và Event, giữ hai khoá ngoại `MemberId`, `EventId`.
- **Registration – CheckIn (1 – 0..1)**: mỗi phiếu đăng ký có tối đa một lần check-in; `CheckIn.RegistrationId` là khoá ngoại.
