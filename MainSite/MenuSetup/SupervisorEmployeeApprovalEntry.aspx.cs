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
using DAL.UserPermissions_DAL;
using DAO.UA_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MenuSetup_SupervisorApprovalEntry : System.Web.UI.Page
{
    SupervisorMenuAppDAL appDal=new SupervisorMenuAppDAL();
    CommonDataLoadDAL aDataLoadDal=new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList();
        }
    }

    public void DropDownList()
    {
        aDataLoadDal.CompanyDropDown(companyDropDownList," ");
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
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
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                if (chkSingle.Visible)
                {
                    chkSingle.Checked = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    
    }

    public void LoadEmp()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            appDal.LoadEmployeeDrop((DropDownList)loadGridView.Rows[i].FindControl("employeeDropDownList"), loadGridView.DataKeys[i]["DepartmentId"].ToString());
            ((DropDownList) loadGridView.Rows[i].FindControl("employeeDropDownList")).SelectedValue =
                loadGridView.DataKeys[i]["EmpInfoId"].ToString();
            CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
            chkSingle.Checked = Convert.ToBoolean(loadGridView.DataKeys[i]["IsChecked"].ToString());

        }
    }

    public void Save()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            try
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                if (chkSingle.Checked)
                {
                    SupervisorMenuAppDAO appDao = new SupervisorMenuAppDAO();
                    {
                        appDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                        appDao.EmpInfoId =
                            Convert.ToInt32(
                                ((DropDownList) loadGridView.Rows[i].FindControl("employeeDropDownList")).SelectedValue);
                        //appDao.MainMenuId = Convert.ToInt32(loadGridView.DataKeys[i]["MainMenuId"].ToString());
                        appDao.SuperMenuAppId =
                            string.IsNullOrEmpty(loadGridView.DataKeys[i]["SuperMenuAppId"].ToString())
                                ? 0
                                : Convert.ToInt32(loadGridView.DataKeys[i]["SuperMenuAppId"].ToString());
                        appDao.FromEmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i]["FromEmpInfoId"].ToString());

                        CheckBox chkIsAllEmployee = (CheckBox)loadGridView.Rows[i].FindControl("chkIsAllEmployee");

                        appDao.IsAllEmployee = chkIsAllEmployee.Checked;

                    }
                    int id = appDal.SaveSupervisorApp(appDao);
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Saved Successfully...');window.location ='SupervisorEmployeeApprovalEntry.aspx';",
                   true);
        
    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(companyDropDownList.SelectedValue.ToString()))
        {



            ddlEmpInfo.DataSource = dt;
            ddlEmpInfo.DataValueField = "EmpInfoId";
            ddlEmpInfo.DataTextField = "EmpName";
            ddlEmpInfo.DataBind();
            ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            ddlEmpInfo.SelectedIndex = 0;

        }
        //aDataLoadDal.DeptByCompanyDropDown(deptDropDownList,companyDropDownList.SelectedValue);
        menuDropDownList.SelectedIndex = 0;
        loadGridView.DataSource = null;
        loadGridView.DataBind();

        DataTable dtdiv = aDataLoadDal.GetDDLComDivision(companyDropDownList.SelectedValue);
        divDropDownList.DataValueField = "Value";
        divDropDownList.DataTextField = "TextField";
        divDropDownList.DataSource = dtdiv;
        divDropDownList.DataBind();
    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        Save();
    }

    protected void deptDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void menuDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dtdata = appDal.GetData(companyDropDownList.SelectedValue,menuDropDownList.SelectedValue);
         if (dtdata.Rows.Count>0)
        {
              loadGridView.DataSource = dtdata;
        loadGridView.DataBind();
        LoadEmp();
        }
        else
         {
             loadGridView.DataSource = null;
             loadGridView.DataBind();
         }
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


            if (ddlEmpInfo.SelectedValue != "")
            {
                DataTable dtdata = appDal.LoadEmpGenInfoNeee(Param(), Param2());
                if (dtdata.Rows.Count > 0)
                {
                    loadGridView.DataSource = dtdata;
                    loadGridView.DataBind();

                    GridView1.DataSource = dtdata;
                    GridView1.DataBind();
                    for (int i = 0; i < loadGridView.Rows.Count; i++)
                    {
                        DropDownList employeeDropDownList = (DropDownList)loadGridView.Rows[i].FindControl("employeeDropDownList");
                        Label lblEmpAppPath = (Label)loadGridView.Rows[i].FindControl("lblEmpAppPath");
                        CheckBox chkIsAllEmployee = (CheckBox)loadGridView.Rows[i].FindControl("chkIsAllEmployee");

                        if (chkIsAllEmployee.Checked)
                        {
                            using (DataTable dt = _commonDataLoad.GetEmpDDLForEntryByGrade())
                            {



                                employeeDropDownList.DataSource = dt;
                                employeeDropDownList.DataValueField = "EmpInfoId";
                                employeeDropDownList.DataTextField = "EmpName";
                                employeeDropDownList.DataBind();
                                employeeDropDownList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                                employeeDropDownList.SelectedIndex = 0;
                                try
                                {
                                    employeeDropDownList.SelectedValue =
                                          loadGridView.DataKeys[i]["EmpInfoId"].ToString();
                                }
                                catch (Exception)
                                {
                                    employeeDropDownList.SelectedIndex = 0;
                                    //throw;
                                }
                            }
                        }
                        else
                        {
                            DataTable aDataTable = new DataTable();
                            aDataTable.Columns.Add("EmpInfoId");
                            aDataTable.Columns.Add("EmpName");
                            aDataTable.Columns.Add("EmpMasterCode");
                            DataRow dataRow = null;
                            dataRow = aDataTable.NewRow();
                            dataRow["EmpInfoId"] = "0";
                            dataRow["EmpName"] = "Please Select an Employee.....";
                            dataRow["EmpMasterCode"] = "";
                            aDataTable.Rows.Add(dataRow);
                            appDal.ReportingEmpData(loadGridView.DataKeys[i]["FromEmpInfoId"].ToString(), aDataTable);

                            employeeDropDownList.DataValueField = "EmpInfoId";
                            employeeDropDownList.DataTextField = "EmpName";
                            employeeDropDownList.DataSource = aDataTable;
                            employeeDropDownList.DataBind();
                            try
                            {
                                employeeDropDownList.SelectedValue =
                                      loadGridView.DataKeys[i]["EmpInfoId"].ToString();
                            }
                            catch (Exception)
                            {
                                employeeDropDownList.SelectedIndex = 0;
                                //throw;
                            }


                            if (employeeDropDownList.SelectedValue != "")
                            {
                                if (employeeDropDownList.SelectedValue != "0")
                                {
                                    int vvvv = 0;
                                    for (int j = 0; j < aDataTable.Rows.Count; j++)
                                    {
                                        if (aDataTable.Rows[j]["EmpInfoId"].ToString() != "0")
                                        {

                                            vvvv = vvvv + 1;

                                            lblEmpAppPath.Text = lblEmpAppPath.Text + (vvvv) + ".  " + aDataTable.Rows[j]["EmpName"].ToString() + "<br> <br>";
                                            if (employeeDropDownList.SelectedValue == aDataTable.Rows[j]["EmpInfoId"].ToString())
                                            {
                                                break;
                                            }
                                        }

                                    }

                                }
                            }



                        }
                        CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                       // chkSingle.Checked = Convert.ToBoolean(loadGridView.DataKeys[i]["IsChecked"].ToString());
                    }
                }
                else
                {
                    AlertMessageBoxShow("No Data Found");
                    loadGridView.DataSource = null;
                    loadGridView.DataBind();
                }

            }


            else if (companyDropDownList.SelectedIndex > 0)
        {
            DataTable dtdata = appDal.LoadEmpGenInfoNeee(Param(), Param2());
            if (dtdata.Rows.Count > 0)
            {
                loadGridView.DataSource = dtdata;
                loadGridView.DataBind();

                GridView1.DataSource = dtdata;
                GridView1.DataBind();
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    DropDownList employeeDropDownList = (DropDownList)loadGridView.Rows[i].FindControl("employeeDropDownList");
                    Label lblEmpAppPath = (Label)loadGridView.Rows[i].FindControl("lblEmpAppPath");
                    CheckBox chkIsAllEmployee = (CheckBox)loadGridView.Rows[i].FindControl("chkIsAllEmployee");

                    if (chkIsAllEmployee.Checked)
                    {
                        using (DataTable dt = _commonDataLoad.GetEmpDDLForEntryByGrade())
                        {



                            employeeDropDownList.DataSource = dt;
                            employeeDropDownList.DataValueField = "EmpInfoId";
                            employeeDropDownList.DataTextField = "EmpName";
                            employeeDropDownList.DataBind();
                            employeeDropDownList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                            employeeDropDownList.SelectedIndex = 0;
                            try
                            {
                                employeeDropDownList.SelectedValue =
                                      loadGridView.DataKeys[i]["EmpInfoId"].ToString();
                            }
                            catch (Exception)
                            {
                                employeeDropDownList.SelectedIndex = 0;
                                //throw;
                            }
                        }
                    }
                    else
                    {
                        DataTable aDataTable = new DataTable();
                        aDataTable.Columns.Add("EmpInfoId");
                        aDataTable.Columns.Add("EmpName");
                        aDataTable.Columns.Add("EmpMasterCode");
                        DataRow dataRow = null;
                        dataRow = aDataTable.NewRow();
                        dataRow["EmpInfoId"] = "0";
                        dataRow["EmpName"] = "Please Select an Employee.....";
                        dataRow["EmpMasterCode"] = "";
                        aDataTable.Rows.Add(dataRow);
                        appDal.ReportingEmpData(loadGridView.DataKeys[i]["FromEmpInfoId"].ToString(), aDataTable);

                        employeeDropDownList.DataValueField = "EmpInfoId";
                        employeeDropDownList.DataTextField = "EmpName";
                        employeeDropDownList.DataSource = aDataTable;
                        employeeDropDownList.DataBind();
                        try
                        {
                            employeeDropDownList.SelectedValue =
                                  loadGridView.DataKeys[i]["EmpInfoId"].ToString();
                        }
                        catch (Exception)
                        {
                            employeeDropDownList.SelectedIndex = 0;
                            //throw;
                        }


                        if (employeeDropDownList.SelectedValue != "")
                        {
                            if (employeeDropDownList.SelectedValue != "0")
                        {
                            int vvvv = 0;
                            for (int j = 0; j < aDataTable.Rows.Count; j++)
                            {
                                if (aDataTable.Rows[j]["EmpInfoId"].ToString() != "0")
                                {

                                    vvvv = vvvv + 1;
                                  
                                    lblEmpAppPath.Text = lblEmpAppPath.Text + (vvvv) + ".  " + aDataTable.Rows[j]["EmpName"].ToString() + "<br> <br>"; 
                                    if (employeeDropDownList.SelectedValue == aDataTable.Rows[j]["EmpInfoId"].ToString())
                                    {
                                        break;
                                    }
                                }

                            }

                        }
                        }
                       

                       
                    }
                    CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                   // chkSingle.Checked = Convert.ToBoolean(loadGridView.DataKeys[i]["IsChecked"].ToString());
                }
            }
            else
            {
                AlertMessageBoxShow("No Data Found");
                loadGridView.DataSource = null;
                loadGridView.DataBind();
            }

        }
             else
             {
                 AlertMessageBoxShow("Please Select Division");
             }
       
        }
        else
        {
              AlertMessageBoxShow("Please Select Company");
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
        //  if (GridView1.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Supervisor_ApprovalList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);


            loadGridView.Columns[0].Visible = false;
            loadGridView.Columns[7].Visible = false;
            loadGridView.Columns[6].Visible = false;
    

            //foreach (GridViewRow gridViewRow in loadGridView.Rows)
            //{
            //   // gridViewRow.Cells[0].Visible = false;
            //    loadGridView.Cells[6].Visible = false;
            //    gridViewRow.Cells[7].Visible = false;

            //}


           int i=

            loadGridView.Rows.Count;
        

            

           loadGridView.AllowPaging = false;
           
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

            //// Set background color of each cell of each data row of GridView1
           


            loadGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + companyDropDownList.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3> Supervisor Approval  List	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        //else
        //{
        //    showMessageBox("No Data Found!!");
        //}
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

    protected void chkIsAllEmployee_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)chk.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        DropDownList employeeDropDownList = (DropDownList)loadGridView.Rows[rowindex].FindControl("employeeDropDownList");
        CheckBox chkIsAllEmployee = (CheckBox)loadGridView.Rows[rowindex].FindControl("chkIsAllEmployee");

        if (chkIsAllEmployee.Checked)
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLForEntryByGrade())
            {



                employeeDropDownList.DataSource = dt;
                employeeDropDownList.DataValueField = "EmpInfoId";
                employeeDropDownList.DataTextField = "EmpName";
                employeeDropDownList.DataBind();
                employeeDropDownList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                employeeDropDownList.SelectedIndex = 0;
                try
                {
                    employeeDropDownList.SelectedValue =
                          loadGridView.DataKeys[rowindex]["EmpInfoId"].ToString();
                }
                catch (Exception)
                {
                    employeeDropDownList.SelectedIndex = 0;
                    //throw;
                }
            }
        }
        else
        {
            DataTable aDataTable = new DataTable();
            aDataTable.Columns.Add("EmpInfoId");
            aDataTable.Columns.Add("EmpName");
            aDataTable.Columns.Add("EmpMasterCode");
            DataRow dataRow = null;
            dataRow = aDataTable.NewRow();
            dataRow["EmpInfoId"] = "0";
            dataRow["EmpName"] = "Please Select an Employee.....";
            dataRow["EmpMasterCode"] = "";
            aDataTable.Rows.Add(dataRow);
            appDal.ReportingEmpData(loadGridView.DataKeys[rowindex]["FromEmpInfoId"].ToString(), aDataTable);

            employeeDropDownList.DataValueField = "EmpInfoId";
            employeeDropDownList.DataTextField = "EmpName";
            employeeDropDownList.DataSource = aDataTable;
            employeeDropDownList.DataBind();
            try
            {
                employeeDropDownList.SelectedValue =
                      loadGridView.DataKeys[rowindex]["EmpInfoId"].ToString();
            }
            catch (Exception)
            {
                employeeDropDownList.SelectedIndex = 0;
                //throw;
            }
        }
 
    }
}