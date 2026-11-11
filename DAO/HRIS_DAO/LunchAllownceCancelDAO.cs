using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class LunchAllownceCancelDAO
    {
        public int LunchAlllowCancelId { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int GuestInternInformationId { get; set; }
        public int EmpInfoId { get; set; }
        public int? CompanyId { get; set; }
        public int ? DivisionId { get; set; }
        public int? DivisionWId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? SubSectionId { get; set; }
        public string Remarks { get; set; }
        public string ActionStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }

    }
}
