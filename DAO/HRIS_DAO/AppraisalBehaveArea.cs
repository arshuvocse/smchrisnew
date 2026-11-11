using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public   class AppraisalBehaveArea
    {

        public int AppraisalBehaveId { get; set; }
        public int AppraisalSelfMasterId { get; set; }
        public int BSCAppraisalSelfBehaveId { get; set; }

        public int? AppraisalMasterId { get; set; }

        public string SkillInfo { get; set; }
        public string SkillType { get; set; }

        public string SupportingEmp { get; set; }
        public string Comments { get; set; }

        public decimal? Score { get; set; }
        public decimal? SetScore { get; set; }
       
        public decimal? SelfScore { get; set; }
        public decimal? MidYearSuperScore { get; set; }
        public decimal SupervisorScore { get; set; }
    }
}
