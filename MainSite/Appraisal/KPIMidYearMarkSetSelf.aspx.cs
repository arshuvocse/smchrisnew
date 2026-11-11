using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Report_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Appraisal_KPIMidYearMarkSetSelf : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private HRIS_SMCEntities _hrEntities = new HRIS_SMCEntities();
    private KPIMIDAppraisalFunctionalPartDAL _appPartA = new KPIMIDAppraisalFunctionalPartDAL();
    private KPIMIDAppraislDashboardDAL _appDashboard = new KPIMIDAppraislDashboardDAL();
    ShowMessage aShowMessage = new ShowMessage();
    SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        
        {
            LoadInitialDDL();
            //DataTable dt = _appDashboard.GetAppraisalDashboard(ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue.ToString()));
            //gv_AppraisalBoard.DataSource = dt;
            //gv_AppraisalBoard.DataBind();




            ddlCompany.SelectedValue = Session["CompanyId"].ToString();
            ddlCompany_OnSelectedIndexChanged(null, null);

            FinancialYearDropDownList_OnSelectedIndexChanged(null, null);

      //      if (Session["radStatus"] != null)
      //      {
      //          radOp.SelectedValue = Session["radStatus"].ToString();
      //          FinancialYearDropDownList.SelectedValue = Session["FinStatus"].ToString();
      //          AppraisalOwn.DataSource = null;
      //          AppraisalOwn.DataBind();
      //          gv_AppraisalBoard.DataSource = null;
      //          gv_AppraisalBoard.DataBind();
      //          Session["radStatus"] = radOp.SelectedValue;
      //          Session["FinStatus"] = FinancialYearDropDownList.SelectedValue;
      //          if (radOp.SelectedValue == "Own")
      //          {
      //              if (FinancialYearDropDownList.SelectedValue != "")
      //              {


      //                  DataTable dt = _appDashboard.GetAppraisalDashboardOwn333fin(Convert.ToInt32(Session["EmpInfoId"]),
      //                      Convert.ToInt32(FinancialYearDropDownList.SelectedValue), "  AND tblKPIMIDAppraisalMasterAppLog.Version=CELog.MaxVer  ", FinancialYearDropDownList.SelectedItem.Text);
      //                  if (dt.Rows.Count > 0)
      //                  {
      //                      AppraisalOwn.DataSource = dt;
      //                      AppraisalOwn.DataBind();
      //                  }
      //                  else
      //                  {


      //                      DataTable dt44 = _appDashboard.GetAppraisalDashboardOwn333fin(Convert.ToInt32(Session["EmpInfoId"]),
      //                      Convert.ToInt32(FinancialYearDropDownList.SelectedValue), "  and aax.ActionStatus='Approved'   ", FinancialYearDropDownList.SelectedItem.Text);


      //                      try
      //                      {
      //                          AppraisalOwn.DataSource = dt44;
      //                          AppraisalOwn.DataBind();
      //                      }
      //                      catch (Exception)
      //                      {
      //                           AppraisalOwn.DataSource = null;
      //                        AppraisalOwn.DataBind();
      //                      }
                        
      //                  }




      //                      DataTable dt2 = _appDashboard.GetAppraisalByPermission2(FinancialYearDropDownList.SelectedItem.Text, Session["EmpInfoId"].ToString());

      //                      for (int i = 0; i < AppraisalOwn.Rows.Count; i++)
      //                      {


      //                          CheckBox MidYearStatus = (CheckBox)AppraisalOwn.Rows[i].FindControl("IsMidYearStatus");


      //                          bool IsMidYearStatu = false;
      //                          try
      //                          {
      //                              IsMidYearStatu = Convert.ToBoolean(dt.Rows[0]["IsMidYearStatus"].ToString());
      //                              MidYearStatus.Checked = IsMidYearStatu;

      //                          }
      //                          catch (Exception)
      //                          {
      //                              IsMidYearStatu = false;
      //                              MidYearStatus.Checked = IsMidYearStatu;

      //                              //throw;
      //                          }
      //                          HiddenField id_appraisalMaster = (HiddenField)AppraisalOwn.Rows[i].FindControl("id_appraisalMaster");


                                
        

       

      //                          DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
      //                          string EmpID = "";
      //                          string Actions = "";
      //                          if (dt3.Rows.Count>0)
      //                          {
      //                              EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
      //                              Actions = dt3.Rows[0]["ActionStatus"].ToString();
      //                          }
      //                          //
      //                          if ( EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
      //                          {
      //                              AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
      //                               AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
      //                          }

      //                          //

      //                          if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
      //                          {
      //                             AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
      //                             AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;

      //                          }


      //                          if (Actions.ToString() == "Verified" || EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
      //                          {
      //                              AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
      //                              AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;

      //                          }
      //                          if (EmpID == "0")
      //                          {
      //                              AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
      //                               AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
      //                          }


      //                          if (dt3.Rows.Count == 0 && dt2.Rows.Count > 0)
      //                          {
      //                           AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = true;
      //                             AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = true;
      //                          }

      //                          if (dt2.Rows.Count > 0 && Actions.ToString() == "Verified" && EmpID == Session["EmpInfoId"].ToString())
      //                          {
      //                              AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = true;
      //                              AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = true;
      //                          }

      //                          //else
      //                          //{
      //                          //    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
      //                          //    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
      //                          //}


      //                          DataTable dt345 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
      //                          string EmpIDa = "";
      //                          string Actionsa = "";

      //                          if (dt345.Rows.Count > 0)
      //                          {
      //                              EmpIDa = dt345.Rows[0]["ForEmpInfoId"].ToString();
      //                              Actionsa = dt345.Rows[0]["ActionStatus"].ToString();
      //                              LinkButton PartC = (LinkButton)AppraisalOwn.Rows[i].FindControl("PartC");
      //                              if (Actionsa == "Approved")
      //                              {
      //                               AppraisalOwn.Columns[7].Visible = true;
      //                               //  AppraisalOwn.HeaderRow.Cells[5].Visible = false;//hide grid column header
      //                                //  PartC.Visible = false;

      //                              }
      //                              else
      //                              {
      //                                 AppraisalOwn.Columns[7].Visible = false;

      //                                  //AppraisalOwn.HeaderRow.Cells[5].Visible = true;//hide grid column header
      //                                  //PartC.Visible = true;
      //                              }
      //                          }
      //                          else
      //                          {
      //                              AppraisalOwn.Columns[7].Visible = false;
      //                          }

      //KPIMIDAppraisalFunctionalPartDAL _aFincDal = new KPIMIDAppraisalFunctionalPartDAL();

      //DataTable dt2Deag = _aFincDal.GetAppraisalPermission2FinYear(Session["EmpInfoId"].ToString(), FinancialYearDropDownList.SelectedItem.Text);
      //if (dt2Deag.Rows.Count == 0)
      //                          {

      //                              Button btn_Save = (Button)AppraisalOwn.Rows[i].FindControl("btn_Save");
      //                              btn_Save.Visible = false;
      //                              try
      //                              {
      //                                  Actionsa = dt345.Rows[0]["ActionStatus"].ToString();
      //                              }
      //                              catch (Exception)
      //                              {
                                        
      //                                  //throw;
      //                              }
                                  
      //                              if (Actionsa != "Approved")
      //                              {
                                   
      //                              }


      //                          }
      //                          else
      //                          {
      //                              ((Label)AppraisalOwn.Rows[i].FindControl("lblExpireStatus")).Text = "";
      //                              ((Button)AppraisalOwn.Rows[i].FindControl("btn_Save")).Visible = true;
      //                          }


      //                          LinkButton PartAOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartAOwn");

      //                          if (PartAOwn.Text == "Not Complete")
      //                          {
      //                              PartAOwn.CssClass = "btn btn-danger btn-sm ";

      //                          }

      //                          if (PartAOwn.Text == "Complete")
      //                          {
      //                              PartAOwn.CssClass = "btn btn-success btn-sm";

      //                          }


      //                          try
      //                          {
      //                              decimal pppp = 0;
      //                              pppp = Convert.ToDecimal(PartAOwn.Text);
      //                              if (pppp >= 0)
      //                              {
      //                                  PartAOwn.CssClass = "btn btn-success btn-sm";

      //                              }

      //                          }
      //                          catch (Exception)
      //                          {

      //                          }


      //                          LinkButton PartBOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartBOwn");

      //                          if (PartBOwn.Text == "Not Complete")
      //                          {
      //                              PartBOwn.CssClass = "btn btn-danger btn-sm ";

      //                          }

      //                          if (PartBOwn.Text == "Complete")
      //                          {
      //                              PartBOwn.CssClass = "btn btn-success btn-sm";

      //                          }



      //                          try
      //                          {
      //                              decimal ppppb = 0;
      //                              ppppb = Convert.ToDecimal(PartBOwn.Text);
      //                              if (ppppb >= 0)
      //                              {
      //                                  PartBOwn.CssClass = "btn btn-success btn-sm";

      //                              }

      //                          }
      //                          catch (Exception)
      //                          {

      //                          }

      //                          try
      //                          {
      //                              LinkButton PartC = (LinkButton)AppraisalOwn.Rows[i].FindControl("PartC");


      //                              PartC.CssClass = "btn btn-success btn-sm";
      //                          }
      //                          catch (Exception)
      //                          {


      //                          }



                                

      //                          LinkButton training = (LinkButton)AppraisalOwn.Rows[i].FindControl("training");

      //                          if (training.Text == "Not Complete")
      //                          {
      //                              training.CssClass = "btn btn-danger btn-sm ";
      //                              training.Text = "Training (0)";



      //                          }

      //                          if (training.Text == "Complete")
      //                          {
      //                              training.CssClass = "btn btn-success btn-sm";

      //                              DataTable dtTra = _appPartA.GetAppraisalTraining(Convert.ToInt32(id_appraisalMaster.Value));
      //                              if (dtTra.Rows.Count > 0)
      //                              {
      //                                  training.Text = "Training (" + dtTra.Rows.Count + ")";
      //                              }

      //                          }
      //                          try
      //                          {
      //                              decimal trainingD = 0;
      //                              trainingD = Convert.ToDecimal(training.Text);
      //                              if (trainingD >= 0)
      //                              {
      //                                  training.CssClass = "btn btn-success btn-sm";

      //                              }

      //                          }
      //                          catch (Exception)
      //                          {

      //                          }


      //                      }
                      
      //              }
      //          }
      //          else
      //          {

      //                if (FinancialYearDropDownList.SelectedValue != "")
      //              {

                  
      //              DataTable DTSupp = _appDashboard.GetAppraisalDashboardSup(Convert.ToInt32(Session["EmpInfoId"]),Convert.ToInt32(FinancialYearDropDownList.SelectedValue)); 

      //              if (DTSupp.Rows.Count>0)
      //              {
      //                  gv_AppraisalBoard.DataSource = DTSupp;
      //                  gv_AppraisalBoard.DataBind();


      //                  DataTable dt2 = _appDashboard.GetAppraisalByPermission2(FinancialYearDropDownList.SelectedItem.Text, Session["EmpInfoId"].ToString());
      //                  for (int i = 0; i < gv_AppraisalBoard.Rows.Count; i++)
      //                  {


      //                      HiddenField id_appraisalMaster = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_appraisalMaster");

      //                      DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
      //                      string EmpID = "";
      //                      string Actions = "";
      //                      if (dt3.Rows.Count > 0)
      //                      {
      //                          EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
      //                          Actions = dt3.Rows[0]["ActionStatus"].ToString();
      //                      }
      //                      //
      //                      //if ( EmpID != Session["EmpInfoId"].ToString()  )
      //                      //{
      //                      //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


      //                      //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

      //                      //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;
      //                      //}

      //                      ////

      //                      //if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString())
      //                      //{
      //                      //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


      //                      //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

      //                      //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;

      //                      //}
      //                      //if (EmpID == "0")
      //                      //{
      //                      //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


      //                      //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

      //                      //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;
                                 
      //                      //}




      //                      LinkButton PartA = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("PartA");

      //                      if (PartA.Text == "Not Complete")
      //                      {
      //                          PartA.CssClass = "btn btn-danger btn-sm ";

      //                      }

      //                      if (PartA.Text == "Complete")
      //                      {
      //                          PartA.CssClass = "btn btn-success btn-sm";

      //                      }


      //                      try
      //                      {
      //                          decimal pppp = 0;
      //                          pppp = Convert.ToDecimal(PartA.Text);
      //                          if (pppp >= 0)
      //                          {
      //                              PartA.CssClass = "btn btn-success btn-sm";

      //                          }
      //                      }
      //                      catch (Exception)
      //                      {

      //                      }



      //                      LinkButton PartB = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("PartB");

      //                      if (PartB.Text == "Not Complete")
      //                      {
      //                          PartB.CssClass = "btn btn-danger btn-sm ";

      //                      }

      //                      if (PartB.Text == "Complete")
      //                      {
      //                          PartB.CssClass = "btn btn-success btn-sm";

      //                      }



      //                      try
      //                      {
      //                          decimal ppppb = 0;
      //                          ppppb = Convert.ToDecimal(PartB.Text);
      //                          if (ppppb >= 0)
      //                          {
      //                              PartB.CssClass = "btn btn-success btn-sm";

      //                          }

      //                      }
      //                      catch (Exception)
      //                      {

      //                      }




      //                      LinkButton PartC = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("PartC");

      //                      if (PartC.Text == "Not Complete")
      //                      {
      //                          PartC.CssClass = "btn btn-danger btn-sm ";

      //                      }
      //                      else
                           
      //                      {
      //                          PartC.CssClass = "btn btn-success btn-sm";
                               
      //                      }


      //                      LinkButton Training = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("Training");

      //                      if (Training.Text == "Not Complete")
      //                      {
      //                          Training.CssClass = "btn btn-danger btn-sm ";

      //                      }

      //                      if (Training.Text == "Complete")
      //                      {
      //                          Training.CssClass = "btn btn-success btn-sm";
      //                          Training.Text = "Completed";
      //                      }
                            

      //                  }
              

      //                  }

      //                 // RadioTextValue();
      //              }
      //              else
      //              {
      //                  gv_AppraisalBoard.DataSource = null;
      //                  gv_AppraisalBoard.DataBind();
      //              }
                   
      //          }
      //      }
      //     // btnSearch_OnClick(null, null);
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();

            ddlCompany.SelectedIndex = 1;

             
        }
    }

    private void LoadDiviion(int comId)
    {
       // try
        {
            List<tblDivision> divList = _hrEntities.tblDivisions.Where(a => a.CompanyId == comId).ToList();

            ddlDivision.DataSource = divList;
            ddlDivision.DataValueField = "DivisionId";
            ddlDivision.DataTextField = "DivisionName";
            ddlDivision.DataBind();
        }
        //catch (Exception)
        {
           // 
           // throw;
        }
       

    }

    private bool CheckStartEndDateExistOrNot(DateTime Start, DateTime End)
    {
        DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();

        bool status = false;
        string COMID = Session["CompanyId"].ToString();

        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(Start, End, COMID);

        if (dataTable.Rows.Count > 0)
        {
            if (COMID == "1")
            {
                FinancialYearDropDownList.SelectedValue = "20";
                // FinancialYearDropDownList.SelectedValue = dataTable.Rows[0]["FinancialYearId"].ToString();
            }
            else
            {
                FinancialYearDropDownList.SelectedValue = "19";
            }

            status = true;
        }

        return status;
    }
    EmployeeContractualReportDAL aEmployeeJobLeftEntryDAL = new EmployeeContractualReportDAL();
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       

        if (ddlCompany.SelectedValue != "")
        {
            int company = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadDiviion(company);
            aEmployeeJobLeftEntryDAL.FinYearByCompDropDown(FinancialYearDropDownList, ddlCompany.SelectedValue);


            if (DateTime.Now != null)
            {


                if (CheckStartEndDateExistOrNot(DateTime.Now, DateTime.Now) == true)
                {

                }

            }
        }
        else
        {
            FinancialYearDropDownList.Items.Clear();
        }
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
         int divId = Convert.ToInt32( ddlDivision.SelectedValue);
        LoadWing(divId);
    }

    private void LoadWing(int divId)
    {
        List<tblDivisionWing> wingList = _hrEntities.tblDivisionWings.Where(d => d.DivisionId == divId).ToList();
       
        ddlWing.DataSource = wingList;
        ddlWing.DataValueField = "DivisionWId";
        ddlWing.DataTextField = "DivisionWingName";
        ddlWing.DataBind();

    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int wingId = Convert.ToInt32(ddlWing.SelectedValue);
        LoadDepartment(wingId);
    }

    private void LoadDepartment(int wingId)
    {
        List<tblDepartment> dptList = _hrEntities.tblDepartments.Where(w => w.DivisionWId == wingId).ToList();
        ddlDepartment.DataSource = dptList;
        ddlDepartment.DataValueField = "DepartmentId";
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataBind();

    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int dptId = Convert.ToInt32(ddlDepartment.SelectedValue);
        LoadSection(dptId);
    }

    private void LoadSection(int dpt)
    {

        DataTable sectionList = _appDashboard.LoadSectionDDl(dpt);
        
        ddlSection.DataSource = sectionList;
        ddlSection.DataValueField = "SectionId";
        ddlSection.DataTextField = "SectionName";
        ddlSection.DataBind();

    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int dptId = Convert.ToInt32(ddlSection.SelectedValue);
        LoadSubsection(dptId);

    }

    private void LoadSubsection(int id)
    {

        DataTable sectionList = _appDashboard.LoadSubsection(id);
        ddlSubsection.DataSource = sectionList;
        ddlSubsection.DataValueField = "SubSectionId";
        ddlSubsection.DataTextField = "SubSectionName";
        ddlSubsection.DataBind();
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        DataTable dt = _appDashboard.GetAppraisalDashboard(ddlCompany.SelectedValue==""?0:Convert.ToInt32(ddlCompany.SelectedValue.ToString()));
        gv_AppraisalBoard.DataSource = dt;
        gv_AppraisalBoard.DataBind();
    }

    protected void PartA_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("selfMaster");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        int masterID = int.Parse(mastrValue); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
        int empInfoId = int.Parse(emoIdValue);
        int selfMaster = int.Parse(hfselfMaster.Value);

        DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
        DataTable dtw = _appPartA.GetAppraisalSelfDetails(Convert.ToInt32(selfMaster));
        DataTable dtw2 = _appPartA.GetAppraisalfDetailsFromSup(Convert.ToInt32(selfMaster));
        id_selfID.Value = selfMaster.ToString();

        if (dtw2.Rows.Count > 0)
        {
            gv_AppraisalFuncSUP.DataSource = dtw2;
            gv_AppraisalFuncSUP.DataBind();
            ViewState["KPIFUNCSUP"] = dtw2;
        }
        else
        {
            gv_AppraisalFuncSUP.DataSource = dtw;
            gv_AppraisalFuncSUP.DataBind();
            ViewState["KPIFUNCSUP"] = dtw;
        }

        id_mastetID.Value = masterID.ToString();
        id_Empid.Value = empInfoId.ToString();
        CalculateBFuncSUP();
        GetEmpInfoByEmpIDFuncSUP(empInfoId);
        CheckImmmiedietSuperVisor();
        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalFunctional.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value  + "' ,'_blank');", true);
        mpFunctionalSup.Show();




        DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
        string EmpID = "";
        string Actions = "";
        if (dt3r.Rows.Count > 0)
        {
            EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
            Actions = dt3r.Rows[0]["ActionStatus"].ToString();
            if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
            {
                btnAppraisalFuncSUPSave.Visible = false;
            }
            


            if (EmpID == "0")
            {
                btnAppraisalFuncSUPSave.Visible = false;
            }
            
        }

    }


    public void CheckImmmiedietSuperVisor()
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + id_Empid.Value + "'");
        if (Session["EmpInfoId"].ToString() != id_Empid.Value)
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
                // btnAppraisalFuncSUPSave.Visible = false;
                for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
                {
                    TextBox tbKpi = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtKpi");
                    TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
                    TextBox txtWeightPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeightPer");
                    TextBox txtTarget = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
                    TextBox selfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
                    TextBox txtTargetPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTargetPer");
                    TextBox txtDeadLine = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtDeadLine");
                    TextBox txtMidStatus = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
                    TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");
                    TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");

                    tbKpi.ReadOnly = true;
                    txtWeight.ReadOnly = true;
                    txtWeightPer.ReadOnly = true;
                    txtTarget.ReadOnly = true;
                    selfMark.ReadOnly = true;
                    txtTargetPer.ReadOnly = true;
                    txtDeadLine.ReadOnly = true;
                    txtMidStatus.ReadOnly = true;
                    txtResult.ReadOnly = true;
                    txtMark.ReadOnly = true;

                }
            }
        }
    }


    private void CalculateBFuncSUP()
    {
        decimal MarkTotal = 0;
        decimal WeightTotal = 0;
        decimal TargetTotal = 0;
        decimal ResultTotal = 0;
        decimal tselfMark = 0;


        try
        {
            if (gv_AppraisalFuncSUP.Rows.Count > 0)
            {
                for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
                {
                    TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");
                    TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
                    TextBox txtTarget = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
                    TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");
                    TextBox txtselfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");





                    if (txtMark.Text == "")
                    {
                        MarkTotal = MarkTotal + 0;
                    }
                    else
                    {
                        MarkTotal = MarkTotal + Convert.ToDecimal(txtMark.Text.ToString());
                    }

                    if (txtWeight.Text == "")
                    {
                        WeightTotal = WeightTotal + 0;
                    }
                    else
                    {
                        WeightTotal = WeightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                    }


                    if (txtTarget.Text == "")
                    {
                        TargetTotal = TargetTotal + 0;
                    }
                    else
                    {
                        TargetTotal = TargetTotal + Convert.ToDecimal(txtTarget.Text.ToString());
                    }



                    if (txtResult.Text == "")
                    {
                        ResultTotal = ResultTotal + 0;
                    }
                    else
                    {
                        try { ResultTotal = ResultTotal + Convert.ToDecimal(txtResult.Text.ToString()); } catch { }
                    }



                    if (txtselfMark.Text == "")
                    {
                        tselfMark = tselfMark + 0;
                    }
                    else
                    {
                        tselfMark = tselfMark + Convert.ToDecimal(txtselfMark.Text.ToString());
                    }

                }



                Label tst2 = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalMark");
                tst2.Text = MarkTotal.ToString();


                Label lblTotalWeight = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalWeight");
                lblTotalWeight.Text = WeightTotal.ToString();




                Label lbltarget = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lbltarget");
                lbltarget.Text = TargetTotal.ToString();


                Label lblresultend = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblresultend");
                lblresultend.Text = ResultTotal.ToString();


                Label lblselfMark = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblselfMark");
                lblselfMark.Text = tselfMark.ToString();

            }
        }
        catch { }
    }


    protected void txtWeight_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thePer = (weightNum / (75.0 / 100.0));
        txtweightper.Text = thePer.ToString("#,##0.00");
        CalculateTotal();




    }
    protected void txtWeightPer_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thenum = (75.00 / 100.00) * weightper;
        txtweight.Text = thenum.ToString("#,##0.00");
        CalculateTotal();
    }


    protected void txtMidStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList ddlMidStatus = (DropDownList)gv_AppraisalFunc.Rows[rowIndex].FindControl("txtMidStatus");
        TextBox txtselfMark = (TextBox)gv_AppraisalFunc.Rows[rowIndex].FindControl("txtselfMark");
        txtselfMark.ReadOnly = false;
        if (ddlMidStatus.SelectedValue == "Not Started" || ddlMidStatus.SelectedValue == "Not Relevant")
        {
            txtselfMark.ReadOnly = true;
            txtselfMark.Text = "";
        }

        DropDownList ddl = (DropDownList)sender;
        ApplyMidYearStatusColor(ddl);


        CalculateB();
    }
    //Saad
    protected void gv_AppraisalFunc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        DropDownList ddlStatus = (DropDownList)e.Row.FindControl("txtMidStatus");
        if (ddlStatus == null) return;

        string midYearStatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MidYearStatus"));
        midYearStatus = (midYearStatus == null) ? "" : midYearStatus.Trim();

        if (midYearStatus != "")
        {
            ListItem li = ddlStatus.Items.FindByValue(midYearStatus);

            if (li == null)
                li = ddlStatus.Items.FindByText(midYearStatus);

            // DB text -> dropdown value mapping
            if (li == null)
            {
                if (string.Equals(midYearStatus, "On Track", StringComparison.OrdinalIgnoreCase))
                    li = ddlStatus.Items.FindByValue("Ongoing");
                else if (string.Equals(midYearStatus, "Behind", StringComparison.OrdinalIgnoreCase))
                    li = ddlStatus.Items.FindByValue("Not Completed");
            }

            if (li != null)
                ddlStatus.SelectedValue = li.Value;
        }
        // ✅ Always apply color
        ApplyMidYearStatusColor(ddlStatus);
    }

    private void ApplyMidYearStatusColor(DropDownList ddl)
    {
        if (ddl == null) return;

        string v = (ddl.SelectedValue == null) ? "" : ddl.SelectedValue.Trim();
        string t = (ddl.SelectedItem == null || ddl.SelectedItem.Text == null) ? "" : ddl.SelectedItem.Text.Trim();

        // Remove previous status-* classes
        string cls = ddl.CssClass ?? "";
        cls = Regex.Replace(cls, @"\bstatus-(red|green|yellow)\b", "").Trim();

        // RED
        if (v == "Not Started" || v == "Not Completed" || t == "Not Started" || t == "Behind" || t == "Not Completed")
            cls = (cls + " status-red").Trim();

        // GREEN
        else if (v == "Ongoing" || v == "Completed" || t == "On Track" || t == "Ongoing" || t == "Completed")
            cls = (cls + " status-green").Trim();

        // YELLOW
        else if (v == "Not Relevant" || t == "Not Relevant")
            cls = (cls + " status-yellow").Trim();

        ddl.CssClass = cls;
    }
    protected void PartAOwn_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("selfMaster");
       

         

        mpe_1.Show();


        int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
        int empInfoId = int.Parse(empID.Value);
        int selfMaster = int.Parse(hfselfMaster.Value);

        DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
        DataTable dtw = _appPartA.GetAppraisalSelfDetailsNew(Convert.ToInt32(selfMaster));
        DataTable dtw2 = _appPartA.GetAppraisalfDetailsFromSup(Convert.ToInt32(selfMaster));
        id_selfID.Value = selfMaster.ToString();

        if (dtw2.Rows.Count > 0)
        {
            gv_AppraisalFunc.DataSource = dtw2;
            gv_AppraisalFunc.DataBind();
            ViewState["KPIFUNC"] = dtw2;
        }
        else
        {
            gv_AppraisalFunc.DataSource = dtw;
            gv_AppraisalFunc.DataBind();
            ViewState["KPIFUNC"] = dtw;
        }

        id_mastetID.Value = masterID.ToString();
        id_Empid.Value = empInfoId.ToString();
        
        GetEmpInfoByEmpID(empInfoId);
        DataTable dt345 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
        string Actionsa = "";
        try
        {
              Actionsa = dt345.Rows[0]["ActionStatus"].ToString();
        }
        catch (Exception)
        {
            
            //throw;
        }
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {

                DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                TextBox txtselfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
            LinkButton lbMidStatus = (LinkButton)gv_AppraisalFunc.Rows[i].FindControl("lbMidStatus");
                for (int j = 0; j < txtMidStatus.Items.Count; j++)
                {
                    if (txtMidStatus.Items[j].Text == gv_AppraisalFunc.DataKeys[i][0].ToString())
                    {
                        txtMidStatus.SelectedIndex = j;
                    }
                }
            if (txtMidStatus.SelectedValue == "Not Started" || txtMidStatus.SelectedValue == "Not Relevant")
            {
                txtselfMark.ReadOnly = true;
                txtselfMark.Text = "";
            }
            //if (Actionsa == "Approved")
            //{
            //    txtMidStatus.Enabled = false;
            //    lbMidStatus.Visible = false;
            //}
            //else
            {
                    lbMidStatus.Visible = true;

                    txtMidStatus.Enabled = true;
                }
            }
         CalculateB();




        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalFunctionalSelfMark.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value + "' ,'_blank');", true);
        btnFunctionalSave.Visible = true;

        DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
         string EmpID = "";
         string Actions = "";
         if (dt3.Rows.Count > 0)
         {
             EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
             Actions = dt3.Rows[0]["ActionStatus"].ToString();
             if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
             {
                 btnFunctionalSave.Visible = false;
             }


             if (EmpID == "0")
             {
                 btnFunctionalSave.Visible = false;
             }
         }
    }
    private void CalculateB()
    {
        try
        {
            decimal weightTotal = 0;
            decimal markTotal = 0;

            if (gv_AppraisalFunc.Rows.Count > 0)
            {
                for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
                {

                    TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
                    //    TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
                    TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");




                    if (txtWeight.Text == "")
                    {
                        weightTotal = weightTotal + 0;
                    }
                    else
                    {
                        weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                    }
                    if (txtMark.Text == "")
                    {
                        markTotal = markTotal + 0;
                    }
                    else
                    {
                        markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                    }
                }



                Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
                tst2.Text = markTotal.ToString();

                Label tst1 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");
                tst1.Text = weightTotal.ToString();
            }
        }
        catch
        {

        }

    }
    private void GetEmpInfoByEmpID(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            { 

                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }

    private void GetEmpInfoByEmpIDTrainSup(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceTrainingSUP.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdTrainingSUP.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameTrainingSUP.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelTrainingSUP.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelTrainingSUP.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelTrainingSUP.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelTrainingSUP.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelTrainingSUP.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }

    private void GetEmpInfoByEmpIDFinalStatus(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceFinalStatus.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdFinalStatus.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameFinalStatus.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelFinalStatus.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelFinalStatus.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelFinalStatus.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelFinalStatus.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelFinalStatus.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }



    private void GetEmpInfoByEmpIDPartBSUP(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceBSup.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdBSup.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameBSup.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelBSup.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelBSup.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelBSup.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelBSup.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelBSup.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }

    private void GetEmpInfoByEmpIDFuncSUP(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceFuncSUP.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdFuncSUP.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameFuncSUP.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelFuncSUP.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelFuncSUP.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelFuncSUP.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelFuncSUP.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelFuncSUP.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }


    private void GetEmpInfoByEmpIDBehavioral(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceBehavioral.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdBehavioral.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameBehavioral.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelBehavioral.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelBehavioral.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelBehavioral.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelBehavioral.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelBehavioral.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }



    private void GetEmpInfoByEmpIDTraining(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceTraining.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdTraining.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameTraining.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelTraining.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelTraining.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelTraining.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelTraining.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelTraining.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }


    protected void PartB_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("selfMaster");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        
         
       // Response.Redirect("AppraisalPartB.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value + "");




        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalPartB.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + hfselfMaster.Value + "' ,'_blank');", true);




        int masterID = int.Parse(mastrValue); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
        int empInfoId = int.Parse(emoIdValue);
        id_Empid.Value = empInfoId.ToString();
        int selfMaster = int.Parse(hfselfMaster.Value);
        id_selfID.Value = selfMaster.ToString();
        DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
        id_mastetID.Value = masterID.ToString();
        //DataTable dt3 = _appPartA.GetAppraisalSelfB(Convert.ToInt32(selfMaster));
        DataTable dt3 = _appPartA.GetAppraisalPartB(Convert.ToInt32(masterID));

        ViewState["PARTB"] = dt3;
        gv_AppraisalPartBSUP.DataSource = dt3;
        gv_AppraisalPartBSUP.DataBind();
        CalculateBPartBSUP();

        CheckImmmiedietSuperVisorPartBSUP(empInfoId.ToString());
        GetEmpInfoByEmpIDPartBSUP(empInfoId);
         MPBSup.Show();

         DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
         string EmpID = "";
         string Actions = "";
         if (dt3r.Rows.Count > 0)
         {
             EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
             Actions = dt3r.Rows[0]["ActionStatus"].ToString();
             if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
             {
                 btnPartBSUPSave.Visible = false;
             }


             if (EmpID == "0")
             {
                 btnPartBSUPSave.Visible = false;
             }
         }
    }

    private void CalculateBPartBSUP()
    {
        decimal TSetScore = 0;
        decimal TSelfScore = 0;
        decimal TSupervisorScore = 0;

        if (gv_AppraisalPartBSUP.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
            {
                Label SetScore = (Label)gv_AppraisalPartBSUP.Rows[i].FindControl("SetScore");
                TextBox SelfScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SelfScore");
                TextBox SupervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");




                if (SetScore.Text == "")
                {
                    TSetScore = TSetScore + 0;
                }
                else
                {
                    TSetScore = TSetScore + Convert.ToDecimal(SetScore.Text.ToString());
                }

                if (SelfScore.Text == "")
                {
                    TSelfScore = TSelfScore + 0;
                }
                else
                {
                    TSelfScore = TSelfScore + Convert.ToDecimal(SelfScore.Text.ToString());
                }



                if (SupervisorScore.Text == "")
                {
                    TSupervisorScore = TSupervisorScore + 0;
                }
                else
                {
                    TSupervisorScore = TSupervisorScore + Convert.ToDecimal(SupervisorScore.Text.ToString());
                }

            }



            Label ddllblTotalSetScore = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("ddllblTotalSetScore");
            ddllblTotalSetScore.Text = TSetScore.ToString();



            Label lblselfscrore = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("lblselfscrore");
            lblselfscrore.Text = TSelfScore.ToString();


            Label lblTotalMark = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("lblTotalMark");
            lblTotalMark.Text = TSupervisorScore.ToString();
        }
    }

    private void CheckImmmiedietSuperVisorPartBSUP(string EmpID)
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + EmpID + "'");
        if (Session["EmpInfoId"].ToString() != id_Empid.Value)
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString().ToString())
            {
               // btnPartBSUPSave.Visible = false;
                for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
                {
                    TextBox txtSkillInfo = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SkillInfo");
                    TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupportingEmp");
                    TextBox txtScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("Weight");
                    TextBox txtSelfScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SelfScore");
                    TextBox supervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");
                    txtSkillInfo.ReadOnly = true;
                    txtSupportingEmp.ReadOnly = true;
                    txtScore.ReadOnly = true;
                    txtSelfScore.ReadOnly = true;
                    supervisorScore.ReadOnly = true;
                }
            }
        }
    }

    protected void PartBOwn_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("selfMaster");


        string mastrValue = mastrId.Value;


        string emoIdValue = empID.Value;


        if (mastrValue == "0" || mastrValue == "")
        {
            AlertMessageBoxShow("Please Compleate The Functional Part First");
        }
        else
        {
            MPBehavioral.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);

            int selfMaster = int.Parse(hfselfMaster.Value);
            id_selfID.Value = selfMaster.ToString();
            DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
            id_mastetID.Value = masterID.ToString();
            DataTable dt33 = _appPartA.GetAppraisalPartB(masterID);
            DataTable dt3 = _appPartA.GetAppraisalSelfB(Convert.ToInt32(selfMaster));
            ViewState["PARTB"] = dt3;


            if (dt33.Rows.Count > 0)
            {
                gv_AppraisalPartB.DataSource = dt33;
                gv_AppraisalPartB.DataBind();
                CalculateBehiber();
                totalBehiberSelf();
            }
            else
            {
                gv_AppraisalPartB.DataSource = dt3;
                gv_AppraisalPartB.DataBind();
                CalculateBehiber();
            }

            GetEmpInfoByEmpIDBehavioral(empInfoId);



            btnBehave.Visible = true;
            DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";
            if (dt3r.Rows.Count > 0)
            {
                EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3r.Rows[0]["ActionStatus"].ToString();
                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnBehave.Visible = false;
                }


                if (EmpID == "0")
                {
                    btnBehave.Visible = false;
                }
            }
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalPartBOwn.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value  +"' ,'_blank');", true);
        }
        

    }

    private void totalBehiberSelf()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }




            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMarkSelf");
            tst2.Text = weightTotal.ToString();
        }
    }

    private void CalculateBehiber()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                Label txtWeight = (Label)gv_AppraisalPartB.Rows[i].FindControl("SetScore");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }




            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("ddllblTotalSetScore");
            tst2.Text = weightTotal.ToString();
        }

    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void Training_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");

        ViewState["TrainingPart"] = null;
        gv_AppraisalTrainingSUP.DataSource = null;
        gv_AppraisalTrainingSUP.DataBind();
        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete the Functional Area");
        }
        else
        {


            int masterID = int.Parse(mastrValue); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(emoIdValue);
            id_mastetID.Value = masterID.ToString();
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalTraining.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "' ,'_blank');", true);



            DataTable dt = _appPartA.GetAppraisalTraining(masterID);
            if (dt.Rows.Count > 0)
            {
                ViewState["TrainingPart"] = dt;
                gv_AppraisalTrainingSUP.DataSource = dt;
                gv_AppraisalTrainingSUP.DataBind();
                for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                {
                    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                    txt_BranchCountry.SelectedValue = dt.Rows[i]["Quater"].ToString();
                }
            }
            CheckImmmiedietSuperVisorTrainingPart(empInfoId.ToString());
            GetEmpInfoByEmpIDTrainSup(Convert.ToInt32(empInfoId.ToString()));
            MPTrainingSUP.Show();

            DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";
            if (dt3.Rows.Count > 0)
            {
                EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3.Rows[0]["ActionStatus"].ToString();
                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnTrainSaveSUP.Visible = false;

                    for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                    {

                        DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                        txt_BranchCountry.Enabled = false;

                        TextBox TrainingNeeds = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                        TrainingNeeds.ReadOnly = true;
                    }
                }


                if (EmpID == "0")
                {
                    btnTrainSaveSUP.Visible = false;
                    for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                    {

                        TextBox TrainingNeeds = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                        TrainingNeeds.ReadOnly = true;


                        DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                        txt_BranchCountry.Enabled = false;
                    }
                }
            }
           
        }
    }

    private void CheckImmmiedietSuperVisorTrainingPart(string empInfoId)
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId + "'");
        if (Session["EmpInfoId"].ToString() != empInfoId)
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != empInfoId.ToString())
            {
                //btnTrainSaveSUP.Visible = false;
                for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                {
                    TextBox txtSkillInfo = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                    TextBox txtSupportingEmp = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingStart");
                    TextBox txtScore = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingEnd");
                    DropDownList ddlQuater = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");

                  
                    txtSkillInfo.ReadOnly = true;
                    //txtSupportingEmp.ReadOnly = true;
                    //txtScore.ReadOnly = true;
                    ddlQuater.Enabled = false;



                }
            }
        }
    }
    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
            AddNewDocGrid_List();

        }
    }

    private bool docVali()
    {
        lblMsg.Text = "";
        if (hfDocFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please click Document Upload Button", this);

            return false;
        }
        if (txtSummaryNote.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);
            lblMsg.Text = "<b>" + hfDocFileName.Value + "</b> has been uploaded.";
            return false;
        }
        return true;

    }

    private void AddNewDocGrid_List()
    {
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DocGrid_List"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;
                //drCurrentRow["DocumentLink"] =  @"file:///D:/UploadMeetingDocument/"+ hfDocFile.Value;
                drCurrentRow["FileName"] = hfDocFileName.Value;




                drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();

                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["DocGrid_List"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_DocumentUpload.DataSource = dtCurrentTable;
                gv_DocumentUpload.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentNote", typeof(string)));
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));


            dr = dt.NewRow();

            dr["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;

            //  dr["DocumentLink"] = @"file:///D:/UploadMeetingDocument/3eec2898121c4467be57981c13852a9e.png"; //@"file:///D:/UploadMeetingDocument/" + hfDocFile.Value;
            dr["FileName"] = hfDocFileName.Value;


            dr["DocumentNote"] = txtSummaryNote.Text.Trim();
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["DocGrid_List"] = dt;

            //Bind the Gridview   
            gv_DocumentUpload.DataSource = dt;
            gv_DocumentUpload.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetDocGrid_List();


        txtSummaryNote.Text = string.Empty;
        // HyperLink2.Text = "No File Uploaded";
        HyperLink2.NavigateUrl = "";
        hfDocFile.Value = "";
    }
    private void SetDocGrid_List()
    {
        int rowIndex = 0;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLink");
                    HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfFileName");
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");


                    if (i < dt.Rows.Count - 1)
                    {
                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();

                    }

                    rowIndex++;
                }
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

    protected void TrainingOwn_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete the Functional Area");
        }
        else
        {
            MPTraining.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);
            id_mastetID.Value = masterID.ToString();

            DataTable dt = _appPartA.GetAppraisalTraining(masterID);
            if (dt.Rows.Count > 0)
            {
                ViewState["TrainingPart"] = dt;
                gv_AppraisalTraining.DataSource = dt;
                gv_AppraisalTraining.DataBind();
                for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                {
                    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                    txt_BranchCountry.SelectedValue = dt.Rows[i]["Quater"].ToString();
                }
            }
            else
            {
                IniTable();
            }
            GetEmpInfoByEmpIDTraining(empInfoId);


            btnTrainSave.Visible = true;
            DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";
            if (dt3.Rows.Count > 0)
            {
                EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3.Rows[0]["ActionStatus"].ToString();
                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnTrainSave.Visible = false;

                    for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                    {
                        gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 1].Visible = false;
                        gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 2].Visible = false;
                        DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                        txt_BranchCountry.Enabled = false;

                        TextBox TrainingNeeds = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                        TrainingNeeds.ReadOnly = true;
                    }
                }


                if (EmpID == "0")
                {
                    btnTrainSave.Visible = false;
                    for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                    {
                        gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 1].Visible = false;
                        gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 2].Visible = false;


                        TextBox TrainingNeeds = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                        TrainingNeeds.ReadOnly = true;


                        DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                        txt_BranchCountry.Enabled = false;
                    }
                }
            }
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalTraining.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "' ,'_blank');", true);
        }
    }


    private void IniTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("TrainingNeeds", typeof(string)));
        dt.Columns.Add(new DataColumn("quaterID", typeof(string)));
        ////  dt.Columns.Add(new DataColumn("TrainingEnd", typeof(string)));

        dr = dt.NewRow();

        dr["TrainingNeeds"] = "";
        dr["quaterID"] = "";
        //dr["TrainingEnd"] = "";

        dt.Rows.Add(dr);
        ViewState["TrainingPart"] = dt;

        gv_AppraisalTraining.DataSource = dt;
        gv_AppraisalTraining.DataBind();
    }
    protected void PartC_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");


        if (FinancialYearDropDownList.SelectedValue != "")
        {

            Session["Status"] = "Edit";


            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('KPIInformationDetailsView.aspx?EmpInfoId=" + empID.Value + "&financialYearId=" + FinancialYearDropDownList.SelectedValue + "' ,'_blank');", true);
            //Response.Redirect("KPIInformationDetailsView.aspx?EmpInfoId=" + empID.Value + "&financialYearId=" + FinancialYearDropDownList.SelectedValue + "");
        }
        else
        {
            AlertMessageBoxShow("Please Select  Financial Year !!");
        }


        //string aValue = aButton.Text;

        //string mastrValue = mastrId.Value;
        //string emoIdValue = empID.Value;
        //if (mastrValue == "0")
        //{
        //    AlertMessageBoxShow("Please Complete the Functional Area");
        //}
        //else
        //{

        //    int masterID = int.Parse(mastrValue);
        //    int empInfoId = int.Parse(emoIdValue);
        //    decimal partA = 0;
        //    decimal partB = 0;
        //    try
        //    {
        //        partA = decimal.Parse(aButton.Text);
        //        partB = decimal.Parse(bButton.Text);
            
        //    }
        //    catch (Exception)
        //    {

               
        //    }

        //    id_mastetID.Value = masterID.ToString();
        //    id_mastetID.Value = masterID.ToString();


        //    partAScore.Text = partA.ToString("F");
        //    partBScore.Text = partB.ToString("F");
        //    totalScore.Text = (partA + partB).ToString("F");
        //    decimal total = partA + partB;
        //    if (total < 61)
        //    {
        //        lblStatus.Text = "Poor";
        //    }

        //    if (total > 60 && total <= 70)
        //    {
        //        lblStatus.Text = "Average";
        //    }

        //    if (total >= 71 && total < 81)
        //    {
        //        lblStatus.Text = "Good";
        //    }
        //    if (total >= 81 && total < 91)
        //    {
        //        lblStatus.Text = "Excellent";
        //    }

        //    if (total > 90)
        //    {
        //        lblStatus.Text = "Outstanding";
        //    }



        //    MPFinalStatus.Show();
        //    if (masterID > 0)
        //    {
        //        DataTable dt = _appraisalPartBdal.GetAppraiSalFinalStatus(masterID);
        //        if (dt.Rows.Count > 0)
        //        {
        //            bool gen = Convert.ToBoolean(dt.Rows[0]["GeneralIncrement"].ToString());
        //            bool SpecialIncrement = Convert.ToBoolean(dt.Rows[0]["SpecialIncrement"].ToString());
        //            bool IsPromotion = Convert.ToBoolean(dt.Rows[0]["IsPromotion"].ToString());
        //            bool Pip = !string.IsNullOrEmpty(dt.Rows[0]["Pip"].ToString()) && Convert.ToBoolean(dt.Rows[0]["Pip"].ToString());
        //            bool Other = !string.IsNullOrEmpty(dt.Rows[0]["Other"].ToString()) && Convert.ToBoolean(dt.Rows[0]["Other"].ToString());

        //            //nothing

        //            int step = string.IsNullOrEmpty(dt.Rows[0]["SpecialStep"].ToString())
        //                ? 0
        //                : Convert.ToInt32(dt.Rows[0]["SpecialStep"].ToString());
        //            if (gen == true)
        //            {
        //                recommend.SelectedValue = "1";
        //            }
        //            if (SpecialIncrement == true)
        //            {
        //                recommend.SelectedValue = "2";
        //                txtStep.Text = step.ToString();
        //            }
        //            if (IsPromotion == true)
        //            {
        //                recommend.SelectedValue = "3";
        //            }
        //            if (Pip == true)
        //            {
        //                recommend.SelectedValue = "4";
        //            }
        //            if (gen == true && IsPromotion == true)
        //            {
        //                recommend.SelectedValue = "5";
        //            }
        //            if (Other == true)
        //            {
        //                recommend.SelectedValue = "6";
        //                txtnote.Text = dt.Rows[0]["Note"].ToString();
        //            }

        //            recommend_SelectedIndexChanged(recommend, (EventArgs)e);
        //        }
        //    }

        //    //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalFinal.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&pa=" + aButton.Text + "&pb=" + bButton.Text + "' ,'_blank');", true);
        //    CheckImmmiedietSuperVisorFinalStatus(empID.Value);


        //    GetEmpInfoByEmpIDFinalStatus(Convert.ToInt32(empID.Value));


        //    DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
        //    string EmpID = "";
        //    string Actions = "";
        //    if (dt3r.Rows.Count > 0)
        //    {
        //        EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
        //        Actions = dt3r.Rows[0]["ActionStatus"].ToString();
        //        if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
        //        {
                  
        //        }


        //        if (EmpID == "0")
        //        {
                 
        //        }
        // }
       // }
    }

    private void CheckImmmiedietSuperVisorFinalStatus(string EmpId)
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + EmpId + "'");
        if (Session["EmpInfoId"].ToString() != EmpId)
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
             //   btnFinalStatusSave.Visible = false;
                //for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
                //{
                //    TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                //    TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                //    TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Weight");
                //    TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");
                //    TextBox supervisorScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupervisorScore");
                //    txtSkillInfo.ReadOnly = true;
                //    txtSupportingEmp.ReadOnly = true;
                //    txtScore.ReadOnly = true;
                //    txtSelfScore.ReadOnly = true;
                //    supervisorScore.ReadOnly = true;
                //}
            }
        }
    }

    protected void radOp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (FinancialYearDropDownList.SelectedValue != "")
        
        {
            string selectedValue = radOp.SelectedValue;
            AppraisalOwn.DataSource = null;
            AppraisalOwn.DataBind();
            gv_AppraisalBoard.DataSource = null;
            gv_AppraisalBoard.DataBind();
            Session["radStatus"] = "";
            Session["radStatus"] = radOp.SelectedValue;


            Session["FinStatus"] = "";
            Session["FinStatus"] = FinancialYearDropDownList.SelectedValue;

            if (selectedValue == "Own")
            {
                DataTable dt = _appDashboard.GetAppraisalDashboardOwn333fin(Convert.ToInt32(Session["EmpInfoId"]),
                            Convert.ToInt32(FinancialYearDropDownList.SelectedValue), "  and fin.FinancialYearDesc='"+ FinancialYearDropDownList.SelectedItem.Text + "'  AND     (tblKPIMIDAppraisalMasterAppLog.Version=CELog.MaxVer or tblKPIMIDAppraisalMasterAppLog.Version is null)     ", FinancialYearDropDownList.SelectedItem.Text);
                        if (dt.Rows.Count > 0)
                        {
                            AppraisalOwn.DataSource = dt;
                            AppraisalOwn.DataBind();
                        }
                        else
                        {


                            DataTable dt44 = _appDashboard.GetAppraisalDashboardOwn333fin(Convert.ToInt32(Session["EmpInfoId"]),
                            Convert.ToInt32(FinancialYearDropDownList.SelectedValue), "  and finappMaster.FinancialYearDesc='" + FinancialYearDropDownList.SelectedItem.Text + "'  and fin.FinancialYearDesc='" + FinancialYearDropDownList.SelectedItem.Text + "' and aax.ActionStatus='Approved'  ", FinancialYearDropDownList.SelectedItem.Text);


                            try
                            {
                                AppraisalOwn.DataSource = dt44;
                                AppraisalOwn.DataBind();
                            }
                            catch (Exception)
                            {
                                 AppraisalOwn.DataSource = null;
                              AppraisalOwn.DataBind();
                            }
                        
                        }




                    DataTable dt2 = _appDashboard.GetAppraisalByPermission2(FinancialYearDropDownList.SelectedItem.Text, Session["EmpInfoId"].ToString());

                    for (int i = 0; i < AppraisalOwn.Rows.Count; i++)
                    {

                        HiddenField id_appraisalMaster = (HiddenField)AppraisalOwn.Rows[i].FindControl("id_appraisalMaster");
                        Label Awaiting = (Label)AppraisalOwn.Rows[i].FindControl("Awaiting");
                        Label Approval = (Label)AppraisalOwn.Rows[i].FindControl("Approval");


                        CheckBox MidYearStatus = (CheckBox)AppraisalOwn.Rows[i].FindControl("IsMidYearStatus");


                        bool IsMidYearStatu = false;
                        try
                        {
                            IsMidYearStatu = Convert.ToBoolean(dt.Rows[0]["IsMidYearStatus"].ToString());
                            MidYearStatus.Checked = IsMidYearStatu;

                        }
                        catch (Exception)
                        {
                            IsMidYearStatu = false;
                            MidYearStatus.Checked = IsMidYearStatu;

                            //throw;
                        }

                        DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
                        string EmpID = "";
                        string Actions = "";
                        if (dt3.Rows.Count > 0)
                        {
                            EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                            Actions = dt3.Rows[0]["ActionStatus"].ToString();
                        }



                        if (EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
                        }

                        //

                        if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;

                        }


                        if (Actions.ToString() == "Verified" && EmpID != Session["EmpInfoId"].ToString() && dt2.Rows.Count == 0)
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;

                        }

                        if (EmpID == "0")
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
                        }


                        if (dt3.Rows.Count == 0 && dt2.Rows.Count>0)
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = true;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = true;
                        }

                        if (dt2.Rows.Count > 0 && Actions.ToString() == "Verified" && EmpID == Session["EmpInfoId"].ToString())
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = true;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = true;
                        }

                        //else
                        //{
                        //    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                        //    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
                        //}
                        //if (Awaiting.Text.Trim() != "" && Approval.ToString() != "Approved")
                        //{
                        //    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                        //    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
                        //}

                        LinkButton PartAOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartAOwn");

                        if (PartAOwn.Text == "Not Complete")
                        {
                            PartAOwn.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartAOwn.Text == "Complete")
                        {
                            PartAOwn.CssClass = "btn btn-success btn-sm";

                        }
                        try
                        {
                            decimal pppp = 0;
                            pppp = Convert.ToDecimal(PartAOwn.Text);
                            if (pppp >= 0)
                            {
                                PartAOwn.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }


                        LinkButton PartBOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartBOwn");

                        if (PartBOwn.Text == "Not Complete")
                        {
                            PartBOwn.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartBOwn.Text == "Complete")
                        {
                            PartBOwn.CssClass = "btn btn-success btn-sm";

                        }

                        try
                        {
                            decimal ppppb = 0;
                            ppppb = Convert.ToDecimal(PartBOwn.Text);
                            if (ppppb >= 0)
                            {
                                PartBOwn.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }
                        DataTable dt345 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
                        string EmpIDa = "";
                        string Actionsa = "";
                        if (dt345.Rows.Count > 0)
                        {
                            EmpIDa = dt345.Rows[0]["ForEmpInfoId"].ToString();
                            Actionsa = dt345.Rows[0]["ActionStatus"].ToString();
                            LinkButton PartC = (LinkButton)AppraisalOwn.Rows[i].FindControl("PartC");
                            if (Actionsa == "Approved")
                            {
                                AppraisalOwn.Columns[7].Visible = true;
                                //  AppraisalOwn.HeaderRow.Cells[5].Visible = false;//hide grid column header
                                //  PartC.Visible = false;

                            }
                            else
                            {
                                AppraisalOwn.Columns[7].Visible = false;

                                //AppraisalOwn.HeaderRow.Cells[5].Visible = true;//hide grid column header
                                //PartC.Visible = true;
                            }
                        }
                        else
                        {
                            AppraisalOwn.Columns[7].Visible = false;
                        }

                    KPIMIDAppraisalFunctionalPartDAL _aFincDal = new KPIMIDAppraisalFunctionalPartDAL();

                    DataTable dt2Deag = _aFincDal.GetAppraisalPermission2FinYear(Session["EmpInfoId"].ToString(), FinancialYearDropDownList.SelectedItem.Text);
                    if (dt2Deag.Rows.Count == 0)
                    {

                        Button btn_Save = (Button)AppraisalOwn.Rows[i].FindControl("btn_Save");
                        btn_Save.Visible = false;
                        try
                        {
                            Actionsa = dt345.Rows[0]["ActionStatus"].ToString();
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        if (Actionsa != "Approved")
                        {
                            ((Label)AppraisalOwn.Rows[i].FindControl("lblExpireStatus")).Text =
                                "Deadline Already expired.";
                        }


                    }
                    else
                    {
                        ((Label)AppraisalOwn.Rows[i].FindControl("lblExpireStatus")).Text = "";
                        ((Button)AppraisalOwn.Rows[i].FindControl("btn_Save")).Visible = true;
                    }
                    LinkButton training = (LinkButton) AppraisalOwn.Rows[i].FindControl("training");

                        if (training.Text == "Not Complete")
                        {
                            training.CssClass = "btn btn-danger btn-sm ";
                            training.Text = "Training (0)";
                          

                            
                        }

                        if (training.Text == "Complete")
                        {
                            training.CssClass = "btn btn-success btn-sm";

                            DataTable dtTra = _appPartA.GetAppraisalTraining(Convert.ToInt32(id_appraisalMaster.Value));
                            if (dtTra.Rows.Count > 0)
                            {
                                training.Text = "Training (" + dtTra.Rows.Count+")";
                            }
                            
                        }


                        try
                        {
                            decimal trainingD = 0;
                            trainingD = Convert.ToDecimal(training.Text);
                            if (trainingD >= 0)
                            {
                                training.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }

                       


                        try
                        {
                            LinkButton PartC = (LinkButton)AppraisalOwn.Rows[i].FindControl("PartC");
                           

                            PartC.CssClass = "btn btn-success btn-sm";

                        }
                        catch (Exception)
                        {
                            
                            
                        }

                        

                    }
                





            }
            else
            {
                DataTable DTSupp = _appDashboard.GetAppraisalDashboardSup(Convert.ToInt32(Session["EmpInfoId"]),
                    Convert.ToInt32(FinancialYearDropDownList.SelectedValue));
               
                if (DTSupp.Rows.Count > 0)
                {
                    gv_AppraisalBoard.DataSource = DTSupp;
                    gv_AppraisalBoard.DataBind();
                    //for (int i = 0; i < gv_AppraisalBoard.Rows.Count; i++)
                    //{
                    //    HiddenField id_empId = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_empId");

                    //    if (Session["EmpInfoId"].ToString() == id_empId.Value)
                    //    {
                    //        gv_AppraisalBoard.Rows[i].Visible = false;
                    //    }

                       
                    //}

                    //if (gv_AppraisalBoard.Rows.Count == 0)
                    //{
                    //    gv_AppraisalBoard.DataSource = null;
                    //    gv_AppraisalBoard.DataBind();
                    //}

                    for (int i = 0; i < gv_AppraisalBoard.Rows.Count; i++)
                    {



                        HiddenField id_appraisalMaster = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_appraisalMaster");

                        DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
                        string EmpID = "";
                        string Actions = "";
                        if (dt3.Rows.Count > 0)
                        {
                            EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                            Actions = dt3.Rows[0]["ActionStatus"].ToString();
                        }
                        //
                        //if (EmpID != Session["EmpInfoId"].ToString())
                        //{
                        //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                        //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                        //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;
                        //}

                        ////

                        //if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString())
                        //{
                        //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                        //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                        //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;

                        //}
                        //if (EmpID == "0")
                        //{
                        //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                        //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                        //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;

                        //}


                        LinkButton PartA = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("PartA");

                        if (PartA.Text == "Not Complete")
                        {
                            PartA.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartA.Text == "Complete")
                        {
                            PartA.CssClass = "btn btn-success btn-sm";

                        }


                        try
                        {
                            decimal pppp = 0;
                            pppp = Convert.ToDecimal(PartA.Text);
                            if (pppp >= 0)
                            {
                                PartA.CssClass = "btn btn-success btn-sm";

                            }
                        }
                        catch (Exception)
                        {

                        }



                        LinkButton PartB = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("PartB");

                        if (PartB.Text == "Not Complete")
                        {
                            PartB.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartB.Text == "Complete")
                        {
                            PartB.CssClass = "btn btn-success btn-sm";

                        }



                        try
                        {
                            decimal ppppb = 0;
                            ppppb = Convert.ToDecimal(PartB.Text);
                            if (ppppb >= 0)
                            {
                                PartB.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }


                        decimal partaa = 0; decimal partbb = 0;


                        try
                        {
                            partaa = Convert.ToDecimal(PartA.Text);
                        }
                        catch (Exception)
                        {

                            // throw;
                        }
                        try
                        {
                            partbb = Convert.ToDecimal(PartB.Text);
                        }
                        catch (Exception)
                        {

                            // throw;
                        }

                        LinkButton PartC = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("PartC");

                        string status = "";
                        decimal total = partaa + partbb;
                        if (total < 61)
                        {
                            status = "Poor";
                        }

                        if (total > 60 && total <= 70)
                        {
                            status = "Average";
                        }

                        if (total >= 71 && total < 81)
                        {
                            status = "Good";
                        }
                        if (total >= 81 && total < 91)
                        {
                            status = "Excellent";
                        }

                        if (total > 90)
                        {
                            status = "Outstanding";
                        }


                        if (PartC.Text == "Not Complete")
                        {
                            PartC.CssClass = "btn btn-danger btn-sm ";

                        }
                        else
                        {
                            PartC.CssClass = "btn btn-success btn-sm";

                        }


                        LinkButton Training = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("Training");

                        if (Training.Text == "Not Complete")
                        {
                            Training.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (Training.Text == "Complete")
                        {
                            Training.CssClass = "btn btn-success btn-sm";
                            Training.Text = "Completed";
                        }


                    }




                    RadioTextValue();
                }
                else
                {
                    gv_AppraisalBoard.DataSource = null;
                    gv_AppraisalBoard.DataBind();
                }

                RadioTextValue();
            }
        }
        else
        {
            
        }
    }

    protected void gv_AppraisalBoard_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex;
            HiddenField idReport = (HiddenField)e.Row.FindControl("idReport");
            LinkButton a = (LinkButton)e.Row.FindControl("PartA");
            LinkButton b = (LinkButton)e.Row.FindControl("PartB");
            LinkButton c = (LinkButton)e.Row.FindControl("PartC");
            LinkButton t = (LinkButton)e.Row.FindControl("Training");


            //if (idReport.Value.ToString() != Session["EmpInfoId"].ToString())
            //{
            //    a.Enabled = false;
            //    b.Enabled = false;
            //    c.Enabled = false;
            //    t.Enabled = false;

            //}

             
        }
    
    }


    protected void SelfScore_OnTextChanged(object sender, EventArgs e)
    {

        try
        {

            int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
            if (Convert.ToDecimal(((Label)gv_AppraisalPartB.Rows[rowIndex].Cells[3].FindControl("Weight")).Text) >=
                Convert.ToDecimal(((TextBox)gv_AppraisalPartB.Rows[rowIndex].Cells[3].FindControl("SelfScore")).Text))
            {
                decimal weightTotal = 0;

                if (gv_AppraisalPartB.Rows.Count > 0)
                {
                    for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
                    {
                        TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");
                        if (txtWeight.Text == "")
                        {
                            weightTotal = weightTotal + 0;
                        }
                        else
                        {
                            weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                        }
                    }
                    Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMarkSelf");
                    tst2.Text = weightTotal.ToString();
                }
            }

            else
            {
                (((TextBox)gv_AppraisalPartB.Rows[rowIndex].Cells[3].FindControl("SelfScore")).Text) = 0.ToString();
                AlertMessageBoxShow("Self-Mark must be less then or Equal to Weight (Number)");
            }

        }
        catch
        {

        }





    }

    public void RadioTextValue()
    
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        for (int i = 0; i < gv_AppraisalBoard.Rows.Count; i++)
        {
            HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_empId");
            RadioButtonList actionRadioButtonList = (RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList");

            string filepath = "";
            if (Session["AppPage"] != null)
            {
                filepath = Session["AppPage"].ToString();
            }

            DataTable dtdata = _appDashboard.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(),
                empID.Value);

            DataTable aDataTable = new DataTable();
            aDataTable.Columns.Add("Value");
            aDataTable.Columns.Add("Text");

            DataRow dataRow = null;


            //if (Session["ForEmpInfoId"].ToString() != Session["EmpInfoId"].ToString())
            if (dtdata.Rows.Count > 0)
            {
                dataRow = aDataTable.NewRow();
                dataRow["Text"] = "Approved";
                dataRow["Value"] = "Approved";
                aDataTable.Rows.Add(dataRow);

                dataRow = aDataTable.NewRow();
                dataRow["Text"] = "Review";
                dataRow["Value"] = "Review";
                aDataTable.Rows.Add(dataRow);

            }
            else
            {
                dataRow = aDataTable.NewRow();
                dataRow["Text"] = "Approved";
                dataRow["Value"] = "Verified";
                aDataTable.Rows.Add(dataRow);

                dataRow = aDataTable.NewRow();
                dataRow["Text"] = "Review";
                dataRow["Value"] = "Review";
                aDataTable.Rows.Add(dataRow);
            }

            actionRadioButtonList.DataValueField = "Value";
            actionRadioButtonList.DataTextField = "Text";
            actionRadioButtonList.DataSource = aDataTable;
            actionRadioButtonList.DataBind();


            //if (Session["ForEmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
            //{
            //    actionRadioButtonList.Items[1].Enabled = false;
            //}
        }
    }


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField selfMaster = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("selfMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");
        TextBox comments = (TextBox)AppraisalOwn.Rows[rowID].FindControl("txt_comments");
        LinkButton PartAOwn = (LinkButton)AppraisalOwn.Rows[rowID].FindControl("PartAOwn");
        LinkButton PartBOwn = (LinkButton)AppraisalOwn.Rows[rowID].FindControl("PartBOwn");

        try
        {
            DataTable dtFinalApprovalSubmit = new DataTable();
            DataTable dtSuppervisorSubmit = new DataTable();
            int FinalApproveCount = 0;
            DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(empID.Value);


            DataTable dtempdataSup = _appDashboard.GetEmpInfo(" WHERE ReportingEmpId is not null and  EmpInfoId='" + empID.Value + "'");

            CheckBox IsMidYearStatus = (CheckBox)AppraisalOwn.Rows[rowID].FindControl("IsMidYearStatus");

            String ddd = "";
            try
            {
                ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
            }
            catch (Exception)
            {

                //throw;
            }

            if (ddd == "True")
            {


            }
            else if (dtempdataSup.Rows.Count > 0)
            {


                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("EmpMasterCode");
                //DataRow dataRow = null;
                //dataRow = aDataTable.NewRow();
                //dataRow["EmpInfoId"] = "0";
                //dataRow["EmpName"] = "Please Select an Employee.....";
                //dataRow["EmpMasterCode"] = "";
                //aDataTable.Rows.Add(dataRow);
                appDal.ReportingEmpData(empID.Value, aDataTable);


                dtSuppervisorSubmit = aDataTable;

               

                for (int i = 0; i < dtSuppervisorSubmit.Rows.Count; i++)
                {
                    dtFinalApprovalSubmit = _appPartA.GetFinalApproveByEmpId(empID.Value, dtSuppervisorSubmit.Rows[i]["EmpInfoId"].ToString());

                    if (dtFinalApprovalSubmit.Rows.Count>0)
                    {
                        FinalApproveCount = FinalApproveCount + 1;
                    }
                }


            }


            if (PartAOwn.Text != "Not Complete" && PartBOwn.Text != "Not Complete")
            {



                if (IsMidYearStatus.Checked == false)
                {






                    if (ddd == "True")
                    {
                        AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO();




                        bool IsMidYearSta = false;
                        bool status2 = _appDashboard.UpdateIsMidYearStatus(Convert.ToInt32(selfMaster.Value), IsMidYearSta);

                        appLogDao.ActionStatus = "Drafted";
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                        appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                        appLogDao.AppraisalMasterId = Convert.ToInt32(mastrId.Value);
                        appLogDao.Comments = comments.Text;




                        int idd = _appDashboard.SaveEmpAppLog(appLogDao);


                        AppraisalMasterAppLogDAO aMastera = new AppraisalMasterAppLogDAO();
                        aMastera.AppraisalMasterId
                            = Convert.ToInt32(mastrId.Value);
                        aMastera.ActionStatus = "Verified";
                        bool status = _appDashboard.UpdateContractural(aMastera);

                        AppraisalMasterAppLogDAO appLogDao1 = new AppraisalMasterAppLogDAO()
                        {
                            ActionStatus = "Verified",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(CheckFinalApproval.Rows[0]["EmpInfoId"].ToString()),
                            AppraisalMasterId = Convert.ToInt32(mastrId.Value),
                            Comments = comments.Text,

                        };
                        int id = _appDashboard.SaveEmpAppLog(appLogDao1);

                        SenMailForApprved(appLogDao1.ForEmpInfoId, " Appraisal Setup Approval ", @"  <br/> Dear Sir, <br/>
A Employee's Appraisal is waiting for your approval. <br/><br/>
 please login with the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    }


                    else if (dtempdataSup.Rows.Count > 0 && FinalApproveCount ==1   && dtSuppervisorSubmit.Rows.Count > 0)
                    {

                        AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO();




                        bool IsMidYearSta = false;
                        bool status2 = _appDashboard.UpdateIsMidYearStatus(Convert.ToInt32(selfMaster.Value), IsMidYearSta);

                        appLogDao.ActionStatus = "Drafted";
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                        appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                        appLogDao.AppraisalMasterId = Convert.ToInt32(mastrId.Value);
                        appLogDao.Comments = comments.Text;




                        int idd = _appDashboard.SaveEmpAppLog(appLogDao);


                        AppraisalMasterAppLogDAO aMastera = new AppraisalMasterAppLogDAO();
                        aMastera.AppraisalMasterId
                            = Convert.ToInt32(mastrId.Value);
                        aMastera.ActionStatus = "Verified";
                        bool status = _appDashboard.UpdateContractural(aMastera);

                        AppraisalMasterAppLogDAO appLogDao1 = new AppraisalMasterAppLogDAO()
                        {
                            ActionStatus = "Verified",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(dtempdataSup.Rows[0]["ReportingEmpId"].ToString()),
                            AppraisalMasterId = Convert.ToInt32(mastrId.Value),
                            Comments = comments.Text,

                        };
                        int id = _appDashboard.SaveEmpAppLog(appLogDao1);

                        SenMailForApprved(appLogDao1.ForEmpInfoId, " Appraisal Setup Approval ", @"  <br/> Dear Sir, <br/>
A Employee's Appraisal is waiting for your approval. <br/><br/>
 please login with the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    }
                    else
                    {
                        AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");
                    }

                }
                else
                {

                    bool IsMidYearSta = true;
                    bool status2 = _appDashboard.UpdateIsMidYearStatus(Convert.ToInt32(selfMaster.Value), IsMidYearSta);
                }


                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                 "alert",
                                 "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                                 true);
            }
            else
            {
                AlertMessageBoxShow("Please Complete Functional & Behavioral Part");

            }
        }
        catch (Exception)
        {

            AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");
        }
       
    }

    protected void btn_print_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField id_empId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");
     

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("AppraisalSelfMasterId");


        string url = "../Report_UI/KPIMidYearInformationView.aspx?EmpInfoId=" + id_empId.Value + "&financialYearId=" + FinancialYearDropDownList.SelectedValue;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

        // Response.Redirect("../Report_UI/KPIMidYearInformationView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }
    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("shuvosmtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("shuvono-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("shuvono-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }

    protected void btn_Save1_OnClick(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        bool confirmstatus = false;
        try
        {
            confirmstatus = Convert.ToBoolean(gv_AppraisalBoard.DataKeys[rowID][1].ToString());
        }
        catch (Exception)
        {
            
            
        }
        if (confirmstatus)
        {





            HiddenField mastrId = (HiddenField) gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
            HiddenField empID = (HiddenField) gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
            TextBox comments = (TextBox) gv_AppraisalBoard.Rows[rowID].FindControl("txt_comments");
            RadioButtonList actionRadioButtonList =
                (RadioButtonList) gv_AppraisalBoard.Rows[rowID].FindControl("actionRadioButtonList");

            AppraisalMasterAppLogDAO aMaster = new AppraisalMasterAppLogDAO();
            aMaster.AppraisalMasterId
                = Convert.ToInt32(mastrId.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = _appDashboard.UpdateContractural(aMaster);
            if (status)
            {
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        _appDashboard.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                    {
                        ActionStatus = actionRadioButtonList.SelectedValue,
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                        AppraisalMasterId = aMaster.AppraisalMasterId,
                        Comments = comments.Text,

                    };
                    int id = _appDashboard.SaveEmpAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                    AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                    {
                        ActionStatus = actionRadioButtonList.SelectedValue,
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        ForEmpInfoId = 0,
                        AppraisalMasterId = aMaster.AppraisalMasterId,
                        Comments = comments.Text,

                    };
                    int id = _appDashboard.SaveEmpAppLog(appLogDao);
                    //_appDashboard.SaveAppraisalMasterFromAppraisalSelf(aMaster.AppraisalMasterId.ToString());
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = _appDashboard.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(),
                        mastrId.Value);
                    DataTable dtempdata2 = _appDashboard.GetEmpInfoPrevious(
                        dtempdata.Rows[0]["PreEmpInfoId"].ToString(), mastrId.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                        {
                            ActionStatus = "Verified",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                            AppraisalMasterId = aMaster.AppraisalMasterId,
                            Comments = comments.Text,

                        };
                        _appDashboard.UpdateAppLog("Review", gv_AppraisalBoard.DataKeys[rowID][0].ToString());
                        int id = _appDashboard.SaveEmpAppLog(appLogDao);
                    }
                    else
                    {
                        AlertMessageBoxShow("Please select Approval Status Approved  this!!!");
                    }
                }


            }
            //Session["AppLogId"] = null;
            gv_AppraisalBoard.DataSource = _appDashboard.GetAppraisalDashboardSup(Convert.ToInt32(Session["EmpInfoId"]),Convert.ToInt32(FinancialYearDropDownList.SelectedValue));
            gv_AppraisalBoard.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                       true);
        }
        else
        {
            AlertMessageBoxShow("Please Complete All The Procedure ");
        }
    }

    protected void ShowPopup(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;

        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;



        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");

        DataTable dtdata = _appDashboard.GetAllComments(mastrId.Value);
        GridView1.DataSource = dtdata;
        GridView1.DataBind();


    }

    protected void ShowPopup1(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;

        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;



        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");

        DataTable dtdata = _appDashboard.GetAllComments(mastrId.Value);
        GridView1.DataSource = dtdata;
        GridView1.DataBind();


    }


    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        radOp_OnSelectedIndexChanged(null, null);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        mpe_1.Hide();

    }

    protected void btnFunctionalSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationlFunctional() == true)
        {
            List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {

                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
               
                DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");


                TextBox txtResultYearEnd = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtResultYearEnd");
                
                
              
                TextBox selfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                
                TextBox txtResult = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtResult");
                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
                CheckBox isActiveCheckBox = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();
                    try
                    {
                        area.AppraisalSelfFucAreaId = Convert.ToInt32(gv_AppraisalFunc.DataKeys[i][1].ToString());
                    }
                    catch (Exception)
                    {
                        area.AppraisalSelfFucAreaId = 0;
                        //throw;
                    }
                    area.IsActive =true;
                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    decimal? selfMarkValue = string.IsNullOrWhiteSpace(selfMark.Text) ? (decimal?)null : Convert.ToDecimal(selfMark.Text.Trim());

                    area.SelfMark = selfMarkValue;

                    try
                    {
                        area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                    }
                    catch (Exception)
                    {
                        area.TargetPer = 0;
                        //throw;
                    }


                    try
                    {
                        area.ResultYearEnd = Convert.ToString(txtResultYearEnd.Text.Trim().ToString());
                    }
                    catch (Exception)
                    {
                        area.ResultYearEnd ="";
                        //throw;
                    }
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());

                    area.SupervisorMark = null;
                    area.MidYearStatus = txtMidStatus.SelectedItem.Text.Trim().ToString();

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);
            aMaster.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalMaster(aMaster, Session["UserId"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialFunctionalDetails(functional, pk, aMaster.AppraisalSelfMasterId);
                }
            }
            else
            {
                result = false;
            }

            if (result == true)
            {


                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                    true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }

        }
    }

    private bool ValidationlFunctional()
    {
        bool isVAlid = true;
        if (gv_AppraisalFunc.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Functional Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
            TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
            DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");

            if (txtMidStatus.SelectedValue == "0" )
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Please select Mid-Year Status! ", this);
                txtMidStatus.Focus();
                break;
            }

                TextBox txtselfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
            if (string.IsNullOrEmpty(txtWeightPer.Text))
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Expected Number Required ", this);
                txtWeight.Focus();
                break;
            }

            if (tbKpi.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Key Performance Indicator Required ", this);
                tbKpi.Focus();
                break;
            }

            if (txtTarget.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox(" Target Required ", this);
                txtTarget.Focus();

                break;
            }
            if (txtWeight.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Weight Required ", this);
                txtWeight.Focus();

                break;
            }

            //if (txtselfMark.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Self-Mark Required ", this);
            //    break;
            //}
            if (txtMidStatus.SelectedValue != "Not Started" && txtMidStatus.SelectedValue != "Ongoing")
            {

                
                if (txtselfMark.Text == "")
                {
                    isVAlid = false;
                    aShowMessage.ShowMessageBox("Please Enter Mid-Year Marking", this);
                    txtselfMark.Focus();
                    break;
                }
            }

            //if (txtMidStatus.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Mid Year Status Required ", this);
            //    break;
            //}
        }
        Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");

        decimal ddddd = 0;
        try
        {
            ddddd = Convert.ToDecimal(tst.Text);
        }
        catch (Exception)
        {
            
            
        }

        //if (ddddd!=75)
        //{
        //    isVAlid = false;
        //    aShowMessage.ShowMessageBox("Weight (Number) Must be 75 ", this);
          
        //}



        Label lblTotalMark = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");

        decimal TotalMark = 0;
        try
        {
            TotalMark = Convert.ToDecimal(lblTotalMark.Text);
        }
        catch (Exception)
        {


        }

        if (TotalMark >= 76)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Total Weight Can Not be Bigger than  75 ", this);

        }

        return isVAlid;
    }

    protected void btnFunctionalCancel_OnClick(object sender, EventArgs e)
    {
        
    }
    protected void CalculateTotalFunc()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                CheckBox chkIsActive = (CheckBox) gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                if (chkIsActive.Checked)
                {

                    TextBox txtMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");



                    if (txtMark.Text == "")
                    {
                        markTotal = markTotal + 0;
                    }
                    else
                    {
                        markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                    }




                }

               
            }
            Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst.Text = markTotal.ToString();

        }
    }
    protected void txtselfMark_OnTextChanged(object sender, EventArgs e)
    {
        int i = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
        TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

        decimal selfMark, weightMark;

        // Check if both inputs are valid decimal numbers
        if (txtMark != null && txtWeight != null &&
            decimal.TryParse(txtMark.Text.Trim(), out selfMark) &&
            decimal.TryParse(txtWeight.Text.Trim(), out weightMark))
        {
            if (selfMark > weightMark)
            {
                txtMark.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Self Mark cannot be greater than Weight!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Please enter valid numbers!');", true);
            txtMark.Text = "";
        }

        CalculateB();

    }

    protected void btnBehavioralClose_Click(object sender, EventArgs e)
    {
        MPBehavioral.Hide();
    }

    protected void btnTrainingClose_Click(object sender, EventArgs e)
    {
        MPTraining.Hide();
    }
    protected void CalculateTotal()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                CheckBox chkIsActive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                    if (chkIsActive.Checked)
                {


                    TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

                    TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
                   
                    if (txtWeight.Text == "")
                    {
                        weightTotal = weightTotal + 0;
                    }
                    else
                    {
                        weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                    }
                    if (txtMark.Text == "")
                    {
                        markTotal = markTotal + 0;
                    }
                    else
                    {
                        markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                    }



                   
                }

            }

            
            Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");
            tst.Text = weightTotal.ToString();

            //Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            //tst2.Text = markTotal.ToString();
        }
    }
    protected void isActiveCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chkIsActive = (CheckBox)gv_AppraisalFunc.Rows[rowIndex].FindControl("isActiveCheckBox");
        LinkButton btnFunction = (LinkButton)gv_AppraisalFunc.Rows[rowIndex].FindControl("btnFunction");
        if (chkIsActive.Checked==false)
        {
            btnFunction.Visible = true;
        }
        else
        {
            btnFunction.Visible = false;
            
        }

        try
        {
            if (Convert.ToDecimal(((TextBox)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text) >=
           Convert.ToDecimal(((TextBox)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text))
            {
               
            }

            else
            {
                (((TextBox)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text) = 0.ToString();
                AlertMessageBoxShow("Self-Mark must be less then or Equal to Weight (Number)");
            }
        }
        catch (Exception)
        {
            ((TextBox)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text = "0";
            AlertMessageBoxShow("Please Fill Weight (Number)");
            //throw;
        }

        CalculateTotalFunc();
        CalculateTotal();
    }
    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingPart"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("TrainingNeeds", typeof(string)));
            dt.Columns.Add(new DataColumn("quaterID", typeof(string)));
            //   dt.Columns.Add(new DataColumn("TrainingEnd", typeof (string)));

            dr = dt.NewRow();

            dr["TrainingNeeds"] = "";
            dr["quaterID"] = "";
            //  dr["TrainingEnd"] = "";

            dt.Rows.Add(dr);
            ViewState["TrainingPart"] = dt;

            gv_AppraisalTraining.DataSource = dt;
            gv_AppraisalTraining.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["TrainingPart"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["TrainingPart"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");



                dtCurrentTable.Rows[i]["TrainingNeeds"] = txtSkillInfo.Text.Trim().ToString() == "" ? "" : txtSkillInfo.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["quaterID"] = txt_BranchCountry.SelectedValue;


            }

            gv_AppraisalTraining.DataSource = dtCurrentTable;
            gv_AppraisalTraining.DataBind();
            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                txt_BranchCountry.SelectedValue = dtCurrentTable.Rows[i]["quaterID"].ToString();
            }
        }
    }



    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingPart"] != null && gv_AppraisalTraining.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["TrainingPart"];

            if (dt.Rows.Count == 0)
            {
                ViewState["TrainingPart"] = null;
            }
            else
            {
                ViewState["TrainingPart"] = dt;
            }
            for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");



                dt.Rows[i]["TrainingNeeds"] = txtSkillInfo.Text.Trim().ToString() == "" ? "" : txtSkillInfo.Text.Trim().ToString();
                dt.Rows[i]["quaterID"] = txt_BranchCountry.SelectedValue;


            }
            dt.Rows.Remove(dt.Rows[rowID]);
            gv_AppraisalTraining.DataSource = dt;
            gv_AppraisalTraining.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                txt_BranchCountry.SelectedValue = dt.Rows[i]["quaterID"].ToString();
            }

        }
    }
    private KPIMIDAppraisalPartBDAL _appraisalPartBdal = new KPIMIDAppraisalPartBDAL();

    protected void btnBehave_OnClick(object sender, EventArgs e)
    {



        if (ValidationBehave() == true)
        {
            List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                Label txtScore = (Label)gv_AppraisalPartB.Rows[i].FindControl("Weight");
                Label SetScore = (Label)gv_AppraisalPartB.Rows[i].FindControl("SetScore");
                TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");

                if (txtSkillInfo.Text.Trim().ToString() != "" && txtSelfScore.Text.Trim().ToString() != "")
                {
                    AppraisalBehaveArea area = new AppraisalBehaveArea();
                    area.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                    area.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);
                    area.SkillInfo = txtSkillInfo.Text.Trim().ToString();
                    area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();
                    area.Score = Convert.ToDecimal(txtScore.Text.ToString());
                    area.SetScore = Convert.ToDecimal(SetScore.Text.ToString());
                    area.SelfScore = Convert.ToDecimal(txtSelfScore.Text.ToString());
                    area.Comments = "";

                    aList.Add(area);

                }


            }
            if (aList.Count > 0)
            {
                bool result = _appraisalPartBdal.SaveAppraisalPartB(aList, Convert.ToInt32(id_selfID.Value));
                if (result == true)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                      true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }

    }

    private bool ValidationBehave()
    {
        bool isVAlid = true;
        if (gv_AppraisalPartB.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Behavioral Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {

            TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");




            if (txtSelfScore.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Behavioral Score Required ", this);
                break;
            }


        }
        return isVAlid;
    }


    private bool ValidationBehaveSupp()
    {
        bool isVAlid = true;
        if (gv_AppraisalPartBSUP.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Behavioral Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
        {

            TextBox SupervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");




            if (SupervisorScore.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Behavioral Supervisor Score Required ", this);
                break;
            }


        }
        return isVAlid;
    }

    protected void btnTrainSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationTrain())
        {


            List<AppraisalTrainingNeeds> aList = new List<AppraisalTrainingNeeds>();
            for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox) gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                TextBox txtSupportingEmp = (TextBox) gv_AppraisalTraining.Rows[i].FindControl("TrainingStart");
                TextBox txtScore = (TextBox) gv_AppraisalTraining.Rows[i].FindControl("TrainingEnd");
                DropDownList ddlQuater = (DropDownList) gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");

                if (txtSkillInfo.Text.Trim().ToString() != "")
                    //&& txtSupportingEmp.Text.Trim().ToString() != "" &&
                    //txtScore.Text.Trim().ToString() != ""
                {

                    AppraisalTrainingNeeds appraisal = new AppraisalTrainingNeeds();
                    appraisal.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                    appraisal.TrainingNeeds = txtSkillInfo.Text.Trim().ToString();
                    appraisal.TrainingStart = Convert.ToDateTime(DateTime.Now);
                    appraisal.TrainingEnd = Convert.ToDateTime(DateTime.Now);
                    appraisal.Quater = Convert.ToInt32(ddlQuater.SelectedValue);
                    aList.Add(appraisal);
                }



            }

            if (aList.Count > 0)
            {
                bool result = _appraisalPartBdal.SaveTrainingNeeds(aList);
                if (result == true)
                {




                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                        true);



                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }
    }


    private bool ValidationTrain()
    {
        bool isVAlid = true;
        if (gv_AppraisalTraining.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Training Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
        {

            TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");




            if (txtSkillInfo.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Training Needs Required ", this);
                break;
            }


        }
        return isVAlid;
    }

    protected void ViewComments_OnClick(object sender, EventArgs e)
    {
         LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete all Function");
        }
        else
        {
            mpComm.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);
            id_mastetID.Value = masterID.ToString();

            DataTable dt = _appPartA.GetViewComments(masterID);
            if (dt.Rows.Count > 0)
            {
                gv_Versions.DataSource = dt;
                gv_Versions.DataBind();
            }
        }
    }

    protected void btncommClose_Click(object sender, EventArgs e)
    {
        mpComm.Hide();
    }

    protected void btnFunctionalSupClose_Click(object sender, EventArgs e)
    {
        mpFunctionalSup.Hide();
    }

    protected void SupervisorMark_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        decimal weight = 0, mark = 0;
        weight = Convert.ToDecimal(((TextBox)gv_AppraisalFuncSUP.Rows[rowID].FindControl("txtWeight")).Text);
        mark = Convert.ToDecimal(((TextBox)gv_AppraisalFuncSUP.Rows[rowID].FindControl("txtMark")).Text);
        if (weight < mark)
        {
            ((TextBox)gv_AppraisalFuncSUP.Rows[rowID].FindControl("txtMark")).Text = "0";
            double weightTotal = 0;

            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");

                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
                }


            }
            Label tst = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalMark");
            tst.Text = weightTotal.ToString();
            aShowMessage.ShowMessageBox("Supuervisor Marks Cannot Be Greater Then Weight", this);
        }
        else
        {


            double weightTotal = 0;

            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");

                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
                }


            }
            Label tst = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalMark");
            tst.Text = weightTotal.ToString();
        }
    }

    protected void txtWeightSUP_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFuncSUP.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFuncSUP.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thePer = (weightNum / (75.0 / 100.0));
        txtweightper.Text = thePer.ToString("#,##0.00");
        CalculateTotalWeightSUP();
    }

    private void CalculateTotalWeightSUP()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFuncSUP.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");

                TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");


                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }
                if (txtMark.Text == "")
                {
                    markTotal = markTotal + 0;
                }
                else
                {
                    markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                }




            }

            Label tst = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalWeight");
            tst.Text = weightTotal.ToString();

            Label tst2 = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalMark");
            tst2.Text = markTotal.ToString();
        }
    }

    protected void btnAppraisalFuncSUPSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationFunCSUp() == true)
        {
            List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
                TextBox selfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
                TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");
                TextBox txtResultYearEnd = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResultYearEnd");
                TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();
                    area.AppraisalSelfFucAreaId = Convert.ToInt32(gv_AppraisalFuncSUP.DataKeys[i][0].ToString());
                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    area.SelfMark = string.IsNullOrEmpty(selfMark.Text) ? 0 : Convert.ToDecimal(selfMark.Text.Trim().ToString());
                    area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                    area.ResultYearEnd = (txtResultYearEnd.Text.Trim().ToString());
                    area.SupervisorMark = Convert.ToDecimal(txtMark.Text.Trim().ToString());
                    area.MidYearStatus = txtMidStatus.Text.Trim().ToString();

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);
            aMaster.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialFunctionalDetails(functional, pk, aMaster.AppraisalSelfMasterId);
                }
            }
            else
            {
                result = false;
            }

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                   true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }

        }
       

    }

    private bool ValidationFunCSUp()
    {
        bool isVAlid = true;
        if (gv_AppraisalFuncSUP.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Functional Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
        {

            TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");
            TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");




            if (txtMark.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Functional Supervisor Score Required ", this);
                break;
            }

            if (txtMark.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Functional Result End Status	 Required ", this);
                break;
            }
        }
        return isVAlid;
    }

    protected void btnMPBSupClose_Click(object sender, EventArgs e)
    {
        MPBSup.Hide();
    }

    protected void SupervisorScore_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox Weight = (TextBox)gv_AppraisalPartBSUP.Rows[rowID].FindControl("Weight");
        TextBox txtsupWeight = (TextBox)gv_AppraisalPartBSUP.Rows[rowID].FindControl("SupervisorScore");
        decimal wet = 0, supervisor = 0;
        wet = Convert.ToDecimal(Weight.Text);
        supervisor = Convert.ToDecimal(txtsupWeight.Text);

        if (supervisor > wet)
        {
            txtsupWeight.Text = "0";
            AlertMessageBoxShow("Supervisor Mark Cannot Be Greater Then Weight");
        }


        double weightTotal = 0;

        for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
        {
            TextBox txtWeight = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");

            if (txtWeight.Text == "")
            {

                weightTotal = weightTotal + 0;
            }
            else
            {
                weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
            }


        }
        Label tst = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("lblTotalMark");
        tst.Text = weightTotal.ToString();
    }

    protected void btnPartBSUPSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationBehaveSupp() == true)
        {
            List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

            for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupportingEmp");
                TextBox txtScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("Weight");
                TextBox txtSelfScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SelfScore");
                Label SetScore = (Label)gv_AppraisalPartBSUP.Rows[i].FindControl("SetScore");
                TextBox supervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");

                if (txtSkillInfo.Text.Trim().ToString() != "" && supervisorScore.Text.Trim().ToString() != "")
                {
                    AppraisalBehaveArea area = new AppraisalBehaveArea();
                    area.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                    area.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);
                    area.SkillInfo = txtSkillInfo.Text.Trim().ToString();
                    area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();
                    area.Score = Convert.ToDecimal(txtScore.Text.ToString());
                    area.SetScore = Convert.ToDecimal(SetScore.Text.ToString());
                    area.SelfScore = Convert.ToDecimal(txtSelfScore.Text.ToString());
                    area.SupervisorScore = Convert.ToDecimal(supervisorScore.Text.ToString());
                    aList.Add(area);

                }


            }
            if (aList.Count > 0)
            {
                bool result = _appraisalPartBdal.SaveAppraisalPartB(aList, Convert.ToInt32(id_selfID.Value));
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                      true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }
    }

    protected void btnFinalStatusClose_Click(object sender, EventArgs e)
    {
       MPFinalStatus.Hide();
    }


    protected void recommend_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (recommend.SelectedValue == "1")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;

        }

        if (recommend.SelectedValue == "2")
        {
            Divsteps.Visible = true;
            Div1Other.Visible = false;

        }

        if (recommend.SelectedValue == "3")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;
        }
        if (recommend.SelectedValue == "4")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;

        }
        if (recommend.SelectedValue == "5")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;

        }
        if (recommend.SelectedValue == "6")
        {
            Div1Other.Visible = true;
            Divsteps.Visible = false;
        }
    }

    protected void btnFinalStatusSave_OnClick(object sender, EventArgs e)
    {
        AppraisalFinalStatus appraisalFinal = new AppraisalFinalStatus();

        string recom = recommend.SelectedValue.ToString();

        if (recom == "1")
        {
            appraisalFinal.GeneralIncrement = true;

            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Pip = false;
            appraisalFinal.Other = false;

        }

        if (recom == "2")
        {
            appraisalFinal.SpecialIncrement = true;

            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Pip = false;
            appraisalFinal.Other = false;
            appraisalFinal.SpecialStep = Convert.ToInt32(txtStep.Text);

        }

        if (recom == "3")
        {
            appraisalFinal.IsPromotion = true;
            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.Pip = false;
            appraisalFinal.Other = false;
        }
        if (recom == "4")
        {
            appraisalFinal.Pip = true;

            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Other = false;

        }

        if (recom == "5")
        {
            appraisalFinal.Pip = false;

            appraisalFinal.GeneralIncrement = true;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = true;
            appraisalFinal.Other = false;

        }

        if (recom == "6")
        {
            appraisalFinal.Other = true;
            appraisalFinal.Pip = false;
            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Note = txtnote.Text.Trim();

        }
        appraisalFinal.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
        appraisalFinal.FinalStatus = lblStatus.Text;
        appraisalFinal.TotalScore = Convert.ToDecimal(totalScore.Text);


        bool result = _appraisalPartBdal.SaaveFinalStatus(appraisalFinal);
        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                       true);
        }
        else
        {
            AlertMessageBoxShow("Operation Failed");
        }
    }

    protected void btnTrainingSUPClose_Click(object sender, EventArgs e)
    {
       MPTrainingSUP.Hide();
    }

    protected void btnTrainSaveSUP_OnClick(object sender, EventArgs e)
    {
        List<AppraisalTrainingNeeds> aList = new List<AppraisalTrainingNeeds>();
        for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
        {
            TextBox txtSkillInfo = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
            TextBox txtSupportingEmp = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingStart");
            TextBox txtScore = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingEnd");
            DropDownList ddlQuater = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");

            if (txtSkillInfo.Text.Trim().ToString() != "")
            //&& txtSupportingEmp.Text.Trim().ToString() != "" &&
            //txtScore.Text.Trim().ToString() != ""
            {

                AppraisalTrainingNeeds appraisal = new AppraisalTrainingNeeds();
                appraisal.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                appraisal.TrainingNeeds = txtSkillInfo.Text.Trim().ToString();
                appraisal.TrainingStart = Convert.ToDateTime(DateTime.Now);
                appraisal.TrainingEnd = Convert.ToDateTime(DateTime.Now);
                appraisal.Quater = Convert.ToInt32(ddlQuater.SelectedValue);
                aList.Add(appraisal);
            }



        }

        if (aList.Count > 0)
        {
            bool result = _appraisalPartBdal.SaveTrainingNeeds(aList);
            if (result == true)
            {




                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                    true);



            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }

    protected void ViewComments2_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete all Function");
        }
        else
        {
            mpComm.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);
            id_mastetID.Value = masterID.ToString();

            DataTable dt = _appPartA.GetViewComments(masterID);
            if (dt.Rows.Count > 0)
            {
                gv_Versions.DataSource = dt;
                gv_Versions.DataBind();
            }
        }
    }

    protected void txtMidStatus_OnTextChanged(object sender, EventArgs e)
    {
        
    }

    protected void txtResult_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        decimal tResult = 0;
       


        for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
        {
            TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");

            if (txtResult.Text == "")
            {
                tResult = tResult + 0;
            }
            else
            {
                tResult = tResult + Convert.ToDecimal(txtResult.Text.ToString());
            }


        }

        Label lblresultend = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblresultend");
        lblresultend.Text = tResult.ToString();
    }

    

    protected void lbUploadDoc_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete the Functional Area");
        }
        else
        {
            ModalPopupExtender1.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);
            id_mastetID.Value = masterID.ToString();


            DataTable dtDoc = _appraisalPartBdal.GetDocDataById(id_mastetID.Value);
            if (dtDoc.Rows.Count > 0)
            {
                ViewState["DocGrid_List"] = dtDoc;
                gv_DocumentUpload.DataSource = dtDoc;
                gv_DocumentUpload.DataBind();
            }
            DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";

            if (dt3.Rows.Count > 0)
            {
                EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3.Rows[0]["ActionStatus"].ToString();
                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnSaveDOcment.Visible = false;

                }
            }
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalTraining.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "' ,'_blank');", true);
        }
    }
    protected void btnDocRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["DocGrid_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = dt;
                gv_DocumentUpload.DataBind();
            }
            else
            {
                ViewState["DocGrid_List"] = null;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = null;
                gv_DocumentUpload.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetDocGrid_List();
    }
    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void btnDocClose_OnClick(object sender, EventArgs e)
    {
         ModalPopupExtender1.Hide();
    }

    protected void btnSaveDOcment_OnClick(object sender, EventArgs e)
    {

        if (id_mastetID.Value!="")
        {
            List<AppraisalInfoDocumentDAO> DocList = new List<AppraisalInfoDocumentDAO>();


            string DocNote = "";
            string Member = "";
            for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
            {
                HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[i].FindControl("hfFileName");


                AppraisalInfoDocumentDAO DocA = new AppraisalInfoDocumentDAO();

                DocA.DocumentLink = hfDocumentLink.Value.ToString();
                DocA.FileName = hfFileName.Value.ToString();
                DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                DocNote = DocNote + lbl_DocumentNote.Text.Trim() + " ";

                DocList.Add(DocA);
            }
           
                _appraisalPartBdal.SaveDocumentDetails(DocList, Convert.ToInt32(id_mastetID.Value));

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='KPIMidYearMarkSetSelf.aspx';",
                   true);
            
        }
        else
        {
            AlertMessageBoxShow("Please Complete All The Procedure ");
        }
       
    }

    protected void PartCSupp_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("selfMaster");
        if (FinancialYearDropDownList.SelectedValue != "")
        {

            Session["Status"] = "Edit";


            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('KPIInformationDetailsView.aspx?EmpInfoId=" + empID.Value + "&financialYearId=" + FinancialYearDropDownList.SelectedValue + "' ,'_blank');", true);
            //Response.Redirect("KPIInformationDetailsView.aspx?EmpInfoId=" + empID.Value + "&financialYearId=" + FinancialYearDropDownList.SelectedValue + "");
        }
        else
        {
            AlertMessageBoxShow("Please Select  Financial Year !!");
        }
    }

    protected void btnFunction_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("KpiInfo", typeof(string)));
            dt.Columns.Add(new DataColumn("KpiWeight", typeof(string)));
            dt.Columns.Add(new DataColumn("KpiWeightPer", typeof(string)));
            dt.Columns.Add(new DataColumn("Target", typeof(string)));
            dt.Columns.Add(new DataColumn("TargetPer", typeof(string)));
            dt.Columns.Add(new DataColumn("Deadline", typeof(string)));
            dt.Columns.Add(new DataColumn("MidYearStatus", typeof(string)));

            dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(bool)));
            dr = dt.NewRow();

            dr["KpiInfo"] = "";
            dr["KpiWeight"] = "";
            dr["KpiWeightPer"] = "";
            dr["Target"] = "";
            dr["TargetPer"] = "";
            dr["Deadline"] = "";
            dr["MidYearStatus"] = "";

            dr["SelfMark"] = "";
            dr["SupervisorMark"] = "";


            dr["IsActive"] = "False";

            dr["SelfMark"] = "";
            dt.Rows.Add(dr);
            ViewState["KPIFUNC"] = dt;

            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["KPIFUNC"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();

            drCurrentRow["IsActive"] = "True";

            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["KPIFUNC"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer"); 
                DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                TextBox txtselfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");


                dtCurrentTable.Rows[i]["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                    ? ""
                    : tbKpi.Text.Trim().ToString();

                
                    tbKpi.ReadOnly = false;
                
                try
                {
                    dtCurrentTable.Rows[i]["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                 ? ""
                 : txtWeight.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeight"] = "0";

                }



                try
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = "0";
                    //throw;
                }


                try
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = "0";
                    //throw;
                }



                try
                {
                    dtCurrentTable.Rows[i]["Target"] = txtTarget.Text.Trim().ToString() == ""
                 ? ""
                 : txtTarget.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["Target"] = "0";
                }

                try
                {
                    dtCurrentTable.Rows[i]["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtTargetPer.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["TargetPer"] = "0";
                }

                dtCurrentTable.Rows[i]["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();
                try
                {
                    dtCurrentTable.Rows[i]["SelfMark"] = txtselfMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtselfMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["SelfMark"] = "0";
                }

                try
                {
                    dtCurrentTable.Rows[i]["SupervisorMark"] = txtMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["SupervisorMark"] = "0";
                }

                dtCurrentTable.Rows[i]["IsActive"] = chkisactive.Checked;
                try
                {
                    dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.SelectedItem.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    
                    //throw;
                }

            }

            gv_AppraisalFunc.DataSource = dtCurrentTable;
            gv_AppraisalFunc.DataBind();

            CalculateTotal();
            CalculateTotalFunc();
        }

        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox tbKpi = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
            TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
            DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
            CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

            TextBox txtselfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");

            int ii = 0;
            try
            {
                ii = Convert.ToInt32(gv_AppraisalFunc.DataKeys[i][1].ToString());
            }
            catch (Exception)
            {
                ii = 0;
                //throw;
            }
            if (ii==0)
            {
                txtDeadLine.Enabled = true;
                txtMidStatus.Enabled = true;
                tbKpi.ReadOnly = false;
                txtWeight.ReadOnly = false;
                txtTarget.ReadOnly = false;
            }

 
        }
    }

    protected void lb_FunctioRemove_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;



        DataTable aTable = new DataTable();
        aTable.Columns.Add(new DataColumn("KpiInfo", typeof(string)));
        aTable.Columns.Add(new DataColumn("KpiWeight", typeof(string)));
        aTable.Columns.Add(new DataColumn("KpiWeightPer", typeof(string)));
        aTable.Columns.Add(new DataColumn("Target", typeof(string)));
        aTable.Columns.Add(new DataColumn("TargetPer", typeof(string)));
        aTable.Columns.Add(new DataColumn("Deadline", typeof(string)));
        aTable.Columns.Add(new DataColumn("MidYearStatus", typeof(string)));
        aTable.Columns.Add(new DataColumn("IsActive", typeof(bool)));
        aTable.Columns.Add(new DataColumn("SelfMark", typeof(string)));
        DataRow dr;


        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            if (i != rowIndex)
            {
                dr = aTable.NewRow();

                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                dr["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                  ? ""
                  : tbKpi.Text.Trim().ToString();

                try
                {
                    dr["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                 ? ""
                 : txtWeight.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dr["KpiWeight"] = "0";

                }



                try
                {
                    dr["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dr["KpiWeightPer"] = "0";
                    //throw;
                }

                try
                {
                    dr["Target"] = txtTarget.Text.Trim().ToString() == ""
                 ? ""
                 : txtTarget.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dr["Target"] = "0";
                }

                try
                {
                    dr["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtTargetPer.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dr["TargetPer"] = "0";
                }

                dr["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();
                try
                {
                    dr["SelfMark"] = txtMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dr["SelfMark"] = "0";
                }

                dr["IsActive"] = chkisactive.Checked;
                //dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();


                aTable.Rows.Add(dr);
            }
        }

        gv_AppraisalFunc.DataSource = aTable;
        gv_AppraisalFunc.DataBind();

       
    }

    protected void lbMidStatus_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
         
      
            AppraisalFunctionalArea area = new AppraisalFunctionalArea();
            try
            {
                area.AppraisalSelfFucAreaId = Convert.ToInt32(gv_AppraisalFunc.DataKeys[rowID][1].ToString());

                DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[rowID].FindControl("txtMidStatus");

                bool status2 = _appDashboard.UpdateMidYearStatus(Convert.ToInt32(area.AppraisalSelfFucAreaId), txtMidStatus.SelectedItem.Text.ToString());
                if (status2)
                {
                    AlertMessageBoxShow("Updated!");
                    mpe_1.Hide();
                 
                }
            }
            catch (Exception)
            {
                area.AppraisalSelfFucAreaId = 0;
                //throw;
            }
        
    }
}
