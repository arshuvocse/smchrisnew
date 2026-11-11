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

namespace DAL.MasterSetup_DAL
{
    public class DesignationStepInformationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public Int32 SaveDesignationStepInfo(DesignationStepInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesignationStepName", aInformationDao.DesignationStepName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            string insertQuery = @"INSERT INTO tblDesignationStep (DesignationStepName,Remarks,IsActive,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@DesignationStepName,@Remarks,@IsActive,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        //View

        public DataTable GetDesignationStepInformation()
        {
            string query = @"SELECT * FROM tblDesignationStep";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");            
        }

        public DataTable GetDesignationStepInformationById(Int32 designationStepId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@designationStepId", designationStepId));

            string query = @"SELECT * FROM tblDesignationStep WHERE DesignationStepId = @designationStepId";
            return aCommonInternalDal.DataContainerDataTable(query,aSqlParameterlist,"HRDB");    
        }

        public bool UpdateDesignationStepInfo(DesignationStepInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DesignationStepName", aInformationDao.DesignationStepName));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            string updateQuery = @"UPDATE tblDesignationStep SET DesignationStepName = @DesignationStepName,Remarks = @Remarks,IsActive = @IsActive ,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE DesignationStepId = '" + aInformationDao.DesignationStepId + "'";

            return aCommonInternalDal.UpdateDataByUpdateCommand(updateQuery, aSqlParameterlist, "HRDB");
        }

        public bool DeleteDesgInfoById(string designationStepId)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@designationStepId", designationStepId));

            string query = @"DELETE FROM tblDesignationStep WHERE DesignationStepId = @designationStepId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }
    }
}
