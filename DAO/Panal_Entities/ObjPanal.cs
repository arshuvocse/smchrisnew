using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Panal_Entities
{
   public class ObjPanal
    {
       public int SL { get; set; }
       public string ManuName { get; set; }
       public string URL { get; set; }
       public string ParantId { get; set; }
       public bool IsApprovalPage { get; set; }
       public int ModuleId { get; set; }
    }
}
