using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Employee_DAL;
using DAL.HealthCare_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_DepartedInfo : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();

    private readonly DepartedDal aDepartedDal = new DepartedDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != "")
        {
            _userId = Convert.ToString(Session["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpkEmpId.Value = mid.ToString();


                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        empMasterCode.Text =
                            emp.EmpMasterCode;


                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }

                        lblEmpName.Text = emp.EmpName;


                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }
                    }


                    LoadFamilyMember(mid);

                    CheckPrevoisDataFamilyMember(mid);

                }
            }
        }

    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        SaveUpdateInfo();

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

        int count = 0;
        int RowCount = 0;

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            CheckBox aBox = (CheckBox)gv_EmpListSearch.Rows[i].FindControl("chkSelect");

            if (aBox.Checked)
            {
                RowCount += 1;
            }
        }

        if (count==RowCount)
        {
            AlertMessageBoxShow("Please Checked at least one ");
        }

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            CheckBox check = (CheckBox)gv_EmpListSearch.Rows[i].FindControl("chkSelect");
            TextBox date = (TextBox)gv_EmpListSearch.Rows[i].FindControl("DeathofDate");
            if (check.Checked)
            {
                if (date.Text.Trim() == "")
                {
                    aShowMessage.ShowMessageBox("Please Select Date", this);
                    date.Focus();
                    return false;
                }
            }

        }


        return true;
    }

    private void SaveUpdateInfo()
    {
        if (Validation() == true)
        {
            List<DepartedDao> aDaos = new List<DepartedDao>();

            for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
            {
                DepartedDao aMaster = new DepartedDao();

                CheckBox aCheckBox = (CheckBox)gv_EmpListSearch.Rows[i].FindControl("chkSelect");

                if (aCheckBox.Checked)
                {
                    Label Relative = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Relative");
                    Label Name = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Family");
                    TextBox Date = (TextBox)gv_EmpListSearch.Rows[i].FindControl("DeathofDate");
                    TextBox remak = (TextBox)gv_EmpListSearch.Rows[i].FindControl("txt_Remark");
                  //  HiddenField Image = (HiddenField)gv_EmpListSearch.Rows[i].FindControl("hfSignature");
                    aMaster.DepartedId = 0;
                    aMaster.EmpInfoId = hdpkEmpId.Value == "" ? 0 : Convert.ToInt32(hdpkEmpId.Value);
                    aMaster.Relative = Relative.Text;
                    aMaster.Name = Name.Text;
                    aMaster.DeathofDate = Convert.ToDateTime(Date.Text);
                    aMaster.Remarks =  string.IsNullOrEmpty(remak.Text) ? null : remak.Text;
                    //aMaster.UploadImage = Image.Value;
                    //aMaster.ImagePath = "C:\\inetpub\\wwwroot\\SMCHRIS\\UploadImg\\" + Image.Value;
                    aDaos.Add(aMaster);
                }
                
            }

            bool del = aDepartedDal.DeleteDetails(hdpkEmpId.Value);

            
                int pk = aDepartedDal.SaveDepartedInfo(aDaos);
                if (pk > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...!');window.location ='EmployeeInfoListUpdate.aspx';",
                        true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
          //  }

        }
    }

    protected void btn_Next_OnClick(object sender, EventArgs e)
    {
        SaveUpdateInfo();

    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void lbPrevious_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpSalaryInfo?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }

    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {

    }

    private void LoadFamilyMember(int empid)
    {

        DataTable dt = formDal.Get_FamilyMemberById_Depart(empid);
        gv_EmpListSearch.DataSource = dt;
        gv_EmpListSearch.DataBind();

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            Label Family = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Family");
            Label Relative = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Relative");

            if (Family.Text == "")
            {
                gv_EmpListSearch.Rows[i].Visible = false;

            }

            string empName = Family.Text.Trim();

            if (empName != "")
            {
                string[] emp = empName.Split('-');
                Relative.Text = emp[0].Trim();
                Family.Text = emp[1].Trim();

            }
        }

    }



    private void CheckPrevoisDataFamilyMember(int empid)
    {

        DataTable dt = formDal.Get_PreviousSave(empid);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < gv_EmpListSearch.Rows.Count; j++)
                {
                    Label Family = (Label)gv_EmpListSearch.Rows[j].FindControl("lbl_Family");
                    
                    CheckBox chkCount = ((CheckBox)gv_EmpListSearch.Rows[j].FindControl("chkSelect"));
                    TextBox Date = (TextBox)gv_EmpListSearch.Rows[j].FindControl("DeathofDate");
                    TextBox remark = (TextBox)gv_EmpListSearch.Rows[j].FindControl("txt_Remark");
                    string empName = Family.Text.Trim();
                    if (empName.Trim() == dt.Rows[i]["Name"].ToString().Trim())
                    {
                        chkCount.Checked = true;
                        Date.Text = dt.Rows[i]["DeathofDate"].ToString().Trim();
                        remark.Text = dt.Rows[i]["Remarks"].ToString().Trim();
                    }
                }
            }
        }

        
    }




    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {

        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chkSelect = ((CheckBox)gv_EmpListSearch.Rows[rowIndex].FindControl("chkSelect"));
        Label Family = (Label)gv_EmpListSearch.Rows[rowIndex].FindControl("lbl_Family");
        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            CheckBox chkCount = ((CheckBox)gv_EmpListSearch.Rows[i].FindControl("chkSelect"));

            if (rowIndex == i)
            {
                if (chkSelect.Checked)
                {

                    //if (Family.Text != "")
                    //{
                    string empName = Family.Text.Trim();

                    //if (empName != "")
                    //{
                    //    string[] emp = empName.Split('-');
                    //    NameofPatient.Text = "";
                    //    Relationship.Text = "";
                    //    NameofPatient.Text = emp[1].Trim();
                    //    Relationship.Text = emp[0].Trim();

                    //    NameofPatient.Attributes.Add("readonly", "readonly");
                    //    Relationship.Attributes.Add("readonly", "readonly");
                    //    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
                    //}
                    //else
                    //{
                    //    NameofPatient.Text = "";
                    //    Relationship.Text = "";
                    //}

                }

                chkCount.Checked = true;
            }
            else
            {
                chkCount.Checked = false;
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
}