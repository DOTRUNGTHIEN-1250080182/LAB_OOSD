using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDichVu : Form
    {
        private DichVuService service = new DichVuService();

        private DataGridView dgvDichVu;
        private DataGridView dgvPhieuSuDung;
        private DataGridView dgvChiTiet;

        private TextBox txtMaDV;
        private TextBox txtTenDV;
        private TextBox txtDonGia;

        private TextBox txtSoPhieuSDDV;
        private TextBox txtSoPhieuDat;

        private TextBox txtMaDVChiTiet;
        private TextBox txtSoLuong;
        private TextBox txtDonGiaChiTiet;

        private DateTimePicker dtpNgaySuDung;

        private Button btnThemDV;
        private Button btnSuaDV;
        private Button btnXoaDV;
        private Button btnLamMoiDV;

        private Button btnThemPhieu;
        private Button btnLamMoiPhieu;

        private Button btnThemChiTiet;
        private Button btnLamMoiChiTiet;

        public FrmDichVu()
        {
            InitializeComponent();

            TaoGiaoDien();

            this.Load += FrmDichVu_Load;
        }

        private void FrmDichVu_Load(object sender, EventArgs e)
        {
            TaiDanhSachDichVu();
            TaiDanhSachPhieuSuDung();
            TaiDanhSachChiTiet();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================

        private void TaoGiaoDien()
        {
            this.Controls.Clear();

            this.Text = "Quản lý dịch vụ";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(1000, 600);
            this.BackColor = Color.White;

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ DỊCH VỤ";
            lblTieuDe.Font = new Font("Arial", 20, FontStyle.Bold);
            lblTieuDe.AutoSize = false;
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 60;

            this.Controls.Add(lblTieuDe);

            TabControl tabControl = new TabControl();
            tabControl.Left = 20;
            tabControl.Top = 70;
            tabControl.Width = 1040;
            tabControl.Height = 570;

            // =================================================
            // TAB 1 - DỊCH VỤ
            // =================================================

            TabPage tabDichVu = new TabPage("Dịch vụ");

            Label lblMaDV = new Label();
            lblMaDV.Text = "Mã dịch vụ:";
            lblMaDV.Left = 20;
            lblMaDV.Top = 25;
            lblMaDV.Width = 100;

            txtMaDV = new TextBox();
            txtMaDV.Left = 130;
            txtMaDV.Top = 20;
            txtMaDV.Width = 180;

            Label lblTenDV = new Label();
            lblTenDV.Text = "Tên dịch vụ:";
            lblTenDV.Left = 340;
            lblTenDV.Top = 25;
            lblTenDV.Width = 100;

            txtTenDV = new TextBox();
            txtTenDV.Left = 450;
            txtTenDV.Top = 20;
            txtTenDV.Width = 220;

            Label lblDonGia = new Label();
            lblDonGia.Text = "Đơn giá:";
            lblDonGia.Left = 700;
            lblDonGia.Top = 25;
            lblDonGia.Width = 70;

            txtDonGia = new TextBox();
            txtDonGia.Left = 775;
            txtDonGia.Top = 20;
            txtDonGia.Width = 150;

            btnThemDV = TaoButton("Thêm", 20, 65, 100);
            btnSuaDV = TaoButton("Sửa", 130, 65, 100);
            btnXoaDV = TaoButton("Xóa", 240, 65, 100);
            btnLamMoiDV = TaoButton("Làm mới", 350, 65, 100);

            btnThemDV.Click += BtnThemDV_Click;
            btnSuaDV.Click += BtnSuaDV_Click;
            btnXoaDV.Click += BtnXoaDV_Click;
            btnLamMoiDV.Click += BtnLamMoiDV_Click;

            dgvDichVu = new DataGridView();
            dgvDichVu.Left = 20;
            dgvDichVu.Top = 115;
            dgvDichVu.Width = 980;
            dgvDichVu.Height = 380;
            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDichVu.ReadOnly = true;
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.CellClick += DgvDichVu_CellClick;

            tabDichVu.Controls.Add(lblMaDV);
            tabDichVu.Controls.Add(txtMaDV);
            tabDichVu.Controls.Add(lblTenDV);
            tabDichVu.Controls.Add(txtTenDV);
            tabDichVu.Controls.Add(lblDonGia);
            tabDichVu.Controls.Add(txtDonGia);

            tabDichVu.Controls.Add(btnThemDV);
            tabDichVu.Controls.Add(btnSuaDV);
            tabDichVu.Controls.Add(btnXoaDV);
            tabDichVu.Controls.Add(btnLamMoiDV);

            tabDichVu.Controls.Add(dgvDichVu);

            // =================================================
            // TAB 2 - PHIẾU SỬ DỤNG DỊCH VỤ
            // =================================================

            TabPage tabPhieu = new TabPage("Phiếu sử dụng");

            Label lblSoPhieuSDDV = new Label();
            lblSoPhieuSDDV.Text = "Số phiếu:";
            lblSoPhieuSDDV.Left = 20;
            lblSoPhieuSDDV.Top = 25;
            lblSoPhieuSDDV.Width = 100;

            txtSoPhieuSDDV = new TextBox();
            txtSoPhieuSDDV.Left = 130;
            txtSoPhieuSDDV.Top = 20;
            txtSoPhieuSDDV.Width = 180;

            Label lblSoPhieuDat = new Label();
            lblSoPhieuDat.Text = "Số phiếu đặt:";
            lblSoPhieuDat.Left = 340;
            lblSoPhieuDat.Top = 25;
            lblSoPhieuDat.Width = 100;

            txtSoPhieuDat = new TextBox();
            txtSoPhieuDat.Left = 450;
            txtSoPhieuDat.Top = 20;
            txtSoPhieuDat.Width = 180;

            Label lblNgaySuDung = new Label();
            lblNgaySuDung.Text = "Ngày sử dụng:";
            lblNgaySuDung.Left = 650;
            lblNgaySuDung.Top = 25;
            lblNgaySuDung.Width = 100;

            dtpNgaySuDung = new DateTimePicker();
            dtpNgaySuDung.Left = 760;
            dtpNgaySuDung.Top = 20;
            dtpNgaySuDung.Width = 180;
            dtpNgaySuDung.Format = DateTimePickerFormat.Short;

            btnThemPhieu = TaoButton("Thêm phiếu", 20, 65, 120);
            btnLamMoiPhieu = TaoButton("Làm mới", 150, 65, 100);

            btnThemPhieu.Click += BtnThemPhieu_Click;
            btnLamMoiPhieu.Click += BtnLamMoiPhieu_Click;

            dgvPhieuSuDung = new DataGridView();
            dgvPhieuSuDung.Left = 20;
            dgvPhieuSuDung.Top = 115;
            dgvPhieuSuDung.Width = 980;
            dgvPhieuSuDung.Height = 380;
            dgvPhieuSuDung.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhieuSuDung.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPhieuSuDung.ReadOnly = true;
            dgvPhieuSuDung.AllowUserToAddRows = false;

            tabPhieu.Controls.Add(lblSoPhieuSDDV);
            tabPhieu.Controls.Add(txtSoPhieuSDDV);
            tabPhieu.Controls.Add(lblSoPhieuDat);
            tabPhieu.Controls.Add(txtSoPhieuDat);
            tabPhieu.Controls.Add(lblNgaySuDung);
            tabPhieu.Controls.Add(dtpNgaySuDung);

            tabPhieu.Controls.Add(btnThemPhieu);
            tabPhieu.Controls.Add(btnLamMoiPhieu);
            tabPhieu.Controls.Add(dgvPhieuSuDung);

            // =================================================
            // TAB 3 - CHI TIẾT SỬ DỤNG
            // =================================================

            TabPage tabChiTiet = new TabPage("Chi tiết sử dụng");

            Label lblPhieuCT = new Label();
            lblPhieuCT.Text = "Số phiếu:";
            lblPhieuCT.Left = 20;
            lblPhieuCT.Top = 25;
            lblPhieuCT.Width = 90;

            TextBox txtPhieuCT = new TextBox();
            txtPhieuCT.Name = "txtPhieuCT";
            txtPhieuCT.Left = 110;
            txtPhieuCT.Top = 20;
            txtPhieuCT.Width = 150;

            Label lblMaDVCT = new Label();
            lblMaDVCT.Text = "Mã dịch vụ:";
            lblMaDVCT.Left = 280;
            lblMaDVCT.Top = 25;
            lblMaDVCT.Width = 90;

            txtMaDVChiTiet = new TextBox();
            txtMaDVChiTiet.Left = 370;
            txtMaDVChiTiet.Top = 20;
            txtMaDVChiTiet.Width = 150;

            Label lblSoLuong = new Label();
            lblSoLuong.Text = "Số lượng:";
            lblSoLuong.Left = 540;
            lblSoLuong.Top = 25;
            lblSoLuong.Width = 80;

            txtSoLuong = new TextBox();
            txtSoLuong.Left = 620;
            txtSoLuong.Top = 20;
            txtSoLuong.Width = 100;

            Label lblDonGiaCT = new Label();
            lblDonGiaCT.Text = "Đơn giá:";
            lblDonGiaCT.Left = 740;
            lblDonGiaCT.Top = 25;
            lblDonGiaCT.Width = 70;

            txtDonGiaChiTiet = new TextBox();
            txtDonGiaChiTiet.Left = 810;
            txtDonGiaChiTiet.Top = 20;
            txtDonGiaChiTiet.Width = 130;

            btnThemChiTiet = TaoButton("Thêm chi tiết", 20, 65, 120);
            btnLamMoiChiTiet = TaoButton("Làm mới", 150, 65, 100);

            btnThemChiTiet.Click += BtnThemChiTiet_Click;
            btnLamMoiChiTiet.Click += BtnLamMoiChiTiet_Click;

            dgvChiTiet = new DataGridView();
            dgvChiTiet.Left = 20;
            dgvChiTiet.Top = 115;
            dgvChiTiet.Width = 980;
            dgvChiTiet.Height = 380;
            dgvChiTiet.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.AllowUserToAddRows = false;

            tabChiTiet.Controls.Add(lblPhieuCT);
            tabChiTiet.Controls.Add(txtPhieuCT);
            tabChiTiet.Controls.Add(lblMaDVCT);
            tabChiTiet.Controls.Add(txtMaDVChiTiet);
            tabChiTiet.Controls.Add(lblSoLuong);
            tabChiTiet.Controls.Add(txtSoLuong);
            tabChiTiet.Controls.Add(lblDonGiaCT);
            tabChiTiet.Controls.Add(txtDonGiaChiTiet);

            tabChiTiet.Controls.Add(btnThemChiTiet);
            tabChiTiet.Controls.Add(btnLamMoiChiTiet);
            tabChiTiet.Controls.Add(dgvChiTiet);

            tabControl.TabPages.Add(tabDichVu);
            tabControl.TabPages.Add(tabPhieu);
            tabControl.TabPages.Add(tabChiTiet);

            this.Controls.Add(tabControl);
        }

        // =====================================================
        // TẠO BUTTON
        // =====================================================

        private Button TaoButton(
            string text,
            int left,
            int top,
            int width)
        {
            Button button = new Button();

            button.Text = text;
            button.Left = left;
            button.Top = top;
            button.Width = width;
            button.Height = 35;
            button.Font = new Font(
                "Arial",
                9,
                FontStyle.Bold);

            button.Cursor = Cursors.Hand;

            return button;
        }

        // =====================================================
        // DỊCH VỤ
        // =====================================================

        private void TaiDanhSachDichVu()
        {
            try
            {
                dgvDichVu.DataSource =
                    service.LayDanhSachDichVu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách dịch vụ.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnThemDV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text) ||
                string.IsNullOrWhiteSpace(txtTenDV.Text) ||
                string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin dịch vụ.");
                return;
            }

            if (!decimal.TryParse(
                txtDonGia.Text,
                out decimal donGia))
            {
                MessageBox.Show(
                    "Đơn giá không hợp lệ.");
                return;
            }

            bool result = service.ThemDichVu(
                txtMaDV.Text.Trim(),
                txtTenDV.Text.Trim(),
                donGia);

            if (result)
            {
                MessageBox.Show(
                    "Thêm dịch vụ thành công.");

                TaiDanhSachDichVu();
                LamMoiDichVu();
            }
            else
            {
                MessageBox.Show(
                    "Không thể thêm dịch vụ. " +
                    "Có thể mã dịch vụ đã tồn tại.");
            }
        }

        private void BtnSuaDV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn dịch vụ cần sửa.");
                return;
            }

            if (!decimal.TryParse(
                txtDonGia.Text,
                out decimal donGia))
            {
                MessageBox.Show(
                    "Đơn giá không hợp lệ.");
                return;
            }

            bool result = service.SuaDichVu(
                txtMaDV.Text.Trim(),
                txtTenDV.Text.Trim(),
                donGia);

            if (result)
            {
                MessageBox.Show(
                    "Cập nhật dịch vụ thành công.");

                TaiDanhSachDichVu();
                LamMoiDichVu();
            }
            else
            {
                MessageBox.Show(
                    "Không thể cập nhật dịch vụ.");
            }
        }

        private void BtnXoaDV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn dịch vụ cần xóa.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa dịch vụ này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool success = service.XoaDichVu(
                txtMaDV.Text.Trim());

            if (success)
            {
                MessageBox.Show(
                    "Xóa dịch vụ thành công.");

                TaiDanhSachDichVu();
                LamMoiDichVu();
            }
            else
            {
                MessageBox.Show(
                    "Không thể xóa dịch vụ. " +
                    "Dịch vụ có thể đang được sử dụng.");
            }
        }

        private void BtnLamMoiDV_Click(object sender, EventArgs e)
        {
            LamMoiDichVu();
            TaiDanhSachDichVu();
        }

        private void DgvDichVu_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvDichVu.Rows[e.RowIndex];

            txtMaDV.Text =
                row.Cells["MaDV"].Value?.ToString();

            txtTenDV.Text =
                row.Cells["TenDV"].Value?.ToString();

            txtDonGia.Text =
                row.Cells["DonGia"].Value?.ToString();
        }

        private void LamMoiDichVu()
        {
            txtMaDV.Clear();
            txtTenDV.Clear();
            txtDonGia.Clear();
        }

        // =====================================================
        // PHIẾU SỬ DỤNG
        // =====================================================

        private void TaiDanhSachPhieuSuDung()
        {
            try
            {
                dgvPhieuSuDung.DataSource =
                    service.LayDanhSachPhieuSuDung();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải phiếu sử dụng dịch vụ.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnThemPhieu_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtSoPhieuSDDV.Text) ||
                string.IsNullOrWhiteSpace(
                txtSoPhieuDat.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            bool result = service.ThemPhieuSuDung(
                txtSoPhieuSDDV.Text.Trim(),
                txtSoPhieuDat.Text.Trim(),
                dtpNgaySuDung.Value);

            if (result)
            {
                MessageBox.Show(
                    "Thêm phiếu sử dụng thành công.");

                TaiDanhSachPhieuSuDung();
                LamMoiPhieu();
            }
            else
            {
                MessageBox.Show(
                    "Không thể thêm phiếu sử dụng.");
            }
        }

        private void BtnLamMoiPhieu_Click(
            object sender,
            EventArgs e)
        {
            LamMoiPhieu();
            TaiDanhSachPhieuSuDung();
        }

        private void LamMoiPhieu()
        {
            txtSoPhieuSDDV.Clear();
            txtSoPhieuDat.Clear();
            dtpNgaySuDung.Value = DateTime.Now;
        }

        // =====================================================
        // CHI TIẾT
        // =====================================================

        private void TaiDanhSachChiTiet()
        {
            try
            {
                dgvChiTiet.DataSource =
                    service.LayChiTietSuDung();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải chi tiết sử dụng.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnThemChiTiet_Click(
            object sender,
            EventArgs e)
        {
            TextBox txtPhieuCT =
                this.Controls
                    .Find("txtPhieuCT", true)
                    .Length > 0
                    ? this.Controls
                        .Find("txtPhieuCT", true)[0]
                        as TextBox
                    : null;

            if (txtPhieuCT == null)
            {
                MessageBox.Show(
                    "Không tìm thấy số phiếu.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtPhieuCT.Text) ||
                string.IsNullOrWhiteSpace(
                txtMaDVChiTiet.Text) ||
                string.IsNullOrWhiteSpace(
                txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(
                txtDonGiaChiTiet.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (!int.TryParse(
                txtSoLuong.Text,
                out int soLuong))
            {
                MessageBox.Show(
                    "Số lượng không hợp lệ.");
                return;
            }

            if (!decimal.TryParse(
                txtDonGiaChiTiet.Text,
                out decimal donGia))
            {
                MessageBox.Show(
                    "Đơn giá không hợp lệ.");
                return;
            }

            bool result =
                service.ThemChiTietSuDung(
                    txtPhieuCT.Text.Trim(),
                    txtMaDVChiTiet.Text.Trim(),
                    soLuong,
                    donGia);

            if (result)
            {
                MessageBox.Show(
                    "Thêm chi tiết sử dụng thành công.");

                TaiDanhSachChiTiet();
                LamMoiChiTiet();
            }
            else
            {
                MessageBox.Show(
                    "Không thể thêm chi tiết sử dụng.");
            }
        }

        private void BtnLamMoiChiTiet_Click(
            object sender,
            EventArgs e)
        {
            LamMoiChiTiet();
            TaiDanhSachChiTiet();
        }

        private void LamMoiChiTiet()
        {
            TextBox txtPhieuCT =
                this.Controls
                    .Find("txtPhieuCT", true)
                    .Length > 0
                    ? this.Controls
                        .Find("txtPhieuCT", true)[0]
                        as TextBox
                    : null;

            if (txtPhieuCT != null)
                txtPhieuCT.Clear();

            txtMaDVChiTiet.Clear();
            txtSoLuong.Clear();
            txtDonGiaChiTiet.Clear();
        }
    }
}