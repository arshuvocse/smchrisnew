using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class SuspendReasonEntryDaL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void GetComapnyNameList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";

            //string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
        public int SaveVacancyEntryInfo(SuspendReasonEntryDao aSuspendReasonEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SuspendReasonEntry", aSuspendReasonEntryDao.SuspendReasonEntry));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspendReasonEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aSuspendReasonEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aSuspendReasonEntryDao.EntryDate));

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aSuspendReasonEntryDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@IsSuspend", aSuspendReasonEntryDao.IsSuspend));
            aSqlParameterlist.Add(new SqlParameter("@IsDisciplinary", aSuspendReasonEntryDao.IsDisciplinary));


            const string queryStr = @"INSERT INTO tblSuspendReasonEntry (SuspendReasonEntry,IsActive,EntryBy,EntryDate, CompanyId, IsSuspend, IsDisciplinary)
                                   VALUES (@SuspendReasonEntry,@IsActive,@EntryBy,@EntryDate, @CompanyId, @IsSuspend, @IsDisciplinary)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetInformation(string parm)
        {
            string queryStr = @"SELECT  *,case
  WHEN re.IsSuspend = 1   THEN 'Yes' 
  
  WHEN re.IsSuspend = 0   THEN 'No'  END AS YSuspend,
  
   
  case
   WHEN re.IsDisciplinary = 1   THEN 'Yes' 
  
  WHEN  re.IsDisciplinary = 0   THEN 'No'  END AS YDisciplinary
 FROM tblSuspendReasonEntry  re
 INNER JOIN  dbo.tblCompanyInfo com ON re.CompanyId=com.CompanyId " + parm;

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetVacaencyInformationById(string SuspendReasonEntryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SuspendReasonEntryId", SuspendReasonEntryId));

            const string queryStr = @"SELECT * FROM tblSuspendReasonEntry WHERE SuspendReasonEntryId = @SuspendReasonEntryId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(SuspendReasonEntryDao aSuspendReasonEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SuspendReasonEntryId", aSuspendReasonEntryDao.SuspendReasonEntryId));
            aSqlParameterlist.Add(new SqlParameter("@SuspendReasonEntry", aSuspendReasonEntryDao.SuspendReasonEntry));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aSuspendReasonEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aSuspendReasonEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspendReasonEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aSuspendReasonEntryDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@IsSuspend", aSuspendReasonEntryDao.IsSuspend));
            aSqlParameterlist.Add(new SqlParameter("@IsDisciplinary", aSuspendReasonEntryDao.IsDisciplinary));

            const string queryStr = @"UPDATE tblSuspendReasonEntry SET SuspendReasonEntry = @SuspendReasonEntry, UpdateBy = @UpdateBy, UpdateDate = @UpdateDate, IsActive = @IsActive, 
CompanyId=@CompanyId, IsSuspend=@IsSuspend, IsDisciplinary=@IsDisciplinary
                                    WHERE SuspendReasonEntryId = @SuspendReasonEntryId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteVacancyEntryfoById(SuspendReasonEntryDao aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SuspendReasonEntryId", aVacancyEntryDao.SuspendReasonEntryId));
         
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aVacancyEntryDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aVacancyEntryDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aVacancyEntryDao.DeleteDate));

            const string queryStr = @"UPDATE tblSuspendReasonEntry SET IsDelete = @IsDelete,DeleteBy = @DeleteBy, DeleteDate=@DeleteDate WHERE SuspendReasonEntryId = @SuspendReasonEntryId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetVacanceyEntryformationParam(string param)
        {
            string queryStr = @"SELECT com.ShortName, tblUser.UserName  EntryBy, us.UserName UpdateBy , * FROM tblSuspendReasonEntry
INNER JOIN  dbo.tblCompanyInfo com ON  tblSuspendReasonEntry.CompanyId =com.CompanyId 
left JOIN  dbo.tblUser   ON  tblSuspendReasonEntry.EntryBy =dbo.tblUser.UserId 
left JOIN  dbo.tblUser  us ON tblSuspendReasonEntry.UpdateBy =us.UserId 
 where  (  IsDelete IS NULL OR  IsDelete = 0)" + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
