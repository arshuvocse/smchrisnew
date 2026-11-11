using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class DesignationInformationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public Int32 SaveDesignationInfo(DesignationInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aInformationDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@Designation", aInformationDao.Designation));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string insertQuery = @"INSERT INTO tblDesignation (SalaryGradeId,Designation,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus, CompanyId)
                                   VALUES (@SalaryGradeId,@Designation,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus, @CompanyId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDesignationInfoDEL(DesignationInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aInformationDao.DesignationId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aInformationDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@Designation", aInformationDao.Designation));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));
         
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            const string insertQuery = @"INSERT INTO DELtblDesignation (DesignationId,SalaryGradeId,Designation,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus, CompanyId)
                                   VALUES (@DesignationId,@SalaryGradeId,@Designation,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus,@CompanyId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        //View

        public DataTable GetDesignationInformation(string Parm)
        {
            string queryStr = @"SELECT tblCompanyInfo.ShortName, SG.GradeCode + isnull( ' : '+ GradeName,'') as GradeName, EmpCat.EmpCategoryName, * From tblDesignation AS DSG 
  INNER JOIN dbo.tblSalaryGrade SG ON DSG.SalaryGradeId=SG.SalaryGradeId
  INNER JOIN dbo.tblEmpCategory EmpCat ON SG.EmpCategoryId=EmpCat.EmpCategoryId
 LEFT JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = DSG.CompanyId
   WHERE DSG.Designation IS NOT NULL " + Parm;
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetDesignationInformationById(Int32 designationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", designationId));

            const string queryStr = @"SELECT SG.GradeCode+' : '+ SG.GradeName as GradeName, EmpCat.EmpCategoryName, EmpCat.EmpCategoryId, SG.SalaryGradeId,  * From tblDesignation AS DSG 
  INNER JOIN dbo.tblSalaryGrade SG ON DSG.SalaryGradeId=SG.SalaryGradeId
  INNER JOIN dbo.tblEmpCategory EmpCat ON SG.EmpCategoryId=EmpCat.EmpCategoryId
                            WHERE DSG.DesignationId =  @DesignationId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");    
        }

        public bool UpdateDesignationInfo(DesignationInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aInformationDao.DesignationId));
            //aSqlParameterlist.Add(new SqlParameter("@DesignationStepId", aInformationDao.DesignationStepId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aInformationDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@Designation", aInformationDao.Designation));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            const string queryStr = @"UPDATE tblDesignation SET SalaryGradeId = @SalaryGradeId,Designation = @Designation,IsActive = @IsActive,
                                     Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate , CompanyId=@CompanyId WHERE DesignationId = @DesignationId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteDesgInfoById(string designationId)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@designationId", designationId));

            const string queryStr = @"DELETE FROM tblDesignation WHERE DesignationId = @designationId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadDesinationStep(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblDesignationStep WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DesignationStepName", "DesignationStepId", queryStr, "HRDB");
        }

        public void GetEmpCategoryList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM dbo.tblEmpCategory WHERE IsActive='true'";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", queryStr, "HRDB");
        }

        public void GetSalaryGradeList(DropDownList ddl, string EmpCategoryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId));

            const string queryStr = @"SELECT   SalaryGradeId, GradeCode  + isnull( ' : '+ GradeName,'') AS GradeName FROM tblSalaryGrade WHERE IsActive = 'True' AND EmpCategoryId =@EmpCategoryId";
            aCommonInternalDal.LoadDropDownValue(ddl, "GradeName", "SalaryGradeId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetSalaryStepList(DropDownList ddl, string salaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", salaryGradeId));

            const string queryStr = @"SELECT SalaryStepId, SalaryStepName FROM tblSalaryStep WHERE IsActive = 'True' AND SalaryGradeId = @SalaryGradeId";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryStepName", "SalaryStepId", queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable CheckGradeExistOrNot(string Designation)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@Designation", Designation));
          

            const string queryStr = @"SELECT * FROM tblDesignation WHERE Designation = @Designation";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

    }
}

