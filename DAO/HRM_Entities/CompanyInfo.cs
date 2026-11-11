using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class CompanyInfo
    {
        public int CompanyInfoId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string FaxNo { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
