using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class ProjectWiseEmployeeAllocationEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable LoadExistingProject(int id)
        {
            string query = @"SELECT AlDetails.EmployeeWiseProjectAllocationMasterId,  AlDetails.IsActive,PSet.ProjectId as ProjectId, AlDetails.EmpWiseProjectDetailID,Masterid.EmpInfoId, PSet.ProjectName, PSet.ProjectStartDate, PSet.ProjectEndDate FROM tblEmployeeWiseProjectAllocationDetail AlDetails
LEFT JOIN dbo.tblProjectSetup PSet ON AlDetails.ProjectId=PSet.ProjectId
LEFT JOIN dbo.tblEmployeeWiseProjectAllocationMaster Masterid ON AlDetails.EmployeeWiseProjectAllocationMasterId=Masterid.EmpWiseProjectID
							 WHERE Masterid.EmpInfoId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadExistingProjectByTop1(int id)
        {
            string query = @"SELECT top 1 AlDetails.EmployeeWiseProjectAllocationMasterId,  AlDetails.IsActive,PSet.ProjectId as ProjectId, AlDetails.EmpWiseProjectDetailID,Masterid.EmpInfoId, PSet.ProjectName, PSet.ProjectStartDate, PSet.ProjectEndDate FROM tblEmployeeWiseProjectAllocationDetail AlDetails
LEFT JOIN dbo.tblProjectSetup PSet ON AlDetails.ProjectId=PSet.ProjectId
LEFT JOIN dbo.tblEmployeeWiseProjectAllocationMaster Masterid ON AlDetails.EmployeeWiseProjectAllocationMasterId=Masterid.EmpWiseProjectID
							 WHERE Masterid.EmpInfoId='" + id + "'  order by AlDetails.EmpWiseProjectDetailID  desc ";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadNewProject(int id)
        {
            string query = @"SELECT ProjectId AS EmpWiseProjectDetailID,
       ProjectName ,
       ProjectStartDate ,
      
       ProjectEndDate ,
       ProjectDescription 
       FROM tblProjectSetup WHERE   ProjectId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public void LoaProjectDropDownList(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM tblProjectSetup WITH (NOLOCK) WHERE IsActive = 1 and  CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "ProjectName", "ProjectId", query, "HRDB");
        }

        public void LoaProjectByCheckDropDownList(DropDownList ddl, string CompanyId, string parm)
        {
            string query = "SELECT * FROM tblProjectSetup WITH (NOLOCK) WHERE IsActive = 1 and  CompanyId='" + CompanyId + "' " + parm + "";
            aCommonInternalDal.LoadDropDownValue(ddl, "ProjectName", "ProjectId", query, "HRDB");
        }

        public Int32 SaveInfo(ProjectWiseEmployeeAllocationDAO aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));
          
            



            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmpTransferAndDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmpTransferAndDao.EntryDate));
            string insertQuery = @" INSERT INTO [dbo].[tblEmployeeWiseProjectAllocationMaster]
           ([EmpInfoId]
           ,[EntryBy]
           ,[EntryDate])
          
     VALUES
           ( @EmpInfoId, 
            @EntryBy ,
			@EntryDate
         )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }


        public bool UpdateVacancyEntryInfo(ProjectWiseEmployeeAllocationDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmployeeWiseProjectAllocationMasterId", aInformationDao.EmployeeWiseProjectAllocationMasterId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInformationDao.EmpInfoId));





            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"UPDATE tblEmployeeWiseProjectAllocationMaster SET EmpInfoId = @EmpInfoId, EntryBy = @EntryBy,EntryDate = @EntryDate
                                   WHERE EmpWiseProjectID = @EmployeeWiseProjectAllocationMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdatenfoDetail(string DetlId, String ProId, bool IsMaster)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpWiseProjectDetailID", DetlId));
            aSqlParameterlist.Add(new SqlParameter("@ProjectId", ProId));
            aSqlParameterlist.Add(new SqlParameter("@IsMaster", IsMaster));






            const string queryStr = @"update tblEmployeeWiseProjectAllocationDetail set IsMaster=@IsMaster,  ProjectId=@ProjectId where EmpWiseProjectDetailID=@EmpWiseProjectDetailID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteDEtails(string id)
        {
            string query = "DELETE FROM tblEmployeeWiseProjectAllocationDetail WHERE EmployeeWiseProjectAllocationMasterId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public bool DeleteDEtailsProjectId(string id)
        {
            string query = "DELETE FROM tblEmployeeWiseProjectAllocationDetail WHERE ProjectId=0";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public Int32 SaveInfoDetails(ProjectWiseEmployeeAllocationDetailDAO aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeWiseProjectAllocationMasterId", aEmpTransferAndDao.EmployeeWiseProjectAllocationMasterId));

            aSqlParameterlist.Add(new SqlParameter("@ProjectId", aEmpTransferAndDao.ProjectId));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEmpTransferAndDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@IsMaster", aEmpTransferAndDao.IsMaster));

            string insertQuery = @"INSERT INTO [dbo].[tblEmployeeWiseProjectAllocationDetail]
           ([ProjectId]
           ,[EmployeeWiseProjectAllocationMasterId], IsActive, IsMaster)
     VALUES
           (@ProjectId,  
           @EmployeeWiseProjectAllocationMasterId, @IsActive,@IsMaster )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        } 
     
    }
}
