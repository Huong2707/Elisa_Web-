using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace giaydepnuna
{
    public partial class HeThong : Form
    {
        public HeThong()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }
        private Form currentFormChild;
        private void OpendChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelbody.Controls.Add(childForm);
            panelbody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }


        private void HeThong_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnnv_Click(object sender, EventArgs e)
        {
            OpendChildForm(new NhanVien());
            trangchu.Text = "Quản Lý Nhân Viên";
        }

        private void btnncc_Click(object sender, EventArgs e)
        {
            OpendChildForm(new NCC());
            trangchu.Text = "Quản Lý Nhà Cung Cấp";

        }

        private void btnnhap_Click(object sender, EventArgs e)
        {
            OpendChildForm(new PNHang());
            trangchu.Text = "Quản Lý Phiếu Nhập";
        }

        private void btnsp_Click(object sender, EventArgs e)
        {
            OpendChildForm(new SanPham());
            trangchu.Text = "Quản Lý Sản Phẩm";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpendChildForm(new HoaDon());
            trangchu.Text ="Quản Lý Hóa Đơn";
        }

        private void btncaidat_Click(object sender, EventArgs e)
        {
            OpendChildForm(new CaiDat());
            trangchu.Text ="Đổi Mật Khẩu";
        }

        private void btnluong_Click(object sender, EventArgs e)
        {
            OpendChildForm(new Luong());
            trangchu.Text = "Bảng Lương Nhân Viên";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dangnhap frm=new Dangnhap();
            frm.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trangchu_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HeThong frm=new HeThong();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpendChildForm(new ThongKe());
            trangchu.Text = "Thống Kê Doanh Thu Theo Tháng";
        }
    }
}
