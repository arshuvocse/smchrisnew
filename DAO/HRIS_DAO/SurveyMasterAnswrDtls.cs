using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class SurveyMasterAnswrDtls
    {

        public int SurveyQuestionAnswerId { get; set; }
        public Nullable<int> SurveyQuestionTitleId { get; set; }
        public Nullable<int> SurveyQuestionGroupId { get; set; }
        public string SurveyQuestionAnswer { get; set; }
        public Nullable<int> EntryBy { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
