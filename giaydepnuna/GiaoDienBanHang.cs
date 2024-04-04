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
    public partial class GiaoDienBanHang : Form
    {
        public GiaoDienBanHang()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GiaoDienBanHang_Load(object sender, EventArgs e)
        {
            string query = "select*from giaydep";
            DataTable dt = kn.laydulieu(query);
            dgvbanhang.DataSource = dt;
        }

        private void txttk_TextChanged(object sender, EventArgs e)
        {
            string query = string.Format("select *from giaydep where tensp  like '%{0}%'", txttk.Text);
            DataTable da = kn.laydulieu(query);
            dgvbanhang.DataSource = da;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HeThongNV frm=new HeThongNV();
            frm.Show();
            this.Hide();
        }
    }
}
