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

namespace DAL.Report_DAL
{
    public class ProbationaryEmployeeReportDal
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
                aCommonInternalDal.LoadDropDownValueCompany(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public DataTable GetAllDivision(string compId)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivision  WITH (NOLOCK) WHERE IsActive='1' AND CompanyId='" + compId + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllWing(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivisionWing  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
  WHERE tblDivisionWing.IsActive='1' AND (Invisible='0' OR Invisible IS NULL) " + param + " ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllDepartment(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDepartment  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblDepartment.IsActive='1' AND (tblDepartment.Invisible='0' OR tblDepartment.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSection.IsActive='1' AND (tblSection.Invisible='0' OR tblSection.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSubSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSubSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSubSection.IsActive='1'  " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable LoadInfoProbationaryEmployeeDAL(string param)
        {

            List<SqlParameter> aSqlParameters = new List<SqlParameter>();

            aSqlParameters.Add(new SqlParameter("@Pram", param));

            return aCommonInternalDal.GetDataByStoreProcedure("sp_AccountsIntegrationProbationaryEmployee", aSqlParameters, "HRDB");

//            string queryStr = @"SELECT     com.ShortName, EG.EmpMasterCode, EG.EmpName, EG.DateOfJoin,  EG.ProbationEndDate, DS.Designation,  EPE.ProbationEndReason, EPE.ExProDate,*  FROM tblProbationEvaluationMaster EPE  WITH (NOLOCK) 
//  INNER JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmpInfoId = EG.EmpInfoId
//                                    INNER JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
//                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
//                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
//                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
//                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
//
//                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
//                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
//                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
//                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
//                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
//                               
//
//                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
//                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
//                                  LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
//
//                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
//                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
//
//								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
//                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
//
//                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
//                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana                             where EPE.ProbationEvaluationMasterId is not null 
//						 " + param + "  ORDER BY EG.EmpMasterCode ASC";
//            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public Int32 SaveProbationConfirmationList(ProbationListDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aDao.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@ZID", aDao.ZID));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDao.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@ProbationEndDate", aDao.ProbationEndDate));
            aSqlParameterlist.Add(new SqlParameter("@Approveby", aDao.Approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", aDao.ApproveDate));


            string query = @"INSERT INTO tblAccountsIntegration_ProbationList
                           (EmpInfoId
           ,EmpMasterCode
           ,ZID
           ,JoiningDate
           ,ProbationEndDate
           ,Approveby
           ,ApproveDate
                            )
                            VALUES
                            (
            @EmpInfoId
           ,@EmpMasterCode
           ,@ZID
           ,@JoiningDate
           ,@ProbationEndDate
           ,@Approveby
           ,@ApproveDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }
    }
}
