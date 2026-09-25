using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class DatPhongService
    {
        // =====================================================
        // 1. KHÁCH HÀNG
        // =====================================================

        public DataTable LayDanhSachKhachHang()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        MaKhach,
                        HoTen,
                        SoCMND,
                        QuocTich
                    FROM KhachHang
                    ORDER BY MaKhach";

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
        // 2. THÊM KHÁCH HÀNG
        // =====================================================

        public bool ThemKhachHang(
            string maKhach,
            string hoTen,
            string soCMND,
            string quocTich)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO KhachHang
                    (
                        MaKhach,
                        HoTen,
                        SoCMND,
                        QuocTich
                    )
                    VALUES
                    (
                        @MaKhach,
                        @HoTen,
                        @SoCMND,
                        @QuocTich
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhach", maKhach);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@SoCMND", soCMND);
                    cmd.Parameters.AddWithValue("@QuocTich", quocTich);

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
        // 3. NHÂN VIÊN
        // =====================================================

        public DataTable LayDanhSachNhanVien()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        MaNV,
                        HoTen,
                        VaiTro
                    FROM NhanVien
                    ORDER BY MaNV";

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
        // 4. DANH SÁCH PHÒNG
        // =====================================================

        public DataTable LayDanhSachPhong()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        SoPhong,
                        MaKhuVuc,
                        SoNguoiToiDa,
                        DonGiaNgay,
                        TrangThai
                    FROM Phong
                    ORDER BY SoPhong";

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
        // 5. DANH SÁCH PHIẾU ĐẶT PHÒNG
        // =====================================================

        public DataTable LayDanhSachDatPhong()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        pdp.SoPhieuDat,
                        pdp.MaKhach,
                        kh.HoTen,
                        pdp.MaNVLeTan,
                        pdp.NgayLap,
                        pdp.NgayNhan,
                        pdp.NgayTraDuKien,
                        pdp.TienCoc,
                        pdp.KenhDat,
                        pdp.TrangThai
                    FROM PhieuDatPhong pdp
                    INNER JOIN KhachHang kh
                        ON pdp.MaKhach = kh.MaKhach
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
        // 6. THÊM PHIẾU ĐẶT PHÒNG
        // =====================================================

        public bool ThemPhieuDatPhong(
            string soPhieuDat,
            string maKhach,
            string maNVLeTan,
            DateTime ngayLap,
            DateTime ngayNhan,
            DateTime ngayTraDuKien,
            decimal tienCoc,
            string kenhDat)
        {
            if (ngayTraDuKien.Date < ngayNhan.Date)
                return false;

            if (tienCoc < 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO PhieuDatPhong
                    (
                        SoPhieuDat,
                        MaKhach,
                        MaNVLeTan,
                        NgayLap,
                        NgayNhan,
                        NgayTraDuKien,
                        TienCoc,
                        KenhDat,
                        TrangThai
                    )
                    VALUES
                    (
                        @SoPhieuDat,
                        @MaKhach,
                        @MaNVLeTan,
                        @NgayLap,
                        @NgayNhan,
                        @NgayTraDuKien,
                        @TienCoc,
                        @KenhDat,
                        N'Đã đặt'
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhieuDat", soPhieuDat);
                    cmd.Parameters.AddWithValue("@MaKhach", maKhach);
                    cmd.Parameters.AddWithValue("@MaNVLeTan", maNVLeTan);
                    cmd.Parameters.AddWithValue("@NgayLap", ngayLap);
                    cmd.Parameters.AddWithValue("@NgayNhan", ngayNhan.Date);
                    cmd.Parameters.AddWithValue("@NgayTraDuKien", ngayTraDuKien.Date);
                    cmd.Parameters.AddWithValue("@TienCoc", tienCoc);
                    cmd.Parameters.AddWithValue("@KenhDat", kenhDat);

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
        // 7. THÊM CHI TIẾT ĐẶT PHÒNG
        // =====================================================

        public bool ThemChiTietDatPhong(
            string soPhieuDat,
            string soPhong,
            int soNguoi)
        {
            if (soNguoi <= 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO ChiTietDatPhong
                    (
                        SoPhieuDat,
                        SoPhong,
                        SoNguoi
                    )
                    VALUES
                    (
                        @SoPhieuDat,
                        @SoPhong,
                        @SoNguoi
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhieuDat", soPhieuDat);
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd.Parameters.AddWithValue("@SoNguoi", soNguoi);

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
        // 8. DANH SÁCH CHI TIẾT ĐẶT PHÒNG
        // =====================================================

        public DataTable LayChiTietDatPhong()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        SoPhieuDat,
                        SoPhong,
                        SoNguoi
                    FROM ChiTietDatPhong
                    ORDER BY SoPhieuDat, SoPhong";

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
        // 9. XÓA CHI TIẾT ĐẶT PHÒNG
        // =====================================================

        public bool XoaChiTietDatPhong(
            string soPhieuDat,
            string soPhong)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    DELETE FROM ChiTietDatPhong
                    WHERE SoPhieuDat = @SoPhieuDat
                    AND SoPhong = @SoPhong";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhieuDat", soPhieuDat);
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);

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