using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;


namespace _192185HApplicationSecurityAssignments
{


    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        string Assignment1DBConnectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["Assignment1DBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;


        public void createUser()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Assignment1DBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@FirstName, @LastName, @CreditCardInfo, @EmailAddress, @PasswordHash, @PasswordSalt, @DateOfBirth, @IV, @Key)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", firstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", lastName.Text.Trim());
                            cmd.Parameters.AddWithValue("@CreditCardInfo", encryptData(CreditCardInfo.Text.Trim()));
                            cmd.Parameters.AddWithValue("@EmailAddress", emailAddress.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@DateOfBirth", birthDate.Text.Trim());
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

            Response.Redirect("Login.aspx");
        }


        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
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

            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }

            return score;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (CaptchaValidation())
            {
                int scores = checkPassword(password.Text);
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
                pwdchecker.Text = "Status : " + status;
                if (scores < 4)
                {
                    pwdchecker.ForeColor = Color.Red;
                    return;
                }
                pwdchecker.ForeColor = Color.Green;



                string pwd = password.Text.ToString().Trim();

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] saltByte = new byte[8];

                rng.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);

                SHA512Managed hashing = new SHA512Managed();

                string pwdWithSalt = pwd + salt;
                byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

                finalHash = Convert.ToBase64String(hashWithSalt);

                RijndaelManaged cipher = new RijndaelManaged();
                cipher.GenerateKey();
                Key = cipher.Key;
                IV = cipher.IV;

                createUser();
            }

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
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LfbjDsaAAAAAPv81gjaAw948dLmmEJlyLAbewF_ &response=" + CaptchaResponse);

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

                        lbl_score.Text = result.ToString();
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