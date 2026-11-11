using System.Web;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DAL.MasterSetup_DAL
{
  public  class ProjectSetupDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public Int32 SaveProjectSet(ProjectSetupDao aProjectDao)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@CompanyId", aProjectDao.CompanyId));
            aSqlParameterList.Add(new SqlParameter("@ProjectName", aProjectDao.ProjectName));
            aSqlParameterList.Add(new SqlParameter("@ProjectStartDate", aProjectDao.ProjectStartDate));
            aSqlParameterList.Add(new SqlParameter("@ProjectEndDate", aProjectDao.ProjectEndDate ?? (object)DBNull.Value));
           
            aSqlParameterList.Add(new SqlParameter("@Parmanent", aProjectDao.Parmanent));
            aSqlParameterList.Add(new SqlParameter("@Temporary", aProjectDao.Temporary));
            aSqlParameterList.Add(new SqlParameter("@ProjectDescription", aProjectDao.ProjectDescription));
            aSqlParameterList.Add(new SqlParameter("@Remarks", aProjectDao.Remarks));
            aSqlParameterList.Add(new SqlParameter("@EntryBy", aProjectDao.EntryBy));
            aSqlParameterList.Add(new SqlParameter("@EntryDate", aProjectDao.EntryDate));
            aSqlParameterList.Add(new SqlParameter("@IsActive", aProjectDao.IsActive));

 aSqlParameterList.Add(new SqlParameter("@IsOtherProject", aProjectDao.IsOtherProject ?? (object)DBNull.Value));

            aSqlParameterList.Add(new SqlParameter("@IsSMCContract", aProjectDao.IsSMCContract ?? (object)DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@IsCompanyDirector", aProjectDao.IsCompanyDirector ?? (object)DBNull.Value));
              aSqlParameterList.Add(new SqlParameter("@IsSMCFundedProjects", aProjectDao.IsSMCFundedProjects ?? (object)DBNull.Value));




              string inserQuery = @"INSERT INTO tblProjectSetup(CompanyId,ProjectName,ProjectStartDate,ProjectEndDate,Parmanent,Temporary,ProjectDescription,Remarks,EntryBy,EntryDate, IsActive,[IsOtherProject]
      ,[IsSMCFundedProjects]
      ,[IsSMCContract]
      ,[IsCompanyDirector]) 
             VALUES(@CompanyId,@ProjectName,@ProjectStartDate,@ProjectEndDate,@Parmanent,@Temporary,@ProjectDescription,@Remarks,@EntryBy,@EntryDate, @IsActive,@IsOtherProject 
      ,@IsSMCFundedProjects 
      ,@IsSMCContract 
      ,@IsCompanyDirector )";

            return aCommonInternalDal.SaveDataByInsertCommandById(inserQuery, aSqlParameterList, "HRDB");
        }

        public Int32 SaveProjectSetDEL(ProjectSetupDao aProjectDao)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@ProjectId", aProjectDao.ProjectId));
            aSqlParameterList.Add(new SqlParameter("@CompanyId", aProjectDao.CompanyId));
            aSqlParameterList.Add(new SqlParameter("@ProjectName", aProjectDao.ProjectName));
            aSqlParameterList.Add(new SqlParameter("@ProjectStartDate", aProjectDao.ProjectStartDate));
            aSqlParameterList.Add(new SqlParameter("@ProjectEndDate", aProjectDao.ProjectEndDate ?? (object)DBNull.Value));

            aSqlParameterList.Add(new SqlParameter("@Parmanent", aProjectDao.Parmanent));
            aSqlParameterList.Add(new SqlParameter("@Temporary", aProjectDao.Temporary));
            aSqlParameterList.Add(new SqlParameter("@ProjectDescription", aProjectDao.ProjectDescription));
            aSqlParameterList.Add(new SqlParameter("@Remarks", aProjectDao.Remarks));
            aSqlParameterList.Add(new SqlParameter("@EntryBy", aProjectDao.EntryBy));
            aSqlParameterList.Add(new SqlParameter("@EntryDate", aProjectDao.EntryDate));
            aSqlParameterList.Add(new SqlParameter("@IsActive", aProjectDao.IsActive));


            aSqlParameterList.Add(new SqlParameter("@IsOtherProject", aProjectDao.IsOtherProject ?? (object)DBNull.Value));

            aSqlParameterList.Add(new SqlParameter("@IsSMCContract", aProjectDao.IsSMCContract ?? (object)DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@IsCompanyDirector", aProjectDao.IsCompanyDirector ?? (object)DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@IsSMCFundedProjects", aProjectDao.IsSMCFundedProjects ?? (object)DBNull.Value));


            string inserQuery = @"INSERT INTO DELtblProjectSetup(ProjectId,CompanyId,ProjectName,ProjectStartDate,ProjectEndDate,Parmanent,Temporary,ProjectDescription,Remarks,EntryBy,EntryDate, IsActive,[IsOtherProject]
      ,[IsSMCFundedProjects]
      ,[IsSMCContract]
      ,[IsCompanyDirector]) 
             VALUES(@ProjectId, @CompanyId,@ProjectName,@ProjectStartDate,@ProjectEndDate,@Parmanent,@Temporary,@ProjectDescription,@Remarks,@EntryBy,@EntryDate, @IsActive,@IsOtherProject 
      ,@IsSMCFundedProjects 
      ,@IsSMCContract 
      ,@IsCompanyDirector )";

            return aCommonInternalDal.SaveDataByInsertCommandById(inserQuery, aSqlParameterList, "HRDB");
        }
        public void GetCompanyListIntoDropdown(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

        //public DataTable GetProjectSetup()
        //{
        //    const string queryStr = @"SELECT * tblProjectSetup";
        //    return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        //}

        public DataTable GetProjectSetup(string param)
        {
             string queryStr = @" SELECT  com.ShortName,* FROM tblProjectSetup PS
 INNER JOIN dbo.tblCompanyInfo  com ON PS.CompanyId=com.CompanyId
  " + param + " ORDER BY PS.ProjectId DESC";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetProjectSetupById(string projectId)
        {
            var aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter(@"projectId", projectId));

            const string queryStr = @"SELECT * FROM tblProjectSetup WHERE ProjectId =@ProjectId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterList, "HRDB");
        }

        public bool UpdateProjectSetup(ProjectSetupDao aProjectDao)
        {
            var aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@ProjectId", aProjectDao.ProjectId));
            aSqlParameterList.Add(new SqlParameter("@CompanyId", aProjectDao.CompanyId));
            aSqlParameterList.Add(new SqlParameter("@ProjectName", aProjectDao.ProjectName));
            aSqlParameterList.Add(new SqlParameter("@ProjectStartDate", aProjectDao.ProjectStartDate));
            aSqlParameterList.Add(new SqlParameter("@ProjectEndDate", aProjectDao.ProjectEndDate ?? (object)DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@Parmanent", aProjectDao.Parmanent));
            aSqlParameterList.Add(new SqlParameter("@Temporary", aProjectDao.Temporary));
            aSqlParameterList.Add(new SqlParameter("@ProjectDescription", aProjectDao.ProjectDescription));
            aSqlParameterList.Add(new SqlParameter("@Remarks", aProjectDao.Remarks));
            aSqlParameterList.Add(new SqlParameter("@UpdateBy", aProjectDao.UpdateBy));
            aSqlParameterList.Add(new SqlParameter("@IsActive", aProjectDao.IsActive));
            aSqlParameterList.Add(new SqlParameter("@UpdateDate", aProjectDao.UpdateDate));

            aSqlParameterList.Add(new SqlParameter("@IsOtherProject", aProjectDao.IsOtherProject ?? (object)DBNull.Value));

            aSqlParameterList.Add(new SqlParameter("@IsSMCContract", aProjectDao.IsSMCContract ?? (object)DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@IsCompanyDirector", aProjectDao.IsCompanyDirector ?? (object)DBNull.Value));
            aSqlParameterList.Add(new SqlParameter("@IsSMCFundedProjects", aProjectDao.IsSMCFundedProjects ?? (object)DBNull.Value));
            const string queryStr = @"UPDATE tblProjectSetup SET CompanyId=@CompanyId, ProjectName=@ProjectName, ProjectStartDate=@ProjectStartDate,ProjectEndDate=@ProjectEndDate,Parmanent=@Parmanent, Temporary=@Temporary,ProjectDescription=@ProjectDescription,Remarks=@Remarks,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate, IsActive=@IsActive ,[IsOtherProject] = @IsOtherProject 
      ,[IsSMCFundedProjects] = @IsSMCFundedProjects 
      ,[IsSMCContract] = @IsSMCContract 
      ,[IsCompanyDirector] = @IsCompanyDirector  WHERE ProjectId=@ProjectId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterList, "HRDB");
        }
        public bool DeleteProjectSetupById(string projectId)
        {
            var aSqlParameterList = new List<SqlParameter>();
            aSqlParameterList.Add(new SqlParameter("@ProjectId", projectId));

            const string queryStr = "DELETE FROM tblProjectSetup WHERE ProjectId =@ProjectId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterList, "HRDB");
        }
    }
}
