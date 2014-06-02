using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CheckIn;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Request.QueryString["QueryCPAID"] != null)
        {

            Byte[] bytes = BusinessLogic.GetCPAImage(int.Parse(Request.QueryString["QueryCPAID"]));
            
            if (bytes == null)
                return;

            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "image/jpg";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.QueryString["QueryCPAID"] + ".jpg");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
  
}
