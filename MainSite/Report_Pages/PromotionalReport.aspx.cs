using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Increment_DAL;
using DAL.Report_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_PromotionalReport : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    PromotionalReportDal aReportDal = new PromotionalReportDal();
    MemoPrintIncrementDAL aDAL = new MemoPrintIncrementDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["finalup"] = "";
            IncrementFromDt.Attributes.Add("readonly", "readonly");
            IncrementToDt.Attributes.Add("readonly", "readonly");

            DropDown();

            LoadHeierchicalTree();
            TreeViewProbationaryEmployee.CollapseAll();
        }
    }


    private void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany_OnSelectedIndexChanged(null, null);

        aReportDal.LoadPromotionTypeDropDownList(ddlPromotionType);
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;
        }
       
    }

    private void LoadHeierchicalTree()
    {
        TreeViewProbationaryEmployee.Nodes.Clear();
        AddTree(TreeViewProbationaryEmployee);
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
        gv_ProbationaryEmployee.DataSource = null;
        gv_ProbationaryEmployee.DataBind();
        LoadInfoIncrement();
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_ProbationaryEmployee.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_ProbationaryEmployee.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_ProbationaryEmployee.Rows[i].Cells[0].FindControl("chkSelect");

            chkBoxRows.Checked = chkBoxHeader.Checked;
        }



        for (int i = 0; i < gv_ProbationaryEmployee.Rows.Count; i++)
        {
            if (gv_ProbationaryEmployee.DataKeys[i]["Status"].ToString() == "1")
            {
                Label statusButton = (Label)gv_ProbationaryEmployee.Rows[i].FindControl("statusLabel");
                statusButton.Attributes.Add("class", "btn btn-outline-success btn-block disabled btn-sm");
                statusButton.Text = "Approved";
                var chkBoxRows = (CheckBox)gv_ProbationaryEmployee.Rows[i].Cells[0].FindControl("chkSelect");
                chkBoxRows.Checked = true;
                chkBoxRows.Enabled = false;
            }
            else
            {
                Label statusButton = (Label)gv_ProbationaryEmployee.Rows[i].FindControl("statusLabel");
                statusButton.Attributes.Add("class", "btn btn-outline-primary btn-block disabled btn-sm");
                statusButton.Text = "Pending";
            }
        }
    }

    private void LoadInfoIncrement()
    {
        DataTable dataTable = aReportDal.LoadInfoPromotionalInfoDAL(IncrementGenerateParamiterList(),
            IncrementGenerateParamiterList_SP());

        if (dataTable.Rows.Count > 0)
        {
            gv_ProbationaryEmployee.DataSource = dataTable;
            gv_ProbationaryEmployee.DataBind();


            for (int kk_m = 0; kk_m < gv_ProbationaryEmployee.Rows.Count; kk_m++)
            {

                HiddenField empCatId = (HiddenField) gv_ProbationaryEmployee.Rows[kk_m].FindControl("hfEmpCategoryId");
                HiddenField lblEmployeeCode =
                    (HiddenField) gv_ProbationaryEmployee.Rows[kk_m].FindControl("hfEmpMasterCode");
                //  HiddenField lblEmployeeCode = (HiddenField)gv_ProbationaryEmployee.Rows[kk_m].FindControl("hfEmpMasterCode");
                HiddenField hfEmployeeId = (HiddenField) gv_ProbationaryEmployee.Rows[kk_m].FindControl("hfEmployeeId");
                HiddenField GradeCode =
                    (HiddenField) gv_ProbationaryEmployee.Rows[kk_m].FindControl("hfSalaryGradeCode");

                HiddenField HFSalaryGradeId =
                    (HiddenField) gv_ProbationaryEmployee.Rows[kk_m].FindControl("HFSalaryGradeId");
                HiddenField HFIncrementalStepId =
                    (HiddenField) gv_ProbationaryEmployee.Rows[kk_m].FindControl("HFIncrementalStepId");


                Label lblBasicAmount =
                 (Label)gv_ProbationaryEmployee.Rows[kk_m].FindControl("lblBasicAmount");


                Label lblHouseRent =
                 (Label)gv_ProbationaryEmployee.Rows[kk_m].FindControl("lblHouseRent");


                Label lblMedical =
                 (Label)gv_ProbationaryEmployee.Rows[kk_m].FindControl("lblMedical");


                Label lblConveyance =
                 (Label)gv_ProbationaryEmployee.Rows[kk_m].FindControl("lblConveyance");

                Label lblWash =
                 (Label)gv_ProbationaryEmployee.Rows[kk_m].FindControl("lblWash");


                Label lblTotal =
                 (Label)gv_ProbationaryEmployee.Rows[kk_m].FindControl("lbTotal");


             


                DataTable dtPart = aDAL.LoadParticularsGridView();

                if (dtPart.Rows.Count > 0)
                {
                    KeyResponGridView.DataSource = dtPart;
                    KeyResponGridView.DataBind();
                }




                if (empCatId.Value == "2")
                {
                    /// Graded
                    decimal res = 0;

                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {

                        TextBox lbl_SalaryBreakUp =
                            (TextBox)
                                KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                        Label lbl_Particulars =
                            (Label)
                                KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                        try
                        {







                            if (lbl_Particulars.Text.Trim() == "Basic Pay")
                            {
                                DataTable aDataTable =
                                    aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Value);
                                Decimal basicAmount = 0;
                                try
                                {
                                    basicAmount =
                                        Math.Round(
                                            Convert.ToDecimal(aDataTable.Rows[0]["Basic"].ToString()),
                                            0);
                                }
                                catch (Exception)
                                {


                                }


                                basicAmount = (Math.Round(basicAmount + (basicAmount*5)/100, 0));
                                lbl_SalaryBreakUp.Text = Math.Round(basicAmount, 0).ToString();

                                lblBasicAmount.Text = lbl_SalaryBreakUp.Text;

                            }





                            if (lbl_Particulars.Text.Trim() == "House Rent")
                            {

                                DataTable aDataTable =
                                    aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Value);
                                Decimal HouseResnt = 0;
                                try
                                {
                                    HouseResnt =
                                        Math.Round(
                                            Convert.ToDecimal(aDataTable.Rows[0]["HRent"].ToString()),
                                            0);
                                }
                                catch (Exception)
                                {


                                }


                                HouseResnt = (Math.Round(HouseResnt + (HouseResnt*5)/100, 0));
                                lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                                lblHouseRent.Text = lbl_SalaryBreakUp.Text;

                            }

                            if (lbl_Particulars.Text.Trim() == "Medical")
                            {


                                DataTable aDataTable =
                                    aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Value);
                                Decimal Medical = 0;
                                try
                                {
                                    Medical =
                                        Math.Round(
                                            Convert.ToDecimal(aDataTable.Rows[0]["Medi"].ToString()),
                                            0);
                                }
                                catch (Exception)
                                {


                                }


                                Medical = (Math.Round(Medical + (Medical*5)/100, 0));
                                lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                                lblMedical.Text = lbl_SalaryBreakUp.Text;


                            }

                            if (lbl_Particulars.Text.Trim() == "Conveyance")
                            {

                                DataTable aDataTable =
                                    aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Value);
                                Decimal Conveyance = 0;
                                try
                                {
                                    Conveyance =
                                        Math.Round(
                                            Convert.ToDecimal(aDataTable.Rows[0]["Conv"].ToString()),
                                            0);
                                }
                                catch (Exception)
                                {


                                }


                                DataTable dtConveyance =
                                    aDAL.CheckConveyanceByMasterCode(lblEmployeeCode.Value.Trim());

                                if (dtConveyance.Rows.Count > 0)
                                {
                                    Conveyance = 0;
                                }
                                else
                                {

                                    Conveyance = (Math.Round(Conveyance + (Conveyance*5)/100, 0));
                                }

                                lbl_SalaryBreakUp.Text = Conveyance.ToString();
                                lblConveyance.Text = lbl_SalaryBreakUp.Text;
                            }


                            if (lbl_Particulars.Text.Trim() == "Washing")
                            {
                                DataTable aDataTable =
                                    aDAL.LoadEmpSalarybyEmpCode(lblEmployeeCode.Value);
                                Decimal Wash = 0;
                                try
                                {
                                    Wash =
                                        Math.Round(
                                            Convert.ToDecimal(aDataTable.Rows[0]["Wash"].ToString()),
                                            0);
                                }
                                catch (Exception)
                                {


                                }


                                Wash = (Math.Round(Wash + (Wash*5)/100, 0));


                                lbl_SalaryBreakUp.Text = Wash.ToString();

                                lblWash.Text = lbl_SalaryBreakUp.Text;

                            }
                        }

                        catch (Exception ex)
                        {

                        }

                        decimal res2 = Convert.ToDecimal(lbl_SalaryBreakUp.Text);

                        res += Math.Round(res2, 0);
                    }


                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {
                        Label lbl_Particulars =
                            (Label)
                                KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                        TextBox lbl_SalaryBreakUp =
                            (TextBox)
                                KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");


                        if (lbl_Particulars.Text.Trim() == "Total")
                        {
                            lbl_SalaryBreakUp.Text = res.ToString();
                            lblTotal.Text = lbl_SalaryBreakUp.Text;

                        }
                    }



                   

                    
                }
                else
                {


                    ////Mangement
                    decimal res = 0;

                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {

                        TextBox lbl_SalaryBreakUp =
                            (TextBox)
                                KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                        Label lbl_Particulars =
                            (Label)
                                KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                        try
                        {




                            DataTable aDataTable =
                                aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value),
                                    Convert.ToInt32(HFIncrementalStepId.Value));
                            Decimal basicAmount = 0;
                            try
                            {
                                basicAmount = Math.Round(
                                    Convert.ToDecimal(aDataTable.Rows[0]["BasicAmount"].ToString()), 0);
                            }
                            catch (Exception)
                            {


                            }


                            if (lbl_Particulars.Text.Trim() == "Basic Pay")
                            {
                                //txtIncrementalStep.Text=



                                lbl_SalaryBreakUp.Text = Math.Round(basicAmount, 0).ToString();

                                lblBasicAmount.Text = lbl_SalaryBreakUp.Text;


                            }

                            if (GradeCode.Value.Trim() == "Special" || GradeCode.Value.Trim() == "M-1" ||
                                GradeCode.Value.Trim() == "M-2A" || GradeCode.Value.Trim() == "M-2B" ||
                                GradeCode.Value.Trim() == "M-3A" || GradeCode.Value.Trim() == "M-3B" ||
                                GradeCode.Value.Trim() == "M-4" || GradeCode.Value.Trim() == "M-5")
                            {
                                decimal Medical = 0;
                                decimal HouseResnt = 0;
                                decimal Conveyance = 0;

                                if (lbl_Particulars.Text.Trim() == "House Rent")
                                {
                                    HouseResnt = (Math.Round(basicAmount, 0)*50)/100;
                                    lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                                    lblHouseRent.Text = lbl_SalaryBreakUp.Text;

                                }

                                if (lbl_Particulars.Text.Trim() == "Medical")
                                {
                                    Medical = (Math.Round(basicAmount, 0)*10)/100;
                                    lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();
                                    lblMedical.Text = lbl_SalaryBreakUp.Text;


                                }

                                if (lbl_Particulars.Text.Trim() == "Conveyance")
                                {
                                    Conveyance = 0;
                                    lbl_SalaryBreakUp.Text = Conveyance.ToString();
                                    lbl_SalaryBreakUp.ReadOnly = false;

                                    lblConveyance.Text = lbl_SalaryBreakUp.Text;

                                }


                                if (lbl_Particulars.Text.Trim() == "Washing")
                                {
                                    lbl_SalaryBreakUp.Text = "0";
                                    lbl_Particulars.Text = "";

                                    lbl_Particulars.Visible = false;
                                    lbl_SalaryBreakUp.Visible = false;

                                    lblWash.Text = lbl_SalaryBreakUp.Text;

                                }
                                //basicAmount

                            }


                            if (GradeCode.Value.Trim() == "M-6A" || GradeCode.Value.Trim() == "M-6B" ||
                                GradeCode.Value.Trim() == "M-7" || GradeCode.Value.Trim() == "M-8" ||
                                GradeCode.Value.Trim() == "M-9")
                            {
                                decimal Medical = 0;
                                decimal HouseResnt = 0;
                                decimal Conveyance = 0;

                                if (lbl_Particulars.Text.Trim() == "House Rent")
                                {
                                    HouseResnt = (Math.Round(basicAmount, 0)*75)/100;
                                    lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();

                                    lblHouseRent.Text = lbl_SalaryBreakUp.Text;

                                }

                                if (lbl_Particulars.Text.Trim() == "Medical")
                                {
                                    Medical = (Math.Round(basicAmount, 0)*10)/100;
                                    lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

                                    lblMedical.Text = lbl_SalaryBreakUp.Text;


                                }

                                if (lbl_Particulars.Text.Trim() == "Conveyance")
                                {
                                    Conveyance = (Math.Round(basicAmount, 0)*5)/100;
                                    lbl_SalaryBreakUp.Text = Math.Round(Conveyance, 0).ToString();
                                    lblConveyance.Text = lbl_SalaryBreakUp.Text;


                                }

                                if (lbl_Particulars.Text.Trim() == "Washing")
                                {
                                    lbl_SalaryBreakUp.Text = "0";
                                    lbl_Particulars.Text = "";

                                    lbl_Particulars.Visible = false;
                                    lbl_SalaryBreakUp.Visible = false;

                                    lblWash.Text = lbl_SalaryBreakUp.Text;

                                }


                            }



                            if (GradeCode.Value.Trim() == "S-5" || GradeCode.Value.Trim() == "S-4" ||
                                GradeCode.Value.Trim() == "S-3" || GradeCode.Value.Trim() == "S-2" ||
                                GradeCode.Value.Trim() == "S-1A" ||
                                GradeCode.Value.Trim() == "S-1B" ||
                                GradeCode.Value.Trim() == "SS-5" ||
                                GradeCode.Value.Trim() == "S-1A" ||
                                GradeCode.Value.Trim() == "SS-4" ||
                                GradeCode.Value.Trim() == "S-1A" ||
                                GradeCode.Value.Trim() == "SS-3" ||
                                GradeCode.Value.Trim() == "SS-2" ||
                                GradeCode.Value.Trim() == "S-1A" ||
                                GradeCode.Value.Trim() == "SS-1A" ||
                                GradeCode.Value.Trim() == "SS-1B"

                                ||
                                GradeCode.Value.Trim() == "S-1" ||
                                GradeCode.Value.Trim() == "SS-1" ||
                                GradeCode.Value.Trim() == "SS-1B" ||
                                GradeCode.Value.Trim() == "M-3" ||
                                GradeCode.Value.Trim() == "M-2" ||
                                GradeCode.Value.Trim() == "M-6" ||
                                GradeCode.Value.Trim() == "M-0" ||
                                GradeCode.Value.Trim() == "S-0")
                            {
                                decimal Medical = 0;
                                decimal HouseResnt = 0;
                                decimal Conveyance = 0;

                                if (lbl_Particulars.Text.Trim() == "House Rent")
                                {
                                    HouseResnt = (Math.Round(basicAmount, 0)*63)/100;
                                    lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();

                                    lblHouseRent.Text = lbl_SalaryBreakUp.Text;

                                }

                                if (lbl_Particulars.Text.Trim() == "Medical")
                                {
                                    Medical = 0;
                                    lbl_SalaryBreakUp.Text = Medical.ToString();
                                    lbl_SalaryBreakUp.ReadOnly = false;

                                    lblMedical.Text = lbl_SalaryBreakUp.Text;

                                }

                                if (lbl_Particulars.Text.Trim() == "Conveyance")
                                {
                                    Conveyance = 0;
                                    lbl_SalaryBreakUp.Text = Conveyance.ToString();
                                    lbl_SalaryBreakUp.ReadOnly = false;

                                    lblConveyance.Text = lbl_SalaryBreakUp.Text;


                                }

                                if (lbl_Particulars.Text.Trim() == "Washing")
                                {
                                    lbl_SalaryBreakUp.Text = "0";

                                    lbl_Particulars.Visible = true;
                                    lbl_SalaryBreakUp.Visible = true;

                                    lbl_SalaryBreakUp.ReadOnly = false;
                                    lblWash.Text = lbl_SalaryBreakUp.Text;

                                }
                            }

                        }

                        catch (Exception)
                        {

                            //throw;
                        }



                        decimal res2 = Convert.ToDecimal(lbl_SalaryBreakUp.Text);

                        res += Math.Round(res2, 0);


                    }

                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {
                        Label lbl_Particulars =
                            (Label)
                                KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                        TextBox lbl_SalaryBreakUp =
                            (TextBox)
                                KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");


                        if (lbl_Particulars.Text.Trim() == "Total")
                        {


                            DataTable aDataTable =
                                aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value),
                                    Convert.ToInt32(HFIncrementalStepId.Value));

                            Decimal GrossAmount = 0;
                            try
                            {
                                GrossAmount =
                                    Math.Round(
                                        Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()),
                                        0);
                            }
                            catch (Exception)
                            {


                            }

                            lbl_Particulars.Font.Bold = true;
                            lbl_SalaryBreakUp.Text = GrossAmount.ToString();
                            lbl_SalaryBreakUp.ReadOnly = true;

                            lblTotal.Text = lbl_SalaryBreakUp.Text;


                        }

                        if (lbl_Particulars.Text.Trim() == "Medical")
                        {



                            DataTable aDataTable =
                                aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(HFSalaryGradeId.Value),
                                    Convert.ToInt32(HFIncrementalStepId.Value));

                            Decimal GrossAmount = 0;
                            try
                            {
                                GrossAmount =
                                    Math.Round(
                                        Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()),
                                        0);
                            }
                            catch (Exception)
                            {


                            }
                            if (GrossAmount != res)
                            {

                                if (lbl_Particulars.Text.Trim() == "Medical")
                                {
                                    decimal newREs = 0;

                                    newREs = GrossAmount - res;

                                    decimal medi = 0;
                                    try
                                    {
                                        medi = Convert.ToDecimal(lbl_SalaryBreakUp.Text.Trim());
                                    }
                                    catch (Exception)
                                    {

                                        //throw;
                                    }

                                    decimal mainres = medi + newREs;


                                    lbl_SalaryBreakUp.Text = mainres.ToString();
                                    lbl_SalaryBreakUp.ReadOnly = false;
                                    lblMedical.Text = lbl_SalaryBreakUp.Text;

                                }
                            }


                        }
                    }

                }
            }
        }
        else
        {
            gv_ProbationaryEmployee.DataSource = null;
            gv_ProbationaryEmployee.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }

    }


    private
        bool ApproveValidation()
    {
        int count = 0;

        for (int i = 0; i < gv_ProbationaryEmployee.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_ProbationaryEmployee.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("You should select at least one employee !!");
            return false;
        }

        return true;
    }
    protected void btn_Approve_OnClick(object sender, EventArgs e)
    {

        if (ApproveValidation())
        {
            for (int i = 0; i < gv_ProbationaryEmployee.Rows.Count; i++)
            {
                var chkBoxRows = (CheckBox)gv_ProbationaryEmployee.Rows[i].Cells[0].FindControl("chkSelect");

                if (chkBoxRows.Checked && gv_ProbationaryEmployee.DataKeys[i]["Status"].ToString() != "1")
                {
                    var aListDao = new PromotionListDao();

                    aListDao.EmpInfoId = Convert.ToInt32(gv_ProbationaryEmployee.DataKeys[i]["EmployeeId"]);
                    aListDao.EmployeePromotionEntryId = Convert.ToInt32(gv_ProbationaryEmployee.DataKeys[i]["EmployeePromotionEntryId"]);
                    aListDao.EmpMasterCode = gv_ProbationaryEmployee.Rows[i].Cells[2].Text.Trim();
                    aListDao.ZID = gv_ProbationaryEmployee.Rows[i].Cells[1].Text.Trim();
                    aListDao.PromoTypeId = gv_ProbationaryEmployee.Rows[i].Cells[5].Text.Trim();
                 
                    aListDao.Effectivedate = Convert.ToDateTime(gv_ProbationaryEmployee.Rows[i].Cells[4].Text.Trim());
                    aListDao.Approveby = Session["LoginName"].ToString();
                    aListDao.ApproveDate = DateTime.Now;

                    aReportDal.SavePromotionList(aListDao);
                }
            }

            btn_Save_OnClick(null, null);
        }

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private String IncrementGenerateParamiterList()
    {
        string parameter = "  ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlPromotionType.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.NPromoTypeId = " + ddlPromotionType.SelectedValue;
        }

        if (Chkreappointment.Checked)
        {
            parameter = parameter + " AND EPE.IsReappointment = 1";
        }

        //if (SeparationFinancialYearDropDownList.SelectedValue != "")
        //{
        //    parameter = parameter + " AND EPE.SubmissionDate = " + SeparationFinancialYearDropDownList.SelectedValue;
        //}

        if (IncrementFromDt.Text != string.Empty && IncrementToDt.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + IncrementFromDt.Text + "' AND '" + IncrementToDt.Text + "' ";
        }
        if (IncrementFromDt.Text != string.Empty && IncrementToDt.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + IncrementFromDt.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (IncrementFromDt.Text == string.Empty && IncrementToDt.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + IncrementToDt.Text + "' AND '" + IncrementToDt.Text + "' ";
        }


        if (SeparationHierchicalParameter() != string.Empty)
        {
            parameter = parameter + SeparationHierchicalParameter();
        }

        Session["finalup"] = "";

        
        return parameter;
    }


    private String IncrementGenerateParamiterList_SP()
    {
        string parameter = "  ";

      

        if (ddlPromotionType.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.NPromoTypeId = " + ddlPromotionType.SelectedValue;
        }

        if (Chkreappointment.Checked)
        {
            parameter = parameter + " AND EPE.IsReappointment = 1";
        }

        //if (SeparationFinancialYearDropDownList.SelectedValue != "")
        //{
        //    parameter = parameter + " AND EPE.SubmissionDate = " + SeparationFinancialYearDropDownList.SelectedValue;
        //}

        if (IncrementFromDt.Text != string.Empty && IncrementToDt.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + IncrementFromDt.Text + "' AND '" + IncrementToDt.Text + "' ";
        }
        if (IncrementFromDt.Text != string.Empty && IncrementToDt.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + IncrementFromDt.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (IncrementFromDt.Text == string.Empty && IncrementToDt.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + IncrementToDt.Text + "' AND '" + IncrementToDt.Text + "' ";
        }


        if (SeparationHierchicalParameter() != string.Empty)
        {
            parameter = parameter + SeparationHierchicalParameter();
        }

        Session["finalup"] = "";


        return parameter;
    }

    public string SeparationHierchicalParameter()
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

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("IncrementInformationReport.aspx");
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_ProbationaryEmployee.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Promotion List_" + DateTime.Now.ToString("dd-MMM-yyyy") +
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
            //frm.Controls.Add(gv_ProbationaryEmployee);
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
   <h3>Promotion List</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            ShowMessageBox("No Data Found!!");
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
    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}