using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmTraPhong : Form
    {
        private readonly TraPhongService service =
            new TraPhongService();

        private DataGridView dgvPhieuDat = null;
        private DataGridView dgvPhong = null;
        private DataGridView dgvHoaDon = null;
        private DataGridView dgvThanhToan = null;

        private TextBox txtSoPhieuDat = null;
        private TextBox txtSoHoaDon = null;
        private TextBox txtMaNV = null;
        private TextBox txtSoNgay = null;
        private TextBox txtTienPhong = null;
        private TextBox txtTienDichVu = null;
        private TextBox txtTongTien = null;

        private TextBox txtMaThanhToan = null;
        private TextBox txtSoTienThanhToan = null;

        private ComboBox cboHinhThuc = null;

        private DateTimePicker dtpNgayLap = null;
        private DateTimePicker dtpNgayThanhToan = null;

        private Button btnTinhTien = null;
        private Button btnTaoHoaDon = null;
        private Button btnThanhToan = null;
        private Button btnTraPhong = null;
        private Button btnLamMoi = null;

        public FrmTraPhong()
        {
            InitializeComponent();

            TaoGiaoDien();

            this.Load += FrmTraPhong_Load;
        }

        // =====================================================
        // FORM LOAD
        // =====================================================

        private void FrmTraPhong_Load(object sender, EventArgs e)
        {
            TaiDanhSachPhieuDat();
            TaiDanhSachHoaDon();
            TaiDanhSachThanhToan();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================

        private void TaoGiaoDien()
        {
            this.Controls.Clear();

            this.Text = "Trả phòng / Thanh toán";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1150, 750);
            this.MinimumSize = new Size(1050, 650);
            this.BackColor = Color.White;

            Label lblTieuDe = new Label();

            lblTieuDe.Text = "TRẢ PHÒNG / THANH TOÁN";
            lblTieuDe.Font =
                new Font("Arial", 20, FontStyle.Bold);

            lblTieuDe.AutoSize = false;
            lblTieuDe.TextAlign =
                ContentAlignment.MiddleCenter;

            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 60;

            this.Controls.Add(lblTieuDe);

            TabControl tabControl = new TabControl();

            tabControl.Left = 20;
            tabControl.Top = 70;
            tabControl.Width = 1090;
            tabControl.Height = 620;

            // =================================================
            // TAB 1 - TRẢ PHÒNG
            // =================================================

            TabPage tabTraPhong =
                new TabPage("Trả phòng");

            Label lblPhieuDat = new Label();

            lblPhieuDat.Text = "Số phiếu đặt:";
            lblPhieuDat.Left = 20;
            lblPhieuDat.Top = 25;
            lblPhieuDat.Width = 110;

            txtSoPhieuDat = new TextBox();

            txtSoPhieuDat.Left = 135;
            txtSoPhieuDat.Top = 20;
            txtSoPhieuDat.Width = 180;

            btnTinhTien =
                TaoButton("Tính tiền", 330, 18, 110);

            btnTraPhong =
                TaoButton("Trả phòng", 450, 18, 110);

            btnLamMoi =
                TaoButton("Làm mới", 570, 18, 100);

            btnTinhTien.Click += BtnTinhTien_Click;
            btnTraPhong.Click += BtnTraPhong_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            dgvPhieuDat = new DataGridView();

            dgvPhieuDat.Left = 20;
            dgvPhieuDat.Top = 70;
            dgvPhieuDat.Width = 1030;
            dgvPhieuDat.Height = 190;

            CauHinhGrid(dgvPhieuDat);

            dgvPhieuDat.CellClick +=
                DgvPhieuDat_CellClick;

            Label lblTienPhong = new Label();

            lblTienPhong.Text = "Tiền phòng:";
            lblTienPhong.Left = 20;
            lblTienPhong.Top = 285;
            lblTienPhong.Width = 100;

            txtTienPhong = new TextBox();

            txtTienPhong.Left = 130;
            txtTienPhong.Top = 280;
            txtTienPhong.Width = 180;
            txtTienPhong.ReadOnly = true;

            Label lblTienDichVu = new Label();

            lblTienDichVu.Text = "Tiền dịch vụ:";
            lblTienDichVu.Left = 330;
            lblTienDichVu.Top = 285;
            lblTienDichVu.Width = 100;

            txtTienDichVu = new TextBox();

            txtTienDichVu.Left = 440;
            txtTienDichVu.Top = 280;
            txtTienDichVu.Width = 180;
            txtTienDichVu.ReadOnly = true;

            Label lblTongTien = new Label();

            lblTongTien.Text = "Tổng tiền:";
            lblTongTien.Left = 640;
            lblTongTien.Top = 285;
            lblTongTien.Width = 90;

            txtTongTien = new TextBox();

            txtTongTien.Left = 740;
            txtTongTien.Top = 280;
            txtTongTien.Width = 200;
            txtTongTien.ReadOnly = true;

            Label lblSoNgay = new Label();

            lblSoNgay.Text = "Số ngày:";
            lblSoNgay.Left = 20;
            lblSoNgay.Top = 330;
            lblSoNgay.Width = 100;

            txtSoNgay = new TextBox();

            txtSoNgay.Left = 130;
            txtSoNgay.Top = 325;
            txtSoNgay.Width = 180;

            dgvPhong = new DataGridView();

            dgvPhong.Left = 20;
            dgvPhong.Top = 375;
            dgvPhong.Width = 1030;
            dgvPhong.Height = 160;

            CauHinhGrid(dgvPhong);

            tabTraPhong.Controls.Add(lblPhieuDat);
            tabTraPhong.Controls.Add(txtSoPhieuDat);

            tabTraPhong.Controls.Add(btnTinhTien);
            tabTraPhong.Controls.Add(btnTraPhong);
            tabTraPhong.Controls.Add(btnLamMoi);

            tabTraPhong.Controls.Add(dgvPhieuDat);

            tabTraPhong.Controls.Add(lblTienPhong);
            tabTraPhong.Controls.Add(txtTienPhong);

            tabTraPhong.Controls.Add(lblTienDichVu);
            tabTraPhong.Controls.Add(txtTienDichVu);

            tabTraPhong.Controls.Add(lblTongTien);
            tabTraPhong.Controls.Add(txtTongTien);

            tabTraPhong.Controls.Add(lblSoNgay);
            tabTraPhong.Controls.Add(txtSoNgay);

            tabTraPhong.Controls.Add(dgvPhong);

            // =================================================
            // TAB 2 - HÓA ĐƠN
            // =================================================

            TabPage tabHoaDon =
                new TabPage("Hóa đơn");

            Label lblSoHoaDon = new Label();

            lblSoHoaDon.Text = "Số hóa đơn:";
            lblSoHoaDon.Left = 20;
            lblSoHoaDon.Top = 25;
            lblSoHoaDon.Width = 100;

            txtSoHoaDon = new TextBox();

            txtSoHoaDon.Left = 130;
            txtSoHoaDon.Top = 20;
            txtSoHoaDon.Width = 180;

            Label lblMaNV = new Label();

            lblMaNV.Text = "Mã nhân viên:";
            lblMaNV.Left = 330;
            lblMaNV.Top = 25;
            lblMaNV.Width = 100;

            txtMaNV = new TextBox();

            txtMaNV.Left = 440;
            txtMaNV.Top = 20;
            txtMaNV.Width = 150;

            Label lblNgayLap = new Label();

            lblNgayLap.Text = "Ngày lập:";
            lblNgayLap.Left = 610;
            lblNgayLap.Top = 25;
            lblNgayLap.Width = 80;

            dtpNgayLap = new DateTimePicker();

            dtpNgayLap.Left = 700;
            dtpNgayLap.Top = 20;
            dtpNgayLap.Width = 170;

            btnTaoHoaDon =
                TaoButton("Tạo hóa đơn", 20, 65, 120);

            btnTaoHoaDon.Click +=
                BtnTaoHoaDon_Click;

            dgvHoaDon = new DataGridView();

            dgvHoaDon.Left = 20;
            dgvHoaDon.Top = 115;
            dgvHoaDon.Width = 1030;
            dgvHoaDon.Height = 400;

            CauHinhGrid(dgvHoaDon);

            dgvHoaDon.CellClick +=
                DgvHoaDon_CellClick;

            tabHoaDon.Controls.Add(lblSoHoaDon);
            tabHoaDon.Controls.Add(txtSoHoaDon);

            tabHoaDon.Controls.Add(lblMaNV);
            tabHoaDon.Controls.Add(txtMaNV);

            tabHoaDon.Controls.Add(lblNgayLap);
            tabHoaDon.Controls.Add(dtpNgayLap);

            tabHoaDon.Controls.Add(btnTaoHoaDon);
            tabHoaDon.Controls.Add(dgvHoaDon);

            // =================================================
            // TAB 3 - THANH TOÁN
            // =================================================

            TabPage tabThanhToan =
                new TabPage("Thanh toán");

            Label lblMaThanhToan = new Label();

            lblMaThanhToan.Text = "Mã thanh toán:";
            lblMaThanhToan.Left = 20;
            lblMaThanhToan.Top = 25;
            lblMaThanhToan.Width = 110;

            txtMaThanhToan = new TextBox();

            txtMaThanhToan.Left = 140;
            txtMaThanhToan.Top = 20;
            txtMaThanhToan.Width = 180;

            Label lblHoaDonTT = new Label();

            lblHoaDonTT.Text = "Số hóa đơn:";
            lblHoaDonTT.Left = 340;
            lblHoaDonTT.Top = 25;
            lblHoaDonTT.Width = 100;

            TextBox txtHoaDonTT =
                new TextBox();

            txtHoaDonTT.Name = "txtHoaDonTT";

            txtHoaDonTT.Left = 450;
            txtHoaDonTT.Top = 20;
            txtHoaDonTT.Width = 170;

            Label lblHinhThuc = new Label();

            lblHinhThuc.Text = "Hình thức:";
            lblHinhThuc.Left = 640;
            lblHinhThuc.Top = 25;
            lblHinhThuc.Width = 80;

            cboHinhThuc = new ComboBox();

            cboHinhThuc.Left = 730;
            cboHinhThuc.Top = 20;
            cboHinhThuc.Width = 180;

            cboHinhThuc.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cboHinhThuc.Items.Add("Tiền mặt");
            cboHinhThuc.Items.Add("Chuyển khoản");
            cboHinhThuc.Items.Add("Thẻ");
            cboHinhThuc.Items.Add("Ví điện tử");

            cboHinhThuc.SelectedIndex = 0;

            Label lblSoTienTT = new Label();

            lblSoTienTT.Text = "Số tiền:";
            lblSoTienTT.Left = 20;
            lblSoTienTT.Top = 75;
            lblSoTienTT.Width = 110;

            txtSoTienThanhToan =
                new TextBox();

            txtSoTienThanhToan.Left = 140;
            txtSoTienThanhToan.Top = 70;
            txtSoTienThanhToan.Width = 180;

            Label lblNgayTT = new Label();

            lblNgayTT.Text = "Ngày thanh toán:";
            lblNgayTT.Left = 340;
            lblNgayTT.Top = 75;
            lblNgayTT.Width = 110;

            dtpNgayThanhToan =
                new DateTimePicker();

            dtpNgayThanhToan.Left = 460;
            dtpNgayThanhToan.Top = 70;
            dtpNgayThanhToan.Width = 180;

            btnThanhToan =
                TaoButton("Thanh toán", 660, 68, 120);

            btnThanhToan.Click +=
                BtnThanhToan_Click;

            dgvThanhToan =
                new DataGridView();

            dgvThanhToan.Left = 20;
            dgvThanhToan.Top = 125;
            dgvThanhToan.Width = 1030;
            dgvThanhToan.Height = 390;

            CauHinhGrid(dgvThanhToan);

            tabThanhToan.Controls.Add(lblMaThanhToan);
            tabThanhToan.Controls.Add(txtMaThanhToan);

            tabThanhToan.Controls.Add(lblHoaDonTT);
            tabThanhToan.Controls.Add(txtHoaDonTT);

            tabThanhToan.Controls.Add(lblHinhThuc);
            tabThanhToan.Controls.Add(cboHinhThuc);

            tabThanhToan.Controls.Add(lblSoTienTT);
            tabThanhToan.Controls.Add(txtSoTienThanhToan);

            tabThanhToan.Controls.Add(lblNgayTT);
            tabThanhToan.Controls.Add(dtpNgayThanhToan);

            tabThanhToan.Controls.Add(btnThanhToan);
            tabThanhToan.Controls.Add(dgvThanhToan);

            // =================================================
            // THÊM TAB
            // =================================================

            tabControl.TabPages.Add(tabTraPhong);
            tabControl.TabPages.Add(tabHoaDon);
            tabControl.TabPages.Add(tabThanhToan);

            this.Controls.Add(tabControl);
        }

        // =====================================================
        // CẤU HÌNH DATAGRIDVIEW
        // =====================================================

        private void CauHinhGrid(DataGridView grid)
        {
            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.ReadOnly = true;

            grid.AllowUserToAddRows = false;

            grid.MultiSelect = false;
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

            button.Font =
                new Font("Arial", 9, FontStyle.Bold);

            button.Cursor = Cursors.Hand;

            return button;
        }

        // =====================================================
        // TẢI DANH SÁCH PHIẾU ĐẶT
        // =====================================================

        private void TaiDanhSachPhieuDat()
        {
            try
            {
                dgvPhieuDat.DataSource =
                    service.LayDanhSachPhieuDat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách phiếu đặt.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CLICK PHIẾU ĐẶT
        // =====================================================

        private void DgvPhieuDat_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvPhieuDat.Rows[e.RowIndex];

            if (row.Cells["SoPhieuDat"].Value != null)
            {
                txtSoPhieuDat.Text =
                    row.Cells["SoPhieuDat"]
                        .Value
                        .ToString();
            }

            TaiDanhSachPhong();
        }

        // =====================================================
        // TẢI PHÒNG
        // =====================================================

        private void TaiDanhSachPhong()
        {
            if (string.IsNullOrWhiteSpace(
                txtSoPhieuDat.Text))
                return;

            try
            {
                dgvPhong.DataSource =
                    service.LayChiTietPhong(
                        txtSoPhieuDat.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải thông tin phòng.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // TÍNH TIỀN
        // =====================================================

        private void BtnTinhTien_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtSoPhieuDat.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn phiếu đặt phòng.");
                return;
            }

            DataTable table =
                service.LayChiTietPhong(
                    txtSoPhieuDat.Text.Trim());

            if (table.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Phiếu đặt chưa có phòng.");
                return;
            }

            decimal tienPhong = 0;

            foreach (DataRow row in table.Rows)
            {
                decimal donGia =
                    Convert.ToDecimal(
                        row["DonGiaNgay"]);

                tienPhong += donGia;
            }

            decimal tienDichVu =
                service.LayTienDichVu(
                    txtSoPhieuDat.Text.Trim());

            int soNgay = 1;

            if (dgvPhieuDat.CurrentRow != null)
            {
                DateTime ngayNhan =
                    Convert.ToDateTime(
                        dgvPhieuDat.CurrentRow
                            .Cells["NgayNhan"]
                            .Value);

                DateTime ngayTra =
                    Convert.ToDateTime(
                        dgvPhieuDat.CurrentRow
                            .Cells["NgayTraDuKien"]
                            .Value);

                soNgay =
                    Math.Max(
                        1,
                        (ngayTra.Date -
                         ngayNhan.Date).Days);
            }

            tienPhong *= soNgay;

            decimal tongTien =
                tienPhong + tienDichVu;

            txtSoNgay.Text =
                soNgay.ToString();

            txtTienPhong.Text =
                tienPhong.ToString("N0");

            txtTienDichVu.Text =
                tienDichVu.ToString("N0");

            txtTongTien.Text =
                tongTien.ToString("N0");
        }

        // =====================================================
        // TẠO HÓA ĐƠN
        // =====================================================

        private void BtnTaoHoaDon_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtSoHoaDon.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập số hóa đơn.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtSoPhieuDat.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn phiếu đặt.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtMaNV.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã nhân viên.");
                return;
            }

            if (!int.TryParse(
                txtSoNgay.Text,
                out int soNgay))
            {
                MessageBox.Show(
                    "Số ngày không hợp lệ.");
                return;
            }

            if (!decimal.TryParse(
                txtTienPhong.Text
                    .Replace(",", ""),
                out decimal tienPhong))
            {
                MessageBox.Show(
                    "Tiền phòng không hợp lệ.");
                return;
            }

            if (!decimal.TryParse(
                txtTienDichVu.Text
                    .Replace(",", ""),
                out decimal tienDichVu))
            {
                MessageBox.Show(
                    "Tiền dịch vụ không hợp lệ.");
                return;
            }

            bool result =
                service.ThemHoaDon(
                    txtSoHoaDon.Text.Trim(),
                    txtSoPhieuDat.Text.Trim(),
                    dtpNgayLap.Value,
                    txtMaNV.Text.Trim(),
                    soNgay,
                    tienPhong,
                    tienDichVu);

            if (result)
            {
                MessageBox.Show(
                    "Tạo hóa đơn thành công.");

                TaiDanhSachHoaDon();
            }
            else
            {
                MessageBox.Show(
                    "Không thể tạo hóa đơn. " +
                    "Kiểm tra mã hóa đơn, phiếu đặt " +
                    "hoặc nhân viên.");
            }
        }

        // =====================================================
        // TẢI HÓA ĐƠN
        // =====================================================

        private void TaiDanhSachHoaDon()
        {
            try
            {
                dgvHoaDon.DataSource =
                    service.LayDanhSachHoaDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách hóa đơn.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CLICK HÓA ĐƠN
        // =====================================================

        private void DgvHoaDon_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvHoaDon.Rows[e.RowIndex];

            string soHoaDon =
                row.Cells["SoHoaDon"]
                    .Value?
                    .ToString() ?? "";

            if (string.IsNullOrWhiteSpace(
                soHoaDon))
                return;

            TextBox txtHoaDonTT =
                this.Controls.Find(
                    "txtHoaDonTT",
                    true).Length > 0
                ? this.Controls.Find(
                    "txtHoaDonTT",
                    true)[0]
                    as TextBox
                : null;

            if (txtHoaDonTT != null)
            {
                txtHoaDonTT.Text =
                    soHoaDon;
            }

            if (row.Cells["TongTien"].Value != null)
            {
                txtSoTienThanhToan.Text =
                    Convert.ToDecimal(
                        row.Cells["TongTien"].Value)
                    .ToString("N0");
            }
        }

        // =====================================================
        // TẢI THANH TOÁN
        // =====================================================

        private void TaiDanhSachThanhToan()
        {
            try
            {
                dgvThanhToan.DataSource =
                    service.LayDanhSachThanhToan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách thanh toán.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // THANH TOÁN
        // =====================================================

        private void BtnThanhToan_Click(
            object sender,
            EventArgs e)
        {
            TextBox txtHoaDonTT =
                this.Controls.Find(
                    "txtHoaDonTT",
                    true).Length > 0
                ? this.Controls.Find(
                    "txtHoaDonTT",
                    true)[0]
                    as TextBox
                : null;

            if (txtHoaDonTT == null ||
                string.IsNullOrWhiteSpace(
                    txtHoaDonTT.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn hoặc nhập số hóa đơn.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtMaThanhToan.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã thanh toán.");
                return;
            }

            if (!decimal.TryParse(
                txtSoTienThanhToan.Text
                    .Replace(",", ""),
                out decimal soTien))
            {
                MessageBox.Show(
                    "Số tiền không hợp lệ.");
                return;
            }

            bool result =
                service.ThemThanhToan(
                    txtMaThanhToan.Text.Trim(),
                    txtHoaDonTT.Text.Trim(),
                    dtpNgayThanhToan.Value,
                    cboHinhThuc.Text,
                    soTien);

            if (result)
            {
                MessageBox.Show(
                    "Thanh toán thành công.");

                TaiDanhSachThanhToan();
                TaiDanhSachHoaDon();

                txtMaThanhToan.Clear();
                txtSoTienThanhToan.Clear();
            }
            else
            {
                MessageBox.Show(
                    "Không thể thực hiện thanh toán.");
            }
        }

        // =====================================================
        // TRẢ PHÒNG
        // =====================================================

        private void BtnTraPhong_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtSoPhieuDat.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn phiếu đặt phòng.");
                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "Bạn có chắc muốn xác nhận trả phòng?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool success =
                service.TraPhong(
                    txtSoPhieuDat.Text.Trim(),
                    DateTime.Now);

            if (success)
            {
                MessageBox.Show(
                    "Trả phòng thành công.");

                TaiDanhSachPhieuDat();
            }
            else
            {
                MessageBox.Show(
                    "Không thể cập nhật trạng thái trả phòng.");
            }
        }

        // =====================================================
        // LÀM MỚI
        // =====================================================

        private void BtnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            txtSoPhieuDat.Clear();

            txtSoNgay.Clear();
            txtTienPhong.Clear();
            txtTienDichVu.Clear();
            txtTongTien.Clear();

            txtSoHoaDon.Clear();
            txtMaNV.Clear();

            txtMaThanhToan.Clear();
            txtSoTienThanhToan.Clear();

            dtpNgayLap.Value =
                DateTime.Now;

            dtpNgayThanhToan.Value =
                DateTime.Now;

            TaiDanhSachPhieuDat();
            TaiDanhSachHoaDon();
            TaiDanhSachThanhToan();

            dgvPhong.DataSource = null;
        }
    }
}