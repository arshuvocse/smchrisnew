using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_HRDeclaration : System.Web.UI.Page
{


    HRDeclarationDal aDeclarationDal = new HRDeclarationDal();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialDDL();


            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                hfHRDeclerationId.Value= (Request.QueryString["MID"]);

                //id_mastetID.Value = (Request.QueryString["MID"]);
                onRecord(Convert.ToInt32(Request.QueryString["MID"]));
            }
        }
    }


    protected void onRecord(int id)
    {

        btnsave.Text = "Update Information";

        DataTable dt = aDeclarationDal.Get_HrDeclarationById(id);

        if (dt.Rows.Count > 0)
        {
            ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
            ddlCompany_OnSelectedIndexChanged(null, null);
            ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialId"].ToString();
            IPDAmount.Text = dt.Rows[0]["IPD"].ToString();
            OPDAmount.Text = dt.Rows[0]["OPD"].ToString();
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
           // ddlCompany.SelectedValue = 1.ToString();
        }

    }


    //private void LoadDropDownList()
    //{
    //    aDeclarationDal.GetFinancialList(ddlFinancialYear);
      
    //}


    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("HRDeclarationView.aspx");
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        using (DataTable dt = formDal.Get_FinancialById(ddlCompany.SelectedValue.ToString()))
        {
            ddlFinancialYear.DataSource = dt;
            ddlFinancialYear.DataValueField = "FinancialYearId";
            ddlFinancialYear.DataTextField = "FinancialYearDesc";
            ddlFinancialYear.DataBind();
            ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
        }
        
    }


    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {         
                try
                {

                    if (hfHRDeclerationId.Value == "")
                    {


                        if (CheckFinancailYear(Convert.ToInt32(ddlFinancialYear.SelectedValue),
                            Convert.ToInt32(ddlCompany.SelectedValue)))
                        {


                          ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Already Exits');",
                          true);
                           
                        }
                        else
                        {

                            Int32 HRDeclerationId = SaveDesignationInformation();

                            if (HRDeclerationId > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "alert",
                              "alert('Operation Successfull Done...');window.location ='HRDeclarationView.aspx';",
                              true);
                                
                            }

                        }
                        
                          
                    }
                    else
                    {

                        if (UpdateCheckFinancailYear(Convert.ToInt32(ddlFinancialYear.SelectedValue),Convert.ToInt32(hfHRDeclerationId.Value)))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                         "alert",
                         "alert('Allready Exits');",
                         true);
                           
                        }           
                        else
                        {
                            
                            bool status = UpdateHRDeclarationInformation();

                            if (status)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                              "alert",
                              "alert('Operation Successfull Done...');window.location ='HRDeclarationView.aspx';",
                              true);
                                
                            }
                        }
                    }

                                     
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }             

        }
    }





    private bool CheckFinancailYear(int FinancialId, int companyId)
    {
        bool status = false;

        DataTable dataTable = aDeclarationDal.IsNotExeistFinacialYear(FinancialId, companyId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }



    private bool UpdateCheckFinancailYear(int FinancialId, int hrID)
    {
        bool status = false;

        DataTable dataTable = aDeclarationDal.UpdateIsCheck(FinancialId, hrID);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool Validation()
    {

        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please! Select Company", this);
            ddlCompany.Focus();
            return false;
        }


        if (ddlFinancialYear.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please! Select Financial Year", this);
            return false;
        }



        if (OPDAmount.Text == "")
        {
            aShowMessage.ShowMessageBox("Please! Input OPD Amount", this);
            return false;
        }


        if (IPDAmount.Text == "")
        {
            aShowMessage.ShowMessageBox("Please! Input IPD Amount", this);
            return false;
        }


      
        return true;
    }



    private void Clear()
    {
        
        ddlFinancialYear.Items.Clear();
        IPDAmount.Text = "";
        OPDAmount.Text = "";
    
    }


    private HRDecleration PrepareDataForSave()
    {
        var aInformationDao = new HRDecleration();

        aInformationDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        if (hfHRDeclerationId.Value != "")
        {
            aInformationDao.HRDeclerationId = Convert.ToInt32(hfHRDeclerationId.Value);
            aInformationDao.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            aInformationDao.UpdateDate = DateTime.Now;
        }
        else
        {
            aInformationDao.HRDeclerationId = 0;
            aInformationDao.EntryBy = Convert.ToInt32(Session["UserId"].ToString());
            aInformationDao.EntryDate = DateTime.Now;
        }

        aInformationDao.IPD = Convert.ToDecimal(IPDAmount.Text);
        aInformationDao.OPD = Convert.ToDecimal(OPDAmount.Text); 
        aInformationDao.FinancialId = Convert.ToInt32(ddlFinancialYear.SelectedValue);


       

        return aInformationDao;
    }


    private Int32 SaveDesignationInformation()
    {
        Int32 retVal;
        try
        {
        
            retVal = aDeclarationDal.SaveDesignationInfo(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }


    private bool UpdateHRDeclarationInformation()
    {
        bool retVal;
        try
        {

            retVal = aDeclarationDal.UpdateHeDeclarationInfo(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }
  
}