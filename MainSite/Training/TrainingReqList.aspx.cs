using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_TrainingReqList : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadGv();

        }
    }

    private void LoadGv()
    {
        DataTable dt = _trainingDal.GetTrainingRequisitionList();

        gv_EmpDetails.DataSource = dt;
        gv_EmpDetails.DataBind();
    }
    protected void lb_edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_EmpDetails.Rows[rowID].FindControl("reqId");
        string a = hdpk.Value;
        Response.Redirect("TrainingRequisition.aspx?mid=" + hdpk.Value);
    }
}