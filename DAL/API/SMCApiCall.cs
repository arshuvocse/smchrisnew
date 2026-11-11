using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAO.HRIS_DAO;
using Newtonsoft.Json;

namespace DAL.API
{
   public class SMCApiCall
    {

        private HttpClient _httpClient;
        public SMCApiCall()
        {
            this._httpClient = new HttpClient();
        }


        //Getting Attendance Reports by filter EmployeeCode,FromDate,ToDate
        public async Task<List<SmcAttendance>> GetAttendanceData(string EmployeeID, string date_form, string date_to)
        {
            List<SmcAttendance> aList = null;
            try
            {
                var response = await _httpClient.GetStringAsync("http://182.160.103.237:8089/smclms/index.php/api/attendanceReport/index/" + EmployeeID + "/" + date_form + "/" + date_to + "");
                aList = JsonConvert.DeserializeObject<List<SmcAttendance>>(response);
            }
            catch (Exception ex)
            {

            }

            return aList;
        }

        //Getting Leave Reports by filter EmployeeCode
        // Date format 2021-06-12
        public async Task<List<SmcLeaveData>> GetLeaveData(string EmployeeID)
        {
            List<SmcLeaveData> aList = null;
            try
            {
                var response = await _httpClient.GetStringAsync("http://182.160.103.237:8089/smclms/index.php/api/leaveReport/index/" + EmployeeID + "");
                aList = JsonConvert.DeserializeObject<List<SmcLeaveData>>(response);
            }
            catch (Exception ex)
            {

            }

            return aList;
        }

    }
}
