using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace giaydepnuna
{
    public partial class Dangnhap : Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            if (rdql.Checked)
            {
             string query = string.Format(" SELECT* from quanly where ACCOUNT='{0}'and PASSWORK='{1}'",
             txttk.Text,
             txtmk.Text);
                DataTable dt = kn.laydulieu(query);
                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    HeThong frm = new HeThong();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại!");
                }
            }
            if (rdnv.Checked)
            {
             string truyvan = string.Format(" SELECT* from nguoidung where ACCOUNT='{0}'and PASSWORK='{1}'",
             txttk.Text,
             txtmk.Text);
                DataTable dt = kn.laydulieu(truyvan);
                if (dt.Rows.Count == 1)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    HeThongNV frm = new HeThongNV();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rdnv_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            

            if (hienthimk.Checked)
            {
               
                txtmk.UseSystemPasswordChar = false;
                
            }
            else
            {
                txtmk.UseSystemPasswordChar = true;
               
            }
            

        }

        private void txtmk_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rdql_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
