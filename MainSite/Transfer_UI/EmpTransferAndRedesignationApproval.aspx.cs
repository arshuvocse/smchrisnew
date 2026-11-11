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
using CrystalDecisions.Shared.Json;
using DAL.COMMON_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;
using DAL.UserPermissions_DAL;

public partial class Transfer_UI_EmpTransferAndRedesignationApproval : System.Web.UI.Page
{
    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {

        TransferRadioButtonList.Items[1].Attributes.Add("hidden", "hidden");
        TransferRadioButtonList.Items[2].Attributes.Add("hidden", "hidden");
       

        if (!IsPostBack)
        {
            ButtonVisible();
            EfectiveDate.Attributes.Add("readonly","readonly");


            //var bli1 = TransferRadioButtonList.Items[1];
            TransferRadioButtonList.Items[1].Attributes.Add("hidden", "hidden");
            TransferRadioButtonList.Items[2].Attributes.Add("hidden", "hidden");

          


            

           
            LoadDropDownList();


            if (Session["EmpTransferAndRedesignationId"] != null)
            {
                EmpTransferAndRedesignationIdHiddenField.Value = Session["EmpTransferAndRedesignationId"].ToString();
                GetOneRecord(EmpTransferAndRedesignationIdHiddenField.Value.ToString());
                Session["EmpTransferAndRedesignationId"] = null;
              
            }
            else
            {
                Response.Redirect("EmpTransferAndRedesignationApprovalList.aspx");
            }
            if (Session["PromotionToTransfer"]!=null)
            {
                companyDropDownList.SelectedValue = Session["comID"].ToString();
                companyDropDownList_OnSelectedIndexChanged(null, null);
                FinancialYearDropDownList.SelectedValue = Session["FinYear"].ToString();
                EfectiveDate.Text = Session["EffDate"].ToString();
                repEmpIdHiddenField.Value = Session["EmpId"].ToString();


                LoadDataNew(Convert.ToInt32(repEmpIdHiddenField.Value));
           //     Session["PromotionToTransfer"] = null;

            }
        }

        
    }


    protected void Button10_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {

            EmpTransferAndRedesignationDao aMaster = new EmpTransferAndRedesignationDao();
            aMaster.EmpTransferAndRedesignationId
                = Convert.ToInt32(EmpTransferAndRedesignationIdHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aEmpTransferAndRedesignationDal.UpdateJobReqStatus2(aMaster);
            if (status)
            {
                int commentid = aEmpTransferAndRedesignationDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        aEmpTransferAndRedesignationDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +

                  "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                       "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        aEmpTransferAndRedesignationDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND Serial='" + serial + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    //DataTable dtempdata = aEmpTransferAndRedesignationDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    //DataTable dtempdata = aEmpTransferAndRedesignationDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //if (dtempdata.Rows.Count>0)
                    //{
                    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //}
                    EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };


                    int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Transfer Approval ", @"  <br/> Dear Sir, <br/>
A Employee Transfer is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    string actionst = "";
                    DataTable dtempdata = aEmpTransferAndRedesignationDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), EmpTransferAndRedesignationIdHiddenField.Value);
                    if (dtempdata.Rows.Count > 0)
                    {
                        actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata2 = aEmpTransferAndRedesignationDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), EmpTransferAndRedesignationIdHiddenField.Value);
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
                        EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
                        {
                            //appLogDao.ActionStatus = "Verified";
                            appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                            appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        if (actionst == "Approved")
                        {
                            aMaster.ActionStatus = "Verified";
                            aEmpTransferAndRedesignationDal.UpdateContractural(aMaster);
                        }
                        aEmpTransferAndRedesignationDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        aEmpTransferAndRedesignationDal.UpdateAppLog("Review", dtempdata2.Rows[a][0].ToString());
                        int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }

                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Operation Successfully Done...');window.location ='EmpTransferAndRedesignationApprovalList.aspx';",
                           true);
            }
       
        }
    }

    public void ButtonVisible()
    {
       

    }

    private void GetOneRecord(string EmpTranRedesId)
    {


        DataTable aDataTable = aEmpTransferAndRedesignationDal.GetEmployeePromotionEntryIdByIdApp(EmpTranRedesId);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {

            bool isselfapp = Convert.ToBoolean(aDataTable.Rows[0]["IsSelfApp"].ToString());
            if (isselfapp)
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["EmpInfoId"].ToString();
            }
            else
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["UserEmpInfoId"].ToString();
            }


            lblRemarks.Text = aDataTable.Rows[0].Field<String>("Remarks").ToString();
            actionstatusHiddenField.Value = aDataTable.Rows[0].Field<String>("ActionStatus").ToString();
            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();
            lblCompany.Text = companyDropDownList.SelectedItem.Text;
            aEmpTransferAndRedesignationDal.EmployeeNameDropDown(OldReportingBodyDropDownList, companyDropDownList.SelectedValue);
            aEmpTransferAndRedesignationDal.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
            FinancialYearDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("FinancialYearId").ToString();
            lblFinancialYearDesc.Text = FinancialYearDropDownList.SelectedItem.Text;
            DataTable dtdata = aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationDS(
                    EmpTransferAndRedesignationIdHiddenField.Value);
            loadGridView.DataSource = dtdata;
            loadGridView.DataBind();

            repEmpIdHiddenField.Value = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeId").ToString(CultureInfo.InvariantCulture);


            if (aDataTable.Rows[0]["AutoProcess"] != null)
            {
                manualUpdateCheckBox.Checked = true;
            }

            manualUpdateCheckBox.Enabled = false;

           

            DataTable EmpDataTable = aEmpTransferAndRedesignationDal.GetEmpSalaryGradeById(repEmpIdHiddenField.Value);
            try
            {
                string idddd = "";
                idddd = EmpDataTable.Rows[0]["SalaryGradeId"].ToString();

                aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryIdEdit(NewdesignationDropDownList, idddd);
            }
            catch (Exception)
            {
                
                //throw;
            }
            EmployeeNameTextBox.Text = aDataTable.Rows[0]["SearchEmptxt"].ToString();
            SearchEmployeeNameTextBoxTextBox.Text = aDataTable.Rows[0]["SearchEmptxt"].ToString();

            lblEmp.Text = SearchEmployeeNameTextBoxTextBox.Text;

            txtEmpId.Text = aDataTable.Rows[0]["EmployeeCode"].ToString();
            DesignationTextBox.Text = aDataTable.Rows[0]["Designation"].ToString();
            JoiningDateTextBox.Text = Convert.ToDateTime(aDataTable.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


            //int SalaryGradeId = 0;

            //DataTable dtdataa = new DataTable();
            //dtdataa = aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryIdDtab(NewSalaryGradeDropDownList, (dtdataa.Rows[0]["SalaryGradeId"].ToString()));

            SalaryGradeTextBox.Text = aDataTable.Rows[0]["GradeName"].ToString();


            OldCompanyDropDownList.SelectedValue = aDataTable.Rows[0]["CompanyId"].ToString();

            if (OldCompanyDropDownList.SelectedValue!="")
            {
                lblEComName.Text = OldCompanyDropDownList.SelectedItem.Text;
                
            }
           
            if (aDataTable.Rows[0]["SalaryLoationId"] != DBNull.Value)
            {
                OldSalaryLocationDropDownList.SelectedValue = aDataTable.Rows[0]["SalaryLoationId"].ToString();

                if (OldSalaryLocationDropDownList.SelectedValue!="")
                {
                    lblEOffice.Text = OldSalaryLocationDropDownList.SelectedItem.Text;
                }
             //   aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(OldJobLocationDropDownList, OldSalaryLocationDropDownList.SelectedValue);
            }

          
            try
            {
                
            }
            catch (Exception)
            {
                
                //throw;
            }
            //if (aDataTable.Rows[0]["JobLocationId"] != DBNull.Value)
            //{
            //    OldJobLocationDropDownList.SelectedValue = aDataTable.Rows[0]["JobLocationId"].ToString();
            //}

            if (aDataTable.Rows[0]["DivisionId"] != DBNull.Value)
            {
                OldDivisionDropDownList.SelectedValue = aDataTable.Rows[0]["DivisionId"].ToString();
                lblEDivision.Text = OldDivisionDropDownList.SelectedItem.Text;
            }

            if (aDataTable.Rows[0]["DivisionWId"] != DBNull.Value)
            {
                OldUnitDropDownList.SelectedValue = aDataTable.Rows[0]["DivisionWId"].ToString();
                lblEWing.Text = OldUnitDropDownList.SelectedItem.Text;
            }

            if (aDataTable.Rows[0]["SectionId"] != DBNull.Value)
            {
                OldSectionDropDownList.SelectedValue = aDataTable.Rows[0]["SectionId"].ToString();

                lblESection.Text = OldSectionDropDownList.SelectedItem.Text;
            }

            if (aDataTable.Rows[0]["SubSectionId"] != DBNull.Value)
            {

                OldSubSectionDropDownList.SelectedValue = aDataTable.Rows[0]["SubSectionId"].ToString();
                lblESubSection.Text = OldSubSectionDropDownList.SelectedItem.Text;
            }

            if (aDataTable.Rows[0]["DepartmentId"] != DBNull.Value)
            {
                OldDepartmentDropDownList.SelectedValue = aDataTable.Rows[0]["DepartmentId"].ToString();
                lblEDepartment.Text = OldDepartmentDropDownList.SelectedItem.Text;
            }

            if (aDataTable.Rows[0]["ReportingEmpId"] != DBNull.Value)
            {
                OldReportingBodyDropDownList.SelectedValue = aDataTable.Rows[0]["ReportingEmpId"].ToString();
                lblEOldReportingBody.Text = OldReportingBodyDropDownList.SelectedItem.Text;
            }
            EmployeeNameTextBox.Text = aDataTable.Rows[0]["NewEmpReportingBody"].ToString();
          //  SearchEmployeeNameTextBoxTextBox.Text = aDataTable.Rows[0]["EGSearchEmptxt"].ToString();


            if (aDataTable.Rows[rowIndex].Field<Boolean>("IsOnlyTransfer") !=null)
            {
                TransferRadioButtonList.Items[0].Selected = aDataTable.Rows[rowIndex].Field<Boolean>("IsOnlyTransfer");

                if (aDataTable.Rows[rowIndex].Field<Boolean>("IsOnlyTransfer")==true)
                {
                    lblTransferRadioButtonList.Text = "Inter Company Transfer ";
                }
               
            }

         

    
            if (aDataTable.Rows[rowIndex].Field<Boolean>("IsInterCompanyTransfer") != null)
            {
                TransferRadioButtonList.Items[3].Selected =
                    aDataTable.Rows[rowIndex].Field<Boolean>("IsInterCompanyTransfer");
                TransferRadioButtonList.Items[1].Attributes.Add("readonly", "readonly");

                if (aDataTable.Rows[rowIndex].Field<Boolean>("IsInterCompanyTransfer") == true)
                {



                    lblTransferRadioButtonList.Text = "Company To Company Transfer";
                }
            }
            if (TransferRadioButtonList.Items[1].Selected == true)
            {

                TransferRadioButtonList.Items[3].Attributes.Add("readonly", "readonly");
               
                Panel1.Visible = true;
                LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                Session["SalaryGradeId"] = null;


                HiddenFieldNewReportingBody.Value = aDataTable.Rows[rowIndex].Field<Int32>("NewEmpReportingBodyId").ToString();
               

              
            //     aEmpTransferAndRedesignationDal.EmployeeNameDropDown(OldReportingBodyDropDownList, companyDropDownList.SelectedValue);




                NewReportingBodyTextBox.Text = aDataTable.Rows[0]["NewEmpReportingBody"].ToString();
               
            }
            DataTable dtadata = aEmpTransferAndRedesignationDal.LoadSuperviseEmployee(repEmpIdHiddenField.Value.ToString());
            presuperGridView.DataSource = dtadata;
            presuperGridView.DataBind();
            if (TransferRadioButtonList.Items[0].Selected == true)
            {
                TransferRadioButtonList.Items[3].Enabled = false;

                ShowExistingAndNew.Visible = true;
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldCompanyId") != null)
                {
                    OldCompanyDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldCompanyId").ToString();
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSalaryLocationId") != null)
                {
                    OldSalaryLocationDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldSalaryLocationId").ToString();
                    aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(OldJobLocationDropDownList, OldSalaryLocationDropDownList.SelectedValue);
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldJobLocationId") != null)
                {
                    OldJobLocationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldJobLocationId").ToString();
                    lblEPlace.Text = OldJobLocationDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldDivisionId") != null)
                {
                    OldDivisionDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldDivisionId").ToString();
                    lblEDivision.Text = OldDivisionDropDownList.SelectedItem.Text;
                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldWingId") != null)
                {
                    OldUnitDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldWingId").ToString();

                    lblEWing.Text = OldUnitDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSectionId") != null)
                {
                    OldSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldSectionId").ToString();
                    lblESection.Text = OldSectionDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSubSectionId") != null)
                {
                    OldSubSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldSubSectionId").ToString();
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewCompanyId") != null)
                {
                    NewCompanyDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewCompanyId").ToString();
                    lblNComName.Text = NewCompanyDropDownList.SelectedItem.Text;
                }
                NewCompanyDropDownList_SelectedIndexChanged(null, null);

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSalaryLocationId") != null)
                {
                    NewSalaryLocationDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewSalaryLocationId").ToString();

                    try
                    {
                        lblNOffice.Text = NewSalaryLocationDropDownList.SelectedItem.Text;
                    }
                    catch (Exception)
                    {
                        
                      //  throw;
                    }

                    aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(NewJobLocationDropDownList, NewSalaryLocationDropDownList.SelectedValue);
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewJobLocationId") != null)
                {

                    NewJobLocationDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewJobLocationId").ToString();
                    lblNPlace.Text = NewJobLocationDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewDivisionId") != null)
                {
                    NewDivisionDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewDivisionId").ToString();
                    try
                    {
                        lblNDivision.Text = NewDivisionDropDownList.SelectedItem.Text;
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }
                    NewDivisionDropDownList_OnSelectedIndexChanged(null, null);
                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewDepartmentId") != null)
                {
                    NewDepartmentDropDownList1.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewDepartmentId").ToString();
                    try
                    {
                        lblNDepartment.Text = NewDepartmentDropDownList1.SelectedItem.Text;
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }
                }


                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewWingId") != null)
                {
                    NewWingDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewWingId").ToString();


                    try
                    {
                        lblNWing.Text = NewWingDropDownList.SelectedItem.Text;
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }
                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSectionId") != null)
                {
                    NewSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewSectionId").ToString();
                    try
                    {
                        lblNSection.Text = NewSectionDropDownList.SelectedItem.Text;
                    }
                    catch (Exception)
                    {
                        
                   //     throw;
                    }
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSubSectionId") != null)
                {
                    NewSubSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewSubSectionId").ToString();

                    lblNSubSection.Text = NewSubSectionDropDownList.SelectedItem.Text;
                }

               
               // aEmpTransferAndRedesignationDal.EmployeeNameDropDown(OldReportingBodyDropDownList, companyDropDownList.SelectedValue);
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldReportingBodyID") != null)
                {
                    OldReportingBodyDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldReportingBodyID").ToString();
                }
                //ssss
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewEmpReportingBodyId") != null)
                {
                    NewEmpBodyTextBoxHiddenField.Value =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewEmpReportingBodyId").ToString();
                }

              

                 if (aDataTable.Rows[rowIndex].Field<Int32?>("OldDepartmentId") != null)
                {
                    OldDepartmentDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldDepartmentId").ToString();

                    lblEDepartment.Text = OldDepartmentDropDownList.SelectedItem.Text;
                }

              

                if (aDataTable.Rows[rowIndex].Field<DateTime?>("EffectiveDate") != null)
                {
                    EfectiveDate.Text =
                        aDataTable.Rows[rowIndex].Field<DateTime>("EffectiveDate").ToString("dd-MMM-yyyy");
                    lblEffDate.Text = EfectiveDate.Text;
                }



                NewEmpBodyTextBox.Text = aDataTable.Rows[0]["NewEmpReportingBody"].ToString();
                lblNReportingBody.Text = NewEmpBodyTextBox.Text;
            }


            if (TransferRadioButtonList.Items[2].Selected == true)
            {
                Panel1.Visible = true;
                ShowExistingAndNew.Visible = true;

                 

                OldCompanyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldCompanyId").ToString();
                OldSalaryLocationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldSalaryLocationId").ToString();
                aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(OldJobLocationDropDownList, OldSalaryLocationDropDownList.SelectedValue);
                OldJobLocationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldJobLocationId").ToString();
                OldDivisionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldDevisionId").ToString();
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldUnitId") != null)
                {
                    OldUnitDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldUnitId").ToString();
                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSectionId") != null)
                {
                    OldSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldSectionId").ToString();
                
                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSubSectionId") != null)
                {

                    OldSubSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldSubSectionId").ToString();

                    lblESubSection.Text =
                        OldSubSectionDropDownList.SelectedItem.Text;
                }

               // HiddenFieldNewReportingBody.Value = aDataTable.Rows[rowIndex].Field<Int32>("NewEmpReportingBodyId").ToString();

                
               // NewReportingBodyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewEmpReportingBodyId").ToString();
                NewCompanyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewCompanyId").ToString();
                NewCompanyDropDownList_SelectedIndexChanged(null, null);
                HiddenFieldNewReportingBody.Value = aDataTable.Rows[rowIndex].Field<Int32>("NewEmpReportingBodyId").ToString();
                NewSalaryLocationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewSalaryLocationId").ToString();
                aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(NewJobLocationDropDownList, OldSalaryLocationDropDownList.SelectedValue);
                NewJobLocationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewJobLocationId").ToString();
                NewDivisionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewDevisionId").ToString();
                NewDivisionDropDownList_OnSelectedIndexChanged(null, null);
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewUnitId") != null)
                {
                    NewWingDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewUnitId").ToString();
                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSectionId") != null)
                {
                    NewSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewSectionId").ToString();
                }
            //    aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryId(NewdesignationDropDownList, Session["SalaryGradeId"].ToString());
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSubSectionId") != null)
                {
                    NewSubSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewSubSectionId").ToString();
                }
                //aEmpTransferAndRedesignationDal.EmployeeNameDropDown(OldReportingBodyDropDownList, companyDropDownList.SelectedValue);
                OldReportingBodyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("PEmpRptBodyId").ToString();
                if (OldReportingBodyDropDownList.SelectedValue!="")
                {
                    lblEOldReportingBody.Text = OldReportingBodyDropDownList.SelectedItem.Text;
                } 
            

                NewEmpBodyTextBoxHiddenField.Value = aDataTable.Rows[rowIndex].Field<Int32>("NewEmpReportingBodyId").ToString();
                NewDepartmentDropDownList1.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewDepartmentId").ToString();
                OldDepartmentDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldDepartmentId").ToString();


                if (OldDepartmentDropDownList.SelectedValue!="")
                {
                    lblEDepartment.Text = OldDepartmentDropDownList.SelectedItem.Text;
                }

                NewEmpBodyTextBox.Text = aDataTable.Rows[0]["EGGAllNewRpttxt"].ToString();
                NewReportingBodyTextBox.Text = aDataTable.Rows[0]["EGGNewRpttxt"].ToString();

                if (aDataTable.Rows[rowIndex].Field<DateTime?>("EffectiveDate") != null)
                {
                    EfectiveDate.Text =
                        aDataTable.Rows[rowIndex].Field<DateTime>("EffectiveDate").ToString("dd-MMM-yyyy");
                }

               //NewEmpBodyTextBox.Text=
            }

            if (TransferRadioButtonList.Items[3].Selected == true)
            {
                
                TransferRadioButtonList.Items[0].Enabled = false;
                ShowExistingAndNew.Visible = true;
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldCompanyId") != null)
                {
                    OldCompanyDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldCompanyId").ToString();
                    lblEComName.Text = OldCompanyDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSalaryLocationId") != null)
                {
                    OldSalaryLocationDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldSalaryLocationId").ToString();
                    lblEOffice.Text = OldSalaryLocationDropDownList.SelectedItem.Text;
                    
                    aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(OldJobLocationDropDownList, OldSalaryLocationDropDownList.SelectedValue);
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldJobLocationId") != null)
                {
                    OldJobLocationDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldJobLocationId").ToString();
                    lblEPlace.Text = OldJobLocationDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldDivisionId") != null)
                {
                    OldDivisionDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldDivisionId").ToString();
                    lblEDivision.Text = OldDivisionDropDownList.SelectedItem.Text;

                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldWingId") != null)
                {
                    OldUnitDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldWingId").ToString();
                    lblEWing.Text = OldUnitDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSectionId") != null)
                {
                    OldSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldSectionId").ToString();

                    lblESection.Text = OldSectionDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldSubSectionId") != null)
                {
                    OldSubSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("OldSubSectionId").ToString();

                    lblESubSection.Text = OldSubSectionDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewCompanyId") != null)
                {
                    NewCompanyDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewCompanyId").ToString();

                    lblNComName.Text = NewCompanyDropDownList.SelectedItem.Text;
                }
                NewCompanyDropDownList_SelectedIndexChanged(null, null);

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSalaryLocationId") != null)
                {
                    NewSalaryLocationDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewSalaryLocationId").ToString();

                    lblNOffice.Text = NewSalaryLocationDropDownList.SelectedItem.Text;
                    aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(NewJobLocationDropDownList, NewSalaryLocationDropDownList.SelectedValue);
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewJobLocationId") != null)
                {

                    NewJobLocationDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewJobLocationId").ToString();
                    lblNPlace.Text = NewJobLocationDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewDivisionId") != null)
                {
                    NewDivisionDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewDivisionId").ToString();

                    lblNDivision.Text = NewDivisionDropDownList.SelectedItem.Text;

                    NewDivisionDropDownList_OnSelectedIndexChanged(null, null);
                }



                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewWingId") != null)
                {
                    NewWingDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewWingId").ToString();
                    lblNWing.Text = NewWingDropDownList.SelectedItem.Text;
                }
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSectionId") != null)
                {
                    NewSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewSectionId").ToString();

                    lblNSection.Text = NewSectionDropDownList.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewSubSectionId") != null)
                {
                    NewSubSectionDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NewSubSectionId").ToString();
                    lblNSubSection.Text = NewSubSectionDropDownList.SelectedItem.Text;
                }


                // aEmpTransferAndRedesignationDal.EmployeeNameDropDown(OldReportingBodyDropDownList, companyDropDownList.SelectedValue);
                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldReportingBodyID") != null)
                {
                    OldReportingBodyDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldReportingBodyID").ToString();
                    lblEOldReportingBody.Text = OldReportingBodyDropDownList.SelectedItem.Text;
                }
                //ssss
                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewEmpReportingBodyId") != null)
                {
                    NewEmpBodyTextBoxHiddenField.Value =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewEmpReportingBodyId").ToString();
                    
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("NewDepartmentId") != null)
                {
                    NewDepartmentDropDownList1.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("NewDepartmentId").ToString();
                    lblNDepartment.Text = NewDepartmentDropDownList1.SelectedItem.Text;
                }

                if (aDataTable.Rows[rowIndex].Field<Int32?>("OldDepartmentId") != null)
                {
                    OldDepartmentDropDownList.SelectedValue =
                        aDataTable.Rows[rowIndex].Field<Int32>("OldDepartmentId").ToString();

                    lblEDepartment.Text = OldDepartmentDropDownList.SelectedItem.Text;
                }



                if (aDataTable.Rows[rowIndex].Field<DateTime?>("EffectiveDate") != null)
                {
                    EfectiveDate.Text =
                        aDataTable.Rows[rowIndex].Field<DateTime>("EffectiveDate").ToString("dd-MMM-yyyy");
                }



                NewEmpBodyTextBox.Text = aDataTable.Rows[0]["NewEmpReportingBody"].ToString();
                lblNReportingBody.Text = NewEmpBodyTextBox.Text;
                //    NewEmpBodyTextBox.Text = aDataTable.Rows[rowIndex].Field<string>("EGGAllNewRpttxt").ToString();

            }


            //aJobCreationDal.GetRequisitionCodeList(RequisitionDropDownList, companyDropDownList.SelectedValue);

            //RequisitionDropDownList.SelectedValue = aDataTable.Rows[0]["ReqCodeId"].ToString();

            //otherBenefitTextBox.Text = aDataTable.Rows[rowIndex].Field<String>("CompensationandOtherBenefits");
            //circulationStartDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("CirculationStartDate").ToString("dd-MMM-yyyy");
            //circulationEndDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("CirculationsdeadlineDate").ToString("dd-MMM-yyyy");
            //probableInterviewDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ProbableInterviewDate").ToString("dd-MMM-yyyy");
            ////probableRecruitmentDateTextBox.Text = aDataTable.Rows[rowIndex].Field<DateTime>("ProbableIRecruitmentDate").ToString("dd-MMM-yyyy");
            //remarksTextBox.Text = aDataTable.Rows[rowIndex].Field<String>("Remarks");
            //jobContextTextBox.Text = aDataTable.Rows[rowIndex].Field<String>("JobContext");

            //IsSalary.Checked = aDataTable.Rows[rowIndex].Field<Boolean>("IsSalary");
            DataTable AppLogComm = aEmpTransferAndRedesignationDal.GetAppLogCommByJobId(Convert.ToInt32(EmpTranRedesId));

            if (AppLogComm.Rows.Count > 0)
            {

                AppLogCommentGridView.DataSource = AppLogComm;
                AppLogCommentGridView.DataBind();
            }
            else
            {
                AppLogCommentGridView.DataSource = null;
                AppLogCommentGridView.DataBind();
            }
            RadioTextValue();
        }
    }
    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        EmpTransferAndRedesignationDao aMaster = new EmpTransferAndRedesignationDao();
        aMaster.EmpTransferAndRedesignationId
            = Convert.ToInt32(EmpTransferAndRedesignationIdHiddenField.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = aEmpTransferAndRedesignationDal.UpdateContractural(aMaster);
        int commentid = aEmpTransferAndRedesignationDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);
            aEmpTransferAndRedesignationDal.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='IncrementApprovalView.aspx';",
                   true);
    }

    private void RadioTextValue()
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
            dtdata = aEmpTransferAndRedesignationDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = aEmpTransferAndRedesignationDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
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


        if (actionstatusHiddenField.Value == "Approved")
        {
            submitButton.Visible = false;
            LinkButton1.Visible = true;
        }
        else
        {
            submitButton.Visible = true;
            LinkButton1.Visible = false;
        }
    }



    protected void Button2_OnClick(object sender, EventArgs e)
    {
         EmpTransferAndRedesignationDao aMaster = new EmpTransferAndRedesignationDao();
            aMaster.EmpTransferAndRedesignationId
                = Convert.ToInt32(EmpTransferAndRedesignationIdHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aEmpTransferAndRedesignationDal.UpdateContractural(aMaster);
            if (status)
            {
                int commentid = aEmpTransferAndRedesignationDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = aEmpTransferAndRedesignationDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };


                    if (appLogDao.ForEmpInfoId != 0)
                    {
                        int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);
                        aEmpTransferAndRedesignationDal.UpdateJobReqStatus2(aMaster);
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
                        aEmpTransferAndRedesignationDal.GetContractualDataInfo(aMaster.EmpTransferAndRedesignationId.ToString());
                    if (dtdatainfo.Rows.Count > 0)
                    {
                        isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                    }
                    if (isselfapp)
                    {
                        DataTable dtempdata = aEmpTransferAndRedesignationDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                             Session["CompanyId"].ToString() + "'");
                        if (dtempdata.Rows.Count > 0)
                        {
                            empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        }
                        EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        //aMaster.ActionStatus = "Verified";
                    aEmpTransferAndRedesignationDal.UpdateJobReqStatus2(aMaster);
                        int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Transfer Approval ",
                            @"  <br/> Dear Sir, <br/>
A Employee Transfer is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/");
                    }
                    else
                    {
                        empid = Convert.ToInt32(dtdatainfo.Rows[0]["ReportingEmpId"].ToString());
                        EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        aMaster.ActionStatus = "Verified";
                        aEmpTransferAndRedesignationDal.UpdateContractural(aMaster);
                        aEmpTransferAndRedesignationDal.UpdateJobReqStatus2(aMaster);
                        aEmpTransferAndRedesignationDal.UpdateSelfApprove(aMaster.EmpTransferAndRedesignationId, true);
                     
                        //aEmpTransferAndRedesignationDal.UpdateJobReqStatus2(aMaster);
                        int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);
                    }

                    
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = aEmpTransferAndRedesignationDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), EmpTransferAndRedesignationIdHiddenField.Value);
                    DataTable dtempdata2 = aEmpTransferAndRedesignationDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), EmpTransferAndRedesignationIdHiddenField.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        EmpTransferAndRedesignationAppLogDAO appLogDao = new EmpTransferAndRedesignationAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.EmpTransferAndRedesignationId = aMaster.EmpTransferAndRedesignationId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        aEmpTransferAndRedesignationDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = aEmpTransferAndRedesignationDal.SavAppLog(appLogDao);
                        aEmpTransferAndRedesignationDal.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {


                       
                            ShowMessageBox("Please select Approval Status Approved  this!!!");
                        

                    }
                    DataTable dtdata = aEmpTransferAndRedesignationDal.GetDataReviewEntryBy(
                        EmpTransferAndRedesignationIdHiddenField.Value, Session["UserId"].ToString(), "Review");
                    if (dtdata.Rows.Count > 0)
                    {
                        Session["Status"] = "";
                        Session["Status"] = "Edit";
                        Session["TransferEdit"] = aMaster.EmpTransferAndRedesignationId.ToString();
                        Response.Redirect("EmpTransferAndRedesignation.aspx?id2=" + aMaster.EmpTransferAndRedesignationId.ToString());
                    }


                }


            }
            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfully Done...');window.location ='EmpTransferAndRedesignationApprovalList.aspx';",
                       true);
        }


      


    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (dynamic)null;
            if (forEmpID>0)
            {
                  GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                      where t.EmpInfoId == forEmpID
                                      select t).FirstOrDefault();
            }
            else
            {
              int EntryEmpID=  Convert.ToInt32(entryempinfoIdHiddenField.Value);


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

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public void Update()
    {
        if (Validation())
        {

            EmpTransferAndRedesignationDao aEmpTransferAndRedesignationDao = new EmpTransferAndRedesignationDao();

            aEmpTransferAndRedesignationDao.EmpTransferAndRedesignationId = Convert.ToInt32(EmpTransferAndRedesignationIdHiddenField.Value);

            aEmpTransferAndRedesignationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            aEmpTransferAndRedesignationDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
            aEmpTransferAndRedesignationDao.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

            aEmpTransferAndRedesignationDao.EmployeeCode = txtEmpId.Text;

            for (int i = 0; i < TransferRadioButtonList.Items.Count; i++)
            {
                if (TransferRadioButtonList.Items[i].Selected)
                {
                    string str = TransferRadioButtonList.Items[i].Text.Trim();

                    if (str == "Company To Company Transfer")
                    {
                      
                        aEmpTransferAndRedesignationDao.IsOnlyTransfer = true;
                        aEmpTransferAndRedesignationDao.IsInterCompanyTransfer = false;
                        aEmpTransferAndRedesignationDao.NewCompanyId = NewCompanyDropDownList.SelectedIndex > 0 ? int.Parse(NewCompanyDropDownList.SelectedValue) : (int?)null;
                        aEmpTransferAndRedesignationDao.OldCompanyId = OldCompanyDropDownList.SelectedIndex > 0 ? int.Parse(OldCompanyDropDownList.SelectedValue) : (int?)null;



                        aEmpTransferAndRedesignationDao.NewSalaryLocationId = NewSalaryLocationDropDownList.SelectedIndex > 0 ? int.Parse(NewSalaryLocationDropDownList.SelectedValue) : (int?)null;
                        aEmpTransferAndRedesignationDao.OldSalaryLocationId = OldSalaryLocationDropDownList.SelectedIndex > 0 ? int.Parse(OldSalaryLocationDropDownList.SelectedValue) : (int?)null;





                        aEmpTransferAndRedesignationDao.NewJobLocationId = NewJobLocationDropDownList.SelectedIndex > 0 ? int.Parse(NewJobLocationDropDownList.SelectedValue) : (int?)null;
                        aEmpTransferAndRedesignationDao.OldJobLocationId = OldJobLocationDropDownList.SelectedIndex > 0 ? int.Parse(OldJobLocationDropDownList.SelectedValue) : (int?)null;


                        aEmpTransferAndRedesignationDao.NewDivisionId = NewDivisionDropDownList.SelectedIndex > 0 ? int.Parse(NewDivisionDropDownList.SelectedValue) : (int?)null;

                        NewDivisionDropDownList_OnSelectedIndexChanged(null, null);
                        aEmpTransferAndRedesignationDao.OldDivisionId = OldDivisionDropDownList.SelectedIndex > 0 ? int.Parse(OldDivisionDropDownList.SelectedValue) : (int?)null;

                        aEmpTransferAndRedesignationDao.NewWingId = NewWingDropDownList.SelectedValue != "" && NewWingDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(NewWingDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldWingId = OldUnitDropDownList.SelectedValue != "" &&
                                                                    OldUnitDropDownList.SelectedValue != null
                            ? (int?)Convert.ToInt32(OldUnitDropDownList.SelectedValue)
                            : null;

                        aEmpTransferAndRedesignationDao.NewDepartmentId = NewDepartmentDropDownList1.SelectedValue != "" &&
                                                                          NewDepartmentDropDownList1.SelectedValue !=
                                                                          null
                            ? (int?)Convert.ToInt32(NewDepartmentDropDownList1.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldDepartmentId = OldDepartmentDropDownList.SelectedValue != "" &&
                                                                          OldDepartmentDropDownList.SelectedValue !=
                                                                          null
                            ? (int?)Convert.ToInt32(OldDepartmentDropDownList.SelectedValue) : null;



                        aEmpTransferAndRedesignationDao.NewSectionId = NewSectionDropDownList.SelectedValue != "" && NewSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(NewSectionDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldSectionId = OldSectionDropDownList.SelectedValue != "" && OldSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(OldSectionDropDownList.SelectedValue) : null;



                        aEmpTransferAndRedesignationDao.NewSubSectionId = NewSubSectionDropDownList.SelectedValue != "" && NewSubSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(NewSubSectionDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldSubSectionId = OldSubSectionDropDownList.SelectedValue != "" && OldSubSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(OldSubSectionDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.EffectiveDate = EfectiveDate.Text != "" && EfectiveDate.Text != null ? (DateTime?)Convert.ToDateTime(EfectiveDate.Text) : null;

                        aEmpTransferAndRedesignationDao.NewEmpReportingBodyId = Convert.ToInt32(NewEmpBodyTextBoxHiddenField.Value);
                        aEmpTransferAndRedesignationDao.OldReportingBodyID = OldReportingBodyDropDownList.SelectedValue != "" && OldReportingBodyDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(OldReportingBodyDropDownList.SelectedValue) : null;
                       
                    }


                    if (str == "Inter Company Transfer")
                    {
                        
                      
                        aEmpTransferAndRedesignationDao.IsOnlyTransfer = false;
                        aEmpTransferAndRedesignationDao.IsInterCompanyTransfer = true;
                        aEmpTransferAndRedesignationDao.NewCompanyId = NewCompanyDropDownList.SelectedIndex > 0 ? int.Parse(NewCompanyDropDownList.SelectedValue) : (int?)null;
                        aEmpTransferAndRedesignationDao.OldCompanyId = OldCompanyDropDownList.SelectedIndex > 0 ? int.Parse(OldCompanyDropDownList.SelectedValue) : (int?)null;



                        aEmpTransferAndRedesignationDao.NewSalaryLocationId = NewSalaryLocationDropDownList.SelectedIndex > 0 ? int.Parse(NewSalaryLocationDropDownList.SelectedValue) : (int?)null;
                        aEmpTransferAndRedesignationDao.OldSalaryLocationId = OldSalaryLocationDropDownList.SelectedIndex > 0 ? int.Parse(OldSalaryLocationDropDownList.SelectedValue) : (int?)null;





                        aEmpTransferAndRedesignationDao.NewJobLocationId = NewJobLocationDropDownList.SelectedIndex > 0 ? int.Parse(NewJobLocationDropDownList.SelectedValue) : (int?)null;
                        aEmpTransferAndRedesignationDao.OldJobLocationId = OldJobLocationDropDownList.SelectedIndex > 0 ? int.Parse(OldJobLocationDropDownList.SelectedValue) : (int?)null;


                        aEmpTransferAndRedesignationDao.NewDivisionId = NewDivisionDropDownList.SelectedIndex > 0 ? int.Parse(NewDivisionDropDownList.SelectedValue) : (int?)null;
                        aEmpTransferAndRedesignationDao.OldDivisionId = OldDivisionDropDownList.SelectedIndex > 0 ? int.Parse(OldDivisionDropDownList.SelectedValue) : (int?)null;

                        aEmpTransferAndRedesignationDao.NewWingId = NewWingDropDownList.SelectedValue != "" && NewWingDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(NewWingDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldWingId = OldUnitDropDownList.SelectedValue != "" &&
                                                                    OldUnitDropDownList.SelectedValue != null
                            ? (int?)Convert.ToInt32(OldUnitDropDownList.SelectedValue)
                            : null;

                        aEmpTransferAndRedesignationDao.NewDepartmentId = NewDepartmentDropDownList1.SelectedValue != "" &&
                                                                          NewDepartmentDropDownList1.SelectedValue !=
                                                                          null
                            ? (int?)Convert.ToInt32(NewDepartmentDropDownList1.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldDepartmentId = OldDepartmentDropDownList.SelectedValue != "" &&
                                                                          OldDepartmentDropDownList.SelectedValue !=
                                                                          null
                            ? (int?)Convert.ToInt32(OldDepartmentDropDownList.SelectedValue) : null;



                        aEmpTransferAndRedesignationDao.NewSectionId = NewSectionDropDownList.SelectedValue != "" && NewSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(NewSectionDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldSectionId = OldSectionDropDownList.SelectedValue != "" && OldSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(OldSectionDropDownList.SelectedValue) : null;



                        aEmpTransferAndRedesignationDao.NewSubSectionId = NewSubSectionDropDownList.SelectedValue != "" && NewSubSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(NewSubSectionDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.OldSubSectionId = OldSubSectionDropDownList.SelectedValue != "" && OldSubSectionDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(OldSubSectionDropDownList.SelectedValue) : null;
                        aEmpTransferAndRedesignationDao.EffectiveDate = EfectiveDate.Text != "" && EfectiveDate.Text != null ? (DateTime?)Convert.ToDateTime(EfectiveDate.Text) : null;

                        aEmpTransferAndRedesignationDao.NewEmpReportingBodyId = Convert.ToInt32(NewEmpBodyTextBoxHiddenField.Value);
                        aEmpTransferAndRedesignationDao.OldReportingBodyID = OldReportingBodyDropDownList.SelectedValue != "" && OldReportingBodyDropDownList.SelectedValue != null ? (int?)Convert.ToInt32(OldReportingBodyDropDownList.SelectedValue) : null;
                      
                    }
                }
            }

       
            aEmpTransferAndRedesignationDao.UpdateBy = Session["UserId"].ToString();
            aEmpTransferAndRedesignationDao.UpdateDate = DateTime.Now;


            //aEmpTransferAndRedesignationDao.AutoProcess = false;
            if (manualUpdateCheckBox.Checked)
            {
                aEmpTransferAndRedesignationDao.AutoProcess = "Manually Updated";
            }


            if (EmpTypeId.Value!="")
            {
                aEmpTransferAndRedesignationDao.EmpTypeId = Convert.ToInt32(EmpTypeId.Value) > 0 ? int.Parse(EmpTypeId.Value) : (int?)null;  
            }
            else
            {
                aEmpTransferAndRedesignationDao.EmpTypeId = null;
            }
          

            aEmpTransferAndRedesignationDao.Remarks = OtherRemarksTextBox.Text;

            aEmpTransferAndRedesignationDal.OnlyTransferUpsateInfo(aEmpTransferAndRedesignationDao);
            aEmpTransferAndRedesignationDal.DeleteDirectlyS(EmpTransferAndRedesignationIdHiddenField.Value.ToString());
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao()
                {
                    EmpTransferAndRedesignationId = Convert.ToInt32(EmpTransferAndRedesignationIdHiddenField.Value),
                    EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())
                };
                int idd =
                    aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationDSSaveInfo(
                        andRedesignationDao);
            }
            aEmpTransferAndRedesignationDal.DeletePS(EmpTransferAndRedesignationIdHiddenField.Value.ToString());
            for (int i = 0; i < presuperGridView.Rows.Count; i++)
            {
                EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao()
                {
                    EmpTransferAndRedesignationId = Convert.ToInt32(EmpTransferAndRedesignationIdHiddenField.Value),
                    EmpInfoId = Convert.ToInt32(presuperGridView.DataKeys[i][0].ToString())
                };
                int idd =
                    aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationPSSaveInfo(
                        andRedesignationDao);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Data Update Successfull...');window.location ='EmpTransferAndRedesignationView.aspx';",
                     true);
        }



      



   





    }


    private void LoadDropDownList()
    {

        //aEmpTransferAndRedesignationDal.EmployeeNameDropDown(EmployeeDropDownList);

        aEmpTransferAndRedesignationDal.LoadCompanyDropDownListOld(OldCompanyDropDownList);
        aEmpTransferAndRedesignationDal.LoadCompanyDropDownList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        aEmpTransferAndRedesignationDal.LoadSalaryLocationDropDownListOld(OldSalaryLocationDropDownList);
        //aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(OldJobLocationDropDownList);
        //aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(OldJobLocationDropDownList);
        aEmpTransferAndRedesignationDal.LoadDivisionDropDownListOld(OldDivisionDropDownList);
        aEmpTransferAndRedesignationDal.LoadDivisionWingDropDownListOld(OldUnitDropDownList);
        aEmpTransferAndRedesignationDal.LoadSectionDropDownListOld(OldSectionDropDownList);
        aEmpTransferAndRedesignationDal.LoadSubSectionDropDownListOld(OldSubSectionDropDownList);
        aEmpTransferAndRedesignationDal.LoadOldDepartmentDropDownListOld(OldDepartmentDropDownList);


      //  aEmpTransferAndRedesignationDal.LoadPreSalaryGradeDropDownList(NewSalaryGradeDropDownList);
        //new
        aEmpTransferAndRedesignationDal.LoadCompanyDropDownList(NewCompanyDropDownList);
        
    }


    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
        }
        catch (Exception)
        {

        }
    }



    public void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aEmpTransferAndRedesignationDal.LoadEmpJInfoInTextBoxById(id);

       ViewState["ffff"] = dtdata;
        if (dtdata.Rows.Count > 0)
        {

            DataTable dtadata = aEmpTransferAndRedesignationDal.LoadSuperviseEmployee(id.ToString());
            presuperGridView.DataSource = dtadata;
            presuperGridView.DataBind();


            EmployeeNameTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
           
            EmpTypeId.Value = dtdata.Rows[0]["EmpTypeId"].ToString();
            txtEmpId.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            DesignationTextBox.Text = dtdata.Rows[0]["Designation"].ToString();
            JoiningDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


            //int SalaryGradeId = 0;

        //  DataTable dtdataa = new DataTable();
           // dtdataa = aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryIdDtab(NewSalaryGradeDropDownList, (dtdataa.Rows[0]["SalaryGradeId"].ToString()));
           
            SalaryGradeTextBox.Text = dtdata.Rows[0]["GradeName"].ToString();


            OldCompanyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            if (TransferRadioButtonList.Items[3].Selected)
            {
                NewCompanyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            }
            OldCompanyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();

            try
            {
                OldSalaryLocationDropDownList.SelectedValue = dtdata.Rows[0]["SalaryLoationId"].ToString();
            }
            catch (Exception)
            {
                
                //throw;
            }
            aEmpTransferAndRedesignationDal.LoadJobLocationDropDownListOld(OldJobLocationDropDownList, OldSalaryLocationDropDownList.SelectedValue);
            try
            {
                OldJobLocationDropDownList.SelectedValue = dtdata.Rows[0]["JobLocationId"].ToString();
            }
            catch (Exception)
            {
                
                //throw;
            }

            OldDivisionDropDownList.SelectedValue = dtdata.Rows[0]["DivisionId"].ToString();
            OldUnitDropDownList.SelectedValue = dtdata.Rows[0]["DivisionWId"].ToString();
             OldSectionDropDownList.SelectedValue = dtdata.Rows[0]["SectionId"].ToString();
           OldSubSectionDropDownList.SelectedValue = dtdata.Rows[0]["SubSectionId"].ToString();
            OldDepartmentDropDownList.SelectedValue = dtdata.Rows[0]["DepartmentId"].ToString();
            OldReportingBodyDropDownList.SelectedValue = dtdata.Rows[0]["ReportingEmpId"].ToString();
        
         Session["SalaryGradeId"] = null;
         Session["SalaryGradeId"] = dtdata.Rows[0]["SalaryGradeId"].ToString();

         aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryId(NewdesignationDropDownList, Session["SalaryGradeId"].ToString());
        }
    }



    public void LoadDataNew(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aEmpTransferAndRedesignationDal.LoadEmpJInfoInTextBoxById(id);

        ViewState["ffff"] = dtdata;
        if (dtdata.Rows.Count > 0)
        {

            DataTable dtadata = aEmpTransferAndRedesignationDal.LoadSuperviseEmployee(id.ToString());
            presuperGridView.DataSource = dtadata;
            presuperGridView.DataBind();

            SearchEmployeeNameTextBoxTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
            EmployeeNameTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();

            EmpTypeId.Value = dtdata.Rows[0]["EmpTypeId"].ToString();
            txtEmpId.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            DesignationTextBox.Text = dtdata.Rows[0]["Designation"].ToString();
            JoiningDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


            //int SalaryGradeId = 0;

            //  DataTable dtdataa = new DataTable();
            // dtdataa = aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryIdDtab(NewSalaryGradeDropDownList, (dtdataa.Rows[0]["SalaryGradeId"].ToString()));

            SalaryGradeTextBox.Text = dtdata.Rows[0]["GradeName"].ToString();


            OldCompanyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            if (TransferRadioButtonList.Items[3].Selected)
            {
                NewCompanyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            }
            OldCompanyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();

            try
            {
                OldSalaryLocationDropDownList.SelectedValue = dtdata.Rows[0]["SalaryLoationId"].ToString();
            }
            catch (Exception)
            {

                //throw;
            }
            aEmpTransferAndRedesignationDal.LoadJobLocationDropDownListOld(OldJobLocationDropDownList, OldSalaryLocationDropDownList.SelectedValue);
            try
            {
                OldJobLocationDropDownList.SelectedValue = dtdata.Rows[0]["JobLocationId"].ToString();
            }
            catch (Exception)
            {

                //throw;
            }

            OldDivisionDropDownList.SelectedValue = dtdata.Rows[0]["DivisionId"].ToString();
            OldUnitDropDownList.SelectedValue = dtdata.Rows[0]["DivisionWId"].ToString();
            OldSectionDropDownList.SelectedValue = dtdata.Rows[0]["SectionId"].ToString();
            OldSubSectionDropDownList.SelectedValue = dtdata.Rows[0]["SubSectionId"].ToString();
            OldDepartmentDropDownList.SelectedValue = dtdata.Rows[0]["DepartmentId"].ToString();
            OldReportingBodyDropDownList.SelectedValue = dtdata.Rows[0]["ReportingEmpId"].ToString();

            Session["SalaryGradeId"] = null;
            Session["SalaryGradeId"] = dtdata.Rows[0]["SalaryGradeId"].ToString();

            aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryId(NewdesignationDropDownList, Session["SalaryGradeId"].ToString());
        }
    }


    public void LoadDataGetOneRecord(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aEmpTransferAndRedesignationDal.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {

           
            DesignationTextBox.Text = dtdata.Rows[0]["Designation"].ToString();
            JoiningDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            SalaryGradeTextBox.Text = dtdata.Rows[0]["GradeName"].ToString();


          //  NewEmpBodyTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();


        }
    }


    protected void TransferRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(TransferRadioButtonList.Items[1].Selected == true)
        //{
        //    lblNewDesignationShow.Visible=true;
        //    NewdesignationDropDownList.Visible = true;
        //}
        aEmpTransferAndRedesignationDal.LoadAllCompanyDropDownList(NewCompanyDropDownList);

        if (ValidationForEmpAndEmpSearch())
        {

        if (TransferRadioButtonList.Items[1].Selected == true)
        {
            Panel1.Visible = true;
            NewCompanyDropDownList.Enabled = true;
            NewCompanyDropDownList.SelectedValue = null;
         
      
        }
        if (TransferRadioButtonList.Items[1].Selected == false)
        {
            Panel1.Visible = false;
            NewCompanyDropDownList.Enabled = true;
            NewCompanyDropDownList.SelectedValue = null;
       
        }


        if (TransferRadioButtonList.Items[0].Selected == true)
        {
            ShowExistingAndNew.Visible = true;
            NewCompanyDropDownList.Enabled = true;
            NewCompanyDropDownList.SelectedValue = null;
        }

        if (TransferRadioButtonList.Items[0].Selected == false)
        {
            ShowExistingAndNew.Visible = false;
            NewCompanyDropDownList.Enabled = true;
            NewCompanyDropDownList.SelectedValue = null;
        }

        if (TransferRadioButtonList.Items[2].Selected == true)
        {
            Panel1.Visible = true;
            ShowExistingAndNew.Visible = true;
            NewCompanyDropDownList.Enabled = true;
           
            NewCompanyDropDownList.SelectedValue = null;

        }

        if (TransferRadioButtonList.Items[3].Selected == true)
        {
           
                Panel1.Visible = false;
                ShowExistingAndNew.Visible = false;
                ShowExistingAndNew.Visible = true;

              //  NewCompanyDropDownList.Enabled = true;
           

                DataTable dt = (DataTable)ViewState["ffff"];

               


             
                //  NewCompanyDropDownList.Attributes.Add("readonly", "readonly");
                // NewCompanyDropDownList.Attributes.Add("disabled", "disabled");

                if (companyDropDownList.SelectedValue != null && companyDropDownList.SelectedValue != string.Empty && repEmpIdHiddenField.Value != null && repEmpIdHiddenField.Value != string.Empty)
                {
                    NewCompanyDropDownList.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                }
                NewCompanyDropDownList.Enabled = false;
                if (NewCompanyDropDownList.SelectedValue != "")
                {
                  
                    aEmpTransferAndRedesignationDal.GetNewDivisionDropDownList(NewDivisionDropDownList, NewCompanyDropDownList.SelectedValue);


                    aEmpTransferAndRedesignationDal.GetNewWingDropDownList(NewWingDropDownList, NewCompanyDropDownList.SelectedValue);
                    aEmpTransferAndRedesignationDal.GetNewSectionDropDownList(NewSectionDropDownList, NewCompanyDropDownList.SelectedValue);
                    aEmpTransferAndRedesignationDal.GetNewSubsectionDropDownList(NewSubSectionDropDownList, NewCompanyDropDownList.SelectedValue);


                    aEmpTransferAndRedesignationDal.LoadSalaryLocationDropDownList(NewSalaryLocationDropDownList);
                    aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(NewJobLocationDropDownList);
                    aEmpTransferAndRedesignationDal.LoadOldDepartmentDropDownList(NewDepartmentDropDownList1);


                    if (companyDropDownList.SelectedValue != "")
                    {
                        Session["CompanyId"] = companyDropDownList.SelectedValue;
                    }

                }

                else
                {
                    NewCompanyDropDownList.Items.Clear();
                }
            }
            else
            {
                TransferRadioButtonList.Items[3].Selected = false;
            }

         

        }

        else
        {
            TransferRadioButtonList.Items[0].Selected = false;
            TransferRadioButtonList.Items[1].Selected = false;
            TransferRadioButtonList.Items[2].Selected = false;
           
            TransferRadioButtonList.Items[3].Selected = false;
        }

        //if (TransferRadioButtonList.Items[2].Selected == false)
        //{
        //    Panel1.Visible = false;
        //    ShowExistingAndNew.Visible = false;
        //}

        TransferRadioButtonList.Items[1].Attributes.Add("hidden", "hidden");
        TransferRadioButtonList.Items[2].Attributes.Add("hidden", "hidden");
    }

    private bool ValidationForEmpAndEmpSearch()
    {
        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
            companyDropDownList.Focus();
            return false;
        }

        if (SearchEmployeeNameTextBoxTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Employee Name !!!", this);
            SearchEmployeeNameTextBoxTextBox.Focus();
            return false;
        }
        return true;
    }

    protected void NewCompanyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {


        try
        {
            if (NewCompanyDropDownList.SelectedValue != "")
            {
                aEmpTransferAndRedesignationDal.GetNewDivisionDropDownList(NewDivisionDropDownList, NewCompanyDropDownList.SelectedValue);


                aEmpTransferAndRedesignationDal.GetNewWingDropDownList(NewWingDropDownList, NewCompanyDropDownList.SelectedValue);
                aEmpTransferAndRedesignationDal.GetNewSectionDropDownList(NewSectionDropDownList, NewCompanyDropDownList.SelectedValue);
                aEmpTransferAndRedesignationDal.GetNewSubsectionDropDownList(NewSubSectionDropDownList, NewCompanyDropDownList.SelectedValue);


                aEmpTransferAndRedesignationDal.LoadSalaryLocationDropDownList(NewSalaryLocationDropDownList);

                aEmpTransferAndRedesignationDal.LoadOldDepartmentDropDownList(NewDepartmentDropDownList1);


                if (companyDropDownList.SelectedValue != "")
                {
                    Session["CompanyId"] = companyDropDownList.SelectedValue;
                }

            }

            else
            {
                NewCompanyDropDownList.Items.Clear();
            }

            TransferRadioButtonList.Items[1].Attributes.Add("hidden", "hidden");
            TransferRadioButtonList.Items[2].Attributes.Add("hidden", "hidden");
        }
        catch (Exception)
        {
            
        }

    

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (EmpTransferAndRedesignationIdHiddenField.Value == string.Empty)
        {
            //DataTable dtdata = aEmpTransferAndRedesignationDal.GetEmployeeEmpTransandReDesig(repEmpIdHiddenField.Value);
            //if (dtdata.Rows.Count < 1)
            //{

                Save();
            //}
            //else
            //{
            //    aShowMessage.ShowMessageBox("Employee  Transfer Exist ", this);
            //}
        }
 
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
       Clear();
    }

    public void Save()
    {
        try
        {
            if (Validation())
            {
                DataTable aTable =
                               aEmpTransferAndRedesignationDal.ValidattionForEffectiveDate(
                                     repEmpIdHiddenField.Value, EfectiveDate.Text);

                if (aTable.Rows.Count > 0)
                {
                    aShowMessage.ShowMessageBox("Data Can not be Inserted", this);
                }
                else
                {
                    EmpTransferAndRedesignationDao aEmpTransferAndRedesignationDao = new EmpTransferAndRedesignationDao();

                    aEmpTransferAndRedesignationDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                    aEmpTransferAndRedesignationDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
                    aEmpTransferAndRedesignationDao.FinancialYearId =
                        Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

                    aEmpTransferAndRedesignationDao.EmployeeCode = txtEmpId.Text;

                    for (int i = 0; i < TransferRadioButtonList.Items.Count; i++)
                    {
                        if (TransferRadioButtonList.Items[i].Selected)
                        {
                            string str = TransferRadioButtonList.Items[i].Text.Trim();

                            if (str == "Company To Company Transfer")
                            {

                                aEmpTransferAndRedesignationDao.IsOnlyTransfer = true;
                                aEmpTransferAndRedesignationDao.IsInterCompanyTransfer = false;

                                aEmpTransferAndRedesignationDao.NewCompanyId = NewCompanyDropDownList.SelectedIndex > 0
                                    ? int.Parse(NewCompanyDropDownList.SelectedValue)
                                    : (int?)null;
                                aEmpTransferAndRedesignationDao.OldCompanyId = OldCompanyDropDownList.SelectedIndex > 0
                                    ? int.Parse(OldCompanyDropDownList.SelectedValue)
                                    : (int?)null;



                                aEmpTransferAndRedesignationDao.NewSalaryLocationId =
                                    NewSalaryLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(NewSalaryLocationDropDownList.SelectedValue)
                                        : (int?)null;

                                aEmpTransferAndRedesignationDao.OldSalaryLocationId =
                                    OldSalaryLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(OldSalaryLocationDropDownList.SelectedValue)
                                        : (int?)null;

                                aEmpTransferAndRedesignationDao.NewJobLocationId =
                                    NewJobLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(NewJobLocationDropDownList.SelectedValue)
                                        : (int?)null;

                                aEmpTransferAndRedesignationDao.OldJobLocationId =
                                    OldJobLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(OldJobLocationDropDownList.SelectedValue)
                                        : (int?)null;

                                aEmpTransferAndRedesignationDao.NewDivisionId = NewDivisionDropDownList.SelectedIndex > 0
                                    ? int.Parse(NewDivisionDropDownList.SelectedValue)
                                    : (int?)null;
                                aEmpTransferAndRedesignationDao.OldDivisionId = OldDivisionDropDownList.SelectedIndex > 0
                                    ? int.Parse(OldDivisionDropDownList.SelectedValue)
                                    : (int?)null;




                                aEmpTransferAndRedesignationDao.NewWingId = NewWingDropDownList.SelectedValue != "" &&
                                                                            NewWingDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(NewWingDropDownList.SelectedValue)
                                    : null;
                                aEmpTransferAndRedesignationDao.OldWingId = OldUnitDropDownList.SelectedValue != "" &&
                                                                            OldUnitDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(OldUnitDropDownList.SelectedValue)
                                    : null;

                                aEmpTransferAndRedesignationDao.NewDepartmentId =
                                    NewDepartmentDropDownList1.SelectedValue != "" &&
                                    NewDepartmentDropDownList1.SelectedValue !=
                                    null
                                        ? (int?)Convert.ToInt32(NewDepartmentDropDownList1.SelectedValue)
                                        : null;
                                aEmpTransferAndRedesignationDao.OldDepartmentId = OldDepartmentDropDownList.SelectedValue !=
                                                                                  "" &&
                                                                                  OldDepartmentDropDownList.SelectedValue !=
                                                                                  null
                                    ? (int?)Convert.ToInt32(OldDepartmentDropDownList.SelectedValue)
                                    : null;



                                aEmpTransferAndRedesignationDao.NewSectionId = NewSectionDropDownList.SelectedValue != "" &&
                                                                               NewSectionDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(NewSectionDropDownList.SelectedValue)
                                    : null;
                                aEmpTransferAndRedesignationDao.OldSectionId = OldSectionDropDownList.SelectedValue != "" &&
                                                                               OldSectionDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(OldSectionDropDownList.SelectedValue)
                                    : null;



                                aEmpTransferAndRedesignationDao.NewSubSectionId = NewSubSectionDropDownList.SelectedValue !=
                                                                                  "" &&
                                                                                  NewSubSectionDropDownList.SelectedValue !=
                                                                                  null
                                    ? (int?)Convert.ToInt32(NewSubSectionDropDownList.SelectedValue)
                                    : null;
                                aEmpTransferAndRedesignationDao.OldSubSectionId = OldSubSectionDropDownList.SelectedValue !=
                                                                                  "" &&
                                                                                  OldSubSectionDropDownList.SelectedValue !=
                                                                                  null
                                    ? (int?)Convert.ToInt32(OldSubSectionDropDownList.SelectedValue)
                                    : null;


                                aEmpTransferAndRedesignationDao.NewEmpReportingBodyId =
                                    Convert.ToInt32(NewEmpBodyTextBoxHiddenField.Value);
                                aEmpTransferAndRedesignationDao.OldReportingBodyID =
                                    OldReportingBodyDropDownList.SelectedValue != "" &&
                                    OldReportingBodyDropDownList.SelectedValue != null
                                        ? (int?)Convert.ToInt32(OldReportingBodyDropDownList.SelectedValue)
                                        : null;

                                aEmpTransferAndRedesignationDao.EffectiveDate = EfectiveDate.Text != "" &&
                                                                                EfectiveDate.Text != null
                                    ? (DateTime?)Convert.ToDateTime(EfectiveDate.Text)
                                    : null;


                            }



                            if (str == "Inter Company Transfer")
                            {

                                aEmpTransferAndRedesignationDao.IsOnlyTransfer = false;
                                aEmpTransferAndRedesignationDao.IsInterCompanyTransfer = true;

                                aEmpTransferAndRedesignationDao.NewCompanyId = NewCompanyDropDownList.SelectedIndex > 0
                                    ? int.Parse(NewCompanyDropDownList.SelectedValue)
                                    : (int?)null;

                                aEmpTransferAndRedesignationDao.OldCompanyId = OldCompanyDropDownList.SelectedIndex > 0
                                    ? int.Parse(OldCompanyDropDownList.SelectedValue)
                                    : (int?)null;



                                aEmpTransferAndRedesignationDao.NewSalaryLocationId =
                                    NewSalaryLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(NewSalaryLocationDropDownList.SelectedValue)
                                        : (int?)null;
                                aEmpTransferAndRedesignationDao.OldSalaryLocationId =
                                    OldSalaryLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(OldSalaryLocationDropDownList.SelectedValue)
                                        : (int?)null;


                                aEmpTransferAndRedesignationDao.NewJobLocationId =
                                    NewJobLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(NewJobLocationDropDownList.SelectedValue)
                                        : (int?)null;
                                aEmpTransferAndRedesignationDao.OldJobLocationId =
                                    OldJobLocationDropDownList.SelectedIndex > 0
                                        ? int.Parse(OldJobLocationDropDownList.SelectedValue)
                                        : (int?)null;


                                aEmpTransferAndRedesignationDao.NewDivisionId = NewDivisionDropDownList.SelectedIndex > 0
                                    ? int.Parse(NewDivisionDropDownList.SelectedValue)
                                    : (int?)null;
                                aEmpTransferAndRedesignationDao.OldDivisionId = OldDivisionDropDownList.SelectedIndex > 0
                                    ? int.Parse(OldDivisionDropDownList.SelectedValue)
                                    : (int?)null;


                                aEmpTransferAndRedesignationDao.NewWingId = NewWingDropDownList.SelectedValue != "" &&
                                                                            NewWingDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(NewWingDropDownList.SelectedValue)
                                    : null;
                                aEmpTransferAndRedesignationDao.OldWingId = OldUnitDropDownList.SelectedValue != "" &&
                                                                            OldUnitDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(OldUnitDropDownList.SelectedValue)
                                    : null;

                                aEmpTransferAndRedesignationDao.NewDepartmentId =
                                    NewDepartmentDropDownList1.SelectedValue != "" &&
                                    NewDepartmentDropDownList1.SelectedValue !=
                                    null
                                        ? (int?)Convert.ToInt32(NewDepartmentDropDownList1.SelectedValue)
                                        : null;
                                aEmpTransferAndRedesignationDao.OldDepartmentId = OldDepartmentDropDownList.SelectedValue !=
                                                                                  "" &&
                                                                                  OldDepartmentDropDownList.SelectedValue !=
                                                                                  null
                                    ? (int?)Convert.ToInt32(OldDepartmentDropDownList.SelectedValue)
                                    : null;



                                aEmpTransferAndRedesignationDao.NewSectionId = NewSectionDropDownList.SelectedValue != "" &&
                                                                               NewSectionDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(NewSectionDropDownList.SelectedValue)
                                    : null;
                                aEmpTransferAndRedesignationDao.OldSectionId = OldSectionDropDownList.SelectedValue != "" &&
                                                                               OldSectionDropDownList.SelectedValue != null
                                    ? (int?)Convert.ToInt32(OldSectionDropDownList.SelectedValue)
                                    : null;



                                aEmpTransferAndRedesignationDao.NewSubSectionId = NewSubSectionDropDownList.SelectedValue !=
                                                                                  "" &&
                                                                                  NewSubSectionDropDownList.SelectedValue !=
                                                                                  null
                                    ? (int?)Convert.ToInt32(NewSubSectionDropDownList.SelectedValue)
                                    : null;
                                aEmpTransferAndRedesignationDao.OldSubSectionId = OldSubSectionDropDownList.SelectedValue !=
                                                                                  "" &&
                                                                                  OldSubSectionDropDownList.SelectedValue !=
                                                                                  null
                                    ? (int?)Convert.ToInt32(OldSubSectionDropDownList.SelectedValue)
                                    : null;


                                aEmpTransferAndRedesignationDao.NewEmpReportingBodyId =
                                    Convert.ToInt32(NewEmpBodyTextBoxHiddenField.Value);
                                //     aEmpTransferAndRedesignationDao.PEmpRptBodyId = Convert.ToInt32(OldReportingBodyDropDownList.SelectedValue);

                                aEmpTransferAndRedesignationDao.OldReportingBodyID =
                                    OldReportingBodyDropDownList.SelectedValue != "" &&
                                    OldReportingBodyDropDownList.SelectedValue != null
                                        ? (int?)Convert.ToInt32(OldReportingBodyDropDownList.SelectedValue)
                                        : null;


                                aEmpTransferAndRedesignationDao.EffectiveDate = EfectiveDate.Text != "" &&
                                                                                EfectiveDate.Text != null
                                    ? (DateTime?)Convert.ToDateTime(EfectiveDate.Text)
                                    : null;

                            }

                        }
                    }
                    aEmpTransferAndRedesignationDao.EmpTypeId = Convert.ToInt32(EmpTypeId.Value) > 0
                        ? int.Parse(EmpTypeId.Value)
                        : (int?)null;
                    aEmpTransferAndRedesignationDao.Remarks = OtherRemarksTextBox.Text;
                    aEmpTransferAndRedesignationDao.EntryBy = Session["UserId"].ToString();
                    aEmpTransferAndRedesignationDao.EntryDate = DateTime.Now;
                    //aEmpTransferAndRedesignationDao.AutoProcess = false;

                    if (manualUpdateCheckBox.Checked)
                    {
                        aEmpTransferAndRedesignationDao.AutoProcess = "Manually Updated";
                    }

                    int id =
                        aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationSaveInfo(aEmpTransferAndRedesignationDao);


                    //For Employee Master Information update ------------------------------------------------------------------------

                    if (manualUpdateCheckBox.Checked)
                    {

                        if (OldCompanyDropDownList.SelectedValue!=NewCompanyDropDownList.SelectedValue)
                        {
                            Int32 empGenId = 0;
                            Int32 company = 0;
                            Int32 office = 0;
                            Int32 place = 0;
                            Int32 division = 0;
                            Int32 wing = 0;
                            Int32 Department = 0;
                            Int32 Section = 0;
                            Int32 subSection = 0;
                            Int32 rptBody = 0;

                            empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);

                            company = Convert.ToInt32(NewCompanyDropDownList.SelectedIndex > 0
                                        ? int.Parse(NewCompanyDropDownList.SelectedValue)
                                        : (int?)null);

                            office = Convert.ToInt32(NewSalaryLocationDropDownList.SelectedIndex > 0
                                            ? int.Parse(NewSalaryLocationDropDownList.SelectedValue)
                                            : (int?)null);

                            place = Convert.ToInt32(NewJobLocationDropDownList.SelectedIndex > 0
                                            ? int.Parse(NewJobLocationDropDownList.SelectedValue)
                                            : (int?)null);

                            division = Convert.ToInt32(NewDivisionDropDownList.SelectedIndex > 0
                                        ? int.Parse(NewDivisionDropDownList.SelectedValue)
                                        : (int?)null);

                            wing = Convert.ToInt32(NewWingDropDownList.SelectedValue != "" &&
                                                                                NewWingDropDownList.SelectedValue != null
                                        ? (int?)Convert.ToInt32(NewWingDropDownList.SelectedValue)
                                        : null);

                            Department = Convert.ToInt32(NewDepartmentDropDownList1.SelectedValue != "" &&
                                        NewDepartmentDropDownList1.SelectedValue !=
                                        null
                                            ? (int?)Convert.ToInt32(NewDepartmentDropDownList1.SelectedValue)
                                            : null);

                            Section = Convert.ToInt32(NewSectionDropDownList.SelectedValue != "" &&
                                                                                   NewSectionDropDownList.SelectedValue != null
                                        ? (int?)Convert.ToInt32(NewSectionDropDownList.SelectedValue)
                                        : null);

                            subSection = Convert.ToInt32(NewSubSectionDropDownList.SelectedValue !=
                                                                                      "" &&
                                                                                      NewSubSectionDropDownList.SelectedValue !=
                                                                                      null
                                        ? (int?)Convert.ToInt32(NewSubSectionDropDownList.SelectedValue)
                                        : null);

                            rptBody = Convert.ToInt32(NewEmpBodyTextBoxHiddenField.Value);


                            UpdateEmployeeMasterInfo(empGenId, company, office, place, division, wing, Department, Section, subSection, rptBody, string.IsNullOrEmpty(EfectiveDate.Text)
                            ? (DateTime?)null
                            : DateTime.Parse(EfectiveDate.Text).Date);
                            using (var db = new HRIS_SMCEntities())
                            {
                                var hhhh = db.tblEmpGeneralInfoes.OrderByDescending(u => u.EmpInfoId).FirstOrDefault();

                                EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                                ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                                /// 
                                /// 

                                   CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
                                for (int i = 0; i < loadGridView.Rows.Count; i++)
                                {
                                    _commonDataLoad.UpdateReportingEmpId(loadGridView.DataKeys[i][0].ToString(),
                                        hhhh.EmpInfoId.ToString());
                                }

                                using (DataTable dtEmpCode = _empdal.GetEmpMasterCodeForNewEntry(hhhh.EmpInfoId))
                                {

                                }
                            }
                        }

                             
                        }
                        



                    if (id > 0)
                    {
                        aEmpTransferAndRedesignationDal.DeleteDirectlyS(id.ToString());
                        for (int i = 0; i < loadGridView.Rows.Count; i++)
                        {
                            EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao()
                            {
                                EmpTransferAndRedesignationId = id,
                                EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())
                            };


                            andRedesignationDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
                            andRedesignationDao.EmpTransferAndRedesignationId = id;

                            if (Convert.ToInt32(loadGridView.DataKeys[i][1].ToString()) != 0)
                            {
                                andRedesignationDao.PrevEmpReportingBodyId = Convert.ToInt32(loadGridView.DataKeys[i][1].ToString());
                            }


                            int idd =
                                aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationDSSaveInfo(
                                    andRedesignationDao);


                            //------------------------------------- Update supervised info -------------------------------------------------------------------------

                            if (manualUpdateCheckBox.Checked)
                            {
                                UpDateSuperVisorInfo(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString()), Convert.ToInt32(repEmpIdHiddenField.Value));
                            }
                        }





                        aEmpTransferAndRedesignationDal.DeletePS(id.ToString());
                        for (int i = 0; i < presuperGridView.Rows.Count; i++)
                        {
                            EmpTransferAndRedesignationDao andRedesignationDao = new EmpTransferAndRedesignationDao()
                            {
                                EmpTransferAndRedesignationId = id,
                                EmpInfoId = Convert.ToInt32(presuperGridView.DataKeys[i][0].ToString())
                            };
                            int idd =
                                aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationPSSaveInfo(
                                    andRedesignationDao);

                            if (manualUpdateCheckBox.Checked)
                            {
                                UpDateSuperVisorInfoByNULL(Convert.ToInt32(presuperGridView.DataKeys[i][0].ToString()));
                            }

                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation successfully done...');window.location ='EmpTransferAndRedesignationView.aspx';",
                            true);



                    }
                }
            }
        }
        catch (Exception)
        {
            
           // throw;
        }
        
     }

    private void UpDateSuperVisorInfoByNULL(int empId)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.EmpInfoId = empId;

        aEmpTransferAndRedesignationDal.UpdateEmployeeSuperVisorIdToNull(empId);
    }


    private void UpDateSuperVisorInfo(int empId, int reportingBodyId)
    {

        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.EmpInfoId = empId;

        if (reportingBodyId != 0)
        {
            aInfo.LineId = reportingBodyId;
        }

        aEmpTransferAndRedesignationDal.UpdateEmployeeSuperVisorId(aInfo);
    }

    private void UpdateEmployeeMasterInfo(int empGenId, int company, int office, int place, int division, int wing, int department, int section, int subSection, int rptBody, DateTime? EfectiveDate)
    {
        EmpTransferAndRedesignationDao aInfo = new EmpTransferAndRedesignationDao();

        if (company != 0)
        {
            aInfo.NewCompanyId = company;
        }

        if (office  != 0)
        {
            aInfo.NewSalaryLocationId = office;
        }

        if (place != 0)
        {
            aInfo.NewJobLocationId = place;
        }

        if (division != 0)
        {
            aInfo.NewDivisionId = division;
        }

        if (department != 0)
        {
           aInfo.NewDepartmentId = department;
        }

        if (wing != 0)
        {
            aInfo.NewWingId = wing;
        }

        if (section != 0)
        {
             aInfo.NewSectionId = section;
        }

        if (subSection != 0)
        {
            aInfo.NewSubSectionId = subSection;
        }

        if (rptBody != 0)
        {
           aInfo.NewEmpReportingBodyId = rptBody;
        }

        if (empGenId != 0)
        {
            aInfo.EmpInfoId = empGenId;
        }


        aInfo.EffectiveDate = EfectiveDate;
        

        aEmpTransferAndRedesignationDal.UpdateEmployeeMasterInfo(aInfo);
    }

    private void Clear()
    {
       // EmployeeDropDownList.SelectedValue = "";
        EmployeeNameTextBox.Text = string.Empty;
        DesignationTextBox.Text = string.Empty;
        JoiningDateTextBox.Text = string.Empty;
        SalaryGradeTextBox.Text = string.Empty;
        OldCompanyDropDownList.SelectedValue = "";
        OldSalaryLocationDropDownList.SelectedValue = "";
        OldJobLocationDropDownList.SelectedValue = "";
        OldDivisionDropDownList.SelectedValue = "";
        OldUnitDropDownList.SelectedValue = "";
        OldSectionDropDownList.SelectedValue = "";
        OldSubSectionDropDownList.SelectedValue = "";
        NewCompanyDropDownList.SelectedValue = "";
        NewSalaryLocationDropDownList.SelectedValue = "";
        NewJobLocationDropDownList.SelectedValue = "";
        NewDivisionDropDownList.SelectedValue = "";
        NewWingDropDownList.SelectedValue = "";
        NewSectionDropDownList.SelectedValue = "";
        NewSubSectionDropDownList.SelectedValue = "";
        NewdesignationDropDownList.SelectedValue = "";


        TransferRadioButtonList.Items[0].Selected = false;
        TransferRadioButtonList.Items[1].Selected = false;
        TransferRadioButtonList.Items[2].Selected = false;
        Panel1.Visible = false;
        ShowExistingAndNew.Visible = false;
        companyDropDownList.SelectedValue = "";
        SearchEmployeeNameTextBoxTextBox.Text = string.Empty;
        EmployeeNameTextBox.Text = "";
        JoiningDateTextBox.Text = string.Empty;
        DesignationTextBox.Text = "";
        SalaryGradeTextBox.Text = "";
        submitButton.Text = "Save";
        NewReportingBodyTextBox.Text = "";
        HiddenFieldNewReportingBody.Value = "";
        directlySuperTextBox.Text = string.Empty;
        directlyEmpIdHiddenField.Value = string.Empty;
        loadGridView.DataSource = null;
        loadGridView.DataBind();

    }


    private bool Validation()
    {
    //    if (EmployeeDropDownList.SelectedValue == "")
    //    {
    //        aShowMessage.ShowMessageBox("Please Select Employe Search !!!", this);
    //        EmployeeDropDownList.Focus();
    //        return false;
    //    }

      

        if (TransferRadioButtonList.Items[0].Selected == true)
        {

            if (NewCompanyDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Company !!!", this);
                NewCompanyDropDownList.Focus();
                return false;
            }



            //if (OldCompanyDropDownList.SelectedValue == NewCompanyDropDownList.SelectedValue)
            //{
            //    aShowMessage.ShowMessageBox("Please Select New Company !!!", this);
            //    NewCompanyDropDownList.Focus();
            //    return false;
            //}





            if (NewEmpBodyTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Enter New Reporting Body !!!", this);
                NewEmpBodyTextBox.Focus();
                return false;
            }


           

            if (EfectiveDate.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Select Effective Date !!!", this);
                EfectiveDate.Focus();
                return false;
            }
           
           
            return true;
        }

        if (TransferRadioButtonList.Items[1].Selected == true)
        {
            if (NewdesignationDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Designation !!!", this);
                NewdesignationDropDownList.Focus();
                return false;
            }

            if (NewReportingBodyTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Select this !!!", this);
                NewReportingBodyTextBox.Focus();
                return false;
            }

        }


        if (TransferRadioButtonList.Items[2].Selected == true)
        {

            if (NewdesignationDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Designation !!!", this);
                NewdesignationDropDownList.Focus();
                return false;
            }
            if (NewCompanyDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Company !!!", this);
                NewCompanyDropDownList.Focus();
                return false;
            }

            if (NewSalaryLocationDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Salary Location !!!", this);
                NewSalaryLocationDropDownList.Focus();
                return false;
            }


            if (NewJobLocationDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Job Location !!!", this);
                NewJobLocationDropDownList.Focus();
                return false;
            }

            if (NewDivisionDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Division !!!", this);
                NewDivisionDropDownList.Focus();
                return false;
            }

       

           
         

            if (NewReportingBodyTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Select this !!!", this);
                NewReportingBodyTextBox.Focus();
                return false;
            }

            return true;
        }

        if (TransferRadioButtonList.Items[3].Selected == true)
        {

            if (NewCompanyDropDownList.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Please Select New Company !!!", this);
                NewCompanyDropDownList.Focus();
                return false;
            }



            if (NewEmpBodyTextBox.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Enter New Reporting Body !!!", this);
                NewEmpBodyTextBox.Focus();
                return false;
            }




            if (EfectiveDate.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Select Effective Date !!!", this);
                EfectiveDate.Focus();
                return false;
            }


            return true;
        }


        return true;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmpTransferAndRedesignationApprovalList.aspx");
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //aEmpTransferAndRedesignationDal.EmployeeNameDropDown(NewReportingBodyDropDownList, companyDropDownList.SelectedValue);
        if (companyDropDownList.SelectedValue != "")
        {

            Session["CompanyId"] = companyDropDownList.SelectedValue;
            aEmpTransferAndRedesignationDal.EmployeeNameDropDown(OldReportingBodyDropDownList, companyDropDownList.SelectedValue);
            aEmpTransferAndRedesignationDal.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
        }
        TransferRadioButtonList.Items[1].Attributes.Add("hidden", "hidden");
        TransferRadioButtonList.Items[2].Attributes.Add("hidden", "hidden");
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue!="")
        {
            string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                repEmpIdHiddenField.Value = emp[0];

                LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                //productNameTextBox.Text = productInfo[1];
                //string productCode = productCodeTextBox.Text.Trim();

            }
            else
            {

                SearchEmployeeNameTextBoxTextBox.Text = "";
                repEmpIdHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }
        }
        else
        {
            SearchEmployeeNameTextBoxTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            companyDropDownList.Focus();
            aShowMessage.ShowMessageBox("Please Select a Company !!", this);
        }
        TransferRadioButtonList.Items[1].Attributes.Add("hidden", "hidden");
        TransferRadioButtonList.Items[2].Attributes.Add("hidden", "hidden");
    }

    protected void NewReportingBodyTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = NewReportingBodyTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':', ':');

            NewReportingBodyTextBox.Text = emp[2];
            HiddenFieldNewReportingBody.Value = emp[0];

           // LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();

        }
        else
        {

            NewReportingBodyTextBox.Text = "";
            HiddenFieldNewReportingBody.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    protected void NewEmpBodyTextBox_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            string empName = NewEmpBodyTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':', ':');

                NewEmpBodyTextBox.Text = emp[2];
                NewEmpBodyTextBoxHiddenField.Value = emp[0];

                // LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                //productNameTextBox.Text = productInfo[1];
                //string productCode = productCodeTextBox.Text.Trim();

            }
            else
            {

                NewEmpBodyTextBox.Text = "";
                NewEmpBodyTextBoxHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }
        }
        catch (Exception)
        {
            
            
        }
       
    }

    protected void NewSalaryGradeDropDownList_Changed(object sender, EventArgs e)
    {
      //  aEmpTransferAndRedesignationDal.LoadNewdesignationDropDownListBySalaryId(NewdesignationDropDownList, NewSalaryGradeDropDownList.SelectedValue);
    }

    protected void EfectiveDate_TextChanged(object sender, EventArgs e)
    {
         


        if (FinancialYearDropDownList.SelectedValue!="")
        {
                 if (EfectiveDate.Text != "")
        {


            if (CheckStartEndDateExistOrNot(EfectiveDate.Text, EfectiveDate.Text) == true)
            {

            }
            if (CheckStartEndDateExistOrNot(EfectiveDate.Text, EfectiveDate.Text) == false)
            {
                aShowMessage.ShowMessageBox("Effective date must be within the finnancial year!!", this);
                EfectiveDate.Text = "";
                EfectiveDate.Focus();

            }
 
        }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select this.", this);
            FinancialYearDropDownList.Focus();
            EfectiveDate.Text = "";
        }
    }
    private bool CheckStartEndDateExistOrNot(string Start, string End)
    {
        bool status = false;

        DataTable dataTable = aEmpTransferAndRedesignationDal.CheckStartEndDateExistOrNotDAL(FinancialYearDropDownList.SelectedValue ,Start, End);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (EmpTransferAndRedesignationIdHiddenField.Value != string.Empty)
       
        {
               DataTable aTable =
                             aEmpTransferAndRedesignationDal.DeleteValidattionForEffectiveDate(EmpTransferAndRedesignationIdHiddenField.Value.ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    Update();
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
           DataTable aTable =
                             aEmpTransferAndRedesignationDal.DeleteValidattionForEffectiveDate(EmpTransferAndRedesignationIdHiddenField.Value.ToString());
        if (aTable.Rows.Count > 0)
        {
            string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
            string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

            if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
            {

                if (
                    aEmpTransferAndRedesignationDal.DeleteUpdateEmployeeTransferEntryById(
                        EmpTransferAndRedesignationIdHiddenField.Value))
                {

                    DataTable aDataTable = aEmpTransferAndRedesignationDal.GetEmpTransRedDesigInformationById(EmpTransferAndRedesignationIdHiddenField.Value);

                    Int32 rowIndex = 0;

                    if (aDataTable.Rows.Count > 0)
                    {

                        Int32 empGenId = 0;
                        Int32 company = 0;
                        Int32 office = 0;
                        Int32 place = 0;
                        Int32 division = 0;
                        Int32 wing = 0;
                        Int32 Department = 0;
                        Int32 Section = 0;
                        Int32 subSection = 0;
                        Int32 rptBody = 0;


                        if (aDataTable.Rows[0]["OldCompanyId"] != DBNull.Value)
                        {
                            company = aDataTable.Rows[rowIndex].Field<Int32>("OldCompanyId"); 
                        }


                        if (aDataTable.Rows[0]["OldSalaryLocationId"] != DBNull.Value)
                        {
                            office = aDataTable.Rows[rowIndex].Field<Int32>("OldSalaryLocationId");
                        }


                        if (aDataTable.Rows[0]["OldJobLocationId"] != DBNull.Value)
                        {
                            place = aDataTable.Rows[rowIndex].Field<Int32>("OldJobLocationId");
                        }

                        if (aDataTable.Rows[0]["OldDivisionId"] != DBNull.Value)
                        {
                            division = aDataTable.Rows[rowIndex].Field<Int32>("OldDivisionId");
                        }

                        if (aDataTable.Rows[0]["OldWingId"] != DBNull.Value)
                        {
                            wing = aDataTable.Rows[rowIndex].Field<Int32>("OldWingId");
                        }


                        if (aDataTable.Rows[0]["OldSectionId"] != DBNull.Value)
                        {
                            Section = aDataTable.Rows[rowIndex].Field<Int32>("OldSectionId");
                        }


                        if (aDataTable.Rows[0]["OldSubSectionId"] != DBNull.Value)
                        {
                            subSection = aDataTable.Rows[rowIndex].Field<Int32>("OldSubSectionId");
                        }

                        if (aDataTable.Rows[0]["OldDepartmentId"] != DBNull.Value)
                        {
                            Department = aDataTable.Rows[rowIndex].Field<Int32>("OldDepartmentId");
                        }


                        if (aDataTable.Rows[0]["OldReportingBodyID"] != DBNull.Value)
                        {
                            rptBody = aDataTable.Rows[rowIndex].Field<Int32>("OldReportingBodyID");
                        }

                     
                        empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                  //      UpdateEmployeeMasterInfo(empGenId, company, office, place, division, wing, Department, Section, subSection, rptBody);



                        DataTable dtdata = aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationDS(EmpTransferAndRedesignationIdHiddenField.Value);

                        if (dtdata.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtdata.Rows.Count; i++)
                            {
                                int reptId = 0;

                                if (dtdata.Rows[0]["PrevEmpReportingBodyId"] != DBNull.Value)
                                {
                                    reptId = dtdata.Rows[0].Field<Int32>("PrevEmpReportingBodyId");
                                }


                                int empId = dtdata.Rows[i].Field<Int32>("EmpInfoId");
                                UpDateSuperVisorInfo(empId, reptId);
                            }
                        }



                        DataTable psuperVisorDtTable = aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationPS(EmpTransferAndRedesignationIdHiddenField.Value);

                        if (psuperVisorDtTable.Rows.Count > 0)
                        {

                            for (int i = 0; i < psuperVisorDtTable.Rows.Count; i++)
                            {
                                int reptId = aDataTable.Rows[0].Field<Int32>("EmployeeId");
                                int empId = psuperVisorDtTable.Rows[i].Field<Int32>("EmpInfoId");
                                UpDateSuperVisorInfo(empId, reptId);
                            }
                        }


                    }


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Data Delete Successfull...');window.location ='EmpTransferAndRedesignationView.aspx';",
                        true);


                }

                else
                {
                    aShowMessage.ShowMessageBox(aMessages.SDivisionDelete, this);

                }
            }
            else
            {
                aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
            }
        }
    }

    protected void directlySuperTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = directlySuperTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':', ':');

            directlySuperTextBox.Text = emp[2];
            directlyEmpIdHiddenField.Value = emp[0];

            DataTable aTable = aEmpTransferAndRedesignationDal.GetEmployeeReportingBodyInfo(Convert.ToInt32(directlyEmpIdHiddenField.Value));

            if (aTable.Rows.Count > 0)
            {

                if (aTable.Rows[0]["ReportingEmpId"] != DBNull.Value)
                {
                    rptHiddenField.Value = aTable.Rows[0]["ReportingEmpId"].ToString();
                }
                else
                {
                    rptHiddenField.Value = 0.ToString(CultureInfo.InvariantCulture);
                }

            }

            // LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();

        }
        else
        {

            directlySuperTextBox.Text = "";
            directlyEmpIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    public void Add()
    {
        DataTable aDataTable=new DataTable();
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("PrevEmpReportingBodyId");

        DataRow dataRow = null;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            dataRow = aDataTable.NewRow();
            dataRow["EmpName"] = loadGridView.Rows[i].Cells[0].Text;
            dataRow["EmpInfoId"] = loadGridView.DataKeys[i][0].ToString();
            dataRow["PrevEmpReportingBodyId"] = loadGridView.DataKeys[i][1].ToString();

            aDataTable.Rows.Add(dataRow);
        }
        dataRow = aDataTable.NewRow();
        dataRow["EmpName"] = directlySuperTextBox.Text;
        dataRow["EmpInfoId"] = directlyEmpIdHiddenField.Value;
        dataRow["PrevEmpReportingBodyId"] = rptHiddenField.Value;

        aDataTable.Rows.Add(dataRow);
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
        directlySuperTextBox.Text = string.Empty;
        directlyEmpIdHiddenField.Value = string.Empty;
    }
    public void Remove(int row)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("PrevEmpReportingBodyId");

        DataRow dataRow = null;
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            if (i != row)
            {
                dataRow = aDataTable.NewRow();
                dataRow["EmpName"] = loadGridView.Rows[i].Cells[0].Text;
                dataRow["EmpInfoId"] = loadGridView.DataKeys[i][0].ToString();
                dataRow["PrevEmpReportingBodyId"] = loadGridView.DataKeys[i][1].ToString();
                aDataTable.Rows.Add(dataRow);
            }
        }
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();

    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (directlySuperTextBox.Text!="")
        { 
             Add();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Fill This Field",this);
        }
       
    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton ImageButton = (ImageButton)sender;
            GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;


            DataTable dtdata = aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationDS(EmpTransferAndRedesignationIdHiddenField.Value);
            int EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString());

            if (dtdata.Rows.Count > 0)
            {
                for (int i = 0; i < dtdata.Rows.Count; i++)
                {
                    if (dtdata.Rows[i].Field<Int32>("EmpInfoId") == EmpInfoId)
                    {
                        int reptId = 0;

                        if (dtdata.Rows[0]["PrevEmpReportingBodyId"] != DBNull.Value)
                        {
                            reptId = dtdata.Rows[0].Field<Int32>("PrevEmpReportingBodyId");
                        }

                        int empId = dtdata.Rows[i].Field<Int32>("EmpInfoId");
                        UpDateSuperVisorInfo(empId, reptId);
                    }
                }
            }


            Remove(rowindex);
        }
        catch (Exception)
        {
            ImageButton ImageButton = (ImageButton)sender;
            GridViewRow currentRow = (GridViewRow)ImageButton.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;


            DataTable dtdata = aEmpTransferAndRedesignationDal.EmpTransferAndRedesignationDS(EmpTransferAndRedesignationIdHiddenField.Value);
           // int EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString());
            Remove(rowindex);
        }
      
    }

    protected void NewSalaryLocationDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aEmpTransferAndRedesignationDal.LoadJobLocationDropDownList(NewJobLocationDropDownList,NewSalaryLocationDropDownList.SelectedValue);
    }

    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        EfectiveDate.Text = "";
    }

    protected void NewDivisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (NewDivisionDropDownList.SelectedValue != "")
            {
                aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                aEmpTransferAndRedesignationDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                aEmpTransferAndRedesignationDal.GetSectionByDivList(NewSectionDropDownList, NewDivisionDropDownList.SelectedValue);
                aEmpTransferAndRedesignationDal.GetSubSectionListAll(NewSubSectionDropDownList, NewDivisionDropDownList.SelectedValue);
            }
            else
            {
                NewDivisionDropDownList.Items.Clear();
            }
        }
        catch (Exception)
        {
           // throw;
        }
       
    }

    protected void NewWingDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (NewWingDropDownList.SelectedValue != "")
            {
                aEmpTransferAndRedesignationDal.GetDepartmentList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
            }
            else
            {
                NewDepartmentDropDownList1.Items.Clear();
            }
        }
        catch (Exception)
        {
            
            //throw;
        }
        
    }

    //protected void NewDepartmentDropDownList1_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (NewDepartmentDropDownList1.SelectedValue != "")
    //    {
    //        aEmpTransferAndRedesignationDal.GetSectionList(NewSectionDropDownList, NewDepartmentDropDownList1.SelectedValue);
    //        DataTable dtgetdata = aEmpTransferAndRedesignationDal.GetDepartmentRelaton(NewDepartmentDropDownList1.SelectedValue, "");
    //        if (dtgetdata.Rows.Count > 0)
    //        {
    //            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
    //            {
    //                wing.Visible = false;
    //                NewDivisionDropDownList.Items.Clear();
    //                aEmpTransferAndRedesignationDal.GetDivisionWingListAll(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
    //                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
    //            }
    //            else
    //            {
    //                wing.Visible = true;
    //                NewWingDropDownList.Items.Clear();
    //                aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
    //                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        NewSectionDropDownList.Items.Clear();
    //    }
    //}

    protected void NewDepartmentDropDownList1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (NewDepartmentDropDownList1.SelectedValue != "")
        {
            aEmpTransferAndRedesignationDal.GetSectionList(NewSectionDropDownList, NewDepartmentDropDownList1.SelectedValue);
            DataTable dtgetdata = aEmpTransferAndRedesignationDal.GetDepartmentRelaton(NewDepartmentDropDownList1.SelectedValue, "");
            if (dtgetdata.Rows.Count > 0)
            {
                if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                {
                    wing.Visible = false;
                    NewWingDropDownList.Items.Clear();
                    aEmpTransferAndRedesignationDal.GetDivisionWingListAll(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                    NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                else
                {
                    wing.Visible = true;
                    NewWingDropDownList.Items.Clear();
                    aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                    NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
            }
        }
        else
        {
            NewSectionDropDownList.Items.Clear();
        }
        if (NewDepartmentDropDownList1.SelectedIndex == 0)
        {
            wing.Visible = true;
            NewWingDropDownList.Items.Clear();
            aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
        }

      
    }

    protected void NewSectionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata1 = aEmpTransferAndRedesignationDal.GetSectionRelaton(NewSectionDropDownList.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                NewDepartmentDropDownList1.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDepartmentByDivListAll(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                NewDepartmentDropDownList1.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = aEmpTransferAndRedesignationDal.GetDepartmentRelaton(NewDepartmentDropDownList1.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                NewWingDropDownList.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDivisionWingListAll(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
            else
            {
                wing.Visible = true;
                NewWingDropDownList.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }
        if (NewSectionDropDownList.SelectedIndex == 0)
        {
            if (wing.Visible == false)
            {
                wing.Visible = true;
                NewWingDropDownList.SelectedValue = null;
                NewWingDropDownList.DataBind();
                aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);

            }
            if (dept.Visible == false)
            {
                dept.Visible = true;
                NewDepartmentDropDownList1.SelectedValue = null;
                NewDepartmentDropDownList1.DataBind();
                aEmpTransferAndRedesignationDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
            }
        }

      
    }

    protected void NewSubSectionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata2 = aEmpTransferAndRedesignationDal.GetSubSectionRelaton(NewSubSectionDropDownList.SelectedValue, "");
        if (dtgetdata2.Rows.Count > 0)
        {
            if (dtgetdata2.Rows[0]["Invisible"].ToString() == "True")
            {
                sec.Visible = false;
                NewSectionDropDownList.Items.Clear();
                aEmpTransferAndRedesignationDal.GetSectionByDivListAll(NewSectionDropDownList, NewDivisionDropDownList.SelectedValue);
                NewSectionDropDownList.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
            }
            else
            {
                sec.Visible = true;
                NewSectionDropDownList.Items.Clear();
                aEmpTransferAndRedesignationDal.GetSectionByDivList(NewSectionDropDownList, NewDivisionDropDownList.SelectedValue);
                NewSectionDropDownList.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
            }
        }
        DataTable dtgetdata1 = aEmpTransferAndRedesignationDal.GetSectionRelaton(NewSectionDropDownList.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                NewDepartmentDropDownList1.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDepartmentByDivListAll(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                NewDepartmentDropDownList1.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
                NewDepartmentDropDownList1.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = aEmpTransferAndRedesignationDal.GetDepartmentRelaton(NewDepartmentDropDownList1.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                NewWingDropDownList.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDivisionWingListAll(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
            else
            {
                wing.Visible = true;
                NewWingDropDownList.Items.Clear();
                aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);
                NewWingDropDownList.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }
        if (NewSubSectionDropDownList.SelectedIndex == 0)
        {
            if (wing.Visible == false)
            {
                wing.Visible = true;
                NewWingDropDownList.SelectedValue = null;
                NewWingDropDownList.DataBind();
                aEmpTransferAndRedesignationDal.GetDivisionWingList(NewWingDropDownList, NewDivisionDropDownList.SelectedValue);

            }
            if (dept.Visible == false)
            {
                dept.Visible = true;
                NewDepartmentDropDownList1.SelectedValue = null;
                NewDepartmentDropDownList1.DataBind();
                aEmpTransferAndRedesignationDal.GetDepartmentByDivList(NewDepartmentDropDownList1, NewDivisionDropDownList.SelectedValue);
            }
            if (sec.Visible == false)
            {
                sec.Visible = true;
                NewSectionDropDownList.SelectedValue = null;
                NewSectionDropDownList.DataBind();
                aEmpTransferAndRedesignationDal.GetSectionByDivList(NewSectionDropDownList, NewDivisionDropDownList.SelectedValue);
            }
        }
       
        
    }
}