using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UA_DAO
{
   public class UserAuthenticationDAO
    {
        public int SL { get; set; }
        public string ManuName { get; set; }
        public string URL { get; set; }
        public string ParantId { get; set; }
        public bool IsApprovalPage { get; set; }
        public int ModuleId { get; set; }
        public bool MenuModule { get; set; }

    }
}
