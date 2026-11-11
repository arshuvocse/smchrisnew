using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_TrainingRecords : System.Web.UI.Page
{
    public TrainingRecordDAL _recordDal = new TrainingRecordDAL();

    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();
            //LoadList();
        }

        try
        {

            gv_trainingList.UseAccessibleHeader = true;
            gv_trainingList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_trainingList.FooterRow.TableSection = TableRowSection.TableFooter;
            gv_trainingList.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }
    }
    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
        LoadList();
    }
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aCommonDataLoadDal.FinYearByCompDropDown(ddlFinancialYear, ddlCompany.SelectedValue);
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public string Parameter()
    {
        string param = "";
        if (ddlCompany.SelectedIndex > 0)
        {
            param = param + " AND A.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }
        if (ddlFinancialYear.SelectedIndex > 0)
        {
            param = param + " AND A.FinancialYearId='" + ddlFinancialYear.SelectedValue + "' ";
        }
        return param;
    }
    public void GetCompany()
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
        aCommonDataLoadDal.CompanyDropDown(ddlCompany, " WHERE CompanyId IN (" + CompanyId() + ") ");
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);

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

                    btnAddNewTraining.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    gv_trainingList.Columns[gv_trainingList.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_trainingList.Columns[gv_trainingList.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_trainingList.Columns[gv_trainingList.Columns.Count - 3].Visible =
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

    protected void btnAddNewTraining_OnClick(object sender, EventArgs e)
    {
       Session["Status"] = "Add";
       Response.Redirect("TrainingRecord.aspx");
    }

    protected void lb_Edit_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");
int mainID = Convert.ToInt32(hdpk.Value);

        if (!CheckAchievementsAllocateOrNot(mainID))
        {
            Session["Status"] = "Edit";



            Response.Redirect("TrainingRecord.aspx?mid=" + hdpk.Value);
        }
        else
        {
            aShowMessage.ShowMessageBox("Can not be Updated", this);
        }
    }



    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblTrainingRecordMasters
                      where t.TrainingRecordMasterId == MainID && t.ActionStatus != "Drafted"
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }


    protected void lb_remove_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

          HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");
        int mainID = Convert.ToInt32(hdpk.Value);

        if (!CheckAchievementsAllocateOrNot(mainID))
        {
            Session["Status"] = "Delete";



            Response.Redirect("TrainingRecord.aspx?mid=" + hdpk.Value);
        }
        else
        {
            aShowMessage.ShowMessageBox("Can not be Updated", this);
        }

       
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    private void LoadList()
    {
        if (ddlCompany.SelectedIndex>0)
        {
            DataTable dt = _recordDal.GetTrainingRecords(Parameter());
            if (dt.Rows.Count > 0)
            {
                gv_trainingList.DataSource = dt;
                gv_trainingList.DataBind();
                gv_trainingList.UseAccessibleHeader = true;
                gv_trainingList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_trainingList.FooterRow.TableSection = TableRowSection.TableFooter;
                gv_trainingList.UseAccessibleHeader = true;

            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!", this);
                gv_trainingList.DataSource = null;
                gv_trainingList.DataBind();
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Company!!", this);
            gv_trainingList.DataSource = null;
            gv_trainingList.DataBind();
        }
       
      
    }

    protected void lb_View_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingRecord.aspx?mid=" + hdpk.Value);
    }

  

    protected void lb_SendMail_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "SendMail";

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingRecordSendMAil.aspx?mid=" + hdpk.Value);
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}