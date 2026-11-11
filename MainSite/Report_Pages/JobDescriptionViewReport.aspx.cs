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

public partial class Report_Pages_JobDescriptionViewReport : System.Web.UI.Page
{
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        GetCompany();
      //  UserPersmissionValidation();

        DataTable dt = _jdDal.GetJdList(" AND A.CompanyId IN (" + CompanyId() + ")");
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

                    detailsViewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

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
        if (gv_JdBoard.Rows.Count>0)
        {
            aShowMessage.ShowMessageBox("Job Description Already Exist , Please Go To Edit mode ",this);
        }
        else
        {
            Session["Status"] = "Add";
            Response.Redirect("JobDescription.aspx");    
        }
        
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
 HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");
 PopUp(Convert.ToInt32(mastrId.Value));

        
    }

    private void PopUp(Int32 mastrId)
    {
        string url = "../Report_UI/JobDescriptionReportViwer.aspx?rptType=" + mastrId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


    protected void btn_Remove_OnClick(object sender, EventArgs e)
    {


        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        DataTable dt = _jdDal.GetJdList("");
        if (dt.Rows.Count > 0)
        {
            //if (dt.Rows[0]["ActionStatus"].ToString() != "Drafted")
            {
                aShowMessage.ShowMessageBox(
                    "Job Description is Approved !!", this);

            }
            //else
            {
                
            }
        }
        else
        {
            Session["Status"] = "Delete";

            HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");

            Response.Redirect("JobDescription.aspx?masterId=" + mastrId.Value + "");
        }

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