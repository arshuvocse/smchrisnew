using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class FacilityMasterDAO
    {
        public int FacilityMasterId { get; set; }
        public string Benefit { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}
