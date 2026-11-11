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
    public class SectionInformationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
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

        public void GetDivisionListIntoDropdown(DropDownList ddl)
        {
            const string queryStr = @"SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }
        public Int32 SaveSectionInfo(SectionInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aInformationDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@SectionName", aInformationDao.SectionName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@Root", aInformationDao.Root));

            const string insertQuery = @"INSERT INTO tblSection (CompanyId,DepartmentId,SectionName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus,Root)
                                   VALUES (@CompanyId, @DepartmentId,@SectionName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus,@Root)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public DataTable GetDepartmentRelaton(string id, string param)
        {
            string queryStr = @"SELECT tblDivisionWing.Invisible,* FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND DepartmentId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public void GetDivisionWingListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetDepartmentByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveSectionInfoDEL(SectionInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aInformationDao.SectionId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aInformationDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@SectionName", aInformationDao.SectionName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@Root", aInformationDao.Root));

            const string insertQuery = @"INSERT INTO DELtblSection (SectionId,CompanyId,DepartmentId,SectionName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus,Root)
                                   VALUES (@SectionId,@CompanyId, @DepartmentId,@SectionName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus,@Root)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
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
            aSqlParameterlist.Add(new SqlParameter("@Invisible", aInformationDao.Invisible));

            const string insertQuery = @"INSERT INTO tblDepartment (CompanyId,DivisionWId,DepartmentName,ShortName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus,Invisible)
                                   VALUES (@CompanyId, @DivisionWId,@DepartmentName,@ShortName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus,@Invisible)";

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
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDepartmentList(DropDownList ddl, string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            string queryStr = "SELECT DepartmentId,DepartmentName FROM tblDepartment WHERE IsActive = 'True' AND DivisionWId = @wingId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr,aSqlParameterlist,"HRDB");
        }

        public DataTable GetSectionInformation(string param)
        {
            string queryStr = @"									 SELECT (CASE WHEN DPT.Invisible IS NULL OR DPT.Invisible='False' THEN DPT.DepartmentName ELSE NULL END)DepartmentName
	,(CASE WHEN DVW.Invisible IS NULL OR DVW.Invisible='False' THEN DVW.DivisionWingName ELSE NULL END)DivisionWingName,* FROM tblSection AS SEC
                               INNER JOIN tblDepartment AS DPT ON SEC.DepartmentId = DPT.DepartmentId 
                               INNER JOIN tblDivisionWing AS DVW ON DPT.DivisionWId = DVW.DivisionWId
							   INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = SEC.CompanyId 
                               INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId WHERE (SEC.Invisible IS NULL OR SEC.Invisible='0') "+param+"";
            return aCommonInternalDal.DataContainerDataTable(queryStr,"HRDB");
        }

        public DataTable GetSectionInformationById(string sectionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@sectionId", sectionId));

            const string queryStr = @"SELECT DV.CompanyId,* FROM tblSection AS SEC
                               INNER JOIN tblDepartment AS DPT ON SEC.DepartmentId = DPT.DepartmentId 
                               INNER JOIN tblDivisionWing AS DVW ON DPT.DivisionWId = DVW.DivisionWId
                               INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId WHERE SEC.SectionId = @sectionId";

            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist,"HRDB");
        }

        public bool UpdateSectionInfo(SectionInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@SectionId", aInformationDao.SectionId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aInformationDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@SectionName", aInformationDao.SectionName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@Root", aInformationDao.Root));

            const string queryStr = @"UPDATE tblSection SET CompanyId=@CompanyId, DepartmentId = @DepartmentId,SectionName = @SectionName,ShortName = @ShortName,
                                      IsActive = @IsActive,Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,Root=@Root WHERE SectionId = @SectionId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
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

            const string queryStr = @"UPDATE tblDepartment SET CompanyId=@CompanyId, DivisionWId = @DivisionWId,DepartmentName = @DepartmentName,ShortName = @ShortName,
                                    IsActive = @IsActive,Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE DepartmentId = @DepartmentId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable SectionAllocatedOrNot(string sectionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SectionId", sectionId));

            const string queryStr = @"SELECT * FROM tblSubSection WHERE SectionId = @sectionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteSectionInfoById(string sectionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SectionId", sectionId));

            const string queryStr = @"DELETE FROM tblSection WHERE SectionId = @sectionId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}
