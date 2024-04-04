using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace giaydepnuna
{
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        private void label5_Click(object sender, EventArgs e)
        {

        }
        public void getData()
        {
            string query = "select* from nhanvien";
            DataTable dt = kn.laydulieu(query);
            dgvnv.DataSource = dt;
            
        }
        public void ClearText()
        {
            txtid.Text = "";
            txtten.Text = "";
            txtdc.Text = "";
            txtsdt.Text = "";
            txtns.Text = "";
            txtcv.Text = "";
            txtnvl.Text = "";
            txttimkiem.Text = "";
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string query = string.Format("insert into nhanvien values('{0}',N'{1}','{2}','{3}','{4}',N'{5}','{6}')",
               txtid.Text,
               txtten.Text,
               txtdc.Text,
               txtsdt.Text,
               txtns.Text,
               txtnvl.Text,
               txtcv.Text);
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Thêm thành công");
                btnlmmoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnlmmoi_Click(object sender, EventArgs e)
        {
            ClearText();
            getData();
        }
      
        private void NhanVien_Load(object sender, EventArgs e)
        {
            getData();
           ChangeName();
        }
      

        private void btnsua_Click(object sender, EventArgs e)
        {
            string query = string.Format("update nhanvien set hoten=N'{1}',dchi=N'{2}',sdt='{3}',ngsinh='{4}',ngvl='{5}',chucvu=N'{6}' where idnv='{0}'",
              txtid.Text,
               txtten.Text,
               txtdc.Text,
               txtsdt.Text,
               txtns.Text,
               txtnvl.Text,
               txtcv.Text);
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Sửa thành công");
                btnlmmoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }
        public void ChangeName()
        {


            dgvnv.Columns[0].HeaderText = "ID Nhân Viên";
            dgvnv.Columns[1].HeaderText = "Họ Tên";
            dgvnv.Columns[2].HeaderText = "Địa chỉ";
            dgvnv.Columns[3].HeaderText = "SĐT";
            dgvnv.Columns[4].HeaderText = "Ngày Sinh";
            dgvnv.Columns[5].HeaderText = "Ngày Vào Làm";
            dgvnv.Columns[6].HeaderText = "Chức Vụ";

        }
        private void btnxoa_Click(object sender, EventArgs e)
        {
            string query = string.Format("delete nhanvien where idnv='{0}'", txtid.Text);
            bool r = kn.thucthi(query);
            if (r)
            {
                MessageBox.Show("Xóa thành công");
                btnlmmoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            HeThong frm=new HeThong();
            frm.Show();
            this.Hide();
        }

        private void dgvnv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtid.Text = dgvnv.Rows[r].Cells[0].Value.ToString();
                txtten.Text = dgvnv.Rows[r].Cells[1].Value.ToString();
                txtdc.Text = dgvnv.Rows[r].Cells[2].Value.ToString();
                txtsdt.Text = dgvnv.Rows[r].Cells[3].Value.ToString();
                txtns.Text = dgvnv.Rows[r].Cells[4].Value.ToString();
                txtnvl.Text = dgvnv.Rows[r].Cells[5].Value.ToString();
                txtcv.Text = dgvnv.Rows[r].Cells[6].Value.ToString();
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string query = string.Format("select* from nhanvien where hoten like N'%{0}%'", txttimkiem.Text);
            DataTable da = kn.laydulieu(query);
            dgvnv.DataSource = da;
            txttimkiem.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvnv.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass MExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                MExcel.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvnv.Columns.Count + 1; i++)
                {
                    MExcel.Cells[1, i] = dgvnv.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvnv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvnv.Columns.Count; j++)
                    {
                        if (dgvnv.Rows[i].Cells[j].Value != null)
                        {
                            MExcel.Cells[i + 2, j + 1] = dgvnv.Rows[i].Cells[j].Value.ToString();
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

        private void txtnvl_ValueChanged(object sender, EventArgs e)
        {
            
            

        }

        private void txttimkiem_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtns_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = txtns.Value;
            DateTime minDate = DateTime.Now.AddYears(-15);


            if (selectedDate > minDate)
            {
                MessageBox.Show("Nhân viên phải có ít nhất 15 tuổi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
    
}
