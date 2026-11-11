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
    public class SalaryStepInforamtionDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void GetSalaryTypeList(DropDownList ddl)
        {
            const string queryStr = @"SELECT SalaryTypeId, SalaryType FROM tblSalaryType";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryType", "SalaryTypeId", queryStr, "HRDB");
        }

        public void GetSalaryGradeList(DropDownList ddl)
        {
            const string queryStr = @"SELECT   SalaryGradeId,ISNULL(GradeCode,'') +isnull( ' : '+ GradeName,'')  AS GradeName FROM tblSalaryGrade WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "GradeName", "SalaryGradeId", queryStr, "HRDB");
        }

        public int SaveSalaryStepInfo(SalaryStepInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aInformationDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryStepName", aInformationDao.SalaryStepName));
            aSqlParameterlist.Add(new SqlParameter("@GrossAmount", aInformationDao.GrossAmount));
            aSqlParameterlist.Add(new SqlParameter("@BasicAmount", aInformationDao.BasicAmount));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblSalaryStep (BasicAmount,GrossAmount,SalaryGradeId,SalaryStepName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@BasicAmount,@GrossAmount,@SalaryGradeId,@SalaryStepName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public int SaveSalaryStepInfoDEL(SalaryStepInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", aInformationDao.SalaryStepId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aInformationDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryStepName", aInformationDao.SalaryStepName));
            aSqlParameterlist.Add(new SqlParameter("@GrossAmount", aInformationDao.GrossAmount));
            aSqlParameterlist.Add(new SqlParameter("@BasicAmount", aInformationDao.BasicAmount));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblSalaryStep (SalaryStepId, BasicAmount,GrossAmount,SalaryGradeId,SalaryStepName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@SalaryStepId, @BasicAmount,@GrossAmount,@SalaryGradeId,@SalaryStepName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable CheckStepExistOrNot(string salaryStep, string SalaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@StepName", salaryStep));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", SalaryGradeId));

            const string queryStr = @"SELECT * FROM tblSalaryStep WHERE SalaryGradeId=@SalaryGradeId and SalaryStepName = @StepName";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable CheckStepExistOrNotDelete(string salaryStep)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@StepName", salaryStep));


            const string queryStr = @" SELECT * FROM tblEmpGeneralInfo  with (nolock) WHERE  SalaryStepId = @StepName";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetSalaryStepInformation(string Parm)
        {
            string queryStr = @"SELECT SG.GradeCode ,SG.GradeName  , STP.SalaryStepId, * FROM tblSalaryStep AS STP with (nolock)  
 left JOIN tblSalaryGrade AS SG ON STP.SalaryGradeId = SG.SalaryGradeId  WHERE STP.SalaryStepId IS NOT null " + Parm +
                                                                         " ORDER BY SG.GradeCode ASC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetSalaryStepInformationById(string salaryStepId)
        {
            string query = @"SELECT * FROM dbo.tblSalaryStep Where SalaryStepId='" + salaryStepId + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool UpdateSalaryStepInfo(SalaryStepInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", aInformationDao.SalaryStepId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aInformationDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryStepName", aInformationDao.SalaryStepName));
            aSqlParameterlist.Add(new SqlParameter("@GrossAmount", aInformationDao.GrossAmount));
            aSqlParameterlist.Add(new SqlParameter("@BasicAmount", aInformationDao.BasicAmount));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblSalaryStep SET GrossAmount=@GrossAmount,BasicAmount=@BasicAmount, SalaryGradeId = @SalaryGradeId,SalaryStepName = @SalaryStepName, IsActive = @IsActive,
                                    Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate 
                                    WHERE SalaryStepId = @SalaryStepId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GradeAllocateOrNot(string salaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", salaryGradeId));

            const string queryStr = @"SELECT * FROM tblSalaryStep WHERE SalaryGradeId = @SalaryGradeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteSalaryStepInfoById(string salaryTypeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryTypeId", salaryTypeId));

            const string queryStr = @"DELETE FROM tblSalaryStep WHERE SalaryStepId = @SalaryTypeId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetSalaryGradeList(DropDownList ddl, string salaryTypeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryTypeId", salaryTypeId));

            const string queryStr = @"SELECT SalaryGradeId, GradeName FROM tblSalaryGrade WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "GradeName", "SalaryGradeId", queryStr, "HRDB");
        }
    }
}
