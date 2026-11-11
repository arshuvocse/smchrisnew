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

public partial class Transfer_UI_MemoPrintEmployeePromotion : System.Web.UI.Page
{
    MemoPrintEmployeePromotionDAL aDAL = new MemoPrintEmployeePromotionDAL();
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
                    dtdata = aDAL.LoadMemoPrintIncrementByMId(mid);
                    if (dtdata.Rows.Count > 0)
                    {

                        lblLabelInfo.Text = dtdata.Rows[0]["HeaderInfo"].ToString();
                        ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
                        lblDate.Text =   Convert.ToDateTime(dtdata.Rows[0]["HeaderDate"]).ToString("MMMM dd, yyyy");
                        lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
                        lblEmployeeCode.Text = dtdata.Rows[0]["EmpCode"].ToString();

                        lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();
                        lblDepartment.Text = dtdata.Rows[0]["Department"].ToString();
                        lblOffice.Text = dtdata.Rows[0]["PlaceofPosting"].ToString();

                      

                        txtPreSalStep.Text = dtdata.Rows[0]["PreviousStep"].ToString();
                        txtIncrementalStep.Text = dtdata.Rows[0]["IncrementalStep"].ToString();
                        txtSalutation.Text = dtdata.Rows[0]["Salutation"].ToString();
                        txtBodyofletter.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["FirstParagraph"].ToString());
                        txtComplimentaryClose.Text =  WebUtility.HtmlDecode(dtdata.Rows[0]["ComplimentaryClose"].ToString());
                        txtSincerely.Text = dtdata.Rows[0]["YoursSincerely"].ToString();
                        txtName.Text =  WebUtility.HtmlDecode(dtdata.Rows[0]["Name"].ToString());
                        txtCopyTO.Text = WebUtility.HtmlDecode(dtdata.Rows[0]["CopyTo"].ToString());


                        txtPreviousSalaryGrade.Text = dtdata.Rows[0]["PreviousSalaryGrade"].ToString();
                        txtNewSalaryGrade.Text = dtdata.Rows[0]["NewSalaryGrade"].ToString();


                        txtPreviousDesignation.Text = dtdata.Rows[0]["PreviousDesignation"].ToString();
                        txtNewDesignation.Text = dtdata.Rows[0]["NewDesignation"].ToString();



                        LoadTasksUpdate();
                        editButton.Visible = true;
                    }

                    else
                    {
                       
                        lblDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                        LoadEmployeeData(Convert.ToInt32(IncrementIdHiddenField.Value));
                        MethodAutoId();
                        lblLabelInfo.Text = ComName.Value + "/HR/" + DateTime.Now.Year + " - " + MasterIdHiddenField.Value.ToString();
                        LoadTasks();
                        submitButton.Visible = true;
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
        Response.Redirect("EmployeePromotionEntryView.aspx");
    }


    private void LoadEmployeeData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aDAL.LoadEmpAllInfofById(id);
        if (dtdata.Rows.Count > 0)
        {

            EmpIdHiddenfield.Value = dtdata.Rows[0]["EmployeeId"].ToString();
            ComId.Value = dtdata.Rows[0]["CompanyId"].ToString();
            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();
            ComName.Value = dtdata.Rows[0]["ShortName"].ToString();
            txtSalutation.Text = "Dear Mr. "+ dtdata.Rows[0]["EmpName"].ToString() +", ";


            lblEmployeeCode.Text = dtdata.Rows[0]["EmployeeCode"].ToString();
           
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();

           
 
         
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();




            lblOffice.Text = dtdata.Rows[0]["SalaryLocation"].ToString();
            txtPreSalStep.Text = dtdata.Rows[0]["CurrentStep"].ToString();
            txtIncrementalStep.Text = dtdata.Rows[0]["IncrementalStep"].ToString();

            txtPreviousSalaryGrade.Text = dtdata.Rows[0]["CurrentGrade"].ToString();
            txtNewSalaryGrade.Text = dtdata.Rows[0]["IncrementalGrade"].ToString();

            txtNewDesignation.Text = dtdata.Rows[0]["Designation"].ToString();
            txtPreviousDesignation.Text = dtdata.Rows[0]["oldDesignation"].ToString();

        }
    }

    private void LoadTasks()
    {
        DataTable SalarySteInfo = new DataTable();

        SalarySteInfo = aDAL.LoadParticularsForEntry();
        gvSalaryStep.DataSource = SalarySteInfo;
        gvSalaryStep.DataBind();
       

    }


    private void LoadTasksUpdate()
    {
        DataTable SalarySteInfo = new DataTable();

        SalarySteInfo = aDAL.LoadParticularsForUpdate(IncrementIdHiddenField.Value);
        gvSalaryStep.DataSource = SalarySteInfo;
        gvSalaryStep.DataBind();


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


        MemoPrintEmployeePromotionDAO aDAO = new MemoPrintEmployeePromotionDAO();

        aDAO.EmployeePromotionId = Convert.ToInt32(IncrementIdHiddenField.Value);
        aDAO.CompanyId = Convert.ToInt32(ComId.Value);
        aDAO.HeaderInfo =  lblLabelInfo.Text;
        aDAO.HeaderDate = Convert.ToDateTime(lblDate.Text);
        aDAO.EmpCode =  lblEmployeeCode.Text;
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
        aDAO.CopyTo = WebUtility.HtmlEncode(txtCopyTO.Text);


        aDAO.PreviousSalaryGrade = txtPreviousSalaryGrade.Text;
        aDAO.NewSalaryGrade = txtNewSalaryGrade.Text;
        aDAO.PreviousDesignation = txtPreviousDesignation.Text;
        aDAO.NewDesignation = txtNewDesignation.Text;
        
        int id = aDAL.SaveInfo(aDAO);

        aDAL.DeleteMemoIncrementDetails(aDAO.EmployeePromotionId.ToString());
        for (int i = 0; i < gvSalaryStep.Rows.Count; i++)
        {
            MemoPrintEmployeePromotionDetailsDAO ADetailsDao = new MemoPrintEmployeePromotionDetailsDAO();
           ADetailsDao. MemoEmployeePromotionId = aDAO.EmployeePromotionId;
            ADetailsDao.ParticularsId = Convert.ToInt32(gvSalaryStep.DataKeys[i][0].ToString());
            ADetailsDao.PreStepId = Convert.ToDecimal(((TextBox) gvSalaryStep.Rows[i].FindControl("txtAmount1")).Text);
            ADetailsDao.NewStepId = Convert.ToDecimal(((TextBox) gvSalaryStep.Rows[i].FindControl("txtAmount2")).Text);

           
            int idd =
                aDAL.MemoIncrementDetailsSaveInfo(
                    ADetailsDao);
        }
        

        ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation successfully done...');",
                 true);


        
    }

    protected void  btnPrint_Click(object sender, EventArgs e)
    {
        PopUp(Convert.ToInt32(IncrementIdHiddenField.Value));
    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/MemoPrintIncrementReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
       Update();
    }

    private void Update()
    {
        MemoPrintEmployeePromotionDAO aDAO = new MemoPrintEmployeePromotionDAO();

        aDAO.EmployeePromotionId = Convert.ToInt32(IncrementIdHiddenField.Value);
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




        aDAL.UpdateInfo(aDAO);

        aDAL.DeleteMemoIncrementDetails(aDAO.EmployeePromotionId.ToString());
        for (int i = 0; i < gvSalaryStep.Rows.Count; i++)
        {
            MemoPrintEmployeePromotionDetailsDAO ADetailsDao = new MemoPrintEmployeePromotionDetailsDAO()
            {
                MemoEmployeePromotionId = aDAO.EmployeePromotionId,
                ParticularsId = Convert.ToInt32(gvSalaryStep.DataKeys[i][0].ToString()),
                PreStepId = Convert.ToDecimal(((TextBox)gvSalaryStep.Rows[i].FindControl("txtAmount1")).Text),
                NewStepId = Convert.ToDecimal(((TextBox)gvSalaryStep.Rows[i].FindControl("txtAmount2")).Text)

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
}