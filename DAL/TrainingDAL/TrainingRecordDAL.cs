using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.TrainingDAL
{
    public class TrainingRecordDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable GetTrainingTitleByHead(int id, string prefixText)
        {
            try
            {

                var aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@id", id));
                aSqlParameterlist.Add(new SqlParameter("@TrainingTitle", prefixText.ToLower()));
                string query = @"SELECT * FROM dbo.tblTrainingBudget2Details WHERE TrainingBudget2Id = @id AND LOWER(TrainingTitle) LIKE '%'+ @TrainingTitle +'%' ";
                return _aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTrainingHeadByYear(int financialYear, int CompanyID)
        {
            try
            {
                var aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@financialYear", financialYear));
                aSqlParameterlist.Add(new SqlParameter("@CompanyID", CompanyID));

                string query = @"SELECT  B.TrainingBudget2DetailsId AS Value ,
                                A.TrainingBudgetNumber + ':' + B.TrainingTitle AS TextField
                        FROM    dbo.tblTrainingBudget2Master A 
						LEFT JOIN dbo.tblTrainingBudget2Details b ON A.TrainingBudget2Id = b.TrainingBudget2Id
                        WHERE   A.FinancialYearId = @financialYear and A.CompanyId=@CompanyID 
                                AND ( A.IsDelete IS NULL
                                      OR A.IsDelete = 0
                                    )
									 AND ( b.IsDelete IS NULL
                                      OR b.IsDelete = 0
                                    )";
                return _aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdateStatus(string id, string status)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@TrainingRecordMasterId", id));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));

            const string query = @"UPDATE dbo.TrainingRecordMasterId SET ActionStatus=@ActionStatus WHERE TrainingRecordMasterId=@TrainingRecordMasterId";
            return _aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public DataTable GetTrainingType()
        {
            try
            {
                string query = @"SELECT  TrainingTypeID AS Value ,
                                TrainingType AS TextField
                        FROM    dbo.tblTrainingType
                        WHERE   IsActive = 1
                                AND (IsDeleted IS NULL OR IsDeleted = 0)";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTrainingVenue()
        {
            try
            {
                string query = @"SELECT SMCVenueID AS Value , VenueName AS TextField 
                    FROM dbo.tblSMCTrainingVenue WHERE IsActive  = 1 AND (IsDeleted IS NULL OR IsDeleted = 0)";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetDepartmentByCompany(int id)
        {
            try
            {
                string query = @"SELECT DepartmentId AS value , DepartmentName AS TextField FROM dbo.tblDepartment WHERE   (Invisible =0 OR Invisible IS NULL)  AND  IsActive = 1  AND CompanyId = " + id + "";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetDepartmentByCompanyPopUp(int id)
        {
            try
            {
                string query = @"SELECT DepartmentId AS value , (DepartmentName +' [ Division: ' + tblDivision.DivisionName +' ]') AS TextField FROM dbo.tblDepartment  with (nolock)
LEFT JOIN dbo.tblDivisionWing  with (nolock) ON dbo.tblDivisionWing.DivisionWId=dbo.tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision  with (nolock) ON dbo.tblDivision.DivisionId=tblDivisionWing.DivisionId

WHERE   (tblDepartment.Invisible =0 OR tblDepartment.Invisible IS NULL)  AND  tblDepartment.IsActive = 1  AND tblDepartment.CompanyId = " + id + "";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSalaryGrade()
        {
            try
            {
                string query = @"SELECT SalaryGradeId AS   value , GradeCode +ISNULL(+':'+GradeName,'') AS TextField   FROM dbo.tblSalaryGrade with (nolock) WHERE IsActive = 1";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEmployee(string param)
        {
            try
            {

              
                string query =
                    @"SELECT A.EmpInfoId , A.EmpMasterCode , A.EmpName , desg.Designation , dpt.DepartmentName , sg.GradeName FROM dbo.tblEmpGeneralInfo A 
                    LEFT JOIN dbo.tblDepartment dpt ON a.DepartmentId = dpt.DepartmentId
                    LEFT JOIN dbo.tblSalaryGrade sg ON a.SalaryGradeId = sg.SalaryGradeId
                    LEFT JOIN dbo.tblDesignation desg ON a.DesignationId = desg.DesignationId
                    LEFT JOIN dbo.tblCompanyInfo cc ON a.CompanyId = cc.CompanyId
                    WHERE   A.IsActive = 1 AND A.EmployeeStatus='Active'  " + param;
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetEmployee(int company, int department, int grade)
        {
            try
            {

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@CompanyId", company));
                aParameters.Add(new SqlParameter("@DepartmentId", department));
                aParameters.Add(new SqlParameter("@SalaryGradeId", grade));
                string query =
                    @"SELECT A.EmpInfoId , A.EmpMasterCode , A.EmpName , desg.Designation , dpt.DepartmentName , sg.GradeName FROM dbo.tblEmpGeneralInfo A 
                    LEFT JOIN dbo.tblDepartment dpt ON a.DepartmentId = dpt.DepartmentId
                    LEFT JOIN dbo.tblSalaryGrade sg ON a.SalaryGradeId = sg.SalaryGradeId
                    LEFT JOIN dbo.tblDesignation desg ON a.DesignationId = desg.DesignationId
                    LEFT JOIN dbo.tblCompanyInfo cc ON a.CompanyId = cc.CompanyId
                    WHERE  a.CompanyId =  COALESCE( NULLIF(@CompanyId , 0) ,a.CompanyId ) AND a.DepartmentId = COALESCE( NULLIF(@DepartmentId , 0) ,a.DepartmentId )  
                    AND a.SalaryGradeId = COALESCE( NULLIF(@SalaryGradeId , 0) ,a.SalaryGradeId ) AND A.IsActive = 1 AND A.EmployeeStatus='Active'";
                return _aCommonInternalDal.DataContainerDataTable(query, aParameters, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public int SaveTrainingRecodMaster(TrainingRecordMaster aMaster, int user)
        {
            try
            {

                int pk = 0;
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                aParameters.Add(new SqlParameter("@TrainingTypeId", aMaster.TrainingTypeId));
                aParameters.Add(new SqlParameter("@TrainingBudget2Id", aMaster.TrainingBudget2Id));
                aParameters.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                aParameters.Add(new SqlParameter("@TrainingDetails", aMaster.TrainingDetails));
                aParameters.Add(new SqlParameter("@TrainingOrgId", aMaster.TrainingOrgId));
                aParameters.Add(new SqlParameter("@TrainingOrgLocation", aMaster.TrainingOrgLocation));
                aParameters.Add(new SqlParameter("@TrainingVenue", aMaster.TrainingVenue));

           
                aParameters.Add(new SqlParameter("@TrainingCost", aMaster.TrainingCost ?? (object)DBNull.Value));
                aParameters.Add(new SqlParameter("@LogisticCost", aMaster.LogisticCost ?? (object)DBNull.Value));
                aParameters.Add(new SqlParameter("@OtherCost", aMaster.OtherCost ?? (object)DBNull.Value));
                aParameters.Add(new SqlParameter("@GrandTotal", aMaster.GrandTotal ?? (object)DBNull.Value));
                aParameters.Add(new SqlParameter("@CostPerParticipant", aMaster.CostPerParticipant));
                aParameters.Add(new SqlParameter("@TrainingDays", aMaster.TrainingDays));
                aParameters.Add(new SqlParameter("@NoOfDays", aMaster.NoOfDays));
                aParameters.Add(new SqlParameter("@StartDate", aMaster.StartDate));
                aParameters.Add(new SqlParameter("@EndDate", aMaster.EndDate));
                aParameters.Add(new SqlParameter("@StartTime", aMaster.StartTime));
                aParameters.Add(new SqlParameter("@EndTime", aMaster.EndTime));
                aParameters.Add(new SqlParameter("@TotalHoure", aMaster.TotalHoure));
                aParameters.Add(new SqlParameter("@TrainingRecordNo", "TR:" + GenerateAutoNumber("tblTrainingRecordMaster", "TrainingRecordNo", System.DateTime.Now)));
                aParameters.Add(new SqlParameter("@EntryBy", user));
                aParameters.Add(new SqlParameter("@EntryDate", DateTime.Now));
                aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));

                string query = @"INSERT INTO dbo.tblTrainingRecordMaster
                                    ( CompanyId ,
                                      FinancialYearId ,
                                      TrainingTypeId ,
                                      TrainingBudget2Id ,
                                      TrainingTitle ,
                                      TrainingDetails ,
                                      TrainingOrgId ,
                                      TrainingOrgLocation ,
                                      TrainingVenue ,
                                      TrainingCost ,
                                      LogisticCost ,
                                      OtherCost ,
                                      TrainingDays ,
                                      NoOfDays ,
                                      StartDate ,
                                      EndDate ,
                                      StartTime ,
                                      EndTime ,
                                      TotalHoure ,
                                      EntryBy ,
                                      EntryDate ,
                                      TrainingRecordNo ,
                                      GrandTotal ,
                                      CostPerParticipant,ActionStatus
                                    )
                            VALUES  ( @CompanyId ,
                                      @FinancialYearId ,
                                      @TrainingTypeId ,
                                      @TrainingBudget2Id ,
                                      @TrainingTitle ,
                                      @TrainingDetails ,
                                      @TrainingOrgId ,
                                      @TrainingOrgLocation ,
                                      @TrainingVenue ,
                                      @TrainingCost ,
                                      @LogisticCost ,
                                      @OtherCost ,
                                      @TrainingDays ,
                                      @NoOfDays ,
                                      @StartDate ,
                                      @EndDate ,
                                      @StartTime ,
                                      @EndTime ,
                                      @TotalHoure ,
                                      @EntryBy ,
                                      @EntryDate ,
                                      @TrainingRecordNo ,
                                      @GrandTotal ,
                                      @CostPerParticipant,@ActionStatus
                                    )";
                pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);

                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public int UpdateTrainingRecord(TrainingRecordMaster aMaster, int id, int user)
        {
            try
            {
                
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                aParameters.Add(new SqlParameter("@TrainingTypeId", aMaster.TrainingTypeId));
                aParameters.Add(new SqlParameter("@TrainingBudget2Id", aMaster.TrainingBudget2Id));
                aParameters.Add(new SqlParameter("@TrainingTitle", aMaster.TrainingTitle));
                aParameters.Add(new SqlParameter("@TrainingDetails", aMaster.TrainingDetails));
                aParameters.Add(new SqlParameter("@TrainingOrgId", aMaster.TrainingOrgId));
                aParameters.Add(new SqlParameter("@TrainingOrgLocation", aMaster.TrainingOrgLocation));
                aParameters.Add(new SqlParameter("@TrainingVenue", aMaster.TrainingVenue));
                aParameters.Add(new SqlParameter("@TrainingCost", aMaster.TrainingCost));
                aParameters.Add(new SqlParameter("@LogisticCost", aMaster.LogisticCost));
                aParameters.Add(new SqlParameter("@OtherCost", aMaster.OtherCost));
                aParameters.Add(new SqlParameter("@GrandTotal", aMaster.GrandTotal));
                aParameters.Add(new SqlParameter("@CostPerParticipant", aMaster.CostPerParticipant));
                aParameters.Add(new SqlParameter("@TrainingDays", aMaster.TrainingDays));
                aParameters.Add(new SqlParameter("@NoOfDays", aMaster.NoOfDays));
                aParameters.Add(new SqlParameter("@StartDate", aMaster.StartDate));
                aParameters.Add(new SqlParameter("@EndDate", aMaster.EndDate));
                aParameters.Add(new SqlParameter("@StartTime", aMaster.StartTime));
                aParameters.Add(new SqlParameter("@EndTime", aMaster.EndTime));
                aParameters.Add(new SqlParameter("@TotalHoure", aMaster.TotalHoure));
                aParameters.Add(new SqlParameter("@TrainingRecordMasterId", id));
                aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));

                aParameters.Add(new SqlParameter("@UpdateBy", user));
                aParameters.Add(new SqlParameter("@UpdateDate", DateTime.Now));

                string update = @"UPDATE  dbo.tblTrainingRecordMaster set
       CompanyId =@CompanyId,
       FinancialYearId =@FinancialYearId,
       TrainingTypeId =@TrainingTypeId,
       TrainingBudget2Id =@TrainingBudget2Id,
       TrainingTitle =@TrainingTitle,
       TrainingDetails =@TrainingDetails,
       TrainingOrgId =@TrainingOrgId,
       TrainingOrgLocation =@TrainingOrgLocation,
       TrainingVenue =@TrainingVenue,
       TrainingCost =@TrainingCost,
       LogisticCost =@LogisticCost,
       OtherCost =@OtherCost,
       TrainingDays =@TrainingDays,
       NoOfDays =@NoOfDays,
       StartDate =@StartDate,
       EndDate =@EndDate,
       StartTime =@StartTime,
       EndTime =@EndTime,
       TotalHoure =@TotalHoure,       
       UpdateBy =@UpdateBy,
       UpdateDate =@UpdateDate,
       GrandTotal =@GrandTotal,
       CostPerParticipant = @CostPerParticipant ,ActionStatus=@ActionStatus WHERE TrainingRecordMasterId = @TrainingRecordMasterId";

                bool result = false;
                result = _aCommonInternalDal.UpdateDataByUpdateCommand(update, aParameters, DataBase.HRDB);
                if (result == true)
                {
                    return id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public bool DeleteTrainingRecord(int id , int user )
        {
            try
            {
                string query = @"update tblTrainingRecordMaster set IsDelete = 1 ,DeleteDate ='"+DateTime.Now.ToString("dd-MMM-yyyy")+"' , DeleteBy = "+user+"  where TrainingRecordMasterId ="+id+" ";

                return _aCommonInternalDal.UpdateDataByUpdateCommand(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public bool SaveTrainingRecordEmployee(List<TrainingRecordDetailsEmployee> aList, int pk)
        {
            try
            {
                string delq = @"Delete From dbo.tbl_trainingRecordDetailsEmployee where TrainingRecordMasterId = " + pk + "";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delq, DataBase.HRDB);
                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingRecordMasterId", item.TrainingRecordMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));

                    string query = @"INSERT INTO dbo.tbl_trainingRecordDetailsEmployee
        ( TrainingRecordMasterId ,
          EmpInfoId
        )
VALUES  ( @TrainingRecordMasterId ,
          @EmpInfoId
        )";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
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
        public bool SaveTrainingAttendance(List<TrainingAttendanceDAO> aList, int pk,int scheDayId,string date)
        {
            try
            {
                string delq = @"DELETE FROM dbo.tblTrainingAttendance WHERE TrainingRecordMasterId='" + pk + "' AND TrainingRecordScheDateId='" + scheDayId + "' AND ATTDate='" + date + "'";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delq, DataBase.HRDB);
                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingRecordMasterId", item.TrainingRecordMasterId));
                    aParameters.Add(new SqlParameter("@ATTDate", item.ATTDate));
                    aParameters.Add(new SqlParameter("@TrainingRecordScheDateId", item.TrainingRecordScheDateId));
                    aParameters.Add(new SqlParameter("@TrainingRecordDetailsEmp", item.TrainingRecordDetailsEmp));
                    aParameters.Add(new SqlParameter("@IsPresent", item.IsPresent));
                    aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                    aParameters.Add(new SqlParameter("@EntryBy", item.EntryBy));
                    aParameters.Add(new SqlParameter("@EntryDate", item.EntryDate));

                    string query = @"INSERT INTO dbo.tblTrainingAttendance
                                (
                                    TrainingRecordMasterId,
                                    ATTDate,
                                    TrainingRecordScheDateId,
                                    EmpInfoId,
                                    IsPresent,
                                    EntryBy,
                                    EntryDate,TrainingRecordDetailsEmp
                                )
                                VALUES
                                (   @TrainingRecordMasterId,
                                    @ATTDate,
                                    @TrainingRecordScheDateId,
                                    @EmpInfoId,
                                    @IsPresent,
                                    @EntryBy,
                                    @EntryDate,@TrainingRecordDetailsEmp
                                )";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
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

        public bool SaveTrainingRecordScheDate(List<TrainingRecordScheDateDAO> aList, int pk)
        {
            try
            {
                string delq = @"Delete From dbo.tblTrainingRecordScheDate where TrainingRecordMasterId = " + pk + "";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delq, DataBase.HRDB);
                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingRecordMasterId", item.TrainingRecordMasterId));
                    aParameters.Add(new SqlParameter("@Date", item.Date));
                    aParameters.Add(new SqlParameter("@Day", item.Day));
                    aParameters.Add(new SqlParameter("@StartTime", item.StartTime));
                    aParameters.Add(new SqlParameter("@EndTime", item.EndTime));

                    string query = @"INSERT INTO dbo.tblTrainingRecordScheDate
                                    (
                                        TrainingRecordMasterId,
                                        Date,
                                        Day,
                                        StartTime,
                                        EndTime
                                    )
                                    VALUES
                                    (   @TrainingRecordMasterId,
                                        @Date,
                                        @Day,
                                        @StartTime,
                                        @EndTime
                                    )";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
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

        public bool SaveTrainingRecordScheduleDay(List<TrainingRecordScheduleDayDAO> aList, int pk)
        {
            try
            {
                string delq = @"Delete From dbo.tblTrainingRecordScheduleDay where TrainingRecordMasterId = " + pk + "";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delq, DataBase.HRDB);
                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingRecordMasterId", item.TrainingRecordMasterId));
                    aParameters.Add(new SqlParameter("@DayName", item.DayName));
                    

                    string query = @"INSERT INTO dbo.tblTrainingRecordScheduleDay
                                    (
                                        TrainingRecordMasterId,
                                        DayName
                                    )
                                    VALUES
                                    (   @TrainingRecordMasterId,
                                        @DayName
                                    )";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
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
        public bool SaveTrainingDetails(List<TrainingDetailsInfo> aList, int masterId)
        {
            bool result = false;
            try
            {

                string delQuery = @"Delete from tblTrainingRecordTrainner  where TrainingRecordMasterId = " + masterId + " ";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQuery, DataBase.HRDB);
                foreach (var item in aList)
                {


                    if (item.TrainerId == 0)
                    {
                        List<SqlParameter> aParam = new List<SqlParameter>();
                        aParam.Add(new SqlParameter("@TrainerId", item.TrainerId));
                        aParam.Add(new SqlParameter("@NotListedName", item.NotListedName));
                        aParam.Add(new SqlParameter("@NotListedDetails", item.NotListedDetails));
                        aParam.Add(new SqlParameter("@TrainingRecordMasterId", masterId));
                        string query = @"Insert into tblTrainingRecordTrainner (TrainingRecordMasterId, TrainerId, NotListedName, NotListedDetails) values (@TrainingRecordMasterId, @TrainerId, @NotListedName, @NotListedDetails)";
                        result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParam, DataBase.HRDB);
                    }
                    else
                    {
                        List<SqlParameter> aParam = new List<SqlParameter>();
                        aParam.Add(new SqlParameter("@TrainerId", item.TrainerId));

                        aParam.Add(new SqlParameter("@TrainingRecordMasterId", masterId));
                        string query = @"Insert into tblTrainingRecordTrainner (TrainingRecordMasterId, TrainerId) values (@TrainingRecordMasterId, @TrainerId)";
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
        public string GenerateAutoNumber(string table, string column, DateTime date)
        {
            try
            {
                string query = @"SELECT (((SUBSTRING((CONVERT(NVARCHAR(5),YEAR(GETDATE()))),3,2)+(CASE WHEN LEN(MONTH(GETDATE()))=1 
		THEN  '0'+CONVERT(NVARCHAR(5),MONTH(GETDATE())) ELSE CONVERT(NVARCHAR(5),MONTH(GETDATE())) END)+
		(CASE WHEN LEN(DAY(GETDATE()))=1 THEN  '0'+CONVERT(NVARCHAR(5),DAY(GETDATE())) ELSE CONVERT(NVARCHAR(5),DAY(GETDATE())) END)))+
		CONVERT(NVARCHAR(5),(ISNULL((MAX(CONVERT(INT,(SUBSTRING( " + column + " ,10,4))))),1000)+1))) as AutoNumber FROM  " + table + "  WHERE  CONVERT(NVARCHAR(11),EntryDate,106)= CONVERT(NVARCHAR(11),'" + (System.DateTime.Now).ToString("dd-MMM-yyyy").Replace("-", " ") + "',106)";
                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                Decimal asd = Convert.ToDecimal(dt.Rows[0][0].ToString()); ;
                Decimal asd1 = asd + 1;

                return dt.Rows[0][0].ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTrainingRecords(string param)
        {

            string query = @"SELECT  A.TrainingRecordMasterId ,
        A.TrainingRecordNo ,
        A.TrainingTitle ,
        C.ShortName ,
        f.FinancialYearDesc ,
        t.TrainingType,
		o.TrainingOrgName,*
FROM    dbo.tblTrainingRecordMaster A
        LEFT JOIN dbo.tblCompanyInfo C ON A.CompanyId = C.CompanyId
        LEFT JOIN dbo.tblFinancialYear f ON A.FinancialYearId = f.FinancialYearId
        LEFT JOIN dbo.tblTrainingType t ON A.TrainingTypeId = t.TrainingTypeID
        LEFT JOIN dbo.tblTrainingOrgInfo o ON A.TrainingOrgId = o.TrainingOrgId
		LEFT JOIN (SELECT TrainingRecordMasterId,MAX(Version)MaxVer FROM dbo.tblTrainingRecordMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY TrainingRecordMasterId) AS LogApp ON LogApp.TrainingRecordMasterId= A.TrainingRecordMasterId
								LEFT JOIN dbo.tblTrainingRecordMasterAppLog ON tblTrainingRecordMasterAppLog.TrainingRecordMasterId = A.TrainingRecordMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblTrainingRecordMasterAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblTrainingRecordMasterAppLog PreLog ON PreLog.TrainingRecordMasterId=A.TrainingRecordMasterId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId
		WHERE ( A.IsDelete IS NULL OR A.IsDelete = 0 ) AND (tblTrainingRecordMasterAppLog.Version=LogApp.MaxVer OR tblTrainingRecordMasterAppLog.Version IS NULL) " + param;
            return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

//        }public DataTable GetTrainingRecords(string param)
//        {
           
//                string query = @"SELECT  A.TrainingRecordMasterId ,
//        A.TrainingRecordNo ,
//        A.TrainingTitle ,
//        C.ShortName ,
//        f.FinancialYearDesc ,
//        t.TrainingType,
//		o.TrainingOrgName
//FROM    dbo.tblTrainingRecordMaster A
//        LEFT JOIN dbo.tblCompanyInfo C ON A.CompanyId = C.CompanyId
//        LEFT JOIN dbo.tblFinancialYear f ON A.FinancialYearId = f.FinancialYearId
//        LEFT JOIN dbo.tblTrainingType t ON A.TrainingTypeId = t.TrainingTypeID
//        LEFT JOIN dbo.tblTrainingOrgInfo o ON A.TrainingOrgId = o.TrainingOrgId
//		WHERE ( A.IsDelete IS NULL OR A.IsDelete = 0 ) " + param;
//                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
           
//        }
        public DataTable GetTrainingRecordsApp(string actionstatus)
        {
            try
            {
                string query = @"SELECT  A.TrainingRecordMasterId ,
        A.TrainingRecordNo ,
        A.TrainingTitle ,
        C.ShortName ,
        f.FinancialYearDesc ,
        t.TrainingType,
		o.TrainingOrgName
FROM    dbo.tblTrainingRecordMaster A
        LEFT JOIN dbo.tblCompanyInfo C ON A.CompanyId = C.CompanyId
        LEFT JOIN dbo.tblFinancialYear f ON A.FinancialYearId = f.FinancialYearId
        LEFT JOIN dbo.tblTrainingType t ON A.TrainingTypeId = t.TrainingTypeID
        LEFT JOIN dbo.tblTrainingOrgInfo o ON A.TrainingOrgId = o.TrainingOrgId
		WHERE (A.IsDelete IS NULL OR A.IsDelete = 0 ) AND ActionStatus='"+actionstatus+"'";
                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetTrainingRecordsAppNew()
        {
            try
            {
                string query = @"SELECT  * FROM    dbo.tblTrainingRecordMaster A
        LEFT JOIN dbo.tblCompanyInfo C ON A.CompanyId = C.CompanyId
        LEFT JOIN dbo.tblFinancialYear f ON A.FinancialYearId = f.FinancialYearId
        LEFT JOIN dbo.tblTrainingType t ON A.TrainingTypeId = t.TrainingTypeID
        LEFT JOIN dbo.tblTrainingOrgInfo o ON A.TrainingOrgId = o.TrainingOrgId
LEFT JOIN dbo.tblUser US ON US.UserId = A.EntryBy
		INNER JOIN (SELECT TrainingRecordMasterId,MAX(Version)MaxVer FROM dbo.tblTrainingRecordMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY TrainingRecordMasterId) AS ProbLog ON ProbLog.TrainingRecordMasterId = A.TrainingRecordMasterId
								INNER JOIN dbo.tblTrainingRecordMasterAppLog ON tblTrainingRecordMasterAppLog.TrainingRecordMasterId = A.TrainingRecordMasterId
		WHERE (A.IsDelete IS NULL OR A.IsDelete = 0 ) AND  Version=ProbLog.MaxVer and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "' ";
                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetTrainingRecord(int id)
        {
            try
            {
                string query = "Select *,tblUser.EmpInfoId as UserEmpInfoId from  dbo.tblTrainingRecordMaster LEFT JOIN dbo.tblUser ON tblUser.UserId = tblTrainingRecordMaster.EntryBy where TrainingRecordMasterId  = " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetTrainingRecordScheDay(int id,string dayname)
        {
            try
            {
                string query = "SELECT * FROM dbo.tblTrainingRecordScheduleDay WHERE TrainingRecordMasterId= " + id + " AND DayName='"+dayname+"'";
                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetTrainingRecordScheDate(int id,string date)
        {
            try
            {
                string query = "SELECT * FROM dbo.tblTrainingRecordScheDate WHERE TrainingRecordMasterId= " + id + " AND Date='"+date+"'";
                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable GetAppLogCommByJobId(int jobId)
        {
            string query = @"SELECT Alg.TrainingRecordMasterAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, Alg.ActionStatus, Alg.ApproveDate, Alg.TrainingRecordMasterId, Alg.Comments FROM tblTrainingRecordMasterAppLog Alg
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and   Alg.TrainingRecordMasterId='" + jobId + "'";
            return _aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetTrainingRecordEmployee(int id)
        {
            try
            {
                string query = @"SELECT  A.EmpInfoId , e.EmpMasterCode , e.EmpName , desg.Designation , dpt.DepartmentName , grd.GradeName
FROM    dbo.tbl_trainingRecordDetailsEmployee A
        LEFT JOIN dbo.tblEmpGeneralInfo e ON A.EmpInfoId = e.EmpInfoId
		LEFT JOIN dbo.tblDesignation desg ON e.DesignationId = desg.DesignationId
		LEFT JOIN dbo.tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblSalaryGrade grd ON e.SalaryGradeId = grd.SalaryGradeId
		WHERE a.TrainingRecordMasterId =" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }



        public DataTable GetTrainingRecordFroSendMail(int id)
        {
            try
            {
                string query = @"SELECT P.TrainingRecordMasterId,E.EmpName, E.EmpMasterCode, E.OfficialEmail , Sup.EmpMasterCode ReportingMasterCode, Sup.EmpName ReportingEmp, Sup.OfficialEmail ReportingEmail
 
FROM dbo.tbl_trainingRecordDetailsEmployee P
INNER JOIN  dbo.tblEmpGeneralInfo E ON E.EmpInfoId = P.EmpInfoId 
LEFT JOIN dbo.tblEmpGeneralInfo AS Sup ON E.ReportingEmpId = Sup.EmpInfoId
   WHERE   
		  P.TrainingRecordMasterId=" + id + " ORDER BY E.EmpMasterCode ASC ";
                return _aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetTrainerTrainingRecord(int id)
        {
            try
            {
                string query = @"select A.TrainerId, Case when B.TrainerName is null then A.NotListedName else B.TrainerName end as TrainerName  ,
                            Case when c.TrainingOrgName is null then A.NotListedDetails else  c.TrainingOrgName end as TrainerDetails
                            from tblTrainingRecordTrainner A 
                            left join tblTrainerInfo B on a.TrainerId = b.TrainerId
                            left join tblTrainingOrgInfo c on b.TrainingOrgId = c.TrainingOrgId where A.TrainingRecordMasterId = " + id + "";

                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                return dt;
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
                string query = @"SELECT * FROM dbo.tblTrainingRecordMasterAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND TrainingRecordMasterId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')";

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
                    aParameters.Add(new SqlParameter("@MPBudgetMasterAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblTrainingRecordMasterAppLog set ActionStatus=@ActionStatus  where TrainingRecordMasterId = @MPBudgetMasterAppLogId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public int SavAppLog(TrainingRecordMasterAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingRecordMasterId", appLogDao.TrainingRecordMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId));


                    string query = @"INSERT INTO dbo.tblTrainingRecordMasterAppLog
                                    (
                                    TrainingRecordMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @TrainingRecordMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblTrainingRecordMasterAppLog WHERE TrainingRecordMasterId=@TrainingRecordMasterId),
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


                    string query = @" INSERT INTO dbo.tblTrainingRecordMasterAppLogComnt
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

        public bool UpdateContractural(string actionstatus, int id)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingRecordMasterId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", actionstatus));


                    string query =
                        @"update tblTrainingRecordMaster set ActionStatus=@ActionStatus  where TrainingRecordMasterId = @TrainingRecordMasterId";

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
        public bool UpdateJobReqStatus2(string actionstatus, int id)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@TrainingRecordMasterId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", actionstatus));


                    string query =
                        @"update tblTrainingRecordMaster set ActionStatus2=@ActionStatus  where TrainingRecordMasterId = @TrainingRecordMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

            }
            return true;
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
        public DataTable GetAppLogStatus(string mid,string forempId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblTrainingRecordMasterAppLog WHERE ForEmpInfoId='"+forempId+"' AND TrainingRecordMasterId='"+mid+"' AND ActionStatus<>'Review'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

   

}
