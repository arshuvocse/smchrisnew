using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class CommetteeSetupDetailsDao
    {
       public int ComSetupDetailsId { get; set; }
       public int ComSetupMasId { get; set; }
       public int EmpInfoId { get; set; }
       public int NewEmpInfoId { get; set; }
       public Boolean IsForward { get; set; }
       public Boolean IsApproved { get; set; }
       public Boolean IsDoctor { get; set; }

       public Boolean IsConvenor { get; set; }
       public Boolean IsMemberSecretory { get; set; }
       public Boolean IsMember { get; set; }

    }
}
