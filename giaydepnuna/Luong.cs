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
    public partial class Luong : Form
    {
        public Luong()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        public void GetData()
        {
            string query = "select idnv,thang,ngcong,thuong,ngcong*300000+thuong as luong from luong";
            DataTable dt=kn.laydulieu(query);
            dgvluong.DataSource = dt;
        }
        public void clearText()
        {
            txtngaycong.Text = "";
            txtthang.Text = "";
            txtthuong.Text = "";
        }
        public void getNV()
        {
            string query = "select* from nhanvien";
            DataTable dt = kn.laydulieu(query);
            cbidnv.DataSource = dt;
            cbidnv.DisplayMember = "IDNV";
            cbidnv.ValueMember = "IDNV";

        }
        public void getThang()
        {
            string query = "select* from LUONG";
            DataTable dt = kn.laydulieu(query);
            cbBThang.DataSource = dt;
            cbBThang.DisplayMember = "THANG";
            cbBThang.ValueMember = "THANG";

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = string.Format("insert into luong values('{0}','{1}','{2}','{3}')",
               cbidnv.SelectedValue,
               txtthang.Text,
                txtngaycong.Text,
                txtthuong.Text);
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Thêm thành công");
                btnlmmoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }

        }
        public void ChangName()
        {
            dgvluong.Columns[0].HeaderText = "ID NV";
            dgvluong.Columns[1].HeaderText = "Tháng";
            dgvluong.Columns[2].HeaderText = "Ngày Công";
            dgvluong.Columns[3].HeaderText = "Thưởng";
            dgvluong.Columns[4].HeaderText = "Lương";
        }

        private void Luong_Load(object sender, EventArgs e)
        {
            getNV();
            GetData();
            ChangName();
            getThang();
        }

        private void dgvluong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                cbidnv.SelectedValue = dgvluong.Rows[r].Cells[0].Value.ToString();
                txtthang.Text = dgvluong.Rows[r].Cells[1].Value.ToString();
                txtngaycong.Text = dgvluong.Rows[r].Cells[2].Value.ToString();
                txtthuong.Text = dgvluong.Rows[r].Cells[3].Value.ToString();   

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearText();
            GetData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = string.Format("update luong set ngcong='{2}',thuong='{3}' where idnv='{0}'and thang='{1}'",
               cbidnv.SelectedValue,
                 txtthang.Text,
                 txtngaycong.Text,
                 txtthuong.Text
                );
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Sửa thành công");
                btnlmmoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
         
        }

        private void txtthang_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvluong.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass MExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                MExcel.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvluong.Columns.Count + 1; i++)
                {
                    MExcel.Cells[1, i] = dgvluong.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvluong.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvluong.Columns.Count; j++)
                    {
                        if (dgvluong.Rows[i].Cells[j].Value != null)
                        {
                            MExcel.Cells[i + 2, j + 1] = dgvluong.Rows[i].Cells[j].Value.ToString();
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            HeThong frm = new HeThong();
            frm.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string sql = "select LUONG.IDNV,LUONG.NGCONG,LUONG.THANG,LUONG.THUONG, NGCONG*300000+THUONG as Luong from LUONG, NHANVIEN where LUONG.IDNV = NHANVIEN.IDNV and HOTEN = '" + txtTenNV.Text + "' and THANG='" + cbBThang.Text + "'";
            DataTable dt = kn.laydulieu(sql);
            dgvluong.DataSource = dt;
        }
    }
}
