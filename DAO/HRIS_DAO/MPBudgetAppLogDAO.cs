using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class MPBudgetAppLogDAO
    {
        public int MPBudgetAppLogId { get; set; }
        public int MPBudgetMasterId { get; set; }
        
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ActionStatus { get; set; }
        public string Comments { get; set; }
    }
}
