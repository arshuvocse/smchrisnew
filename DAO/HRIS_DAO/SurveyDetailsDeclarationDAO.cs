using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SurveyDetailsDeclarationDAO
    {
        public Int32 SurveyDetailsId { set; get; }
        public Int32 SurveyMasterId { set; get; }
        public Int32 SurveyQuestionId { set; get; }
        public  bool IsActive { set; get; }
       

         
    }
}
