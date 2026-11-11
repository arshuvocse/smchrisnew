using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class InterviewBoardGuestDetailsDao
    {
        public Int32 GuestBoardDetailsId { get; set; }
        public Int32 MasterId { get; set; }
        public string GuestName { get; set; }
        public string Company { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
    }
}
