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
    public class DivisionInformationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void GetCompanyListIntoDropdown(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

        public void GetComapnyNameList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";

            //string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
        public Int32 SaveDivisionInfo(DivisionInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionName", aInformationDao.DivisionName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string insertQuery = @"INSERT INTO tblDivision (CompanyId,DivisionName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@CompanyId,@DivisionName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDivisionInfoForDele(DivisionInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aInformationDao.DivisionId));

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionName", aInformationDao.DivisionName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string insertQuery = @"INSERT INTO DELtblDivision (DivisionId,CompanyId,DivisionName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@DivisionId,@CompanyId,@DivisionName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable GetDivisionInformation(string param)
        {
            string query = @"SELECT di.DivisionId, com.ShortName, di.DivisionName, di.ShortName DivShortName,  di.IsActive, di.ApprovalStatus, di.Description, di.Remarks, di.EntryBy, di.EntryDate, di.UpdateBy ,di.UpdateDate FROM tblDivision di

INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = di.CompanyId "+param+"";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetDivisionInformationById(string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@divisionId", divisionId));

            const string query = @"SELECT * FROM tblDivision WHERE DivisionId = @divisionId";           
            return aCommonInternalDal.DataContainerDataTable(query,aSqlParameterlist, "HRDB");
        }

        public bool UpdateDivisionInfo(DivisionInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aInformationDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionName", aInformationDao.DivisionName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string query = @"UPDATE tblDivision SET CompanyId = @CompanyId,DivisionName = @DivisionName,ShortName = @ShortName,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE DivisionId = @DivisionId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public bool DeleteDivisionInfoById(string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            const string query = @"DELETE FROM tblDivision WHERE DivisionId = @divisionId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }

        public DataTable DivisionAllocatedOrNot(string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@divisionId", divisionId));

            const string queryStr = @"SELECT * FROM tblDivisionWing WHERE DivisionId = @divisionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}
