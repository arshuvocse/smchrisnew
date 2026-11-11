using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.AllPrintLetter_DAL;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class AllPrintLetter_MemoPromotion : System.Web.UI.Page
{
    MemoPromotionDAL aDAL = new MemoPromotionDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private int EmpID = 0;
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
                    dtdata = aDAL.LoadMemoPrintIncrementByMId(mid);
                    if (dtdata.Rows.Count > 0)
                    {

                        lblLabelInfo.Text = dtdata.Rows[0]["HeaderInfo"].ToString();
                        ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
                     
                        Session["CompanyId"] = ComId.Value;
                        lblDate.Text =   Convert.ToDateTime(dtdata.Rows[0]["HeaderDate"]).ToString("MMMM dd, yyyy");
                        lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
                        lblEmployeeCode.Text = dtdata.Rows[0]["EmpCode"].ToString();

                        lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();

                        lblCompany.Text = dtdata.Rows[0]["CompanyName"].ToString();


                        lblDepartment.Text = dtdata.Rows[0]["Department"].ToString();




                        lblOffice.Text = dtdata.Rows[0]["PlaceofPosting"].ToString();
                        txtPreSalStep.Text = dtdata.Rows[0]["PreviousStep"].ToString();
                        txtIncrementalStep.Text = dtdata.Rows[0]["IncrementalStep"].ToString();

                        txtSalutation.Text = dtdata.Rows[0]["Salutation"].ToString();
                        txtBodyofletter.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["FirstParagraph"].ToString());
                        txtComplimentaryClose.Text =  WebUtility.HtmlDecode(dtdata.Rows[0]["ComplimentaryClose"].ToString());
                        txtSincerely.Text = dtdata.Rows[0]["YoursSincerely"].ToString();
                        txtName.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["Name"].ToString());
                        repEmpIdHiddenField.Value = (dtdata.Rows[0]["ToEmployee"].ToString());

                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(Convert.ToInt32(repEmpIdHiddenField.Value)))
                        {
                            EmployeeNameTextBox.Text = dtdesignation.Rows[0]["SignatureEmployee"].ToString();
                        }
                        txtCopyTO.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["CopyTo"].ToString());

                        lblHeader.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["Header"].ToString());
                        txtThirdPara.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["ThirdPara"].ToString());
                        txtFourthPara.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["FourthPara"].ToString());
                   //      LoadTasksUpdate();
                        editButton.Visible = true;

                        using (
                            DataTable aDataTable =
                                aDAL.LoadMemoPrintIncrementDetailsByMId(mid))
                        {
                            KeyResponGridView.DataSource = aDataTable;
                            KeyResponGridView.DataBind();
                        }

                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["EmpId"]))
                        {

                              EmpID = int.Parse(Request.QueryString["EmpId"]);
                 
                            lblDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                            LoadEmployeeData(Convert.ToInt32(mid));
                            MethodAutoId();
                            lblLabelInfo.Text = ComName.Value + "/HR/" + DateTime.Now.Year + " - " +
                                                MasterIdHiddenField.Value.ToString();
                            // LoadTasks();
                            submitButton.Visible = true;
                           
                        }
                    }

                   
                }
            }


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
        Response.Redirect("../Transfer_UI/EmployeePromotionEntryView.aspx");
    }


    private void LoadEmployeeData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aDAL.LoadEmpAllInfofById(id);
        if (dtdata.Rows.Count > 0)
        {

            EmpIdHiddenfield.Value = dtdata.Rows[0]["EmployeeId"].ToString();
            ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
            Session["CompanyId"] = "";
            Session["CompanyId"] = ComId.Value;
            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
            ComName.Value = dtdata.Rows[0]["ShortName"].ToString();
            txtSalutation.Text = "Dear Mr. "+ dtdata.Rows[0]["EmpName"].ToString() +", ";

            lblCompany.Text = dtdata.Rows[0]["CompanyName"].ToString();
            lblEmployeeCode.Text = dtdata.Rows[0]["EmployeeCode"].ToString();
           
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();

           
 
         
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();




            lblOffice.Text = dtdata.Rows[0]["SalaryLocation"].ToString();
            lblHeader.Text = dtdata.Rows[0]["NPromoType"].ToString();

            if (lblHeader.Text == "Promotion")
            {
                string Designation = dtdata.Rows[0]["NDesignation"].ToString();
                string NSalaryGrade = dtdata.Rows[0]["NSalaryGrade"].ToString();
                string NSalaryStepName = dtdata.Rows[0]["NSalaryStepName"].ToString();
                string NGrossAmount = dtdata.Rows[0]["NGrossAmount"].ToString();

               // ConvertNumbertoWords(Convert.ToDecimal(NGrossAmount));
              
                
                string EffectDate = "";
                dtdata = aDAL.LoadMemoPrintGetEffectivedateIncrementByMId(mid);
                if (dtdata.Rows.Count > 0)
                {
                    try
                    {
                        EffectDate = Convert.ToDateTime(dtdata.Rows[0]["EffectiveDate"])
                                     .ToString("dd-MMM-yyyy");
                    }
                    catch (Exception)
                    {


                    }

                }
                string Bodyofletter =
                    "The Management is pleased to inform you that your salary has been promoted as Grade " + Designation+" in salary "+ NSalaryGrade+ ", "+NSalaryStepName+ "with effect from "+
                    EffectDate + ". The Monthly salary in your new position will be TK. " + NGrossAmount + "( Taka v"   +" ) Only."+ " Break-up salary is given below: ";
                ;
                txtBodyofletter.Text = Bodyofletter;
                txtComplimentaryClose.Text = "All other terms and conditions of your appoinment letter shall remain unchanged.";
                txtThirdPara.Text = "You will be given a new/updateed job description in line with your new position.";
                txtFourthPara.Text = "The Management wants to congratulate you for your new position and looks forward to your contribution towards progress and prosperity of the organization.";
            }

            if (lblHeader.Text == "Upgradation")
            {
                lblHeader.Text = "Up-gradation";

                string Designation = dtdata.Rows[0]["NDesignation"].ToString();
                string NSalaryGrade = dtdata.Rows[0]["NSalaryGrade"].ToString();
                string NSalaryStepName = dtdata.Rows[0]["NSalaryStepName"].ToString();
                string NGrossAmount = dtdata.Rows[0]["NGrossAmount"].ToString();

                // ConvertNumbertoWords(Convert.ToDecimal(NGrossAmount));


                string EffectDate = "";
                dtdata = aDAL.LoadMemoPrintGetEffectivedateIncrementByMId(mid);
                if (dtdata.Rows.Count > 0)
                {
                    try
                    {
                        EffectDate = Convert.ToDateTime(dtdata.Rows[0]["EffectiveDate"])
                                     .ToString("dd-MMM-yyyy");
                    }
                    catch (Exception)
                    {


                    }

                }
                string JobLength = "";
                string CompanyName = "";
                using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(EmpID))
                {
                    
                    JobLength = dtdesignation.Rows[0]["LengthServicewithSMC"].ToString();
                    CompanyName = dtdesignation.Rows[0]["CompanyName"].ToString();

                }
                string Bodyofletter =
                    "The Management is pleased to inform you that your salary has been promoted as Grade " + Designation + " in salary " + NSalaryGrade + ", " + NSalaryStepName + "with effect from " +
                    EffectDate + ". This up-gradation is due as per policy in consideration of the fact that you have completed " + JobLength + " continuous service with " + CompanyName + " in your present grade." + " Your new monthly salary will be TK." + NGrossAmount + ". A Break-down of the salary is given below: ";
                ;
                txtBodyofletter.Text = Bodyofletter;
                txtComplimentaryClose.Text = "All other terms and conditions of your appoinment letter shall remain unchanged.";
                txtThirdPara.Text = "You will be given a new/updateed job description in line with your new position.";
                txtFourthPara.Text = "The Management wants to congratulate you for your new position and looks forward to your contribution towards progress and prosperity of the organization.";
            }
            //txtPreSalStep.Text = dtdata.Rows[0]["CurrentStep"].ToString();
            //txtIncrementalStep.Text = dtdata.Rows[0]["IncrementalStep"].ToString();


        


        }
    }

    

    protected void submitButton_Click(object sender, EventArgs e)
    {

        if (IncrementIdHiddenField.Value != "" && IncrementIdHiddenField.Value!=null)
        {
            Save();
        }
    }

    public void Save()
    {

        if (Validation())
        {
            MemoPrintPromotionDAO aDAO = new MemoPrintPromotionDAO();

            aDAO.EmployeePromotionEntryId = Convert.ToInt32(IncrementIdHiddenField.Value);
            aDAO.CompanyId = Convert.ToInt32(ComId.Value);
            aDAO.HeaderInfo = lblLabelInfo.Text;
            aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
            aDAO.EmpCode = lblEmployeeCode.Text;
            aDAO.EmpName = lblEmp.Text;
            aDAO.Designation = lblDesignation.Text;
            aDAO.Department = lblDepartment.Text;
            aDAO.PreviousStep = txtPreSalStep.Text;
            aDAO.PlaceofPosting = lblOffice.Text;
            aDAO.IncrementalStep = txtIncrementalStep.Text;
            aDAO.Salutation = txtSalutation.Text;
            aDAO.FirstParagraph = WebUtility.HtmlEncode(txtBodyofletter.Text);
            aDAO.ComplimentaryClose = WebUtility.HtmlEncode(txtComplimentaryClose.Text);
            aDAO.YoursSincerely = txtSincerely.Text;
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
            aDAO.CopyTo = WebUtility.HtmlEncode(txtCopyTO.Text);
            aDAO.ToEmployee = Convert.ToInt32(repEmpIdHiddenField.Value);



            aDAO.CompanyName = lblCompany.Text;

            aDAO.ThirdPara = WebUtility.HtmlEncode(txtThirdPara.Text);
            aDAO.FourthPara = WebUtility.HtmlEncode(txtFourthPara.Text);
            aDAO.Header = WebUtility.HtmlEncode(lblHeader.Text);


            int id = aDAL.SaveInfo(aDAO);

            aDAL.DeleteMemoIncrementDetails(aDAO.EmployeePromotionEntryId.ToString());
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                MemoPrintIncrementDetailsDAO ADetailsDao = new MemoPrintIncrementDetailsDAO()
                {
                    MemoEmployeePromotionId = aDAO.EmployeePromotionEntryId,

                    PName = KeyResponGridView.Rows[i].Cells[0].Text,
                    PAmount = Convert.ToDecimal(KeyResponGridView.Rows[i].Cells[1].Text)

                };
                int idd =
                    aDAL.MemoIncrementDetailsSaveInfo(
                        ADetailsDao);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation successfully done...');",
                true);

        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
       string dd=  (Request.QueryString["mid"]);
       PopUp(Convert.ToInt32(dd));
    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/MemoPrintIncrementReportViwer.aspx?rptType=" + EmpInfoId + "&rt=MemoPromotion";
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
              Update();
        }
     
    }
    private bool Validation()
    {

        if (KeyResponGridView.Rows.Count==0)
        {
            aShowMessage.ShowMessageBox("Please Enter Particulars & Salary Break-Up", this);
            txtPName.Focus();
            return false;
        }


        if (EmployeeNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            EmployeeNameTextBox.Focus();
            return false;
        }








        return true;
    }
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private void Update()
    {
        MemoPrintPromotionDAO aDAO = new MemoPrintPromotionDAO();

        aDAO.EmployeePromotionEntryId = Convert.ToInt32(IncrementIdHiddenField.Value);
        aDAO.CompanyId = Convert.ToInt32(ComId.Value);
        aDAO.HeaderInfo = lblLabelInfo.Text;
        aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
        aDAO.EmpCode = lblEmployeeCode.Text;
        aDAO.EmpName = lblEmp.Text;
        aDAO.Designation = lblDesignation.Text;
        aDAO.Department = lblDepartment.Text;
        aDAO.PreviousStep = txtPreSalStep.Text;
        aDAO.IncrementalStep = txtIncrementalStep.Text;
        aDAO.Salutation = txtSalutation.Text;
        aDAO.FirstParagraph = WebUtility.HtmlEncode(txtBodyofletter.Text);
        aDAO.ComplimentaryClose = WebUtility.HtmlEncode(txtComplimentaryClose.Text);
        aDAO.YoursSincerely = txtSincerely.Text;
        aDAO.Name = WebUtility.HtmlEncode(txtName.Text);
        aDAO.CopyTo = WebUtility.HtmlEncode(txtCopyTO.Text);
        aDAO.ToEmployee = Convert.ToInt32(repEmpIdHiddenField.Value);
        aDAO.CompanyName = lblCompany.Text;

        aDAO.ThirdPara = WebUtility.HtmlEncode(txtThirdPara.Text);
        aDAO.FourthPara = WebUtility.HtmlEncode(txtFourthPara.Text);
        aDAO.Header = WebUtility.HtmlEncode(lblHeader.Text);
        aDAL.UpdateInfo(aDAO);

        aDAL.DeleteMemoIncrementDetails(aDAO.EmployeePromotionEntryId.ToString());
        for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
        {
            MemoPrintIncrementDetailsDAO ADetailsDao = new MemoPrintIncrementDetailsDAO()
            {
                MemoEmployeePromotionId = aDAO.EmployeePromotionEntryId,

                PName = KeyResponGridView.Rows[i].Cells[0].Text,
                PAmount = Convert.ToDecimal(KeyResponGridView.Rows[i].Cells[1].Text)

            };
            int idd =
                aDAL.MemoIncrementDetailsSaveInfo(
                    ADetailsDao);
        }


        ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Data Updated Successfull...');",
                  true);
    }

    protected void deleteImageButtonDirectlySupervices_OnClick(object sender, ImageClickEventArgs e)
    {
        
    }

    protected void editImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string jd = KeyResponGridView.Rows[rowindex].Cells[0].Text;
        string amount = KeyResponGridView.Rows[rowindex].Cells[1].Text;
        txtPName.Text = jd;
        txtPAmount.Text = amount;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("PName");
        aDataTable.Columns.Add("PAmount");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["PName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    dataRow["PAmount"] = KeyResponGridView.Rows[i].Cells[1].Text;
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

        aDataTable.Columns.Add("PName");
        aDataTable.Columns.Add("PAmount");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["PName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    dataRow["PAmount"] = KeyResponGridView.Rows[i].Cells[1].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();
    }

    protected void EducationRequirementImageButton_Click(object sender, EventArgs e)
    {
        if (AddEducationRequirementValidation())
        {
            
            string educationRequirements = txtPName.Text.Trim();
            string Major = txtPAmount.Text.Trim();

            var aDataTable = new DataTable();


            aDataTable.Columns.Add("PName");
            aDataTable.Columns.Add("PAmount");



            DataRow dataRow;

            if (KeyResponGridView.Rows.Count > 0)
            {
                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    if (KeyResponGridView.Rows[i].Cells[0].Text != educationRequirements)
                    {
                        dataRow = aDataTable.NewRow();


                        dataRow["PName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                        dataRow["PAmount"] = KeyResponGridView.Rows[i].Cells[1].Text;

                        aDataTable.Rows.Add(dataRow);
                        txtPName.Text = "";
                        txtPAmount.Text = "";
                    }

                    else
                    {
                        txtPName.Text = "";
                        txtPAmount.Text = "";
                        ShowMessageBox("Data already Exist !!");
                    }
                }

                dataRow = aDataTable.NewRow();


                dataRow["PName"] = educationRequirements;
                dataRow["PAmount"] = Major;

                aDataTable.Rows.Add(dataRow);

                KeyResponGridView.DataSource = aDataTable;
                KeyResponGridView.DataBind();
                txtPName.Text = "";
                txtPAmount.Text = "";

            }


            else
            {
                dataRow = aDataTable.NewRow();

                dataRow["PName"] = educationRequirements;
                dataRow["PAmount"] = Major;


                aDataTable.Rows.Add(dataRow);

                KeyResponGridView.DataSource = aDataTable;
                KeyResponGridView.DataBind();
                txtPName.Text = "";
                txtPAmount.Text = "";
            }
        }

    }
    private bool AddEducationRequirementValidation()
    {
        if (txtPName.Text == "")
        {
            ShowMessageBox("Please Enter Particulars !!!");
            txtPName.Focus();
            return false;
        }


        if (txtPAmount.Text == "")
        {
            ShowMessageBox("Please Enter Salary Break-Up !!!");
            txtPAmount.Focus();
            return false;
        }

        return true;
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void EmployeeNameTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = EmployeeNameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            EmployeeNameTextBox.Text = emp[2];
            repEmpIdHiddenField.Value = emp[0];


        }
        else
        {

            EmployeeNameTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }
}