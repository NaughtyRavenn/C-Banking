
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
    public partial class Transfer_3 : Form
    {
        public Transfer_3()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");
       

       

        

        private void Transfer_3_Load_1(object sender, EventArgs e)
        {

            label9.Text = Transfercs.SetValueForamount + " VND";
            label6.Text = Transfercs.SetValueForAcount_beneficiary;
            label3.Text = Transfercs.SetValueForFull_name_beneficiary;
            label13.Text =Transfercs.SetValueForBranch_beneficiary ;
             
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Transfercs m = new Transfercs();
            m.Show();
            Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
