using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Metadata;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Inverview_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using MKB.TimePicker;

public partial class Report_Pages_InterviewCandidateAttandance : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private InterviewCommonDAL _interviewCommonDal = new InterviewCommonDAL();
    private string _userId;
    private DropDownList ddlCompany;
    private TextBox txt_JobCirculation;
    //private TextBox txt_JobTitle;
    private HiddenField hfJobID;
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlCompany = (DropDownList)IVSearchControl.FindControl("ddlCompany");
        txt_JobCirculation = (TextBox)IVSearchControl.FindControl("txt_JobCirculation");
        //txt_JobTitle = (TextBox)IVSearchControl.FindControl("txt_JobTitle");
        hfJobID = (HiddenField)IVSearchControl.FindControl("hfJobID");
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
          //  UserPersmissionValidation();
            ButtonVisible();
            Session["cid"] = null;
            LoadInitialDDL();
            //SetInitialRow();
        }
    }
    public void UserPersmissionValidation()
    {
        try
        {
            PermissionDAL aPermissionDal = new PermissionDAL();

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

                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            AlertMessageBoxShow(ex.ToString());
        }
    }
    public void ButtonVisible()
    {
       // btn_Save.Visible = Convert.ToBoolean(ViewState["Add"].ToString());
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        btn_Save.Visible = true;
        //    }
        //    //else if (Session["Status"].ToString() == "Edit")
        //    //{
        //    //    editButton.Visible = true;
        //    //}
        //    //else if (Session["Status"].ToString() == "Delete")
        //    //{
        //    //    delButton.Visible = true;
        //    //}
        //    Session["Status"] = null;
        //}

    }

    private void LoadInitialDDL()
    {
        //using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        //{

        //    ddlCompany.DataSource = dt;
        //    ddlCompany.DataValueField = "Value";
        //    ddlCompany.DataTextField = "TextField";
        //    ddlCompany.DataBind();
        //}
    }
    //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Session["cid"] = ddlCompany.SelectedValue;
    //}
    //protected void txt_JobCirculation_OnTextChanged(object sender, EventArgs e)
    //{
    //    string Emp = txt_JobCirculation.Text;
    //    if (!string.IsNullOrEmpty(Emp) && Emp.Length > 5)
    //    {
    //        hfJobID.Value = Emp.Split(':')[0];
    //        txt_JobCirculation.Text = Emp.Split(':')[1];
    //        txt_JobTitle.Text = Emp.Split(':')[2];
    //    }
    //}

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("InterviewCandidateAttandance.aspx");
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                    if (chkSingle.Checked)
                    {

                        HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdpkd");
                        HiddenField hdCandidateID = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdCandidateID");
                        HiddenField hdCompanyId = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdCompanyId");
                        HiddenField hdJobID = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdJobID");

                        TimeSelector txt_InterviewTime = (TimeSelector)gv_InterviewCList.Rows[i].FindControl("txt_InterviewTime");
                        TextBox txt_Remarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_Remarks");
                        DateTime time = DateTime.Parse(string.Format("{0}:{1} {2}", txt_InterviewTime.Hour, txt_InterviewTime.Minute, txt_InterviewTime.AmPm));
                        int pkd = int.Parse(hdpkd.Value);
                        if (pkd > 0)////Edit Mode
                        {
                            tblInterviewCandidateAttandance ci = db.tblInterviewCandidateAttandances.FirstOrDefault(ici => ici.InterviewCandidateAttandanceId == pkd);

                            ci.JobID = int.Parse(hdJobID.Value);
                            ci.CandidateID = int.Parse(hdCandidateID.Value);
                            ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);
                            ci.InterviewDate = DateTime.Today;
                            ci.ReportingTime = time.TimeOfDay;
                            ci.Remarks = txt_Remarks.Text;
                            ci.UpdateBy = _userId;
                            ci.UpdateDate = DateTime.Now;
                            ci.IsActive = true;
                        }
                        else
                        {////New Mode
                            tblInterviewCandidateAttandance ci = new tblInterviewCandidateAttandance();
                            ci.JobID = int.Parse(hdJobID.Value);
                            ci.CandidateID = int.Parse(hdCandidateID.Value);
                            ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);
                            ci.InterviewDate = DateTime.Today;
                            ci.ReportingTime = time.TimeOfDay;
                            ci.Remarks = txt_Remarks.Text;
                            ci.IsActive = true;
                            ci.EntryBy = _userId;
                            ci.EntryDate = DateTime.Now;
                            db.tblInterviewCandidateAttandances.Add(ci);
                        }


                    }
                }
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='InterviewCandidateAttandance.aspx';",
                    true);
            }

        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
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
    protected void btn_LoadList_OnClick(object sender, EventArgs e)
    {
        string cid = ddlCompany.SelectedValue;
        string jobid = hfJobID.Value;

        if (Validation())
        {
            using (DataTable dts = _interviewCommonDal.GetIVCandidateForAttandanceByParam(GetPaeraMeter()))
            {
                if (dts.Rows.Count > 0)
                {
                    gv_InterviewCList.DataSource = dts;
                    gv_InterviewCList.DataBind();

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        string ReportingTime = dts.Rows[i]["ReportingTime"].ToString();
                        if (!string.IsNullOrEmpty(ReportingTime))
                        {
                            TimeSelector txt_InterviewTime = (TimeSelector)gv_InterviewCList.Rows[i].FindControl("txt_InterviewTime");

                            DateTime dt = DateTime.Parse(ReportingTime.Replace(".", ":"));
                            TimeSelector.AmPmSpec am_pm;
                            if (dt.ToString("tt") == "AM")
                            {
                                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                            }
                            else
                            {
                                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                            }
                            txt_InterviewTime.SetTime(dt.Hour, dt.Minute, am_pm);


                            CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                            chkSingle.Checked = true;

                        }

                    }
                }
                else
                {
                    gv_InterviewCList.DataSource = null;
                    gv_InterviewCList.DataBind();
                    AlertMessageBoxShow("No matching data found...!");
                }

            }
        }
       
    }

    private string GetPaeraMeter()
    {
        string param = " ";
        if (ddlCompany.SelectedValue != "")
        {
            param = param + "    ic.CompanyId =  '" + ddlCompany.SelectedValue + "' ";
        }

        if (hfJobID.Value != "")
        {
            param = param + " AND  ic.JobID=  '" + hfJobID.Value + "' ";
        }

        return param;
    }

    private bool Validation()
    {
        if (ddlCompany.SelectedIndex <=0)
        {
            aShowMessage.ShowMessageBox("Please Select Company!!", this);
            ddlCompany.Focus();
            return false;
        }

        if (hfJobID.Value == "" && txt_JobCirculation.Text.Trim()=="")
        {
            aShowMessage.ShowMessageBox("Please Enter Job Circulation!!", this);
            txt_JobCirculation.Focus();
            return false;
        }
 

        return true;
    }

    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        //GridViewRow gvRow = (GridViewRow)cb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_InterviewCList.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Interview_Candidate_Attandance_Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv_InterviewCList.AllowPaging = false;

             string cid = ddlCompany.SelectedValue;
        string jobid = hfJobID.Value;

            using (DataTable dts = _interviewCommonDal.GetIVCandidateForAttandance(cid, "1"))
            {
                if (dts.Rows.Count > 0)
                {
                    gv_InterviewCList.DataSource = dts;
                    gv_InterviewCList.DataBind();

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        string ReportingTime = dts.Rows[i]["ReportingTime"].ToString();
                        if (!string.IsNullOrEmpty(ReportingTime))
                        {
                            TimeSelector txt_InterviewTime =
                                (TimeSelector) gv_InterviewCList.Rows[i].FindControl("txt_InterviewTime");

                            DateTime dt = DateTime.Parse(ReportingTime.Replace(".", ":"));
                            TimeSelector.AmPmSpec am_pm;
                            if (dt.ToString("tt") == "AM")
                            {
                                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                            }
                            else
                            {
                                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                            }
                            txt_InterviewTime.SetTime(dt.Hour, dt.Minute, am_pm);


                            CheckBox chkSingle = (CheckBox) gv_InterviewCList.Rows[i].FindControl("chkSingle");
                            chkSingle.Checked = true;

                        }

                    }
                }
            }

            gv_InterviewCList.Columns[1].Visible =
                        false;
            //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
            //   false;
            //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
            //   false;
 

            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            gv_InterviewCList.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            gv_InterviewCList.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_InterviewCList.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_InterviewCList.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }

            gv_InterviewCList.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text +
                                 "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " +
                                 DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Interview Candidate Attandance Info List</h3>

</span>";

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
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }  
}