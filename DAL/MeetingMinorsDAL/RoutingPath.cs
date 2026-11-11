using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.MeetingMinorsDAO;

namespace DAL.MeetingMinorsDAL
{
    public class RoutingPath
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetDDLCompany()
        {
            string query = @"SELECT com.CompanyId AS Value, com.ShortName AS TextField  FROM dbo.tblCompanyInfo com WITH (NOLOCK)";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetUser(int Id)
        {
            string query = @"SELECT us.UserName+ ISNULL(' : '+emp.EmpName,'') UserName , us.UserId as Value FROM tblUser us WITH (NOLOCK)
                                 left JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = us.EmpInfoId
                                         WHERE us.IsActive=1 AND emp.CompanyId=" + Id;
            return aCommonInternalDal.GetDTforDDL(query,null,DataBase.HRDB);
        }

        public DataTable GetDivisionBycompanyId(int id)
        {
            string query = @"SELECT com.DivisionId AS Value, com.DivisionName AS TextField  FROM dbo.tblDivision com WITH (NOLOCK) where com.CompanyId="+id+"  ";
            return aCommonInternalDal.GetDTforDDL(query, null,DataBase.HRDB);
        }


        public void GetDepartmentByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

                string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
                LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
                LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
                WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
                aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetDepartmentByDiv( int divisionId)
        {
            //var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
                LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
                LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
                WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId ="+divisionId+" AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetEmployee( string param)
        {
            try
            {
                string query = @"select A.EmpInfoId, A.EmpMasterCode , A.EmpName , desg.Designation  from tblEmpGeneralInfo A 
                               left join tblDivision div on a.DivisionId = div.DivisionId 
                               left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId 
                               left join tblDesignation desg on a.DesignationId = desg.DesignationId where a.IsActive=1 "+param+" ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetRoutingpathView( string param )
        {
            string query = @"SELECT  usEmp.EmpName CreateBy, usUp.UserName UpdateBy, MRPM.RoutingPathMaster_ID, MRPM.RoutingPath_Code,MRPM.RoutingPath_Name, Com.CompanyName, Div.DivisionName,
							 DPT.DepartmentName, MRPM.CreateDate,* from tblMeeting_RoutingPathSetupMaster MRPM   WITH (NOLOCK) 
                             LEFT JOIN  tblCompanyInfo Com ON Com.CompanyId = MRPM.CompanyId
                             LEFT JOIN tblDivision Div ON  Div.DivisionId = MRPM.DivisionId
                             LEFT JOIN tblDepartment DPT ON DPT.DepartmentId = MRPM.DepartmentId    
							 left JOIN  dbo.tblUser us   ON  MRPM.CreateBy =us.UserId  
							 left JOIN  dbo.tblEmpGeneralInfo usEmp   ON  us.EmpInfoId =usEmp.EmpInfoId  

left JOIN  dbo.tblUser usUp   ON  MRPM.UpdateBy =usUp.UserId                       
                             Where MRPM.RoutingPathMaster_ID IS NOT NULL " + param + " ORDER  BY MRPM.CreateDate desc ";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


        public Int32 saveMasterPath(RoutingPathSetupMaster aMaster)
        {
            //Int32 ID = 0;
            ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();
            aMaster.RoutingPathMaster_ID = aClsPrimaryKeyFind.PrimaryKeyMax("RoutingPathMaster_ID", "tblMeeting_RoutingPathSetupMaster", "HRDB");
            aMaster.RoutingPath_Code = CodeGenerator(aMaster.RoutingPathMaster_ID);
            SaveMaster(aMaster);
            return aMaster.RoutingPathMaster_ID;
        }

        public string CodeGenerator(int id)
        {
            string code = string.Empty;
            string Id = id.ToString();
            if (Id.Length == 1)
            {
                Id = "000" + Id;
            }
            if (Id.Length == 2)
            {
                Id = "00" + Id;
            }
            if (Id.Length == 3)
            {
                Id = "0" + Id;
            }
            code = "RPS-" + Id;
            return code;
        }

    
        public int SaveMaster( RoutingPathSetupMaster aPathSetupMaster)
        {
            var aSqlParameterlist = new List<SqlParameter>();
          //  aSqlParameterlist.Add(new SqlParameter("@RoutingPathMaster_ID", aPathSetupMaster.RoutingPathMaster_ID));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aPathSetupMaster.CompanyId));
       //     aSqlParameterlist.Add(new SqlParameter("@RoutingPath_Code", aPathSetupMaster.RoutingPath_Code));
            aSqlParameterlist.Add(new SqlParameter("@RoutingPath_Name", aPathSetupMaster.RoutingPath_Name));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aPathSetupMaster.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aPathSetupMaster.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aPathSetupMaster.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aPathSetupMaster.CreateDate));
            string queryStr = @"INSERT INTO tblMeeting_RoutingPathSetupMaster (RoutingPath_Code,RoutingPath_Name,CompanyId,DivisionId,DepartmentId,CreateBy,CreateDate) VALUES ((SELECT 'RP_'+ CAST(ISNULL(MAX(ISNULL(RoutingPathMaster_ID,0)),0)+1001 AS NVARCHAR(MAX)) FROM tblMeeting_RoutingPathSetupMaster WITH(NOLOCK)),@RoutingPath_Name,@CompanyId,@DivisionId,@DepartmentId,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public bool UpdateMaster(RoutingPathSetupMaster aPathSetupMaster)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RoutingPathMaster_ID", aPathSetupMaster.RoutingPathMaster_ID));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aPathSetupMaster.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@RoutingPath_Name", aPathSetupMaster.RoutingPath_Name));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aPathSetupMaster.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aPathSetupMaster.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aPathSetupMaster.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aPathSetupMaster.UpdateDate));
            string queryStr = @"UPDATE tblMeeting_RoutingPathSetupMaster SET RoutingPath_Name=@RoutingPath_Name,CompanyId=@CompanyId,DivisionId=@DivisionId,DepartmentId=@DepartmentId,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate where RoutingPathMaster_ID=@RoutingPathMaster_ID";
            return aCommonInternalDal.SaveDataByInsertCommand(queryStr, aSqlParameterlist, "HRDB");
        }




        public Int32 SaveDataForDetails(List<RoutingPathSetupDetails> aMemberSetupDetailsList)
        {
            Int32 ID = 0;
            foreach (RoutingPathSetupDetails aMemberSetupDetails in aMemberSetupDetailsList)
            {
                ID = SaveDetails(aMemberSetupDetails);
            }
            return ID;
        }

        public bool UpdateDetails(List<RoutingPathSetupDetails> aMemberSetupDetailsList)
        {
            bool ID = false;
            foreach (RoutingPathSetupDetails aMemberSetupDetails in aMemberSetupDetailsList)
            {
                ID = UpdateDetails(aMemberSetupDetails);
            }
            return ID;
        }

        public bool UpdateDetails(RoutingPathSetupDetails aPathSetupDetails)
        {
            var aSqlParameterlist = new List<SqlParameter>();
           
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aPathSetupDetails.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@Seq_No", aPathSetupDetails.Seq_No));
            string queryStr = @"UPDATE tblMeeting_RoutingPathSetupDetails SET EmpInfoId=@EmpInfoId,Seq_No=@Seq_No WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDetails(RoutingPathSetupDetails aPathSetupDetails)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RoutingPathMaster_ID", aPathSetupDetails.RoutingPathMaster_ID));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aPathSetupDetails.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@Seq_No", aPathSetupDetails.Seq_No));
            string queryStr = @"INSERT INTO tblMeeting_RoutingPathSetupDetails (RoutingPathMaster_ID,EmpInfoId,Seq_No) VALUES (@RoutingPathMaster_ID,@EmpInfoId,@Seq_No)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool Save_DeleteMaster(string Id)
        {
            string query = @"INSERT INTO tblMeeting_RoutingPathSetupMaster_DEL SELECT * FROM tblMeeting_RoutingPathSetupMaster WHERE  RoutingPathMaster_ID=" + Id;

            return aCommonInternalDal.SaveDataByInsertCommand(query, "HRDB");
        }

        public int Del_SaveMaster(RoutingPathSetupMaster aPathSetupMaster, int deleteBy)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RoutingPathMaster_ID", aPathSetupMaster.RoutingPathMaster_ID));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aPathSetupMaster.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@RoutingPath_Code", aPathSetupMaster.RoutingPath_Code));
            aSqlParameterlist.Add(new SqlParameter("@RoutingPath_Name", aPathSetupMaster.RoutingPath_Name));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aPathSetupMaster.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aPathSetupMaster.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aPathSetupMaster.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aPathSetupMaster.CreateDate));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aPathSetupMaster.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aPathSetupMaster.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", deleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", DateTime.Now));
            string queryStr = @"INSERT INTO tblMeeting_RoutingPathSetupMaster_DEL (RoutingPathMaster_ID,RoutingPath_Code,RoutingPath_Name,CompanyId,DivisionId,DepartmentId,CreateBy,CreateDate,UpdateBy,UpdateDate,DeleteBy,DeleteDate) VALUES (@RoutingPathMaster_ID,@RoutingPath_Code,@RoutingPath_Name,@CompanyId,@DivisionId,@DepartmentId,@CreateBy,@CreateDate,@UpdateBy,@UpdateDate,@DeleteBy,@DeleteDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public bool DeleteMaster(string Id)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RoutingPathMaster_ID", Id));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", HttpContext.Current.Session["UserId"]));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", DateTime.Now));
            string query = @"
 
 
 
    
 
 INSERT INTO tblMeeting_RoutingPathSetupMaster_DEL ([RoutingPathMaster_ID]
           ,[RoutingPath_Code]
           ,[RoutingPath_Name]
           ,[CompanyId]
           ,[DivisionId]
           ,[DepartmentId]
           ,[CreateBy]
           ,[CreateDate]
           ,[UpdateBy]
           ,[UpdateDate]
           ,[DeleteBy]
           ,[DeleteDate])
SELECT [RoutingPathMaster_ID]
           ,[RoutingPath_Code]
           ,[RoutingPath_Name]
           ,[CompanyId]
           ,[DivisionId]
           ,[DepartmentId]
           ,[CreateBy]
           ,[CreateDate]
           ,[UpdateBy]
           ,[UpdateDate]
           ,@DeleteBy 
           ,@DeleteDate 
FROM tblMeeting_RoutingPathSetupMaster
WHERE [RoutingPathMaster_ID] =@RoutingPathMaster_ID

DELETE FROM [dbo].[tblMeeting_RoutingPathSetupMaster] 
WHERE [RoutingPathMaster_ID] =@RoutingPathMaster_ID
";

            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }


        public bool DeleteDetails(string Id)
        {
            string query = @"Delete from tblMeeting_RoutingPathSetupDetails where RoutingPathMaster_ID="+Id;

            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }


        public DataTable GetMaster(int Id)
        {
            string query = @"Select * from tblMeeting_RoutingPathSetupMaster where RoutingPathMaster_ID="+Id;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable CheckRoutingPath(string RoutingPathName, string DepartmentId)
        {
            string query = @"SELECT * FROM dbo.tblMeeting_RoutingPathSetupMaster  with(nolock)
	WHERE UPPER(RoutingPath_Name)='" + RoutingPathName + "' and   DepartmentId=" + DepartmentId;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable CheckRoutingPathEdit(string RoutingPathName, string DepartmentId, string PathId)
        {
            string query = @"SELECT * FROM dbo.tblMeeting_RoutingPathSetupMaster  with(nolock)
	WHERE UPPER(RoutingPath_Name)='" + RoutingPathName + "' and   DepartmentId=" + DepartmentId + " and  RoutingPathMaster_ID not in ('" + PathId + "')";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetDetails(int Id)
        {
            string query = @"Select MRPD.RoutingPathDetail_ID,MRPD.RoutingPathDetail_ID,A.EmpInfoId, A.EmpMasterCode , A.EmpName , desg.Designation, MRPD.Seq_No from  tblMeeting_RoutingPathSetupDetails MRPD
                             LEFT JOIN tblEmpGeneralInfo A On A.EmpInfoId = MRPD.EmpInfoId
                             LEFT JOIN tblDesignation desg on a.DesignationId = desg.DesignationId
                             Where MRPD.RoutingPathMaster_ID=" + Id;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
    }
}