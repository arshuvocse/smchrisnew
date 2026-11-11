using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;

namespace DAL.MeetingMinorsDAL
{

    public class FloorDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(FloorDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FloorId", aEntryDao.FloorId));
            aSqlParameterlist.Add(new SqlParameter("@OfficeId", aEntryDao.OfficeId));
            aSqlParameterlist.Add(new SqlParameter("@LocationId", aEntryDao.LocationId));

            aSqlParameterlist.Add(new SqlParameter("@FloorName", aEntryDao.FloorName));
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aEntryDao.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aEntryDao.CreateDate));


            const string queryStr = @"INSERT INTO tblMeetingRoom_DEL (MeetingRoomId,OfficeId,LocationId,FloorId,MeetingRoomName,MeetingRoomCapacity,CreateBy,CreateDate)
                                   VALUES (@MeetingRoomId,@OfficeId,@LocationId,@FloorId,@MeetingRoomName,@MeetingRoomCapacity,@CreateBy,@CreateDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetDDLCompany()
        {
            string query = @"SELECT com.CompanyId AS Value, com.ShortName AS TextField  FROM dbo.tblCompanyInfo com WITH (NOLOCK)";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLSalaryLocation()
        {

            string query = @"SELECT dt.SalaryLoationId AS Value, dt.SalaryLocation AS TextField FROM dbo.tblSalaryLocation dt WHERE dt.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }


        public DataTable GetDDLJobLocation(string SalaryLocID)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", SalaryLocID));
            string query = @"SELECT Jloc.JobLocationID AS Value, Jloc.SalaryLoationId, Jloc.Location AS TextField FROM dbo.tblJobLocation Jloc WHERE Jloc.SalaryLoationId=@SalaryLoationId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetDDLFloorLocation(string SalaryLocID)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@LocationId", SalaryLocID));
            string query = @"SELECT Jloc.FloorId AS Value,  Jloc.FloorName AS TextField FROM dbo.tblFloor Jloc WHERE Jloc.LocationId=@LocationId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }


        public int SaveEntryInfo(FloorDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@OfficeId", aEntryDao.OfficeId));
            aSqlParameterlist.Add(new SqlParameter("@LocationId", aEntryDao.LocationId));

            aSqlParameterlist.Add(new SqlParameter("@FloorName", aEntryDao.FloorName));
       
            aSqlParameterlist.Add(new SqlParameter("@CreateBy", aEntryDao.CreateBy));
            aSqlParameterlist.Add(new SqlParameter("@CreateDate", aEntryDao.CreateDate));

            const string queryStr = @"INSERT INTO tblFloor (OfficeId,LocationId,FloorName,CreateBy,CreateDate)
                                   VALUES (@OfficeId,@LocationId,@FloorName,@CreateBy,@CreateDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }




        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MeetingRoomId", MId));

            const string queryStr = @"SELECT * FROM tblFloor WHERE FloorId = @MeetingRoomId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(FloorDAO aUpdateDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FloorId", aUpdateDao.FloorId));
            aSqlParameterlist.Add(new SqlParameter("@OfficeId", aUpdateDao.OfficeId));
            aSqlParameterlist.Add(new SqlParameter("@LocationId", aUpdateDao.LocationId));

            aSqlParameterlist.Add(new SqlParameter("@FloorName", aUpdateDao.FloorName));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aUpdateDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aUpdateDao.UpdateDate));

            const string queryStr = @"UPDATE tblFloor SET OfficeId = @OfficeId, LocationId=@LocationId,FloorName=@FloorName, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE FloorId = @FloorId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }



        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MeetingRoomId", Id));


            const string queryStr = @"DELETE FROM tblFloor  WHERE FloorId = @MeetingRoomId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetEntryformation()
        {
            string queryStr = @"SELECT MTb.FloorId ,MTb.FloorName, us.UserName as EntryBy, usUp.UserName as UpdateBy, SL.SalaryLocation, JL.Location,tblFloor.FloorName,MTb.CreateDate,MTb.UpdateDate FROM tblFloor MTb WITH (NOLOCK)
              left join dbo.tblSalaryLocation SL On SL.SalaryLoationId= MTb.OfficeId 
			  left join dbo.tblJobLocation JL On JL.JobLocationID= MTb.LocationId 
			  left join dbo.tblFloor  On tblFloor.FloorId= MTb.FloorId 
              left JOIN  dbo.tblUser us   ON  MTb.CreateBy =us.UserId  
              left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetCheckOffice(string ComID)
        {
            string query = @"SELECT OfficeId FROM dbo.tblMeetingRoom  with(nolock)
                   where  OfficeId='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheckOffice2(string ComID, string MasterdId)
        {
            string query = @"SELECT OfficeId FROM dbo.tblMeetingRoom with(nolock) where OfficeId ='" + ComID + "' AND MeetingRoomId not in (" + MasterdId + ")";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }


        public DataTable GetCheckLocation(string ComID)
        {
            string query = @"SELECT LocationId FROM dbo.tblMeetingRoom  with(nolock)
                   where  LocationId='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheckLocation2(string ComID, string MasterdId)
        {
            string query = @"SELECT LocationId FROM dbo.tblMeetingRoom with(nolock) where LocationId ='" + ComID + "' AND MeetingRoomId not in (" + MasterdId + ")";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }


        public DataTable GetCheckFloor(string ComID)
        {
            string query = @"SELECT FloorId FROM dbo.tblMeetingRoom  with(nolock)
                   where  FloorId='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheckFloor2(string ComID, string MasterdId)
        {
            string query = @"SELECT FloorId FROM dbo.tblMeetingRoom with(nolock) where FloorId ='" + ComID + "' AND MeetingRoomId not in (" + MasterdId + ")";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }


        public DataTable GetCheckMeetingRoom(string ComID)
        {
            string query = @"SELECT UPPER(MeetingRoomName) as MeetingRoomName FROM dbo.tblMeetingRoom  with(nolock)
                   where  MeetingRoomName='" + ComID + "' ";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public DataTable GetCheckMeetingRoom2(string ComID, string MasterdId)
        {
            string query = @"SELECT UPPER(MeetingRoomName) as MeetingRoomName  FROM dbo.tblMeetingRoom with(nolock) where MeetingRoomName ='" + ComID + "' AND MeetingRoomId not in (" + MasterdId + ")";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }
    }
}
