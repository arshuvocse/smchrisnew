using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class ContractualEmployeeManagement_UI_ContractualEmpManagementView : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    IncrementDal aSuspendDal = new IncrementDal();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();

            UserPersmissionValidation();
           
            LoadInitialDDL();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");

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

    private void LoadInitialDDL()
    {
        _commonDataLoad.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }
    public void GetCompany()
    {
        try
        {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();

        DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
        for (int i = 0; i < userdata.Rows.Count; i++)
        {
            for (int j = 0; j < lchk_Company.Items.Count; j++)
            {
                if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                {
                    lchk_Company.Items[j].Selected = true;
                }
            }
        }
        }
        catch (Exception ex)
        {

            Response.Redirect("/Default.aspx");
        }
    }

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

            Response.Redirect("/Default.aspx");
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


    private void LoadInfo()
    {

             if (ddlCompany.SelectedValue != "")
        {
            DataTable dataTable = aContractualEmpManageDAL.LoadInformationALl(Parameter(), param2(), ddlCompany.SelectedValue, ParameterTransfer());

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
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
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }

        }
             else
             {
                 loadGridView.DataSource = null;
                 loadGridView.DataBind();
                 aShowMessage.ShowMessageBox("Please select company name!!!", this);
             }
    }

    private string param2()
    {

        string parameter = "   ";
        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.CompanyId = " + ddlCompany.SelectedValue;
        }
        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (mas.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }

    

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND EGI.DivisionId = " + ddlDivision.SelectedValue;
        }



        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND EGI.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND EGI.DepartmentId  = " + ddlDepartment.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.EffectiveDate  BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND  mas.EffectiveDate  BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  mas.EffectiveDate  BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }


    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblContractualEmpManages
                      where t.ContractualEmpManageId == MainID && t.AutoProcess == true
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
            result = (from t in db.tblContractualEmpManages
                      where t.ContractualEmpManageId == MainID && (t.ActionStatus2 != "Posted" || t.ActionStatus2!="Cancel")
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
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
               
                 int mainID = Convert.ToInt32(datKey[0]);

                if (!CheckAchievementsAllocateOrNot(mainID))
                {

                    if (!CheckPostedCsncel(mainID))
                {
                  DataTable aTable =
                             aContractualEmpManageDAL.DeleteValidattionForEffectiveDate(Idd.ToString());
                if (aTable.Rows.Count > 0)
                {


                    
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {


                        Session["Status"] = "Edit";
                        Session["ContractualEmpManageId"] = "";
                        Session["ContractualEmpManageId"] = Idd;
                        Response.Redirect("~/ContractualEmployeeManagement_UI/ContractualEmpManagement.aspx");
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                    }

                }

                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                }
                }

                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                }
                }
                else
                {
                    aShowMessage.ShowMessageBox("Can not be Updated", this);
                }

            }

           

        }



          if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();


            Session["ContractualEmpManageId"] = "";
            Session["ContractualEmpManageId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("~/ContractualEmployeeManagement_UI/ContractualEmpManagement.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

            int mainID = Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString());
            int EIdd = Convert.ToInt32(loadGridView.DataKeys[rowindex][1].ToString());

            if (!CheckAchievementsAllocateOrNot(mainID))
            {

                  if (!CheckPostedCsncel(mainID))
                {
                DataTable aTable =
                    aContractualEmpManageDAL.DeleteValidattionForEffectiveDate(divisionId.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {

                          DataTable dtSPPer =
                  aSuspendDal.checkSpecialPermission(EIdd.ToString());

                             if (dtSPPer.Rows.Count > 0)
                    {

                        Session["ContractualEmpManageId"] = "";
                        Session["ContractualEmpManageId"] = divisionId;
                        Session["Status"] = "Delete";
                        Response.Redirect("~/ContractualEmployeeManagement_UI/ContractualEmpManagement.aspx");
                    }
                             else
                             {
                                 aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                             }
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
                    }

                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
                }
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("Can not be Deleted", this);
            }
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string EmployeeJobLeftId = loadGridView.DataKeys[rowindex][0].ToString();

        //    if (aContractualEmpManageDAL.DeleteContractualEmpManageById(EmployeeJobLeftId))
        //    {
        //        LoadInfo();
        //        aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
                
        //    }
        //}

        if (e.CommandName == "Preview")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = loadGridView.DataKeys[0];

            if (datKey != null)
            {

                PopUp(Convert.ToInt32(e.CommandArgument.ToString()));

               

            }



        }
      
    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/ContractualEmpManagementReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("ContractualEmpManagement.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
         
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            aSuspendDal.LoadFinancialYear(ddlFinYear, ddlCompany.SelectedValue);
            aSuspendDal.LoadDivision(ddlDivision, ddlCompany.SelectedValue);
            using (DataTable dt222 = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
        }
        else
        {
            ddlFinYear.Items.Clear();
        }
    }

    protected void btnFilterSearch_OnClick(object sender, EventArgs e)
    {
        LoadInfo();
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

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);

        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
        }
    }


    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "")
            {

                DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
                if (dtgetdata.Rows.Count > 0)
                {
                    if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                    {
                        wing.Visible = false;
                        ddlWing.Items.Clear();
                        _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        // ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    }
                    else
                    {
                        wing.Visible = true;
                        ddlWing.Items.Clear();
                        _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);

                    }
                }
            }
            else
            {

            }
            if (ddlDepartment.SelectedIndex == 0)
            {
                wing.Visible = true;
                ddlWing.SelectedValue = "";
                //  ddlWing.DataBind();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWing.SelectedValue != "")
        {
            _commonDataLoad.GetDepartmentList(ddlDepartment, ddlDivision.SelectedValue);
        }
        else
        {
            ddlDepartment.Items.Clear();
        }
    }
    public string Parameter()
    {
        string parameter = "   ";
        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND Emp.CompanyId = " + ddlCompany.SelectedValue;
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (EPE.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }

        //if (txtSearch.Text != "")
        //{
        //    parameter = parameter + "  and (Emp.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
        //}


        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionId = " + ddlDivision.SelectedValue;
        }



        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND Emp.DepartmentId  = " + ddlDepartment.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  EPE.EffectiveDate  BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND  EPE.EffectiveDate  BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  EPE.EffectiveDate  BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    public string ParameterTransfer()
    {
        string parameter = "   ";
        
        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (reff.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }

        //if (txtSearch.Text != "")
        //{
        //    parameter = parameter + "  and (Emp.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
        //}


        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionId = " + ddlDivision.SelectedValue;
        }



        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND Emp.DepartmentId  = " + ddlDepartment.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  EPE.EffectiveDate  BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND  EPE.EffectiveDate  BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  EPE.EffectiveDate  BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfo();
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ContractualEmpManagementView.aspx");
    }
}