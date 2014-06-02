using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.Web_Pages
{
    public partial class CPASignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillAllState();
                txtSpeciality.Visible = false;
                FillSpeciality();
            }
            if (Session["FileUpload"] == null && ImageUpload.HasFile)
            {
                Session["FileUpload"] = ImageUpload;
                lblFile.Text = ImageUpload.FileName;
            }
            // Next time submit and Session has values but FileUpload is Blank 
            // Return the values from session to FileUpload 
            else if (Session["FileUpload"] != null && (!ImageUpload.HasFile))
            {
                ImageUpload = (FileUpload)Session["FileUpload"];
                lblFile.Text = ImageUpload.FileName;
            }
            // Now there could be another sictution when Session has File but user want to change the file 
            // In this case we have to change the file in session object 
            else if (ImageUpload.HasFile)
            {
                Session["FileUpload"] = ImageUpload;
                lblFile.Text = ImageUpload.FileName;
            }
        }

        private void FillAllState()
        {
            var result = BusinessLogic.GetAllStateList();
            ddlState.DataSource = result.Tables[0];
            ddlState.Items.Clear();
            ddlState.Items.Add("--Please Select State--");

            ddlState.DataTextField = result.Tables[0].Columns["StateName"].ColumnName.ToString();
            ddlState.DataValueField = result.Tables[0].Columns["State_Code"].ColumnName.ToString();
            ddlState.DataBind();
        }
        private void FillSpeciality()
        {
            var result = BusinessLogic.GetAllSpecializationList();
            ddlSpeciality.DataSource = result.Tables[0];
            ddlSpeciality.Items.Clear();
            ddlSpeciality.Items.Add("--Please Select Speciality--");
            ddlSpeciality.DataTextField = result.Tables[0].Columns["Speciality"].ColumnName.ToString();
            ddlSpeciality.DataValueField = result.Tables[0].Columns["ID"].ColumnName.ToString();

            ddlSpeciality.DataBind();

            int last = ddlSpeciality.Items.Count;
            ddlSpeciality.Items.Insert(last, "Other");

            ddlSpeciality.Items.FindByValue("Other").Text = "Other";
            ddlSpeciality.Items.FindByValue("Other").Value = (BusinessLogic.GetNewSpecialityID()).ToString();
        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!Isvalididate())
                return;
            Guid userGuid1 = Guid.NewGuid();
            string userGuid = userGuid1.ToString();
            if (!SendEmail1(userGuid))
            {
                return;
            }
            if (Page.IsValid)
            {
                CPADetails newCPA = new CPADetails();
                //TODO: use calendar control
                newCPA.CompanyName = txtCompanyName.Text;
                newCPA.Email = txtEmail.Text;
                newCPA.FirstName = txtFirstName.Text;
                newCPA.LastName = txtLastName.Text;
                newCPA.Password = txtPassword.Text;
                newCPA.Gender = rbtnMale.Checked ? "M" : "F";
                newCPA.DateOfBirth = DateTime.Parse(txtDateOfBirth.Text);
                newCPA.Address1 = txtOfficeAddress1.Text;
                newCPA.Address2 = txtOfficeAddress2.Text;
                newCPA.ZipCode = txtZipCode.Text;
                newCPA.State = ddlState.SelectedItem.Text;
                newCPA.City = ddlCity.SelectedItem.Text;
                newCPA.PhoneNumber = txtPhNumberPart1.Text;
                
                // Find Latitude longitude of CPA from address
                string address = string.Concat(newCPA.Address1, " ", newCPA.Address2, " ", newCPA.City , " " , newCPA.State, " ", newCPA.ZipCode);
                FindCoordinatesOfCPA(address,newCPA);

                //TODO: check how to handle speciality
                // newCPA.SpecialityID = ddlSpeciality.SelectedValue;
                newCPA.Speciality = txtSpeciality.Text;

                if (ImageUpload != null && ImageUpload.PostedFile != null)
                {
                    int len = ImageUpload.PostedFile.ContentLength;
                    byte[] pic = new byte[len];
                    ImageUpload.PostedFile.InputStream.Read(pic, 0, len);
                    newCPA.Image = pic;
                }
                else
                { newCPA.Image = new byte[0]; }

                newCPA.ActivationToken = userGuid.ToString();
                newCPA.CreatedDate = DateTime.Now;

                bool result = BusinessLogic.CreateNewTempCPA(newCPA);
                if (result)
                {

                    Response.Redirect("~/CPA/VerifyEmail.aspx?Email=" + txtEmail.Text);
                }
            }
        }

        protected void FindCoordinatesOfCPA(string address, CPADetails newCPA)
        {
            double latitutde = 0;
            double longitutde = 0;

            string url = "http://maps.google.com/maps/api/geocode/xml?address=" + address + "&sensor=false";
            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);
                    DataTable dtCoordinates = new DataTable();
                    dtCoordinates.Columns.AddRange(new DataColumn[4] { new DataColumn("Id", typeof(int)),
                    new DataColumn("Address", typeof(string)),
                    new DataColumn("Latitude",typeof(string)),
                    new DataColumn("Longitude",typeof(string)) });
                    foreach (DataRow row in dsResult.Tables["result"].Rows)
                    {
                        string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();
                        DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];
                        dtCoordinates.Rows.Add(row["result_id"], row["formatted_address"], location["lat"], location["lng"]);

                        double.TryParse(location["lat"].ToString(), out latitutde);
                        double.TryParse(location["lng"].ToString(), out longitutde);
                        newCPA.Latitude = latitutde;
                        newCPA.Longitude = longitutde;
                    }
                    if (dtCoordinates.Rows.Count > 0)
                    {
                        //pnlScripts.Visible = true;
                        //rptMarkers.DataSource = dtCoordinates;
                        //rptMarkers.DataBind();
                    }
                }
            }
        }

        private bool SendEmail1(string userGuid)
        {
            bool result;
            var result1 = BusinessLogic.GetEmailDetail("4");
            string FromAddress = result1.Tables[0].Rows[0]["EmailFromAddress"].ToString();

            string sendtext = result1.Tables[0].Rows[0]["EmailContent"].ToString();
            sendtext = sendtext.Replace("{UserName}", txtFirstName.Text);

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
        //    string sendtext = "To confirm CPA <a href= http://localhost:7988/CPA/ValidatedUser1.aspx?Token=" + userGuid + ">Verify your account<a>";
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
        private bool IsTermsAccepted()
        {
            return true;
        }
        protected bool Isvalididate()
        {
            bool result;
            int count = 0;

            if (string.IsNullOrEmpty(txtFirstName.Text) == false && Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z]{1,25}$"))
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

            if (string.IsNullOrEmpty(txtLastName.Text) == false && Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z]{1,25}$"))
            {
                CustValLastName.IsValid = true;
                result = true;
            }
            else
            {
                CustValLastName.IsValid = false;
                CustValLastName.ErrorMessage = "Only Alphabet allowed here";
                result = false;
                count = count + 1;
            }
            if (count == 0)
                return true;
            else
                return false;
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stateCode = ddlState.SelectedItem.Value.ToString();
            var result = BusinessLogic.GetAllCityList(stateCode);
            ddlCity.DataSource = result.Tables[0];
            ddlCity.Items.Clear();
            ddlCity.Items.Add("--Please Select City--");
            ddlCity.DataTextField = result.Tables[0].Columns["CityName"].ColumnName.ToString();
            ddlCity.DataValueField = result.Tables[0].Columns["CityID"].ColumnName.ToString();
            ddlCity.DataBind();

        }

        protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSpeciality.SelectedItem.Text == "Other")
            {
                txtSpeciality.Visible = true;
                txtSpeciality.Text = "";

            }
            else
            {
                txtSpeciality.Visible = false;
                txtSpeciality.Text = ddlSpeciality.SelectedItem.Text;
            }
        }
    }
}