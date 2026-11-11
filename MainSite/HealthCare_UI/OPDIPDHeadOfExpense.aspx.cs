using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using Microsoft.Ajax.Utilities;

public partial class HealthCare_UI_OPDIPDHeadOfExpense : System.Web.UI.Page
{


    IPDOPDHeadOfExpenseDal aExpenseDal = new IPDOPDHeadOfExpenseDal();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //ViewState["RowNumber"] = 1;
            LoadInitialDDL();
            SetInitialRow();


            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                hfMasterId.Value = Request.QueryString["MID"].ToString();
       
                onRecord(Convert.ToInt32(Request.QueryString["MID"]));

                inlineRadio1.Enabled = false;
                inlineRadio2.Enabled = false;
            }
        }


    }

    private int Mcount = 0;
    private void onRecord(int id)
    {

        if (Convert.ToString(Session["HeadView"]) == "View")
        {
            SearchButton.Visible = false;
            btnReset.Visible = false;
            Session["HeadView"] = "";
        }
        else
        {
            SearchButton.Visible = false;
            btnUpdate.Visible = true;
        }

        DataTable onDt = aExpenseDal.Get_HeadOfExpense(id);

        if (onDt.Rows.Count > 0)
        {

            if (Convert.ToBoolean(onDt.Rows[0]["IsOPD"].ToString()))
            {
                inlineRadio2.Checked = true;

            }
            else
            {
                inlineRadio1.Checked = true;
            }

            ddlCompany.SelectedValue = onDt.Rows[0]["CompanyId"].ToString();


            loadGridView.DataSource = null;
            loadGridView.DataBind();
            loadGridView.DataSource = onDt;
            loadGridView.DataBind();
            ViewState["CurrentTable"] = onDt;
            if ( inlineRadio2.Checked == true)
            {
                loadGridView.Columns[4].Visible = false;
                 
            }
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox IsActive = (CheckBox)loadGridView.Rows[i].FindControl("IsActive");
                CheckBox IsMaternity = (CheckBox)loadGridView.Rows[i].FindControl("IsMaternity");

                LinkButton remove = (LinkButton)loadGridView.Rows[i].FindControl("LinkButton1");

                bool Active = false;
                try
                {
                      Active = Convert.ToBoolean(loadGridView.DataKeys[i][0]);
                }
                catch (Exception)
                {
                    
                    //throw;
                }


                bool Maternity = false;
                try
                {
                      Maternity = Convert.ToBoolean(loadGridView.DataKeys[i][1]);
                }
                catch (Exception ex)
                {
                    
                }


                if (Active)
                {
                    IsActive.Checked = true;
                }

                if (Maternity)
                {
                    IsMaternity.Checked = true;
                }

                //if (btnUpdate.Visible)
                //{
              
                //    DataTable chekdt = aExpenseDal.Get_HeadEntryCheck(id);

                //    if (chekdt.Rows.Count > 0)
                //    {
                //        Mcount++;
                //        remove.Visible = false;
                //    }
                //}
              

            }

            //HFCount.Value = Mcount.ToString();

        }
    }


    private void LoadInitialDDL()
    {

        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedValue = 1.ToString();
        }

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("OPDIPDHeadOfExpenseView.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }


    protected void IPD_Click(object sender, EventArgs e)
    {


        if (inlineRadio1.Checked)
        {
            inlineRadio2.Checked = false;
            loadGridView.Columns[5].Visible = true;
        }
        else
        {
            inlineRadio1.Checked = true;
        }     
    }



    protected void OPD_Click(object sender, EventArgs e)
    {


        if (inlineRadio2.Checked)
        {
            inlineRadio1.Checked = false;
            loadGridView.Columns[4].Visible = false;
            
        }
        else
        {
            inlineRadio1.Checked = true;
        }
    }


    protected void ButtonAdd_Click(object sender, EventArgs e)
    {

        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();

        aTable.Columns.Add(new DataColumn("OIPDHeadOfExpenseId", typeof(string)));
        aTable.Columns.Add(new DataColumn("HeadOfExpense", typeof(string)));
        aTable.Columns.Add(new DataColumn("Amount", typeof(string)));
        aTable.Columns.Add(new DataColumn("Remarks", typeof(string)));
        aTable.Columns.Add(new DataColumn("IsActive", typeof(string)));
        aTable.Columns.Add(new DataColumn("IsMaternity", typeof(string)));

        DataRow dr;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            HiddenField hfOIPDHeadOfExpenseId = (HiddenField)loadGridView.Rows[i].Cells[1].FindControl("hfOIPDHeadOfExpenseId");
            TextBox HeadOfExpense = (TextBox)loadGridView.Rows[i].FindControl("HeadOfExpense");
            TextBox Amount = (TextBox)loadGridView.Rows[i].FindControl("Amount");
            TextBox Remarks = (TextBox)loadGridView.Rows[i].FindControl("Remarks");
            CheckBox IsActive = (CheckBox)loadGridView.Rows[i].FindControl("IsActive");
            CheckBox IsMaternity = (CheckBox)loadGridView.Rows[i].FindControl("IsMaternity");
            dr["OIPDHeadOfExpenseId"] = hfOIPDHeadOfExpenseId.Value;
            dr["HeadOfExpense"] = HeadOfExpense.Text;
            dr["Amount"] = Amount.Text;
            dr["Remarks"] = Remarks.Text;
            dr["IsActive"] = IsActive.Checked;

            dr["IsMaternity"] = IsMaternity.Checked;

            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();

                //dr["OIPDHeadOfExpenseId"] = string.Empty;
                //dr["HeadOfExpense"] = HeadOfExpense.Text;
                //dr["Amount"] = Amount.Text;
                //dr["Remarks"] = Remarks.Text;
                //dr["IsActive"] = IsActive.Checked;

                //dr["IsMaternity"] = IsMaternity.Checked;

                dr["OIPDHeadOfExpenseId"] = "0";
                dr["HeadOfExpense"] = string.Empty;
                dr["Amount"] = string.Empty;
                dr["Remarks"] = string.Empty;
                dr["IsActive"] = false;

                dr["IsMaternity"] = false;

                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aTable;
        ViewState["CurrentTable"] = aTable;

        loadGridView.DataBind();


        int count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
             count++;


             CheckBox IsActive = (CheckBox)loadGridView.Rows[i].FindControl("IsActive");
             CheckBox IsMaternity = (CheckBox)loadGridView.Rows[i].FindControl("IsMaternity");

             bool Active = Convert.ToBoolean(loadGridView.DataKeys[i][0]);
             bool Maternity = Convert.ToBoolean(loadGridView.DataKeys[i][1]);

             if (Active)
             {
                 IsActive.Checked = true;
             }

             if (Maternity)
             {
                 IsMaternity.Checked = true;
             }

            //



            //LinkButton remove = (LinkButton)loadGridView.Rows[i].FindControl("LinkButton1");

            //if (btnUpdate.Visible)
            //{
            //    if (count <= Convert.ToInt32(HFCount.Value))
            //    {    
            //        remove.Visible = false;
            //    }
       
            //}

        }


     
      //  AddNewRowToGrid();
    }

    private void SetInitialRow()
    {

       
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("OIPDHeadOfExpenseId", typeof(string)));
            dt.Columns.Add(new DataColumn("HeadOfExpense", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(string)));
            dt.Columns.Add(new DataColumn("IsMaternity", typeof(string)));
            dr = dt.NewRow();
            dr["OIPDHeadOfExpenseId"] = "0";
            dr["HeadOfExpense"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["Remarks"] = string.Empty;
            dr["IsActive"] = string.Empty;
            dr["IsMaternity"] = string.Empty;

            dt.Rows.Add(dr);
            //dr = dt.NewRow();

            //Store the DataTable in ViewState
          //   ViewState["CurrentTable"] = dt;
            loadGridView.DataSource = dt;
            loadGridView.DataBind();
          
    }


    private void AddNewRowToGrid()
    {


        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox HeadOfExpense = (TextBox)loadGridView.Rows[rowIndex].Cells[1].FindControl("HeadOfExpense");
                    TextBox Amount = (TextBox)loadGridView.Rows[rowIndex].Cells[2].FindControl("Amount");
                    TextBox Remarks = (TextBox)loadGridView.Rows[rowIndex].Cells[3].FindControl("Remarks");
                    CheckBox IsActive = (CheckBox)loadGridView.Rows[rowIndex].Cells[4].FindControl("IsActive");
                    CheckBox IsMaternity = (CheckBox)loadGridView.Rows[rowIndex].Cells[5].FindControl("IsMaternity");
              
                    drCurrentRow = dtCurrentTable.NewRow();

                    //drCurrentRow["RowNumber"] = (int)ViewState["RowNumber"] + 1;
                    drCurrentRow["HeadOfExpense"] = HeadOfExpense.Text;
                    drCurrentRow["Amount"] = Amount.Text;
                    drCurrentRow["Remarks"] = Remarks.Text;
                    drCurrentRow["IsActive"] = IsActive.Checked;
                    drCurrentRow["IsMaternity"] = IsMaternity.Checked;
                    rowIndex++;
                }
                //ViewState["RowNumber"] = (int)ViewState["RowNumber"] + 1;
                dtCurrentTable.Rows.Add(drCurrentRow);
             
                ViewState["CurrentTable"] = dtCurrentTable;

                loadGridView.DataSource = dtCurrentTable;
                loadGridView.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    HiddenField hfOIPDHeadOfExpenseId = (HiddenField)loadGridView.Rows[rowIndex].Cells[1].FindControl("hfOIPDHeadOfExpenseId");

                    TextBox HeadOfExpense = (TextBox)loadGridView.Rows[rowIndex].Cells[1].FindControl("HeadOfExpense");
                    TextBox Amount = (TextBox)loadGridView.Rows[rowIndex].Cells[2].FindControl("Amount");
                    TextBox Remarks = (TextBox)loadGridView.Rows[rowIndex].Cells[3].FindControl("Remarks");
                    CheckBox IsActive = (CheckBox)loadGridView.Rows[rowIndex].Cells[4].FindControl("IsActive");
                    CheckBox IsMaternity = (CheckBox)loadGridView.Rows[rowIndex].Cells[5].FindControl("IsMaternity");


                    HeadOfExpense.Text = dt.Rows[i]["HeadOfExpense"].ToString();
                    hfOIPDHeadOfExpenseId.Value = dt.Rows[i]["hfOIPDHeadOfExpenseId"].ToString();
                    Amount.Text = dt.Rows[i]["Amount"].ToString();
                    Remarks.Text = dt.Rows[i]["Remarks"].ToString();
                    IsActive.Checked = Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString());
                    IsMaternity.Checked = Convert.ToBoolean(dt.Rows[i]["IsMaternity"].ToString());

                    rowIndex++;

                }
            }
            // ViewState["CurrentTable"] = dt;

        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }

    protected void loadGridView_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
             LinkButton ad = (LinkButton)e.Row.FindControl("ButtonAdd");

        


            if (lb != null)
            {
                //if (dt.Rows.Count > 1)
                //{
                //    if (e.Row.RowIndex == dt.Rows.Count - 1)
                //    {
                //        lb.Visible = false;
                //    }
                //}
                //else
                //{
                //    lb.Visible = false;
                //}
            }
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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex ;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            HiddenField hfOIPDHeadOfExpenseId = (HiddenField)loadGridView.Rows[rowID].Cells[1].FindControl("hfOIPDHeadOfExpenseId");

            if (hfOIPDHeadOfExpenseId.Value=="0")
            {
                dt.Rows.Remove(dt.Rows[rowID]);
                if (dt.Rows.Count > 0)
                {
                    //Store the current data in ViewState for future reference  
                    ViewState["CurrentTable"] = dt;
                    //Re bind the GridView for the updated data  
                    loadGridView.DataSource = dt;
                    loadGridView.DataBind();
                }
                else
                {
                    ViewState["CurrentTable"] = null;
                    //Re bind the GridView for the updated data  
                    loadGridView.DataSource = null;
                    loadGridView.DataBind();
                }
            }
            else
            {
                AlertMessageBoxShow("Can not be Deleted");
            }

            
        }




        int count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            count++;
            LinkButton remove = (LinkButton)loadGridView.Rows[i].FindControl("LinkButton1");

            CheckBox IsActive = (CheckBox)loadGridView.Rows[i].FindControl("IsActive");
            CheckBox IsMaternity = (CheckBox)loadGridView.Rows[i].FindControl("IsMaternity");

            bool Active =false;

            try
            {
                Active = Convert.ToBoolean(loadGridView.DataKeys[i][0]);
            }
            catch (Exception)
            {
                
                //throw;
            }
            bool Maternity = false;

            try
            {
                Maternity = Convert.ToBoolean(loadGridView.DataKeys[i][1]);
            }
            catch (Exception)
            {
                
                //throw;
            }

            if (Active)
            {
                IsActive.Checked = true;
            }

            if (Maternity)
            {
                IsMaternity.Checked = true;
            }
            //
           

        }


        //Set Previous Data on Postbacks
       // SetPreviousData();
    }



    private bool Validation()
    {


        if (inlineRadio1.Checked == false && inlineRadio2.Checked == false)
        {
            
            AlertMessageBoxShow("Please Select IPD/OPD");
            return false;
        }


        //if (loadGridView.DataSource == null)
        //{
        //    AlertMessageBoxShow("Please input minimum one row");
        //    return false;
        //}
        int count = 0;
        for (int rowIndex = 0; rowIndex < loadGridView.Rows.Count; rowIndex++)
        {

            TextBox HeadOfExpense = (TextBox)loadGridView.Rows[rowIndex].Cells[1].FindControl("HeadOfExpense");
            CheckBox IsActive = (CheckBox)loadGridView.Rows[rowIndex].Cells[1].FindControl("IsActive");

            if (HeadOfExpense.Text=="")
            {
                AlertMessageBoxShow("Please Input Head of Expense");
                return false;
            }
        

            if (IsActive.Checked)
            {
                count = count + 1;
            }

            

        }

        if (count == 0)
        {
            AlertMessageBoxShow("Please select at lease one Is Active Checkbox");
            return false;
        }
        return true;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        if (Validation())
        {

           

             

                    HeadOfExpenseMaster master = new HeadOfExpenseMaster();

                    master.HeadOfExpenseMasterId = 0;

                    master.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    if (inlineRadio1.Checked)
                    {
                        master.IsOPD = false;
                    }
                    else
                    {
                        master.IsOPD = true;
                    }

                    master.EntryBy = Convert.ToInt32(Session["UserId"].ToString());
                    master.EntryDate = DateTime.Now;


                    List<IPDOPDHeadOfExpenseDao> aList = new List<IPDOPDHeadOfExpenseDao>();

                    //IPDOPDHeadOfExpenseDao aExpenseDao = new IPDOPDHeadOfExpenseDao();

                    for (int rowIndex = 0; rowIndex < loadGridView.Rows.Count; rowIndex++)
                    {
                        HiddenField hfOIPDHeadOfExpenseId = (HiddenField)loadGridView.Rows[rowIndex].Cells[1].FindControl("hfOIPDHeadOfExpenseId");
                        TextBox HeadOfExpense = (TextBox) loadGridView.Rows[rowIndex].Cells[1].FindControl("HeadOfExpense");
                        TextBox Amount = (TextBox) loadGridView.Rows[rowIndex].Cells[2].FindControl("Amount");
                        TextBox Remarks = (TextBox) loadGridView.Rows[rowIndex].Cells[3].FindControl("Remarks");
                        CheckBox IsActive = (CheckBox) loadGridView.Rows[rowIndex].Cells[4].FindControl("IsActive");
                        CheckBox IsMaternity = (CheckBox) loadGridView.Rows[rowIndex].Cells[5].FindControl("IsMaternity");


                        

                        IPDOPDHeadOfExpenseDao aExpenseDao = new IPDOPDHeadOfExpenseDao();
                        try
                        {
                            aExpenseDao.OIPDHeadOfExpenseId = Convert.ToInt32(hfOIPDHeadOfExpenseId.Value);
                        }
                        catch (Exception)
                        {
                            aExpenseDao.OIPDHeadOfExpenseId = 0;
                            //throw;
                        }
                        aExpenseDao.HeadOfExpense = HeadOfExpense.Text;
                        aExpenseDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        if (Amount.Text != "")
                        {
                            aExpenseDao.Amount = Convert.ToDecimal(Amount.Text);
                        }
                        else
                        {
                            aExpenseDao.Amount = 0;
                        }

                  
                        aExpenseDao.Remaks = Remarks.Text;
                        aExpenseDao.IsActive = IsActive.Checked;

                        if (inlineRadio1.Checked)
                        {
                            aExpenseDao.IsMaternity = IsMaternity.Checked;
                        }
                    
                        if (inlineRadio1.Checked)
                        {
                            aExpenseDao.IsOPD = false;
                        }
                        else
                        {
                            aExpenseDao.IsOPD = true;
                        }

                        aList.Add(aExpenseDao);
                    }

                    Int32 OIPDHeadOfExpenseId = aExpenseDal.SaveHeadOfExpense(master,aList);

                    if (OIPDHeadOfExpenseId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='OPDIPDHeadOfExpenseView.aspx';",
                            true);

                    }
                    else
                    {
                        AlertMessageBoxShow("Already Exist!");
                    }

              

        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {

        if (Validation())
        {

            

           

                    HeadOfExpenseMaster master = new HeadOfExpenseMaster();

                    master.HeadOfExpenseMasterId = int.Parse(hfMasterId.Value);

                    master.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    if (inlineRadio1.Checked)
                    {
                        master.IsOPD = false;
                    }
                    else
                    {
                        master.IsOPD = true;
                    }

                    master.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            
                    List<IPDOPDHeadOfExpenseDao> aList = new List<IPDOPDHeadOfExpenseDao>();

                    //IPDOPDHeadOfExpenseDao aExpenseDao = new IPDOPDHeadOfExpenseDao();

                    for (int rowIndex = 0; rowIndex < loadGridView.Rows.Count; rowIndex++)
                    {
                        HiddenField hfOIPDHeadOfExpenseId = (HiddenField)loadGridView.Rows[rowIndex].Cells[1].FindControl("hfOIPDHeadOfExpenseId");
                        TextBox HeadOfExpense = (TextBox)loadGridView.Rows[rowIndex].Cells[1].FindControl("HeadOfExpense");
                        TextBox Amount = (TextBox)loadGridView.Rows[rowIndex].Cells[2].FindControl("Amount");
                        TextBox Remarks = (TextBox)loadGridView.Rows[rowIndex].Cells[3].FindControl("Remarks");
                        CheckBox IsActive = (CheckBox)loadGridView.Rows[rowIndex].Cells[4].FindControl("IsActive");
                        CheckBox IsMaternity = (CheckBox)loadGridView.Rows[rowIndex].Cells[5].FindControl("IsMaternity");


                      
                        IPDOPDHeadOfExpenseDao aExpenseDao = new IPDOPDHeadOfExpenseDao();

                        try
                        {
                            aExpenseDao.OIPDHeadOfExpenseId = Convert.ToInt32(hfOIPDHeadOfExpenseId.Value);
                        }
                        catch (Exception)
                        {
                            aExpenseDao.OIPDHeadOfExpenseId = 0;
                            //throw;
                        }
                        aExpenseDao.HeadOfExpense = HeadOfExpense.Text;
                        aExpenseDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        if (Amount.Text != "")
                        {
                            aExpenseDao.Amount = Convert.ToDecimal(Amount.Text);
                        }
                        else
                        {
                            aExpenseDao.Amount = 0;
                        }


                        aExpenseDao.Remaks = Remarks.Text;
                        aExpenseDao.IsActive = IsActive.Checked;

                        if (inlineRadio1.Checked)
                        {
                            aExpenseDao.IsMaternity = IsMaternity.Checked;
                        }

                        if (inlineRadio1.Checked)
                        {
                            aExpenseDao.IsOPD = false;
                        }
                        else
                        {
                            aExpenseDao.IsOPD = true;
                        }

                        aList.Add(aExpenseDao);
                    }

                    bool DHeadOfExpenseId = aExpenseDal.Update_Data(master, aList);

                    if (DHeadOfExpenseId)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successfull Done...');window.location ='OPDIPDHeadOfExpenseView.aspx';",
                            true);

                    }
                    else
                    {
                        AlertMessageBoxShow("Already Exist!");
                    }

              

        }
    } 
}