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
    public partial class SanPham : Form
    {
        public SanPham()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        public void getData()
        {
            string query = "select*from giaydep";
            DataTable dt = kn.laydulieu(query);
            dgvsp.DataSource = dt;
        }
        public void ClearText()
        {
            txtsp.Text = "";
            txttensp.Text = "";
            txtsl.Text = "";
            txtloai.Text = "";
            txtkichco.Text = "";
            txtghichu.Text = "";
            txtgia.Text = "";
        }
        public void getidncc()
        {
            string query = "select*from nhacungcap";
            DataTable dt = kn.laydulieu(query);
            idncc.DataSource = dt;
            idncc.DisplayMember = "IDNCC";
            idncc.ValueMember = "IDNCC";

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvsp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtsp.Text = dgvsp.Rows[r].Cells[0].Value.ToString();
                txttensp.Text = dgvsp.Rows[r].Cells[1].Value.ToString();
                txtsl.Text = dgvsp.Rows[r].Cells[2].Value.ToString();
                txtloai.Text = dgvsp.Rows[r].Cells[3].Value.ToString();
                txtkichco.Text = dgvsp.Rows[r].Cells[4].Value.ToString();
                txtghichu.Text = dgvsp.Rows[r].Cells[5].Value.ToString();
                txtgia.Text = dgvsp.Rows[r].Cells[6].Value.ToString();
                idncc.SelectedValue = dgvsp.Rows[r].Cells[7].Value.ToString();

            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string query = string.Format("insert into giaydep values('{0}', N'{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
               txtsp.Text,
               txttensp.Text,
               txtsl.Text,
               txtloai.Text,
               txtkichco.Text,
               txtghichu.Text,
               txtgia.Text,
               idncc.SelectedValue);
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Thêm thành công");
                btnlammoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
           
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            ClearText();
            getData();
        }


        public void ChangeName()
        {
            dgvsp.Columns[0].HeaderText = "ID SP";
            dgvsp.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvsp.Columns[2].HeaderText = "Số Lượng";
            dgvsp.Columns[3].HeaderText = "Loại";
            dgvsp.Columns[4].HeaderText = "Kích Cỡ";
            dgvsp.Columns[5].HeaderText = "Ghi Chú";
            dgvsp.Columns[6].HeaderText = "Giá";
            dgvsp.Columns[7].HeaderText = "ID NCC";
           
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            getData();
            getidncc();
            ChangeName();

        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string query = string.Format("update giaydep set tensp=N'{1}',soluong='{2}',loai=N'{3}',kichco='{4}',ghichu=N'{5}',gia='{6}',idncc='{7}' where idsp='{0}'",
               txtsp.Text,
               txttensp.Text,
               txtsl.Text,
               txtloai.Text,
               txtkichco.Text,
               txtghichu.Text,
               txtgia.Text,
               idncc.SelectedValue);
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Sửa thành công");
                btnlammoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HeThong frm= new HeThong();
            frm.Show();
            this.Hide();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string query = string.Format("delete giaydep where idsp='{0}'", txtsp.Text);
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Xóa thành công");
                btnlammoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string query = string.Format("select *from giaydep where tensp  like '%{0}%'", txttk.Text);
            DataTable da = kn.laydulieu(query);
            dgvsp.DataSource = da;
        }

        private void idncc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvsp.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass MExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                MExcel.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvsp.Columns.Count + 1; i++)
                {
                    MExcel.Cells[1, i] = dgvsp.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvsp.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvsp.Columns.Count; j++)
                    {
                        if (dgvsp.Rows[i].Cells[j].Value != null)
                        {
                            MExcel.Cells[i + 2, j + 1] = dgvsp.Rows[i].Cells[j].Value.ToString();
                        }


                    }
                }
                MExcel.Columns.AutoFit();
                MExcel.Rows.AutoFit();
                MExcel.Columns.Font.Size = 12;
                MExcel.Visible = true;
            }
            else
            {
                MessageBox.Show("No records found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
