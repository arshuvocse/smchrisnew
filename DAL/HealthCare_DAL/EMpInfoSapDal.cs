using DAL.DataManager;
using DAL.InternalCls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms.VisualStyles;

namespace DAL.HealthCare_DAL
{
   public class EMpInfoSapDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

        public DataTable Get_EmpInfoAll(string Param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);

                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Param", Param));

                dt = accessManager.GetDataTable("sp_GET_GETEmployeeInfo_Sap", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }




        public DataTable Get_Child_ById(string Param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@EmpId", Param));
                dt = accessManager.GetDataTable("sp_GET_Child_ById_Sap", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }




        public DataTable Get_EmpAll_ById(string Param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@EmpId", Param));
                dt = accessManager.GetDataTable("sp_GET_GETEmployeeInfo_ById_Sap", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }






        public DataTable Check_EmpAll_ById(string Param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@EmpId", Param));
                dt = accessManager.GetDataTable("sp_Check_GETEmployeeInfo_ById_Sap", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }


        public bool Get_Add_ToSystem(string Param)
        {
            bool Status = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@Pernr", Param));
                aList.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                Status = accessManager.UpdateData("sp_Save_Insert_EmployeeInformationSap_To_System", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return Status;
        }
    }
}
