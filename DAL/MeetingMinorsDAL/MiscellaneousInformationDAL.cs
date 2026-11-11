using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;

namespace DAL.MeetingMinorsDAL
{
  public  class MiscellaneousInformationDAL
    {
      ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

      public DataTable CheckRoutingPath(string RoutingPathName, string DepartmentId)
      {
          string query = @"SELECT * FROM tblMeeting_MiscellaneousInfo  with(nolock)
	WHERE UPPER(Title)='" + RoutingPathName + "' and   CompanyId=" + DepartmentId;
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }

      public DataTable LoadApprovalPathRpt(string Mid)
      {

          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@MiscellaneousInfoId", Mid));
          const string queryStr = @"SELECT mas.IsMinimumApprovalPerson IsMinimumApprovalPerson, mas.IsEmailNotification NotificationEmail, mas.IsSMSNotification NotificationSMS, e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, * FROM dbo.tblMeeting_MiscellaneousInfoRoutingPath mas WITH (NOLOCK)
INNER JOIN tblEmpGeneralInfo e ON mas.EmpInfoId = e.EmpInfoId 
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId WHERE mas.MiscellaneousInfoId=@MiscellaneousInfoId";
          return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
      }
      public DataTable LoadApprovalLogRpt(string Mid)
      {

          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@MiscellaneousInfoId", Mid));
          const string queryStr = @"
SELECT * from(SELECT 0 Seq_No, Alg.Meeting_MiscellaneousInfoAppLogId, emp.EmpName +ISNULL(' : '+dgs.Designation,'') PreEmp, '' ForEmp, Version, Us.UserName ApproveBy,  CASE WHEN  Alg.ActionStatus='Approved' THEN 'Approved' WHEN  Alg.ActionStatus='Returned' THEN 'Returned'     WHEN  Alg.ActionStatus='Initiator' and Version=2 THEN 'Submitted'    WHEN  Alg.ActionStatus='Initiator' and Version<>2 THEN 'Re-Submitted' ELSE 'Recommended' END ActionStatus, Alg.ApprovedDate, Alg.MiscellaneousInfoId, Alg.Comments
  FROM tblMeeting_MiscellaneousInfoAppLog Alg WITH (NOLOCK)
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
 LEFT JOIN dbo.tblDesignation dgs ON emp.DesignationId = dgs.DesignationId
LEFT JOIN dbo.tblUser Us ON Alg.ApprovedBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted'  AND    Alg.MiscellaneousInfoId=@MiscellaneousInfoId
UNION all
SELECT masRp.Seq_No, 0  Meeting_MiscellaneousInfoAppLogId, emp2.EmpName  +ISNULL(' : '+dgs.Designation,'') PreEmp, '' ForEmp,'' Version,'' ApproveBy, '' ActionStatus,NULL ApprovedDate, 0 MiscellaneousInfoId,  '' Comments FROM dbo.tblMeeting_MiscellaneousInfoRoutingPath masRp  WITH (NOLOCK)
 
 
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=masRp.EmpInfoId
LEFT JOIN dbo.tblDesignation dgs ON emp2.DesignationId = dgs.DesignationId
 WHERE    masRp.EmpInfoId NOT IN(SELECT   PreEmpInfoId  FROM tblMeeting_MiscellaneousInfoAppLog WHERE MiscellaneousInfoId=@MiscellaneousInfoId) AND masRp.MiscellaneousInfoId=@MiscellaneousInfoId)  tblmain  ORDER BY Meeting_MiscellaneousInfoAppLogId asc
";
          return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
      }

      public DataTable LoadDocumentRpt(string Mid)
      {

          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@MiscellaneousInfoId", Mid));
          const string queryStr = @"SELECT ' C:\inetpub\wwwroot\UserAuthentication\UploadMeetingDocument\'+    RTRIM  (LTRIM(SUBSTRING(RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('t/',DocumentLink)+1,len(DocumentLink)))),CHARINDEX('/',RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('t/',RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('t/',DocumentLink)+1,len(DocumentLink)))))+1,len(DocumentLink)))))+1,len(DocumentLink))))  DocumentLinkPreview,  *
  FROM [dbo].[tblMeeting_MiscellaneousInfoDocument] WITH (NOLOCK) WHERE MiscellaneousInfoId=@MiscellaneousInfoId";
          return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
      }
      public DataTable LoadParticularsDetailsrpt_TotalAmountInWord(int Mid)
      {
          ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
          List<SqlParameter> aPeram = new List<SqlParameter>();

          aPeram.Add(new SqlParameter("@Amount", Mid));

          DataTable aTable = _aCommonInternalDal.GetDataByStoreProcedure("sp_GetAmountinWord", aPeram,
              DataBase.HRDB);
          return aTable;
      }

      public DataTable LoadParticularsDetailsrpt_Total(string Mid)
      {

          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", Mid));
          const string queryStr = @"SELECT * FROM  tblMemoIncrementDetails  
WHERE     PName='Total' and MemoIncrementId=@MemoIncrementId";
          return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
      }

      public DataTable GetMasterInfoDALrpt(string Id)
      {

          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@MiscellaneousInfoId", Id));
          const string queryStr = @"SELECT ISNULL('Next Approver Person: ' +Nextemp.EmpName +ISNULL(' : '+Nextdgs.Designation,''),'')  ShortName, usEmp.EmpName+ISNULL(' : '+dgs.Designation,'') CreateBy, useupmp.EmpName UpdateBy, CELog.MaxVer,  STUFF( (SELECT CONCAT(' > ', mgd.EmpName , ' ') FROM tblMeeting_MiscellaneousInfoRoutingPath mm (NOLOCK) INNER JOIN dbo.tblEmpGeneralInfo mgd ON mgd.EmpInfoId=mm.EmpInfoId WHERE mm.MiscellaneousInfoId=mas.MiscellaneousInfoId  ORDER BY mm.Seq_No asc FOR XML PATH ('') ),1,1,'') AS    address,  CASE WHEN  mas.ActionStatus='Approved' THEN 'Approved'   WHEN  mas.ActionStatus='Initiator' THEN 'Initiated' ELSE 'Recommended'end ActionStatus, * FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId  

left JOIN  dbo.tblEmpGeneralInfo Nextemp   ON  mas.RefEmpId =Nextemp.EmpInfoId  

 LEFT JOIN dbo.tblDesignation Nextdgs ON Nextemp.DesignationId = Nextdgs.DesignationId

 LEFT JOIN dbo.tblDesignation dgs ON usemp.DesignationId = dgs.DesignationId
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId
left JOIN  dbo.tblEmpGeneralInfo useupmp   ON  usUp.EmpInfoId =useupmp.EmpInfoId  
left JOIN (SELECT MiscellaneousInfoId,MAX(tblMeeting_MiscellaneousInfoAppLog.Version)MaxVertt,MAX(tblMeeting_MiscellaneousInfoAppLog.approveddate)MaxVer FROM dbo.tblMeeting_MiscellaneousInfoAppLog WHERE ActionStatus not IN
								('Drafted') GROUP BY MiscellaneousInfoId) AS CELog ON CELog.MiscellaneousInfoId= mas.MiscellaneousInfoId
							 
                              where mas.MiscellaneousInfoId=@MiscellaneousInfoId ";
          return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
      }

      public DataTable CheckRoutingPathEdit(string RoutingPathName, string DepartmentId, string PathId)
      {
          string query = @"SELECT * FROM tblMeeting_MiscellaneousInfo  with(nolock)
	WHERE UPPER(Title)='" + RoutingPathName + "' and   CompanyId=" + DepartmentId + " and  MiscellaneousInfoId not in ('" + PathId + "')";
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }
      public DataTable GetMasterDataById(string ID)
      {
          try
          {
              string query = @"	SELECT emp.EmpMasterCode+ISNULL( ' : '+ emp.EmpName,'') AwEmpName,  *
  FROM [dbo].[tblMeeting_MiscellaneousInfo] mas WITH (NOLOCK)
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.RefEmpId WHERE MiscellaneousInfoId= " +
                             ID;

              return  aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

      public DataTable GetMasterEmpDataById(string ID)
      {
          try
          {
              string query = @"	SELECT    *
  FROM [dbo].tblEmpSalaryInfo mas WITH (NOLOCK)  where   IsActive=1 and EmpInfoId= " +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

      public DataTable GetInitialByIdAudit(string ID)
      {
          try
          {
              string query = @"SELECT  us.UserName DeleteBy, com.ShortName,   *   FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
 
INNER JOIN dbo.tblMeeting_MiscellaneousInfoDetail_AuditLog MemAudit ON MemAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Initial'   AND mas.MiscellaneousInfoId=" + ID + @"

 
union all
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
 
INNER JOIN dbo.tblMeeting_MiscellaneousInfoDetail_AuditLog MemAudit ON MemAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Edit'   AND mas.MiscellaneousInfoId=" + ID + @"
union all 
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog  mas
 
INNER JOIN dbo.tblMeeting_MiscellaneousInfoDetail_AuditLog MemAudit ON MemAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Delete'   AND mas.MiscellaneousInfoId=" + ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }




      public DataTable GetMasterByIdAudit(string ID)
      {
          try
          {
              string query = @"SELECT  us.UserName DeleteBy, com.ShortName,   *   FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
 
 
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Initial'   AND mas.MiscellaneousInfoId=" + ID + @"

 
union all
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
 
 
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Edit'   AND mas.MiscellaneousInfoId=" + ID + @"
union all 
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog  mas
 
 
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Delete'   AND mas.MiscellaneousInfoId=" + ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }



      public DataTable GetDocumentListByIdAudit(string ID)
      {
          try
          {
              string query = @"SELECT    mas.AuditLog_MiscellaneousInfoId,mas.DocumentNote,mas.FileName    FROM dbo.tblMeeting_MiscellaneousInfoDocument_AuditLog mas
 
 
 
  WHERE   mas.MiscellaneousInfoId=" + ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


      public DataTable GetMemberListByIdAudit(string ID)
      {
          try
          {
              string query = @"SELECT    mas.AuditLog_MiscellaneousInfoId,mas.Type,mas.EmpMasterCode,mas.EmpName, mas.Designation    FROM dbo.tblMeeting_MiscellaneousInfoDetail_AuditLog mas
 
 
 
  WHERE   mas.MiscellaneousInfoId=" + ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


    
      public DataTable GetDocumentByIdAudit(string ID)
      {
          try
          {
              string query = @"SELECT  us.UserName DeleteBy, com.ShortName,   *   FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
 
 INNER JOIN tblMeeting_MiscellaneousInfoDocument_AuditLog docAudit ON docAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Initial'   AND mas.MiscellaneousInfoId=" + ID + @"

 
union all
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
 
 INNER JOIN tblMeeting_MiscellaneousInfoDocument_AuditLog docAudit ON docAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Edit'   AND mas.MiscellaneousInfoId=" + ID + @"
union all 
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog  mas
 
 INNER JOIN tblMeeting_MiscellaneousInfoDocument_AuditLog docAudit ON docAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Delete'   AND mas.MiscellaneousInfoId=" + ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }



      public DataTable GetEditByIdAudit(string ID)
      {
          try
          {
              string query = @"

 

SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog mas
 INNER JOIN tblMeeting_MiscellaneousInfoDocument_AuditLog docAudit ON docAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
INNER JOIN dbo.tblMeeting_MiscellaneousInfoDetail_AuditLog MemAudit ON MemAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Edit'   AND mas.MiscellaneousInfoId=" + ID ; 

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

      public DataTable GetDeleteByIdAudit(string ID)
      {
          try
          {
              string query = @"

SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MiscellaneousInfo_AuditLog  mas
 INNER JOIN tblMeeting_MiscellaneousInfoDocument_AuditLog docAudit ON docAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
INNER JOIN dbo.tblMeeting_MiscellaneousInfoDetail_AuditLog MemAudit ON MemAudit.AuditLog_MiscellaneousInfoId = mas.AuditLog_MiscellaneousInfoId
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
  WHERE mas.StatusMode='Delete'   AND mas.MiscellaneousInfoId=" + ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

      public DataTable GetMasterDataByIdAuditEdit(string ID)
      {
          try
          {
              string query = @"	SELECT emp.EmpMasterCode+ISNULL( ' : '+ emp.EmpName,'') AwEmpName,  *
  FROM [dbo].[tblMeeting_MiscellaneousInfo] mas WITH (NOLOCK)
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.RefEmpId WHERE MiscellaneousInfoId= " +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }
      public DataTable GetDocDataById(string ID)
      {
          try
          {
              string query = @"SELECT  case when  reverse(left(reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))), charindex('.', reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink)))))) - 1))='pdf' then   'http://182.160.103.234:8088/'+ RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))   when  reverse(left(reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))), charindex('.', reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink)))))) - 1))='jpg' then   'http://182.160.103.234:8088/'+ RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))  when  reverse(left(reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))), charindex('.', reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink)))))) - 1))='png' then   'http://182.160.103.234:8088/'+ RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))    when  reverse(left(reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))), charindex('.', reverse(RTRIM(LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink)))))) - 1))='png' then   'http://182.160.103.234:8088/'+ RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink)))) else  'https://docs.google.com/gview?url=http://182.160.103.234:8088/'+ RTRIM  (LTRIM(SUBSTRING(DocumentLink,CHARINDEX('/',DocumentLink)+1,len(DocumentLink))))+'&embedded=true'
end  DocumentLinkPreview,  *
  FROM [dbo].[tblMeeting_MiscellaneousInfoDocument] WITH (NOLOCK)


 WHERE MiscellaneousInfoId=" +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }
      public DataTable GetDocDataByIdAudit(string ID)
      {
          try
          {
              string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MiscellaneousInfoDocument_AuditLog] WITH (NOLOCK) WHERE MiscellaneousInfoId=" +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


      public DataTable GetMemberListDataById(string ID)
      {
          try
          {
              string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MiscellaneousInfoDetail] WITH (NOLOCK) WHERE MiscellaneousInfoId=" +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }


      }


      public DataTable GetMemberListDataByIdAudit(string ID)
      {
          try
          {
              string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MiscellaneousInfoDetail_AuditLog] WITH (NOLOCK) WHERE MiscellaneousInfoId=" +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

      public DataTable GetAppLogCommByJobId(string MasterId)
      {
          string query = @"
SELECT * from(SELECT 0 Seq_No, Alg.Meeting_MiscellaneousInfoAppLogId, emp.EmpName PreEmp, '' ForEmp, Version, Us.UserName ApproveBy, CASE WHEN  Alg.ActionStatus='Approved' THEN 'Approved'  WHEN  Alg.ActionStatus='Returned' THEN 'Returned'    WHEN  Alg.ActionStatus='Initiator' THEN 'Initiated' ELSE 'Recommended' END ActionStatus, Alg.ApprovedDate, Alg.MiscellaneousInfoId, Alg.Comments
  FROM tblMeeting_MiscellaneousInfoAppLog Alg WITH (NOLOCK)
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId

LEFT JOIN dbo.tblUser Us ON Alg.ApprovedBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted'  AND    Alg.MiscellaneousInfoId=" + MasterId+ @"
UNION all
SELECT masRp.Seq_No, 0  Meeting_MiscellaneousInfoAppLogId, emp2.EmpName PreEmp, '' ForEmp,'' Version,'' ApproveBy, '' ActionStatus,NULL ApprovedDate, 0 MiscellaneousInfoId,  '' Comments FROM dbo.tblMeeting_MiscellaneousInfoRoutingPath masRp  WITH (NOLOCK)
 
 
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=masRp.EmpInfoId
 WHERE    masRp.EmpInfoId NOT IN(SELECT   PreEmpInfoId  FROM tblMeeting_MiscellaneousInfoAppLog WHERE MiscellaneousInfoId=" + MasterId + @") AND masRp.MiscellaneousInfoId=" + MasterId + @")  tblmain  ORDER BY Seq_No ASC

";
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }

      public DataTable GetAppLogCommByJobId2(string MasterId)
      {
          string query = @"
SELECT * from(SELECT 0 Seq_No, Alg.Meeting_MiscellaneousInfoAppLogId,   emp.EmpName  PreEmp, '' ForEmp, Version, Us.UserName ApproveBy, CASE WHEN  Alg.ActionStatus='Approved' THEN 'Approved' WHEN  Alg.ActionStatus='Returned' THEN 'Returned'     WHEN  Alg.ActionStatus='Initiator' THEN 'Initiated' ELSE 'Recommended' END ActionStatus, Alg.ApprovedDate, Alg.MiscellaneousInfoId, Alg.Comments
  FROM tblMeeting_MiscellaneousInfoAppLog Alg WITH (NOLOCK)
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId

LEFT JOIN dbo.tblUser Us ON Alg.ApprovedBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted'  AND    Alg.MiscellaneousInfoId=" + MasterId + @"
UNION all
SELECT masRp.Seq_No, 0  Meeting_MiscellaneousInfoAppLogId, emp2.EmpName PreEmp, '' ForEmp,'' Version,'' ApproveBy, '' ActionStatus,NULL ApprovedDate, 0 MiscellaneousInfoId,  '' Comments FROM dbo.tblMeeting_MiscellaneousInfoRoutingPath masRp  WITH (NOLOCK)
 
 
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=masRp.EmpInfoId
 WHERE    masRp.EmpInfoId NOT IN(SELECT   PreEmpInfoId  FROM tblMeeting_MiscellaneousInfoAppLog WHERE MiscellaneousInfoId=" + MasterId + @") AND masRp.MiscellaneousInfoId=" + MasterId + @")  tblmain  ORDER BY Seq_No ASC

";
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }
      public DataTable GetDelsDataById(string ID)
      {
          try
          {
              string query = @"
 SELECT mas.IsMinimumApprovalPerson IsMinimumApprovalPerson, mas.IsEmailNotification NotificationEmail, mas.IsSMSNotification NotificationSMS, e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, * FROM dbo.tblMeeting_MiscellaneousInfoRoutingPath mas WITH (NOLOCK)
INNER JOIN tblEmpGeneralInfo e ON mas.EmpInfoId = e.EmpInfoId 
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId WHERE mas.MiscellaneousInfoId= " +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


      public DataTable GetMemberListById(string ID)
      {
          try
          {
              string query = @"SELECT    * FROM dbo.tblMeeting_MiscellaneousInfoDetail mas WITH (NOLOCK) WHERE mas.MiscellaneousInfoId= " +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }
      public void GetCompanyListIntoDropdown(DropDownList ddl)
      {
          string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
          aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
      }


      public void GetCompanyListIntoDropdownAll(DropDownList ddl)
      {
          string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo ";
          aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
      }
      public void GetCategoryListIntoDropdown(DropDownList ddl)
      {
          string queryStr = @"
SELECT [CategoryID]
      ,[MeetingCategory]
  FROM [dbo].[tblMeeting_Category] WITH (NOLOCK)";
          aCommonInternalDal.LoadDropDownValue(ddl, "MeetingCategory", "CategoryID", queryStr, "HRDB");
      }
      public DataTable LoadInfo(string parameter)
      {
          string query = @"SELECT CASE   WHEN mas.ActionStatus='Drafted'  THEN  'True' WHEN mas.ActionStatus='Returned' THEN  'True' ELSE  'False' END AS isEditBtn,  emp.EmpMasterCode+ISNULL( ' : '+ emp.EmpName,'') AwEmpName, CASE WHEN mas.ActionStatus='Returned' THEN mas.ActionStatus+ ISNULL(' ['+mas.ReturnComments+']','' ) ELSE  mas.ActionStatus END   ActionStatus,com.ShortName,  usEmp.EmpName+ISNULL(' : '+dgs.Designation,'') CreateBy, usUp.UserName UpdateBy, * FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.RefEmpId
 LEFT JOIN dbo.tblDesignation dgs ON emp.DesignationId = dgs.DesignationId

  left JOIN  dbo.tblEmpGeneralInfo usEmp   ON  us.EmpInfoId =usEmp.EmpInfoId
  

    
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId  WHERE mas.MiscellaneousInfoId IS NOT NULL
" + parameter + " ORDER BY mas.CreateDate desc ";

          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }


      public DataTable LoadInfoDocumentAuditTrail(string parameter, string parameter2)
      {
          string query = @" SELECT mas.StatusMode, CASE WHEN  mas.StatusMode='Edit' THEN  'btn btn-sm btn-warning'   ELSE  'btn btn-sm btn-danger' end  StatusStyle,  com.ShortName, mas.MiscellaneousInfoId,   mas.ActionStatus  ActionStatus,mas.Title,mas.Purpose  

 FROM tblMeeting_MiscellaneousInfo_AuditLog  mas  WITH (NOLOCK)

LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.RefEmpId
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId  

WHERE mas.AuditLog_MiscellaneousInfoId = 
  (SELECT max(AuditLog_MiscellaneousInfoId) FROM tblMeeting_MiscellaneousInfo_AuditLog t2 WHERE t2.MiscellaneousInfoId = mas.MiscellaneousInfoId)  " + parameter;

          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }
      public DataTable LoadInfoApproveList()
      {
          string query = @"		


SELECT com.ShortName,  usEmp.EmpName+ISNULL(' : '+dgs.Designation,'') CreateBy, usUp.UserName UpdateBy, * FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId   
 LEFT JOIN dbo.tblDesignation dgs ON usemp.DesignationId = dgs.DesignationId
 
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId

 INNER JOIN (SELECT MiscellaneousInfoId,MAX(Version)MaxVer FROM dbo.tblMeeting_MiscellaneousInfoAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MiscellaneousInfoId) AS CELog ON CELog.MiscellaneousInfoId= mas.MiscellaneousInfoId
								INNER JOIN dbo.tblMeeting_MiscellaneousInfoAppLog ON tblMeeting_MiscellaneousInfoAppLog.MiscellaneousInfoId = mas.MiscellaneousInfoId
                                where   ((Version=CELog.MaxVer) OR (Version IS NULL))    and  ForEmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }

      public DataTable LoadInfoApproveListCheck(int EmpId)
      {
          string query = @"		


SELECT com.ShortName,  usEmp.EmpName+ISNULL(' : '+dgs.Designation,'') CreateBy, usUp.UserName UpdateBy, * FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId   
 LEFT JOIN dbo.tblDesignation dgs ON usemp.DesignationId = dgs.DesignationId
 
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId

 INNER JOIN (SELECT MiscellaneousInfoId,MAX(Version)MaxVer FROM dbo.tblMeeting_MiscellaneousInfoAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MiscellaneousInfoId) AS CELog ON CELog.MiscellaneousInfoId= mas.MiscellaneousInfoId
								INNER JOIN dbo.tblMeeting_MiscellaneousInfoAppLog ON tblMeeting_MiscellaneousInfoAppLog.MiscellaneousInfoId = mas.MiscellaneousInfoId
                                where   ((Version=CELog.MaxVer) OR (Version IS NULL))    and  ForEmpInfoId='" + EmpId + "'";

          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }

      public DataTable LoadInfoApprovedListDone()
      {
          string query = @"		SELECT com.ShortName, usemp.EmpName CreateBy, usUp.UserName UpdateBy, * FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId  

 
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId

 INNER JOIN (SELECT MiscellaneousInfoId,MAX(Version)MaxVer FROM dbo.tblMeeting_MiscellaneousInfoAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MiscellaneousInfoId) AS CELog ON CELog.MiscellaneousInfoId= mas.MiscellaneousInfoId
								INNER JOIN dbo.tblMeeting_MiscellaneousInfoAppLog ON tblMeeting_MiscellaneousInfoAppLog.MiscellaneousInfoId = mas.MiscellaneousInfoId
                                where   ((Version=CELog.MaxVer) OR (Version IS NULL)) AND tblMeeting_MiscellaneousInfoAppLog.ActionStatus='Approved' AND ForEmpInfoId=0  and  tblMeeting_MiscellaneousInfoAppLog.PreEmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }

      public DataTable LoadInfoApprovedListDoneReport()
      {
          string query = @"	SELECT distinct  com.ShortName, usemp.EmpName CreateBy, usUp.UserName UpdateBy, mas.MiscellaneousInfoId, mas.Title, mas.Purpose, mas.CreateDate FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId  

 
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId
inner join ( select MiscellaneousInfoId,EmpInfoId  from tblMeeting_MiscellaneousInfoRoutingPath
) tbl on tbl.MiscellaneousInfoId=mas.MiscellaneousInfoId

 left JOIN (SELECT MiscellaneousInfoId,MAX(Version)MaxVer FROM dbo.tblMeeting_MiscellaneousInfoAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MiscellaneousInfoId) AS CELog ON CELog.MiscellaneousInfoId= mas.MiscellaneousInfoId
								INNER JOIN dbo.tblMeeting_MiscellaneousInfoAppLog ON tblMeeting_MiscellaneousInfoAppLog.MiscellaneousInfoId = mas.MiscellaneousInfoId
                                where   ((Version=CELog.MaxVer) OR (Version IS NULL)) and mas.ActionStatus !='Drafted' and ((tbl.EmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "') or (mas.CreateBy='" + HttpContext.Current.Session["UserId"].ToString() + "'))";

          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }

      public DataTable LoadInfoApprovedListDoneReportforAdmin()
      {
          string query = @"		SELECT com.ShortName, usemp.EmpName CreateBy, usUp.UserName UpdateBy, * FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId  

 
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId
 
                                where    mas.ActionStatus !='Drafted' ";

          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }
      public void GetUserListDropdown(DropDownList ddl, string ComId)
      {
          string queryStr = @"SELECT us.UserName+ ISNULL(' : '+emp.EmpName,'') UserName, us.UserId FROM tblUser us WITH (NOLOCK)
left JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = us.EmpInfoId
WHERE us.IsActive=1 AND emp.CompanyId=" + ComId;
          aCommonInternalDal.LoadDropDownValue(ddl, "UserName", "UserId", queryStr, "HRDB");
      }

      public void GetMiscellaneousKeySearchDropdown(DropDownList ddl, string ComId, string UserId)
      {
          string queryStr = @"SELECT KeySearch FROM dbo.tblMeeting_MiscellaneousInfo
WITH (NOLOCK)  WHERE CompanyId=" + ComId + "  AND CreateBy=" + UserId + " ORDER BY KeySearch asc";
          aCommonInternalDal.LoadDropDownValue(ddl, "KeySearch", "KeySearch", queryStr, "HRDB");
      }

      public void GetMiscellaneousKeySearchDropdown(DropDownList ddl, string ComId)
      {
          string queryStr = @"SELECT KeySearch FROM dbo.tblMeeting_MiscellaneousInfo
WITH (NOLOCK)  WHERE CompanyId=" + ComId + " ORDER BY KeySearch asc";
          aCommonInternalDal.LoadDropDownValue(ddl, "KeySearch", "KeySearch", queryStr, "HRDB");
      }
      public void GetDepartmentByDivList(DropDownList ddl, string divisionId)
      {
          var aSqlParameterlist = new List<SqlParameter>();

          aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

          string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
          aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
      }
      public DataTable GetEMpInfos(string param)
      {
          string query = @"SELECT e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, * FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
 WHERE   e.EmpInfoId IS NOT NULL and e.IsActive=1   " + param + " ORDER BY e.EmpMasterCode DESC ";

          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }
      public DataTable GetEMpInfoFromRoutingPath(string param)
      {
          string query = @"SELECT  0 IsMinimumApprovalPerson, 0 CanEdit, 0 NotificationEmail,0  NotificationSMS, e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, * FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN tblMeeting_RoutingPathSetupDetails rotDtl ON rotDtl.EmpInfoId = e.EmpInfoId 
INNER JOIN tblMeeting_RoutingPathSetupMaster rotMas ON rotMas.RoutingPathMaster_ID = rotDtl.RoutingPathMaster_ID

INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
 WHERE   e.EmpInfoId IS NOT NULL  AND rotMas.RoutingPathMaster_ID=" + param + " ORDER BY e.EmpMasterCode DESC ";

          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }

      public DataTable GetEMpInfoFromRoutingPathidd(string param, string checkId)
      {
          string query = @"SELECT  0 IsMinimumApprovalPerson, 0 CanEdit, 0 NotificationEmail,0  NotificationSMS, e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, * FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN tblMeeting_RoutingPathSetupDetails rotDtl ON rotDtl.EmpInfoId = e.EmpInfoId 
INNER JOIN tblMeeting_RoutingPathSetupMaster rotMas ON rotMas.RoutingPathMaster_ID = rotDtl.RoutingPathMaster_ID

INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
 WHERE   e.EmpInfoId IS NOT NULL and rotDtl.EmpInfoId=" + checkId + "  AND rotMas.RoutingPathMaster_ID=" + param + " ORDER BY e.EmpMasterCode DESC ";

          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }

      public DataTable GetDDLComDivision(string cid)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@cid", cid));
          string query = @"SELECT d.DivisionId AS Value, d.DivisionName AS TextField FROM dbo.tblDivision d WHERE IsActive=1 and d.CompanyId=@cid";
          return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
      }


      public DataTable GetDDLEmpInfo(string cid)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@cid", cid));
          string query = @"SELECT  EmpInfoId AS Value,  EmpMasterCode+' ; '+EmpName AS TextField FROM dbo.tblEmpGeneralInfo WHERE IsActive=1 AND CompanyId=@cid";
          return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
      }
      public DataTable GetDDLEmpInfoWith()
      {
          string query = @"SELECT  EmpInfoId AS Value,  EmpMasterCode+' : '+EmpName AS TextField FROM dbo.tblEmpGeneralInfo WHERE IsActive=1";
          return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
      }
      public DataTable GetDDLRoutingPath(string cid)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@cid", cid));
          string query = @"SELECT d.RoutingPathMaster_ID AS Value, d.RoutingPath_Name AS TextField FROM dbo.tblMeeting_RoutingPathSetupMaster d WITH (NOLOCK) WHERE d.CompanyId=@cid";
          return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
      }

      public DataTable GetDDLRoutingPathDepartment(string cid, string DeptID)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));
          aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DeptID));
          string query = @"SELECT d.RoutingPathMaster_ID AS Value, d.RoutingPath_Name AS TextField, d.DepartmentId FROM dbo.tblMeeting_RoutingPathSetupMaster d WITH (NOLOCK) WHERE d.CompanyId=@CompanyId AND d.DepartmentId=@DepartmentId";
          return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
      }

      public DataTable GetDDLRoutingPathDivision(string cid, string DeptID)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));
          aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DeptID));
          string query = @"SELECT d.RoutingPathMaster_ID AS Value, d.RoutingPath_Name AS TextField, d.DepartmentId FROM dbo.tblMeeting_RoutingPathSetupMaster d WITH (NOLOCK) WHERE d.CompanyId=@CompanyId AND d.DivisionId=@DepartmentId";
          return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
      }
      public bool UpdateAreaInfo(AreaInformationDao aInformationDao)
      {
          var aSqlParameterlist = new List<SqlParameter>();

          aSqlParameterlist.Add(new SqlParameter("@AreaId", aInformationDao.AreaId));
          aSqlParameterlist.Add(new SqlParameter("@RegionId", aInformationDao.RegionId));
          aSqlParameterlist.Add(new SqlParameter("@AreaName", aInformationDao.AreaName));
          aSqlParameterlist.Add(new SqlParameter("@AreaCode", aInformationDao.AreaCode));
          aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
          aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
          aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
          aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
          aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

          const string queryStr = @"UPDATE tblArea SET RegionId = @RegionId, AreaName = @AreaName,AreaCode = @AreaCode,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE AreaId = @AreaId";

          return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
      }
      public bool UpdateEmpSalStatus(int SalEmpId)
      {
          var aSqlParameterlist = new List<SqlParameter>();

          aSqlParameterlist.Add(new SqlParameter("@EmpSalaryInfoId", SalEmpId));
          

          const string queryStr = @"UPDATE [dbo].[tblEmpSalaryInfo]
   SET  [IsActive] =0,ActiveDate=GETDATE()
     
 WHERE EmpSalaryInfoId=@EmpSalaryInfoId";

          return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
      }
      public Int32 SaveDivisionInfo(DivisionInformationDao aInformationDao)
      {
          var aSqlParameterlist = new List<SqlParameter>();

          aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
          aSqlParameterlist.Add(new SqlParameter("@DivisionName", aInformationDao.DivisionName));
          aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
          aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
          aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
          aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
          aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
          aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
          aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

          const string insertQuery = @"INSERT INTO tblDivision (CompanyId,DivisionName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@CompanyId,@DivisionName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

          return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
      }

      public DataTable GetEmpRoutingPath(string MasId, string SeqNo)
      {
          try
          {
              string query = @"SELECT emp.EmpMasterCode+ISNULL( ' , '+ emp.EmpName,'') AwEmpName, mas.EmpInfoId, mas.Seq_No, * FROM tblMeeting_MiscellaneousInfoRoutingPath mas WITH (NOLOCK)
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.EmpInfoId
WHERE mas.MiscellaneousInfoId=" + MasId + "  and mas.Seq_No > " + SeqNo + "  ORDER BY mas.Seq_No ASC ";

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


      public DataTable GetEmpCanEditDOC(string MasId)
      {
          try
          {
              string query = @"SELECT CanEdit, EmpInfoId FROM tblMeeting_MiscellaneousInfoRoutingPath
WHERE MiscellaneousInfoId=" + MasId + "  and EmpInfoId =" + HttpContext.Current.Session["EmpInfoId"].ToString() + "";

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


      public DataTable GetEmpAllApprovalInfo(string MasId)
      {
          try
          {
              string query = @"SELECT CanEdit, EmpInfoId FROM tblMeeting_MiscellaneousInfoRoutingPath
WHERE MiscellaneousInfoId=" + MasId ;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


      public DataTable GetEmpEntryBy(string MasId)
      {
          try
          {
              string query = @"SELECT us.EmpInfoId FROM tblMeeting_MiscellaneousInfo  mas
LEFT JOIN dbo.tblUser us ON us.UserId=mas.CreateBy
WHERE mas.MiscellaneousInfoId=" + MasId;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }



      public DataTable GetCheckMinimumApproval(string MasId)
      {
          try
          {
              string query = @"SELECT  mas.IsMinimumApprovalPerson FROM dbo.tblMeeting_MiscellaneousInfoRoutingPath mas WITH (NOLOCK)
 
WHERE mas.MiscellaneousInfoId=" + MasId + "   AND mas.EmpInfoId= " + HttpContext.Current.Session["EmpInfoId"].ToString() + " ";

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

      public DataTable GetEmpRoutingPath2(string MasId, string SeqNo)
      {
          try
          {
              string query = @"SELECT emp.EmpMasterCode+ISNULL( ' , '+ emp.EmpName,'') AwEmpName, mas.EmpInfoId, mas.Seq_No, * FROM tblMeeting_MiscellaneousInfoRoutingPath mas WITH (NOLOCK)
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.EmpInfoId
WHERE mas.MiscellaneousInfoId=" + MasId + "  and mas.Seq_No >= " + SeqNo + "  ORDER BY mas.Seq_No ASC ";

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }


      public DataTable GetEmpRoutingPathRreturnfroEntry(string MasId)
      {
          try
          {
              string query = @"SELECT  * FROM tblMeeting_MiscellaneousInfo   WITH (NOLOCK) WHERE MiscellaneousInfoId=" + MasId ;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

      public DataTable GetEmpRoutingPathRreturn(string MasId, string SeqNo)
      {
          try
          {
              string query = @"SELECT mas.EmpInfoId, mas.Seq_No, * FROM tblMeeting_MiscellaneousInfoRoutingPath mas WITH (NOLOCK)
WHERE mas.MiscellaneousInfoId=" + MasId + "  and mas.Seq_No < " + SeqNo + " ORDER BY mas.Seq_No desc ";

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }
      }
      public int SavAppLog(MiscellaneousInfoAppLogDAO appLogDao)
      {

          try
          {
              int pk = 0;


              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@MiscellaneousInfoId", appLogDao.MiscellaneousInfoId));
                  aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                  aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                
                  aParameters.Add(new SqlParameter("@ApprovedBy", appLogDao.ApprovedBy));
                  aParameters.Add(new SqlParameter("@ApprovedDate", appLogDao.ApprovedDate));
                  aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                  aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));



                  string query = @"INSERT INTO [dbo].[tblMeeting_MiscellaneousInfoAppLog]
           ([MiscellaneousInfoId]
           ,[PreEmpInfoId]
           ,[ForEmpInfoId]
           ,[Version]
           ,[ApprovedBy]
           ,[ApprovedDate]
           ,[ActionStatus]
           ,[Comments])
     VALUES
           (@MiscellaneousInfoId
           ,@PreEmpInfoId
           ,@ForEmpInfoId
           ,(SELECT (COUNT(*)+1) FROM dbo.tblMeeting_MiscellaneousInfoAppLog WITH (NOLOCK) WHERE MiscellaneousInfoId=@MiscellaneousInfoId)
           ,@ApprovedBy
           ,@ApprovedDate
           ,@ActionStatus
           ,@Comments)";

                  pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
              }


              return pk;
          }
          catch (Exception exception)
          {

              throw exception;
          }
      }
      public int SaveMaster(MiscellaneousInfoDAO aMaster, string user)
      {
          try
          {
              if (aMaster.MiscellaneousInfoId > 0)
              {
                  /////asdasddasd
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@MiscellaneousInfoId", aMaster.MiscellaneousInfoId));
                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@Title", aMaster.Title));
                  aParameters.Add(new SqlParameter("@Purpose", aMaster.Purpose));
                  aParameters.Add(new SqlParameter("@KeySearch", aMaster.KeySearch));
                  aParameters.Add(new SqlParameter("@UpdateBy", user));

                  aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));


                  aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
                  aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
                  aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
                  aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));
                  aParameters.Add(new SqlParameter("@RefMinAppCount", aMaster.RefMinAppCount));
                  aParameters.Add(new SqlParameter("@RefMinAppCountCheck", aMaster.RefMinAppCountCheck));

                  string query = @"
UPDATE [dbo].[tblMeeting_MiscellaneousInfo]
   SET [CompanyId] = @CompanyId 
      ,[Title] = @Title 
      ,[Purpose] = @Purpose 
   , KeySearch=@Title+ ' '+@Purpose + ' '+ @KeySearch
      ,[UpdateBy] = @UpdateBy 
      ,[UpdateDate] = @UpdateDate, Isapproved=0, ActionStatus=@ActionStatus, RefEmpId=@RefEmpId, RefSeqNo=@RefSeqNo, RefMinAppCount=@RefMinAppCount, RefMinAppCountCheck=@RefMinAppCountCheck
 WHERE  MiscellaneousInfoId=@MiscellaneousInfoId";

                  bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                  if (result == true)
                  {
                      return aMaster.MiscellaneousInfoId;
                  }
                  else
                  {
                      return 0;
                  }

              }
              else
              {


                  List<SqlParameter> aParameters = new List<SqlParameter>();

                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@Title", aMaster.Title));
                  aParameters.Add(new SqlParameter("@Purpose", aMaster.Purpose));
                  aParameters.Add(new SqlParameter("@KeySearch", aMaster.KeySearch));
                  aParameters.Add(new SqlParameter("@CreateBy", user));
                
                  aParameters.Add(new SqlParameter("@CreateDate", System.DateTime.Now));

                  aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
                  aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
                  aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
                  aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));
                  aParameters.Add(new SqlParameter("@RefMinAppCount", aMaster.RefMinAppCount));
                  aParameters.Add(new SqlParameter("@RefMinAppCountCheck", aMaster.RefMinAppCountCheck));

                  string query = @"
INSERT INTO [dbo].[tblMeeting_MiscellaneousInfo]
           ([CompanyId]
           ,[Title]
           ,[Purpose]
           ,[CreateBy]
           ,[CreateDate],
KeySearch,Isapproved,ActionStatus,RefEmpId,RefSeqNo,RefMinAppCount,RefMinAppCountCheck
           )
     VALUES
           (@CompanyId 
           ,@Title 
           ,@Purpose 
           ,@CreateBy 
           ,@CreateDate, @Title+ ' '+@Purpose + ' '+ @KeySearch,@Isapproved,@ActionStatus,@RefEmpId,@RefSeqNo,@RefMinAppCount,@RefMinAppCountCheck)";

                  int pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                  return pk;
              }
          }
          catch (Exception ex)
          {

              throw;
          }
      }

      public int SaveEmpSalaryMaster(EmpSalaryInfoDAO aMaster, string user)
      {
          try
          {
              if (aMaster.EmpSalaryInfoId > 0)
              {
                  /////asdasddasd
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                  
                  aParameters.Add(new SqlParameter("@BasicPay", aMaster.BasicPay ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@HouseRent", aMaster.HouseRent ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Medical", aMaster.Medical ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Conveyance", aMaster.Conveyance ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Washing", aMaster.Washing ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@PaymentType", aMaster.PaymentType ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@BankNameId", aMaster.BankNameId ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@BankAccountNo", aMaster.BankAccountNo ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@ProvidentFundEligible", aMaster.ProvidentFundEligible ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@PF", aMaster.PF ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@MonthlyTax", aMaster.MonthlyTax ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@UpdateBy", user));

                  aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));


            

                  string query = @"
UPDATE [dbo].[tblEmpSalaryInfo]
   SET [EmpInfoId] = @EmpInfoId 
      ,[BasicPay] = @BasicPay 
      ,[HouseRent] = @HouseRent 
      ,[Medical] = @Medical 
      ,[Conveyance] = @Conveyance 
      ,[Washing] = @Washing 
      ,[PaymentType] = @PaymentType 
      ,[BankNameId] = @BankNameId 
      ,[BankAccountNo] = @BankAccountNo 
      ,[ProvidentFundEligible] = @ProvidentFundEligible 
      ,[PF] = @PF 
      ,[MonthlyTax] = @MonthlyTax 
 where EmpInfoId=@EmpInfoId    UPDATE [dbo].[tblEmpGeneralInfo]
   SET  Updateby=@UpdateBy, UpdateDate=@UpdateDate
  where EmpInfoId=@EmpInfoId";

                  bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                  if (result == true)
                  {
                      return aMaster.EmpInfoId;
                  }
                  else
                  {
                      return 0;
                  }

              }
              else
              {


                  List<SqlParameter> aParameters = new List<SqlParameter>();
               
                  aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId ));
                  aParameters.Add(new SqlParameter("@BasicPay", aMaster.BasicPay ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@HouseRent", aMaster.HouseRent ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Medical", aMaster.Medical ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Conveyance", aMaster.Conveyance ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Washing", aMaster.Washing ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@PaymentType", aMaster.PaymentType ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@BankNameId", aMaster.BankNameId ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@BankAccountNo", aMaster.BankAccountNo ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@ProvidentFundEligible", aMaster.ProvidentFundEligible ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@PF", aMaster.PF ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@MonthlyTax", aMaster.MonthlyTax ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@UpdateBy", user));

                  aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                  string query = @"
INSERT INTO [dbo].[tblEmpSalaryInfo]
           ([EmpInfoId]
           ,[BasicPay]
           ,[HouseRent]
           ,[Medical]
           ,[Conveyance]
           ,[Washing]
           ,[PaymentType]
           ,[BankNameId]
           ,[BankAccountNo]
           ,[ProvidentFundEligible]
           ,[PF]
           ,[MonthlyTax],IsActive)
     VALUES
           (@EmpInfoId 
           ,@BasicPay 
           ,@HouseRent 
           ,@Medical 
           ,@Conveyance 
           ,@Washing 
           ,@PaymentType 
           ,@BankNameId 
           ,@BankAccountNo 
           ,@ProvidentFundEligible 
           ,@PF 
           ,@MonthlyTax,1 )      UPDATE [dbo].[tblEmpGeneralInfo]
   SET  Updateby=@UpdateBy, UpdateDate=@UpdateDate
  where EmpInfoId=@EmpInfoId";

                  int pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                  return pk;
              }
          }
          catch (Exception ex)
          {

              throw;
          }
      }



      public bool SaveEmpSalaryMasterForInsert(int OldEmpID, int NewId)
      {
          try
          {
              List<SqlParameter> aParameters = new List<SqlParameter>();
              aParameters.Add(new SqlParameter("@oldEmpId", OldEmpID));
              aParameters.Add(new SqlParameter("@EmpInfoId", NewId));
              string queryDel = @"
INSERT INTO [dbo].[tblEmpSalaryInfo]
           ([EmpInfoId]
           ,[BasicPay]
           ,[HouseRent]
           ,[Medical]
           ,[Conveyance]
           ,[Washing]
           ,[PaymentType]
           ,[BankNameId]
           ,[BankAccountNo]
           ,[ProvidentFundEligible]
           ,[PF]
           ,[MonthlyTax]
           ,[BranchName]
           ,[RoutingNo]
           ,[AccountName]
           ,[IsActive]
           ,[ActiveDate]
           ,[InactiveDate])
  select @EmpInfoId
           ,[BasicPay]
           ,[HouseRent]
           ,[Medical]
           ,[Conveyance]
           ,[Washing]
           ,[PaymentType]
           ,[BankNameId]
           ,[BankAccountNo]
           ,[ProvidentFundEligible]
           ,[PF]
           ,[MonthlyTax]
           ,[BranchName]
           ,[RoutingNo]
           ,[AccountName]
           ,[IsActive]
           ,[ActiveDate]
           ,[InactiveDate] from [tblEmpSalaryInfo] where EmpInfoId=@oldEmpId";

              bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParameters, DataBase.HRDB);

              return delRes;
          }
          catch (Exception ex)
          {

              throw;
          }
      }

      public
             bool SaveRoutingPathDetails(List<MiscellaneousInfoRoutingPathDAO> aList, int masterid)
      {
          try
          {
              List<SqlParameter> aParametersd = new List<SqlParameter>();
              aParametersd.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
              string queryDel = @"DELETE FROM [dbo].[tblMeeting_MiscellaneousInfoRoutingPath]
       WHERE  MiscellaneousInfoId=@MiscellaneousInfoId";

              bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


              bool result = false;
              foreach (var item in aList)
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();

                  aParameters.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
                  aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                  aParameters.Add(new SqlParameter("@Seq_No", item.Seq_No));
                  aParameters.Add(new SqlParameter("@CanEdit", item.CanEdit));
                  aParameters.Add(new SqlParameter("@IsEmailNotification", item.IsEmailNotification));
                  aParameters.Add(new SqlParameter("@IsSMSNotification", item.IsSMSNotification));

                
                  aParameters.Add(new SqlParameter("@IsMinimumApprovalPerson", item.IsMinimumApprovalPerson));
                



                  string query = @"
INSERT INTO [dbo].[tblMeeting_MiscellaneousInfoRoutingPath]
           ([MiscellaneousInfoId]
           ,[EmpInfoId]
           ,[Seq_No]
           ,[CanEdit]
           ,[IsEmailNotification]
           ,[IsSMSNotification]
           ,[IsMinimumApprovalPerson])
     VALUES
           (@MiscellaneousInfoId 
           ,@EmpInfoId 
           ,@Seq_No 
           ,@CanEdit 
           ,@IsEmailNotification 
           ,@IsSMSNotification 
           ,@IsMinimumApprovalPerson)";
                  result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                  if (result == false)
                  {
                      return false;
                  }


              }
              return result;


          }
          catch (Exception x)
          {

              throw;
          }
      }
      public bool UpdateApprovalMasterById(MiscellaneousInfoDAO aMaster)
      {
          var aParameters = new List<SqlParameter>();
          aParameters.Add(new SqlParameter("@MiscellaneousInfoId", aMaster.MiscellaneousInfoId));


          aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
          aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
          aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
          aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));
    
       


          const string query = @" UPDATE [dbo].[tblMeeting_MiscellaneousInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo 
      
      ,[RefMinAppCountCheck] = ((SELECT RefMinAppCountCheck FROM tblMeeting_MiscellaneousInfo WHERE MiscellaneousInfoId=@MiscellaneousInfoId)+1 )
 WHERE MiscellaneousInfoId= @MiscellaneousInfoId";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
      }

      public bool UpdateApprovalMasterByIdForReturn(MiscellaneousInfoDAO aMaster)
      {
          var aParameters = new List<SqlParameter>();
          aParameters.Add(new SqlParameter("@MiscellaneousInfoId", aMaster.MiscellaneousInfoId));


          aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
          aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
          aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
          aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));
          aParameters.Add(new SqlParameter("@ReturnComments", aMaster.ReturnComments));




          const string query = @" UPDATE [dbo].[tblMeeting_MiscellaneousInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo , ReturnComments=@ReturnComments
      
      ,[RefMinAppCountCheck] = ((SELECT RefMinAppCountCheck FROM tblMeeting_MiscellaneousInfo WHERE MiscellaneousInfoId=@MiscellaneousInfoId)+1 )
 WHERE MiscellaneousInfoId= @MiscellaneousInfoId";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
      }
    

      public bool FinalUpdateApprovalMasterById(MiscellaneousInfoDAO aMaster)
      {
          var aParameters = new List<SqlParameter>();
          aParameters.Add(new SqlParameter("@MiscellaneousInfoId", aMaster.MiscellaneousInfoId));


          aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
          aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
          aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
         




          const string query = @" UPDATE [dbo].[tblMeeting_MiscellaneousInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus ,RefEmpId=@RefEmpId
      
      
      
 WHERE MiscellaneousInfoId= @MiscellaneousInfoId ";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
      }


      public bool UpdateApprovalMasterReturrnById(MiscellaneousInfoDAO aMaster)
      {
          var aParameters = new List<SqlParameter>();
          aParameters.Add(new SqlParameter("@MiscellaneousInfoId", aMaster.MiscellaneousInfoId));


          aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
          aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
          aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
          aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));




          const string query = @" UPDATE [dbo].[tblMeeting_MiscellaneousInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo 
      
      ,[RefMinAppCountCheck] = ((SELECT RefMinAppCountCheck FROM tblMeeting_MiscellaneousInfo WHERE MiscellaneousInfoId=@MiscellaneousInfoId)-1 )
 WHERE MiscellaneousInfoId= @MiscellaneousInfoId";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
      }

      public bool UpdateApprovalMasterforNotIsMinApprovalPersonById(MiscellaneousInfoDAO aMaster)
      {
          var aParameters = new List<SqlParameter>();
          aParameters.Add(new SqlParameter("@MiscellaneousInfoId", aMaster.MiscellaneousInfoId));


          aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
          aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
          aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
          aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));




          const string query = @" UPDATE [dbo].[tblMeeting_MiscellaneousInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo 
      
     
 WHERE MiscellaneousInfoId= @MiscellaneousInfoId";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
      }


      public bool UpdateApprovalMasterforNotIsMinApprovalPersonByIdForNew(MiscellaneousInfoDAO aMaster)
      {
          var aParameters = new List<SqlParameter>();
          aParameters.Add(new SqlParameter("@MiscellaneousInfoId", aMaster.MiscellaneousInfoId));


          aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
          aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
          aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
          aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));
          aParameters.Add(new SqlParameter("@RefMinAppCount", aMaster.RefMinAppCount));
          aParameters.Add(new SqlParameter("@RefMinAppCountCheck", aMaster.RefMinAppCountCheck));

          


          const string query = @" UPDATE [dbo].[tblMeeting_MiscellaneousInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo , RefMinAppCount=@RefMinAppCount, RefMinAppCountCheck=@RefMinAppCountCheck
      
     
 WHERE MiscellaneousInfoId= @MiscellaneousInfoId";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
      }
      public bool DeleteById(string  ID)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@MiscellaneousInfoId", ID));


          aSqlParameterlist.Add(new SqlParameter("@DeleteBy", HttpContext.Current.Session["UserId"]));

          aSqlParameterlist.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));


          const string query = @"
INSERT INTO tblMeeting_MiscellaneousInfo_DEL ([MiscellaneousInfoId]
      ,[CompanyId]
      ,[Title]
      ,[Purpose]
      ,[CreateBy]
      ,[CreateDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[KeySearch]
      ,[Isapproved]
      ,[ActionStatus]
      ,[RefEmpId]
      ,[RefSeqNo]
      ,[RefMinAppCount]
      ,[RefMinAppCountCheck]
      ,[DeleteBy]
      ,[DeleteDate] )
SELECT [MiscellaneousInfoId]
      ,[CompanyId]
      ,[Title]
      ,[Purpose]
      ,[CreateBy]
      ,[CreateDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[KeySearch]
      ,[Isapproved]
      ,[ActionStatus]
      ,[RefEmpId]
      ,[RefSeqNo]
      ,[RefMinAppCount]
      ,[RefMinAppCountCheck]
      ,@DeleteBy 
      ,@DeleteDate 
FROM tblMeeting_MiscellaneousInfo
WHERE  MiscellaneousInfoId=@MiscellaneousInfoId

DELETE FROM [dbo].[tblMeeting_MiscellaneousInfo]
      WHERE  MiscellaneousInfoId=@MiscellaneousInfoId";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
      }


      public bool AuditTrailLogById(string ID, string StatusMode)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@MiscellaneousInfoId", ID));
          aSqlParameterlist.Add(new SqlParameter("@StatusMode", StatusMode));


          aSqlParameterlist.Add(new SqlParameter("@DeleteBy", HttpContext.Current.Session["UserId"]));

          aSqlParameterlist.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));


          const string query = @"INSERT INTO tblMeeting_MiscellaneousInfo_AuditLog ([MiscellaneousInfoId]
      ,[CompanyId]
      ,[Title]
      ,[Purpose]
      ,[CreateBy]
      ,[CreateDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[KeySearch]
      ,[Isapproved]
      ,[ActionStatus]
      ,[RefEmpId]
      ,[RefSeqNo]
      ,[RefMinAppCount]
      ,[RefMinAppCountCheck]
      
      ,[DeleteBy]
      ,[DeleteDate],StatusMode)
SELECT [MiscellaneousInfoId]
      ,[CompanyId]
      ,[Title]
      ,[Purpose]
      ,[CreateBy]
      ,[CreateDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[KeySearch]
      ,[Isapproved]
      ,[ActionStatus]
      ,[RefEmpId]
      ,[RefSeqNo]
      ,[RefMinAppCount]
      ,[RefMinAppCountCheck]
      ,@DeleteBy 
      ,@DeleteDate, @StatusMode 
FROM tblMeeting_MiscellaneousInfo
WHERE  MiscellaneousInfoId=@MiscellaneousInfoId 

DECLARE @maxId INT
SELECT @maxId=MAX(AuditLog_MiscellaneousInfoId)  FROM tblMeeting_MiscellaneousInfo_AuditLog


INSERT INTO tblMeeting_MiscellaneousInfoDocument_AuditLog (
      [AuditLog_MiscellaneousInfoId]
      ,[MiscellaneousInfoDocumentID]
      ,[MiscellaneousInfoId]
      ,[DocumentLink]
      ,[DocumentNote]
      ,[FileName])
SELECT @maxId
      
      ,[MiscellaneousInfoDocumentID]
      ,[MiscellaneousInfoId]
      ,[DocumentLink]
      ,[DocumentNote]
      ,[FileName]
FROM tblMeeting_MiscellaneousInfoDocument
WHERE  MiscellaneousInfoId=@MiscellaneousInfoId 



INSERT INTO tblMeeting_MiscellaneousInfoDetail_AuditLog (
      [AuditLog_MiscellaneousInfoId]
      ,[MiscellaneousInfoDetailId]
      ,[MiscellaneousInfoId]
      ,[Type]
      ,[EmpInfoId]
      ,[EmpMasterCode]
      ,[EmpName]
      ,[Designation])
SELECT @maxId
      
     ,[MiscellaneousInfoDetailId]
      ,[MiscellaneousInfoId]
      ,[Type]
      ,[EmpInfoId]
      ,[EmpMasterCode]
      ,[EmpName]
      ,[Designation]
FROM tblMeeting_MiscellaneousInfoDetail
WHERE  MiscellaneousInfoId=@MiscellaneousInfoId 

";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
      }
      public bool SaveDocumentDetails(List<MiscellaneousInfoDocumentDAO> aList, int masterid)
      {
          try
          {
              List<SqlParameter> aParametersd = new List<SqlParameter>();
              aParametersd.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
              string queryDel = @"DELETE FROM [dbo].[tblMeeting_MiscellaneousInfoDocument]
      WHERE  MiscellaneousInfoId=@MiscellaneousInfoId";

              bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


              bool result = false;
              foreach (var item in aList)
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();

                  aParameters.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
                  aParameters.Add(new SqlParameter("@DocumentLink", item.DocumentLink));
                  aParameters.Add(new SqlParameter("@DocumentNote", item.DocumentNote));
                  aParameters.Add(new SqlParameter("@FileName", item.FileName));




                  string query = @"INSERT INTO [dbo].[tblMeeting_MiscellaneousInfoDocument]
           ([MiscellaneousInfoId]
           ,[DocumentLink]
           ,[DocumentNote],FileName)
     VALUES
           (@MiscellaneousInfoId
           ,@DocumentLink 
           ,@DocumentNote,@FileName)";
                  result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                  if (result == false)
                  {
                      return false;
                  }


              }
              return result;


          }
          catch (Exception x)
          {

              throw;
          }
      }

       public bool SaveDetails(List<MiscellaneousInfoDetailDAO> aList, int masterid)
      {
          try
          {
              List<SqlParameter> aParametersd = new List<SqlParameter>();
              aParametersd.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
              string queryDel = @"DELETE FROM [dbo].[tblMeeting_MiscellaneousInfoDetail]
      WHERE  MiscellaneousInfoId=@MiscellaneousInfoId";

              bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


              bool result = false;
              foreach (var item in aList)
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();

                  aParameters.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
                  aParameters.Add(new SqlParameter("@Type", item.Type ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@EmpMasterCode", item.EmpMasterCode ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@EmpName", item.EmpName ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Designation", item.Designation ?? (object)DBNull.Value));




                  string query = @"
INSERT INTO [dbo].[tblMeeting_MiscellaneousInfoDetail]
           ([MiscellaneousInfoId]
           ,[Type]
           ,[EmpInfoId]
           ,[EmpMasterCode]
           ,[EmpName]
           ,[Designation])
     VALUES
           (@MiscellaneousInfoId 
           ,@TYPE 
           ,@EmpInfoId 
           ,@EmpMasterCode 
           ,@EmpName 
           ,@Designation)";
                  result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                  if (result == false)
                  {
                      return false;
                  }


              }
              return result;


          }
          catch (Exception x)
          {

              throw;
          }
      }
    }
}
