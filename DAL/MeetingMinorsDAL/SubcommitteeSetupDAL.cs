using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.MeetingMinorsDAO;

namespace DAL.MeetingMinorsDAL
{
  public  class SubcommitteeSetupDAL
    {

      ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
      public DataTable GetMemberListDataById(string ID)
      {
          try
          {
              string query = @"		SELECT 0 Position, 0 BMemberSetupDetailsID,  Type,0 IsBoardMember,  0 NotificationEmail,0 NotificationSMS, *
  FROM [dbo].[tblMeeting_SubcommitteeDetail] WITH (NOLOCK) WHERE [SubcommitteeMasterId]=" +
                             ID;

              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw ex;
          }


      }

      public DataTable GetforEdit(Int32 Id)
      {
          string query = @"Select  * from tblMeeting_SubcommitteeMaster      WITH (NOLOCK)    
                               
							 
							  where  SubcommitteeMasterId= " + Id;
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }
      public DataTable GetforEdit2(Int32 Id)
      {
          string query = @"SELECT BMemberSetupDetailsID,MeetingMemberTypeId,PositionId FROM tblMeeting_SubcommitteeMasterDetails
WITH (NOLOCK) WHERE SubcommitteeMasterId= " + Id;
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }

      public DataTable GetDDLCompany()
      {
          string query = @"SELECT com.CompanyId AS Value, com.ShortName AS TextField  FROM dbo.tblCompanyInfo com WITH (NOLOCK) ";
          return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
      }
      public void GetCategoryListIntoDropdown(DropDownList ddl)
      {
          string queryStr = @"
SELECT [CategoryID]
      ,[MeetingCategory]
  FROM [dbo].[tblMeeting_Category] WITH (NOLOCK)  WHERE CategoryID NOT IN(1)";
          aCommonInternalDal.LoadDropDownValue(ddl, "MeetingCategory", "CategoryID", queryStr, "HRDB");
      }

      public DataTable GetDDLCat()
      {
          string query = @"SELECT com.CompanyId AS Value, com.ShortName AS TextField  FROM dbo.tblCompanyInfo com WITH (NOLOCK) ";
          return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
      }

      public DataTable GetGrade(String ComId)
      {
          string queryStr = @" SELECT mas.Name, mas.MemberSetupDetailsID,mas.MeetingMemberTypeId FROM dbo.tblMeeting_MemberSetupDetails  mas WITH (NOLOCK)
							  

							  ORDER BY  mas.OrderNo";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      public DataTable GetCheckPartInfo(string ComID)
      {
          string query = @"SELECT UPPER(SubCommitteeName) as MeetingCategory  FROM dbo.tblMeeting_SubcommitteeMaster  with(nolock)
                   where  SubCommitteeName='" + ComID + "' ";
          return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
      }

      public DataTable GetCheckPartInfo2(string ComID, string MasterdId)
      {
          string query = @"SELECT UPPER(SubCommitteeName) as MeetingCategory  FROM dbo.tblMeeting_SubcommitteeMaster  with(nolock)
                   where  SubCommitteeName='" + ComID + "' AND SubcommitteeMasterId not in (" + MasterdId + ")";
          return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
      }

      public int SaveMaster(SubcommitteeMasterDAO aMaster, string user)
      {
          try
          {
              if (aMaster.SubcommitteeMasterId > 0)
              {
                  /////asdasddasd
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@SubcommitteeMasterId", aMaster.SubcommitteeMasterId));
                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@CategoryID", aMaster.CategoryID));
                  aParameters.Add(new SqlParameter("@SubCommitteeName", aMaster.SubCommitteeName));
                  aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
                  aParameters.Add(new SqlParameter("@Remarks", aMaster.Remarks));
                  aParameters.Add(new SqlParameter("@UpdateBy", user));

                  aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));




                  string query = @"UPDATE [dbo].[tblMeeting_SubcommitteeMaster]
   SET [CompanyId] = @CompanyId 
      ,[CategoryID] = @CategoryID 
      ,[SubCommitteeName] = @SubCommitteeName 
      ,[Remarks] = @Remarks  
      ,[UpdateBy] = @UpdateBy 
      ,[UpdateDate] = @UpdateDate ,ActionStatus=@ActionStatus
 WHERE  SubcommitteeMasterId=@SubcommitteeMasterId";

                  bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                  if (result == true)
                  {
                      return aMaster.SubcommitteeMasterId;
                  }
                  else
                  {
                      return 0;
                  }

              }
              else
              {


                  List<SqlParameter> aParameters = new List<SqlParameter>();

                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@CategoryID", aMaster.CategoryID));
                  aParameters.Add(new SqlParameter("@SubCommitteeName", aMaster.SubCommitteeName));
                  aParameters.Add(new SqlParameter("@Remarks", aMaster.Remarks));
                  aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));
                  aParameters.Add(new SqlParameter("@CreateBy", user));

                  aParameters.Add(new SqlParameter("@CreateDate", System.DateTime.Now));

               

                  string query = @"INSERT INTO [dbo].[tblMeeting_SubcommitteeMaster]
           ( [CompanyId]
           ,[CategoryID]
           ,[SubCommitteeName]
           ,[Remarks]
           ,[CreateBy]
           ,[CreateDate],ActionStatus )
     VALUES
           (@CompanyId
           ,@CategoryID
           ,@SubCommitteeName
           ,@Remarks
           ,@CreateBy
           ,@CreateDate,@ActionStatus )";

                  int pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                  return pk;
              }
          }
          catch (Exception ex)
          {

              throw;
          }
      }

      public DataTable loadMember(string param)
      {
          string query = @"SELECT us.UserName as CreateBy, usUp.UserName as UpdateBy, com.ShortName,  cat.MeetingCategory, * from tblMeeting_SubcommitteeMaster BMSM     WITH (NOLOCK)    
                               
							     left JOIN  dbo.tblUser us   ON  BMSM.CreateBy =us.UserId  
              left JOIN  dbo.tblUser usUp   ON  BMSM.UpdateBy =usUp.UserId
left JOIN  dbo.tblCompanyInfo com   ON  BMSM.CompanyId =com.CompanyId
left JOIN  dbo.tblMeeting_Category cat   ON  BMSM.CategoryID =cat.CategoryID
							  where  BMSM.SubcommitteeMasterId IS NOT NUll " + param + " ORDER BY BMSM.CreateDate desc ";

          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }
      public bool DeleteById(int MasterId, string ID, string MemberTypeId, string Position)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@SubcommitteeMasterId", MasterId));
          aSqlParameterlist.Add(new SqlParameter("@BMemberSetupDetailsID", ID));
          aSqlParameterlist.Add(new SqlParameter("@MeetingMemberTypeId", MemberTypeId));
          aSqlParameterlist.Add(new SqlParameter("@PositionId", Position));





          const string query = @"INSERT INTO [dbo].[tblMeeting_SubcommitteeMasterDetails]
           ([SubcommitteeMasterId]
           ,[BMemberSetupDetailsID]
           ,[Name]
           ,[Address]
           ,[MobileNo]
           ,[Email]
           ,[MembershipDate]
           ,[Note]
           ,[MemberType]
           ,[MeetingMemberTypeId]
           ,[JoiningDate]
           ,[CompanyId],PositionId)
		   SELECT @SubcommitteeMasterId, MemberSetupDetailsID
        ,[Name]
      ,[Address]
      ,[MobileNo]
      ,[Email]
      ,[MembershipDate]
      ,[Note]
      ,[MemberType]
      ,@MeetingMemberTypeId
      ,[JoiningDate]
      ,[CompanyId] ,@PositionId
  FROM [dbo].[tblMeeting_MemberSetupDetails] WHERE  MemberSetupDetailsID IN ( @BMemberSetupDetailsID)";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
      }

      public bool SaveDetails(List<MiscellaneousInfoDetailDAO> aList, int masterid)
      {
          try
          {
              List<SqlParameter> aParametersd = new List<SqlParameter>();
              aParametersd.Add(new SqlParameter("@SubcommitteeMasterId", masterid));
              string queryDel = @"DELETE FROM [dbo].[tblMeeting_SubcommitteeDetail]
      WHERE  SubcommitteeMasterId=@SubcommitteeMasterId";

              bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


              bool result = false;
              foreach (var item in aList)
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();

                  aParameters.Add(new SqlParameter("@SubcommitteeMasterId", masterid));
                  aParameters.Add(new SqlParameter("@Type", item.Type ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@EmpMasterCode", item.EmpMasterCode ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@EmpName", item.EmpName ?? (object)DBNull.Value));
                  aParameters.Add(new SqlParameter("@Designation", item.Designation ?? (object)DBNull.Value));




                  string query = @"
INSERT INTO [dbo].[tblMeeting_SubcommitteeDetail]
           ([SubcommitteeMasterId]
           ,[Type]
           ,[EmpInfoId]
           ,[EmpMasterCode]
           ,[EmpName]
           ,[Designation])
     VALUES
           (@SubcommitteeMasterId
           ,@TYPE 
           ,@EmpInfoId 
           ,@EmpMasterCode 
           ,@EmpName 
           ,@Designation)";
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
      public bool DeleteByDetrailsId(int MasterId)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@SubcommitteeMasterId", MasterId));
         





          const string query = @"


DELETE FROM [dbo].[tblMeeting_SubcommitteeMasterDetails]
      WHERE SubcommitteeMasterId=@SubcommitteeMasterId



";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
      }


      public bool DeleteByMasterId(int MasterId)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@SubcommitteeMasterId", MasterId));






          const string query = @"


UPDATE [dbo].[tblMeeting_SubcommitteeMaster]
   SET  [ActionStatus] = 'Inactive'
      WHERE SubcommitteeMasterId=@SubcommitteeMasterId



";


          return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
      }

    }
}
