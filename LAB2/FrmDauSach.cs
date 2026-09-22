using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyThuVien.Services;

namespace QuanLyThuVien
{
    public partial class FrmDauSach : Form
    {
        private readonly DauSachService service = new DauSachService();

        private TextBox txtMaDauSach = null!;
        private TextBox txtTenSach = null!;
        private TextBox txtNamXuatBan = null!;
        private TextBox txtSoLuong = null!;
        private TextBox txtMaTheLoai = null!;
        private TextBox txtMaNhaXuatBan = null!;
        private TextBox txtTimKiem = null!;

        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnTimKiem = null!;
        private Button btnLamMoi = null!;

        private DataGridView dgvDauSach = null!;

        public FrmDauSach()
        {
            TaoGiaoDien();
            LoadDanhSach();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================
        private void TaoGiaoDien()
        {
            this.Text = "Quản lý đầu sách";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1100, 700);
            this.MinimumSize = new Size(1000, 600);

            // =========================
            // TIÊU ĐỀ
            // =========================
            Label lblTieuDe = new Label
            {
                Text = "QUẢN LÝ ĐẦU SÁCH",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(400, 20)
            };

            this.Controls.Add(lblTieuDe);

            // =========================
            // MÃ ĐẦU SÁCH
            // =========================
            Label lblMaDauSach = new Label
            {
                Text = "Mã đầu sách:",
                Location = new Point(30, 80),
                AutoSize = true
            };

            txtMaDauSach = new TextBox
            {
                Location = new Point(150, 75),
                Width = 250
            };

            // =========================
            // TÊN SÁCH
            // =========================
            Label lblTenSach = new Label
            {
                Text = "Tên sách:",
                Location = new Point(30, 120),
                AutoSize = true
            };

            txtTenSach = new TextBox
            {
                Location = new Point(150, 115),
                Width = 250
            };

            // =========================
            // NĂM XUẤT BẢN
            // =========================
            Label lblNamXuatBan = new Label
            {
                Text = "Năm xuất bản:",
                Location = new Point(30, 160),
                AutoSize = true
            };

            txtNamXuatBan = new TextBox
            {
                Location = new Point(150, 155),
                Width = 250
            };

            // =========================
            // SỐ LƯỢNG
            // =========================
            Label lblSoLuong = new Label
            {
                Text = "Số lượng:",
                Location = new Point(30, 200),
                AutoSize = true
            };

            txtSoLuong = new TextBox
            {
                Location = new Point(150, 195),
                Width = 250
            };

            // =========================
            // MÃ THỂ LOẠI
            // =========================
            Label lblMaTheLoai = new Label
            {
                Text = "Mã thể loại:",
                Location = new Point(500, 80),
                AutoSize = true
            };

            txtMaTheLoai = new TextBox
            {
                Location = new Point(620, 75),
                Width = 250
            };

            // =========================
            // MÃ NHÀ XUẤT BẢN
            // =========================
            Label lblMaNhaXuatBan = new Label
            {
                Text = "Mã nhà xuất bản:",
                Location = new Point(500, 120),
                AutoSize = true
            };

            txtMaNhaXuatBan = new TextBox
            {
                Location = new Point(620, 115),
                Width = 250
            };

            // =========================
            // NÚT THÊM
            // =========================
            btnThem = new Button
            {
                Text = "Thêm",
                Location = new Point(500, 170),
                Size = new Size(100, 40)
            };

            btnThem.Click += BtnThem_Click;

            // =========================
            // NÚT SỬA
            // =========================
            btnSua = new Button
            {
                Text = "Sửa",
                Location = new Point(610, 170),
                Size = new Size(100, 40)
            };

            btnSua.Click += BtnSua_Click;

            // =========================
            // NÚT XÓA
            // =========================
            btnXoa = new Button
            {
                Text = "Xóa",
                Location = new Point(720, 170),
                Size = new Size(100, 40)
            };

            btnXoa.Click += BtnXoa_Click;

            // =========================
            // NÚT LÀM MỚI
            // =========================
            btnLamMoi = new Button
            {
                Text = "Làm mới",
                Location = new Point(830, 170),
                Size = new Size(100, 40)
            };

            btnLamMoi.Click += BtnLamMoi_Click;

            // =========================
            // TÌM KIẾM
            // =========================
            Label lblTimKiem = new Label
            {
                Text = "Tìm kiếm:",
                Location = new Point(30, 255),
                AutoSize = true
            };

            txtTimKiem = new TextBox
            {
                Location = new Point(150, 250),
                Width = 300
            };

            btnTimKiem = new Button
            {
                Text = "Tìm kiếm",
                Location = new Point(460, 248),
                Size = new Size(100, 35)
            };

            btnTimKiem.Click += BtnTimKiem_Click;

            // =========================
            // DATAGRIDVIEW
            // =========================
            dgvDauSach = new DataGridView
            {
                Location = new Point(30, 300),
                Size = new Size(1020, 320),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            dgvDauSach.CellClick += DgvDauSach_CellClick;

            // =========================
            // THÊM CONTROLS VÀO FORM
            // =========================
            this.Controls.Add(lblMaDauSach);
            this.Controls.Add(txtMaDauSach);

            this.Controls.Add(lblTenSach);
            this.Controls.Add(txtTenSach);

            this.Controls.Add(lblNamXuatBan);
            this.Controls.Add(txtNamXuatBan);

            this.Controls.Add(lblSoLuong);
            this.Controls.Add(txtSoLuong);

            this.Controls.Add(lblMaTheLoai);
            this.Controls.Add(txtMaTheLoai);

            this.Controls.Add(lblMaNhaXuatBan);
            this.Controls.Add(txtMaNhaXuatBan);

            this.Controls.Add(btnThem);
            this.Controls.Add(btnSua);
            this.Controls.Add(btnXoa);
            this.Controls.Add(btnLamMoi);

            this.Controls.Add(lblTimKiem);
            this.Controls.Add(txtTimKiem);
            this.Controls.Add(btnTimKiem);

            this.Controls.Add(dgvDauSach);
        }

        // =====================================================
        // LOAD DANH SÁCH
        // =====================================================
        private void LoadDanhSach()
        {
            try
            {
                dgvDauSach.DataSource = service.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách đầu sách.\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // THÊM
        // =====================================================
        private void BtnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDauSach.Text) ||
                    string.IsNullOrWhiteSpace(txtTenSach.Text))
                {
                    MessageBox.Show(
                        "Vui lòng nhập đầy đủ thông tin!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (!int.TryParse(txtNamXuatBan.Text, out int namXuatBan))
                {
                    MessageBox.Show("Năm xuất bản phải là số!");
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong))
                {
                    MessageBox.Show("Số lượng phải là số!");
                    return;
                }

                service.Them(
                    txtMaDauSach.Text.Trim(),
                    txtTenSach.Text.Trim(),
                    namXuatBan,
                    soLuong,
                    txtMaTheLoai.Text.Trim(),
                    txtMaNhaXuatBan.Text.Trim()
                );

                MessageBox.Show(
                    "Thêm đầu sách thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Thêm đầu sách thất bại!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // SỬA
        // =====================================================
        private void BtnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtNamXuatBan.Text, out int namXuatBan))
                {
                    MessageBox.Show("Năm xuất bản phải là số!");
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong))
                {
                    MessageBox.Show("Số lượng phải là số!");
                    return;
                }

                service.Sua(
                    txtMaDauSach.Text.Trim(),
                    txtTenSach.Text.Trim(),
                    namXuatBan,
                    soLuong,
                    txtMaTheLoai.Text.Trim(),
                    txtMaNhaXuatBan.Text.Trim()
                );

                MessageBox.Show(
                    "Cập nhật đầu sách thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cập nhật thất bại!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // XÓA
        // =====================================================
        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaDauSach.Text))
                {
                    MessageBox.Show("Vui lòng chọn đầu sách cần xóa!");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa đầu sách này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                service.Xoa(txtMaDauSach.Text.Trim());

                MessageBox.Show(
                    "Xóa đầu sách thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Xóa đầu sách thất bại!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // TÌM KIẾM
        // =====================================================
        private void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();

                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    LoadDanhSach();
                    return;
                }

                dgvDauSach.DataSource = service.TimKiem(tuKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Tìm kiếm thất bại!\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // LÀM MỚI
        // =====================================================
        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            XoaTrang();
            LoadDanhSach();
        }

        // =====================================================
        // CLICK VÀO DÒNG
        // =====================================================
        private void DgvDauSach_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvDauSach.Rows[e.RowIndex];

            txtMaDauSach.Text =
                row.Cells["MaDauSach"].Value?.ToString() ?? "";

            txtTenSach.Text =
                row.Cells["TenSach"].Value?.ToString() ?? "";

            txtNamXuatBan.Text =
                row.Cells["NamXuatBan"].Value?.ToString() ?? "";

            txtSoLuong.Text =
                row.Cells["SoLuongHienCo"].Value?.ToString() ?? "";

            txtMaTheLoai.Text =
                row.Cells["MaTheLoai"].Value?.ToString() ?? "";

            txtMaNhaXuatBan.Text =
                row.Cells["MaNhaXuatBan"].Value?.ToString() ?? "";
        }

        // =====================================================
        // XÓA TRẮNG
        // =====================================================
        private void XoaTrang()
        {
            txtMaDauSach.Clear();
            txtTenSach.Clear();
            txtNamXuatBan.Clear();
            txtSoLuong.Clear();
            txtMaTheLoai.Clear();
            txtMaNhaXuatBan.Clear();
            txtTimKiem.Clear();

            txtMaDauSach.Focus();
        }
    }
}