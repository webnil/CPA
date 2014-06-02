using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class UserReScheduleAppointment : System.Web.UI.Page
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
        private void RefreshAppointment()
        {
         int result = BusinessLogic.ReScheduleAppointment(Request.QueryString["ID"]);
            if (result != 0)
            {  
                Response.Redirect(string.Format("BookAppointment.aspx?CPAID={0}&Schedule={1}&ID={2}",Request.QueryString["CPAID"],Request.QueryString["Schedule"],Request.QueryString["ID"]));
            }
        }

    }
}