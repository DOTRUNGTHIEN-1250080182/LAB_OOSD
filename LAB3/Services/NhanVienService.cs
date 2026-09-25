using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class NhanVienService
    {
        // =========================
        // 1. LẤY DANH SÁCH NHÂN VIÊN
        // =========================
        public DataTable LayDanhSach()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        MaNV,
                        HoTen,
                        VaiTro,
                        SoDienThoai
                    FROM NhanVien
                    ORDER BY MaNV";

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

        // =========================
        // 2. THÊM NHÂN VIÊN
        // =========================
        public bool Them(
            string maNV,
            string hoTen,
            string vaiTro,
            string soDienThoai)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO NhanVien
                    (
                        MaNV,
                        HoTen,
                        VaiTro,
                        SoDienThoai
                    )
                    VALUES
                    (
                        @MaNV,
                        @HoTen,
                        @VaiTro,
                        @SoDienThoai
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@VaiTro", vaiTro);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

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

        // =========================
        // 3. SỬA NHÂN VIÊN
        // =========================
        public bool Sua(
            string maNV,
            string hoTen,
            string vaiTro,
            string soDienThoai)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    UPDATE NhanVien
                    SET
                        HoTen = @HoTen,
                        VaiTro = @VaiTro,
                        SoDienThoai = @SoDienThoai
                    WHERE MaNV = @MaNV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@VaiTro", vaiTro);
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);

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

        // =========================
        // 4. XÓA NHÂN VIÊN
        // =========================
        public bool Xoa(string maNV)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    DELETE FROM NhanVien
                    WHERE MaNV = @MaNV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV);

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