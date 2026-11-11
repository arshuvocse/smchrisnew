using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
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

public partial class Appraisal_BSCSelfFunctionalApproval : System.Web.UI.Page
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

        ButtonVisible();
        HttpContext.Current.Session["qEmpId"] = null;
        HttpContext.Current.Session["qfinYear"] = null;
        if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
        {

            HttpContext.Current.Session["qEmpId"] = int.Parse(Request.QueryString["EmpInfoId"]);
            HttpContext.Current.Session["qfinYear"] = int.Parse(Request.QueryString["financialYearId"]);

        }


    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null || !string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
        {
            Session["Status"] = null;
        }
        else { Response.Redirect("BSCKPIApproval.aspx"); }
    }

    [WebMethod]
    public static object GetEmpinfo(int empInfoId = 0)
    {
        // Your existing logic here, but remove the usage of UI controls
        // since this is a static method now.
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
            catch
            {

            }
            result["Location"] = dtEmp.Rows[0]["Location"].ToString().Trim();
            result["SalaryLocation"] = dtEmp.Rows[0]["SalaryLocation"].ToString().Trim();
            result["ReportingToName"] = dtEmp.Rows[0]["ReportingToName"].ToString().Trim();
            result["EmpInfoId"] = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();
        }

        return result;
    }

    [WebMethod]
    public static List<FuncData> GetFuncDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId;
        if (targetEmpId <= 0 && HttpContext.Current.Session["EmpInfoId"] != null)
        {
            int.TryParse(HttpContext.Current.Session["EmpInfoId"].ToString(), out targetEmpId);
        }

        int targetFinYearId = financialYearId;

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
                    // You can decide whether to continue or rethrow the exception here
                   
                }

                FuncData funcData = new FuncData();

                try
                {
                    funcData.Dimension = row.Table.Columns.Contains("Dimension") ? row["Dimension"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Dimension: " + row["Dimension"] + " - " + ex.Message);
                    
                }
                try
                {
                    funcData.DimensionStr = row.Table.Columns.Contains("DimensionStr") ? row["DimensionStr"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Dimension: " + row["DimensionStr"] + " - " + ex.Message);
                    
                }

                try
                {
                    funcData.ObjectiveGoal = row.Table.Columns.Contains("ObjectiveGoal") ? row["ObjectiveGoal"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting ObjectiveGoal: " + row["ObjectiveGoal"] + " - " + ex.Message);
                   
                }

                try
                {
                    funcData.KPIMeasure = row.Table.Columns.Contains("KPIMeasure") ? row["KPIMeasure"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting KPIMeasure: " + row["KPIMeasure"] + " - " + ex.Message);
               
                }

                try
                {
                    funcData.KpiWeight = weight;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error assigning KpiWeight: " + weight + " - " + ex.Message);
                     
                }

                try
                {
                    funcData.Initiatives = row.Table.Columns.Contains("Initiatives") ? row["Initiatives"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Initiatives: " + row["Initiatives"] + " - " + ex.Message);
                    
                }

                try
                {
                    funcData.Deadline = row.Table.Columns.Contains("Deadline") && !row.IsNull("Deadline") ? (DateTime?)row["Deadline"] : null;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                
                try
                {
                    funcData.deadlineDate = row.Table.Columns.Contains("deadlineDate") ? row["deadlineDate"].ToString() : string.Empty;

                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }

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
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

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
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting KpiWeight: " + row["KpiWeight"] + " - " + ex.Message);
                    // You can decide whether to continue or rethrow the exception here
                   
                }

                FuncData funcData = new FuncData();

                try
                {
                    funcData.Dimension = row.Table.Columns.Contains("Dimension") ? row["Dimension"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Dimension: " + row["Dimension"] + " - " + ex.Message);
                    
                }  
                
                try
                {
                    funcData.DimensionStr = row.Table.Columns.Contains("DimensionStr") ? row["DimensionStr"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Dimension: " + row["Dimension"] + " - " + ex.Message);
                    
                }

                try
                {
                    funcData.ObjectiveGoal = row.Table.Columns.Contains("ObjectiveGoal") ? row["ObjectiveGoal"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting ObjectiveGoal: " + row["ObjectiveGoal"] + " - " + ex.Message);
                   
                }

                try
                {
                    funcData.KPIMeasure = row.Table.Columns.Contains("KPIMeasure") ? row["KPIMeasure"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting KPIMeasure: " + row["KPIMeasure"] + " - " + ex.Message);
               
                }

                try
                {
                    funcData.KpiWeight = weight;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error assigning KpiWeight: " + weight + " - " + ex.Message);
                     
                }

                try
                {
                    funcData.Initiatives = row.Table.Columns.Contains("Initiatives") ? row["Initiatives"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Initiatives: " + row["Initiatives"] + " - " + ex.Message);
                    
                }

                try
                {
                    funcData.deadlineDate =   row.Table.Columns.Contains("Deadline") ? row["Deadline"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                 try
                {
                    funcData.MidYearStatus =   row.Table.Columns.Contains("MidYearStatus") ? row["MidYearStatus"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                
                 try
                {
                    funcData.ResultYearEnd =   row.Table.Columns.Contains("ResultYearEnd") ? row["ResultYearEnd"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                
                
                 try
                {
                    funcData.SelfMark =   row.Table.Columns.Contains("SelfMark") ? row["SelfMark"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                
                
                 try
                {
                    funcData.SupervisorMark =   row.Table.Columns.Contains("SupervisorMark") ? row["SupervisorMark"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }

                funcDataList.Add(funcData);
            }



        }
        return funcDataList;
    }

    [WebMethod]
    public static List<FuncData> GetMidAppraisalFuncDataList()
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        // Assuming _appPartA is an instance of a class with the method GetAppraisalSelfDetails
        DataTable dtaa = _appPartA.GetMidApprsaisalSelfByEmpFinYear(Convert.ToInt32(HttpContext.Current.Session["qEmpId"].ToString()), Convert.ToInt32(HttpContext.Current.Session["qfinYear"].ToString()));
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
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting KpiWeight: " + row["KpiWeight"] + " - " + ex.Message);
                    // You can decide whether to continue or rethrow the exception here
                   
                }

                FuncData funcData = new FuncData();

                try
                {
                    funcData.Dimension = row.Table.Columns.Contains("Dimension") ? row["Dimension"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Dimension: " + row["Dimension"] + " - " + ex.Message);
                    
                }  
                
                try
                {
                    funcData.DimensionStr = row.Table.Columns.Contains("DimensionStr") ? row["DimensionStr"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Dimension: " + row["Dimension"] + " - " + ex.Message);
                    
                }

                try
                {
                    funcData.ObjectiveGoal = row.Table.Columns.Contains("ObjectiveGoal") ? row["ObjectiveGoal"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting ObjectiveGoal: " + row["ObjectiveGoal"] + " - " + ex.Message);
                   
                }

                try
                {
                    funcData.KPIMeasure = row.Table.Columns.Contains("KPIMeasure") ? row["KPIMeasure"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting KPIMeasure: " + row["KPIMeasure"] + " - " + ex.Message);
               
                }

                try
                {
                    funcData.KpiWeight = weight;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error assigning KpiWeight: " + weight + " - " + ex.Message);
                     
                }

                try
                {
                    funcData.Initiatives = row.Table.Columns.Contains("Initiatives") ? row["Initiatives"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Initiatives: " + row["Initiatives"] + " - " + ex.Message);
                    
                }

                try
                {
                    funcData.deadlineDate =   row.Table.Columns.Contains("Deadline") ? row["Deadline"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                 try
                {
                    funcData.MidYearStatus =   row.Table.Columns.Contains("MidYearStatus") ? row["MidYearStatus"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                
                 try
                {
                    funcData.ResultYearEnd =   row.Table.Columns.Contains("ResultYearEnd") ? row["ResultYearEnd"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                
                
                 try
                {
                    funcData.SelfMark =   row.Table.Columns.Contains("SelfMark") ? row["SelfMark"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }
                
                
                 try
                {
                    funcData.SupervisorMark =   row.Table.Columns.Contains("SupervisorMark") ? row["SupervisorMark"].ToString() : string.Empty;
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Error converting Deadline: " + row["Deadline"] + " - " + ex.Message);
                     
                }

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

            int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0);
            int targetFinYearId = financialYearId;

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
        catch (Exception ex)
        {
            // Exception caught safely to prevent 500 internal server errors
        }
        return funcDataList;
    }
    
    [WebMethod]
    public static List<PartBData> GetAppPartBDataList(int empInfoId = 0, int financialYearId = 0)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

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
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

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
        OKRBSCAppraisalPartBDAL _appPartAdd = new OKRBSCAppraisalPartBDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<TrainingNeedsData> funcDataList = new List<TrainingNeedsData>();
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
            catch (Exception)
            {
                //return new { success = false, message = "" };
            }
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
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<TrainingNeedsData> funcDataList = new List<TrainingNeedsData>();
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
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

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
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

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
                    
                    SerialNumber = row["SerialNumber"].ToString(),
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
    public static string SaveInfo(string comments, string approvalStatus, int empInfoId = 0, int financialYearId = 0)
    {
        // Handle the data here
        // For example, save to database or perform business logic

        try
        {
            string _message  = "";
            // Simulate saving data
            // Example: Save to database or perform some action
              bool result = SaveDataToDatabase(comments, approvalStatus, ref _message, empInfoId, financialYearId);

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

            // Return a success message
          
        }
        catch (Exception exMsg)
        {
            // Return an error message

            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();

            try
            {
                mail.To.Add("towsifcreatrix@gmail.com");
            }
            catch (Exception)
            {

                //throw;
            }
            mail.From = new MailAddress("shuvo.creatrixbd@gmail.com");
            mail.From.User.ToString();

            mail.Sender = new System.Net.Mail.MailAddress("shuvo.creatrixbd@gmail.com");
            mail.Subject = "OKR/BSC Apprroval Issue";
            //if (txtMailCC.Text != "")
            //{
            //    MailAddress copy = new MailAddress(txtMailCC.Text);
            //    mail.CC.Add(copy);
            //}
            MailAddress copy2 = new MailAddress("shuvo.creatrixbd@gmail.com");
            mail.Bcc.Add(copy2);


            mail.Body = "OKR/BSC Approval Issue <br> Emp ID: " + HttpContext.Current.Session["qEmpId"].ToString() +
             "<br> Fin Year ID: " + HttpContext.Current.Session["qfinYear"] +
             "<br> Approver ID: " + HttpContext.Current.Session["EmpInfoId"].ToString()+

            "<br><br> <span style='color:red;'>Exception Message:</span>  " + exMsg.Message +
   "<br><br> <b><span style='color:red;'>Exception:</span></b> " + exMsg
       ;






            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            //Attach file using FileUpload Control and put the file in memory stream

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                 ("shuvo.creatrixbd@gmail.com", "glgg rjov qtig veqz");
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Port = 587;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                // showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
                //showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);
         

           

            return new JavaScriptSerializer().Serialize(new
            {
                result = false,
                message = "Something Went Wrong! Please try after sometimes!"
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
private static bool SaveDataToDatabase(string comments, string approvalStatus, ref string _message, int empInfoId = 0, int financialYearId = 0)
    {
        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

        try
        {

            HttpContext.Current.Session["AO_ApproverEmpInfoId"] = null;
              BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
                AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();
            SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();
            DataTable dtFinalApprovalSubmit = new DataTable();
            DataTable dtSuppervisorSubmit = new DataTable();
            int FinalApproveCount = 0;

            DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(targetEmpId.ToString());


            DataTable dtempdataSup = _appDashboard.GetEmpInfo(" WHERE ReportingEmpId is not null and  EmpInfoId='" + targetEmpId.ToString() + "'");


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
                appDal.ReportingEmpData(targetEmpId.ToString(), aDataTable);


                dtSuppervisorSubmit = aDataTable;

                for (int i = 0; i < dtSuppervisorSubmit.Rows.Count; i++)
                {

                    dtFinalApprovalSubmit = _appPartA.GetFinalApproveByEmpId(targetEmpId.ToString(), dtSuppervisorSubmit.Rows[i]["EmpInfoId"].ToString());
                    if (dtFinalApprovalSubmit.Rows.Count > 0)
                    {
                        FinalApproveCount = FinalApproveCount + 1;
                    }

                }


            }
            string mid = "0";
           DataTable dtdata = _appDashboard.GetSupervisorEmployeeAppIdCheck(targetEmpId.ToString());

            DataTable dtaaMas = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
            List<FuncData> funcDataList = new List<FuncData>();
            if (dtaaMas.Rows.Count > 0)
            {

                  mid =  (dtaaMas.Rows[0][0].ToString());

            }

            // Every write below (status updates, applog inserts, master-from-self insert, delete-setup insert) now runs
            // on one shared SqlConnection/SqlTransaction so a failure partway through this multi-step approval flow
            // rolls back everything instead of leaving some tables updated and others not.
            SqlTransaction transaction;
            using (SqlConnection connection = _appPartA.OpenTransactionalConnection(out transaction))
            {
                using (transaction)
                {
                    try
                    {
                        if (dtdata.Rows.Count > 0 && ddd == "True")
                        {
                            AppraisalSelfAppLogDAO aMasterApp = new AppraisalSelfAppLogDAO();
                            aMasterApp.BSCAppraisalSelfMasterId
                                = Convert.ToInt32(mid.ToString());

                            if (approvalStatus == "Review")
                            {
                                aMasterApp.ActionStatus = "Review";

                                bool status = _appPartA.UpdateContractural(connection, transaction, aMasterApp);
                                if (status)
                                {
                                    if (aMasterApp.ActionStatus == "Review")
                                    {
                                        if (status)
                                        {


                                            AppraisalMaster aMaster = new AppraisalMaster();

                                            aMaster.BSCAppraisalMasterId = Convert.ToInt32(mid);
                                            aMaster.EmpInfoId = targetEmpId;
                                            aMaster.FinancialYearId = targetFinYearId;



                                            bool result = false;

                                            int pk = _appPartA.SaveAppraisalSelfMasterforSupper(connection, transaction, aMaster, HttpContext.Current.Session["UserId"] != null ? HttpContext.Current.Session["UserId"].ToString() : "");
                                            if (pk > 0)
                                            {
                                                DataTable dtaa =
                                                        _appPartA.GetCheckApprisalAlreadyExist(Convert.ToInt32(mid));
                                                if (dtaa.Rows.Count > 0)
                                                {
                                                    int BSCAppraisalMasterId = Convert.ToInt32(dtaa.Rows[0]["BSCAppraisalMasterId"].ToString());

                                                    _appPartA.DeleteAppraisalSetupNew(connection, transaction, Convert.ToInt32(BSCAppraisalMasterId));

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
                                                    ShowStatus = "Return"
                                                };
                                                int id = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao);
                                                _appPartA.SaveAppraisalMasterFromAppraisalSelf(connection, transaction,
                                                    aMasterApp.BSCAppraisalSelfMasterId.ToString());
                                                _message = "Operation Successful...";
                                                transaction.Commit();
                                                return true;

                                            }


                                        }
                                    }
                                }
                            }
                            else
                            {

                                aMasterApp.ActionStatus = "Approved";
                                bool status = _appPartA.UpdateContractural(connection, transaction, aMasterApp);

                                if (status)
                                {
                                    AppraisalMaster aMaster = new AppraisalMaster();

                                    aMaster.BSCAppraisalMasterId = mid == "" ? 0 : Convert.ToInt32(mid);
                                    aMaster.EmpInfoId = Convert.ToInt32(HttpContext.Current.Session["qEmpId"].ToString());
                                    aMaster.FinancialYearId = int.Parse(HttpContext.Current.Session["qfinYear"].ToString());


                                    bool result = false;

                                    int pk = 1;
                                    if (pk > 0)
                                    {
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
                                                ShowStatus = "Verified"

                                            };
                                            _appPartA.UpdateAppLog(connection, transaction, "Review", HttpContext.Current.Session["AppLogId"].ToString());
                                            int id = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao);

                                            _message = "Operation Successful...";
                                            transaction.Commit();
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
                            bool status = _appPartA.UpdateContractural(connection, transaction, aMasterApp);

                            if (status)
                            {

                                AppraisalMaster aMaster = new AppraisalMaster();

                                aMaster.BSCAppraisalMasterId = mid == "" ? 0 : Convert.ToInt32(mid);
                                aMaster.EmpInfoId = Convert.ToInt32(HttpContext.Current.Session["qEmpId"].ToString());
                                aMaster.FinancialYearId = int.Parse(HttpContext.Current.Session["qfinYear"].ToString());

                                bool result = false;

                                if (aMasterApp.ActionStatus == "Verified")
                                {
                                    DataTable dtempdata = _appPartA.GetEmpInfo(" WHERE EmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'");
                                    AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO()
                                    {
                                        ActionStatus = approvalStatus,
                                        ApproveDate = DateTime.Now,
                                        ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                                        PreEmpInfoId = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),
                                        ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                                        BSCAppraisalSelfMasterId = aMasterApp.BSCAppraisalSelfMasterId,
                                        Comments = comments,
                                        CommentsEMP = Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"].ToString()),

                                        ShowStatus = "Verified"
                                    };
                                    int id = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao);
                                }
                                else if (aMasterApp.ActionStatus == "Approved")
                                {

                                    DataTable dtaa = _appPartA.GetCheckApprisalAlreadyExist(Convert.ToInt32(mid));
                                    if (dtaa.Rows.Count > 0)
                                    {
                                        int BSCAppraisalMasterId = Convert.ToInt32(dtaa.Rows[0]["BSCAppraisalMasterId"].ToString());

                                        //_appPartA.DeleteAppraisalSetupNew(connection, transaction, Convert.ToInt32(BSCAppraisalMasterId));

                                    }

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

                                        ShowStatus = approvalStatus

                                    };
                                    int id = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao);
                                    _appPartA.SaveAppraisalMasterFromAppraisalSelf(connection, transaction, aMasterApp.BSCAppraisalSelfMasterId.ToString());

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
                                            ShowStatus = "Return",
                                        };
                                        _appPartA.UpdateAppLog(connection, transaction, "Review", HttpContext.Current.Session["AppLogId"].ToString());
                                        int id = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao);
                                    }
                                    else
                                    {

                                        _message = "Please select Approval Status to Approved !!!";
                                        transaction.Rollback();
                                        return false;
                                    }
                                }


                            }
                            HttpContext.Current.Session["AppLogId"] = null;

                            _message = "Operation Successful...";
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            _message = "Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!";

                            transaction.Rollback();
                            return false;

                        }

                        // No branch above matched a commit/rollback/return path; nothing was written, so just release the transaction.
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
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
        catch (Exception exMsg)
        {
            //_message= "This Employees final Approval Person has not been set yet. Please contact with HR Department.!!!";

            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();

            try
            {
                mail.To.Add("towsifcreatrix@gmail.com");
            }
            catch (Exception)
            {

                //throw;
            }
            mail.From = new MailAddress("shuvo.creatrixbd@gmail.com");
            mail.From.User.ToString();

            mail.Sender = new System.Net.Mail.MailAddress("shuvo.creatrixbd@gmail.com");
            mail.Subject = "OKR/BSC Apprroval Issue";
            //if (txtMailCC.Text != "")
            //{
            //    MailAddress copy = new MailAddress(txtMailCC.Text);
            //    mail.CC.Add(copy);
            //}
            MailAddress copy2 = new MailAddress("shuvo.creatrixbd@gmail.com");
            mail.Bcc.Add(copy2);


            mail.Body = "OKR/BSC Approval Issue <br> Emp ID: " + HttpContext.Current.Session["qEmpId"].ToString() +
             "<br> Fin Year ID: " + HttpContext.Current.Session["qfinYear"] +
             "<br> Approver ID: " + HttpContext.Current.Session["EmpInfoId"].ToString()+

                "<br><br> <span style='color:red;'>Exception Message:</span>  " + exMsg.Message +
            "<br><br> <b><span style='color:red;'>Exception:</span></b> " + exMsg
                ;







            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            //Attach file using FileUpload Control and put the file in memory stream

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                 ("shuvo.creatrixbd@gmail.com", "glgg rjov qtig veqz");
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Port = 587;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                // showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
                //showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);


            _message = "Something Went Wrong! Please try after sometimes!";
            return   false;
            //throw;
        }
        return true;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object SaveData(List<FuncData> FuncData, List<PartBData> PartBData, string actionType, string Comments, string Frm, int empInfoId = 0, int financialYearId = 0)
    {

        bool _Result = false;
        string _msg = "";
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();
        SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();
        BASAppraisalMaster aMaster = new BASAppraisalMaster();

        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["qEmpId"] != null ? Convert.ToInt32(HttpContext.Current.Session["qEmpId"]) : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0));
        int targetFinYearId = financialYearId > 0 ? financialYearId : (HttpContext.Current.Session["qfinYear"] != null ? Convert.ToInt32(HttpContext.Current.Session["qfinYear"]) : 0);

        string mid = "0";

        DataTable dtaaMas = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);

        if (dtaaMas.Rows.Count > 0)
        {

            mid = (dtaaMas.Rows[0][0].ToString());

        }
        if (actionType == "Draft")
        {
            aMaster.BSCAppraisalSelfMasterId = Convert.ToInt32(mid.ToString());
            aMaster.EmpInfoId = targetEmpId;

            aMaster.FinancialYearId = targetFinYearId;
            int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, FuncData, PartBData, Frm, HttpContext.Current.Session["UserId"] != null ? HttpContext.Current.Session["UserId"].ToString() : "");
            if (pk > 0)
            {
                _Result = true;
                _msg = "Data Saved successfully!";
            }
        }
        




        try
        {
            // Perform the save operation here
            // You can add your database saving logic or other business logic

            // If the operation is successful, return a success message
            return new
            {
                Result = _Result,
                Message = _msg
            };
        }
        catch (Exception ex)
        {
            // If an error occurs, return an error message
            return new
            {
                Result = _Result,
                Message = "An error occurred while saving data: "
            };
        }



    }

}