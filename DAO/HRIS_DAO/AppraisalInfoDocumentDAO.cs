using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public class AppraisalInfoDocumentDAO
    {

      public int AppraisalInfoDocumentID { get; set; }

      public int? AppraisalMasterId { get; set; }

        public string DocumentLink { get; set; }

        public string DocumentNote { get; set; }
        public string FileName { get; set; }
    }
}
