using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class AppraisalSelfAppLogDAO
    {
        public int AppraisalSelfAppLogId { get; set; }
       public int AppraisalSelfMasterId { get; set; }
        public int BSCAppraisalSelfMasterId { get; set; }
        public int PreEmpInfoId { get; set; }
        public int ForEmpInfoId { get; set; }
        public int Version { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ActionStatus { get; set; }
        public string Comments { get; set; }
        public string ShowStatus { get; set; }

        public int CommentsEMP { get; set; }
    }


    public class EmpSkillWillAssessmentMasterAppLogDAO
    {
        public int EmpSkillWillAssessmentMasterAppLogId { get; set; }
        public int EmpSkillWillMasterId { get; set; }
        public int PreEmpInfoId { get; set; }
        public int ForEmpInfoId { get; set; }
        public int Version { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ActionStatus { get; set; }
        public string Comments { get; set; }

        public int CommentsEMP { get; set; }
    }
}
