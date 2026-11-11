using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class BenefitDetailDAO
    {
        public int BenefitDetailId { get; set; }
        public int BenefitMasterId { get; set; }
        public int EmpCategoryId { get; set; }
        public int SalaryGradeId { get; set; }
    }
}
