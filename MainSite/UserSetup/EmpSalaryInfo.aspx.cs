using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_EmpSalaryInfo : System.Web.UI.Page
{
     ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
         if (Session["UserId"] != "")
        {
            _userId = Convert.ToString(Session["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            using (DataTable dtemp = _commonDataLoad.GetBankName()
           )
            {


                ddlBankName.DataSource = dtemp;
                ddlBankName.DataValueField = "BankId";
                ddlBankName.DataTextField = "BankName";
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Please Select a Bank.....", String.Empty));
                 
            }

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpkEmpId.Value = mid.ToString();


                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        empMasterCode.Text =
                            emp.EmpMasterCode;


                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }

                        lblEmpName.Text = emp.EmpName;


                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }
                    }


                    DataTable dtMaster = AMAsterDal.GetMasterEmpDataById(hdpkEmpId.Value);
                    if (dtMaster.Rows.Count > 0)
                    {
                        hfMasterId.Value = dtMaster.Rows[0]["EmpSalaryInfoId"].ToString();
                        txtBasic.Text = dtMaster.Rows[0]["BasicPay"].ToString();
                        txtHouseRent.Text = dtMaster.Rows[0]["HouseRent"].ToString();
                        txtMedical.Text = dtMaster.Rows[0]["Medical"].ToString();
                        txtConveyance.Text = dtMaster.Rows[0]["Conveyance"].ToString();
                        txtWashing.Text = dtMaster.Rows[0]["Washing"].ToString();

                        ddlPaymentType.SelectedValue = dtMaster.Rows[0]["PaymentType"].ToString();
                        ddlBankName.SelectedValue = dtMaster.Rows[0]["BankNameId"].ToString();

                        if (dtMaster.Rows[0]["ProvidentFundEligible"].ToString()=="True")
                        {
                            rbProvidentFundEligible.Items[0].Selected = true;
                        }
                        if (dtMaster.Rows[0]["ProvidentFundEligible"].ToString() == "False")
                        {
                            rbProvidentFundEligible.Items[1].Selected = true;
                        }
                        txtBankAccountNo.Text = dtMaster.Rows[0]["BankAccountNo"].ToString();
                        txtPF.Text = dtMaster.Rows[0]["PF"].ToString();
                        txtMonthlyTax.Text = dtMaster.Rows[0]["MonthlyTax"].ToString();


                    }

                }
            }
        }

    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        SaveUpdateInfo("");

    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }


       public bool Validation()
    {

       



        if (txtBasic.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            txtBasic.Focus();
            return false;
        }


        if (txtHouseRent.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            txtHouseRent.Focus();
            return false;
        }

        return true;
    }

    private void SaveUpdateInfo(string btn)
    {
        if (Validation() == true)
        {
            EmpSalaryInfoDAO aMaster = new EmpSalaryInfoDAO();

            aMaster.EmpSalaryInfoId = hfMasterId.Value == "" ? 0 : Convert.ToInt32(hfMasterId.Value);
            aMaster.EmpInfoId = hdpkEmpId.Value == "" ? 0 : Convert.ToInt32(hdpkEmpId.Value);



            aMaster.BasicPay = Convert.ToDecimal(txtBasic.Text);
            aMaster.HouseRent = Convert.ToDecimal(txtHouseRent.Text);

            if (txtMedical.Text!="")
             {
                 aMaster.Medical = Convert.ToDecimal(txtMedical.Text);
            }
            else
            {
                aMaster.Medical = null;
            }

            if (txtConveyance.Text != "")
            {
                aMaster.Conveyance = Convert.ToDecimal(txtConveyance.Text);
            }
            else
            {
                aMaster.Conveyance = null;
            }
            if (txtWashing.Text != "")
            {
                aMaster.Washing = Convert.ToDecimal(txtWashing.Text);
            }
            else
            {
                aMaster.Washing = null;
            }

            aMaster.PaymentType = (ddlPaymentType.SelectedValue);
            if (ddlBankName.SelectedValue!="")
            {
                aMaster.BankNameId = Convert.ToInt32(ddlBankName.SelectedValue);
            }
            else
            {
                aMaster.BankNameId = null;
            }

            aMaster.BankAccountNo = (txtBankAccountNo.Text.Trim());
            if (rbProvidentFundEligible.Items[0].Selected)
            {
                aMaster.ProvidentFundEligible = true;

            }
            else
            {
                aMaster.ProvidentFundEligible = false;
                
            }
            if (txtPF.Text != "")
            {
                aMaster.PF = Convert.ToDecimal(txtPF.Text);
            }
            else
            {
                aMaster.PF = null;
            }

            if (txtMonthlyTax.Text != "")
            {
                aMaster.MonthlyTax = Convert.ToDecimal(txtMonthlyTax.Text);
            }
            else
            {
                aMaster.MonthlyTax = null;
            }
                
               
                    int pk = AMAsterDal.SaveEmpSalaryMaster(aMaster, Session["UserId"].ToString());
            if (pk > 0)
            {
                if (btn == "Next")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert",
            "alert('Operation Successful...!');window.location ='DepartedInfo.aspx?mid=" + Request.QueryString["mid"] + "';",
            true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...!');window.location ='EmployeeInfoListUpdate.aspx';",
                    true);
                }
                
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }

    protected
         void btn_Next_OnClick(object sender, EventArgs e)
    {
        string btn = "Next";
        SaveUpdateInfo(btn);
        
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
       }

    protected void lbPrevious_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpOthers?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
        
    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("DepartedInfo?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }
}