using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Audit_Dal
{
    public class EmployeeAuditDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable LoadInfoAuditTrail(string parameter2, string deletepara)
        {
            string query = @"Select 'btn btn-sm btn-warning' StatusStyle,'Edit' Status,EPE.EmployeePromotionEntryId,  Emp.EmpMasterCode,Emp.EmpName,  DEG.Designation, 
  NDEG.DepartmentName , PT.PromotionTypeName , EPE.Effectivedate, U.UserName As DeleteBy, EPE.UpdateDate as DeleteDate, EmployeeId 
  From tblEmployeePromotionEntry EPE   with (NOLOCk)
  left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
  left JOIN dbo.tblDesignation  DEG ON Emp.DesignationId = DEG.DesignationId
  left JOIN dbo.tblDepartment  NDEG ON Emp.DepartmentId = NDEG.DepartmentId 
  left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
  left JOIN dbo.tblPromotionType PT ON EPE.NPromoTypeId =PT.PromotionTypeId
  left JOIN dbo.tblUser U ON EPE.UpdateBy = U.UserId

  where ((EPE.UpdateBy IS NOT NUll) and (epe.IsDelete=0 or epe.IsDelete is Null))  " + parameter2+" " + " "+
 @"Union All
  Select 'btn btn-sm btn-danger' StatusStyle,'Delete' Status,EPE.EmployeePromotionEntryId,  Emp.EmpMasterCode,Emp.EmpName,  DEG.Designation, 
  NDEG.DepartmentName , PT.PromotionTypeName , EPE.Effectivedate, De.UserName as DeleteBy, EPE.DeleteDate , EmployeeId 
  From tblEmployeePromotionEntry EPE   with (NOLOCk)
  left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
  left JOIN dbo.tblDesignation  DEG ON Emp.DesignationId = DEG.DesignationId
  left JOIN dbo.tblDepartment  NDEG ON Emp.DepartmentId = NDEG.DepartmentId 
  left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
  left JOIN dbo.tblPromotionType PT ON EPE.NPromoTypeId =PT.PromotionTypeId

  left JOIN dbo.tblUser De ON EPE.DeleteBy = De.UserId
  where (epe.IsDelete=1 ) " + deletepara+" ";
            
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable Re_Designation( string param)
        {
            string query = @"
Select mas.AuditTrail_EmployeeReDesignationId, mas.StatusMode,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger'   ELSE  'btn btn-sm btn-warning' end  StatusStyle,
mas.EmployeeReDesignationId, mas.Remarks, Emp.EmpName, Emp.EmpMasterCode, newDe.Designation,
fy.FinancialYearDesc, com.ShortName, mas.Effectivedate, mas.ModifyBy, mas.ModifyDate, tblUser.UserName  from tblEmployeeReDesignation_AuditTrail mas
Left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
Left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId 
Left join tblDesignation newDe On newDe.DesignationId = mas.NDesignationId
left join tblEmpGeneralInfo Emp on emp.EmpInfoId = mas.EmployeeId
Left JOIN dbo.tblUser On tblUser.UserId = mas.ModifyBy
WHERE mas.AuditTrail_EmployeeReDesignationId = 
(SELECT max(AuditTrail_EmployeeReDesignationId) FROM tblEmployeeReDesignation_AuditTrail t2 WHERE t2.EmployeeReDesignationId = mas.EmployeeReDesignationId) "+param+" ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable TransferLoadDal(string parameter2 , string deletePara)
        {
            string query =
                @"Select  'btn btn-sm btn-warning' StatusStyle,'Edit' Status, EmpTransferAndRedesignationId, EmployeeId, Emp.EmpMasterCode, emp.EmpName,com.ShortName, deg.Designation, Dpt.DepartmentName,
        U.UserName As DeleteBy,  ETR.UpdateDate as DeleteDate,
        ETR.Effectivedate FROM tblEmpTransferAndRedesignation  ETR  with (Nolock)
        INNER JOIN dbo.tblEmpGeneralInfo  Emp ON ETR.EmployeeId = Emp.EmpInfoId
        left JOIN dbo.tblDesignation  Deg ON Emp.DesignationId = Deg.DesignationId
        left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
        LEFT JOIN dbo.tblCompanyInfo  Com ON  Com.CompanyId= ETR.NewCompanyId 
        left JOIN dbo.tblUser U ON ETR.EntryBy = U.UserId

        where ((ETR.UpdateBy IS NOT NUll) and (ETR.IsDelete=1 or ETR.IsDelete is Null))  " + parameter2 + " " + " " +
                @"Union all
        Select 'btn btn-sm btn-danger' StatusStyle,'Delete' Status, EmpTransferAndRedesignationId, EmployeeId, Emp.EmpMasterCode, emp.EmpName,com.ShortName, deg.Designation, Dpt.DepartmentName,
        De.UserName as DeleteBy ,ETR.DeleteDate,
        ETR.Effectivedate FROM tblEmpTransferAndRedesignation  ETR  with (Nolock)
        INNER JOIN dbo.tblEmpGeneralInfo  Emp ON ETR.EmployeeId = Emp.EmpInfoId
        left JOIN dbo.tblDesignation  Deg ON Emp.DesignationId = Deg.DesignationId
        left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
        LEFT JOIN dbo.tblCompanyInfo  Com ON  Com.CompanyId= ETR.NewCompanyId 

		left JOIN dbo.tblUser De ON ETR.EntryBy = De.UserId
        where (ETR.IsDelete=1) " + deletePara+" "; 
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable IncrementLoadDal(string parameter2)
        {
//            string query = @" Select 'btn btn-sm btn-warning' StatusStyle,'Edit' Status, INC.IncrementId, INC.EmployeeId, INC.EmployeeCode as EmpMasterCode,EGI.EmpName,DSG.Designation,DPT.DepartmentName, 
//                             INC.EffectiveDate, U.UserName As DeleteBy, INC.UpdateDate as DeleteDate  FROM dbo.tblIncrement AS INC
//                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
//                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
//                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
//                             left JOIN dbo.tblUser U ON INC.EntryBy = U.UserId
//							
//					         where ((INC.UpdateBy IS NOT NUll) and (INC.IsDelete=1 or INC.IsDelete is Null)) " + parameter2 + " " + " " +
//                             @"Union all						 
//                             Select 'btn btn-sm btn-danger' StatusStyle,'Delete' Status, INC.IncrementId, INC.EmployeeId, INC.EmployeeCode as EmpMasterCode,EGI.EmpName,DSG.Designation,DPT.DepartmentName, 
//                             INC.EffectiveDate,  De.UserName as DeleteBy, INC.DeleteDate  FROM dbo.tblIncrement AS INC
//                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
//                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
//                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
//                  
//							 left JOIN dbo.tblUser De ON INC.DeleteBy = De.UserId 
//					         where (INC.IsDelete=1) " + DeletePara+" ";


            string query = @"Select mas.IncrementId,mas.StatusMode, CASE WHEN  mas.StatusMode='UPDATE' THEN  'btn btn-sm btn-warning' ELSE 'btn btn-sm btn-danger' end StatusStyle  , com.ShortName, Fy.FinancialYearDesc, EffectiveDate, Emp.EmpMasterCode , Emp.EmpName,
 ISTP.SalaryStepName as IncremantalStep, IIM.Name as IncrementType, StatusMode, tblUser.UserName, ModifyDate from tblIncrement_AuditTrail mas
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
 LEFT JOIN dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
 LEFT JOIN dbo.tblEmpGeneralInfo Emp ON Emp.EmpInfoId= mas.EmployeeId
 LEFT JOIN dbo.tblSalaryStep AS ISTP ON mas.IncrementalStepId = ISTP.SalaryStepId
 Left Join dbo.tblIncrementInfoMaster IIM On IIM.IncrementInfoMasterId = mas.IncrementTypeId 
 Left JOIN dbo.tblUser On tblUser.UserId = mas.ModifyBy
 WHERE mas.AuditTrail_IncrementId = 
 (SELECT max(AuditTrail_IncrementId) FROM tblIncrement_AuditTrail t2 WHERE t2.IncrementId = mas.IncrementId) " +parameter2+" ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable TransferLoadDal(string parameter2)
        {

            string query = @"Select mas.AuditTrail_EmpTransferAndRedesignationId, mas.StatusMode,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger'   ELSE  'btn btn-sm btn-warning' end  StatusStyle ,mas.ModifyDate, mas.EffectiveDate, mas.EmpTransferAndRedesignationId,
Com.ShortName, Emp.EmpName, Emp.EmpMasterCode, fy.FinancialYearDesc,
NCom.ShortName as NCompany, NOffice.SalaryLocation, Nplace.Location, Ndivi.DivisionName, Nwing.DivisionWingName, NDpt.DepartmentName, Nsec.SectionName,
NsubSec.SubSectionName, nRptbody.EmpName as NReportingBody,Us.UserName from tblEmpTransferAndRedesignation_AuditTrail mas with(Nolock)
left JOIN dbo.tblEmpGeneralInfo  Emp ON mas.EmployeeId = Emp.EmpInfoId
LEFT JOIN dbo.tblCompanyInfo  Com ON  Com.CompanyId= mas.NewCompanyId 
Left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId
LEFT JOIN dbo.tblCompanyInfo  NCom ON  NCom.CompanyId= mas.NewCompanyId 
left join tblJobLocation Nplace On Nplace.JobLocationID = mas.NewJobLocationId
left join tblSalaryLocation NOffice ON NOffice.SalaryLoationId = mas.NewSalaryLocationId
left join tblDivision Ndivi on Ndivi.DivisionId= mas.NewDivisionId
left join tblDivisionWing Nwing on Nwing.DivisionWId = mas.NewWingId
left JOIN dbo.tblDepartment  NDpt ON  NDpt.DepartmentId = mas.NewDepartmentId
left join tblSection Nsec on Nsec.SectionId = mas.NewSectionId
left join tblSubSection NsubSec On NsubSec.SubSectionId = mas.NewSubSectionId  
left JOIN dbo.tblEmpGeneralInfo  nRptbody ON  nRptbody.EmpInfoId = mas.NewEmpReportingBodyId
left join tblUser Us On Us.UserId = mas.ModifyBy
WHERE mas.AuditTrail_EmpTransferAndRedesignationId = 
(SELECT max(AuditTrail_EmpTransferAndRedesignationId) FROM tblEmpTransferAndRedesignation_AuditTrail t2 WHERE t2.EmpTransferAndRedesignationId = mas.EmpTransferAndRedesignationId) " + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetMpBudgetLoadDal(string parameter2)
        {

            string query = @"select mas.AuditTrail_MPBudgetMasterId, mas.MPBudgetMasterId,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger'   ELSE  'btn btn-sm btn-warning' end  StatusStyle,
mas.StatusMode, mas.ModifyDate, us.UserName, fy.FinancialYearDesc,com.ShortName, dept.DepartmentName from tblMPBudgetMaster_AuditTrail mas
left  join tblUser us on us.UserId = mas.ModifyBy
left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId
left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
left join tblDepartment dept on dept.DepartmentId = mas.DepartmentId
WHERE mas.AuditTrail_MPBudgetMasterId = 
(SELECT max(AuditTrail_MPBudgetMasterId) FROM tblMPBudgetMaster_AuditTrail t2 WHERE t2.MPBudgetMasterId = mas.MPBudgetMasterId)" + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable SeparationLoadDal(string parameter2)
        {

            string query = @"  Select mas.AuditTrail_EmployeeJobLeftId,mas.EmployeeJobLeftId,mas.StatusMode,CASE WHEN mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end StatusStyle,
  mas.ModifyDate, mas.Reason, 
  mas.JobLeftDate, mas.SubmissionDate, emp.EmpName,JT.JobLeftType, emp.EmpMasterCode,com.ShortName, us.UserName from tblEmployeeJobLeft_AuditTrail mas
  left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
  left join tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmployeeId
  left join tblUser us on us.UserId = mas.ModifyBy
  left join tblJobLeftType JT on JT.JobLeftTypeId = mas.JobLeftTypeId
  WHERE mas.AuditTrail_EmployeeJobLeftId = 
  (SELECT max(AuditTrail_EmployeeJobLeftId) FROM tblEmployeeJobLeft_AuditTrail t2 WHERE t2.EmployeeJobLeftId = mas.EmployeeJobLeftId)  " + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable JobCirculationLoadDal(string parameter2)
        {

            string query = @" Select mas.AuditTrail_JobID,mas.JobID, mas.StatusMode,CASE WHEN mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end StatusStyle,
 mas.ModifyDate, mas.JobContext, mas.CirculationStartDate, mas.CirculationsdeadlineDate, com.ShortName, us.UserName,
mas.Remarks, mas.ProbableInterviewDate, mas.IsSalary, (ReqCode+ ' ( '+ CONVERT(NVARCHAR(12), ReqDate) +' ) '+' : ' +JobTitle)ReqCode from tblJobCreation_AuditTrail mas 
left join tblUser us on us.UserId = mas.ModifyBy
left join tblCompanyInfo Com On Com.CompanyId = mas.CompanyId 
left join tblJobReqForm JRF on JRF.JobReqId = mas.ReqCodeId " + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }






        public DataTable PromotionLoadDal(string parameter)
        {
            string query = @"Select  mas.AuditTrail_EmployeePromotionEntryId,mas.StatusMode, mas.EmployeePromotionEntryId,
CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE  'btn btn-sm btn-warning' end  StatusStyle,
Com.ShortName, Fy.FinancialYearDesc, mas.Effectivedate, emp.EmpName, emp.EmpMasterCode, Ndeg.Designation,
SS.SalaryStepName, ISNULL(SG.GradeCode+ ' : ','')+ ISNULL(SG.GradeName,'') GradeName, Nrepo.EmpName as newRepotingBody, mas.IsReappointment,mas.Remarks,
mas.ModifyDate, tblUser.UserName  from tblEmployeePromotionEntry_AuditTrail mas 
left JOIN dbo.tblCompanyInfo  Com ON com.CompanyId=mas.CompanyId
left join dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmployeeId
left JOIN dbo.tblDesignation  Ndeg ON Ndeg.DesignationId=mas.NDesignationId
left JOIN dbo.tblSalaryGrade  SG ON SG.SalaryGradeId=mas.NSalGradeId
left join dbo.tblSalaryStep SS On SS.SalaryStepId = mas.NSalaryStepId
left join dbo.tblPromotionType Pt On Pt.PromotionTypeId = mas.NPromoTypeId
LEFT JOIN dbo.tblEmpGeneralInfo Nrepo ON Nrepo.EmpInfoId = mas.NRepEmpId
left join dbo.tblUser On tblUser.UserId = mas.ModifyBy 
 WHERE mas.AuditTrail_EmployeePromotionEntryId = 
 (SELECT max(AuditTrail_EmployeePromotionEntryId) FROM tblEmployeePromotionEntry_AuditTrail t2 WHERE t2.EmployeePromotionEntryId = mas.EmployeePromotionEntryId) " + parameter + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable SuspendLoadDal(string parameter)
        {

            string query = @"Select mas.StatusMode,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger'   ELSE  'btn btn-sm btn-warning' end  StatusStyle,
mas.AuditTrail_SuspendId, mas.SuspendId, mas.Effectivedate, mas.Description, mas.ModifyDate, mas.EffectiveToDate, 
tblUser.UserName, Com.ShortName, Fy.FinancialYearDesc, emp.EmpName, emp.EmpMasterCode, mas.Remarks from tblSuspend_AuditTrail mas
left JOIN dbo.tblCompanyInfo  Com ON com.CompanyId=mas.CompanyInfoId
left join dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId
left join dbo.tblUser On tblUser.UserId = mas.ModifyBy 
WHERE mas.AuditTrail_SuspendId = 
(SELECT max(AuditTrail_SuspendId) FROM tblSuspend_AuditTrail t2 WHERE t2.SuspendId = mas.SuspendId)  " + parameter + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable ContractualEmployeeLoadDal(string parameter)
        {
            string query = @"Select emp.EmpName,CASE WHEN  mas.StatusMode='DELETE' THEN 'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end StatusStyle, emp.EmpMasterCode, com.ShortName, us.UserName, mas.StatusMode, mas.ModifyDate, mas.AuditTrail_ContractualEmpManageId, mas.ContractualEmpManageId,
mas.IsExtension,mas.IsRenew,mas.IsPermanentToContractual,mas.IsContractualToPermanent,mas.IsSalaryIncrement,mas.IsNoIncrement,mas.IsFacilityIncluded,mas.IsNoFacility,
mas.Remarks,mas.ExtensionFromDate,mas.ExtensionToDate,mas.RenewStartDate,mas.RenewToDate,mas.PermanentToContractualEffectiveDate,mas.ContractualToPermanentDate,mas.EffectiveDate,
mas.ContractEndDate,mas.PermanentToContractualEndDate, mas.ContractPreiod, mas.IsSMCFundedProjectstoSMCContract, mas.IsSMCContracttoSMCFundedProjects
from tblContractualEmpManage_AuditTrail mas
Left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
left join tblEmpGeneralInfo emp on emp.EmpInfoId = mas.EmployeeId
left join tblUser us on us.UserId = mas.ModifyBy
WHERE mas.AuditTrail_ContractualEmpManageId = 
(SELECT max(AuditTrail_ContractualEmpManageId) FROM tblContractualEmpManage_AuditTrail t2 WHERE t2.ContractualEmpManageId = mas.ContractualEmpManageId) "+parameter+" ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable KPIMasterLoadDal()
        {
            string query = @"
Select Emp.EmpName,CASE WHEN  mas.StatusMode='DELETE' THEN 'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end StatusStyle,
Emp.EmpMasterCode,  mas.StatusMode, mas.ModifyDate, mas.AuditTrail_AppraisalSelfMasterId, Us.UserName, dg.Designation, dept.DepartmentName,
mas.AppraisalSelfMasterId from tblAppraisalSelfMaster_AuditTrail mas 
left join tblEmpGeneralInfo Emp on Emp.EmpInfoId = mas.EmpInfoId 
inner join tblDesignation Dg On dg.DesignationId = Emp.DesignationId
inner join tblDepartment dept on dept.DepartmentId = Emp.DepartmentId
left join tblUser Us on Us.UserId = mas.ModifyBy
WHERE mas.AuditTrail_AppraisalSelfMasterId = 
(SELECT max(AuditTrail_AppraisalSelfMasterId) FROM tblAppraisalSelfMaster_AuditTrail t2 WHERE t2.AppraisalSelfMasterId = mas.AppraisalSelfMasterId)";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }



        public DataTable DiciplinaryActionLoadDal(string parameter)
        {

            string query = @"Select mas.StatusMode,mas.AuditTrail_DiciplinaryId,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger'   ELSE  'btn btn-sm btn-warning' end  StatusStyle, mas.DiciplinaryId, mas.Effectivedate, mas.Description, mas.ModifyDate, 
tblUser.UserName, Com.ShortName, Fy.FinancialYearDesc, emp.EmpName, emp.EmpMasterCode, mas.Remarks from tblDiciplinaryAction_AuditTrail mas
left JOIN dbo.tblCompanyInfo  Com ON com.CompanyId=mas.CompanyInfoId
left join dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId
left join dbo.tblUser On tblUser.UserId = mas.ModifyBy
WHERE mas.AuditTrail_DiciplinaryId = (SELECT max(AuditTrail_DiciplinaryId) FROM tblDiciplinaryAction_AuditTrail t2 WHERE t2.DiciplinaryId = mas.DiciplinaryId) " + parameter + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable JDLoadDal()
        {

            string query = @"Select mas.AuditTrail_JdMasterId,emp.EmpName,emp.EmpMasterCode,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end  StatusStyle,
mas.JdMasterId, mas.JdSummary, mas.StatusMode, 
us.UserName, mas.ModifyDate from tblJdMaster_AuditTrail mas
Left join tblUser us on us.UserId = mas.ModifyBy
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId
WHERE mas.AuditTrail_JdMasterId = 
(SELECT max(AuditTrail_JdMasterId) FROM tblJdMaster_AuditTrail t2 WHERE t2.JdMasterId = mas.JdMasterId) ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable MPBudgetLoadDal(string parameter)
        {

            string query = @"Select mas.StatusMode,mas.AuditTrail_DiciplinaryId,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger'   ELSE  'btn btn-sm btn-warning' end  StatusStyle, mas.DiciplinaryId, mas.Effectivedate, mas.Description, mas.ModifyDate, 
tblUser.UserName, Com.ShortName, Fy.FinancialYearDesc, emp.EmpName, emp.EmpMasterCode, mas.Remarks from tblDiciplinaryAction_AuditTrail mas
left JOIN dbo.tblCompanyInfo  Com ON com.CompanyId=mas.CompanyInfoId
left join dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId
left join dbo.tblUser On tblUser.UserId = mas.ModifyBy
WHERE mas.AuditTrail_DiciplinaryId = (SELECT max(AuditTrail_DiciplinaryId) FROM tblDiciplinaryAction_AuditTrail t2 WHERE t2.DiciplinaryId = mas.DiciplinaryId) " + parameter + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable SeparationLoadDal(string parameter2, string DeletePara)
        {
            string query = @" SELECT 'btn btn-sm btn-warning' StatusStyle,'Edit' Status,Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.ShortName, JobLeftDate, SubmissionDate ,JType.JobLeftType,
						   UserR.UserName AS DeleteBy , EPE.UpdateDate as DeleteDate From tblEmployeeJobLeft EPE
                           INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                           INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                           INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
					       LEFT  JOIN dbo.tblUser  UserR ON EPE.UpdateBy = UserR.UserId   
						   where ((EPE.UpdateBy IS NOT NUll) and (epe.IsDelete=0 or epe.IsDelete is Null)) " + parameter2 + " " + " " +
                           @"Union all						 
                              SELECT 'btn btn-sm btn-danger' StatusStyle,'Delete' Status,Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.ShortName, JobLeftDate, SubmissionDate ,JType.JobLeftType,
						   UserR.UserName AS DeleteBy , EPE.DeleteDate From tblEmployeeJobLeft EPE
                           INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                           INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                           INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
					       LEFT  JOIN dbo.tblUser  UserR ON EPE.DeleteBy = UserR.UserId 
						   where (epe.IsDelete=1 )   " + DeletePara + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable ContactualLoadDal(string parameter2, string DeletePara)
        {
            string query = @" SELECT 'btn btn-sm btn-warning' StatusStyle,'Edit' Status,Emp.EmpName, Emp.EmpMasterCode, Com.ShortName, Ds.Designation,
 Dpt.DepartmentName, EffectiveDate,  us.UserName DeleteBy, EPE.UpdateDate as DeleteDate  From tblContractualEmpManage EPE
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 left JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 left JOIN dbo.tblDesignation  Ds ON Emp.DesignationId = Ds.DesignationId
 left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
 left JOIN dbo.tblUser  us ON EPE.UpdateBy = us.UserId 
 where ((EPE.UpdateBy IS NOT NUll) and (epe.IsDelete=0 or epe.IsDelete is Null)) " + parameter2 + " " + " " +
                           @"Union all						 
                              SELECT 'btn btn-sm btn-danger' StatusStyle,'Delete' Status,Emp.EmpName, Emp.EmpMasterCode, Com.ShortName, Ds.Designation,
 Dpt.DepartmentName, EffectiveDate,  us.UserName DeleteBy, EPE.DeleteDate   From tblContractualEmpManage EPE
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 left JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 left JOIN dbo.tblDesignation  Ds ON Emp.DesignationId = Ds.DesignationId
 left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
 left JOIN dbo.tblUser  us ON EPE.DeleteBy = us.UserId
 where (epe.IsDelete=1)  " + DeletePara + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable DiciplinaryActionLoadDal(string parameter2, string DeletePara)
        {
            string query = @" 
 SELECT 'btn btn-sm btn-warning' StatusStyle,'Edit' Status,SPNDR.SuspendReasonEntry, Dg.Designation, dpt.DepartmentName, 
 EGI.EmpMasterCode ,EGI.EmpName,DCP.EffectiveDate, us.UserName as DeleteBy ,DCp.UpdateDate as DeleteDate
 FROM tblDiciplinaryAction AS DCP 
 LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON DCP.EmpInfoId = EGI.EmpInfoId
 LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
 LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
 LEFT JOIN dbo.tblUser AS us ON us.UserId = DCP.UpdateBy
 LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON DCP.ReasonId = SPNDR.SuspendReasonEntryId
 where (DCP.UpdateBy IS NOT NUll) " + parameter2 + " " + " " +
                           @"Union all						 
                             SELECT 'btn btn-sm btn-danger' StatusStyle,'Delete' Status ,SPNDR.SuspendReasonEntry, Dg.Designation, dpt.DepartmentName, 
 EGI.EmpMasterCode ,EGI.EmpName,DCP.EffectiveDate, us.UserName as  DeleteBy, DCP.EntryDate as DeleteDate
 FROM DELtblDiciplinaryAction AS DCP 
 LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON DCP.EmpInfoId = EGI.EmpInfoId
 LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
 LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
 LEFT JOIN dbo.tblUser AS us ON us.UserId = DCP.EntryBy
 LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON DCP.ReasonId = SPNDR.SuspendReasonEntryId  
 where (DCP.EntryBy IS NOT NUll) " + DeletePara + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable SuspendLoadDal(string parameter2, string DeletePara)
        {
            string query = @" 
 SELECT 'btn btn-sm btn-warning' StatusStyle,'Edit' Status,Dg.Designation, dpt.DepartmentName, EGI.EmpMasterCode,
EGI.EmpName,SPND.EffectiveDate,SPNDR.SuspendReasonEntry, us.UserName as DeleteBy, SPND.UpdateDate as DeleteDate FROM dbo.tblSuspend AS SPND 
LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
LEFT JOIN dbo.tblUser AS us ON us.UserId = SPND.UpdateBy
LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON SPND.ReasonId =  SPNDR.SuspendReasonEntryId 
 where (SPND.UpdateBy IS NOT NUll)  " + parameter2 + " " + " " +
                           @"Union all						 
                           SELECT 'btn btn-sm btn-danger' StatusStyle,'Delete' Status,Dg.Designation, dpt.DepartmentName, EGI.EmpMasterCode,
EGI.EmpName,SPND.EffectiveDate,SPNDR.SuspendReasonEntry, us.UserName as DeleteBy, SPND.EntryDate as DeleteDate  FROM dbo.DELtblSuspend AS SPND 
LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
LEFT JOIN dbo.tblUser AS us ON us.UserId = SPND.EntryBy
LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON SPND.ReasonId =  SPNDR.SuspendReasonEntryId 
 where (SPND.EntryBy IS NOT NUll) 
 " + DeletePara + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        //for sap report
        public DataTable GetIncrementAuditTrailByMasterId(string ID)
        {
            try
            {
                string query = @"  Select mas.AuditTrail_IncrementId,mas.StatusMode, com.ShortName, Fy.FinancialYearDesc, EffectiveDate, Emp.EmpMasterCode, Emp.EmpName,
 ISTP.SalaryStepName as IncremantalStep, IIM.Name as IncrementType, StatusMode, tblUser.UserName, ModifyDate from tblIncrement_AuditTrail mas
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
 LEFT JOIN dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
 LEFT JOIN dbo.tblEmpGeneralInfo Emp ON Emp.EmpInfoId= mas.EmployeeId
 LEFT JOIN dbo.tblSalaryStep AS ISTP ON mas.IncrementalStepId = ISTP.SalaryStepId
 Left Join dbo.tblIncrementInfoMaster IIM On IIM.IncrementInfoMasterId = mas.IncrementTypeId 
 Left JOIN dbo.tblUser On tblUser.UserId = mas.ModifyBy
 WHERE  mas.IncrementId=" + ID ;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GettransferAuditforCrp(string ID)
        {
            string query = @"
Select mas.AuditTrail_EmpTransferAndRedesignationId, mas.StatusMode,mas.IsOnlyTransfer,mas.IsInterCompanyTransfer,mas.ModifyDate, mas.EffectiveDate, mas.EmpTransferAndRedesignationId,
Com.ShortName, Emp.EmpName, Emp.EmpMasterCode, fy.FinancialYearDesc,
NCom.ShortName as NCompany, NOffice.SalaryLocation, Nplace.Location, Ndivi.DivisionName, Nwing.DivisionWingName, NDpt.DepartmentName, Nsec.SectionName,
NsubSec.SubSectionName, nRptbody.EmpName as NReportingBody,Us.UserName from tblEmpTransferAndRedesignation_AuditTrail mas with(Nolock)
left JOIN dbo.tblEmpGeneralInfo  Emp ON mas.EmployeeId = Emp.EmpInfoId
LEFT JOIN dbo.tblCompanyInfo  Com ON  Com.CompanyId= mas.NewCompanyId 
Left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId
LEFT JOIN dbo.tblCompanyInfo  NCom ON  NCom.CompanyId= mas.NewCompanyId 
left join tblJobLocation Nplace On Nplace.JobLocationID = mas.NewJobLocationId
left join tblSalaryLocation NOffice ON NOffice.SalaryLoationId = mas.NewSalaryLocationId
left join tblDivision Ndivi on Ndivi.DivisionId= mas.NewDivisionId
left join tblDivisionWing Nwing on Nwing.DivisionWId = mas.NewWingId
left JOIN dbo.tblDepartment  NDpt ON  NDpt.DepartmentId = mas.NewDepartmentId
left join tblSection Nsec on Nsec.SectionId = mas.NewSectionId
left join tblSubSection NsubSec On NsubSec.SubSectionId = mas.NewSubSectionId  
left JOIN dbo.tblEmpGeneralInfo  nRptbody ON  nRptbody.EmpInfoId = mas.NewEmpReportingBodyId
left join tblUser Us On Us.UserId = mas.ModifyBy
 WHERE mas.AuditTrail_EmpTransferAndRedesignationId is not null and mas.EmpTransferAndRedesignationId=" + ID;

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetSeparationAuditforCrp(string ID)
        {
            string query = @"Select mas.AuditTrail_EmployeeJobLeftId,mas.EmployeeJobLeftId,mas.StatusMode,
  mas.ModifyDate, mas.Reason, 
  mas.JobLeftDate, mas.SubmissionDate, emp.EmpName,JT.JobLeftType, emp.EmpMasterCode,com.ShortName, us.UserName from tblEmployeeJobLeft_AuditTrail mas
  left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
  left join tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmployeeId
  left join tblUser us on us.UserId = mas.ModifyBy
  left join tblJobLeftType JT on JT.JobLeftTypeId = mas.JobLeftTypeId
  WHERE mas.AuditTrail_EmployeeJobLeftId is not null and mas.EmployeeJobLeftId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetKPIMasterAuditforCrp(string ID)
        {
            string query = @"Select Emp.EmpName,CASE WHEN  mas.StatusMode='DELETE' THEN 'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end StatusStyle,
Emp.EmpMasterCode,  mas.StatusMode, mas.ModifyDate, mas.AuditTrail_AppraisalSelfMasterId, Us.UserName, dg.Designation, dept.DepartmentName,
mas.AppraisalSelfMasterId from tblAppraisalSelfMaster_AuditTrail mas 
left join tblEmpGeneralInfo Emp on Emp.EmpInfoId = mas.EmpInfoId 
inner join tblDesignation Dg On dg.DesignationId = Emp.DesignationId
inner join tblDepartment dept on dept.DepartmentId = Emp.DepartmentId
left join tblUser Us on Us.UserId = mas.ModifyBy
WHERE mas.AuditTrail_AppraisalSelfMasterId is Not NUll and mas.AppraisalSelfMasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }



        public DataTable GetKPIFuntionalAuditforCrp(string ID)
        {
            string query = @"SELECT mas.AuditTrail_AppraisalSelfMasterId, mas.AppraisalSelfFucAreaId, mas.AppraisalSelfMasterId,
mas.AuditTrail_AppraisalSelfFucAreaId, mas.KpiInfo, mas.KpiWeight,
mas.KpiWeightPer, mas.Target, mas.TargetPer, mas.Deadline,
mas.IsActive from tblAppraisalSelfFuncArea_AuditTrail mas 
where mas.AppraisalSelfMasterId= " + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetKPIBehavioralAuditforCrp(string ID)
        {
            string query = @"
SELECT mas.AuditTrail_AppraisalSelfMasterId, mas.AuditTrail_AppraisalSelfBehaveId, mas.AppraisalSelfBehaveId, mas.AuditTrail_AppraisalSelfBehaveId, mas.SkillInfo,
mas.SupportingEmp, mas.Score, mas.SetScore from tblAppraisalSelfBehaveArea_AuditTrail mas
where mas.AppraisalSelfMasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEmployeeProbationAuditforCrp(string ID)
        {
            string query = @" Select mas.AuditTrail_ProbationEvaluationMasterId,mas.ProbationEvaluationMasterId ,mas.StatusMode,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end  StatusStyle ,us.UserName, mas.ModifyDate, mas.ExProbation, mas.ExProDate, mas.ProbationEnd,
					  mas.ProbationEndReason,mas.ConfirmDate,com.ShortName ,mas.ActionStatus,Emp.EmpMasterCode, Emp.EmpName,Emp.DateOfJoin,Emp.ProbationEndDate ,DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.Designation, mas.SeparationDate from tblProbationEvaluationMaster_AuditTrail mas
     LEFT JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = mas.EmpInfoId
	 LEFT JOIN tblCompanyInfo com on Emp.CompanyId = com.CompanyId
	       LEFT JOIN dbo.tblDivision AS DSN ON Emp.DivisionId = DSN.DivisionId
		               LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
					            LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId
								left join tbluser us On us.UserId = mas.ModifyBy
								WHERE mas.ProbationEvaluationMasterId = " + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetMpBudgetAuditforCrp(string ID)
        {
            string query = @"
select mas.AuditTrail_MPBudgetMasterId, mas.MPBudgetMasterId,
mas.StatusMode, mas.ModifyDate, us.UserName, fy.FinancialYearDesc,com.ShortName, dept.DepartmentName from tblMPBudgetMaster_AuditTrail mas
left  join tblUser us on us.UserId = mas.ModifyBy
left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId
left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
left join tblDepartment dept on dept.DepartmentId = mas.DepartmentId
WHERE     mas.MPBudgetMasterId =" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetMpBudgetDetailsAuditforCrp(string ID)
        {
            string query = @"SELECT mas.AuditTrail_MPBudgetMasterId, mas.Designation,mas.EmployeeRequisition,mas.DtlRemarks, mas.ReqApproxSalary, mas.ReqTotalSalary, QA.QuarterName, EC.EmpCategoryName
,SG.GradeName, ET.EmpType from tblMPBudgetDetails_AuditTrail mas  
left join tblQuarterInfo QA On QA.QuarterId = mas.QuarterId
left join tblEmpCategory EC On EC.EmpCategoryId = mas.EmpCategoryId
left join tblSalaryGrade SG On SG.SalaryGradeId = mas.SalaryGradeId
left join tblEmployeeType ET ON ET.EmpTypeId = mas.EmployeeTypeId
Where mas.AuditTrail_MPBudgetDetailsId is not null and mas.MPBudgetMasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetJDAuditforCrp(string ID)
        {
            string query = @"Select mas.AuditTrail_JdMasterId,emp.EmpName,emp.EmpMasterCode,
mas.JdMasterId, mas.JdSummary, mas.StatusMode, 
us.UserName, mas.ModifyDate from tblJdMaster_AuditTrail mas
Left join tblUser us on us.UserId = mas.ModifyBy
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId
WHERE mas.AuditTrail_JdMasterId is not Null and mas.JdMasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetJobCirculationAuditforCrp(string ID)
        {
            string query = @"Select mas.AuditTrail_JobID,mas.JobID, mas.StatusMode,
mas.ModifyDate, mas.JobContext, mas.CirculationStartDate, mas.CirculationsdeadlineDate, com.ShortName, us.UserName,
mas.Remarks, mas.ProbableInterviewDate, mas.IsSalary, (ReqCode+ ' ( '+ CONVERT(NVARCHAR(12), ReqDate) +' ) '+' : ' +JobTitle)ReqCode from tblJobCreation_AuditTrail mas 
left join tblUser us on us.UserId = mas.ModifyBy
left join tblCompanyInfo Com On Com.CompanyId = mas.CompanyId 
left join tblJobReqForm JRF on JRF.JobReqId = mas.ReqCodeId
where  mas.JobID = 
" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetJobCirculationDetailsAuditforCrp(string ID)
        {
            string query = @"SELECT JCCD.AuditTrail_JobID, JCCD.WayId, cc.CirculationWay FROM tblJCCirculationWayDetail_AuditTrail  AS JCCD 
left join tblVacancyCirculation cc on cc.VacancyCirculationId = JCCD.WayId
WHERE JCCD.MasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetJdDetailsAuditforCrp(string ID)
        {
            string query = @"
Select * from tblJdDetails_AuditTrail mas
Where mas.JdMasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        //Employee Autid trail dal
        public DataTable GetEmployeeInformationAuditforCrp(string ID)
        {
            string query = @"
SELECT  Emp.AuidtTrail_EmpInfoId, Emp.ModifyDate,ISNULL(us.UserName,'')+ ISNULL( +' : '+ useEmp.EmpName,'') UserName, Emp.StatusMode,Emp.SpouseName,SpouseOccup.Description SpouseOccupation,SpusMax.Description SpouseMaxEducation, PreThana.Title PresentThana, PerThana.Title PermanentThana,PerDis.Titel PermanentDistrict,
 PreDis.Titel PresentDistrict,  PresDiv.Title PresentDivision, PerDiv.Title ParmanentDivision, tblOccupation.Description FatherOccupation, motherOcc.Description MotherOccupation,com.ShortName,
 Div.DivisionName,DivW.DivisionWingName, Sec.SectionName, dept.DepartmentName,
SuSec.SubSectionName, Cat.EmpCategoryName, Sgrd.GradeName, SStep.SalaryStepName, Desg.Designation,
DType.DesigTypeName, SLoc.SalaryLocation, JLOC.Location, EType.EmpType, Pro.ProjectName, eGen.EmpName ReportingEmp,
 * from tblEmpGeneralInfo_AuidtTrail Emp
left JOIN dbo.tblCompanyInfo com ON com.CompanyId = Emp.CompanyId
left JOIN dbo.tblDivision Div ON Div.DivisionId = Emp.DivisionId
left JOIN dbo.tblDivisionWing DivW ON DivW.DivisionWId = Emp.DivisionWId
LEFT JOIN dbo.tblDepartment dept ON dept.DepartmentId = Emp.DepartmentId
LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = Emp.SectionId
LEFT JOIN dbo.tblSubSection SuSec ON SuSec.SubSectionId = Emp.SubSectionId
LEFT JOIN dbo.tblEmpCategory Cat ON Cat.EmpCategoryId = Emp.EmpCategoryId
LEFT JOIN dbo.tblSalaryGrade Sgrd ON Sgrd.SalaryGradeId = Emp.SalaryGradeId
LEFT JOIN dbo.tblSalaryStep SStep ON SStep.SalaryStepId = Emp.SalaryStepId
LEFT JOIN dbo.tblDesignation Desg ON Desg.DesignationId = Emp.DesignationId
LEFT JOIN dbo.tblDesignationType DType ON DType.DesignationTypeId = Emp.DesignationTypeId
LEFT JOIN dbo.tblSalaryLocation SLoc ON SLoc.SalaryLoationId = Emp.SalaryLoationId
LEFT JOIN dbo.tblJobLocation JLOC ON JLOC.JobLocationID = Emp.JobLocationId
LEFT JOIN dbo.tblEmployeeType EType ON EType.EmpTypeId = Emp.EmpTypeId
LEFT JOIN dbo.tblProjectSetup Pro ON Pro.ProjectId = Emp.ProjectID
LEFT JOIN dbo.tblEmpGeneralInfo eGen ON eGen.EmpInfoId=Emp.ReportingEmpId
LEFT JOIN tblOccupation ON Emp.FatherOccupation=tblOccupation.OccupationID
LEFT JOIN tblOccupation motherOcc ON Emp.MotherOccupation=motherOcc.OccupationID
LEFT JOIN tblAddressDivision PresDiv ON Emp.PresentDivision= PresDiv.AddressDivisionID
LEFT JOIN tblAddressDivision PerDiv ON Emp.ParmanentDivision= PerDiv.AddressDivisionID
LEFT JOIN tblDistrict PreDis ON  Emp.PresentDistrict= PreDis.DistrictID
LEFT JOIN tblDistrict PerDis ON  Emp.PermanentDistrict= PerDis.DistrictID
LEFT JOIN tblThana PreThana ON Emp.PresentThana= PreThana.ThanaID
LEFT JOIN tblThana PerThana ON Emp.PermanentThana= PerThana.ThanaID
LEFT JOIN tblEducationName SpusMax ON Emp.SpouseMaxEducation= SpusMax.EducationNameID
LEFT JOIN tblOccupation SpouseOccup ON  Emp.SpouseOccupation= SpouseOccup.OccupationID
LEFT JOIN tblUser us on us.UserId= Emp.ModifyBy
LEFT JOIN dbo.tblEmpGeneralInfo useEmp  on us.EmpInfoId= useEmp.EmpInfoId
WHERE Emp.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEmployeeChildrenAuditforCrp(string ID)
        {
            string query = @"SELECT  tblOccupation.Description AS ChildrenOccupationName,* from tblEmpChildren_AuditTrail 
LEFT JOIN dbo.tblOccupation ON dbo.tblEmpChildren_AuditTrail.ChildrenOccupation=dbo.tblOccupation.OccupationID 
WHERE tblEmpChildren_AuditTrail.IsActive=1 and tblEmpChildren_AuditTrail.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEmployeeEducationAuditforCrp(string ID)
        {
            string query = @"SELECT e.AuditTrail_EmpEducationId,e.AuidtTrail_EmpInfoId,e.EmpEducationId,
       e.EmpInfoId,
       e.EducationNameId,
	   en.Description AS EducationName,
       e.BoardUniversityId,
	   bu.Description AS BoardUniversity,
       e.SubjectGroupId,
	   sg.Description AS SubjectGroup,
       e.EducationalInstituteId,
	   ei.Description AS EducationalInstitute,
       e.FieldOfSpecializationId,
	  sp.Description AS FieldOfSpecialization,
       e.PassingYear,
       e.Result,
       e.CgpaOrTotalMarks,
       e.EduIsLastLevel,
e.IsProfessionalEdu,
       e.IsActive FROM dbo.tblEmpEducation_AuditTrail e 
	   LEFT JOIN dbo.tblEducationName en ON en.EducationNameID = e.EducationNameId
	   LEFT JOIN dbo.tblBoardUniversity bu ON bu.BoardUniversityID = e.BoardUniversityId
	   LEFT JOIN dbo.tblEducationSubjectGroup sg ON sg.EducationSubjectGroupID=e.SubjectGroupId
	   LEFT JOIN dbo.tblEducationalInstitution ei ON ei.InstitutionID=e.EducationalInstituteId
	   LEFT JOIN dbo.tblSpecialization sp ON sp.SpecializationID=e.FieldOfSpecializationId
	   WHERE e.IsActive=1 AND e.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEmployeExperinceAuditforCrp(string ID)
        {
            string query = @"SELECT * from tblEmpExperience_AuditTrail WHERE tblEmpExperience_AuditTrail.IsActive=1 and tblEmpExperience_AuditTrail.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetEmployeeTrainingAuditforCrp(string ID)
        {
            string query = @"SELECT tblEmpTraining_AuditTrail.TrainingName,  tblEmpInfoTrainingType.Description TrainingTypeName, dbo.tblEmpTraining_AuditTrail.TrainingDescription, tblEmpTrainingInstitution.Description TrainingInstitutionName, tblCountry.Title TrainingCountryName,
 tblEmpTraining_AuditTrail.TrFromDate, tblEmpTraining_AuditTrail.TrToDate ,dbo.tblEmpTraining_AuditTrail.TrainingAchievment, dbo.tblEmpTraining_AuditTrail.TrainingDays, tblEmpTraining_AuditTrail.TrainingPlace, tblEmpTraining_AuditTrail.TrRemarks from tblEmpTraining_AuditTrail
LEFT JOIN tblEmpInfoTrainingType ON tblEmpTraining_AuditTrail.TrainingType=tblEmpInfoTrainingType.TrainingTypeID
LEFT JOIN tblEmpTrainingInstitution ON  tblEmpTraining_AuditTrail.TrainingInstitution=tblEmpTrainingInstitution.InstitutionID
LEFT JOIN dbo.tblCountry ON  tblEmpTraining_AuditTrail.TrainingCountry=tblCountry.CountryID WHERE tblEmpTraining_AuditTrail.IsActive=1 and EmpInfoId=  " + ID + " " + " " +
@"UNION ALL 
SELECT mas.TrainingTitle TrainingName, tt.TrainingType TrainingTypeName,  mas.TrainingDetails TrainingDescription,ti.TrainingOrgName  TrainingInstitutionName, ' ' TrainingCountryName  
   
, FORMAT(mas.StartDate,'dd-MMM-yyyy') TrFromDate , FORMAT(mas.EndDate,'dd-MMM-yyyy') TrToDate, ' ' TrainingAchievment, mas.NoOfDays TrainingDays,
CASE
    WHEN TrainingOrgLocation = 0 THEN vn.VenueName
    WHEN TrainingOrgLocation != 0 THEN brn.BranchDetails
    
END AS TrainingPlace, '' TrRemarks FROM dbo.tblTrainingRecordMaster mas
LEFT JOIN  tbl_trainingRecordDetailsEmployee ddlt  ON mas.TrainingRecordMasterId = ddlt.TrainingRecordMasterId
   LEFT JOIN dbo.tblTrainingType tt ON tt.TrainingTypeID = mas.TrainingTypeId
  LEFT JOIN dbo.tblTrainingOrgInfo ti ON ti.TrainingOrgId=mas.TrainingOrgId
  LEFT JOIN tblSMCTrainingVenue vn ON mas.TrainingVenue= vn.SMCVenueID
  LEFT JOIN tblOfficeBranch brn ON brn.TrainingOrgId = mas.TrainingOrgId
 WHERE   ddlt.EmpinfoId    IN (SELECT tblEvaluateTrainingMaster.EmpInfoId FROM tblEvaluateTrainingMaster )  AND ddlt.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEmployeeReferanceAuditforCrp(string ID)
        {
            string query = @"SELECT dbo.tblOccupation.Description RefOccupationName,* from tblEmpReference_AuditTrail
            LEFT JOIN tblOccupation ON dbo.tblEmpReference_AuditTrail.RefOccupation=dbo.tblOccupation.OccupationID WHERE tblEmpReference_AuditTrail.IsActive=1 and tblEmpReference_AuditTrail.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEmployeeNomineeAuditforCrp(string ID)
        {
            string query = @"SELECT tblNominationPurpose.Description NominationPurposeName, tblRelation.Description NomineeRelationName,tblOccupation.Description NomineeOccupationName, * from tblEmpNominee_AuditTrail
LEFT JOIN tblNominationPurpose ON tblEmpNominee_AuditTrail.NominationPurpose=dbo.tblNominationPurpose.NPID
LEFT JOIN tblRelation ON tblEmpNominee_AuditTrail.NomineeRelation=dbo.tblRelation.RelationID
LEFT JOIN  tblOccupation ON tblEmpNominee_AuditTrail.NomineeOccupation=dbo.tblOccupation.OccupationID WHERE tblEmpNominee_AuditTrail.IsActive=1 and tblEmpNominee_AuditTrail.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetEmployeExtraCurriculamAuditforCrp(string ID)
        {
            string query = @"SELECT  tblMasterExtraCurriculam.ExtraCurriculamName,* FROM tblEmpExtraCurriculam_AuditTrail mas

LEFT JOIN dbo.tblMasterExtraCurriculam ON 
mas.MasterExtraCurriculamId= tblMasterExtraCurriculam.MasterExtraCurriculamId  WHERE mas.IsActive=1 and mas.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetDerectlySupervisorAuditforCrp(string ID)
        {
            string query = @"SELECT  AuidtTrail_EmpInfoId,EmpName FROM dbo.tblEmpGeneralInfo_AuidtTrail WHERE ReportingEmpId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

        }

        public DataTable GetOtherTalentAuditforCrp(string ID)
        {
            string query = @"SELECT tblMasterOtherTalents.OtherTalentsName, * FROM dbo.tblEmpOtherTalents_AuditTrail
            LEFT JOIN dbo.tblMasterOtherTalents ON tblMasterOtherTalents.MasterOtherTalentsId = tblEmpOtherTalents_AuditTrail.MasterOtherTalentsId 
            WHERE tblEmpOtherTalents_AuditTrail.IsActive=1 
            AND tblEmpOtherTalents_AuditTrail.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetHobbyAuditforCrp(string ID)
        {
            string query = @"SELECT  tblMasterHobby.HobbyName,* FROM tblEmpHobby_AuditTrail
LEFT JOIN dbo.tblMasterHobby ON tblMasterHobby.MasterHobbyId = tblEmpHobby_AuditTrail.MasterHobbyId 
 WHERE tblEmpHobby_AuditTrail.IsActive=1 and tblEmpHobby_AuditTrail.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }



        public DataTable GetAchievementsAuditforCrp(string ID)
        {
            string query = @"SELECT tblMasterAchievements.AchievementsName, * FROM tblEmpAchievements_AuditTrail 
LEFT JOIN dbo.tblMasterAchievements ON tblMasterAchievements.MasterAchievementsId = tblEmpAchievements_AuditTrail.MasterAchievementsId 
WHERE tblEmpAchievements_AuditTrail.IsActive=1 and tblEmpAchievements_AuditTrail.EmpInfoId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        //


        //job_requisition
        public DataTable GetJobRequisitionAuditforCrp(string ID)
        {
            string query = @"Select mas.AuditTrail_JobReqId,mas.JobReqId,us.UserName, mas.StatusMode, mas.ModifyDate,(Sg.GradeCode+ ' : ' + Sg.GradeName) as Grade,   (Mp.BudgetCode+' ['+mas.JobTitle+']') as BudgetedPosition, FINY.FinancialYearDesc, Dpt.DepartmentName,
com.ShortName, mas.IsManagementApproved, mas.IsBudgeted, mas.ReqDate, mas.Note, mas.JobTitle, Et.EmpType as EmploymentType, EC.EmpCategoryName,mas.Nos,mas.ExpDateOfJoining,
mas.MonthContractual,mas.FundContractual,PJS.ProjectName + ' : ' + PJS.ProjectDescription AS ProjectDescription,mas.ReportingTo, mas.InternalContact, mas.ExternalContact,
mas.PlaceOffice,mas.Justification, mas.JobSummery, mas.ProfCertification,mas.Experience, mas.Skills,mas.Age, mas.CmpSkill, mas.Remarks,mas.OtherExperience, mas.Others, mas.OtherCircula,mas.IsInternalCir, mas.IsOnlineCir, mas.IsSMCWeb, mas.IsNewsPaper,mas.IsHeadHuntFirm ,mas.IsOtherCircula from tblJobReqForm_AuditTrail mas
left join tblSalaryGrade Sg On Sg.SalaryGradeId = mas.GradeId
left JOIN dbo.tblCompanyInfo AS com ON com.CompanyId = mas.CompanyId
LEFT JOIN dbo.tblFinancialYear AS FINY ON mas.FinYearId = FINY.FinancialYearId
LEFT JOIN dbo.tblDepartment AS Dpt ON mas.DeptId = Dpt.DepartmentId
left join tblUser us on us.UserId = mas.ModifyBy
left join tblMPBudgetMaster Mp On Mp.MPBudgetMasterId = mas.BudgetId 
left join tblEmployeeType Et On Et.EmpTypeId = mas.EmpTypeId 
left join tblEmpCategory EC On EC.EmpCategoryId = mas.EmployeeCategoryId
Left join  tblProjectSetup PJS on PJS.ProjectId = mas.ProjectId
where mas.JobReqId =" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }




        public DataTable GetJobReqKeyResponAuditforCrp(string ID)
        {
            string query = @"Select * from tblJobReqKeyRespon_AuditTrail mas
                            where mas.JobReqFormId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEducationRequirementsDetailAuditforCrp(string ID)
        {
            string query = @"Select dg.Designation as DerectlySupervisor, mas.Nos from  tblEducationRequirementsDetail_AuditTrail mas
left join tblDesignation dg on dg.DesignationId = mas.WayId
where mas.MasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEducationRequirDesJobReqAuditforCrp(string ID)
        {
            string query = @"SELECT  ks.Description EducationRequirements , KRS.Major  FROM tblEducationRequirDesJobReq_AuditTrail KRS
 INNER JOIN dbo.tblJobReqForm_AuditTrail JRF ON KRS.JobReqFormId=JRF.JobReqId
 INNER JOIN dbo.tblEducationName ks ON ks.EducationNameID=KRS.EduRequirId
 WHERE JRF.JobReqId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetOtherRequirementDetailAuditforCrp(string ID)
        {
            string query = @"Select * from OtherRequirementDetail_AuditTrail mas
where mas.MasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetCirculationWayDetailAuditforCrp(string ID)
        {
            string query = @"SELECT JCCD.WayId, cc.CirculationWay FROM tblCirculationWayDetail  AS JCCD 
left join tblVacancyCirculation cc on cc.VacancyCirculationId = JCCD.WayId
WHERE JCCD.MasterId=" + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        //

        public DataTable GetContractualEmployeeAuditforCrp(string ID)
        {
            string query = @"Select emp.EmpName, emp.EmpMasterCode, com.ShortName, us.UserName, mas.StatusMode, mas.ModifyDate, mas.AuditTrail_ContractualEmpManageId, mas.ContractualEmpManageId,
mas.IsExtension,mas.IsRenew,mas.IsPermanentToContractual,mas.IsContractualToPermanent,mas.IsSalaryIncrement,mas.IsNoIncrement,mas.IsFacilityIncluded,mas.IsNoFacility,
mas.Remarks,mas.ExtensionFromDate,mas.ExtensionToDate,mas.RenewStartDate,mas.RenewToDate,mas.PermanentToContractualEffectiveDate,mas.ContractualToPermanentDate,mas.EffectiveDate,
mas.ContractEndDate,mas.PermanentToContractualEndDate, mas.ContractPreiod, mas.IsSMCFundedProjectstoSMCContract, mas.IsSMCContracttoSMCFundedProjects
from tblContractualEmpManage_AuditTrail mas
Left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
left join tblEmpGeneralInfo emp on emp.EmpInfoId = mas.EmployeeId
left join tblUser us on us.UserId = mas.ModifyBy
WHERE mas.AuditTrail_ContractualEmpManageId Is Not NUll And mas.ContractualEmpManageId = " + ID;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetRedesignationAuditforCrp(string ID)
        {
            string query = @"Select mas.AuditTrail_EmployeeReDesignationId, mas.StatusMode,
mas.EmployeeReDesignationId, mas.Remarks, Emp.EmpName, Emp.EmpMasterCode, newDe.Designation,
fy.FinancialYearDesc, com.ShortName, mas.Effectivedate, mas.ModifyBy, mas.ModifyDate, tblUser.UserName  from tblEmployeeReDesignation_AuditTrail mas
Left join tblCompanyInfo com on com.CompanyId = mas.CompanyId
Left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId 
Left join tblDesignation newDe On newDe.DesignationId = mas.NDesignationId
left join tblEmpGeneralInfo Emp on emp.EmpInfoId = mas.EmployeeId
Left JOIN dbo.tblUser On tblUser.UserId = mas.ModifyBy
WHERE  mas.EmployeeReDesignationId=" + ID;

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetPromotionAuditforCrp(string ID)
        {
            string query = @"Select  mas.AuditTrail_EmployeePromotionEntryId,mas.StatusMode, mas.EmployeePromotionEntryId,
Com.ShortName, Fy.FinancialYearDesc, mas.Effectivedate, emp.EmpName, emp.EmpMasterCode, Ndeg.Designation,
SS.SalaryStepName, ISNULL(SG.GradeCode+ ' : ','')+ ISNULL(SG.GradeName,'') GradeName, Nrepo.EmpName as newRepotingBody, mas.IsReappointment,mas.Remarks,
mas.ModifyDate, tblUser.UserName  from tblEmployeePromotionEntry_AuditTrail mas 
left JOIN dbo.tblCompanyInfo  Com ON com.CompanyId=mas.CompanyId
left join dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmployeeId
left JOIN dbo.tblDesignation  Ndeg ON Ndeg.DesignationId=mas.NDesignationId
left JOIN dbo.tblSalaryGrade  SG ON SG.SalaryGradeId=mas.NSalGradeId
left join dbo.tblSalaryStep SS On SS.SalaryStepId = mas.NSalaryStepId
left join dbo.tblPromotionType Pt On Pt.PromotionTypeId = mas.NPromoTypeId
LEFT JOIN dbo.tblEmpGeneralInfo Nrepo ON Nrepo.EmpInfoId = mas.NRepEmpId
left join dbo.tblUser On tblUser.UserId = mas.ModifyBy 
Where mas.AuditTrail_EmployeePromotionEntryId IS not null  and mas.EmployeePromotionEntryId=" + ID;

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetSuspendAuditforCrp(string ID)
        {
            string query = @"Select mas.StatusMode,mas.AuditTrail_SuspendId, mas.SuspendId, mas.Effectivedate, mas.Description, mas.ModifyDate, mas.EffectiveToDate, 
tblUser.UserName, Com.ShortName, Fy.FinancialYearDesc, emp.EmpName, emp.EmpMasterCode, mas.Remarks from tblSuspend_AuditTrail mas
left JOIN dbo.tblCompanyInfo  Com ON com.CompanyId=mas.CompanyInfoId
left join dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId
left join dbo.tblUser On tblUser.UserId = mas.ModifyBy 
WHERE mas.AuditTrail_SuspendId is not null and mas.SuspendId=" + ID;

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable GetDiciplinaryActionAuditforCrp(string ID)
        {
            string query = @"Select mas.StatusMode,mas.AuditTrail_DiciplinaryId, mas.DiciplinaryId, mas.Effectivedate, mas.Description, mas.ModifyDate, 
tblUser.UserName, Com.ShortName, Fy.FinancialYearDesc, emp.EmpName, emp.EmpMasterCode, mas.Remarks from tblDiciplinaryAction_AuditTrail mas
left JOIN dbo.tblCompanyInfo  Com ON com.CompanyId=mas.CompanyInfoId
left join dbo.tblFinancialYear Fy ON Fy.FinancialYearId = mas.FinancialYearId
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId
left join dbo.tblUser On tblUser.UserId = mas.ModifyBy 
WHERE mas.AuditTrail_DiciplinaryId is not null and mas.DiciplinaryId=" + ID;

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetPromotionAudi_Ds_tforCrp(string ID)
        {
            string query = @"Select emp.EmpName, emp.EmpMasterCode, mas.AuditTrail_EmployeePromotionEntryDSId, mas.AuditTrail_EmployeePromotionEntryId from tblEmployeePromotionEntryDS_AuditTrail mas
LEFT join dbo.tblEmpGeneralInfo emp On emp.EmpInfoId = mas.EmpInfoId 
where mas.EmployeePromotionEntryId=" + ID;

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable EmployeeProbationLoadDal(string parameter2)
        {

            string query = @"Select mas.AuditTrail_ProbationEvaluationMasterId,mas.ProbationEvaluationMasterId ,mas.StatusMode,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end  StatusStyle ,us.UserName, mas.ModifyDate, mas.ExProbation, mas.ExProDate, mas.ProbationEnd,
					  mas.ProbationEndReason,mas.ConfirmDate,com.ShortName ,mas.ActionStatus,Emp.EmpMasterCode, Emp.EmpName,Emp.DateOfJoin,Emp.ProbationEndDate ,DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.Designation,* from tblProbationEvaluationMaster_AuditTrail mas
     LEFT JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = mas.EmpInfoId
	 LEFT JOIN tblCompanyInfo com on Emp.CompanyId = com.CompanyId
	       LEFT JOIN dbo.tblDivision AS DSN ON Emp.DivisionId = DSN.DivisionId
		               LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
					            LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId
								left join tbluser us On us.UserId = mas.ModifyBy
								WHERE mas.AuditTrail_ProbationEvaluationMasterId = 
(SELECT max(AuditTrail_ProbationEvaluationMasterId) FROM tblProbationEvaluationMaster_AuditTrail t2 WHERE t2.ProbationEvaluationMasterId = mas.ProbationEvaluationMasterId) " + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable JobRequisitionLoadDal(string parameter2)
        {

            string query = @" SELECT mas.AuditTrail_JobReqId,mas.JobReqId, mas.ReqCode, com.ShortName,mas.JobTitle, mas.ReqDate, mas.Nos,FINY.FinancialYearDesc,
Dpt.DepartmentName, mas.ModifyDate, CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end  StatusStyle, mas.StatusMode, us.UserName
FROM dbo.tblJobReqForm_AuditTrail AS mas
INNER JOIN dbo.tblCompanyInfo AS com ON com.CompanyId = mas.CompanyId
LEFT JOIN dbo.tblFinancialYear AS FINY ON mas.FinYearId = FINY.FinancialYearId
LEFT JOIN dbo.tblDepartment AS Dpt ON mas.DeptId = Dpt.DepartmentId
left join tblUser us on us.UserId = mas.ModifyBy
WHERE mas.AuditTrail_JobReqId = 
(SELECT max(AuditTrail_JobReqId) FROM tblJobReqForm_AuditTrail t2 WHERE t2.JobReqId = mas.JobReqId) 
 " + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable EmployeeInformationLoadDal(string parameter2)
        {

            string query = @"  SELECT mas.AuidtTrail_EmpInfoId,mas.EmpInfoId,CASE WHEN  mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end  StatusStyle,
 mas.StatusMode,mas.ModifyDate,us.UserName,mas.EmployeeStatus, EM.EmpInfoId, EM.EmpMasterCode, EM.EmpName, Dpt.DepartmentName, desig.Designation, 
sal.SalaryLocation, Etype.EmpType, div.DivisionName FROM dbo.tblEmpGeneralInfo_AuidtTrail mas WITH (NOLOCK)

left JOIN  dbo.tblEmpGeneralInfo EM ON EM.EmpInfoId = mas.EmpInfoId 
left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EM.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = EM.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = EM.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = EM.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = EM.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = EM.EmpTypeId 
left join tblUser us on us.UserId = mas.ModifyBy

WHERE mas.AuidtTrail_EmpInfoId = 
(SELECT max(AuidtTrail_EmpInfoId) FROM tblEmpGeneralInfo_AuidtTrail t2 WHERE t2.EmpInfoId = mas.EmpInfoId) 
 " + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable TrainningBudgetLoadDal(string parameter2)
        {

            string query = @" Select mas.AuditTrail_TrainingBudget2Id, mas.TrainingBudget2Id, mas.StatusMode, mas.TotalYearlyBudgetCost, mas.ModifyDate,
us.UserName, fy.FinancialYearDesc, comn.ShortName,
CASE WHEN mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end StatusStyle from tblTrainingBudget2Master_AuditTrail mas
left join tblUser us on us.UserId = mas.ModifyBy
left join tblCompanyInfo comn on comn.CompanyId = mas.CompanyId
left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId
WHERE mas.AuditTrail_TrainingBudget2Id = 
(SELECT max(AuditTrail_TrainingBudget2Id) FROM tblTrainingBudget2Master_AuditTrail t2 WHERE t2.TrainingBudget2Id = mas.TrainingBudget2Id)
 " + parameter2 + " ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GeTtrainingBudgetAuditTrailByMasterId(string ID)
        {
            try
            {
                string query = @" Select mas.AuditTrail_TrainingBudget2Id, mas.TrainingBudget2Id, mas.StatusMode, mas.TotalYearlyBudgetCost, mas.ModifyDate,
us.UserName, fy.FinancialYearDesc, comn.ShortName,
CASE WHEN mas.StatusMode='DELETE' THEN  'btn btn-sm btn-danger' ELSE 'btn btn-sm btn-warning' end StatusStyle from tblTrainingBudget2Master_AuditTrail mas
left join tblUser us on us.UserId = mas.ModifyBy
left join tblCompanyInfo comn on comn.CompanyId = mas.CompanyId
left join tblFinancialYear fy on fy.FinancialYearId = mas.FinancialYearId
where mas.TrainingBudget2Id = " + ID;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable GeTtrainingBudgetDetailsAuditTrail(string ID)
        {
            try
            {
                string query = @" 
SELECT mas.AuditTrail_TrainingBudget2Id, mas.AuditTrail_TrainingBudget2DetailsId, mas.TrainingTitle,
mas.ExpectedResult, mas.Grade,CASE
    WHEN  mas.IsExternal = 'true' THEN 'External'
	 WHEN  mas.IsInternal = 'true' THEN 'Internal' 
END AS Externalinternal, 
CASE
    WHEN  mas.IsForeign = 'true' THEN 'Foreign'
	 WHEN  mas.IsLocal = 'true' THEN 'Local'  
END AS ForeignLocal,
mas.IsExternal, mas.IsForeign, mas.IsInternal, mas.IsLocal, mas.Quater,
mas.Referance, mas.Remarks, mas.Budget, mas.BudgetCostParticipant, mas.TotalParticipant
from tblTrainingBudget2Details_AuditTrail mas
left join tblQuarterMonthInfo QTM on QTM.QuarterMonthId = mas.MonthId

where mas.TrainingBudget2Id =" + ID;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
     
}
