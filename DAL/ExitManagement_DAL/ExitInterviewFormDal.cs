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

namespace DAL.Survey
{
    public class ExitInterviewFormDal
    {
        readonly ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            //string query = "SELECT * FROM tblCompanyInfo";

            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            _aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, DataBase.HRDB);
        }

        public DataTable LoadEmployeeInfo(string employeeId, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", employeeId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName
                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE EGI.EmpInfoId = @EmployeeId AND EGI.CompanyId = @CompanyId";

            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        } 
        public DataTable LoadEmployeeInfo(string employeeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", employeeId));
            //aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName
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

        public void LoadDivision(DropDownList ddl, string companyId)
        {

            string query = @"SELECT DSN.DivisionId,
                            DSN.DivisionName FROM dbo.tblDivision AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId;
            _aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", query, DataBase.HRDB);
        }

        public void LoadDepartment(DropDownList ddl, string companyId)
        {
            string query = @"SELECT DSN.DepartmentId,
                            DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId;
            _aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, DataBase.HRDB);
        }

        public void LoadDesignation(DropDownList ddl)
        {
            string query = @"SELECT DSN.DesignationId,
                            DSN.Designation FROM dbo.tblDesignation AS DSN WHERE DSN.IsActive = 1";
            _aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, DataBase.HRDB);
        }

        public void LoadSalaryGrade(DropDownList ddl)
        {
            string query = @"SELECT DSN.SalaryGradeId,
                            DSN.GradeName FROM dbo.tblSalaryGrade AS DSN WHERE DSN.IsActive = 1";
            _aCommonInternalDal.LoadDropDownValue(ddl, "GradeName", "SalaryGradeId", query, DataBase.HRDB);
        }

        public DataTable LoadExitQuestion()
        {
            const string queryStr = @"SELECT * FROM tblExitQuestions";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadExitServayQuestion()
        {
            const string queryStr = @"SELECT ExitServyId,ServayQuestion FROM tblExitServyQs";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public int SaveMasterInfo(ExitFormMasterDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aDao.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", aDao.EmpName));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aDao.DesignationId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDao.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aDao.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@OtherOpenion", aDao.OtherOpenion));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aDao.EntryDate));

            string query = @"INSERT INTO dbo.tblExitInterviewFormMaster
                            (
                                CompanyId,
                                EmployeeId,
                                EmpCode,
                                EmpName,
                                JoiningDate,
                                DivisionId,
                                DepartmentId,
                                DesignationId,
                                SalaryGradeId,
                                OtherOpenion,
                                ActionStatus,
                                EntryBy,
                                EntryDate
                            )
                            VALUES
                            ( 
                                @CompanyId,
                                @EmployeeId,
                                @EmpCode,
                                @EmpName,
                                @JoiningDate,
                                @DivisionId,
                                @DepartmentId,
                                @DesignationId,
                                @SalaryGradeId,
                                @OtherOpenion,
                                @ActionStatus,
                                @EntryBy,
                                @EntryDate
                            )";

            return _aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }


        public int SaveExitReasonDetail(ExitReasonDetailDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterId", aDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@ReasonId", aDao.ReasonId));

            string query = @"INSERT INTO dbo.tblExitReasonDetail
                            (
                                MasterId,
                                ReasonId
                            )
                            VALUES
                            (  
                                @MasterId,
                                @ReasonId
                            )";

            return _aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }

        public int SaveExitServayDetail(ExitServayDetail aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterId", aDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@ServyItemId", aDao.ServyItemId));
            aSqlParameterlist.Add(new SqlParameter("@SarveyRatingValue", aDao.SarveyRatingValue));

            string query = @"INSERT INTO dbo.tblExitServeyDetail
                           (
                               MasterId,
                               ServyItemId,
                               SarveyRatingValue
                           )
                           VALUES
                           (   
                               @MasterId,
                               @ServyItemId,
                               @SarveyRatingValue
                           )";

            return _aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }
    }
}
