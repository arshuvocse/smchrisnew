using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.RecruitmentManagement_BLL;
using DAL.COMMON_DAL;
using DAL.ExitManagement_DAL;
using DAL.Permission_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

using System.Web.Services;

public partial class ExitManagement_UI_EmpExitView : System.Web.UI.Page
{

    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();
            LoadDropDownList();

        }
    }
    JobCreationBll aJobCreationBll = new JobCreationBll();
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
    private void LoadDropDownList()
    {

        aJobCreationBll.LoadComapnyNameList(companyDropDownList);
 

        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
    }

    private void LoadFilterDropDownLists()
    {
        divisionDropDownList.Items.Clear();
        departmentDropDownList.Items.Clear();
        ddlEmpInfo.Items.Clear();

        if (string.IsNullOrEmpty(companyDropDownList.SelectedValue))
        {
             
            ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", ""));
            return;
        }

        using (DataTable divisions = aCommonDataLoadDal.GetDDLComDivisionAll(companyDropDownList.SelectedValue))
        {
            divisionDropDownList.DataSource = divisions;
            divisionDropDownList.DataValueField = "Value";
            divisionDropDownList.DataTextField = "TextField";
            divisionDropDownList.DataBind();
        }

        using (DataTable departments = aCommonDataLoadDal.GetDDLComDepartmentAll(companyDropDownList.SelectedValue))
        {
            departmentDropDownList.DataSource = departments;
            departmentDropDownList.DataValueField = "Value";
            departmentDropDownList.DataTextField = "TextField";
            departmentDropDownList.DataBind();
        }



        using (DataTable employees = aCommonDataLoadDal.GetEmpDDLAActiveOnlyViewALLInactive(companyDropDownList.SelectedValue))
        {
            ddlEmpInfo.DataSource = employees;
            ddlEmpInfo.DataValueField = "EmpInfoId";
            ddlEmpInfo.DataTextField = "EmpName";
            ddlEmpInfo.DataBind();
        }

        ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", ""));
        ddlEmpInfo.SelectedIndex = 0;
    }
    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();

        DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
        for (int i = 0; i < userdata.Rows.Count; i++)
        {
            for (int j = 0; j < lchk_Company.Items.Count; j++)
            {
                if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                {
                    lchk_Company.Items[j].Selected = true;


                }
            }
        }
    }

    public void UserPersmissionValidation()
    {
        try
        {
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                    //    Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }


    private string GenerateParameterOnlyView()
    {
        string parameter = "  ";

        if (divisionDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and     DivisionId = '" + divisionDropDownList.SelectedValue + "'";
        }

        if (departmentDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and     DepartmentId = '" + departmentDropDownList.SelectedValue + "'";
        }

        if (ddlEmpInfo.SelectedIndex > 0)
        {
            parameter = parameter + "  and     EmployeeId = '" + ddlEmpInfo.SelectedValue + "'";
        }

        if (approvalStatusDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and     ApprovalStatus = '" + approvalStatusDropDownList.SelectedValue + "'";
        }

        if (!string.IsNullOrEmpty(notificationDropDownList.SelectedValue))
        {
            parameter = parameter + "  and ISNULL(IsRunning, 0) = " + notificationDropDownList.SelectedValue;
        }


        if (fromDateTextBox.Text.Trim() != "" && toDateTextBox.Text.Trim() != "")
        {
            parameter = parameter + " AND  convert(date,EntryDate)  between '" + fromDateTextBox.Text.Trim() + "' and '" + toDateTextBox.Text.Trim() + "'  ";
        }

        if (fromDateTextBox.Text.Trim() != "" && toDateTextBox.Text.Trim() == "")
        {
                parameter = parameter + " AND  convert(date,EntryDate)  between '" + fromDateTextBox.Text.Trim() + "' and '" + DateTime.Now + "'  ";
            }


        return parameter;
    }

    private string GenerateParameterOnlyViewPharma()
    {
        string parameter = "  ";

        if (divisionDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and     DivisionId = '" + divisionDropDownList.SelectedValue + "'";
        }

        if (departmentDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and     DepartmentId = '" + departmentDropDownList.SelectedValue + "'";
        }

        if (ddlEmpInfo.SelectedIndex > 0)
        {
            parameter = parameter + "  and     EmployeeId = '" + ddlEmpInfo.SelectedValue + "'";
        }

        if (approvalStatusDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and     ApprovalStatus = '" + approvalStatusDropDownList.SelectedValue + "'";
        }

        if (!string.IsNullOrEmpty(notificationDropDownList.SelectedValue))
        {
            parameter = parameter + "  and ISNULL(IsRunning, 0) = " + notificationDropDownList.SelectedValue;
        }


        if (fromDateTextBox.Text.Trim() != "" && toDateTextBox.Text.Trim() != "")
        {
            parameter = parameter + " AND  convert(date,EntryDate)  between '" + fromDateTextBox.Text.Trim() + "' and '" + toDateTextBox.Text.Trim() + "'  ";
        }

        if (fromDateTextBox.Text.Trim() != "" && toDateTextBox.Text.Trim() == "")
        {
            parameter = parameter + " AND  convert(date,EntryDate)  between '" + fromDateTextBox.Text.Trim() + "' and '" + DateTime.Now + "'  ";
        }


        return parameter;
    }
    private DataTable GetMergedClearenceData()
    {
        //DataTable dataTable = aEmployeeJobLeftEntryDAL.ClearenceFormSetupView(companyDropDownList.SelectedValue, GenerateParameterOnlyView());
        DataTable dataTable = new DataTable();
        DataTable dataTablePharma = aEmployeeJobLeftEntryDAL.ClearenceFormSetupViewPharma(companyDropDownList.SelectedValue, GenerateParameterOnlyViewPharma());

        if (dataTable != null && dataTablePharma != null)
        {
            dataTable.Merge(dataTablePharma);
        }
        else if (dataTable == null && dataTablePharma != null)
        {
            dataTable = dataTablePharma;
        }

        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            DataView dv = dataTable.DefaultView;
            dv.Sort = "EntryDate DESC";
            dataTable = dv.ToTable();
        }

        return dataTable;
    }

    private void LoadInfo()
    {
        loadGridView.DataSource = null;
        loadGridView.DataBind();
        if (companyDropDownList.SelectedValue!="")
        {
            DataTable dataTable = GetMergedClearenceData();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                loadGridView.DataSource = dataTable;
                loadGridView.DataBind();
            }
        }
       
    }

    protected string FormatEmployeeApprovalStatus(object exitMasterIdObj, object value)
    {
        string approvalStatus = Convert.ToString(value);
        if (string.IsNullOrWhiteSpace(approvalStatus))
        {
            return "";
        }

        string exitMasterId = Convert.ToString(exitMasterIdObj);

        StringBuilder badges = new StringBuilder();
        badges.Append("<div class=\"approval-status-list\">");

        foreach (string item in approvalStatus.Split('|'))
        {
            string text = item.Trim();
            bool isNotified = false;
            if (text.EndsWith("~1"))
            {
                isNotified = true;
                text = text.Substring(0, text.Length - 2).Trim();
            }
            else if (text.EndsWith("~0"))
            {
                text = text.Substring(0, text.Length - 2).Trim();
            }

            string displayText = text;
            string cssClass = "approval-status-not-reached";
            bool isChecked = isNotified;
            bool showCheckbox = false;

            if (text.EndsWith("(Done)", StringComparison.OrdinalIgnoreCase))
            {
                cssClass = "approval-status-done";
                displayText = text.Substring(0, text.Length - "(Done)".Length).TrimEnd();
                showCheckbox = false;
            }
            else if (text.EndsWith("(Pending)", StringComparison.OrdinalIgnoreCase))
            {
                cssClass = "approval-status-pending";
                displayText = text.Substring(0, text.Length - "(Pending)".Length).TrimEnd();
                showCheckbox = true;
            }
            else if (text.EndsWith("(Not Yet Reached)", StringComparison.OrdinalIgnoreCase))
            {
                displayText = text.Substring(0, text.Length - "(Not Yet Reached)".Length).TrimEnd();
                showCheckbox = false;
            }

            string empCode = "";
            int startIdx = displayText.IndexOf('(');
            int endIdx = displayText.IndexOf(')');
            if (startIdx != -1 && endIdx != -1 && endIdx > startIdx)
            {
                empCode = displayText.Substring(startIdx + 1, endIdx - startIdx - 1).Trim();
            }

            string checkboxHtml = "";
            if (showCheckbox && !string.IsNullOrEmpty(empCode))
            {
                checkboxHtml = string.Format(
                    "<input type=\"checkbox\" class=\"approval-chk\" style=\"margin-right: 4px; vertical-align: middle; transform: scale(1.1); cursor: pointer;\" data-masterid=\"{0}\" data-empcode=\"{1}\" {2} onclick=\"updateApprovalStatus(this);\" />",
                    exitMasterId,
                    empCode,
                    isChecked ? "checked=\"checked\"" : ""
                );
            }

            string badgeContent = "";
            int autoFwdStart = displayText.IndexOf("[Auto forward", StringComparison.OrdinalIgnoreCase);
            int autoFwdEnd = autoFwdStart >= 0 ? displayText.IndexOf(']', autoFwdStart) : -1;

            if (autoFwdStart >= 0 && autoFwdEnd > autoFwdStart)
            {
                string beforeFwd = displayText.Substring(0, autoFwdStart).Trim();
                int colonIdx = displayText.IndexOf(':', autoFwdStart);
                string autoFwdText = "";
                if (colonIdx >= 0 && colonIdx < autoFwdEnd)
                {
                    autoFwdText = displayText.Substring(colonIdx + 1, autoFwdEnd - colonIdx - 1).Trim();
                }
                else
                {
                    string insideBracket = displayText.Substring(autoFwdStart + 1, autoFwdEnd - autoFwdStart - 1).Trim();
                    int toIdx = insideBracket.IndexOf("to", StringComparison.OrdinalIgnoreCase);
                    if (toIdx >= 0)
                    {
                        autoFwdText = insideBracket.Substring(toIdx + 2).Trim();
                    }
                    else
                    {
                        autoFwdText = insideBracket.Length >= 12 ? insideBracket.Substring(12).Trim() : insideBracket;
                    }
                }
                string afterFwd = displayText.Substring(autoFwdEnd + 1).Trim();

                // Extract ONLY the Primary Person (alternate persons are omitted)
                string[] fwdParts = autoFwdText.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string primaryPerson = "";

                for (int pi = 0; pi < fwdParts.Length; pi++)
                {
                    string fwdItem = fwdParts[pi].Trim();
                    bool isPrimary = fwdItem.IndexOf("[Primary Person]", StringComparison.OrdinalIgnoreCase) >= 0
                                  || fwdItem.IndexOf("[Primary Person 1]", StringComparison.OrdinalIgnoreCase) >= 0
                                  || fwdItem.IndexOf("(Primary Person)", StringComparison.OrdinalIgnoreCase) >= 0
                                  || fwdItem.StartsWith("Primary Person:", StringComparison.OrdinalIgnoreCase);

                    if (isPrimary)
                    {
                        primaryPerson = fwdItem;
                        break;
                    }
                }

                // Fallback to first person if no item explicitly marked as Primary Person
                if (string.IsNullOrEmpty(primaryPerson) && fwdParts.Length > 0)
                {
                    primaryPerson = fwdParts[0].Trim();
                }

                if (!string.IsNullOrEmpty(primaryPerson))
                {
                    // Clean up any Primary Person / Alternative tags
                    primaryPerson = System.Text.RegularExpressions.Regex.Replace(primaryPerson, @"\s*\[Primary Person.*?\]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    primaryPerson = System.Text.RegularExpressions.Regex.Replace(primaryPerson, @"\s*\(Primary Person.*?\)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    primaryPerson = System.Text.RegularExpressions.Regex.Replace(primaryPerson, @"^Primary Person:\s*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    primaryPerson = System.Text.RegularExpressions.Regex.Replace(primaryPerson, @"^Alternative:\s*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();
                }

                StringBuilder sbFwd = new StringBuilder();
                sbFwd.Append(HttpUtility.HtmlEncode(beforeFwd));
                if (!string.IsNullOrEmpty(primaryPerson))
                {
                    sbFwd.Append("<div class=\"auto-fwd-box\" style=\"margin-top: 4px; padding-top: 3px; border-top: 1px dashed rgba(255,255,255,0.45); font-size: 11px;\">");
                    sbFwd.Append("<span style=\"background-color: #6f42c1; color: #fff; padding: 2px 6px; border-radius: 3px; font-weight: bold; margin-right: 4px; display: inline-block;\"><i class=\"fa fa-random\"></i> Auto forward to</span> ");
                    sbFwd.Append("<span style=\"color: #fff; font-weight: bold; margin-right: 6px;\">").Append(HttpUtility.HtmlEncode(primaryPerson)).Append("</span>");

                    if (!string.IsNullOrEmpty(afterFwd))
                    {
                        sbFwd.Append("<span style=\"margin-left: 4px; opacity: 0.95;\">").Append(HttpUtility.HtmlEncode(afterFwd)).Append("</span>");
                    }
                    sbFwd.Append("</div>");
                }
                else if (!string.IsNullOrEmpty(afterFwd))
                {
                    sbFwd.Append(" ").Append(HttpUtility.HtmlEncode(afterFwd));
                }

                badgeContent = sbFwd.ToString();
            }
            else
            {
                badgeContent = HttpUtility.HtmlEncode(displayText);
            }

            if (badgeContent.Contains("[Forward to :"))
            {
                badgeContent = System.Text.RegularExpressions.Regex.Replace(
                    badgeContent,
                    @"\[Forward to :\s*(.*?)\]",
                    "<span style=\"background-color: #fd7e14; color: #fff; padding: 1px 5px; border-radius: 3px; font-weight: bold; margin: 0 3px;\"><i class=\"fa fa-share\"></i> Forward to:</span> $1"
                );
            }

            badges.Append("<span class=\"approval-status-badge ")
                .Append(cssClass)
                .Append("\">")
                .Append(checkboxHtml)
                .Append(badgeContent)
                .Append("</span>");
        }

        badges.Append("</div>");
        return badges.ToString();
    }

    protected bool IsExitRunning(object value)
    {
        return value != null && value != DBNull.Value && Convert.ToBoolean(value);
    }

    protected bool IsApprovalCompleted(object value)
    {
        return string.Equals(
            Convert.ToString(value).Trim(),
            "Completed",
            StringComparison.OrdinalIgnoreCase);
    }

    protected string FormatDeclineComment(object value)
    {
        string comment = Convert.ToString(value).Trim();
        return string.IsNullOrEmpty(comment) ? "" : "Comment: " + comment;
    }

    private TextBox DeclineCommentTextBox
    {
        get { return FindControlRecursive(this, "declineCommentTextBox") as TextBox; }
    }

    private Label IsRunningValidationLabel
    {
        get { return FindControlRecursive(this, "isRunningValidationLabel") as Label; }
    }

    private AjaxControlToolkit.ModalPopupExtender IsRunningModal
    {
        get
        {
            return FindControlRecursive(this, "isRunningModalPopupExtender")
                as AjaxControlToolkit.ModalPopupExtender;
        }
    }

    private static Control FindControlRecursive(Control root, string controlId)
    {
        Control control = root.FindControl(controlId);
        if (control != null)
        {
            return control;
        }

        foreach (Control child in root.Controls)
        {
            control = FindControlRecursive(child, controlId);
            if (control != null)
            {
                return control;
            }
        }

        return null;
    }

    private void ShowRunningStatusModal()
    {
        if (IsRunningModal != null)
        {
            IsRunningModal.Show();
        }
    }

    private void HideRunningStatusModal()
    {
        if (IsRunningModal != null)
        {
            IsRunningModal.Hide();
        }
    }

    protected void isRunningCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox isRunningCheckBox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)isRunningCheckBox.NamingContainer;
        DataKey dataKey = loadGridView.DataKeys[row.RowIndex];

        if (dataKey == null)
        {
            LoadInfo();
            return;
        }

        int exitMasterId = Convert.ToInt32(dataKey["ExitMasterId"]);

        if (isRunningCheckBox.Checked)
        {
            if (aEmployeeJobLeftEntryDAL.UpdateExitRunningStatus(exitMasterId, true, ""))
            {
                LoadInfo();
                aShowMessage.ShowMessageBox("Running status updated successfully.", this);
                return;
            }

            LoadInfo();
            aShowMessage.ShowMessageBox("Running status could not be updated.", this);
            return;
        }

        ViewState["RunningExitMasterId"] = exitMasterId.ToString();
        ViewState["PendingIsRunning"] = isRunningCheckBox.Checked;
        DeclineCommentTextBox.Text = "";
        IsRunningValidationLabel.Text = "";
        ShowRunningStatusModal();
    }

    protected void isRunningSubmitButton_OnClick(object sender, EventArgs e)
    {
        string declineComment = DeclineCommentTextBox.Text.Trim();

        if (string.IsNullOrEmpty(declineComment))
        {
            IsRunningValidationLabel.Text = "Decline Comment is required.";
            ShowRunningStatusModal();
            return;
        }

        if (ViewState["RunningExitMasterId"] == null || ViewState["PendingIsRunning"] == null)
        {
            IsRunningValidationLabel.Text = "Running status information was not found.";
            ShowRunningStatusModal();
            return;
        }

        int exitMasterId = Convert.ToInt32(ViewState["RunningExitMasterId"]);
        bool isRunning = Convert.ToBoolean(ViewState["PendingIsRunning"]);

        if (aEmployeeJobLeftEntryDAL.UpdateExitRunningStatus(exitMasterId, isRunning, declineComment))
        {
            ClearRunningStatusModal();
            HideRunningStatusModal();
            LoadInfo();
            aShowMessage.ShowMessageBox("Running status updated successfully.", this);
            return;
        }

        IsRunningValidationLabel.Text = "Running status could not be updated.";
        ShowRunningStatusModal();
    }

    protected void isRunningCancelButton_OnClick(object sender, EventArgs e)
    {
        ClearRunningStatusModal();
        LoadInfo();
        HideRunningStatusModal();
    }

    private void ClearRunningStatusModal()
    {
        ViewState.Remove("RunningExitMasterId");
        ViewState.Remove("PendingIsRunning");
        DeclineCommentTextBox.Text = "";
        IsRunningValidationLabel.Text = "";
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmpExitEntry.aspx"); 
    }
    private void PopUpClearence(int empId)
    {
        string url = "../Report_UI/ClearenceReportViwer.aspx?rptType=" + empId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    ClearenceFormDal aExitDal = new ClearenceFormDal();

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "appSta")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                mpe_1.Show();
                string Idd = datKey[0].ToString();


                DataTable dtDoc = aEmployeeJobLeftEntryDAL.GetDocDataById(Idd);
                if (dtDoc.Rows.Count > 0)
                {
                   
                    gv_AppraisalFunc.DataSource = dtDoc;
                    gv_AppraisalFunc.DataBind();
                }
                else
                {
                    gv_AppraisalFunc.DataSource = null;
                    gv_AppraisalFunc.DataBind(); 
                }

      

                
            }

         

        }

        if (e.CommandName == "Attachment")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                ModalPopupExtender1.Show();
                string Idd = datKey[1].ToString();


                DataTable dtDocNew = aExitDal.GetDocNewDataById(Idd.ToString());
                if (dtDocNew.Rows.Count > 0)
                {
                    ViewState["gvDocGrid_List"] = dtDocNew;
                    gv_Doc.DataSource = dtDocNew;
                    gv_Doc.DataBind();
                }
               
                else
                {
                    ViewState["gvDocGrid_List"] = null;
                    gv_Doc.DataSource = null;
                    gv_Doc.DataBind();
                }




            }



        }



        if (e.CommandName == "Clearence")
        {
            int empId = Convert.ToInt32(e.CommandArgument);

            if (empId > 0)
            {
             

                 
                    PopUpClearence(Convert.ToInt32(empId));
                
                  
               
            }
        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                

                Session["Status"] = "View";
                Response.Redirect("EmpExitEntryViewDetails.aspx?MID=" + Idd);

            }


        }

        if (e.CommandName == "DeleteData")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();


                Delete(Idd);

              
                
            }

          

            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string EmployeeJobLeftId = loadGridView.DataKeys[rowindex][0].ToString();

            //if (aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftById(EmployeeJobLeftId))
            //{
            //    LoadInfo();
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
               
            //}
        }
    }

    private void Delete(string idd)
    {
        EmpExitMasterDao aJobCreationDao = new EmpExitMasterDao();


        aJobCreationDao.ExitMasterId = Convert.ToInt32(idd);

        


        aJobCreationDao.DeleteBy = Convert.ToInt32(Session["UserId"]);



        aJobCreationDao.DeleteDate = DateTime.Now;
        //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
        //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
        bool status = aEmployeeJobLeftEntryDAL.DeleteJobCreationById(aJobCreationDao);

        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='EmpExitView.aspx';",
              true);
        }
    }
    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFilterDropDownLists();
       // LoadInfo();
    }

    protected void divisionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        departmentDropDownList.Items.Clear();

        if (!string.IsNullOrEmpty(divisionDropDownList.SelectedValue))
        {
            aCommonDataLoadDal.GetDepartmentByDivListALL(departmentDropDownList, divisionDropDownList.SelectedValue);
        }
        else if (!string.IsNullOrEmpty(companyDropDownList.SelectedValue))
        {
            using (DataTable departments = aCommonDataLoadDal.GetDDLComDepartmentAll(companyDropDownList.SelectedValue))
            {
                departmentDropDownList.DataSource = departments;
                departmentDropDownList.DataValueField = "Value";
                departmentDropDownList.DataTextField = "TextField";
                departmentDropDownList.DataBind();
            }
        }

      
    }

    protected void searchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfo();
    }

    protected void resetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmpExitView.aspx");
    }

    protected void exportToExcelButton_OnClick(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(companyDropDownList.SelectedValue))
        {
            aShowMessage.ShowMessageBox("Please select a company.", this);
            return;
        }

        DataTable source = GetMergedClearenceData();

        if (source == null || source.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("No data found.", this);
            return;
        }

        DataTable exportData = new DataTable();
        exportData.Columns.Add("SL");
        exportData.Columns.Add("Clearence Form Print");
        exportData.Columns.Add("Attachment");
        exportData.Columns.Add("Employee ID");
        exportData.Columns.Add("Employee Name");
        exportData.Columns.Add("Designation");
        exportData.Columns.Add("Employee Approval Status");
        exportData.Columns.Add("Total Days");
        exportData.Columns.Add("Separation Date");
        exportData.Columns.Add("Notification");
        exportData.Columns.Add("initiator by");
        exportData.Columns.Add("initiator Date");

        int sl = 1;
        foreach (DataRow row in source.Rows)
        {
            DataRow exportRow = exportData.NewRow();
            exportRow["SL"] = sl++;
            exportRow["Clearence Form Print"] = "Print";
            exportRow["Attachment"] = "Download";
            exportRow["Employee ID"] = row["EmpMasterCode"].ToString();
            exportRow["Employee Name"] = row["EmpName"].ToString();
            exportRow["Designation"] = row["Designation"].ToString();
            exportRow["Employee Approval Status"] = row["EmployeeApprovalStatus"].ToString();
            exportRow["Total Days"] = row["TotalDays"] == DBNull.Value
                ? ""
                : row["TotalDays"] + " Days";
            exportRow["Separation Date"] = row["JobLeftDate"] == DBNull.Value
                ? ""
                : Convert.ToDateTime(row["JobLeftDate"]).ToString("dd-MMM-yyyy");

            bool isRunning = row["IsRunning"] != DBNull.Value
                && Convert.ToBoolean(row["IsRunning"]);
            string notification = isRunning ? "Running" : "Not Running";
            string declineComment = row["DeclineComment"].ToString().Trim();

            if (!isRunning && !string.IsNullOrEmpty(declineComment))
            {
                notification += " - Comment: " + declineComment;
            }

            exportRow["Notification"] = notification;
            exportRow["initiator by"] = row["EntryBy"].ToString();
            exportRow["initiator Date"] = row["EntryDate"] == DBNull.Value
                ? ""
                : Convert.ToDateTime(row["EntryDate"]).ToString("dd-MMM-yyyy");
            exportData.Rows.Add(exportRow);
        }

        GridView exportGrid = new GridView();
        exportGrid.DataSource = exportData;
        exportGrid.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader(
            "content-disposition",
            "attachment;filename=EmployeeExitList_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
        Response.ContentType = "application/vnd.ms-excel";
        Response.Charset = "";

        using (StringWriter stringWriter = new StringWriter())
        using (HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter))
        {
            htmlWriter.Write("<h3>" + HttpUtility.HtmlEncode(companyDropDownList.SelectedItem.Text) + "</h3>");
            htmlWriter.Write("<h4>Employee Exit List</h4>");
            exportGrid.RenderControl(htmlWriter);
            Response.Write(stringWriter.ToString());
        }

        Response.Flush();
        Response.End();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        mpe_1.Hide();

    }

    protected void btnDocClose_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
       
    }

    [WebMethod]
    public static string UpdateApproval(int masterId, string empCode, string approvalStatus, bool isDone)
    {
        try
        {
            EmployeeJobLeftEntryDAL dal = new EmployeeJobLeftEntryDAL();
            bool success = dal.UpdateEmpExitDetailStatus(masterId, empCode, approvalStatus, isDone);
            return success ? "Success" : "Failed to update database.";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
