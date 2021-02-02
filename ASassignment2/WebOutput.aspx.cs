using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASassignment2
{
    public partial class WebOutput : System.Web.UI.Page
    {
        string AssignmentASDB = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentASDB"].ConnectionString;
        byte[] ccx =null;
        byte[] Key;
        byte[] IV;


        string userid = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            // check if this session is logged in or not. 
            // prevent session fixation.
            if (Session["USERID"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("WebLogin.aspx", false);
                }
                else
                {
                    userid = (string)Session["USERID"];
                    displayDetails(userid);
                }
            }
            else
            {
                Response.Redirect("WebLogin.aspx", false);
            }
        }
        // decrypt CreditCard information to display
        protected string decryptData(byte[] cipherText)
        {
            string plainText = null;

            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;

                ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrpt = new CryptoStream(msDecrypt, decryptTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrpt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return plainText;
        }
        protected void displayDetails(string userid)
        {
            
            SqlConnection connection = new SqlConnection(AssignmentASDB);
            string sql = "SELECT * FROM Tableaccount WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                       
                        if (reader["FName"] != DBNull.Value)
                        {
                            LabelFname.Text = reader["FName"].ToString();
                        }
                        if (reader["LName"] != DBNull.Value)
                        {
                            LabelLname.Text = reader["LName"].ToString();
                        }
                        if (reader["Email"] != DBNull.Value)
                        {
                            LabelEmail.Text = reader["Email"].ToString();
                        }
                        if (reader["DOB"] != DBNull.Value)
                        {
                            LabelDOB.Text = reader["DOB"].ToString();
                        }
                        
                        if (reader["Cc"] != DBNull.Value)
                        {
                            //print to string after calling the decryption
                            //LabelCC.Text = reader["Cc"].ToString();
                            ccx = Convert.FromBase64String(reader["Cc"].ToString());
                        }
                        if (reader["IV"] != DBNull.Value)
                        {
                            IV = Convert.FromBase64String(reader["IV"].ToString());
                        }
                        if (reader["Key"] != DBNull.Value)
                        {
                            Key = Convert.FromBase64String(reader["Key"].ToString());
                        }


                    }
                    LabelCC.Text = decryptData(ccx);   
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
        }


        //to clear/expire the current session

        protected void ButtonaaaLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("WebLogin.aspx", false);
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Request.Cookies["AuthToken"].Value = string.Empty;
                Request.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
    }
}