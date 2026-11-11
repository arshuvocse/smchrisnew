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
    public class EducationRequirementsDetailDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public Int32 EducationRequirementsDetailSave(EducationRequirementsDetailDao aEducationRequirementsDetailDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@MasterId", aEducationRequirementsDetailDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@WayId", aEducationRequirementsDetailDao.WayId));
            aSqlParameterlist.Add(new SqlParameter("@Nos", aEducationRequirementsDetailDao.Nos));

            string insertQuery = @"INSERT INTO dbo.tblEducationRequirementsDetail
                                            ( MasterId, WayId, Nos)
                                    VALUES  ( @MasterId, @WayId, @Nos)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }
    }
}
