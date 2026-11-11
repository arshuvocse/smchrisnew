using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class EducationRequirementsDetailDao
    {
       public int EducationRequirementsDetailId { get; set; }
       public int MasterId { get; set; }
       public int WayId { get; set; }
       public string Nos { get; set; }
    }
}
