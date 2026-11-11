using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.TrainingDAL
{
  public  class TrainingEffeftivenessDAL
    {
      private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();



      public DataTable LoadEmployeeDataBySuperAdmin(string parameter)
      {


          string queryStr = @"SELECT G.EmpInfoId, G.EmpName,*,M.TrainingRecordMasterId,C.CompanyName,M.TrainingTitle ,F.FinancialYearDesc,M.TrainingTypeId,T.TrainingType
				FROM tblTrainingRecordMaster M
				INNER JOIN dbo.tblCompanyInfo C ON M.CompanyId = C.CompanyId
				INNER JOIN dbo.tblFinancialYear F ON M.FinancialYearId = F.FinancialYearId
					INNER JOIN dbo.tblTrainingType T ON M.TrainingTypeID = T.TrainingTypeID
					INNER JOIN dbo.tbl_trainingRecordDetailsEmployee EE ON M.TrainingRecordMasterId = EE.TrainingRecordMasterId
					INNER JOIN dbo.tblEmpGeneralInfo G ON EE.EmpInfoId = G.EmpInfoId

					WHERE M.IsDelete = 0 OR M.IsDelete is null    ";

          return _aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
      }


      public DataTable LoadEmployeeData(string parameter)
      {


          string queryStr = @"SELECT G.EmpInfoId, G.EmpName,*,M.TrainingRecordMasterId,C.CompanyName,M.TrainingTitle ,F.FinancialYearDesc,M.TrainingTypeId,T.TrainingType
				FROM tblTrainingRecordMaster M
				INNER JOIN dbo.tblCompanyInfo C ON M.CompanyId = C.CompanyId
				INNER JOIN dbo.tblFinancialYear F ON M.FinancialYearId = F.FinancialYearId
					INNER JOIN dbo.tblTrainingType T ON M.TrainingTypeID = T.TrainingTypeID
					INNER JOIN dbo.tbl_trainingRecordDetailsEmployee EE ON M.TrainingRecordMasterId = EE.TrainingRecordMasterId
					INNER JOIN dbo.tblEmpGeneralInfo G ON EE.EmpInfoId = G.EmpInfoId

					WHERE (M.IsDelete = 0 OR M.IsDelete is null ) AND G.ReportingEmpId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'   AND G.EmployeeStatus='Active'";

          return _aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
      }


      public DataTable GetTrainingEmpData(string id)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@ID", id));
          const string queryStr = @"SELECT * FROM dbo.tblTrainingEvaluationMaster WHERE EmpInfoId=@ID";
          return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
      }

      public DataTable LoadEmployeeInfoByTrainingRecordMasterId(string employeeId)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@EmployeeId", employeeId));


          const string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,EGI.CompanyId
                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE EGI.EmpInfoId = @EmployeeId ";

          return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
      }
    }
}
