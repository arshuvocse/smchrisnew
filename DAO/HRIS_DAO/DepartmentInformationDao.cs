using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class DepartmentInformationDao
    {
        public Int32 CompanyId { set; get; }
        public Int32 DepartmentId { set; get; }
        public Int32 DivisionWId { set; get; }
        public string DepartmentName { set; get; }
        public string ShortName { set; get; }
        public bool IsActive { set; get; }
        public string Description { set; get; }
        public string Remarks { set; get; }
        public string EntryBy { set; get; }
        public string ApprovalStatus { set; get; }
        public DateTime EntryDate { set; get; }
        public string UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }
        public string ApproveBy { set; get; }
        public DateTime ApproveDate { set; get; }
        public bool Invisible { get; set; }
        public string Root { get; set; }
    }
}
