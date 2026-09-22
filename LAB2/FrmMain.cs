using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public class FrmMain : Form
    {
        private Button btnDanhMuc = null!;
        private Button btnSach = null!;
        private Button btnDocGia = null!;
        private Button btnMuonTra = null!;
        private Button btnThongKe = null!;
        private Button btnThoat = null!;

        public FrmMain()
        {
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            Text = "Hệ thống quản lý thư viện";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(900, 600);
            BackColor = Color.White;

            Label lblTitle = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN",
                Font = new Font("Arial", 22, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(50, 40),
                Size = new Size(800, 60)
            };

            Controls.Add(lblTitle);

            btnDanhMuc = TaoButton("DANH MỤC", 150, 150);
            btnSach = TaoButton("SÁCH", 450, 150);
            btnDocGia = TaoButton("ĐỘC GIẢ", 150, 250);
            btnMuonTra = TaoButton("MƯỢN - TRẢ", 450, 250);
            btnThongKe = TaoButton("THỐNG KÊ", 150, 350);
            btnThoat = TaoButton("THOÁT", 450, 350);

            btnDanhMuc.Click += BtnDanhMuc_Click;
            btnSach.Click += BtnSach_Click;
            btnDocGia.Click += BtnDocGia_Click;
            btnMuonTra.Click += BtnMuonTra_Click;
            btnThongKe.Click += BtnThongKe_Click;
            btnThoat.Click += BtnThoat_Click;
        }

        private Button TaoButton(string text, int x, int y)
        {
            Button button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(280, 65),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            Controls.Add(button);
            return button;
        }

        private void BtnDanhMuc_Click(object? sender, EventArgs e)
        {
            using FrmDanhMuc frm = new FrmDanhMuc();
            frm.ShowDialog();
        }

        private void BtnSach_Click(object? sender, EventArgs e)
        {
            using FrmSach frm = new FrmSach();
            frm.ShowDialog();
        }

        private void BtnDocGia_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Quản lý độc giả sẽ được xây dựng ở bước tiếp theo.",
                "Thông báo");
        }

        private void BtnMuonTra_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Mượn - Trả sẽ được xây dựng ở bước tiếp theo.",
                "Thông báo");
        }

        private void BtnThongKe_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng Thống kê sẽ được xây dựng ở bước tiếp theo.",
                "Thông báo");
        }

        private void BtnThoat_Click(object? sender, EventArgs e)
        {
            Close();
        }
    }
}