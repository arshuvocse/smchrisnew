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
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using MKB.TimePicker;

public partial class Inverview_InterviewBoardMemberMarksEntry : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private InterviewCommonDAL _interviewCommonDal = new InterviewCommonDAL();
    private int _userId;
    private DropDownList ddlCompany;
    private DropDownList ddlJobCirculation;

    protected void Page_Load(object sender, EventArgs e)
    {
        ddlCompany = (DropDownList)IVSearchControl.FindControl("ddlCompany");
        ddlJobCirculation = (DropDownList)IVSearchControl.FindControl("ddlJobCirculation") as DropDownList;
        if (Session["UserId"] != null)
        {
            _userId = int.Parse(Session["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            LoadInitialDDL();
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
    //        hdJobID.Value = Emp.Split(':')[0];
    //        txt_JobCirculation.Text = Emp.Split(':')[1];
    //        txt_JobTitle.Text = Emp.Split(':')[2];
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
    protected void btn_LoadList_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex <= 0)
        {
            gv_InterviewCList.DataSource = null;
            gv_InterviewCList.DataBind();

            AlertMessageBoxShow("Company required...");
            return;
        }
        if (ddlJobCirculation.SelectedValue=="")
        {
            gv_InterviewCList.DataSource = null;
            gv_InterviewCList.DataBind();

            AlertMessageBoxShow("Job Circulation required...");
            return;
        }
        //if (ddlInterviewPhase.SelectedIndex <= 0)
        //{
        //    //radInterviewActivity.ClearSelection();
        //    gv_InterviewCList.DataSource = null;
        //    gv_InterviewCList.DataBind();

        //    AlertMessageBoxShow("Interview Phase required...");
        //    return;
        //}
        int cid = int.Parse(ddlCompany.SelectedValue);
        int jobid = int.Parse(ddlJobCirculation.SelectedValue);
        int InterviewActivity = 0;//int.Parse(radInterviewActivity.SelectedValue);
        int InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);
        using (DataTable dts = _interviewCommonDal.GetIVBoardMemberForMarksEntry(cid, jobid, InterviewActivity, 1))
        {
            if (dts.Rows.Count>0)
            {
                gv_InterviewCList.DataSource = dts;
                gv_InterviewCList.DataBind();

                for (int i = 0; i < dts.Rows.Count; i++)
                {
                    LinkButton lb_WrittenMarksEntry = (LinkButton)gv_InterviewCList.Rows[i].FindControl("lb_WrittenMarksEntry");
                    LinkButton lb_VivaMarksEntry = (LinkButton)gv_InterviewCList.Rows[i].FindControl("lb_VivaMarksEntry");
                    LinkButton lb_OthersMarksEntry = (LinkButton)gv_InterviewCList.Rows[i].FindControl("lb_OthersMarksEntry");

                    lb_WrittenMarksEntry.Visible = bool.Parse(dts.Rows[i]["Written"].ToString());
                    lb_VivaMarksEntry.Visible = bool.Parse(dts.Rows[i]["Viva"].ToString());
                    lb_OthersMarksEntry.Visible = bool.Parse(dts.Rows[i]["Other"].ToString());
                }
            }
            else
            {
                gv_InterviewCList.DataSource = null;
                gv_InterviewCList.DataBind();
                AlertMessageBoxShow("No Board member found for this search...");
            }
        }
    }

    
    protected void txt_Attitude_TextChanged(object sender, EventArgs e)
    {
        //aDirectStockInBLL.CreateConnection_UA_DB();
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        {
          //  if (((CheckBox)gv_InterviewCMarks.Rows[rowIndex].Cells[4].FindControl("isValueCheckBox")).Checked == true)
            {
                //if (((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("rcvQtyTextBox")).Text != "" && ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("unitValueTextBox")).Text != "")
                {
                    decimal Rqty =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Attitude")).Text);
                    decimal Uval =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Language")).Text);

                    decimal Rqty1 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TechnicalSkill")).Text);
                    decimal Uva1l =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_IQ")).Text);

                    decimal Rqty2 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_GeneralKnowledge")).Text);
                    decimal Uval2 =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Others")).Text);


                    decimal Uval3 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TimeSence")).Text);


                    decimal Total = Math.Round((Rqty + Uval + Rqty1 + Uva1l + Rqty2 + Uval2 + Uval3), 2);
                    ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Total")).Text =
                        Total.ToString();
                }
            }
        }
     
        //aDirectStockInBLL.CloseAllConnection();
    }
    protected void txt_Language_TextChanged(object sender, EventArgs e)
    {
        //aDirectStockInBLL.CreateConnection_UA_DB();
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        {
            //  if (((CheckBox)gv_InterviewCMarks.Rows[rowIndex].Cells[4].FindControl("isValueCheckBox")).Checked == true)
            {
                //if (((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("rcvQtyTextBox")).Text != "" && ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("unitValueTextBox")).Text != "")
                {
                    decimal Rqty =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Attitude")).Text);
                    decimal Uval =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Language")).Text);

                    decimal Rqty1 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TechnicalSkill")).Text);
                    decimal Uva1l =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_IQ")).Text);

                    decimal Rqty2 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_GeneralKnowledge")).Text);
                    decimal Uval2 =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Others")).Text);


                    decimal Uval3 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TimeSence")).Text);


                    decimal Total = Math.Round((Rqty + Uval + Rqty1 + Uva1l + Rqty2 + Uval2 + Uval3), 2);
                    ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Total")).Text =
                        Total.ToString();
                }
            }
        }
        //aDirectStockInBLL.CloseAllConnection();
    }
    protected void txt_TechnicalSkill_TextChanged(object sender, EventArgs e)
    {
        //aDirectStockInBLL.CreateConnection_UA_DB();
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        {
            //  if (((CheckBox)gv_InterviewCMarks.Rows[rowIndex].Cells[4].FindControl("isValueCheckBox")).Checked == true)
            {
                //if (((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("rcvQtyTextBox")).Text != "" && ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("unitValueTextBox")).Text != "")
                {
                    decimal Rqty =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Attitude")).Text);
                    decimal Uval =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Language")).Text);

                    decimal Rqty1 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TechnicalSkill")).Text);
                    decimal Uva1l =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_IQ")).Text);

                    decimal Rqty2 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_GeneralKnowledge")).Text);
                    decimal Uval2 =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Others")).Text);


                    decimal Uval3 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TimeSence")).Text);


                    decimal Total = Math.Round((Rqty + Uval + Rqty1 + Uva1l + Rqty2 + Uval2 + Uval3), 2);
                    ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Total")).Text =
                        Total.ToString();
                }
            }
        }
        //aDirectStockInBLL.CloseAllConnection();
    }
    protected void txt_IQ_TextChanged_TextChanged(object sender, EventArgs e)
    {
        //aDirectStockInBLL.CreateConnection_UA_DB();
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        {
            //  if (((CheckBox)gv_InterviewCMarks.Rows[rowIndex].Cells[4].FindControl("isValueCheckBox")).Checked == true)
            {
                //if (((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("rcvQtyTextBox")).Text != "" && ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("unitValueTextBox")).Text != "")
                {
                    decimal Rqty =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Attitude")).Text);
                    decimal Uval =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Language")).Text);

                    decimal Rqty1 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TechnicalSkill")).Text);
                    decimal Uva1l =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_IQ")).Text);

                    decimal Rqty2 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_GeneralKnowledge")).Text);
                    decimal Uval2 =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Others")).Text);


                    decimal Uval3 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TimeSence")).Text);


                    decimal Total = Math.Round((Rqty + Uval + Rqty1 + Uva1l + Rqty2 + Uval2 + Uval3), 2);
                    ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Total")).Text =
                        Total.ToString();
                }
            }
        }
        //aDirectStockInBLL.CloseAllConnection();
    }
    protected void txt_GeneralKnowledge_TextChanged(object sender, EventArgs e)
    {
        //aDirectStockInBLL.CreateConnection_UA_DB();
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        {
            //  if (((CheckBox)gv_InterviewCMarks.Rows[rowIndex].Cells[4].FindControl("isValueCheckBox")).Checked == true)
            {
                //if (((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("rcvQtyTextBox")).Text != "" && ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("unitValueTextBox")).Text != "")
                {
                    decimal Rqty =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Attitude")).Text);
                    decimal Uval =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Language")).Text);

                    decimal Rqty1 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TechnicalSkill")).Text);
                    decimal Uva1l =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_IQ")).Text);

                    decimal Rqty2 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_GeneralKnowledge")).Text);
                    decimal Uval2 =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Others")).Text);


                    decimal Uval3 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TimeSence")).Text);


                    decimal Total = Math.Round((Rqty + Uval + Rqty1 + Uva1l + Rqty2 + Uval2 + Uval3), 2);
                    ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Total")).Text =
                        Total.ToString();
                }
            }
        }
        //aDirectStockInBLL.CloseAllConnection();
    }
    protected void txt_Others_TextChanged(object sender, EventArgs e)
    {
        //aDirectStockInBLL.CreateConnection_UA_DB();
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        {
            //  if (((CheckBox)gv_InterviewCMarks.Rows[rowIndex].Cells[4].FindControl("isValueCheckBox")).Checked == true)
            {
                //if (((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("rcvQtyTextBox")).Text != "" && ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("unitValueTextBox")).Text != "")
                {
                    decimal Rqty =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Attitude")).Text);
                    decimal Uval =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Language")).Text);

                    decimal Rqty1 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TechnicalSkill")).Text);
                    decimal Uva1l =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_IQ")).Text);

                    decimal Rqty2 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_GeneralKnowledge")).Text);
                    decimal Uval2 =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Others")).Text);


                    decimal Uval3 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TimeSence")).Text);


                    decimal Total = Math.Round((Rqty + Uval + Rqty1 + Uva1l + Rqty2 + Uval2 + Uval3), 2);
                    ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Total")).Text =
                        Total.ToString();
                }
            }
        }
        //aDirectStockInBLL.CloseAllConnection();
    }
    protected void txt_TimeSence_TextChanged(object sender, EventArgs e)
    {
        //aDirectStockInBLL.CreateConnection_UA_DB();
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        {
            //  if (((CheckBox)gv_InterviewCMarks.Rows[rowIndex].Cells[4].FindControl("isValueCheckBox")).Checked == true)
            {
                //if (((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("rcvQtyTextBox")).Text != "" && ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("unitValueTextBox")).Text != "")
                {
                    decimal Rqty =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Attitude")).Text);
                    decimal Uval =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Language")).Text);

                    decimal Rqty1 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TechnicalSkill")).Text);
                    decimal Uva1l =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_IQ")).Text);

                    decimal Rqty2 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_GeneralKnowledge")).Text);
                    decimal Uval2 =
                        Convert.ToDecimal(
                            ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Others")).Text);


                    decimal Uval3 =
                       Convert.ToDecimal(
                           ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_TimeSence")).Text);


                    decimal Total = Math.Round((Rqty + Uval + Rqty1 + Uva1l + Rqty2 + Uval2 + Uval3), 2);
                    ((TextBox)gv_InterviewCMarks.Rows[rowIndex].Cells[3].FindControl("txt_Total")).Text =
                        Total.ToString();
                }
            }
        }
        //aDirectStockInBLL.CloseAllConnection();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            using (var db = new HRIS_SMC_DBEntities())
            {
                for (int i = 0; i < gv_InterviewCMarks.Rows.Count; i++)
                {

                    // HiddenField hdpkd = (HiddenField)gv_InterviewCMarks.Rows[i].FindControl("InterviewMarksDetailsId");

                    TextBox txt_marks = (TextBox) gv_InterviewCMarks.Rows[i].FindControl("txt_marks");

                    int hdddVivaDetMarksId = Convert.ToInt32(gv_InterviewCMarks.DataKeys[i][3].ToString());
//                      HiddenField hdpkd = (HiddenField)gv_InterviewCMarks_W.Rows[i].FindControl("hdInterviewMarksDetailsId");
                    int MMIdd = int.Parse(m_hdpkd.Value);
                    int Jobidd = int.Parse(ddlJobCirculation.SelectedValue);

                    using (DataTable dtv = _interviewCommonDal.VIVAaaaaaaaa_WVO(MMIdd.ToString(), Jobidd.ToString()))
                    {
                        if (dtv.Rows.Count > 0)
                        {
                            tblVivaDetailsMark ci = db.tblVivaDetailsMarks.FirstOrDefault(ici => ici.VivaDetailsMarkId == hdddVivaDetMarksId);
                            ci.BoardDetailsId = int.Parse(m_hdpkd.Value);
                            ci.CandidateID = Convert.ToInt32(gv_InterviewCMarks.DataKeys[i][1].ToString());
                            ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);

                            ci.VivaMarks = decimal.Parse(txt_marks.Text);
                            ci.VivaId = Convert.ToInt32(gv_InterviewCMarks.DataKeys[i][0].ToString());
                            ci.VivaOutOf = decimal.Parse(gv_InterviewCMarks.DataKeys[i][2].ToString());
                            ci.UpdateBy = _userId;
                            ci.UpdateDate = DateTime.Now;
                            
                        }

                        else
                        
                        {
                            tblVivaDetailsMark ci = new tblVivaDetailsMark();
                            ci.BoardDetailsId = int.Parse(m_hdpkd.Value);
                            ci.CandidateID = Convert.ToInt32(gv_InterviewCMarks.DataKeys[i][1].ToString());
                            ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);

                            ci.VivaMarks = decimal.Parse(txt_marks.Text);
                            ci.VivaId = Convert.ToInt32(gv_InterviewCMarks.DataKeys[i][0].ToString());
                            ci.VivaOutOf = decimal.Parse(gv_InterviewCMarks.DataKeys[i][2].ToString());
                            ci.EntryBy = _userId;
                            ci.EntryDate = DateTime.Now;
                            db.tblVivaDetailsMarks.Add(ci);
                            
                        }

                       

                    }
                    
                    
                }
                db.SaveChanges();
                AlertMessageBoxShow("Operation Successful...");
            }

        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }

       

        mpe_1.Hide();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        mpe_1.Hide();
    }

    protected void radInterviewActivity_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex <= 0)
        {
            AlertMessageBoxShow("Company required...");
            return;
        }
        if (ddlJobCirculation.SelectedValue!="")
        {
            AlertMessageBoxShow("Job Circulation required...");
            return;
        }

        int cid = int.Parse(ddlCompany.SelectedValue);
        long JobId = 0;
        int InterviewActivity = 0;//int.Parse(radInterviewActivity.SelectedValue);
        using (var db = new HRIS_SMCEntities())
        {
            tblJobCreation job = (from j in db.tblJobCreations where j.JobCode.Equals(ddlJobCirculation.SelectedValue) select j).FirstOrDefault();
            if (job != null)
            {
                JobId = job.JobID;
            }
        }
        using (DataTable dt = _interviewCommonDal.GetAllInterviewPhase(cid, InterviewActivity, JobId))
        {
            if (dt.Rows.Count > 0)
            {
                ddlInterviewPhase.DataSource = dt;
                ddlInterviewPhase.DataValueField = "Value";
                ddlInterviewPhase.DataTextField = "TextField";
                ddlInterviewPhase.DataBind();

                if (dt.Rows.Count==2)
                {
                    ddlInterviewPhase.SelectedValue = "1";
                }
                
            }
        }

    }
    protected void lb_MarksEntry_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[rowID].FindControl("hdpkd");
        HiddenField hdJobID = (HiddenField)gv_InterviewCList.Rows[rowID].FindControl("hdJobID");
        Label txt_Name = (Label)gv_InterviewCList.Rows[rowID].FindControl("txt_Name");
        m_MemberName.Text = txt_Name.Text;
        var pkd = hdpkd.Value;
        m_hdpkd.Value = pkd;
 
        using (DataTable dtv = _interviewCommonDal.VIVAaaaaaaaa_WVO(pkd, hdJobID.Value))
        {
            if (dtv.Rows.Count > 0)
            {
                gv_InterviewCMarks.DataSource = _interviewCommonDal.GetIVVivaMarksForUpdate(ddlCompany.SelectedValue, pkd, hdJobID.Value);
                gv_InterviewCMarks.DataBind();
                
               
            }
            else
            {
                gv_InterviewCMarks.DataSource = _interviewCommonDal.GetIVVivaMarks(ddlCompany.SelectedValue, hdJobID.Value);
                gv_InterviewCMarks.DataBind();
            }

          
        }


        GetSingleName();
       

      


        mpe_1.Show();
    }

    public void GetSingleName()
    {
        if (gv_InterviewCMarks.Rows.Count > 0)
        {


            string masterText = ((Label)gv_InterviewCMarks.Rows[0].FindControl("txt_CandidateName")).Text;
            for (int i = 1; i < gv_InterviewCMarks.Rows.Count; i++)
            {
                if (masterText.Trim() == ((Label)gv_InterviewCMarks.Rows[i].FindControl("txt_CandidateName")).Text.Trim())
                {
                    ((Label)gv_InterviewCMarks.Rows[i].FindControl("txt_CandidateName")).Text = "";
                }
                else
                {
                    masterText = ((Label)gv_InterviewCMarks.Rows[i].FindControl("txt_CandidateName")).Text.Trim();
                }
            }
        }
    }
    protected void lb_WrittenMarksEntry_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[rowID].FindControl("hdpkd");
        Label txt_Name = (Label)gv_InterviewCList.Rows[rowID].FindControl("txt_Name");
        MemberName_W.Text = txt_Name.Text;
        var pkd = hdpkd.Value;
        HiddenField hdJobID = (HiddenField)gv_InterviewCList.Rows[rowID].FindControl("hdJobID");
        hdpkd_W.Value = pkd;


        DataTable dtdata = _interviewCommonDal.GetIVVivaMarks(ddlCompany.SelectedValue, hdJobID.Value);



        using (DataTable dtw = _interviewCommonDal.GetIVCandidateMarks_WVO(pkd))
        {
            if (dtw.Rows.Count>0)
            {
                gv_InterviewCMarks_W.DataSource = dtw;
                gv_InterviewCMarks_W.DataBind();

                txt_WrittenMarksOutOf.Text = dtdata.Rows[0]["WrittenMarks"].ToString();
                mpe_W.Show();
            }
        }
    }

    protected void lb_VivaMarksEntry_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[rowID].FindControl("hdpkd");
        Label txt_Name = (Label)gv_InterviewCList.Rows[rowID].FindControl("txt_Name");
        MemberName_V.Text = txt_Name.Text;
        var pkd = hdpkd.Value;
        hdpkd_V.Value = pkd;
        using (DataTable dtv = _interviewCommonDal.GetIVCandidateMarks_WVO(pkd))
        {
            if (dtv.Rows.Count > 0)
            {
                gv_InterviewCMarks_V.DataSource = dtv;
                gv_InterviewCMarks_V.DataBind();

                txt_VivaMarksOutOf.Text = dtv.Rows[0]["VivaMarksOutOf"].ToString();
                mpe_V.Show();
            }
        }
    }

    protected void lb_OthersMarksEntry_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[rowID].FindControl("hdpkd");
        Label txt_Name = (Label)gv_InterviewCList.Rows[rowID].FindControl("txt_Name");
        MemberName_O.Text = txt_Name.Text;
        var pkd = hdpkd.Value;
        hdpkd_O.Value = pkd;
        HiddenField hdJobID = (HiddenField)gv_InterviewCList.Rows[rowID].FindControl("hdJobID");
        DataTable dtdata = _interviewCommonDal.GetIVVivaMarks(ddlCompany.SelectedValue, hdJobID.Value);

        using (DataTable dto = _interviewCommonDal.GetIVCandidateMarks_WVO(pkd))
        {
            if (dto.Rows.Count > 0)
            {
                gv_InterviewCMarks_O.DataSource = dto;
                gv_InterviewCMarks_O.DataBind();
                if (dtdata.Rows.Count > 0)
                {
                    txt_OtherMarksOutOf.Text = dtdata.Rows[0]["OtherMarks"].ToString();
                }
                mpe_O.Show();
            }
        }
    }

    protected void btnYes_W_Click(object sender, EventArgs e)
    {
        try
        {
            using (var db = new HRIS_SMC_DBEntities())
            {
                for (int i = 0; i < gv_InterviewCMarks_W.Rows.Count; i++)
                {

                    HiddenField hdpkd = (HiddenField)gv_InterviewCMarks_W.Rows[i].FindControl("hdInterviewMarksDetailsId");
                    HiddenField hdCandidateID = (HiddenField)gv_InterviewCMarks_W.Rows[i].FindControl("hdCandidateID");

                    TextBox txt_WrittenMarks = (TextBox)gv_InterviewCMarks_W.Rows[i].FindControl("txt_WrittenMarks");

                    int pkd = int.Parse(hdpkd.Value);
                    if (pkd > 0)
                    {
                        tblInterviewMarksDetail ci = db.tblInterviewMarksDetails.FirstOrDefault(ici => ici.InterviewMarksDetailsId == pkd);

                        ci.BoardDetailsId = int.Parse(hdpkd_W.Value);
                        ci.CandidateID = int.Parse(hdCandidateID.Value);
                        ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);
                       
                        ci.WrittenMarks = decimal.Parse(txt_WrittenMarks.Text);
                        ci.WrittenOutOf = decimal.Parse(txt_WrittenMarksOutOf.Text);
                        ci.Tag = "Written";
                        ci.UpdateBy = _userId;
                        ci.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        tblInterviewMarksDetail ci = new tblInterviewMarksDetail();
                        ci.BoardDetailsId = int.Parse(hdpkd_W.Value);
                        ci.CandidateID = int.Parse(hdCandidateID.Value);
                        //ci.InterviewActivity = int.Parse(radInterviewActivity.SelectedValue);
                        ci.WrittenMarks = decimal.Parse(txt_WrittenMarks.Text);
                        ci.WrittenOutOf = decimal.Parse(txt_WrittenMarksOutOf.Text);
                        ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);
                        ci.Tag = "Written";
                        ci.EntryBy = _userId;
                        ci.EntryDate = DateTime.Now;
                        db.tblInterviewMarksDetails.Add(ci);
                    }



                }
                db.SaveChanges();
                AlertMessageBoxShow("Operation Successful...");
            }

        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }

        mpe_W.Hide();
    }

    protected void btnNo_W_Click(object sender, EventArgs e)
    {
        mpe_W.Hide();
    }

    protected void btnYes_V_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnNo_V_Click(object sender, EventArgs e)
    {
        mpe_V.Hide();
    }

    protected void btnYes_O_Click(object sender, EventArgs e)
    {
        try
        {
            using (var db = new HRIS_SMC_DBEntities())
            {
                for (int i = 0; i < gv_InterviewCMarks_O.Rows.Count; i++)
                {

                    HiddenField hdpkd = (HiddenField)gv_InterviewCMarks_O.Rows[i].FindControl("hdInterviewMarksDetailsId");
                    HiddenField hdCandidateID = (HiddenField)gv_InterviewCMarks_O.Rows[i].FindControl("hdCandidateID");

                    TextBox txt_OtherMarks = (TextBox)gv_InterviewCMarks_O.Rows[i].FindControl("txt_OtherMarks");

                    int pkd = int.Parse(hdpkd.Value);
                    if (pkd > 0)
                    {
                        tblInterviewMarksDetail ci = db.tblInterviewMarksDetails.FirstOrDefault(ici => ici.InterviewMarksDetailsId == pkd);

                        ci.BoardDetailsId = int.Parse(hdpkd_O.Value);
                        ci.CandidateID = int.Parse(hdCandidateID.Value);
                        ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);
                        ci.OtherMarks = decimal.Parse(txt_OtherMarks.Text);
                        ci.OtherOutOf = decimal.Parse(txt_OtherMarksOutOf.Text);
                        ci.Tag = "OtherMarks";
                        ci.UpdateBy = _userId;
                        ci.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        tblInterviewMarksDetail ci = new tblInterviewMarksDetail();
                        ci.BoardDetailsId = int.Parse(hdpkd_O.Value);
                        ci.CandidateID = int.Parse(hdCandidateID.Value);
                        //ci.InterviewActivity = int.Parse(radInterviewActivity.SelectedValue);
                        ci.OtherMarks = decimal.Parse(txt_OtherMarks.Text);
                        ci.OtherOutOf = decimal.Parse(txt_OtherMarksOutOf.Text);
                        ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);
                        ci.Tag = "OtherMarks";
                        ci.EntryBy = _userId;
                        ci.EntryDate = DateTime.Now;
                        db.tblInterviewMarksDetails.Add(ci);
                    }



                }
                db.SaveChanges();
                AlertMessageBoxShow("Operation Successful...");
            }

        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }

        mpe_O.Hide();
    }

    protected void btnNo_O_Click(object sender, EventArgs e)
    {
        mpe_O.Hide();
    }

    protected void btnAttSave_OnClick(object sender, EventArgs e)
    {
        int jobid = string.IsNullOrEmpty(ddlJobCirculation.SelectedValue)?0:int.Parse(ddlJobCirculation.SelectedValue);
        if (jobid > 0)
        {
            if (!string.IsNullOrEmpty(hfAttFile.Value))
            {
                using (var db = new HRIS_SMCEntities())
                {
                    tblInterviewBoardSetupMaster bm = (from m in db.tblInterviewBoardSetupMasters where m.JobTitleId == jobid select m).FirstOrDefault();
                    bm.AttFileName = hfAttFile.Value;
                    bm.HasAttachment = true;
                    db.SaveChanges();
                }
            }
            else
            {
                AlertMessageBoxShow("No file selected...");
            }
        }
        else
        {
            AlertMessageBoxShow("Job required...");
        }
        
    }

    protected void txt_WrittenMarks_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            int row = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;



            TextBox txt_WrittenMarks = (TextBox)gv_InterviewCMarks_W.Rows[row].Cells[1].FindControl("txt_WrittenMarks");



            if (decimal.Parse(txt_WrittenMarksOutOf.Text) < decimal.Parse(txt_WrittenMarks.Text))
            {
                AlertMessageBoxShow("marks cannot be more than out of marks !!!");
                txt_WrittenMarks.Text = "0";
                txt_WrittenMarks.Focus();
            }
        }
        catch (Exception)
        {
            
            //throw;
        }
    }

    protected void txt_OtherMarks_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            int row = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;



            TextBox txt_OtherMarks = (TextBox)gv_InterviewCMarks_O.Rows[row].Cells[1].FindControl("txt_OtherMarks");



            if (decimal.Parse(txt_OtherMarksOutOf.Text) < decimal.Parse(txt_OtherMarks.Text))
            {
                AlertMessageBoxShow("marks cannot be more than out of marks !!!");
                txt_OtherMarks.Text = "0";
                txt_OtherMarks.Focus();
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void txt_marks_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            int row = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;



            TextBox txt_marks = (TextBox)gv_InterviewCMarks.Rows[row].Cells[1].FindControl("txt_marks");
            TextBox txt_VoutOff = (TextBox)gv_InterviewCMarks.Rows[row].Cells[1].FindControl("txt_VoutOff");




            if (decimal.Parse(txt_VoutOff.Text) < decimal.Parse(txt_marks.Text))
            {
                AlertMessageBoxShow("marks cannot be more than out of marks !!!");
                txt_marks.Text = "0";
                txt_marks.Focus();
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void btnModalUp_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex >= 0)
        {
            if (ddlJobCirculation.SelectedValue != "")
            {
                if (ddlJobCirculation.SelectedValue != "")
                {
                    ModalPopupFileUp.Show();
                }
                else
                {
                    AlertMessageBoxShow("Please Select Job Circulation !!!");

                }
            }
            else
            {
                AlertMessageBoxShow("Please Select Job Circulation !!!");

            }
        }
        else
        {
            AlertMessageBoxShow("Please Select Company !!!");
        }
       
       
    }

    protected void btnSaveFile_Click(object sender, EventArgs e)
    {
        if (ddlJobCirculation.SelectedValue!="")
        {
            if (ddlJobCirculation.SelectedValue != "")
            {

                int _size = 500000;// equal 500 kb
                if ((fu_cv.PostedFile != null) && (fu_cv.PostedFile.ContentLength > 0) && (fu_cv.PostedFile.ContentLength <= _size))
                {
                    //  aInquiryBll.DeleteFilesbyStyleId(styleId.ToString());

                    string fileName = string.Empty;
                    string _fileExt = System.IO.Path.GetExtension(fu_cv.FileName);
                    foreach (HttpPostedFile postedFile in fu_cv.PostedFiles)
                    {
                        string filename = Path.GetFileName(postedFile.FileName);
                        string contentType = postedFile.ContentType;
                        using (Stream fs = postedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                if (_fileExt.ToLower() == ".png" || _fileExt.ToLower() == ".gif" || _fileExt.ToLower() == ".jpeg" ||
                                    _fileExt.ToLower() == ".jpg" || _fileExt.ToLower() == ".pdf" || _fileExt.ToLower() == ".doc" || _fileExt.ToLower() == ".docx" || _fileExt.ToLower() == ".zip" || _fileExt.ToLower() == ".rar")
                                {
                                    if (fu_cv.PostedFile.ContentLength <= _size)
                                    {
 


                                        string AdsFile = "InterviewBoardMemberFileU" + Guid.NewGuid().ToString() + Path.GetExtension(fu_cv.FileName);
                                        
                                        fu_cv.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);

                                        tblInterviewBoardMemberFileUp ci = null;
                                        using (var db = new HRIS_SMC_DBEntities())
                                        {
                                            int pkd = 0;
                                            if (pkd > 0)////Edit Mode
                                            {
                                                  ci = db.tblInterviewBoardMemberFileUps.FirstOrDefault(ici => ici.InterviewBoardMemberFileUpId == pkd);

                                                  ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);
                                                ci.CompanyId = int.Parse(ddlCompany.SelectedValue);
                                                Session["AdsFile"] = "";
                                                Session["AdsFile"] = AdsFile;
                                                ci.FileUp = Session["AdsFile"].ToString();
                                                db.SaveChanges();
                                            }
                                            else
                                            {////New Mode
                                                db.Database.ExecuteSqlCommand(@"INSERT INTO DELtblInterviewBoardMemberFileUp (InterviewBoardMemberFileUpId, CompanyId, JobId, FileUp)
SELECT InterviewBoardMemberFileUpId, CompanyId, JobId, FileUp FROM tblInterviewBoardMemberFileUp
  WHERE JobId={0}", Convert.ToInt32(ddlJobCirculation.SelectedValue));
                                                db.Database.ExecuteSqlCommand("Delete FROM dbo.tblInterviewBoardMemberFileUp WHERE JobId={0}",
                                                Convert.ToInt32(ddlJobCirculation.SelectedValue));
                                                  ci = new tblInterviewBoardMemberFileUp();
                                                  ci.JobId = int.Parse(ddlJobCirculation.SelectedValue);
                                                ci.CompanyId = int.Parse(ddlCompany.SelectedValue);
                                                Session["AdsFile"] = "";
                                                Session["AdsFile"] = AdsFile;
                                                ci.FileUp = Session["AdsFile"].ToString();
                                                db.tblInterviewBoardMemberFileUps.Add(ci);
                                                db.SaveChanges();

                                                AlertMessageBoxShow("File Uploaded Successfully !!!");
                                            }
                                        }

                                        


                                    }
                                    else
                                    {
                                        fu_cv.Focus();
                                        ClientScript.RegisterStartupScript(Type.GetType("System.String"), "messagebox",
                                            "<script type=\"text/javascript\">alert('Max file size is 500 KB ');</script>");
                                    }
                                }
                                else
                                {
                                    fu_cv.Focus();
                                    ClientScript.RegisterStartupScript(Type.GetType("System.String"), "messagebox", "<script type=\"text/javascript\">alert('Only GIF or jpeg or jpg or pdf or doc  or zip or rar allowed');</script>");
                                }

                            }
                        }

                    }
                }


            }
            else
            {
                AlertMessageBoxShow("Please Select Job Circulation !!!");

            }
        }
        else
        {
            AlertMessageBoxShow("Please Select Job Circulation !!!");

        }
    }

    protected void btnCancelFile_Click(object sender, EventArgs e)
    {
        ModalPopupFileUp.Hide();
    }

    protected void lb_Download_OnClick(object sender, EventArgs e)
    {
        using (var db = new HRIS_SMC_DBEntities())
        {
            int JID = Convert.ToInt32(ddlJobCirculation.SelectedValue);

           var ci = db.tblInterviewBoardMemberFileUps.FirstOrDefault(ici => ici.JobId ==JID );
            if (ci != null)
            {
                Session["UpFiless"] = "";
                Session["UpFiless"] = ci.FileUp;

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + Session["UpFiless"]);
                Response.TransmitFile(Server.MapPath("../UploadImg/" + Session["UpFiless"]));
                Response.End();
            }
            else
            {
                AlertMessageBoxShow("No Document Found !!!");
            }

        }
    }
}