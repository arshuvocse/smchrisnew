using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Report_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_AccountsInformationReport : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    AccountsInformationReportDAL aReportDal = new AccountsInformationReportDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["finalup"] = "";
            joiningDtFrTextBox.Attributes.Add("readonly", "readonly");
            joiningDtToTextBox.Attributes.Add("readonly", "readonly");

            SeparationFromDt.Attributes.Add("readonly", "readonly");
            SeparationToDT.Attributes.Add("readonly", "readonly");


            ProbationaryEmployeeFromDt.Attributes.Add("readonly", "readonly");
            ProbationaryEmployeeToDt.Attributes.Add("readonly", "readonly");


            redesignationEffectivefromDT.Attributes.Add("readonly", "readonly");
            redesignationEffectiveToDT.Attributes.Add("readonly", "readonly");

            DropDown();
            heirerchicalTreeView.CollapseAll();
        }

    
    }

    private void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany_OnSelectedIndexChanged(null,null);

         
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;
           
        }
    }

    public void AddTree(TreeView aTreeView)
    {
        try
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
                   aReportDal.GetAllSection(" AND  tblDepartment.DepartmentId='" + dtdeptm.Rows[j]["DepartmentId"].ToString() +
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
        catch (Exception)
        {

            //throw;
        }

    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
         if (cblHeader.SelectedValue != "")
        {
        if (cblHeader.SelectedValue == "NewJoinerList")
        {
            LoadNewJoinerList();
        }
        else
        {
            gv_NewJoinerList.DataSource = null;
            gv_NewJoinerList.DataBind();
        }


        if (cblHeader.SelectedValue == "SeparationList")
        {
            LoadInfoSeparation();
        }
        else
        {
            gv_SeparationList.DataSource = null;
            gv_SeparationList.DataBind();
        }

        if (cblHeader.SelectedValue == "ProbationaryEmployeeList")
        {
            LoadInfoProbationaryEmployee();
        }
        else
        {
            gv_ProbationaryEmployee.DataSource = null;
            gv_ProbationaryEmployee.DataBind();
        }

        if (cblHeader.SelectedValue == "redesignation")
        {
            LoadInforedesignation();
        }
        else
        {
            gv_redesignation.DataSource = null;
            gv_redesignation.DataBind();
        }


             }
         else
         {
             aShowMessage.ShowMessageBox("Please Select a Report Name!!", this);
         }

    }

    private void LoadInforedesignation()
    {
        DataTable dataTable = aReportDal.LoadInforedesignationDAL(redesignationGenerateParamiterList());

        if (dataTable.Rows.Count > 0)
        {
            gv_redesignation.DataSource = dataTable;
            gv_redesignation.DataBind();
           
        }
        else
        {
            gv_redesignation.DataSource = null;
            gv_redesignation.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!!", this);
        }
    }


    private void LoadInfoSeparation()
    {

        DataTable dataTable = aReportDal.LoadInfoSeparationDAL(SeprationGenerateParamiterList());

        if (dataTable.Rows.Count > 0)
        {
            gv_SeparationList.DataSource = dataTable;
            gv_SeparationList.DataBind();
        }
        else
        {
            gv_SeparationList.DataSource = null;
            gv_SeparationList.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }


    private void LoadInfoProbationaryEmployee()
    {

        DataTable dataTable = aReportDal.LoadInfoProbationaryEmployeeDAL(ProbationaryEmployeeGenerateParamiterList());

        if (dataTable.Rows.Count > 0)
        {
            gv_ProbationaryEmployee.DataSource = dataTable;
            gv_ProbationaryEmployee.DataBind();
        }
        else
        {
            gv_ProbationaryEmployee.DataSource = null;
            gv_ProbationaryEmployee.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }
    
      private string SeprationGenerateParamiterList()
    {

        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }


        //if (SeparationFinancialYearDropDownList.SelectedValue != "")
        //{
        //    parameter = parameter + " AND EPE.SubmissionDate = " + SeparationFinancialYearDropDownList.SelectedValue;
        //}

        if (SeparationFromDt.Text != string.Empty && SeparationToDT.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + SeparationFromDt.Text + "' AND '" + SeparationToDT.Text + "' ";
        }
        if (SeparationFromDt.Text != string.Empty && SeparationToDT.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + SeparationFromDt.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (SeparationFromDt.Text == string.Empty && SeparationToDT.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + SeparationToDT.Text + "' AND '" + SeparationToDT.Text + "' ";
        }


        if (SeparationHierchicalParameter() != string.Empty)
        {
            parameter = parameter + SeparationHierchicalParameter();
        }

          Session["finalup"] = "";
        if (SperationManualAuto.SelectedValue != "")
        {

            if (SperationManualAuto.SelectedValue == "Manual")
            {
                parameter = parameter + " AND EPE.AutoProcess = 'Manually Updated'";
            }


            if (SperationManualAuto.SelectedValue == "Final")
            {
                Session["finalup"] = "";
                Session["finalup"] =
                    @"INNER JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review')  GROUP BY EmployeeJobLeftId) AS CELog ON CELog.EmployeeJobLeftId= EPE.EmployeeJobLeftId
								INNER JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId";
                parameter = parameter + " AND EPE.ActionStatus2 = 'Approved'  and Version=CELog.MaxVer  and  ForEmpInfoId =0 ";
            }
           
        }

          return parameter;
    }


      private string ProbationaryEmployeeGenerateParamiterList()
      {

          string parameter = "   ";

          if (ddlCompany.SelectedValue != "")
          {
              parameter = parameter + " AND EG.CompanyId = " + ddlCompany.SelectedValue;
          }


          //if (SeparationFinancialYearDropDownList.SelectedValue != "")
          //{
          //    parameter = parameter + " AND EPE.SubmissionDate = " + SeparationFinancialYearDropDownList.SelectedValue;
          //}

          if (ProbationaryEmployeeFromDt.Text != string.Empty && ProbationaryEmployeeToDt.Text != string.Empty)
          {
              parameter = parameter + " AND EG.ProbationEndDate BETWEEN '" + ProbationaryEmployeeFromDt.Text + "' AND '" + ProbationaryEmployeeToDt.Text + "' ";
          }
          if (ProbationaryEmployeeFromDt.Text != string.Empty && ProbationaryEmployeeToDt.Text == string.Empty)
          {
              parameter = parameter + " AND EG.ProbationEndDate BETWEEN '" + ProbationaryEmployeeFromDt.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
          }

          if (ProbationaryEmployeeFromDt.Text == string.Empty && ProbationaryEmployeeToDt.Text != string.Empty)
          {
              parameter = parameter + " AND EG.ProbationEndDate BETWEEN '" + ProbationaryEmployeeToDt.Text + "' AND '" + ProbationaryEmployeeToDt.Text + "' ";
          }


          if (ProbationaryEmployeeHierchicalParameter() != string.Empty)
          {
              parameter = parameter + ProbationaryEmployeeHierchicalParameter();
          }

          

          return parameter;
      }


      private string  redesignationGenerateParamiterList()
      {

          string parameter = "   ";

          if (ddlCompany.SelectedValue != "")
          {
              parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
          }


          //if (SeparationFinancialYearDropDownList.SelectedValue != "")
          //{
          //    parameter = parameter + " AND EPE.SubmissionDate = " + SeparationFinancialYearDropDownList.SelectedValue;
          //}

          if (redesignationEffectivefromDT.Text != string.Empty && redesignationEffectiveToDT.Text != string.Empty)
          {
              parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + redesignationEffectivefromDT.Text + "' AND '" + redesignationEffectiveToDT.Text + "' ";
          }
          if (redesignationEffectivefromDT.Text != string.Empty && redesignationEffectiveToDT.Text == string.Empty)
          {
              parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + redesignationEffectivefromDT.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
          }

          if (redesignationEffectivefromDT.Text == string.Empty && redesignationEffectiveToDT.Text != string.Empty)
          {
              parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + redesignationEffectiveToDT.Text + "' AND '" + redesignationEffectiveToDT.Text + "' ";
          }


          if (redesignationHierchicalParameter() != string.Empty)
          {
              parameter = parameter + redesignationHierchicalParameter();
          }



          return parameter;
      }
    private void LoadNewJoinerList()
    {
        DataTable dataTable = aReportDal.NewJoinerListDAL(ParamiterNewJoinerList());

        if (dataTable.Rows.Count > 0)
        {
            gv_NewJoinerList.DataSource = dataTable;
            gv_NewJoinerList.DataBind();
        }
        else
        {
            gv_NewJoinerList.DataSource = null;
            gv_NewJoinerList.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }

    private string ParamiterNewJoinerList()
    {
        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }
        if (HierchicalParameter() != string.Empty)
        {
            parameter = parameter + HierchicalParameter();
        }


        if (joiningDtFrTextBox.Text != string.Empty && joiningDtToTextBox.Text != string.Empty)
        {
            parameter = parameter + " AND EG.DateOfJoin BETWEEN '" + joiningDtFrTextBox.Text + "' AND '" + joiningDtToTextBox.Text + "' ";
        }
        if (joiningDtFrTextBox.Text != string.Empty && joiningDtToTextBox.Text == string.Empty)
        {
            parameter = parameter + " AND EG.DateOfJoin BETWEEN '" + joiningDtFrTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (joiningDtFrTextBox.Text == string.Empty && joiningDtToTextBox.Text != string.Empty)
        {
            parameter = parameter + " AND EG.DateOfJoin BETWEEN '" + joiningDtToTextBox.Text + "' AND '" + joiningDtToTextBox.Text + "' ";
        }

        
        return parameter;
    }
    public string HierchicalParameter()
    {
        string param = " ";
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

        param = param + " AND ( ";
        if (div != string.Empty)
        {

            param = param + "   ( EG.DivisionId   In   (" + div.TrimEnd(',') + ")  or";
        }
        if (wing != string.Empty)
        {
            param = param + "  ( EG.DivisionWId   In    (" + wing.TrimEnd(',') + ") ) or";
        }
        if (dept != string.Empty)
        {
            param = param + "  ( EG.DepartmentId   In    (" + dept.TrimEnd(',') + ") ) or";
        }
        if (sec != string.Empty)
        {
            param = param + "  ( EG.SectionId   In    (" + sec.TrimEnd(',') + ") ) or";
        }
        if (subsec != string.Empty)
        {
            param = param + "  ( EG.SubSectionId  In   (" + subsec.TrimEnd(',') + ") ) or";
        }



        if (div == string.Empty && wing == string.Empty && dept == string.Empty && sec == string.Empty && subsec == string.Empty)
        {
            param = param.TrimEnd("AND ( ".ToCharArray());
        }
        else
        {

            param = param.TrimEnd("or".ToCharArray());
            param = param + ")";
        }

        if (div != string.Empty)
        {
            param = param + ")";
        }



        return param;



    }


    public string SeparationHierchicalParameter()
    {
        string param = " ";
        string div = "";
        string wing = "";
        string dept = "";
        string sec = "";
        string subsec = "";
        foreach (TreeNode node in TreeViewSeparationList.CheckedNodes)
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

        param = param + " AND ( ";
        if (div != string.Empty)
        {

            param = param + "   ( EG.DivisionId   In   (" + div.TrimEnd(',') + ")  or";
        }
        if (wing != string.Empty)
        {
            param = param + "  ( EG.DivisionWId   In    (" + wing.TrimEnd(',') + ") ) or";
        }
        if (dept != string.Empty)
        {
            param = param + "  ( EG.DepartmentId   In    (" + dept.TrimEnd(',') + ") ) or";
        }
        if (sec != string.Empty)
        {
            param = param + "  ( EG.SectionId   In    (" + sec.TrimEnd(',') + ") ) or";
        }
        if (subsec != string.Empty)
        {
            param = param + "  ( EG.SubSectionId  In   (" + subsec.TrimEnd(',') + ") ) or";
        }



        if (div == string.Empty && wing == string.Empty && dept == string.Empty && sec == string.Empty && subsec == string.Empty)
        {
            param = param.TrimEnd("AND ( ".ToCharArray());
        }
        else
        {

            param = param.TrimEnd("or".ToCharArray());
            param = param + ")";
        }

        if (div != string.Empty)
        {
            param = param + ")";
        }



        return param;



    }

    public string ProbationaryEmployeeHierchicalParameter()
    {
        string param = " ";
        string div = "";
        string wing = "";
        string dept = "";
        string sec = "";
        string subsec = "";
        foreach (TreeNode node in TreeViewProbationaryEmployee.CheckedNodes)
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

        param = param + " AND ( ";
        if (div != string.Empty)
        {

            param = param + "   ( EG.DivisionId   In   (" + div.TrimEnd(',') + ")  or";
        }
        if (wing != string.Empty)
        {
            param = param + "  ( EG.DivisionWId   In    (" + wing.TrimEnd(',') + ") ) or";
        }
        if (dept != string.Empty)
        {
            param = param + "  ( EG.DepartmentId   In    (" + dept.TrimEnd(',') + ") ) or";
        }
        if (sec != string.Empty)
        {
            param = param + "  ( EG.SectionId   In    (" + sec.TrimEnd(',') + ") ) or";
        }
        if (subsec != string.Empty)
        {
            param = param + "  ( EG.SubSectionId  In   (" + subsec.TrimEnd(',') + ") ) or";
        }



        if (div == string.Empty && wing == string.Empty && dept == string.Empty && sec == string.Empty && subsec == string.Empty)
        {
            param = param.TrimEnd("AND ( ".ToCharArray());
        }
        else
        {

            param = param.TrimEnd("or".ToCharArray());
            param = param + ")";
        }

        if (div != string.Empty)
        {
            param = param + ")";
        }



        return param;



    }

    public string redesignationHierchicalParameter()
    {
        string param = " ";
        string div = "";
        string wing = "";
        string dept = "";
        string sec = "";
        string subsec = "";
        foreach (TreeNode node in TreeViewredesignation.CheckedNodes)
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

        param = param + " AND ( ";
        if (div != string.Empty)
        {

            param = param + "   ( EG.DivisionId   In   (" + div.TrimEnd(',') + ")  or";
        }
        if (wing != string.Empty)
        {
            param = param + "  ( EG.DivisionWId   In    (" + wing.TrimEnd(',') + ") ) or";
        }
        if (dept != string.Empty)
        {
            param = param + "  ( EG.DepartmentId   In    (" + dept.TrimEnd(',') + ") ) or";
        }
        if (sec != string.Empty)
        {
            param = param + "  ( EG.SectionId   In    (" + sec.TrimEnd(',') + ") ) or";
        }
        if (subsec != string.Empty)
        {
            param = param + "  ( EG.SubSectionId  In   (" + subsec.TrimEnd(',') + ") ) or";
        }



        if (div == string.Empty && wing == string.Empty && dept == string.Empty && sec == string.Empty && subsec == string.Empty)
        {
            param = param.TrimEnd("AND ( ".ToCharArray());
        }
        else
        {

            param = param.TrimEnd("or".ToCharArray());
            param = param + ")";
        }

        if (div != string.Empty)
        {
            param = param + ")";
        }



        return param;



    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AccountsInformationReport.aspx");
    }

    protected void cblHeader_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (cblHeader.SelectedValue == "NewJoinerList")
        {
            heirerchicalTreeView.Nodes.Clear();
            AddTree(heirerchicalTreeView);
            pNewJoinerList.Visible = true;
        }
        else
        {
            pNewJoinerList.Visible = false;
            gv_NewJoinerList.DataSource = null;
            gv_NewJoinerList.DataBind();
        }


        if (cblHeader.SelectedValue == "SeparationList")
        {
            TreeViewSeparationList.Nodes.Clear();
            AddTree(TreeViewSeparationList);
            PSeparationList.Visible = true;
        }
        else
        {
            PSeparationList.Visible = false;
            gv_SeparationList.DataSource = null;
            gv_SeparationList.DataBind();
        }


        if (cblHeader.SelectedValue == "ProbationaryEmployeeList")
        {
            TreeViewProbationaryEmployee.Nodes.Clear();
            AddTree(TreeViewProbationaryEmployee);
            PProbationaryEmployee.Visible = true;
        }
        else
        {
            PProbationaryEmployee.Visible = false;
        }


        if (cblHeader.SelectedValue == "redesignation")
        {
            TreeViewredesignation.Nodes.Clear();
            AddTree(TreeViewredesignation);
            Predesignation.Visible = true;
        }
        else
        {
            Predesignation.Visible = false;
            gv_redesignation.DataSource = null;
            gv_redesignation.DataBind();
        }

        if (cblHeader.SelectedValue == "Transfer")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "Promotion")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "Upgradation")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "LeaveEncashment")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "ContractRenewal")
        {

        }

        else
        {
             
        }

        if (cblHeader.SelectedValue == "OtherDeduction")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "NewJoinerList")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "NewJoinerList")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "NewJoinerList")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "NewJoinerList")
        {

        }

        else
        {
             
        }

        if (cblHeader.SelectedValue == "NewJoinerList")
        {

        }
        else
        {
             
        }


        if (cblHeader.SelectedValue == "NewJoinerList")
        {

        }
        else
        {
             
        }

        
        if (cblHeader.SelectedValue == "NewJoinerList")
        {

        }
        else
        {
             
        }


    
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (cblHeader.SelectedValue != "")
        {
            if (cblHeader.SelectedValue == "NewJoinerList")
            {
                if (gv_NewJoinerList.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=New Joiner List_" + DateTime.Now.ToString("dd-MMM-yyyy") +
                                        ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);








                    gv_NewJoinerList.AllowPaging = false;

                    // Create a form to contain the grid  
                    HtmlForm frm = new HtmlForm();
                    gv_NewJoinerList.Parent.Controls.Add(frm);
                    //frm.Attributes["runat"] = "server";
                    //frm.Controls.Add(gv_NewJoinerList);
                    //frm.RenderControl(htw);

                    gv_NewJoinerList.HeaderRow.Style.Add("background-color", "#E5EEF1");

                    // Set background color of each cell of GridView1 header row
                    foreach (TableCell tableCell in gv_NewJoinerList.HeaderRow.Cells)
                    {
                        tableCell.Style["background-color"] = "#E5EEF1";
                    }

                    // Set background color of each cell of each data row of GridView1
                    foreach (GridViewRow gridViewRow in gv_NewJoinerList.Rows)
                    {
                        gridViewRow.BackColor = System.Drawing.Color.White;

                        foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                        {
                            gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                        }
                    }


                    gv_NewJoinerList.RenderControl(htw);

                    string comname = ddlCompany.SelectedItem.Text;
                    if (comname == "SMC")
                    {
                        comname = "Social Marketing Company";
                    }
                    else
                    {
                        comname = "SMC Enterprise Ltd.";
                    }
                    string headerTable = @"<span  style='text-align:center'><h3> " + comname +
                                         "</h3>  </span> <span  style='text-align:center'><h4> Print Date: " +
                                         DateTime.Now.ToString("dd-MMM-yyyy") + "</h4></span>";

                    string SubTi = @"<span   style='text-align:center'>
   <h3>New Joiner List</h3>

</span>";

                    HttpContext.Current.Response.Write(headerTable);
                    HttpContext.Current.Response.Write(SubTi);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    showMessageBox("No Data Found!!");
                }
            }


            if (cblHeader.SelectedValue == "SeparationList")
            {
                if (gv_SeparationList.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=Separation List_" + DateTime.Now.ToString("dd-MMM-yyyy") +
                                        ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);








                    gv_SeparationList.AllowPaging = false;

                    // Create a form to contain the grid  
                    HtmlForm frm = new HtmlForm();
                    gv_SeparationList.Parent.Controls.Add(frm);
                    //frm.Attributes["runat"] = "server";
                    //frm.Controls.Add(gv_SeparationList);
                    //frm.RenderControl(htw);

                    gv_SeparationList.HeaderRow.Style.Add("background-color", "#E5EEF1");

                    // Set background color of each cell of GridView1 header row
                    foreach (TableCell tableCell in gv_SeparationList.HeaderRow.Cells)
                    {
                        tableCell.Style["background-color"] = "#E5EEF1";
                    }

                    // Set background color of each cell of each data row of GridView1
                    foreach (GridViewRow gridViewRow in gv_SeparationList.Rows)
                    {
                        gridViewRow.BackColor = System.Drawing.Color.White;

                        foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                        {
                            gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                        }
                    }


                    gv_SeparationList.RenderControl(htw);

                    string comname = ddlCompany.SelectedItem.Text;
                    if (comname == "SMC")
                    {
                        comname = "Social Marketing Company";
                    }
                    else
                    {
                        comname = "SMC Enterprise Ltd.";
                    }
                    string headerTable = @"<span  style='text-align:center'><h3> " + comname +
                                         "</h3>  </span> <span  style='text-align:center'><h4> Print Date: " +
                                         DateTime.Now.ToString("dd-MMM-yyyy") + "</h4></span>";

                    string SubTi = @"<span   style='text-align:center'>
   <h3>Separation List</h3>

</span>";

                    HttpContext.Current.Response.Write(headerTable);
                    HttpContext.Current.Response.Write(SubTi);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    showMessageBox("No Data Found!!");
                }
            }


            if (cblHeader.SelectedValue == "ProbationaryEmployeeList")
            {
                if (gv_ProbationaryEmployee.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=Probationary Employee List_" + DateTime.Now.ToString("dd-MMM-yyyy") +
                                        ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);








                    gv_ProbationaryEmployee.AllowPaging = false;

                    // Create a form to contain the grid  
                    HtmlForm frm = new HtmlForm();
                    gv_ProbationaryEmployee.Parent.Controls.Add(frm);
                    //frm.Attributes["runat"] = "server";
                    //frm.Controls.Add(gv_SeparationList);
                    //frm.RenderControl(htw);

                    gv_ProbationaryEmployee.HeaderRow.Style.Add("background-color", "#E5EEF1");

                    // Set background color of each cell of GridView1 header row
                    foreach (TableCell tableCell in gv_ProbationaryEmployee.HeaderRow.Cells)
                    {
                        tableCell.Style["background-color"] = "#E5EEF1";
                    }

                    // Set background color of each cell of each data row of GridView1
                    foreach (GridViewRow gridViewRow in gv_ProbationaryEmployee.Rows)
                    {
                        gridViewRow.BackColor = System.Drawing.Color.White;

                        foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                        {
                            gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                        }
                    }


                    gv_ProbationaryEmployee.RenderControl(htw);

                    string comname = ddlCompany.SelectedItem.Text;
                    if (comname == "SMC")
                    {
                        comname = "Social Marketing Company";
                    }
                    else
                    {
                        comname = "SMC Enterprise Ltd.";
                    }
                    string headerTable = @"<span  style='text-align:center'><h3> " + comname +
                                         "</h3>  </span> <span  style='text-align:center'><h4> Print Date: " +
                                         DateTime.Now.ToString("dd-MMM-yyyy") + "</h4></span>";

                    string SubTi = @"<span   style='text-align:center'>
   <h3>Probationary Employee List</h3>

</span>";

                    HttpContext.Current.Response.Write(headerTable);
                    HttpContext.Current.Response.Write(SubTi);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    showMessageBox("No Data Found!!");
                }
            }

            if (cblHeader.SelectedValue == "redesignation")
            {
                if (gv_redesignation.Rows.Count > 0)
                {
                    string attachment = "attachment; filename=Re-designation List_" + DateTime.Now.ToString("dd-MMM-yyyy") +
                                        ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);








                    gv_redesignation.AllowPaging = false;

                    // Create a form to contain the grid  
                    HtmlForm frm = new HtmlForm();
                    gv_redesignation.Parent.Controls.Add(frm);
                    //frm.Attributes["runat"] = "server";
                    //frm.Controls.Add(gv_SeparationList);
                    //frm.RenderControl(htw);

                    gv_redesignation.HeaderRow.Style.Add("background-color", "#E5EEF1");

                    // Set background color of each cell of GridView1 header row
                    foreach (TableCell tableCell in gv_redesignation.HeaderRow.Cells)
                    {
                        tableCell.Style["background-color"] = "#E5EEF1";
                    }

                    // Set background color of each cell of each data row of GridView1
                    foreach (GridViewRow gridViewRow in gv_redesignation.Rows)
                    {
                        gridViewRow.BackColor = System.Drawing.Color.White;

                        foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                        {
                            gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                        }
                    }


                    gv_redesignation.RenderControl(htw);

                    string comname = ddlCompany.SelectedItem.Text;
                    if (comname == "SMC")
                    {
                        comname = "Social Marketing Company";
                    }
                    else
                    {
                        comname = "SMC Enterprise Ltd.";
                    }
                    string headerTable = @"<span  style='text-align:center'><h3> " + comname +
                                         "</h3>  </span> <span  style='text-align:center'><h4> Print Date: " +
                                         DateTime.Now.ToString("dd-MMM-yyyy") + "</h4></span>";

                    string SubTi = @"<span   style='text-align:center'>
   <h3>Re-designation List</h3>

</span>";

                    HttpContext.Current.Response.Write(headerTable);
                    HttpContext.Current.Response.Write(SubTi);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    showMessageBox("No Data Found!!");
                }
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select a Report Name!!", this);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}