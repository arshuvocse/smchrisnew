using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Chart_DAL;

public partial class ChartTest_Chartttttttttt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public class Select
    {
        public int Value { get; set; }
        public string TextField { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static List<Select> GetEmpCountForDept(string comId, string selecteddiv)
    {
        List<Select> genderList = new List<Select>();
        ChartDAL aChartDal = new ChartDAL();


        //  if (comId != null && selecteddiv != null && selectedDept=="-1")
        {
            using (DataTable dt = aChartDal.GetGender(" and  CompanyId = " + 1 ))
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
}