# LAB4 - Hệ thống cửa hàng online e-SHOPPING

## 1. Thông tin bài Lab

- **Tên bài:** LAB4
- **Tên hệ thống:** e-SHOPPING
- **Môn học:** Phân tích và thiết kế phần mềm hướng đối tượng
- **Ngôn ngữ:** C#
- **Nền tảng:** Windows Forms
- **Cơ sở dữ liệu:** SQL Server / LocalDB

## 2. Mô tả hệ thống

Hệ thống e-SHOPPING là phần mềm hỗ trợ quản lý và thực hiện mua hàng trực tuyến.

Hệ thống cho phép khách hàng đăng ký tài khoản, đăng nhập, xem thông tin sản phẩm, quản lý giỏ hàng và thực hiện đặt hàng. Khi đặt hàng, hệ thống hỗ trợ lựa chọn hình thức giao hàng, nhập thông tin người nhận, tính phí giao hàng và thực hiện thanh toán bằng thẻ.

## 3. Chức năng chính

- Đăng ký tài khoản khách hàng.
- Đăng nhập hệ thống.
- Quản lý thông tin khách hàng.
- Xem danh sách và thông tin sản phẩm.
- Quản lý nhóm sản phẩm.
- Quản lý giỏ hàng.
- Thêm, cập nhật và xóa sản phẩm trong giỏ hàng.
- Đặt hàng và tính tổng tiền.
- Lựa chọn hình thức giao hàng.
- Nhập thông tin người nhận.
- Thanh toán bằng thẻ.
- Kiểm tra thông tin thanh toán.
- Lưu thông tin đơn hàng.

## 4. Cơ sở dữ liệu

Database sử dụng:

`EShoppingDB`

Các bảng chính:

- `KhachHang`
- `NhomSanPham`
- `SanPham`
- `GioHang`
- `DonHang`
- `ChiTietDonHang`
- `TheTinDung`

## 5. Công nghệ sử dụng

- C#
- Windows Forms
- .NET
- SQL Server LocalDB
- ADO.NET
- Visual Studio

## 6. Cấu trúc bài Lab

```text
LAB4/
├── README.md
├── EShopping.csproj
└── Các file mã nguồn của hệ thống
