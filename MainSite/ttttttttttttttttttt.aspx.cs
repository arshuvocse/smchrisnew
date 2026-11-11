using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;

public partial class ttttttttttttttttttt : System.Web.UI.Page
{


    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        using (DataTable dt = _commonDataLoad.GetEmpDDL(1.ToString()))
        {



            ddlEmpInfo.DataSource = dt;
            ddlEmpInfo.DataValueField = "EmpInfoId";
            ddlEmpInfo.DataTextField = "EmpName";
            ddlEmpInfo.DataBind();
        }
    }
}