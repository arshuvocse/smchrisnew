using DAL.TrainingDAL;
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
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_TrainingAttendance : System.Web.UI.Page
{
    private TrainingDAL _trainingDal = new TrainingDAL();
    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();

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
        }
        catch (Exception)
        {

            //throw;
        }
       
    }

    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            string text = Path.GetExtension(Request.Path);
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }

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



                    gv_trainingList.Columns[gv_trainingList.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["Add"].ToString());
                 
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("/Default.aspx");
        }
    }
    public string Parameter()
    {
        string param = "";
        if (ddlCompany.SelectedIndex > 0)
        {
            param = param + " AND C.CompanyId='" + ddlCompany.SelectedValue + "' ";
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
    }

    //public void UserPersmissionValidation()
    //{
    //    try
    //    {
    //        string filepath = Path.GetDirectoryName(Request.Path);
    //        filepath = filepath.TrimStart('\\');
    //        filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
    //        DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
    //        if (dtuserpermission.Rows.Count > 0)
    //        {
    //            if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
    //                dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
    //            {


    //                ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
    //                ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
    //                ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
    //                ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

    //                btnAddNewTraining.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

    //                gv_trainingList.Columns[gv_trainingList.Columns.Count - 1].Visible =
    //                    Convert.ToBoolean(ViewState["View"].ToString());
    //                gv_trainingList.Columns[gv_trainingList.Columns.Count - 2].Visible =
    //                    Convert.ToBoolean(ViewState["Delete"].ToString());
    //                gv_trainingList.Columns[gv_trainingList.Columns.Count - 3].Visible =
    //                    Convert.ToBoolean(ViewState["Edit"].ToString());
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        aShowMessage.ShowMessageBox(ex.ToString(), this);
    //    }
    //}

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

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //aCommonDataLoadDal.FinYearByCompDropDown(ddlFinancialYear, ddlCompany.SelectedValue);
    }

    protected void LoadList()
    {
        if (ddlCompany.SelectedIndex > 0)
        {


       
        DataTable dt = _trainingDal.GetTrainingList(Parameter());
        if (dt.Rows.Count > 0)
        {
            gv_trainingList.DataSource = dt;
            gv_trainingList.DataBind();
            gv_trainingList.UseAccessibleHeader = true;
            gv_trainingList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_trainingList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        else
        {
            gv_trainingList.DataSource = null;
            gv_trainingList.DataBind();
        }
    }

else
        {

            gv_trainingList.DataSource = null;
            gv_trainingList.DataBind();
            aShowMessage.ShowMessageBox("Please Select Comapny", this);
        }
      
    }
    protected void lb_Attendance_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingAttendaneSetup.aspx?mid=" + hdpk.Value);
    }

    protected void btnFilterSearch_OnClick(object sender, EventArgs e)
    {
        LoadList();
    }
}