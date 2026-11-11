using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Report_DAL;

public partial class Report_Pages_TrainingInformationReport : System.Web.UI.Page
{
    TrainingInformationDal aTrainingDal = new TrainingInformationDal();
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
        if (reportDropDownList.SelectedValue == "Summary")
        {
            gv_participant.DataSource = null;
            gv_participant.DataBind();

           
            if (ddlCompany.SelectedValue != "")
            {
                if (ddlFinancialYear.SelectedValue != "")
                {
                    DataTable dtDetails = aTrainingDal.GetTrainingAttendanceReport(GenerateParameter());

                    if (dtDetails.Rows.Count > 0)
                    {
                        loadGridView.DataSource = dtDetails;
                        loadGridView.DataBind();

                        int TotalParticipant = dtDetails.AsEnumerable().Sum(row => row.Field<int?>("TotalParticipant") == null ? 0 : row.Field<int>("TotalParticipant"));

                        decimal TotalHoure = dtDetails.AsEnumerable().Sum(row => row.Field<decimal?>("TotalHoure") == null ? 0 : row.Field<decimal>("TotalHoure"));
                        decimal TrainingCost = dtDetails.AsEnumerable().Sum(row => row.Field<decimal?>("TrainingCost") == null ? 0 : row.Field<decimal>("TrainingCost"));
                        decimal LogisticCost = dtDetails.AsEnumerable().Sum(row => row.Field<decimal?>("LogisticCost") == null ? 0 : row.Field<decimal>("LogisticCost"));
                        decimal OtherCost = dtDetails.AsEnumerable().Sum(row => row.Field<decimal?>("OtherCost") == null ? 0 : row.Field<decimal>("OtherCost"));

                        decimal GrandTotal = dtDetails.AsEnumerable().Sum(row => row.Field<decimal?>("GrandTotal") == null ? 0 : row.Field<decimal>("GrandTotal"));
                        loadGridView.FooterRow.Cells[3].Text = "Total: ";
                        loadGridView.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                        loadGridView.FooterRow.Cells[4].Text = TotalParticipant.ToString();
                        loadGridView.FooterRow.Cells[5].Text = TotalHoure.ToString("N2");
                        loadGridView.FooterRow.Cells[6].Text = TrainingCost.ToString("N2");
                        loadGridView.FooterRow.Cells[7].Text = LogisticCost.ToString("N2");
                        loadGridView.FooterRow.Cells[8].Text = OtherCost.ToString("N2");
                        loadGridView.FooterRow.Cells[9].Text = GrandTotal.ToString("N2");

                        //loadGridView.FooterRow.Cells[0].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[1].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[2].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[3].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[4].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[5].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[6].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[7].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[8].BackColor = ColorTranslator.FromHtml("#CCE5FF");
                        //loadGridView.FooterRow.Cells[9].BackColor = ColorTranslator.FromHtml("#CCE5FF");




                    }
                    else
                    {
                        loadGridView.DataSource = null;
                        loadGridView.DataBind();
                    }
                }
                else
                {
                    showMessageBox("Please Select financial year!!");
                }
            }
            else
            {
                showMessageBox("Please Select a Company!!");
            }
        }


        if (reportDropDownList.SelectedValue == "Details")
        {
            

            loadGridView.DataSource = null;
            loadGridView.DataBind();
            if (ddlCompany.SelectedValue != "")
            {
                if (ddlFinancialYear.SelectedValue != "")
                {
                    DataTable dtDetails = aTrainingDal.GetTrainingDetailsReport(GenerateParameter());

                    if (dtDetails.Rows.Count > 0)
                    {
                        gv_participant.DataSource = dtDetails;
                        gv_participant.DataBind();

                        GetSingleName();

                    }
                    else
                    {
                        gv_participant.DataSource = null;
                        gv_participant.DataBind();
                    }
                }
                else
                {
                    showMessageBox("Please Select financial year!!");
                }
            }
            else
            {
                showMessageBox("Please Select a Company!!");
            }
        }

    }
    public void GetSingleName()
    {
        if (gv_participant.Rows.Count > 0)
        {


            string masterText = gv_participant.Rows[0].Cells[1].Text;
            for (int i = 1; i < gv_participant.Rows.Count; i++)
            {
                if (masterText.Trim() ==  gv_participant.Rows[i].Cells[1].Text.Trim())
                {
                    gv_participant.Rows[i].Cells[1].Text = "";
                }
                else
                {
                    masterText = gv_participant.Rows[i].Cells[1].Text.Trim();
                }
            }



            string DateText = gv_participant.Rows[0].Cells[2].Text;
            for (int i = 1; i < gv_participant.Rows.Count; i++)
            {
                if (DateText.Trim() == gv_participant.Rows[i].Cells[2].Text.Trim())
                {
                    gv_participant.Rows[i].Cells[2].Text = "";
                }
                else
                {
                    DateText = gv_participant.Rows[i].Cells[2].Text.Trim();
                }
            }


            string DurationText = gv_participant.Rows[0].Cells[7].Text;
            for (int i = 1; i < gv_participant.Rows.Count; i++)
            {
                if (DurationText.Trim() == gv_participant.Rows[i].Cells[7].Text.Trim())
                {
                    gv_participant.Rows[i].Cells[7].Text = "";
                }
                else
                {
                    DurationText = gv_participant.Rows[i].Cells[7].Text.Trim();
                }
            }





            string InstituteText = gv_participant.Rows[0].Cells[9].Text;
            for (int i = 1; i < gv_participant.Rows.Count; i++)
            {
                if (InstituteText.Trim() == gv_participant.Rows[i].Cells[9].Text.Trim())
                {
                    gv_participant.Rows[i].Cells[9].Text = "";
                }
                else
                {
                    InstituteText = gv_participant.Rows[i].Cells[9].Text.Trim();
                }
            }




            string CsotText = gv_participant.Rows[0].Cells[11].Text;
            for (int i = 1; i < gv_participant.Rows.Count; i++)
            {
                if (CsotText.Trim() == gv_participant.Rows[i].Cells[11].Text.Trim())
                {
                    gv_participant.Rows[i].Cells[11].Text = "";
                }
                else
                {
                    CsotText = gv_participant.Rows[i].Cells[11].Text.Trim();
                }
            }
        }
    }

    private String GenerateParameter()
    {
        string pram = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            pram = pram + " and TRM.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlFinancialYear.SelectedValue != "")
        {
            pram = pram + " AND TRM.FinancialYearId = " + ddlFinancialYear.SelectedValue;
        }

        if (ddlTrainingRecord.SelectedValue != "")
        {
            pram = pram + " AND TRM.TrainingRecordMasterId = " + ddlTrainingRecord.SelectedValue;
        }

     
        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text != string.Empty)
        {
            pram = pram + " AND TRM.EndDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }
        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text == string.Empty)
        {
            pram = pram + " AND TRM.EndDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (ContractualstartDate.Text == string.Empty && ContractualendDate.Text != string.Empty)
        {
            pram = pram + " AND TRM.EndDate BETWEEN '" + ContractualendDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }

        return pram;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=TrainingInformation.xls";
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
            string headerTable = @"<span  style='text-align:left'><h3> Company: " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span  style='text-align:left'><h3>Financial Year: " + ddlFinancialYear.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'> <h3> Training Information Report </h3> </span>";

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

    protected void reportDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        gv_participant.DataSource = null;
        gv_participant.DataBind();

        loadGridView.DataSource = null;
        loadGridView.DataBind();
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
    protected void btnExportToExcel2_Click(object sender, EventArgs e)
    {
        if (gv_participant.Rows.Count > 0)
        {
            // Clear all content output from the buffer stream
            Response.ClearContent();
            // Specify the default file name using "content-disposition" RESPONSE header
            Response.AppendHeader("content-disposition", "attachment; filename=Training_Information_Report.xls");
            // Set excel as the HTTP MIME type
            Response.ContentType = "application/excel";
            // Create an instance of stringWriter for writing information to a string
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            // Create an instance of HtmlTextWriter class for writing markup 
            // characters and text to an ASP.NET server control output stream







            gv_participant.AllowPaging = false;
            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);

            // Set white color as the background color for gridview header row
            gv_participant.HeaderRow.Style.Add("background-color", "#FFFFFF");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_participant.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_participant.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                }
            }
            string headerTable = @"<span  style='text-align:center'><h3> " + ddlCompany.SelectedItem.Text +" (FY: " +ddlFinancialYear.SelectedItem.Text +")</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
               <h3>Training Information Report	</h3>
            
            </span>";
            gv_participant.RenderControl(htw);
            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(stringWriter.ToString());
            Response.End();


        }
        else if (loadGridView.Rows.Count > 0)
        {
            // Clear all content output from the buffer stream
            Response.ClearContent();
            // Specify the default file name using "content-disposition" RESPONSE header
            Response.AppendHeader("content-disposition", "attachment; filename=Training_Summary_Report.xls");
            // Set excel as the HTTP MIME type
            Response.ContentType = "application/excel";
            // Create an instance of stringWriter for writing information to a string
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            // Create an instance of HtmlTextWriter class for writing markup 
            // characters and text to an ASP.NET server control output stream







            loadGridView.AllowPaging = false;
            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);

            // Set white color as the background color for gridview header row
            loadGridView.HeaderRow.Style.Add("background-color", "#FFFFFF");

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
            string headerTable = @"<span  style='text-align:center'><h3> " + ddlCompany.SelectedItem.Text + " (FY: " + ddlFinancialYear.SelectedItem.Text + ")</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
               <h3>Training Summary Report	</h3>
            
            </span>";
            loadGridView.RenderControl(htw);
            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(stringWriter.ToString());
            Response.End();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }
}