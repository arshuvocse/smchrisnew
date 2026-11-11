using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class Venue
    {
      public int SMCVenueID  { set; get; } 
      public string  VenueName  { set; get; } 
      public string Adress  { set; get; } 
      public bool IsActive  { set; get; } 
       public string EntryBy  { set; get; } 
      public DateTime EntryDate  { set; get; } 
      public string Updateby  { set; get; } 
       public DateTime Upatedate  { set; get; } 
      public bool IsDeleted  { set; get; } 
       public string DeleteBy  { set; get; } 
     public DateTime DeleteDate  { set; get; } 
     
    }
}
