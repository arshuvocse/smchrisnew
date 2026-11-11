using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SalaryGradeDao
    {
        public Int32 SalaryGradeId { set; get; }
        public Int32 DesignationTypeId { set; get; }

        public Int32 EmpCategoryId { set; get; }
        public string GradeName { set; get; }
        public string GradeCode { set; get; }
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
    }
}
