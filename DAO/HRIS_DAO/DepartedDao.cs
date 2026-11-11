using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class DepartedDao
    {
        public int DepartedId { set; get; }
        public int? EmpInfoId { set; get; }
        public string Relative { set; get; }
        public string Name { set; get; }
        public DateTime? DeathofDate { set; get; }
        public string Remarks { set; get; }
        public string UploadImage { set; get; }
        public string ImagePath { set; get; }
        public int EntryBy { set; get; }
        public DateTime? EntryDate { set; get; }

    }
}
