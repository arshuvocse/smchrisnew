using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SkillWill_Dao
{
   public class SkillWillAssesmentDecDetailsDao
    {
        public int SkillWillAssesDecDetailsId { get; set; }

        public int KPIDeadLineMasterId { get; set; }

        public int EmpinfoId { get; set; }

        public DateTime? DeadLine { get; set; }

        public string Remarks { get; set; }

        public DateTime? ExtensionDate { get; set; }
    }
}
