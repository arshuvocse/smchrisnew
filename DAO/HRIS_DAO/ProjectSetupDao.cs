using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ProjectSetupDao
    {
        public Int32 ProjectId { set; get; }
        public Int32 CompanyId { set; get; }
        //public string CompanyName { get; set; }
        public string ProjectName { set; get; }
        public DateTime? ProjectStartDate { set; get; }

        public Boolean? IsOtherProject { set; get; }
        public Boolean? IsSMCFundedProjects { set; get; }
        public Boolean? IsSMCContract { set; get; }
        public Boolean? IsCompanyDirector { set; get; }

        public Boolean Parmanent { set; get; }
        public Boolean Temporary { set; get; }
        public DateTime? ProjectEndDate { set; get; }
        public string ProjectDescription { set; get; }
        public string Remarks { set; get; }
        public string EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public string UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }
        public bool IsActive { set; get; }


    }
}
