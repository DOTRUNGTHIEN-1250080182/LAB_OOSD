using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDanhMuc : Form
    {
        // =========================
        // SERVICE
        // =========================
        private readonly KhuVucService khuVucService = new KhuVucService();
        private readonly NhanVienService nhanVienService = new NhanVienService();

        // =========================
        // KHU VỰC
        // =========================
        private TextBox txtMaKhuVuc;
        private TextBox txtTenKhuVuc;
        private Button btnThemKhuVuc;
        private Button btnSuaKhuVuc;
        private Button btnXoaKhuVuc;
        private Button btnLamMoiKhuVuc;
        private DataGridView dgvKhuVuc;

        // =========================
        // NHÂN VIÊN
        // =========================
        private TextBox txtMaNV;
        private TextBox txtHoTen;
        private TextBox txtVaiTro;
        private TextBox txtSoDienThoai;

        private Button btnThemNV;
        private Button btnSuaNV;
        private Button btnXoaNV;
        private Button btnLamMoiNV;

        private DataGridView dgvNhanVien;

        public FrmDanhMuc()
        {
            InitializeComponent();

            TaoGiaoDien();

            this.Load += FrmDanhMuc_Load;
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================
        private void TaoGiaoDien()
        {
            this.Controls.Clear();

            this.Text = "Quản lý danh mục";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            TabPage tabKhuVuc = new TabPage("Khu vực");
            TabPage tabNhanVien = new TabPage("Nhân viên");
            TabPage tabLoaiTienNghi = new TabPage("Loại tiện nghi");
            TabPage tabDichVu = new TabPage("Dịch vụ");
            TabPage tabQuyDinh = new TabPage("Quy định đền bù");

            TaoTabKhuVuc(tabKhuVuc);
            TaoTabNhanVien(tabNhanVien);
            TaoTabLoaiTienNghi(tabLoaiTienNghi);
            TaoTabDichVu(tabDichVu);
            TaoTabQuyDinh(tabQuyDinh);

            tabControl.TabPages.Add(tabKhuVuc);
            tabControl.TabPages.Add(tabNhanVien);
            tabControl.TabPages.Add(tabLoaiTienNghi);
            tabControl.TabPages.Add(tabDichVu);
            tabControl.TabPages.Add(tabQuyDinh);

            this.Controls.Add(tabControl);
        }

        // =====================================================
        // TAB KHU VỰC
        // =====================================================
        private void TaoTabKhuVuc(TabPage tab)
        {
            Label lblMa = new Label();
            lblMa.Text = "Mã khu vực:";
            lblMa.Location = new Point(20, 20);
            lblMa.AutoSize = true;

            txtMaKhuVuc = new TextBox();
            txtMaKhuVuc.Location = new Point(120, 17);
            txtMaKhuVuc.Width = 150;

            Label lblTen = new Label();
            lblTen.Text = "Tên khu vực:";
            lblTen.Location = new Point(300, 20);
            lblTen.AutoSize = true;

            txtTenKhuVuc = new TextBox();
            txtTenKhuVuc.Location = new Point(390, 17);
            txtTenKhuVuc.Width = 200;

            btnThemKhuVuc = new Button();
            btnThemKhuVuc.Text = "Thêm";
            btnThemKhuVuc.Location = new Point(620, 15);
            btnThemKhuVuc.Click += BtnThemKhuVuc_Click;

            btnSuaKhuVuc = new Button();
            btnSuaKhuVuc.Text = "Sửa";
            btnSuaKhuVuc.Location = new Point(700, 15);
            btnSuaKhuVuc.Click += BtnSuaKhuVuc_Click;

            btnXoaKhuVuc = new Button();
            btnXoaKhuVuc.Text = "Xóa";
            btnXoaKhuVuc.Location = new Point(780, 15);
            btnXoaKhuVuc.Click += BtnXoaKhuVuc_Click;

            btnLamMoiKhuVuc = new Button();
            btnLamMoiKhuVuc.Text = "Làm mới";
            btnLamMoiKhuVuc.Location = new Point(850, 15);
            btnLamMoiKhuVuc.Click += BtnLamMoiKhuVuc_Click;

            dgvKhuVuc = new DataGridView();
            dgvKhuVuc.Location = new Point(20, 60);
            dgvKhuVuc.Size = new Size(900, 480);
            dgvKhuVuc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhuVuc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhuVuc.MultiSelect = false;
            dgvKhuVuc.ReadOnly = true;
            dgvKhuVuc.CellClick += DgvKhuVuc_CellClick;

            tab.Controls.Add(lblMa);
            tab.Controls.Add(txtMaKhuVuc);
            tab.Controls.Add(lblTen);
            tab.Controls.Add(txtTenKhuVuc);
            tab.Controls.Add(btnThemKhuVuc);
            tab.Controls.Add(btnSuaKhuVuc);
            tab.Controls.Add(btnXoaKhuVuc);
            tab.Controls.Add(btnLamMoiKhuVuc);
            tab.Controls.Add(dgvKhuVuc);
        }

        // =====================================================
        // TAB NHÂN VIÊN
        // =====================================================
        private void TaoTabNhanVien(TabPage tab)
        {
            Label lblMa = new Label();
            lblMa.Text = "Mã NV:";
            lblMa.Location = new Point(20, 20);
            lblMa.AutoSize = true;

            txtMaNV = new TextBox();
            txtMaNV.Location = new Point(80, 17);
            txtMaNV.Width = 130;

            Label lblHoTen = new Label();
            lblHoTen.Text = "Họ tên:";
            lblHoTen.Location = new Point(230, 20);
            lblHoTen.AutoSize = true;

            txtHoTen = new TextBox();
            txtHoTen.Location = new Point(285, 17);
            txtHoTen.Width = 180;

            Label lblVaiTro = new Label();
            lblVaiTro.Text = "Vai trò:";
            lblVaiTro.Location = new Point(485, 20);
            lblVaiTro.AutoSize = true;

            txtVaiTro = new TextBox();
            txtVaiTro.Location = new Point(540, 17);
            txtVaiTro.Width = 130;

            Label lblSDT = new Label();
            lblSDT.Text = "SĐT:";
            lblSDT.Location = new Point(690, 20);
            lblSDT.AutoSize = true;

            txtSoDienThoai = new TextBox();
            txtSoDienThoai.Location = new Point(730, 17);
            txtSoDienThoai.Width = 130;

            btnThemNV = new Button();
            btnThemNV.Text = "Thêm";
            btnThemNV.Location = new Point(20, 55);
            btnThemNV.Click += BtnThemNV_Click;

            btnSuaNV = new Button();
            btnSuaNV.Text = "Sửa";
            btnSuaNV.Location = new Point(100, 55);
            btnSuaNV.Click += BtnSuaNV_Click;

            btnXoaNV = new Button();
            btnXoaNV.Text = "Xóa";
            btnXoaNV.Location = new Point(180, 55);
            btnXoaNV.Click += BtnXoaNV_Click;

            btnLamMoiNV = new Button();
            btnLamMoiNV.Text = "Làm mới";
            btnLamMoiNV.Location = new Point(260, 55);
            btnLamMoiNV.Click += BtnLamMoiNV_Click;

            dgvNhanVien = new DataGridView();
            dgvNhanVien.Location = new Point(20, 100);
            dgvNhanVien.Size = new Size(900, 440);
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.CellClick += DgvNhanVien_CellClick;

            tab.Controls.Add(lblMa);
            tab.Controls.Add(txtMaNV);
            tab.Controls.Add(lblHoTen);
            tab.Controls.Add(txtHoTen);
            tab.Controls.Add(lblVaiTro);
            tab.Controls.Add(txtVaiTro);
            tab.Controls.Add(lblSDT);
            tab.Controls.Add(txtSoDienThoai);

            tab.Controls.Add(btnThemNV);
            tab.Controls.Add(btnSuaNV);
            tab.Controls.Add(btnXoaNV);
            tab.Controls.Add(btnLamMoiNV);

            tab.Controls.Add(dgvNhanVien);
        }

        // =====================================================
        // CÁC TAB CHƯA LÀM
        // =====================================================
        private void TaoTabLoaiTienNghi(TabPage tab)
        {
            Label lbl = new Label();
            lbl.Text = "Quản lý Loại tiện nghi";
            lbl.Font = new Font("Arial", 16, FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new Point(30, 30);

            tab.Controls.Add(lbl);
        }

        private void TaoTabDichVu(TabPage tab)
        {
            Label lbl = new Label();
            lbl.Text = "Quản lý Dịch vụ";
            lbl.Font = new Font("Arial", 16, FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new Point(30, 30);

            tab.Controls.Add(lbl);
        }

        private void TaoTabQuyDinh(TabPage tab)
        {
            Label lbl = new Label();
            lbl.Text = "Quản lý Quy định đền bù";
            lbl.Font = new Font("Arial", 16, FontStyle.Bold);
            lbl.AutoSize = true;
            lbl.Location = new Point(30, 30);

            tab.Controls.Add(lbl);
        }

        // =====================================================
        // FORM LOAD
        // =====================================================
        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            TaiDuLieuKhuVuc();
            TaiDuLieuNhanVien();
        }

        // =====================================================
        // KHU VỰC - LOAD
        // =====================================================
        private void TaiDuLieuKhuVuc()
        {
            try
            {
                dgvKhuVuc.DataSource = khuVucService.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải khu vực: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // KHU VỰC - THÊM
        // =====================================================
        private void BtnThemKhuVuc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhuVuc.Text) ||
                string.IsNullOrWhiteSpace(txtTenKhuVuc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            bool ketQua = khuVucService.Them(
                txtMaKhuVuc.Text.Trim(),
                txtTenKhuVuc.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show("Thêm khu vực thành công!");
                TaiDuLieuKhuVuc();
                XoaTrangKhuVuc();
            }
            else
            {
                MessageBox.Show("Thêm khu vực thất bại!");
            }
        }

        // =====================================================
        // KHU VỰC - SỬA
        // =====================================================
        private void BtnSuaKhuVuc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhuVuc.Text) ||
                string.IsNullOrWhiteSpace(txtTenKhuVuc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            bool ketQua = khuVucService.Sua(
                txtMaKhuVuc.Text.Trim(),
                txtTenKhuVuc.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show("Sửa khu vực thành công!");
                TaiDuLieuKhuVuc();
                XoaTrangKhuVuc();
            }
            else
            {
                MessageBox.Show("Sửa khu vực thất bại!");
            }
        }

        // =====================================================
        // KHU VỰC - XÓA
        // =====================================================
        private void BtnXoaKhuVuc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhuVuc.Text))
            {
                MessageBox.Show("Vui lòng chọn khu vực cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa khu vực này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool ketQua = khuVucService.Xoa(txtMaKhuVuc.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show("Xóa khu vực thành công!");
                TaiDuLieuKhuVuc();
                XoaTrangKhuVuc();
            }
            else
            {
                MessageBox.Show(
                    "Không thể xóa khu vực.\nCó thể khu vực đang được sử dụng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // KHU VỰC - LÀM MỚI
        // =====================================================
        private void BtnLamMoiKhuVuc_Click(object sender, EventArgs e)
        {
            XoaTrangKhuVuc();
            TaiDuLieuKhuVuc();
        }

        private void XoaTrangKhuVuc()
        {
            txtMaKhuVuc.Clear();
            txtTenKhuVuc.Clear();
            txtMaKhuVuc.Focus();
        }

        // =====================================================
        // KHU VỰC - CLICK GRID
        // =====================================================
        private void DgvKhuVuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvKhuVuc.Rows[e.RowIndex];

            txtMaKhuVuc.Text = row.Cells["MaKhuVuc"].Value.ToString();
            txtTenKhuVuc.Text = row.Cells["TenKhuVuc"].Value.ToString();

            txtMaKhuVuc.Enabled = false;
        }

        // =====================================================
        // NHÂN VIÊN - LOAD
        // =====================================================
        private void TaiDuLieuNhanVien()
        {
            try
            {
                dgvNhanVien.DataSource = nhanVienService.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải nhân viên: " + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // NHÂN VIÊN - THÊM
        // =====================================================
        private void BtnThemNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtVaiTro.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin nhân viên!");
                return;
            }

            bool ketQua = nhanVienService.Them(
                txtMaNV.Text.Trim(),
                txtHoTen.Text.Trim(),
                txtVaiTro.Text.Trim(),
                txtSoDienThoai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                TaiDuLieuNhanVien();
                XoaTrangNhanVien();
            }
            else
            {
                MessageBox.Show("Thêm nhân viên thất bại!");
            }
        }

        // =====================================================
        // NHÂN VIÊN - SỬA
        // =====================================================
        private void BtnSuaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!");
                return;
            }

            bool ketQua = nhanVienService.Sua(
                txtMaNV.Text.Trim(),
                txtHoTen.Text.Trim(),
                txtVaiTro.Text.Trim(),
                txtSoDienThoai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show("Sửa nhân viên thành công!");
                TaiDuLieuNhanVien();
                XoaTrangNhanVien();
            }
            else
            {
                MessageBox.Show("Sửa nhân viên thất bại!");
            }
        }

        // =====================================================
        // NHÂN VIÊN - XÓA
        // =====================================================
        private void BtnXoaNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa nhân viên này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool ketQua = nhanVienService.Xoa(txtMaNV.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show("Xóa nhân viên thành công!");
                TaiDuLieuNhanVien();
                XoaTrangNhanVien();
            }
            else
            {
                MessageBox.Show(
                    "Không thể xóa nhân viên.\nCó thể nhân viên đang được sử dụng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // NHÂN VIÊN - LÀM MỚI
        // =====================================================
        private void BtnLamMoiNV_Click(object sender, EventArgs e)
        {
            XoaTrangNhanVien();
            TaiDuLieuNhanVien();
        }

        private void XoaTrangNhanVien()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtVaiTro.Clear();
            txtSoDienThoai.Clear();

            txtMaNV.Enabled = true;
            txtMaNV.Focus();
        }

        // =====================================================
        // NHÂN VIÊN - CLICK GRID
        // =====================================================
        private void DgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

            txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            txtVaiTro.Text = row.Cells["VaiTro"].Value.ToString();
            txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();

            txtMaNV.Enabled = false;
        }
    }
}