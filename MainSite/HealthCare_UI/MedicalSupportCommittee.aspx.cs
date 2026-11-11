using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HealthCare_Dao;
using DAO.MeetingMinorsDAO;

public partial class HealthCare_UI_MedicalSupportCommittee : System.Web.UI.Page
{
    MedicalSupportComDal aRoutingPath = new MedicalSupportComDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           ButtonVisible();
            Dropdownlist();

            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                PathId.Value = (Request.QueryString["MID"]);
                GetRecord(Convert.ToInt32(PathId.Value));

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
                hiddencreateDate.Visible = false;

            }
            else if (Session["Status"].ToString() == "")
            {
                btn_Save.Visible = false;
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

            Response.Redirect("~/HealthCare_UI/MedicalSupportCommitteeView.aspx");
        }


        
    }


    private void Dropdownlist()
    {

        using (DataTable dt = aRoutingPath.GetDDLCompany())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();

            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }


    }


    private void GetRecord(int id)
    {
        DataTable Master = aRoutingPath.GetMaster(id);

        if (Master.Rows.Count > 0)
        {


            TxtRoutingPathName.Text = Master.Rows[0].Field<string>("MSC_Name").Trim().ToString();
            ddlCompany.SelectedValue = Master.Rows[0].Field<int>("CompanyId").ToString();
            hfDivision.Value = Master.Rows[0].Field<int>("DivisionId").ToString();
            ddlDivision.SelectedValue = hfDivision.Value;
            hfDept.Value = Master.Rows[0].Field<int>("DepartmentId").ToString();
            ddlDepartment.SelectedValue = hfDept.Value;
            hfcreateby.Value = Master.Rows[0].Field<int>("CreateBy").ToString();
            hiddencreateDate.Text = Master.Rows[0].Field<DateTime>("CreateDate").ToString();

        }


        using (DataTable dt = aRoutingPath.GetDivisionBycompanyId(Convert.ToInt32(ddlCompany.SelectedValue)))
        {
            ddlDivision.DataSource = dt;
            ddlDivision.DataValueField = "Value";
            ddlDivision.DataTextField = "TextField";
            ddlDivision.DataBind();
        }

        using (DataTable dt = aRoutingPath.GetDepartmentByDiv(Convert.ToInt32(ddlDivision.SelectedValue)))
        {
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataBind();
        }


        DataTable Details = aRoutingPath.GetDetails(id);
        gv_SaveGridView.DataSource = Details;
        gv_SaveGridView.DataBind();


        for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
        {
            DropDownList SequenceId = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");


            SequenceId.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
            for (int k = 1; k < gv_SaveGridView.Rows.Count + 1; k++)
            {
                SequenceId.Items.Insert(k, new ListItem(k.ToString()));
            }
        }

        if (Details.Rows.Count > 0)
        {

            for (int i = 0; i < Details.Rows.Count; i++)
            {


                var row = Details.Rows[i].Field<int>("Seq_No").ToString();
                var EmpId = Details.Rows[i].Field<Int64>("EmpInfoId").ToString(CultureInfo.InvariantCulture);


                DropDownList aDownList = (DropDownList)gv_SaveGridView.Rows[i].Cells[0].FindControl("SequenceId");
                aDownList.SelectedValue = row;
                HiddenField txt_empInfoId = (HiddenField)gv_SaveGridView.Rows[i].Cells[0].FindControl("txt_empInfoId");

                txt_empInfoId.Value = EmpId;



            }


            //for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
            //{
            //    //HiddenField txt_empInfoId = (HiddenField)gv_SaveGridView.Rows[i].FindControl("txt_empInfoId");
            //    //Label txt_empId = (Label)gv_SaveGridView.Rows[i].FindControl("txt_empId");
            //    //Label txt_name = (Label)gv_SaveGridView.Rows[i].FindControl("txt_name"); 
            //    //Label txt_designation = (Label)gv_SaveGridView.Rows[i].FindControl("txt_designation");
            // //   DropDownList aDownList = (DropDownList) gv_SaveGridView.Rows[i].Cells[j].FindControl("SequenceId");
            //    var row = Details.Rows[i].Field<int>("Seq_No").ToString();
            //    for (int j = 0; j < Details.Rows.Count; j++)
            //    {
            //        DropDownList aDownList = (DropDownList)gv_SaveGridView.Rows[i].Cells[j].FindControl("SequenceId");
            //      // aDownList.SelectedValue = row;
            //       // aDownList.SelectedValue = Details.Rows[j].Field<int>("Seq_No").ToString();
            //        //txt_empInfoId.Value = Details.Rows[j].Field<Int64>("EmpInfoId").ToString(CultureInfo.InvariantCulture);
            //        //txt_empId.Text = Details.Rows[j].Field<string>("EmpMasterCode").ToString();
            //        //txt_name.Text = Details.Rows[j].Field<string>("EmpName").ToString();
            //        //txt_designation.Text = Details.Rows[j].Field<string>("Designation").ToString();

            //    }
            //}

        }

    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            using (DataTable dt = aRoutingPath.GetDivisionBycompanyId(Convert.ToInt32(ddlCompany.SelectedValue)))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();


            }

            using (DataTable dt = aRoutingPath.GetDivisionBycompanyId(Convert.ToInt32(ddlCompany.SelectedValue)))
            {
                ddlDivisionSearch.DataSource = dt;
                ddlDivisionSearch.DataValueField = "Value";
                ddlDivisionSearch.DataTextField = "TextField";
                ddlDivisionSearch.DataBind();



            }
        }
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            aRoutingPath.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
        }
    }

    protected void ddlDivisionSearch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivisionSearch.SelectedValue != "")
        {
            aRoutingPath.GetDepartmentByDivList(ddlDepartmentSearch, ddlDivisionSearch.SelectedValue);
        }
    }

    public string Parameter()
    {
        string param = "";
        if (ddlDivisionSearch.Items.Count > 0)
        {
            if (ddlDivisionSearch.SelectedIndex > 0)
            {
                param = param + " AND A.DivisionId='" + ddlDivisionSearch.SelectedValue + "' ";
            }
        }
        if (ddlDepartmentSearch.Items.Count > 0)
        {
            if (ddlDepartmentSearch.SelectedIndex > 0)
            {
                param = param + " AND A.DepartmentId='" + ddlDepartmentSearch.SelectedValue + "' ";
            }
        }
        return param;

    }


    protected void Button1_OnClick(object sender, EventArgs e)
    {

        load();

    }


    private void load()
    {
        DataTable dt = aRoutingPath.GetEmployee(Parameter());

        gv_allocateEmp.DataSource = dt;
        gv_allocateEmp.DataBind();

    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gv_allocateEmp.HeaderRow.FindControl("txt_checkAll");
        bool result = ChkBoxHeader.Checked == true ? true : false;

        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
            chk.Checked = result;
        }
    }

    protected void txt_DeadLine_ssOnTextChanged(object sender, EventArgs e)
    {

    }
    public bool CheckEmpList()
    {
        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_allocateEmp.Rows[i].Cells[0].FindControl("txt_check");
            for (int j = 0; j < gv_SaveGridView.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                    Label SSStxt_empId = (Label)gv_SaveGridView.Rows[j].FindControl("txt_empId");

                    Label EmpDI = (Label)gv_allocateEmp.Rows[i].FindControl("txt_empId");

                    if (EmpDI.Text == SSStxt_empId.Text)
                    {

                        return false;

                    }

                }

            }

        }
        return true;
    }

    protected void textButton_OnClick(object sender, EventArgs e)
    {

        if (DeadLineValidation())
        {
            if (CheckEmpList())
            {

                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpMasterCode");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("Designation");
                aDataTable.Columns.Add("Position");
                aDataTable.Columns.Add("SequenceId");
                
                DataRow dataRow = null;

                for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
                {
                    CheckBox ChkBoxRows = (CheckBox)gv_allocateEmp.Rows[i].Cells[0].FindControl("txt_check");
                    HiddenField txt_empInfoId = ((HiddenField)gv_allocateEmp.Rows[i].FindControl("txt_empInfoId"));
            
                    Label txt_empId = (Label)gv_allocateEmp.Rows[i].FindControl("txt_empId");
                    Label txt_name = (Label)gv_allocateEmp.Rows[i].FindControl("txt_name");
                    Label txt_designation = (Label)gv_allocateEmp.Rows[i].FindControl("txt_designation");
                    RadioButtonList chkPosition = (RadioButtonList)gv_allocateEmp.Rows[i].FindControl("chkPosition");


                    if (ChkBoxRows.Checked)
                    {
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {


                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] = txt_empInfoId.Value;

                            dataRow["EmpMasterCode"] = txt_empId.Text;
                            dataRow["EmpName"] = txt_name.Text;
                            dataRow["Designation"] = txt_designation.Text;
                            dataRow["Position"] = chkPosition.SelectedItem.Text;



                            aDataTable.Rows.Add(dataRow);
                        }
                    }
                }
                for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
                {

                    CheckBox ChkBoxRows = (CheckBox)gv_SaveGridView.Rows[i].Cells[0].FindControl("txt_check");
                    HiddenField txt_empInfoId = ((HiddenField)gv_SaveGridView.Rows[i].FindControl("txt_empInfoId"));
                    HiddenField SequenceId = ((HiddenField)gv_SaveGridView.Rows[i].FindControl("HF_SequenceId"));
                  
                    Label txt_empId = (Label)gv_SaveGridView.Rows[i].FindControl("txt_empId");
                    Label txt_name = (Label)gv_SaveGridView.Rows[i].FindControl("txt_name");
                    Label txt_designation = (Label)gv_SaveGridView.Rows[i].FindControl("txt_designation");
                    Label chkPosition = (Label)gv_SaveGridView.Rows[i].FindControl("txt_Position");


                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = txt_empInfoId.Value;

                    dataRow["EmpMasterCode"] = txt_empId.Text;
                    dataRow["EmpName"] = txt_name.Text;
                    dataRow["Designation"] = txt_designation.Text;
                    dataRow["Position"] = chkPosition.Text;
                    dataRow["Position"] = chkPosition.Text;
                    dataRow["SequenceId"] = SequenceId.Value;


                    aDataTable.Rows.Add(dataRow);
                }

                gv_SaveGridView.DataSource = aDataTable;
                gv_SaveGridView.DataBind();



                for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
                {
                    DropDownList SequenceId = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");


                    SequenceId.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                    for (int k = 1; k < gv_SaveGridView.Rows.Count + 1; k++)
                    {
                        SequenceId.Items.Insert(k, new ListItem(k.ToString()));
                    }
                }

            }
            else
            {
                ShowMessageBox("Already Exist !!!");
            }
        }

    }

    private bool DeadLineValidation()
    {
        if (gv_allocateEmp.Rows.Count == 0)
        {
            ShowMessageBox("Please select at least one employee !!!");
            return false;
        }

        int totalCount = gv_allocateEmp.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("txt_check")).Checked);

        if (totalCount == 0)
        {
            ShowMessageBox("Please Select Employee");
            return false;
        }


        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {

            CheckBox chkPosition = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");

            if (chkPosition.Checked)
            {
                RadioButtonList aList = (RadioButtonList)gv_allocateEmp.Rows[i].FindControl("chkPosition");

                if (aList.SelectedValue == "")
                {
                    ShowMessageBox("Please Select Position");
                    return false;
                }
            }
        }

        return true;
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {
        if (TxtRoutingPathName.Text == "")
        {
            ShowMessageBox("please fill out this field !!!");
            TxtRoutingPathName.Focus();
            return false;
        }

        if (ddlCompany.SelectedIndex <= 0)
        {
            ShowMessageBox("please fill out this field !!!");
            ddlCompany.Focus();
            return false;
        }

        if (ddlDivision.SelectedValue == "")
        {
            ShowMessageBox("please fill out this field !!!");
            ddlDivision.Focus();
            return false;
        }

        if (ddlDepartment.SelectedValue == "")
        {
            ShowMessageBox("please fill out this field !!!");
            ddlDepartment.Focus();
            return false;
        }

        if (gv_SaveGridView.Rows.Count == 0)
        {
            ShowMessageBox("please fill out this field !!!");
            ddlDepartment.Focus();
            return false;
        }
        for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
        {
            DropDownList SequenceId = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");
            if (SequenceId.SelectedValue == "")
            {
                ShowMessageBox("please fill out Sequence !!!");
                SequenceId.Focus();
                return false;
            }
        }



        if (ddlDepartment.SelectedValue != "")
        {

            string RoutingPathName = TxtRoutingPathName.Text.ToUpper().Trim();
            string DepartmentId = ddlDepartment.SelectedValue;

            using (DataTable dt = aRoutingPath.CheckRoutingPath(RoutingPathName, DepartmentId))
            {
                if (PathId.Value == "")
                {
                    if (dt.Rows.Count > 0)
                    {
                        ShowMessageBox("Already Exist!!!");
                        TxtRoutingPathName.Focus();

                        return false;
                    }
                }
                else
                {
                    DataTable dt2 = aRoutingPath.CheckRoutingPathEdit(RoutingPathName, DepartmentId, PathId.Value);
                    if (dt2.Rows.Count > 0)
                    {


                        ShowMessageBox("Already Exist!!!");
                        TxtRoutingPathName.Focus();
                        return false;
                    }
                }

            }



        }


        return true;


    }

    public bool CheckSeq()
    {

        for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
        {
            for (int j = 0; j < gv_SaveGridView.Rows.Count; j++)
            {
                if (i != j)
                {
                    DropDownList SequenceId = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");
                    DropDownList SequenceId2 = (DropDownList)gv_SaveGridView.Rows[j].FindControl("SequenceId");
                    if (SequenceId.SelectedValue ==
                        SequenceId2.SelectedValue)
                    {

                        return false;
                    }
                }
            }
        }
        return true;



    }


    protected void Btn_Save(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (CheckSeq())
            {


                if (PathId.Value == "")
                {
                    Int32 PathMaster = SaveChangesMaster();

                    if (PathMaster > 0)
                    {
                        Int32 PathDetailsId = SaveChangesforPathDetails(PathMaster);
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Operation Successful...');window.location ='MedicalSupportCommitteeView.aspx';",
                  true);

                    }
                }
                else
                {

                }

            }
            else
            {
                ShowMessageBox("Sequence Can not be Same!!!");
            }
        }
    }

    private void clear()
    {
        PathId.Value = string.Empty;
        hfDept.Value = string.Empty;
        hfDivision.Value = string.Empty;
        //ddlCompany.SelectedValue = string.Empty;
        //ddlDivision.SelectedValue = string.Empty;
        //ddlDepartment.SelectedValue = string.Empty;
        //ddlDepartmentSearch.SelectedValue = string.Empty;
        //ddlDivisionSearch.SelectedValue = string.Empty;
        //gv_SaveGridView.DataSource = null;
        //gv_SaveGridView.DataBind();
        //gv_allocateEmp.DataSource = null;
        //gv_allocateEmp.DataBind();

    }
    private MedicalSupComMasterDao PrepareDataForSaveMaster()
    {

        MedicalSupComMasterDao aMaster = new MedicalSupComMasterDao();
        {
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.MSC_Name = TxtRoutingPathName.Text.ToString();
            aMaster.DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
            aMaster.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
            aMaster.CreateBy = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.CreateDate = DateTime.Now;
        }
        ;

        return aMaster;

    }

    private MedicalSupComMasterDao PrepareDataForUpdateMaster()
    {

        MedicalSupComMasterDao aMaster = new MedicalSupComMasterDao();
        {
            aMaster.MSCMaster_ID = Convert.ToInt32(PathId.Value);
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.MSC_Name = TxtRoutingPathName.Text.ToString();
            aMaster.DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
            aMaster.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
            aMaster.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            aMaster.UpdateDate = DateTime.Now;
        }
        ;

        return aMaster;

    }

    private RoutingPathSetupMaster PrepareDataForSaveDEL()
    {
        var EntryDao = new RoutingPathSetupMaster();
        EntryDao.RoutingPathMaster_ID = Convert.ToInt32(PathId.Value);
        EntryDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        EntryDao.RoutingPath_Name = TxtRoutingPathName.Text.ToString();
        EntryDao.DivisionId = Convert.ToInt32(ddlDivision.SelectedValue);
        EntryDao.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
        EntryDao.CreateBy = Convert.ToInt32(hfcreateby);
        EntryDao.CreateDate = Convert.ToDateTime(hiddencreateDate.Text);
        //  EntryDao.UpdateBy = Convert.ToInt32(hfDivision.Value);
        // EntryDao.UpdateDate = Convert.ToDateTime(hiddenUpdateDate.Text);
        return EntryDao;
    }

    private List<MedicalSupComDetailsDao> PrepareDataForSaveDetails(Int32 Val)
    {
        MedicalSupComDetailsDao aMemberSetupDetails;
        List<MedicalSupComDetailsDao> aMemberSetupDetailsesList = new List<MedicalSupComDetailsDao>();

        for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
        {
            // Label lbl_Name = (Label)gv_SaveGridView.Rows[i].FindControl("txt_empId");
            HiddenField aField = (HiddenField)gv_SaveGridView.Rows[i].FindControl("txt_empInfoId");
            DropDownList aDownList = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");
            Label Position = (Label)gv_SaveGridView.Rows[i].FindControl("txt_Position");

            aMemberSetupDetails = new MedicalSupComDetailsDao();
            aMemberSetupDetails.MSCMaster_ID = Convert.ToInt32(Val);
            aMemberSetupDetails.EmpInfoId = Convert.ToInt32(aField.Value);
            aMemberSetupDetails.Seq_No = Convert.ToInt32(aDownList.SelectedValue);
            aMemberSetupDetails.Position = Position.Text;
            aMemberSetupDetailsesList.Add(aMemberSetupDetails);
        }
        return aMemberSetupDetailsesList;
    }


    private List<MedicalSupComDetailsDao> PrepareDataForUpdateDetails()
    {
        MedicalSupComDetailsDao aMemberSetupDetails;
        List<MedicalSupComDetailsDao> aMemberSetupDetailsesList = new List<MedicalSupComDetailsDao>();

        for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
        {
            // Label lbl_Name = (Label)gv_SaveGridView.Rows[i].FindControl("txt_empId");

            HiddenField aField = (HiddenField)gv_SaveGridView.Rows[i].FindControl("txt_empInfoId");
            DropDownList aDownList = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");
            RadioButtonList Position = (RadioButtonList)gv_SaveGridView.Rows[i].FindControl("txt_Position");

            aMemberSetupDetails = new MedicalSupComDetailsDao();
            aMemberSetupDetails.EmpInfoId = Convert.ToInt32(aField.Value);
            aMemberSetupDetails.Seq_No = Convert.ToInt32(aDownList.SelectedValue);
            aMemberSetupDetails.Position = Position.Text;
            aMemberSetupDetailsesList.Add(aMemberSetupDetails);
        }
        return aMemberSetupDetailsesList;

    }



    private Int32 SaveChangesMaster()
    {
        Int32 retVal;
        try
        {
            retVal = aRoutingPath.SaveMaster(PrepareDataForSaveMaster());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }



    private Int32 SaveChangesMasterDelete(int Id)
    {
        Int32 retVal;
        try
        {

            retVal = aRoutingPath.Del_SaveMaster(PrepareDataForSaveDEL(), Id);

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }






    private bool UpdateMaster()
    {
        bool retVal;
        try
        {
            retVal = aRoutingPath.UpdateMaster(PrepareDataForUpdateMaster());

        }
        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }




    private Int32 SaveChangesforPathDetails(Int32 Val)
    {
        Int32 retVal;
        try
        {
            retVal = aRoutingPath.SaveDataForDetails(PrepareDataForSaveDetails(Val));
        }

        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }


    private bool UpdatePathDetails()
    {
        bool retVal;
        try
        {
            retVal = aRoutingPath.UpdateDetails(PrepareDataForUpdateDetails());
        }

        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/HealthCare_UI/MedicalSupportCommitteeView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (PathId.Value != "")
            {
                if (UpdateMaster())
                {
                    aRoutingPath.DeleteDetails(PathId.Value);
                    Int32 PathDetailsId = SaveChangesforPathDetails(Convert.ToInt32(PathId.Value));
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Operation Successful...');window.location ='MedicalSupportCommitteeView.aspx';",
               true);

                }
            }
        }
    }



    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (PathId.Value != "")
        {
            // int Id = Convert.ToInt32(Session["UserId"].ToString());
            //Int32 DEl_ID = SaveChangesMasterDelete(Id);



            bool ss = aRoutingPath.DeleteMaster(PathId.Value);
            if (ss)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(),
            "alert",
            "alert('Operation Successful...');window.location ='RoutingPathSetupView.aspx';",
            true);
            }
            else
            {
                ShowMessageBox("Operation Faild!!!!");
            }


        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/MeetingMinors/RoutingPathSetupView.aspx");
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

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void deleteImageButtonDirectlySupervices_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField empInfoId = (HiddenField)gv_SaveGridView.Rows[rowID].FindControl("txt_empInfoId");


        //DataTable aTable =
        //    _jdDal.CheckKepiSetpExist(empInfoId.Value, ddlFinancialYear.SelectedValue);
        //if (aTable.Rows.Count > 0)
        //{
        //    AlertMessageBoxShow("Can Not be Deleted...");
        //}
        //else
        {
            var itemCodeTextBox = (LinkButton)sender;
            var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
            int rowindex = 0;

            rowindex = currentRow.RowIndex;

            var aDataTable = new DataTable();


            aDataTable.Columns.Add("EmpInfoId");
            aDataTable.Columns.Add("EmpMasterCode");
            aDataTable.Columns.Add("EmpName");
            aDataTable.Columns.Add("Designation");
            aDataTable.Columns.Add("Position");
            aDataTable.Columns.Add("SequenceId");
            
            DataRow dataRow;

            if (gv_SaveGridView.Rows.Count > 0)
            {
                for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
                {


                    HiddenField txt_empInfoId = ((HiddenField)gv_SaveGridView.Rows[i].FindControl("txt_empInfoId"));
                    HiddenField HF_SequenceId = ((HiddenField)gv_SaveGridView.Rows[i].FindControl("HF_SequenceId"));
                    Label txt_empId = (Label)gv_SaveGridView.Rows[i].FindControl("txt_empId");
                    Label txt_name = (Label)gv_SaveGridView.Rows[i].FindControl("txt_name");
                    Label txt_designation = (Label)gv_SaveGridView.Rows[i].FindControl("txt_designation");
                    Label txt_Position = (Label)gv_SaveGridView.Rows[i].FindControl("txt_Position");

                    if (i != rowindex)
                    {


                        dataRow = aDataTable.NewRow();

                        dataRow["EmpInfoId"] = txt_empInfoId.Value;
                        dataRow["EmpMasterCode"] = txt_empId.Text;
                        dataRow["EmpName"] = txt_name.Text;
                        dataRow["Designation"] = txt_designation.Text;
                        dataRow["Position"] = txt_Position.Text;
                        dataRow["SequenceId"] = HF_SequenceId.Value;
                        aDataTable.Rows.Add(dataRow);
                    }
                }
            }


            gv_SaveGridView.DataSource = aDataTable;
            gv_SaveGridView.DataBind();

            for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
            {
                DropDownList SequenceId = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");
                //HiddenField HF_SequenceId = ((HiddenField)gv_SaveGridView.Rows[i].FindControl("HF_SequenceId"));

                //if (HF_SequenceId.Value != "0")
                //{
                //    SequenceId.SelectedValue = HF_SequenceId.Value;
                //}


                SequenceId.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                for (int k = 1; k < gv_SaveGridView.Rows.Count + 1; k++)
                {
                    SequenceId.Items.Insert(k, new ListItem(k.ToString()));
                }
            }
        }
    }

    protected void chkPosition_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //int rowIndex = ((GridViewRow)(((RadioButtonList)sender).Parent.Parent)).RowIndex;

        //RadioButtonList chkPosition = ((RadioButtonList)gv_allocateEmp.Rows[rowIndex].FindControl("chkPosition"));

        //HiddenField hfPosition = ((HiddenField)gv_allocateEmp.Rows[rowIndex].FindControl("hfPosition"));

        //hfPosition.Value = chkPosition.SelectedValue;
    }

    protected void SequenceId_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        for (int i = 0; i < gv_SaveGridView.Rows.Count; i++)
        {
            DropDownList SequenceId = (DropDownList)gv_SaveGridView.Rows[i].FindControl("SequenceId");
            HiddenField HF_SequenceId = ((HiddenField)gv_SaveGridView.Rows[i].FindControl("HF_SequenceId"));

            if (SequenceId.SelectedValue != "0")
            {
                HF_SequenceId.Value = SequenceId.SelectedValue;
            }
            
        }

    }
}