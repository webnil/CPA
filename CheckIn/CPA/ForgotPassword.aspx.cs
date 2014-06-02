using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.Web_Pages
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            bool isValidEmail = BusinessLogic.IsValidEmail(txtEmail.Text);
            RegularExpressionValidator1.IsValid = isValidEmail;
            if(isValidEmail)
            {
                if (SendEmail(txtEmail.Text))
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Account Details", "alert('Your account details has been sent to your registered email ID.');", true);
                }
            }
        }

        private bool SendEmail(string emailID)
        {
            bool result;
            var result1 = BusinessLogic.GetPassword("6", emailID);
            string FromAddress = result1.Tables[0].Rows[0]["EmailFromAddress"].ToString();

            string sendtext = result1.Tables[0].Rows[0]["EmailContent"].ToString();
            string userPassword = result1.Tables[0].Rows[0]["Password"].ToString();

            sendtext = sendtext.Replace("{UserName}", emailID);
            sendtext = sendtext.Replace("{UserPassword}", userPassword);


            string to = txtEmail.Text;
            string subject = result1.Tables[0].Rows[0]["EmailSubject"].ToString();

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(FromAddress, to, subject, sendtext);
            if (result1.Tables[0].Rows[0]["EmailContentType"].ToString() == "H")
            {
                mail.IsBodyHtml = true;
            }
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(result1.Tables[0].Rows[0]["EmailServerHost"].ToString(), int.Parse(result1.Tables[0].Rows[0]["EmailServerPort"].ToString()));
            try
            {
                client.Send(mail);
                result = true;

            }
            catch (SmtpException se)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Account Details", "alert('Something went wrong.. Please try later.');", true);
                throw se;
                //CustomValidator2.IsValid = false;
                result = false;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Account Details", "alert('Something went wrong.. Please try later.');", true);
                throw ex;

            }
            return result;
        }
    }
}