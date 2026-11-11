using System;

namespace DAO.MeetingMinorsDAO
{
    public class RoutingPathSetupMaster
    {
        public int RoutingPathMaster_ID { get; set; }
        public string RoutingPath_Code { get; set; }
        public string RoutingPath_Name { get; set; }
        public int CompanyId { get; set; }
        public int DivisionId { get; set; }
        public int DepartmentId { get; set; }
        public int  CreateBy { get; set; }
        public DateTime  CreateDate { get; set; }
        public int ? UpdateBy { get; set; }
        public DateTime ? UpdateDate { get; set; }
    }
}