using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class PhongTienNghiService
    {
        // =====================================================
        // 1. LẤY DANH SÁCH PHÒNG
        // =====================================================
        public DataTable LayDanhSachPhong()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        p.SoPhong,
                        p.MaKhuVuc,
                        k.TenKhuVuc,
                        p.SoNguoiToiDa,
                        p.DonGiaNgay
                    FROM Phong p
                    INNER JOIN KhuVuc k
                        ON p.MaKhuVuc = k.MaKhuVuc
                    ORDER BY p.SoPhong";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        // =====================================================
        // 2. THÊM PHÒNG
        // =====================================================
        public bool ThemPhong(
            string soPhong,
            string maKhuVuc,
            int soNguoiToiDa,
            decimal donGiaNgay)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO Phong
                    (
                        SoPhong,
                        MaKhuVuc,
                        SoNguoiToiDa,
                        DonGiaNgay
                    )
                    VALUES
                    (
                        @SoPhong,
                        @MaKhuVuc,
                        @SoNguoiToiDa,
                        @DonGiaNgay
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                    cmd.Parameters.AddWithValue("@SoNguoiToiDa", soNguoiToiDa);
                    cmd.Parameters.AddWithValue("@DonGiaNgay", donGiaNgay);

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
        // 3. SỬA PHÒNG
        // =====================================================
        public bool SuaPhong(
            string soPhong,
            string maKhuVuc,
            int soNguoiToiDa,
            decimal donGiaNgay)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    UPDATE Phong
                    SET
                        MaKhuVuc = @MaKhuVuc,
                        SoNguoiToiDa = @SoNguoiToiDa,
                        DonGiaNgay = @DonGiaNgay
                    WHERE SoPhong = @SoPhong";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoPhong", soPhong);
                    cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                    cmd.Parameters.AddWithValue("@SoNguoiToiDa", soNguoiToiDa);
                    cmd.Parameters.AddWithValue("@DonGiaNgay", donGiaNgay);

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
        // 4. XÓA PHÒNG
        // =====================================================
        public bool XoaPhong(string soPhong)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    DELETE FROM Phong
                    WHERE SoPhong = @SoPhong";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
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

        // =====================================================
        // 5. LẤY DANH SÁCH TIỆN NGHI
        // =====================================================
        public DataTable LayDanhSachTienNghi()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        t.MaTienNghi,
                        t.MaLoaiTN,
                        l.TenLoaiTN,
                        t.SoThuTu,
                        t.NgayMua,
                        t.TrangThai
                    FROM TienNghi t
                    INNER JOIN LoaiTienNghi l
                        ON t.MaLoaiTN = l.MaLoaiTN
                    ORDER BY t.MaTienNghi";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        // =====================================================
        // 6. THÊM TIỆN NGHI
        // =====================================================
        public bool ThemTienNghi(
            string maTienNghi,
            string maLoaiTN,
            int soThuTu,
            DateTime ngayMua,
            string trangThai)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO TienNghi
                    (
                        MaTienNghi,
                        MaLoaiTN,
                        SoThuTu,
                        NgayMua,
                        TrangThai
                    )
                    VALUES
                    (
                        @MaTienNghi,
                        @MaLoaiTN,
                        @SoThuTu,
                        @NgayMua,
                        @TrangThai
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTienNghi", maTienNghi);
                    cmd.Parameters.AddWithValue("@MaLoaiTN", maLoaiTN);
                    cmd.Parameters.AddWithValue("@SoThuTu", soThuTu);
                    cmd.Parameters.AddWithValue("@NgayMua", ngayMua);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

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
        // 7. SỬA TIỆN NGHI
        // =====================================================
        public bool SuaTienNghi(
            string maTienNghi,
            string maLoaiTN,
            int soThuTu,
            DateTime ngayMua,
            string trangThai)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    UPDATE TienNghi
                    SET
                        MaLoaiTN = @MaLoaiTN,
                        SoThuTu = @SoThuTu,
                        NgayMua = @NgayMua,
                        TrangThai = @TrangThai
                    WHERE MaTienNghi = @MaTienNghi";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTienNghi", maTienNghi);
                    cmd.Parameters.AddWithValue("@MaLoaiTN", maLoaiTN);
                    cmd.Parameters.AddWithValue("@SoThuTu", soThuTu);
                    cmd.Parameters.AddWithValue("@NgayMua", ngayMua);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

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
        // 8. XÓA TIỆN NGHI
        // =====================================================
        public bool XoaTienNghi(string maTienNghi)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    DELETE FROM TienNghi
                    WHERE MaTienNghi = @MaTienNghi";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaTienNghi", maTienNghi);

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