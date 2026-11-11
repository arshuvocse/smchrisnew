using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ExitManagement_DAL;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class ExitManagement_UI_EmployeeJobLeftEntryView : System.Web.UI.Page
{
    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();
    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    IncrementDal aSuspendDal = new IncrementDal();

    PermissionDAL aPermissionDal = new PermissionDAL();
    CommonDataLoadDAL aCommonDataLoadDal=new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
            //LoadInfo();
            LoadDropDownList();
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


    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
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
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
            aEmpTransferAndRedesignationDal.FinancialYearDropDown(ddlFinYear, ddlCompany.SelectedValue);
            aSuspendDal.LoadDivision(ddlDivision, ddlCompany.SelectedValue);



            using (DataTable dt222 = _commonDataLoad.GetEmpDDLOnlyView(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;



            }

            //CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
            //using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            //{
            //    ddlDepartment.DataSource = dt;
            //    ddlDepartment.DataValueField = "Value";
            //    ddlDepartment.DataTextField = "TextField";
            //    ddlDepartment.DataBind();
            //}
        }
        else
        {
            ddlFinYear.Items.Clear();
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

    private void LoadDropDownList()
    {
        aEmpTransferAndRedesignationDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }
    public void GetCompany()
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
            DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadInformationALlList(GenerateParamiterList(), GenerateParamiterListTransfer(), ddlCompany.SelectedValue);

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


    private string GenerateParamiterList()
    {

        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND Emp.CompanyId = " + ddlCompany.SelectedValue;
        }
        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (EPE.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
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
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }



    private string GenerateParamiterListTransfer()
    {

        string parameter = "   ";

        
        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Emp.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (reff.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
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
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmployeeJobLeftEntry.aspx"); 
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ExitFormReport")
        {
            int empId = Convert.ToInt32(e.CommandArgument);

            if (empId > 0)
            {
                try
                {
                    DataTable aTable = aEmployeeJobLeftEntryDAL.CheckEmpExitFormInfo(empId);

                    if (aTable.Rows.Count > 0)
                    {
                        PopUp(Convert.ToInt32(empId));
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("There is no exit interview info exist !!", this);
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox("There is no exit interview info exist !!", this);
                    
                    //throw;
                }
            }
        }


        if (e.CommandName == "Clearence")
        {
            int empId = Convert.ToInt32(e.CommandArgument);

            if (empId > 0)
            {
                DataTable aTable = aEmployeeJobLeftEntryDAL.CheckEmpClearenceFormInfo(empId);

                if (aTable.Rows.Count > 0)
                {
                    PopUpClearence(Convert.ToInt32(empId));
                }
                else
                {
                    aShowMessage.ShowMessageBox("There is no clearence info exist !!", this);
                }
            }
        }

        //if (e.CommandName == "PrintLetter")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);



        //    var datKey = loadGridView.DataKeys[rowindex];

        //    if (datKey != null)
        //    {
        //        //Session["ApprovalPage"] = filepath;
                
        //        string Idd = datKey[1].ToString();
        //        Session["Status"] = "Add";
        //        Response.Redirect("../AllPrintLetter/plSeparation.aspx?mid=" + e.CommandArgument.ToString() + "&EmpId=" + Idd);


        //    }



        //}

        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                string EIdd = datKey[1].ToString();
                  DataTable aTable =
                            aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(Idd.ToString());
                
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
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
                        Session["EmployeeJobLeftId"] = "";
                        Session["EmployeeJobLeftId"] = Idd;

                        Session["Status"] = "Edit";
                        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
                    }
                             else
                             {
                                 aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                             }
                              }
                              else
                              {
                                  aShowMessage.ShowMessageBox("Data Can not be Edited or Deleted !!!", this);
                              }
                          }
                          else
                          {
                              aShowMessage.ShowMessageBox("Data Can not be Edited or Deleted !!!", this);
                          }
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                    }
                }
            }
            //}
            //bool status = false;
            //if (!string.IsNullOrEmpty(datKey[1].ToString()))
            //{
            //    status = Convert.ToBoolean(datKey[1].ToString());
            //}
            //if (status)
            //{
            //    aShowMessage.ShowMessageBox("Employee Already Job Left !!", this);
            //}
            //else
            //{
            //    Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
            //}

        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                //DataTable aTable =
                //    aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(Idd.ToString());
                //if (aTable.Rows.Count > 0)
                //{
                //    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
                //    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                //    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                //    {

                        Session["EmployeeJobLeftId"] = "";
                        Session["EmployeeJobLeftId"] = Idd;
                        Session["Status"] = "View";
                        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
                        
                    //}
                    //else
                    //{
                    //    aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
                    //}

               // }
            }

        }



        if (e.CommandName == "DeleteData")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];

            if (datKey != null)
            {

                 string Idd = datKey[0].ToString();
                 string EIdd = datKey[1].ToString();
                  DataTable aTable =
                            aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(Idd.ToString());

                  if (aTable.Rows.Count > 0)
                  {
                      string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
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
                                  Session["EmployeeJobLeftId"] = "";
                                  Session["EmployeeJobLeftId"] = Idd;
                                  Session["Status"] = "Delete";
                                  Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
                    }
                             else
                             {
                                 aShowMessage.ShowMessageBox("Special Permission Required! Data can not be Edited !!", this);
                             }
                              }
                              else
                              {
                                  aShowMessage.ShowMessageBox("Data Can not be Edited or Deleted !!!", this);
                              }
                          }
                          else
                          {
                              aShowMessage.ShowMessageBox("Data Can not be Edited or Deleted !!!", this);
                          }
                      }
                      else
                      {
                          aShowMessage.ShowMessageBox("Data can not be Edited or Deleted !!", this);
                      }
                  }

               
            }
            //bool status = false;
            //if (!string.IsNullOrEmpty(datKey[1].ToString()))
            //{
            //    status = Convert.ToBoolean(datKey[1].ToString());
            //}
            //if (status)
            //{
            //    aShowMessage.ShowMessageBox("Employee Already Job Left !!", this);
            //}
            //else
            //{
            //    Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
            //}
            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string EmployeeJobLeftId = loadGridView.DataKeys[rowindex][0].ToString();

            //if (aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftById(EmployeeJobLeftId))
            //{
            //    LoadInfo();
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);

            //}
        }
    }

    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblEmployeeJobLefts
                      where t.EmployeeJobLeftId == MainID && t.AutoProcess == "Manually Updated"
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
            result = (from t in db.tblEmployeeJobLefts
                      where t.EmployeeJobLeftId == MainID && (t.ActionStatus2 != "Posted" || t.ActionStatus2 != "Cancel")
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }

    private void PopUpClearence(int empId)
    {
        string url = "../Report_UI/ClearenceReportViwer.aspx?rptType=" + empId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
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
    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    private void PopUp(int empId)
    {
        string url = "../Report_UI/ExitManagementReportViwer.aspx?rptType=" + empId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue!="")
        {
            LoadInfo();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("Please Select Company!!", this);
        }
       
    }

    protected void lb_SendMail_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Add";
      

        HiddenField EmployeeId = (HiddenField)loadGridView.Rows[rowID].FindControl("EmployeeId");
        HiddenField EmployeeJobLeftId = (HiddenField)loadGridView.Rows[rowID].FindControl("EmployeeJobLeftId");
        Response.Redirect("../AllPrintLetter/plSeparation.aspx?mid=" + EmployeeJobLeftId.Value + "&EmpId=" + EmployeeId.Value);
        
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeJobLeftEntryView.aspx");
    }
}