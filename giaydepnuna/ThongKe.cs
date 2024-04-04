using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace giaydepnuna
{
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
        }
        Connect kn=new Connect();
        private void ThongKe_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=NGUYENTHITHUHUO\\SQLEXPRESS;Initial Catalog=DOANVEGIAYDEP;Integrated Security=True");

            // Fill dữ liệu từ bảng phieunhapkho
            SqlDataAdapter da = new SqlDataAdapter("SELECT MONTH(nglap) AS Thang, SUM(tongtiennhap) AS Tong FROM phieunhapkho GROUP BY MONTH(nglap)", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Gán dữ liệu vào Series1
            chart1.Series["Hàng Nhập"].Points.DataBind(dt.DefaultView, "Thang", "Tong","");

            // Fill dữ liệu từ bảng hoadon
            SqlDataAdapter dp = new SqlDataAdapter("SELECT MONTH(nglap) AS Thang, SUM(thanhtien) AS Tong FROM hoadon GROUP BY MONTH(nglap)", con);
            DataTable d = new DataTable();
            dp.Fill(d);

            // Gán dữ liệu vào Series2
            chart1.Series["Hàng Xuất"].Points.DataBind(d.DefaultView, "Thang", "Tong", "");

            // Cài đặt các thuộc tính của Chart
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Tháng";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Tổng Tiền ";

            // Đóng kết nối
            con.Close();


        }
    }
}
