using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAO.HealthCare_Dao;

namespace DAL.HealthCare_DAL
{
   public class AdvanceBillDal
    {

        DataAccessManager accessManager = new DataAccessManager();

        public DataTable Get_RequisitionNumberDDl(string comId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", comId));
                dt = accessManager.GetDataTable("sp_GET_RequsitionNumberForDDL",aList);
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


        public DataTable Get_FromMasterForAdvanceBill(int Id)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);

                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", Id));
                dt = accessManager.GetDataTable("sp_GET_ReimbursmentMasterForadvanceBill",aList);
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

        public DataTable Get_FromMasterForAdvanceBillEmpId(int Id)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);

                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", Id));
                dt = accessManager.GetDataTable("sp_GET_EmpInfoforAdvance", aList);
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

        public DataTable Get_ClaimDetailsById(int Id)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);

                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", Id));
                dt = accessManager.GetDataTable("sp_GET_ClaimDetailsbyRequisitionNO", aList);
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


        public int data_save(AdvanceBill aMaster)
        {
            int pk = 0;
            bool result = false;

            try
            {
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                accessManager.SqlConnectionOpen(DataBase.H);
                aSqlParameters.Add(new SqlParameter("AdvanceBillId", aMaster.AdvanceBillId));
                aSqlParameters.Add(new SqlParameter("ReimbursFromMasterId", aMaster.ReimbursFromMasterId));
                aSqlParameters.Add(new SqlParameter("RequitisionNo", aMaster.RequitisionNo));
                aSqlParameters.Add(new SqlParameter("EmpInfoId", aMaster.EmpInfoId));
                aSqlParameters.Add(new SqlParameter("CompanyId", aMaster.CompanyId));
                aSqlParameters.Add(new SqlParameter("FinancialId", aMaster.FinancialId));
                aSqlParameters.Add(new SqlParameter("IsIPD", aMaster.IsIPD));
                aSqlParameters.Add(new SqlParameter("IsOPD", aMaster.IsOPD));
                aSqlParameters.Add(new SqlParameter("IsSpecial", aMaster.IsSpecial));
                aSqlParameters.Add(new SqlParameter("Amount", aMaster.Amount));
                aSqlParameters.Add(new SqlParameter("Remarks", aMaster.Remarks));
                aSqlParameters.Add(new SqlParameter("CarryPerson", aMaster.CarryPerson));
                aSqlParameters.Add(new SqlParameter("EntryBy", aMaster.EntryBy));
        
                pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_AdvanceBill", aSqlParameters);

            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return pk;

        }



        public DataTable Get_AdvanceBill(int Cid)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                aSqlParameters.Add(new SqlParameter("@Cid", Cid));

                dt = accessManager.GetDataTable("sp_GET_AdvanceBill",aSqlParameters);
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
    }
}
