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
using Library.DAO.HRM_Entities;

namespace DAL.Increment_DAL
{
    public class IncrementApprovalDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadCompany(DropDownList ddl)
        {
            
                string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
                aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);
          
        }

        public DataTable GetDataReviewEntryBy(string id, string entryby, string actionstatu)
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


            string queryStr = @"SELECT * FROM dbo.tblIncrement WHERE ActionStatus='" + actionstatu + "' AND EntryBy='" + entryby + "' AND IncrementId='" + id + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public bool UpdateContactAppLog(string status, string id)
        {

            try
            {
                int pk = 0;

                //if (id.JdMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IncrementAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblIncrementAppLog set ActionStatus=@ActionStatus  where IncrementAppLogId = @IncrementAppLogId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public void LoadIncrementType(DropDownList ddl)
        {

            string queryStr = " SELECT * FROM tblIncrementInfoMaster WHERE IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "Name", "IncrementInfoMasterId", queryStr, DataBase.HRDB);

        }
        public DataTable CheckStartEndDateExistOrNotDAL(string FinancialYearId, string StartDate, string EndDate)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@StartDate", StartDate));
            aSqlParameterlist.Add(new SqlParameter("@EndDate", EndDate));


            const string queryStr = @"
SELECT FinancialYearId ,
       StartDate ,
       EndDate 
       FROM tblFinancialYear  WHERE  FinancialYearId=@FinancialYearId and StartDate <= @StartDate AND EndDate >= @EndDate";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadFinancialYear(DropDownList ddl, string companyId)
        {
            string query = @"SELECT FNY.FinancialYearId,
                             FNY.FinancialYearDesc FROM dbo.tblFinancialYear AS FNY WHERE FNY.Status = 'Active' AND FNY.CompanyId =" + companyId;
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, DataBase.HRDB);
        }

        public void LoadDivision(DropDownList ddl, string companyId)
        {
            string query = @"SELECT DSN.DivisionId,
                            DSN.DivisionName FROM dbo.tblDivision AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId;
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", query, DataBase.HRDB);
        }


        public void LoadCategory(DropDownList ddl)
        {

         string query = @"SELECT DSN.EmpCategoryId,
                            DSN.EmpCategoryName FROM dbo.tblEmpCategory AS DSN WHERE DSN.IsActive = 1";
         

        
           aCommonInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", query, DataBase.HRDB);
        }

        public DataTable LoadEmployeeInformation(string parameter)
        {
            string query = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode, EGI.EmpName, ISNULL(EGI.DesignationId,0)DesignationId, ISNULL(EGI.DepartmentId,0)DepartmentId, ISNULL(EGI.DivisionId,0)DivisionId
, ISNULL(EGI.DivisionWId,0)DivisionWId, ISNULL(EGI.SectionId,0)SectionId, ISNULL(EGI.SubSectionId,0)SubSectionId,ISNULL(EGI.SalaryLoationId,0)SalaryLoationId,ISNULL(EGI.JobLocationId,0)JobLocationId,ISNULL(EGI.EmpTypeId,0)EmpTypeId
,EGI.DateOfJoin, DATEDIFF(DAY,EGI.DateOfJoin,GETDATE()) AS ServiceLength, 
                             ISNULL(EGI.SalaryGradeId,0)SalaryGradeId,ISNULL(EGI.SalaryStepId,0)SalaryStepId,FINY.FinancialYearDesc,CI.CompanyName,ISNULL(EGI.DivisionId,0)DivisionId, 
							ISNULL( EGI.EmpCategoryId,0)EmpCategoryId,ISNULL( EGI.EmpTypeId,0)EmpTypeId
FROM dbo.tblEmpGeneralInfo AS EGI 

                             INNER JOIN dbo.tblDesignation AS DSN ON DSN.DesignationId = EGI.DesignationId
                             INNER JOIN dbo.tblDepartment AS DPT ON DPT.DepartmentId = EGI.DepartmentId
                             INNER JOIN dbo.tblFinancialYear AS FINY ON FINY.CompanyId = EGI.CompanyId
                             INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                             INNER JOIN dbo.tblEmpCategory AS EmpCat ON EmpCat.EmpCategoryId = EGI.EmpCategoryId
							 
							  WHERE  EGI.SalaryStepId IS NOT NULL AND EGI.IsActive = 1 " + parameter;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }



        public DataTable LoadIncreamentGetApprovalInformation(string idd)
        {
            string query = @" SELECT Inc.ActionStatus,  EGE.EmpInfoId as UserEmpInfoId,Inc.EmployeeId as EmpInfoId,Inc.IncrementId, CI.ShortName, FINY.FinancialYearDesc, IncType.Name, FORMAT(Inc.EffectiveDate,'dd-MMM-yyyy') EffectiveDate, EGI.EmpMasterCode, EGI.EmpName, DSN.Designation, DPT.DepartmentName,
FORMAT(EGI.DateOfJoin,'dd-MMM-yyyy') DateOfJoin, sg.GradeCode, stCrnt.SalaryStepName SalaryStepName,   stNew.SalaryStepName NewSalaryStepName, Inc.FeedSalary,U.EmpInfoId AS UserEmpInfoId ,* FROM tblIncrement  Inc
 INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON Inc.EmployeeId = EGI.EmpInfoId
 INNER JOIN dbo.tblDesignation AS DSN ON DSN.DesignationId = EGI.DesignationId
                             left JOIN dbo.tblDepartment AS DPT ON DPT.DepartmentId = EGI.DepartmentId
                             left JOIN dbo.tblFinancialYear AS FINY ON FINY.FinancialYearId = Inc.FinancialYearId
                             left JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = Inc.CompanyId
                             left JOIN dbo.tblEmpCategory AS EmpCat ON EmpCat.EmpCategoryId = EGI.EmpCategoryId
							 left JOIN dbo.tblIncrementInfoMaster AS IncType ON Inc.IncrementTypeId = IncType.IncrementInfoMasterId
							 left JOIN dbo.tblSalaryGrade sg ON Inc.SalaryGradeId=sg.SalaryGradeId
			                left JOIN dbo.tblSalaryStep stCrnt ON Inc.CurrentStepId=stCrnt.SalaryStepId			                
							left JOIN dbo.tblSalaryStep stNew ON Inc.IncrementalStepId=stNew.SalaryStepId
LEFT JOIN dbo.tblUser AS U ON U.UserId = Inc.EntryBy
							INNER JOIN dbo.tblEmpGeneralInfo EGE ON EGE.EmpInfoId=U.EmpInfoId
							WHERE Inc.IncrementId=" + idd;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }




        public DataTable GetAppLogCommByJobId(int jobId)
        {
            string query = @" SELECT Alg.IncrementAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, Alg.ActionStatus, Alg.ApproveDate, Alg.IncrementId, Alg.Comments
  FROM tblIncrementAppLog Alg
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and Alg.IncrementId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable ValidattionForEffectiveDate(string id, string date, string type)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
            aSqlParameterlist.Add(new SqlParameter("@IncrementTypeId", type));


            string query = @"SELECT EmployeeId, EffectiveDate FROM dbo.tblIncrement WHERE IncrementTypeId=@IncrementTypeId and EmployeeId=@EmployeeId and EffectiveDate=@EffectiveDate";
            return aCommonInternalDal.DataContainerDataTable(query,aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT  IncrementId, EffectiveDate FROM dbo.tblIncrement WHERE IncrementId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public void LoadDesignation(DropDownList ddl)
        {
            string query = @"SELECT DSN.DesignationId,DSN.Designation FROM dbo.tblDesignation AS DSN WHERE DSN.IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, DataBase.HRDB);
        }

        public void LoadDepartment(DropDownList ddl)
        {
            string query = @"SELECT DPT.DepartmentId,DPT.DepartmentName FROM tblDepartment AS DPT WHERE DPT.IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, DataBase.HRDB);
        }

        public void LoadSalaryGrade(DropDownList ddl)
        {
            string query = @"SELECT SLG.SalaryGradeId,SLG.GradeCode FROM dbo.tblSalaryGrade AS SLG WHERE SLG.IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(ddl, "GradeCode", "SalaryGradeId", query, DataBase.HRDB);
        }

        public void LoadSalaryStep(DropDownList ddl, string salaryGradeId)
        {
            string query = @"SELECT SLTP.SalaryStepId,
              SLTP.SalaryStepName FROM dbo.tblSalaryStep AS SLTP WHERE SLTP.IsActive = 1 AND SLTP.SalaryGradeId = " + salaryGradeId;
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryStepName", "SalaryStepId", query, DataBase.HRDB);
        }

        public int SaveIncrementInfo(IncrementDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aDao.FinancialYearId));


          
                aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aDao.DivisionId ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@IncrementTypeId", (object)aDao.IncrementTypeId ?? DBNull.Value));

            



                aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aDao.DivisionWId ?? DBNull.Value));  
           
        

            
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aDao.DepartmentId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aDao.SectionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aDao.SubSectionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aDao.SalaryLoationId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aDao.JobLocationId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aDao.EmpTypeId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", (object)aDao.DesignationId ?? DBNull.Value));
     
            
           
         
            
         
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", aDao.EmployeeCode));
           
           
           
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@CurrentStepId", aDao.CurrentStepId));
            aSqlParameterlist.Add(new SqlParameter("@IncrementalStepId", aDao.IncrementalStepId));
            aSqlParameterlist.Add(new SqlParameter("@FeedSalary", aDao.FeedSalary));
 
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", DateTime.Now));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", (object)aDao.EffectiveDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AutoProcess", (object)aDao.AutoProcess ?? DBNull.Value));

            string query = @"INSERT INTO dbo.tblIncrement
                           (CompanyId,
                            FinancialYearId, EffectiveDate,
                          
                            EmployeeId,
                            EmployeeCode,
                           
                            DesignationId,
                            DepartmentId,                           
                            SalaryGradeId,
                            CurrentStepId,
                            IncrementalStepId,
                            FeedSalary,                           
                            EntryBy,
                            EntryDate, DivisionId, DivisionWId,   SectionId, SubSectionId,    SalaryLoationId, JobLocationId , EmpTypeId,AutoProcess, IncrementTypeId
                            )
                            VALUES
                            (@CompanyId,
                            @FinancialYearId, @EffectiveDate,                          
                            @EmployeeId,
                            @EmployeeCode,
                            @DesignationId,
                            @DepartmentId,
                            @SalaryGradeId,
                            @CurrentStepId,
                            @IncrementalStepId,
                            @FeedSalary,                           
                            @EntryBy,
                            @EntryDate, @DivisionId, @DivisionWId,   @SectionId, @SubSectionId,   @SalaryLoationId, @JobLocationId , @EmpTypeId, @AutoProcess, @IncrementTypeId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable LoadIncrementInfo(string parameter, string parameter2)
        {
            string query = @"SELECT INC.IncrementId, INC.EmployeeId, INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName AS IncrementalStep, INC.EffectiveDate,ActionStatus2,ForEmp.EmpName as AwEmpName FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                            LEFT JOIN (SELECT IncrementId,MAX(Version)MaxVer FROM dbo.tblIncrementAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY IncrementId) AS LogApp ON LogApp.IncrementId= INC.IncrementId
								LEFT JOIN dbo.tblIncrementAppLog ON tblIncrementAppLog.IncrementId = INC.IncrementId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblIncrementAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblIncrementAppLog PreLog ON PreLog.IncrementId=INC.IncrementId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId
" + parameter + @" UNION ALL
 SELECT 0 AS IncrementId, EmployeeId EmployeeId, Emp.EmpMasterCode EmployeeCode, EmpName EmpName,DSG.Designation Designation ,DPT.DepartmentName DepartmentName,null PreviousStep, 
								 --'Yearly increment' Name,
								   IncrementalStep IncrementalStep, EffectiveDate EffectiveDate,    '' ActionStatus2,''  as AwEmpName  FROM tblIncrement_HistoricalData
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null " + parameter2 + " order by EffectiveDate desc ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable LoadIncrementInfoForProcess(string parameter)
        {
            string query = @"SELECT INC.IncrementId, INC.EmployeeId, INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName AS IncrementalStep, INC.EffectiveDate,ActionStatus2,ForEmp.EmpName as AwEmpName FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                            LEFT JOIN (SELECT IncrementId,MAX(Version)MaxVer FROM dbo.tblIncrementAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY IncrementId) AS LogApp ON LogApp.IncrementId= INC.IncrementId
								LEFT JOIN dbo.tblIncrementAppLog ON tblIncrementAppLog.IncrementId = INC.IncrementId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblIncrementAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblIncrementAppLog PreLog ON PreLog.IncrementId=INC.IncrementId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId
" + parameter + " order by EffectiveDate desc ";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public bool DeleteIncrementMaster(IncrementDao aDao)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@IncrementId", aDao.IncrementId));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aDao.DeleteDate));
            string query = @"UPDATE dbo.tblIncrement SET IsDelete =  @IsDelete,DeleteBy =  @DeleteBy,DeleteDate =  @DeleteDate WHERE IncrementId = @IncrementId ";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEmployeeIncrementalStepInfo(EmpGeneralInfo aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalScaleId", aDao.SalScaleId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDao.EmpInfoId));

            aSqlParameterlist.Add(new SqlParameter("@Updateby", Convert.ToInt32(HttpContext.Current.Session["UserId"])));



            string query = @"UPDATE tblEmpGeneralInfo SET SalaryStepId = @SalScaleId, Updateby=@Updateby, UpdateDate=GETDATE() WHERE EmpInfoId = @EmpInfoId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
        }

        public bool ResetEmployeeIncrementalStepInfo(EmpGeneralInfo aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalScaleId", aDao.SalScaleId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDao.EmpInfoId));

            string query = @"UPDATE tblEmpGeneralInfo SET SalaryStepId = @SalScaleId WHERE EmpInfoId = @EmpInfoId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable FetchEmployeeInfoById(int incrementId)
        {
            string query = @"SELECT EmployeeId,CurrentStepId FROM dbo.tblIncrement WHERE IncrementId = " + incrementId;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public bool UpdateContractural(IncrementDao aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.IncrementId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IncrementId", aMaster.IncrementId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblIncrement set ActionStatus=@ActionStatus  where IncrementId = @IncrementId";

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
        public bool UpdateJobReqStatus2(IncrementDao aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.IncrementId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IncrementId", aMaster.IncrementId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblIncrement set ActionStatus2=@ActionStatus  where IncrementId = @IncrementId";

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

        public DataTable GetSupervisorAppId(string url, string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblSupevisorMenuApproval.MainMenuId WHERE URL='" + url + "' " + param + "";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
        public DataTable GetHRAdminEmployeeAppId(string parameter)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblEmployeeApprovalByOpearationDetail
            LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId " + parameter + "";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
        {

            try
            {
                string query = @"SELECT * FROM dbo.tblIncrementAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND IncrementId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review') ORDER BY IncrementAppLogId DESC";

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
                    aParameters.Add(new SqlParameter("@IncrementAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblIncrementAppLog set ActionStatus=@ActionStatus  where IncrementAppLogId = @IncrementAppLogId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public int SavAppLog(IncrementAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IncrementId", appLogDao.IncrementId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId));


                    string query = @"INSERT INTO dbo.tblIncrementAppLog
                                    (
                                    IncrementId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @IncrementId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblIncrementAppLog WHERE IncrementId=@IncrementId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsId
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

        public int SaveComment(string masterId, string empinfoId, string comments)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    //aParameters.Add(new SqlParameter("@Id", masterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", empinfoId));
                    aParameters.Add(new SqlParameter("@Comments", comments));


                    string query = @" INSERT INTO dbo.tblIncrementAppLogComnt
                                    (
                                        EmpInfoId,
                                        Comments
                                    )
                                    VALUES
                                    (   @EmpInfoId,
                                        @Comments
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

        public DataTable GetEmpInfo(string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblEmpGeneralInfo " + param + "";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetContractualDataInfo(string id)
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


            string queryStr = @"SELECT CONVERT(BIT,(CASE WHEN IsSelfApp IS NULL or IsSelfApp=0 THEN '0' ELSE '1' END ))IsSelfApp,* FROM dbo.tblIncrement
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblIncrement.EmployeeId WHERE IncrementId='" + id + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public bool UpdateSelfApprove(int id, bool selfapp)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ID", id));
                    aParameters.Add(new SqlParameter("@IsSelfApp", selfapp));


                    string query =
                        @"update tblIncrement set IsSelfApp=@IsSelfApp  where IncrementId = @ID";

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
        public DataTable LoadIncrementInfoApp()
        {
            string query = @"SELECT INC.IncrementId, INC.EmployeeId, INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName AS IncrementalStep, INC.EffectiveDate,IncrementAppLogId FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId 
                            INNER JOIN (SELECT IncrementId,MAX(Version)MaxVer FROM dbo.tblIncrementAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY IncrementId) AS CELog ON CELog.IncrementId= INC.IncrementId
								INNER JOIN dbo.tblIncrementAppLog ON tblIncrementAppLog.IncrementId = INC.IncrementId
                                where (INC.IsDelete is null or INC.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


    }
}
