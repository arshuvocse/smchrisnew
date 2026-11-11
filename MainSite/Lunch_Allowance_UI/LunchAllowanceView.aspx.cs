using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Lunch_Allowance_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Lunch_Allowance_UI_LunchAllowanceView : System.Web.UI.Page
{
    AchievementsEntryDAL aVaencyEntryDaL = new AchievementsEntryDAL();
    VivaSetupInfoEntryDAL aDaL = new VivaSetupInfoEntryDAL();

    private   Lunch_Allowance_Dal_Common commmDAl = new        Lunch_Allowance_Dal_Common ();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
          //   UserPersmissionValidation();
            LoadRegionInformation();

            using (DataTable dt = _commonDataLoad.GetCompanyDDL())
            {
                company.DataSource = dt;
                company.DataValueField = "Value";
                company.DataTextField = "TextField";
                company.DataBind();
                company.SelectedIndex = 1;
            }
        }
        try
        {
            this.loadGridView.ShowFooter = true;
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {
            
            //throw;
        }
        


        
        // LoadRegionInformation();
    }
    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }
    public void GetCompany()
    {
        try
        {
            DataTable dtcomp = aPermissionDal.GetCompany();
            lchk_Company.DataValueField = "CompanyId";
            lchk_Company.DataTextField = "ShortName";
            lchk_Company.DataSource = dtcomp;
            lchk_Company.DataBind();

            DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                for (int j = 0; j < lchk_Company.Items.Count; j++)
                {
                    if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                    {
                        lchk_Company.Items[j].Selected = true;


                    }
                }
            }
        }
        catch (Exception)
        {
            
           Response.Redirect("/Default.aspx");
        }
    }

    private void LoadRegionInformation()
    {
        try
        {
            DataTable dataTable = commmDAl._LunchInfoView(" and  com.CompanyId IN (" + CompanyId() + ")");

            if (dataTable.Rows.Count > 0)
            {
                loadGridView.DataSource = dataTable;
                loadGridView.DataBind();
                this.loadGridView.ShowFooter = true;
                loadGridView.UseAccessibleHeader = true;
                loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        catch (Exception)
        {
            
            //throw;
        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                string areaId = dataKey[0].ToString();
                Session["Status"] = "Edit";
                Session["VacancyCirculationId"] = "";
                Session["VacancyCirculationId"] = areaId;
            }

            Response.Redirect("FoodRateEntry.aspx");
        }

         if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string VacancyCirculationId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["VacancyCirculationId"] = "";
            Session["VacancyCirculationId"] = VacancyCirculationId;
            Session["Status"] = "View";
            Response.Redirect("FoodRateEntry.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string VacancyCirculationId = loadGridView.DataKeys[rowindex][1].ToString();
            int Mid = Convert.ToInt32(loadGridView.DataKeys[rowindex][1].ToString());

            tblLunchAllowDetail Bat = null;
            try
            {
                using (var db = new HRIS_SMC_DBEntities())
                {
                    if (Mid > 0)
                    {

                        Bat = (from j in db.tblLunchAllowDetails where j.LunchAllowDetailsID == Mid select j).FirstOrDefault();









                        Bat.Status = "Inactive";
                        
                        db.SaveChanges();

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successful...! ');window.location ='LunchAllowanceView.aspx';",
                            true);
                    }
                }
            }
            catch (Exception)
            {


            }



           
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var areaId = dataKey[0].ToString();

        //        if (!CheckAreaAllocateOrNot(areaId))
        //        {
        //            if (aInformationDal.DeleteAreaInfoById(areaId))
        //            {
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //                LoadRegionInformation();
        //            }
        //        }
        //        else
        //        {
                   
        //         //   showMessageBox("Cann't delete because it contains a Region."); 
        //            LoadRegionInformation();
        //            aShowMessage.ShowMessageBox("Can not be deleted because this is used in Job Location.", this);
        //          //aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //            //
        //        }
        //    }
        //}
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("LunchAllowanceEntry.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
       
    }

  
    protected void CategoryDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}