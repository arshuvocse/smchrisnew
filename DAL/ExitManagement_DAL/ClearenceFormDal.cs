using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;

namespace DAL.Survey
{
    public class ClearenceFormDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable Get_EmpClearance(int empId)
        {
            string queryStr = @" Select CONVERT(varchar,MobIssuDate,106)MobIssuDate, CONVERT(varchar,DBIssuDate,106)DBIssuDate,* from tblEmpClearance Where EmpInfoId =" + empId;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public bool IsEmployeeAlreadyAssigned(int masterId, int empInfoId)
        {
            string queryStr = @"SELECT dtl.EmpInfoId, dtl.EmpInfoIdApproval 
                                FROM [dbo].[tblEmpExitDetail] dtl WITH (NOLOCK) 
                                WHERE dtl.MasterId = " + masterId + @" AND (
                                    dtl.EmpInfoId = " + empInfoId + @" 
                                    OR dtl.EmpInfoIdApproval = " + empInfoId + @"
                                    OR (dtl.IsForward = 1 AND ISNULL(dtl.isForwardBackDone, 0) = 0 AND dtl.IsForwardEmpId = " + empInfoId + @")
                                    OR (
                                        ISNULL(dtl.isForwardBackDone, 0) = 0
                                        AND EXISTS (
                                            SELECT 1 
                                            FROM dbo.tblClearanceForwardingMaster m WITH (NOLOCK)
                                            INNER JOIN dbo.tblClearanceForwardingDetail d WITH (NOLOCK) ON m.ClearanceForwardingId = d.ClearanceForwardingId
                                            WHERE (m.EmployeeId = dtl.EmpInfoIdApproval OR m.EmployeeId = dtl.EmpInfoId)
                                              AND m.Status = 'Forwarded'
                                              AND d.Status = 'Pending'
                                              AND d.SupervisedEmployeeId = " + empInfoId + @"
                                        )
                                    )
                                )";
            DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
            return dt != null && dt.Rows.Count > 0;
        }

        public bool IsClearanceAutoForwarded(int masterId, int exitDetailId, int currentEmpId)
        {
            string detailCondition = exitDetailId > 0
                ? "dtl.ExitDetailId = " + exitDetailId
                : "dtl.MasterId = " + masterId;

            string queryStr = @"
SELECT TOP 1 1 
FROM dbo.tblEmpExitDetail dtl WITH (NOLOCK)
INNER JOIN dbo.tblClearanceForwardingMaster m WITH (NOLOCK) 
    ON (m.EmployeeId = dtl.EmpInfoIdApproval OR m.EmployeeId = dtl.EmpInfoId)
    AND m.Status = 'Forwarded'
INNER JOIN dbo.tblClearanceForwardingDetail d WITH (NOLOCK) 
    ON m.ClearanceForwardingId = d.ClearanceForwardingId
    AND d.Status IN ('Pending', 'Forwarded')
WHERE " + detailCondition;

            DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
            return dt != null && dt.Rows.Count > 0;
        }

        public bool IsEmployeeAlreadyDoneAndForwardedBack(int masterId, int exitDetailId, int selectedEmpId)
        {
            string queryStr = @"
SELECT TOP 1 1 
FROM dbo.tblEmpExitDetail dtl WITH (NOLOCK)
WHERE (dtl.ExitDetailId = " + exitDetailId + @" OR dtl.MasterId = " + masterId + @")
  AND (
      (dtl.IsForwardEmpId = " + selectedEmpId + @" AND ISNULL(dtl.isForwardBackDone, 0) = 1)
      OR EXISTS (
          SELECT 1 
          FROM dbo.tblDepWiseClearanceResourceUpdate upd WITH (NOLOCK) 
          WHERE (upd.exitDetailIdNew = dtl.ExitDetailId OR (dtl.ExitDetailId = " + exitDetailId + @" AND upd.exitDetailIdNew = " + exitDetailId + @")) 
            AND upd.IsDoneEmpId = " + selectedEmpId + @"
      )
      OR (
          ISNULL(dtl.isForwardBackDone, 0) = 1
          AND EXISTS (
              SELECT 1 
              FROM dbo.tblClearanceForwardingMaster m WITH (NOLOCK)
              INNER JOIN dbo.tblClearanceForwardingDetail d WITH (NOLOCK) ON m.ClearanceForwardingId = d.ClearanceForwardingId
              WHERE (m.EmployeeId = dtl.EmpInfoId OR m.EmployeeId = dtl.EmpInfoIdApproval)
                AND d.SupervisedEmployeeId = " + selectedEmpId + @"
          )
      )
  )";
            DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
            return dt != null && dt.Rows.Count > 0;
        }

        public string GetDoneAndForwardedEmpIds(int masterId, int exitDetailId)
        {
            string queryStr = @"
SELECT DISTINCT empId
FROM (
    SELECT dtl.IsForwardEmpId AS empId
    FROM dbo.tblEmpExitDetail dtl WITH (NOLOCK)
    WHERE (dtl.ExitDetailId = " + exitDetailId + @" OR dtl.MasterId = " + masterId + @")
      AND ISNULL(dtl.isForwardBackDone, 0) = 1 AND dtl.IsForwardEmpId IS NOT NULL
    UNION
    SELECT upd.IsDoneEmpId AS empId
    FROM dbo.tblDepWiseClearanceResourceUpdate upd WITH (NOLOCK)
    INNER JOIN dbo.tblEmpExitDetail dtl WITH (NOLOCK) ON upd.exitDetailIdNew = dtl.ExitDetailId
    WHERE (dtl.ExitDetailId = " + exitDetailId + @" OR dtl.MasterId = " + masterId + @")
      AND upd.IsDoneEmpId IS NOT NULL
    UNION
    SELECT d.SupervisedEmployeeId AS empId
    FROM dbo.tblEmpExitDetail dtl WITH (NOLOCK)
    INNER JOIN dbo.tblClearanceForwardingMaster m WITH (NOLOCK) ON (m.EmployeeId = dtl.EmpInfoId OR m.EmployeeId = dtl.EmpInfoIdApproval)
    INNER JOIN dbo.tblClearanceForwardingDetail d WITH (NOLOCK) ON m.ClearanceForwardingId = d.ClearanceForwardingId
    WHERE (dtl.ExitDetailId = " + exitDetailId + @" OR dtl.MasterId = " + masterId + @")
      AND ISNULL(dtl.isForwardBackDone, 0) = 1
) tblDone
WHERE empId IS NOT NULL";

            DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
            List<string> ids = new List<string>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["empId"] != DBNull.Value && !string.IsNullOrEmpty(row["empId"].ToString()))
                    {
                        ids.Add(row["empId"].ToString().Trim());
                    }
                }
            }
            return string.Join(",", ids.ToArray());
        }

        private class PendingApproverGroup
        {
            public string ApproverCode { get; set; }
            public string ApproverName { get; set; }
            public string ApprovalStatus { get; set; }
            public bool IsFwdPending { get; set; }
            public string FwdCode { get; set; }
            public string FwdName { get; set; }
            public List<PendingDelegateItem> AutoDelegates { get; set; }
        }

        private class PendingDelegateItem
        {
            public int EmpId { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
        }

        public bool CheckPendingClearanceAssignment(int masterId, int selectedEmpId, out string message)
        {
            return CheckPendingClearanceAssignment(masterId, selectedEmpId, 0, out message);
        }

        public bool CheckPendingClearanceAssignment(int masterId, int selectedEmpId, int exitDetailId, out string message)
        {
            message = string.Empty;

            if (exitDetailId > 0 && IsEmployeeAlreadyDoneAndForwardedBack(masterId, exitDetailId, selectedEmpId))
            {
                return true;
            }

            try
            {
                string queryStr = @"
SELECT 
    dtl.ExitDetailId,
    dtl.ApprovalStatus,
    ISNULL(dtl.EmpInfoIdApproval, dtl.EmpInfoId) AS CurrentApproverEmpId,
    ISNULL(eApp.EmpMasterCode, eMain.EmpMasterCode) AS ApproverCode,
    ISNULL(eApp.EmpName, eMain.EmpName) AS ApproverName,
    dtl.IsForward,
    dtl.IsForwardEmpId,
    eFwd.EmpMasterCode AS FwdCode,
    eFwd.EmpName AS FwdName,
    dtl.isForwardBackDone,
    fDtl.SupervisedEmployeeId AS AutoForwardEmpId,
    eAuto.EmpMasterCode AS AutoForwardCode,
    eAuto.EmpName AS AutoForwardName,
    fDtl.OrderNo AS AutoForwardOrderNo
FROM dbo.tblEmpExitDetail dtl WITH (NOLOCK)
LEFT JOIN dbo.tblEmpGeneralInfo eMain WITH (NOLOCK) ON dtl.EmpInfoId = eMain.EmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo eApp WITH (NOLOCK) ON dtl.EmpInfoIdApproval = eApp.EmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo eFwd WITH (NOLOCK) ON dtl.IsForwardEmpId = eFwd.EmpInfoId
OUTER APPLY (
    SELECT fDtlInner.SupervisedEmployeeId, fDtlInner.OrderNo
    FROM dbo.tblClearanceForwardingMaster fMstInner WITH (NOLOCK)
    INNER JOIN dbo.tblClearanceForwardingDetail fDtlInner WITH (NOLOCK) 
        ON fMstInner.ClearanceForwardingId = fDtlInner.ClearanceForwardingId
    WHERE (fMstInner.EmployeeId = dtl.EmpInfoIdApproval OR fMstInner.EmployeeId = dtl.EmpInfoId)
      AND fMstInner.Status = 'Forwarded'
      AND fDtlInner.Status = 'Pending'
      AND ISNULL(dtl.isForwardBackDone, 0) = 0
) fDtl
LEFT JOIN dbo.tblEmpGeneralInfo eAuto WITH (NOLOCK) ON fDtl.SupervisedEmployeeId = eAuto.EmpInfoId
WHERE dtl.MasterId = " + masterId + @" AND ISNULL(dtl.IsDone, 0) = 0
ORDER BY dtl.ExitDetailId, ISNULL(fDtl.OrderNo, 999)";

                DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);

                HashSet<int> pendingEmpIds = new HashSet<int>();
                Dictionary<int, PendingApproverGroup> groupedDetails = new Dictionary<int, PendingApproverGroup>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int detailId = Convert.ToInt32(row["ExitDetailId"]);
                        if (!groupedDetails.ContainsKey(detailId))
                        {
                            int approverEmpId = Convert.ToInt32(row["CurrentApproverEmpId"]);
                            pendingEmpIds.Add(approverEmpId);

                            bool isFwdPending = false;
                            if (row["IsForward"] != DBNull.Value && Convert.ToBoolean(row["IsForward"]))
                            {
                                bool isFwdBackDone = row["isForwardBackDone"] != DBNull.Value && Convert.ToBoolean(row["isForwardBackDone"]);
                                if (!isFwdBackDone && row["IsForwardEmpId"] != DBNull.Value)
                                {
                                    isFwdPending = true;
                                    int fwdEmpId = Convert.ToInt32(row["IsForwardEmpId"]);
                                    pendingEmpIds.Add(fwdEmpId);
                                }
                            }

                            groupedDetails[detailId] = new PendingApproverGroup
                            {
                                ApproverCode = row["ApproverCode"] != DBNull.Value ? row["ApproverCode"].ToString().Trim() : "",
                                ApproverName = row["ApproverName"] != DBNull.Value ? row["ApproverName"].ToString().Trim() : "",
                                ApprovalStatus = row["ApprovalStatus"] != DBNull.Value ? row["ApprovalStatus"].ToString().Trim() : "",
                                IsFwdPending = isFwdPending,
                                FwdCode = row["FwdCode"] != DBNull.Value ? row["FwdCode"].ToString().Trim() : "",
                                FwdName = row["FwdName"] != DBNull.Value ? row["FwdName"].ToString().Trim() : "",
                                AutoDelegates = new List<PendingDelegateItem>()
                            };
                        }

                        if (row["AutoForwardEmpId"] != DBNull.Value && !string.IsNullOrEmpty(row["AutoForwardEmpId"].ToString()))
                        {
                            int autoEmpId = Convert.ToInt32(row["AutoForwardEmpId"]);
                            pendingEmpIds.Add(autoEmpId);

                            int orderNo = row["AutoForwardOrderNo"] != DBNull.Value ? Convert.ToInt32(row["AutoForwardOrderNo"]) : 1;
                            string role = orderNo == 1 ? "Primary Person" : "Alternative";

                            groupedDetails[detailId].AutoDelegates.Add(new PendingDelegateItem
                            {
                                EmpId = autoEmpId,
                                Code = row["AutoForwardCode"] != DBNull.Value ? row["AutoForwardCode"].ToString().Trim() : "",
                                Name = row["AutoForwardName"] != DBNull.Value ? row["AutoForwardName"].ToString().Trim() : "",
                                Role = role
                            });
                        }
                    }
                }

                bool isAlreadyPending = pendingEmpIds.Contains(selectedEmpId);
                bool isAlreadyAssigned = IsEmployeeAlreadyAssigned(masterId, selectedEmpId);

                if (isAlreadyPending || isAlreadyAssigned)
                {
                    StringBuilder sb = new StringBuilder();
                    if (isAlreadyPending)
                    {
                        sb.AppendLine("This employee cannot be selected because they are already pending for this clearance approval!");
                    }
                    else
                    {
                        sb.AppendLine("This employee cannot be selected because they are already assigned to this clearance!");
                    }

                    if (groupedDetails.Count > 0)
                    {
                        sb.AppendLine();
                        sb.AppendLine("Currently Pending Person(s):");
                        foreach (var kvp in groupedDetails)
                        {
                            var item = kvp.Value;
                            string roleSuffix = !string.IsNullOrEmpty(item.ApprovalStatus) ? " [" + item.ApprovalStatus + "]" : "";
                            sb.AppendLine("• " + item.ApproverName + " (" + item.ApproverCode + ")" + roleSuffix);

                            if (item.IsFwdPending && !string.IsNullOrEmpty(item.FwdName))
                            {
                                sb.AppendLine("  - Forwarded To: " + item.FwdName + " (" + item.FwdCode + ")");
                            }

                            foreach (var del in item.AutoDelegates)
                            {
                                sb.AppendLine("  - Auto Forward [" + del.Role + "]: " + del.Name + " (" + del.Code + ")");
                            }
                        }
                    }

                    message = sb.ToString().TrimEnd();
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                if (IsEmployeeAlreadyAssigned(masterId, selectedEmpId))
                {
                    message = "This employee cannot be selected as they are already assigned.";
                    return false;
                }
                return true;
            }
        }

        public bool DeleteDataForClearance(int empId)
        {
            string queryStr = @"Delete from tblEmpClearance 
             where EmpInfoId=" + empId + " ";

            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, DataBase.HRDB);
        }


        public int SaveClearanceInfo(EmpClearanceDao aDao, Int32 MasterId, string EntryBy)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", (object)aDao.EmpInfoId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MobIssuDate", (object)aDao.MobIssuDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MobActualPrice", (object)aDao.MobActualPrice ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoDABeforeOne", (object)aDao.MoDABeforeOne ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoDABeforeOnetotwo", (object)aDao.MoDABeforeOnetotwo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoDAAboveTwo", (object)aDao.MoDAAboveTwo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoRemarks", (object)aDao.MoRemarks ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DBIssuDate", (object)aDao.DBIssuDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DBActualPrice", (object)aDao.DBActualPrice ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DBDeductionAmount", (object)aDao.DBDeductionAmount ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DBRemark", (object)aDao.DBRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@PIDeductionAmount", (object)aDao.PIDeductionAmount ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PIActualCost", (object)aDao.PIActualCost ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PIRemark", (object)aDao.PIRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@MarketDues", (object)aDao.MarketDues ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MarketRemarks", (object)aDao.MarketRemarks ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@IDCard", (object)aDao.IDCard ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IDCardRemark", (object)aDao.IDCardRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@TotalDeductionAmount", (object)aDao.TotalDeductionAmount?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ToDeducAmtRemark", (object)aDao.ToDeducAmtRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@EntryBy", EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@ClearenceId", MasterId));
            aSqlParameterlist.Add(new SqlParameter("@TickMark", aDao.TickMark ?? (object)DBNull.Value));



            string query = @"INSERT INTO [dbo].[tblEmpClearance]
           (
            [EmpInfoId]
           ,[MobIssuDate]
           ,[MobActualPrice]
           ,[MoDABeforeOne]
           ,[MoDABeforeOnetotwo]
           ,[MoDAAboveTwo]
           ,[MoRemarks]
           ,[DBIssuDate]
           ,[DBActualPrice]
           ,[DBDeductionAmount]
           ,[DBRemark]
           ,[PIActualCost]
           ,[PIDeductionAmount]
           ,[PIRemark]
           ,[IDCard]
           ,[IDCardRemark]
           ,[MarketDues]
           ,[MarketRemarks]
           ,[TotalDeductionAmount]
           ,[ToDeducAmtRemark]
           ,[EntryBy]
           ,[EntryDate]
         
           ,[TickMark]
           ,[ClearenceId])
     VALUES
           (
            @EmpInfoId, 
            @MobIssuDate,
            @MobActualPrice, 
            @MoDABeforeOne, 
            @MoDABeforeOnetotwo, 
            @MoDAAboveTwo, 
           @MoRemarks, 
           @DBIssuDate, 
           @DBActualPrice, 
           @DBDeductionAmount, 
           @DBRemark, 
           @PIActualCost, 
           @PIDeductionAmount, 
           @PIRemark, 
           @IDCard, 
           @IDCardRemark, 
           @MarketDues, 
           @MarketRemarks, 
           @TotalDeductionAmount, 
           @ToDeducAmtRemark, 
           @EntryBy, 
           GETDATE(), 
         
           @TickMark, 
           @ClearenceId
		   )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDocNewDataById(string ID)
        {
            try
            {
                string query = @"	SELECT  distinct FileName,DocumentLink ,DocumentNote
  FROM [dbo].[tblClearFormDocument] WITH (NOLOCK)   WHERE ClearenceId in (select ClearenceId from tblEmpClearenceForm where EmployeeId=" + ID+")";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
        public DataTable GetDocDataById(string ID)
        {
            try
            {
                string query = @"	SELECT  *
  FROM [dbo].[tblEmpExitDocument] WITH (NOLOCK) WHERE ExitMasterId=" +
                               ID;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        
        
        }


        public DataTable GetShowRemarksById(string ID)
        {
            try
            {
                string query = @"	SELECT  *
  FROM [dbo].[tblEmpExitDetail] WITH (NOLOCK) WHERE ExitDetailId=" +
                               ID;

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            //string query = "SELECT * FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, DataBase.HRDB);
        }
       
        public DataTable LoadEmployeeInfo(string employeeId, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", employeeId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName
                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE EGI.EmpInfoId = @EmployeeId AND EGI.CompanyId = @CompanyId";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable LoadEmployeeInfo(string employeeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", employeeId));
            

            const string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,EGI.SalaryLoationId
                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE EGI.EmpInfoId = @EmployeeId ";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable LoadExitDepartment(string companyId)
        {
            string queryStr = @"SELECT DSN.DepartmentId,
                                DSN.DepartmentName FROM tblDepartment AS DSN WHERE DSN.IsActive = 1 ANd DSN.CompanyId =  " + companyId;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public int SaveExitMasterInfo(ClearenceFormDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aDao.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", aDao.EmpName));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aDao.DesignationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDao.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aDao.SalaryGradeId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aDao.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@Description", aDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeJobLeftId", aDao.EmployeeJobLeftId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@EntryByPersonId", aDao.EntryByPersonId));
            aSqlParameterlist.Add(new SqlParameter("@Recommend", aDao.Recommend));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aDao.SalaryLoationId ?? (object)DBNull.Value));

            string query = @"INSERT INTO dbo.tblEmpClearenceForm
                            (
                                CompanyId,
                                EmployeeId,
                                EmpCode,
                                EmpName,
                                JoiningDate,
                                DivisionId,
                                DesignationId,
                                SalaryGradeId,
                                Description,
                                ActionStatus,
                                EntryBy,
                                EntryDate,
                                EmployeeJobLeftId,
                                EntryByDepartmentId,
                                EntryByPersonId,Recommend, Remarks,SalaryLoationId
                            )
                            VALUES
                            (  
                                @CompanyId,
                                @EmployeeId,
                                @EmpCode,
                                @EmpName,
                                @JoiningDate,
                                @DivisionId,
                                @DesignationId,
                                @SalaryGradeId,
                                @Description,
                                @ActionStatus,
                                @EntryBy,
                                @EntryDate,
                                @EmployeeJobLeftId,
                                @DepartmentId,
                                @EntryByPersonId,@Recommend, @Remarks,@SalaryLoationId
 
                            )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }

        public Int32 SaveClearenceDetail(List<ClearenceFormDetailDao> aCreationLocationDaos)
        {
            Int32 id = 0;

            foreach (var aData in aCreationLocationDaos)
            {
                id = SaveClearenceDetailInfo(aData);
            }

            return id;
        }
        public Int32 SaveClearenceDetail2(List<ClearenceFormDetailDao> aCreationLocationDaos)
        {
            Int32 id = 0;

            foreach (var aData in aCreationLocationDaos)
            {
                id = SaveClearenceDetailInfo2(aData);
            }

            return id;
        }
        private Int32 SaveClearenceDetailInfo2(ClearenceFormDetailDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DepID", aDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@EmpID", aDao.EmpID));
            aSqlParameterlist.Add(new SqlParameter("@Resource", aDao.Resource));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDao.Remarks));

 
            aSqlParameterlist.Add(new SqlParameter("@IsDoneEmpId", aDao.IsDoneEmpId));
            aSqlParameterlist.Add(new SqlParameter("@IsDoneDate", aDao.IsDoneDate));
            aSqlParameterlist.Add(new SqlParameter("@MainRemarks", aDao.MainRemarks));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalCondition", aDao.ApprovalCondition));
            aSqlParameterlist.Add(new SqlParameter("@SetInfo", aDao.SetInfo));
            aSqlParameterlist.Add(new SqlParameter("@Recommend", aDao.Recommend ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@exitDetailIdNew", aDao.exitDetailIdNew ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@exitMasterIdNew", aDao.exitMasterIdNew ?? (object)DBNull.Value));


            string query = @"INSERT INTO dbo.tblDepWiseClearanceResourceUpdate
                            (   
                                DepID,
                                EmpID,
                                Resource,
                                Remarks,IsDoneEmpId,IsDoneDate,MainRemarks,Recommend,ApprovalCondition, SetInfo,exitMasterIdNew,exitDetailIdNew
                            )
                            VALUES
                            (   
                                @DepID,                                
                                @EmpID,
                                @Resource ,
                                @Remarks,@IsDoneEmpId,@IsDoneDate ,@MainRemarks,@Recommend ,@ApprovalCondition ,@SetInfo,@exitMasterIdNew,@exitDetailIdNew  
                              )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }


        private Int32 SaveClearenceDetailInfo(ClearenceFormDetailDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterId", aDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@Resource", aDao.Resource));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDao.Remarks));


            string query = @"INSERT INTO dbo.tblClearenceDetail
                            (   
                                MasterId,
                                Resource,
                                Remarks
                            )
                            VALUES
                            (   
                                @MasterId,                                
                                @Resource,
                                @Remarks      
 
                            )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetUserDepartmentId(int empId)
        {
            string queryStr = @"SELECT EGI.DepartmentId,EGI.DivisionId FROM dbo.tblUser AS USR
                                INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = USR.EmpInfoId WHERE USR.UserId = " + empId;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public bool UpdateEmpExitDetail(int masterId, int empId, int ExitDetailId)
        {
            string queryStr = @"
UPDATE [dbo].[tblEmpExitDetail]
   SET [IsDone] = 1, isFinalDoneDate = GETDATE()
 WHERE  MasterId=" + masterId + " and ExitDetailId=" + ExitDetailId;
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, DataBase.HRDB);
        }
        public bool UpdateForwardtoOtherEmpExitDetail(int masterId, int empId, int AppPer, int ExitDetailId, string ForwardRemarks)
        {
            string queryStr = @"
UPDATE [dbo].[tblEmpExitDetail]
   SET EmpInfoIdApproval =" + AppPer + @"  , ForwardRemarks =@ForwardRemarks, IsForward=0 ,IsForwardEmpId =" + AppPer + @", IsForwardDate = GETDATE(), isForwardBackDone = 0, isForwardBackDoneDate = NULL
 WHERE ExitDetailId=" + ExitDetailId;
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ForwardRemarks", (object)ForwardRemarks ?? DBNull.Value));
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public bool UpdateForwardtoOtherEmpExitDetail2(int masterId, int empId, int AppPer, int ExitDetailId, string ForwardRemarks)
        {
            string queryStr = @"
UPDATE [dbo].[tblEmpExitDetail]
   SET EmpInfoIdApproval =" + AppPer + @"  , ForwardRemarks =@ForwardRemarks   , IsForward=1, IsForwardDate = GETDATE()
 WHERE ExitDetailId=" + ExitDetailId;
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ForwardRemarks", (object)ForwardRemarks ?? DBNull.Value));
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public bool UpdateForwardBackEmpExitDetail(int masterId, int empId, int AppPer, int ExitDetailId, string ForwardRemarks)
        {
            string queryStr = @"
UPDATE [dbo].[tblEmpExitDetail]
   SET EmpInfoIdApproval = EmpInfoId, 
       ForwardRemarks = @ForwardRemarks, 
       IsForward = 1, 
       isForwardBackDone = 1, 
       isForwardBackDoneDate = GETDATE(),
       IsForwardEmpId = ISNULL(IsForwardEmpId, @ForwardedByEmpId)
 WHERE ExitDetailId=" + ExitDetailId;
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ForwardRemarks", (object)ForwardRemarks ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ForwardedByEmpId", AppPer));
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public bool HasActiveClearanceAutoForward(int approverEmpId)
        {
            string queryStr = @"
SELECT TOP 1 1 
FROM dbo.tblClearanceForwardingMaster m
INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId
WHERE m.EmployeeId = " + approverEmpId + @"
  AND m.Status = 'Forwarded'
  AND d.Status = 'Pending'";

            DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
            return dt != null && dt.Rows.Count > 0;
        }

        public bool IsClearanceAutoForwardRecipient(int approverEmpId, int userEmpId)
        {
            string queryStr = @"
SELECT TOP 1 1 
FROM dbo.tblClearanceForwardingMaster m
INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId
WHERE m.EmployeeId = " + approverEmpId + @"
  AND d.SupervisedEmployeeId = " + userEmpId + @"
  AND m.Status = 'Forwarded'
  AND d.Status = 'Pending'";

            DataTable dt = aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
            return dt != null && dt.Rows.Count > 0;
        }


        public DataTable GetClearenceInfo(int empId)
        {
            string queryStr = @"SELECT  EE.DivisionId, EE.CompanyId, CLSF.EmpCode,CLSF.EmpName,Desg.Designation,Sgrd.GradeName,Div.DivisionName,CLSF.JoiningDate,CLSF.Description,
                                TP.JobLeftType AS SeparationDate,JBLFT.JobLeftDate AS EffictiveDate,DEPT.DepartmentName,CLSF.EntryDate as ClearanceEntryDate
								, JL.Location as Location
								,EE.ImagePath as ImagePath,
								tblDepWiseClearanceResourceUpdate.Resource,tblDepWiseClearanceResourceUpdate.Remarks, DATEADD(day, -1, CAST(JBLFT.JobLeftDate AS date)) AS YesterdayDate 
                                FROM dbo.tblEmpClearenceForm AS CLSF

								 -- left JOIN tblJobLocation AS J ON j.SalaryLoationId = CLSF.SalaryLoationId  
								 
								INNER JOIN tblEmpGeneralInfo AS EE ON EE.EmpInfoId = CLSF.EmployeeId
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EE.JobLocationId 

                                INNER JOIN tblClearenceDetail AS CLSD ON CLSD.MasterId = CLSF.ClearenceId
                                left JOIN dbo.tblEmployeeJobLeft AS JBLFT ON JBLFT.EmployeeJobLeftId = CLSF.EmployeeJobLeftId
                                left JOIN tblJobLeftType AS TP ON TP.JobLeftTypeId = JBLFT.JobLeftTypeId
                                left JOIN dbo.tblDivision Div ON Div.DivisionId = CLSF.DivisionId
                                LEFT JOIN dbo.tblDepartment dept ON dept.DepartmentId = CLSF.EntryByDepartmentId
                                LEFT JOIN dbo.tblSalaryGrade Sgrd ON Sgrd.SalaryGradeId = CLSF.SalaryGradeId
                                LEFT JOIN dbo.tblDesignation Desg ON Desg.DesignationId = CLSF.DesignationId   
								
								left join tblDepWiseClearanceResourceUpdate on CLSF.EmployeeId=tblDepWiseClearanceResourceUpdate.EmpID    WHERE CLSF.EmployeeId = " + empId;

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetResourceInfo(int empId)
        {
            string queryStr = @"

SELECT distinct  ApprovalCondition,CASE 
        WHEN ApprovalCondition = 'as Supervisor' 
            THEN (CASE WHEN Recommend = '1' THEN 'Yes' ELSE 'No' END)
        ELSE '' 
    END AS Recommend,  MainRemarks, dgs.Designation, IsDoneEmpId, CASE
        WHEN NULLIF(LTRIM(RTRIM(Resource)), '') IS NULL
            THEN 'No pending issues'
        ELSE Resource
    END AS Resource, com.ShortName  Remarks, case when  tblDepWiseClearanceResourceUpdate.SetInfo='Dep' then tblDepartment.DepartmentName   else tblDivision.DivisionName end  DepartmentName ,emp.ImagePath ImagePath, IsDoneDate  from tblDepWiseClearanceResourceUpdate
								 left join tblDepartment on tblDepWiseClearanceResourceUpdate.DepID=tblDepartment.DepartmentId
								 	 left join tblDivision on tblDepWiseClearanceResourceUpdate.DepID=tblDivision.DivisionId
								 left  JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=dbo.tblDepWiseClearanceResourceUpdate.IsDoneEmpId
								 left  JOIN dbo.tblEmpGeneralInfo empMain ON empMain.EmpInfoId=dbo.tblDepWiseClearanceResourceUpdate.EmpID
								 left  JOIN dbo.tblCompanyInfo com ON com.CompanyId=empMain.CompanyId

								 left join tblEmpExitDetail ed on ed.ExitDetailId=tblDepWiseClearanceResourceUpdate.exitDetailIdNew

								  INNER JOIN dbo.tblDesignation dgs ON emp.DesignationId=dgs.DesignationId
								  where   ed.IsDone=1 and   EmpID= '" + empId + "'   ORDER BY ApprovalCondition DESC   ";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }


        public DataTable GetResourceInfoApprovalPerSOn(int empId)
        {
            string queryStr = @"SELECT emp.EmpMasterCode+' : '+emp.EmpName EmpName , tblEmpExitDetail.IsForwardEmpId, tblEmpExitDetail.EmpInfoId, dbo.tblEmpExitDetail.EmpInfoIdApproval FROM   tblEmpExitDetail
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = tblEmpExitDetail.IsForwardEmpId
 WHERE ExitDetailId='" + empId + "'  AND IsForward=1  ";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable GetResourceInfoforSuppervisor(int empId)
        {
            string queryStr = @" SELECT * FROM   tblEmpExitDetail
 
 WHERE MasterId='" + empId + "'  AND ApprovalStatus='as Supervisor'  AND IsDone=1  ";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }


        public DataTable SuppervisorNullChkDAL(int empId)
        {
            string queryStr = @" SELECT * FROM   tblEmpGeneralInfo  emp
 WHERE  emp.ReportingEmpId IS NOT NULL AND emp.EmpInfoId=  " + empId;

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable CheckClearence(int masterId, int exitDetailId, int id)
        {
            string queryStr = @"SELECT * FROM dbo.tblEmpExitDetail 
WHERE MasterId='" + masterId + "' AND ExitDetailId='" + exitDetailId + @"' 
  AND (
      EmpInfoIdApproval='" + id + @"'
      OR EmpInfoIdApproval IN (
          SELECT m.EmployeeId 
          FROM dbo.tblClearanceForwardingMaster m 
          INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
          WHERE d.SupervisedEmployeeId = '" + id + @"' 
            AND d.Status = 'Pending' 
            AND m.Status = 'Forwarded'
      )
  ) ";

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetEmpDDLByDepartMent(string comId, string dptID)
        {
            string queryStr = @"SELECT e.EmpInfoId,      ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName,  e.EmpInfoId
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId  WHERE  EmpMasterCode IS NOT  NULL AND e.IsActive=1  AND e.EmpInfoId NOT IN('" + HttpContext.Current.Session["EmpInfoId"].ToString() + "') AND e.CompanyId=" + comId + " AND e.DepartmentId=" + dptID;
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }
        public DataTable ResourceDataForDepartment(int empId, int id, string Condi,int  ExitMasterId, int ExitDetailId)
        {
            string queryStr = @"select  tblDepWiseClearanceResourceUpdate.Resource as Otherconsumption,tblDepWiseClearanceResourceUpdate.Remarks,tblDepWiseClearanceResourceUpdate.MainRemarks, Recommend  FROM tblDepWiseClearanceResourceUpdate 
            where EmpID=" + empId + " and exitMasterIdNew=" + ExitMasterId + " and exitDetailIdNew=" + ExitDetailId ;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable ResourceDataForDivision(int empId, int id, string Condi)
        {
            string queryStr = @"select  tblDepWiseClearanceResourceUpdate.Resource as Otherconsumption,tblDepWiseClearanceResourceUpdate.Remarks,tblDepWiseClearanceResourceUpdate.MainRemarks, Recommend  FROM tblDepWiseClearanceResourceUpdate 
            where EmpID=" + empId + " and DepID=" + id + " and ApprovalCondition='" + Condi + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public bool DeleteResourceDataForDepartment(int empId, int id, string Condition, int exitMasterId, int exitDetailId)
        {
            string queryStr = @"delete from tblDepWiseClearanceResourceUpdate 
           where exitMasterIdNew=" + exitMasterId + " and exitDetailIdNew=" + exitDetailId;

            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, DataBase.HRDB);
        }

        public bool SaveDocumentDetails(List<MiscellaneousInfoDocumentDAO> aList, int masterid)
        {
            try
            {
                List<SqlParameter> aParametersd = new List<SqlParameter>();
                aParametersd.Add(new SqlParameter("@ClearenceId", masterid));
                string queryDel = @"DELETE FROM [dbo].[tblClearFormDocument]
      WHERE  ClearenceId=@ClearenceId";

                bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@ClearenceId", masterid));
                    aParameters.Add(new SqlParameter("@DocumentLink", item.DocumentLink));
                    aParameters.Add(new SqlParameter("@DocumentNote", item.DocumentNote));
                    aParameters.Add(new SqlParameter("@FileName", item.FileName));




                    string query = @"INSERT INTO [dbo].[tblClearFormDocument]
           ([ClearenceId]
           ,[DocumentLink]
           ,[DocumentNote],FileName)
     VALUES
           (@ClearenceId 
           ,@DocumentLink
           ,@DocumentNote,@FileName)";
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

        // The clearance submit flow (ClearenceForm.aspx.cs / ClearenceFormEL.aspx.cs btn_Save_OnClick and
        // Button1_OnClick) calls SaveExitMasterInfo, SaveDocumentDetails, SaveClearenceDetail/2,
        // DeleteDataForClearance, SaveClearanceInfo, DeleteResourceDataForDepartment,
        // UpdateForwardtoOtherEmpExitDetail/UpdateForwardBackEmpExitDetail and UpdateEmpExitDetail one after
        // another. Each used to open and commit its own connection independently, so a failure partway
        // through (e.g. document save fails after the master row is already inserted) left the operation
        // half-done with no way to roll back. The overloads below run on a single caller-supplied
        // SqlConnection/SqlTransaction (obtained via OpenTransactionalConnection) so the whole submit
        // commits or rolls back as one unit, matching the pattern used for the BSC Appraisal save
        // (see BSCAppraisalFunctionalPartDAL.OpenTransactionalConnection).

        // DB_Connection.GenerateString is internal to the DAL assembly, so the code-behind (MainSite
        // assembly) can't open the connection itself - this opens it here and hands back the plain,
        // publicly-typed SqlConnection/SqlTransaction for the caller to pass into the overloads below and
        // commit/rollback itself.
        public SqlConnection OpenTransactionalConnection(out SqlTransaction transaction)
        {
            SqlConnection connection = new SqlConnection(DAL.MAIN_FUNCTION.DB_Connection.GenerateString(DataBase.H));
            connection.Open();
            transaction = connection.BeginTransaction();
            return connection;
        }

        // A SqlParameter can only belong to one SqlCommand.Parameters collection at a time, even after that
        // command is disposed, so callers whose parameter list could be reused get their own cloned copies.
        private static SqlParameter[] CloneParameters(List<SqlParameter> aSqlParameterlist)
        {
            SqlParameter[] clones = new SqlParameter[aSqlParameterlist.Count];
            for (int i = 0; i < aSqlParameterlist.Count; i++)
            {
                clones[i] = (SqlParameter)((ICloneable)aSqlParameterlist[i]).Clone();
            }
            return clones;
        }

        private static int ExecuteNonQueryTx(SqlConnection connection, SqlTransaction transaction, string queryString, List<SqlParameter> aSqlParameterlist)
        {
            using (SqlCommand command = new SqlCommand(queryString, connection, transaction))
            {
                if (aSqlParameterlist != null && aSqlParameterlist.Count > 0)
                {
                    command.Parameters.AddRange(CloneParameters(aSqlParameterlist));
                }
                return command.ExecuteNonQuery();
            }
        }

        private static int ExecuteScalarIntTx(SqlConnection connection, SqlTransaction transaction, string queryString, List<SqlParameter> aSqlParameterlist)
        {
            using (SqlCommand command = new SqlCommand(queryString, connection, transaction))
            {
                if (aSqlParameterlist != null && aSqlParameterlist.Count > 0)
                {
                    command.Parameters.AddRange(CloneParameters(aSqlParameterlist));
                }
                object result = command.ExecuteScalar();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        private static DataTable ExecuteDataTableTx(SqlConnection connection, SqlTransaction transaction, string queryString, List<SqlParameter> aSqlParameterlist)
        {
            using (SqlCommand command = new SqlCommand(queryString, connection, transaction))
            {
                if (aSqlParameterlist != null && aSqlParameterlist.Count > 0)
                {
                    command.Parameters.AddRange(CloneParameters(aSqlParameterlist));
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public bool DeleteDataForClearance(SqlConnection connection, SqlTransaction transaction, int empId)
        {
            string queryStr = @"Delete from tblEmpClearance
             where EmpInfoId=" + empId + " ";

            return ExecuteNonQueryTx(connection, transaction, queryStr, null) > 0;
        }

        public int SaveClearanceInfo(SqlConnection connection, SqlTransaction transaction, EmpClearanceDao aDao, Int32 MasterId, string EntryBy)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", (object)aDao.EmpInfoId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MobIssuDate", (object)aDao.MobIssuDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MobActualPrice", (object)aDao.MobActualPrice ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoDABeforeOne", (object)aDao.MoDABeforeOne ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoDABeforeOnetotwo", (object)aDao.MoDABeforeOnetotwo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoDAAboveTwo", (object)aDao.MoDAAboveTwo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MoRemarks", (object)aDao.MoRemarks ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DBIssuDate", (object)aDao.DBIssuDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DBActualPrice", (object)aDao.DBActualPrice ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DBDeductionAmount", (object)aDao.DBDeductionAmount ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DBRemark", (object)aDao.DBRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@PIDeductionAmount", (object)aDao.PIDeductionAmount ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PIActualCost", (object)aDao.PIActualCost ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PIRemark", (object)aDao.PIRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@MarketDues", (object)aDao.MarketDues ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MarketRemarks", (object)aDao.MarketRemarks ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@IDCard", (object)aDao.IDCard ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IDCardRemark", (object)aDao.IDCardRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@TotalDeductionAmount", (object)aDao.TotalDeductionAmount ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ToDeducAmtRemark", (object)aDao.ToDeducAmtRemark ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@EntryBy", EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@ClearenceId", MasterId));
            aSqlParameterlist.Add(new SqlParameter("@TickMark", aDao.TickMark ?? (object)DBNull.Value));

            string query = @"INSERT INTO [dbo].[tblEmpClearance]
           (
            [EmpInfoId]
           ,[MobIssuDate]
           ,[MobActualPrice]
           ,[MoDABeforeOne]
           ,[MoDABeforeOnetotwo]
           ,[MoDAAboveTwo]
           ,[MoRemarks]
           ,[DBIssuDate]
           ,[DBActualPrice]
           ,[DBDeductionAmount]
           ,[DBRemark]
           ,[PIActualCost]
           ,[PIDeductionAmount]
           ,[PIRemark]
           ,[IDCard]
           ,[IDCardRemark]
           ,[MarketDues]
           ,[MarketRemarks]
           ,[TotalDeductionAmount]
           ,[ToDeducAmtRemark]
           ,[EntryBy]
           ,[EntryDate]

           ,[TickMark]
           ,[ClearenceId])
     VALUES
           (
            @EmpInfoId,
            @MobIssuDate,
            @MobActualPrice,
            @MoDABeforeOne,
            @MoDABeforeOnetotwo,
            @MoDAAboveTwo,
           @MoRemarks,
           @DBIssuDate,
           @DBActualPrice,
           @DBDeductionAmount,
           @DBRemark,
           @PIActualCost,
           @PIDeductionAmount,
           @PIRemark,
           @IDCard,
           @IDCardRemark,
           @MarketDues,
           @MarketRemarks,
           @TotalDeductionAmount,
           @ToDeducAmtRemark,
           @EntryBy,
           GETDATE(),

           @TickMark,
           @ClearenceId
		   )  select scope_identity() as ID";

            return ExecuteScalarIntTx(connection, transaction, query, aSqlParameterlist);
        }

        public int SaveExitMasterInfo(SqlConnection connection, SqlTransaction transaction, ClearenceFormDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aDao.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", aDao.EmpName));
            aSqlParameterlist.Add(new SqlParameter("@DesignationId", aDao.DesignationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aDao.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aDao.SalaryGradeId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aDao.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@Description", aDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeJobLeftId", aDao.EmployeeJobLeftId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@EntryByPersonId", aDao.EntryByPersonId));
            aSqlParameterlist.Add(new SqlParameter("@Recommend", aDao.Recommend));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aDao.SalaryLoationId ?? (object)DBNull.Value));

            string query = @"INSERT INTO dbo.tblEmpClearenceForm
                            (
                                CompanyId,
                                EmployeeId,
                                EmpCode,
                                EmpName,
                                JoiningDate,
                                DivisionId,
                                DesignationId,
                                SalaryGradeId,
                                Description,
                                ActionStatus,
                                EntryBy,
                                EntryDate,
                                EmployeeJobLeftId,
                                EntryByDepartmentId,
                                EntryByPersonId,Recommend, Remarks,SalaryLoationId
                            )
                            VALUES
                            (
                                @CompanyId,
                                @EmployeeId,
                                @EmpCode,
                                @EmpName,
                                @JoiningDate,
                                @DivisionId,
                                @DesignationId,
                                @SalaryGradeId,
                                @Description,
                                @ActionStatus,
                                @EntryBy,
                                @EntryDate,
                                @EmployeeJobLeftId,
                                @DepartmentId,
                                @EntryByPersonId,@Recommend, @Remarks,@SalaryLoationId

                            )  select scope_identity() as ID";

            return ExecuteScalarIntTx(connection, transaction, query, aSqlParameterlist);
        }

        public Int32 SaveClearenceDetail(SqlConnection connection, SqlTransaction transaction, List<ClearenceFormDetailDao> aCreationLocationDaos)
        {
            Int32 id = 0;

            foreach (var aData in aCreationLocationDaos)
            {
                id = SaveClearenceDetailInfo(connection, transaction, aData);
            }

            return id;
        }

        public Int32 SaveClearenceDetail2(SqlConnection connection, SqlTransaction transaction, List<ClearenceFormDetailDao> aCreationLocationDaos)
        {
            Int32 id = 0;

            foreach (var aData in aCreationLocationDaos)
            {
                id = SaveClearenceDetailInfo2(connection, transaction, aData);
            }

            return id;
        }

        private Int32 SaveClearenceDetailInfo2(SqlConnection connection, SqlTransaction transaction, ClearenceFormDetailDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DepID", aDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@EmpID", aDao.EmpID));
            aSqlParameterlist.Add(new SqlParameter("@Resource", aDao.Resource));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDao.Remarks));

            aSqlParameterlist.Add(new SqlParameter("@IsDoneEmpId", aDao.IsDoneEmpId));
            aSqlParameterlist.Add(new SqlParameter("@IsDoneDate", aDao.IsDoneDate));
            aSqlParameterlist.Add(new SqlParameter("@MainRemarks", aDao.MainRemarks));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalCondition", aDao.ApprovalCondition));
            aSqlParameterlist.Add(new SqlParameter("@SetInfo", aDao.SetInfo));
            aSqlParameterlist.Add(new SqlParameter("@Recommend", aDao.Recommend ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@exitDetailIdNew", aDao.exitDetailIdNew ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@exitMasterIdNew", aDao.exitMasterIdNew ?? (object)DBNull.Value));

            string query = @"INSERT INTO dbo.tblDepWiseClearanceResourceUpdate
                            (
                                DepID,
                                EmpID,
                                Resource,
                                Remarks,IsDoneEmpId,IsDoneDate,MainRemarks,Recommend,ApprovalCondition, SetInfo,exitMasterIdNew,exitDetailIdNew
                            )
                            VALUES
                            (
                                @DepID,
                                @EmpID,
                                @Resource ,
                                @Remarks,@IsDoneEmpId,@IsDoneDate ,@MainRemarks,@Recommend ,@ApprovalCondition ,@SetInfo,@exitMasterIdNew,@exitDetailIdNew
                              )  select scope_identity() as ID";

            return ExecuteScalarIntTx(connection, transaction, query, aSqlParameterlist);
        }

        private Int32 SaveClearenceDetailInfo(SqlConnection connection, SqlTransaction transaction, ClearenceFormDetailDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterId", aDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@Resource", aDao.Resource));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aDao.Remarks));

            string query = @"INSERT INTO dbo.tblClearenceDetail
                            (
                                MasterId,
                                Resource,
                                Remarks
                            )
                            VALUES
                            (
                                @MasterId,
                                @Resource,
                                @Remarks

                            )  select scope_identity() as ID";

            return ExecuteScalarIntTx(connection, transaction, query, aSqlParameterlist);
        }

        public bool UpdateEmpExitDetail(SqlConnection connection, SqlTransaction transaction, int masterId, int empId, int ExitDetailId)
        {
            string queryStr = @"
UPDATE [dbo].[tblEmpExitDetail]
   SET [IsDone] = 1, isFinalDoneDate = GETDATE()
 WHERE  MasterId=" + masterId + " and ExitDetailId=" + ExitDetailId;
            return ExecuteNonQueryTx(connection, transaction, queryStr, null) > 0;
        }

        public bool UpdateForwardtoOtherEmpExitDetail(SqlConnection connection, SqlTransaction transaction, int masterId, int empId, int AppPer, int ExitDetailId, string ForwardRemarks)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ForwardRemarks", ForwardRemarks ?? string.Empty));

            string queryStr = @"
UPDATE [dbo].[tblEmpExitDetail]
   SET EmpInfoIdApproval =" + AppPer + @"  , ForwardRemarks =@ForwardRemarks, IsForward=0 ,IsForwardEmpId =" + AppPer + @", IsForwardDate = GETDATE(), isForwardBackDone = 0, isForwardBackDoneDate = NULL
 WHERE  ExitDetailId=" + ExitDetailId;
            return ExecuteNonQueryTx(connection, transaction, queryStr, aSqlParameterlist) > 0;
        }

        public bool UpdateForwardBackEmpExitDetail(SqlConnection connection, SqlTransaction transaction, int masterId, int empId, int AppPer, int ExitDetailId, string ForwardRemarks)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ForwardRemarks", ForwardRemarks ?? string.Empty));
            aSqlParameterlist.Add(new SqlParameter("@ForwardedByEmpId", AppPer));

            string queryStr = @"
UPDATE [dbo].[tblEmpExitDetail]
   SET EmpInfoIdApproval = EmpInfoId, 
       ForwardRemarks = @ForwardRemarks, 
       IsForward = 1, 
       isForwardBackDone = 1, 
       isForwardBackDoneDate = GETDATE(),
       IsForwardEmpId = ISNULL(IsForwardEmpId, @ForwardedByEmpId)
 WHERE  ExitDetailId=" + ExitDetailId;
            return ExecuteNonQueryTx(connection, transaction, queryStr, aSqlParameterlist) > 0;
        }

        public bool IsClearanceAutoForwardRecipient(SqlConnection connection, SqlTransaction transaction, int approverEmpId, int userEmpId)
        {
            string queryStr = @"
SELECT TOP 1 1 
FROM dbo.tblClearanceForwardingMaster m
INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId
WHERE m.EmployeeId = " + approverEmpId + @"
  AND d.SupervisedEmployeeId = " + userEmpId + @"
  AND m.Status = 'Forwarded'
  AND d.Status = 'Pending'";

            DataTable dt = ExecuteDataTableTx(connection, transaction, queryStr, null);
            return dt != null && dt.Rows.Count > 0;
        }

        public DataTable CheckClearence(SqlConnection connection, SqlTransaction transaction, int masterId, int exitDetailId, int id)
        {
            string queryStr = @"SELECT * FROM dbo.tblEmpExitDetail 
WHERE MasterId='" + masterId + "' AND ExitDetailId='" + exitDetailId + @"' 
  AND (
      EmpInfoIdApproval='" + id + @"'
      OR EmpInfoIdApproval IN (
          SELECT m.EmployeeId 
          FROM dbo.tblClearanceForwardingMaster m 
          INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
          WHERE d.SupervisedEmployeeId = '" + id + @"' 
            AND d.Status = 'Pending' 
            AND m.Status = 'Forwarded'
      )
  ) ";

            return ExecuteDataTableTx(connection, transaction, queryStr, null);
        }

        public bool DeleteResourceDataForDepartment(SqlConnection connection, SqlTransaction transaction, int empId, int id, string Condition, int exitMasterId, int exitDetailId)
        {
            string queryStr = @"delete from tblDepWiseClearanceResourceUpdate
           where exitMasterIdNew=" + exitMasterId + " and exitDetailIdNew=" + exitDetailId;

            return ExecuteNonQueryTx(connection, transaction, queryStr, null) > 0;
        }

        public bool SaveDocumentDetails(SqlConnection connection, SqlTransaction transaction, List<MiscellaneousInfoDocumentDAO> aList, int masterid)
        {
            List<SqlParameter> aParametersd = new List<SqlParameter>();
            aParametersd.Add(new SqlParameter("@ClearenceId", masterid));
            string queryDel = @"DELETE FROM [dbo].[tblClearFormDocument]
      WHERE  ClearenceId=@ClearenceId";

            ExecuteNonQueryTx(connection, transaction, queryDel, aParametersd);

            bool result = false;
            foreach (var item in aList)
            {
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ClearenceId", masterid));
                aParameters.Add(new SqlParameter("@DocumentLink", item.DocumentLink));
                aParameters.Add(new SqlParameter("@DocumentNote", item.DocumentNote));
                aParameters.Add(new SqlParameter("@FileName", item.FileName));

                string query = @"INSERT INTO [dbo].[tblClearFormDocument]
           ([ClearenceId]
           ,[DocumentLink]
           ,[DocumentNote],FileName)
     VALUES
           (@ClearenceId
           ,@DocumentLink
           ,@DocumentNote,@FileName)";
                result = ExecuteNonQueryTx(connection, transaction, query, aParameters) > 0;

                if (result == false)
                {
                    return false;
                }
            }
            return result;
        }
    }

}
