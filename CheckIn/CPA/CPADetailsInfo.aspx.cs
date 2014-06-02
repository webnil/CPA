using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class CPADetailsInfo : System.Web.UI.Page
    {
        public string cpaid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count == 1)
                {
                    cpaid = Request.QueryString["CPAID"].ToString();
                }
                else
                {
                    cpaid = Session["CpaID"].ToString();
                }
                RefreshUserDetail();
            }
        }
        private void RefreshUserDetail()
        {
            var result = BusinessLogic.GetCPAProfileDetail(int.Parse(cpaid.ToString()));
            //var result = BusinessLogic.GetuserProfileDetail(int.Parse("3"));
            if (result == null || result.Tables[0].Rows.Count == 0)
                return;
            lblCompanyName.Text = result.Tables[0].Rows[0]["CompanyName"].ToString();
            lblOfficeAddress1.Text = result.Tables[0].Rows[0]["Address1"].ToString();
            lblOfficeAddress2.Text = result.Tables[0].Rows[0]["Address2"].ToString();
            lblPractingCity.Text = result.Tables[0].Rows[0]["City"].ToString();
            lblPractingState.Text = result.Tables[0].Rows[0]["State"].ToString();
            lblZipCode.Text = result.Tables[0].Rows[0]["ZipCode"].ToString();
            lblEmail.Text = result.Tables[0].Rows[0]["Email"].ToString();

            lblName.Text = result.Tables[0].Rows[0]["FirstName"].ToString()+" "+result.Tables[0].Rows[0]["LastName"].ToString();
            //string str = string.Format("~/Doc/ImageCSharp.aspx?CPAID={0}", Session["userID"]);
            Image1.ImageUrl = "Handler.ashx?QueryCPAID=" + cpaid.ToString();
            //lblDOB.Text = result.Tables[0].Rows[0]["Month"].ToString() + "/" + result.Tables[0].Rows[0]["Date"].ToString() + "/" +result.Tables[0].Rows[0]["Year"].ToString();
      
            if (result.Tables[0].Rows[0]["Gender"].ToString() == "M")
            {
                lblGender.Text = "Male";

            }
            else
            {
                lblGender.Text = "Female";
            }

            lblPhoneNumber.Text = result.Tables[0].Rows[0]["Phone"].ToString();
            lblSpeciality.Text = BusinessLogic.GetSpeciatityByID( result.Tables[0].Rows[0]["SpecialityID"].ToString());
           


        }
    }
}