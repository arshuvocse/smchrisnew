using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.ExitManagement_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using Library.DAO.HRM_Entities;

public partial class Survey_ProbationEvaluationForm : System.Web.UI.Page
{
    SurveyCommonDAL _surveyCommonDal = new SurveyCommonDAL();
    ProbationperiodDAL aProbationperiodDal=new ProbationperiodDAL();
    private int mid = 0;
    private string _userId;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            actionRadioButtonList.SelectedIndex = 0;
            LoadDropDownList();
            //LoadGrid();
            string id = Request.QueryString["id"];
            hfEmpInfoId.Value = Request.QueryString["id"];
            GetEmpData(id);
            if (Session["MasterId"] !=null)
            {
                masterHiddenField.Value = Session["MasterId"].ToString();
                Session["MasterId"] = "";
                DataTable dtdatainfo = aProbationperiodDal.GetProbationInfo(masterHiddenField.Value);
                if (dtdatainfo.Rows.Count>0)
                {
                     ddlreason.SelectedItem.Text=dtdatainfo.Rows[0]["ProbationEndReason"].ToString();
                 
                    RadioButtonList1.Items[0].Selected = Convert.ToBoolean(dtdatainfo.Rows[0]["ExProbation"].ToString());
                    RadioButtonList1.Items[1].Selected = Convert.ToBoolean(dtdatainfo.Rows[0]["ProbationEnd"].ToString());
                    ExtendProbationDropDownList.Items[1].Selected = Convert.ToBoolean(dtdatainfo.Rows[0]["ExProbation"].ToString());
                    ExtendProbationDropDownList.Items[2].Selected = Convert.ToBoolean(dtdatainfo.Rows[0]["ProbationEnd"].ToString());

                    ExtendProbationDropDownList_OnSelectedIndexChanged(null, null);
                    try
                    {
                        exProDate.Text = Convert.ToDateTime(dtdatainfo.Rows[0]["ExProDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    catch (Exception)
                    {
                        
                       // throw;
                    }
                    bool isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                    if (isselfapp)
                    {
                        entryempinfoIdHiddenField.Value = dtdatainfo.Rows[0]["EmpInfoId"].ToString();
                    }
                    else
                    {
                        entryempinfoIdHiddenField.Value = dtdatainfo.Rows[0]["UserEmpInfoId"].ToString();
                    }

                    actionstatusHiddenField.Value = dtdatainfo.Rows[0].Field<String>("ActionStatus").ToString();
                }
            }
            DataTable dtdata =
                    aProbationperiodDal.GetApprovalComments(masterHiddenField.Value);
            AppLogCommentGridView.DataSource = dtdata;
            AppLogCommentGridView.DataBind();
            if (RadioButtonList1.Items[0].Selected)
            {
                exppr.Visible = true;

            }
            else
            {
                exppr.Visible = false;
                probendreason.Visible = true;
                if (exProDate.Text!=string.Empty)
                {
                    exppr.Visible = true;
                  //  ddlreason.SelectedIndex = 1;
                   
                }
                else
                {
                  //  ddlreason.SelectedIndex = 0;
                }
            }
            
            RadioTextValue();
            VisibleGrid(masterHiddenField.Value);
       //    GetProbData(masterHiddenField.Value);

        }
    }

    protected void ExtendProbationDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ExtendProbationDropDownList.SelectedValue == "1")
        {
            exppr.Visible = true;
            probendreason.Visible = false;
          
            exProDate.Visible = true;

            //if (ddlreason.SelectedValue == "1")
            //{
            //    dateprob.Visible = false;
            //    exProDate.Visible = false;
            //}

        }

        if (ExtendProbationDropDownList.SelectedValue == "2")
        {
            exppr.Visible = false;
            probendreason.Visible = true;
            ddlreason_OnSelectedIndexChanged(null, null);
        }


        // 
    }

    protected void ddlreason_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlreason.SelectedIndex == 1)
        //{
        //    exppr.Visible = true;
        //    dateprob.Text = "Date";
        //    exProDate.Visible = true;
        //}

        if (ddlreason.SelectedValue == "1")
        {
            // ExtendProbationDropDownList.SelectedValue = "1";
            // dateprob.Visible = false;
            exProDate.Visible = true;
            exppr.Visible = true;

        }
        if (ddlreason.SelectedValue == "2")
        {

            //  ExtendProbationDropDownList.SelectedValue = "1";
            //   dateprob.Visible = true;
            exProDate.Visible = true;
            exppr.Visible = true;
        }

        //if (RadioButtonList1.Items[0].Selected)
        //{
        //    exppr.Visible = true;
        //    probendreason.Visible = false;

        //}
    }
    public void GetProbData(string id)
    {
        DataTable dt = aProbationperiodDal.GetProbationMaster(id);
        if (dt.Rows.Count > 0)
        {
            GetEmpData(dt.Rows[0]["EmpInfoId"].ToString());

            ExtendProbationDropDownList.Items[1].Selected = Convert.ToBoolean(dt.Rows[0]["ExProbation"].ToString());
            ExtendProbationDropDownList.Items[2].Selected = Convert.ToBoolean(dt.Rows[0]["ProbationEnd"].ToString());

            try
            {
                exProDate.Text = Convert.ToDateTime(dt.Rows[0]["ExProDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {

                // throw;
            }
            if (ExtendProbationDropDownList.Items[1].Selected)
            {
                exppr.Visible = true;

            }
            else
            {
                exppr.Visible = false;
                probendreason.Visible = true;
                if (exProDate.Text != string.Empty)
                {
                    exppr.Visible = true;
                    ddlreason.SelectedIndex = 1;
                }
                else
                {
                    ddlreason.SelectedIndex = 0;
                }
            }

        }
    }
      protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public void VisibleGrid(string mid)
    {
        DataTable dtdata = aProbationperiodDal.GetProbationInfo(mid);
        if (dtdata.Rows.Count>0)
        {
            bool isselfapp = Convert.ToBoolean(dtdata.Rows[0]["IsSelfApp"].ToString());


            DataTable dtdataSupp = aProbationperiodDal.GetSuperViesd(dtdata.Rows[0]["EmpID"].ToString());

            int supervisorId = string.IsNullOrEmpty(dtdataSupp.Rows[0]["ReportingEmpId"].ToString())
                ? 0
                : Convert.ToInt32(dtdataSupp.Rows[0]["ReportingEmpId"].ToString());
            if ( supervisorId == Convert.ToInt32(Session["EmpInfoId"]))
            {
                Session["CheckReporting"] = "";
                Session["CheckReporting"] = "ReportingEmpId";
                evgrid.Visible = true;
                DataTable dtdataDetails = aProbationperiodDal.GetProbationInfoDetails(mid);
                for (int j = 0; j < gv_ProbationEvaluation.Rows.Count; j++)
                {
                    for (int i = 0; i < dtdataDetails.Rows.Count; i++)
                    {
                        if (((HiddenField)gv_ProbationEvaluation.Rows[j].FindControl("hdpkd")).Value == dtdataDetails.Rows[i]["ValueField"].ToString())
                        {

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsExcellent"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    0]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    0]
                                    .Selected = false;
                            }

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsGood"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    1]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    1]
                                    .Selected = false;
                            }

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsSatisfactory"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    2]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    2]
                                    .Selected = false;
                            }

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsNotSatisfactory"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    3]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    3]
                                    .Selected = false;
                            }

                        }
                    }





                }
            }
            else
            {
                Session["CheckReporting"] = "";
                evgrid.Visible = true;





                 

                            DataTable dtdataDetails = aProbationperiodDal.GetProbationInfoDetails(mid);


                //for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                //{

                //    for (int kk = 0; kk < dtdataDetails.Rows.Count; kk++)
                //    {
                //           if (Convert.ToBoolean(dtdataDetails.Rows[kk]["IsExcellent"].ToString()) == true)
                //            {
                //                ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[
                //                    0]
                //                    .Selected = true;
                //            }


                //           if (Convert.ToBoolean(dtdataDetails.Rows[kk]["IsGood"].ToString()) == true)
                //           {
                //               ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[
                //                   1]
                //                   .Selected = true;
                //           }
                             

                //    }
                //}

                for (int j = 0; j < gv_ProbationEvaluation.Rows.Count; j++)
                {
                    for (int i = 0; i < dtdataDetails.Rows.Count; i++)
                    {
                        if (((HiddenField)gv_ProbationEvaluation.Rows[j].FindControl("hdpkd")).Value==dtdataDetails.Rows[i]["ValueField"].ToString())
                        {

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsExcellent"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    0]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    0]
                                    .Selected = false;
                            }

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsGood"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    1]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    1]
                                    .Selected = false;
                            }

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsSatisfactory"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    2]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    2]
                                    .Selected = false;
                            }

                            if (Convert.ToBoolean(dtdataDetails.Rows[i]["IsNotSatisfactory"].ToString()) == true)
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    3]
                                    .Selected = true;
                            }
                            else
                            {
                                ((RadioButtonList)gv_ProbationEvaluation.Rows[j].FindControl("rad_RatingScale")).Items[
                                    3]
                                    .Selected = false;
                            }

                        }
                    }
                    

                    


                }



                for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                {


                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                        .Enabled = false;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                        .Enabled = false;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                         .Enabled = false;

                    ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                       .Enabled = false;
                }

            }
        }
    }









    public void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        DataTable dtdata = null;
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
            dtdata = aProbationperiodDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = aProbationperiodDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = aEmployeeRequsitionDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["EmpInfoId"].ToString() != Session["ForEmpInfoId"].ToString())



        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
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
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();

        if (actionstatusHiddenField.Value == "Approved")
        {
            submitButton.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            submitButton.Visible = true;
            Button1.Visible = false;
        }
    }

    public bool SelectValidation()
    {
        for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
        {
            int a = 0;
            for (int j = 0; j < ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items.Count; j++)
            {
                if (((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[j].Selected==false)
                {
                    a++;
                }
            }

            if (a == ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items.Count)
            {
                ShowMessageBox("Please evaluate !!");
                return false;
            }
        }

        return true;
    }
    //public bool SelectValidation2()
    //{
    //    actionRadioButtonList. = true;
      
    //}
    public void SaveProbationDetail(int id)
    {

        aProbationperiodDal.DeleteEvalution(Convert.ToInt32(masterHiddenField.Value)); 
        for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
        {
            ProbationEvaluationDetailsDAO aProbationEvaluationDetailsDao = new ProbationEvaluationDetailsDAO();

            aProbationEvaluationDetailsDao.ProbationEvaluationMasterId = id;
            aProbationEvaluationDetailsDao.ValueField =
                Convert.ToInt32(((HiddenField)gv_ProbationEvaluation.Rows[i].FindControl("hdpkd")).Value);
            aProbationEvaluationDetailsDao.KeyRatingCri =
                ((Label)gv_ProbationEvaluation.Rows[i].FindControl("txt_RatingCriterions")).Text;
            aProbationEvaluationDetailsDao.IsExcellent =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                    .Selected;
            aProbationEvaluationDetailsDao.IsGood =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                    .Selected;
            aProbationEvaluationDetailsDao.IsSatisfactory =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                    .Selected;
            aProbationEvaluationDetailsDao.IsNotSatisfactory =
                ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                    .Selected;

            int ida = aProbationperiodDal.SaveProbationDetail(aProbationEvaluationDetailsDao);
        }
    }
    protected void Button2_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {


            ProbationEvaluationMasterDAO aMaster = new ProbationEvaluationMasterDAO();
            aMaster.ProbationEvaluationMasterId
                = Convert.ToInt32(masterHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aProbationperiodDal.UpdateContractural(aMaster);
            if (status)
            {
                int commentid = aProbationperiodDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = aProbationperiodDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                    appLogDao = new ProbationEvaluationAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    if (appLogDao.ForEmpInfoId !=0)
                    {
                        int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);
                        aProbationperiodDal.UpdateJobReqStatus2(aMaster);    
                    }
                    else
                    {
                        ShowMessageBox("Set supervisor for employee");
                    }

                    


                }
                else if (aMaster.ActionStatus == "Approved")
                {

                    int empid = 0;
                    bool isselfapp = false;
                    DataTable dtdatainfo =
                        aProbationperiodDal.GetContractualDataInfo(aMaster.ProbationEvaluationMasterId.ToString());
                    if (dtdatainfo.Rows.Count > 0)
                    {
                        isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                    }

                    if (isselfapp)
                    {


                        DataTable dtempdata =
                            aProbationperiodDal.GetHRAdminEmployeeAppId(" WHERE URL='" +
                                                                             Session["AppPage"].ToString() +
                                                                             "' AND Serial='1' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                             Session["CompanyId"].ToString() + "'");
                        if (dtempdata.Rows.Count > 0)
                        {
                            empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        }
                        ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        //aMaster.ActionStatus = "Verified";
                        //aContractualEmpManageDAL.UpdateContractural(aMaster);
                        aProbationperiodDal.UpdateJobReqStatus2(aMaster);

                        int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Probation Period Approval ", @"  <br/> Dear Sir, <br/>
A Probation Period Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/>   http://182.160.103.234:8088/ <br/>
Thank You.
");
                    }
                    else
                    {
                        empid = Convert.ToInt32(dtdatainfo.Rows[0]["ReportingEmpId"].ToString());
                        ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        aMaster.ActionStatus = "Verified";
                        aProbationperiodDal.UpdateContractural(aMaster);
                        aProbationperiodDal.UpdateJobReqStatus2(aMaster);
                        aProbationperiodDal.UpdateSelfApprove(aMaster.ProbationEvaluationMasterId, true);

                        int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);




                    }
                    //ContractualEmpManageAppLogDAO appLogDao = new ContractualEmpManageAppLogDAO();
                    //{
                    //    appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                    //    appLogDao.ApproveDate = DateTime.Now;
                    //    appLogDao.ApproveBy = Session["UserId"].ToString();
                    //    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    //    appLogDao.ForEmpInfoId = empid;
                    //    appLogDao.ContractualEmpManageId = aMaster.ContractualEmpManageId;
                    //    appLogDao.Comments = commentsTextBox.Text;
                    //    appLogDao.CommentsId = commentid;
                    //};
                    //aMaster.ActionStatus = "Verified";
                    //aContractualEmpManageDAL.UpdateContractural(aMaster);
                    //aContractualEmpManageDAL.UpdateJobReqStatus2(aMaster);

                    //int id = aContractualEmpManageDAL.SaveEmpContractAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = aProbationperiodDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), masterHiddenField.Value);
                    DataTable dtempdata2 = aProbationperiodDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), masterHiddenField.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        string preact = dtempdata2.Rows[0]["ActionStatus"].ToString();
                        if (preact=="Approved")
                        {
                            aProbationperiodDal.UpdateSelfApprove(aMaster.ProbationEvaluationMasterId, false);
                        }

                        aProbationperiodDal.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                        int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);
                        aProbationperiodDal.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {
                        
                        
                        {
                            ShowMessageBox("Please select Approval Status Approved  this!!!");    
                        }
                        
                    }
                    DataTable dtdata = aProbationperiodDal.GetDataReviewEntryBy(
                        masterHiddenField.Value, Session["UserId"].ToString(), "Review");
                    if (dtdata.Rows.Count>0)
                    {
                        Session["ProbEdit"] = aMaster.ProbationEvaluationMasterId.ToString();
                        Response.Redirect("ProbationEvaluationForm.aspx?id="+aMaster.ProbationEvaluationMasterId.ToString());
                    }


                }
                if (Session["CheckReporting"] == "ReportingEmpId")
                {
                    SaveProbationDetail(aMaster.ProbationEvaluationMasterId);
                }
                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Operation Successful...');window.location ='ProbationListApproval.aspx';",
                           true);
            }
        
        }
    }

    
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {

            ProbationEvaluationMasterDAO aMaster = new ProbationEvaluationMasterDAO();
            aMaster.ProbationEvaluationMasterId
                = Convert.ToInt32(masterHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aProbationperiodDal.UpdateJobReqStatus2(aMaster);
            if (status)
            {
                int commentid = aProbationperiodDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        aProbationperiodDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                         "' AND EmpInfoId='" +
                                                                         Session["EmpInfoId"].ToString() +
                                                                         "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                         Session["CompanyId"].ToString() + "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        aProbationperiodDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                         "' AND Serial='" + serial +
                                                                         "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                         Session["CompanyId"].ToString() + "' ");
                    //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    }
                    ;
                    if (appLogDao.ForEmpInfoId!=0)
                    {
                        int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);    
                    }
                    else
                    {
                        ShowMessageBox("Set Supervisor for this employee");
                    }
                    

                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                   
                    ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    }
                    ;

                            
                              



                                //empGenId = Convert.ToInt32(hfEmpInfoId.Value);


                                //date = Convert.ToDateTime(exProDate.Text.Trim()).ToString();


                                //UpdateEmployeeProbitionExtendeddate(empGenId, date);



                                if (exProDate.Text != string.Empty)
            {
                //aEvaluationMasterDao.ExProDate = Convert.ToDateTime(exProDate.Text);
                if (ExtendProbationDropDownList.SelectedValue == "1")
                {
                    
                        Int32 empGenId = 0;
                        string date = "";



                        empGenId = Convert.ToInt32(hfEmpInfoId.Value);


                        date = Convert.ToDateTime(exProDate.Text.Trim()).ToString();


                        UpdateEmployeeProbitionExtendeddate(empGenId, date);
                   
                }



                if (ddlreason.SelectedValue == "2")
                {
                     


                    if (ExtendProbationDropDownList.SelectedValue == "2")
                    {
                         
                            Int32 empGenId = 0;
                            string date = "";
                            bool Isconfirm = true;


                            empGenId = Convert.ToInt32(hfEmpInfoId.Value);


                            date = Convert.ToDateTime(exProDate.Text.Trim()).ToString();


                            UpdateEmployeeConfirmationExtendeddate(empGenId, date, Isconfirm);
                       
                    }

                }

                if (ddlreason.SelectedValue == "1")
                {



                    if (ExtendProbationDropDownList.SelectedValue == "2")
                    {
                        

                            EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();

                           


                            EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO = new EmployeeJobLeftEntryDAO();

                            aEmployeeJobLeftEntryDAO.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                            aEmployeeJobLeftEntryDAO.EmployeeId = Convert.ToInt32(hfEmpInfoId.Value);

                            aEmployeeJobLeftEntryDAO.JobLeftTypeId = Convert.ToInt32(10);

                    

                            aEmployeeJobLeftEntryDAO.IsClearanceForm = false;




                            aEmployeeJobLeftEntryDAO.IsExitInterview = false;


                            aEmployeeJobLeftEntryDAO.JobLeftDate = Convert.ToDateTime(exProDate.Text);
                            aEmployeeJobLeftEntryDAO.Reason = "";


                            aEmployeeJobLeftEntryDAO.SubmissionDate = null;





                            aEmployeeJobLeftEntryDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
                            aEmployeeJobLeftEntryDAO.EntryDate = DateTime.Now;
                            aEmployeeJobLeftEntryDAO.AutoProcess = "Separation From probation period";

                            aEmployeeJobLeftEntryDAL.EmployeePromotionEntrySaveInfo(aEmployeeJobLeftEntryDAO);
                            Int32 empGenId = 0;
                            bool IsProbistion = true;


                            empGenId = Convert.ToInt32(hfEmpInfoId.Value);

                            IsProbistion = false;



                            UpdateEmployeeStepId(empGenId, "End of probation period");
                         
                    }

                }
               
            }
                              

                    int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Probation Period Approval ",
                        @"  <br/> Dear Sir, <br/>
A Probation Period Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/>   http://182.160.103.234:8088/ <br/> Thank you.
");
                    

                  
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    
                    string actionst = "";
                    DataTable dtempdata = aProbationperiodDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), masterHiddenField.Value);
                    if (dtempdata.Rows.Count > 0)
                    {
                        actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata2 = aProbationperiodDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), masterHiddenField.Value);
                    int a = 0;
                    for (int i = 0; i < dtempdata2.Rows.Count; i++)
                    {
                        if (dtempdata.Rows[i]["PreEmpInfoId"].ToString() != dtempdata.Rows[i]["ForEmpInfoId"].ToString())
                        {
                            a = i;
                            break;
                        }
                    }
                    if (dtempdata2.Rows.Count > 0)
                    {
                        ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                        {
                            //appLogDao.ActionStatus = "Verified";
                            appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                            appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        if (actionst == "Approved")
                        {
                            aMaster.ActionStatus = "Verified";
                            aProbationperiodDal.UpdateContractural(aMaster);
                        }
                        aProbationperiodDal.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                        aProbationperiodDal.UpdateContactAppLog("Review", dtempdata2.Rows[a][0].ToString());
                        int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }


                if (Session["CheckReporting"] == "ReportingEmpId")
                {
                    SaveProbationDetail(aMaster.ProbationEvaluationMasterId);
                }
                Session["AppLogId"] = null;

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='ProbationListApproval.aspx';",
                   true);
            }
        }
    }

    private void UpdateEmployeeStepId(int empGenId, string reason)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.InactiveReason = reason;
        aInfo.IsActive = false;
        aInfo.EmployeeStatus = "InActive";
        aInfo.EmpInfoId = empGenId;

        aProbationperiodDal.UpdateEmployeeExitInfo(aInfo);

    }
  
    private void UpdateEmployeeConfirmationExtendeddate(int empGenId, string ExtensionToDate, bool Isconfirm)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ConfirmationDate = Convert.ToDateTime(ExtensionToDate.ToString());

        aInfo.EmpInfoId = empGenId;
        aInfo.ConformationStatus = Isconfirm;

        aProbationperiodDal.UpdateEmployeeConfirmationdateInfo(aInfo);

    }
    private void UpdateEmployeeProbitionExtendeddate(int empGenId, string ExtensionToDate)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ExtProbationPeriodDate = Convert.ToDateTime(ExtensionToDate.ToString());

        aInfo.EmpInfoId = empGenId;

        aProbationperiodDal.UpdateEmployeeProbitionExtendeddateInfo(aInfo);

    }

    private void UpdateEmployeeSeparationExtendeddate(int empGenId, bool IsProbition)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.IsProbationary = IsProbition;

        aInfo.EmpInfoId = empGenId;

        aProbationperiodDal.UpdateEmployeeSeparationInfo(aInfo);

    }
    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (dynamic)null;
            if (forEmpID > 0)
            {
                GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();
            }
            else
            {
                int EntryEmpID = Convert.ToInt32(entryempinfoIdHiddenField.Value);


                GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == EntryEmpID
                                  select t).FirstOrDefault();
            }


            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }

        }

        if (ForMailAddress != "")
        {
            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();




            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Session["EmailID"].ToString());
            try
            {
                mail.To.Add(ForMailAddress.Trim());
            }
            catch (Exception)
            {
                //throw;
            }
            mail.Subject = mSubject;
            mail.Body =
                "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                WebUtility.HtmlDecode(mBody)
                +
                "</div>";

            //Attach file using FileUpload Control and put the file in memory stream

            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(),
                Session["AppPass"].ToString());
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(mail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {

            }
            catch (Exception exe)
            {

            }


            System.Threading.Thread.Sleep(100);
        }



    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        ProbationEvaluationMasterDAO aMaster = new ProbationEvaluationMasterDAO();
        aMaster.ProbationEvaluationMasterId
            = Convert.ToInt32(masterHiddenField.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = aProbationperiodDal.UpdateProbationEvuMaster(aMaster);
        int commentid = aProbationperiodDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aProbationperiodDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);
            aProbationperiodDal.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='ProbationListApproval.aspx';",
                   true);
    }

    private void LoadDropDownList()
    {
        _surveyCommonDal.LoadCompanyDropDownList(ddlCompany);

    }

    public void GetEmpData(string id)
    {
        DataTable dtempdata = aProbationperiodDal.LoadEmployeeInfo(id);
        if (dtempdata.Rows.Count>0)
        {
            empIdHiddenField.Value = dtempdata.Rows[0]["EmpInfoId"].ToString();
            empCode.Text = dtempdata.Rows[0]["EmpMasterCode"].ToString();
            empName.Text = dtempdata.Rows[0]["EmpName"].ToString();
            ddlCompany.SelectedValue = dtempdata.Rows[0]["CompanyId"].ToString();

            LoadGrid(dtempdata.Rows[0]["CompanyId"].ToString());
            try
            {
                dtJoining.Text = Convert.ToDateTime(dtempdata.Rows[0]["DateOfJoin"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {
                
                
            }
            deptNameLabel.Text = dtempdata.Rows[0]["DepartmentName"].ToString().Trim();
            ddlDivision.Text = dtempdata.Rows[0]["DivisionName"].ToString();
            ddlDesignation.Text = dtempdata.Rows[0]["Designation"].ToString();
            LocationLabel.Text = dtempdata.Rows[0]["Location"].ToString();
            lblPlace.Text = dtempdata.Rows[0]["SalaryLocation"].ToString();

            ReportingLabel.Text = dtempdata.Rows[0]["ReportingToName"].ToString();
        }
    }
    private void LoadGrid(string id)
    {
        using (DataTable dt = _surveyCommonDal.GetProbationEvaluationRatingByCompanyId(id))
        {
            gv_ProbationEvaluation.DataSource = dt;
            gv_ProbationEvaluation.DataBind();
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            txt_EmpName.Enabled = true;

            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;
        }
        else
        {
            Session["CompanyId"] = "";
            txt_EmpName.Enabled = false;
        }
    }

    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {
        SetEmployeeInfo();

        if (hfEmpInfoId.Value != "")
        {
            DataTable aTable = _surveyCommonDal.LoadEmployeeInfo(hfEmpInfoId.Value, ddlCompany.SelectedValue);

            if (aTable.Rows.Count > 0)
            {
                ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

                ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);

                empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                empName.Text = aTable.Rows[0].Field<string>("EmpName");

                dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");

            }
            else
            {
                txt_EmpName.Text = "";
                ShowMessageBox("No Information found !!!");
            }
        }
    }


    private void SetEmployeeInfo()
    {
        string empName = txt_EmpName.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            hfEmpInfoId.Value = emp[0];
        }
        else
        {
            hfEmpInfoId.Value = "";
             ShowMessageBox("Input Correct Data !!");
        }

        txt_EmpName.Text = "";
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {
        //DataTable dtexist = aProbationperiodDal.GetPreEmpData(empIdHiddenField.Value);
        //if (dtexist.Rows.Count>0)
        //{
        //    ShowMessageBox("Already Exist !!");
        //    return false;
        //}
        if (Session["CheckReporting"] == "ReportingEmpId")
        {
            if (SelectValidation() == false)
            {
                return false;
            }
        }
        
        return true;
    }
    //public void RadioTextValue()
    //{
    //    //string filepath = Path.GetDirectoryName(Request.Path);
    //    //filepath = filepath.TrimStart('\\');
    //    //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
    //    string filepath = "";
    //    if (Session["PMAppPage"] != null)
    //    {
    //        filepath = Session["PMAppPage"].ToString();
    //    }

    //    DataTable dtdata = aProbationperiodDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

    //    DataTable aDataTable = new DataTable();
    //    aDataTable.Columns.Add("Value");
    //    aDataTable.Columns.Add("Text");

    //    DataRow dataRow = null;



    //    if (dtdata.Rows.Count > 0)
    //    {
    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Approved";
    //        dataRow["Value"] = "Approved";
    //        aDataTable.Rows.Add(dataRow);

    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Review";
    //        dataRow["Value"] = "Review";
    //        aDataTable.Rows.Add(dataRow);

    //    }
    //    else
    //    {
    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Approved";
    //        dataRow["Value"] = "Verified";
    //        aDataTable.Rows.Add(dataRow);

    //        dataRow = aDataTable.NewRow();
    //        dataRow["Text"] = "Review";
    //        dataRow["Value"] = "Review";
    //        aDataTable.Rows.Add(dataRow);
    //    }

    //    actionRadioButtonList.DataValueField = "Value";
    //    actionRadioButtonList.DataTextField = "Text";
    //    actionRadioButtonList.DataSource = aDataTable;
    //    actionRadioButtonList.DataBind();
    //}

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        ProbationEvaluationMasterDAO aMaster = new ProbationEvaluationMasterDAO();
        aMaster.ProbationEvaluationMasterId = Convert.ToInt32(masterHiddenField.Value);
        aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
        bool status = aProbationperiodDal.UpdateProbationEvuMaster(aMaster);
        if (status)
        {
            if (aMaster.ActionStatus == "Verified")
            {
                DataTable dtempdata = aProbationperiodDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO()
                {
                    ActionStatus = actionRadioButtonList.SelectedValue,
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                    ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                    ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId,
                    Comments = commentsTextBox.Text,

                };
                int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);
            }
            else if (aMaster.ActionStatus == "Approved")
            {
                //DataTable dtempdata = _jdDal.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO()
                {
                    ActionStatus = actionRadioButtonList.SelectedValue,
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                    ForEmpInfoId = 0,
                    ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId,
                    Comments = commentsTextBox.Text,

                };
                int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);
                DataTable dtdata = aProbationperiodDal.GetProbationInfo(appLogDao.ProbationEvaluationMasterId.ToString());
                if (dtdata.Rows.Count>0)
                {
                    if (dtdata.Rows[0]["ExProbation"].ToString() == "1")
                    {
                        aProbationperiodDal.UpdateProbationEmp(dtdata.Rows[0]["EmpInfoId"].ToString(), dtdata.Rows[0]["ExProDate"].ToString());
                      //  aProbationperiodDal.SaveEmpProbHistory(dtdata.Rows[0]["EmpInfoId"].ToString());
                    }
                    
                }
            }
            else if (aMaster.ActionStatus == "Review")
            {
                DataTable dtempdata = aProbationperiodDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), masterHiddenField.Value);
                DataTable dtempdata2 = aProbationperiodDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), masterHiddenField.Value);
                ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO()
                {
                    ActionStatus = "Verified",
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                    ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                    ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId,
                    Comments = commentsTextBox.Text,

                };
                aProbationperiodDal.UpdateJDAppLog("Review", Session["JDAppLogId"].ToString());
                int id = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);
            }


        }
        Session["JDAppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='ProbationListApproval.aspx';",
                   true);
        
    }

    private void Clear()
    {
        ddlCompany.SelectedValue = "";
        hfEmpInfoId.Value = "";
        empCode.Text = "";
        empName.Text = "";
        dtJoining.Text = "";
        ddlDivision.Text = "";
        ddlDesignation.Text = "";
        //txt_ProbitionFrom.Text = "";
        //txt_ProbitionTo.Text = "";
        //txt_DueDateOfConfirmation.Text = "";
        txt_SupervisorObservation.Text = "";
            txt_DepartmentHeadObservation.Text = "";
            txt_DivisionHeadObservation.Text = "";
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void RadioButtonList1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.Items[0].Selected)
        {
            exppr.Visible = true;

        }
        else
        {
            exppr.Visible = false;
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ProbationListApproval.aspx");
    }
}