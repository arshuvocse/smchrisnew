using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MenuSetup_DAL
{
    public class MenuSetupCommonDAL
    {
        private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetMainMenuForMenuGroupSetup(string cid="")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));

            const string queryStr = @"SELECT NULL AS MenuGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mm.MenuTypeId, mt.MenuTypeName FROM dbo.tblMainMenu mm INNER JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mm.MenuTypeId WHERE mm.ParantId IS NOT NULL;";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public bool DeleteMenu(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ID", id));

            const string queryStr = @"DELETE FROM dbo.tblActionPageWiseStep WHERE ManuSL=@ID";
            return _aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteMenuGroup(string id,string groupId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ID", id));
            aSqlParameterlist.Add(new SqlParameter("@GroupId", groupId));

            const string queryStr = @"DELETE FROM dbo.tblActionGroupWiseApproval WHERE ManuSL=@ID AND GroupId=@GroupId";
            return _aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveMenuWithCat(string ManuSL, string ASId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ManuSL", ManuSL));
            aSqlParameterlist.Add(new SqlParameter("@ASId", ASId));
            

            const string queryStr = @"INSERT INTO dbo.tblActionPageWiseStep
                                        (
                                            ManuSL,
                                            ASId
                                        )
                                        VALUES
                                        (   @ManuSL,
                                            @ASId
                                        )";

            return _aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveMenuGroup(string ManuSL, string ActionId, string GroupId, bool IsCancel)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ManuSL", ManuSL));
            aSqlParameterlist.Add(new SqlParameter("@ActionId", ActionId));
            aSqlParameterlist.Add(new SqlParameter("@GroupId", GroupId));
            aSqlParameterlist.Add(new SqlParameter("@IsCancel", IsCancel));


            const string queryStr = @"INSERT INTO dbo.tblActionGroupWiseApproval
                                    (
                                        ManuSL,
                                        ActionId,
                                        GroupId,
                                        IsCancel
                                    )
                                    VALUES
                                    (   @ManuSL,
                                        @ActionId,
                                        @GroupId,
                                        @IsCancel
                                    )";

            return _aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetMainMenuByMenuTypeId(string MenuTypeId="")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuTypeId", MenuTypeId));

            const string queryStr = @"SELECT NULL AS MenuGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mm.MenuTypeId, mt.MenuTypeName 
            ,mmp.MenuName AS Parent FROM dbo.tblMainMenu mm 
            INNER JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mm.MenuTypeId 
            INNER JOIN dbo.tblMainMenu mmp ON mmp.SL=mm.ParantId WHERE mm.ParantId IS NOT NULL AND mm.IsInMainMenu=1  AND mm.URL<>'#' ORDER BY mm.ParantId ASC";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuData()
        {


            const string queryStr = @"SELECT * FROM dbo.tblAction WHERE IsShow=1 AND ActionValue<>'Reject'";
            return _aCommonInternalDal.DataContainerDataTable(queryStr,  DataBase.HRDB);
        }
        public DataTable GetMainMenuByMenuTypeId()
        {


            const string queryStr = @"SELECT NULL AS MenuGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mm.MenuTypeId, mt.MenuTypeName 
            ,mmp.MenuName AS Parent FROM dbo.tblMainMenu mm 
            INNER JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mm.MenuTypeId 
            INNER JOIN dbo.tblMainMenu mmp ON mmp.SL=mm.ParantId
            WHERE mm.ParantId IS NOT NULL  ORDER BY mm.ParantId ASC";
            return _aCommonInternalDal.DataContainerDataTable(queryStr,  DataBase.HRDB);
        }
        public DataTable GetMainMenuForMenuApprovalGroupSetup(string cid = "")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));

            const string queryStr = @"SELECT mgd.MenuApprovalGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, 
                mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone,
                mm.Own, mg.MenuApprovalGroupSetupId, mg.CompanyId, mg.MenuApprovalGroupName FROM dbo.tblMainMenu mm 
                LEFT JOIN dbo.tblMenuApprovalGroupSetupDetail mgd ON mgd.MainMenuId=mm.MainMenuId 
                LEFT JOIN dbo.tblMenuApprovalGroupSetup mg ON mg.MenuApprovalGroupSetupId=mgd.MenuApprovalGroupSetupId 
                WHERE mm.ParantId IS NOT NULL AND mm.IsApprovalPage=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMainMenuForMenuApprovalGroupSetupNew(string cid = "")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));

            const string queryStr = @"SELECT *,'0'MenuApprovalGroupSetupDetailId FROM dbo.tblApprovalStepSetup
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblApprovalStepSetup.MainMenuId
INNER JOIN dbo.tblActionPageWiseStep ON dbo.tblApprovalStepSetup.MainMenuId=dbo.tblActionPageWiseStep.ManuSL";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuGroupForMenuGroupPermission(string cid = "1")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));

            const string queryStr = @"SELECT '0'MenuGroupPermissionDtlId, mg.MenuGroupSetupId,mg.CompanyId,mg.MenuGroupName,mg.MenuTypeId,mt.MenuTypeName FROM dbo.tblMenuGroupSetup mg LEFT JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mg.MenuTypeId  WHERE mg.IsActive=1 AND mg.CompanyId=@CompanyId";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuGroupForMenuGroupPermission()
        {
            

            const string queryStr = @"SELECT '0'MenuGroupPermissionDtlId, mg.MenuGroupSetupId,mg.CompanyId,mg.MenuGroupName,mg.MenuTypeId,mt.MenuTypeName FROM dbo.tblMenuGroupSetup mg LEFT JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mg.MenuTypeId  WHERE mg.IsActive=1 ";
            return _aCommonInternalDal.DataContainerDataTable(queryStr,  DataBase.HRDB);
        }
        public DataTable GetMenuApprovalGroupForMenuApprovalGroupPermission(string cid = "1")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));

            const string queryStr = @"SELECT ISNULL(mgp.MenuApprovalGroupPermissionDtlId,0) AS MenuApprovalGroupPermissionDtlId, mg.MenuApprovalGroupSetupId, mg.CompanyId, mg.MenuApprovalGroupName FROM dbo.tblMenuApprovalGroupSetup mg LEFT JOIN dbo.tblMenuApprovalGroupPermissionDtl mgp ON mgp.MenuApprovalGroupSetupId=mg.MenuApprovalGroupSetupId WHERE mg.IsActive=1 AND mg.CompanyId=@CompanyId";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetMenuForApprovalStepSetup(string cid = "1")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));
            //const string queryStr = @"SELECT a.* FROM( SELECT ISNULL(asdm.ApprovalStepSetupMenuDtlId,0) AS ApprovalStepSetupMenuDtlId,mm.MainMenuId,mm.MenuName,ISNULL(asdm.IsActive, 0) AS IsActive FROM dbo.tblApprovalStepSetupMenuDtl asdm INNER JOIN dbo.tblMainMenu mm ON mm.MainMenuId=asdm.MainMenuId INNER JOIN dbo.tblApprovalStepSetup asm ON asm.ApprovalStepSetupId=asdm.ApprovalStepSetupId WHERE asm.CompanyId=@CompanyId AND mm.MenuTypeId=3 AND asm.IsActive=1 UNION SELECT ISNULL(asdm.ApprovalStepSetupMenuDtlId,0) AS ApprovalStepSetupMenuDtlId,mm.MainMenuId,mm.MenuName,ISNULL(asdm.IsActive, 0) AS IsActive FROM dbo.tblMainMenu mm LEFT JOIN dbo.tblApprovalStepSetupMenuDtl asdm ON mm.MainMenuId=asdm.MainMenuId LEFT JOIN dbo.tblApprovalStepSetup asm ON asm.ApprovalStepSetupId=asdm.ApprovalStepSetupId WHERE asdm.ApprovalStepSetupMenuDtlId IS NULL AND mm.MenuTypeId=3) a ORDER BY a.ApprovalStepSetupMenuDtlId DESC;";

            const string queryStr = @"SELECT mm.MainMenuId AS SerialNo,mm.MainMenuId,mm.MenuName FROM dbo.tblMainMenu mm WHERE mm.ParantId IS NULL 
            AND mm.MainMenuId IN  (SELECT DISTINCT mm.ParantId FROM dbo.tblMainMenu mm WHERE mm.MenuTypeId=3 AND mm.ParantId IS NOT NULL)";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetApprovalStepInfo(int ApprovalStepSetupId = 0)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStepSetupId", ApprovalStepSetupId));
            const string queryStr = @"SELECT ISNULL(sd.ApprovalStepSetupStepDtlId,0) AS ApprovalStepSetupStepDtlId,
   i.ApprovalStepInfoId AS Value, i.ApprovalStepName AS TextField , ISNULL(sd.IsActive,0) AS IsActive
  FROM dbo.tblApprovalStepInfo i 
  LEFT JOIN dbo.tblApprovalStepSetupStepDtl sd ON sd.ApprovalStepInfoId = i.ApprovalStepInfoId
  AND sd.ApprovalStepSetupId=@ApprovalStepSetupId
  WHERE i.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMainMenuForMenuGroupSetupByMId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuGroupSetupId", mid));

            const string queryStr = @"select * from (SELECT MenuGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon,
 mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mm.MenuTypeId, mt.MenuTypeName, mgd.[Add] AS PAdd,
  mgd.Edit AS PEdit, mgd.[View] AS PView, mgd.[Delete] AS PDelete, mgd.IsActive ,mmp.MenuName AS Parent
  FROM dbo.tblMainMenu mm 
  INNER JOIN dbo.tblMenuGroupSetupDetail mgd ON mgd.MainMenuId=mm.MainMenuId 
  INNER JOIN dbo.tblMenuGroupSetup mg ON mg.MenuGroupSetupId=mgd.MenuGroupSetupId 
  INNER JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mm.MenuTypeId 
  INNER JOIN dbo.tblMainMenu mmp ON mmp.SL=mm.ParantId
  WHERE mm.ParantId IS NOT NULL AND mg.MenuGroupSetupId=@MenuGroupSetupId AND mgd.IsActive=1  
  
  UNION 
  
  SELECT NULL AS MenuGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, 
  mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mm.MenuTypeId, mt.MenuTypeName, 0 AS PAdd, 0 AS PEdit, 0 AS PView, 
  0 AS PDelete, 0 AS IsActive,mmp.MenuName AS Parent FROM dbo.tblMainMenu mm INNER JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mm.MenuTypeId 
  INNER JOIN dbo.tblMainMenu mmp ON mmp.SL=mm.ParantId
  WHERE mm.ParantId IS NOT NULL AND mm.MainMenuId NOT IN ( SELECT mgd.MainMenuId FROM dbo.tblMenuGroupSetupDetail mgd 
  WHERE mgd.MenuGroupSetupId=@MenuGroupSetupId AND mgd.IsActive=1 ) AND mm.MenuTypeId IN (SELECT mg.MenuTypeId
                                   FROM dbo.tblMenuGroupSetup mg
                                   WHERE mg.MenuGroupSetupId = @MenuGroupSetupId
                                         AND mg.IsActive = 1)) as tbltemp ORDER BY ParantId ASC";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMainMenuForMenuGroupSetupByMIdWithoutType(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuGroupSetupId", mid));

            const string queryStr = @"select distinct * from (SELECT mm.IsInMainMenu, MenuGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon,
 mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mm.MenuTypeId, mt.MenuTypeName, mgd.[Add] AS PAdd,
  mgd.Edit AS PEdit, mgd.[View] AS PView, mgd.[Delete] AS PDelete, mgd.IsActive ,mmp.MenuName AS Parent
  FROM dbo.tblMainMenu mm 
  INNER JOIN dbo.tblMenuGroupSetupDetail mgd ON mgd.MainMenuId=mm.MainMenuId 
  INNER JOIN dbo.tblMenuGroupSetup mg ON mg.MenuGroupSetupId=mgd.MenuGroupSetupId 
  INNER JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mm.MenuTypeId 
  INNER JOIN dbo.tblMainMenu mmp ON mmp.SL=mm.ParantId
  WHERE mm.ParantId IS NOT NULL AND mg.MenuGroupSetupId=@MenuGroupSetupId AND mgd.IsActive=1  and mm.IsInMainMenu=1
  
  UNION 
  
  SELECT mm.IsInMainMenu, NULL AS MenuGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, 
  mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mm.MenuTypeId, mt.MenuTypeName, 0 AS PAdd, 0 AS PEdit, 0 AS PView, 
  0 AS PDelete, 0 AS IsActive,mmp.MenuName AS Parent FROM dbo.tblMainMenu mm INNER JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mm.MenuTypeId 
  INNER JOIN dbo.tblMainMenu mmp ON mmp.SL=mm.ParantId
  WHERE mm.ParantId IS NOT NULL AND mm.MainMenuId NOT IN ( SELECT mgd.MainMenuId FROM dbo.tblMenuGroupSetupDetail mgd 
  WHERE mgd.MenuGroupSetupId=@MenuGroupSetupId AND mgd.IsActive=1 and   mm.IsInMainMenu=1  ) ) as tbltemp where IsInMainMenu=1 ORDER BY ParantId,tbltemp.MainMenuId ASC";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMainMenuForMenuApprovalGroupSetupByMId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuApprovalGroupSetupId", mid));

            const string queryStr = @"SELECT a.*FROM( SELECT mgd.MenuApprovalGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mg.MenuApprovalGroupSetupId, mg.CompanyId, mg.MenuApprovalGroupName, ISNULL(mgd.IsActive, 0) AS IsActive FROM dbo.tblMainMenu mm LEFT JOIN dbo.tblMenuApprovalGroupSetupDetail mgd ON mgd.MainMenuId=mm.MainMenuId LEFT JOIN dbo.tblMenuApprovalGroupSetup mg ON mg.MenuApprovalGroupSetupId=mgd.MenuApprovalGroupSetupId WHERE mg.MenuApprovalGroupSetupId=@MenuApprovalGroupSetupId AND mm.ParantId IS NOT NULL UNION SELECT mgd.MenuApprovalGroupSetupDetailId, mm.MainMenuId, mm.MenuName, mm.URL, mm.ParantId, mm.IsApprovalPage, mm.ModuleId, mm.icon, mm.[Add], mm.[View], mm.Edit, mm.[Delete], mm.Everyone, mm.Own, mg.MenuApprovalGroupSetupId, mg.CompanyId, mg.MenuApprovalGroupName, ISNULL(mgd.IsActive, 0) AS IsActive FROM dbo.tblMainMenu mm LEFT JOIN dbo.tblMenuApprovalGroupSetupDetail mgd ON mgd.MainMenuId=mm.MainMenuId LEFT JOIN dbo.tblMenuApprovalGroupSetup mg ON mg.MenuApprovalGroupSetupId=mgd.MenuApprovalGroupSetupId WHERE mg.MenuApprovalGroupSetupId IS NULL AND mm.ParantId IS NOT NULL) a ORDER BY a.MenuApprovalGroupSetupDetailId DESC;";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMainMenuForMenuApprovalGroupSetupNewByMId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuApprovalGroupSetupId", mid));

            const string queryStr = @"SELECT * FROM (SELECT tblApprovalStepSetup.MainMenuId,MenuName,MenuApprovalGroupSetupDetailId,tblMenuApprovalGroupSetupDetail.IsActive,ActionId,IsCancel,ASId  FROM dbo.tblApprovalStepSetup
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblApprovalStepSetup.MainMenuId
LEFT JOIN dbo.tblMenuApprovalGroupSetupDetail ON tblMenuApprovalGroupSetupDetail.MainMenuId = tblApprovalStepSetup.MainMenuId
LEFT JOIN dbo.tblActionGroupWiseApproval ON dbo.tblMenuApprovalGroupSetupDetail.MainMenuId=dbo.tblActionGroupWiseApproval.ManuSL AND GroupId=MenuApprovalGroupSetupId
INNER JOIN dbo.tblActionPageWiseStep ON dbo.tblApprovalStepSetup.MainMenuId=dbo.tblActionPageWiseStep.ManuSL WHERE MenuApprovalGroupSetupId=@MenuApprovalGroupSetupId
UNION ALL

SELECT tblApprovalStepSetup.MainMenuId,MenuName,'0'MenuApprovalGroupSetupDetailId ,'0'IsActive,'0'ActionId,'0'IsCancel,ASId FROM dbo.tblApprovalStepSetup
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblApprovalStepSetup.MainMenuId
INNER JOIN dbo.tblActionPageWiseStep ON dbo.tblApprovalStepSetup.MainMenuId=dbo.tblActionPageWiseStep.ManuSL
 WHERE tblApprovalStepSetup.MainMenuId NOT IN
(SELECT tblApprovalStepSetup.MainMenuId  FROM dbo.tblApprovalStepSetup
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblApprovalStepSetup.MainMenuId
LEFT JOIN dbo.tblMenuApprovalGroupSetupDetail ON tblMenuApprovalGroupSetupDetail.MainMenuId = tblApprovalStepSetup.MainMenuId WHERE MenuApprovalGroupSetupId=@MenuApprovalGroupSetupId) ) AS tbltemp";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuGroupForMenuGroupPermissionByMId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuGroupPermissionId", mid));

            //const string queryStr = @"SELECT a.* FROM( SELECT mgpd.MenuGroupPermissionDtlId, mg.MenuGroupSetupId, mg.MenuGroupName, mg.MenuTypeId, mt.MenuTypeName, ISNULL(mgpd.IsActive, 0) AS IsActive FROM dbo.tblMenuGroupSetup mg LEFT JOIN dbo.tblMenuGroupPermissionDtl mgpd ON mgpd.MenuGroupSetupId=mg.MenuGroupSetupId LEFT JOIN dbo.tblMenuGroupPermission mgp ON mgp.MenuGroupPermissionId=mgpd.MenuGroupPermissionId LEFT JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mg.MenuTypeId WHERE mgp.MenuGroupPermissionId=@MenuGroupPermissionId UNION SELECT ISNULL(mgpd.MenuGroupPermissionDtlId,0) AS MenuGroupPermissionDtlId, mg.MenuGroupSetupId, mg.MenuGroupName, mg.MenuTypeId, mt.MenuTypeName, ISNULL(mgpd.IsActive, 0) AS IsActive FROM dbo.tblMenuGroupSetup mg LEFT JOIN dbo.tblMenuGroupPermissionDtl mgpd ON mgpd.MenuGroupSetupId=mg.MenuGroupSetupId LEFT JOIN dbo.tblMenuGroupPermission mgp ON mgp.MenuGroupPermissionId=mgpd.MenuGroupPermissionId LEFT JOIN dbo.tblMenuTypeSetup mt ON mt.MenuTypeId=mg.MenuTypeId WHERE mgpd.MenuGroupPermissionDtlId IS NULL) a ORDER BY a.MenuGroupPermissionDtlId DESC;";
            const string queryStr = @"SELECT a.*
FROM
(
    SELECT mgpd.MenuGroupPermissionDtlId,
           mg.MenuGroupSetupId,
           mg.MenuGroupName,
           mg.MenuTypeId,
           mt.MenuTypeName,
           ISNULL(mgpd.IsActive, 0) AS IsActive
    FROM dbo.tblMenuGroupSetup mg
        LEFT JOIN dbo.tblMenuGroupPermissionDtl mgpd
            ON mgpd.MenuGroupSetupId = mg.MenuGroupSetupId
        LEFT JOIN dbo.tblMenuGroupPermission mgp
            ON mgp.MenuGroupPermissionId = mgpd.MenuGroupPermissionId
        LEFT JOIN dbo.tblMenuTypeSetup mt
            ON mt.MenuTypeId = mg.MenuTypeId
    WHERE mgp.MenuGroupPermissionId = @MenuGroupPermissionId
    UNION
    SELECT  0 AS MenuGroupPermissionDtlId,
           mg.MenuGroupSetupId,
           mg.MenuGroupName,
           mg.MenuTypeId,
           mt.MenuTypeName,
            0 AS IsActive
    FROM dbo.tblMenuGroupSetup mg
        INNER JOIN dbo.tblMenuTypeSetup mt
            ON mt.MenuTypeId = mg.MenuTypeId
		INNER JOIN dbo.tblMenuGroupPermissionDtl mgpd
            ON mgpd.MenuGroupSetupId = mg.MenuGroupSetupId
    WHERE mg.IsActive=1 AND mg.MenuGroupSetupId NOT IN (SELECT mgpd.MenuGroupSetupId FROM dbo.tblMenuGroupPermissionDtl mgpd
	WHERE mgpd.MenuGroupPermissionId = @MenuGroupPermissionId)
) a
ORDER BY a.MenuGroupSetupId ASC;";





            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetMenuApprovalGroupForMenuApprovalGroupPermissionByMId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuApprovalGroupPermissionId", mid));

            const string queryStr = @"SELECT a.* FROM( SELECT mgpd.MenuApprovalGroupPermissionDtlId, mg.MenuApprovalGroupSetupId, mg.MenuApprovalGroupName, ISNULL(mgpd.IsActive, 0) AS IsActive FROM dbo.tblMenuApprovalGroupSetup mg LEFT JOIN dbo.tblMenuApprovalGroupPermissionDtl mgpd ON mgpd.MenuApprovalGroupSetupId=mg.MenuApprovalGroupSetupId LEFT JOIN dbo.tblMenuApprovalGroupPermission mgp ON mgp.MenuApprovalGroupPermissionId=mgpd.MenuApprovalGroupPermissionId WHERE mgp.MenuApprovalGroupPermissionId=@MenuApprovalGroupPermissionId UNION SELECT ISNULL(mgpd.MenuApprovalGroupPermissionDtlId, 0) AS MenuApprovalGroupPermissionDtlId, mg.MenuApprovalGroupSetupId, mg.MenuApprovalGroupName, ISNULL(mgpd.IsActive, 0) AS IsActive FROM dbo.tblMenuApprovalGroupSetup mg LEFT JOIN dbo.tblMenuApprovalGroupPermissionDtl mgpd ON mgpd.MenuApprovalGroupSetupId=mg.MenuApprovalGroupSetupId LEFT JOIN dbo.tblMenuApprovalGroupPermission mgp ON mgp.MenuApprovalGroupPermissionId=mgpd.MenuApprovalGroupPermissionId WHERE mgpd.MenuApprovalGroupPermissionDtlId IS NULL) a ORDER BY a.MenuApprovalGroupPermissionDtlId DESC";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuGroupSetupList(string cid = "")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));

            const string queryStr = @"SELECT mg.MenuGroupSetupId,mg.CompanyId,mg.MenuTypeId,c.ShortName,mg.MenuGroupName,mt.MenuTypeName,STUFF( (SELECT CONCAT(',', MenuName , '') FROM tblMainMenu mm (NOLOCK) INNER JOIN dbo.tblMenuGroupSetupDetail mgd ON mgd.MainMenuId=mm.MainMenuId WHERE mgd.MenuGroupSetupId=mg.MenuGroupSetupId AND mgd.IsActive=1 ORDER BY mgd.MenuGroupSetupDetailId FOR XML PATH ('') ),1,1,'') as MenuName FROM dbo.tblMenuGroupSetup mg LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId=mg.CompanyId LEFT JOIN dbo.tblMenuTypeSetup mt ON mg.MenuTypeId=mt.MenuTypeId WHERE mg.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuGroupPermissionList(string cid = "")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));

            const string queryStr = @"SELECT mgp.MenuGroupPermissionId ,mgp.CompanyId ,mgp.UserId, u.UserName, c.ShortName, STUFF( ( SELECT CONCAT(',', mg.MenuGroupName, '') FROM dbo.tblMenuGroupSetup mg (NOLOCK) INNER JOIN dbo.tblMenuGroupPermissionDtl mgpd ON mgpd.MenuGroupSetupId=mg.MenuGroupSetupId WHERE mgpd.MenuGroupPermissionId=mgp.MenuGroupPermissionId AND mgpd.IsActive=1 ORDER BY mg.MenuGroupSetupId FOR XML PATH('') ), 1, 1, '' ) AS MenuGroupName FROM dbo.tblMenuGroupPermission mgp INNER JOIN dbo.tblUser u ON u.UserId=mgp.UserId LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId=mgp.CompanyId WHERE mgp.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);

        }
        public DataTable GetMenuApprovalGroupPermissionList(string cid = "")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));

            const string queryStr = @"SELECT mgp.MenuApprovalGroupPermissionId, mgp.CompanyId, mgp.UserId, u.UserName, c.ShortName, STUFF( ( SELECT CONCAT(',', mg.MenuApprovalGroupName, '') FROM dbo.tblMenuApprovalGroupSetup mg (NOLOCK) INNER JOIN dbo.tblMenuApprovalGroupPermissionDtl mgpd ON mgpd.MenuApprovalGroupSetupId=mg.MenuApprovalGroupSetupId WHERE mgpd.MenuApprovalGroupPermissionId=mgp.MenuApprovalGroupPermissionId AND mgpd.IsActive=1 ORDER BY mg.MenuApprovalGroupSetupId FOR XML PATH('') ), 1, 1, '' ) AS MenuApprovalGroupName FROM HRIS_SMC.dbo.tblMenuApprovalGroupPermission mgp INNER JOIN dbo.tblUser u ON u.UserId=mgp.UserId INNER JOIN dbo.tblCompanyInfo c ON c.CompanyId=mgp.CompanyId WHERE mgp.IsActive=1;";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);

        }
        public DataTable GetMenuApprovalGroupSetupList(string cid = "")
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));

            const string queryStr = @"SELECT mg.MenuApprovalGroupSetupId, mg.CompanyId, c.ShortName, mg.MenuApprovalGroupName, STUFF( ( SELECT CONCAT(',', MenuName, '') FROM tblMainMenu mm (NOLOCK) INNER JOIN dbo.tblMenuApprovalGroupSetupDetail mgd ON mgd.MainMenuId=mm.MainMenuId WHERE mgd.MenuApprovalGroupSetupId=mg.MenuApprovalGroupSetupId AND mgd.IsActive=1 ORDER BY mgd.MenuApprovalGroupSetupDetailId FOR XML PATH('') ), 1, 1, '' ) AS MenuName FROM dbo.tblMenuApprovalGroupSetup mg LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId=mg.CompanyId WHERE mg.IsActive=1;";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetMenuGroupDetailsByMId(string MenuGroupSetupId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuGroupSetupId", MenuGroupSetupId));

            const string queryStr = @"SELECT  mm.MenuName AS TextField FROM dbo.tblMainMenu mm
INNER JOIN dbo.tblMenuGroupSetupDetail mgd ON mgd.MainMenuId = mm.MainMenuId
WHERE mgd.IsActive=1 AND mgd.MenuGroupSetupId=@MenuGroupSetupId";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuById(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MainMenuId", id));

            const string queryStr = @"SELECT * FROM dbo.tblMainMenu WHERE MainMenuId=@MainMenuId ";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetMenuBySl(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MainMenuId", id));

            const string queryStr = @"SELECT * FROM dbo.tblMainMenu WHERE SL=@MainMenuId ";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable MenuSL(string sl)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            DataTable aTableMainMenu = new DataTable();
            string queryString = "select * from tblMainMenu where SL='" + sl + "'";
            aTableMainMenu = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aTableMainMenu;
        }
        public DataTable GetChildMenuByParentID(string MainMenuId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MainMenuId", MainMenuId));
            //const string queryStr = @"SELECT a.* FROM( SELECT ISNULL(asdm.ApprovalStepSetupMenuDtlId,0) AS ApprovalStepSetupMenuDtlId,mm.MainMenuId,mm.MenuName,ISNULL(asdm.IsActive, 0) AS IsActive FROM dbo.tblApprovalStepSetupMenuDtl asdm INNER JOIN dbo.tblMainMenu mm ON mm.MainMenuId=asdm.MainMenuId INNER JOIN dbo.tblApprovalStepSetup asm ON asm.ApprovalStepSetupId=asdm.ApprovalStepSetupId WHERE asm.CompanyId=@CompanyId AND mm.MenuTypeId=3 AND asm.IsActive=1 UNION SELECT ISNULL(asdm.ApprovalStepSetupMenuDtlId,0) AS ApprovalStepSetupMenuDtlId,mm.MainMenuId,mm.MenuName,ISNULL(asdm.IsActive, 0) AS IsActive FROM dbo.tblMainMenu mm LEFT JOIN dbo.tblApprovalStepSetupMenuDtl asdm ON mm.MainMenuId=asdm.MainMenuId LEFT JOIN dbo.tblApprovalStepSetup asm ON asm.ApprovalStepSetupId=asdm.ApprovalStepSetupId WHERE asdm.ApprovalStepSetupMenuDtlId IS NULL AND mm.MenuTypeId=3) a ORDER BY a.ApprovalStepSetupMenuDtlId DESC;";

            const string queryStr = @"SELECT ISNULL(asts.ApprovalStepSetupId,0) AS ApprovalStepSetupId ,mm.MainMenuId,
       mm.MenuName ,ISNULL(asts.IsActive,0) AS	IsActive
FROM dbo.tblMainMenu mm 
LEFT JOIN dbo.tblApprovalStepSetup asts ON mm.MainMenuId = asts.MainMenuId
WHERE mm.ParantId = @MainMenuId
      AND mm.MenuTypeId = 3
      AND mm.ParantId IS NOT NULL;";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
    }
}
