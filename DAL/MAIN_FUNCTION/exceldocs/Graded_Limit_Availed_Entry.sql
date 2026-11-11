/* ==========================================================================
   Graded Limit Update - Opening "Availed Amount" entry
   Database : HRIS_SMC_DB
   Page     : HealthCare_UI/ExpenseReimbursementForm_HR_New.aspx
   Source   : Graded Limit Update.xlsx (sheet "AO OPD" = Claimed, sheet "IPD" = Availed)

   How the page computes "Availed Amount" (sp_GET_RemainingBalance):
     SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) of forms whose latest
     tblReimbursementSelfAppLog.ActionStatus is NOT Draft/Drafted/Review/Return/Rejected,
     for the employee's Type (IPD/OPD) and FinancialYearId.
   So per employee + Type this script inserts ONE approved form:
     tbl_ReimbursmentFormMaster_HealthCare (+ ClaimTKByUser/HR)
     tbl_ReimbursmentformClaimDetails_HC   (one row, Amount = amount to add)
     tblReimbursementSelfAppLog            (one 'Approved' row)

   Rules
     - Only Availed amount is entered; Allocated/Remaining are NOT touched
       (Total Balance comes from tblHRDecleration_Healthcare, FY 22 = 12000 / 288000).
     - If the employee already has availed amount in FY 22 for that Type, only the
       DIFFERENCE (Excel - existing) is inserted, so the total equals the Excel figure.
     - Re-runnable: rows tagged RequitisionNo = '<Type>-MIG-<EmpCode>' are skipped.
     - Run with @Commit = 0 first (preview + rollback), then @Commit = 1.
   ========================================================================== */
USE [HRIS_SMC_DB];
GO
SET NOCOUNT ON;

DECLARE @Commit          BIT = 0;      -- 0 = preview & rollback, 1 = save
DECLARE @OnlyType        NVARCHAR(10) = 'IPD';  -- 'OPD' = OPD only, 'IPD' = IPD only, NULL = both
DECLARE @CompanyId       INT = 2;     -- Excel employees belong to CompanyId 2
DECLARE @FinancialYearId INT;          -- current financial year of @CompanyId (resolved below)
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
DECLARE @EntryBy         INT = 3230;   -- HR user id recorded as EntryBy (change if needed)
DECLARE @HeadOPD         INT = 2;      -- OIPDHeadOfExpenseId 2 = Medicine (OPD)
DECLARE @HeadIPD         INT = 6;      -- OIPDHeadOfExpenseId 6 = Other Inpatient Expense (IPD)

IF OBJECT_ID('tempdb..#Avail') IS NOT NULL DROP TABLE #Avail;
CREATE TABLE #Avail
(
    [Type]          NVARCHAR(10)  NOT NULL,
    EmpMasterCode   NVARCHAR(100) NOT NULL,
    ExcelAvailed    DECIMAL(18,0) NOT NULL,
    EmpInfoId       INT NULL,
    ExistingAvailed DECIMAL(18,0) NOT NULL DEFAULT 0,
    InsertAmount    DECIMAL(18,0) NULL,
    Remark          NVARCHAR(200) NULL
);

INSERT INTO #Avail ([Type], EmpMasterCode, ExcelAvailed)
VALUES
    ('OPD', '50254', 5785)  -- Md. Mahabub Rahman
    ,('OPD', '50187', 12000)  -- Mr. Zakir Hossain
    ,('OPD', '50605', 8201)  -- Mr. Saiakul Islam Tutul
    ,('OPD', '50926', 11120)  -- Mr. Jahangir Alam
    ,('OPD', '30284', 12000)  -- Md.Razib Ahmed
    ,('OPD', '50139', 12000)  -- Mujibur Rahman Bepari
    ,('OPD', '50556', 7921)  -- Md. Chand Ali
    ,('OPD', '50589', 6045)  -- Md. Md. Rajib
    ,('OPD', '51843', 5047)  -- Md. Hanif Hossain
    ,('OPD', '51436', 4858)  -- Md.Obaidul Islam
    ,('OPD', '52703', 8380)  -- Md. Nayan PK
    ,('OPD', '53147', 1634)  -- Md. Shaju Islam
    ,('OPD', '50700', 5340)  -- Md.Shahidul Islam
    ,('OPD', '50253', 12000)  -- Md. Rafiqul Islam
    ,('OPD', '50448', 7445)  -- Md. Shafiqul Islam
    ,('OPD', '52116', 5525)  -- Md. Masud Rana
    ,('OPD', '50364', 1360)  -- MD. ABDULLAH AL MAMUN
    ,('OPD', '50368', 4740)  -- MOHAMMAD ZIAUR RAHMAN
    ,('OPD', '50640', 12000)  -- KHAZA SHARIF UDDIN
    ,('OPD', '50708', 12000)  -- MD. NUR ISLAM RONY
    ,('OPD', '50271', 9753)  -- MD. SHAFIQUL ISLAM
    ,('OPD', '52117', 4313)  -- Md. Ali Hossen
    ,('OPD', '50591', 11781)  -- Md. Golam Faruque
    ,('OPD', '51844', 4190)  -- Md. Walidul Khaled
    ,('OPD', '50257', 12000)  -- Mr. S.M. Dulal Hossain
    ,('OPD', '50365', 12000)  -- Md. Arif Mahmud
    ,('OPD', '50551', 12000)  -- Md. Monir Hossain
    ,('OPD', '50361', 9142)  -- Md. Haris Howlader
    ,('OPD', '50052', 9817)  -- Mr. Ratan Ali Khan
    ,('OPD', '50706', 7000)  -- Md. Monir Khan
    ,('OPD', '50697', 12000)  -- Md. Nur Uddin
    ,('OPD', '50544', 4979)  -- Md. Abul Kalam Azad
    ,('OPD', '50185', 7592)  -- Md. Ismail Akhand
    ,('OPD', '50858', 1530)  -- Md Shamim Hossain
    ,('OPD', '30495', 6997)  -- Md. Sultan Mahmud
    ,('OPD', '50193', 12000)  -- Md. Mizanur Rahman
    ,('OPD', '50696', 12000)  -- Md. Shahid Ali
    ,('OPD', '50317', 5375)  -- Md.Shohag Mia
    ,('OPD', '50593', 5938)  -- Md.Shahed Hossain
    ,('OPD', '50645', 5670)  -- Md.Abdul Karim
    ,('OPD', '50363', 4860)  -- Md.Shafizul Haque
    ,('OPD', '50587', 5120)  -- MD.HABIBUR RAHMAN
    ,('OPD', '53128', 5430)  -- ROMAN
    ,('OPD', '50294', 4890)  -- Mr.Md.Akter Hossain
    ,('OPD', '50545', 12000)  -- Mr. Md. Shohag Sharif
    ,('OPD', '52523', 6160)  -- Md. Amdadur Rahman Abad
    ,('IPD', '30328', 75489)  -- Md. Abdul Karim Hawlader
    ,('IPD', '53472', 59976)  -- Md. Sirajul Islam
    ,('IPD', '50448', 23490)  -- Md. Shafiqul Islam
    ,('IPD', '51841', 4045)  -- Md. Saiful Islam
    ,('IPD', '50255', 6708)  -- Md. Yusuf Ali Bappy
    ,('IPD', '51843', 35950)  -- Md. Hanif Hossain
    ,('IPD', '30256', 47089)  -- Tannir Hossain
    ,('IPD', '30507', 25077)  -- Md. Jahid Hossain Mahin
    ,('IPD', '50256', 18988)  -- Amit Barua
    ,('IPD', '30285', 10910)  -- Md. Saiful Islam
    ,('IPD', '52649', 24000)  -- Lutfor Rahman
    ,('IPD', '52552', 18953)  -- Md. Rahmatullah Pramanik
    ,('IPD', '52934', 169794)  -- Abdur Rahman
    ,('IPD', '50668', 28935)  -- Md. Dalim Hossain
    ,('IPD', '50189', 44230)  -- Md. Jahangir Alam
    ,('IPD', '53148', 20000)  -- Nurul Islam
    ,('IPD', '50937', 8435)  -- Md. Ashik Ali
    ,('IPD', '53694', 66352)  -- Md. Sujaul karim
    ,('IPD', '51656', 13605)  -- Jalilur Rahman
;

-- restrict to one Type ('OPD' / 'IPD'); set NULL to load both
DELETE FROM #Avail WHERE @OnlyType IS NOT NULL AND [Type] <> @OnlyType;

-- map employee
UPDATE a SET EmpInfoId = g.EmpInfoId
FROM #Avail a
JOIN dbo.tblEmpGeneralInfo g ON g.EmpMasterCode = a.EmpMasterCode AND g.CompanyId = @CompanyId;

-- availed already counted by the page (same filter as sp_GET_RemainingBalance)
UPDATE a SET ExistingAvailed = ISNULL(x.Amt, 0)
FROM #Avail a
OUTER APPLY
(
    SELECT SUM(ISNULL(cd.Amount, 0)) AS Amt
    FROM dbo.tbl_ReimbursmentFormMaster_HealthCare rm
    JOIN dbo.tbl_ReimbursmentformClaimDetails_HC cd ON cd.ReimbursFromMasterId = rm.ReimbursFromMasterId
    JOIN dbo.tblReimbursementSelfAppLog lg
         ON lg.ReimbursFromMasterId = rm.ReimbursFromMasterId
        AND lg.Version = (SELECT MAX(Version) FROM dbo.tblReimbursementSelfAppLog WHERE ReimbursFromMasterId = rm.ReimbursFromMasterId)
    WHERE rm.EmpInfoId = a.EmpInfoId AND rm.FinancialYearId = @FinancialYearId AND rm.[Type] = a.[Type]
      AND lg.ActionStatus IS NOT NULL
      AND lg.ActionStatus NOT IN ('Draft','Drafted','Review','Return','Rejected')
) x
WHERE a.EmpInfoId IS NOT NULL;

UPDATE #Avail SET
    InsertAmount = CASE WHEN EmpInfoId IS NULL THEN NULL
                        WHEN ExcelAvailed > ExistingAvailed THEN ExcelAvailed - ExistingAvailed ELSE 0 END,
    Remark = CASE WHEN EmpInfoId IS NULL THEN 'SKIPPED - employee not found in tblEmpGeneralInfo (CompanyId ' + CAST(@CompanyId AS VARCHAR(5)) + ')'
                  WHEN ExcelAvailed <= ExistingAvailed THEN 'SKIPPED - system already has >= Excel availed'
                  WHEN ExistingAvailed > 0 THEN 'Difference only (system already has some availed)'
                  ELSE 'New' END;

-- 30285 / 50937: system already holds more IPD than Excel; per HR instruction insert the Excel amount in full (no delta)
UPDATE #Avail SET InsertAmount = ExcelAvailed, Remark = 'Excel amount inserted in full (HR instruction)'
WHERE EmpInfoId IS NOT NULL AND [Type] = 'IPD' AND EmpMasterCode IN ('30285','50937');

-- already migrated by an earlier run
UPDATE a SET InsertAmount = 0, Remark = 'SKIPPED - already migrated'
FROM #Avail a
WHERE a.EmpInfoId IS NOT NULL
  AND EXISTS (SELECT 1 FROM dbo.tbl_ReimbursmentFormMaster_HealthCare m
              WHERE m.RequitisionNo = a.[Type] + '-MIG-' + a.EmpMasterCode AND m.FinancialYearId = @FinancialYearId);

BEGIN TRY
    BEGIN TRAN;

    DECLARE @Map TABLE (MasterId INT, [Type] NVARCHAR(10), EmpInfoId INT, Amt DECIMAL(18,0));

    -- 1) master (MERGE ... ON 1=0 lets us capture the identity per source row)
    MERGE dbo.tbl_ReimbursmentFormMaster_HealthCare AS tgt
    USING
    (
        SELECT a.[Type], a.EmpMasterCode, a.EmpInfoId, a.InsertAmount, g.EmpName,
               (SELECT TOP 1 UserId FROM dbo.tblUser WHERE EmpInfoId = a.EmpInfoId ORDER BY UserId) AS UserId,
               g.DivisionId, g.DivisionWId, g.DepartmentId, g.SectionId, g.SubSectionId, g.DesignationId, g.EmpCategoryId
        FROM #Avail a
        JOIN dbo.tblEmpGeneralInfo g ON g.EmpInfoId = a.EmpInfoId
        WHERE a.InsertAmount > 0
    ) AS src ON 1 = 0
    WHEN NOT MATCHED THEN
        INSERT ([Type], CompanyId, FinancialYearId, EmpInfoId, EmpMasterCode,
                DivisionId, DivisionWId, DepartmentId, SectionId, SubSectionId, DesignationId, EmpCategoryId,
                PatientName, PatientAge, Relationship, IsActive, EntryBy, EntryDate, RequitisionNo,  /* EntryBy = employee's own tblUser.UserId */
                PaymentStatus, ActionStatus, ShowStatus, SelfDate, SubmitDate, ClaimTKByUser, ClaimTKByHR, IgMiGrate, ApprovedBy, ApprovedDate)
        VALUES (src.[Type], @CompanyId, @FinancialYearId, src.EmpInfoId, src.EmpMasterCode,
                src.DivisionId, src.DivisionWId, src.DepartmentId, src.SectionId, src.SubSectionId, src.DesignationId, src.EmpCategoryId,
                src.EmpName, NULL, 'Self', 1, ISNULL(src.UserId, @EntryBy), GETDATE(), src.[Type] + '-MIG-' + src.EmpMasterCode,
                'New', 'Approved', 'Approved', CONVERT(NVARCHAR(30), GETDATE(), 106), GETDATE(), src.InsertAmount, src.InsertAmount, 1, @EntryBy, GETDATE())
    OUTPUT inserted.ReimbursFromMasterId, inserted.[Type], inserted.EmpInfoId, src.InsertAmount INTO @Map;

    -- 2) one claim row per form
    INSERT INTO dbo.tbl_ReimbursmentformClaimDetails_HC
        (ReimbursFromMasterId, OIPDHeadOfExpenseId, Dates, SINoOfEncloseVoucher, Amount, NoOfDays, Rent, ChildrenNo, IgMiGrate)
    SELECT m.MasterId,
           CASE WHEN m.[Type] = 'OPD' THEN @HeadOPD ELSE @HeadIPD END,
           GETDATE(), 'Opening availed balance (Graded Limit Update)', m.Amt, 1, m.Amt, 0, 1
    FROM @Map m;

    -- 3) approval log (latest version must be a non-draft status for the page to count it)
    INSERT INTO dbo.tblReimbursementSelfAppLog
        (ReimbursFromMasterId, PreEmpInfoId, ForEmpInfoId, Version, ApproveBy, ApproveDate, ActionStatus, Comments, CommentsEMPID)
    SELECT m.MasterId, 0, m.EmpInfoId, 1, CAST(@EntryBy AS NVARCHAR(100)), GETDATE(), 'Approved',
           'Opening availed balance - Graded Limit Update', m.EmpInfoId
    FROM @Map m;

    -- preview: what was done per Excel row
    SELECT a.[Type], a.EmpMasterCode, g.EmpName, a.ExcelAvailed, a.ExistingAvailed, a.InsertAmount, a.Remark
    FROM #Avail a LEFT JOIN dbo.tblEmpGeneralInfo g ON g.EmpInfoId = a.EmpInfoId
    ORDER BY a.Remark, a.[Type], a.EmpMasterCode;

    SELECT [Type], COUNT(*) AS FormsInserted, SUM(Amt) AS TotalAmount FROM @Map GROUP BY [Type];

    -- verification: page figure (sp_GET_RemainingBalance logic) after insert vs Excel; mismatches only (should be empty)
    SELECT v.[Type], v.EmpMasterCode, v.ExcelAvailed, v.AvailedAfter
    FROM
    (
        SELECT a.[Type], a.EmpMasterCode, a.ExcelAvailed,
               ISNULL((SELECT SUM(cd.Amount)
                       FROM dbo.tbl_ReimbursmentFormMaster_HealthCare rm
                       JOIN dbo.tbl_ReimbursmentformClaimDetails_HC cd ON cd.ReimbursFromMasterId = rm.ReimbursFromMasterId
                       JOIN dbo.tblReimbursementSelfAppLog lg ON lg.ReimbursFromMasterId = rm.ReimbursFromMasterId
                            AND lg.Version = (SELECT MAX(Version) FROM dbo.tblReimbursementSelfAppLog WHERE ReimbursFromMasterId = rm.ReimbursFromMasterId)
                       WHERE rm.EmpInfoId = a.EmpInfoId AND rm.FinancialYearId = @FinancialYearId AND rm.[Type] = a.[Type]
                         AND lg.ActionStatus NOT IN ('Draft','Drafted','Review','Return','Rejected')), 0) AS AvailedAfter
        FROM #Avail a WHERE a.EmpInfoId IS NOT NULL
    ) v
    WHERE v.AvailedAfter < v.ExcelAvailed;

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
    RAISERROR('Graded limit availed entry failed: %s', 16, 1, @Msg);
END CATCH

DROP TABLE #Avail;
GO
