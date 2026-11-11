using System.Data.SqlClient;
using System.Web;
using System.Web.Providers.Entities;
using System.Windows.Forms;
using DAL.DataManager;
using DAL.InternalCls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace DAL.HealthCare_DAL
{
   public class ReimbursmentFormDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();




        public DataTable GET_HoliDay(DateTime FROM, DateTime To)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@fromDate", FROM));
                aList.Add(new SqlParameter("@toDate", To));

                dt = accessManager.GetDataTable("sp_GET_HolidayCount", aList);
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


        public DataTable Get_RemainningBalance_Finance(string EmpId, string CompanyId, string financialId, string BalanceType, string type)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@BalanceType", BalanceType));
                aList.Add(new SqlParameter("@EmpId", EmpId));
                aList.Add(new SqlParameter("@FinancialYearId", financialId));
                aList.Add(new SqlParameter("@CompanyId", CompanyId));
                aList.Add(new SqlParameter("@Type", type));
                dt = accessManager.GetDataTable("sp_GET_RemainingBalance_Finance", aList);
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
        public DataTable GetEmployeeDetails(int id)
        {
            try
            {
                string query = @"SELECT  isnull(EGI.ThreeMonthBinding,'') ThreeMonthBinding, CASE
    WHEN (isnull(EGI.EmpCategoryId,0) IN (1, 2) OR isnull(EGI.EmpTypeId,0) = 2)
         AND DATEDIFF(MONTH, CONVERT(DATE, EGI.DateOfJoin), CONVERT(DATE, GETDATE())) >= 3
        THEN 'Yes'

    WHEN (isnull(EGI.EmpCategoryId,0) NOT IN (1, 2) AND isnull(EGI.EmpTypeId,0) <> 2)
         AND DATEDIFF(MONTH, CONVERT(DATE, EGI.DateOfJoin), CONVERT(DATE, GETDATE())) >= 6
        THEN 'Yes'

    ELSE 'No'
END
      AS IsThreeMonthsOrMore,
CASE
    WHEN (isnull(EGI.EmpCategoryId,0) IN (1, 2) OR isnull(EGI.EmpTypeId,0) = 2)
        THEN 'Service length is less than three months. You are not allowed to submit.'
    ELSE 'Service length is less than six months. You are not allowed to submit.'
END AS ServiceRestrictionMessage, FORMAT(EGI.DateOfConformation, 'dd-MMM-yyyy' ) DateOfConformation,   sal.AccountName , bnk.BankName, sal.BankAccountNo,sal.BranchName, sal.RoutingNo, Sloc.SalaryLocation,Sloc.SalaryLoationId ,EGI.OfficialMobile,R.EmpName AS ReportingToName,EGI.EmpInfoId,EGI.EmpName, EGI.DateOfJoin, CI.CompanyId, CI.CompanyName, DSN.DivisionId, DSN.DivisionName, DSNW.DivisionWId, DSNW.DivisionWingName,DPT.DepartmentId, DPT.DepartmentName,
                                    SEC.SectionId, SEC.SectionName, SSEC.SubSectionId, SSEC.SubSectionName, DSG.DesignationId, DSG.Designation,  ETP.EmpTypeId, ETP.EmpType, Jloc.Location, EGI.EmpMasterCode, * 

                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblJobLocation AS Jloc ON EGI.JobLocationId = Jloc.JobLocationID
									LEFT JOIN dbo.tblSalaryLocation AS Sloc ON EGI.SalaryLoationId = Sloc.SalaryLoationId
									LEFT JOIN dbo.tblEmpGeneralInfo AS R ON EGI.ReportingEmpId = R.EmpInfoId
									LEFT JOIN dbo.tblEmpSalaryInfo AS sal ON sal.EmpInfoId = EGI.EmpInfoId
									LEFT JOIN dbo.tblBankInformation AS bnk ON bnk.BankId = sal.BankNameId
                                    where  EGI.EmpInfoId = " + id + "   ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetOption()
        {
            try
            {
                string query = @"Select ''as Descriptiondate, YesNo as YesNo1,* from tblReimbursmentCheckOption_HealthCare";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetCheckEmpStatus(string ID)
        {
            string queryStr = @"SELECT e.EmpInfoId  WHERE EmpMasterCode IS NOT  NULL AND e.IsActive=1 AND e.EmpTypeId=1 AND e.ConformationStatus=1 AND EmpInfoIn=" + ID;
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }


        public DataTable Get_EmpListById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_MedicalSupportCommitteById", aList);
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
        public DataTable Get_ActionStatusById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ActionStatuseById", aList);
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



        public DataTable Get_ReportingBossCheck(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ReportingBossCheck", aList);
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


        public DataTable Get_IpdOpd(int value, string ComId, string EmpId, string Type)
        {
            try
            {
                //string query = @"Select ''as Dates,''as SINoOfEncloseVoucher,OIPDHeadOfExpenseId,HeadOfExpense,Amount from tbl_OPDIPDHeadOfExpense_HealthCare where IsOPD='" + value + "' and CompanyId= " + ComId; 

              //  string query = @"Select case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + EmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField,  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + EmpId + " and isactive=1) else '' end ChildrenNo,  '' RemaksforHR, 0 Rent, ''as Dates,'1' as NoOfDays,''as SINoOfEncloseVoucher,OIPDHeadOfExpenseId,HeadOfExpense,Amount,0 Amount_new,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare where   IsActive=1 and IsOPD=" + value + " and CompanyId= " + ComId + " " + "union all  select  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + EmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField, case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + EmpId + " and isactive=1) else '' end ChildrenNo,   '' RemaksforHR,  0 Rent, '' as Dates,'' as NoOfDays,'' as SINoOfEncloseVoucher, OIPDHeadOfExpenseId, HeadOfExpense, 0 Amount_new,Amount,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare where IsOPD=0 and CompanyId is null";

                string safeEmpId = string.IsNullOrWhiteSpace(EmpId) ? "0" : EmpId.Trim();
                string safeComId = string.IsNullOrWhiteSpace(ComId) ? "0" : ComId.Trim();

                string query = "";

                if (Type == "IPD")
                {
                    if (safeComId == "1")
                    {
                        query = @"Select case when (HeadOfExpense='Caesarian case' or  HeadOfExpense='Normal') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField,  case when (HeadOfExpense='Caesarian case' or  HeadOfExpense='Normal') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,  '' RemaksforHR, 0 Rent, ''as Dates,'1' as NoOfDays,''as SINoOfEncloseVoucher,OIPDHeadOfExpenseId,HeadOfExpense, Amount,0 Amount_new,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare   LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId  where   IsActive=1 and tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=" + value + " and tbl_HeadOfExpenseMaster_Healthcare.CompanyId= " + safeComId + " " + "union all  select  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField, case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,   '' RemaksforHR,  0 Rent, '' as Dates,'' as NoOfDays,'' as SINoOfEncloseVoucher, OIPDHeadOfExpenseId, HeadOfExpense, Amount  Amount_new,Amount,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId where tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=0 and tbl_HeadOfExpenseMaster_Healthcare.CompanyId is null";
                    }
                    else
                    {
                        query = @"Select case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField,  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,  '' RemaksforHR, 0 Rent, ''as Dates,'1' as NoOfDays,''as SINoOfEncloseVoucher,OIPDHeadOfExpenseId,HeadOfExpense,Amount,0 Amount_new,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare   LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId  where   IsActive=1 and tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=" + value + " and tbl_OPDIPDHeadOfExpense_HealthCare.CompanyId= " + safeComId + " " + "union all  select  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField, case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,   '' RemaksforHR,  0 Rent, '' as Dates,'' as NoOfDays,'' as SINoOfEncloseVoucher, OIPDHeadOfExpenseId, HeadOfExpense,  Amount Amount_new,Amount,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId where tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=0 and tbl_HeadOfExpenseMaster_Healthcare.CompanyId is null";
                    }
                }
                else
                {
                    if (safeComId == "1")
                    {
                        query = @"Select case when (HeadOfExpense='Caesarian case' or  HeadOfExpense='Normal') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField,  case when (HeadOfExpense='Caesarian case' or  HeadOfExpense='Normal') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,  '' RemaksforHR, 0 Rent, ''as Dates,'1' as NoOfDays,''as SINoOfEncloseVoucher,OIPDHeadOfExpenseId,HeadOfExpense,0 Amount,0 Amount_new,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare   LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId  where   IsActive=1 and tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=" + value + " and tbl_HeadOfExpenseMaster_Healthcare.CompanyId= " + safeComId + " " + "union all  select  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField, case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,   '' RemaksforHR,  0 Rent, '' as Dates,'' as NoOfDays,'' as SINoOfEncloseVoucher, OIPDHeadOfExpenseId, HeadOfExpense, 0 Amount_new,Amount,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId where tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=0 and tbl_HeadOfExpenseMaster_Healthcare.CompanyId is null";
                    }
                    else
                    {
                        query = @"Select case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField,  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,  '' RemaksforHR, 0 Rent, ''as Dates,'1' as NoOfDays,''as SINoOfEncloseVoucher,OIPDHeadOfExpenseId,HeadOfExpense,0 Amount,0 Amount_new,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare   LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId  where   IsActive=1 and tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=" + value + " and tbl_OPDIPDHeadOfExpense_HealthCare.CompanyId= " + safeComId + " " + "union all  select  case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') and  (select count(*)  from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1)>=2 then 'Yes' else 'No' end  YourConditionField, case when (HeadOfExpense='Caesarean Delivery' or  HeadOfExpense='Normal Delivery') then  (select count(*) from tblEmpChildren  where EmpInfoId=" + safeEmpId + " and isactive=1) else '' end ChildrenNo,   '' RemaksforHR,  0 Rent, '' as Dates,'' as NoOfDays,'' as SINoOfEncloseVoucher, OIPDHeadOfExpenseId, HeadOfExpense, 0 Amount_new,Amount,IsIncrement from tbl_OPDIPDHeadOfExpense_HealthCare LEFT JOIN tbl_HeadOfExpenseMaster_Healthcare On tbl_HeadOfExpenseMaster_Healthcare.HeadOfExpenseMasterId = tbl_OPDIPDHeadOfExpense_HealthCare.HeadOfExpenseMasterId where tbl_OPDIPDHeadOfExpense_HealthCare.IsOPD=0 and tbl_HeadOfExpenseMaster_Healthcare.CompanyId is null";
                    }
                }

                                 
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Get_FinancialById(string ID)
        {
            string queryStr = @"Select * from tblFinancialYear where CompanyId=" + ID;
           
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }


        public DataTable Get_FinancialByIForSelecttedValue(string ID)
        {
            DateTime today = DateTime.Today;
            DateTime targetDate = new DateTime(2026, 7, 5);

            if (today <= targetDate)
            {
                string extendedQuery = @"SELECT *
FROM tblFinancialYear
WHERE
    CONVERT(date, StartDate) <= CONVERT(date, '2026-06-30')
    AND CONVERT(date, EndDate) >= CONVERT(date, '2026-06-30')
    AND CompanyId = " + ID;

                return aCommonInternalDal.GetDTforDDL(extendedQuery, null, DataBase.HRDB);
            }

            string queryStr = @"SELECT * 
FROM tblFinancialYear 
WHERE 
    CONVERT(date, StartDate) <= CONVERT(date, GETDATE()) 
    AND CONVERT(date, EndDate) >= CONVERT(date, GETDATE()) 
    AND CompanyId ="
            + ID;

            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }


        public DataTable Get_FinancialByIForSelecttedValueEdit(string ID)
        {
            string queryStr = @"SELECT * 
FROM tblFinancialYear 
WHERE  
      CompanyId ="
            + ID;
 
            string main = queryStr ;
            return aCommonInternalDal.GetDTforDDL(main, null, DataBase.HRDB);
        }


       public DataTable Get_ReimbusrmentFormlist()
       {
           DataTable dt = new DataTable();
           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);
                
               List<SqlParameter> aSqlParameters = new List<SqlParameter>();
               aSqlParameters.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
               dt = accessManager.GetDataTable("sp_GET_ReimbursmentFromlist", aSqlParameters);
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


       public bool delssssss(string aMaster)
       {
           int pk = 0;
           bool result = false;

           try
           {
               List<SqlParameter> aSqlParameters = new List<SqlParameter>();
               accessManager.SqlConnectionOpen(DataBase.H);
               aSqlParameters.Add(new SqlParameter("@ReimbursFromMasterId", aMaster));
               aSqlParameters.Add(new SqlParameter("@DelBy",HttpContext.Current.Session["UserId"].ToString()));

               result = accessManager.DeleteData("sp_DEL_SettData", aSqlParameters);
               
           }
           catch (Exception e)
           {
               pk = 0;
               accessManager.SqlConnectionClose(true);
               throw e;
           }
           finally
           {
               accessManager.SqlConnectionClose();
           }
           return result;
       }

       public int data_save(ReimbursmentMaster aMaster, List<ReimbursmentberifDiscriptionDao> reimbursmentberifDiscription, List<ReimbursmentEnclosuretickMark> marklist, List<ClaimDetailsDao> claimDetailslList, List<ReimbursmentDocument> reimbursmentlList, List<ReimbursmentFromEmpListDao> empList, string EntryBy, string Mode)
       {
           int pk = 0;
           bool result = false;

           try
           {
               accessManager.SqlConnectionOpen(DataBase.H);

               List<SqlParameter> aSqlParameters = new List<SqlParameter>();
               aSqlParameters.Add(new SqlParameter("@ReimbursFromMasterId", aMaster.ReimbursFromMasterId));
               aSqlParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
               aSqlParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
               aSqlParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
               aSqlParameters.Add(new SqlParameter("@Type", (object)aMaster.Type ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@SelfDate", (object)aMaster.SelfDate ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@SubmitDate", (object)aMaster.SubmitDate ?? DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@PatientName", aMaster.PatientName));
               aSqlParameters.Add(new SqlParameter("@PatientAge", aMaster.PatientAge ?? (object) DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@Relationship", aMaster.Relationship));
               aSqlParameters.Add(new SqlParameter("@EntryBy", aMaster.EntryBy));
               aSqlParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
               aSqlParameters.Add(new SqlParameter("@OfficialMobile", aMaster.OfficialMobile));


               aSqlParameters.Add(new SqlParameter("@BankName", aMaster.BankName ?? (object)DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@BankAccountNo", aMaster.BankAccountNo ?? (object)DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@BranchName", aMaster.BranchName ?? (object)DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@RoutingNo", aMaster.RoutingNo ?? (object)DBNull.Value));


                aSqlParameters.Add(new SqlParameter("@HospitalNameId", aMaster.HospitalNameId ?? (object)DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@HospitalAdmissionDate", aMaster.HospitalAdmissionDate ?? (object)DBNull.Value));
               aSqlParameters.Add(new SqlParameter("@HospitalDischargeDate", aMaster.HospitalDischargeDate ?? (object)DBNull.Value));
                aSqlParameters.Add(new SqlParameter("@HospitalNotificationEmailDoc", aMaster.HospitalNotificationEmailDoc ?? (object)DBNull.Value));
                aSqlParameters.Add(new SqlParameter("@HospitalNotificationEmailFileName", aMaster.HospitalNotificationEmailFileName ?? (object)DBNull.Value));


                if (aMaster.ReimbursFromMasterId > 0)
               {
                   result = accessManager.UpdateData("sp_Update_ReimbursmentMaster", aSqlParameters);

                   if (result)
                   {

                       if ( int.Parse(EntryBy) == aMaster.EntryBy)
                       {
                           List<SqlParameter> gSqlParameters = new List<SqlParameter>();
                           gSqlParameters.Add(new SqlParameter("ReimbursFromMasterId", aMaster.ReimbursFromMasterId));
                           result = accessManager.DeleteData("sp_DEL_reimbursmentfromAllrelationData", gSqlParameters);
                           if (result)
                           {
                               pk = aMaster.ReimbursFromMasterId;

                              
                           }
                       }
                       else
                       {
                           List<SqlParameter> gSqlParameters = new List<SqlParameter>();
                           gSqlParameters.Add(new SqlParameter("@ReimbursFromMasterId", aMaster.ReimbursFromMasterId));
                           gSqlParameters.Add(new SqlParameter("@EntryBy", aMaster.EntryBy));
                           result = accessManager.DeleteData("sp_DEL_Save_reimbursmentfromAllrelationData", gSqlParameters);
                           if (result)
                           {
                               pk = aMaster.ReimbursFromMasterId;
                           }
                       }

                   }
               }
               else
               {

            

                  
                   pk = accessManager.SaveDataReturnPrimaryKey("sp_Save_ReimbursmentMaster", aSqlParameters);
                 
        
               }

               if (pk > 0)
               {
                   if (Mode == "HR")
                   {
                       decimal tk = 0;
                       foreach (var claimList in claimDetailslList)
                       {
                           try
                           {


                               tk = tk + (decimal)claimList.Amount;

                           }
                           catch (Exception exception)
                           {

                           }

                       }

                       List<SqlParameter> sqlTK = new List<SqlParameter>();
                       sqlTK.Add(new SqlParameter("@ReimbursFromMasterId", pk));
                       sqlTK.Add(new SqlParameter("@Mode", "HR"));
                       sqlTK.Add(new SqlParameter("@ClaimTKByUser", tk));
                       result = accessManager.UpdateData("sp_Save_ClaimTK", sqlTK);
                   }

                   else
                   {
                       decimal tk = 0;
                       foreach (var claimList in claimDetailslList)
                       {
                           try
                           {


                               tk = tk + (decimal)claimList.Amount;

                           }
                           catch (Exception exception)
                           {

                           }

                       }

                       List<SqlParameter> sqlTK = new List<SqlParameter>();
                       sqlTK.Add(new SqlParameter("@ReimbursFromMasterId", pk));
                       sqlTK.Add(new SqlParameter("@Mode", "User"));
                       sqlTK.Add(new SqlParameter("@ClaimTKByUser", tk));
                       result = accessManager.UpdateData("sp_Save_ClaimTK", sqlTK);
                   }
                  

                   foreach (var reimbursmentberifDiscriptionDao in reimbursmentberifDiscription)
                   {
                       try
                       {
                           List<SqlParameter> BreifSqlParameters = new List<SqlParameter>();
                           BreifSqlParameters.Add(new SqlParameter("ReimbursFromBriefDescriptionId", reimbursmentberifDiscriptionDao.ReimbursFromBriefDescriptionId));
                           BreifSqlParameters.Add(new SqlParameter("ReimbursFromMasterId", pk));
                           BreifSqlParameters.Add(new SqlParameter("ReibCheckOppId", reimbursmentberifDiscriptionDao.ReibCheckOppId));
                           BreifSqlParameters.Add(new SqlParameter("YesNo", reimbursmentberifDiscriptionDao.YesNo));
                           BreifSqlParameters.Add(new SqlParameter("Date", reimbursmentberifDiscriptionDao.Date));
                           BreifSqlParameters.Add(new SqlParameter("Descriptiondate", (object)reimbursmentberifDiscriptionDao.Descriptiondate ?? DBNull.Value));
                           result = accessManager.SaveData("sp_Save_ReimbursmentBreifDescription", BreifSqlParameters);
                       }
                       catch (Exception exception)
                       {
                           result = false;
                           throw exception;
                       }
                   }



                   foreach (var marklistd in marklist)
                   {
                       try
                       {
                           List<SqlParameter> MarkSqlParameters = new List<SqlParameter>();
                           MarkSqlParameters.Add(new SqlParameter("ReimbursmentEnclosuresTickmarkId", marklistd.ReimbursmentEnclosuresTickmarkId));
                           MarkSqlParameters.Add(new SqlParameter("ReimbursFromMasterId", pk));
                           MarkSqlParameters.Add(new SqlParameter("EnclosuresTickMark", marklistd.EnclosuresTickMark));

                           result = accessManager.SaveData("sp_Save_ReimbursmentEnclosuresTickMark", MarkSqlParameters);
                       }
                       catch (Exception exception)
                       {
                           result = false;
                           throw exception;
                       }
                   }



                   foreach (var claimList in claimDetailslList)
                   {
                       try
                       {
                           List<SqlParameter> ClaimsSqlParameters = new List<SqlParameter>();
                           ClaimsSqlParameters.Add(new SqlParameter("ClaimDetailsId", claimList.ClaimDetailsId));
                           ClaimsSqlParameters.Add(new SqlParameter("ReimbursFromMasterId", pk));
                           ClaimsSqlParameters.Add(new SqlParameter("OIPDHeadOfExpenseId", claimList.OIPDHeadOfExpenseId));
                           ClaimsSqlParameters.Add(new SqlParameter("SINoOfEncloseVoucher", (object)claimList.SINoOfEncloseVoucher ?? DBNull.Value));
                           ClaimsSqlParameters.Add(new SqlParameter("Dates", (object)claimList.Dates ?? DBNull.Value));
                           ClaimsSqlParameters.Add(new SqlParameter("Amount", (object)claimList.Amount ?? DBNull.Value));
                           ClaimsSqlParameters.Add(new SqlParameter("Rent", (object)claimList.Rent ?? DBNull.Value));
                           ClaimsSqlParameters.Add(new SqlParameter("NoOfDays", (object)claimList.Numberofdays ?? DBNull.Value));
                           ClaimsSqlParameters.Add(new SqlParameter("ChildrenNo", (object)claimList.ChildrenNo ?? DBNull.Value));
                           ClaimsSqlParameters.Add(new SqlParameter("RemaksforHR", (object)claimList.RemaksforHR ?? DBNull.Value));
                           result = accessManager.SaveData("sp_Save_ReimbursmentformClaimDetails", ClaimsSqlParameters);
                       }
                       catch (Exception exception)
                       {
                           result = false;
                           throw exception;
                       }

                   }


                   foreach (var reimbursmentDocument in reimbursmentlList)
                   {

                       try
                       {
                           List<SqlParameter> gSqlParameters = new List<SqlParameter>();

                           gSqlParameters.Add(new SqlParameter("ReimbursmentDocumentId", reimbursmentDocument.ReimbursmentDocumentId));
                           gSqlParameters.Add(new SqlParameter("ReimbursFromMasterId", pk));
                           gSqlParameters.Add(new SqlParameter("DocumentLink", (object)reimbursmentDocument.DocumentLink ?? DBNull.Value));
                           gSqlParameters.Add(new SqlParameter("DocumentNote", (object)reimbursmentDocument.DocumentNote ?? DBNull.Value));
                           gSqlParameters.Add(new SqlParameter("FileName", (object)reimbursmentDocument.FileName ?? DBNull.Value));
                           result = accessManager.SaveData("sp_Save_ReimbursmentFormDocment", gSqlParameters);
                       }
                       catch (Exception exception)
                       {
                           result = false;
                           throw exception;
                       }

                   }


                   foreach (var employeelist in empList)
                   {

                       try
                       {
                           List<SqlParameter> gSqlParameters = new List<SqlParameter>();

                           gSqlParameters.Add(new SqlParameter("ReimbursmentEmpListId", employeelist.ReimbursmentEmpListId));
                           gSqlParameters.Add(new SqlParameter("ReimbursFromMasterId", pk));
                           gSqlParameters.Add(new SqlParameter("EmpInfoId", (object)employeelist.EmpInfoId ?? DBNull.Value));

                           result = accessManager.SaveData("sp_Save_ReimbursmentFormEmpList", gSqlParameters);
                       }
                       catch (Exception exception)
                       {
                           result = false;
                           throw exception;
                       }

                   }

               }

           }
           catch (Exception e)
           {
               pk = 0;
               accessManager.SqlConnectionClose(true);
               throw e;
           }
           finally
           {
               accessManager.SqlConnectionClose();
           }

           return pk;

       }


        public DataTable Get_ReimbusrmentFormById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                   aList.Add(new SqlParameter("@Id", ID));
                   dt = accessManager.GetDataTable("sp_GET_ReimbursmentFromlistById",aList);
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

        public int GetOrCreateHospitalNameId(string hospitalName)
        {
            string normalizedHospitalName = (hospitalName ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(normalizedHospitalName))
            {
                return 0;
            }

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>
            {
                new SqlParameter("@HospitalName", normalizedHospitalName)
            };

            const string queryStr = @"
DECLARE @NormalizedHospitalName NVARCHAR(250) = LTRIM(RTRIM(@HospitalName));
DECLARE @HospitalNameId INT;

SELECT TOP 1 @HospitalNameId = HospitalNameId
FROM dbo.tblHospitalName WITH (UPDLOCK, HOLDLOCK)
WHERE UPPER(LTRIM(RTRIM(HospitalName))) = UPPER(@NormalizedHospitalName);

IF (@HospitalNameId IS NULL AND @NormalizedHospitalName <> '')
BEGIN
    INSERT INTO dbo.tblHospitalName (HospitalName)
    VALUES (@NormalizedHospitalName);

    SET @HospitalNameId = CAST(SCOPE_IDENTITY() AS INT);
END

SELECT ISNULL(@HospitalNameId, 0) AS HospitalNameId;";

            using (DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["HospitalNameId"]);
                }
            }

            return 0;
        }



        public DataTable Get_DescriptionById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ReimbursMentFormBerifDescriptionById", aList);
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


        public DataTable Get_TickMarkById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ReimbursMentFromTickMarkById", aList);
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


        public DataTable Get_ClaimDetailsById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ReimbursMentClaimDetailsById", aList);
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

        public DataTable Get_FormDocumentById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ReimbursMentFormDocumentById", aList);
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

        public DataTable Get_ReimbusrmentMasterForRPT(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ReimbursMentFormZForPRTById", aList);
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

        public DataTable Get_briefDescriptionForRPT(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_BriefDescriptionOfIllnessPRTBymasterId", aList);
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

        public DataTable Get_EnClosuresTickMarkForRPT(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_EnclosuresTickMarkPRTBymasterId", aList);
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

        public DataTable Get_EmpListForRPT(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_EmpListPRTBymasterId", aList);
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

        public DataTable Get_ReportForReimbursmentBillInMainReport(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ReimbursMentBillForMainPRTById", aList);
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

        public DataTable Get_ClaimDetailsForRPT(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_ClaimDetailsPRTBymasterId", aList);
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

        public DataTable Get_RemainningBalance(string EmpId, string CompanyId, string financialId, string BalanceType,string type)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@BalanceType", BalanceType));
                aList.Add(new SqlParameter("@EmpId", EmpId));
                aList.Add(new SqlParameter("@FinancialYearId", financialId));
                aList.Add(new SqlParameter("@CompanyId", CompanyId));
                aList.Add(new SqlParameter("@Type", type));
                dt = accessManager.GetDataTable("sp_GET_RemainingBalance", aList);
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

        public DataTable Get_RemainningBillSatelment(string EmpId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

              
                aList.Add(new SqlParameter("@EmpId", EmpId));
                
                dt = accessManager.GetDataTable("sp_GET_RemainingBalance", aList);
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

        public DataTable Get_FamilyMemberById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_FamilyMemberByEmp", aList);
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



        public DataTable Get_FamilyMemberById_Depart(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_FamilyMemberByEmp_dEATH", aList);
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

        public DataTable Get_DataListForFormApproval()
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_ExpensereimbursFormForapproval", aList);
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

        public DataTable Get_DataListForFormApproval_Special()
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CompanyId", HttpContext.Current.Session["CompanyId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_ExpensereimbursFormForapproval_Special", aList);
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

        public bool ExpenseReimbursementFormAppoval(string FromMasterId, string status)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status)); 
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }


        public bool ExpenseReimbursementForm_Retun(string FromMasterId, string status)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status));
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom_Return", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }

        public bool ExpenseReimbursementForwardtoDoctor(string FromMasterId, string comment = "")
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@Comment", string.IsNullOrWhiteSpace(comment) ? (object)DBNull.Value : comment));
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom_ForwardtoDoctor", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }
        public bool ExpenseReimbursementFormAppoval_DoctorStatus(string FromMasterId, string status, bool DoctorStatus)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status));
                aParameters.Add(new SqlParameter("@DoctorStatus", DoctorStatus));
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom_ForDoctor", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }


        public bool ExpenseReimbursementFormAppoval_HeadofDpt(string FromMasterId, string status)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status)); 
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom_HeadofDpt", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }


        public bool ExpenseReimbursementFormAppoval_FApprove(string FromMasterId, string status)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status));
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom_FA", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }

      

        //IF Nominated not Found
        public bool _ExpenseReimbursementFormAppoval(string FromMasterId, string status)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }

        public DataTable Get_PreviousSave(int Id)
        {
            string query = @"Select CONVERT(varchar,DeathofDate,106) DeathofDate,* from tblDepartedInfo Where EmpInfoId=" + Id;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEmpInfo(string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  " + param + "";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CheckFinalApprovalCondition(string ComId, string EmpID)
        {
            try
            {
                string query = @" SELECT * FROM dbo.tblSupevisorMenuApproval  WITH (Nolock)  WHERE  CompanyId='" + ComId + "' AND FromEmpInfoId ='" + EmpID + "' AND EmpInfoId IS NOT NULL ";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable CheckFinalApproval (string EmpID)
        {
            try
            {
                string query = @" SELECT sapp.EmpInfoId, e.Empmastercode+' : '+e.EmpName EmpName FROM dbo.tblSupevisorMenuApproval sapp  WITH (Nolock)
inner join tblEmpGeneralInfo e on e.EmpInfoId=sapp.EmpInfoId
  WHERE    sapp.FromEmpInfoId ='" + EmpID + @"' AND sapp.EmpInfoId IS NOT NULL  

  ";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CheckFinalApprovalConditionwithJoin( string EmpID)
        {
            try
            {
                string query = @" SELECT sapp.EmpInfoId, e.Empmastercode+' : '+e.EmpName EmpName FROM dbo.tblSupevisorMenuApproval sapp  WITH (Nolock)
inner join tblEmpGeneralInfo e on e.EmpInfoId=sapp.EmpInfoId
  WHERE    sapp.FromEmpInfoId ='" + EmpID + @"' AND sapp.EmpInfoId IS NOT NULL and IsAllEmployee=1

  ";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SaveEmpAppLog(ReimbursementSelfAppLogDAO appLogDao)
        {
            try
            {
                int pk = 0;
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ReimbursFromMasterId", appLogDao.ReimbursFromMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", (object)appLogDao.Comments ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@CommentsEMPID", (object)appLogDao.CommentsEMP ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@HRPanel", (object)appLogDao.HRPanel ?? DBNull.Value));

                    string query = @"INSERT INTO dbo.tblReimbursementSelfAppLog
                                    (
                                    ReimbursFromMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentsEMPID,HRPanel
                                    )
                                    VALUES(
                                    @ReimbursFromMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblReimbursementSelfAppLog WHERE ReimbursFromMasterId=@ReimbursFromMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsEMPID,@HRPanel
                                    )";

                    pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }

                return pk;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }



        public int SaveEmpAppLogForHR(ReimbursementSelfAppLogDAO appLogDao)
        {
            try
            {
                int pk = 0;
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ReimbursFromMasterId", appLogDao.ReimbursFromMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", (object)appLogDao.Comments ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@CommentsEMPID", (object)appLogDao.CommentsEMP ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@HRPanel", (object)appLogDao.HRPanel ?? DBNull.Value));

                    string query = @"INSERT INTO dbo.tblReimbursementSelfAppLog
                                    (
                                    ReimbursFromMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentsEMPID,HRPanel, HRPanelShowNotify
                                    )
                                    VALUES(
                                    @ReimbursFromMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblReimbursementSelfAppLog WHERE ReimbursFromMasterId=@ReimbursFromMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsEMPID,@HRPanel, 0
                                    )";

                    pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }

                return pk;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public bool UpdateContractural(ReimbursementSelfAppLogDAO aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.ReimbursFromMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ReimbursFromMasterId", aMaster.ReimbursFromMasterId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"Update tbl_ReimbursmentFormMaster_HealthCare set ActionStatus=@ActionStatus  where ReimbursFromMasterId = @ReimbursFromMasterId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }

        public bool RejectedUpdateContractural(ReimbursementSelfAppLogDAO aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.ReimbursFromMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ReimbursFromMasterId", aMaster.ReimbursFromMasterId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"Update tbl_ReimbursmentFormMaster_HealthCare set ActionStatus=@ActionStatus, ShowStatus =@ActionStatus where ReimbursFromMasterId = @ReimbursFromMasterId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }

        public DataTable GetSupervisorEmployeeAppId(string empinfoId, string fromempInfoId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval WHERE EmpInfoId='" + empinfoId + "' AND FromEmpInfoId='" + fromempInfoId + "'";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateAppLog(string status, string id)
        {

            try
            {
                int pk = 0;

                //if (id.JdMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalSelfAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));

                    string query =
                        @"update tblReimbursementSelfAppLog set ActionStatus=@ActionStatus  where ReimbursementSelfAppLogId = @AppraisalSelfAppLogId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblReimbursementSelfAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND ReimbursFromMasterId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')   order by ReimbursementSelfAppLogId desc";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Get_Nominated_HR(string ApplicationType, string SalaryLocation, string CompanyId)
        {
            try
            {
                string query = @"SELECT  D.EmpInfoId from tblCommiteeSetupMaster M
LEFT JOIN tblCommiteeSetupDetails D ON D.ComSetupMasId = M.ComSetupMasId
WHERE M.ComSetupMasId IS NOT NULL AND IsForward=1 AND M.SalaryLoationId= "+SalaryLocation+" AND M.CompanyId= "+CompanyId+" AND M.ApplicationType='"+ApplicationType+"' ";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Get_Employee_ChildrenCount(string EmpInfoId, string FinancialYearId)
        {
            try
            {
                string query = @"Select count(EmpInfoId) AS TotalCount from tbl_ReimbursmentFormMaster_HealthCare Where EmpInfoId IS NOT NULL AND Relationship='Children' AND ActionStatus='Approved' AND EmpInfoId =" + EmpInfoId + " And FinancialYearId=" + FinancialYearId + " ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable Get_ApplicationComments(string ReimbursmasterId)
        {
            try
            {
                string query = @"SELECT EMP.EmpName, Comments FROM tbl_ReimbursmentFormMaster_HealthCare H
	            INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= H.ReimbursFromMasterId
	            INNER JOIN dbo.tblReimbursementSelfAppLog ON tblReimbursementSelfAppLog.ReimbursFromMasterId = H.ReimbursFromMasterId      
	            LEFT  JOIN tblEmpGeneralInfo EMP ON tblReimbursementSelfAppLog.CommentsEMPID =  EMP.EmpInfoId                 
	            where H.ReimbursFromMasterId IS Not NULL  AND   Version=CELog.MaxVer  AND tblReimbursementSelfAppLog.Comments IS NOT NULL AND  H.ReimbursFromMasterId=" + ReimbursmasterId;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       

    }
}
