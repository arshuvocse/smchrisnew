using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class RecruitmentManagement_UI_JobRequisitionFormApprovalView : System.Web.UI.Page
{
    EmployeeRequsitionDAL aEmployeeRequsitionDal=new EmployeeRequsitionDAL();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
          //  UserPersmissionValidation();
            //LoadEmpJobRequisition();

            LoadEmpJobRequisition();
            LoadDroDownList();
        }
    }
    private void LoadDroDownList()
    {
        aEmployeeRequsitionDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
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
  
    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("JobRequisitionForm.aspx");
    }
    private void LoadEmpJobRequisition()
    {
        
        DataTable aDataTable=new DataTable();
        aDataTable = aEmployeeRequsitionDal.LoadInformationAppALl("");

        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

    }

    private string GenerateParameter()
    {
        string parameter = " ";


        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND   CI.CompanyId  =  '" + companyDropDownList.SelectedValue + "' ";
        }


        if (financialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND   FINY.FinancialYearId =  '" + financialYearDropDownList.SelectedValue + "'  ";
        }

        if (deptDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND Dpt.DepartmentId  =  '" + deptDropDownList.SelectedValue + "'  ";
        }





        return parameter;
    }



    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            Session["Status"] = "Edit";
            Session["AppLogId"] = loadGridView.DataKeys[rowindex][1].ToString();

            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            Session["AppPage"] = filepath;

            var datKey = loadGridView.DataKeys[rowindex]; 
            string Idd = datKey[0].ToString();
            if (datKey != null)
            {

                Session["Status"] = "Add";
                Session["ContractualEmpManageId"] = "";
                Session["ContractualEmpManageId"] = Idd;
                Session["ForEmpInfoId"] = loadGridView.DataKeys[rowindex][2].ToString();
            }

            Response.Redirect("~/RecruitmentManagement_UI/JobRequisitionFormApproval.aspx?mid=" + Idd);

        }

    }

    private void PopUp(Int32 jobReqId)
    {
        string url = "../Report_UI/JobRequisitionFormViewReportPreview.aspx?rptType=" + jobReqId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void reloadButton_OnClick(object sender, EventArgs e)
    {
        LoadEmpJobRequisition();
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            aEmployeeRequsitionDal.LoadFinancialYearForSearch(financialYearDropDownList,
                       companyDropDownList.SelectedValue);
            aEmployeeRequsitionDal.LoadDepartmentByWings(deptDropDownList, companyDropDownList.SelectedValue);
         
        }
        else
        {
            deptDropDownList.Items.Clear();
            financialYearDropDownList.Items.Clear();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
         
       
            
    }
}