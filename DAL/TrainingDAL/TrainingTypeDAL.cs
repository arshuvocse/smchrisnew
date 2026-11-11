using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.TrainingDAL
{
    public class TrainingTypeDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveTrainingType(TrainingTypeDAO aTrainingTypeDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingType", aTrainingTypeDao.TrainingType));
            aSqlParameterlist.Add(new SqlParameter("@Description", aTrainingTypeDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Evalution", aTrainingTypeDao.Evalution));
            aSqlParameterlist.Add(new SqlParameter("@MonthName", aTrainingTypeDao.MonthName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aTrainingTypeDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aTrainingTypeDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aTrainingTypeDao.EntryDate));


            const string queryStr = @"INSERT INTO dbo.tblTrainingType
                                        (
                                            TrainingType,
                                            Description,
                                            Evalution,
                                            MonthName,
                                            IsActive,
                                            EntryBy,
                                            EntryDate
                                        )
                                        VALUES
                                        (  @TrainingType,
                                            @Description,
                                            @Evalution,
                                            NULLIF(@MonthName,' '),
                                            @IsActive,
                                            @EntryBy,
                                            @EntryDate
                                        )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public int SaveTrainingTypeDEL(TrainingTypeDAO aTrainingTypeDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingType", aTrainingTypeDao.TrainingType));
            aSqlParameterlist.Add(new SqlParameter("@Description", aTrainingTypeDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Evalution", aTrainingTypeDao.Evalution));
            aSqlParameterlist.Add(new SqlParameter("@MonthName", aTrainingTypeDao.MonthName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aTrainingTypeDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aTrainingTypeDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aTrainingTypeDao.EntryDate));


            const string queryStr = @"INSERT INTO dbo.tblTrainingType
                                        (
                                            TrainingType,
                                            Description,
                                            Evalution,
                                            MonthName,
                                            IsActive,
                                            EntryBy,
                                            EntryDate
                                        )
                                        VALUES
                                        (  @TrainingType,
                                            @Description,
                                            @Evalution,
                                            NULLIF(@MonthName,' '),
                                            @IsActive,
                                            @EntryBy,
                                            @EntryDate
                                        )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        
        public bool UpdateTrainingtype(TrainingTypeDAO aTrainingTypeDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingTypeID", aTrainingTypeDao.TrainingTypeID));
            aSqlParameterlist.Add(new SqlParameter("@TrainingType", aTrainingTypeDao.TrainingType));
            aSqlParameterlist.Add(new SqlParameter("@Description", aTrainingTypeDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Evalution", aTrainingTypeDao.Evalution));
            aSqlParameterlist.Add(new SqlParameter("@MonthName", aTrainingTypeDao.MonthName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aTrainingTypeDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Updateby", aTrainingTypeDao.Updateby));
            aSqlParameterlist.Add(new SqlParameter("@Upatedate", aTrainingTypeDao.Updatedate));


            const string queryStr = @"UPDATE dbo.tblTrainingType SET 
                                        
                                            TrainingType=@TrainingType,
                                            Description=@Description,
                                            Evalution=@Evalution,
                                            MonthName=NULLiF(@MonthName,' '),
                                            IsActive=@IsActive,
                                            Updateby=@Updateby,
                                            Upatedate=@Upatedate WHERE TrainingTypeID=@TrainingTypeID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteTrainingtype(TrainingTypeDAO aTrainingTypeDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingTypeID", aTrainingTypeDao.TrainingTypeID));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aTrainingTypeDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aTrainingTypeDao.DeleteDate));
            aSqlParameterlist.Add(new SqlParameter("@IsDeleted", aTrainingTypeDao.IsDeleted));
            
            const string queryStr = @"UPDATE dbo.tblTrainingType SET 
                                        
                                            DeleteBy=@DeleteBy,
                                            DeleteDate=@DeleteDate,
                                            IsDeleted=@IsDeleted WHERE TrainingTypeID=@TrainingTypeID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetDataForById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@Id", Id));

            const string queryStr = @"SELECT * FROM dbo.tblTrainingType WHERE TrainingTypeID=@Id";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetDataForView()
        {

            const string queryStr = @"SELECT * FROM dbo.tblTrainingType  WHERE IsDeleted IS NULL OR IsDeleted  = 0 ";
            return aCommonInternalDal.DataContainerDataTable(queryStr,  "HRDB");
        }
    }
}
