using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.RecruitmentManagement_DAL
{
   public class OtherRequirementDetailDAL
    {
        
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public Int32 OtherRequirementsDetailSave(OtherRequirementDetailDAO aOtherRequirementDetailDAO)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@MasterId", aOtherRequirementDetailDAO.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@OtherRequirement", aOtherRequirementDetailDAO.OtherRequirement));

            string insertQuery = @"INSERT INTO dbo.OtherRequirementDetail
                                            ( OtherRequirement, MasterId)
                                    VALUES  ( @OtherRequirement, @MasterId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }
    
    }
}
