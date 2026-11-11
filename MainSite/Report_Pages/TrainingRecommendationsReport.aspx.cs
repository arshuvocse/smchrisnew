using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAL.Report_DAL;
using DAL.TrainingDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_TrainingRecommendationsReport : System.Web.UI.Page
{
    ReportDAL aReportDal = new ReportDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();

    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            DropDown();
            heirerchicalTreeView.CollapseAll();
        }
    }

    private void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany_OnSelectedIndexChanged(null, null);
    }
    private TrainingDAL _trainingDal = new TrainingDAL();
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlCompany.SelectedValue != "")
        {
           Session["CompanyId"] = ddlCompany.SelectedValue; 
        }

        if (ddlCompany.SelectedValue != "")
        {
            heirerchicalTreeView.Nodes.Clear();
            AddTree(heirerchicalTreeView);

            DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
            KPIddlFinancialYear.DataSource = dt;
            KPIddlFinancialYear.DataValueField = "Value";
            KPIddlFinancialYear.DataTextField = "TextField";
            KPIddlFinancialYear.DataBind();
        }
    }

    public void AddTree(TreeView aTreeView)
    {
        DataTable dtdivdata = aReportDal.GetAllDivision(ddlCompany.SelectedValue);
        for (int i = 0; i < dtdivdata.Rows.Count; i++)
        {
            aTreeView.Nodes.Add(new TreeNode((dtdivdata.Rows[i]["DivisionName"].ToString()) + "(Division)", (dtdivdata.Rows[i]["DivisionId"].ToString())));
            DataTable dtwing =
                aReportDal.GetAllWing(" AND  tblDivision.DivisionId='" + dtdivdata.Rows[i]["DivisionId"].ToString() +
                                      "'");
            for (int j = 0; j < dtwing.Rows.Count; j++)
            {
                aTreeView.Nodes[i].ChildNodes.Add(new TreeNode((dtwing.Rows[j]["DivisionWingName"].ToString()) + "(Wing)", (dtwing.Rows[j]["DivisionWId"].ToString())));

                DataTable dtdeptm = aReportDal.GetAllDepartment(" AND  tblDepartment.DivisionWId='" + dtwing.Rows[j]["DivisionWId"].ToString() +
                                     "' AND tblDepartment.Root='Wing'");
                for (int k = 0; k < dtdeptm.Rows.Count; k++)
                {
                    aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtdeptm.Rows[j]["DepartmentName"].ToString()) + "(Department)", (dtdeptm.Rows[j]["DepartmentId"].ToString())));

                    DataTable dtsecm1 =
               aReportDal.GetAllSection(" AND  tblDepartment.DepartmentId='" + dtwing.Rows[j]["DepartmentId"].ToString() +
                                     "' AND tblSection.Root='Department'");
                    for (int l = 0; l < dtsecm1.Rows.Count; l++)
                    {
                        aTreeView.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Add(new TreeNode((dtsecm1.Rows[l]["SectionName"].ToString()) + "(Section)", (dtsecm1.Rows[l]["SectionId"].ToString())));

                        DataTable dtsubsecm1 =
               aReportDal.GetAllSubSection(" AND  tblSection.SectionId='" + dtsecm1.Rows[l]["SectionId"].ToString() +
                                     "' AND tblSubSection.Root='Section'");
                        for (int m = 0; m < dtsubsecm1.Rows.Count; m++)
                        {
                            aTreeView.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Add(new TreeNode((dtsubsecm1.Rows[m]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsecm1.Rows[m]["SubSectionId"].ToString())));
                        }
                    }

                    DataTable dtsubsecm2 =
               aReportDal.GetAllSubSection(" AND  tblDepartment.DepartmentId='" + dtdeptm.Rows[k]["DepartmentId"].ToString() +
                                     "' AND tblSubSection.Root='Department'");
                    for (int l = 0; l < dtsubsecm2.Rows.Count; l++)
                    {
                        aTreeView.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Add(new TreeNode((dtsubsecm2.Rows[l]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsecm2.Rows[l]["SubSectionId"].ToString())));
                    }
                    //DataTable dtsecm = aReportDal.GetAllSection(" AND  tblDepartment.DepartmentId='" + dtdept.Rows[j]["DepartmentId"].ToString() +
                    //                 "' AND tblSection.Root='Department'");
                    //for (int k = 0; k < dtsecm.Rows.Count; k++)
                    //{
                    //    aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtsecm.Rows[k]["SectionName"].ToString()) + "(Section)", (dtsecm.Rows[k]["SectionId"].ToString())));
                    //}

                }

                DataTable dtsecm =
               aReportDal.GetAllSection(" AND  tblDivisionWing.DivisionWId='" + dtwing.Rows[j]["DivisionWId"].ToString() +
                                     "' AND tblSection.Root='Wing'");
                for (int k = 0; k < dtsecm.Rows.Count; k++)
                {
                    aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtsecm.Rows[k]["SectionName"].ToString()) + "(Section)", (dtsecm.Rows[k]["SectionId"].ToString())));


                    DataTable dtsubsecm2 =
                aReportDal.GetAllSubSection(" AND  tblSection.SectionId='" + dtsecm.Rows[k]["SectionId"].ToString() +
                                      "' AND tblSubSection.Root='Section'");
                    for (int l = 0; l < dtsubsecm2.Rows.Count; l++)
                    {
                        aTreeView.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Add(new TreeNode((dtsubsecm2.Rows[l]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsecm2.Rows[l]["SubSectionId"].ToString())));
                    }

                }

                DataTable dtsubsecm =
               aReportDal.GetAllSubSection(" AND  tblDivisionWing.DivisionWId='" + dtwing.Rows[j]["DivisionWId"].ToString() +
                                     "' AND tblSubSection.Root='Wing'");
                for (int k = 0; k < dtsubsecm.Rows.Count; k++)
                {
                    aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtsubsecm.Rows[k]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsecm.Rows[k]["SubSectionId"].ToString())));
                }
            }

            DataTable dtdept =
               aReportDal.GetAllDepartment(" AND  tblDivision.DivisionId='" + dtdivdata.Rows[i]["DivisionId"].ToString() +
                                     "' AND tblDepartment.Root='Division'");
            for (int j = 0; j < dtdept.Rows.Count; j++)
            {
                aTreeView.Nodes[i].ChildNodes.Add(new TreeNode((dtdept.Rows[j]["DepartmentName"].ToString()) + "(Department)", (dtdept.Rows[j]["DepartmentId"].ToString())));

                DataTable dtsecm =
               aReportDal.GetAllSection(" AND  tblDepartment.DepartmentId='" + dtdept.Rows[j]["DepartmentId"].ToString() +
                                     "' AND tblSection.Root='Department'");
                for (int k = 0; k < dtsecm.Rows.Count; k++)
                {
                    aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtsecm.Rows[k]["SectionName"].ToString()) + "(Section)", (dtsecm.Rows[k]["SectionId"].ToString())));


                    DataTable dtsubsecm2 =
                aReportDal.GetAllSubSection(" AND  tblSection.SectionId='" + dtsecm.Rows[k]["SectionId"].ToString() +
                                      "' AND tblSubSection.Root='Section'");
                    for (int l = 0; l < dtsubsecm2.Rows.Count; l++)
                    {
                        aTreeView.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Add(new TreeNode((dtsubsecm2.Rows[l]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsecm2.Rows[l]["SubSectionId"].ToString())));
                    }
                }

                DataTable dtsubsecm =
               aReportDal.GetAllSubSection(" AND  tblDivisionWing.DivisionWId='" + dtdept.Rows[j]["DivisionWId"].ToString() +
                                     "' AND tblSubSection.Root='Wing'");
                for (int k = 0; k < dtsubsecm.Rows.Count; k++)
                {
                    aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtsubsecm.Rows[k]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsecm.Rows[k]["SubSectionId"].ToString())));
                }
            }
            DataTable dtsec =
               aReportDal.GetAllSection(" AND  tblDivision.DivisionId='" + dtdivdata.Rows[i]["DivisionId"].ToString() +
                                     "' AND tblSection.Root='Division'");
            for (int j = 0; j < dtsec.Rows.Count; j++)
            {
                aTreeView.Nodes[i].ChildNodes.Add(new TreeNode((dtsec.Rows[j]["SectionName"].ToString()) + "(Section)", (dtsec.Rows[j]["SectionId"].ToString())));

                DataTable dtsubsecm =
               aReportDal.GetAllSubSection(" AND  tblSection.SectionId='" + dtsec.Rows[i]["SectionId"].ToString() +
                                     "' AND tblSubSection.Root='Section'");
                for (int k = 0; k < dtsubsecm.Rows.Count; k++)
                {
                    aTreeView.Nodes[i].ChildNodes[j].ChildNodes.Add(new TreeNode((dtsubsecm.Rows[k]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsecm.Rows[k]["SubSectionId"].ToString())));
                }

            }


            DataTable dtsubsec =
               aReportDal.GetAllSubSection(" AND  tblDivision.DivisionId='" + dtdivdata.Rows[i]["DivisionId"].ToString() +
                                     "' AND tblSubSection.Root='Division'");
            for (int j = 0; j < dtsubsec.Rows.Count; j++)
            {
                aTreeView.Nodes[i].ChildNodes.Add(new TreeNode((dtsubsec.Rows[j]["SubSectionName"].ToString()) + "(Sub Section)", (dtsubsec.Rows[j]["SubSectionId"].ToString())));
            }
        }

    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                repEmpIdHiddenField.Value = emp[0];

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
            aShowMessage.ShowMessageBox("Please Select a Company !!", this);
            SearchEmployeeNameTextBoxTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            ddlCompany.Focus();
        }
    }

    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();

    protected void lbEducationSearch_OnClick(object sender, EventArgs e)
    {
        LoadaInfooa();
    }

    private void LoadaInfooa()
    {
        if (KPIddlFinancialYear.SelectedValue != "")
        {
            DataTable dta = _appPartA.ReportGetAppraisalTraining(GenerateParm());
            if (dta.Rows.Count > 0)
            {
                gv_AppraisalTraining.DataSource = dta;
                gv_AppraisalTraining.DataBind();
                for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                {
                    DropDownList txt_BranchCountry =
                        (DropDownList) gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                    txt_BranchCountry.SelectedValue = dta.Rows[i]["Quater"].ToString();
                }
                GetSingleName();
            }
            else
            {
                gv_AppraisalTraining.DataSource = null;
                gv_AppraisalTraining.DataBind();
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Financial Year!!!", this);
        }
    }

    public void GetSingleName()
    {
        if (gv_AppraisalTraining.Rows.Count > 0)
        {


            string masterText = ((Label)gv_AppraisalTraining.Rows[0].FindControl("txtEmployeeID")).Text;
            for (int i = 1; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                if (masterText.Trim() == ((Label)gv_AppraisalTraining.Rows[i].FindControl("txtEmployeeID")).Text.Trim())
                {
                    ((Label)gv_AppraisalTraining.Rows[i].FindControl("txtEmployeeID")).Text = "";
                }
                else
                {
                    masterText = ((Label)gv_AppraisalTraining.Rows[i].FindControl("txtEmployeeID")).Text.Trim();
                }
            }



            string Name = ((Label)gv_AppraisalTraining.Rows[0].FindControl("txtEmpName")).Text;
            for (int i = 1; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                if (Name.Trim() == ((Label)gv_AppraisalTraining.Rows[i].FindControl("txtEmpName")).Text.Trim())
                {
                    ((Label)gv_AppraisalTraining.Rows[i].FindControl("txtEmpName")).Text = "";
                }
                else
                {
                    Name = ((Label)gv_AppraisalTraining.Rows[i].FindControl("txtEmpName")).Text.Trim();
                }
            }
        }
    }

    private string GenerateParm()
    {
        string param = "";

        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }
        if (repEmpIdHiddenField.Value != string.Empty)
        {
            param = param + " AND Eg.EmpInfoId ='" + repEmpIdHiddenField.Value + "' ";
        }

        if (HierchicalParameter() != string.Empty)
        {
            param = param + HierchicalParameter();
        }

        if (KPIddlFinancialYear.SelectedValue != "")
        {
            param = param + "  AND tblAppraisalMaster.FinancialYearId = " + KPIddlFinancialYear.SelectedValue;
        }
        return param;
    }


    public string HierchicalParameter()
    {
        string param = "";
        string div = "";
        string wing = "";
        string dept = "";
        string sec = "";
        string subsec = "";
        foreach (TreeNode node in heirerchicalTreeView.CheckedNodes)
        {

            string[] nodetext = node.Text.Split('(');
            string step = nodetext[1].TrimEnd(')');
            if (step == "Division")
            {
                div = node.Value + "," + div;
                //param = param + " AND EG.DivisionId='" + node.Value + "' ";
            }
            else if (step == "Wing")
            {
                wing = node.Value + "," + wing;
                //param = param + " AND EG.DivisionWId='" + node.Value + "' ";
            }
            else if (step == "Department")
            {
                dept = node.Value + "," + dept;
                //param = param + " AND EG.DepartmentId='" + node.Value + "' ";
            }
            else if (step == "Section")
            {
                sec = node.Value + "," + sec;
                //param = param + " AND EG.SectionId='" + node.Value + "' ";
            }
            else
            {
                subsec = node.Value + "," + subsec;
                //param = param + " AND EG.SubSectionId='" + node.Value + "' ";
            }
        }
        if (div != string.Empty)
        {
            param = param + " AND EG.DivisionId " + hierchicalDropDownList.SelectedItem.Text + " (" + div.TrimEnd(',') +
                    ") ";
        }
        if (wing != string.Empty)
        {
            param = param + " AND EG.DivisionWId " + hierchicalDropDownList.SelectedItem.Text + " (" + wing.TrimEnd(',') +
                    ") ";
        }
        if (dept != string.Empty)
        {
            param = param + " AND EG.DepartmentId " + hierchicalDropDownList.SelectedItem.Text + " (" + dept.TrimEnd(',') +
                    ")";
        }
        if (sec != string.Empty)
        {
            param = param + " AND EG.SectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + sec.TrimEnd(',') +
                    ") ";
        }
        if (subsec != string.Empty)
        {
            param = param + " AND EG.SubSectionId " + hierchicalDropDownList.SelectedItem.Text + " (" + subsec.TrimEnd(',') +
                    ") ";
        }
        return param;

    }

    protected void lbEducationReset_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void KPIddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadaInfooa();
    }
}