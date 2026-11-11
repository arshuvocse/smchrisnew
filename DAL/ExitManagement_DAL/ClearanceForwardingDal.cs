using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.DataManager;
using DAO.HRIS_DAO;

namespace DAL.ExitManagement_DAL
{
    public class ClearanceForwardingDal
    {
        DataAccessManager accessManager = new DataAccessManager();

        public bool IsAlreadyForwarded(int companyId, long employeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CompanyId", companyId));
                aList.Add(new SqlParameter("@EmployeeId", employeeId));
                dt = accessManager.GetDataTable("sp_GET_ClearanceForwardingCheckDuplicate", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return dt.Rows.Count > 0;
        }

        public int SaveForwarding(ClearanceForwardingMasterDao aDao, List<long> supervisedEmployeeIds)
        {
            int newId = 0;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);

                List<SqlParameter> masterParams = new List<SqlParameter>();
                masterParams.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
                masterParams.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
                masterParams.Add(new SqlParameter("@ForwardedBy", aDao.ForwardedBy));
                masterParams.Add(new SqlParameter("@Remarks", (object)aDao.Remarks ?? DBNull.Value));
                masterParams.Add(new SqlParameter("@EntryBy", (object)aDao.EntryBy ?? DBNull.Value));
                newId = accessManager.SaveDataReturnPrimaryKey("sp_Save_ClearanceForwardingMaster", masterParams);

                int orderNo = 1;
                foreach (long supervisedEmployeeId in supervisedEmployeeIds)
                {
                    List<SqlParameter> detailParams = new List<SqlParameter>();
                    detailParams.Add(new SqlParameter("@ClearanceForwardingId", newId));
                    detailParams.Add(new SqlParameter("@SupervisedEmployeeId", supervisedEmployeeId));
                    detailParams.Add(new SqlParameter("@OrderNo", orderNo++));
                    accessManager.SaveData("sp_Save_ClearanceForwardingDetail", detailParams);
                }
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return newId;
        }

        public bool UpdateForwarding(ClearanceForwardingMasterDao aDao, List<long> supervisedEmployeeIds)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);

                List<SqlParameter> masterParams = new List<SqlParameter>();
                masterParams.Add(new SqlParameter("@ClearanceForwardingId", aDao.ClearanceForwardingId));
                masterParams.Add(new SqlParameter("@Remarks", (object)aDao.Remarks ?? DBNull.Value));
                masterParams.Add(new SqlParameter("@UpdateBy", (object)aDao.UpdateBy ?? (object)aDao.EntryBy ?? DBNull.Value));
                accessManager.SaveData("sp_Update_ClearanceForwardingMaster", masterParams);

                List<SqlParameter> delParams = new List<SqlParameter>();
                delParams.Add(new SqlParameter("@ClearanceForwardingId", aDao.ClearanceForwardingId));
                accessManager.SaveData("sp_Delete_ClearanceForwardingDetailsPending", delParams);

                int orderNo = 1;
                foreach (long supervisedEmployeeId in supervisedEmployeeIds)
                {
                    List<SqlParameter> detailParams = new List<SqlParameter>();
                    detailParams.Add(new SqlParameter("@ClearanceForwardingId", aDao.ClearanceForwardingId));
                    detailParams.Add(new SqlParameter("@SupervisedEmployeeId", supervisedEmployeeId));
                    detailParams.Add(new SqlParameter("@OrderNo", orderNo++));
                    accessManager.SaveData("sp_Save_ClearanceForwardingDetail", detailParams);
                }
                return true;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public DataTable GetSupervisedEmployeesByDivision(long supervisorEmployeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@SupervisorEmpId", supervisorEmployeeId));
                dt = accessManager.GetDataTable("sp_GET_SupervisedEmployeesByDivision", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return dt;
        }

        public DataTable GetForwardingList(int? companyId, long? employeeId, DateTime? dateFrom, DateTime? dateTo, string status)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@CompanyId", (object)companyId ?? DBNull.Value));
                aList.Add(new SqlParameter("@EmployeeId", (object)employeeId ?? DBNull.Value));
                aList.Add(new SqlParameter("@DateFrom", (object)dateFrom ?? DBNull.Value));
                aList.Add(new SqlParameter("@DateTo", (object)dateTo ?? DBNull.Value));
                aList.Add(new SqlParameter("@Status", (object)status ?? DBNull.Value));
                dt = accessManager.GetDataTable("sp_GET_ClearanceForwardingList", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return dt;
        }

        public DataTable GetForwardingMasterById(int clearanceForwardingId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@ClearanceForwardingId", clearanceForwardingId));
                dt = accessManager.GetDataTable("sp_GET_ClearanceForwardingMasterById", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return dt;
        }

        public DataTable GetForwardingDetailById(int clearanceForwardingId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@ClearanceForwardingId", clearanceForwardingId));
                dt = accessManager.GetDataTable("sp_GET_ClearanceForwardingDetailById", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return dt;
        }

        // Marks a forwarded detail row Done for the employee it was actually forwarded to,
        // and - the first time this happens for a given master record - marks the whole
        // forwarding Done too (any one of the forwarded people completing it is enough).
        // Returns false if it was already completed, not found, or not forwarded to
        // completedByEmployeeId; the stored procedure enforces both checks atomically so a
        // duplicate/concurrent completion attempt cannot double-apply.
        public bool CompleteForwarding(int clearanceForwardingDetailId, long completedByEmployeeId)
        {
            int rowsAffected = 0;
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@ClearanceForwardingDetailId", clearanceForwardingDetailId));
                aList.Add(new SqlParameter("@CompletedBy", completedByEmployeeId));
                rowsAffected = accessManager.SaveDataReturnPrimaryKey("sp_Update_ClearanceForwardingComplete", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
            return rowsAffected > 0;
        }
    }
}
