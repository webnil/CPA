using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace CheckIn.CPA
{
    public partial class BookAppointment : System.Web.UI.Page
    {
        #region  Members

        private string cpid = string.Empty;
        //private string cpid1 = string.Empty;
        private string scheduled = string.Empty;
        private string scheduledTime = string.Empty;
        private string scheduledDate = string.Empty;
        private string id = string.Empty;
        

        public int totalRows = 0;
        public int pageSize = 2;
       

        private DateTime StartingDateOfWeek = DateTime.Now;
        private DateTime DateOfWeek = DateTime.Now;
        private string specialityID = string.Empty;
        private string zipCode = string.Empty;
        private string city = string.Empty;

        enum SortOrder
        {
            Ascending = 1,
            Descending = 2
        }

        #endregion

        public string CPAIDLink { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString.Count > 0)
            {
                cpid = Request.QueryString["CPAID"];
                Session["CpaID"] = cpid;
                scheduledTime = Request.QueryString["ScheduledTime"];
                scheduledDate = Request.QueryString["ScheduledDate"];
                scheduled = Request.QueryString["Schedule"];
                imgCPA.ImageUrl = "Handler.ashx?QueryCPAID=" + cpid;
                FillAllSpeciality();
                RefreshBookingAppointments();
                RefreshSearchResult();
                CPAIDLink = cpid;
            }
            else
            {
                cpid = Session["CpaID"].ToString();
                CPAIDLink = cpid;
            }// pnlAppointment.Visible = true;
        }
        private void FillAllSpeciality()
        {
            var result = BusinessLogic.GetAllSpecializationList();
            //ddlSpeciality.DataSource = result.Tables[0];
            //ddlSpeciality.DataTextField = result.Tables[0].Columns["Speciality"].ColumnName.ToString();
            //ddlSpeciality.DataValueField = result.Tables[0].Columns["ID"].ColumnName.ToString();
            //ddlSpeciality.DataBind();
        }
        private void RefreshBookingAppointments()
        {
            var result = BusinessLogic.GetCPADetails(int.Parse(cpid));

            if (result == null || result.Tables[0].Rows.Count == 0)
                return;

            lnkCPADetail.Text = result.Tables[0].Rows[0]["CompanyName"].ToString();
            //lnkCPADetail.NavigateUrl = "CPADetails.aspx?CPAID=" + cpid;
            lnkCPADetail.NavigateUrl = "~/CPA/CPADetailsInfo.aspx?CPAID=" + cpid;
            lblName.Text = result.Tables[0].Rows[0]["FirstName"].ToString() + " " + result.Tables[0].Rows[0]["LastName"].ToString();
            lblAddress1.Text = result.Tables[0].Rows[0]["Address1"].ToString();
            lblAddress2.Text = result.Tables[0].Rows[0]["Address2"].ToString();
            lblCity.Text = result.Tables[0].Rows[0]["City"].ToString();
            lblState.Text = result.Tables[0].Rows[0]["State"].ToString();
            lblZipCode.Text = result.Tables[0].Rows[0]["ZipCode"].ToString();

            //TODO: define what all parameters to pass over there
            //lnkAppointmentTime.NavigateUrl = "ChangeAppointment.aspx";
            // lnkAppointmentTime.NavigateUrl = "ChangeAppointment.aspx?CPAID=";

          //  lnkAppointmentTime.NavigateUrl = "ChangeAppointment.aspx?CPAID=" + cpid;
            if (scheduled != null)
            {
                lnkBtnAppointmentTime.Text = scheduled;

            }
            else
            {
                DateTime date = DateTime.Parse(scheduledDate);
                lnkBtnAppointmentTime.Text = date.ToString("dddd, dd, MMMM") + " - " + scheduledTime;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string purpose = string.IsNullOrEmpty(txtPurposeOfMeeting.Text) ? "Personal" : txtPurposeOfMeeting.Text;
            string redirectURL = string.Format("~/CPA/CheckNewUser.aspx?CPAID={0}&Schedule={1}&Purpose={2}&ID={3}", Request.QueryString["CPAID"], lnkBtnAppointmentTime.Text, purpose, Request.QueryString["ID"]);
            Response.Redirect(redirectURL);
        }

        protected void lnkBtnAppointmentTime_Click(object sender, EventArgs e)
        {
            pnlAppointment.Visible = true;
        }
     

        #region Page Load

      

        #endregion

        #region Private Methods

        private void RefreshSearchResult()
        {
            // var result = BusinessLogic.GetAllCPA(specialityID, zipCode);
            var result = BusinessLogic.GetCPADetails(int.Parse(cpid));
            if (result == null || result.Tables[0].Rows.Count == 0)
                return;
            gvCPASearchResult.DataSource = result;
            gvCPASearchResult.DataBind();
            if (IsNextWeek())
            {
                LinkButton btnNext = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnNextWeek");
              //  btnNext.Enabled = false;
                btnNext.CssClass = "next inactive";
                btnNext.Attributes["disabled"] = "disabled";
                return;
            }
            if (IsPastWeek())
            {
                LinkButton btnPrev = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnPrevWeek");
                //  btnPrev.Enabled = false;
                btnPrev.CssClass = "prev inactive";
                btnPrev.Attributes["disabled"] = "disabled";
                return;
            }

        }

        #endregion

        #region Sorting And Paging

        protected void gvCPASearchResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvCPASearchResult.PageIndex = e.NewPageIndex;
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
                if (inputDate.Date == DateTime.Now.Date)
                {
                    string hour = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                    string minute = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                    string currentTime = hour + ":" + minute + ":00";
                    result[index].Tables[0].DefaultView.RowFilter = "Time>='" + currentTime + "'";
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
            //  Label cpaID = (Label)e.Row.FindControl("lblCPAID");
            Label cpaID = new Label();
            cpaID.Text = cpid;
           
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
                        e.Row.Cells[1].ColumnSpan = 7;

                        Label lblAppoint = (Label)e.Row.FindControl("lblNoAppointment");

                        lblAppoint.Text = "No Appointment Available";
                        e.Row.Cells.RemoveAt(2);
                        e.Row.Cells.RemoveAt(2);
                        e.Row.Cells.RemoveAt(2);
                        e.Row.Cells.RemoveAt(2);
                        e.Row.Cells.RemoveAt(2);
                        e.Row.Cells.RemoveAt(2);
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
               // tempDate.Text = StartingDateOfWeek.AddDays(numberOfDaysToAdd).ToShortDateString();
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

            RefreshSearchResult();
        }

        private void GetCurrentSearchFilter()
        {
            //specialityID = ddlSpeciality.SelectedItem.Value;
            //zipCode = string.IsNullOrEmpty(txtZipCode.Text) || txtZipCode.Text.Equals(@"Enter Zip Code\City") ? string.Empty : txtZipCode.Text;

           // string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?SpecialityID={0}&ZipCode={1}", specialityID, zipCode);
            city = txtCity.Value;
            zipCode = txtZipCode.Value;
            string redirectQuery = string.Format("~/CPA/DisplayAppointments.aspx?City={0}&ZipCode={1}", city, zipCode);
            Response.Redirect(redirectQuery);
        }

        #endregion

        #region Header Weekly Dates

        protected void btnNextWeek_Click(object sender, EventArgs e)
        {
            LinkButton btnPrev = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnPrevWeek");
            btnPrev.Enabled = true;

           
            UpdateWeekDays(7);
           // GetCurrentSearchFilter();
            RefreshSearchResult();

        }
        private bool IsNextWeek()
        {

            Label tempDate = null;
            tempDate = (Label)gvCPASearchResult.HeaderRow.FindControl("lblSundayDate");
            if (tempDate != null)
            {
                bool result = DateTime.Parse(tempDate.Text) > DateTime.Now.AddDays(30);
                return result;
            }
            return false;

        }
        protected void btnPrevWeek_Click(object sender, EventArgs e)
        {
            LinkButton btnNext = (LinkButton)gvCPASearchResult.HeaderRow.FindControl("btnNextWeek");
            btnNext.Enabled = true;
            UpdateWeekDays(-7);
            //GetCurrentSearchFilter();
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

        protected void gvCPASearchResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlAppointment.Visible = false;
        }

        protected void gvCPASearchResult_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

    }
}