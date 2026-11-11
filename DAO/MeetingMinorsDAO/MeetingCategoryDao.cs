using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
   public class MeetingCategoryDao
    {
       public int CategoryID { get; set; }

       public string MeetingCategory { get; set; }

       public int CreateBy { get; set; }

       public DateTime CreateDate { get; set; }
       public int UpdateBy { get; set; }

       public DateTime UpdateDate { get; set; }  
    }
}
