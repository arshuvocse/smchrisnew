using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class MemotblJobCreationDAO
    {
        public Int32 MemoJobCreationId { set; get; }
        public Int32 JobCreationId { set; get; }
        public string HeaderTitle { set; get; }
        public DateTime HeaderDate { set; get; }
        public string FirstTitle { set; get; }
        public string SecondTitle { set; get; }
        public string Positiontitle { set; get; }
        public string WeOffer { set; get; }
        public string APPLYINSTRACTIONS { set; get; }
        public string NameDesignation { set; get; }
         
       
        public Int32 EntryBy { set; get; }
        public DateTime EntryDate { set; get; }
        public Int32 UpdateBy { set; get; }
        public DateTime UpdateDate { set; get; }

         
    }
}
