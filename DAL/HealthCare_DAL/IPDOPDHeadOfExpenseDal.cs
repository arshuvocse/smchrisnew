using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HealthCare_Dao;

namespace DAL.HealthCare_DAL
{
    public class IPDOPDHeadOfExpenseDal
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();


        public DataTable Get_HeadEntryCheck(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_Check_IPDOPDHeadEntryCheckByMasterId", aList);
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


        public DataTable Get_HeadOfExpense(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_IPDOPDHeadByMasterId", aList);
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

        public Int32 SaveHeadOfExpense(HeadOfExpenseMaster aMaster,List<IPDOPDHeadOfExpenseDao> aDirectStockInDaoList)
        {
            Int32 ID = 0;
            bool Status = false;

            List<SqlParameter> aSqlParameters = new List<SqlParameter>();

            aSqlParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
            aSqlParameters.Add(new SqlParameter("@IsOPD", aMaster.IsOPD));
            aSqlParameters.Add(new SqlParameter("@EntryBy", aMaster.EntryBy));
            aSqlParameters.Add(new SqlParameter("@EntryDate", aMaster.EntryDate));



            const string InQuery = @"
 
INSERT INTO tbl_HeadOfExpenseMaster_Healthcare (CompanyId,IsOPD,EntryBy,EntryDate)
                                         VALUES (@CompanyId,@IsOPD,@EntryBy,@EntryDate)";


            String queryStr = @"
select   *
 from tbl_HeadOfExpenseMaster_Healthcare where CompanyId=LTRIM(RTRIM('" + aMaster.CompanyId + "')) and IsOPD='" + aMaster.IsOPD + "'";
            DataTable dt= aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");


            if (dt.Rows.Count==0)
            {
                ID = aCommonInternalDal.SaveDataByInsertCommandById(InQuery, aSqlParameters, "HRDB");
                
            }
            
           
            if (ID > 0)
            {
                foreach (IPDOPDHeadOfExpenseDao aDao in aDirectStockInDaoList)
                {
                    var aSqlParameterlist = new List<SqlParameter>();

                    aSqlParameterlist.Add(new SqlParameter("@HeadOfExpenseMasterId", ID));
                    aSqlParameterlist.Add(new SqlParameter("@OIPDHeadOfExpenseId", aDao.OIPDHeadOfExpenseId));
                    aSqlParameterlist.Add(new SqlParameter("@HeadOfExpense", aDao.HeadOfExpense));
                    aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
                    aSqlParameterlist.Add(new SqlParameter("@Amount", aDao.Amount));
                    aSqlParameterlist.Add(new SqlParameter("@Remaks", aDao.Remaks));
                    aSqlParameterlist.Add(new SqlParameter("@IsActive", aDao.IsActive));
                    aSqlParameterlist.Add(new SqlParameter("@IsOPD", aDao.IsOPD));
                    aSqlParameterlist.Add(new SqlParameter("@IsMaternity", (object)aDao.IsMaternity ?? DBNull.Value));
                    aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                    aSqlParameterlist.Add(new SqlParameter("@EntryDate", DateTime.Now));

                    string insertQuery = "";
                    if (aDao.OIPDHeadOfExpenseId>0)
                    {
                        insertQuery = @"UPDATE [dbo].[tbl_OPDIPDHeadOfExpense_HealthCare]
   SET [HeadOfExpense] = @HeadOfExpense 
      ,[Amount] = @Amount 
      ,[IsActive] = @IsActive 
      ,[IsMaternity] = @IsMaternity 
      ,[Remaks] = @Remaks 
      ,[IsOPD] = @IsOPD 
      ,[EntryBy] = @EntryBy 
      ,[EntryDate] = @EntryDate 
      ,[CompanyId] = @CompanyId 
      ,[HeadOfExpenseMasterId] = @HeadOfExpenseMasterId 
 WHERE OIPDHeadOfExpenseId=@OIPDHeadOfExpenseId";
                    }
                    else
                    {
                        insertQuery = @"INSERT INTO tbl_OPDIPDHeadOfExpense_HealthCare (HeadOfExpense,HeadOfExpenseMasterId,CompanyId,Amount,Remaks,IsActive,IsOPD,IsMaternity,EntryBy,EntryDate)
                                         VALUES (@HeadOfExpense,@HeadOfExpenseMasterId,@CompanyId,@Amount,@Remaks,@IsActive,@IsOPD,@IsMaternity,@EntryBy,@EntryDate)";
                    }

                      
                    Status = aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");
                }
            }

          
            return ID;
        }

        public DataTable GetHeadofexpenseList()
        {
            string queryStr = @"SELECT H.HeadOfExpenseMasterId,case when usEmp.EmpInfoId is null then  us.UserName else   usEmp.EmpName+ISNULL(' : '+dgs.Designation,'') end  CreateBy, Com.ShortName, CASE 
WHEN IsOPD=1 THEN 'OPD' ELSE 'IPD' END as IsOPD,   H.EntryDate  , H.UpdateDate  FROM tbl_HeadOfExpenseMaster_Healthcare H with (nolock)
LEFT JOIN tblCompanyInfo Com ON Com.CompanyId = H.CompanyId
left JOIN  dbo.tblUser us   ON  H.EntryBy =us.UserId   
 left JOIN  dbo.tblEmpGeneralInfo usEmp   ON  us.EmpInfoId =usEmp.EmpInfoId
 LEFT JOIN dbo.tblDesignation dgs ON usEmp.DesignationId = dgs.DesignationId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public bool Update_Data(HeadOfExpenseMaster aMaster, List<IPDOPDHeadOfExpenseDao> aDirectStockInDaoList)
        {
            int pk = 0;
            bool result = false;
            try
            {
                List<SqlParameter> aSqlParameters = new List<SqlParameter>();
                accessManager.SqlConnectionOpen(DataBase.H);
                aSqlParameters.Add(new SqlParameter("@HeadOfExpenseMasterId", aMaster.HeadOfExpenseMasterId));
                aSqlParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                aSqlParameters.Add(new SqlParameter("@IsOPD", aMaster.IsOPD));
                aSqlParameters.Add(new SqlParameter("@UpdateBy", aMaster.UpdateBy));
                if (aMaster.HeadOfExpenseMasterId > 0)
                {
                    List<SqlParameter> gSqlParameters = new List<SqlParameter>();
                    gSqlParameters.Add(new SqlParameter("@HeadOfExpenseMasterId", aMaster.HeadOfExpenseMasterId));
                    gSqlParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    gSqlParameters.Add(new SqlParameter("@IsOPD", aMaster.IsOPD));
                    DataTable dt = accessManager.GetDataTable("sp_Check_HeadOfExpenseMaster", gSqlParameters);
                    if (dt.Rows.Count == 0)
                    {
                        result = accessManager.UpdateData("sp_Update_HeadOfExpenseMaster", aSqlParameters);
                        if (result)
                        {
                            pk = aMaster.HeadOfExpenseMasterId;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                    if (pk > 0)
                    {
                        foreach (IPDOPDHeadOfExpenseDao aDao in aDirectStockInDaoList)
                        {
                            var aSqlParameterlist = new List<SqlParameter>();
                            aSqlParameterlist.Add(new SqlParameter("@HeadOfExpenseMasterId", pk));
                            aSqlParameterlist.Add(new SqlParameter("@OIPDHeadOfExpenseId", aDao.OIPDHeadOfExpenseId));

                            aSqlParameterlist.Add(new SqlParameter("@HeadOfExpense", aDao.HeadOfExpense));
                            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
                            aSqlParameterlist.Add(new SqlParameter("@Amount", aDao.Amount));
                            aSqlParameterlist.Add(new SqlParameter("@Remaks", aDao.Remaks));
                            aSqlParameterlist.Add(new SqlParameter("@IsActive", aDao.IsActive));
                            aSqlParameterlist.Add(new SqlParameter("@IsOPD", aDao.IsOPD));
                            aSqlParameterlist.Add(new SqlParameter("@IsMaternity", (object)aDao.IsMaternity ?? DBNull.Value));

                            aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                            aSqlParameterlist.Add(new SqlParameter("@EntryDate", DateTime.Now));
                            string insertQuery = "";
                            if (aDao.OIPDHeadOfExpenseId > 0)
                            {
                                insertQuery = @"UPDATE [dbo].[tbl_OPDIPDHeadOfExpense_HealthCare]
   SET [HeadOfExpense] = @HeadOfExpense 
      ,[Amount] = @Amount 
      ,[IsActive] = @IsActive 
      ,[IsMaternity] = @IsMaternity 
      ,[Remaks] = @Remaks 
      ,[IsOPD] = @IsOPD 
      ,[EntryBy] = @EntryBy 
      ,[EntryDate] = @EntryDate 
      ,[CompanyId] = @CompanyId 
      ,[HeadOfExpenseMasterId] = @HeadOfExpenseMasterId 
 WHERE OIPDHeadOfExpenseId=@OIPDHeadOfExpenseId";
                            }
                            else
                            {
                                insertQuery = @"INSERT INTO tbl_OPDIPDHeadOfExpense_HealthCare (HeadOfExpense,HeadOfExpenseMasterId,CompanyId,Amount,Remaks,IsActive,IsOPD,IsMaternity,EntryBy,EntryDate)
                                         VALUES (@HeadOfExpense,@HeadOfExpenseMasterId,@CompanyId,@Amount,@Remaks,@IsActive,@IsOPD,@IsMaternity,@EntryBy,@EntryDate)";
                            }
                            result = aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");


                        }
                    }
                }
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

            return result;

        }
    }
}
