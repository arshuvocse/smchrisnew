using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.WebServiceDal
{
    public class WebServiceDal
    {
        readonly ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetUserbycompany(string prefixText, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT CAST(us.UserId AS NVARCHAR(50))+':'+us.UserName+ ISNULL(' : '+emp.EmpName,'') UserName  FROM tblUser us WITH (NOLOCK)
                                      left JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = us.EmpInfoId
                                       WHERE us.IsActive=1 AND emp.CompanyId=@CompanyId AND UserName LIKE '%'+@SearchText+'%'";

            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetTrainingTopic(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));

            const string queryStr = "SELECT DISTINCT TrainingTopicTitle from tblTrainingTopic  WITH (nolock) WHERE TrainingTopicTitle like '%' + @SearchText + '%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist ,"HRDB");
          
        }

        public DataTable GetParticipantList(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));

            const string queryStr = "SELECT DISTINCT EmpName FROM tblEmpGeneralInfo  WITH (nolock) WHERE EmpName like '%' + @SearchText + '%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetExitImployeeForm(string prefixText, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = "SELECT EGI.EmpMasterCode + ':' + EGI.EmpName + ':' + CAST(EGI.EmpInfoId AS NVARCHAR(50)) AS EmpName FROM dbo.tblEmpGeneralInfo AS EGI  WITH (nolock) WHERE EGI.EmpMasterCode IS NOT NULL  and EGI.CompanyId = @CompanyId AND EmpName like '%' + @SearchText + '%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetExitImployeeFormDept(string prefixText, string companyId,string deptId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", deptId));

            const string queryStr = "SELECT EGI.EmpMasterCode + ':' + EGI.EmpName + ':' + CAST(EGI.EmpInfoId AS NVARCHAR(50)) AS EmpName FROM dbo.tblEmpGeneralInfo AS EGI  WITH (nolock) WHERE EGI.CompanyId = @CompanyId AND DepartmentId=@DepartmentId AND EmpName like '%' + @SearchText + '%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetHospitalNameAuto(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));

            const string queryStr = @"SELECT TOP 15 HospitalName
FROM dbo.tblHospitalName WITH (NOLOCK)
WHERE ISNULL(HospitalName, '') LIKE '%' + @SearchText + '%'
ORDER BY HospitalName ASC";

            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetTrainingLocationList(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));

            const string queryStr = "SELECT DISTINCT TrainingLocation FROM tblTrainingLocation  WITH (nolock) WHERE TrainingLocation like '%' + @SearchText + '%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetCompanyWiseEmployeeInfo(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + ISNULL(e.EmpMasterCode,'') + ' : ' + e.EmpName + ' : ' + ISNULL(d.Designation,'') + ' : ' + ISNULL(dept.DepartmentName,'')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock) 
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1 AND   e.CompanyId = @CompanyId and
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + ISNULL(d.Designation,'') + ' ' + ISNULL(dept.DepartmentName,'') ) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetCompanyWiseEmployeeInfoForEmpIdandName(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @" SELECT   CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + ISNULL(e.EmpMasterCode,'') + ' : ' + ISNULL(e.EmpName,'') + ' : ' + ISNULL(d.Designation,' ') + ' : ' + ISNULL(dept.DepartmentName,' ')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId

WHERE  e.CompanyId = @CompanyId AND
(  CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + ISNULL(e.EmpMasterCode,'') + ' : ' + ISNULL(e.EmpName,'') + ' : ' + ISNULL(d.Designation,' ') + ' : ' + ISNULL(dept.DepartmentName,' ')  ) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetNameofEducationWS(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));


            const string queryStr = @"   SELECT en.Description  +':'+ CAST(en.EducationNameID AS NVARCHAR(500)) AS EducationName    FROM dbo.tblEducationName en  WITH (nolock)

WHERE   en.Description   LIKE '%'+  @SearchText  +'%' ";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetBoardUniversityWS(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));


            const string queryStr = @"   SELECT en.Description  +':'+ CAST(en.BoardUniversityID AS NVARCHAR(500)) AS BoardUniversity    FROM dbo.tblBoardUniversity en  WITH (nolock)

WHERE   en.Description   LIKE '%'+  @SearchText  +'%' ";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetEducationalInstituteWS(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));


            const string queryStr = @"   SELECT en.Description  +':'+ CAST(en.InstitutionID AS NVARCHAR(500)) AS BoardUniversity    FROM dbo.tblEducationalInstitution en  WITH (nolock)

WHERE   en.Description   LIKE '%'+  @SearchText  +'%' ";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetCompanyWiseEmployeeInfoActiveInactiveAll(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' +  ISNULL(e.EmpMasterCode,'') + ' : ' + e.EmpName + ' : ' + ISNULL(d.Designation,' ') + ' : ' + ISNULL(dept.DepartmentName,' ')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
INNER JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
INNER JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE    e.CompanyId = @CompanyId and
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + d.Designation + ' ' + dept.DepartmentName ) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable getMiscellaneousKeySearchDAL(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT LTRIM(RTRIM(KeySearch)) KeySearch  FROM dbo.tblMeeting_MiscellaneousInfo
WITH (NOLOCK) WHERE CompanyId=@CompanyId and LTRIM(RTRIM(KeySearch))   LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable getMiscellaneousKeySearchDAL_Audit(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT LTRIM(RTRIM(mas.KeySearch))+ ' | '+CAST(mas.AuditLog_MiscellaneousInfoId AS NVARCHAR(max))  KeySearch  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
WITH (NOLOCK) WHERE  mas.StatusMode<>'Initial' AND  mas.AuditLog_MiscellaneousInfoId = 
  (SELECT max(AuditLog_MiscellaneousInfoId) FROM tblMeeting_MiscellaneousInfo_AuditLog t2 WHERE t2.MiscellaneousInfoId = mas.MiscellaneousInfoId)  AND   CompanyId=@CompanyId and LTRIM(RTRIM(KeySearch))   LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable getMiscellaneousKeySearchDAL(string prefixText, int companyId, int UserID)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));
            aSqlParameterlist.Add(new SqlParameter("@UserID", UserID));

            const string queryStr = @"SELECT LTRIM(RTRIM(KeySearch)) KeySearch FROM dbo.tblMeeting_MiscellaneousInfo
WITH (NOLOCK) WHERE CompanyId=@CompanyId    and CreateBy=   @UserID and  LTRIM(RTRIM(KeySearch)) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable getMiscellaneousKeySearchDAL_Audit(string prefixText, int companyId, int UserID)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));
            aSqlParameterlist.Add(new SqlParameter("@UserID", UserID));

            const string queryStr = @"SELECT LTRIM(RTRIM(mas.KeySearch))+ ' | '+CAST(mas.AuditLog_MiscellaneousInfoId AS NVARCHAR(max))  KeySearch  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
WITH (NOLOCK) WHERE mas.StatusMode<>'Initial' AND  mas.AuditLog_MiscellaneousInfoId = 
  (SELECT max(AuditLog_MiscellaneousInfoId) FROM tblMeeting_MiscellaneousInfo_AuditLog t2 WHERE t2.MiscellaneousInfoId = mas.MiscellaneousInfoId)  AND  CreateBy=   @UserID and CompanyId=@CompanyId and LTRIM(RTRIM(KeySearch))   LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable getMeetingKeySearchDAL(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT KeySearch FROM dbo.tblMeeting_MeetingInfo
WITH (NOLOCK) WHERE CompanyId=@CompanyId and KeySearch   LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable getMeetingKeySearchhDAL(string prefixText, int companyId, int UserID)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));
            aSqlParameterlist.Add(new SqlParameter("@UserID", UserID));

            const string queryStr = @"SELECT KeySearch FROM dbo.tblMeeting_MeetingInfo
WITH (NOLOCK) WHERE CompanyId=@CompanyId    and CreateBy=   @UserID and  KeySearch LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetMeetingIdNoSuggestions(string prefixText, int companyId, int? userId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SearchText", prefixText),
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@UserId", SqlDbType.Int) { Value = (object)userId ?? DBNull.Value }
            };

            const string query = @"SELECT DISTINCT SearchValue
FROM
(
    SELECT CONVERT(NVARCHAR(50), MeetingInfoID) SearchValue
    FROM dbo.tblMeeting_MeetingInfo WITH (NOLOCK)
    WHERE CompanyId=@CompanyId AND (@UserId IS NULL OR CreateBy=@UserId)
    UNION
    SELECT CONVERT(NVARCHAR(50), MeetingNo) SearchValue
    FROM dbo.tblMeeting_MeetingInfo WITH (NOLOCK)
    WHERE CompanyId=@CompanyId AND MeetingNo IS NOT NULL AND (@UserId IS NULL OR CreateBy=@UserId)
) meetingSearch
WHERE SearchValue LIKE '%' + @SearchText + '%'
ORDER BY SearchValue";

            return _aCommonInternalDal.DataContainerDataTable(query, parameters, "HRDB");
        }

        public DataTable GetMeetingTitleSuggestions(string prefixText, int companyId, int? userId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SearchText", prefixText),
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@UserId", SqlDbType.Int) { Value = (object)userId ?? DBNull.Value }
            };

            const string query = @"SELECT DISTINCT Title SearchValue
FROM dbo.tblMeeting_MeetingInfo WITH (NOLOCK)
WHERE CompanyId=@CompanyId
  AND Title IS NOT NULL
  AND (@UserId IS NULL OR CreateBy=@UserId)
  AND Title LIKE '%' + @SearchText + '%'
ORDER BY SearchValue";

            return _aCommonInternalDal.DataContainerDataTable(query, parameters, "HRDB");
        }

        public DataTable GetCompanyWiseEmployeeInfoActive(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' + ISNULL(e.EmpMasterCode,'') + ' : ' + e.EmpName + ' : ' + ISNULL(d.Designation,' ') + ' : ' + ISNULL(dept.DepartmentName,' ')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
INNER JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
INNER JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1 AND e.EmployeeStatus='Active' AND  e.CompanyId = @CompanyId and
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + d.Designation + ' ' + dept.DepartmentName ) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetCompanyWiseEmployeeInfoCode(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT  ISNULL(e.EmpMasterCode,'') + ' : ' + e.EmpName + ' : ' + ISNULL(d.Designation,' ')  + ' : ' + ISNULL(dept.DepartmentName,' ')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
INNER JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
INNER JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1 AND   e.CompanyId = @CompanyId and
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + d.Designation + ' ' + dept.DepartmentName ) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetCmpWiseEmployeeInfo(string prefixText, int companyId, int dsnId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", dsnId));

            const string queryStr = @"SELECT DISTINCT (EGI.EmpMasterCode + ' : ' + EGI.EmpName + ' : ' + d.Designation + ' : ' + dept.DepartmentName ) AS EmpName FROM dbo.tblJdMaster AS JDM   WITH (nolock)
                                      INNER JOIN dbo.tblJdDetails JDD ON JDD.JdMasterId = JDM.JdMasterId
                                      INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = JDM.EmpInfoId
                                    INNER JOIN dbo.tblDesignation d ON d.DesignationId = EGI.DesignationId
                                    INNER JOIN dbo.tblDepartment dept ON dept.DepartmentId = EGI.DepartmentId
                                      WHERE EGI.CompanyId = @CompanyId AND EGI.DivisionId = @DesignationId AND (EGI.EmpMasterCode + ' : ' + EGI.EmpName + ' : ' + d.Designation + ' : ' + dept.DepartmentName ) LIKE '%'+ @SearchText +'%'";

            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetCompanyWiseEmployeeInfoOnlyForPromotionTrans(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' +  ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE e.IsActive=1 AND e.EmployeeStatus='Active' and  e.CompanyId = @CompanyId and
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + ISNULL(d.Designation,'') + ' ' + ISNULL(dept.DepartmentName, '') ) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetKPIBehaviour(string prefixText)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));


            const string queryStr = @"SELECT KPIBehaviourName FROM tblKPIBehaviour WITH (NOLOCK)
WHERE KPIBehaviourName LIKE '%'+ @SearchText +'%'
ORDER BY KPIBehaviourName asc";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetKPIBehaviourByType(string prefixText, string KPI_Type)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@KPI_Type", KPI_Type));


            const string queryStr = @"SELECT KPIBehaviourName FROM tblKPIBehaviour WITH (NOLOCK)
WHERE  Type=@KPI_Type and KPIBehaviourName LIKE '%'+ @SearchText +'%'
ORDER BY KPIBehaviourName asc";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetCompanyWiseEmployeeInfoOnlyForPromotionTransProfile(string prefixText, int companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT Top 10 CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' +  ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE   e.CompanyId = @CompanyId and
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + ISNULL(d.Designation,'') + ' ' + ISNULL(dept.DepartmentName, '') ) LIKE '%'+ @SearchText +'%'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}
