using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace giaydepnuna
{
    internal class Connect
    {
        private String conSql = @"Data Source=NGUYENTHITHUHUO\SQLEXPRESS;Initial Catalog=DOANVEGIAYDEP;Integrated Security=True";
        private SqlConnection con;
        SqlDataReader reader;
        public Connect()
        {
            con = new SqlConnection(conSql);
        }
        public DataTable laydulieu(string truyvan)
        {
            SqlDataAdapter da = new SqlDataAdapter(truyvan, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public bool thucthi(string truyvan)
        {
            try
            {
                con.Open();
                SqlCommand cm = new SqlCommand(truyvan, con);
                int r = cm.ExecuteNonQuery();
                con.Close();
                return r > 0;

            }
            catch
            {
                return false;
            }

        }

        public  string GetFieldValues(string sql)
        {
            con.Open();
            string ma = " ";         
            SqlCommand cmd = new SqlCommand(sql, con);              
            reader = cmd.ExecuteReader();

            while (reader.Read())
            ma = reader.GetValue(0).ToString();
            
            con.Close();
            return ma;

        }

        //Hàm kiểm tra khoá trùng
        public  bool CheckKey(string sql)
        {
            con.Open();
            SqlDataAdapter dap = new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            dap.Fill(table);
            con.Close();
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

        //Hàm thực hiện câu lệnh SQL
        public void RunSQL(string sql)
        {
            con.Open();
            SqlCommand cmd; //Đối tượng thuộc lớp SqlCommand
            cmd = new SqlCommand();
            cmd.Connection = con; //Gán kết nối
            cmd.CommandText = sql; //Gán lệnh SQL
           
                cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
           
            //cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;
            con.Close();
        }


        //Lấy dữ liệu vào bảng
        public DataTable GetDataToTable(string sql)
        {
            con.Open();
            SqlDataAdapter dap = new SqlDataAdapter(); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Tạo đối tượng thuộc lớp SqlCommand
            dap.SelectCommand = new SqlCommand();
            dap.SelectCommand.Connection = con; //Kết nối cơ sở dữ liệu
            dap.SelectCommand.CommandText = sql; //Lệnh SQL
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            dap.Fill(table);
            con.Close();
            return table;
        }

        public  void RunSqlDel(string sql)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Dữ liệu đang được dùng, không thể xoá...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
            con.Close();
        }

    }
}
