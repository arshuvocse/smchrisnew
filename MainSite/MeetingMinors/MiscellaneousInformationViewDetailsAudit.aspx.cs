using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MiscellaneousInformationViewDetailsAudit : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
         
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();


            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

               id_mastetID.Value=  (Request.QueryString["MID"]);


               DataTable dtMaster = new DataTable();
               if (Request.QueryString["Status"] == "Edit")
               {
                   dtMaster = AMAsterDal.GetMasterDataByIdAuditEdit(id_mastetID.Value);

               }
               else
               {
                   dtMaster = AMAsterDal.GetInitialByIdAudit(id_mastetID.Value);
               }

        
               if (dtMaster.Rows.Count > 0)
                {

                    ddlCompany.SelectedValue = dtMaster.Rows[0]["CompanyId"].ToString();
                    lblCompany.Text = ddlCompany.SelectedItem.Text;
                    ddlCompany_OnSelectedIndexChanged(null, null);
                    txtTitle.Text = dtMaster.Rows[0]["Title"].ToString();
                    lblTitle.Text = dtMaster.Rows[0]["Title"].ToString();
                    txtPurpose.Text = dtMaster.Rows[0]["Purpose"].ToString();
                    lblPurpose.Text = dtMaster.Rows[0]["Purpose"].ToString();
                   
                }

               DataTable dtDoc = AMAsterDal.GetDocDataById(id_mastetID.Value);
               if (dtDoc.Rows.Count > 0)
               {
                   ViewState["DocGrid_List"] = dtDoc;
                   gv_DocumentUpload.DataSource = dtDoc;
                   gv_DocumentUpload.DataBind();
               }


               //DataTable dtDetailos = AMAsterDal.GetDelsDataById(id_mastetID.Value);
               // if (dtDetailos.Rows.Count > 0)
               // {
               //     ViewState["gv_Details_List"] = dtDetailos;
               //     gv_Details_Save.DataSource = dtDetailos;
               //     gv_Details_Save.DataBind();


               // }

               DataTable dtApplogList = AMAsterDal.GetAppLogCommByJobId(id_mastetID.Value);
               if (dtApplogList.Rows.Count > 0)
               {

                   gv_ApprovalList.DataSource = dtApplogList;
                   gv_ApprovalList.DataBind();


               }
             
               
            }
        }
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

            Response.Redirect("BoardMeetingAuditTrail.aspx");
        }


    }
    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BoardMeetingAuditTrail.aspx");
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


                drCurrentRow["DocumentLink"] = "../UploadImg/" + hfDocFile.Value;
                   
               


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
          

            dr = dt.NewRow();


            dr["DocumentLink"] = "../UploadImg/" + hfDocFile.Value;
                   



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

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");
                    

                    if (i < dt.Rows.Count - 1)
                    {
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
}