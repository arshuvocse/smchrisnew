using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Audit_Dal;
using DAL.MeetingMinorsDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_EmployeeAuditTrailNew : System.Web.UI.Page
{
    private MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    
    private EmployeeAuditDal auditDal = new EmployeeAuditDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
        }
    }

    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);

    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView) sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("MeetingEntry.aspx");
    }

    //private void DocumentLoadGrid()
    //{
    //    if (ddlCompany.SelectedValue != "")
    //    {
    //        DataTable aDataTable = AMAsterDal.LoadInfoDocumentAuditTrail(DocumentGenerateParamiterList(), DocumentGenerateParamiterListEdit());
    //        if (aDataTable.Rows.Count > 0)
    //        {
    //            GridViewDocument.DataSource = aDataTable;
    //            GridViewDocument.DataBind();

    //        }
    //        else
    //        {
    //            aShowMessage.ShowMessageBox("No Data Found!!!", this);
    //            GridViewDocument.DataSource = null;
    //            GridViewDocument.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        GridViewDocument.DataSource = null;
    //        GridViewDocument.DataBind();
    //        aShowMessage.ShowMessageBox("Please select company name!!!", this);
    //    }
    //}

    private string IncrementGenerateParamiterListEdit()
    {

        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND INC.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.UpdateDate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND INC.UpdateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.UpdateDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }

        return parameter;
    }


    private string PromotionGenerateParameter()
    {

        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }

        return parameter;
    }


    private string SeperationGenerateParameter()
    {
        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EM.CompanyId  = " + ddlCompany.SelectedValue;
        }


        
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  mas.ModifyDate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND  mas.ModifyDate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND  mas.ModifyDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
       
  
        return parameter;
    }



    private string SuspendGenerateParameter()
    {

        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyInfoId = " + ddlCompany.SelectedValue;
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }

        return parameter;
    }
   
    private string IncrementGenerateParamiterListDelete()
    {

        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND INC.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.DeleteDate  BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND INC.DeleteDate  BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.DeleteDate  BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }

        return parameter;
    }

    //ata hoi
    private void IncrementLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.IncrementLoadDal(PromotionGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                gv_ViewList.DataSource = aDataTable;
                gv_ViewList.DataBind();

                for (int i = 0; i < gv_ViewList.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)gv_ViewList.Rows[i].Cells[0].FindControl("lbl_Incement_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        gv_ViewList.Rows[i].Visible = false;
                    }
                }

            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                gv_ViewList.DataSource = null;
                gv_ViewList.DataBind();
            }
        }
        else
        {
            gv_ViewList.DataSource = null;
            gv_ViewList.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }


    private void TransferLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.TransferLoadDal(PromotionGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewTransfer.DataSource = aDataTable;
                GridViewTransfer.DataBind();

                for (int i = 0; i < gv_ViewList.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewTransfer.Rows[i].Cells[0].FindControl("lbl_Trasfer_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewTransfer.Rows[i].Visible = false;
                    }
                }

            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewTransfer.DataSource = null;
                GridViewTransfer.DataBind();
            }
        }
        else
        {
            GridViewTransfer.DataSource = null;
            GridViewTransfer.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }


    private void MPBudgetLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.GetMpBudgetLoadDal(SeperationGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewMPBudget.DataSource = aDataTable;
                GridViewMPBudget.DataBind();

                for (int i = 0; i < GridViewMPBudget.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewMPBudget.Rows[i].Cells[0].FindControl("lbl_MPBudget_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewMPBudget.Rows[i].Visible = false;
                    }
                }

            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewMPBudget.DataSource = null;
                GridViewMPBudget.DataBind();
            }
        }
        else
        {
            GridViewMPBudget.DataSource = null;
            GridViewMPBudget.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }


    private void SeparationLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.SeparationLoadDal(SeperationGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewSeperation.DataSource = aDataTable;
                GridViewSeperation.DataBind();

                for (int i = 0; i < GridViewSeperation.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewSeperation.Rows[i].Cells[0].FindControl("lbl_Seperation_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewSeperation.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewSeperation.DataSource = null;
                GridViewSeperation.DataBind();
            }
        }
        else
        {
            GridViewSeperation.DataSource = null;
            GridViewSeperation.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }



    private void JobCirculationLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.JobCirculationLoadDal(SeperationGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewJobCirculation.DataSource = aDataTable;
                GridViewJobCirculation.DataBind();

                for (int i = 0; i < GridViewJobCirculation.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewJobCirculation.Rows[i].Cells[0].FindControl("lbl_JobCirulation_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewJobCirculation.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewJobCirculation.DataSource = null;
                GridViewJobCirculation.DataBind();
            }
        }
        else
        {
            GridViewJobCirculation.DataSource = null;
            GridViewJobCirculation.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    } 


    private void PromotionLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.PromotionLoadDal(PromotionGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewPromotion.DataSource = aDataTable;
                GridViewPromotion.DataBind();

                for (int i = 0; i < GridViewPromotion.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewPromotion.Rows[i].Cells[0].FindControl("lbl_Promotion_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewPromotion.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewPromotion.DataSource = null;
                GridViewPromotion.DataBind();
            }
        }
        else
        {
            GridViewPromotion.DataSource = null;
            GridViewPromotion.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }


    private void SuspendLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.SuspendLoadDal(SuspendGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewSuspend.DataSource = aDataTable;
                GridViewSuspend.DataBind();

                for (int i = 0; i < GridViewSuspend.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewSuspend.Rows[i].Cells[0].FindControl("lbl_Suspend_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewSuspend.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewSuspend.DataSource = null;
                GridViewSuspend.DataBind();
            }
        }
        else
        {
            GridViewSuspend.DataSource = null;
            GridViewSuspend.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }


    private void ContractualEmployeeLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.ContractualEmployeeLoadDal(RedesignationPerameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewContructualEmployee.DataSource = aDataTable;
                GridViewContructualEmployee.DataBind();

                for (int i = 0; i < GridViewContructualEmployee.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewContructualEmployee.Rows[i].Cells[0].FindControl("lbl_ContactualEmployee_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewContructualEmployee.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewContructualEmployee.DataSource = null;
                GridViewContructualEmployee.DataBind();
            }
        }
        else
        {
            GridViewSuspend.DataSource = null;
            GridViewSuspend.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    private void KPIMasterLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.KPIMasterLoadDal();
            if (aDataTable.Rows.Count > 0)
            {
                GridViewKIPMaster.DataSource = aDataTable;
                GridViewKIPMaster.DataBind();

                for (int i = 0; i < GridViewKIPMaster.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewKIPMaster.Rows[i].Cells[0].FindControl("lbl_KPI_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewKIPMaster.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewKIPMaster.DataSource = null;
                GridViewKIPMaster.DataBind();
            }
        }
        else
        {
            GridViewKIPMaster.DataSource = null;
            GridViewKIPMaster.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    private void DiciplinaryActionLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.DiciplinaryActionLoadDal(SuspendGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewDiciplinaryAction.DataSource = aDataTable;
                GridViewDiciplinaryAction.DataBind();

                for (int i = 0; i < GridViewDiciplinaryAction.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewDiciplinaryAction.Rows[i].Cells[0].FindControl("lbl_DiciplinaryAction_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewDiciplinaryAction.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewDiciplinaryAction.DataSource = null;
                GridViewDiciplinaryAction.DataBind();
            }
        }
        else
        {
            GridViewDiciplinaryAction.DataSource = null;
            GridViewDiciplinaryAction.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    private void EmployeeJDLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.JDLoadDal();
            if (aDataTable.Rows.Count > 0)
            {
                GridViewJD.DataSource = aDataTable;
                GridViewJD.DataBind();

                for (int i = 0; i < GridViewJD.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewJD.Rows[i].Cells[0].FindControl("lbl_JD_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewJD.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewJD.DataSource = null;
                GridViewJD.DataBind();
            }
        }
        else
        {
            GridViewJD.DataSource = null;
            GridViewJD.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
        gv_ViewList.DataSource = null;
        gv_ViewList.DataBind();

        GridView1.DataSource = null;
        GridView1.DataBind();

        GridViewSuspend.DataSource = null;
        GridViewSuspend.DataBind();

        GridViewPromotion.DataSource = null;
        GridViewPromotion.DataBind();

        GridViewSuspend.DataSource = null;
        GridViewSuspend.DataBind();

        GridViewDiciplinaryAction.DataSource = null;
        GridViewDiciplinaryAction.DataBind();

        GridViewTransfer.DataSource = null;
        GridViewTransfer.DataBind();

        GridViewSeperation.DataSource = null;
        GridViewSeperation.DataBind();

        GridViewJD.DataSource = null;
        GridViewJD.DataBind();

        GridViewMPBudget.DataSource = null;
        GridViewMPBudget.DataBind();

        GridViewKIPMaster.DataSource = null;
        GridViewKIPMaster.DataBind();

        GridViewContructualEmployee.DataSource = null;
        GridViewContructualEmployee.DataBind();

        GridViewJobCirculation.DataSource = null;
        GridViewJobCirculation.DataBind();

        GridViewTrainningBudget.DataSource = null;
        GridViewTrainningBudget.DataBind();

        if (ddlOperation.SelectedValue == "Redesignation")
        {
            LoadGrid();
        }
        else if (ddlOperation.SelectedValue == "Increment")
        {
            IncrementLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "Promotion")
        {
            PromotionLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "Suspend")
        {
            SuspendLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "DiciplinaryAction")
        {
            DiciplinaryActionLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "Transfer")
        {
            TransferLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "separation")
        {
            SeparationLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "JD")
        {
            EmployeeJDLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "MPBudget")
        {
            MPBudgetLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "ContractualEmployee")
        {
            ContractualEmployeeLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "KPI")
        {
            KPIMasterLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "JobCirculation")
        {
            JobCirculationLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "TrainningBudget")
        {
            TrainningBudgetLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "JobRequisition")
        {
            JobRequisitionLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "EmpInfomation")
        {
            EmployeeInformationLoadGrid();
        }
        else if (ddlOperation.SelectedValue == "EmpProbation")
        {
            EmployeeProbationLoadGrid();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please select Operation!!!", this);
            ddlOperation.Focus();
        }

    }

    private void EmployeeProbationLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.EmployeeProbationLoadDal(ProbationGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewEmployeeProbation.DataSource = aDataTable;
                GridViewEmployeeProbation.DataBind();

                for (int i = 0; i < GridViewEmployeeProbation.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewEmployeeProbation.Rows[i].Cells[0].FindControl("lbl_EmployeeProbation_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewEmployeeProbation.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewEmployeeProbation.DataSource = null;
                GridViewEmployeeProbation.DataBind();
            }
        }
        else
        {
            GridViewEmployeeProbation.DataSource = null;
            GridViewEmployeeProbation.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    private string ProbationGenerateParameter()
    {
        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND Emp.CompanyId = " + ddlCompany.SelectedValue;
        }

        return parameter;
    }
    private void JobRequisitionLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.JobRequisitionLoadDal(SeperationGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewJobRequisition.DataSource = aDataTable;
                GridViewJobRequisition.DataBind();

                for (int i = 0; i < GridViewJobRequisition.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewJobRequisition.Rows[i].Cells[0].FindControl("lbl_JobRequisition_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewJobRequisition.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewJobRequisition.DataSource = null;
                GridViewJobRequisition.DataBind();
            }
        }
        else
        {
            GridViewJobRequisition.DataSource = null;
            GridViewJobRequisition.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    private void EmployeeInformationLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.EmployeeInformationLoadDal(SeperationGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewEmployeeInformation.DataSource = aDataTable;
                GridViewEmployeeInformation.DataBind();

                for (int i = 0; i < GridViewEmployeeInformation.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewEmployeeInformation.Rows[i].Cells[0].FindControl("lbl_EmployeeInformation_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewEmployeeInformation.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewEmployeeInformation.DataSource = null;
                GridViewEmployeeInformation.DataBind();
            }
        }
        else
        {
            GridViewEmployeeInformation.DataSource = null;
            GridViewEmployeeInformation.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    } 

    private void TrainningBudgetLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.TrainningBudgetLoadDal(SeperationGenerateParameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewTrainningBudget.DataSource = aDataTable;
                GridViewTrainningBudget.DataBind();

                for (int i = 0; i < GridViewTrainningBudget.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewTrainningBudget.Rows[i].Cells[0].FindControl("lbl_TrainningBudget_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewTrainningBudget.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewTrainningBudget.DataSource = null;
                GridViewTrainningBudget.DataBind();
            }
        }
        else
        {
            GridViewTrainningBudget.DataSource = null;
            GridViewTrainningBudget.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    } 

    private ShowMessage aShowMessage = new ShowMessage();

    private string DocumentGenerateParamiterList()
    {

        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtTitle.Text != "")
        {
            parameter = parameter + "  and  mas.Title LIKE '%''" + txtTitle.Text.Trim() + "''%'   ";
        }
        if (txtPropuse.Text != "")
        {
            parameter = parameter + "  and  mas.Purpose LIKE '%''" + txtPropuse.Text.Trim() + "''%'   ";
        }
        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.DeleteBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }

        return parameter;
    }

    private string RedesignationPerameter()
    {

        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }

      
        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.Effectivedate=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.Effectivedate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }

        return parameter;
    }

    private string GenerateParamiterList()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }


        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.DeleteBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }




        if (txtKeySearch.Text != "")
        {
            parameter = parameter + "  and  mas.KeySearch LIKE '%" + txtKeySearch.Text.Trim() + "%'   ";
        }

        return parameter;
    }

    private string GenerateParamiterListEdit()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }


        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.UpdateBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtCreatedDate.Text + "'  AND " +
                        " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" +
                        DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" +
                        Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }





        return parameter;
    }

    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    private void LoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = auditDal.Re_Designation(RedesignationPerameter());
            if (aDataTable.Rows.Count > 0)
            {
                GridView1.DataSource = aDataTable;
                GridView1.DataBind();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridView1.Rows[i].Cells[0].FindControl("lbl_Incement_Status");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridView1.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BoardMeetingAuditTrail.aspx");

    }

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton) sender;
        GridViewRow gvRow = (GridViewRow) lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField) gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Label lbl_Status = (Label) gv_ViewList.Rows[rowID].FindControl("lbl_Status");
        Response.Redirect("MeetingEntryViewDetailsAudit.aspx?MID=" + mastrId.Value.Trim() + "&Status=" +
                          lbl_Status.Text.Trim());
    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntry.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntry.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["CompanyId"] = "";
        //Session["CompanyId"] = ddlCompany.SelectedValue;
        //AMAsterDal.GetUserListDropdown(ddlCreatedBy, ddlCompany.SelectedValue);
        //if (Session["UserTypeId"].ToString() == "3" ||
        //       Session["UserTypeId"].ToString() == "4")
        //{


        //  ddlCreatedBy.Enabled = true;
        //   // AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue);
        //}
        //else
        //{
        //    ddlCreatedBy.SelectedValue = Session["UserId"].ToString();

        //    ddlCreatedBy.Enabled = false;
        // //   AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue, Session["UserId"].ToString());


        //}

    }

    protected void ddlOperation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        gv_ViewList.DataSource = null;
        gv_ViewList.DataBind();

        GridView1.DataSource = null;
        GridView1.DataBind();

        GridViewSuspend.DataSource = null;
        GridViewSuspend.DataBind();

        GridViewPromotion.DataSource = null;
        GridViewPromotion.DataBind();

        GridViewSuspend.DataSource = null;
        GridViewSuspend.DataBind();

        GridViewDiciplinaryAction.DataSource = null;
        GridViewDiciplinaryAction.DataBind();

        GridViewTransfer.DataSource = null;
        GridViewTransfer.DataBind();

        GridViewSeperation.DataSource = null;
        GridViewSeperation.DataBind();

        GridViewJD.DataSource = null;
        GridViewJD.DataBind();

        GridViewMPBudget.DataSource = null;
        GridViewMPBudget.DataBind();

        GridViewKIPMaster.DataSource = null;
        GridViewKIPMaster.DataBind();

        GridViewContructualEmployee.DataSource = null;
        GridViewContructualEmployee.DataBind();

        GridViewJobCirculation.DataSource = null;
        GridViewJobCirculation.DataBind();

        GridViewTrainningBudget.DataSource = null;
        GridViewTrainningBudget.DataBind();

        GridViewJobRequisition.DataSource = null;
        GridViewJobRequisition.DataBind();


        GridViewEmployeeInformation.DataSource = null;
        GridViewEmployeeInformation.DataBind();



        GridViewEmployeeProbation.DataSource = null;
        GridViewEmployeeProbation.DataBind();



    }

    protected void btnViewDoc_OnClick(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;
        //HiddenField mastrId = (HiddenField)GridViewDocument.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        //string url = "../Report_UI/BoardMeetingAuditTrailViwer.aspx?rptType=" + "Document&MID=" + mastrId.Value.Trim();
        //string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
//        if (gv_ViewList.Rows.Count > 0)
//        {
//            // Clear all content output from the buffer stream
//            Response.ClearContent();
//            // Specify the default file name using "content-disposition" RESPONSE header
//            Response.AppendHeader("content-disposition", "attachment; filename=Meeting_Audit_Trail_Information.xls");
//            // Set excel as the HTTP MIME type
//            Response.ContentType = "application/excel";
//            // Create an instance of stringWriter for writing information to a string
//            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
//            // Create an instance of HtmlTextWriter class for writing markup 
//            // characters and text to an ASP.NET server control output stream


//            gv_ViewList.Columns[gv_ViewList.Columns.Count - 1].Visible =
//               false;




//            gv_ViewList.AllowPaging = false;
//            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);

//            // Set white color as the background color for gridview header row
//            gv_ViewList.HeaderRow.Style.Add("background-color", "#FFFFFF");

//            // Set background color of each cell of GridView1 header row
//            foreach (TableCell tableCell in gv_ViewList.HeaderRow.Cells)
//            {
//                tableCell.Style["background-color"] = "#E5EEF1";
//            }

//            // Set background color of each cell of each data row of GridView1
//            foreach (GridViewRow gridViewRow in gv_ViewList.Rows)
//            {
//                gridViewRow.BackColor = System.Drawing.Color.White;
//                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
//                {
//                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
//                }
//            }
//            string headerTable = @"<span  style='text-align:center'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

//            string SubTi = @"<span   style='text-align:center'>
//               <h3>Meeting Audit Trail Information	</h3>
//            
//            </span>";
//            gv_ViewList.RenderControl(htw);
//            HttpContext.Current.Response.Write(headerTable);
//            HttpContext.Current.Response.Write(SubTi);
//            Response.Write(stringWriter.ToString());
//            Response.End();


//        }
//        else if (GridViewDocument.Rows.Count > 0)
//        {


//            // Clear all content output from the buffer stream
//            Response.ClearContent();
//            // Specify the default file name using "content-disposition" RESPONSE header
//            Response.AppendHeader("content-disposition", "attachment; filename=Document_Audit_Trail_Information.xls");
//            // Set excel as the HTTP MIME type
//            Response.ContentType = "application/excel";
//            // Create an instance of stringWriter for writing information to a string
//            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
//            // Create an instance of HtmlTextWriter class for writing markup 
//            // characters and text to an ASP.NET server control output stream


//            GridViewDocument.Columns[GridViewDocument.Columns.Count - 1].Visible =
//               false;




//            GridViewDocument.AllowPaging = false;
//            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);

//            // Set white color as the background color for gridview header row
//            GridViewDocument.HeaderRow.Style.Add("background-color", "#FFFFFF");

//            // Set background color of each cell of GridView1 header row
//            foreach (TableCell tableCell in GridViewDocument.HeaderRow.Cells)
//            {
//                tableCell.Style["background-color"] = "#E5EEF1";
//            }

//            // Set background color of each cell of each data row of GridView1
//            foreach (GridViewRow gridViewRow in GridViewDocument.Rows)
//            {
//                gridViewRow.BackColor = System.Drawing.Color.White;
//                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
//                {
//                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
//                }
//            }
//            string headerTable = @"<span  style='text-align:center'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

//            string SubTi = @"<span   style='text-align:center'>
//               <h3>Document Audit Trail Information	</h3>
//            
//            </span>";
//            GridViewDocument.RenderControl(htw);
//            HttpContext.Current.Response.Write(headerTable);
//            HttpContext.Current.Response.Write(SubTi);
//            Response.Write(stringWriter.ToString());
//            Response.End();


//        }
//        else
//        {
//            showMessageBox("No Data Found!!");
//        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnIncrementalview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton) sender;
        GridViewRow gvRow = (GridViewRow) lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField) gv_ViewList.Rows[rowID].FindControl("hfIncrementId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "Increment&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof (string), "OPEN_WINDOW", fullURL, true);
    }


    protected void btnPromotionview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewPromotion.Rows[rowID].FindControl("hfEmployeePromotionId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "Promotion&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void btnRedesignationview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridView1.Rows[rowID].FindControl("hfEmployeeReDesignationId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "Redesignation&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


    protected void btnSuspendview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewSuspend.Rows[rowID].FindControl("hfSupendId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "Suspend&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true); 
    }



    protected void DiciplinaryActionview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewDiciplinaryAction.Rows[rowID].FindControl("hfDiciplinaryActionId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "DiciplinaryAction&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true); 
    }



    protected void Transferview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewTransfer.Rows[rowID].FindControl("hftransferId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "Transfer&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true); 
    }


    protected void Seperationview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewSeperation.Rows[rowID].FindControl("hfEmployeeJobLeftId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "Seperation&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    

    protected void JDview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewJD.Rows[rowID].FindControl("hfJDId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "JD&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void MPBudgetView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewMPBudget.Rows[rowID].FindControl("hfMPBudgetId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "MPBudget&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


    protected void ContructualView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewContructualEmployee.Rows[rowID].FindControl("hfContractualEmployeetId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "ContrauctualEmployee&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


    protected void KPIMasterView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewKIPMaster.Rows[rowID].FindControl("hfKPIMastertId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "KIP&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }



    protected void JobCirculationView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewJobCirculation.Rows[rowID].FindControl("hfJobId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "JobCirculation&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


    protected void TrainingBudgetlView_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewTrainningBudget.Rows[rowID].FindControl("hfTrainingBudget2Id");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "TrainningBudget&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

    }


    protected void EmployeeInformationView_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewEmployeeInformation.Rows[rowID].FindControl("hfEMpId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "EmployeeInformation&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

    }


    protected void JobRequisitionlView_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewJobRequisition.Rows[rowID].FindControl("hfJobReqId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "JobRequisition&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

    }


    protected void EmployeeProbationView_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)GridViewEmployeeProbation.Rows[rowID].FindControl("hfEmpProbationId");
        string url = "../Report_UI/EmployeeAuditTrailViewer.aspx?rptType=" + "EmployeeProbation&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url +
                         "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

    }


}