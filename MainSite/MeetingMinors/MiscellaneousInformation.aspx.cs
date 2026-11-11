using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MiscellaneousInformation : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
         
        if (!IsPostBack)
        {
           ButtonVisible();
            LoadDropDownList();
            LoadInitialgv_Member_Save();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

               id_mastetID.Value=  (Request.QueryString["MID"]);

               DataTable dtMaster = AMAsterDal.GetMasterDataById(id_mastetID.Value);
               if (dtMaster.Rows.Count > 0) 
                {

                    ddlCompany.SelectedValue = dtMaster.Rows[0]["CompanyId"].ToString();
                    ddlCompany_OnSelectedIndexChanged(null, null);
                    txtTitle.Text = dtMaster.Rows[0]["Title"].ToString();
                    txtPurpose.Text = dtMaster.Rows[0]["Purpose"].ToString();
                   
                }

               DataTable dtDoc = AMAsterDal.GetDocDataById(id_mastetID.Value);
               if (dtDoc.Rows.Count > 0)
               {
                   ViewState["DocGrid_List"] = dtDoc;
                   gv_DocumentUpload.DataSource = dtDoc;
                   gv_DocumentUpload.DataBind();
               }


               DataTable dtMember_List = AMAsterDal.GetMemberListDataById(id_mastetID.Value);
               if (dtMember_List.Rows.Count > 0)
               {
                   ViewState["gv_Member_List"] = dtMember_List;
                   gv_Member.DataSource = dtMember_List;
                   gv_Member.DataBind();

                   for (int i = 0; i < gv_Member.Rows.Count; i++)
                   {
                       DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                       using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
                       {



                           ddlEmpName.DataSource = dt;
                           ddlEmpName.DataValueField = "EmpInfoId";
                           ddlEmpName.DataTextField = "EmpName";
                           ddlEmpName.DataBind();
                           ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                           ddlEmpName.SelectedIndex = 0;

                       }
                   }

                   for (int i = 0; i < gv_Member.Rows.Count; i++)
                   {
                       RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

                       rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                             .Value;
                       HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
                       DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                       TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


                       if (rbType.SelectedValue == "Employee")
                       {


                           ddlEmpName.Visible = true;
                           txt_EmpName.Visible = false;


                           ddlEmpName.SelectedValue = MemEmpInfoId.Value;
                       }

                       if (rbType.SelectedValue == "Guest")
                       {

                           ddlEmpName.Visible = false;
                           txt_EmpName.Visible = true;


                       }




                   }
               }


               DataTable dtDetailos = AMAsterDal.GetDelsDataById(id_mastetID.Value);
               if (dtDetailos.Rows.Count > 0)
               {
                   ViewState["gv_Details_List"] = dtDetailos;
                   gv_Details_Save.DataSource = dtDetailos;
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
                       DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                       HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

                       ddlSequenceList.SelectedValue = hfSeq_No.Value;




                       HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                       HiddenField hfCanEdit = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfCanEdit"));
                       HiddenField HiNotificationEmailApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                       HiddenField hfNotificationSMSApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));


                       CheckBox chkMimimumCount = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkMimimumCount");

                       if (hfIsMinimumApprovalPerson.Value != "")
                       {

                           chkMimimumCount.Checked = Convert.ToBoolean(hfIsMinimumApprovalPerson.Value);

                       }


                       CheckBox chkIsEdit = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkIsEdit");

                       if (hfCanEdit.Value != "")
                       {
                           chkIsEdit.Checked = Convert.ToBoolean(hfCanEdit.Value);

                       }
                       CheckBoxList chkNotificationApp = (CheckBoxList)gv_Details_Save.Rows[i].Cells[0].FindControl("chkNotification");


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
               if (gv_Details_Save.Rows.Count == 0)
               {
                   lblstatus.Text = "No Approval Path have been  selected.";
               }
               else
               {
                   lblstatus.Text = "";
               }
            }
        }
    }


    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
       
        
    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
                lbDraft.Visible = true;
                
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
                lbDraft.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {

            Response.Redirect("MiscellaneousInformationView.aspx");
        }


    }


    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        AMAsterDal.GetCompanyListIntoDropdownAll(ddlComSearch);
        ddlCompany.SelectedIndex = 1;
        ddlComSearch.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);
        ddlComSearch_OnSelectedIndexChanged(null, null);

        using (DataTable dt = AMAsterDal.GetDDLEmpInfoWith())
        {
            ddlEmp.DataSource = dt;
            ddlEmp.DataValueField = "Value";
            ddlEmp.DataTextField = "TextField";
            ddlEmp.DataBind();
            ddlEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            ddlEmp.SelectedIndex = 0;
        }

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MiscellaneousInformationView.aspx");
    }

    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
        AddNewDocGrid_List();
            
        }
    }

    private bool docVali()
    {
        lblMsg.Text = "";
        if (hfDocFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please click Document Upload Button", this);
            
            return false;
        }
        if (txtSummaryNote.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);
            lblMsg.Text = "<b>" + hfDocFileName.Value + "</b> has been uploaded.";
            return false;
        }
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


                string extension;

                extension = Path.GetExtension(hfDocFile.Value);
                //jpg, png,xlsx,pdf,txt,doc,docx
                if (extension == ".jpg" || extension == ".png")
                {
                    drCurrentRow["DocumentLinkPreview"] = "http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value;
                }
                else
                {
                    drCurrentRow["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value + "&embedded=true";
                }
                
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
            dt.Columns.Add(new DataColumn("DocumentLinkPreview", typeof(string)));

           

            dr = dt.NewRow();
            string extension;

            extension = Path.GetExtension(hfDocFile.Value);
            //jpg, png,xlsx,pdf,txt,doc,docx
            if (extension == ".jpg" || extension == ".png")
            {
                dr["DocumentLinkPreview"] = "http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value;
            }
            else
            {
                dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value + "&embedded=true";
            }

            dr["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;
            //dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value + "&embedded=true";
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
                    HiddenField hfDocumentLinkPreview = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLinkPreview");
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");
                    

                    if (i < dt.Rows.Count - 1)
                    {
                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();
                        hfDocumentLinkPreview.Value = dt.Rows[i]["DocumentLinkPreview"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();
                      
                    }

                    rowIndex++;
                }
            }
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




    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        if (FUDocument.HasFile)
        {
            string _fileExt = System.IO.Path.GetExtension(FUDocument.FileName);
            string AdsFile = "Meeting_Mis_DOC_" + Guid.NewGuid().ToString() + Path.GetExtension(FUDocument.FileName);
            //  fileName = guid.ToString() + imageFileUpload.FileName;
            FUDocument.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
            HyperLink2.NavigateUrl = "../UploadImg/"+AdsFile;
         //   HyperLink2.Text = "Uploaded Successfully";
        }
        else
        {
            HyperLink2.NavigateUrl = "";
         //   HyperLink2.Text = "No File Uploaded";
        }
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

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
           

            if (Session["UserTypeId"].ToString() == "3" ||
                Session["UserTypeId"].ToString() == "4")
            {
                using (DataTable dt2 = AMAsterDal.GetDDLRoutingPath(ddlCompany.SelectedValue))
                {
                    rbRoutingPath.DataSource = dt2;
                    rbRoutingPath.DataValueField = "Value";
                    rbRoutingPath.DataTextField = "TextField";
                    rbRoutingPath.DataBind();
                }
            }
            else
            {

                if (Session["DepartmentId"].ToString()!="")
                {
                    using (DataTable dt2 = AMAsterDal.GetDDLRoutingPathDepartment(ddlCompany.SelectedValue, Session["DepartmentId"].ToString()))
                    {
                        rbRoutingPath.DataSource = dt2;
                        rbRoutingPath.DataValueField = "Value";
                        rbRoutingPath.DataTextField = "TextField";
                        rbRoutingPath.DataBind();
                    }
                }
                else
                {
                    using (DataTable dt2 = AMAsterDal.GetDDLRoutingPathDivision(ddlCompany.SelectedValue, Session["DivisionId"].ToString()))
                    {
                        rbRoutingPath.DataSource = dt2;
                        rbRoutingPath.DataValueField = "Value";
                        rbRoutingPath.DataTextField = "TextField";
                        rbRoutingPath.DataBind();
                    }
                }
                
            }
        }
         else
         {

            
             rbRoutingPath.Items.Clear();
         }
    }
    ShowMessage aShowMessage = new ShowMessage();
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
        
            LoadEMPInfo();
        
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    private string GenerateParameter()
    {
        string parameter = " ";

        //if (ddlComSearch.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    com.CompanyId = '" + ddlComSearch.SelectedValue + "'";
        //}

        //if (ddlDivision.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    div.DivisionId = '" + ddlDivision.SelectedValue + "'";
        //}

        //if (ddlDepartment.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    Dpt.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        //}
        if (ddlEmp.SelectedIndex > 0)
        {
            parameter = parameter + "  and   e.EmpInfoId = '" + ddlEmp.SelectedValue + "'";
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
    public bool CheckEmpList()
    {
        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
            for (int j = 0; j < gv_Details_Save.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                    Label SSStxt_empId = (Label)gv_Details_Save.Rows[j].FindControl("Slbl_EmpMasterCode");

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
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_EmpListSearch.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;

            CheckBox chkSelect = ((CheckBox)gv_EmpListSearch.Rows[i].FindControl("chkSelect"));
            HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearch.Rows[i].FindControl("hfEmpInfoId"));

            if (hfEmpInfoId.Value == Session["EmpInfoId"].ToString())
            {
                chkSelect.Checked = false;
                AlertMessageBoxShow("Inititor can not be added for approval.");
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);

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
                    DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;




                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));


                    CheckBox chkMimimumCount = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkMimimumCount");

                  
                            chkMimimumCount.Checked = true;
                            
                       


                    CheckBox chkIsEdit = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkIsEdit");

                    if (hfCanEdit.Value != "")
                    {
                        chkIsEdit.Checked = false;

                        if (hfCanEdit.Value == "0")
                        {
                            chkIsEdit.Checked = false;

                        }
                        else
                        {
                            chkIsEdit.Checked = true;

                        }

                    }
                    CheckBoxList chkNotificationApp = (CheckBoxList)gv_Details_Save.Rows[i].Cells[0].FindControl("chkNotification");


                    if (HiNotificationEmailApp.Value != "")
                    {

                        if (HiNotificationEmailApp.Value == "0")
                        {
                            chkNotificationApp.Items[0].Selected = false;

                        }
                        else
                        {
                            chkNotificationApp.Items[0].Selected = true;

                        }

                    }

                    if (hfNotificationSMSApp.Value != "")
                    {

                        if (hfNotificationSMSApp.Value == "0")
                        {
                            chkNotificationApp.Items[1].Selected = false;

                        }
                        else
                        {
                            chkNotificationApp.Items[1].Selected = true;

                        }




                    }

                }
            }
            else
            {
                ViewState["gv_Details_List"] = null;
                //Re bind the GridView for the updated data  
                gv_Details_Save.DataSource = null;
                gv_Details_Save.DataBind();
            }
        }

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
                aDataTable.Columns.Add("DivisionName");
                aDataTable.Columns.Add("DepartmentName");
                aDataTable.Columns.Add("Type");
                aDataTable.Columns.Add("NotificationEmail");
                aDataTable.Columns.Add("NotificationSMS");
                aDataTable.Columns.Add("IsMinimumApprovalPerson");
                aDataTable.Columns.Add("Seq_No");
                aDataTable.Columns.Add("CanEdit");



                DataRow dataRow = null;

                for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
                    HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearch.Rows[i].FindControl("hfEmpInfoId"));
                    Label lbl_EmpMasterCode = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpMasterCode");
                    Label lbl_EmpName = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpName");
                    Label lbl_Designation = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Designation");
                    Label lbl_DivisionName = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_DivisionName");
                    Label lbl_DepartmentName = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_DepartmentName");
                  
                    if (chkSelect.Checked)
                    {
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {



                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] = hfEmpInfoId.Value;

                            dataRow["EmpMasterCode"] = lbl_EmpMasterCode.Text;
                            dataRow["EmpName"] = lbl_EmpName.Text;
                            dataRow["Designation"] = lbl_Designation.Text;
                            dataRow["DivisionName"] = lbl_DivisionName.Text;
                            dataRow["DepartmentName"] = lbl_DepartmentName.Text;

                           

                            dataRow["NotificationEmail"] = "";
                            dataRow["NotificationSMS"] = "";
                            dataRow["IsMinimumApprovalPerson"] = "";
                            dataRow["Seq_No"] = "";
                            dataRow["CanEdit"] = "";


                          
                          



                            aDataTable.Rows.Add(dataRow);
                        }
                    }
                }
                for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                {


                    HiddenField ShfEmpInfoId = ((HiddenField)gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId"));
                    Label Slbl_EmpMasterCode = (Label)gv_Details_Save.Rows[i].FindControl("Slbl_EmpMasterCode");
                    Label Slbl_EmpName = (Label)gv_Details_Save.Rows[i].FindControl("Slbl_EmpName");
                    Label Slbl_Designation = (Label)gv_Details_Save.Rows[i].FindControl("Slbl_Designation");
                    Label Slbl_DivisionName = (Label)gv_Details_Save.Rows[i].FindControl("Slbl_DivisionName");
                    Label Slbl_DepartmentName = (Label)gv_Details_Save.Rows[i].FindControl("Slbl_DepartmentName");


                    HiddenField HiNotificationEmail = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                    HiddenField hfNotificationSMS = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));
                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    CheckBox chkMimimumCount = ((CheckBox)gv_Details_Save.Rows[i].FindControl("chkMimimumCount"));
                    CheckBox chkIsEdit = ((CheckBox)gv_Details_Save.Rows[i].FindControl("chkIsEdit"));




                    DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");
                    HiddenField hfCanEdit = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfCanEdit");

                   

                  

                    
                    CheckBoxList chkNotification = ((CheckBoxList)gv_Details_Save.Rows[i].FindControl("chkNotification"));
                  
                   


                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = ShfEmpInfoId.Value;

                    dataRow["EmpMasterCode"] = Slbl_EmpMasterCode.Text;
                    dataRow["EmpName"] = Slbl_EmpName.Text;
                    dataRow["Designation"] = Slbl_Designation.Text;
                    dataRow["DivisionName"] = Slbl_DivisionName.Text;
                    dataRow["DepartmentName"] = Slbl_DepartmentName.Text;
                    dataRow["IsMinimumApprovalPerson"] = hfIsMinimumApprovalPerson.Value;


                    dataRow["CanEdit"] = hfCanEdit.Value;
                    dataRow["Seq_No"] = hfSeq_No.Value;

                    if (hfCanEdit.Value != "")
                    {

                        if (hfCanEdit.Value.Trim() == "0")
                        {
                            chkIsEdit.Checked = false;
                        }
                        else
                        {
                            chkIsEdit.Checked = true;
                        }


                    }


                    ddlSequenceList.SelectedValue = hfSeq_No.Value;
 
                            chkMimimumCount.Checked = true;
                        

                 
                    
                    dataRow["NotificationEmail"] = HiNotificationEmail.Value.Trim();
                    if (HiNotificationEmail.Value.Trim() != "")
                    {
                        if (HiNotificationEmail.Value.Trim()=="0")
                        {
                            chkNotification.Items[0].Selected = false;
                             
                        }
                        else
                        {
                            chkNotification.Items[0].Selected = true;
                            
                        }

                    }

                    dataRow["NotificationSMS"] = hfNotificationSMS.Value.Trim();
                    if (hfNotificationSMS.Value.Trim() != "")
                    {

                        if (hfNotificationSMS.Value.Trim() == "0")
                        {
                            chkNotification.Items[1].Selected = false;

                        }
                        else
                        {
                            chkNotification.Items[1].Selected = true;

                        }
                    

                    }

                    
                   





                    aDataTable.Rows.Add(dataRow);
                }
                ViewState["gv_Details_List"] = aDataTable;
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
                    DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;




                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));


                    CheckBox chkMimimumCount = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkMimimumCount");

                    
                            chkMimimumCount.Checked = true;
                       


                    CheckBox chkIsEdit = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkIsEdit");

                    if (hfCanEdit.Value != "")
                    {

                        if (hfCanEdit.Value.Trim() == "0")
                        {
                            chkIsEdit.Checked = false;
                        }
                        else
                        {
                            chkIsEdit.Checked = true;
                        }


                    }
                    CheckBoxList chkNotificationApp = (CheckBoxList)gv_Details_Save.Rows[i].Cells[0].FindControl("chkNotification");


                    if (HiNotificationEmailApp.Value != "")
                    {

                        if (HiNotificationEmailApp.Value=="0")
                        {
                            chkNotificationApp.Items[0].Selected = false;
                            
                        }
                        else
                        {
                            chkNotificationApp.Items[0].Selected = true;
                            
                        }

                    }

                    if (hfNotificationSMSApp.Value != "")
                    {

                        if (hfNotificationSMSApp.Value == "0")
                        {
                            chkNotificationApp.Items[1].Selected = false;

                        }
                        else
                        {
                            chkNotificationApp.Items[1].Selected = true;

                        }


                      

                    }

                }
            }
            else
            {
                AlertMessageBoxShow("Already Exist !!!");
            }
        }
        if (gv_Details_Save.Rows.Count == 0)
        {
            lblstatus.Text = "No Approval Path have been  selected.";
        }
        else
        {
            lblstatus.Text = "";
        }

        gv_EmpListSearch.DataSource = null;
        gv_EmpListSearch.DataBind();
    }

    protected void rbRoutingPath_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       

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

        if (grade=="")
        {
            AlertMessageBoxShow("Select Routing Path!!!");
        }
        else
        {

            DataTable jobCreationInfos = new DataTable();
            DataTable dtchk = new DataTable();
            dtchk = AMAsterDal.GetEMpInfoFromRoutingPathidd(grade, Session["EmpInfoId"].ToString());

            if (dtchk.Rows.Count==0)
            {
                jobCreationInfos = AMAsterDal.GetEMpInfoFromRoutingPath(grade);
                if (jobCreationInfos.Rows.Count > 0)
                {
                    ViewState["gv_Details_List"] = jobCreationInfos;
                    gv_Details_Save.DataSource = jobCreationInfos;
                    gv_Details_Save.DataBind();


                    for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                    {
                        DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");


                        ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One..", String.Empty));
                        for (int k = 1; k < gv_Details_Save.Rows.Count + 1; k++)
                        {
                            ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                        }
                    }

                    for (int i = 0; i < jobCreationInfos.Rows.Count; i++)
                    {
                        DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                        HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

                        ddlSequenceList.SelectedValue = hfSeq_No.Value;

                    }

                }
                else
                {
                    gv_Details_Save.DataSource = null;
                    gv_Details_Save.DataBind();


                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#exampleModal').modal('hide')", true); 

            }
            else
            {
                AlertMessageBoxShow("This inititor is in the approval path.");
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal').modal('show')", true);
                
            }
        
          
           
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
    protected void chkMimimumCount_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkMimimumCount = ((CheckBox)gv_Details_Save.Rows[rowIndex].FindControl("chkMimimumCount"));

        HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfIsMinimumApprovalPerson"));


        hfIsMinimumApprovalPerson.Value = chkMimimumCount.Checked.ToString();
    }
    protected void chkIsEdit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkIsEdit = ((CheckBox)gv_Details_Save.Rows[rowIndex].FindControl("chkIsEdit"));

        HiddenField hfCanEdit = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfCanEdit"));


        hfCanEdit.Value = chkIsEdit.Checked.ToString();

    }
    protected void chkNotificationApp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBoxList)sender).Parent.Parent)).RowIndex;

        CheckBoxList chkNotificationApp =
            ((CheckBoxList)gv_Details_Save.Rows[rowIndex].FindControl("chkNotification"));

        HiddenField HiNotificationEmailApp =
            ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("HiNotificationEmail"));
        HiddenField hfNotificationSMSApp =
            ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfNotificationSMS"));

        HiNotificationEmailApp.Value = chkNotificationApp.Items[0].Selected.ToString();
        hfNotificationSMSApp.Value = chkNotificationApp.Items[1].Selected.ToString();


    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation(string actionStatus)
    {

        if (ddlCompany.SelectedValue == "")
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

        //if (txtPurpose.Text == "")
        //{
        //    aShowMessage.ShowMessageBox("please fill out this field", this);
        //    txtPurpose.Focus();
        //    return false;
        //}

        if (actionStatus != "Drafted")
        {
            if (gv_DocumentUpload.Rows.Count == 0)
            {
                aShowMessage.ShowMessageBox("Please Upload Document", this);

                return false;
            }
        }

        //if (gv_Details_Save.Rows.Count == 0)
        //{
        //    aShowMessage.ShowMessageBox("Please Add Member List", this);

        //    return false;
        //}


        if (ddlDepartment.SelectedValue != "")
        {


            string Title = txtTitle.Text.ToUpper().Trim();
            string Company = ddlCompany.SelectedValue;

            using (DataTable dt = AMAsterDal.CheckRoutingPath(Title, Company))
            {
                if (id_mastetID.Value == "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        ShowMessageBox("Title Already Exist!!!");
                        txtPurpose.Focus();

                        return false;
                    }
                }
                else
                {
                    DataTable dt2 = AMAsterDal.CheckRoutingPathEdit(Title, Company, id_mastetID.Value);
                    if (dt2.Rows.Count > 0)
                    {


                        ShowMessageBox("Title Already Exist!!!");
                        txtPurpose.Focus();
                        return false;
                    }
                }

            }



        }

        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
           
            DropDownList ddlSequenceList = (DropDownList) gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


            if (ddlSequenceList.SelectedValue=="")
            {
                aShowMessage.ShowMessageBox("please fill out Sequence", this);
                ddlSequenceList.Focus();
                return false;
            }
        }

        return true;
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo("");
    }
    public bool CheckSeq()
    {


        //for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        //{
        //    for (int j = 0; j < gv_Details_Save.Rows.Count; j++)
        //    {
        //        if (i != j)
        //        {
        //            DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList");
        //            DropDownList ddlSequenceList2 = (DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList");
        //            if (ddlSequenceList.SelectedValue ==
        //                ddlSequenceList2.SelectedValue)
        //            {

        //                return false;
        //            }
        //        }
        //    }
        //}

        
        return true;
    }
    private void SaveUpdateInfo(string actionStatus)
    {
        if (Validation(actionStatus) == true)
        {
            if (CheckSeq())
            {
                List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();


                string DocNote = "";
                string Member = "";
                for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField) gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                    Label lbl_DocumentNote = (Label) gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                    HiddenField hfFileName = (HiddenField) gv_DocumentUpload.Rows[i].FindControl("hfFileName");


                    MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();

                    DocA.DocumentLink = hfDocumentLink.Value.ToString();
                    DocA.FileName = hfFileName.Value.ToString();
                    DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                    DocNote = DocNote + lbl_DocumentNote.Text.Trim() + " ";

                    DocList.Add(DocA);
                }


                List<MiscellaneousInfoRoutingPathDAO> RoutingPath = new List<MiscellaneousInfoRoutingPathDAO>();

                int _RefEmpId = 0;
                int _RefSeqNo = 0;
                int _RefMinAppCount = 0;

                for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                {
                    CheckBox chkMimimumCount = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkMimimumCount");
                    DropDownList ddlSequenceList = (DropDownList) gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


                    HiddenField ShfEmpInfoId = (HiddenField) gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId");


                    CheckBox chkIsEdit = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkIsEdit");
                    CheckBoxList chkNotificationApp =
                        (CheckBoxList) gv_Details_Save.Rows[i].FindControl("chkNotification");

                    MiscellaneousInfoRoutingPathDAO Routing = new MiscellaneousInfoRoutingPathDAO();

                    chkMimimumCount.Checked = true;
                    Routing.IsMinimumApprovalPerson = chkMimimumCount.Checked;
                    Routing.Seq_No = Convert.ToInt32(ddlSequenceList.SelectedValue);
                    if (chkMimimumCount.Checked)
                    {
                        _RefMinAppCount++;
                        if (Routing.Seq_No == 1)
                        {
                            _RefEmpId = Convert.ToInt32(ShfEmpInfoId.Value.Trim());
                            _RefSeqNo = (int) Routing.Seq_No;
                        }
                    }

                    Routing.EmpInfoId = Convert.ToInt32(ShfEmpInfoId.Value.Trim());

                    Routing.CanEdit = chkIsEdit.Checked;

                    Routing.IsEmailNotification = chkNotificationApp.Items[0].Selected;


                    if (chkNotificationApp.Items[0].Selected==true)
                    {
                        if (actionStatus != "Drafted")
                        {
                            if (Routing.Seq_No == 1)
                            {
                                SenMailForApprved(Convert.ToInt32(Routing.EmpInfoId),
                                    txtTitle.Text.Trim(),
                                    @"  <br/> Dear Sir, <br/> 
Document is submitted for your Recommendation/Approval in the system.
To login, click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.  ");
                            }
                        }

                    }

                    Routing.IsSMSNotification = chkNotificationApp.Items[1].Selected;

                    RoutingPath.Add(Routing);
                }

                List<MiscellaneousInfoDetailDAO> aEmpList = new List<MiscellaneousInfoDetailDAO>();

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    HiddenField hfType = ((HiddenField) gv_Member.Rows[i].FindControl("hfType"));
                    TextBox txt_EmpMasterCode = ((TextBox) gv_Member.Rows[i].FindControl("txt_EmpMasterCode"));
                    HiddenField MemEmpInfoId = ((HiddenField) gv_Member.Rows[i].FindControl("MemEmpInfoId"));
                    TextBox txt_EmpName = (TextBox) gv_Member.Rows[i].FindControl("txt_EmpName");
                    TextBox txt_Designation = (TextBox) gv_Member.Rows[i].FindControl("txt_Designation");





                    RadioButtonList rbType = ((RadioButtonList) gv_Member.Rows[i].FindControl("rbType"));


                    MiscellaneousInfoDetailDAO AEmp = new MiscellaneousInfoDetailDAO();

                    AEmp.Type = rbType.SelectedValue.ToString();

                    AEmp.EmpInfoId = MemEmpInfoId.Value == "" ? 0 : Convert.ToInt32(MemEmpInfoId.Value);


                    AEmp.EmpMasterCode = txt_EmpMasterCode.Text.ToString();
                    AEmp.EmpName = txt_EmpName.Text.ToString();
                    AEmp.Designation = txt_Designation.Text.ToString();
                    Member = Member + txt_EmpName.Text.Trim() + " ";


                    aEmpList.Add(AEmp);
                }


                MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

                aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

                aMaster.Title = txtTitle.Text.Trim();
                aMaster.Purpose = txtPurpose.Text.Trim();
                aMaster.KeySearch = Member + " " + DocNote;

                aMaster.RefMinAppCountCheck = 0;
                aMaster.Isapproved = false;

                if (actionStatus == "Drafted")
                {
                    aMaster.ActionStatus = actionStatus;
                    aMaster.RefEmpId = 0;
                    aMaster.RefSeqNo = 0;
                    aMaster.RefMinAppCount = 0;
                }
                else
                {
                    
                    aMaster.RefEmpId = _RefEmpId;
                    aMaster.RefSeqNo = _RefSeqNo;
                    aMaster.RefMinAppCount = _RefMinAppCount;

                    if (gv_Details_Save.Rows.Count == 0)
                    {
                        aMaster.Isapproved = true;
                        aMaster.RefEmpId = 0;
                        aMaster.ActionStatus = "Approved";
                    }
                    else
                    {
                        aMaster.ActionStatus = "Initiator";
                    }
                }


                bool result = false;
               
                    int pk = AMAsterDal.SaveMaster(aMaster, Session["UserId"].ToString());
                    if (pk > 0)
                    {
                        result = true;

                        AMAsterDal.SaveRoutingPathDetails(RoutingPath, pk);
                         AMAsterDal.SaveDocumentDetails(DocList, pk);

                     

                        AMAsterDal.SaveDetails(aEmpList, pk);
                        if (actionStatus != "Drafted")
                        {


//del Log
                            if (aMaster.MiscellaneousInfoId > 0)
                            {
                                bool status = AMAsterDal.AuditTrailLogById(pk.ToString(), "Edit");
                            }
                            else
                            {
                                bool status = AMAsterDal.AuditTrailLogById(pk.ToString(), "Initial");
                                
                            }



                            if (gv_Details_Save.Rows.Count > 0)
                            {



                                try
                                {
                                    if (Session["EmpInfoId"].ToString() != "")
                                    {




                                        MiscellaneousInfoAppLogDAO appLogDaoa = new MiscellaneousInfoAppLogDAO();

                                        appLogDaoa.ActionStatus = "Drafted";
                                        appLogDaoa.ApprovedDate = DateTime.Now;
                                        appLogDaoa.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                        appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                                        appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                        appLogDaoa.MiscellaneousInfoId = pk;

                                        appLogDaoa.Comments = "";


                                        int idd = AMAsterDal.SavAppLog(appLogDaoa);


                                        //DataTable dtempdata =
                                        //    AMAsterDal.GetEmpRoutingPath(pk.ToString());
                                        MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                                        {



                                            appLogDao.ActionStatus = "Initiator";
                                            appLogDao.ApprovedDate = DateTime.Now;
                                            appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                            appLogDao.ForEmpInfoId = Convert.ToInt32(_RefEmpId);
                                            appLogDao.MiscellaneousInfoId = pk;

                                            appLogDao.Comments = "";


                                        }
                                        ;
                                        int iddddd = AMAsterDal.SavAppLog(appLogDao);
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
                    }
                    else
                    {
                        result = false;
                    }
               
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='MiscellaneousInformationView.aspx';",
                        true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
            else
            {
                AlertMessageBoxShow("Sequence No Can not be Same");
                
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
    protected void editButton_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo("");
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {


        bool status2 = AMAsterDal.AuditTrailLogById(id_mastetID.Value.ToString(), "Delete");

        bool status = AMAsterDal.DeleteById(id_mastetID.Value);
        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='MiscellaneousInformationView.aspx';",
              true);
        }
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void rbType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((RadioButtonList)sender).Parent.Parent)).RowIndex;

        RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[rowIndex].FindControl("rbType"));

        HiddenField hfType = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("hfType"));


        DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[rowIndex].FindControl("ddlEmpName"));
        TextBox txt_EmpName = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpName"));



        TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpMasterCode"));
        HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("MemEmpInfoId"));
        TextBox txt_Designation = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_Designation"));

        hfType.Value = rbType.SelectedValue;

        if (rbType.SelectedValue == "Employee")
        {
            ddlEmpName.Visible = true;
            txt_EmpName.Visible = false;
           
            txt_EmpName.Text = "";

            using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
            {



                ddlEmpName.DataSource = dt;
                ddlEmpName.DataValueField = "EmpInfoId";
                ddlEmpName.DataTextField = "EmpName";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpName.SelectedIndex = 0;

            }
        }

        if (rbType.SelectedValue == "Guest")
        {
            ddlEmpName.Visible = false;
            txt_EmpName.Visible = true;
            ddlEmpName.SelectedValue = "";
            txt_EmpName.Text = "";
            txt_EmpMasterCode.Text = "";
            MemEmpInfoId.Value = "";
            txt_Designation.Text = "";
           
            ddlEmpName.Items.Clear();
        }

    }

    protected void btn_gv_MemberAdd_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();

        aTable.Columns.Add("Type");

        aTable.Columns.Add("EmpMasterCode");
        aTable.Columns.Add("EmpInfoId");
        aTable.Columns.Add("EmpName");
        aTable.Columns.Add("Designation");
       


        DataRow dr;



        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            HiddenField hfType = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"));
            TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[i].FindControl("txt_EmpMasterCode"));
            HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
            TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");
            TextBox txt_Designation = (TextBox)gv_Member.Rows[i].FindControl("txt_Designation");





            RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));







            dr["EmpInfoId"] = MemEmpInfoId.Value;

            dr["EmpMasterCode"] = txt_EmpMasterCode.Text;
            dr["EmpName"] = txt_EmpName.Text;
            dr["Designation"] = txt_Designation.Text;
           

            dr["Type"] = hfType.Value.Trim();
            if (dr["Type"].ToString() != "")
            {

                dr["Type"] = rbType.SelectedValue;
            }
           



            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();


                dr["EmpInfoId"] = "";

                dr["EmpMasterCode"] = "";
                dr["EmpName"] = "";
                dr["Designation"] = "";

             

                dr["Type"] = hfType.Value.Trim();
                if (dr["Type"].ToString() != "")
                {

                    rbType.SelectedValue = hfType.Value.Trim();
                }
               

                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        gv_Member.DataSource = null;
        gv_Member.DataBind();
        gv_Member.DataSource = aTable;
        ViewState["gv_Member_List"] = aTable;

        gv_Member.DataBind();


        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
            using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
            {



                ddlEmpName.DataSource = dt;
                ddlEmpName.DataValueField = "EmpInfoId";
                ddlEmpName.DataTextField = "EmpName";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpName.SelectedIndex = 0;

            }
        }

        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

            rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                  .Value;
            HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
            DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
            TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


            if (rbType.SelectedValue == "Employee")
            {


                ddlEmpName.Visible = true;
                txt_EmpName.Visible = false;

              
                ddlEmpName.SelectedValue = MemEmpInfoId.Value;
            }

            if (rbType.SelectedValue == "Guest")
            {

                ddlEmpName.Visible = false;
                txt_EmpName.Visible = true;


            }




        }

    }
    private void LoadInitialgv_Member_Save()
    {
        DataTable dtDetails = new DataTable();

        dtDetails.Columns.Add("Type");

        dtDetails.Columns.Add("EmpMasterCode");
        dtDetails.Columns.Add("EmpInfoId");
        dtDetails.Columns.Add("EmpName");
        dtDetails.Columns.Add("Designation");
      


        DataRow dr = null;
        dr = dtDetails.NewRow();

        dr["Type"] = "";

        dr["EmpMasterCode"] = "";
        dr["EmpInfoId"] = "";
        dr["EmpName"] = "";
        dr["Designation"] = "";

       




        dtDetails.Rows.Add(dr);

        gv_Member.DataSource = null;
        gv_Member.DataBind();
        gv_Member.DataSource = dtDetails;
        gv_Member.DataBind();

    }
    protected void btn_gv_Member_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gv_Member_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["gv_Member_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gv_Member_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_Member.DataSource = dt;
                gv_Member.DataBind();



                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    using (DataTable dt22 = _commonDataLoad.GetEmpDDLNewMeetinig())
                    {



                        ddlEmpName.DataSource = dt22;
                        ddlEmpName.DataValueField = "EmpInfoId";
                        ddlEmpName.DataTextField = "EmpName";
                        ddlEmpName.DataBind();
                        ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        ddlEmpName.SelectedIndex = 0;

                    }
                }

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

                    rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                          .Value;
                    HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


                    if (rbType.SelectedValue == "Employee")
                    {


                        ddlEmpName.Visible = true;
                        txt_EmpName.Visible = false;


                        ddlEmpName.SelectedValue = MemEmpInfoId.Value;
                    }

                    if (rbType.SelectedValue == "Guest")
                    {

                        ddlEmpName.Visible = false;
                        txt_EmpName.Visible = true;


                    }




                }

            }
            else
            {
                ViewState["gv_Member_List"] = null;
                //Re bind the GridView for the updated data  
                gv_Member.DataSource = null;
                gv_Member.DataBind();
            }
        }

    }

    protected void ddlEmpName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;

        TextBox txt_EmpName = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpName"));
        TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpMasterCode"));
        HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("MemEmpInfoId"));
        TextBox txt_Designation = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_Designation"));
        DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[rowIndex].FindControl("ddlEmpName"));

        if (ddlEmpName.SelectedValue!="")
        {
            int mid = Convert.ToInt32(ddlEmpName.SelectedValue);
            using (var db = new HRIS_SMCEntities())
            {
                var emp = (from j in db.tblEmpGeneralInfoes
                           
                           where j.EmpInfoId == mid select j).FirstOrDefault();

                txt_EmpMasterCode.Text = emp.EmpMasterCode;
                txt_EmpName.Text = emp.EmpName;

                MemEmpInfoId.Value = emp.EmpInfoId.ToString();
                using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                {
                    txt_Designation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                }
            }

        }
        else
        {
            txt_EmpMasterCode.Text = "";
            MemEmpInfoId.Value = "";
            txt_Designation.Text = "";
        }
    }

    protected void lbDraft_OnClick(object sender, EventArgs e)
    {
        string ActionStatus = "Drafted";
        SaveUpdateInfo(ActionStatus);
    }

    protected void ddlComSearch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDivision.Items.Clear();
        ddlDepartment.Items.Clear();
       
         
            if (ddlComSearch.SelectedValue != "")
            {
                using (DataTable dt = AMAsterDal.GetDDLComDivision(ddlComSearch.SelectedValue))
                {
                    ddlDivision.DataSource = dt;
                    ddlDivision.DataValueField = "Value";
                    ddlDivision.DataTextField = "TextField";
                    ddlDivision.DataBind();
                }

                
            }
            else
            {
                ddlDivision.Items.Clear();
                ddlDepartment.Items.Clear();
            }
        
    }

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkSelect = ((CheckBox)gv_EmpListSearch.Rows[rowIndex].FindControl("chkSelect"));
        HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearch.Rows[rowIndex].FindControl("hfEmpInfoId"));

        if (hfEmpInfoId.Value == Session["EmpInfoId"].ToString())
        {
            chkSelect.Checked = false;

            AlertMessageBoxShow("Inititor can not be added for approval.");
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
            if (ddlSequenceList.SelectedValue!="")
            {
                if (ddlSequenceList2.SelectedValue != "")
                {
                    if (ddlSequenceList.SelectedValue == ddlSequenceList2.SelectedValue)
                    {
                        count++;
                    }
                }


            }

             if (count>1)
        {
            AlertMessageBoxShow("Approval Sequence Already Added");
            ddlSequenceList.SelectedValue = "";
        }
        }

       

    }
}