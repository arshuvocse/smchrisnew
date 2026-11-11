using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using DAL.InternalCls;

namespace DAL.COMMON_DAL
{
    public class UserOperationPermissionCredentialsDal
    {
        readonly ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable CheckUserOperationPermissionCredentials(string userId,Int32 manuId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", userId));
            aSqlParameterlist.Add(new SqlParameter("@ManuId", manuId));

            const string queryStr = @"SELECT * FROM tblMenuDistribution WHERE UserId = @UserId AND MenuSL = @ManuId AND IsActive = 'True'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}
