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

namespace DAL.MasterSetup_DAL
{
    public class EmployeeRequsitionDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

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
        public bool UpdateContractural(string actionstatus, int id)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@JobReqId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", actionstatus));


                    string query =
                        @"update tblJobReqForm set ActionStatus=@ActionStatus  where JobReqId = @JobReqId";

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

        public DataTable GetDataReviewEntryBy(string id, string entryby, string actionstatu)
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


            string queryStr = @"SELECT * FROM dbo.tblJobReqForm WHERE ActionStatus='" + actionstatu + "' AND EntryBy='" + entryby + "' AND JobReqId='" + id + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public bool UpdateJobReqStatus2(EmployeeRequsitionDAO aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.JobReqId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@JobReqId", aMaster.JobReqId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblJobReqForm set ActionStatus2=@ActionStatus  where JobReqId = @JobReqId";

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
        public DataTable LoadInformationAppALl(string param)
        {
            string queryStr = @"SELECT  EPE.JobReqId,  Com.CompanyName,  EPE.JobTitle, EPE.ReqDate, EPE.Nos,FINY.FinancialYearDesc, Dpt.DepartmentName, EPE.ActionStatus,* From dbo.tblJobReqForm EPE
 
 left JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 LEFT JOIN dbo.tblFinancialYear AS FINY ON EPE.FinYearId = FINY.FinancialYearId
  LEFT JOIN dbo.tblDepartment AS Dpt ON EPE.DeptId = Dpt.DepartmentId 
INNER JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY JobReqId) AS CELog ON CELog.JobReqId= EPE.JobReqId
							INNER JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = EPE.JobReqId
                                where Version=CELog.MaxVer   and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public void EmployeeNameDropDown(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblEmpGeneralInfo WHERE CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", query, "HRDB");

        }
        public void GetCompanyListShortNameIntoDropdown(DropDownList ddl)
        {

            string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

        public Int32 SaveEmpReq(EmployeeRequsitionDAO aEmployeeRequsitionDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeeRequsitionDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@JobReqId", aEmployeeRequsitionDao.JobReqId));
            aSqlParameterlist.Add(new SqlParameter("@JobTitleId", aEmployeeRequsitionDao.JobTitleId));
            aSqlParameterlist.Add(new SqlParameter("@GradeId", aEmployeeRequsitionDao.GradeId));
            aSqlParameterlist.Add(new SqlParameter("@Nos", aEmployeeRequsitionDao.Nos));
            aSqlParameterlist.Add(new SqlParameter("@Note", aEmployeeRequsitionDao.Note));
            aSqlParameterlist.Add(new SqlParameter("@ReqCode", aEmployeeRequsitionDao.ReqCode));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeeRequsitionDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@PlaceOffice", aEmployeeRequsitionDao.PlaceOffice));



            //aSqlParameterlist.Add(new SqlParameter("@Others", aEmployeeRequsitionDao.Others));
            aSqlParameterlist.Add(new SqlParameter("@PlaceOfPosting", aEmployeeRequsitionDao.PlaceOfPosting));
            aSqlParameterlist.Add(new SqlParameter("@ReportingTo", aEmployeeRequsitionDao.ReportingTo));
            //aSqlParameterlist.Add(new SqlParameter("@DivisionId", aEmployeeRequsitionDao.DivisionId));
            //aSqlParameterlist.Add(new SqlParameter("@WingId", aEmployeeRequsitionDao.WingId));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aEmployeeRequsitionDao.DeptId));
            aSqlParameterlist.Add(new SqlParameter("@ReplaceEmpId", aEmployeeRequsitionDao.ReplaceEmpId));
            aSqlParameterlist.Add(new SqlParameter("@SeparationDate", aEmployeeRequsitionDao.SeparationDate));
            aSqlParameterlist.Add(new SqlParameter("@Experience", aEmployeeRequsitionDao.Experience));
            aSqlParameterlist.Add(new SqlParameter("@Skills", aEmployeeRequsitionDao.Skills));
            aSqlParameterlist.Add(new SqlParameter("@Age", aEmployeeRequsitionDao.Age));
            aSqlParameterlist.Add(new SqlParameter("@OtherExperience", aEmployeeRequsitionDao.OtherExperience));
            aSqlParameterlist.Add(new SqlParameter("@IsInternalCir", aEmployeeRequsitionDao.IsInternalCir));
            aSqlParameterlist.Add(new SqlParameter("@IsOnlineCir", aEmployeeRequsitionDao.IsOnlineCir));
            aSqlParameterlist.Add(new SqlParameter("@IsSMCWeb", aEmployeeRequsitionDao.IsSMCWeb));
            aSqlParameterlist.Add(new SqlParameter("@IsNewsPaper", aEmployeeRequsitionDao.IsNewsPaper));
            aSqlParameterlist.Add(new SqlParameter("@IsHeadHuntFirm", aEmployeeRequsitionDao.IsHeadHuntFirm));
            aSqlParameterlist.Add(new SqlParameter("@OtherCircula", aEmployeeRequsitionDao.OtherCircula));
            aSqlParameterlist.Add(new SqlParameter("@IsOtherCircula", aEmployeeRequsitionDao.IsOtherCircula));
            aSqlParameterlist.Add(new SqlParameter("@IsReplacement", aEmployeeRequsitionDao.IsReplacement));
            aSqlParameterlist.Add(new SqlParameter("@IsBudgeted", aEmployeeRequsitionDao.IsBudgeted));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aEmployeeRequsitionDao.EmpTypeId));
       
            // aSqlParameterlist.Add(new SqlParameter("@ExpDateOfJoining", aEmployeeRequsitionDao.ExpDateOfJoining));
            try
            {
                aSqlParameterlist.Add(new SqlParameter("@ExpDateOfJoining", aEmployeeRequsitionDao.ExpDateOfJoining ?? (object)DBNull.Value));
            }
            catch (Exception)
            {

                //throw;
            }



            aSqlParameterlist.Add(new SqlParameter("@BudgetId", aEmployeeRequsitionDao.BudgetId));
          
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmployeeRequsitionDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@ReqDate", aEmployeeRequsitionDao.ReqDate));
            aSqlParameterlist.Add(new SqlParameter("@FinYearId", aEmployeeRequsitionDao.FinYearId));


            //New
            aSqlParameterlist.Add(new SqlParameter("@JobSummery", aEmployeeRequsitionDao.JobSummery));
            aSqlParameterlist.Add(new SqlParameter("@JobTitle", aEmployeeRequsitionDao.JobTitle));

            aSqlParameterlist.Add(new SqlParameter("@ProjectId", aEmployeeRequsitionDao.ProjectId));
            aSqlParameterlist.Add(new SqlParameter("@SupervisorId", aEmployeeRequsitionDao.SupervisorId));
            aSqlParameterlist.Add(new SqlParameter("@InternalContact", aEmployeeRequsitionDao.InternalContact));

            aSqlParameterlist.Add(new SqlParameter("@ExternalContact", aEmployeeRequsitionDao.ExternalContact));
            aSqlParameterlist.Add(new SqlParameter("@OfficeId", aEmployeeRequsitionDao.OfficeId));
            aSqlParameterlist.Add(new SqlParameter("@PlaceId", aEmployeeRequsitionDao.PlaceId));

            aSqlParameterlist.Add(new SqlParameter("@ProfCertification", aEmployeeRequsitionDao.ProfCertification));
            aSqlParameterlist.Add(new SqlParameter("@CmpSkill", aEmployeeRequsitionDao.CmpSkill));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aEmployeeRequsitionDao.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmployeeRequsitionDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmployeeRequsitionDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@Justification", aEmployeeRequsitionDao.Justification));

            aSqlParameterlist.Add(new SqlParameter("@MonthContractual", aEmployeeRequsitionDao.MonthContractual));
            aSqlParameterlist.Add(new SqlParameter("@FundContractual", aEmployeeRequsitionDao.FundContractual));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCategoryId", aEmployeeRequsitionDao.EmployeeCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@IsManagementApproved", aEmployeeRequsitionDao.IsManagementApproved));

            string insertQuery = @"INSERT INTO dbo.tblJobReqForm
                                    ( 
                                      JobTitleId ,
                                      CompanyId,
                                      GradeId ,
                                      Nos ,
                                    Note,
                                 ReqCode,
                                    EmployeeId,
                                    OtherExperience,
                                  
                                      PlaceOfPosting ,
                                      ReportingTo ,

                                      DeptId ,
                                      ReplaceEmpId ,
                                      SeparationDate ,
                                      Experience ,
                                      Skills ,
                                      Age ,
                                   
                                      IsInternalCir ,
                                      IsOnlineCir ,
                                      IsSMCWeb ,
                                      IsNewsPaper ,
                                      IsHeadHuntFirm ,
                                      OtherCircula ,IsOtherCircula,
                                      ExpDateOfJoining,IsReplacement,IsBudgeted, BudgetId,  Remarks, EmpTypeId,ReqDate,FinYearId,JobSummery,
	JobTitle,
	ProjectId,

	SupervisorId,

	OfficeId,
	PlaceId,

	InternalContact,
	ExternalContact,
	ProfCertification,
	CmpSkill,
ActionStatus, EntryBy, EntryDate, Justification, MonthContractual, FundContractual, EmployeeCategoryId, IsManagementApproved, PlaceOffice
                                    )
                            VALUES  ( 
                                      @JobTitleId ,
                                      @CompanyId,
                                      @GradeId ,
                                      @Nos ,
                                 @Note,
                                   @ReqCode,
@EmployeeId,
                                   @OtherExperience,
                                 
                                      @PlaceOfPosting ,
                                      @ReportingTo ,
                                   
                                    
                                      @DeptId ,
                                      @ReplaceEmpId ,
                                      @SeparationDate ,
                                      @Experience ,
                                      @Skills ,
                                      @Age ,
                                    
                                      @IsInternalCir ,
                                      @IsOnlineCir ,
                                      @IsSMCWeb ,
                                      @IsNewsPaper ,
                                      @IsHeadHuntFirm ,
                                      @OtherCircula ,@IsOtherCircula,
                                      @ExpDateOfJoining,@IsReplacement,@IsBudgeted, @BudgetId,  @Remarks, @EmpTypeId,@ReqDate,@FinYearId,    
    @JobSummery,
	@JobTitle,
	@ProjectId,
	@SupervisorId,
	@OfficeId,
	@PlaceId,
	@InternalContact,
	@ExternalContact,
	@ProfCertification,
	@CmpSkill,
@ActionStatus, @EntryBy, @EntryDate, @Justification, @MonthContractual, @FundContractual, @EmployeeCategoryId, @IsManagementApproved, @PlaceOffice
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public bool UpdateEmpReq(EmployeeRequsitionDAO aEmployeeRequsitionDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            
            aSqlParameterlist.Add(new SqlParameter("@JobReqId", aEmployeeRequsitionDao.JobReqId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeeRequsitionDao.CompanyId));

            aSqlParameterlist.Add(new SqlParameter("@JobTitleId", aEmployeeRequsitionDao.JobTitleId));
            aSqlParameterlist.Add(new SqlParameter("@GradeId", aEmployeeRequsitionDao.GradeId));
            aSqlParameterlist.Add(new SqlParameter("@Nos", aEmployeeRequsitionDao.Nos));
            aSqlParameterlist.Add(new SqlParameter("@Note", aEmployeeRequsitionDao.Note));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeeRequsitionDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@PlaceOffice", aEmployeeRequsitionDao.PlaceOffice));



            //aSqlParameterlist.Add(new SqlParameter("@Others", aEmployeeRequsitionDao.Others));
            aSqlParameterlist.Add(new SqlParameter("@PlaceOfPosting", aEmployeeRequsitionDao.PlaceOfPosting));
            aSqlParameterlist.Add(new SqlParameter("@ReportingTo", aEmployeeRequsitionDao.ReportingTo));
            //aSqlParameterlist.Add(new SqlParameter("@DivisionId", aEmployeeRequsitionDao.DivisionId));
            //aSqlParameterlist.Add(new SqlParameter("@WingId", aEmployeeRequsitionDao.WingId));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aEmployeeRequsitionDao.DeptId));
            aSqlParameterlist.Add(new SqlParameter("@ReplaceEmpId", aEmployeeRequsitionDao.ReplaceEmpId));
            aSqlParameterlist.Add(new SqlParameter("@SeparationDate", aEmployeeRequsitionDao.SeparationDate));
            aSqlParameterlist.Add(new SqlParameter("@Experience", aEmployeeRequsitionDao.Experience));
            aSqlParameterlist.Add(new SqlParameter("@Skills", aEmployeeRequsitionDao.Skills));
            aSqlParameterlist.Add(new SqlParameter("@Age", aEmployeeRequsitionDao.Age));
            aSqlParameterlist.Add(new SqlParameter("@OtherExperience", aEmployeeRequsitionDao.OtherExperience));
            aSqlParameterlist.Add(new SqlParameter("@IsInternalCir", aEmployeeRequsitionDao.IsInternalCir));
            aSqlParameterlist.Add(new SqlParameter("@IsOnlineCir", aEmployeeRequsitionDao.IsOnlineCir));
            aSqlParameterlist.Add(new SqlParameter("@IsSMCWeb", aEmployeeRequsitionDao.IsSMCWeb));
            aSqlParameterlist.Add(new SqlParameter("@IsNewsPaper", aEmployeeRequsitionDao.IsNewsPaper));
            aSqlParameterlist.Add(new SqlParameter("@IsHeadHuntFirm", aEmployeeRequsitionDao.IsHeadHuntFirm));
            aSqlParameterlist.Add(new SqlParameter("@OtherCircula", aEmployeeRequsitionDao.OtherCircula));
            aSqlParameterlist.Add(new SqlParameter("@IsOtherCircula", aEmployeeRequsitionDao.IsOtherCircula));
            aSqlParameterlist.Add(new SqlParameter("@IsReplacement", aEmployeeRequsitionDao.IsReplacement));
            aSqlParameterlist.Add(new SqlParameter("@IsBudgeted", aEmployeeRequsitionDao.IsBudgeted));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aEmployeeRequsitionDao.EmpTypeId));

            
            aSqlParameterlist.Add(new SqlParameter("@BudgetId", aEmployeeRequsitionDao.BudgetId));

            aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmployeeRequsitionDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@ReqDate", aEmployeeRequsitionDao.ReqDate));
            aSqlParameterlist.Add(new SqlParameter("@FinYearId", aEmployeeRequsitionDao.FinYearId));



            aSqlParameterlist.Add(new SqlParameter("@SupervisorId", aEmployeeRequsitionDao.SupervisorId));
            aSqlParameterlist.Add(new SqlParameter("@InternalContact", aEmployeeRequsitionDao.InternalContact));

            aSqlParameterlist.Add(new SqlParameter("@ExternalContact", aEmployeeRequsitionDao.ExternalContact));
            aSqlParameterlist.Add(new SqlParameter("@OfficeId", aEmployeeRequsitionDao.OfficeId));
            aSqlParameterlist.Add(new SqlParameter("@PlaceId", aEmployeeRequsitionDao.PlaceId));

            aSqlParameterlist.Add(new SqlParameter("@ProfCertification", aEmployeeRequsitionDao.ProfCertification));
            aSqlParameterlist.Add(new SqlParameter("@CmpSkill", aEmployeeRequsitionDao.CmpSkill));

            aSqlParameterlist.Add(new SqlParameter("@ProjectId", aEmployeeRequsitionDao.ProjectId));

            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEmployeeRequsitionDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEmployeeRequsitionDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@Justification", aEmployeeRequsitionDao.Justification));
            try
            {
                aSqlParameterlist.Add(new SqlParameter("@ExpDateOfJoining", aEmployeeRequsitionDao.ExpDateOfJoining ?? (object)DBNull.Value));
            }
            catch (Exception)
            {

                //throw;
            }

            aSqlParameterlist.Add(new SqlParameter("@MonthContractual", aEmployeeRequsitionDao.MonthContractual));
            aSqlParameterlist.Add(new SqlParameter("@FundContractual", aEmployeeRequsitionDao.FundContractual));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCategoryId", aEmployeeRequsitionDao.EmployeeCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@IsManagementApproved", aEmployeeRequsitionDao.IsManagementApproved));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aEmployeeRequsitionDao.ActionStatus));

            string Query = @"Update dbo.tblJobReqForm
                                    set   Justification=@Justification,
                                       JobTitleId=@JobTitleId ,
                                      CompanyId=@CompanyId,
                                      GradeId =@GradeId,
                                      Nos =@Nos,
                                    Note=@Note,
EmployeeId=@EmployeeId,
                                 
                                    OtherExperience=@OtherExperience,
                                  
                                      PlaceOfPosting=@PlaceOfPosting,
                                      ReportingTo=@ReportingTo ,
                                 
                                      DeptId =@DeptId,
                                      ReplaceEmpId=@ReplaceEmpId ,
                                      SeparationDate=@SeparationDate ,
                                      Experience=@Experience ,
                                      Skills=@Skills ,
                                      Age =@Age,
                                   
                                      IsInternalCir=@IsInternalCir ,
                                      IsOnlineCir =@IsOnlineCir,
                                      IsSMCWeb =@IsSMCWeb,
                                      IsNewsPaper=@IsNewsPaper ,
                                      IsHeadHuntFirm=@IsHeadHuntFirm ,
                                      OtherCircula =@OtherCircula,IsOtherCircula=@IsOtherCircula,
                                      ExpDateOfJoining=@ExpDateOfJoining,IsReplacement=@IsReplacement,IsBudgeted=@IsBudgeted, BudgetId=@BudgetId,  Remarks=@Remarks, EmpTypeId=@EmpTypeId,ReqDate=@ReqDate
                                        ,FinYearId=@FinYearId, SupervisorId = @SupervisorId,
	OfficeId = @OfficeId,
	PlaceId = @PlaceId,
	InternalContact = @InternalContact,
	ExternalContact = @ExternalContact,
	ProfCertification = @ProfCertification,
	CmpSkill = @CmpSkill,
    ProjectId = @ProjectId, UpdateBy=@UpdateBy, UpdateDate=@UpdateDate, MonthContractual=@MonthContractual, FundContractual=@FundContractual, EmployeeCategoryId=@EmployeeCategoryId, IsManagementApproved=@IsManagementApproved, ActionStatus=@ActionStatus, PlaceOffice=@PlaceOffice
                                    WHERE JobReqId=@JobReqId ";

            return aCommonInternalDal.UpdateDataByUpdateCommand(Query, aSqlParameterlist, "HRDB");
        }

        public bool UpdateActionStatus(int id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobReqId", id));

            string Query = @"Update dbo.tblJobReqForm
                                    set   ActionStatus='Submitted'  WHERE JobReqId=@JobReqId ";
            return aCommonInternalDal.UpdateDataByUpdateCommand(Query, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEmpReqById(EmployeeRequsitionDAO aEmployeeRequsitionDao)
        {  
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobReqId", aEmployeeRequsitionDao.JobReqId));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aEmployeeRequsitionDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aEmployeeRequsitionDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aEmployeeRequsitionDao.DeleteDate));


            const string query = @"Update tblJobReqForm  set IsDelete=@IsDelete, DeleteBy=@DeleteBy, DeleteDate=@DeleteDate  WHERE JobReqId = @JobReqId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }
        public bool UpdateStatus(string jobReqId, string status, string approveby, DateTime approvedate)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobReqId", jobReqId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));
            aSqlParameterlist.Add(new SqlParameter("@ApproveBy", approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", approvedate));

            const string query = @"UPDATE dbo.tblJobReqForm SET ActionStatus=@ActionStatus,ApproveBy=@ApproveBy,ApproveDate=@ApproveDate WHERE JobReqId=@JobReqId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public int SaveJobReqAppLog(JobReqAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@JobReqAppLogId", appLogDao.JobReqAppLogId));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));


                    string query = @"INSERT INTO dbo.tblJobReqAppLog
                                    (
                                    JobReqAppLogId,
                                    
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments
                                    )
                                    VALUES(
                                    @JobReqAppLogId,
                                    
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments
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

        


        public Int32 SaveEmpReqEducation(ReqEducationDAO aReqEducationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Education", aReqEducationDao.Education));
            aSqlParameterlist.Add(new SqlParameter("@JobReqId", aReqEducationDao.JobReqId));

            string insertQuery = @"INSERT INTO dbo.tblJobReqEducation
                                            ( Education, JobReqId )
                                    VALUES  ( @Education, @JobReqId )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public bool DelEmpReqEducation(string  id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.tblJobReqEducation WHERE JobReqId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveJobReqKeyRespon(JobReqKeyRespon aJobReqKeyRespon)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@JobReqFormId", aJobReqKeyRespon.JobReqFormId));
           // aSqlParameterlist.Add(new SqlParameter("@KeyResId", aJobReqKeyRespon.KeyResId));
            aSqlParameterlist.Add(new SqlParameter("@JobReqKeyResName", aJobReqKeyRespon.JobReqKeyResName));

            string insertQuery = @"INSERT INTO dbo.tblJobReqKeyRespon
                                            ( JobReqFormId, JobReqKeyResName)
                                    VALUES  ( @JobReqFormId,  @JobReqKeyResName)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }

        public Int32 SaveOfficeLocation(OfficeLocationForRequisition aJobReqKeyRespon)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aJobReqKeyRespon.SalaryLoationId));
            aSqlParameterlist.Add(new SqlParameter("@ReqMasterId", aJobReqKeyRespon.ReqMasterId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationName", aJobReqKeyRespon.SalaryLoationName));

           

            string insertQuery = @"INSERT INTO dbo.tblOfficeLocationForRequisition
                                            ( SalaryLoationId, ReqMasterId, SalaryLoationName)
                                    VALUES  ( @SalaryLoationId,  @ReqMasterId, @SalaryLoationName)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }

        public bool DelJobReqKeyRespon(string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.tblJobReqKeyRespon WHERE JobReqFormId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }

        public bool DelOtherRequirementDetail(string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.OtherRequirementDetail WHERE MasterId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }


        public bool DelOfficeDetail(string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.tblOfficeLocationForRequisition WHERE ReqMasterId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }


        public bool DelEducationRequirementsDetail(string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.tblEducationRequirementsDetail WHERE MasterId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }

        public bool DelJobReqEduREqui(string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.tblEducationRequirDesJobReq WHERE JobReqFormId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveEduRequirSave(EducationRequirDesJobReq aJobReqKeyRespon)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@JobReqFormId", aJobReqKeyRespon.JobReqFormId));
            aSqlParameterlist.Add(new SqlParameter("@EduRequirId", aJobReqKeyRespon.EduRequirId));
            aSqlParameterlist.Add(new SqlParameter("@Major", aJobReqKeyRespon.Major));

            string insertQuery = @"INSERT INTO dbo.tblEducationRequirDesJobReq
                                            ( JobReqFormId, EduRequirId, Major)
                                    VALUES  ( @JobReqFormId, @EduRequirId, @Major)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }
        public bool DelKeyResponsbility(string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.tblKeyResponsibility WHERE JobReqId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }

        public void LoadDesignation(DropDownList ddl)
        {
            string query = "SELECT * FROM dbo.tblDesignation";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }

        public void LoadDesignationByCompanyId(DropDownList ddl, string id)
        {
            string query = "SELECT * FROM dbo.tblDesignation WHERE CompanyId="+id;
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }

        public void LoadCodeBudget(DropDownList ddl)
        {
           // string query = "SELECT * FROM dbo.tblMPBudget";
            string query = @"SELECT MPB.MPBudgetDetailId AS MPBudgetId,MPBM.BudgetCode
                             FROM dbo.tblMPBudget AS MPB
                            INNER JOIN dbo.tblMPBudgetMaster AS MPBM ON MPBM.MPBudgetMasterId = MPB.MPBudgetMasterId
                        WHERE MPBM.IsActive = 1";
            aCommonInternalDal.LoadDropDownValue(ddl, "BudgetCode", "MPBudgetId", query, "HRDB");
        }

        //public void KeyResponsibilites(DropDownList ddl)
        //{
        //    string query = " SELECT * FROM dbo.tblKeyResponsibility";
        //    aCommonInternalDal.LoadDropDownValue(ddl, "KeyRespon", "KeyResponId", query, "HRDB");
        //}

        public void EducationRequirementDropDownList(DropDownList ddl)
        {
            string query = " SELECT * FROM dbo.tblEducationName WITH (NOLOCK)";
            aCommonInternalDal.LoadDropDownValue(ddl, "Description", "EducationNameID", query, "HRDB");
        }
        public void LoadGrade(DropDownList ddl)
        {
            string query = "SELECT SalaryGradeId,GradeCode + ISNULL(' : ' + GradeName,'') AS GradeCode FROM dbo.tblSalaryGrade where IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "GradeCode", "SalaryGradeId", query, "HRDB");
        }

        public void LoadGradeByCatID(DropDownList ddl, string EmpCateID)
        {
            string query = "SELECT SalaryGradeId,GradeCode + ' : ' + GradeName AS GradeCode FROM dbo.tblSalaryGrade WHERE EmpCategoryId='" + EmpCateID + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "GradeCode", "SalaryGradeId", query, "HRDB");
        }

        public void GetEmpCategoryDDL(DropDownList ddl)
        {
            string query = @"SELECT c.EmpCategoryId ,c.EmpCategoryName   FROM dbo.tblEmpCategory c WHERE c.IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", query, "HRDB");
        }


        public void GetEmpCategoryDDLTop2(DropDownList ddl)
        {
            string query = @"SELECT top 2 c.EmpCategoryId ,c.EmpCategoryName   FROM dbo.tblEmpCategory c WHERE c.IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", query, "HRDB");
        }
        public void LoadDivision(DropDownList ddl)
        {
            string query = "SELECT * FROM dbo.tblDivision";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", query, "HRDB");
        }
        public void LoadWingsByDivision(DropDownList ddl,string divId)
        {
            string query = "SELECT * FROM dbo.tblDivisionWing WHERE DivisionId='"+divId+"'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", query, "HRDB");
        }
        public void LoadDepartmentbYcOMid(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblDepartment WHERE IsActive=1 AND CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, "HRDB");
        }


        public void LoadDepartmentByWings(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblDepartment WHERE CompanyId='" + CompanyId + "' AND DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE IsActive=1 and UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, "HRDB");
        }

        public void LoadDepartmentByWings2(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblDepartment WHERE CompanyId='" + CompanyId + "' ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, "HRDB");
        }
        public void LoadEmpInfo(DropDownList ddl)
        {
            string query = "SELECT * FROM dbo.tblEmpGeneralInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpMasterCode", "EmpInfoId", query, "HRDB");
        }
        public DataTable LoadEmpJobRequisition(string param)
        {
            string query = @"SELECT JQF.JobReqId, JQF.ReqCode, CI.CompanyName,JQF.JobTitle, JQF.ReqDate, JQF.Nos,FINY.FinancialYearDesc, Dpt.DepartmentName, JQF.ActionStatus
,(JQF.ActionStatus2+(CASE WHEN PreEmp.EmpName IS NULL THEN '' ELSE '('+PreEmp.EmpName+')' END))ActionStatus2,ForEmp.EmpName,PreEmp.EmpName,

CASE
    WHEN CELog.ReqCodeId IS NULL THEN 'No'
    WHEN CELog.ReqCodeId IS NOT  NULL  THEN 'Yes'
  END AS jobCirculation
--(tblEmpGeneralInfo.EmpName +'  ('+ tblJobReqFormAppLog.ActionStatus+')') AS RecruitmentStatus
FROM dbo.tblJobReqForm AS JQF
INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = JQF.CompanyId
LEFT JOIN dbo.tblFinancialYear AS FINY ON JQF.FinYearId = FINY.FinancialYearId
LEFT JOIN dbo.tblDepartment AS Dpt ON JQF.DeptId = Dpt.DepartmentId
--LEFT JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog
 
--WHERE tblJobReqFormAppLog.ActionStatus NOT IN
--('Review') GROUP BY JobReqId) AS CELog ON CELog.JobReqId= JQF.JobReqId

--left JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = JQF.JobReqId			
--left JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblJobReqFormAppLog.ForEmpInfoId

LEFT JOIN (SELECT ReqCodeId  FROM dbo.tblJobCreation
 
WHERE tblJobCreation.ReqCodeId IS NOT null
  GROUP BY ReqCodeId) AS CELog ON CELog.ReqCodeId
  = JQF.JobReqId

LEFT JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY JobReqId) AS LogApp ON LogApp.JobReqId= JQF.JobReqId
								LEFT JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = JQF.JobReqId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblJobReqFormAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblJobReqFormAppLog PreLog ON PreLog.JobReqId=JQF.JobReqId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId

WHERE   ( JQF.IsDelete IS NULL
                                      OR JQF.IsDelete = 0 
                                    ) 

and (tblJobReqFormAppLog.Version=LogApp.MaxVer OR   tblJobReqFormAppLog.Version IS NULL)

" + param  +"  order by   JQF.JobReqId desc";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable RptLoadEmpJobRequisition(string param)
        {
            string query = @" SELECT COUNT(emp.JobID) TotalJoin, r.ReqDate  JobReqFormDate, ( j.Position) AS jobTitle ,dpt.DepartmentName  , cat.EmpCategoryName, r.Nos, r.JobReqId FROM dbo.tblJobCreation j WITH (NOLOCK)
  INNER JOIN dbo.tblJobReqForm r ON r.JobReqId=j.ReqCodeId 
    LEFT  JOIN dbo.tblDepartment dpt ON r.DeptId=dpt.DepartmentId
    LEFT  JOIN dbo.tblEmpCategory cat ON r.EmployeeCategoryId=cat.EmpCategoryId
  LEFT  JOIN dbo.tblEmpGeneralInfo emp ON j.JobID=emp.JobID
  WHERE emp.JobID IS NOT NULL " + param + " GROUP BY   r.ReqDate , ( j.Position)  ,dpt.DepartmentName  , cat.EmpCategoryName, r.Nos ,  r.JobReqId";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadEmpJobRequisitionForCheckJobCreationId(string param)
        {
            string query = @"SELECT JQF.JobReqId, JQF.ReqCode, CI.CompanyName,JQF.JobTitle, JQF.ReqDate, JQF.Nos,FINY.FinancialYearDesc, Dpt.DepartmentName, JQF.ActionStatus,

CASE
    WHEN CELog.ReqCodeId IS NULL THEN 'No'
    WHEN CELog.ReqCodeId IS NOT  NULL  THEN 'Yes'
  END AS jobCirculation
--(tblEmpGeneralInfo.EmpName +'  ('+ tblJobReqFormAppLog.ActionStatus+')') AS RecruitmentStatus
FROM dbo.tblJobReqForm AS JQF
INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = JQF.CompanyId
LEFT JOIN dbo.tblFinancialYear AS FINY ON JQF.FinYearId = FINY.FinancialYearId
LEFT JOIN dbo.tblDepartment AS Dpt ON JQF.DeptId = Dpt.DepartmentId
--LEFT JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog
 
--WHERE tblJobReqFormAppLog.ActionStatus NOT IN
--('Review') GROUP BY JobReqId) AS CELog ON CELog.JobReqId= JQF.JobReqId

--left JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = JQF.JobReqId			
--left JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblJobReqFormAppLog.ForEmpInfoId

LEFT JOIN (SELECT ReqCodeId  FROM dbo.tblJobCreation
 
WHERE tblJobCreation.ReqCodeId IS NOT null
  GROUP BY ReqCodeId) AS CELog ON CELog.ReqCodeId
  = JQF.JobReqId




WHERE   ( JQF.IsDelete IS NULL
                                      OR JQF.IsDelete = 0 
                                    )   and  JQF.JobReqId=" + param;

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmpJobRequisitionApp(string actionstatus)
        {
            string query = @"SELECT JQF.JobReqId, JQF.ReqCode, CI.CompanyName,JQF.JobTitle, JQF.ReqDate, JQF.Nos,FINY.FinancialYearDesc FROM dbo.tblJobReqForm AS JQF
                            INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = JQF.CompanyId
                            INNER JOIN dbo.tblFinancialYear AS FINY ON JQF.FinYearId = FINY.FinancialYearId WHERE ActionStatus='" + actionstatus + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmpJobRequisitionApp()
        {
            string query = @"SELECT JQF.JobReqId, JQF.ReqCode, CI.ShortName CompanyName,JQF.JobTitle, JQF.ReqDate, JQF.Nos,FINY.FinancialYearDesc,JobReqFormAppLogId FROM dbo.tblJobReqForm AS JQF
                            INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = JQF.CompanyId
                            INNER JOIN dbo.tblFinancialYear AS FINY ON JQF.FinYearId = FINY.FinancialYearId
							INNER JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY JobReqId) AS CELog ON CELog.JobReqId= JQF.JobReqId
								INNER JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = JQF.JobReqId
                                where (JQF.IsDelete is null or JQF.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadEmpJobRequisitionAppCheck(int Id)
        {
            string query = @"SELECT JQF.JobReqId, JQF.ReqCode, CI.ShortName CompanyName,JQF.JobTitle, JQF.ReqDate, JQF.Nos,FINY.FinancialYearDesc,JobReqFormAppLogId FROM dbo.tblJobReqForm AS JQF
                            INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = JQF.CompanyId
                            INNER JOIN dbo.tblFinancialYear AS FINY ON JQF.FinYearId = FINY.FinancialYearId
							INNER JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY JobReqId) AS CELog ON CELog.JobReqId= JQF.JobReqId
								INNER JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = JQF.JobReqId
                                where (JQF.IsDelete is null or JQF.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = '" + Id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmpJobRequisitionById(string id)
        {
            string query = @"SELECT EGE.EmpInfoId,MPB.FinancialYearId,* FROM dbo.tblJobReqForm AS JRFM 
                            LEFT JOIN dbo.tblMPBudgetMaster AS MPB ON MPB.CompanyId = JRFM.CompanyId
                            LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON JRFM.ReplaceEmpId = EGI.EmpInfoId
							LEFT JOIN dbo.tblUser AS U ON U.UserId = JRFM.EntryBy
							LEFT JOIN dbo.tblEmpGeneralInfo EGE ON EGE.EmpInfoId=U.EmpInfoId WHERE JRFM.JobReqId = '" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadJobCirculationStatusById(string id)
        {
            string query = @"SELECT JQF.JobReqId, 
CASE
    WHEN CELog.ReqCodeId IS NULL  THEN 'Pending'
    WHEN CELog.ReqCodeId IS NOT  NULL AND    CELog.ActionStatus='Drafted'  THEN 'Pending'
    WHEN CELog.ReqCodeId IS NOT  NULL AND CELog.ActionStatus='Submitted'  THEN 'Done'
  END AS jobCirculation
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN (SELECT ReqCodeId,ActionStatus  FROM dbo.tblJobCreation
WHERE tblJobCreation.ReqCodeId IS NOT null
   AND ( tblJobCreation.IsDelete IS NULL OR tblJobCreation.IsDelete = 0)   GROUP BY ReqCodeId, ActionStatus) AS CELog ON CELog.ReqCodeId
  = JQF.JobReqId
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId = '" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadInterviewCandidateInformationStatusById(string id)
        {
            string query = @"SELECT JQF.JobReqId, CASE
    WHEN CELog.JobID IS NULL THEN 'Pending'
    WHEN CELog.JobID IS NOT  NULL AND  tblInterviewBoardSetupMaster.JobTitleId IS NULL  THEN 'Processing'
	WHEN tblInterviewBoardSetupMaster.JobTitleId IS NOT NULL THEN 'Done'
  END AS InterviewCandidate
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId

LEFT JOIN (SELECT JobID  FROM dbo.tblInterviewCandidateInfo
WHERE tblInterviewCandidateInfo.JobID IS NOT null
   AND ( tblInterviewCandidateInfo.IsActive  = 1) GROUP BY JobID) AS CELog ON CELog.JobID
  = tblJobCreation.JobID
  LEFT JOIN dbo.tblInterviewBoardSetupMaster ON CELog.JobID=tblInterviewBoardSetupMaster.JobTitleId
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  )  AND JQF.JobReqId ='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadCheckLastStatusById(string id)
        {
            string query = @"SELECT DISTINCT ISNULL(JQF.JobReqId, NULL), 1 AS SerialNo,
CASE
    WHEN CELog.ReqCodeId IS NULL   THEN 'Not Done'
   WHEN CELog.ReqCodeId IS NOT  NULL AND  CELog.ActionStatus='Drafted'   THEN 'Not Done'
    WHEN CELog.ReqCodeId IS NOT  NULL AND CELog.ActionStatus='Submitted'  THEN 'Job Circulation'
  END AS jobCirculation
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN (SELECT ReqCodeId,ActionStatus  FROM dbo.tblJobCreation
WHERE tblJobCreation.ReqCodeId IS NOT null
   AND ( tblJobCreation.IsDelete IS NULL OR tblJobCreation.IsDelete = 0)   GROUP BY ReqCodeId, ActionStatus) AS CELog ON CELog.ReqCodeId
  = JQF.JobReqId
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId =" + id +

                          " UNION ALL " +
                           @"SELECT DISTINCT ISNULL(JQF.JobReqId, NULL), 2 AS SerialNo, CASE
    WHEN CELog.JobID IS NULL THEN 'Not Done'
   WHEN CELog.JobID IS NOT  NULL  THEN 'Interview Candidate Information'
  END AS InterviewCandidate
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT JobID  FROM dbo.tblInterviewCandidateInfo
WHERE tblInterviewCandidateInfo.JobID IS NOT null
   AND ( tblInterviewCandidateInfo.IsActive  = 1) GROUP BY JobID) AS CELog ON CELog.JobID
  = tblJobCreation.JobID
  LEFT JOIN dbo.tblInterviewBoardSetupMaster ON CELog.JobID=tblInterviewBoardSetupMaster.JobTitleId
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  )  AND JQF.JobReqId =" + id +
                           " UNION ALL " +
                           @"SELECT DISTINCT ISNULL(JQF.JobReqId, NULL),3 AS SerialNo, CASE
    WHEN CELog.JobTitleId IS NULL THEN 'Not Done'
    WHEN CELog.JobTitleId IS NOT  NULL  THEN 'Interview Board Information'
  END AS lblInterviewBoardInformation
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT JobTitleId  FROM dbo.tblInterviewBoardSetupMaster
WHERE tblInterviewBoardSetupMaster.JobTitleId IS NOT null
   AND ( tblInterviewBoardSetupMaster.IsActive  = 1) GROUP BY JobTitleId) AS CELog ON CELog.JobTitleId
  = tblJobCreation.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId =" + id +
                          " UNION ALL " +
                           @"SELECT DISTINCT ISNULL(CELog.CandidateID, NULL),4 AS SerialNo, CASE
    WHEN CELog.CandidateID IS NULL THEN 'Not Done'
    WHEN CELog.CandidateID IS NOT  NULL  THEN 'Interview Candidate Invitation'
  END AS InterviewCandidateInvitation
FROM dbo.tblJobReqForm AS JQF

LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT I.CandidateID,tblInterviewCandidateInfo.JobID FROM dbo.tblInterviewCandidateInvitation I 
            LEFT JOIN dbo.tblInterviewCandidateInfo ON tblInterviewCandidateInfo.CandidateID = I.CandidateID WHERE (I.IsActive=1 OR I.IsActive IS NULL)  ) AS CELog ON CELog.JobID= tblJobCreation.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  )  AND JQF.JobReqId =" + id +
                          " UNION ALL " +
                           @"SELECT DISTINCT ISNULL(CELog.CandidateID, NULL),5 AS SerialNo, CASE
    WHEN CELog.CandidateID IS NULL THEN 'Not Done'
    WHEN CELog.CandidateID IS NOT  NULL  THEN 'Candidate Attandance'
  END AS InterviewCandidateAttandance
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT I.CandidateID,tblInterviewCandidateInfo.JobID FROM dbo.tblInterviewCandidateAttandance I 
            LEFT JOIN dbo.tblInterviewCandidateInfo ON tblInterviewCandidateInfo.CandidateID = I.CandidateID WHERE I.IsActive=1 ) AS CELog ON CELog.JobID= tblJobCreation.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId =" + id +
                          " UNION ALL " +
                           @"SELECT  DISTINCT ISNULL(JQF.JobReqId, NULL),6 AS SerialNo,  CASE
     WHEN CELog.JobTitleId IS NULL THEN 'Not Done'
    WHEN CELog.JobTitleId IS NOT  NULL and tblEmpGeneralInfo.JobID IS  NULL THEN 'Not Done'
	 WHEN CELog.JobTitleId IS NOT  NULL and tblEmpGeneralInfo.JobID IS NOT NULL THEN 'Interview Board Member Marks Entry'
  END AS MarksEntry
FROM dbo.tblJobReqForm AS JQF

LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT I.JobTitleId FROM dbo.tblInterviewBoardSetupMaster I 
            WHERE I.IsActive=1 ) AS CELog ON CELog.JobTitleId= tblJobCreation.JobID
LEFT JOIN dbo.tblEmpGeneralInfo ON tblJobCreation.JobID=tblEmpGeneralInfo.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId =" + id +
                           " UNION ALL " +
                           @"SELECT DISTINCT ISNULL(tblEmpGeneralInfo.JobID, NULL),7 AS SerialNo, CASE
    WHEN tblEmpGeneralInfo.JobID IS NULL THEN 'Not Done'
    WHEN   tblEmpGeneralInfo.JobID IS NOT NULL THEN 'Employee Information'
  END AS EmpJob
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN dbo.tblEmpGeneralInfo ON tblJobCreation.JobID=tblEmpGeneralInfo.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  )  AND JQF.JobReqId =" + id ;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadInterviewBoardInformationStatusById(string id)
        {
            string query = @"SELECT JQF.JobReqId, CASE
    WHEN CELog.JobTitleId IS NULL THEN 'Pending'
    WHEN CELog.JobTitleId IS NOT  NULL  THEN 'Done'
  END AS lblInterviewBoardInformation
FROM dbo.tblJobReqForm AS JQF
LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT JobTitleId  FROM dbo.tblInterviewBoardSetupMaster
WHERE tblInterviewBoardSetupMaster.JobTitleId IS NOT null
   AND ( tblInterviewBoardSetupMaster.IsActive  = 1) GROUP BY JobTitleId) AS CELog ON CELog.JobTitleId
  = tblJobCreation.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId ='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadInterviewCandidateInvitationStatusById(string id)
        {
            string query = @"
SELECT CELog.CandidateID, CASE
    WHEN CELog.CandidateID IS NULL THEN 'Pending'
    WHEN CELog.CandidateID IS NOT  NULL  THEN 'Done'
  END AS InterviewCandidateInvitation
FROM dbo.tblJobReqForm AS JQF

LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT I.CandidateID,tblInterviewCandidateInfo.JobID FROM dbo.tblInterviewCandidateInvitation I 
            LEFT JOIN dbo.tblInterviewCandidateInfo ON tblInterviewCandidateInfo.CandidateID = I.CandidateID WHERE (I.IsActive=1 OR I.IsActive IS NULL)  ) AS CELog ON CELog.JobID= tblJobCreation.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  )  AND JQF.JobReqId ='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadInterviewCandidateAttandanceStatusById(string id)
        {
            string query = @"SELECT CASE
    WHEN CELog.CandidateID IS NULL THEN 'Pending'
    WHEN CELog.CandidateID IS NOT  NULL  THEN 'Done'
  END AS InterviewCandidateAttandance
FROM dbo.tblJobReqForm AS JQF

LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT I.CandidateID,tblInterviewCandidateInfo.JobID FROM dbo.tblInterviewCandidateAttandance I 
            LEFT JOIN dbo.tblInterviewCandidateInfo ON tblInterviewCandidateInfo.CandidateID = I.CandidateID WHERE I.IsActive=1 ) AS CELog ON CELog.JobID= tblJobCreation.JobID



WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId ='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }




        public DataTable LoadMarksEntryStatusById(string id)
        {
            string query = @"SELECT  JQF.JobReqId,  CASE
     WHEN CELog.JobTitleId IS NULL AND tblEmpGeneralInfo.JobID IS  NULL THEN 'Pending'
    WHEN CELog.JobTitleId IS NOT  NULL and tblEmpGeneralInfo.JobID IS  NULL THEN 'Pending'
	 WHEN CELog.JobTitleId IS NOT  NULL and tblEmpGeneralInfo.JobID IS NOT NULL THEN 'Done'
  END AS MarksEntry
FROM dbo.tblJobReqForm AS JQF

LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN (SELECT I.JobTitleId FROM dbo.tblInterviewBoardSetupMaster I 
            WHERE I.IsActive=1 ) AS CELog ON CELog.JobTitleId= tblJobCreation.JobID
LEFT JOIN dbo.tblEmpGeneralInfo ON tblJobCreation.JobID=tblEmpGeneralInfo.JobID
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  ) AND JQF.JobReqId ='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable LoadEmpReqIdStatusById(string id)
        {
            string query = @"SELECT CASE
    WHEN tblEmpGeneralInfo.JobID IS NULL THEN 'Pending'
    WHEN   tblEmpGeneralInfo.JobID IS NOT NULL THEN 'Done'
  END AS EmpJob
FROM dbo.tblJobReqForm AS JQF

LEFT JOIN dbo.tblJobCreation ON JQF.JobReqId=tblJobCreation.ReqCodeId
LEFT JOIN dbo.tblEmpGeneralInfo ON tblJobCreation.JobID=tblEmpGeneralInfo.JobID
 
WHERE   ( (JQF.IsDelete IS NULL) OR (JQF.IsDelete = 0)  )  AND JQF.JobReqId ='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable RptLoadEmpJobRequisitionById(string id)
        {
            string query = @" SELECT CASE WHEN JRFM.IsReplacement=1 THEN 'Replacement' ELSE 'New' END PositionNewRep,JRFM.ReplaceEmpId, MPB.ShortName,  Fin.FinancialYearId, Fin.FinancialYearDesc, Dpt.DepartmentName, JRFM.ReqDate,JRFM.Note, JRFM.JobTitle, Sgrade.GradeName, JRFM.Nos, JRFM.ExpDateOfJoining, EType.EmpType,  JRFM.ReportingTo,
SLOC.SalaryLocation, JLOc.Location, EGI.EmpName  ReplacementEmployee,tblsal.GradeCode repGradeCode, tblsalStep.SalaryStepName RepSalaryStepName, JRFM.InternalContact, JRFM.ExternalContact, JRFM.Justification, JRFM.JobSummery,JRFM.Experience, JRFM.Skills, JRFM.Age,   JRFM.OtherExperience ,
JRFM.IsManagementApproved, cat.EmpCategoryName,JRFM.IsBudgeted, Pset.ProjectName,  CASE WHEN JRFM.IsReplacement=1 THEN FORMAT(JRFM.SeparationDate, 'dd-MMM-yyyy') ELSE '' END MonthContractual, JRFM.FundContractual, JRFM.Remarks
 FROM dbo.tblJobReqForm AS JRFM 
  LEFT JOIN dbo.tblCompanyInfo AS MPB ON MPB.CompanyId = JRFM.CompanyId
   LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON JRFM.ReplaceEmpId = EGI.EmpInfoId              
   LEFT JOIN dbo.tblSalaryGrade AS tblsal ON tblsal.SalaryGradeId = EGI.SalaryGradeId              
   LEFT JOIN dbo.tblSalaryStep AS tblsalStep ON tblsalStep.SalaryStepId = EGI.SalaryStepId              
   LEFT JOIN dbo.tblEmpGeneralInfo AS rep ON JRFM.ReportingTo = rep.EmpInfoId              
	 LEFT JOIN dbo.tblFinancialYear AS Fin ON JRFM.FinYearId = Fin.FinancialYearId						
	 LEFT JOIN dbo.tblDepartment AS Dpt ON JRFM.DeptId = Dpt.DepartmentId
	 LEFT JOIN dbo.tblProjectSetup AS Pset ON JRFM.ProjectId = Pset.ProjectId
	 LEFT JOIN dbo.tblEmpCategory AS cat ON JRFM.EmployeeCategoryId = cat.EmpCategoryId
		 LEFT JOIN dbo.tblSalaryGrade AS Sgrade ON JRFM.GradeId = Sgrade.SalaryGradeId	
		 LEFT JOIN dbo.tblEmployeeType AS EType ON JRFM.EmpTypeId = EType.EmpTypeId	
		 LEFT JOIN dbo.tblSalaryLocation AS SLOC ON JRFM.OfficeId = SLOC.SalaryLoationId
		 LEFT JOIN dbo.tblJobLocation AS JLOc ON JRFM.PlaceId = JLOc.JobLocationID   WHERE JRFM.JobReqId = '" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadKeyResponseById(string id)
        {
            string query = @" SELECT * FROM tblJobReqKeyRespon WHERE JobReqFormId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadReqEducationById(string id)
        {
            string query = @"   SELECT * FROM dbo.tblJobReqEducation WHERE JobReqId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable GetJobCreationKeyResponByJobId(int jobId)
        {
            string query = @" SELECT *, ks.KeyResponId, ks.KeyRespon, KRS.JobReqKeyResName  FROM dbo.tblJobReqKeyRespon KRS 
								 left JOIN dbo.tblJobReqForm JRF ON KRS.JobReqFormId=JRF.JobReqId
							left JOIN dbo.tblKeyResponsibility ks ON ks.KeyResponId=KRS.KeyResId 
							 WHERE JRF.JobReqId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetOfficeByJobId(int jobId)
        {
            string query = @"SELECT SalaryLoationName as SalaryLocation, SalaryLoationId, SalaryLoationMainId AS SalaryLocationMainId   FROM tblOfficeLocationForRequisition WHERE ReqMasterId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetAppLogCommByJobId(int jobId)
        {
            string query = @"SELECT Alg.JobReqFormAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, Alg.ActionStatus, Alg.ApproveDate, Alg.JobReqId, Alg.Comments FROM tblJobReqFormAppLog Alg
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and Alg.JobReqId='" + jobId + "'   order by Version asc ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetId()
        {
            try
            {
                string query = @"SELECT (COUNT(*)+1)A FROM dbo.tblJobReqForm ";

                return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable GetJobEduReqByJobId(int jobId)
        {
            string query = @"SELECT  EducationNameID ERID,  ks.Description EducationRequirements,*  FROM dbo.tblEducationRequirDesJobReq KRS INNER JOIN dbo.tblJobReqForm JRF ON KRS.JobReqFormId=JRF.JobReqId
							INNER JOIN dbo.tblEducationName ks ON ks.EducationNameID=KRS.EduRequirId
							 WHERE JRF.JobReqId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetEducationRequirementsDetailId(int jobId)
        {
            string query = @"SELECT MasterId, WayId, tblDesignation.Designation, Nos,* FROM dbo.tblEducationRequirementsDetail
LEFT JOIN dbo.tblDesignation ON tblEducationRequirementsDetail.WayId= tblDesignation.DesignationId
 WHERE MasterId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetOtherRequirementsDetailId(int jobId)
        {
            string query = @"SELECT MasterId, OtherRequirement as OtherRequirementsName, * FROM dbo.OtherRequirementDetail
 
 WHERE MasterId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetEmpData(string empId)
        {
//            string query = @"SELECT (CASE WHEN tblSection.Invisible IS NULL OR tblSection.Invisible='False' THEN tblSection.SectionName ELSE NULL END)SectionName,(CASE WHEN tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False' THEN tblDepartment.DepartmentName ELSE NULL END)DepartmentName
//	,(CASE WHEN tblDivisionWing.Invisible IS NULL OR tblDivisionWing.Invisible='False' THEN tblDivisionWing.DivisionWingName ELSE NULL END)DivisionWingName,* FROM dbo.tblEmpGeneralInfo
//INNER JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId
//INNER JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
//INNER JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
//INNER JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblEmpGeneralInfo.DivisionWId
//INNER JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblEmpGeneralInfo.DepartmentId
//INNER JOIN dbo.tblSection ON tblSection.SectionId = tblEmpGeneralInfo.SectionId
//INNER JOIN dbo.tblSubSection ON tblSubSection.SubSectionId = tblEmpGeneralInfo.SubSectionId
//LEFT JOIN dbo.tblEmployeeJobLeft ON dbo.tblEmployeeJobLeft.EmployeeId= dbo.tblEmpGeneralInfo.EmpInfoId
//WHERE EmpInfoId='" + empId+"' ";

            string query = @" SELECT   tblSalaryStep.GrossAmount, Dpt.DepartmentName, tblEmployeeType.EmpType,tblEmpGeneralInfo.EmpMasterCode,tblEmpGeneralInfo.EmpName, * FROM dbo.tblEmpGeneralInfo
INNER JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId
LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
LEFT JOIN dbo.tblSalaryStep ON tblSalaryStep.SalaryStepId = tblEmpGeneralInfo.SalaryStepId
INNER JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
LEFT JOIN dbo.tblEmployeeJobLeft ON dbo.tblEmployeeJobLeft.EmployeeId= dbo.tblEmpGeneralInfo.EmpInfoId
	LEFT JOIN dbo.tblDepartment  Dpt ON tblEmpGeneralInfo.DepartmentId=Dpt.DepartmentId
LEFT JOIN dbo.tblEmployeeType ON tblEmployeeType.EmpTypeId = tblEmpGeneralInfo.EmpTypeId WHERE EmpInfoId='" + empId + "' ";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetEmpDataDesignation(string empId)
        {
            //            string query = @"SELECT (CASE WHEN tblSection.Invisible IS NULL OR tblSection.Invisible='False' THEN tblSection.SectionName ELSE NULL END)SectionName,(CASE WHEN tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False' THEN tblDepartment.DepartmentName ELSE NULL END)DepartmentName
            //	,(CASE WHEN tblDivisionWing.Invisible IS NULL OR tblDivisionWing.Invisible='False' THEN tblDivisionWing.DivisionWingName ELSE NULL END)DivisionWingName,* FROM dbo.tblEmpGeneralInfo
            //INNER JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId
            //INNER JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
            //INNER JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
            //INNER JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblEmpGeneralInfo.DivisionWId
            //INNER JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblEmpGeneralInfo.DepartmentId
            //INNER JOIN dbo.tblSection ON tblSection.SectionId = tblEmpGeneralInfo.SectionId
            //INNER JOIN dbo.tblSubSection ON tblSubSection.SubSectionId = tblEmpGeneralInfo.SubSectionId
            //LEFT JOIN dbo.tblEmployeeJobLeft ON dbo.tblEmployeeJobLeft.EmployeeId= dbo.tblEmpGeneralInfo.EmpInfoId
            //WHERE EmpInfoId='" + empId+"' ";

            string query = @" SELECT tblDesignation.Designation, tblEmpGeneralInfo.EmpName FROM dbo.tblEmpGeneralInfo
INNER JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId WHERE tblEmpGeneralInfo.EmpInfoId='" + empId + "' ";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetEmpDataCode(string empcode)
        {
            //            string query = @"SELECT (CASE WHEN tblSection.Invisible IS NULL OR tblSection.Invisible='False' THEN tblSection.SectionName ELSE NULL END)SectionName,(CASE WHEN tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False' THEN tblDepartment.DepartmentName ELSE NULL END)DepartmentName
            //	,(CASE WHEN tblDivisionWing.Invisible IS NULL OR tblDivisionWing.Invisible='False' THEN tblDivisionWing.DivisionWingName ELSE NULL END)DivisionWingName,* FROM dbo.tblEmpGeneralInfo
            //INNER JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId
            //INNER JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
            //INNER JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
            //INNER JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblEmpGeneralInfo.DivisionWId
            //INNER JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblEmpGeneralInfo.DepartmentId
            //INNER JOIN dbo.tblSection ON tblSection.SectionId = tblEmpGeneralInfo.SectionId
            //INNER JOIN dbo.tblSubSection ON tblSubSection.SubSectionId = tblEmpGeneralInfo.SubSectionId
            //LEFT JOIN dbo.tblEmployeeJobLeft ON dbo.tblEmployeeJobLeft.EmployeeId= dbo.tblEmpGeneralInfo.EmpInfoId
            //WHERE EmpInfoId='" + empId+"' ";

            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo
INNER JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId
INNER JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
INNER JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
LEFT JOIN dbo.tblEmployeeJobLeft ON dbo.tblEmployeeJobLeft.EmployeeId= dbo.tblEmpGeneralInfo.EmpInfoId WHERE tblEmpGeneralInfo.EmpMasterCode='" + empcode + "' ";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetEmpDDL(string ID)
        {
            string queryStr = @"SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName   AS EmpName,  e.EmpInfoId
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL AND e.CompanyId=" + ID;
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }

        public void LoadFinancialYear(DropDownList ddl, string comapnyId)
        {
            string query = @"SELECT FINY.FinancialYearId,
                            FINY.FinancialYearDesc FROM dbo.tblFinancialYear AS FINY 
                            WHERE FINY.Status = 'Active' AND FINY.CompanyId ='" + comapnyId + "'";

            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");
        }

        public void LoadFinancialYearForSearch(DropDownList ddl, string comapnyId)
        {
            string query = @"SELECT FINY.FinancialYearId,
                            FINY.FinancialYearDesc FROM dbo.tblFinancialYear AS FINY 
                            WHERE FINY.Status = 'Active' AND FINY.CompanyId ='" + comapnyId + "'";

            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");
        }

        public void LoadCodeBudgetYearWise(DropDownList ddl, string financialId, string companyId)
        {
            string query = @"  SELECT MPBudgetDetailsId,MPB.MPBudgetMasterId AS MPBudgetId,BudgetCode +'  ['+ Designation+'] ' AS BudgetCode
                            FROM dbo.tblMPBudgetMaster AS MPB
							
							INNER JOIN dbo.tblMPBudgetDetails AS De ON De.MPBudgetMasterId = MPB.MPBudgetMasterId

							WHERE MPB.IsActive=1 AND MPB.ActionStatus2='Approved'  and MPB.FinancialYearId = '" + financialId + "' AND MPB.CompanyId = '" + companyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "BudgetCode", "MPBudgetDetailsId", query, "HRDB");
        }
        public void LoadCodeBudgetYearWise(DropDownList ddl, string financialId, string companyId,string deptID)
        {
            string query = @"  SELECT De.MPBudgetDetailsId,MPB.MPBudgetMasterId AS MPBudgetId, MPB.BudgetCode+' ['+De.Designation+']' BudgetCode
                            FROM dbo.tblMPBudgetMaster AS MPB
							
							INNER JOIN dbo.tblMPBudgetDetails AS De ON De.MPBudgetMasterId = MPB.MPBudgetMasterId
                            WHERE MPB.IsActive=1 and MPB.FinancialYearId = '" + financialId + "' AND MPB.CompanyId = '" + companyId + "' AND MPB.DepartmentId = '" + deptID + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "BudgetCode", "MPBudgetDetailsId", query, "HRDB");
        }

        public void LoadDivisionDdl(DropDownList ddl, string comapnyId)
        {
            string query = @"SELECT * FROM dbo.tblDivision AS DSN WHERE DSN.IsActive = 1 AND DSN.CompanyId = '" + comapnyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", query, "HRDB");
        }

        public DataTable GetEmpJDInfoByEmpCode(string empCode)
        {
            string query = @"SELECT JDD.JdDetailsId,JDD.JdDetailsInfo FROM dbo.tblJdDetails AS JDD 
                            INNER JOIN dbo.tblJdMaster AS JDM ON JDM.JdMasterId = JDD.JdMasterId
                            INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = JDM.EmpInfoId
                            WHERE EGI.EmpMasterCode = '" + empCode + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public void GEtProjectDdl(DropDownList ddl, string companyId)
        {
            string query = "SELECT PJS.ProjectId,PJS.ProjectName + ' : ' + PJS.ProjectDescription AS ProjectDescription FROM dbo.tblProjectSetup AS PJS WHERE PJS.IsActive = 1 AND PJS.CompanyId = '" + companyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "ProjectDescription", "ProjectId", query, "HRDB");
        }

        public void GetSalaryLocationOnOfficeDdl(DropDownList ddl)
        {
            string query = "SELECT SLL.SalaryLoationId,SLL.SalaryLocation FROM dbo.tblSalaryLocation AS SLL WHERE SLL.IsActive = 1 AND SLL.JoinIdSalaryLocation =0";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryLocation", "SalaryLoationId", query, "HRDB");
        }

        public void GetJobLocationOnPlaceDdl(DropDownList ddl, string salaryLocId)
        {
            string query = "SELECT JBL.JobLocationID,JBL.Location FROM dbo.tblJobLocation AS JBL WHERE JBL.IsActive = 1 AND JBL.SalaryLoationId = '" + salaryLocId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Location", "JobLocationID", query, "HRDB");
        }

        public void GetJobLocationOnPlaceAll(DropDownList ddl)
        {
            string query = "SELECT JBL.JobLocationID,JBL.Location FROM dbo.tblJobLocation AS JBL WHERE JBL.IsActive = 1 ";
            aCommonInternalDal.LoadDropDownValue(ddl, "Location", "JobLocationID", query, "HRDB");
        }

        public void GetJobLocationOtherJoin(DropDownList ddl, string salaryLocId)
        {
            string query = "SELECT SLL.SalaryLoationId,SLL.SalaryLocation FROM dbo.tblSalaryLocation AS SLL WHERE SLL.IsActive = 1 AND SLL.JoinIdSalaryLocation ='" + salaryLocId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryLocation", "SalaryLoationId", query, "HRDB");
        }

        public DataTable CurculationWayList()
        {
            string query = @"SELECT VacancyCirculationId,CirculationWay FROM tblVacancyCirculation WHERE IsActive = 1";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public int SaveCirculationWayDetail(CirculationWayDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@MasterId", aDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@WayId", aDao.WayId));

            string query = @"INSERT INTO tblCirculationWayDetail (MasterId,WayId)  VALUES (@MasterId,@WayId)";
            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, "HRDB");
        }
        public DataTable LoadBudgetData(int id)
        {
            string query = @"SELECT MPBD.EmpCategoryId,  MPBD.ReqApproxSalary,MPBD.ReqTotalSalary, MPBD.Designation,MPBD.DtlRemarks,MPBD.SalaryGradeId,MPBM.DepartmentId,MPBD.EmployeeRequisition,MPBD.EmployeeTypeId,MPBD.ProjectId FROM dbo.tblMPBudgetDetails AS MPBD
                            INNER JOIN dbo.tblMPBudgetMaster AS MPBM ON MPBM.MPBudgetMasterId = MPBD.MPBudgetMasterId
                            INNER JOIN dbo.tblSalaryGrade AS SLG ON SLG.SalaryGradeId = MPBD.SalaryGradeId
                            WHERE MPBD.MPBudgetDetailsId= '" + id + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetJCPreferedWayOfCircular(string masterId)
        {
            string query = @"SELECT CWD.WayId FROM tblCirculationWayDetail AS CWD WHERE CWD.MasterId = " + masterId;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable RptGetJCPreferedWayOfCircular(string masterId)
        {
            string query = @"SELECT CWD.WayId, dbo.tblVacancyCirculation.CirculationWay FROM tblCirculationWayDetail AS CWD  
LEFT JOIN tblVacancyCirculation ON tblVacancyCirculation.VacancyCirculationId=CWD.WayId WHERE CWD.MasterId =" + masterId;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable RptGetOffice(string masterId)
        {
            string query = @"SELECT SalaryLoationName as SalaryLocation, SalaryLoationId, SalaryLoationMainId AS SalaryLocationMainId   FROM tblOfficeLocationForRequisition WHERE ReqMasterId=" + masterId;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool DeletePreferedWayOfCircular(string value)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ID", value));

            string Query = @"DELETE tblCirculationWayDetail WHERE MasterId = @ID  ";
            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }

        public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblJobReqFormAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND JobReqId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')     order by JobReqFormAppLogId desc ";

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
                    aParameters.Add(new SqlParameter("@JobReqFormAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblJobReqFormAppLog set ActionStatus=@ActionStatus  where JobReqFormAppLogId = @JobReqFormAppLogId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public int SavAppLog(JobReqFormAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@JobReqId", appLogDao.JobReqId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId ?? (object)DBNull.Value));
                     

                    string query = @"INSERT INTO dbo.tblJobReqFormAppLog
                                    (
                                    JobReqId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @JobReqId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblJobReqFormAppLog WHERE JobReqId=@JobReqId),
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

        public int SaveComment(string masterId, string empinfoId,string comments)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    //aParameters.Add(new SqlParameter("@Id", masterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", empinfoId));
                    aParameters.Add(new SqlParameter("@Comments", comments));


                    string query = @" INSERT INTO dbo.tblJobReqFormAppLogComnt
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


        public DataTable GetAppLogStatus(string mid, string forempId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblJobReqFormAppLog WHERE ForEmpInfoId='" + forempId + "' AND JobReqId='" + mid + "' AND ActionStatus<>'Review'";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetAppLogEmployeeApprovalID(string mid)
        {
            try
            {
                string query = @"select distinct  u.EmpInfoId from [dbo].[tblJobReqForm] J

inner join [dbo].[tblJobReqFormAppLog] A on J.JobReqId=A.JobReqId
inner join tblUser U on U.UserId=A.ApproveBy
inner join tblEmpGeneralInfo E on E.EmpInfoId=U.EmpInfoId where j.JobReqId="+mid;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable getFinalApprovePersonInfo(string mid)
        {
            try
            {
                string query = @"select emp.EmpName, dgs.Designation from tblEmpGeneralInfo emp
left join tblDesignation dgs on emp.DesignationId=dgs.DesignationId where emp.EmpInfoId=" + mid;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
