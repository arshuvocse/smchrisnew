using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.Appraisal
{
    public class JdDesigDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();

        public int SaveJdDesigMaster(JdDesigMaster aMaster, string user)
        {

            try
            {
                int pk = 0;

                if (aMaster.JdDesigMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@DesignationId", aMaster.Designationid));
                    aParameters.Add(new SqlParameter("@ReportToId", aMaster.ReportToId));
                    aParameters.Add(new SqlParameter("@DirectSuperId", aMaster.DirectSuperId));
                    aParameters.Add(new SqlParameter("@InterContId", aMaster.InterContId));
                    aParameters.Add(new SqlParameter("@ExterContId", aMaster.ExterContId));
                    aParameters.Add(new SqlParameter("@JobLocationId", aMaster.JobLocationId));
                    aParameters.Add(new SqlParameter("@DivisionId", aMaster.DivisionId));
                    aParameters.Add(new SqlParameter("@Education", aMaster.Education));
                    aParameters.Add(new SqlParameter("@RelExp", aMaster.RelExp));
                    aParameters.Add(new SqlParameter("@SpecialSkill", aMaster.SpecialSkill));
                    aParameters.Add(new SqlParameter("@OtherReq", aMaster.OtherReq));
                    aParameters.Add(new SqlParameter("@CompSkill", aMaster.CompSkill));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@JdDesigMasterId", aMaster.JdDesigMasterId));
                    aParameters.Add(new SqlParameter("@JdSummary", aMaster.JdSummary));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));

                    string query =
                        @"update tblJdDesigMaster set CompanyId=@CompanyId,
                        DesignationId=@DesignationId,
                        ReportToId=@ReportToId,
                        DirectSuperId=@DirectSuperId,
                        InterContId=@InterContId,
                        ExterContId=@ExterContId,
                        FinancialYearId=@FinancialYearId,
                        JdSummary=@JdSummary,
                        Education=@Education,
                        RelExp=@RelExp,
                        SpecialSkill=@SpecialSkill,
                        OtherReq=@OtherReq,
                        CompSkill=@CompSkill,DivisionId=@DivisionId,JobLocationId=@JobLocationId, UpdateDate= @UpdateDate  , UpdateBy =@UpdateBy   where JdDesigMasterId = @JdDesigMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    if (result == false)
                    {
                        return 0;
                    }
                    else
                    {
                        return aMaster.JdDesigMasterId;
                    }

                }
                else
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@DesignationId", aMaster.Designationid));
                    aParameters.Add(new SqlParameter("@ReportToId", aMaster.ReportToId));
                    aParameters.Add(new SqlParameter("@DirectSuperId", aMaster.DirectSuperId));
                    aParameters.Add(new SqlParameter("@InterContId", aMaster.InterContId));
                    aParameters.Add(new SqlParameter("@ExterContId", aMaster.ExterContId));
                    aParameters.Add(new SqlParameter("@JobLocationId", aMaster.JobLocationId));
                    aParameters.Add(new SqlParameter("@DivisionId", aMaster.DivisionId));
                    aParameters.Add(new SqlParameter("@Education", aMaster.Education));
                    aParameters.Add(new SqlParameter("@RelExp", aMaster.RelExp));
                    aParameters.Add(new SqlParameter("@SpecialSkill", aMaster.SpecialSkill));
                    aParameters.Add(new SqlParameter("@OtherReq", aMaster.OtherReq));
                    aParameters.Add(new SqlParameter("@CompSkill", aMaster.CompSkill));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@JdDesigMasterId", aMaster.JdDesigMasterId));
                    aParameters.Add(new SqlParameter("@JdSummary", aMaster.JdSummary));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@EntryBy", user));

                    string query = @"INSERT INTO dbo.tblJdDesigMaster
                    (
                        CompanyId,
                        DesignationId,
                        ReportToId,
                        DirectSuperId,
                        InterContId,
                        ExterContId,
                        FinancialYearId,
                        JdSummary,
                        Education,
                        RelExp,
                        SpecialSkill,
                        OtherReq,
                        CompSkill,JobLocationId,DivisionId,
                        EntryDate,
                        EntryBy
                        
                    )
                    VALUES
                    (   @CompanyId,
                        @DesignationId,
                        @ReportToId,
                        @DirectSuperId,
                        @InterContId,
                        @ExterContId,
                        @FinancialYearId,
                        @JdSummary,
                        @Education,
                        @RelExp,
                        @SpecialSkill,
                        @OtherReq,
                        @CompSkill,@JobLocationId,@DivisionId,
                        @EntryDate,
                        @EntryBy
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

        public bool SaveJdDesigDetails(List<JdDesigDetails> list, int pk)
        {
            try
            {

                bool result = false;
                string query = @"Delete From tblJdDesigDetails where JdDesigMasterId = " + pk + "";
                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, DataBase.HRDB);
                foreach (var item in list)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@JdDesigMasterId", item.JdDesigMasterId));
                    aParameters.Add(new SqlParameter("@JdDesigDetailsInfo", item.JdDetailsInfo));

                    string query2 =
                        @"Insert into  tblJdDesigDetails (JdDesigMasterId ,JdDetailsInfo ) values (@JdDesigMasterId ,@JdDesigDetailsInfo)";

                    result = _aCommonInternalDal.SaveDataByInsertCommand(query2, aParameters, DataBase.HRDB);
                }
                return result;
            }
            catch (Exception exception)
            {

                throw exception;
            }

        }

        public DataTable GetJdList()
        {
            try
            {
                string query = @"select A.JdDesigMasterId as gv_JdBoard,* from tblJdDesigMaster A 
                                left join tblFinancialYear c on a.FinancialYearId = c.FinancialYearId 
                                left join tblDesignation desg on A.DesignationId = desg.DesignationId
                                LEFT JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = A.CompanyId
                                where a.IsDelete is null or a.IsDelete = 0";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DesignationDropDown(DropDownList aDropDownList)
        {
            string query = "SELECT * FROM dbo.tblDesignation";
            _aCommonInternalDal.LoadDropDownValue(aDropDownList, "Designation", "DesignationId", query, "HRDB");
        }

        public void DivDropDown(DropDownList aDropDownList,string compId)
        {
            string query = "SELECT * FROM dbo.tblDivision WHERE CompanyId='"+compId+"'";
            _aCommonInternalDal.LoadDropDownValue(aDropDownList, "DivisionName", "DivisionId", query, "HRDB");
        }
        public void JobLocationDropDown(DropDownList aDropDownList)
        {
            string query = "SELECT * FROM dbo.tblJobLocation";
            _aCommonInternalDal.LoadDropDownValue(aDropDownList, "Location", "JobLocationID", query, "HRDB");
        }
        public DataTable GetJdDesigByMaster(int id)
        {
            try
            {
                string query = @"select * from tblJdDesigMaster 

                where JdDesigMasterId =" + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable GetId()
        {
            try
            {
                string query = @"SELECT (COUNT(*)+1)A FROM dbo.tblJobReqForm ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable GetSalGrade(int id)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblDesignation
LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblDesignation.SalaryGradeId
WHERE DesignationId='"+id+"'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DataTable GetJdByMaster(int id)
        {
            try
            {
                string query = @"select a.JdDesigMasterId, a.JdSummary , a.Designationid , a.FinancialYearId ,b.CompanyId ,(b.EmpMasterCode+':'+b.EmpName) as employee from tblJdDesigMaster A left join tblEmpGeneralInfo b on a.Designationid = b.Designationid

                where a.JdDesigMasterId =" + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetJdDesigDetails(int id)
        {
            try
            {
                string query = @"Select * from tblJdDesigDetails where JdDesigMasterId=" + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public bool DeleteJd(int masterid, string user)
        {
            try
            {
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@JdDesigMasterId", masterid));

                aParameters.Add(new SqlParameter("@DeleteBy", user));
                aParameters.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
                aParameters.Add(new SqlParameter("@IsDelete", 1));
                string query = @"update tblJdDesigMaster set IsDelete = @IsDelete , DeleteBy=@DeleteBy , DeleteDate = @DeleteDate  where JdDesigMasterId = @JdDesigMasterId";

                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                return result;
            }
            catch (Exception exception)
            {

                throw;
            }
        }


        #region Kpi DeadLine Setup

        public DataTable GetEmployeeForKpiSetUp(int companyId, string deadLine, string remarks)
        {
            try
            {
                string query = @"select A.Designationid, A.EmpMasterCode , A.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName , '" + deadLine + "' as DeadLine , '" + remarks + "' as Remarks  from tblEmpGeneralInfo A " +
                               "left join tblDivision div on a.DivisionId = div.DivisionId " +
                               "left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId " +
                               "left join tblDesignation desg on a.DesignationId = desg.DesignationId where A.CompanyId = " + companyId + " and a.IsActive=1";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public int SaveKpiSetupMaster(KpiDeadlineMaster aMaster, string user)
        {
            try
            {
                int pk = 0;

                if (aMaster.KPIDeadLineMasterId == 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                    string query =
                        @"insert into tblKpiDeadlineMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy)";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                    return pk;
                }
                else
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@KPIDeadLineMasterId", aMaster.KPIDeadLineMasterId));
                    aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));

                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                    string query = @"update tblKpiDeadlineMaster set CompanyId = @CompanyId , FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate where KPIDeadLineMasterId = @KPIDeadLineMasterId ";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        pk = aMaster.KPIDeadLineMasterId;
                    }

                    return pk;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

//        public bool SaveKpiSetupDetails(List<KPIDeadLineDetails> aDetails, int master)
//        {
//            try
//            {

//                bool result = false;
//                string delQ = @"delete from tblKPIDeadLineDetails where KPIDeadLineMasterId = " + master + "";

//                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

//                foreach (KPIDeadLineDetails item in aDetails)
//                {
//                    List<SqlParameter> aParameters = new List<SqlParameter>();
//                    aParameters.Add(new SqlParameter("@Designationid", item.Designationid));
//                    aParameters.Add(new SqlParameter("@DeadLine", item.DeadLine));
//                    aParameters.Add(new SqlParameter("@Remarks", item.Remarks));
//                    aParameters.Add(new SqlParameter("@KPIDeadLineMasterId", master));

//                    string query = @"insert into tblKPIDeadLineDetails (KPIDeadLineMasterId, Designationid, DeadLine, Remarks) 
//                                                                values(@KPIDeadLineMasterId, @Designationid, @DeadLine, @Remarks)";

//                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

//                    if (result == false)
//                    {
//                        break;


//                    }


//                }
//                return result;
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }



        public DataTable GetKpiSetupList()
        {

            try
            {
                string query = @"select a.KPIDeadLineMasterId , c.CompanyName ,d.FinancialYearDesc , b.TotalEmployee  , CONVERT(nvarchar (11),a.EntryDate , 106)EntryDate ,a.EntryBy   from tblKpiDeadlineMaster A 
                                    left join tblCompanyInfo c on a.CompanyId = c.CompanyId
                                    left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
                                    
                                    left join (select count(Designationid)TotalEmployee , KPIDeadLineMasterId from tblKPIDeadLineDetails group by KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
                                    
                                    where a.IsDelete is null or a.IsDelete = 0";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public DataTable GetKpiSetupByMaster(int id)
        {
            try
            {
                string query = @"select * from tblKpiDeadlineMaster where KPIDeadLineMasterId = " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetKpiSetupDetailsByMaster(int id)
        {
            try
            {
                string query = @"SELECT   KPIDeadLineDetailsId, KPIDeadLineMasterId, Designationid, DeadLine, Remarks FROM   tblKPIDeadLineDetails where KPIDeadLineMasterId = " +
                    id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public bool DeleteKpiSetup(int master, string user)
        {
            try
            {
                bool result = false;
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@KPIDeadLineMasterId", master));
                aParameters.Add(new SqlParameter("@DeleteBy", user));
                aParameters.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));

                string query = @"update tblKpiDeadlineMaster set IsDelete = 1  , DeleteBy = @DeleteBy ,DeleteDate = @DeleteDate where KPIDeadLineMasterId = @KPIDeadLineMasterId ";

                result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CheckPreviousKpiDeadline(int com, int fin, int masterId)
        {
            try
            {
                string query = @"select * from tblKpiDeadlineMaster where CompanyId =" + com + " and FinancialYearId = " + fin + " and KPIDeadLineMasterId!= " + masterId + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion Kpi DeadLine Setup



        #region Appraisal Deadline


        public DataTable GetEmpForAppraisalDeadLine(int com, string deadLine, string remarks)
        {
            try
            {
                string query =
                    @" select A.ReportingEmpId as Designationid , Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName ,count(a.Designationid)TotalEmployee  , '" + deadLine + "' as DeadLine , '" + remarks + "' as Remarks from tblEmpGeneralInfo A " +
                    "left join tblEmpGeneralInfo Aa on a.ReportingEmpId = aa.Designationid " +
                    "left join tblDivision div on aa.DivisionId = div.DivisionId  " +
                    "left join tblDepartment dpt on aa.DepartmentId = dpt.DepartmentId " +
                    "left join tblDesignation desg on aa.DesignationId = desg.DesignationId where Aa.CompanyId = " + com + " and aa.IsActive=1 and  a.IsActive=1 " +
                    "group by A.ReportingEmpId, Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }



        public int SaveAppraisalSetupMaster(AppraisalDeadlineMaster aMaster, string user)
        {
            try
            {
                int pk = 0;

                if (aMaster.AppraisalDeadLineMasterId == 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                    string query =
                        @"insert into tblAppraisalDeadlineMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy,FYDes_AppDec) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy, (SELECT FinancialYearDesc 
                     FROM dbo.tblFinancialYear 
                     WHERE FinancialYearId =@FinancialYearId))";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                    return pk;
                }
                else
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@AppraisalDeadLineMasterId", aMaster.AppraisalDeadLineMasterId));
                    aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));

                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                    string query = @"update tblAppraisalDeadlineMaster set CompanyId = @CompanyId , FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate where AppraisalDeadLineMasterId = @AppraisalDeadLineMasterId ";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        pk = aMaster.AppraisalDeadLineMasterId;
                    }

                    return pk;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

//        public bool SaveAppraisalSetupDetails(List<AppraisalDeadLineDetails> aDetails, int master)
//        {
//            try
//            {

//                bool result = false;
//                string delQ = @"delete from tblAppraisalDeadLineDetails where AppraisalDeadLineMasterId = " + master + "";

//                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

//                foreach (AppraisalDeadLineDetails item in aDetails)
//                {
//                    List<SqlParameter> aParameters = new List<SqlParameter>();
//                    aParameters.Add(new SqlParameter("@Designationid", item.Designationid));
//                    aParameters.Add(new SqlParameter("@DeadLine", item.DeadLine));
//                    aParameters.Add(new SqlParameter("@Remarks", item.Remarks));
//                    aParameters.Add(new SqlParameter("@AppraisalDeadLineMasterId", master));

//                    string query = @"insert into tblAppraisalDeadLineDetails (AppraisalDeadLineMasterId, Designationid, DeadLine, Remarks) 
//                                                                values(@AppraisalDeadLineMasterId, @Designationid, @DeadLine, @Remarks)";

//                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

//                    if (result == false)
//                    {
//                        break;


//                    }


//                }
//                return result;
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }
//        }


        public DataTable GetAppraisalSetupList()
        {

            try
            {
                string query = @"select a.AppraisalDeadLineMasterId , c.CompanyName ,d.FinancialYearDesc , b.TotalEmployee  , CONVERT(nvarchar (11),a.EntryDate , 106)EntryDate ,a.EntryBy   from tblAppraisalDeadlineMaster A 
                                    left join tblCompanyInfo c on a.CompanyId = c.CompanyId
                                    left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
                                    
                                    left join (select count(Designationid)TotalEmployee , AppraisalDeadLineMasterId from tblAppraisalDeadLineDetails group by AppraisalDeadLineMasterId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
                                    
                                    where a.IsDelete is null or a.IsDelete = 0";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }



        public DataTable GetAppraisalSetupByMaster(int id)
        {
            try
            {
                string query = @"select * from tblAppraisalDeadlineMaster where AppraisalDeadLineMasterId = " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetAppraisalSetupDetailsByMaster(int id)
        {
            try
            {
                string query = @"SELECT  *  FROM   tblAppraisalDeadLineDetails where AppraisalDeadLineMasterId = " +
                    id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public bool DeleteAppraisalSetup(int master, string user)
        {
            try
            {
                bool result = false;
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@AppraisalDeadLineMasterId", master));
                aParameters.Add(new SqlParameter("@DeleteBy", user));
                aParameters.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));

                string query = @"update tblAppraisalDeadlineMaster set IsDelete = 1  , DeleteBy = @DeleteBy ,DeleteDate = @DeleteDate where AppraisalDeadLineMasterId = @AppraisalDeadLineMasterId ";

                result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable CheckPreviousAppraisalDeadline(int com, int fin, int masterId)
        {
            try
            {
                string query = @"select * from tblAppraisalDeadlineMaster where CompanyId =" + com + " and FinancialYearId = " + fin + " and AppraisalDeadLineMasterId!= " + masterId + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





        #endregion
    }
}
