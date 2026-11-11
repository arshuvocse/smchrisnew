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
    public class EmployeeTypeEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(EmployeeTypeEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpType", aVacancyEntryDao.EmpType));
            aSqlParameterlist.Add(new SqlParameter("@CompanyID", aVacancyEntryDao.CompanyID));
        
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblEmployeeType (EmpType, CompanyID, IsActive,EntryBy,EntryDate)
                                   VALUES (@EmpType, @CompanyID, @IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public int DelSaveVacancyEntryInfo(EmployeeTypeEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aVacancyEntryDao.EmpTypeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpType", aVacancyEntryDao.EmpType));
            aSqlParameterlist.Add(new SqlParameter("@CompanyID", aVacancyEntryDao.CompanyID));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblEmployeeType ( EmpTypeId, EmpType, CompanyID, IsActive,EntryBy,EntryDate)
                                   VALUES (@EmpTypeId, @EmpType, @CompanyID, @IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetCompanyListShortNameIntoDropdown(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
        public DataTable GetVacaencyInformationById(string VacancyCirculationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", VacancyCirculationId));

            const string queryStr = @"SELECT * FROM tblEmployeeType WHERE EmpTypeId = @EmpTypeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(EmployeeTypeEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aVacancyEntryDao.EmpTypeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpType", aVacancyEntryDao.EmpType));
            aSqlParameterlist.Add(new SqlParameter("@CompanyID", aVacancyEntryDao.CompanyID));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aVacancyEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aVacancyEntryDao.UpdateDate));



            const string queryStr = @"UPDATE tblEmployeeType SET EmpType = @EmpType, CompanyID=@CompanyID, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive
                                   WHERE EmpTypeId = @EmpTypeId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteVacancyEntryfoById(EmployeeTypeEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@VacancyCirculationId", aVacancyEntryDao.VacancyCirculationId));
            //aSqlParameterlist.Add(new SqlParameter("@IsDelete", aVacancyEntryDao.IsDelete));
            //aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aVacancyEntryDao.DeleteBy));
            //aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aVacancyEntryDao.DeleteDate));

            const string queryStr = @"UPDATE tblEmployeeType SET IsDelete = @IsDelete,DeleteBy = @DeleteBy, DeleteDate=@DeleteDate WHERE EmpTypeId = @EmpTypeId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }


        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", Id));


            const string queryStr = @"DELETE FROM tblEmployeeType  WHERE EmpTypeId = @EmpTypeId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }


        public DataTable GetVacanceyEntryformationParam( )
        {
            string queryStr = @"SELECT   us.UserName EntryBy, usUp.UserName UpdateBy,*, dbo.tblCompanyInfo.ShortName FROM tblEmployeeType 
LEFT JOIN dbo.tblCompanyInfo ON dbo.tblEmployeeType.CompanyID=dbo.tblCompanyInfo.CompanyId
left JOIN  dbo.tblUser us   ON  tblEmployeeType.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  tblEmployeeType.UpdateBy =usUp.UserId
 WHERE tblEmployeeType.IsActive =1";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
