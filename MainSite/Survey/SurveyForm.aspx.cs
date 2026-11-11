using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Survey;
using DAO.HRIS_DAO_EF;

public partial class Survey_SurveyForm : System.Web.UI.Page
{
    private SurveyCommonDAL _surveyCommonDal = new SurveyCommonDAL();
    private int mid = 0;
    private int EmpID = 0;
    private string _userId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["Mid"]))
            {
                mid = int.Parse(Request.QueryString["Mid"]);
                hdMasID.Value = mid.ToString();
                EmpID = int.Parse(Request.QueryString["EmpID"]);
                hdEmpId.Value = EmpID.ToString();

                using (DataTable dt = _surveyCommonDal.GetQuestionGroupForSurveyForm(mid, EmpID))
                {
                    gv_Menu.DataSource = dt;
                    gv_Menu.DataBind();

                    using (var db = new HRIS_SMC_DBEntities())
                    {
                     var   emp = (from j in db.tblSurveyMasters where j.SurveyMasterId == mid select j).FirstOrDefault();

                        emp.SurveyName = lblSurveyName.Text;
                    }
                }



            }
            //LoadInitialDDL();
            LoadInitialGrid();
        }

    }

    private void LoadInitialGrid()
    {
       
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        tblSurveySubmitMaster mas=null;
        using (var db = new HRIS_SMC_DBEntities())
        {
            //if (mid > 0)
            //{
            //    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
            //}


            //else
            {
                mas = new tblSurveySubmitMaster();


                mas.EmployeeId = Convert.ToInt32(hdEmpId.Value);
                mas.SurveyID = Convert.ToInt32(hdMasID.Value);
                mas.SurveyName = lblSurveyName.Text;
                mas.EntryBy = Convert.ToInt16(_userId);
                mas.EntryDate = DateTime.Now;
                db.tblSurveySubmitMasters.Add(mas);
                db.SaveChanges();


 

                if (gv_Menu.Rows.Count > 0)
                {
                    for (int i = 0; i < gv_Menu.Rows.Count; i++)
                    {
                      //  HiddenField hfSurveyQuestionGroupId = (HiddenField)gv_Menu.Rows[i].FindControl("hfSurveyQuestionGroupId");
                        Label txt_SurveyQuestionGroup = (Label)gv_Menu.Rows[i].FindControl("txt_SurveyQuestionGroup");


                        GridView gv_Child =
                                          ((GridView)gv_Menu.Rows[i].Cells[2].FindControl("gv_Child"));

                        for (int j = 0; j< gv_Child.Rows.Count;j++)
                        {
                            HiddenField hdQuestionGroupId = ((HiddenField)gv_Child.Rows[j].FindControl("hdQuestionGroupId"));
                            HiddenField SurveyQuestionTypeId = ((HiddenField)gv_Child.Rows[j].FindControl("SurveyQuestionTypeId"));
                            RadioButtonList radSingleAns = ((RadioButtonList)gv_Child.Rows[j].FindControl("radSingleAns"));
                            TextBox txtLongAns = ((TextBox)gv_Child.Rows[j].FindControl("txtLongAns"));
                            Label txt_QuestionTitle = ((Label)gv_Child.Rows[j].FindControl("txt_QuestionTitle"));
                            HiddenField QTuesId = ((HiddenField)gv_Child.Rows[j].FindControl("hdSurveyQuestionIdN"));


                            tblSurveySubmitDetail dtls = new tblSurveySubmitDetail();

                            dtls.SurveySubmitMasterId = mas.SurveySubmitMasterId;

                            dtls.QuestionTypeId = Convert.ToInt32(SurveyQuestionTypeId.Value);

                            dtls.QuestionGroupId = Convert.ToInt32(hdQuestionGroupId.Value);
                            dtls.QuestionGroupName = txt_SurveyQuestionGroup.Text;


                            dtls.QuestionNameId = Convert.ToInt32(QTuesId.Value);
                            dtls.QuestionName = txt_QuestionTitle.Text;

                            if (SurveyQuestionTypeId.Value == "1")
                            {
                               
                                dtls.Option01Id =  string.IsNullOrEmpty(radSingleAns.Items[0].Value) ? (int?)null : int.Parse(radSingleAns.Items[0].Value);
                                dtls.Option02Id = string.IsNullOrEmpty(radSingleAns.Items[1].Value) ? (int?)null : int.Parse(radSingleAns.Items[1].Value);
                                dtls.Option03Id = string.IsNullOrEmpty(radSingleAns.Items[2].Value) ? (int?)null : int.Parse(radSingleAns.Items[2].Value);
                                dtls.Option04Id = string.IsNullOrEmpty(radSingleAns.Items[3].Value) ? (int?)null : int.Parse(radSingleAns.Items[3].Value);


                                dtls.Option01Name = string.IsNullOrEmpty(radSingleAns.Items[0].Text)  ? null : radSingleAns.Items[0].Text;
                                dtls.Option02Name = string.IsNullOrEmpty(radSingleAns.Items[1].Text) ? null : radSingleAns.Items[1].Text;
                                dtls.Option03Name = string.IsNullOrEmpty(radSingleAns.Items[2].Text) ? null : radSingleAns.Items[2].Text;
                                dtls.Option04Name = string.IsNullOrEmpty(radSingleAns.Items[3].Text) ? null : radSingleAns.Items[3].Text;

                                if (radSingleAns.Items[0].Selected == true)
                                {
                                    dtls.OptionAns = Convert.ToString(radSingleAns.Items[0].Text);
                                    dtls.OptionAnsId = Convert.ToInt32(radSingleAns.Items[0].Value);
                                }


                                if (radSingleAns.Items[1].Selected == true)
                                {
                                    dtls.OptionAns = Convert.ToString(radSingleAns.Items[1].Text);
                                    dtls.OptionAnsId = Convert.ToInt32(radSingleAns.Items[1].Value);
                                }

                                if (radSingleAns.Items[2].Selected == true)
                                {
                                    dtls.OptionAns = Convert.ToString(radSingleAns.Items[2].Text);
                                    dtls.OptionAnsId = Convert.ToInt32(radSingleAns.Items[2].Value);
                                }

                                if (radSingleAns.Items[3].Selected == true)
                                {
                                    dtls.OptionAns = Convert.ToString(radSingleAns.Items[3].Text);
                                    dtls.OptionAnsId = Convert.ToInt32(radSingleAns.Items[3].Value);
                                }
                            }
                            else
                            {
                                dtls.Description = Convert.ToString(txtLongAns.Text);
                            }






                            db.tblSurveySubmitDetails.Add(dtls);

                            db.SaveChanges();
                        }


                  




                    }

                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                             "alert",
                             "alert('Data Submitted Successfully...');window.location ='SurveyDeclaretionListForEmployee.aspx';",
                             true);
            }

        }



    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SurveyForm.aspx");
    }

    protected void gv_Menu_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hfSurveyQuestionGroupId = (HiddenField)e.Row.FindControl("hfSurveyQuestionGroupId");

            GridView gv_Child = (GridView)e.Row.FindControl("gv_Child");

            int SurveyQuestionGroupId = int.Parse(hfSurveyQuestionGroupId.Value);
            using (DataTable dt = _surveyCommonDal.GetQuestionByGroupId(SurveyQuestionGroupId))
            {
                gv_Child.DataSource = dt;
                gv_Child.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int SurveyQuestionTypeId = int.Parse(dt.Rows[i]["SurveyQuestionTypeId"].ToString());
                    int SurveyQuestionId = int.Parse(dt.Rows[i]["SurveyQuestionId"].ToString());

                    if (SurveyQuestionTypeId==1)
                    {
                        RadioButtonList radSingleAns = (RadioButtonList)gv_Child.Rows[i].FindControl("radSingleAns");
                        radSingleAns.Visible = true;

                        DataTable dtgrade = _surveyCommonDal.GetAnsByGroupId(SurveyQuestionId, SurveyQuestionGroupId);
                        radSingleAns.DataValueField = "SurveyQuestionAnswerId";
                        radSingleAns.DataTextField = "SurveyQuestionAnswer";
                        radSingleAns.DataSource = dtgrade;
                        radSingleAns.DataBind();
 
                         
                    }

                 

                    if (SurveyQuestionTypeId == 4)
                    {
                        TextBox txtLongAns = (TextBox)gv_Child.Rows[i].FindControl("txtLongAns");
                        txtLongAns.Visible = true;
                    }
                }
            }
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SurveyDeclaretionListForEmployee");
    }
}