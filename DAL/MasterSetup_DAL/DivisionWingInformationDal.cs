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
    public class DivisionWingInformationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void GetCompanyListIntoDropdown(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
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

            const string insertQuery = @"INSERT INTO tblDivisionWing (CompanyId,DivisionId,DivisionWingName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@CompanyId,@DivisionId,@DivisionWingName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDivisionWingInfoDEL(DivisionWingInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aInformationDao.DivisionWId));

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

            const string insertQuery = @"INSERT INTO DELtblDivisionWing (DivisionWId,CompanyId,DivisionId,DivisionWingName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@DivisionWId,@CompanyId,@DivisionId,@DivisionWingName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionList(DropDownList ddl, string companyId)
        {
            string queryStr = @"SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True' AND CompanyId = '" + companyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }


        public DataTable GetDivisionWingInformation()
        {
            const string queryStr = @"SELECT * FROM tblDivisionWing AS DVW 
                              INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId WHERE (DVW.Invisible IS NULL OR DVW.Invisible='0')";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetDivisionWingInformationParam(string param)
        {
              string queryStr = @"SELECT * FROM tblDivisionWing AS DVW 
                              INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = DVW.CompanyId  WHERE (DVW.Invisible IS NULL OR DVW.Invisible='0') " + param + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public bool DeleteDivisionWingInfoById(string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            const string queryStr = @"DELETE FROM tblDivisionWing WHERE DivisionWId = @wingId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetDivisionInformationById(string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            const string queryStr = @"SELECT * FROM tblDivisionWing AS DVW 
                              INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId WHERE DivisionWId = @wingId";
            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist, "HRDB");
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

        public DataTable WingAllocatedOrNot(string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            const string queryStr = @"SELECT * FROM tblDepartment WHERE DivisionWId = @wingId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionListIntoDropdown(DropDownList ddl)
        {
            const string queryStr = @"SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }
        public void GetDivisionListIntoDropdownByCom(DropDownList ddl,string companyId)
        {
            string queryStr = @"SELECT DivisionId,DivisionName FROM tblDivision WHERE CompanyId='"+companyId+"' AND  IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }
        public void GetComapnyNameList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";

            //string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
    }
}
