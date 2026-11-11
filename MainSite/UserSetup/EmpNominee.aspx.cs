using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_EmpNominee : System.Web.UI.Page
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
           

 
            txt_NomDateOfNomination.Attributes.Add("readonly", "readonly");


            txt_NomNomineeDOB.Attributes.Add("readonly", "readonly");



         
        
           
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        empMasterCode.Text =  emp.EmpMasterCode;
                        lblEmpName.Text = emp.EmpName;
                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }
                        using (DataTable dtNominee = _commonDataLoad.GetDTEmpNomineeByEmpId(mid))
                        {
                            if (dtNominee.Rows.Count > 0)
                            {
                                ViewState["NomineeTable"] = dtNominee;
                                gv_Nominee.DataSource = dtNominee;
                                gv_Nominee.DataBind();
                            }
                        }
                    }

                }
            }
        }
    }


    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }
    private void LoadDropDownList()
    {
        using (DataTable dt = _commonDataLoad.GetDDLNominationPurpose())
        {
            ddlNomNominationPurpose.DataSource = dt;
            ddlNomNominationPurpose.DataValueField = "Value";
            ddlNomNominationPurpose.DataTextField = "TextField";
            ddlNomNominationPurpose.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLOccupation())
        {
          

            ddlNomNomineeOccupation.DataSource = dt;
            ddlNomNomineeOccupation.DataValueField = "Value";
            ddlNomNomineeOccupation.DataTextField = "TextField";
            ddlNomNomineeOccupation.DataBind();

          

        }

        using (DataTable dt = _commonDataLoad.GetDDLRelation())
        {
            ddlNomNomineeRelation.DataSource = dt;
            ddlNomNomineeRelation.DataValueField = "Value";
            ddlNomNomineeRelation.DataTextField = "TextField";
            ddlNomNomineeRelation.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLRelation())
        {
            ddlNomNomineeRelation.DataSource = dt;
            ddlNomNomineeRelation.DataValueField = "Value";
            ddlNomNomineeRelation.DataTextField = "TextField";
            ddlNomNomineeRelation.DataBind();
        }
    }

    protected void btnAddNominee_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Nominee();
    }
    private void AddNewToGrid_Nominee()
    {
        if (ViewState["NomineeTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["NomineeTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpNomineeId"] = DBNull.Value;
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                if (ddlNomNominationPurpose.SelectedIndex > 0)
                {
                    drCurrentRow["NominationPurpose"] = ddlNomNominationPurpose.SelectedValue;
                    drCurrentRow["NominationPurposeName"] = ddlNomNominationPurpose.SelectedItem.Text;
                }
                if (ddlNomNomineeOccupation.SelectedIndex > 0)
                {
                    drCurrentRow["NomineeOccupation"] = ddlNomNomineeOccupation.SelectedValue;
                    drCurrentRow["NomineeOccupationName"] = ddlNomNomineeOccupation.SelectedItem.Text;
                }
                if (ddlNomNomineeRelation.SelectedIndex > 0)
                {
                    drCurrentRow["NomineeRelation"] = ddlNomNomineeRelation.SelectedValue;
                    drCurrentRow["NomineeRelationName"] = ddlNomNomineeRelation.SelectedItem.Text;
                }

                if (FileUpload1.HasFile)
                {
                    string _fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                    string AdsFile = "NomNomineImg" + Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.FileName);
                    //  fileName = guid.ToString() + imageFileUpload.FileName;
                    FileUpload1.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
                    drCurrentRow["NominationImage"] = AdsFile;
                    drCurrentRow["ShowNominationImage"] = "../UploadImg/" + AdsFile;
                }
                else
                {
                    drCurrentRow["NominationImage"] = "";
                    drCurrentRow["ShowNominationImage"] = "";
                }

                drCurrentRow["NomineeName"] = txt_NomNomineeName.Text;
                drCurrentRow["DateOfNomination"] = txt_NomDateOfNomination.Text;
                try
                {
                    drCurrentRow["NominationPercentage"] = txt_NomNominationPercentage.Text;
                }
                catch (Exception)
                {

                    //throw;
                }
                drCurrentRow["NomineeDOB"] = txt_NomNomineeDOB.Text;
                drCurrentRow["NomineeTelephone"] = txt_NomNomineeTelephone.Text;
                drCurrentRow["NomineeAddress"] = txt_NomNomineeAddress.Text;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["NomineeTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Nominee.DataSource = dtCurrentTable;
                gv_Nominee.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("EmpNomineeId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("NominationImage", typeof(string)));
            dt.Columns.Add(new DataColumn("NominationPurpose", typeof(string)));
            dt.Columns.Add(new DataColumn("NominationPurposeName", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeOccupation", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeOccupationName", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeRelation", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeRelationName", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeName", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfNomination", typeof(string)));
            dt.Columns.Add(new DataColumn("NominationPercentage", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeDOB", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeTelephone", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeAddress", typeof(string)));
            dt.Columns.Add(new DataColumn("ShowNominationImage", typeof(string)));

            dr = dt.NewRow();
            dr["EmpNomineeId"] = "";
            dr["EmpInfoId"] = hdpk.Value;

            if (ddlNomNominationPurpose.SelectedIndex > 0)
            {
                dr["NominationPurpose"] = ddlNomNominationPurpose.SelectedValue;
                dr["NominationPurposeName"] = ddlNomNominationPurpose.SelectedItem.Text;
            }

            if (FileUpload1.HasFile)
            {
                string _fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                string AdsFile = "NomNomineImg" + Guid.NewGuid().ToString() + Path.GetExtension(FileUpload1.FileName);
                //  fileName = guid.ToString() + imageFileUpload.FileName;
                FileUpload1.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
                dr["NominationImage"] = AdsFile;
                dr["ShowNominationImage"] = "../UploadImg/" + AdsFile;

            }
            else
            {
                dr["NominationImage"] = "";
                dr["ShowNominationImage"] = "";
            }
            if (ddlNomNomineeOccupation.SelectedIndex > 0)
            {
                dr["NomineeOccupation"] = ddlNomNomineeOccupation.SelectedValue;
                dr["NomineeOccupationName"] = ddlNomNomineeOccupation.SelectedItem.Text;
            }
            if (ddlNomNomineeRelation.SelectedIndex > 0)
            {
                dr["NomineeRelation"] = ddlNomNomineeRelation.SelectedValue;
                dr["NomineeRelationName"] = ddlNomNomineeRelation.SelectedItem.Text;
            }

            //dr["NominationPurpose"] = ddlNomNominationPurpose.SelectedValue;
            //dr["NominationPurposeName"] = ddlNomNominationPurpose.SelectedItem.Text;
            //dr["NomineeOccupation"] = ddlNomNomineeOccupation.SelectedValue;
            //dr["NomineeOccupationName"] = ddlNomNomineeOccupation.SelectedItem.Text;
            //dr["NomineeRelation"] = ddlNomNomineeRelation.SelectedValue;
            //dr["NomineeRelationName"] = ddlNomNomineeRelation.SelectedItem.Text;

            dr["NomineeName"] = txt_NomNomineeName.Text;
            dr["DateOfNomination"] = txt_NomDateOfNomination.Text;
            dr["NominationPercentage"] = txt_NomNominationPercentage.Text;
            dr["NomineeDOB"] = txt_NomNomineeDOB.Text;
            dr["NomineeTelephone"] = txt_NomNomineeTelephone.Text;
            dr["NomineeAddress"] = txt_NomNomineeAddress.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["NomineeTable"] = dt;

            //Bind the Gridview   
            gv_Nominee.DataSource = dt;
            gv_Nominee.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Nominee();
        ddlNomNominationPurpose.SelectedValue = null;
        ddlNomNomineeOccupation.SelectedValue = null;
        ddlNomNomineeRelation.SelectedValue = null;
        txt_NomNomineeName.Text = string.Empty;
        txt_NomDateOfNomination.Text = string.Empty;
        txt_NomNominationPercentage.Text = string.Empty;
        txt_NomNomineeDOB.Text = string.Empty;
        txt_NomNomineeTelephone.Text = string.Empty;
        txt_NomNomineeAddress.Text = string.Empty;
    }
    private void SetPreviousData_Nominee()
    {
        int rowIndex = 0;
        if (ViewState["NomineeTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["NomineeTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpNomineeId = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("EmpNomineeId");
                    Label lbl_NominationPurpose = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NominationPurpose");
                    HiddenField hfNominationPurpose = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("hfNominationPurpose");
                    Label lbl_NomineeRelation = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeRelation");
                    HiddenField hfNomineeRelation = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("hfNomineeRelation");
                    Label lbl_NomineeOccupation = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeOccupation");
                    HiddenField hfNomineeOccupation = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("hfNomineeOccupation");
                    Label lbl_NomineeName = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeName");
                    Label lbl_DateOfNomination = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_DateOfNomination");
                    Label lbl_NominationPercentage = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NominationPercentage");
                    Label lbl_NomineeDOB = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeDOB");

                    Label lbl_NomineeTelephone = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeTelephone");
                    Label lbl_NomineeAddress = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeAddress");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpNomineeId.Value = dt.Rows[i]["EmpNomineeId"].ToString();
                        lbl_NominationPurpose.Text = dt.Rows[i]["NominationPurposeName"].ToString();
                        hfNominationPurpose.Value = dt.Rows[i]["NominationPurpose"].ToString();
                        lbl_NomineeRelation.Text = dt.Rows[i]["NomineeRelationName"].ToString();
                        hfNomineeRelation.Value = dt.Rows[i]["NomineeRelation"].ToString();
                        lbl_NomineeOccupation.Text = dt.Rows[i]["NomineeOccupationName"].ToString();
                        hfNomineeOccupation.Value = dt.Rows[i]["NomineeOccupation"].ToString();
                        lbl_NomineeName.Text = dt.Rows[i]["NomineeName"].ToString();
                        lbl_DateOfNomination.Text = dt.Rows[i]["DateOfNomination"].ToString();
                        lbl_NominationPercentage.Text = dt.Rows[i]["NominationPercentage"].ToString();
                        lbl_NomineeDOB.Text = dt.Rows[i]["NomineeDOB"].ToString();
                        lbl_NomineeTelephone.Text = dt.Rows[i]["NomineeTelephone"].ToString();
                        lbl_NomineeAddress.Text = dt.Rows[i]["NomineeAddress"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void lb_EditNominee_OnClick(object sender, EventArgs e)
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


            if ((((HiddenField)row.FindControl("hfNominationPurpose")).Value) != string.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("hfNominationPurpose")).Value) > 0))
                {
                    try
                    {
                        ddlNomNominationPurpose.SelectedValue = ((HiddenField)row.FindControl("hfNominationPurpose")).Value;
                    }
                    catch (Exception)
                    {

                        ddlNomNominationPurpose.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlNomNominationPurpose.SelectedIndex = 0;
                }
            }
            else
            {
                ddlNomNominationPurpose.SelectedIndex = 0;
            }




            txt_NomNomineeName.Text = ((Label)row.FindControl("lbl_NomineeName")).Text;



            if ((((HiddenField)row.FindControl("hfNomineeOccupation")).Value) != string.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("hfNomineeOccupation")).Value) > 0))
                {
                    try
                    {
                        ddlNomNomineeOccupation.SelectedValue = ((HiddenField)row.FindControl("hfNomineeOccupation")).Value;
                    }
                    catch (Exception)
                    {

                        ddlNomNomineeOccupation.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlNomNomineeOccupation.SelectedIndex = 0;
                }
            }
            else
            {
                ddlNomNomineeOccupation.SelectedIndex = 0;
            }


            txt_NomDateOfNomination.Text = ((Label)row.FindControl("lbl_DateOfNomination")).Text;



            txt_NomNominationPercentage.Text = ((Label)row.FindControl("lbl_NominationPercentage")).Text;
            txt_NomNomineeDOB.Text = ((Label)row.FindControl("lbl_NomineeDOB")).Text;


            if (((Label)row.FindControl("lbl_NominationImage")).Text!="")
            {
                //string cont_name = "";
                //cont_name = FileUpload1.FileName;
                //string path = Server.MapPath("emp images/");
                //FileUpload1.SaveAs(path + cont_name);
                //string FullPath = "emp images/" + cont_name;
              //FileUpload  filename = Path.GetFileName(((Label)row.FindControl("lbl_NominationImage")).Text);
              //FileUpload1 = filename;
              //FileUpload1.PostedFile.FileName.ToString()=""  //FileUpload1. = ((Label)row.FindControl("lbl_NominationImage")).Text;
            }
            else
            {

            }
         


            if ((((HiddenField)row.FindControl("hfNomineeRelation")).Value) != string.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("hfNomineeRelation")).Value) > 0))
                {
                    try
                    {
                        ddlNomNomineeRelation.SelectedValue = ((HiddenField)row.FindControl("hfNomineeRelation")).Value;
                    }
                    catch (Exception)
                    {

                        ddlNomNomineeRelation.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlNomNomineeRelation.SelectedIndex = 0;
                }
            }
            else
            {
                ddlNomNomineeRelation.SelectedIndex = 0;
            }

            txt_NomNomineeTelephone.Text = ((Label)row.FindControl("lbl_NomineeTelephone")).Text;

            txt_NomNomineeAddress.Text = ((Label)row.FindControl("lbl_NomineeAddress")).Text;




            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["NomineeTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["NomineeTable"];
                dt.Rows.Remove(dt.Rows[rowID]);
                if (dt.Rows.Count > 0)
                {
                    //Store the current data in ViewState for future reference  
                    ViewState["NomineeTable"] = dt;
                    //Re bind the GridView for the updated data  
                    gv_Nominee.DataSource = dt;
                    gv_Nominee.DataBind();
                }
                else
                {
                    ViewState["NomineeTable"] = null;
                    //Re bind the GridView for the updated data  
                    gv_Nominee.DataSource = null;
                    gv_Nominee.DataBind();
                }
            }
            //Set Previous Data on Postbacks  
            SetPreviousData_Nominee();
        }
    }
    protected void lb_RemoveNominee_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["NomineeTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["NomineeTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["NomineeTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Nominee.DataSource = dt;
                gv_Nominee.DataBind();
            }
            else
            {
                ViewState["NomineeTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Nominee.DataSource = null;
                gv_Nominee.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Nominee();
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

                    #region 9. Nominee

                    if (gv_Nominee.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpNominee SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                        for (int i = 0; i < gv_Nominee.Rows.Count; i++)
                        {
                            HiddenField EmpNomineeId = (HiddenField) gv_Nominee.Rows[i].FindControl("EmpNomineeId");
                            HiddenField hfNominationPurpose =
                                (HiddenField) gv_Nominee.Rows[i].FindControl("hfNominationPurpose");
                            Label lbl_NominationImage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationImage");
                            HiddenField hfNomineeRelation =
                                (HiddenField) gv_Nominee.Rows[i].FindControl("hfNomineeRelation");
                            HiddenField hfNomineeOccupation =
                                (HiddenField) gv_Nominee.Rows[i].FindControl("hfNomineeOccupation");
                            Label lbl_NomineeName = (Label) gv_Nominee.Rows[i].FindControl("lbl_NomineeName");
                            Label lbl_DateOfNomination = (Label) gv_Nominee.Rows[i].FindControl("lbl_DateOfNomination");
                            Label lbl_NominationPercentage =
                                (Label) gv_Nominee.Rows[i].FindControl("lbl_NominationPercentage");
                            Label lbl_NomineeDOB = (Label) gv_Nominee.Rows[i].FindControl("lbl_NomineeDOB");
                            Label lbl_NomineeTelephone = (Label) gv_Nominee.Rows[i].FindControl("lbl_NomineeTelephone");
                            Label lbl_NomineeAddress = (Label) gv_Nominee.Rows[i].FindControl("lbl_NomineeAddress");

                            if (string.IsNullOrEmpty(EmpNomineeId.Value))
                            {
                                tblEmpNominee empNominee = new tblEmpNominee();
                                empNominee.EmpInfoId = emp.EmpInfoId;
                                empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value)
                                    ? (int?) null
                                    : int.Parse(hfNominationPurpose.Value);
                                empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value)
                                    ? (int?) null
                                    : int.Parse(hfNomineeRelation.Value);
                                empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value)
                                    ? (int?) null
                                    : int.Parse(hfNomineeOccupation.Value);
                                empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text)
                                    ? null
                                    : lbl_NomineeName.Text;
                                empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                                empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text)
                                    ? (decimal?) null
                                    : decimal.Parse(lbl_NominationPercentage.Text);
                                empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_NomineeDOB.Text).Date;

                                empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text)
                                    ? null
                                    : lbl_NomineeTelephone.Text;
                                empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text)
                                    ? null
                                    : lbl_NomineeAddress.Text;
                                empNominee.IsActive = true;
                                empNominee.NomNomineImg = string.IsNullOrEmpty(lbl_NominationImage.Text) ? null : lbl_NominationImage.Text;
                                db.tblEmpNominees.Add(empNominee);
                            }
                            else
                            {
                                int u_EmpNomineeId = int.Parse(EmpNomineeId.Value);
                                tblEmpNominee empNominee =
                                    (from j in db.tblEmpNominees where j.EmpNomineeId == u_EmpNomineeId select j)
                                        .FirstOrDefault();
                                empNominee.EmpInfoId = emp.EmpInfoId;
                                empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value)
                                    ? (int?) null
                                    : int.Parse(hfNominationPurpose.Value);
                                empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value)
                                    ? (int?) null
                                    : int.Parse(hfNomineeRelation.Value);

                                empNominee.NomNomineImg = string.IsNullOrEmpty(lbl_NominationImage.Text) ? null : lbl_NominationImage.Text;
                                empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value)
                                    ? (int?) null
                                    : int.Parse(hfNomineeOccupation.Value);
                                empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text)
                                    ? null
                                    : lbl_NomineeName.Text;
                                empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                                empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text)
                                    ? (decimal?) null
                                    : decimal.Parse(lbl_NominationPercentage.Text);
                                empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text)
                                    ? (DateTime?) null
                                    : DateTime.Parse(lbl_NomineeDOB.Text).Date;

                                empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text)
                                    ? null
                                    : lbl_NomineeTelephone.Text;
                                empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text)
                                    ? null
                                    : lbl_NomineeAddress.Text;
                                empNominee.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    } ////End Nominee

                    #endregion end 9. Nominee
                }
                else
                {
////Start New Mode
                    //emp = new tblEmpGeneralInfo();

                    //#region 9. Nominee
                    //if (gv_Nominee.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gv_Nominee.Rows.Count; i++)
                    //    {
                    //        HiddenField EmpNomineeId = (HiddenField)gv_Nominee.Rows[i].FindControl("EmpNomineeId");
                    //        HiddenField hfNominationPurpose = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNominationPurpose");
                    //        HiddenField hfNomineeRelation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeRelation");
                    //        HiddenField hfNomineeOccupation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeOccupation");
                    //        Label lbl_NomineeName = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeName");
                    //        Label lbl_NominationImage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationImage");
                    //        Label lbl_DateOfNomination = (Label)gv_Nominee.Rows[i].FindControl("lbl_DateOfNomination");
                    //        Label lbl_NominationPercentage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationPercentage");
                    //        Label lbl_NomineeDOB = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeDOB");
                    //        Label lbl_NomineeTelephone = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeTelephone");
                    //        Label lbl_NomineeAddress = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeAddress");


                    //        tblEmpNominee empNominee = new tblEmpNominee();
                    //        empNominee.EmpInfoId = emp.EmpInfoId;
                    //        empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value) ? (int?)null : int.Parse(hfNominationPurpose.Value);
                    //        empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value) ? (int?)null : int.Parse(hfNomineeRelation.Value);
                    //        empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value) ? (int?)null : int.Parse(hfNomineeOccupation.Value);
                    //        empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text) ? null : lbl_NomineeName.Text;
                    //        empNominee.NomNomineImg = string.IsNullOrEmpty(lbl_NominationImage.Text) ? null : lbl_NominationImage.Text;
                    //        empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text) ? (DateTime?)null : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                    //        empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text) ? (decimal?)null : decimal.Parse(lbl_NominationPercentage.Text);
                    //        empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_NomineeDOB.Text).Date;

                    //        empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text) ? null : lbl_NomineeTelephone.Text;
                    //        empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text) ? null : lbl_NomineeAddress.Text;
                    //        empNominee.IsActive = true;
                    //        db.tblEmpNominees.Add(empNominee);
                    //    }
                    //}////End Nominee
                    //#endregion end 9. Nominee
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
            
                //empMasterCode.Text = emp.EmpMasterCode;
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

                    #region 9. Nominee
                    MasterId = emp.EmpInfoId.ToString();
                    if (gv_Nominee.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpNominee SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                        for (int i = 0; i < gv_Nominee.Rows.Count; i++)
                        {
                            HiddenField EmpNomineeId = (HiddenField)gv_Nominee.Rows[i].FindControl("EmpNomineeId");
                            HiddenField hfNominationPurpose =
                                (HiddenField)gv_Nominee.Rows[i].FindControl("hfNominationPurpose");
                            HiddenField hfNomineeRelation =
                                (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeRelation");
                            HiddenField hfNomineeOccupation =
                                (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeOccupation");
                            Label lbl_NomineeName = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeName");
                            Label lbl_DateOfNomination = (Label)gv_Nominee.Rows[i].FindControl("lbl_DateOfNomination");
                            Label lbl_NominationPercentage =
                                (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationPercentage");
                            Label lbl_NomineeDOB = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeDOB");
                            Label lbl_NomineeTelephone = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeTelephone");
                            Label lbl_NomineeAddress = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeAddress");
                            Label lbl_NominationImage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationImage");
                            if (string.IsNullOrEmpty(EmpNomineeId.Value))
                            {
                                tblEmpNominee empNominee = new tblEmpNominee();
                                empNominee.EmpInfoId = emp.EmpInfoId;
                                empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value)
                                    ? (int?)null
                                    : int.Parse(hfNominationPurpose.Value);
                                empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value)
                                    ? (int?)null
                                    : int.Parse(hfNomineeRelation.Value);
                                empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value)
                                    ? (int?)null
                                    : int.Parse(hfNomineeOccupation.Value);
                                empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text)
                                    ? null
                                    : lbl_NomineeName.Text;
                                empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                                empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text)
                                    ? (decimal?)null
                                    : decimal.Parse(lbl_NominationPercentage.Text);
                                empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_NomineeDOB.Text).Date;
                                empNominee.NomNomineImg = string.IsNullOrEmpty(lbl_NominationImage.Text) ? null : lbl_NominationImage.Text;
                                empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text)
                                    ? null
                                    : lbl_NomineeTelephone.Text;
                                empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text)
                                    ? null
                                    : lbl_NomineeAddress.Text;
                                empNominee.IsActive = true;
                                db.tblEmpNominees.Add(empNominee);
                            }
                            else
                            {
                                int u_EmpNomineeId = int.Parse(EmpNomineeId.Value);
                                tblEmpNominee empNominee =
                                    (from j in db.tblEmpNominees where j.EmpNomineeId == u_EmpNomineeId select j)
                                        .FirstOrDefault();
                                empNominee.EmpInfoId = emp.EmpInfoId;
                                empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value)
                                    ? (int?)null
                                    : int.Parse(hfNominationPurpose.Value);
                                empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value)
                                    ? (int?)null
                                    : int.Parse(hfNomineeRelation.Value);
                                empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value)
                                    ? (int?)null
                                    : int.Parse(hfNomineeOccupation.Value);
                                empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text)
                                    ? null
                                    : lbl_NomineeName.Text;
                                empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                                empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text)
                                    ? (decimal?)null
                                    : decimal.Parse(lbl_NominationPercentage.Text);
                                empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text)
                                    ? (DateTime?)null
                                    : DateTime.Parse(lbl_NomineeDOB.Text).Date;

                                empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text)
                                    ? null
                                    : lbl_NomineeTelephone.Text;
                                empNominee.NomNomineImg = string.IsNullOrEmpty(lbl_NominationImage.Text) ? null : lbl_NominationImage.Text;
                                empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text)
                                    ? null
                                    : lbl_NomineeAddress.Text;
                                empNominee.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    } ////End Nominee

                    #endregion end 9. Nominee
                }
                else
                {
                    //////Start New Mode
                    //emp = new tblEmpGeneralInfo();

                    //#region 9. Nominee
                    //if (gv_Nominee.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gv_Nominee.Rows.Count; i++)
                    //    {
                    //        HiddenField EmpNomineeId = (HiddenField)gv_Nominee.Rows[i].FindControl("EmpNomineeId");
                    //        HiddenField hfNominationPurpose = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNominationPurpose");
                    //        HiddenField hfNomineeRelation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeRelation");
                    //        HiddenField hfNomineeOccupation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeOccupation");
                    //        Label lbl_NomineeName = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeName");
                    //        Label lbl_DateOfNomination = (Label)gv_Nominee.Rows[i].FindControl("lbl_DateOfNomination");
                    //        Label lbl_NominationPercentage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationPercentage");
                    //        Label lbl_NomineeDOB = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeDOB");
                    //        Label lbl_NomineeTelephone = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeTelephone");
                    //        Label lbl_NomineeAddress = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeAddress");
                    //        Label lbl_NominationImage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationImage");

                    //        tblEmpNominee empNominee = new tblEmpNominee();
                    //        empNominee.EmpInfoId = emp.EmpInfoId;
                    //        empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value) ? (int?)null : int.Parse(hfNominationPurpose.Value);
                    //        empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value) ? (int?)null : int.Parse(hfNomineeRelation.Value);
                    //        empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value) ? (int?)null : int.Parse(hfNomineeOccupation.Value);
                    //        empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text) ? null : lbl_NomineeName.Text;
                    //        empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text) ? (DateTime?)null : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                    //        empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text) ? (decimal?)null : decimal.Parse(lbl_NominationPercentage.Text);
                    //        empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_NomineeDOB.Text).Date;
                    //        empNominee.NomNomineImg = string.IsNullOrEmpty(lbl_NominationImage.Text) ? null : lbl_NominationImage.Text;
                    //        empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text) ? null : lbl_NomineeTelephone.Text;
                    //        empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text) ? null : lbl_NomineeAddress.Text;
                    //        empNominee.IsActive = true;
                    //        db.tblEmpNominees.Add(empNominee);
                    //    }
                    //}////End Nominee
                    //#endregion end 9. Nominee
                   
                    //////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                    //MasterId = emp.EmpInfoId.ToString();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...! );",
                true);
                Response.Redirect("EmpOthers.aspx?mid=" + MasterId);
            }

           
            //ScriptManager.RegisterStartupScript(this, this.GetType(),
            //     "alert",
            //     "alert('Operation Successful...! );window.location ='EmpOthers.aspx?mid=" + MasterId + "';",
            //     true);
            //empMasterCode.Text = emp.EmpMasterCode;
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
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx"); 
    }

    protected void lbPrevious_OnClick(object sender, EventArgs e)
    {
        string  EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpReference?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
       
    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpOthers.aspx?mid=" + EmpId);
        }

        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }

  
}