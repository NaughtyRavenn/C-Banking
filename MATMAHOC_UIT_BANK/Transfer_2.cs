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
    public partial class Transfer_2 : Form
    {
        public Transfer_2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


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




        private void Transfer_2_Load_1(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Transfercs m = new Transfercs();
            m.Show();
            Visible = false;
        }
        static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder output = new StringBuilder(arrInput.Length);    //ham hash
            for (int i = 0; i < arrInput.Length; i++)
            {
                output.Append(arrInput[i].ToString("X2"));
            }
            return output.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string pass = "";
            string Balance_owner = "";
            string Balance_beneficiary = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LOGIN Where Account_number = '" + LOGIN.SetValueForAccount_number_owner1 + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        pass = dr["Password"].ToString();
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

            string Passhash = "";
            string duLieu = textBox2.Text;
            byte[] duLieuArray;
            duLieuArray = Encoding.UTF8.GetBytes(duLieu);

            var sha3512 = Sha3.Sha3512().ComputeHash(duLieuArray);
            Passhash = (ByteArrayToString(sha3512)).ToLower();









            string phonePattern = @"^[0-9]\d{10}$"; // VN Phone number pattern   
            bool isPhoneValid = Regex.IsMatch(textBox2.Text, phonePattern);
            if (textBox2.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /*else if (!isPhoneValid)
            {
                MessageBox.Show("Please enter a valid phone number", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }*/
            else if (pass == Passhash)
            {

                Balance_owner = (Transfercs.SetValueForBalance_owner - Transfercs.SetValueForamount).ToString();
                Balance_beneficiary = (Transfercs.SetValueForBalance_beneficiary + Transfercs.SetValueForamount).ToString();
               /* textBox1.Text = Transfercs.SetValueForPhone_beneficiary1;
                textBox3.Text =  LOGIN.SetValueForAccount_number_owner1; */


                byte[] encryptedBalance_beneficiary  ;
                byte[] encryptedBalance_owner  ;
                string Key_admin="";

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

                string a = Transfercs.SetValueForPhone_beneficiary1;
                using (Aes myAes1 = Aes.Create())
                {



                    byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                    myAes1.Key = key_admin_byte;

                    myAes1.IV = iv;
                    // Encrypt the string to an array of bytes.

                    encryptedBalance_beneficiary = EncryptStringToBytes_Aes(Balance_beneficiary, myAes1.Key, myAes1.IV);
                    encryptedBalance_owner = EncryptStringToBytes_Aes(Balance_owner, myAes1.Key, myAes1.IV);

                }
                /*textBox1.Text = BitConverter.ToString(encryptedBalance_beneficiary).Replace("-", "");
                 textBox3.Text = BitConverter.ToString(encryptedBalance_owner).Replace("-","");*/

                con.Open();
                var Balance_beneficiary2 = new SqlCommand("UPDATE  User_information SET Account_balance  =N'" + BitConverter.ToString(encryptedBalance_owner).Replace("-", "") + "' WHERE Account_number  = '3737D3EBEC0C5D645E6C8E409862A803'", con);
                Balance_beneficiary2.ExecuteNonQuery();

                var Balance_owner1 = new SqlCommand("UPDATE User_information SET Account_balance  =N'" + BitConverter.ToString(encryptedBalance_beneficiary).Replace("-", "") + "' WHERE Phone_number  = '0BDF39113F51DF5B928A54FEF23FF8F4'", con);
                Balance_owner1.ExecuteNonQuery();

                con.Close();


                /* con.Open();
                 var Balance_beneficiary2 = new SqlCommand("UPDATE  chau SET chau2  =N'" + "1" + "' WHERE chau  = " + "123" + "", con);
                 Balance_beneficiary2.ExecuteNonQuery();



                 con.Close();*/










                Transfer_3 m = new Transfer_3();
                m.Show();
                Visible = false;
            }
            else
            {
                MessageBox.Show("Mật khẩu xác thực không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
