using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_CompanyInformationView : System.Web.UI.Page
{
    CompanyInformationDal aInformationDal = new CompanyInformationDal();
    Validation aValidation = new Validation();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCompanyInformation();
        }
    }

    private void LoadCompanyInformation()
    {
        DataTable companyInfo = aInformationDal.GetCompanyInformation();

        if (companyInfo.Rows.Count > 0)
        {
            loadGridView.DataSource = companyInfo;
            loadGridView.DataBind();
        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string companyId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["companyId"] = "";
            Session["companyId"] = companyId;

            Response.Redirect("CompanyInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string companyId = loadGridView.DataKeys[rowindex][0].ToString();

            if (aInformationDal.DeleteCompanyInfoById(companyId))
            {
                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
                LoadCompanyInformation();
            }
        }
    }
}