using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService service =
            new DanhMucService();

        // =====================================================
        // NHÂN VIÊN
        // =====================================================

        private DataGridView dgvNhanVien = new DataGridView();

        private TextBox txtMaNhanVien = new TextBox();
        private TextBox txtHoNhanVien = new TextBox();
        private TextBox txtTenNhanVien = new TextBox();
        private TextBox txtPhaiNhanVien = new TextBox();
        private DateTimePicker dtpNgaySinh = new DateTimePicker();
        private TextBox txtChucVu = new TextBox();
        private TextBox txtSdtNhanVien = new TextBox();

        private Button btnThemNhanVien = new Button();
        private Button btnSuaNhanVien = new Button();
        private Button btnXoaNhanVien = new Button();
        private Button btnLamMoiNhanVien = new Button();


        // =====================================================
        // THỂ LOẠI
        // =====================================================

        private DataGridView dgvTheLoai = new DataGridView();

        private TextBox txtMaTheLoai = new TextBox();
        private TextBox txtTenTheLoai = new TextBox();

        private Button btnThemTheLoai = new Button();
        private Button btnSuaTheLoai = new Button();
        private Button btnXoaTheLoai = new Button();
        private Button btnLamMoiTheLoai = new Button();


        // =====================================================
        // NHÀ XUẤT BẢN
        // =====================================================

        private DataGridView dgvNhaXuatBan = new DataGridView();

        private TextBox txtMaNhaXuatBan = new TextBox();
        private TextBox txtTenNhaXuatBan = new TextBox();
        private TextBox txtDiaChi = new TextBox();
        private TextBox txtSoDienThoai = new TextBox();

        private Button btnThemNhaXuatBan = new Button();
        private Button btnSuaNhaXuatBan = new Button();
        private Button btnXoaNhaXuatBan = new Button();
        private Button btnLamMoiNhaXuatBan = new Button();


        public FrmDanhMuc()
        {
            InitializeComponent();

            TaoGiaoDien();

            LoadNhanVien();
            LoadTheLoai();
            LoadNhaXuatBan();
        }


        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================

        private void TaoGiaoDien()
        {
            Text = "Quản lý danh mục";

            Width = 1100;
            Height = 950;

            StartPosition =
                FormStartPosition.CenterScreen;

            // =================================================
            // TIÊU ĐỀ NHÂN VIÊN
            // =================================================

            Label lblTieuDeNhanVien = new Label();

            lblTieuDeNhanVien.Text =
                "QUẢN LÝ NHÂN VIÊN";

            lblTieuDeNhanVien.Font =
                new Font("Arial", 12, FontStyle.Bold);

            lblTieuDeNhanVien.Location =
                new Point(30, 20);

            lblTieuDeNhanVien.AutoSize = true;


            // =================================================
            // NHÂN VIÊN - DÒNG 1
            // =================================================

            Label lblMaNV = TaoLabel(
                "Mã NV:",
                30,
                55);

            txtMaNhanVien.Location =
                new Point(90, 50);

            txtMaNhanVien.Width = 130;


            Label lblHoNV = TaoLabel(
                "Họ:",
                240,
                55);

            txtHoNhanVien.Location =
                new Point(275, 50);

            txtHoNhanVien.Width = 150;


            Label lblTenNV = TaoLabel(
                "Tên:",
                445,
                55);

            txtTenNhanVien.Location =
                new Point(485, 50);

            txtTenNhanVien.Width = 150;


            Label lblPhaiNV = TaoLabel(
                "Phái:",
                650,
                55);

            txtPhaiNhanVien.Location =
                new Point(700, 50);

            txtPhaiNhanVien.Width = 100;


            // =================================================
            // NHÂN VIÊN - DÒNG 2
            // =================================================

            Label lblNgaySinh = TaoLabel(
                "Ngày sinh:",
                30,
                95);

            dtpNgaySinh.Location =
                new Point(110, 90);

            dtpNgaySinh.Width = 180;

            dtpNgaySinh.Format =
                DateTimePickerFormat.Short;


            Label lblChucVu = TaoLabel(
                "Chức vụ:",
                320,
                95);

            txtChucVu.Location =
                new Point(390, 90);

            txtChucVu.Width = 180;


            Label lblSdtNV = TaoLabel(
                "SĐT:",
                600,
                95);

            txtSdtNhanVien.Location =
                new Point(650, 90);

            txtSdtNhanVien.Width = 180;


            // =================================================
            // NÚT NHÂN VIÊN
            // =================================================

            btnThemNhanVien.Text = "THÊM";
            btnThemNhanVien.Location =
                new Point(30, 130);
            btnThemNhanVien.Width = 80;
            btnThemNhanVien.Click +=
                BtnThemNhanVien_Click;


            btnSuaNhanVien.Text = "SỬA";
            btnSuaNhanVien.Location =
                new Point(120, 130);
            btnSuaNhanVien.Width = 80;
            btnSuaNhanVien.Click +=
                BtnSuaNhanVien_Click;


            btnXoaNhanVien.Text = "XÓA";
            btnXoaNhanVien.Location =
                new Point(210, 130);
            btnXoaNhanVien.Width = 80;
            btnXoaNhanVien.Click +=
                BtnXoaNhanVien_Click;


            btnLamMoiNhanVien.Text = "LÀM MỚI";
            btnLamMoiNhanVien.Location =
                new Point(300, 130);
            btnLamMoiNhanVien.Width = 100;
            btnLamMoiNhanVien.Click +=
                BtnLamMoiNhanVien_Click;


            // =================================================
            // BẢNG NHÂN VIÊN
            // =================================================

            CauHinhGrid(
                dgvNhanVien,
                30,
                170,
                1000,
                180);

            dgvNhanVien.SelectionChanged +=
                DgvNhanVien_SelectionChanged;


            // =================================================
            // TIÊU ĐỀ THỂ LOẠI
            // =================================================

            Label lblTieuDeTheLoai = new Label();

            lblTieuDeTheLoai.Text =
                "QUẢN LÝ THỂ LOẠI";

            lblTieuDeTheLoai.Font =
                new Font("Arial", 12, FontStyle.Bold);

            lblTieuDeTheLoai.Location =
                new Point(30, 370);

            lblTieuDeTheLoai.AutoSize = true;


            // =================================================
            // THỂ LOẠI
            // =================================================

            Label lblMaTheLoai = TaoLabel(
                "Mã thể loại:",
                30,
                405);

            txtMaTheLoai.Location =
                new Point(130, 400);

            txtMaTheLoai.Width = 180;


            Label lblTenTheLoai = TaoLabel(
                "Tên thể loại:",
                330,
                405);

            txtTenTheLoai.Location =
                new Point(430, 400);

            txtTenTheLoai.Width = 250;


            btnThemTheLoai.Text = "THÊM";
            btnThemTheLoai.Location =
                new Point(700, 398);
            btnThemTheLoai.Width = 70;
            btnThemTheLoai.Click +=
                BtnThemTheLoai_Click;


            btnSuaTheLoai.Text = "SỬA";
            btnSuaTheLoai.Location =
                new Point(775, 398);
            btnSuaTheLoai.Width = 70;
            btnSuaTheLoai.Click +=
                BtnSuaTheLoai_Click;


            btnXoaTheLoai.Text = "XÓA";
            btnXoaTheLoai.Location =
                new Point(850, 398);
            btnXoaTheLoai.Width = 70;
            btnXoaTheLoai.Click +=
                BtnXoaTheLoai_Click;


            btnLamMoiTheLoai.Text = "LÀM MỚI";
            btnLamMoiTheLoai.Location =
                new Point(925, 398);
            btnLamMoiTheLoai.Width = 105;
            btnLamMoiTheLoai.Click +=
                BtnLamMoiTheLoai_Click;


            CauHinhGrid(
                dgvTheLoai,
                30,
                440,
                1000,
                150);

            dgvTheLoai.SelectionChanged +=
                DgvTheLoai_SelectionChanged;


            // =================================================
            // TIÊU ĐỀ NHÀ XUẤT BẢN
            // =================================================

            Label lblTieuDeNhaXuatBan = new Label();

            lblTieuDeNhaXuatBan.Text =
                "QUẢN LÝ NHÀ XUẤT BẢN";

            lblTieuDeNhaXuatBan.Font =
                new Font("Arial", 12, FontStyle.Bold);

            lblTieuDeNhaXuatBan.Location =
                new Point(30, 610);

            lblTieuDeNhaXuatBan.AutoSize = true;


            // =================================================
            // NHÀ XUẤT BẢN
            // =================================================

            Label lblMaNXB = TaoLabel(
                "Mã NXB:",
                30,
                645);

            txtMaNhaXuatBan.Location =
                new Point(100, 640);

            txtMaNhaXuatBan.Width = 150;


            Label lblTenNXB = TaoLabel(
                "Tên NXB:",
                270,
                645);

            txtTenNhaXuatBan.Location =
                new Point(340, 640);

            txtTenNhaXuatBan.Width = 200;


            Label lblDiaChi = TaoLabel(
                "Địa chỉ:",
                560,
                645);

            txtDiaChi.Location =
                new Point(625, 640);

            txtDiaChi.Width = 220;


            Label lblSDT = TaoLabel(
                "SĐT:",
                30,
                685);

            txtSoDienThoai.Location =
                new Point(100, 680);

            txtSoDienThoai.Width = 150;


            btnThemNhaXuatBan.Text = "THÊM";
            btnThemNhaXuatBan.Location =
                new Point(270, 678);
            btnThemNhaXuatBan.Width = 80;
            btnThemNhaXuatBan.Click +=
                BtnThemNhaXuatBan_Click;


            btnSuaNhaXuatBan.Text = "SỬA";
            btnSuaNhaXuatBan.Location =
                new Point(360, 678);
            btnSuaNhaXuatBan.Width = 80;
            btnSuaNhaXuatBan.Click +=
                BtnSuaNhaXuatBan_Click;


            btnXoaNhaXuatBan.Text = "XÓA";
            btnXoaNhaXuatBan.Location =
                new Point(450, 678);
            btnXoaNhaXuatBan.Width = 80;
            btnXoaNhaXuatBan.Click +=
                BtnXoaNhaXuatBan_Click;


            btnLamMoiNhaXuatBan.Text = "LÀM MỚI";
            btnLamMoiNhaXuatBan.Location =
                new Point(540, 678);
            btnLamMoiNhaXuatBan.Width = 100;
            btnLamMoiNhaXuatBan.Click +=
                BtnLamMoiNhaXuatBan_Click;


            CauHinhGrid(
                dgvNhaXuatBan,
                30,
                720,
                1000,
                150);

            dgvNhaXuatBan.SelectionChanged +=
                DgvNhaXuatBan_SelectionChanged;


            // =================================================
            // THÊM CONTROL
            // =================================================

            Controls.Add(lblTieuDeNhanVien);

            Controls.Add(lblMaNV);
            Controls.Add(txtMaNhanVien);

            Controls.Add(lblHoNV);
            Controls.Add(txtHoNhanVien);

            Controls.Add(lblTenNV);
            Controls.Add(txtTenNhanVien);

            Controls.Add(lblPhaiNV);
            Controls.Add(txtPhaiNhanVien);

            Controls.Add(lblNgaySinh);
            Controls.Add(dtpNgaySinh);

            Controls.Add(lblChucVu);
            Controls.Add(txtChucVu);

            Controls.Add(lblSdtNV);
            Controls.Add(txtSdtNhanVien);

            Controls.Add(btnThemNhanVien);
            Controls.Add(btnSuaNhanVien);
            Controls.Add(btnXoaNhanVien);
            Controls.Add(btnLamMoiNhanVien);

            Controls.Add(dgvNhanVien);


            Controls.Add(lblTieuDeTheLoai);

            Controls.Add(lblMaTheLoai);
            Controls.Add(txtMaTheLoai);

            Controls.Add(lblTenTheLoai);
            Controls.Add(txtTenTheLoai);

            Controls.Add(btnThemTheLoai);
            Controls.Add(btnSuaTheLoai);
            Controls.Add(btnXoaTheLoai);
            Controls.Add(btnLamMoiTheLoai);

            Controls.Add(dgvTheLoai);


            Controls.Add(lblTieuDeNhaXuatBan);

            Controls.Add(lblMaNXB);
            Controls.Add(txtMaNhaXuatBan);

            Controls.Add(lblTenNXB);
            Controls.Add(txtTenNhaXuatBan);

            Controls.Add(lblDiaChi);
            Controls.Add(txtDiaChi);

            Controls.Add(lblSDT);
            Controls.Add(txtSoDienThoai);

            Controls.Add(btnThemNhaXuatBan);
            Controls.Add(btnSuaNhaXuatBan);
            Controls.Add(btnXoaNhaXuatBan);
            Controls.Add(btnLamMoiNhaXuatBan);

            Controls.Add(dgvNhaXuatBan);
        }


        // =====================================================
        // HÀM TẠO LABEL
        // =====================================================

        private Label TaoLabel(
            string text,
            int x,
            int y)
        {
            Label label = new Label();

            label.Text = text;
            label.Location =
                new Point(x, y);

            label.AutoSize = true;

            return label;
        }


        // =====================================================
        // CẤU HÌNH DATAGRIDVIEW
        // =====================================================

        private void CauHinhGrid(
            DataGridView grid,
            int x,
            int y,
            int width,
            int height)
        {
            grid.Location =
                new Point(x, y);

            grid.Size =
                new Size(width, height);

            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.MultiSelect = false;

            grid.ReadOnly = true;

            grid.AllowUserToAddRows = false;

            grid.AllowUserToDeleteRows = false;
        }


        // =====================================================
        // NHÂN VIÊN - LOAD
        // =====================================================

        private void LoadNhanVien()
        {
            dgvNhanVien.DataSource =
                service.LayNhanVien();
        }


        // =====================================================
        // NHÂN VIÊN - CHỌN DÒNG
        // =====================================================

        private void DgvNhanVien_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null)
                return;

            DataGridViewRow row =
                dgvNhanVien.CurrentRow;

            if (row.Cells["MaNhanVien"].Value == null)
                return;

            txtMaNhanVien.Text =
                row.Cells["MaNhanVien"].Value?.ToString();

            txtHoNhanVien.Text =
                row.Cells["Ho"].Value?.ToString();

            txtTenNhanVien.Text =
                row.Cells["Ten"].Value?.ToString();

            txtPhaiNhanVien.Text =
                row.Cells["Phai"].Value?.ToString();

            txtChucVu.Text =
                row.Cells["ChucVu"].Value?.ToString();

            txtSdtNhanVien.Text =
                row.Cells["SoDienThoai"].Value?.ToString();

            if (row.Cells["NgaySinh"].Value != null &&
                DateTime.TryParse(
                    row.Cells["NgaySinh"].Value.ToString(),
                    out DateTime ngay))
            {
                dtpNgaySinh.Value = ngay;
            }
        }


        // =====================================================
        // NHÂN VIÊN - THÊM
        // =====================================================

        private void BtnThemNhanVien_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaNhanVien.Text.Trim() == "" ||
                txtHoNhanVien.Text.Trim() == "" ||
                txtTenNhanVien.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập mã, họ và tên nhân viên.");

                return;
            }

            bool ketQua =
                service.ThemNhanVien(
                    txtMaNhanVien.Text.Trim(),
                    txtHoNhanVien.Text.Trim(),
                    txtTenNhanVien.Text.Trim(),
                    txtPhaiNhanVien.Text.Trim(),
                    dtpNgaySinh.Value.Date,
                    txtChucVu.Text.Trim(),
                    txtSdtNhanVien.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Thêm nhân viên thành công.");

                LoadNhanVien();
                XoaTrangNhanVien();
            }
        }


        // =====================================================
        // NHÂN VIÊN - SỬA
        // =====================================================

        private void BtnSuaNhanVien_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaNhanVien.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên cần sửa.");

                return;
            }

            bool ketQua =
                service.SuaNhanVien(
                    txtMaNhanVien.Text.Trim(),
                    txtHoNhanVien.Text.Trim(),
                    txtTenNhanVien.Text.Trim(),
                    txtPhaiNhanVien.Text.Trim(),
                    dtpNgaySinh.Value.Date,
                    txtChucVu.Text.Trim(),
                    txtSdtNhanVien.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Sửa nhân viên thành công.");

                LoadNhanVien();
            }
        }


        // =====================================================
        // NHÂN VIÊN - XÓA
        // =====================================================

        private void BtnXoaNhanVien_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaNhanVien.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên cần xóa.");

                return;
            }

            DialogResult hoi =
                MessageBox.Show(
                    "Bạn có chắc muốn xóa nhân viên này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo);

            if (hoi != DialogResult.Yes)
                return;

            bool ketQua =
                service.XoaNhanVien(
                    txtMaNhanVien.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Xóa nhân viên thành công.");

                LoadNhanVien();
                XoaTrangNhanVien();
            }
        }


        // =====================================================
        // NHÂN VIÊN - LÀM MỚI
        // =====================================================

        private void BtnLamMoiNhanVien_Click(
            object? sender,
            EventArgs e)
        {
            LoadNhanVien();
            XoaTrangNhanVien();
        }


        private void XoaTrangNhanVien()
        {
            txtMaNhanVien.Clear();
            txtHoNhanVien.Clear();
            txtTenNhanVien.Clear();
            txtPhaiNhanVien.Clear();
            txtChucVu.Clear();
            txtSdtNhanVien.Clear();

            dtpNgaySinh.Value =
                DateTime.Today;
        }


        // =====================================================
        // THỂ LOẠI
        // =====================================================

        private void LoadTheLoai()
        {
            dgvTheLoai.DataSource =
                service.LayTheLoai();
        }


        private void DgvTheLoai_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvTheLoai.CurrentRow == null)
                return;

            if (dgvTheLoai.CurrentRow.Cells["MaTheLoai"].Value == null)
                return;

            txtMaTheLoai.Text =
                dgvTheLoai.CurrentRow
                    .Cells["MaTheLoai"]
                    .Value?.ToString();

            txtTenTheLoai.Text =
                dgvTheLoai.CurrentRow
                    .Cells["TenTheLoai"]
                    .Value?.ToString();
        }


        private void BtnThemTheLoai_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaTheLoai.Text.Trim() == "" ||
                txtTenTheLoai.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập mã và tên thể loại.");

                return;
            }

            bool ketQua =
                service.ThemTheLoai(
                    txtMaTheLoai.Text.Trim(),
                    txtTenTheLoai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Thêm thể loại thành công.");

                LoadTheLoai();
                XoaTrangTheLoai();
            }
        }


        private void BtnSuaTheLoai_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaTheLoai.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn thể loại cần sửa.");

                return;
            }

            bool ketQua =
                service.SuaTheLoai(
                    txtMaTheLoai.Text.Trim(),
                    txtTenTheLoai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Sửa thể loại thành công.");

                LoadTheLoai();
            }
        }


        private void BtnXoaTheLoai_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaTheLoai.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn thể loại cần xóa.");

                return;
            }

            DialogResult hoi =
                MessageBox.Show(
                    "Bạn có chắc muốn xóa thể loại này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo);

            if (hoi == DialogResult.Yes)
            {
                bool ketQua =
                    service.XoaTheLoai(
                        txtMaTheLoai.Text.Trim());

                if (ketQua)
                {
                    MessageBox.Show(
                        "Xóa thể loại thành công.");

                    LoadTheLoai();
                    XoaTrangTheLoai();
                }
            }
        }


        private void BtnLamMoiTheLoai_Click(
            object? sender,
            EventArgs e)
        {
            LoadTheLoai();
            XoaTrangTheLoai();
        }


        private void XoaTrangTheLoai()
        {
            txtMaTheLoai.Clear();
            txtTenTheLoai.Clear();
        }


        // =====================================================
        // NHÀ XUẤT BẢN
        // =====================================================

        private void LoadNhaXuatBan()
        {
            dgvNhaXuatBan.DataSource =
                service.LayNhaXuatBan();
        }


        private void DgvNhaXuatBan_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvNhaXuatBan.CurrentRow == null)
                return;

            if (dgvNhaXuatBan.CurrentRow.Cells["MaNhaXuatBan"].Value == null)
                return;

            txtMaNhaXuatBan.Text =
                dgvNhaXuatBan.CurrentRow
                    .Cells["MaNhaXuatBan"]
                    .Value?.ToString();

            txtTenNhaXuatBan.Text =
                dgvNhaXuatBan.CurrentRow
                    .Cells["TenNhaXuatBan"]
                    .Value?.ToString();

            txtDiaChi.Text =
                dgvNhaXuatBan.CurrentRow
                    .Cells["DiaChi"]
                    .Value?.ToString();

            txtSoDienThoai.Text =
                dgvNhaXuatBan.CurrentRow
                    .Cells["SoDienThoai"]
                    .Value?.ToString();
        }


        private void BtnThemNhaXuatBan_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaNhaXuatBan.Text.Trim() == "" ||
                txtTenNhaXuatBan.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập mã và tên nhà xuất bản.");

                return;
            }

            bool ketQua =
                service.ThemNhaXuatBan(
                    txtMaNhaXuatBan.Text.Trim(),
                    txtTenNhaXuatBan.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSoDienThoai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Thêm nhà xuất bản thành công.");

                LoadNhaXuatBan();
                XoaTrangNhaXuatBan();
            }
        }


        private void BtnSuaNhaXuatBan_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaNhaXuatBan.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn nhà xuất bản cần sửa.");

                return;
            }

            bool ketQua =
                service.SuaNhaXuatBan(
                    txtMaNhaXuatBan.Text.Trim(),
                    txtTenNhaXuatBan.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSoDienThoai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Sửa nhà xuất bản thành công.");

                LoadNhaXuatBan();
            }
        }


        private void BtnXoaNhaXuatBan_Click(
            object? sender,
            EventArgs e)
        {
            if (txtMaNhaXuatBan.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Vui lòng chọn nhà xuất bản cần xóa.");

                return;
            }

            DialogResult hoi =
                MessageBox.Show(
                    "Bạn có chắc muốn xóa nhà xuất bản này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo);

            if (hoi == DialogResult.Yes)
            {
                bool ketQua =
                    service.XoaNhaXuatBan(
                        txtMaNhaXuatBan.Text.Trim());

                if (ketQua)
                {
                    MessageBox.Show(
                        "Xóa nhà xuất bản thành công.");

                    LoadNhaXuatBan();
                    XoaTrangNhaXuatBan();
                }
            }
        }


        private void BtnLamMoiNhaXuatBan_Click(
            object? sender,
            EventArgs e)
        {
            LoadNhaXuatBan();
            XoaTrangNhaXuatBan();
        }


        private void XoaTrangNhaXuatBan()
        {
            txtMaNhaXuatBan.Clear();
            txtTenNhaXuatBan.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
        }
    }
}