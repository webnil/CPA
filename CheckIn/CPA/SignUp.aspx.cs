using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Web.UI.HtmlControls;
namespace CheckIn.Web_Pages
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillDateDropdownList();
            }
        }

        //private void FillDateDropdownList()
        //{
        //    ddlYear.Items.Insert(0, "Select Year");
        //    int index = 1;
        //    for (int Year = 1970; Year <= DateTime.Now.Year; Year++)
        //    {
        //        ListItem li = new ListItem(Year.ToString(), Year.ToString());
        //        ddlYear.Items.Insert(index, li);
        //        index++;
        //    }

        //    var months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
        //    ddlMonth.DataSource = months;
        //    ddlMonth.DataBind();
        //    ddlMonth.Items.Insert(0, "Select Month");
        //}

        //public void FillDays()
        //{
        //    ddlDay.Items.Clear();
        //    if (ddlYear.SelectedValue == "Select Year" || ddlMonth.SelectedValue == "Select Month")
        //        return;
        //    //getting numbner of days in selected month & year
        //    int year = Convert.ToInt32(ddlYear.SelectedValue);
        //    int month = Convert.ToInt32(ddlMonth.SelectedIndex);
        //    int noofdays = DateTime.DaysInMonth(year, month);

        //    //Fill days
        //    ddlDay.Items.Insert(0, "Select Day");
        //    for (int i = 1; i <= noofdays; i++)
        //    {
        //        ddlDay.Items.Add(i.ToString());
        //    }
        //}
        protected void CheckBoxRequired_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = cbTermCondition.Checked;
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (cbTermCondition.Checked == false)
            //if (!Isvalididate())
            {
                CheckBoxRequired.IsValid = false;
                return;
            }
            if (!signUpValidation.ShowMessageBox)
            {
                Guid userGuid1 = Guid.NewGuid();
                String userGuid = userGuid1.ToString();
                if (!SendEmail1(userGuid))
                {
                    return;
                }
                CustomerDetails newCustomer = new CustomerDetails();
                //TODO: use calendar control
                newCustomer.DateOfBirth = DateTime.Parse(GetSelectedDoB());
                newCustomer.Email = txtEmail.Text;
                newCustomer.FirstName = txtFirstName.Text;
                newCustomer.LastName = txtLastName.Text;
                newCustomer.Password = txtPassword.Text;
                // newCustomer.PhoneNumber = txtPhoneNumber.Text;
                newCustomer.PhoneNumber = txtPhNumberPart1.Text ;
                newCustomer.Gender = rbtnMale.Checked ? "M" : "F";
                //if (ImageUpload.PostedFile != null)
                //{
                //    int len = ImageUpload.PostedFile.ContentLength;
                //    byte[] pic = new byte[len];
                //    ImageUpload.PostedFile.InputStream.Read(pic, 0, len);
                //    newCustomer.Image = pic;
                //}
                newCustomer.ActivationToken = userGuid.ToString();
                newCustomer.CreatedDate = DateTime.Now;

                bool result = BusinessLogic.CreateNewTempCustomer(newCustomer);
                if (result)
                {
                    Response.Redirect("~/CPA/VerifyEmail.aspx?Email=" + txtEmail.Text);
                }
            }

        }

        private string GetSelectedDoB()
        {
            return txtDateOfBirth.Text==string.Empty?"1/1/2001":txtDateOfBirth.Text;
            //return "1/1/2001";
            //TODO:
            // Read dropdown values and return dob
        }

        private bool SendEmail1(string userGuid)
        {
            bool result;
            var result1 = BusinessLogic.GetEmailDetail("3");
            string FromAddress = result1.Tables[0].Rows[0]["EmailFromAddress"].ToString();

            string sendtext = result1.Tables[0].Rows[0]["EmailContent"].ToString();
            sendtext = sendtext.Replace("{UserName}", txtFirstName.Text.ToString());

            sendtext = sendtext.Replace("{Email}", txtEmail.Text);
            sendtext = sendtext.Replace("{Password}", txtPassword.Text);
            sendtext = sendtext.Replace("{userGuid}", userGuid.ToString());


            string to = txtEmail.Text;
            string subject = result1.Tables[0].Rows[0]["EmailSubject"].ToString();

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(FromAddress, to, subject, sendtext);
            if (result1.Tables[0].Rows[0]["EmailContentType"].ToString() == "H")
            {
                mail.IsBodyHtml = true;
            }
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(result1.Tables[0].Rows[0]["EmailServerHost"].ToString(), int.Parse(result1.Tables[0].Rows[0]["EmailServerPort"].ToString()));
            //client.Credentials = new NetworkCredential(FromAddress, result1.Tables[0].Rows[0]["EmailPassword"].ToString());
            //client.EnableSsl = true;
            try
            {
               
                client.Send(mail);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "alert('Message Sent ... ');", true);
                result = true;

            }
            catch (SmtpException se)
            {
                throw se;
                //CustomValidator2.IsValid = false;
                result = false;
            }

            return result;
        }
        private bool IsTermsAccepted()
        {
            return cbTermCondition.Checked;
            //valTermsConditions.IsValid = cbTermCondition.Checked;

            //return valTermsConditions.IsValid;
        }

        protected void checkCheckBox(object source, ServerValidateEventArgs args)
        {
            if (cbTermCondition.Checked == true)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected bool Isvalididate()
        {
            bool result;
            int count = 0;
            //valTermsConditions.IsValid = cbTermCondition.Checked;

            //if (!valTermsConditions.IsValid)
            //{
            //    count = count + 1;
            //}

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

            if (count == 0)
                return true;
            else
                return false;
        }

        protected void cbTermCondition_CheckedChanged(object sender, EventArgs e)
        {
            Page.Validate("signUpValidation");

            if (!IsTermsAccepted())
                return;

            if (!signUpValidation.ShowMessageBox)
            {
                btnSignUp.Enabled = cbTermCondition.Checked;
                txtPassword.Focus();
            }
            txtPassword.Text = txtPassword.Text;

        }

        //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    FillDays();
        //}

    }
}