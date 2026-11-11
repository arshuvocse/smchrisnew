using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Providers.Entities;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;

namespace DAL.MeetingMinorsDAL
{
   public class MeetingMinors
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetCheckPartInfo( string ComID)
        {
            string query = @"SELECT * FROM dbo.tblMeeting_BoardMemberSetupMaster  with(nolock)
     where     CompanyId=" + ComID;
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }


        public DataTable GetCheckPartInfo2(string ComID, string CatId, string MasterdId)
        {
            string query = @"SELECT * FROM dbo.tblMeeting_BoardMemberSetupMaster  with(nolock)
     where   CategoryID =" + CatId + " AND CompanyId=" + ComID + "  AND BMemberSetupMasterID not in ("+MasterdId+")";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLMemberName()
        {
            string query = @"SELECT en.MemberTypeId AS Value, en.MemberTypeName AS TextField  FROM dbo.tblMeeting_MemberType en WITH (NOLOCK) ORDER BY en.MemberTypeName ASC";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLCompany()
        {
            string query = @"SELECT com.CompanyId AS Value, com.ShortName AS TextField  FROM dbo.tblCompanyInfo com WITH (NOLOCK) ";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetDDLmemberType()
        {
            string query = @"SELECT com.MeetingMemberTypeId AS Value, com.MemberType AS TextField  FROM dbo.tblMeeting_MemberType com WITH (NOLOCK) ORDER BY com.MemberType ";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }


        public DataTable GetUser(int Id)
        {
            string query = @"SELECT us.UserName+ ISNULL(' : '+emp.EmpName,'') UserName , us.UserId as Value FROM tblUser us WITH (NOLOCK)
                             left JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = us.EmpInfoId
                             WHERE us.IsActive=1 AND emp.CompanyId=" + Id;
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }


        public DataTable GetDDLCompanyByID(Int32 Id)
        {
            string query = @" SELECT com.CompanyId AS Value, com.CompanyName AS TextField  FROM dbo.tblCompanyInfo com WITH (NOLOCK) where com.CompanyId="+Id;
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }



        public Int32 SaveDataForMemberSetupMaster(MemberSetupMaster aMaster)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@BoardMemberName", aMaster.BoardMemberName));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aMaster.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@StartDate", aMaster.StartDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EndDate", aMaster.EndDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Description", aMaster.Description));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aMaster.CreateDate));
            aSqlParameterlist.Add(new SqlParameter("@isActive", aMaster.isActive));
            const string insertQuery = @"INSERT INTO dbo.tblMeeting_BoardMemberSetupMaster(CompanyId, BoardMemberName, Description,CreateBy,CreateDate,StartDate,EndDate,isActive) VALUES (@CompanyId, @BoardMemberName, @Description,@CreateBy,@CreateDate,@StartDate,@EndDate,@isActive)";
            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDataForMemberSetupMasterDelete(MemberSetupMaster aMaster, int Deleteby)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupMasterID", aMaster.BMemberSetupMasterID));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@Description", aMaster.Description));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aMaster.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aMaster.CreateDate));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", Deleteby));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", DateTime.Now));
            const string insertQuery = @"INSERT INTO dbo.tblMeeting_BoardMemberSetupMaster_DEL(BMemberSetupMasterID,CompanyId,Description,CreateBy,CreateDate,DeleteBy,DeleteDate) VALUES (@BMemberSetupMasterID,@CompanyId,@Description,@CreateBy,@CreateDate,@DeleteBy,@DeleteDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }


        public bool UpdateDataForMemberSetupMaster(MemberSetupMaster aMaster)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupMasterID", aMaster.BMemberSetupMasterID));
            aSqlParameterlist.Add(new SqlParameter("@CategoryID", aMaster.CategoryID));

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aMaster.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aMaster.UpdateDate));
            const string UpdateQuery = @"Update dbo.tblMeeting_BoardMemberSetupMaster SET CategoryID=@CategoryID, CompanyId=@CompanyId,UpdateBy=@UpdateBy, UpdateDate=@UpdateDate Where BMemberSetupMasterID=@BMemberSetupMasterID";
            return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDataForDetails(List<MemberSetupDetails> aMemberSetupDetailsList)
        {
            Int32 ID = 0;
            foreach (MemberSetupDetails aMemberSetupDetails in aMemberSetupDetailsList)
            {
                ID = SaveDataForMemberSetupDetails(aMemberSetupDetails);
            }
            return ID;
        }


       

        public bool UpdateDataForMemberSetupDetails(MemberSetupDetails aDetails, string masterId)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupDetailsID", masterId));
             
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupMasterID", aDetails.BMemberSetupMasterID));
            aSqlParameterlist.Add(new SqlParameter("@MeetingMemberTypeId", aDetails.MeetingMemberTypeId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDetails.CompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MemberType", aDetails.MemberType));
            aSqlParameterlist.Add(new SqlParameter("@Name", aDetails.Name ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Address", aDetails.Address ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MobileNo", aDetails.MobileNo));
            aSqlParameterlist.Add(new SqlParameter("@Email", aDetails.Email ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDetails.JoiningDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MembershipDate", aDetails.MembershipDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"]));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", DateTime.Now));
            aSqlParameterlist.Add(new SqlParameter("@Note", aDetails.Note ?? (object)DBNull.Value));
            string insertQuery = @"UPDATE [dbo].[tblMeeting_BoardMemberSetupDetails]
   SET [BMemberSetupMasterID] = @BMemberSetupMasterID 
      ,[Name] = @NAME 
      ,[Address] = @ADDRESS 
      ,[MobileNo] = @MobileNo 
      ,[Email] = @Email 
      ,[MembershipDate] = @MembershipDate 
      ,[Note] = @Note 
      ,[MemberType] = @MemberType 
      ,[MeetingMemberTypeId] = @MeetingMemberTypeId 
      ,[JoiningDate] = @JoiningDate 
      ,[CompanyId] = @CompanyId 
       ,[UpdateBy] = @UpdateBy 
      ,[UpdateDate] = @UpdateDate 
 WHERE  BMemberSetupDetailsID=@BMemberSetupDetailsID";
            return aCommonInternalDal.UpdateDataByUpdateCommand(insertQuery, aSqlParameterlist, "HRDB");
        }


        public bool UpdateData(MemberSetupMaster aDetails, string masterId)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupMasterID", masterId));


            aSqlParameterlist.Add(new SqlParameter("@StartDate", aDetails.StartDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EndDate", aDetails.EndDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDetails.CompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Description", aDetails.Description ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BoardMemberName", aDetails.BoardMemberName ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@isActive", aDetails.isActive));

            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"]));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", DateTime.Now));
          
            string insertQuery = @"UPDATE [dbo].[tblMeeting_BoardMemberSetupMaster]
   SET [CompanyId] = @CompanyId,  
      [Description] = @DESCRIPTION 
      ,[BoardMemberName] = @BoardMemberName , StartDate=@StartDate,EndDate=@EndDate ,isActive=@isActive
 WHERE BMemberSetupMasterID=@BMemberSetupMasterID";
            return aCommonInternalDal.UpdateDataByUpdateCommand(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDataForMemberSetupDetails(MemberSetupDetails aDetails)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupMasterID", aDetails.BMemberSetupMasterID));
            aSqlParameterlist.Add(new SqlParameter("@MeetingMemberTypeId", aDetails.MeetingMemberTypeId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDetails.CompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MemberType", aDetails.MemberType));
            aSqlParameterlist.Add(new SqlParameter("@Name", aDetails.Name ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Address", aDetails.Address ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MobileNo", aDetails.MobileNo));
            aSqlParameterlist.Add(new SqlParameter("@Email", aDetails.Email ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDetails.JoiningDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MembershipDate", aDetails.MembershipDate ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", HttpContext.Current.Session["UserId"]));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate",DateTime.Now)  );
            aSqlParameterlist.Add(new SqlParameter("@Note", aDetails.Note ?? (object)DBNull.Value));

            const string insertQuery = @"INSERT INTO dbo.tblMeeting_BoardMemberSetupDetails(BMemberSetupMasterID,MemberType,Name,Address,MobileNo,Email,MembershipDate,Note,MeetingMemberTypeId,JoiningDate,CompanyId,CreateBy,CreateDate) VALUES (@BMemberSetupMasterID,@MemberType,@Name,@Address,@MobileNo,@Email,@MembershipDate,@Note,@MeetingMemberTypeId,@JoiningDate,@CompanyId,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public bool DeleteById(int MasterId, string ID, string DroList)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SubcommitteeMasterId", MasterId));
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupDetailsID", ID));
            aSqlParameterlist.Add(new SqlParameter("@MemberSetupDetailsID", DroList));





            const string query = @"INSERT INTO [dbo].[tblMeeting_BoardMemberSetupDetails]
           ([BMemberSetupMasterID],
           MemberSetupDetailsID
           ,[Name]
           ,[Address]
           ,[MobileNo]
           ,[Email]
           ,[MembershipDate]
           ,[Note]
           ,[MemberType]
           ,[MeetingMemberTypeId]
           ,[JoiningDate]
           ,[CompanyId],MemberTypeInfoId)
		   SELECT @SubcommitteeMasterId,MemberSetupDetailsID
        ,[Name]
      ,[Address]
      ,[MobileNo]
      ,[Email]
      ,[MembershipDate]
      ,[Note]
      ,[MemberType]
      ,[MeetingMemberTypeId]
      ,[JoiningDate]
      ,[CompanyId] ,@MemberSetupDetailsID
  FROM [dbo].[tblMeeting_MemberSetupDetails] WHERE  MemberSetupDetailsID IN ( @BMemberSetupDetailsID)





";


            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }

        public bool DeleteByDetrailsId(int MasterId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupMasterID", MasterId));






            const string query = @"


DELETE FROM [dbo].[tblMeeting_BoardMemberSetupDetails]
      WHERE BMemberSetupMasterID=@BMemberSetupMasterID
 ";


            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }


        public DataTable GetforEdit2(Int32 Id)
        {
            string query = @"SELECT  MemberSetupDetailsID,MemberTypeInfoId FROM tblMeeting_BoardMemberSetupDetails
WITH (NOLOCK) WHERE BMemberSetupMasterID= " + Id;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetforEdit(Int32 Id)
        {
            string query = @"SELECT FORMAT(StartDate,'dd-MMM-yyyy')	 StartDate,
 FORMAT(EndDate,'dd-MMM-yyyy') EndDate,	* FROM dbo.tblMeeting_BoardMemberSetupMaster WHERE BMemberSetupMasterID=" + Id;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable loadMember(string param)
        {
            string query = @" SELECT  case when BMSM.isActive=1 then 'Active' else 'Inactive' end ActiveStatus, us.UserName as CreateBy, usUp.UserName as UpdateBy, com.ShortName,  * from tblMeeting_BoardMemberSetupMaster BMSM     WITH (NOLOCK)    
                                 left JOIN  dbo.tblUser us   ON  BMSM.CreateBy =us.UserId  
              left JOIN  dbo.tblUser usUp   ON  BMSM.UpdateBy =usUp.UserId
 
left JOIN  dbo.tblCompanyInfo com   ON  BMSM.CompanyId =com.CompanyId
							  where  BMSM.BMemberSetupMasterID IS NOT NUll  " + param + " ORDER BY BMSM.CreateDate desc ";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public bool deleteDetails(string id)
        {
            string query = @"Delete from tblMeeting_BoardMemberSetupDetails where BMemberSetupMasterID="+id;
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }


        public bool DeleteMaster(string id)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BMemberSetupMasterID", id));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", HttpContext.Current.Session["UserId"]));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", DateTime.Now));
            string query = @"


INSERT INTO [dbo].[tblMeeting_BoardMemberSetupMaster_DEL]
           ([BMemberSetupMasterID]
           ,[CompanyId]
           ,[Description]
           ,[CreateBy]
           ,[CreateDate]
           ,[UpdateBy]
           ,[UpdateDate]
           ,[DeleteBy]
           ,[DeleteDate],BoardMemberName)
SELECT [BMemberSetupMasterID]
           ,[CompanyId]
           ,[Description]
           ,[CreateBy]
           ,[CreateDate]
           ,[UpdateBy]
           ,[UpdateDate]
           ,@DeleteBy 
           ,@DeleteDate,BoardMemberName 
FROM tblMeeting_BoardMemberSetupMaster
WHERE BMemberSetupMasterID =@BMemberSetupMasterID

Delete from tblMeeting_BoardMemberSetupMaster where BMemberSetupMasterID=@BMemberSetupMasterID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query,aSqlParameterlist, "HRDB");
        }


      
    }
}
