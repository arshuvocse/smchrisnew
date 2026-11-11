using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
  public  class ContractualEmpManagementReportDal
    {
      ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

      public DataTable GetContractualEmpManageById(string id)
      {
          string query = @" SELECT com.ShortName, EG.EmpName AS EmployeeName ,  EG.EmpMasterCode, deg.Designation, SG.GradeCode+':'+ SG.GradeName AS GradeName,   Div.DivisionName, Wing.DivisionWingName, Sec.SectionName, SubSec.SubSectionName, Dpt.DepartmentName, * FROM dbo.tblContractualEmpManage
           INNER JOIN dbo.tblEmpGeneralInfo EG ON tblContractualEmpManage.EmployeeId= EG.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
								LEFT JOIN dbo.tblCompanyInfo  com ON tblContractualEmpManage.CompanyId=com.CompanyId
           WHERE  ContractualEmpManageId='" + id + "'";
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }


      public DataTable LoadProject(int id)
      {
          string query = @"SELECT Pset.ProjectId, mas.EmpInfoId,  Pset.ProjectName, Pset.ProjectStartDate, Pset.ProjectEndDate,* FROM tblEmployeeWiseProjectAllocationMaster mas
LEFT JOIN tblEmployeeWiseProjectAllocationDetail Det ON  mas.EmpWiseProjectID=Det.EmployeeWiseProjectAllocationMasterId
LEFT JOIN dbo.tblProjectSetup Pset ON  Det.ProjectId=Pset.ProjectId  WHERE mas.EmpInfoId='" + id + "'";


          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }


      public DataTable RptSurveyLoad(string Masterid, string EmpID)
      {
          string query = @"SELECT tblSurveyMaster.SurveyName, tblEmpGeneralInfo.EmpName, tblEmpGeneralInfo.EmpMasterCode, com.CompanyName, DS.Designation,   * FROM dbo.tblSurveySubmitMaster
inner JOIN dbo.tblSurveyMaster ON tblSurveyMaster.SurveyMasterId=tblSurveySubmitMaster.SurveyID
INNER JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId=tblSurveySubmitMaster.EmployeeId
      LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = tblEmpGeneralInfo.CompanyId
                    LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = tblEmpGeneralInfo.DesignationId WHERE tblSurveySubmitMaster.SurveyID='" + Masterid + "' and tblSurveySubmitMaster.EmployeeId='" + EmpID + "'";


          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }



      public DataTable RptSurveyLoadDetails(int id)
      {
          string query =
              @"SELECT * FROM dbo.tblSurveySubmitDetail WHERE  QuestionTypeId=1 AND  SurveySubmitMasterId='" + id + "'";


          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");

      }

      public DataTable RptSurveyLoadDetails2(int id)
      {
          string query =
              @"SELECT * FROM dbo.tblSurveySubmitDetail WHERE  QuestionTypeId=4 AND  SurveySubmitMasterId='" + id + "'";


          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");

      }


    }
}
