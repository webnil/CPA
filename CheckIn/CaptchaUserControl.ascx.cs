using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckIn
{
    public partial class CaptchaUserControl : System.Web.UI.UserControl
    {  
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UpdateCaptchaText();
            }
        }

        public string Text
        {
            get
            {
                if (Session["Captcha"] != null)
                {
                    return Session["Captcha"].ToString();
                }
                else
                {
                    return null;
                }
            }

        }
        private void UpdateCaptchaText()
        {
           // txtCaptchaText.Text = string.Empty;
           // lblStatus.Visible = false;
            Session["Captcha"] = Guid.NewGuid().ToString().Substring(0, 6);
            var string1 = Session["Captcha"];

        }
        protected void btnReGenerate_Click(object sender, EventArgs e)
        {
            UpdateCaptchaText();
        }

        //private bool CheckCpatch(string UserText)
        //{
        //    bool success = false;
        //    if (Session["Captcha"] != null)
        //    {
        //        if ((Convert.ToString(Session["Captcha"])
        //            == UserText.ToString()))
        //        {

        //            success = true;

        //        }

        //    }
        //    lblStatus.Visible = true;
        //    if (success)
        //    {
        //        lblStatus.Text = "Success";
        //        lblStatus.ForeColor = System.Drawing.Color.Green;
        //    }
        //    else
        //    {
        //        lblStatus.Text = "Failure";
        //        lblStatus.ForeColor = System.Drawing.Color.Red;

        //    }
        //    return success;
        //}
    }
}