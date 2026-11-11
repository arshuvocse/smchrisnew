using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class VacancyEntryDao
    {
        public Int32 VacancyCirculationId { set; get; } 
       public string   CirculationWay { set; get; } 
       public bool   IsActive { set; get; } 
       public Int32   EntryBy { set; get; } 
       public DateTime   EntryDate { set; get; } 
      public Int32    UpdateBy { set; get; }
      public DateTime UpdateDate { set; get; }

      public Int32 DeleteBy { set; get; }
      public DateTime DeleteDate { set; get; }

      public bool IsDelete { set; get; } 

         
    }
}
