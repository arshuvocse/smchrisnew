using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAL.DataManager;

namespace DAL.Chart_DAL
{
    public class ChartDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetGender(string Parm)
        {
            string queryStr = @"SELECT Gender,COUNT(*)Total FROM dbo.tblEmpGeneralInfo  with (nolock) WHERE Gender IS NOT NULL  and EmployeeStatus='Active' AND tblEmpGeneralInfo.IsActive='1' " + Parm + " GROUP BY Gender";
            return aCommonInternalDal.DataContainerDataTable(queryStr,  "HRDB");
        }

        public DataTable GetEmployeeType(string param)
        {
            string queryStr = @"SELECT EmpType,COUNT(*)Total FROM dbo.tblEmpGeneralInfo   with (nolock) 
            LEFT JOIN dbo.tblEmployeeType ON tblEmployeeType.EmpTypeId = tblEmpGeneralInfo.EmpTypeId
            WHERE (EmpType IS NOT NULL " + param + ") AND EmployeeStatus='Active' AND tblEmpGeneralInfo.IsActive='1'  GROUP BY EmpType UNION ALL SELECT 'Program Contractual',COUNT(*)Total FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE IsProgramContractual=1 " + param + " AND EmployeeStatus='Active' AND tblEmpGeneralInfo.IsActive='1'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetEmployeeGrade(string param)
        {
            string queryStr = @"SELECT GradeCode,COUNT(*)Total    FROM dbo.tblEmpGeneralInfo    with (nolock) 
            LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
            WHERE GradeCode IS NOT NULL AND EmployeeStatus='Active' AND tblEmpGeneralInfo.IsActive='1' " + param + " GROUP BY GradeCode, dbo.tblSalaryGrade.SalaryGradeId ORDER BY dbo.tblSalaryGrade.SalaryGradeId ASC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetJoinedEmployee(string year,string param)
        {
            string queryStr = @"SELECT FORMAT(DateOfJoin,'MMM')JoiningMonth,COUNT(*)Total FROM dbo.tblEmpGeneralInfo    with (nolock) 
WHERE YEAR(DateOfJoin)='" + year + "' " + param + " GROUP BY FORMAT(DateOfJoin,'MMM') ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetBirthYearEmployee(string param)
        {
            string queryStr = @"SELECT  '20-30'Interval,COUNT(*)Total FROM
            (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '20' AND '30' " +
            " UNION ALL "+
            " SELECT  '31-40'Interval,COUNT(*)Total FROM "+
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '31' AND '40' " +
            " UNION ALL "+
            " SELECT  '41-50'Interval,COUNT(*)Total FROM "+
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '41' AND '50' " +
            " UNION ALL "+
            " SELECT  '51-60'Interval,COUNT(*)Total FROM "+
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '51' AND '60'"

            +
            " UNION ALL " +
            " SELECT  '61-70'Interval,COUNT(*)Total FROM " +
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '61' AND '70'"

              +
            " UNION ALL " +
            " SELECT  '71-80'Interval,COUNT(*)Total FROM " +
            " (SELECT DATEDIFF(YEAR,DateOfBirth,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '71' AND '80'"
            ;


            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetJoiningYearEmployee(string param)
        {
             
                string queryStr =
                    @"SELECT  '0-5'Interval,COUNT(*)Total FROM
            (SELECT DATEDIFF(YEAR,DateOfJoin,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE IsActive='1' AND EmployeeStatus='Active' " +
                    param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '0' AND '5' " +
                    " UNION ALL " +
                    " SELECT  '6-10'Interval,COUNT(*)Total FROM " +
                    " (SELECT DATEDIFF(YEAR,DateOfJoin,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " +
                    param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '6' AND '10' " +
                    " UNION ALL " +
                    " SELECT  '11-15'Interval,COUNT(*)Total FROM " +
                    " (SELECT DATEDIFF(YEAR,DateOfJoin,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " +
                    param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '11' AND '15' " +
                    " UNION ALL " +
                    " SELECT  '16-20'Interval,COUNT(*)Total FROM " +
                    " (SELECT DATEDIFF(YEAR,DateOfJoin,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " +
                    param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '16' AND '20' " +
                    " UNION ALL " +
                    " SELECT  '21-25'Interval,COUNT(*)Total FROM " +
                    " (SELECT DATEDIFF(YEAR,DateOfJoin,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " +
                    param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '21' AND '25' " +
                    " UNION ALL " +
                    " SELECT  '26-30'Interval,COUNT(*)Total FROM " +
                    " (SELECT DATEDIFF(YEAR,DateOfJoin,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " +
                    param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '26' AND '30' " +
                    " UNION ALL " +
                    " SELECT  '31-35'Interval,COUNT(*)Total FROM " +
                    " (SELECT DATEDIFF(YEAR,DateOfJoin,GETDATE())AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " +
                    param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '31' AND '35'";

                return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
             
            


              
        }

        public DataTable GetJoiningYearEmployeeByJoiningDate(string param, string dDate)
        {
            string queryStr2 = @"SELECT  '0-5'Interval,COUNT(*)Total FROM
            (SELECT DATEDIFF(YEAR,DateOfJoin,'" + dDate + "')AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '0' AND '5' " +
" UNION ALL " +
" SELECT  '6-10'Interval,COUNT(*)Total FROM " +
" (SELECT DATEDIFF(YEAR,DateOfJoin,'" + dDate + "')AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '6' AND '10' " +
" UNION ALL " +
" SELECT  '11-15'Interval,COUNT(*)Total FROM " +
" (SELECT DATEDIFF(YEAR,DateOfJoin,'" + dDate + "')AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '11' AND '15' " +
" UNION ALL " +
" SELECT  '16-20'Interval,COUNT(*)Total FROM " +
" (SELECT DATEDIFF(YEAR,DateOfJoin,'" + dDate + "')AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '16' AND '20' " +
" UNION ALL " +
" SELECT  '21-25'Interval,COUNT(*)Total FROM " +
" (SELECT DATEDIFF(YEAR,DateOfJoin,'" + dDate + "')AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '21' AND '25' " +
" UNION ALL " +
" SELECT  '26-30'Interval,COUNT(*)Total FROM " +
" (SELECT DATEDIFF(YEAR,DateOfJoin,'" + dDate + "')AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '26' AND '30' " +
" UNION ALL " +
" SELECT  '31-35'Interval,COUNT(*)Total FROM " +
" (SELECT DATEDIFF(YEAR,DateOfJoin,'" + dDate + "')AS Years FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsActive='1' AND EmployeeStatus='Active' " + param + ")AS tbltemp WHERE tbltemp.Years BETWEEN '31' AND '35'";
            return aCommonInternalDal.DataContainerDataTable(queryStr2, "HRDB");
        }

        public DataTable GetJobLeftEmployee(string year,string param)
        {
            string queryStr = @"SELECT FORMAT(JobLeftDate,'yyyy')JobLeftMonth,COUNT(*)Total FROM dbo.tblEmployeeJobLeft  WITH (NOLOCK) 
WHERE YEAR(JobLeftDate)='" + year + "'  " + param + " GROUP BY FORMAT(JobLeftDate,'yyyy')";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetJobLeftEmployeeByDeptYear( string param)
        {
            string queryStr = @"SELECT  FORMAT(JobLeftDate,'yyyy')JobLeftMonth,COUNT(*)Total FROM dbo.tblEmployeeJobLeft    with (nolock) 
LEFT JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblEmployeeJobLeft.EmployeeId
WHERE  " + param+"  GROUP BY FORMAT(JobLeftDate,'yyyy'),YEAR(JobLeftDate)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetTrainingEmployee(int com)
        {
            string queryStr = @" SELECT * FROM dbo.tblTrainingMaster    with (nolock)  WHERE ( IsDelete IS NULL OR IsDelete = 0) AND TrainingEnd  >= GETDATE() and  CompanyId='" + com + "' ORDER BY TrainingMasterId DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        
        }
        public DataTable GetEmployeeJoinLeftRetireInfo(int company)
        {
            string queryStr = @"SELECT tbltemp.Month,   SUM(tbltemp.PermJoin)PermJoin,  
 SUM(tbltemp.ContJoin)ContJoin,    SUM(tbltemp.PermContJoin)PermContJoin,    
 SUM(tbltemp.LeftPer)LeftPer,    SUM(tbltemp.LeftCont)LeftCont,    SUM(tbltemp.LeftPermCont)LeftPermCont,     SUM(tbltemp.RetirePerm)RetirePerm,    
 SUM(tbltemp.RetireCont)RetireCont,    SUM(tbltemp.RetirePermCont)RetirePermCont,    (SUM(tbltemp.PermJoin)+     SUM(tbltemp.ContJoin)+    
  SUM(tbltemp.PermContJoin)+     SUM(tbltemp.LeftPer)+     SUM(tbltemp.LeftCont)+     SUM(tbltemp.LeftPermCont)+    SUM(tbltemp.RetirePerm)+   
    SUM(tbltemp.RetireCont)+    SUM(tbltemp.RetirePermCont))Total,tbltemp.MonthNo FROM (
	
	--
	SELECT MONTH(DateOfJoin)MonthNo,
	FORMAT(DateOfJoin,'MMMM')Month,COUNT(*)PermJoin,'0'ContJoin,'0'PermContJoin, '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont 
	 FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE   EmpTypeId='1' AND  year(DateOfJoin)= YEAR(GETDATE())  AND CompanyId='" + company + "' GROUP BY  FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin) " + @"
	 
	 
	  UNION ALL  SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,'0'PermJoin,COUNT(*)ContJoin,'0'PermContJoin,  '0'LeftPer,'0'LeftCont,
	  '0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo  with (nolock) WHERE  EmpTypeId='2' AND  year(DateOfJoin)=YEAR(GETDATE())  AND 
	  CompanyId='" + company + @"' GROUP BY  FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin)  
	  
	  
	  UNION ALL  SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,
	  '0'PermJoin,'0'ContJoin,COUNT(*)PermContJoin, '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo    with (nolock) 
	   WHERE IsProgramContractual='1' AND  year(DateOfJoin)= YEAR(GETDATE())  AND CompanyId='" + company + @"' GROUP BY  FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin)
	    
--


		 UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,
		 '0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft    with (nolock)   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  EmpTypeId='1'
		  and year(SubmissionDate)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"' GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM') 
		  

		
		  
		  
		    UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  '0'LeftPer,COUNT(*)LeftCont,'0'LeftPermCont,
			'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft    with (nolock)   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId 
			  WHERE  EmpTypeId='2' and year(SubmissionDate)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')  
			  
			  
			   UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  '0'LeftPer,'0'LeftCont,COUNT(*)LeftPermCont,
			   '0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft    with (nolock)   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId 
			   WHERE IsProgramContractual='1' and year(SubmissionDate)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')  
			   
			   
			   ---
			   
			    UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,
				  COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,COUNT(*)RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft  WITH (NOLOCK) 
				   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId 
				   WHERE  EmpTypeId='1' AND JobLeftTypeId='2' and year(SubmissionDate)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"'
				   
				    GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')   UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,
					FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,COUNT(*)RetireCont,'0'RetirePermCont  
					FROM dbo.tblEmployeeJobLeft    with (nolock)    LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId   WHERE  EmpTypeId='2' AND JobLeftTypeId='2'
					 and year(SubmissionDate)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')  
					
					
					  UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, 
					   '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,COUNT(*)RetirePermCont  FROM dbo.tblEmployeeJobLeft     with (nolock) 
					    LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  IsProgramContractual='1'
						 AND JobLeftTypeId='2' and year(SubmissionDate)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"'
						 
						 
						  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM') )AS tbltemp GROUP BY tbltemp.Month,tbltemp.MonthNo  
						  
						  
						  
						  
						  
						  
						  
						   UNION ALL  SELECT 'Total'Month,SUM(tbltemmm.PermJoin)PermJoin,    SUM(tbltemmm.ContJoin)ContJoin,    
						   SUM(tbltemmm.PermContJoin)PermContJoin,     SUM(tbltemmm.LeftPer)LeftPer,     SUM(tbltemmm.LeftCont)LeftCont,    SUM(tbltemmm.LeftPermCont)LeftPermCont,    
						   SUM(tbltemmm.RetirePerm)RetirePerm,     SUM(tbltemmm.RetireCont)RetireCont,     SUM(tbltemmm.RetirePermCont)RetirePermCont,     (SUM(tbltemmm.PermJoin)+     SUM(tbltemmm.ContJoin)+    SUM(tbltemmm.PermContJoin)+    SUM(tbltemmm.LeftPer)+    SUM(tbltemmm.LeftCont)+    SUM(tbltemmm.LeftPermCont)+    SUM(tbltemmm.RetirePerm)+     SUM(tbltemmm.RetireCont)+    SUM(tbltemmm.RetirePermCont))Total,'13'MonthNo FROM (SELECT     SUM(tbltemp.PermJoin)PermJoin,    SUM(tbltemp.ContJoin)ContJoin,    SUM(tbltemp.PermContJoin)PermContJoin,    SUM(tbltemp.LeftPer)LeftPer,
						       SUM(tbltemp.LeftCont)LeftCont,    SUM(tbltemp.LeftPermCont)LeftPermCont,    SUM(tbltemp.RetirePerm)RetirePerm,    SUM(tbltemp.RetireCont)RetireCont,   
							    SUM(tbltemp.RetirePermCont)RetirePermCont,    (SUM(tbltemp.PermJoin)+    SUM(tbltemp.ContJoin)+    SUM(tbltemp.PermContJoin)+    SUM(tbltemp.LeftPer)+   
								 SUM(tbltemp.LeftCont)+    SUM(tbltemp.LeftPermCont)+    SUM(tbltemp.RetirePerm)+    SUM(tbltemp.RetireCont)+    SUM(tbltemp.RetirePermCont))Total FROM
								  (SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,COUNT(*)PermJoin,'0'ContJoin,'0'PermContJoin,  '0'LeftPer,'0'LeftCont,'0'LeftPermCont,
								  '0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  EmpTypeId='1'  and year(DateOfJoin)= YEAR(GETDATE())  AND
								   tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin)  UNION ALL  SELECT MONTH(DateOfJoin)MonthNo,
								   FORMAT(DateOfJoin,'MMMM')Month,'0'PermJoin,COUNT(*)ContJoin,'0'PermContJoin,  '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,
								   '0'RetirePermCont  FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  EmpTypeId='2' and year(DateOfJoin)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"' 
								    GROUP BY FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin)  UNION ALL  SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,'0'PermJoin,'0'ContJoin,
									COUNT(*)PermContJoin,  '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  
									FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  WHERE  IsProgramContractual='1' and year(DateOfJoin)= YEAR(GETDATE())  AND 
									tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin)  
									
									UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,
									'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft  WITH (NOLOCK) 
									  LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId
									   WHERE  EmpTypeId='1' and year(DateOfJoin)= YEAR(GETDATE()) 
									 AND tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')   
									 
									 UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, 
									  '0'LeftPer,COUNT(*)LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft   WITH (NOLOCK) 
									   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId   WHERE  EmpTypeId='2' 
									   AND year(DateOfJoin)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM') 
									   
									     UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  
										 '0'LeftPer,'0'LeftCont,COUNT(*)LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft    WITH (NOLOCK) 
										 LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE IsProgramContractual='1'
										  and year(DateOfJoin)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + @"'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')  
										   UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,COUNT(*)RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft  WITH (NOLOCK)   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  EmpTypeId='1' AND JobLeftTypeId='2' and year(DateOfJoin)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + "'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')   UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,COUNT(*)RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft  WITH (NOLOCK)   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId   WHERE  EmpTypeId='2' AND JobLeftTypeId='2' and year(DateOfJoin)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + "'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM')   UNION ALL  SELECT MONTH(SubmissionDate)MonthNo,FORMAT(SubmissionDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin,  '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,COUNT(*)RetirePermCont  FROM dbo.tblEmployeeJobLeft  WITH (NOLOCK)   LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  IsProgramContractual='1' AND JobLeftTypeId='2' and year(DateOfJoin)= YEAR(GETDATE())  AND tblEmpGeneralInfo.CompanyId='" + company + "'  GROUP BY MONTH(SubmissionDate),FORMAT(SubmissionDate,'MMMM') )AS tbltemp GROUP BY tbltemp.Month,tbltemp.MonthNo ) AS tbltemmm";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public void GetDDLComDepartment(DropDownList ddl, string comapnyId)
        {
            string query = @"SELECT d.DepartmentId  , d.DepartmentName   FROM dbo.tblDepartment d    with (nolock)  WHERE  (d.Invisible IS NULL OR d.Invisible=0) and d.CompanyId='" + comapnyId + "'";

            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, "HRDB");
        }


        public DataTable GetDdlDepartmentByCompanyId(string selecteddiv)
        { 
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", selecteddiv));
            //string query = @"SELECT dept.DepartmentId as Value,dept.ShortName,dept.DepartmentName as TextField FROM dbo.tblDepartment dept WHERE dept.CompanyId='" + CompanyId + "' AND DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            string query = @"SELECT DepartmentId as Value,DepartmentName as TextField FROM tblDepartment    with (nolock) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetComapnyNameList()
        {
            string queryStr = "SELECT CompanyId as Value,CompanyName, ShortName as TextField FROM tblCompanyInfo    with (nolock)  WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";


            return aCommonInternalDal.GetDTforDDLForChartCompanyLoad(queryStr, null, DataBase.HRDB);
        }

        public DataTable GetDdlComDivision(string comId)
        { 
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", comId));

            string query = "";

            if (HttpContext.Current.Session["UserTypeId"].ToString() =="4")
            {
                query = @"SELECT d.DivisionId AS Value, d.DivisionName AS TextField FROM dbo.tblDivision d     with (nolock) 

WHERE d.CompanyId=@cid";
            }
            else
            {
                query = @"SELECT d.DivisionId AS Value, d.DivisionName AS TextField FROM dbo.tblDivision d     with (nolock) 
inner JOIN dbo.tblEmpGeneralInfo e ON d.DivisionId=e.DivisionId
WHERE d.CompanyId=@cid and e.EmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";
            }


            
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
    
        public DataTable GetUserDashBoardSetting(string userId)
        {
            string query = @"SELECT tblDashboardSetting.* FROM dbo.tblUser    with (nolock) 
LEFT JOIN dbo.tblDashboardSetting ON dbo.tblUser.UserId=dbo.tblDashboardSetting.UserId
  WHERE  dbo.tblDashboardSetting.UserId='" + userId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadProbationEmployeeData(string parameter)
        {


            string queryStr = @"	SELECT    ISNULL(FORMAT(EGI.ExtProbationPeriodDate,'dd-MMM-yyyy'),FORMAT(ProbationEndDate,'dd-MMM-yyyy'))AS ProbationEndDate, EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, FORMAT(EGI.DateOfJoin,'dd-MMM-yyyy') DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,*
                                    FROM tblEmpGeneralInfo AS EGI    with (nolock) 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblUser AS US ON EGI.EmpInfoId = US.EmpInfoId
									LEFT JOIN dbo.tblDashboardSetting AS DS ON US.UserId = DS.UserId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE IsProbationary=1 and EGI.IsActive='1' " + parameter + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }


        public DataTable LoadProbationEmployeeDataAll(string parameter)
        {


            string queryStr = @"	SELECT    ISNULL(EGI.ExtProbationPeriodDate,ProbationEndDate)AS ProbationEndDate, EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,*
                                    FROM tblEmpGeneralInfo AS EGI    with (nolock) 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblUser AS US ON EGI.EmpInfoId = US.EmpInfoId
									LEFT JOIN dbo.tblDashboardSetting AS DS ON US.UserId = DS.UserId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE IsProbationary=1 and EGI.IsActive='1' " + parameter + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }


        public DataTable LoadContractualEmployeeDataForAll(string parameter)
        {


            string queryStr = @"	SELECT    ISNULL(EGI.ExtContractDate,ContractEndDate)AS ContractEndDate, EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,*
                                    FROM tblEmpGeneralInfo AS EGI    with (nolock) 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
 
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE  EGI.IsProgramContractual=1 and EGI.IsActive='1'  " + parameter + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable LoadRetirementEmployeeDataForAll(string parameter)
        {


            string queryStr = @"	SELECT   ISNULL(EGI.DateOfRetirement,EGI.DateOfRetirement)AS DateOfRetirement, EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,*
                                    FROM tblEmpGeneralInfo AS EGI    with (nolock) 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
 
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE  EGI.IsActive='1'  " + parameter + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadContractualEmployeeData(string parameter)
        {


            string queryStr = @"	select  FORMAT(EGI.ContractEndDate,'dd-MMM-yyyy') AS ContractEndDate,
	 EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, FORMAT(EGI.DateOfJoin,'dd-MMM-yyyy') DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,*
                                    FROM tblEmpGeneralInfo AS EGI    with (nolock) 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblUser AS US ON EGI.EmpInfoId = US.EmpInfoId
									LEFT JOIN dbo.tblDashboardSetting AS DS ON US.UserId = DS.UserId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE  EGI.EmpTypeId=2 and EGI.IsActive='1'    " + parameter + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadRetirementEmployeeData(string parameter)
        {


            string queryStr = @"	SELECT    ISNULL(EGI.DateOfRetirement,EGI.DateOfRetirement)AS DateOfRetirement, EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,*
                                    FROM tblEmpGeneralInfo AS EGI  with (nolock)
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblUser AS US ON EGI.EmpInfoId = US.EmpInfoId
									LEFT JOIN dbo.tblDashboardSetting AS DS ON US.UserId = DS.UserId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE  EGI.EmpTypeId=1 and EGI.IsActive='1'  " + parameter + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
    }
}
