using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.TrainingAndLearningDevelopment_DAL
{
    public class TrainingSetupDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void GetOrgListOnDropdown   (DropDownList ddl)
        {
            const string queryStr = "SELECT OrgTypeId,OrgTypeName FROM tblOrganizationType";
            aCommonInternalDal.LoadDropDownValue(ddl, "OrgTypeName", "OrgTypeId", queryStr, "HRDB");
        }

        public Int32 SaveOrganizationInfo(TrainingOrganizationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingOrgName", aInformationDao.TrainingOrgName));
            aSqlParameterlist.Add(new SqlParameter("@OrgTypeId", aInformationDao.OrgTypeId));
            aSqlParameterlist.Add(new SqlParameter("@OrgAddress", aInformationDao.OrgAddress));
            aSqlParameterlist.Add(new SqlParameter("@IsLocal", aInformationDao.IsLocal));
            aSqlParameterlist.Add(new SqlParameter("@IsForeign", aInformationDao.IsForeign));
            aSqlParameterlist.Add(new SqlParameter("@IsInHouse", aInformationDao.IsInHouse));
            aSqlParameterlist.Add(new SqlParameter("@ConPersonName", aInformationDao.ConPersonName));
            aSqlParameterlist.Add(new SqlParameter("@Email", aInformationDao.Email));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", aInformationDao.PhoneNo));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string insertQuery = @"INSERT INTO tblTrainingOrganization (TrainingOrgName,OrgTypeId,OrgAddress,IsLocal,IsForeign,IsInHouse,ConPersonName,Email,PhoneNo,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@TrainingOrgName,@OrgTypeId,@OrgAddress,@IsLocal,@IsForeign,@IsInHouse,@ConPersonName,@Email,@PhoneNo,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable GetTrainingOrganizationInformation()
        {
            const string query = @"SELECT * FROM tblTrainingOrganization AS ORG 
                                   INNER JOIN tblOrganizationType AS LCT ON ORG.OrgTypeId = LCT.OrgTypeId";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetOrgInformationById(string orgId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterId", orgId));

            const string query = @"SELECT * FROM tblTrainingOrganization WHERE TrainingOrgId = @MasterId";           
            return aCommonInternalDal.DataContainerDataTable(query,aSqlParameterlist, "HRDB");
        }

        public bool UpdateOrganizationInfo(TrainingOrganizationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingOrgId", aInformationDao.TrainingOrgId));
            aSqlParameterlist.Add(new SqlParameter("@TrainingOrgName", aInformationDao.TrainingOrgName));
            aSqlParameterlist.Add(new SqlParameter("@OrgTypeId", aInformationDao.OrgTypeId));
            aSqlParameterlist.Add(new SqlParameter("@OrgAddress", aInformationDao.OrgAddress));
            aSqlParameterlist.Add(new SqlParameter("@IsLocal", aInformationDao.IsLocal));
            aSqlParameterlist.Add(new SqlParameter("@IsForeign", aInformationDao.IsForeign));
            aSqlParameterlist.Add(new SqlParameter("@IsInHouse", aInformationDao.IsInHouse));
            aSqlParameterlist.Add(new SqlParameter("@ConPersonName", aInformationDao.ConPersonName));
            aSqlParameterlist.Add(new SqlParameter("@Email", aInformationDao.Email));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", aInformationDao.PhoneNo));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string query = @"UPDATE tblTrainingOrganization SET TrainingOrgName = @TrainingOrgName,OrgTypeId = @OrgTypeId,OrgAddress = @OrgAddress,IsLocal = @IsLocal,
                                 IsForeign = @IsForeign,IsInHouse = @IsInHouse,ConPersonName = @ConPersonName,Email = @Email,PhoneNo = @PhoneNo,
                                 Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE TrainingOrgId = @TrainingOrgId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public bool DeleteTrainingOrgInfoById(string orgId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@OrgId", orgId));

            bool status = false;
            const string query = @"DELETE FROM tblTrainingOrganization WHERE TrainingOrgId = @OrgId";

            if (aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB"))
            {
                const string queryStr = @"DELETE FROM tblTrainingLocation WHERE MasterId = @OrgId";
                status = aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
            }
 
            return status ;
        }

        public DataTable DivisionAllocatedOrNot(string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@divisionId", divisionId));

            const string queryStr = @"SELECT * FROM tblDivisionWing WHERE DivisionId = @divisionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public int SaveOrganizationLocationInfo(List<TrainingOrganizationLocationDao> aList)
        {
            Int32 id = 0;

            foreach (TrainingOrganizationLocationDao aLocationDao in aList)
            {
                var aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@MasterId", aLocationDao.MasterId));
                aSqlParameterlist.Add(new SqlParameter("@TrainingLocation", aLocationDao.TrainingLocation));

                const string queryStr = @"INSERT INTO tblTrainingLocation (MasterId,TrainingLocation) VALUES (@MasterId,@TrainingLocation)";
                id = aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
            }

            return id;
        }

        public bool DeleteLocationInformation(string masterId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterId", masterId));

            const string query = @"DELETE FROM tblTrainingLocation WHERE MasterId = @MasterId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }

        public DataTable GetLocationDetai(string masterId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterId", masterId));

            const string query = @"SELECT TrainingLocation AS Location FROM tblTrainingLocation  WHERE MasterId = @MasterId";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
        }
    }
    }

