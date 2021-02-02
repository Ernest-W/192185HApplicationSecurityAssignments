using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _192185HApplicationSecurityAssignments
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["LoggedIn"] != null && Session["AuthenticationToken"] != null && Request.Cookies["AuthenticationToken"] != null)
            {
                if (!Session["AuthenticationToken"].ToString().Equals(Request.Cookies["AuthenticationToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    LoginSuccessMessage.Text = "You have successfully logged in !";
                    LoginSuccessMessage.ForeColor = System.Drawing.Color.Green;
                    LogOutButton.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            } 
            /*if (Session["LoggedIn"] != null)
            {
                LoginSuccessMessage.Text = "You have successfully logged in !";
                LoginSuccessMessage.ForeColor = System.Drawing.Color.Green;
                LogOutButton.Visible = true;
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            } */
        }

        protected void LogMeOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-10);
            }

            if (Request.Cookies["AuthenticationToken"] != null)
            {
                Response.Cookies["AuthenticationToken"].Value = string.Empty;
                Response.Cookies["AuthenticationToken"].Expires = DateTime.Now.AddDays(-10);
            }
            

        }
    }
}