using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_ExpenseReimbursementFormList : System.Web.UI.Page
{

    ReimbursmentFormDal aFormDal = new ReimbursmentFormDal();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // UserPersmissionValidation();
            loadReimbursmentFromList();
        }
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

                   btn_New.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

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

    protected void loadReimbursmentFromList()
    {
        DataTable dt = aFormDal.Get_ReimbusrmentFormlist();
        loadGridView.DataSource = dt;
        loadGridView.DataBind();

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            Label update = (Label)loadGridView.Rows[i].FindControl("PendingTo");
            string acStatus = loadGridView.DataKeys[i]["ActionStatus"].ToString();
            LinkButton aButton = (LinkButton)loadGridView.Rows[i].FindControl("btnUpdate");

            if (acStatus != "Draft")
            {
                if (update.Text == "")
                {
                    aButton.Visible = true;
                }
                else
                {
                    update.Visible = true;
                }
            }
            else
            {
                update.Visible = true;
            }
        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {   
        if (e.CommandName == "ViewReport")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                PopUp(Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString()));
            }
        }
    }

    private void PopUp(Int32 MasterId)
    {
        string url = "../Report_UI/ReimbursmentFormReportViewer.aspx?rptType=" + MasterId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
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

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        Session["dsdadf"] = "";
        Session["dsdadf"] = "View";
        HiddenField hfMasterId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfReimbursFromMasterId");
        Response.Redirect("ExpenseFormViewDetails.aspx?MID=" + hfMasterId.Value.Trim());
    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfMasterId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfReimbursFromMasterId");
        DataTable dt = aFormDal.Get_ActionStatusById(Convert.ToInt32(hfMasterId.Value.Trim()));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["ActionStatus"].ToString().Trim() == "Draft" || dt.Rows[0]["ActionStatus"].ToString().Trim() == "Review")
            {
                Response.Redirect("ExpenseReimbursementFormSelfEntry.aspx?MID=" + hfMasterId.Value.Trim());
            }
            else
            {
                AlertMessageBoxShow("You Can not edit this.....");
            }

          //  Response.Redirect("ExpenseReimbursementFormSelfEntry.aspx?MID=" + hfMasterId.Value.Trim());
        }
    }
    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        string ReimbursFromMaster = loadGridView.DataKeys[rowID]["ReimbursFromMasterId"].ToString();
        DataTable dt = aFormDal.Get_ReportingBossCheck(Convert.ToInt32(ReimbursFromMaster));
        if (dt.Rows.Count >0)
        {

            ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO();

            appLogDao.ActionStatus = "Drafted";
            appLogDao.ApproveDate = DateTime.Now;
            appLogDao.ApproveBy = Session["UserId"].ToString();
            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
            appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
            appLogDao.ReimbursFromMasterId = Convert.ToInt32(ReimbursFromMaster);
            // appLogDao.Comments = txt_Comments.Text;
            appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());

            int idd = aFormDal.SaveEmpAppLog(appLogDao);

            ReimbursementSelfAppLogDAO appLogDao1 = new ReimbursementSelfAppLogDAO()
            {
                ActionStatus = "Verified",
                ApproveDate = DateTime.Now,
                ApproveBy = Session["UserId"].ToString(),
                PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                ForEmpInfoId = Convert.ToInt32(dt.Rows[0]["ReportingEmpId"].ToString()),
                ReimbursFromMasterId = Convert.ToInt32(ReimbursFromMaster),
                // Comments = txt_Comments.Text,
                CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),
            };
            int id = aFormDal.SaveEmpAppLog(appLogDao1);

            if (id > 0)
            {
                loadReimbursmentFromList();
                AlertMessageBoxShow("Update Successfully");
            }
        }
        else
        {
            AlertMessageBoxShow("Supervisor not found");
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

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
       
        
        Response.Redirect("ExpenseReimbursementFormSelfEntry.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    // EmployeeList 

    protected void btnDel_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfMasterId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfReimbursFromMasterId");
        DataTable dt = aFormDal.Get_ActionStatusById(Convert.ToInt32(hfMasterId.Value.Trim()));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["ActionStatus"].ToString().Trim() == "Draft")
            {

                bool rei = aFormDal.delssssss(hfMasterId.Value);

                if (rei)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfully Done...');window.location ='ExpenseReimbursementFormList.aspx';",
                        true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Already Exist...');",
                  true);
                
                }

                // Response.Redirect("ExpenseReimbursementFormEntry.aspx?MID=" + hfMasterId.Value.Trim());
            }
            else
            {

                AlertMessageBoxShow("You can not delete this.....");

            }
        }
    }

    protected void btnCmt_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField MasterId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfReimbursFromMasterId");
        DataTable DT = aFormDal.Get_ApplicationComments(MasterId.Value);
        if (DT.Rows.Count > 0)
        {
            gv_EmpListSearch.DataSource = DT;
            gv_EmpListSearch.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#CommentsModal').modal('show')", true);

            // data-toggle="modal" data-target="#exampleModal23"
        }
        else
        {
            aShowMessage.ShowMessageBox("There is no feedback for this Application", this);
        }
    }
}