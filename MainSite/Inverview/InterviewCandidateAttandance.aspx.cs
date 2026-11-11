using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IdentityModel.Metadata;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Inverview_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using MKB.TimePicker;

public partial class Inverview_InterviewCandidateAttandance : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private InterviewCommonDAL _interviewCommonDal = new InterviewCommonDAL();
    private string _userId;
    private DropDownList ddlCompany;
    private DropDownList ddlJobCirculation;
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlCompany = (DropDownList)IVSearchControl.FindControl("ddlCompany");
        ddlJobCirculation = (DropDownList)IVSearchControl.FindControl("ddlJobCirculation") as DropDownList;
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            UserPersmissionValidation();
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
        try
        {
            btn_Save.Visible = Convert.ToBoolean(ViewState["Add"].ToString());
        }
        catch (Exception)
        {
            
            //throw;
        }
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

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private bool Validation()
    {


        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlCompany.Focus();
            return false;
        }

        if (ddlJobCirculation.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlJobCirculation.Focus();
            return false;
        }

        if (gv_InterviewCList.Rows.Count < 0)
        {
            aShowMessage.ShowMessageBox("Interview Candidate List", this);
           
            return false;
        }


        int totalCount = gv_InterviewCList.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkSingle")).Checked);

        if (totalCount == 0)
        {

            aShowMessage.ShowMessageBox("Please Select At Least One Candidate", this);
            return false;
        }


        return true;
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        { 
            if (Validation())
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

                            if ((!CheckEmpDepartmentAllocateOrNot(hdJobID.Value.ToString())) && (!CheckEmpDepartmentAllocateOrNotViva(hdJobID.Value.ToString()) )== true)
                            {
                                
                            
                            
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

                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                   "alert",
                                   "alert('Can Not be Inserted!!!');",
                                   true);

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

        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    private bool CheckEmpDepartmentAllocateOrNot(string departmentId)
    {
        bool status = false;

        DataTable dataTable = aCommonDAL.WritMarksEntryByJobIdForAttend(departmentId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }


    private bool CheckEmpDepartmentAllocateOrNotViva(string departmentId)
    {
        bool status = false;

        DataTable dataTable = aCommonDAL.VivaMarksEntryByJobIdForAttend(departmentId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }



    ValidationDeleteCommonDAL aCommonDAL = new ValidationDeleteCommonDAL();
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
        if (vali())
        {
            string cid = ddlCompany.SelectedValue;
            string jobid = ddlJobCirculation.SelectedValue;

            using (DataTable dts = _interviewCommonDal.GetIVCandidateForAttandance(cid, jobid))
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
                            gv_InterviewCList.Rows[i].Cells[0].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[1].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[2].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[3].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[4].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[5].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[6].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[7].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[8].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[9].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[10].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[11].BackColor = Color.Coral;
                           
                        }

                    }
                }
                else
                {
                    gv_InterviewCList.DataSource = null;
                    gv_InterviewCList.DataBind();
                    AlertMessageBoxShow("No matching data found...!");
                }

                //for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
                //{
                //    TextBox txt_Remarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_Remarks");
                //    txt_Remarks.Text = txt_remarks.Text.Trim();
                //}

            }
        }
    }
    private bool vali()
    {
        if (ddlCompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company!!", this);
            ddlCompany.Focus();
            return false;
        }
        if (ddlJobCirculation.SelectedValue == "")
        {
            AlertMessageBoxShow("Please Select Job Circulation !!!");
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

    protected void txt_remarks_OnTextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
        {
            TextBox txt_Remarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_Remarks");
            txt_Remarks.Text = txt_remarks.Text.Trim();
        }
    }
}