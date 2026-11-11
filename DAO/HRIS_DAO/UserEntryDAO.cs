using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class UserEntryDAO
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int UserTypeId { get; set; }
        public string  UserName { get; set; }
        public string Designation { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public string ActiveDate { get; set; }
        public string InActiveDate { get; set; }
        public string CompanyName { get; set; }
        public string UserType { get; set; }
        public string EmpMasterCode { get; set; }
        public int UserCategoryId { get; set; }
        public int EmpInfoId { get; set; }
        public bool IsActive { get; set; }
    }
}
