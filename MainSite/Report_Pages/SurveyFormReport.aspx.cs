using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ExitManagement_DAL;
using DAL.Permission_DAL;
using DAL.Transfer_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_SurveyFormReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            loadDropDownList();
        }
    }
    EmployeeSeparationReportDAL aSeparationDAL = new EmployeeSeparationReportDAL();

    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    private void loadDropDownList()
    {
        aCommonDataLoadDal.CompanyDropDown(companyDropDownList, " WHERE CompanyId IN(" + CompanyId() + ")");
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);



    }
    SurveyDeclaretionEntryDAL atblEmployeePromotionEntryDAL = new SurveyDeclaretionEntryDAL();
    private void LoadInfo()
    {
        DataTable EmployeedataTable = atblEmployeePromotionEntryDAL.GeEmpGeneralInfoById(SurveyNameDropDownList.SelectedValue);

        if (EmployeedataTable.Rows.Count > 0)
        {
            EmpSaveGridView1.DataSource = EmployeedataTable;
            EmpSaveGridView1.DataBind();
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

    PermissionDAL aPermissionDal = new PermissionDAL();

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
       
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        SurveyNameDropDownList.Items.Clear();
        SeparationFinancialYearDropDownList.Items.Clear();
        if (companyDropDownList.SelectedValue!="")
        {
            aSeparationDAL.FinYearByCompDropDown(SeparationFinancialYearDropDownList, companyDropDownList.SelectedValue);
            aSeparationDAL.SurveyByCompDropDown(SurveyNameDropDownList, companyDropDownList.SelectedValue, SeparationFinancialYearDropDownList.SelectedValue);
        }
        else
        {
            SurveyNameDropDownList.Items.Clear();
            SeparationFinancialYearDropDownList.Items.Clear();
        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (SurveyNameDropDownList.SelectedValue!="")
        {
            if (e.CommandName == "ViewReport")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument);


                var datKey = EmpSaveGridView1.DataKeys[0];

                if (datKey != null)
                {

                    string EmpID = datKey["EmpInfoId"].ToString();
                    string surveyId = SurveyNameDropDownList.SelectedValue;
                    PopUp(surveyId, EmpID);

                 //   Response.Redirect("../Report_UI/SurveyReportViewr.aspx?MasterID=" + surveyId + "&EmpID=" + EmpID);
                   

                }


            }
        }
      
    }
    private void PopUp(string surveyId, string EmpID)
    {
        string url = "../Report_UI/SurveyReportViewr.aspx?rptType=" + "rptSurvey" + "&MasterID=" + surveyId+ "&EmpID=" + EmpID;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (validation())
        {
            LoadInfo();
        }
       
    }
    ShowMessage aShowMessage = new ShowMessage();
    private bool validation()
    {
       


        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select a company!!!", this);
            companyDropDownList.Focus();
            return false;
        }


        if (SeparationFinancialYearDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Financial Year!!!", this);
            SeparationFinancialYearDropDownList.Focus();
            return false;
        }


        if (SurveyNameDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Survey Name!!!", this);
            SurveyNameDropDownList.Focus();
            return false;
        }

        return true;
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void SeparationFinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (SeparationFinancialYearDropDownList.SelectedValue != "")
        {
          
            aSeparationDAL.SurveyByCompDropDown(SurveyNameDropDownList, companyDropDownList.SelectedValue, SeparationFinancialYearDropDownList.SelectedValue);
        }
        else
        {
            SurveyNameDropDownList.Items.Clear();
        }
    }
}