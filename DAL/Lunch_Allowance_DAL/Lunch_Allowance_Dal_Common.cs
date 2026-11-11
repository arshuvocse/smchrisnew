using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Lunch_Allowance_DAL
{
   public class Lunch_Allowance_Dal_Common
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetVacanceyEntryformationParam(string parm)
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy,  tblCompanyInfo.ShortName, * FROM tblYearlyHoliday hol
LEFT JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = hol.CompanyId
left JOIN  dbo.tblUser us   ON  hol.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  hol.UpdateBy =usUp.UserId WHERE (hol.IsDelete is NULL) OR (hol.IsDelete =0 ) " + parm + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }



        public DataTable _FoodRateView(string parm)
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy,  tblCompanyInfo.ShortName, cat.EmpCategoryName, * FROM tblFoodRate hol
LEFT JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = hol.CompanyId
LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = hol.EmployeeType
left JOIN  dbo.tblUser us   ON  hol.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  hol.UpdateBy =usUp.UserId WHERE (hol.IsDelete is NULL) OR (hol.IsDelete =0 ) " + parm + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable _GuestListView(string parm)
        {
            string queryStr = @"SELECT emp.EmpMasterCode+' : '+emp.EmpName EmpName, us.UserName EntryBy,   tblCompanyInfo.ShortName,dpt.DepartmentName,  * FROM tblunchGuestInternInformationMaster hol
LEFT JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = hol.CompanyId
LEFT JOIN dbo.tblDivision div ON div.DivisionId = hol.DivisionId
LEFT JOIN dbo.tblDepartment dpt ON dpt.DepartmentId = hol.DepartmentId


LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = hol.ReferancePersonId



 
left JOIN  dbo.tblUser us   ON  hol.EntryBy =us.UserId  
  WHERE  GuestInternInformationMasterId is not null " + parm + "   order by hol.EntryDate desc ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public int SavAppLog(int id, string Types)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@GuestInternInformationId", id));
                    aParameters.Add(new SqlParameter("@DelBy", HttpContext.Current.Session["UserId"].ToString()));
                    aParameters.Add(new SqlParameter("@Types", Types));
                  


                    string query = @"INSERT INTO [dbo].[tblDellunchGuestInternInformation]
           ([GuestInternInformationId]
           ,[Type]
           ,[Name]
           ,[CompanyId]
           ,[DivisionId]
           ,[DepartmentId]
           ,[LunchDate]
           ,[ReferancePersonId]
           ,[Remarks]
           ,[Types]
           ,[EntryBy]
           ,[EntryDate]
           ,[LunchToDate]
           ,[UpdateBy]
           ,[UpdateDate]
           ,[DelBy]
           ,[DelDate])
     select  [GuestInternInformationId]
           ,[Type]
           ,[Name]
           ,[CompanyId]
           ,[DivisionId]
           ,[DepartmentId]
           ,[LunchDate]
           ,[ReferancePersonId]
           ,[Remarks]
           ,@Types
           ,[EntryBy]
           ,[EntryDate]
           ,[LunchToDate]
           ,[UpdateBy]
           ,[UpdateDate]
           ,@DelBy
           ,getdate() from tblunchGuestInternInformation where GuestInternInformationId=@GuestInternInformationId";

                    pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }


                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public DataTable _LunchInfoView(string parm)
        {
            string queryStr = @"SELECT com.ShortName, dtls.Status, dtls.fromDate, dtls.ToDate, * FROM tblLunchAllowMaster mas
LEFT JOIN tblLunchAllowDetails dtls on  mas.LunchAllowID=dtls.LunchAllowID
LEFT JOIN tblEmpGeneralInfo e ON e.EmpInfoId=dtls.EmployeeId
LEFT JOIN dbo.tblCompanyInfo com on  com.CompanyId=dtls.CompanyId
 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
left JOIN  dbo.tblEmpCategory cat ON cat.EmpCategoryId = e.EmpCategoryId 

WHERE dtls.LunchAllowDetailsID IS NOT NULL " + parm + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
