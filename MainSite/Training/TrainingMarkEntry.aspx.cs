using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;

public partial class Training_TrainingMarkEntry : System.Web.UI.Page
{
    TrainingDAL aTrainingDal=new TrainingDAL();
    CommonDataLoadDAL aCommonDataLoadDal=new CommonDataLoadDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserPersmissionValidation();
            DropDownList();
        }
    }

    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            string text = Path.GetExtension(Request.Path);
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }

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

                    Button1.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                   
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("/Default.aspx");
        }
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aCommonDataLoadDal.FinYearByCompDropDown(ddlFinancialYear, ddlCompany.SelectedValue);
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    public bool Validation()
    {
        if (gv_AllEmployee.Rows.Count<1)
        {
            AlertMessageBoxShow("Choose Training Title !!");
            return false;
        }
        return true;
    }
    public void DropDownList()
    {
        aCommonDataLoadDal.CompanyDropDown(ddlCompany,"");
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);
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
    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aTrainingDal.TrainingRecordDropDown(ddlTrainingRecord,ddlCompany.SelectedValue,ddlFinancialYear.SelectedValue);
        
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            TrainingMarksMasterDAO aTrainingMarksMasterDao = new TrainingMarksMasterDAO()
            {
                TrainigMarkId = string.IsNullOrEmpty(hdpk.Value)?0:Convert.ToInt32(hdpk.Value),
                TrainingRecordMasterId = Convert.ToInt32(ddlTrainingRecord.SelectedValue),
                Remarks = txtRemarks.Text,
                OutOfMark = Convert.ToDecimal(outMarksTextBox.Text),
                

            };
            int id = aTrainingDal.SaveTrainingMarksMaster(aTrainingMarksMasterDao);
            if (id>0)
            {
                List<TrainingMarksDetailDAO> aTrainingMarksDetailDaoList=new List<TrainingMarksDetailDAO>();
                for (int i = 0; i < gv_AllEmployee.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_AllEmployee.Rows[i].FindControl("chkSingle");
                    if (chkSingle.Checked)
                    {
                        TrainingMarksDetailDAO aTrainingMarksDetailDao = new TrainingMarksDetailDAO()
                        {
                            EmpInfoId = Convert.ToInt32(gv_AllEmployee.DataKeys[i][1].ToString()),
                            TrainigMarkId = id,
                            TrainingRecordDetailsEmp = Convert.ToInt32(gv_AllEmployee.DataKeys[i][0].ToString()),
                            PreMark = string.IsNullOrEmpty(((TextBox)gv_AllEmployee.Rows[i].FindControl("txt_Pre")).Text) ? 0 : Convert.ToDecimal(((TextBox)gv_AllEmployee.Rows[i].FindControl("txt_Pre")).Text),
                            PostMark = string.IsNullOrEmpty(((TextBox)gv_AllEmployee.Rows[i].FindControl("txt_Post")).Text) ? 0 : Convert.ToDecimal(((TextBox)gv_AllEmployee.Rows[i].FindControl("txt_Post")).Text),
                        };
                        aTrainingMarksDetailDaoList.Add(aTrainingMarksDetailDao);
                    }
                }
                int iddd = aTrainingDal.SaveTrainingMarksDetail(aTrainingMarksDetailDaoList, id);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='TrainingMarkEntry.aspx';",
                true);

        }
    }

    protected void ddlTrainingRecord_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTrainingRecord.SelectedValue!="")
        {
            DataTable dtDetails = aTrainingDal.GetEmployeeForAttendance(Convert.ToInt32(ddlTrainingRecord.SelectedValue));
            gv_AllEmployee.DataSource = dtDetails;
            gv_AllEmployee.DataBind();
            for (int i = 0; i < gv_AllEmployee.Rows.Count; i++)
            {
                DataTable dtdata = aTrainingDal.GetTrainingMarksDetail(gv_AllEmployee.DataKeys[i][1].ToString(),
                    ddlTrainingRecord.SelectedValue, gv_AllEmployee.DataKeys[i][0].ToString());
                if (dtdata.Rows.Count > 0)
                {
                    CheckBox chkSingle = (CheckBox)gv_AllEmployee.Rows[i].FindControl("chkSingle");
                    chkSingle.Checked = true;
                    ((TextBox)gv_AllEmployee.Rows[i].FindControl("txt_Pre")).Text = dtdata.Rows[0]["PreMark"].ToString();
                    ((TextBox)gv_AllEmployee.Rows[i].FindControl("txt_Post")).Text = dtdata.Rows[0]["PostMark"].ToString();
                    txtRemarks.Text = dtdata.Rows[0]["Remarks"].ToString();
                    outMarksTextBox.Text = dtdata.Rows[0]["OutOfMark"].ToString();
                    hdpk.Value = dtdata.Rows[0]["TrainigMarkId"].ToString();
                }
            }
        }
        else
        {
            gv_AllEmployee.DataSource = null;
            gv_AllEmployee.DataBind();
        }
     
    }
}