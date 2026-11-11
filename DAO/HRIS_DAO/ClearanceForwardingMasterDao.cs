using System;

namespace DAO.HRIS_DAO
{
    public class ClearanceForwardingMasterDao
    {
        public int ClearanceForwardingId { get; set; }
        public int CompanyId { get; set; }
        public long EmployeeId { get; set; }
        public int ForwardedBy { get; set; }
        public DateTime ForwardedDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
