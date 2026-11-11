using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.InternalCls;

using Library.DAO.HRM_Entities;

namespace Library.DAL.HRM_DAL
{
    public class WeeklyHolidayDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveWeeklyHoliday(WeeklyHoliday aWeeklyHoliday)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@WeeklyHolidayId", aWeeklyHoliday.WeeklyHolidayId));
            aSqlParameterlist.Add(new SqlParameter("@EmpId", aWeeklyHoliday.EmpId));
            aSqlParameterlist.Add(new SqlParameter("@FirstHolidayName", aWeeklyHoliday.FirstHolidayName));
            aSqlParameterlist.Add(new SqlParameter("@SecondHolidayName", aWeeklyHoliday.SecondHolidayName));
            aSqlParameterlist.Add(new SqlParameter("@DayQty", aWeeklyHoliday.DayQty));


            string insertQuery = @"insert into tblEmpWeeklyHoliday (WeeklyHolidayId,EmpId,FirstHolidayName,SecondHolidayName,DayQty) 
            values (@WeeklyHolidayId,@EmpId,@FirstHolidayName,@SecondHolidayName,@DayQty)";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");

        }
        public bool HasHolidayName(WeeklyHoliday aWeeklyHoliday)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpId", aWeeklyHoliday.EmpId));
            aSqlParameterlist.Add(new SqlParameter("@FirstHolidayName", aWeeklyHoliday.FirstHolidayName));
            aSqlParameterlist.Add(new SqlParameter("@SecondHolidayName", aWeeklyHoliday.SecondHolidayName));
            string query = "select * from dbo.tblEmpWeeklyHoliday where EmpId = @EmpId ";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, aSqlParameterlist, "HRDB");

            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    return true;
                }
            }
            return false;
        }
        public DataTable LoadWeeklyHolidayView()
        {
            string query = @"SELECT * FROM dbo.tblEmpWeeklyHoliday 
                             LEFT JOIN tblEmpGeneralInfo ON dbo.tblEmpWeeklyHoliday.EmpId=tblEmpGeneralInfo.EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadEmpMasterCode(string empid)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo where EmpInfoId='" + empid + "' and IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmpId(string EmpMasterCode)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo where EmpMasterCode='" + EmpMasterCode + "' and IsActive=1 and EmployeeStatus='Active'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public WeeklyHoliday WeeklyHolidayEditLoad(string WeeklyHolidayId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@WeeklyHolidayId", WeeklyHolidayId));
            string query = "select * from tblEmpWeeklyHoliday where WeeklyHolidayId = @WeeklyHolidayId";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, aSqlParameterlist, "HRDB");

            WeeklyHoliday aWeeklyHoliday = new WeeklyHoliday();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aWeeklyHoliday.WeeklyHolidayId = Int32.Parse(dataReader["WeeklyHolidayId"].ToString());
                    aWeeklyHoliday.SecondHolidayName = dataReader["SecondHolidayName"].ToString();
                    aWeeklyHoliday.FirstHolidayName = dataReader["FirstHolidayName"].ToString();
                    aWeeklyHoliday.DayQty = dataReader["DayQty"].ToString();
                    aWeeklyHoliday.EmpId = Convert.ToInt32(dataReader["EmpId"].ToString());
                }
            }
            return aWeeklyHoliday;
        }
        public bool UpdateWeeklyHolidayInfo(WeeklyHoliday aWeeklyHoliday)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@WeeklyHolidayId", aWeeklyHoliday.WeeklyHolidayId));
            aSqlParameterlist.Add(new SqlParameter("@EmpId", aWeeklyHoliday.EmpId));
            aSqlParameterlist.Add(new SqlParameter("@FirstHolidayName", aWeeklyHoliday.FirstHolidayName));
            aSqlParameterlist.Add(new SqlParameter("@SecondHolidayName", aWeeklyHoliday.SecondHolidayName));
            aSqlParameterlist.Add(new SqlParameter("@DayQty", aWeeklyHoliday.DayQty));

            string query = @"UPDATE tblEmpWeeklyHoliday SET FirstHolidayName=@FirstHolidayName,EmpId=@EmpId,SecondHolidayName=@SecondHolidayName,DayQty=@DayQty WHERE WeeklyHolidayId=@WeeklyHolidayId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
    }
}
