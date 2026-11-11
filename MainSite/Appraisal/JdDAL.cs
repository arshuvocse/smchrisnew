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
    public class JdDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();

        public int SaveJdMaster(JdMaster aMaster, string user)
        {

            try
            {
                int pk = 0;

                if (aMaster.JdMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@JdMasterId", aMaster.JdMasterId));
                    aParameters.Add(new SqlParameter("@JdSummary", aMaster.JdSummary));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));

                    string query =
                        @"update tblJdMaster set JdSummary=@JdSummary ,  EmpInfoId  = @EmpInfoId , FinancialYearId =@FinancialYearId , UpdateDate= @UpdateDate  , UpdateBy =@UpdateBy   where JdMasterId = @JdMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    if (result == false)
                    {
                        return 0;
                    }
                    else
                    {
                        return aMaster.JdMasterId;
                    }

                }
                else
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@JdSummary", aMaster.JdSummary));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@EntryBy", user));

                    string query = @"Insert into tblJdMaster (EmpInfoId, FinancialYearId, JdSummary, EntryDate, EntryBy) 
                        values(@EmpInfoId, @FinancialYearId, @JdSummary, @EntryDate, @EntryBy)";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }


                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public void LoadEmpCategory(DropDownList ddl)
        {
            string query = @"SELECT * FROM dbo.tblEmpCategory ";
            _aCommonInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", query, DataBase.HRDB);
        }
        public void LoadDept(DropDownList ddl,string compId)
        {
            string query = @"SELECT * FROM dbo.tblDepartment WHERE Invisible IS NULL and CompanyId='" + compId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, DataBase.HRDB);
        }
        public bool SaveJdDetails(List<JdDetails> list, int pk)
        {
            try
            {

                bool result = false;
                string query = @"Delete From tblJdDetails where JdMasterId = " + pk + "";
                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, DataBase.HRDB);
                foreach (var item in list)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@JdMasterId", item.JdMasterId));
                    aParameters.Add(new SqlParameter("@JdDetailsInfo", item.JdDetailsInfo));

                    string query2 =
                        @"Insert into  tblJdDetails (JdMasterId ,JdDetailsInfo ) values (@JdMasterId ,@JdDetailsInfo)";

                    result = _aCommonInternalDal.SaveDataByInsertCommand(query2, aParameters, DataBase.HRDB);
                }
                return result;
            }
            catch (Exception exception)
            {

                throw exception;
            }

        }

        public DataTable GetJdList(string param)
        {
            try
            {
                string query = @"select a.JdMasterId, a.JdSummary , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) as employee , dpt.DepartmentName ,c.FinancialYearDesc from tblJdMaster A left join tblEmpGeneralInfo b on a.EmpInfoId = b.EmpInfoId
                                left join tblFinancialYear c on a.FinancialYearId = c.FinancialYearId 
                                left join tblDepartment dpt on b.DepartmentId = dpt.DepartmentId
                                left join tblDesignation desg on b.DesignationId = desg.DesignationId
                                where a.IsDelete is null or a.IsDelete = 0" ;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetJdByMaster(int id)
        {
            try
            {
                string query = @"select a.JdMasterId, a.JdSummary , a.EmpInfoId , a.FinancialYearId ,b.CompanyId ,(b.EmpMasterCode+':'+b.EmpName) as employee from tblJdMaster A left join tblEmpGeneralInfo b on a.EmpInfoId = b.EmpInfoId

                where a.JdMasterId =" + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetJdDetails(int id)
        {
            try
            {
                string query = @"Select * from tblJdDetails where JdMasterId=" + id + "";

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
                aParameters.Add(new SqlParameter("@JdMasterId", masterid));

                aParameters.Add(new SqlParameter("@DeleteBy", user));
                aParameters.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
                aParameters.Add(new SqlParameter("@IsDelete", 1));
                string query = @"update tblJdMaster set IsDelete = @IsDelete , DeleteBy=@DeleteBy , DeleteDate = @DeleteDate  where JdMasterId = @JdMasterId";

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
                string query = @"select A.EmpInfoId, A.EmpMasterCode , A.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName , '" + deadLine + "' as DeadLine , '" + remarks + "' as Remarks  from tblEmpGeneralInfo A " +
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
        public DataTable GetEmployeeForKpiSetUpNew(string companyId, string deadLine, string remarks,string param)
        {
            try
            {
                string query = @"select A.EmpInfoId, A.EmpMasterCode , A.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName , '" + deadLine + "' as DeadLine , '" + remarks + "' as Remarks  from tblEmpGeneralInfo A " +
                               "left join tblDivision div on a.DivisionId = div.DivisionId " +
                               "left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId " +
                               "left join tblDesignation desg on a.DesignationId = desg.DesignationId where A.CompanyId = " + companyId + " and a.IsActive=1 "+param+"";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        //Shuvo
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
                    aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                    aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                    aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                    string query =
                        @"insert into tblKpiDeadlineMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy,Subject, DeclarationDate) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy,@Subject, @DeclarationDate)";

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
                    aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));

                    string query = @"update tblKpiDeadlineMaster set CompanyId = @CompanyId ,Subject=@Subject, FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate, DeclarationDate=@DeclarationDate where KPIDeadLineMasterId = @KPIDeadLineMasterId ";

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

        public bool SaveKpiSetupDetails(List<KPIDeadLineDetails> aDetails, int master)
        {
            try
            {

                bool result = false;
                string delQ = @"delete from tblKPIDeadLineDetails where KPIDeadLineMasterId = " + master + "";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

                foreach (KPIDeadLineDetails item in aDetails)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpinfoId", item.EmpinfoId));
                    aParameters.Add(new SqlParameter("@DeadLine", item.DeadLine));
                    aParameters.Add(new SqlParameter("@Remarks", item.Remarks));
                    aParameters.Add(new SqlParameter("@KPIDeadLineMasterId", master));

                    string query = @"insert into tblKPIDeadLineDetails (KPIDeadLineMasterId, EmpinfoId, DeadLine, Remarks) 
                                                                values(@KPIDeadLineMasterId, @EmpinfoId, @DeadLine, @Remarks)";

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


        //shuvo
        public DataTable GetKpiSetupList()
            //string param
        {

            try
            {
                string query = @"select a.KPIDeadLineMasterId , c.CompanyName ,d.FinancialYearDesc , b.TotalEmployee  , CONVERT(nvarchar (11),a.EntryDate , 106)EntryDate ,a.EntryBy,*   from tblKpiDeadlineMaster A 
                                    left join tblCompanyInfo c on a.CompanyId = c.CompanyId
                                    left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
                                    
                                    left join (select count(EmpinfoId)TotalEmployee , KPIDeadLineMasterId from tblKPIDeadLineDetails group by KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
                                    
                                    where (a.IsDelete is null or a.IsDelete = 0)" ;
                //+ param
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
                string query = @"SELECT    dtls.EmpinfoId,KPIDeadLineDetailsId, KPIDeadLineMasterId, empInfo.EmpMasterCode, empInfo.EmpName, DeadLine, dtls.Remarks, 0 AS DivisionName,
 desg.Designation, Dpt.DepartmentName  FROM   tblKPIDeadLineDetails dtls
INNER JOIN dbo.tblEmpGeneralInfo empInfo ON  dtls.EmpinfoId=empInfo.EmpInfoId
left JOIN dbo.tblDesignation desg ON  empInfo.DesignationId=desg.DesignationId
left JOIN dbo.tblDepartment Dpt ON  empInfo.DepartmentId=Dpt.DepartmentId
 
 where KPIDeadLineMasterId = " +
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
                    @" select A.ReportingEmpId as EmpInfoId , Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName ,count(a.EmpInfoId)TotalEmployee  , '" + deadLine + "' as DeadLine , '" + remarks + "' as Remarks from tblEmpGeneralInfo A " +
                    "left join tblEmpGeneralInfo Aa on a.ReportingEmpId = aa.EmpInfoId " +
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
        public DataTable GetEmpForAppraisalDeadLineNew(int com, string deadLine, string remarks,string param)
        {
            try
            {
                string query =
                    @" select A.ReportingEmpId as EmpInfoId , Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName ,count(a.EmpInfoId)TotalEmployee  , '" + deadLine + "' as DeadLine , '" + remarks + "' as Remarks from tblEmpGeneralInfo A " +
                    "left join tblEmpGeneralInfo Aa on a.ReportingEmpId = aa.EmpInfoId " +
                    "left join tblDivision div on aa.DivisionId = div.DivisionId  " +
                    "left join tblDepartment dpt on aa.DepartmentId = dpt.DepartmentId " +
                    "left join tblDesignation desg on aa.DesignationId = desg.DesignationId where Aa.CompanyId = " + com + " and aa.IsActive=1 and  a.IsActive=1 "+param+" " +
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
                    aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                    string query =
                        @"insert into tblAppraisalDeadlineMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy,Subject,FYDes_AppDec) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy,@Subject, (SELECT FinancialYearDesc 
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
                    aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                    string query = @"update tblAppraisalDeadlineMaster set CompanyId = @CompanyId,Subject=@Subject , FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate where AppraisalDeadLineMasterId = @AppraisalDeadLineMasterId ";

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

        public bool SaveAppraisalSetupDetails(List<AppraisalDeadLineDetails> aDetails, int master)
        {
            try
            {

                bool result = false;
                string delQ = @"delete from tblAppraisalDeadLineDetails where AppraisalDeadLineMasterId = " + master + "";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

                foreach (AppraisalDeadLineDetails item in aDetails)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpinfoId", item.EmpinfoId));
                    aParameters.Add(new SqlParameter("@DeadLine", item.DeadLine));
                    aParameters.Add(new SqlParameter("@Remarks", item.Remarks));
                    aParameters.Add(new SqlParameter("@AppraisalDeadLineMasterId", master));

                    string query = @"insert into tblAppraisalDeadLineDetails (AppraisalDeadLineMasterId, EmpinfoId, DeadLine, Remarks) 
                                                                values(@AppraisalDeadLineMasterId, @EmpinfoId, @DeadLine, @Remarks)";

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
