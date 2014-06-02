using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Net;
using System.Net.Mail;

namespace CheckIn.CPA
{
    public partial class CheckNewUser : System.Web.UI.Page
    {
        #region Private Members

        private string cpid = string.Empty;
        private string schedule = string.Empty;
        private string scheduledTime = string.Empty;
        private string scheduledDate = string.Empty;
        private string purpose = string.Empty;
        private string id = string.Empty;
        private int role;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString.Count > 0)
            {
                cpid = Request.QueryString["CPAID"];
                schedule = Request.QueryString["Schedule"];
                scheduledTime = Request.QueryString["ScheduledTime"];
                scheduledDate = Request.QueryString["ScheduledDate"];
                purpose = Request.QueryString["Purpose"];
                id = Request.QueryString["ID"];
                imgCPA.ImageUrl = "Handler.ashx?QueryCPAID=" + cpid;
                FillAllSpeciality();
                RefreshBookingAppointments();

            }

            if (Session["userName"] != null && int.Parse(Session["roleID"].ToString())==1)
            {
                pnlLoggedIn.Visible = true;
                pnlNotLoggedIn.Visible = false;
                lblUserName.Text = "LoggedIn User " + Session["userName"].ToString();
                lnkBtnLoggedOut.Text = "Not " + Session["userName"].ToString() + ". Kindly Log Off";

            }
            else
            {
                pnlLoggedIn.Visible = false;
                pnlNotLoggedIn.Visible = true;
               
            }
            if (Session["FileUpload1"] == null && ImageUpload.HasFile)
            {
                Session["FileUpload1"] = ImageUpload;
                lblFile.Text = ImageUpload.FileName;
            }
            // Next time submit and Session has values but FileUpload is Blank 
            // Return the values from session to FileUpload 
            else if (Session["FileUpload1"] != null && (!ImageUpload.HasFile))
            {
                ImageUpload = (FileUpload)Session["FileUpload1"];
                lblFile.Text = ImageUpload.FileName;
            }
            // Now there could be another sictution when Session has File but user want to change the file 
            // In this case we have to change the file in session object 
            else if (ImageUpload.HasFile)
            {
                Session["FileUpload1"] = ImageUpload;
                lblFile.Text = ImageUpload.FileName;
            }
        }
        private void FillAllSpeciality()
        {
            var result = BusinessLogic.GetAllSpecializationList();
        //    ddlSpeciality.DataSource = result.Tables[0];
        //    ddlSpeciality.DataTextField = result.Tables[0].Columns["Speciality"].ColumnName.ToString();
        //    ddlSpeciality.DataValueField = result.Tables[0].Columns["ID"].ColumnName.ToString();
        //    ddlSpeciality.DataBind();
        }
        private void RefreshBookingAppointments()
        {
            var result = BusinessLogic.GetCPADetails(int.Parse(cpid));
            if (result == null || result.Tables[0].Rows.Count == 0)
                return;

            lnkCPADetail.Text = result.Tables[0].Rows[0]["CompanyName"].ToString();
            lnkCPADetail.NavigateUrl = "~/CPA/CPADetailsInfo.aspx?CPAID=" + cpid;
           // lblName.Text = result.Tables[0].Rows[0]["CompanyName"].ToString();
            lblName.Text = result.Tables[0].Rows[0]["FirstName"].ToString() + " " + result.Tables[0].Rows[0]["LastName"].ToString();
            lblAddress1.Text = result.Tables[0].Rows[0]["Address1"].ToString();
            lblAddress2.Text = result.Tables[0].Rows[0]["Address2"].ToString();
            lblCity.Text = result.Tables[0].Rows[0]["City"].ToString();
            lblState.Text = result.Tables[0].Rows[0]["State"].ToString();
            lblZipCode.Text = result.Tables[0].Rows[0]["ZipCode"].ToString();

            lblScheduleDate.Text = Request.QueryString["Schedule"];
            lblPurposeOfVisit.Text = Request.QueryString["Purpose"];
           
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

        }

        //protected void btnNewUser_Click(object sender, EventArgs e)
        //{
        //    pnlNewUser.Visible = true;
        //    pnlRegisteredUser.Visible = false;
        //    btnBack.Visible = false;
        //}

        //protected void btnRegisteredUser_Click(object sender, EventArgs e)
        //{
        //    pnlNewUser.Visible = false;
        //    pnlRegisteredUser.Visible = true;
        //    btnBack.Visible = false;
        //}

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            bool result;
            if ((BusinessLogic.IsLoginSuccessful(txtLoginEmail.Text, txtLoginPassword.Text)) && ((BusinessLogic.GetRoleID(txtLoginEmail.Text, txtLoginPassword.Text)) == 1))
            {
                //if ((role = BusinessLogic.GetRoleID(txtLoginEmail.Text, txtLoginPassword.Text)) > 0)
                //{
                    string user;
                    role = BusinessLogic.GetRoleID(txtLoginEmail.Text, txtLoginPassword.Text);
                    if ((user = BusinessLogic.GetLoggedInUserName(txtLoginEmail.Text, txtLoginPassword.Text)) != null)
                    {
                        Session["userName"] = user;
                        Session["roleID"] = role;
                    }

                    int userID = BusinessLogic.GetUserID(txtLoginEmail.Text, txtLoginPassword.Text);
                    Session["userID"] = userID;
                    string redirectURL = string.Format("~/CPA/PersonalInformation.aspx?CPAID={0}&Schedule={1}&Purpose={2}&UserID={3}&ID={4}", Request.QueryString["CPAID"], lblScheduleDate.Text,
                                    lblPurposeOfVisit.Text, userID, Request.QueryString["ID"]);
                    //TODO: set login user name. FormsAuthentication.SetCookies
                    Response.Redirect(redirectURL);
                //}
            }
            else
            {
                loginValidation.IsValid = false;
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            //if (!IsTermsAccepted())
            //    return;

            if (!Isvalididate())
                return;
            Guid userGuid1 = Guid.NewGuid();
            string userGuid = userGuid1.ToString();
            if (!SendEmail1(userGuid))
            {
                return;
            }
            if (!signUpValidation.ShowMessageBox)
            {
                
                CustomerDetails newCustomer = new CustomerDetails();
                //TODO: use calendar control
                newCustomer.DateOfBirth = DateTime.Parse(txtDateOfBirth.Text);//Exact(txtDD.Text + "/" + txtMM.Text + "/" + txtYYYY.Text, "M/d/yyyy", CultureInfo.InvariantCulture);
                newCustomer.Email = txtEmail.Text;
                newCustomer.FirstName = txtFirstName.Text;
                newCustomer.LastName = txtLastName.Text;
                newCustomer.Password = txtPassword.Text;
                newCustomer.PhoneNumber = txtPhNumberPart1.Text;
                newCustomer.Gender = rbtnMale.Checked ? "M" : "F";
               string local= HttpContext.Current.Request.Url.Port.ToString(); 
                int len;
                byte[] pic;
                //if (Session["FileUpload1"] != null)
                if (ImageUpload.PostedFile != null)
                {
                     len = ImageUpload.PostedFile.ContentLength;
                     pic = new byte[len];
                     ImageUpload.PostedFile.InputStream.Read(pic, 0, len);
                     newCustomer.Image = pic;
                }
                else
                {
                     len = 0;
                     pic = new byte[len];
                     newCustomer.Image = pic;
                }
                
                //ImageUpload.PostedFile.InputStream.Read(pic, 0, len);
                //newCustomer.Image = pic;
                newCustomer.ActivationToken = userGuid.ToString();
                newCustomer.CreatedDate = DateTime.Now;



                bool result = BusinessLogic.CreateNewTempCustomer(newCustomer);
                //if (result)
                //{

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
                if (result)
                {
                    //TODO: set login user name. FormsAuthentication.SetCookies
                    //string user;

                    //Session["roleID"] = 1;

                    //int userID = BusinessLogic.GetNewUserID();
                    //Session["userID"] = userID;
                    //if ((user = BusinessLogic.GetLoggedInUserName(txtEmail.Text, txtPassword.Text)) != null)
                    //{
                    //    Session["userName"] = user;
                    Response.Redirect("~/CPA/VerifyEmail.aspx?Email=" + txtEmail.Text);



                }
            }
        }
         private bool SendEmail1(string  userGuid)
        {
            bool result;
            var result1 = BusinessLogic.GetEmailDetail("5");
            string FromAddress = result1.Tables[0].Rows[0]["EmailFromAddress"].ToString();

            string sendtext = result1.Tables[0].Rows[0]["EmailContent"].ToString();
            sendtext = sendtext.Replace("{UserName}", txtFirstName.Text);

            sendtext = sendtext.Replace("{Email}", txtEmail.Text);
            sendtext = sendtext.Replace("{Password}", txtPassword.Text);
            sendtext = sendtext.Replace("{userGuid}", userGuid.ToString());
            sendtext = sendtext.Replace("{CPAID}", Request.QueryString["CPAID"].ToString());
            sendtext = sendtext.Replace("{Schedule}", Server.UrlEncode(lblScheduleDate.Text));
            sendtext = sendtext.Replace("{Purpose}", lblPurposeOfVisit.Text);
            sendtext = sendtext.Replace("{ID}", Request.QueryString["ID"]);


            

            string to = txtEmail.Text;
            string subject = result1.Tables[0].Rows[0]["EmailSubject"].ToString();

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(FromAddress, to, subject, sendtext);
            if (result1.Tables[0].Rows[0]["EmailContentType"].ToString() == "H")
            {
                mail.IsBodyHtml = true;
            }
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(result1.Tables[0].Rows[0]["EmailServerHost"].ToString(), int.Parse(result1.Tables[0].Rows[0]["EmailServerPort"].ToString()));
            client.Credentials = new NetworkCredential(FromAddress, result1.Tables[0].Rows[0]["EmailPassword"].ToString());
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
                result = true;

            }
            catch (SmtpException se)
            {

                CustomValidator2.IsValid = false;
                result = false;

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
                //Response.Write(se);
            }
               return result;
        }
            
        //private bool SendEmail1(Guid userGuid)
        //{
        //    bool result;
        //    //Guid userGuid = Guid.NewGuid();
        //    SmtpSection cfg = NetSectionGroup.GetSectionGroup(WebConfigurationManager.OpenWebConfiguration("~/web.config")).MailSettings.Smtp;

        //    string FromAddress = cfg.From;
        //    string sendtext = "To confirm <a href= http://localhost:7988/CPA/ValidatedUser.aspx?Token=" + userGuid + ">Verify your account<a>";
        //    string to = txtEmail.Text;
        //    string subject = "Verification";

        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(FromAddress, to, subject, sendtext);
        //    mail.IsBodyHtml = true;
        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(cfg.Network.Host, cfg.Network.Port);
        //    client.Credentials = new NetworkCredential(FromAddress, cfg.Network.Password);
        //    client.EnableSsl = true;
        //    try
        //    {
        //        client.Send(mail);
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
        //        result = true;

        //    }
        //    catch (SmtpException se)
        //    {

        //        CustomValidator2.IsValid = false;
        //        result = false;

        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
        //        //Response.Write(se);
        //    }
        //    return result;
        //}
        protected bool Isvalididate()
        {
            bool result;
            int count = 0;

            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || txtFirstName.Text.Contains(' '))
            {
                CustValFisrtName.IsValid = false;
                CustValFisrtName.ErrorMessage = "First name should not contain spaces";
                result = false;
                count = count + 1;
            }

            else if (System.Text.RegularExpressions.Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z]{1,25}$"))
            {
                CustValFisrtName.IsValid = true;
                result = true;
            }
            else
            {
                CustValFisrtName.IsValid = false;
                CustValFisrtName.ErrorMessage = "Only Alphabet allowed here";
                result = false;
                count = count + 1;
            }


            if (true == string.IsNullOrEmpty(txtLastName.Text))
            {
                CustValLastName.IsValid = false;
                CustValLastName.ErrorMessage = "Last name must not be empty!";
                result = false;
                count = count + 1;
            }
            else if(txtLastName.Text.Contains(' '))
            {
                CustValLastName.IsValid = false;
                CustValLastName.ErrorMessage = "Last name should not contain spaces";
                result = false;
                count = count + 1;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z]{1,25}$"))
            {
                CustValLastName.IsValid = true;
                result = true;
            }
            else
            {
                CustValFisrtName.IsValid = false;
                CustValFisrtName.ErrorMessage = "Only Alphabet allowed here";
                result = false;
                count = count + 1;
            }
            //if (CaptchaUserControl.Text == txtCaptchaText.Text)//txtCaptchaText.Text.ToString()))
            //{
            //    lblStatus.Visible = true;

            //    lblStatus.Text = "Success";
            //    lblStatus.ForeColor = System.Drawing.Color.Green;
            //    result = true;
            //}
            //else
            //{
            //    lblStatus.Visible = true;
            //    lblStatus.Text = "Failure";
            //    lblStatus.ForeColor = System.Drawing.Color.Red;

            //    result = false;
            //    count = count + 1;
            //}
            if (cbAcceptTerms.Checked)
            {
                CheckBoxRequired.IsValid = cbAcceptTerms.Checked;
                result = true;
            }
            else
            {
                CheckBoxRequired.IsValid = false;
                count = count + 1;
                result = false;
            }
            if (count == 0)
                return true;
            else
                return false;
        }
        private bool IsTermsAccepted()
        {
            CheckBoxRequired.IsValid = cbAcceptTerms.Checked;
            return CheckBoxRequired.IsValid;
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            int userID = int.Parse(Session["userID"].ToString());


            string redirectURL = string.Format("~/CPA/PersonalInformation.aspx?CPAID={0}&Schedule={1}&Purpose={2}&UserID={3}&ID={4}", Request.QueryString["CPAID"], lblScheduleDate.Text,
                            lblPurposeOfVisit.Text, userID, Request.QueryString["ID"]);
            Response.Redirect(redirectURL);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string redirectURL = string.Format("~/CPA/BookAppointment.aspx?CPAID={0}&Schedule={1}&ID={2}", Request.QueryString["CPAID"], lblScheduleDate.Text, Request.QueryString["ID"]);
            Response.Redirect(redirectURL);
        }

        protected void btnRefineSearch_Click(object sender, EventArgs e)
        {

            //string specialityID = ddlSpeciality.SelectedItem.Value;
            //string zipCode = string.IsNullOrEmpty(txtZipCode.Text) || txtZipCode.Text.Equals(@"Enter Zip Code\City") ? string.Empty : txtZipCode.Text;

            //string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?SpecialityID={0}&ZipCode={1}", specialityID, zipCode);
            //Response.Redirect(redirectQuery);
             string city = txtCity.Value;
            string zipCode = txtZipCode.Value;
            string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?City={0}&ZipCode={1}", city, zipCode);
            Response.Redirect(redirectQuery);
            //Response.Redirect("~/Default.aspx");
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
          
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
           
        }

        protected void lnkBtnLoggedOut_Click(object sender, EventArgs e)
        {
            Session["userName"] = null;
            Session["userID"] = null;
            Session["roleID"] = null;
            string redirectURL = string.Format("~/CPA/CheckNewUser.aspx?CPAID={0}&Schedule={1}&Purpose={2}&ID={3}", Request.QueryString["CPAID"], Request.QueryString["Schedule"], purpose, Request.QueryString["ID"]);
            Response.Redirect(redirectURL);
            pnlLoggedIn.Visible = false;
            pnlNotLoggedIn.Visible = true;
        }

        protected void btnPreYear_Click(object sender, EventArgs e)
        {
           
            
        }

        protected void btnNextYear_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnRegisteredUser_Click1(object sender, EventArgs e)
        {
            pnlNewUser.Visible = false;
            pnlRegisteredUser.Visible = true;
            btnBack.Visible = false;
        }

        protected void btnNewUser_Click1(object sender, EventArgs e)
        {
            pnlNewUser.Visible = true;
            pnlRegisteredUser.Visible = false;
            btnBack.Visible = false;
        }
    }    
    
}