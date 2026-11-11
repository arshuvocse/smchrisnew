using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.RecruitmentManagement_BLL;
using DAL.RecruitmentManagement_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class RecruitmentManagement_UI_JobCreationView : System.Web.UI.Page
{
    JobCreationBll aJobCreationBll = new JobCreationBll();

    JobCreationDal aJobCreationDal = new JobCreationDal();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadJobCreationInfo();
        }
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobCreation.aspx");
    }



    private void LoadJobCreationInfo()
    {
        DataTable jobCreationInfos = new DataTable();

        jobCreationInfos = aJobCreationDal.GetJobCreationInfosApp();
        loadGridView.DataSource = jobCreationInfos;
        loadGridView.DataBind();
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string jobReqId = datKey[0].ToString();
                Session["JobID"] = "";
                Session["JobID"] = jobReqId;

            }

            Response.Redirect("JobCreation.aspx");

        }

        if (e.CommandName == "DeleteData")
        {
            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string masterId = loadGridView.DataKeys[rowindex][0].ToString();

            //bool masterStatus = aJobCreationDal.DeleteJobCreationById(masterId);
            //bool detailStatus = aJobCreationDal.DeleteJobCreationDetailById(masterId);

            //if (masterStatus && detailStatus)
            //{
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
            //    LoadJobCreationInfo();
            //}
        }


    }
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    }
    protected void Button2_OnClick(object sender, EventArgs e)
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
            if (chkSingle.Checked)
            {
                aJobCreationDal.UpdateStatus(loadGridView.DataKeys[i][0].ToString(),
                    jobreqRadioButtonList.SelectedValue);
            }
        }
        LoadJobCreationInfo();
        aShowMessage.ShowMessageBox("Approval Successfully Done", this);
    }
}