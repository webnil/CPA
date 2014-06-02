using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CheckIn.CPA
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var result = BusinessLogic.GetAllStateList();

                if (Session["statelist"] == null)
                    Session["statelist"] = result.Tables[0];

                ddlState.DataSource = (DataTable)Session["statelist"];

                ddlState.Items.Clear();
                ddlState.Items.Add(new ListItem("Select State", "0"));

                ddlState.DataTextField = result.Tables[0].Columns["StateName"].ColumnName.ToString();
                ddlState.DataValueField = result.Tables[0].Columns["State_Code"].ColumnName.ToString();
                ddlState.DataBind();  
            }
        }
    }
}