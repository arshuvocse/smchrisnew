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
using  DAL.DataManager;
using DAO.HRIS_DAO;

namespace DAL.COMMON_DAL
{
    public class CompanyHierarchyDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadDivision(DropDownList ddl, string companyId)
        {
            string query = @"SELECT DSN.DivisionId,
                            DSN.DivisionName FROM dbo.tblDivision AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId;
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", query, DataBase.HRDB);
        }
        public void GetDivisionWingList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetDepartmentByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE  tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDepartmentList(DropDownList ddl, string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            string queryStr = "SELECT DepartmentId,DepartmentName FROM tblDepartment WHERE IsActive = 'True' AND DivisionWId = @wingId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetDivisionWingListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetDepartmentRelaton(string id, string param)
        {
            string queryStr = @"SELECT tblDivisionWing.Invisible,* FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE   (tblDivisionWing.Invisible IS NULL OR Invisible='False')  and tblDepartment.IsActive = 'True' AND DepartmentId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

    }
}
