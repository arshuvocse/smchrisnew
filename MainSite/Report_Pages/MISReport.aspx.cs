using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Report_DAL;

public partial class Report_Pages_MISReport : System.Web.UI.Page
{
    private static string usserId = "";
    private static SummaryReportPageDAL aSumm = new SummaryReportPageDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usserId = Session["UserId"].ToString();
        }
        catch (Exception)
        {

            Response.Redirect("~/Default.aspx");
        }

        if (!this.IsPostBack)
        {
            lblDate.InnerText = DateTime.Now.ToString("dd MMMM, yyyy");
            LoadddlCompany();

            //Department & Garde Wise Staff
            LoadDeptAndGradeWiseNoofEmployee();
            Table_8();
            Table_FiledHeadOffice();

          //  LoadDeptAndDesignationWiseNoofEmployee();
            LoadDeptAndTypeWiseNoofEmployee();
        //    LoadDeptAndGenderWiseNoofEmployee();
            LoadGradeWiseNoofEmployee();
            //LoadGradeWiseManWoman();

        }
    }

    //private void LoadDeptAndDesignationWiseNoofEmployee()
    //{
    //    staffByDesignation.InnerHtml = "";

    //    if (ddlCompany.SelectedValue != "")
    //    {
    //        DataTable dt = aSumm.GetDeptAndDesignationWiseNoofEmployee(ddlCompany.SelectedValue);

    //        DataTable newTable = new DataTable();

    //        if (dt.Rows.Count > 0)
    //        {
    //            newTable.Columns.Add("SL");
    //            newTable.Columns.Add("Department");
    //            newTable.Columns.Add("Manager & above");
    //            newTable.Columns.Add("Officer to DM");
    //            newTable.Columns.Add("Graded");
    //            newTable.Columns.Add("Sub total");
    //            newTable.Columns.Add("Casual");
    //            newTable.Columns.Add("Sub contract");
    //            newTable.Columns.Add("All total");



    //            // Update Datatble

    //            var departmentsList = new List<string>();

    //            DataRow dataRow;
    //            int sl = 1;

    //            for (int j = 0; j < dt.Rows.Count; j++)
    //            {

    //                if (j == 0)
    //                {
    //                    dataRow = newTable.NewRow();
    //                    dataRow["SL"] = sl;
    //                    dataRow["Department"] = dt.Rows[j]["DepartmentName"].ToString();
    //                    newTable.Rows.Add(dataRow);
    //                    departmentsList.Add(dt.Rows[j]["DepartmentName"].ToString());
    //                    sl++;
    //                }
    //                else
    //                {
    //                    if (!departmentsList.Contains(dt.Rows[j]["DepartmentName"].ToString()))
    //                    {
    //                        dataRow = newTable.NewRow();
    //                        dataRow["SL"] = sl;
    //                        dataRow["Department"] = dt.Rows[j]["DepartmentName"].ToString();
    //                        newTable.Rows.Add(dataRow);
    //                        departmentsList.Add(dt.Rows[j]["DepartmentName"].ToString());
    //                        sl++;
    //                    }
    //                }

    //            }

    //            dataRow = newTable.NewRow();
    //            dataRow["Department"] = "Total Staff";
    //            newTable.Rows.Add(dataRow);


    //            //for (int i = 0; i < newTable.Rows.Count; i++)
    //            //{
    //            //    for (int j = 0; j < dt.Rows.Count; j++)
    //            //    {
    //            //        if ((dt.Rows[j]["DepartmentName"].ToString() == newTable.Rows[i]["Department"].ToString()))
    //            //        {
    //            //            for (int k = 0; k < newTable.Columns.Count; k++)
    //            //            {
    //            //                if (dt.Rows[j]["EmpType"].ToString() == newTable.Columns[k].ColumnName)
    //            //                {
    //            //                    newTable.Rows[i][k] = (newTable.Rows[i][k].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i][k].ToString())) + Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);

    //            //                    newTable.Rows[i]["Sub total"] = (newTable.Rows[i]["Sub total"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i]["Sub total"].ToString())) + Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);

    //            //                    newTable.Rows[i]["All total"] = (newTable.Rows[i]["All total"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i]["All total"].ToString())) + Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);
    //            //                }

    //            //                if (dt.Rows[j]["Gender"].ToString() == newTable.Columns[k].ColumnName)
    //            //                {
    //            //                    newTable.Rows[i][k] = (newTable.Rows[i][k].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i][k].ToString())) + Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);

    //            //                }

    //            //            }

    //            //        }
    //            //    }
    //            //}


    //            for (int i = 0; i < newTable.Rows.Count; i++)
    //            {
    //                for (int k = 0; k < newTable.Columns.Count; k++)
    //                {
    //                    if (newTable.Rows[i][k].Equals(DBNull.Value))
    //                    {

    //                        if (k != 0)
    //                        {
    //                            newTable.Rows[i][k] = 0;
    //                        }

    //                    }

    //                    //if (k != 0 && k != 1 && i != newTable.Rows.Count - 1)
    //                    //{
    //                    //    newTable.Rows[newTable.Rows.Count - 1][k] = (newTable.Rows[newTable.Rows.Count - 1][k].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[newTable.Rows.Count - 1][k].ToString())) + (Convert.ToInt32(newTable.Rows[i][k].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i][k].ToString())));
    //                    //}


    //                }

    //            }


    //        }


    //        string html = "";

    //        html += "<table class='table table-bordered  thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>";
    //        html += "<thead>";
    //        html += "<tr class='text-center'><th colspan='" + newTable.Columns.Count + "' style=' background-color: #EBF1DE;'> <h3 class='mt-4'> Table-2: Staff by designation </h3> </th></tr>";
    //        html += "<tr>";
    //        for (int k = 0; k < newTable.Columns.Count; k++)
    //        {
    //            html += "<th>" + newTable.Columns[k].ColumnName + "</th>";
    //        }
    //        html += "</tr>";
    //        html += "</thead>";


    //        for (int i = 0; i < newTable.Rows.Count; i++)
    //        {
    //            html += "<tr>";
    //            for (int k = 0; k < newTable.Columns.Count; k++)
    //            {
    //                html += "<td>" + newTable.Rows[i][k].ToString() + "</td>";
    //            }
    //            html += "</tr>";
    //        }

    //        html += "</table>";


    //        staffByDesignation.InnerHtml = html;
    //    }
    //}

    //private void LoadGradeWiseManWoman()
    //{
    //    gradeWiseManWoman.InnerHtml = "";

    //    if (ddlCompany.SelectedValue != "")
    //    {
    //        DataTable dt = aSumm.GetDesigAndGradeWiseNoofEmployee(ddlCompany.SelectedValue);

    //        DataTable newTable = new DataTable();

    //        if (dt.Rows.Count > 0)
    //        {
    //            newTable.Columns.Add("SL");
    //            newTable.Columns.Add("Designation");
    //            newTable.Columns.Add("Grade");
    //            newTable.Columns.Add("Male");
    //            newTable.Columns.Add("Male(%)");
    //            newTable.Columns.Add("FeMale");
    //            newTable.Columns.Add("FeMale(%)");

    //            // Update Datatble

    //            var departmentsList = new List<string>();

    //            DataRow dataRow;
    //            int sl = 1;

    //            for (int j = 0; j < dt.Rows.Count; j++)
    //            {

    //                if (j == 0)
    //                {
    //                    dataRow = newTable.NewRow();
    //                    dataRow["SL"] = sl;
    //                    dataRow["Designation"] = dt.Rows[j]["Designation"].ToString();
    //                    dataRow["Grade"] = dt.Rows[j]["GradeCode"].ToString();
    //                    newTable.Rows.Add(dataRow);
    //                    departmentsList.Add(dt.Rows[j]["Designation"].ToString() + ":" +
    //                                        dt.Rows[j]["GradeCode"].ToString());
    //                    sl++;
    //                }
    //                else
    //                {
    //                    if (
    //                        !departmentsList.Contains(dt.Rows[j]["Designation"].ToString() + ":" +
    //                                                  dt.Rows[j]["GradeCode"].ToString()))
    //                    {
    //                        dataRow = newTable.NewRow();
    //                        dataRow["SL"] = sl;
    //                        dataRow["Designation"] = dt.Rows[j]["Designation"].ToString();
    //                        dataRow["Grade"] = dt.Rows[j]["GradeCode"].ToString();
    //                        newTable.Rows.Add(dataRow);
    //                        departmentsList.Add(dt.Rows[j]["Designation"].ToString() + ":" +
    //                                            dt.Rows[j]["GradeCode"].ToString());
    //                        sl++;
    //                    }
    //                }

    //            }

    //            dataRow = newTable.NewRow();
    //            dataRow["Designation"] = "Total Staff";
    //            newTable.Rows.Add(dataRow);


    //            for (int i = 0; i < newTable.Rows.Count; i++)
    //            {
    //                int noOf = 0;

    //                for (int j = 0; j < dt.Rows.Count; j++)
    //                {
    //                    if ((dt.Rows[j]["Designation"].ToString() == newTable.Rows[i]["Designation"].ToString()) &&
    //                        (dt.Rows[j]["GradeCode"].ToString() == newTable.Rows[i]["Grade"].ToString()))
    //                    {
    //                        for (int k = 0; k < newTable.Columns.Count; k++)
    //                        {


    //                            if (dt.Rows[j]["Gender"].ToString() == newTable.Columns[k].ColumnName)
    //                            {
    //                                newTable.Rows[i][k] = (newTable.Rows[i][k].Equals(DBNull.Value)
    //                                    ? 0
    //                                    : Convert.ToInt32(newTable.Rows[i][k].ToString())) +
    //                                                      Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);


    //                            }



    //                        }

    //                    }
    //                    else
    //                    {
    //                        noOf = 0;
    //                    }
    //                }
    //            }


    //            for (int i = 0; i < newTable.Rows.Count; i++)
    //            {
    //                for (int k = 0; k < newTable.Columns.Count; k++)
    //                {
    //                    if (newTable.Rows[i][k].Equals(DBNull.Value))
    //                    {

    //                        if (k != 0 && k != 1 && k != 2)
    //                        {
    //                            newTable.Rows[i][k] = 0;
    //                        }


    //                    }

    //                    if (k != 0 && k != 1 && k != 2 && i != newTable.Rows.Count - 1)
    //                    {
    //                        newTable.Rows[newTable.Rows.Count - 1][k] =
    //                            (newTable.Rows[newTable.Rows.Count - 1][k].Equals(DBNull.Value)
    //                                ? 0
    //                                : Convert.ToInt32(newTable.Rows[newTable.Rows.Count - 1][k].ToString())) +
    //                            (Convert.ToInt32(newTable.Rows[i][k].Equals(DBNull.Value)
    //                                ? 0
    //                                : Convert.ToInt32(newTable.Rows[i][k].ToString())));
    //                    }


    //                }

    //            }


    //            for (int i = 0; i < newTable.Rows.Count; i++)
    //            {
    //                int total = 0;
    //                for (int k = 0; k < newTable.Columns.Count; k++)
    //                {
    //                    if (newTable.Columns[k].ColumnName == "Male")
    //                    {
    //                        total = (newTable.Rows[i]["Male"].Equals(DBNull.Value)
    //                            ? 0
    //                            : Convert.ToInt32(newTable.Rows[i]["Male"].ToString()))
    //                            +
    //                            (newTable.Rows[i]["FeMale"].Equals(DBNull.Value)
    //                            ? 0
    //                            : Convert.ToInt32(newTable.Rows[i]["FeMale"].ToString()));

    //                        newTable.Rows[i]["Male(%)"] = total == 0 ? 0 : ((newTable.Rows[i]["Male"].Equals(DBNull.Value)
    //                            ? 0
    //                            : Convert.ToInt32(newTable.Rows[i]["Male"].ToString())) * 100) / total;

    //                        newTable.Rows[i]["FeMale(%)"] = total == 0 ? 0 : ((newTable.Rows[i]["FeMale"].Equals(DBNull.Value)
    //                           ? 0
    //                           : Convert.ToInt32(newTable.Rows[i]["FeMale"].ToString())) * 100) / total;
    //                    }
    //                }
    //            }

    //        }


    //        string html = "";

    //        html +=
    //            "<table class='table table-bordered text-center thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>";
    //        html += "<thead>";
    //        html += "<tr class='text-center' ><th colspan='" + newTable.Columns.Count + "' style=' background-color: #EBF1DE;'> <h3 class='mt-4'> Table-5: Grade wise Men-Women </h3> </th></tr>";

    //        html += "<tr>";
    //        html += "<th colspan='3'> </th>";
    //        html += "<th colspan='2'>Men</th>";
    //        html += "<th colspan='2'>Women</th>";
    //        html += "</tr>";
    //        html += "<tr>";
    //        for (int k = 0; k < newTable.Columns.Count; k++)
    //        {

    //            if (newTable.Columns[k].ColumnName == "Designation" || newTable.Columns[k].ColumnName == "SL" || newTable.Columns[k].ColumnName == "Grade")
    //            {
    //                html += "<th>" + newTable.Columns[k].ColumnName + "</th>";
    //            }
    //            else if (newTable.Columns[k].ColumnName == "Male" || newTable.Columns[k].ColumnName == "FeMale")
    //            {
    //                html += "<th> # </th>";
    //            }
    //            else
    //            {
    //                html += "<th> % </th>";
    //            }

    //        }
    //        html += "</tr>";
    //        html += "</thead>";


    //        for (int i = 0; i < newTable.Rows.Count; i++)
    //        {
    //            html += "<tr>";
    //            for (int k = 0; k < newTable.Columns.Count; k++)
    //            {
    //                if (newTable.Columns[k].ColumnName == "Designation")
    //                {
    //                    html += "<td class='text-left'>" + newTable.Rows[i][k].ToString() + "</td>";
    //                }
    //                else
    //                {
    //                    html += "<td>" + newTable.Rows[i][k].ToString() + "</td>";
    //                }

    //            }
    //            html += "</tr>";
    //        }

    //        html += "</table>";


    //        gradeWiseManWoman.InnerHtml = html;
    //    }
    //}

    private void LoadddlCompany()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
        }
        if (ddlCompany.SelectedValue == "1")
        {
            lblCompany.InnerText = "SOCIAL MARKETING COMPANY";
        }

        else
        {

            lblCompany.InnerText = "SMC ENTERPRISE  LTD.";

        }
    }

    private void LoadGradeWiseNoofEmployee()
    {
        gradeWiseStaff.InnerHtml = "";

        if (ddlCompany.SelectedValue != "")
        {
            
            DataTable dtdata = aSumm.GradeWiseNoofEmployee(ddlCompany.SelectedValue);

            int Permanent = 0;
            int Contract = 0;
            int Subtotal = 0;
            int Male = 0;
            int MalePer = 0;
            int Female = 0;
            int FemalePer = 0;
         

          
            try
            {
                Permanent = dtdata.AsEnumerable().Sum(row => row.Field<int>("Permanent"));
                Contract = dtdata.AsEnumerable().Sum(row => row.Field<int>("Contract"));
                Subtotal = dtdata.AsEnumerable().Sum(row => row.Field<int>("# of Staff"));
             
                Male = dtdata.AsEnumerable().Sum(row => row.Field<int>("Male"));
              //  MalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("MalePer"));
                Female = dtdata.AsEnumerable().Sum(row => row.Field<int>("Female"));
              //  FemalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("FemalePer"));

            }
            catch (Exception)
            {

                //throw;
            }

            decimal InPermanent = 0;
            decimal InContract = 0;
            decimal InMalePer = 0;
            decimal InFemalePer = 0;
            try
            {

                InPermanent = Math.Round((Convert.ToDecimal(Permanent) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);
                InContract = Math.Round((Convert.ToDecimal(Contract) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);


                InMalePer = Math.Round((Convert.ToDecimal(Male) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);
                InFemalePer = Math.Round((Convert.ToDecimal(Female) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);


            }
            catch (Exception)
            {

                //throw;
            }



            string html = "<table id='tblTable'   class='table table-bordered thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' >";
            html = html + "<thead> " + "<tr>" +
                         "<th   colspan='8'  > Table-4: Grade wise staff </th>" +

                         "<th colspan='4' >Table-5: Grade wise Men-Women	 </th>" +
                         "</tr> ";

            html = html + "<thead> " + "<tr>" +
                        "<th   colspan='8'  >   </th>" +
                       
                        "<th colspan='2' > Men		 </th>" +
                        "<th colspan='2' > Women		 </th>" +
                        "</tr></thead><tr> ";
            for (int i = 0; i < dtdata.Columns.Count; i++)
            {
                if (dtdata.Columns[i].ColumnName == "Male")
                {
                    html = html + "<th>" + "#" + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "MalePer")
                {
                    html = html + "<th>" + "%" + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Female")
                {
                    html = html + "<th>" + "#" + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "FemalePer")
                {
                    html = html + "<th>" + "%" + "</th>";

                }
                else
                {
                    html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";

                }

            }

            html = html + "</tr></thead>";

            html = html + "<tbody class='text-left'>";

            for (int i = 0; i < dtdata.Rows.Count; i++)
            {
                html = html + "<tr>";

                html = html + "<td>" + (i + 1) + "</td>";
                for (int j = 1; j < dtdata.Columns.Count; j++)
                {

                    html = html + "<td>" + dtdata.Rows[i][j].ToString() + "</td>";
                    //    if (dtdata.Columns[i].ColumnName == "Permanent")
                    //    {
                    //        decimal pp = 0;
                    //        try
                    //        {
                    //         pp   = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                    //        }
                    //        catch (Exception)
                    //        {

                    //            //throw;
                    //        }
                    //        Permanent = Permanent + pp;
                    //    }

                    //    if (dtdata.Columns[i].ColumnName == "Contract")
                    //    {
                    //        decimal pp = 0;
                    //        try
                    //        {
                    //            pp = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                    //        }
                    //        catch (Exception)
                    //        {

                    //            //throw;
                    //        }
                    //        Contract = Contract + pp;
                    //    }
                }

                html = html + "</tr>";
            }


            html = html + "</tbody>";

            html = html + "<tfoot>";
            for (int i = 0; i < dtdata.Columns.Count; i++)
            {
                if (dtdata.Columns[i].ColumnName == "Permanent")
                {
                    html = html + "<th>" + Permanent + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Contract")
                {
                    html = html + "<th>" + Contract + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "Sub total")
                {
                    html = html + "<th>" + Subtotal + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "# of Staff")
                {
                    html = html + "<th>" + Subtotal + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Sl")
                {
                    html = html + "<th   colspan='3'>" + "Total staff" + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "Designation")
                {

                }
                else if (dtdata.Columns[i].ColumnName == "Grade")
                {

                }
                    
                else if (dtdata.Columns[i].ColumnName == "Male")
                {
                    html = html + "<th>" + Male + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "MalePer")
                {
                    html = html + "<th>" + InMalePer + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "Female")
                {
                    html = html + "<th>" + Female + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "FemalePer")
                {
                    html = html + "<th>" + InFemalePer + "</th>";

                }

                else
                {
                    html = html + "<th>" + "0" + "</th>";
                }
            }

             

            html = html + "</tfoot>";

            html = html + "</table>";


            gradeWiseStaff.InnerHtml = html;
        }
        
        
    }



    //private void LoadDeptAndGenderWiseNoofEmployee()
    //{
    //    departmentGenderWiseStaff.InnerHtml = "";

    //    if (ddlCompany.SelectedValue != "")
    //    {
    //        DataTable dt = aSumm.GetDeptAndGenderWiseNoofEmployee(ddlCompany.SelectedValue);

    //        DataTable newTable = new DataTable();

    //        if (dt.Rows.Count > 0)
    //        {
    //            newTable.Columns.Add("SL");
    //            newTable.Columns.Add("Department");
    //            newTable.Columns.Add("Male");
    //            newTable.Columns.Add("Male(%)");
    //            newTable.Columns.Add("FeMale");
    //            newTable.Columns.Add("FeMale(%)");

    //            // Update Datatble

    //            var departmentsList = new List<string>();

    //            DataRow dataRow;
    //            int sl = 1;

    //            for (int j = 0; j < dt.Rows.Count; j++)
    //            {

    //                if (j == 0)
    //                {
    //                    dataRow = newTable.NewRow();
    //                    dataRow["SL"] = sl;
    //                    dataRow["Department"] = dt.Rows[j]["DepartmentName"].ToString();
    //                    newTable.Rows.Add(dataRow);
    //                    departmentsList.Add(dt.Rows[j]["DepartmentName"].ToString());
    //                    sl++;
    //                }
    //                else
    //                {
    //                    if (!departmentsList.Contains(dt.Rows[j]["DepartmentName"].ToString()))
    //                    {
    //                        dataRow = newTable.NewRow();
    //                        dataRow["SL"] = sl;
    //                        dataRow["Department"] = dt.Rows[j]["DepartmentName"].ToString();
    //                        newTable.Rows.Add(dataRow);
    //                        departmentsList.Add(dt.Rows[j]["DepartmentName"].ToString());
    //                        sl++;
    //                    }
    //                }

    //            }

    //            dataRow = newTable.NewRow();
    //            dataRow["Department"] = "Total Staff";
    //            newTable.Rows.Add(dataRow);


    //            for (int i = 0; i < newTable.Rows.Count; i++)
    //            {
    //                for (int j = 0; j < dt.Rows.Count; j++)
    //                {
    //                    if ((dt.Rows[j]["DepartmentName"].ToString() == newTable.Rows[i]["Department"].ToString()))
    //                    {
    //                        for (int k = 0; k < newTable.Columns.Count; k++)
    //                        {
    //                            if (dt.Rows[j]["Gender"].ToString() == newTable.Columns[k].ColumnName)
    //                            {
    //                                newTable.Rows[i][k] = dt.Rows[j]["NoOfEmp"];
    //                            }

    //                        }

    //                    }
    //                }
    //            }


    //            for (int i = 0; i < newTable.Rows.Count; i++)
    //            {
    //                for (int k = 0; k < newTable.Columns.Count; k++)
    //                {
    //                    if (newTable.Rows[i][k].Equals(DBNull.Value))
    //                    {

    //                        if (k != 0)
    //                        {
    //                            newTable.Rows[i][k] = 0;
    //                        }


    //                    }

    //                    if (k != 0 && k != 1 && i != newTable.Rows.Count - 1)
    //                    {
    //                        newTable.Rows[newTable.Rows.Count - 1][k] =
    //                            (newTable.Rows[newTable.Rows.Count - 1][k].Equals(DBNull.Value)
    //                                ? 0
    //                                : Convert.ToInt32(newTable.Rows[newTable.Rows.Count - 1][k].ToString())) +
    //                            (Convert.ToInt32(newTable.Rows[i][k].Equals(DBNull.Value)
    //                                ? 0
    //                                : Convert.ToInt32(newTable.Rows[i][k].ToString())));
    //                    }


    //                }

    //            }


    //            for (int i = 0; i < newTable.Rows.Count; i++)
    //            {
    //                int total = 0;
    //                for (int k = 0; k < newTable.Columns.Count; k++)
    //                {
    //                    if (newTable.Columns[k].ColumnName == "Male")
    //                    {
    //                        total = (newTable.Rows[i]["Male"].Equals(DBNull.Value)
    //                            ? 0
    //                            : Convert.ToInt32(newTable.Rows[i]["Male"].ToString())) 
    //                            +
    //                            (newTable.Rows[i]["FeMale"].Equals(DBNull.Value)
    //                            ? 0
    //                            : Convert.ToInt32(newTable.Rows[i]["FeMale"].ToString()));

    //                        newTable.Rows[i]["Male(%)"] = ((newTable.Rows[i]["Male"].Equals(DBNull.Value)
    //                            ? 0
    //                            : Convert.ToInt32(newTable.Rows[i]["Male"].ToString()))*100)/total;

    //                        newTable.Rows[i]["FeMale(%)"] = ((newTable.Rows[i]["FeMale"].Equals(DBNull.Value)
    //                           ? 0
    //                           : Convert.ToInt32(newTable.Rows[i]["FeMale"].ToString())) * 100) / total;
    //                    }
    //                }
    //            }

    //        }


    //        string html = "";

    //        html +=
    //            "<table class='table table-bordered text-center thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>";
    //        html += "<thead>";
    //        html += "<tr class='text-center' ><th colspan='" + newTable.Columns.Count + "' style=' background-color: #EBF1DE;'> <h3 class='mt-4'> Table-3: Department wise Men-Women </h3> </th>" +
    //                "" +
    //                "</tr>";

    //        html += "<tr>";
    //        html += "<th colspan='2'> </th>";
    //        html += "<th colspan='2'>Men</th>";
    //        html += "<th colspan='2'>Women</th>";
    //        html += "</tr>";
            
    //        html += "<tr>";
    //        for (int k = 0; k < newTable.Columns.Count; k++)
    //        {

    //            if (newTable.Columns[k].ColumnName == "Department" || newTable.Columns[k].ColumnName == "SL")
    //            {
    //                html += "<th>" + newTable.Columns[k].ColumnName + "</th>";
    //            }
    //            else if (newTable.Columns[k].ColumnName == "Male" || newTable.Columns[k].ColumnName == "FeMale")
    //            {
    //                html += "<th> # </th>";
    //            }
    //            else
    //            {
    //                html += "<th> % </th>";
    //            }
                
    //        }
    //        html += "</tr>";
    //        html += "</thead>";


    //        for (int i = 0; i < newTable.Rows.Count; i++)
    //        {
    //            html += "<tr>";
    //            for (int k = 0; k < newTable.Columns.Count; k++)
    //            {
    //                if (newTable.Columns[k].ColumnName == "Department")
    //                {
    //                    html += "<td class='text-left'>" + newTable.Rows[i][k].ToString() + "</td>";
    //                }
    //                else
    //                {
    //                    html += "<td>" + newTable.Rows[i][k].ToString() + "</td>";
    //                }
                    
    //            }
    //            html += "</tr>";
    //        }

    //        html += "</table>";


    //        departmentGenderWiseStaff.InnerHtml = html;
    //    }
    //}

    private void LoadDeptAndTypeWiseNoofEmployee()
    {
        departmentTypeWiseStaff.InnerHtml = "";

        if (ddlCompany.SelectedValue != "")
        {
            int Permanent = 0;
            int Contract = 0;
            int Subtotal = 0;
            int Male = 0;
            int MalePer = 0;
            int Female = 0;
            int FemalePer = 0;
            int Manager_above = 0;
            int Officer_DM = 0;
            
            DataTable dtdata = aSumm.GetDeptAndTypeWiseNoofEmployee(ddlCompany.SelectedValue);
            try
            {
                Permanent = dtdata.AsEnumerable().Sum(row => row.Field<int>("Permanent"));
                Contract = dtdata.AsEnumerable().Sum(row => row.Field<int>("Contract"));
                Subtotal = dtdata.AsEnumerable().Sum(row => row.Field<int>("Sub total"));
                Subtotal = dtdata.AsEnumerable().Sum(row => row.Field<int>("Sub total"));
                Male = dtdata.AsEnumerable().Sum(row => row.Field<int>("Male"));
                MalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("MalePer"));
                Female = dtdata.AsEnumerable().Sum(row => row.Field<int>("Female"));
                FemalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("FemalePer"));
                Manager_above = dtdata.AsEnumerable().Sum(row => row.Field<int>("Manager & above"));
                Officer_DM = dtdata.AsEnumerable().Sum(row => row.Field<int>("Officer to DM"));
            }
            catch (Exception)
            {
                
                //throw;
            }

            decimal InPermanent = 0;
            decimal InContract = 0;
            decimal InMalePer = 0;
            decimal InFemalePer = 0;
            try
            {

                InPermanent = Math.Round((Convert.ToDecimal(Permanent) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);
                InContract = Math.Round((Convert.ToDecimal(Contract) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);


                InMalePer = Math.Round((Convert.ToDecimal(Male) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);
                InFemalePer = Math.Round((Convert.ToDecimal(Female) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);

               
            }
            catch (Exception)
            {

                //throw;
            }

            string html = "<table id='tblTable'   class='table table-bordered thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>";
            html = html + "<thead style='background-color: #EBF1DE;'> " + "<tr>" +
                         "<th   colspan='9'  > Table-1: Department & employment type wise staff </th>" +
                         "<th colspan='7'  > Table-2: Staff by designation </th>" +
                         "<th colspan='4' > Table-3: Department wise Men-Women	 </th>" +
                         "</tr> ";

            html = html + "<thead style='background-color: #EBF1DE;'> " + "<tr>" +
                        "<th   colspan='9'  >   </th>" +
                        "<th colspan='7'  > </th>" +
                        "<th colspan='2' > Men		 </th>" +
                        "<th colspan='2' > Women		 </th>" +
                        "</tr></thead><tr> ";
            for (int i = 0; i < dtdata.Columns.Count; i++)
            {
                if (dtdata.Columns[i].ColumnName == "Sub total1")
                {
                    html = html + "<th>" + "Sub total" + "</th>";
                    
                }
                else if (dtdata.Columns[i].ColumnName == "Casual1")
                {
                    html = html + "<th>" + "Casual" + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Sub contract1")
                {
                    html = html + "<th>" + "Sub contract" + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "All total1")
                {
                    html = html + "<th>" + "All total" + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Male")
                {
                    html = html + "<th>" + "#" + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "MalePer")
                {
                    html = html + "<th>" + "%" + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Female")
                {
                    html = html + "<th>" + "#" + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "FemalePer")
                {
                    html = html + "<th>" + "%" + "</th>";

                }
                else
                {
                    html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";

                }

            }

            html = html + "</tr></thead>";

            html = html + "<tbody class='text-left'>";

            for (int i = 0; i < dtdata.Rows.Count; i++)
            {
                html = html + "<tr>";
                for (int j = 0; j < dtdata.Columns.Count; j++)
                {
                    html = html + "<td>" + dtdata.Rows[i][j].ToString() + "</td>";
                //    if (dtdata.Columns[i].ColumnName == "Permanent")
                //    {
                //        decimal pp = 0;
                //        try
                //        {
                //         pp   = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                //        }
                //        catch (Exception)
                //        {
                            
                //            //throw;
                //        }
                //        Permanent = Permanent + pp;
                //    }

                //    if (dtdata.Columns[i].ColumnName == "Contract")
                //    {
                //        decimal pp = 0;
                //        try
                //        {
                //            pp = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                //        }
                //        catch (Exception)
                //        {

                //            //throw;
                //        }
                //        Contract = Contract + pp;
                //    }
               }

                html = html + "</tr>";
            }


            html = html + "</tbody>";


            //for (int i = 0; i < dtdata.Rows.Count; i++)
            //{
            //    html += "<tr>";
            //    for (int k = 0; k < dtdata.Columns.Count; k++)
            //    {
            //        html += "<td>" + dtdata.Rows[i][k].ToString() + "</td>";
            //    }
            //    html += "</tr>";
            //}



            html = html + "<tfoot>";
           
            for (int i = 0; i < dtdata.Columns.Count; i++)
            {
                if (dtdata.Columns[i].ColumnName == "Permanent")
                {
                    html = html + "<th>" + InPermanent + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Contract")
                {
                    html = html + "<th>" + InContract + "</th>";

                }

              
                else if (dtdata.Columns[i].ColumnName == "Sl")
                {
                    html = html + "<th   colspan='2'>" + "In %" + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "Department")
                {

                }
               

                else
                {
                    html = html + "<th>" + " " + "</th>";
                }
            }

            html = html + "</tfoot>";



            html = html + "<tfoot>";
            for (int i = 0; i < dtdata.Columns.Count; i++)
            {
                if (dtdata.Columns[i].ColumnName == "Permanent")
                {
                    html = html + "<th>" + Permanent + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Contract")
                {
                    html = html + "<th>" + Contract + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "Sub total")
                {
                    html = html + "<th>" + Subtotal + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "All total")
                {
                    html = html + "<th>" + Subtotal + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "Manager & above")
                {
                    html = html + "<th>" + Manager_above + "</th>";
                }

                else if (dtdata.Columns[i].ColumnName == "Officer to DM")
                {
                    html = html + "<th>" + Officer_DM + "</th>";
                }
                else if (dtdata.Columns[i].ColumnName == "Sl")
                {
                    html = html + "<th   colspan='2'>" + "Total staff" + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "Department")
                {

                }
                else if (dtdata.Columns[i].ColumnName == "Male")
                {
                    html = html + "<th>" + Male + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "MalePer")
                {
                    html = html + "<th>" + InMalePer + "</th>";

                }

                else if (dtdata.Columns[i].ColumnName == "Female")
                {
                    html = html + "<th>" + Female + "</th>";

                }
                else if (dtdata.Columns[i].ColumnName == "FemalePer")
                {
                    html = html + "<th>" + InFemalePer + "</th>";

                }

                else
                {
                    html = html + "<th>" + "0" + "</th>";
                }
            }

            html = html + "</tfoot>";

            html = html + "</table>";
            departmentTypeWiseStaff.InnerHtml = html;

          

          //  departmentTypeWiseStaff.InnerHtml = html;
        }
        
    }

    private void Table_8()
    {
        divTable8.InnerHtml = "";

        if (ddlCompany.SelectedValue != "")
        {
            using (DataTable dtdata = aSumm.GeBindTable8DataDAL(Convert.ToInt32((ddlCompany.SelectedValue))))
            {
                if (dtdata.Rows.Count > 0)
                {
                    
                    int Subtotal = 0;
                    int Male = 0;
                    int MalePer = 0;
                    int Female = 0;
                    int FemalePer = 0;


                    try
                    {
                        
                        Subtotal = dtdata.AsEnumerable().Sum(row => row.Field<int>("Total # of staff"));

                        Male = dtdata.AsEnumerable().Sum(row => row.Field<int>("Male"));
                        //  MalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("MalePer"));
                        Female = dtdata.AsEnumerable().Sum(row => row.Field<int>("Female"));
                        //  FemalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("FemalePer"));
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    decimal InPermanent = 0;
                    decimal InContract = 0;
                    decimal InMalePer = 0;
                    decimal InFemalePer = 0;
                    try
                    {

                        


                        InMalePer = Math.Round((Convert.ToDecimal(Male) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);
                        InFemalePer = Math.Round((Convert.ToDecimal(Female) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);


                    }
                    catch (Exception)
                    {

                        //throw;
                    }



                    string html = "<table id='tblTable'  class='table table-bordered thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' >";
                    html = html + "<thead> " + "<tr>" +
                                 "<th  colspan='8' > Table-8: Men-Women ratio in Program Division (Head Office and Field Office) </th>" +

                                
                                 "</tr> ";

                   

                    html = html + "<thead> " + "<tr>" +
                                "<th   colspan='3'  >   </th>" +

                                "<th colspan='2' > Men		 </th>" +
                                "<th colspan='2' > Women		 </th>" +
                                "</tr></thead><tr> ";
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        if (dtdata.Columns[i].ColumnName == "Male")
                        {
                            html = html + "<th>" + "#" + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "MalePer")
                        {
                            html = html + "<th>" + "%" + "</th>";

                        }
                        else if (dtdata.Columns[i].ColumnName == "Female")
                        {
                            html = html + "<th>" + "#" + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "FemalePer")
                        {
                            html = html + "<th>" + "%" + "</th>";

                        }
                        else
                        {
                            html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";

                        }

                    }

                    html = html + "</tr></thead>";

                    html = html + "<tbody class='text-left'>";

                    for (int i = 0; i < dtdata.Rows.Count; i++)
                    {
                        html = html + "<tr>";
                        for (int j = 0; j < dtdata.Columns.Count; j++)
                        {
                            html = html + "<td>" + dtdata.Rows[i][j].ToString() + "</td>";
                            //    if (dtdata.Columns[i].ColumnName == "Permanent")
                            //    {
                            //        decimal pp = 0;
                            //        try
                            //        {
                            //         pp   = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                            //        }
                            //        catch (Exception)
                            //        {

                            //            //throw;
                            //        }
                            //        Permanent = Permanent + pp;
                            //    }

                            //    if (dtdata.Columns[i].ColumnName == "Contract")
                            //    {
                            //        decimal pp = 0;
                            //        try
                            //        {
                            //            pp = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                            //        }
                            //        catch (Exception)
                            //        {

                            //            //throw;
                            //        }
                            //        Contract = Contract + pp;
                            //    }
                        }

                        html = html + "</tr>";
                    }


                    html = html + "</tbody>";

                    html = html + "<tfoot>";
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {


                        if (dtdata.Columns[i].ColumnName == "Total # of staff")
                        {
                            html = html + "<th>" + Subtotal + "</th>";

                        }
                       
                        else if (dtdata.Columns[i].ColumnName == "Sl")
                        {
                            html = html + "<th   colspan='2'>" + "Total" + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "Project")
                        {

                        }
                        

                        else if (dtdata.Columns[i].ColumnName == "Male")
                        {
                            html = html + "<th>" + Male + "</th>";

                        }
                        else if (dtdata.Columns[i].ColumnName == "MalePer")
                        {
                            html = html + "<th>" + InMalePer + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "Female")
                        {
                            html = html + "<th>" + Female + "</th>";

                        }
                        else if (dtdata.Columns[i].ColumnName == "FemalePer")
                        {
                            html = html + "<th>" + InFemalePer + "</th>";

                        }

                        else
                        {
                            html = html + "<th>" + "0" + "</th>";
                        }
                    }



                    html = html + "</tfoot>";

                    html = html + "</table>";


                    divTable8.InnerHtml = html;

                }
            }
        }
    }


    private void Table_FiledHeadOffice()
    {
        table7.InnerHtml = "";

        if (ddlCompany.SelectedValue != "")
        {
            using (DataTable dtdata = aSumm.GeBindTableDataDAL_Head_field(Convert.ToInt32((ddlCompany.SelectedValue))))
            {
                if (dtdata.Rows.Count > 0)
                {

                    int Subtotal = 0;
                    int Male = 0;
                    int MalePer = 0;
                    int Female = 0;
                    int FemalePer = 0;


                    try
                    {

                        Subtotal = dtdata.AsEnumerable().Sum(row => row.Field<int>("Total # of staff"));

                        Male = dtdata.AsEnumerable().Sum(row => row.Field<int>("Male"));
                        //  MalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("MalePer"));
                        Female = dtdata.AsEnumerable().Sum(row => row.Field<int>("Female"));
                        //  FemalePer = dtdata.AsEnumerable().Sum(row => row.Field<int>("FemalePer"));
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    decimal InPermanent = 0;
                    decimal InContract = 0;
                    decimal InMalePer = 0;
                    decimal InFemalePer = 0;
                    try
                    {




                        InMalePer = Math.Round((Convert.ToDecimal(Male) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);
                        InFemalePer = Math.Round((Convert.ToDecimal(Female) / Convert.ToDecimal(Subtotal)) * 100, 0, MidpointRounding.AwayFromZero);


                    }
                    catch (Exception)
                    {

                        //throw;
                    }



                    string html = "<table id='tblTable'  class='table table-bordered thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;' >";
                    html = html + "<thead> " + "<tr>" +
                                 "<th  colspan='8' >  Table-7: Head office & Field and Project/Fund wise Men-Women ratio  </th>" +


                                 "</tr> ";



                    html = html + "<thead> " + "<tr>" +
                                "<th   colspan='3'  >   </th>" +

                                "<th colspan='3' > Head Office 		 </th>" +
                                "<th colspan='3' > Field		 </th>" +

                                "<th colspan='3' > Total		 </th></tr></thead><tr> ";
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        if (dtdata.Columns[i].ColumnName == "HeadOfficeMale")
                        {
                            html = html + "<th>" + "Men" + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "HeadOfficeFeMale")
                        {
                            html = html + "<th>" + "Women" + "</th>";

                        }
                        else if (dtdata.Columns[i].ColumnName == "HeadOffice")
                        {
                            html = html + "<th>" + "Total" + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "FieldMale")
                        {
                            html = html + "<th>" + "Men" + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "FieldFeMale")
                        {
                            html = html + "<th>" + "Women" + "</th>";

                        }

                        else if (dtdata.Columns[i].ColumnName == "Field")
                        {
                            html = html + "<th>" + "Total" + "</th>";

                        }

                         
                        else
                        {
                            html = html + "<th>" + dtdata.Columns[i].ColumnName + "</th>";

                        }

                    }

                    html = html + "</tr></thead>";

                    html = html + "<tbody class='text-left'>";

                    for (int i = 0; i < dtdata.Rows.Count; i++)
                    {
                        html = html + "<tr>";
                        for (int j = 0; j < dtdata.Columns.Count; j++)
                        {
                            html = html + "<td>" + dtdata.Rows[i][j].ToString() + "</td>";
                            //    if (dtdata.Columns[i].ColumnName == "Permanent")
                            //    {
                            //        decimal pp = 0;
                            //        try
                            //        {
                            //         pp   = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                            //        }
                            //        catch (Exception)
                            //        {

                            //            //throw;
                            //        }
                            //        Permanent = Permanent + pp;
                            //    }

                            //    if (dtdata.Columns[i].ColumnName == "Contract")
                            //    {
                            //        decimal pp = 0;
                            //        try
                            //        {
                            //            pp = Convert.ToDecimal(dtdata.Rows[i][j].ToString());
                            //        }
                            //        catch (Exception)
                            //        {

                            //            //throw;
                            //        }
                            //        Contract = Contract + pp;
                            //    }
                        }

                        html = html + "</tr>";
                    }


                    html = html + "</tbody>";

                    //html = html + "<tfoot>";
                    //for (int i = 0; i < dtdata.Columns.Count; i++)
                    //{


                    //    if (dtdata.Columns[i].ColumnName == "Total # of staff")
                    //    {
                    //        html = html + "<th>" + Subtotal + "</th>";

                    //    }

                    //    else if (dtdata.Columns[i].ColumnName == "Sl")
                    //    {
                    //        html = html + "<th   colspan='2'>" + "Total" + "</th>";

                    //    }

                    //    else if (dtdata.Columns[i].ColumnName == "Project")
                    //    {

                    //    }


                    //    else if (dtdata.Columns[i].ColumnName == "Male")
                    //    {
                    //        html = html + "<th>" + Male + "</th>";

                    //    }
                    //    else if (dtdata.Columns[i].ColumnName == "MalePer")
                    //    {
                    //        html = html + "<th>" + InMalePer + "</th>";

                    //    }

                    //    else if (dtdata.Columns[i].ColumnName == "Female")
                    //    {
                    //        html = html + "<th>" + Female + "</th>";

                    //    }
                    //    else if (dtdata.Columns[i].ColumnName == "FemalePer")
                    //    {
                    //        html = html + "<th>" + InFemalePer + "</th>";

                    //    }

                    //    else
                    //    {
                    //        html = html + "<th>" + "0" + "</th>";
                    //    }
                    //}



                    //html = html + "</tfoot>";

                    html = html + "</table>";


                    table7.InnerHtml = html;

                }
            }
        }
    }

    private void LoadDeptAndGradeWiseNoofEmployee()
    {
        departmentGradeWiseStaff.InnerHtml = "";

        if (ddlCompany.SelectedValue != "")
        {
            DataTable dt = aSumm.GetDeptAndGradeWiseNoofEmployee(ddlCompany.SelectedValue);

            DataTable newTable = new DataTable();

            if (dt.Rows.Count > 0)
            {

                // Create Datatable


                newTable.Columns.Add("SL");
                newTable.Columns.Add("Department");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!newTable.Columns.Contains(dt.Rows[i]["GradeCode"].ToString()))
                    {
                        newTable.Columns.Add(dt.Rows[i]["GradeCode"].ToString());
                    }
                }

                newTable.Columns.Add("Management");
                newTable.Columns.Add("Graded");

                newTable.Columns.Add("Sub total");
                newTable.Columns.Add("Casual");
                newTable.Columns.Add("Sub contract");
                newTable.Columns.Add("Grand total");


                // Update Datatble

                var departmentsList = new List<string>();

                DataRow dataRow;

                int sl = 1;

                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    if (j == 0)
                    {
                        dataRow = newTable.NewRow();
                        dataRow["SL"] = sl;
                        dataRow["Department"] = dt.Rows[j]["DepartmentName"].ToString();
                        newTable.Rows.Add(dataRow);
                        departmentsList.Add(dt.Rows[j]["DepartmentName"].ToString());
                        sl++;
                    }
                    else
                    {
                        if (!departmentsList.Contains(dt.Rows[j]["DepartmentName"].ToString()))
                        {
                            dataRow = newTable.NewRow();
                            dataRow["SL"] = sl;
                            dataRow["Department"] = dt.Rows[j]["DepartmentName"].ToString();
                            newTable.Rows.Add(dataRow);
                            departmentsList.Add(dt.Rows[j]["DepartmentName"].ToString());
                            sl++;
                        }
                    }

                }

                dataRow = newTable.NewRow();
                dataRow["Department"] = "Total Staff";
                newTable.Rows.Add(dataRow);


                for (int i = 0; i < newTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if ((dt.Rows[j]["DepartmentName"].ToString() == newTable.Rows[i]["Department"].ToString()))
                        {
                            for (int k = 0; k < newTable.Columns.Count; k++)
                            {
                                if (dt.Rows[j]["GradeCode"].ToString() == newTable.Columns[k].ColumnName)
                                {
                                    newTable.Rows[i][k] = dt.Rows[j]["NoOfEmp"];
                                }
                                else
                                {
                                    if (dt.Rows[j]["EmpCategoryName"].ToString() == newTable.Columns[k].ColumnName)
                                    {
                                        newTable.Rows[i][k] = (newTable.Rows[i][k].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i][k].ToString())) + Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);

                                        newTable.Rows[i]["Sub total"] = (newTable.Rows[i]["Sub total"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i]["Sub total"].ToString())) + Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);

                                        newTable.Rows[i]["Grand total"] = (newTable.Rows[i]["Grand total"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i]["Grand total"].ToString())) + Convert.ToInt32(dt.Rows[j]["NoOfEmp"]);
                                    }
                                }
                            }

                        }
                    }
                }

                for (int i = 0; i < newTable.Rows.Count; i++)
                {
                    for (int k = 0; k < newTable.Columns.Count; k++)
                    {
                        if (newTable.Rows[i][k].Equals(DBNull.Value))
                        {
                            if (k != 0)
                            {
                                newTable.Rows[i][k] = 0;
                            }
                        }

                        if (k != 0 && k != 1 && i != newTable.Rows.Count - 1)
                        {
                            newTable.Rows[newTable.Rows.Count - 1][k] = (newTable.Rows[newTable.Rows.Count - 1][k].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[newTable.Rows.Count - 1][k].ToString())) + (Convert.ToInt32(newTable.Rows[i][k].Equals(DBNull.Value) ? 0 : Convert.ToInt32(newTable.Rows[i][k].ToString())));

                            if (newTable.Columns[k].ColumnName == "SubTotal")
                            {

                            }
                        }


                    }

                }

            }

            string html = "";

            html += "<table class='table table-bordered thead-dark table-striped table-hover' cellspacing='0' rules='all' border='1' style='border-collapse:collapse;'>";
            html += "<thead>";
            html += "<tr class='text-center'><th colspan='" + newTable.Columns.Count + "' style='background-color: #EBF1DE;'> <h3 class='mt-4'> Table-6: Department & grade wise staff </h3> </th></tr>";
            html += "<tr>";
            for (int k = 0; k < newTable.Columns.Count; k++)
            {
                html += "<th>" + newTable.Columns[k].ColumnName + "</th>";
            }
            html += "</tr>";
            html += "</thead>";


            for (int i = 0; i < newTable.Rows.Count; i++)
            {
                html += "<tr>";
                for (int k = 0; k < newTable.Columns.Count; k++)
                {
                    html += "<td>" + newTable.Rows[i][k].ToString() + "</td>";
                }
                html += "</tr>";
            }


            html += "</table>";


            departmentGradeWiseStaff.InnerHtml = html;
        }

        

    }

    public class Select
    {
        public int Value { get; set; }
        public string TextField { get; set; }
    }
    [WebMethod(EnableSession = true)]
    public static List<Select> LoadCompany()
    {
        List<Select> ComList = new List<Select>();

        using (DataTable dt = aSumm.GetComapnyNameList())
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Select select = new Select();
                    select.Value = int.Parse(dt.Rows[i]["Value"].ToString());
                    select.TextField = dt.Rows[i]["TextField"].ToString();

                    ComList.Add(select);

                }
            }
        }
        return ComList;
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue=="1")
        {
            lblCompany.InnerText = "SOCIAL MARKETING COMPANY";
        }

        else
        {

            lblCompany.InnerText = "SMC ENTERPRISE  LTD.";

        }
        LoadDeptAndGradeWiseNoofEmployee();
        LoadDeptAndTypeWiseNoofEmployee();
        //LoadDeptAndGenderWiseNoofEmployee();
        LoadGradeWiseNoofEmployee();
        Table_8();
        Table_FiledHeadOffice();
    }
}