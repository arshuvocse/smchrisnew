using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class MedicalSupComDetailsDao
    {
       public int  MSCDetail_ID { get; set; }
       public int MSCMaster_ID { get; set; }
       public int EmpInfoId { get; set; }
       public int Seq_No { get; set; }
       public string Position { get; set; }
       
    }
}
