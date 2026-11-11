using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MeetingStatusList : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    MeetingEntryDAL AMeetingEntryDAL = new MeetingEntryDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
        }
    }

    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        AMAsterDal.GetCategoryListIntoDropdown(ddlCategorySearch);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);

    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("MeetingEntry.aspx");
    }

   
    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
       if (ValidateDateRanges())
       {
           LoadGrid();
       }
    }
    ShowMessage aShowMessage = new ShowMessage();



    private string BuildSearchQueryString()
    {
        var query = new System.Text.StringBuilder();

        string selectedSection = string.IsNullOrWhiteSpace(rblSearchSection.SelectedValue)
            ? "All"
            : rblSearchSection.SelectedValue;
        bool includeMeeting = selectedSection == "All" || selectedSection == "Meeting";

        // Company Filter (always required)
        if (ddlCompany.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(ddlCompany.SelectedValue) && ddlCompany.SelectedValue != "-1")
        {
            query.Append(" AND mas.CompanyId = " + ddlCompany.SelectedValue);
        }

        if (includeMeeting)
        {
            if (!string.IsNullOrWhiteSpace(txtMeetingNo.Text))
            {
                string searchVal = txtMeetingNo.Text.Replace("'", "''").Trim();
                query.Append(string.Format(@" AND (CONVERT(NVARCHAR(50), mas.MeetingInfoID) LIKE '%{0}%'
                    OR CONVERT(NVARCHAR(50), mas.MeetingNo) LIKE '%{0}%'
                    OR mas.MeetingCode LIKE '%{0}%')", searchVal));
            }

            if (!string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                query.Append(string.Format(" AND mas.Title LIKE '%{0}%'", txtTitle.Text.Replace("'", "''").Trim()));
            }

            if (ddlCategorySearch.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(ddlCategorySearch.SelectedValue) && ddlCategorySearch.SelectedValue != "-1")
            {
                query.Append(" AND mas.CategoryID = " + ddlCategorySearch.SelectedValue);
            }

            if (!string.IsNullOrWhiteSpace(ddlClassificationSearch.SelectedValue) && ddlClassificationSearch.SelectedValue != "-1")
            {
                query.Append(string.Format(" AND mas.Classification = '{0}'", ddlClassificationSearch.SelectedValue.Replace("'", "''").Trim()));
            }

            if (chkEntryTypeNotice.Checked && !chkEntryTypeMinutes.Checked)
            {
                query.Append(" AND mas.IsNotice = 1");
            }
            else if (chkEntryTypeMinutes.Checked && !chkEntryTypeNotice.Checked)
            {
                query.Append(" AND (mas.IsNotice = 0 OR mas.IsNotice IS NULL)");
            }

            DateTime tempDate;
            if (DateTime.TryParseExact(txtMeetingDateFrom.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
            {
                query.Append(string.Format(" AND CAST(mas.MeetingDate AS DATE) >= '{0}'", tempDate.ToString("yyyy-MM-dd")));
            }

            if (DateTime.TryParseExact(txtMeetingDateTo.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate))
            {
                query.Append(string.Format(" AND CAST(mas.MeetingDate AS DATE) <= '{0}'", tempDate.ToString("yyyy-MM-dd")));
            }

            if (!string.IsNullOrWhiteSpace(ddlMeetingStatus.SelectedValue) && ddlMeetingStatus.SelectedValue != "-1")
            {
                string status = ddlMeetingStatus.SelectedValue;
                if (status == "Upcoming")
                {
                    query.Append(" AND CAST(mas.MeetingDate AS DATE) > CAST(GETDATE() AS DATE)");
                }
                else if (status == "Today")
                {
                    query.Append(" AND CAST(mas.MeetingDate AS DATE) = CAST(GETDATE() AS DATE)");
                }
                else if (status == "Completed")
                {
                    query.Append(" AND CAST(mas.MeetingDate AS DATE) < CAST(GETDATE() AS DATE)");
                }
            }

            var selectedMembers = ddlMemberEmployeeId.Items.Cast<ListItem>()
                .Where(item => item.Selected && !string.IsNullOrWhiteSpace(item.Value) && item.Value != "-1")
                .Select(item => item.Value.Replace("'", "''").Trim())
                .ToList();

            if (selectedMembers.Count > 0)
            {
                var memberConditions = selectedMembers.Select(empVal => string.Format(@"(detail.EmpMasterCode LIKE '%{0}%'
                           OR CONVERT(NVARCHAR(50), detail.EmpInfoId) LIKE '%{0}%'
                           OR CONVERT(NVARCHAR(50), detail.BMemberSetupDetailsID) LIKE '%{0}%')", empVal));

                query.Append(string.Format(@" AND EXISTS (
                    SELECT 1 FROM dbo.tblMeeting_MeetingInfoDetail detail WITH (NOLOCK)
                    WHERE detail.MeetingInfoID = mas.MeetingInfoID
                      AND ({0})
                )", string.Join(" OR ", memberConditions)));
            }
        }

        if (selectedSection == "All" || selectedSection == "Agenda")
        {
            if (!string.IsNullOrWhiteSpace(ddlImplementationStatus.SelectedValue) && ddlImplementationStatus.SelectedValue != "-1")
            {
                query.Append(string.Format(@" AND EXISTS (
                    SELECT 1 FROM dbo.tblMeeting_MeetingInfoAgenda agenda WITH (NOLOCK)
                    WHERE agenda.MeetingInfoID = mas.MeetingInfoID
                      AND agenda.ImplementationStatus = '{0}'
                )", ddlImplementationStatus.SelectedValue.Replace("'", "''").Trim()));
            }
        }

        if (selectedSection == "All" || selectedSection == "Minutes")
        {
            if (!string.IsNullOrWhiteSpace(txtKeySearch.Text))
            {
                string attachmentText = txtKeySearch.Text.Replace("'", "''").Trim();
                query.Append(string.Format(@" AND (
                    mas.KeySearch LIKE '%{0}%'
                    OR EXISTS (
                        SELECT 1
                        FROM dbo.tblMeeting_MintuesEntryInfoDocument document WITH (NOLOCK)
                        WHERE document.MeetingInfoId = mas.MeetingInfoID
                          AND (document.ExtractedText LIKE '%{0}%'
                               OR document.DocumentNote LIKE '%{0}%'
                               OR document.FileName LIKE '%{0}%')
                    )
                )", attachmentText));
            }
        }

        if (selectedSection == "All" || selectedSection == "Approval")
        {
            if (!string.IsNullOrWhiteSpace(ddlApprovalStatus.SelectedValue) && ddlApprovalStatus.SelectedValue != "-1")
            {
                query.Append(string.Format(" AND mas.ActionStatus = '{0}'", ddlApprovalStatus.SelectedValue.Replace("'", "''").Trim()));
            }
        }

        if (selectedSection != "Approval")
        {
            //if (ddlCreatedBy.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(ddlCreatedBy.SelectedValue) && ddlCreatedBy.SelectedValue != "-1")
            //{
            //    query.Append(" AND mas.CreateBy = " + ddlCreatedBy.SelectedValue);
            //}

            DateTime tempDate;
            if (DateTime.TryParse(txtCreatedDate.Text, out tempDate))
            {
                query.Append(string.Format(" AND CAST(mas.CreateDate AS DATE) >= '{0}'", tempDate.ToString("yyyy-MM-dd")));
            }

            if (DateTime.TryParse(txtToDate.Text, out tempDate))
            {
                query.Append(string.Format(" AND CAST(mas.CreateDate AS DATE) <= '{0}'", tempDate.ToString("yyyy-MM-dd")));
            }
        }

        return query.ToString();
    }

    private bool ValidateDateRanges()
    {
        DateTime fromDate;
        DateTime toDate;

        bool includeMeeting = rblSearchSection.SelectedValue == "All" || rblSearchSection.SelectedValue == "Meeting";

        if (includeMeeting && DateTime.TryParseExact(txtMeetingDateFrom.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate) &&
            DateTime.TryParseExact(txtMeetingDateTo.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate) && fromDate.Date > toDate.Date)
        {
            aShowMessage.ShowMessageBox("Meeting Held From Date can not be greater than Meeting Held To Date", this);
            return false;
        }

        if (DateTime.TryParse(txtCreatedDate.Text, out fromDate) &&
            DateTime.TryParse(txtToDate.Text, out toDate) && fromDate.Date > toDate.Date)
        {
            aShowMessage.ShowMessageBox("Created Date From can not be greater than Created Date To", this);
            return false;
        }

        return true;
    }
    private void LoadGrid()
    {
        if (ddlCompany.SelectedIndex > 0 && ddlCompany.SelectedValue != "" && ddlCompany.SelectedValue != "-1")
        {
            DataTable aDataTable = AMeetingEntryDAL.LoadInfo(BuildSearchQueryString());
            if (aDataTable.Rows.Count > 0)
            {
                gv_ViewList.DataSource = aDataTable;
                gv_ViewList.DataBind();
                gv_ViewList.UseAccessibleHeader = true;
                gv_ViewList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_ViewList.FooterRow.TableSection = TableRowSection.TableFooter;
                gv_ViewList.UseAccessibleHeader = true;
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                gv_ViewList.DataSource = null;
                gv_ViewList.DataBind();
            }
        }
        else
        {
            gv_ViewList.DataSource = null;
            gv_ViewList.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MeetingStatusList.aspx");
    }


 

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntryViewDetails.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntry.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntry.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CompanyId"] = "";
        Session["CompanyId"] = ddlCompany.SelectedValue;
        AMAsterDal.GetUserListDropdown(ddlCreatedBy, ddlCompany.SelectedValue);
        if (Session["UserTypeId"].ToString() == "3" ||
               Session["UserTypeId"].ToString() == "4")
        {
            

          ddlCreatedBy.Enabled = true;
           // AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue);
        }
        else
        {
            try
            {
                ddlCreatedBy.SelectedValue = Session["UserId"].ToString();
            }
            catch
            {

            }

            ddlCreatedBy.Enabled = false;
         //   AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue, Session["UserId"].ToString());


        }

        int companyId;
        if (ddlCompany.SelectedIndex > 0 && int.TryParse(ddlCompany.SelectedValue, out companyId))
        {
            int? userId = null;
            if (Session["UserTypeId"].ToString() != "3" && Session["UserTypeId"].ToString() != "4")
            {
                userId = Convert.ToInt32(Session["UserId"].ToString());
            }

            using (DataTable dt = AMeetingEntryDAL.GetMeetingMemberSearchDropdown(companyId, userId))
            {
                ddlMemberEmployeeId.DataSource = dt;
                ddlMemberEmployeeId.DataValueField = "Value";
                ddlMemberEmployeeId.DataTextField = "TextField";
                ddlMemberEmployeeId.DataBind();
            }
        }
        else
        {
            ddlMemberEmployeeId.Items.Clear();
        }


       
      
          
    }
}
