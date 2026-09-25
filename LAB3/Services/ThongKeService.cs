using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class ThongKeService
    {
        // =====================================================
        // 1. LẤY DANH SÁCH HÓA ĐƠN
        // =====================================================

        public DataTable LayDanhSachHoaDon()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        SoHoaDon,
                        SoPhieuDat,
                        NgayLap,
                        MaNV,
                        SoNgayTinhTien,
                        TienPhong,
                        TienDichVu,
                        TongTien,
                        TrangThai
                    FROM HoaDon
                    ORDER BY NgayLap DESC";

                using (SqlDataAdapter da =
                    new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // =====================================================
        // 2. TỔNG TIỀN PHÒNG
        // =====================================================

        public decimal LayTongTienPhong()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT ISNULL(SUM(TienPhong), 0)
                    FROM HoaDon";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    conn.Open();

                    return Convert.ToDecimal(
                        cmd.ExecuteScalar());
                }
            }
        }

        // =====================================================
        // 3. TỔNG TIỀN DỊCH VỤ
        // =====================================================

        public decimal LayTongTienDichVu()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT ISNULL(SUM(TienDichVu), 0)
                    FROM HoaDon";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    conn.Open();

                    return Convert.ToDecimal(
                        cmd.ExecuteScalar());
                }
            }
        }

        // =====================================================
        // 4. TỔNG DOANH THU
        // =====================================================

        public decimal LayTongDoanhThu()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT ISNULL(SUM(TongTien), 0)
                    FROM HoaDon";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    conn.Open();

                    return Convert.ToDecimal(
                        cmd.ExecuteScalar());
                }
            }
        }
    }
}