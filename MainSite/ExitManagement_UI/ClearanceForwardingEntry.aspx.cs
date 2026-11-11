using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.ExitManagement_BLL;
using DAL.COMMON_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class ExitManagement_UI_ClearanceForwardingEntry : System.Web.UI.Page
{
    private readonly CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private readonly ClearanceForwardingBll aClearanceForwardingBll = new ClearanceForwardingBll();
    private readonly ShowMessage aShowMessage = new ShowMessage();
    private bool? _hasOwnEmployeeRecord;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }

        if (!IsPostBack)
        {
            int editId = 0;
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out editId) && editId > 0)
            {
                hfClearanceForwardingId.Value = editId.ToString();
            }
            else if (Session["ClearanceForwardingId"] != null && int.TryParse(Session["ClearanceForwardingId"].ToString(), out editId) && editId > 0)
            {
                hfClearanceForwardingId.Value = editId.ToString();
                Session["ClearanceForwardingId"] = null;
            }

            if (editId > 0)
            {
                LoadCompanyDropDown();
                LoadEditData(editId);
            }
            else
            {
                LoadCompanyDropDown();
                LoadEmployeeDropDown();
                LoadSupervisedGrid();
            }
        }
    }

    private int GetEditForwardingId()
    {
        int id = 0;
        if (!string.IsNullOrEmpty(hfClearanceForwardingId.Value) && int.TryParse(hfClearanceForwardingId.Value, out id) && id > 0)
        {
            return id;
        }
        if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out id) && id > 0)
        {
            return id;
        }
        return 0;
    }

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

    // Admin role (UserTypeId 3 = Admin, 4 = Super Admin) or HR department/division
    // are treated as elevated users on this page - they can change the Company and
    // pick any employee; everyone else is locked to their own company/record.
    private bool IsHrUser()
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

    // True if Session["EmpInfoId"] actually maps to a real tblEmpGeneralInfo row.
    // Logins not tied to a real employee (e.g. an admin account) fail this even
    // when IsHrUser() would otherwise be false, and must not be locked to a
    // "own record" flow that has no record to show.
    private bool HasOwnEmployeeRecord()
    {
        if (_hasOwnEmployeeRecord.HasValue)
        {
            return _hasOwnEmployeeRecord.Value;
        }

        _hasOwnEmployeeRecord = false;
        if (Session["EmpInfoId"] != null)
        {
            using (DataTable dt = _commonDataLoad.GetEmployeeById(Session["EmpInfoId"].ToString()))
            {
                _hasOwnEmployeeRecord = dt.Rows.Count > 0;
            }
        }

        return _hasOwnEmployeeRecord.Value;
    }

    // Company/Employee are picked from the dropdowns for HR/admin users, or for
    // any session with no resolvable own employee record; everyone else stays
    // locked to their own company/record from Session.
    private bool UseDropDownSelection()
    {
        return IsHrUser() || !HasOwnEmployeeRecord();
    }

    private void LoadCompanyDropDown()
    {
        using (DataTable dt = _commonDataLoad.GetCompany())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }

        if (IsHrUser())
        {
            if (ddlCompany.Items.Count > 1)
            {
                ddlCompany.SelectedIndex = 1;
            }
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
        LoadSupervisedGrid();
    }

    private void LoadEmployeeDropDown()
    {
        if (!UseDropDownSelection())
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

        if (ddlCompany.SelectedValue != "" && ddlCompany.SelectedValue != "-1")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue))
            {
                ddlEmployee.DataSource = dt;
                ddlEmployee.DataValueField = "EmpInfoId";
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("---Select Employee---", "0"));
        }
        else
        {
            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem("---Select Employee---", "0"));
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSupervisedGrid();
    }

    // Never trusts the posted dropdown value for a non-HR user with a real own
    // record - Session["EmpInfoId"] is always the source of truth for them.
    private long GetEffectiveEmployeeId()
    {
        int editId = GetEditForwardingId();
        if (editId > 0 && !string.IsNullOrEmpty(hfEditEmployeeId.Value))
        {
            long editEmpId;
            if (long.TryParse(hfEditEmployeeId.Value, out editEmpId) && editEmpId > 0)
            {
                return editEmpId;
            }
        }

        if (!UseDropDownSelection())
        {
            return Session["EmpInfoId"] != null ? Convert.ToInt64(Session["EmpInfoId"]) : 0;
        }

        long employeeId;
        return long.TryParse(ddlEmployee.SelectedValue, out employeeId) ? employeeId : 0;
    }

    // Never trusts the posted dropdown value for a non-HR/non-admin user with a
    // real own record - Session["CompanyId"] is always the source of truth for them.
    private int GetEffectiveCompanyId()
    {
        int editId = GetEditForwardingId();
        if (editId > 0 && !string.IsNullOrEmpty(hfEditCompanyId.Value))
        {
            int editCompId;
            if (int.TryParse(hfEditCompanyId.Value, out editCompId) && editCompId > 0)
            {
                return editCompId;
            }
        }

        int selectedCompanyId;
        if (int.TryParse(ddlCompany.SelectedValue, out selectedCompanyId) && selectedCompanyId > 0)
        {
            return selectedCompanyId;
        }

        if (Session["CompanyId"] != null && int.TryParse(Session["CompanyId"].ToString(), out selectedCompanyId) && selectedCompanyId > 0)
        {
            return selectedCompanyId;
        }

        return 0;
    }

    private void LoadSupervisedGrid()
    {
        hfOrderedSupervisedIds.Value = "";
        long employeeId = GetEffectiveEmployeeId();
        if (employeeId <= 0)
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            return;
        }

        using (DataTable dt = aClearanceForwardingBll.GetSupervisedEmployeesByDivision(employeeId))
        {
            loadGridView.DataSource = dt;
            loadGridView.DataBind();
        }
    }

    private bool Validation()
    {
        if (GetEffectiveCompanyId() <= 0)
        {
            aShowMessage.ShowMessageBox("Please select a Company.", this);
            return false;
        }

        if (GetEffectiveEmployeeId() <= 0)
        {
            aShowMessage.ShowMessageBox("Please select an Employee.", this);
            return false;
        }

        if (GetSelectedSupervisedEmployeeIds().Count == 0)
        {
            aShowMessage.ShowMessageBox("Please select at least one supervised employee to forward.", this);
            return false;
        }

        return true;
    }

    private List<long> GetSelectedSupervisedEmployeeIds()
    {
        List<long> selectedIds = new List<long>();
        string orderedIdsStr = hfOrderedSupervisedIds.Value != null ? hfOrderedSupervisedIds.Value.Trim() : "";
        if (string.IsNullOrEmpty(orderedIdsStr) && !string.IsNullOrEmpty(Request.Form["hfOrderedSupervisedIds"]))
        {
            orderedIdsStr = Request.Form["hfOrderedSupervisedIds"].Trim();
        }

        if (!string.IsNullOrEmpty(orderedIdsStr))
        {
            string[] parts = orderedIdsStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                long id;
                if (long.TryParse(part.Trim(), out id) && id > 0 && !selectedIds.Contains(id))
                {
                    selectedIds.Add(id);
                }
            }
        }

        if (selectedIds.Count == 0)
        {
            foreach (GridViewRow row in loadGridView.Rows)
            {
                CheckBox chkSupervisedSelect = (CheckBox)row.FindControl("chkSupervisedSelect");
                if (chkSupervisedSelect != null && chkSupervisedSelect.Checked)
                {
                    selectedIds.Add(Convert.ToInt64(loadGridView.DataKeys[row.RowIndex]["EmpInfoId"]));
                }
            }
        }

        return selectedIds;
    }

    private void LoadEditData(int clearanceForwardingId)
    {
        using (DataTable dtMaster = aClearanceForwardingBll.GetForwardingMasterById(clearanceForwardingId))
        {
            if (dtMaster == null || dtMaster.Rows.Count == 0)
            {
                aShowMessage.ShowMessageBox("Clearance forwarding record not found.", this);
                Response.Redirect("ClearanceForwardingList.aspx");
                return;
            }

            DataRow row = dtMaster.Rows[0];

            int companyId = Convert.ToInt32(row["CompanyId"]);
            long employeeId = Convert.ToInt64(row["EmployeeId"]);

            hfEditCompanyId.Value = companyId.ToString();
            hfEditEmployeeId.Value = employeeId.ToString();

            if (ddlCompany.Items.FindByValue(companyId.ToString()) != null)
            {
                ddlCompany.SelectedValue = companyId.ToString();
            }
            ddlCompany.Enabled = false;

            ddlEmployee.Items.Clear();
            string empCode = row["EmpMasterCode"] != null ? row["EmpMasterCode"].ToString() : "";
            string empName = row["EmpName"] != null ? row["EmpName"].ToString() : "";
            string displayText = !string.IsNullOrEmpty(empCode) ? (empCode + " : " + empName) : empName;
            ddlEmployee.Items.Add(new ListItem(displayText, employeeId.ToString()));
            ddlEmployee.SelectedValue = employeeId.ToString();
            ddlEmployee.Enabled = false;

            txtRemarks.Text = row["Remarks"] != DBNull.Value ? row["Remarks"].ToString() : "";

            btnForward.Text = "Update";
            btnForward.CssClass = "btn btn-sm btn-primary";
            lblPageTitle.Text = "Edit Clearance Forwarding";

            LoadSupervisedGridForEdit(clearanceForwardingId, employeeId);
        }
    }

    private void LoadSupervisedGridForEdit(int clearanceForwardingId, long supervisorEmpId)
    {
        DataTable dtSupervised = aClearanceForwardingBll.GetSupervisedEmployeesByDivision(supervisorEmpId);
        DataTable dtDetail = aClearanceForwardingBll.GetForwardingDetailById(clearanceForwardingId);

        DataTable dtMerged = new DataTable();
        dtMerged.Columns.Add("EmpInfoId", typeof(long));
        dtMerged.Columns.Add("EmpMasterCode", typeof(string));
        dtMerged.Columns.Add("EmpName", typeof(string));
        dtMerged.Columns.Add("Designation", typeof(string));
        dtMerged.Columns.Add("DivisionName", typeof(string));
        dtMerged.Columns.Add("OrderNo", typeof(int));
        dtMerged.Columns.Add("IsSelected", typeof(bool));

        Dictionary<long, int> savedOrder = new Dictionary<long, int>();
        int defaultOrder = 1;
        if (dtDetail != null)
        {
            foreach (DataRow dRow in dtDetail.Rows)
            {
                long subId = Convert.ToInt64(dRow["SupervisedEmployeeId"]);
                int order = dRow["OrderNo"] != DBNull.Value ? Convert.ToInt32(dRow["OrderNo"]) : defaultOrder;
                savedOrder[subId] = order;
                defaultOrder++;
            }
        }

        if (dtSupervised != null)
        {
            foreach (DataRow sRow in dtSupervised.Rows)
            {
                long empId = Convert.ToInt64(sRow["EmpInfoId"]);
                DataRow newRow = dtMerged.NewRow();
                newRow["EmpInfoId"] = empId;
                newRow["EmpMasterCode"] = sRow["EmpMasterCode"];
                newRow["EmpName"] = sRow["EmpName"];
                newRow["Designation"] = sRow.Table.Columns.Contains("Designation") ? sRow["Designation"] : "";
                newRow["DivisionName"] = sRow.Table.Columns.Contains("DivisionName") ? sRow["DivisionName"] : "";

                if (savedOrder.ContainsKey(empId))
                {
                    newRow["OrderNo"] = savedOrder[empId];
                    newRow["IsSelected"] = true;
                }
                else
                {
                    newRow["OrderNo"] = 99999;
                    newRow["IsSelected"] = false;
                }
                dtMerged.Rows.Add(newRow);
            }
        }

        if (dtDetail != null)
        {
            foreach (DataRow dRow in dtDetail.Rows)
            {
                long subId = Convert.ToInt64(dRow["SupervisedEmployeeId"]);
                bool exists = false;
                foreach (DataRow mRow in dtMerged.Rows)
                {
                    if (Convert.ToInt64(mRow["EmpInfoId"]) == subId)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    DataRow missingRow = dtMerged.NewRow();
                    missingRow["EmpInfoId"] = subId;
                    missingRow["EmpMasterCode"] = dRow["EmpMasterCode"];
                    missingRow["EmpName"] = dRow["EmpName"];
                    missingRow["Designation"] = dRow.Table.Columns.Contains("Designation") ? dRow["Designation"] : "";
                    missingRow["DivisionName"] = dRow.Table.Columns.Contains("DivisionName") ? dRow["DivisionName"] : "";
                    missingRow["OrderNo"] = savedOrder.ContainsKey(subId) ? savedOrder[subId] : 99999;
                    missingRow["IsSelected"] = true;
                    dtMerged.Rows.Add(missingRow);
                }
            }
        }

        DataView dv = dtMerged.DefaultView;
        dv.Sort = "OrderNo ASC, EmpMasterCode ASC";
        DataTable dtSorted = dv.ToTable();

        loadGridView.DataSource = dtSorted;
        loadGridView.DataBind();

        List<string> orderedSelectedIds = new List<string>();
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            GridViewRow row = loadGridView.Rows[i];
            long empId = Convert.ToInt64(loadGridView.DataKeys[row.RowIndex]["EmpInfoId"]);
            CheckBox chk = (CheckBox)row.FindControl("chkSupervisedSelect");
            if (savedOrder.ContainsKey(empId))
            {
                if (chk != null)
                {
                    chk.Checked = true;
                }
                orderedSelectedIds.Add(empId.ToString());
            }
        }

        hfOrderedSupervisedIds.Value = string.Join(",", orderedSelectedIds);
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        if (!Validation())
        {
            return;
        }

        int companyId = GetEffectiveCompanyId();
        long employeeId = GetEffectiveEmployeeId();
        int editForwardingId = GetEditForwardingId();

        if (editForwardingId > 0)
        {
            ClearanceForwardingMasterDao aDao = new ClearanceForwardingMasterDao
            {
                ClearanceForwardingId = editForwardingId,
                CompanyId = companyId,
                EmployeeId = employeeId,
                Remarks = txtRemarks.Text.Trim(),
                UpdateBy = Session["LoginName"] != null ? Session["LoginName"].ToString() : ""
            };

            bool success = aClearanceForwardingBll.UpdateForwarding(aDao, GetSelectedSupervisedEmployeeIds());
            if (success)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Clearance forwarding updated successfully.');window.location='ClearanceForwardingList.aspx';", true);
            }
            else
            {
                aShowMessage.ShowMessageBox("Failed to update clearance forwarding.", this);
            }
            return;
        }

        if (aClearanceForwardingBll.IsAlreadyForwarded(companyId, employeeId))
        {
            aShowMessage.ShowMessageBox("This clearance has already been forwarded.", this);
            return;
        }

        ClearanceForwardingMasterDao newDao = new ClearanceForwardingMasterDao
        {
            CompanyId = companyId,
            EmployeeId = employeeId,
            ForwardedBy = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0,
            Remarks = txtRemarks.Text.Trim(),
            EntryBy = Session["LoginName"] != null ? Session["LoginName"].ToString() : ""
        };

        aClearanceForwardingBll.SaveForwarding(newDao, GetSelectedSupervisedEmployeeIds());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
            "alert('Clearance forwarded successfully.');window.location='ClearanceForwardingList.aspx';", true);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        int editId = GetEditForwardingId();
        if (editId > 0)
        {
            Response.Redirect("ClearanceForwardingEntry.aspx?id=" + editId);
        }
        else
        {
            Response.Redirect("ClearanceForwardingEntry.aspx");
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void ListViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ClearanceForwardingList.aspx");
    }
}
