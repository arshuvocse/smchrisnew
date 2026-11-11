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
using DAL.MenuSetup_DAL;
using DAL.Permission_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class SuspendAndDiciplinary_UI_IncrementView : System.Web.UI.Page
{
    DataTable aDataTable = new DataTable();

    IncrementDal aSuspendDal = new IncrementDal();
    private UserCommonDAL _UserCommonDAL = new UserCommonDAL();
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

            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
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

    readonly IncrementDal aIncrementDal = new IncrementDal();

    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompany(ddlCompany);
        aIncrementDal.LoadIncrementType(ddlIncrementType);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            aSuspendDal.LoadFinancialYear(ddlFinYear, ddlCompany.SelectedValue);
            aCompanyHierarchyDal.LoadDivision(ddlDivision, ddlCompany.SelectedValue);


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
            ddlWing.Enabled = true;
            aCompanyHierarchyDal.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            aCompanyHierarchyDal.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);

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

                DataTable dtgetdata = aCompanyHierarchyDal.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
                if (dtgetdata.Rows.Count > 0)
                {
                    if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                    {
                        ddlWing.Enabled = false;
                        ddlWing.CssClass = "form-control form-control-sm";
                        ddlWing.Items.Clear();
                        aCompanyHierarchyDal.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        // ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    }
                    else
                    {
                        ddlWing.Enabled = true;
                        ddlWing.CssClass = "form-control form-control-sm";
                        ddlWing.Items.Clear();
                        aCompanyHierarchyDal.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);

                    }
                }
            }
            else
            {

            }
            if (ddlDepartment.SelectedIndex == 0)
            {
                ddlWing.Enabled = false;
                ddlWing.CssClass = "form-control form-control-sm";
                ddlWing.SelectedValue = "";
                //  ddlWing.DataBind();
                aCompanyHierarchyDal.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
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
            aCompanyHierarchyDal.GetDepartmentList(ddlDepartment, ddlWing.SelectedValue);
        }
        else
        {
            ddlDepartment.Items.Clear();
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

    private string GenerateParamiterList()
    {


        string parameter =
            " WHERE  (tblIncrementAppLog.Version=LogApp.MaxVer OR tblIncrementAppLog.Version IS NULL) and  (INC.IsDelete=0 OR INC.IsDelete IS NULL)";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND INC.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (INC.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }
        if (ddlIncrementType.SelectedIndex >0) {
            parameter = parameter + " AND INC.IncrementTypeId = " + ddlIncrementType.SelectedValue;

    }

if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND INC.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND E.DivisionId = " + ddlDivision.SelectedValue;
        }

       

        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND E.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND E.DepartmentId  = " + ddlDepartment.SelectedValue;
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


    private string GenerateParamiterListTransfer()
    {


        string parameter =
            "  ";

      
        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (reff.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }
        if (ddlIncrementType.SelectedIndex > 0)
        {
            parameter = parameter + " AND INC.IncrementTypeId = " + ddlIncrementType.SelectedValue;

        }

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND INC.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND E.DivisionId = " + ddlDivision.SelectedValue;
        }



        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND E.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND E.DepartmentId  = " + ddlDepartment.SelectedValue;
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
             if (ddlCompany.SelectedValue != "")
        {
            aDataTable = aSuspendDal.LoadIncrementInfo(GenerateParamiterList(), parm2(), ddlCompany.SelectedValue, GenerateParamiterListTransfer());
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

    private string parm2()
    {

        string parameter = "  ";


        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND  Emp.CompanyId= " + ddlCompany.SelectedValue;
        }



      



        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlIncrementType.SelectedIndex > 0)
        {
            parameter = parameter + " AND tblIncrement_HistoricalData.Remarks = '" + ddlIncrementType.SelectedItem.Text + "'";

        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (tblIncrement_HistoricalData.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND tblIncrement_HistoricalData.EmployeeId=0 ";
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
            parameter = parameter + " AND EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
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


        if (e.CommandName == "ViewData")
        {

            //var dataKey = loadGridView.DataKeys[0];


            //string suspendId = null;

            //if (dataKey != null)
            //    suspendId = dataKey[0].ToString();

            //Session["Status"] = "";
            //Session["Status"] = "Edit";
            //Session["IncrementEdit"] = suspendId.ToString();
            //Response.Redirect("IncrementEntry2.aspx?id=" + suspendId.ToString());
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            DataTable aTable =
                aSuspendDal.DeleteValidattionForEffectiveDate(Convert.ToInt32(e.CommandArgument).ToString());
            if (aTable.Rows.Count > 0)
            {


                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    string EIdd = datKey[1].ToString();
                    int mainID = Convert.ToInt32(datKey[0].ToString());



                    if (!CheckAchievementsAllocateOrNot(mainID))
                    {

                        if (!CheckPostedCsncel(mainID))
                        {

                            DataTable dtSPPer =
                                aSuspendDal.checkSpecialPermission(EIdd.ToString());

                            if (dtSPPer.Rows.Count > 0)
                            {
                                IncrementDao aDAO = new IncrementDao();

                                aDAO.IncrementId = Convert.ToInt32(e.CommandArgument);
                                aDAO.IsDelete = true;
                                aDAO.DeleteBy = Convert.ToInt32(Session["UserId"]);
                                aDAO.DeleteDate = DateTime.Now;


                                ResetEmpGeneralInfo(aDAO.IncrementId);

                                aSuspendDal.DeleteIncrementMaster(aDAO);





                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Data Deleted Successfully!!');",
                                    true);
                            }
                            else
                            {
                                aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!",
                                    this);
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
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted!!", this);
                }

            }



        }

        if (e.CommandName == "Preview")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            DataTable aTable =
                aSuspendDal.DeleteValidattionForEffectiveDate(Convert.ToInt32(e.CommandArgument).ToString());
            if (aTable.Rows.Count > 0)
            {


                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    string EIdd = datKey[1].ToString();
                    int mainID = Convert.ToInt32(datKey[0].ToString());



                    if (!CheckAchievementsAllocateOrNot(mainID))
                    {

                        if (!CheckPostedCsncel(mainID))
                        {

                            DataTable dtSPPer =
                                aSuspendDal.checkSpecialPermission(EIdd.ToString());

                            if (dtSPPer.Rows.Count > 0)
                            {

                                string EmployeeId = datKey["EmployeeId"].ToString();
                                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal",
                                    "window.open('MemoPrintIncrement.aspx?mid=" + e.CommandArgument.ToString() +
                                    "&EmpId=" + EmployeeId + "' ,'_blank');", true);
                                //  Response.Redirect("MemoPrintIncrement.aspx?mid=" + e.CommandArgument.ToString() + "&EmpId=" + EmployeeId);
                                Session["Status"] = "View";
                            }
                            else
                            {
                                aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!",
                                    this);
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
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted!!", this);
                }

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

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("IncrementView.aspx");
    }

    protected void btnEdit_OnClickick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfEmployeeId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfEmployeeId");
        HiddenField hfIncrementId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfIncrementId");

        DataTable dtSPPer =
                    aSuspendDal.checkSpecialPermission(hfEmployeeId.Value.ToString());

      
        if (hfIncrementId.Value == "0")
        {
            aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
        }
        else
        {

            //try
            //{
            //    DataTable aTable =
            //        aSuspendDal.editValidattionForEffectiveDate(hfEmployeeId.Value.ToString());



            //    DataTable aTable2 =
            //        aSuspendDal.editValidattionForEffectiveDate2(hfIncrementId.Value.ToString());


            //    if (aTable.Rows[0]["EffectiveDate"].ToString() == aTable2.Rows[0]["EffectiveDate"].ToString())
            //    {

            //        if (dtSPPer.Rows.Count > 0)
            //        {

                   
            //        Session["Status"] = "Edit";
            //        Session["IncrementEdit"] = "";
            //        Session["IncrementEdit"] = hfIncrementId.Value;

            //        Response.Redirect("IncrementEntryEdit.aspx");
            //        }
            //        else
            //        {
            //            aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
            //        }

            //    }
            //    else
            //    {
            //        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
            //    }
            //}
            //catch (Exception)
            {
                 if (dtSPPer.Rows.Count > 0)
                    {
                Session["Status"] = "Edit";
                Session["IncrementEdit"] = "";
                Session["IncrementEdit"] = hfIncrementId.Value;

                Response.Redirect("IncrementEntryEdit.aspx");
                    }
                 else
                 {
                     aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                 }

            }
        }



    }

    protected void ViewReportImageButton_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfEmployeeId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfEmployeeId");
        HiddenField hfIncrementId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfIncrementId");

        DataTable dtSPPer =
                    aSuspendDal.checkSpecialPermission(hfEmployeeId.Value.ToString());


        if (hfIncrementId.Value == "0")
        {
            aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
        }
        else
        {

            //try
            //{
            //    DataTable aTable =
            //        aSuspendDal.editValidattionForEffectiveDate(hfEmployeeId.Value.ToString());



            //    DataTable aTable2 =
            //        aSuspendDal.editValidattionForEffectiveDate2(hfIncrementId.Value.ToString());


            //    if (aTable.Rows[0]["EffectiveDate"].ToString() == aTable2.Rows[0]["EffectiveDate"].ToString())
            //    {

            //        if (dtSPPer.Rows.Count > 0)
            //        {


            //        Session["Status"] = "Edit";
            //        Session["IncrementEdit"] = "";
            //        Session["IncrementEdit"] = hfIncrementId.Value;

            //        Response.Redirect("IncrementEntryEdit.aspx");
            //        }
            //        else
            //        {
            //            aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
            //        }

            //    }
            //    else
            //    {
            //        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
            //    }
            //}
            //catch (Exception)
            {
                if (dtSPPer.Rows.Count == 0)
                {

                    string EmployeeId = hfEmployeeId.Value;
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal",
                        "window.open('MemoPrintIncrement.aspx?mid=" + hfIncrementId.Value +
                        "&EmpId=" + EmployeeId + "' ,'_blank');", true);
                    //  Response.Redirect("MemoPrintIncrement.aspx?mid=" + e.CommandArgument.ToString() + "&EmpId=" + EmployeeId);
                 
                }
                else
                {
                    aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                }

            }
        }



    }

    protected void deleteImageButton_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfEmployeeId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfEmployeeId");
        HiddenField hfIncrementId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfIncrementId");

        DataTable aTable =
            aSuspendDal.DeleteValidattionForEffectiveDate(Convert.ToInt32(hfIncrementId.Value).ToString());
        if (aTable.Rows.Count > 0)
        {


            string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
            string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

            if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
            {
                string EIdd = hfEmployeeId.Value;
                int mainID = Convert.ToInt32(hfIncrementId.Value);



                if (!CheckAchievementsAllocateOrNot(mainID))
                {

                    if (!CheckPostedCsncel(mainID))
                    {

                        DataTable dtSPPer =
                            aSuspendDal.checkSpecialPermission(EIdd.ToString());

                        if (dtSPPer.Rows.Count > 0)
                        {
                            IncrementDao aDAO = new IncrementDao();

                            aDAO.IncrementId = Convert.ToInt32(hfIncrementId.Value);
                            aDAO.IsDelete = true;
                            aDAO.DeleteBy = Convert.ToInt32(Session["UserId"]);
                            aDAO.DeleteDate = DateTime.Now;


                            ResetEmpGeneralInfo(aDAO.IncrementId);

                            aSuspendDal.DeleteIncrementMaster(aDAO);





                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Data Deleted Successfully!!');",
                                true);
                        }
                        else
                        {
                            aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!",
                                this);
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
            else
            {
                aShowMessage.ShowMessageBox("Data Can not be Deleted!!", this);
            }

        }
    }
}