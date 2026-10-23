# HỆ THỐNG QUẢN LÝ NHÂN SỰ SƠN ĐÔNG Á

## 1. Giới thiệu

Phần mềm quản lý nhân sự Công ty Sơn Đông Á được xây dựng bằng C# Windows Forms, sử dụng SQL Server để lưu trữ dữ liệu.

## 2. Cấu trúc thư mục project

```text
QuanLyNhanSuSonDongA/
│
├── Dependencies/
│
├── Form1.cs
│   ├── Form1.Designer.cs
│   └── Form1.resx
│
├── Program.cs
│
├── README.md
│
└── QuanLyNhanSuSonDongA.csproj
```

## 3. Giải thích cấu trúc

- **Dependencies:** Quản lý các thư viện của project.
- **Form1.cs:** Xử lý giao diện và sự kiện.
- **Form1.Designer.cs:** Chứa mã thiết kế giao diện Windows Forms.
- **Form1.resx:** Lưu tài nguyên của Form.
- **Program.cs:** Điểm khởi chạy chương trình.
- **README.md:** Mô tả thông tin và cấu trúc project.
- **QuanLyNhanSuSonDongA.csproj:** File cấu hình project C#.

## 4. Công nghệ sử dụng

- C# Windows Forms
- .NET 8
- Visual Studio 2026
- SQL Server LocalDB
- Entity Framework Core (dự kiến tích hợp)

## 5. Các chức năng chính

1. Quản lý nhân viên
2. Quản lý tuyển dụng
3. Quản lý hợp đồng
4. Quản lý lương và chức vụ
5. Quản lý bảo hiểm
6. Quản lý chấm công
7. Quản lý khen thưởng, kỷ luật
8. Tính lương
9. Thống kê, báo cáo

## 6. Cơ sở dữ liệu

Database: `QuanLyNhanSuSonDongA`

Cơ sở dữ liệu dự kiến gồm 12 bảng chính, liên kết với nhau bằng khóa chính và khóa ngoại.
