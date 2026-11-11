using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class FinancialYear
    {
      public int FinancialYearId{get;set;}
      public int FinancialCode{get;set;}
      public DateTime StartDate{get;set;}
      public DateTime EndDate { get; set; }
      public int CompanyId{get;set;}
      public string Status{get;set;}
      public DateTime ActiveDate { get; set; }
      //public DateTime InActiveDate{get;set;}
      public string FinancialYearDesc{get;set;}
    }
}
