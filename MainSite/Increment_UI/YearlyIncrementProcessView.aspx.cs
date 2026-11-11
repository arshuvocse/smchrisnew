using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAL.Report_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;
using PermissionDAL = DAL.Permission_DAL.PermissionDAL;

public partial class Increment_UI_YearlyIncrementProcessView : System.Web.UI.Page
{
    DataTable aDataTable = new DataTable();
    IncrementDal aSuspendDal = new IncrementDal();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    EmployeeProfileDAL aEmployeeInfoListReportDAL = new EmployeeProfileDAL();
    EmployeeProfileDAL ddd = new EmployeeProfileDAL();
    SupervisorMenuAppDAL ddddd = new SupervisorMenuAppDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
            LoadDropDownList();

            //GetCompany();
          //  UserPersmissionValidation();
            //EmpIncrementLoad();
        }

        try
        {

            //loadGridView.UseAccessibleHeader = true;
            //loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            //loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            //loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }
    }

    public void ReportingEmpData(string empinfoid, DataTable aDataTable)
    {
        DataRow dataRow = null;
        DataTable dtdata1 = ddd.GetRefEmpInfoDAL2(empinfoid);
        DataTable dtdata = ddddd.LoadEmpGenInfoGetRef(" AND E.EmpInfoId='" + dtdata1.Rows[0]["ReferenceID"].ToString() + "' ");

        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["ReferenceID"] = dtdata.Rows[0]["FromEmpInfoId"].ToString();

            aDataTable.Rows.Add(dataRow);

            ReportingEmpData(dtdata.Rows[0]["FromEmpInfoId"].ToString(), aDataTable);
        }

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }


    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompany(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            aSuspendDal.LoadFinancialYear(ddlFinYear, ddlCompany.SelectedValue);
            aSuspendDal.LoadDivision(ddlDivision, ddlCompany.SelectedValue);
            Session["CompanyId"] = ddlCompany.SelectedValue;
            using (DataTable dt222 = _commonDataLoad.GetEmpDDL(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
        }
        else
        {
            ddlFinYear.Items.Clear();
            ddlEmpInfo.Items.Clear();
        }
    }
   

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);

        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
        }
    }
     

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "")
            {

                DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
                if (dtgetdata.Rows.Count > 0)
                {
                    if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                    {
                        wing.Visible = false;
                        ddlWing.Items.Clear();
                        _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        // ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    }
                    else
                    {
                        wing.Visible = true;
                        ddlWing.Items.Clear();
                        _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);

                    }
                }
            }
            else
            {

            }
            if (ddlDepartment.SelectedIndex == 0)
            {
                wing.Visible = true;
                ddlWing.SelectedValue = "";
                //  ddlWing.DataBind();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWing.SelectedValue != "")
        {
            _commonDataLoad.GetDepartmentList(ddlDepartment, ddlDivision.SelectedValue);
        }
        else
        {
            ddlDepartment.Items.Clear();
        }
    }
    //public void GetCompany()
    //{
    //    DataTable dtcomp = aPermissionDal.GetCompany();
    //    lchk_Company.DataValueField = "CompanyId";
    //    lchk_Company.DataTextField = "ShortName";
    //    lchk_Company.DataSource = dtcomp;
    //    lchk_Company.DataBind();

    //    DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
    //    for (int i = 0; i < userdata.Rows.Count; i++)
    //    {
    //        for (int j = 0; j < lchk_Company.Items.Count; j++)
    //        {
    //            if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
    //            {
    //                lchk_Company.Items[j].Selected = true;


    //            }
    //        }
    //    }
    //}

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
        //for (int i = 0; i < lchk_Company.Items.Count; i++)
        //{
        //    if (lchk_Company.Items[i].Selected)
        //    {
        //        companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
        //    }
        //}
        //companyid = companyid.TrimEnd(',');
        return companyid;
    }

    private string GenerateParamiterList()
    {


        string parameter = " WHERE  (tblIncrementAppLog.Version=LogApp.MaxVer OR tblIncrementAppLog.Version IS NULL) AND EGI.IsActive = 1 AND EGI.EmployeeStatus IN ('Active') AND   (IsDelete=0 OR IsDelete IS NULL)";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND INC.CompanyId = " + ddlCompany.SelectedValue ;
        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and INC.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + "  ";
        }

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND INC.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND E.DivisionId = " + ddlDivision.SelectedValue;
        }

       

        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND E.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND E.DepartmentId  = " + ddlDepartment.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    private void EmpIncrementLoad()
    {
             if (ddlCompany.SelectedValue != "")
        {
        aDataTable = aSuspendDal.LoadIncrementInfoForProcess(GenerateParamiterList());
        if (aDataTable.Rows.Count>0)
        {
            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();
            //loadGridView.UseAccessibleHeader = true;
            //loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            //loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            //loadGridView.UseAccessibleHeader = true;
        }
        else
        {
            aShowMessage.ShowMessageBox("No Data Found!!!", this);
            loadGridView.DataSource = null;
            loadGridView.DataBind(); 
        }
        }
             else
             {
                 loadGridView.DataSource = null;
                 loadGridView.DataBind();
                 aShowMessage.ShowMessageBox("Please select company name!!!", this);
             }
    }

    private string parm2()
    {

        string parameter = "  ";


        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND  Emp.CompanyId= " + ddlCompany.SelectedValue;
        }



      



        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionId = " + ddlDivision.SelectedValue;
        }

       

        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND Emp.DepartmentId  = " + ddlDepartment.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblIncrements
                      where t.IncrementId == MainID && t.AutoProcess == "Manually Updated"
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }

    private bool CheckPostedCsncel(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblIncrements
                      where t.IncrementId == MainID && (t.ActionStatus2 != "Posted" || t.ActionStatus2 != "Cancel")
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];

            Session["Status"] = "View";
            string suspendId = null;

            if (dataKey != null)
                suspendId = dataKey[0].ToString();

            Session["suspendId"] = suspendId;
            Response.Redirect("EmployeeSuspend.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
             int rowindex = Convert.ToInt32(e.CommandArgument);
            
            DataTable aTable =
                             aSuspendDal.DeleteValidattionForEffectiveDate(Convert.ToInt32(e.CommandArgument).ToString());
            if (aTable.Rows.Count > 0)
            {


                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {


                    int mainID = Convert.ToInt32(Convert.ToInt32(e.CommandArgument));

                if (!CheckAchievementsAllocateOrNot(mainID))
                {

                    if (!CheckPostedCsncel(mainID))
                {
                    IncrementDao aDAO = new IncrementDao();

                    aDAO.IncrementId = Convert.ToInt32(e.CommandArgument);
                    aDAO.IsDelete = true;
                    aDAO.DeleteBy = Convert.ToInt32(Session["UserId"]);
                    aDAO.DeleteDate = DateTime.Now;


                    ResetEmpGeneralInfo(aDAO.IncrementId);

                    aSuspendDal.DeleteIncrementMaster(aDAO);
                   

                
                  

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Data Deleted Successfully!!');",
                     true);
                }

                    else
                    {
                        aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                    }
                }
                else
                {
                    aShowMessage.ShowMessageBox("Can not be Updated", this);
                }
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted!!",this);
                }

            }
            
 

        }

        if (e.CommandName == "Preview")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = loadGridView.DataKeys[0];

            if (datKey != null)
            {
                
                string EmployeeId = datKey["EmployeeId"].ToString();
                Response.Redirect("MemoPrintIncrement.aspx?mid=" + e.CommandArgument.ToString() + "&EmpId=" + EmployeeId);
                Session["Status"] = "View";

            }



        }
    }

    private void ResetEmpGeneralInfo(int incrementId)
    {
        DataTable aTable = aSuspendDal.FetchEmployeeInfoById(incrementId);

        if (aTable.Rows.Count > 0)
        {
            Int32 employeeId = aTable.Rows[0].Field<Int32>("EmployeeId");
            Int32 currentStepId = aTable.Rows[0].Field<Int32>("CurrentStepId");

            EmpGeneralInfo aInfo = new EmpGeneralInfo();

            aInfo.SalScaleId = currentStepId;
            aInfo.EmpInfoId = employeeId;

            aSuspendDal.ResetEmployeeIncrementalStepInfo(aInfo);
        }
    }


    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
       Response.Redirect("IncrementEntry2.aspx");
    }

    protected void reloadButton_OnClick(object sender, EventArgs e)
    {
        EmpIncrementLoad();
    }


    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        EmpIncrementLoad();
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("YearlyIncrementProcessView.aspx");
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.EmpIncrementLoad();
    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        if (Validation2())
        {


            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                var chkBoxRows = (CheckBox) loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

                if (chkBoxRows.Checked)
                {
                    HiddenField HFIncrementId = (HiddenField) loadGridView.Rows[i].FindControl("HFIncrementId");
                    HiddenField HFEmployeeId = (HiddenField) loadGridView.Rows[i].FindControl("HFEmployeeId");
                    GetDataforMEMEPrint(HFIncrementId.Value, HFIncrementId.Value, HFEmployeeId.Value);

                    DataTable aTable =
                        aDAL.DeleteValidattionForEffectiveDate(Convert.ToInt32(HFIncrementId.Value).ToString());
                    if (aTable.Rows.Count > 0)
                    {

                        Update(HFIncrementId.Value);
                    }
                    else
                    {
                        Save(HFIncrementId.Value);

                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successfull...');",
                true);
        }
    }


    private void Update(string IncrementId)
    {

        if (Validation2())
        {
            MemoPrintIncrementDAO aDAO = new MemoPrintIncrementDAO();




            aDAO.IncrementId = Convert.ToInt32(IncrementId);

            aDAO.CompanyId = Convert.ToInt32(ComId.Value);
            aDAO.HeaderInfo = lblLabelInfo.Text;
            aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
            aDAO.EmpCode = lblEmployeeCode.Text;
            aDAO.EmpName = lblEmp.Text;
            aDAO.Designation = lblDesignation.Text;
            aDAO.Department = lblDepartment.Text;
            aDAO.PreviousStep = txtPreSalStep.Text;
            aDAO.PlaceofPosting = lblOffice.Text;
            aDAO.IncrementalStep = txtIncrementalStep.Text;
            aDAO.Salutation = txtSalutation.Text;
            aDAO.FirstParagraph = WebUtility.HtmlEncode(txtBodyofletter.Text);
            aDAO.ComplimentaryClose = WebUtility.HtmlEncode(txtComplimentaryClose.Text);
            aDAO.YoursSincerely = txtSincerely.Text;
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.CopyTo = WebUtility.HtmlEncode(txtCopyTO.Text);
            try
            {
                aDAO.ToEmployee = Convert.ToInt32(repEmpIdHiddenField.Value);
            }
            catch (Exception)
            {
                aDAO.ToEmployee = null;
                //throw;
            }


            aDAO.CompanyName = lblCompany.Text;
            aDAO.Subject = txtSubject.Text;

            aDAL.UpdateInfo(aDAO);


            aDAL.DeleteMemoIncrementDetails(aDAO.IncrementId.ToString());
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                Label lbl_SalaryBreakUp =
                                       (Label)
                                           KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                Label lbl_Particulars =
                  (Label)
                      KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                Label lbl_SalaryBreakUpPre =
            (Label)
                KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUpPre");

                if (lbl_Particulars.Text.Trim() != "")
                {
                    MemoPrintIncrementDetailsDAO ADetailsDao = new MemoPrintIncrementDetailsDAO()
                    {
                        MemoIncrementId = aDAO.IncrementId,

                        PName = lbl_Particulars.Text.Trim(),
                        PAmount = Convert.ToDecimal(lbl_SalaryBreakUp.Text.Trim()),
                        PAmountPre = Convert.ToDecimal(lbl_SalaryBreakUpPre.Text.Trim())

                    };
                    int idd =
                        aDAL.MemoIncrementDetailsSaveInfo(
                            ADetailsDao);
                }
            }


        }
    }
    public void Save(string IncrementId)
    {

        if (Validation2())
        {
            MemoPrintIncrementDAO aDAO = new MemoPrintIncrementDAO();

            aDAO.IncrementId = Convert.ToInt32(IncrementId);
            aDAO.CompanyId = Convert.ToInt32(ComId.Value);
            aDAO.HeaderInfo = lblLabelInfo.Text;
            aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
            aDAO.EmpCode = lblEmployeeCode.Text;
            aDAO.EmpName = lblEmp.Text;
            aDAO.Designation = lblDesignation.Text;
            aDAO.Department = lblDepartment.Text;
            aDAO.PreviousStep = txtPreSalStep.Text;
            aDAO.PlaceofPosting = lblOffice.Text;
            aDAO.IncrementalStep = txtIncrementalStep.Text;
            aDAO.Salutation = txtSalutation.Text;
            aDAO.FirstParagraph = WebUtility.HtmlEncode(txtBodyofletter.Text);
            aDAO.ComplimentaryClose = WebUtility.HtmlEncode(txtComplimentaryClose.Text);
            aDAO.YoursSincerely = txtSincerely.Text;
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.CopyTo = WebUtility.HtmlEncode(txtCopyTO.Text);
            try
            {
                aDAO.ToEmployee = Convert.ToInt32(repEmpIdHiddenField.Value);
            }
            catch (Exception)
            {
                aDAO.ToEmployee = null;
                //throw;
            }

            aDAO.CompanyName = lblCompany.Text;
            aDAO.Subject = txtSubject.Text;

            int id = aDAL.SaveInfo(aDAO);

            aDAL.DeleteMemoIncrementDetails(aDAO.IncrementId.ToString());
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {

                Label lbl_SalaryBreakUpPre =
                (Label)
                    KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUpPre");
            
                Label lbl_SalaryBreakUp =
                                       (Label)
                                           KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                Label lbl_Particulars =
                  (Label)
                      KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                if (lbl_Particulars.Text.Trim() != "")
                {
                    MemoPrintIncrementDetailsDAO ADetailsDao = new MemoPrintIncrementDetailsDAO()
                    {
                        MemoIncrementId = aDAO.IncrementId,

                        PName = lbl_Particulars.Text.Trim(),
                        PAmount = Convert.ToDecimal(lbl_SalaryBreakUp.Text.Trim()),
                        PAmountPre = Convert.ToDecimal(lbl_SalaryBreakUpPre.Text.Trim())


                    };
                    int idd =
                        aDAL.MemoIncrementDetailsSaveInfo(
                            ADetailsDao);
                }
            }


        





            //Response.Redirect("MemoPrintIncrement.aspx?mid=" + mid + "&EmpId=" + EmpID);
        }

    }

    private bool Validation2()
    {
        Int32 count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("Please Select at least one employee !!!");
            return false;
        }

        return true;
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    private void LoadEmployeeData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aDAL.LoadEmpAllInfofById(id);
        if (dtdata.Rows.Count > 0)
        {

            empCatId.Value = dtdata.Rows[0]["EmpCategoryId"].ToString();
            EmpIdHiddenfield.Value = dtdata.Rows[0]["EmployeeId"].ToString();
            ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
            Session["CompanyId"] = "";
            Session["CompanyId"] = ComId.Value;
            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
            ComName.Value = dtdata.Rows[0]["ShortName"].ToString();



            txtSalutation.Text = "Dear " + dtdata.Rows[0]["EmpName"].ToString() + ", ";




            lblCompany.Text = dtdata.Rows[0]["CompanyName"].ToString();
            lblEmployeeCode.Text = dtdata.Rows[0]["EmployeeCode"].ToString();

            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();




            lblDepartment.Text = dtdata.Rows[0]["DivisionName"].ToString();




            lblOffice.Text = dtdata.Rows[0]["SalaryLocation"].ToString();
            txtPreSalStep.Text = dtdata.Rows[0]["CurrentStep"].ToString();
            txtIncrementalStep.Text = dtdata.Rows[0]["IncrementalStep"].ToString();



        }
    }
    private string NoGenerator(int id)
    {
        string code = string.Empty;
        int Id = id + 1;


        code = Id.ToString();
        return code;
    }
    MemoPrintIncrementDAL aDAL = new MemoPrintIncrementDAL();
   
    private void MethodAutoId()
    {
        DataTable dt = aDAL.GetId(Convert.ToInt32(ComId.Value));
        MasterIdHiddenField.Value = NoGenerator(Convert.ToInt32(dt.Rows[0][0].ToString()));
    }

    private void GetDataforMEMEPrint(string IncrementId, string EmployeeId, string EmpIddd)
    {




        lblDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
        LoadEmployeeData(Convert.ToInt32(EmployeeId));
        MethodAutoId();
        lblLabelInfo.Text = ComName.Value + "/HR/" + DateTime.Now.Year + " - " +
                            MasterIdHiddenField.Value.ToString();
        // LoadTasks();
        submitButton.Visible = true;
        string EffectDate = "";
        DataTable dtdata = aDAL.LoadMemoPrintGetEffectivedateIncrementByMId(Convert.ToInt32(IncrementId));
        if (dtdata.Rows.Count > 0)
        {

            HFSalaryGradeId.Value = dtdata.Rows[0]["SalaryGradeId"].ToString();
            DataTable dtdataSig = aDAL.LoadSignaturePerson(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(ComId.Value));


            if (ComId.Value == "1")
            {
                txtComplimentaryClose.Text = "We would like to take this opportunity to express our appreciation for your contribution to the Company and hope that you will continue to strive for better results.";
            }
            else
            {
                txtComplimentaryClose.Text = @"The Management looks forward to your contribution towards progress and prosperity of the organization.";
            }
            if (dtdataSig.Rows.Count > 0)
            {
                repEmpIdHiddenField.Value = dtdataSig.Rows[0]["EmployeeId"].ToString();

                DataTable dtdEmp = aDAL.LoadEmpName(Convert.ToInt32(repEmpIdHiddenField.Value));
                EmployeeNameTextBox.Text = dtdEmp.Rows[0]["EmpName"].ToString();

            }

            GradeCode.Value = dtdata.Rows[0]["GradeCode"].ToString();
            HFIncrementalStepId.Value = dtdata.Rows[0]["IncrementalStepId"].ToString();

            try
            {
                EffectDate = Convert.ToDateTime(dtdata.Rows[0]["EffectiveDate"])
                             .ToString("MMMM dd, yyyy"
);
            }
            catch (Exception)
            {


            }

        }
        DataTable dtPart = aDAL.LoadParticularsGridView();
        if (dtPart.Rows.Count > 0)
        {
            KeyResponGridView.DataSource = dtPart;
            KeyResponGridView.DataBind();




            if (empCatId.Value == "2")
            {
                /// Graded
                decimal res = 0;

                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {

                    Label lbl_SalaryBreakUp =
                        (Label)
                            KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                    Label lbl_Particulars =
                        (Label)
                            KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                    try
                    {







                        if (lbl_Particulars.Text.Trim() == "Basic Pay")
                        {
                            DataTable aDataTable =
                         aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Text);
                            Decimal basicAmount = 0;
                            try
                            {
                                basicAmount =
                                    Math.Round(
                                        Convert.ToDecimal(aDataTable.Rows[0]["Basic"].ToString()),
                                        0);
                            }
                            catch (Exception)
                            {


                            }


                            basicAmount = (Math.Round(basicAmount + (basicAmount * 5) / 100, 0));
                            lbl_SalaryBreakUp.Text = Math.Round(basicAmount, 0).ToString();



                        }





                        if (lbl_Particulars.Text.Trim() == "House Rent")
                        {

                            DataTable aDataTable =
                       aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Text);
                            Decimal HouseResnt = 0;
                            try
                            {
                                HouseResnt =
                                    Math.Round(
                                        Convert.ToDecimal(aDataTable.Rows[0]["HRent"].ToString()),
                                        0);
                            }
                            catch (Exception)
                            {


                            }


                            HouseResnt = (Math.Round(HouseResnt + (HouseResnt * 5) / 100, 0));
                            lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Medical")
                        {


                            DataTable aDataTable =
                    aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Text);
                            Decimal Medical = 0;
                            try
                            {
                                Medical =
                                    Math.Round(
                                        Convert.ToDecimal(aDataTable.Rows[0]["Medi"].ToString()),
                                        0);
                            }
                            catch (Exception)
                            {


                            }


                            Medical = (Math.Round(Medical + (Medical * 5) / 100, 0));
                            lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                        }

                        if (lbl_Particulars.Text.Trim() == "Conveyance")
                        {

                            DataTable aDataTable =
                 aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Text);
                            Decimal Conveyance = 0;
                            try
                            {


                                Conveyance =
                                    Math.Round(
                                        Convert.ToDecimal(aDataTable.Rows[0]["Conv"].ToString()),
                                        0);
                            }
                            catch (Exception)
                            {


                            }


                            DataTable dtConveyance =
            aDAL.CheckConveyanceByMasterCode(lblEmployeeCode.Text.Trim());

                            if (dtConveyance.Rows.Count>0)
                            {
                                Conveyance = 0;
                            }
                            else
                            {
                                Conveyance = (Math.Round(Conveyance + (Conveyance * 5) / 100, 0));
                                
                            }


                            lbl_SalaryBreakUp.Text = Conveyance.ToString();

                        }


                        if (lbl_Particulars.Text.Trim() == "Washing")
                        {
                            DataTable aDataTable =
              aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Text);
                            Decimal Wash = 0;
                            try
                            {
                                Wash =
                                    Math.Round(
                                        Convert.ToDecimal(aDataTable.Rows[0]["Wash"].ToString()),
                                        0);
                            }
                            catch (Exception)
                            {


                            }


                            Wash = (Math.Round(Wash + (Wash * 5) / 100, 0));


                            lbl_SalaryBreakUp.Text = Wash.ToString();


                        }
                    }

                    catch (Exception ex)
                    {

                    }

                    decimal res2 = Convert.ToDecimal(lbl_SalaryBreakUp.Text);

                    res += Math.Round(res2, 0);
                }


                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    Label lbl_Particulars =
                        (Label)
                            KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                    Label lbl_SalaryBreakUp =
                        (Label)
                            KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");


                    if (lbl_Particulars.Text.Trim() == "Total")
                    {
                        lbl_SalaryBreakUp.Text = res.ToString();
                    }
                }




    string Bodyofletter = "";

                                    if (ComId.Value == "1")
                                    {
                                        Bodyofletter =
              "We are glad to inform you that the Company has decided to give you one step annual increment in your existing salary grade, " +
              GradeCode.Value + ", with effect from " +
              EffectDate + ". Your revised monthly gross salary will be taka " +
              res + ".00 (" + NumberToText(Convert.ToInt64(res)) + " only) in " + txtIncrementalStep.Text + ". The complete detail of your restructured salary is highlighted in Table-1 of this letter. " +
              EffectDate + ".";

                                    }
                                    else
                                    {
                                        Bodyofletter = @"The Management is pleased to inform you that your salary has been increased in your current grade on account of Annual Increment with effect from " +
                                    EffectDate + ". The details of your salary are as follows:";
                                    }

                                    txtBodyofletter.Text = Bodyofletter;
            }
            else
            {


                ////Mangement
                decimal res = 0;

                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {

                    Label lbl_SalaryBreakUp =
                          (Label)
                              KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                    Label lbl_Particulars =
                      (Label)
                          KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                    try
                    {




                        DataTable aDataTable =
             aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));
                        Decimal basicAmount = 0;
                        try
                        {
                            basicAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["BasicAmount"].ToString()), 0);
                        }
                        catch (Exception)
                        {


                        }


                        if (lbl_Particulars.Text.Trim() == "Basic Pay")
                        {
                            //txtIncrementalStep.Text=



                            lbl_SalaryBreakUp.Text = Math.Round(basicAmount, 0).ToString();



                        }

                        if (GradeCode.Value.Trim() == "Special" || GradeCode.Value.Trim() == "M-1" || GradeCode.Value.Trim() == "M-2A" || GradeCode.Value.Trim() == "M-2B" || GradeCode.Value.Trim() == "M-3A" || GradeCode.Value.Trim() == "M-3B" || GradeCode.Value.Trim() == "M-4" || GradeCode.Value.Trim() == "M-5")
                        {
                            decimal Medical = 0;
                            decimal HouseResnt = 0;
                            decimal Conveyance = 0;

                            if (lbl_Particulars.Text.Trim() == "House Rent")
                            {
                                HouseResnt = (Math.Round(basicAmount, 0) * 50) / 100;
                                lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                            }

                            if (lbl_Particulars.Text.Trim() == "Medical")
                            {
                                Medical = (Math.Round(basicAmount, 0) * 10) / 100;
                                lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                            }

                            if (lbl_Particulars.Text.Trim() == "Conveyance")
                            {
                             

                                  DataTable dtConveyance =
            aDAL.CheckConveyanceByMasterCode(lblEmployeeCode.Text.Trim());

                                if (dtConveyance.Rows.Count > 0)
                                {
                                    Conveyance = 0;
                                }
                                else
                                {

                                    Conveyance = 0;

                                }
                                lbl_SalaryBreakUp.Text = Conveyance.ToString();
                                
                            }


                            if (lbl_Particulars.Text.Trim() == "Washing")
                            {
                                lbl_SalaryBreakUp.Text = "0";
                                lbl_Particulars.Text = "";

                                lbl_Particulars.Visible = false;
                                lbl_SalaryBreakUp.Visible = false;


                            }
                            //basicAmount

                        }


                        if (GradeCode.Value.Trim() == "M-6A" || GradeCode.Value.Trim() == "M-6B" || GradeCode.Value.Trim() == "M-7" || GradeCode.Value.Trim() == "M-8" || GradeCode.Value.Trim() == "M-9")
                        {
                            decimal Medical = 0;
                            decimal HouseResnt = 0;
                            decimal Conveyance = 0;

                            if (lbl_Particulars.Text.Trim() == "House Rent")
                            {
                                HouseResnt = (Math.Round(basicAmount, 0) * 75) / 100;
                                lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                            }

                            if (lbl_Particulars.Text.Trim() == "Medical")
                            {
                                Medical = (Math.Round(basicAmount, 0) * 10) / 100;
                                lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                            }

                            if (lbl_Particulars.Text.Trim() == "Conveyance")
                            {
                                   DataTable dtConveyance =
            aDAL.CheckConveyanceByMasterCode(lblEmployeeCode.Text.Trim());

                                if (dtConveyance.Rows.Count > 0)
                                {
                                    Conveyance = 0;
                                }
                                else
                                {

                                    Conveyance = (Math.Round(basicAmount, 0)*5)/100;
                                }
                                lbl_SalaryBreakUp.Text = Math.Round(Conveyance, 0).ToString();


                            }

                            if (lbl_Particulars.Text.Trim() == "Washing")
                            {
                                lbl_SalaryBreakUp.Text = "0";
                                lbl_Particulars.Text = "";

                                lbl_Particulars.Visible = false;
                                lbl_SalaryBreakUp.Visible = false;


                            }


                        }



                        if (GradeCode.Value.Trim() == "S-5" || GradeCode.Value.Trim() == "S-4" ||
                                         GradeCode.Value.Trim() == "S-3" || GradeCode.Value.Trim() == "S-2" ||
                                         GradeCode.Value.Trim() == "S-1A" ||
                                         GradeCode.Value.Trim() == "S-1B" ||
                                         GradeCode.Value.Trim() == "SS-5" ||
                                         GradeCode.Value.Trim() == "S-1A" ||
                                         GradeCode.Value.Trim() == "SS-4" ||
                                         GradeCode.Value.Trim() == "S-1A" ||
                                         GradeCode.Value.Trim() == "SS-3" ||
                                         GradeCode.Value.Trim() == "SS-2" ||
                                         GradeCode.Value.Trim() == "S-1A" ||
                                         GradeCode.Value.Trim() == "SS-1A" ||
                                         GradeCode.Value.Trim() == "SS-1B"

                                          ||
                                         GradeCode.Value.Trim() == "S-1" ||
                                         GradeCode.Value.Trim() == "SS-1" ||
                                         GradeCode.Value.Trim() == "SS-1B" ||
                                         GradeCode.Value.Trim() == "M-3" ||
                                         GradeCode.Value.Trim() == "M-2" ||
                                         GradeCode.Value.Trim() == "M-6" ||
                                         GradeCode.Value.Trim() == "M-0" ||
                                         GradeCode.Value.Trim() == "S-0")
                        {
                            decimal Medical = 0;
                            decimal HouseResnt = 0;
                            decimal Conveyance = 0;

                            if (lbl_Particulars.Text.Trim() == "House Rent")
                            {
                                HouseResnt = (Math.Round(basicAmount, 0) * 63) / 100;
                                lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                            }

                            if (lbl_Particulars.Text.Trim() == "Medical")
                            {
                                Medical = 0;
                                lbl_SalaryBreakUp.Text = Medical.ToString();
                               
                            }

                            if (lbl_Particulars.Text.Trim() == "Conveyance")
                            {

                                   DataTable dtConveyance =
            aDAL.CheckConveyanceByMasterCode(lblEmployeeCode.Text.Trim());

                                if (dtConveyance.Rows.Count > 0)
                                {
                                    Conveyance = 0;
                                }
                                else
                                {
                                    Conveyance = 0;
                                }
                                lbl_SalaryBreakUp.Text = Conveyance.ToString();
                               

                            }

                            if (lbl_Particulars.Text.Trim() == "Washing")
                            {
                                lbl_SalaryBreakUp.Text = "0";

                                lbl_Particulars.Visible = true;
                                lbl_SalaryBreakUp.Visible = true;

                                
                            }
                        }

                    }

                    catch (Exception)
                    {

                        //throw;
                    }



                    decimal res2 = Convert.ToDecimal(lbl_SalaryBreakUp.Text);

                    res += Math.Round(res2, 0);


                }

                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    Label lbl_Particulars =
                        (Label)
                            KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                    Label lbl_SalaryBreakUp =
                       (Label)
                           KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");


                    if (lbl_Particulars.Text.Trim() == "Total")
                    {


                        DataTable aDataTable =
           aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));

                        Decimal GrossAmount = 0;
                        try
                        {
                            GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
                        }
                        catch (Exception)
                        {


                        }

                        lbl_Particulars.Font.Bold = true;
                        lbl_SalaryBreakUp.Text = GrossAmount.ToString();
              



                    }

                    if (lbl_Particulars.Text.Trim() == "Medical")
                    {



                        DataTable aDataTable =
             aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));

                        Decimal GrossAmount = 0;
                        try
                        {
                            GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
                        }
                        catch (Exception)
                        {


                        }
                        if (GrossAmount != res)
                        {

                            if (lbl_Particulars.Text.Trim() == "Medical")
                            {
                                decimal newREs = 0;

                                newREs = GrossAmount - res;

                                decimal medi = 0;
                                try
                                {
                                    medi = Convert.ToDecimal(lbl_SalaryBreakUp.Text.Trim());
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                                decimal mainres = medi + newREs;


                                lbl_SalaryBreakUp.Text = mainres.ToString();
                            }
                        }
                    }
                }
                DataTable aDataTable2 =
  aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value), Convert.ToInt32(HFIncrementalStepId.Value));
                Decimal GrossAmount2 = 0;
                try
                {
                    GrossAmount2 = Math.Round(Convert.ToDecimal(aDataTable2.Rows[0]["GrossAmount"].ToString()), 0);
                }
                catch (Exception)
                {


                }

               
    string Bodyofletter = "";

                                    if (ComId.Value == "1")
                                    {
                                        //double number = Convert.ToDouble(GrossAmount2);
                                        //string formattedNumber = number.ToString("#,##0");
                                        //Console.WriteLine(formattedNumber);

                                        Bodyofletter =
              "We are glad to inform you that the Company has decided to give you one step annual increment in your existing salary grade, " +
              GradeCode.Value + ", with effect from " +
              EffectDate + ". Your revised monthly gross salary will be taka " +
              GrossAmount2 + ".00 (" + NumberToText(Convert.ToInt64(GrossAmount2)) + " only) in " + txtIncrementalStep.Text.ToLower() + ". A breakdown of your new salary is given below: ";
  

                                    }
                                    else
                                    {
                                        Bodyofletter = @"The Management is pleased to inform you that your salary has been increased in your current grade on account of Annual Increment with effect from " +
                                    EffectDate + ". The details of your salary are as follows:";
                                    }

                                    txtBodyofletter.Text = Bodyofletter;
            }




            string rptTypeIdMul = "";
            DataTable dtref = aEmployeeInfoListReportDAL.GetRefEmpInfoDAL(EmpIddd);
            if (dtref.Rows.Count > 0)
            {
                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");

                DataRow dataRow = null;
                dataRow = aDataTable.NewRow();
                dataRow["EmpInfoId"] = "0";

                aDataTable.Rows.Add(dataRow);
                ReportingEmpData(EmpIddd, dtref);
                string myId = "";
                for (int i = 0; i < dtref.Rows.Count; i++)
                {
                    myId += dtref.Rows[i]["ReferenceID"].ToString().Trim() + ",";
                }


                myId = myId.Trim().TrimEnd(',');
                rptTypeIdMul = EmpIddd + "," + myId.Trim();
            }
            DataTable dtPartPrevious = new DataTable();
            if (rptTypeIdMul == "")
            {
                dtPartPrevious = aDAL.LoadMemoPrintIncrementDetailsPreviousByMId(EmpIddd);
            }
            else
            {
                dtPartPrevious = aDAL.LoadMemoPrintIncrementDetailsPreviousByMId(rptTypeIdMul);

            }


            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {

                Label lbl_SalaryBreakUpPre =
                    (Label)
                        KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUpPre");
                Label lbl_Particulars =
                    (Label)
                        KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                for (int j = 0; j < dtPartPrevious.Rows.Count; j++)
                {
                    if (dtPartPrevious.Rows[j]["PName"].ToString().Trim() == lbl_Particulars.Text.Trim())
                    {
                        lbl_SalaryBreakUpPre.Text = dtPartPrevious.Rows[j]["PAmount"].ToString().Trim();
                    }
                    if (lbl_Particulars.Text.Trim() == "")
                    {
                        KeyResponGridView.Rows[i].Visible = false;
                    }
                }
            }

        }
    }

    public static string NumberToText(long number)
    {
        StringBuilder wordNumber = new StringBuilder();

        string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
        string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", 
                                       "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        if (number == 0) { return "Zero"; }
        if (number < 0)
        {
            wordNumber.Append("Negative ");
            number = -number;
        }

        long[] groupedNumber = new long[] { 0, 0, 0, 0 };
        int groupIndex = 0;

        while (number > 0)
        {
            groupedNumber[groupIndex++] = number % 1000;
            number /= 1000;
        }

        for (int i = 3; i >= 0; i--)
        {
            long group = groupedNumber[i];

            if (group >= 100)
            {
                wordNumber.Append(ones[group / 100 - 1] + " Hundred ");
                group %= 100;

                if (group == 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }

            if (group >= 20)
            {
                if ((group % 10) != 0)
                    wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");
                else
                    wordNumber.Append(tens[group / 10 - 2] + " ");
            }
            else if (group > 0)
                wordNumber.Append(ones[group - 1] + " ");

            if (group != 0 && i > 0)
                wordNumber.Append(powers[i - 1]);
            if (i == 1 && groupedNumber[i] != 0 && (groupedNumber[i - 1] > 0 || groupedNumber[i - 2] > 0 || groupedNumber[i - 3] > 0))
            {
                wordNumber.Append("and ");
            }
        }

        return wordNumber.ToString().Trim();
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Validation2())
        {

                string MasterIncrementID = "";
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                var chkBoxRows = (CheckBox) loadGridView.Rows[i].Cells[0].FindControl("chkSelect");




                if (chkBoxRows.Checked)
                {
                    HiddenField HFIncrementId = (HiddenField) loadGridView.Rows[i].FindControl("HFIncrementId");

                    string res2 = HFIncrementId.Value;
                    MasterIncrementID += res2+",";
                }

            }
            string mmm = MasterIncrementID.TrimEnd(',');
            PopUp((mmm),"PDF");
        }
    }

    private void PopUp(string EmpInfoId, string PrintType)
    {
        string url = "../Report_UI/MemoPrintIncrementReportViwer.aspx?rptType=" + EmpInfoId + "&rt=MemoPIAll&PrintType=" + PrintType;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void btnDoc_OnClick(object sender, EventArgs e)
    {
        if (Validation2())
        {

            string MasterIncrementID = "";
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");




                if (chkBoxRows.Checked)
                {
                    HiddenField HFIncrementId = (HiddenField)loadGridView.Rows[i].FindControl("HFIncrementId");

                    string res2 = HFIncrementId.Value;
                    MasterIncrementID += res2 + ",";
                }

            }
            string mmm = MasterIncrementID.TrimEnd(',');
            PopUp((mmm), "DOC");
        }
    }
}