using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class VacancyCirculationDAO
    {
       public Int32 VacancyCirculationId { set; get; }
       public string CirculationWay { set; get; }
        public Boolean IsActive { set; get; }
        public string EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public string UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }
    }
}
