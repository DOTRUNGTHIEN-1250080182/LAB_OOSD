
USE QuanLyNhanSuSonDongA;
GO

-- 1. PHÒNG BAN
IF OBJECT_ID('dbo.PhongBan', 'U') IS NULL
BEGIN
    CREATE TABLE PhongBan (
        MaPhong VARCHAR(10) PRIMARY KEY,
        TenPhong NVARCHAR(100) NOT NULL
    );
END;
GO

-- 2. NHÂN VIÊN
IF OBJECT_ID('dbo.NhanVien', 'U') IS NULL
BEGIN
    CREATE TABLE NhanVien (
        MaNV VARCHAR(10) PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        NgaySinh DATE,
        DiaChi NVARCHAR(200),
        TrangThai NVARCHAR(50),
        MaPhong VARCHAR(10),
        FOREIGN KEY (MaPhong)
            REFERENCES PhongBan(MaPhong)
    );
END;
GO

-- 3. QUYẾT ĐỊNH TUYỂN DỤNG
IF OBJECT_ID('dbo.QuyetDinhTuyenDung', 'U') IS NULL
BEGIN
    CREATE TABLE QuyetDinhTuyenDung (
        MaQD VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        NgayTuyen DATE,
        ThoiGianThuViec INT,
        LuongThuViec DECIMAL(18,2),
        DieuKhoan NVARCHAR(500),
        MaPhong VARCHAR(10),
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV),
        FOREIGN KEY (MaPhong)
            REFERENCES PhongBan(MaPhong)
    );
END;
GO

-- 4. HỢP ĐỒNG
IF OBJECT_ID('dbo.HopDong', 'U') IS NULL
BEGIN
    CREATE TABLE HopDong (
        MaHD VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        LoaiHD NVARCHAR(50),
        NgayKy DATE,
        NgayHetHan DATE,
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV)
    );
END;
GO

-- 5. QUYẾT ĐỊNH LƯƠNG
IF OBJECT_ID('dbo.QuyetDinhLuong', 'U') IS NULL
BEGIN
    CREATE TABLE QuyetDinhLuong (
        MaQDL VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        LuongCoBan DECIMAL(18,2),
        PhuCap DECIMAL(18,2),
        NgayApDung DATE,
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV)
    );
END;
GO

-- 6. QUYẾT ĐỊNH CHỨC VỤ
IF OBJECT_ID('dbo.QuyetDinhChucVu', 'U') IS NULL
BEGIN
    CREATE TABLE QuyetDinhChucVu (
        MaQDCV VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        ChucVu NVARCHAR(100),
        PhuCapChucVu DECIMAL(18,2),
        NgayBoNhiem DATE,
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV)
    );
END;
GO

-- 7. BẢO HIỂM
IF OBJECT_ID('dbo.BaoHiem', 'U') IS NULL
BEGIN
    CREATE TABLE BaoHiem (
        MaBH VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        LoaiBH NVARCHAR(20),
        MucDong DECIMAL(18,2),
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV)
    );
END;
GO

-- 8. BẢNG CHẤM CÔNG
IF OBJECT_ID('dbo.BangChamCong', 'U') IS NULL
BEGIN
    CREATE TABLE BangChamCong (
        MaCC VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        Thang INT,
        Nam INT,
        SoNgayCong INT,
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV)
    );
END;
GO

-- 9. PHIẾU PHỤ TRỘI
IF OBJECT_ID('dbo.PhieuPhuTroi', 'U') IS NULL
BEGIN
    CREATE TABLE PhieuPhuTroi (
        MaPhieu VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        NgayPhuTroi DATE,
        SoGio DECIMAL(5,2),
        HinhThuc NVARCHAR(50),
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV)
    );
END;
GO

-- 10. BẢNG LƯƠNG
IF OBJECT_ID('dbo.BangLuong', 'U') IS NULL
BEGIN
    CREATE TABLE BangLuong (
        MaBL VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NOT NULL,
        Thang INT,
        Nam INT,
        TongLuong DECIMAL(18,2),
        ThucLanh DECIMAL(18,2),
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV)
    );
END;
GO

-- 11. QUYẾT ĐỊNH KHEN THƯỞNG
IF OBJECT_ID('dbo.QuyetDinhKhenThuong', 'U') IS NULL
BEGIN
    CREATE TABLE QuyetDinhKhenThuong (
        MaKT VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NULL,
        MaPhong VARCHAR(10) NULL,
        LyDo NVARCHAR(300),
        SoTien DECIMAL(18,2),
        NgayQuyetDinh DATE,
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV),
        FOREIGN KEY (MaPhong)
            REFERENCES PhongBan(MaPhong),
        CHECK (
            (MaNV IS NOT NULL AND MaPhong IS NULL)
            OR
            (MaNV IS NULL AND MaPhong IS NOT NULL)
        )
    );
END;
GO

-- 12. QUYẾT ĐỊNH KỶ LUẬT
IF OBJECT_ID('dbo.QuyetDinhKyLuat', 'U') IS NULL
BEGIN
    CREATE TABLE QuyetDinhKyLuat (
        MaKL VARCHAR(10) PRIMARY KEY,
        MaNV VARCHAR(10) NULL,
        MaPhong VARCHAR(10) NULL,
        LyDo NVARCHAR(300),
        SoTienPhat DECIMAL(18,2),
        NgayQuyetDinh DATE,
        FOREIGN KEY (MaNV)
            REFERENCES NhanVien(MaNV),
        FOREIGN KEY (MaPhong)
            REFERENCES PhongBan(MaPhong),
        CHECK (
            (MaNV IS NOT NULL AND MaPhong IS NULL)
            OR
            (MaNV IS NULL AND MaPhong IS NOT NULL)
        )
    );
END;
GO

-- KIỂM TRA SỐ BẢNG
SELECT COUNT(*) AS SoLuongBang
FROM sys.tables;
