using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MemberInformation : System.Web.UI.Page
{

    MemberInfoDaL aMinors = new MemberInfoDaL();

    ShowMessage aShowMessage = new ShowMessage();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtJoiningDate.Attributes.Add("readonly", "readonly");
            TxtMembershipDate.Attributes.Add("readonly", "readonly");
            Dropdownlist();
            DataTable dt = new DataTable();
          
            dt.Columns.Add(new DataColumn("MemberType", typeof(string)));
            dt.Columns.Add(new DataColumn("Company", typeof(string)));
            dt.Columns.Add(new DataColumn("CompanyId", typeof(string)));
            dt.Columns.Add(new DataColumn("MeetingMemberTypeId", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("MobileNo", typeof(string)));
            dt.Columns.Add(new DataColumn("JoiningDate", typeof(string)));
            dt.Columns.Add(new DataColumn("MembershipDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Note", typeof(string)));
            dt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            gv_Children.DataSource = dt;
            gv_Children.DataBind();
            ButtonVisible();
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                btn_Save.Visible = false;
                MemberMasterdHiddenField.Value = (Request.QueryString["MID"]);
                GetRecord(Convert.ToInt32(MemberMasterdHiddenField.Value));
                
            }
        }
    }




    public void ButtonVisible()
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

            Response.Redirect("MemberInformationView.aspx");
        }


    }

    private void GetRecord(Int32 Id)
    {
        DataTable Dt = aMinors.GetforEdit(Id);


        if (Dt.Rows.Count > 0)
        {
            try
            {
                ddlOrderNo.SelectedValue = Dt.Rows[0]["OrderNo"].ToString();

            }
            catch (Exception)
            {
                ddlOrderNo.SelectedValue = null;

                //throw;
            }

            ddlCompany.SelectedValue = Dt.Rows[0]["CompanyId"].ToString();
            ddlMemberType.SelectedValue = Dt.Rows[0]["MeetingMemberTypeId"].ToString();
            TxtName.Text = Dt.Rows[0].Field<string>("Name").ToString();
            TxtAddress.Text = Dt.Rows[0].Field<string>("Address").ToString();
            TxtPhone.Text = Dt.Rows[0].Field<string>("MobileNo").ToString();
            TxtEmail.Text = Dt.Rows[0].Field<string>("Email").ToString();

            try
            {
                txtJoiningDate.Text = Dt.Rows[0].Field<string>("JoiningDate").ToString();
            }
            catch (Exception)
            {
                
                //throw;
            }


            try
            {
                TxtMembershipDate.Text = Dt.Rows[0].Field<string>("MembershipDate").ToString();

            }
            catch (Exception)
            {

                //throw;
            }

            TxtNote.Text = Dt.Rows[0].Field<string>("Note").ToString();
            

            gv_Children.DataSource = null;
            gv_Children.DataBind();
            ViewState["MemberTable"] = null;

          
        }

    }

    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();

    private void Dropdownlist()
    {

        using (DataTable dt = aMinors.GetDDLMemberType())
        {
            ddlMemberType.DataSource = dt;
            ddlMemberType.DataValueField = "Value";
            ddlMemberType.DataTextField = "TextField";
            ddlMemberType.DataBind();
        }
        AMAsterDal.GetCategoryListIntoDropdown(ddlCategory);

        using (DataTable dt = aMinors.GetDDLCompany())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }
        //using (DataTable dt = aMinors.GetDDLmemberType())
        //{
        //    ddlMemberType.DataSource = dt;
        //    ddlMemberType.DataValueField = "Value";
        //    ddlMemberType.DataTextField = "TextField";
        //    ddlMemberType.DataBind();
           
            
        //}

        ddlOrderNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
        

        for (int i = 1; i <= 100; i++)
        {
            ddlOrderNo.Items.Add(i.ToString());
        }
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnAddtolist_OnClick(object sender, EventArgs e)
    {
        if (DataValidation2())
        {

            
                AddNewToGrid_Children();
            

           
        }
        else
        {
            //TxtName.Focus();
            //aShowMessage.ShowMessageBox("please fill out this field!!! ", this);
        }
      
    }

    private void AddNewToGrid_Children()
    {
        if (ViewState["MemberTable"] != null)
        {
           

                DataTable dtCurrentTable = (DataTable) ViewState["MemberTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();


                    if (ddlCompany.SelectedIndex > 0)
                    {
                        drCurrentRow["Company"] = ddlCompany.SelectedItem.Text.Trim();
                        drCurrentRow["CompanyId"] = ddlCompany.SelectedValue.Trim();
                    }
                    else
                    {
                        drCurrentRow["Company"] = null;
                        drCurrentRow["CompanyId"] = null;
                    }




                    if (ddlMemberType.SelectedIndex > 0)
                    {
                        drCurrentRow["MemberType"] = ddlMemberType.SelectedItem.Text.Trim();
                        drCurrentRow["MeetingMemberTypeId"] = ddlMemberType.SelectedValue.Trim();
                    }
                    else
                    {
                        drCurrentRow["MemberType"] = null;
                        drCurrentRow["MeetingMemberTypeId"] = null;
                    }


                    drCurrentRow["Name"] = TxtName.Text;
                    drCurrentRow["Address"] = TxtAddress.Text;

                    drCurrentRow["Email"] = TxtEmail.Text;
                    drCurrentRow["MobileNo"] = TxtPhone.Text;
                    try
                    {
                        drCurrentRow["MembershipDate"] = (TxtMembershipDate.Text);
                    }
                    catch (Exception)
                    {
                        drCurrentRow["MembershipDate"] = null;
                        //throw;
                    }

                    try
                    {
                        drCurrentRow["JoiningDate"] = (txtJoiningDate.Text);
                    }
                    catch (Exception)
                    {
                        drCurrentRow["JoiningDate"] = null;
                        //throw;
                    }

                    drCurrentRow["OrderNo"] = ddlOrderNo.SelectedValue
                        ;
                    drCurrentRow["Note"] = TxtNote.Text;
                    //add new row to DataTable   
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState for future reference   
                    ViewState["MemberTable"] = dtCurrentTable;

                    //Rebind the Grid with the current data to reflect changes   
                    gv_Children.DataSource = dtCurrentTable;
                    gv_Children.DataBind();
                
            }
            
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("MemberType", typeof(string)));
            dt.Columns.Add(new DataColumn("Company", typeof(string)));
            dt.Columns.Add(new DataColumn("CompanyId", typeof(string)));
            dt.Columns.Add(new DataColumn("MeetingMemberTypeId", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Email", typeof(string)));
            dt.Columns.Add(new DataColumn("MobileNo", typeof(string)));
            dt.Columns.Add(new DataColumn("JoiningDate", typeof(string)));
            dt.Columns.Add(new DataColumn("MembershipDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Note", typeof(string)));
            dt.Columns.Add(new DataColumn("OrderNo", typeof(string)));

            dr = dt.NewRow();



            if (ddlCompany.SelectedIndex > 0)
            {
                dr["Company"] = ddlCompany.SelectedItem.Text.Trim();
                dr["CompanyId"] = ddlCompany.SelectedValue.Trim();
            }
            else
            {
                dr["Company"] = null;
                dr["CompanyId"] = null;
            }

            if (ddlMemberType.SelectedIndex > 0)
            {
                dr["MemberType"] = ddlMemberType.SelectedItem.Text.Trim();
                dr["MeetingMemberTypeId"] = ddlMemberType.SelectedValue.Trim();
            }
            else
            {
                 dr["MemberType"] = null;
                dr["MeetingMemberTypeId"] = null;
            }
            dr["OrderNo"] = ddlOrderNo.SelectedValue
                ;
            dr["Name"] = TxtName.Text;
            dr["Address"] = TxtAddress.Text;
            dr["Email"] = TxtEmail.Text;
            dr["MobileNo"] = TxtPhone.Text;
            dr["JoiningDate"] = txtJoiningDate.Text.ToString();
            dr["MembershipDate"] = TxtMembershipDate.Text.ToString();
            dr["Note"] = TxtNote.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["MemberTable"] = dt;

            //Bind the Gridview   
            gv_Children.DataSource = dt;
            gv_Children.DataBind();
        }
        //Set Previous Data on Postbacks   
      //  SetPreviousData_Children();
       // txtMemberType.SelectedItem.Text = string.Empty;
      //  txtMemberType.SelectedValue = string.Empty;
        txtMemberType.Text = string.Empty;
        ddlCompany.SelectedValue = null;
        ddlMemberType.SelectedValue = null;
        ddlOrderNo.SelectedValue = null;
        TxtName.Text = string.Empty;
        TxtAddress.Text = null;
        TxtEmail.Text = null;
        TxtPhone.Text = null;
        TxtMembershipDate.Text = null;
        txtJoiningDate.Text = null;
        TxtNote.Text = null;

        //txt_EmpChildrenDOB.Text = string.Empty;
        //ddlChildrenMaritalStatus.SelectedValue = string.Empty;

    }


    private void SetPreviousData_Children()
    {
        int rowIndex = 0;
        if (ViewState["ChildrenTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ChildrenTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lbl_MemberType = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_MemberType");
                    HiddenField _MemberTypeId = (HiddenField)gv_Children.Rows[rowIndex].FindControl("HFMemberTypeId");
             
                    Label lbl_Name = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_Name");
                    Label lbl_Address = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_Address");
                    Label lbl_Email =
                        (Label)gv_Children.Rows[rowIndex].FindControl("lbl_Email");

                    Label lbl_Phone = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_Phone");
                    Label lbl_Date = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_Date");
                    Label lbl_Note =
                        (Label)gv_Children.Rows[rowIndex].FindControl("lbl_Note");

                    if (i < dt.Rows.Count - 1)
                    {
                   
                        lbl_MemberType.Text = dt.Rows[i]["MemberTypeName"].ToString();
                        _MemberTypeId.Value = dt.Rows[i]["MemberTypeId"].ToString();
                        lbl_Name.Text = dt.Rows[i]["Name"].ToString();
                        lbl_Address.Text = dt.Rows[i]["Address"].ToString();
                        lbl_Email.Text = dt.Rows[i]["Email"].ToString();
                        lbl_Phone.Text = dt.Rows[i]["MobileNo"].ToString();
                        lbl_Date.Text = dt.Rows[i]["MembershipDate"].ToString();
                        lbl_Note.Text = dt.Rows[i]["Note"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }


    protected void lb_RemoveMember_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["MemberTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["MemberTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["MemberTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Children.DataSource = dt;
                gv_Children.DataBind();
            }
            else
            {
                ViewState["MemberTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Children.DataSource = null;
                gv_Children.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
      //  SetPreviousData_Children();
    }




    protected void lb_EditMember_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
        
            int index = row.RowIndex;

            TxtName.Text = ((Label)row.FindControl("lbl_Name")).Text;
            try
            {
                ddlMemberType.SelectedValue = ((HiddenField)row.FindControl("hfMeetingMemberTypeId")).Value;
            
            }
            catch (Exception)
            {
                ddlMemberType.SelectedValue = null;
                //throw;
            }


            try
            {
                ddlOrderNo.SelectedValue = ((Label)row.FindControl("lbl_OrderNo")).Text;

            }
            catch (Exception)
            {
                ddlOrderNo.SelectedValue = null;
                //throw;
            }


            try
            {
                ddlCompany.SelectedValue = ((HiddenField)row.FindControl("hfCompanyId")).Value;

            }
            catch (Exception)
            {
                ddlCompany.SelectedValue = null;
                //throw;
            }

            TxtAddress.Text = ((Label)row.FindControl("lbl_Address")).Text;

            TxtPhone.Text = ((Label)row.FindControl("lbl_Phone")).Text;
            TxtEmail.Text = ((Label)row.FindControl("lbl_Email")).Text;

            TxtMembershipDate.Text = ((Label)row.FindControl("lbl_Date")).Text;
            txtJoiningDate.Text = ((Label)row.FindControl("lbl_JoiningDate")).Text;

            TxtNote.Text = ((Label)row.FindControl("lbl_Note")).Text;

            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["MemberTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["MemberTable"];
                dt.Rows.Remove(dt.Rows[rowID]);
                if (dt.Rows.Count > 0)
                {
                    //Store the current data in ViewState for future reference  
                    ViewState["MemberTable"] = dt;
                    //Re bind the GridView for the updated data  
                    gv_Children.DataSource = dt;
                    gv_Children.DataBind();
                }
                else
                {
                    ViewState["MemberTable"] = null;
                    //Re bind the GridView for the updated data  
                    gv_Children.DataSource = null;
                    gv_Children.DataBind();
                }
            }
            else
            {
                DataTable dt = (DataTable)ViewState["MemberTable"];

      


                dt.Rows.Remove(dt.Rows[rowID]);
                if (dt.Rows.Count > 0)
                {
                    //Store the current data in ViewState for future reference  
                    ViewState["MemberTable"] = dt;
                    //Re bind the GridView for the updated data  
                    gv_Children.DataSource = dt;
                    gv_Children.DataBind();
                }
                else
                {
                    ViewState["MemberTable"] = null;
                    //Re bind the GridView for the updated data  
                    gv_Children.DataSource = null;
                    gv_Children.DataBind();
                }
            }
            //Set Previous Data on Postbacks  
           // SetPreviousData_Children();
        }
    }


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

        if (DataValidation())
        {
            if (MemberMasterdHiddenField.Value == "")
            {
                try
                {
                    Int32 masterId = 0;// SaveChangesMaster();

                   
                        Int32 detailsId = SaveChangesforPettycashDetails(masterId);

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Operation Successful...');window.location ='MemberInformationView.aspx';",
              true);
                       
                    
                    
                    
                }
                catch (Exception ex)
                {
                   //throw 
                }
            }
            else
            {
                try
                {
                    if (SaveChangesUpdateMaster())
                    {
                        //UpdateChangesMaster();
                        aShowMessage.ShowMessageBox("Data Update Successfully",this);
                        Response.Redirect("MemberInformationView.aspx");
                        Clear();
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
                
            }


        }



    }


    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (DataValidation2())
        {
            if (MemberMasterdHiddenField.Value != "")
            {

                try
                {
                  
                    if (SaveChangesUpdateMaster())
                    {
                        //aMinors.deleteDetails(MemberMasterdHiddenField.Value);
                       // Int32 detailsId = SaveChangesforPettycashDetails(Convert.ToInt32(MemberMasterdHiddenField.Value));


                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successful...');window.location ='MemberInformationView.aspx';",
                            true);
                    }

                }
                catch (Exception exception)
                {
                    
                }

            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (MemberMasterdHiddenField.Value != "")
        {
            try
            {

               
               

                
                    if (aMinors.DeleteMaster(MemberMasterdHiddenField.Value))
                    {
                      
                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Operation Successful...');window.location ='MemberInformationView.aspx';",
              true);
                       
                    }
                   
                

            }
            catch (Exception ex)
            {
                //throw 
            }
        }

    }

    protected  void cancelButton_OnClick (object sender, EventArgs e)
    {

    }

    private void Clear()
    {
        MemberMasterdHiddenField.Value = string.Empty;


    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool DataValidation()
    {
       

        if (gv_Children.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Please Add Member Information", this);
            return false;
        }

        //if (ddlCategory.SelectedValue != "")
        //{





        //    using (DataTable dt = aMinors.GetCheckPartInfo(ddlCompany.SelectedValue, ddlCategory.SelectedValue))
        //    {
        //        if (MemberMasterdHiddenField.Value == "")
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                showMessageBox("Already Exist!!!");
        //                ddlCategory.Focus();

        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            DataTable dt2 = aMinors.GetCheckPartInfo2(ddlCompany.SelectedValue, ddlCategory.SelectedValue, MemberMasterdHiddenField.Value);
        //            if (dt2.Rows.Count > 0)
        //            {


        //                showMessageBox("Already Exist!!!");
        //                ddlCategory.Focus();
        //                return false;
        //            }
        //        }

        //    }
           


        //}

        return true;
    }
    private bool DataValidation2()
    {
        if (ddlCompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company", this);
            ddlCompany.Focus();
            return false;
        }

        if (ddlMemberType.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Member Type", this);
            ddlMemberType.Focus();
            return false;

        }
        if (TxtName.Text == "")
        {
            aShowMessage.ShowMessageBox("please fill out this field", this);
            TxtName.Focus();
            return false;

        }

      
        return true;
    }


    private MemberSetupMaster PrepareDataForSaveMaster()
    {

        MemberSetupMaster aMaster = new MemberSetupMaster();
        {
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            aMaster.Description = TxtDescription.Text.ToString();
            aMaster.CreateBy = Session["UserId"].ToString();
            aMaster.CreateDate = DateTime.Now;

        }
        ;

        return aMaster;

    }


    private MemberSetupMaster PrepareDataForSaveMasterDelete()
    {

        MemberSetupMaster aMaster = new MemberSetupMaster();
        {
            aMaster.BMemberSetupMasterID = Convert.ToInt32(MemberMasterdHiddenField.Value);
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.Description = TxtDescription.Text.ToString();
            aMaster.CreateBy = Session["UserId"].ToString();
            aMaster.CreateDate = DateTime.Now;

        }
        ;

        return aMaster;

    }

    private MemberDetailsInfoNewDAO PrepareDataForUpdateMaster()
    {

        MemberDetailsInfoNewDAO aMaster = new MemberDetailsInfoNewDAO();
        {
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedIndex > 0 ? ddlCompany.SelectedValue : null);
            aMaster.OrderNo = Convert.ToInt32(ddlOrderNo.SelectedIndex > 0 ? ddlOrderNo.SelectedValue : null);
            aMaster.MemberSetupDetailsID = 0;
            aMaster.MeetingMemberTypeId = Convert.ToInt32(ddlMemberType.SelectedIndex > 0 ? ddlMemberType.SelectedValue : null);
            if (ddlMemberType.SelectedIndex > 0)
            {
                aMaster.MemberType = ddlMemberType.SelectedItem.Text;
            }

            aMaster.Name = TxtName.Text.ToString();
            aMaster.Address = TxtAddress.Text.ToString();
            aMaster.Email = TxtEmail.Text.Trim().ToString();
            aMaster.MobileNo = (TxtPhone.Text);
            try
            {
                aMaster.MembershipDate = Convert.ToDateTime(TxtMembershipDate.Text);
            }
            catch (Exception)
            {
                aMaster.MembershipDate = null;
                //throw;
            }

            try
            {
                aMaster.JoiningDate = Convert.ToDateTime(txtJoiningDate.Text);
            }
            catch (Exception)
            {
                aMaster.JoiningDate = null;
                //throw;
            }
            aMaster.Note = TxtNote.Text.ToString();
        }
        ;

        return aMaster;

    }

    private List<MemberDetailsInfoNewDAO> PrepareDataForSaveDetails(Int32 Val)
    {
        MemberDetailsInfoNewDAO aMemberSetupDetails;
        List<MemberDetailsInfoNewDAO> aMemberSetupDetailsesList = new List<MemberDetailsInfoNewDAO>();

        for (int i = 0; i < gv_Children.Rows.Count; i++)
        {

            HiddenField hfCompanyId = (HiddenField)gv_Children.Rows[i].FindControl("hfCompanyId");
            HiddenField hfMeetingMemberTypeId = (HiddenField)gv_Children.Rows[i].FindControl("hfMeetingMemberTypeId");
            Label lbl_MemberType = (Label)gv_Children.Rows[i].FindControl("lbl_MemberType");
            Label lbl_OrderNo = (Label)gv_Children.Rows[i].FindControl("lbl_OrderNo");


            Label lbl_Name = (Label)gv_Children.Rows[i].FindControl("lbl_Name");
            Label lbl_Address = (Label)gv_Children.Rows[i].FindControl("lbl_Address");
            Label lbl_Email =
                (Label)gv_Children.Rows[i].FindControl("lbl_Email");
            Label lbl_Phone = (Label)gv_Children.Rows[i].FindControl("lbl_Phone");
            Label lbl_Date = (Label)gv_Children.Rows[i].FindControl("lbl_Date");
            Label lbl_JoiningDate = (Label)gv_Children.Rows[i].FindControl("lbl_JoiningDate");
            Label lbl_Note =
                (Label)gv_Children.Rows[i].FindControl("lbl_Note");
            aMemberSetupDetails = new MemberDetailsInfoNewDAO();
            aMemberSetupDetails.MemberSetupMasterID = Val;
            aMemberSetupDetails.MemberType = lbl_MemberType.Text.ToString().Trim();

            try
            {
                aMemberSetupDetails.OrderNo = Convert.ToInt32(lbl_OrderNo.Text.Trim());
            }
            catch (Exception)
            {
                aMemberSetupDetails.OrderNo = null;
                //throw;
            }

            try
            {
                aMemberSetupDetails.CompanyId = Convert.ToInt32(hfCompanyId.Value.Trim());
            }
            catch (Exception)
            {
                aMemberSetupDetails.CompanyId = null;
                //throw;
            }
            try
            {
                aMemberSetupDetails.MeetingMemberTypeId = Convert.ToInt32(hfMeetingMemberTypeId.Value.Trim());
            }
            catch (Exception)
            {
                aMemberSetupDetails.MeetingMemberTypeId = null;
                //throw;
            }
            aMemberSetupDetails.Name = lbl_Name.Text.ToString();
            aMemberSetupDetails.Address = lbl_Address.Text.ToString();
            aMemberSetupDetails.Email = lbl_Email.Text.Trim().ToString();
            aMemberSetupDetails.MobileNo = (lbl_Phone.Text);
            try
            {
                aMemberSetupDetails.MembershipDate = Convert.ToDateTime(lbl_Date.Text);
            }
            catch (Exception)
            {
                aMemberSetupDetails.MembershipDate = null;
                //throw;
            }

            try
            {
                aMemberSetupDetails.JoiningDate = Convert.ToDateTime(lbl_JoiningDate.Text);
            }
            catch (Exception)
            {
                aMemberSetupDetails.JoiningDate = null;
                //throw;
            }
            aMemberSetupDetails.Note = lbl_Note.Text.ToString();
            aMemberSetupDetailsesList.Add(aMemberSetupDetails);
        }
        return aMemberSetupDetailsesList;

    }

    private List<MemberDetailsInfoNewDAO> PrepareDataForUpdateDetails()
    {
        MemberDetailsInfoNewDAO aMemberSetupDetails;
        List<MemberDetailsInfoNewDAO> aMemberSetupDetailsesList = new List<MemberDetailsInfoNewDAO>();

        for (int i = 0; i < gv_Children.Rows.Count; i++)
        {
         
            Label lbl_Name = (Label)gv_Children.Rows[i].FindControl("lbl_Name");
            Label lbl_Address = (Label)gv_Children.Rows[i].FindControl("lbl_Address");
            Label lbl_Email =
                (Label)gv_Children.Rows[i].FindControl("lbl_Email");
            Label lbl_Phone = (Label)gv_Children.Rows[i].FindControl("lbl_Phone");
            Label lbl_Date = (Label)gv_Children.Rows[i].FindControl("lbl_Date");
            Label lbl_Note =
                (Label)gv_Children.Rows[i].FindControl("lbl_Note");
            aMemberSetupDetails = new MemberDetailsInfoNewDAO();
            aMemberSetupDetails.MemberSetupMasterID = Convert.ToInt32(MemberMasterdHiddenField.Value);
            aMemberSetupDetails.MemberType = txtMemberType.Text.ToString();
            aMemberSetupDetails.Name = lbl_Name.Text.ToString();
            aMemberSetupDetails.Address = lbl_Address.Text.ToString();
            aMemberSetupDetails.Email = lbl_Email.Text.Trim().ToString();
            aMemberSetupDetails.MobileNo =  (lbl_Phone.Text);
            try
            {
                aMemberSetupDetails.MembershipDate = Convert.ToDateTime(lbl_Date.Text);
            }
            catch (Exception)
            {
                aMemberSetupDetails.MembershipDate = null;
                //throw;
            }
            aMemberSetupDetails.Note = lbl_Note.Text.ToString();
            aMemberSetupDetailsesList.Add(aMemberSetupDetails);
        }
        return aMemberSetupDetailsesList;

    }

    private Int32 SaveChangesforPettycashDetails(Int32 Val)
    {
        Int32 retVal;
        try
        {
            retVal = aMinors.SaveDataForDetails(PrepareDataForSaveDetails(Val));
        }
        
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }

    private Int32 SaveChangesMaster()
    {
        Int32 retVal;
        try
        {
            retVal = aMinors.SaveDataForMemberSetupMaster(PrepareDataForSaveMaster());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }


    private Int32 SaveMasterForDelete(int Id)
    {
        Int32 retVal;
        try
        {
            retVal = aMinors.SaveDataForMemberSetupMasterDelete(PrepareDataForSaveMasterDelete(), Id);

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }


    private bool SaveChangesUpdateMaster()
    {
        bool retVal;
        try
        {
            retVal = aMinors.UpdateDataForMemberSetupDetails(PrepareDataForUpdateMaster(), MemberMasterdHiddenField.Value);

        }
        catch (Exception ex)
        {
            retVal = false;
            throw ex;
        }

        return retVal;
    }

    //private bool UpdateChangesMaster()
    //{
    //    bool retVal;
    //    try
    //    {
    //        retVal = aMinors.UpdateDetailsDataBll(PrepareDataForUpdateDetails());

    //    }
    //    catch (Exception ex)
    //    {
    //        retVal = false;
    //        throw ex;
    //    }

    //    return retVal;
    //}


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MemberInformationView.aspx");
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
