using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class ValidatedUser1 : System.Web.UI.Page
    {
       private string activationToken;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string activationToken = Request.QueryString["Token"];
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
            var result = BusinessLogic.GetTempCPADetails(activationToken);
            DateTime now = DateTime.Now;
            TimeSpan weekSpan = now.AddDays(7) - now;

             DateTime ValidDate=DateTime.Now.Subtract(weekSpan);

           if (result == null || result.Tables[0].Rows.Count == 0)
            {
               IsValid = false;
           }
           else if  (result.Tables[0].Rows[0]["ActivationToken"].ToString() == activationToken && (DateTime)result.Tables[0].Rows[0]["CreatedDate"] >= ValidDate)
                {

                CPADetails newCustomer = new CPADetails();
               
                newCustomer.Email = result.Tables[0].Rows[0]["Email"].ToString();
                newCustomer.Password = result.Tables[0].Rows[0]["Password"].ToString();
                newCustomer.FirstName = result.Tables[0].Rows[0]["FirstName"].ToString();
                newCustomer.LastName = result.Tables[0].Rows[0]["LastName"].ToString();
                newCustomer.DateOfBirth = (DateTime)result.Tables[0].Rows[0]["DateOfBirth"];
                newCustomer.Gender = result.Tables[0].Rows[0]["Gender"].ToString();
                newCustomer.PhoneNumber = result.Tables[0].Rows[0]["Phone"].ToString();
                newCustomer.Image = (byte[])result.Tables[0].Rows[0]["CPAImage"];

                newCustomer.CompanyName = result.Tables[0].Rows[0]["CompanyName"].ToString();
                newCustomer.Address1= result.Tables[0].Rows[0]["Address1"].ToString();
                newCustomer.Address2= result.Tables[0].Rows[0]["Address2"].ToString();
                newCustomer.State = result.Tables[0].Rows[0]["State"].ToString();
                newCustomer.City = result.Tables[0].Rows[0]["City"].ToString();
                newCustomer.ZipCode = result.Tables[0].Rows[0]["ZipCode"].ToString();
               // newCustomer.SpecialityID = result.Tables[0].Rows[0]["SpecialityID"].ToString();
                newCustomer.Speciality = result.Tables[0].Rows[0]["Speciality"].ToString();
                newCustomer.Latitude = double.Parse(result.Tables[0].Rows[0]["Latitude"].ToString());
                newCustomer.Longitude = double.Parse(result.Tables[0].Rows[0]["Longitude"].ToString());

                
               bool result1 = BusinessLogic.CreateNewCPA(newCustomer);
                if (result1)
                    {
                        int count = BusinessLogic.DeleteTempCPADetails(activationToken);
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
    }
}