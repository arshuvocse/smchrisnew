using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Medica_Ul_EmpMedicalCheckUpViewReport : System.Web.UI.Page
{
    EmpExitDal aExitDal = new EmpExitDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            //   UserPersmissionValidation();
        
            LoadDropDownList();
            using (DataTable dt = _commonDataLoad.GetCompanyDDL())
            {
                company.DataSource = dt;
                company.DataValueField = "Value";
                company.DataTextField = "TextField";
                company.DataBind();
                company.SelectedIndex = 1;
            }
        }

        CheckuptxtDate.Attributes.Add("readonly", "readonly");
        CheckuptxtToDate.Attributes.Add("readonly", "readonly");
        

    }
    private void LoadDropDownList()
    {
        aExitDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);
        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";

            ddlDesignation.DataBind();

            ddlDesignation.SelectedValue = "Please Select One..";
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

    PermissionDAL aPermissionDal = new PermissionDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    VivaSetupInfoEntryDAL aCheckupDaL = new VivaSetupInfoEntryDAL();
    public void GetCompany()
    {
        try
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
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        }
    }

    private void LoadCheckupInformation()
    {
        try
        {
            if (CheckupddlStatus.SelectedValue == "1")
            {

                DataTable dataTable = aCheckupDaL.GeMedicalCheckupDetailsInfoRPT(" and tblMedicalCheckUpMaster.CompanyId IN (" + CompanyId() + ")" + CheckupCheckupparam());

                if (dataTable.Rows.Count > 0)
                {
                    CheckupGridView1.DataSource = dataTable;
                    CheckupGridView1.DataBind();

                }
                else
                {
                    CheckupGridView1.DataSource = null;
                    CheckupGridView1.DataBind();
                    aShowMessage.ShowMessageBox("No Data Found !!!", this);
                }
            }

            else  if (CheckupddlStatus.SelectedValue == "2")
            {

                DataTable dataTable = aCheckupDaL.GeMedicalCheckupDetailsInfoNotRPT(" and tblEmpGeneralInfo.CompanyId IN (" + CompanyId() + ")" + CheckupCheckupparam());

                if (dataTable.Rows.Count > 0)
                {
                    CheckupGridView1.DataSource = dataTable;
                    CheckupGridView1.DataBind();

                }
                else
                {
                    CheckupGridView1.DataSource = null;
                    CheckupGridView1.DataBind();
                    aShowMessage.ShowMessageBox("No Data Found !!!", this);
                }
            }
            else
            {
                CheckupGridView1.DataSource = null;
                CheckupGridView1.DataBind();
                CheckupddlStatus.Focus();
                aShowMessage.ShowMessageBox("Please Select Status !!!", this);
            }
         
        }
        catch (Exception)
        {

            //throw;
        }
    }

    private string CheckupCheckupparam()
    {
        string Checkupparameter = " ";

        if (CheckupddlStatus.SelectedValue == "1")
        {
            if (ddlCompany.SelectedValue != "")
            {
                Checkupparameter = Checkupparameter + " AND   tblMedicalCheckUpMaster.CompanyId  =  '" + ddlCompany.SelectedValue + "' ";
            }


            if (CheckuptxtDate.Text != string.Empty && CheckuptxtToDate.Text != string.Empty)
            {
                Checkupparameter = Checkupparameter + " AND tblMedicalCheckUpMaster.Date BETWEEN '" + CheckuptxtDate.Text + "' AND '" + CheckuptxtToDate.Text + "' ";
            }
            if (CheckuptxtDate.Text != string.Empty && CheckuptxtToDate.Text == string.Empty)
            {
                Checkupparameter = Checkupparameter + " AND  tblMedicalCheckUpMaster.Date BETWEEN '" + CheckuptxtDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
            }

            if (CheckuptxtDate.Text == string.Empty && CheckuptxtToDate.Text != string.Empty)
            {
                Checkupparameter = Checkupparameter + " AND  tblMedicalCheckUpMaster.Date BETWEEN '" + CheckuptxtToDate.Text + "' AND '" + CheckuptxtToDate.Text + "' ";
            }


            if (ddlDivision.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.DivisionId = '" + ddlDivision.SelectedValue + "'";
            }

            if (ddlDepartment.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
            }

            if (ddlSection.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.SectionId = '" + ddlSection.SelectedValue + "'";
            }

            if (ddlSubSection.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.SubSectionId = '" + ddlSubSection.SelectedValue + "'";
            }

            if (txtSearch.Text != "")
            {
                Checkupparameter = Checkupparameter + "  and (tblEmpGeneralInfo.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
            }

            if (NameTextBox.Text != "")
            {
                Checkupparameter = Checkupparameter + "  and  ( tblEmpGeneralInfo.EmpName LIKE '%" + NameTextBox.Text.Trim() + "%')";
            }

            if (ddlDesignation.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.DesignationId = '" + ddlDesignation.SelectedValue + "'";
            }

            if (ddlSalaryLocation.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
            }

        }


        if (CheckupddlStatus.SelectedValue == "2")
        {


            if (ddlSection.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.SectionId = '" + ddlSection.SelectedValue + "'";
            }

            if (ddlSubSection.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.SubSectionId = '" + ddlSubSection.SelectedValue + "'";
            }

            if (txtSearch.Text != "")
            {
                Checkupparameter = Checkupparameter + "  and (tblEmpGeneralInfo.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
            }

            if (NameTextBox.Text != "")
            {
                Checkupparameter = Checkupparameter + "  and  ( tblEmpGeneralInfo.EmpName LIKE '%" + NameTextBox.Text.Trim() + "%')";
            }

            if (ddlDesignation.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.DesignationId = '" + ddlDesignation.SelectedValue + "'";
            }

            if (ddlSalaryLocation.SelectedIndex > 0)
            {
                Checkupparameter = Checkupparameter + "  and    tblEmpGeneralInfo.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
            }

        }




        return Checkupparameter;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmpMedicalCheckUp.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }


    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if ((ddlCompany.SelectedValue != "") && (CheckuptxtDate.Text != "") && (CheckuptxtToDate.Text != ""))
        {
            LoadCheckupInformation();
        }
        else
        {
            CheckupGridView1.DataSource = null;
            CheckupGridView1.DataBind();
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select all the searching criteria !!!", this);
        }
    }

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (CheckupGridView1.Rows.Count > 0)
        {
            string attachment = "attachment; filename=EmployeeInformationList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);





            LoadCheckupInformation();


            CheckupGridView1.AllowPaging = false;
            
            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            CheckupGridView1.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            CheckupGridView1.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of CheckupGridView1 header row
            foreach (TableCell tableCell in CheckupGridView1.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of CheckupGridView1
            foreach (GridViewRow gridViewRow in CheckupGridView1.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            CheckupGridView1.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3> Medical Check-Up Completed  
  List	</h3>

</span>";

            string notco = @"<span   style='text-align:center'>
   <h3> Medical Check-Up Not Completed  
  List	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);

            if (CheckupddlStatus.SelectedValue=="1")
            {
                HttpContext.Current.Response.Write(SubTi);
            }
            if (CheckupddlStatus.SelectedValue == "2")
            {
                HttpContext.Current.Response.Write(notco);
            }
          
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'CheckupGridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
         
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        string empName = NameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            NameTextBox.Text = emp[2];

        }
    }

    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(ddlCompany.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(ddlCompany.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(ddlCompany.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }
        }
    }
}