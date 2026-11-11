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
   public class MeetingStatusSearchCriteria
   {
       public int? CompanyId { get; set; }
       public string MeetingIdOrNo { get; set; }
       public string MeetingTitle { get; set; }
       public int? CategoryId { get; set; }
       public string Classification { get; set; }
       public DateTime? MeetingDateFrom { get; set; }
       public DateTime? MeetingDateTo { get; set; }
       public int? CreatedBy { get; set; }
       public DateTime? CreatedDateFrom { get; set; }
       public DateTime? CreatedDateTo { get; set; }
       public string MeetingStatus { get; set; }
       public string ApprovalStatus { get; set; }
       public string ImplementationStatus { get; set; }
       public bool? HasDocument { get; set; }
       public string MemberEmployeeId { get; set; }
       public string KeySearch { get; set; }
   }

   public class MeetingEntryDAL
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


       public DataTable GetMasterByIdAudit(string ID)
       {
           try
           {
               string query = @"SELECT  us.UserName DeleteBy, com.ShortName,   *   FROM dbo.tblMeeting_MeetingInfo_AuditTrail mas
 
 
 left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
 LEFT JOIN dbo.tblMeeting_Category mmCat ON mmCat.CategoryID = mas.CategoryID
 LEFT JOIN dbo.tblMeeting_SubcommitteeMaster subCat ON subCat.SubcommitteeMasterId = mas.SubCommitteeId
 LEFT JOIN dbo.tblSalaryLocation offi ON offi.SalaryLoationId = mas.OfficeId
 LEFT JOIN dbo.tblJobLocation loc ON loc.JobLocationID = mas.LocationId
 LEFT JOIN dbo.tblFloor fl  ON fl.FloorId = mas.FloorId
 LEFT JOIN dbo.tblMeetingRoom mr   ON mr.MeetingRoomId = mas.MettingRoomId






  WHERE mas.StatusMode='Initial'   AND mas.MeetingInfoID=" + ID + @"

 
union all
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MeetingInfo_AuditTrail mas
 
 
  left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
 LEFT JOIN dbo.tblMeeting_Category mmCat ON mmCat.CategoryID = mas.CategoryID
 LEFT JOIN dbo.tblMeeting_SubcommitteeMaster subCat ON subCat.SubcommitteeMasterId = mas.SubCommitteeId
 LEFT JOIN dbo.tblSalaryLocation offi ON offi.SalaryLoationId = mas.OfficeId
 LEFT JOIN dbo.tblJobLocation loc ON loc.JobLocationID = mas.LocationId
 LEFT JOIN dbo.tblFloor fl  ON fl.FloorId = mas.FloorId
 LEFT JOIN dbo.tblMeetingRoom mr   ON mr.MeetingRoomId = mas.MettingRoomId
  WHERE mas.StatusMode='Edit'   AND mas.MeetingInfoID=" + ID + @"
union all 
SELECT   us.UserName DeleteBy, com.ShortName,   *  FROM dbo.tblMeeting_MeetingInfo_AuditTrail  mas
  left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
 LEFT JOIN dbo.tblMeeting_Category mmCat ON mmCat.CategoryID = mas.CategoryID
 LEFT JOIN dbo.tblMeeting_SubcommitteeMaster subCat ON subCat.SubcommitteeMasterId = mas.SubCommitteeId
 LEFT JOIN dbo.tblSalaryLocation offi ON offi.SalaryLoationId = mas.OfficeId
 LEFT JOIN dbo.tblJobLocation loc ON loc.JobLocationID = mas.LocationId
 LEFT JOIN dbo.tblFloor fl  ON fl.FloorId = mas.FloorId
 LEFT JOIN dbo.tblMeetingRoom mr   ON mr.MeetingRoomId = mas.MettingRoomId
  WHERE mas.StatusMode='Delete'    AND mas.MeetingInfoID=" + ID;

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
               string query = @"SELECT    mas.AuditTrail_MeetingInfoID,mas.DocumentNote,mas.FileName    FROM dbo.tblMeeting_MintuesEntryInfoDocument_AuditTrail mas
 
 
 
  WHERE   mas.MeetingInfoId=" + ID;

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
               string query = @"SELECT    mas.AuditTrail_MeetingInfoID,mas.Type,mas.EmpMasterCode,mas.EmpName, mas.Designation, mas.Position    FROM dbo.tblMeeting_MeetingInfoDetail_AuditTrail mas
 
 
 
  WHERE   mas.MeetingInfoID=" + ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetAgendaListByIdAudit(string ID)
       {
           try
           {
               string query = @"SELECT    mas.AuditTrail_MeetingInfoID,mas.Agenda,mas.Remarks    FROM dbo.tblMeeting_MeetingInfoAgenda_AuditTrail mas
 
 
 
  WHERE   mas.MeetingInfoID=" + ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public bool AuditTrailLogById(string ID, string StatusMode)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@MeetingInfoID", ID));
           aSqlParameterlist.Add(new SqlParameter("@StatusMode", StatusMode));


           aSqlParameterlist.Add(new SqlParameter("@DeleteBy", HttpContext.Current.Session["UserId"]));

           aSqlParameterlist.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));


           const string query = @"INSERT INTO tblMeeting_MeetingInfo_AuditTrail (
 [MeetingInfoID]
      ,[MeetingCode]
      ,[CompanyId]
      ,[CategoryID]
      ,[Title]
      ,[MeetingPurpose]
      ,[Classification]
      ,[MeetingDate]
      ,[StartTime]
      ,[EndTime]
      ,[IsOfficePremisis]
      ,[IsOuterPremisis]
      ,[IsVirtualMeeting]
      ,[OfficeId]
      ,[LocationId]
      ,[FloorId]
      ,[MettingRoomId]
      ,[Location]
      ,[LocationDescription]
      ,[Remarks]
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
      ,[SubCommitteeId]
      ,[StatusMode]
      ,[DeleteBy]
      ,[DeleteDate]
)
SELECT [MeetingInfoID]
      ,[MeetingCode]
      ,[CompanyId]
      ,[CategoryID]
      ,[Title]
      ,[MeetingPurpose]
      ,[Classification]
      ,[MeetingDate]
      ,[StartTime]
      ,[EndTime]
      ,[IsOfficePremisis]
      ,[IsOuterPremisis]
      ,[IsVirtualMeeting]
      ,[OfficeId]
      ,[LocationId]
      ,[FloorId]
      ,[MettingRoomId]
      ,[Location]
      ,[LocationDescription]
      ,[Remarks]
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
      ,[SubCommitteeId]

	  ,@StatusMode 
      ,@DeleteBy 
      ,@DeleteDate
FROM tblMeeting_MeetingInfo 
WHERE  MeetingInfoID=@MeetingInfoID 

DECLARE @maxId INT
SELECT @maxId=MAX(AuditTrail_MeetingInfoID)  FROM tblMeeting_MeetingInfo_AuditTrail


INSERT INTO tblMeeting_MeetingInfoDetail_AuditTrail (
     [Meeting_MeetingInfoDetailId]
      ,[AuditTrail_MeetingInfoID]
      ,[MeetingInfoID]
      ,[Type]
      ,[EmpInfoId]
      ,[EmpMasterCode]
      ,[EmpName]
      ,[Designation]
      ,[NotificationEmail]
      ,[NotificationSMS]
      ,[Position]
      ,[Division]
      ,[Deparment]
      ,[EmployeeType]
      ,[Name]
      ,[Address]
      ,[MobileNo]
      ,[Email]
      ,[MembershipDate]
      ,[Note]
      ,[MemberType]
      ,[MeetingMemberTypeId]
      ,[JoiningDate]
      ,[CompanyId]
      ,[IsBoardMember]
      ,[BMemberSetupDetailsID])
SELECT  [Meeting_MeetingInfoDetailId]
,@maxId
       
      ,[MeetingInfoID]
      ,[Type]
      ,[EmpInfoId]
      ,[EmpMasterCode]
      ,[EmpName]
      ,[Designation]
      ,[NotificationEmail]
      ,[NotificationSMS]
      ,[Position]
      ,[Division]
      ,[Deparment]
      ,[EmployeeType]
      ,[Name]
      ,[Address]
      ,[MobileNo]
      ,[Email]
      ,[MembershipDate]
      ,[Note]
      ,[MemberType]
      ,[MeetingMemberTypeId]
      ,[JoiningDate]
      ,[CompanyId]
      ,[IsBoardMember]
      ,[BMemberSetupDetailsID]
FROM tblMeeting_MeetingInfoDetail
WHERE  MeetingInfoID=@MeetingInfoID 



INSERT INTO tblMeeting_MintuesEntryInfoDocument_AuditTrail (
     [MintuesEntryInfoDocumentID]
      ,[AuditTrail_MeetingInfoID]
      ,[MeetingInfoId]
      ,[DocumentLink]
      ,[DocumentNote]
      ,[FileName])
SELECT [MintuesEntryInfoDocumentID]
    ,@maxId
      ,[MeetingInfoId]
      ,[DocumentLink]
      ,[DocumentNote]
      ,[FileName]
FROM tblMeeting_MintuesEntryInfoDocument
WHERE  MeetingInfoID=@MeetingInfoID  





INSERT INTO tblMeeting_MeetingInfoAgenda_AuditTrail (
    [MeetingInfoAgendaID]
      ,[AuditTrail_MeetingInfoID]
      ,[MeetingInfoID]
      ,[Agenda]
      ,[PresentorId]
      ,[Remarks])
SELECT [MeetingInfoAgendaID]
      ,@maxId
      ,[MeetingInfoID]
      ,[Agenda]
      ,[PresentorId]
      ,[Remarks]
FROM tblMeeting_MeetingInfoAgenda
WHERE  MeetingInfoID=@MeetingInfoID  

";


           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }
       public DataTable GetEmpAllApprovalInfo(string MasId)
       {
           try
           {
               string query = @"SELECT CanEdit, EmpInfoId FROM tblMeeting_MeetingEntryAPPROVALPATHSETUP
WHERE MeetingInfoId=" + MasId;

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
               string query = @"SELECT CanEdit, EmpInfoId FROM tblMeeting_MeetingEntryAPPROVALPATHSETUP
WHERE MeetingInfoId=" + MasId + "  and EmpInfoId =" + HttpContext.Current.Session["EmpInfoId"].ToString() + "";

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
               string query = @"SELECT mas.EmpInfoId, mas.Seq_No, * FROM tblMeeting_MeetingEntryAPPROVALPATHSETUP mas WITH (NOLOCK)
WHERE mas.MeetingInfoID=" + MasId + "  and mas.Seq_No < " + SeqNo + " ORDER BY mas.Seq_No desc ";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetEmpRoutingPath(string MasId, string SeqNo)
       {
           try
           {
               string query = @"SELECT emp.EmpMasterCode+ISNULL( ' , '+ emp.EmpName,'') AwEmpName, mas.EmpInfoId, mas.Seq_No, * FROM tblMeeting_MeetingEntryAPPROVALPATHSETUP mas WITH (NOLOCK)
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.EmpInfoId
WHERE mas.MeetingInfoID=" + MasId + "  and mas.Seq_No > " + SeqNo + "  ORDER BY mas.Seq_No ASC ";

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
               string query = @"SELECT  mas.IsMinimumApprovalPerson FROM tblMeeting_MeetingEntryAPPROVALPATHSETUP mas WITH (NOLOCK)
 
WHERE mas.MeetingInfoID=" + MasId + "   AND mas.EmpInfoId= " + HttpContext.Current.Session["EmpInfoId"].ToString() + " ";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public bool FinalUpdateApprovalMasterById(MeetingEntryDAO aMaster)
       {
           var aParameters = new List<SqlParameter>();
           aParameters.Add(new SqlParameter("@MeetingInfoID", aMaster.MeetingInfoID));


           aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
           aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
           aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));





           const string query = @" UPDATE [dbo].[tblMeeting_MeetingInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus , RefEmpId=@RefEmpId
      
      
      
 WHERE MeetingInfoID= @MeetingInfoID      

 
";


           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
       }


       public bool UpdateApprovalMasterReturrnById(MeetingEntryDAO aMaster)
       {
           var aParameters = new List<SqlParameter>();
           aParameters.Add(new SqlParameter("@MeetingInfoID", aMaster.MeetingInfoID));


           aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
           aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
           aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
           aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));




           const string query = @" UPDATE [dbo].[tblMeeting_MeetingInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo 
      
      ,[RefMinAppCountCheck] = ((SELECT RefMinAppCountCheck FROM tblMeeting_MeetingInfo WHERE MeetingInfoID=@MeetingInfoID)-1 )
 WHERE MeetingInfoID= @MeetingInfoID";


           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
       }

       public bool UpdateApprovalMasterforNotIsMinApprovalPersonById(MeetingEntryDAO aMaster)
       {
           var aParameters = new List<SqlParameter>();
           aParameters.Add(new SqlParameter("@MeetingInfoID", aMaster.MeetingInfoID));


           aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
           aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
           aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
           aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));




           const string query = @" UPDATE [dbo].[tblMeeting_MeetingInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo 
      
     
 WHERE MeetingInfoID= @MeetingInfoID";


           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
       }
       public DataTable GetEmpEntryBy(string MasId)
       {
           try
           {
               string query = @"SELECT us.EmpInfoId FROM tblMeeting_MeetingInfo  mas
LEFT JOIN dbo.tblUser us ON us.UserId=mas.CreateBy
WHERE mas.MeetingInfoID=" + MasId;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public bool UpdateApprovalMasterById(MeetingEntryDAO aMaster)
       {
           var aParameters = new List<SqlParameter>();
           aParameters.Add(new SqlParameter("@MeetingInfoID", aMaster.MeetingInfoID));


           aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
           aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
           aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
           aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));




           const string query = @" UPDATE [dbo].[tblMeeting_MeetingInfo]
   SET [Isapproved] =  @Isapproved 
      ,[ActionStatus] = @ActionStatus 
      ,[RefEmpId] = @RefEmpId 
      ,[RefSeqNo] = @RefSeqNo 
      
      ,[RefMinAppCountCheck] = ((SELECT RefMinAppCountCheck FROM tblMeeting_MeetingInfo WHERE MeetingInfoID=@MeetingInfoID)+1 )
 WHERE MeetingInfoID= @MeetingInfoID";


           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, "HRDB");
       }
       public DataTable GetEmpRoutingPath2(string MasId, string SeqNo)
       {
           try
           {
               string query = @"SELECT emp.EmpMasterCode+ISNULL( ' , '+ emp.EmpName,'') AwEmpName, mas.EmpInfoId, mas.Seq_No, * FROM tblMeeting_MeetingEntryAPPROVALPATHSETUP mas WITH (NOLOCK)
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.EmpInfoId
WHERE mas.MeetingInfoID=" + MasId + "  and mas.Seq_No >= " + SeqNo + "  ORDER BY mas.Seq_No ASC ";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable LoadInfoApproveList()
       {
           string query = @"	 SELECT cat.MeetingCategory, com.ShortName, us.UserName CreateBy, usUp.UserName UpdateBy, * FROM tblMeeting_MeetingInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblMeeting_Category cat   ON  cat.CategoryID = mas.CategoryID 
 
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId

 left JOIN (SELECT MeetingInfoID,MAX(Version)MaxVer FROM dbo.tblMeeting_MeetingInfoAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MeetingInfoID) AS CELog ON CELog.MeetingInfoID= mas.MeetingInfoID
								left JOIN dbo.tblMeeting_MeetingInfoAppLog ON tblMeeting_MeetingInfoAppLog.MeetingInfoID = mas.MeetingInfoID
                                where  ( Version=CELog.MaxVer OR Version IS NULL)   and  ForEmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }

       public DataTable GetEmpInfobyID(string comId)
       {
           string queryStr = @"SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName,  e.EmpInfoId
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL AND e.IsActive=1 AND e.CompanyId=" + comId ;
           //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
           return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
       }

       public void GetMeetingKeySearchDropdown(DropDownList ddl, string ComId)
       {
           string queryStr = @"SELECT KeySearch FROM dbo.tblMeeting_MeetingInfo
WITH (NOLOCK)  WHERE CompanyId=" + ComId + " ORDER BY KeySearch asc";
           aCommonInternalDal.LoadDropDownValue(ddl, "KeySearch", "KeySearch", queryStr, "HRDB");
       }
       public void GetMeetingKeySearchDropdown(DropDownList ddl, string ComId, string UserId)
       {
           string queryStr = @"SELECT KeySearch FROM dbo.tblMeeting_MeetingInfo
WITH (NOLOCK)  WHERE CompanyId=" + ComId + " AND CreateBy=" + UserId + " ORDER BY KeySearch asc";
           aCommonInternalDal.LoadDropDownValue(ddl, "KeySearch", "KeySearch", queryStr, "HRDB");
       }

       public DataTable GetEmpMemberInfoByCategory(string comId)
       {
           string query = @"SELECT  1 IsBoardMember, 'Employee' AS Type,  mas.MemberSetupDetailsID BMemberSetupDetailsID,'' EmpMasterCode, '' EmpInfoId, mas.Name EmpName, '' Designation ,0 NotificationEmail,0 NotificationSMS, 
0 AS Position,   * FROM tblMeeting_MemberSetupDetails mas 
 
WHERE mas.CompanyId=" + comId + "   ORDER  BY mas.OrderNo ASC  ";

           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetDDLComDivisionEntry(string cid, string Category)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@cid", cid));
           aSqlParameterlist.Add(new SqlParameter("@Category", Category));
           string query = @"SELECT d.SubcommitteeMasterId AS Value, d.SubCommitteeName AS TextField FROM dbo.tblMeeting_SubcommitteeMaster d WHERE d.CompanyId=@cid AND CategoryID=@Category and d.ActionStatus='Active'";
           return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
       }
       public DataTable GetDDLComDivision(string cid, string Category)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@cid", cid));
           aSqlParameterlist.Add(new SqlParameter("@Category", Category));
           string query = @"SELECT d.SubcommitteeMasterId AS Value, d.SubCommitteeName AS TextField FROM dbo.tblMeeting_SubcommitteeMaster d WHERE d.CompanyId=@cid AND CategoryID=@Category";
           return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
       }
       public DataTable GetEmpMemberInfoBySubCOmmitte(string comId, string CatId)
       {
           string query = @"SELECT 0 Position,  1 IsBoardMember,'Employee' AS Type,'' EmpMasterCode, '' EmpInfoId, mas.Name EmpName, '' Designation ,0 NotificationEmail,0 NotificationSMS, 
  PositionId,MeetingMemberTypeId,   * FROM tblMeeting_SubcommitteeMasterDetails mas WITH (NOLOCK)
INNER JOIN tblMeeting_SubcommitteeMaster dtl ON dtl.SubcommitteeMasterId=mas.SubcommitteeMasterId
WHERE dtl.CompanyId=" + comId + " AND dtl.SubcommitteeMasterId=" + CatId + "   ORDER  BY  mas.Name ";

           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public bool DeleteMaster(string Id)
       {

           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@MeetingInfoID", Id));
           aSqlParameterlist.Add(new SqlParameter("@DeleteBy", HttpContext.Current.Session["UserId"]));
           aSqlParameterlist.Add(new SqlParameter("@DeleteDate", DateTime.Now));
           string query = @"
 INSERT INTO [dbo].[tblMeeting_MeetingInfo_DEL]
           ([MeetingInfoID]
      ,[MeetingCode]
      ,[CompanyId]
      ,[CategoryID]
      ,[Title]
      ,[MeetingPurpose]
      ,[Classification]
      ,[MeetingDate]
      ,[StartTime]
      ,[EndTime]
      ,[IsOfficePremisis]
      ,[IsOuterPremisis]
      ,[IsVirtualMeeting]
      ,[OfficeId]
      ,[LocationId]
      ,[FloorId]
      ,[MettingRoomId]
      ,[Location]
      ,[LocationDescription]
      ,[Remarks]
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
      ,[SubCommitteeId]
      ,[DeleteBy]
      ,[DeleteDate])


		   SELECT [MeetingInfoID]
      ,[MeetingCode]
      ,[CompanyId]
      ,[CategoryID]
      ,[Title]
      ,[MeetingPurpose]
      ,[Classification]
      ,[MeetingDate]
      ,[StartTime]
      ,[EndTime]
      ,[IsOfficePremisis]
      ,[IsOuterPremisis]
      ,[IsVirtualMeeting]
      ,[OfficeId]
      ,[LocationId]
      ,[FloorId]
      ,[MettingRoomId]
      ,[Location]
      ,[LocationDescription]
      ,[Remarks]
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
      ,[SubCommitteeId]
           ,@DeleteBy 
           ,@DeleteDate 
FROM tblMeeting_MeetingInfo
WHERE MeetingInfoID =@MeetingInfoID

DELETE FROM [dbo].tblMeeting_MeetingInfo
WHERE MeetingInfoID =@MeetingInfoID



";

           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }

       public DataTable GetDocDataById(string ID)
       {
           try
           {
               string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MintuesEntryInfoDocument] WITH (NOLOCK) WHERE MeetingInfoId=" +
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
           string query = @"SELECT Alg.Meeting_MeetingInfoAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, case when  Alg.ActionStatus='Initiator' then 'Pending'   when  Alg.ActionStatus='Drafted' then 'Drafted'   when  Alg.ActionStatus='Verified' then 'Ongoing'    when Alg.ActionStatus='Approved' then 'Implemented'     else     Alg.ActionStatus end   ActionStatus, Alg.ApprovedDate, Alg.MeetingInfoID, Alg.Comments
  FROM dbo.tblMeeting_MeetingInfoAppLog Alg WITH (NOLOCK)
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApprovedBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and Alg.MeetingInfoID=" + MasterId;
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable GetAgendDataById(string ID)
       {
           try
           {
               string query = @"	SELECT  PresentorId AS Presentor, * 
  FROM [dbo].[tblMeeting_MeetingInfoAgenda] WITH (NOLOCK)  WHERE MeetingInfoId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetMeetingInfoDetailByIdEmp(string ID)
       {
           try
           {
               string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MeetingInfoDetail] WITH (NOLOCK) WHERE  IsBoardMember=0 and MeetingInfoId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetMeetingInfoDetailByIdBoardMember(string ID)
       {
           try
           {
               string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MeetingInfoDetail] WITH (NOLOCK) WHERE  IsBoardMember=1 and MeetingInfoId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetMeetingInfoDetailById(string ID)
       {
           try
           {
               string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MeetingInfoDetail] WITH (NOLOCK) WHERE MeetingInfoId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetMeetingApprovalPathById(string ID)
       {
           try
           {
               string query = @" 	SELECT  IsEmailNotification NotificationEmail, IsSMSNotification NotificationSMS,   *
  FROM [dbo].[tblMeeting_MeetingEntryAPPROVALPATHSETUP] WITH (NOLOCK)
  LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = tblMeeting_MeetingEntryAPPROVALPATHSETUP.EmpInfoId
  LEFT JOIN dbo.tblDesignation dg ON dg.DesignationId = emp.DesignationId WHERE MeetingInfoId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetMasterDataById(string ID)
       {
           try
           {
               string query = @"	SELECT  *
  FROM [dbo].[tblMeeting_MeetingInfo] WITH (NOLOCK) WHERE MeetingInfoID=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }



       public DataTable GetMasterDataByIdApproval(string ID)
       {
           try
           {
               string query = @" SELECT  case when  IsNotice=1 then 'Yes' else 'No' end  Notice, sub.SubCommitteeName,  ltrim(right(convert(NVARCHAR(500), StartTime, 100), 7)) AS StartTime, ltrim(right(convert(NVARCHAR(500), EndTime, 100), 7)) AS EndTime, *
  FROM [dbo].[tblMeeting_MeetingInfo] WITH (NOLOCK) 
  LEFT JOIN tblMeeting_SubcommitteeMaster sub ON tblMeeting_MeetingInfo.SubCommitteeId=sub.SubcommitteeMasterId
  WHERE MeetingInfoID=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetMasterDataByIdAudit(string ID)
       {
           try
           {
               string query = @" SELECT sub.SubCommitteeName,  ltrim(right(convert(NVARCHAR(500), StartTime, 100), 7)) AS StartTime, ltrim(right(convert(NVARCHAR(500), EndTime, 100), 7)) AS EndTime, *
  FROM [dbo].[tblMeeting_MeetingInfo_DEL] WITH (NOLOCK) 
  LEFT JOIN tblMeeting_SubcommitteeMaster sub ON tblMeeting_MeetingInfo_DEL.SubCommitteeId=sub.SubcommitteeMasterId
  WHERE MeetingInfoID=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable getMeetingCalenderDAL(string parameter)
       {


           string queryStr = @"SELECT ISNULL('Company: '+com.ShortName,'')+''+ ISNULL(', Title:'+Title,'') + ''+ ISNULL(', Time: '+ LTRIM(right(convert(NVARCHAR(500), StartTime, 100), 7)),'') +''+ ISNULL('-'+LTRIM(right(convert(NVARCHAR(500), EndTime, 100), 7)),'') + ISNULL(', Meeting Category: '+cat.MeetingCategory,'') AS    Title,  FORMAT(MeetingDate,'yyyy-MM-dd') MeetingDate,* FROM dbo.tblMeeting_MeetingInfo WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = tblMeeting_MeetingInfo.CompanyId
LEFT JOIN dbo.tblMeeting_Category cat ON cat.CategoryID = tblMeeting_MeetingInfo.CategoryID WHERE MeetingInfoID IS NOT NULL " + parameter + "";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }
       public DataTable GetCapacityDataByMeetingId(string ID)
       {
           try
           {
               string query = @"	SELECT  MeetingRoomCapacity
  FROM  dbo.tblMeetingRoom WITH (NOLOCK) WHERE MeetingRoomId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable LoadInfo(string parameter)
       {
           string query = @"SELECT   mas.ActionStatus,  emp.EmpMasterCode+ISNULL( ' : '+ emp.EmpName,'') AwEmpName,  '' ActionStatusShow,  com.ShortName, cat.MeetingCategory,mas.MeetingPurpose,mas.MeetingDate, mas.Classification, us.UserName CreateBy, COALESCE(NULLIF(creatorEmp.EmpName, ''), us.UserName) CreatedByEmployeeName, creatorEmp.EmpMasterCode CreatedByEmployeeId, usUp.UserName UpdateBy,* FROM tblMeeting_MeetingInfo mas WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
LEFT JOIN dbo.tblMeeting_Category cat ON cat.CategoryID = mas.CategoryID
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
LEFT JOIN dbo.tblEmpGeneralInfo creatorEmp ON creatorEmp.EmpInfoId = us.EmpInfoId
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.RefEmpId
 
								 

left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId  WHERE   mas.MeetingInfoID IS NOT NULL 
" + parameter + "  ORDER BY mas.MeetingDate desc";

           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }

       public DataTable GetMeetingMemberSearchDropdown(int companyId, int? userId)
       {
           const string query = @"SELECT DISTINCT memberData.SearchValue AS Value,
       memberData.SearchValue +
       CASE WHEN memberData.MemberName='' THEN '' ELSE ' : ' + memberData.MemberName END AS TextField
FROM
(
    SELECT COALESCE(NULLIF(detail.EmpMasterCode,''),
                    CONVERT(NVARCHAR(50), detail.EmpInfoId),
                    CONVERT(NVARCHAR(50), detail.BMemberSetupDetailsID)) SearchValue,
           COALESCE(NULLIF(detail.EmpName,''), NULLIF(detail.Name,''), '') MemberName
    FROM dbo.tblMeeting_MeetingInfoDetail detail WITH (NOLOCK)
    INNER JOIN dbo.tblMeeting_MeetingInfo meeting WITH (NOLOCK)
        ON meeting.MeetingInfoID=detail.MeetingInfoID
    WHERE meeting.CompanyId=@CompanyId
      AND (@UserId IS NULL OR meeting.CreateBy=@UserId)
) memberData
WHERE memberData.SearchValue IS NOT NULL
ORDER BY TextField";

           var parameters = new List<SqlParameter>
           {
               new SqlParameter("@CompanyId", SqlDbType.Int) { Value = companyId },
               new SqlParameter("@UserId", SqlDbType.Int) { Value = (object)userId ?? DBNull.Value }
           };

           return aCommonInternalDal.GetDTforDDL(query, parameters, DataBase.HRDB);
       }

       public DataTable LoadInfo(MeetingStatusSearchCriteria criteria)
       {
           const string query = @"SELECT mas.ActionStatus,
       emp.EmpMasterCode + ISNULL(' : ' + emp.EmpName, '') AwEmpName,
       CASE
           WHEN mas.ActionStatus='Initiator' THEN 'Pending' + ISNULL(' [' + FORMAT(tblMeeting_MeetingInfoAppLog.ApprovedDate,'dd-MMM-yyyy') + ']', ' [' + FORMAT(mas.CreateDate,'dd-MMM-yyyy') + ']')
           WHEN mas.ActionStatus='Drafted' THEN 'Drafted [' + FORMAT(mas.CreateDate,'dd-MMM-yyyy') + ']'
           WHEN mas.ActionStatus='Verified' THEN 'Ongoing' + ISNULL(' [' + FORMAT(tblMeeting_MeetingInfoAppLog.ApprovedDate,'dd-MMM-yyyy') + ']', ' [' + FORMAT(mas.CreateDate,'dd-MMM-yyyy') + ']')
           WHEN mas.ActionStatus='Approved' THEN 'Implemented' + ISNULL(' [' + FORMAT(tblMeeting_MeetingInfoAppLog.ApprovedDate,'dd-MMM-yyyy') + ']', ' [' + FORMAT(mas.CreateDate,'dd-MMM-yyyy') + ']')
           ELSE mas.ActionStatus
       END ActionStatusShow,
       com.ShortName,
       cat.MeetingCategory,
       mas.MeetingPurpose,
       mas.MeetingDate,
       mas.Classification,
       us.UserName CreateBy,
       usUp.UserName UpdateBy,
       *
FROM dbo.tblMeeting_MeetingInfo mas WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
LEFT JOIN dbo.tblMeeting_Category cat ON cat.CategoryID = mas.CategoryID
LEFT JOIN dbo.tblUser us ON mas.CreateBy = us.UserId
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.RefEmpId
LEFT JOIN
(
    SELECT MeetingInfoID, MAX(Version) MaxVer
    FROM dbo.tblMeeting_MeetingInfoAppLog
    WHERE ActionStatus NOT IN ('Review')
    GROUP BY MeetingInfoID
) AS CELog ON CELog.MeetingInfoID = mas.MeetingInfoID
LEFT JOIN dbo.tblMeeting_MeetingInfoAppLog
    ON tblMeeting_MeetingInfoAppLog.MeetingInfoID = mas.MeetingInfoID
LEFT JOIN dbo.tblUser usUp ON mas.UpdateBy = usUp.UserId
WHERE (Version = CELog.MaxVer OR Version IS NULL)
  AND mas.MeetingInfoID IS NOT NULL
  AND (@CompanyId IS NULL OR mas.CompanyId = @CompanyId)
  AND (@MeetingIdOrNo IS NULL
       OR CONVERT(NVARCHAR(50), mas.MeetingInfoID) LIKE '%' + @MeetingIdOrNo + '%'
       OR CONVERT(NVARCHAR(50), mas.MeetingNo) LIKE '%' + @MeetingIdOrNo + '%'
       OR mas.MeetingCode LIKE '%' + @MeetingIdOrNo + '%')
  AND (@MeetingTitle IS NULL OR mas.Title LIKE '%' + @MeetingTitle + '%')
  AND (@CategoryId IS NULL OR mas.CategoryID = @CategoryId)
  AND (@Classification IS NULL OR mas.Classification = @Classification)
  AND (@MeetingDateFrom IS NULL OR CAST(mas.MeetingDate AS DATE) >= @MeetingDateFrom)
  AND (@MeetingDateTo IS NULL OR CAST(mas.MeetingDate AS DATE) <= @MeetingDateTo)
  AND (@CreatedBy IS NULL OR mas.CreateBy = @CreatedBy)
  AND (@CreatedDateFrom IS NULL OR CAST(mas.CreateDate AS DATE) >= @CreatedDateFrom)
  AND (@CreatedDateTo IS NULL OR CAST(mas.CreateDate AS DATE) <= @CreatedDateTo)
  AND (@MeetingStatus IS NULL
       OR (@MeetingStatus = 'Upcoming' AND CAST(mas.MeetingDate AS DATE) > CAST(GETDATE() AS DATE))
       OR (@MeetingStatus = 'Today' AND CAST(mas.MeetingDate AS DATE) = CAST(GETDATE() AS DATE))
       OR (@MeetingStatus = 'Completed' AND CAST(mas.MeetingDate AS DATE) < CAST(GETDATE() AS DATE)))
  AND (@ApprovalStatus IS NULL OR mas.ActionStatus = @ApprovalStatus)
  AND (@ImplementationStatus IS NULL OR EXISTS
      (
          SELECT 1
          FROM dbo.tblMeeting_MeetingInfoAgenda agenda WITH (NOLOCK)
          WHERE agenda.MeetingInfoID = mas.MeetingInfoID
            AND agenda.ImplementationStatus = @ImplementationStatus
      ))
  AND (@HasDocument IS NULL
       OR (@HasDocument = 1 AND EXISTS
          (
              SELECT 1
              FROM dbo.tblMeeting_MintuesEntryInfoDocument doc WITH (NOLOCK)
              WHERE doc.MeetingInfoID = mas.MeetingInfoID
          ))
       OR (@HasDocument = 0 AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.tblMeeting_MintuesEntryInfoDocument doc WITH (NOLOCK)
              WHERE doc.MeetingInfoID = mas.MeetingInfoID
          )))
  AND (@MemberEmployeeId IS NULL OR EXISTS
      (
          SELECT 1
          FROM dbo.tblMeeting_MeetingInfoDetail detail WITH (NOLOCK)
          WHERE detail.MeetingInfoID = mas.MeetingInfoID
            AND (detail.EmpMasterCode LIKE '%' + @MemberEmployeeId + '%'
                 OR CONVERT(NVARCHAR(50), detail.EmpInfoId) LIKE '%' + @MemberEmployeeId + '%'
                 OR CONVERT(NVARCHAR(50), detail.BMemberSetupDetailsID) LIKE '%' + @MemberEmployeeId + '%')
      ))
  AND (@KeySearch IS NULL OR mas.KeySearch LIKE '%' + @KeySearch + '%')
ORDER BY mas.MeetingDate DESC";

           var parameters = new List<SqlParameter>
           {
               new SqlParameter("@CompanyId", SqlDbType.Int) { Value = (object)criteria.CompanyId ?? DBNull.Value },
               new SqlParameter("@MeetingIdOrNo", SqlDbType.NVarChar, 100) { Value = (object)criteria.MeetingIdOrNo ?? DBNull.Value },
               new SqlParameter("@MeetingTitle", SqlDbType.NVarChar, 500) { Value = (object)criteria.MeetingTitle ?? DBNull.Value },
               new SqlParameter("@CategoryId", SqlDbType.Int) { Value = (object)criteria.CategoryId ?? DBNull.Value },
               new SqlParameter("@Classification", SqlDbType.NVarChar, 100) { Value = (object)criteria.Classification ?? DBNull.Value },
               new SqlParameter("@MeetingDateFrom", SqlDbType.Date) { Value = (object)criteria.MeetingDateFrom ?? DBNull.Value },
               new SqlParameter("@MeetingDateTo", SqlDbType.Date) { Value = (object)criteria.MeetingDateTo ?? DBNull.Value },
               new SqlParameter("@CreatedBy", SqlDbType.Int) { Value = (object)criteria.CreatedBy ?? DBNull.Value },
               new SqlParameter("@CreatedDateFrom", SqlDbType.Date) { Value = (object)criteria.CreatedDateFrom ?? DBNull.Value },
               new SqlParameter("@CreatedDateTo", SqlDbType.Date) { Value = (object)criteria.CreatedDateTo ?? DBNull.Value },
               new SqlParameter("@MeetingStatus", SqlDbType.NVarChar, 30) { Value = (object)criteria.MeetingStatus ?? DBNull.Value },
               new SqlParameter("@ApprovalStatus", SqlDbType.NVarChar, 50) { Value = (object)criteria.ApprovalStatus ?? DBNull.Value },
               new SqlParameter("@ImplementationStatus", SqlDbType.NVarChar, 50) { Value = (object)criteria.ImplementationStatus ?? DBNull.Value },
               new SqlParameter("@HasDocument", SqlDbType.Bit) { Value = (object)criteria.HasDocument ?? DBNull.Value },
               new SqlParameter("@MemberEmployeeId", SqlDbType.NVarChar, 100) { Value = (object)criteria.MemberEmployeeId ?? DBNull.Value },
               new SqlParameter("@KeySearch", SqlDbType.NVarChar, 1000) { Value = (object)criteria.KeySearch ?? DBNull.Value }
           };

           return aCommonInternalDal.DataContainerDataTable(query, parameters, DataBase.HRDB);
       }

       public DataTable LoadInfoReport(string parameter)
       {
           string query = @"SELECT   mas.MeetingNo AS MeetingNo, YEAR(mas.MeetingDate)  MeetingYear, format(mas.MeetingDate,'dd-MMM-yyyy')  MeetingDate, case when  mas.IsNotice=1 then 'Yes' else 'No' end  Notice,
MA.AgendaNo AS AgendaNo  ,MA.Agenda,MA.Observation,MA.Decision,MA.ImplementationStatus  
  FROM [dbo].[tblMeeting_MeetingInfo] mas WITH (NOLOCK) 
  LEFT JOIN dbo.tblMeeting_MeetingInfoAgenda MA ON MA.MeetingInfoID = mas.MeetingInfoID
  where  mas.MeetingInfoID is not null
" + parameter + "  ";

           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }


       public DataTable LoadInfoAuditTrail(string parameter, string parameter2)
       {
           string query = @"
SELECT   cat.MeetingCategory, mas.StatusMode,  CASE WHEN  mas.StatusMode='Edit' THEN  'btn btn-sm btn-warning'   ELSE  'btn btn-sm btn-danger' end  StatusStyle, 'Delete' Status,com.ShortName, us.UserName DeleteBy, *   FROM tblMeeting_MeetingInfo_AuditTrail  mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.DeleteBy =us.UserId  
 LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = mas.RefEmpId
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId  
left JOIN  dbo.tblMeeting_Category cat   ON  cat.CategoryID =mas.CategoryID  


 

WHERE mas.AuditTrail_MeetingInfoID = 
  (SELECT max(t2.AuditTrail_MeetingInfoID) FROM tblMeeting_MeetingInfo_AuditTrail t2 WHERE t2.MeetingInfoID = mas.MeetingInfoID) ";

           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }

       public int SaveMaster(MeetingEntryDAO aMaster, string user)
       {
           try
           {
               if (aMaster.MeetingInfoID > 0)
               {
                   /////asdasddasd
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@MeetingInfoID", aMaster.MeetingInfoID));
                   aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Title", aMaster.Title ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@KeySearch", aMaster.KeySearch ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@CategoryID", aMaster.CategoryID ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@SubCommitteeId", aMaster.SubCommitteeId ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@MeetingPurpose", aMaster.MeetingPurpose ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@Classification", aMaster.Classification ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@MeetingDate", aMaster.MeetingDate ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@StartTime", aMaster.StartTime ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@EndTime", aMaster.EndTime ?? (object)DBNull.Value));




                   aParameters.Add(new SqlParameter("@IsOfficePremisis", aMaster.IsOfficePremisis ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@IsOuterPremisis", aMaster.IsOuterPremisis ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@IsVirtualMeeting", aMaster.IsVirtualMeeting ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@OfficeId", aMaster.OfficeId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@LocationId", aMaster.LocationId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@FloorId", aMaster.FloorId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@MettingRoomId", aMaster.MettingRoomId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Location", aMaster.Location ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@LocationDescription", aMaster.LocationDescription ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Remarks", aMaster.Remarks ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
                   aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
                   aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));
                   aParameters.Add(new SqlParameter("@RefMinAppCount", aMaster.RefMinAppCount));
                   aParameters.Add(new SqlParameter("@RefMinAppCountCheck", aMaster.RefMinAppCountCheck));

                   aParameters.Add(new SqlParameter("@IsNotice", aMaster.IsNotice ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@MeetingNo", Convert.ToInt32(aMaster.MeetingNo)));

                   aParameters.Add(new SqlParameter("@UpdateBy", user));

                   aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                   aParameters.Add(new SqlParameter("@MailAddress", aMaster.MailAddress ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@MailBody", aMaster.MailBody ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@IsSendMail", aMaster.IsSendMail ?? (object)DBNull.Value));

                   string query = @"UPDATE [dbo].[tblMeeting_MeetingInfo]
   SET [CompanyId] = @CompanyId
      ,[CategoryID] = @CategoryID 
      ,[MeetingPurpose] = @MeetingPurpose 
      ,[Classification] = @Classification 
      ,[MeetingDate] = @MeetingDate 
      ,[StartTime] = @StartTime 
      ,[EndTime] = @EndTime  
      ,[UpdateBy] = @UpdateBy 
      ,[UpdateDate] = @UpdateDate 
      ,[IsOfficePremisis] = @IsOfficePremisis
      ,[IsOuterPremisis] = @IsOuterPremisis
      ,[IsVirtualMeeting] = @IsVirtualMeeting
      ,[OfficeId] = @OfficeId
      ,[LocationId] = @LocationId
      ,[FloorId] = @FloorId
      ,[MettingRoomId] = @MettingRoomId
      ,[Location] = @Location
      ,[LocationDescription] = @LocationDescription
      ,[IsNotice] = @IsNotice
      ,[MeetingNo] = @MeetingNo
      ,[Remarks] = @Remarks,Title=@Title, KeySearch=@KeySearch, SubCommitteeId=@SubCommitteeId,Isapproved=@Isapproved,ActionStatus=@ActionStatus,RefEmpId=@RefEmpId,RefSeqNo=@RefSeqNo,RefMinAppCount=@RefMinAppCount,RefMinAppCountCheck=@RefMinAppCountCheck,MailAddress=@MailAddress,MailBody=@MailBody,IsSendMail=@IsSendMail
 WHERE MeetingInfoID=@MeetingInfoID";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                   if (result == true)
                   {
                       return aMaster.MeetingInfoID;
                   }
                   else
                   {
                       return 0;
                   }

               }
               else
               {


                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Title", aMaster.Title ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@KeySearch", aMaster.KeySearch ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@CategoryID", aMaster.CategoryID ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@MeetingPurpose", aMaster.MeetingPurpose ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@Classification", aMaster.Classification ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@MeetingDate", aMaster.MeetingDate ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@StartTime", aMaster.StartTime ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@EndTime", aMaster.EndTime ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@CreateBy", user));

                   aParameters.Add(new SqlParameter("@CreateDate", System.DateTime.Now));

                   aParameters.Add(new SqlParameter("@IsOfficePremisis", aMaster.IsOfficePremisis ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@IsOuterPremisis", aMaster.IsOuterPremisis ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@IsVirtualMeeting", aMaster.IsVirtualMeeting ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@OfficeId", aMaster.OfficeId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@LocationId", aMaster.LocationId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@FloorId", aMaster.FloorId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@MettingRoomId", aMaster.MettingRoomId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Location", aMaster.Location ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@LocationDescription", aMaster.LocationDescription ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Remarks", aMaster.Remarks ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@Isapproved", aMaster.Isapproved));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
                   aParameters.Add(new SqlParameter("@RefEmpId", aMaster.RefEmpId));
                   aParameters.Add(new SqlParameter("@RefSeqNo", aMaster.RefSeqNo));
                   aParameters.Add(new SqlParameter("@RefMinAppCount", aMaster.RefMinAppCount));
                   aParameters.Add(new SqlParameter("@RefMinAppCountCheck", aMaster.RefMinAppCountCheck));
                   aParameters.Add(new SqlParameter("@SubCommitteeId", aMaster.SubCommitteeId ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@IsNotice", aMaster.IsNotice ?? (object)DBNull.Value));

                   aParameters.Add(new SqlParameter("@MeetingNo", Convert.ToInt32(aMaster.MeetingNo)));

                   aParameters.Add(new SqlParameter("@MailAddress", aMaster.MailAddress ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@MailBody", aMaster.MailBody ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@IsSendMail", aMaster.IsSendMail ?? (object)DBNull.Value));

                   string query = @"INSERT INTO [dbo].[tblMeeting_MeetingInfo]
           ([MeetingCode]
           ,[CompanyId]
           ,[CategoryID]
           ,[MeetingPurpose]
           ,[Classification]
           ,[MeetingDate]
           ,[StartTime]
           ,[EndTime]
		   ,[CreateBy]
           ,[CreateDate]
           ,[IsOfficePremisis]
           ,[IsOuterPremisis]
           ,[IsVirtualMeeting]
           ,[OfficeId]
           ,[LocationId]
           ,[FloorId]
           ,[MettingRoomId]
           ,[Location]
           ,[LocationDescription]
           ,[Remarks],Title,KeySearch,Isapproved,ActionStatus,RefEmpId,RefSeqNo,RefMinAppCount,RefMinAppCountCheck,SubCommitteeId,IsNotice,MeetingNo,MailAddress,MailBody,IsSendMail
		   )
     VALUES
           (NULL
           ,@CompanyId
           ,@CategoryID
           ,@MeetingPurpose
           ,@Classification
           ,@MeetingDate
           ,@StartTime
           ,@EndTime,@CreateBy
           ,@CreateDate
           ,@IsOfficePremisis
           ,@IsOuterPremisis
           ,@IsVirtualMeeting
           ,@OfficeId
           ,@LocationId
           ,@FloorId
           ,@MettingRoomId
           ,@Location
           ,@LocationDescription
           ,@Remarks,@Title,@KeySearch,@Isapproved,@ActionStatus,@RefEmpId,@RefSeqNo,@RefMinAppCount,@RefMinAppCountCheck,@SubCommitteeId,@IsNotice,@MeetingNo,@MailAddress,@MailBody,@IsSendMail )";

                   int pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                   return pk;
               }
           }
           catch (Exception ex)
           {

               throw;
           }
       }



       public bool SaveRoutingPathDetails(List<MiscellaneousInfoRoutingPathDAO> aList, int masterid)
       {
           try
           {
               List<SqlParameter> aParametersd = new List<SqlParameter>();
               aParametersd.Add(new SqlParameter("@MeetingInfoId", masterid));
               string queryDel = @"DELETE FROM [dbo].[tblMeeting_MeetingEntryAPPROVALPATHSETUP]
       WHERE  MeetingInfoId=@MeetingInfoId";

               bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


               bool result = false;
               foreach (var item in aList)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@MeetingInfoId", masterid));
                   aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                   aParameters.Add(new SqlParameter("@Seq_No", item.Seq_No));
                   aParameters.Add(new SqlParameter("@CanEdit", item.CanEdit));
                   aParameters.Add(new SqlParameter("@IsEmailNotification", item.IsEmailNotification));
                   aParameters.Add(new SqlParameter("@IsSMSNotification", item.IsSMSNotification));


                   aParameters.Add(new SqlParameter("@IsMinimumApprovalPerson", item.IsMinimumApprovalPerson));




                   string query = @"
INSERT INTO [dbo].[tblMeeting_MeetingEntryAPPROVALPATHSETUP]
           ([MeetingInfoId]
           ,[EmpInfoId]
           ,[Seq_No]
           ,[CanEdit]
           ,[IsEmailNotification]
           ,[IsSMSNotification]
           ,[IsMinimumApprovalPerson])
     VALUES
           (@MeetingInfoId 
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


       public bool SaveDetails(List<MeetingInfoDetailDAO> aList, int masterid, int isboard)
       {
           try
           {
               List<SqlParameter> aParametersd = new List<SqlParameter>();
               aParametersd.Add(new SqlParameter("@MeetingInfoID", masterid));
               string queryDel = @"DELETE FROM [dbo].[tblMeeting_MeetingInfoDetail]
       WHERE  MeetingInfoID=@MeetingInfoID and IsBoardMember= " + isboard;

               bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


               bool result = false;
               foreach (var item in aList)
               {
                   if (item.IsBoardMember=="0")
                   {
                       List<SqlParameter> aParameters = new List<SqlParameter>();

                       aParameters.Add(new SqlParameter("@MeetingInfoId", masterid));
                       aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@Type", item.Type ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@EmpMasterCode", item.EmpMasterCode ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@EmpName", item.EmpName));
                       aParameters.Add(new SqlParameter("@Designation", item.Designation));



                       aParameters.Add(new SqlParameter("@NotificationEmail", item.NotificationEmail));
                       aParameters.Add(new SqlParameter("@NotificationSMS", item.NotificationSMS));
                       aParameters.Add(new SqlParameter("@Position", item.Position));
                       aParameters.Add(new SqlParameter("@IsBoardMember", item.IsBoardMember));
                       aParameters.Add(new SqlParameter("@BMemberSetupDetailsID", item.BMemberSetupDetailsID));




                       string query = @"

DECLARE @division NVARCHAR(50)
DECLARE @Department NVARCHAR(50)
DECLARE @EmpType NVARCHAR(50)
SELECT @division=div.DivisionName, @Department=dpt.DepartmentName, @EmpType=tp.EmpType FROM dbo.tblEmpGeneralInfo WITH (nolock)
LEFT JOIN dbo.tblDivision div ON div.DivisionId = tblEmpGeneralInfo.DivisionId
LEFT JOIN dbo.tblDepartment dpt ON dpt.DepartmentId = tblEmpGeneralInfo.DepartmentId
LEFT JOIN dbo.tblEmployeeType tp ON tp.EmpTypeId = tblEmpGeneralInfo.EmpTypeId
 WHERE EmpInfoId=@EmpInfoId


INSERT INTO [dbo].[tblMeeting_MeetingInfoDetail]
           ([MeetingInfoID]
           ,[Type]
           ,[EmpInfoId]
           ,[EmpMasterCode]
           ,[EmpName]
           ,[Designation]
           ,[NotificationEmail]
           ,[NotificationSMS]
           ,[Position], Division, Deparment, EmployeeType,IsBoardMember,BMemberSetupDetailsID)
     VALUES
           (@MeetingInfoID 
           ,@TYPE 
           ,@EmpInfoId 
           ,@EmpMasterCode 
           ,@EmpName 
           ,@Designation 
           ,@NotificationEmail 
           ,@NotificationSMS 
           ,@POSITION, @division, @Department,@EmpType,@IsBoardMember,@BMemberSetupDetailsID )   





";
                       result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   }

                   if (item.IsBoardMember == "1")
                   {
                       List<SqlParameter> aParameters = new List<SqlParameter>();

                       aParameters.Add(new SqlParameter("@MeetingInfoId", masterid));
                       aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@Type", item.Type ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@EmpMasterCode", item.EmpMasterCode ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@EmpName", item.EmpName));
                       aParameters.Add(new SqlParameter("@Designation", item.Designation));





                       aParameters.Add(new SqlParameter("@NotificationEmail", item.NotificationEmail));
                       aParameters.Add(new SqlParameter("@NotificationSMS", item.NotificationSMS));
                       aParameters.Add(new SqlParameter("@Position", item.Position));

                       aParameters.Add(new SqlParameter("@IsBoardMember", item.IsBoardMember));
                       aParameters.Add(new SqlParameter("@BMemberSetupDetailsID", item.BMemberSetupDetailsID));


                       string query = @"

DECLARE @Name NVARCHAR(50)
DECLARE @Address NVARCHAR(50)
DECLARE @MobileNo NVARCHAR(50)
DECLARE @Email NVARCHAR(50)
DECLARE @MembershipDate NVARCHAR(50)
DECLARE @Note NVARCHAR(50)
DECLARE @MemberType NVARCHAR(50)
DECLARE @MeetingMemberTypeId  INT =NULL
DECLARE @JoiningDate DATETIME=NULL
DECLARE @CompanyId INT =NULL
SELECT   @Name= [Name]
      ,@Address=[Address]
      ,@MobileNo=[MobileNo]
      ,@Email=[Email]
      ,@MembershipDate=[MembershipDate]
      ,@Note=[Note]
      ,@MemberType=[MemberType]
      ,@MeetingMemberTypeId=[MeetingMemberTypeId]
      ,@JoiningDate=[JoiningDate]
      ,@CompanyId=[CompanyId] 
  FROM [dbo].[tblMeeting_BoardMemberSetupDetails] WHERE BMemberSetupDetailsID=@BMemberSetupDetailsID

INSERT INTO [dbo].[tblMeeting_MeetingInfoDetail]
           ([MeetingInfoID]
           ,[Type]
           ,[EmpInfoId]
           ,[EmpMasterCode]
           ,[EmpName]
           ,[Designation]
           ,[NotificationEmail]
           ,[NotificationSMS]
           ,[Position],[Name]
           ,[Address]
           ,[MobileNo]
           ,[Email]
           ,[MembershipDate]
           ,[Note]
           ,[MemberType]
           ,[MeetingMemberTypeId]
           ,[JoiningDate]
           ,[CompanyId],IsBoardMember,BMemberSetupDetailsID)
     VALUES
           (@MeetingInfoID 
           ,@TYPE 
           ,@EmpInfoId 
           ,@EmpMasterCode 
           ,@EmpName 
           ,@Designation 
           ,@NotificationEmail 
           ,@NotificationSMS 
           ,@POSITION,@Name 
      ,@Address 
      ,@MobileNo 
      ,@Email 
      ,@MembershipDate 
      ,@Note 
      ,@MemberType 
      ,@MeetingMemberTypeId 
      ,@JoiningDate 
      ,@CompanyId ,@IsBoardMember,@BMemberSetupDetailsID)





";
                       result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   }


                   if (item.IsBoardMember == "2")
                   {
                       List<SqlParameter> aParameters = new List<SqlParameter>();

                       aParameters.Add(new SqlParameter("@MeetingInfoId", masterid));
                       aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@Type", item.Type ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@EmpMasterCode", item.EmpMasterCode ?? (object)DBNull.Value));
                       aParameters.Add(new SqlParameter("@EmpName", item.EmpName));
                       aParameters.Add(new SqlParameter("@Designation", item.Designation));



                       aParameters.Add(new SqlParameter("@NotificationEmail", item.NotificationEmail));
                       aParameters.Add(new SqlParameter("@NotificationSMS", item.NotificationSMS));
                       aParameters.Add(new SqlParameter("@Position", item.Position));


                       aParameters.Add(new SqlParameter("@IsBoardMember", item.IsBoardMember));
                       aParameters.Add(new SqlParameter("@BMemberSetupDetailsID", item.BMemberSetupDetailsID));

                       string query = @"INSERT INTO [dbo].[tblMeeting_MeetingInfoDetail]
           ([MeetingInfoID]
           ,[Type]
           ,[EmpInfoId]
           ,[EmpMasterCode]
           ,[EmpName]
           ,[Designation]
           ,[NotificationEmail]
           ,[NotificationSMS]
           ,[Position],IsBoardMember,BMemberSetupDetailsID)
     VALUES
           (@MeetingInfoID 
           ,@TYPE 
           ,@EmpInfoId 
           ,@EmpMasterCode 
           ,@EmpName 
           ,@Designation 
           ,@NotificationEmail 
           ,@NotificationSMS 
           ,@POSITION ,@IsBoardMember,@BMemberSetupDetailsID)





";
                       result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   }
                  

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

       public bool DeleteById(string ID)
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
      ,[DeleteBy]
      ,[DeleteDate])
SELECT [MiscellaneousInfoId]
      ,[CompanyId]
      ,[Title]
      ,[Purpose]
      ,[CreateBy]
      ,[CreateDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,@DeleteBy 
      ,@DeleteDate 
FROM tblMeeting_MiscellaneousInfo
WHERE  MiscellaneousInfoId=@MiscellaneousInfoId

DELETE FROM [dbo].[tblMeeting_MiscellaneousInfo]
      WHERE  MiscellaneousInfoId=@MiscellaneousInfoId";


           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }
       public bool SaveDocumentDetails(List<MiscellaneousInfoDocumentDAO> aList, int masterid)
       {
           try
           {
               List<SqlParameter> aParametersd = new List<SqlParameter>();
               aParametersd.Add(new SqlParameter("@MeetingInfoId", masterid));
               string queryDel = @"DELETE FROM [dbo].[tblMeeting_MintuesEntryInfoDocument]
      WHERE  MeetingInfoId=@MeetingInfoId";

               bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


               bool result = false;
               foreach (var item in aList)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@MeetingInfoId", masterid));
                   aParameters.Add(new SqlParameter("@DocumentLink", item.DocumentLink));
                   aParameters.Add(new SqlParameter("@DocumentNote", (object)item.DocumentNote ?? DBNull.Value));
                   aParameters.Add(new SqlParameter("@FileName", item.FileName));
                   aParameters.Add(new SqlParameter("@ExtractedText", (object)item.ExtractedText ?? DBNull.Value));
                   aParameters.Add(new SqlParameter("@DocType", (object)item.DocType ?? DBNull.Value));




                   string query = @"INSERT INTO [dbo].[tblMeeting_MintuesEntryInfoDocument]
           ([MeetingInfoId]
           ,[DocumentLink]
           ,[DocumentNote],FileName,ExtractedText,DocType)
     VALUES
           (@MeetingInfoId
           ,@DocumentLink
           ,@DocumentNote,@FileName,@ExtractedText,@DocType)";
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
       public int SavAppLog(MeetingInfoAppLogIdDAO appLogDao)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@MeetingInfoID", appLogDao.MeetingInfoID));
                   aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                   aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));

                   aParameters.Add(new SqlParameter("@ApprovedBy", appLogDao.ApprovedBy));
                   aParameters.Add(new SqlParameter("@ApprovedDate", appLogDao.ApprovedDate));
                   aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                   aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));



                   string query = @"INSERT INTO [dbo].[tblMeeting_MeetingInfoAppLog]
           ([MeetingInfoID]
           ,[PreEmpInfoId]
           ,[ForEmpInfoId]
           ,[Version]
           ,[ApprovedBy]
           ,[ApprovedDate]
           ,[ActionStatus]
           ,[Comments])
     VALUES
           (@MeetingInfoID
           ,@PreEmpInfoId
           ,@ForEmpInfoId
           ,(SELECT (COUNT(*)+1) FROM dbo.tblMeeting_MeetingInfoAppLog WITH (NOLOCK) WHERE MeetingInfoID=@MeetingInfoID)
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

       public bool SaveAgendaDetails(List<MeetingInfoAgendaDAO> aList, int masterid)
       {
           try
           {
               List<SqlParameter> aParametersd = new List<SqlParameter>();
               aParametersd.Add(new SqlParameter("@MeetingInfoId", masterid));
               string queryDel = @"DELETE FROM [dbo].[tblMeeting_MeetingInfoAgenda]
      WHERE  MeetingInfoId=@MeetingInfoId";

               bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);

              
               bool result = false;

               int serialNumberCounter = 1;
               foreach (var item in aList)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@MeetingInfoId", masterid));
                   aParameters.Add(new SqlParameter("@Agenda", item.Agenda));
                   aParameters.Add(new SqlParameter("@Remarks", item.Remarks));
                   aParameters.Add(new SqlParameter("@Observation", item.Observation));
                   aParameters.Add(new SqlParameter("@Decision", item.Decision));
                   aParameters.Add(new SqlParameter("@PresentorId", item.PresentorId ?? (object)DBNull.Value));
                   aParameters.Add(new SqlParameter("@ImplementationStatus", item.ImplementationStatus));
                   aParameters.Add(new SqlParameter("@AgendaNo", serialNumberCounter));

                  
                   serialNumberCounter++;
                 
                      
                   string query = @"
INSERT INTO [dbo].[tblMeeting_MeetingInfoAgenda]
           ([MeetingInfoID]
           ,[Agenda]
           ,[PresentorId],Remarks, Observation, Decision,ImplementationStatus,AgendaNo)
     VALUES
           (@MeetingInfoID 
           ,@Agenda 
           ,@PresentorId,@Remarks, @Observation, @Decision,@ImplementationStatus,@AgendaNo )";
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

        public string GetCompanyIdByEmpInfoId(string empInfoId)
        {
            try
            {
                string query = "SELECT CompanyId FROM dbo.tblEmpGeneralInfo WITH (NOLOCK) WHERE EmpInfoId = " + empInfoId;
                System.Data.DataTable dt = aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["CompanyId"].ToString();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        public System.Data.DataTable GetEmpDetailsByEmpInfoId(string empInfoId)
        {
            try
            {
                string query = "SELECT emp.EmpName, emp.EmpMasterCode, des.DesignationName AS Designation FROM dbo.tblEmpGeneralInfo emp WITH (NOLOCK) LEFT JOIN dbo.tblDesignation des WITH (NOLOCK) ON des.DesignationId = emp.DesignationId WHERE emp.EmpInfoId = " + empInfoId;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch
            {
                return null;
            }
        }

        // Top-N, server-filtered employee lookup for the Add Employees row typeahead —
        // avoids loading a company's entire active roster into a GridView row's
        // DropDownList (and ViewState) on every postback like GetDDLEmpInfo did.
        public System.Data.DataTable SearchEmpForMeeting(string companyId, string term)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CompanyId", companyId),
                new SqlParameter("@Term", "%" + (term ?? string.Empty) + "%")
            };
            string query = @"SELECT TOP 20 emp.EmpInfoId AS EmpInfoId, emp.EmpMasterCode AS EmpMasterCode,
                                     emp.EmpName AS EmpName, ISNULL(des.Designation,'') AS Designation
                              FROM dbo.tblEmpGeneralInfo emp WITH (NOLOCK)
                              LEFT JOIN dbo.tblDesignation des WITH (NOLOCK) ON des.DesignationId = emp.DesignationId
                              WHERE emp.IsActive = 1 AND emp.CompanyId = @CompanyId
                                AND (emp.EmpName LIKE @Term OR emp.EmpMasterCode LIKE @Term)
                              ORDER BY emp.EmpName";
            return aCommonInternalDal.DataContainerDataTable(query, parameters, DataBase.HRDB);
        }
    }
}
