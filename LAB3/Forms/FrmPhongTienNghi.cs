using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmPhongTienNghi : Form
    {
        private readonly PhongTienNghiService service =
            new PhongTienNghiService();

        // =====================================================
        // PHÒNG
        // =====================================================
        private TextBox txtSoPhong;
        private ComboBox cboMaKhuVuc;
        private TextBox txtSoNguoiToiDa;
        private TextBox txtDonGiaNgay;

        private Button btnThemPhong;
        private Button btnSuaPhong;
        private Button btnXoaPhong;
        private Button btnLamMoiPhong;

        private DataGridView dgvPhong;

        // =====================================================
        // TIỆN NGHI
        // =====================================================
        private TextBox txtMaTienNghi;
        private ComboBox cboMaLoaiTN;
        private TextBox txtSoThuTu;
        private DateTimePicker dtpNgayMua;
        private TextBox txtTrangThai;

        private Button btnThemTienNghi;
        private Button btnSuaTienNghi;
        private Button btnXoaTienNghi;
        private Button btnLamMoiTienNghi;

        private DataGridView dgvTienNghi;

        public FrmPhongTienNghi()
        {
            InitializeComponent();

            TaoGiaoDien();

            this.Load += FrmPhongTienNghi_Load;
        }

        // =====================================================
        // FORM LOAD
        // =====================================================
        private void FrmPhongTienNghi_Load(object sender, EventArgs e)
        {
            TaiDanhSachPhong();
            TaiDanhSachTienNghi();
            TaiKhuVuc();
            TaiLoaiTienNghi();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================
        private void TaoGiaoDien()
        {
            this.Controls.Clear();

            this.Text = "Quản lý phòng & tiện nghi";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1050, 700);
            this.BackColor = Color.White;

            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            TabPage tabPhong = new TabPage("Phòng");
            TabPage tabTienNghi = new TabPage("Tiện nghi");

            TaoTabPhong(tabPhong);
            TaoTabTienNghi(tabTienNghi);

            tabControl.TabPages.Add(tabPhong);
            tabControl.TabPages.Add(tabTienNghi);

            this.Controls.Add(tabControl);
        }

        // =====================================================
        // TAB PHÒNG
        // =====================================================
        private void TaoTabPhong(TabPage tab)
        {
            Label lblSoPhong = new Label();
            lblSoPhong.Text = "Số phòng:";
            lblSoPhong.Location = new Point(20, 20);
            lblSoPhong.AutoSize = true;

            txtSoPhong = new TextBox();
            txtSoPhong.Location = new Point(90, 17);
            txtSoPhong.Width = 120;

            Label lblKhuVuc = new Label();
            lblKhuVuc.Text = "Khu vực:";
            lblKhuVuc.Location = new Point(230, 20);
            lblKhuVuc.AutoSize = true;

            cboMaKhuVuc = new ComboBox();
            cboMaKhuVuc.Location = new Point(290, 17);
            cboMaKhuVuc.Width = 150;
            cboMaKhuVuc.DropDownStyle = ComboBoxStyle.DropDownList;

            Label lblNguoi = new Label();
            lblNguoi.Text = "Số người tối đa:";
            lblNguoi.Location = new Point(460, 20);
            lblNguoi.AutoSize = true;

            txtSoNguoiToiDa = new TextBox();
            txtSoNguoiToiDa.Location = new Point(560, 17);
            txtSoNguoiToiDa.Width = 100;

            Label lblGia = new Label();
            lblGia.Text = "Đơn giá/ngày:";
            lblGia.Location = new Point(680, 20);
            lblGia.AutoSize = true;

            txtDonGiaNgay = new TextBox();
            txtDonGiaNgay.Location = new Point(770, 17);
            txtDonGiaNgay.Width = 120;

            // -----------------------------
            // BUTTON
            // -----------------------------
            btnThemPhong = new Button();
            btnThemPhong.Text = "Thêm";
            btnThemPhong.Location = new Point(20, 55);
            btnThemPhong.Click += BtnThemPhong_Click;

            btnSuaPhong = new Button();
            btnSuaPhong.Text = "Sửa";
            btnSuaPhong.Location = new Point(100, 55);
            btnSuaPhong.Click += BtnSuaPhong_Click;

            btnXoaPhong = new Button();
            btnXoaPhong.Text = "Xóa";
            btnXoaPhong.Location = new Point(180, 55);
            btnXoaPhong.Click += BtnXoaPhong_Click;

            btnLamMoiPhong = new Button();
            btnLamMoiPhong.Text = "Làm mới";
            btnLamMoiPhong.Location = new Point(260, 55);
            btnLamMoiPhong.Click += BtnLamMoiPhong_Click;

            // -----------------------------
            // DATAGRIDVIEW
            // -----------------------------
            dgvPhong = new DataGridView();
            dgvPhong.Location = new Point(20, 100);
            dgvPhong.Size = new Size(960, 480);

            dgvPhong.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvPhong.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPhong.MultiSelect = false;
            dgvPhong.ReadOnly = true;

            dgvPhong.CellClick += DgvPhong_CellClick;

            // -----------------------------
            // ADD CONTROL
            // -----------------------------
            tab.Controls.Add(lblSoPhong);
            tab.Controls.Add(txtSoPhong);

            tab.Controls.Add(lblKhuVuc);
            tab.Controls.Add(cboMaKhuVuc);

            tab.Controls.Add(lblNguoi);
            tab.Controls.Add(txtSoNguoiToiDa);

            tab.Controls.Add(lblGia);
            tab.Controls.Add(txtDonGiaNgay);

            tab.Controls.Add(btnThemPhong);
            tab.Controls.Add(btnSuaPhong);
            tab.Controls.Add(btnXoaPhong);
            tab.Controls.Add(btnLamMoiPhong);

            tab.Controls.Add(dgvPhong);
        }

        // =====================================================
        // TAB TIỆN NGHI
        // =====================================================
        private void TaoTabTienNghi(TabPage tab)
        {
            Label lblMa = new Label();
            lblMa.Text = "Mã tiện nghi:";
            lblMa.Location = new Point(20, 20);
            lblMa.AutoSize = true;

            txtMaTienNghi = new TextBox();
            txtMaTienNghi.Location = new Point(110, 17);
            txtMaTienNghi.Width = 120;

            Label lblLoai = new Label();
            lblLoai.Text = "Loại tiện nghi:";
            lblLoai.Location = new Point(250, 20);
            lblLoai.AutoSize = true;

            cboMaLoaiTN = new ComboBox();
            cboMaLoaiTN.Location = new Point(340, 17);
            cboMaLoaiTN.Width = 150;
            cboMaLoaiTN.DropDownStyle = ComboBoxStyle.DropDownList;

            Label lblSTT = new Label();
            lblSTT.Text = "Số thứ tự:";
            lblSTT.Location = new Point(510, 20);
            lblSTT.AutoSize = true;

            txtSoThuTu = new TextBox();
            txtSoThuTu.Location = new Point(580, 17);
            txtSoThuTu.Width = 80;

            Label lblNgay = new Label();
            lblNgay.Text = "Ngày mua:";
            lblNgay.Location = new Point(680, 20);
            lblNgay.AutoSize = true;

            dtpNgayMua = new DateTimePicker();
            dtpNgayMua.Location = new Point(750, 17);
            dtpNgayMua.Width = 150;
            dtpNgayMua.Format = DateTimePickerFormat.Short;

            Label lblTrangThai = new Label();
            lblTrangThai.Text = "Trạng thái:";
            lblTrangThai.Location = new Point(20, 60);
            lblTrangThai.AutoSize = true;

            txtTrangThai = new TextBox();
            txtTrangThai.Location = new Point(100, 57);
            txtTrangThai.Width = 160;

            // -----------------------------
            // BUTTON
            // -----------------------------
            btnThemTienNghi = new Button();
            btnThemTienNghi.Text = "Thêm";
            btnThemTienNghi.Location = new Point(290, 55);
            btnThemTienNghi.Click += BtnThemTienNghi_Click;

            btnSuaTienNghi = new Button();
            btnSuaTienNghi.Text = "Sửa";
            btnSuaTienNghi.Location = new Point(370, 55);
            btnSuaTienNghi.Click += BtnSuaTienNghi_Click;

            btnXoaTienNghi = new Button();
            btnXoaTienNghi.Text = "Xóa";
            btnXoaTienNghi.Location = new Point(450, 55);
            btnXoaTienNghi.Click += BtnXoaTienNghi_Click;

            btnLamMoiTienNghi = new Button();
            btnLamMoiTienNghi.Text = "Làm mới";
            btnLamMoiTienNghi.Location = new Point(530, 55);
            btnLamMoiTienNghi.Click += BtnLamMoiTienNghi_Click;

            // -----------------------------
            // DATAGRIDVIEW
            // -----------------------------
            dgvTienNghi = new DataGridView();
            dgvTienNghi.Location = new Point(20, 100);
            dgvTienNghi.Size = new Size(960, 480);

            dgvTienNghi.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvTienNghi.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvTienNghi.MultiSelect = false;
            dgvTienNghi.ReadOnly = true;

            dgvTienNghi.CellClick += DgvTienNghi_CellClick;

            // -----------------------------
            // ADD CONTROL
            // -----------------------------
            tab.Controls.Add(lblMa);
            tab.Controls.Add(txtMaTienNghi);

            tab.Controls.Add(lblLoai);
            tab.Controls.Add(cboMaLoaiTN);

            tab.Controls.Add(lblSTT);
            tab.Controls.Add(txtSoThuTu);

            tab.Controls.Add(lblNgay);
            tab.Controls.Add(dtpNgayMua);

            tab.Controls.Add(lblTrangThai);
            tab.Controls.Add(txtTrangThai);

            tab.Controls.Add(btnThemTienNghi);
            tab.Controls.Add(btnSuaTienNghi);
            tab.Controls.Add(btnXoaTienNghi);
            tab.Controls.Add(btnLamMoiTienNghi);

            tab.Controls.Add(dgvTienNghi);
        }

        // =====================================================
        // LOAD KHU VỰC
        // =====================================================
        private void TaiKhuVuc()
        {
            try
            {
                KhuVucService serviceKhuVuc =
                    new KhuVucService();

                DataTable table =
                    serviceKhuVuc.LayDanhSach();

                cboMaKhuVuc.DataSource = table;
                cboMaKhuVuc.DisplayMember = "TenKhuVuc";
                cboMaKhuVuc.ValueMember = "MaKhuVuc";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải khu vực: " + ex.Message);
            }
        }

        // =====================================================
        // LOAD LOẠI TIỆN NGHI
        // =====================================================
        private void TaiLoaiTienNghi()
        {
            try
            {
                using (System.Data.SqlClient.SqlConnection conn =
                    QuanLyKhachSan.Data.Db.GetConnection())
                {
                    string sql = @"
                        SELECT
                            MaLoaiTN,
                            TenLoaiTN
                        FROM LoaiTienNghi
                        ORDER BY MaLoaiTN";

                    using (System.Data.SqlClient.SqlCommand cmd =
                        new System.Data.SqlClient.SqlCommand(sql, conn))
                    {
                        using (System.Data.SqlClient.SqlDataAdapter adapter =
                            new System.Data.SqlClient.SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            cboMaLoaiTN.DataSource = table;
                            cboMaLoaiTN.DisplayMember = "TenLoaiTN";
                            cboMaLoaiTN.ValueMember = "MaLoaiTN";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải loại tiện nghi: " + ex.Message);
            }
        }

        // =====================================================
        // LOAD PHÒNG
        // =====================================================
        private void TaiDanhSachPhong()
        {
            try
            {
                dgvPhong.DataSource =
                    service.LayDanhSachPhong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách phòng: " +
                    ex.Message);
            }
        }

        // =====================================================
        // LOAD TIỆN NGHI
        // =====================================================
        private void TaiDanhSachTienNghi()
        {
            try
            {
                dgvTienNghi.DataSource =
                    service.LayDanhSachTienNghi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải danh sách tiện nghi: " +
                    ex.Message);
            }
        }

        // =====================================================
        // THÊM PHÒNG
        // =====================================================
        private void BtnThemPhong_Click(
            object sender,
            EventArgs e)
        {
            int soNguoi;

            decimal donGia;

            if (string.IsNullOrWhiteSpace(txtSoPhong.Text) ||
                cboMaKhuVuc.SelectedValue == null ||
                !int.TryParse(
                    txtSoNguoiToiDa.Text,
                    out soNguoi) ||
                !decimal.TryParse(
                    txtDonGiaNgay.Text,
                    out donGia))
            {
                MessageBox.Show(
                    "Vui lòng nhập đúng thông tin phòng!");

                return;
            }

            if (soNguoi <= 0 || donGia < 0)
            {
                MessageBox.Show(
                    "Số người phải > 0 và đơn giá phải >= 0!");

                return;
            }

            bool ketQua = service.ThemPhong(
                txtSoPhong.Text.Trim(),
                cboMaKhuVuc.SelectedValue.ToString(),
                soNguoi,
                donGia);

            if (ketQua)
            {
                MessageBox.Show(
                    "Thêm phòng thành công!");

                TaiDanhSachPhong();
                XoaTrangPhong();
            }
            else
            {
                MessageBox.Show(
                    "Thêm phòng thất bại!");
            }
        }

        // =====================================================
        // SỬA PHÒNG
        // =====================================================
        private void BtnSuaPhong_Click(
            object sender,
            EventArgs e)
        {
            int soNguoi;

            decimal donGia;

            if (string.IsNullOrWhiteSpace(txtSoPhong.Text) ||
                cboMaKhuVuc.SelectedValue == null ||
                !int.TryParse(
                    txtSoNguoiToiDa.Text,
                    out soNguoi) ||
                !decimal.TryParse(
                    txtDonGiaNgay.Text,
                    out donGia))
            {
                MessageBox.Show(
                    "Vui lòng nhập đúng thông tin phòng!");

                return;
            }

            bool ketQua = service.SuaPhong(
                txtSoPhong.Text.Trim(),
                cboMaKhuVuc.SelectedValue.ToString(),
                soNguoi,
                donGia);

            if (ketQua)
            {
                MessageBox.Show(
                    "Sửa phòng thành công!");

                TaiDanhSachPhong();
                XoaTrangPhong();
            }
            else
            {
                MessageBox.Show(
                    "Sửa phòng thất bại!");
            }
        }

        // =====================================================
        // XÓA PHÒNG
        // =====================================================
        private void BtnXoaPhong_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoPhong.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn phòng cần xóa!");

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa phòng này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool ketQua =
                service.XoaPhong(
                    txtSoPhong.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Xóa phòng thành công!");

                TaiDanhSachPhong();
                XoaTrangPhong();
            }
            else
            {
                MessageBox.Show(
                    "Không thể xóa phòng.\n" +
                    "Có thể phòng đang được sử dụng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // LÀM MỚI PHÒNG
        // =====================================================
        private void BtnLamMoiPhong_Click(
            object sender,
            EventArgs e)
        {
            XoaTrangPhong();
            TaiDanhSachPhong();
        }

        private void XoaTrangPhong()
        {
            txtSoPhong.Clear();
            txtSoNguoiToiDa.Clear();
            txtDonGiaNgay.Clear();

            txtSoPhong.Enabled = true;

            if (cboMaKhuVuc.Items.Count > 0)
                cboMaKhuVuc.SelectedIndex = 0;

            txtSoPhong.Focus();
        }

        // =====================================================
        // CLICK PHÒNG
        // =====================================================
        private void DgvPhong_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvPhong.Rows[e.RowIndex];

            txtSoPhong.Text =
                row.Cells["SoPhong"].Value.ToString();

            cboMaKhuVuc.SelectedValue =
                row.Cells["MaKhuVuc"].Value.ToString();

            txtSoNguoiToiDa.Text =
                row.Cells["SoNguoiToiDa"].Value.ToString();

            txtDonGiaNgay.Text =
                row.Cells["DonGiaNgay"].Value.ToString();

            txtSoPhong.Enabled = false;
        }

        // =====================================================
        // THÊM TIỆN NGHI
        // =====================================================
        private void BtnThemTienNghi_Click(
            object sender,
            EventArgs e)
        {
            int soThuTu;

            if (string.IsNullOrWhiteSpace(txtMaTienNghi.Text) ||
                cboMaLoaiTN.SelectedValue == null ||
                !int.TryParse(
                    txtSoThuTu.Text,
                    out soThuTu) ||
                string.IsNullOrWhiteSpace(
                    txtTrangThai.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đúng thông tin tiện nghi!");

                return;
            }

            bool ketQua =
                service.ThemTienNghi(
                    txtMaTienNghi.Text.Trim(),
                    cboMaLoaiTN.SelectedValue.ToString(),
                    soThuTu,
                    dtpNgayMua.Value.Date,
                    txtTrangThai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Thêm tiện nghi thành công!");

                TaiDanhSachTienNghi();
                XoaTrangTienNghi();
            }
            else
            {
                MessageBox.Show(
                    "Thêm tiện nghi thất bại!");
            }
        }

        // =====================================================
        // SỬA TIỆN NGHI
        // =====================================================
        private void BtnSuaTienNghi_Click(
            object sender,
            EventArgs e)
        {
            int soThuTu;

            if (string.IsNullOrWhiteSpace(
                    txtMaTienNghi.Text) ||
                cboMaLoaiTN.SelectedValue == null ||
                !int.TryParse(
                    txtSoThuTu.Text,
                    out soThuTu))
            {
                MessageBox.Show(
                    "Vui lòng nhập đúng thông tin tiện nghi!");

                return;
            }

            bool ketQua =
                service.SuaTienNghi(
                    txtMaTienNghi.Text.Trim(),
                    cboMaLoaiTN.SelectedValue.ToString(),
                    soThuTu,
                    dtpNgayMua.Value.Date,
                    txtTrangThai.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Sửa tiện nghi thành công!");

                TaiDanhSachTienNghi();
                XoaTrangTienNghi();
            }
            else
            {
                MessageBox.Show(
                    "Sửa tiện nghi thất bại!");
            }
        }

        // =====================================================
        // XÓA TIỆN NGHI
        // =====================================================
        private void BtnXoaTienNghi_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                    txtMaTienNghi.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn tiện nghi cần xóa!");

                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa tiện nghi này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            bool ketQua =
                service.XoaTienNghi(
                    txtMaTienNghi.Text.Trim());

            if (ketQua)
            {
                MessageBox.Show(
                    "Xóa tiện nghi thành công!");

                TaiDanhSachTienNghi();
                XoaTrangTienNghi();
            }
            else
            {
                MessageBox.Show(
                    "Không thể xóa tiện nghi.\n" +
                    "Có thể tiện nghi đang được sử dụng.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // =====================================================
        // LÀM MỚI TIỆN NGHI
        // =====================================================
        private void BtnLamMoiTienNghi_Click(
            object sender,
            EventArgs e)
        {
            XoaTrangTienNghi();
            TaiDanhSachTienNghi();
        }

        private void XoaTrangTienNghi()
        {
            txtMaTienNghi.Clear();
            txtSoThuTu.Clear();
            txtTrangThai.Clear();

            txtMaTienNghi.Enabled = true;

            if (cboMaLoaiTN.Items.Count > 0)
                cboMaLoaiTN.SelectedIndex = 0;

            dtpNgayMua.Value = DateTime.Today;

            txtMaTienNghi.Focus();
        }

        // =====================================================
        // CLICK TIỆN NGHI
        // =====================================================
        private void DgvTienNghi_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvTienNghi.Rows[e.RowIndex];

            txtMaTienNghi.Text =
                row.Cells["MaTienNghi"].Value.ToString();

            cboMaLoaiTN.SelectedValue =
                row.Cells["MaLoaiTN"].Value.ToString();

            txtSoThuTu.Text =
                row.Cells["SoThuTu"].Value.ToString();

            if (row.Cells["NgayMua"].Value != null)
            {
                dtpNgayMua.Value =
                    Convert.ToDateTime(
                        row.Cells["NgayMua"].Value);
            }

            txtTrangThai.Text =
                row.Cells["TrangThai"].Value.ToString();

            txtMaTienNghi.Enabled = false;
        }
    }
}