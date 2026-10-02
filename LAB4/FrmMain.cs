using System;
using System.Drawing;
using System.Windows.Forms;

namespace EShopping
{
    public class FrmMain : Form
    {
        private Panel pnlHeader = new Panel();
        private Panel pnlFooter = new Panel();
        private Panel pnlContent = new Panel();

        private Label lblLogo = new Label();
        private Label lblSubTitle = new Label();

        private TableLayoutPanel tblMenu =
            new TableLayoutPanel();

        private Panel cardKhachHang = new Panel();
        private Panel cardSanPham = new Panel();
        private Panel cardGioHang = new Panel();
        private Panel cardDatHang = new Panel();

        private Button btnThoat = new Button();

        public FrmMain()
        {
            InitializeForm();

            cardKhachHang.Click += cardKhachHang_Click;
            cardSanPham.Click += cardSanPham_Click;
            cardGioHang.Click += cardGioHang_Click;
            cardDatHang.Click += cardDatHang_Click;

            btnThoat.Click += btnThoat_Click;
        }

        // =====================================================
        // FORM
        // =====================================================

        private void InitializeForm()
        {
            this.Text =
                "e-SHOPPING - Hệ thống cửa hàng online";

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.Size =
                new Size(1200, 750);

            this.MinimumSize =
                new Size(1000, 650);

            this.BackColor =
                Color.FromArgb(245, 247, 250);

            // =================================================
            // HEADER
            // =================================================

            pnlHeader.Dock =
                DockStyle.Top;

            pnlHeader.Height =
                120;

            pnlHeader.BackColor =
                Color.FromArgb(35, 70, 115);

            // LOGO

            lblLogo.Text =
                "e-SHOPPING";

            lblLogo.ForeColor =
                Color.White;

            lblLogo.Font =
                new Font(
                    "Segoe UI",
                    28,
                    FontStyle.Bold);

            lblLogo.AutoSize = true;

            lblLogo.Location =
                new Point(40, 20);

            pnlHeader.Controls.Add(lblLogo);

            // SUB TITLE

            lblSubTitle.Text =
                "HỆ THỐNG CỬA HÀNG ONLINE";

            lblSubTitle.ForeColor =
                Color.FromArgb(220, 230, 242);

            lblSubTitle.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Regular);

            lblSubTitle.AutoSize = true;

            lblSubTitle.Location =
                new Point(43, 70);

            pnlHeader.Controls.Add(lblSubTitle);

            // TEXT BÊN PHẢI

            Label lblAdmin =
                new Label();

            lblAdmin.Text =
                "QUẢN TRỊ HỆ THỐNG";

            lblAdmin.ForeColor =
                Color.White;

            lblAdmin.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            lblAdmin.AutoSize = true;

            lblAdmin.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblAdmin.Location =
                new Point(
                    970,
                    48);

            pnlHeader.Controls.Add(lblAdmin);

            this.Controls.Add(pnlHeader);

            // =================================================
            // FOOTER
            // =================================================

            pnlFooter.Dock =
                DockStyle.Bottom;

            pnlFooter.Height =
                42;

            pnlFooter.BackColor =
                Color.FromArgb(35, 42, 52);

            Label lblFooter =
                new Label();

            lblFooter.Text =
                "e-SHOPPING  •  Hệ thống cửa hàng online";

            lblFooter.ForeColor =
                Color.FromArgb(220, 225, 230);

            lblFooter.Font =
                new Font(
                    "Segoe UI",
                    9);

            lblFooter.Dock =
                DockStyle.Fill;

            lblFooter.TextAlign =
                ContentAlignment.MiddleCenter;

            pnlFooter.Controls.Add(lblFooter);

            this.Controls.Add(pnlFooter);

            // =================================================
            // CONTENT
            // =================================================

            pnlContent.Dock =
                DockStyle.Fill;

            pnlContent.BackColor =
                Color.FromArgb(245, 247, 250);

            this.Controls.Add(pnlContent);

            // =================================================
            // TIÊU ĐỀ CONTENT
            // =================================================

            Label lblTitle =
                new Label();

            lblTitle.Text =
                "QUẢN LÝ HỆ THỐNG";

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold);

            lblTitle.ForeColor =
                Color.FromArgb(40, 50, 65);

            lblTitle.AutoSize = true;

            lblTitle.Location =
                new Point(0, 20);

            lblTitle.Dock =
                DockStyle.Top;

            lblTitle.Height =
                50;

            lblTitle.TextAlign =
                ContentAlignment.MiddleCenter;

            pnlContent.Controls.Add(lblTitle);

            // =================================================
            // TABLE MENU
            // =================================================

            tblMenu.Dock =
                DockStyle.None;

            tblMenu.ColumnCount = 2;
            tblMenu.RowCount = 2;

            tblMenu.ColumnStyles.Clear();
            tblMenu.RowStyles.Clear();

            // 2 cột bằng nhau

            tblMenu.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent,
                    50F));

            tblMenu.ColumnStyles.Add(
                new ColumnStyle(
                    SizeType.Percent,
                    50F));

            // 2 dòng bằng nhau

            tblMenu.RowStyles.Add(
                new RowStyle(
                    SizeType.Percent,
                    50F));

            tblMenu.RowStyles.Add(
                new RowStyle(
                    SizeType.Percent,
                    50F));

            tblMenu.Size =
                new Size(900, 330);

            // Căn giữa bảng

            tblMenu.Anchor =
                AnchorStyles.None;

            // =================================================
            // TỌA ĐỘ TABLE
            // =================================================

            pnlContent.Controls.Add(tblMenu);

            // =================================================
            // CARD 1
            // =================================================

            CreateCard(
                cardKhachHang,
                "KH",
                "KHÁCH HÀNG",
                "Quản lý thông tin khách hàng",
                Color.FromArgb(52, 120, 246));

            // =================================================
            // CARD 2
            // =================================================

            CreateCard(
                cardSanPham,
                "SP",
                "SẢN PHẨM",
                "Quản lý thông tin sản phẩm",
                Color.FromArgb(72, 160, 110));

            // =================================================
            // CARD 3
            // =================================================

            CreateCard(
                cardGioHang,
                "GH",
                "GIỎ HÀNG",
                "Quản lý sản phẩm trong giỏ",
                Color.FromArgb(235, 155, 45));

            // =================================================
            // CARD 4
            // =================================================

            CreateCard(
                cardDatHang,
                "DH",
                "ĐẶT HÀNG",
                "Đặt hàng và tính tiền",
                Color.FromArgb(145, 90, 200));

            // =================================================
            // ĐƯA CARD VÀO TABLE
            // =================================================

            tblMenu.Controls.Add(
                cardKhachHang,
                0,
                0);

            tblMenu.Controls.Add(
                cardSanPham,
                1,
                0);

            tblMenu.Controls.Add(
                cardGioHang,
                0,
                1);

            tblMenu.Controls.Add(
                cardDatHang,
                1,
                1);

            // =================================================
            // NÚT THOÁT
            // =================================================

            btnThoat.Text =
                "THOÁT HỆ THỐNG";

            btnThoat.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            btnThoat.ForeColor =
                Color.White;

            btnThoat.BackColor =
                Color.FromArgb(220, 70, 70);

            btnThoat.FlatStyle =
                FlatStyle.Flat;

            btnThoat.FlatAppearance.BorderSize =
                0;

            btnThoat.Size =
                new Size(190, 45);

            btnThoat.Anchor =
                AnchorStyles.None;

            pnlContent.Controls.Add(btnThoat);

            // =================================================
            // ĐẶT VỊ TRÍ TỰ ĐỘNG
            // =================================================

            pnlContent.Resize +=
                pnlContent_Resize;

            // Gọi lần đầu

            pnlContent_Resize(
                null,
                EventArgs.Empty);
        }

        // =====================================================
        // RESIZE CONTENT
        // =====================================================

        private void pnlContent_Resize(
            object sender,
            EventArgs e)
        {
            // -------------------------
            // TABLE
            // -------------------------

            int tableX =
                (pnlContent.ClientSize.Width -
                 tblMenu.Width) / 2;

            int tableY = 85;

            tblMenu.Location =
                new Point(
                    tableX,
                    tableY);

            // -------------------------
            // NÚT THOÁT
            // -------------------------

            int buttonX =
                (pnlContent.ClientSize.Width -
                 btnThoat.Width) / 2;

            int buttonY =
                tableY +
                tblMenu.Height +
                25;

            btnThoat.Location =
                new Point(
                    buttonX,
                    buttonY);
        }

        // =====================================================
        // TẠO CARD
        // =====================================================

        private void CreateCard(
            Panel card,
            string iconText,
            string title,
            string description,
            Color accentColor)
        {
            card.Dock =
                DockStyle.Fill;

            card.Margin =
                new Padding(12);

            card.BackColor =
                Color.White;

            card.BorderStyle =
                BorderStyle.FixedSingle;

            card.Cursor =
                Cursors.Hand;

            // =================================================
            // THANH MÀU
            // =================================================

            Panel accent =
                new Panel();

            accent.Dock =
                DockStyle.Left;

            accent.Width =
                7;

            accent.BackColor =
                accentColor;

            card.Controls.Add(accent);

            // =================================================
            // ICON
            // =================================================

            Label lblIcon =
                new Label();

            lblIcon.Text =
                iconText;

            lblIcon.Font =
                new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold);

            lblIcon.ForeColor =
                Color.White;

            lblIcon.BackColor =
                accentColor;

            lblIcon.TextAlign =
                ContentAlignment.MiddleCenter;

            lblIcon.Size =
                new Size(65, 65);

            lblIcon.Location =
                new Point(30, 30);

            card.Controls.Add(lblIcon);

            // =================================================
            // TITLE
            // =================================================

            Label lblTitle =
                new Label();

            lblTitle.Text =
                title;

            lblTitle.Font =
                new Font(
                    "Segoe UI",
                    14,
                    FontStyle.Bold);

            lblTitle.ForeColor =
                Color.FromArgb(
                    40,
                    50,
                    65);

            lblTitle.AutoSize =
                true;

            lblTitle.Location =
                new Point(115, 32);

            card.Controls.Add(lblTitle);

            // =================================================
            // DESCRIPTION
            // =================================================

            Label lblDescription =
                new Label();

            lblDescription.Text =
                description;

            lblDescription.Font =
                new Font(
                    "Segoe UI",
                    9);

            lblDescription.ForeColor =
                Color.FromArgb(
                    110,
                    120,
                    130);

            lblDescription.AutoSize =
                true;

            lblDescription.Location =
                new Point(115, 70);

            card.Controls.Add(lblDescription);

            // =================================================
            // MŨI TÊN
            // =================================================

            Label lblArrow =
                new Label();

            lblArrow.Text =
                "›";

            lblArrow.Font =
                new Font(
                    "Segoe UI",
                    22,
                    FontStyle.Bold);

            lblArrow.ForeColor =
                accentColor;

            lblArrow.AutoSize =
                true;

            lblArrow.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Right;

            lblArrow.Location =
                new Point(
                    390,
                    42);

            card.Controls.Add(lblArrow);
        }

        // =====================================================
        // KHÁCH HÀNG
        // =====================================================

        private void cardKhachHang_Click(
            object sender,
            EventArgs e)
        {
            FrmKhachHang frm =
                new FrmKhachHang();

            frm.ShowDialog();
        }

        // =====================================================
        // SẢN PHẨM
        // =====================================================

        private void cardSanPham_Click(
            object sender,
            EventArgs e)
        {
            MessageBox.Show(
                "Chức năng quản lý sản phẩm sẽ thực hiện tiếp theo.",
                "Sản phẩm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =====================================================
        // GIỎ HÀNG
        // =====================================================

        private void cardGioHang_Click(
            object sender,
            EventArgs e)
        {
            MessageBox.Show(
                "Chức năng quản lý giỏ hàng sẽ thực hiện tiếp theo.",
                "Giỏ hàng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =====================================================
        // ĐẶT HÀNG
        // =====================================================

        private void cardDatHang_Click(
            object sender,
            EventArgs e)
        {
            MessageBox.Show(
                "Chức năng đặt hàng sẽ thực hiện tiếp theo.",
                "Đặt hàng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =====================================================
        // THOÁT
        // =====================================================

        private void btnThoat_Click(
            object sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    "Bạn có muốn thoát chương trình?",
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