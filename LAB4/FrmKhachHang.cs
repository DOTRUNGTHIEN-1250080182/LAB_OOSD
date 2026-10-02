using EShopping.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace EShopping
{
    public class FrmKhachHang : Form
    {
        // =========================
        // KHAI BÁO CONTROL
        // =========================

        private TextBox txtMaKH = new TextBox();
        private TextBox txtHoTen = new TextBox();
        private DateTimePicker dtpNgaySinh = new DateTimePicker();
        private TextBox txtCMND = new TextBox();
        private TextBox txtDiaChi = new TextBox();
        private TextBox txtDienThoai = new TextBox();
        private TextBox txtTenDangNhap = new TextBox();
        private TextBox txtMatKhau = new TextBox();
        private TextBox txtEmail = new TextBox();

        private Button btnThem = new Button();
        private Button btnSua = new Button();
        private Button btnXoa = new Button();
        private Button btnLamMoi = new Button();

        private DataGridView dgvKhachHang = new DataGridView();

        // =========================
        // CONSTRUCTOR
        // =========================

        public FrmKhachHang()
        {
            InitializeForm();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            dgvKhachHang.CellClick += dgvKhachHang_CellClick;

            LoadData();
        }

        // =========================
        // THIẾT KẾ FORM
        // =========================

        private void InitializeForm()
        {
            this.Text = "Quản lý khách hàng";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(900, 600);
            this.BackColor = Color.White;

            // =========================
            // TIÊU ĐỀ
            // =========================

            Label lblTitle = new Label();

            lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
            lblTitle.Font = new Font(
                "Segoe UI",
                20,
                FontStyle.Bold);

            lblTitle.AutoSize = false;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 60;

            this.Controls.Add(lblTitle);

            // =========================
            // PANEL THÔNG TIN
            // =========================

            Panel pnlThongTin = new Panel();

            pnlThongTin.Location = new Point(20, 70);
            pnlThongTin.Size = new Size(1040, 280);
            pnlThongTin.BorderStyle = BorderStyle.FixedSingle;

            this.Controls.Add(pnlThongTin);

            // =========================
            // MÃ KHÁCH HÀNG
            // =========================

            AddLabel(
                pnlThongTin,
                "Mã khách hàng:",
                20,
                20);

            txtMaKH.Location = new Point(150, 17);
            txtMaKH.Size = new Size(220, 30);

            pnlThongTin.Controls.Add(txtMaKH);

            // =========================
            // HỌ TÊN
            // =========================

            AddLabel(
                pnlThongTin,
                "Họ tên:",
                530,
                20);

            txtHoTen.Location = new Point(650, 17);
            txtHoTen.Size = new Size(330, 30);

            pnlThongTin.Controls.Add(txtHoTen);

            // =========================
            // NGÀY SINH
            // =========================

            AddLabel(
                pnlThongTin,
                "Ngày sinh:",
                20,
                65);

            dtpNgaySinh.Location = new Point(150, 62);
            dtpNgaySinh.Size = new Size(220, 30);
            dtpNgaySinh.Format = DateTimePickerFormat.Short;

            pnlThongTin.Controls.Add(dtpNgaySinh);

            // =========================
            // CMND / PASSPORT
            // =========================

            AddLabel(
                pnlThongTin,
                "CMND/Passport:",
                530,
                65);

            txtCMND.Location = new Point(650, 62);
            txtCMND.Size = new Size(330, 30);

            pnlThongTin.Controls.Add(txtCMND);

            // =========================
            // ĐỊA CHỈ
            // =========================

            AddLabel(
                pnlThongTin,
                "Địa chỉ:",
                20,
                110);

            txtDiaChi.Location = new Point(150, 107);
            txtDiaChi.Size = new Size(220, 30);

            pnlThongTin.Controls.Add(txtDiaChi);

            // =========================
            // ĐIỆN THOẠI
            // =========================

            AddLabel(
                pnlThongTin,
                "Điện thoại:",
                530,
                110);

            txtDienThoai.Location = new Point(650, 107);
            txtDienThoai.Size = new Size(330, 30);

            pnlThongTin.Controls.Add(txtDienThoai);

            // =========================
            // TÊN ĐĂNG NHẬP
            // =========================

            AddLabel(
                pnlThongTin,
                "Tên đăng nhập:",
                20,
                155);

            txtTenDangNhap.Location = new Point(150, 152);
            txtTenDangNhap.Size = new Size(220, 30);

            pnlThongTin.Controls.Add(txtTenDangNhap);

            // =========================
            // MẬT KHẨU
            // =========================

            AddLabel(
                pnlThongTin,
                "Mật khẩu:",
                530,
                155);

            txtMatKhau.Location = new Point(650, 152);
            txtMatKhau.Size = new Size(330, 30);
            txtMatKhau.UseSystemPasswordChar = true;

            pnlThongTin.Controls.Add(txtMatKhau);

            // =========================
            // EMAIL
            // =========================

            AddLabel(
                pnlThongTin,
                "Email:",
                20,
                200);

            txtEmail.Location = new Point(150, 197);
            txtEmail.Size = new Size(220, 30);

            pnlThongTin.Controls.Add(txtEmail);

            // =========================
            // BUTTON
            // =========================

            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";
            btnLamMoi.Text = "Làm mới";

            Button[] buttons =
            {
                btnThem,
                btnSua,
                btnXoa,
                btnLamMoi
            };

            int x = 530;

            foreach (Button button in buttons)
            {
                button.Size = new Size(105, 38);
                button.Location = new Point(x, 200);

                pnlThongTin.Controls.Add(button);

                x += 115;
            }

            // =========================
            // DATAGRIDVIEW
            // =========================

            dgvKhachHang.Location =
                new Point(20, 370);

            dgvKhachHang.Size =
                new Size(1040, 260);

            dgvKhachHang.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            dgvKhachHang.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvKhachHang.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvKhachHang.MultiSelect = false;

            dgvKhachHang.ReadOnly = true;

            dgvKhachHang.AllowUserToAddRows = false;

            this.Controls.Add(dgvKhachHang);
        }

        // =========================
        // TẠO LABEL
        // =========================

        private void AddLabel(
            Panel panel,
            string text,
            int x,
            int y)
        {
            Label label = new Label();

            label.Text = text;
            label.Location = new Point(x, y);
            label.Size = new Size(125, 30);

            label.Font = new Font(
                "Segoe UI",
                9,
                FontStyle.Regular);

            panel.Controls.Add(label);
        }

        // =========================
        // LOAD DỮ LIỆU
        // =========================

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn =
                    DbConnection.GetConnection())
                {
                    string sql =
                        "SELECT * FROM KhachHang";

                    using (SqlDataAdapter adapter =
                        new SqlDataAdapter(sql, conn))
                    {
                        DataTable table =
                            new DataTable();

                        adapter.Fill(table);

                        dgvKhachHang.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu:\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // THÊM KHÁCH HÀNG
        // =========================

        private void btnThem_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin bắt buộc!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                using (SqlConnection conn =
                    DbConnection.GetConnection())
                {
                    string sql = @"
                        INSERT INTO KhachHang
                        (
                            MaKH,
                            HoTen,
                            NgaySinh,
                            CMND_Passport,
                            DiaChi,
                            DienThoai,
                            TenDangNhap,
                            MatKhau,
                            Email
                        )
                        VALUES
                        (
                            @MaKH,
                            @HoTen,
                            @NgaySinh,
                            @CMND,
                            @DiaChi,
                            @DienThoai,
                            @TenDangNhap,
                            @MatKhau,
                            @Email
                        )";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaKH",
                            txtMaKH.Text);

                        cmd.Parameters.AddWithValue(
                            "@HoTen",
                            txtHoTen.Text);

                        cmd.Parameters.AddWithValue(
                            "@NgaySinh",
                            dtpNgaySinh.Value.Date);

                        cmd.Parameters.AddWithValue(
                            "@CMND",
                            txtCMND.Text);

                        cmd.Parameters.AddWithValue(
                            "@DiaChi",
                            txtDiaChi.Text);

                        cmd.Parameters.AddWithValue(
                            "@DienThoai",
                            txtDienThoai.Text);

                        cmd.Parameters.AddWithValue(
                            "@TenDangNhap",
                            txtTenDangNhap.Text);

                        cmd.Parameters.AddWithValue(
                            "@MatKhau",
                            txtMatKhau.Text);

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            txtEmail.Text);

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Thêm khách hàng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi thêm khách hàng:\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // SỬA KHÁCH HÀNG
        // =========================

        private void btnSua_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần sửa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                using (SqlConnection conn =
                    DbConnection.GetConnection())
                {
                    string sql = @"
                        UPDATE KhachHang
                        SET
                            HoTen = @HoTen,
                            NgaySinh = @NgaySinh,
                            CMND_Passport = @CMND,
                            DiaChi = @DiaChi,
                            DienThoai = @DienThoai,
                            TenDangNhap = @TenDangNhap,
                            MatKhau = @MatKhau,
                            Email = @Email
                        WHERE MaKH = @MaKH";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaKH",
                            txtMaKH.Text);

                        cmd.Parameters.AddWithValue(
                            "@HoTen",
                            txtHoTen.Text);

                        cmd.Parameters.AddWithValue(
                            "@NgaySinh",
                            dtpNgaySinh.Value.Date);

                        cmd.Parameters.AddWithValue(
                            "@CMND",
                            txtCMND.Text);

                        cmd.Parameters.AddWithValue(
                            "@DiaChi",
                            txtDiaChi.Text);

                        cmd.Parameters.AddWithValue(
                            "@DienThoai",
                            txtDienThoai.Text);

                        cmd.Parameters.AddWithValue(
                            "@TenDangNhap",
                            txtTenDangNhap.Text);

                        cmd.Parameters.AddWithValue(
                            "@MatKhau",
                            txtMatKhau.Text);

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            txtEmail.Text);

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Cập nhật khách hàng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi cập nhật:\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // XÓA KHÁCH HÀNG
        // =========================

        private void btnXoa_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng cần xóa!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc muốn xóa khách hàng này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection conn =
                    DbConnection.GetConnection())
                {
                    string sql =
                        "DELETE FROM KhachHang " +
                        "WHERE MaKH = @MaKH";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaKH",
                            txtMaKH.Text);

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Xóa khách hàng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi xóa:\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================
        // LÀM MỚI
        // =========================

        private void btnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            ClearForm();
            LoadData();
        }

        // =========================
        // CLICK VÀO DÒNG
        // =========================

        private void dgvKhachHang_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvKhachHang.Rows[e.RowIndex];

            txtMaKH.Text =
                row.Cells["MaKH"].Value?.ToString() ?? "";

            txtHoTen.Text =
                row.Cells["HoTen"].Value?.ToString() ?? "";

            txtCMND.Text =
                row.Cells["CMND_Passport"].Value?.ToString() ?? "";

            txtDiaChi.Text =
                row.Cells["DiaChi"].Value?.ToString() ?? "";

            txtDienThoai.Text =
                row.Cells["DienThoai"].Value?.ToString() ?? "";

            txtTenDangNhap.Text =
                row.Cells["TenDangNhap"].Value?.ToString() ?? "";

            txtMatKhau.Text =
                row.Cells["MatKhau"].Value?.ToString() ?? "";

            txtEmail.Text =
                row.Cells["Email"].Value?.ToString() ?? "";

            if (row.Cells["NgaySinh"].Value != null &&
                row.Cells["NgaySinh"].Value != DBNull.Value)
            {
                dtpNgaySinh.Value =
                    Convert.ToDateTime(
                        row.Cells["NgaySinh"].Value);
            }
        }

        // =========================
        // XÓA TRẮNG FORM
        // =========================

        private void ClearForm()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtCMND.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtEmail.Clear();

            dtpNgaySinh.Value =
                DateTime.Now;
        }
    }
}