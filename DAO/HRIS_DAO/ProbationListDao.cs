using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ProbationListDao
    {
         public Int32 ConfirmationId { set; get; }
	     public Int32 EmpInfoId { set; get; }
         public string EmpMasterCode { set; get; }
	     public string ZID { set; get; }
	     public DateTime JoiningDate { set; get; }
	     public DateTime ProbationEndDate { set; get; }
         public string Approveby { set; get; }
         public DateTime ApproveDate { set; get; }
    }


    public class PromotionListDao
    {
        public int Promotion_ConfirmationId { get; set; }

        public int? EmployeePromotionEntryId { get; set; }

        public int? EmpInfoId { get; set; }

        public string EmpMasterCode { get; set; }

        public string ZID { get; set; }

        public DateTime? Effectivedate { get; set; }

        public string PromoTypeId { get; set; }

        public string Approveby { get; set; }

        public DateTime? ApproveDate { get; set; }
    }
}
