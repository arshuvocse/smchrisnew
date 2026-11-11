using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SkillWill_Dao
{
   public class EmpSkillWillDetailsDao
    {
       public int EmpSkillWillDetailsId { get; set; }
       public int EmpSkillWillMasterId { get; set; }
       public string Areasconsidered { get; set; }
       public string KRA { get; set; }
       public int SKILL { get; set; }
       public int WILL { get; set; }
    }
}
