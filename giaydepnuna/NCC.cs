using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*using Microsoft.Office.Interop.Excel;*//*
using app = Microsoft.Office.Interop.Excel.Application;*/

namespace giaydepnuna
{
    public partial class NCC : Form
    {
        public NCC()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        public void getdata()
        {
            string query = "select*from nhacungcap";
            DataTable dt = kn.laydulieu(query);
            dgvncc.DataSource = dt;
        }
        public void clearText()
        {
            txtid.Text = "";
            txtten.Text = "";
            txtdc.Text = "";
            txtsdt.Text = "";
            txtgc.Text = "";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //Close();
            if (dgvncc.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass MExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                MExcel.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvncc.Columns.Count + 1; i++)
                {
                    MExcel.Cells[1, i] = dgvncc.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvncc.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvncc.Columns.Count; j++)
                    {     
                        if (dgvncc.Rows[i].Cells[j].Value!=null)
                        {
                            MExcel.Cells[i + 2, j + 1] = dgvncc.Rows[i].Cells[j].Value.ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            string query = string.Format(" insert into nhacungcap values('{0}',N'{1}',N'{2}','{3}',N'{4}')",
               txtid.Text,
               txtten.Text,
               txtdc.Text,
               txtsdt.Text,
               txtgc.Text);
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

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string query = string.Format("select* from nhacungcap where tenncc like N'%{0}%'", txttk.Text);
            DataTable da = kn.laydulieu(query);
            dgvncc.DataSource = da;
            txttk.Text = "";
        }
        public void ChangeName()
        {
            dgvncc.Columns[0].HeaderText = "ID NCC";
            dgvncc.Columns[1].HeaderText = "Tên NCC";
            dgvncc.Columns[2].HeaderText = "Địa Chỉ";
            dgvncc.Columns[3].HeaderText = "SĐT";
            dgvncc.Columns[4].HeaderText = "Ghi Chú";
        }

        private void NCC_Load(object sender, EventArgs e)
        {
            getdata();
            ChangeName();
        }

        private void dgvncc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtid.Text = dgvncc.Rows[r].Cells[0].Value.ToString();
                txtten.Text = dgvncc.Rows[r].Cells[1].Value.ToString();
                txtdc.Text = dgvncc.Rows[r].Cells[2].Value.ToString();
                txtsdt.Text = dgvncc.Rows[r].Cells[3].Value.ToString();
                txtgc.Text = dgvncc.Rows[r].Cells[4].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getdata();
            clearText();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string query = string.Format("update nhacungcap set  tenncc=N'{1}', dchi=N'{2}',sdt='{3}',ghichu=N'{4}' where idncc='{0}'",
                txtid.Text,
                txtten.Text,
                txtdc.Text,
                txtsdt.Text,
                txtgc.Text);
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

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string query = string.Format("delete nhacungcap where idncc='{0}'", txtid.Text);
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

        private void txttk_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HeThong frm=new HeThong();
            frm.Show();
            this.Hide();
        }
    }
}
