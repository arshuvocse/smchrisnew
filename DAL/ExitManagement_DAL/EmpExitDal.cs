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
using DAO.MeetingMinorsDAO;

namespace DAL.Survey
{
    public class EmpExitDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            //string query = "SELECT * FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);
        }

        public DataTable GetEmpDDL( string ID)
        {
            string queryStr = @"SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName,  e.EmpInfoId
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL AND e.CompanyId=" + ID;
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }


        public DataTable GetEmpDDLiSiNACTIVE(string ID)
        {
            //            string queryStr = @"select distinct * from ( SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName +  ISNULL(' : ' + d.Designation,'')  +  ISNULL(' : ' +dept.DepartmentName, '')  AS EmpName 
            //FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
            //left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
            //left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL and e.IsActive=0 AND e.CompanyId=" + ID   + @"

            //union all 

            //SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName +  ISNULL(' : ' + d.Designation,'')  +  ISNULL(' : ' +dept.DepartmentName, '')  AS EmpName 
            //FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
            //left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
            //left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId   
            // inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId   
            //    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
            // WHERE  EmpMasterCode IS NOT  NULL  and  e.IsActive=1  and     reff.ShowCompany in (ComAssain) ) tbl";

            string queryStr = @"SELECT DISTINCT * FROM (

SELECT e.EmpInfoId,
       ISNULL(e.EmpMasterCode,'') + ' : ' + e.EmpName
       + ISNULL(' : ' + d.Designation,'')
       + ISNULL(' : ' + dept.DepartmentName, '') AS EmpName
FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
LEFT JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
LEFT JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE EmpMasterCode IS NOT NULL
  AND e.IsActive = 0
  AND e.CompanyId=" + ID + @"

UNION ALL

SELECT e.EmpInfoId,
       ISNULL(e.EmpMasterCode,'') + ' : ' + e.EmpName
       + ISNULL(' : ' + d.Designation,'')
       + ISNULL(' : ' + dept.DepartmentName, '') AS EmpName
FROM dbo.tblEmpGeneralInfo e WITH (NOLOCK)
LEFT JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
LEFT JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
INNER JOIN tblEmpAllRefference reff ON e.EmpInfoId = reff.RefferenceEmpId
INNER JOIN (
    SELECT NewEmployeeId,
           CASE
               WHEN IsSMCRecordView = 1 THEN '1'
               WHEN IsELRecordView = 1 THEN '2'
               ELSE '1,2'
           END ComAssain
    FROM tblEmpSpecialTransfer
    WHERE OnlyView = 1
) tblPer ON reff.EmployeeId = tblPer.NewEmployeeId
WHERE EmpMasterCode IS NOT NULL
  AND e.IsActive = 0
  AND reff.ShowCompany IN (ComAssain)

) tbl";
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }
        public DataTable GetEmpDDLByDepartMent(string comId, string dptID)
        {
            string queryStr = @"SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName,  e.EmpInfoId
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL AND e.IsActive=1 AND e.CompanyId=" + comId + " AND e.DepartmentId=" + dptID;
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }

        public DataTable GetEmpDDLByDepartMentSMCCel(string dptID)
        {
            string queryStr = @"SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName,  e.EmpInfoId
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL AND e.IsActive=1 AND e.DepartmentId=" + dptID;
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }

        public DataTable LoadEmployeeInfo(string employeeId, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", employeeId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            //   const string queryStr = @"SELECT EGI.SalaryGradeId, EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
            //                            DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName
            //                           FROM tblEmpGeneralInfo AS EGI 
            //                           LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
            //                           LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
            //                           LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
            //                           LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
            //                           LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
            //                           LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
            //                           LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
            //LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
            //LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId  WHERE EGI.EmpInfoId = @EmployeeId AND EGI.CompanyId = @CompanyId";

            const string queryStr = @"SELECT DISTINCT * 
FROM
(
    SELECT 
        EGI.SalaryGradeId,
        EGI.EmpInfoId,
        EGI.EmpMasterCode,
        EGI.EmpName,
        EGI.DateOfJoin,
        DSN.DivisionId,
        DSN.DivisionName,
        DPT.DepartmentId,
        DPT.DepartmentName,
        DSG.DesignationId,
        DSG.Designation,
        SLG.SalaryGradeId AS SalaryGradeId2,
        SLG.GradeName
    FROM tblEmpGeneralInfo AS EGI
    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
    LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
    LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId
    WHERE EGI.EmpInfoId = @EmployeeId AND EGI.CompanyId = @CompanyId

    UNION ALL

    SELECT 
        EGI.SalaryGradeId,
        EGI.EmpInfoId,
        EGI.EmpMasterCode,
        EGI.EmpName,
        EGI.DateOfJoin,
        DSN.DivisionId,
        DSN.DivisionName,
        DPT.DepartmentId,
        DPT.DepartmentName,
        DSG.DesignationId,
        DSG.Designation,
        SLG.SalaryGradeId AS SalaryGradeId2,
        SLG.GradeName
    FROM tblEmpGeneralInfo AS EGI
    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
    LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
    LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId
    INNER JOIN tblEmpAllRefference reff ON EGI.EmpInfoId = reff.RefferenceEmpId
    INNER JOIN
    (
        SELECT 
            NewEmployeeId,
            CASE 
                WHEN IsSMCRecordView = 1 THEN '1'
                WHEN IsELRecordView = 1 THEN '2'
                ELSE '1,2'
            END AS ComAssain
        FROM tblEmpSpecialTransfer
        WHERE OnlyView = 1
    ) tblPer ON reff.EmployeeId = tblPer.NewEmployeeId
    WHERE EGI.EmpInfoId = @EmployeeId
      AND reff.ShowCompany IN (ComAssain)
) T;";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable LoadExitDepartment(string companyId)
        {
            string queryStr = @"SELECT DSN.DepartmentId,
                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId + " AND DSN.DepartmentId<>'3'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadEmpDivision(string empID)
        {
            string queryStr = @"select * from  tblEmpGeneralInfo where EmpInfoId=  " + empID ;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        
        
        public DataTable LoadExitMAster(string MID)
        {
            string queryStr = @"SELECT * FROM    tblEmpExitMaster WHERE ExitMasterId=" + MID;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadExitMAsterSupervisor(string MID)
        {
            string queryStr = @"SELECT  'Employee ID: '+ emp.EmpMasterCode+ ' , Employee Name: '+ emp.EmpName EmpName FROM    tblEmpExitDetail
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = tblEmpExitDetail.EmpInfoIdApproval
 WHERE  ApprovalStatus='as Supervisor' AND MasterId=" + MID;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadExitMAsterDepartment(string MID)
        {
            string queryStr = @"SELECT  'Employee ID: '+ emp.EmpMasterCode+ ' , Employee Name: '+ emp.EmpName EmpName FROM    tblEmpExitDetail
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = tblEmpExitDetail.EmpInfoIdApproval WHERE   ApprovalStatus='as Department' AND MasterId=" + MID;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable LoadExitDepartmentNotInEmployee(string companyId,string empinfoId)
        {
            string queryStr = @"SELECT 'Dep' SetInfo,DSN.DepartmentId,
                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId + @"

								UNION ALL 
SELECT DISTINCT 'Div'SetInfo ,tblDivision.DivisionId,tblDivision.DivisionName FROM dbo.tblEmpGeneralInfo 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
WHERE tblEmpGeneralInfo.IsActive=1 AND DepartmentId IS NULL AND tblEmpGeneralInfo.DivisionId IS NOT NULL ANd tblEmpGeneralInfo.CompanyId =  " + companyId + " " +
                              "" +
                              "" +
                              "" +
                              "" +
                              @"union all
SELECT DISTINCT 'Div'SetInfo ,tblDivision.DivisionId,tblDivision.DivisionName FROM dbo.tblEmpGeneralInfo 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
WHERE tblEmpGeneralInfo.IsActive=1 AND DepartmentId IS NULL AND tblEmpGeneralInfo.DivisionId IS NOT NULL ANd tblEmpGeneralInfo.CompanyId =2 and tblEmpGeneralInfo.DivisionId=45" ;

//            string queryStr = @"SELECT DSN.DepartmentId,
//                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId + " AND DSN.DepartmentId NOT IN (SELECT DepartmentId FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId='" + empinfoId + "')";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable LoadExitDepartmentNotInEmployeeSmcEL(string companyId, string empinfoId)
        {
            string queryStr = @"SELECT  'Dep' SetInfo,  DSN.DepartmentId,
                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId + " " +
                              "" +
                              "" +
                              "" +
                              "" +
                              @"UNION ALL 

								SELECT  'Dep' SetInfo,  DSN.DepartmentId,
                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.DepartmentId IN (5,20,8)

		UNION ALL 
SELECT DISTINCT 'Div'SetInfo ,tblDivision.DivisionId,tblDivision.DivisionName FROM dbo.tblEmpGeneralInfo 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblEmpGeneralInfo.DivisionId
WHERE tblEmpGeneralInfo.IsActive=1 AND DepartmentId IS NULL AND tblEmpGeneralInfo.DivisionId IS NOT NULL ANd tblEmpGeneralInfo.CompanyId = " + companyId +
                              "" +
                              @"union all
SELECT 'Dep' SetInfo,DSN.DepartmentId,
                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId = 1 AND DepartmentName ='Corporate Communication' ";

            //            string queryStr = @"SELECT DSN.DepartmentId,
            //                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId + " AND DSN.DepartmentId NOT IN (SELECT DepartmentId FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId='" + empinfoId + "')";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable LoadEmployeeInfoDeptWise(string empinfoId)
        {
            string queryStr = @"SELECT * FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId='"+empinfoId+"'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable LoadEmployeeInfoParameter(string param)
        {
            string queryStr = @"SELECT  ISNULL(EmpMasterCode+' : '  ,'') +   EmpName   AS EmpName,* FROM dbo.tblEmpGeneralInfo " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public bool IsEmployeeDeclaredInClearanceAutoForward(string empInfoId)
        {
            const string queryStr = @"SELECT TOP 1 1 FROM dbo.tblClearanceForwardingMaster WITH (NOLOCK)
                                      WHERE EmployeeId = @EmpId AND Status = 'Forwarded'";
            var aSqlParameterlist = new List<SqlParameter> { new SqlParameter("@EmpId", empInfoId) };
            DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
            return dt != null && dt.Rows.Count > 0;
        }

        public DataTable CheckExistEmployee(string id)
        {
            string queryStr = @"SELECT * FROM dbo.tblEmpExitMaster WHERE EmployeeId='"+id+"'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
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
        public bool SaveDocumentDetails(List<EmpExitDocumentDAO> aList, int masterid)
        {
            try
            {
                List<SqlParameter> aParametersd = new List<SqlParameter>();
                aParametersd.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
                string queryDel = @"DELETE FROM [dbo].[tblEmpExitDocument]
      WHERE  ExitMasterId=@MiscellaneousInfoId";

                bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@MiscellaneousInfoId", masterid));
                    aParameters.Add(new SqlParameter("@DocumentLink", item.DocumentLink));
                    aParameters.Add(new SqlParameter("@DocumentNote", item.DocumentNote));
                    aParameters.Add(new SqlParameter("@FileName", item.FileName));




                    string query = @"INSERT INTO [dbo].[tblEmpExitDocument]
           ([ExitMasterId]
           ,[DocumentLink]
           ,[DocumentNote],FileName)
     VALUES
           (@MiscellaneousInfoId
           ,@DocumentLink 
           ,@DocumentNote,@FileName)";
                    result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                    if (result == false)
                    {
                        return false;
                    }


                }
                return result;


            }
            catch (Exception x)
            {

                throw;
            }
        }


        public int SaveExitMasterInfo(EmpExitMasterDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aDao.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", aDao.EmpName));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aDao.DesignationId));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDao.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aDao.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@Description", aDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aDao.EntryDate));

            string query = @"INSERT INTO dbo.tblEmpExitMaster
                            (
                                CompanyId,
                                EmployeeId,
                                EmpCode,
                                EmpName,
                                JoiningDate,
                                DivisionId,
                                DesignationId,
                                SalaryGradeId,
                                Description,
                                ActionStatus,
                                EntryBy,
                                EntryDate
                            )
                            VALUES
                            (  
                                @CompanyId,
                                @EmployeeId,
                                @EmpCode,
                                @EmpName,
                                @JoiningDate,
                                @DivisionId,
                                @DesignationId,
                                @SalaryGradeId,
                                @Description,
                                @ActionStatus,
                                @EntryBy,
                                @EntryDate
 
                            )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }
        public int SaveExitMasterInfoSales(EmpExitMasterDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aDao.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", aDao.EmpName));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aDao.DesignationId));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDao.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aDao.SalaryGradeId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aDao.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@Description", aDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsSales", "True"));

            string query = @"INSERT INTO dbo.tblEmpExitMaster
                            (
                                CompanyId,
                                EmployeeId,
                                EmpCode,
                                EmpName,
                                JoiningDate,
                                DivisionId,
                                DesignationId,
                                SalaryGradeId,
                                Description,
                                ActionStatus,
                                EntryBy,
                                EntryDate,IsSales
                            )
                            VALUES
                            (  
                                @CompanyId,
                                @EmployeeId,
                                @EmpCode,
                                @EmpName,
                                @JoiningDate,
                                @DivisionId,
                                @DesignationId,
                                @SalaryGradeId,
                                @Description,
                                @ActionStatus,
                                @EntryBy,
                                @EntryDate,@IsSales
 
                            )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }
        public int SaveExitDetailInfo(EmpExitDetailDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterId", aDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeIdForClearance", aDao.EmployeeIdForClearance));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@SetInfo", aDao.SetInfo));

            string query = @"INSERT INTO dbo.tblEmpExitDetail
                            (
                                MasterId,
                                DepartmentId,
                                EmpInfoId,
IsDone, EmployeeIdForClearance,EmpInfoIdApproval,ApprovalStatus,SetInfo
                            )
                            VALUES
                            (   @MasterId,
                                @DepartmentId,
                                @EmpInfoId,
0,@EmployeeIdForClearance,@EmpInfoId,@ApprovalStatus,@SetInfo
                            )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }

        public void ReportingEmpData(string empinfoid, DataTable aDataTable)
        {
            DataRow dataRow = null;
            DataTable dtdata1 = LoadEmpGenInfo(" AND E.EmpInfoId='" + empinfoid + "' ");
            DataTable dtdata = LoadEmpGenInfo(" AND E.EmpInfoId='" + dtdata1.Rows[0]["ReportingEmpId"].ToString() + "' ");

            if (dtdata.Rows.Count > 0)
            {
                dataRow = aDataTable.NewRow();
                dataRow["EmpInfoId"] = dtdata.Rows[0]["FromEmpInfoId"].ToString();
                dataRow["EmpName"] = dtdata.Rows[0]["EmpName"].ToString();
                dataRow["EmpMasterCode"] = dtdata.Rows[0]["EmpMasterCode"].ToString();
                dataRow["DepartmentId"] = dtdata.Rows[0]["DepartmentId"].ToString();
                aDataTable.Rows.Add(dataRow);

                ReportingEmpData(dtdata.Rows[0]["FromEmpInfoId"].ToString(), aDataTable);
            }

        }
        public DataTable LoadEmpGenInfo(string param)
        {
            string query = @" SELECT EmpF.EmpMasterCode+' : ' +EmpF.EmpName +  ISNULL( ' : ' +DSGF.Designation,'') FinalApprover, ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName +  ISNULL( ' : ' +DSG.Designation,'')  +  ISNULL(' : ' +DP.DepartmentName, '')  AS EmpName, CASE WHEN SMA.IsAllEmployee=1 THEN  'True' ELSE 'False' END IsAllEmployee , Dv.DivisionName, SMA.EmpInfoId,DSG.Designation,DP.DepartmentName,SMA.SuperMenuAppId,(CASE WHEN SMA.SuperMenuAppId IS NULL THEN 'False' ELSE 'True' END)IsChecked,DP.DepartmentId,E.EmpInfoId as FromEmpInfoId,
E.EmpMasterCode,E.EmpName,E.ReportingEmpId , E.EmpCategoryId, E.SalaryGradeId  FROM dbo.tblEmpGeneralInfo E  With (nolock)
            LEFT JOIN dbo.tblDepartment DP  With (nolock) ON DP.DepartmentId = E.DepartmentId
            LEFT JOIN dbo.tblDesignation DSG  With (nolock)  ON DSG.DesignationId = E.DesignationId
  LEFT JOIN dbo.tblDivision Dv  With (nolock) ON Dv.DivisionId = E.DivisionId
			LEFT JOIN dbo.tblSupevisorMenuApproval SMA  With (nolock) ON E.EmpInfoId=SMA.FromEmpInfoId
  LEFT JOIN dbo.tblEmpGeneralInfo EmpF  With (nolock) ON SMA.EmpInfoId = EmpF.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGF  With (nolock)  ON DSGF.DesignationId = EmpF.DesignationId

			
			 WHERE  (E.EmpMasterCode IS NOT NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
    }
}
