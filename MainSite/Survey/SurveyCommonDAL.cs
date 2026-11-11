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

namespace DAL.Survey
{
    public class SurveyCommonDAL
    {
        private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            _aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, "HRDB");
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

        public DataTable GetQuestionGroupForSurveyForm(string cid = "1")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));
            const string queryStr = @"SELECT DISTINCT g.SurveyQuestionGroupId AS SerialNo, g.SurveyQuestionGroupId, g.SurveyQuestionGroup 
FROM dbo.tblSurveyQuestionGroup g
INNER JOIN dbo.tblSurveyQuestion sq ON sq.SurveyQuestionGroupId = g.SurveyQuestionGroupId
INNER JOIN dbo.tblSurveyDetails sd ON sd.SurveyQuestionId = sq.SurveyQuestionId
INNER JOIN dbo.tblSurveyMaster sm ON sm.SurveyMasterId = sd.SurveyMasterId
WHERE sm.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetQuestionByGroupId(int SurveyQuestionGroupId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", SurveyQuestionGroupId));
            const string queryStr = @"SELECT sq.SurveyQuestionGroupId, sd.SurveyDetailsId,sq.SurveyQuestionId, sq.QuestionTitle ,sq.SurveyQuestionTypeId
FROM dbo.tblSurveyQuestionGroup g
INNER JOIN dbo.tblSurveyQuestion sq ON sq.SurveyQuestionGroupId = g.SurveyQuestionGroupId
INNER JOIN dbo.tblSurveyDetails sd ON sd.SurveyQuestionId = sq.SurveyQuestionId
INNER JOIN dbo.tblSurveyMaster sm ON sm.SurveyMasterId = sd.SurveyMasterId
WHERE sm.IsActive=1 AND sq.SurveyQuestionGroupId=@SurveyQuestionGroupId";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetProbationEvaluationRating()
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", SurveyQuestionGroupId));
            const string queryStr = @"SELECT r.tblProbationEvaluationRatingId as ValueField,r.TextField FROM dbo.tblProbationEvaluationRating r WHERE r.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, null, DataBase.HRDB);
        }

        public DataTable GetProbationEvaluationRatingByCompanyId(string id)
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", SurveyQuestionGroupId));
          string queryStr = @"SELECT r.tblProbationEvaluationRatingId as ValueField,r.TextField FROM dbo.tblProbationEvaluationRating r WHERE r.IsActive=1 and CompanyId='"+id+"'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, null, DataBase.HRDB);
        }
    }
}
