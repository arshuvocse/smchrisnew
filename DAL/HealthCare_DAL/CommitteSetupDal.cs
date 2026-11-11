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
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using DAO.SkillWill_Dao;

namespace DAL.HealthCare_DAL
{
    public class CommitteSetupDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

        public DataTable Get_CommitteeSetup()
        {
            try
            {
                string query = @"Select  M.ComSetupMasId, Com.ShortName,M.ApplicationType AS IsOPD, SL.SalaryLocation+ ISNULL( ' : '+ComSal.ShortName,'') SalaryLocation, En.UserName AS EntryBy, M.EntryDate, Un.UserName as UpdateBy, M.UpdateDate,  * from tblCommiteeSetupMaster M 
LEFT JOIN tblCompanyInfo Com On M.CompanyId = Com.CompanyId
LEFT JOIN tblSalaryLocation SL ON M.SalaryLoationId = SL.SalaryLoationId
LEFT JOIN tblCompanyInfo ComSal On SL.ComID = ComSal.CompanyId

LEFT JOIN tblUser En ON M.EntryBy = En.UserId
LEFT JOIn tblUser Un ON M.UpdateBy = Un.UserId   union all
Select  M.ComSetupMasId, Com.ShortName,M.ApplicationType AS IsOPD, SL.SalaryLocation+ ISNULL( ' : '+ComSal.ShortName,'') SalaryLocation, En.UserName AS EntryBy, M.EntryDate, Un.UserName as UpdateBy, M.UpdateDate,  * from tblCommiteeSetupMaster M 
inner JOIN tblCompanyInfoOther Com On M.CompanyId = Com.CompanyId
LEFT JOIN tblSalaryLocation SL ON M.SalaryLoationId = SL.SalaryLoationId
LEFT JOIN tblCompanyInfo ComSal On SL.ComID = ComSal.CompanyId

LEFT JOIN tblUser En ON M.EntryBy = En.UserId
LEFT JOIn tblUser Un ON M.UpdateBy = Un.UserId";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable Get_CommitteeSetupById(int ID)
        {
            try
            {
                string query = @"SELECT Emp.EmpMasterCode+ ':'+Emp.EmpName EmpName , 0 AS ddlEmpId ,* FROM tblCommiteeSetupMaster M 
LEFT JOIN tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId
LEFT JOIN tblEmpGeneralInfo EMP ON D.EmpInfoId = Emp.EmpInfoId
WHERE M.ComSetupMasId=" + ID;
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }






        public DataTable Get_CheckApplicationExi(string CompnayId, string Type, string LocationId)
        {
            try
            {
                string query = @"WITH CTE AS (SELECT*,ROW_NUMBER() OVER (PARTITION BY ReimbursFromMasterId ORDER BY ReimbursementSelfApplogId DESC) AS RowNum FROM tblReimbursementSelfAppLog 
WHERE HRPanel = 1 and   ForEmpInfoId IN (SELECT D.EmpInfoId FROM tblCommiteeSetupMaster M 
LEFT JOIN tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId
WHERE M.CompanyId="+CompnayId+" AND M.ApplicationType='"+Type+"' ANd M.SalaryLoationId="+LocationId+" AND IsForward=1))SELECT * FROM CTE WHERE RowNum = 1";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public int Save_CommitteeSetup(CommiteeSetupMasterDao aMaster, List<CommetteeSetupDetailsDao> aDetailsDaos)
        {
            int pk = 0;

            bool status = false;
            try
            {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                   // aParameters.Add(new SqlParameter("@IsOPD", aMaster.IsOPD));
                    aParameters.Add(new SqlParameter("@ApplicationType", aMaster.ApplicationType));
                    aParameters.Add(new SqlParameter("@SalaryLoationId", aMaster.SalaryLoationId));
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@IsActive", aMaster.IsActive));
                    aParameters.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));

                    if (aMaster.ComSetupMasId == 0)
                    {
                        string query =
                            @" INSERT INTO [dbo].[tblCommiteeSetupMaster]
                        ([ApplicationType]
                        ,[SalaryLoationId]
                        ,[CompanyId]
                        ,[EntryBy]
                        ,[EntryDate]                 
                        ,[IsActive])
                    VALUES
                        (@ApplicationType
                        ,@SalaryLoationId
                        ,@CompanyId
                        ,@EntryBy
                        ,GETDATE()
                        ,@IsActive)";

                        pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);

                        if (pk > 0)
                        {
                            bool result = false;

                            foreach (CommetteeSetupDetailsDao item in aDetailsDaos)
                            {
                                List<SqlParameter> gParameters = new List<SqlParameter>();
                                gParameters.Add(new SqlParameter("@ComSetupMasId", pk));
                                gParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                                gParameters.Add(new SqlParameter("@IsForward", item.IsForward));
                                gParameters.Add(new SqlParameter("@IsApproved", item.IsApproved));
                                gParameters.Add(new SqlParameter("@IsDoctor", item.IsDoctor));
                                gParameters.Add(new SqlParameter("@IsConvenor", item.IsConvenor));
                                gParameters.Add(new SqlParameter("@IsMemberSecretory", item.IsMemberSecretory));
                                gParameters.Add(new SqlParameter("@IsMember", item.IsMember)); 


                               
                      
                                string Deatilsquery = @"INSERT INTO [dbo].[tblCommiteeSetupDetails]
                                ([ComSetupMasId]
                                ,[EmpInfoId]
                                ,[IsForward]
                                ,[IsApproved]
                                ,[IsDoctor],IsConvenor,IsMemberSecretory,IsMember)
                                 VALUES
                                (@ComSetupMasId
                                ,@EmpInfoId 
                                ,@IsForward 
                                ,@IsApproved
                                ,@IsDoctor,@IsConvenor, @IsMemberSecretory, @IsMember)";

                                result = aCommonInternalDal.SaveDataByInsertCommand(Deatilsquery, gParameters, DataBase.HRDB);
                            }
                        }

                    }
                    else
                    {
                        aParameters.Add(new SqlParameter("@ComSetupMasId", aMaster.ComSetupMasId));
                        aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));

                        string query = @"UPDATE [dbo].[tblCommiteeSetupMaster]
                        SET [ApplicationType] = @ApplicationType
                            ,[SalaryLoationId] = @SalaryLoationId
                        ,[CompanyId] = @CompanyId
                      
                        ,[UpdateBy] = @UpdateBy
                        ,[UpdateDate] = GETDATE()
                        
                        WHERE ComSetupMasId=@ComSetupMasId";

                        status =  aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                        if (status)
                        {

                            string DeleteQuery = @"DELETE FROM tblCommiteeSetupDetails WHERE ComSetupMasId="+aMaster.ComSetupMasId;
                            aCommonInternalDal.DeleteDataByDeleteCommand(DeleteQuery, DataBase.HRDB);

                            foreach (CommetteeSetupDetailsDao item in aDetailsDaos)
                            {
                                  List<SqlParameter> gParameters = new List<SqlParameter>();
                                  gParameters.Add(new SqlParameter("@ComSetupMasId", aMaster.ComSetupMasId));
                                  gParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                                  gParameters.Add(new SqlParameter("@IsForward", item.IsForward));
                                  gParameters.Add(new SqlParameter("@IsApproved", item.IsApproved));
                                  gParameters.Add(new SqlParameter("@IsDoctor", item.IsDoctor));
                                  string Deatilsquery = @"INSERT INTO [dbo].[tblCommiteeSetupDetails]
                                ([ComSetupMasId]
                                ,[EmpInfoId]
                                ,[IsForward]
                                ,[IsApproved]
                                ,[IsDoctor])
                                 VALUES
                                (@ComSetupMasId
                                ,@EmpInfoId 
                                ,@IsForward 
                                ,@IsApproved
                                ,@IsDoctor)";
                                  aCommonInternalDal.SaveDataByInsertCommand(Deatilsquery, gParameters, DataBase.HRDB);
                            }

                            pk = aMaster.ComSetupMasId;
                            
                        }
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pk;
        }


        public int Save_CommitteeSetup_Edit(CommiteeSetupMasterDao aMaster, List<CommetteeSetupDetailsDao> aDetailsDaos)
        {
            int pk = 0;

            bool status = false;
            try
            {
                List<SqlParameter> aParameters = new List<SqlParameter>();
                // aParameters.Add(new SqlParameter("@IsOPD", aMaster.IsOPD));
                aParameters.Add(new SqlParameter("@ApplicationType", aMaster.ApplicationType));
                aParameters.Add(new SqlParameter("@SalaryLoationId", aMaster.SalaryLoationId));
                aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                aParameters.Add(new SqlParameter("@IsActive", aMaster.IsActive));
                aParameters.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));

                if (aMaster.ComSetupMasId == 0)
                {
                    string query =
                        @" INSERT INTO [dbo].[tblCommiteeSetupMaster]
                        ([ApplicationType]
                        ,[SalaryLoationId]
                        ,[CompanyId]
                        ,[EntryBy]
                        ,[EntryDate]                 
                        ,[IsActive])
                    VALUES
                        (@ApplicationType
                        ,@SalaryLoationId
                        ,@CompanyId
                        ,@EntryBy
                        ,GETDATE()
                        ,@IsActive)";

                    pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);

                    if (pk > 0)
                    {
                        bool result = false;

                        foreach (CommetteeSetupDetailsDao item in aDetailsDaos)
                        {
                            List<SqlParameter> gParameters = new List<SqlParameter>();
                            gParameters.Add(new SqlParameter("@ComSetupMasId", pk));
                            gParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                            gParameters.Add(new SqlParameter("@IsForward", item.IsForward));
                            gParameters.Add(new SqlParameter("@IsApproved", item.IsApproved));
                            gParameters.Add(new SqlParameter("@IsDoctor", item.IsDoctor));



                            string Deatilsquery = @"INSERT INTO [dbo].[tblCommiteeSetupDetails]
                                ([ComSetupMasId]
                                ,[EmpInfoId]
                                ,[IsForward]
                                ,[IsApproved]
                                ,[IsDoctor])
                                 VALUES
                                (@ComSetupMasId
                                ,@EmpInfoId 
                                ,@IsForward 
                                ,@IsApproved
                                ,@IsDoctor)";
                            result = aCommonInternalDal.SaveDataByInsertCommand(Deatilsquery, gParameters, DataBase.HRDB);



                        }
                    }

                }
                else
                {
                    aParameters.Add(new SqlParameter("@ComSetupMasId", aMaster.ComSetupMasId));
                    aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));

                    string query = @"UPDATE [dbo].[tblCommiteeSetupMaster]
                        SET [ApplicationType] = @ApplicationType
                            ,[SalaryLoationId] = @SalaryLoationId
                        ,[CompanyId] = @CompanyId
                      
                        ,[UpdateBy] = @UpdateBy
                        ,[UpdateDate] = GETDATE()
                        
                        WHERE ComSetupMasId=@ComSetupMasId";

                    status = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (status)
                    {

                        string DeleteQuery = @"DELETE FROM tblCommiteeSetupDetails WHERE ComSetupMasId=" + aMaster.ComSetupMasId;
                        aCommonInternalDal.DeleteDataByDeleteCommand(DeleteQuery, DataBase.HRDB);

                        foreach (CommetteeSetupDetailsDao item in aDetailsDaos)
                        {
                            List<SqlParameter> gParameters = new List<SqlParameter>();
                            gParameters.Add(new SqlParameter("@ComSetupMasId", aMaster.ComSetupMasId));

                            if (item.NewEmpInfoId>0)
                            {
                                gParameters.Add(new SqlParameter("@EmpInfoId", item.NewEmpInfoId));
                                 
                            }
                            else
                            {
                                gParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                                
                            }

                            gParameters.Add(new SqlParameter("@IsForward", item.IsForward));
                            gParameters.Add(new SqlParameter("@IsApproved", item.IsApproved));
                            gParameters.Add(new SqlParameter("@IsDoctor", item.IsDoctor));

                            gParameters.Add(new SqlParameter("@IsConvenor", item.IsConvenor));
                            gParameters.Add(new SqlParameter("@IsMemberSecretory", item.IsMemberSecretory));
                            gParameters.Add(new SqlParameter("@IsMember", item.IsMember)); 


                            string Deatilsquery = @"INSERT INTO [dbo].[tblCommiteeSetupDetails]
                                ([ComSetupMasId]
                                ,[EmpInfoId]
                                ,[IsForward]
                                ,[IsApproved]
                                ,[IsDoctor],IsConvenor,IsMemberSecretory,IsMember)
                                 VALUES
                                (@ComSetupMasId
                                ,@EmpInfoId 
                                ,@IsForward 
                                ,@IsApproved
                                ,@IsDoctor,@IsConvenor, @IsMemberSecretory, @IsMember)";
                            aCommonInternalDal.SaveDataByInsertCommand(Deatilsquery, gParameters, DataBase.HRDB);


                            if (item.IsForward)
                            {

                                string queryForCheck = @"SELECT   lg.ReimbursementSelfAppLogId,   M.ReimbursFromMasterId,   EMP.SalaryLoationId, M.Type,ForEmpInfoId, M.CompanyId  FROM tbl_ReimbursmentFormMaster_HealthCare M
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = Emp.EmpInfoId
LEFT JOIN tblDivision Divi ON M.DivisionId = Divi.DivisionId
LEFT JOIN tblDepartment Dept ON M.DepartmentId = Dept.DepartmentId
LEFT JOIN tblDesignation DEG ON M.DesignationId = DEG.DesignationId
LEFT JOIN tblCompanyInfo COM ON M.CompanyId = COM.CompanyId
LEFT JOIN tblReimbursementSelfAppLog lg ON M.ReimbursFromMasterId = lg.ReimbursFromMasterId
LEFT JOIN TopSheetGenerateDetails_H TSGD ON M.ReimbursFromMasterId = TSGD.ReimbursFromMasterId
left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=M.ReimbursFromMasterId 
INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= M.ReimbursFromMasterId
WHERE M.ReimbursFromMasterId IS NOT NULL AND  Version=CELog.MaxVer AND  M.ReimbursFromMasterId NOT IN (Select ReimbursFromMasterId from TopSheetGenerateDetails_H) AND lg.ActionStatus ='Verified'   and M.ReimbursFromMasterId  in ( SELECT ReimbursFromMasterId FROM tblCommitteeFeedback_HC)  

and EMP.SalaryLoationId=" + aMaster.SalaryLoationId + " and M.Type='" + aMaster.ApplicationType + @"' and M.CompanyId=" + aMaster.CompanyId + @"

--and Emp.EmpMasterCode='50443'";
                                DataTable dt = aCommonInternalDal.DataContainerDataTable(queryForCheck, DataBase.HRDB);

                                foreach (DataRow row in dt.Rows)
                                {
                                    // Accessing data in each row
                                    int reimbursementSelfAppLogId = Convert.ToInt32(row["ReimbursementSelfAppLogId"]);
                                    int reimbursFromMasterId = Convert.ToInt32(row["ReimbursFromMasterId"]);
                                    int salaryLocationId = Convert.ToInt32(row["SalaryLoationId"]);
                                    string type = row["Type"].ToString();
                                    int forEmpInfoId = 0;
                                    try
                                    {
                                        forEmpInfoId = Convert.ToInt32(row["ForEmpInfoId"]);
                                    }
                                    catch
                                    {

                                    }
                                    int companyId = Convert.ToInt32(row["CompanyId"]);



                                    if (forEmpInfoId != item.EmpInfoId)
                                    {
                                        string queryUpdateAppLoag = @"UPDATE [dbo].[tblReimbursementSelfAppLog]
   SET  
       [ForEmpInfoId] = " + item.EmpInfoId + @"
 WHERE ReimbursementSelfAppLogId=" + reimbursementSelfAppLogId;

                                        status = aCommonInternalDal.UpdateDataByUpdateCommand(queryUpdateAppLoag, aParameters, DataBase.HRDB);
                                    }
                                }
                            }


                        }

                        pk = aMaster.ComSetupMasId;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pk;
        }

        public void GetDDLSalaryLocation(DropDownList ddl)
        {
            string queryStr = @"SELECT dt.SalaryLoationId AS Value, dt.SalaryLocation  +' : '+com.ShortName AS TextField FROM dbo.tblSalaryLocation dt
left join tblCompanyInfo com on dt.ComID=com.CompanyId WHERE dt.IsActive=1";

            aCommonInternalDal.LoadDropDownValue(ddl, "TextField", "Value", queryStr, DataBase.HRDB);
        }

        public void GetDDLCompany(DropDownList ddl)
        {
            string queryStr = @"Select CompanyId , ShortName from tblCompanyInfo";

            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);
        }
        public void GetDDLCompanyHC(DropDownList ddl)
        {
            string queryStr = @"SELECT CompanyId, ShortName 
FROM tblCompanyInfo

UNION ALL

SELECT CompanyId, ShortName 
FROM tblCompanyInfoOther";

            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);
        }

        public void GetDDLEmployee(DropDownList ddl)
        {
            string queryStr = @"Select EmpInfoId, EmpMasterCode + ':'+ EmpName As EmpName  from tblEmpGeneralInfo Where IsActive=1";

            aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", queryStr, DataBase.HRDB);
        }




        public DataTable Get_ActivityCheck(string CompanyId, string Type, string Location)
        {
            try
            {
                string query = @"SELECT * FROM tblCommiteeSetupMaster WHERE CompanyId="+CompanyId+" AND ApplicationType='"+Type+"' AND SalaryLoationId="+Location+" ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
