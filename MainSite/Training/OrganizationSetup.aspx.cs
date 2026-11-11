using System.IO;
using System.Net;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;

public partial class Training_OrganizationSetup : System.Web.UI.Page
{

    private TrainingDAL _trainingDal = new TrainingDAL();
    private int mid = 0;
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_RegYear.Attributes.Add("readonly", "readonly");
            Session["EmpOption"] = "Employee";

            ButtonVisible();
            LoadOrgType();
            LoadInitialDDL();
           
           // AddNewRowToGrid();


            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {
                    submitButton.Text = "Update";
                    DataTable training = _trainingDal.GetOrganizationById(mid);
                    DataTable trainer = _trainingDal.GetLeadTrainerByOrgId(mid);
                    DataTable braunch = _trainingDal.GetBrunchByOrgId(mid);
                    txt_OrganizationName.Text = training.Rows[0][1].ToString();
                    ddlOrgType.SelectedValue = training.Rows[0][2].ToString();
                    txt_Address.Text = training.Rows[0][3].ToString();
                    bool isForeign = Convert.ToBoolean(training.Rows[0][4].ToString());
                    bool isLocal = Convert.ToBoolean(training.Rows[0][5].ToString());
                    bool isInhouse = Convert.ToBoolean(training.Rows[0][6].ToString());
                    string seletedVal = isForeign == true ? "Foreign" : isLocal == true ? "Local" : "Inouse";
                    ddlCompany.SelectedValue = training.Rows[0]["CompanyId"].ToString();

                    bool HasTin = false;
                    bool HasVat = false;
                    bool HasTradeLicense = false;
                    bool HasBankSolv = false;
                    string others = "";

                    
                    if (isLocal == true)
                    {
                        rad_OrganizationType.Items[1].Selected = true;
                    }
                    //if (isInhouse == true)
                    //{
                    //    rad_OrganizationType.Items[0].Selected = true;
                    //}
                    if (isForeign == true)
                    {
                        rad_OrganizationType.Items[0].Selected = true;


                    }
                    rad_OrganizationType.SelectedValue = seletedVal;

                    rad_OrganizationType_OnSelectedIndexChanged(rad_OrganizationType, (EventArgs) e);
                    ddlCountry.SelectedValue = isForeign == false ? "" : training.Rows[0]["CountryID"].ToString();
                    rad_vendorAudit.SelectedValue = training.Rows[0]["VendorAudit"].ToString()==""? "false":"true";
                    txt_remarks.Text = training.Rows[0]["Remarks"].ToString();
                    txt_OrgProfile.Text = training.Rows[0]["OrgProfile"].ToString();



                    txt_Clients.Text = training.Rows[0]["Clients"].ToString();
                    rad_clientsRecom.SelectedValue = training.Rows[0]["ClientsRecommendation"].ToString()==""? "false":"true";
                    rad_logistic.SelectedValue = training.Rows[0]["LogisticsFacility"].ToString() == "" ? "false" : "true"; 
                    txt_affiliation.Text = training.Rows[0][13].ToString();
                    txt_RegYear.Text = Convert.ToDateTime(training.Rows[0]["RegistrationYear"].ToString()).ToString("dd-MMM-yyyy");
                    txt_contactPerson.Text = training.Rows[0]["ContactPerson"].ToString();
                    txt_cellNo.Text = training.Rows[0]["ContactPersonCell"].ToString();
                    txt_Email.Text = training.Rows[0]["ContactPersonEmail"].ToString();
                   

                    HasTin = Convert.ToBoolean(training.Rows[0]["HasTin"].ToString());
                    HasVat = Convert.ToBoolean(training.Rows[0]["HasVat"].ToString());
                    HasTradeLicense = Convert.ToBoolean(training.Rows[0]["HasTradeLicense"].ToString());
                    HasBankSolv = Convert.ToBoolean(training.Rows[0]["HasBankSolv"].ToString());
                    others = training.Rows[0]["Others"].ToString();
                    if (HasTin == true)
                    {
                        chkDocument.Items[0].Selected = true;
                    }

                    if (HasVat == true)
                    {
                        chkDocument.Items[1].Selected = true;
                    }

                    if (HasTradeLicense == true)
                    {
                        chkDocument.Items[2].Selected = true;
                    }

                    if (HasBankSolv == true)
                    {
                        chkDocument.Items[3].Selected = true;
                    }

                    if (others != "" )
                    {
                        chkDocument.Items[4].Selected = true;
                        div_other.Visible = true;
                        txt_Others.Visible = true;
                        txt_Others.Text = others;
                    }
                    
                    ViewState["BranchTable"] = braunch;
                    ViewState["TrainerTable"] = trainer;
                    gv_LeadTrainer.DataSource = trainer;
                    gv_LeadTrainer.DataBind();
                    gv_OfficeBranchs.DataSource = braunch;
                    gv_OfficeBranchs.DataBind();

                    if (braunch.Rows.Count > 0)
                    {
                        for (int i = 0; i < gv_OfficeBranchs.Rows.Count; i++)
                        {
                            DropDownList txt_BranchCountry =
                                (DropDownList) gv_OfficeBranchs.Rows[i].FindControl("ddl_BrunchCountry");


                            txt_BranchCountry.SelectedValue = braunch.Rows[i]["CountryID"].ToString();

                        }
                    }
                    else
                    {
                        SetInitialRow();
                        SetInitialRowFileUpload();
                    }
                }
            }
            else
            {
                SetInitialRow();
                SetInitialRowFileUpload();
            }
        }
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
            Response.Redirect("OrganizationList.aspx");
        }

    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrganizationList.aspx");
    }

    protected void LoadOrgType()
    {
        DataTable dt = _trainingDal.GetOrganizationType();
        ddlOrgType.DataSource = dt;
        ddlOrgType.DataValueField = "Value";
        ddlOrgType.DataTextField = "TextField";
        ddlOrgType.DataBind();
           
           
    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
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
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        if (mid > 0)
        {
          bool result =   UpdateEntry(mid);

          if (result == true)
          {
              ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='OrganizationList.aspx';",
                    true);
            

              ResetInput();
             // var timer = new DispatcherTimer();
           // Response.Redirect("OrganizationList.aspx");

          }
         
        }
        else
        {
            if (Validation() == true)
            {
               bool result= SaveNewEntry();

               if (result == true)
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='OrganizationList.aspx';",
                   true);
                   ResetInput();
               }
              
            }
          
        }
       
    }


    private void ResetInput(){
        txt_OrganizationName.Text = null;
        txt_Address.Text = null;
        txt_OrgProfile.Text = null;
       // txt_VendorAudit.Text = null;
        txt_remarks.Text = null;
        txt_RegYear.Text = null;
        txt_Clients.Text = null;
        //txt_ClientRecom.Text = null;
        //txt_Logistics.Text = null;
        txt_affiliation.Text = null;
        txt_Others.Text = null;
        hdpk.Value = null;
        txt_cellNo.Text = null;
        txt_Email.Text = null;
        txt_contactPerson.Text = null;

        ddlOrgType.SelectedValue = null;
        ddlOrgType.SelectedValue = null;
       
        LoadOrgType();
        chkDocument.SelectedValue = null;

        SetInitialRow();
    }

    public bool UpdateEntry(int id){

        bool final = false;
        if (Validation() == true)
        {
          
            TrainingOrgInfo aOrg = new TrainingOrgInfo();

            aOrg.HasTin = false;
            aOrg.HasVat = false;
            aOrg.HasTradeLicense = false;
            aOrg.HasBankSolv = false;
            aOrg.Others = "";

            List<string> selectedValues = chkDocument.Items.Cast<ListItem>()
             .Where(li => li.Selected)
             .Select(li => li.Value)
             .ToList();


            foreach (string a in selectedValues)
            {

                if (a == "Tin")
                {
                    aOrg.HasTin = true;
                }

                if (a == "Vat")
                {
                    aOrg.HasVat = true;
                }
                if (a == "Trade")
                {
                    aOrg.HasTradeLicense = true;
                }
                if (a == "Bank")
                {
                    aOrg.HasBankSolv = true;
                }
                if (a == "Other")
                {
                    aOrg.Others = txt_Others.Text.Trim();
                }
            }

            aOrg.OrgTypeId = Convert.ToInt32(ddlOrgType.SelectedValue);
            aOrg.TrainingOrgName = txt_OrganizationName.Text.Trim();
            string origin = rad_OrganizationType.SelectedValue;

            if (origin == "Inhouse")
            {
                aOrg.IsInHouse = true;
                aOrg.IsLocal = false;
                aOrg.IsForeign = false;
            }
            if (origin == "Local")
            {
                aOrg.IsLocal = true;
                aOrg.IsForeign = false;
                aOrg.IsInHouse = false;
            }
            if (origin == "Foreign")
            {
                aOrg.IsForeign = true;
                aOrg.IsInHouse = false;
                aOrg.IsLocal = false;
                aOrg.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            aOrg.OrgAddress = txt_Address.Text == null ? "" : txt_Address.Text.Trim();
            aOrg.OrgProfile = txt_OrgProfile.Text.Trim();
            aOrg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            //aOrg.VendorAudit = txt_VendorAudit.Text.Trim();
            aOrg.VendorAudit = rad_vendorAudit.SelectedValue == ""
                ? false
                : Convert.ToBoolean(rad_vendorAudit.SelectedValue);
            aOrg.Remarks = txt_remarks.Text.Trim();
            aOrg.RegistrationYear = Convert.ToDateTime(txt_RegYear.Text.Trim());
            aOrg.Clients = txt_Clients.Text.Trim();
            aOrg.ClientsRecommendation = rad_clientsRecom.SelectedValue == ""
                ? false
                : Convert.ToBoolean(rad_clientsRecom.SelectedValue);
            aOrg.LogisticsFacility = rad_logistic.SelectedValue == ""
                ? false
                : Convert.ToBoolean(rad_logistic.SelectedValue);
            aOrg.Affiliation = txt_affiliation.Text.Trim();
            aOrg.ContactPerson = txt_contactPerson.Text.Trim();
            aOrg.ContactPersonCell = txt_cellNo.Text.Trim();
            aOrg.ContactPersonEmail = txt_Email.Text.Trim();
            aOrg.TrainingOrgId = id;


            final = _trainingDal.UpdateTrainingOrganization(aOrg, Convert.ToInt32(Session["UserId"].ToString()));


            if (final == true)
            {

                List<TrainerInfo> aList = new List<TrainerInfo>();
                for (int i = 0; i < gv_LeadTrainer.Rows.Count; i++)
                {
                    TextBox txt_trainer = (TextBox)gv_LeadTrainer.Rows[i].FindControl("txt_Trainer");

                    TrainerInfo trainer = new TrainerInfo();
                    trainer.TrainerName = txt_trainer.Text.Trim();
                    trainer.TrainingOrgId = id;
                    aList.Add(trainer);
                }
                final = _trainingDal.UpdateTrainnerInfo(aList);

            }
            if (final == true)
            {
                List<OfficeBranch> aOfficeList = new List<OfficeBranch>();

                for (int i = 0; i < gv_OfficeBranchs.Rows.Count; i++)
                {
                    TextBox branch = (TextBox)gv_OfficeBranchs.Rows[i].FindControl("txt_OfficeBranch");

                    TextBox txt_BranchAddress = (TextBox)gv_OfficeBranchs.Rows[i].FindControl("txt_BranchAddress");
                    DropDownList txt_BranchCountry = (DropDownList)gv_OfficeBranchs.Rows[i].FindControl("ddl_BrunchCountry");
                    OfficeBranch trainer = new OfficeBranch();
                    trainer.BranchDetails = branch.Text.Trim();
                    trainer.BranchAddress = txt_BranchAddress.Text.Trim();
                    trainer.CountryID = txt_BranchCountry.SelectedValue == "" ? 0 : Convert.ToInt32(txt_BranchCountry.SelectedValue);
                    trainer.TrainingOrgId = aOrg.TrainingOrgId;
                    aOfficeList.Add(trainer);
                }
                final = _trainingDal.UpdateBranchInfo(aOfficeList);
                //final = result1;
            }

            
        }

        return final;

    }
    public bool SaveNewEntry() {

        bool final = false;

        TrainingOrgInfo aOrg = new TrainingOrgInfo();

        aOrg.HasTin = false;
        aOrg.HasVat = false;
        aOrg.HasTradeLicense = false;
        aOrg.HasBankSolv = false;
        aOrg.Others = "";

        List<string> selectedValues = chkDocument.Items.Cast<ListItem>()
         .Where(li => li.Selected)
         .Select(li => li.Value)
         .ToList();


        foreach (string a in selectedValues) {

            if (a == "Tin")
            {
                aOrg.HasTin = true;
            }

            if (a == "Vat")
            {
                aOrg.HasVat = true;
            }
            if (a == "Trade")
            {
                aOrg.HasTradeLicense = true;
            }
            if (a == "Bank")
            {
                aOrg.HasBankSolv = true;
            }
            if (a == "Other")
            {
                aOrg.Others = txt_Others.Text.Trim();
            }
        }

        aOrg.OrgTypeId = Convert.ToInt32(ddlOrgType.SelectedValue);
        aOrg.TrainingOrgName = txt_OrganizationName.Text.Trim();
        string origin = rad_OrganizationType.SelectedValue;

        if (origin == "Inhouse")
        {
            aOrg.IsInHouse = true;
            aOrg.IsLocal = false;
            aOrg.IsForeign = false;
        }
        if (origin == "Local")
        {
            aOrg.IsLocal = true;
            aOrg.IsForeign = false;
            aOrg.IsInHouse = false;
        }
        if (origin == "Foreign")
        {
            aOrg.IsForeign = true;
            aOrg.IsInHouse = false;
            aOrg.IsLocal = false;
            aOrg.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        }
        aOrg.OrgAddress = txt_Address.Text==null?"":txt_Address.Text.Trim();
        aOrg.OrgProfile = txt_OrgProfile.Text.Trim();
        aOrg.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        //aOrg.VendorAudit = txt_VendorAudit.Text.Trim();
        aOrg.VendorAudit = rad_vendorAudit.SelectedValue == ""
            ? false
            : Convert.ToBoolean(rad_vendorAudit.SelectedValue);
        aOrg.Remarks = txt_remarks.Text.Trim();
        aOrg.RegistrationYear = Convert.ToDateTime(txt_RegYear.Text.Trim());
        aOrg.Clients = txt_Clients.Text.Trim();
        aOrg.ClientsRecommendation = rad_clientsRecom.SelectedValue == ""
            ? false
            : Convert.ToBoolean(rad_clientsRecom.SelectedValue);
        aOrg.LogisticsFacility = rad_logistic.SelectedValue == ""
            ? false
            : Convert.ToBoolean(rad_logistic.SelectedValue);
        aOrg.Affiliation = txt_affiliation.Text.Trim();
        aOrg.ContactPerson = txt_contactPerson.Text.Trim();
        aOrg.ContactPersonCell = txt_cellNo.Text.Trim();
        aOrg.ContactPersonEmail = txt_Email.Text.Trim();




        int result = _trainingDal.SaveTrainingOrganization(aOrg, Convert.ToInt32(Session["UserId"].ToString()));

        if (result > 0)
        {

            List<TrainerInfo> aList = new List<TrainerInfo>();
            for (int i = 0; i < gv_LeadTrainer.Rows.Count; i++)
            {
                TextBox txt_trainer = (TextBox)gv_LeadTrainer.Rows[i].FindControl("txt_Trainer");

                TrainerInfo trainer = new TrainerInfo();
                trainer.TrainerName = txt_trainer.Text.Trim();
                trainer.TrainingOrgId = result;
                aList.Add(trainer);
            }


            List<OfficeBranch> aOfficeList = new List<OfficeBranch>();

            for (int i = 0; i < gv_OfficeBranchs.Rows.Count; i++)
            {
                TextBox branch = (TextBox)gv_OfficeBranchs.Rows[i].FindControl("txt_OfficeBranch");
              
                TextBox txt_BranchAddress = (TextBox)gv_OfficeBranchs.Rows[i].FindControl("txt_BranchAddress");
                DropDownList txt_BranchCountry = (DropDownList)gv_OfficeBranchs.Rows[i].FindControl("ddl_BrunchCountry");
                OfficeBranch trainer = new OfficeBranch();
                trainer.BranchDetails = branch.Text.Trim();
                trainer.BranchAddress = txt_BranchAddress.Text.Trim();
                trainer.CountryID = txt_BranchCountry.SelectedValue == "" ? 0 : Convert.ToInt32(txt_BranchCountry.SelectedValue);
                trainer.TrainingOrgId = result;
                aOfficeList.Add(trainer);
            }
            bool result1 = _trainingDal.SaveTrainer(aList);

            if (result1 == true)
            {
                result1 = _trainingDal.SaveBrunch(aOfficeList);
            }

            final = result1;

           

          
        }

        return final;


    }
    protected void gv_LeadTrainer_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["TrainerTable"];
            LinkButton lb = (LinkButton)e.Row.FindControl("lb_Remove");
        
        }
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("TrainerName", typeof(string)));
       

        dr = dt.NewRow();
        //dr["TrainerId"] = "";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["TrainerTable"] = dt;

        //Bind the Gridview   
        gv_LeadTrainer.DataSource = dt;
        gv_LeadTrainer.DataBind();

        // Branch Table

        DataTable dt2 = new DataTable();
        DataRow dr2 = null;


        dt2.Columns.Add(new DataColumn("BranchDetails", typeof(string)));
        dt2.Columns.Add(new DataColumn("BranchAddress", typeof(string)));
        dt2.Columns.Add(new DataColumn("CountryID", typeof(string)));

        dr2 = dt2.NewRow();
        //dr["TrainerId"] = "";
        dt2.Rows.Add(dr2);

        //Store the DataTable in ViewState for future reference   
        ViewState["BranchTable"] = dt2;

        //Bind the Gridview   
        gv_OfficeBranchs.DataSource = dt2;
        gv_OfficeBranchs.DataBind();



    }


    private void SetInitialRowFileUpload()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("TitleName", typeof(string)));
        dt.Columns.Add(new DataColumn("FileUpload1", typeof(FileUpload)));


        dr = dt.NewRow();
        //dr["TrainerId"] = "";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["FileUpload"] = dt;

        //Bind the Gridview   
        FileUploadGridView.DataSource = dt;
        FileUploadGridView.DataBind();

        // Branch Table

       



    }

    protected void AddNewRowToGrid()
    {
        if (ViewState["TrainerTable"] != null) {


            DataTable dtCurrentTable = (DataTable)ViewState["TrainerTable"];
            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();
            

           
            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["TrainerTable"] = dtCurrentTable;

            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
                TextBox txt_trainer = (TextBox)gv_LeadTrainer.Rows[i].FindControl("txt_Trainer");
                dtCurrentTable.Rows[i]["TrainerName"] = txt_trainer.Text.Trim();
            }

            gv_LeadTrainer.DataSource = dtCurrentTable;
            gv_LeadTrainer.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;


            dt.Columns.Add(new DataColumn("TrainerName", typeof(string)));


            dr = dt.NewRow();
            dr["TrainerName"] = "";
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["TrainerTable"] = dt;

            //Bind the Gridview   
            gv_LeadTrainer.DataSource = dt;
            gv_LeadTrainer.DataBind();

        }
    }


    protected void AddNewRowToGridFileUpload()
    {
        if (ViewState["FileUpload"] != null)
        {


            DataTable dtCurrentTable = (DataTable)ViewState["FileUpload"];
            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["FileUpload"] = dtCurrentTable;

            try
            {
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    TextBox txt_trainer = (TextBox)FileUploadGridView.Rows[i].FindControl("txt_Trainer");
                    FileUpload FileUpload1_Doc = (FileUpload)FileUploadGridView.Rows[i].FindControl("FileUpload1_Doc");
                    dtCurrentTable.Rows[i]["TitleName"] = txt_trainer.Text.Trim();

                    System.IO.Stream stream = FileUpload1_Doc.PostedFile.InputStream;

                    int length = FileUpload1_Doc.PostedFile.ContentLength;

                    byte[] data = new byte[length];

                    //Fill data
                    stream.Read(data, 0, length);
                    String extension = System.IO.Path.GetExtension(FileUpload1_Doc.PostedFile.FileName);
                    dtCurrentTable.Rows[i]["FileUpload1"] =

    System.IO.Path.GetDirectoryName(extension)

    ;
                }
            }
            catch (Exception)
            {

            }
           

            FileUploadGridView.DataSource = dtCurrentTable;
            FileUploadGridView.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;


            dt.Columns.Add(new DataColumn("TitleName", typeof(string)));
            dt.Columns.Add(new DataColumn("FileUpload1", typeof(FileUpload)));


            dr = dt.NewRow();
            dr["TitleName"] = "";
            dr["FileUpload1"] = "";
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["FileUpload"] = dt;

            //Bind the Gridview   
            FileUploadGridView.DataSource = dt;
            FileUploadGridView.DataBind();

        }
    }
    protected void btn_AddTrainner_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
 
    protected void btn_AddBranch_Click(object sender, EventArgs e)
    {
        AddRowToBranchGrid();
    }

    protected void AddRowToBranchGrid()
    {
        if (ViewState["BranchTable"] != null)
        {


            DataTable dtCurrentTable = (DataTable)ViewState["BranchTable"];
            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["BranchTable"] = dtCurrentTable;

            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
                TextBox txt_Branch = (TextBox)gv_OfficeBranchs.Rows[i].FindControl("txt_OfficeBranch");
                TextBox txt_BranchAddress = (TextBox)gv_OfficeBranchs.Rows[i].FindControl("txt_BranchAddress");
                DropDownList txt_BranchCountry = (DropDownList)gv_OfficeBranchs.Rows[i].FindControl("ddl_BrunchCountry");
                dtCurrentTable.Rows[i]["BranchDetails"] = txt_Branch.Text.Trim();
                dtCurrentTable.Rows[i]["BranchAddress"] = txt_BranchAddress.Text.Trim();
                dtCurrentTable.Rows[i]["CountryID"] = txt_BranchCountry.SelectedValue;
                

                 //DataTable dt = _trainingDal.GetCountry();
                 // txt_BranchCountry.DataSource = dt;
                 // txt_BranchCountry.DataValueField = "Value";
                 // txt_BranchCountry.DataTextField = "TextField"; 
             
                 // txt_BranchCountry.DataBind();
                
            }

          

            gv_OfficeBranchs.DataSource = dtCurrentTable;
            gv_OfficeBranchs.DataBind();
            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
                DropDownList txt_BranchCountry = (DropDownList)gv_OfficeBranchs.Rows[i].FindControl("ddl_BrunchCountry");
                txt_BranchCountry.SelectedValue = dtCurrentTable.Rows[i]["CountryID"].ToString();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;


            dt.Columns.Add(new DataColumn("BranchDetails", typeof(string)));
            dt.Columns.Add(new DataColumn("BranchAddress", typeof(string)));
            dt.Columns.Add(new DataColumn("CountryID", typeof(string)));


            dr = dt.NewRow();
            dr["BranchDetails"] = "";
            dr["BranchAddress"] = "";
            dr["CountryID"] = "";
            DataTable dtc = _trainingDal.GetCountry();
           

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["BranchTable"] = dt;

            //Bind the Gridview   
            gv_OfficeBranchs.DataSource = dt;
            gv_OfficeBranchs.DataBind();

        }
    }

    protected void lb_RemoveBranch_Click(object sender, EventArgs e)
    {

        if (ViewState["BranchTable"] != null && gv_OfficeBranchs.Rows.Count == 1)
        {
            TextBox txt_Branch = (TextBox)gv_OfficeBranchs.Rows[0].FindControl("txt_OfficeBranch");
            TextBox txt_BranchAddress = (TextBox)gv_OfficeBranchs.Rows[0].FindControl("txt_BranchAddress");
            DropDownList txt_BranchCountry = (DropDownList)gv_OfficeBranchs.Rows[0].FindControl("ddl_BrunchCountry");
            txt_Branch.Text = null;
            txt_BranchAddress.Text = null;
            txt_BranchCountry.SelectedValue = null;
        }
        if (ViewState["BranchTable"] != null && gv_OfficeBranchs.Rows.Count>1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["BranchTable"];

            dt.Rows.Remove(dt.Rows[rowID]);

            gv_OfficeBranchs.DataSource = dt;
            gv_OfficeBranchs.DataBind();
        }
    }

   
    protected void chkDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
         List<string> selectedValues = chkDocument.Items.Cast<ListItem>()
         .Where(li => li.Selected)
         .Select(li => li.Value)
         .ToList();

         div_other.Visible = false;
         txt_Others.Visible = true;
        foreach (string a in selectedValues) {

          
            if (a == "Other")
            {
               div_other.Visible = true;
            txt_Others.Visible = true;
            }
            else
            {
                div_other.Visible = false;
                txt_Others.Visible = true;
            }
           
        }
        
    }

    protected bool Validation()
    {
       bool isValid = true;
        if (string.IsNullOrEmpty(txt_OrganizationName.Text.Trim()))
        {
            aShowMessage.ShowMessageBox(aMessages.VOrgName, this);
            isValid =  false;
        }

        if (string.IsNullOrEmpty(ddlCompany.Text.Trim()) || ddlCompany.Text.Trim() =="-1")
        {
            aShowMessage.ShowMessageBox("Company Required !!", this);
            isValid = false;
        }
        if (string.IsNullOrEmpty(ddlOrgType.Text.Trim()) || ddlOrgType.Text.Trim() == "-1")
        {
            aShowMessage.ShowMessageBox("Organization Type Required !!", this);
            isValid = false;
        }
        if (ddlOrgType.SelectedValue == "" )
        {
            aShowMessage.ShowMessageBox(aMessages.VOrgType, this);
            isValid = false;
        }

        if (string.IsNullOrEmpty(txt_OrgProfile.Text.Trim()))
        {
            aShowMessage.ShowMessageBox("Organization Profile Required", this);
            isValid = false;
        }
        if (string.IsNullOrEmpty(txt_contactPerson.Text.Trim()))
        {
            aShowMessage.ShowMessageBox("Contact Person Required", this);
            isValid = false;
        }
        if (string.IsNullOrEmpty(txt_cellNo.Text.Trim()))
        {
            aShowMessage.ShowMessageBox("Contact Person Phone Required", this);
            isValid = false;
        }
        
     
       
        if (txt_RegYear.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Registration Year Required", this);
        }

        return isValid;
    }
    protected void lb_RemoveTrainer_Click(object sender, EventArgs e)
    {
        if ( ViewState["TrainerTable"] != null &&(gv_LeadTrainer.Rows.Count == 1))
        {
            TextBox txt_trainer = (TextBox)gv_LeadTrainer.Rows[0].FindControl("txt_Trainer");
            txt_trainer.Text = null;
        }
        if (ViewState["TrainerTable"] != null && gv_LeadTrainer.Rows.Count>1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["TrainerTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["TrainerTable"] = null;
            }
            else
            {
                ViewState["TrainerTable"] = dt;
            }


            gv_LeadTrainer.DataSource = dt;
            gv_LeadTrainer.DataBind();
           
        }
    }
    protected void addNewOrgType_Click(object sender, EventArgs e)
    {
        mpe_1.Show();
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        mpe_1.Hide();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (txt_orgType.Text == "")
        {

            aShowMessage.ShowMessageBox("Please Enter Organizaion Type", this);

            return;
        }
        else
        {

            bool result = _trainingDal.InsertOrganizationType((txt_orgType.Text.Trim()));
            if(result==true){
                  AlertMessageBoxShow("Operation Successful...");
                  txt_orgType.Text = "";
                 mpe_1.Hide();
                 LoadOrgType();
            }
            else{
                AlertMessageBoxShow("Operation Failed...");
            }
        }
    }

    protected void rad_OrganizationType_OnSelectedIndexChanged(object sender, EventArgs e)
    {

         string origin = rad_OrganizationType.SelectedValue;


        if (origin == "Foreign")
        {
            CountryDiv.Visible = true;

            DataTable dt = _trainingDal.GetCountry();
            ddlCountry.DataSource = dt;
            ddlCountry.DataValueField = "Value";
            ddlCountry.DataTextField = "TextField";
            ddlCountry.DataBind();
            ddlCountry.Enabled = true;
        }
        else
        {
            CountryDiv.Visible = true;
           
            DataTable dt = _trainingDal.GetCountry();
            ddlCountry.DataSource = dt;
            ddlCountry.DataValueField = "Value";
            ddlCountry.DataTextField = "TextField";
            ddlCountry.DataBind();
            ddlCountry.SelectedIndex = 13;
            ddlCountry.Enabled = false;


        }

       
    }

    protected void gv_OfficeBranchs_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < gv_OfficeBranchs.Rows.Count; i++)
        {
            DropDownList txt_BranchCountry = (DropDownList)gv_OfficeBranchs.Rows[i].FindControl("ddl_BrunchCountry");

            DataTable dt = _trainingDal.GetCountry();
            txt_BranchCountry.DataSource = dt;
            txt_BranchCountry.DataValueField = "Value";
            txt_BranchCountry.DataTextField = "TextField";
            txt_BranchCountry.DataBind();
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        if (mid > 0)
        {
            bool result = UpdateEntry(mid);

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='OrganizationList.aspx';",
                   true);


                ResetInput();
                // var timer = new DispatcherTimer();
                // Response.Redirect("OrganizationList.aspx");

            }

        }
        else
        {
            if (Validation() == true)
            {
                bool result = SaveNewEntry();

                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='OrganizationList.aspx';",
                   true);
                    ResetInput();
                }

            }

        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;


        //HiddenField hdpk = (HiddenField)gv_OrgList.Rows[rowID].FindControl("hdpk");

        bool result = _trainingDal.DeleteOrgInfo(Convert.ToInt32(hdpk.Value), Convert.ToInt32(Session["UserId"].ToString()));

        if (result)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='OrganizationList.aspx';",
                   true);
            //LoadOrgList();
        }
        else
        {

            AlertMessageBoxShow("Operation Failed...");

        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void FileUploadGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void lb_FileUploadGridView_Click(object sender, EventArgs e)
    {
        if (ViewState["FileUpload"] != null && gv_OfficeBranchs.Rows.Count == 1)
        {
            TextBox txt_Branch = (TextBox)gv_OfficeBranchs.Rows[0].FindControl("TitleName");
            FileUpload FileUpload1 = (FileUpload)gv_OfficeBranchs.Rows[0].FindControl("FileUpload1");
          
            txt_Branch.Text = null;
          // FileUpload1.HasFile = false;
          
        }
        if (ViewState["FileUpload"] != null && gv_OfficeBranchs.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["FileUpload"];

            dt.Rows.Remove(dt.Rows[rowID]);

            FileUploadGridView.DataSource = dt;
            FileUploadGridView.DataBind();
        }
    }

    protected void btn_FileUploadGridView_Click(object sender, EventArgs e)
    {
        AddNewRowToGridFileUpload();
    }
}