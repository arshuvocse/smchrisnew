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

public partial class Transfer_UI_EmpTransferAndRedesignationView : System.Web.UI.Page
{
    IncrementDal aSuspendDal = new IncrementDal();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            GetCompany();
            UserPersmissionValidation();
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
        catch (Exception)
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
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
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

    private string Param2()
    {
        string parameter = "   ";


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (tblTransferHistory.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }

        //if (ddlCompany.SelectedValue != "")
        //{
        //    parameter = parameter + " AND CompanyName = '" + ddlCompany.SelectedItem.Text+"'";
        //}



        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND Wing= '" + ddlWing.SelectedItem.Text + "'";
        }

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND Division = '" + ddlDivision.SelectedItem.Text + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND Department = '" + ddlDepartment.SelectedItem.Text + "'";
        }

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND tblTransferHistory.EmployeeId=0 ";
        }
        //if (ddlFinYear.SelectedValue != "")
        //{
        //    parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        //}


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
            parameter = parameter + " AND EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    private void LoadEmpTransferAndRedesignationInfo()
    {

          if (ddlCompany.SelectedValue != "")
        {
            DataTable dataTable = aEmpTransferAndRedesignationDal.EmpTransferAndRedesignation(" WHERE ETR.CompanyId IN (" + CompanyId() + ") AND (ETR.IsDelete is null or ETR.IsDelete=0) " + GenerateParamiterList(), Param2(), GenerateParamiterList_SP(), chkIsSpecialTransfer.Checked);

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
            aShowMessage.ShowMessageBox("Data Not Found!!", this);
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
            parameter = parameter + " AND ETR.CompanyId = " + ddlCompany.SelectedValue;
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and (ETR.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
        }

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND ETR.NewDivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlWing.SelectedValue != "")
        {
            parameter = parameter + "  AND ETR.NewWingId = " + ddlWing.SelectedValue;
        }



        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND ETR.NewDepartmentId = " + ddlDepartment.SelectedValue;
        }

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND ETR.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }


         private string GenerateParamiterList_SP()
         {

             string parameter = "   ";


             parameter = parameter + " and ETR.IsOnlyTransfer=1";
           


             if (ddlEmpInfo.SelectedValue != "")
             {
                 parameter = parameter + "  and (ETR.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + " ) ";
             }

             if (ddlDivision.SelectedValue != "")
             {
                 parameter = parameter + "  AND ETR.NewDivisionId = " + ddlDivision.SelectedValue;
             }

             if (ddlWing.SelectedValue != "")
             {
                 parameter = parameter + "  AND ETR.NewWingId = " + ddlWing.SelectedValue;
             }



             if (ddlDepartment.SelectedIndex > 0)
             {
                 parameter = parameter + "  AND ETR.NewDepartmentId = " + ddlDepartment.SelectedValue;
             }

             if (ddlFinYear.SelectedValue != "")
             {
                 parameter = parameter + "  AND ETR.FinancialYearId = " + ddlFinYear.SelectedValue;
             }


             if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
             {
                 parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
             }
             if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
             {
                 parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
             }

             if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
             {
                 parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
             }

             return parameter;
         }
     

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmpTransferAndRedesignation.aspx");
    }

    private bool CheckPostedCsncel(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblEmpTransferAndRedesignations
                      where t.EmpTransferAndRedesignationId == MainID && (t.ActionStatus2 != "Posted" || t.ActionStatus2 != "Cancel")
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblEmpTransferAndRedesignations
                      where t.EmpTransferAndRedesignationId == MainID && t.AutoProcess == "Manually Updated"
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
           
            int? status = null;
            if (!string.IsNullOrEmpty(datKey[0].ToString()))
            {


                status = Convert.ToInt32(datKey[0].ToString());

                //DataTable dataTable = aEmpTransferAndRedesignationDal.EmpCheckLatestRowTransferAndRedesignation( Convert.ToInt32(status));

                //if (dataTable.Rows.Count >0)
                //{
                //    int PrimaryId = Convert.ToInt32(datKey[0]) ;
                //int Latestid=    Convert.ToInt32(dataTable.Rows[0].Field<Int32>("EmpTransferAndRedesignationId").ToString());

                //if (PrimaryId == Latestid)
                //    {

                DataTable aTable =
                    aEmpTransferAndRedesignationDal.DeleteValidattionForEffectiveDate(Convert.ToInt32(status).ToString());
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

                        Session["Status"] = "Edit";
                        Session["EmpTransferAndRedesignationId"] = "";
                        Session["EmpTransferAndRedesignationId"] = Convert.ToInt32(status).ToString();
                        Response.Redirect("EmpTransferAndRedesignation.aspx");

                              }
                            else
                            {
                                aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                            }
                        }
                        else
                        {
                            aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                        }
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                    }

                }
            }
            //        }
            //    else
            //    {
            //        aShowMessage.ShowMessageBox(" Can Not Be Edited !!", this);
            //    }
               
            //        //Session["Status"] = "Edit";
            //        //Session["EmpTransferAndRedesignationId"] = "";
            //        //Session["EmpTransferAndRedesignationId"] = idd;
            //        //Response.Redirect("EmpTransferAndRedesignation.aspx");
            //    }
            //    //else
            //    //{
            //    //    aShowMessage.ShowMessageBox("Employee A !!", this);
            //    //    string Id = datKey[0].ToString();
            //    //    if (datKey != null)
            //    //    {
            //    //        Session["Status"] = "Edit";
            //    //        Session["EmpTransferAndRedesignationId"] = "";
            //    //        Session["EmpTransferAndRedesignationId"] = Id;
            //    //        Response.Redirect("EmpTransferAndRedesignation.aspx");
            //    //    }
            //    //}

            //}

           
            //if (status>0)
            //{
            //    aShowMessage.ShowMessageBox("Employee Already transferd !!", this);
            //}
            //else
            //{
            //    Response.Redirect("EmpTransferAndRedesignation.aspx");
            //}

        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

              DataTable aTable =
                    aEmpTransferAndRedesignationDal.DeleteValidattionForEffectiveDate(Convert.ToInt32(divisionId).ToString());
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

            Session["EmpTransferAndRedesignationId"] = "";
            Session["EmpTransferAndRedesignationId"] = divisionId;
            Session["Status"] = "Delete";
            Response.Redirect("EmpTransferAndRedesignation.aspx");

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

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["EmpTransferAndRedesignationId"] = "";
            Session["EmpTransferAndRedesignationId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("EmpTransferAndRedesignation.aspx");

            //bool status = false;
            //if (!string.IsNullOrEmpty(loadGridView.DataKeys[rowindex][0].ToString()))
            //{
            //    status = Convert.ToBoolean(loadGridView.DataKeys[rowindex][0].ToString());
            //}
            //if (status)
            //{
            //    aShowMessage.ShowMessageBox("Employee Already transferd !!", this);
            //}
            //else
            //{
            //    Response.Redirect("EmpTransferAndRedesignation.aspx");
            //}
        }

        if (e.CommandName == "ViewReport")
        {

            PopUp(Convert.ToInt32(e.CommandArgument.ToString()));
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string companyId = loadGridView.DataKeys[rowindex][0].ToString();

        //    if (aEmpTransferAndRedesignationDal.DeleteEmpTransferAndRedesignationById(companyId))
        //    {
        //        aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //        LoadEmpTransferAndRedesignationInfo();
        //    }
        //}

    }

    private void PopUp(Int32 EmpTransferAndRedesignation)
    {
        string url = "../Report_UI/EmpTransferAndRedesignationReportViwer.aspx?rptType=" + EmpTransferAndRedesignation;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
            aEmpTransferAndRedesignationDal.FinancialYearDropDown(ddlFinYear, ddlCompany.SelectedValue);
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
            ddlEmpInfo.DataSource = null;
           
            ddlEmpInfo.DataBind();
        }
       
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadEmpTransferAndRedesignationInfo();
    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmpTransferAndRedesignationView.aspx");
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmpTransferAndRedesignationView.aspx");
    }
}