using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
  public  class DesignationTypeDAL
    {

        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveSalaryLocationInfo(DesignationTypeDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesigTypeName", aInformationDao.DesigTypeName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblDesignationType (DesigTypeName,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@DesigTypeName,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public int SaveDesignationWiseKPISetupInfo(DesignationWiseKPISetupDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aInformationDao.DesignationId));
           
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @" Declare @Count Int

select @Count=count(*) from tblDesignationWiseKPISetup where DesignationId=LTRIM(RTRIM(@DesignationId))
 
 if(@Count=0)

INSERT INTO [dbo].[tblDesignationWiseKPISetup]
           ([DesignationId],
            IsActive
           ,[EntryBy]
           ,[EntryDate]
          )
     VALUES
           (@DesignationId 
           
           ,1
           ,@EntryBy
           ,@EntryDate
         )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable CheckLocationExistOrNot(string DesigTypeName)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DesigTypeName", DesigTypeName));

            const string queryStr = @"SELECT * FROM tblDesignationType WHERE DesigTypeName = @DesigTypeName";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetSalaryLocationInformation()
        {
            const string queryStr = @"SELECT * FROM tblDesignationType ORDER BY  DesignationTypeId DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetDesignationWiseKPISetupInformation()
        {
            const string queryStr = @"select dgs.Designation, usentry.UserName EntryUser,useUp.UserName upDateUser,  mas.* from tblDesignationWiseKPISetup mas
left join tblDesignation dgs on dgs.DesignationId=mas.DesignationId
left join tblUser usentry on usentry.UserId=mas.EntryBy
left join tblUser useUp on useUp.UserId=mas.UpdateBy ORDER BY  dgs.Designation asc";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetSalaryLocationInformationById(string DesignationTypeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", DesignationTypeId));

            const string queryStr = @"SELECT * FROM tblDesignationType WHERE DesignationTypeId = @DesignationTypeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetDesignationWiseKPISetupById(string DesignationTypeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", DesignationTypeId));

            const string queryStr = @"SELECT * FROM tblDesignationWiseKPISetup WHERE DesignationWiseKPIId = @DesignationTypeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateSalaryLocationInfo(DesignationTypeDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", aInformationDao.DesignationTypeId));
            aSqlParameterlist.Add(new SqlParameter("@DesigTypeName", aInformationDao.DesigTypeName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblDesignationType SET DesigTypeName = @DesigTypeName,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE DesignationTypeId = @DesignationTypeId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool UpdateDesignationWiseKPISetupInfo(DesignationWiseKPISetupDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesignationWiseKPIId", aInformationDao.DesignationWiseKPIId));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aInformationDao.DesignationId));
          
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"
Declare @Count Int

select @Count=count(*) from tblDesignationWiseKPISetup where  DesignationId=LTRIM(RTRIM(@DesignationId)) and DesignationWiseKPIId not in (@DesignationWiseKPIId)
 print @Count
 if(@Count=0) 

 UPDATE [dbo].[tblDesignationWiseKPISetup]
   SET [DesignationId] = @DesignationId 
       
      ,[UpdateBy] = @UpdateBy 
      ,[UpdateDate] = @UpdateDate 
 WHERE DesignationWiseKPIId=@DesignationWiseKPIId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable RegionAllocatedOrNot(string regionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionId", regionId));

            const string queryStr = @"SELECT * FROM tblArea WHERE RegionId = @RegionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteSalaryLocationInfoById(string salaryLoationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", salaryLoationId));

            const string queryStr = @"DELETE FROM tblDesignationType WHERE DesignationTypeId = @DesignationTypeId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable SalaryGradeAllocatedOrNot(string DesignationTypeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", DesignationTypeId));

            const string queryStr = @"SELECT * FROM tblSalaryGrade WHERE DesignationTypeId = @DesignationTypeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}
