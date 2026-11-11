using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_JdList : System.Web.UI.Page
{
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        GetCompany();
        //UserPersmissionValidation();

        DataTable dt = _jdDal.GetJdListApp(" AND A.CompanyId IN (" + CompanyId() + ")");
        gv_JdBoard.DataSource = dt;
        gv_JdBoard.DataBind();
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

                    //detailsViewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 3].Visible =
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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("JobDescription.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";
        Session["AppLogId"] = gv_JdBoard.DataKeys[rowID][0].ToString();

        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');
        filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        Session["AppPage"] = filepath;

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");
        HiddenField empIdHF = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("empIdHF");

        Response.Redirect("JobDescriptionApprovalView.aspx?masterId=" + mastrId.Value + "&EmpInfoId=" + empIdHF.Value);
    }

    protected void btn_Remove_OnClick(object sender, EventArgs e)
    {


        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        Session["Status"] = "Delete";

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");

        Response.Redirect("JobDescription.aspx?masterId=" + mastrId.Value + "");


        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");

        //bool result = _jdDal.DeleteJd(Convert.ToInt32(mastrId.Value), Session["LoginName"].ToString());

        //if (result == true)
        //{
        //    AlertMessageBoxShow("Operation Successful...");

        //    DataTable dt = _jdDal.GetJdList();
        //    gv_JdBoard.DataSource = dt;
        //    gv_JdBoard.DataBind();
           
        //}
        //else
        //{
        //    AlertMessageBoxShow("Operation Failed...");

        //}
    }


    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");

        Response.Redirect("JobDescription.aspx?masterId=" + mastrId.Value + "");
    }
}