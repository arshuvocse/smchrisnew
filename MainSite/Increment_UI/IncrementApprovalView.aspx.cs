using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class Increment_UI_IncrementApprovalView : System.Web.UI.Page
{
    DataTable aDataTable = new DataTable();
    IncrementDal aSuspendDal = new IncrementDal();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
            LoadDropDownList();

            //GetCompany();
          //  UserPersmissionValidation();
            EmpIncrementLoad();
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
        if (ddlCompany.SelectedValue != "")
        {
            aSuspendDal.LoadFinancialYear(ddlFinYear, ddlCompany.SelectedValue);

        }
        else
        {
            ddlFinYear.Items.Clear();
        }
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
                        Convert.ToBoolean(ViewState["View"].ToString());
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

    private string GenerateParamiterList()
    {
        string parameter = " WHERE EGI.IsActive = 1 AND EGI.EmployeeStatus IN ('Active') AND   (IsDelete=0 OR IsDelete IS NULL)";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND INC.CompanyId = " + ddlCompany.SelectedValue ;
        }

       

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND INC.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    private void EmpIncrementLoad()
    {
        aDataTable = aSuspendDal.LoadIncrementInfoApp();
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
          //  aShowMessage.ShowMessageBox("No Data Found!!!", this);
            loadGridView.DataSource = null;
            loadGridView.DataBind(); 
        }
        
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];

            Session["Status"] = "View";
            string suspendId = null;

            if (dataKey != null)
                suspendId = dataKey[0].ToString();

            Session["suspendId"] = suspendId;
            Response.Redirect("EmployeeSuspend.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
             int rowindex = Convert.ToInt32(e.CommandArgument);
            
            DataTable aTable =
                             aSuspendDal.DeleteValidattionForEffectiveDate(Convert.ToInt32(e.CommandArgument).ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    IncrementDao aDAO = new IncrementDao();

                    aDAO.IncrementId = Convert.ToInt32(e.CommandArgument);
                    aDAO.IsDelete = true;
                    aDAO.DeleteBy = Convert.ToInt32(Session["UserId"]);
                    aDAO.DeleteDate = DateTime.Now;


                    ResetEmpGeneralInfo(aDAO.IncrementId);

                    aSuspendDal.DeleteIncrementMaster(aDAO);
                   

                 this.EmpIncrementLoad();
                  

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Data Deleted Successfully!!');",
                     true);
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted!!",this);
                }

            }
            
 

        }

        if (e.CommandName == "ApprovalData")
        {


            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string jobReqId = datKey[0].ToString();
                string filepath = Path.GetDirectoryName(Request.Path);
                filepath = filepath.TrimStart('\\');
                string exten = Path.GetExtension(Request.Path);
                if (exten == string.Empty)
                {
                    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                }
                else
                {
                    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                }
                Session["IncrementAppLogId"] = "";
                Session["IncrementAppLogId"] = jobReqId;
                Session["AppLogId"] = datKey[2].ToString();
                Session["AppPage"] = filepath;


               
                string EmployeeId = datKey["EmployeeId"].ToString();
                Response.Redirect("IncrementEntryApproval.aspx?mid=" + jobReqId);

            }
            
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
        Session["Status"] = "Add";
       Response.Redirect("IncrementEntry2.aspx");
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
}