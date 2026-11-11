using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace DAL.MenuSetup_DAL
{
    public class UserCommonDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        ClsCommonInternalDAL _commonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetEMpInfos(string param, string param2)
        {
            string query = @"select distinct * from (SELECT e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
 WHERE   " + param + @"      union all  SELECT e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName  FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId

	inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE   e.IsActive=1  and     reff.ShowCompany in (ComAssain) 
      " + param2 + "  )tbl order by  EmpMasterCode ";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetEMpInfosOnlyView(string param, string param2)
        {
            string query = @"select Distinct * from (

SELECT  EmpImage EmpImageN, '~/UploadImg/'+ EmpImage EmpImage, e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName  FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
 WHERE   " + param + @"    
union all

SELECT  EmpImage EmpImageN, '~/UploadImg/'+ EmpImage EmpImage,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName  FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
  inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE  EmpMasterCode IS NOT  NULL and     reff.ShowCompany in (ComAssain)     " + param2 + @"   ) tbl ORDER BY  EmpMasterCode DESC ";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEMpInfosForGrupRewport(string param)
        {
            string query = @"SELECT com.ShortName, e.SMCOldCode,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, Jloc.Location Place, e.OfficialMobile, * FROM dbo.tblEmpGeneralInfo e WITH (NoLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblJobLocation Jloc ON Jloc.JobLocationID = e.JobLocationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId  
 WHERE   " + param + " ORDER BY e.EmpMasterCode DESC ";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetEMpInfosForGrupRewport_New(string param, string param2)
        {
            string query = @"select distinct * from (SELECT   com.ShortName, e.SMCOldCode,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, Jloc.Location Place, e.OfficialMobile  FROM dbo.tblEmpGeneralInfo e WITH (NoLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblJobLocation Jloc ON Jloc.JobLocationID = e.JobLocationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId  where EmpMasterCode IS NOT  NULL    " + param + @"   
 
union all 
SELECT   com.ShortName, e.SMCOldCode,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, Jloc.Location Place, e.OfficialMobile  FROM dbo.tblEmpGeneralInfo e WITH (NoLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblJobLocation Jloc ON Jloc.JobLocationID = e.JobLocationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId  
  inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE  EmpMasterCode IS NOT  NULL  and  e.IsActive=1  and     reff.ShowCompany in (ComAssain)
 and        reff.ShowCompany in (ComAssain)  " + param2 + @"          )tbl
 ORDER BY EmpMasterCode ASC";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEmpChildrenSpouseRewportGrade(string param, string BirthParam, string param2)
        {
            string query = @"select distinct * from (SELECT e.SpouseName,    cast((DATEDIFF(m, e.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, e.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, e.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength,CountChildern, cat.EmpCategoryName, com.ShortName, e.SMCOldCode,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, Jloc.Location Place, e.OfficialMobile,
CASE WHEN EXISTS (SELECT 1 FROM dbo.tblSpouse sp WHERE sp.EmpInfoId = e.EmpInfoId AND sp.IsActive = 1) THEN 1
  ELSE 0
END AS NoSpouse  FROM dbo.tblEmpGeneralInfo e WITH (NoLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblJobLocation Jloc ON Jloc.JobLocationID = e.JobLocationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId
LEFT JOIN  dbo.tblEmpCategory cat ON cat.EmpCategoryId = e.EmpCategoryId
LEFT JOIN (SELECT EmpInfoId, COUNT(EmpChildrenId) CountChildern FROM dbo.tblEmpChildren  WHERE  IsActive=1  
  "

              
                +BirthParam+"  GROUP BY EmpInfoId ) tblChil ON tblChil.EmpInfoId = e.EmpInfoId  "
  + "  WHERE e.EmpCategoryId=2   " + param + @"  Union all 


	 SELECT e.SpouseName,    cast((DATEDIFF(m, e.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, e.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, e.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength,CountChildern, cat.EmpCategoryName, com.ShortName, e.SMCOldCode,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, Jloc.Location Place, e.OfficialMobile,
CASE WHEN EXISTS (SELECT 1 FROM dbo.tblSpouse sp WHERE sp.EmpInfoId = e.EmpInfoId AND sp.IsActive = 1) THEN 1
  ELSE 0
END AS NoSpouse  FROM dbo.tblEmpGeneralInfo e WITH (NoLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblJobLocation Jloc ON Jloc.JobLocationID = e.JobLocationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId
LEFT JOIN  dbo.tblEmpCategory cat ON cat.EmpCategoryId = e.EmpCategoryId
inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
    
LEFT JOIN (SELECT EmpInfoId, COUNT(EmpChildrenId) CountChildern FROM dbo.tblEmpChildren  WHERE  IsActive=1 " + BirthParam + @"      GROUP BY EmpInfoId ) tblChil ON tblChil.EmpInfoId = e.EmpInfoId  
	 
	   WHERE e.EmpCategoryId=2   and  EmpMasterCode IS NOT  NULL  and  e.IsActive=1  and     reff.ShowCompany in (ComAssain)
 and        reff.ShowCompany in (ComAssain)    " + param2 + @" ) tbl   order by EmpMasterCode asc";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEmpChildrenSpouseRewportManagement(string param, string BirthParam, string param2)
        {
            string query = @"select distinct * from (SELECT e.SpouseName,    cast((DATEDIFF(m, e.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, e.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, e.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength,CountChildern, cat.EmpCategoryName, com.ShortName, e.SMCOldCode,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, Jloc.Location Place, e.OfficialMobile,
CASE WHEN EXISTS (SELECT 1 FROM dbo.tblSpouse sp WHERE sp.EmpInfoId = e.EmpInfoId AND sp.IsActive = 1) THEN 1
  ELSE 0
END AS NoSpouse  FROM dbo.tblEmpGeneralInfo e WITH (NoLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblJobLocation Jloc ON Jloc.JobLocationID = e.JobLocationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId
LEFT JOIN  dbo.tblEmpCategory cat ON cat.EmpCategoryId = e.EmpCategoryId
LEFT JOIN (SELECT EmpInfoId, COUNT(EmpChildrenId) CountChildern FROM dbo.tblEmpChildren  WHERE  IsActive=1  
  "


                + BirthParam + "  GROUP BY EmpInfoId ) tblChil ON tblChil.EmpInfoId = e.EmpInfoId  "
  + "  WHERE e.EmpCategoryId=1   " + param + @"  Union all 


	 SELECT e.SpouseName,    cast((DATEDIFF(m, e.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, e.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, e.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength,CountChildern, cat.EmpCategoryName, com.ShortName, e.SMCOldCode,  e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, Jloc.Location Place, e.OfficialMobile,
CASE WHEN EXISTS (SELECT 1 FROM dbo.tblSpouse sp WHERE sp.EmpInfoId = e.EmpInfoId AND sp.IsActive = 1) THEN 1
  ELSE 0
END AS NoSpouse  FROM dbo.tblEmpGeneralInfo e WITH (NoLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblJobLocation Jloc ON Jloc.JobLocationID = e.JobLocationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId
LEFT JOIN  dbo.tblEmpCategory cat ON cat.EmpCategoryId = e.EmpCategoryId
inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
    
LEFT JOIN (SELECT EmpInfoId, COUNT(EmpChildrenId) CountChildern FROM dbo.tblEmpChildren  WHERE  IsActive=1 " + BirthParam + @"      GROUP BY EmpInfoId ) tblChil ON tblChil.EmpInfoId = e.EmpInfoId  
	 
	   WHERE e.EmpCategoryId=1   and  EmpMasterCode IS NOT  NULL  and  e.IsActive=1  and     reff.ShowCompany in (ComAssain)
 and        reff.ShowCompany in (ComAssain)    " + param2 + @" ) tbl  order by EmpMasterCode asc";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetEMpInfosForLunchAllowance(string param, string ComId)
        {
            string query = @"SELECT Ldtls.EmployeeId EmployeeIdForCheck, tblFood.FoodRateId, tblFood.Rate, FORMAT(tblFood.Effectivedate,'dd-MMM-yyyy'), cat.EmpCategoryName, e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName,ISNULL(Ldtls.Status,'Active') AllowStatus,  ISNULL(FORMAT(Ldtls.fromDate,'dd-MMM-yyyy'),'') AllowFromDate, ISNULL(FORMAT(Ldtls.ToDate,'dd-MMM-yyyy'),'') AllowToDate, ISNULL(FORMAT(Ldtls.InactiveDate,'dd-MMM-yyyy'),'') InactiveDate, 
 * FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
left JOIN  dbo.tblEmpCategory cat ON cat.EmpCategoryId = e.EmpCategoryId 
LEFT JOIN tblLunchAllowDetails Ldtls ON e.EmpInfoId= Ldtls.EmployeeId
INNER JOIN  (SELECT tblFoodRate.EmployeeType,EffectiveDate,FoodRateId,Rate FROM dbo.tblFoodRate
INNER JOIN (SELECT EmployeeType,MAX(EffectiveDate)AS Dt from dbo.tblFoodRate GROUP BY EmployeeType) tblmax 
ON tblmax.EmployeeType = tblFoodRate.EmployeeType AND CONVERT(DATE,dbo.tblFoodRate.EffectiveDate)=CONVERT(DATE,tblmax.Dt) WHERE tblFoodRate.CompanyId='" + ComId + " '   AND  ((IsDelete=0) OR (IsDelete IS NULL))  )tblFood ON tblFood.EmployeeType=e.EmpCategoryId WHERE      e.EmpMasterCode IS NOT NULL   " + param + " ORDER BY e.EmpMasterCode DESC ";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetEMpInfosForLunchAllowanceDelLog(string param, string ComId)
        {
            string query = @"SELECT Ldtls.Rate, Ldtls.EmployeeId EmployeeIdForCheck,   cat.EmpCategoryName, e.EmployeeStatus, e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName,ISNULL(Ldtls.Status,'Active') AllowStatus,  ISNULL(FORMAT(Ldtls.fromDate,'dd-MMM-yyyy'),'') AllowFromDate, ISNULL(FORMAT(Ldtls.ToDate,'dd-MMM-yyyy'),'') AllowToDate, ISNULL(FORMAT(Ldtls.InactiveDate,'dd-MMM-yyyy'),'') InactiveDate, 
 * FROM  tblLunchAllowDetailsLog Ldtls WITH (NOLOCK)
 inner JOIN  dbo.tblEmpGeneralInfo e ON e.EmpInfoId= Ldtls.EmployeeId
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = e.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
left JOIN  dbo.tblEmpCategory cat ON cat.EmpCategoryId = e.EmpCategoryId 

  WHERE      e.EmpMasterCode IS NOT NULL  " + param + "  ORDER BY Ldtls.EntryDate, Ldtls.UpdateDate DESC ";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetUserList()
        {
            const string queryStr = @"SELECT u.UserId, u.CompanyId, u.UserTypeId, u.UserName, u.Designation, u.ContactNo, u.Email, u.Password, u.Remarks, u.UserCategoryId, u.IsActive, u.ActiveDate, u.InActiveDate, u.EmpInfoId, u.UserDataPermissionId,c.ShortName,ut.UserType FROM dbo.tblUser u
               INNER JOIN dbo.tblCompanyInfo c ON c.CompanyId = u.CompanyId INNER JOIN dbo.tblUserType ut ON ut.UserTypeId = u.UserTypeId";
            return _commonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadUserList(string param)
        {
            List<UserEntryDAO> lUser = new List<UserEntryDAO>();
              string queryStr = @"SELECT e.EmpName,   u.UserId,u.UserName,u.ContactNo,u.Email,u.Remarks,u.IsActive ,
CONVERT(NVARCHAR(20),u.ActiveDate,106) AS ActiveDate,
CONVERT(NVARCHAR(20),u.InActiveDate,106) AS InActiveDate  ,
 ut.UserType,e.EmpMasterCode
FROM dbo.tblUser u WITH (NOLOCK)
INNER JOIN dbo.tblUserType ut   WITH (NOLOCK)  ON ut.UserTypeId = u.UserTypeId
LEFT JOIN dbo.tblEmpGeneralInfo e   WITH (NOLOCK)  ON e.EmpInfoId = u.EmpInfoId WHERE UserId<>'1'"+ param + " ORDER BY u.UserId"  ;


            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable LoadUserListActive(String comId)
        {

            //            string queryStr = @"SELECT e.EmpName, u.Password,    u.UserId,u.UserName,u.ContactNo,u.Email,u.Remarks,u.IsActive ,
            //CONVERT(NVARCHAR(20),u.ActiveDate,106) AS ActiveDate,
            //CONVERT(NVARCHAR(20),u.InActiveDate,106) AS InActiveDate  ,
            // ut.UserType,e.EmpMasterCode
            //FROM dbo.tblUser u WITH (NOLOCK)
            //INNER JOIN dbo.tblUserType ut  WITH (NOLOCK) ON ut.UserTypeId = u.UserTypeId
            //LEFT JOIN dbo.tblEmpGeneralInfo e  WITH (NOLOCK) ON e.EmpInfoId = u.EmpInfoId WHERE UserId  not in(1,10) and u.IsActive=1 and e.CompanyId=" + comId + " ORDER BY u.UserName";

            string queryStr = @"SELECT e.EmpName, u.Password, u.UserId, u.UserName, u.ContactNo, u.Email, u.Remarks, u.IsActive,
CONVERT(NVARCHAR(20),u.ActiveDate,106) AS ActiveDate,
CONVERT(NVARCHAR(20),u.InActiveDate,106) AS InActiveDate,
ut.UserType, e.EmpMasterCode
FROM dbo.tblUser u WITH (NOLOCK)
INNER JOIN dbo.tblUserType ut WITH (NOLOCK) ON ut.UserTypeId = u.UserTypeId
LEFT JOIN dbo.tblEmpGeneralInfo e WITH (NOLOCK) ON e.EmpInfoId = u.EmpInfoId
WHERE UserId NOT IN (1,10) 
AND u.IsActive = 1 
AND e.CompanyId = " + comId + @"

UNION ALL

SELECT e.EmpName, u.Password, u.UserId, u.UserName, u.ContactNo, u.Email, u.Remarks, u.IsActive,
CONVERT(NVARCHAR(20),u.ActiveDate,106) AS ActiveDate,
CONVERT(NVARCHAR(20),u.InActiveDate,106) AS InActiveDate,
ut.UserType, e.EmpMasterCode
FROM dbo.tblUser u WITH (NOLOCK)
INNER JOIN dbo.tblUserType ut WITH (NOLOCK) ON ut.UserTypeId = u.UserTypeId
LEFT JOIN dbo.tblEmpGeneralInfo e WITH (NOLOCK) ON e.EmpInfoId = u.EmpInfoId
INNER JOIN tblEmpAllRefference reff ON e.EmpInfoId = reff.RefferenceEmpId
INNER JOIN (
    SELECT NewEmployeeId,
           CASE 
               WHEN IsSMCRecordView = 1 THEN '1'
               WHEN IsELRecordView = 1 THEN '2'
               ELSE '1,2'
           END ComAssain
    FROM tblEmpSpecialTransfer
    WHERE OnlyView = 1
) tblPer ON reff.EmployeeId = tblPer.NewEmployeeId
WHERE UserId NOT IN (1,10)
AND u.IsActive = 1
AND reff.ShowCompany IN (ComAssain)

ORDER BY u.UserName";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        
        public DataTable LoadUserListActiveNEqw(String comId)
        {

            string queryStr = @"select distinct  * from (
SELECT e.EmpName, u.Password,    u.UserId,u.UserName,u.ContactNo,u.Email,u.Remarks,u.IsActive ,
CONVERT(NVARCHAR(20),u.ActiveDate,106) AS ActiveDate,
CONVERT(NVARCHAR(20),u.InActiveDate,106) AS InActiveDate  ,
 ut.UserType,e.EmpMasterCode
FROM dbo.tblUser u WITH (NOLOCK)
INNER JOIN dbo.tblUserType ut  WITH (NOLOCK) ON ut.UserTypeId = u.UserTypeId
LEFT JOIN dbo.tblEmpGeneralInfo e  WITH (NOLOCK) ON e.EmpInfoId = u.EmpInfoId WHERE UserId  not in(1,10) and u.IsActive=1  and  EmpMasterCode IS NOT  NULL  and e.CompanyId=" + comId + @" 


union all

SELECT e.EmpName, u.Password,    u.UserId,u.UserName,u.ContactNo,u.Email,u.Remarks,u.IsActive ,
CONVERT(NVARCHAR(20), u.ActiveDate, 106) AS ActiveDate,
CONVERT(NVARCHAR(20), u.InActiveDate, 106) AS InActiveDate,
 ut.UserType,e.EmpMasterCode
FROM dbo.tblUser u WITH(NOLOCK)
INNER JOIN dbo.tblUserType ut  WITH(NOLOCK) ON ut.UserTypeId = u.UserTypeId
LEFT JOIN dbo.tblEmpGeneralInfo e  WITH(NOLOCK) ON e.EmpInfoId = u.EmpInfoId
inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId
    inner join(select NewEmployeeId, case when IsSMCRecordView= 1 then '1'   when IsELRecordView = 1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView = 1  ) tblPer on reff.EmployeeId = tblPer.NewEmployeeId
 WHERE EmpMasterCode IS NOT  NULL and  e.IsActive = 1  and reff.ShowCompany in (ComAssain)
 )tbl  order by EmpMasterCode asc ";


            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public List<tblEmpGeneralInfo> LoadEmployeeInfoList()
        {
            List<tblEmpGeneralInfo> lEmp = new List<tblEmpGeneralInfo>();
            string queryStr = @"SELECT * FROM dbo.tblEmpGeneralInfo e WHERE e.IsActive=1 AND CompanyId IN (SELECT CompanyId FROM  dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";


            using (DataTable dt = _commonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tblEmpGeneralInfo emp = new tblEmpGeneralInfo();
                        emp.EmpInfoId = int.Parse(dt.Rows[i]["EmpInfoId"].ToString());
                        emp.EmpMasterCode = dt.Rows[i]["EmpMasterCode"].ToString();
                        emp.EmpName = dt.Rows[i]["EmpName"].ToString();
                        emp.FatherName = dt.Rows[i]["FatherName"].ToString();
                        emp.IsActive = bool.Parse(dt.Rows[i]["IsActive"].ToString());

                        lEmp.Add(emp);
                    }
                }
            }
            return lEmp;
        }
    }
}
