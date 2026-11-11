using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Inverview_InterviewBoardSetupList : System.Web.UI.Page
{

    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ValidationDeleteCommonDAL aCommonDAL = new ValidationDeleteCommonDAL();
   
    private DropDownList ddlcompany;
    private DropDownList ddlJobCirculation;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ddlcompany = (DropDownList)IVSearchControl.FindControl("ddlCompany") as DropDownList;
        ddlJobCirculation = (DropDownList)IVSearchControl.FindControl("ddlJobCirculation") as DropDownList;
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();
        //    SetInitialRow();
        }
    }

    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();

        try
        {
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

                    btn_NewInterviewBoardSetup.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    gv_InterviewBoardMember.Columns[gv_InterviewBoardMember.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_InterviewBoardMember.Columns[gv_InterviewBoardMember.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_InterviewBoardMember.Columns[gv_InterviewBoardMember.Columns.Count - 3].Visible =
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

    private void SetInitialRow()
    {
        if (Validation())
        {int idJobID = Convert.ToInt32(ddlJobCirculation.SelectedValue);
            DataTable dt = new DataTable();
            using (var db = new HRIS_SMCEntities())
            {
                
               
                var joblist = (from i in db.tblInterviewBoardSetupMasters
                    join j in db.tblJobCreations on i.JobTitleId equals (int) j.JobID
                    join c in db.tblCompanyInfoes on i.CompanyId equals c.CompanyId
                               where i.IsActive == true  && j.JobID == idJobID
                    select new {i.SetupMasterId, j.JobCode, j.Position, c.ShortName, i.InterviewDate, i.InterviewFromTime, i.InterviewToTime}).ToList();
                dt = ToDataTable(joblist);


                if (dt.Rows.Count> 0)
                {
                    gv_InterviewBoardMember.DataSource = dt;
                    gv_InterviewBoardMember.DataBind();
                }
                else
                {
                    gv_InterviewBoardMember.DataSource = null;
                    gv_InterviewBoardMember.DataBind();

                    aShowMessage.ShowMessageBox("No data found !!!", this);
                }
            }
          
        }
    }

    private bool Validation()
    {
        if (ddlcompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company!!", this);
            ddlcompany.Focus();
            return false;
        }

        if (ddlJobCirculation.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Job Circulation!!", this);
            ddlJobCirculation.Focus();
            return false;
        }


        return true;
    }
    private DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdpk = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpk");
        DataTable aDataTable = new DataTable();
        aDataTable = aCommonDAL.LoadBoardSetupByMasterId(hdpk.Value);
        if (aDataTable.Rows.Count > 0)
        {
            if (aDataTable.Rows[0]["ApprovalStatus"].ToString() != "Submitted")
            {

                Session["Status"] = "Edit";
                Response.Redirect("InterviewBoardSetup.aspx?mid=" + hdpk.Value);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Can Not be Edited!!!');",
               true);

            }
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

    protected void lb_Remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

       

        HiddenField hdpk = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpk");
        DataTable aDataTable = new DataTable();
        aDataTable = aCommonDAL.LoadBoardSetupByMasterId(hdpk.Value);
         if (aDataTable.Rows.Count > 0)
        {
            if (aDataTable.Rows[0]["ApprovalStatus"].ToString() != "Submitted")
            {
 Session["Status"] = "Delete";
                Response.Redirect("InterviewBoardSetup.aspx?mid=" + hdpk.Value);
                
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Can Not be Deleted!!!');",
               true);

            }

        }
         
            //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;
        //HiddenField hdpk = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpk");
        //int pk = Int32.Parse(hdpk.Value);

        //var db = new HRIS_SMCEntities();
        //tblInterviewBoardSetupMaster IVMaster = (from emd in db.tblInterviewBoardSetupMasters where emd.SetupMasterId == pk select emd).FirstOrDefault();
        //IVMaster.IsActive = false;
        ////db.tblInterviewBoardSetupMasters.Add(IVMaster);
        //db.SaveChanges();

        //SetInitialRow();
    }

    protected void btn_NewInterviewBoardSetup_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("InterviewBoardSetup.aspx");
    }

    protected void lb_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";

        HiddenField hdpk = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpk");

        Response.Redirect("InterviewBoardSetup.aspx?mid=" + hdpk.Value);

    }

    protected void btn_LoadList_OnClick(object sender, EventArgs e)
    {
        SetInitialRow();
    }

    protected void lb_Print_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";

        HiddenField hdpk = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpk");

        PopUp(Convert.ToInt32(hdpk.Value));
    }

    private void PopUp(Int32 jobReqId)
    {
        string url = "../Report_UI/InterviewBoardSetupReportViwer.aspx?rptType=" + jobReqId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void lb_SendMail_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "SendMail";

        HiddenField hdpk = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpk");
        Response.Redirect("InterviewBoardSetupSendMAil.aspx?mid=" + hdpk.Value);
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