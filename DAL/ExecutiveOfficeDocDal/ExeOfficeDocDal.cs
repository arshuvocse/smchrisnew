using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.ExcOfficeDoc_Dao;
using DAO.MeetingMinorsDAO;

namespace DAL.ExecutiveOfficeDocDal
{
    public class ExeOfficeDocDal
    {

        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public Int32 SaveInfoDEL(ExeofficeDocCategory_DEL_Dao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCatId", aInformationDao.ExeOfficeDocCatId));
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCategory", aInformationDao.ExeOfficeDocCategory));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aInformationDao.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aInformationDao.CreateDate));
            const string queryStr = @"INSERT INTO tblExeOfficeDocCategory_DEL(ExeOfficeDocCatId,ExeOfficeDocCategory,CreateBy,CreateDate)
                                   VALUES (@ExeOfficeDocCatId,@ExeOfficeDocCategory,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public int SaveEntryInfo(ExeOfficeDocCategoryDao aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCategory", aVacancyEntryDao.ExeOfficeDocCategory));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aVacancyEntryDao.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aVacancyEntryDao.CreateDate));
            const string queryStr = @"INSERT INTO tblExeOfficeDocCategory (ExeOfficeDocCategory,CreateBy,CreateDate)
                                   VALUES (@ExeOfficeDocCategory,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCatId", MId));
            const string queryStr = @"SELECT * FROM tblExeOfficeDocCategory WHERE ExeOfficeDocCatId = @ExeOfficeDocCatId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(ExeOfficeDocCategoryDao aUpdateDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCatId", aUpdateDao.ExeOfficeDocCatId));
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCategory", aUpdateDao.ExeOfficeDocCategory));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aUpdateDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aUpdateDao.UpdateDate));

            const string queryStr = @"UPDATE tblExeOfficeDocCategory SET ExeOfficeDocCategory = @ExeOfficeDocCategory, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE ExeOfficeDocCatId = @ExeOfficeDocCatId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCatId", Id));

            const string queryStr = @"DELETE FROM tblExeOfficeDocCategory  WHERE ExeOfficeDocCatId = @ExeOfficeDocCatId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetEntryformation()
        {
            string queryStr = @"SELECT us.UserName as EntryBy, usUp.UserName as UpdateBy, * FROM tblExeOfficeDocCategory MTb
              left JOIN  dbo.tblUser us   ON  MTb.CreateBy =us.UserId  
              left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId  	  ORDER BY MTb.ExeOfficeDocCategory DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetCheckPartInfo(string ComID)
        {
            string query = @"SELECT UPPER(ExeOfficeDocCategory) as ExeOfficeDocCategory  FROM dbo.tblExeOfficeDocCategory  with(nolock)
                   where  ExeOfficeDocCategory='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheck(string ComID)
        {
            string query = @"Select ExeOfficeDocCatId from tblExeOffiDocUpMaster where ExeOfficeDocCatId='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheckPartInfo2(string ComID, string MasterdId)
        {
            string query = @"SELECT UPPER(ExeOfficeDocCategory) as ExeOfficeDocCategory FROM dbo.tblExeOfficeDocCategory  with(nolock)
                      where  ExeOfficeDocCategory ='" + ComID + "' AND ExeOfficeDocCatId not in (" + MasterdId + ")";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }
    }
    
}
