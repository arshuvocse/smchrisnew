using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class BenefitJobNatureDAO
    {
        public int BenefitNatureId { get; set; }
        public int BenefitMasterId { get; set; }
        public bool PermConfirmed { get; set; }
        public bool PermProbation { get; set; }
        public bool ContConfirmed { get; set; }
        public bool ContProbation { get; set; }
        public int ContYear { get; set; }
        public bool CasualConfirmed { get; set; }
        public bool CasualProbation { get; set; }
        public bool Perma { get; set; }
        public bool Contra { get; set; }
        public bool Casua { get; set; }

    }
}
