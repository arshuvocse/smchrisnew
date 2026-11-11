using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public  class MemoPrintIncrementDAO
    {
        public int MemoIncrementId { get; set; }
        public int CompanyId { get; set; }
        public int? IncrementId { get; set; }
        public string HeaderInfo { get; set; }
        public DateTime HeaderDate { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string PlaceofPosting { get; set; }
        public string PreviousStep { get; set; }
        public string IncrementalStep { get; set; }
        public string Salutation { get; set; }
        public string FirstParagraph { get; set; }
        public string ComplimentaryClose { get; set; }
        public string YoursSincerely { get; set; }
        public string Name { get; set; }
        public string CopyTo { get; set; }
        public int? ToEmployee { get; set; }
        public string CompanyName { get; set; }
        public string Subject { get; set; }

    }
}
