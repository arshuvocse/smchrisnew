using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HealthCare_Dao;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_AdvanceBillEntry : System.Web.UI.Page
{
    private BillSettlementDal aSettlementDal = new BillSettlementDal();

    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private AdvanceBillDal advanceBill = new AdvanceBillDal();


    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
            LoadInitialDDL();

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {

                hfReimbursmentFormId.Value = Request.QueryString["id"];
                onRecord(Convert.ToInt32(Request.QueryString["id"]));
            }

        }
        {
            try
            {
              //  OnDataBound(null, null);
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
    }

    protected void onRecord(int id)
    {
        DataTable dt = aSettlementDal.Get_EmpInfoForBillSettlement(id);

        if (dt.Rows.Count > 0)
        {

            lblEmployeeName.Text = dt.Rows[0]["EmpName"].ToString().Trim();

            hfEmpID.Value = dt.Rows[0]["EmpInfoId"].ToString().Trim();

            lblEmpId.Text = dt.Rows[0]["EmpMasterCode"].ToString().Trim();

            deptNameLabel.Text = dt.Rows[0]["DepartmentName"].ToString().Trim();

            desigNameLabel.Text = dt.Rows[0]["Designation"].ToString().Trim();

            try
            {
                OfficailMobile.Text = dt.Rows[0]["OfficialMobile"].ToString().Trim();
            }
            catch (Exception)
            {
                //throw;
            }

            LocationLabel.Text = dt.Rows[0]["SalaryLocation"].ToString();
            lblPlace.Text = dt.Rows[0]["Location"].ToString();

            ReportingLabel.Text = dt.Rows[0]["ReportingToName"].ToString();

        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AdvanceBillView.aspx");
    }

    private void ReadOnltDate()
    {
        ddlCompany.Attributes.Add("readonly", "readonly");
        ddlFinancialYear.Attributes.Add("readonly", "readonly");

        
    }

    private void LoadInitialDDL()
    {


        if (hfMasterId.Value == "")
        {
            using (DataTable dt = _commonDataLoad.GetCompanyDDL())
            {

                ddlCompany.DataSource = dt;
                ddlCompany.DataValueField = "Value";
                ddlCompany.DataTextField = "TextField";
                ddlCompany.DataBind();
                ddlCompany.SelectedIndex = 1;
             

                //if (ddlCompany.SelectedValue != "")
                //{
                //    using (DataTable dtt = formDal.Get_FinancialById(ddlCompany.SelectedValue.ToString()))
                //    {
                //        ddlFinancialYear.DataSource = dtt;
                //        ddlFinancialYear.DataValueField = "FinancialYearId";
                //        ddlFinancialYear.DataTextField = "FinancialYearDesc";
                //        ddlFinancialYear.DataBind();
                //        ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
                //    }
      
                //}

            }
        }
        else
        {
            using (DataTable dt = _commonDataLoad.GetCompanyDDL())
            {

                ddlCompany.DataSource = dt;
                ddlCompany.DataValueField = "Value";
                ddlCompany.DataTextField = "TextField";
                ddlCompany.DataBind();
                ddlCompany.SelectedIndex = 1;

            }
        }
        ddlCompany_OnSelectedIndexChanged(null, null);
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


            try
            {
                using (DataTable dtt = formDal.Get_FinancialByIForSelecttedValue(ddlCompany.SelectedValue.ToString()))
                {
                    ddlFinancialYear.SelectedValue = dtt.Rows[0]["FinancialYearId"].ToString();
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        using (DataTable dt222 = _commonDataLoad.GetEmpDDLIsActive(ddlCompany.SelectedValue.ToString()))
        {
            ddlEmployee.DataSource = dt222;
            ddlEmployee.DataValueField = "EmpInfoId";
            ddlEmployee.DataTextField = "EmpName";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            ddlEmployee.SelectedIndex = 0;
        }

       
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




    private bool Validation()
    {
        if (ddlCompany.SelectedValue == " " || ddlCompany.SelectedIndex == 0)
        {

            AlertMessageBoxShow("Please Select Company");
            ddlCompany.Focus();
            return false;
        }

        if (ddlFinancialYear.SelectedValue == " " || ddlFinancialYear.SelectedIndex == 0)
        {

            AlertMessageBoxShow("Please Select Financial Year");
            ddlFinancialYear.Focus();
            return false;
        }


        if (ddlEmployee.SelectedValue == " " || ddlEmployee.SelectedIndex == 0)
        {

            AlertMessageBoxShow("Please Select Employee Year");
            ddlEmployee.Focus();
            return false;
        }


     

        if (txtAmount.Text == "")
        {
            AlertMessageBoxShow("Please input Amount!");
            txtAmount.Focus();
            return false;
        }
        if (CarryPerson.Text == "")
        {
            AlertMessageBoxShow("Please input Payment To!");
            CarryPerson.Focus();
            return false;
        }


        if (Remarks.Text == "")
        {
            AlertMessageBoxShow("Please input Purpose!");
            Remarks.Focus();
            return false;
        }


        if (hfSignature.Value == "")
        {
            AlertMessageBoxShow("Please Upload!");
           
            return false;
        }


        return true;
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void save_onclick(object sender, EventArgs e)
    {

        if (Validation())
        {
            AdvanceBill advance = new AdvanceBill();
            advance.ReimbursFromMasterId = Convert.ToInt32(0);
            advance.RequitisionNo = "";
            advance.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            advance.FinancialId = Convert.ToInt32(ddlFinancialYear.SelectedValue);


            advance.IsOPD = false;
            advance.IsIPD = false;
            advance.IsSpecial = false;

            if (inlineRadio.Items[0].Selected)
            {
                advance.IsOPD = true;
            }
            if (inlineRadio.Items[1].Selected)
            {
                advance.IsIPD = true;
            }

            if (inlineRadio.Items[2].Selected)
            {
                advance.IsSpecial = true;
            }
            advance.Amount = Convert.ToDecimal(txtAmount.Text);
            advance.EntryBy = Convert.ToInt32(Session["UserId"].ToString());
            advance.Remarks = Remarks.Text;
            advance.CarryPerson = CarryPerson.Text;
            advance.EmpInfoId = int.Parse(ddlEmployee.SelectedValue);
            advance.MemoImage = hfSignature.Value;
            advance.ImagePath = string.IsNullOrEmpty(hfSignature.Value) ? null : "C:\\inetpub\\wwwroot\\SMCHRIS\\UploadImg\\" + hfSignature.Value;
            int Id = advanceBill.data_save(advance);

            if (Id > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='AdvanceBillView.aspx';",
                    true);
            }

        }
    }

    protected void ddlRequisitionNo_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlRequisitionNo.SelectedValue != "")
        {
            DataTable dt = advanceBill.Get_FromMasterForAdvanceBill(Convert.ToInt32(ddlRequisitionNo.SelectedValue));

            if (dt.Rows.Count > 0)
            {
                //ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                //ddlCompany_OnSelectedIndexChanged(null,null);

                if (ddlCompany.SelectedValue != "")
                {
                    using (DataTable data = formDal.Get_FinancialById(ddlCompany.SelectedValue.ToString()))
                    {
                        ddlFinancialYear.DataSource = data;
                        ddlFinancialYear.DataValueField = "FinancialYearId";
                        ddlFinancialYear.DataTextField = "FinancialYearDesc";
                        ddlFinancialYear.DataBind();
                        ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
                        ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                    }
                }

                string status = dt.Rows[0]["Type"].ToString();

                if (status == "OPD")
                {

                    inlineRadio.Items[0].Selected = true;
                   
                }
                else if (status == "IPD")
                {

                    inlineRadio.Items[1].Selected = true;

                }
                else if (status == "Special")
                {
                    inlineRadio.Items[2].Selected = true;
                }

                ReadOnltDate();
            }



            DataTable dataTable = aSettlementDal.Get_EmpInfoForBillSettlement(Convert.ToInt32(ddlRequisitionNo.SelectedValue));

            if (dataTable.Rows.Count > 0)
            {

                lblEmployeeName.Text = dataTable.Rows[0]["EmpName"].ToString().Trim();
                hfEmpID.Value = dataTable.Rows[0]["EmpInfoId"].ToString().Trim();
                lblEmpId.Text = dataTable.Rows[0]["EmpMasterCode"].ToString().Trim();
                deptNameLabel.Text = dataTable.Rows[0]["DepartmentName"].ToString().Trim();
                desigNameLabel.Text = dataTable.Rows[0]["Designation"].ToString().Trim();
                try
                {
                    OfficailMobile.Text = dataTable.Rows[0]["OfficialMobile"].ToString().Trim();
                }
                catch (Exception)
                {
                    //throw;
                }

                LocationLabel.Text = dt.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dt.Rows[0]["Location"].ToString();
                ReportingLabel.Text = dataTable.Rows[0]["ReportingToName"].ToString();

            }

            DataTable claim = advanceBill.Get_ClaimDetailsById(Convert.ToInt32(ddlRequisitionNo.SelectedValue));

            if (claim.Rows.Count > 0)
            {
                GridView1.DataSource = claim;
                GridView1.DataBind();
                decimal markTotal = 0;
                GettotalMark(markTotal);
            }
            else
            {
                
            }

        }
    }

    private void GettotalMark(decimal markTotal)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");


            if (Amount.Text == "")
            {
                markTotal = markTotal + 0;
            }
            else
            {
                markTotal = markTotal + Convert.ToDecimal(Amount.Text.ToString());
            }
        }

        Label tst2 = (Label)GridView1.FooterRow.FindControl("lblTotalMark");
        tst2.Text = markTotal.ToString();
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "";
        cell.BackColor = ColorTranslator.FromHtml("#ffffff");
        cell.ForeColor = Color.Black;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Claim Details";
        cell.BackColor = ColorTranslator.FromHtml("#B4C6E7");
        cell.ForeColor = Color.Black;
        row.Controls.Add(cell);

        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
    ShowMessage aShowMessage = new ShowMessage();

    protected void txtAmount_OnTextChanged(object sender, EventArgs e)
    {
        GettotalMark(0);
        decimal markTotal = 0;
        decimal Total = 0;
        decimal amnt = 0;

        if (txtAmount.Text == "0")
        {
            aShowMessage.ShowMessageBox("Amount (BDT) can not be Zero", this);
            txtAmount.Focus();
            txtAmount.Text = string.Empty;
        }

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");


            if (Amount.Text == "")
            {
                markTotal = markTotal + 0;
            }
            else
            {
                markTotal = markTotal + Convert.ToDecimal(Amount.Text.ToString());
            }
        }
        Total = markTotal;
        if (txtAmount.Text != "")
        {
            if (txtAmount.Text != "0")
            {
                amnt = Convert.ToDecimal(txtAmount.Text.ToString());


            }


        }

        if (Total >= amnt)
        {

        }
        else
        {
            aShowMessage.ShowMessageBox("Amount can not be greater than Total Amount (BDT)", this);
            txtAmount.Focus();
            txtAmount.Text = string.Empty;

        }
    }

    protected void ddlEmployee_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            DataTable dtEmp = formDal.GetEmployeeDetails(Convert.ToInt32(ddlEmployee.SelectedValue));
            if (dtEmp.Rows.Count > 0)
            {

                lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();

                hfEmpID.Value = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();

                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();

                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();

                try
                {
                    OfficailMobile.Text = dtEmp.Rows[0]["OfficialMobile"].ToString().Trim();
                }
                catch (Exception)
                {
                    //throw;
                }

                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();

                //Bank Info

                //lblBankAccountNo.Text = dtEmp.Rows[0]["BankAccountNo"].ToString().Trim();
                //lblBankName.Text = dtEmp.Rows[0]["BankName"].ToString().Trim();
                //lblBranchName.Text = dtEmp.Rows[0]["BranchName"].ToString().Trim();
                //lblRoutingNo.Text = dtEmp.Rows[0]["RoutingNo"].ToString().Trim();


              

            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
}