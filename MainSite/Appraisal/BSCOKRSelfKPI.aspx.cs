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
using System.Transactions;

public partial class Appraisal_BSCOKRSelfKPI : System.Web.UI.Page
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
    public static object SaveData(List<FuncData> FuncData, List<PartBData> PartBData, string actionType, string Comments, string Frm, int empInfoId = 0, int financialYearId = 0)
    {

        bool _Result = false;
        string _msg = "";
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();
        SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();
        BASAppraisalMaster aMaster = new BASAppraisalMaster();

        int targetEmpId = empInfoId > 0 ? empInfoId : Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]);
        int targetFinYearId = financialYearId;

        string mid = "0";

        DataTable dtaaMas = _appPartA.GetApprsaisalSelfByEmpFinYear(targetEmpId, targetFinYearId);

        if (dtaaMas.Rows.Count > 0)
        {

            mid = (dtaaMas.Rows[0][0].ToString());

        }
        if (actionType== "Draft")
        {
            aMaster.BSCAppraisalSelfMasterId = Convert.ToInt32(mid.ToString());
            aMaster.EmpInfoId = targetEmpId;

            aMaster.FinancialYearId = targetFinYearId;
            int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, FuncData, PartBData, Frm, HttpContext.Current.Session["UserId"].ToString());
            if (pk > 0)
            {
                _Result = true;
                _msg = "Data drafted successfully!";
            }
        }
        else
        {
         
            try
            {


                bool SubmitStatus = false;
                DataTable dtFinalApprovalSubmit = new DataTable();
                DataTable dtSuppervisorSubmit = new DataTable();
                int FinalApproveCount = 0;
                DataTable CheckFinalApprovalNew = _appPartA.CheckFinalApprovalConditionNotSuppervisor(targetEmpId.ToString());


                DataTable dtempdataSup = _appDashboard.GetEmpInfo(" WHERE ReportingEmpId is not null and  EmpInfoId='" + targetEmpId.ToString() + "'");

                String ddd = "";
                try
                {
                    ddd = CheckFinalApprovalNew.Rows[0]["IsAllEmployee"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }

                if (ddd == "True")
                {
                    SubmitStatus = true;

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
                if (FinalApproveCount == 1 && CheckFinalApprovalNew.Rows.Count > 0)
                {
                    SubmitStatus = true;

                }

                if (SubmitStatus)
                {





                    DataTable dtempdata = _appPartA.GetEmpInfo(" WHERE EmpInfoId='" + targetEmpId.ToString() + "'");


                    int repId = 0;

                    try
                    {
                        repId =
                            (int)dtempdata.Rows[0]["ReportingEmpId"];
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    DataTable CheckFinalApproval =
                        _appPartA.CheckFinalApprovalConditionNotSuppervisor(targetEmpId.ToString());


                    bool result = false;

                    if (CheckFinalApproval.Rows.Count > 0)
                    {


                        if (CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString() == "True")
                        {
                            aMaster.BSCAppraisalSelfMasterId = Convert.ToInt32(mid.ToString());
                            aMaster.EmpInfoId = targetEmpId;

                            aMaster.FinancialYearId = targetFinYearId;

                            int pk = 0;
                            AppraisalSelfAppLogDAO appLogDao1 = null;

                            SqlTransaction transaction;
                            using (SqlConnection connection = _appPartA.OpenTransactionalConnection(out transaction))
                            {
                                try
                                {
                                    pk = _appPartA.SaveAppraisalSelfMaster(connection, transaction, aMaster, FuncData, PartBData, Frm, HttpContext.Current.Session["UserId"].ToString());

                                    if (pk > 0)
                                    {
                                        AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                                        appLogDao.ActionStatus = "Drafted";
                                        appLogDao.ApproveDate = DateTime.Now;
                                        appLogDao.ApproveBy = HttpContext.Current.Session["UserId"].ToString();
                                        appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                                        appLogDao.ForEmpInfoId = targetEmpId;
                                        appLogDao.BSCAppraisalSelfMasterId = Convert.ToInt32(pk);
                                        appLogDao.Comments = Comments;
                                        appLogDao.CommentsEMP = targetEmpId;
                                        appLogDao.ShowStatus = "Drafted";

                                        int idd = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao);

                                        AppraisalSelfAppLogDAO aMastera = new AppraisalSelfAppLogDAO();
                                        aMastera.BSCAppraisalSelfMasterId
                                            = Convert.ToInt32(pk);

                                        aMastera.ActionStatus = "Verified";
                                        bool status = _appPartA.UpdateContractural(connection, transaction, aMastera);
                                        appLogDao1 = new AppraisalSelfAppLogDAO()
                                        {
                                            ActionStatus = "Verified",
                                            ApproveDate = DateTime.Now,
                                            ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                                            PreEmpInfoId = targetEmpId,
                                            ForEmpInfoId = Convert.ToInt32(CheckFinalApproval.Rows[0]["EmpInfoId"].ToString()),
                                            BSCAppraisalSelfMasterId = Convert.ToInt32(pk),
                                            Comments = Comments,
                                            CommentsEMP = targetEmpId,
                                             ShowStatus = "Submitted"

                                        };
                                        int id = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao1);

                                        transaction.Commit();
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                    }
                                }
                                catch (Exception)
                                {
                                    transaction.Rollback();
                                    throw;
                                }
                            }

                            if (pk > 0)
                            {
                                _Result = true;
                                _msg = "Data Submitted successfully!";
                                bool v = SenMailForApprved(appLogDao1.ForEmpInfoId, " BSC Approval ", @"  <br/> Dear Sir, <br/>
                                An Employee's BSC is waiting for your approval. <br/><br/>
                                please login for the details from the below link.<br/>    http://182.160.103.234:8088/
                                 <br/> Thank You.");
                            }

                        }


                        else if (repId > 0)
                        {
                            aMaster.BSCAppraisalSelfMasterId = Convert.ToInt32(mid.ToString()); ;
                            aMaster.EmpInfoId = targetEmpId;


                            aMaster.FinancialYearId = targetFinYearId; ;

                            int pk = 0;
                            AppraisalSelfAppLogDAO appLogDao1 = null;

                            SqlTransaction transaction;
                            using (SqlConnection connection = _appPartA.OpenTransactionalConnection(out transaction))
                            {
                                try
                                {
                                    pk = _appPartA.SaveAppraisalSelfMaster(connection, transaction, aMaster, FuncData, PartBData, Frm, HttpContext.Current.Session["UserId"].ToString());

                                    if (pk > 0)
                                    {
                                        AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                                        appLogDao.ActionStatus = "Drafted";
                                        appLogDao.ApproveDate = DateTime.Now;
                                        appLogDao.ApproveBy = HttpContext.Current.Session["UserId"].ToString();
                                        appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                                        appLogDao.ForEmpInfoId = targetEmpId;
                                        appLogDao.BSCAppraisalSelfMasterId = Convert.ToInt32(pk);
                                        appLogDao.Comments = Comments;
                                        appLogDao.CommentsEMP = targetEmpId;
                                        appLogDao.ShowStatus = "Drafted";

                                        int idd = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao);

                                        AppraisalSelfAppLogDAO aMastera = new AppraisalSelfAppLogDAO();
                                        aMastera.BSCAppraisalSelfMasterId
                                            = Convert.ToInt32(pk);
                                        aMastera.ActionStatus = "Verified";
                                        bool status = _appPartA.UpdateContractural(connection, transaction, aMastera);
                                        appLogDao1 = new AppraisalSelfAppLogDAO()
                                        {
                                            ActionStatus = "Verified",
                                            ApproveDate = DateTime.Now,
                                            ApproveBy = HttpContext.Current.Session["UserId"].ToString(),
                                            PreEmpInfoId = targetEmpId,
                                            ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                                            BSCAppraisalSelfMasterId = Convert.ToInt32(pk),
                                            Comments = Comments,
                                            CommentsEMP = targetEmpId,
                                            ShowStatus = "Submitted"

                                        };
                                        int id = _appPartA.SaveEmpAppLog(connection, transaction, appLogDao1);

                                        transaction.Commit();
                                    }
                                    else
                                    {
                                        transaction.Rollback();
                                    }
                                }
                                catch (Exception)
                                {
                                    transaction.Rollback();
                                    throw;
                                }
                            }

                            if (pk > 0)
                            {
                                _Result = true;
                                _msg = "Data Submitted successfully!";
                                bool v = SenMailForApprved(appLogDao1.ForEmpInfoId, " BSC Approval ", @"  <br/> Dear Sir, <br/>
                                An Employee's BSC is waiting for your approval. <br/><br/>
                                please login for the details from the below link.<br/>    http://182.160.103.234:8088/
                                 <br/> Thank You.");



                            }

                        }
                        else
                        {
                            _Result = false;
                            _msg = "Your Reporting Employee is not set yet!!!";
                            //   AlertMessageBoxShow("Your Reporting Employee is not set yet!!!");
                        }

                    }

                    else
                    {
                        _Result = false;
                        _msg = "final approval has not been set yet!!!";
                        // AlertMessageBoxShow("final approval has not been set yet!!!");
                    }




                }
                else
                {
                    //AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");
                    _Result = false;
                    _msg = "Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!";
                }

            }
            catch (Exception exMsg)
            {
                 
                _Result = false;
                _msg = "Something Went Wrong! Please try after sometimes!";
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
    public static object GetEmpinfo(int empInfoId = 0)
    {
        var result = new Dictionary<string, string>();
        BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
        int targetEmpId = empInfoId > 0 ? empInfoId : (HttpContext.Current.Session["EmpInfoId"] != null ? Convert.ToInt32(HttpContext.Current.Session["EmpInfoId"]) : 0);
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

}