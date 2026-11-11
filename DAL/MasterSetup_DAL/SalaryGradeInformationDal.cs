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
    public class SalaryGradeInformationDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void GetDesignationTypeList(DropDownList ddl)
        {
            const string queryStr = @" SELECT  DesignationTypeId, DesigTypeName FROM  dbo.tblDesignationType WHERE IsActive='true'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DesigTypeName", "DesignationTypeId", queryStr, "HRDB");
        }

        public void GetEmpCategoryList(DropDownList ddl)
        {
            const string queryStr = @"SELECT EmpCategoryId, EmpCategoryName FROM tblEmpCategory WHERE IsActive='true'";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", queryStr, "HRDB");
        }

        public int SaveSalaryGradeInfo(SalaryGradeDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aInformationDao.EmpCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", aInformationDao.DesignationTypeId));
            aSqlParameterlist.Add(new SqlParameter("@GradeName", aInformationDao.GradeName));
            aSqlParameterlist.Add(new SqlParameter("@GradeCode", aInformationDao.GradeCode));
        
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblSalaryGrade (EmpCategoryId,DesignationTypeId,GradeName,GradeCode, IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@EmpCategoryId,@DesignationTypeId,@GradeName, @GradeCode, @IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable CheckGradeExistOrNot(string GradeCode)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@GradeCode", GradeCode));

            const string queryStr = @"SELECT * FROM tblSalaryGrade WHERE GradeCode = @GradeCode";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetSalaryGradeInformation()
        {
            const string queryStr = @"SELECT SG.SalaryGradeId, CAT.EmpCategoryName , Typ.DesigTypeName,SG.GradeName, SG.GradeCode,  sg.IsActive, SG.ApprovalStatus, SG.Description, sg.Remarks, SG.EntryBy, SG.EntryDate, SG.EntryDate, SG.UpdateBy, SG.UpdateDate   FROM tblSalaryGrade AS SG 
                                    
									  INNER JOIN dbo.tblEmpCategory AS CAT ON SG.EmpCategoryId = CAT.EmpCategoryId
									  INNER JOIN dbo.tblDesignationType AS Typ ON SG.DesignationTypeId = Typ.DesignationTypeId ORDER BY SG.SalaryGradeId DESC
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetSalaryGradeInformationById(string salaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", salaryGradeId));

            const string queryStr = @"SELECT * FROM tblSalaryGrade WHERE SalaryGradeId = @SalaryGradeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateSalaryTypeInfo(SalaryGradeDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aInformationDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aInformationDao.EmpCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", aInformationDao.DesignationTypeId));
            aSqlParameterlist.Add(new SqlParameter("@GradeName", aInformationDao.GradeName));
            aSqlParameterlist.Add(new SqlParameter("@GradeCode", aInformationDao.GradeCode));
           
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblSalaryGrade SET EmpCategoryId = @EmpCategoryId, DesignationTypeId= @DesignationTypeId, GradeName = @GradeName , GradeCode=@GradeCode, IsActive = @IsActive,
                                    Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate 
                                    WHERE SalaryGradeId = @SalaryGradeId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GradeAllocateOrNot(string salaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", salaryGradeId));

            const string queryStr = @"SELECT * FROM tblSalaryStep WHERE SalaryGradeId = @SalaryGradeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteSalaryGradeInfoById(string salaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", salaryGradeId));

            const string queryStr = @"DELETE FROM tblSalaryGrade WHERE SalaryGradeId = @SalaryGradeId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}

