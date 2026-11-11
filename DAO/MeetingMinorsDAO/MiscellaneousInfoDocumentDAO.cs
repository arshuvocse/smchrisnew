using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
   public class MiscellaneousInfoDocumentDAO
    {
        public int MiscellaneousInfoDocumentID { get; set; }

        public int? MiscellaneousInfoId { get; set; }

        public string DocumentLink { get; set; }

        public string DocumentNote { get; set; }
        public string FileName { get; set; }
        public string ExtractedText { get; set; }
        public string DocType { get; set; }
    }
}
