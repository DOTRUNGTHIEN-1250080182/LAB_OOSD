using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmMain : Form
    {
        private Button btnDanhMuc;
        private Button btnPhongTienNghi;
        private Button btnDatPhong;
        private Button btnDichVu;
        private Button btnTraPhong;
        private Button btnThongKe;
        private Button btnThoat;

        public FrmMain()
        {
            InitializeComponent();

            TaoGiaoDien();

            this.Load += FrmMain_Load;
        }

        // =====================================================
        // FORM LOAD
        // =====================================================
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Khởi tạo Form chính
        }

        // =====================================================
        // TẠO GIAO DIỆN
        // =====================================================
        private void TaoGiaoDien()
        {
            this.Controls.Clear();

            this.Text = "Hệ thống quản lý khách sạn";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 650);
            this.MinimumSize = new Size(900, 550);
            this.BackColor = Color.White;

            // =================================================
            // TIÊU ĐỀ
            // =================================================
            Label lblTieuDe = new Label();

            lblTieuDe.Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN";
            lblTieuDe.Font = new Font(
                "Arial",
                24,
                FontStyle.Bold);

            lblTieuDe.AutoSize = false;
            lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;
            lblTieuDe.Dock = DockStyle.Top;
            lblTieuDe.Height = 100;

            this.Controls.Add(lblTieuDe);

            // =================================================
            // PANEL
            // =================================================
            Panel panel = new Panel();

            panel.Width = 760;
            panel.Height = 400;
            panel.Left = 110;
            panel.Top = 120;

            this.Controls.Add(panel);

            // =================================================
            // DANH MỤC
            // =================================================
            btnDanhMuc = TaoButton(
                "DANH MỤC",
                20,
                20);

            btnDanhMuc.Click += BtnDanhMuc_Click;

            // =================================================
            // PHÒNG & TIỆN NGHI
            // =================================================
            btnPhongTienNghi = TaoButton(
                "PHÒNG & TIỆN NGHI",
                390,
                20);

            btnPhongTienNghi.Click += BtnPhongTienNghi_Click;

            // =================================================
            // ĐẶT PHÒNG
            // =================================================
            btnDatPhong = TaoButton(
                "ĐẶT PHÒNG",
                20,
                140);

            btnDatPhong.Click += BtnDatPhong_Click;

            // =================================================
            // DỊCH VỤ
            // =================================================
            btnDichVu = TaoButton(
                "DỊCH VỤ",
                390,
                140);

            btnDichVu.Click += BtnDichVu_Click;

            // =================================================
            // TRẢ PHÒNG
            // =================================================
            btnTraPhong = TaoButton(
                "TRẢ PHÒNG / THANH TOÁN",
                20,
                260);

            btnTraPhong.Click += BtnTraPhong_Click;

            // =================================================
            // THỐNG KÊ
            // =================================================
            btnThongKe = TaoButton(
                "THỐNG KÊ",
                390,
                260);

            btnThongKe.Click += BtnThongKe_Click;

            // Thêm các nút vào Panel
            panel.Controls.Add(btnDanhMuc);
            panel.Controls.Add(btnPhongTienNghi);
            panel.Controls.Add(btnDatPhong);
            panel.Controls.Add(btnDichVu);
            panel.Controls.Add(btnTraPhong);
            panel.Controls.Add(btnThongKe);

            // =================================================
            // NÚT THOÁT
            // =================================================
            btnThoat = new Button();

            btnThoat.Text = "THOÁT";
            btnThoat.Width = 140;
            btnThoat.Height = 45;

            btnThoat.Left = 420;
            btnThoat.Top = 530;

            btnThoat.Font = new Font(
                "Arial",
                10,
                FontStyle.Bold);

            btnThoat.Cursor = Cursors.Hand;

            btnThoat.Click += BtnThoat_Click;

            this.Controls.Add(btnThoat);
        }

        // =====================================================
        // TẠO BUTTON
        // =====================================================
        private Button TaoButton(
            string text,
            int left,
            int top)
        {
            Button button = new Button();

            button.Text = text;

            button.Width = 330;
            button.Height = 90;

            button.Left = left;
            button.Top = top;

            button.Font = new Font(
                "Arial",
                12,
                FontStyle.Bold);

            button.Cursor = Cursors.Hand;

            return button;
        }

        // =====================================================
        // DANH MỤC
        // =====================================================
        private void BtnDanhMuc_Click(
            object sender,
            EventArgs e)
        {
            FrmDanhMuc form = new FrmDanhMuc();

            form.ShowDialog();
        }

        // =====================================================
        // PHÒNG & TIỆN NGHI
        // =====================================================
        private void BtnPhongTienNghi_Click(object sender, EventArgs e)
        {
            FrmPhongTienNghi form = new FrmPhongTienNghi();
            form.ShowDialog();
        }

        // =====================================================
        // ĐẶT PHÒNG
        // =====================================================
        private void BtnDatPhong_Click(object sender, EventArgs e)
        {
            FrmDatPhong form = new FrmDatPhong();
            form.ShowDialog();
        }

        // =====================================================
        // DỊCH VỤ
        // =====================================================
        private void BtnDichVu_Click(object sender, EventArgs e)
        {
            FrmDichVu form = new FrmDichVu();
            form.ShowDialog();
        }

        // =====================================================
        // TRẢ PHÒNG / THANH TOÁN
        // =====================================================
        private void BtnTraPhong_Click(object sender, EventArgs e)
        {
            FrmTraPhong form = new FrmTraPhong();
            form.ShowDialog();
        }

        // =====================================================
        // THỐNG KÊ
        // =====================================================
        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            FrmThongKe form = new FrmThongKe();
            form.ShowDialog();
        }

        // =====================================================
        // THOÁT
        // =====================================================
        private void BtnThoat_Click(
            object sender,
            EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn thoát chương trình không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}