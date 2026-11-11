using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class AppraisalFinalStatus
    {

        public int ApprisalFinalStatusId { get; set; }

        public int? AppraisalMasterId { get; set; }
        public int? BSCAppraisalMasterId { get; set; }

        public decimal? TotalScore { get; set; }

        public string FinalStatus { get; set; }
        public string Justification { get; set; }

        public bool? GeneralIncrement { get; set; }

        public bool? SpecialIncrement { get; set; }

        public int? SpecialStep { get; set; }

        public int SpecialStepPercent { get; set; }

        public bool? IsPromotion { get; set; }

        public bool? Pip { get; set; }
        public bool? Other { get; set; }
        public string Note { get; set; }


        public string DocumentLink { get; set; }

       
        public string FileName { get; set; }
    }
}
