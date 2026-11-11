using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.MeetingMinorsDAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class ContractualEmployeeManagement_UI_ContractualEmpApprovalList : System.Web.UI.Page
{
    ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();

            //UserPersmissionValidation();
            LoadInfo();

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
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();


    private void LoadInfo()
    {

        using (DataTable dt222 = _commonDataLoad.GetEmpDDLForWithoutCompany())
        {



            ddlEmpInfo.DataSource = dt222;
            ddlEmpInfo.DataValueField = "EmpInfoId";
            ddlEmpInfo.DataTextField = "EmpName";
            ddlEmpInfo.DataBind();
            ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            ddlEmpInfo.SelectedIndex = 0;
        }
        DataTable dataTable = aContractualEmpManageDAL.LoadInformationAppALl("");

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            int count = 0;
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {

              
                DataTable dtdata = null;

                string filepath = "../ContractualEmployeeManagement_UI/ContractualEmpApprovalList.aspx";
                 dtdata = aContractualEmpManageDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                               " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");


                 if (dtdata.Rows.Count>0)
                 {
                     CheckBox chkSelect = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
                     HiddenField hfContractualEmpManageId = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hfContractualEmpManageId");

                     DataTable dtdatainfo =
                     aContractualEmpManageDAL.GetContractualDataInfo((hfContractualEmpManageId.Value.ToString()));

                     bool isselfapp = false;
                     if (dtdatainfo.Rows.Count > 0)
                     {
                         try
                         {
                             isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                         }
                         catch (Exception)
                         {

                             //throw;
                         }
                     }

                     if (isselfapp == true)
                     {
                         count = count + 1;
                         chkSelect.Visible = true;
                     }
                     
                     
                 }
            }
            if (count>0)
            {
                btnForWordDiv.Visible = true;
            }
           
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            Session["Status"] = "Edit";
            Session["AppLogId"] = loadGridView.DataKeys[rowindex][1].ToString();

            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            Session["AppPage"] = filepath;

            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                  Session["Status"] = "Edit";
                Session["ContractualEmpManageId"] = "";
                Session["ContractualEmpManageId"] = Idd;
                Response.Redirect("ContractualEmpApproval.aspx");
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

            Session["ContractualEmpManageId"] = "";
            Session["ContractualEmpManageId"] = divisionId;
            Session["Status"] = "Delete";
            Response.Redirect("~/ContractualEmployeeManagement_UI/ContractualEmpManagement.aspx");
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
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {

        Response.Redirect("ContractualEmpList.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
         
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
       
    }

    protected void btnBehavioralClose_OnClick(object sender, EventArgs e)
    {
        MPBehavioral.Hide();

    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {

        //if (ddlEmpInfo.SelectedValue!="")
        //{
        //    for (int i = 0; i < loadGridView.Rows.Count; i++)
        //    {
        //        CheckBox chkSelect = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
        //        if (chkSelect.Checked)
        //        {
        //            HiddenField hfContractualEmpManageId = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hfContractualEmpManageId");

        //            ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
        //            aMaster.ContractualEmpManageId
        //                = Convert.ToInt32(hfContractualEmpManageId.Value);
        //            aMaster.ActionStatus = "Verified";
        //            bool status = true; //aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
        //            if (status)
        //            {

        //                //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
        //                ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
        //                {
        //                    appLogDao.ActionStatus = "Verified";
        //                    appLogDao.ApproveDate = DateTime.Now;
        //                    appLogDao.ApproveBy = Session["UserId"].ToString();
        //                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
        //                    appLogDao.ForEmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
        //                    appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
        //                    appLogDao.Comments = "";
        //                    appLogDao.CommentsId = 0;

        //                }
        //                int id = aContractualEmpManageDAL.SaveEmpContractAppLogForWard(appLogDao);

        //                if (id > 0)
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                 "alert",
        //                 "alert('Operation Successfully Done......');window.location ='ContractualEmpApprovalList.aspx';",
        //                 true);
        //                }
        //                else
        //                {
        //                    aShowMessage.ShowMessageBox("Operation Faild!!", this);
        //                }
        //            }
        //        }

        //    }
        //}
        //else
        //{
        //    aShowMessage.ShowMessageBox("Please Select an Employee!!", this);
            
        //}



        List<MiscellaneousInfoRoutingPathDAO> RoutingPath = new List<MiscellaneousInfoRoutingPathDAO>();
        int _RefEmpId = 0;
        int _RefSeqNo = 0;
       


            for (int kk = 0; kk < loadGridView.Rows.Count; kk++)
            {
                CheckBox chkSelect = (CheckBox)loadGridView.Rows[kk].Cells[0].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    HiddenField hfContractualEmpManageId =
                        (HiddenField)loadGridView.Rows[kk].Cells[0].FindControl("hfContractualEmpManageId");

                    int pk = Convert.ToInt32(hfContractualEmpManageId.Value);
                    for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                    {
            DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


            HiddenField ShfEmpInfoId = (HiddenField)gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId");



            MiscellaneousInfoRoutingPathDAO Routing = new MiscellaneousInfoRoutingPathDAO();

          
 
            Routing.Seq_No = Convert.ToInt32(ddlSequenceList.SelectedValue);
            
                 
               

            Routing.EmpInfoId = Convert.ToInt32(ShfEmpInfoId.Value.Trim());





                        if (Routing.Seq_No == 1)
                        {

                            ContractualEmpManageDAO aMaster = new ContractualEmpManageDAO();
                            aMaster.ContractualEmpManageId
                                = Convert.ToInt32(hfContractualEmpManageId.Value);
                            aMaster.ActionStatus = "Verified";
                            bool status = aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);
                           
                               

                                ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                                {
                                    appLogDao.ActionStatus = "Verified";
                                    appLogDao.ApproveDate = DateTime.Now;
                                    appLogDao.ApproveBy = Session["UserId"].ToString();
                                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                    appLogDao.ForEmpInfoId = Convert.ToInt32( Routing.EmpInfoId);
                                    appLogDao.ContractualEmpManageId = Convert.ToInt32(hfContractualEmpManageId.Value);
                                    appLogDao.Comments = "";
                                    appLogDao.CommentsId = 0;
                                    appLogDao.IsForward = true;
                               
                           
                                }

                                int id = aContractualEmpManageDAL.SaveEmpContractAppLogForward(appLogDao);

                                SenMailForApprved(Convert.ToInt32(Routing.EmpInfoId),
                                    "Contractual Employee Form Approval",
                                    @"  <br/> Dear Sir, <br/> 
Document is submitted for your Recommendation/Approval in the system.
To login, click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.  ");
                        



                        }
                        RoutingPath.Add(Routing);
           
        }
                  bool res=  aContractualEmpManageDAL.SaveRoutingPathDetails(RoutingPath, pk);
                    if (res)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Operation Successfully Done......');window.location ='ContractualEmpApprovalList.aspx';",
                          true);     
                    }
            }
        }
     
    }

    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
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
            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();




            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Session["EmailID"].ToString());
            try
            {
                mail.To.Add(ForMailAddress.Trim());
            }
            catch (Exception)
            {
                //throw;
            }
            mail.Subject = mSubject;
            mail.Body =
                "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                WebUtility.HtmlDecode(mBody)
                +
                "</div>";

            //Attach file using FileUpload Control and put the file in memory stream

            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(),
                Session["AppPass"].ToString());
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(mail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                // showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
                //  showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);
        }



    }
    public void Add()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("Seq_No");

        DataRow dataRow = null;
        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();
            HiddenField ShfEmpInfoId = ((HiddenField)gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId"));
            Label Slbl_EmpName = (Label)gv_Details_Save.Rows[i].FindControl("Slbl_EmpName");
            DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
            HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");
            dataRow["EmpName"] = Slbl_EmpName.Text;
            dataRow["EmpInfoId"] = ShfEmpInfoId.Value;
            dataRow["Seq_No"] = hfSeq_No.Value;
            ddlSequenceList.SelectedValue = hfSeq_No.Value;

            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["EmpName"] = ddlEmpInfo.SelectedItem.Text;
        dataRow["EmpInfoId"] = ddlEmpInfo.SelectedValue;
        

        aDataTable.Rows.Add(dataRow);
        gv_Details_Save.DataSource = aDataTable;
        gv_Details_Save.DataBind();

        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


            ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
            for (int k = 1; k < gv_Details_Save.Rows.Count + 1; k++)
            {
                ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
            }
        }

        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            DropDownList ddlSequenceList =
                (DropDownList) gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
            HiddenField hfSeq_No = (HiddenField) gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

            ddlSequenceList.SelectedValue = hfSeq_No.Value;


        }

        if (gv_Details_Save.Rows.Count == 0)
        {
            lblstatus.Text = "No Approval Path have been  selected.";
        }
        else
        {
            lblstatus.Text = "";
        }
        ddlEmpInfo.SelectedValue = null;
       
    }


    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("Seq_No");


        DataRow dataRow = null;
        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();

                HiddenField ShfEmpInfoId = ((HiddenField)gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId"));
                Label Slbl_EmpName = (Label)gv_Details_Save.Rows[i].FindControl("Slbl_EmpName");
                DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");
                dataRow["EmpName"] = Slbl_EmpName.Text;
                dataRow["EmpInfoId"] = ShfEmpInfoId.Value;
                dataRow["Seq_No"] = hfSeq_No.Value;
                aDataTable.Rows.Add(dataRow);
            }
        }
        gv_Details_Save.DataSource = aDataTable;
        gv_Details_Save.DataBind();


        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


            ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
            for (int k = 1; k < gv_Details_Save.Rows.Count + 1; k++)
            {
                ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
            }
        }

        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            DropDownList ddlSequenceList =
                (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
            HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

            ddlSequenceList.SelectedValue = hfSeq_No.Value;


        }

        if (gv_Details_Save.Rows.Count == 0)
        {
            lblstatus.Text = "No Approval Path have been  selected.";
        }
        else
        {
            lblstatus.Text = "";
        }

    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (ddlEmpInfo.SelectedValue != "")
        {
            Add();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Fill This Field", this);
            ddlEmpInfo.Focus();
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
    protected void btnForrr_OnClick(object sender, EventArgs e)
    {
        int count = 0;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            CheckBox chkSelect = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");


            if (chkSelect.Checked)
            {
                count = count + 1;
            }
        }


        if (count != 0)
        {

            MPBehavioral.Show();




        }
        else
        {

            aShowMessage.ShowMessageBox("Please Select At Least One Row!", this);
            MPBehavioral.Hide();
        }
    }


    protected void ddlSequenceList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;

        DropDownList ddlSequenceList = ((DropDownList)gv_Details_Save.Rows[rowIndex].FindControl("ddlSequenceList"));
        int count = 0;
        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            DropDownList ddlSequenceList2 = ((DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList"));
            if (ddlSequenceList.SelectedValue != "")
            {
                if (ddlSequenceList2.SelectedValue != "")
                {
                    if (ddlSequenceList.SelectedValue == ddlSequenceList2.SelectedValue)
                    {
                        count++;
                    }
                }


            }

            if (count > 1)
            {
                AlertMessageBoxShow("Approval Sequence Already Added");
                ddlSequenceList.SelectedValue = "";
            }
        }



    }
    protected void btn_DetailsRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        Remove(rowID);

        if (gv_Details_Save.Rows.Count == 0)
        {
            lblstatus.Text = "No Approval Path have been  selected.";
        }
        else
        {
            lblstatus.Text = "";
        }
        //Set Previous Data on Postbacks  
        //  SetDocGrid_List();
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
}
