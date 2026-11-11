using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Transfer_UI_EmployeePromotionEntryView : System.Web.UI.Page
{

    tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();
    tblEmployeePromotionEntryDAL atblEmployeePromotionEntryDAL = new tblEmployeePromotionEntryDAL();
    IncrementDal aSuspendDal = new IncrementDal();

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();
           
            LoadDropDownList();
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
            aSuspendDal.LoadDivision(ddlDivision, ddlCompany.SelectedValue);
            //CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
            //using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            //{
            //    ddlDepartment.DataSource = dt;
            //    ddlDepartment.DataValueField = "Value";
            //    ddlDepartment.DataTextField = "TextField";
            //    ddlDepartment.DataBind();
            //}


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

    
    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();
        try
        {

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
            string text = Path.GetExtension(Request.Path);
            if (text==string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path)+".aspx";    
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);    
            }
            
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

            DataTable dataTable =
                atblEmployeePromotionEntryDAL.LoadInformationALl(
                    " WHERE EPE.CompanyId IN (" + CompanyId() + ") AND (EPE.IsDelete is null or EPE.IsDelete=0)" +
                    GenerateParamiterList(), Param2(), ddlCompany.SelectedValue, GenerateParamiterListTranfer());

            if (dataTable.Rows.Count > 0)
            {
                loadGridView.DataSource = dataTable;
                loadGridView.DataBind();
                loadGridView.UseAccessibleHeader = true;
                loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
                loadGridView.UseAccessibleHeader = true;
            }
            else
            {
                loadGridView.DataSource = null;
                loadGridView.DataBind();
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
            }
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    private string Param2()
    {
        string parameter = "   ";

 

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND Emp.CompanyId = " + ddlCompany.SelectedValue;
        }



        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionWId = " + ddlWing.SelectedValue;
        }

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND Emp.DepartmentId = " + ddlDepartment.SelectedValue;
        }


        //if (ddlFinYear.SelectedValue != "")
        //{
        //    parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        //}


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.EffectDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.EffectDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.EffectDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (EPE.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }

        return parameter;
    }

    private string GenerateParamiterList()
    {
 
        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND EPE.DepartmentId = " + ddlDepartment.SelectedValue;
        }
       

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (EPE.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }
        return parameter;
    }



    private string GenerateParamiterListTranfer()
    {

        string parameter = "   ";

       

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.DivisionWId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND EPE.DepartmentId = " + ddlDepartment.SelectedValue;
        }


        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (reff.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }
        return parameter;
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
                        _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                      //  ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
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
    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblEmployeePromotionEntries
                      where t.EmployeePromotionEntryId == MainID && t.AutoProcess == "Manually Updated"
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
                string EIdd = datKey[2].ToString();
                string pkId = datKey[0].ToString();
                if (pkId == "0")
                {
                    aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                }
                else
                {

                    try
                    {
                        DataTable aTable =
                        atblEmployeePromotionEntryDAL.editValidattionForEffectiveDate(EIdd.ToString());

                        DataTable aTable2 =
                            atblEmployeePromotionEntryDAL.editValidattionForEffectiveDate2(pkId.ToString());


                        if (aTable.Rows[0]["EffectiveDate"].ToString() == aTable2.Rows[0]["EffectiveDate"].ToString())
                        //{
                        //    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                        //    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                        //    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                        //    {
                        //        int mainID = Convert.ToInt32(Convert.ToInt32(e.CommandArgument));

                        //        if (!CheckAchievementsAllocateOrNot(mainID))
                        //        {

                        //            if (!CheckPostedCsncel(mainID))


                        {

                            DataTable dtSPPer =
                  aSuspendDal.checkSpecialPermission(EIdd.ToString());

                             if (dtSPPer.Rows.Count > 0)
                    {
                            Session["Status"] = "Edit";
                            Session["EmployeePromotionEntryId"] = "";
                            Session["EmployeePromotionEntryId"] = pkId;

                            Response.Redirect("EmployeePromotionEdit.aspx");
                    }
                             else
                             {
                                 aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                             }
                        }
                        else
                        {
                            aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                        }
                        // }
                        //    else
                        //    {
                        //        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                        //    }
                        //}
                        //else
                        //{
                        //    aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                        //}
                    }
                    catch (Exception)
                    {
                              DataTable dtSPPer =
                  aSuspendDal.checkSpecialPermission(EIdd.ToString());

                             if (dtSPPer.Rows.Count > 0)
                    {
                        Session["Status"] = "Edit";
                        Session["EmployeePromotionEntryId"] = "";
                        Session["EmployeePromotionEntryId"] = pkId;

                        Response.Redirect("EmployeePromotionEdit.aspx");
                    }
                             else
                             {
                                 aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                             }
                    }
                   
                
            
        }


    }
          

        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["EmployeePromotionEntryId"] = "";
            Session["EmployeePromotionEntryId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("EmployeePromotionEntry.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();
            string EIdd = loadGridView.DataKeys[rowindex][2].ToString();
            if (divisionId == "0")
            {
                aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
            }
            else
            {
                DataTable aTable =
                    atblEmployeePromotionEntryDAL.DeleteValidattionForEffectiveDate(divisionId.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {
                        int mainID = Convert.ToInt32(Convert.ToInt32(e.CommandArgument));
                        if (!CheckAchievementsAllocateOrNot(mainID))
                        {

                            if (!CheckPostedCsncel(mainID))
                            {

                                   DataTable dtSPPer =
                  aSuspendDal.checkSpecialPermission(EIdd.ToString());

                             if (dtSPPer.Rows.Count > 0)
                    {
                                Session["EmployeePromotionEntryId"] = "";
                                Session["EmployeePromotionEntryId"] = divisionId;
                                Session["Status"] = "Delete";
                                Response.Redirect("EmployeePromotionEntry.aspx");

                    }
                             else
                             {
                                 aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                             }
                            }
                            else
                            {
                                aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
                            }
                        }
                        else
                        {
                            aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
                        }
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
                    }
                }
            }
            //bool status = false;
            //if (!string.IsNullOrEmpty(loadGridView.DataKeys[rowindex][1].ToString()))
            //{
            //    status = Convert.ToBoolean(loadGridView.DataKeys[rowindex][1].ToString());
            //}
            //if (status)
            //{
            //    aShowMessage.ShowMessageBox("Employee Already Promoted !!", this);
            //}
            //else
            //{
            //    Response.Redirect("EmployeePromotionEntry.aspx");
            //}
        }

        if (e.CommandName == "Preview")
        {

            PopUp1(Convert.ToInt32(e.CommandArgument.ToString()));
        }

        //if (e.CommandName == "Preview")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);



        //    var datKey = loadGridView.DataKeys[0];

        //    if (datKey != null)
        //    {

        //        Response.Redirect("MemoPrintEmployeePromotion.aspx?mid=" + e.CommandArgument.ToString());
        //        Session["Status"] = "View";

        //    }



        //}

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string companyId = loadGridView.DataKeys[rowindex][0].ToString();

        //    if (atblEmployeePromotionEntryDAL.DeleteEmployeePromotionEntryById(companyId))
        //    {
        //        aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //        LoadInfo();
        //    }
        //}


        if (e.CommandName == "PrintLetter")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = loadGridView.DataKeys[0];

            if (datKey != null)
            {
                //Session["ApprovalPage"] = filepath;

                string EmployeeId = datKey["EmployeeId"].ToString();
                Session["Status"] = "Add";
                Response.Redirect("../AllPrintLetter/MemoPromotion.aspx?mid=" + e.CommandArgument.ToString() + "&EmpId=" + EmployeeId);


            }



        }
    }


    private bool CheckPostedCsncel(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblEmployeePromotionEntries
                      where t.EmployeePromotionEntryId == MainID && (t.ActionStatus2 != "Posted" || t.ActionStatus2 != "Cancel")
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }

    private void PopUp(Int32 EmployeePromotion)
    {
        string url = "../Report_UI/EmployeePromotionEntryViewReportViwer.aspx?rptType=" + EmployeePromotion;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    private void PopUp1(Int32 EmployeePromotion)
    {
        string url = "../Report_UI/EmployeePromotionEntryViewReportViwer.aspx?rptType=" + EmployeePromotion;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmployeePromotionEntry.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
  
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfo();
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeePromotionEntryView.aspx");
    }
}