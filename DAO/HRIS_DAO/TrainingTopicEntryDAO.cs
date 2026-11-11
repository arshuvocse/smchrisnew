using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingTopicEntryDAO
    {
        public Int32 TrainingTopicId { set; get; }
        public string TrainingTopic { set; get; }
        public int TraingingSerial { set; get; } 
       public bool   IsActive { set; get; } 
       public Int32   EntryBy { set; get; } 
       public DateTime   EntryDate { set; get; } 
      public Int32    UpdateBy { set; get; }
      public DateTime UpdateDate { set; get; }

      public Int32 DeleteBy { set; get; }
      public DateTime DeleteDate { set; get; }

      public bool IsDelete { set; get; }

      public int TraingingHeadingId { set; get; }

     

         
    }
}
