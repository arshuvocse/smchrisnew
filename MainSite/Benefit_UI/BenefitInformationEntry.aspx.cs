using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Benefit_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Benefit_UI_BenefitInformationEntry : System.Web.UI.Page
{
    BenefitDAL aBenefitDal=new BenefitDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            CheckBoxData();
            if (Session["BenefitId"] !=null)
            {
                beneftIdHiddenField.Value = Session["BenefitId"].ToString();
                GetData(beneftIdHiddenField.Value);
                Session["BenefitId"] = null;
            }
            else
            {
                saveButton.Visible = true;
            }
        }
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                saveButton.Visible = true;
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

    }

    public void CheckBoxData()
    {
        DataTable aDataTable = null;
        aDataTable=new DataTable();
        aDataTable = aBenefitDal.GetGradeData("1");
        managementCheckBoxList.DataValueField = "SalaryGradeId";
        managementCheckBoxList.DataTextField = "GradeCode";
        managementCheckBoxList.DataSource = null;
        managementCheckBoxList.DataBind();
        managementCheckBoxList.DataSource = aDataTable;
        managementCheckBoxList.DataBind();

        aDataTable = new DataTable();
        aDataTable = aBenefitDal.GetGradeData("2");
        gradedCheckBoxList.DataValueField = "SalaryGradeId";
        gradedCheckBoxList.DataTextField = "GradeCode";
        gradedCheckBoxList.DataSource = null;
        gradedCheckBoxList.DataBind();
        gradedCheckBoxList.DataSource = aDataTable;
        gradedCheckBoxList.DataBind();


    }

    public void GetData(string id)
    {
        DataTable dtdata = aBenefitDal.GetBenefitName(id);
        if (dtdata.Rows.Count>0)
        {
            benefittextBox.Text = dtdata.Rows[0]["Benefit"].ToString();
            isactiveCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsActive"].ToString());
            //for (int i = 0; i < dtdata.Rows.Count; i++)
            //{
            //    for (int j = 0; j < managementCheckBoxList.Items.Count; j++)
            //    {
            //        if (managementCheckBoxList.Items[j].Value == dtdata.Rows[i]["SalaryGradeId"].ToString())
            //        {
            //            managementCheckBoxList.Items[j].Selected = true;
            //        }
            //    }
            //    for (int j = 0; j < gradedCheckBoxList.Items.Count; j++)
            //    {
            //        if (gradedCheckBoxList.Items[j].Value == dtdata.Rows[i]["SalaryGradeId"].ToString())
            //        {
            //            gradedCheckBoxList.Items[j].Selected = true;
            //        }
            //    }
            //}
            //natureCheckBoxList.Items[0].Selected = Convert.ToBoolean(dtdata.Rows[0]["Perma"].ToString());
            //natureCheckBoxList.Items[1].Selected = Convert.ToBoolean(dtdata.Rows[0]["Contra"].ToString());
            //natureCheckBoxList.Items[2].Selected = Convert.ToBoolean(dtdata.Rows[0]["Casua"].ToString());
            //permCheckBoxList.Items[0].Selected = Convert.ToBoolean(dtdata.Rows[0]["PermConfirmed"].ToString());
            //permCheckBoxList.Items[1].Selected = Convert.ToBoolean(dtdata.Rows[0]["PermProbation"].ToString());
            //contCheckBoxList.Items[0].Selected = Convert.ToBoolean(dtdata.Rows[0]["ContConfirmed"].ToString());
            //contCheckBoxList.Items[1].Selected = Convert.ToBoolean(dtdata.Rows[0]["ContProbation"].ToString());
            //casualCheckBoxList.Items[0].Selected = Convert.ToBoolean(dtdata.Rows[0]["CasualConfirmed"].ToString());
            //casualCheckBoxList.Items[1].Selected = Convert.ToBoolean(dtdata.Rows[0]["CasualProbation"].ToString());
            //contractyearTextBox.Text =
            //    (string.IsNullOrEmpty(dtdata.Rows[0]["ContYear"].ToString())
            //        ? 0
            //        : Convert.ToInt32(dtdata.Rows[0]["ContYear"].ToString())).ToString();
            //type.Visible = true;
            //contyear.Visible = false;
            //perm.Visible = false;
            //cont.Visible = false;
            //cas.Visible = false;
            //if (natureCheckBoxList.Items[0].Selected)
            //{
            //    permCheckBoxList.Visible = true;
            //    perm.Visible = true;
            //}
            //if (natureCheckBoxList.Items[1].Selected)
            //{
            //    contCheckBoxList.Visible = true;
            //    contyear.Visible = true;
            //    cont.Visible = true;
            //}
            //if (natureCheckBoxList.Items[2].Selected)
            //{
            //    cas.Visible = true;

            //    casualCheckBoxList.Visible = true;
            //}
        }
    }
    private bool Validation()
    {
        

        if (benefittextBox.Text == string.Empty)
        {
            ShowMessageBox("Give Benefit Name");
            benefittextBox.Focus();
            return false;
        }

        return true;
    }
    protected void natureCheckBoxList_SelectedIndexChanged(object sender, EventArgs e)
    {
        type.Visible = true;
        contyear.Visible = false;
        perm.Visible = false;
        cont.Visible = false;
        cas.Visible = false;
        if (natureCheckBoxList.Items[0].Selected)
        {
            permCheckBoxList.Visible = true;
            perm.Visible = true;
        }
        if (natureCheckBoxList.Items[1].Selected)
        {
            contCheckBoxList.Visible = true;
            contyear.Visible = true;
            cont.Visible = true;
        }
        if (natureCheckBoxList.Items[2].Selected)
        {
            cas.Visible = true;
            
            casualCheckBoxList.Visible = true;
        }
    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public void Clear()
    {
        beneftIdHiddenField.Value = string.Empty;
        contractyearTextBox.Text = string.Empty;

        for (int i = 0; i < natureCheckBoxList.Items.Count; i++)
        {
            natureCheckBoxList.Items[i].Selected = false;
        }
        for (int i = 0; i < permCheckBoxList.Items.Count; i++)
        {
            permCheckBoxList.Items[i].Selected = false;
        }
        for (int i = 0; i < contCheckBoxList.Items.Count; i++)
        {
            contCheckBoxList.Items[i].Selected = false;
        }
        for (int i = 0; i < casualCheckBoxList.Items.Count; i++)
        {
            casualCheckBoxList.Items[i].Selected = false;
        }
        benefittextBox.Text = string.Empty;
        isactiveCheckBox.Checked = false;
        CheckBoxData();

        type.Visible = true;
        contyear.Visible = false;
        perm.Visible = false;
        cont.Visible = false;
        cas.Visible = false;
        if (natureCheckBoxList.Items[0].Selected)
        {
            permCheckBoxList.Visible = true;
            perm.Visible = true;
        }
        if (natureCheckBoxList.Items[1].Selected)
        {
            contCheckBoxList.Visible = true;
            contyear.Visible = true;
            cont.Visible = true;
        }
        if (natureCheckBoxList.Items[2].Selected)
        {
            cas.Visible = true;

            casualCheckBoxList.Visible = true;
        }
        manCheckBox.Checked = false;
        gradedCheckBox.Checked = false;
    }

    public void Update()
    {
        if (Validation())
        {
            BenefitMasterDAO aBenefitMasterDao = new BenefitMasterDAO()
            {
                BenefitMasterId = Convert.ToInt32(beneftIdHiddenField.Value),
                Benefit = benefittextBox.Text,
                IsActive = isactiveCheckBox.Checked,
                UpdateBy = Session["LoginName"].ToString(),
                UpdateDate = DateTime.Now
            };
            bool id = aBenefitDal.UpdateBenefitName(aBenefitMasterDao);
            if (id ==true)
            {
            ShowMessageBox("Data Update Successfully");
            Clear();
        }
        //if (id ==true)
            //{
            //    aBenefitDal.DeleteBenefitJobNature(beneftIdHiddenField.Value);
            //    BenefitJobNatureDAO aBenefitJobNatureDao = new BenefitJobNatureDAO()
            //    {
            //        PermProbation = permCheckBoxList.Items[1].Selected,
            //        PermConfirmed = permCheckBoxList.Items[0].Selected,
            //        BenefitMasterId = Convert.ToInt32(beneftIdHiddenField.Value),
            //        CasualConfirmed = casualCheckBoxList.Items[0].Selected,
            //        CasualProbation = casualCheckBoxList.Items[1].Selected,
            //        ContConfirmed = contCheckBoxList.Items[0].Selected,
            //        ContProbation = contCheckBoxList.Items[1].Selected,
            //        Perma = natureCheckBoxList.Items[0].Selected,
            //        Contra = natureCheckBoxList.Items[1].Selected,
            //        Casua = natureCheckBoxList.Items[2].Selected

            //    };
            //    if (contractyearTextBox.Text != string.Empty)
            //    {
            //        aBenefitJobNatureDao.ContYear = Convert.ToInt32(contractyearTextBox.Text);
            //    }
            //    int a = aBenefitDal.SaveBenefitJobNature(aBenefitJobNatureDao);
            //    aBenefitDal.DeleteBenefitDetail(beneftIdHiddenField.Value);
            //    for (int i = 0; i < managementCheckBoxList.Items.Count; i++)
            //    {
            //        if (managementCheckBoxList.Items[i].Selected)
            //        {
            //            BenefitDetailDAO aBenefitDetailDao = new BenefitDetailDAO()
            //            {
            //                BenefitMasterId = Convert.ToInt32(beneftIdHiddenField.Value),
            //                EmpCategoryId = 1,
            //                SalaryGradeId = Convert.ToInt32(managementCheckBoxList.Items[i].Value)
            //            };
            //            int ida = aBenefitDal.SaveBenefitDetail(aBenefitDetailDao);
            //        }
            //    }
            //    for (int i = 0; i < gradedCheckBoxList.Items.Count; i++)
            //    {
            //        if (gradedCheckBoxList.Items[i].Selected)
            //        {
            //            BenefitDetailDAO aBenefitDetailDao = new BenefitDetailDAO()
            //            {
            //                BenefitMasterId = Convert.ToInt32(beneftIdHiddenField.Value),
            //                EmpCategoryId = 1,
            //                SalaryGradeId = Convert.ToInt32(gradedCheckBoxList.Items[i].Value)
            //            };
            //            int idb = aBenefitDal.SaveBenefitDetail(aBenefitDetailDao);
            //        }
              //  }
             
            }
        }
    
    public void Save()
    {
        if (Validation())
        {
            BenefitMasterDAO aBenefitMasterDao = new BenefitMasterDAO()
            {
                Benefit = benefittextBox.Text,
                IsActive = isactiveCheckBox.Checked,
                EntryBy = Session["UserId"].ToString(),
                EntryDate = DateTime.Now
            };
            int id = aBenefitDal.SaveBenefitNameEntry(aBenefitMasterDao);
            if (id > 0)
            {
                //BenefitJobNatureDAO aBenefitJobNatureDao = new BenefitJobNatureDAO()
                //{
                //    PermProbation = permCheckBoxList.Items[1].Selected,
                //    PermConfirmed = permCheckBoxList.Items[0].Selected,
                //    BenefitMasterId = id,
                //    CasualConfirmed = casualCheckBoxList.Items[0].Selected,
                //    CasualProbation = casualCheckBoxList.Items[1].Selected,
                //    ContConfirmed = contCheckBoxList.Items[0].Selected,
                //    ContProbation = contCheckBoxList.Items[1].Selected,
                //    Perma = natureCheckBoxList.Items[0].Selected,
                //    Contra = natureCheckBoxList.Items[1].Selected,
                //    Casua = natureCheckBoxList.Items[2].Selected
                //};
                //if (contractyearTextBox.Text != string.Empty)
                //{
                //    aBenefitJobNatureDao.ContYear = Convert.ToInt32(contractyearTextBox.Text);
                //}
                //int a = aBenefitDal.SaveBenefitJobNature(aBenefitJobNatureDao);
                //for (int i = 0; i < managementCheckBoxList.Items.Count; i++)
                //{
                //    if (managementCheckBoxList.Items[i].Selected)
                //    {
                //        BenefitDetailDAO aBenefitDetailDao = new BenefitDetailDAO()
                //        {
                //            BenefitMasterId = id,
                //            EmpCategoryId = 1,
                //            SalaryGradeId = Convert.ToInt32(managementCheckBoxList.Items[i].Value)
                //        };
                //        int ida = aBenefitDal.SaveBenefitDetail(aBenefitDetailDao);
                //    }
                //}
                //for (int i = 0; i < gradedCheckBoxList.Items.Count; i++)
                //{
                //    if (gradedCheckBoxList.Items[i].Selected)
                //    {
                //        BenefitDetailDAO aBenefitDetailDao = new BenefitDetailDAO()
                //        {
                //            BenefitMasterId = id,
                //            EmpCategoryId = 1,
                //            SalaryGradeId = Convert.ToInt32(gradedCheckBoxList.Items[i].Value)
                //        };
                //        int idb = aBenefitDal.SaveBenefitDetail(aBenefitDetailDao);
                //    }
                //}
                ShowMessageBox("Data Saved Successfully");
                Clear();
            }
        }
    }
    protected void saveButton_OnClick(object sender, EventArgs e)
    {
        if (beneftIdHiddenField.Value==string.Empty)
        {
            Save();
        }
      
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BenefitNameView.aspx");
    }

    protected void manCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < managementCheckBoxList.Items.Count; i++)
        {
            if (manCheckBox.Checked)
            {
                managementCheckBoxList.Items[i].Selected = true;    
            }
            else
            {
                managementCheckBoxList.Items[i].Selected = false
                    ;
            }
        }
    }

    protected void gradedCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gradedCheckBoxList.Items.Count; i++)
        {
            if (gradedCheckBox.Checked)
            {
                gradedCheckBoxList.Items[i].Selected = true;    
            }
            else
            {
                gradedCheckBoxList.Items[i].Selected = false;
            }
            
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
         Clear();
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (beneftIdHiddenField.Value != string.Empty)
      
        {
            Update();
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {

      //  aBenefitDal.DeleteBenefitMaster(loadGridView.DataKeys[rowindex][0].ToString());
         // aBenefitDal.DeleteBenefitDetail(loadGridView.DataKeys[rowindex][0].ToString());
         //  aBenefitDal.DeleteBenefitJobNature(loadGridView.DataKeys[rowindex][0].ToString());

        if (aBenefitDal.DeleteBenefitName(beneftIdHiddenField.Value))
            {
               // aBenefitDal.DeleteBenefitDetail(beneftIdHiddenField.Value);
              //  aBenefitDal.DeleteBenefitJobNature(beneftIdHiddenField.Value);
                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
                Clear();

            }
       
        else
        {
            aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

        }
    }
}