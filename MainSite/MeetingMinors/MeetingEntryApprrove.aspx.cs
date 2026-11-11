using System;
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
using DAL.MasterSetup_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MeetingEntryApprrove : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    MeetingEntryDAL AMeetingEntryDal = new MeetingEntryDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
         Page.Form.Attributes.Add("enctype", "multipart/form-data");
         FUDocument.Attributes["onchange"] = "UploadFile(this)";
        if (!IsPostBack)
        {
       ButtonVisible();
            LoadInitialGrid();
            LoadInitialGridDetails_Save();
            LoadDropDownList();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                id_mastetID.Value = (Request.QueryString["MID"]);

                DataTable dtMaster = AMeetingEntryDal.GetMasterDataByIdApproval(id_mastetID.Value);
                if (dtMaster.Rows.Count > 0)
                {
                     hfSeqNo.Value = dtMaster.Rows[0]["RefSeqNo"].ToString();
                     hfRefMinAppCount.Value = dtMaster.Rows[0]["RefMinAppCount"].ToString();
                    ddlCompany.SelectedValue = dtMaster.Rows[0]["CompanyId"].ToString();
                    lblCompany.Text = ddlCompany.SelectedItem.Text;
                    ddlCompany_OnSelectedIndexChanged(null, null);
                    txtTitle.Text = dtMaster.Rows[0]["Title"].ToString();
                    lblTitle.Text = dtMaster.Rows[0]["Title"].ToString();
                    ddlCategory.SelectedValue = dtMaster.Rows[0]["CategoryID"].ToString();
                    lblMeetingCategory.Text = ddlCategory.SelectedItem.Text;
                    txtMeetingpurpose.Text = dtMaster.Rows[0]["MeetingPurpose"].ToString();
                    lblMeetingNote.Text = dtMaster.Rows[0]["MeetingPurpose"].ToString();
                    lblSubCommitteeName.Text = dtMaster.Rows[0]["SubCommitteeName"].ToString();
                    ddlClassification.SelectedValue = dtMaster.Rows[0]["Classification"].ToString();
                    lblClassification.Text = ddlClassification.SelectedItem.Text;
                    try
                    {
                        txtMeetingDate.Text = Convert.ToDateTime(dtMaster.Rows[0]["MeetingDate"]).ToString("dd-MMM-yyyy");

                        lblDate.Text = txtMeetingDate.Text;
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }

                    txtStartTime.Text = dtMaster.Rows[0]["StartTime"].ToString();
                    lblStartTime.Text = txtStartTime.Text;
                    txtEndTime.Text = dtMaster.Rows[0]["EndTime"].ToString();
                    lblEndTime.Text = txtEndTime.Text;


           bool     IsOfficePremisis=     Convert.ToBoolean(dtMaster.Rows[0]["IsOfficePremisis"].ToString());
                       bool     IsOuterPremisis=        Convert.ToBoolean(dtMaster.Rows[0]["IsOuterPremisis"].ToString());
                       bool     IsVirtualMeeting=        Convert.ToBoolean(dtMaster.Rows[0]["IsVirtualMeeting"].ToString());

                    if (IsOfficePremisis==true)
                    {
                        rbLocation.Items[0].Selected = true;

                      



                    }
                    if (IsOuterPremisis == true)
                    {
                        rbLocation.Items[1].Selected = true;
                    }
                    if (IsVirtualMeeting == true)
                    {
                        rbLocation.Items[2].Selected = true;
                    }

                    rbLocation_OnSelectedIndexChanged(null, null);



                    if (IsOfficePremisis == true)
                    {

                        divOfficePremisis.Visible = true;
                        ddlOffice.SelectedValue = dtMaster.Rows[0]["OfficeId"].ToString();
                        ddlOffice_OnSelectedIndexChanged(null, null);
                        ddlLocation.SelectedValue = dtMaster.Rows[0]["LocationId"].ToString();
                        ddlLocation_OnSelectedIndexChanged(null, null);
                        ddlFloor.SelectedValue = dtMaster.Rows[0]["FloorId"].ToString();
                        ddlFloor_OnSelectedIndexChanged(null, null);

                        ddlMettingRoomName.SelectedValue = dtMaster.Rows[0]["MettingRoomId"].ToString();
                        ddlMettingRoomName_OnSelectedIndexChanged(null, null);
                        if (ddlOffice.SelectedIndex > 0)
                        {
                            lblOffice.Text = ddlOffice.SelectedItem.Text;
                             
                        }
                        if (ddlLocation.SelectedIndex > 0)
                        {
                            lblLocation2.Text = ddlLocation.SelectedItem.Text;

                        }

                        if (ddlFloor.SelectedIndex > 0)
                        {
                            lblFloor.Text = ddlFloor.SelectedItem.Text;

                        }

                        if (ddlMettingRoomName.SelectedIndex > 0)
                        {
                            lblMeetingRoom.Text = ddlMettingRoomName.SelectedItem.Text;

                        }
                        lblCapacity.Text = lblCapacity.Text;

                    }
                    if (IsOuterPremisis == true)
                    {
                        divOuterPremisis.Visible = true;
                        rbLocation.Items[1].Selected = true;

                        txtLocation.Text = dtMaster.Rows[0]["Location"].ToString();
                        txtDescription.Text = dtMaster.Rows[0]["LocationDescription"].ToString();

                        lblOuterLocation.Text = txtLocation.Text;
                        lblOuterDescription.Text = txtDescription.Text;

                    }
                    if (IsVirtualMeeting == true)
                    {
                        divVirtualMeeting.Visible = true;
                        rbLocation.Items[2].Selected = true;
                        txtRemarks.Text = dtMaster.Rows[0]["Remarks"].ToString();
                        lblRemarks.Text = txtRemarks.Text;
                    }




                }


                DataTable dtempdata =
             AMeetingEntryDal.GetEmpRoutingPath(id_mastetID.Value.ToString(), hfSeqNo.Value);
                if (dtempdata.Rows.Count > 0)
                {
                    lblNextApp.Text = dtempdata.Rows[0]["AwEmpName"].ToString();

                }
                else
                {
                    lblNextApp.Text = "You are final Approval Person";
                }



                DataTable dtDoc = AMeetingEntryDal.GetDocDataById(id_mastetID.Value);
                if (dtDoc.Rows.Count > 0)
                {
                    ViewState["DocGrid_List"] = dtDoc;
                    gv_DocumentUpload.DataSource = dtDoc;
                    gv_DocumentUpload.DataBind();
                }


                DataTable dtDocEdit =
               AMeetingEntryDal.GetEmpCanEditDOC(id_mastetID.Value.ToString());

                if (dtDocEdit.Rows.Count > 0)
                {

                    bool CanEdit = Convert.ToBoolean(dtDocEdit.Rows[0]["CanEdit"].ToString());
                    if (CanEdit == true)
                    {
                        divDoc.Visible = true;

                        gv_DocumentUpload.Columns[3].Visible = true;
                    }
                }


                DataTable MeetingInfoDetail = AMeetingEntryDal.GetMeetingInfoDetailById(id_mastetID.Value);
                if (MeetingInfoDetail.Rows.Count > 0)
                {
                    ViewState["gv_Details_List"] = MeetingInfoDetail;
                    gv_Details_Save.DataSource = MeetingInfoDetail;
                    gv_Details_Save.DataBind();


                    for (int i = 0; i < MeetingInfoDetail.Rows.Count; i++)
                    {
                        RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
                        rbType.SelectedValue = MeetingInfoDetail.Rows[i]["Type"].ToString();

                        CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));

                        bool NotificationEmail = Convert.ToBoolean(MeetingInfoDetail.Rows[i]["NotificationEmail"].ToString());
                        bool NotificationSMS = Convert.ToBoolean(MeetingInfoDetail.Rows[i]["NotificationSMS"].ToString());

                        if (NotificationEmail==true)
                        {
                            chkNotification.Items[0].Selected = true;
                        }

                        if (NotificationSMS == true)
                        {
                            chkNotification.Items[1].Selected = true;
                        }


 


                        RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("chkPosition"));



                        chkPosition.SelectedValue = MeetingInfoDetail.Rows[i]["Position"].ToString();

                    }
                }

                DataTable ApprovalPath = AMeetingEntryDal.GetMeetingApprovalPathById(id_mastetID.Value);
                if (ApprovalPath.Rows.Count > 0)
                {
                    ViewState["gv_Details_App"] = ApprovalPath;
                    gv_ApprovalPathDetail.DataSource = ApprovalPath;
                    gv_ApprovalPathDetail.DataBind();



                    for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                    {
                        DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");


                        ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One..", String.Empty));
                        for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                        {
                            ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                        }
                    }


                    for (int i = 0; i < ApprovalPath.Rows.Count; i++)
                    {
                        DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                        HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                        ddlSequenceList.SelectedValue = hfSeq_No.Value;


                        HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                        HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                        HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                        HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));


                        CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkMimimumCount");

                        if (hfIsMinimumApprovalPerson.Value != "")
                        {
                            chkMimimumCount.Checked = Convert.ToBoolean(hfIsMinimumApprovalPerson.Value);

                        }


                        CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkIsEdit");

                        if (hfCanEdit.Value != "")
                        {
                            chkIsEdit.Checked = Convert.ToBoolean(hfCanEdit.Value);

                        }
                        CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkNotificationApp");


                        if (HiNotificationEmailApp.Value != "")
                        {
                            chkNotificationApp.Items[0].Selected = Convert.ToBoolean(HiNotificationEmailApp.Value);

                        }

                        if (hfNotificationSMSApp.Value != "")
                        {
                            chkNotificationApp.Items[1].Selected = Convert.ToBoolean(hfNotificationSMSApp.Value);

                        }


                    }


                }
                DataTable dtAgendaa = AMeetingEntryDal.GetAgendDataById(id_mastetID.Value);
                if (dtAgendaa.Rows.Count > 0)
                {
                   
                    gv_AgendaList.DataSource = dtAgendaa;
                    gv_AgendaList.DataBind();
                }



                DataTable dtApplogList = AMeetingEntryDal.GetAppLogCommByJobId(id_mastetID.Value);
                if (dtApplogList.Rows.Count > 0)
                {

                    gv_ApprovalList.DataSource = dtApplogList;
                    gv_ApprovalList.DataBind();


                }


            }
        }
    }
    private void LoadInitialGrid()
    {
        DataTable dtDetails = new DataTable();

        dtDetails.Columns.Add("Agenda");
        dtDetails.Columns.Add("Remarks");

        dtDetails.Columns.Add("Presentor");


        DataRow dr = null;
        dr = dtDetails.NewRow();

        dr["Agenda"] = "";

        dr["Presentor"] = "";
        dr["Remarks"] = "";

      

        dtDetails.Rows.Add(dr);

        gv_AgendaList.DataSource = null;
        gv_AgendaList.DataBind();
        gv_AgendaList.DataSource = dtDetails;
        gv_AgendaList.DataBind();

    }


    private void LoadInitialGridDetails_Save()
    {
        DataTable dtDetails = new DataTable();

        dtDetails.Columns.Add("Type");

        dtDetails.Columns.Add("EmpMasterCode");
        dtDetails.Columns.Add("EmpInfoId");
        dtDetails.Columns.Add("EmpName");
        dtDetails.Columns.Add("Designation");
        dtDetails.Columns.Add("NotificationEmail");
        dtDetails.Columns.Add("NotificationSMS");
        dtDetails.Columns.Add("Position");
        


        DataRow dr = null;
        dr = dtDetails.NewRow();

        dr["Type"] = "";

        dr["EmpMasterCode"] = "";
        dr["EmpInfoId"] = "";
        dr["EmpName"] = "";
        dr["Designation"] = "";

        dr["NotificationEmail"] = "";
        dr["NotificationSMS"] = "";
        dr["Position"] = "";
        



        dtDetails.Rows.Add(dr);

        gv_Details_Save.DataSource = null;
        gv_Details_Save.DataBind();
        gv_Details_Save.DataSource = dtDetails;
        gv_Details_Save.DataBind();

    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {

            Response.Redirect("MeetingApprovalView.aspx");
        }


    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        AMAsterDal.GetCategoryListIntoDropdown(ddlCategory);
        ddlCompany.SelectedIndex = 1;

        using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
        {
            ddlOffice.DataSource = dt;
            ddlOffice.DataValueField = "Value";
            ddlOffice.DataTextField = "TextField";
            ddlOffice.DataBind();
        }

        ddlCompany_OnSelectedIndexChanged(null, null);

    }
    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);

            AMAsterDal.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);

        }
        else
        {

            ddlDepartment.Items.Clear();
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
    private bool docVali()
    {
        if (hfDocFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please Upload a Document", this);

            return false;
        }
        if (txtSummaryNote.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);

            return false;
        }
        return true;

    }

    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
            AddNewDocGrid_List();
        }
       
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
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");
                    HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfFileName");


                    if (i < dt.Rows.Count - 1)
                    {
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();

                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        if (FUDocument.HasFile)
        {
            string _fileExt = System.IO.Path.GetExtension(FUDocument.FileName);
            string AdsFile = "Meeting_Mis_DOC_" + Guid.NewGuid().ToString() + Path.GetExtension(FUDocument.FileName);
            //  fileName = guid.ToString() + imageFileUpload.FileName;
            FUDocument.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
            HyperLink2.NavigateUrl = "../UploadImg/" + AdsFile;
            HyperLink2.Text = "Uploaded Successfully";
        }
        else
        {
            HyperLink2.NavigateUrl = "";
        //    HyperLink2.Text = "No File Uploaded";
        }
    }
    protected void btn_DetailsRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gv_Details_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["gv_Details_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gv_Details_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_Details_Save.DataSource = dt;
                gv_Details_Save.DataBind();


            }
            else
            {
                ViewState["gv_Details_List"] = null;
                //Re bind the GridView for the updated data  
                gv_Details_Save.DataSource = null;
                gv_Details_Save.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        //  SetDocGrid_List();


        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
            rbType.SelectedValue = ((HiddenField)gv_Details_Save.Rows[i].Cells[1].FindControl("hfType"))
                    .Value;



            CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));

            HiddenField HiNotificationEmail = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
            HiddenField hfNotificationSMS = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));

            if (HiNotificationEmail.Value != "")
            {
                try
                {
                    chkNotification.Items[0].Selected = Convert.ToBoolean(HiNotificationEmail.Value);
                }
                catch (Exception)
                {

                    //throw;
                }

            }

            if (hfNotificationSMS.Value != "")
            {
                try
                {
                    chkNotification.Items[1].Selected = Convert.ToBoolean(hfNotificationSMS.Value);
                }
                catch (Exception)
                {

                    //throw;
                }

            }


            RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("chkPosition"));

            HiddenField hfPosition = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfPosition"));

            chkPosition.SelectedValue = hfPosition.Value;

        }
    }

    private bool AddMemberValidation()
    {
        if (gv_EmpListSearch.Rows.Count == 0)
        {

            AlertMessageBoxShow("Please select at least one employee !!!");
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);

            return false;
        }

        int totalCount = gv_EmpListSearch.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkSelect")).Checked);

        if (totalCount == 0)
        {
            aShowMessage.ShowMessageBox("Please Select Employee", this);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);



            return false;
        }






        return true;
    }


    private bool AddMemberValidationApp()
    {
        if (gv_EmpListSearchAPP.Rows.Count == 0)
        {

            AlertMessageBoxShow("Please select at least one employee !!!");
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);

            return false;
        }

        int totalCount = gv_EmpListSearchAPP.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkSelectAPP")).Checked);

        if (totalCount == 0)
        {
            aShowMessage.ShowMessageBox("Please Select Employee", this);
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);



            return false;
        }






        return true;
    }

    public bool CheckEmpListApp()
    {
        for (int i = 0; i < gv_EmpListSearchAPP.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearchAPP.Rows[i].Cells[0].FindControl("chkSelectAPP");
            for (int j = 0; j < gv_ApprovalPathDetail.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                    Label SSStxt_empId = (Label)gv_ApprovalPathDetail.Rows[j].FindControl("Appbl_EmpMasterCode");

                    Label EmpDI = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_EmpMasterCodeAPP");

                    if (EmpDI.Text == SSStxt_empId.Text)
                    {


                        return false;

                    }

                }

            }

        }
        return true;
    }
    public bool CheckEmpList()
    {
        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
            for (int j = 0; j < gv_Details_Save.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                    TextBox SSStxt_empId = (TextBox)gv_Details_Save.Rows[j].FindControl("txt_EmpMasterCode");

                    Label EmpDI = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpMasterCode");

                    if (EmpDI.Text == SSStxt_empId.Text)
                    {


                        return false;

                    }

                }

            }

        }
        return true;
    }
    protected void btnAddToListEmp_OnClick(object sender, EventArgs e)
    {
        if (AddMemberValidation())
        {
            if (CheckEmpList())
            {

                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpMasterCode");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("Designation");


                aDataTable.Columns.Add("Type");
                aDataTable.Columns.Add("NotificationEmail");
                aDataTable.Columns.Add("NotificationSMS");
                aDataTable.Columns.Add("Position");




                DataRow dataRow = null;

        int count = 0;

                for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
                    HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearch.Rows[i].FindControl("hfEmpInfoId"));
                    Label lbl_EmpMasterCode = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpMasterCode");
                    Label lbl_EmpName = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpName");
                    Label lbl_Designation = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Designation");

                    if (chkSelect.Checked)
                    {
                        count++;
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {



                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] = hfEmpInfoId.Value;

                            dataRow["EmpMasterCode"] = lbl_EmpMasterCode.Text;
                            dataRow["EmpName"] = lbl_EmpName.Text;
                            dataRow["Designation"] = lbl_Designation.Text;

                            dataRow["Type"] = "";

                            dataRow["NotificationEmail"] = "";
                            dataRow["NotificationSMS"] = "";
                          

                            dataRow["Position"] = "";
                          


                            aDataTable.Rows.Add(dataRow);
                        }
                    }
                }
                for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                {


                    HiddenField hfType = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfType"));
                    TextBox txt_EmpMasterCode = ((TextBox)gv_Details_Save.Rows[i].FindControl("txt_EmpMasterCode"));
                    HiddenField ShfEmpInfoId = ((HiddenField)gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId"));
                    TextBox txt_EmpName = (TextBox)gv_Details_Save.Rows[i].FindControl("txt_EmpName");
                    TextBox txt_Designation = (TextBox)gv_Details_Save.Rows[i].FindControl("txt_Designation");

                    HiddenField HiNotificationEmail = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                    HiddenField hfNotificationSMS = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));
                    HiddenField hfPosition = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfPosition"));



                    RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
                    CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));
                    RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("chkPosition"));
                   


             



                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = ShfEmpInfoId.Value;

                    dataRow["EmpMasterCode"] = txt_EmpMasterCode.Text;
                    dataRow["EmpName"] = txt_EmpName.Text;
                    dataRow["Designation"] = txt_Designation.Text;
                    
                    dataRow["Type"] = hfType.Value.Trim();
                    if (hfType.Value.Trim()!="")
                    {
                        try
                        {
                            rbType.SelectedValue = hfType.Value.Trim();
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }
                         
                    }
                    dataRow["NotificationEmail"] = HiNotificationEmail.Value.Trim();
                    if (HiNotificationEmail.Value.Trim() != "")
                    {
                        try
                        {
                            chkNotification.Items[0].Selected = Convert.ToBoolean(HiNotificationEmail.Value.Trim());
                        }
                        catch (Exception)
                        {
                            chkNotification.Items[0].Selected = false;
                            // throw;
                        }

                    }

                    dataRow["NotificationSMS"] = hfNotificationSMS.Value.Trim();
                    if (hfNotificationSMS.Value.Trim() != "")
                    {
                        try
                        {
                            chkNotification.Items[1].Selected = Convert.ToBoolean(hfNotificationSMS.Value.Trim());
                        }
                        catch (Exception)
                        {

                            chkNotification.Items[1].Selected = false;
                        }

                    }

                    dataRow["Position"] = hfPosition.Value.Trim();
                    if (hfPosition.Value.Trim()!="")
                    {
                        try
                        {
                            chkPosition.SelectedValue = hfPosition.Value.Trim();
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }
                    } 
                   







                    aDataTable.Rows.Add(dataRow);
                }
                ViewState["gv_Details_List"] = aDataTable;
                gv_Details_Save.DataSource = aDataTable;
                gv_Details_Save.DataBind();



                 


                for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                {
                    RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
                    rbType.SelectedValue = ((HiddenField)gv_Details_Save.Rows[i].Cells[1].FindControl("hfType"))
                            .Value;



                    CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));

                    HiddenField HiNotificationEmail = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                    HiddenField hfNotificationSMS = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));

                    if (HiNotificationEmail.Value != "")
                    {
                        try
                        {
                            chkNotification.Items[0].Selected = Convert.ToBoolean(HiNotificationEmail.Value);
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }

                    if (hfNotificationSMS.Value != "")
                    {
                        try
                        {
                            chkNotification.Items[1].Selected = Convert.ToBoolean(hfNotificationSMS.Value);
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                    }


                    RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("chkPosition"));

                    HiddenField hfPosition = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfPosition"));

                    chkPosition.SelectedValue = hfPosition.Value;

                }


                for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
                {
                    var chkBoxRows = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
                    for (int j = 0; j < gv_Details_Save.Rows.Count; j++)
                    {
                        if (chkBoxRows.Checked)
                        {
                            TextBox SSStxt_empId = (TextBox)gv_Details_Save.Rows[j].FindControl("txt_EmpMasterCode");

                            Label EmpDI = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpMasterCode");

                            if (EmpDI.Text == SSStxt_empId.Text)
                            {

                                RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
                                rbType.SelectedValue = "Employee";((HiddenField)gv_Details_Save.Rows[i].Cells[1].FindControl("hfType"))
                                        .Value = "Employee";
                               

                            }

                        }

                    }

                }
            }
            else
            {
                AlertMessageBoxShow("Already Exist !!!");
            }
        }

    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_EmpListSearch.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);

    }

    ShowMessage aShowMessage = new ShowMessage();

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadEMPInfo();
        }
        else
        {
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
        }
    }

    private string GenerateParameter()
    {
        string parameter = " ";

        if (ddlCompany.SelectedIndex > 0)
        {
            parameter = parameter + "  and    com.CompanyId = '" + ddlCompany.SelectedValue + "'";
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            parameter = parameter + "  and    div.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  and    Dpt.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }
        return parameter;
    }

    private string GenerateParameterApp()
    {
        string parameter = " ";

        if (ddlCompany.SelectedIndex > 0)
        {
            parameter = parameter + "  and    com.CompanyId = '" + ddlCompany.SelectedValue + "'";
        }

        if (ddlDivisionAPP.SelectedIndex > 0)
        {
            parameter = parameter + "  and    div.DivisionId = '" + ddlDivisionAPP.SelectedValue + "'";
        }

        if (ddlDepartmentAPP.SelectedIndex > 0)
        {
            parameter = parameter + "  and    Dpt.DepartmentId = '" + ddlDepartmentAPP.SelectedValue + "'";
        }
        return parameter;
    }

    private void LoadEMPInfo()
    {
        DataTable jobCreationInfos = new DataTable();
        jobCreationInfos = AMAsterDal.GetEMpInfos(GenerateParameter());
        if (jobCreationInfos.Rows.Count > 0)
        {

            gv_EmpListSearch.DataSource = jobCreationInfos;
            gv_EmpListSearch.DataBind();



        }
        else
        {
            gv_EmpListSearch.DataSource = null;
            gv_EmpListSearch.DataBind();
            AlertMessageBoxShow("No Data Found!!!");

        }
    }


    private void LoadEMPInfoAappEmp()
    {
        DataTable jobCreationInfos = new DataTable();
        jobCreationInfos = AMAsterDal.GetEMpInfos(GenerateParameterApp());
        if (jobCreationInfos.Rows.Count > 0)
        {

            gv_EmpListSearchAPP.DataSource = jobCreationInfos;
            gv_EmpListSearchAPP.DataBind();



        }
        else
        {
            gv_EmpListSearchAPP.DataSource = null;
            gv_EmpListSearchAPP.DataBind();
            AlertMessageBoxShow("No Data Found!!!");

        }
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            using (DataTable dt = AMAsterDal.GetDDLComDivision(ddlCompany.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();

                ddlDivisionAPP.DataSource = dt;
                ddlDivisionAPP.DataValueField = "Value";
                ddlDivisionAPP.DataTextField = "TextField";
                ddlDivisionAPP.DataBind();
            }
            using (DataTable dt2 = AMAsterDal.GetDDLRoutingPath(ddlCompany.SelectedValue))
            {
                rbRoutingPath.DataSource = dt2;
                rbRoutingPath.DataValueField = "Value";
                rbRoutingPath.DataTextField = "TextField";
                rbRoutingPath.DataBind();
            }

        //    using (DataTable dt2 = AMeetingEntryDal.GetEmpInfobyID(ddlCompany.SelectedValue))
        //    {

        //        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        //        {
        //DropDownList ddlPresentor= ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));

        //ddlPresentor.DataSource = dt2;
        //ddlPresentor.DataValueField = "EmpInfoId";
        //ddlPresentor.DataTextField = "EmpName";
        //ddlPresentor.DataBind();
        //ddlPresentor.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
        //ddlPresentor.SelectedIndex = 0;
        //        }
        //    }
        }
        else
        {
            //for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
            //{
            //    DropDownList ddlPresentor = ((DropDownList) gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));
            //    ddlPresentor.Items.Clear();
            //}
            ddlDivision.Items.Clear();
            ddlDepartment.Items.Clear();
             rbRoutingPath.Items.Clear();
        }
    }

    protected void btnAgenaListAdd_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();
        aTable.Columns.Add("Agenda");
        aTable.Columns.Add("Presentor");
        aTable.Columns.Add("Remarks");


        DataRow dr;



        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            dr["Agenda"] =
                   ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtAgenda")).Text.Trim();


            dr["Remarks"] =
                ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtRemarks")).Text.Trim();

            dr["Presentor"] =
                ((HiddenField)gv_AgendaList.Rows[i].Cells[2].FindControl("hfPresentor")).Value.Trim();


          
            dr["Presentor"] =
             ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor")).SelectedValue.Trim();


            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();
                dr["Agenda"] = "";
                dr["Presentor"] = "";
                dr["Remarks"] = "";

                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        gv_AgendaList.DataSource = null;
        gv_AgendaList.DataBind();
        gv_AgendaList.DataSource = aTable;
        gv_AgendaList.DataBind();

        //using (DataTable dt2 = AMeetingEntryDal.GetEmpInfobyID(ddlCompany.SelectedValue))
        //{

        //    for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        //    {
        //        DropDownList ddlPresentor = ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));

        //        ddlPresentor.DataSource = dt2;
        //        ddlPresentor.DataValueField = "EmpInfoId";
        //        ddlPresentor.DataTextField = "EmpName";
        //        ddlPresentor.DataBind();
        //        ddlPresentor.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
        //        ddlPresentor.SelectedIndex = 0;



        //        ddlPresentor.SelectedValue = ((HiddenField)gv_AgendaList.Rows[i].Cells[1].FindControl("hfPresentor"))
        //                 .Value;
        //    }
        //}

    }

    protected void btnAgenaLisRemove_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();
        aTable.Columns.Add("Agenda");
        aTable.Columns.Add("Presentor");
        aTable.Columns.Add("Remarks");
        DataRow dr;

        for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        {
            if (rowIndex != i)
            {
                dr = aTable.NewRow();
                dr["Remarks"] =
               ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtRemarks")).Text.Trim();
                dr["Agenda"] =
                  ((TextBox)gv_AgendaList.Rows[i].Cells[2].FindControl("txtAgenda")).Text.Trim();

                dr["Presentor"] =
                    ((HiddenField)gv_AgendaList.Rows[i].Cells[2].FindControl("hfPresentor")).Value.Trim();
                dr["Presentor"] =
           ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor")).SelectedValue.Trim();

                aTable.Rows.Add(dr);
            }
        }

        if (aTable.Rows.Count > 0)
        {
            gv_AgendaList.DataSource = null;
            gv_AgendaList.DataBind();
            gv_AgendaList.DataSource = aTable;
            gv_AgendaList.DataBind();



        }


        //using (DataTable dt2 = AMeetingEntryDal.GetEmpInfobyID(ddlCompany.SelectedValue))
        //{

        //    for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
        //    {
        //        DropDownList ddlPresentor = ((DropDownList)gv_AgendaList.Rows[i].Cells[2].FindControl("ddlPresentor"));

        //        ddlPresentor.DataSource = dt2;
        //        ddlPresentor.DataValueField = "EmpInfoId";
        //        ddlPresentor.DataTextField = "EmpName";
        //        ddlPresentor.DataBind();
        //        ddlPresentor.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
        //        ddlPresentor.SelectedIndex = 0;



        //        ddlPresentor.SelectedValue = ((HiddenField)gv_AgendaList.Rows[i].Cells[1].FindControl("hfPresentor"))
        //                 .Value;
        //    }
        //}
    }

    protected void btnOkayRoutingPath_OnClick(object sender, EventArgs e)
    {
        string grade = "";
        for (int i = 0; i < rbRoutingPath.Items.Count; i++)
        {
            if (rbRoutingPath.Items[i].Selected)
            {
                grade = rbRoutingPath.Items[i].Value;
            }
        }

        if (grade == "")
        {
            AlertMessageBoxShow("Select Routing Path!!!");
        }
        else
        {

            DataTable jobCreationInfos = new DataTable();
            jobCreationInfos = AMAsterDal.GetEMpInfoFromRoutingPath(grade);
            if (jobCreationInfos.Rows.Count > 0)
            {
                ViewState["gv_Details_App"] = jobCreationInfos;
                gv_ApprovalPathDetail.DataSource = jobCreationInfos;
                gv_ApprovalPathDetail.DataBind();


            



                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");


                    ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One..", String.Empty));
                    for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                    {
                        ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                    }
                }


                for (int i = 0; i < jobCreationInfos.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;

                }


            }
            else
            {
                gv_ApprovalPathDetail.DataSource = null;
                gv_ApprovalPathDetail.DataBind();


            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#exampleModal').modal('hide')", true);

        }




    }

    protected void Appbtn_DetailsRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gv_Details_App"] != null)
        {
            DataTable dt = (DataTable)ViewState["gv_Details_App"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gv_Details_App"] = dt;
                //Re bind the GridView for the updated data  
                gv_ApprovalPathDetail.DataSource = dt;
                gv_ApprovalPathDetail.DataBind();
            }
            else
            {
                ViewState["gv_Details_App"] = null;
                //Re bind the GridView for the updated data  
                gv_ApprovalPathDetail.DataSource = null;
                gv_ApprovalPathDetail.DataBind();
            }


            for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
            {
                DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


                ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                {
                    ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                }
            }

            for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
            {
                DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                ddlSequenceList.SelectedValue = hfSeq_No.Value;




                HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));


                CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkMimimumCount");

                if (hfIsMinimumApprovalPerson.Value != "")
                {
                    chkMimimumCount.Checked = true;

                }


                CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkIsEdit");

                if (hfCanEdit.Value != "")
                {
                    chkIsEdit.Checked = true;

                }
                CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkNotificationApp");


                if (HiNotificationEmailApp.Value != "")
                {
                    chkNotificationApp.Items[0].Selected = true;

                }

                if (hfNotificationSMSApp.Value != "")
                {
                    chkNotificationApp.Items[1].Selected = true;

                }

            }
        }
    }


    protected void ddlDivisionAPP_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivisionAPP.SelectedValue != "")
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);

            AMAsterDal.GetDepartmentByDivList(ddlDepartmentAPP, ddlDivisionAPP.SelectedValue);

        }
        else
        {

            ddlDepartmentAPP.Items.Clear();
        }
    }

    protected void btnSearchAPP_OnClick(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadEMPInfoAappEmp();
        }
        else
        {
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
        }
    }

    protected void chkSelectAllAPP_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_EmpListSearchAPP.HeaderRow.FindControl("chkSelectAllAPP");

        for (int i = 0; i < gv_EmpListSearchAPP.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearchAPP.Rows[i].Cells[0].FindControl("chkSelectAPP");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModalAPPEmpp').modal('show')", true);
    }

    protected void btnAddToListEmpAPP_OnClick(object sender, EventArgs e)
    {
        if (AddMemberValidationApp())
        {
            if (CheckEmpListApp())
            {

                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpMasterCode");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("Designation");
                aDataTable.Columns.Add("Seq_No");
                aDataTable.Columns.Add("IsMinimumApprovalPerson");
                aDataTable.Columns.Add("CanEdit");
                aDataTable.Columns.Add("NotificationEmail");
                aDataTable.Columns.Add("NotificationSMS");
               



                DataRow dataRow = null;

                for (int i = 0; i < gv_EmpListSearchAPP.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)gv_EmpListSearchAPP.Rows[i].Cells[0].FindControl("chkSelectAPP");
                    HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearchAPP.Rows[i].FindControl("hfEmpInfoIdAPP"));
                    Label lbl_EmpMasterCode = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_EmpMasterCodeAPP");
                    Label lbl_EmpName = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_EmpNameAPP");
                    Label lbl_Designation = (Label)gv_EmpListSearchAPP.Rows[i].FindControl("lbl_DesignationAPP");

                    if (chkSelect.Checked)
                    {
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {



                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] = hfEmpInfoId.Value;

                            dataRow["EmpMasterCode"] = lbl_EmpMasterCode.Text;
                            dataRow["EmpName"] = lbl_EmpName.Text;
                            dataRow["Designation"] = lbl_Designation.Text;

                            dataRow["Seq_No"] = "";
                            dataRow["IsMinimumApprovalPerson"] = "";
                            dataRow["CanEdit"] = "";
                            dataRow["NotificationEmail"] = "";
                            dataRow["NotificationSMS"] = "";
                          

                            aDataTable.Rows.Add(dataRow);
                        }
                    }
                }
                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {


                    HiddenField ShfEmpInfoId = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("ApphfEmpInfoId"));
                    HiddenField hfSeq_No = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfSeq_No"));
                    Label Slbl_EmpMasterCode = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Appbl_EmpMasterCode");
                    Label Slbl_EmpName = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Applbl_EmpName");
                    Label Slbl_Designation = (Label)gv_ApprovalPathDetail.Rows[i].FindControl("Applbl_Designation");



                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));



                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = ShfEmpInfoId.Value;

                    dataRow["EmpMasterCode"] = Slbl_EmpMasterCode.Text;
                    dataRow["EmpName"] = Slbl_EmpName.Text;
                    dataRow["Designation"] = Slbl_Designation.Text;
                    dataRow["Seq_No"] = hfSeq_No.Value;


                    dataRow["IsMinimumApprovalPerson"] = hfIsMinimumApprovalPerson.Value;
                    dataRow["CanEdit"] = hfCanEdit.Value;
                    dataRow["NotificationEmail"] = HiNotificationEmailApp.Value;
                    dataRow["NotificationSMS"] = hfNotificationSMSApp.Value;
                          






                    aDataTable.Rows.Add(dataRow);
                }
                ViewState["gv_Details_App"] = aDataTable;
                gv_ApprovalPathDetail.DataSource = aDataTable;
                gv_ApprovalPathDetail.DataBind();



                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


                    ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                    for (int k = 1; k < gv_ApprovalPathDetail.Rows.Count + 1; k++)
                    {
                        ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                    }
                }

                for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;




                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("HiNotificationEmailApp"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("hfNotificationSMSApp"));


                    CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkMimimumCount");

                    if (hfIsMinimumApprovalPerson.Value!="")
                    {
                        chkMimimumCount.Checked =true;
                         
                    }


                    CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkIsEdit");

                    if (hfCanEdit.Value != "")
                    {
                        chkIsEdit.Checked = true;

                    }
                    CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].Cells[0].FindControl("chkNotificationApp");


                    if (HiNotificationEmailApp.Value != "")
                    {
                        chkNotificationApp.Items[0].Selected = true;

                    }

                    if (hfNotificationSMSApp.Value != "")
                    {
                        chkNotificationApp.Items[1].Selected = true;

                    }

                }
            }
            else
            {
                AlertMessageBoxShow("Already Exist !!!");
            }
        }
        
    }

    protected void rbLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbLocation.Items[0].Selected == true)
        {
            ddlOffice.Enabled = true;
            ddlLocation.Enabled = true;
            ddlFloor.Enabled = true;
            ddlMettingRoomName.Enabled = true;
            txtCapacity.ReadOnly = false;



            txtLocation.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtRemarks.ReadOnly = true;

            txtLocation.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtRemarks.Text = string.Empty;


        }

        if (rbLocation.Items[1].Selected == true)
        {
            ddlOffice.Enabled = false;
            ddlLocation.Enabled = false;
            ddlFloor.Enabled = false;
            ddlMettingRoomName.Enabled = false;
            txtCapacity.ReadOnly = true;

            txtLocation.ReadOnly = false;
            txtDescription.ReadOnly = false;
            txtRemarks.ReadOnly = true;



            ddlOffice.SelectedValue = null;
            ddlLocation.Items.Clear();
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = string.Empty;
            txtRemarks.Text = string.Empty;

           



        }

        if (rbLocation.Items[2].Selected == true)
        {
            ddlOffice.Enabled = false;
            ddlLocation.Enabled = false;
            ddlFloor.Enabled = false;
            ddlMettingRoomName.Enabled = false;
            txtCapacity.ReadOnly = true;

            txtLocation.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtRemarks.ReadOnly = false;

            ddlOffice.SelectedValue = null;
            ddlLocation.Items.Clear();
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo();
    }

    public bool Validation()
    {

       
        if (ddlCompany.SelectedValue=="")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            ddlCompany.Focus();
            return false;
        }



        if (txtTitle.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            txtTitle.Focus();
            return false;
        }

        if (ddlCategory.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            ddlCategory.Focus();
            return false;
        }

        if (txtMeetingpurpose.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            txtMeetingpurpose.Focus();
            return false;
        }

        if (txtMeetingDate.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            txtMeetingDate.Focus();
            return false;
        }
        return true;
    }

    private void SaveUpdateInfo()
    {
        if (Validation() == true)
        {

            string KeySearch = "";
            string MasterSearch = "";
            string DocumentSearch = "";
            string AgendaSearch = "";
            string MemberSearch = "";
            List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();

            for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
            {
                HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfFileName");


                MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();
                DocA.FileName = hfFileName.Value.ToString();
                DocA.DocumentLink = hfDocumentLink.Value.ToString();
                DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                DocumentSearch = DocumentSearch + lbl_DocumentNote.Text.Trim() + " ";

                DocList.Add(DocA);
            }


            List<MeetingInfoAgendaDAO> AgendaList = new List<MeetingInfoAgendaDAO>();

            for (int i = 0; i < gv_AgendaList.Rows.Count; i++)
            {
                TextBox txtAgenda = (TextBox)gv_AgendaList.Rows[i].FindControl("txtAgenda");
                TextBox txtRemarks = (TextBox)gv_AgendaList.Rows[i].FindControl("txtRemarks");
                DropDownList ddlPresentor = (DropDownList)gv_AgendaList.Rows[i].FindControl("ddlPresentor");


                MeetingInfoAgendaDAO AgendaA = new MeetingInfoAgendaDAO();

                AgendaA.Agenda = txtAgenda.Text.Trim().ToString();
                AgendaA.Remarks = txtRemarks.Text.Trim().ToString();
                AgendaSearch = AgendaSearch + txtAgenda.Text.Trim().ToString() + " ";

                AgendaA.PresentorId =    Convert.ToInt32(ddlPresentor.SelectedIndex > 0 ? ddlPresentor.SelectedValue : null);
                AgendaList.Add(AgendaA);
            }



            List<MiscellaneousInfoRoutingPathDAO> RoutingPath = new List<MiscellaneousInfoRoutingPathDAO>();

            for (int i = 0; i < gv_ApprovalPathDetail.Rows.Count; i++)
            {
                CheckBox chkMimimumCount = (CheckBox)gv_ApprovalPathDetail.Rows[i].FindControl("chkMimimumCount");
                DropDownList ddlSequenceList = (DropDownList)gv_ApprovalPathDetail.Rows[i].FindControl("ddlSequenceList");


                HiddenField ApphfEmpInfoId = (HiddenField)gv_ApprovalPathDetail.Rows[i].FindControl("ApphfEmpInfoId");


                CheckBox chkIsEdit = (CheckBox)gv_ApprovalPathDetail.Rows[i].FindControl("chkIsEdit");
                CheckBoxList chkNotificationApp = (CheckBoxList)gv_ApprovalPathDetail.Rows[i].FindControl("chkNotificationApp");
               


                MiscellaneousInfoRoutingPathDAO Routing = new MiscellaneousInfoRoutingPathDAO();


                Routing.IsMinimumApprovalPerson = chkMimimumCount.Checked;
                Routing.Seq_No = Convert.ToInt32(ddlSequenceList.SelectedValue);
                Routing.EmpInfoId = Convert.ToInt32(ApphfEmpInfoId.Value.Trim());

                Routing.CanEdit = chkIsEdit.Checked;
                Routing.IsEmailNotification = chkNotificationApp.Items[0].Selected;
                Routing.IsSMSNotification = chkNotificationApp.Items[1].Selected;


                RoutingPath.Add(Routing);
            }





            List<MeetingInfoDetailDAO> MeetingInfoDetailList = new List<MeetingInfoDetailDAO>();

            for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
            {
                HiddenField hfType = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfType"));
                TextBox txt_EmpMasterCode = ((TextBox)gv_Details_Save.Rows[i].FindControl("txt_EmpMasterCode"));
                HiddenField ShfEmpInfoId = ((HiddenField)gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId"));
                TextBox txt_EmpName = (TextBox)gv_Details_Save.Rows[i].FindControl("txt_EmpName");
                TextBox txt_Designation = (TextBox)gv_Details_Save.Rows[i].FindControl("txt_Designation");

                HiddenField hfNotification = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotification"));
                HiddenField hfPosition = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfPosition"));



                RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
                CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));
                RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("chkPosition"));




                MeetingInfoDetailDAO MeetingInfoDetail = new MeetingInfoDetailDAO();


                MeetingInfoDetail.Type = rbType.SelectedValue;

                if (rbType.SelectedValue == "Employee")
                {

                    try
                    {
                        MeetingInfoDetail.EmpInfoId = Convert.ToInt32(ShfEmpInfoId.Value);
                    }
                    catch (Exception)
                    {

                        MeetingInfoDetail.EmpInfoId =null;
                    }
                }
                else
                {
                    MeetingInfoDetail.EmpInfoId = null;
                   
                   


                }

                MeetingInfoDetail.EmpMasterCode = (txt_EmpMasterCode.Text.Trim());
                MeetingInfoDetail.EmpName = (txt_EmpName.Text.Trim());
                MeetingInfoDetail.Designation = (txt_Designation.Text.Trim());
                MeetingInfoDetail.NotificationEmail = (chkNotification.Items[0].Selected);
                MeetingInfoDetail.NotificationSMS = (chkNotification.Items[1].Selected);
                MeetingInfoDetail.Position = (chkPosition.SelectedValue);

                MemberSearch = MemberSearch + txt_EmpName.Text.Trim() + " ";

                MeetingInfoDetailList.Add(MeetingInfoDetail);
            }



            MeetingEntryDAO aMaster = new MeetingEntryDAO();

            aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            string classfic = "";
            if (ddlClassification.SelectedValue!="")
            {
                classfic = ddlCategory.SelectedItem.Text.Trim();
            }

            MasterSearch = MasterSearch + txtTitle.Text.Trim() + " "  
                 + 
                txtMeetingpurpose.Text.Trim() + " " +
                classfic + " " 
                + txtMeetingDate.Text.Trim() + " " + txtStartTime.Text.Trim() + "" +
                " " + txtEndTime.Text.Trim();
            aMaster.Title = txtTitle.Text.Trim();
           
            aMaster.CategoryID = Convert.ToInt32( ddlCategory.SelectedIndex > 0 ? ddlCategory.SelectedValue : null);
           
            aMaster.MeetingPurpose = txtMeetingpurpose.Text.Trim();
            aMaster.Classification = ddlClassification.SelectedIndex > 0 ? ddlClassification.SelectedValue : null;
            aMaster.MeetingDate = string.IsNullOrEmpty(txtMeetingDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(txtMeetingDate.Text).Date;

            aMaster.StartTime = string.IsNullOrEmpty(txtStartTime.Text)
                         ? (TimeSpan?)null
                         : TimeSpan.Parse(txtStartTime.Text);

            aMaster.EndTime = string.IsNullOrEmpty(txtEndTime.Text)
                      ? (TimeSpan?)null
                      : TimeSpan.Parse(txtEndTime.Text);



            aMaster.IsOfficePremisis = rbLocation.Items[0].Selected;
            aMaster.IsOuterPremisis = rbLocation.Items[1].Selected;
            aMaster.IsVirtualMeeting = rbLocation.Items[2].Selected;

          
           if (rbLocation.Items[0].Selected == true)
        {

            aMaster.OfficeId = Convert.ToInt32(ddlOffice.SelectedIndex > 0 ? ddlOffice.SelectedValue : null);
            aMaster.LocationId = Convert.ToInt32(ddlLocation.SelectedIndex > 0 ? ddlLocation.SelectedValue : null);
            aMaster.FloorId = Convert.ToInt32(ddlFloor.SelectedIndex > 0 ? ddlFloor.SelectedValue : null);
            aMaster.MettingRoomId = Convert.ToInt32(ddlMettingRoomName.SelectedIndex > 0 ? ddlMettingRoomName.SelectedValue : null);
            aMaster.Location = null;
            aMaster.LocationDescription = null;

            }

           if (rbLocation.Items[1].Selected == true)
           {

               aMaster.OfficeId = null;
               aMaster.LocationId = null;
               aMaster.FloorId = null;
               aMaster.MettingRoomId = null;
               aMaster.Location = txtLocation.Text;
               aMaster.LocationDescription = txtDescription.Text;


           }

           if (rbLocation.Items[2].Selected == true)
           {

               aMaster.OfficeId = null;
               aMaster.LocationId = null;
               aMaster.FloorId = null;
               aMaster.MettingRoomId = null;
               aMaster.Location = null;
               aMaster.LocationDescription = null;
               aMaster.Remarks = txtRemarks.Text;


           }

           KeySearch = MasterSearch + " " + MemberSearch+" " + DocumentSearch + " " + AgendaSearch;
            aMaster.KeySearch = KeySearch;
            bool result = false;
            int pk = AMeetingEntryDal.SaveMaster(aMaster, Session["UserId"].ToString());
           
               
                if (pk > 0)
                {
                    result = true;
                    AMeetingEntryDal.SaveDetails(MeetingInfoDetailList, pk, 0);
                    AMeetingEntryDal.SaveRoutingPathDetails(RoutingPath, pk);
                     AMeetingEntryDal.SaveDocumentDetails(DocList, pk);
                     AMeetingEntryDal.SaveAgendaDetails(AgendaList, pk);
                }
            
            else
            {
                result = false;
            }

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MeetingEntry.aspx';",
                    true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }
    protected void editButton_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo();
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (id_mastetID.Value != "")
        {
            // int Id = Convert.ToInt32(Session["UserId"].ToString());
            //Int32 DEl_ID = SaveChangesMasterDelete(Id);



            bool ss = AMeetingEntryDal.DeleteMaster(id_mastetID.Value);
            if (ss)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert",
            "alert('Operation Successful...');window.location ='MeetingStatusList.aspx';",
            true);
            }
            else
            {
                AlertMessageBoxShow("Operation Faild!!!!");
            }


        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MeetingStatusList.aspx");
    }

    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlOffice.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlOffice.SelectedValue))
            {
                ddlLocation.DataSource = dt;
                ddlLocation.DataValueField = "Value";
                ddlLocation.DataTextField = "TextField";
                ddlLocation.DataBind();
            }
        }
        else
        {
            ddlLocation.Items.Clear();
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = "";
        }
    }

    protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLFloor(ddlLocation.SelectedValue))
            {
                ddlFloor.DataSource = dt;
                ddlFloor.DataValueField = "Value";
                ddlFloor.DataTextField = "TextField";
                ddlFloor.DataBind();
            }
        }

        else
        {
            ddlFloor.Items.Clear();
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = "";
        }
    }

    protected void ddlFloor_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFloor.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLMeetingRoom(ddlFloor.SelectedValue))
            {
                ddlMettingRoomName.DataSource = dt;
                ddlMettingRoomName.DataValueField = "Value";
                ddlMettingRoomName.DataTextField = "TextField";
                ddlMettingRoomName.DataBind();
            }
        }

        else
        {
          
            ddlMettingRoomName.Items.Clear();
            txtCapacity.Text = "";
        }
    }

    protected void ddlMettingRoomName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMettingRoomName.SelectedIndex > 0)
        {
            DataTable dtMaster = AMeetingEntryDal.GetCapacityDataByMeetingId(ddlMettingRoomName.SelectedValue);
            if (dtMaster.Rows.Count > 0)
            {
                txtCapacity.Text = dtMaster.Rows[0]["MeetingRoomCapacity"].ToString();
            }
        }
        else
        {

            
            txtCapacity.Text = "";
        }
    }

    protected void btn_DetailsAdd_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();

        aTable.Columns.Add("Type");

        aTable.Columns.Add("EmpMasterCode");
        aTable.Columns.Add("EmpInfoId");
        aTable.Columns.Add("EmpName");
        aTable.Columns.Add("Designation");
        aTable.Columns.Add("NotificationEmail");
        aTable.Columns.Add("NotificationSMS");
        aTable.Columns.Add("Position");
        

        DataRow dr;



        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            HiddenField hfType = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfType"));
            TextBox txt_EmpMasterCode = ((TextBox)gv_Details_Save.Rows[i].FindControl("txt_EmpMasterCode"));
            HiddenField ShfEmpInfoId = ((HiddenField)gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId"));
            TextBox txt_EmpName = (TextBox)gv_Details_Save.Rows[i].FindControl("txt_EmpName");
            TextBox txt_Designation = (TextBox)gv_Details_Save.Rows[i].FindControl("txt_Designation");

            HiddenField HiNotificationEmail = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
            HiddenField hfNotificationSMS = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));
            HiddenField hfPosition = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfPosition"));



            RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
            CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));
            RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("chkPosition"));





        

            dr["EmpInfoId"] = ShfEmpInfoId.Value;

            dr["EmpMasterCode"] = txt_EmpMasterCode.Text;
            dr["EmpName"] = txt_EmpName.Text;
            dr["Designation"] = txt_Designation.Text;
            hfType.Value = "Guest";
            
            dr["Type"] = hfType.Value.Trim();
            if (dr["Type"].ToString() != "")
            {

                dr["Type"] = rbType.SelectedValue;
            }
            dr["NotificationEmail"] = HiNotificationEmail.Value.Trim();
            if (dr["NotificationEmail"].ToString() != "")
            {
                dr["NotificationEmail"] = chkNotification.Items[0].Selected;

            }


            dr["NotificationSMS"] = hfNotificationSMS.Value.Trim();
            if (dr["NotificationSMS"].ToString() != "")
            {
                dr["NotificationSMS"] = chkNotification.Items[1].Selected;

            }

            dr["Position"] = hfPosition.Value.Trim();
            if (dr["Position"].ToString() != "")
            {
                dr["Position"] = hfPosition.Value.Trim();
            } 
                   






            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();
              

                dr["EmpInfoId"] = "";

                dr["EmpMasterCode"] = "";
                dr["EmpName"] = "";
                dr["Designation"] = "";

                hfType.Value = "Guest";

                dr["Type"] = hfType.Value.Trim();
                if (dr["Type"].ToString() != "")
                {

                    rbType.SelectedValue = hfType.Value.Trim();
                }
                dr["NotificationEmail"] = "";
                dr["NotificationSMS"] = "";
                chkNotification.SelectedValue = null;

                dr["Position"] = "";
                chkPosition.SelectedValue = null;

                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        gv_Details_Save.DataSource = null;
        gv_Details_Save.DataBind();
        gv_Details_Save.DataSource = aTable;
        ViewState["gv_Details_List"] = aTable;

        gv_Details_Save.DataBind();

        //using (DataTable dt2 = AMeetingEntryDal.GetEmpInfobyID(ddlCompany.SelectedValue))
        //{

        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
               RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
             rbType.SelectedValue = ((HiddenField)gv_Details_Save.Rows[i].Cells[1].FindControl("hfType"))
                     .Value;



             CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));

             HiddenField HiNotificationEmail = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
             HiddenField hfNotificationSMS = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));

             if (HiNotificationEmail.Value!="")
            {
                try
                {
                    chkNotification.Items[0].Selected = Convert.ToBoolean(HiNotificationEmail.Value);
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                 
            }

             if (hfNotificationSMS.Value != "")
             {
                 try
                 {
                     chkNotification.Items[1].Selected = Convert.ToBoolean(hfNotificationSMS.Value);
                 }
                 catch (Exception)
                 {
                     
                     //throw;
                 }

             }


             RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("chkPosition"));

             HiddenField hfPosition = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfPosition"));

             chkPosition.SelectedValue = hfPosition.Value;
             
        }
        //}

    }

    protected void rbType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((RadioButtonList)sender).Parent.Parent)).RowIndex;

        RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[rowIndex].FindControl("rbType"));

        HiddenField hfType = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfType"));

        hfType.Value = rbType.SelectedValue;


    }

    protected void chkNotification_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBoxList)sender).Parent.Parent)).RowIndex;

        CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[rowIndex].FindControl("chkNotification"));

        HiddenField HiNotificationEmail = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("HiNotificationEmail"));
        HiddenField hfNotificationSMS = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfNotificationSMS"));

        HiNotificationEmail.Value = chkNotification.Items[0].Selected.ToString();
        hfNotificationSMS.Value = chkNotification.Items[1].Selected.ToString();

    }

    protected void chkPosition_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((RadioButtonList)sender).Parent.Parent)).RowIndex;

        RadioButtonList chkPosition = ((RadioButtonList)gv_Details_Save.Rows[rowIndex].FindControl("chkPosition"));

        HiddenField hfPosition = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfPosition"));

        hfPosition.Value = chkPosition.SelectedValue;
    }

    protected void chkNotificationApp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow) (((CheckBoxList) sender).Parent.Parent)).RowIndex;

        CheckBoxList chkNotificationApp =
            ((CheckBoxList) gv_ApprovalPathDetail.Rows[rowIndex].FindControl("chkNotificationApp"));

        HiddenField HiNotificationEmailApp =
            ((HiddenField) gv_ApprovalPathDetail.Rows[rowIndex].FindControl("HiNotificationEmailApp"));
        HiddenField hfNotificationSMSApp =
            ((HiddenField) gv_ApprovalPathDetail.Rows[rowIndex].FindControl("hfNotificationSMSApp"));

        HiNotificationEmailApp.Value = chkNotificationApp.Items[0].Selected.ToString();
        hfNotificationSMSApp.Value = chkNotificationApp.Items[1].Selected.ToString();


    }


    protected void chkIsEdit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkIsEdit = ((CheckBox)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("chkIsEdit"));

        HiddenField hfCanEdit = ((HiddenField)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("hfCanEdit"));


        hfCanEdit.Value = chkIsEdit.Checked.ToString();
        
    }

    protected void chkMimimumCount_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkMimimumCount = ((CheckBox)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("chkMimimumCount"));

        HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_ApprovalPathDetail.Rows[rowIndex].FindControl("hfIsMinimumApprovalPerson"));


        hfIsMinimumApprovalPerson.Value = chkMimimumCount.Checked.ToString();
    }

    protected void ddlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue!="")
        {
              DataTable jobCreationInfos = new DataTable();
              jobCreationInfos = AMeetingEntryDal.GetEmpMemberInfoByCategory(ddlCompany.SelectedValue);
            if (jobCreationInfos.Rows.Count > 0)
            {

                ViewState["gv_Details_List"] = jobCreationInfos;
                 gv_Details_Save.DataSource = jobCreationInfos;
                 gv_Details_Save.DataBind();

                 for (int i = 0; i < jobCreationInfos.Rows.Count; i++)
                 {
                     RadioButtonList rbType = ((RadioButtonList)gv_Details_Save.Rows[i].FindControl("rbType"));
                     rbType.SelectedValue = ((HiddenField)gv_Details_Save.Rows[i].Cells[1].FindControl("hfType"))
                             .Value;





                 }
            }
        }
    }

    protected void btnReturn_OnClick(object sender, EventArgs e)
    {
        try
        {
            int pk = Convert.ToInt32(id_mastetID.Value);
            if (Session["EmpInfoId"].ToString() != "")
            {


                if (divDoc.Visible==true)
                {
                    List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();

                    for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
                    {
                        HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                        Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                        HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfFileName");


                        MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();
                        DocA.FileName = hfFileName.Value.ToString();
                        DocA.DocumentLink = hfDocumentLink.Value.ToString();
                        DocA.DocumentNote = lbl_DocumentNote.Text.Trim();


                        DocList.Add(DocA);
                    }
                    AMeetingEntryDal.SaveDocumentDetails(DocList, pk);

                }

                MeetingEntryDAO aMaster = new MeetingEntryDAO();

                aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

                aMaster.ActionStatus = "Returned";
                aMaster.RefEmpId = 0;
                aMaster.RefSeqNo = 0;
                aMaster.RefMinAppCount = 0;

                bool status = AMeetingEntryDal.UpdateApprovalMasterById(aMaster);




                MeetingInfoAppLogIdDAO appLogDao = new MeetingInfoAppLogIdDAO();
                {



                    appLogDao.ActionStatus = "Returned";
                    appLogDao.ApprovedDate = DateTime.Now;
                    appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    appLogDao.ForEmpInfoId = 0;
                    appLogDao.MeetingInfoID = pk;

                    appLogDao.Comments = commentsTextBox.Text.Trim();


                }
                int iddddd = AMeetingEntryDal.SavAppLog(appLogDao);

                if (status)
                {
                    DataTable dtEntryBy = AMeetingEntryDal.GetEmpEntryBy(pk.ToString());

                    SenMailForApprved(Convert.ToInt32(dtEntryBy.Rows[0]["EmpInfoId"].ToString()), " Meeting Managment Approval ", @" <br/> <br/> Dear Sir, <br/> 
Your submitted Meeting has been Returned. 
To login into the system please click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/> Thank You.  ");

                    DataTable dtLoop =
                  AMeetingEntryDal.GetEmpRoutingPathRreturn(pk.ToString(), hfSeqNo.Value);


                    for (int i = 0; i < dtLoop.Rows.Count; i++)
                    {


                        SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), "  Meeting Managment Approval ", @" <br/> <br/> Dear Sir, <br/> 
                         Your approved Meeting has been Returned. 
                            To login into the system please click the below link.<br/>  http://182.160.103.234:8088/
                            " + "<br/> Thank You.  ");

                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Operation Successful...');window.location ='MeetingApprovalView.aspx';",
                             true);
                }

               




                //DataTable dtempdata =
                //    AMeetingEntryDal.GetEmpRoutingPathRreturn(pk.ToString(), hfSeqNo.Value);

                //if (dtempdata.Rows.Count > 0)
                //{

                //    DataTable dtIsApptove =
                //     AMeetingEntryDal.GetCheckMinimumApproval(pk.ToString());
                //    bool isAppPerson = false;

                //    if (dtIsApptove.Rows.Count > 0)
                //    {
                //        isAppPerson = Convert.ToBoolean(dtIsApptove.Rows[0]["IsMinimumApprovalPerson"].ToString());
                //    }
                //    bool status = false;
                //    if (isAppPerson)
                //    {
                //        MeetingEntryDAO aMaster = new MeetingEntryDAO();

                //        aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                //        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //        ;
                //        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());
                //        ;

                //        aMaster.Isapproved = false;
                //        aMaster.ActionStatus = "Return";

                //        status = AMeetingEntryDal.UpdateApprovalMasterReturrnById(aMaster);
                //    }
                //    else
                //    {
                //        MeetingEntryDAO aMaster = new MeetingEntryDAO();

                //        aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                //        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //        ;
                //        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());
                //        ;

                //        aMaster.Isapproved = false;
                //        aMaster.ActionStatus = "Return";

                //        status = AMeetingEntryDal.UpdateApprovalMasterforNotIsMinApprovalPersonById(aMaster);
                //    }

                //    if (status)
                //    {
                //        MeetingInfoAppLogIdDAO appLogDao = new MeetingInfoAppLogIdDAO();
                //        {



                //            appLogDao.ActionStatus = "Return";
                //            appLogDao.ApprovedDate = DateTime.Now;
                //            appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                //            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                //            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //            appLogDao.MeetingInfoID = pk;

                //            appLogDao.Comments = commentsTextBox.Text.Trim();


                //        }
                //        int iddddd = AMeetingEntryDal.SavAppLog(appLogDao);
                //    }



                    //ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //         "alert",
                    //         "alert('Operation Successful...');window.location ='MeetingApprovalView.aspx';",
                    //         true);
                }
                else
                {
                    //MeetingEntryDAO aMaster = new MeetingEntryDAO();

                    //aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

                    //aMaster.ActionStatus = "Drafted";
                    //aMaster.RefEmpId = 0;
                    //aMaster.RefSeqNo = 0;
                    //aMaster.RefMinAppCount = 0;

                    //bool status = AMeetingEntryDal.UpdateApprovalMasterById(aMaster);




                    //MeetingInfoAppLogIdDAO appLogDao = new MeetingInfoAppLogIdDAO();
                    //{



                    //    appLogDao.ActionStatus = "Verified";
                    //    appLogDao.ApprovedDate = DateTime.Now;
                    //    appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                    //    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    //    appLogDao.ForEmpInfoId = 0;
                    //    appLogDao.MeetingInfoID = pk;

                    //    appLogDao.Comments = commentsTextBox.Text.Trim();


                    //}
                    //int iddddd = AMeetingEntryDal.SavAppLog(appLogDao);

                    //if (status)
                    //{

                    //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //             "alert",
                    //             "alert('Operation Successful...');window.location ='MiscellaneousInformationApprovalList.aspx';",
                    //             true);
                    //}
                }



                //                            SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
                //An Increment is waiting for your approval. <br/><br/>
                // please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
                //");


          
        }
        catch (Exception)
        {

            //throw;
        }
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

    protected void btnApprove_OnClick(object sender, EventArgs e)
    {
        try
        {
            int pk = Convert.ToInt32(id_mastetID.Value);
            if (Session["EmpInfoId"].ToString() != "")
            {




                if (divDoc.Visible == true)
                {
                    List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();

                    for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
                    {
                        HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                        Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                        HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfFileName");


                        MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();
                        DocA.FileName = hfFileName.Value.ToString();
                        DocA.DocumentLink = hfDocumentLink.Value.ToString();
                        DocA.DocumentNote = lbl_DocumentNote.Text.Trim();


                        DocList.Add(DocA);
                    }
                    AMeetingEntryDal.SaveDocumentDetails(DocList, pk);

                }



                DataTable dtempdata =
                    AMeetingEntryDal.GetEmpRoutingPath(pk.ToString(), hfSeqNo.Value);

                if (dtempdata.Rows.Count > 0)
                {
                    DataTable dtIsApptove =
                    AMeetingEntryDal.GetCheckMinimumApproval(pk.ToString());
                    bool isAppPerson = false;

                    if (dtIsApptove.Rows.Count>0)
                    {
                        isAppPerson = Convert.ToBoolean(dtIsApptove.Rows[0]["IsMinimumApprovalPerson"].ToString()); 
                    }

                    bool status = false;
                    if (isAppPerson)
                    {
                        MeetingEntryDAO aMaster = new MeetingEntryDAO();

                        aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());

                        aMaster.Isapproved = false;
                        aMaster.ActionStatus = "Verified";

                        status = AMeetingEntryDal.UpdateApprovalMasterById(aMaster);
                    }
                    else
                    {
                        MeetingEntryDAO aMaster = new MeetingEntryDAO();

                        aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());

                        aMaster.Isapproved = false;
                        aMaster.ActionStatus = "Verified";

                        status = AMeetingEntryDal.UpdateApprovalMasterforNotIsMinApprovalPersonById(aMaster);

                    }



                    if (status)
                    {
                        MeetingInfoAppLogIdDAO appLogDao = new MeetingInfoAppLogIdDAO();
                        {



                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApprovedDate = DateTime.Now;
                            appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                            appLogDao.MeetingInfoID = pk;

                            appLogDao.Comments = commentsTextBox.Text.Trim();


                        }
                        int iddddd = AMeetingEntryDal.SavAppLog(appLogDao);
                        Session["empAppNoti2"] = "";
                        Session["empAppNoti2"] = appLogDao.ForEmpInfoId;
                    }
                    DataTable dtMaster = AMeetingEntryDal.GetMasterDataById(pk.ToString());
                    if (dtMaster.Rows.Count > 0)
                    {







                        hfRefMinAppCountCheck.Value = dtMaster.Rows[0]["RefMinAppCountCheck"].ToString();

                        if (hfRefMinAppCount.Value == hfRefMinAppCountCheck.Value)
                        {
                            MeetingEntryDAO aMaster = new MeetingEntryDAO();

                            aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);



                            aMaster.RefEmpId = 0;
                            aMaster.Isapproved = true;
                            aMaster.ActionStatus = "Approved";

                            AMeetingEntryDal.FinalUpdateApprovalMasterById(aMaster);


                            MeetingInfoAppLogIdDAO appLogDao = new MeetingInfoAppLogIdDAO();
                            {



                                appLogDao.ActionStatus = "Approved";
                                appLogDao.ApprovedDate = DateTime.Now;
                                appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(0);
                                appLogDao.MeetingInfoID = pk;

                                appLogDao.Comments = commentsTextBox.Text.Trim();


                            }
                            int iddddd = AMeetingEntryDal.SavAppLog(appLogDao);

                            EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();

                            DataTable dtLoop = AMeetingEntryDal.GetEmpAllApprovalInfo(pk.ToString());

                            DataTable finapp = aEmployeeRequsitionDal.getFinalApprovePersonInfo(appLogDao.PreEmpInfoId.ToString());
                            string finEmpName = "";
                            string finEmpdes = "";
                            if (finapp.Rows.Count > 0)
                            {
                                finEmpName = finapp.Rows[0]["EmpName"].ToString();
                                finEmpdes = finapp.Rows[0]["Designation"].ToString();
                            }

                            if (dtLoop.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtLoop.Rows.Count; i++)
                                {


                                    SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), "  Meeting Approved ", @"  <br/> Dear Sir, <br/>
Meeting Information has been approved. <br/><br/><br/>Final Approved By:<br/>" + finEmpName + @"<br/>" + finEmpdes + @"


<br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");

                                }
                            }
                        }
                        else
                        {
                            SenMailForApprved(Convert.ToInt32(Session["empAppNoti2"].ToString()),
                                                          " Meeting Approval ", @"  <br/> Dear Sir, <br/>
Meeting Information is waiting for your Approval.<br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                        }



                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Operation Successful...');window.location ='MeetingApprovalView.aspx';",
                             true);
                }
                else
                {

                    DataTable dtempdataFinApp =
               AMeetingEntryDal.GetEmpRoutingPath2(pk.ToString(), hfSeqNo.Value);
                    DataTable dtIsApptove =
                    AMeetingEntryDal.GetCheckMinimumApproval(pk.ToString());
                    bool isAppPerson = false;

                    if (dtIsApptove.Rows.Count > 0)
                    {
                        isAppPerson = Convert.ToBoolean(dtIsApptove.Rows[0]["IsMinimumApprovalPerson"].ToString());
                    }
                    if (dtempdataFinApp.Rows.Count > 0)
                    {
                        if (isAppPerson)
                        {
                            MeetingEntryDAO aMaster = new MeetingEntryDAO();

                            aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                            aMaster.RefEmpId = Convert.ToInt32(0);
                            aMaster.RefSeqNo = Convert.ToInt32(dtempdataFinApp.Rows[0]["Seq_No"].ToString());

                            aMaster.Isapproved = false;
                            aMaster.ActionStatus = "Verified";

                            AMeetingEntryDal.UpdateApprovalMasterById(aMaster);
                        }
                        else
                        {
                            MeetingEntryDAO aMaster = new MeetingEntryDAO();

                            aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                            aMaster.RefEmpId = Convert.ToInt32(0.ToString());
                            aMaster.RefSeqNo = Convert.ToInt32(dtempdataFinApp.Rows[0]["Seq_No"].ToString());

                            aMaster.Isapproved = false;
                            aMaster.ActionStatus = "Verified";
                            AMeetingEntryDal.UpdateApprovalMasterforNotIsMinApprovalPersonById(aMaster);
                        }

                        DataTable dtMaster = AMeetingEntryDal.GetMasterDataById(pk.ToString());
                        if (dtMaster.Rows.Count > 0)
                        {
                            hfRefMinAppCountCheck.Value = dtMaster.Rows[0]["RefMinAppCountCheck"].ToString();

                            if (hfRefMinAppCount.Value == hfRefMinAppCountCheck.Value)
                            {
                                MeetingEntryDAO aMaster = new MeetingEntryDAO();

                                aMaster.MeetingInfoID = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);




                                aMaster.Isapproved = true;
                                aMaster.ActionStatus = "Approved";

                                AMeetingEntryDal.FinalUpdateApprovalMasterById(aMaster);


                                MeetingInfoAppLogIdDAO appLogDao = new MeetingInfoAppLogIdDAO();
                                {



                                    appLogDao.ActionStatus = "Approved";
                                    appLogDao.ApprovedDate = DateTime.Now;
                                    appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                    appLogDao.ForEmpInfoId = Convert.ToInt32(0);
                                    appLogDao.MeetingInfoID = pk;

                                    appLogDao.Comments = commentsTextBox.Text.Trim();


                                }
                                int iddddd = AMeetingEntryDal.SavAppLog(appLogDao);
                                EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();

                                DataTable dtLoop = AMeetingEntryDal.GetEmpAllApprovalInfo(pk.ToString());

                                DataTable finapp = aEmployeeRequsitionDal.getFinalApprovePersonInfo(appLogDao.PreEmpInfoId.ToString());
                                string finEmpName = "";
                                string finEmpdes = "";
                                if (finapp.Rows.Count > 0)
                                {
                                    finEmpName = finapp.Rows[0]["EmpName"].ToString();
                                    finEmpdes = finapp.Rows[0]["Designation"].ToString();
                                }

                                if (dtLoop.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dtLoop.Rows.Count; i++)
                                    {


                                        SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), "  Meeting Approved ", @"  <br/> Dear Sir, <br/>
Meeting Information has been approved. <br/><br/><br/>Final Approved By:<br/>" + finEmpName + @"<br/>" + finEmpdes + @"


<br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");

                                    }
                                }

                            }



                        }
                    }



                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Operation Successful...');window.location ='MeetingApprovalView.aspx';",
                             true);

                }



                //                            SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
                //An Increment is waiting for your approval. <br/><br/>
                // please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
                //");


            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
}