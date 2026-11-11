using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.ExitManagement_DAL;
using DAL.HealthCare_DAL;
using DAL.MasterSetup_DAL;
using DAL.MeetingMinorsDAL;
using DAL.Permission_DAL;
using DAL.RecruitmentApp_DAL;
using DAL.SKILL_WILL_DAL;
using DAL.Survey;
using DAL.UserProfileDAL;
using DAO.HRIS_DAO;


public partial class DashBoard_UI_DashBoard : System.Web.UI.Page
{
  //  DashBoardBll aDashBoardBll = new DashBoardBll();
    KPIDashboardDAL aDAL = new KPIDashboardDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    RecruitmentAppDAL appDal = new RecruitmentAppDAL();
    private CommitteeApprovalPanelDal approvalPanel = new CommitteeApprovalPanelDal();
    PermissionDAL aPermissionDal = new PermissionDAL();

    KPIInformationViewDAL afinancial = new KPIInformationViewDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack || Request["__EVENTTARGET"] == dashboardLoadTimer.UniqueID)
        {
            if (Session["EmpInfoId"] != null && Session["EmpInfoId"] != "")
            {
                GetCompany();
                if (!IsPostBack)
                {
                    try
                    {
                       
                        GetOneRecord(Session["EmpInfoId"].ToString());
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    return;
                }

                try
                {
                    GetOneRecord(Session["EmpInfoId"].ToString());

 


                        

                            EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();

                            DataTable mpPending = aDAL.GetManpowerBudgetInfoApproval(Session["EmpInfoId"].ToString(), ddlFinancialYear.SelectedValue);

                            lblManpowerBudgetPending.Text = mpPending.Rows.Count.ToString();

                            DataTable RequisitionPen = new DataTable();
                            RequisitionPen = aEmployeeRequsitionDal.LoadEmpJobRequisitionApp();
                            lblRequisitionPen.Text = RequisitionPen.Rows.Count.ToString();



                            DataTable RecruitmentPending = new DataTable();
                            RecruitmentPending = appDal.GetRecruitmentInfo("");
                            lblRecruitmentPending.Text = RecruitmentPending.Rows.Count.ToString();


                   
                    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();
                    string currentEmpId = Session["EmpInfoId"] != null ? Session["EmpInfoId"].ToString() : "0";
                    string clearanceFilter = @" EIM.ExitMasterId IN (
                        SELECT MasterId FROM dbo.tblEmpExitDetail 
                        WHERE IsDone = 0 
                          AND (
                              (
                                  EmpInfoIdApproval = '" + currentEmpId + @"'
                                  AND (
                                      ISNULL(isForwardBackDone, 0) = 1
                                      OR EmpInfoIdApproval NOT IN (
                                          SELECT m.EmployeeId 
                                          FROM dbo.tblClearanceForwardingMaster m 
                                          INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                                          WHERE m.Status = 'Forwarded' AND d.Status = 'Pending'
                                      )
                                  )
                              )
                              OR (
                                  ISNULL(isForwardBackDone, 0) = 0
                                  AND EmpInfoIdApproval IN (
                                      SELECT m.EmployeeId 
                                      FROM dbo.tblClearanceForwardingMaster m 
                                      INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                                      WHERE d.SupervisedEmployeeId = '" + currentEmpId + @"' 
                                        AND d.Status = 'Pending' 
                                        AND m.Status = 'Forwarded'
                                  )
                              )
                          )
                    )";

                    DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlApprovalNew(clearanceFilter);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        loadGridView.DataSource = dataTable;
                        loadGridView.DataBind();
                        lblClearenceFormApproval.Text = dataTable.Rows.Count.ToString();
                    }
                    else
                    {
                        loadGridView.DataSource = null;
                        loadGridView.DataBind();
                        lblClearenceFormApproval.Text = "0";
                    }

                    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();


                     DataTable aDataTable = AMAsterDal.LoadInfoApproveList();
                    if (aDataTable.Rows.Count > 0)
                    {
                        lblMiscellaneousMeetingPending.Text = aDataTable.Rows.Count.ToString();
                    }
                    else
                    {
                        lblMiscellaneousMeetingPending.Text = "0";
                    }
                }
                catch (Exception)
                {
                    
                    //throw;
                }




                try
                {

                    BSCAppraisalFunctionalPartDAL _aFincDal = new BSCAppraisalFunctionalPartDAL();
                    DataTable dt22 = new DataTable();
                    DataTable dt = _aFincDal.GetAppraisalByKpiPermissionsssDashboard(Session["EmpInfoId"].ToString(),
                          "  and   tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer ", "  ");
                    if (dt.Rows.Count > 0)
                    {
                        GridViewokrbscSelf.DataSource = dt;
                        GridViewokrbscSelf.DataBind();
                         

                    }
                    else
                    {
                        dt22 = _aFincDal.GetAppraisalByKpiPermissionsssDashboard(Session["EmpInfoId"].ToString(), "", "  ");
                        GridViewokrbscSelf.DataSource = dt22;
                        GridViewokrbscSelf.DataBind();
                         
                    }


                    DataTable dt2 = _aFincDal.GetAppraisalByKpiPermission2FinYearDashboard(Session["EmpInfoId"].ToString());

                    for (int i = 0; i < GridViewokrbscSelf.Rows.Count; i++)
                    { 
                        DataTable dt3 = new DataTable();
                        if (dt.Rows.Count > 0)
                        {


                            dt3 =
                              _aFincDal.GetAppraisalByPermission3(dt.Rows[0]["BSCAppraisalSelfMasterId"].ToString());
                        }

                        else
                        {

                        }
                        string EmpID = "";
                        string Actions = "";
                        if (dt3.Rows.Count > 0)
                        {
                            EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                            Actions = dt3.Rows[0]["ActionStatus"].ToString();
                        }

                        if (GridViewokrbscSelf.DataKeys[i][0].ToString() == "Verified")
                        {
                            lblOKBSCSetupPersonal.Text = "0";
                        }
                        else
                        {
                            lblOKBSCSetupPersonal.Text = "1";
                        }

                        if (GridViewokrbscSelf.DataKeys[i][0].ToString() == "Approved")
                        {
                            lblOKBSCSetupPersonal.Text = "0";
                        }
                        else
                        {
                            lblOKBSCSetupPersonal.Text = "1";
                        }


                        if (dt2.Rows.Count > 0 && Actions.ToString() == "Verified" && EmpID != Session["EmpInfoId"].ToString())
                        {
                            lblOKBSCSetupPersonal.Text = "0";
                        }
                        else
                        {
                            lblOKBSCSetupPersonal.Text = "1";
                        }

                        //if (gv_JdBoard.DataKeys[i][0].ToString() != "Approved" || dt2.Rows.Count == 0)
                        //{
                        //    ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = false;



                        //}
                        //if (dt2.Rows.Count != 0 || gv_JdBoard.DataKeys[i][0].ToString() != "Approved")
                        //{
                        //    ((LinkButton)gv_JdBoard.Rows[i].FindControl("btn_edit")).Visible = true;
                        //}


                        AppraisalFunctionalPartDAL _aFincDalrr = new AppraisalFunctionalPartDAL();

                        DataTable dt2255 = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard(Session["EmpInfoId"].ToString(),
                               "  ");
                        if (dt2255.Rows.Count > 0)
                        {
                            if (dt2.Rows.Count == 0)
                            {

                                lblOKBSCSetupPersonal.Text = "0";
                            }
                            else
                            {
                                lblOKBSCSetupPersonal.Text = "1";
                            }
                        }
                    }

                    //    BSCAppraisalFunctionalPartDAL _aFincDalrr = new BSCAppraisalFunctionalPartDAL();
                    //DataTable dt = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard(Session["EmpInfoId"].ToString(),
                    //              "  and   tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer and app.ActionStatus<>'Approved'  ");
                    //if (dt.Rows.Count > 0)
                    //{

                    //    DataTable dt3 = new DataTable();
                    //    dt3 =
                    //       _aFincDalrr.GetAppraisalByPermission3sssssdsds(dt.Rows[0]["BSCAppraisalSelfMasterId"].ToString());
                    //    string EmpID = "";
                    //    string Actions = "";
                    //    if (dt3.Rows.Count > 0)
                    //    {
                    //        EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                    //        Actions = dt3.Rows[0]["ActionStatus"].ToString();
                    //    }


                    //    if (dt.Rows[0]["ActionStatus"].ToString() == "Verified")
                    //    {
                    //        lblOKBSCSetupPersonal.Text = "0";
                    //    }

                    //    else if (dt.Rows[0]["ActionStatus"].ToString() == "Approved")
                    //    {
                    //        lblOKBSCSetupPersonal.Text = "0";
                    //    }
                    //    else if (Actions.ToString() == "Verified" && EmpID != Session["EmpInfoId"].ToString())
                    //    {

                    //        lblOKBSCSetupPersonal.Text = "0";
                    //    }
                    //    else
                    //    {
                    //        lblOKBSCSetupPersonal.Text = dt.Rows.Count.ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    DataTable dt22 = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard_New(Session["EmpInfoId"].ToString(),
                    //         " and ISNULL(app.ActionStatus,'')<> 'Approved' ");
                    //    DataTable dt2 = _aFincDalrr.GetAppraisalByKpiPermission2(Session["EmpInfoId"].ToString());
                    //    if (dt22.Rows.Count > 0)
                    //    {
                    //        if (dt2.Rows.Count != 0 || dt22.Rows[0]["ActionStatus"].ToString() == "Approved")
                    //        {
                    //            lblOKBSCSetupPersonal.Text = dt22.Rows.Count.ToString();
                    //        }

                    //        if (dt22.Rows[0]["ActionStatus"].ToString() == "Approved")
                    //        {
                    //            lblOKBSCSetupPersonal.Text = "0";
                    //        }
                    //    }
                    //}






                }
                catch (Exception)
                {

                    //throw;
                }


                try
                {
                    
       AppraisalFunctionalPartDAL _aFincDalrr = new AppraisalFunctionalPartDAL();
       DataTable dt = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard(Session["EmpInfoId"].ToString(),
                     "  and   tblAppraisalSelfAppLog.Version=CELog.MaxVer and app.ActionStatus<>'Approved'  ");
                    if (dt.Rows.Count > 0)
                    {

                        DataTable dt3 = new DataTable();
                        dt3 =
                           _aFincDalrr.GetAppraisalByPermission3sssssdsds(dt.Rows[0]["AppraisalSelfMasterId"].ToString());
                        string EmpID = "";
                        string Actions = "";
                        if (dt3.Rows.Count > 0)
                        {
                            EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                            Actions = dt3.Rows[0]["ActionStatus"].ToString();
                        }


                        if (dt.Rows[0]["ActionStatus"].ToString() == "Verified")
                        {
                            lblKPIPendingforSetup.Text = "0";
                        }

                        else if (dt.Rows[0]["ActionStatus"].ToString() == "Approved")
                        {
                            lblKPIPendingforSetup.Text = "0";
                        }
                        else if (  Actions.ToString() == "Verified" && EmpID != Session["EmpInfoId"].ToString())
                        {

                            lblKPIPendingforSetup.Text = "0";
                        }
                        else
                        {
                            lblKPIPendingforSetup.Text = dt.Rows.Count.ToString();
                        }
                    }
                    else
                    {
                        DataTable dt22 = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard_New(Session["EmpInfoId"].ToString(),
                             " and ISNULL(app.ActionStatus,'')<> 'Approved' ");
                        DataTable dt2 = _aFincDalrr.GetAppraisalByKpiPermission2(Session["EmpInfoId"].ToString());
                        if (dt22.Rows.Count > 0)
                        {
                            if (dt2.Rows.Count != 0 || dt22.Rows[0]["ActionStatus"].ToString() == "Approved")
                            {
                                lblKPIPendingforSetup.Text = dt22.Rows.Count.ToString();
                            }

                            if (dt22.Rows[0]["ActionStatus"].ToString() == "Approved")
                            {
                                lblKPIPendingforSetup.Text = "0";
                            }
                        }
                    }


                  

                    

                }
                catch (Exception)
                {
                    
                    //throw;
                }

             
                    try
                {
                    
       BSCAppraisalFunctionalPartDAL _aFincDalrr = new BSCAppraisalFunctionalPartDAL();
       DataTable dt = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard(Session["EmpInfoId"].ToString(),
                     "  and   tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer and app.ActionStatus<>'Approved'  ");
                    if (dt.Rows.Count > 0)
                    {

                        DataTable dt3 = new DataTable();
                        dt3 =
                           _aFincDalrr.GetAppraisalByPermission3sssssdsds(dt.Rows[0]["BSCAppraisalSelfMasterId"].ToString());
                        string EmpID = "";
                        string Actions = "";
                        if (dt3.Rows.Count > 0)
                        {
                            EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                            Actions = dt3.Rows[0]["ActionStatus"].ToString();
                        }


                        if (dt.Rows[0]["ActionStatus"].ToString() == "Verified")
                        {
                            lblBSCKPIPendingforSetup.Text = "0";
                        }

                        else if (dt.Rows[0]["ActionStatus"].ToString() == "Approved")
                        {
                            lblBSCKPIPendingforSetup.Text = "0";
                        }
                        else if (  Actions.ToString() == "Verified" && EmpID != Session["EmpInfoId"].ToString())
                        {

                            lblBSCKPIPendingforSetup.Text = "0";
                        }
                        else
                        {
                            lblBSCKPIPendingforSetup.Text = dt.Rows.Count.ToString();
                        }
                    }
                    else
                    {
                        DataTable dt22 = _aFincDalrr.GetAppraisalByKpiPermissionDashBoard_New(Session["EmpInfoId"].ToString(),
                             " and ISNULL(app.ActionStatus,'')<> 'Approved' ");
                        DataTable dt2 = _aFincDalrr.GetAppraisalByKpiPermission2(Session["EmpInfoId"].ToString());
                        if (dt22.Rows.Count > 0)
                        {
                            if (dt2.Rows.Count != 0 || dt22.Rows[0]["ActionStatus"].ToString() == "Approved")
                            {
                                lblBSCKPIPendingforSetup.Text = dt22.Rows.Count.ToString();
                            }

                            if (dt22.Rows[0]["ActionStatus"].ToString() == "Approved")
                            {
                                lblBSCKPIPendingforSetup.Text = "0";
                            }
                        }
                    }


                  

                    

                }
                catch (Exception)
                {
                    
                    //throw;
                }

             


                try
                {
                    BSCAppraisalFunctionalPartDAL _aFincDal = new BSCAppraisalFunctionalPartDAL();

                    DataTable dtKPIStatusPending = _aFincDal.GetSelfAppraisalListApprove(Session["EmpInfoId"].ToString(), "");
                    if (dtKPIStatusPending.Rows.Count > 0)
                    {

                        int dddCount = 0;

                        gv_JdBoard.DataSource = dtKPIStatusPending;
                        gv_JdBoard.DataBind();

                        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
                        {
                            HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[i].FindControl("EmpInfoId");

                            if (Session["EmpInfoId"].ToString() == EmpInfoId.Value.Trim())
                            {
                                //dddCount--;
                                //if (dddCount < 0)
                                //{
                                //    dddCount = 0;
                                //}
                                gv_JdBoard.Rows[i].Visible = false;
                            }
                            else
                            {
                                dddCount++;

                            }

                        }

                        if (dddCount > 0)
                        {
                            lblBSCOKRKPIPending.Text = dddCount.ToString();

                        }
                        else
                        {
                            lblBSCOKRKPIPending.Text = "0";

                        }


                    }

                    else
                    {
                        lblBSCOKRKPIPending.Text = "0";
                    }

                }
                catch (Exception)
                {

                    //throw;
                }


                try
                {
      AppraisalFunctionalPartDAL _aFincDal = new AppraisalFunctionalPartDAL();

      DataTable dtKPIStatusPending = _aFincDal.GetSelfAppraisalListApprove(Session["EmpInfoId"].ToString(),"");
                    if (dtKPIStatusPending.Rows.Count > 0)
                    {

                        int dddCount = 0;

                        gv_JdBoard.DataSource = dtKPIStatusPending;
                        gv_JdBoard.DataBind();

                        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
                        {
                            HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[i].FindControl("EmpInfoId");

                            if (Session["EmpInfoId"].ToString() == EmpInfoId.Value.Trim())
                            {
                             //dddCount--;
                             //   if (dddCount < 0)
                             //   {
                             //       dddCount = 0;
                             //   }
                                gv_JdBoard.Rows[i].Visible = false;
                            }
                            else
                            {
                                  dddCount++;
                                
                            }

                        }

                        if (dddCount>0)
                        {
                            lblKPIPending.Text = dddCount.ToString();
                            
                        }
                        else
                        {
                            lblKPIPending.Text = "0";
                            
                        }


                    }

                    else
                    {
                        lblKPIPending.Text = "0";
                    }

                }
                catch (Exception)
                {
                    
                    //throw;
                }

                ProbationperiodDAL aDal = new ProbationperiodDAL();

                DataTable dtdata = aDal.LoadEmployeeDataProbation("");
                if (dtdata.Rows.Count > 0)
                {
                    lblEmployeeProbationPeriod.Text = dtdata.Rows.Count.ToString();

                }
                else
                {
                    lblEmployeeProbationPeriod.Text = "0";

                }

                ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();
                DataTable dataTable22 = aContractualEmpManageDAL.LoadInformationAppALl("");

                if (dataTable22.Rows.Count > 0)
                {
                    lblEmployeeStateChangeApproval.Text = dataTable22.Rows.Count.ToString();
                }
                else
                {
                    lblEmployeeStateChangeApproval.Text = "0";
                }


                AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();
                try
                {


                    DataTable dt = _appDashboard.GetAppraisalDashboardOwn333DashNew_(Convert.ToInt32(Session["EmpInfoId"]),
                            "  AND tblAppraisalMasterAppLog.Version=CELog.MaxVer  ");
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["EmpInfoId"].ToString() == Session["EmpInfoId"].ToString() && dt.Rows[0]["ActionStatusAppraisal"].ToString() != "Approved" && dt.Rows[0]["EmpName"].ToString().Trim() == dt.Rows[0]["PendingEmpApp"].ToString().Trim())
                        {
                            lblAppPendingforSetup.Text = dt.Rows.Count.ToString();
                        }





                    }
                    else
                    {
                        int totalCount = 0; // To track how many dt2 have rows

                        DataTable dataTable = _appDashboard.GetActiveDeadlineFinancialYear(Convert.ToInt32(Session["EmpInfoId"]));
                        string fYwe = "";
                        int fYearId = 0;
                        if (dataTable.Rows.Count > 0)
                        {
                            fYwe = dataTable.Rows[0]["FinancialYearDesc"].ToString().Trim();
                            if (dataTable.Rows[0]["FinancialYearId"] != DBNull.Value && dataTable.Rows[0]["FinancialYearId"].ToString() != "")
                            {
                                fYearId = Convert.ToInt32(dataTable.Rows[0]["FinancialYearId"]);
                            }
                        }

                        DataTable dt44 = _appDashboard.GetAppraisalDashboardOwn333fin(
                                Convert.ToInt32(Session["EmpInfoId"]),
                                fYearId,
                                "",
                                fYwe);

                        string empInfoId = Session["EmpInfoId"].ToString();
                        DataTable dt2 = _appDashboard.GetAppraisalByPermission2nnnnnnnn(fYwe, empInfoId);

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt44.Rows)
                            {
                                DataTable dt3 = _appDashboard.GetAppraisalByPermission3(row["AppraisalMasterId"].ToString());

                                if (dt3.Rows.Count == 0)
                                {
                                    totalCount++; // Not yet submitted for approval, and still within permission window
                                }
                            }
                        }

                        lblAppPendingforSetup.Text = totalCount > 0 ? totalCount.ToString() : "0";
                    }
                }
                catch (Exception)
                {

                    // throw;
                }







                try
                {



                    MBSCOKRAppraislDashboardDAL _appDashboardMOkr = new MBSCOKRAppraislDashboardDAL();
                    DataTable dt = new DataTable(); dt=_appDashboardMOkr.GetAppraisalDashboardOwn333finDashvboarddds(Convert.ToInt32(Session["EmpInfoId"]),  "  AND tblMBSCAppraisalMasterAppLog.Version=CELog.MaxVer  " );
                    if (dt.Rows.Count > 0)
                    {
                         
                    }
                    else
                    {


                          dt = _appDashboardMOkr.GetAppraisalDashboardOwn333finDashvboarddds(Convert.ToInt32(Session["EmpInfoId"]),
                         "  and aax.ActionStatus='Approved'   ");


                        

                    }


                    int ddddCount = 1;
                    if (dt.Rows.Count > 0)
                    {
                       

                            for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string AppraisalSelfMasterId = dt.Rows[i]["AppraisalSelfMasterId"].ToString();


                            DataTable dt3 = _appDashboard.GetAppraisalByPermission3(AppraisalSelfMasterId);
                            string EmpID = "";
                            string Actions = "";
                            if (dt3.Rows.Count > 0)
                            {
                                EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                                Actions = dt3.Rows[0]["ActionStatus"].ToString();
                            }


                            if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString())
                            {

                                ddddCount = 0;
                            }


                            if (Actions.ToString() == "Verified" || EmpID != Session["EmpInfoId"].ToString())
                            {
                                ddddCount = 0;
                            }
                            if (EmpID == "0")
                            {
                                ddddCount = 0;
                            }


                            if (dt3.Rows.Count == 0)
                            {
                                ddddCount = 0;
                            }

                            if (Actions.ToString() == "Verified" && EmpID == Session["EmpInfoId"].ToString())
                            {
                                ddddCount = 0;
                            }

                            else
                            {
                                ddddCount = 0;
                            }

                            if (Actions.ToString() == "" && EmpID == "")
                            {
                                ddddCount = 0;
                            }


                        }
                    }
                    else
                    {
                        ddddCount = 0;
                    }


                    if (ddddCount > 0)
                    {
                        lblMOKBSCAppraisalPersonal.Text = "1";
                    }
                    else
                    {
                        lblMOKBSCAppraisalPersonal.Text = "0";
                    }
                } catch (Exception)
                { 
}
                

                try
                {

                    string COMID = Session["CompanyId"].ToString();

                    DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(DateTime.Now, DateTime.Now, COMID);
                    string fYwe = "";
                    if (dataTable.Rows.Count > 0)
                    {
                        fYwe= dataTable.Rows[0]["FinancialYearDesc"].ToString().Trim();
                    }

                    KPIMIDAppraislDashboardDAL _appDashboardMOkr = new KPIMIDAppraislDashboardDAL();
                    DataTable dt2 = _appDashboardMOkr.GetAppraisalByPermission2(fYwe, Session["EmpInfoId"].ToString());

                    if (dt2.Rows.Count > 0)
                    {
                      
                        DataTable dt = _appDashboardMOkr.GetAppraisalDashboardOwn333fin_dsh(Convert.ToInt32(Session["EmpInfoId"]), fYwe,
                                                  @"       	 	  and aax.ActionStatus='Approved'   and aax.AppraisalSelfMasterId   not in ( select   aax.AppraisalSelfMasterId from tblKPIMIDAppraisalMaster aax
				  LEFT JOIN (SELECT  AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= aax.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = aax.AppraisalMasterId

								where   (Version=CELog.MaxVer ) and ( aax.EmpInfoId =   " + Convert.ToInt32(Session["EmpInfoId"]) + @"   or ( aax.EmpInfoId=tblKPIMIDAppraisalMasterAppLog.ForEmpInfoId and  ISNULL(tblKPIMIDAppraisalMasterAppLog.ActionStatus,'')   in ('Verified')))     and ( ISNULL(tblKPIMIDAppraisalMasterAppLog.ActionStatus,'')   in ('Approved')  or ( aax.EmpInfoId<>tblKPIMIDAppraisalMasterAppLog.ForEmpInfoId ))
				  )			 ");
                        if (dt.Rows.Count > 0)
                        {
                            lblKPIMidSelfMark.Text = "1";
                        }
                        else
                        {
                            lblKPIMidSelfMark.Text = "0";
                        }
                    }

                  
                } catch (Exception)
                { 
}

                try
                {

                    string COMID = Session["CompanyId"].ToString();

                    DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(DateTime.Now, DateTime.Now, COMID);
                    string fYwe = "";
                    if (dataTable.Rows.Count > 0)
                    {
                        fYwe= dataTable.Rows[0]["FinancialYearDesc"].ToString().Trim();
                    }

                    KPIMIDAppraislDashboardDAL _appDashboardMOkr = new KPIMIDAppraislDashboardDAL();
                    DataTable dt2 = _appDashboardMOkr.GetAppraisalByPermissionOKRBSC(fYwe, Session["EmpInfoId"].ToString());

                    if (dt2.Rows.Count > 0)
                    {
                        MBSCOKRAppraislDashboardDAL _OOOKR = new MBSCOKRAppraislDashboardDAL();
                        DataTable dt = _OOOKR.GetAppraisalDashboardOwn333finDashboard(Convert.ToInt32(Session["EmpInfoId"]), fYwe,
                                                  @"  and ( isnull(tblMBSCAppraisalMasterAppLog.ActionStatus,'')='Approved' or isnull(aax.ActionStatus,'')='Approved' )     AND (tblMBSCAppraisalMasterAppLog.Version=CELog.MaxVer or tblMBSCAppraisalMasterAppLog.Version is null) and (tblMBSCAppraisalMasterAppLog.ForEmpInfoId =emp.EmpInfoId or tblMBSCAppraisalMasterAppLog.ForEmpInfoId is null) ");
                        if (dt.Rows.Count > 0)
                        {
                            lblOKRBSCMidSelfMark.Text = "1";
                        }
                        else
                        {
                            lblOKRBSCMidSelfMark.Text = "0";
                        }
                    }

                  
                } catch (Exception)
                { 
}


BSCOKRAppraislDashboardDAL _appDashboardOKR = new BSCOKRAppraislDashboardDAL();
                try
                {


                    DataTable dt = _appDashboardOKR.GetAppraisalDashboardOwn333DashNew_(Convert.ToInt32(Session["EmpInfoId"]),
                            "  AND tblBSCAppraisalMasterAppLog.Version=CELog.MaxVer  ");
                    int totalCount = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["EmpInfoId"].ToString() == Session["EmpInfoId"].ToString() && row["ActionStatusAppraisal"].ToString() != "Approved" && row["EmpName"].ToString().Trim() == row["PendingEmpApp"].ToString().Trim())
                        {
                            totalCount++;
                        }
                    }

                    if (totalCount == 0)
                    {
                        DataTable dataTable = _appDashboardOKR.GetActiveDeadlineFinancialYear(Convert.ToInt32(Session["EmpInfoId"]));
                        string fYwe = "";
                        int fYearId = 0;
                        if (dataTable.Rows.Count > 0)
                        {
                            fYwe = dataTable.Rows[0]["FinancialYearDesc"].ToString().Trim();
                            if (dataTable.Rows[0]["FinancialYearId"] != DBNull.Value && dataTable.Rows[0]["FinancialYearId"].ToString() != "")
                            {
                                fYearId = Convert.ToInt32(dataTable.Rows[0]["FinancialYearId"]);
                            }
                        }

                        DataTable dt44 = _appDashboardOKR.GetAppraisalDashboardOwn333fin(
                                Convert.ToInt32(Session["EmpInfoId"]),
                                fYearId,
                                "",
                                fYwe);

                        string empInfoId = Session["EmpInfoId"].ToString();
                        DataTable dt2 = _appDashboardOKR.GetAppraisalByPermission2nnnnnnn(fYwe, empInfoId);

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt44.Rows)
                            {
                                DataTable dt3 = _appDashboardOKR.GetAppraisalByPermission3(row["AppraisalMasterId"].ToString());

                                if (dt3.Rows.Count == 0)
                                {
                                    totalCount++; // Not yet submitted for approval, and still within permission window
                                }
                            }
                        }
                    }

                    lblAppOKRBSEPendingforSetup.Text = totalCount > 0 ? totalCount.ToString() : "0";
                }
                catch (Exception)
                {

                    // throw;
                }
                try
                {
                    
                    DataTable DTSupp = _appDashboard.GetAppraisalDashboardSupApproval(Convert.ToInt32(Session["EmpInfoId"]));

                    if (DTSupp.Rows.Count > 0)
                    {

                        int dddCount = 0;

                        for (int i = 0; i < DTSupp.Rows.Count; i++)
                        {

                            
                            if (DTSupp.Rows[i]["EmpInfoId"].ToString() == Session["EmpInfoId"].ToString() )
                            {
                                //dddCount--;
                                //if (dddCount < 0)
                                //{
                                //    dddCount = 0;
                                //}
                            }
                            else
                            {
                                dddCount++; 
                            }
                        }

                        if (dddCount > 0)
                        {
                            lblAppPending.Text = dddCount.ToString();

                        }
                        else
                        {
                            lblAppPending.Text = "0";

                        }

                        
                    }
                    else
                    {
                        lblAppPending.Text = "0";
                    }
                }
                catch (Exception)
                {
                    
                  //  throw;
                }



                try
                {
                    BSCOKRAppraislDashboardDAL _appDashboardOKRBSC = new BSCOKRAppraislDashboardDAL();
                    DataTable DTSupp = _appDashboardOKRBSC.GetAppraisalDashboardSupApproval(Convert.ToInt32(Session["EmpInfoId"]));

                    if (DTSupp.Rows.Count > 0)
                    {

                        int dddCount = 0;

                        for (int i = 0; i < DTSupp.Rows.Count; i++)
                        {


                            if (DTSupp.Rows[i]["EmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
                            {
                                //dddCount--;

                                //if (dddCount < 0)
                                //{
                                //    dddCount = 0;
                                //}
                            }
                            else
                            {
                                dddCount++;
                            }
                        }

                        if (dddCount > 0)
                        {
                            lblOKBSCAppraisalApproval.Text = dddCount.ToString();

                        }
                        else
                        {
                            lblOKBSCAppraisalApproval.Text = "0";

                        }


                    }
                    else
                    {
                        lblOKBSCAppraisalApproval.Text = "0";
                    }
                }
                catch (Exception)
                {

                    //  throw;
                }
                
                try
                {
                   KPIMIDAppraislDashboardDAL _appDashboardOKRBSC = new KPIMIDAppraislDashboardDAL();
                    DataTable DTSupp = _appDashboardOKRBSC.GetAppraisalDashboardSupApproval(Convert.ToInt32(Session["EmpInfoId"]));

                    if (DTSupp.Rows.Count > 0)
                    {

                        int dddCount = 0;

                        for (int i = 0; i < DTSupp.Rows.Count; i++)
                        {


                            if (DTSupp.Rows[i]["EmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
                            {
                                //dddCount--;
                                //if (dddCount < 0)
                                //{
                                //    dddCount = 0;
                                //}
                            }
                            else
                            {
                                dddCount++;
                            }
                        }

                        if (dddCount > 0)
                        {
                            lblKPIMidSupervisorMark.Text = dddCount.ToString();

                        }
                        else
                        {
                            lblKPIMidSupervisorMark.Text = "0";

                        }


                    }
                    else
                    {
                        lblKPIMidSupervisorMark.Text = "0";
                    }
                }
                catch (Exception)
                {

                    //  throw;
                }


                try
                {
                    MBSCOKRAppraislDashboardDAL _appDashboardOKRBSC = new MBSCOKRAppraislDashboardDAL();
                    DataTable DTSupp = _appDashboardOKRBSC.GetAppraisalDashboardSupApproval(Convert.ToInt32(Session["EmpInfoId"]));

                    if (DTSupp.Rows.Count > 0)
                    {

                        int dddCount = 0;

                        for (int i = 0; i < DTSupp.Rows.Count; i++)
                        {


                            if (DTSupp.Rows[i]["EmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
                            {
                                //dddCount--;
                                //if (dddCount < 0)
                                //{
                                //    dddCount = 0;
                                //}
                            }
                            else
                            {
                                dddCount++;
                            }
                        }

                        if (dddCount > 0)
                        {
                            lblMOKBSCAppraisalApproval.Text = dddCount.ToString();

                        }
                        else
                        {
                            lblMOKBSCAppraisalApproval.Text = "0";

                        }


                    }
                    else
                    {
                        lblMOKBSCAppraisalApproval.Text = "0";
                    }
                }
                catch (Exception)
                {

                    //  throw;
                }


                try
                {

      Skill_Will_Dal aDalsss = new Skill_Will_Dal();

    DataTable dtSkillWill = aDalsss.GetSkillWillMasterApprovalList();

                    if (dtSkillWill.Rows.Count > 0)
                    {
                        lblSkillWill.Text = dtSkillWill.Rows.Count.ToString();
                    }
                    else
                    {
                        lblSkillWill.Text = "0";
                    }

                }

                catch (Exception)
                {

                    //  throw;
                }



                try
                {

                    SkillWillDeclarationDal aDalsss = new SkillWillDeclarationDal();

                    DataTable dtSkillWill = aDalsss.GetDeclarationReportingEmpForDashboard();

                    if (dtSkillWill.Rows.Count > 0)
                    {
                        lblSkillWillSetup.Text = dtSkillWill.Rows.Count.ToString();
                    }
                    else
                    {
                        lblSkillWillSetup.Text = "0";
                    }

                }

                catch (Exception)
                {

                    //  throw;
                }

                 try
                {
      ReimbursmentFormDal aDalre = new ReimbursmentFormDal();


      DataTable dt = aDalre.Get_DataListForFormApproval();

                if (dt.Rows.Count > 0)
                {
                    lblExpenseReimbursementForm.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    lblExpenseReimbursementForm.Text = "0";

                }

                }

                 catch (Exception)
                 {

                     //  throw;
                 }


                try
                {

                    DataTable dt = approvalPanel.Get_CommitteeFeedBack(Session["EmpInfoId"].ToString(), " AND H.ActionStatus ='Verified' and comm.Feedback IS NULL AND H.IsForwardtoDoctor =1", "1,2,4");

                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                string hfSalaryLoationId = dt.Rows[i]["SalaryLoationId"].ToString();
                                string hfApplicationType = dt.Rows[i]["Type"].ToString();

                                DataTable DtCheck = approvalPanel.Get_CommitteeCheck(hfApplicationType, hfSalaryLoationId);
                                if (DtCheck.Rows.Count == 0)
                                {
                                    dt.Rows.RemoveAt(i);
                                    i--;
                                }
                            }

                            lblCommitteeFeedbackPanel.Text = dt.Rows.Count.ToString();

                        }
                        else
                        {
                            lblCommitteeFeedbackPanel.Text = "0";

                        }
                    
           
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                NotiShowHide();
            }
        }
    }
    protected void dashboardLoadTimer_Tick(object sender, EventArgs e)
    {
        dashboardLoadTimer.Enabled = false;
    }

    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();
        try
        {

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
        catch (Exception ex)
        {

            Response.Redirect("/Default.aspx");
        }
    }

    private void NotiShowHide()
    {
        if (lblKPIPendingforSetup.Text!="0")
        {
            hpKPIPendingforSetup.Visible = true;
        }
        
        if (lblOKBSCSetupPersonal.Text!="0")
        {
            hpOKBSCSetupPersonal.Visible = true;
        }
        
        if (lblBSCKPIPendingforSetup.Text!="0")
        {
            hpBSCKPIPendingforSetup.Visible = true;
        }

        if (lblKPIPending.Text != "0")
        {
            hpKPIPendingforApproval.Visible = true;
        }

        if (lblBSCOKRKPIPending.Text != "0")
        {
            hpBSCOKRKPIPending.Visible = true;
        }


        if (lblAppPendingforSetup.Text != "0")
        {
            hpAppPendingforSetup.Visible = true;
        }

         if (lblOKBSCAppraisalApproval.Text != "0")
        {
            hpOKBSCAppraisalApproval.Visible = true;
        }
         
         if (lblMOKBSCAppraisalApproval.Text != "0")
        {
            hpMOKBSCAppraisalApproval.Visible = true;
        }
         

         if (lblAppOKRBSEPendingforSetup.Text != "0")
        {
            hpAppOKRBSEPendingforSetup.Visible = true;
        }



        if (lblAppPending.Text != "0")
        {
            hpAppraisalPendingforApproval.Visible = true;
        }


        if (lblManpowerBudgetPending.Text != "0")
        {
            hpManpowerBudgetPending.Visible = true;
        }



        if (lblRequisitionPen.Text != "0")
        {
            hpRequisitionPen.Visible = true;
        }




        if (lblRecruitmentPending.Text != "0")
        {
            hpRecruitmentPending.Visible = true;
        }




        if (lblClearenceFormApproval.Text != "0")
        {
            hpClearenceFormApproval.Visible = true;
        }
        else
        {
            hpClearenceFormApproval.Visible = false;
        }





        if (lblMiscellaneousMeetingPending.Text != "0")
        {
            hpMiscellaneousMeetingPending.Visible = true;
        }



        if (lblEmployeeProbationPeriod.Text != "0")
        {
            hpEmployeeProbationPeriod.Visible = true;
        }



        if (lblEmployeeStateChangeApproval.Text != "0")
        {
            hpEmployeeStateChangeApproval.Visible = true;
        }




        if (lblSkillWillSetup.Text != "0")
        {
            hpSkillWillSetup.Visible = true;
        }




        if (lblSkillWill.Text != "0")
        {
            hpSkillWill.Visible = true;
        }



        if (lblExpenseReimbursementForm.Text != "0")
        {
            hpExpenseReimbursementForm.Visible = true;
        }


        if (lblCommitteeFeedbackPanel.Text != "0")
        {
            hpHCDoctorFeedback.Visible = true;
        }
        
        if (lblMOKBSCAppraisalPersonal.Text != "0")
        {
            hpMOKBSCAppraisalPersonal.Visible = true;
        }
        if (lblKPIMidSupervisorMark.Text != "0")
        {
            hlKPIMidSupervisorMark.Visible = true;
        }



        if (lblKPIMidSelfMark.Text != "0")
        {
            hlKPIMidSelfMark.Visible = true;
        }

        if (lblOKRBSCMidSelfMark.Text != "0")
        {
            hlOKRBSCMidSelfMark.Visible = true;
        }

    }

    private void GetKPISelfStatus(string EmpID, string FinId)
    {
        DataTable dtKPISelfStatus = aDAL.GetKPISelfStatusDAL(EmpID, FinId);

        const int rowIndex = 0;

        if (dtKPISelfStatus.Rows.Count > 0)
        {
            lblKPISelfStatus.Text = "Done";
        }
        else
        {
            lblKPISelfStatus.Text = "Pending";
        }



        DataTable dtApppSelfStatus = aDAL.GetApprisalSelfStatusDAL(EmpID, FinId);



        if (dtApppSelfStatus.Rows.Count > 0)
        {
            lblApprisalSelfStatus.Text = "Done";
        }
        else
        {
            lblApprisalSelfStatus.Text = "Pending";
        }



        DataTable dtKPIStatus = aDAL.GetKPIStatusDAL(EmpID, FinId);
        if (dtKPIStatus.Rows.Count > 0)
        {
            lblKpiDone.Text = dtKPIStatus.Rows.Count.ToString();
        }
           
        else
        {
            lblKpiDone.Text = "0";
        }





       




        DataTable dtAppppStatu = aDAL.GetAppppStatusDAL(EmpID, FinId);
        if (dtAppppStatu.Rows.Count > 0)
        {
            lblAppDone.Text = dtAppppStatu.Rows.Count.ToString();
        }
        else
        {
            lblAppDone.Text = "0";
        }



        DataTable dtAppppStatuPen = aDAL.GetAppppStatusDALPend(EmpID, FinId);
        if (dtAppppStatuPen.Rows.Count > 0)
        {
            lblAppPending.Text = dtAppppStatuPen.Rows.Count.ToString();
        }
        else
        {
            lblAppPending.Text = "0";
        }
    }

    private DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();


    private void GetOneRecord(string id)
    {

        DataTable dataTable = aDAL.GetEmployeeInfoDAL(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows[rowIndex].Field<string>("EmpImage") != "")
            {
                UserImage.ImageUrl = "~/UploadImg/" + dataTable.Rows[rowIndex].Field<string>("EmpImage");
            }
            else
            {
                UserImage.ImageUrl = "../Assets/man-icon.png";
            }


            lblshortName.Text = dataTable.Rows[rowIndex].Field<string>("EmpName");
            lblDesignation.Text = dataTable.Rows[rowIndex].Field<string>("Designation");
            lblID.Text = dataTable.Rows[rowIndex].Field<string>("EmpMasterCode");

            try
            {
                int comId = Convert.ToInt32(dataTable.Rows[rowIndex].Field<int>("CompanyId"));
                ComPanyID.Value = dataTable.Rows[rowIndex].Field<int>("CompanyId").ToString();
                DataTable dtaa = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(comId));
                ddlFinancialYear.DataSource = dtaa;
                ddlFinancialYear.DataValueField = "Value";
                ddlFinancialYear.DataTextField = "TextField";
                ddlFinancialYear.DataBind();
            }
            catch (Exception)
            {

                //throw;
            }


        }
    }
    private bool CheckStartEndDateExistOrNot(DateTime Start, DateTime End)
    {
        bool status = false;

        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(Start, End, ComPanyID.Value);

        if (dataTable.Rows.Count > 0)
        {
            ddlFinancialYear.SelectedValue = dataTable.Rows[0]["FinancialYearId"].ToString();
            status = true;
        }

        return status;
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {

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
}
