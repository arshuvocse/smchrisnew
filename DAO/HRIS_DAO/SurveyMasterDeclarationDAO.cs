using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SurveyMasterDeclarationDAO
    {
        public Int32 SurveyMasterId { set; get; }
        public Int32 CompanyId { set; get; }
        public Int32 FinancialYearId { set; get; }
        public string SurveyName { set; get; }
        public  DateTime SurveyFrom { set; get; }
        public  DateTime SurveyTo { set; get; }
        public bool IsActive { set; get; }
        public Int32 EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public Int32 UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }


        public bool IsDelete { set; get; }
        public Int32 DeleteBy { set; get; }
        public DateTime DeleteDate { set; get; }

         
    }
}
