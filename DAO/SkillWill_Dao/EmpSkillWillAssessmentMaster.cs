using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SkillWill_Dao
{
   public class EmpSkillWillAssessmentMaster
    {
       public int EmpSkillWillMasterId { get; set; }

       public int EmpInfoId { get; set; }
       public int FinancialYearId { get; set; }

        public int EntryBy { get; set; }

        public DateTime EntryDate { get; set; }

        public int UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}
