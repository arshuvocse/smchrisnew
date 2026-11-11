/*
Fix: Submit/Draft on ExpenseReimbursementFormSelfEntry.aspx (IPD, with Hospital
Notification Email PDF attached) throws a SQL error ("Procedure or function
sp_Save_ReimbursmentMaster has too many arguments specified") because the C#
code (ReimbursmentFormDal.data_save) always passes @HospitalNotificationEmailDoc
and @HospitalNotificationEmailFileName, but neither the table nor the two
stored procedures know about these columns/parameters. The unhandled SqlException
then trips <customErrors defaultRedirect="Default.aspx"> in Web.config, which is
why the user gets bounced to the login page.

This script:
  1. Adds the two missing columns to tbl_ReimbursmentFormMaster_HealthCare.
  2. Adds the two missing parameters (default NULL, so existing callers are
     unaffected) to sp_Save_ReimbursmentMaster and sp_Update_ReimbursmentMaster,
     and persists them.

Run against the HRIS database.
*/

-- 1. New columns
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.tbl_ReimbursmentFormMaster_HealthCare')
      AND name = 'HospitalNotificationEmailDoc'
)
BEGIN
    ALTER TABLE [dbo].[tbl_ReimbursmentFormMaster_HealthCare]
        ADD [HospitalNotificationEmailDoc] NVARCHAR(500) NULL;
END
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.tbl_ReimbursmentFormMaster_HealthCare')
      AND name = 'HospitalNotificationEmailFileName'
)
BEGIN
    ALTER TABLE [dbo].[tbl_ReimbursmentFormMaster_HealthCare]
        ADD [HospitalNotificationEmailFileName] NVARCHAR(500) NULL;
END
GO

-- 2a. sp_Save_ReimbursmentMaster
ALTER PROCEDURE [dbo].[sp_Save_ReimbursmentMaster]


 @ReimbursFromMasterId INT = 0,
 @Type NVARCHAR(MAX) = NULL,
 @CompanyId INT = NULL,
 @EmpInfoId INT = NULL,
 @FinancialYearId INT = NULL,
 @PatientName  NVARCHAR(MAX) = NULL,
 @PatientAge INT = NULL,
 @Relationship NVARCHAR(max) = NULL, @OfficialMobile NVARCHAR(MAX) = NULL,
 @EntryBy INT = NULL,
 @SelfDate NVARCHAR(MAX) = NULL,
 @SubmitDate Datetime = NULL,
 @ActionStatus NVARCHAR(50) = NULL,

 @BankName	nvarchar(MAX)	= NULL,
@BankAccountNo	nvarchar(MAX)	= NULL,
@BranchName	nvarchar(MAX)	= NULL,
@RoutingNo	nvarchar(MAX)	= NULL,
 @HospitalNameId INT = NULL,
 @HospitalAdmissionDate date=null,
 @HospitalDischargeDate date=null,
 @HospitalNotificationEmailDoc NVARCHAR(500) = NULL,
 @HospitalNotificationEmailFileName NVARCHAR(500) = NULL

AS
BEGIN


      DECLARE @codeD NVARCHAR(MAX)

	  IF(@Type='OPD')
	  BEGIN
	   SELECT  @codeD = 'OPD-' + ( CONVERT(NVARCHAR(MAX), ( COUNT(ReimbursFromMasterId) + 10001 )) ) FROM  tbl_ReimbursmentFormMaster_HealthCare
	  END

	  IF(@Type='IPD')
	  BEGIN
	   SELECT  @codeD = 'IPD-' + ( CONVERT(NVARCHAR(MAX), ( COUNT(ReimbursFromMasterId) + 10001 )) ) FROM  tbl_ReimbursmentFormMaster_HealthCare
	  END

	  IF(@Type='Special')
	  BEGIN
	   SELECT  @codeD = 'SPC-' + ( CONVERT(NVARCHAR(MAX), ( COUNT(ReimbursFromMasterId) + 10001 )) ) FROM  tbl_ReimbursmentFormMaster_HealthCare
	  END

	  	update tblEmpGeneralInfo set OfficialMobile=@OfficialMobile  where EmpInfoId = @EmpInfoId

	INSERT  INTO [dbo].[tbl_ReimbursmentFormMaster_HealthCare]
    (
[Type],
[CompanyId],
[FinancialYearId],
[EmpInfoId],
[EmpMasterCode],
[DivisionId],
[DivisionWId],
[DepartmentId],
[SectionId],
[SubSectionId],
[DesignationId],
[EmpCategoryId],
[PatientName],
[PatientAge],
[Relationship],
[IsActive],
[EntryBy],
[RequitisionNo],
[EntryDate],PaymentStatus, ActionStatus,
 SelfDate ,
 SubmitDate , BankName	 ,
 BankAccountNo ,
 BranchName ,
 RoutingNo,  HospitalNameId,HospitalAdmissionDate,HospitalDischargeDate,
 HospitalNotificationEmailDoc,HospitalNotificationEmailFileName
	)
    SELECT
   	@Type,
     @CompanyId ,
     @FinancialYearId ,
     @EmpInfoId ,
	EmpMasterCode,
	[DivisionId],
	[DivisionWId],
	[DepartmentId],
	[SectionId],
	[SubSectionId],
	[DesignationId],
	[EmpCategoryId],
	@PatientName ,
    @PatientAge ,
    @Relationship ,
	1,
	@EntryBy,
	@codeD,
	GETDATE(),'New',@ActionStatus,  @SelfDate ,
 @SubmitDate ,@BankName	 ,
@BankAccountNo ,
@BranchName ,
@RoutingNo	,@HospitalNameId,@HospitalAdmissionDate,@HospitalDischargeDate,
@HospitalNotificationEmailDoc,@HospitalNotificationEmailFileName

	FROM tblEmpGeneralInfo where EmpInfoId = @EmpInfoId


   SELECT SCOPE_IDENTITY()
END
GO

-- 2b. sp_Update_ReimbursmentMaster
ALTER PROCEDURE [dbo].[sp_Update_ReimbursmentMaster]

 @ReimbursFromMasterId INT = 0,
 @Type NVARCHAR(MAX) = NULL,
 @CompanyId INT = NULL,
 @EmpInfoId INT = NULL,
 @FinancialYearId INT = NULL,
 @PatientName  NVARCHAR(MAX) = NULL,
 @OfficialMobile NVARCHAR(MAX) = NULL,
 @PatientAge INT = NULL,
 @Relationship NVARCHAR(max) = NULL,
 @EntryBy INT = NULL,
 @SelfDate NVARCHAR(MAX) NULL,
 @SubmitDate Datetime NULL,
 @ActionStatus NVARCHAR(50) = NULL,

 @BankName	nvarchar(MAX)	= NULL,
@BankAccountNo	nvarchar(MAX)	= NULL,
@BranchName	nvarchar(MAX)	= NULL,
@RoutingNo	nvarchar(MAX)	= NULL,
 @HospitalNameId INT = NULL,
 @HospitalAdmissionDate date=null,
 @HospitalDischargeDate date=null,
 @HospitalNotificationEmailDoc NVARCHAR(500) = NULL,
 @HospitalNotificationEmailFileName NVARCHAR(500) = NULL



AS
BEGIN

DECLARE @Entry_By NVARCHAR(MAX)

SELECT @Entry_By=EntryBy FROM tbl_ReimbursmentFormMaster_HealthCare WHERE ReimbursFromMasterId = @ReimbursFromMasterId


IF(@Entry_By != @EntryBy)
BEGIN

  INSERT INTO [dbo].[tbl_ReimbursmentFormMaster_HealthCareDel]
           ([ReimbursFromMasterId]
           ,[IsOPD]
           ,[RemainingBalance]
           ,[CompanyId]
           ,[FinancialYearId]
           ,[EmpInfoId]
           ,[EmpMasterCode]
           ,[DivisionId]
           ,[DivisionWId]
           ,[DepartmentId]
           ,[SectionId]
           ,[SubSectionId]
           ,[DesignationId]
           ,[EmpCategoryId]
           ,[PatientName]
           ,[PatientAge]
           ,[Relationship]
           ,[IsActive]
           ,[EntryBy]
           ,[EntryDate]
           ,[RequitisionNo]
           ,[ActionStatus]
           ,[PaymentStatus]
           ,[UpdateBy]
           ,[UpdateDate]
           ,[ApprovedBy]
           ,[ApprovedDate]
           ,[DelBy]
           ,[DelDate])

     SELECT [ReimbursFromMasterId]
           ,[IsOPD]
           ,[RemainingBalance]
           ,[CompanyId]
           ,[FinancialYearId]
           ,[EmpInfoId]
           ,[EmpMasterCode]
           ,[DivisionId]
           ,[DivisionWId]
           ,[DepartmentId]
           ,[SectionId]
           ,[SubSectionId]
           ,[DesignationId]
           ,[EmpCategoryId]
           ,[PatientName]
           ,[PatientAge]
           ,[Relationship]
           ,[IsActive]
           ,[EntryBy]
           ,[EntryDate]
           ,[RequitisionNo]
           ,[ActionStatus]
           ,[PaymentStatus]
           ,[UpdateBy]
           ,[UpdateDate]
           ,[ApprovedBy]
           ,[ApprovedDate]
           ,@EntryBy
           ,GETDATE() FROM tbl_ReimbursmentFormMaster_HealthCare WHERE  ReimbursFromMasterId =@ReimbursFromMasterId

END


DECLARE @EmpMasterCode NVARCHAR(MAX)
DECLARE @DivisionId NVARCHAR(MAX)
DECLARE @DivisionWId NVARCHAR(MAX)
DECLARE @DepartmentId NVARCHAR(MAX)
DECLARE @SectionId NVARCHAR(MAX)
DECLARE @SubSectionId NVARCHAR(MAX)
DECLARE @DesignationId NVARCHAR(MAX)
DECLARE @EmpCategoryId NVARCHAR(MAX)


SELECT

    @EmpMasterCode= [EmpMasterCode],
	@DivisionId =[DivisionId],
	@DivisionWId=[DivisionWId],
	@DepartmentId=[DepartmentId],
	@SectionId=[SectionId],
	@SubSectionId=[SubSectionId],
	@DesignationId=[DesignationId],
	@EmpCategoryId=[EmpCategoryId] FROM tblEmpGeneralInfo where EmpInfoId = @EmpInfoId

	update tblEmpGeneralInfo set OfficialMobile=@OfficialMobile  where EmpInfoId = @EmpInfoId

	UPDATE  tbl_ReimbursmentFormMaster_HealthCare
	SET [Type]=@Type,
[CompanyId]=@CompanyId,
[FinancialYearId]=@FinancialYearId,
[EmpInfoId]=@EmpInfoId,
[EmpMasterCode]=@EmpMasterCode,
[DivisionId]=@DivisionId,
[DivisionWId]=@DivisionWId,
[DepartmentId]=@DepartmentId,
[SectionId]=@SectionId,
[SubSectionId]=@SubSectionId,
[DesignationId]=@DesignationId,
[EmpCategoryId]=@EmpCategoryId,
[PatientName]=@PatientName,
[PatientAge]=@PatientAge,
[Relationship]=@Relationship,
SelfDate=@SelfDate,
SubmitDate=@SubmitDate,
[UpdateBy]=@EntryBy,
[UpdateDate]=GETDATE(),
ActionStatus=@ActionStatus, BankName=@BankName	 ,
 BankAccountNo=@BankAccountNo ,
 BranchName =@BranchName,
 RoutingNo=@RoutingNo	, HospitalNameId=@HospitalNameId,  HospitalAdmissionDate=@HospitalAdmissionDate, HospitalDischargeDate=@HospitalDischargeDate,
 HospitalNotificationEmailDoc=@HospitalNotificationEmailDoc, HospitalNotificationEmailFileName=@HospitalNotificationEmailFileName
where ReimbursFromMasterId =@ReimbursFromMasterId

END
GO
