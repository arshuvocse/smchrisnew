using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using DAO.HRIS_DAO;
namespace DAL.TrainingDAL
{
    public class TrainingDAL
    {

        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager aAcessManager = new DataAccessManager();


        #region Organization Setup

        public DataTable GetOrganizationType()
        {
            string query = @"SELECT OrgTypeId as Value,OrgTypeName as TextField FROM tblOrganizationType";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetCountry()
        {
            string query = @"SELECT CountryID as Value,Title as TextField FROM tblCountry";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }


        public bool InsertOrganizationType(string typeName)
        {
            try
            {
                bool result = false;
                string query = @"Insert into  tblOrganizationType (OrgTypeName) values('" + typeName.ToString() + "') ";

                result = _aCommonInternalDal.SaveDataByInsertCommand(query, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int SaveTrainingOrganization(TrainingOrgInfo aInfo, int user)
        {
            try
            {

                int result = 0;
                List<SqlParameter> aParamList = new List<SqlParameter>();

                aParamList.Add(new SqlParameter("@TrainingOrgName", aInfo.TrainingOrgName));
                aParamList.Add(new SqlParameter("@OrgTypeId", aInfo.OrgTypeId));
                aParamList.Add(new SqlParameter("@OrgAddress", aInfo.OrgAddress));
                aParamList.Add(new SqlParameter("@IsForeign", aInfo.IsForeign));
                aParamList.Add(new SqlParameter("@IsLocal", aInfo.IsLocal));
                aParamList.Add(new SqlParameter("@IsInHouse", aInfo.IsInHouse));
                aParamList.Add(new SqlParameter("@CompanyId", aInfo.CompanyId));

                aParamList.Add(new SqlParameter("@VendorAudit", aInfo.VendorAudit));
                aParamList.Add(new SqlParameter("@Remarks", aInfo.Remarks));
                aParamList.Add(new SqlParameter("@OrgProfile", aInfo.OrgProfile));
                aParamList.Add(new SqlParameter("@Clients", aInfo.Clients));
                aParamList.Add(new SqlParameter("@ClientsRecommendation", aInfo.ClientsRecommendation));
                aParamList.Add(new SqlParameter("@LogisticsFacility", aInfo.LogisticsFacility));
                aParamList.Add(new SqlParameter("@Affiliation", aInfo.Affiliation));
                aParamList.Add(new SqlParameter("@RegistrationYear", aInfo.RegistrationYear));
                aParamList.Add(new SqlParameter("@HasTin", aInfo.HasTin));
                aParamList.Add(new SqlParameter("@HasVat", aInfo.HasVat));
                aParamList.Add(new SqlParameter("@HasTradeLicense", aInfo.HasTradeLicense));
                aParamList.Add(new SqlParameter("@HasBankSolv", aInfo.HasBankSolv));
                aParamList.Add(new SqlParameter("@Others", aInfo.Others));
                aParamList.Add(new SqlParameter("@EntryBy", user));
                aParamList.Add(new SqlParameter("@ContactPerson", aInfo.ContactPerson));
                aParamList.Add(new SqlParameter("@ContactPersonCell", aInfo.ContactPersonCell));
                aParamList.Add(new SqlParameter("@ContactPersonEmail", aInfo.ContactPersonEmail));
                aParamList.Add(new SqlParameter("@CountryID", aInfo.CountryID));
                aParamList.Add(new SqlParameter("@EntryDate", System.DateTime.Now));



                string qString = @"INSERT INTO tblTrainingOrgInfo (TrainingOrgName, OrgTypeId, OrgAddress, IsForeign, IsLocal, IsInHouse, VendorAudit, Remarks, OrgProfile, Clients, ClientsRecommendation, LogisticsFacility, Affiliation,
                    RegistrationYear, EntryBy, EntryDate, HasTin,HasVat,HasTradeLicense,HasBankSolv,Others , ContactPerson , ContactPersonCell ,ContactPersonEmail , CountryID , CompanyId )
            VALUES (@TrainingOrgName, @OrgTypeId, @OrgAddress, @IsForeign, @IsLocal , @IsInHouse , @VendorAudit, @Remarks, @OrgProfile, @Clients, @ClientsRecommendation, @LogisticsFacility, @Affiliation, 
                         @RegistrationYear, @EntryBy, @EntryDate,@HasTin,@HasVat,@HasTradeLicense,@HasBankSolv,@Others,@ContactPerson , @ContactPersonCell, @ContactPersonEmail , @CountryID , @CompanyId)";
                result = _aCommonInternalDal.SaveDataByInsertCommandById(qString, aParamList, DataBase.H);
                return result;
            }
            catch (Exception exception)
            {

                throw exception;
            }
            finally
            {

            }
        }


        public bool SaveTrainer(List<TrainerInfo> list)
        {
            try
            {
                bool result = false;
                foreach (var item in list)
                {

                    List<SqlParameter> aParamList = new List<SqlParameter>();

                    aParamList.Add(new SqlParameter("@TrainerName", item.TrainerName));
                    aParamList.Add(new SqlParameter("@TrainingOrgId", item.TrainingOrgId));
                    string qString = @"Insert into tblTrainerInfo (TrainingOrgId, TrainerName) Values(@TrainingOrgId, @TrainerName)";

                    result = _aCommonInternalDal.SaveDataByInsertCommand(qString, aParamList, DataBase.H);
                    if (result == false)
                    {
                        break;
                    }
                }
                return result;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool SaveBrunch(List<OfficeBranch> list)
        {
            try
            {
                bool result = false;
                foreach (var item in list)
                {

                    List<SqlParameter> aParamList = new List<SqlParameter>();

                    aParamList.Add(new SqlParameter("@BranchDetails", item.BranchDetails));
                    aParamList.Add(new SqlParameter("@TrainingOrgId", item.TrainingOrgId));
                    aParamList.Add(new SqlParameter("@BranchAddress", item.BranchAddress));
                    aParamList.Add(new SqlParameter("@CountryID", item.CountryID));
                    string qString = @"Insert into tblOfficeBranch (TrainingOrgId, BranchDetails , BranchAddress , CountryID) Values(@TrainingOrgId, @BranchDetails ,@BranchAddress , @CountryID)";

                    result = _aCommonInternalDal.SaveDataByInsertCommand(qString, aParamList, DataBase.H);
                    if (result == false)
                    {
                        break;
                    }
                }
                return result;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetOrganizationList(string param)
        {
            try
            {
                string query = @"SELECT  * , CASE WHEN a.IsForeign =1 THEN 'Foreign' WHEN a.IsLocal=1 then'Local' ELSE 'Inhouse' END AS Origin
                    FROM    tblTrainingOrgInfo A
                    LEFT JOIN tblOrganizationType B ON A.OrgTypeId = B.OrgTypeId
                    Left join tblCompanyInfo C on a.CompanyId = c.CompanyId
                    WHERE   IsDelete IS NULL
                    OR IsDelete = 0 " + param;
                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.H);
                return dt;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public DataTable GetFinList(string param)
        {
            try
            {
                string query = @"SELECT * from  tblFinancialYear A " + param;
                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.H);
                return dt;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool DeleteOrgInfo(int id, int user)
        {
            bool result = false;
            List<SqlParameter> aperam = new List<SqlParameter>();

            aperam.Add(new SqlParameter("@TrainingOrgId", id));
            aperam.Add(new SqlParameter("@DeleteBy", user));
            aperam.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
            string query = @"update tblTrainingOrgInfo set  IsDelete=1 , DeleteBy = @DeleteBy ,DeleteDate=@DeleteDate where TrainingOrgId = @TrainingOrgId ";


            result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, aperam, DataBase.HRDB);
            return result;

        }

        public DataTable GetOrganizationById(int id)
        {

            string query = @"Select * from tblTrainingOrgInfo where TrainingOrgId=" + id + " ";
            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.H);
            return dt;

        }
        public DataTable GetLeadTrainerByOrgId(int id)
        {
            string query = @"Select * from tblTrainerInfo where  TrainingOrgId=" + id + " ";
            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.H);
            return dt;

        }
        public DataTable GetBrunchByOrgId(int id)
        {
            string query = @"Select * from tblOfficeBranch where  TrainingOrgId=" + id + " ";
            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.H);
            return dt;

        }


        public bool UpdateTrainingOrganization(TrainingOrgInfo aInfo, int user)
        {

            List<SqlParameter> aParamList = new List<SqlParameter>();

            aParamList.Add(new SqlParameter("@TrainingOrgId", aInfo.TrainingOrgId));
            aParamList.Add(new SqlParameter("@TrainingOrgName", aInfo.TrainingOrgName));
            aParamList.Add(new SqlParameter("@OrgTypeId", aInfo.OrgTypeId));
            aParamList.Add(new SqlParameter("@OrgAddress", aInfo.OrgAddress));
            aParamList.Add(new SqlParameter("@IsForeign", aInfo.IsForeign));
            aParamList.Add(new SqlParameter("@IsLocal", aInfo.IsLocal));
            aParamList.Add(new SqlParameter("@IsInHouse", aInfo.IsInHouse));


            aParamList.Add(new SqlParameter("@VendorAudit", aInfo.VendorAudit));
            aParamList.Add(new SqlParameter("@Remarks", aInfo.Remarks));
            aParamList.Add(new SqlParameter("@OrgProfile", aInfo.OrgProfile));
            aParamList.Add(new SqlParameter("@Clients", aInfo.Clients));
            aParamList.Add(new SqlParameter("@ClientsRecommendation", aInfo.ClientsRecommendation));
            aParamList.Add(new SqlParameter("@LogisticsFacility", aInfo.LogisticsFacility));
            aParamList.Add(new SqlParameter("@Affiliation", aInfo.Affiliation));
            aParamList.Add(new SqlParameter("@RegistrationYear", aInfo.RegistrationYear));
            aParamList.Add(new SqlParameter("@HasTin", aInfo.HasTin));
            aParamList.Add(new SqlParameter("@HasVat", aInfo.HasVat));
            aParamList.Add(new SqlParameter("@HasTradeLicense", aInfo.HasTradeLicense));
            aParamList.Add(new SqlParameter("@HasBankSolv", aInfo.HasBankSolv));
            aParamList.Add(new SqlParameter("@Others", aInfo.Others));
            aParamList.Add(new SqlParameter("@EntryBy", user));
            aParamList.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
            aParamList.Add(new SqlParameter("@ContactPerson", aInfo.ContactPerson));
            aParamList.Add(new SqlParameter("@ContactPersonCell", aInfo.ContactPersonCell));
            aParamList.Add(new SqlParameter("@ContactPersonEmail", aInfo.ContactPersonEmail));
            aParamList.Add(new SqlParameter("@CountryID", aInfo.CountryID));
            aParamList.Add(new SqlParameter("@CompanyId", aInfo.CompanyId));

            string query = @" update tblTrainingOrgInfo set TrainingOrgName =@TrainingOrgName, OrgTypeId=@OrgTypeId, OrgAddress=@OrgAddress,
                              IsForeign=@IsForeign, IsLocal=@IsLocal, IsInHouse=@IsInHouse,  CountryID = @CountryID,
                              VendorAudit=@VendorAudit, Remarks=@Remarks, OrgProfile=@OrgProfile, ContactPerson = @ContactPerson , ContactPersonCell = @ContactPersonCell , ContactPersonEmail = @ContactPersonEmail,
                              Clients=@Clients, ClientsRecommendation=@ClientsRecommendation, LogisticsFacility = @LogisticsFacility, Affiliation=@Affiliation, CompanyId = @CompanyId,
                              RegistrationYear=@RegistrationYear, HasTin=@HasTin,HasVat=@HasVat,HasTradeLicense=@HasTradeLicense,HasBankSolv=@HasBankSolv,Others=@Others  where TrainingOrgId =@TrainingOrgId  ";

            return _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParamList, DataBase.H);
        }


        public bool UpdateTrainnerInfo(List<TrainerInfo> list)
        {
            try
            {
                bool result = false;

                string delString = "Delete From tblTrainerInfo where TrainingOrgId =" + list.First().TrainingOrgId + "";
                _aCommonInternalDal.DeleteDataByDeleteCommand(delString, DataBase.H);


                result = list.Count > 0 ? SaveTrainer(list) : true;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool UpdateBranchInfo(List<OfficeBranch> list)
        {
            try
            {
                bool result = false;

                string delString = "Delete From tblOfficeBranch where TrainingOrgId =" + list.First().TrainingOrgId + "";
                _aCommonInternalDal.DeleteDataByDeleteCommand(delString, DataBase.H);
                result = list.Count > 0 ? SaveBrunch(list) : true;
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region TrainingSetup

        public DataTable GetEmployeeMarksInfo(string generateParameter)
        {
            string query = @"SELECT CI.CompanyName,FNY.FinancialYearDesc,TDM.TrainingTitle,EGI.EmpName,DSG.Designation,DPT.DepartmentName,
                             MRKSM.OutOfMark,MRKD.PreMark,MRKD.PostMark,MRKSM.Remarks FROM dbo.tblTrainingMarksMaster AS MRKSM  WITH (NOLOCK) 
                             LEFT JOIN dbo.tblTrainingMarkDetail AS MRKD ON MRKD.TrainigMarkId = MRKSM.TrainigMarkId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = MRKD.EmpInfoId
                             LEFT JOIN dbo.tblDesignation AS DSG ON DSG.DesignationId = EGI.DesignationId
                             LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblTrainingRecordMaster AS TDM ON MRKSM.TrainingRecordMasterId = TDM.TrainingRecordMasterId
                             LEFT JOIN dbo.tblFinancialYear AS FNY ON FNY.FinancialYearId = TDM.FinancialYearId
                             LEFT JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TDM.CompanyId " + generateParameter;
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetFianncialYearByComIdDDl(int id)
        {
            string query = @"SELECT FinancialYearId as Value,FinancialYearDesc as TextField FROM tblFinancialYear WITH (NOLOCK) where CompanyId =" + id + " and Status ='Active'  ";
            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);


        }

            public DataTable GetFianncialYearByComIdChkList(int id)
        {
            string query = @"SELECT * FROM(SELECT  FinancialYearId as Value,FinancialYearDesc as TextField,*  FROM dbo.tblFinancialYear WHERE  CompanyId=" + id + @" and Status ='Active' 
UNION ALL  SELECT  FinancialYearId as Value,FinancialYearDesc as TextField,*  FROM dbo.tblFinancialYearNew  WHERE  CompanyId=" + id + @" and Status ='Active' ) tbl   ORDER BY FinancialYearDesc
";
            return _aCommonInternalDal.GetDTforDDLCheckList(query, null, DataBase.HRDB);


        }

            public DataTable GetEmpByDesignation(int comId, int DesigId)
            {
                string query = @"SELECT e.EmpInfoId Value,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ISNULL( ' : ' + d.Designation,'')  + ISNULL(' : ' + dept.DepartmentName, '')  AS TextField 
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL AND e.IsActive=1 AND e.CompanyId=" + comId + " AND e.DesignationId=" + DesigId;
                return _aCommonInternalDal.GetDTforDDLCheckList(query, null, DataBase.HRDB);


            }
        
        public DataTable GetJobDescInfo(int empId)
        {
            string query = @"SELECT  1 IsActive, JobReqKeyResName AS JdDetailsInfo FROM dbo.tblJobCreation
LEFT JOIN dbo.tblJobReqKeyRespon ON dbo.tblJobCreation.ReqCodeId=dbo.tblJobReqKeyRespon.JobReqFormId
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.JobID=dbo.tblJobCreation.JobID WHERE EmpInfoId='" +empId+"'";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);


        }

        public DataTable GetExistingJobDescInfo(int empId)
        {
            string query = @"SELECT  d.Active IsActive, * FROM dbo.tblJdMaster M
INNER JOIN dbo.tblJdDetails D ON M.JdMasterId=D.JdMasterId WHERE EmpInfoId='" + empId + "'";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);


        }

        public FinancialYear GetFinancialYear(int id)
        {
            string query = @"SELECT * from tblFinancialYear where FinancialYearId = " + id + "";

            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            dt.Columns.RemoveAt(7);
            FinancialYear aYear = new FinancialYear();
            aYear.StartDate = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
            aYear.EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString());
            aYear.FinancialYearId = Convert.ToInt32(dt.Rows[0]["FinancialYearId"].ToString());
            return aYear;
        }


        public List<YearQuater> GetQuater(DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<SqlParameter> aperam = new List<SqlParameter>();
                aperam.Add(new SqlParameter("@StartDate", fromDate));
                aperam.Add(new SqlParameter("@EndDate", toDate));
                aAcessManager.SqlConnectionOpen(DataBase.HRDB);
                SqlDataReader dr = aAcessManager.GetSqlDataReader("sp_GetQuaterByYear", aperam);
                List<YearQuater> alist = new List<YearQuater>();
                while (dr.Read())
                {
                    YearQuater aInfo = new YearQuater();
                    aInfo.YearRange = dr["YearRange"].ToString();
                    aInfo.QuarterNum = dr["QuarterNum"].ToString();
                    aInfo.QuarterDetails = dr["QuarterDetails"].ToString();
                    alist.Add(aInfo);
                }
                return alist;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                aAcessManager.SqlConnectionClose();
            }
        }

        public DataTable GetQuaterNew(int comId)
        {
            try
            {
                string query =
                    @"Select QuarterId as Value,QuarterName as TextField  from tblQuarterInfo where CompanyId=" + comId +
                    " and IsActive = 1";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                    
                throw ex;
            }
        }
        public DataTable GetTotalBudgetCostSum(string comId,string finId)
        {
            try
            {
                string query =
                    @"SELECT SUM(TotalBudget)TYBC FROM dbo.tblTrainingBudget2Master

 WHERE CompanyId='" + comId + "' AND FinancialYearId='" + finId + "' AND (tblTrainingBudget2Master.IsDelete IS NULL OR tblTrainingBudget2Master.IsDelete=0)";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetGrade(string EmpCatId)
        {
            try
            {
                string query =
                    @"select * from tblSalaryGrade WHERE EmpCategoryId=" + EmpCatId + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetGradeAll()
        {
            try
            {
                string query =
                    @"select * from tblSalaryGrade";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetMonthByQuater(int quaterId)
        {

            try
            {
                string query = @"Select QuarterMonthId as Value , MonthName as TextField from tblQuarterMonthInfo where IsActive = 1 and QuarterId = " + quaterId + " ";

                return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTrainingOrgDDl()
        {

            string query = @"SELECT TrainingOrgId as Value,TrainingOrgName as TextField FROM tblTrainingOrgInfo   where (IsDelete IS NULL
                                      OR IsDelete = 0
                                    )";
            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        }

        public DataTable GetTrainnerDDl(int orgId)
        {

            string query = @"SELECT TrainerId as Value,TrainerName as TextField FROM tblTrainerInfo where  TrainerName!='' and TrainingOrgId = " + orgId + " ";
            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        }

        public DataTable GetOrgBranch(int id)
        {
            string query = @"SELECT OfficeBranchId as Value,BranchDetails as TextField FROM tblOfficeBranch where BranchDetails!='' and TrainingOrgId = " + id + " ";
            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        }

        public DataTable GetTrainnerInfo(int id)
        {

            string query = @"Select  a.TrainerId , a.TrainerName , B.TrainingOrgName as TrainerDetails from tblTrainerInfo A left join  tblTrainingOrgInfo b on a.TrainingOrgId=b.TrainingOrgId  where  a.TrainerId =" + id + "";
            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        }


        public int SaveTrainingSetupMaster(TrainingMasterInfo aMaster, string user)
        {





            int result = 0;
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                param.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                param.Add(new SqlParameter("@TrainingBudgetAllocationId", aMaster.TrainingBudgetAllocationId));
                param.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                param.Add(new SqlParameter("@TrainingDetails", aMaster.TrainingDetails));
                param.Add(new SqlParameter("@OfficeBranchId", aMaster.OfficeBranchId));
                param.Add(new SqlParameter("@Quater", aMaster.Quater));
      
                param.Add(new SqlParameter("@TrainingOrgId", aMaster.TrainingOrgId));
                param.Add(new SqlParameter("@TrainingStart", aMaster.TrainingStart));
                param.Add(new SqlParameter("@TrainingEnd", aMaster.TrainingEnd));
                param.Add(new SqlParameter("@TrainingDuration", aMaster.TrainingDuration));
                param.Add(new SqlParameter("@TrainingEvaluation", aMaster.TrainingEvaluation));
                param.Add(new SqlParameter("@TrainingSetupNumber", "TS:"+GenerateAutoNumber("tblTrainingMaster","TrainingSetupNumber",System.DateTime.Now)));
                param.Add(new SqlParameter("@EntryBy", user));
                param.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                param.Add(new SqlParameter("@TrainingRequisitionMasterId", aMaster.TrainingRequisitionMasterId));

                
               // GenerateAutoNumber(string table, string column, DateTime date)

                string query = @"insert into tblTrainingMaster (CompanyId, FinancialYearId, TrainingTitle, TrainingDetails, OfficeBranchId
                            , TrainingOrgId, TrainingStart, TrainingEnd, TrainingDuration, TrainingEvaluation, EntryBy, EntryDate,TrainingBudgetAllocationId,TrainingSetupNumber,TrainingRequisitionMasterId,Quater) 
                            values(@CompanyId, @FinancialYearId, @TrainingTitle, @TrainingDetails, @OfficeBranchId,
                            @TrainingOrgId, @TrainingStart, @TrainingEnd, @TrainingDuration, @TrainingEvaluation, @EntryBy, @EntryDate,@TrainingBudgetAllocationId,@TrainingSetupNumber,@TrainingRequisitionMasterId,@Quater)";

                result = _aCommonInternalDal.SaveDataByInsertCommandById(query, param, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public bool SaveTrainingDetails(List<TrainingDetailsInfo> aList, int masterId)
        {
            bool result = false;
            try
            {

                string delQuery = @"Delete from tblTrainingDetails  where TrainingMasterId = " + masterId + " ";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQuery, DataBase.HRDB);
                foreach (var item in aList)
                {


                    if (item.TrainerId == 0)
                    {
                        List<SqlParameter> aParam = new List<SqlParameter>();
                        aParam.Add(new SqlParameter("@TrainerId", item.TrainerId));
                        aParam.Add(new SqlParameter("@NotListedName", item.NotListedName));
                        aParam.Add(new SqlParameter("@NotListedDetails", item.NotListedDetails));
                        aParam.Add(new SqlParameter("@TrainingMasterId", masterId));
                        string query = @"Insert into tblTrainingDetails (TrainingMasterId, TrainerId, NotListedName, NotListedDetails) values (@TrainingMasterId, @TrainerId, @NotListedName, @NotListedDetails)";
                        result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParam, DataBase.HRDB);
                    }
                    else
                    {
                        List<SqlParameter> aParam = new List<SqlParameter>();
                        aParam.Add(new SqlParameter("@TrainerId", item.TrainerId));

                        aParam.Add(new SqlParameter("@TrainingMasterId", masterId));
                        string query = @"Insert into tblTrainingDetails (TrainingMasterId, TrainerId) values (@TrainingMasterId, @TrainerId)";
                        result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParam, DataBase.HRDB);
                    }

                    if (result == false)
                    {
                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool UpdateAllocationDetails(List<TrainingBudgetAllocationDetails> aList , bool fromRequisition = false )
        {
            try
            {

                bool resilt = false;
                if (fromRequisition == true)
                {
                    foreach (var item in aList)
                    {
                        List<SqlParameter> aPerams2 = new List<SqlParameter>();

                        aPerams2.Add(new SqlParameter("@TrainingBudgetAllocationId", item.TrainingBudgetAllocationId));
                        aPerams2.Add(new SqlParameter("@TrainingBudgetAllocationDetailsId", item.TrainingBudgetAllocationDetailsId));
                        aPerams2.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                        aPerams2.Add(new SqlParameter("@IsActive", item.IsActive));


                        string query2 = @"Update tblTrainingRequisitionDetails set IsActive =@IsActive where TrainingRequisitionDetailsId = @TrainingBudgetAllocationDetailsId and  EmpInfoId=@EmpInfoId ";
                        resilt = _aCommonInternalDal.UpdateDataByUpdateCommand(query2, aPerams2, DataBase.HRDB);

                        if (resilt == false)
                        {
                            return resilt;
                        }

                    }
                }
                else
                {
                    foreach (var item in aList)
                    {
                        List<SqlParameter> aPerams = new List<SqlParameter>();

                        aPerams.Add(new SqlParameter("@TrainingBudgetAllocationId", item.TrainingBudgetAllocationId));
                        aPerams.Add(new SqlParameter("@TrainingBudgetAllocationDetailsId", item.TrainingBudgetAllocationDetailsId));
                        aPerams.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                        aPerams.Add(new SqlParameter("@IsActive", item.IsActive));


                        string query = @"Update tblTrainingBudgetAllocationDetails set IsActive =@IsActive where TrainingBudgetAllocationId = @TrainingBudgetAllocationId and  TrainingBudgetAllocationDetailsId=@TrainingBudgetAllocationDetailsId ";
                        resilt = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aPerams, DataBase.HRDB);

                        if (resilt == false)
                        {
                            return resilt;
                        }

                    }
                }
                return resilt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public bool DeleteTrainingSetup(int id, string user)
        {
            try
            {
                bool result = false;
                List<SqlParameter> aperam = new List<SqlParameter>();

                aperam.Add(new SqlParameter("@TrainingMasterId", id));
                aperam.Add(new SqlParameter("@DeleteBy", user));
                aperam.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
                string query = @"update tblTrainingMaster set  IsDelete=1 , DeleteBy = @DeleteBy ,DeleteDate=@DeleteDate where TrainingMasterId = @TrainingMasterId ";


                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, aperam, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
//        public DataTable GetTrainingList()
//        {
//            string query = @"select a.TrainingSetupNumber,case when ta.ForDepartment=1 then 'Department' when ta.ForGrade=1 then 'Grade' when ta.ForEmployee=1 then 'Employee' else 'Requisition:Employee' end as SpecifcFor ,
//q.QuarterName as quater,
//
// a.*,b.TrainingOrgName , c.FinancialYearDesc , d.ShortName from tblTrainingMaster a 
//			
//                Left join tblTrainingOrgInfo b on a.TrainingOrgId = b.TrainingOrgId 
//                left join tblFinancialYear c on a.FinancialYearId = c.FinancialYearId
//                left join tblCompanyInfo d on a.CompanyId = d.CompanyId
//				left join tblQuarterInfo q on a.Quater = q.QuarterId
//				left join tblTrainingBudgetAllocationMaster ta on a.TrainingBudgetAllocationId = ta.TrainingBudgetAllocationId where a.IsDelete is null or a.IsDelete=0";

//            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//            return dt;
//        }


        public DataTable GetTrainingById(int id)
        {
            string query = @"select * from tblTrainingMaster where TrainingMasterId = " + id + "";

            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            return dt;
        }

        public DataTable GetTrainingDetails(int id)
        {

            string query = @"select A.TrainerId, Case when B.TrainerName is null then A.NotListedName else B.TrainerName end as TrainerName  ,
                            Case when c.TrainingOrgName is null then A.NotListedDetails else  c.TrainingOrgName end as TrainerDetails
                            from tblTrainingDetails A 
                            left join tblTrainerInfo B on a.TrainerId = b.TrainerId
                            left join tblTrainingOrgInfo c on b.TrainingOrgId = c.TrainingOrgId where A.TrainingMasterId = " + id + "";

            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            return dt;

        }


        public bool UpdatTrainingMaster(TrainingMasterInfo aMaster, string user)
        {

            bool result = false;
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@TrainingMasterId", aMaster.TrainingMasterId));
                param.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                param.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                param.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                param.Add(new SqlParameter("@TrainingDetails", aMaster.TrainingDetails));
                param.Add(new SqlParameter("@OfficeBranchId", aMaster.OfficeBranchId));
                param.Add(new SqlParameter("@Quater", aMaster.Quater));
                param.Add(new SqlParameter("@TrainingOrgId", aMaster.TrainingOrgId));
                param.Add(new SqlParameter("@TrainingStart", aMaster.TrainingStart));
                param.Add(new SqlParameter("@TrainingEnd", aMaster.TrainingEnd));
                param.Add(new SqlParameter("@TrainingDuration", aMaster.TrainingDuration));
                param.Add(new SqlParameter("@TrainingEvaluation", aMaster.TrainingEvaluation));
                param.Add(new SqlParameter("@TrainingBudgetAllocationId", aMaster.TrainingBudgetAllocationId));
                param.Add(new SqlParameter("@UpdateBy", user));
                param.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                param.Add(new SqlParameter("@TrainingRequisitionMasterId", aMaster.TrainingRequisitionMasterId));

                string query = @"update tblTrainingMaster set CompanyId = @CompanyId, FinancialYearId=@FinancialYearId, TrainingTitle=@TrainingTitle, 
                                TrainingDetails=@TrainingDetails, OfficeBranchId=@OfficeBranchId,  TrainingOrgId=@TrainingOrgId, 
                                TrainingStart=@TrainingStart, TrainingEnd=@TrainingEnd, TrainingDuration=@TrainingDuration, TrainingEvaluation=@TrainingEvaluation,TrainingRequisitionMasterId = @TrainingRequisitionMasterId,  
                                UpdateBy=@UpdateBy, UpdateDate =@UpdateDate ,TrainingBudgetAllocationId=@TrainingBudgetAllocationId ,Quater=@Quater   where TrainingMasterId = @TrainingMasterId ";
                result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, param, DataBase.HRDB);
                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public bool UpdateTrainingDetails(List<TrainingDetailsInfo> aList, int masterId)
        {
            bool result = false;
            try
            {
                string query = @"Delete from tblTrainingDetails where TrainingMasterId = " + masterId + " ";

                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, DataBase.HRDB);
                if (result == true)
                {
                    result = SaveTrainingDetails(aList, masterId);
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public DataTable GetTrainingBudget(int year , string quater)
        {
            string query = @"select a.BudgetAllocationNumber+':'+b.TrainingTitle as TextField , a.TrainingBudgetAllocationId as value from tblTrainingBudgetAllocationMaster A 
                    left join tblTrainingBudgetMaster B on a.TrainingBudgetMasterId = b.TrainingBudgetMasterId 
                    where a.FinancialYearId = " + year + " and  a.Quater = '" + quater + "'";
            return _aCommonInternalDal.GetDTforDDL(query, null,DataBase.HRDB);
        }



        public DataTable GetTrainingRequistion(int year, string quater)
        {
            string query = @"select  TrainingRequisitionMasterId as value, TrainingReqNumber+':'+TrainingTitle as TextField from tblTrainingRequisitionMaster  where FinancialYearId = " + year + " and Quater ='" + quater + "' and  TrainingRequisitionMasterId not in (SELECT TrainingRequisitionMasterId FROM dbo.tblTrainingMaster ) ";
            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        } 
        #endregion


        #region Training Budget

        public DataTable GetTrainingTitle(int comId, int finYearId)
        {

            string query = @"select TrainingMasterId as Value ,  TrainingTitle  as TextField from tblTrainingMaster where CompanyId = " + comId + "  and FinancialYearId = " + finYearId + " ";

            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        }


        public DataTable GetDepartmentddl()
        {
            string query = @"Select   DepartmentId as Value , DepartmentName as TextField from tblDepartment where IsActive=1";
            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        }

        public DataTable GetGradeForddl()
        {
            string query = @"Select   GradeId as Value , GradeName as TextField from tblEmployeeGrade ";
            return _aCommonInternalDal.GetDTforDDL(query, null,DataBase.HRDB);
        }
        public DataTable GetUserbycompany(string prefixText, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SearchText", prefixText));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT CAST(us.UserId AS NVARCHAR(50))+':'+us.UserName+ ISNULL(' : '+emp.EmpName,'') UserName  FROM tblUser us WITH (NOLOCK)
                                      left JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = us.EmpInfoId
                                       WHERE us.IsActive=1 AND emp.CompanyId=@CompanyId AND UserName LIKE '%'+@SearchText+'%'";

            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public string[] GetEmployee(string peram , string company)
        {

            string[] temp = null;
            List<SqlParameter> aPe = new List<SqlParameter>();
            aPe.Add(new SqlParameter("@peram", peram));
            string query = @"select A.EmpMasterCode+' : '+A.EmpName+' : '+LTRIM(RTRIM(Desg.Designation)) +' : '+b.DepartmentName as Employee from tblEmpGeneralInfo A 
                            left join tblDepartment b on a.DepartmentId = b.DepartmentId
                            left join tblDesignation desg on a.DesignationId = desg.DesignationId
                            where a.CompanyId = "+Convert.ToInt32(company)+" and   A.EmpMasterCode like '%" + peram + "%' or  A.EmpName like  '%" + peram + "%' or Desg.Designation like '%" + peram + "%' or DepartmentName like '%" + peram + "%' ";

            //  DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            var dr = _aCommonInternalDal.DataContainerDataReader(query, aPe, DataBase.HRDB);

            List<string> a = new List<string>();
            while (dr.Read())
            {

                string astring = dr["Employee"].ToString();
                a.Add(astring);
            }
            return a.ToArray();

        }


        public string[] GetEmployeeAuto2(string peram)
        {

            string[] temp = null;
            List<SqlParameter> aPe = new List<SqlParameter>();
            aPe.Add(new SqlParameter("@peram", peram));
            string query = @"select A.EmpMasterCode+' : '+A.EmpName+' : '+LTRIM(RTRIM(Desg.Designation)) +' : '+b.DepartmentName as Employee from tblEmpGeneralInfo A 
                            left join tblDepartment b on a.DepartmentId = b.DepartmentId
                            left join tblDesignation desg on a.DesignationId = desg.DesignationId
                            where    A.EmpMasterCode like '%" + peram + "%' or  A.EmpName like  '%" + peram + "%' or Desg.Designation like '%" + peram + "%' or DepartmentName like '%" + peram + "%' ";

            //  DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            var dr = _aCommonInternalDal.DataContainerDataReader(query, aPe, DataBase.HRDB);

            List<string> a = new List<string>();
            while (dr.Read())
            {

                string astring = dr["Employee"].ToString();
                a.Add(astring);
            }
            return a.ToArray();

        }


        public int GetEmployeeIdByCode(string empCode)
        {

            int EmpInfoId = 0;
            string query = "Select * from tblEmpGeneralInfo where EmpMasterCode = '" + empCode + "'";

            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            if (dt.Rows.Count > 0)
            {
                EmpInfoId = dt.Rows[0]["EmpInfoId"].ToString() == null ? 0 : Convert.ToInt32(dt.Rows[0]["EmpInfoId"].ToString());
            }
            return EmpInfoId;

        }

        public int SaveTraininBidgetMaster(TrainingBudgetMaster aMaster, string user)
        {
            try
            {

                int result = 0;

                if (aMaster.TrainingBudgetMasterId > 0)
                {

                    bool? isApprove = null;
                    List<SqlParameter> aPeramList = new List<SqlParameter>();
                    aPeramList.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aPeramList.Add(new SqlParameter("@TrainingBudgetMasterId", aMaster.TrainingBudgetMasterId));
                    aPeramList.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aPeramList.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                    aPeramList.Add(new SqlParameter("@TrainingResult", aMaster.TrainingResult));
                    aPeramList.Add(new SqlParameter("@TotalParticipant", aMaster.TotalParticipant));
                    aPeramList.Add(new SqlParameter("@Duration", aMaster.Duration));
                    aPeramList.Add(new SqlParameter("@IsExternal", aMaster.IsExternal));
                    aPeramList.Add(new SqlParameter("@IsInternal", aMaster.IsInternal));
                    aPeramList.Add(new SqlParameter("@IsLocal", aMaster.IsLocal));
                    aPeramList.Add(new SqlParameter("@IsForeign", aMaster.IsForeign));
                    aPeramList.Add(new SqlParameter("@CostParticipant", aMaster.CostParticipant));
                    aPeramList.Add(new SqlParameter("@Budget", aMaster.Budget));
                    aPeramList.Add(new SqlParameter("@Referance", aMaster.Referance));
                    aPeramList.Add(new SqlParameter("@Remarks", aMaster.Remarks));
                    aPeramList.Add(new SqlParameter("@ForDepartment", aMaster.ForDepartment));
                    aPeramList.Add(new SqlParameter("@ForGrade", aMaster.ForGrade));
                    aPeramList.Add(new SqlParameter("@ForEmployee", aMaster.ForEmployee));
                    // aPeramList.Add(new SqlParameter("@IsApprove", isApprove));

                    aPeramList.Add(new SqlParameter("@UpdateBy", user));
                    aPeramList.Add(new SqlParameter("@ApprovalStatus", "Pending".ToString()));
                    aPeramList.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    bool isUpdate = false;
                    string query = @"Update tblTrainingBudgetMaster set   CompanyId = @CompanyId, TrainingTitle = @TrainingTitle, TrainingResult = @TrainingResult, TotalParticipant = @TotalParticipant, Duration=@Duration, 
                                    IsExternal=@IsExternal, IsInternal=@IsInternal, IsLocal=@IsLocal, IsForeign=@IsForeign, CostParticipant=@CostParticipant, Budget=@Budget, Referance=@Referance, Remarks=@Remarks, ForDepartment=@ForDepartment, 
                                    ForGrade=@ForGrade, ForEmployee=@ForEmployee, UpdateBy=@UpdateBy, UpdateDate=@UpdateDate , ApprovalStatus = @ApprovalStatus,IsApprove=NULL, FinancialYearId=@FinancialYearId  where TrainingBudgetMasterId = @TrainingBudgetMasterId";
                    isUpdate = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aPeramList, DataBase.HRDB);

                    if (isUpdate == true)
                    {
                        result = aMaster.TrainingBudgetMasterId;
                    }
                }

                else
                {


                    List<SqlParameter> aPeramList = new List<SqlParameter>();
                    aPeramList.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aPeramList.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                    aPeramList.Add(new SqlParameter("@TrainingResult", aMaster.TrainingResult));
                    aPeramList.Add(new SqlParameter("@TotalParticipant", aMaster.TotalParticipant));
                    aPeramList.Add(new SqlParameter("@Duration", aMaster.Duration));
                    aPeramList.Add(new SqlParameter("@IsExternal", aMaster.IsExternal));
                    aPeramList.Add(new SqlParameter("@IsInternal", aMaster.IsInternal));
                    aPeramList.Add(new SqlParameter("@IsLocal", aMaster.IsLocal));
                    aPeramList.Add(new SqlParameter("@IsForeign", aMaster.IsForeign));
                    aPeramList.Add(new SqlParameter("@CostParticipant", aMaster.CostParticipant));
                    aPeramList.Add(new SqlParameter("@Budget", aMaster.Budget));
                    aPeramList.Add(new SqlParameter("@Referance", aMaster.Referance));
                    aPeramList.Add(new SqlParameter("@Remarks", aMaster.Remarks));
                    aPeramList.Add(new SqlParameter("@ForDepartment", aMaster.ForDepartment));
                    aPeramList.Add(new SqlParameter("@ForGrade", aMaster.ForGrade));
                    aPeramList.Add(new SqlParameter("@ForEmployee", aMaster.ForEmployee));
                    aPeramList.Add(new SqlParameter("@EntryBy", user));
                    aPeramList.Add(new SqlParameter("@ApprovalStatus", "Pending"));
                    aPeramList.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aPeramList.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aPeramList.Add(new SqlParameter("@TrainingBudgetNumber", "TB:" + GenerateAutoNumber("tblTrainingBudgetMaster", "TrainingBudgetNumber", System.DateTime.Now)));
                    

                    string query = @"Insert into tblTrainingBudgetMaster ( CompanyId, TrainingTitle, TrainingResult, TotalParticipant, Duration, IsExternal, IsInternal, IsLocal, IsForeign, CostParticipant, Budget, Referance, Remarks, ForDepartment, 
                         ForGrade, ForEmployee, EntryBy, EntryDate , ApprovalStatus,FinancialYearId, TrainingBudgetNumber) 
                        values (@CompanyId, @TrainingTitle, @TrainingResult, @TotalParticipant, @Duration, @IsExternal, @IsInternal, @IsLocal, @IsForeign, @CostParticipant, @Budget, @Referance, @Remarks, @ForDepartment, 
                         @ForGrade, @ForEmployee, @EntryBy, @EntryDate , @ApprovalStatus, @FinancialYearId, @TrainingBudgetNumber)";

                    result = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPeramList, DataBase.HRDB);
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public bool SaveTrainingBudgetDpt(List<TrainingBudgetDetailsDpt> adptList, int masterId)
        {
            try
            {
                bool result = false;



                foreach (var item in adptList)
                {
                    int resultInt = 0;
                    List<SqlParameter> aPeramList = new List<SqlParameter>();
                    aPeramList.Add(new SqlParameter("@TrainingBudgetMasterId", item.TrainingBudgetMasterId));
                    aPeramList.Add(new SqlParameter("@DepartmentId", item.DepartmentId));
                    aPeramList.Add(new SqlParameter("@Qty", item.Qty));
                    aPeramList.Add(new SqlParameter("@FinancialYearId", item.FinancialYearId));
                    aPeramList.Add(new SqlParameter("@Quater", item.Quater));
                    aPeramList.Add(new SqlParameter("@TrainingMonth", item.TrainingMonth));
                    aPeramList.Add(new SqlParameter("@FromDate", item.FromDate));
                    aPeramList.Add(new SqlParameter("@ToDate", item.ToDate));

                    string query = @"insert into tblTrainingBudgetDetailsDpt (TrainingBudgetMasterId, DepartmentId, FinancialYearId, Qty, Quater, TrainingMonth, FromDate, ToDate) 
                                    values(@TrainingBudgetMasterId, @DepartmentId, @FinancialYearId, @Qty, @Quater, @TrainingMonth, @FromDate, @ToDate)";

                    resultInt = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPeramList, DataBase.HRDB);
                    if (resultInt > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    if (result == false)
                    {
                        break;
                    }

                }

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool DeleteTrainingDetailsDptByMaster(int id)
        {
            try
            {
                bool result = false;
                string query = @"Delete from tblTrainingBudgetDetailsDpt where TrainingBudgetMasterId = " + id + "";
                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool DeleteTrainingDetailsGrdByMaster(int id)
        {
            try
            {
                bool result = false;
                string query = @"Delete from tblTrainingBudgetDetailsGrade where TrainingBudgetMasterId = " + id + "";
                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public bool DeleteTrainingDetailsEmpByMaster(int id)
        {
            try
            {
                bool result = false;
                string query = @"Delete from tblTrainingBudgetDetailsEmployee where TrainingBudgetMasterId = " + id + "";
                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, DataBase.HRDB);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SaveTrainingBudgetGrade(List<TrainingBudgetDetailsGrade> adptList, int masterId)
        {
            try
            {
                bool result = false;

                foreach (var item in adptList)
                {
                    int resultInt = 0;
                    List<SqlParameter> aPeramList = new List<SqlParameter>();
                    aPeramList.Add(new SqlParameter("@TrainingBudgetMasterId", item.TrainingBudgetMasterId));
                    aPeramList.Add(new SqlParameter("@GradeId", item.GradeId));
                    aPeramList.Add(new SqlParameter("@Qty", item.Qty));
                    aPeramList.Add(new SqlParameter("@FinancialYearId", item.FinancialYearId));
                    aPeramList.Add(new SqlParameter("@Quater", item.Quater));
                    aPeramList.Add(new SqlParameter("@TrainingMonth", item.TrainingMonth));
                    aPeramList.Add(new SqlParameter("@FromDate", item.FromDate));
                    aPeramList.Add(new SqlParameter("@ToDate", item.ToDate));

                    string query = @"insert into tblTrainingBudgetDetailsGrade (TrainingBudgetMasterId, GradeId, FinancialYearId, Qty, Quater, TrainingMonth, FromDate, ToDate) 
                                    values(@TrainingBudgetMasterId, @GradeId, @FinancialYearId, @Qty, @Quater, @TrainingMonth, @FromDate, @ToDate)";

                    resultInt = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPeramList, DataBase.HRDB);
                    if (resultInt > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    if (result == false)
                    {
                        break;
                    }

                }

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public bool SaveTrainingBudgetEmployee(List<TrainingBudgetDetailsEmployee> adptList, int masterId)
        {
            try
            {
                bool result = false;

                foreach (var item in adptList)
                {
                    int resultInt = 0;
                    List<SqlParameter> aPeramList = new List<SqlParameter>();
                    aPeramList.Add(new SqlParameter("@TrainingBudgetMasterId", item.TrainingBudgetMasterId));
                    aPeramList.Add(new SqlParameter("@EmployeeId", item.EmployeeId));

                    aPeramList.Add(new SqlParameter("@FinancialYearId", item.FinancialYearId));
                    aPeramList.Add(new SqlParameter("@Quater", item.Quater));
                    aPeramList.Add(new SqlParameter("@TrainingMonth", item.TrainingMonth));
                    aPeramList.Add(new SqlParameter("@FromDate", item.FromDate));
                    aPeramList.Add(new SqlParameter("@ToDate", item.ToDate));

                    string query = @"insert into tblTrainingBudgetDetailsEmployee (TrainingBudgetMasterId, EmployeeId, FinancialYearId, Quater,  TrainingMonth, FromDate, ToDate) 
                                    values(@TrainingBudgetMasterId, @EmployeeId, @FinancialYearId,  @Quater, @TrainingMonth, @FromDate, @ToDate)";

                    resultInt = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPeramList, DataBase.HRDB);
                    if (resultInt > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                    if (result == false)
                    {
                        break;
                    }

                }

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public DataTable GetTrainingBudgetList()
        {
            try
            {
                string query = @"SELECT  a.TrainingBudgetMasterId,a.TrainingBudgetNumber,a.TrainingTitle , c.ShortName , a.ApprovalStatus ,fy.FinancialYearDesc,
                                  CASE WHEN A.ForDepartment = 1 THEN 'For Derartment '
                                       WHEN A.ForGrade = 1 THEN 'For Grade'
                                       ELSE 'For Employee'
                                  END AS ForSector ,
                                  CASE WHEN A.IsExternal = 1 THEN 'External'
                                       WHEN A.IsInternal = 1 THEN 'Internal'
                                                END AS ExOrIn ,
                                    CASE WHEN A.IsLocal = 1 THEN 'Local'
                                         WHEN A.IsForeign = 1 THEN 'Foreign'
                                    END AS LocalForeign
                            FROM    tblTrainingBudgetMaster A
                                    LEFT JOIN tblCompanyInfo C ON A.CompanyId = C.CompanyId
                            		left JOIN dbo.tblFinancialYear fy ON A.FinancialYearId = fy.FinancialYearId
                            WHERE   A.IsDelete IS NULL
                                OR A.IsDelete = 0   ";

                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetTrainingBudgetByMaster(int masterId)
        {

            try
            {
                string query = @"Select * from tblTrainingBudgetMaster where TrainingBudgetMasterId=" + masterId + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataTable GetTrainingDetailsDptByMaster(int id)
        {
            try
            {
                string query = @"SELECT  A.TrainingBudgetDetailsDptId ,
        A.DepartmentId AS departmentId ,
        A.FinancialYearId AS finYear ,
        A.Qty AS quantity ,
        A.Quater AS quaterId ,
		quater.QuarterName AS quater,
		
        A.TrainingMonth AS monthId ,
        qMonth.MonthName AS month ,
        CONVERT(NVARCHAR(11), A.FromDate, 106) AS fromDate ,
        CONVERT(NVARCHAR(11), A.ToDate, 106) AS toDate ,
        b.DepartmentName AS department
FROM    dbo.tblTrainingBudgetDetailsDpt A
        LEFT JOIN tblDepartment b ON A.DepartmentId = b.DepartmentId
		LEFT JOIN dbo.tblQuarterInfo quater ON a.Quater = quater.QuarterId
		LEFT JOIN dbo.tblQuarterMonthInfo qMonth ON a.TrainingMonth = qMonth.QuarterMonthId
WHERE   A.TrainingBudgetDetailsDptId NOT IN (
        SELECT  alocD.TrainingBudgetDetailsDptId
        FROM    dbo.tblTrainingBudgetAllocationDetails alocD
                LEFT JOIN dbo.tblTrainingBudgetAllocationMaster alocM ON alocM.TrainingBudgetAllocationId = alocD.TrainingBudgetAllocationId
        WHERE   alocM.TrainingBudgetMasterId = A.TrainingBudgetMasterId )
 or A.TrainingBudgetDetailsDptId IN  (
        SELECT  alocD.TrainingBudgetDetailsDptId
        FROM    dbo.tblTrainingBudgetAllocationDetails alocD
                LEFT JOIN dbo.tblTrainingBudgetAllocationMaster alocM ON alocM.TrainingBudgetAllocationId = alocD.TrainingBudgetAllocationId
        WHERE   alocM.TrainingBudgetMasterId = A.TrainingBudgetMasterId AND alocM.isDelete=1   )
        AND A.TrainingBudgetMasterId =" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public DataTable GetTrainingDetailsDptByMasterQuater(int id ,string quater)
        {
            try
            {
                string query = @"SELECT  A.TrainingBudgetDetailsDptId ,
        A.DepartmentId AS departmentId ,
        A.FinancialYearId AS finYear ,
        A.Qty AS quantity ,
        A.Quater AS quaterId ,
		q.QuarterName AS quater,
        A.TrainingMonth AS month ,
        CONVERT(NVARCHAR(11), A.FromDate, 106) AS fromDate ,
        CONVERT(NVARCHAR(11), A.ToDate, 106) AS toDate ,
        b.DepartmentName AS department
FROM    tblTrainingBudgetDetailsDpt A
        LEFT JOIN tblDepartment b ON A.DepartmentId = b.DepartmentId
		LEFT JOIN dbo.tblQuarterInfo q ON a.Quater = q.QuarterId where a.TrainingBudgetMasterId= " + id + " and a.Quater="+Convert.ToInt32(quater)+" ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTrainingDetailsGradeByMasterQuater(int id, string quater)
        {
            string query = @"select  a.TrainingBudgetDetailsGradeId, a.GradeId as gradeId   , a.FinancialYearId as finYear ,
                                    a.Qty as quantity,  A.Quater AS quaterId ,
		q.QuarterName AS quater ,a.TrainingMonth as month , 
                                    CONVERT(nvarchar(11) , a.FromDate,106)  as fromDate   , CONVERT(nvarchar(11) , a.toDate,106) as toDate, 
                                    b.GradeName as grade from tblTrainingBudgetDetailsGrade A 
LEFT JOIN dbo.tblQuarterInfo q ON a.Quater = q.QuarterId
                                    left join tblEmployeeGrade b on a.GradeId = b.GradeId where a.TrainingBudgetMasterId= " + id + " and  a.Quater='" + quater + "' ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable GetTrainingDetailsGrdByMaster(int id)
        {
            try
            {
                string query = @"SELECT  A.TrainingBudgetDetailsGradeId ,
                A.GradeId AS gradeId ,
                A.FinancialYearId AS finYear ,
                A.Qty AS quantity ,
                 A.Quater AS quaterId ,
		quater.QuarterName AS quater,
		
        A.TrainingMonth AS monthId ,
        qMonth.MonthName AS month ,
                CONVERT(NVARCHAR(11), A.FromDate, 106) AS fromDate ,
                CONVERT(NVARCHAR(11), A.ToDate, 106) AS toDate ,
                b.GradeName AS grade
        FROM    tblTrainingBudgetDetailsGrade A
                LEFT JOIN tblEmployeeGrade b ON A.GradeId = b.GradeId
                LEFT JOIN dbo.tblQuarterInfo quater ON a.Quater = quater.QuarterId
		        LEFT JOIN dbo.tblQuarterMonthInfo qMonth ON a.TrainingMonth = qMonth.QuarterMonthId
        WHERE  A.TrainingBudgetDetailsGradeId NOT IN (
        SELECT  alocD.TrainingBudgetDetailsGradeId
        FROM    dbo.tblTrainingBudgetAllocationDetails alocD
                LEFT JOIN dbo.tblTrainingBudgetAllocationMaster alocM ON alocM.TrainingBudgetAllocationId = alocD.TrainingBudgetAllocationId
        WHERE   alocM.TrainingBudgetMasterId = A.TrainingBudgetMasterId )
 or A.TrainingBudgetDetailsGradeId IN  (
        SELECT  alocD.TrainingBudgetDetailsGradeId
        FROM    dbo.tblTrainingBudgetAllocationDetails alocD
                LEFT JOIN dbo.tblTrainingBudgetAllocationMaster alocM ON alocM.TrainingBudgetAllocationId = alocD.TrainingBudgetAllocationId
        WHERE   alocM.TrainingBudgetMasterId = A.TrainingBudgetMasterId AND alocM.isDelete=1   )				
		AND  A.TrainingBudgetMasterId =" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetTrainingDetailsEmpByMaster(int id)
        {
            try
            {
                string query = @"select a.EmployeeId as employeeId   , a.FinancialYearId as finYear ,
                             A.Quater AS quaterId ,
		quater.QuarterName AS quater,
		
        A.TrainingMonth AS monthId ,
        qMonth.MonthName AS month , CONVERT(nvarchar(11) , a.FromDate,106)  as fromDate ,
                            CONVERT(nvarchar(11) , a.toDate,106) as toDate, 
                            b.EmpMasterCode+' : '+b.EmpName+' : '+ desg.Designation+' : '+dpt.DepartmentName as employee
                            from tblTrainingBudgetDetailsEmployee A 
                            left join tblEmpGeneralInfo b on a.EmployeeId = b.EmpInfoId 
                            left join tblDesignation desg on b.DesignationId= desg.DesignationId
                            left join tblDepartment dpt on b.DepartmentId = dpt.DepartmentId
                            LEFT JOIN dbo.tblQuarterInfo quater ON a.Quater = quater.QuarterId
		                    LEFT JOIN dbo.tblQuarterMonthInfo qMonth ON a.TrainingMonth = qMonth.QuarterMonthId
                             where a.TrainingBudgetMasterId=" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public int GetEmployeeCountByDeptId(int id)
        {
            int empCount = 0;
            string query = @"select count(*)EmpCount from tblEmpGeneralInfo where DepartmentId = "+id+" and isActive=1";

            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            if (dt == null || dt.Rows.Count == 0)
            {
                empCount = 0;
            }
            else
            {
                empCount = Convert.ToInt32(dt.Rows[0][0].ToString());
            }

            return empCount;

        }

        public int GetEmployeeCountByGradeId(int id)
        {
            int empCount = 0;
            string query = @"select count(*)EmpCount from tblEmpGeneralInfo where EmpGradeId = " + id + " and isActive=1";

            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            if (dt == null || dt.Rows.Count == 0)
            {
                empCount = 0;
            }
            else
            {
                empCount = Convert.ToInt32(dt.Rows[0][0].ToString());
            }

            return empCount;

        }



        public DataTable GetTraininByFinancialYearAndCompany(int com , int year)
        {
            try
            {
                string query = @"select TrainingBudgetMasterId as Value  , TrainingBudgetNumber+' : '+TrainingTitle as TextField from tblTrainingBudgetMaster where 
        CompanyId = "+com+" AND FinancialYearId = "+year+" AND IsApprove=1 AND (IsDelete IS  NULL OR IsDelete = 0) ";
                 return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        public DataTable GetEmployeeByDptId(int id)
        {
            string query = @"select a.EmpInfoId , a.EmpMasterCode , a.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation from tblEmpGeneralInfo A 
                                left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId
                                left join tblDesignation desg on a.DesignationId = desg.DesignationId
                                left join tblEmployeeGrade grd on a.EmpGradeId = grd.GradeId where a.isActive=1 and a.DepartmentId =" +id+"";

            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable GetEmployeeByGradeId(int id)
        {
            string query = @"select a.EmpInfoId , a.EmpMasterCode , a.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation from tblEmpGeneralInfo A 
                                left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId
                                left join tblDesignation desg on a.DesignationId = desg.DesignationId
                                left join tblEmployeeGrade grd on a.EmpGradeId = grd.GradeId where a.isActive=1 and a.EmpGradeId =" + id + "";

            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetTrainingBudgetEmployeeDetails (int id , string quater){

            string query = @"select M.TrainingBudgetDetailsEmpId as DetailsId, a.EmpInfoId , a.EmpMasterCode , a.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation from tblTrainingBudgetDetailsEmployee M
								left join  tblEmpGeneralInfo A on M.EmployeeId = a.EmpInfoId
                                left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId
                                left join tblDesignation desg on a.DesignationId = desg.DesignationId
                                left join tblEmployeeGrade grd on a.EmpGradeId = grd.GradeId where a.isActive=1 and M.TrainingBudgetMasterId= " + id + " and M.Quater = '"+quater+"'";

            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetTrainingBudgetDetailsEmployee(int master)
        {
            string query = @"SELECT  COUNT(*) EmpCount ,
        a.Quater AS quaterId ,
		q.QuarterName AS Quater,
        TrainingBudgetMasterId
FROM    tblTrainingBudgetDetailsEmployee A
	LEFT JOIN dbo.tblQuarterInfo q ON a.Quater = q.QuarterId
WHERE   A.TrainingBudgetMasterId = " + master + " group by quater ,TrainingBudgetMasterId ,q.QuarterName";

            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public string GenerateAutoNumber(string table, string column, DateTime date)
        {
            try
            {
                string query = @"SELECT (((SUBSTRING((CONVERT(NVARCHAR(5),YEAR(GETDATE()))),3,2)+(CASE WHEN LEN(MONTH(GETDATE()))=1 
		THEN  '0'+CONVERT(NVARCHAR(5),MONTH(GETDATE())) ELSE CONVERT(NVARCHAR(5),MONTH(GETDATE())) END)+
		(CASE WHEN LEN(DAY(GETDATE()))=1 THEN  '0'+CONVERT(NVARCHAR(5),DAY(GETDATE())) ELSE CONVERT(NVARCHAR(5),DAY(GETDATE())) END)))+
		CONVERT(NVARCHAR(5),(ISNULL((MAX(CONVERT(INT,(SUBSTRING( " + column + " ,10,4))))),1000)+1))) as AutoNumber FROM  " + table + "  WHERE  CONVERT(NVARCHAR(11),EntryDate,106)= CONVERT(NVARCHAR(11),'"+(System.DateTime.Now).ToString("dd-MMM-yyyy").Replace("-"," ")+"',106)";
            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            Decimal asd = Convert.ToDecimal(dt.Rows[0][0].ToString()); ;
            Decimal asd1 = asd + 1;

            return dt.Rows[0][0].ToString();
        
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }



        public bool DeleteBudget(int id, string user)
        {
            bool result = false;
            List<SqlParameter> aperam = new List<SqlParameter>();

            aperam.Add(new SqlParameter("@TrainingBudgetMasterId", id));
            aperam.Add(new SqlParameter("@DeleteBy", user));
            aperam.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
            string query = @"update tblTrainingBudgetMaster set  IsDelete=1 , DeleteBy = @DeleteBy ,DeleteDate=@DeleteDate where TrainingBudgetMasterId = @TrainingBudgetMasterId ";


            result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, aperam, DataBase.HRDB);
            return result;

        }


        public bool DeleteTrainingBudgetWiseAllocation(int id, string user)
        {
            try
            {
                bool result = false;
            List<SqlParameter> aperam = new List<SqlParameter>();

            aperam.Add(new SqlParameter("@TrainingBudgetAllocationId", id));
            aperam.Add(new SqlParameter("@DeleteBy", user));
            aperam.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
            string query = @"update tblTrainingBudgetAllocationMaster set  IsDelete=1 , DeleteBy = @DeleteBy ,DeleteDate=@DeleteDate where TrainingBudgetAllocationId = @TrainingBudgetAllocationId ";


            result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, aperam, DataBase.HRDB);
            return result;
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }


       
        #endregion


        #region budget wise Allocation

        public DataTable GetBudgetAlloationList()
        {
            try
            {
                string query = @"SELECT  A.TrainingBudgetAllocationId ,
        quater.QuarterName AS  Quater ,
        A.BudgetAllocationNumber ,
        b.TrainingBudgetNumber + b.TrainingTitle AS Training ,
        CASE WHEN A.ForDepartment = 1 THEN 'Department'
             WHEN A.ForGrade = 1 THEN 'Grade'
             WHEN A.ForEmployee = 1 THEN 'Employee'
             ELSE 'a'
        END AS SpecificFor
FROM    tblTrainingBudgetAllocationMaster A
        LEFT JOIN tblTrainingBudgetMaster b ON A.TrainingBudgetMasterId = b.TrainingBudgetMasterId
		LEFT JOIN dbo.tblQuarterInfo quater ON a.Quater = quater.QuarterId
WHERE   A.IsDelete IS NULL
        OR A.IsDelete = 0";

                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public DataTable GetEmployeeDetailsByMasterCode(string code)
        {

            try
            {
                string query = @"select A.EmpMasterCode, A.EmpName , A.EmpInfoId, desg.Designation, dpt.DepartmentName , grd.GradeName from tblEmpGeneralInfo A 
                                    left join tblDesignation desg on a.DesignationId=desg.DesignationId  
                                    left join tblDepartment dpt on a.DepartmentId=dpt.DepartmentId
                                    left join tblEmployeeGrade grd on a.EmpGradeId = grd.GradeId where A.EmpMasterCode = '" + code + "'";
                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                return dt;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        public int SaveBudgetAllocationMaster(TrainingBudgetAllocationMaster aMaster , string user)
        {
            try
            {
                int result = 0;


                if (aMaster.TrainingBudgetAllocationId >0)
                {

                    List<SqlParameter> aPerams = new List<SqlParameter>();
                    aPerams.Add(new SqlParameter("@ForDepartment", aMaster.ForDepartment));
                    aPerams.Add(new SqlParameter("@ForEmployee", aMaster.ForEmployee));
                    aPerams.Add(new SqlParameter("@TrainingBudgetAllocationId", aMaster.TrainingBudgetAllocationId));
                    aPerams.Add(new SqlParameter("@ForGrade", aMaster.ForGrade));
                    aPerams.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aPerams.Add(new SqlParameter("@TrainingBudgetMasterId", aMaster.TrainingBudgetMasterId));
                    aPerams.Add(new SqlParameter("@Quater", aMaster.Quater));
                    aPerams.Add(new SqlParameter("@TrainingBudgetDetailsId", aMaster.TrainingBudgetDetailsId));
                    aPerams.Add(new SqlParameter("@UpdateBy", user));
                    aPerams.Add(new SqlParameter("@UpdateDate", DateTime.Now));

                    string query = @"update tblTrainingBudgetAllocationMaster set 
                            ForDepartment = @ForDepartment, ForGrade=@ForGrade, ForEmployee=@ForEmployee, TrainingBudgetDetailsId=@TrainingBudgetDetailsId, FinancialYearId =@FinancialYearId, UpdateBy=@UpdateBy, 
                            UpdateDate=@UpdateDate, Quater=@Quater,  TrainingBudgetMasterId=@TrainingBudgetMasterId where TrainingBudgetAllocationId = @TrainingBudgetAllocationId ";


                    bool update = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aPerams, DataBase.HRDB);
                    if (update == true)
                    {
                        result = aMaster.TrainingBudgetAllocationId;
                    }
                }

                else
                {
                    List<SqlParameter> aPerams = new List<SqlParameter>();
                    aPerams.Add(new SqlParameter("@ForDepartment", aMaster.ForDepartment));
                    aPerams.Add(new SqlParameter("@ForEmployee", aMaster.ForEmployee));
                    aPerams.Add(new SqlParameter("@ForGrade", aMaster.ForGrade));
                    aPerams.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aPerams.Add(new SqlParameter("@TrainingBudgetMasterId", aMaster.TrainingBudgetMasterId));
                    aPerams.Add(new SqlParameter("@Quater", aMaster.Quater));
                    aPerams.Add(new SqlParameter("@TrainingBudgetDetailsId", aMaster.TrainingBudgetDetailsId));
                    aPerams.Add(new SqlParameter("@EntryBy", user));
                    aPerams.Add(new SqlParameter("@EntryDate", DateTime.Now));
                    aPerams.Add(new SqlParameter("@BudgetAllocationNumber", "TA:" + GenerateAutoNumber("tblTrainingBudgetAllocationMaster", "BudgetAllocationNumber", DateTime.Now)));

                    string query = @"insert into  tblTrainingBudgetAllocationMaster ( ForDepartment, ForGrade, ForEmployee, TrainingBudgetDetailsId, EntryBy, EntryDate, FinancialYearId, Quater, BudgetAllocationNumber,TrainingBudgetMasterId)
                                            values(@ForDepartment, @ForGrade, @ForEmployee, @TrainingBudgetDetailsId, @EntryBy, @EntryDate, @FinancialYearId, @Quater, @BudgetAllocationNumber,@TrainingBudgetMasterId)";


                    result = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPerams, DataBase.HRDB);
                }
                return result;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public int SaveBudgetAllocationMasterEmp(TrainingBudgetAllocationMaster aMaster, string user)
        {
            try
            {
                int result = 0;
                List<SqlParameter> aPerams = new List<SqlParameter>();
                aPerams.Add(new SqlParameter("@ForDepartment", aMaster.ForDepartment));
                aPerams.Add(new SqlParameter("@ForEmployee", aMaster.ForEmployee));
                aPerams.Add(new SqlParameter("@ForGrade", aMaster.ForGrade));
                aPerams.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                aPerams.Add(new SqlParameter("@Quater", aMaster.Quater));
                
                aPerams.Add(new SqlParameter("@TrainingBudgetMasterId", aMaster.TrainingBudgetMasterId));
                aPerams.Add(new SqlParameter("@EntryBy", user));
                aPerams.Add(new SqlParameter("@EntryDate",DateTime.Now));
                aPerams.Add(new SqlParameter("@BudgetAllocationNumber", "TA:"+GenerateAutoNumber("tblTrainingBudgetAllocationMaster", "BudgetAllocationNumber",DateTime.Now)));

                string query = @"insert into  tblTrainingBudgetAllocationMaster ( ForDepartment, ForGrade, ForEmployee, EntryBy, EntryDate, FinancialYearId, Quater, BudgetAllocationNumber,TrainingBudgetMasterId)
                                            values(@ForDepartment, @ForGrade, @ForEmployee, @EntryBy, @EntryDate, @FinancialYearId, @Quater, @BudgetAllocationNumber,@TrainingBudgetMasterId)";

         
               result = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPerams, DataBase.HRDB);

                return result;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        
        public bool SaveBudgetAllocationDetails( List<TrainingBudgetAllocationDetails>  aList)
        {
            try
            {

                string qDel = @"Delete from tblTrainingBudgetAllocationDetails where  TrainingBudgetAllocationId = " + aList[0].TrainingBudgetAllocationId + "";

                bool resulDel = _aCommonInternalDal.DeleteDataByDeleteCommand(qDel , DataBase.HRDB);

                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aPerams = new List<SqlParameter>();
                aPerams.Add(new SqlParameter("@TrainingBudgetAllocationId", item.TrainingBudgetAllocationId));
                aPerams.Add(new SqlParameter("@TrainingBudgetDetailsDptId", item.TrainingBudgetDetailsDptId));
                aPerams.Add(new SqlParameter("@TrainingBudgetDetailsGradeId", item.TrainingBudgetDetailsGradeId));
                aPerams.Add(new SqlParameter("@TrainingBudgetDetailsEmpId", item.TrainingBudgetDetailsEmpId));
                aPerams.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                aPerams.Add(new SqlParameter("@Quater", item.Quater));

                string query = @"insert into tblTrainingBudgetAllocationDetails (TrainingBudgetAllocationId, TrainingBudgetDetailsDptId,TrainingBudgetDetailsGradeId,TrainingBudgetDetailsEmpId, EmpInfoId, Quater) 
                    values (@TrainingBudgetAllocationId, @TrainingBudgetDetailsDptId,@TrainingBudgetDetailsGradeId,@TrainingBudgetDetailsEmpId, @EmpInfoId, @Quater)";

                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aPerams , DataBase.HRDB);
                    if (result == false)
                        break;
                }
                return result;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public DataTable GetBudgetAllocationMaster(int id)
        {
            string query = @"select a.* , B.CompanyId from  tblTrainingBudgetAllocationMaster  A 
                    left join tblTrainingBudgetMaster b on a.TrainingBudgetMasterId=b.TrainingBudgetMasterId where a.TrainingBudgetAllocationId = " + id + "";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB); ;
        }

        public DataTable GetEmployeeByAlloationMasterDpt(int id)
        {

            string query = @"SELECT  A.TrainingBudgetDetailsDptId AS DetailsId ,
        A.EmpInfoId ,
		A.Quater AS quaterId,
        q.QuarterName Quater ,
        e.EmpMasterCode ,
        e.EmpName ,
        grd.GradeName ,
        dpt.DepartmentName ,
        desg.Designation
FROM    tblTrainingBudgetAllocationDetails A
        LEFT JOIN tblEmpGeneralInfo e ON A.EmpInfoId = e.EmpInfoId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblEmployeeGrade grd ON e.EmpGradeId = grd.GradeId
		LEFT JOIN dbo.tblQuarterInfo q ON a.Quater =q.QuarterId
WHERE   A.TrainingBudgetAllocationId =" + id + "";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB); ;
        }


        public DataTable GetEmployeeByAlloationMasterGrade(int id)
        {

            string query = @"select a.TrainingBudgetDetailsGradeId as DetailsId,  a.EmpInfoId,A.Quater AS quaterId,
        q.QuarterName Quater  , e.EmpMasterCode , e.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation from tblTrainingBudgetAllocationDetails A 
left join tblEmpGeneralInfo e on a.EmpInfoId = e.EmpInfoId
left join tblDesignation desg on e.DesignationId = desg.DesignationId
left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
left join tblEmployeeGrade grd on e.EmpGradeId = grd.GradeId
LEFT JOIN dbo.tblQuarterInfo q ON a.Quater =q.QuarterId
where a. TrainingBudgetAllocationId= " + id + "";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB); ;
        }


        public DataTable GetEmployeeByAlocationMaster(int id)
        {
            try
            {
                string query = @"SELECT  b.TrainingBudgetAllocationDetailsId  ,a.Quater,
        
                            b.EmpInfoId ,
		                     e.EmpMasterCode , e.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation ,
		                     grd.GradeName
                            
                    FROM    dbo.tblTrainingBudgetAllocationMaster a
                            LEFT JOIN dbo.tblTrainingBudgetAllocationDetails b ON b.TrainingBudgetAllocationId = a.TrainingBudgetAllocationId
		                    left join tblEmpGeneralInfo e on b.EmpInfoId = e.EmpInfoId
                            left join tblDesignation desg on e.DesignationId = desg.DesignationId
                            left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
                            left join tblEmployeeGrade grd on e.EmpGradeId = grd.GradeId WHERE a.TrainingBudgetAllocationId =" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB); ;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public DataTable GetEmployeeByRequisitionMasterMaster(int id)
        {
            try
            {
                string query = @"SELECT  A.Quater , b.TrainingRequisitionDetailsId AS TrainingBudgetAllocationDetailsId,
                            b.EmpInfoId ,
		                     e.EmpMasterCode , e.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation ,
		                     grd.GradeName
FROM    dbo.tblTrainingRequisitionMaster A
        INNER JOIN dbo.tblTrainingRequisitionDetails b ON b.TrainingRequisitionMasterId = A.TrainingRequisitionMasterId
		 left join tblEmpGeneralInfo e on b.EmpInfoId = e.EmpInfoId
                            left join tblDesignation desg on e.DesignationId = desg.DesignationId
                            left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
                            left join tblEmployeeGrade grd on e.EmpGradeId = grd.GradeId WHERE A.TrainingRequisitionMasterId  =" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB); ;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetEmployeeByTrainingMasterandQuater(int id , string quater)
        {
           


            string query = @"select a.EmpInfoId , a.EmpMasterCode , a.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation from tblTrainingBudgetDetailsEmployee M
								left join  tblEmpGeneralInfo A on M.EmployeeId = a.EmpInfoId
                                left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId
                                left join tblDesignation desg on a.DesignationId = desg.DesignationId
                                left join tblEmployeeGrade grd on a.EmpGradeId = grd.GradeId where a.isActive=1 and M.TrainingBudgetMasterId= " + id + " and M.Quater = '" + quater + "'";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB); ;
        }
       
        #endregion

        #region TrainingAttendance

//        public DataTable GetEmployeeForAttendance(int id)
//        {
//            string query = @"select a.TrainingBudgetAllocationDetailsId , e.EmpInfoId ,a.Quater , e.EmpMasterCode,e.EmpName,desg.Designation,DepartmentName,grd.GradeName 
//                                from tblTrainingBudgetAllocationDetails A left join 
//                            tblEmpGeneralInfo e on a.EmpInfoId = e.EmpInfoId
//                            left join tblDesignation desg on e.DesignationId = desg.DesignationId
//                            left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
//                left join tblEmployeeGrade grd on e.EmpGradeId = grd.GradeId where a.TrainingBudgetAllocationId = " + id + " and A.isActive=1";
//            return _aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
//        }


        public DataTable GetEmployeeForAttendanceByRequisitionMaster(int id)
        {
            try
            {
                string query = @"SELECT  A.Quater , b.TrainingRequisitionDetailsId AS TrainingBudgetAllocationDetailsId,
                            b.EmpInfoId ,
		                     e.EmpMasterCode , e.EmpName ,grd.GradeName, dpt.DepartmentName , desg.Designation ,
		                     grd.GradeName
FROM    dbo.tblTrainingRequisitionMaster A
        INNER JOIN dbo.tblTrainingRequisitionDetails b ON b.TrainingRequisitionMasterId = A.TrainingRequisitionMasterId
		 left join tblEmpGeneralInfo e on b.EmpInfoId = e.EmpInfoId
                            left join tblDesignation desg on e.DesignationId = desg.DesignationId
                            left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
                            left join tblEmployeeGrade grd on e.EmpGradeId = grd.GradeId WHERE A.TrainingRequisitionMasterId  =" + id + " where is a.Active=1";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB); ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SaveTrainingAttendanceMaster(TrainingAttendanceMaster aMaster, string user)
        {
            try
            {
                int result = 0;

                if (aMaster.TrainingAttendanceMasterId > 0)
                {
                    List<SqlParameter> aPerams = new List<SqlParameter>();
                    aPerams.Add(new SqlParameter("@TrainingMasterId", aMaster.TrainingMasterId));
                    aPerams.Add(new SqlParameter("@TrainingAttendanceMasterId", aMaster.TrainingAttendanceMasterId));
                    aPerams.Add(new SqlParameter("@TrainingRquisitionMaster", aMaster.TrainingRquisitionMaster));
                    
                    aPerams.Add(new SqlParameter("@TrainingAllocationId", aMaster.TrainingAllocationId));
                    aPerams.Add(new SqlParameter("@Quater", aMaster.Quater));
                    aPerams.Add(new SqlParameter("@UpdateBy", user));
                    aPerams.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                    string query = @"update tblTrainingAttendanceMaster set TrainingMasterId = @TrainingMasterId, 
                                    TrainingAllocationId = @TrainingAllocationId, UpdateBy = @UpdateBy, TrainingRquisitionMaster=@TrainingRquisitionMaster,
                                    UpdateDate=@UpdateDate, Quater=@Quater where  TrainingAttendanceMasterId = @TrainingAttendanceMasterId ";

                    bool a = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aPerams, DataBase.HRDB);

                    if (a == true)
                    {
                        result = aMaster.TrainingAttendanceMasterId;
                    }
                }
                else
                {
                    List<SqlParameter> aPerams = new List<SqlParameter>();
                    aPerams.Add(new SqlParameter("@TrainingMasterId", aMaster.TrainingMasterId));
                    
                    aPerams.Add(new SqlParameter("@TrainingAllocationId", aMaster.TrainingAllocationId));
                    aPerams.Add(new SqlParameter("@TrainingRquisitionMaster", aMaster.TrainingRquisitionMaster));

                    
                    aPerams.Add(new SqlParameter("@Quater", aMaster.Quater));
                    aPerams.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aPerams.Add(new SqlParameter("@EntryBy", user));

                    string query = @"insert into  tblTrainingAttendanceMaster (TrainingMasterId, TrainingAllocationId, EntryBy, EntryDate, Quater,TrainingRquisitionMaster) values (@TrainingMasterId, @TrainingAllocationId, @EntryBy, @EntryDate, @Quater,@TrainingRquisitionMaster)";

                    result = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPerams, DataBase.HRDB);
                }
                

                return result;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public bool SaveTrainingAttendanceDetails(List<TrainingAttendanceDetails> aDetails , int master)
        {
            try
            {

                bool result = false;
                string queryDel = @"Delete from tblTrainingAttendanceDetails where  TrainingAttendanceMasterId ="+master+"";
                result = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel , DataBase.HRDB);
                
                foreach (var item in aDetails)
                {
                    List<SqlParameter> aPeram = new List<SqlParameter>();

                    aPeram.Add(new SqlParameter("@IsPresent", item.IsPresent));
                    aPeram.Add(new SqlParameter("@TrainingAttendanceMasterId", master));
                    aPeram.Add(new SqlParameter("@TrainingBudgetAllocationDetailsId", item.TrainingBudgetAllocationDetailsId));
                    aPeram.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                    aPeram.Add(new SqlParameter("@TrainingRequisitionDetailsId", item.TrainingRequisitionDetailsId));
                    string query = @"insert into tblTrainingAttendanceDetails (TrainingAttendanceMasterId, EmpInfoId, TrainingBudgetAllocationDetailsId, IsPresent,TrainingRequisitionDetailsId) values 
                                      (@TrainingAttendanceMasterId, @EmpInfoId, @TrainingBudgetAllocationDetailsId, @IsPresent,@TrainingRequisitionDetailsId)";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aPeram, DataBase.HRDB);

                }
                return result;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public DataTable GetAttendanceMasterForEdit(int trainingMasterId)
        {
            try
            {
                string query = "Select * from tblTrainingAttendanceMaster where TrainingMasterId = " + trainingMasterId + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DataTable GetAttendanceDetails(int masterID)
        {
            try
            {
                string query = @"select a.TrainingBudgetAllocationDetailsId, tm.Quater , e.EmpInfoId , e.EmpMasterCode,e.EmpName,desg.Designation,DepartmentName,grd.GradeName 
                                from tblTrainingAttendanceDetails A 
                                left join tblTrainingAttendanceMaster tm on a.TrainingAttendanceMasterId = tm.TrainingAttendanceMasterId
                                left join 
                            tblEmpGeneralInfo e on a.EmpInfoId = e.EmpInfoId
                            left join tblDesignation desg on e.DesignationId = desg.DesignationId
                            left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
                left join tblEmployeeGrade grd on e.EmpGradeId = grd.GradeId where a.TrainingAttendanceMasterId  = " + masterID + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public DataTable GetEmployeeFromRequisitionDetails(int id)
        {
            string query = @"select a.TrainingRequisitionDetailsId as  TrainingBudgetAllocationDetailsId ,e.EmpInfoId, b.Quater ,e.EmpMasterCode,e.EmpName,desg.Designation,DepartmentName,grd.GradeName   from  tblTrainingRequisitionDetails a 
							left join tblTrainingRequisitionMaster b on a.TrainingRequisitionMasterId = b.TrainingRequisitionMasterId
								left join 
                            tblEmpGeneralInfo e on a.EmpInfoId = e.EmpInfoId
                            left join tblDesignation desg on e.DesignationId = desg.DesignationId
                            left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
							left join tblEmployeeGrade grd on e.EmpGradeId = grd.GradeId where a.TrainingRequisitionMasterId = " +id+" and  a.isActive=1 ";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


        }
        #endregion

        #region Evaluation Form

        public DataTable GetTrainingSetupByforEvaluation(int comid, int finId)
        {

            try
            {
                string query = @"select  a.TrainingSetupNumber+':'+a.TrainingTitle as Training  , a.TrainingMasterId   from tblTrainingMaster  A where a.TrainingEvaluation = 1 and 
                                    a.CompanyId = " + comid + " and a.FinancialYearId = " + finId + " ";

              return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public int SaveEvaluationForm( EvaluationFormMaster aMaster , string user )
        {
            try
            {
                int result = 0;

                if (aMaster.EvaluationFormMasterId > 0)
                {
                    List<SqlParameter> aPerams = new List<SqlParameter>();
                    
                    aPerams.Add(new SqlParameter("@EvaluationFormMasterId", aMaster.EvaluationFormMasterId));
                    
                    aPerams.Add(new SqlParameter("@UpdateBy", user));
                    aPerams.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                    string query = @"update  tblEvaluationFormMaster set  UpdateBy =@UpdateBy , UpdateDate = @UpdateDate where  EvaluationFormMasterId = @EvaluationFormMasterId";

                    bool resultUp = _aCommonInternalDal.UpdateDataByUpdateCommand(query , aPerams , DataBase.HRDB);

                    if (resultUp == true)
                    {
                        result = aMaster.EvaluationFormMasterId;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                else
                {
                    List<SqlParameter> aPerams = new List<SqlParameter>();
                    
                    aPerams.Add(new SqlParameter("@EvaluationFormNo", "EF:"+GenerateAutoNumber("tblEvaluationFormMaster", "EvaluationFormNo", System.DateTime.Now)));
                    aPerams.Add(new SqlParameter("@EntryBy", user));
                    aPerams.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                    string query = @"insert into tblEvaluationFormMaster ( EvaluationFormNo, EntryBy, EntryDate) values (@EvaluationFormNo, @EntryBy, @EntryDate)";
                    result = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPerams, DataBase.HRDB);
                }
               
                return result;


            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void TriningHeadingDropDown(DropDownList ddl)
        {
            string query = "SELECT  TraingingHeading  TraingingHeading, * FROM dbo.tblTraingingHeading WHERE IsActive='1' AND (IsDelete IS NULL OR IsDelete='False')";
            _aCommonInternalDal.LoadDropDownValue(ddl, "TraingingHeading", "TraingingHeadingId", query, DataBase.HRDB);

        }
        public void TriningTopicDropDown(DropDownList ddl,string id)
        {
            string query = "SELECT TrainingTopic  TrainingTopic, * FROM dbo.tblTrainingSetupTopic WHERE TraingingHeadingId='" + id + "' AND IsActive='1' AND (IsDelete IS NULL OR IsDelete='False')";
            _aCommonInternalDal.LoadDropDownValue(ddl, "TrainingTopic", "TrainingTopicId", query, DataBase.HRDB);

        }
        public bool SaveEvaluationFormDetails(List<EvaluationFormDetails> aList , int master) 
        {
            try
            {

                bool result = false;
                
                foreach (var item in aList)
                {
                    if (item.EvaluationFormDetailsId >0)
                    {
                        List<SqlParameter> aperams = new List<SqlParameter>();
                        aperams.Add(new SqlParameter("@EvaluationFormMasterId", master));
                        aperams.Add(new SqlParameter("@EvaluationFormDetailsId", item.EvaluationFormDetailsId));
                        aperams.Add(new SqlParameter("@TraingingHeadingId", item.TraingingHeadingId));
                        aperams.Add(new SqlParameter("@TrainingTopicId", item.TrainingTopicId));
                        aperams.Add(new SqlParameter("@FailedText", item.FailedText));
                        //aperams.Add(new SqlParameter("@TopicText", item.TopicText));
                        aperams.Add(new SqlParameter("@AverageText", item.AverageText));
                        aperams.Add(new SqlParameter("@AboveAverageText", item.AboveAverageText));
                        aperams.Add(new SqlParameter("@ExcellentText", item.ExcellentText));
                        aperams.Add(new SqlParameter("@IsActive", item.IsActive));

                        string query = @"UPDATE tblEvaluationFormDetails SET IsActive=@IsActive,EvaluationFormMasterId=@EvaluationFormMasterId, TraingingHeadingId=@TraingingHeadingId,TrainingTopicId=@TrainingTopicId, FailedText=@FailedText, AverageText=@AverageText, AboveAverageText=@AboveAverageText, ExcellentText=@ExcellentText WHERE EvaluationFormDetailsId=@EvaluationFormDetailsId ";
                        result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aperams, DataBase.HRDB);
                    }
                    else
                    {
                        List<SqlParameter> aperams = new List<SqlParameter>();
                        aperams.Add(new SqlParameter("@EvaluationFormMasterId", master));
                        aperams.Add(new SqlParameter("@TraingingHeadingId", item.TraingingHeadingId));
                        aperams.Add(new SqlParameter("@TrainingTopicId", item.TrainingTopicId));
                        aperams.Add(new SqlParameter("@FailedText", item.FailedText));
                        //aperams.Add(new SqlParameter("@TopicText", item.TopicText));
                        aperams.Add(new SqlParameter("@AverageText", item.AverageText));
                        aperams.Add(new SqlParameter("@AboveAverageText", item.AboveAverageText));
                        aperams.Add(new SqlParameter("@ExcellentText", item.ExcellentText));
                        aperams.Add(new SqlParameter("@IsActive", item.IsActive));

                        string query = @"Insert into tblEvaluationFormDetails (EvaluationFormMasterId, TraingingHeadingId,TrainingTopicId, FailedText, AverageText, AboveAverageText, ExcellentText,IsActive) 
                                    values(@EvaluationFormMasterId, @TraingingHeadingId,@TrainingTopicId, @FailedText, @AverageText, @AboveAverageText, @ExcellentText,@IsActive)";
                        result = _aCommonInternalDal.SaveDataByInsertCommand(query, aperams, DataBase.HRDB);   
                    }

                     

                }

                return result;
            }
            catch (Exception ex)
            {
                
                throw;
            }

        }
        public int SaveEvaluationEmployee(EvaluateTrainingMaster aTrainingMaster)
        {
            try
            {

               
                    List<SqlParameter> aperams = new List<SqlParameter>();
                    aperams.Add(new SqlParameter("@TrainingRecordMasterId", aTrainingMaster.TrainingRecordMasterId));
                    aperams.Add(new SqlParameter("@EmpInfoId", aTrainingMaster.EmpInfoId));
                    aperams.Add(new SqlParameter("@EntryBy", aTrainingMaster.EntryBy));
                    aperams.Add(new SqlParameter("@Comments", aTrainingMaster.Comments));
                    aperams.Add(new SqlParameter("@EntryDate", aTrainingMaster.EntryDate));
                    aperams.Add(new SqlParameter("@ReportingEmpId", aTrainingMaster.ReportingEmpId ??(object) DBNull.Value));
                    
                    string query = @"INSERT INTO dbo.tblEvaluateTrainingMaster
                                    (
                                        TrainingRecordMasterId,
                                        EntryBy,
                                        EntryDate,EmpInfoId, Comments, ReportingEmpId
                                    )
                                    VALUES
                                    ( @TrainingRecordMasterId,
                                        @EntryBy,
                                        @EntryDate,@EmpInfoId, @Comments, @ReportingEmpId
                                    )";




                    return _aCommonInternalDal.SaveDataByInsertCommandById(query, aperams, DataBase.HRDB); ;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool SaveEvaluateEmployeeDetails(List<EvaluatTrainingDetails> aList, int master)
        {
            try
            {

                bool result = false;

                string querydel = "Delete from tblEvaluatTrainingDetails where  EvaluateTrainingMaster = " + master + "";

                result = _aCommonInternalDal.DeleteDataByDeleteCommand(querydel, DataBase.HRDB);

                foreach (var item in aList)
                {
                    List<SqlParameter> aperams = new List<SqlParameter>();
                    aperams.Add(new SqlParameter("@EvaluateTrainingMaster", master));
                    aperams.Add(new SqlParameter("@EvaluationFormDetailsId", item.EvaluationFormDetailsId));
                    aperams.Add(new SqlParameter("@IsFailed", item.IsFailed));
                    aperams.Add(new SqlParameter("@IsAverage", item.IsAverage));
                    //aperams.Add(new SqlParameter("@TopicText", item.TopicText));
                    aperams.Add(new SqlParameter("@IsAbvAverage", item.IsAbvAverage));
                    aperams.Add(new SqlParameter("@IsExcellent", item.IsExcellent));
                    
                    string query = @"INSERT INTO dbo.tblEvaluatTrainingDetails
                                    (
                                        EvaluateTrainingMaster,
                                        EvaluationFormDetailsId,
                                        IsFailed,
                                        IsAverage,
                                        IsAbvAverage,
                                        IsExcellent
                                    )
                                    VALUES
                                    (   @EvaluateTrainingMaster,
                                        @EvaluationFormDetailsId,
                                        @IsFailed,
                                        @IsAverage,
                                        @IsAbvAverage,
                                        @IsExcellent
                                    )";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aperams, DataBase.HRDB);

                }

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public DataTable GetTrainingEvaluationForm()
        {
            string Query = @"SELECT G.EmpName,*,M.TrainingRecordMasterId,C.CompanyName,M.TrainingTitle ,F.FinancialYearDesc,M.TrainingTypeId,T.TrainingType
				FROM tblTrainingRecordMaster M
				INNER JOIN dbo.tblCompanyInfo C ON M.CompanyId = C.CompanyId
				INNER JOIN dbo.tblFinancialYear F ON M.FinancialYearId = F.FinancialYearId
					INNER JOIN dbo.tblTrainingType T ON M.TrainingTypeID = T.TrainingTypeID
					INNER JOIN dbo.tbl_trainingRecordDetailsEmployee EE ON M.TrainingRecordMasterId = EE.TrainingRecordMasterId
					INNER JOIN dbo.tblEmpGeneralInfo G ON EE.EmpInfoId = G.EmpInfoId

					WHERE M.IsDelete = 0 OR M.IsDelete is null AND G.EmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "' AND M.TrainingRecordMasterId NOT IN (SELECT TrainingRecordMasterId FROM dbo.tblEvaluateTrainingMaster WHERE EmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "')";

            return _aCommonInternalDal.DataContainerDataTable(Query, DataBase.HRDB);
        }

        public DataTable GetTrainingEvaluationFormForShow()
        {
            string Query = @"SELECT G.EmpName,*,M.TrainingRecordMasterId,C.CompanyName,M.TrainingTitle ,F.FinancialYearDesc,M.TrainingTypeId,T.TrainingType
				FROM tblTrainingRecordMaster M
				INNER JOIN dbo.tblCompanyInfo C ON M.CompanyId = C.CompanyId
				INNER JOIN dbo.tblFinancialYear F ON M.FinancialYearId = F.FinancialYearId
					INNER JOIN dbo.tblTrainingType T ON M.TrainingTypeID = T.TrainingTypeID
					INNER JOIN dbo.tbl_trainingRecordDetailsEmployee EE ON M.TrainingRecordMasterId = EE.TrainingRecordMasterId
					INNER JOIN dbo.tblEmpGeneralInfo G ON EE.EmpInfoId = G.EmpInfoId

					WHERE M.IsDelete = 0 OR M.IsDelete is null AND G.EmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

            return _aCommonInternalDal.DataContainerDataTable(Query, DataBase.HRDB);
        }

        public DataTable GetEvaluationFromById(int id)
        {
            string query = @"select * from tblEvaluationFormMaster A left join tblTrainingMaster B on a.TraingMasreId = b.TrainingMasterId where a.EvaluationFormMasterId = "+id+"";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetEvaluationFormDetails(int id)
        {
            string query = @"select EvaluationFormDetailsId, TrainingTopic as topic , failedtext as failed , averagetext as avarage, aboveaveragetext as abvavarage, excellenttext as excellent,TraingingHeading,TraingingHeading AS heading,*
 from tblEvaluationFormDetails 
 LEFT JOIN dbo.tblTrainingSetupTopic ON dbo.tblEvaluationFormDetails.TrainingTopicId=dbo.tblTrainingSetupTopic.TrainingTopicId
 LEFT JOIN dbo.tblTraingingHeading ON tblTraingingHeading.TraingingHeadingId = tblTrainingSetupTopic.TraingingHeadingId";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable GetEvaluationFormDetails2(int id)
        {
            string query = @" SELECT  EvaluationFormDetailsId, TrainingTopic topic , failedtext as failed , averagetext as avarage, aboveaveragetext as abvavarage, excellenttext as excellent, tblTraingingHeading.TraingingHeading TraingingHeading,*
 from tblEvaluationFormDetails 
 LEFT JOIN dbo.tblTrainingSetupTopic ON dbo.tblEvaluationFormDetails.TrainingTopicId=dbo.tblTrainingSetupTopic.TrainingTopicId
 LEFT JOIN dbo.tblTraingingHeading ON tblTraingingHeading.TraingingHeadingId = tblTrainingSetupTopic.TraingingHeadingId
 --INNER JOIN tblEvaluateTrainingMaster mas ON  mas.EvaluateTrainingMasterId=tblEvaluationFormDetails.EvaluationFormMasterId
  WHERE tblEvaluationFormDetails.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable GetEvaluationFormDetails2EditMode(int id)
        {
            string query = @"   select * from tblEvaluatTrainingDetails dt
  inner join tblEvaluateTrainingMaster mas on mas.EvaluateTrainingMasterId=dt.EvaluateTrainingMaster where TrainingRecordMasterId=" + id;
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetEvaluationFormDetails2New(int id)
        {
            string query = @" SELECT  EvaluationFormDetailsId, TrainingTopic topic , failedtext as failed , averagetext as avarage, aboveaveragetext as abvavarage, excellenttext as excellent, tblTraingingHeading.TraingingHeading TraingingHeading,*
 from tblEvaluationFormDetails 
 LEFT JOIN dbo.tblTrainingSetupTopic ON dbo.tblEvaluationFormDetails.TrainingTopicId=dbo.tblTrainingSetupTopic.TrainingTopicId
 LEFT JOIN dbo.tblTraingingHeading ON tblTraingingHeading.TraingingHeadingId = tblTrainingSetupTopic.TraingingHeadingId
 --INNER JOIN tblEvaluateTrainingMaster mas ON  mas.EvaluateTrainingMasterId=tblEvaluationFormDetails.EvaluationFormMasterId
  WHERE tblEvaluationFormDetails.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
//        public DataTable GetEvaluationFormDetails(int id)
//        {
//            string query = @"select EvaluationFormDetailsId, TopicText as topic , failedtext as failed , averagetext as avarage, aboveaveragetext as abvavarage, excellenttext as excellent
// from tblEvaluationFormDetails where 
//EvaluationFormMasterId =" + id + "";
//            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//        }

        public bool DeleteTrainingEvaluationForm(int id, string user)
        {
            try
            {
                bool result = false;
                List<SqlParameter> aperam = new List<SqlParameter>();

                aperam.Add(new SqlParameter("@EvaluationFormMasterId", id));
                aperam.Add(new SqlParameter("@DeleteBy", user));
                aperam.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
                string query = @"update tblEvaluationFormMaster set  IsDelete=1 , DeleteBy = @DeleteBy ,DeleteDate=@DeleteDate where EvaluationFormMasterId = @EvaluationFormMasterId ";


                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, aperam, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion  


        #region Training Requisition


        public int SaveTrainingReqMaster(TrainingRequisitionMaster aMaster, string user)
        {
            try
            {

                int result = 0;

                if (aMaster.TrainingRequisitionMasterId > 0)
                {
                    List<SqlParameter> aPrams = new List<SqlParameter>();
                    aPrams.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aPrams.Add(new SqlParameter("@TrainingRequisitionMasterId", aMaster.TrainingRequisitionMasterId));
                    aPrams.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aPrams.Add(new SqlParameter("@Quater", aMaster.Quater));
                    aPrams.Add(new SqlParameter("@ReqBy", aMaster.ReqBy));
                    aPrams.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                    aPrams.Add(new SqlParameter("@UpdateBy", aMaster.UpdateBy));
                    aPrams.Add(new SqlParameter("@UpdateDate", aMaster.UpdateDate));

                    string query = @"update tblTrainingRequisitionMaster set  TrainingTitle= @TrainingTitle ,CompanyId =@CompanyId , FinancialYearId= @FinancialYearId,
                        ReqBy = @ReqBy , Quater = @Quater , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate where TrainingRequisitionMasterId = @TrainingRequisitionMasterId";
                    bool updateResult = _aCommonInternalDal.UpdateDataByUpdateCommand(query , aPrams , DataBase.HRDB);

                    if (updateResult == true)
                    {
                        result = aMaster.TrainingRequisitionMasterId;
                    }
                    else{
                        result = 0;
                    }
                }
                else
                {
                    List<SqlParameter> aPrams = new List<SqlParameter>();
                    aPrams.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aPrams.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aPrams.Add(new SqlParameter("@Quater", aMaster.Quater));
                    aPrams.Add(new SqlParameter("@ReqBy", aMaster.ReqBy));
                    aPrams.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                    aPrams.Add(new SqlParameter("@EntryBy", aMaster.EntryBy));
                    aPrams.Add(new SqlParameter("@EntryDate", aMaster.EntryDate));
                    aPrams.Add(new SqlParameter("@TrainingReqNumber", "TR:" + GenerateAutoNumber("tblTrainingRequisitionMaster", "TrainingReqNumber", DateTime.Now)));

                    string query = @"insert into tblTrainingRequisitionMaster (TrainingTitle, Quater, CompanyId, FinancialYearId, ReqBy, EntryBy, EntryDate , TrainingReqNumber) 
                                values(@TrainingTitle,@Quater, @CompanyId, @FinancialYearId, @ReqBy, @EntryBy, @EntryDate , @TrainingReqNumber)";



                    result = _aCommonInternalDal.SaveDataByInsertCommandById(query, aPrams, DataBase.HRDB);
                }
               
                return result;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public bool SaveTrainingReqDetails(List<TrainingRequisitionDetails> aList, int aMasterId)
        {


            try
            {
                bool save = false;

                string delQuery = @"delete from tblTrainingRequisitionDetails where TrainingRequisitionMasterId =" + aMasterId + "";
                bool delete = _aCommonInternalDal.DeleteDataByDeleteCommand(delQuery, DataBase.HRDB);
                foreach (var item in aList)
                {
                    List<SqlParameter> aPerams = new List<SqlParameter>();

                    aPerams.Add(new SqlParameter("@TrainingRequisitionMasterId", aMasterId));
                    aPerams.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));


                    string query = @"insert into  tblTrainingRequisitionDetails (TrainingRequisitionMasterId , EmpInfoId) 
                    values (@TrainingRequisitionMasterId , @EmpInfoId)";
                    save = _aCommonInternalDal.SaveDataByInsertCommand(query, aPerams, DataBase.HRDB);

                }

                return save;

            }
            catch (Exception ex)
            {
                
                throw;
            }

        }



        public DataTable GetTrainingRequisitionList()
        {
            try
            {
                string query = @"select a.TrainingRequisitionMasterId ,a.TrainingReqNumber , TrainingTitle , c.ShortName , f.FinancialYearDesc from tblTrainingRequisitionMaster A left join tblCompanyInfo c on a.CompanyId = c.CompanyId 
                                left join tblFinancialYear f on a.FinancialYearId = f.FinancialYearId  where a.IsDelete  is null or a.IsDelete = 0";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public DataTable GetTrainingRequisitionMaster(int id)
        {
            string query = "select * from tblTrainingRequisitionMaster where TrainingRequisitionMasterId = " + id + "";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetTrainingRequisitionDetails(int id)
        {
            string query = @"select a.EmpInfoId as employeeId , (e.EmpMasterCode+':'+e.EmpName+':'+deg.Designation+':'+dpt.DepartmentName)  as employee , m.Quater as quater from tblTrainingRequisitionDetails a 
left join tblEmpGeneralInfo e on a.EmpInfoId  = e.EmpInfoId 
left join tblDepartment dpt on e.DepartmentId = dpt.DepartmentId
left join tblDesignation deg on e.DesignationId = deg.DesignationId 
left join tblTrainingRequisitionMaster m on a.TrainingRequisitionMasterId = m.TrainingRequisitionMasterId
where a.TrainingRequisitionMasterId=" + id + "";
            return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        #endregion


        #region Training calander

        public DataTable GetTrainingCalander()
        {
            try
            {
                aAcessManager.SqlConnectionOpen(DataBase.HRDB);
                DataSet dt = aAcessManager.GetDataSet("sp_Get_TrainingCalander");
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                
                throw;
            }
            finally
            {
                aAcessManager.SqlConnectionClose();
            }
        }
#endregion



        #region TrainingBudget2

        public int SaveTrainingBudget2Master(TrainingBudget2Master amaster, string user)
        {
            try
            {
                int pk = 0;
                if (amaster.TrainingBudget2Id == 0)
                {
                    List<SqlParameter> aParm = new List<SqlParameter>();

                    aParm.Add(new SqlParameter("@CompanyId", amaster.CompanyId));
                    aParm.Add(new SqlParameter("@FinancialYearId", amaster.FinancialYearId));
                    aParm.Add(new SqlParameter("@TotalYearlyBudgetCost", amaster.TotalYearlyBudgetCost));
                    aParm.Add(new SqlParameter("@TotalBudget", amaster.TotalBudget));
                    aParm.Add(new SqlParameter("@EntryBy", user));
                    aParm.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aParm.Add(new SqlParameter("@TrainingBudgetNumber", "TB:" + GenerateAutoNumber("tblTrainingBudget2Master", "TrainingBudgetNumber", System.DateTime.Now)));
                     string query = @"Insert into tblTrainingBudget2Master (CompanyId,FinancialYearId,TotalYearlyBudgetCost,TotalBudget ,EntryBy , EntryDate,TrainingBudgetNumber) 
                            values  (@CompanyId,@FinancialYearId,@TotalYearlyBudgetCost,@TotalBudget ,@EntryBy ,@EntryDate,@TrainingBudgetNumber)";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParm, DataBase.HRDB);
                }

                if (amaster.TrainingBudget2Id > 0)
                {
                    List<SqlParameter> aParm = new List<SqlParameter>();

                    aParm.Add(new SqlParameter("@CompanyId", amaster.CompanyId));
                    aParm.Add(new SqlParameter("@FinancialYearId", amaster.FinancialYearId));
                    aParm.Add(new SqlParameter("@TotalYearlyBudgetCost", amaster.TotalYearlyBudgetCost));
                    aParm.Add(new SqlParameter("@TotalBudget", amaster.TotalBudget));
                    aParm.Add(new SqlParameter("@TrainingBudget2Id", amaster.TrainingBudget2Id));
                    aParm.Add(new SqlParameter("@UpdateBy", user));
                    aParm.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                    string query =@"update  tblTrainingBudget2Master set CompanyId =@CompanyId , FinancialYearId = @FinancialYearId 
                                    ,TotalYearlyBudgetCost = @TotalYearlyBudgetCost , TotalBudget = @TotalBudget  ,UpdateBy = @UpdateBy 
                                     , UpdateDate = @UpdateDate where  TrainingBudget2Id = @TrainingBudget2Id ";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParm, DataBase.HRDB);
                    if (result == true)
                    {
                        pk = amaster.TrainingBudget2Id;
                    }
                    else
                    {
                        pk = 0;
                    }
                }

                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public int SaveTrainingMarksMaster(TrainingMarksMasterDAO amaster)
        {
            try
            {
                int pk = 0;
                if (amaster.TrainigMarkId == 0)
                {
                    List<SqlParameter> aParm = new List<SqlParameter>();

                    aParm.Add(new SqlParameter("@TrainingRecordMasterId", amaster.TrainingRecordMasterId));
                    aParm.Add(new SqlParameter("@OutOfMark", amaster.OutOfMark));
                    aParm.Add(new SqlParameter("@Remarks", amaster.Remarks));
                    aParm.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                    aParm.Add(new SqlParameter("@EntryDate", DateTime.Now));

                    string query = @"INSERT INTO dbo.tblTrainingMarksMaster
                                    (
                                        TrainingRecordMasterId,
                                        OutOfMark,EntryBy,EntryDate,Remarks
                                    )
                                    VALUES
                                    (   @TrainingRecordMasterId,
                                        @OutOfMark,@EntryBy,@EntryDate,@Remarks
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParm, DataBase.HRDB);
                }

                if (amaster.TrainigMarkId > 0)
                {
                    List<SqlParameter> aParm = new List<SqlParameter>();

                    aParm.Add(new SqlParameter("@TrainingRecordMasterId", amaster.TrainingRecordMasterId));
                    aParm.Add(new SqlParameter("@TrainigMarkId", amaster.TrainigMarkId));
                    aParm.Add(new SqlParameter("@OutOfMark", amaster.OutOfMark));
                    aParm.Add(new SqlParameter("@Remarks", amaster.Remarks));
                    aParm.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                    aParm.Add(new SqlParameter("@UpdateDate", DateTime.Now));

                    string query = @"UPDATE dbo.tblTrainingMarksMaster SET
                                    
                                        TrainingRecordMasterId=@TrainingRecordMasterId,
                                        OutOfMark=@OutOfMark,Remarks=@Remarks,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate WHERE TrainigMarkId=@TrainigMarkId
                                    ";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParm, DataBase.HRDB);
                    if (result == true)
                    {
                        pk = amaster.TrainigMarkId;
                    }
                    else
                    {
                        pk = 0;
                    }
                }

                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
       
        public int SaveTrainingMarksDetail(List<TrainingMarksDetailDAO> aDetail,int pkid)
        {
            try
            {
                string delQ = @"Delete from tblTrainingMarkDetail where  TrainigMarkId= " + pkid+ " ";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);
                int pk = 0;
                foreach (var amaster in aDetail)
                {
//                 
                    //if (amaster.TrainingMarkDetailId == 0)
                    {
                        List<SqlParameter> aParm = new List<SqlParameter>();

                        aParm.Add(new SqlParameter("@TrainingRecordDetailsEmp", amaster.TrainingRecordDetailsEmp));
                        aParm.Add(new SqlParameter("@TrainigMarkId", amaster.TrainigMarkId));
                        aParm.Add(new SqlParameter("@EmpInfoId", amaster.EmpInfoId));
                        aParm.Add(new SqlParameter("@PreMark", amaster.PreMark));
                        aParm.Add(new SqlParameter("@PostMark", amaster.PostMark));

                        string query = @"INSERT INTO dbo.tblTrainingMarkDetail
                                    (
                                        TrainingRecordDetailsEmp,TrainigMarkId,
                                        EmpInfoId,
                                        PreMark,
                                        PostMark
                                    )
                                    VALUES
                                    (   @TrainingRecordDetailsEmp,@TrainigMarkId,
                                        @EmpInfoId,
                                        @PreMark,
                                        @PostMark
                                    )";

                        pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParm, DataBase.HRDB);
                    }
                }
                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public void TrainingRecordDropDown(DropDownList ddl,string companyId,string finYearId)
        {
            string queryStr = @"SELECT * FROM dbo.tblTrainingRecordMaster WHERE   ( IsDelete IS NULL OR IsDelete = 0)   AND CompanyId='" + companyId + "' AND FinancialYearId='" + finYearId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "TrainingTitle", "TrainingRecordMasterId", queryStr, DataBase.HRDB);
        }
        public bool SaveTrainingBudget2Details(List<TrainingBudget2Details> aDetails, int pk)
        {
            try
            {
                string delQ = @"Delete from tblTrainingBudget2Details where  TrainingBudget2Id= " + pk + " ";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);
                bool result = false;
                foreach (var item in aDetails)
                {
//                    if (item.TrainingBudget2DetailsId > 0)
//                    {
//                        List<SqlParameter> aPeList = new List<SqlParameter>();
//                        aPeList.Add(new SqlParameter("@TrainingBudget2DetailsId", item.TrainingBudget2DetailsId));
//                        aPeList.Add(new SqlParameter("@TrainingBudget2Id", pk));
//                        aPeList.Add(new SqlParameter("@TrainingTitle", item.TrainingTitle));
//                        aPeList.Add(new SqlParameter("@ExpectedResult", item.ExpectedResult));
//                        aPeList.Add(new SqlParameter("@Quater", item.Quater));
//                        aPeList.Add(new SqlParameter("@Grade", item.Grade));
//                        aPeList.Add(new SqlParameter("@EmpCategoryId", item.EmpCategoryId));
//                        aPeList.Add(new SqlParameter("@MonthId", item.MonthId));
//                        aPeList.Add(new SqlParameter("@IsInternal", item.IsInternal));
//                        aPeList.Add(new SqlParameter("@IsExternal", item.IsExternal));
//                        aPeList.Add(new SqlParameter("@IsForeign", item.IsForeign));
//                        aPeList.Add(new SqlParameter("@IsLocal", item.IsLocal));
//                        aPeList.Add(new SqlParameter("@TotalParticipant", item.TotalParticipant));
//                        aPeList.Add(new SqlParameter("@BudgetCostParticipant", item.BudgetCostParticipant));
//                        aPeList.Add(new SqlParameter("@Budget", item.Budget));
//                        aPeList.Add(new SqlParameter("@Referance", item.Referance));
//                        aPeList.Add(new SqlParameter("@Remarks", item.Remarks));
//                        aPeList.Add(new SqlParameter("@UpdateBy", item.UpdateBy));
//                        aPeList.Add(new SqlParameter("@UpdateDate", item.UpdateDate));


//                        string query = @"UPDATE  dbo.tblTrainingBudget2Details SET
//                             
//                              TrainingTitle=@TrainingTitle ,
//                              ExpectedResult=@ExpectedResult ,
//                              Quater=@Quater ,
//                                Grade=@Grade, EmpCategoryId=@EmpCategoryId,
//                              MonthId=NULLIF(@MonthId,'0') ,
//                              IsInternal=@IsInternal ,
//                              IsExternal=@IsExternal ,
//                              IsForeign=@IsForeign ,
//                              IsLocal=@IsLocal ,
//                              TotalParticipant=@TotalParticipant ,
//                              BudgetCostParticipant=@BudgetCostParticipant ,
//                              Budget=@Budget ,
//                              Referance=@Referance ,
//                              Remarks=@Remarks,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate WHERE TrainingBudget2DetailsId=@TrainingBudget2DetailsId
//                        ";


//                        result = _aCommonInternalDal.SaveDataByInsertCommand(query, aPeList, DataBase.HRDB);
//                    }
//                    else
                    {


                        List<SqlParameter> aPeList = new List<SqlParameter>();
                        aPeList.Add(new SqlParameter("@TrainingBudget2Id", pk));
                        aPeList.Add(new SqlParameter("@TrainingTitle", item.TrainingTitle));
                        aPeList.Add(new SqlParameter("@ExpectedResult", item.ExpectedResult));
                        aPeList.Add(new SqlParameter("@Quater", item.Quater));
                        aPeList.Add(new SqlParameter("@Grade", item.Grade));
                        aPeList.Add(new SqlParameter("@EmpCategoryId", item.EmpCategoryId));
                        aPeList.Add(new SqlParameter("@MonthId", item.MonthId));
                        aPeList.Add(new SqlParameter("@IsInternal", item.IsInternal));
                        aPeList.Add(new SqlParameter("@IsExternal", item.IsExternal));
                        aPeList.Add(new SqlParameter("@IsForeign", item.IsForeign));
                        aPeList.Add(new SqlParameter("@IsLocal", item.IsLocal));
                        aPeList.Add(new SqlParameter("@TotalParticipant", item.TotalParticipant));
                        aPeList.Add(new SqlParameter("@BudgetCostParticipant", item.BudgetCostParticipant));
                        aPeList.Add(new SqlParameter("@Budget", item.Budget));
                        aPeList.Add(new SqlParameter("@Referance", item.Referance));
                        aPeList.Add(new SqlParameter("@Remarks", item.Remarks));
                        aPeList.Add(new SqlParameter("@EntryBy", item.EntryBy));
                        aPeList.Add(new SqlParameter("@EntryDate", item.EntryDate));


                        string query = @"INSERT INTO dbo.tblTrainingBudget2Details
                            ( TrainingBudget2Id ,
                              TrainingTitle ,
                              ExpectedResult ,
                              Quater ,
                                Grade, EmpCategoryId,
                              MonthId ,
                              IsInternal ,
                              IsExternal ,
                              IsForeign ,
                              IsLocal ,
                              TotalParticipant ,
                              BudgetCostParticipant ,
                              Budget ,
                              Referance ,
                              Remarks,EntryBy,EntryDate
                        )
                VALUES  ( @TrainingBudget2Id ,
                          @TrainingTitle ,
                          @ExpectedResult ,
                          @Quater ,
                          @Grade, @EmpCategoryId,
                          NULLIF(@MonthId,'0') ,
                          @IsInternal ,
                          @IsExternal ,
                          @IsForeign ,
                          @IsLocal ,
                          @TotalParticipant ,
                          @BudgetCostParticipant ,
                          @Budget ,
                          @Referance ,
                          @Remarks,@EntryBy,@EntryDate
                        )";


                        result = _aCommonInternalDal.SaveDataByInsertCommand(query, aPeList, DataBase.HRDB);
                    }
                }
                return result;



            }
            catch (Exception ex)
            {
                    
                throw ex;
            }
        }


        public DataTable TrainingBudget2List(string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblTrainingBudget2Master A 
                                
                                LEFT JOIN dbo.tblCompanyInfo c ON A.CompanyId =  c.CompanyId
                                LEFT JOIN dbo.tblFinancialYear fy ON A.FinancialYearId = fy.FinancialYearId
                                WHERE (A.IsDelete IS NULL OR A.IsDelete  = 0 ) " + param + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                    
                throw ex;
            }

        }

        public DataTable GetTrainingBudget2ById(int id)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblTrainingBudget2Master
								LEFT JOIN dbo.tblTrainingBudget2Details ON tblTrainingBudget2Details.TrainingBudget2Id = tblTrainingBudget2Master.TrainingBudget2Id where tblTrainingBudget2Master.TrainingBudget2Id = " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                    
                throw;
            }
        }

        public DataTable GetTrainingBudget2Details(int id )
        {
            try
            {
                string query = @"sELECT	(case when MonthId is null then '0' else MonthId end )MonthId, A.EmpCategoryId Category,A.*,
		 Quater,
		m.MonthName AS Month, 
		
		CASE WHEN A.IsInternal = 1  AND A.IsExternal = 0 THEN 'Internal'
		 WHEN  A.IsExternal = 1 AND A.IsInternal = 0 THEN 'External'   END AS InternalExternal,
		CASE WHEN A.IsForeign = 1 AND A.IsLocal = 0 THEN 'Foreign'
		WHEN A.IsLocal=1 AND A.IsForeign= 0 THEN 'Local' END AS ForeignLocal
		 FROM dbo.tblTrainingBudget2Details A  
		
		 LEFT JOIN dbo.tblQuarterMonthInfo m ON m.QuarterMonthId = a.MonthId
		 WHERE A.TrainingBudget2Id= " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
#endregion


        #region training Requisition 2
        public int SaveTrainingRequisition2Master(TrainingRequisition2Master aMaster, string user)
        {
            try
            {
                int pk = 0;
                if (aMaster.TrainingRequisition2Id == 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@RequisitionBy", aMaster.RequisitionBy));
                    aParameters.Add(new SqlParameter("@RequisitionNo", "TB:" + GenerateAutoNumber("tblTrainingRequisition2Master", "RequisitionNo", System.DateTime.Now)));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                    string query = @"INSERT INTO dbo.tblTrainingRequisition2Master
                                    ( CompanyId ,
                                      FinancialYearId ,
                                      RequisitionBy ,
                                      EntryBy ,
                                      EntryDate ,         
                                      RequisitionNo
                                    )
                            VALUES  ( @CompanyId ,
                                      @FinancialYearId ,
                                      @RequisitionBy ,
                                      @EntryBy ,
                                      @EntryDate ,          
                                      @RequisitionNo
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);


                }

                return pk;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool SaveTrainingReq2Details(List<TrainingRequisition2Details> aList, int pk)
        {
            try
            {
                bool result = false;
                string delQ = @"delete from  tblTrainingRequisition2Details where TrainingRequisition2Id = " + pk + "";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);


                foreach (var item in aList)
                {
                    List<SqlParameter> aPeram  = new List<SqlParameter>();

                    aPeram.Add(new SqlParameter("@TrainingTitle", item.TrainingTitle));
                    aPeram.Add(new SqlParameter("@ExpectedResult", item.ExpectedResult));
                    aPeram.Add(new SqlParameter("@QuaterId", item.QuaterId));
                    aPeram.Add(new SqlParameter("@MonthId", item.MonthId));
                    aPeram.Add(new SqlParameter("@TrainingRequisition2Id", item.TrainingRequisition2Id));

                    string query =@"insert into tblTrainingRequisition2Details (TrainingRequisition2Id ,QuaterId , MonthId , TrainingTitle , ExpectedResult )
                    values(@TrainingRequisition2Id ,@QuaterId , @MonthId , @TrainingTitle , @ExpectedResult)";

                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aPeram, DataBase.HRDB);
                    if (result == false)
                    {
                        
                        return false;
                    }
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
       
#endregion


        #region Training Budget2

        public DataTable GetTrainingBudget2(int finYear, int quater)
        {
            try
            {
                string query = @"SELECT b.TrainingTitle AS TextField  , b.TrainingBudget2DetailsId AS value FROM dbo.tblTrainingBudget2Master A LEFT JOIN dbo.tblTrainingBudget2Details B ON B.TrainingBudget2Id = A.TrainingBudget2Id
	   WHERE a.FinancialYearId = " + finYear + " AND b.QuaterId = " + quater + "";
                return _aCommonInternalDal.GetDTforDDL(query, null,DataBase.HRDB);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public DataTable GetTrainingRequistion2(int year, int quater)
        {
            string query = @" SELECT b.TrainingTitle AS TextField , b.TrainingRequisition2DetailsId AS value FROM tblTrainingRequisition2Master A LEFT JOIN dbo.tblTrainingRequisition2Details b ON a.TrainingRequisition2Id = b.TrainingRequisition2Id
	   WHERE a.FinancialYearId = "+year+" AND b.QuaterId = "+quater+"";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public bool DeleteBudget2(int id, string user)
        {
            bool result = false;
            List<SqlParameter> aperam = new List<SqlParameter>();

            aperam.Add(new SqlParameter("@TrainingBudget2Id", id));
            aperam.Add(new SqlParameter("@DeleteBy", user));
            aperam.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
            string query = @"update tblTrainingBudget2Master set  IsDelete=1 , DeleteBy = @DeleteBy ,DeleteDate=@DeleteDate where TrainingBudget2Id = @TrainingBudget2Id ";


            result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, aperam, DataBase.HRDB);
            return result;

        }
#endregion


        #region Training Setup2

        public int SaveTrainingSetup2(TrainingSetup2Master aMaster, string user)
        {
            try
            {

                int pk = 0;
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                aParameters.Add(new SqlParameter("@TrainingDetails", aMaster.TrainingDetails));
                aParameters.Add(new SqlParameter("@FromReq", aMaster.FromReq));
                aParameters.Add(new SqlParameter("@TrainingReq2DetailsId", aMaster.TrainingReq2DetailsId));
                aParameters.Add(new SqlParameter("@TrainingBudget2DetailsId", aMaster.TrainingBudget2DetailsId));
                aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                aParameters.Add(new SqlParameter("@StartDate", aMaster.StartDate));
                aParameters.Add(new SqlParameter("@EndDate", aMaster.EndDate));
                aParameters.Add(new SqlParameter("@Duration", aMaster.Duration));
                aParameters.Add(new SqlParameter("@TrainingOrgId", aMaster.TrainingOrgId));
                aParameters.Add(new SqlParameter("@OfficeBranchId", aMaster.OfficeBranchId));
                aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                aParameters.Add(new SqlParameter("@TrainingSetupNo", "TS:" + GenerateAutoNumber("tblTrainingSetup2Master", "TrainingSetupNo", System.DateTime.Now)));
                aParameters.Add(new SqlParameter("@EntryBy", user));

                string query = @"insert into tblTrainingSetup2Master 
                                    (TrainingTitle ,  TrainingDetails , FromReq , TrainingReq2DetailsId,  TrainingBudget2DetailsId, CompanyId, FinancialYearId, StartDate, EndDate,
                                   Duration , EntryDate , TrainingSetupNo , EntryBy, TrainingOrgId , OfficeBranchId ) values  (@TrainingTitle ,  @TrainingDetails , @FromReq , @TrainingReq2DetailsId,  @TrainingBudget2DetailsId, @CompanyId, @FinancialYearId, @StartDate, @EndDate,
                                   @Duration , @EntryDate , @TrainingSetupNo , @EntryBy , @TrainingOrgId , @OfficeBranchId) ";
                 
                pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                return pk;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public bool SaveTrainingSetup2Details(List<TrainingSetup2Details> aList, int pk)
        {
            try
            {
                string delQ = @"Delete from tblTrainingSetup2Details where TrainingSetup2MasterId = " + pk + "";
                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingSetup2MasterId", item.TrainingSetup2MasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                    string query =@"Insert into tblTrainingSetup2Details ( TrainingSetup2MasterId ,EmpInfoId ) values (@TrainingSetup2MasterId ,@EmpInfoId)";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                    if (result == false)
                    {
                        return false;
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

#endregion
        public bool UpdateStatus(string TrainingRecordMasterId, string status, string approveby, DateTime approvedate)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@TrainingRecordMasterId", TrainingRecordMasterId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));
            aSqlParameterlist.Add(new SqlParameter("@ApproveBy", approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", approvedate));

            const string query = @"UPDATE dbo.tblTrainingRecordMaster SET ActionStatus=@ActionStatus,ApproveBy=@ApproveBy,ApproveDate=@ApproveDate WHERE TrainingRecordMasterId=@TrainingRecordMasterId";
            return _aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public DataTable GetEvaluationForm()
        {
            //            string Query = @"select a.EvaluationFormMasterId,a.EvaluationFormNo , b.TrainingSetupNumber , b.TrainingTitle   , co.ShortName , fy.FinancialYearDesc  
            //
            //                            from tblEvaluationFormMaster A left join tblTrainingMaster B on a.TraingMasreId = b.TrainingMasterId
            //                            left join tblFinancialYear fy on b.FinancialYearId = fy.FinancialYearId
            //                            left join tblCompanyInfo co on b.CompanyId = co.CompanyId where a.IsDelete is null or a.IsDelete=0";


            string Query = @"SELECT TRM.TrainingRecordMasterId ,CI.CompanyName,FINY.FinancialYearDesc,TRM.TrainingTitle,T.TrainingType,EGI.EmpName FROM dbo.tblTrainingRecordMaster AS TRM 
                             INNER JOIN tbl_trainingRecordDetailsEmployee AS TRME ON TRM.TrainingRecordMasterId = TRME.TrainingRecordMasterId
                             LEFT JOIN tblEmpGeneralInfo AS EGI ON TRME.EmpInfoId = EGI.EmpInfoId
                             INNER JOIN tblCompanyInfo AS CI ON CI.CompanyId = TRM.CompanyId
                             INNER JOIN tblFinancialYear AS FINY ON TRM.FinancialYearId = FINY.FinancialYearId
                             INNER JOIN dbo.tblTrainingType T ON TRM.TrainingTypeID = T.TrainingTypeID 
                             WHERE TRM.IsDelete = 0 OR TRM.IsDelete IS NULL";

            return _aCommonInternalDal.DataContainerDataTable(Query, DataBase.HRDB);
        }


        public DataTable GetTrainingList(string param)
        {
            string query = @"SELECT M.TrainingRecordMasterId,C.CompanyName,M.TrainingTitle ,F.FinancialYearDesc,M.TrainingTypeId,T.TrainingType
				FROM tblTrainingRecordMaster M
				INNER JOIN dbo.tblCompanyInfo C ON M.CompanyId = C.CompanyId
				INNER JOIN dbo.tblFinancialYear F ON M.FinancialYearId = F.FinancialYearId
					INNER JOIN dbo.tblTrainingType T ON M.TrainingTypeID = T.TrainingTypeID

					WHERE (M.IsDelete = 0 OR M.IsDelete is null) "+param+"" ;

            DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            return dt;
        }

        public void DateDropDown(DropDownList ddl,string id)
        {
            string query = @"SELECT REPLACE(REPLACE(CONVERT(VARCHAR,Date,106), ' ','-'), ',','')Date,* FROM dbo.tblTrainingRecordScheDate WHERE TrainingRecordMasterId='" + id + "'";

            _aCommonInternalDal.LoadDropDownValue(ddl, "Date", "TrainingRecordScheDateId", query, DataBase.HRDB);
        }
        public DataTable GetEmployeeForAttendance(int id)
        {
            string query = @"SELECT EGI.EmpInfoId,TRM.TrainingRecordMasterId,EGI.EmpMasterCode AS EmpMasterCode,desg.Designation AS Designation,dpt.DepartmentName AS DepartmentName,CI.CompanyName,FINY.FinancialYearDesc,TRM.TrainingTitle,T.TrainingType,EGI.EmpName AS EmpName,* 
FROM dbo.tblTrainingRecordMaster AS TRM 

INNER JOIN tbl_trainingRecordDetailsEmployee AS TRME ON TRM.TrainingRecordMasterId = TRME.TrainingRecordMasterId
INNER JOIN tblEmpGeneralInfo AS EGI ON TRME.EmpInfoId = EGI.EmpInfoId
INNER JOIN tblCompanyInfo AS CI ON CI.CompanyId = TRM.CompanyId
INNER JOIN tblFinancialYear AS FINY ON TRM.FinancialYearId = FINY.FinancialYearId
INNER JOIN dbo.tblTrainingType T ON TRM.TrainingTypeID = T.TrainingTypeID
   left join tblDesignation desg on EGI.DesignationId = desg.DesignationId
                            left join tblDepartment dpt on EGI.DepartmentId = dpt.DepartmentId where  TRM.TrainingRecordMasterId = " + id + " and (TRM.IsDelete = 0 OR TRM.IsDelete is null)";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetTrainingMarksDetail(string empId,string recordMasterId,string recorddetailemp   )
        {
            string query = @"SELECT * FROM dbo.tblTrainingMarkDetail
LEFT JOIN dbo.tblTrainingMarksMaster ON dbo.tblTrainingMarksMaster.TrainigMarkId=dbo.tblTrainingMarkDetail.TrainigMarkId
WHERE EmpInfoId='"+empId+"' AND TrainingRecordMasterId='"+recordMasterId+"' AND TrainingRecordDetailsEmp='"+recorddetailemp+"'";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetAttandance(int id,string attdate,string schedateId,string empinfoId)
        {
            string query = @"SELECT * FROM dbo.tblTrainingAttendance WHERE TrainingRecordMasterId='"+id+"' AND ATTDate='"+attdate+"' AND TrainingRecordScheDateId='"+schedateId+"' AND EmpInfoId='"+empinfoId+"'";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetPermissionForEvulate(string MasterId, string empinfoId)
        {
            try
            {
                string query = @"  SELECT *
 FROM  dbo.tblEvaluateTrainingMaster
 
	WHERE TrainingRecordMasterId=" + MasterId + "  AND EmpInfoId= " + empinfoId + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
                string query = @"SELECT * FROM dbo.tblTrainingBudget2MasterAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND TrainingBudget2Id='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
                    aParameters.Add(new SqlParameter("@TrainingBudget2AppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblTrainingBudget2MasterAppLog set ActionStatus=@ActionStatus  where TrainingBudget2AppLogId = @TrainingBudget2AppLogId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public int SavAppLog(TrainingBudget2MasterAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingBudget2Id", appLogDao.TrainingBudget2Id));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId ?? (object)DBNull.Value));


                    string query = @"INSERT INTO dbo.tblTrainingBudget2MasterAppLog
                                    (
                                    TrainingBudget2Id,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @TrainingBudget2Id,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblTrainingBudget2MasterAppLog WHERE TrainingBudget2Id=@TrainingBudget2Id),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsId
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
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


                    string query = @" INSERT INTO dbo.tblTrainingBudget2MasterAppLogComnt
                                    (
                                        EmpInfoId,
                                        Comments
                                    )
                                    VALUES
                                    (   @EmpInfoId,
                                        @Comments
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
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
                string query = @"SELECT * FROM dbo.tblTrainingBudget2MasterAppLog WHERE ForEmpInfoId='" + forempId + "' AND TrainingBudget2Id='" + mid + "' AND ActionStatus<>'Review'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEmpInfo(string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblEmpGeneralInfo " + param + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
                    aParameters.Add(new SqlParameter("@TrainingBudget2Id", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", actionstatus));


                    string query =
                        @"update tblTrainingBudget2Master set ActionStatus=@ActionStatus  where TrainingBudget2Id = @TrainingBudget2Id";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }
        public bool UpdateJobReqStatus2(TrainingBudget2Master aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.TrainingBudget2Id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingBudget2Id", aMaster.TrainingBudget2Id));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblTrainingBudget2Master set ActionStatus2=@ActionStatus  where TrainingBudget2Id = @TrainingBudget2Id";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
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

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable TrainingBudget2ListApp()
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblTrainingBudget2Master A 
                                
                                LEFT JOIN dbo.tblCompanyInfo c ON A.CompanyId =  c.CompanyId
                                LEFT JOIN dbo.tblFinancialYear fy ON A.FinancialYearId = fy.FinancialYearId
                                INNER JOIN (SELECT TrainingBudget2Id,MAX(Version)MaxVer FROM dbo.tblTrainingBudget2MasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY TrainingBudget2Id) AS CELog ON CELog.TrainingBudget2Id= A.TrainingBudget2Id
								INNER JOIN dbo.tblTrainingBudget2MasterAppLog ON tblTrainingBudget2MasterAppLog.TrainingBudget2Id = A.TrainingBudget2Id
                                where (A.IsDelete is null or A.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




    }





}

