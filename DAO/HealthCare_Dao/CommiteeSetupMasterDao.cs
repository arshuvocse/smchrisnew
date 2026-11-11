using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class CommiteeSetupMasterDao
    {
       public int ComSetupMasId { get; set; }
       public Boolean? IsOPD { get; set; }
       public int? SalaryLoationId { get; set; }
       public int? CompanyId { get; set; }
       public int EntryBy { get; set; }
       public DateTime? EntryDate { get; set; }
       public int UpdateBy { get; set; }
       public int UpdateDate { get; set; }
       public Boolean IsActive { get; set; }
       public string ApplicationType { get; set; }

    }
}
