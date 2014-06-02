using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class UserProfile : System.Web.UI.Page
    {
        #region

        private string specialityID = string.Empty;
        private string zipCode = string.Empty;

        #endregion
        public string userID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                if (Request.QueryString["UserID"] != null)
                {
                    userID = Request.QueryString["UserID"];
                    Session["userID"] = userID;
                }
                else
                {
                    userID = Session["userID"].ToString();
                }
                 FillAllSpeciality();
                 RefreshUserProfile();
            }
        }
        private void RefreshUserProfile()
        {
            var result = BusinessLogic.GetuserProfileDetail(int.Parse(Session["userID"].ToString()));
            if (result == null || result.Tables[0].Rows.Count == 0)
                return;
            txtEmail.Text = result.Tables[0].Rows[0]["Email"].ToString();
           
            txtFirstName.Text = result.Tables[0].Rows[0]["FirstName"].ToString();
            txtLastName.Text = result.Tables[0].Rows[0]["LastName"].ToString();
            txtDateOfBirth.Text = result.Tables[0].Rows[0]["Month"].ToString() + "/" + result.Tables[0].Rows[0]["Date"].ToString() + "/" + result.Tables[0].Rows[0]["Year"].ToString();
            if (result.Tables[0].Rows[0]["Gender"].ToString() == "M")
            {
                rbtnMale.Checked = true;

            }
            else
            {
                rbtnFemale.Checked = true;
            }

            txtPhNumberPart1.Text = result.Tables[0].Rows[0]["Phone"].ToString();
            imgUser.ImageUrl = "UserHandler.ashx?UserID=" + Session["userID"];

        }
        private void FillAllSpeciality()
        {
            var result = BusinessLogic.GetAllSpecializationList();
        }

        private void UpdateUserProfile()
        {
            var result = BusinessLogic.GetuserProfileDetail(int.Parse(Session["userID"].ToString()));//(Session["userID"].ToString()));
            if (result == null || result.Tables[0].Rows.Count == 0)
                return;
            txtEmail.Text = result.Tables[0].Rows[0]["Email"].ToString();
            txtFirstName.Text = result.Tables[0].Rows[0]["FirstName"].ToString();
            txtLastName.Text = result.Tables[0].Rows[0]["LastName"].ToString();
            txtDateOfBirth.Text = result.Tables[0].Rows[0]["Month"].ToString() + "/" + result.Tables[0].Rows[0]["Date"].ToString() + "/" + result.Tables[0].Rows[0]["Year"].ToString();
            if (result.Tables[0].Rows[0]["Gender"].ToString() == "M")
            {
                rbtnMale.Checked = true;

            }
            else
            {
                rbtnFemale.Checked = true;
            }
            string phone = result.Tables[0].Rows[0]["Phone"].ToString();
            txtPhNumberPart1.Text = phone;
            //imgEditUser.ImageUrl = "UserHandler.ashx?UserID=" + Session["userID"]; 
            imgUser.ImageUrl = "UserHandler.ashx?UserID=" + Session["userID"]; 
          
        }
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


            if (string.IsNullOrWhiteSpace(txtLastName.Text) || txtLastName.Text.Contains(' '))
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


            if (System.Text.RegularExpressions.Regex.IsMatch(txtPhNumberPart1.Text, @"^[2-9]\d{2}-\d{3}-\d{4}$"))
            {
                CustPhoneNumber.IsValid = true;
                result = true;
            }
            else
            {
                CustPhoneNumber.IsValid = false;
                count = count + 1;
            }
           
            if (count == 0)
                return true;
            else
                return false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Isvalididate())
                return;
            CustomerDetails customer = new CustomerDetails();
            //TODO: use calendar control
            customer.UserID = Session["userID"].ToString();
            //customer.UserID = "3";
            customer.Email = txtEmail.Text;
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastName.Text;

            customer.DateOfBirth = DateTime.Parse(txtDateOfBirth.Text);
            customer.Gender = rbtnMale.Checked ? "M" : "F";
            customer.PhoneNumber = txtPhNumberPart1.Text ;
            if (ImageUpload.HasFile)
            {
                int len = ImageUpload.PostedFile.ContentLength;
                byte[] pic = new byte[len];
                ImageUpload.PostedFile.InputStream.Read(pic, 0, len);
                customer.Image = pic;

            }
            else
            {
                customer.Image = BusinessLogic.GetUserImage(int.Parse(Session["userID"].ToString()));
            }
            if(customer.Image == null)
            {
                customer.Image = new byte[0];
            }
             bool result=BusinessLogic.UpdateCustomerDetails(customer);

             if (result)
             {
                Session["userName"] = txtFirstName.Text;
                ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "Account Details", "alert('Your account details has been updated successfully!');", true);
                RefreshUserProfile();
             }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RefreshUserProfile();
        
        }

        #region Refine Search

        protected void btnRefineSearch_Click(object sender, EventArgs e)
        {
            GetCurrentSearchFilter();
            RefreshUserProfile();
        }

        private void GetCurrentSearchFilter()
        {
            //string city = txtCity.Value;
            //string zipCode = txtZipCode.Value;
            //string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?City={0}&ZipCode={1}", city, zipCode);
            //Response.Redirect(redirectQuery);
        }
        #endregion

    }
}