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
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_SalaryMatrixReport : System.Web.UI.Page
{
    SalaryMatrixReportDAL aVaencyEntryDaL = new SalaryMatrixReportDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            this.GridView1.ShowFooter = true;
            this.GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;  //This adds the <tfoot> element. Remove if you don't have a footer row12    
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {

            //throw;
        }
        if (!IsPostBack)
        {
            GridView3.Visible = false;
            GridView3.DataSource = new List<Item>
        {
            new Item {Text1 = "Graded"}
        };
            GridView3.DataBind();

            GridView4.Visible = false;
            GridView4.DataSource = new List<Item>
        {
            new Item {Text1 = "Management"}
        };
            GridView4.DataBind();
             
            LoadRegionInformation();

       //     OnRowDataBound(null, null);
        }
    }
    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }
 
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


           

         //  for (int kk = 0; kk < GridView1.Rows.Count; kk++)
            {
               
               // string group = GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();
            
                //string group = GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();
                DataTable dtStep = aVaencyEntryDaL.GetStepInfo(Convert.ToInt32(1.ToString()));
            
                for (int i = 0; i < dtStep.Rows.Count; i++)
                {
                    try
                    {
                        e.Row.Cells[2 + i].Text = dtStep.Rows[i]["SalaryStepName"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }


                }

            }
        }
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {




            int id = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Values[0]);

            DataTable dtStep = aVaencyEntryDaL.GetStepInfo(Convert.ToInt32(id));

                for (int i = 0; i < dtStep.Rows.Count; i++)
                {
                    try
                    {
                        e.Row.Cells[2 + i].Text = dtStep.Rows[i]["GrossAmount"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
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
        DataTable dataTable = aVaencyEntryDaL.GetVacanceyEntryformationParam();

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            
        }


       
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
              //  string areaId = dataKey[0].ToString();
                DataTable dtSG = aVaencyEntryDaL.GetSgInfoManagement();

                if (dtSG.Rows.Count > 0)
                {
                    GridView1.DataSource = dtSG;
                    GridView1.DataBind();




                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }


                DataTable dtSG2 = aVaencyEntryDaL.GetSgInfoGraded();

                if (dtSG2.Rows.Count > 0)
                {
                    GridView2.DataSource = dtSG2;
                    GridView2.DataBind();




                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }

             
        }

       
    }

    private void PopUp1(Int32 EmployeePromotion)
    {
        string url = "../Report_UI/SalaryMatrixViewer.aspx?rptType=" + EmployeePromotion;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("AchievementsEntry.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        const int manuId = 12;
        DataTable gridPermission = aOperationCredentials.MnageUserOperationOnGridView(Session["UserId"].ToString(), manuId);
        const int rowIndex = 0;

        bool edit = false;
        bool delete = false;

        if (gridPermission.Rows.Count > 0)
        {
            edit = gridPermission.Rows[rowIndex].Field<bool>("Edit");
            delete = gridPermission.Rows[rowIndex].Field<bool>("Delete");
        }

        if (edit)
        {
            loadGridView.Columns[12].Visible = true;
        }

        if (delete)
        {
            loadGridView.Columns[13].Visible = true;
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void OnRowDataBoundGrad(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {



            //  for (int kk = 0; kk < GridView1.Rows.Count; kk++)
            {

                // string group = GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();

                //string group = GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();
                DataTable dtStep = aVaencyEntryDaL.GetStepInfo(Convert.ToInt32(1.ToString()));

                for (int i = 0; i < dtStep.Rows.Count; i++)
                {
                    try
                    {
                        e.Row.Cells[2 + i].Text = dtStep.Rows[i]["SalaryStepName"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }


                }

            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {




            int id = Convert.ToInt32(GridView2.DataKeys[e.Row.RowIndex].Values[0]);

            DataTable dtStep = aVaencyEntryDaL.GetStepInfoGrade(Convert.ToInt32(id));

            for (int i = 0; i < dtStep.Rows.Count; i++)
            {
                try
                {
                    e.Row.Cells[2 + i].Text = dtStep.Rows[i]["GrossAmount"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }
            }

        }
    }
    public class Item
    {
        public string Text1 { get; set; }
        public string Text2 { get; set; }
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            GridView3.Visible = true;
            GridView4.Visible = true;

            string attachment = "attachment; filename=Salary_Matrix_Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            StringWriter sw2 = new StringWriter();
            HtmlTextWriter htw2 = new HtmlTextWriter(sw2);

         

 

      

             
            HtmlForm frm = new HtmlForm();
            GridView4.Parent.Controls.Add(frm);

            GridView1.Parent.Controls.Add(frm);
            GridView2.Parent.Controls.Add(frm);
            GridView3.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            GridView1.HeaderRow.Style.Add("background-color", "#E5EEF1");


            GridView2.HeaderRow.Style.Add("background-color", "#E5EEF1");

           
            foreach (TableCell tableCell in GridView1.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            foreach (TableCell tableCell in GridView2.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }


            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in GridView1.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            foreach (GridViewRow gridViewRow in GridView2.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }

            foreach (GridViewRow gridViewRow in GridView3.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#92a8d1";


                    gridViewRowTableCell.RowSpan = 2;


                    gridViewRowTableCell.Font.Size = FontUnit.Large;


                }
            }



            foreach (GridViewRow gridViewRow in GridView4.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#92a8d1";

                    gridViewRowTableCell.RowSpan=2;
                    gridViewRowTableCell.Font.Size = FontUnit.Large;






                }
            }
            GridView4.RenderControl(htw);

            GridView1.RenderControl(htw);
            GridView3.RenderControl(htw2);
            
            GridView2.RenderControl(htw2);
            string headerTable = @"<span  style='text-align:left'><h3> Social Marketing Company " + "</h3>  </span>    " + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

        

            HttpContext.Current.Response.Write(headerTable);
          
            Response.Write(sw.ToString());
            Response.Write(sw2.ToString());
            Response.End();
            GridView3.Visible = false;
            GridView4.Visible = false;

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

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        var gridView = (GridView)sender;
        var header = (GridViewRow)gridView.Controls[0].Controls[0];

       
        header.Cells[0].ColumnSpan = 12;
        
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "Customers";
        cell.ColumnSpan = 2;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Employees";
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }
}