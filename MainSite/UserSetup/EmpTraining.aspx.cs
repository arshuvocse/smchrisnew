using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_EmpTraining : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != "")
        {
            _userId = Convert.ToString(Session["UserId"].ToString());
        }
        if (!IsPostBack)
        {
            LoadDropDownList();


            
            txt_TrFromDate.Attributes.Add("readonly", "readonly");
            txt_TrToDate.Attributes.Add("readonly", "readonly");
           
        
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
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
                        using (DataTable dtTraining = _commonDataLoad.GetDTEmpTrainingByEmpId(mid))
                        {
                            if (dtTraining.Rows.Count > 0)
                            {
                                ViewState["TrainingTable"] = dtTraining;
                                gv_Training.DataSource = dtTraining;
                                gv_Training.DataBind();
                            }
                        }
                    }

                }
            }
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }
    private void LoadDropDownList()
    {
        using (DataTable dt = _commonDataLoad.GetDDLTrainingType())
        {
            ddlTrTrainingType.DataSource = dt;
            ddlTrTrainingType.DataValueField = "Value";
            ddlTrTrainingType.DataTextField = "TextField";
            ddlTrTrainingType.DataBind();
        }

        using (DataTable dt = _commonDataLoad.GetDDLTrainingInstitution())
        {
            ddlTrTrainingInstitution.DataSource = dt;
            ddlTrTrainingInstitution.DataValueField = "Value";
            ddlTrTrainingInstitution.DataTextField = "TextField";
            ddlTrTrainingInstitution.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLCountry())
        {
            ddlTrTrainingCountry.DataSource = dt;
            ddlTrTrainingCountry.DataValueField = "Value";
            ddlTrTrainingCountry.DataTextField = "TextField";
            ddlTrTrainingCountry.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLCountry())
        {
            ddlTrTrainingCountry.DataSource = dt;
            ddlTrTrainingCountry.DataValueField = "Value";
            ddlTrTrainingCountry.DataTextField = "TextField";
            ddlTrTrainingCountry.DataBind();
        }
    }

    protected void btnAddTraining_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Training();
    }
    private void AddNewToGrid_Training()
    {
        if (ViewState["TrainingTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["TrainingTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpTrainingId"] = DBNull.Value;
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["TrainingName"] = txt_TrTrainingName.Text;

                if (ddlTrTrainingType.SelectedIndex > 0)
                {
                    drCurrentRow["TrainingTypeName"] = ddlTrTrainingType.SelectedItem.Text;
                    drCurrentRow["TrainingType"] = ddlTrTrainingType.SelectedValue;
                }

                if (ddlTrTrainingInstitution.SelectedIndex > 0)
                {
                    drCurrentRow["TrainingInstitutionName"] = ddlTrTrainingInstitution.SelectedItem.Text;
                    drCurrentRow["TrainingInstitution"] = ddlTrTrainingInstitution.SelectedValue;
                }

                if (ddlTrTrainingCountry.SelectedIndex > 0)
                {
                    drCurrentRow["TrainingCountryName"] = ddlTrTrainingCountry.SelectedItem.Text;
                    drCurrentRow["TrainingCountry"] = ddlTrTrainingCountry.SelectedValue;
                }


                drCurrentRow["TrainingDescription"] = txt_TrTrainingDescription.Text;
                drCurrentRow["TrainingPlace"] = txt_TrTrainingPlace.Text;
                drCurrentRow["TrainingAchievment"] = txt_TrTrainingAchievment.Text;
                drCurrentRow["TrFromDate"] = txt_TrFromDate.Text;
                drCurrentRow["TrToDate"] = txt_TrToDate.Text;
                try
                {
                    drCurrentRow["TrainingDays"] = txt_TrTrainingDays.Text;
                }
                catch (Exception)
                {

                    //throw;
                }
                drCurrentRow["TrRemarks"] = txt_TrRemarks.Text;

                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["TrainingTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Training.DataSource = dtCurrentTable;
                gv_Training.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("EmpTrainingId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingType", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingTypeName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingInstitution", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingInstitutionName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingPlace", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingCountry", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingCountryName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingAchievment", typeof(string)));
            dt.Columns.Add(new DataColumn("TrFromDate", typeof(string)));
            dt.Columns.Add(new DataColumn("TrToDate", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingDays", typeof(string)));
            dt.Columns.Add(new DataColumn("TrRemarks", typeof(string)));

            dr = dt.NewRow();
            dr["EmpTrainingId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["TrainingName"] = txt_TrTrainingName.Text;

            //dr["TrainingTypeName"] = ddlTrTrainingType.SelectedItem.Text;
            //dr["TrainingType"] = ddlTrTrainingType.SelectedValue;
            //dr["TrainingInstitutionName"] = ddlTrTrainingInstitution.SelectedItem.Text;
            //dr["TrainingInstitution"] = ddlTrTrainingInstitution.SelectedValue;
            //dr["TrainingCountryName"] = ddlTrTrainingCountry.SelectedItem.Text;
            //dr["TrainingCountry"] = ddlTrTrainingCountry.SelectedValue;

            if (ddlTrTrainingType.SelectedIndex > 0)
            {
                dr["TrainingTypeName"] = ddlTrTrainingType.SelectedItem.Text;
                dr["TrainingType"] = ddlTrTrainingType.SelectedValue;
            }

            if (ddlTrTrainingInstitution.SelectedIndex > 0)
            {
                dr["TrainingInstitutionName"] = ddlTrTrainingInstitution.SelectedItem.Text;
                dr["TrainingInstitution"] = ddlTrTrainingInstitution.SelectedValue;
            }

            if (ddlTrTrainingCountry.SelectedIndex > 0)
            {
                dr["TrainingCountryName"] = ddlTrTrainingCountry.SelectedItem.Text;
                dr["TrainingCountry"] = ddlTrTrainingCountry.SelectedValue;
            }


            dr["TrainingDescription"] = txt_TrTrainingDescription.Text;
            dr["TrainingPlace"] = txt_TrTrainingPlace.Text;
            dr["TrainingAchievment"] = txt_TrTrainingAchievment.Text;
            dr["TrFromDate"] = txt_TrFromDate.Text;
            dr["TrToDate"] = txt_TrToDate.Text;
            dr["TrainingDays"] = txt_TrTrainingDays.Text;
            dr["TrRemarks"] = txt_TrRemarks.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["TrainingTable"] = dt;

            //Bind the Gridview   
            gv_Training.DataSource = dt;
            gv_Training.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Training();
        txt_TrTrainingName.Text = string.Empty;
        ddlTrTrainingType.SelectedValue = null;
        ddlTrTrainingInstitution.SelectedValue = null;
        ddlTrTrainingCountry.SelectedValue = null;
        txt_TrTrainingDescription.Text = string.Empty;
        txt_TrTrainingPlace.Text = string.Empty;
        txt_TrTrainingAchievment.Text = string.Empty;
        txt_TrFromDate.Text = string.Empty;
        txt_TrToDate.Text = string.Empty;
        txt_TrTrainingDays.Text = string.Empty;
        txt_TrRemarks.Text = string.Empty;
    }
    private void SetPreviousData_Training()
    {
        int rowIndex = 0;
        if (ViewState["TrainingTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["TrainingTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpTrainingId = (HiddenField)gv_Training.Rows[rowIndex].FindControl("EmpTrainingId");
                    Label lbl_TrainingName = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingName");

                    Label lbl_TrainingType = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingType");
                    HiddenField hfTrainingType = (HiddenField)gv_Training.Rows[rowIndex].FindControl("hfTrainingType");
                    Label lbl_TrainingInstitution = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingInstitution");
                    HiddenField hfTrainingInstitution = (HiddenField)gv_Training.Rows[rowIndex].FindControl("hfTrainingInstitution");
                    Label lbl_TrainingCountry = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingCountry");
                    HiddenField hfTrainingCountry = (HiddenField)gv_Training.Rows[rowIndex].FindControl("hfTrainingCountry");

                    Label lbl_TrainingDescription = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingDescription");
                    Label lbl_TrainingPlace = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingPlace");
                    Label lbl_TrainingAchievment = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingAchievment");
                    Label lbl_TrFromDate = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrFromDate");
                    Label lbl_TrToDate = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrToDate");
                    Label lbl_TrainingDays = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingDays");
                    Label lbl_TrRemarks = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrRemarks");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpTrainingId.Value = dt.Rows[i]["EmpTrainingId"].ToString();
                        lbl_TrainingName.Text = dt.Rows[i]["TrainingName"].ToString();

                        lbl_TrainingType.Text = dt.Rows[i]["TrainingTypeName"].ToString();
                        lbl_TrainingInstitution.Text = dt.Rows[i]["TrainingInstitutionName"].ToString();
                        lbl_TrainingCountry.Text = dt.Rows[i]["TrainingCountryName"].ToString();

                        hfTrainingType.Value = dt.Rows[i]["TrainingType"].ToString();
                        hfTrainingInstitution.Value = dt.Rows[i]["TrainingInstitution"].ToString();
                        hfTrainingCountry.Value = dt.Rows[i]["TrainingCountry"].ToString();

                        lbl_TrainingDescription.Text = dt.Rows[i]["TrainingDescription"].ToString();
                        lbl_TrainingPlace.Text = dt.Rows[i]["TrainingPlace"].ToString();
                        lbl_TrainingAchievment.Text = dt.Rows[i]["TrainingAchievment"].ToString();
                        lbl_TrFromDate.Text = dt.Rows[i]["TrFromDate"].ToString();
                        lbl_TrToDate.Text = dt.Rows[i]["TrToDate"].ToString();
                        lbl_TrainingDays.Text = dt.Rows[i]["TrainingDays"].ToString();
                        lbl_TrRemarks.Text = dt.Rows[i]["TrRemarks"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void lb_EditTraining_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;

        GridViewRow row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
            //gets the row index selected
            int index = row.RowIndex;

            //gets the datakey
            // int itemID = gv_Children.DataKeys(index).Value;

            //access row values and assign it to your TextBox
            //  txt_EmpChildrenName.Text = row.Cells[1].Text;
            //  ddlEmpChildrenGender.SelectedValue = row.Cells[2].Text;
            ////  ddlEmpChildrenOccupation.SelectedItem.Text = row.Cells[3].Text;
            //  txt_EmpChildrenDOB.Text = row.Cells[4].Text;


            //If you are using TemplateField then you can access them using FindControl() method




            txt_TrTrainingName.Text = ((Label)row.FindControl("lbl_TrainingName")).Text;
            ddlTrTrainingType.SelectedValue = string.IsNullOrEmpty(((HiddenField)row.FindControl("hfTrainingType")).Value) ? "-1" : ((HiddenField)row.FindControl("hfTrainingType")).Value;
            txt_TrTrainingDescription.Text = ((Label)row.FindControl("lbl_TrainingDescription")).Text;

            ddlTrTrainingInstitution.SelectedValue = string.IsNullOrEmpty(((HiddenField)row.FindControl("hfTrainingInstitution")).Value) ? "-1" : ((HiddenField)row.FindControl("hfTrainingInstitution")).Value;
            txt_TrTrainingPlace.Text = ((Label)row.FindControl("lbl_TrainingPlace")).Text;
            ddlTrTrainingCountry.SelectedValue = string.IsNullOrEmpty(((HiddenField)row.FindControl("hfTrainingCountry")).Value) ? "-1" : ((HiddenField)row.FindControl("hfTrainingCountry")).Value;


            txt_TrTrainingAchievment.Text = ((Label)row.FindControl("lbl_TrainingAchievment")).Text;
            txt_TrFromDate.Text = ((Label)row.FindControl("lbl_TrFromDate")).Text;
            txt_TrToDate.Text = ((Label)row.FindControl("lbl_TrToDate")).Text;


            txt_TrTrainingDays.Text = ((Label)row.FindControl("lbl_TrainingDays")).Text;
            txt_TrRemarks.Text = ((Label)row.FindControl("lbl_TrRemarks")).Text;


            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["TrainingTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["TrainingTable"];
                dt.Rows.Remove(dt.Rows[rowID]);
                if (dt.Rows.Count > 0)
                {
                    //Store the current data in ViewState for future reference  
                    ViewState["TrainingTable"] = dt;
                    //Re bind the GridView for the updated data  
                    gv_Training.DataSource = dt;
                    gv_Training.DataBind();
                }
                else
                {
                    ViewState["TrainingTable"] = null;
                    //Re bind the GridView for the updated data  
                    gv_Training.DataSource = null;
                    gv_Training.DataBind();
                }
            }
            //Set Previous Data on Postbacks  
            SetPreviousData_Training();
        }
    }

    protected void lb_RemoveTraining_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["TrainingTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["TrainingTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["TrainingTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Training.DataSource = dt;
                gv_Training.DataBind();
            }
            else
            {
                ViewState["TrainingTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Training.DataSource = null;
                gv_Training.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Training();
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        
         #region fff

        try
        {
            string EmpMasterCode = string.Empty;
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblEmpGeneralInfo emp = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {
                    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                    EmpMasterCode = emp.EmpMasterCode;

                    #region 7. Training
                    if (gv_Training.Rows.Count == 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpTraining SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                    }
                    if (gv_Training.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpTraining SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                        for (int i = 0; i < gv_Training.Rows.Count; i++)
                        {
                            HiddenField EmpTrainingId = (HiddenField) gv_Training.Rows[i].FindControl("EmpTrainingId");
                            Label lbl_TrainingName = (Label) gv_Training.Rows[i].FindControl("lbl_TrainingName");
                            HiddenField hfTrainingType = (HiddenField) gv_Training.Rows[i].FindControl("hfTrainingType");
                            Label lbl_TrainingDescription =
                                (Label) gv_Training.Rows[i].FindControl("lbl_TrainingDescription");
                            HiddenField hfTrainingInstitution =
                                (HiddenField) gv_Training.Rows[i].FindControl("hfTrainingInstitution");
                            Label lbl_TrainingPlace = (Label) gv_Training.Rows[i].FindControl("lbl_TrainingPlace");
                            HiddenField hfTrainingCountry =
                                (HiddenField) gv_Training.Rows[i].FindControl("hfTrainingCountry");
                            Label lbl_TrainingAchievment =
                                (Label) gv_Training.Rows[i].FindControl("lbl_TrainingAchievment");
                            Label lbl_TrFromDate = (Label) gv_Training.Rows[i].FindControl("lbl_TrFromDate");
                            Label lbl_TrToDate = (Label) gv_Training.Rows[i].FindControl("lbl_TrToDate");
                            Label lbl_TrainingDays = (Label) gv_Training.Rows[i].FindControl("lbl_TrainingDays");
                            Label lbl_TrRemarks = (Label) gv_Training.Rows[i].FindControl("lbl_TrRemarks");

                            if (string.IsNullOrEmpty(EmpTrainingId.Value))
                            {
                                tblEmpTraining empTraining = new tblEmpTraining();
                                empTraining.EmpInfoId = emp.EmpInfoId;
                                empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text)
                                    ? null
                                    : lbl_TrainingName.Text;
                                empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value)
                                    ? (int?) null
                                    : int.Parse(hfTrainingType.Value);
                                empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text)
                                    ? null
                                    : lbl_TrainingDescription.Text;
                                empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value)
                                    ? (int?) null
                                    : int.Parse(hfTrainingInstitution.Value);
                                empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text)
                                    ? null
                                    : lbl_TrainingPlace.Text;
                                empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value)
                                    ? (int?) null
                                    : int.Parse(hfTrainingCountry.Value);
                                empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text)
                                    ? null
                                    : lbl_TrainingAchievment.Text;
                                empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_TrFromDate.Text).Date;
                                empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_TrToDate.Text).Date;
                                empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text)
                                    ? (int?) null
                                    : int.Parse(lbl_TrainingDays.Text);
                                empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text)
                                    ? null
                                    : lbl_TrRemarks.Text;
                                empTraining.IsActive = true;
                                db.tblEmpTrainings.Add(empTraining);
                            }
                            else
                            {
                                int u_EmpTrainingId = int.Parse(EmpTrainingId.Value);
                                tblEmpTraining empTraining =
                                    (from j in db.tblEmpTrainings where j.EmpTrainingId == u_EmpTrainingId select j)
                                        .FirstOrDefault();
                                empTraining.EmpInfoId = emp.EmpInfoId;
                                empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text)
                                    ? null
                                    : lbl_TrainingName.Text;
                                empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value)
                                    ? (int?) null
                                    : int.Parse(hfTrainingType.Value);
                                empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text)
                                    ? null
                                    : lbl_TrainingDescription.Text;
                                empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value)
                                    ? (int?) null
                                    : int.Parse(hfTrainingInstitution.Value);
                                empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text)
                                    ? null
                                    : lbl_TrainingPlace.Text;
                                empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value)
                                    ? (int?) null
                                    : int.Parse(hfTrainingCountry.Value);
                                empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text)
                                    ? null
                                    : lbl_TrainingAchievment.Text;
                                empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_TrFromDate.Text).Date;
                                empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_TrToDate.Text).Date;
                                empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text)
                                    ? (int?) null
                                    : int.Parse(lbl_TrainingDays.Text);
                                empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text)
                                    ? null
                                    : lbl_TrRemarks.Text;
                                empTraining.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    } ////End Training

                    #endregion end 7. Training
                }
                else
                {
////Start New Mode
                    //emp = new tblEmpGeneralInfo();
                    //#region 7. Training

                    //if (gv_Training.Rows.Count == 0)
                    //{
                    //    //making previous inactive
                    //    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpTraining SET IsActive=0 WHERE EmpInfoId={0}",
                    //        emp.EmpInfoId);
                    //}
                    //if (gv_Training.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gv_Training.Rows.Count; i++)
                    //    {
                    //        HiddenField EmpTrainingId = (HiddenField)gv_Training.Rows[i].FindControl("EmpTrainingId");
                    //        Label lbl_TrainingName = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingName");
                    //        HiddenField hfTrainingType = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingType");
                    //        Label lbl_TrainingDescription = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDescription");
                    //        HiddenField hfTrainingInstitution = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingInstitution");
                    //        Label lbl_TrainingPlace = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingPlace");
                    //        HiddenField hfTrainingCountry = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingCountry");
                    //        Label lbl_TrainingAchievment = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingAchievment");
                    //        Label lbl_TrFromDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrFromDate");
                    //        Label lbl_TrToDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrToDate");
                    //        Label lbl_TrainingDays = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDays");
                    //        Label lbl_TrRemarks = (Label)gv_Training.Rows[i].FindControl("lbl_TrRemarks");

                    //        tblEmpTraining empTraining = new tblEmpTraining();
                    //        empTraining.EmpInfoId = emp.EmpInfoId;
                    //        empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text) ? null : lbl_TrainingName.Text;
                    //        empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value) ? (int?)null : int.Parse(hfTrainingType.Value);
                    //        empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text) ? null : lbl_TrainingDescription.Text;
                    //        empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value) ? (int?)null : int.Parse(hfTrainingInstitution.Value);
                    //        empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text) ? null : lbl_TrainingPlace.Text;
                    //        empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value) ? (int?)null : int.Parse(hfTrainingCountry.Value);
                    //        empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text) ? null : lbl_TrainingAchievment.Text;
                    //        empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrFromDate.Text).Date;
                    //        empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrToDate.Text).Date;
                    //        empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text) ? (int?)null : int.Parse(lbl_TrainingDays.Text);
                    //        empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text) ? null : lbl_TrRemarks.Text;
                    //        empTraining.IsActive = true;
                    //        db.tblEmpTrainings.Add(empTraining);
                    //    }
                    //}////End Training
                    //#endregion end 7. Training

                    //EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                    //////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                    //using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(emp.EmpInfoId))
                    //{
                    //    if (dtEmpCode.Rows.Count > 0)
                    //    {
                    //        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                    //    }

                    //}
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmployeeInfoList.aspx';",
                  true);
            }

           
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
         #endregion
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void btn_Next_OnClick(object sender, EventArgs e)
    {
        #region fff

        try
        {
            string MasterId = string.Empty;
            string EmpMasterCode = string.Empty;
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblEmpGeneralInfo emp = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {
                    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                    EmpMasterCode = emp.EmpMasterCode;
                    MasterId = emp.EmpInfoId.ToString();

                    #region 7. Training
                    if (gv_Training.Rows.Count == 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpTraining SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                    }
                    if (gv_Training.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpTraining SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                        for (int i = 0; i < gv_Training.Rows.Count; i++)
                        {
                            HiddenField EmpTrainingId = (HiddenField)gv_Training.Rows[i].FindControl("EmpTrainingId");
                            Label lbl_TrainingName = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingName");
                            HiddenField hfTrainingType = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingType");
                            Label lbl_TrainingDescription =
                                (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDescription");
                            HiddenField hfTrainingInstitution =
                                (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingInstitution");
                            Label lbl_TrainingPlace = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingPlace");
                            HiddenField hfTrainingCountry =
                                (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingCountry");
                            Label lbl_TrainingAchievment =
                                (Label)gv_Training.Rows[i].FindControl("lbl_TrainingAchievment");
                            Label lbl_TrFromDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrFromDate");
                            Label lbl_TrToDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrToDate");
                            Label lbl_TrainingDays = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDays");
                            Label lbl_TrRemarks = (Label)gv_Training.Rows[i].FindControl("lbl_TrRemarks");

                            if (string.IsNullOrEmpty(EmpTrainingId.Value))
                            {
                                tblEmpTraining empTraining = new tblEmpTraining();
                                empTraining.EmpInfoId = emp.EmpInfoId;
                                empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text)
                                    ? null
                                    : lbl_TrainingName.Text;
                                empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value)
                                    ? (int?)null
                                    : int.Parse(hfTrainingType.Value);
                                empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text)
                                    ? null
                                    : lbl_TrainingDescription.Text;
                                empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value)
                                    ? (int?)null
                                    : int.Parse(hfTrainingInstitution.Value);
                                empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text)
                                    ? null
                                    : lbl_TrainingPlace.Text;
                                empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value)
                                    ? (int?)null
                                    : int.Parse(hfTrainingCountry.Value);
                                empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text)
                                    ? null
                                    : lbl_TrainingAchievment.Text;
                                empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_TrFromDate.Text).Date;
                                empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_TrToDate.Text).Date;
                                empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text)
                                    ? (int?)null
                                    : int.Parse(lbl_TrainingDays.Text);
                                empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text)
                                    ? null
                                    : lbl_TrRemarks.Text;
                                empTraining.IsActive = true;
                                db.tblEmpTrainings.Add(empTraining);
                            }
                            else
                            {
                                int u_EmpTrainingId = int.Parse(EmpTrainingId.Value);
                                tblEmpTraining empTraining =
                                    (from j in db.tblEmpTrainings where j.EmpTrainingId == u_EmpTrainingId select j)
                                        .FirstOrDefault();
                                empTraining.EmpInfoId = emp.EmpInfoId;
                                empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text)
                                    ? null
                                    : lbl_TrainingName.Text;
                                empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value)
                                    ? (int?)null
                                    : int.Parse(hfTrainingType.Value);
                                empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text)
                                    ? null
                                    : lbl_TrainingDescription.Text;
                                empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value)
                                    ? (int?)null
                                    : int.Parse(hfTrainingInstitution.Value);
                                empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text)
                                    ? null
                                    : lbl_TrainingPlace.Text;
                                empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value)
                                    ? (int?)null
                                    : int.Parse(hfTrainingCountry.Value);
                                empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text)
                                    ? null
                                    : lbl_TrainingAchievment.Text;
                                empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_TrFromDate.Text).Date;
                                empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_TrToDate.Text).Date;
                                empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text)
                                    ? (int?)null
                                    : int.Parse(lbl_TrainingDays.Text);
                                empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text)
                                    ? null
                                    : lbl_TrRemarks.Text;
                                empTraining.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    } ////End Training

                    #endregion end 7. Training
                }
                else
                {
                    ////Start New Mode
                    //emp = new tblEmpGeneralInfo();

                    //if (gv_Training.Rows.Count == 0)
                    //{
                    //    //making previous inactive
                    //    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpTraining SET IsActive=0 WHERE EmpInfoId={0}",
                    //        emp.EmpInfoId);
                    //}
                    //#region 7. Training
                    //if (gv_Training.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gv_Training.Rows.Count; i++)
                    //    {
                    //        HiddenField EmpTrainingId = (HiddenField)gv_Training.Rows[i].FindControl("EmpTrainingId");
                    //        Label lbl_TrainingName = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingName");
                    //        HiddenField hfTrainingType = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingType");
                    //        Label lbl_TrainingDescription = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDescription");
                    //        HiddenField hfTrainingInstitution = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingInstitution");
                    //        Label lbl_TrainingPlace = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingPlace");
                    //        HiddenField hfTrainingCountry = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingCountry");
                    //        Label lbl_TrainingAchievment = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingAchievment");
                    //        Label lbl_TrFromDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrFromDate");
                    //        Label lbl_TrToDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrToDate");
                    //        Label lbl_TrainingDays = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDays");
                    //        Label lbl_TrRemarks = (Label)gv_Training.Rows[i].FindControl("lbl_TrRemarks");

                    //        tblEmpTraining empTraining = new tblEmpTraining();
                    //        empTraining.EmpInfoId = emp.EmpInfoId;
                    //        empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text) ? null : lbl_TrainingName.Text;
                    //        empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value) ? (int?)null : int.Parse(hfTrainingType.Value);
                    //        empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text) ? null : lbl_TrainingDescription.Text;
                    //        empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value) ? (int?)null : int.Parse(hfTrainingInstitution.Value);
                    //        empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text) ? null : lbl_TrainingPlace.Text;
                    //        empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value) ? (int?)null : int.Parse(hfTrainingCountry.Value);
                    //        empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text) ? null : lbl_TrainingAchievment.Text;
                    //        empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrFromDate.Text).Date;
                    //        empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrToDate.Text).Date;
                    //        empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text) ? (int?)null : int.Parse(lbl_TrainingDays.Text);
                    //        empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text) ? null : lbl_TrRemarks.Text;
                    //        empTraining.IsActive = true;
                    //        db.tblEmpTrainings.Add(empTraining);
                    //    }
                    //}////End Training
                    //#endregion end 7. Training
                    //MasterId = emp.EmpInfoId.ToString();
                    //EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                    //////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                    //using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(emp.EmpInfoId))
                    //{
                    //    if (dtEmpCode.Rows.Count > 0)
                    //    {
                    //        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                    //    }

                    //}
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmpReference.aspx?mid=" + MasterId + "';",
                 true);
            }

          
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
        #endregion
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpReference?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }

    protected void lbPrevious_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpExperience?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }
}