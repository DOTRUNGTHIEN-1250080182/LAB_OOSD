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
Ngoài ra, `DuLieu` là lớp chứa các danh sách dữ liệu để ứng dụng lưu/tải; `DataStore` là lớp hỗ trợ lưu trữ; `MainForm` là lớp giao diện. Đây không phải ba Class nghiệp vụ bổ sung của sơ đồ tối giản.
5. Cách mở và chạy trong Visual Studio
Yêu cầu: Windows, Visual Studio 2022 (có workload .NET desktop development) và .NET 8 SDK.
Giải nén thư mục `CuaHangNuocGiaiKhat_WinForms`.
Mở Visual Studio, chọn Open a project or solution.
Chọn file `CuaHangNuocGiaiKhat.csproj`.
Đợi Visual Studio tải project, sau đó nhấn F5 hoặc Start để chạy.
Khi chạy lần đầu, chương trình tự tạo một số dữ liệu mẫu.
Lưu ý: Giao diện được tạo trực tiếp bằng code trong `MainForm.cs`, vì vậy project không có `Form1.Designer.cs`. Project cũng không sử dụng SQL Server hoặc Oracle.
6. Dữ liệu được lưu ở đâu?
Dữ liệu được lưu tự động trong file:
```text
%LOCALAPPDATA%\CuaHangNuocGiaiKhat\data.json
```
Đây là đường dẫn dữ liệu của máy Windows khi chạy chương trình, không phải file nằm sẵn trong thư mục source code.
7. Gợi ý demo trên lớp
Bán hàng bình thường: Mở tab Bán hàng → chọn một sản phẩm còn tồn → nhập số lượng → Thanh toán ngay → Lập hóa đơn.
Khách quen thiếu hàng: Chọn khách quen → nhập số lượng lớn hơn tồn kho → lập hóa đơn → xem tab Phiếu hẹn.
Đặt hàng và nhận hàng: Vào các tab Đặt và nhận hàng → tạo đơn đặt → nhận hàng từng đợt để cập nhật kho.
Ghi nợ: Chọn khách quen → chọn hình thức Ghi nợ → lập hóa đơn → xem tab Phiếu ghi nợ.
8. Giới hạn của phiên bản đơn giản
Mỗi đơn đặt hàng và hóa đơn hiện chỉ hỗ trợ một loại nước giải khát.
Chưa xây dựng bảng chi tiết nhiều mặt hàng cho mỗi chứng từ.
Chưa có đăng nhập, phân quyền, in hóa đơn hay báo cáo thống kê nâng cao.
Dữ liệu JSON chỉ phù hợp với bài thực hành/demo, chưa phù hợp hệ thống nhiều người dùng.
---
Ghi chú: Tài liệu README này mô tả đúng cấu trúc source code hiện tại và là phần có thể đưa vào bài báo cáo thực hành.
