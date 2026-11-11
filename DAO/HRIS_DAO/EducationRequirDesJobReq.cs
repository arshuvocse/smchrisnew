using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public  class EducationRequirDesJobReq
    {

      public Int32 EduRequirJobId { set; get; }
      public Int32 JobReqFormId { set; get; }
      public Int32 EduRequirId { set; get; }
      public string Major { set; get; }
    }
}
