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
using DAO.HealthCare_Dao;

namespace DAL.HealthCare_DAL
{
   public class BillSettlementDal
    {
       DataAccessManager accessManager = new DataAccessManager();

       public DataTable Get_ReimbusrmentFormlist()
       {
           DataTable dt = new DataTable();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
               dt = accessManager.GetDataTable("sp_GET_ReimbursmentFromlist");
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


       public DataTable Get_ReportForReimbursmentBill(int ID)
       {
           DataTable dt = new DataTable();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
               List<SqlParameter> aList = new List<SqlParameter>();

               aList.Add(new SqlParameter("@Id", ID));
               dt = accessManager.GetDataTable("sp_GET_ReimbursMentBillForPRTById", aList);
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
       ClsCommonInternalDAL aDal = new ClsCommonInternalDAL();

       public bool DeleteIncrementMaster(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@BillSettlmentId", Id));
           aSqlParameterlist.Add(new SqlParameter("@DeleteBy", HttpContext.Current.Session["UserId"]));
           string query = @"
 DECLARE @codeD NVARCHAR(MAX)

    SELECT  @codeD = ReimbursFromMasterId from [tbl_billSettlement_Healthcare] where  BillSettlmentId=@BillSettlmentId
update tbl_ReimbursmentFormMaster_HealthCare set PaymentStatus='New' where ReimbursFromMasterId=@codeD

INSERT INTO tbl_billSettlement_Healthcare_Del ([BillSettlmentId]
      ,[ReimbursFromMasterId]
      ,[ClaimNo]
      ,[SettlementDate]
      ,[RecommendedPayment]
      ,[PayableFrom]
      ,[OPDIPDBalance]
      ,[PaybleAmount]
      ,[RemainBalance]
      ,[PaymentType]
      ,[CheckDate]
      ,[EntryBy]
      ,[EntryDate]
      ,[OtherBalance],[CashDate]
      ,[IsExtraAllocate]
      ,[ExtraAllocateTK]
      ,[BankId]
      ,[AccountNo]
      ,[DeleteBy]
      ,[DeleteDate])
SELECT [BillSettlmentId]
      ,[ReimbursFromMasterId]
      ,[ClaimNo]
      ,[SettlementDate]
      ,[RecommendedPayment]
      ,[PayableFrom]
      ,[OPDIPDBalance]
      ,[PaybleAmount]
      ,[RemainBalance]
      ,[PaymentType]
      ,[CheckDate]
      ,[EntryBy]
      ,[EntryDate]
      ,[OtherBalance]  ,[CashDate]
      ,[IsExtraAllocate]
      ,[ExtraAllocateTK]
      ,[BankId]
      ,[AccountNo],@DeleteBy,GETDATE()
  FROM [dbo].[tbl_billSettlement_Healthcare]
 
WHERE BillSettlmentId=@BillSettlmentId    
delete from [tbl_billSettlement_Healthcare] where  BillSettlmentId=@BillSettlmentId";

           return aDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
       }

       public DataTable Get_BillSettlement_For_View()
       {
           DataTable dt = new DataTable();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
               dt = accessManager.GetDataTable("sp_GET_BillSettlementViewList");
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

       public DataTable Get_BillSettlement(int ID)
       {
           DataTable dt = new DataTable();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
               List<SqlParameter> aList = new List<SqlParameter>();

               aList.Add(new SqlParameter("@Id", ID));
               dt = accessManager.GetDataTable("sp_GET_BillSettlement", aList);
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


       public DataTable Get_EmpInfoForBillSettlement(int ID)
       {
           DataTable dt = new DataTable();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
               List<SqlParameter> aList = new List<SqlParameter>();

               aList.Add(new SqlParameter("@Id", ID));
               dt = accessManager.GetDataTable("sp_GET_EmpinfoforBillSettlement", aList);
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

       public DataTable Get_AdvanceBill(int ID)
       {
           DataTable dt = new DataTable();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
               List<SqlParameter> aList = new List<SqlParameter>();

               aList.Add(new SqlParameter("@Id", ID));
               dt = accessManager.GetDataTable("sp_GET_AdvanceBill_HealthcareByReimbursFromMasterId", aList);
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

       public int data_save(BillSettlement aMaster, List<BillSettlementDocument> DocumentList)
       {
           int pk = 0;
           bool result = false;

           try
           {
               List<SqlParameter> aSqlParameters = new List<SqlParameter>();
               accessManager.SqlConnectionOpen(DataBase.H);
               aSqlParameters.Add(new SqlParameter("@BillSettlmentId", aMaster.BillSettlmentId));
               aSqlParameters.Add(new SqlParameter("@ReimbursFromMasterId", aMaster.ReimbursFromMasterId));
               aSqlParameters.Add(new SqlParameter("@SettlementDate", aMaster.SettlementDate));
               aSqlParameters.Add(new SqlParameter("@RecommendedPayment", aMaster.RecommendedPayment));
               aSqlParameters.Add(new SqlParameter("@PayableFrom", aMaster.PayableFrom));
               aSqlParameters.Add(new SqlParameter("@OPDIPDBalance",  (object)aMaster.OPDIPDBalance ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@OtherBalance", (object)aMaster.OtherBalance ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@PaybleAmount", (object)aMaster.PaybleAmount ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@ApplicationAmount", (object)aMaster.ApplicationAmount ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@RemainBalance", (object)aMaster.RemainBalance ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@PaymentType", aMaster.PaymentType));
               aSqlParameters.Add(new SqlParameter("@CheckDate", (object)aMaster.CheckDate ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@AddvanceTK", (object)aMaster.AddvanceTK ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@EntryBy", aMaster.EntryBy));

               aSqlParameters.Add(new SqlParameter("@CashDate", (object)aMaster.CashDate ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@IsExtraAllocate", (object)aMaster.IsExtraAllocate ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@ExtraAllocateTK", (object)aMaster.ExtraAllocateTK ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@BankId", (object)aMaster.BankId ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@AccountNo", (object)aMaster.AccountNo ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@CurrentBlnce", (object)aMaster.CurrentBlnce ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@RemaiingBlnce", (object)aMaster.RemaiingBlnce ?? DBNull.Value));


               aSqlParameters.Add(new SqlParameter("@MainRemarks", (object)aMaster.MainRemarks ?? DBNull.Value));

               pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_BillSettlement", aSqlParameters);

               if (pk > 0)
               {
                   foreach (var reimbursmentDocument in DocumentList)
                   {
                       try
                       {
                           List<SqlParameter> gSqlParameters = new List<SqlParameter>();
                           gSqlParameters.Add(new SqlParameter("BillSettlementDocumentId", reimbursmentDocument.BillSettlementDocumentId));
                           gSqlParameters.Add(new SqlParameter("BillSettlmentId", pk));
                           gSqlParameters.Add(new SqlParameter("DocumentLink", (object)reimbursmentDocument.DocumentLink ?? DBNull.Value));
                           gSqlParameters.Add(new SqlParameter("DocumentNote", (object)reimbursmentDocument.DocumentNote ?? DBNull.Value));
                           gSqlParameters.Add(new SqlParameter("FileName", (object)reimbursmentDocument.FileName ?? DBNull.Value));
                           result = accessManager.SaveData("sp_Save_BillsettlementDocment", gSqlParameters);
                       }
                       catch (Exception exception)
                       {
                           result = false;
                           throw exception;
                       }
                   }

               }

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

           return pk;

       }



    }
}
