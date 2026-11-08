\# Phân tích vi phạm SOLID trong ClubManager



| # | Vị trí | Nguyên tắc | Mô tả | Hậu quả |

|---|--------|-----------|-------|---------|

| 1 | CreateClub | SRP | Một method vừa validate, lưu, ghi log, gửi thông báo | Sửa việc này dễ hỏng việc kia, khó test riêng |

| 2 | CreateClub, AddMemberToClub, CreateEvent, PrintClubReport | DIP | Gọi thẳng File.AppendAllText | Không thay được nơi ghi log, test phải ghi file thật |

| 3 | CreateClub (chuỗi if/else theo email) | OCP | Thêm kênh thông báo (SMS...) phải sửa code cũ | Dễ sinh lỗi, method ngày càng dài |

| 4 | AddMemberToClub | OCP + SRP | Gửi email và Slack cứng trong method nghiệp vụ | Thêm kênh mới phải sửa method |

| 5 | CreateEvent | SRP + OCP | Tạo sự kiện và tự gửi mail cho từng thành viên | Thêm cách báo mới phải sửa CreateEvent |

| 6 | PrintClubReport | OCP + SRP | Báo cáo gắn cứng vào Console, trộn nghiệp vụ và log | Muốn xuất CSV phải sửa method |

| 7 | Toàn class | SRP/ISP | God class gom CLB, thành viên, sự kiện, báo cáo, log | Client chỉ cần một việc vẫn phụ thuộc cả class |

