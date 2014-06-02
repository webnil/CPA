using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;

namespace CheckIn.Web_Pages
{
    public partial class ViewScheduledAppointments : System.Web.UI.Page
    {
        #region Members

        public int totalRows = 0;
        public int pageSize = 2;

        private DateTime StartingDateOfWeek = DateTime.Now;
        //private string specialityID = string.Empty;
        //private string zipCode = string.Empty;
        private string userId = string.Empty;

        public string userID { get; set; }

        enum SortOrder
        {
            Ascending = 1,
            Descending = 2
        }

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["UserID"] != null)
                {
                    userID = Request.QueryString["UserID"];
                    Session["userID"] = userID;
                }
                else
                {
                    userID = Session["userID"].ToString();
                }
                //userId = Session["userID"].ToString();
              
                FillAllSpeciality();
                RefreshSearchResult();
            }
        }

        #endregion

        #region Private Methods

        private void RefreshSearchResult()
        {
            //var result = BusinessLogic.GetAllCPA(specialityID, zipCode);
            var result = BusinessLogic.GetSCheduleCPACustomerDetails2(userID);

            if (result == null || result.Tables[0].Rows.Count == 0)
                return;

            gvAppointments.DataSource = result;
            gvAppointments.DataBind();

        }

        private void FillAllSpeciality()
        {
            var result = BusinessLogic.GetAllSpecializationList();
            //ddlSpeciality.DataSource = result.Tables[0];
            //ddlSpeciality.DataTextField = result.Tables[0].Columns["Speciality"].ColumnName.ToString();
            //ddlSpeciality.DataValueField = result.Tables[0].Columns["ID"].ColumnName.ToString();
            //ddlSpeciality.DataBind();
        }


        #endregion

        #region Sorting And Paging

        protected void gvAppointments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvAppointments.PageIndex = e.NewPageIndex;
            //RefreshSearchResult("Price", SortOrder.Ascending, whereCluas);
        }

        protected void gvAppointments_Sorting(object sender, GridViewSortEventArgs e)
        {
            //SortOrder sortDirection = GetSortDirection(e.SortExpression).ToString() == "Ascending" ? SortOrder.Ascending : SortOrder.Descending;
            //RefreshSearchResult(e.SortExpression, sortDirection, whereCluas);
        }

        protected SortDirection GetSortDirection(string column)
        {
            SortDirection nextDir = SortDirection.Ascending; // Default next sort expression behaviour.
            //if (ViewState["sort"] != null && ViewState["sort"].ToString() == column)
            //{   // Exists... DESC.
            //    nextDir = SortDirection.Descending;
            //    ViewState["sort"] = null;
            //}
            //else
            //{   // Doesn't exists, set ViewState.
            //    ViewState["sort"] = column;
            //}
            return nextDir;
        }

        private void createPagingSummaryOnPagerTemplate(object sender, int totalRows, int pageSize)
        {
            GridView gv = sender as GridView;
            if (gv != null)
            {
                //Get Bottom Pager Row from a gridview
                GridViewRow bottomRow = gv.BottomPagerRow;
                GridViewRow topRow = gv.TopPagerRow;

                if (bottomRow != null)
                {
                    //create new cell to add to page strip
                    TableCell pagingSummaryCell = new TableCell();
                    pagingSummaryCell.Text = DisplayCusotmPagingSummary(totalRows, gv.PageIndex, pageSize);
                    pagingSummaryCell.HorizontalAlign = HorizontalAlign.Right;
                    pagingSummaryCell.VerticalAlign = VerticalAlign.Middle;
                    pagingSummaryCell.Width = Unit.Percentage(100);
                    pagingSummaryCell.Height = Unit.Pixel(35);
                    //Getting table which shows PagingStrip
                    Table tbl = (Table)bottomRow.Cells[0].Controls[0];

                    //BottomPagerRow will be visible false if pager doesn't have numbers and page number 1 will be displayed
                    if (totalRows <= pageSize)
                    {
                        gv.BottomPagerRow.Visible = true;
                        tbl.Rows[0].Cells.Clear();
                        tbl.Width = Unit.Percentage(100);
                    }
                    //Find table and add paging summary text
                    tbl.Rows[0].Cells.Add(pagingSummaryCell);
                    //assign header row color to footer row
                    tbl.BackColor = System.Drawing.ColorTranslator.FromHtml("#1AD9F2");
                    tbl.Width = Unit.Percentage(100);
                }
                if (topRow != null)
                {
                    //create new cell to add to page strip
                    TableCell pagingSummaryCell = new TableCell();
                    pagingSummaryCell.Text = DisplayCusotmPagingSummary(totalRows, gv.PageIndex, pageSize);
                    pagingSummaryCell.HorizontalAlign = HorizontalAlign.Right;
                    pagingSummaryCell.VerticalAlign = VerticalAlign.Middle;
                    pagingSummaryCell.Width = Unit.Percentage(100);
                    pagingSummaryCell.Height = Unit.Pixel(35);
                    //Getting table which shows PagingStrip
                    Table tbl = (Table)topRow.Cells[0].Controls[0];

                    //BottomPagerRow will be visible false if pager doesn't have numbers and page number 1 will be displayed
                    if (totalRows <= pageSize)
                    {
                        gv.TopPagerRow.Visible = true;
                        tbl.Rows[0].Cells.Clear();
                        tbl.Width = Unit.Percentage(100);
                    }
                    //Find table and add paging summary text
                    tbl.Rows[0].Cells.Add(pagingSummaryCell);
                    //assign header row color to footer row
                    tbl.BackColor = System.Drawing.ColorTranslator.FromHtml("#1AD9F2");
                    tbl.Width = Unit.Percentage(100);
                }
            }

        }

        public static string DisplayCusotmPagingSummary(int numberOfRecords, int currentPage, int pageSize)
        {
            StringBuilder strDisplaySummary = new StringBuilder();
            int numberOfPages;
            if (numberOfRecords > pageSize)
            {
                // Calculating the total number of pages
                numberOfPages = (int)Math.Ceiling((double)numberOfRecords / (double)pageSize);
            }
            else
            {
                numberOfPages = 1;
            }
            strDisplaySummary.Append("Showing ");
            int floor = (currentPage * pageSize) + 1;
            strDisplaySummary.Append(floor.ToString());
            strDisplaySummary.Append("-");
            int ceil = ((currentPage * pageSize) + pageSize);

            if (ceil > numberOfRecords)
            {
                strDisplaySummary.Append(numberOfRecords.ToString());
            }
            else
            {
                strDisplaySummary.Append(ceil.ToString());
            }

            strDisplaySummary.Append(" of ");
            strDisplaySummary.Append(numberOfRecords.ToString());
            strDisplaySummary.Append(" results ");
            return strDisplaySummary.ToString();
        }

        protected void ddlSortColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["sortColumn"] = (sender as DropDownList).Text;
            gvAppointments.Sort((sender as DropDownList).Text, SortDirection.Ascending);

        }
        #endregion

        #region Grid View Events

        private Repeater FillAppiontmentDetailsRepeater(string repeaterName, GridViewRowEventArgs e, DataSet result)
        {
            Repeater tempRepeater = (Repeater)e.Row.FindControl(repeaterName);
            if (tempRepeater != null && result != null)
            {
                tempRepeater.DataSource = result;
                tempRepeater.DataBind();
            }
            return tempRepeater;
        }
        protected void gvAppointments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    FillAllAppointmentsForCurrentWeek(e);
            //}
        }

        //private void FillAllAppointmentsForCurrentWeek(GridViewRowEventArgs e)
        //{
        //    Label cpaID = (Label)e.Row.FindControl("lblCPAID");
      
        //    if (cpaID != null)// && tempDate != null)
        //    {

        //        DataSet result = BusinessLogic.GetSCheduleCPACustomerDetails(cpaID.Text, userId);

        //        if (result != null )
        //        {

        //            FillAppiontmentDetailsRepeater("rptPurposeOfAppointments", e, result);
        //        }

        //        result = BusinessLogic.GetCPACustomerSchedule(cpaID.Text, userId);
        //        if (result != null)
        //        {
        //            FillAppiontmentDetailsRepeater("rptScheduleAppointments", e, result);
        //        }

           
               
        //    }
        //}

     
        protected void gvAppointments_DataBound(object sender, EventArgs e)
        {
            //createPagingSummaryOnPagerTemplate(sender, totalRows, pageSize);
        }
        #endregion

        #region Refine Search

        protected void btnRefineSearch_Click(object sender, EventArgs e)
        {
            GetCurrentSearchFilter();

            RefreshSearchResult();
        }

        private void GetCurrentSearchFilter()
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

        #endregion

       

    }
}