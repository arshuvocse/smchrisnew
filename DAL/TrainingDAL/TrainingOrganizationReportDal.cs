using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.TrainingDAL
{
    public class TrainingOrganizationReportDal
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetOrganizationList(string param)
        {
            try
            {
                string query = @"SELECT  * , CASE WHEN a.IsForeign =1 THEN 'Foreign' WHEN a.IsLocal=1 then'Local' ELSE 'Inhouse' END AS Origin
                    FROM    tblTrainingOrgInfo A
                    LEFT JOIN tblOrganizationType B ON A.OrgTypeId = B.OrgTypeId
                    Left join tblCompanyInfo C on a.CompanyId = c.CompanyId
                    WHERE   IsDelete IS NULL
                    OR IsDelete = 0 " + param;
                DataTable dt = _aCommonInternalDal.DataContainerDataTable(query, DataBase.H);
                return dt;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public DataTable GetTrainingOrgReport(string generateParameter)
        {
              string query = @"SELECT CI.CompanyName,ORG.TrainingOrgName,OGT.OrgTypeName,CTRY.Title,ORG.ContactPerson,ORG.ContactPersonEmail,
                             ORG.ContactPersonCell,ORG.OrgAddress,ORG.OrgProfile,
                             CASE WHEN ORG.IsForeign = 1 AND ORG.IsLocal = 0 THEN 'Foreign' WHEN ORG.IsLocal=1 AND ORG.IsForeign= 0 THEN 'Local' END AS ForeignLocal,
                             CASE WHEN ORG.VendorAudit = 1 THEN 'Yes' WHEN  ORG.VendorAudit = 0 THEN 'No' END AS VendorAudit, 
                             CASE WHEN ORG.ClientsRecommendation = 1 THEN 'Yes' WHEN  ORG.ClientsRecommendation = 0 THEN 'No' END AS ClientsRecommendation,
                             CASE WHEN ORG.LogisticsFacility = 1 THEN 'Yes' WHEN  ORG.LogisticsFacility = 0 THEN 'No' END AS LogisticsFacility,
                             CASE WHEN ORG.HasTin = 1 THEN 'Yes' WHEN  ORG.HasTin = 0 THEN 'No' END AS HasTin,
                             CASE WHEN ORG.HasVat = 1 THEN 'Yes' WHEN  ORG.HasVat = 0 THEN 'No' END AS HasVat,
                             CASE WHEN ORG.HasTradeLicense = 1 THEN 'Yes' WHEN  ORG.HasTradeLicense = 0 THEN 'No' END AS HasTradeLicense,
                             CASE WHEN ORG.HasBankSolv = 1 THEN 'Yes' WHEN  ORG.HasBankSolv = 0 THEN 'No' END AS HasBankSolv,
                             CASE WHEN ORG.Others = 1 THEN 'Yes' WHEN  ORG.Others = 0 THEN 'No' END AS Others              
                             FROM dbo.tblTrainingOrgInfo AS ORG
                             LEFT JOIN dbo.tblTrainerInfo AS TI ON TI.TrainingOrgId = ORG.TrainingOrgId
                             LEFT JOIN dbo.tblOfficeBranch AS OFC ON OFC.TrainingOrgId = ORG.TrainingOrgId
                             LEFT JOIN dbo.tblOrganizationType AS OGT ON OGT.OrgTypeId = ORG.OrgTypeId
                             LEFT JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = ORG.CompanyId
                             LEFT JOIN dbo.tblCountry AS CTRY ON CTRY.CountryID = ORG.CountryID" + generateParameter;
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public void LoadddlOrganization(DropDownList ddl, string companyId)
        {
            string queryStr = @"SELECT TrainingOrgId,TrainingOrgName FROM dbo.tblTrainingOrgInfo WHERE IsDelete IS NULL OR IsDelete != 1 AND CompanyId = '" + companyId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "TrainingOrgName", "TrainingOrgId", queryStr, DataBase.HRDB);
        }
    }
}
