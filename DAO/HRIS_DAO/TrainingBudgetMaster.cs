using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingBudgetMaster
    {

      public int TrainingBudgetMasterId{get;set;}
      public int CompanyId{get;set;}
      public int FinancialYearId { get; set; }
        
      public string TrainingTitle{get;set;}
      public string TrainingResult { get; set; }
      public decimal TotalParticipant{get;set;}
      public decimal Duration { get; set; }
      public bool IsExternal{get;set;}
      public bool IsInternal { get; set; }
      public bool IsLocal { get; set; }
      public bool IsForeign { get; set; }
      public decimal CostParticipant{get;set;}
      public decimal Budget{get;set;}
      public string Referance{get;set;}
      public string Remarks { get; set; }
      public bool ForDepartment{get;set;}
      public bool ForGrade { get; set; }
      public bool ForEmployee { get; set; }
      public string EntryBy{get;set;}
      public DateTime? EntryDate{get;set;}
      public string UpdateBy { get; set; }
      public string ApprovalStatus { get; set; }
      public DateTime? UpdateDate { get; set; }
      public bool? IsApprove { get; set; }
      public DateTime? ApproveDate { get; set; }

      public List<TrainingBudgetDetailsDpt> BudgetDetailsDpt { get; set; }

      public List<TrainingBudgetDetailsGrade> BudgetDetailsGrade { get; set; }

      List<TrainingBudgetDetailsEmployee> BudgetDetailsEmployee { get; set; }
    }
}
