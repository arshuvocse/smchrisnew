/*
New "Clearance Forwarding" module: lets a supervisor (or HR, on behalf of a
supervisor) forward clearance responsibility to their directly-supervised
employees (tblEmpGeneralInfo.ReportingEmpId chain). This script creates the
new master/detail tables and stored procedures backing:
  - MainSite\ExitManagement_UI\ClearanceForwardingEntry.aspx
  - MainSite\ExitManagement_UI\ClearanceForwardingList.aspx
  - DAL\ExitManagement_DAL\ClearanceForwardingDal.cs

Run against the HRIS_SMC_DB database.
*/

-- 1. Tables
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'tblClearanceForwardingMaster')
BEGIN
    CREATE TABLE dbo.tblClearanceForwardingMaster (
        ClearanceForwardingId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblClearanceForwardingMaster PRIMARY KEY,
        CompanyId INT NOT NULL,
        EmployeeId BIGINT NOT NULL,        -- the supervisor/forwarder (tblEmpGeneralInfo.EmpInfoId)
        ForwardedBy INT NOT NULL,          -- Session["UserId"]
        ForwardedDate DATETIME NOT NULL,
        Status VARCHAR(20) NOT NULL CONSTRAINT DF_tblClearanceForwardingMaster_Status DEFAULT ('Forwarded'),
        Remarks NVARCHAR(500) NULL,
        EntryBy NVARCHAR(50) NULL,
        EntryDate DATETIME NULL,
        UpdateBy NVARCHAR(50) NULL,
        UpdateDate DATETIME NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'tblClearanceForwardingDetail')
BEGIN
    CREATE TABLE dbo.tblClearanceForwardingDetail (
        ClearanceForwardingDetailId INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_tblClearanceForwardingDetail PRIMARY KEY,
        ClearanceForwardingId INT NOT NULL CONSTRAINT FK_tblClearanceForwardingDetail_Master REFERENCES dbo.tblClearanceForwardingMaster(ClearanceForwardingId),
        SupervisedEmployeeId BIGINT NOT NULL,   -- tblEmpGeneralInfo.EmpInfoId
        Status VARCHAR(20) NOT NULL CONSTRAINT DF_tblClearanceForwardingDetail_Status DEFAULT ('Pending'),
        CompletedBy BIGINT NULL,
        CompletedDate DATETIME NULL,
        OrderNo INT NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_tblClearanceForwardingDetail_MasterId')
BEGIN
    CREATE INDEX IX_tblClearanceForwardingDetail_MasterId ON dbo.tblClearanceForwardingDetail(ClearanceForwardingId);
END
GO

-- 2. Stored procedures

IF OBJECT_ID('dbo.sp_Save_ClearanceForwardingMaster') IS NOT NULL
    DROP PROCEDURE dbo.sp_Save_ClearanceForwardingMaster;
GO
CREATE PROCEDURE dbo.sp_Save_ClearanceForwardingMaster
    @CompanyId INT,
    @EmployeeId BIGINT,
    @ForwardedBy INT,
    @Remarks NVARCHAR(500) = NULL,
    @EntryBy NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.tblClearanceForwardingMaster
        (CompanyId, EmployeeId, ForwardedBy, ForwardedDate, Status, Remarks, EntryBy, EntryDate)
    VALUES
        (@CompanyId, @EmployeeId, @ForwardedBy, GETDATE(), 'Forwarded', @Remarks, @EntryBy, GETDATE());

    SELECT SCOPE_IDENTITY();
END
GO

IF OBJECT_ID('dbo.sp_Save_ClearanceForwardingDetail') IS NOT NULL
    DROP PROCEDURE dbo.sp_Save_ClearanceForwardingDetail;
GO
CREATE PROCEDURE dbo.sp_Save_ClearanceForwardingDetail
    @ClearanceForwardingId INT,
    @SupervisedEmployeeId BIGINT,
    @OrderNo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.tblClearanceForwardingDetail
        (ClearanceForwardingId, SupervisedEmployeeId, Status, OrderNo)
    VALUES
        (@ClearanceForwardingId, @SupervisedEmployeeId, 'Pending', @OrderNo);
END
GO

IF OBJECT_ID('dbo.sp_GET_ClearanceForwardingCheckDuplicate') IS NOT NULL
    DROP PROCEDURE dbo.sp_GET_ClearanceForwardingCheckDuplicate;
GO
CREATE PROCEDURE dbo.sp_GET_ClearanceForwardingCheckDuplicate
    @CompanyId INT,
    @EmployeeId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 ClearanceForwardingId
    FROM dbo.tblClearanceForwardingMaster
    WHERE CompanyId = @CompanyId
      AND EmployeeId = @EmployeeId
      AND Status = 'Forwarded';
END
GO

IF OBJECT_ID('dbo.sp_GET_ClearanceForwardingList') IS NOT NULL
    DROP PROCEDURE dbo.sp_GET_ClearanceForwardingList;
GO
CREATE PROCEDURE dbo.sp_GET_ClearanceForwardingList
    @CompanyId INT = NULL,
    @EmployeeId BIGINT = NULL,
    @DateFrom DATETIME = NULL,
    @DateTo DATETIME = NULL,
    @Status VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        m.ClearanceForwardingId,
        m.CompanyId,
        c.CompanyName,
        m.EmployeeId,
        emp.EmpMasterCode,
        emp.EmpName,
        m.ForwardedBy,
        fb.UserName AS ForwardedByName,
        m.ForwardedDate,
        m.Status,
        m.Remarks,
        STUFF((
            SELECT ', ' + se.EmpName + CASE WHEN ISNULL(d.OrderNo, 1) = 1 THEN ' (Responsible Person)' ELSE '' END
            FROM dbo.tblClearanceForwardingDetail d
            INNER JOIN dbo.tblEmpGeneralInfo se ON se.EmpInfoId = d.SupervisedEmployeeId
            WHERE d.ClearanceForwardingId = m.ClearanceForwardingId
            ORDER BY ISNULL(d.OrderNo, 99999), d.ClearanceForwardingDetailId ASC
            FOR XML PATH('')
        ), 1, 2, '') AS SupervisedEmployeeNames
    FROM dbo.tblClearanceForwardingMaster m
    INNER JOIN dbo.tblCompanyInfo c ON c.CompanyId = m.CompanyId
    INNER JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = m.EmployeeId
    LEFT JOIN dbo.tblUser fb ON fb.UserId = m.ForwardedBy
    WHERE (@CompanyId IS NULL OR m.CompanyId = @CompanyId)
      AND (@EmployeeId IS NULL OR m.EmployeeId = @EmployeeId)
      AND (@DateFrom IS NULL OR m.ForwardedDate >= @DateFrom)
      AND (@DateTo IS NULL OR m.ForwardedDate < DATEADD(DAY, 1, @DateTo))
      AND (@Status IS NULL OR @Status = '' OR m.Status = @Status)
    ORDER BY m.ForwardedDate DESC;
END
GO

IF OBJECT_ID('dbo.sp_GET_ClearanceForwardingMasterById') IS NOT NULL
    DROP PROCEDURE dbo.sp_GET_ClearanceForwardingMasterById;
GO
CREATE PROCEDURE dbo.sp_GET_ClearanceForwardingMasterById
    @ClearanceForwardingId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        m.ClearanceForwardingId,
        m.CompanyId,
        c.CompanyName,
        m.EmployeeId,
        emp.EmpMasterCode,
        emp.EmpName,
        m.ForwardedBy,
        fb.UserName AS ForwardedByName,
        m.ForwardedDate,
        m.Status,
        m.Remarks
    FROM dbo.tblClearanceForwardingMaster m
    INNER JOIN dbo.tblCompanyInfo c ON c.CompanyId = m.CompanyId
    INNER JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId = m.EmployeeId
    LEFT JOIN dbo.tblUser fb ON fb.UserId = m.ForwardedBy
    WHERE m.ClearanceForwardingId = @ClearanceForwardingId;
END
GO

IF OBJECT_ID('dbo.sp_GET_ClearanceForwardingDetailById') IS NOT NULL
    DROP PROCEDURE dbo.sp_GET_ClearanceForwardingDetailById;
GO
CREATE PROCEDURE dbo.sp_GET_ClearanceForwardingDetailById
    @ClearanceForwardingId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.ClearanceForwardingDetailId,
        d.ClearanceForwardingId,
        d.SupervisedEmployeeId,
        se.EmpMasterCode,
        se.EmpName,
        des.Designation,
        d.Status,
        d.OrderNo,
        ISNULL(div.DivisionName, '') AS DivisionName
    FROM dbo.tblClearanceForwardingDetail d
    INNER JOIN dbo.tblEmpGeneralInfo se ON se.EmpInfoId = d.SupervisedEmployeeId
    LEFT JOIN dbo.tblDesignation des ON des.DesignationId = se.DesignationId
    LEFT JOIN dbo.tblDivision div ON se.DivisionId = div.DivisionId
    WHERE d.ClearanceForwardingId = @ClearanceForwardingId
    ORDER BY ISNULL(d.OrderNo, 9999) ASC, d.ClearanceForwardingDetailId ASC;
END
GO

IF OBJECT_ID('dbo.sp_GET_SupervisedEmployeesByDivision') IS NOT NULL
    DROP PROCEDURE dbo.sp_GET_SupervisedEmployeesByDivision;
GO
CREATE PROCEDURE dbo.sp_GET_SupervisedEmployeesByDivision
    @SupervisorEmpId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DivId INT, @CompId INT;
    SELECT @DivId = DivisionId, @CompId = CompanyId FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId = @SupervisorEmpId;

    SELECT 
        e.EmpInfoId, 
        e.EmpMasterCode, 
        e.EmpName, 
        ISNULL(des.Designation, '') AS Designation, 
        ISNULL(div.DivisionName, '') AS DivisionName
    FROM dbo.tblEmpGeneralInfo e
    LEFT JOIN dbo.tblDesignation des ON e.DesignationId = des.DesignationId
    LEFT JOIN dbo.tblDivision div ON e.DivisionId = div.DivisionId
    WHERE e.EmpInfoId <> @SupervisorEmpId 
      AND e.IsActive = 1
      AND (@DivId IS NULL OR e.DivisionId = @DivId)
      AND (@CompId IS NULL OR e.CompanyId = @CompId)
    ORDER BY 
        CASE WHEN ISNUMERIC(e.EmpMasterCode) = 1 THEN CAST(e.EmpMasterCode AS BIGINT) ELSE 999999999 END ASC, 
        e.EmpMasterCode ASC;
END
GO

IF OBJECT_ID('dbo.sp_Update_ClearanceForwardingMaster') IS NOT NULL
    DROP PROCEDURE dbo.sp_Update_ClearanceForwardingMaster;
GO
CREATE PROCEDURE dbo.sp_Update_ClearanceForwardingMaster
    @ClearanceForwardingId INT,
    @Remarks NVARCHAR(500) = NULL,
    @UpdateBy NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.tblClearanceForwardingMaster
       SET Remarks = @Remarks,
           UpdateBy = @UpdateBy,
           UpdateDate = GETDATE()
     WHERE ClearanceForwardingId = @ClearanceForwardingId;
END
GO

IF OBJECT_ID('dbo.sp_Delete_ClearanceForwardingDetailsPending') IS NOT NULL
    DROP PROCEDURE dbo.sp_Delete_ClearanceForwardingDetailsPending;
GO
CREATE PROCEDURE dbo.sp_Delete_ClearanceForwardingDetailsPending
    @ClearanceForwardingId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.tblClearanceForwardingDetail
    WHERE ClearanceForwardingId = @ClearanceForwardingId
      AND Status = 'Pending';
END
GO
