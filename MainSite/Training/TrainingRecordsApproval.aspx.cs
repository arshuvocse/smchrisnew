using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAL.TrainingDAL;

public partial class Training_TrainingRecords : System.Web.UI.Page
{
    public TrainingRecordDAL _recordDal = new TrainingRecordDAL();
    ClsApprovalAction approvalAction = new ClsApprovalAction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataLoad();
        }
    }
    public void DataLoad()
    {
        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');
        string exten = Path.GetExtension(Request.Path);
        if (exten == string.Empty)
        {
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
        }
        else
        {
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        }

        string userName = Session["UserId"].ToString();
        DataLoadByCondition(filepath, userName);
        //approvalAction.LoadActionControlByUser(jobreqRadioButtonList, filepath, userName);

    }
    private void DataLoadByCondition(string pageName, string user)
    {
        DataTable aDataTable = new DataTable();
        string ActionStatus = approvalAction.LoadForApprovalByUserCondition(pageName, user);
        aDataTable = _recordDal.GetTrainingRecordsAppNew();
        gv_trainingList.DataSource = aDataTable;
        gv_trainingList.DataBind();
    }

    protected void btnAddNewTraining_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("TrainingRecord.aspx");
    }
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_trainingList.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_trainingList.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_trainingList.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_trainingList.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    }
    //protected void Button2_OnClick(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < gv_trainingList.Rows.Count; i++)
    //    {
    //        CheckBox chkSingle = (CheckBox)gv_trainingList.Rows[i].FindControl("chkSingle");
    //        if (chkSingle.Checked)
    //        {
    //            _recordDal.UpdateStatus(gv_trainingList.DataKeys[i][0].ToString(),
    //                jobreqRadioButtonList.SelectedValue);
    //        }
    //    }
    //    LoadList();
    //    AlertMessageBoxShow("Approval Successfully Done");
    //}
    protected void lb_Edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        
        int rowID = gvRow.RowIndex;








        var datKey = gv_trainingList.DataKeys[rowID];
        if (datKey != null)
        {
            string jobReqId = datKey[1].ToString();
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            string exten = Path.GetExtension(Request.Path);
            if (exten == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }
            Session["TrainingRecordMasterId"] = "";
            Session["TrainingRecordMasterId"] = jobReqId;
            Session["AppLogId"] = datKey[2].ToString();
            Session["AppPage"] = filepath;


        }

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
        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingRecordApproveView2.aspx?mid=" + hdpk.Value);
    }

    //protected void lb_remove_OnClick(object sender, EventArgs e)
    //{
    //    LinkButton lb = (LinkButton)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    int rowID = gvRow.RowIndex;

    //    HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");



    //    bool result = _recordDal.DeleteTrainingRecord(Convert.ToInt32(hdpk.Value), Convert.ToInt32(Session["UserId"].ToString()));

    //    if (result == true)
    //    {
    //        AlertMessageBoxShow("Operation Successful...");
    //        LoadList();
    //    }
    //    else
    //    {

    //        AlertMessageBoxShow("Operation Failed...");

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
    //private void LoadList()
    //{
    //    DataTable dt = _recordDal.GetTrainingRecordsApp();
    //    gv_trainingList.DataSource = dt;
    //    gv_trainingList.DataBind();
    //}
    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}