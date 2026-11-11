using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Medica_Ul_EmpMedicalCheckUpView : System.Web.UI.Page
{
    EmpExitDal aExitDal = new EmpExitDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            //   UserPersmissionValidation();
        
            LoadDropDownList();
            using (DataTable dt = _commonDataLoad.GetCompanyDDL())
            {
                company.DataSource = dt;
                company.DataValueField = "Value";
                company.DataTextField = "TextField";
                company.DataBind();
                company.SelectedIndex = 1;
            }
        }

        txtDate.Attributes.Add("readonly", "readonly");
        txtToDate.Attributes.Add("readonly", "readonly");

        try
        {

            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }
    }
    private void LoadDropDownList()
    {
        aExitDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany.SelectedIndex = 1;

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
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    VivaSetupInfoEntryDAL aDaL = new VivaSetupInfoEntryDAL();
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

    private void LoadRegionInformation()
    {
        try
        {
            DataTable dataTable = aDaL.GeMedicalCheckupInfo(" and tblMedicalCheckUpMaster.CompanyId IN (" + CompanyId() + ")" + param());

            if (dataTable.Rows.Count > 0)
            {
                loadGridView.DataSource = dataTable;
                loadGridView.DataBind();
                loadGridView.UseAccessibleHeader = true;
                loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
                loadGridView.UseAccessibleHeader = true;
               
            }
            else
            {
                loadGridView.DataSource = null;
                loadGridView.DataBind();
                aShowMessage.ShowMessageBox("No Data Found !!!", this);
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    private string param()
    {
        string parameter = " ";


        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND   tblMedicalCheckUpMaster.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
        }


        if (txtDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND tblMedicalCheckUpMaster.Date BETWEEN '" + txtDate.Text + "' AND '" + txtToDate.Text + "' ";
        }
        if (txtDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND  tblMedicalCheckUpMaster.Date BETWEEN '" + txtDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  tblMedicalCheckUpMaster.Date BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
        }





        return parameter;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmpMedicalCheckUp.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                string areaId = dataKey[0].ToString();
                Session["Status"] = "Edit";
                Session["VacancyCirculationId"] = "";
                Session["VacancyCirculationId"] = areaId;
            }

            Response.Redirect("EmpMedicalCheckUp.aspx");
        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string VacancyCirculationId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["VacancyCirculationId"] = "";
            Session["VacancyCirculationId"] = VacancyCirculationId;
            Session["Status"] = "View";
            Response.Redirect("EmpMedicalCheckUp.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string VacancyCirculationId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["VacancyCirculationId"] = "";
            Session["VacancyCirculationId"] = VacancyCirculationId;
            Session["Status"] = "Delete";
            Response.Redirect("EmpMedicalCheckUp.aspx");
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var areaId = dataKey[0].ToString();

        //        if (!CheckAreaAllocateOrNot(areaId))
        //        {
        //            if (aInformationDal.DeleteAreaInfoById(areaId))
        //            {
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //                LoadRegionInformation();
        //            }
        //        }
        //        else
        //        {

        //         //   showMessageBox("Cann't delete because it contains a Region."); 
        //            LoadRegionInformation();
        //            aShowMessage.ShowMessageBox("Can not be deleted because this is used in Job Location.", this);
        //          //aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //            //
        //        }
        //    }
        //}
    }


    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            LoadRegionInformation();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
        }
    }

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
}