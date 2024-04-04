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
    public partial class PNHang : Form
    {
        DataTable CTPHIEUNHAPKHO;
        public PNHang()
        {
            InitializeComponent();
        }
        Connect kn = new Connect();
        public void getidsp()
        {
            string query = "select* from giaydep";
            DataTable dt = kn.laydulieu(query);
            cmbsp.DataSource = dt;
            cmbsp.DisplayMember = "IDSP";
            cmbsp.ValueMember = "IDSP";
        }
        public void getidncc()
        {
            string query = "select* from nhacungcap";
            DataTable dt = kn.laydulieu(query);
            cmbncc.DataSource = dt;
            cmbncc.DisplayMember = "IDNCC";
            cmbncc.ValueMember = "IDNCC";
        }
        public void getnv()
        {
            string query = "select* from nhanvien";
            DataTable dt = kn.laydulieu(query);
            cmbnv.DataSource = dt;
            cmbnv.DisplayMember = "IDNV";
            cmbnv.ValueMember = "IDNV";
        }
        public string ConvertDateTime(string date)// định dạng lại ngày tháng năm
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }
        public void Clear()
        {
            txtpn.Text = "";
            txtnl.Text = "";
            txtsl.Text = "";
            txtten.Text = "";
            txttien.Text = "";
            cmbncc.Text = "";
            cmbnv.Text = "";
            cmbsp.Text = "";
            txttk.Text = "";
            txtdongia.Text = "";
        }
        public void getCbbMaPNK()
        {
            string query = "select* from PHIEUNHAPKHO";
            DataTable dt = kn.laydulieu(query);
            txttk.DataSource = dt;
            txttk.DisplayMember = "IDPNK";
            txttk.ValueMember = "IDPNK";
        }
        private void PNHang_Load(object sender, EventArgs e)
        {
            getidsp();
            getidncc();
            getnv();
            getCbbMaPNK();
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnlmmoi.Enabled = true;
            txtpn.ReadOnly = true;
            txtten.ReadOnly = true;
            txtdongia.ReadOnly = true;
            txttien.ReadOnly = true;
            if (txtpn.Text != "")
            {
                LoadInfoHoaDon();
                btnHuy.Enabled = true;
                btnlmmoi.Enabled = true;
            }
            LoadDGV();
        }
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NGLAP FROM PHIEUNHAPKHO WHERE IDPNK = N'" + txtpn.Text + "'";
            txtnl.Value = DateTime.Parse(kn.GetFieldValues(str));
            str = "SELECT IDNV FROM PHIEUNHAPKHO WHERE IDPNK = N'" + txtpn.Text + "'";
            cmbnv.Text = kn.GetFieldValues(str);
            str = "SELECT TONGTIENNHAP FROM HOADON WHERE IDPNK = N'" + txtpn.Text + "'";
            txttien.Text = kn.GetFieldValues(str);
            /* lblBangchu.Text = "Bằng chữ: " + ChuyenSoSangChu(txtTongTien.Text);*/
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
            txtpn.Text = "";
            txtnl.Value = DateTime.Now;
            cmbncc.Text = "";
            /* lblBangchu.Text = "Bằng chữ: ";*/
            cmbnv.Text = "";
            cmbsp.Text = "";
            txttien.Text = "";
            txtsl.Text = "";
            txtdongia.Text = "";

        }
        public void LoadDGV()
        {
            string sql = string.Format(" SELECT CTPHIEUNHAPKHO.IDSP , GIAYDEP.TENSP,CTPHIEUNHAPKHO.SOLUONG,GIAYDEP.GIA, GIAYDEP.GIA*CTPHIEUNHAPKHO.SOLUONG as ThanhTien\r\nFROM CTPHIEUNHAPKHO, GIAYDEP\r\nWHERE CTPHIEUNHAPKHO.IDPNK = N'{0}'\r\nAND CTPHIEUNHAPKHO.IDSP = GIAYDEP.IDSP ", txtpn.Text);
            CTPHIEUNHAPKHO = kn.laydulieu(sql);
            dgvctphieunhap.DataSource = CTPHIEUNHAPKHO;

            dgvctphieunhap.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvctphieunhap.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvctphieunhap.Columns[2].HeaderText = "Số Lượng";
            dgvctphieunhap.Columns[3].HeaderText = "Đơn Giá";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = false;
            btnLuu.Enabled = true;
            btnlmmoi.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtpn.Text = CreateKey("PN");
            LoadDGV();
        }

        private void cmbsp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cmbsp.Text == "")
            {
                txtten.Text = "";
                txtdongia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TENSP FROM GIAYDEP WHERE IDSP =N'" + cmbsp.SelectedValue + "'";
            txtten.Text = kn.GetFieldValues(str);
            str = "SELECT GIA FROM GIAYDEP WHERE IDSP =N'" + cmbsp.SelectedValue + "'";
            txtdongia.Text = kn.GetFieldValues(str);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLmoi, tong, Tongmoi;
            sql = "SELECT IDPNK FROM PHIEUNHAPKHO WHERE IDPNK=N'" + txtpn.Text + "'";
            if (!kn.CheckKey(sql))
            {
                if (cmbnv.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải chọn mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbnv.Focus();//khi hiển thị ra thông báo thì chuột sẽ điều hướng đến cmbnv để mk chọn
                    return;
                }

                string idpnk = txtpn.Text.Trim();
                if (!kn.CheckKey("SELECT IDPNK FROM PHIEUNHAPKHO WHERE IDPNK = N'" + idpnk + "'"))
                {
                    // Thực hiện thêm giá trị mới vào bảng PHIEUNHAPKHO
                    sql = "INSERT INTO PHIEUNHAPKHO(IDPNK, NGLAP, IDNV, IDNCC, TONGTIENNHAP) VALUES(N'" + idpnk + "', '" + txtnl.Text + "', '" + cmbnv.SelectedValue + "', '" + cmbncc.Text + "', '" + txttien.Text + "')";
                    // Tiếp tục với thực thi câu lệnh SQL...
                }
                else
                {
                    MessageBox.Show("Giá trị IDPNK đã tồn tại.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    kn.RunSQL(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (cmbsp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbsp.Focus();
                return;
            }
            if ((txtsl.Text.Trim().Length == 0) || (txtsl.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsl.Text = "";
                txtsl.Focus();
                return;
            }
            sql = "SELECT IDSP FROM CTPHIEUNHAPKHO WHERE IDSP = N'" + cmbsp.SelectedValue + "' AND IDPNK = N'" + txtpn.Text.Trim() + "'";
            if (kn.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cmbsp.Focus();
                return;
            }
           

            double gia;
            if (double.TryParse(txtdongia.Text, out gia))
            {
                sql = "INSERT INTO CTPHIEUNHAPKHO(IDPNK, IDSP, SOLUONG, DONGIA) VALUES ('" + txtpn.Text.Trim() + "', '" + cmbsp.SelectedValue + "', '" + txtsl.Text + "', " + gia.ToString() + ")";
                // Tiếp tục xử lý câu lệnh SQL và các thao tác khác...
            }
            else
            {
                MessageBox.Show("Giá không hợp lệ", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                kn.RunSQL(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadDGV();
            sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM GIAYDEP WHERE IDSP = N'" + cmbsp.SelectedValue + "'"));
            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLmoi = sl - Convert.ToDouble(txtsl.Text);
            sql = "UPDATE GIAYDEP SET SOLUONG =" + SLmoi + " WHERE IDSP= N'" + cmbsp.SelectedValue + "'";

            /*// Cập nhật lại tổng tiền cho phiếu nhập
            tong = Convert.ToDouble(kn.Get1GTRI("SELECT DONGIA FROM CTPHIEUNHAPKHO WHERE IDPNK = N'" + txtpn.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txttien.Text);
            sql = "UPDATE PHIEUNHAPKHO SET TONGTIENNHAP = " + Tongmoi + " WHERE IDPNK = N'" + txtpn.Text + "'";
            try
            {
                kn.RunSQL(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/

            ResetValuesHang();
            btnHuy.Enabled = true;
            btnThem.Enabled = true;
            btnlmmoi.Enabled = true;

        }
        private void ResetValuesHang()
        {
            cmbsp.Text = "";
            txtsl.Text = "";
            txttien.Text = "0";
        }

        private void txttien_Click(object sender, EventArgs e)
        {

        }

        private void txttien_TextChanged(object sender, EventArgs e)
        {

            string str;
            double tong;
            str = "SELECT SUM(SOLUONG * DONGIA) AS TongTien FROM CTPHIEUNHAPKHO where IDPNK= '" + txtpn.Text + "' GROUP BY IDPNK ";
            txttien.Text = kn.GetFieldValues(str);

            /*tong = Convert.ToDouble(kn.GetFieldValues("SELECT THANHTIEN FROM CTHOADON WHERE IDHD = N'" + idhd.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtTongTien.Text);*/

            double.TryParse(txttien.Text, out tong);
            str = "UPDATE PHIEUNHAPKHO SET TONGTIENNHAP = '" + tong + "' WHERE IDPNK = N'" + txtpn.Text + "'";
            try
            {
                kn.RunSQL(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi SQL: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvctphieunhap_DoubleClick(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi, Tenxoa;
            if (CTPHIEUNHAPKHO.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvctphieunhap.CurrentRow.Cells["IDSP"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvctphieunhap.CurrentRow.Cells["SOLUONG"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvctphieunhap.CurrentRow.Cells["GIA"].Value.ToString());
                sql = "DELETE CTPHIEUNHAPKHO WHERE IDPNK=N'" + txtpn.Text + "' AND IDSP = N'" + MaHangxoa + "'";
                kn.RunSQL(sql);
                // Cập nhật lại số lượng cho các mặt hàng
                sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM GIAYDEP WHERE IDSP = N'" + MaHangxoa + "'"));
                slcon = sl - SoLuongxoa;
                sql = "UPDATE GIAYDEP SET SOLUONG =" + slcon + " WHERE IDSP= N'" + MaHangxoa + "'";
                kn.RunSQL(sql);
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(kn.GetFieldValues("SELECT TONGTIENNHAP FROM PHIEUNHAPKHO WHERE IDPNK = N'" + txtpn.Text + "'"));
                tongmoi = tong - ThanhTienxoa * SoLuongxoa;
                sql = "UPDATE PHIEUNHAPKHO SET TONGTIENNHAP =" + tongmoi + " WHERE IDPNK = N'" + txtpn.Text + "'";
                kn.RunSQL(sql);
                txttien.Text = tongmoi.ToString();

                LoadDGV();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT IDSP,SOLUONG FROM CTPHIEUNHAPKHO WHERE IDPNK = N'" + txtpn.Text + "'";
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
                sql = "DELETE CTPHIEUNHAPKHO WHERE IDPNK=N'" + txtpn.Text + "'";
                kn.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETE PHIEUNHAPKHO WHERE IDPNK=N'" + txtpn.Text + "'";
                kn.RunSqlDel(sql);
                ResetValues();
                LoadDGV();
                btnHuy.Enabled = false;
                btnlmmoi.Enabled = true;
            }
        }

        private void btnlmmoi_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (txttk.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttk.Focus();
                return;
            }
            txtpn.Text = txttk.Text;

            string sql;
            sql = string.Format(" SELECT CTPHIEUNHAPKHO.IDSP , GIAYDEP.TENSP,CTPHIEUNHAPKHO.SOLUONG,GIAYDEP.GIA, GIAYDEP.GIA*CTPHIEUNHAPKHO.SOLUONG as ThanhTien FROM CTPHIEUNHAPKHO ,GIAYDEP WHERE CTPHIEUNHAPKHO.IDPNK = N'{0}' AND CTPHIEUNHAPKHO.IDSP = GIAYDEP.IDSP", txtpn.Text);
            CTPHIEUNHAPKHO = kn.laydulieu(sql);
            dgvctphieunhap.DataSource = CTPHIEUNHAPKHO;

            dgvctphieunhap.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvctphieunhap.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvctphieunhap.Columns[2].HeaderText = "Số Lượng";
            dgvctphieunhap.Columns[3].HeaderText = "Đơn Giá";

            string str = $"SELECT TONGTIENNHAP FROM PHIEUNHAPKHO WHERE IDPNK = N'{txtpn.Text}'";
            string tong = kn.GetFieldValues(str);
            txttien.Text = tong;

            LoadDGV();
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnlmmoi.Enabled = true;
            txttk.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvctphieunhap.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass MExcel = new Microsoft.Office.Interop.Excel.ApplicationClass();
                MExcel.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvctphieunhap.Columns.Count + 1; i++)
                {
                    MExcel.Cells[1, i] = dgvctphieunhap.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvctphieunhap.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvctphieunhap.Columns.Count; j++)
                    {
                        if (dgvctphieunhap.Rows[i].Cells[j].Value != null)
                        {
                            MExcel.Cells[i + 2, j + 1] = dgvctphieunhap.Rows[i].Cells[j].Value.ToString();
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbncc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvctphieunhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvctphieunhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
