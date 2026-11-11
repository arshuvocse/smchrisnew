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
    public class SalaryMatrixReportDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(AchievementsEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@AchievementsName", aEntryDao.AchievementsName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblMasterAchievements (AchievementsName,IsActive,EntryBy,EntryDate)
                                   VALUES (@AchievementsName,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveInfoDEL(AchievementsEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterAchievementsId", aInformationDao.MasterAchievementsId));
            aSqlParameterlist.Add(new SqlParameter("@AchievementsName", aInformationDao.AchievementsName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblMasterAchievements (MasterAchievementsId,AchievementsName,IsActive,EntryBy,EntryDate)
                                   VALUES (@MasterAchievementsId, @AchievementsName, @IsActive, @EntryBy, @EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterAchievementsId", Id));


            const string queryStr = @"DELETE FROM tblMasterAchievements  WHERE MasterAchievementsId = @MasterAchievementsId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetVacaencyInformationById(string MasterAchievementsId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterAchievementsId", MasterAchievementsId));

            const string queryStr = @"SELECT * FROM tblMasterAchievements WHERE MasterAchievementsId = @MasterAchievementsId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(AchievementsEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterAchievementsId", aInformationDao.MasterAchievementsId));
            aSqlParameterlist.Add(new SqlParameter("@AchievementsName", aInformationDao.AchievementsName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));


            const string queryStr = @"UPDATE tblMasterAchievements SET AchievementsName = @AchievementsName, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive
                                   WHERE MasterAchievementsId = @MasterAchievementsId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

       

        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }


        public DataTable GetVacanceyEntryformationParam( )
        {
            string queryStr = @"SELECT * FROM tblSalaryMatrix ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetSgInfoManagement()
        {
            string queryStr = @"SELECT sg.SalaryGradeId, sg.GradeCode+ isnull(' : '+sg.GradeName,'') GradeName   FROM dbo.tblSalaryGrade sg WITH (NOLOCK) WHERE sg.EmpCategoryId=1 ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetSgInfoGraded()
        {
            string queryStr = @"SELECT sg.SalaryGradeId, sg.GradeCode+ isnull(' : '+sg.GradeName,'') GradeName   FROM dbo.tblSalaryGrade sg WITH (NOLOCK) WHERE sg.EmpCategoryId=2 ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

//        public DataTable GetStepInfo( int SgId)
//        {
//            string queryStr = @"SELECT    mas.SalaryStepName,   'Gross: '+  CAST(ISNULL(mas.GrossAmount,0) AS NVARCHAR(max)) +', Basic: '+  CAST(ISNULL(mas.BasicAmount,0) AS NVARCHAR(max)) GrossAmount   
//
// FROM dbo.tblSalaryStep mas WITH (NOLOCK)
// 
// 
//WHERE mas.IsActive=1  AND mas.SalaryGradeId=   " + SgId;
//            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
//        }


//        public DataTable GetStepInfoGrade(int SgId)
//        {
//            string queryStr = @"SELECT    mas.SalaryStepName,   CAST(ISNULL(mas.BasicAmount,0) AS NVARCHAR(max)) GrossAmount     
//
// FROM dbo.tblSalaryStep mas WITH (NOLOCK)
// 
// 
//WHERE mas.IsActive=1  AND mas.SalaryGradeId=   " + SgId;
//            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
//        }




        public DataTable GetStepInfo(int SgId)
        {
            string queryStr = @"SELECT  mas.SalaryStepName, 'Gross: '+  CAST(ISNULL(mas.GrossAmount,0) AS NVARCHAR(max)) +', Basic: '+  CAST(ISNULL(mas.BasicAmount,0) AS NVARCHAR(max)) GrossAmount  FROM dbo.tblSalaryStep mas WITH (NOLOCK)

              WHERE mas.IsActive=1  AND mas.SalaryGradeId=   " + SgId + " Order by CONVERT(int, SUBSTRING(SalaryStepName, 6, CHARINDEX('-', SalaryStepName) - 1),0)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetStepInfoGrade(int SgId)
        {
            string queryStr = @"SELECT    mas.SalaryStepName,   CAST(ISNULL(mas.BasicAmount,0) AS NVARCHAR(max)) GrossAmount     

 FROM dbo.tblSalaryStep mas WITH (NOLOCK)
 
 
WHERE mas.IsActive=1  AND mas.SalaryGradeId=   " + SgId + " Order by CONVERT(int, SUBSTRING(SalaryStepName, 6, CHARINDEX('-', SalaryStepName) - 1),0)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetMatrixReport(string ll)
        {
            string queryStr = @"SELECT  mas.IsActive, C.EmpCategoryName,mas.SalaryStepName,  sg.GradeCode+ isnull(' : '+sg.GradeName,'') GradeName,  mas.GrossAmount,mas.BasicAmount, '' AS Category, '' AS Effectivedate, '' AS Demo1s, '' AS Demo2s

 FROM dbo.tblSalaryStep mas WITH (NOLOCK)
LEFT JOIN tblSalaryGrade  sg ON sg.SalaryGradeId = mas.SalaryGradeId
 INNER JOIN dbo.tblEmpCategory C ON C.EmpCategoryId=sg.EmpCategoryId
WHERE mas.IsActive=1 ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
     
}
