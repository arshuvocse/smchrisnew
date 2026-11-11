using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
   public class EmpExitDocumentDAO
    {
       public int EmpExitDocumentId { get; set; }

       public int? ExitMasterId { get; set; }

        public string DocumentLink { get; set; }

        public string DocumentNote { get; set; }
        public string FileName { get; set; }
    }
}
