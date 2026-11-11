using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;

namespace DAL.MeetingMinorsDAL
{
    public class MeetingCategoryDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(MeetingCategoryDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CategoryID", aInformationDao.CategoryID));
            aSqlParameterlist.Add(new SqlParameter("@MeetingCategory", aInformationDao.MeetingCategory));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aInformationDao.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aInformationDao.CreateDate));
            const string queryStr = @"INSERT INTO tblMeeting_Category_DEL(CategoryID,MeetingCategory,CreateBy,CreateDate)
                                   VALUES (@CategoryID,@MeetingCategory,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(MeetingCategoryDao aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MeetingCategory", aVacancyEntryDao.MeetingCategory));

            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aVacancyEntryDao.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aVacancyEntryDao.CreateDate));

            const string queryStr = @"INSERT INTO tblMeeting_Category (MeetingCategory,CreateBy,CreateDate)
                                   VALUES (@MeetingCategory,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CategoryID", MId));
            const string queryStr = @"SELECT * FROM tblMeeting_Category WHERE CategoryID = @CategoryID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(MeetingCategoryDao aUpdateDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CategoryID", aUpdateDao.CategoryID));
            aSqlParameterlist.Add(new SqlParameter("@MeetingCategory", aUpdateDao.MeetingCategory));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aUpdateDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aUpdateDao.UpdateDate));

            const string queryStr = @"UPDATE tblMeeting_Category SET MeetingCategory = @MeetingCategory, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE CategoryID = @CategoryID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }



        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CategoryID", Id));

            const string queryStr = @"DELETE FROM tblMeeting_Category  WHERE CategoryID = @CategoryID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetEntryformation()
        {
            string queryStr = @"SELECT us.UserName as EntryBy, usUp.UserName as UpdateBy, * FROM tblMeeting_Category MTb
              left JOIN  dbo.tblUser us   ON  MTb.CreateBy =us.UserId  
              left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetCheckPartInfo(string ComID)
        {
            string query = @"SELECT UPPER(MeetingCategory) as MeetingCategory  FROM dbo.tblMeeting_Category  with(nolock)
                   where  MeetingCategory='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheckPartInfo2(string ComID, string MasterdId)
        {
            string query = @"SELECT UPPER(MeetingCategory) as MeetingCategory FROM dbo.tblMeeting_Category  with(nolock)
                      where  MeetingCategory ='" + ComID + "' AND CategoryID not in (" + MasterdId + ")";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }
    }

}
