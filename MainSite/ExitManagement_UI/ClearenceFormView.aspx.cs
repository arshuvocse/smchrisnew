using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.ExitManagement_DAL;
using DAL.Permission_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class ExitManagement_UI_EmployeeJobLeftEntryView : System.Web.UI.Page
{

    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("../Default.aspx");
            return;
        }

        if (!IsPostBack)
        {
            GetCompany();
            //UserPersmissionValidation();

            LoadInfo();
        }
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

                    //addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
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

    private void LoadInfo()
    {
        DataTable dataTable = new DataTable();
        string currentEmpId = Session["EmpInfoId"] != null ? Session["EmpInfoId"].ToString() : "0";

        DataTable dtMasters = aEmployeeJobLeftEntryDAL.GetPendingClearanceMasterIds(currentEmpId);
        if (dtMasters == null || dtMasters.Rows.Count == 0)
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            return;
        }

        List<string> masterIds = new List<string>();
        foreach (DataRow row in dtMasters.Rows)
        {
            if (row["MasterId"] != null && !string.IsNullOrEmpty(row["MasterId"].ToString()))
            {
                masterIds.Add(row["MasterId"].ToString());
            }
        }

        if (masterIds.Count == 0)
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            return;
        }

        string filter = " EIM.ExitMasterId IN (" + string.Join(",", masterIds) + ") ";

        dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlApprovalNew(filter);

        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();

            ClearenceFormDal aEmployeeInfoListReportDAL = new ClearenceFormDal();

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                string empinfoidForMain = loadGridView.DataKeys[i]["empinfoidForMain"] != null ? loadGridView.DataKeys[i]["empinfoidForMain"].ToString() : "";
                string EmpInfoIdApproval = loadGridView.DataKeys[i]["EmpInfoIdApproval"] != null ? loadGridView.DataKeys[i]["EmpInfoIdApproval"].ToString() : "";

                loadGridView.Columns[1].Visible = true;

                if (empinfoidForMain != EmpInfoIdApproval)
                {
                    continue;
                }

                DataTable dtEmpInfo =
                    aEmployeeInfoListReportDAL.GetResourceInfoApprovalPerSOn(
                        Convert.ToInt32(loadGridView.DataKeys[i]["ExitDetailId"].ToString()));
            }
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmployeeJobLeftEntry.aspx");
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        mpe_1.Hide();

    }
    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Clearance")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[1].ToString();


                string jobLeftId = datKey[0].ToString();
                string ExitDetailId = datKey[2].ToString();
                string ApprovalStatus = datKey[3].ToString();
                string ExitMasterId = datKey[4].ToString();
                string CompanyName = datKey["CompanyName"].ToString().Trim();
                Session["EmployeeJobLeftId"] = "";
                Session["EmployeeJobLeftId"] = jobLeftId;

                //Session["Status"] = "Edit";
                string clearancePage = CompanyName.Equals("SMC EL", StringComparison.OrdinalIgnoreCase)
                    ? "ClearenceFormEL.aspx"
                    : "ClearenceForm.aspx";

                Response.Redirect("~/ExitManagement_UI/" + clearancePage + "?EMPID=" + Idd + "&ExitDetailId=" + ExitDetailId + "&ApprovalStatus=" + ApprovalStatus + "&ExitMasterId=" + ExitMasterId);
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
        if (e.CommandName == "appComm")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                mpe_1.Show();
                string Idd = datKey[4].ToString();


                DataTable dtDoc = aEmployeeJobLeftEntryDAL.GetDocDataByIdOtherComment(Idd);
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


        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                Session["EmployeeJobLeftId"] = "";
                Session["EmployeeJobLeftId"] = Idd;

                Session["Status"] = "View";

            }

            Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");

        }

        if (e.CommandName == "DeleteData")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                Session["EmployeeJobLeftId"] = "";
                Session["EmployeeJobLeftId"] = Idd;
                Session["Status"] = "Delete";
            }

            Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");

            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string EmployeeJobLeftId = loadGridView.DataKeys[rowindex][0].ToString();

            //if (aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftById(EmployeeJobLeftId))
            //{
            //    LoadInfo();
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);

            //}
        }
    }




    protected void gv_History_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Clearence")
        {
            int empId = Convert.ToInt32(e.CommandArgument);

            if (empId > 0)
            {



                PopUpClearence(Convert.ToInt32(empId));



            }
        }






    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    private void PopUpClearence(int empId)
    {
        string url = "../Report_UI/ClearenceReportViwer.aspx?rptType=" + empId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void bllHistory_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();


        string currentEmpId = Session["EmpInfoId"] != null ? Session["EmpInfoId"].ToString() : "0";
        string historyFilter = @"  and EIM.ExitMasterId IN (
            SELECT MasterId FROM dbo.tblEmpExitDetail 
            WHERE IsDone = 1 
              AND (
                  EmpInfoIdApproval = '" + currentEmpId + @"' 
                  OR EmpInfoId = '" + currentEmpId + @"' 
                  OR IsForwardEmpId = '" + currentEmpId + @"'
                  OR EmpInfoIdApproval IN (
                      SELECT m.EmployeeId 
                      FROM dbo.tblClearanceForwardingMaster m 
                      INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                      WHERE d.SupervisedEmployeeId = '" + currentEmpId + @"'
                  )
              )
        ) ";
        DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlApprovalsss(historyFilter);
  //  dataTable   = aEmployeeJobLeftEntryDAL.LoadInformationALlApproval(" AND EIM.ExitMasterId IN (SELECT MasterId FROM dbo.tblEmpExitDetail WHERE  IsDone=0 AND EmpInfoIdApproval='" + Session["EmpInfoId"].ToString() + "')");
        //DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlClear(" AND   EPE.CompanyId IN (" + CompanyId() + ") AND EIM.ExitMasterId IN (SELECT MasterId FROM dbo.tblEmpExitDetail WHERE  IsDone=0 AND EmpInfoIdApproval='" + Session["EmpInfoId"].ToString() + "')");

        if (dataTable.Rows.Count > 0)
        {
            gv_History.DataSource = dataTable;
            gv_History.DataBind();

        }
        else
        {
            gv_History.DataSource = null;
            gv_History.DataBind();
        }

    }

    protected void LinkButton2_OnClick(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }

    protected string GetApprovedForwardInfo(object approvedByObj, object forwardedToObj)
    {
        string approvedBy = approvedByObj != null ? approvedByObj.ToString().Trim() : "";
        string forwardedTo = forwardedToObj != null ? forwardedToObj.ToString().Trim() : "";

        StringBuilder sb = new StringBuilder();
        if (!string.IsNullOrEmpty(approvedBy))
        {
            sb.Append("<div><span style=\"background-color: #28a745; color: #fff; padding: 2px 6px; border-radius: 3px; font-size: 11px; font-weight: bold; margin-right: 4px; display: inline-block;\">Approved By:</span><span style=\"font-weight: 500;\">");
            sb.Append(HttpUtility.HtmlEncode(approvedBy));
            sb.Append("</span></div>");
        }
        if (!string.IsNullOrEmpty(forwardedTo))
        {
            sb.Append("<div style=\"margin-top: 4px;\"><span style=\"background-color: #fd7e14; color: #fff; padding: 2px 6px; border-radius: 3px; font-size: 11px; font-weight: bold; margin-right: 4px; display: inline-block;\"><i class=\"fa fa-share\"></i> Forward to:</span><span style=\"font-weight: 500;\">");
            sb.Append(HttpUtility.HtmlEncode(forwardedTo));
            sb.Append("</span></div>");
        }
        else
        {
            sb.Append("<div style=\"margin-top: 4px;\"><span style=\"background-color: #6c757d; color: #fff; padding: 2px 6px; border-radius: 3px; font-size: 11px; font-weight: bold; margin-right: 4px; display: inline-block;\"><i class=\"fa fa-share\"></i> Forward to:</span><span style=\"color: #6c757d; font-style: italic; font-weight: 500;\">N/A (Not Forwarded)</span></div>");
        }
        return sb.ToString();
    }

    protected string GetForwardStatusBadge(object forwardObj)
    {
        if (forwardObj == null || forwardObj == DBNull.Value) return "";
        string forward = forwardObj.ToString();

        try
        {
            string badgeStyle = "padding: 3px 8px; border-radius: 10px; font-size: 12px; font-weight: bold; display: inline-block; line-height: 1;";

            // Highlight [Auto Forward] if present, or prepend if forwarded
            string autoFwdBadge = "<span style=\"" + badgeStyle + " background-color: #6f42c1; color: white; margin-right: 5px;\"><i class=\"fa fa-random\"></i> AUTO FORWARD</span>";
            if (forward.Contains("[Auto Forward]"))
            {
                forward = forward.Replace("[Auto Forward]", autoFwdBadge);
            }
            else if (forward.StartsWith("Forwarded from", StringComparison.OrdinalIgnoreCase))
            {
                forward = autoFwdBadge + " " + forward;
            }

            // Strip out any Primary Person tag so it is not displayed
            forward = System.Text.RegularExpressions.Regex.Replace(forward, @"\s*(\[|\()?Primary Person[^\]\)]*(\]|\))?", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (forward.Contains("Pending"))
            {
                return forward.Replace("Pending", "<span style=\"" + badgeStyle + " background-color: #dc3545; color: white;\">Pending</span>");
            }
            else if (forward.Contains("Done"))
            {
                return forward.Replace("Done", "<span style=\"" + badgeStyle + " background-color: #28a745; color: white;\">Done</span>");
            }
            else if (forward.Contains("N/A"))
            {
                return forward.Replace("N/A", "<span style=\"" + badgeStyle + " background-color: #ffc107; color: black;\">N/A</span>");
            }
        }
        catch
        {
            // fallback
        }

        return forward;
    }

    protected string GetAssignListStatusColored(object statusObj)
    {
        if (statusObj == null || statusObj == DBNull.Value) return "";
        string status = statusObj.ToString();

        try
        {
            string[] parts = status.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            List<string> formattedParts = new List<string>();

            string badgeStyle = "padding: 4px 8px; border-radius: 12px; font-size: 11px; font-weight: bold; display: inline-block; line-height: 1.4; margin: 3px; color: white;";

            foreach (string part in parts)
            {
                bool isDone = part.Contains("| (Done)");
                bool isPending = part.Contains("| (Pending)");
                string name = part.Replace("| (Pending)", "").Replace("| (Done)", "").Trim();

                string bgColor = isDone ? "#28a745" : (isPending ? "#fd7e14" : "#6c757d");

                // Check if there is a forwarded section like [Forwarded To: ...] or [Auto Forwarded To: ...]
                int fwdIdx = name.IndexOf("[Forwarded To:", StringComparison.OrdinalIgnoreCase);
                int prefixLen = 14;
                if (fwdIdx < 0)
                {
                    fwdIdx = name.IndexOf("[Auto Forwarded To:", StringComparison.OrdinalIgnoreCase);
                    prefixLen = 19;
                }
                if (fwdIdx < 0)
                {
                    fwdIdx = name.IndexOf("[Auto forward:", StringComparison.OrdinalIgnoreCase);
                    prefixLen = 14;
                }
                if (fwdIdx < 0)
                {
                    fwdIdx = name.IndexOf("[Auto Forwarded:", StringComparison.OrdinalIgnoreCase);
                    prefixLen = 16;
                }

                int lastBracket = name.LastIndexOf(']');
                if (fwdIdx >= 0 && lastBracket > fwdIdx + prefixLen)
                {
                    string approverName = name.Substring(0, fwdIdx).Trim();
                    string fwdContent = name.Substring(fwdIdx + prefixLen, lastBracket - (fwdIdx + prefixLen)).Trim();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style=\"" + badgeStyle + " background-color: " + bgColor + "; border-radius: 8px; padding: 6px 10px;\">");
                    sb.Append("<div style=\"font-size: 11.5px;\"><strong>" + HttpUtility.HtmlEncode(approverName) + "</strong></div>");
                    sb.Append("<div style=\"margin-top: 3px; padding-top: 3px; border-top: 1px dashed rgba(255,255,255,0.4); font-size: 10.5px;\">");

                    // Extract ONLY the Primary Person employee (alternate persons are omitted)
                    string[] fwdList = fwdContent.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    string primaryPerson = "";

                    for (int fi = 0; fi < fwdList.Length; fi++)
                    {
                        string fwdItem = fwdList[fi].Trim();
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
                    if (string.IsNullOrEmpty(primaryPerson) && fwdList.Length > 0)
                    {
                        primaryPerson = fwdList[0].Trim();
                    }

                    if (!string.IsNullOrEmpty(primaryPerson))
                    {
                        // Clean up any Primary Person / role tags from name
                        primaryPerson = System.Text.RegularExpressions.Regex.Replace(primaryPerson, @"\s*\[Primary Person.*?\]", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        primaryPerson = System.Text.RegularExpressions.Regex.Replace(primaryPerson, @"\s*\(Primary Person.*?\)", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        primaryPerson = System.Text.RegularExpressions.Regex.Replace(primaryPerson, @"^Primary Person:\s*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();

                        sb.Append("<span style=\"background-color: #6f42c1; color: #fff; padding: 2px 7px; border-radius: 4px; font-size: 10.5px; font-weight: bold; margin-right: 5px; display: inline-block; box-shadow: 0 1px 2px rgba(0,0,0,0.2);\"><i class=\"fa fa-random\"></i> Auto Forwarded to</span> ");
                        sb.Append("<span style=\"display: inline-block; background: rgba(0,0,0,0.25); border-radius: 4px; padding: 2px 6px; margin: 1px 2px; font-weight: bold;\">");
                        sb.Append(HttpUtility.HtmlEncode(primaryPerson));
                        sb.Append("</span>");
                    }

                    sb.Append("</div>");
                    sb.Append("</div>");
                    formattedParts.Add(sb.ToString());
                }
                else
                {
                    formattedParts.Add("<span style=\"" + badgeStyle + " background-color: " + bgColor + ";\">" + HttpUtility.HtmlEncode(name) + "</span>");
                }
            }

            return string.Join(" ", formattedParts);
        }
        catch
        {
            return HttpUtility.HtmlEncode(status);
        }
    }
}
