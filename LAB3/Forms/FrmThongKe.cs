using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService service =
            new ThongKeService();

        private DataGridView dgvHoaDon = null;
        private Label lblTongDoanhThu = null;
        private Label lblTongTienPhong = null;
        private Label lblTongTienDichVu = null;
        private Button btnLamMoi = null;

        public FrmThongKe()
        {
            InitializeComponent();
            TaoGiaoDien();

            this.Load += FrmThongKe_Load;
        }

        // =====================================================
        // FORM LOAD
        // =====================================================

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            TaiDuLieu();
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================

        private void TaoGiaoDien()
        {
            this.Controls.Clear();

            this.Text = "Thống kê";
            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.Size =
                new Size(1100, 700);

            this.BackColor =
                Color.White;

            Label lblTieuDe = new Label();

            lblTieuDe.Text =
                "THỐNG KÊ DOANH THU";

            lblTieuDe.Font =
                new Font(
                    "Arial",
                    20,
                    FontStyle.Bold);

            lblTieuDe.TextAlign =
                ContentAlignment.MiddleCenter;

            lblTieuDe.Dock =
                DockStyle.Top;

            lblTieuDe.Height = 60;

            this.Controls.Add(lblTieuDe);

            // =================================================
            // TỔNG TIỀN PHÒNG
            // =================================================

            lblTongTienPhong =
                TaoLabelThongKe(
                    "Tổng tiền phòng: 0",
                    30,
                    80,
                    300);

            // =================================================
            // TỔNG TIỀN DỊCH VỤ
            // =================================================

            lblTongTienDichVu =
                TaoLabelThongKe(
                    "Tổng tiền dịch vụ: 0",
                    350,
                    80,
                    300);

            // =================================================
            // TỔNG DOANH THU
            // =================================================

            lblTongDoanhThu =
                TaoLabelThongKe(
                    "Tổng doanh thu: 0",
                    670,
                    80,
                    350);

            this.Controls.Add(
                lblTongTienPhong);

            this.Controls.Add(
                lblTongTienDichVu);

            this.Controls.Add(
                lblTongDoanhThu);

            // =================================================
            // NÚT LÀM MỚI
            // =================================================

            btnLamMoi =
                new Button();

            btnLamMoi.Text =
                "Làm mới";

            btnLamMoi.Left = 30;
            btnLamMoi.Top = 145;
            btnLamMoi.Width = 110;
            btnLamMoi.Height = 35;

            btnLamMoi.Click +=
                BtnLamMoi_Click;

            this.Controls.Add(btnLamMoi);

            // =================================================
            // DATAGRIDVIEW
            // =================================================

            dgvHoaDon =
                new DataGridView();

            dgvHoaDon.Left = 30;
            dgvHoaDon.Top = 200;
            dgvHoaDon.Width = 990;
            dgvHoaDon.Height = 400;

            dgvHoaDon.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvHoaDon.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvHoaDon.ReadOnly = true;

            dgvHoaDon.AllowUserToAddRows =
                false;

            dgvHoaDon.MultiSelect =
                false;

            this.Controls.Add(dgvHoaDon);
        }

        // =====================================================
        // LABEL THỐNG KÊ
        // =====================================================

        private Label TaoLabelThongKe(
            string text,
            int left,
            int top,
            int width)
        {
            Label label =
                new Label();

            label.Text = text;

            label.Left = left;
            label.Top = top;
            label.Width = width;
            label.Height = 50;

            label.Font =
                new Font(
                    "Arial",
                    11,
                    FontStyle.Bold);

            label.BorderStyle =
                BorderStyle.FixedSingle;

            label.TextAlign =
                ContentAlignment.MiddleCenter;

            return label;
        }

        // =====================================================
        // TẢI DỮ LIỆU
        // =====================================================

        private void TaiDuLieu()
        {
            try
            {
                DataTable dt =
                    service.LayDanhSachHoaDon();

                dgvHoaDon.DataSource =
                    dt;

                decimal tongTienPhong =
                    service.LayTongTienPhong();

                decimal tongTienDichVu =
                    service.LayTongTienDichVu();

                decimal tongDoanhThu =
                    service.LayTongDoanhThu();

                lblTongTienPhong.Text =
                    "Tổng tiền phòng: " +
                    tongTienPhong.ToString("N0");

                lblTongTienDichVu.Text =
                    "Tổng tiền dịch vụ: " +
                    tongTienDichVu.ToString("N0");

                lblTongDoanhThu.Text =
                    "Tổng doanh thu: " +
                    tongDoanhThu.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu thống kê.\n" +
                    ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // LÀM MỚI
        // =====================================================

        private void BtnLamMoi_Click(
            object sender,
            EventArgs e)
        {
            TaiDuLieu();
        }
    }
}