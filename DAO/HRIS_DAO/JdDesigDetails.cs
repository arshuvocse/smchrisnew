using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class JdDesigDetails
    {
        public int JdDesigDetailsId { get; set; }

        public int? JdDesigMasterId { get; set; }

        public string JdDetailsInfo { get; set; }
    }
}
