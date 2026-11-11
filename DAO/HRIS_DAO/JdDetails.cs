using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class JdDetails
    {
        public int JdDetailsId { get; set; }

        public int? JdMasterId { get; set; }
       public bool IsActive { get; set; }

        public string JdDetailsInfo { get; set; }
    }
}
