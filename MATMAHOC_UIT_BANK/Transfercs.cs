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
using System.Security.Cryptography;
 
using SHA3.Net;//thhu vien hash imporrt 2.00 vao 1 lan trong packets trong o D
using System.IO;
namespace MATMAHOC_UIT_BANK
{
    public partial class Transfercs : Form
    {
        



           public static string SetValueForFull_name_beneficiary =  "";
           public static string SetValueForBranch_beneficiary =  "";


        public static string SetValueForPhone_number_beneficiary = "";
        public static string SetValueForAcount_beneficiary = "";
        public static string SetValueForPhone_beneficiary1 = "";
        public static long SetValueForBalance_owner = 0;
        public static long SetValueForBalance_beneficiary = 0;
        public static long SetValueForamount = 0;
        public Transfercs()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");

        SqlConnection con1 = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");




        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null)/*|| cipherText.Length <= 0)*/
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }







        private void button1_Click(object sender, EventArgs e)
        {
            string AccountCodePattern = @"^[0-9]\d{9}$";
            string Account2CodePattern = @"^[0-9]\d{12}$";
            bool isaccount2Valid = Regex.IsMatch(textBox1.Text, Account2CodePattern);
            bool isaccountValid = Regex.IsMatch(textBox1.Text, AccountCodePattern);
            string AmountCodePattern = "[0-9]$";
            bool isAmountValid = Regex.IsMatch(textBox2.Text, AmountCodePattern);

            string Account_number = "";
            string Full_name = "";
            string Account_numberphone = "";
            string Account_balance= "";
            string Key_admin = "";
            string Branch = "";

            byte[] encryptedPhoneorAccount_number;

            string Full_name_Decrypt;
            string Sex_Decrypt;
            string Email_Decrypt;
            string Account_balance_Decrypt;
            string CMND_CCCD_Decrypt;
            string Branch_Decrypt;
            string Phone_number_Decrypt;
            string Account_number_Decrypt;





            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Key_Admin Where Account_number = '9710010910511'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Key_admin = dr["Key_Admin"].ToString();
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi !");

            }
            finally
            {
                con.Close();
            }

            byte[] key_admin_byte = new byte[Key_admin.Length / 2];
            for (int i = 0, h = 0; h < Key_admin.Length; i++, h += 2)
            {
                key_admin_byte[i] = (byte)Int32.Parse(Key_admin.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }


            

            con.Close();


            using (Aes myAes1 = Aes.Create())
            {



                byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                myAes1.Key = key_admin_byte;

                myAes1.IV = iv;
                // Encrypt the string to an array of bytes.

                encryptedPhoneorAccount_number = EncryptStringToBytes_Aes(textBox1.Text, myAes1.Key, myAes1.IV);


            }

            if (textBox1.Text.Length == 10 )
            {

                
            


          
      con.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM User_information Where Phone_number = '" + BitConverter.ToString(encryptedPhoneorAccount_number).Replace("-", "") + "'", con);


                SqlDataAdapter da4 = new SqlDataAdapter(cmd2);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Account_balance = dr4["Account_balance"].ToString();
                    }

                }
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Full_name = dr4["Full_name"].ToString();
                    }

                }
                SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
                DataTable dt3 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Branch = dr4["Branch"].ToString();
                    }

                }
                SqlDataAdapter da13 = new SqlDataAdapter(cmd2);
                DataTable dt13 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Account_numberphone = dr4["Phone_number"].ToString();
                    }

                }
                SqlDataAdapter da14 = new SqlDataAdapter(cmd2);
                DataTable dt14 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Account_number = dr4["Account_number"].ToString();
                    }

                }
                con.Close();

                byte[] Account_number_byte = new byte[Account_number.Length / 2];
                for (int i = 0, h = 0; h < Account_number.Length; i++, h += 2)
                {
                    Account_number_byte[i] = (byte)Int32.Parse(Account_number.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }
                byte[] Branch_byte = new byte[Branch.Length / 2];
                for (int i = 0, h = 0; h < Branch.Length; i++, h += 2)
                {
                    Branch_byte[i] = (byte)Int32.Parse(Branch.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }
                byte[] Full_name_byte = new byte[Full_name.Length / 2];
                for (int i = 0, h = 0; h < Full_name.Length; i++, h += 2)
                {
                    Full_name_byte[i] = (byte)Int32.Parse(Full_name.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }

                byte[] Account_balance_byte = new byte[Account_balance.Length / 2];
                for (int i = 0, h = 0; h < Account_balance.Length; i++, h += 2)
                {
                    Account_balance_byte[i] = (byte)Int32.Parse(Account_balance.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }
                byte[] Phone_number_byte = new byte[Account_numberphone.Length / 2];
                for (int i = 0, h = 0; h < Account_numberphone.Length; i++, h += 2)
                {
                    Phone_number_byte[i] = (byte)Int32.Parse(Account_numberphone.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }
                using (Aes myAes1 = Aes.Create())
                {



                    byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                    myAes1.Key = key_admin_byte;

                    myAes1.IV = iv;
                    // Encrypt the string to an array of bytes.

                    Account_balance_Decrypt = DecryptStringFromBytes_Aes(Account_balance_byte, myAes1.Key, myAes1.IV);
                    Branch_Decrypt = DecryptStringFromBytes_Aes(Branch_byte, myAes1.Key, myAes1.IV);
                    Full_name_Decrypt = DecryptStringFromBytes_Aes(Full_name_byte, myAes1.Key, myAes1.IV);
                    Phone_number_Decrypt = DecryptStringFromBytes_Aes(Phone_number_byte, myAes1.Key, myAes1.IV);
                    Account_number_Decrypt = DecryptStringFromBytes_Aes(Account_number_byte, myAes1.Key, myAes1.IV);
                }


                SetValueForBalance_beneficiary = long.Parse(Account_balance_Decrypt);
                SetValueForamount = long.Parse(textBox2.Text);
                SetValueForAcount_beneficiary = Account_number_Decrypt;
                SetValueForFull_name_beneficiary = Full_name_Decrypt;
                SetValueForBranch_beneficiary = Branch_Decrypt;
                SetValueForPhone_number_beneficiary = Phone_number_Decrypt;
                SetValueForPhone_beneficiary1 = Account_numberphone;

            }
            if (textBox1.Text.Length == 13)
            {


                con.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM User_information Where Account_number = '" + BitConverter.ToString(encryptedPhoneorAccount_number).Replace("-", "") + "'", con);


                SqlDataAdapter da4 = new SqlDataAdapter(cmd2);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Account_balance = dr4["Account_balance"].ToString();
                    }

                }
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Full_name = dr4["Full_name"].ToString();
                    }

                }
                SqlDataAdapter da3 = new SqlDataAdapter(cmd2);
                DataTable dt3 = new DataTable();
                da4.Fill(dt4);
                if (dt4 != null)
                {
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        Branch = dr4["Account_balance"].ToString();
                    }

                }
                con.Close();

                byte[] Key_Admin_byte = new byte[Account_balance.Length / 2];
                for (int i = 0, h = 0; h < Account_balance.Length; i++, h += 2)
                {
                    Key_Admin_byte[i] = (byte)Int32.Parse(Account_balance.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }
                byte[] Branch_byte = new byte[Branch.Length / 2];
                for (int i = 0, h = 0; h < Branch.Length; i++, h += 2)
                {
                    Branch_byte[i] = (byte)Int32.Parse(Branch.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }
                byte[] Full_name_byte = new byte[Full_name.Length / 2];
                for (int i = 0, h = 0; h < Full_name.Length; i++, h += 2)
                {
                    Full_name_byte[i] = (byte)Int32.Parse(Full_name.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }

                byte[] Account_balance_byte = new byte[Account_balance.Length / 2];
                for (int i = 0, h = 0; h < Account_balance.Length; i++, h += 2)
                {
                    Account_balance_byte[i] = (byte)Int32.Parse(Account_balance.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                }
                using (Aes myAes1 = Aes.Create())
                {



                    byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                    myAes1.Key = key_admin_byte;

                    myAes1.IV = iv;
                    // Encrypt the string to an array of bytes.

                    Account_balance_Decrypt = DecryptStringFromBytes_Aes(Account_balance_byte, myAes1.Key, myAes1.IV);
                    Branch_Decrypt = DecryptStringFromBytes_Aes(Branch_byte, myAes1.Key, myAes1.IV);
                    Full_name_Decrypt = DecryptStringFromBytes_Aes(Full_name_byte, myAes1.Key, myAes1.IV);


                }


                SetValueForBalance_beneficiary = long.Parse(textBox2.Text);
                SetValueForamount = long.Parse(textBox2.Text);
                SetValueForAcount_beneficiary = Account_numberphone;
                SetValueForFull_name_beneficiary = Full_name_Decrypt;
                SetValueForBranch_beneficiary = Branch_Decrypt;


            }


            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!isaccountValid && !isaccount2Valid)
            {
                MessageBox.Show("Please enter a valid Phone Number/Account Number");
            }

            else if (!isAmountValid)
            {
                MessageBox.Show("Please enter a valid Amount");
            }
            else if (  Full_name == "")
            {
                 
                MessageBox.Show("Số điện thoại và tài khoản không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((SetValueForBalance_owner - SetValueForamount) > 50000)
            {
                Transfer_1 m = new Transfer_1();
                m.Show();
                Visible = false;

                con.Close();
            }
            else
            {
                MessageBox.Show("Số dư bạn không đủ để thực hiện giao dịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }

        }

        private void Transfercs_Load_1(object sender, EventArgs e)
        {
             
            string Full_name = "";
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM User_information Where Account_number = '" + LOGIN.SetValueForAccount_number_owner + "'", con);

            SqlDataAdapter da2 = new SqlDataAdapter(cmd1);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            if (dt2 != null)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    Full_name = dr2["Full_name"].ToString();
                }
            }
            label5.Text = LOGIN.SetValueForFullname_owner  + " transfer";
            label9.Text = LOGIN.SetValueForAccount_number_owner;
            label4.Text = LOGIN.SetValueForAccount_balance_owner + " VND";

            SetValueForBalance_owner = long.Parse(LOGIN.SetValueForAccount_balance_owner); ;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Main m = new Main();
            m.Show();
            Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
