using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Increment_DAL;
using DAO.HRIS_DAO;

public partial class Report_Pages_MemoPrintJobCirculation : System.Web.UI.Page
{
    MemoPrintJobCirculationDAL aDAL = new MemoPrintJobCirculationDAL();
    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {




            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                
          
                mid = int.Parse(Request.QueryString["mid"]);
                 IncrementIdHiddenField.Value = mid.ToString();
                if (mid > 0)
                {
                    DataTable dtdata = new DataTable();
                    dtdata = aDAL.LoadMemoJobCreationByMId(mid);
                    if (dtdata.Rows.Count > 0)
                    {
                        MasterIdHiddenField.Value = dtdata.Rows[0]["MemoJobCreationId"].ToString();

                        lblLabelInfo.Text = dtdata.Rows[0]["HeaderTitle"].ToString();
                        lblDate.Text = Convert.ToDateTime(dtdata.Rows[0]["HeaderDate"]).ToString("MMMM dd, yyyy");


                        txtFirstBody.Text = Server.HtmlDecode(dtdata.Rows[0]["FirstTitle"].ToString());

                        txtSecendBody.Text = Server.HtmlDecode(dtdata.Rows[0]["SecondTitle"].ToString());
                        txtPositiontitle.Text = dtdata.Rows[0]["Positiontitle"].ToString();

                        txtWhatWeOffer.Text = Server.HtmlDecode(dtdata.Rows[0]["WeOffer"].ToString());

                        txtInstraction.Text = Server.HtmlDecode(dtdata.Rows[0]["APPLYINSTRACTIONS"].ToString()); ;
                        txtDesignationEach.Text = Server.HtmlDecode(dtdata.Rows[0]["NameDesignation"].ToString()); ;



                        DataTable dtkeyres = aDAL.UpLoadKeyResponseById(MasterIdHiddenField.Value);
                        if (dtkeyres.Rows.Count > 0)
                        {
                            KeyResponGridView.DataSource = dtkeyres;
                            KeyResponGridView.DataBind();
                        }


                        DataTable OherRequirements = aDAL.UpGetOtherRequirementsDetailId(MasterIdHiddenField.Value);

                        if (OherRequirements.Rows.Count > 0)
                        {
                            OtherRequirementsGridView.DataSource = OherRequirements;
                            OtherRequirementsGridView.DataBind();
                        }
                      
                        editButton.Visible = true;
                    }

                    else
                    {
                       
                        lblDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
              
                       
                      
                        submitButton.Visible = true;

                        LoadTextFirstTime();
                    }

                   
                }
            }
            else
            {
                Response.Redirect("/RecruitmentManagement_UI/JobCreationView.aspx");
            }


        }
    }

    private void LoadTextFirstTime()
    {
      
        

         DataTable dtdatad = aDAL.GetJobCreationInformationById(mid.ToString());
         if (dtdatad.Rows.Count > 0)
         { 
             ComName.Value = dtdatad.Rows[0]["ShortName"].ToString();
         lblLabelInfo.Text = dtdatad.Rows[0]["JobCode"].ToString();
         HiddenFieldJobReqId.Value = dtdatad.Rows[0]["JobReqId"].ToString();
         txtPositiontitle.Text = dtdatad.Rows[0]["JobTitle"].ToString() + " , " + dtdatad.Rows[0]["DepartmentName"].ToString() + " (" + dtdatad.Rows[0]["Nos"].ToString() + ")";
        }

        txtWhatWeOffer.Text = Server.HtmlDecode(@"Selected candidate will be offered an attractive salary and benefit
                                                                         Packages which includes PF, Gratuity, Incentive, Profit share, 
                                                   Festival bonuses, Group life insurance, leave   encashment etc.
");

        txtInstraction.Text = @"Interested candidates may apply in confidence with a details cv with names and contact information of three reference (non-relative), a cover letter supporting the qualification of the candidate for the position and two copies of recent passport size color photographs to the recruitment.smcel@smc-bd.org or apply through www.smc-bd.org/index.php/job/. The deadline for submission of application is April 4, 2019.
";
        txtDesignationEach.Text = "";

        txtFirstBody.Text = ComName.Value +
                            " Enterprise Limited, a large, well-reputed organization involved in the sales, marketing, and production on of health, consumer and pharmaceutical products, is looking for a talented and aspiring individual for the following position. The position will be based at the Head Office, Banani Dhaka";

        txtSecendBody.Text = ComName.Value +
                             " Enterprise Limited has a vacancy for the folowing position for Head Office";

        DataTable dtkeyres = aDAL.LoadKeyResponseById(HiddenFieldJobReqId.Value.ToString());
        if (dtkeyres.Rows.Count > 0)
        {
            KeyResponGridView.DataSource = dtkeyres;
            KeyResponGridView.DataBind();
        }


        DataTable OherRequirements = aDAL.GetOtherRequirementsDetailId(HiddenFieldJobReqId.Value.ToString());

        if (OherRequirements.Rows.Count > 0)
        {
        OtherRequirementsGridView.DataSource = OherRequirements;
        OtherRequirementsGridView.DataBind();
        }
              
         

    }

    private void MethodAutoId()
    {
        DataTable dt = aDAL.GetId(Convert.ToInt32(ComId.Value));
        MasterIdHiddenField.Value = NoGenerator(Convert.ToInt32(dt.Rows[0][0].ToString()));
    }



    private string  NoGenerator(int id)
    {
        string code = string.Empty;
        int Id = id+1;

 
        code =  Id.ToString();
        return code;
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../RecruitmentManagement_UI/JobCreationView.aspx");
    }


    private void LoadEmployeeData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aDAL.LoadEmpAllInfofById(id);
        if (dtdata.Rows.Count > 0)
        {

            EmpIdHiddenfield.Value = dtdata.Rows[0]["EmployeeId"].ToString();
            ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
           

        }
    }

    private void LoadTasks()
    {
        DataTable SalarySteInfo = new DataTable();

        SalarySteInfo = aDAL.LoadParticularsForEntry();
       
       

    }


    private void LoadTasksUpdate()
    {
        DataTable SalarySteInfo = new DataTable();

        SalarySteInfo = aDAL.LoadParticularsForUpdate(IncrementIdHiddenField.Value);
       

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {

        if (MasterIdHiddenField.Value == "")
        {
            Save();
        }
    }

    public void Save()
    {


        MemotblJobCreationDAO aDAO = new MemotblJobCreationDAO();

        aDAO.JobCreationId = Convert.ToInt32(IncrementIdHiddenField.Value);
      
        aDAO.HeaderTitle = lblLabelInfo.Text;
        aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
        aDAO.FirstTitle = Server.HtmlEncode(txtFirstBody.Text);
        aDAO.SecondTitle = Server.HtmlEncode(txtSecendBody.Text);
        aDAO.Positiontitle =  (txtPositiontitle.Text);
        aDAO.WeOffer = Server.HtmlEncode(txtWhatWeOffer.Text);
        aDAO.APPLYINSTRACTIONS = Server.HtmlEncode(txtInstraction.Text);
        aDAO.NameDesignation = Server.HtmlEncode(txtDesignationEach.Text);
        aDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
        aDAO.EntryDate = DateTime.Now;
       

        int id = aDAL.SaveInfo(aDAO);

        if (id > 0)
        {


            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                MemotblJobReqKeyResponDAO aJobReqKeyRespon = new MemotblJobReqKeyResponDAO();

                aJobReqKeyRespon.MemoJobCreationId = id;
              
                aJobReqKeyRespon.JobReqKeyResName = KeyResponGridView.Rows[i].Cells[0].Text;


                aDAL.SaveJobReqKeyRespon(aJobReqKeyRespon);
            }
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                MemoOtherRequirementDetailDAO aOtherRequirementDetailDAO =
                    new MemoOtherRequirementDetailDAO();
                aOtherRequirementDetailDAO.MemoJobCreationId = id;
                aOtherRequirementDetailDAO.OtherRequirement = OtherRequirementsGridView.Rows[i].Cells[0].Text;


                aDAL.OtherRequirementsDetailSave(aOtherRequirementDetailDAO);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Operation successfully done...');",
                  true);
        }



    }


    public void Update()
    {


        MemotblJobCreationDAO aDAO = new MemotblJobCreationDAO();

        aDAO.MemoJobCreationId = Convert.ToInt32(MasterIdHiddenField.Value);

        aDAO.JobCreationId = Convert.ToInt32(IncrementIdHiddenField.Value);

        aDAO.HeaderTitle = lblLabelInfo.Text;
        aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
        aDAO.FirstTitle = WebUtility.HtmlEncode(txtFirstBody.Text);

        aDAO.SecondTitle = WebUtility.HtmlEncode(txtSecendBody.Text);
        aDAO.Positiontitle = (txtPositiontitle.Text);
        aDAO.WeOffer = WebUtility.HtmlEncode(txtWhatWeOffer.Text);
        aDAO.APPLYINSTRACTIONS = WebUtility.HtmlEncode(txtInstraction.Text);
        aDAO.NameDesignation = WebUtility.HtmlEncode(txtDesignationEach.Text);
        aDAO.UpdateBy = Convert.ToInt32(Session["UserId"]);


        aDAO.UpdateDate = DateTime.Now;


         aDAL.UpdateInfo(aDAO);

         if (Convert.ToInt32(MasterIdHiddenField.Value) > 0)
        {

            aDAL.DeleteMemoKeyResponDetails(MasterIdHiddenField.Value.ToString());
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                MemotblJobReqKeyResponDAO aJobReqKeyRespon = new MemotblJobReqKeyResponDAO();

                aJobReqKeyRespon.MemoJobCreationId = Convert.ToInt32(MasterIdHiddenField.Value);

                aJobReqKeyRespon.JobReqKeyResName = KeyResponGridView.Rows[i].Cells[0].Text;


                aDAL.SaveJobReqKeyRespon(aJobReqKeyRespon);
            }

            aDAL.DeleteMemoRequirementsDetails(MasterIdHiddenField.Value.ToString());
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                MemoOtherRequirementDetailDAO aOtherRequirementDetailDAO =
                    new MemoOtherRequirementDetailDAO();
                aOtherRequirementDetailDAO.MemoJobCreationId = Convert.ToInt32(MasterIdHiddenField.Value);
                aOtherRequirementDetailDAO.OtherRequirement = OtherRequirementsGridView.Rows[i].Cells[0].Text;


                aDAL.OtherRequirementsDetailSave(aOtherRequirementDetailDAO);
            }

 
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Data Updated Successfull...');",
                  true);
        }



    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        PopUp(Convert.ToInt32(MasterIdHiddenField.Value));
    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/MemoPrintJobCirculationReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (MasterIdHiddenField.Value != "")
        {
            Update();
        }
    }

   

    protected void textButton_OnClick(object sender, EventArgs e)
    {
        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobReqKeyResName");
        DataRow dataRow;

        if (jdTextBox.Text != "")
        {
            string jd = jdTextBox.Text.Trim();

            if (KeyResponGridView.Rows.Count == 0)
            {
                dataRow = aDataTable.NewRow();

                dataRow = aDataTable.NewRow();
                dataRow["JobReqKeyResName"] = jd;

                aDataTable.Rows.Add(dataRow);

                KeyResponGridView.DataSource = aDataTable;
                KeyResponGridView.DataBind();
                jdTextBox.Text = string.Empty;
            }

            else
            {
                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }

                dataRow = aDataTable.NewRow();
                dataRow["JobReqKeyResName"] = jd;

                aDataTable.Rows.Add(dataRow);

                KeyResponGridView.DataSource = aDataTable;
                KeyResponGridView.DataBind();
                jdTextBox.Text = string.Empty;
            }
        }
    
}

    protected void editImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string jd = KeyResponGridView.Rows[rowindex].Cells[0].Text;
        jdTextBox.Text = jd;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobReqKeyResName");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();
    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobReqKeyResName");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();

    }

    protected void OtherRequirementsAddButton_OnClick(object sender, EventArgs e)
    {
        var aDataTable = new DataTable();

        aDataTable.Columns.Add("OtherRequirementsName");
        DataRow dataRow;

        if (othersTextBox.Text != "")
        {
            string jd = othersTextBox.Text.Trim();

            if (OtherRequirementsGridView.Rows.Count == 0)
            {
                dataRow = aDataTable.NewRow();

                dataRow = aDataTable.NewRow();
                dataRow["OtherRequirementsName"] = jd;

                aDataTable.Rows.Add(dataRow);

                OtherRequirementsGridView.DataSource = aDataTable;
                OtherRequirementsGridView.DataBind();
                othersTextBox.Text = string.Empty;
            }

            else
            {
                for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                {
                    if (OtherRequirementsGridView.Rows[i].Cells[0].Text != jd)
                    {
                        dataRow = aDataTable.NewRow();
                        dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                        aDataTable.Rows.Add(dataRow);
                    }

                    else
                    {
                        othersTextBox.Text = "";
                        ShowMessageBox("Data already Exist !!");
                    }
                }

                dataRow = aDataTable.NewRow();
                dataRow["OtherRequirementsName"] = jd;

                aDataTable.Rows.Add(dataRow);

                OtherRequirementsGridView.DataSource = aDataTable;
                OtherRequirementsGridView.DataBind();
                othersTextBox.Text = string.Empty;
            }
        }
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void editOtherRequirementsGridViewButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string jd = OtherRequirementsGridView.Rows[rowindex].Cells[0].Text;
        othersTextBox.Text = jd;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("OtherRequirementsName");

        DataRow dataRow;

        if (OtherRequirementsGridView.Rows.Count > 0)
        {
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        OtherRequirementsGridView.DataSource = aDataTable;
        OtherRequirementsGridView.DataBind();
    }

    protected void deleteOtherRequirementsGridViewButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();


        aDataTable.Columns.Add("OtherRequirementsName");

        DataRow dataRow;

        if (OtherRequirementsGridView.Rows.Count > 0)
        {
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();


                    dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        OtherRequirementsGridView.DataSource = aDataTable;
        OtherRequirementsGridView.DataBind();
    }
}