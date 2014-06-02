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
    public partial class CancelCPAAppointment : System.Web.UI.Page
    {
       
            
        #region Private Members

        private string cpid = string.Empty;
        private string schedule = string.Empty;
        private string id = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.QueryString.Count > 0)
            { 
                RefreshAppointment();
            }
        }
        //private void RefreshAppointment()
        //{
        
        // int result = BusinessLogic.ReScheduleAppointment(Request.QueryString["ID"]);
        //    if (result != 0)
        //    {  
        //        Response.Redirect(String.Format("~/CPA/ViewScheduledAppointments.aspx?UserID={0}" , Session["userID"]));
        //    }
        //}
        private void RefreshAppointment()
        {
           // ScriptManager.RegisterStartupScript(this,this.Page.GetType(),"Alert","Confirm(' 
            var result = BusinessLogic.CancelAppointment(int.Parse(Request.QueryString["ID"]));
            if (result == null || result.Tables[0].Rows.Count == 0)
            {
                string mes = "invalid";
            }
            else
            { bool IsMailSent;
            var result1=BusinessLogic.GetEmailDetail("2");
            string FromAddress = result1.Tables[0].Rows[0]["EmailFromAddress"].ToString();

            string sendtext = result1.Tables[0].Rows[0]["EmailContent"].ToString();
            sendtext = sendtext.Replace("{UserName}", result.Tables[0].Rows[0]["FirstName"].ToString());

            sendtext = sendtext.Replace("{CPAName}", result.Tables[0].Rows[0]["Name"].ToString());
            sendtext = sendtext.Replace("{CPADetails}", result.Tables[0].Rows[0]["CompanyName"].ToString());
            sendtext = sendtext.Replace("{Address1}", result.Tables[0].Rows[0]["Address1"].ToString());
            sendtext = sendtext.Replace("{City}", result.Tables[0].Rows[0]["City"].ToString());
            sendtext = sendtext.Replace("{State}", result.Tables[0].Rows[0]["State"].ToString());
            sendtext = sendtext.Replace("{ZipCode}", result.Tables[0].Rows[0]["ZipCode"].ToString());

            DateTime date = DateTime.Parse(result.Tables[0].Rows[0]["Date"].ToString());
            string ScheduledDate= date.ToString("dddd, dd, MMMM") + " - " + result.Tables[0].Rows[0]["Time"].ToString();
            sendtext = sendtext.Replace("{ScheduledDate}", ScheduledDate);
            sendtext = sendtext.Replace("{PurposeOfVisit}", result.Tables[0].Rows[0]["PurposeOfVisit"].ToString());
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
                IsMailSent= true;

            }
            catch (SmtpException se)
            {

                
                IsMailSent = false;

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
                //Response.Write(se);
            }
            if (IsMailSent)
            {
                Response.Redirect(String.Format("~/CPA/ViewScheduledAppointments.aspx?UserID={0}", Session["userID"]));
            }
            }
        }
        
        //private bool SendEmail(string Email)
        //{
        //    bool result;
        //    //Guid userGuid = Guid.NewGuid();
        //    SmtpSection cfg = NetSectionGroup.GetSectionGroup(WebConfigurationManager.OpenWebConfiguration("~/web.config")).MailSettings.Smtp;
            
        //    string FromAddress = cfg.From;
        //    string sendtext = " Appointment is Cancel ";
        //    string to = Email;
        //    string subject = "Cancel Appointment";

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

                
        //        result = false;

        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
        //        //Response.Write(se);
        //    }
        //    return result;
        //}
       
    }
}