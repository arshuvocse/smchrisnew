using DAL.Appraisal;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appraisal_BSCOKRSelfKPIDtls : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
          ButtonVisible();
        }


    [System.Web.Services.WebMethod]
    public static List<string> GetKPIBehaviourNames(string type)
    {
        // Assuming _appPartA is an instance of the class where you retrieve data
    
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        // Retrieve data based on the selected type
        DataTable dtSkill = _appPartA.getKPIBehaviourName(type);

        // Convert DataTable to List<string> (or another appropriate type)
        List<string> skills = new List<string>();

        foreach (DataRow row in dtSkill.Rows)
        {
            skills.Add(row["KPIBehaviourName"].ToString());
        }

        return skills;
    }

    public class SaveDataRequest
    {
        public List<FuncDataModel> FuncData { get; set; }
        public List<PartBDataModel> PartBData { get; set; }
    }

    public class FuncDataModel
    {
        public string Dimension { get; set; }
        public string ObjectiveGoal { get; set; }
        public string KPIMeasure { get; set; }
        public float KpiWeight { get; set; }
        public string Initiatives { get; set; }
        public string Deadline { get; set; } // Use string to handle ISO format
    }

    public class PartBDataModel
    {
        public string SkillInfo { get; set; }
        public string SupportingEmp { get; set; }
        public float Score { get; set; }
    }


    public void ButtonVisible()
    {
        if (string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
        {
            Response.Redirect("BSCOKRAppraisalSelfList.aspx");
        }
    }

    

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static void SaveData(SaveDataRequest requestData)
    //{
    //    // Your implementation here
    //}


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static void SaveData(AppraisalDataModel data)
    //{
    //    try
    //    {
    //        // Initialize your database context or connection here

    //            // Save FuncData
    //            foreach (var funcRow in data.FuncData)
    //            {
    //                // Create a new entity instance for FuncData
    //                var funcEntity = new FuncRowModel
    //                {
    //                    Dimension = funcRow.Dimension,
    //                    ObjectiveGoal = funcRow.ObjectiveGoal,
    //                    KPIMeasure = funcRow.KPIMeasure,
    //                    KpiWeight = funcRow.KpiWeight,
    //                    Initiatives = funcRow.Initiatives,
    //                    Deadline = funcRow.Deadline
    //                };

    //                // Add the entity to the DbContext and save changes
    //              //  dbContext.FuncDataEntities.Add(funcEntity);
    //            }

    //            // Save PartBData
    //            foreach (var partBRow in data.PartBData)
    //            {
    //                // Create a new entity instance for PartBData
    //                var partBEntity = new PartBRowModel
    //                {
    //                    SkillInfo = partBRow.SkillInfo,
    //                    SupportingEmp = partBRow.SupportingEmp,
    //                    Score = partBRow.Score
    //                };

    //                // Add the entity to the DbContext and save changes

    //            }

    //            // Save all changes to the database

    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception if necessary (e.g., using a logging framework)
    //        throw new Exception("Error saving data: " + ex.Message);
    //    }
    //}
    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object SaveData(List<FuncData> FuncData, List<PartBData> PartBData, int empInfoId, int financialYearId)
    {
        bool _Result = false;
        string _msg = "";
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        BASAppraisalMaster aMaster = new BASAppraisalMaster();

        int targetEmpId = empInfoId > 0 ? empInfoId : Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]);
        int targetFinYearId = financialYearId;

        string mid = "0";

        DataTable dtaaMas = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);

        if (dtaaMas.Rows.Count > 0)
        {
            mid = dtaaMas.Rows[0][0].ToString();
        }

        try
        {
            aMaster.BSCAppraisalSelfMasterId = Convert.ToInt32(mid);
            aMaster.EmpInfoId = targetEmpId;
            aMaster.FinancialYearId = targetFinYearId;

            string userId = HttpContext.Current.Session["UserId"] != null ? HttpContext.Current.Session["UserId"].ToString() : "";
            int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, FuncData, PartBData, "", userId);
            if (pk > 0)
            {
                _Result = true;
                _msg = "Data saved successfully!";
            }
            else
            {
                _msg = "Failed to save data.";
            }
        }
        catch (Exception ex)
        {
            _msg = ex.Message;
        }

        return new { Result = _Result, Message = _msg };
    }

    [WebMethod]
    public static List<FuncData> GetFuncDataList(int empInfoId, int financialYearId)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]);
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
                if (row.Table.Columns.Contains("KpiWeight") && !row.IsNull("KpiWeight"))
                {
                    weight = Convert.ToDecimal(row["KpiWeight"]);
                }

                FuncData funcData = new FuncData();
                funcData.Dimension = row.Table.Columns.Contains("Dimension") ? row["Dimension"].ToString() : string.Empty;
                funcData.DimensionStr = row.Table.Columns.Contains("DimensionStr") ? row["DimensionStr"].ToString() : string.Empty;
                funcData.ObjectiveGoal = row.Table.Columns.Contains("ObjectiveGoal") ? row["ObjectiveGoal"].ToString() : string.Empty;
                funcData.KPIMeasure = row.Table.Columns.Contains("KPIMeasure") ? row["KPIMeasure"].ToString() : string.Empty;
                funcData.KpiWeight = weight;
                funcData.Initiatives = row.Table.Columns.Contains("Initiatives") ? row["Initiatives"].ToString() : string.Empty;
                if (row.Table.Columns.Contains("Deadline") && !row.IsNull("Deadline"))
                {
                    funcData.Deadline = Convert.ToDateTime(row["Deadline"]);
                }
                funcDataList.Add(funcData);
            }
        }
        return funcDataList;
    }

    [WebMethod]
    public static List<PartBData> GetPartBDataList(int empInfoId, int financialYearId)
    {
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]);
        int targetFinYearId = financialYearId;

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);
        List<PartBData> funcDataList = new List<PartBData>();
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);
            DataTable dt2 = _appPartA.GetAppraisalSelfB(mid);

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
        return funcDataList;
    }        

    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
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
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("shuvosmtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("shuvono-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("shuvono-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    //[WebMethod]
    //public static string SaveData(string FuncData, string PartBData)
    //{
    //    try
    //    {
    //        // Deserialize the JSON data
    //        var jsSerializer = new JavaScriptSerializer();
    //        var funcRowsData = jsSerializer.Deserialize<List<FuncDataModel>>(FuncData);
    //        var partBRowsData = jsSerializer.Deserialize<List<PartBDataModel>>(PartBData);

    //        // Log received data for debugging
    //        System.Diagnostics.Trace.TraceInformation("Received FuncData: " + FuncData);
    //        System.Diagnostics.Trace.TraceInformation("Received PartBData: " + PartBData);

    //        // Validate data
    //        if (funcRowsData == null)
    //            throw new ArgumentNullException("FuncData", "FuncData is null");
    //        if (partBRowsData == null)
    //            throw new ArgumentNullException("PartBData", "PartBData is null");

    //        // Process the data (you need to implement this part according to your logic)
    //        // For example, save it to the database or perform other operations
    //        if (funcRowsData.Count == 0 && partBRowsData.Count == 0)
    //        {
    //            throw new InvalidOperationException("No data provided in FuncData or PartBData");
    //        }

    //        // Return a success message
    //        return "Data saved successfully!";
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the error message
    //        System.Diagnostics.Trace.TraceError("Error in SaveData: " + ex.ToString());
    //        return "An error occurred: " + ex.Message;
    //    }
    //}

    //}
    [WebMethod]
    public static object GetEmpinfo(int empInfoId)
    {
        var result = new Dictionary<string, string>();
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]);
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
            result["DateOfJoin"] = dtEmp.Rows[0]["DateOfJoin"] != DBNull.Value ? Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy") : "";
            result["Location"] = dtEmp.Rows[0]["Location"].ToString().Trim();
            result["SalaryLocation"] = dtEmp.Rows[0]["SalaryLocation"].ToString().Trim();
            result["ReportingToName"] = dtEmp.Rows[0]["ReportingToName"].ToString().Trim();
            result["EmpInfoId"] = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();
        }

        return result;
    }

}