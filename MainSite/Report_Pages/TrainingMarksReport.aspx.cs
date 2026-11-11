using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.TrainingDAL;

public partial class Report_Pages_TrainingMarksReport : System.Web.UI.Page
{
    TrainingDAL aTrainingDal = new TrainingDAL();
    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList();
        }
    }

    public void DropDownList()
    {
        aCommonDataLoadDal.CompanyDropDown(ddlCompany, "");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aCommonDataLoadDal.FinYearByCompDropDown(ddlFinancialYear, ddlCompany.SelectedValue);
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aTrainingDal.TrainingRecordDropDown(ddlTrainingRecord, ddlCompany.SelectedValue, ddlFinancialYear.SelectedValue);

    }

    protected void ddlTrainingRecord_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        LoadInfo();

    }


    public void LoadInfo()
    {
       
            DataTable dtDetails = aTrainingDal.GetEmployeeMarksInfo(GenerateParameter());


            if (dtDetails.Rows.Count > 0)
            {
                gv_AllEmployee.DataSource = dtDetails;
                gv_AllEmployee.DataBind();
            }
            else
            {
                gv_AllEmployee.DataSource = null;
                gv_AllEmployee.DataBind();
            }

        
        
    }

    private String GenerateParameter()
    {
        string pram = " WHERE ";

        if (ddlCompany.SelectedValue != "")
        {
            pram = pram + " TDM.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlFinancialYear.SelectedValue != "")
        {
            pram = pram + " AND TDM.FinancialYearId = " + ddlFinancialYear.SelectedValue;
        }

        if (ddlTrainingRecord.SelectedValue != "")
        {
            pram = pram + " AND MRKSM.TrainingRecordMasterId = " + 1;
        }

        return pram;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_AllEmployee.Rows.Count > 0)
        {
            string attachment = "attachment; filename=TrainingMarks.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv_AllEmployee.AllowPaging = false;



            //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;

            this.LoadInfo();

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            gv_AllEmployee.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            gv_AllEmployee.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_AllEmployee.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_AllEmployee.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            gv_AllEmployee.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> Company: " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span  style='text-align:left'><h3> Financial Year: " + ddlFinancialYear.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Training Marks	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }  

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            if (ddlFinancialYear.SelectedValue != "")
            {
                LoadInfo();
            }
            else
            {
                showMessageBox("Select financial year !!!");
            }

        }
        else
        {
            showMessageBox("Select company !!!");
        }
        
    }
}