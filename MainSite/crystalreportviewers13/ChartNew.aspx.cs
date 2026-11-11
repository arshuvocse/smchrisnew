using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.Chart_DAL;
using DAL.Inverview_DAL;
using DAL.UserProfileDAL;
using DAO.HRIS_DAO;

public partial class ChartTest_ChartNew : System.Web.UI.Page
{
    private static string usserId = "";
    KPIDashboardDAL aDAL = new KPIDashboardDAL();

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
            BindContracualData();
            BindRetairmentData();
            BindDummyData();
           // EmployeeJobLeftInfo();
          //  EmployeeProbationEmployeeListInfo();
         //   LoadRetirementInfo();
             TrainingData(1)
            ;
          GetJoinleftData(1);
      //      LoadYear();



            //if (DateTime.Now != null)
            //{


            //    if (CheckStartEndDateExistOrNot(DateTime.Now, DateTime.Now) == true)
            //    {
            //        GetKPISelfStatus(Session["EmpInfoId"].ToString(), ddlFinancialYear.SelectedValue);
            //    }

            //}
        }
        
    }

    private void GetKPISelfStatus(string EmpID, string FinId)
    {
        DataTable dtKPISelfStatus = aDAL.GetKPISelfStatusDAL(EmpID, FinId);

        const int rowIndex = 0;

        if (dtKPISelfStatus.Rows.Count > 0)
        {
            lblKPISelfStatus.Text = "Done";
        }
        else
        {
            lblKPISelfStatus.Text = "Pending";
        }



        DataTable dtApppSelfStatus = aDAL.GetApprisalSelfStatusDAL(EmpID, FinId);



        if (dtApppSelfStatus.Rows.Count > 0)
        {
            lblApprisalSelfStatus.Text = "Done";
        }
        else
        {
            lblApprisalSelfStatus.Text = "Pending";
        }



        DataTable dtKPIStatus = aDAL.GetKPIStatusDAL(EmpID, FinId);
        if (dtKPIStatus.Rows.Count > 0)
        {
            lblKpiDone.Text = dtKPIStatus.Rows.Count.ToString();
        }

        else
        {
            lblKpiDone.Text = "0";
        }





        DataTable dtKPIStatusPending = aDAL.GetKPIStatusDALPending(EmpID, FinId);
        if (dtKPIStatusPending.Rows.Count > 0)
        {
            lblKPIPending.Text = dtKPIStatusPending.Rows.Count.ToString();
        }

        else
        {
            lblKPIPending.Text = "0";
        }





        DataTable dtAppppStatu = aDAL.GetAppppStatusDAL(EmpID, FinId);
        if (dtAppppStatu.Rows.Count > 0)
        {
            lblAppDone.Text = dtAppppStatu.Rows.Count.ToString();
        }
        else
        {
            lblAppDone.Text = "0";
        }



        DataTable dtAppppStatuPen = aDAL.GetAppppStatusDALPend(EmpID, FinId);
        if (dtAppppStatuPen.Rows.Count > 0)
        {
            lblAppPending.Text = dtAppppStatuPen.Rows.Count.ToString();
        }
        else
        {
            lblAppPending.Text = "0";
        }
    }

    private DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();

    private bool CheckStartEndDateExistOrNot(DateTime Start, DateTime End)
    {
        bool status = false;
        string COMID = Session["CompanyId"].ToString();
        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(Start, End, COMID);

        if (dataTable.Rows.Count > 0)
        {
            ddlFinancialYear.SelectedValue = dataTable.Rows[0]["FinancialYearId"].ToString();
            status = true;
        }

        return status;
    }
    public void GetJoinleftData(int id)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Month");

        dt.Columns.Add("PermJoin");

        dt.Columns.Add("ContJoin");
        dt.Columns.Add("PermContJoin");
        dt.Columns.Add("LeftPer");
        dt.Columns.Add("LeftPermCont");
        dt.Columns.Add("RetirePerm");
       



        dt.Rows.Add();

        GridView1.DataSource = dt;

        GridView1.DataBind();
       

    }
    public void TrainingData(int com)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("TrainingMasterId");
        dt.Columns.Add("TrainingTitle");

        dt.Columns.Add("TrainingStart");

        dt.Columns.Add("TrainingEnd");

    

        dt.Rows.Add();

        loadGridView.DataSource = dt;

        loadGridView.DataBind();
         
    }
    [WebMethod(EnableSession = true)]
    public static ProductDetails[] LoadRetirementInfo(string comId, string selecteddiv)
    {
        List<ProductDetails> RetirementData = new List<ProductDetails>();
        ChartDAL aChartDal = new ChartDAL();
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(usserId);
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {
           string dd=     dtuser.Rows[0]["Retirement"].ToString();
                DataTable dtdata2 =
                    aChartDal.LoadRetirementEmployeeDataForAll(
                        "  AND (EGI.DateOfRetirement BETWEEN GETDATE() AND DATEADD(month, " + dd + ", GETDATE()) ) AND EGI.CompanyId=" +
                        comId + "     and  EGI.DivisionId =" + selecteddiv);


                if (dtdata2.Rows.Count > 0)
                {



                    foreach (DataRow DR in dtdata2.Rows)
                    {

                        ProductDetails objProduct = new ProductDetails();

                        objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                        objProduct.EmpName = DR["EmpName"].ToString();

                        objProduct.DepartmentName = DR["DepartmentName"].ToString();

                        objProduct.Designation = DR["Designation"].ToString();

                        objProduct.DateOfRetirement = DR["DateOfRetirement"].ToString();
                        objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                        RetirementData.Add(objProduct);

                    }

                    
                }

            }

            else
            {

                DataTable dtdata3 = aChartDal.LoadRetirementEmployeeDataForAll("  AND EGI.DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " + comId + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))   AND EGI.CompanyId='" + comId + "' and  EGI.DivisionId =" + selecteddiv + " AND (EGI.DateOfRetirement BETWEEN GETDATE() AND DATEADD(month, " + 1 + ", GETDATE()) ) ");

                if (dtdata3.Rows.Count > 0)
                {



                    foreach (DataRow DR in dtdata3.Rows)
                    {

                        ProductDetails objProduct = new ProductDetails();

                        objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                        objProduct.EmpName = DR["EmpName"].ToString();

                        objProduct.DepartmentName = DR["DepartmentName"].ToString();

                        objProduct.Designation = DR["Designation"].ToString();

                        objProduct.DateOfRetirement = DR["DateOfRetirement"].ToString();
                        objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                        RetirementData.Add(objProduct);

                    }

                    
                }
            }
           
        }
        catch (Exception)
        {

          
        }
        return RetirementData.ToArray();
    }



    [WebMethod(EnableSession = true)]
    public static ProductDetails[] LoadRetirementInfoDtp(string comId, string selecteddiv, string selectedDept)
    {
        List<ProductDetails> RetirementData = new List<ProductDetails>();
        ChartDAL aChartDal = new ChartDAL();
        try
        {
            DataTable dtuser = aChartDal.GetUserDashBoardSetting(usserId);
            const int rowIndex = 0;
            if (dtuser.Rows.Count > 0)
            {
                string dd = dtuser.Rows[0]["Retirement"].ToString();

                DataTable dtdata2 =
                    aChartDal.LoadRetirementEmployeeDataForAll(
                        "  AND (EGI.DateOfRetirement BETWEEN GETDATE() AND DATEADD(month, " + dd + ", GETDATE()) ) AND EGI.CompanyId=" +
                        comId + "     and  EGI.DivisionId =" + selecteddiv + "     and  EGI.DepartmentId =" + selectedDept);


                if (dtdata2.Rows.Count > 0)
                {



                    foreach (DataRow DR in dtdata2.Rows)
                    {

                        ProductDetails objProduct = new ProductDetails();

                        objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                        objProduct.EmpName = DR["EmpName"].ToString();

                        objProduct.DepartmentName = DR["DepartmentName"].ToString();

                        objProduct.Designation = DR["Designation"].ToString();

                        objProduct.DateOfRetirement = DR["DateOfRetirement"].ToString();
                        objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                        RetirementData.Add(objProduct);

                    }


                }

            }

            else
            {

                DataTable dtdata3 = aChartDal.LoadRetirementEmployeeDataForAll("  AND EGI.DepartmentId IN (SELECT DepartmentId   FROM dbo.tblDepartment dept WHERE dept.CompanyId= " +
                        comId + " AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "'))   AND EGI.CompanyId='" + comId + "' and  EGI.DivisionId =" + selecteddiv + "     and  EGI.DepartmentId =" + selectedDept + " AND (EGI.DateOfRetirement BETWEEN GETDATE() AND DATEADD(month, " +1 + ", GETDATE()) ) ");

                if (dtdata3.Rows.Count > 0)
                {



                    foreach (DataRow DR in dtdata3.Rows)
                    {

                        ProductDetails objProduct = new ProductDetails();

                        objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                        objProduct.EmpName = DR["EmpName"].ToString();

                        objProduct.DepartmentName = DR["DepartmentName"].ToString();

                        objProduct.Designation = DR["Designation"].ToString();

                        objProduct.DateOfRetirement = DR["DateOfRetirement"].ToString();
                        objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                        RetirementData.Add(objProduct);

                    }


                }
            }

        }
        catch (Exception)
        {


        }
        return RetirementData.ToArray();
    }

    public class ProductDetails
    {

        public string EmpMasterCode { get; set; }

        public string EmpName { get; set; }

        public string DepartmentName { get; set; }

        public string Designation { get; set; }

        public string ProbationEndDate { get; set; }
        public string ContractEndDate { get; set; } 
        public string DateOfRetirement { get; set; }
        public string DateOfJoin { get; set; }

    }

    public class LeftJoinDtls
    {

        public string Month { get; set; }

        public string PermJoin { get; set; }

        public string ContJoin { get; set; }

        public string PermContJoin { get; set; }

        public string LeftPer { get; set; }
        public string LeftCont { get; set; }
        public string LeftPermCont { get; set; }
        public string RetirePerm { get; set; }
        public string RetireCont { get; set; }
        public string RetirePermCont { get; set; }

    }

    public void BindDummyData()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("EmpMasterCode");

        dt.Columns.Add("EmpName");

        dt.Columns.Add("DepartmentName");

        dt.Columns.Add("Designation");
        dt.Columns.Add("ProbationEndDate");

        dt.Columns.Add("DateOfJoin");

        dt.Rows.Add();

        ProbationGridView.DataSource = dt;

        ProbationGridView.DataBind();

    }


    public void BindContracualData()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("EmpMasterCode");

        dt.Columns.Add("EmpName");

        dt.Columns.Add("DepartmentName");

        dt.Columns.Add("Designation");
        dt.Columns.Add("ContractEndDate");

        dt.Columns.Add("DateOfJoin");

        dt.Rows.Add();

        ContractualGridView.DataSource = dt;

        ContractualGridView.DataBind();

    }

    public void BindRetairmentData()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("EmpMasterCode");

        dt.Columns.Add("EmpName");

        dt.Columns.Add("DepartmentName");

        dt.Columns.Add("Designation");
        dt.Columns.Add("DateOfRetirement");

        dt.Columns.Add("DateOfJoin");

        dt.Rows.Add();

        RetirementGridView.DataSource = dt;

        RetirementGridView.DataBind();

    }

     [WebMethod(EnableSession = true)]
    public static ProductDetails[] EmployeeProbationEmployeeListInfo(string comId)
    {
        List<ProductDetails> MyData = new List<ProductDetails>();
        ChartDAL aChartDal = new ChartDAL();
        using (DataTable dt = aChartDal.LoadProbationEmployeeData(" and  EGI.CompanyId IN (" + comId + ") "))
        {
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow DR in dt.Rows)
                {

                    ProductDetails objProduct = new ProductDetails();

                    objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                    objProduct.EmpName = DR["EmpName"].ToString();

                    objProduct.DepartmentName = DR["DepartmentName"].ToString();

                    objProduct.Designation = DR["Designation"].ToString();

                    objProduct.ProbationEndDate = DR["ProbationEndDate"].ToString();
                    objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                    MyData.Add(objProduct);

                }

                return MyData.ToArray();
            }
        }

        return MyData.ToArray();
        //using (DataTable dtss = aChartDal.LoadContractualEmployeeData(null))
        //{
        //    if (dtss.Rows.Count > 0)
        //    {



        //        ContractualGridView.DataSource = dtss;
        //        ContractualGridView.DataBind();
        //    }
        //}
    }


     [WebMethod(EnableSession = true)]
     public static ProductDetails[] EmployeeProbationEmployeeListInfoFordiv(string comId, string selecteddiv="''")
     {
         List<ProductDetails> MyData = new List<ProductDetails>();
         ChartDAL aChartDal = new ChartDAL();
         using (DataTable dt = aChartDal.LoadProbationEmployeeData(" and  EGI.CompanyId IN (" + comId + ") and  EGI.DivisionId =" + selecteddiv + ""))
         {
             if (dt.Rows.Count > 0)
             {



                 foreach (DataRow DR in dt.Rows)
                 {

                     ProductDetails objProduct = new ProductDetails();

                     objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                     objProduct.EmpName = DR["EmpName"].ToString();

                     objProduct.DepartmentName = DR["DepartmentName"].ToString();

                     objProduct.Designation = DR["Designation"].ToString();

                     objProduct.ProbationEndDate = DR["ProbationEndDate"].ToString();
                     objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                     MyData.Add(objProduct);

                 }

                 return MyData.ToArray();
             }
         }

         return MyData.ToArray();
         //using (DataTable dtss = aChartDal.LoadContractualEmployeeData(null))
         //{
         //    if (dtss.Rows.Count > 0)
         //    {



         //        ContractualGridView.DataSource = dtss;
         //        ContractualGridView.DataBind();
         //    }
         //}
     }




     [WebMethod(EnableSession = true)]
     public static ProductDetails[] EmployeeProbationEmployeeListInfoForDept(string comId, string selecteddiv, string selectedDept)
     {
         List<ProductDetails> MyData = new List<ProductDetails>();
         ChartDAL aChartDal = new ChartDAL();
         using (DataTable dt = aChartDal.LoadProbationEmployeeData(" and  EGI.CompanyId IN (" + comId + ") and  EGI.DivisionId =" + selecteddiv + " and  EGI.DepartmentId =" + selectedDept + ""))
         {
             if (dt.Rows.Count > 0)
             {



                 foreach (DataRow DR in dt.Rows)
                 {

                     ProductDetails objProduct = new ProductDetails();

                     objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                     objProduct.EmpName = DR["EmpName"].ToString();

                     objProduct.DepartmentName = DR["DepartmentName"].ToString();

                     objProduct.Designation = DR["Designation"].ToString();

                     objProduct.ProbationEndDate = DR["ProbationEndDate"].ToString();
                     objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                     MyData.Add(objProduct);

                 }

                 return MyData.ToArray();
             }
         }

         return MyData.ToArray();
         //using (DataTable dtss = aChartDal.LoadContractualEmployeeData(null))
         //{
         //    if (dtss.Rows.Count > 0)
         //    {



         //        ContractualGridView.DataSource = dtss;
         //        ContractualGridView.DataBind();
         //    }
         //}
     }

    



    [WebMethod(EnableSession = true)]
    public static LeftJoinDtls[] EmployeeJoinLeftRetireInfo(string comId)
    {
        List<LeftJoinDtls> leftjjjjzxz = new List<LeftJoinDtls>();
        ChartDAL aChartDal = new ChartDAL();
        using (DataTable dt = aChartDal.GetEmployeeJoinLeftRetireInfo(Convert.ToInt32(comId)))
       
        {
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow DR in dt.Rows)
                {

                    LeftJoinDtls objLeft = new LeftJoinDtls();

                    objLeft.Month = DR["Month"].ToString();

                    objLeft.PermJoin = DR["PermJoin"].ToString();

                    objLeft.ContJoin = DR["ContJoin"].ToString();

                    objLeft.PermContJoin = DR["PermContJoin"].ToString();

                    objLeft.LeftPer = DR["LeftPer"].ToString();
                    objLeft.LeftPermCont = DR["LeftPermCont"].ToString();
                    objLeft.RetirePerm = DR["RetirePerm"].ToString();
                    objLeft.RetireCont = DR["RetireCont"].ToString();
                    objLeft.RetirePermCont = DR["RetirePermCont"].ToString();


                    leftjjjjzxz.Add(objLeft);

                }

                return leftjjjjzxz.ToArray();
            }
        }

        return leftjjjjzxz.ToArray();
       
    }



    [WebMethod(EnableSession = true)]
    public static ProductDetails[] EmployeeProbationContractualInfoFordiv(string comId, string selecteddiv)
    {
        List<ProductDetails> ContractualData = new List<ProductDetails>();
        ChartDAL aChartDal = new ChartDAL();
        using (DataTable dt = aChartDal.LoadContractualEmployeeData(" and  EGI.CompanyId IN (" + comId + ") and  EGI.DivisionId =" + selecteddiv + ""))
        {
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow DR in dt.Rows)
                {

                    ProductDetails objProduct = new ProductDetails();

                    objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                    objProduct.EmpName = DR["EmpName"].ToString();

                    objProduct.DepartmentName = DR["DepartmentName"].ToString();

                    objProduct.Designation = DR["Designation"].ToString();

                    objProduct.ContractEndDate = DR["ContractEndDate"].ToString();
                    objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                    ContractualData.Add(objProduct);

                }

                return ContractualData.ToArray();
            }
        }

        return ContractualData.ToArray();
        //using (DataTable dtss = aChartDal.LoadContractualEmployeeData(null))
        //{
        //    if (dtss.Rows.Count > 0)
        //    {



        //        ContractualGridView.DataSource = dtss;
        //        ContractualGridView.DataBind();
        //    }
        //}
    }


    [WebMethod(EnableSession = true)]
    public static ProductDetails[] EmployeeProbationContractualInfoForDpt(string comId, string selecteddiv, string selectedDept)
    {
        List<ProductDetails> ContractualData = new List<ProductDetails>();
        ChartDAL aChartDal = new ChartDAL();
        using (DataTable dt = aChartDal.LoadContractualEmployeeData(" and  EGI.CompanyId IN (" + comId + ") and  EGI.DivisionId =" + selecteddiv + " and  EGI.DepartmentId =" + selectedDept + ""))
        {
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow DR in dt.Rows)
                {

                    ProductDetails objProduct = new ProductDetails();

                    objProduct.EmpMasterCode = DR["EmpMasterCode"].ToString();

                    objProduct.EmpName = DR["EmpName"].ToString();

                    objProduct.DepartmentName = DR["DepartmentName"].ToString();

                    objProduct.Designation = DR["Designation"].ToString();

                    objProduct.ContractEndDate = DR["ContractEndDate"].ToString();
                    objProduct.DateOfJoin = DR["DateOfJoin"].ToString();



                    ContractualData.Add(objProduct);

                }

                return ContractualData.ToArray();
            }
        }

        return ContractualData.ToArray();
        //using (DataTable dtss = aChartDal.LoadContractualEmployeeData(null))
        //{
        //    if (dtss.Rows.Count > 0)
        //    {



        //        ContractualGridView.DataSource = dtss;
        //        ContractualGridView.DataBind();
        //    }
        //}
    }

    [WebMethod]
    public static string GetCustomers()
    {


        string constr = ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRIS_SMC"].ConnectionString;
          using (SqlConnection con = new SqlConnection(constr))
          {
              using (SqlCommand cmd = new SqlCommand())
              {
                  cmd.CommandText = @"SELECT tbltemp.Month," +
      "   SUM(tbltemp.PermJoin)PermJoin," +
      "   SUM(tbltemp.ContJoin)ContJoin," +
     "    SUM(tbltemp.PermContJoin)PermContJoin," +
     "    SUM(tbltemp.LeftPer)LeftPer," +
     "    SUM(tbltemp.LeftCont)LeftCont," +
     "    SUM(tbltemp.LeftPermCont)LeftPermCont," +
    "     SUM(tbltemp.RetirePerm)RetirePerm," +
     "    SUM(tbltemp.RetireCont)RetireCont," +
     "    SUM(tbltemp.RetirePermCont)RetirePermCont," +
     "    (SUM(tbltemp.PermJoin)+" +
    "     SUM(tbltemp.ContJoin)+" +
    "     SUM(tbltemp.PermContJoin)+" +
    "     SUM(tbltemp.LeftPer)+" +
    "     SUM(tbltemp.LeftCont)+" +
    "     SUM(tbltemp.LeftPermCont)+" +
     "    SUM(tbltemp.RetirePerm)+" +
    "     SUM(tbltemp.RetireCont)+" +
     "    SUM(tbltemp.RetirePermCont))Total,tbltemp.MonthNo FROM (SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,COUNT(*)PermJoin,'0'ContJoin,'0'PermContJoin," +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo WHERE   EmpTypeId='1' AND  year(DateOfJoin)='2019' AND CompanyId='" + 1 + "' GROUP BY " +
 " FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin) " +
 " UNION ALL " +
 " SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,'0'PermJoin,COUNT(*)ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo WHERE  EmpTypeId='2' AND  year(DateOfJoin)='2019' AND CompanyId='" + 1 + "' GROUP BY " +
 " FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin) " +
 " UNION ALL " +
 " SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,'0'PermJoin,'0'ContJoin,COUNT(*)PermContJoin," +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo WHERE IsProgramContractual='1' AND  year(DateOfJoin)='2019' AND CompanyId='" + 1 + "' GROUP BY " +
 " FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin) " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  EmpTypeId='1' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,COUNT(*)LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId " +
 "  WHERE  EmpTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,COUNT(*)LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE IsProgramContractual='1' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " +
 " UNION ALL " +


 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,COUNT(*)RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  EmpTypeId='1' AND JobLeftTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,COUNT(*)RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId " +
 "  WHERE  EmpTypeId='2' AND JobLeftTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "'  " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,COUNT(*)RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  IsProgramContractual='1' AND JobLeftTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "'  " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM') )AS tbltemp GROUP BY tbltemp.Month,tbltemp.MonthNo  " +
 " UNION ALL " +

 " SELECT 'Total'Month,SUM(tbltemmm.PermJoin)PermJoin, " +
      "   SUM(tbltemmm.ContJoin)ContJoin, " +
      "   SUM(tbltemmm.PermContJoin)PermContJoin, " +
     "    SUM(tbltemmm.LeftPer)LeftPer, " +
     "    SUM(tbltemmm.LeftCont)LeftCont, " +
      "   SUM(tbltemmm.LeftPermCont)LeftPermCont, " +
      "   SUM(tbltemmm.RetirePerm)RetirePerm, " +
     "    SUM(tbltemmm.RetireCont)RetireCont, " +
     "    SUM(tbltemmm.RetirePermCont)RetirePermCont, " +
     "    (SUM(tbltemmm.PermJoin)+ " +
     "    SUM(tbltemmm.ContJoin)+ " +
      "   SUM(tbltemmm.PermContJoin)+ " +
      "   SUM(tbltemmm.LeftPer)+ " +
      "   SUM(tbltemmm.LeftCont)+ " +
      "   SUM(tbltemmm.LeftPermCont)+ " +
      "   SUM(tbltemmm.RetirePerm)+ " +
     "    SUM(tbltemmm.RetireCont)+ " +
      "   SUM(tbltemmm.RetirePermCont))Total,'13'MonthNo FROM (SELECT  " +
      "   SUM(tbltemp.PermJoin)PermJoin, " +
      "   SUM(tbltemp.ContJoin)ContJoin, " +
      "   SUM(tbltemp.PermContJoin)PermContJoin, " +
      "   SUM(tbltemp.LeftPer)LeftPer, " +
      "   SUM(tbltemp.LeftCont)LeftCont, " +
      "   SUM(tbltemp.LeftPermCont)LeftPermCont, " +
      "   SUM(tbltemp.RetirePerm)RetirePerm, " +
      "   SUM(tbltemp.RetireCont)RetireCont, " +
      "   SUM(tbltemp.RetirePermCont)RetirePermCont, " +
      "   (SUM(tbltemp.PermJoin)+ " +
      "   SUM(tbltemp.ContJoin)+ " +
      "   SUM(tbltemp.PermContJoin)+ " +
      "   SUM(tbltemp.LeftPer)+ " +
      "   SUM(tbltemp.LeftCont)+ " +
      "   SUM(tbltemp.LeftPermCont)+ " +
      "   SUM(tbltemp.RetirePerm)+ " +
      "   SUM(tbltemp.RetireCont)+ " +
      "   SUM(tbltemp.RetirePermCont))Total FROM (SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,COUNT(*)PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo WHERE  EmpTypeId='1'  and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin) " +
 " UNION ALL " +
 " SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,'0'PermJoin,COUNT(*)ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo WHERE  EmpTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin) " +
 " UNION ALL " +
 " SELECT MONTH(DateOfJoin)MonthNo,FORMAT(DateOfJoin,'MMMM')Month,'0'PermJoin,'0'ContJoin,COUNT(*)PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmpGeneralInfo WHERE  IsProgramContractual='1' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY FORMAT(DateOfJoin,'MMMM') ,MONTH(DateOfJoin) " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  EmpTypeId='1' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,COUNT(*)LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId " +
 "  WHERE  EmpTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " +
 " UNION ALL " +
 " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +
 " '0'LeftPer,'0'LeftCont,COUNT(*)LeftPermCont,'0'RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE IsProgramContractual='1' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " + " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " + " UNION ALL " + " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +" COUNT(*)LeftPer,'0'LeftCont,'0'LeftPermCont,COUNT(*) RetirePerm,'0'RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " +
 " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  EmpTypeId='1' AND JobLeftTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +  " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " + " UNION ALL " + " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " +  " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,COUNT(*)RetireCont,'0'RetirePermCont  FROM dbo.tblEmployeeJobLeft " + " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId " + "  WHERE  EmpTypeId='2' AND JobLeftTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " + " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM')  " + " UNION ALL " + " SELECT MONTH(JobLeftDate)MonthNo,FORMAT(JobLeftDate,'MMMM')Month,'0'PermJoin,'0'ContJoin,'0'PermContJoin, " + " '0'LeftPer,'0'LeftCont,'0'LeftPermCont,'0'RetirePerm,'0'RetireCont,COUNT(*)RetirePermCont  FROM dbo.tblEmployeeJobLeft " + " LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE  IsProgramContractual='1' AND JobLeftTypeId='2' and year(DateOfJoin)='2019' AND tblEmpGeneralInfo.CompanyId='" + 1 + "' " +
 " GROUP BY MONTH(JobLeftDate),FORMAT(JobLeftDate,'MMMM') )AS tbltemp GROUP BY tbltemp.Month,tbltemp.MonthNo ) AS tbltemmm";
                  cmd.Connection = con;
                  using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                  {
                      DataSet ds = new DataSet();
                      sda.Fill(ds);
                      return ds.GetXml();
                  }
              }
          }
    }




     [WebMethod]
     public static string GetProbationEmployeeList()
     {


         string constr = ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRIS_SMC"].ConnectionString;
         using (SqlConnection con = new SqlConnection(constr))
         {
             using (SqlCommand cmd = new SqlCommand())
             {
                 cmd.CommandText = @"	SELECT    ISNULL(EGI.ExtProbationPeriodDate,ProbationEndDate)AS ProbationEndDate, EGI.EmpInfoId,EGI.EmpMasterCode,EGI.EmpName, EGI.DateOfJoin, DSN.DivisionId,DSN.DivisionName,DPT.DepartmentId,DPT.DepartmentName,
                                     DSG.DesignationId,DSG.Designation, SLG.SalaryGradeId, SLG.GradeName,*
                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblUser AS US ON EGI.EmpInfoId = US.EmpInfoId
									LEFT JOIN dbo.tblDashboardSetting AS DS ON US.UserId = DS.UserId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId WHERE IsProbationary=1 and EGI.IsActive='1' ";
                 cmd.Connection = con;
                 using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                 {
                     DataSet ds = new DataSet();
                     sda.Fill(ds);
                     return ds.GetXmlSchema();
                 }
             }
         }
     }


    [WebMethod(EnableSession = true)]
     public static List<Select> LoadDept(string selecteddiv)
    {
        List<Select> deptLists = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();
        using (DataTable dt = aChartDal.GetDdlDepartmentByCompanyId(selecteddiv))
        {
            if (dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Select select = new Select();
                    select.Value = int.Parse(dt.Rows[i]["Value"].ToString());
                    select.TextField = dt.Rows[i]["TextField"].ToString();
                    deptLists.Add(select);
                }
            }
        }
        return deptLists;
    }


    [WebMethod(EnableSession = true)]
    public static List<Select> LoadCompany()
    {
        List<Select> ComList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();
        using (DataTable dt = aChartDal.GetComapnyNameList())
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

    [WebMethod(EnableSession = true)]
    public static List<Select> LoadDivisionByComId(string comId)
    {
        List<Select> divList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetDdlComDivision(comId))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Select select = new Select();
                    select.Value = int.Parse(dt.Rows[i]["Value"].ToString());
                    select.TextField = dt.Rows[i]["TextField"].ToString();

                    divList.Add(select);

                }
            }
        }
        return divList;
    }







    private void LoadYear()
    {
        ddlFromYear.Items.Insert(0, "Select Year.....");
        int index = 1;
        for (int i = 2020; i > 1970; i--)
        {
            ddlFromYear.Items.Insert(index, new ListItem(i.ToString(), i.ToString()));
            index++;
        }



        ddlToYear.Items.Insert(0, "Select Year.....");
        int index2 = 1;
        for (int i = 2020; i > 1970; i--)
        {
            ddlToYear.Items.Insert(index2, new ListItem(i.ToString(), i.ToString()));
            index2++;
        }
    }


    [WebMethod(EnableSession = true)]
    public static List<Select> GetEmpCountForDept(string comId, string selecteddiv)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();


      //  if (comId != null && selecteddiv != null && selectedDept=="-1")
        {
            using (DataTable dt = aChartDal.GetGender(" and  CompanyId = " + comId + " AND DivisionId =" + selecteddiv + ""))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Select select = new Select();
                        select.Value = int.Parse(dt.Rows[i]["Total"].ToString());
                        select.TextField = dt.Rows[i]["Gender"].ToString();
                        genderList.Add(select);
                    }
                }
            }
        }
      //  else
        //{
        //    using (DataTable dt = aChartDal.GetGender(" and  CompanyId = " + comId + " AND DivisionId =" + selecteddiv + "" + " AND DivisionId =" + selecteddiv + ""))
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                Select select = new Select();
        //                select.Value = int.Parse(dt.Rows[i]["Total"].ToString());
        //                select.TextField = dt.Rows[i]["Gender"].ToString();
        //                genderList.Add(select);
        //            }
        //        }
        //    }
        //}
        return genderList;

    }



    [WebMethod(EnableSession = true)]
    public static List<Select> GetEmpCountForDeptByDepttt(string comId, string selecteddiv, string selectedDept)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();


        //  if (comId != null && selecteddiv != null && selectedDept=="-1")
        {
            using (DataTable dt = aChartDal.GetGender(" and  CompanyId = " + comId + " AND DivisionId =" + selecteddiv + "" + " AND DepartmentId =" + selectedDept + ""))
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Select select = new Select();
                        select.Value = int.Parse(dt.Rows[i]["Total"].ToString());
                        select.TextField = dt.Rows[i]["Gender"].ToString();
                        genderList.Add(select);
                    }
                }
            }
        }
        //  else
        //{
        //    using (DataTable dt = aChartDal.GetGender(" and  CompanyId = " + comId + " AND DivisionId =" + selecteddiv + "" + " AND DivisionId =" + selecteddiv + ""))
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                Select select = new Select();
        //                select.Value = int.Parse(dt.Rows[i]["Total"].ToString());
        //                select.TextField = dt.Rows[i]["Gender"].ToString();
        //                genderList.Add(select);
        //            }
        //        }
        //    }
        //}
        return genderList;

    }


    [WebMethod(EnableSession = true)]
    public static List<Select> GetEmployeeType(string comId, string selecteddiv)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetEmployeeType("And tblEmpGeneralInfo.CompanyId IN (" + comId + ") " + " AND tblEmpGeneralInfo.DivisionId =" + selecteddiv + ""))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Select select = new Select();
                    select.Value = int.Parse(dt.Rows[i]["Total"].ToString());
                    select.TextField = dt.Rows[i]["EmpType"].ToString();
                    genderList.Add(select);
                }
            }
        }
        return genderList;

    }



    [WebMethod(EnableSession = true)]
    public static List<Select> GetEmployeeTypeByDpt(string comId, string selecteddiv, string selectedDept)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetEmployeeType("And tblEmpGeneralInfo.CompanyId IN (" + comId + ") " + " AND tblEmpGeneralInfo.DivisionId =" + selecteddiv + "" + " AND DepartmentId =" + selectedDept + ""))
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Select select = new Select();
                    select.Value = int.Parse(dt.Rows[i]["Total"].ToString());
                    select.TextField = dt.Rows[i]["EmpType"].ToString();
                    genderList.Add(select);
                }
            }
        }
        return genderList;

    }



    [WebMethod(EnableSession = true)]
    public static List<Select> GetYearWiseSeperation(string comId, string selecteddiv)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetJobLeftEmployeeByDeptYear("  tblEmployeeJobLeft.CompanyId  IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);


                Select select = new Select();
                select.Value = int.Parse(y[i].ToString());
                select.TextField = x[i];
                genderList.Add(select);
            }

           
           
        }
        return genderList;

    }

    [WebMethod(EnableSession = true)]
    public static List<Select> GetYearWiseSeperationDept(string comId, string selecteddiv, string selectedDept)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetJobLeftEmployeeByDeptYear("  tblEmployeeJobLeft.CompanyId  IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv + " AND tblEmpGeneralInfo.DepartmentId =" + selectedDept + ""))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);


                Select select = new Select();
                select.Value = ss; //int.Parse(x[i].ToString());
                select.TextField = int.Parse(x[i].ToString()).ToString();
                genderList.Add(select);
            }




        }
        return genderList;

    }


    [WebMethod(EnableSession = true)]
    public static List<Select> GetAgeGroup(string comId, string selecteddiv)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetBirthYearEmployee(" and  tblEmpGeneralInfo.CompanyId   IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);


                Select select = new Select();
                select.Value = int.Parse(y[i].ToString());
                select.TextField = x[i];
                genderList.Add(select);
            }




        }
        return genderList;

    }

    [WebMethod(EnableSession = true)]
    public static List<Select> GetAgeGroupDept(string comId, string selecteddiv, string selectedDept)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetBirthYearEmployee(" and  tblEmpGeneralInfo.CompanyId   IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv + " AND tblEmpGeneralInfo.DepartmentId =" + selectedDept + ""))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);
                 Select select = new Select();
                select.Value = int.Parse(y[i].ToString());
                select.TextField = x[i];
                genderList.Add(select);
            }




        }
        return genderList;

    }


    [WebMethod(EnableSession = true)]
    public static List<Select> GradewiseEmployeeByDept(string comId, string selecteddiv)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetEmployeeGrade(" and  tblEmpGeneralInfo.CompanyId   IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv ))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);


                Select select = new Select();
                select.Value = int.Parse(y[i].ToString());
                select.TextField = x[i];
                genderList.Add(select);
            }




        }
        return genderList;

    }


    [WebMethod(EnableSession = true)]
    public static List<Select> GradewiseEmployeeByDeptForDep(string comId, string selecteddiv, string selectedDept)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetEmployeeGrade(" and  tblEmpGeneralInfo.CompanyId   IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv + " AND tblEmpGeneralInfo.DepartmentId =" + selectedDept + ""))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);


                Select select = new Select();
                select.Value = int.Parse(y[i].ToString());
                select.TextField = x[i];
                genderList.Add(select);
            }




        }
        return genderList;

    }

    [WebMethod(EnableSession = true)]
    public static List<Select> GetLengthofServiceFroDept(string comId, string selecteddiv, string selectedDept, string dDate)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetJoiningYearEmployeeByJoiningDate(" and  tblEmpGeneralInfo.CompanyId    IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv + " and  tblEmpGeneralInfo.DepartmentId  =" + selectedDept, dDate.ToString()))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);


                Select select = new Select();
                select.Value = int.Parse(y[i].ToString());
                select.TextField = x[i];
                genderList.Add(select);
            }




        }
        return genderList;

    }



    [WebMethod(EnableSession = true)]
    public static List<Select> GetLengthofService(string comId, string selecteddiv, string dDate)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();

        using (DataTable dt = aChartDal.GetJoiningYearEmployeeByJoiningDate(" and  tblEmpGeneralInfo.CompanyId    IN (" + comId + ") " + " and tblEmpGeneralInfo.DivisionId=" + selecteddiv, dDate.ToString()))
        {

            string[] x = new string[dt.Rows.Count];
            decimal[] y = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }

            int dd = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ss = Convert.ToInt32(dt.Rows[i][1]);


                Select select = new Select();
                select.Value = int.Parse(y[i].ToString());
                select.TextField = x[i];
                genderList.Add(select);
            }




        }
        return genderList;

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
            HeaderCell.BackColor = Color.White;
            HeaderCell.BorderColor = Color.White;

            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = " ";
            HeaderCell.BackColor = Color.White;
            HeaderCell.BorderColor = Color.White;


            HeaderCell.ColumnSpan = 1;

            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Joining";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#00D1D1"); 
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Separation";
            HeaderCell.ColumnSpan = 3;
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0A08A");

            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Retirement";
            HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF6283");  

            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);

             

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
            string headerTable = @"<span  style='text-align:left'><h3> " + "SMC" + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}





public class Select
{
    public int Value { get; set; }
    public string TextField { get; set; }
}