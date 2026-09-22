using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public class FrmDocGia : Form
    {
        private readonly DocGiaService _service = new DocGiaService();

        private TextBox txtMaDocGia = null!;
        private TextBox txtHo = null!;
        private TextBox txtTen = null!;
        private DateTimePicker dtpNgaySinh = null!;
        private ComboBox cboPhai = null!;
        private TextBox txtSoDienThoai = null!;
        private TextBox txtDiaChi = null!;
        private TextBox txtEmail = null!;
        private TextBox txtAnh3x4 = null!;
        private TextBox txtTimKiem = null!;

        private DataGridView dgvDocGia = null!;

        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnLamMoi = null!;
        private Button btnTim = null!;

        public FrmDocGia()
        {
            TaoGiaoDien();
            LoadDuLieu();
        }

        private void TaoGiaoDien()
        {
            Text = "Quản lý độc giả";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1150, 800);
            BackColor = Color.White;

            Label lblTitle = new Label
            {
                Text = "QUẢN LÝ ĐỘC GIẢ",
                Font = new Font("Arial", 20, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(30, 15),
                Size = new Size(1070, 45)
            };

            Controls.Add(lblTitle);

            // Mã độc giả
            TaoLabel("Mã độc giả:", 30, 80);

            txtMaDocGia = new TextBox
            {
                Location = new Point(130, 77),
                Size = new Size(180, 30)
            };

            Controls.Add(txtMaDocGia);

            // Họ
            TaoLabel("Họ:", 340, 80);

            txtHo = new TextBox
            {
                Location = new Point(380, 77),
                Size = new Size(180, 30)
            };

            Controls.Add(txtHo);

            // Tên
            TaoLabel("Tên:", 590, 80);

            txtTen = new TextBox
            {
                Location = new Point(640, 77),
                Size = new Size(180, 30)
            };

            Controls.Add(txtTen);

            // Ngày sinh
            TaoLabel("Ngày sinh:", 850, 80);

            dtpNgaySinh = new DateTimePicker
            {
                Location = new Point(930, 77),
                Size = new Size(170, 30),
                Format = DateTimePickerFormat.Short
            };

            Controls.Add(dtpNgaySinh);

            // Phái
            TaoLabel("Phái:", 30, 125);

            cboPhai = new ComboBox
            {
                Location = new Point(130, 122),
                Size = new Size(180, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboPhai.Items.Add("Nam");
            cboPhai.Items.Add("Nữ");
            cboPhai.SelectedIndex = 0;

            Controls.Add(cboPhai);

            // Số điện thoại
            TaoLabel("SĐT:", 340, 125);

            txtSoDienThoai = new TextBox
            {
                Location = new Point(380, 122),
                Size = new Size(180, 30)
            };

            Controls.Add(txtSoDienThoai);

            // Địa chỉ
            TaoLabel("Địa chỉ:", 590, 125);

            txtDiaChi = new TextBox
            {
                Location = new Point(640, 122),
                Size = new Size(460, 30)
            };

            Controls.Add(txtDiaChi);

            // Email
            TaoLabel("Email:", 30, 170);

            txtEmail = new TextBox
            {
                Location = new Point(130, 167),
                Size = new Size(430, 30)
            };

            Controls.Add(txtEmail);

            // Ảnh 3x4
            TaoLabel("Ảnh 3x4:", 590, 170);

            txtAnh3x4 = new TextBox
            {
                Location = new Point(640, 167),
                Size = new Size(460, 30)
            };

            Controls.Add(txtAnh3x4);

            // Các nút
            btnThem = TaoButton("THÊM", 30, 220);
            btnSua = TaoButton("SỬA", 140, 220);
            btnXoa = TaoButton("XÓA", 250, 220);
            btnLamMoi = TaoButton("LÀM MỚI", 360, 220);

            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnLamMoi.Click += BtnLamMoi_Click;

            // Tìm kiếm
            TaoLabel("Tìm kiếm:", 30, 275);

            txtTimKiem = new TextBox
            {
                Location = new Point(130, 272),
                Size = new Size(450, 30)
            };

            Controls.Add(txtTimKiem);

            btnTim = TaoButton("TÌM", 600, 270);
            btnTim.Click += BtnTim_Click;

            // Bảng dữ liệu
            dgvDocGia = new DataGridView
            {
                Location = new Point(30, 325),
                Size = new Size(1070, 390),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            dgvDocGia.CellClick += DgvDocGia_CellClick;

            Controls.Add(dgvDocGia);
        }

        private void TaoLabel(string text, int x, int y)
        {
            Label label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(95, 30),
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
                Size = new Size(100, 35),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            Controls.Add(button);

            return button;
        }

        private void LoadDuLieu()
        {
            try
            {
                dgvDocGia.DataSource = _service.LayDocGia();
                DatTenCot();
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

        private void DatTenCot()
        {
            if (dgvDocGia.Columns.Count == 0)
                return;

            dgvDocGia.Columns["MaDocGia"].HeaderText = "Mã độc giả";
            dgvDocGia.Columns["Ho"].HeaderText = "Họ";
            dgvDocGia.Columns["Ten"].HeaderText = "Tên";
            dgvDocGia.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvDocGia.Columns["Phai"].HeaderText = "Phái";
            dgvDocGia.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dgvDocGia.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvDocGia.Columns["Email"].HeaderText = "Email";
            dgvDocGia.Columns["Anh3x4"].HeaderText = "Ảnh 3x4";
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtMaDocGia.Text))
            {
                MessageBox.Show("Vui lòng nhập mã độc giả.");
                txtMaDocGia.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHo.Text))
            {
                MessageBox.Show("Vui lòng nhập họ.");
                txtHo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên.");
                txtTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.");
                txtDiaChi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email.");
                txtEmail.Focus();
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

                _service.ThemDocGia(
                    txtMaDocGia.Text.Trim(),
                    txtHo.Text.Trim(),
                    txtTen.Text.Trim(),
                    dtpNgaySinh.Value.Date,
                    cboPhai.Text,
                    txtSoDienThoai.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtAnh3x4.Text.Trim()
                );

                MessageBox.Show("Thêm độc giả thành công.");

                LoadDuLieu();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể thêm độc giả:\n" + ex.Message,
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

                _service.SuaDocGia(
                    txtMaDocGia.Text.Trim(),
                    txtHo.Text.Trim(),
                    txtTen.Text.Trim(),
                    dtpNgaySinh.Value.Date,
                    cboPhai.Text,
                    txtSoDienThoai.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtAnh3x4.Text.Trim()
                );

                MessageBox.Show("Cập nhật độc giả thành công.");

                LoadDuLieu();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể cập nhật độc giả:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDocGia.Text))
                {
                    MessageBox.Show("Vui lòng chọn độc giả cần xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa độc giả này không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                _service.XoaDocGia(txtMaDocGia.Text.Trim());

                MessageBox.Show("Xóa độc giả thành công.");

                LoadDuLieu();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xóa độc giả.\nNếu độc giả đã có thẻ hoặc phiếu mượn, dữ liệu có thể đang được liên kết.\n\n"
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
                    LoadDuLieu();
                }
                else
                {
                    dgvDocGia.DataSource = _service.TimKiemDocGia(tuKhoa);
                    DatTenCot();
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

        private void DgvDocGia_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvDocGia.Rows[e.RowIndex];

            txtMaDocGia.Text = row.Cells["MaDocGia"].Value?.ToString() ?? "";
            txtHo.Text = row.Cells["Ho"].Value?.ToString() ?? "";
            txtTen.Text = row.Cells["Ten"].Value?.ToString() ?? "";

            if (DateTime.TryParse(
                row.Cells["NgaySinh"].Value?.ToString(),
                out DateTime ngaySinh))
            {
                dtpNgaySinh.Value = ngaySinh;
            }

            string phai = row.Cells["Phai"].Value?.ToString() ?? "";

            if (cboPhai.Items.Contains(phai))
                cboPhai.SelectedItem = phai;

            txtSoDienThoai.Text =
                row.Cells["SoDienThoai"].Value?.ToString() ?? "";

            txtDiaChi.Text =
                row.Cells["DiaChi"].Value?.ToString() ?? "";

            txtEmail.Text =
                row.Cells["Email"].Value?.ToString() ?? "";

            txtAnh3x4.Text =
                row.Cells["Anh3x4"].Value?.ToString() ?? "";
        }

        private void XoaTrang()
        {
            txtMaDocGia.Clear();
            txtHo.Clear();
            txtTen.Clear();

            dtpNgaySinh.Value = DateTime.Now;

            if (cboPhai.Items.Count > 0)
                cboPhai.SelectedIndex = 0;

            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtAnh3x4.Clear();

            txtMaDocGia.Focus();
        }
    }
}