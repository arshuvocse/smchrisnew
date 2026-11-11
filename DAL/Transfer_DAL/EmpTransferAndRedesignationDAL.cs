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

namespace DAL.Transfer_DAL
{
    public class EmpTransferAndRedesignationDAL
    {




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

        public void LoadPreSalaryGradeDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblSalaryGrade";
            aCommonInternalDal.LoadDropDownValue(ddl, "GradeCode", "SalaryGradeId", query, "HRDB");
        }
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        EmpTransferAndRedesignationDao aEmpTransferAndRedesignationDao= new EmpTransferAndRedesignationDao();
        public void EmployeeNameDropDown(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblEmpGeneralInfo WHERE CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", query, "HRDB");

        }
        public void FinancialYearDropDown(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT  FinancialYearId, FinancialYearDesc FROM dbo.tblFinancialYear WHERE Status='Active' and CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");

        }


        public void NewReportingEmployeeNameDropDown(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblEmpGeneralInfo WHERE CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", query, "HRDB");

        }


        public void LoadNewdesignationDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblDesignation";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }

    

        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
                aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
        public void LoadAllCompanyDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo ";
                aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
            }
            catch (Exception)
            {

                //throw;
            }
        }
        public void LoadCompanyDropDownListOld(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

        public DataTable LoadInfoSeparationDAL(string param)
        {

            List<SqlParameter> aSqlParameters = new List<SqlParameter>();

            aSqlParameters.Add(new SqlParameter("@Pram", param));

            return aCommonInternalDal.GetDataByStoreProcedure("sp_AccountsIntegrationSeperation", aSqlParameters, "HRDB");

        }

            public void LoadSalaryLocationDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblSalaryLocation WHERE IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryLocation", "SalaryLoationId", query, "HRDB");
        }

        public void LoadSalaryLocationDropDownListOld(DropDownList ddl)
        {
            string query = "SELECT * FROM tblSalaryLocation";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryLocation", "SalaryLoationId", query, "HRDB");
        }



        public void LoadJobLocationDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblJobLocation WHERE  IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(ddl, "Location", "JobLocationID", query, "HRDB");
        }
        public void LoadJobLocationDropDownList(DropDownList ddl,string id)
        {
            string query = "SELECT * FROM tblJobLocation WHERE IsActive = 1 AND SalaryLoationId='" + id + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Location", "JobLocationID", query, "HRDB");
        }
        public void LoadJobLocationDropDownListOld(DropDownList ddl, string id)
        {
            string query = "SELECT * FROM tblJobLocation WHERE SalaryLoationId='" + id + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Location", "JobLocationID", query, "HRDB");
        }

        public void LoadDivisionDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblDivision WHERE IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", query, "HRDB");
        }

        public void LoadDivisionDropDownListOld(DropDownList ddl)
        {
            string query = "SELECT * FROM tblDivision";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", query, "HRDB");
        }

        public void LoadDivisionWingDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblDivisionWing  WHERE IsActive = 1 and ((Invisible =0 ) OR (Invisible IS  NULL))";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", query, "HRDB");
        }
        public void LoadDivisionWingDropDownListOld(DropDownList ddl)
        {
            string query = "SELECT * FROM tblDivisionWing ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", query, "HRDB");
        }

        public void LoadSectionDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblSection  WHERE IsActive = 1 and ((Invisible =0 ) OR (Invisible IS  NULL))";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", query, "HRDB");
        }
        public void LoadSectionDropDownListOld(DropDownList ddl)
        {
            string query = "SELECT * FROM tblSection";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", query, "HRDB");
        }
        public void LoadSubSectionDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblSubSection WHERE IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(ddl, "SubSectionName", "SubSectionId", query, "HRDB");
        }
        public void LoadSubSectionDropDownListOld(DropDownList ddl)
        {
            string query = "SELECT * FROM tblSubSection ";
            aCommonInternalDal.LoadDropDownValue(ddl, "SubSectionName", "SubSectionId", query, "HRDB");
        }

        public void LoadOldDepartmentDropDownList(DropDownList ddl)
        {
            string query = "SELECT DepartmentId, DepartmentName FROM dbo.tblDepartment  WHERE  IsActive = 1 and ((Invisible =0 ) OR (Invisible IS  NULL))";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, "HRDB");
        }

        public void LoadOldDepartmentDropDownListOld(DropDownList ddl)
        {
            string query = "SELECT DepartmentId, DepartmentName FROM dbo.tblDepartment";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, "HRDB");
        }


        public DataTable LoadEmpJInfoInTextBoxById(int id)
        {
            string query = @"SELECT  Egf.SubSectionId ,deg.Designation,SG.GradeName,Com.CompanyName, loc.SalaryLocation, JLoc.Location, Div.DivisionName, Wing.DivisionWingName, Egf.SalaryGradeId, Sec.SectionName, SubSec.SubSectionName, Egf.DateOfJoin, Egf.DepartmentId, Egf.ReportingEmpId, *  FROM dbo.tblEmpGeneralInfo Egf

							left JOIN dbo.tblDesignation  deg ON Egf.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON Egf.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON Egf.CompanyId=Com.CompanyId
							left JOIN dbo.tblSalaryLocation  Loc ON Egf.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON Egf.JobLocationId=JLoc.JobLocationID
							left JOIN dbo.tblDivision  Div ON Egf.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON Egf.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSection  Sec ON Egf.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON Egf.SubSectionId=SubSec.SubSectionId			
				
		
							 WHERE Egf.EmpInfoId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadSuperviseEmployee(string id)
        {
            string query = @"SELECT 0 PrevEmpReportingBodyId, * FROM dbo.tblEmpGeneralInfo WHERE ReportingEmpId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadSuperviseEmployeeActive(string id)
        {
            string query = @"SELECT 0 PrevEmpReportingBodyId, * FROM dbo.tblEmpGeneralInfo WHERE ReportingEmpId='" + id + "' and IsActive=1";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public void LoadNewdesignationDropDownListBySalaryId(DropDownList ddl, string SalaryGradeId)
        {

            string query = "SELECT * FROM tblDesignation WHERE IsActive = 1 AND SalaryGradeId='" + SalaryGradeId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }

        public void LoadNewdesignationDropDownListBySalaryIdEdit(DropDownList ddl, string SalaryGradeId)
        {

            string query = "SELECT * FROM tblDesignation WHERE IsActive = 1 AND SalaryGradeId='" + SalaryGradeId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }



        public void LoadNewdesignationDropDownListByEmpTransferId(DropDownList ddl, string EmpTransferId)
        {

            string query = "SELECT * FROM tblDesignation WHERE IsActive='true' AND SalaryGradeId='" + EmpTransferId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }


        public void LoadNewdesignationDropDownListBySalaryIdDtab(DropDownList ddl, string SalaryGradeId)
        {

            string query = "SELECT * FROM tblDesignation WHERE IsActive='true' AND SalaryGradeId='" + SalaryGradeId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }

      

        public void GetNewDivisionDropDownList(DropDownList ddl, string companyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            string queryStr = "SELECT DivisionId, DivisionName FROM dbo.tblDivision WHERE  IsActive = 1 and CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, aSqlParameterlist, "HRDB");
        }



        public void GetNewWingDropDownList(DropDownList ddl, string companyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            string queryStr = "SELECT DivisionWId, DivisionWingName FROM dbo.tblDivisionWing  WHERE  IsActive = 1 and ((Invisible =0 ) OR (Invisible IS  NULL))  AND   CompanyId =@CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }


        public void GetNewSectionDropDownList(DropDownList ddl, string companyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            string queryStr = "SELECT SectionId, SectionName FROM dbo.tblSection  WHERE  IsActive = 1 and ((Invisible =0 ) OR (Invisible IS  NULL))   AND  CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
        }



        public void GetNewSubsectionDropDownList(DropDownList ddl, string companyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            string queryStr = "  SELECT SubSectionId, SubSectionName FROM dbo.tblSubSection WHERE  IsActive = 1 and   CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "SubSectionName", "SubSectionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable ValidattionForEffectiveDate(string id, string date)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
            string query = @"SELECT EmployeeId, EffectiveDate FROM dbo.tblEmpTransferAndRedesignation WHERE  EmployeeId=@EmployeeId and EffectiveDate=@EffectiveDate";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT  EmpTransferAndRedesignationId, EffectiveDate FROM dbo.tblEmpTransferAndRedesignation WHERE EmpTransferAndRedesignationId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
                        @"update tblEmpTransferAndRedesignationAppLog set ActionStatus=@ActionStatus  where EmpTransferAndRedesignationAppLogAppLogId = @IncrementAppLogId";

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
                string query = @"SELECT * FROM dbo.tblEmpTransferAndRedesignationAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND EmpTransferAndRedesignationId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')  order by EmpTransferAndRedesignationAppLogAppLogId desc ";

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


            string queryStr = @"
SELECT   CONVERT(BIT,(CASE WHEN IsSelfApp IS NULL or IsSelfApp=0 THEN '0' ELSE '1' END ))IsSelfApp,* FROM dbo.tblEmpTransferAndRedesignation
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=tblEmpTransferAndRedesignation.EmployeeId WHERE  EmpTransferAndRedesignationId='" + id + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public int SavAppLog(EmpTransferAndRedesignationAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IncrementId", appLogDao.EmpTransferAndRedesignationId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId));


                    string query = @"INSERT INTO dbo.tblEmpTransferAndRedesignationAppLog
                                    (
                                    EmpTransferAndRedesignationId,
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
                                    (SELECT (COUNT(*)+1) FROM dbo.tblEmpTransferAndRedesignationAppLog WHERE EmpTransferAndRedesignationId=@IncrementId),
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



        public DataTable GetDataReviewEntryBy(string id, string entryby, string actionstatu)
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


            string queryStr = @"SELECT * FROM dbo.tblEmpTransferAndRedesignation WHERE ActionStatus='" + actionstatu + "' AND EntryBy='" + entryby + "' AND EmpTransferAndRedesignationId='" + id + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public bool UpdateJobReqStatus2(EmpTransferAndRedesignationDao aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.EmpTransferAndRedesignationId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IncrementId", aMaster.EmpTransferAndRedesignationId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblEmpTransferAndRedesignation set ActionStatus2=@ActionStatus  where EmpTransferAndRedesignationId = @IncrementId";

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


                    string query = @" INSERT INTO dbo.tblEmpTransferAndRedesignationAppLogComnt
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


        public bool UpdateContractural(EmpTransferAndRedesignationDao aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.EmpTransferAndRedesignationId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@IncrementId", aMaster.EmpTransferAndRedesignationId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblEmpTransferAndRedesignation set ActionStatus=@ActionStatus  where EmpTransferAndRedesignationId = @IncrementId";

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
                        @"update tblEmpTransferAndRedesignation set IsSelfApp=@IsSelfApp  where EmpTransferAndRedesignationId = @ID";

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
        public Int32 EmpTransferAndRedesignationSaveInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmpTransferAndDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmpTransferAndDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@IsOnlyTransfer", aEmpTransferAndDao.IsOnlyTransfer ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aEmpTransferAndDao.FinancialYearId));
         
            aSqlParameterlist.Add(new SqlParameter("@IsInterCompanyTransfer", aEmpTransferAndDao.IsInterCompanyTransfer ?? (object)DBNull.Value));
        
            aSqlParameterlist.Add(new SqlParameter("@NewCompanyId", aEmpTransferAndDao.NewCompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldCompanyId", aEmpTransferAndDao.OldCompanyId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewSalaryLocationId", aEmpTransferAndDao.NewSalaryLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldSalaryLocationId", aEmpTransferAndDao.OldSalaryLocationId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewJobLocationId", aEmpTransferAndDao.NewJobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldJobLocationId", aEmpTransferAndDao.OldJobLocationId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewDivisionId", aEmpTransferAndDao.NewDivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldDivisionId", aEmpTransferAndDao.OldDivisionId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewWingId", aEmpTransferAndDao.NewWingId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldWingId", aEmpTransferAndDao.OldWingId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewDepartmentId", aEmpTransferAndDao.NewDepartmentId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldDepartmentId", aEmpTransferAndDao.OldDepartmentId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewSectionId", aEmpTransferAndDao.NewSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldSectionId", aEmpTransferAndDao.OldSectionId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewSubSectionId", aEmpTransferAndDao.NewSubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldSubSectionId", aEmpTransferAndDao.OldSubSectionId ?? (object)DBNull.Value));


            aSqlParameterlist.Add(new SqlParameter("@NewEmpReportingBodyId", aEmpTransferAndDao.NewEmpReportingBodyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldReportingBodyID", aEmpTransferAndDao.OldReportingBodyID ?? (object)DBNull.Value));



            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aEmpTransferAndDao.EffectiveDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AutoProcess", aEmpTransferAndDao.AutoProcess ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aEmpTransferAndDao.EmpTypeId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmpTransferAndDao.Remarks ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsReappointment", aEmpTransferAndDao.IsReappointment ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", aEmpTransferAndDao.EmployeeCode));

          

            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmpTransferAndDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmpTransferAndDao.EntryDate));
            string insertQuery = @"INSERT INTO [dbo].[tblEmpTransferAndRedesignation]
            ([CompanyId]
           ,[FinancialYearId]
           ,[EmployeeId]
           ,[EmpTypeId]
           ,[IsInterCompanyTransfer]
           ,[IsOnlyTransfer]
           ,[NewCompanyId]
           ,[NewSalaryLocationId]
           ,[NewJobLocationId]
           ,[NewDivisionId]
           ,[NewWingId]
           ,[NewDepartmentId]
           ,[NewSectionId]
           ,[NewSubSectionId]
           ,[NewEmpReportingBodyId]
           ,[OldCompanyId]
           ,[OldSalaryLocationId]
           ,[OldJobLocationId]
           ,[OldDivisionId]
           ,[OldWingId]
           ,[OldDepartmentId]
           ,[OldSectionId]
           ,[OldSubSectionId]
           ,[OldReportingBodyID]
           ,[EffectiveDate]
           ,[Remarks]
           ,[EntryBy]
           ,[EntryDate]
           
           ,[AutoProcess],EmployeeCode,IsReappointment)
     VALUES
           (@CompanyId
           ,@FinancialYearId
           ,@EmployeeId
           ,@EmpTypeId
           ,@IsInterCompanyTransfer
           ,@IsOnlyTransfer
           ,@NewCompanyId
           ,@NewSalaryLocationId
           ,@NewJobLocationId
           ,@NewDivisionId
           ,@NewWingId
           ,@NewDepartmentId
           ,@NewSectionId
           ,@NewSubSectionId
           ,@NewEmpReportingBodyId
           ,@OldCompanyId
           ,@OldSalaryLocationId
           ,@OldJobLocationId
           ,@OldDivisionId
           ,@OldWingId
           ,@OldDepartmentId
           ,@OldSectionId
           ,@OldSubSectionId
           ,@OldReportingBodyID
           ,@EffectiveDate
           ,@Remarks
           ,@EntryBy
           ,@EntryDate
           ,@AutoProcess, @EmployeeCode, @IsReappointment)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public Int32 EmpTransferAndRedesignationDSSaveInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", aEmpTransferAndDao.EmpTransferAndRedesignationId));

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));

            string insertQuery = @"INSERT INTO [dbo].[tblEmpTransferAndRedesignationDS]
           (EmpTransferAndRedesignationId
           ,EmpInfoId
           )
     VALUES
           (@EmpTransferAndRedesignationId
           ,@EmpInfoId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public bool EmpSpecialTransfer (EmpSpecialTransferDAO aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", aEmpTransferAndDao.EmpTransferAndRedesignationId));

            aSqlParameterlist.Add(new SqlParameter("@SpecialTransfer", aEmpTransferAndDao.SpecialTransfer ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RegularTransfer", aEmpTransferAndDao.RegularTransfer ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FullTransfer", aEmpTransferAndDao.FullTransfer ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SalaryTransfer", aEmpTransferAndDao.SalaryTransfer ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OnlyView", aEmpTransferAndDao.OnlyView ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@EditView", aEmpTransferAndDao.EditView ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RecordUpdateTypeSalaryTransfer", aEmpTransferAndDao.RecordUpdateTypeSalaryTransfer ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmpTransferAndDao.EmployeeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewEmployeeId", aEmpTransferAndDao.NewEmployeeId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OnlyViewComId", aEmpTransferAndDao.OnlyViewComId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EditViewComId", aEmpTransferAndDao.EditViewComId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@IsSMCRecordView", aEmpTransferAndDao.IsSMCRecordView ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsELRecordView", aEmpTransferAndDao.IsELRecordView ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewComId", aEmpTransferAndDao.NewComId ?? (object)DBNull.Value));


            return aCommonInternalDal.SaveDataByInsertSP("sp_Save_SP", aSqlParameterlist, DataBase.HRDB);



             }


        public Int32 WithoutEmpSpecialTransfer(int? OldEmployeeId, int? NewEmployeeId, int? NewComId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", OldEmployeeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewEmployeeId", NewEmployeeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewComId", NewComId ?? (object)DBNull.Value));





            string insertQuery = @"


 



declare @KPIDeadLineMasterId int =0
declare @AppraisalSelfMasterId int =0 
declare @AppraisalSelfMasterIdJust int =0 
 

declare @FinancialYearId int =0 
declare @FinancialYear  nvarchar(max)
 
declare @AppraisalMasterId int =0 
select @AppraisalMasterId=ISNULL(AppraisalMasterId,0),@FinancialYear=fin.FinancialYearDesc, @AppraisalSelfMasterId= AppraisalSelfMasterId   from tblAppraisalMaster mas
inner join tblFinancialYear fin on fin.FinancialYearId=mas.FinancialYearId
  where EmpInfoId=@EmployeeId and CurrentStatus<>'Approved'

  if(@AppraisalMasterId=0)
  begin

  select @AppraisalMasterId=ISNULL(AppraisalMasterId,0),@FinancialYear=fin.FinancialYearDesc, @AppraisalSelfMasterId= AppraisalSelfMasterId   from tblAppraisalMaster mas
inner join tblFinancialYear fin on fin.FinancialYearId=mas.FinancialYearId
  where EmpInfoId=@EmployeeId and CurrentStatus='Approved' and mas.SelfApprove='Posted'
  end
print @AppraisalMasterId
print @FinancialYear

if(@FinancialYear<>'')
begin
 select @FinancialYearId= FinancialYearId from tblFinancialYear where FinancialYearDesc= @FinancialYear  and CompanyId=@NewComId
 end

if(@AppraisalMasterId>0)
begin 

declare @AppraisalDeadLineMasterId int =0
select @AppraisalDeadLineMasterId=mas.AppraisalDeadLineMasterId from tblAppraisalDeadLineDetails  dtl
inner join tblAppraisalDeadlineMaster mas on dtl.AppraisalDeadLineMasterId=mas.AppraisalDeadLineMasterId
  where  FinancialYearId=@FinancialYearId and CompanyId=@NewComId
 INSERT INTO [dbo].[tblAppraisalDeadLineDetails]
           ([AppraisalDeadLineMasterId]
           ,[EmpinfoId]
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate])
    select top 1 @AppraisalDeadLineMasterId
           ,@NewEmployeeId
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate] from  [tblAppraisalDeadLineDetails]  where [AppraisalDeadLineMasterId]=@AppraisalDeadLineMasterId


update tblAppraisalMaster  set    EmpInfoId=@NewEmployeeId, FinancialYearId=@FinancialYearId where   AppraisalMasterId=@AppraisalMasterId




update  tblAppraisalMasterAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)),  PreEmpInfoId=@NewEmployeeId where AppraisalMasterId=@AppraisalMasterId  and PreEmpInfoId=@EmployeeId


update  tblAppraisalMasterAppLog set   Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)),    ForEmpInfoId=@NewEmployeeId where AppraisalMasterId=@AppraisalMasterId and ForEmpInfoId=@EmployeeId

end


if(@AppraisalSelfMasterId>0)
begin 

select @KPIDeadLineMasterId=mas.KPIDeadLineMasterId from tblKPIDeadLineDetails  dtl
inner join tblKpiDeadlineMaster mas on dtl.KPIDeadLineMasterId=mas.KPIDeadLineMasterId
  where  FinancialYearId=@FinancialYearId and CompanyId=@NewComId
  INSERT INTO [dbo].[tblKPIDeadLineDetails]
           ([KPIDeadLineMasterId]
           ,[EmpinfoId]
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend])
     select top 1 @KPIDeadLineMasterId
           ,@NewEmployeeId
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend] from [tblKPIDeadLineDetails] where   KPIDeadLineMasterId=@KPIDeadLineMasterId

update tblAppraisalSelfMaster  set EmpInfoId=@NewEmployeeId, FinancialYearId=@FinancialYearId where    AppraisalSelfMasterId=@AppraisalSelfMasterId



update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), PreEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterId  and PreEmpInfoId=@EmployeeId


update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), ForEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterId  and ForEmpInfoId=@EmployeeId
end

else
begin
select   @FinancialYear=fin.FinancialYearDesc,@AppraisalSelfMasterIdJust= ISNULL(AppraisalSelfMasterId,0)   from tblAppraisalSelfMaster   mas
inner join tblFinancialYear fin on fin.FinancialYearId=mas.FinancialYearId  
  where EmpInfoId=@EmployeeId and ActionStatus<>'Approved'
end


if(@FinancialYear<>'')
begin
 select @FinancialYearId= FinancialYearId from tblFinancialYear where FinancialYearDesc= @FinancialYear  and CompanyId=@NewComId
 end


if(@AppraisalSelfMasterIdJust>0)
begin  
select @KPIDeadLineMasterId=mas.KPIDeadLineMasterId from tblKPIDeadLineDetails  dtl
inner join tblKpiDeadlineMaster mas on dtl.KPIDeadLineMasterId=mas.KPIDeadLineMasterId
  where  FinancialYearId=@FinancialYearId and CompanyId=@NewComId
if(@KPIDeadLineMasterId>0)
begin
  INSERT INTO [dbo].[tblKPIDeadLineDetails]
           ([KPIDeadLineMasterId]
           ,[EmpinfoId]
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend])
     select top 1 @KPIDeadLineMasterId
           ,@NewEmployeeId
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend] from [tblKPIDeadLineDetails] where   KPIDeadLineMasterId=@KPIDeadLineMasterId


update tblAppraisalSelfMaster  set EmpInfoId=@NewEmployeeId, FinancialYearId=@FinancialYearId where    AppraisalSelfMasterId=@AppraisalSelfMasterIdJust


update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), PreEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterIdJust  and PreEmpInfoId=@EmployeeId


update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), ForEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterIdJust  and ForEmpInfoId=@EmployeeId
end
end

update tbl_AdvanceBill_Healthcare  set EmpInfoId=@NewEmployeeId  where    EmpInfoId=@EmployeeId
update tbl_ReimbursmentFormMaster_HealthCare  set EmpInfoId=@NewEmployeeId  where    EmpInfoId=@EmployeeId

";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }


        public Int32 WithoutEmpSpecialTransferEmpGen(int? OldEmployeeId, int? NewEmployeeId, int? NewComId, int? smc, int? smcEL)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", OldEmployeeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewEmployeeId", NewEmployeeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewComId", NewComId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsSMCRecordView", smc ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsELRecordView", smcEL ?? (object)DBNull.Value));





            string insertQuery = @"


 

update      tblEmpGeneralInfo set IsSpTransfer=0 where EmpInfoId=@EmployeeId

update      tblEmpGeneralInfo set IsSpTransfer=1 where EmpInfoId=@NewEmployeeId

INSERT INTO [dbo].[tblEmpSpecialTransfer]
           ([EmpTransferAndRedesignationId]
           ,[SpecialTransfer]
           ,[RegularTransfer]
           ,[FullTransfer]
           ,[SalaryTransfer]
           ,[OnlyView]
           ,[EditView],RecordUpdateTypeSalaryTransfer,EmployeeId,NewEmployeeId,OnlyViewComId,EditViewComId,IsSMCRecordView,IsELRecordView,IsBothRecordView)
     VALUES
           (0 
           ,1
           ,0
           ,1
           ,0 
           ,1
           ,0,1,@EmployeeId,@NewEmployeeId,@NewComId,0 ,@IsSMCRecordView,@IsELRecordView,0)      





";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 EmpSpecialTransferInsertSelect(int? NewEmpId,int? OldEmpiD)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            

            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", OldEmpiD ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewEmployeeId", NewEmpId ?? (object)DBNull.Value));
           


            string insertQuery = @"


update      tblEmpGeneralInfo set IsSpTransfer=1 where EmpInfoId=@NewEmployeeId
update      tblEmpGeneralInfo set IsSpTransfer=0 where EmpInfoId=@EmployeeId
INSERT INTO [dbo].[tblEmpSpecialTransfer]
           ([EmpTransferAndRedesignationId]
           ,[SpecialTransfer]
           ,[RegularTransfer]
           ,[FullTransfer]
           ,[SalaryTransfer]
           ,[OnlyView]
           ,[EditView]
           ,[RecordUpdateTypeSalaryTransfer]
           ,[EmployeeId]
           ,[NewEmployeeId]
           ,[OnlyViewComId]
           ,[EditViewComId]
           ,[IsSMCRecordView]
           ,[IsELRecordView]
           ,[IsBothRecordView])
    select  top 1 
            0
           ,[SpecialTransfer]
           ,[RegularTransfer]
           ,[FullTransfer]
           ,[SalaryTransfer]
           ,[OnlyView]
           ,[EditView]
           ,[RecordUpdateTypeSalaryTransfer]
           ,@EmployeeId
           ,@NewEmployeeId
           ,[OnlyViewComId]
           ,[EditViewComId]
           ,[IsSMCRecordView]
           ,[IsELRecordView]
           ,[IsBothRecordView]  from tblEmpSpecialTransfer where NewEmployeeId=@EmployeeId   order by  EmpSpecialTransferId desc   









declare @KPIDeadLineMasterId int =0
declare @AppraisalSelfMasterId int =0 
declare @AppraisalSelfMasterIdJust int =0 
 

declare @FinancialYearId int =0 
declare @FinancialYear  nvarchar(max)
 
declare @AppraisalMasterId int =0 
select @AppraisalMasterId=ISNULL(AppraisalMasterId,0),@FinancialYear=fin.FinancialYearDesc, @AppraisalSelfMasterId= AppraisalSelfMasterId   from tblAppraisalMaster mas
inner join tblFinancialYear fin on fin.FinancialYearId=mas.FinancialYearId
  where EmpInfoId=@EmployeeId and CurrentStatus<>'Approved'

print @AppraisalMasterId
print @FinancialYear

if(@FinancialYear<>'')
begin
 select @FinancialYearId= FinancialYearId from tblFinancialYear where FinancialYearDesc= @FinancialYear  and CompanyId=@NewComId
 end

if(@AppraisalMasterId>0)
begin 

declare @AppraisalDeadLineMasterId int =0
select @AppraisalDeadLineMasterId=mas.AppraisalDeadLineMasterId from tblAppraisalDeadLineDetails  dtl
inner join tblAppraisalDeadlineMaster mas on dtl.AppraisalDeadLineMasterId=mas.AppraisalDeadLineMasterId
  where  FinancialYearId=@FinancialYearId and CompanyId=@NewComId
 INSERT INTO [dbo].[tblAppraisalDeadLineDetails]
           ([AppraisalDeadLineMasterId]
           ,[EmpinfoId]
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate])
    select top 1 @AppraisalDeadLineMasterId
           ,@NewEmployeeId
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate] from  [tblAppraisalDeadLineDetails]  where [AppraisalDeadLineMasterId]=@AppraisalDeadLineMasterId


update tblAppraisalMaster  set    EmpInfoId=@NewEmployeeId, FinancialYearId=@FinancialYearId where   AppraisalMasterId=@AppraisalMasterId




update  tblAppraisalMasterAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)),  PreEmpInfoId=@NewEmployeeId where AppraisalMasterId=@AppraisalMasterId  and PreEmpInfoId=@EmployeeId


update  tblAppraisalMasterAppLog set   Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)),    ForEmpInfoId=@NewEmployeeId where AppraisalMasterId=@AppraisalMasterId and ForEmpInfoId=@EmployeeId

end


if(@AppraisalSelfMasterId>0)
begin 

select @KPIDeadLineMasterId=mas.KPIDeadLineMasterId from tblKPIDeadLineDetails  dtl
inner join tblKpiDeadlineMaster mas on dtl.KPIDeadLineMasterId=mas.KPIDeadLineMasterId
  where  FinancialYearId=@FinancialYearId and CompanyId=@NewComId
  INSERT INTO [dbo].[tblKPIDeadLineDetails]
           ([KPIDeadLineMasterId]
           ,[EmpinfoId]
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend])
     select top 1 @KPIDeadLineMasterId
           ,@NewEmployeeId
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend] from [tblKPIDeadLineDetails] where   KPIDeadLineMasterId=@KPIDeadLineMasterId

update tblAppraisalSelfMaster  set EmpInfoId=@NewEmployeeId, FinancialYearId=@FinancialYearId where    AppraisalSelfMasterId=@AppraisalSelfMasterId



update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), PreEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterId  and PreEmpInfoId=@EmployeeId


update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), ForEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterId  and ForEmpInfoId=@EmployeeId
end

else
begin
select   @FinancialYear=fin.FinancialYearDesc,@AppraisalSelfMasterIdJust= ISNULL(AppraisalSelfMasterId,0)   from tblAppraisalSelfMaster   mas
inner join tblFinancialYear fin on fin.FinancialYearId=mas.FinancialYearId  
  where EmpInfoId=@EmployeeId and ActionStatus<>'Approved'
end


if(@FinancialYear<>'')
begin
 select @FinancialYearId= FinancialYearId from tblFinancialYear where FinancialYearDesc= @FinancialYear  and CompanyId=@NewComId
 end


if(@AppraisalSelfMasterIdJust>0)
begin  
select @KPIDeadLineMasterId=mas.KPIDeadLineMasterId from tblKPIDeadLineDetails  dtl
inner join tblKpiDeadlineMaster mas on dtl.KPIDeadLineMasterId=mas.KPIDeadLineMasterId
  where  FinancialYearId=@FinancialYearId and CompanyId=@NewComId
if(@KPIDeadLineMasterId>0)
begin
  INSERT INTO [dbo].[tblKPIDeadLineDetails]
           ([KPIDeadLineMasterId]
           ,[EmpinfoId]
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend])
     select top 1 @KPIDeadLineMasterId
           ,@NewEmployeeId
           ,[DeadLine]
           ,[Remarks]
           ,[ExtensionDate]
           ,[IsMailSend] from [tblKPIDeadLineDetails] where   KPIDeadLineMasterId=@KPIDeadLineMasterId


update tblAppraisalSelfMaster  set EmpInfoId=@NewEmployeeId, FinancialYearId=@FinancialYearId where    AppraisalSelfMasterId=@AppraisalSelfMasterIdJust


update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), PreEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterIdJust  and PreEmpInfoId=@EmployeeId


update  tblAppraisalSelfAppLog set  Rmrks='Transfer or State Changed'+cast(GETDATE() as nvarchar(max)), ForEmpInfoId=@NewEmployeeId where AppraisalSelfMasterId=@AppraisalSelfMasterIdJust  and ForEmpInfoId=@EmployeeId
end
end

update tbl_AdvanceBill_Healthcare  set EmpInfoId=@NewEmployeeId  where    EmpInfoId=@EmployeeId
update tbl_ReimbursmentFormMaster_HealthCare  set EmpInfoId=@NewEmployeeId  where    EmpInfoId=@EmployeeId

";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 InsertMappinigEmpRefferId(string RefId, string FinalEmpId, int ComId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RefferenceEmpId", RefId));

            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", FinalEmpId));
            aSqlParameterlist.Add(new SqlParameter("@ComId", ComId));

            string insertQuery = @"INSERT INTO [dbo].[tblEmpAllRefference]
           ([EmployeeId]
           ,[RefferenceEmpId],ShowCompany)
     VALUES
           (@EmployeeId 
           ,@RefferenceEmpId,@ComId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public Int32 EmpTransferAndRedesignationPSSaveInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", aEmpTransferAndDao.EmpTransferAndRedesignationId));

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));

            string insertQuery = @"INSERT INTO [dbo].[tblEmpTransferAndRedesignationPS]
           (EmpTransferAndRedesignationId
           ,EmpInfoId
           )
     VALUES
           (@EmpTransferAndRedesignationId
           ,@EmpInfoId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public bool DeleteDirectlyS(string id)
        {
            string query = "DELETE FROM tblEmpTransferAndRedesignationDS WHERE EmpTransferAndRedesignationId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }
        public bool DeletePS(string id)
        {
            string query = "DELETE FROM tblEmpTransferAndRedesignationPS WHERE EmpTransferAndRedesignationId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }
        public DataTable EmpTransferAndRedesignationDS(string id)
        {
            string queryStr = @"SELECT * FROM tblEmpTransferAndRedesignationDS
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=tblEmpTransferAndRedesignationDS.EmpInfoId WHERE EmpTransferAndRedesignationId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable LoadIncrementInfoApp()
        {
            string query = @"  SELECT ETR.EmpTransferAndRedesignationId, Emp.EmpMasterCode, emp.EmpName,com.CompanyName, deg.Designation, Dpt.DepartmentName,  SAL.SalaryLocation,* FROM tblEmpTransferAndRedesignation  ETR
INNER JOIN dbo.tblEmpGeneralInfo  Emp ON ETR.EmployeeId = Emp.EmpInfoId
left JOIN dbo.tblDesignation  Deg ON Emp.DesignationId = Deg.DesignationId
left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
LEFT JOIN dbo.tblCompanyInfo  Com ON  Com.CompanyId= ETR.NewCompanyId 
     INNER JOIN  dbo.tblCompanyInfo comm ON comm.CompanyId = ETR.CompanyId 
LEFT JOIN dbo.tblSalaryLocation  SAL ON ETR.NewSalaryLocationId = SAL.SalaryLoationId 
  INNER JOIN (SELECT EmpTransferAndRedesignationId,MAX(Version)MaxVer FROM dbo.tblEmpTransferAndRedesignationAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmpTransferAndRedesignationId) AS CELog ON CELog.EmpTransferAndRedesignationId= ETR.EmpTransferAndRedesignationId
								INNER JOIN dbo.tblEmpTransferAndRedesignationAppLog ON tblEmpTransferAndRedesignationAppLog.EmpTransferAndRedesignationId =ETR.EmpTransferAndRedesignationId
                                where    Version=CELog.MaxVer  and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable EmpTransferAndRedesignation(string param, string param2, string param_SP, bool isSP)
        {

            string queryStr = "";

            if (isSP)
            {
                queryStr =
              @"
SELECT distinct EmpTransferAndRedesignationId, ETR.EmployeeId, Emp.EmpMasterCode, emp.EmpName,com.CompanyName, deg.Designation, Dpt.DepartmentName,  SAL.SalaryLocation, ETR.IsOnlyTransfer,  ETR.IsInterCompanyTransfer,
ETR.Effectivedate FROM tblEmpTransferAndRedesignation  ETR  with (Nolock)
INNER JOIN dbo.tblEmpGeneralInfo  Emp ON ETR.EmployeeId = Emp.EmpInfoId
left JOIN dbo.tblDesignation  Deg ON Emp.DesignationId = Deg.DesignationId
left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
LEFT JOIN dbo.tblCompanyInfo  Com ON  Com.CompanyId= ETR.NewCompanyId 
     INNER JOIN  dbo.tblCompanyInfo comm ON comm.CompanyId = ETR.CompanyId 
	 inner JOIN   tblEmpAllRefference reff  ON Emp.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId  =tblPer.NewEmployeeId
LEFT JOIN dbo.tblSalaryLocation  SAL ON ETR.NewSalaryLocationId = SAL.SalaryLoationId   WHERE ETR.CompanyId IN ('1','2') AND (ETR.IsDelete is null or ETR.IsDelete=0)  and     reff.ShowCompany in (ComAssain)   " +
              param_SP + " ORDER BY Effectivedate DESC ";
            }

            else
            {
                queryStr = @"  select distinct * from (SELECT EmpTransferAndRedesignationId, EmployeeId, Emp.EmpMasterCode, emp.EmpName,com.CompanyName, deg.Designation, Dpt.DepartmentName,  SAL.SalaryLocation, ETR.IsOnlyTransfer,  ETR.IsInterCompanyTransfer,
ETR.Effectivedate FROM tblEmpTransferAndRedesignation  ETR  with (Nolock)
INNER JOIN dbo.tblEmpGeneralInfo  Emp ON ETR.EmployeeId = Emp.EmpInfoId
left JOIN dbo.tblDesignation  Deg ON Emp.DesignationId = Deg.DesignationId
left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
LEFT JOIN dbo.tblCompanyInfo  Com ON  Com.CompanyId= ETR.NewCompanyId 
     INNER JOIN  dbo.tblCompanyInfo comm ON comm.CompanyId = ETR.CompanyId 

LEFT JOIN dbo.tblSalaryLocation  SAL ON ETR.NewSalaryLocationId = SAL.SalaryLoationId  " + param +



@" UNION ALL SELECT  0 EmpTransferAndRedesignationId, EmployeeId, EmployeeOldID EmpMasterCode, EmployeeName EmpName, CompanyName CompanyName, Designation, Department, '' SalaryLocation, '' IsOnlyTransfer,  '' IsInterCompanyTransfer, Effectivedate  FROM tblTransferHistory  with (Nolock) where EmployeeId is not null " + param2 + " ) tbl ORDER BY Effectivedate DESC ";
            }

          
           
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable EmpCheckLatestRowTransferAndRedesignation( int par2)
        {
            string queryStr = @" SELECT TOP 1  EmpTransferAndRedesignationId, EmployeeId FROM tblEmpTransferAndRedesignation  where    EmployeeId='"+par2+"' ORDER BY EmpTransferAndRedesignationId DESC ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }



        public DataTable GetEmpTransRedDesigInformationById(string id)
        {
            string query = @"SELECT  EGSearchEmp.EmpName  SearchEmptxt, EGGNewRpt.EmpName NewEmpReportingBody, EGGAllNewRpt.EmpName OldEmpReportingBody ,   * FROM tblEmpTransferAndRedesignation
INNER JOIN dbo.tblEmpGeneralInfo EGSearchEmp ON tblEmpTransferAndRedesignation.EmployeeId= EGSearchEmp.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo EGGNewRpt ON tblEmpTransferAndRedesignation.NewEmpReportingBodyId= EGGNewRpt.EmpInfoId  
LEFT JOIN dbo.tblEmpGeneralInfo EGGAllNewRpt ON tblEmpTransferAndRedesignation.OldReportingBodyID= EGGAllNewRpt.EmpInfoId	


left JOIN dbo.tblDesignation  deg ON EGSearchEmp.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON EGSearchEmp.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON EGSearchEmp.CompanyId=Com.CompanyId
							left JOIN dbo.tblSalaryLocation  Loc ON EGSearchEmp.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON EGSearchEmp.JobLocationId=JLoc.JobLocationID
							left JOIN dbo.tblDivision  Div ON EGSearchEmp.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EGSearchEmp.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSection  Sec ON EGSearchEmp.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EGSearchEmp.SubSectionId=SubSec.SubSectionId	  WHERE  EmpTransferAndRedesignationId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEmpSpTranferById(string id)
        {
            string query = @"SELECT top 1 SpecialTransfer, RegularTransfer,FullTransfer,SalaryTransfer,ShowCompany    FROM tblEmpSpecialTransfer sptbl
inner join tblEmpAllRefference  reftbl on sptbl.NewEmployeeId=reftbl.EmployeeId
 	  WHERE  EmpTransferAndRedesignationId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
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
        public DataTable GetAppLogCommByJobId(int jobId)
        {
            string query = @" 

							 SELECT Alg.EmpTransferAndRedesignationAppLogAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, Alg.ActionStatus,
 Alg.ApproveDate, Alg.EmpTransferAndRedesignationId, Alg.Comments
  FROM tblEmpTransferAndRedesignationAppLog Alg
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and Alg.EmpTransferAndRedesignationId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEmployeePromotionEntryIdByIdApp(string id)
        {
            string query = @"SELECT  tblEmpTransferAndRedesignation.ActionStatus,  EGE.EmpInfoId as UserEmpInfoId,tblEmpTransferAndRedesignation.EmployeeId as EmpInfoId, EGSearchEmp.EmpName  SearchEmptxt, EGGNewRpt.EmpName NewEmpReportingBody, EGGAllNewRpt.EmpName OldEmpReportingBody ,   * FROM tblEmpTransferAndRedesignation
INNER JOIN dbo.tblEmpGeneralInfo EGSearchEmp ON tblEmpTransferAndRedesignation.EmployeeId= EGSearchEmp.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo EGGNewRpt ON tblEmpTransferAndRedesignation.NewEmpReportingBodyId= EGGNewRpt.EmpInfoId  
LEFT JOIN dbo.tblEmpGeneralInfo EGGAllNewRpt ON tblEmpTransferAndRedesignation.OldReportingBodyID= EGGAllNewRpt.EmpInfoId	


left JOIN dbo.tblDesignation  deg ON EGSearchEmp.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON EGSearchEmp.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON EGSearchEmp.CompanyId=Com.CompanyId
							left JOIN dbo.tblSalaryLocation  Loc ON EGSearchEmp.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON EGSearchEmp.JobLocationId=JLoc.JobLocationID
							left JOIN dbo.tblDivision  Div ON EGSearchEmp.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EGSearchEmp.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSection  Sec ON EGSearchEmp.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EGSearchEmp.SubSectionId=SubSec.SubSectionId	 
							LEFT JOIN dbo.tblUser AS U ON U.UserId = tblEmpTransferAndRedesignation.EntryBy
															INNER JOIN dbo.tblEmpGeneralInfo EGE ON EGE.EmpInfoId=U.EmpInfoId
							 WHERE  EmpTransferAndRedesignationId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetEmpSalaryGradeById(string id)
        {
            string query = @"SELECT EmpInfoId, SalaryGradeId FROM dbo.tblEmpGeneralInfo WHERE  IsActive = 1 AND EmpInfoId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public bool OnlyTransferUpsateInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
        {
           

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", aEmpTransferAndDao.EmpTransferAndRedesignationId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmpTransferAndDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmpTransferAndDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aEmpTransferAndDao.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@IsOnlyTransfer", aEmpTransferAndDao.IsOnlyTransfer ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@IsInterCompanyTransfer", aEmpTransferAndDao.IsInterCompanyTransfer ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewCompanyId", aEmpTransferAndDao.NewCompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldCompanyId", aEmpTransferAndDao.OldCompanyId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewSalaryLocationId", aEmpTransferAndDao.NewSalaryLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldSalaryLocationId", aEmpTransferAndDao.OldSalaryLocationId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewJobLocationId", aEmpTransferAndDao.NewJobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldJobLocationId", aEmpTransferAndDao.OldJobLocationId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewDivisionId", aEmpTransferAndDao.NewDivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldDivisionId", aEmpTransferAndDao.OldDivisionId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewWingId", aEmpTransferAndDao.NewWingId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldWingId", aEmpTransferAndDao.OldWingId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewDepartmentId", aEmpTransferAndDao.NewDepartmentId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldDepartmentId", aEmpTransferAndDao.OldDepartmentId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewSectionId", aEmpTransferAndDao.NewSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldSectionId", aEmpTransferAndDao.OldSectionId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewSubSectionId", aEmpTransferAndDao.NewSubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldSubSectionId", aEmpTransferAndDao.OldSubSectionId ?? (object)DBNull.Value));


            aSqlParameterlist.Add(new SqlParameter("@NewEmpReportingBodyId", aEmpTransferAndDao.NewEmpReportingBodyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OldReportingBodyID", aEmpTransferAndDao.OldReportingBodyID ?? (object)DBNull.Value));



            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aEmpTransferAndDao.EffectiveDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AutoProcess", aEmpTransferAndDao.AutoProcess ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aEmpTransferAndDao.EmpTypeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmpTransferAndDao.Remarks ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEmpTransferAndDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEmpTransferAndDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", aEmpTransferAndDao.EmployeeCode));


            string UpdateQuery = @"UPDATE [dbo].[tblEmpTransferAndRedesignation]
   SET [CompanyId]=@CompanyId
           ,[FinancialYearId]=@FinancialYearId
           ,[EmployeeId]=@EmployeeId
           ,[EmpTypeId]=@EmpTypeId
           ,[IsInterCompanyTransfer]=@IsInterCompanyTransfer
           ,[IsOnlyTransfer]=@IsOnlyTransfer
           ,[NewCompanyId]=@NewCompanyId
           ,[NewSalaryLocationId]=@NewSalaryLocationId
           ,[NewJobLocationId]=@NewJobLocationId
           ,[NewDivisionId]=@NewDivisionId
           ,[NewWingId]=@NewWingId
           ,[NewDepartmentId]=@NewDepartmentId
           ,[NewSectionId]=@NewSectionId
           ,[NewSubSectionId]=@NewSubSectionId
           ,[NewEmpReportingBodyId]=@NewEmpReportingBodyId
           ,[OldCompanyId]=@OldCompanyId
           ,[OldSalaryLocationId]=@OldSalaryLocationId
           ,[OldJobLocationId]=@OldJobLocationId
           ,[OldDivisionId]=@OldDivisionId
           ,[OldWingId]=@OldWingId
           ,[OldDepartmentId]=@OldDepartmentId
           ,[OldSectionId]=@OldSectionId
           ,[OldSubSectionId]=@OldSubSectionId
           ,[OldReportingBodyID]=@OldReportingBodyID
           ,[EffectiveDate]=@EffectiveDate
           ,[Remarks]=@Remarks
           ,[UpdateBy]=@UpdateBy
           ,[UpdateDate]=@UpdateDate
           
           ,[AutoProcess]=@AutoProcess, EmployeeCode=@EmployeeCode
 WHERE EmpTransferAndRedesignationId=@EmpTransferAndRedesignationId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
        }


 

        public bool DeleteEmpTransferAndRedesignationById(string JobId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", JobId));

            const string query = @"DELETE FROM tblEmpTransferAndRedesignation WHERE EmpTransferAndRedesignationId = @EmpTransferAndRedesignationId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }




      
        public DataTable GetEmployeeEmpTransandReDesig(string empinfoId)
        {
            string query = @"SELECT * FROM dbo.tblEmpTransferAndRedesignation WHERE EmployeeId='" + empinfoId + "' AND (AutoProcess IS NULL OR AutoProcess='0')";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        
        }
        public bool DeleteUpdateEmployeeTransferEntryById(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ID", id));

            string query = @"UPDATE dbo.tblEmpTransferAndRedesignation SET IsDelete='1',DeleteBy='" + HttpContext.Current.Session["UserId"].ToString() + "',DeleteDate='" + DateTime.Now + "' WHERE EmpTransferAndRedesignationId=@ID";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }


        public DataTable GetEmpTransferAndRedesignationInfoDALrpt(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", Id));
            const string queryStr = @"SELECT ETR.EmpTransferAndRedesignationId,  com.ShortName, EG.EmpName, EG.EmpMasterCode, deg.Designation ,
 Dpt.DepartmentName,EG.DateOfJoin, EmpTpe.EmpType, ETR.EffectiveDate, ETR.IsOnlyTransfer, ETR.IsInterCompanyTransfer,
 Newcom.ShortName NewCompany, Loc.SalaryLocation NewOffice, JLoc.Location NewPlace,
 Div.DivisionName NewDivisionName, Sec.SectionName NewSectionName, SubSec.SubSectionName NewSubSectionName, NewDpt.DepartmentName NewDepartmentName,
 EGNewReportEmp.EmpName,

 * FROM tblEmpTransferAndRedesignation ETR
		LEFT JOIN dbo.tblCompanyInfo  com ON ETR.CompanyId=com.CompanyId
left JOIN dbo.tblEmpGeneralInfo EG ON ETR.EmployeeId= EG.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
LEFT JOIN dbo.tblEmployeeType   EmpTpe ON EG.EmpTypeId=EmpTpe.EmpTypeId

LEFT JOIN dbo.tblCompanyInfo  Newcom ON ETR.CompanyId=com.CompanyId
left JOIN dbo.tblSalaryLocation  Loc ON ETR.NewSalaryLocationId=Loc.SalaryLoationId
left JOIN dbo.tblJobLocation  JLoc ON ETR.NewJobLocationId=JLoc.JobLocationID


	LEFT JOIN dbo.tblDivision  Div ON ETR.NewDevisionId=Div.DivisionId
	LEFT JOIN dbo.tblDepartment  NewDpt ON ETR.NewDepartmentId=NewDpt.DepartmentId
 
 
 LEFT JOIN dbo.tblSection  Sec ON ETR.NewSectionId=Sec.SectionId
 LEFT JOIN dbo.tblSubSection  SubSec ON ETR.NewSubSectionId=SubSec.SubSectionId	
 
left JOIN dbo.tblEmpGeneralInfo EGNewReportEmp ON ETR.NewEmpReportingBodyId= EGNewReportEmp.EmpInfoId

WHERE ETR.EmpTransferAndRedesignationId=@EmpTransferAndRedesignationId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetDepartmentRelaton(string id, string param)
        {
            string queryStr = @"SELECT tblDivisionWing.Invisible,* FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND DepartmentId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetSectionRelaton(string id, string param)
        {
            string queryStr = @"SELECT tblDepartment.Invisible,* FROM dbo.tblSection
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE dbo.tblSection.IsActive = 'True' AND SectionId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetSubSectionRelaton(string id, string param)
        {
            string queryStr = @"SELECT dbo.tblSection.Invisible,* FROM dbo.tblSubSection
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE dbo.tblSubSection.IsActive = 'True' AND SubSectionId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public void GetDivisionWingListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetSubSectionListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT * FROM dbo.tblSubSection
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblSubSection.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "SubSectionName", "SubSectionId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetDepartmentByDivListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetDepartmentByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetSectionByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT * FROM dbo.tblSection
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblSection.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblSection.Invisible IS NULL OR tblSection.Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetSectionByDivListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT * FROM dbo.tblSection
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblSection.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionList(DropDownList ddl, string companyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            string queryStr = "SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True' AND CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionWingList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDepartmentList(DropDownList ddl, string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            string queryStr = "SELECT DepartmentId,DepartmentName FROM tblDepartment WHERE IsActive = 'True' AND DivisionWId = @wingId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetSectionList(DropDownList ddl, string departmentId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@departmentId", departmentId));

            string queryStr = "SELECT SectionId,SectionName FROM tblSection WHERE IsActive = 'True' AND DepartmentId = @departmentId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEmployeeMasterInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@NewCompanyId", aEmpTransferAndDao.NewCompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewSalaryLocationId", aEmpTransferAndDao.NewSalaryLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewJobLocationId", aEmpTransferAndDao.NewJobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewDivisionId", aEmpTransferAndDao.NewDivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewWingId", aEmpTransferAndDao.NewWingId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewDepartmentId", aEmpTransferAndDao.NewDepartmentId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewSectionId", aEmpTransferAndDao.NewSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewSubSectionId", aEmpTransferAndDao.NewSubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewEmpReportingBodyId", aEmpTransferAndDao.NewEmpReportingBodyId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aEmpTransferAndDao.EffectiveDate ?? (object)DBNull.Value));


           


            aSqlParameterlist.Add(new SqlParameter("@Updateby", HttpContext.Current.Session["UserId"].ToString() ?? (object)DBNull.Value));

           




            string UpdateQuery = @"
--tblEmpGeneralInfo
INSERT INTO [dbo].[tblEmpGeneralInfo]  (  
       [ReportingEmpId]
      ,[EmpMasterCode]
      ,[CompanyId]
      ,[DivisionId]
      ,[DivisionWId]
      ,[DepartmentId]
      ,[SectionId]
      ,[SubSectionId]
      ,[EmpCategoryId]
      ,[SalaryGradeId]
      ,[SalaryStepId]
      ,[DesignationId]
      ,[DesignationTypeId]
      ,[SalaryLoationId]
      ,[JobLocationId]
      ,[EmpName]
      ,[ShortName]
      ,[Gender]
      ,[BloodGroup]
      ,[TinNo]
      ,[FatherName]
      ,[FatherOccupation]
      ,[MotherName]
      ,[MotherOccupation]
      ,[DateOfBirth]
      ,[DateOfJoin]
      ,[Religion]
      ,[MaritalStatus]
      ,[EmpTypeId]
      ,[ProjectID]
      ,[SalaryFromProject]
      ,[IsProbationary]
      ,[ProbationEndDate]
      ,[ExtProbationPeriodDate]
      ,[ContractEndDate]
      ,[ExtContractDate]
      ,[Nationality]
      ,[NationalityID]
      ,[PassportNo]
      ,[ExpectedServiceLength]
      ,[DateOfRetirement]
      ,[DateOfConformation]
      ,[PlaceOfBirth]
      ,[AddressPresent]
      ,[PresentDivision]
      ,[PresentDistrict]
      ,[PresentThana]
      ,[PresentTelNo]
      ,[AddressPermanent]
      ,[ParmanentDivision]
      ,[PermanentDistrict]
      ,[PermanentThana]
      ,[ParmanentTelNo]
      ,[PersonalEmail]
      ,[OfficialEmail]
      ,[PersonalMobile]
      ,[OfficialMobile]
      ,[FaxNo]
      ,[EmergencyContactPerson]
      ,[EmergencyContactAddress]
      ,[EmergencyContactNumber]
      ,[SpouseName]
      ,[SpouseDateOfBirth]
      ,[SpouseMaxEducation]
      ,[SpouseOccupation]
      ,[DateOfMarriage]
      ,[DateOfConfirmation]
      ,[ConformationStatus]
      ,[Age]
      ,[NAge]
      ,[IsActive]
      ,[ActionStatus]
      ,[MedicalInformation]
      ,[JobID]
      ,[CandidateID]
      ,[Remarks]
      ,[EntryBy]
      ,[EntryDate]
      ,[Updateby]
      ,[UpdateDate]
      ,[EmployeeStatus]
      ,[InactiveReason]
      ,[ApprovalDate]
      ,[IsDeleted]
      ,[DeleteBy]
      ,[DeleteDate]
      ,[EmpImage]
      ,[EmpSign]
      ,[PayType]
      ,[SMCOldCode]
      ,[NationalIdNo]
      ,[IsProgramContractual]
      ,[NomineeImage]
      ,[ContractPeriod]
      ,[Floor],[ReferenceID], ContractStartDate )

 

SELECT  ISNULL(@NewEmpReportingBodyId,NULL)
      ,NULL
      ,@NewCompanyId
      ,ISNULL(@NewDivisionId,NULL)
      ,ISNULL(@NewWingId, NULL)
      ,ISNULL(@NewDepartmentId, NULL)
      ,ISNULL(@NewSectionId, NULL)
      ,ISNULL(@NewSubSectionId, NULL)
      ,[EmpCategoryId]
      ,[SalaryGradeId]
      ,[SalaryStepId]
      ,[DesignationId]
      ,[DesignationTypeId]
      ,ISNULL(@NewSalaryLocationId, NULL)
      ,ISNULL(@NewJobLocationId,NULL)
      ,[EmpName]
      ,[ShortName]
      ,[Gender]
      ,[BloodGroup]
      ,[TinNo]
      ,[FatherName]
      ,[FatherOccupation]
      ,[MotherName]
      ,[MotherOccupation]
      ,[DateOfBirth]
      ,DateOfJoin
      ,[Religion]
      ,[MaritalStatus]
      ,EmpTypeId
      ,[ProjectID]
      ,[SalaryFromProject]
      ,[IsProbationary]
      ,[ProbationEndDate]
      ,[ExtProbationPeriodDate]
      ,ContractEndDate
      ,[ExtContractDate]
      ,[Nationality]
      ,[NationalityID]
      ,[PassportNo]
      ,[ExpectedServiceLength]
      ,[DateOfRetirement]
      ,DateOfConformation
      ,[PlaceOfBirth]
      ,[AddressPresent]
      ,[PresentDivision]
      ,[PresentDistrict]
      ,[PresentThana]
      ,[PresentTelNo]
      ,[AddressPermanent]
      ,[ParmanentDivision]
      ,[PermanentDistrict]
      ,[PermanentThana]
      ,[ParmanentTelNo]
      ,[PersonalEmail]
      ,[OfficialEmail]
      ,[PersonalMobile]
      ,[OfficialMobile]
      ,[FaxNo]
      ,[EmergencyContactPerson]
      ,[EmergencyContactAddress]
      ,[EmergencyContactNumber]
      ,[SpouseName]
      ,[SpouseDateOfBirth]
      ,[SpouseMaxEducation]
      ,[SpouseOccupation]
      ,[DateOfMarriage]
      ,DateOfConfirmation
      ,[ConformationStatus]
      ,[Age]
      ,[NAge]
      ,[IsActive]
      ,[ActionStatus]
      ,[MedicalInformation]
      ,[JobID]
      ,[CandidateID]
      ,[Remarks]
      ,@Updateby
      ,GETDATE()
      ,null
      ,null
      ,[EmployeeStatus]
      ,[InactiveReason]
      ,[ApprovalDate]
      ,[IsDeleted]
      ,[DeleteBy]
      ,[DeleteDate]
      ,[EmpImage]
      ,[EmpSign]
      ,[PayType]
      ,[SMCOldCode]
      ,[NationalIdNo]
      ,[IsProgramContractual]
      ,[NomineeImage]
      ,ContractPeriod
      ,[Floor],@EmpInfoId,  ContractStartDate
  FROM [dbo].[tblEmpGeneralInfo] WHERE EmpInfoId=@EmpInfoId

UPDATE tblEmpGeneralInfo SET IsActive=0, EmployeeStatus='InActive', InactiveReason='Company To Company Transfer', Updateby=@Updateby, UpdateDate=GETDATE()  WHERE EmpInfoId=@EmpInfoId
-- Get Max EmpInfoId From  tblEmpGeneralInfo
DECLARE @EmpMaxId BIGINT =NULL
SELECT @EmpMaxId=   MAX(EmpInfoId) FROM dbo.tblEmpGeneralInfo 


DECLARE @EmpMasterCode NVARCHAR(max)
select @EmpMasterCode=EmpMasterCode FROM [dbo].[tblEmpGeneralInfo] WHERE EmpInfoId=@EmpInfoId
 UPDATE tblEmpGeneralInfo SET  SMCOldCode=@EmpMasterCode  WHERE EmpInfoId=@EmpMaxId



-- tblEmpChildren
INSERT INTO [dbo].[tblEmpChildren]
           ([EmpInfoId]
           ,[ChildrenName]
           ,[ChildrenGender]
           ,[ChildrenOccupation]
           ,[ChildrenDOB]
           ,[ChildrenMaritalStatus]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[ChildrenName]
      ,[ChildrenGender]
      ,[ChildrenOccupation]
      ,[ChildrenDOB]
      ,[ChildrenMaritalStatus]
      ,[IsActive]
  FROM [dbo].[tblEmpChildren]  WHERE EmpInfoId=@EmpInfoId


-- tblEmpEducation
INSERT INTO [dbo].[tblEmpEducation]
           ([EmpInfoId]
           ,[EducationNameId]
           ,[SubjectGroupId]
           ,[BoardUniversityId]
           ,[Result]
           ,[EducationalInstituteId]
           ,[PassingYear]
           ,[CgpaOrTotalMarks]
           ,[FieldOfSpecializationId]
           ,[EduIsLastLevel]
           ,[IsActive]
           ,[IsProfessionalEdu])

		   SELECT  @EmpMaxId
      ,[EducationNameId]
           ,[SubjectGroupId]
           ,[BoardUniversityId]
           ,[Result]
           ,[EducationalInstituteId]
           ,[PassingYear]
           ,[CgpaOrTotalMarks]
           ,[FieldOfSpecializationId]
           ,[EduIsLastLevel]
           ,[IsActive]
           ,[IsProfessionalEdu]
  FROM [dbo].[tblEmpEducation]  WHERE EmpInfoId=@EmpInfoId



--tblEmpExperience

INSERT INTO [dbo].[tblEmpExperience]
           ([EmpInfoId]
           ,[ExpContactPerson]
           ,[ExpCompany]
           ,[ExpAddress]
           ,[ExpTelNo]
           ,[ExpNatureofBusiness]
           ,[ExpDesignation]
           ,[ExpJobDescription]
           ,[ExpFromDate]
           ,[ExpToDate]
           ,[ExpJobType]
           ,[ExpLastJob]
           ,[ExpRemarks]
           ,[ExpLeavingSalary]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[ExpContactPerson]
           ,[ExpCompany]
           ,[ExpAddress]
           ,[ExpTelNo]
           ,[ExpNatureofBusiness]
           ,[ExpDesignation]
           ,[ExpJobDescription]
           ,[ExpFromDate]
           ,[ExpToDate]
           ,[ExpJobType]
           ,[ExpLastJob]
           ,[ExpRemarks]
           ,[ExpLeavingSalary]
           ,[IsActive]
  FROM [dbo].[tblEmpExperience]  WHERE EmpInfoId=@EmpInfoId



--tblEmpTraining

INSERT INTO [dbo].[tblEmpTraining]
           ([EmpInfoId]
           ,[TrainingName]
           ,[TrainingType]
           ,[TrainingDescription]
           ,[TrainingInstitution]
           ,[TrainingPlace]
           ,[TrainingCountry]
           ,[TrainingAchievment]
           ,[TrFromDate]
           ,[TrToDate]
           ,[TrainingDays]
           ,[TrRemarks]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[TrainingName]
           ,[TrainingType]
           ,[TrainingDescription]
           ,[TrainingInstitution]
           ,[TrainingPlace]
           ,[TrainingCountry]
           ,[TrainingAchievment]
           ,[TrFromDate]
           ,[TrToDate]
           ,[TrainingDays]
           ,[TrRemarks]
           ,[IsActive]
  FROM [dbo].[tblEmpTraining]  WHERE EmpInfoId=@EmpInfoId



--tblEmpReference

INSERT INTO [dbo].[tblEmpReference]
           ([EmpInfoId]
           ,[ReferenceName]
           ,[RefOccupation]
           ,[RefAddress]
           ,[RefMobile]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[ReferenceName]
           ,[RefOccupation]
           ,[RefAddress]
           ,[RefMobile]
           ,[IsActive]
  FROM [dbo].[tblEmpReference]  WHERE EmpInfoId=@EmpInfoId


--tblEmpNominee

INSERT INTO [dbo].[tblEmpNominee]
           ([EmpInfoId]
           ,[NominationPurpose]
           ,[NomineeName]
           ,[NomineeOccupation]
           ,[DateOfNomination]
           ,[NominationPercentage]
           ,[NomineeDOB]
           ,[NomineeRelation]
           ,[NomineeAddress]
           ,[NomineeTelephone]
           ,[IsActive]
           ,[NomNomineImg])

		   SELECT  @EmpMaxId
       ,[NominationPurpose]
           ,[NomineeName]
           ,[NomineeOccupation]
           ,[DateOfNomination]
           ,[NominationPercentage]
           ,[NomineeDOB]
           ,[NomineeRelation]
           ,[NomineeAddress]
           ,[NomineeTelephone]
           ,[IsActive]
           ,[NomNomineImg]
  FROM [dbo].[tblEmpNominee]  WHERE EmpInfoId=@EmpInfoId


--tblEmpExtraCurriculam

INSERT INTO [dbo].[tblEmpExtraCurriculam]
           ([EmpInfoId]
           ,[MasterExtraCurriculamId]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[MasterExtraCurriculamId]
           ,[IsActive]
  FROM [dbo].[tblEmpExtraCurriculam]  WHERE EmpInfoId=@EmpInfoId

--tblEmpOtherTalents

INSERT INTO [dbo].[tblEmpOtherTalents]
           ([EmpInfoId]
           ,[MasterOtherTalentsId]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[MasterOtherTalentsId]
           ,[IsActive]
  FROM [dbo].[tblEmpOtherTalents]  WHERE EmpInfoId=@EmpInfoId


--tblEmpAchievements

INSERT INTO [dbo].[tblEmpAchievements]
           ([EmpInfoId]
           ,[MasterAchievementsId]
           ,[IsActive])

		   SELECT  @EmpMaxId
        ,[MasterAchievementsId]
           ,[IsActive]
  FROM [dbo].[tblEmpAchievements]  WHERE EmpInfoId=@EmpInfoId

--tblEmpHobby

INSERT INTO [dbo].[tblEmpHobby]
           ([EmpInfoId]
           ,[MasterHobbyId]
           ,[IsActive])

		   SELECT  @EmpMaxId
         ,[MasterHobbyId]
           ,[IsActive]
  FROM [dbo].[tblEmpHobby]  WHERE EmpInfoId=@EmpInfoId




";

            return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
        }



        public bool UpdateEmployeeMasterInfoforInterCom(EmpTransferAndRedesignationDao aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));

            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aEmpTransferAndDao.NewSalaryLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aEmpTransferAndDao.NewJobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aEmpTransferAndDao.NewDivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aEmpTransferAndDao.NewWingId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aEmpTransferAndDao.NewDepartmentId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aEmpTransferAndDao.NewSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aEmpTransferAndDao.NewSubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ReportingEmpId", aEmpTransferAndDao.NewEmpReportingBodyId ?? (object)DBNull.Value));

      





            aSqlParameterlist.Add(new SqlParameter("@Updateby", HttpContext.Current.Session["UserId"].ToString() ?? (object)DBNull.Value));






            string UpdateQuery = @"UPDATE [dbo].[tblEmpGeneralInfo]
   SET ReportingEmpId =@ReportingEmpId,
   SalaryLoationId=@SalaryLoationId,
   JobLocationId=@JobLocationId,
   DivisionId=@DivisionId,
   DivisionWId=@DivisionWId,
   DepartmentId=@DepartmentId,
   SectionId=@SectionId,
   SubSectionId=@SubSectionId,
  Updateby=@Updateby, UpdateDate=GETDATE() where EmpInfoId=@EmpInfoId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEmployeeSuperVisorId(EmpGeneralInfo aInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", (object)aInfo.EmpInfoId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RptBodyId", (object)aInfo.LineId ?? DBNull.Value));

            string query = @"UPDATE tblEmpGeneralInfo SET ReportingEmpId = @RptBodyId WHERE EmpInfoId =  @EmpInfoId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
        }

        public bool UpdateEmployeeSuperVisorIdToNull(int empId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            EmpGeneralInfo aInfo = new EmpGeneralInfo();

            aInfo.EmpInfoId = empId;

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@RptBodyId", aInfo.LineId ?? (object)DBNull.Value));

            string query = @"UPDATE tblEmpGeneralInfo SET ReportingEmpId = @RptBodyId WHERE EmpInfoId =  @EmpInfoId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable EmpTransferAndRedesignationPS(string value)
        {
            string query = @"SELECT * FROM dbo.tblEmpTransferAndRedesignationPS WHERE EmpTransferAndRedesignationId ='" + value + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEmployeeReportingBodyInfo(int value)
        {
            string query = @"SELECT ReportingEmpId FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId ='" + value + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
    }
}
