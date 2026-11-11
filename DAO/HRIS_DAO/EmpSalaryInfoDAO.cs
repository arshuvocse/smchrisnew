using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public  class EmpSalaryInfoDAO
    {

      public Int32 EmpSalaryInfoId { set; get; }
      public int EmpInfoId { set; get; }
      public decimal? BasicPay { set; get; }
      public decimal? HouseRent { set; get; }
      public decimal? Medical { set; get; }
      public decimal? Conveyance { set; get; }
      public decimal? Washing { set; get; }

      public string PaymentType { get; set; }

      public int? BankNameId { get; set; }

      public string BankAccountNo { get; set; }

      public bool? ProvidentFundEligible { get; set; }

      public decimal? PF { get; set; }

      public decimal? MonthlyTax { get; set; }
    }
}
