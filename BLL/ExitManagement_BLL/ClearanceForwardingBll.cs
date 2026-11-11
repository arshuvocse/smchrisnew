using System;
using System.Collections.Generic;
using System.Data;
using DAL.ExitManagement_DAL;
using DAO.HRIS_DAO;

namespace BLL.ExitManagement_BLL
{
    public class ClearanceForwardingBll
    {
        ClearanceForwardingDal aClearanceForwardingDal = new ClearanceForwardingDal();

        public bool IsAlreadyForwarded(int companyId, long employeeId)
        {
            return aClearanceForwardingDal.IsAlreadyForwarded(companyId, employeeId);
        }

        public int SaveForwarding(ClearanceForwardingMasterDao aDao, List<long> supervisedEmployeeIds)
        {
            return aClearanceForwardingDal.SaveForwarding(aDao, supervisedEmployeeIds);
        }

        public bool UpdateForwarding(ClearanceForwardingMasterDao aDao, List<long> supervisedEmployeeIds)
        {
            return aClearanceForwardingDal.UpdateForwarding(aDao, supervisedEmployeeIds);
        }

        public DataTable GetSupervisedEmployeesByDivision(long supervisorEmployeeId)
        {
            return aClearanceForwardingDal.GetSupervisedEmployeesByDivision(supervisorEmployeeId);
        }

        public DataTable GetForwardingList(int? companyId, long? employeeId, DateTime? dateFrom, DateTime? dateTo, string status)
        {
            return aClearanceForwardingDal.GetForwardingList(companyId, employeeId, dateFrom, dateTo, status);
        }

        public DataTable GetForwardingMasterById(int clearanceForwardingId)
        {
            return aClearanceForwardingDal.GetForwardingMasterById(clearanceForwardingId);
        }

        public DataTable GetForwardingDetailById(int clearanceForwardingId)
        {
            return aClearanceForwardingDal.GetForwardingDetailById(clearanceForwardingId);
        }

        public bool CompleteForwarding(int clearanceForwardingDetailId, long completedByEmployeeId)
        {
            return aClearanceForwardingDal.CompleteForwarding(clearanceForwardingDetailId, completedByEmployeeId);
        }
    }
}
