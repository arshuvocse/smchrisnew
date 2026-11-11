using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
     public class TrainingOrganizationDao
    {
       

        public Int32 TrainingOrgId { set; get; }
        public Int32 OrgTypeId { set; get; }
        public string TrainingOrgName { set; get; }
        public string OrgAddress { set; get; }
        public bool IsForeign { set; get; }
        public bool IsLocal { set; get; }
        public bool IsInHouse { set; get; }
        public string TrainingLocation { set; get; }
        public string ConPersonName { set; get; }
        public string PhoneNo { set; get; }
        public string Email { set; get; }
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
