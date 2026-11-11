using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class AchievementsEntryDAO
    {
        public Int32 MasterAchievementsId { set; get; }
        public string AchievementsName { set; get; }
        public bool IsActive { set; get; }
        public Int32 EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public Int32 UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }

         
    }
}
