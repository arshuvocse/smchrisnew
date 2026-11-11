using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Permission_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using HELPER_FUNCTIONS.HELPERS;

public partial class SuspendAndDiciplinary_UI_DiciplinaryActionView : System.Web.UI.Page
{

    DiciplinaryActionDal actionDal = new DiciplinaryActionDal();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            GetCompany();
           UserPersmissionValidation();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
        }
    }
    private void LoadDropDownList()
    {
        actionDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        //actionDal.EmployeeTypeList(typeDropDownList);
    }

    public void GetCompany()
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

    private void LoadDiciplenaryActionView()
    {
        DataTable aTable = new DataTable();

        aTable = actionDal.GetDiciplinaryActionInfo(" and DCPA.CompanyInfoId IN (" + CompanyId() + ")" + GenerateParamiterList());

        if ( aTable.Rows.Count>0)
        {
            loadGridView.DataSource = aTable;
            loadGridView.DataBind();
         }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }
    private string GenerateParamiterList()
    {

        string parameter = "   ";

         if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND DCPA.CompanyInfoId = " + ddlCompany.SelectedValue;
        }





        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND DCPA.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND DCPA.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND DCPA.Effectivedate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }
    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("DiciplinaryAction.aspx");
    }

    protected void reloadButton_OnClick(object sender, EventArgs e)
    {
        LoadDiciplenaryActionView();
    }


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            var datKey = loadGridView.DataKeys[0];
            if (datKey != null)
            {
                string MId = e.CommandArgument.ToString();
              DataTable aTable =
                            actionDal.DeleteValidattionForEffectiveDate(MId.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {
              
                Session["Status"] = "Edit";
                Session["MId"] = "";
                Session["MId"] = MId;
                Response.Redirect("DiciplinaryAction.aspx");
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                    }
                }
              
            }

          
        }

        if (e.CommandName == "ViewData")
        {
             var datKey = loadGridView.DataKeys[0];
            if (datKey != null)
            {
                string MId = e.CommandArgument.ToString();

                Session["MId"] = "";
                Session["MId"] = MId;
                Session["Status"] = "View";
                Response.Redirect("DiciplinaryAction.aspx");
            }
        }

        if (e.CommandName == "DeleteData")
        {
                var datKey = loadGridView.DataKeys[0];
            if (datKey != null)
            {
                string MId = e.CommandArgument.ToString();

                DataTable aTable =
                    actionDal.DeleteValidattionForEffectiveDate(MId.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {
                        Session["MId"] = "";
                        Session["MId"] = MId;
                        Session["Status"] = "Delete";
                        Response.Redirect("DiciplinaryAction.aspx");
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
                    }
                }
            }

        }

        if (e.CommandName == "Preview")
        {
            var datKey = loadGridView.DataKeys[0];
            if (datKey != null)
            {
                string MId = e.CommandArgument.ToString();

                PopUp(Convert.ToInt32(MId));



            }



        }
    }
    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/DiciplinaryActionReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    protected void SearchButton_OnClick(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
            LoadDiciplenaryActionView();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
        }
        
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Employee_Diciplinary_Info_List.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            loadGridView.AllowPaging = false;




            loadGridView.Columns[1].Visible =
               false;


            loadGridView.Columns[10].Visible =
            false;
            loadGridView.Columns[11].Visible =
             false;
            loadGridView.Columns[12].Visible =
               false;
            LoadDiciplenaryActionView();

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
            string headerTable = @"<span  style='text-align:left'><h3> Company Name: " + ddlCompany.SelectedItem.Text +
                                 "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " +
                                 DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";





            HttpContext.Current.Response.Write(headerTable);



            string style = @"<style> .text { mso-number-format:\@; } </style> ";
            Response.Write(style);
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
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}