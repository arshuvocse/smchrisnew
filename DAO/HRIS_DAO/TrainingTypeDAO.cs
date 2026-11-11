using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingTypeDAO
    {
        public int TrainingTypeID { get; set; }
        public string TrainingType { get; set; }
        public string Description { get; set; }
        public bool Evalution { get; set; }
        public string MonthName { get; set; }
        public bool IsActive { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string Updateby { get; set; }
        public DateTime Updatedate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeleteBy { get; set; }
        public DateTime DeleteDate { get; set; }


    }
}
