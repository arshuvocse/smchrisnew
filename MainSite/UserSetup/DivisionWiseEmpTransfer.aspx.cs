using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.DivisionWiseEmpTransfer;
using DAL.Permission_DAL;
using DAO.DivisionWiseEmpTransfer;

public partial class UserSetup_DivisionWiseEmpTransfer : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();

    private  DivisionWiseTransferDal aTransferDal = new DivisionWiseTransferDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserPersmissionValidation();
            LoadInitialDDL();
        }
    }

    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            string text = Path.GetExtension(Request.Path);
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }

            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    LinkButton1.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                    //    Convert.ToBoolean(ViewState["View"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                    //    Convert.ToBoolean(ViewState["Delete"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                    //    Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("/Default.aspx");
        }
    }


    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    private string GenerateParameter()
    {
        string parameter = " ";

        if (ddlCompany.SelectedIndex > 0)
        {
            parameter = parameter + "  and Emp.CompanyId = '" + ddlCompany.SelectedValue + "'";
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            parameter = parameter + "  and Emp.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlWing.SelectedIndex > 0)
        {
            parameter = parameter + "  and Emp.DivisionId = '" + ddlWing.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  and Emp.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }

        if (ddlSection.SelectedIndex > 0)
        {
            parameter = parameter + "  and Emp.SectionId = '" + ddlSection.SelectedValue + "'";
        }

        if (ddlSubSection.SelectedIndex > 0)
        {
            parameter = parameter + "  and Emp.SubSectionId = '" + ddlSubSection.SelectedValue + "'";
        }


        return parameter;
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    private void GET_EmpInformation()
    {
        DataTable Dt = aTransferDal.Get_EmployeeInformation(GenerateParameter());

        if (Dt.Rows.Count > 0)
        {

            loadGridView.DataSource = null;
            loadGridView.DataBind();

            loadGridView.DataSource = Dt;
            loadGridView.DataBind();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            AlertMessageBoxShow("Data Not Found");
        }

    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
            Session["CompanyId"] = ddlCompany.SelectedValue;
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();


                ddlTransferDivision.DataSource = dt;
                ddlTransferDivision.DataValueField = "Value";
                ddlTransferDivision.DataTextField = "TextField";
                ddlTransferDivision.DataBind();
            }


            using (DataTable dt = _commonDataLoad.GetDDLComWind(ddlCompany.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            //using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            //{
            //    ddlDepartment.DataSource = dt;
            //    ddlDepartment.DataValueField = "Value";
            //    ddlDepartment.DataTextField = "TextField";
            //    ddlDepartment.DataBind();
            //}
            //using (DataTable dt = _commonDataLoad.GetDDLComSection(ddlCompany.SelectedValue))
            //{
            //    ddlSection.DataSource = dt;
            //    ddlSection.DataValueField = "Value";
            //    ddlSection.DataTextField = "TextField";
            //    ddlSection.DataBind();
            //}
            //using (DataTable dt = _commonDataLoad.GetDDLComSubSection(ddlCompany.SelectedValue))
            //{
            //    ddlSubSection.DataSource = dt;
            //    ddlSubSection.DataValueField = "Value";
            //    ddlSubSection.DataTextField = "TextField";
            //    ddlSubSection.DataBind();
            //}

        }
    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();

            ddlCompany.SelectedIndex = 1;
            ddlCompany_SelectedIndexChanged(null, null);
        }

     
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
            try
            {
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            }
            catch (Exception)
            {
                //throw;
            }
            try
            {
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            }
            catch (Exception)
            {
                //throw;
            }
        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
        }
    }


    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWing.SelectedValue != "")
        {
            _commonDataLoad.GetDepartmentList(ddlDepartment, ddlWing.SelectedValue);
        }
        else
        {
            ddlDepartment.Items.Clear();
        }
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "")
            {
                ddlSection.Items.Clear();
                ddlSubSection.Items.Clear();
                using (DataTable dtgetdata = aTransferDal.Get_Section_By_Department( Convert.ToInt32(ddlDepartment.SelectedValue)))
                {
                    ddlSection.DataSource = dtgetdata;
                    ddlSection.DataValueField = "SectionId";
                    ddlSection.DataTextField = "SectionName";
                    ddlSection.DataBind();
                    ddlSection.Items.Insert(0, new ListItem("Select an Option.....", String.Empty));
                }
            }
      
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            
            ddlSubSection.Items.Clear();
            if (ddlSection.SelectedValue != "")
            {
                using (DataTable dtgetdata = aTransferDal.Get_SubSection_By_Section(Convert.ToInt32(ddlSection.SelectedValue)))
                {
                    ddlSubSection.DataSource = dtgetdata;
                    ddlSubSection.DataValueField = "SubSectionId";
                    ddlSubSection.DataTextField = "SubSectionName";
                    ddlSubSection.DataBind();
                    ddlSubSection.Items.Insert(0, new ListItem("Select an Option.....", String.Empty));
                }
            }

        }
        catch (Exception)
        {

            //throw;
        }
     
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
           
    }



    protected void loadGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
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



    private bool Validation()
    {
       

        if (ddlCompany.SelectedIndex == 0)
        {
            AlertMessageBoxShow("Please Select Company..");

            return false;
        }

        return true;
    }


    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            GET_EmpInformation();
        }

   
    }
    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void SaveButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            

            DivisionWiseEmpTransferDao aDao = new DivisionWiseEmpTransferDao();


            if (rbType.Items[0].Selected)
            {

                aDao.IsEmployeeShiftHierarchyGenerate = true;
                aDao.IsOnlyEmployeeTransfer = false;

            if (ddlDivision.SelectedValue != "")
            {
                aDao.DivisionId = int.Parse(ddlDivision.SelectedValue);
            }

            if (ddlWing.SelectedValue != "")
            {
                aDao.DivisionWId = int.Parse(ddlWing.SelectedValue);
            }

            if (ddlDepartment.SelectedValue != "")
            {
                aDao.DepartmentId = int.Parse(ddlDepartment.SelectedValue);
            }

            if (ddlSection.SelectedValue != "")
            {
                aDao.SectionId = int.Parse(ddlSection.SelectedValue);
            }

            if (ddlSubSection.SelectedValue != "")
            {
                aDao.SubSectionId = int.Parse(ddlSubSection.SelectedValue);
            }


            }
            else
            {
                aDao.IsEmployeeShiftHierarchyGenerate = false;
                aDao.IsOnlyEmployeeTransfer = true;
                if (ddlTransferDivision.SelectedValue != "")
                {
                    aDao.DivisionId = int.Parse(ddlTransferDivision.SelectedValue);
                }

                if (ddlTransWing.SelectedValue != "")
                {
                    aDao.DivisionWId = int.Parse(ddlTransWing.SelectedValue);
                }

                if (ddlTransDepartment.SelectedValue != "")
                {
                    aDao.DepartmentId = int.Parse(ddlTransDepartment.SelectedValue);
                }

                if (ddlTransSection.SelectedValue != "")
                {
                    aDao.SectionId = int.Parse(ddlTransSection.SelectedValue);
                }

                if (ddlTransSubSection.SelectedValue != "")
                {
                    aDao.SubSectionId = int.Parse(ddlTransSubSection.SelectedValue);
                }


            }
            aDao.TransferDivisionId = int.Parse(ddlTransferDivision.SelectedValue);

            aDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

            string EmpIdAA = "";
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox check = (CheckBox)loadGridView.Rows[i].FindControl("Checked");

                if (check.Checked)
                {
                    int EmpId = Convert.ToInt32(loadGridView.DataKeys[i][0]);

                    EmpIdAA += EmpId+",";
             

                   
                  
                }
   
            }

            EmpIdAA = EmpIdAA.Trim(',');
            aDao.EmpInfoId = EmpIdAA;


            bool status = aTransferDal.Update(aDao);
            if (status)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Operation Successfull Done...');window.location ='DivisionWiseEmpTransfer.aspx';",
                          true);
            }

        }
        catch (Exception a)
        {
            throw a;
        }
    }

    protected void CheckUncheckAll(object sender, EventArgs e)
    {

        CheckBox ChkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("CheckAll");
        foreach (GridViewRow row in loadGridView.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox) row.FindControl("Checked");
            if (ChkBoxHeader.Checked)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }

        }

        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        //{
        //  CheckBox allcheBox = (CheckBox)loadGridView.Rows[i].FindControl("CheckAll");

        //  CheckBox check = (CheckBox)loadGridView.Rows[i].FindControl("Checked");

        //    if (allcheBox.Checked)
        //    {
        //        check.Checked = true;
        //    }
        //    else
        //    {
        //        check.Checked = false;
        //    }

        //}

     
    }

    protected void ddlTransferDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTransferDivision.SelectedValue != "")
        {
            ddlTransDepartment.Items.Clear();
            ddlTransSection.Items.Clear();
            ddlTransSubSection.Items.Clear();
            try
            {
                _commonDataLoad.GetDivisionWingList(ddlTransWing, ddlTransferDivision.SelectedValue);
            }
            catch (Exception)
            {
                //throw;
            }
            try
            {
                _commonDataLoad.GetDepartmentByDivList(ddlTransDepartment, ddlTransferDivision.SelectedValue);
            }
            catch (Exception)
            {
                //throw;
            }
        }
        else
        {
            ddlTransWing.Items.Clear();
            ddlTransDepartment.Items.Clear();
        }
    }

    protected void ddlTransWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTransWing.SelectedValue != "")
        {
            _commonDataLoad.GetDepartmentList(ddlTransDepartment, ddlTransWing.SelectedValue);
        }
        else
        {
            ddlTransDepartment.Items.Clear();
        }
    }

    protected void ddlTransDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTransDepartment.SelectedValue != "")
            {
                ddlTransSection.Items.Clear();
                ddlTransSubSection.Items.Clear();
                using (DataTable dtgetdata = aTransferDal.Get_Section_By_Department(Convert.ToInt32(ddlTransDepartment.SelectedValue)))
                {
                    ddlTransSection.DataSource = dtgetdata;
                    ddlTransSection.DataValueField = "SectionId";
                    ddlTransSection.DataTextField = "SectionName";
                    ddlTransSection.DataBind();
                    ddlTransSection.Items.Insert(0, new ListItem("Select an Option.....", String.Empty));
                }
            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void ddlTransSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlTransSubSection.Items.Clear();
            if (ddlTransSection.SelectedValue != "")
            {
                using (DataTable dtgetdata = aTransferDal.Get_SubSection_By_Section(Convert.ToInt32(ddlTransSection.SelectedValue)))
                {
                    ddlTransSubSection.DataSource = dtgetdata;
                    ddlTransSubSection.DataValueField = "SubSectionId";
                    ddlTransSubSection.DataTextField = "SubSectionName";
                    ddlTransSubSection.DataBind();
                    ddlTransSubSection.Items.Insert(0, new ListItem("Select an Option.....", String.Empty));
                }
            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void rbType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTransferDivision.SelectedValue = null;
        ddlTransWing.Items.Clear();
        ddlTransDepartment.Items.Clear();
        ddlTransSection.Items.Clear();
        ddlTransSubSection.Items.Clear();
        
        if (rbType.Items[0].Selected)
        {
            divTO.Visible = false;
        }
        else
        {
            divTO.Visible = true;
            
        }
    }
}