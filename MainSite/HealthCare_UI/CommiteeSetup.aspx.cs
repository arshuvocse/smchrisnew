using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using DAO.HealthCare_Dao;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_CommiteeSetup : System.Web.UI.Page
{
    private CommitteSetupDal aCommitteSetupDal = new CommitteSetupDal();
    ShowMessage aShowMessage = new ShowMessage();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitailDropdownlist();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                hfMasterId.Value = (Request.QueryString["MID"]);

                onRecord(Convert.ToInt32(Request.QueryString["MID"]));
            }
        }
    }

    protected void onRecord(int id)
    {

        SearchButton.Visible = false;
        UpdateBtn.Visible = true;

        DataTable dt = aCommitteSetupDal.Get_CommitteeSetupById(id);

        if (dt.Rows.Count > 0)
        {

            ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
            ddlSalarylocation.SelectedValue = dt.Rows[0]["SalaryLoationId"].ToString();
            inlineRadio1.SelectedValue = dt.Rows[0]["ApplicationType"].ToString();

            //IsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());         
            //if (dt.Rows[0]["IsOPD"].ToString() == "True")
            //{
            //    inlineRadio1.Items[0].Selected = true;
            //}
            //else
            //{
            //    inlineRadio1.Items[1].Selected = true;
            //}

            itemsGridView.DataSource = dt;
            itemsGridView.DataBind();

            for (int i = 0; i < itemsGridView.Rows.Count; i++)
            {
                CheckBox Forword = (CheckBox)itemsGridView.Rows[i].FindControl("ckForward");
                CheckBox Approved = (CheckBox)itemsGridView.Rows[i].FindControl("ckApproved");
                CheckBox Doctor = (CheckBox)itemsGridView.Rows[i].FindControl("ckDoctor");

                if (itemsGridView.DataKeys[i][1].ToString()=="True")
                {
                    Forword.Checked = true;
                }

                if (itemsGridView.DataKeys[i][2].ToString() == "True")
                {
                    Approved.Checked = true;
                }

                if (itemsGridView.DataKeys[i][3].ToString() == "True")
                {
                    Doctor.Checked = true;
                }
            }

        }
    }

    private void InitailDropdownlist()
    {
        aCommitteSetupDal.GetDDLSalaryLocation(ddlSalarylocation);
        aCommitteSetupDal.GetDDLEmployee(ddlEmployee);
        aCommitteSetupDal.GetDDLCompanyHC(ddlCompany);
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("CommitteeSetupView.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    bool Validation()
    {

        if (ddlSalarylocation.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Office",this);
            ddlSalarylocation.Focus();
            return false;
        }

        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Company", this);
            ddlCompany.Focus();
            return false;
        }

        if (itemsGridView.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Please Add At least One", this);
            return false;
        }


        if (hfMasterId.Value == "")
        {
            if (ddlCompany.SelectedValue != "" && ddlSalarylocation.SelectedValue != "" &&
                inlineRadio1.SelectedValue != "")
            {
                DataTable dt = aCommitteSetupDal.Get_ActivityCheck(ddlCompany.SelectedValue, inlineRadio1.SelectedValue,
                    ddlSalarylocation.SelectedValue);

                if (dt.Rows.Count > 0)
                {
                    aShowMessage.ShowMessageBox("Committee Already Exists", this);
                    return false;
                }
            }
        }

        
        return true;
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            int id = 0;

            id = aCommitteSetupDal.Save_CommitteeSetup(MasterDataPrepareForSave(), DetailsDataPrepareForSave());

            if (id > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');window.location ='CommitteeSetupView.aspx';",
                    true);
            }
        }
    }
    
    protected void AddToList_OnClick(object sender, EventArgs e)
    {
        if (AddToListValidation())
        {
            DataTable aDataTable = new DataTable();

            aDataTable.Columns.Add("ComSetupDetailsId");
            aDataTable.Columns.Add("ComSetupMasId");
            aDataTable.Columns.Add("EmpInfoId");
            aDataTable.Columns.Add("IsForward");
            aDataTable.Columns.Add("IsApproved");
            aDataTable.Columns.Add("IsDoctor");
            aDataTable.Columns.Add("EmpName");


            aDataTable.Columns.Add("IsConvenor");
            aDataTable.Columns.Add("IsMemberSecretory");
            aDataTable.Columns.Add("IsMember");

            DataRow dataRow = null;

          //  string fabCategory = "";

                
            for (int i = 0; i < itemsGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                CheckBox Forword = (CheckBox)itemsGridView.Rows[i].FindControl("ckForward");
                CheckBox Approved = (CheckBox)itemsGridView.Rows[i].FindControl("ckApproved");
                CheckBox ckDoctor = (CheckBox)itemsGridView.Rows[i].FindControl("ckDoctor");


                CheckBox ckConvenor = (CheckBox)itemsGridView.Rows[i].FindControl("ckConvenor");
                CheckBox ckMemberSecretory = (CheckBox)itemsGridView.Rows[i].FindControl("ckMemberSecretory");
                CheckBox ckMember = (CheckBox)itemsGridView.Rows[i].FindControl("ckMember");


                dataRow["EmpInfoId"] = itemsGridView.DataKeys[i][0].ToString();
                dataRow["EmpName"] = itemsGridView.Rows[i].Cells[1].Text.ToString();
                dataRow["IsForward"] = Forword.Checked;
                dataRow["IsApproved"] = Approved.Checked;
                dataRow["IsDoctor"] = ckDoctor.Checked;

                dataRow["IsConvenor"] = ckConvenor.Checked;
                dataRow["IsMemberSecretory"] = ckMemberSecretory.Checked;
                dataRow["IsMember"] = ckMember.Checked;

                aDataTable.Rows.Add(dataRow);
            }

            dataRow = aDataTable.NewRow();

            dataRow["EmpInfoId"] = ddlEmployee.SelectedValue;
            dataRow["EmpName"] = ddlEmployee.SelectedItem.Text;

            aDataTable.Rows.Add(dataRow);

            itemsGridView.DataSource = aDataTable;
            itemsGridView.DataBind();

            for (int i = 0; i < itemsGridView.Rows.Count; i++)
            {
                CheckBox Forword = (CheckBox)itemsGridView.Rows[i].FindControl("ckForward");
                CheckBox Approved = (CheckBox)itemsGridView.Rows[i].FindControl("ckApproved");
                CheckBox Doctor = (CheckBox)itemsGridView.Rows[i].FindControl("ckDoctor");


                CheckBox ckConvenor = (CheckBox)itemsGridView.Rows[i].FindControl("ckConvenor");
                CheckBox ckMemberSecretory = (CheckBox)itemsGridView.Rows[i].FindControl("ckMemberSecretory");
                CheckBox ckMember = (CheckBox)itemsGridView.Rows[i].FindControl("ckMember");


                if (itemsGridView.DataKeys[i][1].ToString() == "True")
                {
                    Forword.Checked = true;
                }

                if (itemsGridView.DataKeys[i][2].ToString() == "True")
                {
                    Approved.Checked = true;
                }

                if (itemsGridView.DataKeys[i][3].ToString() == "True")
                {
                    Doctor.Checked = true;
                }


                if (itemsGridView.DataKeys[i][4].ToString() == "True")
                {
                    ckConvenor.Checked = true;
                }

                if (itemsGridView.DataKeys[i][5].ToString() == "True")
                {
                    ckMemberSecretory.Checked = true;
                }

                if (itemsGridView.DataKeys[i][6].ToString() == "True")
                {
                    ckMember.Checked = true;
                }
            }

        }
    }

    private bool AddToListValidation()
    {
        
        if (ddlEmployee.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Employee !!",this);
            ddlEmployee.Focus();
            return false;
        }

        for (int i = 0; i < itemsGridView.Rows.Count; i++)
        {
            if (itemsGridView.DataKeys[i][0].ToString() == ddlEmployee.SelectedValue)
            {
                aShowMessage.ShowMessageBox("This employee already exists !!", this);
                return false;
            }
        }

        return true;
    }

    protected void itemdeleteImageButton_Click(object sender, EventArgs e)
    {
        LinkButton productCodeTextBox = (LinkButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;
        DataTable aDataTable = new DataTable();

        aDataTable.Columns.Add("ComSetupDetailsId");
        aDataTable.Columns.Add("ComSetupMasId");
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("IsForward");
        aDataTable.Columns.Add("IsApproved");
        aDataTable.Columns.Add("IsDoctor");
        aDataTable.Columns.Add("EmpName");


        DataRow dataRow = null;
        for (int i = 0; i < itemsGridView.Rows.Count; i++)
        {
            if (i != rowindex)
            {
                dataRow = aDataTable.NewRow();
                CheckBox Forword = (CheckBox)itemsGridView.Rows[i].FindControl("ckForward");
                CheckBox Approved = (CheckBox)itemsGridView.Rows[i].FindControl("ckApproved");
                CheckBox ckDoctor = (CheckBox)itemsGridView.Rows[i].FindControl("ckDoctor");
                dataRow["EmpInfoId"] = itemsGridView.DataKeys[i][0].ToString();
                dataRow["EmpName"] = itemsGridView.Rows[i].Cells[1].Text.ToString();
                dataRow["IsForward"] = Forword.Checked;
                dataRow["IsApproved"] = Approved.Checked;
                dataRow["IsDoctor"] = ckDoctor.Checked;
                aDataTable.Rows.Add(dataRow);
            }
        }

        itemsGridView.DataSource = aDataTable;
        itemsGridView.DataBind();

        for (int i = 0; i < itemsGridView.Rows.Count; i++)
        {
            CheckBox Forword = (CheckBox)itemsGridView.Rows[i].FindControl("ckForward");
            CheckBox Approved = (CheckBox)itemsGridView.Rows[i].FindControl("ckApproved");
            CheckBox Doctor = (CheckBox)itemsGridView.Rows[i].FindControl("ckDoctor");

            if (itemsGridView.DataKeys[i][1].ToString() == "True")
            {
                Forword.Checked = true;
            }

            if (itemsGridView.DataKeys[i][2].ToString() == "True")
            {
                Approved.Checked = true;
            }

            if (itemsGridView.DataKeys[i][3].ToString() == "True")
            {
                Doctor.Checked = true;
            }
        }
    }

    private CommiteeSetupMasterDao MasterDataPrepareForSave()
    {
        CommiteeSetupMasterDao aMasterDao = new CommiteeSetupMasterDao();

        if (hfMasterId.Value == "")
        {
            aMasterDao.ComSetupMasId = 0;
        }
        else
        {
            aMasterDao.ComSetupMasId = Convert.ToInt32(hfMasterId.Value);
        }
        aMasterDao.ApplicationType = inlineRadio1.SelectedItem.Value;
        aMasterDao.SalaryLoationId = Convert.ToInt32(ddlSalarylocation.SelectedValue);
        aMasterDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aMasterDao.IsActive = true;
        return aMasterDao;
    }

    private List<CommetteeSetupDetailsDao> DetailsDataPrepareForSave()
    {
        List<CommetteeSetupDetailsDao> aList = new List<CommetteeSetupDetailsDao>();

        for (int i = 0; i < itemsGridView.Rows.Count; i++)
        {
            var aInfo = new CommetteeSetupDetailsDao();

            CommetteeSetupDetailsDao aDao = new CommetteeSetupDetailsDao();
            CheckBox Forward = (CheckBox)itemsGridView.Rows[i].FindControl("ckForward");
            CheckBox Approved = (CheckBox)itemsGridView.Rows[i].FindControl("ckApproved");
            CheckBox ckDoctor = (CheckBox)itemsGridView.Rows[i].FindControl("ckDoctor");

            CheckBox ckConvenor = (CheckBox)itemsGridView.Rows[i].FindControl("ckConvenor");
            CheckBox ckMemberSecretory = (CheckBox)itemsGridView.Rows[i].FindControl("ckMemberSecretory");
            CheckBox ckMember = (CheckBox)itemsGridView.Rows[i].FindControl("ckMember");


            aInfo.EmpInfoId = int.Parse(itemsGridView.DataKeys[i][0].ToString());
            aInfo.IsForward = Forward.Checked;
            aInfo.IsApproved = Approved.Checked;
            aInfo.IsDoctor = ckDoctor.Checked;

            aInfo.IsConvenor = ckConvenor.Checked;
            aInfo.IsMemberSecretory = ckMemberSecretory.Checked;
            aInfo.IsMember = ckMember.Checked;


            aList.Add(aInfo);
        }

        return aList;
    }

    protected void ckForward_OnCheckedChanged(object sender, EventArgs e)
    {

        CheckBox button = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

       // CheckBox Currentcheck = (CheckBox)itemsGridView.Rows[rowindex].FindControl("ckForward");

        for (int i = 0; i < itemsGridView.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)itemsGridView.Rows[i].FindControl("ckForward");

            if (rowindex == i)
            {
                check.Checked = true;
                //aShowMessage.ShowMessageBox("You can forward only one person",this);
            }
            else
            {
                check.Checked = false;
            }
        }
    }

    protected void ckApproved_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox button = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        // CheckBox Currentcheck = (CheckBox)itemsGridView.Rows[rowindex].FindControl("ckForward");

        for (int i = 0; i < itemsGridView.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)itemsGridView.Rows[i].FindControl("ckApproved");

            if (rowindex == i)
            {
                check.Checked = true;
                //aShowMessage.ShowMessageBox("You can forward only one person",this);
            }
            else
            {
                check.Checked = false;
            }
        }
    }

    protected void IsActive_OnCheckedChanged(object sender, EventArgs e)
    {
        

    }

    protected void ckDoctor_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox button = (CheckBox)sender;
        GridViewRow currentRow = (GridViewRow)button.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        // CheckBox Currentcheck = (CheckBox)itemsGridView.Rows[rowindex].FindControl("ckForward");

        for (int i = 0; i < itemsGridView.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)itemsGridView.Rows[i].FindControl("ckDoctor");

            if (rowindex == i)
            {
                check.Checked = true;
                //aShowMessage.ShowMessageBox("You can forward only one person",this);
            }
            else
            {
                check.Checked = false;
            }
        }
    }

    protected void ckConvenor_OnCheckedChanged(object sender, EventArgs e)
    {
         
    }

    protected void ckMemberSecretory_OnCheckedChanged(object sender, EventArgs e)
    {
         
    }
}