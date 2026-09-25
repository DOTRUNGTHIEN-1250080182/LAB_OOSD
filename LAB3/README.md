# LAB 3 - HỆ THỐNG QUẢN LÝ KHÁCH SẠN

**Sinh viên:** Đỗ Trung Thiện  
**MSSV:** 1250080182  


## 1. Thông tin bài thực hành

- Đề tài: Hệ thống quản lý khách sạn
- Ngôn ngữ lập trình: C#
- Giao diện: Windows Forms
- Cơ sở dữ liệu: SQL Server LocalDB
- Công nghệ truy cập dữ liệu: ADO.NET
- Môi trường phát triển: Visual Studio

## 2. Mục tiêu

Xây dựng ứng dụng quản lý khách sạn nhằm hỗ trợ nhân viên thực hiện các nghiệp vụ quản lý phòng, khách hàng, đặt phòng, dịch vụ, trả phòng, thanh toán và thống kê doanh thu.

## 3. Các chức năng chính

### Quản lý danh mục
- Quản lý khu vực
- Quản lý nhân viên
- Quản lý loại tiện nghi
- Quản lý dịch vụ
- Quản lý quy định đền bù

### Quản lý phòng và tiện nghi
- Thêm, sửa, xóa phòng
- Quản lý số người tối đa
- Quản lý đơn giá phòng
- Quản lý tiện nghi

### Đặt phòng
- Quản lý thông tin khách hàng
- Lập phiếu đặt phòng
- Chọn phòng
- Nhập ngày nhận và ngày trả dự kiến
- Quản lý tiền cọc
- Quản lý kênh đặt phòng

### Quản lý dịch vụ
- Quản lý thông tin dịch vụ
- Ghi nhận dịch vụ khách sử dụng
- Quản lý số lượng dịch vụ sử dụng

### Trả phòng và thanh toán
- Kiểm tra thông tin phiếu đặt phòng
- Tính tiền phòng
- Tổng hợp tiền dịch vụ
- Lập hóa đơn
- Ghi nhận thanh toán

### Thống kê
- Hiển thị danh sách hóa đơn
- Thống kê tổng tiền phòng
- Thống kê tổng tiền dịch vụ
- Thống kê tổng doanh thu

## 4. Cơ sở dữ liệu

Tên cơ sở dữ liệu:

`QuanLyKhachSan`

SQL Server:

`(localdb)\MSSQLLocalDB`

Ứng dụng sử dụng lớp `Db` để kết nối với cơ sở dữ liệu SQL Server LocalDB.

## 5. Cấu trúc chương trình

```text
LAB3
├── Data
│   └── Db.cs
│
├── Forms
│   ├── FrmMain.cs
│   ├── FrmDanhMuc.cs
│   ├── FrmPhongTienNghi.cs
│   ├── FrmDatPhong.cs
│   ├── FrmDichVu.cs
│   ├── FrmTraPhong.cs
│   └── FrmThongKe.cs
│
├── Services
│   ├── KhuVucService.cs
│   ├── DatPhongService.cs
│   ├── DichVuService.cs
│   ├── PhongTienNghiService.cs
│   ├── TraPhongService.cs
│   └── ThongKeService.cs
│
├── Properties
├── Program.cs
├── App.config
├── QuanLyKhachSan.csproj
├── QuanLyKhachSan.slnx
└── QuanLyKhachSan.sql
