using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAL.MAIN_FUNCTION;
using DAO.ACC_DAO;


namespace DAL.ACC_DAL
{
    public class FinancialYearDAL
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

    
        

        public int SaveFinancialYear(FinancialYearEntry aFinancialYearEntry)
            {
             int pk = 0;
           bool result = false;

           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aFinancialYearEntry.FinancialYearId));
                //aSqlParameterlist.Add(new SqlParameter("@FinancialCode", aFinancialYearEntry.FinancialCode));
                aSqlParameterlist.Add(new SqlParameter("@StartDate", aFinancialYearEntry.StartDate));
                aSqlParameterlist.Add(new SqlParameter("@EndDate", aFinancialYearEntry.EndDate));
                aSqlParameterlist.Add(new SqlParameter("@CompanyId", aFinancialYearEntry.CompanyId));
                //aSqlParameterlist.Add(new SqlParameter("@Status", aFinancialYearEntry.Status));
                aSqlParameterlist.Add(new SqlParameter("@ActiveDate", aFinancialYearEntry.ActiveDate));
                aSqlParameterlist.Add(new SqlParameter("@InActiveDate", aFinancialYearEntry.InActiveDate));

                pk= accessManager.SaveDataReturnPrimaryKey("sp_I_FinancialYear", aSqlParameterlist);

           }
           catch (Exception e)
           {
               pk = 0;
               accessManager.SqlConnectionClose(true);
               throw e;
           }
           finally
           {
               accessManager.SqlConnectionClose();
           }

           return pk;
            }

            

           

            public DataTable LoadFinancialYear(string search)
            {
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@FinancialCode", search));
                return accessManager.GetDataTable("sp_GET_FinancialYear", aSqlParameterlist);
            }

            public DataTable FinancialYearEditLoad(string FinancialYearId)
            {
                accessManager.SqlConnectionOpen(DataBase.H);
            DataTable dt = new DataTable();

                try
            {
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinancialYearId));
                dt= accessManager.GetDataTable("sp_GET_FinancialYearById", aSqlParameterlist);

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

            public bool UpdateFinancialYear(FinancialYearEntry aFinancialYearEntry)
            {
                bool istr;
                    try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aFinancialYearEntry.FinancialYearId));
                aSqlParameterlist.Add(new SqlParameter("@StartDate", aFinancialYearEntry.StartDate));
                aSqlParameterlist.Add(new SqlParameter("@EndDate", aFinancialYearEntry.EndDate));
                aSqlParameterlist.Add(new SqlParameter("@CompanyId", aFinancialYearEntry.CompanyId));
                aSqlParameterlist.Add(new SqlParameter("@Status", aFinancialYearEntry.Status));
                aSqlParameterlist.Add(new SqlParameter("@ActiveDate", aFinancialYearEntry.ActiveDate));
                aSqlParameterlist.Add(new SqlParameter("@InActiveDate", aFinancialYearEntry.InActiveDate));

               istr = accessManager.UpdateData("sp_UD_FinancialYear", aSqlParameterlist);
           }
                    catch (Exception e)
                    {
                        istr = false;
                        accessManager.SqlConnectionClose(true);
                        throw e;
                    }
                    finally
                    {
                        accessManager.SqlConnectionClose();
                    }

                    return istr;
            }
        public bool DeleteFinancialYear(FinancialYearEntry aFinancialYearEntry)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aFinancialYearEntry.FinancialYearId));


            return accessManager.DeleteData("sp_DEL_FinancialYear", aSqlParameterlist);
        }
    }
}
