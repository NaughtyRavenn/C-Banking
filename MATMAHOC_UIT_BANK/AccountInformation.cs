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
    public partial class AccountInformation : Form
    {
        public AccountInformation()
        {
            InitializeComponent();

        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");




       

         

        private void AccountÌnformation_Load(object sender, EventArgs e)
        {
            /*string Full_name = "";
            string CMND = "";
            string Email = "";
            string Sex = "";

            string Account_balance = "";
            string Branch = "";



            con.Open();



            SqlCommand cmd = new SqlCommand("SELECT * FROM User_information Where Account_number = '" + LOGIN.SetValueForAccount_owner + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Full_name = dr["Full_name"].ToString();

                }

            }

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            if (dt1 != null)
            {
                foreach (DataRow dr1 in dt.Rows)
                {
                    Email = dr1["Email"].ToString();
                }

            }
            SqlDataAdapter da2 = new SqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();
            da.Fill(dt1);
            if (dt1 != null)
            {
                foreach (DataRow dr2 in dt.Rows)
                {
                    Sex = dr2["Sex"].ToString();
                }

            }

            SqlDataAdapter da4 = new SqlDataAdapter(cmd);
            DataTable dt4 = new DataTable();
            da.Fill(dt4);
            if (dt1 != null)
            {
                foreach (DataRow dr4 in dt.Rows)
                {
                    Account_balance = dr4["Account_balance"].ToString();
                }

            }
            SqlDataAdapter da5 = new SqlDataAdapter(cmd);
            DataTable dt5 = new DataTable();
            da.Fill(dt5);
            if (dt1 != null)
            {
                foreach (DataRow dr5 in dt.Rows)
                {
                    Branch = dr5["Branch"].ToString();
                }

            }
            SqlDataAdapter da6 = new SqlDataAdapter(cmd);
            DataTable dt6 = new DataTable();
            da.Fill(dt6);
            if (dt1 != null)
            {
                foreach (DataRow dr6 in dt.Rows)
                {
                    CMND = dr6["CMND_CCCD"].ToString();
                }

            }*/
            label27.Text = LOGIN.SetValueForFullname_owner;
            label5.Text = LOGIN.SetValueForCMND_CCCD_owner;
            label30.Text = LOGIN.SetValueForEmail_owner;
            label6.Text = LOGIN.SetValueForSex_owner;
            label4.Text = LOGIN.SetValueForBranch_owner;
            label10.Text = LOGIN.SetValueForAccount_number_owner;
            label2.Text = LOGIN.SetValueForAccount_balance_owner + " VND";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Main m = new Main();
            m.Show();
            Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
