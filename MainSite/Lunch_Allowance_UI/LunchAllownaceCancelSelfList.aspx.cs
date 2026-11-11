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

public partial class Lunch_Allowance_UI_LunchAllownaceCancelSelfList : System.Web.UI.Page
{
    AchievementsEntryDAL aVaencyEntryDaL = new AchievementsEntryDAL();
    VivaSetupInfoEntryDAL aDaL = new VivaSetupInfoEntryDAL();

    private Lunch_Allowance_Dal_Common commmDAl = new Lunch_Allowance_Dal_Common();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();


    private LunchAllowanceCancelDAL allowanceCancelDal = new LunchAllowanceCancelDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // GetCompany();
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

                    //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                    //    Convert.ToBoolean(ViewState["View"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                    //    Convert.ToBoolean(ViewState["Delete"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                    //    Convert.ToBoolean(ViewState["Edit"].ToString());
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
            string EmpInfoId = Session["EmpInfoId"].ToString();
            DataTable dataTable = allowanceCancelDal._LunchCancelList(EmpInfoId);

            if (dataTable.Rows.Count > 0)
            {
                loadGridView.DataSource = dataTable;
                loadGridView.DataBind();
                this.loadGridView.ShowFooter = true;
                loadGridView.UseAccessibleHeader = true;
                loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;

                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    LinkButton aButton = (LinkButton)loadGridView.Rows[i].FindControl("btnEdit");

                        var status = loadGridView.DataKeys[i][4].ToString();

                        if (status == "1")
                        {
                            aButton.Visible = true;
                        }
                        else
                        {
                            aButton.Visible = false;
                            
                        }

                        
                        
                }
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
        Session["CancelStatus"] = "Add";
        Response.Redirect("~/Lunch_Allowance_UI/LunchAllownaceCancelSelfEntry.aspx");
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


    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        Session["CancelStatus"] = "";
        Session["CancelStatus"] = "View";
        string value = loadGridView.DataKeys[rowID][1].ToString();
        string Date = loadGridView.DataKeys[rowID][2].ToString();
        Session["CancelDate"] = "";
        Session["CancelDate"] = Date.Trim();


        Response.Redirect("LunchAllownaceCancelSelfEntry.aspx?MID=" + value.Trim());
    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        string value = loadGridView.DataKeys[rowID][1].ToString();
        string Date = loadGridView.DataKeys[rowID][2].ToString();

        Session["CancelDate"] = "";
        Session["CancelDate"] = Date.Trim();

        Session["CancelStatus"] = "";
        Session["CancelStatus"] = "Edit";

        DataTable dt = allowanceCancelDal.GetLunchCancelInfoByEmpId(int.Parse(value), Date);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["ActionStatus"].ToString().Trim() == "Draft")
            {
                Response.Redirect("LunchAllownaceCancelSelfEntry.aspx?MID=" + value.Trim());
            }

            else if (dt.Rows[0]["ActionStatus"].ToString().Trim() == "Returned")
            {
                Response.Redirect("LunchAllownaceCancelSelfEntry.aspx?MID=" + value.Trim());
            }
            else
            {
                showMessageBox("You Can not edit this.....");
            }
        }

    }

}