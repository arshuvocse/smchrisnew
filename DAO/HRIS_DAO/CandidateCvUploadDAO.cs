using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public   class CandidateCvUploadDAO
    {
        public int CvUploadId { get; set; }
        public int CandidateID { get; set; }
        public string Cv_Upload { get; set; }
    }
}
