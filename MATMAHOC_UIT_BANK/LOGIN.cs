
 
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
using System.Text.RegularExpressions;
using SHA3.Net;//thhu vien hash imporrt 2.00 vao 1 lan trong packets trong o D
using System.IO;
namespace MATMAHOC_UIT_BANK
{
    public partial class LOGIN : Form
    {
        string Full_name_Decrypt;
        string Sex_Decrypt;
        string Email_Decrypt;
        string Account_balance_Decrypt;
        string CMND_CCCD_Decrypt;
        string Branch_Decrypt;
        string Phone_number_Decrypt;

        public static string SetValueForAccount_number_owner1 = "";
        public static string SetValueForAccount_number_owner = "";
        public static string SetValueForFullname_owner = "";
        public static string SetValueForAccount_balance_owner = "";
        public static string SetValueForSex_owner = "";
        public static string SetValueForPhone_number_owner = "";
        public static string SetValueForCMND_CCCD_owner = "";
        public static string SetValueForBranch_owner = "";
        public static string SetValueForpassword = "";
        public static string SetValueForEmail_owner = "";
        public LOGIN()
        {
            InitializeComponent();
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
        public  static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null )/*|| cipherText.Length <= 0)*/
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





        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-7DD7VRR\SQLEXPRESS;Initial Catalog=MATMAHOC;Integrated Security=True");





        private void button1_Click(object sender, EventArgs e)
        {


            /*con.Open();
            var Balance_beneficiary2 = new SqlCommand("DELETE  User_information WHERE   Account_balance  =N'" + textBox1 + "'" , con);
            Balance_beneficiary2.ExecuteNonQuery();


             
            con.Close();*/


            string Full_name_Decrypt;
            string Sex_Decrypt;
            string Email_Decrypt;
            string Account_balance_Decrypt;
            string CMND_CCCD_Decrypt;
            string Branch_Decrypt;
            string Phone_number_Decrypt;
            string Account_number_Decrypt;

            string pass = "";
            string Account_number = "";
            string Account_balance = "";
            string Full_name = "";
            string Passhash = "";
            string Sex = "";
            string Email = "";
            string CMND_CCCD = "";
            string Branch = "";
            string Phone_number="";
            string Key_AES = "";


            string duLieu = textBox2.Text;
            byte[] duLieuArray;
            duLieuArray = Encoding.UTF8.GetBytes(duLieu);

            var sha3512 = Sha3.Sha3512().ComputeHash(duLieuArray);
            Passhash = (ByteArrayToString(sha3512)).ToLower();


            string Passhash1 = "";
            string duLieu1 = textBox1.Text;
            byte[] duLieuArray1;
            duLieuArray1 = Encoding.UTF8.GetBytes(duLieu1);

            var sha3512_1 = Sha3.Sha3512().ComputeHash(duLieuArray1);
            Passhash1 = (ByteArrayToString(sha3512_1)).ToLower();





            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM LOGIN Where Phone_number = '" + Passhash1 + "'", con);
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
                SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                da.Fill(dt2);
                if (dt2 != null)
                {
                    foreach (DataRow dr2 in dt.Rows)
                    {
                        Account_number = dr2["Account_number"].ToString();
                    }

                }


            }
            catch (Exception)
            {
                 

            }
            finally
            {
                con.Close();
            }
            string Key_Admin = "";
            con.Open();
            SqlCommand cmd5 = new SqlCommand("SELECT * FROM Key_Admin Where Account_number = '9710010910511 '", con);
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            DataTable dt5 = new DataTable();
            da5.Fill(dt5);
            if (dt5 != null)
            {
                foreach (DataRow dr5 in dt5.Rows)
                {
                    Key_Admin = dr5["Key_Admin"].ToString();
                }
            }
           
           con.Close();


           




            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Key_AES Where Account_number = '" + Account_number + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1 != null)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    Key_AES = dr1["Key_AES"].ToString();
                }
            }
            con.Close();


            con.Open();
            SqlCommand cmd7 = new SqlCommand("SELECT * FROM User_information Where Account_number = '" + Account_number + "'", con);
            SqlDataAdapter da33 = new SqlDataAdapter(cmd7);
            DataTable dt33 = new DataTable();
            da33.Fill(dt33);
            if (dt33 != null)
            {
                foreach (DataRow dr3 in dt33.Rows)
                {
                    Account_balance = dr3["Account_balance"].ToString();
                }
            }
            SqlDataAdapter da3 = new SqlDataAdapter(cmd7);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            if (dt3 != null)
            {
                foreach (DataRow dr3 in dt3.Rows)
                {
                    Full_name = dr3["Full_name"].ToString();
                }
            }

            SqlDataAdapter da4 = new SqlDataAdapter(cmd7);
            DataTable dt4 = new DataTable();
            da3.Fill(dt3);
            if (dt3 != null)
            {
                foreach (DataRow dr3 in dt3.Rows)
                {
                    Sex = dr3["Sex"].ToString();
                }
            }
            SqlDataAdapter da6 = new SqlDataAdapter(cmd7);
            DataTable dt6 = new DataTable();
            da3.Fill(dt3);
            if (dt3 != null)
            {
                foreach (DataRow dr3 in dt3.Rows)
                {
                    Email = dr3["Email"].ToString();
                }
            }
            SqlDataAdapter da7 = new SqlDataAdapter(cmd7);
            DataTable dt7 = new DataTable();
            da3.Fill(dt3);
            if (dt3 != null)
            {
                foreach (DataRow dr3 in dt3.Rows)
                {
                    Phone_number = dr3["Phone_number"].ToString();
                }
            }
            SqlDataAdapter da8 = new SqlDataAdapter(cmd7);
            DataTable dt8 = new DataTable();
            da3.Fill(dt3);
            if (dt3 != null)
            {
                foreach (DataRow dr3 in dt3.Rows)
                {
                    CMND_CCCD = dr3["CMND_CCCD"].ToString();
                }
            }
            SqlDataAdapter da9 = new SqlDataAdapter(cmd7);
            DataTable dt9 = new DataTable();
            da3.Fill(dt3);
            if (dt3 != null)
            {
                foreach (DataRow dr3 in dt3.Rows)
                {
                    Branch = dr3["Branch"].ToString();
                }
            }



             

            /*     var chars = "0123456789ABCDEF";
                 var stringChars = new char[48];
                 var random = new Random();
                 for (int i = 0; i < stringChars.Length; i++)
                 {
                     stringChars[i] = chars[random.Next(chars.Length)];
                 } 

                 var finalString = new String(stringChars);
                  */

            byte[] Key = new byte[24];
            for (int i = 0, h = 0; h < Key_AES.Length; i++, h += 2)
            {
                Key[i] = (byte)Int32.Parse(Key_AES.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] Full_name_byte = new byte[Full_name.Length/2];
            for (int i = 0, h = 0; h < Full_name.Length; i++, h += 2)
            {
                Full_name_byte[i] = (byte)Int32.Parse(Full_name.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] Email_byte = new byte[Email.Length / 2];
            for (int i = 0, h = 0; h < Email.Length; i++, h += 2)
            {
                Email_byte[i] = (byte)Int32.Parse(Email.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] Sex_byte = new byte[Sex.Length / 2];
            for (int i = 0, h = 0; h < Sex.Length; i++, h += 2)
            {
                Sex_byte[i] = (byte)Int32.Parse(Sex.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] Account_balance_byte = new byte[Account_balance.Length / 2];
            for (int i = 0, h = 0; h < Account_balance.Length; i++, h += 2)
            {
                Account_balance_byte[i] = (byte)Int32.Parse(Account_balance.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] CMND_CCCD_byte = new byte[CMND_CCCD.Length / 2];
            for (int i = 0, h = 0; h < CMND_CCCD.Length; i++, h += 2)
            {
                CMND_CCCD_byte[i] = (byte)Int32.Parse(CMND_CCCD.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] Branch_byte = new byte[Branch.Length / 2];
            for (int i = 0, h = 0; h < Branch.Length; i++, h += 2)
            {
                Branch_byte[i] = (byte)Int32.Parse(Branch.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] Phone_number_byte = new byte[Phone_number.Length / 2];
            for (int i = 0, h = 0; h < Phone_number.Length; i++, h += 2)
            {
                Phone_number_byte[i] = (byte)Int32.Parse(Phone_number.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            byte[] Account_number_byte = new byte[Account_number.Length / 2];
            for (int i = 0, h = 0; h < Account_number.Length; i++, h += 2)
            {
                Account_number_byte[i] = (byte)Int32.Parse(Account_number.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }


             byte[] Key_Admin_byte = new byte[Key_Admin.Length / 2];
            for (int i = 0, h = 0; h < Key_Admin.Length; i++, h += 2)
            {
                Key_Admin_byte[i] = (byte)Int32.Parse(Key_Admin.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }
            using (Aes myAes1 = Aes.Create())
            {



                byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                myAes1.Key = Key_Admin_byte;

                myAes1.IV = iv;
                // Encrypt the string to an array of bytes.

                Account_balance_Decrypt = DecryptStringFromBytes_Aes(Account_balance_byte, myAes1.Key, myAes1.IV);
                Branch_Decrypt = DecryptStringFromBytes_Aes(Branch_byte, myAes1.Key, myAes1.IV);
                 Phone_number_Decrypt = DecryptStringFromBytes_Aes(Phone_number_byte, myAes1.Key, myAes1.IV); 
                Full_name_Decrypt = DecryptStringFromBytes_Aes(Full_name_byte, myAes1.Key, myAes1.IV);
                Account_number_Decrypt = DecryptStringFromBytes_Aes(Account_number_byte, myAes1.Key, myAes1.IV);

            }

            using (Aes myAes = Aes.Create())
            {
                byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                myAes.Key = Key;
                myAes.IV = iv;
                // Decrypt the bytes to a string.
                 
                 
                Sex_Decrypt = DecryptStringFromBytes_Aes(Sex_byte, myAes.Key, myAes.IV);
                Email_Decrypt = DecryptStringFromBytes_Aes(Email_byte, myAes.Key, myAes.IV);
                CMND_CCCD_Decrypt = DecryptStringFromBytes_Aes(CMND_CCCD_byte, myAes.Key, myAes.IV);
                /*Branch_Decrypt = DecryptStringFromBytes_Aes(Branch_byte, myAes.Key, myAes.IV);
                Phone_number_Decrypt = DecryptStringFromBytes_Aes(Phone_number_byte, myAes.Key, myAes.IV);
                Full_name_Decrypt = DecryptStringFromBytes_Aes(Full_name_byte, myAes.Key, myAes.IV);*/
            }

            SetValueForEmail_owner = Email_Decrypt;
            SetValueForFullname_owner = Full_name_Decrypt;
            SetValueForAccount_number_owner = Account_number_Decrypt;
            SetValueForAccount_balance_owner = Account_balance_Decrypt;
            SetValueForSex_owner = Sex_Decrypt;
       /*     SetValueForPhone_number_owner = Phone_number_Decrypt;*/
            SetValueForCMND_CCCD_owner = CMND_CCCD_Decrypt;
            SetValueForBranch_owner = Branch_Decrypt;
            SetValueForAccount_number_owner1 = Account_number;










            string phonePattern = @"^[0-9]\d{9}$"; // VN Phone number pattern   
            bool isPhoneValid = Regex.IsMatch(textBox1.Text, phonePattern);

            if (textBox1.Text == "" || textBox2.Text == "")
            {

                MessageBox.Show("Điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!isPhoneValid)
            {
                MessageBox.Show("Please enter a valid phone number", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (pass == Passhash)
            {


                Main q = new Main();
                q.Show();
                Visible = false;

                con.Close();


            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
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

        
 

        private void button2_Click_1(object sender, EventArgs e)
        {
            CreateAccount q = new CreateAccount();
            q.Show();
            Visible = false;
        }
        /*public static void Main(string[] args)
        {
            var chars = "0123456789ABCDEF";
            var stringChars = new char[48];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            Console.WriteLine("String:   {0}", finalString);

            byte[] a = new byte[24];
            for (int i = 0, h = 0; h < finalString.Length; i++, h += 2)
            {
                a[i] = (byte)Int32.Parse(finalString.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }

            *//*Console.WriteLine("[{0}]", string.Join(", ", a));*//*

            

        }*/

        private void LOGIN_Load(object sender, EventArgs e)
        {



            /* string original = "Nguyeenx DDangw Chaau";

             // Create a new instance of the Aes
             // class.  This generates a new key and initialization
             // vector (IV).
             using (Aes myAes = Aes.Create())
             {

                    var chars = "0123456789ABCDEF";
             var stringChars = new char[48];
             var random = new Random();
             for (int i = 0; i < stringChars.Length; i++)
             {
                 stringChars[i] = chars[random.Next(chars.Length)];
             }

             var finalString = new String(stringChars);
             Console.WriteLine("String:   {0}", finalString);

             byte[] key = new byte[24];
             for (int i = 0, h = 0; h < finalString.Length; i++, h += 2)
             {
                     key[i] = (byte)Int32.Parse(finalString.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
             }

                 byte[] iv = new byte[16] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };
                 myAes.Key = key;
                 myAes.IV = iv;
                 // Encrypt the string to an array of bytes.
                 byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                 // Decrypt the bytes to a string.
                 string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

                 //Display the original data and the decrypted data.


                                 label1.Text =  BitConverter.ToString(encrypted).Replace("-", "");
                 textBox1.Text = roundtrip;


             }*/
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
