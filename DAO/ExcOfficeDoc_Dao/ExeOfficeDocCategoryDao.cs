using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ExcOfficeDoc_Dao
{
   public class ExeOfficeDocCategoryDao
    { 
        public int ExeOfficeDocCatId { get; set; }

        public string ExeOfficeDocCategory { get; set; }

        public int? CreateBy { get; set; }

        public DateTime CreateDate { get; set; }
        public int? UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }  
    }
}
