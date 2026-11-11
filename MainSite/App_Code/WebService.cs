using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAL.DataManager;
using DAL.InternalCls;
using DAL.TrainingAndLearningDevelopment_DAL;
using DAL.TrainingDAL;
using DAL.WebServiceDal;
using DAO.HRIS_DAO_EF;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {
    private TrainingDAL _trainingDal = new TrainingDAL();
    WebServiceDal aServiceDal = new WebServiceDal();
    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }


    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetUserbyCompanyId(string prefixText)
    {
        var companyId = Session["CompanyId"].ToString();
        DataTable employee = null;

        if (companyId != "")
        {
            employee = aServiceDal.GetUserbycompany(prefixText, companyId);
        }

        var topics = new string[employee.Rows.Count];

        int i = 0;

        foreach (DataRow dr in employee.Rows)
        {
            topics.SetValue(dr["UserName"].ToString(), i);
            i++;
        }

        return topics;
    }

    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetEployeeAutoComp(string prefixText, int count)
    {


        string[] temp = null;
        //if (!string.IsNullOrEmpty(Session["cid"].ToString()))
        //{
        //    var cid = int.Parse(Session["cid"].ToString());

        //    using (var db = new HRIS_SMCEntities())
        //    {
        //        temp = (from e in db.tblMenuApprovalGroupSetups
        //                join s in db.tblCompanyInfoes on e.CompanyId equals s.CompanyId
        //                where e.MenuApprovalGroupName.Contains(prefixText) && s.CompanyId.Equals(cid)
        //                select e.MenuApprovalGroupName).ToArray();
        //    }
        //}
        string companyId = Session["CompanyId"].ToString();
        temp = _trainingDal.GetEmployee(prefixText, companyId);

        return temp;
    }
    //Region for Training Module

    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetExitImployee(string prefixText)
    {
        var companyId = Session["CompanyId"].ToString();
        DataTable employee = null;

        if (companyId != "")
        {
            employee = aServiceDal.GetExitImployeeForm(prefixText, companyId);
        }

        var topics = new string[employee.Rows.Count];

        int i = 0;

        foreach (DataRow dr in employee.Rows)
        {
            topics.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return topics;
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]

    public string[] GetExitImployeeDept(string prefixText, string contextKey)
    {
        var companyId = Session["CompanyId"].ToString();
        DataTable employee = null;

        if (companyId != "")
        {
            employee = aServiceDal.GetExitImployeeFormDept(prefixText, companyId,contextKey);
        }

        var topics = new string[employee.Rows.Count];

        int i = 0;

        foreach (DataRow dr in employee.Rows)
        {
            topics.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return topics;
    }
    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetExitImployeeForm(string prefixText)
    {
        var companyId = Session["CompanyId"].ToString();
        DataTable employee = null;

        if (companyId != "")
        {
            employee = aServiceDal.GetExitImployeeForm(prefixText, companyId);
        }

        var topics = new string[employee.Rows.Count];

        int i = 0;

        foreach (DataRow dr in employee.Rows)
        {
            topics.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return topics;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetHospitalNameAuto(string prefixText, int count)
    {
        DataTable hospitalNames = aServiceDal.GetHospitalNameAuto(prefixText);
        var topics = new string[hospitalNames.Rows.Count];

        for (int i = 0; i < hospitalNames.Rows.Count; i++)
        {
            topics[i] = hospitalNames.Rows[i]["HospitalName"].ToString();
        }

        return topics;
    }










    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetTrainingTopic(string prefixText)
    {
        DataTable trainingTopic = aServiceDal.GetTrainingTopic(prefixText);

        var topics = new string[trainingTopic.Rows.Count];

        int i = 0;

        foreach (DataRow dr in trainingTopic.Rows)
        {
            topics.SetValue(dr["TrainingTopicTitle"].ToString(), i);
            i++;
        }

        return topics;
    }
    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetEployeeAutoComp2(string prefixText, int count)
    {


        string[] temp = null;
        //if (!string.IsNullOrEmpty(Session["cid"].ToString()))
        //{
        //    var cid = int.Parse(Session["cid"].ToString());

        //    using (var db = new HRIS_SMCEntities())
        //    {
        //        temp = (from e in db.tblMenuApprovalGroupSetups
        //                join s in db.tblCompanyInfoes on e.CompanyId equals s.CompanyId
        //                where e.MenuApprovalGroupName.Contains(prefixText) && s.CompanyId.Equals(cid)
        //                select e.MenuApprovalGroupName).ToArray();
        //    }
        //}

        temp = _trainingDal.GetEmployeeAuto2(prefixText);

        return temp;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetParticipantList(string prefixText)
    {
        DataTable participant = aServiceDal.GetParticipantList(prefixText);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }
    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetExitClearenceImployee(string prefixText)
    {
        var companyId = Session["CompanyId"].ToString();
        DataTable employee = null;

        if (companyId != "")
        {
           // employee = aServiceDal.GetClearenceImployeeInfo(prefixText, companyId);
        }

        var topics = new string[employee.Rows.Count];

        int i = 0;

        foreach (DataRow dr in employee.Rows)
        {
            topics.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return topics;
    }
    
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetTrainingLocation(string prefixText)
    {
        DataTable participant = aServiceDal.GetTrainingLocationList(prefixText);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["TrainingLocation"].ToString(), i);
            i++;
        }

        return list;
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompanyWiseEmployeeInfo(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetCompanyWiseEmployeeInfo(prefixText, companyId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }




    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetNameofEducationWS(string prefixText)
    {
         
        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetNameofEducationWS(prefixText);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EducationName"].ToString(), i);
            i++;
        }

        return list;
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetBoardUniversityWS(string prefixText)
    {

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetBoardUniversityWS(prefixText);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["BoardUniversity"].ToString(), i);
            i++;
        }

        return list;
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetEducationalInstituteWS(string prefixText)
    {

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetEducationalInstituteWS(prefixText);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["BoardUniversity"].ToString(), i);
            i++;
        }

        return list;
    }



    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompanyWiseEmployeeInfoForIDANdNae(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetCompanyWiseEmployeeInfoForEmpIdandName(prefixText, companyId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompanyWiseEmployeeInfoActiveInactiveAll(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetCompanyWiseEmployeeInfoActiveInactiveAll(prefixText, companyId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }



    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] getMiscellaneousKeySearch(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }
         DataTable participant =new DataTable();
        if (Session["UserTypeId"].ToString() == "3" ||
            Session["UserTypeId"].ToString() == "4")
        {

            participant = aServiceDal.getMiscellaneousKeySearchDAL(prefixText, companyId);
        }
        else
        {
            participant = aServiceDal.getMiscellaneousKeySearchDAL(prefixText, companyId, Convert.ToInt32(Session["UserId"].ToString()));
            
        }

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["KeySearch"].ToString(), i);
            i++;
        }

        return list;
    }



    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] getMiscellaneousKeySearch_Audit(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }
        DataTable participant = new DataTable();
        if (Session["UserTypeId"].ToString() == "3" ||
            Session["UserTypeId"].ToString() == "4")
        {

            participant = aServiceDal.getMiscellaneousKeySearchDAL_Audit(prefixText, companyId);
        }
        else
        {
            participant = aServiceDal.getMiscellaneousKeySearchDAL_Audit(prefixText, companyId, Convert.ToInt32(Session["UserId"].ToString()));

        }

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["KeySearch"].ToString(), i);
            i++;
        }

        return list;
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] getMeetingKeySearch(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }
        DataTable participant = new DataTable();
        if (Session["UserTypeId"].ToString() == "3" ||
            Session["UserTypeId"].ToString() == "4")
        {

            participant = aServiceDal.getMeetingKeySearchDAL(prefixText, companyId);
        }
        else
        {
            participant = aServiceDal.getMeetingKeySearchhDAL(prefixText, companyId, Convert.ToInt32(Session["UserId"].ToString()));

        }

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["KeySearch"].ToString(), i);
            i++;
        }

        return list;
    }

    private int? GetMeetingSuggestionUserId()
    {
        if (Session["UserTypeId"] != null &&
            (Session["UserTypeId"].ToString() == "3" || Session["UserTypeId"].ToString() == "4"))
        {
            return null;
        }

        return Session["UserId"] == null ? (int?)null : Convert.ToInt32(Session["UserId"].ToString());
    }

    private static string[] GetSearchValues(DataTable suggestions)
    {
        var list = new string[suggestions.Rows.Count];
        for (int i = 0; i < suggestions.Rows.Count; i++)
        {
            list[i] = suggestions.Rows[i]["SearchValue"].ToString();
        }
        return list;
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetMeetingIdNoSuggestions(string prefixText)
    {
        int companyId = Session["CompanyId"] == null ? 0 : Convert.ToInt32(Session["CompanyId"].ToString());
        return GetSearchValues(aServiceDal.GetMeetingIdNoSuggestions(prefixText, companyId, GetMeetingSuggestionUserId()));
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetMeetingTitleSuggestions(string prefixText)
    {
        int companyId = Session["CompanyId"] == null ? 0 : Convert.ToInt32(Session["CompanyId"].ToString());
        return GetSearchValues(aServiceDal.GetMeetingTitleSuggestions(prefixText, companyId, GetMeetingSuggestionUserId()));
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompanyWiseEmployeeInfoActive(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetCompanyWiseEmployeeInfoActive(prefixText, companyId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompanyWiseEmployeeInfoCode(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetCompanyWiseEmployeeInfo(prefixText, companyId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }
    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetEmpNameAuto(string prefixText, int count)
    {
        string[] temp = null;
        var EmpOption = Session["EmpOption"].ToString();
        var cid = int.Parse(Session["cid"].ToString());
        if (!string.IsNullOrEmpty(EmpOption))
        {
            if (EmpOption == "Employee")
            {
                using (var db = new HRIS_SMCEntities())
                {
                    temp = (from e in db.tblEmpGeneralInfoes where e.CompanyId==cid &&
                                e.EmpName.Contains(prefixText) select e.EmpName).ToArray();
                }
            }
            else if (EmpOption == "CompanyGuest")
            {
                using (var db = new HRIS_SMCEntities())
                {
                    temp = (from e in db.tblEmpGeneralInfoes
                        where e.CompanyId != cid &&
                              e.EmpName.Contains(prefixText)
                        select e.EmpName).ToArray();
                }
            }
            
            
        }
        
        return temp;
    }
    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public List<string> GetJobCirculationAuto(string prefixText, int count)
    {
        List<string> temp = new List<string>();
        if (Session["cid"]!=null)
        {
            var CompanyId = int.Parse(Session["cid"].ToString());
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT (CAST(j.JobID AS NVARCHAR(20)) +':'+ j.JobCode+' :' + j.Position) AS job FROM dbo.tblJobCreation j WHERE (j.JobCode+j.Position) LIKE '%" + prefixText + "%' AND j.CompanyId=@CompanyId";
            using (DataTable dt = aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        temp.Add(dt.Rows[i]["job"].ToString());
                    }

                }
            }
        }

        return temp;
    }

    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public List<string> GetJobCirculationAutoPosition(string prefixText, int count)
    {
        List<string> temp = new List<string>();
        string query = string.Empty;
        int CompanyId=0;
        int FinYearId=0;
        int DepartmentId = 0;
        string startDate = string.Empty;
        string endDate = string.Empty;
        if (Session["cid"] != null)
        {
            CompanyId = int.Parse(Session["cid"].ToString());
        }
        if (Session["FinYearId"] != null)
        {
            if (!string.IsNullOrEmpty(Session["FinYearId"].ToString()))
            {
                FinYearId = int.Parse(Session["FinYearId"].ToString());    
            }
            
        }
        if (Session["DepartmentId"] != null)
        {
            if (!string.IsNullOrEmpty(Session["DepartmentId"].ToString()))
            {
                DepartmentId = int.Parse(Session["DepartmentId"].ToString());
            }
        }
        if (Session["startDate"] != null)
        {
            if (!string.IsNullOrEmpty(Session["startDate"].ToString()))
            {

                startDate = Session["startDate"].ToString();
            }
        }
        if (Session["endDate"] != null)
        {
            if (!string.IsNullOrEmpty(Session["endDate"].ToString()))
            {
                endDate = Session["endDate"].ToString();
            }
        }

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        var aSqlParameterlist = new List<SqlParameter>();
        aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId));
        aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));
        aSqlParameterlist.Add(new SqlParameter("@FinYearId", FinYearId));
        aSqlParameterlist.Add(new SqlParameter("@startDate", startDate));
        aSqlParameterlist.Add(new SqlParameter("@endDate", endDate));

        if (CompanyId > 0 && DepartmentId > 0 && FinYearId > 0)
        {
            query = @"SELECT (CAST(j.JobID AS NVARCHAR(20)) + ':' + j.JobCode + ' :' + j.Position) AS job FROM dbo.tblJobCreation j INNER JOIN dbo.tblJobReqForm r ON r.JobReqId=j.ReqCodeId WHERE  (j.JobCode + j.Position) LIKE '%" + prefixText + "%' AND   j.ActionStatus='Submitted' and  ( (r.IsDelete) IS NULL OR (r.IsDelete = 0 )  ) and r.CompanyId =@CompanyId and r.DeptId=@DepartmentId AND r.FinYearId=@FinYearId";
        }
        //if (DepartmentId>0)
        //{
        //    query += " AND r.DeptId=@DepartmentId";
        //}
        //if (FinYearId>0)
        //{
        //    query += " AND r.FinYearId=@FinYearId";
        //}
        if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
        {
            query += " AND CAST(j.CirculationStartDate AS DATE)=CAST('"+ startDate+"' AS DATE)";
        }
        if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
        {
            query += " AND CAST(j.CirculationStartDate AS DATE)<=CAST('" + endDate + "' AS DATE)";
        }
        if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
        {
            query += " AND CAST(j.CirculationStartDate AS DATE) BETWEEN CAST('"+startDate+"' AS DATE) AND CAST('"+endDate+"' AS DATE)";
        }


        {
            {
                //query = @"SELECT (CAST(j.JobID AS NVARCHAR(20)) + ':' + j.JobCode + ' :' + j.Position) AS job FROM dbo.tblJobCreation j INNER JOIN dbo.tblJobReqForm r ON r.JobReqId=j.ReqCodeId WHERE (j.JobCode + j.Position) LIKE '%" + prefixText + "%' AND r.CompanyId =@CompanyId AND r.DeptId=@DepartmentId AND r.FinYearId=@FinYearId AND CAST(j.CirculationStartDate AS DATE) BETWEEN CAST(@startDate AS DATE) AND CAST(@endDate AS DATE)";
                try
                {
                    using (DataTable dt = aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                temp.Add(dt.Rows[i]["job"].ToString());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    
                    //throw;
                }
            }
            return temp;
        }
        //catch (Exception)
        //{
            
        //    //throw;
        //}
        //return temp;
    }


    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetMenuGroupNameAuto(string prefixText, int count)
    {
        string[] temp = null;
        //if (!string.IsNullOrEmpty(Session["cid"].ToString()))
        {
            //var cid = int.Parse(Session["cid"].ToString());

            using (var db = new HRIS_SMCEntities())
            {
                temp = (from e in db.tblMenuGroupSetups
                    
                    where e.MenuGroupName.Contains(prefixText) 
                    select e.MenuGroupName).ToArray();
            }
        }
        
        return temp;
    }


    [WebMethod(EnableSession = true)]
    public string[] GetEmpNameDesigIDAuto(string prefixText, int count)
    {
        string[] temp = null;
        if (!string.IsNullOrEmpty(Session["cid"].ToString()))
        {
            var cid = int.Parse(Session["cid"].ToString());

            using (var db = new HRIS_SMCEntities())
            {
                temp = (from e in db.tblEmpGeneralInfoes
                    join s in db.tblDesignations on e.DesignationId equals s.DesignationId
                    where (e.EmpInfoId+e.EmpName+s.Designation).Contains(prefixText) 
                    select e.EmpInfoId+">"+e.EmpName+">"+s.Designation).ToArray();
            }
        }

        return temp;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetEmpNameDesigIDAuto_AllCompany(string prefixText, int count)
    {
        string[] temp = null;
        using (var db = new HRIS_SMCEntities())
        {
            temp = (from e in db.tblEmpGeneralInfoes
                join s in db.tblDesignations on e.DesignationId equals s.DesignationId
                    where (e.EmpInfoId + e.EmpMasterCode + e.EmpName + s.Designation).Contains(prefixText)
                    select e.EmpInfoId + ">" + e.EmpMasterCode + ">" + e.EmpName + ">" + s.Designation).ToArray();
        }

        return temp;
    }
    [WebMethod(EnableSession = true)]
    public List<string> GetEmpSelectedCompanyAuto(string prefixText, int count)
    {
        List<string> temp = new List<string>();
        if (!string.IsNullOrEmpty(Session["selectedCompanys"].ToString()))
        {
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
            var selectedCompanys = Session["selectedCompanys"].ToString();

            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@selectedCompanys", selectedCompanys) };
            string query =
                @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + e.EmpMasterCode + ' : ' + e.EmpName + ' : ' + d.Designation + ' : ' + dept.DepartmentName AS Emp
FROM dbo.tblEmpGeneralInfo e
INNER JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
INNER JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1 AND e.CompanyId IN (" + selectedCompanys + ") AND (CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + d.Designation + ' ' + dept.DepartmentName ) LIKE '%" + prefixText + "%'";
            using (DataTable dt = aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        temp.Add(dt.Rows[i]["Emp"].ToString());
                    }
                    
                }
            }
        }

        return temp;
    }


    [WebMethod(EnableSession = true)]
    public List<string> GetEmpSelectedALL(string prefixText, int count)
    {
        List<string> temp = new List<string>();
        
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
          
           
            string query =
                @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + e.EmpMasterCode + ' : ' + e.EmpName + ' : ' + ISNULL(d.Designation,'') + ' : ' + ISNULL(dept.DepartmentName,'') AS Emp
FROM dbo.tblEmpGeneralInfo e
INNER JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1    AND (CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + ISNULL(d.Designation,'') + ' ' + ISNULL(dept.DepartmentName,'') )  LIKE '%" + prefixText + "%'";
            using (DataTable dt = aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        temp.Add(dt.Rows[i]["Emp"].ToString());
                    }

                }
            }
     

        return temp;
    }


    [WebMethod(EnableSession = true)]
    public List<string> GetEmpOneCompanyAuto(string prefixText, int count)
    {
        List<string> temp = new List<string>();
        if (!string.IsNullOrEmpty(Session["cid"].ToString()))
        {
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
            var cid = int.Parse(Session["cid"].ToString());

            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@cid", cid) };
            string query =
                @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + e.EmpMasterCode + ' : ' + e.EmpName + ' : ' + d.Designation + ' : ' + dept.DepartmentName AS Emp
FROM dbo.tblEmpGeneralInfo e
INNER JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
INNER JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1 AND e.CompanyId=@cid AND (CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + d.Designation + ' ' + dept.DepartmentName ) LIKE '%" + prefixText + "%'";
            using (DataTable dt = aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        temp.Add(dt.Rows[i]["Emp"].ToString());
                    }

                }
            }
        }

        return temp;
    }


    [WebMethod(EnableSession = true)]
    public List<string> GetEmpAutoForIVBoardSetup(string prefixText, int count)
    {
        List<string> temp = new List<string>();
        if (!string.IsNullOrEmpty(Session["EmpOption"].ToString()))
        {
            ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
            var cid = int.Parse(Session["EmpOption"].ToString());

            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@cid", cid) };
            string query =
                @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + e.EmpMasterCode + ' : ' + e.EmpName + ' : ' + d.Designation + ' : ' + dept.DepartmentName AS Emp
FROM dbo.tblEmpGeneralInfo e
LEFT JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
LEFT JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1 AND e.CompanyId="+cid+" AND (CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + d.Designation + ' ' + dept.DepartmentName ) LIKE '%" + prefixText + "%'";
            using (DataTable dt = aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        temp.Add(dt.Rows[i]["Emp"].ToString());
                    }

                }
            }
        }

        return temp;
    }


    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public string[] GetMenuApprovalGroupNameAuto(string prefixText, int count)
    {
        string[] temp = null;
        if (!string.IsNullOrEmpty(Session["cid"].ToString()))
        {
            var cid = int.Parse(Session["cid"].ToString());

            using (var db = new HRIS_SMCEntities())
            {
                temp = (from e in db.tblMenuApprovalGroupSetups
                    join s in db.tblCompanyInfoes on e.CompanyId equals s.CompanyId
                    where e.MenuApprovalGroupName.Contains(prefixText) && s.CompanyId.Equals(cid)
                        select e.MenuApprovalGroupName).ToArray();
            }
        }
        
        return temp;
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCmpWiseEmployeeInfo(string prefixText)
    {
        Int32 companyId = 0;
        Int32 dsnId = 0;
        try
        {

        
        

        if (Session["DsnCmpId"].ToString() != "")
        {
            companyId = Convert.ToInt32(Session["DsnCmpId"].ToString());
        }

        if (Session["DsnId"].ToString() != "")
        {
            dsnId = Convert.ToInt32(Session["DsnId"].ToString());
        }
        }
        catch (Exception ex)
        {


        }
        DataTable participant = aServiceDal.GetCmpWiseEmployeeInfo(prefixText, companyId, dsnId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
        
    }
    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompanyWiseEmployeeInfoOnlyForPromotion(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetCompanyWiseEmployeeInfoOnlyForPromotionTrans(prefixText, companyId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetKPIBehaviour(string prefixText)
    {
      

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetKPIBehaviour(prefixText);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["KPIBehaviourName"].ToString(), i);
            i++;
        }

        return list;
    }


    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetKPIBehaviourByType(string prefixText)
    {
        string KPI_Type = "Nai";

        if (Session["KPI_Type"] != null)
        {
            KPI_Type = (Session["KPI_Type"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetKPIBehaviourByType(prefixText, KPI_Type);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["KPIBehaviourName"].ToString(), i);
            i++;
        }

        return list;
    }

    [WebMethod(EnableSession = true)]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetEmpProfile(string prefixText)
    {
        Int32 companyId = 0;

        if (Session["CompanyId"] != null)
        {
            companyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }

        //DataTable participant = aServiceDal.GetParticipantList(prefixText);

        DataTable participant = aServiceDal.GetCompanyWiseEmployeeInfoOnlyForPromotionTransProfile(prefixText, companyId);

        var list = new string[participant.Rows.Count];

        int i = 0;

        foreach (DataRow dr in participant.Rows)
        {
            list.SetValue(dr["EmpName"].ToString(), i);
            i++;
        }

        return list;
    }

    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public static List<string> GetAutoCompleteData2NEW(string username)
    {
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection(@"Data Source=CSTL-PC-03\SQLSERVER2014;Initial Catalog=HRIS_SMC_DBNew;Integrated Security=true;"))
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' +  ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE  
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + ISNULL(d.Designation,'') + ' ' + ISNULL(dept.DepartmentName, '') ) LIKE '%'+ @SearchText +'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["EmpName"].ToString());
                }
                return result;
            }
        }
    }


    [System.Web.Script.Services.ScriptMethod]
    [WebMethod(EnableSession = true)]
    public List<trafficSourceData> getTrafficSourceData(List<string> gData)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRIS_SMC"].ConnectionString;
        List<trafficSourceData> t = new List<trafficSourceData>();
        string[] arrColor = new string[] { "#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775", "#5A69A6" };

        using (SqlConnection cn = new SqlConnection(connectionString))
        {
            string myQuery = @"SELECT  '20-30'Interval,COUNT(*)Total FROM
            (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo WHERE IsActive='1' AND EmployeeStatus='Active' " + gData + ")AS tbltemp WHERE tbltemp.Years BETWEEN '20' AND '30' " +
            " UNION ALL "+
            " SELECT  '31-40'Interval,COUNT(*)Total FROM "+
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo WHERE  IsActive='1' AND EmployeeStatus='Active' " + gData + ")AS tbltemp WHERE tbltemp.Years BETWEEN '31' AND '40' " +
            " UNION ALL "+
            " SELECT  '41-50'Interval,COUNT(*)Total FROM "+
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo WHERE  IsActive='1' AND EmployeeStatus='Active' " + gData + ")AS tbltemp WHERE tbltemp.Years BETWEEN '41' AND '50' " +
            " UNION ALL "+
            " SELECT  '51-60'Interval,COUNT(*)Total FROM "+
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo WHERE  IsActive='1' AND EmployeeStatus='Active' " + gData + ")AS tbltemp WHERE tbltemp.Years BETWEEN '51' AND '60'"

            +
            " UNION ALL " +
            " SELECT  '61-70'Interval,COUNT(*)Total FROM " +
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo WHERE  IsActive='1' AND EmployeeStatus='Active' " + gData + ")AS tbltemp WHERE tbltemp.Years BETWEEN '61' AND '70'"

              +
            " UNION ALL " +
            " SELECT  '71-80'Interval,COUNT(*)Total FROM " +
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo WHERE  IsActive='1' AND EmployeeStatus='Active' " + gData + ")AS tbltemp WHERE tbltemp.Years BETWEEN '71' AND '80'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = myQuery;
            cmd.CommandType = CommandType.Text;
            if (Session["BarChartAgeGroup"]!=null)
            {
                cmd.Parameters.AddWithValue("@month", gData[2]);
            }
         
            cmd.Connection = cn;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                int counter = 0;
                while (dr.Read())
                {
                    trafficSourceData tsData = new trafficSourceData();
                    tsData.value = dr["Total"].ToString();
                    tsData.label = dr["Interval"].ToString();
                    tsData.color = arrColor[counter];
                    t.Add(tsData);
                    counter++;
                }
            }
        }
        return t;
    }

    public class trafficSourceData

    {

        public string label { get; set; }
        public string value { get; set; }
        public string color { get; set; }
        public string hightlight { get; set; }
    }
}
