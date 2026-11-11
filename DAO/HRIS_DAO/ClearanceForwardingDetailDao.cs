namespace DAO.HRIS_DAO
{
    public class ClearanceForwardingDetailDao
    {
        public int ClearanceForwardingDetailId { get; set; }
        public int ClearanceForwardingId { get; set; }
        public long SupervisedEmployeeId { get; set; }
        public string Status { get; set; }
        public int? OrderNo { get; set; }
    }
}
