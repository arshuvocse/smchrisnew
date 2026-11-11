using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAL.MAIN_FUNCTION;

namespace DAL.UserPermissions_DAL
{
    public class EmployeeBeenCardDal
    {

       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

       public DataTable GetEmployeeInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));

           const string queryStr = @"SELECT Emp.EmpMasterCode,Emp.EmpInfoId,EMP.EmpName,Desg.Designation,Cat.EmpCategoryName, 
                                     Div.DivisionName,DivW.DivisionWingName, Sec.SectionName, dept.DepartmentName,
                                     SuSec.SubSectionName, SLoc.SalaryLocation AS Office, JLOC.Location AS Place,Sgrd.GradeName, SStep.SalaryStepName,*  from tblEmpGeneralInfo Emp
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
                                     WHERE Emp.EmpInfoId = @EmpInfoId";

           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmpPromotionInfo(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));

           const string queryStr = @"SELECT   EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, ISNULL(pgrd.GradeCode,'') +' ; '+ ISNULL(pgrd.GradeName,'')  AS PreviousGrade,PSTEP.SalaryStepName AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')+' ; '+ ISNULL(ngrd.GradeName,'') GradeName,nSTEP.SalaryStepName,nsesg.Designation
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId

                                     WHERE EMPP.EmployeeId = @EmpInfoId

									 UNION ALL 

									 SELECT FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, ''PreviousGrade, '' previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')+' ; '+ ISNULL(GradeName,'') GradeName, '' SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory


									  WHERE EmployeeID = @EmpInfoId";

           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmpPromotionInfoMulti(string Id)
       {

           

             string queryStr = @"SELECT   EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, ISNULL(pgrd.GradeCode,'') +' ; '+ ISNULL(pgrd.GradeName,'')  AS PreviousGrade,PSTEP.SalaryStepName AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')+' ; '+ ISNULL(ngrd.GradeName,'') GradeName,nSTEP.SalaryStepName,nsesg.Designation
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId

                                     WHERE EMPP.EmployeeId   in (" + Id + @")

									 UNION ALL 

									 SELECT FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, ''PreviousGrade, '' previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')+' ; '+ ISNULL(GradeName,'') GradeName, '' SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory


									  WHERE EmployeeID  in (" + Id + ")";

           return aCommonInternalDal.DataContainerDataTable(queryStr,  DataBase.HRDB);
       }
       public DataTable GetEmpTransferInfo(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));

           const string queryStr = @"SELECT  TD.EffectiveDate,EGI.EmpMasterCode,FIN.FinancialYearDesc,CASE WHEN TD.IsInterCompanyTransfer = 1 THEN 'Yes' ELSE 'No' END AS InterCompanyTransfer,NCI.ShortName AS NewCompany,
                                     NLC.SalaryLocation AS NewOffice, NLOC.Location AS NewPlace, DSN.DivisionName AS NewDivision,
                                      NW.DivisionWingName AS NewWing, NDPT.DepartmentName AS NewDepartment,
                                     NSec.SectionName AS NewSection, NSuSec.SubSectionName AS NewSubSection 
                                     FROM dbo.tblEmpTransferAndRedesignation AS TD
                                     INNER JOIN dbo.tblFinancialYear AS FIN ON FIN.FinancialYearId = TD.FinancialYearId
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON TD.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TD.OldCompanyId
                                     INNER JOIN dbo.tblCompanyInfo AS NCI ON NCI.CompanyId = TD.NewCompanyId
                                     LEFT JOIN dbo.tblSalaryLocation PLC ON PLC.SalaryLoationId = TD.OldSalaryLocationId
                                     LEFT JOIN dbo.tblSalaryLocation NLC ON NLC.SalaryLoationId = TD.NewSalaryLocationId
                                     LEFT JOIN dbo.tblJobLocation NLOC ON NLOC.JobLocationID = TD.NewJobLocationId
                                     LEFT JOIN dbo.tblJobLocation PLOC ON PLOC.JobLocationID = TD.OldJobLocationId
                                     LEFT JOIN dbo.tblDivision PDiv ON PDiv.DivisionId = TD.OldDivisionId
                                     left JOIN dbo.tblDivision DSN ON DSN.DivisionId = TD.NewDivisionId
                                     left JOIN dbo.tblDivisionWing NW ON NW.DivisionWId = TD.NewWingId
                                     left JOIN dbo.tblDivisionWing PNW ON PNW.DivisionWId = TD.OldWingId
                                     LEFT JOIN dbo.tblDepartment Pdept ON Pdept.DepartmentId = TD.OldDepartmentId
                                     LEFT JOIN dbo.tblDepartment NDPT ON NDPT.DepartmentId = TD.NewDepartmentId
                                     LEFT JOIN dbo.tblSection PSec ON PSec.SectionId = TD.OldSectionId
                                     LEFT JOIN dbo.tblSection NSec ON NSec.SectionId = TD.NewSectionId
                                     LEFT JOIN dbo.tblSubSection PSuSec ON TD.OldSubSectionId = PSuSec.SubSectionId 
                                     LEFT JOIN dbo.tblSubSection NSuSec ON TD.NewSubSectionId = NSuSec.SubSectionId

									 WHERE TD.EmployeeId =  @EmpInfoId  

 UNION ALL SELECT  EffectiveDate, EmployeeOldID EmpMasterCode,'' FinancialYearDesc, '' InterCompanyTransfer,
									  CompanyName as NewCompany, Office NewOffice, Place NewPlace, Division NewDivision, Wing NewWing, Department NewDepartment,
									  Section NewSection, SubSection NewSubSection 
									    FROM tblTransferHistory  WHERE EmployeeId = @EmpInfoId ";

           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmpTransferInfoMulti(string Id)
       {

          

             string queryStr = @"SELECT  TD.EffectiveDate,EGI.EmpMasterCode,FIN.FinancialYearDesc,CASE WHEN TD.IsInterCompanyTransfer = 1 THEN 'Yes' ELSE 'No' END AS InterCompanyTransfer,NCI.ShortName AS NewCompany,
                                     NLC.SalaryLocation AS NewOffice, NLOC.Location AS NewPlace, DSN.DivisionName AS NewDivision,
                                      NW.DivisionWingName AS NewWing, NDPT.DepartmentName AS NewDepartment,
                                     NSec.SectionName AS NewSection, NSuSec.SubSectionName AS NewSubSection 
                                     FROM dbo.tblEmpTransferAndRedesignation AS TD
                                     INNER JOIN dbo.tblFinancialYear AS FIN ON FIN.FinancialYearId = TD.FinancialYearId
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON TD.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TD.OldCompanyId
                                     INNER JOIN dbo.tblCompanyInfo AS NCI ON NCI.CompanyId = TD.NewCompanyId
                                     LEFT JOIN dbo.tblSalaryLocation PLC ON PLC.SalaryLoationId = TD.OldSalaryLocationId
                                     LEFT JOIN dbo.tblSalaryLocation NLC ON NLC.SalaryLoationId = TD.NewSalaryLocationId
                                     LEFT JOIN dbo.tblJobLocation NLOC ON NLOC.JobLocationID = TD.NewJobLocationId
                                     LEFT JOIN dbo.tblJobLocation PLOC ON PLOC.JobLocationID = TD.OldJobLocationId
                                     LEFT JOIN dbo.tblDivision PDiv ON PDiv.DivisionId = TD.OldDivisionId
                                     left JOIN dbo.tblDivision DSN ON DSN.DivisionId = TD.NewDivisionId
                                     left JOIN dbo.tblDivisionWing NW ON NW.DivisionWId = TD.NewWingId
                                     left JOIN dbo.tblDivisionWing PNW ON PNW.DivisionWId = TD.OldWingId
                                     LEFT JOIN dbo.tblDepartment Pdept ON Pdept.DepartmentId = TD.OldDepartmentId
                                     LEFT JOIN dbo.tblDepartment NDPT ON NDPT.DepartmentId = TD.NewDepartmentId
                                     LEFT JOIN dbo.tblSection PSec ON PSec.SectionId = TD.OldSectionId
                                     LEFT JOIN dbo.tblSection NSec ON NSec.SectionId = TD.NewSectionId
                                     LEFT JOIN dbo.tblSubSection PSuSec ON TD.OldSubSectionId = PSuSec.SubSectionId 
                                     LEFT JOIN dbo.tblSubSection NSuSec ON TD.NewSubSectionId = NSuSec.SubSectionId

									 WHERE TD.EmployeeId  in (" + Id + @")

 UNION ALL SELECT  EffectiveDate, EmployeeOldID EmpMasterCode,'' FinancialYearDesc, '' InterCompanyTransfer,
									  CompanyName as NewCompany, Office NewOffice, Place NewPlace, Division NewDivision, Wing NewWing, Department NewDepartment,
									  Section NewSection, SubSection NewSubSection 
									    FROM tblTransferHistory  WHERE EmployeeId in (" + Id + ")";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }

       public DataTable GetEmpIncrementInfo(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));

           const string queryStr = @"SELECT INC.IncrementId,INC.EffectiveDate,INC.EmployeeCode, INF.Name  Name,STP.SalaryStepName AS CurrentStep,CSTP.SalaryStepName AS IncrementalStep,INC.FeedSalary FROM dbo.tblIncrement AS INC 
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = INC.CompanyId
                                     INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = INC.EmployeeId

                                     LEFT JOIN dbo.tblSalaryStep STP ON STP.SalaryStepId = INC.CurrentStepId
                                     LEFT JOIN dbo.tblSalaryStep CSTP ON CSTP.SalaryStepId = INC.IncrementalStepId
                                     LEFT JOIN dbo.tblIncrementInfoMaster AS INF ON INF.IncrementInfoMasterId = INC.IncrementTypeId
                                      WHERE INC.EmployeeId = @EmpInfoId

									 UNION ALL SELECT NULL AS IncrementId,EffectiveDate, Emp.EmpMasterCode, 'Yearly increment' Name, '' CurrentStep, IncrementalStep,  NULL FeedSalary FROM tblIncrement_HistoricalData
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									     WHERE tblIncrement_HistoricalData.EmployeeId = @EmpInfoId";

           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpIncrementInfoMulti(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));

             string queryStr = @"SELECT INC.IncrementId,INC.EffectiveDate,INC.EmployeeCode, INF.Name  Name,STP.SalaryStepName AS CurrentStep,CSTP.SalaryStepName AS IncrementalStep,INC.FeedSalary FROM dbo.tblIncrement AS INC 
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = INC.CompanyId
                                     INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = INC.EmployeeId

                                     LEFT JOIN dbo.tblSalaryStep STP ON STP.SalaryStepId = INC.CurrentStepId
                                     LEFT JOIN dbo.tblSalaryStep CSTP ON CSTP.SalaryStepId = INC.IncrementalStepId
                                     LEFT JOIN dbo.tblIncrementInfoMaster AS INF ON INF.IncrementInfoMasterId = INC.IncrementTypeId
                                      WHERE INC.EmployeeId in (" + Id + @") UNION ALL SELECT NULL AS IncrementId,EffectiveDate, Emp.EmpMasterCode, 'Yearly increment' Name, '' CurrentStep, IncrementalStep,  NULL FeedSalary FROM tblIncrement_HistoricalData
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									     WHERE tblIncrement_HistoricalData.EmployeeId in (" + Id + ")";

           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpStateTransferInfo(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT  EGI.EmpInfoId,CNTE.ContractualEmpManageId,NEW.EmpMasterCode EmployeeOldID,EGI.EmpName,CNTE.EffectiveDate, ISNULL(EGI.EmpMasterCode,'') EmpMasterCode ,
                                     CASE WHEN CNTE.IsRenew = 1 THEN 'Renew' 
                                     	 WHEN CNTE.IsExtension = 1 THEN	'Extension'
                                     	 WHEN CNTE.IsPermanentToContractual = 1 THEN 'Permanent To Contractual'
                                     	 WHEN CNTE.IsContractualToPermanent = 1 THEN 'Contractual To Permanent'
                                          END AS ChangedState,
                                     CASE WHEN CNTE.IsRenew = 1 THEN CONVERT(VARCHAR,CNTE.RenewStartDate, 7) + '  to ' +  CONVERT(VARCHAR, CNTE.RenewToDate, 7)
                                     	 WHEN CNTE.IsExtension = 1 THEN	 CONVERT(VARCHAR,CNTE.ExtensionFromDate, 7) + '  to ' +  CONVERT(VARCHAR, CNTE.ExtensionToDate, 7)
                                     	 WHEN CNTE.IsPermanentToContractual = 1 THEN CONVERT(VARCHAR,CNTE.PermanentToContractualEffectiveDate, 7) + '  to ' +  CONVERT(VARCHAR, CNTE.PermanentToContractualEndDate, 7)
                                          END AS Duration,
                                     CASE WHEN CNTE.IsSalaryIncrement = 1 THEN 'Salary Increment'
                                     	 WHEN CNTE.IsNoIncrement = 1 THEN 'No Increment'
                                     	 ELSE 'No Increment'
                                     	 END AS Increment,
                                     CASE WHEN CNTE.IsFacilityIncluded = 1 THEN 'Facility Included'
                                     	 WHEN CNTE.IsNoFacility = 1 THEN 'No Facility'
                                     	 ELSE 'No Facility'
                                     	 END AS Facility
                                     FROM dbo.tblContractualEmpManage AS CNTE
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON CNTE.EmployeeId = EGI.EmpInfoId
                                     LEFT JOIN dbo.tblEmpGeneralInfo NEW ON NEW.EmpInfoId= EGI.ReferenceID
									
                                     WHERE   CNTE.EmployeeId =@EmpInfoId

									UNION ALL SELECT EGI.EmpInfoId, NULL ContractualEmpManageId,'' EmployeeOldID,EGI.EmpName, mas.EffectiveDate, EGI.EmpMasterCode,
								TypeOfStateChange	ChangedState,   CONVERT(VARCHAR,mas.EffectiveDate, 7) + '  to ' +  CONVERT(VARCHAR, mas.ExtensionToDate, 7) AS Duration, '' AS Increment, '' AS Facility
									  FROM tblStateChange_HistoricalDataId mas
									   INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON mas.EmployeeId = EGI.EmpInfoId
									      WHERE  mas.EmployeeId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmpStateTransferInfoMulti(string Id)
       {

           
             string queryStr = @"SELECT  EGI.EmpInfoId,CNTE.ContractualEmpManageId,NEW.EmpMasterCode EmployeeOldID,EGI.EmpName,CNTE.EffectiveDate, ISNULL(EGI.EmpMasterCode,'') EmpMasterCode ,
                                     CASE WHEN CNTE.IsRenew = 1 THEN 'Renew' 
                                     	 WHEN CNTE.IsExtension = 1 THEN	'Extension'
                                     	 WHEN CNTE.IsPermanentToContractual = 1 THEN 'Permanent To Contractual'
                                     	 WHEN CNTE.IsContractualToPermanent = 1 THEN 'Contractual To Permanent'
                                          END AS ChangedState,
                                     CASE WHEN CNTE.IsRenew = 1 THEN CONVERT(VARCHAR,CNTE.RenewStartDate, 7) + '  to ' +  CONVERT(VARCHAR, CNTE.RenewToDate, 7)
                                     	 WHEN CNTE.IsExtension = 1 THEN	 CONVERT(VARCHAR,CNTE.ExtensionFromDate, 7) + '  to ' +  CONVERT(VARCHAR, CNTE.ExtensionToDate, 7)
                                     	 WHEN CNTE.IsPermanentToContractual = 1 THEN CONVERT(VARCHAR,CNTE.PermanentToContractualEffectiveDate, 7) + '  to ' +  CONVERT(VARCHAR, CNTE.PermanentToContractualEndDate, 7)
                                          END AS Duration,
                                     CASE WHEN CNTE.IsSalaryIncrement = 1 THEN 'Salary Increment'
                                     	 WHEN CNTE.IsNoIncrement = 1 THEN 'No Increment'
                                     	 ELSE 'No Increment'
                                     	 END AS Increment,
                                     CASE WHEN CNTE.IsFacilityIncluded = 1 THEN 'Facility Included'
                                     	 WHEN CNTE.IsNoFacility = 1 THEN 'No Facility'
                                     	 ELSE 'No Facility'
                                     	 END AS Facility
                                     FROM dbo.tblContractualEmpManage AS CNTE
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON CNTE.EmployeeId = EGI.EmpInfoId
                                     LEFT JOIN dbo.tblEmpGeneralInfo NEW ON NEW.EmpInfoId= EGI.ReferenceID
									
                                     WHERE   CNTE.EmployeeId in (" + Id + @")

									UNION ALL  SELECT EGI.EmpInfoId, NULL ContractualEmpManageId,'' EmployeeOldID,EGI.EmpName, mas.EffectiveDate, EGI.EmpMasterCode,
								TypeOfStateChange	ChangedState,   CONVERT(VARCHAR,mas.EffectiveDate, 7) + '  to ' +  CONVERT(VARCHAR, mas.ExtensionToDate, 7) AS Duration, '' AS Increment, '' AS Facility
									  FROM tblStateChange_HistoricalDataId mas
									   INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON mas.EmployeeId = EGI.EmpInfoId
									      WHERE  mas.EmployeeId in (" + Id + ")";
           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }

       public DataTable EmpRedesignationInfo(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT  ERD.Effectivedate , Dsig.Designation  FROM tblEmployeeReDesignation ERD
  left JOIN dbo.tblDesignation  Dsig ON ERD.NDesignationId = Dsig.DesignationId
 WHERE EmployeeId= @EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable EmpRedesignationInfoMulti(string Id)
       {

     
             string queryStr = @"SELECT  ERD.Effectivedate , Dsig.Designation  FROM tblEmployeeReDesignation ERD
  left JOIN dbo.tblDesignation  Dsig ON ERD.NDesignationId = Dsig.DesignationId
 WHERE EmployeeId in (" + Id+")";
           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }

   }
}
