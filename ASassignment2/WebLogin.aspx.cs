using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASassignment2
{
    public partial class WebLogin : System.Web.UI.Page
    {
        string AssignmentASDB = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentASDB"].ConnectionString;
        private string errorMsg;
        private object success;

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

                        WebLogin jsonObject = js.Deserialize<WebLogin>(jsonResponse);
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
            ////if (Captcha())
            {

                // Retrieve data from DB
                SqlConnection connection = new SqlConnection(AssignmentASDB);
                string sql = "SELECT * FROM Tableaccount WHERE Email='" + TextBoxEmail.Text + "'";

                string pwd = TextBoxPw.Text.ToString().Trim();
                string userid = TextBoxEmail.Text.ToString().Trim();



                SHA512Managed hashing = new SHA512Managed();
                string dbHash = getDBHash(userid);
                string dbSalt = getDBSalt(userid);


                try
                {
                    //this is to prevent session fixation attacks
                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);
                        if (userHash.Equals(dbHash))
                        {
                            if (userHash.Equals(dbHash))
                            {

                                Session["USERID"] = userid;

                                // Create a new GUID and save into the session
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;

                                // Create a new cookie with this GUID value
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                                Response.Redirect("WebOutput.aspx", false);
                            }
                            else
                            {
                                errorlabel.Text = "Incorrect Email or password.";
                            }
                        }
                    }
                    else
                    {
                        errorMsg = "Email or password is not valid. Please try again.";
                        Response.Redirect("WebLogin.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { }



            }
        }
            protected string getDBHash(string userid)
            {
                string h = null;
                SqlConnection connection = new SqlConnection(AssignmentASDB);
                string sql = "select PwHash FROM Tableaccount WHERE Email=@USERID";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@USERID", userid);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            if (reader["PwHash"] != null)
                            {
                                if (reader["PwHash"] != DBNull.Value)
                                {
                                    h = reader["PwHash"].ToString();
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { connection.Close(); }
                return h;
            } 
        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(AssignmentASDB);
            string sql = "select PwSalt FROM Tableaccount WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PwSalt"] != null)
                        {
                            if (reader["PwSalt"] != DBNull.Value)
                            {
                                s = reader["PwSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }


    }

}