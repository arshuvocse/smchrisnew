using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using BLL.ExitManagement_BLL;
using DAL.COMMON_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class ExitManagement_UI_ClearanceForwardingList : System.Web.UI.Page
{
    private readonly CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private readonly ClearanceForwardingBll aClearanceForwardingBll = new ClearanceForwardingBll();
    private readonly ShowMessage aShowMessage = new ShowMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadCompanyDropDown();
            LoadEmployeeDropDown();
            Search();
        }
    }

    // Role check: Only system admin accounts (smcadmin/admin or accounts without an active employee record)
    // are treated as system Admin. Normal employees (even with UserTypeId 4) are governed by their department/division.
    private bool IsAdmin()
    {
        string loginName = Session["LoginName"] != null ? Session["LoginName"].ToString().Trim().ToLower() : "";
        if (loginName == "admin" || loginName == "smcadmin" || loginName.Contains("admin"))
        {
            return true;
        }

        string userRole = Session["UserRole"] != null ? Session["UserRole"].ToString().ToLower() : "";
        string role = Session["Role"] != null ? Session["Role"].ToString().ToLower() : "";
        string userType = Session["UserType"] != null ? Session["UserType"].ToString().ToLower() : "";
        if (userRole.Contains("admin") || role.Contains("admin") || userType.Contains("admin"))
        {
            return true;
        }

        if (Session["UserTypeId"] != null)
        {
            string utId = Session["UserTypeId"].ToString().Trim();
            if (utId == "3" || utId == "4")
            {
                string empId = Session["EmpInfoId"] != null ? Session["EmpInfoId"].ToString().Trim() : "";
                if (string.IsNullOrEmpty(empId) || empId == "0" || empId == "444444444")
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Returns true if current user is an Admin OR belongs to an HR / Human Resource department or division.
    // Inspects session parameters as well as database records for the logged-in employee.
    private bool IsAdminOrHrUser()
    {
        if (IsAdmin())
        {
            return true;
        }

        int departmentId;
        if (Session["DepartmentId"] != null && int.TryParse(Session["DepartmentId"].ToString(), out departmentId)
            && departmentId > 0
            && _commonDataLoad.IsHrDepartment(departmentId))
        {
            return true;
        }

        int divisionId;
        if (Session["DivisionId"] != null && int.TryParse(Session["DivisionId"].ToString(), out divisionId)
            && divisionId > 0
            && _commonDataLoad.IsHrDivision(divisionId))
        {
            return true;
        }

        string deptName = Session["DepartmentName"] != null ? Session["DepartmentName"].ToString() : (Session["Department"] != null ? Session["Department"].ToString() : "");
        if (!string.IsNullOrEmpty(deptName))
        {
            string lower = deptName.ToLower();
            if (lower.Contains("hr") || lower.Contains("human resource") || lower.Contains("human"))
            {
                return true;
            }
        }

        string divName = Session["DivisionName"] != null ? Session["DivisionName"].ToString() : (Session["Division"] != null ? Session["Division"].ToString() : "");
        if (!string.IsNullOrEmpty(divName))
        {
            string lower = divName.ToLower();
            if (lower.Contains("hr") || lower.Contains("human resource") || lower.Contains("human"))
            {
                return true;
            }
        }

        if (Session["EmpInfoId"] != null && !string.IsNullOrEmpty(Session["EmpInfoId"].ToString())
            && _commonDataLoad.IsHrEmployee(Session["EmpInfoId"].ToString()))
        {
            return true;
        }

        return false;
    }

    private void LoadCompanyDropDown()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }

        if (IsAdminOrHrUser())
        {
            ListItem selectItem = ddlCompany.Items.FindByValue("-1");
            if (selectItem != null)
            {
                selectItem.Text = "All";
                selectItem.Value = "";
            }
            else if (ddlCompany.Items.FindByValue("") == null)
            {
                ddlCompany.Items.Insert(0, new ListItem("All", ""));
            }

            ddlCompany.SelectedValue = "";
            ddlCompany.Enabled = true;
        }
        else
        {
            string userCompanyId = "";
            if (Session["EmpInfoId"] != null && !string.IsNullOrEmpty(Session["EmpInfoId"].ToString()))
            {
                using (DataTable dtEmp = _commonDataLoad.GetEmployeeById(Session["EmpInfoId"].ToString()))
                {
                    if (dtEmp.Rows.Count > 0 && dtEmp.Columns.Contains("CompanyId") && dtEmp.Rows[0]["CompanyId"] != DBNull.Value)
                    {
                        userCompanyId = dtEmp.Rows[0]["CompanyId"].ToString().Trim();
                    }
                }
            }

            if (string.IsNullOrEmpty(userCompanyId) && Session["CompanyId"] != null)
            {
                userCompanyId = Session["CompanyId"].ToString().Trim();
            }

            if (!string.IsNullOrEmpty(userCompanyId) && ddlCompany.Items.FindByValue(userCompanyId) != null)
            {
                ddlCompany.SelectedValue = userCompanyId;
            }
            else if (ddlCompany.Items.Count > 1)
            {
                ddlCompany.SelectedIndex = 1;
            }

            ddlCompany.Enabled = false;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEmployeeDropDown();
        Search();
    }

    private void LoadEmployeeDropDown()
    {
        if (!IsAdminOrHrUser())
        {
            ddlEmployee.Enabled = false;
            ddlEmployee.Items.Clear();

            if (Session["EmpInfoId"] != null && !string.IsNullOrEmpty(Session["EmpInfoId"].ToString()))
            {
                using (DataTable dt = _commonDataLoad.GetEmployeeById(Session["EmpInfoId"].ToString()))
                {
                    if (dt.Rows.Count > 0)
                    {
                        string empCode = dt.Columns.Contains("EmpMasterCode") ? dt.Rows[0]["EmpMasterCode"].ToString() : "";
                        string empName = dt.Columns.Contains("EmpName") ? dt.Rows[0]["EmpName"].ToString() : "";
                        string displayText = !string.IsNullOrEmpty(empCode) ? (empCode + " : " + empName) : empName;
                        ddlEmployee.Items.Add(new ListItem(displayText, dt.Rows[0]["EmpInfoId"].ToString()));
                        ddlEmployee.SelectedValue = dt.Rows[0]["EmpInfoId"].ToString();
                    }
                }
            }
            return;
        }

        ddlEmployee.Enabled = true;
        ddlEmployee.Items.Clear();

        DataTable dtEmployees = null;
        if (!string.IsNullOrEmpty(ddlCompany.SelectedValue) && ddlCompany.SelectedValue != "-1")
        {
            dtEmployees = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue);
        }
        else
        {
            dtEmployees = _commonDataLoad.GetEmpDDLALLwwwwwwwActive("");
        }

        using (dtEmployees)
        {
            if (dtEmployees != null)
            {
                ddlEmployee.DataSource = dtEmployees;
                ddlEmployee.DataValueField = "EmpInfoId";
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataBind();
            }
        }

        ListItem dummyItem = ddlEmployee.Items.FindByValue("-1");
        if (dummyItem != null)
        {
            ddlEmployee.Items.Remove(dummyItem);
        }
        dummyItem = ddlEmployee.Items.FindByValue("0");
        if (dummyItem != null)
        {
            ddlEmployee.Items.Remove(dummyItem);
        }

        ddlEmployee.Items.Insert(0, new ListItem("---Select Employee---", "0"));
        ddlEmployee.SelectedIndex = 0;
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        Search();
    }

    private int? GetEffectiveCompanyFilter()
    {
        if (!IsAdminOrHrUser())
        {
            string userCompanyId = Session["CompanyId"] != null ? Session["CompanyId"].ToString().Trim() : "";
            int parsedCompanyId;
            if (int.TryParse(userCompanyId, out parsedCompanyId) && parsedCompanyId > 0)
            {
                return parsedCompanyId;
            }
            return null;
        }

        int selectedCompanyId;
        if (int.TryParse(ddlCompany.SelectedValue, out selectedCompanyId) && selectedCompanyId > 0)
        {
            return selectedCompanyId;
        }

        return null;
    }

    // Admin or HR can see all employees' data (returns null when "---Select Employee---" / 0 is chosen)
    // or filter by a specific employee.
    // Regular employees strictly can ONLY see their own data (returns Session["EmpInfoId"], never null).
    private long? GetEffectiveEmployeeFilter()
    {
        if (!IsAdminOrHrUser())
        {
            if (Session["EmpInfoId"] != null)
            {
                long sessionEmpId;
                if (long.TryParse(Session["EmpInfoId"].ToString(), out sessionEmpId) && sessionEmpId > 0)
                {
                    return sessionEmpId;
                }
            }
            return -1; // If non-admin/non-HR has no valid employee ID, return -1 so 0 records match (never leak data)
        }

        long employeeId;
        return long.TryParse(ddlEmployee.SelectedValue, out employeeId) && employeeId > 0
            ? employeeId
            : (long?)null;
    }

    private void Search()
    {
        pnlDetails.Visible = false;

        int? companyId = GetEffectiveCompanyFilter();

        DateTime? dateFrom = null;
        DateTime parsedDateFrom;
        if (DateTime.TryParse(txtDateFrom.Text, out parsedDateFrom))
        {
            dateFrom = parsedDateFrom;
        }

        DateTime? dateTo = null;
        DateTime parsedDateTo;
        if (DateTime.TryParse(txtDateTo.Text, out parsedDateTo))
        {
            dateTo = parsedDateTo;
        }

        using (DataTable dt = aClearanceForwardingBll.GetForwardingList(companyId, GetEffectiveEmployeeFilter(), dateFrom, dateTo, ddlStatus.SelectedValue))
        {
            loadGridView.DataSource = dt;
            loadGridView.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Search();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        ddlStatus.SelectedIndex = 0;
        LoadCompanyDropDown();
        LoadEmployeeDropDown();
        Search();
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewData")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int clearanceForwardingId = Convert.ToInt32(loadGridView.DataKeys[rowIndex]["ClearanceForwardingId"]);
            ShowDetails(clearanceForwardingId);
        }
        else if (e.CommandName == "EditData")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int clearanceForwardingId = Convert.ToInt32(loadGridView.DataKeys[rowIndex]["ClearanceForwardingId"]);
            Session["ClearanceForwardingId"] = clearanceForwardingId;
            Response.Redirect("ClearanceForwardingEntry.aspx?id=" + clearanceForwardingId);
        }
    }

    protected bool CanCurrentUserEdit(object statusObj, object employeeIdObj, object forwardedByObj)
    {
        if (statusObj == null)
        {
            return false;
        }

        string status = statusObj.ToString().Trim();
        if (!string.Equals(status, "Forwarded", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (IsAdminOrHrUser())
        {
            return true;
        }

        long currentEmployeeId = Session["EmpInfoId"] != null ? Convert.ToInt64(Session["EmpInfoId"]) : 0;
        int currentUserId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;

        long recordEmpId = employeeIdObj != null && employeeIdObj != DBNull.Value ? Convert.ToInt64(employeeIdObj) : 0;
        int recordForwardedBy = forwardedByObj != null && forwardedByObj != DBNull.Value ? Convert.ToInt32(forwardedByObj) : 0;

        if (currentEmployeeId > 0 && currentEmployeeId == recordEmpId)
        {
            return true;
        }

        if (currentUserId > 0 && currentUserId == recordForwardedBy)
        {
            return true;
        }

        return false;
    }

    private void ShowDetails(int clearanceForwardingId)
    {
        ViewState["CurrentClearanceForwardingId"] = clearanceForwardingId;

        using (DataTable dtMaster = aClearanceForwardingBll.GetForwardingMasterById(clearanceForwardingId))
        {
            if (dtMaster.Rows.Count == 0)
            {
                return;
            }

            DataRow row = dtMaster.Rows[0];

            if (!IsAdminOrHrUser())
            {
                long rowEmpId = Convert.ToInt64(row["EmployeeId"]);
                long sessionEmpId = Session["EmpInfoId"] != null ? Convert.ToInt64(Session["EmpInfoId"]) : 0;
                if (rowEmpId != sessionEmpId)
                {
                    pnlDetails.Visible = false;
                    return;
                }
            }

            lblDetailCompany.Text = row["CompanyName"].ToString();
            lblDetailEmployee.Text = row["EmpMasterCode"] + " : " + row["EmpName"];
            lblDetailForwardedBy.Text = row["ForwardedByName"].ToString();
            lblDetailForwardDate.Text = Convert.ToDateTime(row["ForwardedDate"]).ToString("dd-MMM-yyyy");
            lblDetailStatus.Text = row["Status"].ToString();
            lblDetailRemarks.Text = row["Remarks"].ToString();
        }

        using (DataTable dtDetail = aClearanceForwardingBll.GetForwardingDetailById(clearanceForwardingId))
        {
            gvDetail.DataSource = dtDetail;
            gvDetail.DataBind();
        }

        pnlDetails.Visible = true;
    }

    protected void loadGridView_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.HeaderRow != null && gv.Rows.Count > 0)
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnCloseDetails_Click(object sender, EventArgs e)
    {
        pnlDetails.Visible = false;
    }

    protected string GetPriorityBadgeClass(object orderNoObj, int index)
    {
        int order = orderNoObj != null && orderNoObj != DBNull.Value ? Convert.ToInt32(orderNoObj) : (index + 1);
        if (order == 1) return "badge-responsible";
        return "badge-alternate";
    }

    protected string GetPriorityBadgeStyle(object orderNoObj, int index)
    {
        int order = orderNoObj != null && orderNoObj != DBNull.Value ? Convert.ToInt32(orderNoObj) : (index + 1);
        if (order == 1)
        {
            return "background-color: #20c997; color: #ffffff; padding: 2px 8px; border-radius: 4px; font-size: 11px; font-weight: bold; min-width: 135px; display: inline-block; text-align: center; box-shadow: 0 1px 2px rgba(0,0,0,0.15);";
        }
        return "background-color: #6c757d; color: #ffffff; padding: 2px 8px; border-radius: 4px; font-size: 11px; font-weight: 500; min-width: 135px; display: inline-block; text-align: center; box-shadow: 0 1px 2px rgba(0,0,0,0.1);";
    }

    protected string GetPriorityText(object orderNoObj, int index)
    {
        int order = orderNoObj != null && orderNoObj != DBNull.Value ? Convert.ToInt32(orderNoObj) : (index + 1);
        return order == 1 ? "<i class='fa fa-star' style='color: #ffc107; margin-right: 3px;'></i> Responsible Person" : "<i class='fa fa-user' style='margin-right: 3px;'></i> Alternate";
    }

    protected string FormatSupervisedEmployeeList(object namesObj)
    {
        if (namesObj == null || namesObj == DBNull.Value)
        {
            return "";
        }

        string raw = namesObj.ToString().Trim();
        if (string.IsNullOrEmpty(raw))
        {
            return "";
        }

        string[] items = raw.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();
        foreach (string item in items)
        {
            string trimmed = item.Trim();
            bool isPrimary = trimmed.Contains("(Primary Person)") || trimmed.Contains("(Primary Person 1)")
                || trimmed.Contains("(Responsible Person)") || trimmed.Contains("(Responsible Person 1)");
            string empName = System.Text.RegularExpressions.Regex.Replace(trimmed, @"\s*\((Primary|Responsible) Person.*?\)", "").Trim();

            sb.Append("<div class='mb-1 text-left' style='line-height: 1.8; margin-bottom: 4px;'>");
            if (isPrimary)
            {
                sb.Append("<span class='badge-responsible' style='background-color: #20c997; color: #ffffff; padding: 2px 8px; border-radius: 4px; font-size: 11px; font-weight: bold; min-width: 140px; display: inline-block; text-align: center; margin-right: 6px; box-shadow: 0 1px 2px rgba(0,0,0,0.15);'>");
                sb.Append("<i class='fa fa-star' style='color: #ffc107; margin-right: 3px;'></i> Responsible Person");
                sb.Append("</span>");
                sb.Append("<span style='font-weight: 600; font-size: 12px; color: #2c3e50;'>");
                sb.Append(HttpUtility.HtmlEncode(empName));
                sb.Append("</span>");
            }
            else
            {
                sb.Append("<span class='badge-alternate' style='background-color: #6c757d; color: #ffffff; padding: 2px 8px; border-radius: 4px; font-size: 11px; font-weight: 500; min-width: 140px; display: inline-block; text-align: center; margin-right: 6px; box-shadow: 0 1px 2px rgba(0,0,0,0.1);'>");
                sb.Append("<i class='fa fa-user' style='margin-right: 3px;'></i> Alternate");
                sb.Append("</span>");
                sb.Append("<span style='font-weight: 500; font-size: 12px; color: #495057;'>");
                sb.Append(HttpUtility.HtmlEncode(empName));
                sb.Append("</span>");
            }
            sb.Append("</div>");
        }

        return sb.ToString();
    }

    // Gates the Complete button visibility - purely cosmetic, the real
    // authorization check is repeated server-side in gvDetail_RowCommand.
    protected bool CanCurrentUserComplete(object statusObj, object supervisedEmployeeIdObj)
    {
        if (statusObj == null || supervisedEmployeeIdObj == null || statusObj.ToString() != "Pending")
        {
            return false;
        }

        long currentEmployeeId = Session["EmpInfoId"] != null ? Convert.ToInt64(Session["EmpInfoId"]) : 0;
        return currentEmployeeId > 0 && currentEmployeeId == Convert.ToInt64(supervisedEmployeeIdObj);
    }

    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "CompleteForwarding")
        {
            return;
        }

        int rowIndex = Convert.ToInt32(e.CommandArgument);
        int clearanceForwardingDetailId = Convert.ToInt32(gvDetail.DataKeys[rowIndex]["ClearanceForwardingDetailId"]);
        long supervisedEmployeeId = Convert.ToInt64(gvDetail.DataKeys[rowIndex]["SupervisedEmployeeId"]);

        // Re-checked here, not trusted from the bound Visible flag - a hidden button is not
        // an authorization boundary.
        long currentEmployeeId = Session["EmpInfoId"] != null ? Convert.ToInt64(Session["EmpInfoId"]) : 0;
        if (currentEmployeeId == 0 || currentEmployeeId != supervisedEmployeeId)
        {
            aShowMessage.ShowMessageBox("This clearance was not forwarded to you.", this);
            return;
        }

        if (!aClearanceForwardingBll.CompleteForwarding(clearanceForwardingDetailId, currentEmployeeId))
        {
            aShowMessage.ShowMessageBox("This clearance has already been completed.", this);
        }

        if (ViewState["CurrentClearanceForwardingId"] != null)
        {
            ShowDetails(Convert.ToInt32(ViewState["CurrentClearanceForwardingId"]));
        }

        Search();
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["ClearanceForwardingId"] = null;
        Response.Redirect("ClearanceForwardingEntry.aspx");
    }
}
