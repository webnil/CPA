using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CheckIn
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAllSpeciality();
            }
            // RegZipCode.ValidationExpression=
        }

        private void FillAllSpeciality()
        {
            var result = BusinessLogic.GetAllStateList();
            
            if (Session["statelist"] == null)
            Session["statelist"] = result.Tables[0];
            
            ddlState.DataSource = (DataTable)Session["statelist"];

            ddlState.Items.Clear();
            ddlState.Items.Add(new ListItem("Select State","0"));

            ddlState.DataTextField = result.Tables[0].Columns["StateName"].ColumnName.ToString();
            ddlState.DataValueField = result.Tables[0].Columns["State_Code"].ColumnName.ToString();
            ddlState.DataBind();            
           
        }

        protected bool validateFields()
        {
            CustomValidator2.IsValid = true;

            if (string.IsNullOrEmpty(txtZipCode.Value) && string.IsNullOrEmpty(ddlCity.DataTextField))
            {

                CustomValidator2.IsValid = false;
                CustomValidator2.ErrorMessage = "enter zip code or city to search CPA!";
                
            }

            if (txtZipCode.Value.Equals("Enter Zip Code") && string.IsNullOrEmpty(ddlCity.DataTextField))
            {

                CustomValidator2.IsValid = false;
                CustomValidator2.ErrorMessage = "enter zip code or city to search CPA!";
            }

            if (txtZipCode.Value.Equals("Enter Zip Code") && ddlCity.SelectedItem.Text.ToLower().Equals("select city"))
            {
                CustomValidator2.IsValid = false;
                CustomValidator2.ErrorMessage = "enter zip code or city to search CPA!";
            }

            return CustomValidator2.IsValid;
        }

        protected bool validateZipCode()
        {
            CustomValidator1.IsValid = false;

            if (txtZipCode.Value.Equals("Enter Zip Code"))
            {
                CustomValidator1.IsValid = true;

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtZipCode.Value, @"^(\d{5}-\d{4}|\d{5}|\d{9})$"))
                {
                    CustomValidator1.IsValid = true;
                }

            }
            return CustomValidator1.IsValid;
        }

        protected void btnSearchCPA_Click(object sender, EventArgs e)
        {
            bool isCityOrZipProvided = true;

            //if (!validateFields())
            //    return;

            //if (!validateZipCode())
            //    return;

         //   if (string.IsNullOrEmpty(txtZipCode.Value) && string.IsNullOrEmpty(ddlCity.DataTextField))
         //   {

         //       ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
         //"err_msg",
         //"alert('Please enter zip code or city to search CPA!');",
         //true);
         //       return;
         //   }
         //   if (txtZipCode.Value.Equals("Enter Zip Code") && string.IsNullOrEmpty(ddlCity.DataTextField))
         //   {

         //       ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
         //"err_msg",
         //"alert('Please enter zip code or city to search CPA!');",
         //true);
         //       return;
         //   }
         //   if (txtZipCode.Value.Equals("Enter Zip Code") && ddlCity.SelectedItem.Text.Equals("--Please Select city--"))
         //   {
         //       ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
         //"err_msg",
         //"alert('Please enter zip code or city to search CPA!');",
         //true);
         //       return;
         //   }

            if (Page.IsValid)
            {
                string zipCode = txtZipCode.Value;
                string city = string.Empty;

                if (ddlCity.SelectedItem != null)
                {
                    city = ddlCity.SelectedItem.Text;
                }

                string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?City={0}&ZipCode={1}", city, zipCode);
                Response.Redirect(redirectQuery);
            }
            //Response.Redirect("~/Default.aspx");
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stateCode = ddlState.SelectedItem.Value.ToString();

            var result = BusinessLogic.GetAllCityList(stateCode);
            ddlCity.DataSource = result.Tables[0];
            ddlCity.Items.Clear();
            ddlCity.Items.Add("Select City");
            ddlCity.DataTextField = result.Tables[0].Columns["CityName"].ColumnName.ToString();
            ddlCity.DataValueField = result.Tables[0].Columns["CityID"].ColumnName.ToString();
            ddlCity.DataBind();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

            Session["city"] = ddlCity.SelectedItem.Text;


        }

        protected void validateFields(object source, ServerValidateEventArgs args)
        {
            CustomValidator2.IsValid = true;
            CustomValidator2.ErrorMessage = "Enter Zip Code or City to search CPA!";

            if (string.IsNullOrEmpty(txtZipCode.Value) && string.IsNullOrEmpty(ddlCity.DataTextField))
            {
                args.IsValid = false;
               
            }
            else if (txtZipCode.Value.Equals("Enter Zip Code") && string.IsNullOrEmpty(ddlCity.DataTextField))
            {

                args.IsValid = false;
                
            }
            else if ((txtZipCode.Value.Equals("Enter Zip Code") || txtZipCode.Value.Trim() == string.Empty) && ddlCity.SelectedItem.Text.ToLower().Equals("select city"))
            {
                args.IsValid = false;

            }
        }

        protected void validateZipCode(object source, ServerValidateEventArgs args)
        {
            CustomValidator1.IsValid = false;

            if (txtZipCode.Value.Equals("Enter Zip Code") || txtZipCode.Value.Trim() == string.Empty)
            {
                args.IsValid = true;

            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtZipCode.Value, @"^(\d{5}-\d{4}|\d{5}|\d{9})$"))
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }

            }
        }
    }
}