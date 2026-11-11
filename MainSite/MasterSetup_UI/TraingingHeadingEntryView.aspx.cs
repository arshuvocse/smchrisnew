using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_TraingingHeadingEntryView : System.Web.UI.Page
{
    TraingingHeadingEntryDAL aVaencyEntryDaL = new TraingingHeadingEntryDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
      UserPersmissionValidation();
            LoadRegionInformation();
        }

        try
        {

            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }
    }


    private bool CheckAchievementsAllocateOrNot(int MainID)
    {

        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblTrainingSetupTopics
                      where t.TraingingHeadingId == MainID && (t.IsDelete == false || t.IsDelete == null)
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
    }
    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }
    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();

        DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
        for (int i = 0; i < userdata.Rows.Count; i++)
        {
            for (int j = 0; j < lchk_Company.Items.Count; j++)
            {
                if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                {
                    lchk_Company.Items[j].Selected = true;


                }
            }
        }
    }

    private void LoadRegionInformation()
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacanceyEntryformationParam();

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            this.loadGridView.ShowFooter = true;
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                int areaId = Convert.ToInt32(dataKey[0].ToString());

                if (!CheckAchievementsAllocateOrNot(Convert.ToInt32(areaId)))
                {
                   
                    Session["Status"] = "Edit";
                    Session["VacancyCirculationId"] = "";
                    Session["VacancyCirculationId"] = areaId;
                    Response.Redirect("TraingingHeadingEntry.aspx");
                }
                else
                {
                    aShowMessage.ShowMessageBox("Uaable to edit or delete!!! ", this);
                }
            }

           
        }

         if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string VacancyCirculationId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["VacancyCirculationId"] = "";
            Session["VacancyCirculationId"] = VacancyCirculationId;
            Session["Status"] = "View";
            Response.Redirect("TraingingHeadingEntry.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            int VacancyCirculationId = Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString());




            if (!CheckAchievementsAllocateOrNot(Convert.ToInt32(VacancyCirculationId)))
                {
           
                Session["VacancyCirculationId"] = "";
                Session["VacancyCirculationId"] = VacancyCirculationId;
                Session["Status"] = "Delete";
                Response.Redirect("TraingingHeadingEntry.aspx");    
            }
            else
            {
                aShowMessage.ShowMessageBox("Uaable to edit or delete!!! ", this);
            }
            
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var areaId = dataKey[0].ToString();

        //        if (!CheckAreaAllocateOrNot(areaId))
        //        {
        //            if (aInformationDal.DeleteAreaInfoById(areaId))
        //            {
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //                LoadRegionInformation();
        //            }
        //        }
        //        else
        //        {
                   
        //         //   showMessageBox("Cann't delete because it contains a Region."); 
        //            LoadRegionInformation();
        //            aShowMessage.ShowMessageBox("Can not be deleted because this is used in Job Location.", this);
        //          //aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //            //
        //        }
        //    }
        //}
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("TraingingHeadingEntry.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        const int manuId = 12;
        DataTable gridPermission = aOperationCredentials.MnageUserOperationOnGridView(Session["UserId"].ToString(), manuId);
        const int rowIndex = 0;

        bool edit = false;
        bool delete = false;

        if (gridPermission.Rows.Count > 0)
        {
            edit = gridPermission.Rows[rowIndex].Field<bool>("Edit");
            delete = gridPermission.Rows[rowIndex].Field<bool>("Delete");
        }

        if (edit)
        {
            loadGridView.Columns[12].Visible = true;
        }

        if (delete)
        {
            loadGridView.Columns[13].Visible = true;
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}