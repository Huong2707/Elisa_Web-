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
    public partial class HoaDon : Form
    {
        DataTable CTHOADON;
        public HoaDon()
        {
            InitializeComponent();
            
            txtTongTien.TextChanged += txtTongTien_TextChanged;

        }
        Connect kn = new Connect();

        /// <summary>
        /// hien thi du lieu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HoaDon_Load(object sender, EventArgs e)
        {

            getidsp();
            getnv();
            getCbbMaHDON();

            btnTaoHD.Enabled = true;
            btnLuuHoaDon.Enabled = false;
            btnHuyHD.Enabled = false;
            btnlammoi.Enabled = true;
            idhd.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenHang.ReadOnly = true;            
            txtdongia.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtTongTien.Text = "0";

            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (idhd.Text != "")
            {
                LoadInfoHoaDon();
                btnHuyHD.Enabled = true;
                btnlammoi.Enabled = true;
            }
            LoadDGV();

        }

        /// <summary>
        /// load du lieu
        /// </summary>
        public void LoadDGV()
        {
            string sql = string.Format(" SELECT CTHOADON.IDSP , GIAYDEP.TENSP,CTHOADON.SoLuong,GIAYDEP.Gia,GIAYDEP.Gia*CTHOADON.SoLuong as ThanhTien FROM CTHOADON,GIAYDEP WHERE CTHOADON.IDHD = N'{0}' AND CTHOADON.IDSP = GIAYDEP.IDSP ", idhd.Text);
            CTHOADON = kn.laydulieu(sql);
            dgvThongtinSP.DataSource = CTHOADON;

            dgvThongtinSP.Columns[0].HeaderText = "Mã hàng";
            dgvThongtinSP.Columns[1].HeaderText = "Tên hàng";
            dgvThongtinSP.Columns[2].HeaderText = "Số lượng";
            dgvThongtinSP.Columns[3].HeaderText = "Đơn giá";
           
        }

        private void dgvThongtinSP_DoubleClick(object sender, EventArgs e)
        {
           
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi,Tenxoa;
            if (CTHOADON.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvThongtinSP.CurrentRow.Cells["IDSP"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvThongtinSP.CurrentRow.Cells["SOLUONG"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvThongtinSP.CurrentRow.Cells["GIA"].Value.ToString());
                sql = "DELETE CTHOADON WHERE IDHD=N'" + idhd.Text + "' AND IDSP = N'" + MaHangxoa + "'";
                kn.RunSQL(sql);
                // Cập nhật lại số lượng cho các mặt hàng
                sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM GIAYDEP WHERE IDSP = N'" + MaHangxoa + "'"));
                slcon = sl + SoLuongxoa;
                sql = "UPDATE GIAYDEP SET SOLUONG =" + slcon + " WHERE IDSP= N'" + MaHangxoa + "'";
                kn.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
               

                tong = Convert.ToDouble(kn.GetFieldValues("SELECT THANHTIEN FROM HOADON WHERE IDHD = N'" + idhd.Text + "'"));
                tongmoi = tong - ThanhTienxoa*SoLuongxoa;
                sql = "UPDATE HOADON SET THANHTIEN =" + tongmoi + " WHERE IDHD = N'" + idhd.Text + "'";
                kn.RunSQL(sql);
                txtTongTien.Text = tongmoi.ToString();
              
                LoadDGV();
            }
        }


        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NGLAP FROM HOADON WHERE IDHD = N'" + idhd.Text + "'";
            dtimeNgayLapHoaDon.Value =DateTime.Parse(kn.GetFieldValues(str)) ;
            str = "SELECT IDNV FROM HOADON WHERE IDHD = N'" + idhd.Text + "'";
            cbBnv.Text = kn.GetFieldValues(str);
            str = "SELECT THANHTIEN FROM HOADON WHERE IDHD = N'" + idhd.Text + "'";
            txtTongTien.Text = kn.GetFieldValues(str);
            
        }



        
        /// <summary>
        /// Fill combo
        /// </summary>


        public void getidsp()
        {
            string query = "select* from giaydep";
            DataTable dt = kn.laydulieu(query);
            cbBsp.DataSource = dt;
            cbBsp.DisplayMember = "IDSP";
            cbBsp.ValueMember = "IDSP";
        }
        public void getnv()
        {
            string query = "select* from nhanvien";
            DataTable dt = kn.laydulieu(query);
            cbBnv.DataSource = dt;
            cbBnv.DisplayMember = "IDNV";
            cbBnv.ValueMember = "IDNV";
            
        }

        public void getCbbMaHDON()
        {
            string query = "select* from HOADON";
            DataTable dt = kn.laydulieu(query);
            cbBoxMaHD.DataSource = dt;
            cbBoxMaHD.DisplayMember = "IDHD";
            cbBoxMaHD.ValueMember = "IDHD";
        }

       


        public void clearText()
        {
            idhd.Text = "";
            txtTenNV.Text = "";
            txtTenHang.Text = "";
            txtTongTien.Text = "";
            idhd.Text = "";
            txtsl.Text = "";
            txtdongia.Text = "";
            cbBoxMaHD.Text = "";
            cbBsp.Text = "";
        }



        




        /// <summary>
        /// button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void btnDong_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            clearText();
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            
            btnHuyHD.Enabled = false;
            btnLuuHoaDon.Enabled = true;
            btnlammoi.Enabled = false;
            btnTaoHD.Enabled = false;
            ResetValues();
            idhd.Text = CreateKey("HDB");
            LoadDGV();

            dgvThongtinSP.ClearSelection();

        }


        private void btnHuyHD_Click(object sender, EventArgs e)
        {
         double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT IDSP,SOLUONG FROM CTHOADON WHERE IDHD = N'" + idhd.Text + "'";
                DataTable tblHang = kn.GetDataToTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM GIAYDEP WHERE IDSP = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "UPDATE GIAYDEP SET SOLUONG =" + slcon + " WHERE IDSP= N'" + tblHang.Rows[hang][0].ToString() + "'";
                    kn.RunSQL(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE CTHOADON WHERE IDHD=N'" + idhd.Text + "'";
                kn.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE HOADON WHERE IDHD=N'" + idhd.Text + "'";
                kn.RunSqlDel(sql);
                ResetValues();
                LoadDGV();
                btnHuyHD.Enabled = false;
                btnlammoi.Enabled = true;
            }

        }




        /// <summary>
        /// Cac Ham xu ly so hoa
        /// </summary>
        /// <param name="tiento"></param>
        /// <returns></returns>

        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }

        private void ResetValues()
        {
            idhd.Text = "";
            dtimeNgayLapHoaDon.Value = DateTime.Now;
            cbBnv.Text = "";         
            txtTongTien.Text = "0";
            
            cbBsp.Text = "";
            txtsl.Text = "";
            
           
        }

        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }


        

        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT IDHD FROM HOADON WHERE IDHD=N'" + idhd.Text + "'";
            if (!kn.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
               /* if (dtimeNgayLapHoaDon.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtimeNgayLapHoaDon.Focus();
                    return;
                }*/
                if (cbBnv.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbBnv.Focus();
                    return;
                }
                sql = "INSERT INTO HOADON(IDHD, IDNV, NGLAP, THANHTIEN) VALUES (N'" + idhd.Text.Trim() + "','" +
                cbBnv.SelectedValue + "','" + dtimeNgayLapHoaDon.Text + "','" + txtTongTien.Text + "')";

                try
                {
                    kn.RunSQL(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // Lưu thông tin của các mặt hàng
            if (cbBsp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBsp.Focus();
                return;
            }
            if ((txtsl.Text.Trim().Length == 0) || (txtsl.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsl.Text = "";
                txtsl.Focus();
                return;
            }
            
            sql = "SELECT IDSP FROM CTHOADON WHERE IDSP = N'" + cbBsp.SelectedValue + "' AND IDHD = N'" + idhd.Text.Trim() + "'";
            if (kn.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cbBsp.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM GIAYDEP WHERE IDSP = N'" +cbBsp.SelectedValue + "'"));
            if (Convert.ToDouble(txtsl.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsl.Text = "";
                txtsl.Focus();
                return;
            }
            sql = "INSERT INTO CTHOADON(IDHD,IDSP,SOLUONG,DONGIA) VALUES(N'" + idhd.Text.Trim() + "',N'" + cbBsp.SelectedValue + "','" + txtsl.Text + "','" + txtdongia.Text +"')";
            try
            {
                kn.RunSQL(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadDGV();
            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLcon = sl - Convert.ToDouble(txtsl.Text);
            sql = "UPDATE GIAYDEP SET SOLUONG =" + SLcon + " WHERE IDSP= N'" + cbBsp.SelectedValue + "'";
        
            if (!double.TryParse(txtsl.Text, out double soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ResetValuesHang();
            btnHuyHD.Enabled = true;
            btnTaoHD.Enabled = true;
            btnlammoi.Enabled = true;

            
            
        }

        private void ResetValuesHang()
        {
            cbBsp.Text = "";
            txtsl.Text = "";
            txtTongTien.Text = "0";
        }



        public string ConvertDateTime(string date)
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }




        private void cbBsp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbBsp.Text == "")
            {
                txtTenHang.Text = "";
                txtdongia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TENSP FROM GIAYDEP WHERE IDSP =N'" + cbBsp.SelectedValue + "'";
            txtTenHang.Text = kn.GetFieldValues(str);
            str = "SELECT GIA FROM GIAYDEP WHERE IDSP =N'" + cbBsp.SelectedValue + "'";
            txtdongia.Text = kn.GetFieldValues(str);

            
        }

        private void cbBnv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbBnv.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select HOTEN from NHANVIEN where IDNV=N'" + cbBnv.SelectedValue + "'";
            txtTenNV.Text = kn.GetFieldValues(str);
        }

        private void btnTimkiemHD_Click(object sender, EventArgs e)
        {
            if (cbBoxMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBoxMaHD.Focus();
                return;
            }
            idhd.Text = cbBoxMaHD.Text;

            string sql;
            sql = string.Format(" SELECT CTHOADON.IDSP , GIAYDEP.TENSP,CTHOADON.SoLuong,GIAYDEP.Gia, GIAYDEP.Gia*CTHOADON.SoLuong as ThanhTien FROM CTHOADON, GIAYDEP WHERE CTHOADON.IDHD = N'{0}' AND CTHOADON.IDSP = GIAYDEP.IDSP ", cbBoxMaHD.Text);
            CTHOADON = kn.laydulieu(sql);
            dgvThongtinSP.DataSource = CTHOADON;

            dgvThongtinSP.Columns[0].HeaderText = "Mã hàng";
            dgvThongtinSP.Columns[1].HeaderText = "Tên hàng";
            dgvThongtinSP.Columns[2].HeaderText = "Số lượng";
            dgvThongtinSP.Columns[3].HeaderText = "Đơn giá";

            string str = $"SELECT THANHTIEN FROM HOADON WHERE IDHD = N'{idhd.Text}'";
            string tong = kn.GetFieldValues(str);
            txtTongTien.Text = tong;

            LoadDGV();
            btnHuyHD.Enabled = true;
            btnLuuHoaDon.Enabled = true;
            btnlammoi.Enabled = true;
            cbBoxMaHD.SelectedIndex = -1;
        }





       

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            string str;
            double tong;
            str = "SELECT SUM(SOLUONG * DONGIA) AS TongTien FROM CTHOADON where IDHD= '" + idhd.Text + "' GROUP BY IDHD ";
            txtTongTien.Text = kn.GetFieldValues(str);

            /*tong = Convert.ToDouble(kn.GetFieldValues("SELECT THANHTIEN FROM CTHOADON WHERE IDHD = N'" + idhd.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtTongTien.Text);*/
            
            double.TryParse(txtTongTien.Text, out tong);
            str = "UPDATE HOADON SET THANHTIEN = '" + tong + "' WHERE IDHD = N'" + idhd.Text + "'";
            try
            {
                kn.RunSQL(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvThongtinSP.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass MExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                MExcel.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvThongtinSP.Columns.Count + 1; i++)
                {
                    MExcel.Cells[1, i] = dgvThongtinSP.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvThongtinSP.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvThongtinSP.Columns.Count; j++)
                    {
                        if (dgvThongtinSP.Rows[i].Cells[j].Value != null)
                        {
                            MExcel.Cells[i + 2, j + 1] = dgvThongtinSP.Rows[i].Cells[j].Value.ToString();
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

        private void dgvThongtinSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

