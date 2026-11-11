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
using DAL.ExecutiveOfficeDocDal;
using DAL.MeetingMinorsDAL;
using DAL.Permission_DAL;
using DAO.ExcOfficeDoc_Dao;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MiscellaneousInformation : System.Web.UI.Page
{


    ExecutiveOfficeDocUpDal AMAsterDal = new ExecutiveOfficeDocUpDal();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        EfectiveDate.Attributes.Add("readonly", "readonly");

        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();

            
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                id_mastetID.Value = (Request.QueryString["MID"]);

                DataTable dtMaster = AMAsterDal.GetMasterDataById(id_mastetID.Value);
                if (dtMaster.Rows.Count > 0)
                {
                    ddlCompany.SelectedValue = dtMaster.Rows[0]["CompanyId"].ToString();

                  //  AMAsterDal.GetExeOfficeCategoryListIntoDropdown(ddlcategory);

                    ddlcategory.SelectedValue = dtMaster.Rows[0]["ExeOfficeDocCatId"].ToString();
                    ddlcategory_OnTextChanged(null, null);
                     

                    try
                    {
                        ddlSubCategory.SelectedValue = dtMaster.Rows[0]["ExeOfficeDocSubCatId"].ToString();

                    }
                    catch (Exception)
                    {
                        ddlSubCategory.SelectedValue = null;
                        //throw;
                    }
                    
                    txtPurpose.Text = dtMaster.Rows[0]["Remarks"].ToString();


                    try
                    {
                       EfectiveDate.Text = Convert.ToDateTime(dtMaster.Rows[0]["DocumentEntryDate"]).ToString("dd-MMM-yyyy");
                    }
                    catch (Exception)
                    {

                        //   throw;
                    }

                }

                DataTable dtDoc = AMAsterDal.GetDocDataById(id_mastetID.Value);
                if (dtDoc.Rows.Count > 0)
                {
                    ViewState["DocGrid_List"] = dtDoc;
                    DocumentUpload.DataSource = dtDoc;
                    DocumentUpload.DataBind();
                }


            }
        }
    }








    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

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
               // lbDraft.Visible = true;

            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
              //  lbDraft.Visible = true;

            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "View")
            {
                editButton.Visible = false;
               // lbDraft.Visible = false;
                delButton.Visible = false;
              //  orBTN.Visible = false;
            }
            Session["Status"] = null;
        }
        else
        {

            Response.Redirect("ExecutiveOfficeDocUploadView.aspx");


        }


    }


    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        AMAsterDal.GetExeOfficeCategoryListIntoDropdown(ddlcategory);


        // ddlCompany_OnSelectedIndexChanged(null, null);


    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExecutiveOfficeDocUploadView.aspx");


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
                DocumentUpload.DataSource = dtCurrentTable;
                DocumentUpload.DataBind();
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
            DocumentUpload.DataSource = dt;
            DocumentUpload.DataBind();
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
                    HiddenField hfDocumentLink = (HiddenField)DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLink");
                    HiddenField hfFileName = (HiddenField)DocumentUpload.Rows[rowIndex].FindControl("hfFileName");
                    HiddenField hfDocumentLinkPreview = (HiddenField)DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLinkPreview");
                    HyperLink HLDocumentLink = (HyperLink)DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");


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
                DocumentUpload.DataSource = dt;
                DocumentUpload.DataBind();
            }
            else
            {
                ViewState["DocGrid_List"] = null;
                //Re bind the GridView for the updated data  
                DocumentUpload.DataSource = null;
                DocumentUpload.DataBind();
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
            string AdsFile = "Exe_Office_DOC_Upload_" + Guid.NewGuid().ToString() + Path.GetExtension(FUDocument.FileName);
            //  fileName = guid.ToString() + imageFileUpload.FileName;
            FUDocument.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
            HyperLink2.NavigateUrl = "../UploadImg/" + AdsFile;
            //   HyperLink2.Text = "Uploaded Successfully";
        }
        else
        {
            HyperLink2.NavigateUrl = "";
            //   HyperLink2.Text = "No File Uploaded";
        }
    }




    ShowMessage aShowMessage = new ShowMessage();
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
        if (ddlCompany.SelectedIndex > 0)
        {
            //  LoadEMPInfo();
        }
        else
        {
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
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
        if (ddlcategory.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            ddlcategory.Focus();
            return false;
        }

        if (EfectiveDate.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            EfectiveDate.Focus();
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
            if (DocumentUpload.Rows.Count == 0)
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
            try
            {

             List<ExeOfficeDocDetailsDao> DocList = new List<ExeOfficeDocDetailsDao>(); 
             string DocNote = "";
            string Member = "";
            for (int i = 0; i < DocumentUpload.Rows.Count; i++)
            {
                HiddenField hfDocumentLink = (HiddenField)DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                Label lbl_DocumentNote = (Label)DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                HiddenField hfFileName = (HiddenField)DocumentUpload.Rows[i].FindControl("hfFileName");
                ExeOfficeDocDetailsDao DocA = new ExeOfficeDocDetailsDao();
                DocA.DocumentLink = hfDocumentLink.Value.ToString();
                DocA.FileName = hfFileName.Value.ToString();
                DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                DocNote = DocNote + lbl_DocumentNote.Text.Trim() + " ";
                DocList.Add(DocA);
            }

            ExeOffiDocUpMasterDao aMaster = new ExeOffiDocUpMasterDao();

            aMaster.ExeOffiDocUpId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

            aMaster.ExeOfficeDocCatId = Convert.ToInt32(ddlcategory.SelectedValue);

            aMaster.Remarks = txtPurpose.Text.Trim();
            aMaster.DocumentEntryDate = string.IsNullOrEmpty(EfectiveDate.Text) ? (DateTime?)null : DateTime.Parse(EfectiveDate.Text).Date;
            aMaster.Isapproved = false;

            //if (actionStatus == "Drafted")
            //{
            //    aMaster.ActionStatus = actionStatus;

            //}
            //else
            //{
            //    aMaster.ActionStatus = "Submitted";
            //}

            bool result = false;

            try
            {
                aMaster.ExeOfficeDocSubCatId = Convert.ToInt32(ddlSubCategory.SelectedValue);
            }
            catch (Exception)
            {
                
            }
           

            int pk = AMAsterDal.SaveMaster(aMaster, Session["UserId"].ToString());
            if (pk > 0)
            {
                result = true;

                AMAsterDal.SaveDocumentDetails(DocList, pk);
            }
            else
            {
                result = false;
            }
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='ExecutiveOfficeDocUploadView.aspx';",
                    true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }

            }
            catch (Exception)
            {

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

    protected void lbDraft_OnClick(object sender, EventArgs e)
    {
        string ActionStatus = "Drafted";
        SaveUpdateInfo(ActionStatus);
    }


    protected void btnOkayRoutingPath_OnClick(object sender, EventArgs e)
    {

    }

    protected void ddlComSearch_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void ddlcategory_OnTextChanged(object sender, EventArgs e)
    {
        if (ddlcategory.SelectedValue != "")
        {
            try
            {
                using (DataTable dtt = AMAsterDal.GetSubCategory(ddlcategory.SelectedValue.ToString()))
                {
                    if (dtt.Rows.Count > 0)
                    {
                        ddlSubCategory.DataSource = dtt;
                        ddlSubCategory.DataValueField = "ExeOfficeDocSubCatId";
                        ddlSubCategory.DataTextField = "ExeOfficeDocSubCate";
                        ddlSubCategory.DataBind();
                        ddlSubCategory.Items.Insert(0, new ListItem("Select any one", String.Empty));
                    }
                    else
                    {
                        ddlSubCategory.Items.Clear();
                    }
                        
                }
            }
            catch (Exception)
            {
                
            }
        }
        else
        {
           
        }
    }
}