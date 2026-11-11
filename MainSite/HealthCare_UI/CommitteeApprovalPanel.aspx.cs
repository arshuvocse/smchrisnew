using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_CommitteeApprovalPanel : System.Web.UI.Page
{

    private CommitteeApprovalPanelDal approvalPanelDal = new CommitteeApprovalPanelDal();

    ShowMessage aShowMessage = new ShowMessage();
    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadInitailDropdown();

            gv_JdBoard.DataSource = null;
            gv_JdBoard.DataBind();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                hfTopsheetGeneMasId.Value = Request.QueryString["MID"];
                DataTable DT = approvalPanelDal.Get_CommitteePanel(" AND H.TopsheetGeneMasId =" + Request.QueryString["MID"], Request.QueryString["MID"]);
                if (DT.Rows.Count > 0)
                {
                    gv_JdBoard.DataSource = DT;
                    gv_JdBoard.DataBind();
                    gv_JdBoard.HeaderRow.Cells[8].Visible = false;
                    gv_JdBoard.Columns[8].Visible = false;
                }

            }
        }
    }

    private void loadInitailDropdown()
    {
        approvalPanelDal.Load_Meeting(ddlMeeting);
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerateView.aspx");
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_JdBoard.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var MeCheck = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("MeCheck");
            MeCheck.Checked = chkBoxHeader.Checked;
        }
    }

    private bool Validation()
    {

        int count = 0;

        if (actionRadioButtonList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Approval Action",this);
            return false;
        }


        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)gv_JdBoard.Rows[i].FindControl("MeCheck");

            if (check.Checked)
            {
                count++;
            }
        }

        if (count == 0)
        {
            aShowMessage.ShowMessageBox("Please Select Minimum One Application",this);

            return false;
        }


        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)gv_JdBoard.Rows[i].FindControl("MeCheck");
            TextBox comments = (TextBox)gv_JdBoard.Rows[i].FindControl("txtcomments");
            if (check.Checked==false)
            {
                if (comments.Text == "")
                {
                    aShowMessage.ShowMessageBox("Comments Can not be empty", this);
                    comments.Focus();
                    return false;
                }

            }
            
        }
        return true;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            Submit();
        }
    }

    private void Submit()
    {
        bool ApprovalStatus = false;
           
        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)gv_JdBoard.Rows[i].FindControl("MeCheck");
            HiddenField ReimformMasterId = (HiddenField)gv_JdBoard.Rows[i].FindControl("hfeimbursFromMasterId");
            TextBox comments = (TextBox)gv_JdBoard.Rows[i].FindControl("txtcomments");
                    
            if (check.Checked)
            {
                         
                  ApprovalStatus = approvalPanelDal.ExpenseReimbursementFormAppoval(ReimformMasterId.Value, actionRadioButtonList.SelectedValue);

                if (ApprovalStatus)
                {
                    if (actionRadioButtonList.SelectedValue == "Review")
                    {
                        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[i].FindControl("HFEmpInfoId");
                        HiddenField HFReimbursFromLogId = (HiddenField)gv_JdBoard.Rows[i].FindControl("HFReimbursFromMasterLogId");
                        ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                        {
                            ActionStatus = "Review",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = 0,
                            ForEmpInfoId = Convert.ToInt32(EmpInfoId.Value),
                            ReimbursFromMasterId = int.Parse(ReimformMasterId.Value),
                            Comments = comments.Text,
                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        };

                        approvalPanelDal.UpdateAppLog("Review", HFReimbursFromLogId.Value);
                        int id = approvalPanelDal.SaveEmpAppLog(appLogDao);
                        ApprovalStatus = formDal.ExpenseReimbursementForm_Retun(ReimformMasterId.Value, actionRadioButtonList.SelectedValue);

                    }

                    if (actionRadioButtonList.SelectedValue == "Approved")
                    {
                        ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                        {
                            ActionStatus = "Approved",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = int.Parse(Session["EmpInfoId"].ToString()),
                            ForEmpInfoId = 0,
                            ReimbursFromMasterId = int.Parse(ReimformMasterId.Value),
                            Comments = comments.Text,
                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        };
                        int id = approvalPanelDal.SaveEmpAppLog(appLogDao);

                        bool acStatus = formDal.ExpenseReimbursementFormAppoval_FApprove(ReimformMasterId.Value, "Approved");

                    }
                }
            }
            else
            {

                HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[i].FindControl("HFEmpInfoId");
                HiddenField HFReimbursFromLogId = (HiddenField)gv_JdBoard.Rows[i].FindControl("HFReimbursFromMasterLogId");
                ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                {
                    ActionStatus = "Review",
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = 0,
                    ForEmpInfoId = Convert.ToInt32(EmpInfoId.Value),
                    ReimbursFromMasterId = int.Parse(ReimformMasterId.Value),
                    Comments = comments.Text,
                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                };

                approvalPanelDal.UpdateAppLog("Review", HFReimbursFromLogId.Value);
                int id = approvalPanelDal.SaveEmpAppLog(appLogDao);
      
                ApprovalStatus = approvalPanelDal.Returnfrom_MettingList(ReimformMasterId.Value, comments.Text.Trim(), hfTopsheetGeneMasId.Value);
                
            }

            ApprovalStatus = approvalPanelDal.Returnfrom_MettingStatusUpdate( hfTopsheetGeneMasId.Value);

        }
        if (ApprovalStatus)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
          "alert",
          "alert('Operation Successfully Done...');window.location ='TopSheetGenerateView.aspx';",
          true);

        }
       
    }

    private string GenerateParam()
    {
        string param = "";

        if (ddlMeeting.SelectedIndex != 0)
        {
            param = param + " AND H.TopsheetGeneMasId =" + ddlMeeting.SelectedValue + "";
        }

        //if (MeetingDate.Text != "")
        //{
        //    param = param + " AND H.MeetingDate ='"+MeetingDate.Text+"' ";
        //}

        return param;

    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {

       
    }

    protected void gv_JdBoard_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "A")
        {

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

    protected void actionRadioButtonList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (gv_JdBoard.Rows.Count != 0)
        {
            if (actionRadioButtonList.SelectedValue == "Review")
            {
                gv_JdBoard.HeaderRow.Cells[8].Visible = true;
                gv_JdBoard.Columns[8].Visible = true;
            }
            else
            {
                gv_JdBoard.HeaderRow.Cells[8].Visible = false;
                gv_JdBoard.Columns[8].Visible = false;
            }
        }
    }

    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        Session["dsdadf"] = "";
        Session["dsdadf"] = "View";
        HiddenField MasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfeimbursFromMasterId");
       // Response.Redirect("ApplicationView_CommitteePanel.aspx?MID=" + MasterId.Value.Trim());
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('ApplicationView_MeetingPanel.aspx?MID=" + MasterId.Value.Trim() + "' ,'_blank');", true);
    }
}