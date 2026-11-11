using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Lunch_Allowance_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Lunch_Allowance_UI_launchAllawenceReportPage : System.Web.UI.Page
{
    LunchAllowanceCancelDAL allowanceCancelDal=new LunchAllowanceCancelDAL();
    ShowMessage aShowMessage = new ShowMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            effectiveDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            DropDownList();

            try
            {
                GetMonthList(ddlmonth);
                GetYearList(ddlYear);
            }

            catch (Exception ex) { }

        }
    }

    public void DropDownList()
    {
        allowanceCancelDal.LoadCompany(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        empStatusDropDownList_OnSelectedIndexChanged(null, null);

        companyDropDownList_OnSelectedIndexChanged(null, null);

        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";

            ddlDesignation.DataBind();

            ddlDesignation.SelectedValue = "Please Select One..";
        }

        using (DataTable dt = _commonDataLoad.GetDDLComCategory())
        {
            ddlEmpCategory.DataSource = dt;
            ddlEmpCategory.DataValueField = "Value";
            ddlEmpCategory.DataTextField = "TextField";
            ddlEmpCategory.DataBind();
        }
    }



    public void GetYearList(DropDownList ddl)
    {


        int i;

        for (i = 2015; i <= 2050; i++)
        {
            ddl.Items.Add(i.ToString());
            ddl.Items.FindByValue(System.DateTime.Now.Year.ToString());
        }
        string strYear = System.DateTime.Now.Year.ToString();

        ddl.SelectedValue = strYear;


    }
    public void GetMonthList(DropDownList ddl)
    {
        DateTime month = Convert.ToDateTime(DateTime.Now);
        for (int i = 0; i < 12; i++)
        {
            DateTime NextMont = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = NextMont.ToString("MMMM");
            list.Value = NextMont.Month.ToString();
            ddl.Items.Add(list);
        }
        //ddl.Items.Insert(0, "Select Month");
        ddl.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {

        if (validfa())
        {
            DataTable dtDate = _commonDataLoad.GetDatewithinDateRange(effectiveDateTextBox.Text, txtToDate.Text);

            string mainDate = "";
            foreach (DataRow row in dtDate.Rows)
            {
                mainDate = mainDate + row["mainDate"].ToString() + ',';
            }
            string mainDate_ = mainDate.TrimEnd(',');
            string com = "";

            if (companyDropDownList.SelectedValue == "1")
            {
                com = "SOCIAL MARKETING COMPANY";
            }

            else
            {

                com = "SMC ENTERPRISE  LTD.";

            }
            try
            {
              int  RowCount = 0;
              decimal TotalCount = 0;

                string html = "";
                if (ddlEmpCategory.SelectedIndex <= 0)
                {

                    DataTable dtdata = allowanceCancelDal.Get_ReportLunch(GenerateParameter()+
                        " and alw.CategoryId=1", mainDate_);
                    RowCount = RowCount+ dtdata.Rows.Count;
                    html =
                        "<table id='tblTable' style='height:200px;' class='table table-bordered text-center thead-dark table-hover table-striped tableFixHead' >";
                    html = html + "<thead> " + "<tr>" +
                           "<th colspan='47'   ><h3> " + com + "</h3> </th>" +

                           "</tr>" + "<tr>" +
                           "<th colspan='47'   ><h4>  Lunch  Sheet for Date Range " + effectiveDateTextBox.Text +
                           ", " + txtToDate.Text + "</h3> </th>" +

                           "</tr><tr>" +
                           "<th colspan='4'   ><h4>Management " + "</h3> </th>" +

                           "</tr>";
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";
                    }
                    html = html + "<th>" + "Total" + "</th>";

                   

                    html = html + "</tr></thead>";

                    html = html + "<tbody>";

                    for (int i = 0; i < dtdata.Rows.Count; i++)
                    {
                        html = html + "<tr>";
                        int Total = 0;
                        decimal FoodRate = 0;
                        for (int j = 0; j < dtdata.Columns.Count; j++)
                        {

                            if (dtdata.Rows[i][j].ToString()=="0")
                            {
                                html = html + "<td bgcolor='IndianRed'>" + dtdata.Rows[i][j].ToString() + "</td>";
                                
                            }
                            else
                            {
                                
                                    try
                                    {
                                        if (dtdata.Rows[i][j].ToString() == "1")
                                        {
                                            Total = Total + Convert.ToInt32(dtdata.Rows[i][j].ToString()); 
                                        }

                                        try
                                        {
                                            decimal FoodRate22 = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                                            if (FoodRate22 > 30)
                                            {
                                                FoodRate = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            
                                         //   throw;
                                        }
                                           
                                       
                                        
                                    }
                                    catch (Exception)
                                    {
                                        
                                       // throw;
                                    }//
                             
                            
                                html = html + "<td>" + dtdata.Rows[i][j].ToString() + "</td>";
                                
                            }

                           
                        }

                        TotalCount = TotalCount + (Total*FoodRate);
                        html = html + "<td>" + Total * FoodRate + "</td>";
                        html = html + "</tr>";
                    }


                    html = html + "</tbody>";

                    //html = html + "<tfoot>";
                    //for (int i = 0; i < dtdata.Columns.Count; i++)
                    //{
                    //    html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";
                    //}

                    //html = html + "</tfoot>";

                    //DataTable dtdaGraded = allowanceCancelDal.GetLunchAllowEmpReportNew(GenerateParameter(),
                    //    " and mas.CategoryId=2", ddlYear.SelectedValue, ddlmonth.SelectedItem.Text);

                    DataTable dtdaGraded = allowanceCancelDal.Get_ReportLunch(GenerateParameter() +
                     " and alw.CategoryId=2", mainDate_);

                    RowCount = RowCount + dtdaGraded.Rows.Count;
                    html = html + "<thead> " + "<tr>" +

                           "<th colspan='4'   ><h4>Graded " + "</h3> </th>" +

                           "</tr>";

                    html = html + "</tr></thead>";

                    html = html + "<tbody>";

                    for (int i = 0; i < dtdaGraded.Rows.Count; i++)
                    {
                        int Total = 0;
                        decimal FoodRate = 0;
                        html = html + "<tr>";
                        for (int j = 0; j < dtdaGraded.Columns.Count; j++)
                        {
                            if (dtdaGraded.Rows[i][j].ToString() == "0")
                            {
                                html = html + "<td bgcolor='IndianRed'>" + dtdaGraded.Rows[i][j].ToString() + "</td>";

                            }
                            else
                            {

                                try
                                {
                                    if (dtdaGraded.Rows[i][j].ToString() == "1")
                                    {
                                        Total = Total + Convert.ToInt32(dtdaGraded.Rows[i][j].ToString());
                                    }

                                    try
                                    {
                                        decimal FoodRate22 = Convert.ToDecimal(dtdaGraded.Rows[i][j].ToString());
                                        if (FoodRate22 > 30)
                                        {
                                            FoodRate = Convert.ToDecimal(dtdaGraded.Rows[i][j].ToString());
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        //   throw;
                                    }



                                }
                                catch (Exception)
                                {

                                    // throw;
                                }//

                                html = html + "<td>" + dtdaGraded.Rows[i][j].ToString() + "</td>";

                            }
                        }
                        TotalCount = TotalCount + (Total * FoodRate);

                        html = html + "<td>" + Total * FoodRate + "</td>";
                        html = html + "</tr>";
                    }


                    html = html + "</tbody>";


                    //DataTable dtdaGuest = allowanceCancelDal.GetLunchAllowEmpReportNew(GenerateParameter(),
                    //    " and mas.CategoryId=11", ddlYear.SelectedValue, ddlmonth.SelectedItem.Text);

                    DataTable dtdaGuest = allowanceCancelDal.Get_ReportLunch(GenerateParameter() +
                     " and alw.CategoryId=11", mainDate_);
                    RowCount = RowCount + dtdaGuest.Rows.Count;

                    html = html + "<thead> " + "<tr>" +

                           "<th colspan='4'   ><h4>Guest " + "</h3> </th>" +

                           "</tr>";

                    html = html + "</tr></thead>";

                    html = html + "<tbody>";

                    for (int i = 0; i < dtdaGuest.Rows.Count; i++)
                    {
                        int Total = 0;
                        decimal FoodRate = 0;
                        html = html + "<tr>";
                        for (int j = 0; j < dtdaGuest.Columns.Count; j++)
                        {
                            if (dtdaGuest.Rows[i][j].ToString() == "0")
                            {
                                html = html + "<td bgcolor='IndianRed'>" + dtdaGuest.Rows[i][j].ToString() + "</td>";

                            }
                            else
                            {
                                html = html + "<td>" + dtdaGuest.Rows[i][j].ToString() + "</td>";

                                try
                                {
                                    if (dtdaGuest.Rows[i][j].ToString() == "1")
                                    {
                                        Total = Total + Convert.ToInt32(dtdaGuest.Rows[i][j].ToString());
                                    }

                                    try
                                    {
                                        decimal FoodRate22 = Convert.ToDecimal(dtdaGuest.Rows[i][j].ToString());
                                        if (FoodRate22 > 30)
                                        {
                                            FoodRate = Convert.ToDecimal(dtdaGuest.Rows[i][j].ToString());
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        //   throw;
                                    }



                                }
                                catch (Exception)
                                {

                                    // throw;
                                }//


                            }
                        }
                        TotalCount = TotalCount + (Total * FoodRate);

                        html = html + "<td>" + Total * FoodRate + "</td>";
                        html = html + "</tr>";
                    }


                    html = html + "</tbody>";


                    //DataTable dtdaIntern = allowanceCancelDal.GetLunchAllowEmpReportNew(GenerateParameter(),
                    //    " and mas.CategoryId=12", ddlYear.SelectedValue, ddlmonth.SelectedItem.Text);
                    DataTable dtdaIntern = allowanceCancelDal.Get_ReportLunch(GenerateParameter() +
   " and alw.CategoryId=12", mainDate_);
                    RowCount = RowCount + dtdaIntern.Rows.Count;

                    html = html + "<thead> " + "<tr>" +

                           "<th colspan='4'   ><h4>Intern " + "</h3> </th>" +

                           "</tr>";

                    html = html + "</tr></thead>";

                    html = html + "<tbody>";

                    for (int i = 0; i < dtdaIntern.Rows.Count; i++)
                    {
                        int Total = 0;
                        decimal FoodRate = 0;
                        html = html + "<tr>";
                        for (int j = 0; j < dtdaIntern.Columns.Count; j++)
                        {
                          
                            if (dtdaIntern.Rows[i][j].ToString() == "0")
                            {
                                html = html + "<td bgcolor='IndianRed'>" + dtdaIntern.Rows[i][j].ToString() + "</td>";

                            }
                            else
                            {
                                html = html + "<td>" + dtdaIntern.Rows[i][j].ToString() + "</td>";
                                try
                                {
                                    if (dtdaIntern.Rows[i][j].ToString() == "1")
                                    {
                                        Total = Total + Convert.ToInt32(dtdaIntern.Rows[i][j].ToString());
                                    }

                                    try
                                    {
                                        decimal FoodRate22 = Convert.ToDecimal(dtdaIntern.Rows[i][j].ToString());
                                        if (FoodRate22 > 30)
                                        {
                                            FoodRate = Convert.ToDecimal(dtdaIntern.Rows[i][j].ToString());
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        //   throw;
                                    }



                                }
                                catch (Exception)
                                {

                                    // throw;
                                }//
                            }
                        }
                        TotalCount = TotalCount + (Total * FoodRate);

                        html = html + "<td>" + Total * FoodRate + "</td>";

                        html = html + "</tr>";
                    }


                    DataTable dtdaCasual = allowanceCancelDal.Get_ReportLunch(GenerateParameter() +
 " and alw.CategoryId=13", mainDate_);
                    RowCount = RowCount + dtdaCasual.Rows.Count;

                    html = html + "<thead> " + "<tr>" +

                           "<th colspan='4'   ><h4>Casual " + "</h3> </th>" +

                           "</tr>";

                    html = html + "</tr></thead>";

                    html = html + "<tbody>";

                    for (int i = 0; i < dtdaCasual.Rows.Count; i++)
                    {
                        int Total = 0;
                        decimal FoodRate = 0;
                        html = html + "<tr>";
                        for (int j = 0; j < dtdaCasual.Columns.Count; j++)
                        {

                            if (dtdaCasual.Rows[i][j].ToString() == "0")
                            {
                                html = html + "<td bgcolor='IndianRed'>" + dtdaCasual.Rows[i][j].ToString() + "</td>";

                            }
                            else
                            {
                                html = html + "<td>" + dtdaCasual.Rows[i][j].ToString() + "</td>";
                                try
                                {
                                    if (dtdaCasual.Rows[i][j].ToString() == "1")
                                    {
                                        Total = Total + Convert.ToInt32(dtdaCasual.Rows[i][j].ToString());
                                    }

                                    try
                                    {
                                        decimal FoodRate22 = Convert.ToDecimal(dtdaCasual.Rows[i][j].ToString());
                                        if (FoodRate22 > 30)
                                        {
                                            FoodRate = Convert.ToDecimal(dtdaCasual.Rows[i][j].ToString());
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        //   throw;
                                    }



                                }
                                catch (Exception)
                                {

                                    // throw;
                                }//
                            }
                        }
                        TotalCount = TotalCount + (Total * FoodRate);

                        html = html + "<td>" + Total * FoodRate + "</td>";

                        html = html + "</tr>";
                    }


                    html = html + "<tfoot>";
                    html = html + "<th>Grand Total" + "</th>";
                    html = html + "<th>" + RowCount + "</th>";
                    for (int i = 0; i < dtdaCasual.Columns.Count; i++)
                    {
                        //dtdata.Columns[i].ColumnName +
                        if (i > 1)
                        {
                            html = html + "<th>" + "</th>";
                        }
                        //html = html + "<th>"+TotalCount + "</th>";
                    }
                    html = html + "<th>" + TotalCount + "</th>";
                    html = html + "</tfoot>";
                    html = html + "</tbody>";

                    html = html + "</table>";
                }

                else
                {
                    DataTable dtdata = allowanceCancelDal.Get_ReportLunch(GenerateParameter() +
" and alw.CategoryId=" + ddlEmpCategory.SelectedValue, mainDate_);

                    RowCount = RowCount + dtdata.Rows.Count;

                    //DataTable dtdata = allowanceCancelDal.GetLunchAllowEmpReportNew(GenerateParameter(), " and mas.CategoryId=" + ddlEmpCategory.SelectedValue, ddlYear.SelectedValue, ddlmonth.SelectedItem.Text);

                    html = "<table id='tblTable' style='height:200px;' class='table table-bordered text-center thead-dark table-hover table-striped tableFixHead' >";
                    html = html + "<thead> " + "<tr>" +
                                 "<th colspan='47'   ><h3> " + com + "</h3> </th>" +

                                 "</tr>" + "<tr>" +
                                  "<th colspan='47'   ><h4>  Lunch  Sheet for Date Range " + effectiveDateTextBox.Text +
                           ", " + txtToDate.Text + "</h3> </th>" +

                                 "</tr><tr>" +
                                 "<th colspan='4'   ><h4> " + ddlEmpCategory.SelectedItem.Text + "</h4> </th>" +

                                 "</tr>";
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";
                    }

                    html = html + "</tr></thead>";

                    html = html + "<tbody>";

                    for (int i = 0; i < dtdata.Rows.Count; i++)
                    {
                        int Total = 0;
                        decimal FoodRate = 0;
                        html = html + "<tr>";
                        for (int j = 0; j < dtdata.Columns.Count; j++)
                        {
                            if (dtdata.Rows[i][j].ToString() == "0")
                            {
                                html = html + "<td bgcolor='IndianRed'>" + dtdata.Rows[i][j].ToString() + "</td>";

                            }
                            else
                            {
                                html = html + "<td>" + dtdata.Rows[i][j].ToString() + "</td>";
                                try
                                {
                                    if (dtdata.Rows[i][j].ToString() == "1")
                                    {
                                        Total = Total + Convert.ToInt32(dtdata.Rows[i][j].ToString());
                                    }

                                    try
                                    {
                                        decimal FoodRate22 = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                                        if (FoodRate22 > 30)
                                        {
                                            FoodRate = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                                        }
                                    }
                                    catch (Exception)
                                    {

                                        //   throw;
                                    }



                                }
                                catch (Exception)
                                {

                                    // throw;
                                }//
                            }
                        }
                        html = html + "<td>" + Total * FoodRate + "</td>";

                        TotalCount = TotalCount + (Total * FoodRate);
                        html = html + "</tr>";
                    }

                    html = html + "<tfoot>";
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";
                    }

                    html = html + "</tfoot>";
                    html = html + "</tbody>";

                    html = html + "<tfoot>";
                    html = html + "<th>Grand Total" + "</th>";
                    html = html + "<th>" + RowCount + "</th>";
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        //dtdata.Columns[i].ColumnName +
                        if (i > 1)
                        {
                            html = html + "<th>" + "</th>";
                        }
                        //html = html + "<th>"+TotalCount + "</th>";
                    }
                    html = html + "<th>" + TotalCount + "</th>";
                    html = html + "</tfoot>";
                    html = html + "</tbody>";
                    html = html + "</table>";

                }

              
                data.InnerHtml = html;
            }
            catch (Exception)
            {
                
             //   throw;
            }



            try
            {
               

                //html = html + "<tfoot>";
                //for (int i = 0; i < dtdata.Columns.Count; i++)
                //{
                //    html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";
                //}

                //html = html + "</tfoot>";

             
                
            }
            catch (Exception)
            {

                //   throw;
            }
         
        }
    }
    protected void empStatusDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (empStatusDropDownList.SelectedValue == "0")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDL(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
        else if (empStatusDropDownList.SelectedValue == "Yes")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActive(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
        else
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAInactive(companyDropDownList.SelectedValue.ToString()))
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
    private bool validfa()
    {
        


        if (ddlmonth.SelectedValue == "")
        {
            AlertMessageBoxShow("Please Select Month...");
            effectiveDateTextBox.Focus();
            return false;





        }
        if ((ddlYear.SelectedValue == ""))
        {
            AlertMessageBoxShow("Please Select Year...");
            txtToDate.Focus();
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

    private string GenerateParameter()
    {
        string parameter = " ";

        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND alw.CompanyId='" + companyDropDownList.SelectedValue + "' ";
        }

        //if (ddlYear.SelectedValue != "")
        //{
        //    parameter = parameter + " AND mas.Year='" + ddlYear.SelectedValue + "' ";
        //}

        if (ddlDesignation.SelectedIndex != 0)
        {
            parameter = parameter + " AND alw.DesginationId='" + ddlDesignation.SelectedValue + "' ";
        }

        if (ddlSalaryLocation.SelectedIndex != 0)
        {
            parameter = parameter + " AND emp.SalaryLoationId='" + ddlSalaryLocation.SelectedValue + "' ";
        }

        if (ddlDepartment.SelectedIndex != 0)
        {
            parameter = parameter + " AND alw.DepartmentId='" + ddlDepartment.SelectedItem.Text + "' ";
        }

        //if (ddlmonth.SelectedValue != "")
        //{
        //    parameter = parameter + " AND mas.Month='" + ddlmonth.SelectedItem.Text + "' ";
        //}
        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + " AND alw.EmployeeId='" + ddlEmpInfo.SelectedValue + "' ";
        }


        if (effectiveDateTextBox.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + "  and Convert(Date, alw.Date) BETWEEN '" + effectiveDateTextBox.Text + "' AND '" + txtToDate.Text + "' ";
        }


        if (chkMange.Checked)
        {
            parameter = parameter + @" AND emp.SalaryGradeId in (1,2,3,4,5,6,7) ";
      
        }

       
        //if (companyDropDownList.SelectedValue != "")
        //{
        //    parameter = parameter + " AND mas.CompanyId='" + companyDropDownList.SelectedValue + "' ";
        //}

        //if (ddlYear.SelectedValue != "")
        //{
        //    parameter = parameter + " AND mas.Year='" + ddlYear.SelectedValue + "' ";
        //}

        //if (ddlDesignation.SelectedIndex != 0)
        //{
        //    parameter = parameter + " AND mas.Designation='" + ddlDesignation.SelectedItem.Text + "' ";
        //}

        //if (ddlDepartment.SelectedIndex != 0)
        //{
        //    parameter = parameter + " AND mas.DepartmentName='" + ddlDepartment.SelectedItem.Text + "' ";
        //}

        //if (ddlmonth.SelectedValue != "")
        //{
        //    parameter = parameter + " AND mas.Month='" + ddlmonth.SelectedItem.Text + "' ";
        //}
        //if (ddlEmpInfo.SelectedValue != "")
        //{
        //    parameter = parameter + " AND mas.EmployeeId='" + ddlEmpInfo.SelectedValue + "' ";
        //}

        return parameter;
    }


    protected void submitButton_OnClick(object sender, EventArgs e)
    {
    

    }

    protected void Button11_OnClick(object sender, EventArgs e)
    {
        
    }


    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            _commonDataLoad.GetSubSectionListAll(ddlSubSection, ddlDivision.SelectedValue);
        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
        }
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
        //else
        //{
        //    txtSearch.Text = "";
        //    txtSearch.Text = "";
        //    //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string empName = NameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            NameTextBox.Text = emp[2];

        }
        //else
        //{
        //    NameTextBox.Text = "";
        //    NameTextBox.Text = "";
        //  //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("LunchAllownaceCancel.aspx");
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedIndex > 0)
        {
            empStatusDropDownList_OnSelectedIndexChanged(null, null);

            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(companyDropDownList.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(companyDropDownList.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(companyDropDownList.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(companyDropDownList.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(companyDropDownList.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }
        }
        else
        {
            ddlDivision.Items.Clear();
            ddlWing.Items.Clear();
                ddlDepartment.Items.Clear();
                ddlSection.Items.Clear();
                    ddlSubSection.Items.Clear();
                    ddlSalaryLocation.Items.Clear();
        }
    }
}