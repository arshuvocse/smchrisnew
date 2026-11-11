using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MPBudget;
using DAL.Permission_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Survey_ProbationList : System.Web.UI.Page
{
    private static string _userId;
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    ProbationperiodDAL aDal=new ProbationperiodDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            //GetCompany();
        
            //LoadInitialDDL();
         
            LoadManpowerBudgetInfo();
           
        }
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

    private void LoadManpowerBudgetInfo()
    {
        DataTable dtdata = aDal.LoadEmployeeDataProbation(Parameter());
        loadGridView.DataSource = dtdata;
        loadGridView.DataBind();
        //if (ddlCompany.SelectedValue != "")
        //{
        //    if (ddlDepartment.SelectedValue != "")
        //    {
        //        if (ddlFinYear.SelectedValue != "")
        //        {
        //            DataTable aTable = _commonDataLoad.GetManpowerBudgetListInfo(ddlCompany.SelectedValue, ddlDepartment.SelectedValue, ddlFinYear.SelectedValue, " And bm.CompanyId IN (" + CompanyId() + ")");

        //            //if (aTable.Rows.Count > 0)
        //            {
        //                loadGridView.DataSource = aTable;
        //                loadGridView.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            ddlFinYear.Focus();
        //        }
        //    }
        //    else
        //    {
        //        ddlDepartment.Focus();
        //    }
        //}
        //else
        //{
        //    ddlCompany.Focus();
        //}         
    }

    public string Parameter()
    {
        string param = "";
        if (ddlCompany.SelectedIndex>0)
        {
            param = param + " AND EGI.CompanyId='"+ddlCompany.SelectedValue+"' ";
        }
        if (ddlDepartment.SelectedIndex>0)
        {
            param = param + " AND EGI.DepartmentId='" + ddlDepartment.SelectedValue + "' ";
            
        }
        if (startDate.Text !=string.Empty && endDate.Text!=string.Empty)
        {
            param = param + " AND (EGI.ProbationEndDate BETWEEN '" + startDate.Text + "' AND '" + endDate.Text + "')";
        }
        return param;
    }
    private void LoadInitialDDL()
    {
        _commonDataLoad.GetCompanyListIntoDropdown(ddlCompany);
    }

    //Response.Redirect("MPBudgetEntry.aspx");
    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("MPBudgetEntry.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static List<MPBudgetViewModel> LoadMPBudgetTable()
    {
        MPBudgetCommonDAL _mpBudgetCommonDal = new MPBudgetCommonDAL();
       return _mpBudgetCommonDal.GetMPBudgetList();
    }

    [WebMethod(EnableSession = true)]
    public static List<MPBudgetViewModel> FilterMPBudgetTable(string company,string dept, string finyear)
    {
        MPBudgetCommonDAL _mpBudgetCommonDal = new MPBudgetCommonDAL();
      
            return _mpBudgetCommonDal.FilterMPBudgetTable(company, dept, finyear);
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteMPBudgetRowByMId(string MPBudgetId)
    {
        string status = "ok";
        int mid = int.Parse(MPBudgetId);
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                var mpb = (from u in db.tblMPBudgetMasters where u.MPBudgetMasterId ==  mid select u).FirstOrDefault();
                mpb.IsActive = false;
                mpb.UpdateDate = DateTime.Now;
                mpb.UpdateBy = _userId;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlCompany.SelectedIndex > 0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLDepartmentByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            //using (DataTable dt = _commonDataLoad.GetDDLFinYearByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            //{
            //    ddlFinYear.DataSource = dt;
            //    ddlFinYear.DataValueField = "Value";
            //    ddlFinYear.DataTextField = "TextField";
            //    ddlFinYear.DataBind();
            //}

        }
    }


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                //string filepath = Path.GetDirectoryName(Request.Path);
                //filepath = filepath.TrimStart('\\');
                //string exten = Path.GetExtension(Request.Path);
                //if (exten == string.Empty)
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                //}
                //else
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                //}
                //Session["ApprovalPage"] = filepath;
                string id = datKey["MPBudgetMasterId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Edit";

                Response.Redirect("MPBudgetEntry.aspx?mid=" + loadGridView.DataKeys[rowindex][0].ToString());
                //+ "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());    

            }

            

        }
        if (e.CommandName == "Evaluate")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                Session["MasterId"] = "";
                //string filepath = Path.GetDirectoryName(Request.Path);
                //filepath = filepath.TrimStart('\\');
                //string exten = Path.GetExtension(Request.Path);
                //if (exten == string.Empty)
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                //}
                //else
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                //}
                //Session["ApprovalPage"] = filepath;
                Session["AppLogId"] = loadGridView.DataKeys[rowindex][2].ToString();

                string filepath = Path.GetDirectoryName(Request.Path);
                filepath = filepath.TrimStart('\\');
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                Session["PMAppPage"] = filepath;
                Session["AppPage"] = filepath;
                Session["MasterId"] = loadGridView.DataKeys[rowindex][1].ToString();
                Session["JDAppLogId"] = loadGridView.DataKeys[rowindex][2].ToString();
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                //Session["Status"] = "Edit";
                Response.Redirect("ProbationEvaluationFormApproval.aspx?id=" + loadGridView.DataKeys[rowindex][0].ToString());
                //+ "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());    

            }

            

        }

          if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];

            if (datKey != null)
            {


                string id = datKey["MPBudgetMasterId"].ToString();
               // string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "View";
                Response.Redirect("MPBudgetEntry.aspx?mid=" + loadGridView.DataKeys[rowindex][0].ToString());// + "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());
            }
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];

            if (datKey != null)
            {


                string id = datKey["MPBudgetMasterId"].ToString();
               // string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Delete";
                Response.Redirect("MPBudgetEntry.aspx?mid=" + loadGridView.DataKeys[rowindex][0].ToString());// + "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());    
            }
        }

    }

    protected void btnFilterSearch_OnClick(object sender, EventArgs e)
    {
        LoadManpowerBudgetInfo();
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}