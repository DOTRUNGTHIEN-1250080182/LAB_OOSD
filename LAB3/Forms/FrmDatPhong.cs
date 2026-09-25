using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDatPhong : Form
    {
        private readonly DatPhongService service =
            new DatPhongService();

        // =====================================================
        // KHÁCH HÀNG
        // =====================================================

        private TextBox txtMaKhach;
        private TextBox txtHoTen;
        private TextBox txtSoCMND;
        private TextBox txtQuocTich;

        private Button btnThemKhach;
        private Button btnLamMoiKhach;

        private DataGridView dgvKhachHang;

        // =====================================================
        // ĐẶT PHÒNG
        // =====================================================

        private TextBox txtSoPhieuDat;
        private ComboBox cboMaKhach;
        private ComboBox cboNhanVien;
        private ComboBox cboSoPhong;
        private ComboBox cboKenhDat;

        private DateTimePicker dtpNgayLap;
        private DateTimePicker dtpNgayNhan;
        private DateTimePicker dtpNgayTra;

        private TextBox txtTienCoc;
        private NumericUpDown nudSoNguoi;

        private Button btnTaoPhieu;
        private Button btnThemPhong;
        private Button btnLamMoi;

        private DataGridView dgvDatPhong;

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public FrmDatPhong()
        {
            InitializeComponent();

            TaoGiaoDien();

            this.Load += FrmDatPhong_Load;
        }

        // =====================================================
        // LOAD FORM
        // =====================================================

        private void FrmDatPhong_Load(object sender, EventArgs e)
        {
            TaiKhachHang();
            TaiNhanVien();
            TaiPhong();
            TaiDanhSachDatPhong();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================

        private void TaoGiaoDien()
        {
            this.Controls.Clear();

            this.Text = "QUẢN LÝ ĐẶT PHÒNG";
            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.Size =
                new Size(1150, 720);

            this.MinimumSize =
                new Size(1000, 650);

            this.BackColor =
                Color.White;

            TabControl tabControl =
                new TabControl();

            tabControl.Dock =
                DockStyle.Fill;

            TabPage tabKhachHang =
                new TabPage("Khách hàng");

            TabPage tabDatPhong =
                new TabPage("Đặt phòng");

            TaoTabKhachHang(tabKhachHang);
            TaoTabDatPhong(tabDatPhong);

            tabControl.TabPages.Add(tabKhachHang);
            tabControl.TabPages.Add(tabDatPhong);

            this.Controls.Add(tabControl);
        }

        // =====================================================
        // TAB KHÁCH HÀNG
        // =====================================================

        private void TaoTabKhachHang(TabPage tab)
        {
            // -----------------------------
            // Mã khách
            // -----------------------------

            Label lblMaKhach =
                new Label();

            lblMaKhach.Text =
                "Mã khách:";

            lblMaKhach.Location =
                new Point(20, 20);

            lblMaKhach.AutoSize =
                true;

            txtMaKhach =
                new TextBox();

            txtMaKhach.Location =
                new Point(100, 17);

            txtMaKhach.Width =
                130;

            // -----------------------------
            // Họ tên
            // -----------------------------

            Label lblHoTen =
                new Label();

            lblHoTen.Text =
                "Họ tên:";

            lblHoTen.Location =
                new Point(250, 20);

            lblHoTen.AutoSize =
                true;

            txtHoTen =
                new TextBox();

            txtHoTen.Location =
                new Point(310, 17);

            txtHoTen.Width =
                200;

            // -----------------------------
            // Số CMND
            // -----------------------------

            Label lblSoCMND =
                new Label();

            lblSoCMND.Text =
                "Số CMND:";

            lblSoCMND.Location =
                new Point(530, 20);

            lblSoCMND.AutoSize =
                true;

            txtSoCMND =
                new TextBox();

            txtSoCMND.Location =
                new Point(600, 17);

            txtSoCMND.Width =
                150;

            // -----------------------------
            // Quốc tịch
            // -----------------------------

            Label lblQuocTich =
                new Label();

            lblQuocTich.Text =
                "Quốc tịch:";

            lblQuocTich.Location =
                new Point(770, 20);

            lblQuocTich.AutoSize =
                true;

            txtQuocTich =
                new TextBox();

            txtQuocTich.Location =
                new Point(840, 17);

            txtQuocTich.Width =
                150;

            // -----------------------------
            // Nút thêm
            // -----------------------------

            btnThemKhach =
                new Button();

            btnThemKhach.Text =
                "Thêm khách";

            btnThemKhach.Location =
                new Point(20, 60);

            btnThemKhach.Width =
                110;

            btnThemKhach.Click +=
                BtnThemKhach_Click;

            // -----------------------------
            // Nút làm mới
            // -----------------------------

            btnLamMoiKhach =
                new Button();

            btnLamMoiKhach.Text =
                "Làm mới";

            btnLamMoiKhach.Location =
                new Point(140, 60);

            btnLamMoiKhach.Width =
                100;

            btnLamMoiKhach.Click +=
                BtnLamMoiKhach_Click;

            // -----------------------------
            // DataGridView
            // -----------------------------

            dgvKhachHang =
                new DataGridView();

            dgvKhachHang.Location =
                new Point(20, 110);

            dgvKhachHang.Size =
                new Size(1020, 480);

            dgvKhachHang.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvKhachHang.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvKhachHang.MultiSelect =
                false;

            dgvKhachHang.ReadOnly =
                true;

            // -----------------------------
            // Thêm vào Tab
            // -----------------------------

            tab.Controls.Add(lblMaKhach);
            tab.Controls.Add(txtMaKhach);

            tab.Controls.Add(lblHoTen);
            tab.Controls.Add(txtHoTen);

            tab.Controls.Add(lblSoCMND);
            tab.Controls.Add(txtSoCMND);

            tab.Controls.Add(lblQuocTich);
            tab.Controls.Add(txtQuocTich);

            tab.Controls.Add(btnThemKhach);
            tab.Controls.Add(btnLamMoiKhach);

            tab.Controls.Add(dgvKhachHang);
        }

        // =====================================================
        // TAB ĐẶT PHÒNG
        // =====================================================

        private void TaoTabDatPhong(TabPage tab)
        {
            // -----------------------------
            // Số phiếu
            // -----------------------------

            Label lblSoPhieu =
                new Label();

            lblSoPhieu.Text =
                "Số phiếu:";

            lblSoPhieu.Location =
                new Point(20, 20);

            lblSoPhieu.AutoSize =
                true;

            txtSoPhieuDat =
                new TextBox();

            txtSoPhieuDat.Location =
                new Point(90, 17);

            txtSoPhieuDat.Width =
                130;

            // -----------------------------
            // Khách hàng
            // -----------------------------

            Label lblKhach =
                new Label();

            lblKhach.Text =
                "Khách hàng:";

            lblKhach.Location =
                new Point(240, 20);

            lblKhach.AutoSize =
                true;

            cboMaKhach =
                new ComboBox();

            cboMaKhach.Location =
                new Point(320, 17);

            cboMaKhach.Width =
                200;

            cboMaKhach.DropDownStyle =
                ComboBoxStyle.DropDownList;

            // -----------------------------
            // Nhân viên
            // -----------------------------

            Label lblNhanVien =
                new Label();

            lblNhanVien.Text =
                "Nhân viên:";

            lblNhanVien.Location =
                new Point(540, 20);

            lblNhanVien.AutoSize =
                true;

            cboNhanVien =
                new ComboBox();

            cboNhanVien.Location =
                new Point(610, 17);

            cboNhanVien.Width =
                180;

            cboNhanVien.DropDownStyle =
                ComboBoxStyle.DropDownList;

            // -----------------------------
            // Kênh đặt
            // -----------------------------

            Label lblKenhDat =
                new Label();

            lblKenhDat.Text =
                "Kênh đặt:";

            lblKenhDat.Location =
                new Point(810, 20);

            lblKenhDat.AutoSize =
                true;

            cboKenhDat =
                new ComboBox();

            cboKenhDat.Location =
                new Point(880, 17);

            cboKenhDat.Width =
                140;

            cboKenhDat.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboKenhDat.Items.Add("Điện thoại");
            cboKenhDat.Items.Add("Website");
            cboKenhDat.Items.Add("Trực tiếp");

            cboKenhDat.SelectedIndex = 2;

            // -----------------------------
            // Ngày lập
            // -----------------------------

            Label lblNgayLap =
                new Label();

            lblNgayLap.Text =
                "Ngày lập:";

            lblNgayLap.Location =
                new Point(20, 65);

            lblNgayLap.AutoSize =
                true;

            dtpNgayLap =
                new DateTimePicker();

            dtpNgayLap.Location =
                new Point(90, 62);

            dtpNgayLap.Width =
                130;

            dtpNgayLap.Format =
                DateTimePickerFormat.Short;

            dtpNgayLap.Value =
                DateTime.Today;

            // -----------------------------
            // Ngày nhận
            // -----------------------------

            Label lblNgayNhan =
                new Label();

            lblNgayNhan.Text =
                "Ngày nhận:";

            lblNgayNhan.Location =
                new Point(240, 65);

            lblNgayNhan.AutoSize =
                true;

            dtpNgayNhan =
                new DateTimePicker();

            dtpNgayNhan.Location =
                new Point(320, 62);

            dtpNgayNhan.Width =
                130;

            dtpNgayNhan.Format =
                DateTimePickerFormat.Short;

            dtpNgayNhan.Value =
                DateTime.Today;

            // -----------------------------
            // Ngày trả
            // -----------------------------

            Label lblNgayTra =
                new Label();

            lblNgayTra.Text =
                "Ngày trả:";

            lblNgayTra.Location =
                new Point(470, 65);

            lblNgayTra.AutoSize =
                true;

            dtpNgayTra =
                new DateTimePicker();

            dtpNgayTra.Location =
                new Point(530, 62);

            dtpNgayTra.Width =
                130;

            dtpNgayTra.Format =
                DateTimePickerFormat.Short;

            dtpNgayTra.Value =
                DateTime.Today.AddDays(1);

            // -----------------------------
            // Tiền cọc
            // -----------------------------

            Label lblTienCoc =
                new Label();

            lblTienCoc.Text =
                "Tiền cọc:";

            lblTienCoc.Location =
                new Point(680, 65);

            lblTienCoc.AutoSize =
                true;

            txtTienCoc =
                new TextBox();

            txtTienCoc.Location =
                new Point(750, 62);

            txtTienCoc.Width =
                120;

            txtTienCoc.Text =
                "0";

            // -----------------------------
            // Phòng
            // -----------------------------

            Label lblPhong =
                new Label();

            lblPhong.Text =
                "Phòng:";

            lblPhong.Location =
                new Point(20, 110);

            lblPhong.AutoSize =
                true;

            cboSoPhong =
                new ComboBox();

            cboSoPhong.Location =
                new Point(90, 107);

            cboSoPhong.Width =
                130;

            cboSoPhong.DropDownStyle =
                ComboBoxStyle.DropDownList;

            // -----------------------------
            // Số người
            // -----------------------------

            Label lblSoNguoi =
                new Label();

            lblSoNguoi.Text =
                "Số người:";

            lblSoNguoi.Location =
                new Point(240, 110);

            lblSoNguoi.AutoSize =
                true;

            nudSoNguoi =
                new NumericUpDown();

            nudSoNguoi.Location =
                new Point(320, 107);

            nudSoNguoi.Width =
                100;

            nudSoNguoi.Minimum =
                1;

            nudSoNguoi.Maximum =
                50;

            nudSoNguoi.Value =
                1;

            // -----------------------------
            // Tạo phiếu
            // -----------------------------

            btnTaoPhieu =
                new Button();

            btnTaoPhieu.Text =
                "Tạo phiếu";

            btnTaoPhieu.Location =
                new Point(450, 105);

            btnTaoPhieu.Width =
                110;

            btnTaoPhieu.Click +=
                BtnTaoPhieu_Click;

            // -----------------------------
            // Thêm phòng
            // -----------------------------

            btnThemPhong =
                new Button();

            btnThemPhong.Text =
                "Thêm phòng";

            btnThemPhong.Location =
                new Point(570, 105);

            btnThemPhong.Width =
                110;

            btnThemPhong.Click +=
                BtnThemPhong_Click;

            // -----------------------------
            // Làm mới
            // -----------------------------

            btnLamMoi =
                new Button();

            btnLamMoi.Text =
                "Làm mới";

            btnLamMoi.Location =
                new Point(690, 105);

            btnLamMoi.Width =
                100;

            btnLamMoi.Click +=
                BtnLamMoi_Click;

            // -----------------------------
            // DataGridView
            // -----------------------------

            dgvDatPhong =
                new DataGridView();

            dgvDatPhong.Location =
                new Point(20, 155);

            dgvDatPhong.Size =
                new Size(1020, 420);

            dgvDatPhong.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvDatPhong.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDatPhong.MultiSelect =
                false;

            dgvDatPhong.ReadOnly =
                true;

            // -----------------------------
            // Thêm controls
            // -----------------------------

            tab.Controls.Add(lblSoPhieu);
            tab.Controls.Add(txtSoPhieuDat);

            tab.Controls.Add(lblKhach);
            tab.Controls.Add(cboMaKhach);

            tab.Controls.Add(lblNhanVien);
            tab.Controls.Add(cboNhanVien);

            tab.Controls.Add(lblKenhDat);
            tab.Controls.Add(cboKenhDat);

            tab.Controls.Add(lblNgayLap);
            tab.Controls.Add(dtpNgayLap);

            tab.Controls.Add(lblNgayNhan);
            tab.Controls.Add(dtpNgayNhan);

            tab.Controls.Add(lblNgayTra);
            tab.Controls.Add(dtpNgayTra);

            tab.Controls.Add(lblTienCoc);
            tab.Controls.Add(txtTienCoc);

            tab.Controls.Add(lblPhong);
            tab.Controls.Add(cboSoPhong);

            tab.Controls.Add(lblSoNguoi);
            tab.Controls.Add(nudSoNguoi);

            tab.Controls.Add(btnTaoPhieu);
            tab.Controls.Add(btnThemPhong);
            tab.Controls.Add(btnLamMoi);

            tab.Controls.Add(dgvDatPhong);
        }

        // =====================================================
        // TẢI KHÁCH HÀNG
        // =====================================================

        private void TaiKhachHang()
        {
            try
            {
                DataTable table =
                    service.LayDanhSachKhachHang();

                dgvKhachHang.DataSource =
                    table;

                cboMaKhach.DataSource =
                    table.Copy();

                cboMaKhach.DisplayMember =
                    "HoTen";

                cboMaKhach.ValueMember =
                    "MaKhach";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải khách hàng: " +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // TẢI NHÂN VIÊN
        // =====================================================

        private void TaiNhanVien()
        {
            try
            {
                DataTable table =
                    service.LayDanhSachNhanVien();

                cboNhanVien.DataSource =
                    table;

                cboNhanVien.DisplayMember =
                    "HoTen";

                cboNhanVien.ValueMember =
                    "MaNV";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải nhân viên: " +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // TẢI PHÒNG
        // =====================================================

        private void TaiPhong()
        {
            try
            {
                DataTable table =
                    service.LayDanhSachPhong();

                cboSoPhong.DataSource =
                    table;

                cboSoPhong.DisplayMember =
                    "SoPhong";

                cboSoPhong.ValueMember =
                    "SoPhong";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải phòng: " +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // TẢI DANH SÁCH ĐẶT PHÒNG
        // =====================================================

        private void TaiDanhSachDatPhong()
        {
            try
            {
                DataTable table =
                    service.LayDanhSachDatPhong();

                dgvDatPhong.DataSource =
                    table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách đặt phòng: " +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // THÊM KHÁCH HÀNG
        // =====================================================

        private void BtnThemKhach_Click(
            object sender,
            EventArgs e)
        {
            string maKhach =
                txtMaKhach.Text.Trim();

            string hoTen =
                txtHoTen.Text.Trim();

            string soCMND =
                txtSoCMND.Text.Trim();

            string quocTich =
                txtQuocTich.Text.Trim();

            if (string.IsNullOrWhiteSpace(maKhach))
            {
                MessageBox.Show(
                    "Vui lòng nhập Mã khách!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtMaKhach.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show(
                    "Vui lòng nhập Họ tên!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(soCMND))
            {
                MessageBox.Show(
                    "Vui lòng nhập Số CMND!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSoCMND.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(quocTich))
            {
                MessageBox.Show(
                    "Vui lòng nhập Quốc tịch!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtQuocTich.Focus();
                return;
            }

            bool ketQua =
                service.ThemKhachHang(
                    maKhach,
                    hoTen,
                    soCMND,
                    quocTich);

            if (ketQua)
            {
                MessageBox.Show(
                    "Thêm khách hàng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiKhachHang();

                txtMaKhach.Clear();
                txtHoTen.Clear();
                txtSoCMND.Clear();
                txtQuocTich.Clear();

                txtMaKhach.Focus();
            }
            else
            {
                MessageBox.Show(
                    "Thêm khách hàng thất bại!\n" +
                    "Có thể Mã khách hoặc Số CMND đã tồn tại.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // LÀM MỚI KHÁCH HÀNG
        // =====================================================

        private void BtnLamMoiKhach_Click(
            object sender,
            EventArgs e)
        {
            txtMaKhach.Clear();
            txtHoTen.Clear();
            txtSoCMND.Clear();
            txtQuocTich.Clear();

            TaiKhachHang();

            txtMaKhach.Focus();
        }

        // =====================================================
        // TẠO PHIẾU ĐẶT PHÒNG
        // =====================================================

        private void BtnTaoPhieu_Click(
            object sender,
            EventArgs e)
        {
            string soPhieuDat =
                txtSoPhieuDat.Text.Trim();

            if (string.IsNullOrWhiteSpace(soPhieuDat))
            {
                MessageBox.Show(
                    "Vui lòng nhập Số phiếu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSoPhieuDat.Focus();
                return;
            }

            if (cboMaKhach.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn khách hàng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (cboKenhDat.SelectedItem == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn Kênh đặt!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            decimal tienCoc;

            if (!decimal.TryParse(
                txtTienCoc.Text.Trim(),
                out tienCoc))
            {
                MessageBox.Show(
                    "Tiền cọc phải là số!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTienCoc.Focus();
                return;
            }

            if (tienCoc < 0)
            {
                MessageBox.Show(
                    "Tiền cọc không được âm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (dtpNgayTra.Value.Date <
                dtpNgayNhan.Value.Date)
            {
                MessageBox.Show(
                    "Ngày trả dự kiến phải lớn hơn hoặc bằng ngày nhận!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string maKhach =
                cboMaKhach.SelectedValue.ToString();

            string maNV =
                cboNhanVien.SelectedValue.ToString();

            string kenhDat =
                cboKenhDat.SelectedItem.ToString();

            bool ketQua =
                service.ThemPhieuDatPhong(
                    soPhieuDat,
                    maKhach,
                    maNV,
                    dtpNgayLap.Value,
                    dtpNgayNhan.Value,
                    dtpNgayTra.Value,
                    tienCoc,
                    kenhDat);

            if (ketQua)
            {
                MessageBox.Show(
                    "Tạo phiếu đặt phòng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDanhSachDatPhong();
            }
            else
            {
                MessageBox.Show(
                    "Tạo phiếu đặt phòng thất bại!\n" +
                    "Kiểm tra dữ liệu hoặc Số phiếu đã tồn tại.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // THÊM PHÒNG VÀO PHIẾU
        // =====================================================

        private void BtnThemPhong_Click(
            object sender,
            EventArgs e)
        {
            string soPhieuDat =
                txtSoPhieuDat.Text.Trim();

            if (string.IsNullOrWhiteSpace(soPhieuDat))
            {
                MessageBox.Show(
                    "Vui lòng nhập Số phiếu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSoPhieuDat.Focus();
                return;
            }

            if (cboSoPhong.SelectedValue == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn phòng!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int soNguoi =
                Convert.ToInt32(
                    nudSoNguoi.Value);

            string soPhong =
                cboSoPhong.SelectedValue.ToString();

            bool ketQua =
                service.ThemChiTietDatPhong(
                    soPhieuDat,
                    soPhong,
                    soNguoi);

            if (ketQua)
            {
                MessageBox.Show(
                    "Thêm phòng vào phiếu thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDanhSachDatPhong();
            }
            else
            {
                MessageBox.Show(
                    "Thêm phòng thất bại!\n" +
                    "Kiểm tra Số phiếu hoặc phòng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // LÀM MỚI ĐẶT PHÒNG
        // =====================================================

        private void BtnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            txtSoPhieuDat.Clear();

            txtTienCoc.Text =
                "0";

            dtpNgayLap.Value =
                DateTime.Today;

            dtpNgayNhan.Value =
                DateTime.Today;

            dtpNgayTra.Value =
                DateTime.Today.AddDays(1);

            nudSoNguoi.Value =
                1;

            if (cboKenhDat.Items.Count > 0)
            {
                cboKenhDat.SelectedIndex =
                    2;
            }

            TaiKhachHang();
            TaiNhanVien();
            TaiPhong();
            TaiDanhSachDatPhong();

            txtSoPhieuDat.Focus();
        }
    }
}