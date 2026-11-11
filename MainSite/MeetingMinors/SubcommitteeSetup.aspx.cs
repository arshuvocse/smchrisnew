using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;
using Microsoft.Ajax.Utilities;

public partial class MeetingMinors_SubcommitteeSetup : System.Web.UI.Page
{
    private SubcommitteeSetupDAL AMAster = new SubcommitteeSetupDAL();
    MemberInfoDaL aMinors = new MemberInfoDaL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            Dropdownlist();
            LoadInitialgv_Member_Save();
            // load();
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                id_mastetID.Value = (Request.QueryString["MID"]);
                GetRecord(Convert.ToInt32(id_mastetID.Value));

            }
        }

    }


    private void LoadInitialgv_Member_Save()
    {
        DataTable dtDetails = new DataTable();

        dtDetails.Columns.Add("Type");

        dtDetails.Columns.Add("EmpMasterCode");
        dtDetails.Columns.Add("EmpInfoId");
        dtDetails.Columns.Add("EmpName");
        dtDetails.Columns.Add("Designation");



        DataRow dr = null;
        dr = dtDetails.NewRow();

        dr["Type"] = "";

        dr["EmpMasterCode"] = "";
        dr["EmpInfoId"] = "";
        dr["EmpName"] = "";
        dr["Designation"] = "";






        dtDetails.Rows.Add(dr);

        gv_Member.DataSource = null;
        gv_Member.DataBind();
        gv_Member.DataSource = dtDetails;
        gv_Member.DataBind();

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
    private void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                Button2.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "View")
            {
                Button2.Visible = false;
                editButton.Visible = false;
                delButton.Visible = false;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {

            Response.Redirect("SubcommitteeSetupView.aspx");
        }


    }


    protected void btn_gv_Member_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gv_Member_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["gv_Member_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gv_Member_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_Member.DataSource = dt;
                gv_Member.DataBind();



                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    using (DataTable dt22 = _commonDataLoad.GetEmpDDLNewMeetinig())
                    {



                        ddlEmpName.DataSource = dt22;
                        ddlEmpName.DataValueField = "EmpInfoId";
                        ddlEmpName.DataTextField = "EmpName";
                        ddlEmpName.DataBind();
                        ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        ddlEmpName.SelectedIndex = 0;

                    }
                }

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

                    rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                          .Value;
                    HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


                    if (rbType.SelectedValue == "Employee")
                    {


                        ddlEmpName.Visible = true;
                        txt_EmpName.Visible = false;


                        ddlEmpName.SelectedValue = MemEmpInfoId.Value;
                    }

                    if (rbType.SelectedValue == "Guest")
                    {

                        ddlEmpName.Visible = false;
                        txt_EmpName.Visible = true;


                    }




                }

            }
            else
            {
                ViewState["gv_Member_List"] = null;
                //Re bind the GridView for the updated data  
                gv_Member.DataSource = null;
                gv_Member.DataBind();
            }
        }

    }

    protected void ddlEmpName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;

        TextBox txt_EmpName = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpName"));
        TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpMasterCode"));
        HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("MemEmpInfoId"));
        TextBox txt_Designation = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_Designation"));
        DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[rowIndex].FindControl("ddlEmpName"));

        if (ddlEmpName.SelectedValue != "")
        {
            int mid = Convert.ToInt32(ddlEmpName.SelectedValue);
            using (var db = new HRIS_SMCEntities())
            {
                var emp = (from j in db.tblEmpGeneralInfoes

                           where j.EmpInfoId == mid
                           select j).FirstOrDefault();

                txt_EmpMasterCode.Text = emp.EmpMasterCode;
                txt_EmpName.Text = emp.EmpName;

                MemEmpInfoId.Value = emp.EmpInfoId.ToString();
                using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                {
                    txt_Designation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                }
            }

        }
        else
        {
            txt_EmpMasterCode.Text = "";
            MemEmpInfoId.Value = "";
            txt_Designation.Text = "";
        }
    }
    protected void rbType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((RadioButtonList)sender).Parent.Parent)).RowIndex;

        RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[rowIndex].FindControl("rbType"));

        HiddenField hfType = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("hfType"));


        DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[rowIndex].FindControl("ddlEmpName"));
        TextBox txt_EmpName = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpName"));



        TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpMasterCode"));
        HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("MemEmpInfoId"));
        TextBox txt_Designation = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_Designation"));

        hfType.Value = rbType.SelectedValue;

        if (rbType.SelectedValue == "Employee")
        {
            ddlEmpName.Visible = true;
            txt_EmpName.Visible = false;

            txt_EmpName.Text = "";

            using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
            {



                ddlEmpName.DataSource = dt;
                ddlEmpName.DataValueField = "EmpInfoId";
                ddlEmpName.DataTextField = "EmpName";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpName.SelectedIndex = 0;

            }
        }

        if (rbType.SelectedValue == "Guest")
        {
            ddlEmpName.Visible = false;
            txt_EmpName.Visible = true;
            ddlEmpName.SelectedValue = "";
            txt_EmpName.Text = "";
            txt_EmpMasterCode.Text = "";
            MemEmpInfoId.Value = "";
            txt_Designation.Text = "";

            ddlEmpName.Items.Clear();
        }

    }
    protected void btn_gv_MemberAdd_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();

        aTable.Columns.Add("Type");

        aTable.Columns.Add("EmpMasterCode");
        aTable.Columns.Add("EmpInfoId");
        aTable.Columns.Add("EmpName");
        aTable.Columns.Add("Designation");



        DataRow dr;



        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            HiddenField hfType = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"));
            TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[i].FindControl("txt_EmpMasterCode"));
            HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
            TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");
            TextBox txt_Designation = (TextBox)gv_Member.Rows[i].FindControl("txt_Designation");





            RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));







            dr["EmpInfoId"] = MemEmpInfoId.Value;

            dr["EmpMasterCode"] = txt_EmpMasterCode.Text;
            dr["EmpName"] = txt_EmpName.Text;
            dr["Designation"] = txt_Designation.Text;


            dr["Type"] = hfType.Value.Trim();
            if (dr["Type"].ToString() != "")
            {

                dr["Type"] = rbType.SelectedValue;
            }




            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();


                dr["EmpInfoId"] = "";

                dr["EmpMasterCode"] = "";
                dr["EmpName"] = "";
                dr["Designation"] = "";



                dr["Type"] = hfType.Value.Trim();
                if (dr["Type"].ToString() != "")
                {

                    rbType.SelectedValue = hfType.Value.Trim();
                }


                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        gv_Member.DataSource = null;
        gv_Member.DataBind();
        gv_Member.DataSource = aTable;
        ViewState["gv_Member_List"] = aTable;

        gv_Member.DataBind();


        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
            using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
            {



                ddlEmpName.DataSource = dt;
                ddlEmpName.DataValueField = "EmpInfoId";
                ddlEmpName.DataTextField = "EmpName";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpName.SelectedIndex = 0;

            }
        }

        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

            rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                  .Value;
            HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
            DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
            TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


            if (rbType.SelectedValue == "Employee")
            {


                ddlEmpName.Visible = true;
                txt_EmpName.Visible = false;


                ddlEmpName.SelectedValue = MemEmpInfoId.Value;
            }

            if (rbType.SelectedValue == "Guest")
            {

                ddlEmpName.Visible = false;
                txt_EmpName.Visible = true;


            }




        }

    }

    private void GetRecord(Int32 Id)
    {
        DataTable Dt = AMAster.GetforEdit(Id);


        if (Dt.Rows.Count > 0)
        {
            ddlCompany.SelectedValue = Dt.Rows[0].Field<int>("CompanyId").ToString();
            ddlCategory.SelectedValue = Dt.Rows[0]["CategoryID"].ToString();
            txtSubCommittee.Text = Dt.Rows[0].Field<string>("SubCommitteeName").ToString();
            txtRemarks.Text = Dt.Rows[0].Field<string>("Remarks").ToString();

            string st = Dt.Rows[0].Field<string>("ActionStatus").ToString();
            if (st == "Active")
            {
                isActive.Checked=true;
            }
            else
            {
                isActive.Checked = false;
                
            }

            DataTable dtMember_List = AMAster.GetMemberListDataById(id_mastetID.Value);
            if (dtMember_List.Rows.Count > 0)
            {
                ViewState["gv_Member_List"] = dtMember_List;
                gv_Member.DataSource = dtMember_List;
                gv_Member.DataBind();

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
                    {



                        ddlEmpName.DataSource = dt;
                        ddlEmpName.DataValueField = "EmpInfoId";
                        ddlEmpName.DataTextField = "EmpName";
                        ddlEmpName.DataBind();
                        ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        ddlEmpName.SelectedIndex = 0;

                    }
                }

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

                    rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                          .Value;
                    HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


                    if (rbType.SelectedValue == "Employee")
                    {


                        ddlEmpName.Visible = true;
                        txt_EmpName.Visible = false;


                        ddlEmpName.SelectedValue = MemEmpInfoId.Value;
                    }

                    if (rbType.SelectedValue == "Guest")
                    {

                        ddlEmpName.Visible = false;
                        txt_EmpName.Visible = true;


                    }




                }
            }


            DataTable Dt2 = AMAster.GetforEdit2(Id);
            if (Dt2.Rows.Count>0)
            {
                //for (int k = 0; k < Dt2.Rows.Count; k++)
                //{
                //    int ddd = Convert.ToInt32(Dt2.Rows[k]["BMemberSetupDetailsID"].ToString());
                //    for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
                //    {

                //        if (ddd == Convert.ToInt32(gradeCheckBoxList.Items[i].Value))
                //        {
                //            gradeCheckBoxList.Items[i].Selected = true;
                //        }
                        
                //    }
                //}

                for (int k = 0; k < Dt2.Rows.Count; k++)
                {
                    int ddd = 0;
                    try
                    {
                        ddd = Convert.ToInt32(Dt2.Rows[k]["BMemberSetupDetailsID"].ToString());
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        HiddenField hfMemberSetupDetailsID = (HiddenField)GridView1.Rows[i].FindControl("hfMemberSetupDetailsID");
                        DropDownList ddlMemberType = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberType");
                        DropDownList ddlPosition = (DropDownList)GridView1.Rows[i].FindControl("ddlPosition");
                        CheckBox chkSelect = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");


                        if (ddd == Convert.ToInt32(hfMemberSetupDetailsID.Value))
                        {
                            chkSelect.Checked = true;

                            ddlMemberType.SelectedValue = Dt2.Rows[k]["MeetingMemberTypeId"].ToString();
                            ddlPosition.SelectedValue = Dt2.Rows[k]["PositionId"].ToString();

                        }
                        else
                        {
                            //  chkSelect.Checked = false;

                        }


                    }
                }

              

            }
           
            
        }

    }

 

 
    
   
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
 
 
    private void Dropdownlist()
    {
        using (DataTable dt = AMAster.GetDDLCompany())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }

        AMAster.GetCategoryListIntoDropdown(ddlCategory);


     DataTable dtgrade = AMAster.GetGrade(ddlCompany.SelectedValue);
        //gradeCheckBoxList.DataValueField = "MemberSetupDetailsID";
        //gradeCheckBoxList.DataTextField = "Name";
        //gradeCheckBoxList.DataSource = dtgrade;
        //gradeCheckBoxList.DataBind();

        GridView1.DataSource = dtgrade;
        GridView1.DataBind();

        DataTable dtMemberType = aMinors.GetDDLMemberType();
        DataTable dtMemberPostion = aMinors.GetDDLMemberPostion();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            DropDownList ddlMemberType = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberType");
            DropDownList ddlPosition = (DropDownList)GridView1.Rows[i].FindControl("ddlPosition");


            ddlMemberType.DataSource = dtMemberType;
                ddlMemberType.DataValueField = "Value";
                ddlMemberType.DataTextField = "TextField";
                ddlMemberType.DataBind();





                ddlPosition.DataSource = dtMemberPostion;
                ddlPosition.DataValueField = "Value";
                ddlPosition.DataTextField = "TextField";
                ddlPosition.DataBind();



                for (int k = 0; k < dtgrade.Rows.Count; k++)
            {


                ddlMemberType.SelectedValue = dtgrade.Rows[k]["MeetingMemberTypeId"].ToString();
            }

        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SubcommitteeSetupView.aspx");
    }

    protected void SSGradeCheck_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            if (SSGradeCheck.Checked)
            {
                gradeCheckBoxList.Items[i].Selected = true;
            }
            else
            {
                gradeCheckBoxList.Items[i].Selected = false
                    ;
            }
        }

     
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCompany.SelectedValue!="")
        //{
        //    DataTable dtgrade = AMAster.GetGrade(ddlCompany.SelectedValue);
        //    gradeCheckBoxList.DataValueField = "BMemberSetupDetailsID";
        //    gradeCheckBoxList.DataTextField = "Name";
        //    gradeCheckBoxList.DataSource = dtgrade;
        //    gradeCheckBoxList.DataBind();
        //}
        //else
        //{
        //    gradeCheckBoxList.Items.Clear();
        //}
    }
    ShowMessage aShowMessage = new ShowMessage();
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool DataValidation()
    {
        if (ddlCompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company", this);
            ddlCompany.Focus();
            return false;

        }
        if (ddlCategory.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Category", this);
            ddlCategory.Focus();
            return false;

        }


        if (txtSubCommittee.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            ddlCategory.Focus();
            return false;

        }


        using (DataTable dt = AMAster.GetCheckPartInfo(txtSubCommittee.Text.Trim().ToUpper()))
        {
            if (id_mastetID.Value == "")
            {
                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Sub-Committee Name Already Exist!!!');",
                        true);

                    txtSubCommittee.Focus();

                    return false;
                }
            }
            else
            {
                DataTable dt2 = AMAster.GetCheckPartInfo2(txtSubCommittee.Text.Trim().ToUpper(), id_mastetID.Value);
                if (dt2.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Sub-Committee Name Already Exist!!!');",
                        true);

                    txtSubCommittee.Focus();
                    return false;
                }
            }
        }

        Int32 count = 0;

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("Please Select at least one employee !!!");
            return false;
        }
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {


            var chkBoxRows = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                // if (ddlIncrementType.SelectedItem.Text != "Step Adjustment")
                {
                    var ddlMemberType = (DropDownList)GridView1.Rows[i].FindControl("ddlMemberType");
                    var ddlPosition = (DropDownList)GridView1.Rows[i].FindControl("ddlPosition");
                    if (ddlMemberType.SelectedIndex <= 0)
                    {
                        ShowMessageBox("Please select Present Status !!!");
                        return false;
                    }

                    if (ddlPosition.SelectedIndex <= 0)
                    {
                        ShowMessageBox("Please select Position !!!");
                        return false;
                    }
                }

            }


        }





        return true;
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (DataValidation())
        {
            SaveUpInfo();
            
        }
    }

    private void SaveUpInfo()
    {
        SubcommitteeMasterDAO aMaster = new SubcommitteeMasterDAO();

        aMaster.SubcommitteeMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

        aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);

        aMaster.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
        aMaster.SubCommitteeName = txtSubCommittee.Text.Trim();
        aMaster.Remarks = txtRemarks.Text.Trim();

        if (isActive.Checked)
        {
               aMaster.ActionStatus = "Active";
        }
         else
            {
                aMaster.ActionStatus = "Inactive";
                
            }
        List<MiscellaneousInfoDetailDAO> aEmpList = new List<MiscellaneousInfoDetailDAO>();

        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            HiddenField hfType = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"));
            TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[i].FindControl("txt_EmpMasterCode"));
            HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
            TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");
            TextBox txt_Designation = (TextBox)gv_Member.Rows[i].FindControl("txt_Designation");





            RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));


            MiscellaneousInfoDetailDAO AEmp = new MiscellaneousInfoDetailDAO();

            AEmp.Type = rbType.SelectedValue.ToString();

            AEmp.EmpInfoId = MemEmpInfoId.Value == "" ? 0 : Convert.ToInt32(MemEmpInfoId.Value);


            AEmp.EmpMasterCode = txt_EmpMasterCode.Text.ToString();
            AEmp.EmpName = txt_EmpName.Text.ToString();
            AEmp.Designation = txt_Designation.Text.ToString();
           


            aEmpList.Add(AEmp);
        }



        int pk = AMAster.SaveMaster(aMaster,Session["UserId"].ToString());
        if (pk > 0)
        {
            
                AMAster.DeleteByDetrailsId(pk);
                AMAster.SaveDetails(aEmpList, pk);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    var chkBoxRows = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("chkSelect");
                    HiddenField hfMemberSetupDetailsID = (HiddenField)GridView1.Rows[i].Cells[0].FindControl("hfMemberSetupDetailsID");
                    DropDownList ddlMemberType = (DropDownList)GridView1.Rows[i].Cells[0].FindControl("ddlMemberType");
                    DropDownList ddlPosition = (DropDownList)GridView1.Rows[i].Cells[0].FindControl("ddlPosition");
                    if (chkBoxRows.Checked)
                    {
                        AMAster.DeleteById(pk, hfMemberSetupDetailsID.Value, ddlMemberType.SelectedValue, ddlPosition.SelectedValue);

                    }
                }

                //for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
                //{
                //    if (gradeCheckBoxList.Items[i].Selected)
                //    {
                //        AMAster.DeleteById(pk, gradeCheckBoxList.Items[i].Value);
                //    }
                //}
            

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='SubcommitteeSetupView.aspx';",
                true);
        }
    }

    public string GradeParam()
    {
         
        string grade = "";

        for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            if (gradeCheckBoxList.Items[i].Selected)
            {
                grade = gradeCheckBoxList.Items[i].Value + "," + grade;
            }
        }

        return grade;

    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {

        if (DataValidation())
        {
            SaveUpInfo();

        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (id_mastetID.Value != "")
        {
            if (AMAster.DeleteByMasterId(Convert.ToInt32(id_mastetID.Value)))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Operation Successful...');window.location ='SubcommitteeSetupView.aspx';",
               true);
            }
            
        }
    }
}