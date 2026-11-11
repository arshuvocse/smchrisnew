using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Org.BouncyCastle.Ocsp;

public partial class Appraisal_BSCOKRDetailsView : System.Web.UI.Page
{
  

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        //ButtonVisible();
        HttpContext.Current.Session["qEmpId"] = null;
        HttpContext.Current.Session["qfinYear"] = null;

        string empInfoIdStr = Request.QueryString["EmpInfoId"] ?? Request.QueryString["empInfoId"];
        string finYearStr = Request.QueryString["financialYearId"] ?? Request.QueryString["FinancialYearId"] ?? Request.QueryString["finYear"];

        if (!string.IsNullOrEmpty(empInfoIdStr))
        {
            int empId;
            if (int.TryParse(empInfoIdStr, out empId))
            {
                HttpContext.Current.Session["qEmpId"] = empId;
            }
        }

        if (!string.IsNullOrEmpty(finYearStr))
        {
            int finYear;
            if (int.TryParse(finYearStr, out finYear))
            {
                HttpContext.Current.Session["qfinYear"] = finYear;
            }
        }
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Edit")
            {
                //editButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else { Response.Redirect("BSCKPIApproval.aspx"); }
    }

    [WebMethod]
    public static object GetEmpinfo(int empInfoId = 0)
    {
        var result = new Dictionary<string, string>();
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        DataTable dtEmp = _appPartA.GetEmployeeDetails(targetEmpId);
        if (dtEmp.Rows.Count > 0)
        {
            result["CompanyId"] = dtEmp.Rows[0]["CompanyId"].ToString().Trim();
            result["EmpName"] = dtEmp.Rows[0]["EmpName"].ToString().Trim();
            result["EmpMasterCode"] = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
            result["CompanyName"] = dtEmp.Rows[0]["CompanyName"].ToString().Trim();
            result["DivisionName"] = dtEmp.Rows[0]["DivisionName"].ToString().Trim();
            result["DivisionWingName"] = dtEmp.Rows[0]["DivisionWingName"].ToString().Trim();
            result["DepartmentName"] = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
            result["SectionName"] = dtEmp.Rows[0]["SectionName"].ToString().Trim();
            result["SubSectionName"] = dtEmp.Rows[0]["SubSectionName"].ToString().Trim();
            result["Designation"] = dtEmp.Rows[0]["Designation"].ToString().Trim();
            try
            {
                result["DateOfJoin"] = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            }
            catch { }
            result["Location"] = dtEmp.Rows[0]["Location"].ToString().Trim();
            result["SalaryLocation"] = dtEmp.Rows[0]["SalaryLocation"].ToString().Trim();
            result["ReportingToName"] = dtEmp.Rows[0]["ReportingToName"].ToString().Trim();
            result["EmpInfoId"] = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();
        }

        return result;
    }

    protected void gv_AppraisalFunc_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gv_AppraisalFunc1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    [WebMethod]
    public static List<FuncData> GetFuncDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<FuncData> funcDataList = new List<FuncData>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);
            DataTable dt2 = _appPartA.GetAppraisalSelfDetails(mid);

            foreach (DataRow row in dt2.Rows)
            {
                decimal weight = 0;
                try
                {
                    if (row.Table.Columns.Contains("KpiWeight") && !row.IsNull("KpiWeight"))
                    {
                        weight = Convert.ToDecimal(row["KpiWeight"]);
                    }
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting KpiWeight: " + row["KpiWeight"] + " - " + ex.Message);
                }

                FuncData funcData = new FuncData();

                try
                {
                    funcData.Dimension = row.Table.Columns.Contains("Dimension") ? row["Dimension"].ToString() : string.Empty;
                }
                catch { }

                try
                {
                    funcData.DimensionStr = row.Table.Columns.Contains("DimensionStr") && !row.IsNull("DimensionStr") ? row["DimensionStr"].ToString() : string.Empty;
                }
                catch { }

                try
                {
                    funcData.ObjectiveGoal = row.Table.Columns.Contains("ObjectiveGoal") ? row["ObjectiveGoal"].ToString() : string.Empty;
                }
                catch { }

                try
                {
                    funcData.KPIMeasure = row.Table.Columns.Contains("KPIMeasure") ? row["KPIMeasure"].ToString() : string.Empty;
                }
                catch { }

                funcData.KpiWeight = weight;

                try
                {
                    funcData.Initiatives = row.Table.Columns.Contains("Initiatives") ? row["Initiatives"].ToString() : string.Empty;
                }
                catch { }

                try
                {
                    funcData.Deadline = row.Table.Columns.Contains("Deadline") && !row.IsNull("Deadline") ? (DateTime?)row["Deadline"] : null;
                }
                catch { }

                try
                {
                    funcData.deadlineDate = row.Table.Columns.Contains("deadlineDate") && !row.IsNull("deadlineDate") ? row["deadlineDate"].ToString() : string.Empty;
                }
                catch { }

                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static List<FuncData> GetAppraisalFuncDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<FuncData> funcDataList = new List<FuncData>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);
            DataTable dt2 = _appPartA.GetAppraisalfDetailsFromSup(mid);

            foreach (DataRow row in dt2.Rows)
            {
                decimal weight = 0;
                try
                {
                    if (row.Table.Columns.Contains("KpiWeight") && !row.IsNull("KpiWeight"))
                    {
                        weight = Convert.ToDecimal(row["KpiWeight"]);
                    }
                }
                catch { }

                FuncData funcData = new FuncData();
                funcData.Dimension = row.Table.Columns.Contains("Dimension") ? row["Dimension"].ToString() : string.Empty;
                funcData.DimensionStr = row.Table.Columns.Contains("DimensionStr") ? row["DimensionStr"].ToString() : string.Empty;
                funcData.ObjectiveGoal = row.Table.Columns.Contains("ObjectiveGoal") ? row["ObjectiveGoal"].ToString() : string.Empty;
                funcData.KPIMeasure = row.Table.Columns.Contains("KPIMeasure") ? row["KPIMeasure"].ToString() : string.Empty;
                funcData.KpiWeight = weight;
                funcData.Initiatives = row.Table.Columns.Contains("Initiatives") ? row["Initiatives"].ToString() : string.Empty;
                funcData.deadlineDate = row.Table.Columns.Contains("Deadline") ? row["Deadline"].ToString() : string.Empty;
                funcData.MidYearStatus = row.Table.Columns.Contains("MidYearStatus") ? row["MidYearStatus"].ToString() : string.Empty;
                funcData.ResultYearEnd = row.Table.Columns.Contains("ResultYearEnd") ? row["ResultYearEnd"].ToString() : string.Empty;
                funcData.SelfMark = row.Table.Columns.Contains("SelfMark") ? row["SelfMark"].ToString() : string.Empty;
                funcData.SupervisorMark = row.Table.Columns.Contains("SupervisorMark") ? row["SupervisorMark"].ToString() : string.Empty;

                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static List<FuncData> GetMidAppraisalFuncDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetMidApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<FuncData> funcDataList = new List<FuncData>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);
            DataTable dt2 = _appPartA.GetMidAppraisalfDetailsFromSup(mid);

            foreach (DataRow row in dt2.Rows)
            {
                decimal weight = 0;
                try
                {
                    if (row.Table.Columns.Contains("KpiWeight") && !row.IsNull("KpiWeight"))
                    {
                        weight = Convert.ToDecimal(row["KpiWeight"]);
                    }
                }
                catch { }

                FuncData funcData = new FuncData();
                funcData.Dimension = row.Table.Columns.Contains("Dimension") ? row["Dimension"].ToString() : string.Empty;
                funcData.DimensionStr = row.Table.Columns.Contains("DimensionStr") ? row["DimensionStr"].ToString() : string.Empty;
                funcData.ObjectiveGoal = row.Table.Columns.Contains("ObjectiveGoal") ? row["ObjectiveGoal"].ToString() : string.Empty;
                funcData.KPIMeasure = row.Table.Columns.Contains("KPIMeasure") ? row["KPIMeasure"].ToString() : string.Empty;
                funcData.KpiWeight = weight;
                funcData.Initiatives = row.Table.Columns.Contains("Initiatives") ? row["Initiatives"].ToString() : string.Empty;
                funcData.deadlineDate = row.Table.Columns.Contains("Deadline") ? row["Deadline"].ToString() : string.Empty;
                funcData.MidYearStatus = row.Table.Columns.Contains("MidYearStatus") ? row["MidYearStatus"].ToString() : string.Empty;
                funcData.ResultYearEnd = row.Table.Columns.Contains("ResultYearEnd") ? row["ResultYearEnd"].ToString() : string.Empty;
                funcData.SelfMark = row.Table.Columns.Contains("SelfMark") ? row["SelfMark"].ToString() : string.Empty;
                funcData.SupervisorMark = row.Table.Columns.Contains("SupervisorMark") ? row["SupervisorMark"].ToString() : string.Empty;

                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static List<PartBData> GetPartBDataList(int empInfoId = 0, int financialYearId = 0)
    {
        List<PartBData> funcDataList = new List<PartBData>();
        try
        {
            BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

            int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
            int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

            DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
            if (dtaa != null && dtaa.Rows.Count > 0 && dtaa.Rows[0][0] != DBNull.Value)
            {
                int mid = Convert.ToInt32(dtaa.Rows[0][0]);
                DataTable dt2 = _appPartA.GetAppraisalSelfB(mid);

                if (dt2 != null)
                {
                    foreach (DataRow row in dt2.Rows)
                    {
                        PartBData funcData = new PartBData
                        {
                            SkillInfo = row.Table.Columns.Contains("SkillInfo") && !row.IsNull("SkillInfo") ? row["SkillInfo"].ToString() : string.Empty,
                            SkillType = row.Table.Columns.Contains("SkillType") && !row.IsNull("SkillType") ? row["SkillType"].ToString() : string.Empty,
                            SupportingEmp = row.Table.Columns.Contains("SupportingEmp") && !row.IsNull("SupportingEmp") ? row["SupportingEmp"].ToString() : string.Empty,
                            Score = row.Table.Columns.Contains("Score") && !row.IsNull("Score") ? Convert.ToDecimal(row["Score"]) : 0,
                            SetScore = row.Table.Columns.Contains("SetScore") && !row.IsNull("SetScore") ? Convert.ToDecimal(row["SetScore"]) : 0
                        };

                        funcDataList.Add(funcData);
                    }
                }
            }
        }
        catch { }
        return funcDataList;
    }

    [WebMethod]
    public static List<PartBData> GetAppPartBDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<PartBData> funcDataList = new List<PartBData>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);
            DataTable dt2 = _appPartA.GetAppraisalPartB(mid);

            foreach (DataRow row in dt2.Rows)
            {
                PartBData funcData = new PartBData
                {
                    SkillInfo = row.Table.Columns.Contains("SkillInfo") && !row.IsNull("SkillInfo") ? row["SkillInfo"].ToString() : string.Empty,
                    SupportingEmp = row.Table.Columns.Contains("SupportingEmp") && !row.IsNull("SupportingEmp") ? row["SupportingEmp"].ToString() : string.Empty,
                    Score = row.Table.Columns.Contains("Score") && !row.IsNull("Score") ? Convert.ToDecimal(row["Score"]) : 0,
                    SetScore = row.Table.Columns.Contains("SetScore") && !row.IsNull("SetScore") ? Convert.ToDecimal(row["SetScore"]) : 0,
                    SelfScore = row.Table.Columns.Contains("SelfScore") && !row.IsNull("SelfScore") ? Convert.ToDecimal(row["SelfScore"]) : 0,
                    SupervisorScore = row.Table.Columns.Contains("SupervisorScore") && !row.IsNull("SupervisorScore") ? Convert.ToDecimal(row["SupervisorScore"]) : 0
                };

                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static List<PartBData> GetMidAppPartBDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<PartBData> funcDataList = new List<PartBData>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);
            DataTable dt2 = _appPartA.GetMidAppraisalPartB(mid);

            foreach (DataRow row in dt2.Rows)
            {
                PartBData funcData = new PartBData
                {
                    SkillInfo = row.Table.Columns.Contains("SkillInfo") && !row.IsNull("SkillInfo") ? row["SkillInfo"].ToString() : string.Empty,
                    SupportingEmp = row.Table.Columns.Contains("SupportingEmp") && !row.IsNull("SupportingEmp") ? row["SupportingEmp"].ToString() : string.Empty,
                    Score = row.Table.Columns.Contains("Score") && !row.IsNull("Score") ? Convert.ToDecimal(row["Score"]) : 0,
                    SetScore = row.Table.Columns.Contains("SetScore") && !row.IsNull("SetScore") ? Convert.ToDecimal(row["SetScore"]) : 0,
                    SelfScore = row.Table.Columns.Contains("SelfScore") && !row.IsNull("SelfScore") ? Convert.ToDecimal(row["SelfScore"]) : 0,
                    SupervisorScore = row.Table.Columns.Contains("SupervisorScore") && !row.IsNull("SupervisorScore") ? Convert.ToDecimal(row["SupervisorScore"]) : 0
                };

                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static object GetAppraisalScores(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][1]);
            DataTable dtfinal = _appPartA.GetFinalMark(mid);

            decimal partA = 0;
            decimal partB = 0;
            try
            {
                partA = Convert.ToDecimal(dtfinal.Rows[0]["SupScoreFunc"].ToString());
                partB = Convert.ToDecimal(dtfinal.Rows[0]["SupScoreBehave"].ToString());
            }
            catch { }
            partA = Convert.ToDecimal(partA) * Convert.ToDecimal(0.75);

            decimal total = partA + partB;
            string status;

            if (total <= 60) status = "Poor";
            else if (total <= 75) status = "Average";
            else if (total <= 80) status = "Good";
            else if (total <= 90) status = "Excellent";
            else status = "Outstanding";

            return new
            {
                success = true,
                partA = partA.ToString("F"),
                partB = partB.ToString("F"),
                total = total.ToString("F"),
                status = status
            };
        }
        return null;
    }

    [WebMethod]
    public static object GetAppraisalFinalStatus(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        OKRBSCAppraisalPartBDAL _appPartAdd = new OKRBSCAppraisalPartBDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][1]);
            DataTable dt45 = _appPartAdd.GetAppraiSalFinalStatus(mid);

            if (dt45.Rows.Count > 0)
            {
                var result = new
                {
                    GeneralIncrement = Convert.ToBoolean(dt45.Rows[0]["GeneralIncrement"]),
                    SpecialIncrement = Convert.ToBoolean(dt45.Rows[0]["SpecialIncrement"]),
                    IsPromotion = Convert.ToBoolean(dt45.Rows[0]["IsPromotion"]),
                    Pip = !string.IsNullOrEmpty(dt45.Rows[0]["Pip"].ToString()) && Convert.ToBoolean(dt45.Rows[0]["Pip"]),
                    Other = !string.IsNullOrEmpty(dt45.Rows[0]["Other"].ToString()) && Convert.ToBoolean(dt45.Rows[0]["Other"]),
                    SpecialStep = dt45.Rows[0]["SpecialStep"].ToString(),
                    Note = dt45.Rows[0]["Note"].ToString(),
                    Justification = dt45.Rows[0]["Justification"].ToString(),
                    DocumentLink = dt45.Rows[0]["DocumentLink"].ToString(),
                    FileName = dt45.Rows[0]["FileName"].ToString()
                };
                return result;
            }
        }
        return null;
    }

    [WebMethod]
    public static List<TrainingNeedsData> GetAppraisalTrainingList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<TrainingNeedsData> funcDataList = new List<TrainingNeedsData>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][1]);
            DataTable dt2 = _appPartA.GetAppraisalTraining(mid);

            foreach (DataRow row in dt2.Rows)
            {
                TrainingNeedsData funcData = new TrainingNeedsData
                {
                    TrainingNeeds = row["TrainingNeeds"].ToString(),
                    Quater = row["Quater"].ToString()
                };

                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static List<ApprovalBSCDAO> GetApprovalDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : (HttpContext.Current.Session["FinancialYearId"] != null ? Convert.ToInt32(HttpContext.Current.Session["FinancialYearId"]) : 0));

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<ApprovalBSCDAO> funcDataList = new List<ApprovalBSCDAO>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);
            DataTable dt2 = _appPartA.GetApproveLogBySelfMaster(mid);

            foreach (DataRow row in dt2.Rows)
            {
                ApprovalBSCDAO funcData = new ApprovalBSCDAO
                {
                    SerialNumber = row.Table.Columns.Contains("SerialNumber") ? row["SerialNumber"].ToString() : string.Empty,
                    Employee = row["Employee"].ToString(),
                    Comments = row["PreviousVersion"].ToString(),
                    ApprovalDate = row["EntryDate"].ToString(),
                    ApprovalStatusText = row["ApproveStatus"].ToString(),
                };

                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static List<string> GetKPIBehaviourNames(string type)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        DataTable dtSkill = _appPartA.getKPIBehaviourName(type);
        List<string> skills = new List<string>();
        foreach (DataRow row in dtSkill.Rows)
        {
            skills.Add(row["KPIBehaviourName"].ToString());
        }
        return skills;
    }

    [WebMethod]
    public static string SaveInfo(string comments, string approvalStatus, int empInfoId = 0, int financialYearId = 0)
    {
        try
        {
            string _message = "";
            bool result = SaveDataToDatabase(comments, approvalStatus, _message);

            if (result)
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    result = true,
                    message = _message
                });
            }
            else
            {
                return new JavaScriptSerializer().Serialize(new
                {
                    result = false,
                    message = _message
                });
            }
        }
        catch (Exception ex)
        {
            return new JavaScriptSerializer().Serialize(new
            {
                result = false,
                message = ex.Message
            });
        }
    }




    [WebMethod]
    public static List<RadioOption> RadioTextValue()
    {
        string filepath = "";
        if (HttpContext.Current.Session["AppPage"] != null)
        {
            filepath = HttpContext.Current.Session["AppPage"].ToString();
        }

        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        DataTable dtdata = _appPartA.GetSupervisorEmployeeAppId(HttpContext.Current.Session["EmpInfoId"].ToString(), HttpContext.Current.Session["qEmpId"].ToString());

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;

        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
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
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        List<RadioOption> radioOptions = new List<RadioOption>();
        foreach (DataRow row in aDataTable.Rows)
        {
            radioOptions.Add(new RadioOption
            {
                Text = row["Text"].ToString(),
                Value = row["Value"].ToString(),
                Enabled = true // Default to enabled
            });
        }

        try
        {
            if (HttpContext.Current.Session["ForEmpInfoId"].ToString() == HttpContext.Current.Session["EmpInfoId"].ToString())
            {
                if (radioOptions.Count > 1)
                {
                    radioOptions[1].Enabled = false; // Disable the second radio button
                }
            }
        }
        catch (Exception)
        {
            // Handle exception if needed
        }

        return radioOptions;
    }



    public class RadioOption
{
    public string Text { get; set; }
    public string Value { get; set; }
        public bool Enabled { get; set; }
    }

// Example method to simulate saving data to a database
private static bool SaveDataToDatabase(string comments, string approvalStatus, string _message)
    {
        try
        {
              BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
                AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();
            SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();
            DataTable dtFinalApprovalSubmit = new DataTable();
            DataTable dtSuppervisorSubmit = new DataTable();
            int FinalApproveCount = 0;

            DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(HttpContext.Current.Session["qEmpId"].ToString());


            DataTable dtempdataSup = _appDashboard.GetEmpInfo(" WHERE ReportingEmpId is not null and  EmpInfoId='" + HttpContext.Current.Session["qEmpId"].ToString() + "'");


            String ddd = "";
            try
            {
                ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
            }
            catch (Exception)
            {

                //throw;
            }

            if (ddd == "True")
            {


            }
            else if (dtempdataSup.Rows.Count > 0)
            {


                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("EmpMasterCode");
                //DataRow dataRow = null;
                //dataRow = aDataTable.NewRow();
                //dataRow["EmpInfoId"] = "0";
                //dataRow["EmpName"] = "Please Select an Employee.....";
                //dataRow["EmpMasterCode"] = "";
                //aDataTable.Rows.Add(dataRow);
                appDal.ReportingEmpData(HttpContext.Current.Session["qEmpId"].ToString(), aDataTable);


                dtSuppervisorSubmit = aDataTable;

                for (int i = 0; i < dtSuppervisorSubmit.Rows.Count; i++)
                {

                    dtFinalApprovalSubmit = _appPartA.GetFinalApproveByEmpId(HttpContext.Current.Session["qEmpId"].ToString(), dtSuppervisorSubmit.Rows[i]["EmpInfoId"].ToString());
                    if (dtFinalApprovalSubmit.Rows.Count > 0)
                    {
                        FinalApproveCount = FinalApproveCount + 1;
                    }

                }


            }
            string mid = "0";
           DataTable dtdata = _appDashboard.GetSupervisorEmployeeAppIdCheck(HttpContext.Current.Session["qEmpId"].ToString());

            DataTable dtaaMas = _appPartA.GetApprsaisalSelfByEmpFinYear(Convert.ToInt32(HttpContext.Current.Session["qEmpId"].ToString()), Convert.ToInt32(HttpContext.Current.Session["qfinYear"].ToString()));
            List<FuncData> funcDataList = new List<FuncData>();
            if (dtaaMas.Rows.Count > 0)
            {

                  mid =  (dtaaMas.Rows[0][0].ToString());

            }

                if (dtdata.Rows.Count > 0 && ddd == "True")
            {
                AppraisalSelfAppLogDAO aMasterApp = new AppraisalSelfAppLogDAO();
                aMasterApp.BSCAppraisalSelfMasterId
                    = Convert.ToInt32(mid.ToString());

                if (approvalStatus == "Review")
                {
                    aMasterApp.ActionStatus = "Review";

                    bool status = _appPartA.UpdateContractural(aMasterApp);
                    if (status)
                    {
                        if (aMasterApp.ActionStatus == "Review")
                        {
                            if (status)
                            {
                                

                                AppraisalMaster aMaster = new AppraisalMaster();

                                aMaster.BSCAppraisalMasterId =   Convert.ToInt32(mid);
                                aMaster.EmpInfoId = Convert.ToInt32(HttpContext.Current.Session["qEmpId"].ToString());
                                aMaster.FinancialYearId = int.Parse(HttpContext.Current.Session["qfinYear"].ToString());
                              


                                bool result = false;
                                
                                    int pk = _appPartA.SaveAppraisalSelfMasterforSupper(aMaster, HttpContext.Current.Session["UserId"].ToString());
                                    if (pk > 0)
                                    {







                                    DataTable dtaa =
                                            _appPartA.GetCheckApprisalAlreadyExist(Convert.ToInt32(mid));
                                        if (dtaa.Rows.Count > 0)
                                        {
                                            int BSCAppraisalMasterId = Convert.ToInt32(dtaa.Rows[0]["BSCAppraisalMasterId"].ToString());

                                            _appPartA.DeleteAppraisalSetupNew(Convert.ToInt32(BSCAppraisalMasterId));

                                        }


                                        //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                                        AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                        {
                                            ActionStatus = approvalStatus,
                                            ApproveDate = DateTime.Now,
                                            ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                                            PreEmpInfoId = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),
                                            ForEmpInfoId = 0,
                                            BSCAppraisalSelfMasterId = aMasterApp.BSCAppraisalSelfMasterId,
                                            Comments = comments,
                                            CommentsEMP = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),

                                        };
                                        int id = _appPartA.SaveEmpAppLog(appLogDao);
                                        _appPartA.SaveAppraisalMasterFromAppraisalSelf(
                                            aMasterApp.BSCAppraisalSelfMasterId.ToString());
                                    _message = "Operation Successful...";
                                    return true;
                                   
                                }
                                


                            }
                        }
                    }
                }
                else
                {

                    aMasterApp.ActionStatus = "Approved";
                    bool status = _appPartA.UpdateContractural(aMasterApp);

                    if (status)
                    {
                        //                        if (chkFunc.Checked && chkBehavioral.Checked)
                        //                        {
                        //                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                        //                                @"  <br/> Dear Sir, <br/>
                        //Your KPI has been modified by Your Supervisor. <br/><br/>
                        //please login with the below link.<br/><br/>   http://182.160.103.234:8088/
                        //<br/> Thank You.");

                        //                        }


                        //                        else if (chkFunc.Checked && chkBehavioral.Checked == false)
                        //                        {
                        //                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                        //                                @"  <br/> Dear Sir, <br/>
                        //Your KPI has been modified by Your Supervisor. <br/><br/>
                        //please login with the below link.<br/><br/>   http://182.160.103.234:8088/
                        //<br/> Thank You.");

                        //                        }


                        //                        else if (chkFunc.Checked == false && chkBehavioral.Checked)
                        //                        {
                        //                            SenMailForApprved(Convert.ToInt32(Request.QueryString["EmpInfoId"]), " KPI Setup modified ",
                        //                                @"  <br/> Dear Sir, <br/>
                        //Your KPI has been modified by Your Supervisor. <br/><br/>
                        //please login with the below link.<br/><br/>   http://182.160.103.234:8088/
                        //<br/> Thank You.");

                        //                        }




                     

                        AppraisalMaster aMaster = new AppraisalMaster();

                        aMaster.BSCAppraisalMasterId = mid == "" ? 0 : Convert.ToInt32(mid);
                        aMaster.EmpInfoId = Convert.ToInt32(HttpContext.Current.Session["qEmpId"].ToString());
                        aMaster.FinancialYearId = int.Parse(HttpContext.Current.Session["qfinYear"].ToString());


                        bool result = false;
                       
                            int pk = 1;
                            if (pk > 0)
                            {








                                //result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                                //result = SaveAppraisalSelfB(pk);





                                DataTable dtempdata = _appPartA.GetEmpInfoPrevious(HttpContext.Current.Session["EmpInfoid"].ToString(),
                                    mid);
                                DataTable dtempdata2 =
                                    _appPartA.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(),
                                        mid);

                                DataTable dtempdata2empid = _appDashboard.GetEmpIdfromKPIInfo(
                     mid);

                                if (dtempdata2empid.Rows.Count > 0)
                                {
                                    AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                    {
                                        ActionStatus = "Verified",
                                        ApproveDate = DateTime.Now,
                                        ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                                        PreEmpInfoId = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),
                                        ForEmpInfoId = Convert.ToInt32(dtempdata2empid.Rows[0]["EmpInfoId"].ToString()),
                                        BSCAppraisalSelfMasterId = aMasterApp.BSCAppraisalSelfMasterId,
                                        Comments = comments,
                                        CommentsEMP = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),

                                    };
                                    _appPartA.UpdateAppLog("Review", HttpContext.Current.Session["AppLogId"].ToString());
                                    int id = _appPartA.SaveEmpAppLog(appLogDao);



                                //                                    SenMailForApprved(appLogDao.ForEmpInfoId, " KPI Setup Approval ",
                                //                                        @"  <br/> Dear Sir, <br/>
                                //Review your KPI. <br/><br/>
                                //please login with the below link.<br/><br/>   http://182.160.103.234:8088/
                                //<br/> Thank You.");


                                _message = "Operation Successful...";
                                return true;
                            }
                            }
                         


                    }
                }
            }
            else if (dtdata.Rows.Count > 0 && FinalApproveCount == 1 && dtSuppervisorSubmit.Rows.Count > 0)
            {
                AppraisalSelfAppLogDAO aMasterApp = new AppraisalSelfAppLogDAO();
                aMasterApp.BSCAppraisalSelfMasterId
                    = Convert.ToInt32(mid);




                aMasterApp.ActionStatus = approvalStatus;
                bool status = _appPartA.UpdateContractural(aMasterApp);





                if (status)
                {
                     

                    AppraisalMaster aMaster = new AppraisalMaster();

                    aMaster.BSCAppraisalMasterId = mid == "" ? 0 : Convert.ToInt32(mid);
                    aMaster.EmpInfoId = Convert.ToInt32(HttpContext.Current.Session["qEmpId"].ToString());
                    aMaster.FinancialYearId = int.Parse(HttpContext.Current.Session["qfinYear"].ToString());




                    bool result = false;
                    //if (functional.Count > 0)
                    //{
                    //    int pk = _appPartA.SaveAppraisalSelfMasterforSupper(aMaster, HttpContext.Current.Session["UserId"].ToString());
                    //    if (pk > 0)
                    //    {








                    //        result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                    //        result = SaveAppraisalSelfB(pk);
                    //    }
                    //}


                    if (aMasterApp.ActionStatus == "Verified")
                    {
                        DataTable dtempdata = _appPartA.GetEmpInfo(" WHERE EmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'");
                        AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                        {
                            ActionStatus =approvalStatus,
                            ApproveDate = DateTime.Now,
                            ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                            BSCAppraisalSelfMasterId = aMasterApp.BSCAppraisalSelfMasterId,
                            Comments = comments,
                            CommentsEMP = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),

                        };
                        int id = _appPartA.SaveEmpAppLog(appLogDao);
                    }
                    else if (aMasterApp.ActionStatus == "Approved")
                    {

                        DataTable dtaa = _appPartA.GetCheckApprisalAlreadyExist(Convert.ToInt32(mid));
                        if (dtaa.Rows.Count > 0)
                        {
                            int BSCAppraisalMasterId = Convert.ToInt32(dtaa.Rows[0]["BSCAppraisalMasterId"].ToString());

                            //_appPartA.DeleteAppraisalSetupNew(Convert.ToInt32(BSCAppraisalMasterId));

                        }


                        //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                        AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                        {
                            ActionStatus = approvalStatus,
                            ApproveDate = DateTime.Now,
                            ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),
                            ForEmpInfoId = 0,
                            BSCAppraisalSelfMasterId = aMasterApp.BSCAppraisalSelfMasterId,
                            Comments = comments,
                            CommentsEMP = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),

                        };
                        int id = _appPartA.SaveEmpAppLog(appLogDao);
                        _appPartA.SaveAppraisalMasterFromAppraisalSelf(aMasterApp.BSCAppraisalSelfMasterId.ToString());

//                        SenMailForApprved(appLogDao.ForEmpInfoId, " KPI Setup Approval ", @"  <br/> Dear Sir, <br/>
//An Employee's KPI is waiting for your approval. <br/><br/>
//please login with the below link.<br/><br/>   http://182.160.103.234:8088/
//<br/> Thank You.");


                    }
                    else if (aMasterApp.ActionStatus == "Review")
                    {
                        DataTable dtempdata = _appPartA.GetEmpInfoPrevious(HttpContext.Current.Session["EmpInfoid"].ToString(), mid);
                        DataTable dtempdata2 = _appPartA.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), mid);

                        if (dtempdata2.Rows.Count > 0)
                        {
                            AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                            {
                                ActionStatus = "Verified",
                                ApproveDate = DateTime.Now,
                                ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                                PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                                ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                                BSCAppraisalSelfMasterId = aMasterApp.BSCAppraisalSelfMasterId,
                                Comments = comments,
                                CommentsEMP = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),

                            };
                            _appPartA.UpdateAppLog("Review", HttpContext.Current.Session["AppLogId"].ToString());
                            int id = _appPartA.SaveEmpAppLog(appLogDao);



//                            SenMailForApprved(appLogDao.ForEmpInfoId, " KPI Setup Approval ", @"  <br/> Dear Sir, <br/>
//Review your KPI. <br/><br/>
//please login with the below link.<br/><br/>   http://182.160.103.234:8088/
//<br/> Thank You.");
                        }
                        else
                        {
                             
                            _message = "Please select Approval Status to Approved !!!";
                            return false;
                        }
                    }


                }
                HttpContext.Current.Session["AppLogId"] = null;
              
                _message = "Operation Successful...";
                return true;
            }
            else
            {
                _message = "Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!";
                return false;
                 
            }

            //bool result = false;
            //result = _appPartA.SaveAppraisalSelfApprove(Convert.ToInt32(id_mastetID.Value.Trim()), "Approved",
            //    Session["LoginName"].ToString(), txtRemarks.Text.Trim());
            //result = SaveApprovedAppraisal();
            //if (result == true)
            //{



            //   // result = SaveAppraisalSelfApprove()


            //    ScriptManager.RegisterStartupScript(this, this.GetType(),
            //       "alert",
            //       "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
            //       true);
            //}
            //else
            //{
            //    AlertMessageBoxShow("Operation Failed");
            //}
        }
        catch (Exception)
        {
            _message= "This Employees final Approval Person has not been set yet. Please contact with HR Department.!!!";
            return   false;
            //throw;
        }
        return true;
    }
}