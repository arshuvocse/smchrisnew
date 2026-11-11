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

public partial class UserSetup_EmpReference : System.Web.UI.Page
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
                        using (DataTable dtReference = _commonDataLoad.GetDTEmpReferenceByEmpId(mid))
                        {
                            if (dtReference.Rows.Count > 0)
                            {
                                ViewState["ReferenceTable"] = dtReference;
                                gv_Reference.DataSource = dtReference;
                                gv_Reference.DataBind();
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
        using (DataTable dt = _commonDataLoad.GetDDLOccupation())
        {
            

            ddlRefOccupation.DataSource = dt;
            ddlRefOccupation.DataValueField = "Value";
            ddlRefOccupation.DataTextField = "TextField";
            ddlRefOccupation.DataBind();

           

        }
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


                    #region 8. Reference
                    if (gv_Reference.Rows.Count == 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpReference SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                    }
                    if (gv_Reference.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpReference SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                        for (int i = 0; i < gv_Reference.Rows.Count; i++)
                        {
                            HiddenField EmpReferenceId =
                                (HiddenField) gv_Reference.Rows[i].FindControl("EmpReferenceId");
                            Label lbl_ReferenceName = (Label) gv_Reference.Rows[i].FindControl("lbl_ReferenceName");
                            HiddenField hfRefOccupation =
                                (HiddenField) gv_Reference.Rows[i].FindControl("hfRefOccupation");
                            Label lbl_RefAddress = (Label) gv_Reference.Rows[i].FindControl("lbl_RefAddress");
                            Label lbl_RefMobile = (Label) gv_Reference.Rows[i].FindControl("lbl_RefMobile");

                            if (string.IsNullOrEmpty(EmpReferenceId.Value))
                            {
                                tblEmpReference empReference = new tblEmpReference();
                                empReference.EmpInfoId = emp.EmpInfoId;
                                empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text)
                                    ? null
                                    : lbl_ReferenceName.Text;
                                empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value)
                                    ? (int?) null
                                    : int.Parse(hfRefOccupation.Value);
                                empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text)
                                    ? null
                                    : lbl_RefAddress.Text;
                                empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text)
                                    ? null
                                    : lbl_RefMobile.Text;
                                empReference.IsActive = true;
                                db.tblEmpReferences.Add(empReference);
                            }
                            else
                            {
                                int u_EmpReferenceId = int.Parse(EmpReferenceId.Value);
                                tblEmpReference empReference =
                                    (from j in db.tblEmpReferences where j.EmpReferenceId == u_EmpReferenceId select j)
                                        .FirstOrDefault();
                                empReference.EmpInfoId = emp.EmpInfoId;
                                empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text)
                                    ? null
                                    : lbl_ReferenceName.Text;
                                empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value)
                                    ? (int?) null
                                    : int.Parse(hfRefOccupation.Value);
                                empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text)
                                    ? null
                                    : lbl_RefAddress.Text;
                                empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text)
                                    ? null
                                    : lbl_RefMobile.Text;
                                empReference.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    } ////End Reference

                    #endregion end 8. Reference
                }
                else
                {
////Start New Mode
                    //emp = new tblEmpGeneralInfo();

                    //#region 8. Reference
                    //if (gv_Reference.Rows.Count == 0)
                    //{
                    //    //making previous inactive
                    //    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpReference SET IsActive=0 WHERE EmpInfoId={0}",
                    //        emp.EmpInfoId);
                    //}
                    //if (gv_Reference.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gv_Reference.Rows.Count; i++)
                    //    {
                    //        HiddenField EmpReferenceId =
                    //            (HiddenField) gv_Reference.Rows[i].FindControl("EmpReferenceId");
                    //        Label lbl_ReferenceName = (Label) gv_Reference.Rows[i].FindControl("lbl_ReferenceName");
                    //        HiddenField hfRefOccupation =
                    //            (HiddenField) gv_Reference.Rows[i].FindControl("hfRefOccupation");
                    //        Label lbl_RefAddress = (Label) gv_Reference.Rows[i].FindControl("lbl_RefAddress");
                    //        Label lbl_RefMobile = (Label) gv_Reference.Rows[i].FindControl("lbl_RefMobile");

                    //        tblEmpReference empReference = new tblEmpReference();
                    //        empReference.EmpInfoId = emp.EmpInfoId;
                    //        empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text)
                    //            ? null
                    //            : lbl_ReferenceName.Text;
                    //        empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value)
                    //            ? (int?) null
                    //            : int.Parse(hfRefOccupation.Value);
                    //        empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text)
                    //            ? null
                    //            : lbl_RefAddress.Text;
                    //        empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text)
                    //            ? null
                    //            : lbl_RefMobile.Text;
                    //        empReference.IsActive = true;
                    //        db.tblEmpReferences.Add(empReference);

                    //    }
                    //} ////End Reference

                    //#endregion end 8. Reference

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
              "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode +
              "');window.location ='EmployeeInfoEntry.aspx';",
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

                    #region 8. Reference

                    if (gv_Reference.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpReference SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                        for (int i = 0; i < gv_Reference.Rows.Count; i++)
                        {
                            HiddenField EmpReferenceId =
                                (HiddenField)gv_Reference.Rows[i].FindControl("EmpReferenceId");
                            Label lbl_ReferenceName = (Label)gv_Reference.Rows[i].FindControl("lbl_ReferenceName");
                            HiddenField hfRefOccupation =
                                (HiddenField)gv_Reference.Rows[i].FindControl("hfRefOccupation");
                            Label lbl_RefAddress = (Label)gv_Reference.Rows[i].FindControl("lbl_RefAddress");
                            Label lbl_RefMobile = (Label)gv_Reference.Rows[i].FindControl("lbl_RefMobile");

                            if (string.IsNullOrEmpty(EmpReferenceId.Value))
                            {
                                tblEmpReference empReference = new tblEmpReference();
                                empReference.EmpInfoId = emp.EmpInfoId;
                                empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text)
                                    ? null
                                    : lbl_ReferenceName.Text;
                                empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value)
                                    ? (int?)null
                                    : int.Parse(hfRefOccupation.Value);
                                empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text)
                                    ? null
                                    : lbl_RefAddress.Text;
                                empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text)
                                    ? null
                                    : lbl_RefMobile.Text;
                                empReference.IsActive = true;
                                db.tblEmpReferences.Add(empReference);
                            }
                            else
                            {
                                int u_EmpReferenceId = int.Parse(EmpReferenceId.Value);
                                tblEmpReference empReference =
                                    (from j in db.tblEmpReferences where j.EmpReferenceId == u_EmpReferenceId select j)
                                        .FirstOrDefault();
                                empReference.EmpInfoId = emp.EmpInfoId;
                                empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text)
                                    ? null
                                    : lbl_ReferenceName.Text;
                                empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value)
                                    ? (int?)null
                                    : int.Parse(hfRefOccupation.Value);
                                empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text)
                                    ? null
                                    : lbl_RefAddress.Text;
                                empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text)
                                    ? null
                                    : lbl_RefMobile.Text;
                                empReference.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    } ////End Reference

                    #endregion end 8. Reference
                }
                else
                {
                    ////Start New Mode
                    //emp = new tblEmpGeneralInfo();

                    //#region 8. Reference

                    //if (gv_Reference.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gv_Reference.Rows.Count; i++)
                    //    {
                    //        HiddenField EmpReferenceId =
                    //            (HiddenField)gv_Reference.Rows[i].FindControl("EmpReferenceId");
                    //        Label lbl_ReferenceName = (Label)gv_Reference.Rows[i].FindControl("lbl_ReferenceName");
                    //        HiddenField hfRefOccupation =
                    //            (HiddenField)gv_Reference.Rows[i].FindControl("hfRefOccupation");
                    //        Label lbl_RefAddress = (Label)gv_Reference.Rows[i].FindControl("lbl_RefAddress");
                    //        Label lbl_RefMobile = (Label)gv_Reference.Rows[i].FindControl("lbl_RefMobile");

                    //        tblEmpReference empReference = new tblEmpReference();
                    //        empReference.EmpInfoId = emp.EmpInfoId;
                    //        empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text)
                    //            ? null
                    //            : lbl_ReferenceName.Text;
                    //        empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value)
                    //            ? (int?)null
                    //            : int.Parse(hfRefOccupation.Value);
                    //        empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text)
                    //            ? null
                    //            : lbl_RefAddress.Text;
                    //        empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text)
                    //            ? null
                    //            : lbl_RefMobile.Text;
                    //        empReference.IsActive = true;
                    //        db.tblEmpReferences.Add(empReference);

                    //    }
                    //} ////End Reference

                    //#endregion end 8. Reference
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
                "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmpNominee.aspx?mid=" + MasterId + "';",
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

    protected void btnAddReference_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Reference();
    }
    private void AddNewToGrid_Reference()
    {
        if (ViewState["ReferenceTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["ReferenceTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpReferenceId"] = DBNull.Value;
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["ReferenceName"] = txt_RefReferenceName.Text;
                drCurrentRow["RefOccupation"] = ddlRefOccupation.SelectedValue;
                drCurrentRow["RefOccupationName"] = ddlRefOccupation.SelectedItem.Text;
                drCurrentRow["RefAddress"] = txt_RefAddress.Text;
                drCurrentRow["RefMobile"] = txt_RefMobile.Text;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["ReferenceTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Reference.DataSource = dtCurrentTable;
                gv_Reference.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("EmpReferenceId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("ReferenceName", typeof(string)));
            dt.Columns.Add(new DataColumn("RefOccupation", typeof(string)));
            dt.Columns.Add(new DataColumn("RefOccupationName", typeof(string)));
            dt.Columns.Add(new DataColumn("RefAddress", typeof(string)));
            dt.Columns.Add(new DataColumn("RefMobile", typeof(string)));

            dr = dt.NewRow();
            dr["EmpReferenceId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["ReferenceName"] = txt_RefReferenceName.Text;
            dr["RefOccupation"] = ddlRefOccupation.SelectedValue;
            dr["RefOccupationName"] = ddlRefOccupation.SelectedItem.Text;
            dr["RefAddress"] = txt_RefAddress.Text;
            dr["RefMobile"] = txt_RefMobile.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["ReferenceTable"] = dt;

            //Bind the Gridview   
            gv_Reference.DataSource = dt;
            gv_Reference.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Reference();
        txt_RefReferenceName.Text = string.Empty;
        ddlRefOccupation.SelectedValue = null;
        txt_RefAddress.Text = string.Empty;
        txt_RefMobile.Text = string.Empty;
    }
    private void SetPreviousData_Reference()
    {
        int rowIndex = 0;
        if (ViewState["ReferenceTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ReferenceTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpReferenceId = (HiddenField)gv_Reference.Rows[rowIndex].FindControl("EmpReferenceId");
                    Label lbl_ReferenceName = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_ReferenceName");
                    HiddenField hfRefOccupation = (HiddenField)gv_Reference.Rows[rowIndex].FindControl("hfRefOccupation");
                    Label lbl_RefOccupation = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_RefOccupation");
                    Label lbl_RefAddress = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_RefAddress");
                    Label lbl_RefMobile = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_RefMobile");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpReferenceId.Value = dt.Rows[i]["EmpReferenceId"].ToString();
                        lbl_ReferenceName.Text = dt.Rows[i]["ReferenceName"].ToString();
                        hfRefOccupation.Value = dt.Rows[i]["RefOccupation"].ToString();
                        lbl_RefOccupation.Text = dt.Rows[i]["RefOccupationName"].ToString();
                        lbl_RefAddress.Text = dt.Rows[i]["RefAddress"].ToString();
                        lbl_RefMobile.Text = dt.Rows[i]["RefMobile"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void lb_EditReference_OnClick(object sender, EventArgs e)
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




            txt_RefReferenceName.Text = ((Label)row.FindControl("lbl_ReferenceName")).Text;
            ddlRefOccupation.SelectedValue = ((HiddenField)row.FindControl("hfRefOccupation")).Value;
            txt_RefAddress.Text = ((Label)row.FindControl("lbl_RefAddress")).Text;



            txt_RefMobile.Text = ((Label)row.FindControl("lbl_RefMobile")).Text;


            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["ReferenceTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["ReferenceTable"];
                dt.Rows.Remove(dt.Rows[rowID]);
                if (dt.Rows.Count > 0)
                {
                    //Store the current data in ViewState for future reference  
                    ViewState["ReferenceTable"] = dt;
                    //Re bind the GridView for the updated data  
                    gv_Reference.DataSource = dt;
                    gv_Reference.DataBind();
                }
                else
                {
                    ViewState["ReferenceTable"] = null;
                    //Re bind the GridView for the updated data  
                    gv_Reference.DataSource = null;
                    gv_Reference.DataBind();
                }
            }
            //Set Previous Data on Postbacks  
            SetPreviousData_Reference();
        }
    }

    protected void lb_RemoveReference_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["ReferenceTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ReferenceTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["ReferenceTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Reference.DataSource = dt;
                gv_Reference.DataBind();
            }
            else
            {
                ViewState["ReferenceTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Reference.DataSource = null;
                gv_Reference.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Reference();
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
            Response.Redirect("EmpNominee?mid=" + EmpId);
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
            Response.Redirect("EmpTraining?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }
}