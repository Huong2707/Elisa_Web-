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
    public partial class HeThongNV : Form
    {
        public HeThongNV()
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
            pictureBox16.Controls.Add(childForm);
            pictureBox16.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

       

        private void HeThongNV_Load(object sender, EventArgs e)
        {


        }

      

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                    }

        private void button5_Click(object sender, EventArgs e)
        {
            OpendChildForm(new GiaoDienBanHang());
            trangchu.Text = "Sản Phẩm Có Trong Cửa Hàng";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpendChildForm(new HoaDon());
            trangchu.Text = "Quản Lý Hóa Đơn";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpendChildForm(new DoiMKNV());
            trangchu.Text = "Đổi mật khẩu";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dangnhap frm=new Dangnhap();
            frm.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            HeThongNV frm=new HeThongNV();
            frm.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
