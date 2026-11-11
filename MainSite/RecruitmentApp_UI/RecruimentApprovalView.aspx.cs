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
using DAL.RecruitmentApp_DAL;
using DAL.RecruitmentManagement_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class RecruitmentManagement_UI_JobCreationView : System.Web.UI.Page
{
    JobCreationBll aJobCreationBll = new JobCreationBll();

    JobCreationDal aJobCreationDal = new JobCreationDal();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    PermissionDAL aPermissionDal = new PermissionDAL();
    RecruitmentAppDAL appDal=new RecruitmentAppDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            //UserPersmissionValidation();
            LoadJobCreationInfo();
            LoadDroDownList();
        }
    }

    private void LoadDroDownList()
    {
        aJobCreationDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
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
            //string filepath = Path.GetDirectoryName(Request.Path);
            //filepath = filepath.TrimStart('\\');

            //string text = Path.GetExtension(Request.Path);
            //if (text == string.Empty)
            //{
            //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            //}
            //else
            //{
            //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            //}

            ////filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            //DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            //if (dtuserpermission.Rows.Count > 0)
            //{
            //    if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
            //        dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
            //    {
            //        ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
            //        ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
            //        ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
            //        ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

            //        AddNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

            //        loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            Convert.ToBoolean(ViewState["View"].ToString());
            //        loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //            Convert.ToBoolean(ViewState["Delete"].ToString());
            //        loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //            Convert.ToBoolean(ViewState["Edit"].ToString());
            //    }
            //}
            //else
            //{
            //    Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            //}
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
    public void GetDepet()
    {
        try
        {
            DataTable dtcomp = aPermissionDal.LoadDepartmentByWings(companyDropDownList.SelectedValue);
            lchk_Dpt.DataValueField = "DepartmentId";
            lchk_Dpt.DataTextField = "DepartmentName";
            lchk_Dpt.DataSource = dtcomp;
            lchk_Dpt.DataBind();

            // DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            //for (int i = 0; i < userdata.Rows.Count; i++)
            //{
            for (int j = 0; j < lchk_Dpt.Items.Count; j++)
            {

                lchk_Dpt.Items[j].Selected = true;



            }
            //}
        }
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        }
    }

    public string DepartmentId()
    {
        string deptId = "";
        for (int i = 0; i < lchk_Dpt.Items.Count; i++)
        {
            if (lchk_Dpt.Items[i].Selected)
            {
                deptId = deptId + "'" + lchk_Dpt.Items[i].Value + "'" + ",";
            }
        }
        deptId = deptId.TrimEnd(',');
        return deptId;
    }
  
    private void LoadJobCreationInfo()
    {
        GetDepet();
        DataTable jobCreationInfos = new DataTable();

        jobCreationInfos = appDal.GetRecruitmentInfo("");

        if (jobCreationInfos.Rows.Count>0)
        {
            loadGridView.DataSource = jobCreationInfos;
            loadGridView.DataBind();
            
        }
        //else
        //{
        //    loadGridView.DataSource = null;
        //    loadGridView.DataBind();
        //    aShowMessage.ShowMessageBox("No Data Found!!", this);
        //}
       
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
                string AcTionStattus = datKey[1].ToString();

                if (AcTionStattus != "Submitted")
                {
                    Session["JobID"] = "";
                Session["JobID"] = jobReqId;
                Response.Redirect("JobCreation.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
        "alert",
        "alert('Can Not be Edited!!!');",
        true);
                }
               



            }

          

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
                string AcTionStattus = datKey[1].ToString();
                 if (AcTionStattus != "Submitted")
                {
                Session["JobID"] = "";
                Session["JobID"] = jobReqId;
                Response.Redirect("JobCreation.aspx");
                }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(),
         "alert",
         "alert('Can Not be Edited!!!');",
         true);
                 }
               
            }

            
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

                PopUp(Convert.ToInt32(e.CommandArgument.ToString()));

                //Response.Redirect("../Report_Pages/MemoPrintJobCirculation.aspx?mid=" + e.CommandArgument.ToString());
                //Session["Status"] = "View";

            }



        }

    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/PrintJobCirculationReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
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

    protected void homeButton_OnClick(object sender, EventArgs e)
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
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            companyDropDownList.Focus();
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
        }
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
          LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowindex = gvRow.RowIndex;



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
            Session["RecruitmentId"] = "";
            Session["RecruitmentId"] = Idd;
            Session["JobId"] = loadGridView.DataKeys[rowindex][3].ToString();
            Session["CompanyId"] = loadGridView.DataKeys[rowindex][4].ToString(); 
            Session["ForEmpInfoId"] = loadGridView.DataKeys[rowindex][2].ToString();
        }

        Response.Redirect("~/RecruitmentApp_UI/RecruitmentApproval.aspx");
    }
}