using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class TraPhongService
    {
        // =====================================================
        // 1. LẤY DANH SÁCH PHIẾU ĐẶT ĐỂ TRẢ PHÒNG
        // =====================================================

        public DataTable LayDanhSachPhieuDat()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        pdp.SoPhieuDat,
                        kh.HoTen,
                        pdp.NgayNhan,
                        pdp.NgayTraDuKien,
                        pdp.TienCoc,
                        pdp.TrangThai
                    FROM PhieuDatPhong pdp
                    INNER JOIN KhachHang kh
                        ON pdp.MaKhach = kh.MaKhach
                    WHERE pdp.TrangThai IN (N'Đã đặt', N'Đang ở')
                    ORDER BY pdp.SoPhieuDat";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // =====================================================
        // 2. LẤY THÔNG TIN PHÒNG TRONG PHIẾU ĐẶT
        // =====================================================

        public DataTable LayChiTietPhong(string soPhieuDat)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        ct.SoPhieuDat,
                        ct.SoPhong,
                        ct.SoNguoi,
                        p.SoNguoiToiDa,
                        p.DonGiaNgay
                    FROM ChiTietDatPhong ct
                    INNER JOIN Phong p
                        ON ct.SoPhong = p.SoPhong
                    WHERE ct.SoPhieuDat = @SoPhieuDat
                    ORDER BY ct.SoPhong";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhieuDat", soPhieuDat);

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        // =====================================================
        // 3. TÍNH TIỀN DỊCH VỤ
        // =====================================================

        public decimal LayTienDichVu(string soPhieuDat)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT ISNULL(SUM(ct.SoLuong * ct.DonGia), 0)
                    FROM PhieuSuDungDV ps
                    INNER JOIN ChiTietPhieuSuDungDV ct
                        ON ps.SoPhieuSDDV = ct.SoPhieuSDDV
                    WHERE ps.SoPhieuDat = @SoPhieuDat";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@SoPhieuDat",
                        soPhieuDat);

                    conn.Open();

                    object result = cmd.ExecuteScalar();

                    return result == null ||
                           result == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(result);
                }
            }
        }

        // =====================================================
        // 4. LẤY HÓA ĐƠN
        // =====================================================

        public DataTable LayDanhSachHoaDon()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        hd.SoHoaDon,
                        hd.SoPhieuDat,
                        hd.NgayLap,
                        hd.MaNV,
                        hd.SoNgayTinhTien,
                        hd.TienPhong,
                        hd.TienDichVu,
                        hd.TongTien,
                        hd.TrangThai
                    FROM HoaDon hd
                    ORDER BY hd.NgayLap DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // =====================================================
        // 5. TẠO HÓA ĐƠN
        // =====================================================

        public bool ThemHoaDon(
            string soHoaDon,
            string soPhieuDat,
            DateTime ngayLap,
            string maNV,
            int soNgayTinhTien,
            decimal tienPhong,
            decimal tienDichVu)
        {
            if (soNgayTinhTien <= 0)
                return false;

            if (tienPhong < 0 || tienDichVu < 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO HoaDon
                    (
                        SoHoaDon,
                        SoPhieuDat,
                        NgayLap,
                        MaNV,
                        SoNgayTinhTien,
                        TienPhong,
                        TienDichVu,
                        TrangThai
                    )
                    VALUES
                    (
                        @SoHoaDon,
                        @SoPhieuDat,
                        @NgayLap,
                        @MaNV,
                        @SoNgayTinhTien,
                        @TienPhong,
                        @TienDichVu,
                        N'Chưa thanh toán'
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@SoHoaDon",
                        soHoaDon);

                    cmd.Parameters.AddWithValue(
                        "@SoPhieuDat",
                        soPhieuDat);

                    cmd.Parameters.AddWithValue(
                        "@NgayLap",
                        ngayLap);

                    cmd.Parameters.AddWithValue(
                        "@MaNV",
                        maNV);

                    cmd.Parameters.AddWithValue(
                        "@SoNgayTinhTien",
                        soNgayTinhTien);

                    cmd.Parameters.AddWithValue(
                        "@TienPhong",
                        tienPhong);

                    cmd.Parameters.AddWithValue(
                        "@TienDichVu",
                        tienDichVu);

                    conn.Open();

                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
            }
        }

        // =====================================================
        // 6. LẤY DANH SÁCH THANH TOÁN
        // =====================================================

        public DataTable LayDanhSachThanhToan()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        MaThanhToan,
                        SoHoaDon,
                        NgayThanhToan,
                        HinhThuc,
                        SoTien
                    FROM ThanhToan
                    ORDER BY NgayThanhToan DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // =====================================================
        // 7. THÊM THANH TOÁN
        // =====================================================

        public bool ThemThanhToan(
            string maThanhToan,
            string soHoaDon,
            DateTime ngayThanhToan,
            string hinhThuc,
            decimal soTien)
        {
            if (soTien <= 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();

                    transaction = conn.BeginTransaction();

                    string sqlThanhToan = @"
                        INSERT INTO ThanhToan
                        (
                            MaThanhToan,
                            SoHoaDon,
                            NgayThanhToan,
                            HinhThuc,
                            SoTien
                        )
                        VALUES
                        (
                            @MaThanhToan,
                            @SoHoaDon,
                            @NgayThanhToan,
                            @HinhThuc,
                            @SoTien
                        )";

                    using (SqlCommand cmd =
                        new SqlCommand(
                            sqlThanhToan,
                            conn,
                            transaction))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaThanhToan",
                            maThanhToan);

                        cmd.Parameters.AddWithValue(
                            "@SoHoaDon",
                            soHoaDon);

                        cmd.Parameters.AddWithValue(
                            "@NgayThanhToan",
                            ngayThanhToan);

                        cmd.Parameters.AddWithValue(
                            "@HinhThuc",
                            hinhThuc);

                        cmd.Parameters.AddWithValue(
                            "@SoTien",
                            soTien);

                        cmd.ExecuteNonQuery();
                    }

                    // Kiểm tra tổng số tiền đã thanh toán
                    string sqlCheck = @"
                        SELECT
                            CASE
                                WHEN ISNULL(SUM(tt.SoTien), 0)
                                     >= hd.TongTien
                                THEN 1
                                ELSE 0
                            END
                        FROM HoaDon hd
                        LEFT JOIN ThanhToan tt
                            ON hd.SoHoaDon = tt.SoHoaDon
                        WHERE hd.SoHoaDon = @SoHoaDon
                        GROUP BY hd.SoHoaDon, hd.TongTien";

                    bool daThanhToan = false;

                    using (SqlCommand cmd =
                        new SqlCommand(
                            sqlCheck,
                            conn,
                            transaction))
                    {
                        cmd.Parameters.AddWithValue(
                            "@SoHoaDon",
                            soHoaDon);

                        object result = cmd.ExecuteScalar();

                        if (result != null &&
                            result != DBNull.Value)
                        {
                            daThanhToan =
                                Convert.ToInt32(result) == 1;
                        }
                    }

                    if (daThanhToan)
                    {
                        string sqlUpdate = @"
                            UPDATE HoaDon
                            SET TrangThai = N'Đã thanh toán'
                            WHERE SoHoaDon = @SoHoaDon";

                        using (SqlCommand cmd =
                            new SqlCommand(
                                sqlUpdate,
                                conn,
                                transaction))
                        {
                            cmd.Parameters.AddWithValue(
                                "@SoHoaDon",
                                soHoaDon);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();

                    return true;
                }
                catch (SqlException)
                {
                    try
                    {
                        transaction?.Rollback();
                    }
                    catch
                    {
                    }

                    return false;
                }
            }
        }

        // =====================================================
        // 8. CẬP NHẬT TRẠNG THÁI PHIẾU ĐẶT KHI TRẢ PHÒNG
        // =====================================================

        public bool TraPhong(
            string soPhieuDat,
            DateTime ngayTraThucTe)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    UPDATE PhieuDatPhong
                    SET
                        NgayTraThucTe = @NgayTraThucTe,
                        TrangThai = N'Đã trả'
                    WHERE SoPhieuDat = @SoPhieuDat";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@SoPhieuDat",
                        soPhieuDat);

                    cmd.Parameters.AddWithValue(
                        "@NgayTraThucTe",
                        ngayTraThucTe);

                    conn.Open();

                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
            }
        }

        // =====================================================
        // 9. LẤY DANH SÁCH ĐỀN BÙ
        // =====================================================

        public DataTable LayDanhSachDenBu()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        db.SoPhieuDenBu,
                        db.SoPhieuDat,
                        db.SoPhong,
                        db.NgayLap,
                        db.MaNV,
                        db.TongTien
                    FROM PhieuDenBu db
                    ORDER BY db.NgayLap DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }

        // =====================================================
        // 10. THÊM PHIẾU ĐỀN BÙ
        // =====================================================

        public bool ThemPhieuDenBu(
            string soPhieuDenBu,
            string soPhieuDat,
            string soPhong,
            DateTime ngayLap,
            string maNV,
            decimal tongTien)
        {
            if (tongTien < 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO PhieuDenBu
                    (
                        SoPhieuDenBu,
                        SoPhieuDat,
                        SoPhong,
                        NgayLap,
                        MaNV,
                        TongTien
                    )
                    VALUES
                    (
                        @SoPhieuDenBu,
                        @SoPhieuDat,
                        @SoPhong,
                        @NgayLap,
                        @MaNV,
                        @TongTien
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@SoPhieuDenBu",
                        soPhieuDenBu);

                    cmd.Parameters.AddWithValue(
                        "@SoPhieuDat",
                        soPhieuDat);

                    cmd.Parameters.AddWithValue(
                        "@SoPhong",
                        soPhong);

                    cmd.Parameters.AddWithValue(
                        "@NgayLap",
                        ngayLap);

                    cmd.Parameters.AddWithValue(
                        "@MaNV",
                        maNV);

                    cmd.Parameters.AddWithValue(
                        "@TongTien",
                        tongTien);

                    conn.Open();

                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
            }
        }
    }
}