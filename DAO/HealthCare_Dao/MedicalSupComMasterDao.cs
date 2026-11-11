using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class MedicalSupComMasterDao
    {

       public int MSCMaster_ID { get; set; }

       public string MSC_Code { get; set; }

       public string MSC_Name { get; set; }

       public int CompanyId { get; set; }
       public int DivisionId { get; set; }
       public int DepartmentId { get; set; }
       public int CreateBy { get; set; }

       public DateTime? CreateDate { get; set; }

       public int UpdateBy { get; set; }

       public DateTime? UpdateDate { get; set; }

    }
}
