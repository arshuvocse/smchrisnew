using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.UserProfileDAL;

public partial class UserProfile_KPIDashboard : System.Web.UI.Page
{

    KPIDashboardDAL aDAL = new KPIDashboardDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmpInfoId"] != null && Session["EmpInfoId"] != "")
            {

                GetOneRecord(Session["EmpInfoId"].ToString());


                if (DateTime.Now != null)
                {


                    if (CheckStartEndDateExistOrNot(DateTime.Now, DateTime.Now) == true)
                    {

                    }

                }

            }
        }
    }

    private DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();


    private void GetOneRecord(string id)
    {

        DataTable dataTable = aDAL.GetEmployeeInfoDAL(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows[rowIndex].Field<string>("EmpImage") != "")
            {
                UserImage.ImageUrl = "~/UploadImg/" + dataTable.Rows[rowIndex].Field<string>("EmpImage");
            }
            else
            {

            }

            
            lblshortName.Text = dataTable.Rows[rowIndex].Field<string>("EmpName");
            lblDesignation.Text = dataTable.Rows[rowIndex].Field<string>("Designation");
            lblID.Text = dataTable.Rows[rowIndex].Field<string>("EmpMasterCode");

            try
            {
                int comId = Convert.ToInt32(dataTable.Rows[rowIndex].Field<int>("CompanyId"));

                DataTable dtaa = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(comId));
                ddlFinancialYear.DataSource = dtaa;
                ddlFinancialYear.DataValueField = "Value";
                ddlFinancialYear.DataTextField = "TextField";
                ddlFinancialYear.DataBind();
            }
            catch (Exception)
            {
                
                //throw;
            }


        }
    }
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

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}