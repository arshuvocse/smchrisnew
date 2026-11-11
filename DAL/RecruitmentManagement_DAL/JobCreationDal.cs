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

namespace DAL.RecruitmentManagement_DAL
{
    public class JobCreationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


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
                            WHERE  FINY.CompanyId ='" + comapnyId + "'";

            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");
        }

        public void LoadDepartmentByWings(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblDepartment WHERE CompanyId='" + CompanyId + "' AND DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE IsActive=1 and UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, "HRDB");
        }
        public void GetCompanyListShortNameIntoDropdown(DropDownList ddl)
        {
          
            string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
        public string GenerateAutoNumber(string table, string column, DateTime date)
        {
            try
            {
                string query = @"SELECT (((SUBSTRING((CONVERT(NVARCHAR(5),YEAR(GETDATE()))),3,2)+(CASE WHEN LEN(MONTH(GETDATE()))=1 
		THEN  '0'+CONVERT(NVARCHAR(5),MONTH(GETDATE())) ELSE CONVERT(NVARCHAR(5),MONTH(GETDATE())) END)+
		(CASE WHEN LEN(DAY(GETDATE()))=1 THEN  '0'+CONVERT(NVARCHAR(5),DAY(GETDATE())) ELSE CONVERT(NVARCHAR(5),DAY(GETDATE())) END)))+
		CONVERT(NVARCHAR(5),(ISNULL((MAX(CONVERT(INT,(SUBSTRING( JobCode ,7,10))))),1000)+1))) as AutoNumber  FROM  " + table + "  WHERE  CONVERT(NVARCHAR(11),EntryDate,106)= CONVERT(NVARCHAR(11),'" + (System.DateTime.Now).ToString("dd-MMM-yyyy").Replace("-", " ") + "',106)";
                DataTable dt = aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                Decimal asd = Convert.ToDecimal(dt.Rows[0][0].ToString()); ;
                Decimal asd1 = asd + 1;

                return dt.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void GetComapnyNameList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";

            //string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

        public void GetDepartmentList(DropDownList ddl)
        {
            string queryStr = "SELECT DepartmentId, DepartmentName FROM tblDepartment";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, "HRDB");
        }


        public void GetRequisitionCodeList(DropDownList ddl, string companyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            string queryStr = "SELECT JobReqId, (ReqCode+ ' ( '+ CONVERT(NVARCHAR(12), ReqDate) +' ) '+' : ' +JobTitle)ReqCode  FROM dbo.tblJobReqForm LEFT JOIN dbo.tblDesignation ON dbo.tblJobReqForm.JobReqId=dbo.tblDesignation.DesignationId WHERE ( (tblJobReqForm.IsDelete IS NULL ) OR( tblJobReqForm.IsDelete = 0)   ) AND tblJobReqForm.ActionStatus2='Approved'  and   tblJobReqForm.CompanyId= @CompanyId  ";
            aCommonInternalDal.LoadDropDownValue(ddl, "ReqCode", "JobReqId", queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetPositionDescriptionListByReqCode(int id)
        {
            string query = @"   SELECT * FROM dbo.tblJobReqForm Where JobReqId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public bool UpdateStatus(string jobReqId, string status)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobID", jobReqId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));

            const string query = @"UPDATE dbo.tblJobCreation SET ActionStatus=@ActionStatus WHERE JobID=@JobID";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }


        public bool UpdateActionStatus(int id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobID", id));

            string Query = @"Update dbo.tblJobCreation
                                    set   ActionStatus='Submitted'  WHERE JobID=@JobID ";
            return aCommonInternalDal.UpdateDataByUpdateCommand(Query, aSqlParameterlist, "HRDB");
        }

        public DataTable GetId()
        {
            try
            {
                string query = @"SELECT (COUNT(*)+1)A FROM dbo.tblJobCreation ";

                return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void GetSectionList(DropDownList ddl)
        {
            string queryStr = "SELECT SectionID,SectionName FROM tblSection";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionID", queryStr, "HRDB");
        }

        public Int32 SaveJobCreationInfo(JobCreationDao aJobCreationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ReqCodeId", aJobCreationDao.ReqCodeId));

            aSqlParameterlist.Add(new SqlParameter("@JobCode", aJobCreationDao.JobCode));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aJobCreationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@JobContext", aJobCreationDao.JobContext));

            aSqlParameterlist.Add(new SqlParameter("@CompensationandOtherBenefits", aJobCreationDao.CompensationandOtherBenefits));
           

            aSqlParameterlist.Add(new SqlParameter("@CirculationStartDate", aJobCreationDao.CirculationStartDate));
            aSqlParameterlist.Add(new SqlParameter("@CirculationsdeadlineDate", aJobCreationDao.CirculationsdeadlineDate));

            try
            {
                aSqlParameterlist.Add(new SqlParameter("@ProbableInterviewDate", aJobCreationDao.ProbableInterviewDate ?? (object)DBNull.Value));
            }
            catch (Exception)
            {
                
                //throw;
            }
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aJobCreationDao.Remarks));

            aSqlParameterlist.Add(new SqlParameter("@IsSalary", aJobCreationDao.IsSalary));

            aSqlParameterlist.Add(new SqlParameter("@Position", aJobCreationDao.Position));

            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aJobCreationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aJobCreationDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aJobCreationDao.ActionStatus));
         
             
            string insertQuery = @"INSERT INTO tblJobCreation
                                   ( 
                                    ReqCodeId, JobCode,CompanyId,JobContext, CompensationandOtherBenefits, IsSalary, CirculationStartDate, CirculationsdeadlineDate, ProbableInterviewDate,
                                    Remarks, EntryBy, EntryDate,Position, ActionStatus
                                  
                                    )
                            VALUES  (@ReqCodeId ,
                                      @JobCode,
                                      @CompanyId ,
                                      @JobContext , @CompensationandOtherBenefits, @IsSalary, @CirculationStartDate, @CirculationsdeadlineDate, @ProbableInterviewDate,
                                 @Remarks, @EntryBy, @EntryDate,@Position, @ActionStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }


        public Int32 SaveKeyJobLocationCirculation(KeyJobLocationCirculationDao aKeyJobLocationCirculationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


          
            aSqlParameterlist.Add(new SqlParameter("@JobCreationId", aKeyJobLocationCirculationDao.JobCreationId));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aKeyJobLocationCirculationDao.JobLocationId));

            string insertQuery = @"INSERT INTO dbo.tblKeyJobLocationCirculation
                                            (JobCreationId, JobLocationId)
                                    VALUES  (@JobCreationId, @JobLocationId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }
        public Int64 SaveEducationalReqDetail(JobCreationEdReqDao jobCreationEdReqDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobID", jobCreationEdReqDao.JobID));
            aSqlParameterlist.Add(new SqlParameter("@ERID", jobCreationEdReqDao.ERID));

            string insertQuery = @"INSERT INTO tblJobCreationEdReq (JobID,ERID) VALUES (@JobID,@ERID)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }

        public Int64 SaveJobLocationDetail(JobCreationLocationDao jobCreationLocationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobID", jobCreationLocationDao.JobID));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationID", jobCreationLocationDao.JobLocationID));

            string insertQuery = @"INSERT INTO tblJobCreationLocation (JobID,JobLocationID) VALUES (@JobID,@JobLocationID)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable GetJobCreationInfos(string param)
        {
            string query = @"SELECT *,JC.JobID,JC.JobCode, RF.ReqCode, Ci.CompanyName,  JC.CirculationStartDate, JC.JobContext, jc.CompensationandOtherBenefits, 
                                   JC.EntryBy, JC.EntryDate, JC.Updateby,JC.UpdateDate  FROM tblJobCreation AS JC 
                                   LEFT JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                                   LEFT JOIN tblJobCreationLocation AS JCL ON JC.JobID = JCL.JobID
                                  
                                   LEFT JOIN dbo.tblJobReqForm AS RF ON JC.ReqCodeId = RF.JobReqId
                                   LEFT JOIN dbo.tblFinancialYear AS FY ON RF.FinYearId = FY.FinancialYearId
                                   LEFT JOIN dbo.tblDepartment AS dpt ON RF.DeptId = dpt.DepartmentId      " + param;

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetJobCreationInfosApp()
        {
            string query = @"SELECT JC.JobID,JC.JobCode, RF.ReqCode, Ci.CompanyName,  JC.CirculationStartDate, JC.JobContext, jc.CompensationandOtherBenefits, 
                                   JC.EntryBy, JC.EntryDate, JC.Updateby,JC.UpdateDate  FROM tblJobCreation AS JC 
                                   LEFT JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                                   LEFT JOIN tblJobCreationLocation AS JCL ON JC.JobID = JCL.JobID
                                  
                                   LEFT JOIN dbo.tblJobReqForm AS RF ON JC.ReqCodeId = RF.JobReqId WHERE JC.ActionStatus<>'Approved'";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetJobCreationInfoByJobId(long jobId)
        {
            string query = @"SELECT * FROM tblJobCreation AS JC
                            INNER JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                            INNER JOIN tblDepartment AS DPT ON DPT.DepartmentID = JC.DepartmentID
                            INNER JOIN tblSection AS SEC ON JC.SectionID = SEC.SectionID WHERE JobID = '" + jobId + "'";
            
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetJobCreationEduReqByJobId(long jobId)
        {
            string query = @"SELECT * FROM tblJobCreationEdReq AS EDQ
                            INNER JOIN tblEducationRequirements AS EDQR ON EDQR.ERID = EDQ.ERID WHERE EDQ.JobID ='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable EditLoacationLoadJobLocationCirculation(string id)
        {
            string query = @"  SELECT  * FROM dbo.tblJobLocationCirculation WHERE JobCreationID='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetJobCreationLocationByJobId(long jobId)
        {
            string query = @"SELECT * FROM tblJobCreationLocation AS JCL
                            INNER JOIN tblJobLocation AS JL ON JL.JobLocationID = JCL.JobLocationID WHERE JCL.JobID = '" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadEmpJobRequisitionById(int id)
        {
            string query = @"  SELECT *, JRF.JobReqId, cf.GradeName GradeName, Deg.Designation , ET.EmpType , Div.DivisionName , WinDiv.DivisionWingName, det.DepartmentName FROM dbo.tblJobReqForm JRF
							     left JOIN dbo.tblSalaryGrade cf ON JRF.GradeId = cf.SalaryGradeId
							   left JOIN dbo.tblDesignation Deg ON JRF.JobTitleId = Deg.DesignationId
							      left JOIN dbo.tblEmployeeType ET ON JRF.EmpTypeId=ET.EmpTypeId
							      left JOIN dbo.tblDivision Div ON JRF.DivisionId=Div.DivisionId
							      left JOIN dbo.tblDivisionWing WinDiv ON JRF.WingId=WinDiv.DivisionWId
							     left JOIN dbo.tblDepartment Det ON JRF.DeptId=Det.DepartmentId
								 
								  WHERE JRF.JobReqId= '" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool UpdateJobCreationInformation(JobCreationDao aJobCreationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobID", aJobCreationDao.JobID));
            aSqlParameterlist.Add(new SqlParameter("@ReqCodeId", aJobCreationDao.ReqCodeId));
            aSqlParameterlist.Add(new SqlParameter("@JobCode", aJobCreationDao.JobCode));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aJobCreationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@JobContext", aJobCreationDao.JobContext));
            aSqlParameterlist.Add(new SqlParameter("@CompensationandOtherBenefits", aJobCreationDao.CompensationandOtherBenefits));
            aSqlParameterlist.Add(new SqlParameter("@CirculationStartDate", aJobCreationDao.CirculationStartDate));
            aSqlParameterlist.Add(new SqlParameter("@CirculationsdeadlineDate", aJobCreationDao.CirculationsdeadlineDate));
            try
            {
                aSqlParameterlist.Add(new SqlParameter("@ProbableInterviewDate", aJobCreationDao.ProbableInterviewDate ?? (object)DBNull.Value));
            }
            catch (Exception)
            {

                //throw;
            }
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aJobCreationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@IsSalary", aJobCreationDao.IsSalary));
            aSqlParameterlist.Add(new SqlParameter("@Updateby", aJobCreationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aJobCreationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@Position", aJobCreationDao.Position));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aJobCreationDao.ActionStatus));


            string query = @"UPDATE dbo.tblJobCreation SET ReqCodeId=@ReqCodeId,JobCode=@JobCode,CompanyId=@CompanyId,JobContext=@JobContext,CompensationandOtherBenefits=@CompensationandOtherBenefits,
                            CirculationStartDate=@CirculationStartDate,
                           CirculationsdeadlineDate=@CirculationsdeadlineDate,ProbableInterviewDate=@ProbableInterviewDate,                       
                           Remarks=@Remarks,IsSalary=@IsSalary,UpdateBy=@UpdateBy,
                           UpdateDate=@UpdateDate,Position=@Position, ActionStatus=@ActionStatus WHERE JobID = '" + aJobCreationDao.JobID + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public bool DeleteJobCreationEduReqDetailInformationByJobId(long jobId)
        {
            string query = @"DELETE  FROM tblJobCreationEdReq WHERE JobID = '" + jobId + "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public bool DeleteJobCreationLocationDetailInformationByJobId(long jobId)
        {
            string query = @"DELETE  FROM tblJobLocation WHERE JobID = '" + jobId + "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public Int32 SaveDegreeInformation(string degree)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Degree", degree));

            string insertQuery = @"INSERT INTO tblEducationRequirements (EducationRequirements) VALUES (@Degree)";
            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable GetJobCreationInformationById(string id)
        {
            string query = @"    SELECT tblJobReqForm.JobReqId JobReqId,* FROM dbo.tblJobCreation 
LEFT JOIN  tblJobReqForm ON tblJobCreation.JobID = tblJobReqForm.JobReqId  Where JobID='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetJobReqInformationById(string id)
        {
            string query = @"SELECT ReqCodeId FROM dbo.tblJobCreation WHERE ReqCodeId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }





        public void KeyJobCircularLocationDropDown(DropDownList ddl)
        {
            string query = " SELECT * FROM dbo.tblJobLocation";
            aCommonInternalDal.LoadDropDownValue(ddl, "Location", "JobLocationID", query, "HRDB");
        }


        public bool DeleteJobCreationById(JobCreationDao aJobCreationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobID", aJobCreationDao.JobID));


          
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aJobCreationDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aJobCreationDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aJobCreationDao.DeleteDate));


            const string query = @"Update tblJobCreation  set IsDelete=@IsDelete, DeleteBy=@DeleteBy, DeleteDate=@DeleteDate  WHERE JobID = @JobID";

        
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }





        public DataTable GetJobLocationByJobId(int jobId)
        {
            string query = @"   SELECT *, ks.JobLocationID, ks.Location  FROM dbo.tblKeyJobLocationCirculation KRS INNER JOIN dbo.tblJobCreation JRF ON KRS.JobCreationId=JRF.JobID
							INNER JOIN dbo.tblJobLocation ks ON ks.JobLocationID=KRS.JobLocationId WHERE JRF.JobID='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public bool DelJobReqKeyRespon(string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ID", id));


            string Query = @"DELETE FROM dbo.tblKeyJobLocationCirculation WHERE JobCreationId=@ID  ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(Query, aSqlParameterlist, "HRDB");
        }


        public DataTable CurculationWayList()
        {
            string query = @"SELECT VacancyCirculationId,CirculationWay FROM tblVacancyCirculation WHERE IsActive = 1";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable GetPreferedWayOfCircular(string reqMasterId)
        {
            string query = @"SELECT CWD.WayId FROM tblCirculationWayDetail AS CWD WHERE CWD.MasterId = " + reqMasterId;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public int SaveCirculationWayDetail(JCCirculationWayDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@MasterId", aDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@WayId", aDao.WayId));

            string query = @"INSERT INTO tblJCCirculationWayDetail (MasterId,WayId)  VALUES (@MasterId,@WayId)";
            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, "HRDB");
        }

        public DataTable GetJCPreferedWayOfCircular(string masterId)
        {
            string query = @"SELECT JCCD.WayId FROM tblJCCirculationWayDetail AS JCCD WHERE JCCD.MasterId = " + masterId;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool DeleteCirculationWayDetail(string masterId)
        {
            string query = @"DELETE FROM tblJCCirculationWayDetail WHERE MasterId = " + masterId;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public bool DeleteJobCreationDetailById(string masterId)
        {
            string query = @"DELETE FROM tblJCCirculationWayDetail WHERE MasterId = " + masterId;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }
    }


}
