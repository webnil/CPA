using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class ValidateUserBooking : System.Web.UI.Page
    {

        private int role;
        private string activationToken;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string activationToken = Request.QueryString["Token"].ToString();
                if (ValidateUser(activationToken))
                {
                    PnlValid.Visible = true;
                    pnlNotValid.Visible = false;
                }
                else
                {
                    pnlNotValid.Visible = true;
                    PnlValid.Visible = false;
                }

            }
           
        }
        public bool ValidateUser(string activationToken)
        {
            bool IsValid;
            var result = BusinessLogic.GetTempUserDetails(activationToken);
            DateTime now = DateTime.Now;
            TimeSpan weekSpan = now.AddDays(7) - now;

             DateTime ValidDate=DateTime.Now.Subtract(weekSpan);

           if (result == null || result.Tables[0].Rows.Count == 0)
            {
               IsValid = false;
           }
           else if  (result.Tables[0].Rows[0]["ActivationToken"].ToString() == activationToken && (DateTime)result.Tables[0].Rows[0]["CreatedDate"] >= ValidDate)
                {

                CustomerDetails newCustomer = new CustomerDetails();

                newCustomer.Email = result.Tables[0].Rows[0]["Email"].ToString();
                newCustomer.Password = result.Tables[0].Rows[0]["Password"].ToString();
                newCustomer.FirstName = result.Tables[0].Rows[0]["FirstName"].ToString();
                newCustomer.LastName = result.Tables[0].Rows[0]["LastName"].ToString();
                newCustomer.DateOfBirth = (DateTime)result.Tables[0].Rows[0]["DateOfBirth"];
                newCustomer.Gender = result.Tables[0].Rows[0]["Gender"].ToString();
                newCustomer.PhoneNumber = result.Tables[0].Rows[0]["Phone"].ToString();
                newCustomer.Image = (byte[])result.Tables[0].Rows[0]["UserImage"];

                bool result1 = BusinessLogic.CreateNewCustomer(newCustomer);
                if (result1)
                    {
                        int count = BusinessLogic.DeleteTempUserDetails(activationToken);

                        

                        //    string user;
                        //    Session["roleID"] = 1;
                        //    if ((user = BusinessLogic.GetLoggedInUserName(txtEmail.Text, txtPassword.Text)) != null)
                        //    {
                        //        Session["userName"] = user;
                        //    }
                        //    int userID = BusinessLogic.GetNewUserID();
                        //    Session["userID"] = userID;
                        //    string redirectURL = string.Format("~/CPA/PersonalInformation.aspx?CPAID={0}&Schedule={1}&Purpose={2}&UserID={3}&ID={4}", Request.QueryString["CPAID"], lblScheduleDate.Text,
                        //                    lblPurposeOfVisit.Text, userID, Request.QueryString["ID"]);
                        //    //TODO: set login user name. FormsAuthentication.SetCookies
                        //    Response.Redirect(redirectURL);
                        //}
                        IsValid = true;
                    }
                else
                    {
                        IsValid = false;
                    }

                }
            else
            {
                //String message = "Validation Expire";
                IsValid = false;

            }
        
        
        return IsValid;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            bool result;
            if (result = BusinessLogic.IsLoginSuccessful(txtLoginEmail.Text, txtLoginPassword.Text))
            {
                if ((role = BusinessLogic.GetRoleID(txtLoginEmail.Text, txtLoginPassword.Text)) > 0)
                {
                    string user;
                    if ((user = BusinessLogic.GetLoggedInUserName(txtLoginEmail.Text, txtLoginPassword.Text)) != null)
                    {
                        Session["userName"] = user;
                        Session["roleID"] = role;
                    }

                    int userID = BusinessLogic.GetUserID(txtLoginEmail.Text, txtLoginPassword.Text);
                    string schedule=Server.UrlDecode(Request.QueryString["Schedule"]);
                    Session["userID"] = userID;
                    string redirectURL = string.Format("~/CPA/PersonalInformation.aspx?CPAID={0}&Schedule={1}&Purpose={2}&UserID={3}&ID={4}", Request.QueryString["CPAID"],schedule,
                                    Request.QueryString["Purpose"], userID, Request.QueryString["ID"]);
                    //TODO: set login user name. FormsAuthentication.SetCookies
                    Response.Redirect(redirectURL);
                }
            }
            else
            {
                loginValidation.IsValid = false;
            }
        }
    }
}