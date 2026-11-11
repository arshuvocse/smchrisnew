using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Permission_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_TrainingTypeList : System.Web.UI.Page
{
    TrainingTypeDAL aTrainingTypeDal=new TrainingTypeDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserPersmissionValidation();
            LoadList();
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
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    public void LoadList()
    {
        DataTable dt = aTrainingTypeDal.GetDataForView();
        gv_trainingList.DataSource =dt;
        gv_trainingList.DataBind();
        this.gv_trainingList.ShowFooter = true;
        gv_trainingList.UseAccessibleHeader = true;
        gv_trainingList.HeaderRow.TableSection = TableRowSection.TableHeader;
        gv_trainingList.FooterRow.TableSection = TableRowSection.TableFooter;

    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");
        Session["TrTypeId"]=hdpk.Value;
        Session["Status"] = "Edit";
        Response.Redirect("TrainingTypeEntry.aspx");
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void lb_remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");
        Session["TrTypeId"] = hdpk.Value;
        Session["Status"] = "Delete";
        Response.Redirect("TrainingTypeEntry.aspx");
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");

        //TrainingTypeDAO aTypeDao=new TrainingTypeDAO();
        //aTypeDao.TrainingTypeID = Convert.ToInt32(hdpk.Value);
        //aTypeDao.DeleteBy = Session["UserId"].ToString();
        //aTypeDao.DeleteDate = DateTime.Now;
        //aTypeDao.IsDeleted = true;

        //bool result = aTrainingTypeDal.DeleteTrainingtype(aTypeDao);

        //if (result == true)
        //{
        //    AlertMessageBoxShow("Operation Successful...");
        //    LoadList();
        //}
        //else
        //{

        //    AlertMessageBoxShow("Operation Failed...");

        //}
    }

    protected void btnAddNewTraining_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("TrainingTypeEntry.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void lb_Delete_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");
        Session["TrTypeId"] = hdpk.Value;
        Session["Status"] = "View";
        Response.Redirect("TrainingTypeEntry.aspx");
        
    }
}