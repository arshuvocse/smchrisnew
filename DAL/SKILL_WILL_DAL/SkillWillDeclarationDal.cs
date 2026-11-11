using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.SkillWill_Dao;

namespace DAL.SKILL_WILL_DAL
{
   public class SkillWillDeclarationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable DeleteValidattionForEffectiveDate(string CompanyId, string FinancialYearId)
        {
            string query = @"select * from  tblSkillWillAssesDecMaster where CompanyId =" + CompanyId + " and FinancialYearId =  " + FinancialYearId;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public DataTable CheckKepiSetpExist(string empInfoId, string FinancialYearId)
        {
            string query = @"SELECT * FROM  tblSkillWillAssesDecDetails D
             LEFT JOIN  tblSkillWillAssesDecMaster M ON D.SkillWillAssesDecMasId = M.SkillWillAssesDecMasId where EmpInfoId =" + empInfoId + " and FinancialYearId =  "+FinancialYearId;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public int SaveSkillWillAssessmentDecMaster(SkillWillAssessmentDecMasterDao aMaster, string user)
        {
            try
            {
                int pk = 0;

                if (aMaster.SkillWillAssesDecMasId == 0)
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
                        @"insert into tblSkillWillAssesDecMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy,Subject, DeclarationDate) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy,@Subject, @DeclarationDate)";

                    pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                    return pk;
                }
                else
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@SkillWillAssesDecMasId", aMaster.SkillWillAssesDecMasId));
                    aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                    aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));

                    string query = @"update tblSkillWillAssesDecMaster set CompanyId = @CompanyId ,Subject=@Subject, FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate, DeclarationDate=@DeclarationDate where SkillWillAssesDecMasId = @SkillWillAssesDecMasId ";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        pk = aMaster.SkillWillAssesDecMasId;
                    }

                    return pk;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool SaveKpiSetupDetails(List<SkillWillAssesmentDecDetailsDao> aDetails, int master)
        {
            try
            {

                bool result = false;
                string delQ = @"delete from tblSkillWillAssesDecDetails where SkillWillAssesDecMasId = " + master + "";

                bool del = aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

                foreach (SkillWillAssesmentDecDetailsDao item in aDetails)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpinfoId", item.EmpinfoId));
                    aParameters.Add(new SqlParameter("@DeadLine", item.DeadLine));
                    aParameters.Add(new SqlParameter("@Remarks", item.Remarks));
                    aParameters.Add(new SqlParameter("@SkillWillAssesDecMasId", master));

                    string query = @"insert into tblSkillWillAssesDecDetails (SkillWillAssesDecMasId, EmpinfoId, DeadLine, Remarks) 
                                                                values(@SkillWillAssesDecMasId, @EmpinfoId, @DeadLine, @Remarks)";

                    result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

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


        public DataTable GetSkillWillDeclarationList(string param)
            //string param
        {

            try
            {
                string query = @" SElECT a.SkillWillAssesDecMasId , c.CompanyName ,d.FinancialYearDesc , b.TotalEmployee  , CONVERT(nvarchar (11),a.EntryDate , 106)EntryDate ,a.EntryBy,
 CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate, *   FROM tblSkillWillAssesDecMaster A 
 left join tblCompanyInfo c on a.CompanyId = c.CompanyId
 left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId                                   
 left join (select count(EmpinfoId)TotalEmployee , SkillWillAssesDecMasId FROM tblSkillWillAssesDecDetails group by SkillWillAssesDecMasId) B on a.SkillWillAssesDecMasId = b.SkillWillAssesDecMasId                                     
 where (a.IsDelete is null or a.IsDelete = 0)" + param;
                //+ param
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }




        public DataTable GetKpiSetupDetailsByMaster(int id)
        {
            try
            {
                string query = @"SELECT mas.FinancialYearId,   dtls.EmpinfoId,SkillWillAssesDecDetailsId, dtls.SkillWillAssesDecMasId, empInfo.EmpMasterCode, empInfo.EmpName,  FORMAT(DeadLine,'dd-MMM-yyyy') DeadLine, dtls.Remarks, 0 AS DivisionName,
 desg.Designation, Dpt.DepartmentName,*  FROM   tblSkillWillAssesDecDetails dtls
INNER JOIN dbo.tblEmpGeneralInfo empInfo ON  dtls.EmpinfoId=empInfo.EmpInfoId
left JOIN dbo.tblDesignation desg ON  empInfo.DesignationId=desg.DesignationId
left JOIN dbo.tblDepartment Dpt ON  empInfo.DepartmentId=Dpt.DepartmentId
left JOIN dbo.tblSkillWillAssesDecMaster mas ON  mas.SkillWillAssesDecMasId=dtls.SkillWillAssesDecMasId
 where dtls.SkillWillAssesDecMasId = " +
                               id + " ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
                string query = @"Select ISNULL(tblSkillWillAssesDecMaster.UpdateDate,tblSkillWillAssesDecMaster.EntryDate) UpdateDate,  * from tblSkillWillAssesDecMaster where SkillWillAssesDecMasId = " + id + "";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetJdDetailsForView(int id)
        {
            try
            {
                string query = @" SELECT  FORMAT(dtls.DeadLine,'dd-MMM-yyyy') DeadLine,  dtls.EmpinfoId,SkillWillAssesDecDetailsId, SkillWillAssesDecMasId, empInfo.EmpMasterCode, empInfo.EmpName, DeadLine, dtls.Remarks, 0 AS DivisionName,
 desg.Designation, Dpt.DepartmentName  FROM   tblSkillWillAssesDecDetails dtls
INNER JOIN dbo.tblEmpGeneralInfo empInfo ON  dtls.EmpinfoId=empInfo.EmpInfoId
left JOIN dbo.tblDesignation desg ON  empInfo.DesignationId=desg.DesignationId
left JOIN dbo.tblDepartment Dpt ON  empInfo.DepartmentId=Dpt.DepartmentId
where SkillWillAssesDecMasId = " +
                               id + " ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }



        public bool DeleteSkillWillDeclaration(int master, string user)
        {
            try
            {
                bool result = false;
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@KPIDeadLineMasterId", master));
                string query = @"
            DELETE FROM [dbo].[tblSkillWillAssesDecDetails]
            WHERE  SkillWillAssesDecMasId=@KPIDeadLineMasterId
            DELETE FROM [dbo].[tblSkillWillAssesDecMaster]
            WHERE  SkillWillAssesDecMasId=@KPIDeadLineMasterId
            ";
            result = aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, DataBase.HRDB);
            return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetEmployeeForKpiSetUpNew(string companyId, string deadLine, string remarks, string param)
        {
            try
            {
                string query = @"select distinct  A.EmpInfoId, A.EmpMasterCode , A.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName , '" + deadLine + "' as DeadLine , '" + remarks + "' as Remarks  from tblEmpGeneralInfo A " +
                               "Inner join tblEmpGeneralInfo se On A.EmpInfoId = se.ReportingEmpId  " +
                               "left join tblDivision div on a.DivisionId = div.DivisionId " +
                               "left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId " +
                               "left join tblDesignation desg on a.DesignationId = desg.DesignationId where A.CompanyId = " + companyId + " and a.IsActive=1 " + param + "";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable GetDeclarationReportingEmp(string EmpID, string FinancialYear)
        {
            try
            {
                string query = @"SELECT  ISNULL(SWAM.EmpSkillWillMasterId,0) EmpSkillWillMasterId, case when  SWAM.ActionStatus='Review' then 'Returned' +ISNULL( ' ['+tblapp.Comments+']','') when  SWAM.ActionStatus='Verified' then 'Submitted' else SWAM.ActionStatus end ActionStatus , ISNULL(SWAM.EmpSkillWillMasterId,0)EmpSkillWillMasterId,Se.EmpInfoId,A.FinancialYearId,( 'Employee ID: '+ Se.EmpMasterCode + ', Employee Name: ' + Se.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,CASE WHEN   Se.EmpInfoId IN (Select EmpInfoId from tblEmpSkillWillAssessmentMaster)  Then 'YES' ELSE 'NO' END Status,
        dpt.DepartmentName
        FROM    dbo.tblSkillWillAssesDecMaster A
        LEFT JOIN dbo.tblSkillWillAssesDecDetails b ON A.SkillWillAssesDecMasId = b.SkillWillAssesDecMasId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
		Inner JOIn  tblEmpGeneralInfo Se ON Se.ReportingEmpId = e.EmpInfoId
		 LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN tblFinancialYear y ON y.FinancialYearId = A.FinancialYearId
        LEFT JOIN tblEmpSkillWillAssessmentMaster SWAM ON Se.EmpInfoId=SWAM.EmpInfoId

		left join (select   top 1 a.EmpSkillWillMasterId, a.Comments from tblEmpSkillWillAssessmentMasterAppLog a where a.ActionStatus='Review' group by a.EmpSkillWillMasterId, a.Comments) tblapp on  SWAM.EmpSkillWillMasterId=tblapp.EmpSkillWillMasterId
	 
        WHERE  b.EmpinfoId =  " + Convert.ToInt32(EmpID) + " and A.FinancialYearId=" + FinancialYear ;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetDeclarationReportingEmpForDashboard()
        {
            try
            {
                string query = @"SELECT  ISNULL(SWAM.EmpSkillWillMasterId,0) EmpSkillWillMasterId, case when  SWAM.ActionStatus='Review' then 'Returned' +ISNULL( ' ['+tblapp.Comments+']','') when  SWAM.ActionStatus='Verified' then 'Submitted' else SWAM.ActionStatus end ActionStatus , ISNULL(SWAM.EmpSkillWillMasterId,0)EmpSkillWillMasterId,Se.EmpInfoId,A.FinancialYearId,( 'Employee ID: '+ Se.EmpMasterCode + ', Employee Name: ' + Se.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,CASE WHEN   Se.EmpInfoId IN (Select EmpInfoId from tblEmpSkillWillAssessmentMaster)  Then 'YES' ELSE 'NO' END Status,
        dpt.DepartmentName
        FROM    dbo.tblSkillWillAssesDecMaster A
        LEFT JOIN dbo.tblSkillWillAssesDecDetails b ON A.SkillWillAssesDecMasId = b.SkillWillAssesDecMasId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
		Inner JOIn  tblEmpGeneralInfo Se ON Se.ReportingEmpId = e.EmpInfoId
		 LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN tblFinancialYear y ON y.FinancialYearId = A.FinancialYearId
        LEFT JOIN tblEmpSkillWillAssessmentMaster SWAM ON Se.EmpInfoId=SWAM.EmpInfoId

		left join (select   top 1 a.EmpSkillWillMasterId, a.Comments from tblEmpSkillWillAssessmentMasterAppLog a where a.ActionStatus='Review' group by a.EmpSkillWillMasterId, a.Comments) tblapp on  SWAM.EmpSkillWillMasterId=tblapp.EmpSkillWillMasterId
	 
        WHERE  ((SWAM.ActionStatus is  null) or  (SWAM.ActionStatus not in ('Verified','Approved'))) and   b.EmpinfoId =  '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
