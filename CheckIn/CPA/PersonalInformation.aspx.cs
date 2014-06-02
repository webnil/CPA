using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class PersonalInformation : System.Web.UI.Page
    {
        #region Private Members

        private string cpid = string.Empty;
        private string scheduledTime = string.Empty;
        private string scheduledDate = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString.Count > 0)
            {
                cpid = Request.QueryString["CPAID"];
                imgCPA.ImageUrl = "Handler.ashx?QueryCPAID=" + cpid;
                FillAllSpeciality();
                RefreshPageData();
                pnlBookedAppointment.Visible = false;
                pnlMaxBook.Visible = false;

            }
        }
        private void FillAllSpeciality()
        {
            var result = BusinessLogic.GetAllSpecializationList();
            //ddlSpeciality.DataSource = result.Tables[0];
            //ddlSpeciality.DataTextField = result.Tables[0].Columns["Speciality"].ColumnName.ToString();
            //ddlSpeciality.DataValueField = result.Tables[0].Columns["ID"].ColumnName.ToString();
            //ddlSpeciality.DataBind();
        }
        private void RefreshPageData()
        {
            var result = BusinessLogic.GetCPADetails(int.Parse(cpid));
            if (result == null || result.Tables[0].Rows.Count == 0)
                return;

            lnkCPADetail.Text = result.Tables[0].Rows[0]["CompanyName"].ToString();
            lnkCPADetail.NavigateUrl = "CPADetailsInfo.aspx?CPAID=" + cpid;
            lblName.Text = result.Tables[0].Rows[0]["FirstName"].ToString() + " " + result.Tables[0].Rows[0]["LastName"].ToString();
            lblAddress1.Text = result.Tables[0].Rows[0]["Address1"].ToString();
            lblAddress2.Text = result.Tables[0].Rows[0]["Address2"].ToString();
            lblState.Text = result.Tables[0].Rows[0]["State"].ToString();
            lblZipCode.Text = result.Tables[0].Rows[0]["ZipCode"].ToString();
            lblCPAName.Text = result.Tables[0].Rows[0]["LastName"].ToString() + ", " + result.Tables[0].Rows[0]["FirstName"].ToString();
            lblScheduleDate.Text = Request.QueryString["Schedule"];
            lblPurposeOfVisit.Text = Request.QueryString["Purpose"];

            RefreshCustomerDetails();
        }

        private void RefreshCustomerDetails()
        {
            var result = BusinessLogic.GetCustomerDetails(Request.QueryString["UserID"]);

            if (result == null || result.Tables[0].Rows.Count == 0)
                return;

            lblCustomerName.Text = result.Tables[0].Rows[0]["LastName"].ToString() + ", " + result.Tables[0].Rows[0]["FirstName"].ToString();
            lblPhoneNumber.Text = result.Tables[0].Rows[0]["Phone"].ToString();

        }

        protected void btnEditContact_Click(object sender, EventArgs e)
        {
            var result = BusinessLogic.GetCustomerDetails(Session["userID"].ToString());

            if (result == null || result.Tables[0].Rows.Count == 0)
                return;
            txtFirstName.Text = result.Tables[0].Rows[0]["FirstName"].ToString();
            txtLastName.Text = result.Tables[0].Rows[0]["LastName"].ToString();
            string phone = result.Tables[0].Rows[0]["Phone"].ToString();
            txtPhNumberPart1.Text = phone;
            pnlEditCustomerDetails.Visible = true;
            pnlCustomerDetails.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEditCustomerDetails.Visible = false;
            pnlCustomerDetails.Visible = true;
        }
        protected bool Isvalididate()
        {
            bool result;
            int count = 0;

            if (System.Text.RegularExpressions.Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z]{1,25}$"))
            {
                //CustValFisrtName.IsValid = true;
                result = true;
            }
            else
            {
                //CustValFisrtName.IsValid = false;
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Invalid First Name", "alert('Only Alphabet allowed in First Name!');", true);
                //CustValFisrtName.ErrorMessage = "Only Alphabet allowed here";
                result = false;
                count = count + 1;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z]{1,25}$"))
            {
                //CustValLastName.IsValid = true;
                result = true;
            }
            else
            {
                //CustValLastName.IsValid = false;
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Invalid Last Name", "alert('Only Alphabet allowed in Last Name!');", true);
                //CustValFisrtName.ErrorMessage = "Only Alphabet allowed here";
                result = false;
                count = count + 1;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(txtPhNumberPart1.Text , @"^[2-9]\d{2}-\d{3}-\d{4}$"))
            {
                //CustPhoneNumber.IsValid = true;
                result = true;
            }
            else
            {
                //CustPhoneNumber.IsValid = false;
                count = count + 1;
            }

            if (count == 0)
                return true;
            else
                return false;
        }
        protected void btnSaveContactInformation_Click(object sender, EventArgs e)
        {
            if (!Isvalididate())
                return;
            pnlEditCustomerDetails.Visible = false;
            pnlCustomerDetails.Visible = true;

            string phoneNumber = txtPhNumberPart1.Text ;
            string updateQuery = string.Format("UPDATE Customer SET FirstName='{0}', LastName='{1}',Phone='{2}' WHERE CustomerID={3}", txtFirstName.Text, txtLastName.Text, phoneNumber, Request.QueryString["UserID"]);

            DBHelper.ExecuteNonQuery(updateQuery);

            RefreshCustomerDetails();
        }

        protected void btnBookAppointment_Click(object sender, EventArgs e)
        {
            string scheduleDateID = Request.QueryString["ID"];
            if (BusinessLogic.GetSCheduleMaxAppointment(Session["userID"].ToString()) >= BusinessLogic.GetFirmSettingValue("MaxAppointmentCount"))
            {
                pnlMaxBook.Visible = true;
                //pnlCPADetails.Visible = false;
                pnlEditCustomerDetails.Visible = false;
                pnlCustomerDetails.Visible = false;
                pnlBookedAppointment.Visible = false;
                return;

            }
            if (!BusinessLogic.IsBookAppointmentAvailable(Request.QueryString["ID"].ToString()))
            {
                pnlBookedAppointment.Visible = true;
                pnlMaxBook.Visible = false;
                pnlEditCustomerDetails.Visible = false;
                pnlCustomerDetails.Visible = false;
                return;
            }

            bool result = BusinessLogic.BookNewAppointment(Request.QueryString["CPAID"], Request.QueryString["UserID"], lblPurposeOfVisit.Text, scheduleDateID);
            if (result)
            {
                SendEmail();
                string redirectURL = string.Format("~/CPA/FinishBookingApp.aspx?CPAID={0}&Schedule={1}&Purpose={2}&ID={3}&UserID={4}", Request.QueryString["CPAID"], lblScheduleDate.Text, lblPurposeOfVisit.Text, Request.QueryString["ID"], Request.QueryString["UserID"]);
                Response.Redirect(redirectURL);
            }
        }

        private bool SendEmail()
        {
            bool result;
            //Guid userGuid = Guid.NewGuid();
            //SmtpSection cfg = NetSectionGroup.GetSectionGroup(WebConfigurationManager.OpenWebConfiguration("~/web.config")).MailSettings.Smtp;
            var result1 = BusinessLogic.GetEmailDetail("1");
            string FromAddress = result1.Tables[0].Rows[0]["EmailFromAddress"].ToString();

            string sendtext = result1.Tables[0].Rows[0]["EmailContent"].ToString();
            sendtext = sendtext.Replace("{UserName}", BusinessLogic.GetLoggedInName(int.Parse(Session["userID"].ToString())));

            sendtext = sendtext.Replace("{CPAName}", lblCPAName.Text);
            sendtext = sendtext.Replace("{CPADetails}", lnkCPADetail.Text);
            sendtext = sendtext.Replace("{Address1}", lblAddress1.Text);
            sendtext = sendtext.Replace("{City}", lblCity.Text);
            sendtext = sendtext.Replace("{State}", lblState.Text);
            sendtext = sendtext.Replace("{ZipCode}", lblZipCode.Text);
            sendtext = sendtext.Replace("{ScheduledDate}", lblScheduleDate.Text);
            sendtext = sendtext.Replace("{PurposeOfVisit}", lblPurposeOfVisit.Text);
            sendtext = sendtext.Replace("{UserID}", Session["userID"].ToString());

            string to = BusinessLogic.GetLoggedInEmailID(int.Parse(Session["userID"].ToString()));
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


                result = false;

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
                //Response.Write(se);
            }
            return result;
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

        }

        protected void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || txtFirstName.Text.Contains(' '))
            {
                //CustValFisrtName.IsValid = false;
                //CustValFisrtName.ErrorMessage = "First name should not contain spaces";
                return;

            }

            else if (System.Text.RegularExpressions.Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z]{1,25}$"))
            {
                //CustValFisrtName.IsValid = true;


            }
            else
            {
                //CustValFisrtName.IsValid = false;
                //CustValFisrtName.ErrorMessage = "Only Alphabet allowed here";
                return;


            }

        }

        protected void txtLastName_TextChanged(object sender, EventArgs e)
        {


            if (string.IsNullOrWhiteSpace(txtLastName.Text) || txtFirstName.Text.Contains(' '))
            {
                //CustValLastName.IsValid = false;
                //CustValLastName.ErrorMessage = "Last name should not contain spaces";

                return;
            }

            else if (System.Text.RegularExpressions.Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z]{1,25}$"))
            {
                //CustValLastName.IsValid = true;


            }
            else
            {
                //CustValFisrtName.IsValid = false;
                //CustValFisrtName.ErrorMessage = "Only Alphabet allowed here";
                return;

            }

        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            string city = string.Empty;
            string zipCode = string.Empty;
            if (Session["city"] != null)
            {
                city = Session["city"].ToString();
            }
            if (Session["zipCode"] != null)
            {
                zipCode = Session["zipCode"].ToString();
            }

            string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?City={0}&ZipCode={1}", city, zipCode);
            Response.Redirect(redirectQuery);
        }

        protected void btnAlreadyBook_Click(object sender, EventArgs e)
        {
            string city = string.Empty;
            string zipCode = string.Empty;
            if (Session["city"] != null)
            {
                city = Session["city"].ToString();
            }
            if (Session["zipCode"] != null)
            {
                zipCode = Session["zipCode"].ToString();
            }

            string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?City={0}&ZipCode={1}", city, zipCode);
            Response.Redirect(redirectQuery);
        }
    }
}