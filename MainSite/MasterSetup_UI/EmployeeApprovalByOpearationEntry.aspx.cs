using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_EmployeeApprovalByOpearationEntry : System.Web.UI.Page
{

    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    EmployeeApprovalByOpearationEntryDAL aEmployeeRequsitionDal = new EmployeeApprovalByOpearationEntryDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDroDownList();
        }
    }
    
    private void LoadDroDownList()
    {
        aEmployeeRequsitionDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);

        aEmployeeRequsitionDal.MenuDropDownNew(OperationDropDownList);
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                ((DropDownList)loadGridView.Rows[i].FindControl("serialDropDownList")).Items.Add(j.ToString());
            }    
        }
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
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
    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        aEmployeeRequsitionDal.MenuDropDownNew(OperationDropDownList);
        loadGridView.DataSource = null;
        loadGridView.DataBind();
        if (companyDropDownList.SelectedValue != "")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActive(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
                
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        
    }

    public bool Validation()
    {
        if (companyDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select company !!!!");
            companyDropDownList.Focus();
            return false;
        }
        if (OperationDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select Operation !!!!");
            OperationDropDownList.Focus();
            return false;
        }

        if (ddlEmpInfo.SelectedValue == "")
        {
            ShowMessageBox("Please Enter Employee Name !!!!");
            ddlEmpInfo.Focus();
            return false;
        }
        return true;
    }
    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
           Add();  
        }
    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton ImageButton = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        Remove(rowindex);
    }

    public void Add()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("CompanyId");
        
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("MainMenuId");
        aDataTable.Columns.Add("Serial");
        aDataTable.Columns.Add("ShortName");
        aDataTable.Columns.Add("Operation");
        aDataTable.Columns.Add("EmpName");
      //  aDataTable.Columns.Add("Isheadperson");

        DataRow dataRow = null;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();
            dataRow["CompanyId"] = loadGridView.DataKeys[i][0].ToString();
            dataRow["EmpInfoId"] = loadGridView.DataKeys[i][1].ToString();
            dataRow["MainMenuId"] = loadGridView.DataKeys[i][2].ToString();
            dataRow["Serial"] =
                ((DropDownList) loadGridView.Rows[i].FindControl("serialDropDownList")).SelectedItem.Text;

            dataRow["ShortName"] =  loadGridView.Rows[i].Cells[1].Text;
            dataRow["EmpName"] = loadGridView.Rows[i].Cells[3].Text;
            dataRow["Operation"] = loadGridView.Rows[i].Cells[2].Text;
          //  dataRow["Isheadperson"] = loadGridView.Rows[i].Cells[4].Text;

          

            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();

        dataRow["CompanyId"] = companyDropDownList.SelectedValue;
        dataRow["EmpInfoId"] = ddlEmpInfo.SelectedValue;
        dataRow["ShortName"] = companyDropDownList.SelectedItem.Text;
        dataRow["MainMenuId"] = OperationDropDownList.SelectedValue;
        dataRow["Operation"] = OperationDropDownList.SelectedItem.Text;
        dataRow["EmpName"] = ddlEmpInfo.SelectedItem.Text;
       

        aDataTable.Rows.Add(dataRow);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

      //  companyDropDownList.SelectedValue = "";
        repEmpIdHiddenField.Value = string.Empty;
        ddlEmpInfo.SelectedValue = null;
        //OperationDropDownList.SelectedValue = ""; ;
        //OperationDropDownList.SelectedValue = string.Empty;
        directlySuperTextBox.Text = string.Empty;
        for (int i = 0; i < aDataTable.Rows.Count; i++)
        {
            ((DropDownList) loadGridView.Rows[i].FindControl("serialDropDownList")).SelectedValue =
                aDataTable.Rows[i]["Serial"].ToString();
        }
    }
    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("CompanyId");
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("MainMenuId");

        aDataTable.Columns.Add("ShortName");
        aDataTable.Columns.Add("Operation");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("Serial");
     //   aDataTable.Columns.Add("Serial");

        DataRow dataRow = null;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();
                dataRow["CompanyId"] = loadGridView.DataKeys[i][0].ToString();
                dataRow["EmpInfoId"] = loadGridView.DataKeys[i][1].ToString();
                dataRow["MainMenuId"] = loadGridView.DataKeys[i][2].ToString();

                dataRow["ShortName"] = loadGridView.Rows[i].Cells[1].Text;
                dataRow["EmpName"] = loadGridView.Rows[i].Cells[3].Text;
                dataRow["Operation"] = loadGridView.Rows[i].Cells[2].Text;
                dataRow["Serial"] =
              ((DropDownList)loadGridView.Rows[i].FindControl("serialDropDownList")).SelectedItem.Text;

                aDataTable.Rows.Add(dataRow);
            }
        }
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
        for (int i = 0; i < aDataTable.Rows.Count; i++)
        {
            ((DropDownList)loadGridView.Rows[i].FindControl("serialDropDownList")).SelectedValue =
                aDataTable.Rows[i]["Serial"].ToString();
        }



    }

    protected void SearchEmployeeNameTextBox_OnTextChanged(object sender, EventArgs e)
    {
         
    }

    protected void directlySuperTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = directlySuperTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':', ':');

            directlySuperTextBox.Text = emp[2];
            repEmpIdHiddenField.Value = emp[0];
        }
        else
        {
            repEmpIdHiddenField.Value = "";
            directlySuperTextBox.Text = "";

            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();
        aTable = aEmployeeRequsitionDal.GetOperationInfo(OperationDropDownList.SelectedValue,companyDropDownList.SelectedValue);

        if (aTable.Rows.Count > 0)
        {
            EmployeeApprovalByOpearationMasterDao atblEmployeePromotionEntryDAO =
            new EmployeeApprovalByOpearationMasterDao();
            atblEmployeePromotionEntryDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
            atblEmployeePromotionEntryDAO.EntryDate = DateTime.Now;


            bool status =
                aEmployeeRequsitionDal.DeleteEmpTransferAndRedesignationDSSaveInfo(OperationDropDownList.SelectedValue,
                    companyDropDownList.SelectedValue);
            int id = Convert.ToInt32(aTable.Rows[0]["EmployeeApprovalByOpearationMasterId"].ToString());
                //aEmployeeRequsitionDal.SaveInfo(atblEmployeePromotionEntryDAO);

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                EmployeeApprovalByOpearationDetails andRedesignationDao = new EmployeeApprovalByOpearationDetails();

                andRedesignationDao.CompanyId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
                andRedesignationDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][1].ToString());
                andRedesignationDao.Serial =
                    Convert.ToInt32(
                        ((DropDownList)loadGridView.Rows[i].FindControl("serialDropDownList")).SelectedItem.Text);
                andRedesignationDao.Operation = Convert.ToInt32(loadGridView.DataKeys[i][2].ToString());
                andRedesignationDao.MasterId = id;

                andRedesignationDao.Isheadperson =
                    Convert.ToBoolean(
                        ((CheckBox)loadGridView.Rows[i].FindControl("chkIsheadperson")).Checked);

                //;
                int idd = aEmployeeRequsitionDal.EmpTransferAndRedesignationDSSaveInfo(andRedesignationDao);

                //------------------------------------- Update supervised info -------------------------------------------------------------------------
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation successfully done...');window.location ='EmployeeApprovalByOpearationEntry.aspx';",
                    true);
            }
         
        }
        else
        {
            EmployeeApprovalByOpearationMasterDao atblEmployeePromotionEntryDAO =
             new EmployeeApprovalByOpearationMasterDao();
            atblEmployeePromotionEntryDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
            atblEmployeePromotionEntryDAO.EntryDate = DateTime.Now;

            int id = aEmployeeRequsitionDal.SaveInfo(atblEmployeePromotionEntryDAO);

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                EmployeeApprovalByOpearationDetails andRedesignationDao = new EmployeeApprovalByOpearationDetails();

                andRedesignationDao.CompanyId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
                andRedesignationDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][1].ToString());
                andRedesignationDao.Serial =
                    Convert.ToInt32(
                        ((DropDownList)loadGridView.Rows[i].FindControl("serialDropDownList")).SelectedItem.Text);
                andRedesignationDao.Operation = Convert.ToInt32(loadGridView.DataKeys[i][2].ToString());
                andRedesignationDao.MasterId = id;
                andRedesignationDao.Isheadperson =
                    Convert.ToBoolean(
                        ((CheckBox)loadGridView.Rows[i].FindControl("chkIsheadperson")).Checked);

                //bool status = aEmployeeRequsitionDal.DeleteEmpTransferAndRedesignationDSSaveInfo(andRedesignationDao);
                int idd = aEmployeeRequsitionDal.EmpTransferAndRedesignationDSSaveInfo(andRedesignationDao);

                //------------------------------------- Update supervised info -------------------------------------------------------------------------
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation successfully done...');window.location ='EmployeeApprovalByOpearationEntry.aspx';",
                    true);
            }
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }
    protected void serialDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            int rowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;

            DropDownList chkRow = ((DropDownList)loadGridView.Rows[rowIndex].Cells[2].FindControl("serialDropDownList"));
            DropDownList rrrrr = ((DropDownList)loadGridView.Rows[i].Cells[2].FindControl("serialDropDownList"));
            if (chkRow.SelectedValue != rrrrr.SelectedValue)
            {
                // aShowMessage.ShowMessageBox("Not Same",this);
            }
            else
            {
             //   aShowMessage.ShowMessageBox("Sl No Can't be Same!!", this);
              //  chkRow.SelectedValue = null;
            }
        }
    }
    protected void OperationDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();
        aTable = aEmployeeRequsitionDal.GetOperationInfo(OperationDropDownList.SelectedValue,companyDropDownList.SelectedValue);

        if (aTable.Rows.Count>0)
        {
             loadGridView.DataSource = aTable;
             loadGridView.DataBind();
             for (int i = 0; i < aTable.Rows.Count; i++)
             {
                 ((DropDownList)loadGridView.Rows[i].FindControl("serialDropDownList")).SelectedValue =
                     aTable.Rows[i]["serial"].ToString();
             }

             for (int i = 0; i < aTable.Rows.Count; i++)
             {
                 if (aTable.Rows[i]["Isheadperson"].ToString()!="")
                 {

                     if (aTable.Rows[i]["Isheadperson"].ToString()=="1")
                     {
                         ((CheckBox)loadGridView.Rows[i].FindControl("chkIsheadperson")).Checked=true;
                     }
                     else
                     {
                         ((CheckBox)loadGridView.Rows[i].FindControl("chkIsheadperson")).Checked = false;
                     }
                 }
                 else
                 {
                     ((CheckBox)loadGridView.Rows[i].FindControl("chkIsheadperson")).Checked = false;
                 }

                
                      
             }
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
        
    }

    protected void ddlEmpInfo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlEmpInfo.SelectedValue != "")
        {
            repEmpIdHiddenField.Value = ddlEmpInfo.SelectedValue;
        }
        else
        {
            repEmpIdHiddenField.Value = "";
        }

      
    }
}