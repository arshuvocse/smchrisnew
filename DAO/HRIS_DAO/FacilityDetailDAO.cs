using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class FacilityDetailDAO
    {
        public int FacilityDetailId { get; set; }
        public int FacilityMasterId { get; set; }
        public int EmpCategoryId { get; set; }
        public int SalaryGradeId { get; set; }
    }
}
