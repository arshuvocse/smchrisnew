using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class AddressDistrictEntryDAO
    {
        public Int32 DistrictID { set; get; }
        public Int32 DivisionID { set; get; }
        public string Titel { set; get; } 
     
       public Int32   EntryBy { set; get; } 
       public DateTime   EntryDate { set; get; } 
      public Int32    UpdateBy { set; get; }
      public DateTime UpdateDate { set; get; }

      

         
    }
}
