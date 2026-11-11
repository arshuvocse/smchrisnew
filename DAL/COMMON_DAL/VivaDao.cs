using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.COMMON_DAL
{
   public class VivaDao
    {
        public int VivaId { get; set; }
        public string VivaName { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Category { get; set; }
        public Nullable<int> EntryBy { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Remarks { get; set; }
    }
}
