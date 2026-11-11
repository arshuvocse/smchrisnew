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
using DAL.MasterSetup_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MiscellaneousInformationApprove : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");


         
        if (!IsPostBack)
        {

            if (Session["Status"] != null)
            {


                LoadDropDownList();


                if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
                {

                    id_mastetID.Value = (Request.QueryString["MID"]);

                    DataTable dtMaster = AMAsterDal.GetMasterDataById(id_mastetID.Value);
                    if (dtMaster.Rows.Count > 0)
                    {
                        hfSeqNo.Value = dtMaster.Rows[0]["RefSeqNo"].ToString();
                        ddlCompany.SelectedValue = dtMaster.Rows[0]["CompanyId"].ToString();
                        lblCompany.Text = ddlCompany.SelectedItem.Text;
                        ddlCompany_OnSelectedIndexChanged(null, null);
                        txtTitle.Text = dtMaster.Rows[0]["Title"].ToString();
                        lblTitle.Text = dtMaster.Rows[0]["Title"].ToString();
                        txtPurpose.Text = dtMaster.Rows[0]["Purpose"].ToString();
                        lblPurpose.Text = dtMaster.Rows[0]["Purpose"].ToString();

                        hfRefMinAppCount.Value = dtMaster.Rows[0]["RefMinAppCount"].ToString();



                    }

                    DataTable dtempdata =
                        AMAsterDal.GetEmpRoutingPath(id_mastetID.Value.ToString(), hfSeqNo.Value);
                    if (dtempdata.Rows.Count > 0)
                    {
                        lblNextApp.Text = dtempdata.Rows[0]["AwEmpName"].ToString();

                    }
                    else
                    {
                        lblNextApp.Text = "You are the final Approver";
                    }









                    DataTable dtDoc = AMAsterDal.GetDocDataById(id_mastetID.Value);
                    if (dtDoc.Rows.Count > 0)
                    {
                        ViewState["DocGrid_List"] = dtDoc;
                        gv_DocumentUpload.DataSource = dtDoc;
                        gv_DocumentUpload.DataBind();


                        for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
                        {
                            HiddenField hfMintuesEntryInfoDocumentID = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfMintuesEntryInfoDocumentID");
                            LinkButton btnDocRemove = (LinkButton)gv_DocumentUpload.Rows[i].FindControl("btnDocRemove");

                            if (hfMintuesEntryInfoDocumentID.Value == "0")
                            {
                                btnDocRemove.Visible = true;
                            }
                            else
                            {
                                btnDocRemove.Visible = false;

                            }

                        }
                    }
                    DataTable dtDocEdit =
                        AMAsterDal.GetEmpCanEditDOC(id_mastetID.Value.ToString());

                    if (dtDocEdit.Rows.Count > 0)
                    {

                        bool CanEdit = Convert.ToBoolean(dtDocEdit.Rows[0]["CanEdit"].ToString());
                        if (CanEdit == true)
                        {
                            divDoc.Visible = true;

                            //gv_DocumentUpload.Columns[3].Visible = true;
                        }
                    }

                    DataTable dtDetailos = AMAsterDal.GetMemberListById(id_mastetID.Value);
                    if (dtDetailos.Rows.Count > 0)
                    {
                        ViewState["gv_Details_List"] = dtDetailos;
                        gv_Member.DataSource = dtDetailos;
                        gv_Member.DataBind();


                    }

                    DataTable dtApplogList = AMAsterDal.GetAppLogCommByJobId2(id_mastetID.Value);
                    if (dtApplogList.Rows.Count > 0)
                    {

                        gv_ApprovalList.DataSource = dtApplogList;
                        gv_ApprovalList.DataBind();

                        for (int i = 0; i < gv_ApprovalList.Rows.Count; i++)
                        {
                            Label lbl_ActionStatus = (Label)gv_ApprovalList.Rows[i].FindControl("lbl_ActionStatus");


                            if (lbl_ActionStatus.Text == "Initiated")
                            {
                                if (i == 0)
                                {
                                    lbl_ActionStatus.Text = "Submitted";
                                }

                                if (i > 0)
                                {
                                    lbl_ActionStatus.Text = "Re-Submitted";
                                }
                            }
                           

                        }


                    }

                   Session["Status"] = null;
                }
            }
            else
            {
                Response.Redirect("MiscellaneousInformationApprovalList.aspx");
                
            }
        }
    }


     
    protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
       
        
    }
    
    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MiscellaneousInformationApprovalList.aspx");
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


                drCurrentRow["FileName"] = hfDocFileName.Value;

                drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();
                drCurrentRow["MiscellaneousInfoDocumentID"] = "0";
                
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["DocGrid_List"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_DocumentUpload.DataSource = dtCurrentTable;
                gv_DocumentUpload.DataBind();

               // gv_DocumentUpload.Columns[3].Visible = true;
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentNote", typeof(string)));
            dt.Columns.Add(new DataColumn("MiscellaneousInfoDocumentID", typeof(string)));
          

            dr = dt.NewRow();


            dr["DocumentLink"] = "../UploadImg/" + hfDocFile.Value;

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

        for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
        {
            HiddenField hfMintuesEntryInfoDocumentID = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfMintuesEntryInfoDocumentID");
            LinkButton btnDocRemove = (LinkButton)gv_DocumentUpload.Rows[i].FindControl("btnDocRemove");

            if (hfMintuesEntryInfoDocumentID.Value=="0")
            {
                btnDocRemove.Visible = true;
            }
            else
            {
                btnDocRemove.Visible = false;
                
            }

        }
        txtSummaryNote.Text = string.Empty;
        HyperLink2.Text = "No File Uploaded";
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
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");
                    HiddenField hfDocumentLinkPreview = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLinkPreview");
                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");
                    HiddenField hfMintuesEntryInfoDocumentID = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfMintuesEntryInfoDocumentID");
                    

                    if (i < dt.Rows.Count - 1)
                    {
                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();
                        hfDocumentLinkPreview.Value = dt.Rows[i]["DocumentLinkPreview"].ToString();
                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();
                        hfMintuesEntryInfoDocumentID.Value = dt.Rows[i]["MiscellaneousInfoDocumentID"].ToString();
                      
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
    protected void gv_DocumentUpload2_PreRender(object sender, EventArgs e)
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

        for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
        {
            HiddenField hfMintuesEntryInfoDocumentID = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfMintuesEntryInfoDocumentID");
            LinkButton btnDocRemove = (LinkButton)gv_DocumentUpload.Rows[i].FindControl("btnDocRemove");

            if (hfMintuesEntryInfoDocumentID.Value == "0")
            {
                btnDocRemove.Visible = true;
            }
            else
            {
                btnDocRemove.Visible = false;

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
            HyperLink2.NavigateUrl = "../UploadImg/"+AdsFile;
            HyperLink2.Text = "Uploaded Successfully";
        }
        else
        {
            HyperLink2.NavigateUrl = "";
            HyperLink2.Text = "No File Uploaded";
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
            using (DataTable dt = AMAsterDal.GetDDLComDivision(ddlCompany.SelectedValue))
        {
            ddlDivision.DataSource = dt;
            ddlDivision.DataValueField = "Value";
            ddlDivision.DataTextField = "TextField";
            ddlDivision.DataBind();
        }


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

             ddlDivision.Items.Clear();
             ddlDepartment.Items.Clear();
             rbRoutingPath.Items.Clear();
         }
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
                


                DataRow dataRow = null;

                for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)gv_EmpListSearch.Rows[i].Cells[0].FindControl("chkSelect");
                    HiddenField hfEmpInfoId = ((HiddenField)gv_EmpListSearch.Rows[i].FindControl("hfEmpInfoId"));
                    Label lbl_EmpMasterCode = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpMasterCode");
                    Label lbl_EmpName = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_EmpName");
                    Label lbl_Designation = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Designation");
                  
                    if (chkSelect.Checked)
                    {
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {



                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] = hfEmpInfoId.Value;

                            dataRow["EmpMasterCode"] = lbl_EmpMasterCode.Text;
                            dataRow["EmpName"] = lbl_EmpName.Text;
                            dataRow["Designation"] = lbl_Designation.Text;
                      



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
                 

                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = ShfEmpInfoId.Value;

                    dataRow["EmpMasterCode"] = Slbl_EmpMasterCode.Text;
                    dataRow["EmpName"] = Slbl_EmpName.Text;
                    dataRow["Designation"] = Slbl_Designation.Text;
                  



                    aDataTable.Rows.Add(dataRow);
                }
                ViewState["gv_Details_List"] = aDataTable;
                gv_Details_Save.DataSource = aDataTable;
                gv_Details_Save.DataBind();
            }
            else
            {
                AlertMessageBoxShow("Already Exist !!!");
            }
        }
        
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
            jobCreationInfos = AMAsterDal.GetEMpInfoFromRoutingPath(grade);
            if (jobCreationInfos.Rows.Count > 0)
            {

                gv_Details_Save.DataSource = jobCreationInfos;
                gv_Details_Save.DataBind();



            }
            else
            {
                gv_Details_Save.DataSource = null;
                gv_Details_Save.DataBind();
               

            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#exampleModal').modal('hide')", true); 
           
        }


       
         
    }

    public bool Validation()
    {
        return true;
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo();
    }

    private void SaveUpdateInfo()
    {
        if (Validation() == true)
        {
            List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();


            string DocNote = "";
            for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
            {
                HiddenField hfDocumentLink = (HiddenField) gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                Label lbl_DocumentNote = (Label) gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");


                MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();

                DocA.DocumentLink = hfDocumentLink.Value.ToString();
                DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                DocNote = DocNote + lbl_DocumentNote.Text.Trim() +" ";

                DocList.Add(DocA);
            }


            List<MiscellaneousInfoRoutingPathDAO> RoutingPath = new List<MiscellaneousInfoRoutingPathDAO>();

            for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
            {
                CheckBox chkMimimumCount = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkMimimumCount");
                DropDownList ddlSequenceList = (DropDownList) gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


                HiddenField ShfEmpInfoId = (HiddenField) gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId");


                CheckBox chkIsEdit = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkIsEdit");
                CheckBox chkEmail = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkEmail");
                CheckBox chkSMS = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkSMS");


                MiscellaneousInfoRoutingPathDAO Routing = new MiscellaneousInfoRoutingPathDAO();


                Routing.IsMinimumApprovalPerson = chkMimimumCount.Checked;
                Routing.Seq_No = Convert.ToInt32(ddlSequenceList.SelectedValue);
                Routing.EmpInfoId = Convert.ToInt32(ShfEmpInfoId.Value.Trim());

                Routing.CanEdit = chkIsEdit.Checked;
                Routing.IsEmailNotification = chkEmail.Checked;
                Routing.IsSMSNotification = chkSMS.Checked;


                RoutingPath.Add(Routing);
            }


            MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

            aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

            aMaster.Title = txtTitle.Text.Trim();
            aMaster.Purpose = txtPurpose.Text.Trim();
            aMaster.KeySearch = DocNote;

            bool result = false;
            if (RoutingPath.Count > 0)
            {
                int pk = AMAsterDal.SaveMaster(aMaster, Session["UserId"].ToString());
                if (pk > 0)
                {
                    result = AMAsterDal.SaveRoutingPathDetails(RoutingPath, pk);
                    AMAsterDal.SaveDocumentDetails(DocList, pk);
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
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo();
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {



        bool status = AMAsterDal.DeleteById(id_mastetID.Value);

        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='MiscellaneousInformationView.aspx';",
              true);
        }
    }

    protected void Button2_OnClick(object sender, EventArgs e)
    {
       
    }

    protected void Button1_OnClick(object sender, EventArgs e)
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

                        DocA.DocumentLink = hfDocumentLink.Value.ToString();
                        DocA.FileName = hfFileName.Value.ToString();
                        DocA.DocumentNote = lbl_DocumentNote.Text.Trim();


                        DocList.Add(DocA);
                    }

                    AMAsterDal.SaveDocumentDetails(DocList, pk);
                     
                }
                


                DataTable dtempdata =
                    AMAsterDal.GetEmpRoutingPath(pk.ToString(), hfSeqNo.Value);

                if (dtempdata.Rows.Count>0)
                {

                    DataTable dtIsApptove =
                      AMAsterDal.GetCheckMinimumApproval(pk.ToString());
                    bool isAppPerson = false;

                    if (dtIsApptove.Rows.Count > 0)
                    {
                        isAppPerson = Convert.ToBoolean(dtIsApptove.Rows[0]["IsMinimumApprovalPerson"].ToString());
                    }
                    bool status = false;
                    if (isAppPerson)
                    {
                        MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                        aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());

                        aMaster.Isapproved = false;
                        aMaster.ActionStatus = "Verified";

                        status = AMAsterDal.UpdateApprovalMasterById(aMaster);
                    }
                    else
                    {
                        MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                        aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());

                        aMaster.Isapproved = false;
                        aMaster.ActionStatus = "Verified";

                        status = AMAsterDal.UpdateApprovalMasterforNotIsMinApprovalPersonById(aMaster);
                        
                    }

                   

                    if (status)
                    {
                        MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                        {



                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApprovedDate = DateTime.Now;
                            appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                            appLogDao.MiscellaneousInfoId = pk;

                            appLogDao.Comments = commentsTextBox.Text.Trim();


                        }
                        int iddddd = AMAsterDal.SavAppLog(appLogDao);

                        Session["empAppNoti"] = "";
                       Session["empAppNoti"]= appLogDao.ForEmpInfoId;
                    }
                    DataTable dtMaster = AMAsterDal.GetMasterDataById(pk.ToString());
                    if (dtMaster.Rows.Count > 0)
                    {



                      



                        hfRefMinAppCountCheck.Value = dtMaster.Rows[0]["RefMinAppCountCheck"].ToString();

                        if (hfRefMinAppCount.Value == hfRefMinAppCountCheck.Value)
                        {
                            MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                            aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                          

                            aMaster.Isapproved = true;
                            aMaster.ActionStatus = "Approved";
                            aMaster.RefEmpId = 0;
                              AMAsterDal.FinalUpdateApprovalMasterById(aMaster);


                              MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                              {



                                  appLogDao.ActionStatus = "Approved";
                                  appLogDao.ApprovedDate = DateTime.Now;
                                  appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                  appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                  appLogDao.ForEmpInfoId = Convert.ToInt32(0);
                                  appLogDao.MiscellaneousInfoId = pk;

                                  appLogDao.Comments = commentsTextBox.Text.Trim();


                              }
                              int iddddd = AMAsterDal.SavAppLog(appLogDao);


                              EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();

                              DataTable dtLoop = AMAsterDal.GetEmpAllApprovalInfo(pk.ToString());
                              DataTable dtEntryBy = AMAsterDal.GetEmpEntryBy(pk.ToString());

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
//                                  for (int i = 0; i < dtLoop.Rows.Count; i++)
//                                  {


//                                      SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), " Miscellaneous minutes Meeting approved ", @" <br/> <br/> Dear Sir, <br/><br/>
//A Miscellaneous minutes Meeting  has been approved. <br/><br/><br/>Final Approved By:<br/>" + finEmpName + @"<br/>" + finEmpdes + @"
//
//
//<br/><br/>
//To login into the system please click the below link.<br/>  http://182.160.103.234:8088/
//" + "<br/><br/>Thank You.  ");

//                                  }

                                  SenMailForApprved(Convert.ToInt32(dtEntryBy.Rows[0]["EmpInfoId"].ToString()), lblTitle.Text.Trim(), @" <br/> <br/> Dear Sir, <br/> 
Your submitted Documents has been approved. 
To login into the system please click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/> Thank You.  ");
                              }
                        }
                        else
                        {

                            SenMailForApprved(Convert.ToInt32(Session["empAppNoti"].ToString()),
                                                             lblTitle.Text.Trim(), @"  <br/> Dear Sir, <br/> 
Document is submitted for your Recommendation/Approval in the system.
To login, click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.  ");
                        }



                      
                       
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Operation Successful...');window.location ='MiscellaneousInformationApprovalList.aspx';",
                             true);
                }
                else
                {

                    DataTable dtempdataFinApp =
               AMAsterDal.GetEmpRoutingPath2(pk.ToString(), hfSeqNo.Value);
                    DataTable dtIsApptove =
                   AMAsterDal.GetCheckMinimumApproval(pk.ToString());
                    bool isAppPerson = false;

                    if (dtIsApptove.Rows.Count > 0)
                    {
                        isAppPerson = Convert.ToBoolean(dtIsApptove.Rows[0]["IsMinimumApprovalPerson"].ToString());
                    }
                     if (dtempdataFinApp.Rows.Count>0)
                    {
                        if (isAppPerson)
                        {
                            MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                            aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                            aMaster.RefEmpId = Convert.ToInt32(0);
                            aMaster.RefSeqNo = Convert.ToInt32(dtempdataFinApp.Rows[0]["Seq_No"].ToString());

                            aMaster.Isapproved = false;
                            aMaster.ActionStatus = "Verified";

                            AMAsterDal.UpdateApprovalMasterById(aMaster); 
                        }
                        else
                        {
                            MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                            aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                            aMaster.RefEmpId = Convert.ToInt32(0.ToString());
                            aMaster.RefSeqNo = Convert.ToInt32(dtempdataFinApp.Rows[0]["Seq_No"].ToString());

                            aMaster.Isapproved = false;
                            aMaster.ActionStatus = "Verified";
                           AMAsterDal.UpdateApprovalMasterforNotIsMinApprovalPersonById(aMaster);
                        }

                        DataTable dtMaster = AMAsterDal.GetMasterDataById(pk.ToString());
                        if (dtMaster.Rows.Count > 0)
                        {
                            hfRefMinAppCountCheck.Value = dtMaster.Rows[0]["RefMinAppCountCheck"].ToString();

                            if (hfRefMinAppCount.Value == hfRefMinAppCountCheck.Value)
                            {
                                MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                                aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);



                                aMaster.RefEmpId = 0;
                                aMaster.Isapproved = true;
                                aMaster.ActionStatus = "Approved";

                                AMAsterDal.FinalUpdateApprovalMasterById(aMaster);


                                MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                                {



                                    appLogDao.ActionStatus = "Approved";
                                    appLogDao.ApprovedDate = DateTime.Now;
                                    appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                    appLogDao.ForEmpInfoId = Convert.ToInt32(0);
                                    appLogDao.MiscellaneousInfoId = pk;

                                    appLogDao.Comments = commentsTextBox.Text.Trim();


                                }
                                int iddddd = AMAsterDal.SavAppLog(appLogDao);


                                EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();

                                DataTable dtLoop = AMAsterDal.GetEmpAllApprovalInfo(pk.ToString());
                                DataTable dtEntryBy = AMAsterDal.GetEmpEntryBy(pk.ToString());
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
//                                    for (int i = 0; i < dtLoop.Rows.Count; i++)
//                                    {


//                                        SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), "  Miscellaneous minutes Meeting  Approved ", @"  <br/> Dear Sir, <br/>
//A Miscellaneous minutes Meeting  has been approved. <br/><br/><br/>Final Approved By:<br/>" + finEmpName + @"<br/>" + finEmpdes + @"
//
//
//<br/><br/>
// please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
//");

//                                    }

                                    SenMailForApprved(Convert.ToInt32(dtEntryBy.Rows[0]["EmpInfoId"].ToString()), "  Document Approval System (DAS) ", @" <br/> <br/> Dear Sir, <br/> 
Your submitted Documents has been approved. 
To login into the system please click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.  ");
                                }

                               
                            }



                        }
                    }

                   

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Operation Successful...');window.location ='MiscellaneousInformationApprovalList.aspx';",
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
    protected void Button2a_OnClick(object sender, EventArgs e)
    {
       
    }

    protected void btnReject_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void btnReturn_OnClick(object sender, EventArgs e)
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

                        DocA.DocumentLink = hfDocumentLink.Value.ToString();
                        DocA.FileName = hfFileName.Value.ToString();
                        DocA.DocumentNote = lbl_DocumentNote.Text.Trim();


                        DocList.Add(DocA);
                    }

                    AMAsterDal.SaveDocumentDetails(DocList, pk);

                }



           

                //if (dtempdata.Rows.Count > 0)
                //{

                //    DataTable dtIsApptove =
                // AMAsterDal.GetCheckMinimumApproval(pk.ToString());
                //    bool isAppPerson = false;

                //    if (dtIsApptove.Rows.Count > 0)
                //    {
                //        isAppPerson = Convert.ToBoolean(dtIsApptove.Rows[0]["IsMinimumApprovalPerson"].ToString());
                //    }
                //    bool status = false;
                //    if (isAppPerson)
                //    {
                //        MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                //        aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                //        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //        ;
                //        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());
                //        ;

                //        aMaster.Isapproved = false;
                //        aMaster.ActionStatus = "Return";

                //        status = AMAsterDal.UpdateApprovalMasterReturrnById(aMaster);
                //    }
                //    else
                //    {
                //        MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                //        aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                //        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //        ;
                //        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());
                //        ;

                //        aMaster.Isapproved = false;
                //        aMaster.ActionStatus = "Return";

                //        status = AMAsterDal.UpdateApprovalMasterforNotIsMinApprovalPersonById(aMaster);
                //    }

                //    if (status)
                //    {
                //        MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                //        {



                //            appLogDao.ActionStatus = "Return";
                //            appLogDao.ApprovedDate = DateTime.Now;
                //            appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                //            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                //            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //            appLogDao.MiscellaneousInfoId = pk;

                //            appLogDao.Comments = commentsTextBox.Text.Trim();


                //        }
                //        int iddddd = AMAsterDal.SavAppLog(appLogDao);
                //    }



                //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                //             "alert",
                //             "alert('Operation Successful...');window.location ='MiscellaneousInformationApprovalList.aspx';",
                //             true);
                //}
                //else
                {
                    MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                    aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

                    aMaster.ActionStatus = "Returned";
                    aMaster.RefEmpId = 0;
                    aMaster.RefSeqNo = 0;
                    aMaster.RefMinAppCount = 0;

                 bool   status = AMAsterDal.UpdateApprovalMasterById(aMaster);




                 MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                 {



                     appLogDao.ActionStatus = "Returned";
                     appLogDao.ApprovedDate = DateTime.Now;
                     appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                     appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                     appLogDao.ForEmpInfoId = 0;
                     appLogDao.MiscellaneousInfoId = pk;

                     appLogDao.Comments = commentsTextBox.Text.Trim();


                 }
                 int iddddd = AMAsterDal.SavAppLog(appLogDao);

                 if (status)
                    {
                        DataTable dtEntryBy = AMAsterDal.GetEmpEntryBy(pk.ToString());

                        SenMailForApprved(Convert.ToInt32(dtEntryBy.Rows[0]["EmpInfoId"].ToString()), lblTitle.Text.Trim(), @" <br/> <br/> Dear Sir, <br/> 
Document is submitted for your Recommendation/Approval in the system.
To login, click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.   ");

                        DataTable dtLoop =
                      AMAsterDal.GetEmpRoutingPathRreturn(pk.ToString(), hfSeqNo.Value);


                        for (int i = 0; i < dtLoop.Rows.Count; i++)
                        {


                            SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), lblTitle.Text.Trim(), @" <br/> <br/> Dear Sir, <br/> 
                        Document is submitted for your Recommendation/Approval in the system.
To login, click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You. ");

                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                 "alert",
                                 "alert('Operation Successful...');window.location ='MiscellaneousInformationApprovalList.aspx';",
                                 true);
                    }
                }




//                SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
//                An Increment is waiting for your approval. <br/><br/>
//                 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
//                ");


            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}