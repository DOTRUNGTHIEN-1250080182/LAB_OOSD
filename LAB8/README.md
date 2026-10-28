# Đỗ Trung Thiện 
# MSSV: 1250080182
HỆ THỐNG QUẢN LÝ CỬA HÀNG NƯỚC GIẢI KHÁT
Bài 2 – Thực hành Phân tích thiết kế hướng đối tượng  
Ngôn ngữ: C# · Giao diện: Windows Forms · Nền tảng: .NET 8
1. Giới thiệu
Đây là chương trình mẫu quản lý cửa hàng nước giải khát, xây dựng theo bài tập phân tích thiết kế hướng đối tượng. Chương trình sử dụng giao diện Windows Forms, hỗ trợ quản lý danh mục và thực hiện các nghiệp vụ cơ bản: đặt hàng, nhập hàng, bán hàng, hẹn giao hàng và ghi nợ cho khách quen.
2. Cấu trúc thư mục của project
```text
CuaHangNuocGiaiKhat_WinForms/
│
├── CuaHangNuocGiaiKhat.csproj   # Cấu hình project .NET 8 Windows Forms
├── Program.cs                   # Điểm khởi chạy ứng dụng (Main)
├── MainForm.cs                  # Giao diện chính và xử lý các thao tác
├── Models.cs                    # Các lớp đối tượng (Class) nghiệp vụ
├── DataStore.cs                 # Đọc, ghi dữ liệu JSON và tạo dữ liệu mẫu
└── README.md                    # Giới thiệu, cấu trúc và hướng dẫn chạy
```
Giải thích các file:
File	Chức năng
`Program.cs`	Khởi động ứng dụng và mở `MainForm`.
`MainForm.cs`	Tạo các tab, biểu mẫu, bảng dữ liệu và xử lý nút bấm.
`Models.cs`	Định nghĩa các lớp quản lý nước giải khát, khách hàng, hóa đơn, đơn đặt hàng, phiếu hẹn và ghi nợ.
`DataStore.cs`	Lưu và tải dữ liệu từ file JSON; tạo dữ liệu mẫu khi chạy lần đầu.
`CuaHangNuocGiaiKhat.csproj`	Khai báo nền tảng .NET 8 và Windows Forms.
`README.md`	Hướng dẫn sử dụng project.
3. Các chức năng chính
Quản lý nhà cung ứng: Thêm, sửa và xóa khi không còn liên quan đến giao dịch.
Quản lý nước giải khát: Thêm, sửa, xóa, theo dõi đơn giá và số lượng tồn.
Quản lý khách hàng: Thêm, sửa, phân biệt khách quen và khách vãng lai.
Đặt hàng: Lập đơn đặt hàng cho nhà cung ứng.
Nhận hàng: Lập phiếu giao hàng, cập nhật tồn kho, tối đa 3 đợt cho mỗi đơn.
Bán hàng: Kiểm tra tồn kho, lập hóa đơn và xử lý thanh toán.
Phiếu hẹn giao hàng: Theo dõi số lượng còn thiếu và giao tiếp cho khách quen.
Phiếu ghi nợ: Lập, theo dõi và xác nhận thanh toán khoản nợ của khách quen.
4. Các Class nghiệp vụ chính
Chương trình sử dụng 8 Class nghiệp vụ, phù hợp với phiên bản Class Diagram tối giản:
```text
NuocGiaiKhat
NhaCungUng
DonDatHang
PhieuGiaoHang
KhachHang
HoaDon
PhieuHen
PhieuGhiNo
```

