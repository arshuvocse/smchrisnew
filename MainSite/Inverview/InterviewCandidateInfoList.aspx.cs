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
using DAL.Inverview_DAL;
using DAL.MasterSetup_DAL;
using DAL.MPBudget;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Inverview_InterviewCandidateInfoList : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aCommonDAL = new ValidationDeleteCommonDAL();
    private static string _userId;
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private InterviewCandidateInfoListDAL aInterviewCandidateInfoList =new InterviewCandidateInfoListDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    private HiddenField hfJobID;
    private DropDownList ddlJobCirculation;
    private DropDownList ddlcompany;
    private DropDownList ddlFinYear;
    private DropDownList ddlDepartment;
    private TextBox txt_JobCirculation;
    protected void Page_Load(object sender, EventArgs e)
    {
        hfJobID = (HiddenField)IVSearchControl.FindControl("hfJobID");
        ddlcompany = (DropDownList)IVSearchControl.FindControl("ddlCompany") as DropDownList;
        ddlJobCirculation = (DropDownList)IVSearchControl.FindControl("ddlJobCirculation") as DropDownList;
        ddlFinYear = (DropDownList)IVSearchControl.FindControl("ddlFinYear") as DropDownList;
        ddlDepartment = (DropDownList)IVSearchControl.FindControl("ddlDepartment") as DropDownList;
        txt_JobCirculation = (TextBox)IVSearchControl.FindControl("txt_JobCirculation");
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            GetCompany();

            UserPersmissionValidation();
            //LoadGetInterviewCandidateInfoList();
        }
    }

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
    private void LoadGetInterviewCandidateInfoList(string jobid)
    {
        //string jobfilter = string.Empty;
        //if (!string.IsNullOrEmpty(jobid))
        //{
        //    jobfilter = " AND JobID=" + jobid;
        //}
        if (Validation())
        {
            using (
                DataTable dataTable =
                    aInterviewCandidateInfoList.GetInterviewCandidateInfoList(
                        " WHERE tblInterviewCandidateInfo.CompanyId IN (" + CompanyId() + ")" + GetPaeraMeter()))
            {
                if (dataTable.Rows.Count > 0)
                {
                    loadGridView.DataSource = dataTable;
                    loadGridView.DataBind();
                }
            }
        }
        
    }
    private string GetPaeraMeter()
    {
        string param = " ";
        if (ddlcompany.SelectedValue != "")
        {
            param = param + " and   com.CompanyId =  '" + ddlcompany.SelectedValue + "' ";
        }

        if (ddlJobCirculation.SelectedValue != "")
        {
            param = param + " AND  JobID=  '" + ddlJobCirculation.SelectedValue + "' ";
        }

        return param;
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

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
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



    [WebMethod(EnableSession = true)]
    public static List<InterviewCandidateInfoDao> LoadInterviewCandidateInfoList()
    {
        InterviewCommonDAL _interviewCommonDal = new InterviewCommonDAL();
        return _interviewCommonDal.LoadInterviewCandidateInfoList();
    }
    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("InterviewCandidateInfo.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteInterviewCandidateInfo(string CandidateID)
    {
        string status = "ok";
        int mid = int.Parse(CandidateID);
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                var mpb = (from u in db.tblInterviewCandidateInfoes where u.CandidateID == mid select u).FirstOrDefault();
                mpb.IsActive = false;
                mpb.UpdateDate = DateTime.Now;
                mpb.Updateby = _userId;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }

    
    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "EditData")
        {



            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

            if (!CheckEmpDepartmentAllocateOrNot(divisionId.ToString()))
                            {
           

                Session["Status"] = "Edit";


                Response.Redirect("InterviewCandidateInfo.aspx?mid=" + divisionId);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Can Not be Edited. Already Candidate Invited!!!');",
               true);

            }
        }
        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["CandidateID"] = "";
            Session["CandidateID"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("InterviewCandidateInfo.aspx?mid=" + divisionId);
        }

        if (e.CommandName == "DeleteData")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();
            if (!CheckEmpDepartmentAllocateOrNot(divisionId.ToString()))
            {
                Session["CandidateID"] = "";
                Session["CandidateID"] = divisionId;
                Session["Status"] = "Delete";
                Response.Redirect("InterviewCandidateInfo.aspx?mid=" + divisionId);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Can Not be Deleted!!!');",
              true);
        }
    }
    private bool CheckEmpDepartmentAllocateOrNot(string departmentId)
    {
        bool status = false;

        DataTable dataTable = aCommonDAL.CandidateInfoForItvitationAllocatedOrNotEMP(departmentId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }
    private bool Validation()
    {
        if (ddlcompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company!!", this);
            ddlcompany.Focus();
            return false;
        }


        if (ddlFinYear.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Financial Year!!", this);
            ddlFinYear.Focus();
            return false;
        }

        if (ddlDepartment.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Department!!", this);
            ddlDepartment.Focus();
            return false;
        }

        if (ddlJobCirculation.SelectedValue == "" )
        {
            aShowMessage.ShowMessageBox("Please Enter Job Circulation!!", this);
            ddlJobCirculation.Focus();
            return false;
        }


        return true;
    }
    protected void btn_LoadList_OnClick(object sender, EventArgs e)
    {

        if (ddlJobCirculation.SelectedValue != "")
        {
           

            string jobid = ddlJobCirculation.SelectedValue;
            LoadGetInterviewCandidateInfoList(jobid);
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Enter Job Circulation!!", this);
        }
       
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Interview_Candidate_Info_List.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            loadGridView.AllowPaging = false;





            loadGridView.Columns[13].Visible =
               false;
            loadGridView.Columns[14].Visible =
               false;
            loadGridView.Columns[15].Visible =
            false;

            LoadGetInterviewCandidateInfoList(ddlJobCirculation.SelectedValue);

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            loadGridView.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in loadGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in loadGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }

            loadGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> Company Name: " + ddlcompany.SelectedItem.Text +
                                 "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " +
                                 DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Interview Candidate Info List</h3>
   <span style=' font-size:15px;text-align:center'>Job Circulation: " + txt_JobCirculation.Text + " </span></span>";

       

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
             

            string style = @"<style> .text { mso-number-format:\@; } </style> ";
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
        
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
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
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}