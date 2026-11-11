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

public partial class Training_TrainingAttendaneSetup : System.Web.UI.Page
{

    private int mid = 0;
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {
                    DataTable dt = _trainingDal.GetTrainingById(mid);
                    // trainingTaitle.InnerText = dt.Rows[0]["TrainingTitle"].ToString();
                   

              //      quater.Value = dt.Rows[0]["Quater"].ToString();
                   


                    //allocationId.Value = dt.Rows[0]["TrainingBudgetAllocationId"].ToString() == "" ? "0": dt.Rows[0]["TrainingBudgetAllocationId"].ToString();
                    //int reqMaster = dt.Rows[0]["TrainingRequisitionMasterId"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["TrainingRequisitionMasterId"].ToString());
                    //req.Value = reqMaster.ToString(); ;
                    
                    //if (reqMaster > 0)
                    //{
                    //    fromReqAlock.Value = "Requisition";
                    //       DataTable exDt = _trainingDal.GetAttendanceMasterForEdit(mid);
                           
                    //    if (exDt.Rows.Count > 0)
                    //    {
                    //        string attMaster = exDt.Rows[0]["TrainingAttendanceMasterId"].ToString();
                    //        attmaster.Value = attMaster;
                    //        DataTable dtReq = _trainingDal.GetEmployeeFromRequisitionDetails(reqMaster);
                    //        gv_AllEmployee.DataSource = dtReq;
                    //        gv_AllEmployee.DataBind();
                    //    }
                    //    else
                    //    {
                           
                    //        DataTable dtReq = _trainingDal.GetEmployeeFromRequisitionDetails(reqMaster);
                    //        gv_AllEmployee.DataSource = dtReq;
                    //        gv_AllEmployee.DataBind();
                    //    }
                        
                    //}
                    //else
                    {
                        //fromReqAlock.Value = "Allocation";
                        //DataTable exDt = _trainingDal.GetAttendanceMasterForEdit(mid);
                        //if (exDt.Rows.Count > 0)
                        //{
                        //    string attMaster = exDt.Rows[0]["TrainingAttendanceMasterId"].ToString();
                        //    attmaster.Value = attMaster;
                        //    DataTable dtAttDetails = _trainingDal.GetAttendanceDetails(Convert.ToInt32(attMaster));

                        //    gv_AllEmployee.DataSource = dtAttDetails;
                        //    gv_AllEmployee.DataBind();
                        //}
                        //else
                        {
                            
                            //CheckBox cb = (CheckBox)sender;
                            //if (cb.Checked)
                            //{
                            //    for (int i = 0; i < gv_AllEmployee.Rows.Count; i++)
                            //    {
                            //        CheckBox chkSingle = (CheckBox)gv_AllEmployee.Rows[i].FindControl("lb_empCheck");
                            //        chkSingle.Checked = true;
                            //    }
                            //}
                        }
                    }
                    DropDownList();
                   


                }
            }
        }
    }

    public void DropDownList()
    {
        _trainingDal.DateDropDown(ddldate,hdpk.Value);
    }
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_AllEmployee.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_AllEmployee.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_AllEmployee.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_AllEmployee.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ddldate.SelectedIndex!=0)
        {
            List<TrainingAttendanceDAO> adetails = new List<TrainingAttendanceDAO>();

            if (gv_AllEmployee.Rows.Count > 0)
            {
                for (int i = 0; i < gv_AllEmployee.Rows.Count; i++)
                {

                    CheckBox chkSingle = (CheckBox)gv_AllEmployee.Rows[i].FindControl("chkSingle");
                    TrainingAttendanceDAO attendanceDao = new TrainingAttendanceDAO()
                    {
                        ATTDate = Convert.ToDateTime(ddldate.SelectedItem.Text),
                        EmpInfoId = Convert.ToInt32(gv_AllEmployee.DataKeys[i][1].ToString()),
                        EntryBy = Session["UserId"].ToString(),
                        EntryDate = DateTime.Now,
                        IsPresent = chkSingle.Checked,
                        TrainingRecordDetailsEmp = Convert.ToInt32(gv_AllEmployee.DataKeys[i][0].ToString()),
                        TrainingRecordMasterId = Convert.ToInt32(hdpk.Value),
                        TrainingRecordScheDateId = Convert.ToInt32(ddldate.SelectedValue),
                    };
                    adetails.Add(attendanceDao);

                }
            }

            TrainingRecordDAL aTrainingRecordDal = new TrainingRecordDAL();
            aTrainingRecordDal.SaveTrainingAttendance(adetails, Convert.ToInt32(hdpk.Value), Convert.ToInt32(ddldate.SelectedValue), ddldate.SelectedItem.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successful...');window.location ='TrainingAttendance.aspx';",
                     true);
        }

        else
        {
            showMessageBox("Please Select Date!!");
        }
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingAttendaneSetup.aspx?mid=" + hdpk.Value);
    }
    
    //protected void lb_empCheck_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingAttendance.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }

    protected void ddldate_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtDetails = _trainingDal.GetEmployeeForAttendance(Convert.ToInt32(hdpk.Value));
        gv_AllEmployee.DataSource = dtDetails;
        gv_AllEmployee.DataBind();
        
        for (int i = 0; i < gv_AllEmployee.Rows.Count; i++)
        {
            DataTable dtdata = _trainingDal.GetAttandance(Convert.ToInt32(hdpk.Value), ddldate.SelectedItem.Text,
            ddldate.SelectedValue,gv_AllEmployee.DataKeys[i][1].ToString());
            CheckBox chkSingle = (CheckBox)gv_AllEmployee.Rows[i].FindControl("chkSingle");
            if (dtdata.Rows.Count>0)
            {
                chkSingle.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsPresent"].ToString());

            }
            else
            {
                chkSingle.Checked = false;
            }
        }
    }
}