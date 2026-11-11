using System;
using System.ComponentModel.DataAnnotations;

namespace DAO.HRIS_DAO
{
    public class MidYearKPISetupDAO
    {
        
        public int MidYearKPISetupId { get; set; }

        public int? CompanyId { get; set; }

        public int? FinancialYearId { get; set; }

        [StringLength(50)]
        public string SelectedActionStatus { get; set; }

        public int? EntryBy { get; set; }

        public DateTime? EntryDate { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }

}
