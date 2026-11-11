using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ExcOfficeDoc_Dao
{
   public class ExeOffiDocUpMasterDao
    {
       public int ExeOffiDocUpId { get; set; }
       public int? CompanyId { get; set; }
       public int? ExeOfficeDocCatId { get; set; }
       public int? ExeOfficeDocSubCatId { get; set; }
       public string Remarks { get; set; }
       public int? CreateBy { get; set; }
       public DateTime CreateDate { get; set; }
       public int? UpdateBy { get; set; }
       public DateTime UpdateDate { get; set; }
       public string ActionStatus { get; set; }
       public  bool Isapproved { get; set; }

       public DateTime? DocumentEntryDate { get; set; }


    }
}
