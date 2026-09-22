using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmMuonTra : Form
    {
        private readonly MuonTraService service = new MuonTraService();

        private TextBox txtMaPhieu = null!;
        private TextBox txtMaDocGia = null!;
        private TextBox txtMaNhanVien = null!;
        private TextBox txtMaSach = null!;

        private DateTimePicker dtpNgayMuon = null!;
        private DateTimePicker dtpHanTra = null!;
        private DateTimePicker dtpNgayTra = null!;

        private ComboBox cboTinhTrang = null!;

        private Button btnMuon = null!;
        private Button btnTra = null!;
        private Button btnLamMoi = null!;

        private DataGridView dgvPhieuMuon = null!;
        private DataGridView dgvChiTiet = null!;

        public FrmMuonTra()
        {
            InitializeComponent();

            TaoGiaoDien();
            KhoiTaoGiaTriMacDinh();
            LoadPhieuMuon();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================
        private void TaoGiaoDien()
        {
            Text = "Quản lý mượn - trả sách";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1150, 750);
            BackColor = Color.White;

            // =========================
            // TIÊU ĐỀ
            // =========================
            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ MƯỢN - TRẢ SÁCH",
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 15),
                Size = new Size(1090, 50)
            };

            Controls.Add(lblTitle);

            // =========================
            // THÔNG TIN MƯỢN
            // =========================
            GroupBox grpThongTin = new GroupBox
            {
                Text = "THÔNG TIN MƯỢN SÁCH",
                Location = new Point(20, 75),
                Size = new Size(1090, 170),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            Controls.Add(grpThongTin);

            // Mã phiếu
            TaoLabel(grpThongTin, "Mã phiếu:", 20, 30);

            txtMaPhieu = new TextBox
            {
                Location = new Point(110, 27),
                Size = new Size(180, 25)
            };

            grpThongTin.Controls.Add(txtMaPhieu);

            // Mã độc giả
            TaoLabel(grpThongTin, "Mã độc giả:", 330, 30);

            txtMaDocGia = new TextBox
            {
                Location = new Point(430, 27),
                Size = new Size(180, 25)
            };

            grpThongTin.Controls.Add(txtMaDocGia);

            // Mã nhân viên
            TaoLabel(grpThongTin, "Mã nhân viên:", 650, 30);

            txtMaNhanVien = new TextBox
            {
                Location = new Point(760, 27),
                Size = new Size(180, 25)
            };

            grpThongTin.Controls.Add(txtMaNhanVien);

            // Mã sách
            TaoLabel(grpThongTin, "Mã sách:", 20, 75);

            txtMaSach = new TextBox
            {
                Location = new Point(110, 72),
                Size = new Size(180, 25)
            };

            grpThongTin.Controls.Add(txtMaSach);

            // Ngày mượn
            TaoLabel(grpThongTin, "Ngày mượn:", 330, 75);

            dtpNgayMuon = new DateTimePicker
            {
                Location = new Point(430, 72),
                Size = new Size(180, 25),
                Format = DateTimePickerFormat.Short
            };

            grpThongTin.Controls.Add(dtpNgayMuon);

            // Hạn trả
            TaoLabel(grpThongTin, "Hạn trả:", 650, 75);

            dtpHanTra = new DateTimePicker
            {
                Location = new Point(760, 72),
                Size = new Size(180, 25),
                Format = DateTimePickerFormat.Short
            };

            grpThongTin.Controls.Add(dtpHanTra);

            // Tình trạng
            TaoLabel(grpThongTin, "Tình trạng:", 20, 120);

            cboTinhTrang = new ComboBox
            {
                Location = new Point(110, 117),
                Size = new Size(180, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboTinhTrang.Items.AddRange(new object[]
            {
                "Bình thường",
                "Trễ",
                "Hư",
                "Mất"
            });

            grpThongTin.Controls.Add(cboTinhTrang);

            // Nút mượn
            btnMuon = TaoButton(
                grpThongTin,
                "MƯỢN SÁCH",
                330,
                115);

            // Nút trả
            btnTra = TaoButton(
                grpThongTin,
                "TRẢ SÁCH",
                480,
                115);

            // Nút làm mới
            btnLamMoi = TaoButton(
                grpThongTin,
                "LÀM MỚI",
                630,
                115);

            btnMuon.Click += BtnMuon_Click;
            btnTra.Click += BtnTra_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            // =========================
            // DANH SÁCH PHIẾU MƯỢN
            // =========================
            Label lblPhieu = new Label
            {
                Text = "DANH SÁCH PHIẾU MƯỢN",
                Font = new Font("Arial", 11, FontStyle.Bold),
                Location = new Point(20, 255),
                Size = new Size(400, 30)
            };

            Controls.Add(lblPhieu);

            dgvPhieuMuon = new DataGridView
            {
                Location = new Point(20, 290),
                Size = new Size(1090, 150),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            dgvPhieuMuon.CellClick += DgvPhieuMuon_CellClick;

            Controls.Add(dgvPhieuMuon);

            // =========================
            // CHI TIẾT PHIẾU MƯỢN
            // =========================
            Label lblChiTiet = new Label
            {
                Text = "CHI TIẾT PHIẾU MƯỢN",
                Font = new Font("Arial", 11, FontStyle.Bold),
                Location = new Point(20, 455),
                Size = new Size(400, 30)
            };

            Controls.Add(lblChiTiet);

            dgvChiTiet = new DataGridView
            {
                Location = new Point(20, 490),
                Size = new Size(1090, 180),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            dgvChiTiet.CellClick += DgvChiTiet_CellClick;

            Controls.Add(dgvChiTiet);
        }

        // =====================================================
        // GIÁ TRỊ MẶC ĐỊNH
        // =====================================================
        private void KhoiTaoGiaTriMacDinh()
        {
            dtpNgayMuon.Value = DateTime.Today;
            dtpHanTra.Value = DateTime.Today.AddDays(7);
            DateTime ngayTra = DateTime.Today;

            if (cboTinhTrang.Items.Count > 0)
                cboTinhTrang.SelectedIndex = 0;
        }

        // =====================================================
        // LABEL
        // =====================================================
        private void TaoLabel(
            Control parent,
            string text,
            int x,
            int y)
        {
            Label label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(100, 25),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };

            parent.Controls.Add(label);
        }

        // =====================================================
        // BUTTON
        // =====================================================
        private Button TaoButton(
            Control parent,
            string text,
            int x,
            int y)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(130, 32),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            parent.Controls.Add(button);

            return button;
        }

        // =====================================================
        // LOAD PHIẾU MƯỢN
        // =====================================================
        private void LoadPhieuMuon()
        {
            try
            {
                if (dgvPhieuMuon == null)
                    return;

                DataTable dt = service.LayPhieuMuon();

                dgvPhieuMuon.DataSource = null;
                dgvPhieuMuon.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách phiếu mượn.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CLICK PHIẾU MƯỢN
        // =====================================================
        private void DgvPhieuMuon_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (!dgvPhieuMuon.Columns.Contains("MaPhieuMuon"))
                    return;

                string maPhieu =
                    dgvPhieuMuon.Rows[e.RowIndex]
                    .Cells["MaPhieuMuon"]
                    .Value?
                    .ToString() ?? "";

                txtMaPhieu.Text = maPhieu;

                if (string.IsNullOrWhiteSpace(maPhieu))
                    return;

                dgvChiTiet.DataSource =
                    service.LayChiTietPhieuMuon(maPhieu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải chi tiết phiếu mượn.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CLICK CHI TIẾT
        // =====================================================
        private void DgvChiTiet_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                DataGridViewRow row =
                    dgvChiTiet.Rows[e.RowIndex];

                if (dgvChiTiet.Columns.Contains("MaDauSach"))
                {
                    txtMaSach.Text =
                        row.Cells["MaDauSach"]
                        .Value?
                        .ToString() ?? "";
                }

                if (dgvChiTiet.Columns.Contains("TinhTrangTra"))
                {
                    string tinhTrang =
                        row.Cells["TinhTrangTra"]
                        .Value?
                        .ToString() ?? "";

                    if (!string.IsNullOrWhiteSpace(tinhTrang))
                    {
                        int index =
                            cboTinhTrang.Items.IndexOf(tinhTrang);

                        if (index >= 0)
                            cboTinhTrang.SelectedIndex = index;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể chọn chi tiết phiếu.\n\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // MƯỢN SÁCH
        // =====================================================
        private void BtnMuon_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                string maPhieu = txtMaPhieu.Text.Trim();
                string maDocGia = txtMaDocGia.Text.Trim();
                string maNhanVien = txtMaNhanVien.Text.Trim();
                string maSach = txtMaSach.Text.Trim();

                // Kiểm tra dữ liệu nhập
                if (string.IsNullOrWhiteSpace(maPhieu) ||
                    string.IsNullOrWhiteSpace(maDocGia) ||
                    string.IsNullOrWhiteSpace(maNhanVien) ||
                    string.IsNullOrWhiteSpace(maSach))
                {
                    MessageBox.Show(
                        "Vui lòng nhập đầy đủ:\n" +
                        "- Mã phiếu\n" +
                        "- Mã độc giả\n" +
                        "- Mã nhân viên\n" +
                        "- Mã sách",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Hạn trả >= ngày mượn
                if (dtpHanTra.Value.Date < dtpNgayMuon.Value.Date)
                {
                    MessageBox.Show(
                        "Hạn trả không được nhỏ hơn ngày mượn.",
                        "Vi phạm quy tắc",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Kiểm tra độc giả
                if (!service.KiemTraDocGia(maDocGia))
                {
                    MessageBox.Show(
                        "Độc giả không tồn tại.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Kiểm tra quá hạn
                if (service.CoSachQuaHan(maDocGia))
                {
                    MessageBox.Show(
                        "Độc giả đang có sách quá hạn nên không được mượn thêm.",
                        "Vi phạm quy tắc",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Tối đa 3 đầu sách
                if (service.DemSachDangMuon(maDocGia) >= 3)
                {
                    MessageBox.Show(
                        "Độc giả đã mượn tối đa 3 đầu sách.",
                        "Vi phạm quy tắc",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Kiểm tra sách
                if (!service.KiemTraSachCon(maSach))
                {
                    MessageBox.Show(
                        "Sách không tồn tại hoặc đã hết.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Kiểm tra sách đã có trong phiếu
                if (service.SachDaCoTrongPhieu(maPhieu, maSach))
                {
                    MessageBox.Show(
                        "Sách này đã có trong phiếu mượn.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // =========================
                // TẠO PHIẾU
                // =========================
                int ketQuaPhieu =
                    service.TaoPhieuMuon(
                        maPhieu,
                        maDocGia,
                        maNhanVien,
                        dtpNgayMuon.Value.Date,
                        dtpHanTra.Value.Date);

                if (ketQuaPhieu <= 0)
                {
                    MessageBox.Show(
                        "Không tạo được phiếu mượn.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // =========================
                // TẠO CHI TIẾT
                // =========================
                string maChiTiet =
                    "CT" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                int ketQuaChiTiet =
                    service.ThemChiTietMuon(
                        maChiTiet,
                        maPhieu,
                        maSach);

                if (ketQuaChiTiet <= 0)
                {
                    MessageBox.Show(
                        "Không thêm được chi tiết phiếu mượn.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // =========================
                // GIẢM SỐ LƯỢNG
                // =========================
                int ketQuaSoLuong =
                    service.GiamSoLuongSach(maSach);

                if (ketQuaSoLuong <= 0)
                {
                    MessageBox.Show(
                        "Không thể cập nhật số lượng sách.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                MessageBox.Show(
                    "Mượn sách thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadPhieuMuon();
                XoaNhapLieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi mượn sách:\n\n" +
                    ex.ToString(),
                    "Lỗi chi tiết",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // TRẢ SÁCH
        // =====================================================
        private void BtnTra_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                if (dgvChiTiet.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Vui lòng chọn sách cần trả.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string maChiTiet =
                    dgvChiTiet.CurrentRow
                    .Cells["MaChiTietMuon"]
                    .Value?
                    .ToString() ?? "";

                string maSach =
                    dgvChiTiet.CurrentRow
                    .Cells["MaDauSach"]
                    .Value?
                    .ToString() ?? "";

                if (string.IsNullOrWhiteSpace(maChiTiet))
                {
                    MessageBox.Show(
                        "Không xác định được chi tiết phiếu mượn.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(maSach))
                {
                    MessageBox.Show(
                        "Không xác định được mã sách.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                string tinhTrang =
                    cboTinhTrang.SelectedItem?
                    .ToString() ?? "Bình thường";

                // =========================
                // CẬP NHẬT TRẢ SÁCH
                // =========================
                int ketQuaTra =
                    service.TraSach(
                        maChiTiet,
                        dtpNgayTra.Value.Date,
                        tinhTrang);

                if (ketQuaTra <= 0)
                {
                    MessageBox.Show(
                        "Không cập nhật được thông tin trả sách.",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                // Nếu không mất thì cộng lại số lượng
                if (tinhTrang != "Mất")
                {
                    service.TangSoLuongSach(maSach);
                }

                MessageBox.Show(
                    "Trả sách thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                string maPhieu =
                    txtMaPhieu.Text.Trim();

                if (!string.IsNullOrWhiteSpace(maPhieu))
                {
                    dgvChiTiet.DataSource =
                        service.LayChiTietPhieuMuon(maPhieu);
                }

                LoadPhieuMuon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi khi trả sách:\n\n" +
                    ex.ToString(),
                    "Lỗi chi tiết",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // LÀM MỚI
        // =====================================================
        private void BtnLamMoi_Click(
            object? sender,
            EventArgs e)
        {
            XoaNhapLieu();

            dgvChiTiet.DataSource = null;

            LoadPhieuMuon();
        }

        // =====================================================
        // XÓA NHẬP LIỆU
        // =====================================================
        private void XoaNhapLieu()
        {
            txtMaPhieu.Clear();
            txtMaDocGia.Clear();
            txtMaNhanVien.Clear();
            txtMaSach.Clear();

            dtpNgayMuon.Value = DateTime.Today;
            dtpHanTra.Value = DateTime.Today.AddDays(7);
            dtpNgayTra.Value = DateTime.Today;

            if (cboTinhTrang.Items.Count > 0)
                cboTinhTrang.SelectedIndex = 0;
        }
    }
}