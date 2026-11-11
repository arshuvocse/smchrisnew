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
using DAL.MasterSetup_DAL;
using DAL.Report_DAL;
using DAL.UserPermissions_DAL;
using DAO.UA_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MenuSetup_SystemLoginLog : System.Web.UI.Page
{
    SupervisorMenuAppDAL appDal=new SupervisorMenuAppDAL();
    CommonDataLoadDAL aDataLoadDal=new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFrmDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            txtFrmDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            DropDownList();
        }
    }

    public void DropDownList()
    {
        aDataLoadDal.CompanyDropDown(companyDropDownList," ");
        companyDropDownList.SelectedIndex = 1;
       
        aDataLoadDal.MenuDropDown(menuDropDownList);
        aEmployeeRequsitionDal.GetEmpCategoryDDL(ddlEmpCategoryEx);
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
  

  

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

  

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void deptDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    SeparationReportDal aReportDal = new SeparationReportDal();

    protected void menuDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void divDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aDataLoadDal.GetDepartmentByDivList(deptDropDownList,divDropDownList.SelectedValue);
    }

    public string Param()
    {
        string param = " ";
        if (companyDropDownList.SelectedIndex>0)
        {
            param = param + " AND E.CompanyId='" + companyDropDownList.SelectedValue + "' ";
        }
        if (divDropDownList.SelectedIndex>0)
        {
            param = param + " AND E.DivisionId='" + divDropDownList.SelectedValue + "' ";
        }
        if (deptDropDownList.SelectedIndex>0)
        {
            param = param + " AND E.DepartmentId='" + deptDropDownList.SelectedValue + "' ";
        }

        if (ddlEmpCategoryEx.SelectedIndex > 0)
        {
            param = param + " AND  E.EmpCategoryId ='" + ddlEmpCategoryEx.SelectedValue + "' ";
        }

        if (gradeDropDownList.SelectedIndex > 0)
        {
            param = param + " AND  E.SalaryGradeId ='" + gradeDropDownList.SelectedValue + "' ";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            param = param + "  and  ( e.EmpInfoId=" + ddlEmpInfo.SelectedValue.Trim() + ")";
        }

        if (txtFrmDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            param = param + " AND convert(Date,lk.LoginTime) BETWEEN '" + txtFrmDate.Text + "' AND '" + txtToDate.Text + "' ";
        }
        if (txtFrmDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            param = param + " AND convert(Date,lk.LoginTime) BETWEEN '" + txtFrmDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        return param;
    }


    public string Param2()
    {
        string param = " ";
         
        if (divDropDownList.SelectedIndex > 0)
        {
            param = param + " AND E.DivisionId='" + divDropDownList.SelectedValue + "' ";
        }
        if (deptDropDownList.SelectedIndex > 0)
        {
            param = param + " AND E.DepartmentId='" + deptDropDownList.SelectedValue + "' ";
        }

        if (ddlEmpCategoryEx.SelectedIndex > 0)
        {
            param = param + " AND  E.EmpCategoryId ='" + ddlEmpCategoryEx.SelectedValue + "' ";
        }

        if (gradeDropDownList.SelectedIndex > 0)
        {
            param = param + " AND  E.SalaryGradeId ='" + gradeDropDownList.SelectedValue + "' ";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            param = param + "  and  ( e.EmpInfoId=" + ddlEmpInfo.SelectedValue.Trim() + ")";
        }

        return param;
    }
        
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            DataTable dtdata = aReportDal.LoadSystemLogDAL(Param());
            if (dtdata.Rows.Count > 0)
            {
                GridView1.DataSource = dtdata;
                GridView1.DataBind();

                
            }
            else
            {
                AlertMessageBoxShow("No Data Found");
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
       
        }
        else
        {
              AlertMessageBoxShow("Please Select Company Name");
        }
        
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            string attachment = "attachment; filename=System_LoginLoglList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);






            GridView1.AllowPaging = false;
           
            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            GridView1.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            GridView1.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in GridView1.HeaderRow.Cells)
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


            GridView1.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + companyDropDownList.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3> Supervisor Approval  List	</h3>

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
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);
    }
    EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();
    protected void ddlEmpCategoryEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategoryEx.SelectedIndex > 0)
        {




            aEmployeeRequsitionDal.LoadGradeByCatID(gradeDropDownList, ddlEmpCategoryEx.SelectedValue);
        }
        else
        {
            gradeDropDownList.SelectedValue = null;
        }
    }

   
}