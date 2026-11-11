using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.UA_DAO;

namespace DAL.UserPermissions_DAL
{
    public class SupervisorMenuAppDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveSupervisorApp(SupervisorMenuAppDAO appDao)
        {
            if (appDao.SuperMenuAppId==0)
            {
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@CompanyId", appDao.CompanyId));
                //aSqlParameterlist.Add(new SqlParameter("@MainMenuId", appDao.MainMenuId));
                aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", appDao.EmpInfoId));
                aSqlParameterlist.Add(new SqlParameter("@FromEmpInfoId", appDao.FromEmpInfoId));
                aSqlParameterlist.Add(new SqlParameter("@IsAllEmployee", appDao.IsAllEmployee));
                aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                aSqlParameterlist.Add(new SqlParameter("@EntryDate", DateTime.Now));
                //aSqlParameterlist.Add(new SqlParameter("@UpdateDate", appDao.UpdateDate));
                //aSqlParameterlist.Add(new SqlParameter("@UpdateBy", appDao.UpdateBy));



                string query = @"INSERT INTO dbo.tblSupevisorMenuApproval
                            (
                                CompanyId,
                                
                                EmpInfoId,FromEmpInfoId,
                                EntryBy,
                                EntryDate,IsAllEmployee
                            )
                            VALUES
                            (   @CompanyId,
                                
                                @EmpInfoId,@FromEmpInfoId,
                                @EntryBy,
                                @EntryDate,@IsAllEmployee
                            )";
                return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, "HRDB");    
            }
            else
            {
                UpdateSupervisorApp(appDao);
                return appDao.SuperMenuAppId;
            }
            
        }



        public bool UpdateSupervisorApp(SupervisorMenuAppDAO appDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SuperMenuAppId", appDao.SuperMenuAppId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", appDao.CompanyId));
            //aSqlParameterlist.Add(new SqlParameter("@MainMenuId", appDao.MainMenuId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", appDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@FromEmpInfoId", appDao.FromEmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@IsAllEmployee", appDao.IsAllEmployee));

            aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", DateTime.Now));
            //aSqlParameterlist.Add(new SqlParameter("@UpdateDate", appDao.UpdateDate));
            //aSqlParameterlist.Add(new SqlParameter("@UpdateBy", appDao.UpdateBy));



            string query = @"Update dbo.tblSupevisorMenuApproval SET
                            
                                CompanyId=@CompanyId,
                                IsAllEmployee=@IsAllEmployee,
                                EmpInfoId=@EmpInfoId,
                                UpdateBy=@EntryBy,
                                UpdateDate=@EntryDate,FromEmpInfoId=@FromEmpInfoId
                            WHERE SuperMenuAppId=@SuperMenuAppId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public DataTable ApprovalSuperMenus(string menuId)
        {
            string query = @"SELECT * FROM dbo.tblMainMenu WHERE IsApprovalPage=1 AND IsSupervisor=1 AND  MainMenuId='"+menuId+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetPreviousData(string deptId,string menuId)
        {
            string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval
            INNER JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblSupevisorMenuApproval.EmpInfoId
            WHERE DepartmentId='" + deptId + "' AND tblSupevisorMenuApproval.MainMenuId='" + menuId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetPreviousDataEmpWise(string deptId, string empinfoId)
        {
            string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval
            INNER JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblSupevisorMenuApproval.EmpInfoId
            WHERE DepartmentId='" + deptId + "' AND tblSupevisorMenuApproval.EmpInfoId='" + empinfoId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadDepartment(string companyId)
        {
            string query = @"SELECT * FROM dbo.tblDepartment WHERE (Invisible IS NULL OR Invisible=0) AND CompanyId='"+companyId+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadDivision(string companyId)
        {
            string query = @"SELECT * FROM dbo.tblDepartment WHERE IsActive=1 AND CompanyId='" + companyId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public void LoadEmployeeDrop(DropDownList ddl,string departmentId)
        {
            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo 
INNER JOIN (SELECT ReportingEmpId,COUNT(EmpInfoId)AS C FROM dbo.tblEmpGeneralInfo GROUP BY ReportingEmpId HAVING COUNT(EmpInfoId)>0) AS tblt 
ON tblt.ReportingEmpId = tblEmpGeneralInfo.EmpInfoId WHERE DepartmentId='"+departmentId+"'";
            aCommonInternalDal.LoadDropDownValue(ddl,"EmpName","EmpInfoId",query,"HRDB");
        } 
        public void LoadEmployeeDropDiv(DropDownList ddl,string divId)
        {
            string query = "SELECT * FROM dbo.tblEmpGeneralInfo WHERE DivisionId='" + divId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl,"EmpName","EmpInfoId",query,"HRDB");
        }
        public DataTable GetData(string companyId,string menuId)
        {
            DataTable aDataTable = new DataTable();
            aDataTable.Columns.Add("MenuName");
            aDataTable.Columns.Add("SuperMenuAppId");
            aDataTable.Columns.Add("MainMenuId");
            aDataTable.Columns.Add("DepartmentId");
            aDataTable.Columns.Add("DepartmentName");
            aDataTable.Columns.Add("EmpInfoId");
            aDataTable.Columns.Add("IsChecked");
            DataRow dataRow = null;

            DataTable dtappsupermenu = ApprovalSuperMenus(menuId);
            DataTable dtloaddeprtmnet = LoadDivision(companyId);
            for (int i = 0; i < dtappsupermenu.Rows.Count; i++)
            {

                for (int j = 0; j < dtloaddeprtmnet.Rows.Count; j++)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["MenuName"] = dtappsupermenu.Rows[i]["MenuName"].ToString();
                    dataRow["MainMenuId"] = dtappsupermenu.Rows[i]["MainMenuId"].ToString();
                    dataRow["DepartmentId"] = dtloaddeprtmnet.Rows[j]["DepartmentId"].ToString();
                    dataRow["DepartmentName"] = dtloaddeprtmnet.Rows[j]["DepartmentName"].ToString();

                    DataTable dtpredata = GetPreviousData(dtloaddeprtmnet.Rows[j]["DepartmentId"].ToString(),
                        dtappsupermenu.Rows[i]["MainMenuId"].ToString());
                    if (dtpredata.Rows.Count>0)
                    {
                        dataRow["SuperMenuAppId"] = dtpredata.Rows[0]["SuperMenuAppId"].ToString();
                        dataRow["EmpInfoId"] = dtpredata.Rows[0]["EmpInfoId"].ToString();
                        dataRow["IsChecked"] = "True";
                        
                    }
                    else
                    {
                        dataRow["SuperMenuAppId"] = "0";
                        dataRow["EmpInfoId"] = "0";
                        dataRow["IsChecked"] = "False";
                    }

                    aDataTable.Rows.Add(dataRow);
                }

            }
            return aDataTable;

        }

        public void ReportingEmpData(string empinfoid, DataTable aDataTable)
        {
            DataRow dataRow = null;
            DataTable dtdata1 = LoadEmpGenInfo(" AND E.EmpInfoId='" + empinfoid + "' ");
            DataTable dtdata = LoadEmpGenInfo(" AND E.EmpInfoId='" + dtdata1.Rows[0]["ReportingEmpId"].ToString() + "' ");
            
            if (dtdata.Rows.Count>0)
            {
                dataRow = aDataTable.NewRow();
                dataRow["EmpInfoId"] = dtdata.Rows[0]["FromEmpInfoId"].ToString();
                dataRow["EmpName"] = dtdata.Rows[0]["EmpName"].ToString();
                dataRow["EmpMasterCode"] = dtdata.Rows[0]["EmpMasterCode"].ToString();
                aDataTable.Rows.Add(dataRow);

                ReportingEmpData(dtdata.Rows[0]["FromEmpInfoId"].ToString(), aDataTable);
            }

        }



        public void ReportingEmpDataFinalApprover(string empinfoid, DataTable aDataTable, string fAppId)
        {
            DataRow dataRow = null;
            DataTable dtdata1 = LoadEmpGenInfoFirEMp(" AND E.EmpInfoId='" + empinfoid + "' ");
            DataTable dtdata = LoadEmpGenInfoFirEMp(" AND E.EmpInfoId='" + dtdata1.Rows[0]["ReportingEmpId"].ToString() + "' ");
            bool cc = false; 
            if (dtdata.Rows.Count > 0)
            {
                if (fAppId == dtdata.Rows[0]["FromEmpInfoId"].ToString())
                {
                    cc=true;
                }
                if (cc==false)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = dtdata.Rows[0]["FromEmpInfoId"].ToString();
                    dataRow["EmpName"] = dtdata.Rows[0]["EmpName"].ToString();
                    dataRow["EmpMasterCode"] = dtdata.Rows[0]["EmpMasterCode"].ToString();
                    aDataTable.Rows.Add(dataRow);

                    ReportingEmpDataFinalApprover(dtdata.Rows[0]["FromEmpInfoId"].ToString(), aDataTable, fAppId);
           
                }
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

			
			 WHERE E.IsActive=1 and (E.EmpMasterCode IS NOT NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadEmpGenInfoFirEMp(string param)
        {
            string query = @" SELECT EmpF.EmpMasterCode+' : ' +EmpF.EmpName +  ISNULL( ' : ' +DSGF.Designation,'') FinalApprover, ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName    AS EmpName, CASE WHEN SMA.IsAllEmployee=1 THEN  'True' ELSE 'False' END IsAllEmployee , Dv.DivisionName, SMA.EmpInfoId,DSG.Designation,DP.DepartmentName,SMA.SuperMenuAppId,(CASE WHEN SMA.SuperMenuAppId IS NULL THEN 'False' ELSE 'True' END)IsChecked,DP.DepartmentId,E.EmpInfoId as FromEmpInfoId,
E.EmpMasterCode,E.EmpName,E.ReportingEmpId , E.EmpCategoryId, E.SalaryGradeId  FROM dbo.tblEmpGeneralInfo E  With (nolock)
            LEFT JOIN dbo.tblDepartment DP  With (nolock) ON DP.DepartmentId = E.DepartmentId
            LEFT JOIN dbo.tblDesignation DSG  With (nolock)  ON DSG.DesignationId = E.DesignationId
  LEFT JOIN dbo.tblDivision Dv  With (nolock) ON Dv.DivisionId = E.DivisionId
			LEFT JOIN dbo.tblSupevisorMenuApproval SMA  With (nolock) ON E.EmpInfoId=SMA.FromEmpInfoId
  LEFT JOIN dbo.tblEmpGeneralInfo EmpF  With (nolock) ON SMA.EmpInfoId = EmpF.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGF  With (nolock)  ON DSGF.DesignationId = EmpF.DesignationId

			
			 WHERE E.IsActive=1 and (E.EmpMasterCode IS NOT NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadEmpGenInfoNeee(string param, string param2)
        {
            string query = @" 
select distinct * from (
SELECT EmpF.EmpMasterCode+' : ' +EmpF.EmpName +  ISNULL( ' : ' +DSGF.Designation,'') FinalApprover, ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName +  ISNULL( ' : ' +DSG.Designation,'')  +  ISNULL(' : ' +DP.DepartmentName, '')  AS EmpName, CASE WHEN SMA.IsAllEmployee=1 THEN  'True' ELSE 'False' END IsAllEmployee , Dv.DivisionName, SMA.EmpInfoId,DSG.Designation,DP.DepartmentName,0 SuperMenuAppId,1 IsChecked,DP.DepartmentId,E.EmpInfoId as FromEmpInfoId,
E.EmpMasterCode, E.ReportingEmpId , E.EmpCategoryId, E.SalaryGradeId  FROM dbo.tblEmpGeneralInfo E  With (nolock)
            LEFT JOIN dbo.tblDepartment DP  With (nolock) ON DP.DepartmentId = E.DepartmentId
            LEFT JOIN dbo.tblDesignation DSG  With (nolock)  ON DSG.DesignationId = E.DesignationId
  LEFT JOIN dbo.tblDivision Dv  With (nolock) ON Dv.DivisionId = E.DivisionId
			LEFT JOIN dbo.tblSupevisorMenuApproval SMA  With (nolock) ON E.EmpInfoId=SMA.FromEmpInfoId
  LEFT JOIN dbo.tblEmpGeneralInfo EmpF  With (nolock) ON SMA.EmpInfoId = EmpF.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGF  With (nolock)  ON DSGF.DesignationId = EmpF.DesignationId

			
			 WHERE E.IsActive=1 and (E.EmpMasterCode IS NOT NULL) " + param + @"    union all

			 SELECT EmpF.EmpMasterCode+' : ' +EmpF.EmpName +  ISNULL( ' : ' +DSGF.Designation,'') FinalApprover, ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName +  ISNULL( ' : ' +DSG.Designation,'')  +  ISNULL(' : ' +DP.DepartmentName, '')  AS EmpName, CASE WHEN SMA.IsAllEmployee=1 THEN  'True' ELSE 'False' END IsAllEmployee , Dv.DivisionName, SMA.EmpInfoId,DSG.Designation,DP.DepartmentName,0 SuperMenuAppId,  1 IsChecked,DP.DepartmentId,E.EmpInfoId as FromEmpInfoId,
E.EmpMasterCode,E.ReportingEmpId , E.EmpCategoryId, E.SalaryGradeId  FROM dbo.tblEmpGeneralInfo E  With (nolock)
            LEFT JOIN dbo.tblDepartment DP  With (nolock) ON DP.DepartmentId = E.DepartmentId
            LEFT JOIN dbo.tblDesignation DSG  With (nolock)  ON DSG.DesignationId = E.DesignationId
  LEFT JOIN dbo.tblDivision Dv  With (nolock) ON Dv.DivisionId = E.DivisionId
			LEFT JOIN dbo.tblSupevisorMenuApproval SMA  With (nolock) ON E.EmpInfoId=SMA.FromEmpInfoId
  LEFT JOIN dbo.tblEmpGeneralInfo EmpF  With (nolock) ON SMA.EmpInfoId = EmpF.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGF  With (nolock)  ON DSGF.DesignationId = EmpF.DesignationId
    inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE     e.IsActive=1  and     reff.ShowCompany in (ComAssain)  " + param2 + ") tbl";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable EmployeeFinalApproverReport(string param, string param2)
        {
            string query = @" select Distinct * from ( SELECT  EmpF.EmpMasterCode+' : ' +EmpF.EmpName +  ISNULL( ' : ' +DSGF.Designation,'') FinalApprover,  EmpRep.EmpMasterCode+' : ' +EmpRep.EmpName +  ISNULL( ' : ' +DSGFRep.Designation,'') ReportingEmp, EmpLv.EmpMasterCode+' : ' +EmpLv.EmpName +  ISNULL( ' : ' +DSGFLV.Designation,'') LeaveEmp, ISNULL(e.EmpMasterCode+' : ','')   + e.EmpName EmpName,  ISNULL( DSG.Designation,'')  Designation,  ISNULL( DP.DepartmentName, '')  AS DepartmentName,   ISNULL( Dv.DivisionName, '')  AS DivisionName, sal.SalaryLocation FROM dbo.tblEmpGeneralInfo E  With (nolock)
            LEFT JOIN dbo.tblDepartment DP  With (nolock) ON DP.DepartmentId = E.DepartmentId
            LEFT JOIN dbo.tblDesignation DSG  With (nolock)  ON DSG.DesignationId = E.DesignationId
  LEFT JOIN dbo.tblDivision Dv  With (nolock) ON Dv.DivisionId = E.DivisionId
			LEFT JOIN dbo.tblSupevisorMenuApproval SMA  With (nolock) ON E.EmpInfoId=SMA.FromEmpInfoId
  LEFT JOIN dbo.tblEmpGeneralInfo EmpF  With (nolock) ON SMA.EmpInfoId = EmpF.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGF  With (nolock)  ON DSGF.DesignationId = EmpF.DesignationId
    
  LEFT JOIN dbo.tblEmpGeneralInfo EmpRep  With (nolock) ON E.ReportingEmpId = EmpRep.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGFRep  With (nolock)  ON DSGFRep.DesignationId = EmpRep.DesignationId

   LEFT JOIN dbo.tblEmpGeneralInfo EmpLv  With (nolock) ON E.LeaveApprovalId = EmpLv.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGFLV  With (nolock)  ON DSGFLV.DesignationId = EmpLv.DesignationId
    
   LEFT JOIN dbo.tblSalaryLocation sal  With (nolock)  ON sal.SalaryLoationId = e.SalaryLoationId
			 WHERE E.IsActive=1  " + param + @"   union all 


			  SELECT  EmpF.EmpMasterCode+' : ' +EmpF.EmpName +  ISNULL( ' : ' +DSGF.Designation,'') FinalApprover,  EmpRep.EmpMasterCode+' : ' +EmpRep.EmpName +  ISNULL( ' : ' +DSGFRep.Designation,'') ReportingEmp, EmpLv.EmpMasterCode+' : ' +EmpLv.EmpName +  ISNULL( ' : ' +DSGFLV.Designation,'') LeaveEmp, ISNULL(e.EmpMasterCode+' : ','')   + e.EmpName EmpName,  ISNULL( DSG.Designation,'')  Designation,  ISNULL( DP.DepartmentName, '')  AS DepartmentName,   ISNULL( Dv.DivisionName, '')  AS DivisionName, sal.SalaryLocation FROM dbo.tblEmpGeneralInfo E  With (nolock)
            LEFT JOIN dbo.tblDepartment DP  With (nolock) ON DP.DepartmentId = E.DepartmentId
            LEFT JOIN dbo.tblDesignation DSG  With (nolock)  ON DSG.DesignationId = E.DesignationId
  LEFT JOIN dbo.tblDivision Dv  With (nolock) ON Dv.DivisionId = E.DivisionId
			LEFT JOIN dbo.tblSupevisorMenuApproval SMA  With (nolock) ON E.EmpInfoId=SMA.FromEmpInfoId
  LEFT JOIN dbo.tblEmpGeneralInfo EmpF  With (nolock) ON SMA.EmpInfoId = EmpF.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGF  With (nolock)  ON DSGF.DesignationId = EmpF.DesignationId
    
  LEFT JOIN dbo.tblEmpGeneralInfo EmpRep  With (nolock) ON E.ReportingEmpId = EmpRep.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGFRep  With (nolock)  ON DSGFRep.DesignationId = EmpRep.DesignationId

   LEFT JOIN dbo.tblEmpGeneralInfo EmpLv  With (nolock) ON E.LeaveApprovalId = EmpLv.EmpInfoId
   LEFT JOIN dbo.tblDesignation DSGFLV  With (nolock)  ON DSGFLV.DesignationId = EmpLv.DesignationId
    
   LEFT JOIN dbo.tblSalaryLocation sal  With (nolock)  ON sal.SalaryLoationId = e.SalaryLoationId
     inner JOIN   tblEmpAllRefference reff  ON E.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE        reff.ShowCompany in (ComAssain) 
			 and E.IsActive=1  " + param2 + " )tbl  ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadEmpGenInfoGetRef(string param)
        {
            string query = @"SELECT  E.EmpInfoId as FromEmpInfoId   FROM dbo.tblEmpGeneralInfo E 
            LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = E.DepartmentId
            LEFT JOIN dbo.tblDesignation DSG ON DSG.DesignationId = E.DesignationId
  LEFT JOIN dbo.tblDivision Dv ON Dv.DivisionId = E.DivisionId
		  WHERE (E.EmpMasterCode IS NOT NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
    }
}
