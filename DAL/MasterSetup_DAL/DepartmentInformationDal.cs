using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace DAL.MasterSetup_DAL
{
    public class DepartmentInformationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public void GetComapnyNameList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";

            //string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
        public void GetCompanyListIntoDropdown(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
      
        public DataTable lunchHCLAst(int id)
        {
            try
            {
                string query = @"select * from tblLunchTimeSet where LunchTimeSetId=" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void GetDivisionListIntoDropdown(DropDownList ddl)
        {
            const string queryStr = @"SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }

        public Int32 SaveDepartmentInfo(DepartmentInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aInformationDao.DivisionWId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentName", aInformationDao.DepartmentName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@Root", aInformationDao.Root));

            const string insertQuery = @"INSERT INTO tblDepartment (CompanyId,DivisionWId,DepartmentName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus,Root)
                                   VALUES (@CompanyId, @DivisionWId,@DepartmentName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus,@Root)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }


        public Int32 SaveDepartmentInfoDEL(DepartmentInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aInformationDao.DepartmentId));

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aInformationDao.DivisionWId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentName", aInformationDao.DepartmentName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@Root", aInformationDao.Root));

            const string insertQuery = @"INSERT INTO DELtblDepartment (DepartmentId,CompanyId,DivisionWId,DepartmentName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus,Root)
                                   VALUES (@DepartmentId,@CompanyId, @DivisionWId,@DepartmentName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus,@Root)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveDivisionWingInfo(DivisionWingInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aInformationDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWingName", aInformationDao.DivisionWingName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@Invisible", aInformationDao.Invisible));

            const string insertQuery = @"INSERT INTO tblDivisionWing (CompanyId,DivisionId,DivisionWingName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus,Invisible)
                                   VALUES (@CompanyId,@DivisionId,@DivisionWingName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus,@Invisible)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionList(DropDownList ddl, string companyId)
        {
            string queryStr = "SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True' AND CompanyId = '" + companyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }

        public void GetDivisionWingList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr,aSqlParameterlist, "HRDB");
        }

        public DataTable GetDepartmentInformation()
        {
            const string queryStr = @"SELECT (CASE WHEN DVW.Invisible IS NULL OR DVW.Invisible='False' THEN DVW.DivisionWingName ELSE NULL END)DivisionWingName,* FROM tblDepartment AS DPT 
                                      INNER JOIN tblDivisionWing AS DVW ON DPT.DivisionWId = DVW.DivisionWId
                                     INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId WHERE (DPT.Invisible IS NULL OR DPT.Invisible='0')";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetDepartmentInformationParam(string param)
        {
            string queryStr = @"SELECT (CASE WHEN DVW.Invisible IS NULL OR DVW.Invisible='False' THEN DVW.DivisionWingName ELSE NULL END)DivisionWingName,* FROM tblDepartment AS DPT 
                                      INNER JOIN tblDivisionWing AS DVW ON DPT.DivisionWId = DVW.DivisionWId
									   INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = DPT.CompanyId 
                                     INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId WHERE (DPT.Invisible IS NULL OR DPT.Invisible='0') " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetDepartmentInformationById(string departmentId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@departmentId", departmentId));

            const string queryStr = @"SELECT DV.CompanyId,* FROM tblDepartment AS DPT 
                                      INNER JOIN tblDivisionWing AS DVW ON DPT.DivisionWId = DVW.DivisionWId
                                     INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId WHERE DepartmentId = @departmentId";
            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist, "HRDB");
        }

        public bool UpdateDepartmentInfo(DepartmentInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aInformationDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aInformationDao.DivisionWId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentName", aInformationDao.DepartmentName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@Root", aInformationDao.Root));

            const string queryStr = @"UPDATE tblDepartment SET  DepartmentName = @DepartmentName,ShortName = @ShortName,
                                    IsActive = @IsActive,Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate  WHERE DepartmentId = @DepartmentId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool UpdateDivisionWingInfo(DivisionWingInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aInformationDao.DivisionWId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aInformationDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWingName", aInformationDao.DivisionWingName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblDivisionWing SET CompanyId=@CompanyId, DivisionId = @DivisionId,DivisionWingName = @DivisionWingName,ShortName = @ShortName,IsActive = @IsActive,
                                     Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE DivisionWId = @DivisionWId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable DepartmentAllocatedOrNot(string departmentId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@departmentId", departmentId));

            const string queryStr = @"SELECT * FROM tblSection WHERE DepartmentId = @departmentId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteDepartmentInfoById(string departmentId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@departmentId", departmentId));

            const string queryStr = @"DELETE FROM tblDepartment WHERE DepartmentId = @departmentId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
    }
    
}
