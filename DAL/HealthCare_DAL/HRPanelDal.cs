using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.HealthCare_DAL
{
   public class HRPanelDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

        public DataTable Get_HRPanel(string Param )
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", Param));
             
                aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_HRPanel",aList);
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

        public DataTable Get_HRPanelPayroll(string Param )
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", Param));
             
                aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_HRPanelPayroll", aList);
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
        }   public DataTable Get_HRPanelHolding(string Param )
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", Param));
             
                aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_HRPanelForHolding", aList);
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


        public DataTable Get_HRPanelForAllBill(string Param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", Param));

                //aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_HRPanelForAllBill", aList);
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

        public DataTable Get_HealthCareReport(string Param, string BillType)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", Param));
                aList.Add(new SqlParameter("@BillType", BillType));
                aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_HealthCareReport", aList);
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

        public DataTable Get_HRPanel_ReturnApplication(string Param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", Param));
                dt = accessManager.GetDataTable("sp_GET_HRPanelReturnApplication", aList);
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



        public DataTable Get_HRPanelCheckCommeti(string ApplicationType, string SalaryLoationId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@ApplicationType", ApplicationType));
                aList.Add(new SqlParameter("@SalaryLoationId", SalaryLoationId));
                aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_HRPanelCheckCommiteee", aList);
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
        
        public DataTable Get_HRPanelCheckCommetiCompany(string ApplicationType, string SalaryLoationId, int ComId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@ApplicationType", ApplicationType));
                aList.Add(new SqlParameter("@SalaryLoationId", SalaryLoationId));
                aList.Add(new SqlParameter("@ComId", ComId));
                aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_HRPanelCheckCommiteeeCompany", aList);
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




        public DataTable Get_CommitteeFeedback(string ID)
        {
            try
            {
                string query = @"SELECT  TOP 1 * FROM tblCommitteeFeedback_HC WHERE ReimbursFromMasterId IS NOT NULL AND Feedback IS NOT NUll AND ReimbursFromMasterId=" + ID + " order By ComfeedbackId DESC ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool MDApprovalPermission(string FromMasterId, string status, string Comm)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status));
                aParameters.Add(new SqlParameter("@Comm", Comm));

                // aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Permission_MdSirApproval", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return result;
        }
        public DataTable Get_MaxLogInformation(string ID)
        {
            try
            {
                string query = @"SELECT H.ReimbursFromMasterId, tblReimbursementSelfAppLog.ReimbursementSelfAppLogId, ForEmpInfoId  FROM tbl_ReimbursmentFormMaster_HealthCare H
	INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= H.ReimbursFromMasterId
	INNER JOIN dbo.tblReimbursementSelfAppLog ON tblReimbursementSelfAppLog.ReimbursFromMasterId = H.ReimbursFromMasterId                             
	where H.ReimbursFromMasterId IS Not NULL AND Version=CELog.MaxVer AND H.ReimbursFromMasterId NOT IN(Select ReimbursFromMasterId from TopSheetGenerateDetails_H)  AND H.ReimbursFromMasterId=" + ID;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
