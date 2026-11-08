# User Stories — EduHub

Viết ít nhất 5 User Story, đủ 3 vai trò (Sinh viên, Ban CLB, Admin).
Mỗi story có ít nhất 2 tiêu chí nghiệm thu (Given/When/Then hoặc gạch đầu dòng).

## US-01 · Sinh viên: đăng ký tham gia sự kiện

Là sinh viên, tôi muốn đăng ký tham gia sự kiện của câu lạc bộ, để giữ chỗ trước khi sự kiện diễn ra.

- Tiêu chí 1: Given sự kiện còn chỗ, When tôi bấm đăng ký, Then hệ thống tạo phiếu đăng ký với trạng thái Pending.
- Tiêu chí 2: Given sự kiện đã đủ số lượng đăng ký, When tôi bấm đăng ký, Then hệ thống từ chối và báo "hết chỗ".
- Tiêu chí 3: Given tôi đã đăng ký sự kiện này, When tôi đăng ký lần nữa, Then hệ thống không tạo thêm phiếu trùng.

## US-02 · Sinh viên: xem danh sách sự kiện sắp diễn ra

Là sinh viên, tôi muốn xem các sự kiện sắp diễn ra của câu lạc bộ, để chọn sự kiện phù hợp với mình.

- Tiêu chí 1: Given có sự kiện sắp diễn ra, When tôi mở trang sự kiện, Then tôi thấy tên, thời gian, hình thức (online/offline) và số chỗ còn lại.
- Tiêu chí 2: Given không có sự kiện nào sắp diễn ra, When tôi mở trang sự kiện, Then tôi thấy thông báo "chưa có sự kiện".

## US-03 · Ban CLB: tạo sự kiện

Là thành viên Ban CLB, tôi muốn tạo sự kiện với sức chứa tối đa, để kiểm soát số người tham dự.

- Tiêu chí 1: Given tôi thuộc Ban CLB, When tôi nhập tên sự kiện, ngày và capacity > 0, Then sự kiện được tạo và gắn với CLB của tôi.
- Tiêu chí 2: Given tôi nhập capacity <= 0 hoặc để trống tên, When tôi lưu, Then hệ thống báo lỗi và không tạo sự kiện.

## US-04 · Ban CLB: xác nhận đăng ký và check-in

Là thành viên Ban CLB, tôi muốn xác nhận đăng ký và check-in người tham dự, để thống kê số người đến thực tế.

- Tiêu chí 1: Given phiếu đăng ký đang Pending, When tôi xác nhận, Then trạng thái chuyển thành Confirmed.
- Tiêu chí 2: Given phiếu đăng ký đã Confirmed, When người đó đến sự kiện và tôi check-in, Then hệ thống ghi nhận thời điểm và phương thức check-in (Manual hoặc QrCode).
- Tiêu chí 3: Given phiếu đăng ký chưa Confirmed hoặc đã Cancelled, When tôi check-in, Then hệ thống từ chối.

## US-05 · Admin: quản lý câu lạc bộ

Là Admin, tôi muốn tạo câu lạc bộ và thêm thành viên vào câu lạc bộ, để các CLB có không gian tổ chức hoạt động.

- Tiêu chí 1: Given tôi là Admin, When tôi nhập tên và mô tả CLB hợp lệ, Then CLB mới được tạo.
- Tiêu chí 2: Given tên CLB để trống, When tôi lưu, Then hệ thống báo lỗi và không tạo CLB.
- Tiêu chí 3: Given sinh viên đã là thành viên của CLB, When tôi thêm lại sinh viên đó, Then hệ thống từ chối thêm trùng.

## US-06 · Admin: xem báo cáo tham dự

Là Admin, tôi muốn xem báo cáo số đăng ký và số check-in của từng sự kiện, để đánh giá hiệu quả hoạt động của CLB.

- Tiêu chí 1: Given sự kiện có đăng ký, When tôi mở báo cáo, Then tôi thấy tổng số đăng ký, tổng số check-in và danh sách tên người tham dự.
- Tiêu chí 2: Given sự kiện chưa có ai đăng ký, When tôi mở báo cáo, Then các số liệu hiển thị là 0.
