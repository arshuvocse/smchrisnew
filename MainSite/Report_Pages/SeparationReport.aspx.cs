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
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_SeparationReport : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    SeparationReportDal aReportDal = new SeparationReportDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["finalup"] = "";
            SeparationFromDt.Attributes.Add("readonly", "readonly");
            SeparationToDT.Attributes.Add("readonly", "readonly");

            DropDown();

            LoadHeierchicalTree();
            TreeViewSeparationList.CollapseAll();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private void LoadHeierchicalTree()
    {
        TreeViewSeparationList.Nodes.Clear();
        AddTree(TreeViewSeparationList);
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
    private void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany_OnSelectedIndexChanged(null, null);
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

    private void LoadInfoSeparation()
    {

        DataTable dataTable = aReportDal.LoadInfoSeparationDAL(SeprationGenerateParamiterList(), SeprationGenerateParamiterSP());

        if (dataTable.Rows.Count > 0)
        {
            gv_SeparationList.DataSource = dataTable;
            gv_SeparationList.DataBind();

            //for (int i = 0; i < gv_SeparationList.Rows.Count; i++)
            //{
            //    if (gv_SeparationList.DataKeys[i]["Status"].ToString() == "1")
            //    {
            //        Label statusButton = (Label)gv_SeparationList.Rows[i].FindControl("statusLabel");
            //        statusButton.Attributes.Add("class", "btn btn-outline-success btn-block disabled btn-sm");
            //        statusButton.Text = "Approved";
            //        var chkBoxRows = (CheckBox)gv_SeparationList.Rows[i].Cells[0].FindControl("chkSelect");
            //        chkBoxRows.Checked = true;
            //        chkBoxRows.Enabled = false;
            //    }
            //    else
            //    {
            //        Label statusButton = (Label)gv_SeparationList.Rows[i].FindControl("statusLabel");
            //        statusButton.Attributes.Add("class", "btn btn-outline-primary btn-block disabled btn-sm");
            //        statusButton.Text = "Pending";
            //    }
            //}
        }
        else
        {
            gv_SeparationList.DataSource = null;
            gv_SeparationList.DataBind();
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



    private string SeprationGenerateParamiterSP()
    {

        string parameter = "   ";

        //if (ddlCompany.SelectedValue != "")
        //{
        //    parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        //}


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

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        //if (cblHeader.SelectedValue == "SeparationList")
        //{

        gv_SeparationList.DataSource = null;
        gv_SeparationList.DataBind();
        LoadInfoSeparation();
        //}
        //else
        //{
            
        //}
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SeparationReport.aspx");
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
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

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void btn_Approve_OnClick(object sender, EventArgs e)
    {
        if (ApproveValidation())
        {
            for (int i = 0; i < gv_SeparationList.Rows.Count; i++)
            {
                var chkBoxRows = (CheckBox)gv_SeparationList.Rows[i].Cells[0].FindControl("chkSelect");

                if (chkBoxRows.Checked && gv_SeparationList.DataKeys[i]["Status"].ToString() != "1")
                {
                    var aListDao = new SeperationListDao();

                    aListDao.EmpInfoId = Convert.ToInt32(gv_SeparationList.DataKeys[i]["EmpInfoId"]);
                    aListDao.EmpMasterCode = gv_SeparationList.Rows[i].Cells[2].Text.Trim();
                    aListDao.ZID = gv_SeparationList.Rows[i].Cells[1].Text.Trim();
                    aListDao.SeperationDate = Convert.ToDateTime(gv_SeparationList.Rows[i].Cells[3].Text.Trim());
                    aListDao.SeperationTypeId = Convert.ToInt32(gv_SeparationList.DataKeys[i]["JobLeftTypeId"]);
                    aListDao.Approveby = Session["LoginName"].ToString();
                    aListDao.ApproveDate = DateTime.Now;

                    aReportDal.SaveSeperationConfirmationList(aListDao);
                }
            }

            btn_Save_OnClick(null, null);
        }
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    private bool ApproveValidation()
    {
        int count = 0;

        for (int i = 0; i < gv_SeparationList.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_SeparationList.Rows[i].Cells[0].FindControl("chkSelect");

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

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_SeparationList.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_SeparationList.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_SeparationList.Rows[i].Cells[0].FindControl("chkSelect");

            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }
}