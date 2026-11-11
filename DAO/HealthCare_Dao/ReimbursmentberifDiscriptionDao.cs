using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class ReimbursmentberifDiscriptionDao
    {
        public int ReimbursFromBriefDescriptionId { get; set; }

        public int? ReimbursFromMasterId { get; set; }

        public int? ReibCheckOppId { get; set; }

        public bool? YesNo { get; set; }

        public bool? Date { get; set; }

        public DateTime? Descriptiondate { get; set; }
    }
}
