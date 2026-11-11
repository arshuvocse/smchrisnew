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
using DAL.Chart_DAL;
using DAL.Permission_DAL;

public partial class ChartTest_Chart : System.Web.UI.Page
{
    ChartDAL aChartDal=new ChartDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        

        // Set Border Color
        
        if (!IsPostBack)
        {
            ReferenceDateTextBox.Attributes.Add("readonly", "readonly");
            GetCompany();
            Year();
            TrainingData(Convert.ToInt32(ddlCompany.SelectedValue));
            GetJoinleftData(Convert.ToInt32(ddlCompany.SelectedValue));
            ddlCompany.SelectedIndex = 0;
            for (int i = 0; i < lchk_Company.Items.Count; i++)
            {
                if (lchk_Company.Items[i].Value == ddlCompany.SelectedValue)
                {
                    lchk_Company.Items[i].Selected = true;
                }
                else
                {
                    lchk_Company.Items[i].Selected = false;
                }
            }
            Session["ChartCompId"] = CompanyId();
            GenderChart();
            TypeChart1("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
            TypeChart2("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
            BirthYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" +  " AND DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))     ");
            JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + " AND DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))     ");
            JoinedEmployeeBarChart("2019",
                "And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
            LeftEmployeeBarChart("2019", "And CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
            EmployeeGradeBarChart("1");

            LoadProbationInfo();
            LoadContractualInfo();
            LoadRetirementInfo();
           

            using (DataTable dt = aChartDal.GetDdlDepartmentByCompanyId((ddlCompany.SelectedValue)))
            {
                DeptDropDownList.DataSource = dt;
                DeptDropDownList.DataValueField = "Value";
                DeptDropDownList.DataTextField = "TextField";
                DeptDropDownList.DataBind();

                DptBarChart4DropDownList.DataSource = dt;
                DptBarChart4DropDownList.DataValueField = "Value";
                DptBarChart4DropDownList.DataTextField = "TextField";
                DptBarChart4DropDownList.DataBind();

                BarChart5DropDownList.DataSource = dt;
                BarChart5DropDownList.DataValueField = "Value";
                BarChart5DropDownList.DataTextField = "TextField";
                BarChart5DropDownList.DataBind();


                YearWiseDropDownList.DataSource = dt;
                YearWiseDropDownList.DataValueField = "Value";
                YearWiseDropDownList.DataTextField = "TextField";
                YearWiseDropDownList.DataBind();




                DptProbationDropDownList.DataSource = dt;
                DptProbationDropDownList.DataValueField = "Value";
                DptProbationDropDownList.DataTextField = "TextField";
                DptProbationDropDownList.DataBind();



                DptContractualDropDownList.DataSource = dt;
                DptContractualDropDownList.DataValueField = "Value";
                DptContractualDropDownList.DataTextField = "TextField";
                DptContractualDropDownList.DataBind();



                DptRetirementDropDownList.DataSource = dt;
                DptRetirementDropDownList.DataValueField = "Value";
                DptRetirementDropDownList.DataTextField = "TextField";
                DptRetirementDropDownList.DataBind();
            }

 LoadYear();
 
           
        }
        
    //    LeftEmployeeBarChartByDptYear(" CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + ParamYearWise());
        //GetCompany();
        //Year();
        //TrainingData(Convert.ToInt32(ddlCompany.SelectedValue));
        //GetJoinleftData(Convert.ToInt32(ddlCompany.SelectedValue));
        //ddlCompany.SelectedIndex = 0;
        //for (int i = 0; i < lchk_Company.Items.Count; i++)
        //{
        //    if (lchk_Company.Items[i].Value == ddlCompany.SelectedValue)
        //    {
        //        lchk_Company.Items[i].Selected = true;
        //    }
        //    else
        //    {
        //        lchk_Company.Items[i].Selected = false;
        //    }
        //}
        //Session["ChartCompId"] = CompanyId();
        //GenderChart();
        //TypeChart1("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        //TypeChart2("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        //BirthYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        //JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        //JoinedEmployeeBarChart("2019",
        //    "And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        //LeftEmployeeBarChart("2019", "And CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        //EmployeeGradeBarChart("1");

        //LoadProbationInfo();
        //LoadContractualInfo();


        //using (DataTable dt = aChartDal.GetDDLDepartmentByCompanyId(int.Parse(ddlCompany.SelectedValue)))
        //{
        //    DeptDropDownList.DataSource = dt;
        //    DeptDropDownList.DataValueField = "Value";
        //    DeptDropDownList.DataTextField = "TextField";
        //    DeptDropDownList.DataBind();
        //}
    }

    private void LoadYear()
    {
        FromYearDropDownList.Items.Insert(0, "Select Year.....");
        int index = 1;
        for (int i = 2020; i > 1970; i--)
        {
            FromYearDropDownList.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
            index++;
        }



        ToYearDropDownList.Items.Insert(0, "Select Year.....");
        int index2 = 1;
        for (int i = 2020; i > 1970; i--)
        {
            ToYearDropDownList.Items.Insert(index2, new ListItem(i.ToString(), i.ToString()));
            index2++;
        }
    }


    public void GenderChart()
    {
        try
        
        {
            DataTable dt = aChartDal.GetGender(" and  CompanyId =  " + ddlCompany.SelectedValue + "  AND DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))     ");
            
            PieChart1.ChartTitle = "";
            PieChart1.BackColor = Color.White;
            foreach (DataRow row in dt.Rows)
            {
                PieChart1.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                {
                    Category = row["Gender"].ToString(),
                    Data = Convert.ToInt32(row["Total"]),
                    PieChartValueColor = row["Gender"].ToString() == "Male" ? "#FA993B" : "#FE655C"

                });

               
               
            }
            int dd = 0;
            foreach (DataRow row in dt.Rows)
            {
                int ss = Convert.ToInt32(row["Total"]);
                 dd+=  ss ;
            }
           
            lblToGender.Text ="Total: "+ dd.ToString();
        }
        catch (Exception)
        {
            
           Response.Redirect("/Default.aspx");
        }
    }

    public void GetJoinleftData(int id)
    {
        DataTable dtdata = aChartDal.GetEmployeeJoinLeftRetireInfo(id);
        GridView1.DataSource = dtdata;
        GridView1.DataBind();

    }
    public void TypeChart1(string id)
    {
        DataTable dt = aChartDal.GetEmployeeType(id);
        if (dt.Rows.Count>0)
        {
            foreach (DataRow row in dt.Rows)
            {
                PieChart2.ChartTitle = " ";
                Random rnd = new Random();
                Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                string hex = randomColor.R.ToString("X2") + randomColor.G.ToString("X2") + randomColor.B.ToString("X2");
                hex = "#" + hex;
                //BackColor = randomColor;
                PieChart2.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                {
                    Category = row["EmpType"].ToString(),
                    Data = Convert.ToInt32(row["Total"]),
                    //PieChartValueColor = hex
                });



            }
            int dd = 0;
            foreach (DataRow row in dt.Rows)
            {
                int ss = Convert.ToInt32(row["Total"]);
                dd += ss;
            }

            lblEmpTypeRatio.Text = "Total: " + dd.ToString();
        }
        else
        {
            PieChart2.ChartTitle = string.Format("No Data Found!!");
            lblEmpTypeRatio.Text = "No Data Found!!";
        }
    }
    public void TypeChart2(string id)
    {
        //DataTable dt = aChartDal.GetEmployeeType(id);
        //foreach (DataRow row in dt.Rows)
        //{
        //    Random rnd = new Random();
        //    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        //    string hex = randomColor.R.ToString("X2") + randomColor.G.ToString("X2") + randomColor.B.ToString("X2");
        //    hex = "#" + hex;
        //    //BackColor = randomColor;
        //    PieChart3.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
        //    {
        //        Category = row["EmpType"].ToString(),
        //        Data = Convert.ToInt32(row["Total"]),
        //        //PieChartValueColor = hex
        //    });
        //}
    }
    public void JoinedEmployeeBarChart(string year,string param)
    {
        DataTable dt = aChartDal.GetJoinedEmployee(year,param);
        if (dt.Rows.Count>0)
        {
            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Math.Round(Convert.ToDecimal(Convert.ToInt32(dt.Rows[i][1])), 0);
            }
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, BarColor = "#0076b3" });
            
            BarChart1.CategoriesAxis = string.Join(",", x);
             
            BarChart1.ChartTitle = string.Format("Month Wise Employee Join");
            //if (x.Length > 3)
            //{
            //    BarChart1.ChartWidth = (x.Length * 40).ToString();
            //}
            
        }
        else
        {
            BarChart1.ChartTitle = string.Format("No Data Found!!");
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
    public void LeftEmployeeBarChart(string year,string param)
    {
        DataTable dt = aChartDal.GetJobLeftEmployee(year,param);
        if (dt.Rows.Count>0)
        {
            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            BarChart3.Series.Add(new AjaxControlToolkit.LineChartSeries { Data = y });
            BarChart3.CategoriesAxis = string.Join(",", x);
            BarChart3.ChartTitle = string.Format(" ");
            //if (x.Length > 3)
            //{
            //    BarChart3.ChartWidth = (x.Length * 40).ToString();
            //}
            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);
                dd += ss;
            }

            lblYearWiseSeperation.Text = "Total: " + dd.ToString();
        }
        else
        {
            BarChart3.ChartTitle = string.Format("No Data Found!!");
            lblYearWiseSeperation.Text = "No Data Found!!";
        }
    }

    public void LeftEmployeeBarChartByDptYear(string param)
    {
        DataTable dt = aChartDal.GetJobLeftEmployeeByDeptYear( param);
        if (dt.Rows.Count > 0)
        {
            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            BarChart3.Series.Add(new AjaxControlToolkit.LineChartSeries { Data = y });
            BarChart3.CategoriesAxis = string.Join(",", x);
            BarChart3.ChartTitle = string.Format(" ");
            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);
                dd += ss;
            }

            lblYearWiseSeperation.Text = "Total: " + dd.ToString();

        }
        else
        {
            BarChart3.ChartTitle = string.Format("No Data Found!!");
            lblYearWiseSeperation.Text = "No Data Found!!";
        }
    }
    public void BirthYearEmployeeBarChart(string param)
    {
        DataTable dt = aChartDal.GetBirthYearEmployee(param);
        if (dt.Rows.Count>0)
        {
              string[] x = new string[dt.Rows.Count];
        decimal[] y = new decimal[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = dt.Rows[i][0].ToString();
            y[i] = Convert.ToInt32(dt.Rows[i][1]);
        }
        BarChart4.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, BarColor = "#0076b3" });
        BarChart4.CategoriesAxis = string.Join(",", x);
        BarChart4.ChartTitle = string.Format(" ");


        int dd = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int ss = Convert.ToInt32(dt.Rows[i][1]);
            dd += ss;
        }

        lblAgeGroup.Text = "Total: " + dd.ToString();
        }
        else
        {
            BarChart4.ChartTitle = string.Format("No Data Found!!");
            lblAgeGroup.Text = "No Data Found!!";
        }
        //if (x.Length > 3)
        //{
        //    BarChart4.ChartWidth = (x.Length * 100).ToString();
        //}
    }
    public void JoiningYearEmployeeBarChart(string param)
    {
        DataTable dt = aChartDal.GetJoiningYearEmployee(param);
        if (dt.Rows.Count > 0)
        {
            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            BarChart5.Series.Add(new AjaxControlToolkit.BarChartSeries {Data = y, BarColor = "#0076b3"});
            BarChart5.CategoriesAxis = string.Join(",", x);
            BarChart5.ChartTitle = string.Format(" ");

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);
                dd += ss;
            }

            lblLengthofService.Text = "Total: " + dd.ToString();
        }
        else
        {
            BarChart5.ChartTitle = string.Format("No Data Found!!");
            lblLengthofService.Text = "No Data Found!!";
        }
        //if (x.Length > 3)
        //{
        //    BarChart5.ChartWidth = (x.Length * 70).ToString();
        //}
    }
    public void EmployeeGradeBarChart(string id)
    {
        DataTable dt = aChartDal.GetEmployeeGrade(" and  CompanyId =  " + ddlCompany.SelectedValue + "   ");
        if (dt.Rows.Count>0)
        {
            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            BarChart2.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, BarColor = "#7CB5EC" });
            BarChart2.CategoriesAxis = string.Join(",", x);

            BarChart2.BaseLineColor = Color.Blue.ToString();
            BarChart2.ChartTitle = string.Format(" ");

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);
                dd += ss;
            }

            lbltoGradewiseEmployee.Text = "Total: " + dd.ToString();
            
        }
        else
        {
            BarChart2.ChartTitle = string.Format("No Data Found!!");
            lbltoGradewiseEmployee.Text = "No Data Found!!";
        }
        //if (x .Length > 3)
        //{
        //    BarChart2.ChartWidth = (x.Length * 40).ToString();
        //}
    }
    public void GetCompany()
    {
        PermissionDAL aPermissionDal = new PermissionDAL();
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();

        ddlCompany.DataValueField = "CompanyId";
        ddlCompany.DataTextField = "ShortName";
        ddlCompany.DataSource = dtcomp;
        ddlCompany.DataBind();

    }
    public void Year()
    {
        for (int i = DateTime.Now.Year - 10; i <= DateTime.Now.Year + 10; i++)
        {
            ddlyear.Items.Add(i.ToString());
        }
    }

    public void TrainingData(int com)
    {
        DataTable dtdata = aChartDal.GetTrainingEmployee(com);
        loadGridView.DataSource = dtdata;
        loadGridView.DataBind();
    }
    protected void ddlyear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        JoinedEmployeeBarChart(ddlyear.SelectedItem.Text, " And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");


        Session["ChartCompId"] = CompanyId();
        GenderChart();
        TypeChart1("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        TypeChart2("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
       // BirthYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");

        LeftEmployeeBarChart("2019", "And CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        EmployeeGradeBarChart("1");
    }

    protected void lchk_Company_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ChartCompId"] = CompanyId();

      
    }

    protected void GridView1_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = " ";
            HeaderCell.BackColor = Color.FromName("#F5F5F5");
            HeaderCell.BorderColor = Color.FromName("#F5F5F5"); 

            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = " ";
            HeaderCell.BackColor = Color.FromName("#F5F5F5");
            HeaderCell.BorderColor = Color.FromName("#F5F5F5"); 


            HeaderCell.ColumnSpan = 1;
            
            HeaderGridRow.Cells.Add(HeaderCell);
           
          

            HeaderCell = new TableCell();
            HeaderCell.Text = "Joining";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BackColor = Color.GreenYellow;
            HeaderGridRow.Cells.Add(HeaderCell);
          

            HeaderCell = new TableCell();
            HeaderCell.Text = "Separation";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BackColor = Color.Salmon;

            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Retirement";
            HeaderCell.BackColor = Color.Red;

            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

        } 
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Value==ddlCompany.SelectedValue)
            {
                lchk_Company.Items[i].Selected = true;
            }
            else
            {
                lchk_Company.Items[i].Selected = false;
            }
        }

        TrainingData(Convert.ToInt32(ddlCompany.SelectedValue));
        GetJoinleftData(Convert.ToInt32(ddlCompany.SelectedValue));
       
        Session["ChartCompId"] = CompanyId();
        GenderChart();
        TypeChart1("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        TypeChart2("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        BirthYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + " AND DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))     ");
        JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + " AND DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))     ");
        JoinedEmployeeBarChart("2019",
            "And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        LeftEmployeeBarChart("2019", "And CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
        EmployeeGradeBarChart("1");

        LoadProbationInfo();
        LoadContractualInfo();
        LoadRetirementInfo();


        using (DataTable dt = aChartDal.GetDdlDepartmentByCompanyId((ddlCompany.SelectedValue)))
        {
            DeptDropDownList.DataSource = dt;
            DeptDropDownList.DataValueField = "Value";
            DeptDropDownList.DataTextField = "TextField";
            DeptDropDownList.DataBind();

            DptBarChart4DropDownList.DataSource = dt;
            DptBarChart4DropDownList.DataValueField = "Value";
            DptBarChart4DropDownList.DataTextField = "TextField";
            DptBarChart4DropDownList.DataBind();

            BarChart5DropDownList.DataSource = dt;
            BarChart5DropDownList.DataValueField = "Value";
            BarChart5DropDownList.DataTextField = "TextField";
            BarChart5DropDownList.DataBind();


            YearWiseDropDownList.DataSource = dt;
            YearWiseDropDownList.DataValueField = "Value";
            YearWiseDropDownList.DataTextField = "TextField";
            YearWiseDropDownList.DataBind();




            DptProbationDropDownList.DataSource = dt;
            DptProbationDropDownList.DataValueField = "Value";
            DptProbationDropDownList.DataTextField = "TextField";
            DptProbationDropDownList.DataBind();



            DptContractualDropDownList.DataSource = dt;
            DptContractualDropDownList.DataValueField = "Value";
            DptContractualDropDownList.DataTextField = "TextField";
            DptContractualDropDownList.DataBind();



            DptRetirementDropDownList.DataSource = dt;
            DptRetirementDropDownList.DataValueField = "Value";
            DptRetirementDropDownList.DataTextField = "TextField";
            DptRetirementDropDownList.DataBind();
        }
 //       Session["ChartCompId"] = CompanyId();
       
 //       TypeChart1("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
 //       TypeChart2("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
 //       BirthYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
 //       JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
 //       JoinedEmployeeBarChart("2019",
 //           "And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
 //       LeftEmployeeBarChart("2019", "And CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
 //       EmployeeGradeBarChart("1");

 //       GetJoinleftData(Convert.ToInt32(ddlCompany.SelectedValue));
 //       TrainingData(Convert.ToInt32(ddlCompany.SelectedValue));
 //       LoadProbationInfo();
 //       LoadContractualInfo();

 //       using (DataTable dt = aChartDal.GetDDLDepartmentByCompanyId(int.Parse(ddlCompany.SelectedValue)))
 //       {
 //           DeptDropDownList.DataSource = dt;
 //           DeptDropDownList.DataValueField = "Value";
 //           DeptDropDownList.DataTextField = "TextField";
 //           DeptDropDownList.DataBind();
 //       }
 //GenderChart();

    }



    private void LoadProbationInfo()
    {
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(Session["UserId"].ToString());
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {

                DataTable dtdata2 =
                    aChartDal.LoadProbationEmployeeData(
                        "  AND (ISNULL(EGI.ExtProbationPeriodDate,EGI.ProbationEndDate) BETWEEN GETDATE() AND DATEADD(month, DS.Prohibition, GETDATE()) ) AND EGI.CompanyId='" +
                        ddlCompany.SelectedValue + "'  ");


                ProbationGridView.DataSource = dtdata2;
                ProbationGridView.DataBind();


            }
            else
            {
                DataTable dtdata2 =
                   aChartDal.LoadProbationEmployeeDataAll(
                       " AND EGI.DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))  AND EGI.CompanyId='" +
                       ddlCompany.SelectedValue + "'  ");


                ProbationGridView.DataSource = dtdata2;
                ProbationGridView.DataBind();
            }
        }
        catch (Exception)
        {
            
            Response.Redirect("/Default.aspx");
        }
    }

    private void LoadContractualInfo()
    {
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(Session["UserId"].ToString());
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {

                DataTable dtdata2 = aChartDal.LoadContractualEmployeeData("  AND (ISNULL(EGI.ExtContractDate,EGI.ContractEndDate) BETWEEN GETDATE() AND DATEADD(month, DS.Contractual, GETDATE()) ) AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");


                ContractualGridView.DataSource = dtdata2;
                ContractualGridView.DataBind();

            }

            else
            {

                DataTable dtdata3 = aChartDal.LoadContractualEmployeeDataForAll(" AND EGI.DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))  AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");
                ContractualGridView.DataSource = dtdata3;
                ContractualGridView.DataBind();
            }
        }
        catch (Exception)
        {
            
           Response.Redirect("/Default.aspx");
        }
    }


    private void LoadRetirementInfo()
    {
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(Session["UserId"].ToString());
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {

                DataTable dtdata2 = aChartDal.LoadRetirementEmployeeData("  AND (ISNULL(EGI.ExtContractDate,EGI.ContractEndDate) BETWEEN GETDATE() AND DATEADD(month, DS.Contractual, GETDATE()) ) AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");


                RetirementGridView.DataSource = dtdata2;
                RetirementGridView.DataBind();

            }

            else
            {

                DataTable dtdata3 = aChartDal.LoadRetirementEmployeeDataForAll("  AND EGI.DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))   AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");
                RetirementGridView.DataSource = dtdata3;
                RetirementGridView.DataBind();
            }
        }
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            string attachment = "attachment; filename=EmployeeJoinleftDataInfo.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            GridView1.AllowPaging = false;



            
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
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Employee Join left List	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
             
        }
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void DeptDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = aChartDal.GetGender(" and  CompanyId =  " + ddlCompany.SelectedValue + "  AND DepartmentId =" + DeptDropDownList.SelectedValue + "    ");
        if (dt.Rows.Count>0)
        {
            PieChart1.ChartTitle = " ";
            PieChart1.BackColor = Color.White;
            foreach (DataRow row in dt.Rows)
            {
                PieChart1.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                {
                    Category = row["Gender"].ToString(),
                    Data = Convert.ToInt32(row["Total"]),
                    PieChartValueColor = row["Gender"].ToString() == "Male" ? "#FA993B" : "#FE655C"

                });
            }
            int dd = 0;
            foreach (DataRow row in dt.Rows)
            {
                int ss = Convert.ToInt32(row["Total"]);
                dd += ss;
            }

            lblToGender.Text = "Total: " + dd.ToString();
        }
        else
        {
            PieChart1.ChartTitle = "No Data Found!! ";
            lblToGender.Text = "No Data Found!!";
        }
         
           
    }

    protected void DptBarChart4DropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BirthYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + "  AND DepartmentId =" + DptBarChart4DropDownList.SelectedValue + "    ");
    }

    protected void DptBarBarChart5DropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (BarChart5DropDownList.SelectedIndex > 0)
        {

            if (ReferenceDateTextBox.Text != "")
            {
                JoiningYearEmployeeBarChartByJoiningDate("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + "  AND DepartmentId =" + BarChart5DropDownList.SelectedValue + "    ");
            }

            else
            {
                JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + "  AND DepartmentId =" + BarChart5DropDownList.SelectedValue + "    ");
            }

           
        }
        else
        {
            if (ReferenceDateTextBox.Text != "")
            {
                JoiningYearEmployeeBarChartByJoiningDate("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
            }

            else
            {
                JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + " AND DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))     ");
            }
        }
    }

    protected void ReferenceDateTextBox_OnTextChanged(object sender, EventArgs e)
    {


        if (BarChart5DropDownList.SelectedIndex > 0)
        {

            if (ReferenceDateTextBox.Text != "")
            {
                JoiningYearEmployeeBarChartByJoiningDate("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + "  AND DepartmentId =" + BarChart5DropDownList.SelectedValue + "    ");
            }

            else
            {
                JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + "  AND DepartmentId =" + BarChart5DropDownList.SelectedValue + "    ");
            }


        }
        else
        {
            if (ReferenceDateTextBox.Text != "")
            {
                JoiningYearEmployeeBarChartByJoiningDate("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")");
            }

            else
            {
                JoiningYearEmployeeBarChart("And tblEmpGeneralInfo.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + " AND DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + ddlCompany.SelectedValue + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))     ");
            }
        }

       

       
    }


    public void JoiningYearEmployeeBarChartByJoiningDate(string param)
    {
        DataTable dt = aChartDal.GetJoiningYearEmployeeByJoiningDate(param, ReferenceDateTextBox.Text.Trim());
        if (dt.Rows.Count>0)
        {
            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            BarChart5.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, BarColor = "#0076b3" });
            BarChart5.CategoriesAxis = string.Join(",", x);
            BarChart5.ChartTitle = string.Format(" ");
            //if (x.Length > 3)
            //{
            //    BarChart5.ChartWidth = (x.Length * 70).ToString();
            //}

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);
                dd += ss;
            }

            lblLengthofService.Text = "Total: " + dd.ToString();
        }
        else
        {
            BarChart5.ChartTitle = string.Format("No Data Found!! ");
            lblLengthofService.Text = "No Data Found!! ";
        }
    }

    protected void YearWiseDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LeftEmployeeBarChartByDptYear(" tblEmployeeJobLeft.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + ParamYearWise());
    }

    protected void FromYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LeftEmployeeBarChartByDptYear(" tblEmployeeJobLeft.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + ParamYearWise());
    }

    protected void ToYearDropDownList1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LeftEmployeeBarChartByDptYear(" tblEmployeeJobLeft.CompanyId IN (" + Session["ChartCompId"].ToString() + ")" + ParamYearWise());
    }

    private string ParamYearWise()
    {
        string parameter = "    ";
        if (YearWiseDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  tblEmpGeneralInfo.DepartmentId= " + YearWiseDropDownList.SelectedValue;
        }

        if (FromYearDropDownList.SelectedIndex > 0 && ToYearDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + " AND YEAR(JobLeftDate) BETWEEN '" + FromYearDropDownList.SelectedItem.Text + "' AND '" + ToYearDropDownList.SelectedItem.Text + "' ";
        }
        //if (FromYearDropDownList.SelectedIndex > 0 && ToYearDropDownList.Text == string.Empty)
        //{
        //    parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("yyyy") + "' ";
        //}

        //if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        //{
        //    parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        //}
        return parameter;
    }

    protected void btnUpcommingTrainingLinkButton_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=UpcommingTrainingInfo.xls";
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

           
           this.TrainingData(Convert.ToInt32(ddlCompany.SelectedValue));

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
   <h3>Upcomming Training List	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
             
        }
    }

    protected void Retirement_Click(object sender, EventArgs e)
    {
        if (RetirementGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=RetirementListInfo.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            RetirementGridView.AllowPaging = false;



            //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;


           // this.LoadRetirementInfo();

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            RetirementGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            RetirementGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in RetirementGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in RetirementGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            RetirementGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Retirement List	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            
        }
    }

    protected void ProbationLinkButton_Click(object sender, EventArgs e)
    {
        if (ProbationGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=ProbationEmployeeListInfo.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            ProbationGridView.AllowPaging = false;



            //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;


             
           
           

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            ProbationGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            ProbationGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in ProbationGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in ProbationGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            ProbationGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Probation Employee List Info	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            
        }
    }

    protected void Contractual_Click(object sender, EventArgs e)
    {
        if (ContractualGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=ContractualEmployeeListInfo.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            ContractualGridView.AllowPaging = false;



            //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
            //            false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;


            

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            ContractualGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            ContractualGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in ContractualGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in ContractualGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            ContractualGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Contractual Employee List</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            
        }
    }

    protected void DptProbationDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(Session["UserId"].ToString());
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {

                DataTable dtdata2 =
                    aChartDal.LoadProbationEmployeeData(
                        "  AND (ISNULL(EGI.ExtProbationPeriodDate,EGI.ProbationEndDate) BETWEEN GETDATE() AND DATEADD(month, DS.Prohibition, GETDATE()) ) AND EGI.CompanyId='" +
                        ddlCompany.SelectedValue + "'  ");


                ProbationGridView.DataSource = dtdata2;
                ProbationGridView.DataBind();


            }
            else
            {
                DataTable dtdata2 =
                   aChartDal.LoadProbationEmployeeDataAll(
                       " AND EGI.DepartmentId  ='" + DptProbationDropDownList.SelectedValue+ "'  AND EGI.CompanyId='" +
                       ddlCompany.SelectedValue + "'  ");


                ProbationGridView.DataSource = dtdata2;
                ProbationGridView.DataBind();
            }
        }
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        } 
    }

    protected void DptContractualDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(Session["UserId"].ToString());
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {

                DataTable dtdata2 = aChartDal.LoadContractualEmployeeData("  AND (ISNULL(EGI.ExtContractDate,EGI.ContractEndDate) BETWEEN GETDATE() AND DATEADD(month, DS.Contractual, GETDATE()) ) AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");


                ContractualGridView.DataSource = dtdata2;
                ContractualGridView.DataBind();

            }

            else
            {

                DataTable dtdata3 = aChartDal.LoadContractualEmployeeDataForAll(" AND EGI.DepartmentId='" + DptContractualDropDownList.SelectedValue+ "'  AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");
                ContractualGridView.DataSource = dtdata3;
                ContractualGridView.DataBind();
            }
        }
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        }
    }

    protected void DptRetirementDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(Session["UserId"].ToString());
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {

                DataTable dtdata2 = aChartDal.LoadRetirementEmployeeData("  AND (ISNULL(EGI.ExtContractDate,EGI.ContractEndDate) BETWEEN GETDATE() AND DATEADD(month, DS.Contractual, GETDATE()) ) AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");


                RetirementGridView.DataSource = dtdata2;
                RetirementGridView.DataBind();

            }

            else
            {

                DataTable dtdata3 = aChartDal.LoadRetirementEmployeeDataForAll("  AND EGI.DepartmentId ='" + DptRetirementDropDownList.SelectedValue+ "'   AND EGI.CompanyId='" + ddlCompany.SelectedValue + "'   ");
                RetirementGridView.DataSource = dtdata3;
                RetirementGridView.DataBind();
            }
        }
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        }
    }
}