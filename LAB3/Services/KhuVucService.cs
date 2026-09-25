using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class KhuVucService
    {
        // =========================
        // LẤY DANH SÁCH KHU VỰC
        // =========================
        public DataTable LayDanhSach()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT 
                        MaKhuVuc,
                        TenKhuVuc
                    FROM KhuVuc
                    ORDER BY MaKhuVuc";

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
        // THÊM KHU VỰC
        // =========================
        public bool Them(string maKhuVuc, string tenKhuVuc)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO KhuVuc
                    (
                        MaKhuVuc,
                        TenKhuVuc
                    )
                    VALUES
                    (
                        @MaKhuVuc,
                        @TenKhuVuc
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                    cmd.Parameters.AddWithValue("@TenKhuVuc", tenKhuVuc);

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
        // SỬA KHU VỰC
        // =========================
        public bool Sua(string maKhuVuc, string tenKhuVuc)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    UPDATE KhuVuc
                    SET TenKhuVuc = @TenKhuVuc
                    WHERE MaKhuVuc = @MaKhuVuc";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);
                    cmd.Parameters.AddWithValue("@TenKhuVuc", tenKhuVuc);

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
        // XÓA KHU VỰC
        // =========================
        public bool Xoa(string maKhuVuc)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    DELETE FROM KhuVuc
                    WHERE MaKhuVuc = @MaKhuVuc";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKhuVuc", maKhuVuc);

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