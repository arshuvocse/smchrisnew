using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
   public class EmployeeJobLeftEntryReportDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetEmployeeJobLeftEntryById(string id)
        {
            string query = @"SELECT Et.EmpType, LT.JobLeftType, com.ShortName,  EG.EmpName AS EmployeeName , EG.EmpMasterCode, deg.Designation, SG.GradeCode+':'+ SG.GradeName AS GradeName, 
  Div.DivisionName, Wing.DivisionWingName, Sec.SectionName, SubSec.SubSectionName, Dpt.DepartmentName,  * FROM tblEmployeeJobLeft
LEFT JOIN dbo.tblEmpGeneralInfo EG ON tblEmployeeJobLeft.EmployeeId= EG.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId								
								LEFT JOIN dbo.tblCompanyInfo  com ON tblEmployeeJobLeft.CompanyId=com.CompanyId	
								LEFT JOIN dbo.tblJobLeftType  LT ON tblEmployeeJobLeft.JobLeftTypeId=LT.JobLeftTypeId
								LEFT JOIN dbo.tblEmployeeType  Et ON EG.EmpTypeId=Et.EmpTypeId
								WHERE tblEmployeeJobLeft.EmployeeJobLeftId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadEmpJobleftBenefitByBenefit(string id, string benefitId)
        {
            string queryStr = @"SELECT * FROM dbo.tblEmployeeJobLeftBenefit
LEFT JOIN dbo.tblBenefitMaster ON dbo.tblBenefitMaster.BenefitMasterId=dbo.tblEmployeeJobLeftBenefit.BenefitId WHERE EmployeeJobLeftId='" + id + "' AND BenefitId='" + benefitId + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
