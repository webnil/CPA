using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace CheckIn.Web_Pages
{
    public partial class DisplayAppointments : System.Web.UI.Page
    {
        #region Members

        public int totalRows = 0;
        public int pageSize = 2;

        private DateTime StartingDateOfWeek = DateTime.Now;
        private string city = string.Empty;
        private string zipCode = string.Empty;
        private string cityCriteria = string.Empty;
        private string zipCodeCriteria = string.Empty;

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl body = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("myBody");
            if(body !=null)
            {
                body.Attributes.Add("onload", "initialize();");
            }

            if (!IsPostBack)
            {
                if (Request.QueryString.Count == 1)
                {
                    if (Request.QueryString["City"] != null)
                    {
                        city = Request.QueryString["City"];
                        Session["city"] = city;
                    }
                    else if(Request.QueryString["ZipCode"] != null)
                    {
                            zipCode = Request.QueryString["ZipCode"];
                            Session["zipCode"] = zipCode;
                    }
                }

                if (Request.QueryString.Count == 2)
                {
                    city = Request.QueryString["City"];
                    zipCode = Request.QueryString["ZipCode"];
                    Session["city"] = city;
                    Session["zipCode"] = zipCode;

                    lblSerchCriteria.Text = "Showing result for ";
                }
                FillSerchCriteria();
                FillAllSpeciality();
                RefreshSearchResult();
            }
        }

        #endregion

        #region Private Methods
        private void FillSerchCriteria()
        {
            //txtCity.Value = city;
            //txtZipCode.Value = zipCode;
            if ((city == null || city == "" || city == "Enter City" || city == "--Please Select city--") && (zipCode == null || zipCode == "" || zipCode == "Enter Zip Code"))
            {
                lblSerchCriteria.Text = "";
            }
            else if (city == null || city == "" || city == "Enter City" || city == "--Please Select city--")
            {
                //cityCriteria = "";
                lblSerchCriteria.Text = "Showing result for : Zipcode=" + zipCode;//txtZipCode.Value;
            }
            else if (zipCode == null || zipCode == "" || zipCode == "Enter Zip Code")
            {
                //zipCodeCriteria = "";
                lblSerchCriteria.Text = "Showing result for : City=" + city;// txtCity.Value;
            }
            else
            {
                lblSerchCriteria.Text = "Showing result for : City=" + city + " and Zipcode=" + zipCode;//txtCity.Value + " and Zipcode=" + txtZipCode.Value;
            }
            txtCity.Value = "Enter City";
            txtZipCode.Value = "Enter Zip Code";

        }
        private void RefreshSearchResult()
        {
            city = Session["city"].ToString();
            zipCode = Session["zipCode"].ToString();
            var result = BusinessLogic.GetAllCPA(city, zipCode);
            //if (result == null || result.Tables[0].Rows.Count == 0)
            //    return;
            gvCPASearchResult.DataSource = result;
            gvCPASearchResult.DataBind();
            if (IsNextWeek())
            {
                LinkButton btnNext = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnNextWeek");
               // btnNext.Enabled = false;
              
                //btnNext.Visible = false;
                btnNext.CssClass = "next inactive";
                btnNext.Attributes["disabled"] = "disabled";
                return;
            }
            if (IsPastWeek())
            {
                LinkButton btnPrev = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnPrevWeek");
               // btnPrev.Enabled = false;
                //btnPrev.Visible = false;
                btnPrev.CssClass = "prev inactive";
                btnPrev.Attributes["disabled"] = "disabled";
                return;
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


        #endregion

        #region Sorting And Paging

        protected void gvCPASearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCPASearchResult.PageIndex = e.NewPageIndex;
            RefreshSearchResult();
            //RefreshSearchResult("Price", SortOrder.Ascending, whereCluas);
        }

        protected void gvCPASearchResult_Sorting(object sender, GridViewSortEventArgs e)
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
            gvCPASearchResult.Sort((sender as DropDownList).Text, SortDirection.Ascending);

        }
        #endregion

        #region Grid View Events

        private Repeater FillAppiontmentDetailsRepeater(string repeaterName, GridViewRowEventArgs e, DataSet[] result, int index)
        {
            Repeater tempRepeater = (Repeater)e.Row.FindControl(repeaterName);
            if (tempRepeater != null && index < result.Length && result[index] != null)
            {
                DateTime inputDate = DateTime.Parse(result[index].Tables[0].Rows[0][1].ToString());
                if(inputDate.Date== DateTime.Now.Date)
                {
                    string hour = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                    string minute = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                    string currentTime =hour + ":" + minute + ":00";
                    result[index].Tables[0].DefaultView.RowFilter= "Time>='" +  currentTime+ "'" ;
                    tempRepeater.DataSource = result[index].Tables[0];
                    tempRepeater.DataBind();
                }
                else
                {
                    tempRepeater.DataSource = result[index];
                    tempRepeater.DataBind();
                }
            }
            return tempRepeater;
        }
        protected void gvCPASearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            SetWeeklyHeaderDates(e);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FillAllAppointmentsForCurrentWeek(e);
            }
        }

        private void FillAllAppointmentsForCurrentWeek(GridViewRowEventArgs e)
        {
            Label cpaID = (Label)e.Row.FindControl("lblCPAID");
            Session["CpaID"] = cpaID.Text;

            Label tempDate = null;
            if (gvCPASearchResult.HeaderRow != null)
            {
                tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblMondayDate");
            }
            if (cpaID != null && tempDate != null)
            {
                DataSet[] result = BusinessLogic.GetAllAppointmentsForWeek(int.Parse(cpaID.Text), (DateTime.Parse(tempDate.Text))).ToArray();
                if (result != null && result.Length > 0)
                {
                    int index1, count;
                    for (index1 = 0, count = 0; index1 < result.Length; index1++)
                    {
                        if (result[index1] == null)
                        { count++; }
                    }
                    if (index1 == count)
                    {
                        e.Row.Cells[2].ColumnSpan = 7;

                        Label lblAppoint = (Label)e.Row.FindControl("lblNoAppointment");

                        lblAppoint.Text = "No Appointment Available";
                        e.Row.Cells.RemoveAt(3);
                        e.Row.Cells.RemoveAt(3);
                        e.Row.Cells.RemoveAt(3);
                        e.Row.Cells.RemoveAt(3);
                        e.Row.Cells.RemoveAt(3);
                        e.Row.Cells.RemoveAt(3);
                    }
                    else
                    {
                        for (int index = 0; index < result.Length; index++)
                        {
                            switch (index)
                            {
                                case 0:
                                    FillAppiontmentDetailsRepeater("rptMondayAppointments", e, result, index);
                                    break;
                                case 1:
                                    FillAppiontmentDetailsRepeater("rptTuesdayAppointments", e, result, index);
                                    break;
                                case 2:
                                    FillAppiontmentDetailsRepeater("rptWednesdayAppointments", e, result, index);
                                    break;
                                case 3:
                                    FillAppiontmentDetailsRepeater("rptThursdayAppointments", e, result, index);
                                    break;
                                case 4:
                                    FillAppiontmentDetailsRepeater("rptFridayAppointments", e, result, index);
                                    break;
                                case 5:
                                    FillAppiontmentDetailsRepeater("rptSaturdayAppointments", e, result, index);
                                    break;
                                case 6:
                                    FillAppiontmentDetailsRepeater("rptSundayAppointments", e, result, index);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }    
        }

        private void SetCurrentHeaderDate(GridViewRowEventArgs e, string currentDat, int numberOfDaysToAdd)
        {
            Label tempDate = (Label)e.Row.FindControl(currentDat);
            if (tempDate != null)
            {

                //tempDate.Text = StartingDateOfWeek.AddDays(numberOfDaysToAdd).ToShortDateString();

                DateTime HeaderDate = StartingDateOfWeek.AddDays(numberOfDaysToAdd);
                tempDate.Text = HeaderDate.ToString("MMM") + "-" + HeaderDate.ToString("dd") + "-" + HeaderDate.ToString("yyyy");
            }
        }

        private void SetWeeklyHeaderDates(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Label tempDate = (Label)e.Row.FindControl("lblMondayDate");
                if (tempDate != null)
                {
                    int monDayDate = GetMondayDate(StartingDateOfWeek.DayOfWeek.ToString());
                    StartingDateOfWeek = StartingDateOfWeek.AddDays(monDayDate);
                    tempDate.Text = (StartingDateOfWeek).ToString("MMM") + "-" + StartingDateOfWeek.ToString("dd") + "-" + StartingDateOfWeek.ToString("yyyy");
                    //tempDate.Text = StartingDateOfWeek.ToShortDateString();
                }
                SetCurrentHeaderDate(e, "lblTuesdayDate", 1);
                SetCurrentHeaderDate(e, "lblWednesdayDate", 2);
                SetCurrentHeaderDate(e, "lblThursdayDate", 3);
                SetCurrentHeaderDate(e, "lblFridayDate", 4);
                SetCurrentHeaderDate(e, "lblSaturdayDate", 5);
                SetCurrentHeaderDate(e, "lblSundayDate", 6);
            }
        }

        private int GetMondayDate(string day)
        {
            int startDay = 0;
            switch (day)
            {
                case "Monday": startDay = 0;
                    break;
                case "Tuesday": startDay = -1;
                    break;
                case "Wednesday": startDay = -2;
                    break;
                case "Thursday": startDay = -3;
                    break;
                case "Friday": startDay = -4;
                    break;
                case "Saturday": startDay = -5;
                    break;
                case "Sunday": startDay = -6;
                    break;
            }
            return startDay;
        }
        protected void gvCPASearchResult_DataBound(object sender, EventArgs e)
        { 
            //createPagingSummaryOnPagerTemplate(sender, totalRows, pageSize);
        }
        #endregion

        #region Refine Search

        protected void btnRefineSearch_Click(object sender, EventArgs e)
        {
            GetCurrentSearchFilter();
            FillSerchCriteria();

            RefreshSearchResult();
        }

        private void GetCurrentSearchFilter()
        {
            city = txtCity.Value;
            zipCode = txtZipCode.Value;
            Session["city"] = city;
            Session["zipCode"] = zipCode;
            //txtCity.Value = "Enter City";
            //txtZipCode.Value = "Enter Zip Code";
        }
        private void GetCurrentCityZipCode()
        {
            city = Session["city"].ToString();
             zipCode=Session["zipCode"].ToString();
        }
        #endregion

        #region Header Weekly Dates

        protected void btnNextWeek_Click(object sender, EventArgs e)
        {
            LinkButton btnPrev = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnPrevWeek");
            btnPrev.Enabled = true;

           
            UpdateWeekDays(7);
          
           // GetCurrentSearchFilter();
            GetCurrentCityZipCode();
            RefreshSearchResult();
            //if (IsNextWeek())
            //{
            //    Button btnNext = (Button)gvCPASearchResult.HeaderRow.FindControl("btnNextWeek");
            //    btnNext.Enabled = false;
            //    return;
            //}

        }
        private bool IsNextWeek()
        {

            Label tempDate = null;
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblSundayDate");
            if (tempDate != null)
            {   bool result =DateTime.Parse(tempDate.Text) > DateTime.Now.AddDays(30);
            return result;
            }
            return false;

        }

        protected void btnPrevWeek_Click(object sender, EventArgs e)
        {
            if (IsPastWeek())
            {
                LinkButton btnPrev = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnPrevWeek");
                btnPrev.Enabled = false;
                return;
            }
            LinkButton btnNext = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnNextWeek");
            btnNext.Enabled = true;
            UpdateWeekDays(-7);
           // GetCurrentSearchFilter();
            GetCurrentCityZipCode();
            RefreshSearchResult();
           
            
           

        }

        private bool IsPastWeek()
        {
            Label tempDate = null;
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblMondayDate");
            if (tempDate != null)
            {
                return DateTime.Parse(tempDate.Text) < DateTime.Now;
            }
            return false;
        }

        private void UpdateWeekDays(int newDateUpdateValue)
        {
            Label tempDate = null;
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblMondayDate");
            if (tempDate != null)
            {
                tempDate.Text = DateTime.Parse(tempDate.Text).AddDays(newDateUpdateValue).ToShortDateString();
                StartingDateOfWeek = DateTime.Parse(tempDate.Text);
            }
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblTuesdayDate");
            if (tempDate != null)
            {
                tempDate.Text = DateTime.Parse(tempDate.Text).AddDays(newDateUpdateValue).ToShortDateString();
            }
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblWednesdayDate");
            if (tempDate != null)
            {
                tempDate.Text = DateTime.Parse(tempDate.Text).AddDays(newDateUpdateValue).ToShortDateString();
            }
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblThursdayDate");
            if (tempDate != null)
            {
                tempDate.Text = DateTime.Parse(tempDate.Text).AddDays(newDateUpdateValue).ToShortDateString();
            }
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblFridayDate");
            if (tempDate != null)
            {
                tempDate.Text = DateTime.Parse(tempDate.Text).AddDays(newDateUpdateValue).ToShortDateString();
            }
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblSaturdayDate");
            if (tempDate != null)
            {
                tempDate.Text = DateTime.Parse(tempDate.Text).AddDays(newDateUpdateValue).ToShortDateString();
            }
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblSundayDate");
            if (tempDate != null)
            {
                tempDate.Text = DateTime.Parse(tempDate.Text).AddDays(newDateUpdateValue).ToShortDateString();
            }
        }
      
        #endregion

        #region Google Map Marker
        //public string ConvertDataTabletoString()
        //{

        //    DataTable dt = new DataTable();
        //    using (SqlConnection con = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=TestDB;Integrated Security=true"))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("select title=City,lat=latitude,lng=longitude,description from LocationDetails", con))
        //        {
        //            con.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //            Dictionary<string, object> row;
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                row = new Dictionary<string, object>();
        //                foreach (DataColumn col in dt.Columns)
        //                {
        //                    row.Add(col.ColumnName, dr[col]);
        //                }
        //                rows.Add(row);
        //            }
        //            return serializer.Serialize(rows);
        //        }
        //    }
        //    return string.Empty;
        //}

        public string GetCPALocations()
        {
            DataTable dt = BusinessLogic.GetCPALocations() ;
            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);

            }
            else
            {
                return string.Empty;
            }
        }

        
        #endregion
    }
}