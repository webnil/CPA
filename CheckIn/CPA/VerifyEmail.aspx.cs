using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn
{
    public partial class VerifyMail : System.Web.UI.Page
    {
        public string EmailID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
             EmailID = Request.QueryString["Email"];
                  
        }
    }
}