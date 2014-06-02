using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class ViewAllAppointments : System.Web.UI.Page
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
            lblName.Text = result.Tables[0].Rows[0]["CompanyName"].ToString();
            lblAddress1.Text = result.Tables[0].Rows[0]["Address1"].ToString();
            lblAddress2.Text = result.Tables[0].Rows[0]["Address2"].ToString();
            lblState.Text = result.Tables[0].Rows[0]["State"].ToString();
            lblZipCode.Text = result.Tables[0].Rows[0]["ZipCode"].ToString();

            lblScheduleDate.Text = Request.QueryString["Schedule"];
            lblPurposeOfVisit.Text = Request.QueryString["Purpose"];
            lnkViewAllAppointment.NavigateUrl = String.Format("~/CPA/ViewScheduledAppointments.aspx?CustomerID={0}&ID={1}", Request.QueryString["UserID"], Request.QueryString["ID"]);

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

    }
}