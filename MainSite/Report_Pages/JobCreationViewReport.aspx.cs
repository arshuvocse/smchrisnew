using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.RecruitmentManagement_BLL;
using DAL.Permission_DAL;
using DAL.RecruitmentManagement_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_JobCreationViewReport : System.Web.UI.Page
{
    JobCreationBll aJobCreationBll = new JobCreationBll();

    JobCreationDal aJobCreationDal = new JobCreationDal();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    PermissionDAL aPermissionDal = new PermissionDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
        //    UserPersmissionValidation();
          //  LoadJobCreationInfo();
            LoadDroDownList();
        }
    }

    private void LoadDroDownList()
    {
        aJobCreationDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
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

            string text = Path.GetExtension(Request.Path);
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }

            //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
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

                    AddNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

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
    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("JobCreation.aspx");
    }



    private void LoadJobCreationInfo()
    {
        DataTable jobCreationInfos = new DataTable();

        jobCreationInfos = aJobCreationBll.LoadJobCreationInfos("  WHERE ( JC.IsDelete IS NULL OR JC.IsDelete = 0 ) and CI.CompanyId IN (" + CompanyId() + ") " + GenerateParameter() + "");
        loadGridView.DataSource = jobCreationInfos;
        loadGridView.DataBind();
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);

            Session["Status"] = "Edit";

            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string jobReqId = datKey[0].ToString();
                Session["JobID"] = "";
                Session["JobID"] = jobReqId;

            }

            Response.Redirect("JobCreation.aspx");

        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);

            Session["Status"] = "View";

            var datKey = loadGridView.DataKeys[rowindex];

            if (datKey != null)
            {
                string jobReqId = datKey[0].ToString();
                Session["JobID"] = "";
                Session["JobID"] = jobReqId;

            }

            Response.Redirect("JobCreation.aspx");

        }

        if (e.CommandName == "DeleteData")
        {
            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string masterId = loadGridView.DataKeys[rowindex][0].ToString();

            //bool masterStatus = aJobCreationDal.DeleteJobCreationById(masterId);
            //bool detailStatus = aJobCreationDal.DeleteJobCreationDetailById(masterId);

            //if (masterStatus && detailStatus)
            //{
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
            //    LoadJobCreationInfo();
            //}

            int rowindex = Convert.ToInt32(e.CommandArgument);

            Session["Status"] = "Delete";

            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string jobReqId = datKey[0].ToString();
                Session["JobID"] = "";
                Session["JobID"] = jobReqId;
            }

            Response.Redirect("JobCreation.aspx");
        }
        if (e.CommandName == "Reportd")
        {
          

        }


        if (e.CommandName == "Preview")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = loadGridView.DataKeys[0];

            if (datKey != null)
            {

                Response.Redirect("MemoPrintJobCirculation.aspx?mid=" + e.CommandArgument.ToString());
                Session["Status"] = "View";

            }



        }

    }


    private string GenerateParameter()
    {
        string parameter = " ";


        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND   CI.CompanyId =  '" + companyDropDownList.SelectedValue + "' ";
        }


        if (financialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  FY.FinancialYearId =  '" + financialYearDropDownList.SelectedValue + "'  ";
        }

        if (deptDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND dpt.DepartmentId =  '" + deptDropDownList.SelectedValue + "'  ";
        }





        return parameter;
    }


    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            aJobCreationDal.LoadFinancialYearForSearch(financialYearDropDownList,
                       companyDropDownList.SelectedValue);
            aJobCreationDal.LoadDepartmentByWings(deptDropDownList, companyDropDownList.SelectedValue);
            
        }
        else
        {
            deptDropDownList.Items.Clear();
            financialYearDropDownList.Items.Clear();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            LoadJobCreationInfo();
        }
        else
        {
            companyDropDownList.Focus();
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
        }
    }
}