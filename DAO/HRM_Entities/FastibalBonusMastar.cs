using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class FastibalBonusMastar
    {
        public int FastivalBonusId { get; set; }
        public int FestivalId { get; set; }
        public int CompanyInfoId { get; set; }
        public int FestivalYear { get; set; }
        public int EmpCategoryId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string BonusApplicableOn { get; set; }
        public DateTime ServiceLenghMarginDate { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string Status { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime LockDate { get; set; }
    }
}
