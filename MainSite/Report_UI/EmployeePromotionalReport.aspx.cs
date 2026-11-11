using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAL.Report_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_UI_EmployeePromotionalReport : System.Web.UI.Page
{

    EmployeeProfileDAL aReportDal = new EmployeeProfileDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    DataSet ds = new DataSet();  
    StringBuilder htmlTable = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
         
         
        if (!IsPostBack)
        {
            DropDown();

           
        }

      
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {

    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    //protected void cblHeader_OnSelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    public void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);

        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);
         
    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
        
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        SearchEmployeeNameTextBoxInProfile1.Text = "";
        repEmpIdHiddenField.Value = "";
        if (ddlCompany.SelectedValue != "")
        {
            //Session["CompanyId"] = ddlCompany.SelectedValue;

            //using (DataTable dt = _commonDataLoad.GetEmpDDL(ddlCompany.SelectedValue))
            //{
            //    allocationNoDropdownlist.DataSource = dt;
            //    allocationNoDropdownlist.DataValueField = "EmpInfoId";
            //    allocationNoDropdownlist.DataTextField = "EmpName";
            //    allocationNoDropdownlist.DataBind();


                
            //}


            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
    }

    public void LoadData()
    {

        


    }


    public string Parameter()
    {
        string param = "";
        try
        {
            if (SearchEmployeeNameTextBoxInProfile1.Text != " ")
            {
                param = param + " tblEmpGeneralInfo.EmpInfoId ='" + repEmpIdHiddenField.Value + "' ";
            }


        }
        catch (Exception)
        {


        }


        return param;
    }
    //
   
    //

    public void GridBind()
    {


        DataTable dtdata = aReportDal.GetEmployeeInfoDAL(Parameter());
          
     

        string html = "<table class='table table-bordered text-center thead-dark table-hover table-striped' >";
        html = html + "<thead>";
        //for (int i = 0; i < dtdata.Columns.Count; i++)
        //{
        //    html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";
        //}




     
        html = html+ "</thead>";

        html = html + "<tbody>";

        for (int i = 0; i < dtdata.Rows.Count; i++)
        {
            html = html + "<tr>";
           
                html = html + "<td>" + dtdata.Rows[Convert.ToInt32("EmpInfoId")].ToString() + "</td>";
            

            html = html + "</tr>";
          
        }
      

        html = html + "</tbody>";
        html = html + "</table>";
        data.InnerHtml = html;


    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        if (validat())
        {

            Session["BI"] = "true";

            Session["PI"] = string.Empty;

            string head = "";
            if (cblHeader.Items[0].Selected)
            {
                  head = cblHeader.Items[0].Text;
                
            }
            Session["PI"] = cblHeader.Items[0].Selected;
            


            Session["UGI"] = string.Empty;
            if (cblHeader.Items[1].Selected)
            {
                head = cblHeader.Items[1].Text;

            }

            Session["UGI"] = cblHeader.Items[1].Selected;


            Session["SP"] = string.Empty;

            if (cblHeader.Items[2].Selected)
            {
                head = cblHeader.Items[2].Text;

            }





            Session["SP"] = cblHeader.Items[2].Selected;









            PopUp(Convert.ToInt32(ddlEmpInfo.SelectedValue.Trim()), head);
            
        }
    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool validat()
    {
        if (ddlCompany.SelectedValue == string.Empty)
        {
            ShowMessageBox("Please Select Company Name");
            ddlCompany.Focus();
            return false;
        }

        if (ddlEmpInfo.SelectedValue == string.Empty)
        {
            ShowMessageBox("Employee Name Can not be Empty!!!");
            ddlEmpInfo.Focus();
            return false;
        }
        if (cblHeader.SelectedValue == string.Empty)
        {
            ShowMessageBox("Please Select Name of Information");
            cblHeader.Focus();
            return false;
        }


        return true;
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeePromotionalReport.aspx");
    }

    private void PopUp(Int32 EmpInfoId, string header)
    {
        string url = "../Report_UI/EmployeePromotionalReportViwer.aspx?rptType=" + EmpInfoId+"&header=" + header;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {


        string empName = SearchEmployeeNameTextBoxInProfile1.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            SearchEmployeeNameTextBoxInProfile1.Text = emp[2];
            repEmpIdHiddenField.Value = emp[0];

            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();

        }
        else
        {

            SearchEmployeeNameTextBoxInProfile1.Text = "";
            repEmpIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }



        //if (ddlCompany.SelectedValue != "")
        //{
        //    string empName = SearchEmployeeNameTextBoxInProfile1.Text.Trim();

        //    if (empName.Contains(':'))
        //    {
        //        string[] emp = empName.Split(':');

        //        SearchEmployeeNameTextBoxInProfile1.Text = emp[2];
        //        repEmpIdHiddenField.Value = emp[0];

        //        //productNameTextBox.Text = productInfo[1];
        //        //string productCode = productCodeTextBox.Text.Trim();

        //    }
        //    else
        //    {

        //        SearchEmployeeNameTextBoxInProfile1.Text = "";
        //        repEmpIdHiddenField.Value = "";
        //        aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //    }

        //}
        //else
        //{
        //    aShowMessage.ShowMessageBox("Please Select a Company !!", this);
        //    SearchEmployeeNameTextBoxInProfile1.Text = "";
        //    repEmpIdHiddenField.Value = "";
        //    ddlCompany.Focus();
        //}



    }

    //private void LoadInitialGrid(GridView gridView)
    //{
    //    try
    //    {

    //        DataTable Dt = aReportDal.GetEmployeeProfile(Parameter());

    //        if (Dt.Rows.Count > 0)
    //               {
    //                   gridView.DataSource = Dt;

    //                   gridView.DataBind();
    //                   itemsGridView.UseAccessibleHeader = true;
    //                  // itemGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
    //                   itemsGridView.FooterRow.TableSection = TableRowSection.TableFooter;
    //                   itemsGridView.UseAccessibleHeader = true;
    //        }

          

    //    }
    //    catch (Exception)
    //    {
    //        gridView.DataSource = null;
    //        gridView.DataBind();
    //        //throw;
    //    }
    //}


    //protected void itemsGridView_RowCommand(object sender, GridViewPageEventArgs e)
    //{
    //    itemsGridView.PageIndex = e.NewPageIndex;
    //    LoadInitialGrid(itemsGridView);
    //}

    protected void loadGridView_PageIndexChanging(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static List<string> GetAutoCompleteData(string username)
    {
        List<string> result = new List<string>();
        using (SqlConnection con = new SqlConnection(@"Data Source=172.100.1.17\SQLSERVER2014;Initial Catalog=HRIS_SMC_DB;Integrated Security=false;  User ID=sa; Password=SMC**10;"))
        {
            using (SqlCommand cmd = new SqlCommand(@"SELECT Top 10 CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' +  ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE  
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + ISNULL(d.Designation,'') + ' ' + ISNULL(dept.DepartmentName, '') ) LIKE '%'+ @SearchText +'%'", con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchText", username);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["EmpName"].ToString());
                }
                return result;
            }
        }
    }


    [WebMethod]
    public static string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRIS_SMC"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT Top 10 CAST(e.EmpInfoId AS NVARCHAR(50)) + ' : ' +  ISNULL(e.EmpMasterCode,'')  + ' : ' + e.EmpName + ' : ' +  ISNULL(d.Designation,'')  + ' : ' + ISNULL(dept.DepartmentName, '')  AS EmpName,  e.EmpInfoId
FROM dbo.tblEmpGeneralInfo e  WITH (nolock)
left JOIN dbo.tblDesignation d ON d.DesignationId = e.DesignationId
left JOIN dbo.tblDepartment dept ON dept.DepartmentId = e.DepartmentId
WHERE  
(CAST(e.EmpInfoId AS NVARCHAR(50)) + ' ' + e.EmpMasterCode + ' ' + e.EmpName + ' ' + ISNULL(d.Designation,'') + ' ' + ISNULL(dept.DepartmentName, '') ) LIKE '%'+ @SearchText +'%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["EmpName"], sdr["EmpInfoId"]));
                    }
                }
                conn.Close();
            }
        }
        return customers.ToArray();
    }

    protected void SSGradeCheck_OnCheckedChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < cblHeader.Items.Count; i++)
        {
            if (SSGradeCheck.Checked)
            {
                cblHeader.Items[i].Selected = true;
            }
            else
            {
                cblHeader.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void cblHeader_OnTextChanged(object sender, EventArgs e)
    {
       
    }
}