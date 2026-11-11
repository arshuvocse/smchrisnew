using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SurveyQuestionSetupEntryDAO
    {
        public Int32 SurveyQuestionId { set; get; }
        public string QuestionTitle { set; get; }
        public Int32 SurveyQuestionTypeId { set; get; }

        public Int32 SurveyQuestionGroupId { set; get; }

        public bool IsActive { set; get; }
        public Int32 EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public Int32 UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }

         
    }
}
