using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Inverview_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Inverview_InterviewBoardSetup : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private InterviewCommonDAL _interviewCommonDAL = new InterviewCommonDAL();

    private int mid = 0;
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
            string compId = "";
            txt_InterviewDate.Attributes.Add("readonly", "readonly");
            ButtonVisible();
            Session["cid"] = null;
            Session["EmpOption"] = "1";
            LoadInitialDDL();

            Session["FinYearId"] = "";
            Session["DepartmentId"] = "";
            Session["startDate"] = "";
            Session["endDate"] = "";
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var IVMaster = (from ivm in db.tblInterviewBoardSetupMasters
                                        join jb in db.tblJobCreations on (int)ivm.JobTitleId equals jb.JobID
                                        where ivm.SetupMasterId == mid
                                        select new
                                        {
                                            ivm.CompanyId,
                                            jb.JobCode,
                                            jb.Position,
                                            jb.JobID,
                                            ivm.EmailBody,
                                            ivm.Remarks,
                                        
                                ivm.Vanue,
                                ivm.InterviewDate,
                                ivm.InterviewFromTime,
                                ivm.InterviewToTime,
                                ivm.InterviewPhase
                            }).FirstOrDefault();

                        ddlCompany.SelectedValue = IVMaster.CompanyId.ToString();
                        compId = IVMaster.CompanyId.ToString();
                        
                        Session["cid"] = IVMaster.CompanyId;
                        ddlJobCirculation.SelectedValue = IVMaster.JobID.ToString();
                        try
                        {
                            ddlJobCirculation.SelectedItem.Text = IVMaster.Position;
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }
                        //txt_JobTitle.Text = IVMaster.Position;
                        txt_EmailBody.Text = IVMaster.EmailBody;
                        txt_InterviewMasterRemarks.Text = IVMaster.Remarks;
                        
                        ddlInterviewPhase.SelectedValue = IVMaster.InterviewPhase.ToString();
                        txt_InterviewVenue.Text = IVMaster.Vanue;
                        txt_InterviewDate.Text = IVMaster.InterviewDate.Value.ToString("dd-MMM-yyyy");

                        if (!string.IsNullOrEmpty(IVMaster.InterviewFromTime))
                        {
                            DateTime dtF = DateTime.Parse(IVMaster.InterviewFromTime.Replace(".", ":"));
                            MKB.TimePicker.TimeSelector.AmPmSpec am_pmF;
                            if (dtF.ToString("tt") == "AM")
                            {
                                am_pmF = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                            }
                            else
                            {
                                am_pmF = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                            }
                            txt_InterviewFromTime.SetTime(dtF.Hour, dtF.Minute, am_pmF);
                        }

                        if (!string.IsNullOrEmpty(IVMaster.InterviewToTime))
                        {
                            DateTime dtT = DateTime.Parse(IVMaster.InterviewToTime.Replace(".", ":"));
                            MKB.TimePicker.TimeSelector.AmPmSpec am_pmT;
                            if (dtT.ToString("tt") == "AM")
                            {
                                am_pmT = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
                            }
                            else
                            {
                                am_pmT = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                            }
                            txt_InterviewToTime.SetTime(dtT.Hour, dtT.Minute, am_pmT);
                        }


                        using (DataTable dtg = _interviewCommonDAL.GetIVBoardSetupDtlByMId(mid))
                        {
                            if (dtg.Rows.Count>0)
                            {
                                ViewState["CurrentTable"] = dtg;
                                gv_InterviewBoardMember.DataSource = dtg;
                                gv_InterviewBoardMember.DataBind();

                                for (int i = 0; i < dtg.Rows.Count; i++)
                                {
                                    DropDownList ddlEmpOption = (DropDownList)gv_InterviewBoardMember.Rows[i].FindControl("ddlEmpOption");
                                    ddlEmpOption.SelectedValue = dtg.Rows[i]["MemberType"].ToString();
                                    TextBox txt_ActivityOther = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_ActivityOther");
                                    CheckBoxList lchk_InterviewActivity = (CheckBoxList)gv_InterviewBoardMember.Rows[i].FindControl("lchk_InterviewActivity");
                                    using (DataTable dtA = _commonDataLoad.GetInterviewActivity())
                                    {
                                        lchk_InterviewActivity.DataSource = dtA;
                                        lchk_InterviewActivity.DataValueField = "Value";
                                        lchk_InterviewActivity.DataTextField = "TextField";
                                        lchk_InterviewActivity.DataBind();
                                    }
                                    foreach (ListItem item in lchk_InterviewActivity.Items)
                                    {
                                        if (int.Parse(item.Value) == 1)
                                        {
                                            item.Selected = bool.Parse(dtg.Rows[i]["Written"].ToString());
                                        }
                                        if (int.Parse(item.Value) == 2)
                                        {
                                            item.Selected = bool.Parse(dtg.Rows[i]["Viva"].ToString());
                                        }
                                        if (int.Parse(item.Value) == 4)
                                        {
                                            item.Selected = bool.Parse(dtg.Rows[i]["Other"].ToString());
                                            txt_ActivityOther.Text = dtg.Rows[i]["OtherRemarks"].ToString();
                                            txt_ActivityOther.Visible = true;
                                        }
                                    }
                                    string selectedItems = String.Join(",",
                                        lchk_InterviewActivity.Items.OfType<ListItem>().Where(r => r.Selected)
                                            .Select(r => r.Value));
                                    dtg.Rows[i]["InterviewActivity"] = selectedItems;
                                }
                            }
                            ViewState["CurrentTable"] = dtg;
                        }
                    }
                    DataTable dtvibadata = _interviewCommonDAL.GetVivaInformationForEdit2(mid.ToString());
                    if (dtvibadata.Rows.Count>0)
                    {
                        writtenCheckBox.Checked = Convert.ToBoolean(dtvibadata.Rows[0]["IsWritten"].ToString());
                        otherCheckBox.Checked = Convert.ToBoolean(dtvibadata.Rows[0]["IsOther"].ToString());
                        vivaCheckBox.Checked = Convert.ToBoolean(dtvibadata.Rows[0]["IsViva"].ToString());
                        writtenTextBox.Text = dtvibadata.Rows[0]["WrittenMarks"].ToString();
                        otherMarksTextBox.Text = dtvibadata.Rows[0]["OtherMarks"].ToString();
                        grid.Visible = vivaCheckBox.Checked;
                        other.Visible = otherCheckBox.Checked;
                        written.Visible = writtenCheckBox.Checked;
                    }

                    DataTable dtviva = _interviewCommonDAL.GetVivaInformationForEdit(mid.ToString(),compId);
                    if (dtviva.Rows.Count>0)
                    {
                        
                        GridView1.DataSource = dtviva;
                        GridView1.DataBind();
                    }

                }
            }
            else
            {
                SetInitialRow();
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
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
                orBTN.Visible = true;
                btnSubmit.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;

                orUp.Visible = true;
                btnUpdateforSubmit.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("InterviewBoardSetupList.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
        //using (DataTable dt = _commonDataLoad.GetInterviewActivity())
        //{

        //    lchk_InterviewActivity.DataSource = dt;
        //    lchk_InterviewActivity.DataValueField = "Value";
        //    lchk_InterviewActivity.DataTextField = "TextField";
        //    lchk_InterviewActivity.DataBind();
        //}
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
    }

    //protected void btn_CopyIVInfo_OnClick(object sender, EventArgs e)
    //{
    //    //if (ddlCompany.SelectedIndex <= 0)
    //    //{
    //    //    radInterviewActivity.ClearSelection();
    //    //    AlertMessageBoxShow("Company required...");
    //    //    return;
    //    //}
    //    //if (string.IsNullOrEmpty(txt_JobCirculation.Text))
    //    //{
    //    //    radInterviewActivity.ClearSelection();
    //    //    AlertMessageBoxShow("Job Circulation required...");
    //    //    return;
    //    //}
    //    int cid = int.Parse(ddlCompany.SelectedValue);
    //    long JobId = 0;
    //    int InterviewActivity = 1;//int.Parse(radInterviewActivity.SelectedValue);
    //    using (var db = new HRIS_SMCEntities())
    //    {
    //        tblJobCreation job = (from j in db.tblJobCreations where j.JobCode.Equals(txt_JobCirculation.Text) select j).FirstOrDefault();
    //        if (job != null)
    //        {
    //            JobId = job.JobID;
    //        }
    //    }
    //    int InterviewPhase = 1;//string.IsNullOrEmpty(txt_InterviewPhase.Text) ? 1 : int.Parse(txt_InterviewPhase.Text);
    //    if (InterviewPhase>1)
    //    {
    //        using (DataTable dt = _interviewCommonDAL.CopyIVFromPreviousPhase(cid, JobId, InterviewActivity,InterviewPhase-1))
    //        {
    //            int SetupMasterId = int.Parse(dt.Rows[0]["SetupMasterId"].ToString());
    //            ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
    //            txt_JobCirculation.Text = dt.Rows[0]["Position"].ToString();
    //            //txt_JobTitle.Text = dt.Rows[0]["Position"].ToString();
    //            //radInterviewActivity.SelectedValue = dt.Rows[0]["InterviewActivity"].ToString();
    //            //txt_InterviewPhase.Text = (int.Parse( dt.Rows[0]["InterviewPhase"].ToString())+1).ToString();
    //            txt_InterviewVenue.Text = dt.Rows[0]["Vanue"].ToString();
    //            txt_InterviewDate.Text = DateTime.Parse(dt.Rows[0]["InterviewDate"].ToString()).ToString("dd-MMM-yyyy");
    //            DateTime dbtime = DateTime.Parse(dt.Rows[0]["InterviewTime"].ToString().Replace(".", ":"));
    //            MKB.TimePicker.TimeSelector.AmPmSpec am_pm;
    //            if (dbtime.ToString("tt") == "AM")
    //            {
    //                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.AM;
    //            }
    //            else
    //            {
    //                am_pm = MKB.TimePicker.TimeSelector.AmPmSpec.PM;
    //            }
    //            txt_InterviewTime.SetTime(dbtime.Hour, dbtime.Minute, am_pm);
    //            using (DataTable dtg = _interviewCommonDAL.GetIVBoardSetupDtlByMId(SetupMasterId))
    //            {
    //                ViewState["CurrentTable"] = dtg;
    //                gv_InterviewBoardMember.DataSource = dtg;
    //                gv_InterviewBoardMember.DataBind();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //AlertMessageBoxShow("This is the 1st Phase for "+radInterviewActivity.SelectedItem.Text+" of job circulation: "+txt_JobCirculation.Text);
    //    }
    //}
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    public bool VivaValidation()
    {
        
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("txt_check");
            TextBox txt_outof = (TextBox)GridView1.Rows[i].FindControl("txt_outof");
            if (chk.Checked && !string.IsNullOrEmpty(txt_outof.Text))
            {
                //AlertMessageBoxShow("Input Marks In The Checked Rows ");
                return true;
            }
        }
        return false;
        
    }

    public void SaveInterViewVivaSetup(int mid)
    {
        List<InterviewBoardMarksSetupDAO> aMarksSetupDao = new List<InterviewBoardMarksSetupDAO>();
        if (vivaCheckBox.Checked==false)
        {
            InterviewBoardMarksSetupDAO aBoardMarksSetupDao=new InterviewBoardMarksSetupDAO();
            aBoardMarksSetupDao.SetupMasterId = mid;
            aBoardMarksSetupDao.IsOther = otherCheckBox.Checked;
            aBoardMarksSetupDao.IsViva = vivaCheckBox.Checked;
            aBoardMarksSetupDao.IsWritten= writtenCheckBox.Checked;
            if (otherCheckBox.Checked)
            {
                aBoardMarksSetupDao.OtherMarks = Convert.ToDecimal(otherMarksTextBox.Text);    
            }
            if (writtenCheckBox.Checked)
            {
                aBoardMarksSetupDao.WrittenMarks = Convert.ToDecimal(writtenTextBox.Text);    
                
            }
            aMarksSetupDao.Add(aBoardMarksSetupDao);

            _interviewCommonDAL.SaveBoardMemberVivaSetup(aMarksSetupDao,mid);

        }
        else
        {
            if (VivaValidation())
            {


                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chk = (CheckBox) GridView1.Rows[i].FindControl("txt_check");
                    TextBox txt_outof = (TextBox) GridView1.Rows[i].FindControl("txt_outof");
                    if (chk.Checked)
                    {


                        InterviewBoardMarksSetupDAO aBoardMarksSetupDao = new InterviewBoardMarksSetupDAO();
                        aBoardMarksSetupDao.SetupMasterId = mid;
                        aBoardMarksSetupDao.IsOther = otherCheckBox.Checked;
                        aBoardMarksSetupDao.IsViva = vivaCheckBox.Checked;
                        aBoardMarksSetupDao.IsWritten = writtenCheckBox.Checked;
                        if (otherCheckBox.Checked)
                        {
                            aBoardMarksSetupDao.OtherMarks = Convert.ToDecimal(otherMarksTextBox.Text);
                        }
                        if (writtenCheckBox.Checked)
                        {
                            aBoardMarksSetupDao.WrittenMarks = Convert.ToDecimal(writtenTextBox.Text);

                        }

                        aBoardMarksSetupDao.VivaId = Convert.ToInt32(GridView1.DataKeys[i][0].ToString());
                        aBoardMarksSetupDao.VivaMarks = Convert.ToDecimal(txt_outof.Text);

                        aMarksSetupDao.Add(aBoardMarksSetupDao);

                        
                    }

                }
                _interviewCommonDAL.SaveBoardMemberVivaSetup(aMarksSetupDao, mid);
            }
        }
        
        
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (!CheckFatherOccupationAllocateOrNot(ddlJobCirculation.SelectedValue))
        {
            string ApprovalStatus = "Posted";
            Submit(ApprovalStatus);
        }

        else
        {
            AlertMessageBoxShow("Board Member Already Exist !!!");
        }
    }



    protected void ddlEmpOption_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
        DropDownList ddlEmpOption = (DropDownList)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("ddlEmpOption");
        Session["EmpOption"] = ddlEmpOption.SelectedValue;
    }



    protected void btn_AddRow_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("BoardDetailsId", typeof(string)));
        dt.Columns.Add(new DataColumn("MemberType", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpMasterCode", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpName", typeof(string)));
        dt.Columns.Add(new DataColumn("Designation", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("CompanyName", typeof(string)));
        dt.Columns.Add(new DataColumn("OfficialEmail", typeof(string)));
        dt.Columns.Add(new DataColumn("EmailBody", typeof(string)));
        dt.Columns.Add(new DataColumn("OfficialMobile", typeof(string)));
        dt.Columns.Add(new DataColumn("InterviewActivity", typeof(string)));
        dt.Columns.Add(new DataColumn("OtherRemarks", typeof(string)));

        dr = dt.NewRow();
        dr["EmpMasterCode"] = "";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;

        //Bind the Gridview   
        gv_InterviewBoardMember.DataSource = dt;
        gv_InterviewBoardMember.DataBind();

    }

    private void AddNewRowToGrid()
    {

        if (ViewState["CurrentTable"] != null)
        {

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                //drCurrentRow["SL"] = dtCurrentTable.Rows.Count + 1;

                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   

                ViewState["CurrentTable"] = dtCurrentTable;


                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {

                    ////extract the TextBox values   
                    HiddenField hdpkd = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdpkd");
                    HiddenField hdEmpInfoId = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdEmpInfoId");
                    TextBox txt_EmpID = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpID");
                    TextBox txt_EmpName = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpName");
                    TextBox txt_EmpDesignation = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpDesignation");
                    TextBox txt_EmpDepartment = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpDepartment");
                    TextBox txt_EmpCompany = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpCompany");
                    TextBox txt_EmpEmail = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpEmail");
                    TextBox txt_EmpPhone = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpPhone");

                    if (hdpkd.Value != string.Empty)
                    {
                        dtCurrentTable.Rows[i]["BoardDetailsId"] = hdpkd.Value;
                    }
                    else
                    {
                        dtCurrentTable.Rows[i]["BoardDetailsId"] = 0;
                    }

                   
                    dtCurrentTable.Rows[i]["EmpMasterCode"] = txt_EmpID.Text;
                   if (hdEmpInfoId.Value!=string.Empty)
                    {
                        dtCurrentTable.Rows[i]["EmpInfoId"] = hdEmpInfoId.Value;
                    }
                   else
                   {
                       dtCurrentTable.Rows[i]["EmpInfoId"] = 0;
                   }
                   

                    dtCurrentTable.Rows[i]["EmpName"] = txt_EmpName.Text;
                    dtCurrentTable.Rows[i]["Designation"] = txt_EmpDesignation.Text;
                    dtCurrentTable.Rows[i]["DepartmentName"] = txt_EmpDepartment.Text;
                    dtCurrentTable.Rows[i]["CompanyName"] = txt_EmpCompany.Text;
                    dtCurrentTable.Rows[i]["OfficialEmail"] = txt_EmpEmail.Text;
                    //dtCurrentTable.Rows[i]["EmailBody"] = txt_EmpEmail.Text;
                    dtCurrentTable.Rows[i]["OfficialMobile"] = txt_EmpPhone.Text;

                    ////extract the DropDownList Selected Items   
                    DropDownList ddlEmpOption = (DropDownList)gv_InterviewBoardMember.Rows[i].FindControl("ddlEmpOption");
                    dtCurrentTable.Rows[i]["MemberType"] = ddlEmpOption.SelectedValue;

                    CheckBoxList lchk_InterviewActivity = (CheckBoxList)gv_InterviewBoardMember.Rows[i].FindControl("lchk_InterviewActivity");
                    TextBox txt_ActivityOther = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_ActivityOther");

                    string selectedItems = String.Join(",",
                        lchk_InterviewActivity.Items.OfType<ListItem>().Where(r => r.Selected)
                            .Select(r => r.Value));
                    //foreach (ListItem item in lchk_InterviewActivity.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        dtCurrentTable.Rows[i]["InterviewActivity"] = item.Value;
                    //    }
                    //}
                    
                    dtCurrentTable.Rows[i]["InterviewActivity"] = selectedItems;
                    dtCurrentTable.Rows[i]["OtherRemarks"] = txt_ActivityOther.Text;
                }

                //Rebind the Grid with the current data to reflect changes   
                gv_InterviewBoardMember.DataSource = dtCurrentTable;
                gv_InterviewBoardMember.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");

        }
        //Set Previous Data on Postbacks   
        SetPreviousDataAdd();
    }

    private void SetPreviousDataAdd()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlEmpOption = (DropDownList)gv_InterviewBoardMember.Rows[rowIndex].FindControl("ddlEmpOption");
                    HiddenField hdpkd = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdpkd");
                    HiddenField hdEmpInfoId = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdEmpInfoId");
                    TextBox txt_EmpID = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpID");
                    TextBox txt_EmpName = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpName");
                    TextBox txt_EmpDesignation = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpDesignation");
                    TextBox txt_EmpDepartment = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpDepartment");
                    TextBox txt_EmpCompany = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpCompany");
                    TextBox txt_EmpEmail = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpEmail");
                    TextBox txt_EmpPhone = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpPhone");

                    CheckBoxList lchk_InterviewActivity = (CheckBoxList)gv_InterviewBoardMember.Rows[rowIndex].FindControl("lchk_InterviewActivity");
                    TextBox txt_ActivityOther = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_ActivityOther");
                    if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox   
                        hdpkd.Value = dt.Rows[i]["BoardDetailsId"].ToString();
                        txt_EmpID.Text = dt.Rows[i]["EmpMasterCode"].ToString();
                        hdEmpInfoId.Value = dt.Rows[i]["EmpInfoId"].ToString();
                        txt_EmpName.Text = dt.Rows[i]["EmpName"].ToString();
                        txt_EmpDesignation.Text = dt.Rows[i]["Designation"].ToString();
                        txt_EmpDepartment.Text = dt.Rows[i]["DepartmentName"].ToString();
                        txt_EmpCompany.Text = dt.Rows[i]["CompanyName"].ToString();
                        txt_EmpEmail.Text = dt.Rows[i]["OfficialEmail"].ToString();
                        txt_EmpPhone.Text = dt.Rows[i]["OfficialMobile"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        ddlEmpOption.ClearSelection();
                        //ddlEmpOption.Items.FindByText(dt.Rows[i]["MemberType"].ToString()).Selected = true;
                        ddlEmpOption.Items.FindByValue(dt.Rows[i]["MemberType"].ToString()).Selected = true;

                        lchk_InterviewActivity.ClearSelection();
                        List<string> stringList = dt.Rows[i]["InterviewActivity"].ToString().Split(',').ToList();

                        foreach(string str in stringList)
                        {
                            if (!string.IsNullOrEmpty(str))
                            {
                                lchk_InterviewActivity.Items.FindByValue(str).Selected = true;
                                if (str == "4")
                                {
                                    txt_ActivityOther.ReadOnly = false;
                                }
                            }
                            
                        }

                        txt_ActivityOther.Text = dt.Rows[i]["OtherRemarks"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }
    private void SetPreviousDataRemove()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlEmpOption = (DropDownList)gv_InterviewBoardMember.Rows[rowIndex].FindControl("ddlEmpOption");
                    HiddenField hdpkd = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdpkd");
                    HiddenField hdEmpInfoId = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdEmpInfoId");
                    TextBox txt_EmpID = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpID");
                    TextBox txt_EmpName = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpName");
                    TextBox txt_EmpDesignation = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpDesignation");
                    TextBox txt_EmpDepartment = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpDepartment");
                    TextBox txt_EmpCompany = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpCompany");
                    TextBox txt_EmpEmail = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpEmail");
                    TextBox txt_EmpPhone = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_EmpPhone");

                    CheckBoxList lchk_InterviewActivity = (CheckBoxList)gv_InterviewBoardMember.Rows[rowIndex].FindControl("lchk_InterviewActivity");
                    TextBox txt_ActivityOther = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].FindControl("txt_ActivityOther");
                    //if (i < dt.Rows.Count - 1)
                    {

                        //Assign the value from DataTable to the TextBox   
                        hdpkd.Value = dt.Rows[i]["BoardDetailsId"].ToString();
                        txt_EmpID.Text = dt.Rows[i]["EmpMasterCode"].ToString();
                        hdEmpInfoId.Value = dt.Rows[i]["EmpInfoId"].ToString();
                        txt_EmpName.Text = dt.Rows[i]["EmpName"].ToString();
                        txt_EmpDesignation.Text = dt.Rows[i]["Designation"].ToString();
                        txt_EmpDepartment.Text = dt.Rows[i]["DepartmentName"].ToString();
                        txt_EmpCompany.Text = dt.Rows[i]["CompanyName"].ToString();
                        txt_EmpEmail.Text = dt.Rows[i]["OfficialEmail"].ToString();
                        txt_EmpPhone.Text = dt.Rows[i]["OfficialMobile"].ToString();

                        //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                        ddlEmpOption.ClearSelection();
                        //ddlEmpOption.Items.FindByText(dt.Rows[i]["MemberType"].ToString()).Selected = true;
                        try
                        {
                            ddlEmpOption.Items.FindByValue(dt.Rows[i]["MemberType"].ToString()).Selected = true;
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }

                        lchk_InterviewActivity.ClearSelection();
                        List<string> stringList = dt.Rows[i]["InterviewActivity"].ToString().Split(',').ToList();

                        foreach (string str in stringList)
                        {
                            if (!string.IsNullOrEmpty(str))
                            {
                                lchk_InterviewActivity.Items.FindByValue(str).Selected = true;
                                if (str == "4")
                                {
                                    txt_ActivityOther.ReadOnly = false;
                                }
                            }

                        }

                        txt_ActivityOther.Text = dt.Rows[i]["OtherRemarks"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    protected void gv_InterviewBoardMember_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            LinkButton lb = (LinkButton)e.Row.FindControl("lb_Remove");
            CheckBoxList lchk_InterviewActivity = (CheckBoxList)e.Row.FindControl("lchk_InterviewActivity");
            using (DataTable dtA = _commonDataLoad.GetInterviewActivity())
            {

                lchk_InterviewActivity.DataSource = dtA;
                lchk_InterviewActivity.DataValueField = "Value";
                lchk_InterviewActivity.DataTextField = "TextField";
                lchk_InterviewActivity.DataBind();
            }
            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dt.Rows.Count - 1)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }

    protected void lb_Remove_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            HiddenField hdpkd = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpkd");
            //if (hdpkd.Value != "0")
            //{
            //if (!string.IsNullOrEmpty(hdpkd.Value))
            //{
            //    int pkd = Int32.Parse(hdpkd.Value);
            //    var db = new HRIS_SMCEntities();
            //    tblInterviewBoardSetupDetail IVDetails = (from emd in db.tblInterviewBoardSetupDetails where emd.BoardDetailsId == pkd select emd).FirstOrDefault();
            //    IVDetails.IsActive = false;
            //    db.SaveChanges(); 
            //}

       
                 
            //}

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        ResetRowID(dt);
                    }
                }

                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable"] = dt;

                //Re bind the GridView for the updated data  
                gv_InterviewBoardMember.DataSource = dt;
                gv_InterviewBoardMember.DataBind();
            }

            //Set Previous Data on Postbacks  
            SetPreviousDataRemove();
        }
        catch (Exception ex )
        {
         //   AlertMessageBoxShow("Error on removing selected row..."+ex.Message);

            //LinkButton lb = (LinkButton)sender;
            //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            //int rowID = gvRow.RowIndex;
            //HiddenField hdpkd = (HiddenField)gv_InterviewBoardMember.Rows[rowID].FindControl("hdpkd");

            //int pkd = 0;
            //if (hdpkd.Value != "")
            //{
            //     pkd = Int32.Parse(hdpkd.Value);
            //}
         



            //var db = new HRIS_SMCEntities();
            //tblInterviewBoardSetupDetail IVDetails = (from emd in db.tblInterviewBoardSetupDetails where emd.BoardDetailsId == pkd select emd).FirstOrDefault();
            ////IVDetails.IsActive = false;
            ////db.tblInterviewBoardSetupDetails.Add(IVDetails);
            ////db.SaveChanges();


            //if (ViewState["CurrentTable"] != null)
            //{

            //    DataTable dt = (DataTable)ViewState["CurrentTable"];
            //    if (dt.Rows.Count > 1)
            //    {
            //        if (gvRow.RowIndex < dt.Rows.Count - 1)
            //        {
            //            //Remove the Selected Row data and reset row number  
            //            dt.Rows.Remove(dt.Rows[rowID]);
            //            ResetRowID(dt);
            //        }
            //    }

            //    //Store the current data in ViewState for future reference  
            //    ViewState["CurrentTable"] = dt;

            //    //Re bind the GridView for the updated data  
            //    gv_InterviewBoardMember.DataSource = dt;
            //    gv_InterviewBoardMember.DataBind();
            //}

            ////Set Previous Data on Postbacks  
            //SetPreviousData();
        }
       
    }

    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }
    }

    private string InsertRecords(StringCollection sc)
    {
        StringBuilder sb = new StringBuilder(string.Empty);
        string[] splitItems = null;
        const string sqlStatement = "INSERT INTO GridViewDynamicData (Field1,Field2,Field3,Field4) VALUES";
        foreach (string item in sc)
        {
            if (item.Contains(","))
            {
                splitItems = item.Split(",".ToCharArray());
                sb.AppendFormat("{0}('{1}','{2}','{3}','{4}'); ", sqlStatement, splitItems[0], splitItems[1], splitItems[2], splitItems[3]);
            }
        }

        return sb.ToString();
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;
        StringCollection sc = new StringCollection();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values  
                    TextBox box1 = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].Cells[1].FindControl("TextBox1");
                    TextBox box2 = (TextBox)gv_InterviewBoardMember.Rows[rowIndex].Cells[2].FindControl("TextBox2");
                    DropDownList ddl1 = (DropDownList)gv_InterviewBoardMember.Rows[rowIndex].Cells[3].FindControl("DropDownList1");
                    DropDownList ddl2 = (DropDownList)gv_InterviewBoardMember.Rows[rowIndex].Cells[4].FindControl("DropDownList2");
                    //get the values from TextBox and DropDownList  
                    //then add it to the collections with a comma "," as the delimited values  
                    sc.Add(string.Format("{0},{1},{2},{3}", box1.Text, box2.Text, ddl1.SelectedItem.Text, ddl2.SelectedItem.Text));
                    rowIndex++;
                }
                //Call the method for executing inserts  
                InsertRecords(sc);
            }
        }
    }


    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            var EmpOption = Session["EmpOption"].ToString();
            if (EmpOption != "3")
            {
                GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
                //DropDownList ddlEmpOption = (DropDownList)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("ddlEmpOption");

                TextBox txt_EmpName = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_EmpName");
                
                string Emp = txt_EmpName.Text;
                int EmpInfoId=0;
                if (!string.IsNullOrEmpty(Emp) && Emp.Length > 10)
                {
                    EmpInfoId = int.Parse(Emp.Split(':')[0]);
                    Emp = Emp.Split(':')[2];
                }
                using (var db = new HRIS_SMCEntities())
                {
                    var emp = (from em in db.vw_EmpInfo where em.EmpInfoId == EmpInfoId select em).FirstOrDefault();
                    if (emp != null)
                    {
                        HiddenField hdEmpInfoId = (HiddenField)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("hdEmpInfoId");
                        TextBox txt_EmpID = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_EmpID");
                        TextBox txt_EmpDesignation = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_EmpDesignation");
                        TextBox txt_EmpDepartment = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_EmpDepartment");
                        TextBox txt_EmpCompany = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_EmpCompany");
                        TextBox txt_EmpEmail = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_EmpEmail");
                        TextBox txt_EmpPhone = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_EmpPhone");

                        hdEmpInfoId.Value = EmpInfoId.ToString();
                        txt_EmpID.Text = emp.EmpMasterCode;
                        txt_EmpName.Text = Emp;
                        txt_EmpDesignation.Text = emp.Designation;
                        txt_EmpDepartment.Text = emp.DepartmentName;
                        txt_EmpCompany.Text = emp.CompanyName;
                        txt_EmpEmail.Text = emp.OfficialEmail;
                        txt_EmpPhone.Text = emp.OfficialMobile;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }

    }

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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        //Session["Status"] = "View";
        Response.Redirect("InterviewBoardSetupList.aspx");
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

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("InterviewBoardSetup.aspx");
    }

    protected void radInterviewActivity_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCompany.SelectedIndex<=0)
        //{
        //    radInterviewActivity.ClearSelection();
        //    AlertMessageBoxShow("Company required...");
        //    return;
        //}
        //if (string.IsNullOrEmpty(txt_JobCirculation.Text))
        //{
        //    radInterviewActivity.ClearSelection();
        //    AlertMessageBoxShow("Job Circulation required...");
        //    return;
        //}
        
        int cid = int.Parse(ddlCompany.SelectedValue);
        long JobId = 0;
        int InterviewActivity = 1;//int.Parse(radInterviewActivity.SelectedValue);
        using (var db = new HRIS_SMCEntities())
        {
            tblJobCreation job = (from j in db.tblJobCreations where j.JobCode.Equals(ddlJobCirculation.SelectedItem.Text) select j).FirstOrDefault();
            if (job != null)
            {
                JobId = job.JobID;
            }
        }
        using (DataTable dt = _interviewCommonDAL.GetInterviewPhase(cid, InterviewActivity, JobId))
        {
            if (dt.Rows.Count>0)
            {
                //txt_InterviewPhase.Text = dt.Rows[0]["InterviewPhase"].ToString();
                //AlertMessageBoxShow("This is the " + txt_InterviewPhase.Text + " Phase for " + radInterviewActivity.SelectedItem.Text + " of job circulation: " + txt_JobCirculation.Text);
            }
        }

    }

    protected void ddlInterviewPhase_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    

    //protected void lchk_InterviewActivity_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //        if (lchk_InterviewActivity.Items.FindByValue("4").Selected)
    //        {
    //            txt_ActivityOther.Visible = true;
    //        }
    //        else
    //        {
    //            txt_ActivityOther.Visible = false;
    //        }
        
    //}

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        string ApprovalStatus = "Posted";
        Submit(ApprovalStatus);
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        int pk = Int32.Parse(hdpk.Value);

        var db = new HRIS_SMCEntities();
        tblInterviewBoardSetupMaster IVMaster = (from emd in db.tblInterviewBoardSetupMasters where emd.SetupMasterId == pk select emd).FirstOrDefault();
        IVMaster.IsActive = false;
        //db.tblInterviewBoardSetupMasters.Add(IVMaster);
        db.SaveChanges();
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='InterviewBoardSetupList.aspx';",
                        true);
    }

    protected void lchk_InterviewActivity_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((CheckBoxList)sender).NamingContainer);
        CheckBoxList lchk_InterviewActivity = (CheckBoxList)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("lchk_InterviewActivity");
        TextBox txt_ActivityOther = (TextBox)gv_InterviewBoardMember.Rows[row.RowIndex].FindControl("txt_ActivityOther");

        if (lchk_InterviewActivity.Items.FindByValue("4").Selected)
        {
            txt_ActivityOther.ReadOnly = false;
        }
        else
        {
            txt_ActivityOther.ReadOnly = true;
        }
    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
    {

        CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("txt_checkAll");
        bool result = ChkBoxHeader.Checked == true ? true : false;

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("txt_check");
            chk.Checked = result;
        }
        mp1.Show();
    }

    protected void btnYes_OnClick(object sender, EventArgs e)
    {
        if (Validations())
        {
            test.Value = "1";
            mp1.Hide();
        }
      
    }

    private bool Validations()
    {
        if (writtenCheckBox.Checked)
        {
            if (writtenTextBox.Text == "")
            {
                AlertMessageBoxShow("Input written Marks");
                test.Value = "1";
                mp1.Show();
                writtenTextBox.Focus();
                return false;
            }
        }


        if (otherCheckBox.Checked)
        {
            if (otherMarksTextBox.Text == "")
            {
                AlertMessageBoxShow("Input other Marks");
                test.Value = "1";
                mp1.Show();
                otherMarksTextBox.Focus();
                return false;
            }
        }
        return true;
    }

    protected void LinkButton1_OnClick(object sender, EventArgs e)
    {
        //var pkd = hdpkd.Value;
        //m_hdpkd.Value = pkd;
        mp1.Show();
    }

    protected void writtenCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        written.Visible = writtenCheckBox.Checked;
        mp1.Show();
    }

    protected void otherCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        other.Visible = otherCheckBox.Checked;
        mp1.Show();
    }

    protected void vivaCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        grid.Visible = vivaCheckBox.Checked;
        DataTable dtviva = _interviewCommonDAL.GetVivaInformation(ddlCompany.SelectedValue);
        GridView1.DataSource = dtviva;
        GridView1.DataBind();
        mp1.Show();
    }

    protected void txt_check_OnCheckedChanged(object sender, EventArgs e)
    {
        mp1.Show();
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        if (!CheckFatherOccupationAllocateOrNot(ddlJobCirculation.SelectedValue))
        {
            string ApprovalStatus = "Submitted";
            Submit(ApprovalStatus);
        }
        else
        {
            AlertMessageBoxShow("Board Member Already Exist !!!");
        }
     
    }

    private bool CheckFatherOccupationAllocateOrNot(string FatherOccupationId)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.InterviewBoardSetupAllocatedOrNot(FatherOccupationId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();
    private void Submit(string approvalStatus)
    {
        if (validass())
        {
           // if (Visible)
            {
                DateTime timeF = DateTime.Parse(string.Format("{0}:{1} {2}", txt_InterviewFromTime.Hour, txt_InterviewFromTime.Minute, txt_InterviewFromTime.AmPm));
                DateTime timeT = DateTime.Parse(string.Format("{0}:{1} {2}", txt_InterviewToTime.Hour, txt_InterviewToTime.Minute, txt_InterviewToTime.AmPm));
                mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
                try
                {
                    if (gv_InterviewBoardMember.Rows.Count > 0)
                    {
                        using (var db = new HRIS_SMCEntities())
                        {
                            tblInterviewBoardSetupMaster IVMaster = null;
                            //tblJobCreation job = (from j in db.tblJobCreations where j.JobCode == txt_JobCirculation.Text select j).FirstOrDefault();
                            if (mid > 0)////Edit Mode
                            {
                                IVMaster = (from em in db.tblInterviewBoardSetupMasters where em.SetupMasterId == mid select em).FirstOrDefault();

                                IVMaster.CompanyId = Int32.Parse(ddlCompany.SelectedValue);
                                IVMaster.JobTitleId = int.Parse(ddlJobCirculation.SelectedValue);//job == null ? 0 : (int) job.JobID;
                                ////No Phase Update on Edit Mode
                                //IVMaster.InterviewPhase = Int32.Parse(txt_InterviewPhase.Text);
                                IVMaster.Vanue = txt_InterviewVenue.Text;
                                IVMaster.InterviewDate = DateTime.Parse(txt_InterviewDate.Text);

                                IVMaster.Remarks = txt_InterviewMasterRemarks.Text;
                                IVMaster.EmailBody = txt_EmailBody.Text;
                                IVMaster.InterviewFromTime = timeF.TimeOfDay.ToString();
                                IVMaster.InterviewToTime = timeT.TimeOfDay.ToString();
                                IVMaster.IsActive = true;
                                IVMaster.UpdateBy = _userId;
                                IVMaster.ApprovalStatus = approvalStatus;
                                IVMaster.UpdateDate = DateTime.Now;
                                db.SaveChanges();

                                SaveInterViewVivaSetup(IVMaster.SetupMasterId);
                            }
                            else
                            {////New Mode
                                IVMaster = new tblInterviewBoardSetupMaster();
                                IVMaster.CompanyId = Int32.Parse(ddlCompany.SelectedValue);
                                IVMaster.JobTitleId = int.Parse(ddlJobCirculation.SelectedValue);//job == null ? 0 : (int) job.JobID;
                                IVMaster.InterviewPhase = Int32.Parse(ddlInterviewPhase.SelectedValue);
                                IVMaster.Vanue = txt_InterviewVenue.Text;
                                IVMaster.InterviewDate = DateTime.Parse(txt_InterviewDate.Text);
                                IVMaster.Remarks = txt_InterviewMasterRemarks.Text;
                                IVMaster.EmailBody = txt_EmailBody.Text;
                                IVMaster.InterviewFromTime = timeF.TimeOfDay.ToString();
                                IVMaster.InterviewToTime = timeT.TimeOfDay.ToString();
                                IVMaster.IsActive = true;

                                IVMaster.EntryBy = _userId;
                                IVMaster.ApprovalStatus = approvalStatus;
                                IVMaster.EntryBy = _userId;

                                IVMaster.EntryDate = DateTime.Now;
                                db.tblInterviewBoardSetupMasters.Add(IVMaster);
                                db.SaveChanges();

                                SaveInterViewVivaSetup(IVMaster.SetupMasterId);
                            }

                            //save grid
                            tblInterviewBoardSetupDetail IVDetails = null;
                            for (int i = 0; i < gv_InterviewBoardMember.Rows.Count; i++)
                            {
                                ////todo home
                                DropDownList ddlEmpOption = (DropDownList)gv_InterviewBoardMember.Rows[i].FindControl("ddlEmpOption");
                                HiddenField hdpkd = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdpkd");
                                HiddenField hdEmpInfoId = (HiddenField)gv_InterviewBoardMember.Rows[i].FindControl("hdEmpInfoId");
                                TextBox txt_EmpID = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpID");
                                TextBox txt_EmpName = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpName");
                                TextBox txt_EmpDesignation = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpDesignation");
                                TextBox txt_EmpDepartment = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpDepartment");
                                TextBox txt_EmpCompany = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpCompany");
                                TextBox txt_EmpEmail = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpEmail");
                                TextBox txt_ActivityOther = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_ActivityOther");
                                CheckBoxList lchk_InterviewActivity = (CheckBoxList)gv_InterviewBoardMember.Rows[i].FindControl("lchk_InterviewActivity");

                                TextBox txt_EmpPhone = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_EmpPhone");
                                //var emp = (from em in db.vw_EmpInfo where em.EmpName == txt_EmpName.Text select em.EmpInfoId).FirstOrDefault();

                                if (!string.IsNullOrEmpty(hdpkd.Value) && Convert.ToInt32(hdpkd.Value) > 0)
                                {
                                    var pkd = int.Parse(hdpkd.Value);
                                    IVDetails = (from emd in db.tblInterviewBoardSetupDetails where emd.BoardDetailsId == pkd select emd).FirstOrDefault();
                                    IVDetails.MemberType = ddlEmpOption.SelectedValue;
                                    IVDetails.EmployeeId = string.IsNullOrEmpty(hdEmpInfoId.Value) ? (int?)null : int.Parse(hdEmpInfoId.Value);
                                    IVDetails.Name = txt_EmpName.Text;
                                    IVDetails.Designation = txt_EmpDesignation.Text;
                                    IVDetails.Department = txt_EmpDepartment.Text;
                                    IVDetails.Company = txt_EmpCompany.Text;
                                    IVDetails.Email = txt_EmpEmail.Text;
                                    foreach (ListItem item in lchk_InterviewActivity.Items)
                                    {
                                        if (int.Parse(item.Value) == 1)
                                        {
                                            IVDetails.Written = item.Selected;
                                        }
                                        if (int.Parse(item.Value) == 2)
                                        {
                                            IVDetails.Viva = item.Selected;
                                        }
                                        if (int.Parse(item.Value) == 4)
                                        {
                                            IVDetails.Other = item.Selected;
                                            IVDetails.OtherRemarks = txt_ActivityOther.Text;
                                        }
                                    }
                                    IVDetails.Phone = txt_EmpPhone.Text;
                                    IVDetails.IsActive = true;

                                    db.SaveChanges();
                                }
                                else
                                {
                                    IVDetails = new tblInterviewBoardSetupDetail();

                                    IVDetails.MasterId = IVMaster.SetupMasterId;
                                    IVDetails.MemberType = ddlEmpOption.SelectedValue;
                                    IVDetails.EmployeeId = string.IsNullOrEmpty(hdEmpInfoId.Value) ? (int?)null : int.Parse(hdEmpInfoId.Value);
                                    IVDetails.Name = txt_EmpName.Text;
                                    IVDetails.Designation = txt_EmpDesignation.Text;
                                    IVDetails.Department = txt_EmpDepartment.Text;
                                    IVDetails.Company = txt_EmpCompany.Text;
                                    IVDetails.Email = txt_EmpEmail.Text;
                                    foreach (ListItem item in lchk_InterviewActivity.Items)
                                    {
                                        if (int.Parse(item.Value) == 1)
                                        {
                                            IVDetails.Written = item.Selected;
                                        }
                                        if (int.Parse(item.Value) == 2)
                                        {
                                            IVDetails.Viva = item.Selected;
                                        }
                                        if (int.Parse(item.Value) == 4)
                                        {
                                            IVDetails.Other = item.Selected;
                                            IVDetails.OtherRemarks = txt_ActivityOther.Text;
                                        }
                                    }
                                    IVDetails.Phone = txt_EmpPhone.Text;
                                    IVDetails.IsActive = true;



                                    db.tblInterviewBoardSetupDetails.Add(IVDetails);
                                }

                            }
                            db.SaveChanges();
                            //AlertMessageBoxShow("Operation Successful...");
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation Successful...');window.location ='InterviewBoardSetupList.aspx';",
                                true);
                        }
                    }
                    else
                    {
                        AlertMessageBoxShow("No board member listed to save...");
                    }

                }
                catch (Exception ex)
                {
                    AlertMessageBoxShow(ex.Message);
                } 
            }
        }
    }

    private bool validass()
    {
        if (ddlJobCirculation.SelectedValue == "")
        {
            AlertMessageBoxShow("Please Select Job Circulation !!!");
            return false;
            
        }
       

        if (txt_InterviewDate.Text=="")
        {
            AlertMessageBoxShow("Please Select Interview Date !!!");
            return false;
        }

        if ((writtenCheckBox.Checked == false) && (otherCheckBox.Checked == false) && (vivaCheckBox.Checked == false))
        {
             mp1.Show();

             AlertMessageBoxShow("Please Select At Least One Check Box!!!");
            return false;
        }

        //for (int i = 0; i < gv_InterviewBoardMember.Rows.Count; i++)
        //{
        //    CheckBoxList lchk_InterviewActivity = (CheckBoxList)gv_InterviewBoardMember.Rows[i].FindControl("lchk_InterviewActivity");
        //    TextBox txt_ActivityOther = (TextBox)gv_InterviewBoardMember.Rows[i].FindControl("txt_ActivityOther");


        //    lchk_InterviewActivity.Items.
        //    //foreach (ListItem item in lchk_InterviewActivity.Items)
        //    //{

        //    //    if (item.Selected==false)
        //    //    {
        //    //        AlertMessageBoxShow("Please Select one!!!");
        //    //        return false;


        //    //    }
              
        //    //    if (int.Parse(item.Value) == 4)
        //    //    {
                     
        //    //         if (txt_ActivityOther.Text=="")
        //    //        {
        //    //            AlertMessageBoxShow("Please Select tex!!!");
        //    //            return false;
        //    //        } 
                       
        //    //    }
        //    //}
        //}

        return true;
    }

    protected void btnUpdateforSubmit_OnClick(object sender, EventArgs e)
    {
        string ApprovalStatus = "Submitted";
        Submit(ApprovalStatus);
    }
}