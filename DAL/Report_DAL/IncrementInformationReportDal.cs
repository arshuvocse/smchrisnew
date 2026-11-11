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

namespace DAL.Report_DAL
{
    public class IncrementInformationReportDal
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
                aCommonInternalDal.LoadDropDownValueCompany(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public void LoadIncrementType(DropDownList ddl)
        {
            const string queryStr = " SELECT * FROM tblIncrementInfoMaster WHERE IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "Name", "IncrementInfoMasterId", queryStr, DataBase.HRDB);
        }

        public DataTable GetAllDivision(string compId)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivision  WITH (NOLOCK) WHERE IsActive='1' AND CompanyId='" + compId + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllWing(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivisionWing  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
  WHERE tblDivisionWing.IsActive='1' AND (Invisible='0' OR Invisible IS NULL) " + param + " ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllDepartment(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDepartment  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblDepartment.IsActive='1' AND (tblDepartment.Invisible='0' OR tblDepartment.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSection.IsActive='1' AND (tblSection.Invisible='0' OR tblSection.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSubSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSubSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSubSection.IsActive='1'  " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable LoadInfoIncrementInfoDAL(string param, string param2)
        {
            string queryStr = @"
select distinct * from (SELECT '' Status, tblIncrement.EmployeeId EmpInfoId, EmployeeCode , '5' as IncrementPercent, tblMemoIncrement.IncrementalStep as NewStepId,          tblx.PAmount as Basic,tblxy.PAmount as HouseRent
,tblMedi.PAmount as MedicalAllowance,tblCon.PAmount as ConveyanceAllowance,tblWash.PAmount as WashingAllowance,EffectiveDate,

case when tblIncrement.CompanyId=1 then '100000' else '100080' end as ZID,0 as Conveyance_Deduction ,0 as TAX,

case when tblEmpGeneralInfo.EmpCategoryId=1 then (tblx.PAmount*10)/100  else ((tblx.PAmount+tblxy.PAmount+tblMedi.PAmount+tblCon.PAmount+tblWash.PAmount)*5)/100 end as PF

FROM dbo.tblIncrement
inner join tblEmpGeneralInfo on tblIncrement.EmployeeCode=tblEmpGeneralInfo.EmpMasterCode
inner join tblMemoIncrement on tblIncrement.IncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Basic Pay' ) tblx on tblx.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='House Rent' ) tblxy on tblxy.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Medical' ) tblMedi on tblMedi.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Conveyance' ) tblCon on tblCon.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Washing' ) tblWash on tblWash.MemoIncrementId=tblMemoIncrement.IncrementId

where tblIncrement.IncrementId IS NOT NULL   " + param + @"     
union all 

SELECT '' Status, tblIncrement.EmployeeId EmpInfoId, EmployeeCode , '5' as IncrementPercent, tblMemoIncrement.IncrementalStep as NewStepId,          tblx.PAmount as Basic,tblxy.PAmount as HouseRent
,tblMedi.PAmount as MedicalAllowance,tblCon.PAmount as ConveyanceAllowance,tblWash.PAmount as WashingAllowance,EffectiveDate,

case when tblIncrement.CompanyId=1 then '100000' else '100080' end as ZID,0 as Conveyance_Deduction ,0 as TAX,

case when tblEmpGeneralInfo.EmpCategoryId=1 then (tblx.PAmount*10)/100  else ((tblx.PAmount+tblxy.PAmount+tblMedi.PAmount+tblCon.PAmount+tblWash.PAmount)*5)/100 end as PF

FROM dbo.tblIncrement
inner join tblEmpGeneralInfo on tblIncrement.EmployeeCode=tblEmpGeneralInfo.EmpMasterCode
inner join tblMemoIncrement on tblIncrement.IncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Basic Pay' ) tblx on tblx.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='House Rent' ) tblxy on tblxy.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Medical' ) tblMedi on tblMedi.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Conveyance' ) tblCon on tblCon.MemoIncrementId=tblMemoIncrement.IncrementId
left join (Select PAmount,PName,MemoIncrementId from  tblMemoIncrementDetails where PName='Washing' ) tblWash on tblWash.MemoIncrementId=tblMemoIncrement.IncrementId
 inner JOIN   tblEmpAllRefference reff  ON tblEmpGeneralInfo.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE  EmpMasterCode IS NOT  NULL  and  tblEmpGeneralInfo.IsActive=1  and     reff.ShowCompany in (ComAssain)
 and tblIncrement.IncrementId IS NOT NULL    " + param2 + @"       and     reff.ShowCompany in (ComAssain) ) tbl
 ORDER BY EmployeeCode ASC";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        //public DataTable LoadInfoIncrementInfoDAL(string param)
        //{
        //    List<SqlParameter> aSqlParameters = new List<SqlParameter>();
        //    aSqlParameters.Add(new SqlParameter("@Pram", param));
        //    return aCommonInternalDal.GetDataByStoreProcedure("sp_AccountsIntegrationIncrement", aSqlParameters, "HRDB");
        //}

        public Int32 SaveSeperationConfirmationList(IncrementConfirmationDao aListDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aListDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aListDao.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@ZID", aListDao.ZID));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aListDao.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@PF", aListDao.PF));
            aSqlParameterlist.Add(new SqlParameter("@Approveby", aListDao.Approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", aListDao.ApproveDate));


            string query = @"INSERT INTO tblAccountsIntegration_IncrementList
                           (EmpInfoId
           ,EmpMasterCode
           ,ZID
           ,EffectiveDate
           ,PF
           ,Approveby
           ,ApproveDate
           )
           VALUES
           (
            @EmpInfoId
           ,@EmpMasterCode
           ,@ZID
           ,@EffectiveDate
           ,@PF
           ,@Approveby
           ,@ApproveDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }
    }
}
