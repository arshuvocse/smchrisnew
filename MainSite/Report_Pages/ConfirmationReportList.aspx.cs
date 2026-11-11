using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Report_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_ConfirmationReportList : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    ConfirmationReportListDal aReportDal = new ConfirmationReportListDal();
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);
        
        //aReportDal.LoadIncrementType(ddlIncrementType);
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

    private void LoadInfoIncrement()
    {
        DataTable dataTable = aReportDal.LoadInfoIncrementInfoDAL(IncrementGenerateParamiterList());

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

    private String IncrementGenerateParamiterList()
    {
        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EG.CompanyId = " + ddlCompany.SelectedValue;
        }


        if (IncrementFromDt.Text != string.Empty && IncrementToDt.Text != string.Empty)
        {
            parameter = parameter + " AND EG.DateOfConformation  BETWEEN '" + IncrementFromDt.Text + "' AND '" + IncrementToDt.Text + "' ";
        }
        if (IncrementFromDt.Text != string.Empty && IncrementToDt.Text == string.Empty)
        {
            parameter = parameter + " AND EG.DateOfConformation  BETWEEN '" + IncrementFromDt.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (IncrementFromDt.Text == string.Empty && IncrementToDt.Text != string.Empty)
        {
            parameter = parameter + " AND EG.DateOfConformation  BETWEEN '" + IncrementToDt.Text + "' AND '" + IncrementToDt.Text + "' ";
        }


        //if (SeparationHierchicalParameter() != string.Empty)
        //{
        //    parameter = parameter + SeparationHierchicalParameter();
        //}

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
        Response.Redirect("ContractRenewalReport.aspx");
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_ProbationaryEmployee.Rows.Count > 0)
        {
            string attachment = "attachment; filename= Confirmation List_" + DateTime.Now.ToString("dd-MMM-yyyy") +
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
   <h3 Confirmation List</h3>

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

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}