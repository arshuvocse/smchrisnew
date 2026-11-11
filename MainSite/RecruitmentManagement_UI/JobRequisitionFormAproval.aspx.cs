using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_JobRequisitionFormView : System.Web.UI.Page
{
    EmployeeRequsitionDAL aEmployeeRequsitionDal=new EmployeeRequsitionDAL();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    ClsApprovalAction approvalAction=new ClsApprovalAction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
        {
            DataLoad();
             LoadEmpJobRequisition();
        }
            catch (Exception ex)
            {


            }
        }

        try
        {

            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
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

    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobRequisitionForm.aspx");
    }
    public void DataLoad()
    {
        string filepath = Path.GetDirectoryName(Request.Path);
        filepath = filepath.TrimStart('\\');
        string exten = Path.GetExtension(Request.Path);
        if (exten==string.Empty)
        {
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path)+".aspx";    
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
        aDataTable = aEmployeeRequsitionDal.LoadEmpJobRequisitionApp(ActionStatus);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }
    private void LoadEmpJobRequisition()
    {

        DataTable aDataTable = new DataTable();
        aDataTable = aEmployeeRequsitionDal.LoadEmpJobRequisitionApp();

        if (aDataTable.Rows.Count>0)
        {
            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }

      

    }



    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {

         

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey !=null)
            {
                string jobReqId = datKey[0].ToString();
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
                Session["JobReqId"] = "";
                Session["JobReqId"] = jobReqId;
                Session["AppLogId"] = datKey[1].ToString();
                Session["AppPage"] = filepath;
                Response.Redirect("JobRequisitionFormApproval.aspx?mid=" + jobReqId);

            }

            
        }


        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string companyId = loadGridView.DataKeys[rowindex][0].ToString();

        //    if (aEmployeeRequsitionDal.DeleteEmpReqById(companyId))
        //    {
        //        aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //        //LoadEmpJobRequisition();
        //    }
        //}
    }

    protected void reloadButton_OnClick(object sender, EventArgs e)
    {
        //LoadEmpJobRequisition();
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
        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        //{
        //    CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
        //    if (chkSingle.Checked)
        //    {
        //        aEmployeeRequsitionDal.UpdateStatus(loadGridView.DataKeys[i][0].ToString(),
        //            jobreqRadioButtonList.SelectedItem.Text,Session["UserId"].ToString(),DateTime.Now);
        //    }
        //}
        //DataLoad();
        //aShowMessage.ShowMessageBox(""+jobreqRadioButtonList.SelectedItem.Text+" Successfully Done",this);
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}