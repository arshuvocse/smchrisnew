using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingBudgetDetailsDpt
    {

      public int TrainingBudgetDetailsDptId{get;set;}
      public int TrainingBudgetMasterId{get;set;}
      public int DepartmentId{get;set;}
      public decimal Qty{get;set;}
      public int FinancialYearId{get;set;}
      public int Quater{get;set;}
      public int TrainingMonth{get;set;}
      public DateTime? FromDate{get;set;}
      public DateTime? ToDate { get; set; }
    }
}
