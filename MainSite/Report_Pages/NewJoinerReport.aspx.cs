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
using iTextSharp.text.xml.xmp;

public partial class Report_Pages_NewoinerReport : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    NewJoinerReportDal aReportDal = new NewJoinerReportDal();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["finalup"] = "";
            joiningDtFrTextBox.Attributes.Add("readonly", "readonly");
            joiningDtToTextBox.Attributes.Add("readonly", "readonly");

            DropDown();

            LoadHeierchicalTree();
            heirerchicalTreeView.CollapseAll();
        }

        
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'CheckupGridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    private void LoadHeierchicalTree()
    {
        heirerchicalTreeView.Nodes.Clear();
        AddTree(heirerchicalTreeView);
    }

    private void DropDown()
    {
        aReportDal.LoadCompanyDropDownList(ddlCompany);
        //ddlCompany_OnSelectedIndexChanged(null, null);
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlCompany.SelectedValue != "")
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;

        }
    }


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "" && joiningDtFrTextBox.Text != "" && joiningDtToTextBox.Text != "")
        {
        //    if (cblHeader.SelectedValue == "NewJoinerList")
        //    {
                LoadNewJoinerList();
            //}
            //else
            //{
            //    gv_NewJoinerList.DataSource = null;
            //    gv_NewJoinerList.DataBind();
            //}

        }
        else
        {
            aShowMessage.ShowMessageBox("Please Fill all Filtering Criteria!!", this);
        }

    }

    private void LoadNewJoinerList()
    {
        DataTable dataTable = aReportDal.NewJoinerListDAL(ParamiterNewJoinerList(), ParamiterNewJoinerList2());

        if (dataTable.Rows.Count > 0)
        {
            gv_NewJoinerList.DataSource = dataTable;
            gv_NewJoinerList.DataBind();

            for (int i = 0; i < gv_NewJoinerList.Rows.Count ; i++)
            {

                Label lbl_GradeCode = (Label)gv_NewJoinerList.Rows[i].FindControl("lbl_GradeCode");
                HiddenField id_empId = (HiddenField)gv_NewJoinerList.Rows[i].FindControl("id_empId");
                Label lbl_Basic = (Label)gv_NewJoinerList.Rows[i].FindControl("lbl_Basic");
                Label lbl_HouseRent = (Label)gv_NewJoinerList.Rows[i].FindControl("lbl_HouseRent");


        
                Label lbl_MedicalAllowance = (Label)gv_NewJoinerList.Rows[i].FindControl("lbl_MedicalAllowance");
                Label lbl_ConveyanceAllowance = (Label)gv_NewJoinerList.Rows[i].FindControl("lbl_ConveyanceAllowance");
                Label lbl_WashingAllowance = (Label)gv_NewJoinerList.Rows[i].FindControl("lbl_WashingAllowance");


                decimal basicAmount = 0;
                try
                {
                      basicAmount = Convert.ToDecimal(lbl_Basic.Text);


                      lbl_Basic.Text = Math.Round(basicAmount, 0).ToString();
                }
                catch (Exception)
                {
                    
                    //throw;
                }

                if (lbl_Basic.Text!="")
                {


                    if (lbl_GradeCode.Text.Trim() == "Special" || lbl_GradeCode.Text.Trim() == "M-1" ||
                        lbl_GradeCode.Text.Trim() == "M-2A" || lbl_GradeCode.Text.Trim() == "M-2B" ||
                        lbl_GradeCode.Text.Trim() == "M-3A" || lbl_GradeCode.Text.Trim() == "M-3B" ||
                        lbl_GradeCode.Text.Trim() == "M-4" || lbl_GradeCode.Text.Trim() == "M-5")
                    {
                        decimal Medical = 0;
                        decimal HouseResnt = 0;
                        decimal Conveyance = 0;
                        decimal Washing = 0;


                     
                            HouseResnt = (Math.Round(basicAmount, 0)*50)/100;
                         
                       
                            Medical = (Math.Round(basicAmount, 0)*10)/100;
                          


                         

                        lbl_HouseRent.Text = Math.Round(HouseResnt, 0).ToString();




                        lbl_MedicalAllowance.Text = Math.Round(Medical, 0).ToString();
                        lbl_ConveyanceAllowance.Text = Math.Round(Conveyance, 0).ToString();
                        lbl_WashingAllowance.Text = Math.Round(Washing, 0).ToString();
                    }


                    if (lbl_GradeCode.Text.Trim() == "M-6A" || lbl_GradeCode.Text.Trim() == "M-6B" || lbl_GradeCode.Text.Trim() == "M-7" || lbl_GradeCode.Text.Trim() == "M-8" || lbl_GradeCode.Text.Trim() == "M-9")
                                           
                    {
                        decimal Medical = 0;
                        decimal HouseResnt = 0;
                        decimal Conveyance = 0;

                        decimal Washing = 0;
                       
                        
                            HouseResnt = (Math.Round(basicAmount, 0) * 75) / 100;
                       

                       
                            Medical = (Math.Round(basicAmount, 0) * 10) / 100;
                             

                        
                            Conveyance = (Math.Round(basicAmount, 0) * 5) / 100;


                            lbl_HouseRent.Text = Math.Round(HouseResnt, 0).ToString(); 
                        lbl_MedicalAllowance.Text = Math.Round(Medical, 0).ToString();
                            lbl_ConveyanceAllowance.Text = Math.Round(Conveyance, 0).ToString();
                            lbl_WashingAllowance.Text = Math.Round(Washing, 0).ToString();
                    }



                    if (lbl_GradeCode.Text.Trim() == "S-5" || lbl_GradeCode.Text.Trim() == "S-4" ||
                                     lbl_GradeCode.Text.Trim() == "S-3" || lbl_GradeCode.Text.Trim() == "S-2" ||
                                     lbl_GradeCode.Text.Trim() == "S-1A" ||
                                     lbl_GradeCode.Text.Trim() == "S-1B" ||
                                     lbl_GradeCode.Text.Trim() == "SS-5" ||
                                     lbl_GradeCode.Text.Trim() == "S-1A" ||
                                     lbl_GradeCode.Text.Trim() == "SS-4" ||
                                     lbl_GradeCode.Text.Trim() == "S-1A" ||
                                     lbl_GradeCode.Text.Trim() == "SS-3" ||
                                     lbl_GradeCode.Text.Trim() == "SS-2" ||
                                     lbl_GradeCode.Text.Trim() == "S-1A" ||
                                     lbl_GradeCode.Text.Trim() == "SS-1A" ||
                                     lbl_GradeCode.Text.Trim() == "SS-1B"

                                      ||
                                     lbl_GradeCode.Text.Trim() == "S-1" ||
                                     lbl_GradeCode.Text.Trim() == "SS-1" ||
                                     lbl_GradeCode.Text.Trim() == "SS-1B" ||
                                     lbl_GradeCode.Text.Trim() == "M-3" ||
                                     lbl_GradeCode.Text.Trim() == "M-2" ||
                                     lbl_GradeCode.Text.Trim() == "M-6" ||
                                     lbl_GradeCode.Text.Trim() == "M-0" ||
                                     lbl_GradeCode.Text.Trim() == "S-0")
                    {
                        decimal Medical = 0;
                        decimal HouseResnt = 0;
                        decimal Conveyance = 0;
                        decimal Washing = 0;

                      
                            HouseResnt = (Math.Round(basicAmount, 0) * 63) / 100;
                        

                       
                            Medical = 0;
                        
                       
                            Conveyance = 0;
                          

                      

                        lbl_HouseRent.Text = Math.Round(HouseResnt, 0).ToString();
                        lbl_MedicalAllowance.Text = Math.Round(Medical, 0).ToString();
                        lbl_ConveyanceAllowance.Text = Math.Round(Conveyance, 0).ToString();
                        lbl_WashingAllowance.Text = Math.Round(Washing, 0).ToString();
                    }


                }

          
              //  lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();

               
            }
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
            parameter = parameter + " AND E.CompanyId = " + ddlCompany.SelectedValue;
        }
        //if (HierchicalParameter() != string.Empty)
        //{
        //    parameter = parameter + HierchicalParameter();
        //}
        if (joiningDtFrTextBox.Text != string.Empty && joiningDtToTextBox.Text != string.Empty)
        {
            parameter = parameter + " AND E.DateOfJoin BETWEEN '" + joiningDtFrTextBox.Text + "' AND '" + joiningDtToTextBox.Text + "' ";
        }
        if (joiningDtFrTextBox.Text != string.Empty && joiningDtToTextBox.Text == string.Empty)
        {
            parameter = parameter + " AND E.DateOfJoin BETWEEN '" + joiningDtFrTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (joiningDtFrTextBox.Text == string.Empty && joiningDtToTextBox.Text != string.Empty)
        {
            parameter = parameter + " AND E.DateOfJoin BETWEEN '" + joiningDtToTextBox.Text + "' AND '" + joiningDtToTextBox.Text + "' ";
        }

        return parameter;
    }

    private string ParamiterNewJoinerList2()
    {
        string parameter = "    ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND E.CompanyId = " + ddlCompany.SelectedValue;
        }
        //if (HierchicalParameter() != string.Empty)
        //{
        //    parameter = parameter + HierchicalParameter();
        //}
        if (joiningDtFrTextBox.Text != string.Empty && joiningDtToTextBox.Text != string.Empty)
        {
            parameter = parameter + " AND trns.EffectiveDate BETWEEN '" + joiningDtFrTextBox.Text + "' AND '" + joiningDtToTextBox.Text + "' ";
        }
        if (joiningDtFrTextBox.Text != string.Empty && joiningDtToTextBox.Text == string.Empty)
        {
            parameter = parameter + " AND trns.EffectiveDate BETWEEN '" + joiningDtFrTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (joiningDtFrTextBox.Text == string.Empty && joiningDtToTextBox.Text != string.Empty)
        {
            parameter = parameter + " AND trns.EffectiveDate BETWEEN '" + joiningDtToTextBox.Text + "' AND '" + joiningDtToTextBox.Text + "' ";
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
    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("NewoinerReport.aspx");
    }
    
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        //if (cblHeader.SelectedValue != "")
        //{
        //    if (cblHeader.SelectedValue == "NewJoinerList")
        //    {
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
            //}
        //}
        //else
        //{
        //    aShowMessage.ShowMessageBox("Please Select a Report Name!!", this);
        //}
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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
}