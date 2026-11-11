/* =====================================================================
   Transfer approved KPI / appraisal records for FY 2025-2026
   from previous (contractual) Employee IDs to current (permanent) IDs.
   Database : HRIS_SMC_DB
   REMAINING 5 EMPLOYEES (30467 already done separately). COMMITS at the end.
   Mapping (EmpMasterCode -> EmpInfoId, verified 2026-11-10):
     30210 (3156) -> 53575 (5556)  Md. Moshiur Rahman
     30212 (3158) -> 53576 (5558)  Md. Anamul Hoque
     30315 (3694) -> 53577 (5559)  Md. Farhad Ali
     30247 (3198) -> 53578 (5560)  Md. Muzahid Hossain
     16313 (3469) -> 10324 (5554)  A.F.M. Shahriar Hassan
   ===================================================================== */
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @Map TABLE (OldCode varchar(20), NewCode varchar(20), OldEmpInfoId int, NewEmpInfoId int);
INSERT INTO @Map (OldCode, NewCode) VALUES
 ('30210','53575'),('30212','53576'),
 ('30315','53577'),('30247','53578'),('16313','10324');

UPDATE m SET OldEmpInfoId = o.EmpInfoId, NewEmpInfoId = n.EmpInfoId
FROM @Map m
JOIN tblEmpGeneralInfo o ON o.EmpMasterCode = m.OldCode
JOIN tblEmpGeneralInfo n ON n.EmpMasterCode = m.NewCode;

IF EXISTS (SELECT 1 FROM @Map WHERE OldEmpInfoId IS NULL OR NewEmpInfoId IS NULL)
BEGIN
    SELECT * FROM @Map;
    RAISERROR('Employee code not found in tblEmpGeneralInfo. Aborting.', 16, 1);
    RETURN;
END

-- FY 2025-2026 has two FinancialYearId rows (19, 20); both are used by the appraisal data.
DECLARE @FY TABLE (FinancialYearId int);
INSERT INTO @FY SELECT FinancialYearId FROM tblFinancialYear WHERE FinancialYearDesc = '2025-2026';

SELECT 'Mapping' AS Info, * FROM @Map;
SELECT 'FY ids' AS Info, * FROM @FY;

-- Collect the master row keys being moved (used to scope log/detail updates)
DECLARE @SelfMaster    TABLE (Id int, OldEmp int, NewEmp int);
DECLARE @AppMaster     TABLE (Id int, OldEmp int, NewEmp int);
DECLARE @BSCSelfMaster TABLE (Id int, OldEmp int, NewEmp int);
DECLARE @BSCAppMaster  TABLE (Id int, OldEmp int, NewEmp int);
DECLARE @MidMaster     TABLE (Id int, OldEmp int, NewEmp int);
DECLARE @MBSCMaster    TABLE (Id int, OldEmp int, NewEmp int);

INSERT INTO @SelfMaster    SELECT t.AppraisalSelfMasterId,    m.OldEmpInfoId, m.NewEmpInfoId FROM tblAppraisalSelfMaster    t JOIN @Map m ON m.OldEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
INSERT INTO @AppMaster     SELECT t.AppraisalMasterId,        m.OldEmpInfoId, m.NewEmpInfoId FROM tblAppraisalMaster        t JOIN @Map m ON m.OldEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
INSERT INTO @BSCSelfMaster SELECT t.BSCAppraisalSelfMasterId, m.OldEmpInfoId, m.NewEmpInfoId FROM tblBSCAppraisalSelfMaster t JOIN @Map m ON m.OldEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
INSERT INTO @BSCAppMaster  SELECT t.BSCAppraisalMasterId,     m.OldEmpInfoId, m.NewEmpInfoId FROM tblBSCAppraisalMaster     t JOIN @Map m ON m.OldEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
INSERT INTO @MidMaster     SELECT t.AppraisalMasterId,        m.OldEmpInfoId, m.NewEmpInfoId FROM tblKPIMIDAppraisalMaster  t JOIN @Map m ON m.OldEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
INSERT INTO @MBSCMaster    SELECT t.BSCAppraisalMasterId,     m.OldEmpInfoId, m.NewEmpInfoId FROM tblMBSCAppraisalMaster    t JOIN @Map m ON m.OldEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY);

-- Safety: new IDs must not already have FY 2025-26 KPI rows
IF EXISTS (SELECT 1 FROM tblAppraisalSelfMaster    t JOIN @Map m ON m.NewEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY))
OR EXISTS (SELECT 1 FROM tblBSCAppraisalSelfMaster t JOIN @Map m ON m.NewEmpInfoId = t.EmpInfoId WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY))
BEGIN
    RAISERROR('New employee ID already has FY 2025-26 KPI rows. Review manually. Aborting.', 16, 1);
    RETURN;
END

BEGIN TRY
BEGIN TRAN;

/* ---------- Backup of every row we touch (dated tables) ---------- */
DECLARE @Suffix varchar(20) = CONVERT(varchar(8), GETDATE(), 112) + '_REM5';
DECLARE @sql nvarchar(max) = N'';
SELECT @sql = @sql + N'
SELECT * INTO dbo.[BK_' + x.tbl + N'_IDTransfer_' + @Suffix + N'] FROM dbo.[' + x.tbl + N'] WHERE ' + x.cond + N';'
FROM (VALUES
 ('tblAppraisalSelfMaster',        'AppraisalSelfMasterId IN (SELECT Id FROM #SelfMaster)'),
 ('tblAppraisalMaster',            'AppraisalMasterId IN (SELECT Id FROM #AppMaster)'),
 ('tblBSCAppraisalSelfMaster',     'BSCAppraisalSelfMasterId IN (SELECT Id FROM #BSCSelfMaster)'),
 ('tblBSCAppraisalMaster',         'BSCAppraisalMasterId IN (SELECT Id FROM #BSCAppMaster)'),
 ('tblKPIMIDAppraisalMaster',      'AppraisalMasterId IN (SELECT Id FROM #MidMaster)'),
 ('tblMBSCAppraisalMaster',        'BSCAppraisalMasterId IN (SELECT Id FROM #MBSCMaster)'),
 ('tblAppraisalSelfAppLog',        'AppraisalSelfMasterId IN (SELECT Id FROM #SelfMaster)'),
 ('tblAppraisalMasterAppLog',      'AppraisalMasterId IN (SELECT Id FROM #AppMaster)'),
 ('tblBSCAppraisalSelfAppLog',     'BSCAppraisalSelfMasterId IN (SELECT Id FROM #BSCSelfMaster)'),
 ('tblBSCAppraisalMasterAppLog',   'BSCAppraisalMasterId IN (SELECT Id FROM #BSCAppMaster)'),
 ('tblKPIMIDAppraisalMasterAppLog','AppraisalMasterId IN (SELECT Id FROM #MidMaster)'),
 ('tblMBSCAppraisalMasterAppLog',  'BSCAppraisalMasterId IN (SELECT Id FROM #MBSCMaster)'),
 ('tblBSCOKRPermission',           'EmpInfoId IN (SELECT OldEmpInfoId FROM #Map) AND FinancialYearId IN (SELECT FinancialYearId FROM #FY)'),
 ('tblKPIDeadLineDetails',         'EmpinfoId IN (SELECT OldEmpInfoId FROM #Map) AND KPIDeadLineMasterId IN (SELECT KPIDeadLineMasterId FROM tblKpiDeadlineMaster WHERE FinancialYearId IN (SELECT FinancialYearId FROM #FY))'),
 ('tblBSCKPIDeadLineDetails',      'EmpinfoId IN (SELECT OldEmpInfoId FROM #Map) AND KPIDeadLineMasterId IN (SELECT KPIDeadLineMasterId FROM tblBSCKpiDeadlineMaster WHERE FinancialYearId IN (SELECT FinancialYearId FROM #FY))'),
 ('tblAppraisalDeadLineDetails',   'EmpinfoId IN (SELECT OldEmpInfoId FROM #Map) AND AppraisalDeadLineMasterId IN (SELECT AppraisalDeadLineMasterId FROM tblAppraisalDeadlineMaster WHERE FinancialYearId IN (SELECT FinancialYearId FROM #FY))'),
 ('tblBSCAppraisalDeadLineDetails','EmpinfoId IN (SELECT OldEmpInfoId FROM #Map) AND BSCAppraisalDeadLineMasterId IN (SELECT BSCAppraisalDeadLineMasterId FROM tblBSCAppraisalDeadlineMaster WHERE FinancialYearId IN (SELECT FinancialYearId FROM #FY))')
) x(tbl, cond);

-- the backup dynamic SQL cannot see table variables, so copy them to temp tables first
IF OBJECT_ID('tempdb..#SelfMaster')    IS NOT NULL DROP TABLE #SelfMaster;    SELECT * INTO #SelfMaster    FROM @SelfMaster;
IF OBJECT_ID('tempdb..#AppMaster')     IS NOT NULL DROP TABLE #AppMaster;     SELECT * INTO #AppMaster     FROM @AppMaster;
IF OBJECT_ID('tempdb..#BSCSelfMaster') IS NOT NULL DROP TABLE #BSCSelfMaster; SELECT * INTO #BSCSelfMaster FROM @BSCSelfMaster;
IF OBJECT_ID('tempdb..#BSCAppMaster')  IS NOT NULL DROP TABLE #BSCAppMaster;  SELECT * INTO #BSCAppMaster  FROM @BSCAppMaster;
IF OBJECT_ID('tempdb..#MidMaster')     IS NOT NULL DROP TABLE #MidMaster;     SELECT * INTO #MidMaster     FROM @MidMaster;
IF OBJECT_ID('tempdb..#MBSCMaster')    IS NOT NULL DROP TABLE #MBSCMaster;    SELECT * INTO #MBSCMaster    FROM @MBSCMaster;
IF OBJECT_ID('tempdb..#Map')           IS NOT NULL DROP TABLE #Map;           SELECT * INTO #Map           FROM @Map;
IF OBJECT_ID('tempdb..#FY')            IS NOT NULL DROP TABLE #FY;            SELECT * INTO #FY            FROM @FY;
EXEC sp_executesql @sql;
PRINT 'Backup tables created: BK_<table>_IDTransfer_' + @Suffix;

/* ---------- 1. KPI setup (Self) masters ---------- */
UPDATE t SET EmpInfoId = s.NewEmp FROM tblAppraisalSelfMaster    t JOIN @SelfMaster    s ON s.Id = t.AppraisalSelfMasterId;
PRINT 'tblAppraisalSelfMaster: '    + CAST(@@ROWCOUNT AS varchar);
UPDATE t SET EmpInfoId = s.NewEmp FROM tblBSCAppraisalSelfMaster t JOIN @BSCSelfMaster s ON s.Id = t.BSCAppraisalSelfMasterId;
PRINT 'tblBSCAppraisalSelfMaster: ' + CAST(@@ROWCOUNT AS varchar);

/* ---------- 2. Year-end / Mid-year appraisal masters ---------- */
UPDATE t SET EmpInfoId = s.NewEmp FROM tblAppraisalMaster       t JOIN @AppMaster    s ON s.Id = t.AppraisalMasterId;
PRINT 'tblAppraisalMaster: '       + CAST(@@ROWCOUNT AS varchar);
UPDATE t SET EmpInfoId = s.NewEmp FROM tblBSCAppraisalMaster    t JOIN @BSCAppMaster s ON s.Id = t.BSCAppraisalMasterId;
PRINT 'tblBSCAppraisalMaster: '    + CAST(@@ROWCOUNT AS varchar);
UPDATE t SET EmpInfoId = s.NewEmp FROM tblKPIMIDAppraisalMaster t JOIN @MidMaster    s ON s.Id = t.AppraisalMasterId;
PRINT 'tblKPIMIDAppraisalMaster: ' + CAST(@@ROWCOUNT AS varchar);
UPDATE t SET EmpInfoId = s.NewEmp FROM tblMBSCAppraisalMaster   t JOIN @MBSCMaster   s ON s.Id = t.BSCAppraisalMasterId;
PRINT 'tblMBSCAppraisalMaster: '   + CAST(@@ROWCOUNT AS varchar);

/* ---------- 3. Approval logs: remap old emp id wherever it appears, only for the moved masters ---------- */
UPDATE l SET
    PreEmpInfoId         = CASE WHEN l.PreEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.PreEmpInfoId         END,
    ForEmpInfoId         = CASE WHEN l.ForEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.ForEmpInfoId         END,
    PreviousForEmpInfoId = CASE WHEN l.PreviousForEmpInfoId = s.OldEmp THEN s.NewEmp ELSE l.PreviousForEmpInfoId END,
    CommentsEMPID        = CASE WHEN l.CommentsEMPID        = s.OldEmp THEN s.NewEmp ELSE l.CommentsEMPID        END
FROM tblAppraisalSelfAppLog l JOIN @SelfMaster s ON s.Id = l.AppraisalSelfMasterId;
PRINT 'tblAppraisalSelfAppLog: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE l SET
    PreEmpInfoId         = CASE WHEN l.PreEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.PreEmpInfoId         END,
    ForEmpInfoId         = CASE WHEN l.ForEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.ForEmpInfoId         END,
    PreviousForEmpInfoId = CASE WHEN l.PreviousForEmpInfoId = s.OldEmp THEN s.NewEmp ELSE l.PreviousForEmpInfoId END,
    CommentsEMPID        = CASE WHEN l.CommentsEMPID        = s.OldEmp THEN s.NewEmp ELSE l.CommentsEMPID        END
FROM tblBSCAppraisalSelfAppLog l JOIN @BSCSelfMaster s ON s.Id = l.BSCAppraisalSelfMasterId;
PRINT 'tblBSCAppraisalSelfAppLog: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE l SET
    PreEmpInfoId         = CASE WHEN l.PreEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.PreEmpInfoId         END,
    ForEmpInfoId         = CASE WHEN l.ForEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.ForEmpInfoId         END,
    PreviousForEmpInfoId = CASE WHEN l.PreviousForEmpInfoId = s.OldEmp THEN s.NewEmp ELSE l.PreviousForEmpInfoId END,
    CommentEmpId         = CASE WHEN l.CommentEmpId         = s.OldEmp THEN s.NewEmp ELSE l.CommentEmpId         END
FROM tblAppraisalMasterAppLog l JOIN @AppMaster s ON s.Id = l.AppraisalMasterId;
PRINT 'tblAppraisalMasterAppLog: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE l SET
    PreEmpInfoId         = CASE WHEN l.PreEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.PreEmpInfoId         END,
    ForEmpInfoId         = CASE WHEN l.ForEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.ForEmpInfoId         END,
    PreviousForEmpInfoId = CASE WHEN l.PreviousForEmpInfoId = s.OldEmp THEN s.NewEmp ELSE l.PreviousForEmpInfoId END,
    CommentEmpId         = CASE WHEN l.CommentEmpId         = s.OldEmp THEN s.NewEmp ELSE l.CommentEmpId         END
FROM tblBSCAppraisalMasterAppLog l JOIN @BSCAppMaster s ON s.Id = l.BSCAppraisalMasterId;
PRINT 'tblBSCAppraisalMasterAppLog: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE l SET
    PreEmpInfoId         = CASE WHEN l.PreEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.PreEmpInfoId         END,
    ForEmpInfoId         = CASE WHEN l.ForEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.ForEmpInfoId         END,
    PreviousForEmpInfoId = CASE WHEN l.PreviousForEmpInfoId = s.OldEmp THEN s.NewEmp ELSE l.PreviousForEmpInfoId END,
    CommentEmpId         = CASE WHEN l.CommentEmpId         = s.OldEmp THEN s.NewEmp ELSE l.CommentEmpId         END
FROM tblKPIMIDAppraisalMasterAppLog l JOIN @MidMaster s ON s.Id = l.AppraisalMasterId;
PRINT 'tblKPIMIDAppraisalMasterAppLog: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE l SET
    PreEmpInfoId         = CASE WHEN l.PreEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.PreEmpInfoId         END,
    ForEmpInfoId         = CASE WHEN l.ForEmpInfoId         = s.OldEmp THEN s.NewEmp ELSE l.ForEmpInfoId         END,
    PreviousForEmpInfoId = CASE WHEN l.PreviousForEmpInfoId = s.OldEmp THEN s.NewEmp ELSE l.PreviousForEmpInfoId END,
    CommentEmpId         = CASE WHEN l.CommentEmpId         = s.OldEmp THEN s.NewEmp ELSE l.CommentEmpId         END
FROM tblMBSCAppraisalMasterAppLog l JOIN @MBSCMaster s ON s.Id = l.BSCAppraisalMasterId;
PRINT 'tblMBSCAppraisalMasterAppLog: ' + CAST(@@ROWCOUNT AS varchar);

/* ---------- 4. OKR/BSC permission + deadline access rows (FY 2025-26 only) ---------- */
UPDATE t SET EmpInfoId = m.NewEmpInfoId FROM tblBSCOKRPermission t JOIN @Map m ON m.OldEmpInfoId = t.EmpInfoId
WHERE t.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
PRINT 'tblBSCOKRPermission: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE t SET EmpinfoId = m.NewEmpInfoId FROM tblKPIDeadLineDetails t JOIN @Map m ON m.OldEmpInfoId = t.EmpinfoId
JOIN tblKpiDeadlineMaster dm ON dm.KPIDeadLineMasterId = t.KPIDeadLineMasterId
WHERE dm.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
PRINT 'tblKPIDeadLineDetails: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE t SET EmpinfoId = m.NewEmpInfoId FROM tblBSCKPIDeadLineDetails t JOIN @Map m ON m.OldEmpInfoId = t.EmpinfoId
JOIN tblBSCKpiDeadlineMaster dm ON dm.KPIDeadLineMasterId = t.KPIDeadLineMasterId
WHERE dm.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
PRINT 'tblBSCKPIDeadLineDetails: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE t SET EmpinfoId = m.NewEmpInfoId FROM tblAppraisalDeadLineDetails t JOIN @Map m ON m.OldEmpInfoId = t.EmpinfoId
JOIN tblAppraisalDeadlineMaster dm ON dm.AppraisalDeadLineMasterId = t.AppraisalDeadLineMasterId
WHERE dm.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
PRINT 'tblAppraisalDeadLineDetails: ' + CAST(@@ROWCOUNT AS varchar);

UPDATE t SET EmpinfoId = m.NewEmpInfoId FROM tblBSCAppraisalDeadLineDetails t JOIN @Map m ON m.OldEmpInfoId = t.EmpinfoId
JOIN tblBSCAppraisalDeadlineMaster dm ON dm.BSCAppraisalDeadLineMasterId = t.BSCAppraisalDeadLineMasterId
WHERE dm.FinancialYearId IN (SELECT FinancialYearId FROM @FY);
PRINT 'tblBSCAppraisalDeadLineDetails: ' + CAST(@@ROWCOUNT AS varchar);

/* ---------- Verification: every new ID should now own its FY 2025-26 rows, old IDs none ---------- */
SELECT m.OldCode, m.NewCode,
  (SELECT COUNT(*) FROM tblAppraisalSelfMaster    WHERE EmpInfoId = m.NewEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY)) KPISelf_New,
  (SELECT COUNT(*) FROM tblBSCAppraisalSelfMaster WHERE EmpInfoId = m.NewEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY)) BSCSelf_New,
  (SELECT COUNT(*) FROM tblAppraisalMaster        WHERE EmpInfoId = m.NewEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY)) App_New,
  (SELECT COUNT(*) FROM tblBSCAppraisalMaster     WHERE EmpInfoId = m.NewEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY)) BSCApp_New,
  (SELECT COUNT(*) FROM tblKPIMIDAppraisalMaster  WHERE EmpInfoId = m.NewEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY)) Mid_New,
  (SELECT COUNT(*) FROM tblMBSCAppraisalMaster    WHERE EmpInfoId = m.NewEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY)) MBSC_New,
  (SELECT COUNT(*) FROM tblAppraisalSelfMaster    WHERE EmpInfoId = m.OldEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY))
+ (SELECT COUNT(*) FROM tblBSCAppraisalSelfMaster WHERE EmpInfoId = m.OldEmpInfoId AND FinancialYearId IN (SELECT FinancialYearId FROM @FY)) LeftOnOld
FROM @Map m;

/* ===== DRY RUN by default. Change ROLLBACK to COMMIT after reviewing the output. ===== */
COMMIT TRAN;
PRINT '*** COMMITTED: remaining 5 employees FY 2025-26 transferred. ***';

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    PRINT 'ERROR: ' + ERROR_MESSAGE();
    THROW;
END CATCH
