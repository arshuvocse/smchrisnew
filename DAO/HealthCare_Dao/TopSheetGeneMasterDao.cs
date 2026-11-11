using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class TopSheetGeneMasterDao
    {
       public int TopsheetGeneMasId { get; set; }

       public string MeetingNo { get; set; }

       public string MeetingTitle { get; set; }
       
       public string Venue { get; set; }

       public TimeSpan? MeetingTime { get; set; }

       public DateTime? MeetingDate { get; set; }

       public int EntryBy { get; set; }

       public DateTime EntryDate { get; set; }

       public int UpdateBy { get; set; }

       public DateTime UpdateDate { get; set; }

       public bool Isactive { get; set; }
    }
}
