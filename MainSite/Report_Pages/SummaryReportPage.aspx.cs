using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Chart_DAL;
using DAL.Report_DAL;

public partial class Report_Pages_SummaryReportPage : System.Web.UI.Page

{
    
 private static string usserId = "";
 private static SummaryReportPageDAL aSumm = new SummaryReportPageDAL();
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
           BindTable01_DepartmentEmploymentData();
            BindTable05_GradeWiseStaff();
            BindTable03_ProjectFundWisemalefemaleRatio();
            BindTable04_ProjectFundWisemalefemaleRatio();
 
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    public void BindTable01_DepartmentEmploymentData()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("ItemNo");
        dt.Columns.Add("DepartmentName");

        dt.Columns.Add("Permnaent");

        dt.Columns.Add("Contractual");
        dt.Columns.Add("SubTotal");
        dt.Columns.Add("Casual");
        dt.Columns.Add("AllTotal");


        dt.Rows.Add();

        gv_Table01_DepartmentEmployment.DataSource = dt;

        gv_Table01_DepartmentEmployment.DataBind();

    }


    public void BindTable03_ProjectFundWisemalefemaleRatio()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("ItemNo");
        dt.Columns.Add("ProjectName");

        dt.Columns.Add("AllTotal");
        dt.Columns.Add("Male");
        dt.Columns.Add("MalePer");
        dt.Columns.Add("Female");

        dt.Columns.Add("FemalePer");
        


        dt.Rows.Add();

        gv_Table03_ProjectFundWisemalefemaleRatio.DataSource = dt;

        gv_Table03_ProjectFundWisemalefemaleRatio.DataBind();

    }


    public void BindTable04_ProjectFundWisemalefemaleRatio()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("ItemNo");
        dt.Columns.Add("ProjectName");

        dt.Columns.Add("Permnaent");

        dt.Columns.Add("Contractual");
        dt.Columns.Add("SubTotal");
        dt.Columns.Add("Casual");
        dt.Columns.Add("AllTotal");


        dt.Rows.Add();

        gv_Table04_ProjectWiseStaff.DataSource = dt;

        gv_Table04_ProjectWiseStaff.DataBind();

    }

    public void BindTable05_GradeWiseStaff()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("ItemNo");
        dt.Columns.Add("Designation");

        dt.Columns.Add("GradeName");
        dt.Columns.Add("AllTotal");

        dt.Columns.Add("Permnaent");
        dt.Columns.Add("Contractual");
        dt.Columns.Add("Casual");
        dt.Columns.Add("Male");
        dt.Columns.Add("MalePer");
        dt.Columns.Add("Female");
        dt.Columns.Add("FeMalePer");
        


        dt.Rows.Add();

        gv_Table05_GradeWiseStaff.DataSource = dt;

        gv_Table05_GradeWiseStaff.DataBind();

    }




    [WebMethod(EnableSession = true)]
    public static table03[] GeBindTable03_PrjectWiseMaleFemaleData(string comId)
    {
        List<table03> tbl03 = new List<table03>();

        using (DataTable dt = aSumm.GeBindTable03_PrjectWiseMaleFemaleDataDAL(Convert.ToInt32((comId))))
        {
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow DR in dt.Rows)
                {

                    table03 tbl033 = new table03();

                    tbl033.ItemNo = DR["ItemNo"].ToString();
                    tbl033.ProjectName = DR["ProjectName"].ToString();

                    tbl033.AllTotal = DR["AllTotal"].ToString();

                    tbl033.Male = DR["Male"].ToString();

                  
                    tbl033.MalePer = DR["MalePer"].ToString();
                    tbl033.Female = DR["Female"].ToString();
                    tbl033.FemalePer = DR["FemalePer"].ToString();
                   

                    tbl03.Add(tbl033);

                }

                return tbl03.ToArray();
            }
        }

        return tbl03.ToArray();

    }



    [WebMethod(EnableSession = true)]
    public static table04[] GetTable04_ProjectWiseStaffData(string comId)
    {
        List<table04> ltable04 = new List<table04>();

        using (DataTable dt = aSumm.GeBindTable04_ProjectWiseStaffDAL(Convert.ToInt32((comId))))
        {
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow DR in dt.Rows)
                {

                    table04 table04ll = new table04();

                    table04ll.ItemNo = DR["ItemNo"].ToString();
                    table04ll.ProjectName = DR["ProjectName"].ToString();

                    table04ll.Permnaent = DR["Permnaent"].ToString();

                    table04ll.Contractual = DR["Contractual"].ToString();

                    table04ll.SubTotal = DR["SubTotal"].ToString();

                    table04ll.Casual = DR["Casual"].ToString();
                    table04ll.AllTotal = DR["AllTotal"].ToString();


                    ltable04.Add(table04ll);

                }

                return ltable04.ToArray();
            }
        }

        return ltable04.ToArray();

    }


 [WebMethod(EnableSession = true)]
    public static table01[] GetTable01_DepartmentEmploymentData(string comId)
    {
        List<table01> leftjjjjzxz = new List<table01>();
       
        using (DataTable dt = aSumm.GetTable01_DepartmentEmploymentDataDAL(Convert.ToInt32((comId))))
       
        {
            if (dt.Rows.Count > 0)
            {



                foreach (DataRow DR in dt.Rows)
                {

                    table01 objLeft = new table01();

                    objLeft.ItemNo = DR["ItemNo"].ToString();
                    objLeft.DepartmentName = DR["DepartmentName"].ToString();

                    objLeft.Permnaent = DR["Permnaent"].ToString();

                    objLeft.Contractual = DR["Contractual"].ToString();

                    objLeft.SubTotal = DR["SubTotal"].ToString();

                    objLeft.Casual = DR["Casual"].ToString();
                    objLeft.AllTotal = DR["AllTotal"].ToString();
                  

                    leftjjjjzxz.Add(objLeft);

                }

                return leftjjjjzxz.ToArray();
            }
        }

        return leftjjjjzxz.ToArray();
       
    }


 [WebMethod(EnableSession = true)]
 public static table05[] GeBindTable05_GradeWiseStaffData(string comId)
 {
     List<table05> Table05L = new List<table05>();

     using (DataTable dt = aSumm.GeBindTable05_GradeWiseStaffDataDAL(Convert.ToInt32((comId))))
     {
         if (dt.Rows.Count > 0)
         {



             foreach (DataRow DR in dt.Rows)
             {

                 table05 Table05Ll = new table05();

                 Table05Ll.ItemNo = DR["ItemNo"].ToString();
                // Table05Ll.Designation = DR["Designation"].ToString();
                 Table05Ll.GradeName = DR["GradeName"].ToString();
               
                 Table05Ll.AllTotal = DR["AllTotal"].ToString();


                 Table05Ll.Permnaent = DR["Permnaent"].ToString();
                 Table05Ll.Contractual = DR["Contractual"].ToString();
                 Table05Ll.Casual = DR["Casual"].ToString();
                 Table05Ll.Male = DR["Male"].ToString();


                 Table05Ll.MalePer = DR["MalePer"].ToString();
                 Table05Ll.Female = DR["Female"].ToString();
                 Table05Ll.FeMalePer = DR["FeMalePer"].ToString();


                 Table05L.Add(Table05Ll);

             }

             return Table05L.ToArray();
         }
     }

     return Table05L.ToArray();

 }
    
    public class table01
    {

           public string ItemNo { get; set; }
           public string DepartmentName { get; set; }
           public string ProjectName { get; set; }

           public string Permnaent { get; set; }

           public string Contractual { get; set; }



           public string SubTotal { get; set; }
           public string Casual { get; set; }
           public string AllTotal { get; set; }
    

    }
    public class table04
    {

        public string ItemNo { get; set; }
       
        public string ProjectName { get; set; }

        public string Permnaent { get; set; }

        public string Contractual { get; set; }



        public string SubTotal { get; set; }
        public string Casual { get; set; }
        public string AllTotal { get; set; }


    }

    public class table05
    {

        public string ItemNo { get; set; }
        public string Designation { get; set; }
     

        public string GradeName { get; set; }

        public string Permnaent { get; set; }

        public string AllTotal { get; set; }
        public string SubTotal { get; set; }
        public string Contractual { get; set; }
        public string Casual { get; set; }
        public string Male { get; set; }
        public string MalePer { get; set; }
        public string Female { get; set; }
        public string FeMalePer { get; set; }
       


    }

    public class table03
    {

        public string ItemNo { get; set; }
      
        public string ProjectName { get; set; }

     
 
        public string AllTotal { get; set; }
       
        public string Male { get; set; }
        public string MalePer { get; set; }
        public string Female { get; set; }
      
        public string FemalePer { get; set; }


    }
}