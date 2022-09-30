
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
    public partial class Transfer_1 : Form
    {
        public Transfer_1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");
 

    

    
 
        private void Transfer_1_Load_1(object sender, EventArgs e)
        {

            label9.Text = Transfercs.SetValueForamount + " VND";

             

            label2.Text = Transfercs.SetValueForPhone_number_beneficiary;

            label4.Text = Transfercs.SetValueForFull_name_beneficiary;
             


            label13.Text = Transfercs.SetValueForBranch_beneficiary;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Transfer_2 m = new Transfer_2();
            m.Show();
            Visible = false;
            con.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Transfercs m = new Transfercs();
            m.Show();
            Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
