using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.InternalCls;
using Library.DAO.HRM_Entities;

namespace Library.DAL.HRM_DAL
{
    public class UserDAL
    {
        private ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool SaveUserInformation(UserInformation aInformation)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", aInformation.UserId));
            aSqlParameterlist.Add(new SqlParameter("@UserName", aInformation.UserName));
            aSqlParameterlist.Add(new SqlParameter("@UserType", aInformation.UserType));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aInformation.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@LoginName", aInformation.LoginName));
            aSqlParameterlist.Add(new SqlParameter("@Password", aInformation.Password));
            aSqlParameterlist.Add(new SqlParameter("@UserStatus", aInformation.UserStatus));
            aSqlParameterlist.Add(new SqlParameter("@Email", aInformation.Email));
            aSqlParameterlist.Add(new SqlParameter("@ContactNo", aInformation.ContactNo));

            string insertQuery = @"insert into tblUser (UserId,UserName,UserType,EmpMasterCode,LoginName,UserStatus,Email,Password,ContactNo) 
            values (@UserId,@UserName,@UserType,@EmpMasterCode,@LoginName,@UserStatus,@Email,@Password,@ContactNo)";
            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");

        }
        public bool HasUserInformationName(UserInformation aUserInformation)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@LoginName", aUserInformation.LoginName));
            string query = "select * from tblUser where LoginName = @LoginName";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, aSqlParameterlist, "HRDB");

            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    return true;
                }
            }
            return false;
        }
        public DataTable LoadUserView()
        {
            string query = @"SELECT tblUser.UserId,tblUser.UserName,tblUser.UserType,tblUser.LoginName,tblUser.Password,tblUser.UserStatus,tblUser.Email,tblUser.ContactNo,tblUser.EmpMasterCode FROM tblUser ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmpInfo(string EmpMasterCode)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo WHERE EmpMasterCode='" + EmpMasterCode + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public UserInformation UserInformationEditLoad(string userId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", userId));
            string query = "select * from tblUser where UserId = @UserId";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, aSqlParameterlist, "HRDB");

            UserInformation aUserInformation = new UserInformation();
            
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aUserInformation.UserId = Int32.Parse(dataReader["UserId"].ToString());
                    aUserInformation.EmpMasterCode = dataReader["EmpMasterCode"].ToString();
                    aUserInformation.UserName = dataReader["UserName"].ToString();
                    aUserInformation.UserType = dataReader["UserType"].ToString();
                    aUserInformation.LoginName = dataReader["LoginName"].ToString();
                    aUserInformation.Password = dataReader["Password"].ToString();
                    aUserInformation.UserStatus = dataReader["UserStatus"].ToString();
                    aUserInformation.Email = dataReader["Email"].ToString();
                    aUserInformation.ContactNo = dataReader["ContactNo"].ToString();
                }
            }
            return aUserInformation;
        }

        public bool UpdateUserInfo(UserInformation aUserInformation)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", aUserInformation.UserId));
            aSqlParameterlist.Add(new SqlParameter("@UserName", aUserInformation.UserName));
            aSqlParameterlist.Add(new SqlParameter("@UserType", aUserInformation.UserType));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aUserInformation.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@LoginName", aUserInformation.LoginName));
            aSqlParameterlist.Add(new SqlParameter("@Password", aUserInformation.Password));
            aSqlParameterlist.Add(new SqlParameter("@UserStatus", aUserInformation.UserStatus));
            aSqlParameterlist.Add(new SqlParameter("@Email", aUserInformation.Email));
            aSqlParameterlist.Add(new SqlParameter("@ContactNo", aUserInformation.ContactNo));

            string query = @"UPDATE tblUser SET UserName=@UserName,UserType=@UserType,EmpMasterCode=@EmpMasterCode,LoginName=@LoginName,Password=@Password,UserStatus=@UserStatus,Email=@Email,ContactNo=@ContactNo WHERE UserId=@UserId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public bool DeleteUserInfo(UserInformation aUserInformation)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", aUserInformation.UserId));

            string query = @"DELETE FROM dbo.tblUser WHERE UserId='"+aUserInformation.UserId+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
    }
}
