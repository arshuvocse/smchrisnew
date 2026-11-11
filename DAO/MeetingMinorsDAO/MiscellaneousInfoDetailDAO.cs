using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
    public class MiscellaneousInfoDetailDAO
    {
        public int MiscellaneousInfoDetailId { get; set; }

        public int? MiscellaneousInfoId { get; set; }

        public string Type { get; set; }

        public int? EmpInfoId { get; set; }

        public string EmpMasterCode { get; set; }

        public string EmpName { get; set; }

        public string Designation { get; set; }

    }
}
