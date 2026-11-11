/* ==========================================================================
   ROLLBACK for Graded_Limit_Availed_Entry*.sql
   Database : HRIS_SMC_DB

   Deletes ONLY the forms created by the Graded Limit script. A form is deleted only if ALL are true:
     - tbl_ReimbursmentFormMaster_HealthCare.RequitisionNo LIKE '%-MIG-%'   (OPD-MIG-<code> / IPD-MIG-<code>)
     - IgMiGrate = 1
     - its claim row has SINoOfEncloseVoucher = 'Opening availed balance (Graded Limit Update)'
     - it is NOT referenced by a top sheet (TopSheetGenerateDetails_H)  -> reported, not deleted
   Deletes, in order: tblReimbursementSelfAppLog -> tbl_ReimbursmentformClaimDetails_HC -> master.

   HOW TO RUN
     1) Run as-is (@Commit = 0): shows exactly what would be deleted, then rolls back.
        Save the "BACKUP" result grids (right-click > Save Results As) if you want a copy.
     2) Set @Commit = 1 and run again to delete.
   ========================================================================== */
USE [HRIS_SMC_DB];
GO
SET NOCOUNT ON;

DECLARE @Commit BIT = 0;     -- 0 = preview & rollback, 1 = delete

IF OBJECT_ID('tempdb..#Del') IS NOT NULL DROP TABLE #Del;
SELECT m.ReimbursFromMasterId AS Id
INTO #Del
FROM dbo.tbl_ReimbursmentFormMaster_HealthCare m
WHERE m.RequitisionNo LIKE '%-MIG-%'
  AND m.IgMiGrate = 1
  AND EXISTS (SELECT 1 FROM dbo.tbl_ReimbursmentformClaimDetails_HC cd
              WHERE cd.ReimbursFromMasterId = m.ReimbursFromMasterId
                AND cd.SINoOfEncloseVoucher = 'Opening availed balance (Graded Limit Update)')
  AND NOT EXISTS (SELECT 1 FROM dbo.TopSheetGenerateDetails_H t WHERE t.ReimbursFromMasterId = m.ReimbursFromMasterId);

PRINT '=== Forms that will be deleted ===';
SELECT m.[Type], COUNT(*) Forms, SUM(m.ClaimTKByUser) TotalAmount
FROM dbo.tbl_ReimbursmentFormMaster_HealthCare m JOIN #Del d ON d.Id = m.ReimbursFromMasterId
GROUP BY m.[Type];

PRINT '=== Skipped (tagged -MIG- but used in a top sheet, NOT deleted) ===';
SELECT m.ReimbursFromMasterId, m.RequitisionNo, m.EmpMasterCode, m.ClaimTKByUser
FROM dbo.tbl_ReimbursmentFormMaster_HealthCare m
WHERE m.RequitisionNo LIKE '%-MIG-%' AND m.IgMiGrate = 1
  AND NOT EXISTS (SELECT 1 FROM #Del d WHERE d.Id = m.ReimbursFromMasterId);

PRINT '=== BACKUP 1/3: master ===';
SELECT m.* FROM dbo.tbl_ReimbursmentFormMaster_HealthCare m JOIN #Del d ON d.Id = m.ReimbursFromMasterId ORDER BY m.ReimbursFromMasterId;
PRINT '=== BACKUP 2/3: claims ===';
SELECT cd.* FROM dbo.tbl_ReimbursmentformClaimDetails_HC cd JOIN #Del d ON d.Id = cd.ReimbursFromMasterId ORDER BY cd.ClaimDetailsId;
PRINT '=== BACKUP 3/3: logs ===';
SELECT l.* FROM dbo.tblReimbursementSelfAppLog l JOIN #Del d ON d.Id = l.ReimbursFromMasterId ORDER BY l.ReimbursementSelfAppLogId;

BEGIN TRY
    BEGIN TRAN;

    DELETE l  FROM dbo.tblReimbursementSelfAppLog          l  JOIN #Del d ON d.Id = l.ReimbursFromMasterId;  PRINT 'logs deleted   : ' + CAST(@@ROWCOUNT AS VARCHAR(10));
    DELETE cd FROM dbo.tbl_ReimbursmentformClaimDetails_HC cd JOIN #Del d ON d.Id = cd.ReimbursFromMasterId; PRINT 'claims deleted : ' + CAST(@@ROWCOUNT AS VARCHAR(10));
    DELETE m  FROM dbo.tbl_ReimbursmentFormMaster_HealthCare m JOIN #Del d ON d.Id = m.ReimbursFromMasterId; PRINT 'forms deleted  : ' + CAST(@@ROWCOUNT AS VARCHAR(10));

    IF @Commit = 1
    BEGIN
        COMMIT TRAN;
        PRINT 'COMMITTED - rollback of Graded Limit data done.';
    END
    ELSE
    BEGIN
        ROLLBACK TRAN;
        PRINT 'PREVIEW ONLY - nothing deleted. Set @Commit = 1 to delete.';
    END
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    DECLARE @Msg NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('Rollback failed: %s', 16, 1, @Msg);
END CATCH

PRINT '=== Remaining -MIG- forms ===';
SELECT COUNT(*) AS MigFormsRemaining FROM dbo.tbl_ReimbursmentFormMaster_HealthCare WHERE RequitisionNo LIKE '%-MIG-%';

DROP TABLE #Del;
GO
