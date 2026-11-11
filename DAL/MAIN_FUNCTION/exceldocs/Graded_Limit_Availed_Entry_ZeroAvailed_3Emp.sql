/* ==========================================================================
   Graded Limit Update - Opening entry for the 3 employees with Excel Availed = 0
   Database : HRIS_SMC_DB
   Employees: OPD 50025 (Ershad Rashid), 50044 (Abdul Mannan), 50269 (Alamgir Hossain)
              Excel: Balance 12000 | Availed 0 | Remaining 12000

   Inserts ONE approved form per employee with Amount = Excel Availed (0) - 20000 = -20000
   (@Adjust below), same structure as
   Graded_Limit_Availed_Entry_FullAmount.sql (master + claim row + 'Approved' log, tagged OPD-MIG-<code>).
   NEGATIVE amount: page Availed becomes -20000 and Remaining 32000 for these employees.
   Rolled back by Graded_Limit_Availed_ROLLBACK.sql (same -MIG- tag + claim remark).
   Re-runnable: an employee that already has OPD-MIG-<code> in the year is skipped.
   Run with @Commit = 0 first (preview + rollback), then @Commit = 1.
   ========================================================================== */
USE [HRIS_SMC_DB];
GO
SET NOCOUNT ON;

DECLARE @Commit          BIT = 0;      -- 0 = preview & rollback, 1 = save
DECLARE @CompanyId       INT = 2;
DECLARE @Adjust          DECIMAL(18,0) = -20000;   -- added to Excel Availed (0) -> amount inserted
DECLARE @FinancialYearId INT;
SELECT TOP 1 @FinancialYearId = FinancialYearId
FROM dbo.tblFinancialYear
WHERE CompanyId = @CompanyId AND Status = 'Active'
  AND CONVERT(DATE, GETDATE()) BETWEEN CONVERT(DATE, StartDate) AND CONVERT(DATE, EndDate)
ORDER BY FinancialYearId DESC;
IF @FinancialYearId IS NULL
BEGIN
    RAISERROR('Current financial year not found for the company.', 16, 1);
    RETURN;
END
PRINT 'Using FinancialYearId = ' + CAST(@FinancialYearId AS VARCHAR(10));
DECLARE @EntryBy INT = 3230;   -- HR user id recorded as ApprovedBy / fallback EntryBy
DECLARE @HeadOPD INT = 2;      -- OIPDHeadOfExpenseId 2 = Medicine (OPD)

IF OBJECT_ID('tempdb..#Avail') IS NOT NULL DROP TABLE #Avail;
CREATE TABLE #Avail
(
    [Type]        NVARCHAR(10)  NOT NULL,
    EmpMasterCode NVARCHAR(100) NOT NULL,
    ExcelAvailed  DECIMAL(18,0) NOT NULL,
    EmpInfoId     INT NULL,
    Remark        NVARCHAR(200) NULL
);

INSERT INTO #Avail ([Type], EmpMasterCode, ExcelAvailed)
VALUES
    ('OPD', '50025', 0)  -- Mr. Ershad Rashid
    ,('OPD', '50044', 0)  -- Mr. Abdul Mannan
    ,('OPD', '50269', 0)  -- Mr. Alamgir Hossain
;

UPDATE a SET EmpInfoId = g.EmpInfoId
FROM #Avail a
JOIN dbo.tblEmpGeneralInfo g ON g.EmpMasterCode = a.EmpMasterCode AND g.CompanyId = @CompanyId;

UPDATE #Avail SET Remark = CASE WHEN EmpInfoId IS NULL THEN 'SKIPPED - employee not found in tblEmpGeneralInfo (CompanyId ' + CAST(@CompanyId AS VARCHAR(5)) + ')' ELSE 'New' END;

UPDATE a SET Remark = 'SKIPPED - already migrated'
FROM #Avail a
WHERE a.EmpInfoId IS NOT NULL
  AND EXISTS (SELECT 1 FROM dbo.tbl_ReimbursmentFormMaster_HealthCare m
              WHERE m.RequitisionNo = a.[Type] + '-MIG-' + a.EmpMasterCode AND m.FinancialYearId = @FinancialYearId);

BEGIN TRY
    BEGIN TRAN;

    DECLARE @Map TABLE (MasterId INT, [Type] NVARCHAR(10), EmpInfoId INT, Amt DECIMAL(18,0));

    MERGE dbo.tbl_ReimbursmentFormMaster_HealthCare AS tgt
    USING
    (
        SELECT a.[Type], a.EmpMasterCode, a.EmpInfoId, a.ExcelAvailed + @Adjust AS InsertAmount, g.EmpName,
               (SELECT TOP 1 UserId FROM dbo.tblUser WHERE EmpInfoId = a.EmpInfoId ORDER BY UserId) AS UserId,
               g.DivisionId, g.DivisionWId, g.DepartmentId, g.SectionId, g.SubSectionId, g.DesignationId, g.EmpCategoryId
        FROM #Avail a
        JOIN dbo.tblEmpGeneralInfo g ON g.EmpInfoId = a.EmpInfoId
        WHERE a.Remark = 'New'
    ) AS src ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT ([Type], CompanyId, FinancialYearId, EmpInfoId, EmpMasterCode,
                DivisionId, DivisionWId, DepartmentId, SectionId, SubSectionId, DesignationId, EmpCategoryId,
                PatientName, PatientAge, Relationship, IsActive, EntryBy, EntryDate, RequitisionNo,
                PaymentStatus, ActionStatus, ShowStatus, SelfDate, SubmitDate, ClaimTKByUser, ClaimTKByHR, IgMiGrate, ApprovedBy, ApprovedDate)
        VALUES (src.[Type], @CompanyId, @FinancialYearId, src.EmpInfoId, src.EmpMasterCode,
                src.DivisionId, src.DivisionWId, src.DepartmentId, src.SectionId, src.SubSectionId, src.DesignationId, src.EmpCategoryId,
                src.EmpName, NULL, 'Self', 1, ISNULL(src.UserId, @EntryBy), GETDATE(), src.[Type] + '-MIG-' + src.EmpMasterCode,
                'New', 'Approved', 'Approved', CONVERT(NVARCHAR(30), GETDATE(), 106), GETDATE(), src.InsertAmount, src.InsertAmount, 1, @EntryBy, GETDATE())
    OUTPUT inserted.ReimbursFromMasterId, inserted.[Type], inserted.EmpInfoId, src.InsertAmount INTO @Map;

    INSERT INTO dbo.tbl_ReimbursmentformClaimDetails_HC
        (ReimbursFromMasterId, OIPDHeadOfExpenseId, Dates, SINoOfEncloseVoucher, Amount, NoOfDays, Rent, ChildrenNo, IgMiGrate)
    SELECT m.MasterId, @HeadOPD, GETDATE(), 'Opening availed balance (Graded Limit Update)', m.Amt, 1, m.Amt, 0, 1
    FROM @Map m;

    INSERT INTO dbo.tblReimbursementSelfAppLog
        (ReimbursFromMasterId, PreEmpInfoId, ForEmpInfoId, Version, ApproveBy, ApproveDate, ActionStatus, Comments, CommentsEMPID)
    SELECT m.MasterId, 0, m.EmpInfoId, 1, CAST(@EntryBy AS NVARCHAR(100)), GETDATE(), 'Approved',
           'Opening availed balance - Graded Limit Update', m.EmpInfoId
    FROM @Map m;

    SELECT a.[Type], a.EmpMasterCode, g.EmpName, a.ExcelAvailed, a.Remark
    FROM #Avail a LEFT JOIN dbo.tblEmpGeneralInfo g ON g.EmpInfoId = a.EmpInfoId;

    SELECT [Type], COUNT(*) AS FormsInserted, SUM(Amt) AS TotalAmount FROM @Map GROUP BY [Type];

    IF @Commit = 1
    BEGIN
        COMMIT TRAN;
        PRINT 'COMMITTED.';
    END
    ELSE
    BEGIN
        ROLLBACK TRAN;
        PRINT 'PREVIEW ONLY - rolled back. Set @Commit = 1 to save.';
    END
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRAN;
    DECLARE @Msg NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('Zero availed entry failed: %s', 16, 1, @Msg);
END CATCH

DROP TABLE #Avail;
GO
