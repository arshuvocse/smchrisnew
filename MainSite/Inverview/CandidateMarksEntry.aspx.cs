using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Inverview_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO_EF;

public partial class Inverview_CandidateMarksEntry : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private InterviewCommonDAL _interviewCommonDal = new InterviewCommonDAL();
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            UserPersmissionValidation();
            ButtonVisible();
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
        btn_Save.Visible = Convert.ToBoolean(ViewState["Add"].ToString());
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
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }

        //using (DataTable dt = _commonDataLoad.GetInterviewActivity())
        //{

        //    rad_InterviewActivity.DataSource = dt;
        //    rad_InterviewActivity.DataValueField = "Value";
        //    rad_InterviewActivity.DataTextField = "TextField";
        //    rad_InterviewActivity.DataBind();
        //}
    }
    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("BoardDetailsId", typeof(string)));
        dt.Columns.Add(new DataColumn("MemberType", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpMasterCode", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpName", typeof(string)));
        dt.Columns.Add(new DataColumn("Designation", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("CompanyName", typeof(string)));
        dt.Columns.Add(new DataColumn("Email", typeof(string)));
        dt.Columns.Add(new DataColumn("CellNumber", typeof(string)));

        dr = dt.NewRow();
        dr["EmpMasterCode"] = "";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;

        //Bind the Gridview   
        gv_InterviewCList.DataSource = dt;
        gv_InterviewCList.DataBind();

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
    }

    protected void txt_JobCirculation_OnTextChanged(object sender, EventArgs e)
    {
        string Emp = txt_JobCirculation.Text;
        if (!string.IsNullOrEmpty(Emp) && Emp.Length > 5)
        {
            hdJobID.Value = Emp.Split(':')[0];
            txt_JobCirculation.Text = Emp.Split(':')[1];
            txt_JobTitle.Text = Emp.Split(':')[2];
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("InterviewCandidateInvitation.aspx");
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
                        HiddenField hdJobID = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdJobID");
                        
                        TextBox txt_WrittenMarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_WrittenMarks");
                        TextBox txt_WrittenMarksOutOf = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_WrittenMarksOutOf");
                        TextBox txt_VivaMarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_VivaMarks");
                        TextBox txt_VivaMarksOutOf = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_VivaMarksOutOf");
                        TextBox txt_OtherMarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_OtherMarks");
                        TextBox txt_OtherMarksOutOf = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_OtherMarksOutOf");





                        int pkd = int.Parse(hdpkd.Value);
                        if (pkd > 0)////Edit Mode
                        {
                            tblInterviewCandidateTotalMark ci = db.tblInterviewCandidateTotalMarks.FirstOrDefault(ici => ici.InterviewCandidateTotalMarksId == pkd);

                            ci.CandidateID = int.Parse(hdCandidateID.Value);
                            ci.JobID = int.Parse(hdJobID.Value);
                            ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);

                            ci.WrittenMarks = decimal.Parse(txt_WrittenMarks.Text);
                            ci.WrittenMarksOutOf = decimal.Parse(txt_WrittenMarksOutOf.Text);

                            ci.VivaMarks = decimal.Parse(txt_VivaMarks.Text);
                            ci.VivaMarksOutOf = decimal.Parse(txt_VivaMarksOutOf.Text);

                            ci.OtherMarks = decimal.Parse(txt_OtherMarks.Text);
                            ci.OtherMarksOutOf = decimal.Parse(txt_OtherMarksOutOf.Text);


                            ci.UpdateBy = _userId;
                            ci.UpdateDate = DateTime.Now;
                        }
                        else
                        {////New Mode
                            tblInterviewCandidateTotalMark ci = new tblInterviewCandidateTotalMark();
                            ci.CandidateID = int.Parse(hdCandidateID.Value);
                            ci.JobID = int.Parse(hdJobID.Value);
                            ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);

                            ci.WrittenMarks = decimal.Parse(txt_WrittenMarks.Text);
                            ci.WrittenMarksOutOf = decimal.Parse(txt_WrittenMarksOutOf.Text);

                            ci.VivaMarks = decimal.Parse(txt_VivaMarks.Text);
                            ci.VivaMarksOutOf = decimal.Parse(txt_VivaMarksOutOf.Text);

                            ci.OtherMarks = decimal.Parse(txt_OtherMarks.Text);
                            ci.OtherMarksOutOf = decimal.Parse(txt_OtherMarksOutOf.Text);
                            ci.EntryBy = _userId;
                            ci.EntryDate = DateTime.Now;
                            db.tblInterviewCandidateTotalMarks.Add(ci);
                        }


                    }
                }
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='CandidateMarksEntry.aspx';",
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
        string jobid = hdJobID.Value;
        string InterviewPhase = ddlInterviewPhase.SelectedValue;

        using (DataTable dt = _interviewCommonDal.GetIVCandidateForMarksEntry(cid, jobid, InterviewPhase))
        {
            if (dt.Rows.Count>0)
            {
                gv_InterviewCList.DataSource = dt;
                gv_InterviewCList.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");

                    chkSingle.Checked = int.Parse(dt.Rows[i]["InterviewCandidateTotalMarksId"].ToString()) > 0;
                } 
            }
            else
            {
                gv_InterviewCList.DataSource = null;
                gv_InterviewCList.DataBind();
                AlertMessageBoxShow("No Candidate found for this search...");
            }
            
        }
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

    protected void chkAllPhone_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSinglePhone = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSinglePhone");
                chkSinglePhone.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSinglePhone = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSinglePhone");
                chkSinglePhone.Checked = false;
            }
        }
    }

    protected void chkAllEmail_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        //GridViewRow gvRow = (GridViewRow)cb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleEmail = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleEmail");
                chkSingleEmail.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleEmail = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleEmail");
                chkSingleEmail.Checked = false;
            }
        }
    }

    protected void chkAllSMS_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        //GridViewRow gvRow = (GridViewRow)cb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleSMS = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleSMS");
                chkSingleSMS.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleSMS = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleSMS");
                chkSingleSMS.Checked = false;
            }
        }
    }
}