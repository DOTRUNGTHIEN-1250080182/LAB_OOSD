using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;

namespace QuanLyKhachSan.Services
{
    public class DichVuService
    {
        // =====================================================
        // 1. LẤY DANH SÁCH DỊCH VỤ
        // =====================================================

        public DataTable LayDanhSachDichVu()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        MaDV,
                        TenDV,
                        DonGia
                    FROM DichVu
                    ORDER BY MaDV";

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
        // 2. THÊM DỊCH VỤ
        // =====================================================

        public bool ThemDichVu(
            string maDV,
            string tenDV,
            decimal donGia)
        {
            if (donGia < 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO DichVu
                    (
                        MaDV,
                        TenDV,
                        DonGia
                    )
                    VALUES
                    (
                        @MaDV,
                        @TenDV,
                        @DonGia
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    cmd.Parameters.AddWithValue("@TenDV", tenDV);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);

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
        // 3. SỬA DỊCH VỤ
        // =====================================================

        public bool SuaDichVu(
            string maDV,
            string tenDV,
            decimal donGia)
        {
            if (donGia < 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    UPDATE DichVu
                    SET
                        TenDV = @TenDV,
                        DonGia = @DonGia
                    WHERE MaDV = @MaDV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDV", maDV);
                    cmd.Parameters.AddWithValue("@TenDV", tenDV);
                    cmd.Parameters.AddWithValue("@DonGia", donGia);

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
        // 4. XÓA DỊCH VỤ
        // =====================================================

        public bool XoaDichVu(string maDV)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    DELETE FROM DichVu
                    WHERE MaDV = @MaDV";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDV", maDV);

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
        // 5. LẤY DANH SÁCH PHIẾU SỬ DỤNG DỊCH VỤ
        // =====================================================

        public DataTable LayDanhSachPhieuSuDung()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        ps.SoPhieuSDDV,
                        ps.SoPhieuDat,
                        ps.NgaySuDung
                    FROM PhieuSuDungDV ps
                    ORDER BY ps.SoPhieuSDDV";

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
        // 6. THÊM PHIẾU SỬ DỤNG DỊCH VỤ
        // =====================================================

        public bool ThemPhieuSuDung(
            string soPhieuSDDV,
            string soPhieuDat,
            DateTime ngaySuDung)
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO PhieuSuDungDV
                    (
                        SoPhieuSDDV,
                        SoPhieuDat,
                        NgaySuDung
                    )
                    VALUES
                    (
                        @SoPhieuSDDV,
                        @SoPhieuDat,
                        @NgaySuDung
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@SoPhieuSDDV",
                        soPhieuSDDV);

                    cmd.Parameters.AddWithValue(
                        "@SoPhieuDat",
                        soPhieuDat);

                    cmd.Parameters.AddWithValue(
                        "@NgaySuDung",
                        ngaySuDung.Date);

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
        // 7. LẤY CHI TIẾT SỬ DỤNG DỊCH VỤ
        // =====================================================

        public DataTable LayChiTietSuDung()
        {
            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    SELECT
                        ct.SoPhieuSDDV,
                        ct.MaDV,
                        dv.TenDV,
                        ct.SoLuong,
                        ct.DonGia
                    FROM ChiTietPhieuSuDungDV ct
                    INNER JOIN DichVu dv
                        ON ct.MaDV = dv.MaDV
                    ORDER BY ct.SoPhieuSDDV, ct.MaDV";

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
        // 8. THÊM CHI TIẾT SỬ DỤNG DỊCH VỤ
        // =====================================================

        public bool ThemChiTietSuDung(
            string soPhieuSDDV,
            string maDV,
            int soLuong,
            decimal donGia)
        {
            if (soLuong <= 0)
                return false;

            if (donGia < 0)
                return false;

            using (SqlConnection conn = Db.GetConnection())
            {
                string sql = @"
                    INSERT INTO ChiTietPhieuSuDungDV
                    (
                        SoPhieuSDDV,
                        MaDV,
                        SoLuong,
                        DonGia
                    )
                    VALUES
                    (
                        @SoPhieuSDDV,
                        @MaDV,
                        @SoLuong,
                        @DonGia
                    )";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@SoPhieuSDDV",
                        soPhieuSDDV);

                    cmd.Parameters.AddWithValue(
                        "@MaDV",
                        maDV);

                    cmd.Parameters.AddWithValue(
                        "@SoLuong",
                        soLuong);

                    cmd.Parameters.AddWithValue(
                        "@DonGia",
                        donGia);

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