using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;    
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace ASassignment2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string AssignmentASDB = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentASDB"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        public string success { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        

        public bool Captcha()
        {
            bool result = true;
            string captchaResponse = Request.Form["g-recaptcha-response"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LdIB0QaAAAAAEXU3l1AsPCUKVlj6b8SGapMyth8 &response=" + captchaResponse);

            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        WebForm1 jsonObject = js.Deserialize<WebForm1>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (Captcha())
            {
                string pwd = PwTb.Text.ToString().Trim();
                string cfmPwd = CfmPwTb.Text.ToString().Trim();

                if (pwd != cfmPwd)
                {
                    Response.Redirect("WebRegister.aspx", false);
                }
                else
                {
                    int scores = checkPassword(PwTb.Text);
                    string status = "";
                    switch (scores)
                    {
                        case 1:
                            status = "Very Weak";
                            break;
                        case 2:
                            status = "Weak";
                            break;
                        case 3:
                            status = "Medium";
                            break;
                        case 4:
                            status = "Strong";
                            break;
                        case 5:
                            status = "Very Strong";
                            break;
                        default:
                            break;
                    }
                    lbl_pwdchecker.Text = "Status : " + status;
                    /*
                    if (scores < 4)
                    {
                        lbl_pwdchecker.ForeColor = Color.Red;
                        return;
                    }
                    lbl_pwdchecker.ForeColor = Color.Green;
                    */



                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    byte[] saltByte = new byte[8];

                    //to encrypt value into Base64String
                    rng.GetBytes(saltByte);
                    salt = Convert.ToBase64String(saltByte);
                    SHA512Managed hashing = new SHA512Managed();
                    string pwdWithSalt = pwd + salt;
                    byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                    finalHash = Convert.ToBase64String(hashWithSalt);

                    RijndaelManaged cipher = new RijndaelManaged();
                    //encryption key
                    cipher.GenerateKey();
                    Key = cipher.Key;
                    IV = cipher.IV;

                    // calls func
                    AccountCreate();

                    Response.Redirect("WebLogin.aspx");
                }
            }
        }
        public void AccountCreate()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AssignmentASDB))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Tableaccount VALUES(@FName,@LName,@DOB,@PwHash,@PwSalt,@Cc,@Email,@IV,@Key)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FName", HttpUtility.HtmlEncode(FNameTb.Text.Trim()));
                            cmd.Parameters.AddWithValue("@LName", HttpUtility.HtmlEncode(LNameTb.Text.Trim()));
                            cmd.Parameters.AddWithValue("@DOB", HttpUtility.HtmlEncode(DobTb.Text.Trim()));
                             // Codes to encrypt the cradit card info
                            cmd.Parameters.AddWithValue("@PwHash", finalHash);
                            cmd.Parameters.AddWithValue("@PwSalt", salt);  
                            
                            
                            cmd.Parameters.AddWithValue("@Cc", Convert.ToBase64String(DataEncryption(CCITb.Text.Trim())));      
                            
                            cmd.Parameters.AddWithValue("@Email", HttpUtility.HtmlEncode(EmailTb.Text.Trim()));                         
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));


                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        private int checkPassword(string password)
        {
            int score = 0;
            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }

            if (password.Any(char.IsUpper))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "^[a-zA-Z0-9 ]*$"))
            {
                score++;
            }
            return score;
        }
        protected byte[] DataEncryption(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();

                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }

        
    }

}

