
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Text.RegularExpressions;

namespace MATMAHOC_UIT_BANK
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

       
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");

        

        

        private void button3_Click_1(object sender, EventArgs e)
        {
            LOGIN m = new LOGIN();
            m.Show();
            Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AccountInformation q = new AccountInformation();
            q.Show();
            Visible = false;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Transfercs m = new Transfercs();
            m.Show();
            Visible = false;
        }

        private void Main_Load_1(object sender, EventArgs e)
        {
            label4.Text = LOGIN.SetValueForFullname_owner;
        }
    }
}
