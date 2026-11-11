using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.UA_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MenuSetup_SupervisorApprovalEntry : System.Web.UI.Page
{
    SupervisorMenuAppDAL appDal=new SupervisorMenuAppDAL();
    CommonDataLoadDAL aDataLoadDal=new CommonDataLoadDAL();
    AppLogReportDAL appLogReportDal=new AppLogReportDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList();
        }
    }

    public void DropDownList()
    {
        aDataLoadDal.CompanyDropDown(companyDropDownList," ");
        companyDropDownList.SelectedIndex = 1;
        aDataLoadDal.MenuDropDown(menuDropDownList);
        companyDropDownList_OnSelectedIndexChanged(null, null);
    }

    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                if (chkSingle.Visible)
                {
                    chkSingle.Checked = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    
    }

    public void LoadEmp()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            appDal.LoadEmployeeDrop((DropDownList)loadGridView.Rows[i].FindControl("employeeDropDownList"), loadGridView.DataKeys[i]["DepartmentId"].ToString());
            ((DropDownList) loadGridView.Rows[i].FindControl("employeeDropDownList")).SelectedValue =
                loadGridView.DataKeys[i]["EmpInfoId"].ToString();
            CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
            chkSingle.Checked = Convert.ToBoolean(loadGridView.DataKeys[i]["IsChecked"].ToString());

        }
    }

    public void Save()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            try
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                if (chkSingle.Checked)
                {


                    SupervisorMenuAppDAO appDao = new SupervisorMenuAppDAO()
                    {
                        CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue),
                        EmpInfoId =
                            Convert.ToInt32(
                                ((DropDownList) loadGridView.Rows[i].FindControl("employeeDropDownList")).SelectedValue),
                        MainMenuId = Convert.ToInt32(loadGridView.DataKeys[i]["MainMenuId"].ToString()),
                        SuperMenuAppId =
                            string.IsNullOrEmpty(loadGridView.DataKeys[i]["SuperMenuAppId"].ToString())
                                ? 0
                                : Convert.ToInt32(loadGridView.DataKeys[i]["SuperMenuAppId"].ToString()),


                    };
                    int id = appDal.SaveSupervisorApp(appDao);
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Saved Successfully...');window.location ='SupervisorApprovalEntry.aspx';",
                   true);
        
    }
    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //aDataLoadDal.DeptByCompanyDropDown(deptDropDownList,companyDropDownList.SelectedValue);
        aDataLoadDal.FinYearByCompDropDown(finyearDropDownList1,companyDropDownList.SelectedValue);
        menuDropDownList.SelectedIndex = 0;
        loadGridView.DataSource = null;
        loadGridView.DataBind();
    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        Save();
    }

    protected void deptDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void menuDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        // DataTable dtdata = appDal.GetData(companyDropDownList.SelectedValue,menuDropDownList.SelectedValue);
        // if (dtdata.Rows.Count>0)
        //{
        //      loadGridView.DataSource = dtdata;
        //loadGridView.DataBind();
        //LoadEmp();
        //}
        //else
        // {
        //     loadGridView.DataSource = null;
        //     loadGridView.DataBind();
        // }
    }

    public void DataLoad()
    {
        string MainTable = "";
        string AppLogTable = "";
        string MainTableId = "";
        string mainTableCol = "";
        string deptCol = "";
        string empcol = "";
        string companyId = companyDropDownList.SelectedValue;
        string finyearId = finyearDropDownList1.SelectedValue;
        string fromdate = "";
        string todate = "";


        MainTable = "";
        AppLogTable = "";
        MainTableId = "";
        mainTableCol = "";
        empcol = "";
        deptCol = "";

        DataTable dtdata = null;

        DataTable dtfinyear = appLogReportDal.GetFinyear(companyId, finyearId);
        if (dtfinyear.Rows.Count>0)
        {
            fromdate = Convert.ToDateTime(dtfinyear.Rows[0]["StartDate"].ToString()).ToString("dd-MMM-yyyy");
            todate = Convert.ToDateTime(dtfinyear.Rows[0]["EndDate"].ToString()).ToString("dd-MMM-yyyy");
        }
        if (menuDropDownList.SelectedValue == "3123")
        {
            MainTable = "tblMPBudgetMaster";
            AppLogTable = "tblMPBudgetMasterAppLog";
            MainTableId = "MPBudgetMasterId";
            mainTableCol = "BudgetCode";
            deptCol = "DepartmentId";
            empcol = "";

            dtdata = appLogReportDal.GetOtherAppLogInfo(companyId, finyearId, MainTable, AppLogTable, MainTableId,
                mainTableCol,deptCol);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        
        }
        if (menuDropDownList.SelectedValue == "3124")
        {
            MainTable = "tblJobReqForm";
            AppLogTable = "tblJobReqFormAppLog";
            MainTableId = "JobReqId";
            mainTableCol = "ReqCode";
            deptCol = "DeptId";
            empcol = "";

            dtdata = appLogReportDal.GetOtherAppLogInfo(companyId, finyearId, MainTable, AppLogTable, MainTableId,
                mainTableCol,deptCol);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        if (menuDropDownList.SelectedValue == "3131")
        {
            MainTable = "tblAppraisalSelfMaster";
            AppLogTable = "tblAppraisalSelfAppLog";
            MainTableId = "AppraisalSelfMasterId";
            mainTableCol = "";
            deptCol = "";
            empcol = "EmpInfoId";

            dtdata = appLogReportDal.GetEmpAppLogInfo(companyId, fromdate, todate, MainTable, AppLogTable, empcol,
                MainTableId);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        if (menuDropDownList.SelectedValue == "3145")
        {
            MainTable = "tblTrainingRecordMaster";
            AppLogTable = "tblTrainingRecordMasterAppLog";
            MainTableId = "TrainingRecordMasterId";
            mainTableCol = "TrainingRecordNo";
            deptCol = "";
            empcol = "";

            dtdata = appLogReportDal.GetOtherAppLogInfo(companyId, finyearId, MainTable, AppLogTable, MainTableId,
                mainTableCol, deptCol);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        if (menuDropDownList.SelectedValue == "3159")
        {
            MainTable = "tblJdMaster";
            AppLogTable = "tblJDAppLog";
            MainTableId = "JdMasterId";
            mainTableCol = "";
            deptCol = "";
            empcol = "EmpInfoId";

            dtdata = appLogReportDal.GetEmpAppLogInfo(companyId,fromdate,todate, MainTable, AppLogTable, empcol,
                MainTableId);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        if (menuDropDownList.SelectedValue == "3161")
        {
            MainTable = "tblProbationEvaluationMaster";
            AppLogTable = "tblProbationEvaluationAppLog";
            MainTableId = "ProbationEvaluationMasterId";
            mainTableCol = "";
            deptCol = "";
            empcol = "EmpInfoId";

            dtdata = appLogReportDal.GetEmpAppLogInfo(companyId, fromdate, todate, MainTable, AppLogTable, empcol,
                MainTableId);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        ///Problem
        if (menuDropDownList.SelectedValue == "3162")
        {
            MainTable = "tblAppraisalMaster";
            AppLogTable = "tblAppraisalMasterAppLog";
            MainTableId = "AppraisalMasterId";
            mainTableCol = "";
            deptCol = "";
            empcol = "EmpInfoId";

            dtdata = appLogReportDal.GetEmpAppLogInfo(companyId, fromdate, todate, MainTable, AppLogTable, empcol,
                MainTableId);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        if (menuDropDownList.SelectedValue == "3185")
        {
            MainTable = "tblContractualEmpManage";
            AppLogTable = "tblContractualEmpManageAppLog";
            MainTableId = "ContractualEmpManageId";
            mainTableCol = "";
            deptCol = "";
            empcol = "EmployeeId";

            dtdata = appLogReportDal.GetEmpAppLogInfo(companyId, fromdate, todate, MainTable, AppLogTable, empcol,
                MainTableId);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        //Problem
        if (menuDropDownList.SelectedValue == "3248")
        {
            MainTable = "tblRecruitmentApproval";
            AppLogTable = "tblRecruitmentApprovalAppLog";
            MainTableId = "RecruitmentId";
            mainTableCol = "";
            deptCol = "";
            empcol = "EmployeeId";

            dtdata = appLogReportDal.GetEmpAppLogInfo(companyId, fromdate, todate, MainTable, AppLogTable, empcol,
                MainTableId);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        if (menuDropDownList.SelectedValue == "3249")
        {
            MainTable = "tblTrainingBudget2Master";
            AppLogTable = "tblTrainingBudget2MasterAppLog";
            MainTableId = "TrainingBudget2Id";
            mainTableCol = "TrainingBudgetNumber";
            deptCol = "";
            empcol = "";

            dtdata = appLogReportDal.GetOtherAppLogInfo(companyId, finyearId, MainTable, AppLogTable, MainTableId,
                mainTableCol, deptCol);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
        if (menuDropDownList.SelectedValue == "3251")
        {
            MainTable = "tblIncrement";
            AppLogTable = "tblIncrementAppLog";
            MainTableId = "IncrementId";
            mainTableCol = "TrainingBudgetNumber";
            deptCol = "";
            empcol = "EmployeeId";

            dtdata = appLogReportDal.GetEmpAppLogInfo(companyId, fromdate, todate, MainTable, AppLogTable, empcol,
                MainTableId);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();


        }
    }

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    protected void btnFilterSearch_OnClick(object sender, EventArgs e)
    {

        if (Validation())
        {
             DataLoad();
        }
       
    }

    private bool Validation()
    {


        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
            companyDropDownList.Focus();
            return false;
        }


        if (finyearDropDownList1.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
            finyearDropDownList1.Focus();
            return false;
        }

        if (menuDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
            menuDropDownList.Focus();
            return false;
        }

        

        return true;
    }
}