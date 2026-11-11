using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;

public partial class HealthCare_UI_ExpenseReimbursementFormApproval : System.Web.UI.Page
{
    private ReimbursmentFormDal aDal = new ReimbursmentFormDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Get_FormListForApproval();
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
    }

    protected  void Get_FormListForApproval()
    {
        DataTable dt = aDal.Get_DataListForFormApproval();

        if (dt.Rows.Count > 0)
        {
           // ActionBtn.Visible = true;
           gv_ViewList.DataSource = dt;
            gv_ViewList.DataBind();
        }
        else
        {
          //  btnsave.Visible = false;
        }
     
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void CheckUncheckAll(object sender, EventArgs e)
    {

        CheckBox ChkBoxHeader = (CheckBox)gv_ViewList.HeaderRow.FindControl("CheckAll");
        foreach (GridViewRow row in gv_ViewList.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("Checked");
            if (ChkBoxHeader.Checked)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
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

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    private bool Validation()
    {
        int count = 0;
        int row = 0;



        //if (actionRadioButtonList.SelectedIndex == -1)
        //{
        //    AlertMessageBoxShow("Please Select Approval Action");
        //    return false;
        //}


        for (int i = 0; i < gv_ViewList.Rows.Count; i++)
        {
            row++;
            CheckBox check = (CheckBox)gv_ViewList.Rows[i].FindControl("Checked");
            if (!check.Checked)
            {
                count++;
            }
        }
        if (row == count)
        {
            AlertMessageBoxShow("Please! Select minimum One");
            return false;
        }

        return true;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        //if (Validation())
        //{


        //    string status = actionRadioButtonList.SelectedValue.ToString();

        //    string FormMasterId = "";
        //    for (int i = 0; i < gv_ViewList.Rows.Count; i++)
        //    {
        //        CheckBox check = (CheckBox)gv_ViewList.Rows[i].FindControl("Checked");

        //        if (check.Checked)
        //        {
        //            int EmpId = Convert.ToInt32(gv_ViewList.DataKeys[i][0]);

        //            FormMasterId += EmpId + ",";

        //        }
        //    }

        //    FormMasterId = FormMasterId.Trim(',');

        //    bool ApprovalStatus = aDal.ExpenseReimbursementFormAppoval(FormMasterId, status);

        //    if (ApprovalStatus)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(),
        //            "alert",
        //            "alert('Operation Successfull Done...');window.location ='ExpenseReimbursementFormApproval.aspx';",
        //            true);
        //    }
        //}

    }

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        int EmpId = Convert.ToInt32(gv_ViewList.DataKeys[rowID][0]);
        int ForEmpId = Convert.ToInt32(gv_ViewList.DataKeys[rowID][1]);

        Session["AppLogId"] = gv_ViewList.DataKeys[rowID][2].ToString();

     //   Response.Redirect("ExpenseFormApproval.aspx?MID=" + EmpId + "ForEmpId=" + ForEmpId+"");

        Response.Redirect("ExpenseFormApproval.aspx?MID=" + EmpId + "&ForEmpId=" + ForEmpId + "");
    }
}