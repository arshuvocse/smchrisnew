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
using Org.BouncyCastle.Ocsp;

namespace DAL.HealthCare_DAL
{
    public class HealthCareListDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

        public DataTable Get_All_Employee_List(string param)
        {
            try
            {
                //                string query = @"SELECT M.ReimbursFromMasterId, M.CompanyId, M.EmpInfoId,M.EmpMasterCode, Emp.EmpName, DEG.Designation, Divi.DivisionName, Dept.DepartmentName, M.ActionStatus, M.PaymentStatus FROM tbl_ReimbursmentFormMaster_HealthCare M
                //LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
                //LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
                //LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
                //LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
                //LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
                //WHERE M.ReimbursFromMasterId IS NOT NULL  "+param+" ";


                //                string query = @"SELECT M.ReimbursFromMasterId, M.CompanyId, M.EmpInfoId,M.EmpMasterCode, Emp.EmpName,
                //DEG.Designation, Divi.DivisionName, Dept.DepartmentName, M.ActionStatus, M.PaymentStatus, EMP.SalaryLoationId, M.Type FROM tbl_ReimbursmentFormMaster_HealthCare M
                //LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
                //LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
                //LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
                //LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
                //LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
                //LEFT JOIN tblReimbursementSelfAppLog lg ON M.ReimbursFromMasterId = lg.ReimbursFromMasterId
                //LEFT JOIN TopSheetGenerateDetails_H TSGD ON M.ReimbursFromMasterId = TSGD.ReimbursFromMasterId
                //WHERE M.ReimbursFromMasterId IS NOT NULL AND  M.ReimbursFromMasterId NOT IN (Select ReimbursFromMasterId from TopSheetGenerateDetails_H) AND lg.ActionStatus ='Approved' AND ForEmpInfoId=" + HttpContext.Current.Session["EmpInfoId"].ToString() + " ";
                string query = "";



//                if ( HttpContext.Current.Session["EmpInfoId"].ToString() == "4468")
//                {
//                    query =
                        
////                        = @"select Distinct * from ( SELECT  SEC.SectionName SectionName,  case when  m.ReturnFromCommiteMeeting=1 then 'Yes' else 'No' end  ReturnFromCommiteMeeting, M.Relationship, M.PatientName, FORMAT(M.SubmitDate,'dd-MMM-yyyy') SubmitDate, tblamt.Amount, M.ReimbursFromMasterId, M.CompanyId, M.EmpInfoId,M.EmpMasterCode, Emp.EmpName,
////DEG.Designation, Divi.DivisionName, Dept.DepartmentName, M.ActionStatus, M.PaymentStatus, EMP.SalaryLoationId, M.Type FROM tbl_ReimbursmentFormMaster_HealthCare M
////LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
////LEFT JOIN tblSection SEC ON EMP.SectionId = SEC.SectionId 
////LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
////LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
////LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
////LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
////LEFT JOIN tblReimbursementSelfAppLog lg ON M.ReimbursFromMasterId = lg.ReimbursFromMasterId
////LEFT JOIN TopSheetGenerateDetails_H TSGD ON M.ReimbursFromMasterId = TSGD.ReimbursFromMasterId
////left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=M.ReimbursFromMasterId 
////INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= M.ReimbursFromMasterId
////WHERE M.ReimbursFromMasterId IS NOT NULL AND  Version=CELog.MaxVer AND  M.ReimbursFromMasterId NOT IN (Select ReimbursFromMasterId from TopSheetGenerateDetails_H) AND lg.ActionStatus ='Verified'   and M.ReimbursFromMasterId  in ( SELECT ReimbursFromMasterId FROM tblCommitteeFeedback_HC)  AND ForEmpInfoId=" + HttpContext.Current.Session["EmpInfoId"].ToString() + @" union all

//@"SELECT  SEC.SectionName SectionName,  case when  m.ReturnFromCommiteMeeting=1 then 'Yes' else 'No' end  ReturnFromCommiteMeeting, M.Relationship, M.PatientName, FORMAT(M.SubmitDate,'dd-MMM-yyyy') SubmitDate, tblamt.Amount, M.ReimbursFromMasterId, M.CompanyId, M.EmpInfoId,M.EmpMasterCode, Emp.EmpName,
//DEG.Designation, Divi.DivisionName, Dept.DepartmentName, M.ActionStatus, M.PaymentStatus, EMP.SalaryLoationId, M.Type FROM tbl_ReimbursmentFormMaster_HealthCare M
//LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
//LEFT JOIN tblSection SEC ON EMP.SectionId = SEC.SectionId 
//LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
//LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
//LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
//LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
//LEFT JOIN tblReimbursementSelfAppLog lg ON M.ReimbursFromMasterId = lg.ReimbursFromMasterId
//LEFT JOIN TopSheetGenerateDetails_H TSGD ON M.ReimbursFromMasterId = TSGD.ReimbursFromMasterId
//left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=M.ReimbursFromMasterId 
//INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= M.ReimbursFromMasterId
//WHERE M.ReimbursFromMasterId IS NOT NULL AND  Version=CELog.MaxVer AND  M.ReimbursFromMasterId NOT IN (Select ReimbursFromMasterId from TopSheetGenerateDetails_H) AND lg.ActionStatus ='Verified'   and M.ReimbursFromMasterId  in ( SELECT ReimbursFromMasterId FROM tblCommitteeFeedback_HC)   and EMP.HealthcareCompanyId=1 ";
//                }
//                else if (HttpContext.Current.Session["EmpInfoId"].ToString() == "3322")
//                {
//                    query = @"SELECT  SEC.SectionName SectionName,  case when  m.ReturnFromCommiteMeeting=1 then 'Yes' else 'No' end  ReturnFromCommiteMeeting, M.Relationship, M.PatientName, FORMAT(M.SubmitDate,'dd-MMM-yyyy') SubmitDate, tblamt.Amount, M.ReimbursFromMasterId, M.CompanyId, M.EmpInfoId,M.EmpMasterCode, Emp.EmpName,
//DEG.Designation, Divi.DivisionName, Dept.DepartmentName, M.ActionStatus, M.PaymentStatus, EMP.SalaryLoationId, M.Type FROM tbl_ReimbursmentFormMaster_HealthCare M
//LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
//LEFT JOIN tblSection SEC ON EMP.SectionId = SEC.SectionId 
//LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
//LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
//LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
//LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
//LEFT JOIN tblReimbursementSelfAppLog lg ON M.ReimbursFromMasterId = lg.ReimbursFromMasterId
//LEFT JOIN TopSheetGenerateDetails_H TSGD ON M.ReimbursFromMasterId = TSGD.ReimbursFromMasterId
//left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=M.ReimbursFromMasterId 
//INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= M.ReimbursFromMasterId
//WHERE M.ReimbursFromMasterId IS NOT NULL AND  Version=CELog.MaxVer AND  M.ReimbursFromMasterId NOT IN (Select ReimbursFromMasterId from TopSheetGenerateDetails_H) AND lg.ActionStatus ='Verified'   and M.ReimbursFromMasterId  in ( SELECT ReimbursFromMasterId FROM tblCommitteeFeedback_HC)  AND  M.CompanyId=1 ";
//                }
                
//                else
                {
                    query = @"SELECT  SEC.SectionName SectionName,  case when  m.ReturnFromCommiteMeeting=1 then 'Yes' else 'No' end  ReturnFromCommiteMeeting, M.Relationship, M.PatientName, FORMAT(M.SubmitDate,'dd-MMM-yyyy') SubmitDate, tblamt.Amount, M.ReimbursFromMasterId, M.CompanyId, M.EmpInfoId,M.EmpMasterCode, Emp.EmpName,
DEG.Designation, Divi.DivisionName, Dept.DepartmentName, M.ActionStatus, M.PaymentStatus, EMP.SalaryLoationId, M.Type FROM tbl_ReimbursmentFormMaster_HealthCare M
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
LEFT JOIN tblSection SEC ON EMP.SectionId = SEC.SectionId 
LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
LEFT JOIN tblReimbursementSelfAppLog lg ON M.ReimbursFromMasterId = lg.ReimbursFromMasterId
LEFT JOIN TopSheetGenerateDetails_H TSGD ON M.ReimbursFromMasterId = TSGD.ReimbursFromMasterId
left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=M.ReimbursFromMasterId 
INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= M.ReimbursFromMasterId
WHERE M.ReimbursFromMasterId IS NOT NULL AND  Version=CELog.MaxVer AND  M.ReimbursFromMasterId NOT IN (Select ReimbursFromMasterId from TopSheetGenerateDetails_H) AND lg.ActionStatus ='Verified'   and M.ReimbursFromMasterId  in ( SELECT ReimbursFromMasterId FROM tblCommitteeFeedback_HC)   " + param + " ";
                }


          

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable Get_All_Employee_List_CommittePanel(string param)
        {
            try
            {
                string query = @"SELECT  case when  m.ReturnFromCommiteMeeting=1 then 'Yes' else 'No' end  ReturnFromCommiteMeeting, M.Relationship, M.PatientName, FORMAT(M.SubmitDate,'dd-MMM-yyyy') SubmitDate, tblamt.Amount, M.ReimbursFromMasterId, M.CompanyId, M.EmpInfoId,M.EmpMasterCode, Emp.EmpName,
DEG.Designation, Divi.DivisionName, Dept.DepartmentName, M.ActionStatus, M.PaymentStatus, EMP.SalaryLoationId, M.Type FROM tbl_ReimbursmentFormMaster_HealthCare M
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
LEFT JOIN tblSalaryInformation sal ON Emp.SalaryLoationId = sal.SalaryInfoId
LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
LEFT JOIN tblReimbursementSelfAppLog lg ON M.ReimbursFromMasterId = lg.ReimbursFromMasterId
LEFT JOIN TopSheetGenerateDetails_H TSGD ON M.ReimbursFromMasterId = TSGD.ReimbursFromMasterId
left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=M.ReimbursFromMasterId 
INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= M.ReimbursFromMasterId
WHERE M.ReimbursFromMasterId IS NOT NULL AND  Version=CELog.MaxVer AND  M.ReimbursFromMasterId NOT IN (Select ReimbursFromMasterId from TopSheetGenerateDetails_H) AND lg.ActionStatus ='Verified'   and M.ReimbursFromMasterId  in ( SELECT ReimbursFromMasterId FROM tblCommitteeFeedback_HC)  AND ForEmpInfoId=" + HttpContext.Current.Session["EmpInfoId"].ToString() + " "+param+" ";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable Get_All_Employee_List_CommitteFeadBackPanel(string param, string EMpId, string Status)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@param", param));
                aList.Add(new SqlParameter("@EMpId", EMpId));
                aList.Add(new SqlParameter("@Status", Status));
                dt = accessManager.GetDataTable("sp_GET_Application_For_CommitteeFeakbackView_New", aList);
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

        public DataTable Get_Complete_Employee_List_SAP_Intregration(string param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@param", param));
                dt = accessManager.GetDataTable("sp_GET_Application_For_SAP_Intregration", aList);
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

        public bool UPDATE_Employee_Application_SAP_Intregration(string param)
        {
            bool status = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@ReimbursFromMasterId", param));
                status=  accessManager.UpdateData("sp_Update_Application_FOR_SAP_Intregration", aList);
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

            return status;
        }
        
        public bool UPDATE_Apprisal_SAP_Intregration(string param)
        {
            bool status = false;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@AppraisalMasterId", param));
                status=  accessManager.UpdateData("sp_Update_Appraisal_FOR_SAP_Intregration", aList);
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

            return status;
        }


        public DataTable Get_Meeting_ListByTopsheetGeneMasId(string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_Get_Meeting_ListByTopsheetGeneMasId", aList);
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

      

        public void GetDDLCompany(DropDownList ddl)
        {
            string queryStr = @"Select CompanyId , ShortName from tblCompanyInfo";

            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);
        }
    }
}
