using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.MeetingMinorsDAO
{
    public class MeetingInfoAgendaDAO
    {
        public string MeetingInfoAgendaID { get; set; }

        public string MeetingInfoID { get; set; }

        public string Agenda { get; set; }
        public string Remarks { get; set; }
        public string Observation { get; set; }
        public string Decision { get; set; }

        public int? PresentorId { get; set; }
        public string ImplementationStatus { get; set; }
    }
}
