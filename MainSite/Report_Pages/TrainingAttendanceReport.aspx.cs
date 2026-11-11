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
using DAL.Report_DAL;
using DAL.TrainingDAL;

public partial class Report_Pages_TrainingAttendanceReport : System.Web.UI.Page
{
    TrainingAttendanceReportDal aTrainingDal = new TrainingAttendanceReportDal();
    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        ContractualstartDate.Attributes.Add("readonly", "readonly");
        ContractualendDate.Attributes.Add("readonly", "readonly");
        if (!IsPostBack)
        {
            DropDownList();
        }
    }

    public void DropDownList()
    {
        aCommonDataLoadDal.CompanyDropDown(ddlCompany, "");
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);
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
        if (ddlCompany.SelectedValue != "")
        {

            if (ddlFinancialYear.SelectedValue != "")
            {
                if (ddlTrainingRecord.SelectedValue != "")
                {
                    DataTable dtDetails = aTrainingDal.GetTrainingAttendanceReport(GenerateParameter());


                    if (dtDetails.Rows.Count > 0)
                    {
                        loadGridView.DataSource = dtDetails;
                        loadGridView.DataBind();
                    }
                    else
                    {
                        loadGridView.DataSource = null;
                        loadGridView.DataBind();
                    }
                }
                else
                {
                    showMessageBox("Please select training!!!");
                }
            }
            else
            {
                showMessageBox("Please select financial year!!!");
            }
        }
        else
        {
            showMessageBox("Please select company!!!");
        }

    }


    private String GenerateParameter()
    {
        string pram = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            pram = pram + " AND MRKSM.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlFinancialYear.SelectedValue != "")
        {
            pram = pram + " AND MRKSM.FinancialYearId = " + ddlFinancialYear.SelectedValue;
        }

        if (ddlTrainingRecord.SelectedValue != "")
        {
            pram = pram + " AND MRKSM.TrainingRecordMasterId = "+ ddlTrainingRecord.SelectedValue;
        }

        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text != string.Empty)
        {
            pram = pram + " AND TANDC.ATTDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }
        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text == string.Empty)
        {
            pram = pram + " AND TANDC.ATTDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (ContractualstartDate.Text == string.Empty && ContractualendDate.Text != string.Empty)
        {
            pram = pram + " AND TANDC.ATTDate BETWEEN '" + ContractualendDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }

        return pram;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=TrainingAttendanceMarks.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            loadGridView.AllowPaging = false;



            //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;

            this.LoadInfo();

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            loadGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in loadGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in loadGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            loadGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3> Training Attendance	</h3>

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
        LoadInfo();
    }

    public static DataTable GetInversedDataTable(DataTable table, string columnX,params string[] columnsToIgnore)
    {
        //Create a DataTable to Return
        DataTable returnTable = new DataTable();

        if (columnX == "")
            columnX = table.Columns[0].ColumnName;

        //Add a Column at the beginning of the table

        returnTable.Columns.Add(columnX);

        //Read all DISTINCT values from columnX Column in the provided DataTale
        List<string> columnXValues = new List<string>();

        //Creates list of columns to ignore
        List<string> listColumnsToIgnore = new List<string>();
        if (columnsToIgnore.Length > 0)
            listColumnsToIgnore.AddRange(columnsToIgnore);

        if (!listColumnsToIgnore.Contains(columnX))
            listColumnsToIgnore.Add(columnX);

        foreach (DataRow dr in table.Rows)
        {
            string columnXTemp = dr[columnX].ToString();
            //Verify if the value was already listed
            if (!columnXValues.Contains(columnXTemp))
            {
                //if the value id different from others provided, add to the list of 
                //values and creates a new Column with its value.
                columnXValues.Add(columnXTemp);
                returnTable.Columns.Add(columnXTemp);
            }
            else
            {
                //Throw exception for a repeated value
                throw new Exception("The inversion used must have " +
                                    "unique values for column " + columnX);
            }
        }

        //Add a line for each column of the DataTable

        foreach (DataColumn dc in table.Columns)
        {
            if (!columnXValues.Contains(dc.ColumnName) &&
                !listColumnsToIgnore.Contains(dc.ColumnName))
            {
                DataRow dr = returnTable.NewRow();
                dr[0] = dc.ColumnName;
                returnTable.Rows.Add(dr);
            }
        }

        //Complete the datatable with the values
        for (int i = 0; i < returnTable.Rows.Count; i++)
        {
            for (int j = 1; j < returnTable.Columns.Count; j++)
            {
                returnTable.Rows[i][j] =
                  table.Rows[j - 1][returnTable.Rows[i][0].ToString()].ToString();
            }
        }

        return returnTable;
    }
}