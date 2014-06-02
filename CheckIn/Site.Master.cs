using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public string userID {get;set;}

        protected void Page_Load(object sender, EventArgs e)
        {   
            if (Session["userName"] != null)
            {
               
                if (Session["roleID"].ToString() == "1")
                {
                    Label2.Text = "Welcome " + Session["userName"].ToString() + "  ";
                    //Menu1.Items[0].Text = "We";
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Panel3.Visible = false;
                }
                else if (Session["roleID"].ToString() == "2")
                {
                    Label5.Text = "Welcome " + Session["userName"].ToString() + "  ";
                    //Menu1.Items[0].Text = "We";
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                }
             
            }
            else
            {
               // Label2.Enabled = false;
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
               
            }
            //Menu1.Visible = false;
        }

      

    }
}
