/*
Adds the missing "complete" action to the Clearance Forwarding module
(SQL_Scripts\ClearanceForwarding_Schema.sql). Lets a supervised employee mark
their own forwarded detail row as Done; the first one to do so also marks the
whole master record Done (any-one-of-the-forwarded-people-completes rule).
Backs:
  - MainSite\ExitManagement_UI\ClearanceForwardingList.aspx
  - DAL\ExitManagement_DAL\ClearanceForwardingDal.cs (CompleteForwarding)

Run against the HRIS_SMC_DB database, after ClearanceForwarding_Schema.sql.
*/

-- 1. Audit columns on the detail row (who completed it and when)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.tblClearanceForwardingDetail') AND name = 'CompletedBy'
)
BEGIN
    ALTER TABLE dbo.tblClearanceForwardingDetail ADD CompletedBy BIGINT NULL;
END
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.tblClearanceForwardingDetail') AND name = 'CompletedDate'
)
BEGIN
    ALTER TABLE dbo.tblClearanceForwardingDetail ADD CompletedDate DATETIME NULL;
END
GO

-- 2. Stored procedure
-- Only the employee the row was forwarded to (@CompletedBy = SupervisedEmployeeId) can
-- complete it, and only while it is still Pending - the WHERE clause on the UPDATE makes
-- this atomic, so concurrent duplicate-completion attempts can't both succeed. Returns 1
-- if this call is the one that completed it, 0 if it was already done, not found, or not
-- forwarded to this employee.
IF OBJECT_ID('dbo.sp_Update_ClearanceForwardingComplete') IS NOT NULL
    DROP PROCEDURE dbo.sp_Update_ClearanceForwardingComplete;
GO
CREATE PROCEDURE dbo.sp_Update_ClearanceForwardingComplete
    @ClearanceForwardingDetailId INT,
    @CompletedBy BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MasterId INT;
    DECLARE @Updated INT;

    BEGIN TRAN;

    UPDATE dbo.tblClearanceForwardingDetail
       SET Status = 'Done',
           CompletedBy = @CompletedBy,
           CompletedDate = GETDATE()
     WHERE ClearanceForwardingDetailId = @ClearanceForwardingDetailId
       AND SupervisedEmployeeId = @CompletedBy
       AND Status = 'Pending';

    SET @Updated = @@ROWCOUNT;

    IF @Updated > 0
    BEGIN
        SELECT @MasterId = ClearanceForwardingId
        FROM dbo.tblClearanceForwardingDetail
        WHERE ClearanceForwardingDetailId = @ClearanceForwardingDetailId;

        UPDATE dbo.tblClearanceForwardingMaster
           SET Status = 'Done'
         WHERE ClearanceForwardingId = @MasterId
           AND Status <> 'Done';
    END

    COMMIT TRAN;

    SELECT @Updated;
END
GO
