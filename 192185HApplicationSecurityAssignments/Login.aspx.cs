using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;


namespace _192185HApplicationSecurityAssignments
{
    public partial class Login : System.Web.UI.Page
    {

        string Assignment1DBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Assignment1DBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //if (CaptchaValidation())
            //{
                string email = login_emailAddress.Text.ToString().Trim();
                string pwd = login_password.Text.ToString().Trim();

                SHA512Managed hashing = new SHA512Managed();
                string dbHash = getDBHash(email);
                string dbSalt = getDBSalt(email);

                try
                {
                    if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                    {
                        string pwdWithSalt = pwd + dbSalt;
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        string userHash = Convert.ToBase64String(hashWithSalt);

                        if (userHash.Equals(dbHash))
                        {
                            Session["LoggedIn"] = email;
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthenticationToken"] = guid;

                            Response.Cookies.Add(new HttpCookie("AuthenticationToken", guid));

                            Response.Redirect("SuccessLogin.aspx", false);
                        }
                        else
                        {
                            errorMsg.Text = "Email Address or password is not valid. Please try again.";
                            Response.Redirect("Login.aspx", false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                finally { }
            //}

        }




        protected string getDBHash(string email)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(Assignment1DBConnectionString);
            string sql = "select PasswordHash FROM Users WHERE EmailAddress = @USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
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


        protected string getDBSalt(string email)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(Assignment1DBConnectionString);
            string sql = "select PASSWORDSALT FROM Users WHERE EmailAddress = @USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", email);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
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



        public class Captcha
        {
            public string successful { get; set; }
            public List<string> ErrorMessage { get; set; }
        }


        public bool CaptchaValidation()
        {
            bool result = true;

            string CaptchaResponse = Request.Form["g-recaptcha-response"];
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=SECRETKEY &response=" + CaptchaResponse);

            try
            {
                using (WebResponse webResponse = Req.GetResponse())
                {
                    using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string JSONResponse = streamReader.ReadToEnd();

                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        Captcha captcha = serializer.Deserialize<Captcha>(JSONResponse);

                        result = Convert.ToBoolean(captcha.successful);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }



    }
}
