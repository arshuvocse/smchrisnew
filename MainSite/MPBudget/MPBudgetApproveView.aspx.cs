using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.InternalCls;
using DAL.MPBudget;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using System.Net.Mail;
using System.Net;
using DAL.MasterSetup_DAL;

public partial class MPBudget_MPBudgetEntry : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private MPBudgetCommonDAL _mpBudgetCommonDal = new MPBudgetCommonDAL();
    private int mid = 0;
    private string _userId;
    private int _EMpId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }

        try
        {
            _EMpId = Convert.ToInt32(Session["EmpInfoId"].ToString());
        }
        catch (Exception)
        {
            _EMpId = 0;
            //throw;
        }
        if (!IsPostBack)
        {
            LoadInitialDDL();
            //SetInitialRow();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                if (mid > 0)
                {
                    hdpk.Value = mid.ToString();
                    //hddpk.Value = Request.QueryString["mdid"];
                    LoadEditMode();
                }
                DataTable AppLogComm = _mpBudgetCommonDal.GetAppLogCommById(Convert.ToInt32(mid));

                if (AppLogComm.Rows.Count > 0)
                {
                    AppLogCommentGridView.DataSource = AppLogComm;
                    AppLogCommentGridView.DataBind();
                }
            }
            DataLoad();
            RadioTextValue();
            //LoadEditInfo();
        }
    }
    public void DataLoad()
    {
       // ClsApprovalAction approvalAction = new ClsApprovalAction();

       // string userName = Session["UserId"].ToString();

       //approvalAction.LoadActionControlByUser(jobreqRadioButtonList, Session["ApprovalPage"].ToString(), userName);
       // Session["ApprovalPage"] = null;
       //RadItemRemove();
       // btn_Save.Text = "Save";

    }
    private void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        DataTable dtdata = null;
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
           

            dtdata = _mpBudgetCommonDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                  " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' and  CompanyId='" + Session["CompanyId"].ToString() + "' ORDER BY Serial ASC ");
        
        }
        else
        {
            dtdata = _mpBudgetCommonDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = aEmployeeRequsitionDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["EmpInfoId"].ToString() != Session["ForEmpInfoId"].ToString())



        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

        }
        else
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();
        if (aDataTable.Rows.Count>0)
        {
            actionRadioButtonList.Items[0].Selected=true;
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
            btn_Save.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            btn_Save.Visible = true;
            Button1.Visible = false;
        }
    }

    //public void RadItemRemove()
    //{
    //    string[] id = new string[5];
    //    int count = 0;
    //    for (int i = 0; i < jobreqRadioButtonList.Items.Count; i++)
    //    {

    //        if (jobreqRadioButtonList.Items[i].Enabled == false)
    //        {
    //            id[count] = (jobreqRadioButtonList.Items[i].Value);
    //            count++;

    //        }
    //    }
    //    foreach (string a in id)
    //    {
    //        for (int i = 0; i < jobreqRadioButtonList.Items.Count; i++)
    //        {

    //            if (a!=null)
    //            {
    //                if (jobreqRadioButtonList.Items[i].Value ==  a.ToString())
    //                {

    //                    jobreqRadioButtonList.Items.RemoveAt(i);
    //                }    
    //            }
                
    //        }
    //    }
    //}

    private void LoadEditMode()
    {
        using (DataTable dt = _mpBudgetCommonDal.GetMPBudgetById(mid))
        {
            if (dt.Rows.Count > 0)
            {
                entryempinfoIdHiddenField.Value = dt.Rows[0]["EmpInfoId"].ToString();
                actionstatusHiddenField.Value = dt.Rows[0].Field<String>("ActionStatus").ToString();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var CompanyId = dt.Rows[i]["CompanyId"].ToString().Trim();
                    ddlCompany.SelectedValue = CompanyId;
                    ddlCompany_SelectedIndexChanged(null, null);
                    ddlDepartment.SelectedValue = dt.Rows[i]["DepartmentId"].ToString().Trim();
                    ddlDepartment_OnSelectedIndexChanged(null, null);
                    ddlFinYear.SelectedValue = dt.Rows[i]["FinancialYearId"].ToString().Trim();
                    string mdid = Request.QueryString["mdid"];
                    using (DataTable dtDetails = _mpBudgetCommonDal.GetMPBudgetDetailsBymid(Convert.ToInt32(mid)))
                    {
                        ViewState["MPTable"] = dtDetails;
                        gv_MP.DataSource = dtDetails;
                        gv_MP.DataBind();
                    }

                    lblCompany.Text = ddlCompany.SelectedItem.Text;
                    lblDept.Text = ddlDepartment.SelectedItem.Text;
                    lblFinancialYear.Text = ddlFinYear.SelectedItem.Text;
                   


                }
            }
        }
    }

    private void LoadEditInfo()
    {
       
        int rowID = 0;

        if (ViewState["MPTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["MPTable"];

            ddlEmpCategoryEx.SelectedValue = dt.Rows[rowID]["EmpCategoryId"].ToString();
            ddlEmpCategoryEx_OnSelectedIndexChanged(null, null);

            radEmpType.SelectedValue = dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim();

            //radEmpType.Items.FindByValue(dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim()).Selected = true;
            radEmpType_OnSelectedIndexChanged(null, null);
            if (dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim() == "3")
            {
                ddlProjectName.SelectedValue = dt.Rows[rowID]["ProjectId"].ToString();
                lblProjectName.Text = ddlProjectName.SelectedItem.Text;
            }

            lblEmpType.Text = radEmpType.SelectedItem.Text;
            txt_Designation.Text = dt.Rows[rowID]["Designation"].ToString();
            ddlGradeEx.SelectedValue = dt.Rows[rowID]["SalaryGradeId"].ToString();
            ddlQuarter.SelectedValue = dt.Rows[rowID]["QuarterId"].ToString();

            txt_EmployeeRequisition.Text = dt.Rows[rowID]["EmployeeRequisition"].ToString();
            txt_ReqApproxSalary.Text = dt.Rows[rowID]["ReqApproxSalary"].ToString();
            lbl_ReqTotalSalary.Text = dt.Rows[rowID]["ReqTotalSalary"].ToString();
            txtRemarks.Text = dt.Rows[rowID]["DtlRemarks"].ToString();

            ddlGradeEx_OnSelectedIndexChanged(null, null);

            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["MPTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_MP.DataSource = dt;
                gv_MP.DataBind();
            }
            else
            {
                ViewState["MPTable"] = null;
                //Re bind the GridView for the updated data  
                gv_MP.DataSource = null;
                gv_MP.DataBind();
            }
        }
        //Set Previous Data on Postbacks  

        lblDesignation.Text = txt_Designation.Text;
        lblEmployeeRequisition.Text = txt_EmployeeRequisition.Text;
        lblNewDesignation.Text = newDesignDropDownList.SelectedItem.Text;
        lblSAlarRngFrom.Text = txt_ReqApproxSalary.Text;

        lblSalaryRangeTo.Text = lbl_ReqTotalSalary.Text;

        lblQuarter.Text = ddlQuarter.SelectedItem.Text;

        lblJobSummary.Text = txtRemarks.Text;

        SetPreviousData();
    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }

        using (DataTable dt = _commonDataLoad.GetEmpCategoryDDL())
        {
            ddlEmpCategoryEx.DataSource = dt;
            ddlEmpCategoryEx.DataValueField = "Value";
            ddlEmpCategoryEx.DataTextField = "TextField";
            ddlEmpCategoryEx.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetEmpType())
        {
            radEmpType.DataSource = dt;
            radEmpType.DataValueField = "Value";
            radEmpType.DataTextField = "TextField";
            radEmpType.DataBind();
        }
        _commonDataLoad.LoadDesignaation(newDesignDropDownList);
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MPBudgetList.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLDepartmentByCompanyId2(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLFinYearByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlFinYear.DataSource = dt;
                ddlFinYear.DataValueField = "Value";
                ddlFinYear.DataTextField = "TextField";
                ddlFinYear.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetQuarterByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlQuarter.DataSource = dt;
                ddlQuarter.DataValueField = "Value";
                ddlQuarter.DataTextField = "TextField";
                ddlQuarter.DataBind();
            }

        }
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (valiHRR())
            {
                
           
            
            int mainid
                = Convert.ToInt32(hdpk.Value);

            string ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = _mpBudgetCommonDal.UpdateContractural(ActionStatus,mainid);
            if (status)
            {
                int commentid = _mpBudgetCommonDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    txtComment.Text);
                if (ActionStatus == "Verified")
                {
                    DataTable dtempdata = _mpBudgetCommonDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.MPBudgetMasterId = mainid;
                        appLogDao.Comments = txtComment.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = _mpBudgetCommonDal.SavAppLog(appLogDao);
                    _mpBudgetCommonDal.UpdateJobReqStatus2(ActionStatus, mainid);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Manpower Budget Approval ", @"  <br/> Dear Sir, <br/>
Manpower Budget is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (ActionStatus == "Approved")
                {
                    int empid = 0;
                    DataTable dtempdata =  _mpBudgetCommonDal.GetHRAdminEmployeeAppId(" WHERE URL='" +
                                                                           Session["AppPage"].ToString() +
                                                                           "' AND Serial='1'" +
                                                                       "   AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    if (dtempdata.Rows.Count > 0)
                    {
                        empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    }
                    MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.MPBudgetMasterId = mainid;
                        appLogDao.Comments = txtComment.Text;
                        appLogDao.CommentsId = commentid;
                    };
                    ActionStatus = "Verified";
                    _mpBudgetCommonDal.UpdateJobReqStatus2(ActionStatus, mainid);
                    int id = _mpBudgetCommonDal.SavAppLog(appLogDao);
                    SenMailForApprved(appLogDao.ForEmpInfoId, " Manpower Budget Approval ", @"  <br/> Dear Sir, <br/>
Manpower Budget is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");

                }
                else if (ActionStatus == "Review")
                {
                    DataTable dtempdata = _mpBudgetCommonDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hdpk.Value);
                    DataTable dtempdata2 = _mpBudgetCommonDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hdpk.Value);


                    if (dtempdata2.Rows.Count > 0)
                    {
                        MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.MPBudgetMasterId = mainid;
                            appLogDao.Comments = txtComment.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        _mpBudgetCommonDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = _mpBudgetCommonDal.SavAppLog(appLogDao);
                        _mpBudgetCommonDal.UpdateJobReqStatus2(ActionStatus, mainid);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                    DataTable dtdata = _mpBudgetCommonDal.GetDataReviewEntryBy(
                      hdpk.Value, Session["UserId"].ToString(), "Review");
                    if (dtdata.Rows.Count > 0)
                    {
                        Session["Status"] = "";
                        Session["Status"] = "Edit";
                        Session["MPBudgetEdit"] = mainid.ToString();
                        Response.Redirect("MPBudgetEntry.aspx?id2=" + mainid.ToString());
                    }

                }

                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Data Saved Successfully...');window.location ='MPBudgetListApproval.aspx';",
                           true);
            }
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow("Please Choose an action for approval");
        }
    }

    private bool valiHRR()
    {

        DataTable dtempdata = _mpBudgetCommonDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'" +
                                                                     "  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
   
        if (dtempdata.Rows.Count == 0)
        {
            AlertMessageBoxShow("HR Approval Information is not exist!! ");
            return false;
        }


        tblEmpGeneralInfo bm;
        using (var db = new HRIS_SMCEntities())
        {
            bm = (from m in db.tblEmpGeneralInfoes where m.EmpInfoId == _EMpId select m).FirstOrDefault();

            try
            {
                int empid = (int)bm.ReportingEmpId;

                if (empid == null)
                {
                    AlertMessageBoxShow("Supervisor not Found");
                    return false;
                }
            }
            catch (Exception)
            {
                AlertMessageBoxShow("Supervisor not Found");
                return false;
                //throw;
            }
        }


        return true;
    }


    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }



    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("MPBudgetDetailsId", typeof(string)));
        dt.Columns.Add(new DataColumn("CompanyId", typeof(string)));
        dt.Columns.Add(new DataColumn("CompanyName", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentId", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("FinancialYearId", typeof(string)));
        dt.Columns.Add(new DataColumn("FinancialYearDesc", typeof(string)));
        dt.Columns.Add(new DataColumn("DesignationId", typeof(string)));
        dt.Columns.Add(new DataColumn("Designation", typeof(string)));
        dt.Columns.Add(new DataColumn("ExistingEmployee", typeof(string)));
        dt.Columns.Add(new DataColumn("ExistingSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryGradeId", typeof(string)));
        dt.Columns.Add(new DataColumn("GradeName", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryStepId", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryStepName", typeof(string)));
        dt.Columns.Add(new DataColumn("GExistingEmployee", typeof(string)));
        dt.Columns.Add(new DataColumn("GExistingSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("EmployeeRequisition", typeof(string)));
        dt.Columns.Add(new DataColumn("ReqApproxSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("ReqTotalSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("QuarterId", typeof(string)));
        dt.Columns.Add(new DataColumn("QuarterName", typeof(string)));

        dr = dt.NewRow();
        dr["MPBudgetDetailsId"] = "0";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["MPTable"] = dt;

        //Bind the Gridview   
        gv_MP.DataSource = dt;
        gv_MP.DataBind();

    }
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        var cid = ddlCompany.SelectedValue;
        var quartar = ddlQuarter.SelectedValue;
        var dept = ddlDepartment.SelectedValue;
        var finYear = ddlFinYear.SelectedValue;

        var grade = String.Empty;
        var desig = String.Empty;
        var step = String.Empty;

        var FilterType = string.Empty;

        ////check for new designation
        //if (chk_NewDesignation.Checked)
        //{////New
        //    FilterType = "new";
        //    grade = ddlGradeNew.SelectedValue;
        //    desig = ddlDesignationNew.SelectedValue;
        //    step = ddlStepNew.SelectedValue;
        //}
        //else////Existing
        //{
        //    FilterType = "ex";
        //    grade = ddlGradeEx.SelectedValue;
        //    desig = "";////TODO ddlDesignationEx.SelectedValue;
        //    step = ddlStepEx.SelectedValue;
        //}
        //FilterType = "ex";
        grade = ddlGradeEx.SelectedValue;
        desig = txt_Designation.Text;
        step = ddlStepEx.SelectedValue;

        #region Validation to add grid

        if (cid == "-1")
        {
            AlertMessageBoxShow("Company required...");
            return;
        }
        if (grade == "-1" || grade == "")
        {
            AlertMessageBoxShow("Grade required...");
            return;
        }
        if (desig == "-1" || desig == "")
        {
            AlertMessageBoxShow("Designation required...");
            return;
        }
        //if (step == "-1" || step == "")
        //{
        //    AlertMessageBoxShow("Step required...");
        //    return;
        //}
        if (quartar == "-1" || quartar == "")
        {
            AlertMessageBoxShow("Quarter required...");
            return;
        }
        if (dept == "-1" || dept == "")
        {
            AlertMessageBoxShow("Department required...");
            return;
        }
        if (finYear == "-1" || finYear == "")
        {
            AlertMessageBoxShow("Financial Year required...");
            return;
        }
        if (string.IsNullOrEmpty(txt_EmployeeRequisition.Text))
        {
            AlertMessageBoxShow("Employee Requisition required...");
            return;
        }
        if (string.IsNullOrEmpty(txt_ReqApproxSalary.Text))
        {
            AlertMessageBoxShow("Salary Range From required...");
            return;
        }
        if (string.IsNullOrEmpty(lbl_ReqTotalSalary.Text))
        {
            AlertMessageBoxShow("Salary Range To required...");
            return;
        }
        int r = 0;
        for ( r = 0; r < radEmpType.Items.Count; r++)
        {
            if (radEmpType.Items[r].Selected)
            {
                break;
            }
        }
        if (r==radEmpType.Items.Count)
        {
            AlertMessageBoxShow("Select Employee Type...");
            return;
        }

        if (ViewState["MPTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["MPTable"];
            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                if ((dtCurrentTable.Rows[i]["SalaryGradeId"].ToString() == grade)
                    && (dtCurrentTable.Rows[i]["DesignationId"].ToString() == desig)
                    && (dtCurrentTable.Rows[i]["SalaryStepId"].ToString() == step)
                    && (dtCurrentTable.Rows[i]["QuarterId"].ToString() == quartar))
                {
                    AlertMessageBoxShow("This Grade, Designation, Step, Quarter already exists in below table...");
                    return;
                }
            }
        }
        #endregion

        AddNewRowToGrid(FilterType, grade, desig, step);

        #region Form Reset
        //ddlGradeNew.SelectedValue = "-1";
        //ddlDesignationNew.SelectedValue = "-1";
        //ddlStepNew.SelectedValue = "-1";

        //chk_NewDesignation.Checked = false;

        //ddlGradeEx.SelectedValue = "-1";
        ////TODO ddlDesignationEx.SelectedValue = "-1";
        txt_Designation.Text = string.Empty;
        //ddlStepEx.SelectedValue = "-1";

        //lblExGradeTotalEmp.Text = String.Empty;
        ////TODO lblExGradeTotalSal.Text = String.Empty;
        ///lblExGradeMaxSal.Text = String.Empty;
        ///lblExGradeMinSal.Text = String.Empty;

        txt_EmployeeRequisition.Text = String.Empty;
        txt_ReqApproxSalary.Text = String.Empty;
        lbl_ReqTotalSalary.Text = String.Empty;
        txtRemarks.Text = String.Empty;

        //ddlQuarter.DataSource = null;
        //ddlQuarter.DataBind();
        #endregion
    }
    private void AddNewRowToGrid(string FilterType, string grade, string desig, string step)
    {
        if (ViewState["MPTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["MPTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["MPBudgetDetailsId"] = "0";
                drCurrentRow["MPBudgetDetailsId"] = "0";
                drCurrentRow["Designation"] = txt_Designation.Text;
                drCurrentRow["EmpCategoryName"] = ddlEmpCategoryEx.SelectedItem.Text;
                drCurrentRow["EmpCategoryId"] = ddlEmpCategoryEx.SelectedValue;
                drCurrentRow["SalaryGradeId"] = ddlGradeEx.SelectedValue;
                drCurrentRow["GradeName"] = ddlGradeEx.SelectedItem.Text;
                drCurrentRow["EmployeeRequisition"] = txt_EmployeeRequisition.Text.Trim();
                drCurrentRow["ReqApproxSalary"] = txt_ReqApproxSalary.Text.Trim();
                drCurrentRow["ReqTotalSalary"] = lbl_ReqTotalSalary.Text;
                drCurrentRow["QuarterId"] = ddlQuarter.SelectedValue;
                drCurrentRow["QuarterName"] = ddlQuarter.SelectedItem.Text;
                drCurrentRow["DtlRemarks"] = txtRemarks.Text;
                drCurrentRow["EmployeeType"] = radEmpType.SelectedItem.Text;
                drCurrentRow["EmployeeTypeId"] = radEmpType.SelectedValue.Trim();
                if (radEmpType.SelectedValue.Trim() == "3")
                {
                    drCurrentRow["Project"] = ddlProjectName.SelectedItem.Text;
                    drCurrentRow["ProjectId"] = ddlProjectName.SelectedValue;
                }

                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["MPTable"] = dtCurrentTable;
                //Rebind the Grid with the current data to reflect changes   
                gv_MP.DataSource = dtCurrentTable;
                gv_MP.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("MPBudgetDetailsId", typeof(string)));
            dt.Columns.Add(new DataColumn("FilterType", typeof(string)));
            dt.Columns.Add(new DataColumn("DesignationId", typeof(string)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpCategoryId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpCategoryName", typeof(string)));
            dt.Columns.Add(new DataColumn("SalaryGradeId", typeof(string)));
            dt.Columns.Add(new DataColumn("GradeName", typeof(string)));
            dt.Columns.Add(new DataColumn("SalaryStepId", typeof(string)));
            dt.Columns.Add(new DataColumn("SalaryStepName", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeRequisition", typeof(string)));
            dt.Columns.Add(new DataColumn("ReqApproxSalary", typeof(string)));
            dt.Columns.Add(new DataColumn("ReqTotalSalary", typeof(string)));
            dt.Columns.Add(new DataColumn("QuarterId", typeof(string)));
            dt.Columns.Add(new DataColumn("QuarterName", typeof(string)));
            dt.Columns.Add(new DataColumn("DtlRemarks", typeof(string)));

            dt.Columns.Add(new DataColumn("EmployeeType", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeTypeId", typeof(string)));
            dt.Columns.Add(new DataColumn("Project", typeof(string)));
            dt.Columns.Add(new DataColumn("ProjectId", typeof(string)));

            dr = dt.NewRow();
            dr["MPBudgetDetailsId"] = "0";
            dr["Designation"] = txt_Designation.Text;
            dr["EmpCategoryName"] = ddlEmpCategoryEx.SelectedItem.Text;
            dr["EmpCategoryId"] = ddlEmpCategoryEx.SelectedValue;
            dr["SalaryGradeId"] = ddlGradeEx.SelectedValue;
            dr["GradeName"] = ddlGradeEx.SelectedItem.Text;
            dr["EmployeeRequisition"] = txt_EmployeeRequisition.Text.Trim();
            dr["ReqApproxSalary"] = txt_ReqApproxSalary.Text.Trim();
            dr["ReqTotalSalary"] = lbl_ReqTotalSalary.Text;
            dr["QuarterId"] = ddlQuarter.SelectedValue;
            dr["QuarterName"] = ddlQuarter.SelectedItem.Text;
            dr["DtlRemarks"] = txtRemarks.Text;
            dr["EmployeeType"] = radEmpType.SelectedItem.Text;
            dr["EmployeeTypeId"] = radEmpType.SelectedValue.Trim();
            if (radEmpType.SelectedValue.Trim() == "3")
            {
                dr["Project"] = ddlProjectName.SelectedItem.Text;
                dr["ProjectId"] = ddlProjectName.SelectedValue;
            }




            //dr["FilterType"] = FilterType;
            //dr["DesignationId"] = 0;
            ////TODO dr["Designation"] =  FilterType == "new" ? ddlDesignationNew.SelectedItem.Text : ddlDesignationEx.SelectedItem.Text;

            //dr["GradeName"] = grade;
            //dr["GradeName"] =  FilterType == "new" ? ddlGradeNew.SelectedItem.Text : ddlGradeEx.SelectedItem.Text;

            //dr["SalaryStepId"] = step;
            //dr["SalaryStepName"]  =  FilterType == "new" ? ddlStepNew.SelectedItem.Text : ddlStepEx.SelectedItem.Text;
            //dr["SalaryStepName"]  = ddlStepEx.SelectedItem.Text;


            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["MPTable"] = dt;
            //Bind the Gridview   
            gv_MP.DataSource = dt;
            gv_MP.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["MPTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["MPTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hdpkd = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdpkd");
                    //HiddenField hdFilterType = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdFilterType");
                    //HiddenField hdDesignation = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdDesignation");
                    Label lblDesignation = (Label)gv_MP.Rows[rowIndex].FindControl("lblDesignation");
                    HiddenField hdEmpCategoryId = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdEmpCategoryId");
                    Label lblEmpCategoryName = (Label)gv_MP.Rows[rowIndex].FindControl("lblEmpCategoryName");

                    HiddenField hdSalaryGrade = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdSalaryGrade");
                    Label lblSalaryGrade = (Label)gv_MP.Rows[rowIndex].FindControl("lblSalaryGrade");
                    //HiddenField hdSalaryStep = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdSalaryStep");
                    //Label lblSalaryStep = (Label)gv_MP.Rows[rowIndex].FindControl("lblSalaryStep");
                    Label lblEmpReq = (Label)gv_MP.Rows[rowIndex].FindControl("lblEmployeeReq");
                    Label lblReqSalPerEmp = (Label)gv_MP.Rows[rowIndex].FindControl("lblReqSalPerEmp");
                    Label lblReqTotalSalary = (Label)gv_MP.Rows[rowIndex].FindControl("lblReqTotalSalary");
                    HiddenField hdQuarter = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdQuarter");
                    Label lblQuarter = (Label)gv_MP.Rows[rowIndex].FindControl("lblQuarter");
                    TextBox txtDtlRemarks = (TextBox)gv_MP.Rows[rowIndex].FindControl("txtDtlRemarks");

                    HiddenField hdProject = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdProject");
                    Label lblProject = (Label)gv_MP.Rows[rowIndex].FindControl("lblProject");

                    HiddenField hdEmployeeType = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdEmployeeType");
                    Label lblEmployeeType = (Label)gv_MP.Rows[rowIndex].FindControl("lblEmployeeType");

                    if (i < dt.Rows.Count - 1)
                    {
                        hdpkd.Value = dt.Rows[i]["MPBudgetDetailsId"].ToString();
                        lblDesignation.Text = dt.Rows[i]["Designation"].ToString();
                        hdEmpCategoryId.Value = dt.Rows[i]["EmpCategoryId"].ToString();
                        lblEmpCategoryName.Text = dt.Rows[i]["EmpCategoryName"].ToString();
                        hdSalaryGrade.Value = dt.Rows[i]["SalaryGradeId"].ToString();
                        lblSalaryGrade.Text = dt.Rows[i]["GradeName"].ToString();
                        lblEmpReq.Text = dt.Rows[i]["EmployeeRequisition"].ToString();
                        lblReqSalPerEmp.Text = dt.Rows[i]["ReqApproxSalary"].ToString();
                        lblReqTotalSalary.Text = dt.Rows[i]["ReqTotalSalary"].ToString();
                        hdQuarter.Value = dt.Rows[i]["QuarterId"].ToString();
                        lblQuarter.Text = dt.Rows[i]["QuarterName"].ToString();
                        txtDtlRemarks.Text = dt.Rows[i]["DtlRemarks"].ToString();

                        hdProject.Value = dt.Rows[i]["ProjectId"].ToString();
                        lblProject.Text = dt.Rows[i]["Project"].ToString();

                        hdEmployeeType.Value = dt.Rows[i]["EmployeeTypeId"].ToString();
                        lblEmployeeType.Text = dt.Rows[i]["EmployeeType"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }
    protected void lb_Remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        //HiddenField hdpkd = (HiddenField)gv_MP.Rows[rowID].FindControl("hdpkd");
        //int pkd = Int32.Parse(hdpkd.Value);

        //var db = new HRIS_SMCEntities();
        //tblInterviewBoardSetupDetail IVDetails = (from emd in db.tblInterviewBoardSetupDetails where emd.BoardDetailsId == pkd select emd).FirstOrDefault();
        //IVDetails.IsActive = false;
        ////db.tblInterviewBoardSetupDetails.Add(IVDetails);
        //db.SaveChanges();


        if (ViewState["MPTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["MPTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["MPTable"] = dt;

                //Re bind the GridView for the updated data  
                gv_MP.DataSource = dt;
                gv_MP.DataBind();
            }
            else
            {
                ViewState["MPTable"] = null;

                //Re bind the GridView for the updated data  
                gv_MP.DataSource = null;
                gv_MP.DataBind();
            }


        }

        //Set Previous Data on Postbacks  
        SetPreviousData();
    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        if (ViewState["MPTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["MPTable"];

            ddlEmpCategoryEx.SelectedValue = dt.Rows[rowID]["EmpCategoryId"].ToString();
            ddlEmpCategoryEx_OnSelectedIndexChanged(null, null);

            radEmpType.SelectedValue = dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim();
            //radEmpType.Items.FindByValue(dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim()).Selected = true;
            radEmpType_OnSelectedIndexChanged(null, null);
            if (dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim() == "3")
            {
                ddlProjectName.SelectedValue = dt.Rows[rowID]["ProjectId"].ToString();
            }


            txt_Designation.Text = dt.Rows[rowID]["Designation"].ToString();
            ddlGradeEx.SelectedValue = dt.Rows[rowID]["SalaryGradeId"].ToString();
            ddlQuarter.SelectedValue = dt.Rows[rowID]["QuarterId"].ToString();

            txt_EmployeeRequisition.Text = dt.Rows[rowID]["EmployeeRequisition"].ToString();
            txt_ReqApproxSalary.Text = dt.Rows[rowID]["ReqApproxSalary"].ToString();
            lbl_ReqTotalSalary.Text = dt.Rows[rowID]["ReqTotalSalary"].ToString();
            txtRemarks.Text = dt.Rows[rowID]["DtlRemarks"].ToString();

            ddlGradeEx_OnSelectedIndexChanged(null, null);

            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["MPTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_MP.DataSource = dt;
                gv_MP.DataBind();
            }
            else
            {
                ViewState["MPTable"] = null;
                //Re bind the GridView for the updated data  
                gv_MP.DataSource = null;
                gv_MP.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData();
    }

    //private void ResetRowID(DataTable dt)
    //{
    //    int rowNumber = 1;
    //    if (dt.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            row[0] = rowNumber;
    //            rowNumber++;
    //        }
    //    }
    //}

    //protected void txt_ReqApproxSalary_OnTextChanged(object sender, EventArgs e)
    //{
    //    var EmployeeRequisition = decimal.Parse(txt_EmployeeRequisition.Text.Trim());
    //    var ReqApproxSalary = decimal.Parse(txt_ReqApproxSalary.Text.Trim());
    //    lbl_ReqTotalSalary.Text = (EmployeeRequisition * ReqApproxSalary).ToString();
    //}

    //protected void txt_EmployeeRequisition_OnTextChanged(object sender, EventArgs e)
    //{
    //    var EmployeeRequisition = decimal.Parse(string.IsNullOrEmpty(txt_EmployeeRequisition.Text.Trim())?"0": txt_EmployeeRequisition.Text.Trim());
    //    var ReqApproxSalary = decimal.Parse(string.IsNullOrEmpty(txt_ReqApproxSalary.Text.Trim())?"0" :txt_ReqApproxSalary.Text.Trim());
    //    lbl_ReqTotalSalary.Text = (EmployeeRequisition * ReqApproxSalary).ToString();
    //}

    protected void ddlGradeEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGradeEx.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLDesignationByGrade(int.Parse(ddlGradeEx.SelectedValue)))
            {
                ////TODO
                //ddlDesignationEx.DataSource = dt;
                //ddlDesignationEx.DataValueField = "Value";
                //ddlDesignationEx.DataTextField = "TextField";
                //ddlDesignationEx.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLStepByGrade(int.Parse(ddlGradeEx.SelectedValue)))
            {
                ddlStepEx.DataSource = dt;
                ddlStepEx.DataValueField = "Value";
                ddlStepEx.DataTextField = "TextField";
                ddlStepEx.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetGradeExChangeInfo(int.Parse(ddlGradeEx.SelectedValue)))
            {
                if (dt.Rows.Count > 0)
                {
                    lblExGradeTotalEmp.Text = dt.Rows[0]["ExGradeTotalEmp"].ToString();
                    //lblExGradeTotalSal.Text = dt.Rows[0]["ExGradeTotalSal"].ToString();
                    lblExGradeMaxSal.Text = dt.Rows[0]["ExGradeMaxSal"].ToString();
                    lblExGradeMinSal.Text = dt.Rows[0]["ExGradeMinSal"].ToString();
                }

            }
        }
    }

    protected void ddlDesignationEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlStepEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStepEx.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetStepExChangeInfo(int.Parse(ddlStepEx.SelectedValue)))
            {
                if (dt.Rows.Count > 0)
                {
                    ////TODO lblExStepTotalEmp.Text = dt.Rows[0]["ExStepTotalEmp"].ToString();
                }

            }
        }
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLGradeByDepartment(int.Parse(ddlDepartment.SelectedValue)))
            {
                ddlGradeEx.DataSource = dt;
                ddlGradeEx.DataValueField = "Value";
                ddlGradeEx.DataTextField = "TextField";
                ddlGradeEx.DataBind();
            }
        }
    }

    //protected void chk_NewDesignation_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    if (chk_NewDesignation.Checked)
    //    {
    //        using (DataTable dt = _commonDataLoad.GetEmpCategoryDDL())
    //        {
    //            ddlEmpCategoryNew.DataSource = dt;
    //            ddlEmpCategoryNew.DataValueField = "Value";
    //            ddlEmpCategoryNew.DataTextField = "TextField";
    //            ddlEmpCategoryNew.DataBind();
    //        }

    //        using (DataTable dt = _commonDataLoad.GetDDLGradeNew(int.Parse(ddlCompany.SelectedValue)))
    //        {
    //            ddlGradeNew.DataSource = dt;
    //            ddlGradeNew.DataValueField = "Value";
    //            ddlGradeNew.DataTextField = "TextField";
    //            ddlGradeNew.DataBind();
    //        }
    //        txt_EmployeeRequisition.Text = "0";
    //        txt_ReqApproxSalary.Text = "0";
    //        lbl_ReqTotalSalary.Text = "0";
    //    }
    //    else
    //    {
    //        ddlGradeNew.DataSource = null;
    //        ddlGradeNew.DataBind();

    //        ddlDesignationNew.DataSource = null;
    //        ddlDesignationNew.DataBind();

    //        ddlStepNew.DataSource = null;
    //        ddlStepNew.DataBind();


    //    }
    //}

    //protected void ddlGradeNew_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlGradeNew.SelectedIndex>0)
    //    {
    //        using (DataTable dt = _commonDataLoad.GetDDLDesignationByGrade(int.Parse(ddlGradeNew.SelectedValue)))
    //        {
    //            ddlDesignationNew.DataSource = dt;
    //            ddlDesignationNew.DataValueField = "Value";
    //            ddlDesignationNew.DataTextField = "TextField";
    //            ddlDesignationNew.DataBind();
    //        }

    //        using (DataTable dt = _commonDataLoad.GetDDLStepByGrade(int.Parse(ddlGradeNew.SelectedValue)))
    //        {
    //            ddlStepNew.DataSource = dt;
    //            ddlStepNew.DataValueField = "Value";
    //            ddlStepNew.DataTextField = "TextField";
    //            ddlStepNew.DataBind();
    //        }
    //    }
    //}


    protected void ddlEmpCategoryEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategoryEx.SelectedIndex > 0)
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                using (DataTable dt = _commonDataLoad.GetDDLGradeByDepartmentAndCategory(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmpCategoryEx.SelectedValue)))
                {
                    ddlGradeEx.DataSource = dt;
                    ddlGradeEx.DataValueField = "Value";
                    ddlGradeEx.DataTextField = "TextField";
                    ddlGradeEx.DataBind();
                }
            }
            else
            {
                AlertMessageBoxShow("Please select Department first...");
            }
        }
    }

    //protected void ddlEmpCategoryNew_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlEmpCategoryNew.SelectedIndex > 0)
    //    {
    //        using (DataTable dt = _commonDataLoad.GetDDLGradeNewByCategory(int.Parse(ddlCompany.SelectedValue), int.Parse(ddlEmpCategoryNew.SelectedValue)))
    //        {
    //            ddlGradeNew.DataSource = dt;
    //            ddlGradeNew.DataValueField = "Value";
    //            ddlGradeNew.DataTextField = "TextField";
    //            ddlGradeNew.DataBind();
    //        }
    //    }
    //}

    protected void radEmpType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (radEmpType.SelectedIndex >= 0)
        {
            if (radEmpType.SelectedValue.Trim() == "3")
            {
                if (ddlCompany.SelectedIndex > 0)
                {
                    ddlProjectName.Enabled = true;
                    using (DataTable dt = _commonDataLoad.GetProjectNameDDL(int.Parse(ddlCompany.SelectedValue)))
                    {
                        ddlProjectName.DataSource = dt;
                        ddlProjectName.DataValueField = "Value";
                        ddlProjectName.DataTextField = "TextField";
                        ddlProjectName.DataBind();
                    }

                }
                else
                {
                    AlertMessageBoxShow("Company required...!");
                }

            }
            else
            {
                ddlProjectName.Enabled = false;
                ddlProjectName.DataSource = null;
                ddlProjectName.DataBind();
            }
        }
    }

    protected void BacktoList_OnClick(object sender, EventArgs e)
    {
      Response.Redirect("MPBudgetListApproval.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {

            if (valiHRR())
            {


                int mainid
                    = Convert.ToInt32(hdpk.Value);
                string ActionStatus = actionRadioButtonList.SelectedValue;
                bool status = _mpBudgetCommonDal.UpdateJobReqStatus2(ActionStatus, mainid);
                if (status)
                {
                    int commentid = _mpBudgetCommonDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                        txtComment.Text);
                    if (ActionStatus == "Verified")
                    {
                        DataTable dtempdata =

                        _mpBudgetCommonDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                  "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                  "' " +
                                                                      "   AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");

                        int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                        DataTable dtempdata2 =
                           _mpBudgetCommonDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                   "' AND Serial='" + serial + "' " +
                                                                       "  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                        //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                        MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                            appLogDao.MPBudgetMasterId = mainid;
                            appLogDao.Comments = txtComment.Text;
                            appLogDao.CommentsId = commentid;

                        }
                        ;
                        int id = _mpBudgetCommonDal.SavAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Manpower Budget Approval ", @"  <br/> Dear Sir, <br/>
Manpower Budget is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");

                    }
                    else if (ActionStatus == "Approved")
                    {
                        int empid = 0;
                        //DataTable dtempdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                        //if (dtempdata.Rows.Count>0)
                        //{
                        //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        //}
                        MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.MPBudgetMasterId = mainid;
                            appLogDao.Comments = txtComment.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        ;


                        int id = _mpBudgetCommonDal.SavAppLog(appLogDao);


                        EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();

                        DataTable dtLoop = _mpBudgetCommonDal.GetAppLogEmployeeApprovalID(hdpk.Value.ToString());
                        DataTable finapp = aEmployeeRequsitionDal.getFinalApprovePersonInfo(appLogDao.PreEmpInfoId.ToString());
                        string finEmpName = "";
                        string finEmpdes = "";
                        if (finapp.Rows.Count > 0)
                        {
                            finEmpName = finapp.Rows[0]["EmpName"].ToString();
                            finEmpdes = finapp.Rows[0]["Designation"].ToString();
                        }

                        if (dtLoop.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtLoop.Rows.Count; i++)
                            {


                                SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), " Manpower Budget Approved ", @"  <br/> Dear Sir, <br/>
Manpower Budget has been Approved. <br/><br/><br/>Final Approved By:<br/>" + finEmpName + @"<br/>" + finEmpdes + @"


<br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");

                            }
                        }


 
                    }
                    else if (ActionStatus == "Review")
                    {
                        string actionst = "";
                        DataTable dtempdata = _mpBudgetCommonDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(),
                            mainid.ToString());
                        if (dtempdata.Rows.Count > 0)
                        {
                            actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                        }
                        DataTable dtempdata2 =
                            _mpBudgetCommonDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(),
                                mainid.ToString());
                        int a = 0;
                        for (int i = 0; i < dtempdata2.Rows.Count; i++)
                        {
                            if (dtempdata.Rows[i]["PreEmpInfoId"].ToString() !=
                                dtempdata.Rows[i]["ForEmpInfoId"].ToString())
                            {
                                a = i;
                                break;
                            }
                        }
                        if (dtempdata2.Rows.Count > 0)
                        {
                            MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
                            {
                                //appLogDao.ActionStatus = "Verified";
                                appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                                appLogDao.MPBudgetMasterId = mainid;
                                appLogDao.Comments = txtComment.Text;
                                appLogDao.CommentsId = commentid;
                            }
                            if (actionst == "Approved")
                            {
                                ActionStatus = "Verified";
                                _mpBudgetCommonDal.UpdateContractural(ActionStatus, mainid);
                            }
                            _mpBudgetCommonDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                            _mpBudgetCommonDal.UpdateAppLog("Review", dtempdata2.Rows[a][0].ToString());
                            int id = _mpBudgetCommonDal.SavAppLog(appLogDao);
                        }
                        else
                        {
                            ShowMessageBox("Please select Approval Status Approved  this!!!");
                        }

                        DataTable dtdata = _mpBudgetCommonDal.GetDataReviewEntryBy(
                            hdpk.Value, Session["UserId"].ToString(), "Review");
                        if (dtdata.Rows.Count > 0)
                        {
                            Session["Status"] = "";
                            Session["Status"] = "Edit";
                            Session["MPBudgetEdit"] = mainid.ToString();
                            Response.Redirect("MPBudgetEntry.aspx?id2=" + mainid.ToString());
                        }

                    }
                    Session["AppLogId"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Data Saved Successfully...');window.location ='MPBudgetListApproval.aspx';",
                        true);

                }

            }
        }
    }


    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();




            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Session["EmailID"].ToString());
            try
            {
                mail.To.Add(ForMailAddress.Trim());
            }
            catch (Exception)
            {
                //throw;
            }
            mail.Subject = mSubject;
            mail.Body =
                "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                WebUtility.HtmlDecode(mBody)
                +
                "</div>";

            //Attach file using FileUpload Control and put the file in memory stream

            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(),
                Session["AppPass"].ToString());
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(mail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
            //    showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);
        }



    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        
        int mainid
            = Convert.ToInt32(hdpk.Value);
        string ActionStatus = "Rejected";
        bool status = _mpBudgetCommonDal.UpdateContractural(ActionStatus, mainid);
        int commentid = _mpBudgetCommonDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                txtComment.Text);
        if (ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.MPBudgetMasterId = mainid;
                appLogDao.Comments = txtComment.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = _mpBudgetCommonDal.SavAppLog(appLogDao);
            _mpBudgetCommonDal.UpdateJobReqStatus2(ActionStatus, mainid);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='MPBudgetListApproval.aspx';",
                   true);
    }

}