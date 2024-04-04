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
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                Dangnhap frm = new Dangnhap();
                frm.Show();
                this.Hide();
            }
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }
    }
}
