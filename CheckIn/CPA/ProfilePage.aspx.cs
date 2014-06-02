using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn.CPA
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.Parse(Session["roleID"].ToString()) == 1)
            {
                Response.Redirect("~/CPA/UserProfile.aspx?UserID="+Session["userID"]);
            }
            else if (int.Parse(Session["roleID"].ToString()) == 2)
            {
                Response.Redirect("~/CPA/CPAProfile.aspx?UserID="+Session["userID"]);

            }

        }
    }
}