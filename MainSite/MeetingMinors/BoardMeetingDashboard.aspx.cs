using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;

public partial class MeetingMinors_BoardMeetingDashboard : System.Web.UI.Page
{
  


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public class MeetingEntryDAO
    {
        //public string MeetingInfoID { get; set; }

     

        //public string CompanyId { get; set; }

        //public string CategoryID { get; set; }


        public string title { get; set; }
        

        public string  start { get; set; }

        public string end { get; set; }

      

       

    }

  

    [WebMethod(EnableSession = true)]
    public static MeetingEntryDAO[] GetMeetingCalender()
    {
        MeetingEntryDAL AMeetingEntryDal = new MeetingEntryDAL();
        DataTable aTable = AMeetingEntryDal.getMeetingCalenderDAL("");

        List<MeetingEntryDAO> aList = new List<MeetingEntryDAO>();

      
     
       

        if (aTable.Rows.Count > 0)
        {

            foreach (DataRow dr in aTable.Rows)
            {
                var aData = new MeetingEntryDAO();

                aData.title = dr["Title"].ToString();


                //+ ", Start Time: " + dr["StartTime"].ToString() + ", End Time: " + dr["EndTime"].ToString()
                //aData.Day = Convert.ToInt32(dr["Day"].ToString());
                aData.start = dr["MeetingDate"].ToString() ;

                aData.end = dr["MeetingDate"].ToString()  ;
               
               
               
                aList.Add(aData);

            }

            return aList.ToArray();
        }


        return aList.ToArray();

    }

}