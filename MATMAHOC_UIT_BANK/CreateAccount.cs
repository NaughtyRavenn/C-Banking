
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
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using SHA3.Net;//thhu vien hash imporrt 2.00 vao 1 lan trong packets trong o D
using System.IO;

namespace MATMAHOC_UIT_BANK
{
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            LOGIN m = new LOGIN();
            m.Show();
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {

 
            string Passhash = "";
            string duLieu = textBox4.Text;
            byte[] duLieuArray;
            duLieuArray = Encoding.UTF8.GetBytes(duLieu);

            var sha3512 = Sha3.Sha3512().ComputeHash(duLieuArray);
            Passhash = (ByteArrayToString(sha3512)).ToLower();



            string Passhash1 = "";
            string duLieu1 = textBox5.Text;
            byte[] duLieuArray1;
            duLieuArray1 = Encoding.UTF8.GetBytes(duLieu1);

            var sha3512_1 = Sha3.Sha3512().ComputeHash(duLieuArray1);
            Passhash1 = (ByteArrayToString(sha3512_1)).ToLower();



            // Create string variables that contain the patterns   
            string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"; // Email address pattern  
            string CMNDCodePattern = @"^[0-9]\d{8}$";
            string phonePattern = @"^[0-9]\d{9}$"; // VN Phone number pattern   
            string FullnameCodePattern = @"^[A-Za-z]+[\s][A-Za-z]+[\s][A-Za-z]+$";
            // Create a bool variable and use the Regex.IsMatch static method which returns true if a specific value matches a specific pattern  
            bool isEmailValid = Regex.IsMatch(textBox2.Text, emailPattern);
            bool isCMNDValid = Regex.IsMatch(textBox8.Text, CMNDCodePattern);
            bool isPhoneValid = Regex.IsMatch(textBox5.Text, phonePattern);
            bool isfullnameValid = Regex.IsMatch(textBox1.Text, FullnameCodePattern);
            string pass = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LOGIN Where Phone_number = '" + textBox1.Text + "'", con);
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




           
            if (textBox1.Text == "" || textBox2.Text == "" || textBox5.Text == "" || textBox4.Text == "" || textBox8.Text == "" || comboBox4.Text == "" || comboBox5.Text == "")
            {
                MessageBox.Show("Điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!isfullnameValid)
            {
                MessageBox.Show("Please enter a valid fullname!");
            }
            else if (!isEmailValid)
            {
                MessageBox.Show("Please enter a valid email!");
            }

            else if (!isCMNDValid)
            {
                MessageBox.Show("Please enter a valid CMND number!");
            }

            else if (!isPhoneValid)
            {
                MessageBox.Show("Please enter a valid phone number!");
            }
            else
            if (textBox4.Text != textBox3.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (pass == textBox3.Text)
            {
                MessageBox.Show("Tài khoản đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else

            {

                {
                    string Key_admin = "";
                    byte[] encryptedFull_name;
                    byte[] encryptedEmail;
                    byte[] encryptedAccount_balance;
                    byte[] encryptedCMND_CCCD;
                    byte[] encryptedBranch;
                    byte[] encryptedAccount_number;
                    byte[] encryptedSex;
                    byte[] encryptedPhone;
                    var finalString1 = "";
                    var finalString = "";

                    var chars = "0123456789";
                    var stringChars = new char[13];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    finalString1 = new String(stringChars);
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

                    byte[] key_admin_byte = new byte[Key_admin.Length/2];
                    for (int i = 0, h = 0; h < Key_admin.Length; i++, h += 2)
                    {
                        key_admin_byte[i] = (byte)Int32.Parse(Key_admin.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                    }

               
                    using (Aes myAes1 = Aes.Create())
                    {

                         

                        byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                        myAes1.Key = key_admin_byte;

                        myAes1.IV = iv;
                        // Encrypt the string to an array of bytes.

                        encryptedAccount_balance = EncryptStringToBytes_Aes("500000", myAes1.Key, myAes1.IV);

                        encryptedBranch = EncryptStringToBytes_Aes(comboBox5.Text, myAes1.Key, myAes1.IV);
                        encryptedAccount_number = EncryptStringToBytes_Aes(finalString1, myAes1.Key, myAes1.IV);
                        encryptedFull_name = EncryptStringToBytes_Aes(textBox1.Text, myAes1.Key, myAes1.IV);
                        encryptedPhone = EncryptStringToBytes_Aes(textBox5.Text, myAes1.Key, myAes1.IV);
                    }



            




                    /*long a = 2353463248954 - long.Parse(textBox5.Text);
                    */

                     






                    // Create a new instance of the Aes
                    // class.  This generates a new key and initialization
                    // vector (IV).
                    using (Aes myAes = Aes.Create())
                    {

                        var chars1 = "0123456789ABCDEF";
                        var stringChars1 = new char[48];
                        var random1 = new Random();
                        for (int i = 0; i < stringChars1.Length; i++)
                        {
                            stringChars1[i] = chars1[random1.Next(chars1.Length)];
                        }

                        finalString = new String(stringChars1);


                        byte[] key = new byte[24];
                        for (int i = 0, h = 0; h < finalString.Length; i++, h += 2)
                        {
                            key[i] = (byte)Int32.Parse(finalString.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
                        }
                         
                        byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                        myAes.Key = key;
                         
                        myAes.IV = iv;
                        // Encrypt the string to an array of bytes.
                         
                        encryptedEmail = EncryptStringToBytes_Aes(textBox2.Text, myAes.Key, myAes.IV);
                        
                        encryptedCMND_CCCD = EncryptStringToBytes_Aes(textBox8.Text, myAes.Key, myAes.IV);
                        
                         
                        encryptedSex = EncryptStringToBytes_Aes(comboBox4.Text, myAes.Key, myAes.IV);
                         
                        // Decrypt the bytes to a string.
                        /*string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);*/


                        /*  label1.Text = BitConverter.ToString(encrypted).Replace("-", "");
                          textBox1.Text = roundtrip;*/
                    }








                    con.Open();
                    var KEY_AES = new SqlCommand("INSERT INTO Key_AES(Key_AES,Account_number) VALUES( N'" + finalString + "',N'" + BitConverter.ToString(encryptedAccount_number).Replace("-", "") + "'  )", con);
                    KEY_AES.ExecuteNonQuery();

                    var LOGIN = new SqlCommand("INSERT INTO LOGIN(Phone_number,Password,Account_number) VALUES(N'" + Passhash1 + "',N'" + Passhash + "',N'" + BitConverter.ToString(encryptedAccount_number).Replace("-", "") + "' )", con);
                    LOGIN.ExecuteNonQuery();
                    var User_information = new SqlCommand("INSERT INTO User_information(Full_name,Sex,Email,Account_balance,CMND_CCCD,Branch,Account_number,Phone_number) VALUES(N'" + BitConverter.ToString(encryptedFull_name).Replace("-", "") + "',N'" + BitConverter.ToString(encryptedSex).Replace("-", "") + "',N'" + BitConverter.ToString(encryptedEmail).Replace("-", "") + "',N'" + BitConverter.ToString(encryptedAccount_balance).Replace("-", "") + "',N'" + BitConverter.ToString(encryptedCMND_CCCD).Replace("-", "") + "',N'" + BitConverter.ToString(encryptedBranch).Replace("-", "") + "',N'" + BitConverter.ToString(encryptedAccount_number).Replace("-", "") + "',N'" + BitConverter.ToString(encryptedPhone).Replace("-", "") + "')", con);
                    User_information.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Tạo tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox8.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    comboBox5.Text = "";
                    comboBox4.Text = "";
                }
            }
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
        private void button2_Click(object sender, EventArgs e)
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
            if (cipherText == null || cipherText.Length <= 0)
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

        private void CreateAccount_Load(object sender, EventArgs e)
        {
            comboBox4.Items.Add("Male");
            comboBox4.Items.Add("Female");
            comboBox4.Items.Add("Other genders");
            comboBox5.Items.Add("UITBANK - Chi nhánh Sài Gòn ");
            comboBox5.Items.Add("UITBANK - Chi nhánh Hà Nội ");
            comboBox5.Items.Add("UITBANK - Chi nhánh Hải Phòng ");
            comboBox5.Items.Add("UITBANK - Chi nhánh Đà Nẵng ");
            comboBox5.Items.Add("UITBANK - Chi nhánh Cần Thơ ");
        }
    }
}
