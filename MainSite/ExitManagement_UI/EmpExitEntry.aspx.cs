using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;

public partial class Survey_EmpExitEntry : System.Web.UI.Page
{
    EmpExitDal aExitDal = new EmpExitDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();
        }
        
    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
            }
            
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("EmpExitView.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void LoadDepartmentCheckBoxList()
    {
        DataTable aTable = aExitDal.LoadExitDepartment(ddlCompany.SelectedValue);

        loadGridView.DataSource = aTable;
        loadGridView.DataBind();
    }
    private void LoadDepartmentCheckBoxListNotInEmpDept(string empinfoId)
    {
          DataTable aTable =new DataTable();
        if (ddlCompany.SelectedValue=="1")
        {
              aTable = aExitDal.LoadExitDepartmentNotInEmployee(ddlCompany.SelectedValue, empinfoId);
        }
        else
        {
            aTable = aExitDal.LoadExitDepartmentNotInEmployeeSmcEL(ddlCompany.SelectedValue, empinfoId);
            
        }

        loadGridView.DataSource = aTable;
        loadGridView.DataBind();


        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            int DepartmentId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
            ListBox ddlEmpInfoList = (ListBox)loadGridView.Rows[i].FindControl("ddlEmpInfoList");
            ddlEmpInfoList.Enabled = false;

            if (ddlCompany.SelectedValue == "1")
            {

                if (DepartmentId.ToString() == "129")
                {
                    using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMentSMCCel(DepartmentId.ToString()))
                    {
                        ddlEmpInfoList.DataSource = dt222;
                        ddlEmpInfoList.DataValueField = "EmpInfoId";
                        ddlEmpInfoList.DataTextField = "EmpName";
                        ddlEmpInfoList.DataBind();
                    }
                }
                else
                {
                    using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMent(ddlCompany.SelectedValue, DepartmentId.ToString()))
                    {
                        ddlEmpInfoList.DataSource = dt222;
                        ddlEmpInfoList.DataValueField = "EmpInfoId";
                        ddlEmpInfoList.DataTextField = "EmpName";
                        ddlEmpInfoList.DataBind();
                    }

                }

            }
            else
            {
                using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMentSMCCel(DepartmentId.ToString()))
                {
                    ddlEmpInfoList.DataSource = dt222;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                } 
            }

        }
    }

    private void LoadDropDownList()
    {
        aExitDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveDataValidation())
        {
            Int32 masterId = aExitDal.SaveExitMasterInfo(PrepareDateForMasterSave());

            if (masterId > 0)
            {
                Int32 id = SaveExitDetailInfo(PrepareDateForDetailSave(masterId));
                



                if (id > 0)
                {

                    List<EmpExitDocumentDAO> DocList = new List<EmpExitDocumentDAO>();
                    for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
                    {
                        HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                        Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                        HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfFileName");


                        EmpExitDocumentDAO DocA = new EmpExitDocumentDAO();

                        DocA.DocumentLink = hfDocumentLink.Value.ToString();
                        DocA.FileName = hfFileName.Value.ToString();
                        DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                        

                        DocList.Add(DocA);
                    }

                    aExitDal.SaveDocumentDetails(DocList, masterId);

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
             "alert",
             "alert('Opearation Successful...');window.location ='EmpExitEntry.aspx';",
             true);
                    Clear();
                }
            }
        }
    }

    public void LoadReportingEmpWithDept()
    {
        //hfDivision.Value == "18";
        if (hfDivision.Value == "48")
        {
            DataTable dtdata = LoadSupervisor(hfEmpInfoId.Value);
            GridView1.DataSource = dtdata;
            GridView1.DataBind();
        }
        {
            DataTable dtdata =
                aExitDal.LoadEmployeeInfoParameter("WHERE EmpInfoId IN (" +
                                                   GetReportingEmpId(hfEmpInfoId.Value) + ")");
            GridView1.DataSource = dtdata;
            GridView1.DataBind();
        }
       
    }

    public DataTable LoadSupervisor(string empinfoid)
    {
        //SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("EmpMasterCode");
        aDataTable.Columns.Add("DepartmentId");
        //DataRow dataRow = null;
        //dataRow = aDataTable.NewRow();
        //dataRow["EmpInfoId"] = "0";
        //dataRow["EmpName"] = "Please Select an Employee.....";
        //dataRow["EmpMasterCode"] = "";
        //aDataTable.Rows.Add(dataRow);
        aExitDal.ReportingEmpData(empinfoid, aDataTable);

        return aDataTable;
    }
    public string GetReportingEmpId(string empinfoId)
    {
        DataTable dtdata = aExitDal.LoadEmployeeInfoDeptWise(empinfoId);
        if (dtdata.Rows.Count>0)
        {
            return "'" + dtdata.Rows[0]["ReportingEmpId"].ToString() + "'" ;
        }
        else
        {
            return "'0'";
        }
    }
    private int SaveExitDetailInfo(List<EmpExitDetailDao> aList)
    {
        Int32 id = 0;

        foreach (var aDao in aList)
        {
            id = aExitDal.SaveExitDetailInfo(aDao);
        }

        return id;
    }
    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("shuvosmtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("shuvono-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("shuvono-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }


    private List<EmpExitDetailDao> PrepareDateForDetailSave(int masterId)
    {
        List<EmpExitDetailDao> aDaos = new List<EmpExitDetailDao>();
        EmpExitDetailDao aDao;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {

            CheckBox check = (CheckBox)GridView1.Rows[i].FindControl("chkEmpSelect");
            if (check.Checked)
            {
                aDao = new EmpExitDetailDao();
                aDao.MasterId = masterId;
                aDao.EmpInfoId = Convert.ToInt32(GridView1.DataKeys[i][1].ToString());
                try
                {
                    aDao.DepartmentId = Convert.ToInt32(GridView1.DataKeys[i][0].ToString());
                    aDao.SetInfo = "Dep";
                }
                catch (Exception)
                {

                    DataTable aTable = aExitDal.LoadEmpDivision(aDao.EmpInfoId.ToString());
                    if (aTable.Rows.Count>0)
                    {
                        aDao.DepartmentId = Convert.ToInt32(aTable.Rows[0]["DivisionId"].ToString());
                        aDao.SetInfo = "Div";

                    }
                    
                    else
                    {
                        aDao.DepartmentId = 0;
                        aDao.SetInfo = "Div";
                    }

                }
               
                aDao.EmployeeIdForClearance = Convert.ToInt32(hfEmpInfoId.Value);
                aDao.ApprovalStatus = "as Supervisor";

//                SenMailForApprved(aDao.EmpInfoId, "  Clearence Form Review ", @"  <br/> Dear Sir, <br/>
//Clearence Form Review is waiting for your Confirmation (as a Supervisor). <br/><br/>
// please login for the details from the below link.<br/>  http://182.160.103.234:8088/ <br/>
//Thank You.
//");
                aDaos.Add(aDao);
            }


        }
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].FindControl("chkSelect");
            ListBox ddlEmpInfoList = (ListBox)loadGridView.Rows[i].FindControl("ddlEmpInfoList");

            if (chkBoxRows != null && chkBoxRows.Checked)
            {
                foreach (ListItem item in ddlEmpInfoList.Items)
                {
                    if (item.Selected && !string.IsNullOrEmpty(item.Value))
                    {
                        aDao = new EmpExitDetailDao();
                        aDao.MasterId = masterId;
                        aDao.EmpInfoId = Convert.ToInt32(item.Value);
                        aDao.EmployeeIdForClearance = Convert.ToInt32(hfEmpInfoId.Value);
                        var dataKey = loadGridView.DataKeys[i][0].ToString();
                        aDao.DepartmentId = Convert.ToInt32(dataKey);
                        aDao.ApprovalStatus = "as Department";
                        aDao.SetInfo = (loadGridView.DataKeys[i][1].ToString());
                        aDaos.Add(aDao);
                    }
                }
            }
        }

        return aDaos;
    }

    private EmpExitMasterDao PrepareDateForMasterSave()
    {
        EmpExitMasterDao aMasterDao = new EmpExitMasterDao();

        aMasterDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aMasterDao.EmployeeId = Convert.ToInt32(hfEmpInfoId.Value);
        aMasterDao.EmpCode = empCode.Text.Trim();
        aMasterDao.EmpName = empName.Text.Trim();
        aMasterDao.JoiningDate = Convert.ToDateTime(dtJoining.Text.Trim());
        try
        {
            aMasterDao.DesignationId = Convert.ToInt32(hfDesignation.Value);
        }
        catch
        {
            aMasterDao.DesignationId = 0;
        }
        aMasterDao.DivisionId = Convert.ToInt32(hfDivision.Value);
        if (hfSalaryGrade.Value!="")
        {
            aMasterDao.SalaryGradeId = Convert.ToInt32(hfSalaryGrade.Value);
        }
     
        aMasterDao.Description = descriptionTextbox.Text.Trim();

        aMasterDao.ActionStatus = "Posted";

        aMasterDao.EntryBy = Session["LoginName"].ToString();
        aMasterDao.EntryDate = DateTime.Now;

        return aMasterDao;
    }

    private bool SaveDataValidation()
    {
        if (ddlCompany.SelectedValue == "")
        {
            ShowMessageBox("You have to select company !!!");
            return false;
        }

        if (hfEmpInfoId.Value == "")
        {
            ShowMessageBox("You have to select employee !!!");
            return false;
        }

        // Server-side guard: blocks submit even if the select-time check was bypassed
        if (aExitDal.IsEmployeeDeclaredInClearanceAutoForward(hfEmpInfoId.Value))
        {
            ShowMessageBox(AutoForwardDuplicateMsg);
            return false;
        }



        Int32 count2 = 0;

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkEmpSelect");

            if (chkBoxRows.Checked)
            {
                count2++;
            }

            if (count2 > 0)
            {
                break;
            }
        }

        if (count2 == 0)
        {
            ShowMessageBox("You have to select at least one Supervisor !!!");
            return false;
        }

        Int32 count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].FindControl("chkSelect");

            if (chkBoxRows != null && chkBoxRows.Checked)
            {
                count ++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("You have to select at least one department !!!");
            return false;
        }

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].FindControl("chkSelect");
            ListBox ddlEmpInfoList = (ListBox)loadGridView.Rows[i].FindControl("ddlEmpInfoList");

            if (chkBoxRows != null && chkBoxRows.Checked)
            {
                bool hasSelection = false;
                int selectedCount = 0;
                foreach (ListItem item in ddlEmpInfoList.Items)
                {
                    if (item.Selected && !string.IsNullOrEmpty(item.Value))
                    {
                        hasSelection = true;
                        selectedCount++;
                    }
                }
                if (!hasSelection)
                {
                    ShowMessageBox("Employee is required !!!");
                    ddlEmpInfoList.Focus();
                    return false;
                }

                //string deptName = HttpUtility.HtmlDecode(loadGridView.Rows[i].Cells[2].Text).Trim();
                //if (deptName.Equals("Information and Communication Technology", StringComparison.OrdinalIgnoreCase))
                //{
                //    if (selectedCount > 1)
                //    {
                //        ShowMessageBox("For Information and Communication Technology department, you cannot select more than one employee !!!");
                //        ddlEmpInfoList.Focus();
                //        return false;
                //    }
                //}
            }
        }

        return true;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlCompany.SelectedValue = "";
        txt_EmpName.Text = "";
        hfEmpInfoId.Value = "";
        empName.Text = "";
        empCode.Text = "";
        ddlDivision.Text = "";
        hfDivision.Value = "";
        ddlDesignation.Text = "";
        hfDesignation.Value = "";
        ddlSalaryGrade.Text = "";
        hfSalaryGrade.Value = "";

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        txt_EmpName.Enabled = false;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        using (DataTable dt222 = aExitDal.GetEmpDDLiSiNACTIVE(ddlCompany.SelectedValue.ToString()))
        {



            ddlEmpInfo.DataSource = dt222;
            ddlEmpInfo.DataValueField = "EmpInfoId";
            ddlEmpInfo.DataTextField = "EmpName";
            ddlEmpInfo.DataBind();
            ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            ddlEmpInfo.SelectedIndex = 0;
        }
        //if (ddlCompany.SelectedValue != "")
        //{
        //    txt_EmpName.Enabled = true;

        //    Session["CompanyId"] = "";
        //    Session["CompanyId"] = ddlCompany.SelectedValue;

        //    //LoadDepartmentCheckBoxList();
        //}
        //else
        //{
        //    Session["CompanyId"] = "";
        //    txt_EmpName.Enabled = false;
        //}


    }
    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
            AddNewDocGrid_List();

        }
    }
    protected void btnDocRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["DocGrid_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = dt;
                gv_DocumentUpload.DataBind();
            }
            else
            {
                ViewState["DocGrid_List"] = null;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = null;
                gv_DocumentUpload.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetDocGrid_List();
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

    private bool docVali()
    {
        lblMsg.Text = "";
        if (hfDocFile.Value == "")
        {
            ShowMessageBox("Please click Document Upload Button");

            return false;
        }
        //if (txtSummaryNote.Text == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);
        //    lblMsg.Text = "<b>" + hfDocFileName.Value + "</b> has been uploaded.";
        //    return false;
        //}
        return true;

    }

    private void AddNewDocGrid_List()
    {
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DocGrid_List"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;
                //drCurrentRow["DocumentLink"] =  @"file:///D:/UploadMeetingDocument/"+ hfDocFile.Value;
                drCurrentRow["FileName"] = hfDocFileName.Value;




                drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();

                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["DocGrid_List"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_DocumentUpload.DataSource = dtCurrentTable;
                gv_DocumentUpload.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentNote", typeof(string)));
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));


            dr = dt.NewRow();

            dr["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;

            //  dr["DocumentLink"] = @"file:///D:/UploadMeetingDocument/3eec2898121c4467be57981c13852a9e.png"; //@"file:///D:/UploadMeetingDocument/" + hfDocFile.Value;
            dr["FileName"] = hfDocFileName.Value;


            dr["DocumentNote"] = txtSummaryNote.Text.Trim();
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["DocGrid_List"] = dt;

            //Bind the Gridview   
            gv_DocumentUpload.DataSource = dt;
            gv_DocumentUpload.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetDocGrid_List();


        txtSummaryNote.Text = string.Empty;
        // HyperLink2.Text = "No File Uploaded";
        HyperLink2.NavigateUrl = "";
        hfDocFile.Value = "";
    }

    private void SetDocGrid_List()
    {
        int rowIndex = 0;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLink");
                    HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfFileName");
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");


                    if (i < dt.Rows.Count - 1)
                    {
                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }

    public void GetDepartmentContextKey()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            TextBox employeeTextBox = (TextBox)loadGridView.Rows[i].FindControl("employeeTextBox");

            AjaxControlToolkit.AutoCompleteExtender modal = (AjaxControlToolkit.AutoCompleteExtender)employeeTextBox.FindControl("empAutoCompleteExtender1");
            modal.ContextKey = loadGridView.DataKeys[i][0].ToString();
            //drCheckBox.Checked = false;
        }
    }
    public void GetDepartmentContextKey(int row)
    {
        int i = row;
        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            TextBox employeeTextBox = (TextBox)loadGridView.Rows[i].FindControl("employeeTextBox");

            AjaxControlToolkit.AutoCompleteExtender modal = (AjaxControlToolkit.AutoCompleteExtender)employeeTextBox.FindControl("empAutoCompleteExtender1");
            modal.ContextKey = loadGridView.DataKeys[i][0].ToString();
            //drCheckBox.Checked = false;
        }
    }
    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {
        SetEmployeeInfo();

        if (hfEmpInfoId.Value != "")
        {
            DataTable aTable = aExitDal.LoadEmployeeInfo(hfEmpInfoId.Value, ddlCompany.SelectedValue);

            if (aTable.Rows.Count > 0)
            {
                ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                try
                {
                    hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    hfDivision.Value = "0";
                    //throw;
                }
                deptHiddenField.Value = aTable.Rows[0].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);
                ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                try
                {
                    hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    hfSalaryGrade.Value = "";
                    //throw;
                }

                empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                empName.Text = aTable.Rows[0].Field<string>("EmpName");

                dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");
                LoadDepartmentCheckBoxListNotInEmpDept(hfEmpInfoId.Value);
                //GetDepartmentContextKey();
                LoadReportingEmpWithDept();
            }
            else
            {
                txt_EmpName.Text = "";
                ShowMessageBox("No Information found !!!");
            }
        }
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    private void SetEmployeeInfo()
    {
        string empName = txt_EmpName.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            hfEmpInfoId.Value = emp[2];
            txt_EmpName.Text = emp[1].Trim();
        }
        else
        {
            txt_EmpName.Text = "";
            hfEmpInfoId.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }

       // txt_EmpName.Text = "";
    }


    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
            ListBox ddlEmpInfoList = (ListBox)loadGridView.Rows[i].FindControl("ddlEmpInfoList");
          
            if (chkBoxRows.Checked)
            {
                var Div = loadGridView.DataKeys[i][1].ToString();
                var DivId = loadGridView.DataKeys[i][0].ToString();
                if (Div == "Div")
                {
                    ddlEmpInfoList.Enabled = true;
                    using (DataTable dt22 = _commonDataLoad.GetEmpDDLAActivebyDivisionId(ddlCompany.SelectedValue, DivId))
                    {
                        if (ddlCompany.SelectedValue == "1")
                        {
                            using (DataTable dt22_2 = _commonDataLoad.GetEmpDDLAActivebyDivisionId("2", DivId))
                            {
                                dt22.Merge(dt22_2);
                            }
                        }
                        ddlEmpInfoList.DataSource = dt22;
                        ddlEmpInfoList.DataValueField = "EmpInfoId";
                        ddlEmpInfoList.DataTextField = "EmpName";
                        ddlEmpInfoList.DataBind();
                    }
                }
                else
                {
                    ddlEmpInfoList.Enabled = true;
                    var DepartmentId = DivId;
                    if (ddlCompany.SelectedValue == "1")
                    {
                        if (DepartmentId == "129")
                        {
                            using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMentSMCCel(DepartmentId))
                            {
                                ddlEmpInfoList.DataSource = dt222;
                                ddlEmpInfoList.DataValueField = "EmpInfoId";
                                ddlEmpInfoList.DataTextField = "EmpName";
                                ddlEmpInfoList.DataBind();
                            }
                        }
                        else
                        {
                            using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMent(ddlCompany.SelectedValue, DepartmentId))
                            {
                                ddlEmpInfoList.DataSource = dt222;
                                ddlEmpInfoList.DataValueField = "EmpInfoId";
                                ddlEmpInfoList.DataTextField = "EmpName";
                                ddlEmpInfoList.DataBind();
                            }
                        }
                    }
                    else
                    {
                        using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMentSMCCel(DepartmentId))
                        {
                            ddlEmpInfoList.DataSource = dt222;
                            ddlEmpInfoList.DataValueField = "EmpInfoId";
                            ddlEmpInfoList.DataTextField = "EmpName";
                            ddlEmpInfoList.DataBind();
                        }
                    }
                }
            }
            else
            {
                ddlEmpInfoList.ClearSelection();
                ddlEmpInfoList.Enabled = false;
            }
            GetDepartmentContextKey(i);
        }
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox dropDown = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        
        var chkBoxRows = (CheckBox)loadGridView.Rows[rowindex].FindControl("chkSelect");
        ListBox ddlEmpInfoList = (ListBox)loadGridView.Rows[rowindex].FindControl("ddlEmpInfoList");

        if (chkBoxRows != null && chkBoxRows.Checked)
        {
            var Div = loadGridView.DataKeys[rowindex][1].ToString();
            var DivId = loadGridView.DataKeys[rowindex][0].ToString();
            if (Div == "Div")
            {
                ddlEmpInfoList.Enabled = true;
                using (DataTable dt22 = _commonDataLoad.GetEmpDDLAActivebyDivisionId(ddlCompany.SelectedValue, DivId))
                {
                    if (ddlCompany.SelectedValue == "1")
                    {
                        using (DataTable dt22_2 = _commonDataLoad.GetEmpDDLAActivebyDivisionId("2", DivId))
                        {
                            dt22.Merge(dt22_2);
                        }
                    }
                    ddlEmpInfoList.DataSource = dt22;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                }
            }
            else
            {
                ddlEmpInfoList.Enabled = true;
                var DepartmentId = DivId;
                if (ddlCompany.SelectedValue == "1")
                {
                    if (DepartmentId == "129")
                    {
                        using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMentSMCCel(DepartmentId))
                        {
                            ddlEmpInfoList.DataSource = dt222;
                            ddlEmpInfoList.DataValueField = "EmpInfoId";
                            ddlEmpInfoList.DataTextField = "EmpName";
                            ddlEmpInfoList.DataBind();
                        }
                    }
                    else
                    {
                        using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMent(ddlCompany.SelectedValue, DepartmentId))
                        {
                            ddlEmpInfoList.DataSource = dt222;
                            ddlEmpInfoList.DataValueField = "EmpInfoId";
                            ddlEmpInfoList.DataTextField = "EmpName";
                            ddlEmpInfoList.DataBind();
                        }
                    }
                }
                else
                {
                    using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMentSMCCel(DepartmentId))
                    {
                        ddlEmpInfoList.DataSource = dt222;
                        ddlEmpInfoList.DataValueField = "EmpInfoId";
                        ddlEmpInfoList.DataTextField = "EmpName";
                        ddlEmpInfoList.DataBind();
                    }
                }
            }
        }
        else
        {
            ddlEmpInfoList.ClearSelection();
            ddlEmpInfoList.Enabled = false ;
        }
        GetDepartmentContextKey(rowindex);
    }

    protected void employeeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        TextBox dropDown = (TextBox)sender;
        GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;     
        
        SetEmployeeInfo(rowindex);

    }

    private void SetEmployeeInfo(int rowindex)
    {
        var empNameTextBox = (TextBox)loadGridView.Rows[rowindex].Cells[0].FindControl("employeeTextBox");
        var empIdTextBox = (HiddenField)loadGridView.Rows[rowindex].Cells[0].FindControl("hdfEmpInfoId");
        
        string empName = empNameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            if (CheckEmpIdExistOrNot(emp[2], rowindex))
            {
                empIdTextBox.Value = emp[2];
                empNameTextBox.Text = emp[1];
            }
            else
            {
                empIdTextBox.Value = "";
                empNameTextBox.Text = "";
                ShowMessageBox("Employee already Exist !!");
            }
           
        }
        else
        {
            empIdTextBox.Value = "";
            empNameTextBox.Text = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }

    private bool CheckEmpIdExistOrNot(string empid, int rowindex)
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            if (i != rowindex)
            {
                //var empIdTextBox = (HiddenField)loadGridView.Rows[rowindex].Cells[0].FindControl("hdfEmpInfoId");
                var empIdTextBox1 = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hdfEmpInfoId");

                if (empIdTextBox1.Value == empid)
                {
                    return false;
                }
                
            }
        }

        return true;
    }
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();

    public void GetEmpinfo(string id)
    {
        //string empid = txt_employee.Text.Trim();
        //if (empid.Contains(":"))
        {
            //string[] strsp = empid.Split(':');
            //int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);

            DataTable dtEmp = _appPartA.GetEmployeeDetails(Convert.ToInt32(id));
            if (dtEmp.Rows.Count > 0)
            {


                lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();




                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();




            }
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    id_Empid.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    public bool CheckExistExitEmp(string id)
    {
        DataTable dtdata = aExitDal.CheckExistEmployee(id);
        if (dtdata.Rows.Count>0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private const string AutoForwardDuplicateMsg = "This employee has already been declared for clearance through Auto Forward. Duplicate clearance requests are not allowed.";

    protected void ddlEmpInfo_OnTextChanged(object sender, EventArgs e)
    {
       if (ddlEmpInfo.SelectedValue != "" && aExitDal.IsEmployeeDeclaredInClearanceAutoForward(ddlEmpInfo.SelectedValue))
        {
            hfEmpInfoId.Value = "";
            ddlEmpInfo.SelectedIndex = 0;
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            ShowMessageBox(AutoForwardDuplicateMsg);
            return;
        }

       if (CheckExistExitEmp(ddlEmpInfo.SelectedValue)==false)
        {


            hfEmpInfoId.Value = ddlEmpInfo.SelectedValue;

            if (hfEmpInfoId.Value != "")
            {
                GetEmpinfo(hfEmpInfoId.Value);
                DataTable aTable = aExitDal.LoadEmployeeInfo(hfEmpInfoId.Value, ddlCompany.SelectedValue);

                if (aTable.Rows.Count > 0)
                {
                    ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                    try
                    {
                        hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId")
                            .ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        hfDivision.Value = "0";
                        //throw;
                    }

                    try
                    {
                    deptHiddenField.Value = aTable.Rows[0].Field<Int32>("DepartmentId")
                        .ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                    try
                    {
                        hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId")
                            .ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                    try
                    {
                        hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId")
                            .ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        hfSalaryGrade.Value = "";
                        //throw;
                    }

                    empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                    empName.Text = aTable.Rows[0].Field<string>("EmpName");

                    dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");
                    LoadDepartmentCheckBoxListNotInEmpDept(hfEmpInfoId.Value);
                    //GetDepartmentContextKey();
                    LoadReportingEmpWithDept();
                }
                else
                {
                    txt_EmpName.Text = "";
                    ShowMessageBox("No Information found !!!");
                }
            }
        }
        else
        {
            ShowMessageBox("Employee Information Already Exist");
        }
    }

    protected void ddlEmpInfoList_OnTextChanged(object sender, EventArgs e)
    {
        
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
         Response.Redirect("EmpExitView.aspx");
    }
}