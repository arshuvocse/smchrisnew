using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.TrainingDAL
{
    public class TrainingBudgetDal
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable TrainingBudget2List(string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblTrainingBudget2Master A                                 
                                LEFT JOIN dbo.tblCompanyInfo c ON A.CompanyId =  c.CompanyId
                                LEFT JOIN dbo.tblFinancialYear fy ON A.FinancialYearId = fy.FinancialYearId
                                WHERE (A.IsDelete IS NULL OR A.IsDelete  = 0 ) " + param + "";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable GetTrainingBudgetDAL(string id)
        {
            try
            {
                string query = @"SELECT CI.CompanyName,FINY.FinancialYearDesc,TBZD.TrainingTitle,TBZD.ExpectedResult,TBZD.Grade,TBZD.Quater,
                                 CASE WHEN TBZD.IsInternal = 1  AND TBZD.IsExternal = 0 THEN 'Internal' WHEN  TBZD.IsExternal = 1 AND TBZD.IsInternal = 0 THEN 'External' END AS InternalExternal,
                                 CASE WHEN TBZD.IsForeign = 1 AND TBZD.IsLocal = 0 THEN 'Foreign' WHEN TBZD.IsLocal=1 AND TBZD.IsForeign= 0 THEN 'Local' END AS ForeignLocal,
                                 MNTH.MonthName AS Month,TBZD.TotalParticipant,TBZD.BudgetCostParticipant AS BudgetedCost,TBZD.Budget AS BudgetAmount,TBZD.Remarks
                                 FROM tblTrainingBudget2Master AS TBZ
                                 LEFT JOIN dbo.tblTrainingBudget2Details AS TBZD ON TBZD.TrainingBudget2Id = TBZ.TrainingBudget2Id
                                 LEFT JOIN dbo.tblQuarterMonthInfo MNTH ON MNTH.QuarterMonthId = TBZD.MonthId
                                 LEFT JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TBZ.CompanyId
                                 LEFT JOIN dbo.tblFinancialYear AS FINY ON FINY.FinancialYearId = TBZ.FinancialYearId WHERE TBZ.TrainingBudget2Id = " + id + "";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
    }
}
