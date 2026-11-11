using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class EmployeeCategoryInformationDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveEmployeeCategoryInfo(EmployeeCategoryInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryName", aInformationDao.EmpCategoryName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblEmpCategory (EmpCategoryName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@EmpCategoryName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public int SaveEmployeeCategoryInfoDEL(EmployeeCategoryInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aInformationDao.EmpCategoryId));

            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryName", aInformationDao.EmpCategoryName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO DELtblEmpCategory (EmpCategoryId, EmpCategoryName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@EmpCategoryId, @EmpCategoryName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable CheckEmpCategoryExistOrNot(string CategoryName)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryName", CategoryName));

            const string queryStr = @"SELECT * FROM tblEmpCategory WHERE EmpCategoryName = @EmpCategoryName";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetEmpCategoryInformation()
        {
            const string queryStr = @"SELECT * FROM tblEmpCategory ORDER BY  EmpCategoryId DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetEmpCategoryInformationById(string locationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", locationId));

            const string queryStr = @"SELECT * FROM tblEmpCategory WHERE EmpCategoryId = @EmpCategoryId";
            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist, "HRDB");
        }

        public bool UpdateEmpCategoryNInfo(EmployeeCategoryInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aInformationDao.EmpCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryName", aInformationDao.EmpCategoryName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblEmpCategory SET EmpCategoryName = @EmpCategoryName,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE EmpCategoryId = @EmpCategoryId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable RegionAllocatedOrNot(string regionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionId", regionId));

            const string queryStr = @"SELECT * FROM tblArea WHERE RegionId = @RegionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist ,"HRDB");
        }

        public bool DeleteEmpCategoryInfoById(string EmpCategoryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId));

            const string queryStr = @"DELETE FROM tblEmpCategory WHERE EmpCategoryId = @EmpCategoryId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist ,"HRDB");
        }



        public DataTable SalaryGradeAllocatedOrNot(string EmpCategoryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId));

            const string queryStr = @"SELECT * FROM tblSalaryGrade WHERE EmpCategoryId = @EmpCategoryId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}

