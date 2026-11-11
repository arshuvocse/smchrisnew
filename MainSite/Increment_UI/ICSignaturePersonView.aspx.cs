using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class Increment_UI_ICSignaturePersonView : System.Web.UI.Page
{
    DataTable aDataTable = new DataTable();
    ICSignaturePersonDAL aSuspendDal = new ICSignaturePersonDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    CompanyHierarchyDAL aCompanyHierarchyDal=new CompanyHierarchyDAL();

    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            LoadDropDownList();

            //GetCompany();
         UserPersmissionValidation();
            //EmpIncrementLoad();
        }

        try
        {

            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }
    }


    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompany(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
   

  
    //public void GetCompany()
    //{
    //    DataTable dtcomp = aPermissionDal.GetCompany();
    //    lchk_Company.DataValueField = "CompanyId";
    //    lchk_Company.DataTextField = "ShortName";
    //    lchk_Company.DataSource = dtcomp;
    //    lchk_Company.DataBind();

    //    DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
    //    for (int i = 0; i < userdata.Rows.Count; i++)
    //    {
    //        for (int j = 0; j < lchk_Company.Items.Count; j++)
    //        {
    //            if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
    //            {
    //                lchk_Company.Items[j].Selected = true;


    //            }
    //        }
    //    }
    //}

    public void UserPersmissionValidation()
    {
        try
        {
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public string CompanyId()
    {
        string companyid = "";
        //for (int i = 0; i < lchk_Company.Items.Count; i++)
        //{
        //    if (lchk_Company.Items[i].Selected)
        //    {
        //        companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
        //    }
        //}
        //companyid = companyid.TrimEnd(',');
        return companyid;
    }

  

    private void EmpIncrementLoad()
    {
             if (ddlCompany.SelectedValue != "")
        {
            aDataTable = aSuspendDal.LoadIncrementInfo(ddlCompany.SelectedValue);
        if (aDataTable.Rows.Count>0)
        {
            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        else
        {
            aShowMessage.ShowMessageBox("No Data Found!!!", this);
            loadGridView.DataSource = null;
            loadGridView.DataBind(); 
        }
        }
             else
             {
                 loadGridView.DataSource = null;
                 loadGridView.DataBind();
                 aShowMessage.ShowMessageBox("Please select company name!!!", this);
             }
    }

 

    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblIncrements
                      where t.IncrementId == MainID && t.AutoProcess == "Manually Updated"
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }

    private bool CheckPostedCsncel(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblIncrements
                      where t.IncrementId == MainID && (t.ActionStatus2 != "Posted" || t.ActionStatus2 != "Cancel")
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

        

        if (e.CommandName == "DeleteData")
        {
             int rowindex = Convert.ToInt32(e.CommandArgument);

             int mainID = Convert.ToInt32(Convert.ToInt32(e.CommandArgument));

             aSuspendDal.DeleteMaster(mainID.ToString());


             ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Opearation Successfully Done...');window.location ='ICSignaturePersonView.aspx';",
                true);
 

        }

         
    }

    private void ResetEmpGeneralInfo(int incrementId)
    {
        DataTable aTable = aSuspendDal.FetchEmployeeInfoById(incrementId);

        if (aTable.Rows.Count > 0)
        {
            Int32 employeeId = aTable.Rows[0].Field<Int32>("EmployeeId");
            Int32 currentStepId = aTable.Rows[0].Field<Int32>("CurrentStepId");

            EmpGeneralInfo aInfo = new EmpGeneralInfo();

            aInfo.SalScaleId = currentStepId;
            aInfo.EmpInfoId = employeeId;

            aSuspendDal.ResetEmployeeIncrementalStepInfo(aInfo);
        }
    }


    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add"; Response.Redirect("ICSignaturePerson.aspx");
    }

    protected void reloadButton_OnClick(object sender, EventArgs e)
    {
        EmpIncrementLoad();
    }


    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        EmpIncrementLoad();
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("IncrementView.aspx");
    }
}