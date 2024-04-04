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
    public partial class DoiMKNV : Form
    {
        public DoiMKNV()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        private void button1_Click(object sender, EventArgs e)
        {
            string query = string.Format(" SELECT* from nguoidung where ACCOUNT='{0}'and PASSWORK='{1}'",
          txttaikhoan.Text,
          txtmkold.Text);
            DataTable dt = kn.laydulieu(query);
            if (dt.Rows.Count == 1)
            {
                string qr = string.Format(" update nguoidung set passwork='{1}' where account='{0}'",
                    txttaikhoan.Text,
                    txtmknew.Text);
                bool r = kn.thucthi(qr);
                if (r)
                {
                    if (txtmknew.Text == txtnhaplai.Text)
                    {
                        MessageBox.Show("Lưu thành công");
                    }
                    else
                    {
                        MessageBox.Show("Lưu thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng vui lòng nhập lại!");
                }
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HeThongNV frm = new HeThongNV();
            frm.Show();
            this.Hide();
        }

        private void DoiMKNV_Load(object sender, EventArgs e)
        {

        }

        private void hienthi_CheckedChanged(object sender, EventArgs e)
        {
            if (hienthi.Checked)
            {

                txtmknew.UseSystemPasswordChar = false;
                txtnhaplai.UseSystemPasswordChar = false;

            }
            else
            {
                txtmknew.UseSystemPasswordChar = true;
                txtnhaplai.UseSystemPasswordChar = true;

            }
        }
    }

}
