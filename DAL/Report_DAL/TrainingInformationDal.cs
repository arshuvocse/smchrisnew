using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
    public class TrainingInformationDal
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public void TrainingRecordDropDown(DropDownList ddl, string companyId, string finYearId)
        {
            string queryStr = @"SELECT * FROM dbo.tblTrainingRecordMaster WHERE   ( IsDelete IS NULL OR IsDelete = 0)   AND CompanyId='" + companyId + "' AND FinancialYearId='" + finYearId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "TrainingTitle", "TrainingRecordMasterId", queryStr, DataBase.HRDB);
        }

        public DataTable GetTrainingAttendanceReport(string generateParameter)
        {
            string query = @"SELECT  COUNT(TRD.EmpInfoId) TotalParticipant , TYP.TrainingType , TRM.TrainingTitle ,  TRM.StartDate,  
                             TRM.EndDate,TRM.TotalHoure,TRM.NoOfDays, ORG.TrainingOrgName,
                             TRM.TrainingCost, trm.LogisticCost, TRM.OtherCost, TRM.GrandTotal,  STV.VenueName,CON.Title AS Place 
                             FROM dbo.tblTrainingRecordMaster AS TRM
                             LEFT JOIN dbo.tbl_trainingRecordDetailsEmployee AS TRD ON TRD.TrainingRecordMasterId = TRM.TrainingRecordMasterId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = TRD.EmpInfoId
                             LEFT JOIN dbo.tblDesignation AS DSG ON DSG.DesignationId = EGI.DesignationId
                             LEFT JOIN dbo.tblTrainingOrgInfo AS ORG ON ORG.TrainingOrgId = TRM.TrainingOrgId
                             LEFT JOIN dbo.tblSMCTrainingVenue AS STV ON STV.SMCVenueID = TRM.TrainingVenue 
                             --LEFT JOIN (SELECT SS.TrainingBudget2Id, (SELECT  SS.BudgetHead FROM tblTrainingBudget2Details US 
                             --WHERE US.TrainingBudget2Id = SS.TrainingBudget2Id FOR XML PATH('')) [Quater] FROM tblTrainingBudget2Master SS
                             --GROUP BY SS.TrainingBudget2Id) AS TBZ ON TRM.TrainingBudget2Id = TBZ.TrainingBudget2Id
                             LEFT JOIN dbo.tblCountry AS CON ON CON.CountryID = TRM.TrainingOrgLocation
                             LEFT JOIN dbo.tblTrainingType AS TYP ON TRM.TrainingTypeId = TYP.TrainingTypeID
                             LEFT JOIN dbo.tblFinancialYear AS FNY ON FNY.FinancialYearId = TRM.FinancialYearId
                             LEFT JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TRM.CompanyId WHERE TRM.TrainingRecordMasterId IS NOT NULL AND (TRM.IsDelete is null or TRM.IsDelete = 0)

							  " + generateParameter + @" GROUP BY TYP.TrainingType , TRM.TrainingTitle ,  TRM.StartDate,  
                             TRM.EndDate,TRM.TotalHoure,TRM.NoOfDays, ORG.TrainingOrgName,
                             TRM.TrainingCost, trm.LogisticCost, TRM.OtherCost, TRM.GrandTotal,  STV.VenueName,CON.Title";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }


        public DataTable GetTrainingDetailsReport(string generateParameter)
        {
            string query = @"SELECT TrM.TrainingTitle, 'S.Date: '+ FORMAT(TRM.StartDate, 'dd-MMM-yyyy') + ' to E.Date: ' +  FORMAT(TRM.EndDate,'dd-MMM-yyy') AS TrainingDate,
EG.EmpMasterCode, EG.EmpName, DS.Designation, DP.DivisionName, CAST( TrM.NoOfDays AS varchar) +' Days '+ CAST( TrM.TotalHoure AS varchar)+' Hours' AS Durration ,
tbldate.TotalTime, ORG.TrainingOrgName,  TrM.CostPerParticipant,CASE
    WHEN TrainingOrgLocation = 0 THEN vn.VenueName
    WHEN TrainingOrgLocation != 0 THEN brn.TrainingOrgName  
END AS TrainingPlace, TrM.TotalHoure ManHour
FROM tbl_trainingRecordDetailsEmployee part
INNER  JOIN tblTrainingRecordMaster   TrM ON TrM.TrainingRecordMasterId = part.TrainingRecordMasterId
INNER JOIN tblEmpGeneralInfo EG ON EG.EmpInfoId = part.EmpInfoId
left JOIN (SELECT TOP 1  TrainingRecordMasterId, 'S.Time: '+ CAST(StartTime AS NVARCHAR(10)) +' E.Time: '+  CAST(EndTime AS NVARCHAR(10)) AS TotalTime FROM   tblTrainingRecordScheDate  ) tbldate ON tbldate.TrainingRecordMasterId = TrM.TrainingRecordMasterId 
left JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
LEFT JOIN dbo.tblDivision DP ON DP.DivisionId = EG.DivisionId
LEFT JOIN dbo.tblTrainingOrgInfo AS ORG ON ORG.TrainingOrgId = TRM.TrainingOrgId
LEFT JOIN dbo.tblSMCTrainingVenue AS STV ON STV.SMCVenueID = TRM.TrainingVenue 
LEFT JOIN tblSMCTrainingVenue vn ON trm.TrainingVenue= vn.SMCVenueID
inner JOIN tblTrainingOrgInfo brn ON brn.TrainingOrgId = trm.TrainingOrgId


WHERE TRM.TrainingRecordMasterId IS NOT NULL AND (TRM.IsDelete is null or TRM.IsDelete = 0)

							  " + generateParameter ;
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
    }
}
