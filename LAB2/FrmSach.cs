using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmSach : Form
    {
        private readonly SachService _service = new SachService();

        private TextBox txtMaSach = null!;
        private TextBox txtTenSach = null!;
        private NumericUpDown numNamXB = null!;
        private NumericUpDown numSoLuong = null!;
        private ComboBox cboTheLoai = null!;
        private ComboBox cboNhaXuatBan = null!;
        private TextBox txtTimKiem = null!;
        private DataGridView dgvSach = null!;

        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnLamMoi = null!;
        private Button btnTim = null!;

        public FrmSach()
        {
            InitializeComponent();
            TaoGiaoDien();
            LoadDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý sách";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1100, 750);
            BackColor = Color.White;

            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ SÁCH",
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 15),
                Size = new Size(1040, 45)
            };
            Controls.Add(lblTitle);

            // Mã sách
            TaoLabel("Mã sách:", 30, 80);
            txtMaSach = new TextBox
            {
                Location = new Point(130, 77),
                Size = new Size(180, 30)
            };
            Controls.Add(txtMaSach);

            // Tên sách
            TaoLabel("Tên sách:", 340, 80);
            txtTenSach = new TextBox
            {
                Location = new Point(430, 77),
                Size = new Size(250, 30)
            };
            Controls.Add(txtTenSach);

            // Năm xuất bản
            TaoLabel("Năm XB:", 710, 80);
            numNamXB = new NumericUpDown
            {
                Location = new Point(790, 77),
                Size = new Size(120, 30),
                Minimum = 1900,
                Maximum = 2100,
                Value = DateTime.Now.Year
            };
            Controls.Add(numNamXB);

            // Số lượng
            TaoLabel("Số lượng:", 30, 125);
            numSoLuong = new NumericUpDown
            {
                Location = new Point(130, 122),
                Size = new Size(180, 30),
                Minimum = 0,
                Maximum = 100000,
                Value = 1
            };
            Controls.Add(numSoLuong);

            // Thể loại
            TaoLabel("Thể loại:", 340, 125);
            cboTheLoai = new ComboBox
            {
                Location = new Point(430, 122),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Controls.Add(cboTheLoai);

            // Nhà xuất bản
            TaoLabel("Nhà XB:", 710, 125);
            cboNhaXuatBan = new ComboBox
            {
                Location = new Point(790, 122),
                Size = new Size(250, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            Controls.Add(cboNhaXuatBan);

            // Buttons
            btnThem = TaoButton("THÊM", 30, 175);
            btnSua = TaoButton("SỬA", 140, 175);
            btnXoa = TaoButton("XÓA", 250, 175);
            btnLamMoi = TaoButton("LÀM MỚI", 360, 175);

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            // Tìm kiếm
            TaoLabel("Tìm kiếm:", 30, 225);

            txtTimKiem = new TextBox
            {
                Location = new Point(130, 222),
                Size = new Size(450, 30)
            };
            Controls.Add(txtTimKiem);

            btnTim = TaoButton("TÌM", 600, 220);
            btnTim.Click += BtnTim_Click;

            // DataGridView
            dgvSach = new DataGridView
            {
                Location = new Point(30, 270),
                Size = new Size(1010, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            dgvSach.CellClick += DgvSach_CellClick;

            Controls.Add(dgvSach);
        }

        private void TaoLabel(string text, int x, int y)
        {
            Label label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(90, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Controls.Add(label);
        }

        private Button TaoButton(string text, int x, int y)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(100, 35)
            };

            Controls.Add(button);
            return button;
        }

        private void LoadDuLieu()
        {
            try
            {
                dgvSach.DataSource = _service.LaySach();

                LoadTheLoai();
                LoadNhaXuatBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoadTheLoai()
        {
            string sql = @"
                SELECT MaTheLoai, TenTheLoai
                FROM TheLoai
                ORDER BY MaTheLoai";

            DataTable dt = QuanLyThuVien.Data.Db.Query(sql);

            cboTheLoai.DataSource = dt;
            cboTheLoai.DisplayMember = "TenTheLoai";
            cboTheLoai.ValueMember = "MaTheLoai";
        }

        private void LoadNhaXuatBan()
        {
            string sql = @"
                SELECT MaNhaXuatBan, TenNhaXuatBan
                FROM NhaXuatBan
                ORDER BY MaNhaXuatBan";

            DataTable dt = QuanLyThuVien.Data.Db.Query(sql);

            cboNhaXuatBan.DataSource = dt;
            cboNhaXuatBan.DisplayMember = "TenNhaXuatBan";
            cboNhaXuatBan.ValueMember = "MaNhaXuatBan";
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sách.");
                txtMaSach.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenSach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sách.");
                txtTenSach.Focus();
                return false;
            }

            if (cboTheLoai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn thể loại.");
                return false;
            }

            if (cboNhaXuatBan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà xuất bản.");
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieu())
                    return;

                _service.ThemSach(
                    txtMaSach.Text.Trim(),
                    txtTenSach.Text.Trim(),
                    (int)numNamXB.Value,
                    (int)numSoLuong.Value,
                    cboTheLoai.SelectedValue!.ToString()!,
                    cboNhaXuatBan.SelectedValue!.ToString()!
                );

                MessageBox.Show("Thêm sách thành công.");

                LoadDuLieu();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm sách:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!KiemTraDuLieu())
                    return;

                _service.SuaSach(
                    txtMaSach.Text.Trim(),
                    txtTenSach.Text.Trim(),
                    (int)numNamXB.Value,
                    (int)numSoLuong.Value,
                    cboTheLoai.SelectedValue!.ToString()!,
                    cboNhaXuatBan.SelectedValue!.ToString()!
                );

                MessageBox.Show("Cập nhật sách thành công.");

                LoadDuLieu();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật sách:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaSach.Text))
                {
                    MessageBox.Show("Vui lòng chọn sách cần xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa sách này không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                _service.XoaSach(txtMaSach.Text.Trim());

                MessageBox.Show("Xóa sách thành công.");

                LoadDuLieu();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa sách.\nNếu sách đã có dữ liệu mượn, hệ thống có thể không cho xóa.\n\n"
                    + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            LoadDuLieu();
            XoaTrang();
        }

        private void BtnTim_Click(object? sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();

                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    dgvSach.DataSource = _service.LaySach();
                }
                else
                {
                    dgvSach.DataSource = _service.TimKiemSach(tuKhoa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tìm kiếm:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void DgvSach_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvSach.Rows[e.RowIndex];

            txtMaSach.Text = row.Cells["MaDauSach"].Value?.ToString() ?? "";
            txtTenSach.Text = row.Cells["TenSach"].Value?.ToString() ?? "";

            if (int.TryParse(row.Cells["NamXuatBan"].Value?.ToString(), out int nam))
                numNamXB.Value = Math.Max(numNamXB.Minimum, Math.Min(numNamXB.Maximum, nam));

            if (int.TryParse(row.Cells["SoLuongHienCo"].Value?.ToString(), out int soLuong))
                numSoLuong.Value = Math.Max(numSoLuong.Minimum, Math.Min(numSoLuong.Maximum, soLuong));

            if (row.Cells["MaTheLoai"].Value != null)
                cboTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value.ToString();

            if (row.Cells["MaNhaXuatBan"].Value != null)
                cboNhaXuatBan.SelectedValue = row.Cells["MaNhaXuatBan"].Value.ToString();
        }

        private void XoaTrang()
        {
            txtMaSach.Clear();
            txtTenSach.Clear();

            numNamXB.Value = DateTime.Now.Year;
            numSoLuong.Value = 1;

            if (cboTheLoai.Items.Count > 0)
                cboTheLoai.SelectedIndex = 0;

            if (cboNhaXuatBan.Items.Count > 0)
                cboNhaXuatBan.SelectedIndex = 0;

            txtMaSach.Focus();
        }
    }
}