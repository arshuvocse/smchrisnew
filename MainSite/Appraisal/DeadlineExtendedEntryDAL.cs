using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.Appraisal
{
  public  class DeadlineExtendedEntryDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();
        public void LoadDept(DropDownList ddl, string compId)
        {
            string query = @"SELECT * FROM dbo.tblDepartment WHERE Invisible IS NULL and CompanyId='" + compId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, DataBase.HRDB);
        }
        public bool DeleteEmployeeJobLeftById(DeadlineExtendedEntryDAO aMaster)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DeadlineExtensionRequestId", aMaster.DeadlineExtensionRequestId));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aMaster.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aMaster.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aMaster.DeleteDate));


            const string query = @"Update tblDeadlineExtensionRequest  set IsDelete=@IsDelete, DeleteBy=@DeleteBy, DeleteDate=@DeleteDate  WHERE DeadlineExtensionRequestId = @DeadlineExtensionRequestId";
            //  const string query = @"DELETE FROM tblEmployeeJobLeft WHERE EmployeeJobLeftId = @EmployeeJobLeftId";
            return _aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }
        public DataTable CheckPreviousDeadlineExtentionDate(int com, string operation, int fin, int masterId)
        {
            try
            {
                string query = @"select * from tblDeadlineExtensionRequest where CompanyId =" + com + " and FinYearId = " + fin + " and Operation='" + operation + "' and DeadlineExtensionRequestId!= " + masterId + " ";
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
                string query = @" SELECT    dtls.EmployeeId EmpInfoId,dtls.DeadlineExtensionRequestDetailsId, dtls.DeadlineExtensionRequestId, empInfo.EmpMasterCode, empInfo.EmpName, dtls.ExtensionDate ExtendedDate,   0 AS DivisionName,
 desg.Designation, Dpt.DepartmentName  FROM   tblDeadlineExtensionRequestDetails dtls
INNER JOIN dbo.tblEmpGeneralInfo empInfo ON  dtls.EmployeeId=empInfo.EmpInfoId
left JOIN dbo.tblDesignation desg ON  empInfo.DesignationId=desg.DesignationId

LEFT JOIN dbo.tblDeadlineExtensionRequest Req ON    dtls.DeadlineExtensionRequestId=Req.DeadlineExtensionRequestId
left JOIN dbo.tblDepartment Dpt ON  Req.DepartmentId=Dpt.DepartmentId
 
 where dtls.DeadlineExtensionRequestId = " +
                    id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public DataTable GetAppraisalSetupDetailsForApproval(string Parm )
        {
            try
            {
                string query = @" SELECT    Req.Operation,dtls.EmployeeId EmpInfoId,dtls.DeadlineExtensionRequestDetailsId, dtls.DeadlineExtensionRequestId, empInfo.EmpMasterCode, empInfo.EmpName,   CONVERT(nvarchar (11),dtls.ExtensionDate , 106)ExtendedDate,   0 AS DivisionName,
 desg.Designation, Dpt.DepartmentName  FROM   tblDeadlineExtensionRequestDetails dtls
INNER JOIN dbo.tblEmpGeneralInfo empInfo ON  dtls.EmployeeId=empInfo.EmpInfoId
left JOIN dbo.tblDesignation desg ON  empInfo.DesignationId=desg.DesignationId

left JOIN dbo.tblDeadlineExtensionRequest Req ON  dtls.DeadlineExtensionRequestId=Req.DeadlineExtensionRequestId
left JOIN dbo.tblCompanyInfo Com ON  Req.CompanyId=Com.CompanyId
left JOIN dbo.tblFinancialYear FinY ON  Req.FinYearId=FinY.FinancialYearId

left JOIN dbo.tblDepartment Dpt ON  Req.DepartmentId=Dpt.DepartmentId
 
 where dtls.ApprovalStatus = 'Posted' " + Parm + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public DataTable GetEmpForAppraisalDeadLineNew(int com, string deadLine,  string param)
        {
            try
            {
                string query =
                    @" select dptUU.DepartmentId EmpDepartmentNameID, A.ReportingEmpId as EmpInfoId , Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName ,count(a.EmpInfoId)TotalEmployee, '" + deadLine + "' as ExtendedDate   from tblEmpGeneralInfo A " +
                    "left join tblEmpGeneralInfo Aa on a.ReportingEmpId = aa.EmpInfoId " +
                    "left join tblDivision div on aa.DivisionId = div.DivisionId  " +
                    "left join tblDepartment dpt on aa.DepartmentId = dpt.DepartmentId " +
                    "left join tblDesignation desg on aa.DesignationId = desg.DesignationId LEFT JOIN dbo.tblUser us ON us.EmpInfoId = A.EmpInfoId " +
                    "  left join tblEmpGeneralInfo EG on us.EmpInfoId = EG.EmpInfoId  " +
                    "left join tblDepartment dptUU on EG.DepartmentId = dptUU.DepartmentId " +
                    "  where Aa.CompanyId = " + com + " and aa.IsActive=1 and  a.IsActive=1 " + param + " " +
                    "group by A.ReportingEmpId, Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName,  dptUU.DepartmentId";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public DataTable GetEmpForDeadlineExtensionRequestApproval(int com, string deadLine, string param)
        {
            try
            {
                string query =
                    @" select A.ReportingEmpId as EmpInfoId , Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName ,count(a.EmpInfoId)TotalEmployee, '" + deadLine + "' as ExtendedDate   from tblEmpGeneralInfo A " +
                    "left join tblEmpGeneralInfo Aa on a.ReportingEmpId = aa.EmpInfoId " +
                    "left join tblDivision div on aa.DivisionId = div.DivisionId  " +
                    "left join tblDepartment dpt on aa.DepartmentId = dpt.DepartmentId " +
                    "left join tblDesignation desg on aa.DesignationId = desg.DesignationId LEFT JOIN dbo.tblUser us ON us.EmpInfoId = A.EmpInfoId  where Aa.CompanyId = " + com + " and aa.IsActive=1 and  a.IsActive=1 " + param + " " +
                    "group by A.ReportingEmpId, Aa.EmpMasterCode , Aa.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName";
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
                string query = @"select * from tblDeadlineExtensionRequest where DeadlineExtensionRequestId = " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetDeadlineExtendedEntryList(string param)
        {

            try
            {
                string query = @"SELECT tblDeadlineExtensionRequest.DeadlineExtensionRequestId ,
       tblCompanyInfo.CompanyName ,
       tblFinancialYear.FinancialYearDesc ,
       tblDepartment.DepartmentName ,
       tblDeadlineExtensionRequest.Operation ,
       tblDeadlineExtensionRequest.ExtensionDate ,
       tblDeadlineExtensionRequest.Description ,
       tblDeadlineExtensionRequest.Remarks ,
       tblUser.UserName AS EntryBy ,
       tblDeadlineExtensionRequest.EntryDate ,
      upby.UserName UpdateBy ,
       tblDeadlineExtensionRequest.UpdateDate
      
       IsDelete FROM tblDeadlineExtensionRequest

	   INNER JOIN dbo.tblCompanyInfo  ON tblCompanyInfo.CompanyId = tblDeadlineExtensionRequest.CompanyId
	   left JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblDeadlineExtensionRequest.DepartmentId
	   INNER JOIN dbo.tblFinancialYear ON tblFinancialYear.FinancialYearId = tblDeadlineExtensionRequest.FinYearId
	   LEFT JOIN dbo.tblUser ON tblUser.UserId = tblDeadlineExtensionRequest.EntryBy
	   	   left JOIN dbo.tblUser upby ON upby.UserId = tblDeadlineExtensionRequest.UpdateBy
	  
		   
		    WHERE
		   ( tblDeadlineExtensionRequest.IsDelete IS NULL
                                      OR tblDeadlineExtensionRequest.IsDelete = 0
                                    )  " + param;
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public DataTable GetDeadlineExtendedEntryListApproved(string param)
        {

            try
            {
                string query = @"SELECT DeadlineExtensionRequestId ,
       tblCompanyInfo.CompanyName ,
       tblFinancialYear.FinancialYearDesc ,
       tblDepartment.DepartmentName ,
       tblDeadlineExtensionRequest.Operation ,
       tblDeadlineExtensionRequest.ExtensionDate ,
       tblDeadlineExtensionRequest.Description ,
       tblDeadlineExtensionRequest.Remarks ,
       tblUser.UserName AS EntryBy ,
       tblDeadlineExtensionRequest.EntryDate ,
      upby.UserName UpdateBy ,
       tblDeadlineExtensionRequest.UpdateDate , 
      
       IsDelete FROM tblDeadlineExtensionRequest

	   INNER JOIN dbo.tblCompanyInfo  ON tblCompanyInfo.CompanyId = tblDeadlineExtensionRequest.CompanyId
	   left JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblDeadlineExtensionRequest.DepartmentId
	   INNER JOIN dbo.tblFinancialYear ON tblFinancialYear.FinancialYearId = tblDeadlineExtensionRequest.FinYearId
	   LEFT JOIN dbo.tblUser ON tblUser.UserId = tblDeadlineExtensionRequest.EntryBy
	   	   left JOIN dbo.tblUser upby ON upby.UserId = tblDeadlineExtensionRequest.UpdateBy WHERE
		   ( tblDeadlineExtensionRequest.IsDelete IS NULL
                                      OR tblDeadlineExtensionRequest.IsDelete = 0
                                    )" + param;
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }



        public DataTable GetFianncialYearByComIdDDl(int id)
        {
            string query = @"SELECT FinancialYearId as Value,FinancialYearDesc as TextField FROM tblFinancialYear where CompanyId =" + id + " and Status ='Active' ";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);



        }


        public int SaveDeadlineExtendedEntry(DeadlineExtendedEntryDAO aMaster, int user)
        {
            try
            {
                int pk = 0;

                if (aMaster.DeadlineExtensionRequestId == 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinYearId", aMaster.FinYearId));
                   // aParameters.Add(new SqlParameter("@DepartmentId", aMaster.));
                    aParameters.Add(new SqlParameter("@Operation", aMaster.Operation));
                    
                    aParameters.Add(new SqlParameter("@Description", aMaster.Description));
                    aParameters.Add(new SqlParameter("@Remarks", aMaster.Remarks));
                    aParameters.Add(new SqlParameter("@IsEmployee", aMaster.IsEmployee ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@IsDepartment", aMaster.IsDepartment ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@DepartmentId", aMaster.DepartmentId ?? (object)DBNull.Value));
               
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                    string query =
                        @"insert into tblDeadlineExtensionRequest (CompanyId, FinYearId, DepartmentId , Operation,   Description, Remarks,  EntryDate, EntryBy, IsEmployee, IsDepartment) 
              values(@CompanyId, @FinYearId, @DepartmentId , @Operation,   @Description, @Remarks,  @EntryDate, @EntryBy, @IsEmployee, @IsDepartment)";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                    return pk;
                }
                else
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@DeadlineExtensionRequestId", aMaster.DeadlineExtensionRequestId));
                    aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                    aParameters.Add(new SqlParameter("@FinYearId", aMaster.FinYearId));
                    // aParameters.Add(new SqlParameter("@DepartmentId", aMaster.));
                    aParameters.Add(new SqlParameter("@Operation", aMaster.Operation));

                    aParameters.Add(new SqlParameter("@Description", aMaster.Description));
                    aParameters.Add(new SqlParameter("@Remarks", aMaster.Remarks));
                    aParameters.Add(new SqlParameter("@IsEmployee", aMaster.IsEmployee ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@IsDepartment", aMaster.IsDepartment ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@DepartmentId", aMaster.DepartmentId ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                    string query = @"update tblDeadlineExtensionRequest set IsDepartment=@IsDepartment, IsEmployee=@IsEmployee, CompanyId=@CompanyId, FinYearId=@FinYearId, DepartmentId=@DepartmentId , Operation=@Operation,  Description=@Description, Remarks=@Remarks, UpdateBy = @UpdateBy , UpdateDate = @UpdateDate where DeadlineExtensionRequestId = @DeadlineExtensionRequestId ";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        pk = aMaster.DeadlineExtensionRequestId;
                    }

                    return pk;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool SaveAppraisalSetupDetails(List<DeadlineExtensionRequestDetailsDAO> aDetails, int master)
        {
            try
            {

                bool result = false;
                string delQ = @"delete from tblDeadlineExtensionRequestDetails where DeadlineExtensionRequestId = " + master + "";

                bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

                foreach (DeadlineExtensionRequestDetailsDAO item in aDetails)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmployeeId", item.EmployeeId));
                    aParameters.Add(new SqlParameter("@ExtensionDate", item.ExtensionDate));
                 
                    aParameters.Add(new SqlParameter("@DeadlineExtensionRequestId", master));

                    string query = @"insert into tblDeadlineExtensionRequestDetails (DeadlineExtensionRequestId, EmployeeId, ExtensionDate, ApprovalStatus ) 
                                                                values(@DeadlineExtensionRequestId, @EmployeeId, @ExtensionDate,'Posted')";

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


        public bool SaveDeadlineExtensionRequestDetailsApproval(List<DeadlineExtensionRequestDetailsDAO> aDetails, List<AppraisalDeadLineDetails> aDetailsAppraisal, List<KPIDeadLineDetails> aKPIDeadLineDetails)
      {
          bool result = false;
          foreach (DeadlineExtensionRequestDetailsDAO item in aDetails)
          {
              List<SqlParameter> aParameters = new List<SqlParameter>();
              aParameters.Add(new SqlParameter("@EmployeeId", item.EmployeeId));
              aParameters.Add(new SqlParameter("@ApprovedBy", item.ApprovedBy));
              aParameters.Add(new SqlParameter("@ApproveDate", item.ApproveDate));


              aParameters.Add(new SqlParameter("@DeadlineExtensionRequestDetailsId", item.DeadlineExtensionRequestDetailsId));

              string query = @"update tblDeadlineExtensionRequestDetails set  EmployeeId=@EmployeeId,  ApprovalStatus='Approved', ApprovedBy=@ApprovedBy,ApproveDate=@ApproveDate where DeadlineExtensionRequestDetailsId=@DeadlineExtensionRequestDetailsId";

              result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

              if (result == false)
              {
                  break;


              }


          }



          foreach (AppraisalDeadLineDetails itemApprisal in aDetailsAppraisal)
          {
              List<SqlParameter> aParameters = new List<SqlParameter>();
       //       aParameters.Add(new SqlParameter("@EmpinfoId", itemApprisal.EmpinfoId));
              aParameters.Add(new SqlParameter("@ExtensionDate", itemApprisal.ExtensionDate));

            //  aParameters.Add(new SqlParameter("@AppraisalDeadLineDetailsId", itemApprisal.AppraisalDeadLineDetailsId));

              string query = @"update tblAppraisalDeadLineDetails
set ExtensionDate =@ExtensionDate
 FROM 
   dbo.tblAppraisalDeadlineMaster as gm
  INNER join dbo.tblCompanyInfo as com ON  gm.CompanyId=com.CompanyId 
  INNER join dbo.tblFinancialYear as FinY ON  gm.FinancialYearId=FinY.FinancialYearId  
  INNER join dbo.tblAppraisalDeadLineDetails as ddddd ON  gm.AppraisalDeadLineMasterId=ddddd.AppraisalDeadLineMasterId  

   
  
  left join dbo.tblDeadlineExtensionRequest as Req ON  Req.CompanyId=com.CompanyId 
  left join dbo.tblDeadlineExtensionRequestDetails as DeReq ON  Req.DeadlineExtensionRequestId=DeReq.DeadlineExtensionRequestId 
  and  req.FinYearId=FinY.FinancialYearId 
   
where
   (gm.CompanyId =Req.CompanyId AND gm.FinancialYearId =req.FinYearId AND ddddd.EmpinfoId=DeReq.EmployeeId
    )
";

              result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

              if (result == false)
              {
                  break;


              }


          }



          foreach (KPIDeadLineDetails itemKPI in aKPIDeadLineDetails)
          {
              List<SqlParameter> aParameters = new List<SqlParameter>();
              //       aParameters.Add(new SqlParameter("@EmpinfoId", itemApprisal.EmpinfoId));
              aParameters.Add(new SqlParameter("@ExtensionDate", itemKPI.ExtensionDate));

              //  aParameters.Add(new SqlParameter("@AppraisalDeadLineDetailsId", itemApprisal.AppraisalDeadLineDetailsId));

              string query = @"update tblKPIDeadLineDetails
set ExtensionDate =@ExtensionDate
 FROM 
   dbo.tblKpiDeadlineMaster as gm
  INNER join dbo.tblCompanyInfo as com ON  gm.CompanyId=com.CompanyId 
  INNER join dbo.tblFinancialYear as FinY ON  gm.FinancialYearId=FinY.FinancialYearId  
  INNER join dbo.tblKPIDeadLineDetails as ddddd ON  gm.KPIDeadLineMasterId=ddddd.KPIDeadLineMasterId  

   
  
  left join dbo.tblDeadlineExtensionRequest as Req ON  Req.CompanyId=com.CompanyId 
  left join dbo.tblDeadlineExtensionRequestDetails as DeReq ON  Req.DeadlineExtensionRequestId=DeReq.DeadlineExtensionRequestId 
  and  req.FinYearId=FinY.FinancialYearId 
   
where
   (gm.CompanyId =Req.CompanyId AND gm.FinancialYearId =req.FinYearId AND ddddd.EmpinfoId=DeReq.EmployeeId
    )";

              result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

              if (result == false)
              {
                  break;


              }


          }
          return result;
        


        

      }

    }
}
