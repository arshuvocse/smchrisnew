using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class CompanyInformationDao
    {
        public Int32 CompanyId { set; get; }
        public string CompanyName { set; get; }
        public string ShortName { set; get; }
        public string Address { set; get; }
        public string ContactNo { set; get; }
        public string FaxNumber { set; get; }
        public string Pabx { set; get; }
        public string EmailAdress { set; get; }
        public string Hotline { set; get; }
        public string Description { set; get; }
        public string Remarks { set; get; }
        public string EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public string UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; } 
    }
}
