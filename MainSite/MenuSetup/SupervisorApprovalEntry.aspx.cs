using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.UserPermissions_DAL;
using DAO.UA_DAO;

public partial class MenuSetup_SupervisorApprovalEntry : System.Web.UI.Page
{
    SupervisorMenuAppDAL appDal=new SupervisorMenuAppDAL();
    CommonDataLoadDAL aDataLoadDal=new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DropDownList();
        }
    }

    public void DropDownList()
    {
        aDataLoadDal.CompanyDropDown(companyDropDownList," ");
        aDataLoadDal.MenuDropDown(menuDropDownList);
    }

    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                if (chkSingle.Visible)
                {
                    chkSingle.Checked = true;
                }
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

    public void LoadEmp()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            appDal.LoadEmployeeDrop((DropDownList)loadGridView.Rows[i].FindControl("employeeDropDownList"), loadGridView.DataKeys[i]["DepartmentId"].ToString());
            ((DropDownList) loadGridView.Rows[i].FindControl("employeeDropDownList")).SelectedValue =
                loadGridView.DataKeys[i]["EmpInfoId"].ToString();
            CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
            chkSingle.Checked = Convert.ToBoolean(loadGridView.DataKeys[i]["IsChecked"].ToString());

        }
    }

    public void Save()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            try
            {
                CheckBox chkSingle = (CheckBox)loadGridView.Rows[i].FindControl("chkSingle");
                if (chkSingle.Checked)
                {


                    SupervisorMenuAppDAO appDao = new SupervisorMenuAppDAO()
                    {
                        CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue),
                        EmpInfoId =
                            Convert.ToInt32(
                                ((DropDownList) loadGridView.Rows[i].FindControl("employeeDropDownList")).SelectedValue),
                        MainMenuId = Convert.ToInt32(loadGridView.DataKeys[i]["MainMenuId"].ToString()),
                        SuperMenuAppId =
                            string.IsNullOrEmpty(loadGridView.DataKeys[i]["SuperMenuAppId"].ToString())
                                ? 0
                                : Convert.ToInt32(loadGridView.DataKeys[i]["SuperMenuAppId"].ToString()),


                    };
                    int id = appDal.SaveSupervisorApp(appDao);
                }
            }
            catch (Exception)
            {
                
                
            }
            
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Saved Successfully...');window.location ='SupervisorApprovalEntry.aspx';",
                   true);
        
    }
    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //aDataLoadDal.DeptByCompanyDropDown(deptDropDownList,companyDropDownList.SelectedValue);
        menuDropDownList.SelectedIndex = 0;
        loadGridView.DataSource = null;
        loadGridView.DataBind();
    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        Save();
    }

    protected void deptDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void menuDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dtdata = appDal.GetData(companyDropDownList.SelectedValue,menuDropDownList.SelectedValue);
         if (dtdata.Rows.Count>0)
        {
              loadGridView.DataSource = dtdata;
        loadGridView.DataBind();
        LoadEmp();
        }
        else
         {
             loadGridView.DataSource = null;
             loadGridView.DataBind();
         }
    }
}