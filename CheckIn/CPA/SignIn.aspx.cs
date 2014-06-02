using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.Web_Pages
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            bool result= BusinessLogic.IsLoginSuccessful(txtEmail.Text, txtPassword.Text);
            if (result)
            {
                int role;
                if ((role = BusinessLogic.GetRoleID(txtEmail.Text, txtPassword.Text)) > 0)
                {
                    string user;
                    Session["roleID"] = role;

                    Session["userID"] = BusinessLogic.GetUserID(txtEmail.Text, txtPassword.Text);
                    if (role == 1)
                    {
                        if ((user = BusinessLogic.GetLoggedInUserName(txtEmail.Text, txtPassword.Text)) != null)
                        {
                            Session["userName"] = user;
                            Response.Redirect("~/Default.aspx");
                        }
                    }
                    if (role == 2)
                    {
                        if ((user = BusinessLogic.GetLoggedInCPAName(txtEmail.Text, txtPassword.Text)) != null)
                        {
                            Session["userName"] = user;
                            Response.Redirect("~/CPA/ManageAppointment.aspx");
                        }
                    }
                    if (role == 3)
                    {
                    }
                }
            }
            else
            {
                loginValidation.IsValid = false;
            }

        }
    }
}