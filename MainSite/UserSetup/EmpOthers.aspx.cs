using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_EmpOthers : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != "")
        {
            _userId = Convert.ToString(Session["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            LoadDropDownList();

            
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        empMasterCode.Text = emp.EmpMasterCode;
                        lblEmpName.Text = emp.EmpName;
                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }
                        using (DataTable dtExtraCurriculam = _commonDataLoad.GetDTEmpExtraCurriculamByEmpId(mid))
                        {
                            if (dtExtraCurriculam.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtExtraCurriculam.Rows.Count; i++)
                                {
                                    chkExtraCurriculam.Items.FindByValue(dtExtraCurriculam.Rows[i]["Value"].ToString())
                                        .Selected = true;
                                }
                            }
                        }

                        using (DataTable dtOtherTalents = _commonDataLoad.GetDTEmpOtherTalentsByEmpId(mid))
                        {
                            if (dtOtherTalents.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtOtherTalents.Rows.Count; i++)
                                {
                                    chkOtherTalents.Items.FindByValue(dtOtherTalents.Rows[i]["Value"].ToString())
                                        .Selected = true;
                                }
                            }
                        }

                        using (DataTable dtAchievements = _commonDataLoad.GetDTEmpAchievementsByEmpId(mid))
                        {
                            if (dtAchievements.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtAchievements.Rows.Count; i++)
                                {
                                    chkAchievements.Items.FindByValue(dtAchievements.Rows[i]["Value"].ToString())
                                        .Selected = true;
                                }
                            }
                        }

                        using (DataTable dtHobby = _commonDataLoad.GetDTEmpHobbyByEmpId(mid))
                        {
                            if (dtHobby.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtHobby.Rows.Count; i++)
                                {
                                    chkHobby.Items.FindByValue(dtHobby.Rows[i]["Value"].ToString()).Selected = true;
                                }
                            }
                        }

                    }
                }
            }
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void LoadDropDownList()
    {
        using (DataTable dt = _commonDataLoad.GetMasterExtraCurriculam())
        {
            chkExtraCurriculam.DataSource = dt;
            chkExtraCurriculam.DataValueField = "Value";
            chkExtraCurriculam.DataTextField = "TextField";
            chkExtraCurriculam.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetMasterOtherTalents())
        {
            chkOtherTalents.DataSource = dt;
            chkOtherTalents.DataValueField = "Value";
            chkOtherTalents.DataTextField = "TextField";
            chkOtherTalents.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetMasterAchievements())
        {
            chkAchievements.DataSource = dt;
            chkAchievements.DataValueField = "Value";
            chkAchievements.DataTextField = "TextField";
            chkAchievements.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetMasterHobby())
        {
            chkHobby.DataSource = dt;
            chkHobby.DataValueField = "Value";
            chkHobby.DataTextField = "TextField";
            chkHobby.DataBind();
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
          #region fff

        try
        {
            string EmpMasterCode = string.Empty;
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblEmpGeneralInfo emp = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {
                    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                    EmpMasterCode = emp.EmpMasterCode;


                    #region 10. Others

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpExtraCurriculam SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkExtraCurriculam.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterExtraCurriculamId = int.Parse(item.Value);
                            var empExtraCurriculam = (from j in db.tblEmpExtraCurriculams
                                                      where j.EmpInfoId == emp.EmpInfoId
                                                      && j.MasterExtraCurriculamId == MasterExtraCurriculamId
                                                      select j).FirstOrDefault();
                            if (empExtraCurriculam == null)
                            {
                                empExtraCurriculam = new tblEmpExtraCurriculam();
                                empExtraCurriculam.EmpInfoId = emp.EmpInfoId;
                                empExtraCurriculam.MasterExtraCurriculamId = MasterExtraCurriculamId;
                                empExtraCurriculam.IsActive = true;
                                db.tblEmpExtraCurriculams.Add(empExtraCurriculam);
                            }
                            else
                            {
                                empExtraCurriculam.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End ExtraCurriculam


                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpOtherTalents SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkOtherTalents.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterOtherTalentsId = int.Parse(item.Value);
                            var empOtherTalent = (from j in db.tblEmpOtherTalents
                                                  where j.EmpInfoId == emp.EmpInfoId
                                                        && j.MasterOtherTalentsId == MasterOtherTalentsId
                                                  select j).FirstOrDefault();
                            if (empOtherTalent == null)
                            {
                                empOtherTalent = new tblEmpOtherTalent();
                                empOtherTalent.EmpInfoId = emp.EmpInfoId;
                                empOtherTalent.MasterOtherTalentsId = MasterOtherTalentsId;
                                empOtherTalent.IsActive = true;
                                db.tblEmpOtherTalents.Add(empOtherTalent);
                            }
                            else
                            {
                                empOtherTalent.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End OtherTalents

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpAchievements SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkAchievements.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterAchievementsId = int.Parse(item.Value);
                            var empAchievement = (from j in db.tblEmpAchievements
                                                  where j.EmpInfoId == emp.EmpInfoId
                                                        && j.MasterAchievementsId == MasterAchievementsId
                                                  select j).FirstOrDefault();
                            if (empAchievement == null)
                            {
                                empAchievement = new tblEmpAchievement();
                                empAchievement.EmpInfoId = emp.EmpInfoId;
                                empAchievement.MasterAchievementsId = MasterAchievementsId;
                                empAchievement.IsActive = true;
                                db.tblEmpAchievements.Add(empAchievement);
                            }
                            else
                            {
                                empAchievement.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Achievements

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpHobby SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkHobby.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterHobbyId = int.Parse(item.Value);
                            var empHobby = (from j in db.tblEmpHobbies
                                            where j.EmpInfoId == emp.EmpInfoId
                                                  && j.MasterHobbyId == MasterHobbyId
                                            select j).FirstOrDefault();
                            if (empHobby == null)
                            {
                                empHobby = new tblEmpHobby();
                                empHobby.EmpInfoId = emp.EmpInfoId;
                                empHobby.MasterHobbyId = MasterHobbyId;
                                empHobby.IsActive = true;
                                db.tblEmpHobbies.Add(empHobby);
                            }
                            else
                            {
                                empHobby.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Hobby
                    #endregion end 10. Others
                }

                else
                {

                    //emp = new tblEmpGeneralInfo();
                    //#region 10. Others
                    //foreach (ListItem item in chkExtraCurriculam.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpExtraCurriculam empExtraCurriculam = new tblEmpExtraCurriculam();
                    //        empExtraCurriculam.EmpInfoId = emp.EmpInfoId;
                    //        empExtraCurriculam.MasterExtraCurriculamId = int.Parse(item.Value);
                    //        empExtraCurriculam.IsActive = true;
                    //        db.tblEmpExtraCurriculams.Add(empExtraCurriculam);
                    //    }
                    //}////End ExtraCurriculam
                    //foreach (ListItem item in chkOtherTalents.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpOtherTalent empOtherTalent = new tblEmpOtherTalent();
                    //        empOtherTalent.EmpInfoId = emp.EmpInfoId;
                    //        empOtherTalent.MasterOtherTalentsId = int.Parse(item.Value);
                    //        empOtherTalent.IsActive = true;
                    //        db.tblEmpOtherTalents.Add(empOtherTalent);
                    //    }
                    //}////End OtherTalents
                    //foreach (ListItem item in chkAchievements.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpAchievement empAchievement = new tblEmpAchievement();
                    //        empAchievement.EmpInfoId = emp.EmpInfoId;
                    //        empAchievement.MasterAchievementsId = int.Parse(item.Value);
                    //        empAchievement.IsActive = true;
                    //        db.tblEmpAchievements.Add(empAchievement);
                    //    }
                    //}////End Achievements
                    //foreach (ListItem item in chkHobby.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpHobby empHobby = new tblEmpHobby();
                    //        empHobby.EmpInfoId = emp.EmpInfoId;
                    //        empHobby.MasterHobbyId = int.Parse(item.Value);
                    //        empHobby.IsActive = true;
                    //        db.tblEmpHobbies.Add(empHobby);
                    //    }
                    //}////End Hobby


                    //#endregion end 10. Others
                }
            } ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...!');window.location ='EmployeeInfoList.aspx';",
                    true);
            //empMasterCode.Text = emp.EmpMasterCode;
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
          #endregion
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void btn_Next_OnClick(object sender, EventArgs e)
    {
        #region fff

        try
        {
            string MasterId = string.Empty;
            
            string EmpMasterCode = string.Empty;
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblEmpGeneralInfo emp = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {
                    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                    EmpMasterCode = emp.EmpMasterCode;
                    MasterId = emp.EmpInfoId.ToString();

                    #region 10. Others

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpExtraCurriculam SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkExtraCurriculam.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterExtraCurriculamId = int.Parse(item.Value);
                            var empExtraCurriculam = (from j in db.tblEmpExtraCurriculams
                                                      where j.EmpInfoId == emp.EmpInfoId
                                                      && j.MasterExtraCurriculamId == MasterExtraCurriculamId
                                                      select j).FirstOrDefault();
                            if (empExtraCurriculam == null)
                            {
                                empExtraCurriculam = new tblEmpExtraCurriculam();
                                empExtraCurriculam.EmpInfoId = emp.EmpInfoId;
                                empExtraCurriculam.MasterExtraCurriculamId = MasterExtraCurriculamId;
                                empExtraCurriculam.IsActive = true;
                                db.tblEmpExtraCurriculams.Add(empExtraCurriculam);
                            }
                            else
                            {
                                empExtraCurriculam.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End ExtraCurriculam


                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpOtherTalents SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkOtherTalents.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterOtherTalentsId = int.Parse(item.Value);
                            var empOtherTalent = (from j in db.tblEmpOtherTalents
                                                  where j.EmpInfoId == emp.EmpInfoId
                                                        && j.MasterOtherTalentsId == MasterOtherTalentsId
                                                  select j).FirstOrDefault();
                            if (empOtherTalent == null)
                            {
                                empOtherTalent = new tblEmpOtherTalent();
                                empOtherTalent.EmpInfoId = emp.EmpInfoId;
                                empOtherTalent.MasterOtherTalentsId = MasterOtherTalentsId;
                                empOtherTalent.IsActive = true;
                                db.tblEmpOtherTalents.Add(empOtherTalent);
                            }
                            else
                            {
                                empOtherTalent.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End OtherTalents

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpAchievements SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkAchievements.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterAchievementsId = int.Parse(item.Value);
                            var empAchievement = (from j in db.tblEmpAchievements
                                                  where j.EmpInfoId == emp.EmpInfoId
                                                        && j.MasterAchievementsId == MasterAchievementsId
                                                  select j).FirstOrDefault();
                            if (empAchievement == null)
                            {
                                empAchievement = new tblEmpAchievement();
                                empAchievement.EmpInfoId = emp.EmpInfoId;
                                empAchievement.MasterAchievementsId = MasterAchievementsId;
                                empAchievement.IsActive = true;
                                db.tblEmpAchievements.Add(empAchievement);
                            }
                            else
                            {
                                empAchievement.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Achievements

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpHobby SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkHobby.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterHobbyId = int.Parse(item.Value);
                            var empHobby = (from j in db.tblEmpHobbies
                                            where j.EmpInfoId == emp.EmpInfoId
                                                  && j.MasterHobbyId == MasterHobbyId
                                            select j).FirstOrDefault();
                            if (empHobby == null)
                            {
                                empHobby = new tblEmpHobby();
                                empHobby.EmpInfoId = emp.EmpInfoId;
                                empHobby.MasterHobbyId = MasterHobbyId;
                                empHobby.IsActive = true;
                                db.tblEmpHobbies.Add(empHobby);
                            }
                            else
                            {
                                empHobby.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Hobby
                    #endregion end 10. Others
                }

                else
                {

                    //emp = new tblEmpGeneralInfo();
                    //#region 10. Others
                    //foreach (ListItem item in chkExtraCurriculam.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpExtraCurriculam empExtraCurriculam = new tblEmpExtraCurriculam();
                    //        empExtraCurriculam.EmpInfoId = emp.EmpInfoId;
                    //        empExtraCurriculam.MasterExtraCurriculamId = int.Parse(item.Value);
                    //        empExtraCurriculam.IsActive = true;
                    //        db.tblEmpExtraCurriculams.Add(empExtraCurriculam);
                    //    }
                    //}////End ExtraCurriculam
                    //foreach (ListItem item in chkOtherTalents.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpOtherTalent empOtherTalent = new tblEmpOtherTalent();
                    //        empOtherTalent.EmpInfoId = emp.EmpInfoId;
                    //        empOtherTalent.MasterOtherTalentsId = int.Parse(item.Value);
                    //        empOtherTalent.IsActive = true;
                    //        db.tblEmpOtherTalents.Add(empOtherTalent);
                    //    }
                    //}////End OtherTalents
                    //foreach (ListItem item in chkAchievements.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpAchievement empAchievement = new tblEmpAchievement();
                    //        empAchievement.EmpInfoId = emp.EmpInfoId;
                    //        empAchievement.MasterAchievementsId = int.Parse(item.Value);
                    //        empAchievement.IsActive = true;
                    //        db.tblEmpAchievements.Add(empAchievement);
                    //    }
                    //}////End Achievements
                    //foreach (ListItem item in chkHobby.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        tblEmpHobby empHobby = new tblEmpHobby();
                    //        empHobby.EmpInfoId = emp.EmpInfoId;
                    //        empHobby.MasterHobbyId = int.Parse(item.Value);
                    //        empHobby.IsActive = true;
                    //        db.tblEmpHobbies.Add(empHobby);
                    //    }
                    //}////End Hobby


                    //#endregion end 10. Others
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(),
             "alert",
             "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmpSalaryInfo.aspx?mid=" + MasterId + "';",
             true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //        "alert",
            //        "alert('Operation Successful...!');window.location ='EmployeeInfoList.aspx';",
            //        true);
            //empMasterCode.Text = emp.EmpMasterCode;
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
        #endregion
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void lbPrevious_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpNominee?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }

    protected void chkExtraAll_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkExtraCurriculam.Items.Count; i++)
        {
            if (chkExtraAll.Checked)
            {
                chkExtraCurriculam.Items[i].Selected = true;
            }
            else
            {
                chkExtraCurriculam.Items[i].Selected = false
                    ;
            }
        }

    }

    protected void chkOtherALL_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkOtherTalents.Items.Count; i++)
        {
            if (chkOtherALL.Checked)
            {
                chkOtherTalents.Items[i].Selected = true;
            }
            else
            {
                chkOtherTalents.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void chkAchievementsALL_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkAchievements.Items.Count; i++)
        {
            if (chkAchievementsALL.Checked)
            {
                chkAchievements.Items[i].Selected = true;
            }
            else
            {
                chkAchievements.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }
    protected void chkHobbyALL_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkHobby.Items.Count; i++)
        {
            if (chkHobbyALL.Checked)
            {
                chkHobby.Items[i].Selected = true;
            }
            else
            {
                chkHobby.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {

        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpSalaryInfo?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
       
    }
}