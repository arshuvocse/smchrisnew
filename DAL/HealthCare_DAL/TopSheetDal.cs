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
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;

namespace DAL.HealthCare_DAL
{
    public class TopSheetDal
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

        public DataTable Get_TopSheet_RPT(int param)
        {



            // (BS.CurrentBlnce-BS.PaybleAmount) AS ClosingBalance

            try
            {
                string query = @"

  WITH RankedFeedback AS (
    SELECT
        ReimbursFromMasterId,
        Feedback,
        ROW_NUMBER() OVER (PARTITION BY ReimbursFromMasterId ORDER BY FeedbackDate DESC) AS RowNum
    FROM
        tblCommitteeFeedback_HC
)
Select ROW_NUMBER() Over (Order by TH.TopsheetGeneMasId) As SL,  rm.HeadofDptDate, '' ActualAmount, hso.HospitalName, isnull('Hospital Admission Date: '+format(HospitalAdmissionDate,'dd-MMM-yyyy'),'') + isnull('  '+'Hospital Discharge Date: '+format(HospitalDischargeDate,'dd-MMM-yyyy'),'') HospitalAddmissionDischargeDate, ISNULL(RB.RMBalance,0) AS ReminingBlanace,     '' BlankRemarks, RM.CompanyId,  case when  RM.CompanyId=1 then 'Human Resource Division'  else dv.DivisionName  end DivisionName , Emp.SalaryLoationId, com.CompanyName, RM.Type,  case when tblDoc.ReimbursFromMasterId is not null then 'Yes' else 'No' end DocumentStatus, FORMAT(tblDocDate.DocDate,'dd-MMM-yyyy') DocVisitDate, Dpt.DepartmentName,  RM.Relationship, RM.SelfDate as PatientName, FORMAT(RM.SubmitDate,'dd-MMM-yyyy') SubmitDate, RM.BankName,Rm.BankAccountNo, Rm.RoutingNo,RM.BranchName,  TH.TopsheetGeneMasId, TH.MeetingNo, TH.MeetingDate, TH.MeetingTime, RM.RequitisionNo AS ClaimNo, EMP.EmpName, EMP.EmpMasterCode, DG.Designation, SL.SalaryLocation, RM.Relationship, RM.PatientName AS Illness, RM.SubmitDate,
ISNULL(ClaimTKByHR,tblamt.Amount) AS Ceilling, '' AS BillAmount, ISNULL(ClaimTKByUser,tblamt.Amount)  AS ApplicableAmount, SEC.SectionName   AS ClosingBalance, Comm.Feedback AS Remark, Comment.Comments as Venue  from TopSheetGenerateMaster_H TH 
LEFT JOIN TopSheetGenerateDetails_H TD ON TH.TopsheetGeneMasId = TD.TopsheetGeneMasId
LEFT JOIN tbl_ReimbursmentFormMaster_HealthCare RM ON  TD.ReimbursFromMasterId = RM.ReimbursFromMasterId 
LEFT JOIN tblEmpGeneralInfo EMP ON RM.EmpInfoId = EMP.EmpInfoId
LEFT JOIN tblSection SEC ON EMP.SectionId = SEC.SectionId 
LEFT JOIN tblSalaryLocation SL ON Emp.SalaryLoationId = SL.SalaryLoationId
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision dv ON EMP.DivisionId = dv.DivisionId
LEFT JOIN tblCompanyInfo com ON EMP.CompanyId = com.CompanyId
LEFT JOIN (SELECT A.ReimbursFromMasterId ,A.Comments
FROM tbl_ReimbursmentFormMaster_HealthCare H
LEFT JOIN tblReimbursementSelfAppLog A ON H.ReimbursFromMasterId = A.ReimbursFromMasterId
WHERE A.Version = (
    SELECT MAX(Version)
    FROM tblReimbursementSelfAppLog
    WHERE ReimbursFromMasterId = H.ReimbursFromMasterId
)

) Comment on RM.ReimbursFromMasterId = Comment.ReimbursFromMasterId
LEFT JOIN (SELECT  TOP 1 Feedback, ReimbursFromMasterId FROM tblCommitteeFeedback_HC WHERE ReimbursFromMasterId IS NOT NULL AND Feedback IS NOT NUll  order By ComfeedbackId DESC ) feedbak On feedbak.ReimbursFromMasterId = RM.ReimbursFromMasterId
LEFT JOIN tbl_billSettlement_Healthcare BS ON RM.ReimbursFromMasterId = BS.ReimbursFromMasterId
LEFT JOIN (
    SELECT
        ReimbursFromMasterId,
        Feedback
    FROM
        RankedFeedback
    WHERE
        RowNum = 1
) AS Comm ON Comm.ReimbursFromMasterId = RM.ReimbursFromMasterId
left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=RM.ReimbursFromMasterId 
left join (select ReimbursFromMasterId, min(tbl_ReimbursmentformClaimDetails_HC.Dates) DocDate from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblDocDate on tblDocDate.ReimbursFromMasterId=RM.ReimbursFromMasterId 
left join (select ReimbursFromMasterId from tbl_ReimbursmentDocument_HealthCare  group by ReimbursFromMasterId) tblDoc on tblDoc.ReimbursFromMasterId=RM.ReimbursFromMasterId 

LEFT JOIN tblHospitalName hso ON hso.HospitalNameId = RM.HospitalNameId

OUTER APPLY  fn_GET_RemainingBalance
(
    rm.Type,
    EMP.EmpInfoId,        -- @EmpId (আপনার SP/function যেটা expects)
    RM.FinancialYearId,
    RM.CompanyId,
    RM.Type
) RB

WHERE TH.TopsheetGeneMasId IS NOT NULL AND TH.TopsheetGeneMasId=" + param;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Get_TopSheet_RPTCSV(int param)
        {



            // (BS.CurrentBlnce-BS.PaybleAmount) AS ClosingBalance

            try
            {
                string query = @"
  WITH RankedFeedback AS (
    SELECT
        ReimbursFromMasterId,
        Feedback,
        ROW_NUMBER() OVER (PARTITION BY ReimbursFromMasterId ORDER BY FeedbackDate DESC) AS RowNum
    FROM
        tblCommitteeFeedback_HC
)
Select ROW_NUMBER() Over (Order by TH.TopsheetGeneMasId) As SL,  RM.CompanyId, RM.Type, Emp.SalaryLoationId, '' CustomerReference, EMP.EmpName PayeeName, RM.BankAccountNo  PayeeBankAccNo, '' PayeeAccType, RM.RoutingNo  PayeeBankRouting, ISNULL(ClaimTKByUser,tblamt.Amount)    Amount   , '' Reason, '' PaymentDate, '' DebitACNo, 's.akter@smc-bd.org ; tanjila.shormi@smc-bd.org' PayeeEmailAddress   from TopSheetGenerateMaster_H TH 
LEFT JOIN TopSheetGenerateDetails_H TD ON TH.TopsheetGeneMasId = TD.TopsheetGeneMasId
LEFT JOIN tbl_ReimbursmentFormMaster_HealthCare RM ON  TD.ReimbursFromMasterId = RM.ReimbursFromMasterId 
LEFT JOIN tblEmpGeneralInfo EMP ON RM.EmpInfoId = EMP.EmpInfoId
LEFT JOIN tblSection SEC ON EMP.SectionId = SEC.SectionId 
LEFT JOIN tblSalaryLocation SL ON Emp.SalaryLoationId = SL.SalaryLoationId
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision dv ON EMP.DivisionId = dv.DivisionId
LEFT JOIN tblCompanyInfo com ON EMP.CompanyId = com.CompanyId
LEFT JOIN (SELECT A.ReimbursFromMasterId ,A.Comments
FROM tbl_ReimbursmentFormMaster_HealthCare H
LEFT JOIN tblReimbursementSelfAppLog A ON H.ReimbursFromMasterId = A.ReimbursFromMasterId
WHERE A.Version = (
    SELECT MAX(Version)
    FROM tblReimbursementSelfAppLog
    WHERE ReimbursFromMasterId = H.ReimbursFromMasterId
)

) Comment on RM.ReimbursFromMasterId = Comment.ReimbursFromMasterId
LEFT JOIN (SELECT  TOP 1 Feedback, ReimbursFromMasterId FROM tblCommitteeFeedback_HC WHERE ReimbursFromMasterId IS NOT NULL AND Feedback IS NOT NUll  order By ComfeedbackId DESC ) feedbak On feedbak.ReimbursFromMasterId = RM.ReimbursFromMasterId
LEFT JOIN tbl_billSettlement_Healthcare BS ON RM.ReimbursFromMasterId = BS.ReimbursFromMasterId
LEFT JOIN (
    SELECT
        ReimbursFromMasterId,
        Feedback
    FROM
        RankedFeedback
    WHERE
        RowNum = 1
) AS Comm ON Comm.ReimbursFromMasterId = RM.ReimbursFromMasterId
left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=RM.ReimbursFromMasterId 
left join (select ReimbursFromMasterId, min(tbl_ReimbursmentformClaimDetails_HC.Dates) DocDate from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblDocDate on tblDocDate.ReimbursFromMasterId=RM.ReimbursFromMasterId 
left join (select ReimbursFromMasterId from tbl_ReimbursmentDocument_HealthCare  group by ReimbursFromMasterId) tblDoc on tblDoc.ReimbursFromMasterId=RM.ReimbursFromMasterId 

LEFT JOIN tblHospitalName hso ON hso.HospitalNameId = RM.HospitalNameId

OUTER APPLY  fn_GET_RemainingBalance
(
    rm.Type,
    EMP.EmpInfoId,        -- @EmpId (আপনার SP/function যেটা expects)
    RM.FinancialYearId,
    RM.CompanyId,
    RM.Type
) RB

WHERE TH.TopsheetGeneMasId IS NOT NULL AND TH.TopsheetGeneMasId=" + param;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Get_Convenor_MemberSecretory_RPT(string ApplicationType, string CompanyId, string SalaryLoationId)
        {
            try
            {
                string query = @"Select  top 1   tblcon.conName ConvenorID,  EMP.EmpName ConvenorName,  tblHR.DepartmentName ConvenorDepartmentName, DG.Designation  ConvenorDesignation, MemberSecretoryID, emberSecretoryName, MemberSecretoryDepartmentName,MemberSecretoryDesignation from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId


left join (Select   M.ApplicationType, M.CompanyId, m.SalaryLoationId, EMP.EmpMasterCode MemberSecretoryID, EMP.EmpName emberSecretoryName, ISNULL(Dpt.DepartmentName, Div.DivisionName  ) MemberSecretoryDepartmentName, DG.Designation  MemberSecretoryDesignation from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId

where    IsMemberSecretory=1 and   M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId=" + SalaryLoationId + @") tblSec on M.ApplicationType=tblSec.ApplicationType and M.CompanyId=tblSec.CompanyId and M.SalaryLoationId=tblSec.SalaryLoationId


left join (Select   M.ApplicationType, M.CompanyId, m.SalaryLoationId,  EMP.EmpName conName  from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId

where    IsConvenor=1 and   M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId=" + SalaryLoationId + @") tblcon on M.ApplicationType=tblcon.ApplicationType and M.CompanyId=tblSec.CompanyId and M.SalaryLoationId=tblcon.SalaryLoationId


left join (Select   M.ApplicationType, M.CompanyId, m.SalaryLoationId,  EMP.EmpName conName, Dpt.DepartmentName  from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId

where    IsForward=1 and   M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId="+SalaryLoationId+@") tblHR on M.ApplicationType=tblHR.ApplicationType and M.CompanyId=tblSec.CompanyId and M.SalaryLoationId=tblcon.SalaryLoationId

where M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId=" + SalaryLoationId + @" and IsDoctor=1
 ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Get_Convenor_MemberSecretory_RPT_mail(string ApplicationType, string CompanyId, string SalaryLoationId)
        {
            try
            {
                string query = @"Select  top 1   tblcon.conName ConvenorID,  EMP.EmpMasterCode ConvenorName,  ISNULL(Dpt.DepartmentName, Div.DivisionName  ) ConvenorDepartmentName, DG.Designation  ConvenorDesignation, MemberSecretoryID, emberSecretoryName, MemberSecretoryDepartmentName,MemberSecretoryDesignation from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId


left join (Select   M.ApplicationType, M.CompanyId, m.SalaryLoationId, EMP.EmpMasterCode MemberSecretoryID, EMP.EmpName emberSecretoryName, ISNULL(Dpt.DepartmentName, Div.DivisionName  ) MemberSecretoryDepartmentName, DG.Designation  MemberSecretoryDesignation from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId

where    IsMemberSecretory=1 and   M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId=" + SalaryLoationId + @") tblSec on M.ApplicationType=tblSec.ApplicationType and M.CompanyId=tblSec.CompanyId and M.SalaryLoationId=tblSec.SalaryLoationId


left join (Select   M.ApplicationType, M.CompanyId, m.SalaryLoationId,  EMP.EmpMasterCode conName  from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId

where    IsConvenor=1 and   M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId=" + SalaryLoationId + @") tblcon on M.ApplicationType=tblcon.ApplicationType and M.CompanyId=tblSec.CompanyId and M.SalaryLoationId=tblcon.SalaryLoationId

where M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId=" + SalaryLoationId + @" and IsDoctor=1
 ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Get_Member_RPT(string ApplicationType, string CompanyId, string SalaryLoationId)
        {
            try
            {
                string query = @"Select  EMP.EmpMasterCode ConvenorID,  EMP.EmpName ConvenorName,  ISNULL(Dpt.DepartmentName, Div.DivisionName  ) ConvenorDepartmentName, DG.Designation  ConvenorDesignation   from tblCommiteeSetupMaster M 
inner  JOIN tblCommiteeSetupDetails dtl On M.ComSetupMasId = dtl.ComSetupMasId
 LEFT JOIN tblEmpGeneralInfo EMP ON dtl.EmpInfoId = EMP.EmpInfoId
 
LEFT JOIN tblDesignation DG ON EMP.DesignationId = DG.DesignationId
LEFT JOIN tblDepartment Dpt ON EMP.DepartmentId = Dpt.DepartmentId
LEFT JOIN tblDivision Div ON EMP.DivisionId = Div.DivisionId


where M.ApplicationType='" + ApplicationType + "' and M.CompanyId=" + CompanyId + @" and m.SalaryLoationId=" + SalaryLoationId + @" and IsMember=1
 ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Get_TopSheet(string parm )
        {
            try
            {
                string query = @"SELECT distinct convert(varchar(20),MeetingTime,100) MeetingTime, ISNULL(tblOPD.OPDAmt,0) OPDAmt,  ISNULL(tblIPD.IPDAmt,0) IPDAmt,ISNULL(tblCount.AppCount,0) AppCount,   EN.UserName EntryBy, Un.UserName UpdateBy,  M.* FROM TopSheetGenerateMaster_H M 
LEFT JOIN tblUser EN ON M.EntryBy = EN.UserId
LEFT JOIN tblUser Un ON M.UpdateBy = Un.UserId   
left join (select dtl.TopsheetGeneMasId,  ISNULL(SUM(clm.Amount),0) OPDAmt  from TopSheetGenerateDetails_H dtl
inner join tbl_ReimbursmentFormMaster_HealthCare frm on  dtl.ReimbursFromMasterId=frm.ReimbursFromMasterId
inner join tbl_ReimbursmentformClaimDetails_HC clm on  clm.ReimbursFromMasterId=frm.ReimbursFromMasterId
where frm.Type='OPD'
group by dtl.TopsheetGeneMasId
) tblOPD on  tblOPD.TopsheetGeneMasId=M.TopsheetGeneMasId

left join (select dtl.TopsheetGeneMasId,  ISNULL(SUM(clm.Amount),0) IPDAmt  from TopSheetGenerateDetails_H dtl
inner join tbl_ReimbursmentFormMaster_HealthCare frm on  dtl.ReimbursFromMasterId=frm.ReimbursFromMasterId
inner join tbl_ReimbursmentformClaimDetails_HC clm on  clm.ReimbursFromMasterId=frm.ReimbursFromMasterId
where frm.Type='IPD'
group by dtl.TopsheetGeneMasId
) tblIPD on  tblIPD.TopsheetGeneMasId=M.TopsheetGeneMasId

left join (Select  TH.TopsheetGeneMasId,  ISNULL(count(TH.TopsheetGeneMasId),0) AppCount  from TopSheetGenerateMaster_H TH 
LEFT JOIN TopSheetGenerateDetails_H TD ON TH.TopsheetGeneMasId = TD.TopsheetGeneMasId
LEFT JOIN tbl_ReimbursmentFormMaster_HealthCare RM ON  TD.ReimbursFromMasterId = RM.ReimbursFromMasterId 
 
LEFT JOIN tbl_billSettlement_Healthcare BS ON RM.ReimbursFromMasterId = BS.ReimbursFromMasterId
 
WHERE TH.TopsheetGeneMasId IS NOT NULL 

group by TH.TopsheetGeneMasId
) tblCount on  tblCount.TopsheetGeneMasId=M.TopsheetGeneMasId

LEFT JOIN (SELECT  DISTINCT TopsheetGeneMasId, R.CompanyId FROM TopSheetGenerateDetails_H H
LEFT JOIN tbl_ReimbursmentFormMaster_HealthCare R ON  H.ReimbursFromMasterId= R.ReimbursFromMasterId
) Company on Company.TopsheetGeneMasId = M.TopsheetGeneMasId

 where M.TopsheetGeneMasId is not null    " + parm + " order by m.EntryDate desc";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable Get_TopSheet_BillSattelment()
        {
            try
            {
                string query = @"SELECT EN.UserName EntryBy, Un.UserName UpdateBy, * FROM TopSheetGenerateMaster_H M 
LEFT JOIN tblUser EN ON M.EntryBy = EN.UserId
LEFT JOIN tblUser Un ON M.UpdateBy = Un.UserId

where m.TopsheetGeneMasId  not  in ( select  TopsheetGeneMasId  from  TopSheetGenerateDetails_H t
 inner join  [tbl_billSettlement_Healthcare] bil on bil.ReimbursFromMasterId=t.ReimbursFromMasterId 
 inner join  tbl_ReimbursmentFormMaster_HealthCare mas on mas.ReimbursFromMasterId=bil.ReimbursFromMasterId  where ISNULL(mas.PaymentStatus,'')='Full' )   order by M.EntryDate desc";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Get_TopSheet_meetingCheck(string Id)
        {
            try
            {
                string query = @"Select * from TopSheetGenerateMaster_H Where MeetingNo='"+Id+"' ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Get_CommitteeSetupById(int ID)
        {
            try
            {
                string query = @"SELECT Emp.EmpMasterCode+ ':'+Emp.EmpName EmpName ,* FROM tblCommiteeSetupMaster M 
LEFT JOIN tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId
LEFT JOIN tblEmpGeneralInfo EMP ON D.EmpInfoId = Emp.EmpInfoId
WHERE M.ComSetupMasId=" + ID;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Get_FeedBack(string ID)
        {
            try
            {
                string query = @"SELECT * FROM tblCommitteeFeedback_HC WHERE ReimbursFromMasterId=" + ID;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save_TopSheet(TopSheetGeneMasterDao aMaster, List<TopSheetGenerateDetailsDao> aDetailsDaos, List<ReimbursementSelfAppLogDAO>seList)
        {
            int pk = 0;
            int masterId = 0;
            bool status = false;
            try
            {
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MeetingTitle", aMaster.MeetingTitle));
                aParameters.Add(new SqlParameter("@MeetingNo", aMaster.MeetingNo));
                aParameters.Add(new SqlParameter("@Venue", aMaster.Venue));
                aParameters.Add(new SqlParameter("@MeetingDate", aMaster.MeetingDate));
                aParameters.Add(new SqlParameter("@MeetingTime", aMaster.MeetingTime));
                aParameters.Add(new SqlParameter("@Isactive", aMaster.Isactive));
                aParameters.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));

                if (aMaster.TopsheetGeneMasId == 0)
                {
                    string query =
                        @" INSERT INTO [dbo].[TopSheetGenerateMaster_H]
           ([MeetingTitle]
           ,[MeetingNo]
           ,[Venue]
           ,[MeetingTime]
           ,[MeetingDate]
           ,[EntryBy]
           ,[EntryDate]
           ,[Isactive], MeetingStatus)
     VALUES
           (@MeetingTitle
           ,@MeetingNo
           ,@Venue
           ,@MeetingTime
           ,@MeetingDate
           ,@EntryBy
           ,GETDATE()
           ,@Isactive, 'Pending')";

                    pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);

                    if (pk > 0)
                    {
                        
                        bool result = false;

                        foreach (TopSheetGenerateDetailsDao item in aDetailsDaos)
                        {
                            List<SqlParameter> gParameters = new List<SqlParameter>();
                            gParameters.Add(new SqlParameter("@TopsheetGeneMasId", pk));
                            gParameters.Add(new SqlParameter("@ReimbursFromMasterId", item.ReimbursFromMasterId));
                            string Deatilsquery = @"INSERT INTO [dbo].[TopSheetGenerateDetails_H]
                                                    ([TopsheetGeneMasId]
                                                    ,[ReimbursFromMasterId])
                                                     VALUES
                                                     (@TopsheetGeneMasId
                                                      ,@ReimbursFromMasterId)";
                            result = aCommonInternalDal.SaveDataByInsertCommand(Deatilsquery, gParameters, DataBase.HRDB);
                        }




                        foreach (ReimbursementSelfAppLogDAO item in seList)
                        {

                            List<SqlParameter> pParameters = new List<SqlParameter>();
                            pParameters.Add(new SqlParameter("@ReimbursFromMasterId", item.ReimbursFromMasterId));
                            pParameters.Add(new SqlParameter("@PreEmpInfoId", item.PreEmpInfoId));
                            pParameters.Add(new SqlParameter("@ForEmpInfoId", item.ForEmpInfoId));
                            pParameters.Add(new SqlParameter("@Version", item.Version));
                            pParameters.Add(new SqlParameter("@ApproveBy", item.ApproveBy));
                            pParameters.Add(new SqlParameter("@ApproveDate", item.ApproveDate));
                            pParameters.Add(new SqlParameter("@ActionStatus", item.ActionStatus));
                            pParameters.Add(new SqlParameter("@Comments", (object)item.Comments ?? DBNull.Value));
                            pParameters.Add(new SqlParameter("@CommentsEMPID", (object)item.CommentsEMP ?? DBNull.Value));

                            string Lg_query = @"    



UPDATE tbl_ReimbursmentFormMaster_HealthCare SET  ShowStatus='In Process' Where ReimbursFromMasterId = @ReimbursFromMasterId

INSERT INTO dbo.tblReimbursementSelfAppLog
                                    (
                                    ReimbursFromMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentsEMPID
                                    )
                                    VALUES(
                                    @ReimbursFromMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblReimbursementSelfAppLog WHERE ReimbursFromMasterId=@ReimbursFromMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsEMPID
                                    )";

                            masterId = aCommonInternalDal.SaveDataByInsertCommandById(Lg_query, pParameters, DataBase.HRDB);


                        }

                    }

                }
                else
                {
//                    aParameters.Add(new SqlParameter("@ComSetupMasId", aMaster.ComSetupMasId));
//                    aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));

//                    string query = @"UPDATE [dbo].[tblCommiteeSetupMaster]
//                        SET [IsOPD] = @IsOPD
//                            ,[SalaryLoationId] = @SalaryLoationId
//                        ,[CompanyId] = @CompanyId
//                      
//                        ,[UpdateBy] = @UpdateBy
//                        ,[UpdateDate] = GETDATE()
//                        
//                        WHERE ComSetupMasId=@ComSetupMasId";

//                    status = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

//                    if (status)
//                    {

//                        string DeleteQuery = @"DELETE FROM tblCommiteeSetupDetails WHERE ComSetupMasId=" + aMaster.ComSetupMasId;
//                        aCommonInternalDal.DeleteDataByDeleteCommand(DeleteQuery, DataBase.HRDB);

//                        foreach (CommetteeSetupDetailsDao item in aDetailsDaos)
//                        {
//                            List<SqlParameter> gParameters = new List<SqlParameter>();
//                            gParameters.Add(new SqlParameter("@ComSetupMasId", aMaster.ComSetupMasId));
//                            gParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
//                            gParameters.Add(new SqlParameter("@IsForward", item.IsApproved));
//                            gParameters.Add(new SqlParameter("@IsApproved", item.IsForward));
//                            string Deatilsquery = @"INSERT INTO [dbo].[tblCommiteeSetupDetails]
//                                ([ComSetupMasId]
//                                ,[EmpInfoId]
//                                ,[IsForward]
//                                ,[IsApproved])
//                                 VALUES
//                                (@ComSetupMasId
//                                ,@EmpInfoId 
//                                ,@IsForward 
//                                ,@IsApproved )";
//                            aCommonInternalDal.SaveDataByInsertCommand(Deatilsquery, gParameters, DataBase.HRDB);
//                        }

//                        pk = aMaster.ComSetupMasId;

//                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pk;
        }

        public void GetDDLSalaryLocation(DropDownList ddl)
        {
            string queryStr = @"SELECT dt.SalaryLoationId AS Value, dt.SalaryLocation AS TextField FROM dbo.tblSalaryLocation dt WHERE dt.IsActive=1";

            aCommonInternalDal.LoadDropDownValue(ddl, "TextField", "Value", queryStr, DataBase.HRDB);
        }

        public void GetDDLCompany(DropDownList ddl)
        {
            string queryStr = @"Select CompanyId , ShortName from tblCompanyInfo";

            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);
        }

        public void GetDDLEmployee(DropDownList ddl)
        {
            string queryStr = @"Select EmpInfoId, EmpMasterCode + ':'+ EmpName As EmpName  from tblEmpGeneralInfo Where IsActive=1";

            aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", queryStr, DataBase.HRDB);
        }


        public DataTable Get_Nominated_Committee(string ApplicationType, string SalaryLocation, string CompanyId)
        {
            try
            {
                string query = @"SELECT  D.EmpInfoId from tblCommiteeSetupMaster M
LEFT JOIN tblCommiteeSetupDetails D ON D.ComSetupMasId = M.ComSetupMasId
WHERE M.ComSetupMasId IS NOT NULL AND IsApproved=1 AND M.SalaryLoationId= " + SalaryLocation + " AND M.CompanyId= " + CompanyId + " AND M.ApplicationType='" + ApplicationType + "' ";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
