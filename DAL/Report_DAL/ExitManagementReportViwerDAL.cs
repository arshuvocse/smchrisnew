using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
   public class ExitManagementReportViwerDAL
    {

        private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetJdByMaster(int id)
        {
            try
            {
                string query = @"SELECT com.ShortName, Emp.EmpMasterCode, Emp.EmpName, emp.DateOfBirth, Dpt.DepartmentName, Div.DivisionName, Desg.Designation FROM tblExitInterviewFormMaster mas
LEFT JOIN dbo.tblEmpGeneralInfo Emp ON mas.EmployeeId=Emp.EmpInfoId
LEFT JOIN dbo.tblCompanyInfo com ON Emp.CompanyId=com.CompanyId
LEFT JOIN dbo.tblDepartment Dpt ON mas.DepartmentId=Dpt.DepartmentId
LEFT JOIN dbo.tblDivision Div ON mas.DivisionId=Div.DivisionId
LEFT JOIN dbo.tblDesignation Desg ON mas.DesignationId=Desg.DesignationId WHERE mas.EmployeeId=" + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetExitServeyDetail(int id)
        {
            try
            {
                string query = @"SELECT QS.ServayQuestion,STD.SarveyRatingValue,CASE
                                WHEN STD.SarveyRatingValue=1  THEN 'Strongly Agree' 
                                WHEN STD.SarveyRatingValue=2  THEN 'Agree' 
                                WHEN STD.SarveyRatingValue=3  THEN 'Neutral' 
                                WHEN STD.SarveyRatingValue=4  THEN 'Disagree' 
                                WHEN STD.SarveyRatingValue=5  THEN 'Strongly Disagree'       
                                END  AS AnserScript FROM dbo.tblExitInterviewFormMaster AS EXTM
                                INNER JOIN tblExitServeyDetail STD ON EXTM.ExitMasterId = STD.MasterId
                                INNER JOIN tblExitServyQs AS QS ON STD.ServyItemId = QS.ExitServyId
                                WHERE EXTM.EmployeeId = " + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public DataTable GetExitReasonDetail(int id)
        {
            try
            {
                string query = @"SELECT (RSN.Question + ', ') FROM dbo.tblExitInterviewFormMaster AS EXTM
 INNER JOIN tblExitReasonDetail AS	RSD ON EXTM.ExitMasterId = RSD.MasterId
 LEFT JOIN tblExitQuestions AS RSN ON RSD.ReasonId = RSN.ExitQuestionId
 WHERE EXTM.EmployeeId = " + id + " FOR XML PATH('')  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
