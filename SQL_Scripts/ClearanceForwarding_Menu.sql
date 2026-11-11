/*
Registers or updates the "Clearance Forwarding" page in dbo.tblMainMenu
under "Employee Separation" (ParantId='54'), and copies active menu permissions
from "Reimbursement Form for HR" (dbo.tblMenuGroupSetupDetail) so that everyone
with access to "Reimbursement Form for HR" gets access to "Clearance Forwarding".

Run against the HRIS_SMC_DB database.
*/

BEGIN TRANSACTION;

BEGIN TRY
    DECLARE @TargetMenuName NVARCHAR(100) = N'Clearance Forwarding';
    DECLARE @TargetURL NVARCHAR(250) = N'../ExitManagement_UI/ClearanceForwardingList.aspx';
    DECLARE @ParentId NVARCHAR(50) = N'54'; -- Employee Separation
    DECLARE @SourceMenuName NVARCHAR(100) = N'Reimbursement Form for HR';
    
    DECLARE @TargetMainMenuId INT;
    DECLARE @SourceMainMenuId INT;
    DECLARE @TargetSL INT = 98; -- Positioned under Clearance Form (SL=97)

    -- 1. Identify Source Menu ("Reimbursement Form for HR")
    SELECT TOP 1 @SourceMainMenuId = MainMenuId 
    FROM dbo.tblMainMenu 
    WHERE MenuName = @SourceMenuName;

    IF (@SourceMainMenuId IS NULL)
    BEGIN
        RAISERROR('Source menu "Reimbursement Form for HR" not found.', 16, 1);
    END

    -- 2. Ensure Target Menu ("Clearance Forwarding") exists in tblMainMenu
    SELECT TOP 1 @TargetMainMenuId = MainMenuId 
    FROM dbo.tblMainMenu 
    WHERE URL = @TargetURL OR MenuName = @TargetMenuName;

    IF (@TargetMainMenuId IS NULL)
    BEGIN
        INSERT INTO dbo.tblMainMenu
        (
            SL, MenuName, URL, ParantId, IsApprovalPage, ModuleId, MenuTypeId, icon,
            [Add], [View], [Edit], [Delete], Everyone, Own, IsInMainMenu, IsSupervisor, Remarks
        )
        VALUES
        (
            @TargetSL, @TargetMenuName, @TargetURL, @ParentId,
            0, 5, 2, '"icon fa fa-angle-right"',
            1, 1, 1, 1, 0, NULL, 1, NULL, N'Clearance Forwarding'
        );

        SET @TargetMainMenuId = SCOPE_IDENTITY();
        PRINT 'Inserted new menu "Clearance Forwarding" with MainMenuId: ' + CAST(@TargetMainMenuId AS NVARCHAR(20));
    END
    ELSE
    BEGIN
        UPDATE dbo.tblMainMenu
        SET SL = @TargetSL,
            MenuName = @TargetMenuName,
            URL = @TargetURL,
            ParantId = @ParentId,
            IsInMainMenu = 1,
            [Add] = 1,
            [View] = 1,
            [Edit] = 1,
            [Delete] = 1
        WHERE MainMenuId = @TargetMainMenuId;

        PRINT 'Updated existing menu "Clearance Forwarding" (MainMenuId: ' + CAST(@TargetMainMenuId AS NVARCHAR(20)) + ')';
    END

    -- 3. Copy/Mirror active permissions from "Reimbursement Form for HR" to "Clearance Forwarding"
    MERGE dbo.tblMenuGroupSetupDetail AS target
    USING (
        SELECT 
            MenuGroupSetupId,
            @TargetMainMenuId AS MainMenuId,
            IsActive = 1,
            [Add] = ISNULL([Add], 1),
            [View] = ISNULL([View], 1),
            [Edit] = ISNULL([Edit], 1),
            [Delete] = ISNULL([Delete], 0)
        FROM dbo.tblMenuGroupSetupDetail
        WHERE MainMenuId = @SourceMainMenuId AND IsActive = 1
    ) AS source
    ON (target.MenuGroupSetupId = source.MenuGroupSetupId AND target.MainMenuId = source.MainMenuId)
    WHEN MATCHED THEN
        UPDATE SET 
            target.IsActive = 1,
            target.[Add] = source.[Add],
            target.[View] = source.[View],
            target.[Edit] = source.[Edit],
            target.[Delete] = source.[Delete]
    WHEN NOT MATCHED THEN
        INSERT (MenuGroupSetupId, MainMenuId, IsActive, [Add], [View], [Edit], [Delete])
        VALUES (source.MenuGroupSetupId, source.MainMenuId, 1, source.[Add], source.[View], source.[Edit], source.[Delete]);

    COMMIT TRANSACTION;
    PRINT 'SUCCESS: Permissions successfully granted to "Clearance Forwarding" based on "Reimbursement Form for HR".';

    -- 4. Verification Output
    SELECT 
        m.MainMenuId,
        m.MenuName,
        m.URL,
        g.MenuGroupSetupId,
        g.MenuGroupName,
        d.IsActive,
        d.[Add],
        d.[View],
        d.[Edit],
        d.[Delete]
    FROM dbo.tblMenuGroupSetupDetail d
    INNER JOIN dbo.tblMainMenu m ON d.MainMenuId = m.MainMenuId
    INNER JOIN dbo.tblMenuGroupSetup g ON d.MenuGroupSetupId = g.MenuGroupSetupId
    WHERE d.MainMenuId = @TargetMainMenuId AND d.IsActive = 1;

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'ERROR: ' + ERROR_MESSAGE();
END CATCH;
GO

