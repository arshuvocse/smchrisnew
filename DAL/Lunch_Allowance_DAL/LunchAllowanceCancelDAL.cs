using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.Lunch_Allowance_DAL
{
    public class LunchAllowanceCancelDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable _LunchCancelList(string ID)
        {
            string queryStr = @"SELECT LAC.LunchAlllowCancelId,LAC.EmpInfoId,Emp.EmpMasterCode,Emp.EmpName,Designation,DepartmentName,LAC.ActionStatus,   case when  (DATEDIFF(DAY,CONVERT(DATE,LAC.EffectiveDate),CONVERT(DATE, getdate())))<10 then  1 else 0 end Checkk,   LAC.EffectiveDate EffectiveDate ,LAC.Remarks FROM dbo.tblLunchAllowCancel  LAC with (nolock)
LEFT JOIN tblEmpGeneralInfo Emp ON LAC.EmpInfoId = Emp.EmpInfoId
LEFT JOIN dbo.tblDesignation ON Emp.DesignationId=dbo.tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON Emp.DepartmentId=dbo.tblDepartment.DepartmentId
WHERE LAC.LunchAlllowCancelId IS NOT NULL AND LAC.EmpInfoId=" + ID + " ORDER BY EffectiveDate desc";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }




        public DataTable GetLunchCancelInfoByEmpId(int empinfoId, string Date)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@empinfoId", empinfoId));
            aSqlParameterlist.Add(new SqlParameter("@CancelDate", Date));
            string query = @"SELECT LAC.LunchAlllowCancelId,LAC.EmpInfoId,LAC.ActionStatus,CONVERT(varchar,LAC.EffectiveDate,106)EffectiveDate,LAC.Remarks FROM dbo.tblLunchAllowCancel  LAC  WHERE LAC.EmpInfoId=@empinfoId  AND  LAC.EffectiveDate=@CancelDate";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public Int32 SaveLunchAllowCancel(LunchAllownceCancelDAO allownceCancelDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", allownceCancelDao.EffectiveDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", allownceCancelDao.EmpInfoId ));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", allownceCancelDao.CompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", allownceCancelDao.DivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", allownceCancelDao.DivisionWId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", allownceCancelDao.DepartmentId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", allownceCancelDao.SectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", allownceCancelDao.SubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", allownceCancelDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", allownceCancelDao.ActionStatus));

            //aSqlParameterlist.Add(new SqlParameter("@ExpCompany", ExpDao.ExpCompany ?? (object)DBNull.Value));



            string insertQuery = @"INSERT INTO dbo.tblLunchAllowCancel
                                    (
                                        EffectiveDate,
                                        EmpInfoId,
                                        CompanyId,
                                        DivisionId,
                                        DivisionWId,
                                        DepartmentId,
                                        SectionId,
                                        SubSectionId,
                                        Remarks,ActionStatus
                                    )
                                    VALUES
                                    (   @EffectiveDate,
                                        @EmpInfoId,
                                        @CompanyId,
                                        @DivisionId,
                                        @DivisionWId,
                                        @DepartmentId,
                                        @SectionId,
                                        @SubSectionId,
                                        @Remarks,@ActionStatus
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }
        public bool DeleteLunchAllowCancel(LunchAllownceCancelDAO allownceCancelDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", allownceCancelDao.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", allownceCancelDao.EmpInfoId));
            

            //aSqlParameterlist.Add(new SqlParameter("@ExpCompany", ExpDao.ExpCompany ?? (object)DBNull.Value));



            string insertQuery = @"DELETE FROM dbo.tblLunchAllowCancel WHERE   CONVERT(Date,EffectiveDate)=CONVERT(Date,@EffectiveDate) AND EmpInfoId=@EmpInfoId ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(insertQuery, aSqlParameterlist, "HRDB");

        }

        public bool ApproveLunchAllowCancel(string LunchAlllowCancelId, string action, string comments, string lblDateData)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();



            aSqlParameterlist.Add(new SqlParameter("@LunchAlllowCancelId", LunchAlllowCancelId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", action));
            aSqlParameterlist.Add(new SqlParameter("@ReturnComments", comments));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", lblDateData));
            aSqlParameterlist.Add(new SqlParameter("@ApprovedBy", HttpContext.Current.Session["UserId"].ToString()));


            //aSqlParameterlist.Add(new SqlParameter("@ExpCompany", ExpDao.ExpCompany ?? (object)DBNull.Value));



            string insertQuery = @"UPDATE [dbo].[tblLunchAllowCancel]
   SET [ActionStatus] = @ActionStatus ,EffectiveDate=@EffectiveDate,
       [ApprovedBy] = @ApprovedBy ,
	  ReturnComments=@ReturnComments
      ,[ApprovedDate] = getdate()
 WHERE  LunchAlllowCancelId=@LunchAlllowCancelId ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(insertQuery, aSqlParameterlist, "HRDB");

        }
        public DataTable GetLunchAllowEmp(string date, string param)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@date", date));
            string query = @"SELECT  DATEDIFF ( day, @date  , ISNULL(ToDate,GETDATE()) ) DateDiffer, ISNULL(ToDate,GETDATE()) ToDateNew, '' Remarks, * ,'0'LunchAlllowCancelId FROM dbo.tblLunchAllowMaster
LEFT JOIN dbo.tblLunchAllowDetails ON tblLunchAllowDetails.LunchAllowID = tblLunchAllowMaster.LunchAllowID
LEFT JOIN dbo.tblEmpGeneralInfo e ON  e.EmpInfoId=dbo.tblLunchAllowDetails.EmployeeId
LEFT JOIN dbo.tblDesignation ON e.DesignationId=dbo.tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON e.DepartmentId=dbo.tblDepartment.DepartmentId
 WHERE @date BETWEEN fromDate AND ISNULL(ToDate,GETDATE())" + param;
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetLunchAllowEmpActive(string date, string date2, string param, string param22)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@date", date));
            aSqlParameterlist.Add(new SqlParameter("@date2", date2));
            string query = @"  select distinct * from (  SELECT '' fromDate, 0 GuestInternInformationId, e.EmpInfoId, tblcan.ActionStatus, DATEDIFF ( day, @date  , ISNULL(ToDate,GETDATE()) ) DateDiffer, ISNULL(ToDate,GETDATE()) ToDateNew, e.EmpMasterCode, e.EmpName, DivisionName, DepartmentName, Designation, '' Remarks, '0' LunchAlllowCancelId FROM dbo.tblLunchAllowMaster
LEFT JOIN dbo.tblLunchAllowDetails ON tblLunchAllowDetails.LunchAllowID = tblLunchAllowMaster.LunchAllowID
LEFT JOIN dbo.tblEmpGeneralInfo e ON  e.EmpInfoId=dbo.tblLunchAllowDetails.EmployeeId
LEFT JOIN dbo.tblDesignation ON e.DesignationId=dbo.tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON e.DepartmentId=dbo.tblDepartment.DepartmentId
LEFT JOIN dbo.tblDivision div ON e.DivisionId=div.DivisionId
left join (select EmpInfoId, CONVERT(date,EffectiveDate)EffectiveDate, ActionStatus from tblLunchAllowCancel)tblcan on CONVERT(date, tblLunchAllowDetails.fromDate)=tblcan.EffectiveDate and tblcan.EmpInfoId=tblLunchAllowDetails.EmployeeId
 WHERE  CONVERT(Date,@date)  between   CONVERT(Date,tblLunchAllowDetails.fromDate) and   CONVERT(Date,tblLunchAllowDetails.ToDate) and tblLunchAllowDetails.Status='Active'    " + param + @" union all

 SELECT     format( LunchToDate,'dd-MMM-yyyy') fromDate,   GuestInternInformationId,0 EmpInfoId, '' ActionStatus, DATEDIFF ( day, @date  , ISNULL(LunchToDate,GETDATE()) ) DateDiffer, ISNULL(LunchToDate,GETDATE()) ToDateNew, Type EmpMasterCode,  name EmpName,'' DivisionName, '' DepartmentName,'' Designation, '' Remarks, '0' LunchAlllowCancelId FROM dbo.tblunchGuestInternInformation
 
--LEFT JOIN dbo.tblEmpGeneralInfo e ON  e.EmpInfoId=dbo.tblLunchAllowDetails.EmployeeId
--LEFT JOIN dbo.tblDesignation ON e.DesignationId=dbo.tblDesignation.DesignationId
--LEFT JOIN dbo.tblDepartment ON e.DepartmentId=dbo.tblDepartment.DepartmentId
--LEFT JOIN dbo.tblDivision div ON e.DivisionId=div.DivisionId
 
 WHERE LunchDate  BETWEEN @date AND  @date2    " + param22 + ")tbl";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetLunchAllowCancelList(string param, string param2)
        {
            string query = @"select * from ( SELECT   LunchAlllowCancelId, com.ShortName, e.EmpMasterCode, e.EmpName ,DivisionName,DepartmentName,Designation,EffectiveDate,alo.ActionStatus FROM dbo.tblLunchAllowCancel alo
 
LEFT JOIN dbo.tblEmpGeneralInfo e ON  e.EmpInfoId=alo.EmpInfoId
LEFT JOIN dbo.tblCompanyInfo com ON  com.CompanyId=alo.CompanyId
LEFT JOIN dbo.tblDivision div ON e.DivisionId=div.DivisionId

LEFT JOIN dbo.tblDesignation ON e.DesignationId=dbo.tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON e.DepartmentId=dbo.tblDepartment.DepartmentId
 WHERE  alo.LunchAlllowCancelId is not null and alo.EmpInfoId not in (select EmployeeId   from tblLunchAllowDetails where  tblLunchAllowDetails.Status='Inactive'  ) " + param + @" union all

 SELECT   0 LunchAlllowCancelId,com.ShortName, alo.Type EmpMasterCode, alo.Name EmpName ,'' DivisionName,DepartmentName,'' Designation,alo.LunchDate EffectiveDate,'Cancel' ActionStatus FROM dbo.tblDellunchGuestInternInformation alo
 
 
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = alo.CompanyId
LEFT JOIN dbo.tblDivision div ON div.DivisionId = alo.DivisionId
LEFT JOIN dbo.tblDepartment dpt ON dpt.DepartmentId = alo.DepartmentId

 WHERE  alo.DELGuestInternInformationId is not null  " + param2 + ")tbl  order by  EffectiveDate desc ";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable GetLunchAllowEmpCheckSecond(DateTime date, DateTime ExcaTime)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@date", date));
            aSqlParameterlist.Add(new SqlParameter("@ExcaTime", ExcaTime));
            string query = @" SELECT  DATEDIFF ( SECOND,  @date,@ExcaTime ) CoutSecend ";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetLuchSetupTime()
        {

            string query = @" select LunchTime from tblLunchTimeSet where IsActive=1 ";
            return aCommonInternalDal.DataContainerDataTable(query,  DataBase.HRDB);
        }

        public DataTable GetLunchAllowEmpNew(DateTime StartDateTime, DateTime EndDateTime, string param)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@StartDateTime", StartDateTime));
            aSqlParameterlist.Add(new SqlParameter("@EndDateTime", EndDateTime));
            string query = @"
WITH DateRange(DateData) AS 
(
    SELECT @StartDateTime as Date
    UNION ALL
    SELECT DATEADD(d,1,DateData)
    FROM DateRange 
    WHERE DateData < @EndDateTime
)
--SELECT DateData
--FROM DateRange
--OPTION (MAXRECURSION 0)


select * from (SELECT '' ActionStatus,  EmpInfoId,EmpMasterCode,EmpName,Designation,DepartmentName, '' Remarks, '0'LunchAlllowCancelId FROM dbo.tblLunchAllowMaster
LEFT JOIN dbo.tblLunchAllowDetails ON tblLunchAllowDetails.LunchAllowID = tblLunchAllowMaster.LunchAllowID
LEFT JOIN dbo.tblEmpGeneralInfo e ON  e.EmpInfoId=dbo.tblLunchAllowDetails.EmployeeId
LEFT JOIN dbo.tblDesignation ON e.DesignationId=dbo.tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON e.DepartmentId=dbo.tblDepartment.DepartmentId

 WHERE @StartDateTime BETWEEN fromDate AND ISNULL(ToDate,GETDATE())   " + param + @")
 as tblt,DateRange OPTION (MAXRECURSION 0)" ;
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }



        public DataTable GetLunchAllowEmpReport( string param)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@param", param));
            string query = @" 

SELECT [DailyLaunchAllowenceId],
       e.EmpMasterCode, 
       e.EmpName, 
    tblDesignation.Designation
      ,com.ShortName,
      tblEmpCategory.EmpCategoryName
      ,tblEmployeeType.EmpType
      ,tblDepartment.DepartmentName
      ,tblSection.SectionName
      ,tblSubSection.SubSectionName
      ,tblDivisionWing.DivisionWingName
      ,[Date]
      ,[Foodrate]
      ,[Status]
      , 
	  CASE
    WHEN IsSMCEmp =1 THEN 'SMC Employee'
    WHEN IsSMCEmp =0 THEN 'Other Employee'
    
END AS IsSMCEmployee
  FROM [dbo].[tblDailyLaunchAllowence] dla

  LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId= dla.CompanyId
  LEFT JOIN dbo.tblEmpGeneralInfo e ON e.EmpInfoId= dla.EmployeeId
 LEFT JOIN dbo.tblDesignation ON dla.DesginationId=dbo.tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON dla.DepartmentId=dbo.tblDepartment.DepartmentId
LEFT JOIN dbo.tblEmpCategory ON dla.CategoryId=dbo.tblEmpCategory.EmpCategoryId
LEFT JOIN dbo.tblEmployeeType ON dla.TypeId=dbo.tblEmployeeType.EmpTypeId
LEFT JOIN dbo.tblSection ON dla.SectionId=dbo.tblSection.SectionId
LEFT JOIN dbo.tblSubSection ON dla.SubsectionId=dbo.tblSubSection.SubSectionId
LEFT JOIN dbo.tblDivisionWing ON dla.DivWingID=dbo.tblDivisionWing.DivisionWId

WHERE dla.DailyLaunchAllowenceId IS NOT NULL " + param;
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        DataAccessManager accessManager = new DataAccessManager();

        public DataTable Get_ReportLunch(string param, string ListToPivot)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@param", param));
                aList.Add(new SqlParameter("@ColumnToPivot", "TotalDays"));
                aList.Add(new SqlParameter("@ListToPivot", ListToPivot));
            
                dt = accessManager.GetDataTable("DynamicPivotBrandWiseDCR", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }
        public DataTable GetLunchAllowEmpReportNew(string param, string parm2, string Year, string Month)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@param", param));

            string query = "";

            if (Month == "January")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }

           
            if (Month == "February")
            {
                if (DateTime.IsLeapYear(Convert.ToInt32(Year)))
                {
                    query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
                }
                else
                {
                    query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
                }

                
            }


            if (Month == "March")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }

            if (Month == "April")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
     
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }
            if (Month == "May")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }


            if (Month == "June")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
 
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }


            if (Month == "July")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
 
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }


            if (Month == "August")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
 
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }


            if (Month == "September")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }




            if (Month == "October")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }



            if (Month == "November")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      
      
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }


            if (Month == "December")
            {
                query = @" 

SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS SI, [EmpMasterCode] [Employee ID]
      ,[EmpName] [Employee Name]
      ,[Designation]
      ,[DepartmentName] Department
      ,[Floor],'' [Batch/Room],
	  com.ShortName [Company]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      
      ,[Foodrate] Rate
      ,[TotalDays] [Total days]
      ,[TotalFoodrate] Amount, '' Reimburs, '' Adjustment, '' [To be deduct]


       
  FROM [dbo].[tblFoodRateReport]  mas with (nolock)
  left join  tblCompanyInfo com on com.CompanyId=mas.CompanyId

WHERE  mas.FoodRateReportId IS NOT NULL   " + param + parm2;
            }
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetLunchAllowCancel(string date,string empid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@date", date));
            aSqlParameterlist.Add(new SqlParameter("@empid", empid));
            string query = @"SELECT case when ActionStatus='Canceled' then ActionStatus+' ['+ReturnComments+']' when ActionStatus='Returned' then ActionStatus+' ['+ReturnComments+']' else ActionStatus end ActionStatus, Remarks FROM dbo.tblLunchAllowCancel with (nolock) WHERE CONVERT(Date,EffectiveDate)= CONVERT(Date,@date) AND EmpInfoId=@empid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetEmpInfo(string empinfoId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@empinfoId", empinfoId));
            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId=@empinfoId";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetAllDate(string From, string ToDate)
        {

            string query = @"SELECT  DateString FROM dbo.DateRange_To_TableSL ('" + From + "','" + ToDate + "') ";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public void LoadCompany(DropDownList ddl)
        {

            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);

        }

    }
}
