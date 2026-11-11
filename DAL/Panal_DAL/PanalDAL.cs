using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.Panal_Entities;

namespace DAL.Panal_DAL
{
  public class PanalDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public bool MenuSaveDal(ObjPanal aObjPanal)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SL", aObjPanal.SL));
            aSqlParameterlist.Add(new SqlParameter("@ManuName", aObjPanal.ManuName));
            aSqlParameterlist.Add(new SqlParameter("@URL", aObjPanal.URL));
            aSqlParameterlist.Add(new SqlParameter("@ParantId", aObjPanal.ParantId));
            aSqlParameterlist.Add(new SqlParameter("@IsApprovalPage", aObjPanal.IsApprovalPage));
            aSqlParameterlist.Add(new SqlParameter("@ModuleId", aObjPanal.ModuleId));
            string query = @"INSERT INTO dbo.tblMainMenu ( SL ,ManuName ,URL ,ParantId,IsApprovalPage,ModuleId) VALUES  ( @SL ,@ManuName ,@URL ,@ParantId,@IsApprovalPage,@ModuleId)";
            return aCommonInternalDal.SaveDataByInsertCommand(query, aSqlParameterlist, "HRDB");
        }

      public bool ApprovalPageStepsSave(ObjActionPageWiseStep actionPageWise)
      {
          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@PWASId", actionPageWise.PWASId));
          aSqlParameterlist.Add(new SqlParameter("@ManuSL", actionPageWise.ManuSL));
          aSqlParameterlist.Add(new SqlParameter("@ASId", actionPageWise.ASId));
          string query = @"INSERT INTO dbo.tblActionPageWiseStep ( PWASId ,ManuSL ,ASId) VALUES  (@PWASId,@ManuSL,@ASId)";
          return aCommonInternalDal.SaveDataByInsertCommand(query, aSqlParameterlist, "HRDB");
      }

      public bool ApprovalPageStepsDelete(int sl)
      {
          string query = @"DELETE dbo.tblActionPageWiseStep WHERE ManuSL='"+sl.ToString()+"'";
          return  aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");

      }
      public DataTable GetApprovalPageSteps(int ManuSL)
      {
          string query = @"select * from tblActionPageWiseStep where ManuSL='" + ManuSL + "'";
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }

        public bool MenuPermissionRemove(int sl, int userId)
        {
            string parantId = Convert.ToString(sl);
            string query = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE SL =" + sl + " ) AND UserId=" + userId + "";
            string query1 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "' ) AND UserId=" + userId + "";
            string query2 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "') ) AND UserId=" + userId + "";
            string query3 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "')) ) AND UserId=" + userId + "";

            bool ok = aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
            bool ok1 = aCommonInternalDal.DeleteDataByDeleteCommand(query1, "HRDB");
            bool ok2 = aCommonInternalDal.DeleteDataByDeleteCommand(query2, "HRDB");
            bool ok3 = aCommonInternalDal.DeleteDataByDeleteCommand(query3, "HRDB");

            return true;
        }



        public void MainMenuDropDown(DropDownList aDropDownList)
        {
            string query = "select * from tblMainMenu where ParantId is null or ParantId=''";
            aCommonInternalDal.LoadDropDownValue(aDropDownList, "ManuName", "SL", query, "HRDB");
        }

        public void ActionStepDropDown(DropDownList aDropDownList)
        {
            string query = "select * from tblActionSteps order by ASId";
            aCommonInternalDal.LoadDropDownValue(aDropDownList, "ActionSteps", "ASId", query, "HRDB");
        }
        public void MenuDropDown(DropDownList aDropDownList, string id)
        {
            string query = "select * from tblMainMenu where  ParantId='" + id + "'";
            aCommonInternalDal.LoadDropDownValue(aDropDownList, "ManuName", "SL", query, "HRDB");

        }

        public bool CheckMenuSl(int MenuSl, int userId)
        {
            string query = @"select * from tblMenuDistribution where MenuSL=" + MenuSl + " and UserId=" + userId + "";
            DataTable aTable = new DataTable();
            aTable = aCommonInternalDal.DataContainerDataTable(query, "HRDB");
            if (aTable.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        public string GetParantId(int sL)
        {
            string parantId = string.Empty;
            string query = "select * from tblMainMenu where  SL=" + sL + "";
            DataTable aTable = new DataTable();
            aTable = aCommonInternalDal.DataContainerDataTable(query, "HRDB");
            parantId = aTable.Rows[0]["ParantId"].ToString();

            return parantId;
        }

        public bool SaveMainMenu(int MenuSl, int userId)
        {
            ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();
            int SL = aClsPrimaryKeyFind.PrimaryKeyMax("SL", "tblMenuDistribution", "HRDB");

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SL", SL));
            aSqlParameterlist.Add(new SqlParameter("@UserId", userId));
            aSqlParameterlist.Add(new SqlParameter("@MenuSL", MenuSl));

            string query = @"INSERT INTO tblMenuDistribution ( SL ,UserId ,MenuSL ,Status) VALUES  ( @SL ,@UserId ,@MenuSL ,  'True'  )";
            return aCommonInternalDal.SaveDataByInsertCommand(query, aSqlParameterlist, "HRDB");
        }



        public void UserDdl(DropDownList userDropDownList)
        {
            string query = "select * from tblUser order by LoginName ";
            aCommonInternalDal.LoadDropDownValue(userDropDownList, "LoginName", "UserId", query, "HRDB");
        }

        public void MainMenu(DropDownList userDropDownList, string userId)
        {
            string query = "select * from tblMainMenu INNER JOIN  tblMenuDistribution ON tblMainMenu.SL=tblMenuDistribution.MenuSL  where UserId='" + userId + "' and ( ParantId IS NULL OR ParantId='')";
            aCommonInternalDal.LoadDropDownValue(userDropDownList, "ManuName", "SL", query, "HRDB");
        }
        public DataTable MainMenuLoad(string userId)
        {
            string query = @"   SELECT  tbltemp.* FROM  (SELECT MM.*,ISNULL(MD.Status,'False') AS Status  FROM dbo.tblMainMenu MM " +
                              " LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL " +
                              " WHERE MD.UserId = '" + userId + "'  " +
                              " union  " +
                              " SELECT MM.*, Status=0  FROM dbo.tblMainMenu MM   " +
                              " WHERE MM.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')  )   " +
                              " AS tbltemp WHERE tbltemp.ParantId IS NULL OR tbltemp.ParantId =''   " +
                              " ORDER BY tbltemp.SL";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable ApprovalMenuLoad(string parantId)
        {
            string Query = @" SELECT tblA.* FROM(SELECT * FROM dbo.tblMainMenu WHERE ParantId='"+parantId+"' "+
                              "  UNION ALL "+
                              "    SELECT * FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId='"+parantId+"') "+
                               "   UNION ALL "+
                             "     SELECT * FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId='"+parantId+"'))) "+
                             "     AS tblA WHERE tblA.IsApprovalPage=1 ORDER BY tblA.SL ASC";


            return aCommonInternalDal.DataContainerDataTable(Query, "HRDB");
        }

        public void ApprovalManuDropDown(DropDownList userDropDownList, string userId, string parantId)
        {
            string Query = @" SELECT MM.*,ISNULL(MD.Status,'False') AS [Status]  FROM dbo.tblMainMenu MM      "+
                             " INNER JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL      " +
                           //" INNER JOIN dbo.tblActionUserWiseApproval AA ON MD.MenuSL =AA.ManuSL AND MD.UserId =AA.UserId " +
                           " INNER JOIN dbo.tblActionPageWiseStep AA ON MD.MenuSL =AA.ManuSL" +
                             " WHERE (MM.ParantId IS NOT NULL OR MM.ParantId <>'') and MD.UserId = '" + userId + "' AND MM.ModuleId='" + parantId + "' AND MM.IsApprovalPage=1 AND MD.Status=1 ORDER BY ManuName ASC";
            aCommonInternalDal.LoadDropDownValue(userDropDownList, "ManuName", "SL", Query, "HRDB");
        }
        public DataTable OtherMenuLoad(string userId, string parantId)
        {
            //string query = @"   SELECT  tbltemp.* FROM  (SELECT MM.*,ISNULL(MD.Status,'False') AS Status  FROM dbo.tblMainMenu MM " +
            //                  " LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL " +
            //                  " WHERE MD.UserId = '" + userId + "'  " +
            //                  " union  " +
            //                  " SELECT MM.*, Status=0  FROM dbo.tblMainMenu MM   " +
            //                  " WHERE MM.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')  )   " +
            //                  " AS tbltemp WHERE  tbltemp.ParantId ='" + parantId + "'   " +
            //                  " ORDER BY tbltemp.SL";


            string Query = @" SELECT tblSelected.* FROM  (SELECT tblMenuTemp.*,tbl1.[Status]  FROM  (SELECT * FROM dbo.tblMainMenu WHERE  " +
                                "  ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "') " +

                                "    UNION " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "')) " +

                                 "   UNION  " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))))   " +
                                "   AS tblMenuTemp   " +

                                "   INNER JOIN (SELECT MM.*,ISNULL(MD.Status,'False') AS [Status]  FROM dbo.tblMainMenu MM   " +
                                "   LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL   " +
                                "   WHERE (MM.ParantId IS NOT NULL OR MM.ParantId <>'') and MD.UserId = '" + userId + "') AS tbl1 ON tblMenuTemp.SL = tbl1.SL)   " +
                                "   AS tblSelected  " +


                               "    UNION   " +

                                "   SELECT tblMenuTemp.* ,Status=0 FROM  (SELECT * FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "')  " +

                                "    UNION  " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))  " +

                                "    UNION  " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                               "    ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                               "    ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))))   " +
                              "     AS tblMenuTemp WHERE tblMenuTemp.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')";


            return aCommonInternalDal.DataContainerDataTable(Query, "HRDB");
        }
        public void LoadActionDAL(RadioButtonList rdl)
        {
            aCommonInternalDal.LoadAction(rdl);

        }

        public DataTable GetManuWiseSelectedActionDAL(ObjUserWiseApprovalPermission approvalPermission)
      {
          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@UserId", approvalPermission.UserId));
          aSqlParameterlist.Add(new SqlParameter("@ManuSL", approvalPermission.ManuSL));
         
          string query = @"SELECT * from tblActionUserWiseApproval WHERE UserId=@UserId AND ManuSL=@ManuSL";
          return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
      }

      public DataTable GetActionPageWiseStepDAL(string manuSL)
      {
          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@ManuSL", manuSL));
          string query = @"SELECT * from tblActionPageWiseStep WHERE ManuSL=@ManuSL";
          return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
      }

      public bool UserWiseApprovalPermissionDAL(ObjUserWiseApprovalPermission approvalPermission)
      {
          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@UWAId", approvalPermission.UWAId));
          aSqlParameterlist.Add(new SqlParameter("@UserId", approvalPermission.UserId));
          aSqlParameterlist.Add(new SqlParameter("@LoginName", approvalPermission.LoginName));
          aSqlParameterlist.Add(new SqlParameter("@ManuSL", approvalPermission.ManuSL));
          aSqlParameterlist.Add(new SqlParameter("@ActionId", approvalPermission.ActionId));

          string deleteQuery = @"DELETE tblActionUserWiseApproval WHERE UserId=@UserId AND ManuSL=@ManuSL";
          aCommonInternalDal.DeleteDataByDeleteCommand(deleteQuery, aSqlParameterlist,"HRDB");

          string query = @"INSERT INTO dbo.tblActionUserWiseApproval
        ( UWAId ,
          UserId ,
          LoginName ,
          ManuSL ,
          ActionId
        )
VALUES  ( @UWAId ,
          @UserId ,
          @LoginName ,
          @ManuSL ,
          @ActionId
        )";
          return aCommonInternalDal.SaveDataByInsertCommand(query, aSqlParameterlist, "HRDB");
      }
    }
}
