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

namespace DAL.ExecutiveOfficeDocDal
{
  public  class ExeOfficeDocSubcategoryDal
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

        public int SaveEntryInfo(ExeOffDocSubCategoryDao aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCatId", aVacancyEntryDao.ExeOfficeDocCatId));
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocSubCate", aVacancyEntryDao.ExeOfficeDocSubCate));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aVacancyEntryDao.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aVacancyEntryDao.CreateDate));
            const string queryStr = @"INSERT INTO tblExeOfficeDocSubCategory (ExeOfficeDocCatId,ExeOfficeDocSubCate,CreateBy,CreateDate)
                                   VALUES (@ExeOfficeDocCatId,@ExeOfficeDocSubCate,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocSubCatId", MId));
            const string queryStr = @"SELECT * FROM tblExeOfficeDocSubCategory WHERE ExeOfficeDocSubCatId = @ExeOfficeDocSubCatId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(ExeOffDocSubCategoryDao aUpdateDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocSubCatId", aUpdateDao.ExeOfficeDocSubCatId));
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocCatId", aUpdateDao.ExeOfficeDocCatId));
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocSubCate", aUpdateDao.ExeOfficeDocSubCate));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aUpdateDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aUpdateDao.UpdateDate));
            const string queryStr = @"UPDATE tblExeOfficeDocSubCategory SET ExeOfficeDocCatId=@ExeOfficeDocCatId ,ExeOfficeDocSubCate = @ExeOfficeDocSubCate, UpdateBy = @UpdateBy,UpdateDate=@UpdateDate
                                    WHERE ExeOfficeDocSubCatId = @ExeOfficeDocSubCatId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ExeOfficeDocSubCatId", Id));

            const string queryStr = @"DELETE FROM tblExeOfficeDocSubCategory  WHERE ExeOfficeDocSubCatId = @ExeOfficeDocSubCatId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetEntryformation()
        {
            string queryStr = @"SELECT us.UserName as EntryBy, usUp.UserName as UpdateBy,C.ExeOfficeDocCategory, * FROM tblExeOfficeDocSubCategory MTb
LEFT JOIN tblExeOfficeDocCategory C ON C.ExeOfficeDocCatId = MTb.ExeOfficeDocCatId
LEFT JOIN  dbo.tblUser us   ON  MTb.CreateBy =us.UserId  
LEFT JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId order by C.ExeOfficeDocCategory asc";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetCheckPartInfo(string ComID, string subCat)
        {
            string query = @"SELECT ExeOfficeDocCatId  FROM dbo.tblExeOfficeDocSubCategory  with(nolock)
                   where  ExeOfficeDocCatId='" + ComID + "' and LTRIM(RTRIM(ExeOfficeDocSubCate))='" + subCat+"'";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheck(string ComID)
        {
            string query = @"Select ExeOfficeDocSubCatId from tblExeOfficeDocSubCategory where ExeOfficeDocSubCatId='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheckPartInfo2(string ComID,String subCat, string MasterdId)
        {
            string query = @"SELECT ExeOfficeDocCatId FROM dbo.tblExeOfficeDocSubCategory  with(nolock)
                      where  ExeOfficeDocCatId ='" + ComID + "' and LTRIM(RTRIM(ExeOfficeDocSubCate))='"+subCat+"' AND ExeOfficeDocSubCatId not in (" + MasterdId + ")";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }
    }
}
