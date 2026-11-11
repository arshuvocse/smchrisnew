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

public partial class HealthCare_UI_AdvanceBillSelfEntry : System.Web.UI.Page
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

        inlineRadio1.Enabled = false;
        inlineRadio2.Enabled = false;
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

                try
                {
                    ddlCompany.SelectedValue = Session["CompanyId"].ToString();
                }
                catch (Exception)
                {

                    ddlCompany.SelectedIndex = 1;
                }
                ddlCompany_OnSelectedIndexChanged(null, null);

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
            }
        }
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataTable dt = formDal.Get_FinancialByIForSelecttedValue(ddlCompany.SelectedValue.ToString()))
        {
            ddlFinancialYear.DataSource = dt;
            ddlFinancialYear.DataValueField = "FinancialYearId";
            ddlFinancialYear.DataTextField = "FinancialYearDesc";
            ddlFinancialYear.DataBind();
            ddlFinancialYear.SelectedIndex = 1;
        }

        //using (DataTable dt = advanceBill.Get_RequisitionNumberDDl(ddlCompany.SelectedValue.ToString()))
        //{
        //    ddlRequisitionNo.DataSource = dt;
        //    ddlRequisitionNo.DataValueField = "ReimbursFromMasterId";
        //    ddlRequisitionNo.DataTextField = "RequitisionNo";
        //    ddlRequisitionNo.DataBind();
        //    ddlRequisitionNo.Items.Insert(0, new ListItem("Please Select Requisition Number.....", String.Empty));
        //}


        using (DataTable dtemp = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue.ToString()))
        {
            ddlRequisitionNo.DataSource = dtemp;
            ddlRequisitionNo.DataValueField = "EmpInfoId";
            ddlRequisitionNo.DataTextField = "EmpName";
            ddlRequisitionNo.DataBind();
            ddlRequisitionNo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            try
            {
                ddlRequisitionNo.SelectedValue = Convert.ToInt32(Session["EmpInfoId"]).ToString();
                ddlRequisitionNo_OnSelectedIndexChanged(null, null);
            }
            catch (Exception)
            {
                ddlRequisitionNo.SelectedIndex = 0;
                //throw;
            }
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
        if (ddlCompany.SelectedValue == "" )
        {

            AlertMessageBoxShow("Please Select Company");
            ddlCompany.Focus();
            return false;
        }

        if (ddlFinancialYear.SelectedValue == "" )
        {

            AlertMessageBoxShow("Please Select Financial Year");
            ddlFinancialYear.Focus();
            return false;
        }

        if (ddlRequisitionNo.SelectedValue == "")
        {

            AlertMessageBoxShow("Please Select Requisition Number");
            ddlRequisitionNo.Focus();
            return false;
        }


        if (txtAmount.Text == "")
        {
            AlertMessageBoxShow("Please input Amount!");
            txtAmount.Focus();
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
            advance.ReimbursFromMasterId = Convert.ToInt32(ddlRequisitionNo.SelectedValue);
            advance.RequitisionNo = ddlRequisitionNo.SelectedItem.Text;
            advance.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            advance.FinancialId = Convert.ToInt32(ddlFinancialYear.SelectedValue);

            if (inlineRadio1.Checked)
            {
                advance.IsOPD = false;
            }
            if (inlineRadio2.Checked)
            {
                advance.IsOPD = true;
            }
            advance.Amount = Convert.ToDecimal(txtAmount.Text);
            advance.EntryBy = Convert.ToInt32(Session["UserId"].ToString());
            advance.Remarks = Remarks.Text;
            advance.CarryPerson = CarryPerson.Text;
            int Id = advanceBill.data_save(advance);

            if (Id > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='AdvanceBillSelfEntry.aspx';",
                    true);
            }

        }
    }


    protected void ddlRequisitionNo_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlRequisitionNo.SelectedValue != "")
        {
            DataTable dataTable = advanceBill.Get_FromMasterForAdvanceBillEmpId(Convert.ToInt32(ddlRequisitionNo.SelectedValue));

            if (dataTable.Rows.Count > 0)
            {
                //ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                //ddlCompany_OnSelectedIndexChanged(null,null);

                

               // bool status = Convert.ToBoolean(dt.Rows[0]["IsOPD"].ToString());

                //if (status)
                //{
                //    inlineRadio2.Checked = true;
                //    inlineRadio1.Checked = false;
                //}
                //else
                //{
                //    inlineRadio1.Checked = true;
                //    inlineRadio2.Checked = false;
                //}

                //ReadOnltDate();
            }



            //DataTable dataTable = aSettlementDal.Get_EmpInfoForBillSettlement(Convert.ToInt32(ddlRequisitionNo.SelectedValue));

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

                LocationLabel.Text = dataTable.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dataTable.Rows[0]["Location"].ToString();
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

    protected void inlineRadio1_OnCheckedChangedck(object sender, EventArgs e)
    {
        if (inlineRadio1.Checked)
        {
            inlineRadio2.Checked = false;
        }
        else
        {
            inlineRadio1.Checked = true;
        }
    }

    protected void OPD_Click(object sender, EventArgs e)
    {


        if (inlineRadio2.Checked)
        {
            inlineRadio1.Checked = false;

        }
        else
        {
            inlineRadio1.Checked = true;
        }
    }
}