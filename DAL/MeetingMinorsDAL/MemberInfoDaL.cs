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
    public class MemberInformationSearchCriteria
    {
        public int? CompanyId { get; set; }
        public int? MemberTypeId { get; set; }
    }

    public class MemberInfoDaL
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();




        public DataTable CheckBoardMeeting(string param)
        {
            string query = @"SELECT * FROM dbo.tblMeeting_BoardMemberSetupDetails WHERE  MemberSetupDetailsID=" + param;

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable CheckSubcommittee(string param)
        {
            string query = @"SELECT * FROM tblMeeting_SubcommitteeMasterDetails  WHERE  BMemberSetupDetailsID=
" + param;

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetCheckPartInfo( string ComID, string CatId)
        {
            string query = @"SELECT * FROM dbo.tblMeeting_BoardMemberSetupMaster  with(nolock)
     where   CategoryID =" + CatId + " AND CompanyId=" + ComID;
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
        public DataTable GetDDLMemberType()
        {
            string query = @"SELECT en.MemberTypeId AS Value, en.MemberType AS TextField  FROM dbo.tblMemberType_BM en WITH (NOLOCK) ORDER BY en.MemberType ASC";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLMemberPostion()
        {
            string query = @"SELECT en.PostionStatusId AS Value, en.PositionName AS TextField  FROM dbo.tblPosition_BM en WITH (NOLOCK) ORDER BY en.PositionName ASC";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLCompany()
        {
            string query = @"SELECT com.CompanyId AS Value, com.ShortName AS TextField
                             FROM dbo.tblCompanyInfo com WITH (NOLOCK)
                             WHERE com.CompanyId IN
                             (
                                 SELECT CompanyId
                                 FROM dbo.tblUserCompanyMaping
                                 WHERE IsActive = 1
                                   AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + @"'
                             )";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetAllCompaniesForRadioButton()
        {
            string query = @"SELECT CompanyId AS Value, ShortName AS TextField FROM tblCompanyInfo WITH (NOLOCK)";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
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
            aSqlParameterlist.Add(new SqlParameter("@CategoryID", aMaster.CategoryID));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aMaster.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@Description", aMaster.Description));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aMaster.CreateDate));
            const string insertQuery = @"INSERT INTO dbo.tblMeeting_BoardMemberSetupMaster(CompanyId, CategoryID, Description,CreateBy,CreateDate) VALUES (@CompanyId, @CategoryID, @Description,@CreateBy,@CreateDate)";
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

        public Int32 SaveDataForDetails(List<MemberDetailsInfoNewDAO> aMemberSetupDetailsList)
        {
            Int32 ID = 0;
            foreach (MemberDetailsInfoNewDAO aMemberSetupDetails in aMemberSetupDetailsList)
            {
                ID = SaveDataForMemberSetupDetails(aMemberSetupDetails);
            }
            return ID;
        }


       

        public bool UpdateDataForMemberSetupDetails(MemberDetailsInfoNewDAO aDetails, string masterId)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemberSetupDetailsID", masterId));

            aSqlParameterlist.Add(new SqlParameter("@MemberSetupMasterID", aDetails.MemberSetupMasterID));
            aSqlParameterlist.Add(new SqlParameter("@MeetingMemberTypeId", aDetails.MeetingMemberTypeId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDetails.CompanyId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OrderNo", aDetails.OrderNo ?? (object)DBNull.Value));
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
            string insertQuery = @"UPDATE [dbo].[tblMeeting_MemberSetupDetails]
   SET [MemberSetupMasterID] = @MemberSetupMasterID 
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
      ,[UpdateDate] = @UpdateDate, OrderNo=@OrderNo 
 WHERE  MemberSetupDetailsID=@MemberSetupDetailsID










  UPDATE Per
SET 

  
           
    Per.Name=Addr.Name, 
    Per.Address=Addr.Address, 
    Per.MobileNo=Addr.MobileNo, 
    Per.Email=Addr.Email, 
    Per.MembershipDate=Addr.MembershipDate, 
    Per.MemberType=Addr.MemberType, 
    Per.MeetingMemberTypeId=Addr.MeetingMemberTypeId, 
    Per.JoiningDate=Addr.JoiningDate, 
    Per.CompanyId=Addr.CompanyId, 
    Per.Note=Addr.Note
FROM tblMeeting_BoardMemberSetupDetails Per
INNER JOIN
tblMeeting_MemberSetupDetails Addr
ON Per.MemberSetupDetailsID = Addr.MemberSetupDetailsID  WHERE Per.MemberSetupDetailsID=@MemberSetupDetailsID




UPDATE Per
SET 

  
           
    Per.Name=Addr.Name, 
    Per.Address=Addr.Address, 
    Per.MobileNo=Addr.MobileNo, 
    Per.Email=Addr.Email, 
    Per.MembershipDate=Addr.MembershipDate, 
    Per.MemberType=Addr.MemberType, 
    Per.MeetingMemberTypeId=Addr.MeetingMemberTypeId, 
    Per.JoiningDate=Addr.JoiningDate, 
    Per.CompanyId=Addr.CompanyId, 
    Per.Note=Addr.Note
FROM dbo.tblMeeting_SubcommitteeMasterDetails Per
INNER JOIN
tblMeeting_MemberSetupDetails Addr
ON Per.BMemberSetupDetailsID = Addr.MemberSetupDetailsID  WHERE Per.BMemberSetupDetailsID=@MemberSetupDetailsID



";
            return aCommonInternalDal.UpdateDataByUpdateCommand(insertQuery, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveDataForMemberSetupDetails(MemberDetailsInfoNewDAO aDetails)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemberSetupMasterID", aDetails.MemberSetupMasterID));
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
            aSqlParameterlist.Add(new SqlParameter("@OrderNo", aDetails.OrderNo ?? (object)DBNull.Value));

            const string insertQuery = @"INSERT INTO dbo.tblMeeting_MemberSetupDetails(MemberSetupMasterID,MemberType,Name,Address,MobileNo,Email,MembershipDate,Note,MeetingMemberTypeId,JoiningDate,CompanyId,CreateBy,CreateDate,OrderNo) VALUES (@MemberSetupMasterID,@MemberType,@Name,@Address,@MobileNo,@Email,@MembershipDate,@Note,@MeetingMemberTypeId,@JoiningDate,@CompanyId,@CreateBy,@CreateDate,@OrderNo)";
            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }


        public DataTable GetforEdit(Int32 Id)
        {
            string query = @"Select com.ShortName Company, BMSM.CompanyId CompanyId, FORMAT(BMSM.MembershipDate,'dd-MMM-yyyy') MembershipDate, FORMAT(BMSM.JoiningDate,'dd-MMM-yyyy') JoiningDate,  * from tblMeeting_MemberSetupDetails BMSM     WITH (NOLOCK)    
                               
							   left JOIN  dbo.tblUser us   ON  BMSM.CreateBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  BMSM.UpdateBy =usUp.UserId
left JOIN  dbo.tblCompanyInfo com   ON  BMSM.CompanyId =com.CompanyId
							  where     BMSM.MemberSetupDetailsID=" + Id;
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable loadMember(string param)
        {
            string query = @"Select  us.UserName as CreateBy, usUp.UserName as UpdateBy,com.ShortName,  * from tblMeeting_MemberSetupDetails BMSM     WITH (NOLOCK)    
                               
							 left JOIN  dbo.tblUser us   ON  BMSM.CreateBy =us.UserId  
              left JOIN  dbo.tblUser usUp   ON  BMSM.UpdateBy =usUp.UserId
left JOIN  dbo.tblCompanyInfo com   ON  BMSM.CompanyId =com.CompanyId
							  where  BMSM.MemberSetupDetailsID IS NOT NUll " + param + " ORDER BY BMSM.OrderNo ASC  ";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable loadMember(MemberInformationSearchCriteria criteria)
        {
            const string query = @"SELECT us.UserName AS CreateBy,
       usUp.UserName AS UpdateBy,
       com.ShortName,
       BMSM.*
FROM dbo.tblMeeting_MemberSetupDetails BMSM WITH (NOLOCK)
LEFT JOIN dbo.tblUser us ON BMSM.CreateBy = us.UserId
LEFT JOIN dbo.tblUser usUp ON BMSM.UpdateBy = usUp.UserId
LEFT JOIN dbo.tblCompanyInfo com ON BMSM.CompanyId = com.CompanyId
WHERE BMSM.MemberSetupDetailsID IS NOT NULL
  AND (@CompanyId IS NULL OR BMSM.CompanyId = @CompanyId)
  AND (@MemberTypeId IS NULL OR BMSM.MeetingMemberTypeId = @MemberTypeId)
ORDER BY BMSM.OrderNo ASC";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@CompanyId", SqlDbType.Int)
                {
                    Value = (object)criteria.CompanyId ?? DBNull.Value
                },
                new SqlParameter("@MemberTypeId", SqlDbType.Int)
                {
                    Value = (object)criteria.MemberTypeId ?? DBNull.Value
                }
            };

            return aCommonInternalDal.DataContainerDataTable(query, parameters, DataBase.HRDB);
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
 
INSERT INTO [dbo].[tblMeeting_MemberSetupDetailsDEL]
([MemberSetupDetailsID]
      ,[MemberSetupMasterID]
      ,[Name]
      ,[Address]
      ,[MobileNo]
      ,[Email]
      ,[MembershipDate]
      ,[Note]
      ,[MemberType]
      ,[MeetingMemberTypeId]
      ,[JoiningDate]
      ,[CompanyId]
      ,[CreateBy]
      ,[CreateDate]
      ,[UpdateBy]
      ,[UpdateDate],[DeleteBy]
           ,[DeleteDate])
           
SELECT [MemberSetupDetailsID]
      ,[MemberSetupMasterID]
      ,[Name]
      ,[Address]
      ,[MobileNo]
      ,[Email]
      ,[MembershipDate]
      ,[Note]
      ,[MemberType]
      ,[MeetingMemberTypeId]
      ,[JoiningDate]
      ,[CompanyId]
      ,[CreateBy]
      ,[CreateDate]
      ,[UpdateBy]
      ,[UpdateDate]
           ,@DeleteBy 
           ,@DeleteDate
FROM tblMeeting_MemberSetupDetails
WHERE MemberSetupDetailsID =@BMemberSetupMasterID
Delete from tblMeeting_MemberSetupDetails where MemberSetupDetailsID=@BMemberSetupMasterID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query,aSqlParameterlist, "HRDB");
        }


      
    }
}
