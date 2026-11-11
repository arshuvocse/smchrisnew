using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;

public partial class Inverview_IVSearchControl : System.Web.UI.UserControl
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialDDL();
            ddlCompany_SelectedIndexChanged(null, null);

            Readonly();
        }
    }

    private void Readonly()
    {
        startDate.Attributes.Add("readonly", "readonly");
        endDate.Attributes.Add("readonly", "readonly");
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

        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex>0)
        {
            Session["cid"] = "";
            Session["cid"] = ddlCompany.SelectedValue;

            using (DataTable dt = _commonDataLoad.GetDDLFinYearByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlFinYear.DataSource = dt;
                ddlFinYear.DataValueField = "Value";
                ddlFinYear.DataTextField = "TextField";
                ddlFinYear.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLDepartmentByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
        }
        
    }

    public void clear()
    {
        hfJobID.Value = string.Empty;
        txt_JobCirculation.Text = string.Empty;
        ddlJobCirculation.Items.Clear();
    }

    protected void txt_JobCirculation_OnTextChanged(object sender, EventArgs e)
    {
        if (valdatioon())
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_JobCirculation.Text))
                {
                    string job = txt_JobCirculation.Text;
                    hfJobID.Value = job.Split(':')[0];
                    txt_JobCirculation.Text = job.Split(':')[2];
                    //txt_JobTitle.Text = job.Split(':')[2];
                }
            }
            catch (Exception ex)
            {
                txt_JobCirculation.Text = "";
                hfJobID.Value = "";


                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Input Correct Data !!');",
                    true);
            }
        }
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    private bool valdatioon()
    {
        if (ddlCompany.SelectedIndex <= 0)
        {
            showMessageBox("Please Select Company!!");
            ddlCompany.Focus();
            txt_JobCirculation.Text = "";
            hfJobID.Value = "";
            return false;
        }

        if (ddlFinYear.SelectedIndex <= 0)
        {
            showMessageBox("Please Select Financial Year!!");
            ddlFinYear.Focus();
            txt_JobCirculation.Text = "";
            hfJobID.Value = "";
            return false;
        }

        if (ddlDepartment.SelectedIndex <= 0)
        {
            showMessageBox("Please Select Department!!");
            ddlDepartment.Focus();
            txt_JobCirculation.Text = "";
            hfJobID.Value = "";
            return false;
        }
        return true;
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            Session["DepartmentId"] = "";
            Session["DepartmentId"] = ddlDepartment.SelectedValue;

            using (DataTable dt = _commonDataLoad.GetDDJobCirculation(parm()))
            {
                ddlFinYear.DataSource = dt;
                ddlFinYear.DataValueField = "Value";
                ddlFinYear.DataTextField = "TextField";
                ddlFinYear.DataBind();
            }
            clear();
        }
        else
        {
            Session["DepartmentId"] = "";
            clear();
        }
    }

    private string parm()
    {
        string prm = "";
        if (ddlCompany.SelectedIndex > 0)
        {
            prm = prm + " nd r.CompanyId =" + ddlCompany.SelectedValue;
        }
        if (ddlFinYear.SelectedIndex > 0)
        {
            prm = prm + " nd r.FinYearId =" + ddlFinYear.SelectedValue;
        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            prm = prm + " nd r.DeptId =" + ddlDepartment.SelectedValue;
        }
        return prm;
    }

    protected void ddlFinYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFinYear.SelectedIndex > 0)
        {
            Session["FinYearId"] = "";
            Session["FinYearId"] = ddlFinYear.SelectedValue;
            clear();
        }
        else
        {
            clear();
            Session["FinYearId"] = "";
        }
    }

    protected void startDate_OnTextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(startDate.Text))
        {
            Session["startDate"] = "";
            Session["startDate"] = startDate.Text;
            clear();
        }
    }

    protected void endDate_OnTextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(endDate.Text))
        {
            Session["endDate"] = "";
            Session["endDate"] = endDate.Text;
            clear();
        }
    }
}