using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.ExitManagement_DAL;
using DAL.MasterSetup_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Medica_Ul_EmpMedicalCheckUp : System.Web.UI.Page
{
    private int mid = 0;
    private int _userId;
    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();
    EmpExitDal aExitDal = new EmpExitDal();
    VivaSetupInfoEntryDAL aDaL = new VivaSetupInfoEntryDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDate.Attributes.Add("readonly", "readonly");
        if (Session["UserId"] != null)
        {
            _userId = int.Parse(Session["UserId"].ToString());
        }

        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();
          
            if (Session["VacancyCirculationId"] != null)
            {
                try
                {

                    mid = Convert.ToInt32(Session["VacancyCirculationId"].ToString());
                    (m_hdpkd.Value) = mid.ToString();
                    if (mid > 0)
                    {

                        using (var db = new HRIS_SMC_DBEntities())
                        {
                            var Bat = (from j in db.tblMedicalCheckUpMasters where j.MedicalCheckUpMasterId == mid select j).FirstOrDefault();
                            ddlCompany.SelectedValue = Bat.CompanyId.ToString();
                            txt_EmpName.Enabled = true;
                            Session["CompanyId"] = ddlCompany.SelectedValue;
                            txtComments.Text = Bat.Comments;
                            txtDate.Text = Convert.ToDateTime(Bat.Date).ToString("dd-MMM-yyyy");


                            DataTable dtedu = aDaL.LoadMedicalEditLoadById(mid.ToString());
                            if (dtedu.Rows.Count>0)
                            {
                                GridView1.DataSource = dtedu;
                                GridView1.DataBind();
                            }

                            

                        }
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
                Session["VacancyCirculationId"] = null;
            }
        }
        
    }


    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("EmpMedicalCheckUpView.aspx");
        }

    }
    private void LoadDropDownList()
    {
        aExitDal.LoadCompanyDropDownList(ddlCompany);

    }



    public string GetReportingEmpId(string empinfoId)
    {
        DataTable dtdata = aExitDal.LoadEmployeeInfoDeptWise(empinfoId);
        if (dtdata.Rows.Count > 0)
        {
            return "'" + dtdata.Rows[0]["ReportingEmpId"].ToString() + "'";
        }
        else
        {
            return "'0'";
        }
    }
    private int SaveExitDetailInfo(List<EmpExitDetailDao> aList)
    {
        Int32 id = 0;

        foreach (var aDao in aList)
        {
            id = aExitDal.SaveExitDetailInfo(aDao);
        }

        return id;
    }

    

   
    private bool SaveDataValidation()
    {
        if (ddlCompany.SelectedValue == "")
        {
            ShowMessageBox("You have to select company !!!");
            return false;
        }

        if (hfEmpInfoId.Value == "")
        {
            ShowMessageBox("You have to select employee !!!");
            return false;
        }

       
        

        return true;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlCompany.SelectedValue = "";
        txt_EmpName.Text = "";
        hfEmpInfoId.Value = "";
        empName.Text = "";
        empCode.Text = "";
        ddlDivision.Text = "";
        
        ddlDesignation.Text = "";
      
        txt_EmpName.Enabled = false;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            txt_EmpName.Enabled = true;

            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;

            //LoadDepartmentCheckBoxList();
        }
        else
        {
            Session["CompanyId"] = "";
            txt_EmpName.Enabled = false;
        }


    }

 
    
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
            string empName = txt_EmpName.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                txt_EmpName.Text = emp[1];
                repEmpIdHiddenField.Value = emp[2];

                LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
              
               
            }
            else
            {

                txt_EmpName.Text = "";
                repEmpIdHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }
        }

        else
        {
            txt_EmpName.Text = "";
            repEmpIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("please Select a Company !!", this);
            ddlCompany.Focus();
        }
    }


    public void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aEmployeeJobLeftEntryDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {


            //EmployeeNameTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
            //DesignationTextBox.Text = dtdata.Rows[0]["Designation"].ToString();
            //JoiningDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            //SalaryGradeTextBox.Text = dtdata.Rows[0]["GradeName"].ToString();


            empName.Text = dtdata.Rows[0]["EmpName"].ToString();

           
            empCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
          
            ddlDesignation.Text = dtdata.Rows[0]["Designation"].ToString();


            ddlDivision.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            


        }
    }


    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    private void SetEmployeeInfo()
    {
        string empName = txt_EmpName.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            hfEmpInfoId.Value = emp[2];
        }
        else
        {
            hfEmpInfoId.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }

        txt_EmpName.Text = "";
    }


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            SaveUpdate();
        }
      
    }

    private void SaveUpdate()
    {
        try
        {
            using (var db = new HRIS_SMC_DBEntities())
            {
                tblMedicalCheckUpMaster mast = null;
                mid = string.IsNullOrEmpty(m_hdpkd.Value) ? 0 : int.Parse(m_hdpkd.Value);

                if (mid > 0)
                {
                    mast = db.tblMedicalCheckUpMasters.FirstOrDefault(ici => ici.MedicalCheckUpMasterId == mid);


                    mast.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    mast.Date = Convert.ToDateTime(txtDate.Text);
                    mast.Comments = txtComments.Text;

                    mast.UpdateBy = _userId;
                    mast.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                }

                else
                {
                    mast = new tblMedicalCheckUpMaster();
                    mast.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    mast.Date = Convert.ToDateTime(txtDate.Text);
                    mast.Comments = txtComments.Text;

                    mast.EntryBy = _userId;
                    mast.EntryDate = DateTime.Now;
                    db.tblMedicalCheckUpMasters.Add(mast);
                    db.SaveChanges();
                }

                db.Database.ExecuteSqlCommand("Delete FROM dbo.tblMedicalCheckUp WHERE MasterId={0}",
                                               Convert.ToInt32(mast.MedicalCheckUpMasterId));
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    int empId = Convert.ToInt32(GridView1.DataKeys[i][0].ToString());
                    int comId = Convert.ToInt32(GridView1.DataKeys[i][1].ToString());


                    string Remarks = ((TextBox) GridView1.Rows[i].FindControl("txtRemarks")).Text;


                    tblMedicalCheckUp Bat = null;

                    

                   
                    
                        Bat = new tblMedicalCheckUp();
                        Bat.EmpInfoId = empId;
                        Bat.MasterId = mast.MedicalCheckUpMasterId;
                        Bat.CompanyId = comId;

                        Bat.Remarks = Remarks;
                        Bat.EntryBy = _userId;
                        Bat.EntryDate = DateTime.Now;
                        db.tblMedicalCheckUps.Add(Bat);
                     
                }
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation Successful...! ');window.location ='EmpMedicalCheckUpView.aspx';",
                                true);
            }
        }


        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void textButton_OnClick(object sender, EventArgs e)
    {
        if (AddJobTitleValidation())
        {
            int comId = Convert.ToInt32(ddlCompany.SelectedValue);
            string EmpID = repEmpIdHiddenField.Value;
            string lblempName = empName.Text;
            string lblempCode = empCode.Text;
            string date = txtDate.Text;
            string Comments = txtComments.Text;
            

            var aDataTable = new DataTable();

            aDataTable.Columns.Add("CompanyId");
            aDataTable.Columns.Add("EmpInfoId");
            aDataTable.Columns.Add("Designation");
            aDataTable.Columns.Add("EmpMasterCode");
            aDataTable.Columns.Add("EmpName");
            aDataTable.Columns.Add("Date");
            aDataTable.Columns.Add("Comments");
            aDataTable.Columns.Add("Remarks");
           


            DataRow dataRow;


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[1].Text != lblempCode)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["EmpInfoId"] = GridView1.DataKeys[i][0].ToString();
                    dataRow["CompanyId"] = GridView1.DataKeys[i][1].ToString();
                    dataRow["EmpMasterCode"] = GridView1.Rows[i].Cells[1].Text;
                    dataRow["EmpName"] = GridView1.Rows[i].Cells[2].Text;
                    dataRow["Date"] = GridView1.Rows[i].Cells[3].Text;
                    dataRow["Comments"] = GridView1.Rows[i].Cells[4].Text;

                    dataRow["Remarks"] = ((TextBox)GridView1.Rows[i].FindControl("txtRemarks")).Text;
                    aDataTable.Rows.Add(dataRow);
                    clearalll();
                }

                else
                {
                    clearalll();
                    ShowMessageBox("Data already Exist !!");
                }
            }

            dataRow = aDataTable.NewRow();

            dataRow["CompanyId"] = comId;
            dataRow["EmpInfoId"] = EmpID;
            dataRow["EmpMasterCode"] = lblempCode;
            dataRow["EmpName"] = lblempName;
            dataRow["Date"] = date;
            dataRow["Comments"] = Comments;
            dataRow["Remarks"] = "";
            aDataTable.Rows.Add(dataRow);

            GridView1.DataSource = aDataTable;
            GridView1.DataBind();
            clearalll();
          //  jobtitleDropDownList.SelectedValue = "";

        }
    }

    private void clearalll()
    {
        txt_EmpName.Text = string.Empty;
        empCode.Text = string.Empty;
            empName.Text = string.Empty;
            ddlDivision.Text = string.Empty;
            ddlDesignation.Text = string.Empty;
        repEmpIdHiddenField.Value = null;

    }

    private bool AddJobTitleValidation()
    {

        if (ddlCompany.SelectedValue == "")
        {
            ShowMessageBox("Please select a Company !!!");
            ddlCompany.Focus();
            return false;
        }


        if (txtDate.Text == "")
        {
            ShowMessageBox("Please Chosse Date !!!");
            txtDate.Focus();
            return false;
        }
        if (repEmpIdHiddenField.Value == "")
        {
            ShowMessageBox("Please Enter a Employee !!!");
            txt_EmpName.Focus();
            return false;
        }


        if (txt_EmpName.Text == "")
        {
            ShowMessageBox("Please Enter a Employee !!!");
            txt_EmpName.Focus();
            return false;
        }






        return true;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmpMedicalCheckUpView.aspx");
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void deleteImageButtonDirectlySupervices_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("CompanyId");
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("Designation");
        aDataTable.Columns.Add("EmpMasterCode");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("Date");
        aDataTable.Columns.Add("Comments");
        aDataTable.Columns.Add("Remarks");
           

        DataRow dataRow;

        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["EmpInfoId"] = GridView1.DataKeys[i][0].ToString();
                    dataRow["CompanyId"] = GridView1.DataKeys[i][1].ToString();
                    dataRow["EmpMasterCode"] = GridView1.Rows[i].Cells[1].Text;
                    dataRow["EmpName"] = GridView1.Rows[i].Cells[2].Text;
                    dataRow["Date"] = GridView1.Rows[i].Cells[3].Text;
                    dataRow["Comments"] = GridView1.Rows[i].Cells[4].Text;

                    dataRow["Remarks"] = ((TextBox)GridView1.Rows[i].FindControl("txtRemarks")).Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        GridView1.DataSource = aDataTable;
        GridView1.DataBind();

    }


    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            SaveUpdate();
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        mid = string.IsNullOrEmpty(m_hdpkd.Value) ? 0 : int.Parse(m_hdpkd.Value);
        if (m_hdpkd.Value != string.Empty)
        {

          //  if (!CheckEmpDepartmentAllocateOrNot(mid.ToString()))
            {
                tblMedicalCheckUpMaster Bat = null;
                try
                {
                    using (var db = new HRIS_SMC_DBEntities())
                    {
                        if (mid > 0)
                        {

                            Bat = (from j in db.tblMedicalCheckUpMasters where j.MedicalCheckUpMasterId == mid select j).FirstOrDefault();









                            Bat.IsDelete = false;
                            Bat.DeleteBy = _userId;
                            Bat.DeleteDate = DateTime.Now;
                            db.SaveChanges();

                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation Successful...! ');window.location ='EmpMedicalCheckUpView.aspx';",
                                true);
                        }
                    }
                }
                catch (Exception)
                {


                }
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(),
            //        "alert",
            //        "alert('Can not be Updated & Deleted! Already Defined......');window.location ='VivaSetupInfoView.aspx';",
            //        true);

            //}
        }
    }

    private bool Validation()
    {


        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlCompany.Focus();
            return false;
        }

        if (txtDate.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            txtDate.Focus();
            return false;
        }

        if (GridView1.Rows.Count==0)
        {
            aShowMessage.ShowMessageBox("At least One row add to list", this);
           
            return false;
        } 




        return true;
    }
}