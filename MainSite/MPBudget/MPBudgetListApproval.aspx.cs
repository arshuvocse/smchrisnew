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
using DAL.InternalCls;
using DAL.MPBudget;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class MPBudget_MPBudgetList : System.Web.UI.Page
{
    private static string _userId;
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ClsApprovalAction approvalAction = new ClsApprovalAction();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            GetCompany();
           // UserPersmissionValidation();

            LoadInitialDDL();
            //List<MPBudgetViewModel> lModels = _mpBudgetCommonDal.GetMPBudgetList();
            DataLoad();

        }
    }
    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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


                   
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
             

               

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                 
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
    public void DataLoad()
    {
        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');
        string exten = Path.GetExtension(Request.Path);
        if (exten == string.Empty)
        {
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
        }
        else
        {
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        }

        string userName = Session["UserId"].ToString();
        DataLoadByCondition(filepath, userName);
        //approvalAction.LoadActionControlByUser(jobreqRadioButtonList, filepath, userName);

    }
    private void DataLoadByCondition(string pageName, string user)
    {
        DataTable aDataTable = new DataTable();
        string ActionStatus = approvalAction.LoadForApprovalByUserCondition(pageName, user);
        aDataTable = _commonDataLoad.GetManpowerBudgetInfoApproval();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    private void LoadManpowerBudgetInfo()
    {

        if (ddlCompany.SelectedValue != "")
        {
            if (ddlDepartment.SelectedValue != "")
            {
                if (ddlFinYear.SelectedValue != "")
                {
                    DataTable aTable = _commonDataLoad.GetManpowerBudgetInfoApproval();

                    if (aTable.Rows.Count > 0)
                    {
                        loadGridView.DataSource = aTable;
                        loadGridView.DataBind();
                    }
                }
                else
                {
                    ddlFinYear.Focus();
                }
            }
            else
            {
                ddlDepartment.Focus();
            }
        }
        else
        {
            ddlCompany.Focus();
        }
       
        
    }

   

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }
    }

    //Response.Redirect("MPBudgetEntry.aspx");
    protected void btn_New_OnClick(object sender, EventArgs e)
    {
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
            using (DataTable dt = _commonDataLoad.GetDDLFinYearByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlFinYear.DataSource = dt;
                ddlFinYear.DataValueField = "Value";
                ddlFinYear.DataTextField = "TextField";
                ddlFinYear.DataBind();
            }

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
                string jobReqId = datKey[0].ToString();
                string filepath = Path.GetDirectoryName(Request.Path);
                filepath = filepath.TrimStart('\\');
                string exten = Path.GetExtension(Request.Path);
                if (exten == string.Empty)
                {
                    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                }
                else
                {
                    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                }
                Session["MPBudgetMasterId"] = "";
                Session["MPBudgetMasterId"] = jobReqId;
                Session["AppLogId"] = datKey[1].ToString();
                Session["AppPage"] = filepath;
                

            }
            Response.Redirect("MPBudgetApproveView.aspx?mid=" + loadGridView.DataKeys[rowindex][0].ToString());

           

            

        }

    }

    protected void btnFilterSearch_OnClick(object sender, EventArgs e)
    {
        LoadManpowerBudgetInfo();
    }
}