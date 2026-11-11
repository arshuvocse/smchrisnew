using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.BirthDayMailGenerate_DAL;
using DAL.COMMON_DAL;
using DAL.MenuSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class BirthDayMailGenerate_BirthDayMailGenerateForEmployee : System.Web.UI.Page
{
    private static string _userId;
    private BirthDayMailGenerateDAL _commonDataLoad = new BirthDayMailGenerateDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            LoadInitialDDL();
            GetCompany();
      //      UserPersmissionValidation();
         //   EmpBirthDayLoadEMPInfo();
        }
    }

    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
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

                    btn_New.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    EmpBirthDayloadGridView.Columns[EmpBirthDayloadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    EmpBirthDayloadGridView.Columns[EmpBirthDayloadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    EmpBirthDayloadGridView.Columns[EmpBirthDayloadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public void GetCompany()
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
    private void EmpBirthDayLoadEMPInfo()
    {
        DataTable jobCreationInfos = new DataTable();

        jobCreationInfos = _commonDataLoad.GetEMpInfos( EmpBirthDayGenerateParameter() + "");
        EmpBirthDayloadGridView.DataSource = jobCreationInfos;
        EmpBirthDayloadGridView.DataBind();
    }
    [WebMethod(EnableSession = true)]
    public static List<tblEmpGeneralInfo> LoadEmployeeInfoList()
    {
        UserCommonDAL _userCommonDal = new UserCommonDAL();
        return _userCommonDal.LoadEmployeeInfoList();
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteEmp(string EmpInfoId)
    {
        string status = "ok";
        int mid = int.Parse(EmpInfoId);
        try
        {
            //using (var db = new HRIS_SMCEntities())
            //{
            //    var mpb = (from u in db.tblEmpGeneralInfoes where u.EmpInfoId == mid select u).FirstOrDefault();
            //    mpb.IsActive = false;
            //    mpb.UpdateDate = DateTime.Now;
            //    mpb.Updateby = _userId;
            //    db.SaveChanges();
            //}

        CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
            bool sts = _commonDataLoad.DeleteEmployeeById(mid);


        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }


    private string EmpBirthDayGenerateParameter()
    {
        string parameter = " ";

        if (ddlCompany.SelectedIndex >0)
        {
            parameter = parameter + "  and    com.CompanyId = '"+ddlCompany.SelectedValue+"'";
        }

        if (MonthDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and   FORMAT(e.DateOfBirth, 'MMMM') = '" + MonthDropDownList.SelectedValue + "'";
        }

       

      



        return parameter;
    }

    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmployeeInfoEntry.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = EmpBirthDayloadGridView.DataKeys[0];
            if (datKey != null)
            {
                //string filepath = Path.GetDirectoryName(Request.Path);
                //filepath = filepath.TrimStart('\\');
                //string exten = Path.GetExtension(Request.Path);
                //if (exten == string.Empty)
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                //}
                //else
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                //}
                //Session["ApprovalPage"] = filepath;
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Edit";
               
                Response.Redirect("EmployeeInfoEntry.aspx?mid=" +  e.CommandArgument.ToString());
                //+ "&mdid=" + EmpBirthDayloadGridView.DataKeys[rowindex][1].ToString());    

            }


        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = EmpBirthDayloadGridView.DataKeys[0];

            if (datKey != null)
            {
                //Session["ApprovalPage"] = filepath;
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "View";
                Response.Redirect("EmployeeInfoEntry.aspx?mid=" + e.CommandArgument.ToString());
               

            }

           

        }

        if (e.CommandName == "DeleteData")
        {
            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string masterId = EmpBirthDayloadGridView.DataKeys[rowindex][0].ToString();

            //bool masterStatus = aJobCreationDal.DeleteJobCreationById(masterId);
            //bool detailStatus = aJobCreationDal.DeleteJobCreationDetailById(masterId);

            //if (masterStatus && detailStatus)
            //{
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
            //    LoadJobCreationInfo();
            //}

            int rowindex = Convert.ToInt32(e.CommandArgument);

          

            var datKey = EmpBirthDayloadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Delete";
                Response.Redirect("EmployeeInfoEntry.aspx?mid=" + e.CommandArgument.ToString());
            }

            
        }
        if (e.CommandName == "ViewReport")
        {

            PopUp(Convert.ToInt32(e.CommandArgument.ToString()));
        }



    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/EmployeeInfoListReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void EmpBirthDaySearchButton_OnClick(object sender, EventArgs e)
    {
        if (EmpBirthDayValidation())
        {
            EmpBirthDayLoadEMPInfo();
        }
        
    }

    private bool EmpBirthDayValidation()
    {


        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select a Company!!", this);
            ddlCompany.Focus();
            return false;
        }

        if (MonthDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox("Please Select a Month!!", this);
            MonthDropDownList.Focus();
            return false;
        }
 

        return true;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EmpBirthDayloadGridView.PageIndex = e.NewPageIndex;
        
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        
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

   

    

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (EmpBirthDayloadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=MonthWiseBirthDayEmployeeInformation.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

           


             
            //EmpBirthDayloadGridView.Columns[1].Visible =
            //            false;
            //EmpBirthDayloadGridView.Columns[10].Visible =
            //   false;
            //EmpBirthDayloadGridView.Columns[11].Visible =
            //   false;
            //EmpBirthDayloadGridView.Columns[12].Visible =
            //false;

           EmpBirthDayloadGridView.AllowPaging = false;
           this.EmpBirthDayLoadEMPInfo();
            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            EmpBirthDayloadGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(EmpBirthDayloadGridView);
            //frm.RenderControl(htw);

            EmpBirthDayloadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in EmpBirthDayloadGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in EmpBirthDayloadGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            EmpBirthDayloadGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3> Employee Information List	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
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
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void EmpBirthDaybtnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BirthDayMailGenerateForEmployee.aspx");
    }
}